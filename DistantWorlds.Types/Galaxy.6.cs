using BaconDistantWorlds;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DistantWorlds.Types
{
    public partial class Galaxy
    {
        public string GenerateRuinAbilitiesSummary(Ruin ruin)
        {
            string empty = string.Empty;
            bool flag = CheckRuinsHaveBenefit(ruin, PlayerEmpire);
            bool flag2 = false;
            if ((!ruin.PlayerEmpireEncountered || ruin.Type != RuinType.UnlockResearchProject) && (!ruin.PlayerEmpireEncountered || flag || 1 == 0))
            {
                empty = empty + "(" + TextResolver.GetText("Not Investigated - Details Unknown") + ")";
            }
            else
            {
                empty = empty + string.Format(TextResolver.GetText("X Development bonus for colony"), ruin.DevelopmentBonus.ToString("+#%")) + "\n\n";
                if (ruin.BonusDefensive > 0.0)
                {
                    empty += string.Format(TextResolver.GetText("Ruins Bonus Defensive"), TextResolver.GetText("Colony").ToLower(CultureInfo.InvariantCulture), ruin.BonusDefensive.ToString("#%"));
                    empty = empty.Substring(0, empty.Length - 2);
                }
                if (ruin.BonusDiplomacy > 0.0)
                {
                    empty += string.Format(TextResolver.GetText("Ruins Bonus Diplomacy"), ruin.BonusDiplomacy.ToString("+#%"));
                }
                if (ruin.BonusHappiness > 0.0)
                {
                    empty += string.Format(TextResolver.GetText("Ruins Bonus Happiness"), ruin.BonusHappiness.ToString("+#%"));
                }
                if (ruin.BonusResearchEnergy > 0.0)
                {
                    empty += string.Format(TextResolver.GetText("Ruins Bonus Energy Research"), ruin.BonusResearchEnergy.ToString("+#%"));
                }
                if (ruin.BonusResearchHighTech > 0.0)
                {
                    empty += string.Format(TextResolver.GetText("Ruins Bonus HighTech Research"), ruin.BonusResearchHighTech.ToString("+#%"));
                }
                if (ruin.BonusResearchWeapons > 0.0)
                {
                    empty += string.Format(TextResolver.GetText("Ruins Bonus Weapons Research"), ruin.BonusResearchWeapons.ToString("+#%"));
                }
                if (ruin.BonusWealth > 0.0)
                {
                    empty += string.Format(TextResolver.GetText("Ruins Bonus Colony Income"), ruin.BonusWealth.ToString("+#%"));
                }
            }
            return empty;
        }

        public bool SelectSpecialRuins(Habitat habitat, EventMessageType eventMessageType)
        {
            return SelectSpecialRuins(habitat, eventMessageType, null, 0, allowCreatures: true);
        }

        public bool SelectSpecialRuins(Habitat habitat, EventMessageType eventMessageType, bool allowCreatures)
        {
            return SelectSpecialRuins(habitat, eventMessageType, null, 0, allowCreatures);
        }

        public bool SelectSpecialRuins(Habitat habitat, EventMessageType eventMessageType, Race race, int specialValue)
        {
            return SelectSpecialRuins(habitat, eventMessageType, race, specialValue, allowCreatures: true);
        }

        public bool SelectSpecialRuins(Habitat habitat, EventMessageType eventMessageType, Race race, int specialValue, bool allowCreatures)
        {
            if (habitat == null)
            {
                return false;
            }
            if (habitat.Ruin != null)
            {
                return true;
            }
            if (eventMessageType == EventMessageType.AncientBattleDebrisField || eventMessageType == EventMessageType.CreatureOutbreak || eventMessageType == EventMessageType.FreeSuperShip || eventMessageType == EventMessageType.IndependentPopulation || eventMessageType == EventMessageType.LostColonyFound || eventMessageType == EventMessageType.NewEmpireEmerges || eventMessageType == EventMessageType.NewEmpireRaceAbility || eventMessageType == EventMessageType.PirateFactionJoinsYou || eventMessageType == EventMessageType.TreasureFound)
            {
                throw new ArgumentException("SelectSpecialRuins cannot support this eventMessageType");
            }
            string name = TextResolver.GetText("Ancient Ruins");
            int pictureRef = 0;
            Habitat habitat2 = DetermineHabitatSystemStar(habitat);
            SelectRelativeHabitatSurfacePoint(habitat, out var x, out var y);
            Ruin ruin = new Ruin(name, 0, 0.1 + Rnd.NextDouble() * 0.2, x, y, 0, 0, 0);
            int num = 0;
            switch (eventMessageType)
            {
                case EventMessageType.ExoticTechDiscovered:
                    {
                        int num3 = 0;
                        ResearchNodeDefinitionList researchNodeDefinitionList = ResearchNodeDefinitions.FindAllNodesBySpecialFunctionCode(3);
                        if (researchNodeDefinitionList.Count > 0)
                        {
                            int index = Rnd.Next(0, researchNodeDefinitionList.Count);
                            num3 = researchNodeDefinitionList[index].ResearchNodeId;
                            switch (Rnd.Next(0, 2))
                            {
                                case 0:
                                    name = string.Format(TextResolver.GetText("Hidden Fortress of X"), habitat2.Name);
                                    pictureRef = 14;
                                    break;
                                case 1:
                                    name = TextResolver.GetText("Nexus of the Red Claw");
                                    pictureRef = 7;
                                    break;
                            }
                            ruin.Type = RuinType.Component;
                            ruin.ResearchProjectId = num3;
                        }
                        else
                        {
                            name = GenerateRuinName(habitat, out pictureRef);
                        }
                        break;
                    }
                case EventMessageType.GalacticRefugees:
                    ruin.Type = RuinType.Refugees;
                    ruin.RefugeesGenerated = false;
                    name = string.Format(TextResolver.GetText("Great Beacon of X"), habitat2.Name);
                    pictureRef = 6;
                    break;
                case EventMessageType.LostBuiltObjectCoordinates:
                    ruin.Type = RuinType.LostBuiltObject;
                    ruin.LostBuiltObjectGenerated = false;
                    name = GenerateRuinName(habitat, out pictureRef);
                    break;
                case EventMessageType.LostColonyCoordinates:
                    ruin.Type = RuinType.LostColony;
                    ruin.LostColonyGenerated = false;
                    name = GenerateRuinName(habitat, out pictureRef);
                    break;
                case EventMessageType.OriginsDiscovery:
                    ruin.OriginsRace = race;
                    ruin.Type = RuinType.Origins;
                    ruin.OriginsApprovalRatingBonus = specialValue;
                    name = string.Format(TextResolver.GetText("Great Archives of X"), habitat2.Name);
                    pictureRef = 10;
                    break;
                case EventMessageType.SleepersAwake:
                    {
                        Race race2 = null;
                        int iterationCount = 0;
                        while (ConditionCheckLimit(race2 == null || RaceFamilies.GetIdsBySpecialFunctionCode(1).Contains(race2.FamilyId) || !race2.Playable, 100, ref iterationCount))
                        {
                            race2 = SelectRace(habitat.Type, RaceFamilies.GetIdsBySpecialFunctionCode(1));
                            if (race2 == null)
                            {
                                num = Rnd.Next(0, 20);
                                race2 = Races[num];
                            }
                        }
                        if (race2 == null)
                        {
                            race2 = Races[0];
                        }
                        ruin.Type = RuinType.NewPopulation;
                        ruin.HabitatNewRace = race2;
                        if (habitat.Quality < 0.5f)
                        {
                            habitat.BaseQuality = (float)(0.5 + Rnd.NextDouble() * 0.3);
                        }
                        if (race2 != null)
                        {
                            name = string.Format(TextResolver.GetText("Silent Chamber of the RACE"), race2.Name);
                        }
                        pictureRef = ((habitat.Type != HabitatType.Ice) ? 1 : 0);
                        break;
                    }
                case EventMessageType.SpecialGovernmentType:
                    {
                        int num2 = -1;
                        GovernmentAttributes firstByAvailability = Governments.GetFirstByAvailability(2);
                        GovernmentAttributes firstByAvailability2 = Governments.GetFirstByAvailability(3);
                        if (firstByAvailability != null && firstByAvailability2 != null)
                        {
                            if (_RuinsGovernmentWayOfAncients < _RuinsGovernmentWayOfDarkness)
                            {
                                num2 = firstByAvailability.GovernmentId;
                                _RuinsGovernmentWayOfAncients++;
                                name = string.Format(TextResolver.GetText("Imperial Archive X"), Rnd.Next(100, 1000).ToString());
                                pictureRef = 11;
                            }
                            else
                            {
                                num2 = firstByAvailability2.GovernmentId;
                                _RuinsGovernmentWayOfDarkness++;
                                name = TextResolver.GetText("Temple of Eternal Blackness");
                                pictureRef = 12;
                            }
                            ruin.Type = RuinType.Government;
                            ruin.SpecialGovernmentId = num2;
                        }
                        break;
                    }
            }
            ruin.Name = name;
            ruin.PictureRef = pictureRef;
            ruin.Description = SelectRuinDescription(habitat);
            RuinCount++;
            if (!_RuinsHabitats.Contains(habitat))
            {
                _RuinsHabitats.Add(habitat);
            }
            if (habitat.Owner == null && allowCreatures && Rnd.Next(0, 3) == 1 && _CreaturePrevalence > 0.0)
            {
                switch (Rnd.Next(0, 3))
                {
                    case 0:
                        GenerateCreatureAtHabitat(CreatureType.Ardilus, habitat, lockLocation: true);
                        break;
                    case 1:
                        if (AllowGiantKaltorGeneration)
                        {
                            GenerateCreatureAtHabitat(CreatureType.Kaltor, habitat, lockLocation: true);
                        }
                        break;
                    case 2:
                        if (AllowGiantKaltorGeneration)
                        {
                            int num4 = Rnd.Next(2, 4);
                            for (int i = 0; i < num4; i++)
                            {
                                GenerateCreatureAtHabitat(CreatureType.Kaltor, habitat, lockLocation: true);
                            }
                        }
                        break;
                }
            }
            habitat.Ruin = ruin;
            return true;
        }

        public bool SelectRuinsUnlockTech(Habitat habitat, int researchNodeId)
        {
            if (habitat == null || researchNodeId < 0)
            {
                return false;
            }
            if (habitat.Ruin != null)
            {
                return true;
            }
            int pictureRef = 0;
            string name = GenerateRuinName(habitat, out pictureRef);
            SelectRelativeHabitatSurfacePoint(habitat, out var x, out var y);
            Ruin ruin = new Ruin(name, pictureRef, 0.1 + Rnd.NextDouble() * 0.2, x, y, 0, 0, 0);
            ruin.Type = RuinType.UnlockResearchProject;
            ruin.ResearchProjectId = researchNodeId;
            ruin.Description = SelectRuinDescription(habitat);
            RuinCount++;
            if (!_RuinsHabitats.Contains(habitat))
            {
                _RuinsHabitats.Add(habitat);
            }
            habitat.Ruin = ruin;
            return true;
        }

        public Habitat FindPlanetMoonBeyondRangeOrFurthestNoRuins(Habitat center, HabitatList habitats, double range, out double distance)
        {
            Habitat result = null;
            distance = 0.0;
            double num = 0.0;
            double num2 = range * range;
            if (center != null && habitats != null)
            {
                for (int i = 0; i < habitats.Count; i++)
                {
                    Habitat habitat = habitats[i];
                    if (habitat == null || habitat == center || (habitat.Category != HabitatCategoryType.Planet && habitat.Category != HabitatCategoryType.Moon) || habitat.Ruin != null)
                    {
                        continue;
                    }
                    bool flag = false;
                    switch (habitat.Type)
                    {
                        case HabitatType.Volcanic:
                        case HabitatType.Desert:
                        case HabitatType.MarshySwamp:
                        case HabitatType.Continental:
                        case HabitatType.Ocean:
                        case HabitatType.BarrenRock:
                        case HabitatType.Ice:
                            flag = true;
                            break;
                    }
                    if (flag)
                    {
                        double num3 = CalculateDistanceSquared(center.Xpos, center.Ypos, habitat.Xpos, habitat.Ypos);
                        if (num3 >= num2)
                        {
                            distance = Math.Sqrt(num3);
                            return habitat;
                        }
                        if (num3 > num)
                        {
                            result = habitat;
                            num = num3;
                        }
                    }
                }
            }
            distance = Math.Sqrt(num);
            return result;
        }

        private Race SelectRace(HabitatType nativeHabitatType)
        {
            return SelectRace(nativeHabitatType, byte.MaxValue);
        }

        private Race SelectRace(HabitatType nativeHabitatType, byte raceFamilyIdToExclude)
        {
            return SelectRace(nativeHabitatType, new List<byte> { raceFamilyIdToExclude });
        }

        private Race SelectRace(HabitatType nativeHabitatType, List<byte> raceFamilyIdsToExclude)
        {
            Race race = null;
            int num = 0;
            while ((race == null || race.NativeHabitatType != nativeHabitatType || raceFamilyIdsToExclude.Contains(race.FamilyId) || !race.Playable) && num < 100)
            {
                int index = Rnd.Next(0, 20);
                race = Races[index];
                num++;
            }
            return race;
        }

        public void GenerateSilverMistRuins()
        {
            if (!GameDisasterEventsEnabled || !(_CreaturePrevalence > 0.0))
            {
                return;
            }
            int num = Math.Max(1, Math.Min(3, StarCount / 450));
            for (int i = 0; i < num; i++)
            {
                Habitat habitat = FindLonelyHabitat(RuinType.CreatureSwarmSilverMist);
                if (habitat != null)
                {
                    GenerateSilverMistRuin(habitat);
                    SilverMistCreatureRuinsHabitat = habitat;
                }
            }
        }

        public void GenerateSilverMistRuin(Habitat habitat)
        {
            string description = SelectRuinDescription(habitat);
            int pictureRef = 0;
            string name = GenerateRuinName(habitat, out pictureRef);
            SelectRelativeHabitatSurfacePoint(habitat, out var x, out var y);
            Ruin ruin = new Ruin(name, pictureRef, 0.1 + Rnd.NextDouble() * 0.2, x, y, 0, 0, 0);
            ruin.Type = RuinType.CreatureSwarmSilverMist;
            ruin.Description = description;
            habitat.Ruin = ruin;
            RuinCount++;
            if (!_RuinsHabitats.Contains(habitat))
            {
                _RuinsHabitats.Add(habitat);
            }
        }

        public void SelectRuins(Habitat habitat)
        {
            SelectRuins(habitat, definitePlacement: false, assignCreatures: true, allowNegativeEffects: true);
        }

        public void SelectRuins(Habitat habitat, bool definitePlacement, bool assignCreatures, bool allowNegativeEffects)
        {
            SelectRuins(habitat, definitePlacement, assignCreatures, allowNegativeEffects, allowMapReveal: true);
        }

        public void SelectRuins(Habitat habitat, bool definitePlacement, bool assignCreatures, bool allowNegativeEffects, bool allowMapReveal)
        {
            if (habitat.Ruin != null || (!definitePlacement && ((habitat.Empire != null && habitat.Empire != IndependentEmpire) || habitat.Category == HabitatCategoryType.Asteroid || habitat.Category == HabitatCategoryType.Star || habitat.Category == HabitatCategoryType.GasCloud || habitat.Diameter < 60 || (habitat.Type != HabitatType.Continental && habitat.Type != HabitatType.MarshySwamp && habitat.Type != HabitatType.Desert))))
            {
                return;
            }
            double num = 1.0;
            double num2 = Rnd.NextDouble();
            double num3 = 0.0;
            bool flag = false;
            int researchBonus = 0;
            int mapSystemReveal = 0;
            int moneyBonus = 0;
            if (habitat.Owner == null)
            {
                if (allowMapReveal)
                {
                    switch (Rnd.Next(0, 3))
                    {
                        case 0:
                            researchBonus = Rnd.Next(60000, 120000);
                            break;
                        case 1:
                            mapSystemReveal = Rnd.Next(7, 14);
                            break;
                        case 2:
                            moneyBonus = Rnd.Next(4000, 10000);
                            break;
                    }
                }
                else
                {
                    switch (Rnd.Next(0, 2))
                    {
                        case 0:
                            researchBonus = Rnd.Next(60000, 120000);
                            break;
                        case 1:
                            moneyBonus = Rnd.Next(4000, 10000);
                            break;
                    }
                }
            }
            RuinType type = RuinType.Standard;
            if (allowNegativeEffects && (_CreaturePrevalence > 0.0 || _PiratePrevalence > 0.0) && Rnd.Next(0, 4) == 1)
            {
                researchBonus = 0;
                mapSystemReveal = 0;
                moneyBonus = 0;
                int num4 = Rnd.Next(0, 2);
                if (_CreaturePrevalence <= 0.0 || !AllowGiantKaltorGeneration)
                {
                    num4 = 1;
                }
                else if (_PiratePrevalence <= 0.0)
                {
                    num4 = 0;
                }
                switch (num4)
                {
                    case 0:
                        type = RuinType.CreatureSwarm;
                        break;
                    case 1:
                        type = RuinType.PirateAmbush;
                        break;
                }
            }
            string description = SelectRuinDescription(habitat);
            int pictureRef = 0;
            string name = GenerateRuinName(habitat, out pictureRef);
            double x;
            double y;
            switch (habitat.Type)
            {
                case HabitatType.Continental:
                    num3 = num * 0.16;
                    if (definitePlacement || num2 <= num3)
                    {
                        SelectRelativeHabitatSurfacePoint(habitat, out x, out y);
                        Ruin ruin2 = (habitat.Ruin = new Ruin(name, pictureRef, 0.1 + Rnd.NextDouble() * 0.2, x, y, researchBonus, mapSystemReveal, moneyBonus));
                        flag = true;
                    }
                    break;
                case HabitatType.MarshySwamp:
                    num3 = num * 0.16;
                    if (definitePlacement || num2 <= num3)
                    {
                        SelectRelativeHabitatSurfacePoint(habitat, out x, out y);
                        Ruin ruin5 = (habitat.Ruin = new Ruin(name, pictureRef, 0.1 + Rnd.NextDouble() * 0.2, x, y, researchBonus, mapSystemReveal, moneyBonus));
                        flag = true;
                    }
                    break;
                case HabitatType.Desert:
                    num3 = num * 0.06;
                    if (definitePlacement || num2 <= num3)
                    {
                        SelectRelativeHabitatSurfacePoint(habitat, out x, out y);
                        Ruin ruin3 = (habitat.Ruin = new Ruin(name, pictureRef, 0.1 + Rnd.NextDouble() * 0.2, x, y, researchBonus, mapSystemReveal, moneyBonus));
                        flag = true;
                    }
                    break;
                case HabitatType.Ocean:
                    num3 = num * 0.06;
                    if (definitePlacement || num2 <= num3)
                    {
                        SelectRelativeHabitatSurfacePoint(habitat, out x, out y);
                        Ruin ruin6 = (habitat.Ruin = new Ruin(name, pictureRef, 0.1 + Rnd.NextDouble() * 0.2, x, y, researchBonus, mapSystemReveal, moneyBonus));
                        flag = true;
                    }
                    break;
                case HabitatType.Ice:
                    num3 = num * 0.06;
                    if (definitePlacement || num2 <= num3)
                    {
                        SelectRelativeHabitatSurfacePoint(habitat, out x, out y);
                        Ruin ruin7 = (habitat.Ruin = new Ruin(name, pictureRef, 0.1 + Rnd.NextDouble() * 0.2, x, y, researchBonus, mapSystemReveal, moneyBonus));
                        flag = true;
                    }
                    break;
                case HabitatType.Volcanic:
                    num3 = num * 0.06;
                    if (definitePlacement || num2 <= num3)
                    {
                        SelectRelativeHabitatSurfacePoint(habitat, out x, out y);
                        Ruin ruin4 = (habitat.Ruin = new Ruin(name, pictureRef, 0.1 + Rnd.NextDouble() * 0.2, x, y, researchBonus, mapSystemReveal, moneyBonus));
                        flag = true;
                    }
                    break;
                case HabitatType.BarrenRock:
                    num3 = num * 0.06;
                    if (definitePlacement || num2 <= num3)
                    {
                        SelectRelativeHabitatSurfacePoint(habitat, out x, out y);
                        Ruin ruin = (habitat.Ruin = new Ruin(name, pictureRef, 0.1 + Rnd.NextDouble() * 0.2, x, y, researchBonus, mapSystemReveal, moneyBonus));
                        flag = true;
                    }
                    break;
            }
            if (habitat.Ruin != null)
            {
                habitat.Ruin.Type = type;
                habitat.Ruin.Description = description;
            }
            if (!flag)
            {
                return;
            }
            RuinCount++;
            if (!_RuinsHabitats.Contains(habitat))
            {
                _RuinsHabitats.Add(habitat);
            }
            if (!assignCreatures || habitat.Owner != null || Rnd.Next(0, 3) != 1 || !(_CreaturePrevalence > 0.0))
            {
                return;
            }
            switch (Rnd.Next(0, 3))
            {
                case 0:
                    GenerateCreatureAtHabitat(CreatureType.Ardilus, habitat, lockLocation: true);
                    break;
                case 1:
                    if (AllowGiantKaltorGeneration)
                    {
                        GenerateCreatureAtHabitat(CreatureType.Kaltor, habitat, lockLocation: true);
                    }
                    break;
                case 2:
                    if (AllowGiantKaltorGeneration)
                    {
                        int num5 = Rnd.Next(2, 4);
                        for (int i = 0; i < num5; i++)
                        {
                            GenerateCreatureAtHabitat(CreatureType.Kaltor, habitat, lockLocation: true);
                        }
                    }
                    break;
            }
        }

        private string SelectRuinDescription(Habitat habitat)
        {
            string empty = string.Empty;
            string[] array = new string[6]
            {
            TextResolver.GetText("From our position in orbit we can see that"),
            TextResolver.GetText("Our orbital inspection reveals that"),
            TextResolver.GetText("Surveying from orbit indicates that"),
            TextResolver.GetText("Our scanners show that"),
            TextResolver.GetText("Our sensors indicate that"),
            TextResolver.GetText("From our orbital inspection we detect that")
            };
            string[] array2 = new string[5]
            {
            TextResolver.GetText("appear completely deserted"),
            TextResolver.GetText("have been vacated for a considerable time"),
            TextResolver.GetText("have lain undisturbed for centuries"),
            TextResolver.GetText("are in an advanced state of decay and disrepair"),
            TextResolver.GetText("date from an extremely distant past age")
            };
            string[] array3 = new string[5]
            {
            TextResolver.GetText("lie deep in a dense forest"),
            TextResolver.GetText("sit on a rocky mountain outcrop"),
            TextResolver.GetText("are located in the midst of a grassy plain"),
            TextResolver.GetText("sit at the bottom of a forest-covered valley"),
            TextResolver.GetText("are perched atop rocky coastal cliffs overlooking a vast ocean")
            };
            string[] array4 = new string[5]
            {
            TextResolver.GetText("are surrounded by lush jungle"),
            TextResolver.GetText("are situated on a reed-covered island in the middle of a marshy bog"),
            TextResolver.GetText("are located in a clearing in the heart of a swampy jungle"),
            TextResolver.GetText("sit in a grassy riverside clearing upstream from a large waterfall"),
            TextResolver.GetText("are half-buried in undergrowth near a huge swampy wasteland")
            };
            string[] array5 = new string[5]
            {
            TextResolver.GetText("lie half buried by deep sand drifts"),
            TextResolver.GetText("are nearly consumed by a vast sandbank"),
            TextResolver.GetText("are set into the wall of a rocky canyon"),
            TextResolver.GetText("are perched atop a precipice overlooking a vast dusty plain"),
            TextResolver.GetText("are situated atop a large mesa in a desert plain")
            };
            string[] array6 = new string[2]
            {
            TextResolver.GetText("sit on a rocky island lashed by giant storm waves from the surrounding sea"),
            TextResolver.GetText("sit half-submerged atop a vast reef in the midst of an endless ocean")
            };
            string[] array7 = new string[2]
            {
            TextResolver.GetText("lie deep in snow drifts at the peak of a windswept mountain pass"),
            TextResolver.GetText("are located near the edge of an icy tundra wasteland")
            };
            string[] array8 = new string[2]
            {
            TextResolver.GetText("sit on a stone ledge overlooking a fiery lake of lava"),
            TextResolver.GetText("lie in the midst of a vast rocky plain blanketed with thousands of geothermal steam vents")
            };
            string[] array9 = new string[2]
            {
            TextResolver.GetText("sit at the center of a shadowy, star-lit plain"),
            TextResolver.GetText("lie at the bottom of a desolate, rocky valley")
            };
            string empty2 = string.Empty;
            empty2 = habitat.Type switch
            {
                HabitatType.BarrenRock => array9[Rnd.Next(0, array9.Length)],
                HabitatType.Continental => array3[Rnd.Next(0, array3.Length)],
                HabitatType.Ice => array7[Rnd.Next(0, array7.Length)],
                HabitatType.MarshySwamp => array4[Rnd.Next(0, array4.Length)],
                HabitatType.Ocean => array6[Rnd.Next(0, array6.Length)],
                HabitatType.Desert => array5[Rnd.Next(0, array5.Length)],
                HabitatType.Volcanic => array8[Rnd.Next(0, array8.Length)],
                _ => array2[Rnd.Next(0, array2.Length)],
            };
            if (Rnd.Next(0, 4) == 1)
            {
                empty2 = array2[Rnd.Next(0, array2.Length)];
            }
            string text = array[Rnd.Next(0, array.Length)];
            return text + " " + TextResolver.GetText("the ruins") + " " + empty2 + ".";
        }

        public void SelectCreatures(Habitat habitat)
        {
            if (_CreaturePrevalence <= 0.0)
            {
                return;
            }
            double num = Rnd.NextDouble();
            if (habitat.Type == HabitatType.BarrenRock || habitat.Category == HabitatCategoryType.Asteroid)
            {
                double num2 = _CreaturePrevalence * 0.009;
                if (num <= num2)
                {
                    GenerateCreatureAtHabitat(CreatureType.RockSpaceSlug, habitat);
                }
            }
            else if (habitat.Type == HabitatType.Desert)
            {
                double num3 = _CreaturePrevalence * 0.32;
                bool flag = habitat.Resources.ContainsName("Korabbian Spice");
                if (num <= num3 || flag)
                {
                    GenerateCreatureAtHabitat(CreatureType.DesertSpaceSlug, habitat);
                    if (flag)
                    {
                        GenerateCreatureAtHabitat(CreatureType.DesertSpaceSlug, habitat);
                        GenerateCreatureAtHabitat(CreatureType.DesertSpaceSlug, habitat);
                    }
                }
            }
            else if (habitat.Type == HabitatType.FrozenGasGiant)
            {
                double num4 = _CreaturePrevalence * 0.15;
                if (AllowGiantKaltorGeneration && num <= num4)
                {
                    GenerateCreatureAtHabitat(CreatureType.Kaltor, habitat);
                }
            }
            else if (habitat.Category == HabitatCategoryType.GasCloud)
            {
                double num5 = _CreaturePrevalence * 0.15;
                if (AllowGiantKaltorGeneration && num <= num5)
                {
                    int num6 = Rnd.Next(3, 10);
                    for (int i = 0; i < num6; i++)
                    {
                        GenerateCreatureAtHabitat(CreatureType.Kaltor, habitat);
                    }
                }
            }
            else if (habitat.Type == HabitatType.GasGiant)
            {
                double num7 = _CreaturePrevalence * 0.06;
                if (num <= num7)
                {
                    GenerateCreatureAtHabitat(CreatureType.Ardilus, habitat);
                }
            }
        }

        private Creature GenerateCreatureAtHabitat(CreatureType creatureType, Habitat habitat)
        {
            return GenerateCreatureAtHabitat(creatureType, habitat, lockLocation: false);
        }

        public Creature GenerateCreatureAtHabitat(CreatureType creatureType, Habitat habitat, bool lockLocation)
        {
            return GenerateCreatureAtHabitat(creatureType, habitat, lockLocation, -2000000001, -2000000001);
        }

        public Creature GenerateCreatureAtHabitat(CreatureType creatureType, Habitat habitat, bool lockLocation, int offsetX, int offsetY)
        {
            Habitat habitat2 = DetermineHabitatSystemStar(habitat);
            switch (creatureType)
            {
                case CreatureType.SilverMist:
                    {
                        Creature creature2 = new Creature(this, CreatureType.SilverMist, habitat, offsetX, offsetY);
                        creature2.LocationLocked = false;
                        Creatures.Add(creature2);
                        creature2.NearestSystemStar = habitat2;
                        if (Systems != null && Systems.Count > habitat2.SystemIndex)
                        {
                            Systems[habitat2.SystemIndex].Creatures.Add(creature2);
                        }
                        return creature2;
                    }
                case CreatureType.Ardilus:
                    {
                        Creature creature3 = new Creature(this, CreatureType.Ardilus, habitat, offsetX, offsetY);
                        creature3.LocationLocked = lockLocation;
                        Creatures.Add(creature3);
                        creature3.NearestSystemStar = habitat2;
                        if (Systems != null && Systems.Count > habitat2.SystemIndex)
                        {
                            Systems[habitat2.SystemIndex].Creatures.Add(creature3);
                        }
                        return creature3;
                    }
                case CreatureType.DesertSpaceSlug:
                    {
                        Creature creature4 = new Creature(this, CreatureType.DesertSpaceSlug, habitat, offsetX, offsetY);
                        creature4.LocationLocked = lockLocation;
                        Creatures.Add(creature4);
                        creature4.NearestSystemStar = habitat2;
                        if (Systems != null && Systems.Count > habitat2.SystemIndex)
                        {
                            Systems[habitat2.SystemIndex].Creatures.Add(creature4);
                        }
                        return creature4;
                    }
                case CreatureType.RockSpaceSlug:
                    {
                        Creature creature5 = new Creature(this, CreatureType.RockSpaceSlug, habitat, offsetX, offsetY);
                        if (Rnd.Next(0, 30) == 1)
                        {
                            creature5.Size = Rnd.Next(300, 400);
                            creature5.MaxSize = 450;
                            creature5.AttackStrength = (int)((double)creature5.Size / 30.0);
                            creature5.DamageKillThreshhold = (int)((double)creature5.Size * 1.1);
                        }
                        creature5.LocationLocked = lockLocation;
                        Creatures.Add(creature5);
                        creature5.NearestSystemStar = habitat2;
                        if (Systems != null && Systems.Count > habitat2.SystemIndex)
                        {
                            Systems[habitat2.SystemIndex].Creatures.Add(creature5);
                        }
                        return creature5;
                    }
                case CreatureType.Kaltor:
                    {
                        Creature creature = new Creature(this, CreatureType.Kaltor, habitat, offsetX, offsetY);
                        creature.LocationLocked = lockLocation;
                        Creatures.Add(creature);
                        creature.NearestSystemStar = habitat2;
                        if (Systems != null && Systems.Count > habitat2.SystemIndex)
                        {
                            Systems[habitat2.SystemIndex].Creatures.Add(creature);
                        }
                        return creature;
                    }
                default:
                    return null;
            }
        }

        public CreatureList GenerateCreaturesAtLocation(CreatureType creatureType, int amount, double x, double y, int anchorRange, int offsetRange)
        {
            CreatureList creatureList = new CreatureList();
            Habitat habitat = FastFindNearestSystem(x, y);
            double num = double.MaxValue;
            if (habitat != null)
            {
                num = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            for (int i = 0; i < amount; i++)
            {
                int offsetX = offsetRange - Rnd.Next(0, offsetRange * 2);
                int offsetY = offsetRange - Rnd.Next(0, offsetRange * 2);
                Creature creature = null;
                switch (creatureType)
                {
                    case CreatureType.SilverMist:
                        creature = new Creature(this, CreatureType.SilverMist, null, offsetX, offsetY, new Point((int)x, (int)y), anchorRange);
                        break;
                    case CreatureType.Ardilus:
                        creature = new Creature(this, CreatureType.Ardilus, null, offsetX, offsetY, new Point((int)x, (int)y), anchorRange);
                        break;
                    case CreatureType.DesertSpaceSlug:
                        creature = new Creature(this, CreatureType.DesertSpaceSlug, null, offsetX, offsetY, new Point((int)x, (int)y), anchorRange);
                        break;
                    case CreatureType.RockSpaceSlug:
                        creature = new Creature(this, CreatureType.RockSpaceSlug, null, offsetX, offsetY, new Point((int)x, (int)y), anchorRange);
                        break;
                    case CreatureType.Kaltor:
                        creature = new Creature(this, CreatureType.Kaltor, null, offsetX, offsetY, new Point((int)x, (int)y), anchorRange);
                        break;
                }
                if (creature != null)
                {
                    creature.LocationLocked = true;
                    Creatures.Add(creature);
                    creature.NearestSystemStar = null;
                    creatureList.Add(creature);
                    if (num <= (double)MaxSolarSystemSize + 5000.0 && Systems != null && Systems.Count > habitat.SystemIndex)
                    {
                        creature.NearestSystemStar = habitat;
                        Systems[habitat.SystemIndex].Creatures.Add(creature);
                    }
                }
            }
            return creatureList;
        }

        public static double CalculateCrashResearchProgramCost(Empire empire, ResearchNode project)
        {
            double result = 0.0;
            if (project != null)
            {
                result = project.Cost - project.Progress;
                result /= 4.0;
            }
            return result;
        }

        public void ClearIndependentColoniesFromSystem(int systemIndex)
        {
            if (systemIndex < 0 || systemIndex >= Systems.Count)
            {
                return;
            }
            HabitatList habitats = Systems[systemIndex].Habitats;
            for (int i = 0; i < habitats.Count; i++)
            {
                Habitat habitat = habitats[i];
                if (habitat != null && habitat.Population != null && habitat.Population.Count > 0 && habitat.Empire == IndependentEmpire)
                {
                    habitat.ClearColony(null);
                    if (IndependentColonies.Contains(habitat))
                    {
                        IndependentColonies.Remove(habitat);
                    }
                }
            }
        }

        public void SetEmpireForAllIndependentHabitats()
        {
            if (IndependentEmpire == null)
            {
                return;
            }
            foreach (Habitat habitat in Habitats)
            {
                if (habitat.Population.Count > 0 && habitat.Population.TotalAmount > 0 && habitat.Empire == null)
                {
                    habitat.Owner = IndependentEmpire;
                    habitat.Empire = IndependentEmpire;
                }
            }
        }

        public void SetNativeResourceCargoAndStartingStrategicCargoForAllIndependentHabitats()
        {
            if (IndependentEmpire == null)
            {
                return;
            }
            foreach (Habitat habitat in Habitats)
            {
                SetNativeResourceCargo(habitat);
                SetColonyStartingStrategicResources(habitat);
            }
        }

        private void SetNativeResourceCargo(Habitat habitat)
        {
            if (habitat.Population.Count <= 0 || habitat.Population.TotalAmount <= 0)
            {
                return;
            }
            if (habitat.Cargo == null)
            {
                habitat.Cargo = new CargoList();
            }
            if (habitat.Cargo == null)
            {
                return;
            }
            if (habitat.Resources.Count > 0)
            {
                foreach (HabitatResource resource2 in habitat.Resources)
                {
                    Resource resource = new Resource(resource2.ResourceID);
                    int amount = 500 + (int)(2000.0 * Rnd.NextDouble());
                    Cargo cargo = new Cargo(resource, amount, IndependentEmpire);
                    habitat.Cargo.Add(cargo);
                }
            }
            for (int i = 0; i < ResourceSystem.FuelResources.Count; i++)
            {
                ResourceDefinition resourceDefinition = ResourceSystem.FuelResources[i];
                if (resourceDefinition != null && resourceDefinition.IsFuel)
                {
                    habitat.Cargo.Add(new Cargo(new Resource(resourceDefinition.ResourceID), 3000, IndependentEmpire));
                }
            }
            habitat.IsRefuellingDepot = true;
        }

        private void SetColonyStartingStrategicResources(Habitat habitat)
        {
            if (habitat == null || habitat.Population == null || habitat.Population.Count <= 0 || habitat.Population.TotalAmount <= 0)
            {
                return;
            }
            if (habitat.Cargo == null)
            {
                habitat.Cargo = new CargoList();
            }
            for (int i = 0; i < ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; i++)
            {
                ResourceDefinition resourceDefinition = ResourceSystem.StrategicResourcesOrderedByRelativeImportance[i];
                if (resourceDefinition != null && resourceDefinition.ColonyGrowthResourceLevel > 0f)
                {
                    habitat.Cargo.Add(new Cargo(new Resource(resourceDefinition.ResourceID), habitat.CalculateStrategicResourceConsumptionPerYear(resourceDefinition.ResourceID), IndependentEmpire));
                }
            }
        }

        private RaceList SelectHabitatTypeRaces(HabitatType type)
        {
            RaceList raceList = new RaceList();
            for (int i = 0; i < Races.Count; i++)
            {
                if (Races[i].NativeHabitatType == type)
                {
                    raceList.Add(Races[i]);
                }
            }
            return raceList.ResolvePlayableRaces();
        }

        private bool CheckLocationOverlap(GalaxyLocation location, GalaxyLocationType type)
        {
            double num = 10000.0;
            Rectangle rectangle = new Rectangle((int)((double)location.Xpos / num), (int)((double)location.Ypos / num), (int)((double)location.Width / num), (int)((double)location.Height / num));
            for (int i = 0; i < GalaxyLocations.Count; i++)
            {
                GalaxyLocation galaxyLocation = GalaxyLocations[i];
                if (galaxyLocation.Type == type)
                {
                    Rectangle rect = new Rectangle((int)((double)galaxyLocation.Xpos / num), (int)((double)galaxyLocation.Ypos / num), (int)((double)galaxyLocation.Width / num), (int)((double)galaxyLocation.Height / num));
                    if (rectangle.IntersectsWith(rect))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckLocationProximity(GalaxyLocation location, double acceptableDistance, GalaxyLocationType type)
        {
            CalculateGalaxyLocationMidPoint(location, out var x, out var y);
            for (int i = 0; i < GalaxyLocations.Count; i++)
            {
                GalaxyLocation galaxyLocation = GalaxyLocations[i];
                if (galaxyLocation.Type == type && galaxyLocation != location)
                {
                    CalculateGalaxyLocationMidPoint(galaxyLocation, out var x2, out var y2);
                    double num = CalculateDistance(x, y, x2, y2);
                    if (num < acceptableDistance)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void CalculateGalaxyLocationMidPoint(GalaxyLocation location, out double x, out double y)
        {
            x = (double)location.Xpos + (double)location.Width / 2.0;
            y = (double)location.Ypos + (double)location.Height / 2.0;
        }

        private RaceList DetermineAggressiveRaces(RaceList races, int aggressionLevel, int intelligenceLevel)
        {
            RaceList raceList = new RaceList();
            List<int> list = new List<int>();
            RaceList raceList2 = races.ResolvePlayableRaces();
            for (int i = 0; i < raceList2.Count; i++)
            {
                list.Add(raceList2[i].AggressionLevel);
            }
            Race[] array = raceList2.ToArray();
            int[] keys = list.ToArray();
            Array.Sort(keys, array);
            Array.Reverse(array);
            for (int j = 0; j < array.Length; j++)
            {
                if (array[j].AggressionLevel >= aggressionLevel && array[j].IntelligenceLevel >= intelligenceLevel)
                {
                    raceList.Add(array[j]);
                }
            }
            return raceList;
        }

        private double CheckDistanceBetweenLocations(GalaxyLocation location, GalaxyLocation otherLocation)
        {
            double num = CalculateDistance((double)location.Xpos + (double)location.Width / 2.0, (double)location.Ypos + (double)location.Height / 2.0, (double)otherLocation.Xpos + (double)otherLocation.Width / 2.0, (double)otherLocation.Ypos + (double)otherLocation.Height / 2.0);
            return num - ((double)location.Width / 2.0 + (double)otherLocation.Width / 2.0);
        }

        private double CheckDistanceFromLocation(GalaxyLocation location, double x, double y)
        {
            return CalculateDistance(x, y, (double)location.Xpos + (double)location.Width / 2.0, (double)location.Ypos + (double)location.Height / 2.0);
        }

        public void SetupAlienRacePopulations(EmpireStartList empireStarts, int aggressiveRacesRequired)
        {
            _WidespreadRaces = new RaceList();
            RaceList raceList = DetermineAggressiveRaces(Races, 115, 85);
            int num = 0;
            double num2 = (double)SectorSize * 2.0;
            bool flag = false;
            double val = Math.Sqrt(1400.0) / Math.Sqrt(StarCount);
            val = Math.Max(1.0, Math.Min(val, 3.0));
            double num3 = 0.85 * ((double)SizeX / Math.Sqrt(empireStarts.Count));
            double radiusFromCenterMaximum = 1.0;
            GalaxyLocation location = null;
            int totalColonyAmount = empireStarts.TotalColonyAmount;
            double num4 = (double)totalColonyAmount / (double)empireStarts.Count;
            double val2 = (double)SectorSize * (20.0 / (double)empireStarts.Count) * val;
            double num5 = (double)SectorSize * 1.0;
            val2 = Math.Min(val2, (double)SectorSize * 4.5);
            for (int i = 0; i < empireStarts.Count; i++)
            {
                Race resolvedRace = empireStarts[i].ResolvedRace;
                GalaxyLocation galaxyLocation = DetermineRaceRegion(resolvedRace);
                if (galaxyLocation != null)
                {
                    continue;
                }
                double d = (double)empireStarts.TotalColoniesForRace(resolvedRace) / num4;
                double num6 = (double)empireStarts[i].ProjectedColonyAmount / num4;
                double val3 = num5 + val2 * num6;
                val3 = Math.Min(val3, (double)SectorSize * 6.5);
                double num7 = num3 * Math.Sqrt(Math.Sqrt(d));
                ObtainRandomGalaxyCoordinates(0.0, radiusFromCenterMaximum, out var x, out var y);
                x -= num7 / 2.0;
                y -= num7 / 2.0;
                if (i > 0 && raceList.Count > 1 && empireStarts.Count > 4 && aggressiveRacesRequired > 0 && num < aggressiveRacesRequired && raceList.Contains(resolvedRace))
                {
                    double num8 = CheckDistanceFromLocation(location, x, y);
                    int num9 = 0;
                    flag = true;
                    while (num8 > num2 && num9 < 50)
                    {
                        ObtainRandomGalaxyCoordinates(0.0, radiusFromCenterMaximum, out x, out y);
                        x -= num7 / 2.0;
                        y -= num7 / 2.0;
                        num8 = CheckDistanceFromLocation(location, x, y);
                        num9++;
                    }
                    if (num9 >= 50)
                    {
                        flag = false;
                    }
                }
                GalaxyLocation galaxyLocation2 = new GalaxyLocation(resolvedRace.Name + " Region", GalaxyLocationType.RaceRegion, x, y, num7, num7, -1);
                int num10 = 0;
                int num11 = 0;
                while (CheckLocationOverlap(galaxyLocation2, GalaxyLocationType.RaceRegion) && num11 < 20)
                {
                    ObtainRandomGalaxyCoordinates(0.0, radiusFromCenterMaximum, out x, out y);
                    x -= num7 / 2.0;
                    y -= num7 / 2.0;
                    if (i > 0 && raceList.Count > 1 && empireStarts.Count > 4 && aggressiveRacesRequired > 0 && num < aggressiveRacesRequired && raceList.Contains(resolvedRace))
                    {
                        double num12 = CheckDistanceFromLocation(location, x, y);
                        int num13 = 0;
                        flag = true;
                        while (num12 > num2 && num13 < 50)
                        {
                            ObtainRandomGalaxyCoordinates(0.0, radiusFromCenterMaximum, out x, out y);
                            x -= num7 / 2.0;
                            y -= num7 / 2.0;
                            num12 = CheckDistanceFromLocation(location, x, y);
                            num13++;
                        }
                        if (num13 >= 50)
                        {
                            flag = false;
                        }
                    }
                    galaxyLocation2 = new GalaxyLocation(resolvedRace.Name + " Region", GalaxyLocationType.RaceRegion, x, y, num7, num7, -1);
                    num10++;
                    if (num10 > 50)
                    {
                        num11++;
                        num7 *= 0.9;
                        num7 = Math.Max(num7, 300000.0);
                        num10 = 0;
                    }
                }
                galaxyLocation2.ShowName = false;
                galaxyLocation2.RelatedRace = resolvedRace;
                _GalaxyLocations.Add(galaxyLocation2);
                AddGalaxyLocationIndex(galaxyLocation2);
                if (flag)
                {
                    num++;
                    flag = false;
                }
                if (i == 0)
                {
                    location = galaxyLocation2;
                }
            }
            _ContinentalRaces = new RaceList();
            _ContinentalRaces.Add(Races[0]);
            _ContinentalRaces.Add(Races[1]);
            _ContinentalRaces.Add(Races[3]);
            _ContinentalRaces.Add(Races[4]);
            _ContinentalRaces.Add(Races[6]);
            _ContinentalRaces.Add(Races[8]);
            _ContinentalRaces.Add(Races[9]);
            _ContinentalRaces.Add(Races[19]);
            _ContinentalRaces.Add(Races[10]);
            _ContinentalRaces.Add(Races[16]);
            _ContinentalRaces.Add(Races[17]);
            _MarshySwampRaces = new RaceList();
            _MarshySwampRaces.Add(Races[0]);
            _MarshySwampRaces.Add(Races[1]);
            _MarshySwampRaces.Add(Races[3]);
            _MarshySwampRaces.Add(Races[4]);
            _MarshySwampRaces.Add(Races[6]);
            _MarshySwampRaces.Add(Races[8]);
            _MarshySwampRaces.Add(Races[10]);
            _MarshySwampRaces.Add(Races[16]);
            _MarshySwampRaces.Add(Races[17]);
            _DesertRaces = new RaceList();
            _DesertRaces.Add(Races[2]);
            _DesertRaces.Add(Races[3]);
            _DesertRaces.Add(Races[4]);
            _DesertRaces.Add(Races[6]);
            _DesertRaces.Add(Races[11]);
            _DesertRaces.Add(Races[13]);
            _DesertRaces.Add(Races[18]);
            _OceanRaces = new RaceList();
            _OceanRaces.Add(Races[5]);
            _OceanRaces.Add(Races[7]);
            _OceanRaces.Add(Races[12]);
            _IceRaces = new RaceList();
            _IceRaces.Add(Races[15]);
            _IceRaces.Add(Races[9]);
            _VolcanicRaces = new RaceList();
            _VolcanicRaces.Add(Races[2]);
            _VolcanicRaces.Add(Races[13]);
            _VolcanicRaces.Add(Races[14]);
            _BarrenRockRaces = new RaceList();
        }

        public GalaxyLocation DetermineRaceRegion(Race race)
        {
            for (int i = 0; i < _GalaxyLocations.Count; i++)
            {
                if (_GalaxyLocations[i].Type == GalaxyLocationType.RaceRegion && _GalaxyLocations[i].RelatedRace == race)
                {
                    return _GalaxyLocations[i];
                }
            }
            return null;
        }

        public void DetermineRandomCoordinatesInLocation(GalaxyLocation location, out double x, out double y)
        {
            x = (double)location.Xpos + (double)location.Width * Rnd.NextDouble();
            y = (double)location.Ypos + (double)location.Height * Rnd.NextDouble();
        }

        public void SelectPopulation(Habitat habitat, Habitat sun)
        {
            if (habitat.Diameter < 75)
            {
                return;
            }
            if (_RaceUsed == null)
            {
                _RaceUsed = new bool[Races.Count];
            }
            Race race = null;
            GalaxyLocation galaxyLocation = DetermineNearestRaceRegion(habitat.Xpos, habitat.Ypos);
            if (galaxyLocation != null)
            {
                race = galaxyLocation.RelatedRace;
            }
            if (race != null && race.NativeHabitatType == habitat.Type && !CheckIndependentColonyLimitForRace(race))
            {
                if (habitat.Quality < 0.6f)
                {
                    habitat.BaseQuality = 0.5f + (float)(Rnd.NextDouble() * 0.4);
                }
                int num = 1;
                for (int i = 0; i < num; i++)
                {
                    IndependentCount++;
                    long amount = CalculatePopulationAmount(habitat, race);
                    Population population = new Population(race, amount);
                    population.GrowthRate = 1f + ((float)race.ReproductiveRate - 1f) / 3f;
                    _RaceIndependentColonyCount[race.PictureRef]++;
                    habitat.Population.Add(population);
                    RenameSystemIfHome(sun, race);
                }
                habitat.Population.RecalculateTotalAmount();
            }
        }

        public bool CheckNearIndependentColony(double x, double y, double range)
        {
            double num = range * range;
            for (int i = 0; i < IndependentColonies.Count; i++)
            {
                Habitat habitat = IndependentColonies[i];
                if (habitat != null)
                {
                    double num2 = CalculateDistanceSquared(x, y, habitat.Xpos, habitat.Ypos);
                    if (num2 < num)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckIndependentColonyLimitForRace(Race race)
        {
            double num = (double)_LifePrevalence / 1000.0;
            int num2 = (int)(Math.Sqrt(StarCount) / 3.5 * num);
            if (_RaceIndependentColonyCount == null || _RaceIndependentColonyCount.Count == 0)
            {
                _RaceIndependentColonyCount = new List<int>();
                for (int i = 0; i < Races.Count; i++)
                {
                    _RaceIndependentColonyCount.Add(0);
                }
            }
            int num3 = _RaceIndependentColonyCount[race.PictureRef];
            if (num3 >= num2)
            {
                return true;
            }
            return false;
        }

        private void FilterRacesByColonyLimits(ref RaceList filteredRaces)
        {
            double num = (double)_LifePrevalence / 1300.0;
            int num2 = (int)(Math.Sqrt(StarCount) / 4.0 * num);
            if (_RaceIndependentColonyCount == null || _RaceIndependentColonyCount.Count == 0)
            {
                _RaceIndependentColonyCount = new List<int>();
                for (int i = 0; i < Races.Count; i++)
                {
                    _RaceIndependentColonyCount.Add(0);
                }
            }
            RaceList raceList = new RaceList();
            foreach (Race filteredRace in filteredRaces)
            {
                int num3 = _RaceIndependentColonyCount[filteredRace.PictureRef];
                if (num3 >= num2)
                {
                    raceList.Add(filteredRace);
                }
            }
            foreach (Race item in raceList)
            {
                filteredRaces.Remove(item);
            }
        }

        private void RenameSystemIfHome(Habitat sun, Race race)
        {
            int pictureRef = race.PictureRef;
            if (!_RaceUsed[pictureRef])
            {
                sun.Name = race.HomeSystemName;
                _RaceUsed[pictureRef] = true;
            }
        }

        private long CalculatePopulationAmount(Habitat habitat, Race race)
        {
            double num = habitat.Quality * 1000f;
            if (habitat.Type == race.NativeHabitatType)
            {
                num *= 1.5;
            }
            long num2 = 0L;
            int num3 = Rnd.Next(0, 30);
            num2 = ((num3 < 0 || num3 > 6) ? (Rnd.Next(100000, 300000) * (long)num) : (Rnd.Next(300000, 600000) * (long)num));
            if (_Age > 0)
            {
                num2 = (long)((double)num2 * Math.Pow(1.2, _Age));
            }
            return num2;
        }

        private void SelectPopulationOLD(Habitat habitat)
        {
            double num = 0.0;
            if (habitat.Diameter < 75)
            {
                return;
            }
            num = habitat.Type switch
            {
                HabitatType.Continental => 33.0 / (double)Races.Count,
                HabitatType.Ice => 4.0 / (double)Races.Count,
                HabitatType.MarshySwamp => 28.0 / (double)Races.Count,
                HabitatType.Ocean => 10.0 / (double)Races.Count,
                HabitatType.Desert => 18.0 / (double)Races.Count,
                HabitatType.Volcanic => 5.0 / (double)Races.Count,
                HabitatType.BarrenRock => 0.2 / (double)Races.Count,
                _ => 0.0,
            };
            int num2 = -1;
            if (Rnd.Next(0, 1000) < (int)((double)_LifePrevalence * num))
            {
                switch (habitat.Type)
                {
                    case HabitatType.Continental:
                        while (num2 < 0)
                        {
                            num2 = habitat.Population.Add(SelectContinentalPopulation(habitat));
                        }
                        break;
                    case HabitatType.Ice:
                        while (num2 < 0)
                        {
                            num2 = habitat.Population.Add(SelectIcePopulation(habitat));
                        }
                        break;
                    case HabitatType.MarshySwamp:
                        while (num2 < 0)
                        {
                            num2 = habitat.Population.Add(SelectMarshySwampPopulation(habitat));
                        }
                        break;
                    case HabitatType.Ocean:
                        while (num2 < 0)
                        {
                            num2 = habitat.Population.Add(SelectOceanPopulation(habitat));
                        }
                        break;
                    case HabitatType.Desert:
                        while (num2 < 0)
                        {
                            num2 = habitat.Population.Add(SelectDesertPopulation(habitat));
                        }
                        break;
                    case HabitatType.Volcanic:
                        while (num2 < 0)
                        {
                            num2 = habitat.Population.Add(SelectVolcanicPopulation(habitat));
                        }
                        break;
                    case HabitatType.BarrenRock:
                        while (num2 < 0)
                        {
                            num2 = habitat.Population.Add(SelectBarrenRockPopulation(habitat));
                        }
                        break;
                }
            }
            habitat.Population.RecalculateTotalAmount();
        }

        private Population SelectBarrenRockPopulation(Habitat habitat)
        {
            int num = Rnd.Next(0, 15);
            Population result = null;
            long num2 = (long)Rnd.Next(5000, 30000) * 1000L;
            if (_Age > 0)
            {
                num2 = (long)((double)num2 * Math.Pow(1.7, _Age));
            }
            if (num >= 0 && num <= 15)
            {
                result = new Population(Races[18], num2);
            }
            return result;
        }

        private Population SelectIcePopulation(Habitat habitat)
        {
            int num = Rnd.Next(0, 18);
            Population result = null;
            long num2 = 0L;
            int num3 = Rnd.Next(0, 30);
            num2 = ((num3 >= 0 && num3 <= 1) ? ((long)Rnd.Next(1000000, 2500000) * 1000L) : ((num3 < 2 || num3 > 4) ? ((long)Rnd.Next(20000, 300000) * 1000L) : ((long)Rnd.Next(300000, 1000000) * 1000L)));
            if (_Age > 0)
            {
                num2 = (long)((double)num2 * Math.Pow(1.7, _Age));
            }
            if (num >= 0 && num <= 6)
            {
                result = new Population(Races[32], num2);
            }
            if (num >= 7 && num <= 14)
            {
                result = new Population(Races[15], num2);
            }
            if (num >= 15 && num <= 18)
            {
                result = new Population(Races[23], num2);
            }
            return result;
        }

        private Population SelectVolcanicPopulation(Habitat habitat)
        {
            int num = Rnd.Next(0, 22);
            Population result = null;
            long num2 = 0L;
            int num3 = Rnd.Next(0, 30);
            num2 = ((num3 >= 0 && num3 <= 1) ? ((long)Rnd.Next(1000000, 2500000) * 1000L) : ((num3 < 2 || num3 > 4) ? ((long)Rnd.Next(20000, 300000) * 1000L) : ((long)Rnd.Next(300000, 1000000) * 1000L)));
            if (_Age > 0)
            {
                num2 = (long)((double)num2 * Math.Pow(1.7, _Age));
            }
            if (num >= 0 && num <= 5)
            {
                result = new Population(Races[17], num2);
            }
            if (num >= 6 && num <= 8)
            {
                result = new Population(Races[3], num2);
            }
            if (num >= 9 && num <= 17)
            {
                result = new Population(Races[29], num2);
            }
            if (num >= 18 && num <= 22)
            {
                result = new Population(Races[30], num2);
            }
            return result;
        }

        private Population SelectOceanPopulation(Habitat habitat)
        {
            int num = Rnd.Next(0, 27);
            Population result = null;
            long num2 = 0L;
            int num3 = Rnd.Next(0, 30);
            num2 = ((num3 >= 0 && num3 <= 1) ? ((long)Rnd.Next(1000000, 2500000) * 1000L) : ((num3 < 2 || num3 > 4) ? ((long)Rnd.Next(20000, 300000) * 1000L) : ((long)Rnd.Next(300000, 1000000) * 1000L)));
            if (_Age > 0)
            {
                num2 = (long)((double)num2 * Math.Pow(1.7, _Age));
            }
            if (num >= 0 && num <= 7)
            {
                result = new Population(Races[8], num2);
            }
            if (num >= 8 && num <= 16)
            {
                result = new Population(Races[11], num2);
            }
            if (num >= 17 && num <= 21)
            {
                result = new Population(Races[26], num2);
            }
            if (num >= 22 && num <= 27)
            {
                result = new Population(Races[27], num2);
            }
            return result;
        }

        public Population SelectDesertPopulation(Habitat habitat)
        {
            int num = Rnd.Next(0, 66);
            Population result = null;
            long num2 = 0L;
            int num3 = Rnd.Next(0, 30);
            num2 = ((num3 >= 0 && num3 <= 1) ? ((long)Rnd.Next(1000000, 2500000) * 1000L) : ((num3 < 2 || num3 > 4) ? ((long)Rnd.Next(20000, 300000) * 1000L) : ((long)Rnd.Next(300000, 1000000) * 1000L)));
            if (_Age > 0)
            {
                num2 = (long)((double)num2 * Math.Pow(1.7, _Age));
            }
            if (num >= 0 && num <= 6)
            {
                result = new Population(Races[3], num2);
            }
            if (num >= 7 && num <= 10)
            {
                result = new Population(Races[4], num2);
            }
            if (num >= 11 && num <= 17)
            {
                result = new Population(Races[6], num2);
            }
            if (num >= 18 && num <= 20)
            {
                result = new Population(Races[7], num2);
            }
            if (num >= 21 && num <= 24)
            {
                result = new Population(Races[9], num2);
            }
            if (num >= 25 && num <= 31)
            {
                result = new Population(Races[10], num2);
            }
            if (num >= 32 && num <= 37)
            {
                result = new Population(Races[16], num2);
            }
            if (num >= 38 && num <= 43)
            {
                result = new Population(Races[24], num2);
            }
            if (num >= 44 && num <= 51)
            {
                result = new Population(Races[29], num2);
            }
            if (num >= 52 && num <= 55)
            {
                result = new Population(Races[33], num2);
            }
            if (num >= 56 && num <= 59)
            {
                result = new Population(Races[36], num2);
            }
            if (num >= 60 && num <= 66)
            {
                result = new Population(Races[37], num2);
            }
            return result;
        }

        public Population SelectMarshySwampPopulation(Habitat habitat)
        {
            int num = Rnd.Next(0, 114);
            Population result = null;
            long num2 = 0L;
            int num3 = Rnd.Next(0, 30);
            num2 = ((num3 >= 0 && num3 <= 1) ? ((long)Rnd.Next(1000000, 2500000) * 1000L) : ((num3 < 2 || num3 > 4) ? ((long)Rnd.Next(20000, 300000) * 1000L) : ((long)Rnd.Next(300000, 1000000) * 1000L)));
            if (_Age > 0)
            {
                num2 = (long)((double)num2 * Math.Pow(1.7, _Age));
            }
            if (num >= 0 && num <= 6)
            {
                result = new Population(Races[0], num2);
            }
            if (num >= 7 && num <= 12)
            {
                result = new Population(Races[1], num2);
            }
            if (num >= 13 && num <= 18)
            {
                result = new Population(Races[2], num2);
            }
            if (num >= 19 && num <= 21)
            {
                result = new Population(Races[4], num2);
            }
            if (num >= 22 && num <= 26)
            {
                result = new Population(Races[5], num2);
            }
            if (num >= 27 && num <= 31)
            {
                result = new Population(Races[6], num2);
            }
            if (num >= 32 && num <= 37)
            {
                result = new Population(Races[7], num2);
            }
            if (num >= 38 && num <= 44)
            {
                result = new Population(Races[10], num2);
            }
            if (num >= 45 && num <= 51)
            {
                result = new Population(Races[12], num2);
            }
            if (num >= 52 && num <= 55)
            {
                result = new Population(Races[13], num2);
            }
            if (num >= 56 && num <= 63)
            {
                result = new Population(Races[14], num2);
            }
            if (num >= 64 && num <= 68)
            {
                result = new Population(Races[19], num2);
            }
            if (num >= 69 && num <= 72)
            {
                result = new Population(Races[26], num2);
            }
            if (num >= 73 && num <= 77)
            {
                result = new Population(Races[31], num2);
            }
            if (num >= 78 && num <= 81)
            {
                result = new Population(Races[21], num2);
            }
            if (num >= 82 && num <= 86)
            {
                result = new Population(Races[22], num2);
            }
            if (num >= 87 && num <= 88)
            {
                result = new Population(Races[23], num2);
            }
            if (num >= 89 && num <= 95)
            {
                result = new Population(Races[25], num2);
            }
            if (num >= 96 && num <= 101)
            {
                result = new Population(Races[28], num2);
            }
            if (num >= 102 && num <= 109)
            {
                result = new Population(Races[34], num2);
            }
            if (num >= 110 && num <= 114)
            {
                result = new Population(Races[35], num2);
            }
            return result;
        }

        public Population SelectContinentalPopulation(Habitat habitat)
        {
            int num = Rnd.Next(0, 111);
            Population result = null;
            long num2 = 0L;
            int num3 = Rnd.Next(0, 30);
            num2 = ((num3 >= 0 && num3 <= 1) ? ((long)Rnd.Next(1000000, 2500000) * 1000L) : ((num3 < 2 || num3 > 4) ? ((long)Rnd.Next(20000, 300000) * 1000L) : ((long)Rnd.Next(300000, 1000000) * 1000L)));
            if (_Age > 0)
            {
                num2 = (long)((double)num2 * Math.Pow(1.7, _Age));
            }
            if (num >= 0 && num <= 5)
            {
                result = new Population(Races[0], num2);
            }
            if (num >= 6 && num <= 10)
            {
                result = new Population(Races[1], num2);
            }
            if (num >= 11 && num <= 15)
            {
                result = new Population(Races[2], num2);
            }
            if (num >= 16 && num <= 19)
            {
                result = new Population(Races[4], num2);
            }
            if (num >= 20 && num <= 24)
            {
                result = new Population(Races[5], num2);
            }
            if (num >= 25 && num <= 29)
            {
                result = new Population(Races[6], num2);
            }
            if (num >= 30 && num <= 37)
            {
                result = new Population(Races[7], num2);
            }
            if (num >= 38 && num <= 44)
            {
                result = new Population(Races[10], num2);
            }
            if (num >= 45 && num <= 50)
            {
                result = new Population(Races[12], num2);
            }
            if (num >= 51 && num <= 53)
            {
                result = new Population(Races[13], num2);
            }
            if (num >= 54 && num <= 60)
            {
                result = new Population(Races[14], num2);
            }
            if (num >= 61 && num <= 67)
            {
                result = new Population(Races[15], num2);
            }
            if (num >= 68 && num <= 73)
            {
                result = new Population(Races[38], num2);
            }
            if (num >= 74 && num <= 77)
            {
                result = new Population(Races[20], num2);
            }
            if (num >= 78 && num <= 81)
            {
                result = new Population(Races[21], num2);
            }
            if (num >= 82 && num <= 85)
            {
                result = new Population(Races[22], num2);
            }
            if (num >= 86 && num <= 87)
            {
                result = new Population(Races[23], num2);
            }
            if (num >= 88 && num <= 94)
            {
                result = new Population(Races[25], num2);
            }
            if (num >= 95 && num <= 100)
            {
                result = new Population(Races[28], num2);
            }
            if (num >= 101 && num <= 107)
            {
                result = new Population(Races[34], num2);
            }
            if (num >= 108 && num <= 111)
            {
                result = new Population(Races[35], num2);
            }
            return result;
        }

        private double CalculateLifePrevalenceMultiplierForMoonType(HabitatType type, Habitat star)
        {
            double result = 1.0;
            List<double> prevalenceThresholds = new List<double>();
            CalculateMoonTypePrevalenceByPlanetType(star, out prevalenceThresholds);
            switch (type)
            {
                case HabitatType.Continental:
                    result = 1.0 / prevalenceThresholds[1];
                    break;
                case HabitatType.MarshySwamp:
                    result = 1.0 / prevalenceThresholds[2];
                    break;
                case HabitatType.Ocean:
                    result = 1.0 / prevalenceThresholds[3];
                    break;
                case HabitatType.Desert:
                    result = 1.0 / prevalenceThresholds[4];
                    break;
                case HabitatType.Ice:
                    result = 1.0 / prevalenceThresholds[0];
                    break;
                case HabitatType.Volcanic:
                    result = 1.0 / prevalenceThresholds[5];
                    break;
                case HabitatType.BarrenRock:
                    result = 0.0;
                    break;
            }
            return result;
        }

        private void CalculateMoonTypePrevalenceByPlanetType(Habitat planet, out List<double> prevalenceThresholds)
        {
            prevalenceThresholds = new List<double>();
            if (planet.Diameter >= 430)
            {
                if (planet.Type == HabitatType.FrozenGasGiant)
                {
                    prevalenceThresholds.Add(0.15);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.85);
                }
                else
                {
                    prevalenceThresholds.Add(0.08);
                    prevalenceThresholds.Add(0.024);
                    prevalenceThresholds.Add(0.024);
                    prevalenceThresholds.Add(0.062);
                    prevalenceThresholds.Add(0.062);
                    prevalenceThresholds.Add(0.086);
                    prevalenceThresholds.Add(0.662);
                }
            }
            else if (planet.Diameter >= 340)
            {
                if (planet.Type == HabitatType.FrozenGasGiant)
                {
                    prevalenceThresholds.Add(0.15);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.85);
                }
                else
                {
                    prevalenceThresholds.Add(0.093);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.093);
                    prevalenceThresholds.Add(0.093);
                    prevalenceThresholds.Add(0.093);
                    prevalenceThresholds.Add(0.628);
                }
            }
            else
            {
                prevalenceThresholds.Add(0.0);
                prevalenceThresholds.Add(0.0);
                prevalenceThresholds.Add(0.0);
                prevalenceThresholds.Add(0.0);
                prevalenceThresholds.Add(0.0);
                prevalenceThresholds.Add(0.0);
                prevalenceThresholds.Add(1.0);
            }
            _ = (1.0 - _ColonyPrevalence) / 2.0;
            prevalenceThresholds[1] *= _ColonyPrevalence;
            prevalenceThresholds[2] *= _ColonyPrevalence;
        }

        private void SelectMoonType(Habitat parentHabitat, out int diameter, out HabitatType type, out HabitatAtmosphereType atmosphereType, out int atmosphereDensity, out int pictureRef, out int landscapePictureRef)
        {
            type = HabitatType.BarrenRock;
            atmosphereType = HabitatAtmosphereType.None;
            atmosphereDensity = 0;
            pictureRef = 0;
            diameter = 0;
            landscapePictureRef = 0;
            List<double> prevalenceThresholds = new List<double>();
            CalculateMoonTypePrevalenceByPlanetType(parentHabitat, out prevalenceThresholds);
            double num = Rnd.NextDouble();
            double num2 = 0.0;
            double num3 = 0.0;
            int minOrbitDistance;
            int maxOrbitDistance;
            for (int i = 0; i < prevalenceThresholds.Count; i++)
            {
                num2 = num3;
                num3 += prevalenceThresholds[i];
                if (num >= num2 && num < num3)
                {
                    switch (i)
                    {
                        case 0:
                            SelectIcePlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        case 1:
                            SelectContinentalPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        case 2:
                            SelectMarshySwampPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        case 3:
                            SelectOceanPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        case 4:
                            SelectDesertPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        case 5:
                            SelectVolcanicPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        case 6:
                            SelectBarrenRockPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        default:
                            SelectBarrenRockPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                    }
                    break;
                }
            }
            if (diameter <= 0)
            {
                SelectBarrenRockPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
            }
            if ((double)diameter > (double)parentHabitat.Diameter * 0.33)
            {
                diameter = (int)((double)parentHabitat.Diameter * ((double)Rnd.Next(28, 33) * 0.01));
            }
            switch (type)
            {
                case HabitatType.Continental:
                    _MoonCountContinental++;
                    break;
                case HabitatType.MarshySwamp:
                    _MoonCountMarshySwamp++;
                    break;
                case HabitatType.Desert:
                    _MoonCountDesert++;
                    break;
                case HabitatType.Ocean:
                    _MoonCountOcean++;
                    break;
                case HabitatType.Ice:
                    _MoonCountIce++;
                    break;
                case HabitatType.Volcanic:
                    _MoonCountVolcanic++;
                    break;
                case HabitatType.BarrenRock:
                    break;
            }
        }

        public void SelectBarrenRockPlanet(out HabitatType type, out int pictureRef, out int diameter, out int minOrbitDistance, out int maxOrbitDistance, out int landscapePictureRef)
        {
            type = HabitatType.BarrenRock;
            diameter = Rnd.Next(80, 340);
            minOrbitDistance = 2500;
            maxOrbitDistance = 11500;
            SelectBarrenRockPlanet(diameter, out pictureRef, out landscapePictureRef);
        }

        public void SelectBarrenRockPlanet(int diameter, out int pictureRef, out int landscapePictureRef)
        {
            pictureRef = GalaxyImages.HabitatImageOffsetBarrenRock + Rnd.Next(0, GalaxyImages.HabitatImageCountBarrenRock);
            landscapePictureRef = GalaxyImages.LandscapeImageOffsetBarrenRock + Rnd.Next(0, GalaxyImages.LandscapeImageCountBarrenRock);
        }

        public void SelectContinentalPlanet(out HabitatType type, out int pictureRef, out int diameter, out int minOrbitDistance, out int maxOrbitDistance, out int landscapePictureRef)
        {
            type = HabitatType.Continental;
            diameter = Rnd.Next(200, 320);
            minOrbitDistance = 5000;
            maxOrbitDistance = 10000;
            SelectContinentalPlanet(diameter, out pictureRef, out landscapePictureRef);
        }

        public void SelectContinentalPlanet(int diameter, out int pictureRef, out int landscapePictureRef)
        {
            pictureRef = GalaxyImages.HabitatImageOffsetContinental + Rnd.Next(0, GalaxyImages.HabitatImageCountContinental);
            landscapePictureRef = GalaxyImages.LandscapeImageOffsetContinental + Rnd.Next(0, GalaxyImages.LandscapeImageCountContinental);
        }

        public void SelectIcePlanet(out HabitatType type, out int pictureRef, out int diameter, out int minOrbitDistance, out int maxOrbitDistance, out int landscapePictureRef)
        {
            type = HabitatType.Ice;
            diameter = Rnd.Next(180, 320);
            minOrbitDistance = 18000;
            maxOrbitDistance = 23000;
            SelectIcePlanet(diameter, out pictureRef, out landscapePictureRef);
        }

        public void SelectIcePlanet(int diameter, out int pictureRef, out int landscapePictureRef)
        {
            pictureRef = GalaxyImages.HabitatImageOffsetIce + Rnd.Next(0, GalaxyImages.HabitatImageCountIce);
            landscapePictureRef = GalaxyImages.LandscapeImageOffsetIce + Rnd.Next(0, GalaxyImages.LandscapeImageCountIce);
        }

        public void SelectMarshySwampPlanet(out HabitatType type, out int pictureRef, out int diameter, out int minOrbitDistance, out int maxOrbitDistance, out int landscapePictureRef)
        {
            type = HabitatType.MarshySwamp;
            diameter = Rnd.Next(200, 320);
            minOrbitDistance = 5000;
            maxOrbitDistance = 9500;
            SelectMarshySwampPlanet(diameter, out pictureRef, out landscapePictureRef);
        }

        public void SelectMarshySwampPlanet(int diameter, out int pictureRef, out int landscapePictureRef)
        {
            pictureRef = GalaxyImages.HabitatImageOffsetMarshySwamp + Rnd.Next(0, GalaxyImages.HabitatImageCountMarshySwamp);
            landscapePictureRef = GalaxyImages.LandscapeImageOffsetMarshySwamp + Rnd.Next(0, GalaxyImages.LandscapeImageCountMarshySwamp);
        }

        public void SelectOceanPlanet(out HabitatType type, out int pictureRef, out int diameter, out int minOrbitDistance, out int maxOrbitDistance, out int landscapePictureRef)
        {
            type = HabitatType.Ocean;
            diameter = Rnd.Next(200, 320);
            minOrbitDistance = 5000;
            maxOrbitDistance = 10000;
            SelectOceanPlanet(diameter, out pictureRef, out landscapePictureRef);
        }

        public void SelectOceanPlanet(int diameter, out int pictureRef, out int landscapePictureRef)
        {
            pictureRef = GalaxyImages.HabitatImageOffsetOcean + Rnd.Next(0, GalaxyImages.HabitatImageCountOcean);
            landscapePictureRef = GalaxyImages.LandscapeImageOffsetOcean + Rnd.Next(0, GalaxyImages.LandscapeImageCountOcean);
        }

        public void SelectDesertPlanet(out HabitatType type, out int pictureRef, out int diameter, out int minOrbitDistance, out int maxOrbitDistance, out int landscapePictureRef)
        {
            type = HabitatType.Desert;
            diameter = Rnd.Next(180, 330);
            minOrbitDistance = 3000;
            maxOrbitDistance = 10300;
            SelectDesertPlanet(diameter, out pictureRef, out landscapePictureRef);
        }

        public void SelectDesertPlanet(int diameter, out int pictureRef, out int landscapePictureRef)
        {
            pictureRef = GalaxyImages.HabitatImageOffsetDesert + Rnd.Next(0, GalaxyImages.HabitatImageCountDesert);
            landscapePictureRef = GalaxyImages.LandscapeImageOffsetDesert + Rnd.Next(0, GalaxyImages.LandscapeImageCountDesert);
        }

        public void SelectVolcanicPlanet(out HabitatType type, out int pictureRef, out int diameter, out int minOrbitDistance, out int maxOrbitDistance, out int landscapePictureRef)
        {
            type = HabitatType.Volcanic;
            diameter = Rnd.Next(160, 330);
            minOrbitDistance = 1250;
            maxOrbitDistance = 3500;
            SelectVolcanicPlanet(diameter, out pictureRef, out landscapePictureRef);
        }

        public void SelectVolcanicPlanet(int diameter, out int pictureRef, out int landscapePictureRef)
        {
            pictureRef = GalaxyImages.HabitatImageOffsetVolcanic + Rnd.Next(0, GalaxyImages.HabitatImageCountVolcanic);
            landscapePictureRef = GalaxyImages.LandscapeImageOffsetVolcanic + Rnd.Next(0, GalaxyImages.LandscapeImageCountVolcanic);
        }

        public void SelectHabitatPictures(Habitat habitat)
        {
            if (habitat.Category == HabitatCategoryType.Asteroid)
            {
                switch (habitat.Type)
                {
                    case HabitatType.BarrenRock:
                        habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetAsteroidsNormal + Rnd.Next(0, GalaxyImages.HabitatImageCountAsteroidsNormal));
                        habitat.LandscapePictureRef = (short)(GalaxyImages.LandscapeImageOffsetBarrenRock + Rnd.Next(0, GalaxyImages.LandscapeImageCountBarrenRock));
                        break;
                    case HabitatType.Ice:
                        habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetAsteroidsIce + Rnd.Next(0, GalaxyImages.HabitatImageCountAsteroidsIce));
                        habitat.LandscapePictureRef = (short)(GalaxyImages.LandscapeImageOffsetBarrenRock + Rnd.Next(0, GalaxyImages.LandscapeImageCountBarrenRock));
                        break;
                    case HabitatType.Metal:
                        habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetAsteroidsMetal + Rnd.Next(0, GalaxyImages.HabitatImageCountAsteroidsMetal));
                        habitat.LandscapePictureRef = (short)(GalaxyImages.LandscapeImageOffsetBarrenRock + Rnd.Next(0, GalaxyImages.LandscapeImageCountBarrenRock));
                        break;
                    default:
                        habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetAsteroidsNormal + Rnd.Next(0, GalaxyImages.HabitatImageCountAsteroidsNormal));
                        habitat.LandscapePictureRef = (short)(GalaxyImages.LandscapeImageOffsetBarrenRock + Rnd.Next(0, GalaxyImages.LandscapeImageCountBarrenRock));
                        break;
                }
                return;
            }
            switch (habitat.Type)
            {
                case HabitatType.BarrenRock:
                    habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetBarrenRock + Rnd.Next(0, GalaxyImages.HabitatImageCountBarrenRock));
                    habitat.LandscapePictureRef = (short)(GalaxyImages.LandscapeImageOffsetBarrenRock + Rnd.Next(0, GalaxyImages.LandscapeImageCountBarrenRock));
                    break;
                case HabitatType.Continental:
                    habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetContinental + Rnd.Next(0, GalaxyImages.HabitatImageCountContinental + GalaxyImages.HabitatImageCountForest));
                    habitat.LandscapePictureRef = (short)(GalaxyImages.LandscapeImageOffsetContinental + Rnd.Next(0, GalaxyImages.LandscapeImageCountContinental + GalaxyImages.LandscapeImageCountForest));
                    break;
                case HabitatType.FrozenGasGiant:
                    habitat.LandscapePictureRef = (short)(GalaxyImages.LandscapeImageOffsetFrozenGasGiant + Rnd.Next(0, GalaxyImages.LandscapeImageCountFrozenGasGiant));
                    if (habitat.Resources != null && habitat.Resources.Count > 0)
                    {
                        if (Rnd.Next(0, 5) == 1)
                        {
                            habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetFrozenGasGiantAny + Rnd.Next(0, GalaxyImages.HabitatImageCountFrozenGasGiantAny));
                            break;
                        }
                        string text2 = "Tyderios";
                        int num2 = 0;
                        foreach (HabitatResource resource in habitat.Resources)
                        {
                            if (resource.Abundance > num2)
                            {
                                text2 = resource.Name;
                                num2 = resource.Abundance;
                            }
                        }
                        switch (text2.ToLower(CultureInfo.InvariantCulture))
                        {
                            case "argon":
                                habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetFrozenGasGiantArgon + Rnd.Next(0, GalaxyImages.HabitatImageCountFrozenGasGiantArgon));
                                break;
                            case "helium":
                                habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetFrozenGasGiantHelium + Rnd.Next(0, GalaxyImages.HabitatImageCountFrozenGasGiantHelium));
                                break;
                            case "krypton":
                                habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetFrozenGasGiantKrypton + Rnd.Next(0, GalaxyImages.HabitatImageCountFrozenGasGiantKrypton));
                                break;
                            case "tyderios":
                                habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetFrozenGasGiantTyderios + Rnd.Next(0, GalaxyImages.HabitatImageCountFrozenGasGiantTyderios));
                                break;
                            default:
                                {
                                    int maxValue3 = GalaxyImages.HabitatImageCountFrozenGasGiantArgon + GalaxyImages.HabitatImageCountFrozenGasGiantHelium + GalaxyImages.HabitatImageCountFrozenGasGiantKrypton + GalaxyImages.HabitatImageCountFrozenGasGiantTyderios + GalaxyImages.HabitatImageCountFrozenGasGiantAny;
                                    habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetFrozenGasGiantArgon + Rnd.Next(0, maxValue3));
                                    break;
                                }
                        }
                    }
                    else
                    {
                        int maxValue4 = GalaxyImages.HabitatImageCountFrozenGasGiantAny + GalaxyImages.HabitatImageCountFrozenGasGiantArgon + GalaxyImages.HabitatImageCountFrozenGasGiantHelium + GalaxyImages.HabitatImageCountFrozenGasGiantKrypton + GalaxyImages.HabitatImageCountFrozenGasGiantTyderios;
                        habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetFrozenGasGiantArgon + Rnd.Next(0, maxValue4));
                    }
                    break;
                case HabitatType.GasGiant:
                    habitat.LandscapePictureRef = (short)(GalaxyImages.LandscapeImageOffsetGasGiant + Rnd.Next(0, GalaxyImages.LandscapeImageCountGasGiant));
                    if (habitat.Resources != null && habitat.Resources.Count > 0)
                    {
                        if (Rnd.Next(0, 5) == 1)
                        {
                            habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetGasGiantAny + Rnd.Next(0, GalaxyImages.HabitatImageCountGasGiantAny));
                            break;
                        }
                        string text = "Hydrogen";
                        int num = 0;
                        foreach (HabitatResource resource2 in habitat.Resources)
                        {
                            if (resource2.Abundance > num)
                            {
                                text = resource2.Name;
                                num = resource2.Abundance;
                            }
                        }
                        switch (text.ToLower(CultureInfo.InvariantCulture))
                        {
                            case "argon":
                                habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetGasGiantArgon + Rnd.Next(0, GalaxyImages.HabitatImageCountGasGiantArgon));
                                break;
                            case "helium":
                                habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetGasGiantHelium + Rnd.Next(0, GalaxyImages.HabitatImageCountGasGiantHelium));
                                break;
                            case "krypton":
                                habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetGasGiantKrypton + Rnd.Next(0, GalaxyImages.HabitatImageCountGasGiantKrypton));
                                break;
                            case "caslon":
                                habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetGasGiantCaslon + Rnd.Next(0, GalaxyImages.HabitatImageCountGasGiantCaslon));
                                break;
                            case "hydrogen":
                                habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetGasGiantHydrogen + Rnd.Next(0, GalaxyImages.HabitatImageCountGasGiantHydrogen));
                                break;
                            default:
                                {
                                    int maxValue = GalaxyImages.HabitatImageCountGasGiantAny + GalaxyImages.HabitatImageCountGasGiantArgon + GalaxyImages.HabitatImageCountGasGiantCaslon + GalaxyImages.HabitatImageCountGasGiantHelium + GalaxyImages.HabitatImageCountGasGiantHydrogen + GalaxyImages.HabitatImageCountGasGiantKrypton;
                                    habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetGasGiantArgon + Rnd.Next(0, maxValue));
                                    break;
                                }
                        }
                    }
                    else
                    {
                        int maxValue2 = GalaxyImages.HabitatImageCountGasGiantAny + GalaxyImages.HabitatImageCountGasGiantArgon + GalaxyImages.HabitatImageCountGasGiantCaslon + GalaxyImages.HabitatImageCountGasGiantHelium + GalaxyImages.HabitatImageCountGasGiantHydrogen + GalaxyImages.HabitatImageCountGasGiantKrypton;
                        habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetGasGiantArgon + Rnd.Next(0, maxValue2));
                    }
                    break;
                case HabitatType.Ice:
                    habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetIce + Rnd.Next(0, GalaxyImages.HabitatImageCountIce));
                    habitat.LandscapePictureRef = (short)(GalaxyImages.LandscapeImageOffsetIce + Rnd.Next(0, GalaxyImages.LandscapeImageCountIce));
                    break;
                case HabitatType.MarshySwamp:
                    habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetMarshySwamp + Rnd.Next(0, GalaxyImages.HabitatImageCountMarshySwamp));
                    habitat.LandscapePictureRef = (short)(GalaxyImages.LandscapeImageOffsetMarshySwamp + Rnd.Next(0, GalaxyImages.LandscapeImageCountMarshySwamp));
                    break;
                case HabitatType.Ocean:
                    habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetOcean + Rnd.Next(0, GalaxyImages.HabitatImageCountOcean));
                    habitat.LandscapePictureRef = (short)(GalaxyImages.LandscapeImageOffsetOcean + Rnd.Next(0, GalaxyImages.LandscapeImageCountOcean));
                    break;
                case HabitatType.Desert:
                    habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetDesert + Rnd.Next(0, GalaxyImages.HabitatImageCountDesert));
                    habitat.LandscapePictureRef = (short)(GalaxyImages.LandscapeImageOffsetDesert + Rnd.Next(0, GalaxyImages.LandscapeImageCountDesert));
                    break;
                case HabitatType.Volcanic:
                    habitat.PictureRef = (short)(GalaxyImages.HabitatImageOffsetVolcanic + Rnd.Next(0, GalaxyImages.HabitatImageCountVolcanic));
                    habitat.LandscapePictureRef = (short)(GalaxyImages.LandscapeImageOffsetVolcanic + Rnd.Next(0, GalaxyImages.LandscapeImageCountVolcanic));
                    break;
                case HabitatType.MainSequence:
                    habitat.PictureRef = (short)(GalaxyImages.MapStarImageOffsetMainSequence + Rnd.Next(0, GalaxyImages.MapStarImageCountMainSequence));
                    habitat.MapPictureRef = (byte)(GalaxyImages.MapStarImageOffsetMainSequence + Rnd.Next(0, GalaxyImages.MapStarImageCountMainSequence));
                    break;
                case HabitatType.RedGiant:
                    habitat.PictureRef = (short)(GalaxyImages.MapStarImageOffsetRedGiant + Rnd.Next(0, GalaxyImages.MapStarImageCountRedGiant));
                    habitat.MapPictureRef = (byte)(GalaxyImages.MapStarImageOffsetRedGiant + Rnd.Next(0, GalaxyImages.MapStarImageCountRedGiant));
                    break;
                case HabitatType.SuperGiant:
                    habitat.PictureRef = (short)(GalaxyImages.MapStarImageOffsetSuperGiant + Rnd.Next(0, GalaxyImages.MapStarImageCountSuperGiant));
                    habitat.MapPictureRef = (byte)(GalaxyImages.MapStarImageOffsetSuperGiant + Rnd.Next(0, GalaxyImages.MapStarImageCountSuperGiant));
                    break;
                case HabitatType.WhiteDwarf:
                    habitat.PictureRef = (short)(GalaxyImages.MapStarImageOffsetWhiteDwarf + Rnd.Next(0, GalaxyImages.MapStarImageCountWhiteDwarf));
                    habitat.MapPictureRef = (byte)(GalaxyImages.MapStarImageOffsetWhiteDwarf + Rnd.Next(0, GalaxyImages.MapStarImageCountWhiteDwarf));
                    break;
                case HabitatType.Neutron:
                    habitat.PictureRef = (short)(GalaxyImages.MapStarImageOffsetNeutron + Rnd.Next(0, GalaxyImages.MapStarImageCountNeutron));
                    habitat.MapPictureRef = (byte)(GalaxyImages.MapStarImageOffsetNeutron + Rnd.Next(0, GalaxyImages.MapStarImageCountNeutron));
                    break;
                case HabitatType.BlackHole:
                    habitat.PictureRef = (short)(GalaxyImages.MapStarImageOffsetBlackHole + Rnd.Next(0, GalaxyImages.MapStarImageCountBlackHole));
                    habitat.MapPictureRef = (byte)(GalaxyImages.MapStarImageOffsetBlackHole + Rnd.Next(0, GalaxyImages.MapStarImageCountBlackHole));
                    break;
                case HabitatType.SuperNova:
                    break;
            }
        }

        public void SelectGasGiantPlanet(out HabitatType type, out int pictureRef, out int diameter, out int minOrbitDistance, out int maxOrbitDistance, out int landscapePictureRef)
        {
            type = HabitatType.GasGiant;
            diameter = Rnd.Next(550, 970);
            minOrbitDistance = 12000;
            maxOrbitDistance = 17000;
            SelectGasGiantPlanet(diameter, out pictureRef, out landscapePictureRef);
        }

        public void SelectGasGiantPlanet(int diameter, out int pictureRef, out int landscapePictureRef)
        {
            int maxValue = GalaxyImages.HabitatImageCountGasGiantAny + GalaxyImages.HabitatImageCountGasGiantArgon + GalaxyImages.HabitatImageCountGasGiantCaslon + GalaxyImages.HabitatImageCountGasGiantHelium + GalaxyImages.HabitatImageCountGasGiantHydrogen + GalaxyImages.HabitatImageCountGasGiantKrypton;
            pictureRef = GalaxyImages.HabitatImageOffsetGasGiantArgon + Rnd.Next(0, maxValue);
            landscapePictureRef = GalaxyImages.LandscapeImageOffsetGasGiant + Rnd.Next(0, GalaxyImages.LandscapeImageCountGasGiant);
        }

        public void SelectFrozenGasGiantPlanet(out HabitatType type, out int pictureRef, out int diameter, out int minOrbitDistance, out int maxOrbitDistance, out int landscapePictureRef)
        {
            type = HabitatType.FrozenGasGiant;
            diameter = Rnd.Next(480, 680);
            minOrbitDistance = 17500;
            maxOrbitDistance = 22000;
            SelectFrozenGasGiantPlanet(diameter, out pictureRef, out landscapePictureRef);
        }

        public void SelectFrozenGasGiantPlanet(int diameter, out int pictureRef, out int landscapePictureRef)
        {
            int maxValue = GalaxyImages.HabitatImageCountFrozenGasGiantAny + GalaxyImages.HabitatImageCountFrozenGasGiantArgon + GalaxyImages.HabitatImageCountFrozenGasGiantHelium + GalaxyImages.HabitatImageCountFrozenGasGiantKrypton + GalaxyImages.HabitatImageCountFrozenGasGiantTyderios;
            pictureRef = GalaxyImages.HabitatImageOffsetFrozenGasGiantArgon + Rnd.Next(0, maxValue);
            landscapePictureRef = GalaxyImages.LandscapeImageOffsetFrozenGasGiant + Rnd.Next(0, GalaxyImages.LandscapeImageCountFrozenGasGiant);
        }

        private double CalculateLifePrevalenceMultiplierForPlanetType(HabitatType type, Habitat star)
        {
            double result = 1.0;
            List<double> prevalenceThresholds = new List<double>();
            CalculatePlanetTypePrevalenceByStarType(star, out prevalenceThresholds);
            switch (type)
            {
                case HabitatType.Continental:
                    result = 1.0 / prevalenceThresholds[1];
                    break;
                case HabitatType.MarshySwamp:
                    result = 1.0 / prevalenceThresholds[2];
                    break;
                case HabitatType.Ocean:
                    result = 1.0 / prevalenceThresholds[3];
                    break;
                case HabitatType.Desert:
                    result = 1.0 / prevalenceThresholds[4];
                    break;
                case HabitatType.Ice:
                    result = 1.0 / prevalenceThresholds[5];
                    break;
                case HabitatType.Volcanic:
                    result = 1.0 / prevalenceThresholds[6];
                    break;
                case HabitatType.BarrenRock:
                    result = 0.0;
                    break;
            }
            return result;
        }

        private void CalculatePlanetTypePrevalenceByStarType(Habitat star, out List<double> prevalenceThresholds)
        {
            prevalenceThresholds = new List<double>();
            switch (star.Type)
            {
                case HabitatType.MainSequence:
                case HabitatType.RedGiant:
                case HabitatType.SuperGiant:
                    prevalenceThresholds.Add(0.213);
                    prevalenceThresholds.Add(0.015);
                    prevalenceThresholds.Add(0.019);
                    prevalenceThresholds.Add(0.029);
                    prevalenceThresholds.Add(0.036);
                    prevalenceThresholds.Add(0.058);
                    prevalenceThresholds.Add(0.062);
                    prevalenceThresholds.Add(0.281);
                    prevalenceThresholds.Add(0.287);
                    break;
                case HabitatType.WhiteDwarf:
                    prevalenceThresholds.Add(0.216);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.176);
                    prevalenceThresholds.Add(0.098);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.51);
                    break;
                case HabitatType.Neutron:
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.333);
                    prevalenceThresholds.Add(0.118);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.549);
                    break;
                default:
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    prevalenceThresholds.Add(0.0);
                    break;
            }
            double num = 1.0 - (1.0 - _ColonyPrevalence) / 2.0;
            prevalenceThresholds[1] *= _ColonyPrevalence;
            prevalenceThresholds[2] *= _ColonyPrevalence;
            prevalenceThresholds[7] *= num;
        }

        private void SelectPlanetType(Habitat parentHabitat, out HabitatType type, out int pictureRef, out int diameter, out int minOrbitDistance, out int maxOrbitDistance, out int landscapePictureRef)
        {
            type = HabitatType.BarrenRock;
            pictureRef = 0;
            landscapePictureRef = 0;
            diameter = 0;
            minOrbitDistance = 0;
            maxOrbitDistance = 0;
            List<double> prevalenceThresholds = new List<double>();
            CalculatePlanetTypePrevalenceByStarType(parentHabitat, out prevalenceThresholds);
            double num = Rnd.NextDouble();
            double num2 = 0.0;
            double num3 = 0.0;
            for (int i = 0; i < prevalenceThresholds.Count; i++)
            {
                num2 = num3;
                num3 += prevalenceThresholds[i];
                if (num >= num2 && num < num3)
                {
                    switch (i)
                    {
                        case 0:
                            SelectFrozenGasGiantPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        case 1:
                            SelectContinentalPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        case 2:
                            SelectMarshySwampPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        case 3:
                            SelectOceanPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        case 4:
                            SelectDesertPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        case 5:
                            SelectIcePlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        case 6:
                            SelectVolcanicPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        case 7:
                            SelectGasGiantPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        case 8:
                            SelectBarrenRockPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                        default:
                            SelectBarrenRockPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            break;
                    }
                    break;
                }
            }
            if (diameter <= 0)
            {
                SelectBarrenRockPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
            }
            switch (type)
            {
                case HabitatType.Continental:
                    _PlanetCountContinental++;
                    break;
                case HabitatType.MarshySwamp:
                    _PlanetCountMarshySwamp++;
                    break;
                case HabitatType.Desert:
                    _PlanetCountDesert++;
                    break;
                case HabitatType.Ocean:
                    _PlanetCountOcean++;
                    break;
                case HabitatType.Ice:
                    _PlanetCountIce++;
                    break;
                case HabitatType.Volcanic:
                    _PlanetCountVolcanic++;
                    break;
                case HabitatType.BarrenRock:
                    break;
            }
        }

        public Habitat SelectStar(HabitatType type, double x, double y)
        {
            Habitat habitat = null;
            int num = 83;
            int num2 = 800;
            short num3 = 0;
            short num4 = 0;
            short num5 = 0;
            int num6 = 0;
            switch (type)
            {
                case HabitatType.MainSequence:
                    num2 = Rnd.Next(950, 1400);
                    num = ((num2 > 1200) ? 84 : 83);
                    num6 = Rnd.Next(0, 4);
                    num3 = (short)Rnd.Next(40, 60);
                    num4 = (short)Rnd.Next(5, 20);
                    num5 = (short)Rnd.Next(5, 12);
                    break;
                case HabitatType.RedGiant:
                    num2 = Rnd.Next(1520, 1720);
                    num = 85;
                    num6 = Rnd.Next(0, 3);
                    num3 = (short)Rnd.Next(70, 95);
                    num4 = (short)Rnd.Next(5, 20);
                    num5 = (short)Rnd.Next(5, 12);
                    break;
                case HabitatType.SuperGiant:
                    num2 = Rnd.Next(1720, 2150);
                    num = 86;
                    num6 = Rnd.Next(0, 3);
                    num3 = (short)Rnd.Next(80, 100);
                    num4 = (short)Rnd.Next(5, 20);
                    num5 = (short)Rnd.Next(5, 12);
                    break;
                case HabitatType.WhiteDwarf:
                    num2 = Rnd.Next(260, 350);
                    num = 87;
                    num6 = Rnd.Next(0, 3);
                    num3 = (short)Rnd.Next(10, 30);
                    num4 = (short)Rnd.Next(20, 40);
                    num5 = (short)Rnd.Next(40, 60);
                    break;
                case HabitatType.Neutron:
                    num2 = Rnd.Next(180, 230);
                    num = 88;
                    num3 = (short)Rnd.Next(1, 5);
                    num4 = (short)Rnd.Next(60, 90);
                    num5 = (short)Rnd.Next(120, 200);
                    break;
                case HabitatType.BlackHole:
                    num2 = Rnd.Next(4500, 6500);
                    num = 95;
                    num3 = (short)Rnd.Next(10, 15);
                    num4 = (short)Rnd.Next(60, 80);
                    num5 = (short)Rnd.Next(90, 130);
                    break;
            }
            double num7 = (double)MaxSolarSystemSize + 500.0;
            x = Math.Max(num7, Math.Min(x, (double)SizeX - num7));
            y = Math.Max(num7, Math.Min(y, (double)SizeY - num7));
            habitat = new Habitat(this, HabitatCategoryType.Star, type, GenerateCodeName(), x, y);
            if (habitat.Type == HabitatType.BlackHole)
            {
                habitat.Name = GenerateBlackHoleName();
            }
            habitat.Diameter = (short)num2;
            habitat.PictureRef = (short)num;
            habitat.LandscapePictureRef = -1;
            habitat.SolarRadiation = (byte)num3;
            habitat.MicrowaveRadiation = (byte)num4;
            habitat.XrayRadiation = (byte)num5;
            SelectHabitatPictures(habitat);
            return habitat;
        }

        public void SelectStar(out HabitatType type, out int diameter, out int pictureRef, out int mapPictureRef, out short solarRadiation, out short microwaveRadiation, out short xrayRadiation)
        {
            int num = Rnd.Next(0, 77);
            type = HabitatType.BarrenRock;
            diameter = 0;
            pictureRef = 0;
            mapPictureRef = 1;
            solarRadiation = 0;
            microwaveRadiation = 0;
            xrayRadiation = 0;
            int num2 = 0;
            if (num >= 0 && num <= 61)
            {
                type = HabitatType.MainSequence;
                diameter = Rnd.Next(950, 1400);
                if (diameter <= 1200)
                {
                    pictureRef = 83;
                }
                else
                {
                    pictureRef = 84;
                }
                num2 = Rnd.Next(0, 4);
                mapPictureRef = num2 + 1;
                solarRadiation = (short)Rnd.Next(40, 60);
                microwaveRadiation = (short)Rnd.Next(5, 20);
                xrayRadiation = (short)Rnd.Next(5, 12);
            }
            else if (num >= 62 && num <= 66)
            {
                type = HabitatType.RedGiant;
                diameter = Rnd.Next(1450, 1620);
                pictureRef = 85;
                num2 = Rnd.Next(0, 3);
                mapPictureRef = num2 + 7;
                solarRadiation = (short)Rnd.Next(70, 95);
                microwaveRadiation = (short)Rnd.Next(5, 20);
                xrayRadiation = (short)Rnd.Next(5, 12);
            }
            else if (num >= 67 && num <= 69)
            {
                type = HabitatType.SuperGiant;
                diameter = Rnd.Next(1620, 1950);
                pictureRef = 86;
                num2 = Rnd.Next(0, 3);
                mapPictureRef = num2 + 7;
                solarRadiation = (short)Rnd.Next(80, 100);
                microwaveRadiation = (short)Rnd.Next(5, 20);
                xrayRadiation = (short)Rnd.Next(5, 12);
            }
            else if (num >= 70 && num <= 72)
            {
                type = HabitatType.WhiteDwarf;
                diameter = Rnd.Next(260, 350);
                pictureRef = 87;
                num2 = Rnd.Next(0, 3);
                mapPictureRef = num2 + 13;
                solarRadiation = (short)Rnd.Next(10, 30);
                microwaveRadiation = (short)Rnd.Next(20, 40);
                xrayRadiation = (short)Rnd.Next(40, 60);
            }
            else if (num >= 73 && num <= 74)
            {
                type = HabitatType.Neutron;
                diameter = Rnd.Next(180, 230);
                pictureRef = 88;
                mapPictureRef = 6;
                solarRadiation = (short)Rnd.Next(1, 5);
                microwaveRadiation = (short)Rnd.Next(60, 90);
                xrayRadiation = (short)Rnd.Next(120, 200);
            }
            else if (num >= 75 && num <= 75)
            {
                type = HabitatType.BlackHole;
                diameter = Rnd.Next(4500, 6500);
                pictureRef = 95;
                mapPictureRef = 0;
                solarRadiation = (short)Rnd.Next(10, 15);
                microwaveRadiation = (short)Rnd.Next(60, 80);
                xrayRadiation = (short)Rnd.Next(90, 130);
            }
            else
            {
                type = HabitatType.SuperNova;
                diameter = Rnd.Next(300, 900);
                pictureRef = 0;
                mapPictureRef = 0;
                solarRadiation = (short)Rnd.Next(60, 80);
                microwaveRadiation = (short)Rnd.Next(70, 110);
                xrayRadiation = (short)Rnd.Next(160, 220);
            }
        }

        private HabitatAtmosphereType SelectAtmosphere(HabitatType habitatType, int habitatSize, out int atmosphereDensity)
        {
            HabitatAtmosphereType result = HabitatAtmosphereType.None;
            atmosphereDensity = 0;
            int num = 0;
            num = Rnd.Next(0, 10);
            switch (habitatType)
            {
                case HabitatType.BarrenRock:
                    result = ((num == 10 && habitatSize > 40) ? HabitatAtmosphereType.CarbonDioxide : HabitatAtmosphereType.None);
                    atmosphereDensity = Rnd.Next(10, 50);
                    break;
                case HabitatType.Continental:
                    if (num >= 0 && num <= 5)
                    {
                        result = HabitatAtmosphereType.NitrogenOxygen;
                    }
                    if (num >= 6 && num <= 8)
                    {
                        result = HabitatAtmosphereType.Oxygen;
                    }
                    if (num >= 9 && num <= 10)
                    {
                        result = HabitatAtmosphereType.CarbonDioxide;
                    }
                    atmosphereDensity = Rnd.Next(60, 100);
                    break;
                case HabitatType.FrozenGasGiant:
                    result = ((num > 6) ? HabitatAtmosphereType.NitrogenArgonMethane : HabitatAtmosphereType.HydrogenHelium);
                    atmosphereDensity = Rnd.Next(60, 100);
                    break;
                case HabitatType.GasGiant:
                    result = ((num > 9) ? HabitatAtmosphereType.NitrogenArgonMethane : HabitatAtmosphereType.HydrogenHelium);
                    atmosphereDensity = Rnd.Next(60, 100);
                    break;
                case HabitatType.Ice:
                    if (num >= 0 && num <= 5)
                    {
                        result = HabitatAtmosphereType.NitrogenOxygen;
                    }
                    if (num >= 6 && num <= 7)
                    {
                        result = HabitatAtmosphereType.CarbonDioxide;
                    }
                    if (num >= 8 && num <= 8)
                    {
                        result = HabitatAtmosphereType.SulphurDioxide;
                    }
                    if (num >= 9 && num <= 10)
                    {
                        result = HabitatAtmosphereType.Oxygen;
                    }
                    atmosphereDensity = Rnd.Next(40, 90);
                    break;
                case HabitatType.MarshySwamp:
                    if (num >= 0 && num <= 5)
                    {
                        result = HabitatAtmosphereType.CarbonDioxide;
                    }
                    if (num >= 6 && num <= 8)
                    {
                        result = HabitatAtmosphereType.NitrogenOxygen;
                    }
                    if (num >= 9 && num <= 10)
                    {
                        result = HabitatAtmosphereType.NitrogenArgonMethane;
                    }
                    atmosphereDensity = Rnd.Next(60, 100);
                    break;
                case HabitatType.Ocean:
                    if (num >= 0 && num <= 5)
                    {
                        result = HabitatAtmosphereType.CarbonDioxide;
                    }
                    if (num >= 6 && num <= 9)
                    {
                        result = HabitatAtmosphereType.NitrogenOxygen;
                    }
                    if (num >= 10 && num <= 10)
                    {
                        result = HabitatAtmosphereType.SulphurDioxide;
                    }
                    atmosphereDensity = Rnd.Next(50, 100);
                    break;
                case HabitatType.Desert:
                    if (num >= 0 && num <= 5)
                    {
                        result = HabitatAtmosphereType.NitrogenOxygen;
                    }
                    if (num >= 6 && num <= 8)
                    {
                        result = HabitatAtmosphereType.Oxygen;
                    }
                    if (num >= 9 && num <= 10)
                    {
                        result = HabitatAtmosphereType.None;
                    }
                    atmosphereDensity = Rnd.Next(30, 80);
                    break;
                case HabitatType.Volcanic:
                    if (num >= 0 && num <= 6)
                    {
                        result = HabitatAtmosphereType.SulphurDioxide;
                    }
                    if (num >= 7 && num <= 8)
                    {
                        result = HabitatAtmosphereType.CarbonDioxide;
                    }
                    if (num >= 9 && num <= 10)
                    {
                        result = HabitatAtmosphereType.NitrogenArgonMethane;
                    }
                    atmosphereDensity = Rnd.Next(30, 90);
                    break;
            }
            return result;
        }

        public double CalculateAngleFromCoords(double x, double y, double centerX, double centerY, double distance)
        {
            double num = 0.0;
            double num2 = Math.PI / 2.0;
            double num3 = num2 * -1.0;
            if (x < centerX)
            {
                if (y < centerY)
                {
                    return num3 - (num2 + Math.Asin((y - centerY) / distance));
                }
                return num2 + (num2 - Math.Asin((y - centerY) / distance));
            }
            if (y < centerY)
            {
                return Math.Asin((y - centerY) / distance);
            }
            return Math.Asin((y - centerY) / distance);
        }

        public static double CalculateDistanceStatic(double x1, double y1, double x2, double y2)
        {
            double num = x1 - x2;
            double num2 = y1 - y2;
            return Math.Sqrt(num2 * num2 + num * num);
        }

        public static double CalculateDistanceSquaredStatic(double x1, double y1, double x2, double y2)
        {
            double num = x1 - x2;
            double num2 = y1 - y2;
            return num2 * num2 + num * num;
        }

        public double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            double num = x1 - x2;
            double num2 = y1 - y2;
            return Math.Sqrt(num2 * num2 + num * num);
        }

        public Habitat IdentifyWonderHabitat(Empire empire, PlanetaryFacility wonder)
        {
            if (wonder != null && empire != null && empire.Colonies != null)
            {
                for (int i = 0; i < empire.Colonies.Count; i++)
                {
                    Habitat habitat = empire.Colonies[i];
                    if (habitat == null || habitat.HasBeenDestroyed || habitat.Facilities == null)
                    {
                        continue;
                    }
                    for (int j = 0; j < habitat.Facilities.Count; j++)
                    {
                        PlanetaryFacility planetaryFacility = habitat.Facilities[j];
                        if (planetaryFacility != null && planetaryFacility.ConstructionProgress >= 1f && planetaryFacility.Type == PlanetaryFacilityType.Wonder && planetaryFacility.WonderType == wonder.WonderType)
                        {
                            return habitat;
                        }
                    }
                }
            }
            return null;
        }

        public Habitat IdentifyRuinHabitat(Ruin ruin)
        {
            for (int i = 0; i < RuinsHabitats.Count; i++)
            {
                if (RuinsHabitats[i].Ruin != null && RuinsHabitats[i].Ruin == ruin)
                {
                    return RuinsHabitats[i];
                }
            }
            return null;
        }

        private Habitat FindNearestSystemGasCloudAsteroidInIndex(int x, int y, GalaxyIndex index, out double distance)
        {
            Habitat habitat = null;
            distance = double.MaxValue;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            for (int i = 0; i < habitatList.Count; i++)
            {
                if (habitatList[i].Parent == null)
                {
                    double num = CalculateDistanceSquared(x, y, habitatList[i].Xpos, habitatList[i].Ypos);
                    if (num < distance)
                    {
                        habitat = habitatList[i];
                        distance = num;
                    }
                }
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        public BuiltObject FastFindNearestShipInSystem(double x, double y, Habitat systemStar)
        {
            BuiltObject result = null;
            double num = double.MaxValue;
            BuiltObjectList builtObjectsAtLocation = GetBuiltObjectsAtLocation(x, y, MaxSolarSystemSize * 2);
            for (int i = 0; i < builtObjectsAtLocation.Count; i++)
            {
                BuiltObject builtObject = builtObjectsAtLocation[i];
                if (builtObject != null && builtObject.NearestSystemStar == systemStar)
                {
                    double num2 = CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                    if (num2 < num)
                    {
                        num = num2;
                        result = builtObjectsAtLocation[i];
                    }
                }
            }
            return result;
        }

        public Habitat FastFindNearestPlanetMoonOfTypesUnoccupiedSystem(double x, double y, Empire empire, List<HabitatType> types)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                foreach (GalaxyIndex item in galaxyIndexList)
                {
                    double distance;
                    Habitat habitat = FastFindNearestPlanetMoonOfTypedUnoccupiedSystemInIndex((int)x, (int)y, item, out distance, empire, types);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private Habitat FastFindNearestPlanetMoonOfTypedUnoccupiedSystemInIndex(int x, int y, GalaxyIndex index, out double distance, Empire empire, List<HabitatType> types)
        {
            Habitat habitat = null;
            SystemInfoList systemInfoList = SystemsIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < systemInfoList.Count; i++)
            {
                if (systemInfoList[i].PlanetCount <= 0)
                {
                    continue;
                }
                EmpireSystemSummary dominantEmpire = systemInfoList[i].DominantEmpire;
                if (dominantEmpire != null && dominantEmpire.Empire != null && dominantEmpire.Empire != empire)
                {
                    continue;
                }
                if (systemInfoList[i].SystemStar != null)
                {
                    int num = CheckEmpireTerritoryIdAtLocation(systemInfoList[i].SystemStar.Xpos, systemInfoList[i].SystemStar.Ypos);
                    if (num >= 0 && num != empire.EmpireId)
                    {
                        continue;
                    }
                }
                int num2 = Rnd.Next(0, systemInfoList[i].Habitats.Count);
                for (int j = num2; j < systemInfoList[i].Habitats.Count; j++)
                {
                    if ((systemInfoList[i].Habitats[j].Category == HabitatCategoryType.Moon || systemInfoList[i].Habitats[j].Category == HabitatCategoryType.Planet) && (types == null || types.Count == 0 || types.Contains(systemInfoList[i].Habitats[j].Type)) && (systemInfoList[i].Habitats[j].Empire == null || systemInfoList[i].Habitats[j].Empire == IndependentEmpire))
                    {
                        double num3 = CalculateDistanceSquared(x, y, systemInfoList[i].Habitats[j].Xpos, systemInfoList[i].Habitats[j].Ypos);
                        if (num3 < distance)
                        {
                            habitat = systemInfoList[i].Habitats[j];
                            distance = num3;
                        }
                    }
                }
                for (int k = 0; k < num2; k++)
                {
                    if ((systemInfoList[i].Habitats[k].Category == HabitatCategoryType.Moon || systemInfoList[i].Habitats[k].Category == HabitatCategoryType.Planet) && (types == null || types.Count == 0 || types.Contains(systemInfoList[i].Habitats[k].Type)) && (systemInfoList[i].Habitats[k].Empire == null || systemInfoList[i].Habitats[k].Empire == IndependentEmpire))
                    {
                        double num4 = CalculateDistanceSquared(x, y, systemInfoList[i].Habitats[k].Xpos, systemInfoList[i].Habitats[k].Ypos);
                        if (num4 < distance)
                        {
                            habitat = systemInfoList[i].Habitats[k];
                            distance = num4;
                        }
                    }
                }
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        public Habitat FastFindNearestSystemWithPlanets(double x, double y)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    SystemInfo systemInfo = FastFindNearestSystemWithPlanetsInIndex((int)x, (int)y, index, out distance);
                    if (distance < num)
                    {
                        result = systemInfo.SystemStar;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private SystemInfo FastFindNearestSystemWithPlanetsInIndex(int x, int y, GalaxyIndex index, out double distance)
        {
            SystemInfo systemInfo = null;
            SystemInfoList systemInfoList = SystemsIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < systemInfoList.Count; i++)
            {
                if (systemInfoList[i].PlanetCount > 0)
                {
                    double num = CalculateDistanceSquared(x, y, systemInfoList[i].SystemStar.Xpos, systemInfoList[i].SystemStar.Ypos);
                    if (num < distance)
                    {
                        systemInfo = systemInfoList[i];
                        distance = num;
                    }
                }
            }
            if (systemInfo != null)
            {
                distance = CalculateDistance(x, y, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
            }
            return systemInfo;
        }

        public SystemInfoDistanceList GenerateDistanceOrderedSystemList(double x, double y)
        {
            SystemInfoDistanceList systemInfoDistanceList = new SystemInfoDistanceList();
            for (int i = 0; i < Systems.Count; i++)
            {
                SystemInfo systemInfo = Systems[i];
                double distance = CalculateDistanceSquared(x, y, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
                SystemInfoDistance systemInfoDistance = new SystemInfoDistance();
                systemInfoDistance.SystemInfo = systemInfo;
                systemInfoDistance.Distance = distance;
                systemInfoDistanceList.Add(systemInfoDistance);
            }
            systemInfoDistanceList.Sort();
            return systemInfoDistanceList;
        }

        public bool CheckEmpireCanRefuelAtEmpire(BuiltObject shipToRefuel, Empire empire, Empire refuelingEmpire)
        {
            if (shipToRefuel == null)
            {
                return false;
            }
            if (shipToRefuel.Role == BuiltObjectRole.Military)
            {
                if (empire == null || refuelingEmpire == null)
                {
                    return true;
                }
                if (empire == refuelingEmpire)
                {
                    return true;
                }
                if (empire == IndependentEmpire)
                {
                    return true;
                }
                if (refuelingEmpire == IndependentEmpire)
                {
                    return true;
                }
                if (empire.PirateEmpireBaseHabitat != null)
                {
                    if (refuelingEmpire == empire || refuelingEmpire == IndependentEmpire)
                    {
                        return true;
                    }
                    return false;
                }
                if (refuelingEmpire.PirateEmpireBaseHabitat != null)
                {
                    if (empire == refuelingEmpire)
                    {
                        return true;
                    }
                    return false;
                }
                if (refuelingEmpire.Policy != null)
                {
                    return refuelingEmpire.ObtainDiplomaticRelation(empire)?.MilitaryRefuelingToOther ?? false;
                }
            }
            return true;
        }

        public StellarObject FastFindNearestRefuellingPoint(double x, double y, ResourceList fuelTypes, Empire empire, BuiltObject shipToRefuel)
        {
            return FastFindNearestRefuellingPoint(x, y, fuelTypes, empire, shipToRefuel, includeResupplyShips: false, null);
        }

        public StellarObject FastFindNearestRefuellingPoint(double x, double y, ResourceList fuelTypes, Empire empire, BuiltObject shipToRefuel, bool includeResupplyShips, Empire empireToExclude)
        {
            return FastFindNearestRefuellingPoint(x, y, fuelTypes, empire, shipToRefuel, includeResupplyShips, empireToExclude, 1);
        }

        public StellarObject FastFindNearestRefuellingPoint(double x, double y, ResourceList fuelTypes, Empire empire, BuiltObject shipToRefuel, bool includeResupplyShips, Empire empireToExclude, int shipsToRefuel)
        {
            double num = double.MaxValue;
            StellarObject stellarObject = null;
            if (includeResupplyShips && empire != null)
            {
                double num2 = double.MaxValue;
                for (int i = 0; i < empire.ResupplyShips.Count; i++)
                {
                    BuiltObject builtObject = empire.ResupplyShips[i];
                    if (builtObject.IsFunctional && builtObject.IsDeployed)
                    {
                        double num3 = CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                        if (num3 < num2 && CheckSufficientFuelAvailable(empire, fuelTypes, builtObject, builtObject.ActualEmpire))
                        {
                            num2 = num3;
                            stellarObject = builtObject;
                        }
                    }
                }
                for (int j = 0; j < empire.RefuellingDepots.Count; j++)
                {
                    BuiltObject builtObject2 = empire.RefuellingDepots[j];
                    if ((builtObject2.SubRole != BuiltObjectSubRole.ResupplyShip || builtObject2.IsDeployed) && builtObject2.ParentHabitat == null && builtObject2.IsFunctional)
                    {
                        double num4 = CalculateDistanceSquared(x, y, builtObject2.Xpos, builtObject2.Ypos);
                        if (num4 < num2 && CheckSufficientFuelAvailable(empire, fuelTypes, builtObject2, builtObject2.ActualEmpire))
                        {
                            num2 = num4;
                            stellarObject = builtObject2;
                        }
                    }
                }
                if (stellarObject != null)
                {
                    num = Math.Sqrt(num2);
                }
            }
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num5 = 0;
            int num6 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num6, 10000, ref iterationCount))
            {
                num6 = DetermineSectorBoundaries(num5, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int k = 0; k < galaxyIndexList.Count; k++)
                {
                    GalaxyIndex index = galaxyIndexList[k];
                    double distance;
                    StellarObject stellarObject2 = FastFindNearestRefuellingPointInIndex((int)x, (int)y, index, out distance, fuelTypes, empire, shipToRefuel, includeResupplyShips, empireToExclude, shipsToRefuel, num);
                    if (!(distance < num))
                    {
                        continue;
                    }
                    if (shipsToRefuel <= 1)
                    {
                        stellarObject = stellarObject2;
                        num = distance;
                    }
                    else if (stellarObject != null && stellarObject.DockingBays != null && stellarObject.DockingBays.Count >= 4)
                    {
                        if (stellarObject2.DockingBays != null && stellarObject2.DockingBays.Count >= 4)
                        {
                            stellarObject = stellarObject2;
                            num = distance;
                        }
                    }
                    else
                    {
                        stellarObject = stellarObject2;
                        num = distance;
                    }
                }
                num5++;
                if (num5 > IndexMaxX)
                {
                    break;
                }
            }
            return stellarObject;
        }

        public bool IdentifyWhetherSystemIsRefuellingPointForEmpire(Habitat systemStar, Empire empire, Resource fuelType, BuiltObject testMilitaryShip)
        {
            SystemVisibilityStatus systemVisibilityStatus = SystemVisibilityStatus.Visible;
            if (empire != null && empire != IndependentEmpire)
            {
                systemVisibilityStatus = empire.CheckSystemVisibilityStatus(systemStar.SystemIndex);
            }
            if (systemVisibilityStatus == SystemVisibilityStatus.Explored || systemVisibilityStatus == SystemVisibilityStatus.Visible)
            {
                if (systemStar.Category == HabitatCategoryType.GasCloud && systemStar.BasesAtHabitat.Count > 0)
                {
                    foreach (BuiltObject item in systemStar.BasesAtHabitat)
                    {
                        if (!item.IsRefuellingDepot || item.Empire == null)
                        {
                            continue;
                        }
                        bool flag = true;
                        if (empire != null && empire != IndependentEmpire)
                        {
                            flag = empire.IsObjectVisibleToThisEmpire(item, includeLongRangeScanners: true, includeShipsOutsideSystems: false);
                        }
                        if (!flag || !IsStellarObjectDockable(item, empire) || !CheckEmpireCanRefuelAtEmpire(testMilitaryShip, empire, item.Empire))
                        {
                            continue;
                        }
                        int num = -1;
                        if (item.Cargo != null)
                        {
                            num = item.Cargo.IndexOf(fuelType, item.Empire);
                        }
                        if (num >= 0)
                        {
                            int num2 = item.Cargo[num].Available;
                            if (empire != item.Empire)
                            {
                                int num3 = CalculateResourceLevel(fuelType, item);
                                num2 -= num3;
                            }
                            if (num2 >= MinimumLevelForRefuellingPoint)
                            {
                                return true;
                            }
                        }
                    }
                }
                SystemInfo systemInfo = Systems[systemStar.SystemIndex];
                for (int i = 0; i < systemInfo.Habitats.Count; i++)
                {
                    Habitat habitat = systemInfo.Habitats[i];
                    if (habitat.BasesAtHabitat.Count > 0)
                    {
                        foreach (BuiltObject item2 in habitat.BasesAtHabitat)
                        {
                            if (!item2.IsRefuellingDepot || item2.Empire == null || !IsStellarObjectDockable(item2, empire))
                            {
                                continue;
                            }
                            bool flag2 = true;
                            if (empire != null && empire != IndependentEmpire && item2.SubRole != BuiltObjectSubRole.SmallSpacePort && item2.SubRole != BuiltObjectSubRole.MediumSpacePort && item2.SubRole != BuiltObjectSubRole.LargeSpacePort)
                            {
                                flag2 = empire.IsObjectVisibleToThisEmpire(item2, includeLongRangeScanners: true, includeShipsOutsideSystems: false);
                            }
                            if (!flag2 || !CheckEmpireCanRefuelAtEmpire(testMilitaryShip, empire, item2.Empire))
                            {
                                continue;
                            }
                            int num4 = -1;
                            if (item2.Cargo != null)
                            {
                                num4 = item2.Cargo.IndexOf(fuelType, item2.Empire);
                            }
                            if (num4 >= 0)
                            {
                                int num5 = item2.Cargo[num4].Available;
                                if (empire != item2.Empire)
                                {
                                    int num6 = CalculateResourceLevel(fuelType, item2);
                                    num5 -= num6;
                                }
                                if (num5 >= MinimumLevelForRefuellingPoint)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (habitat.Population.Count <= 0 || habitat.Empire == null || !IsStellarObjectDockable(habitat, empire) || !CheckEmpireCanRefuelAtEmpire(testMilitaryShip, empire, habitat.Empire))
                        {
                            continue;
                        }
                        int num7 = -1;
                        if (habitat.Cargo != null)
                        {
                            num7 = habitat.Cargo.IndexOf(fuelType, habitat.Empire);
                        }
                        if (num7 >= 0)
                        {
                            int num8 = habitat.Cargo[num7].Available;
                            if (empire != habitat.Empire)
                            {
                                int num9 = CalculateResourceLevel(fuelType, habitat);
                                num8 -= num9;
                            }
                            if (num8 >= MinimumLevelForRefuellingPoint)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public StellarObject DetermineNearestHabitatIfPossible(StellarObject target)
        {
            StellarObject result = target;
            if (target is Habitat)
            {
                result = target;
            }
            else if (target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)target;
                result = ((builtObject.ParentHabitat != null) ? builtObject.ParentHabitat : ((builtObject.NearestSystemStar == null) ? ((StellarObject)builtObject) : ((StellarObject)builtObject.NearestSystemStar)));
            }
            else if (target is Creature)
            {
                Creature creature = (Creature)target;
                result = ((creature.ParentHabitat != null) ? creature.ParentHabitat : ((creature.NearestSystemStar == null) ? ((StellarObject)creature) : ((StellarObject)creature.NearestSystemStar)));
            }
            return result;
        }

        private StellarObject FastFindNearestRefuellingPointInIndex(double x, double y, GalaxyIndex index, out double distance, ResourceList fuelTypes, Empire empire, BuiltObject shipToRefuel, bool includeResupplyShips, Empire empireToExclude, int shipsToRefuel, double nearestDistance)
        {
            StellarObject stellarObject = null;
            distance = double.MaxValue;
            double num = Math.Min(double.MaxValue, nearestDistance * nearestDistance);
            SystemInfoList systemInfoList = SystemsIndex[index.X][index.Y];
            for (int i = 0; i < systemInfoList.Count; i++)
            {
                SystemInfo systemInfo = systemInfoList[i];
                SystemVisibilityStatus systemVisibilityStatus = SystemVisibilityStatus.Visible;
                if (empire != null && empire != IndependentEmpire)
                {
                    systemVisibilityStatus = empire.CheckSystemVisibilityStatus(systemInfo.SystemStar.SystemIndex);
                }
                if (systemVisibilityStatus != SystemVisibilityStatus.Explored && systemVisibilityStatus != SystemVisibilityStatus.Visible)
                {
                    continue;
                }
                double num2 = CalculateDistanceSquared(x, y, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
                if (!(num2 < num) && !(num2 < 2500000000.0))
                {
                    continue;
                }
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud && systemInfo.SystemStar.BasesAtHabitat.Count > 0)
                {
                    for (int j = 0; j < systemInfo.SystemStar.BasesAtHabitat.Count; j++)
                    {
                        BuiltObject builtObject = systemInfo.SystemStar.BasesAtHabitat[j];
                        if (!builtObject.IsRefuellingDepot || builtObject.Empire == null || (empireToExclude != null && builtObject.Empire == empireToExclude))
                        {
                            continue;
                        }
                        bool flag = true;
                        if (empire != null && empire.PirateEmpireBaseHabitat == null && empire != IndependentEmpire)
                        {
                            flag = empire.IsObjectVisibleToThisEmpire(builtObject, includeLongRangeScanners: true, includeShipsOutsideSystems: false);
                        }
                        if (!flag || !IsStellarObjectDockable(builtObject, empire) || !CheckEmpireCanRefuelAtEmpire(shipToRefuel, empire, builtObject.Empire))
                        {
                            continue;
                        }
                        double num3 = CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                        if (!(num3 < distance) || !CheckSufficientFuelAvailable(empire, fuelTypes, builtObject, builtObject.ActualEmpire))
                        {
                            continue;
                        }
                        bool flag2 = false;
                        if (shipsToRefuel <= 1)
                        {
                            if ((builtObject.DockingBays != null && builtObject.DockingBays.Count >= 4) || (builtObject.DockingBayWaitQueue != null && builtObject.DockingBayWaitQueue.Count <= 3))
                            {
                                flag2 = true;
                            }
                        }
                        else if (builtObject.DockingBays != null && builtObject.DockingBays.Count >= 4)
                        {
                            flag2 = true;
                        }
                        if (flag2)
                        {
                            distance = num3;
                            num = num3;
                            stellarObject = builtObject;
                        }
                    }
                }
                for (int k = 0; k < systemInfo.Habitats.Count; k++)
                {
                    Habitat habitat = systemInfo.Habitats[k];
                    double num4 = CalculateDistanceSquared(x, y, habitat.Xpos, habitat.Ypos);
                    if (!(num4 < num))
                    {
                        continue;
                    }
                    bool flag3 = false;
                    if (habitat.BasesAtHabitat.Count > 0)
                    {
                        for (int l = 0; l < habitat.BasesAtHabitat.Count; l++)
                        {
                            BuiltObject builtObject2 = habitat.BasesAtHabitat[l];
                            if (!builtObject2.IsRefuellingDepot || builtObject2.Empire == null || (empireToExclude != null && builtObject2.Empire == empireToExclude) || !IsStellarObjectDockable(builtObject2, empire))
                            {
                                continue;
                            }
                            bool flag4 = true;
                            if (empire != null && empire != IndependentEmpire && builtObject2.SubRole != BuiltObjectSubRole.SmallSpacePort && builtObject2.SubRole != BuiltObjectSubRole.MediumSpacePort && builtObject2.SubRole != BuiltObjectSubRole.LargeSpacePort)
                            {
                                flag4 = empire.IsObjectVisibleToThisEmpire(builtObject2, includeLongRangeScanners: true, includeShipsOutsideSystems: false);
                            }
                            if (!flag4 || !CheckEmpireCanRefuelAtEmpire(shipToRefuel, empire, builtObject2.Empire))
                            {
                                continue;
                            }
                            double num5 = CalculateDistanceSquared(x, y, builtObject2.Xpos, builtObject2.Ypos);
                            if (!(num5 < distance) || !CheckSufficientFuelAvailable(empire, fuelTypes, builtObject2, builtObject2.ActualEmpire))
                            {
                                continue;
                            }
                            bool flag5 = false;
                            if (shipsToRefuel <= 1)
                            {
                                if ((builtObject2.DockingBays != null && builtObject2.DockingBays.Count >= 4) || (builtObject2.DockingBayWaitQueue != null && builtObject2.DockingBayWaitQueue.Count <= 0))
                                {
                                    flag5 = true;
                                }
                            }
                            else if (builtObject2.DockingBays != null && builtObject2.DockingBays.Count >= 4)
                            {
                                flag5 = true;
                            }
                            if (flag5)
                            {
                                distance = num5;
                                stellarObject = builtObject2;
                                num = num5;
                                flag3 = true;
                            }
                        }
                    }
                    if (!flag3 && habitat.Population.Count > 0 && habitat.IsRefuellingDepot && habitat.Empire != null && (empireToExclude == null || habitat.Empire != empireToExclude) && IsStellarObjectDockable(habitat, empire) && CheckEmpireCanRefuelAtEmpire(shipToRefuel, empire, habitat.Empire))
                    {
                        double num6 = CalculateDistanceSquared(x, y, habitat.Xpos, habitat.Ypos);
                        if (num6 < distance && CheckSufficientFuelAvailable(empire, fuelTypes, habitat, habitat.Empire))
                        {
                            distance = num6;
                            stellarObject = habitat;
                            num = num6;
                        }
                    }
                }
            }
            if (stellarObject != null)
            {
                distance = CalculateDistance(x, y, stellarObject.Xpos, stellarObject.Ypos);
            }
            return stellarObject;
        }

        public bool CheckFuelSuppliedAtLocation(ResourceList fuelTypes, BuiltObject builtObject, Empire refuellingEmpire, bool mustHaveActualSupply)
        {
            bool result = true;
            if (mustHaveActualSupply)
            {
                result = CheckSufficientFuelAvailable(refuellingEmpire, fuelTypes, builtObject, builtObject.ActualEmpire);
            }
            else if (builtObject.SubRole == BuiltObjectSubRole.GasMiningStation && builtObject.IsResourceExtractor)
            {
                if (builtObject.ParentHabitat != null && builtObject.ParentHabitat.Resources != null)
                {
                    for (int i = 0; i < fuelTypes.Count; i++)
                    {
                        if (!builtObject.ParentHabitat.Resources.ContainsId(fuelTypes[i].ResourceID))
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            else if (refuellingEmpire.PirateEmpireBaseHabitat != null && (builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort) && builtObject.IsResourceExtractor && builtObject.ParentHabitat != null && builtObject.ParentHabitat.Resources != null)
            {
                bool flag = true;
                for (int j = 0; j < fuelTypes.Count; j++)
                {
                    if (!builtObject.ParentHabitat.Resources.ContainsId(fuelTypes[j].ResourceID))
                    {
                        flag = false;
                        break;
                    }
                }
                result = flag || CheckSufficientFuelAvailable(refuellingEmpire, fuelTypes, builtObject, builtObject.ActualEmpire);
            }
            else
            {
                result = CheckSufficientFuelAvailable(refuellingEmpire, fuelTypes, builtObject, builtObject.ActualEmpire);
            }
            return result;
        }

        public bool CheckSufficientFuelAvailable(Empire fuellingEmpire, ResourceList fuelTypes, StellarObject fuelLocation, Empire locationEmpire)
        {
            bool result = true;
            if (fuelTypes != null && fuelTypes.Count > 0 && fuelLocation != null && locationEmpire != null)
            {
                result = false;
                for (int i = 0; i < fuelTypes.Count; i++)
                {
                    Resource resource = fuelTypes[i];
                    if (resource == null)
                    {
                        continue;
                    }
                    int num = -1;
                    if (fuelLocation.Cargo != null)
                    {
                        num = fuelLocation.Cargo.IndexOf(resource, locationEmpire);
                    }
                    if (num < 0)
                    {
                        continue;
                    }
                    int num2 = fuelLocation.Cargo[num].Available;
                    if (fuellingEmpire != fuelLocation.Empire)
                    {
                        int num3 = 0;
                        if (fuelLocation is BuiltObject)
                        {
                            num3 = CalculateResourceLevel(resource, (BuiltObject)fuelLocation);
                        }
                        else if (fuelLocation is Habitat)
                        {
                            num3 = CalculateResourceLevel(resource, (Habitat)fuelLocation);
                        }
                        num2 -= num3;
                    }
                    if (num2 >= (int)resource.SortTag)
                    {
                        result = true;
                        continue;
                    }
                    result = false;
                    break;
                }
            }
            return result;
        }

        public Habitat FastFindNearestUncolonizedOwnedSystem(double x, double y)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    SystemInfo systemInfo = FindNearestOwnedUncolonizedSystemInIndex((int)x, (int)y, index, out distance);
                    if (distance < num)
                    {
                        result = systemInfo.SystemStar;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private SystemInfo FindNearestOwnedUncolonizedSystemInIndex(int x, int y, GalaxyIndex index, out double distance)
        {
            SystemInfo systemInfo = null;
            SystemInfoList systemInfoList = SystemsIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < systemInfoList.Count; i++)
            {
                bool disputed = false;
                int num = EmpireTerritory.CheckSystemOwnership(this, systemInfoList[i].SystemStar, out disputed);
                if (num < 0)
                {
                    continue;
                }
                SystemInfo systemInfo2 = Systems[systemInfoList[i].SystemStar];
                if (systemInfo2 == null || (systemInfo2.DominantEmpire != null && systemInfo2.DominantEmpire.Empire != null))
                {
                    continue;
                }
                bool flag = true;
                Empire byEmpireId = Empires.GetByEmpireId(num);
                if (byEmpireId != null && byEmpireId.Reclusive)
                {
                    flag = false;
                }
                if (flag)
                {
                    double num2 = CalculateDistanceSquared(x, y, systemInfoList[i].SystemStar.Xpos, systemInfoList[i].SystemStar.Ypos);
                    if (num2 < distance)
                    {
                        systemInfo = systemInfoList[i];
                        distance = num2;
                    }
                }
            }
            if (systemInfo != null)
            {
                distance = CalculateDistance(x, y, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
            }
            return systemInfo;
        }

        public Habitat FindNearestHabitatInSystem(SystemInfo system, double x, double y)
        {
            double num = double.MaxValue;
            Habitat result = null;
            if (system != null)
            {
                if (system.SystemStar != null)
                {
                    double num2 = CalculateDistanceSquared(x, y, system.SystemStar.Xpos, system.SystemStar.Ypos);
                    if (num2 < num)
                    {
                        num = num2;
                        result = system.SystemStar;
                    }
                }
                if (system.Habitats != null)
                {
                    for (int i = 0; i < system.Habitats.Count; i++)
                    {
                        Habitat habitat = system.Habitats[i];
                        if (habitat != null)
                        {
                            double num3 = CalculateDistanceSquared(x, y, habitat.Xpos, habitat.Ypos);
                            if (num3 < num)
                            {
                                result = habitat;
                                num = num3;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public Habitat FastFindNearestSystem(double x, double y)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    SystemInfo systemInfo = FindNearestSystemInIndex((int)x, (int)y, index, out distance);
                    if (distance < num)
                    {
                        result = systemInfo.SystemStar;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private SystemInfo FindNearestSystemInIndex(int x, int y, GalaxyIndex index, out double distance)
        {
            SystemInfo systemInfo = null;
            SystemInfoList systemInfoList = SystemsIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < systemInfoList.Count; i++)
            {
                SystemInfo systemInfo2 = systemInfoList[i];
                if (systemInfo2 != null && systemInfo2.SystemStar != null)
                {
                    double num = CalculateDistanceSquared(x, y, systemInfo2.SystemStar.Xpos, systemInfo2.SystemStar.Ypos);
                    if (num < distance)
                    {
                        systemInfo = systemInfo2;
                        distance = num;
                    }
                }
            }
            if (systemInfo != null)
            {
                distance = CalculateDistance(x, y, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
            }
            return systemInfo;
        }

        public Habitat FindNearestSystemGasCloudAsteroid(int x, int y)
        {
            return FindNearestSystemGasCloudAsteroid((double)x, (double)y);
        }

        public Habitat FindNearestSystemGasCloudAsteroid(double x, double y)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    Habitat habitat = FindNearestSystemGasCloudAsteroidInIndex((int)x, (int)y, index, out distance);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        public void SelectHyperJumpExitPoint(out double x, out double y, double minimumExitDistance)
        {
            double num = Rnd.NextDouble() * Math.PI;
            if (Rnd.Next(0, 2) == 1)
            {
                num *= -1.0;
            }
            double num2 = minimumExitDistance + Rnd.NextDouble() * minimumExitDistance * 0.4;
            x = Math.Cos(num) * num2;
            y = Math.Sin(num) * num2;
        }

        public void SelectRelativePoint(double range, out double x, out double y)
        {
            double num = range * Rnd.NextDouble();
            double num2 = SelectRandomHeading();
            x = Math.Cos(num2) * num;
            y = Math.Sin(num2) * num;
        }

        public void SelectRelativeHabitatSurfacePoint(Habitat habitat, out double x, out double y)
        {
            double range = 50.0;
            if (habitat != null)
            {
                double num = (double)habitat.Diameter - 10.0;
                if (num < 1.0)
                {
                    num = 1.0;
                }
                range = num / 2.0;
            }
            SelectRelativePoint(range, out x, out y);
        }

        public void SelectRelativeParkingPoint(double minimumDistance, out double x, out double y)
        {
            double num = Rnd.NextDouble() * Math.PI;
            if (Rnd.Next(0, 2) == 1)
            {
                num *= -1.0;
            }
            double num2 = minimumDistance + Rnd.NextDouble() * (double)MovementDecelerationRange;
            x = Math.Cos(num) * num2;
            y = Math.Sin(num) * num2;
        }

        public void SelectRelativeParkingPoint(out double x, out double y)
        {
            SelectRelativeParkingPoint(MovementDecelerationRange, out x, out y);
        }

        public static void CorrectIndexCoords(ref int x, ref int y)
        {
            if (x < 0)
            {
                x = 0;
            }
            else if (x >= IndexMaxX)
            {
                x = IndexMaxX - 1;
            }
            if (y < 0)
            {
                y = 0;
            }
            else if (y >= IndexMaxY)
            {
                y = IndexMaxY - 1;
            }
        }

        public static void CorrectSectorCoords(ref int x, ref int y)
        {
            if (x < 0)
            {
                x = 0;
            }
            else if (x >= SectorMaxX)
            {
                x = SectorMaxX - 1;
            }
            if (y < 0)
            {
                y = 0;
            }
            else if (y >= SectorMaxY)
            {
                y = SectorMaxY - 1;
            }
        }

        public Habitat FindNearestSystemGasCloudAsteroidInIndex(double x, double y, GalaxyIndex index)
        {
            double distance = double.MaxValue;
            return FindNearestSystemGasCloudAsteroidInIndex((int)x, (int)y, index, out distance);
        }

        private Habitat FindNearestUnexploredHabitatInSystem(double x, double y, Habitat systemStar, Empire empire, double asteroidRangeFactor)
        {
            double num = double.MaxValue;
            Habitat result = null;
            double num2 = asteroidRangeFactor * asteroidRangeFactor;
            if (systemStar != null && empire.ResourceMap != null)
            {
                for (int i = 0; i < Systems[systemStar.SystemIndex].Habitats.Count; i++)
                {
                    Habitat habitat = Systems[systemStar.SystemIndex].Habitats[i];
                    bool flag = false;
                    if (!empire.ResourceMap.CheckResourcesKnown(habitat))
                    {
                        flag = true;
                    }
                    else if (!empire.Reclusive && habitat.Ruin != null)
                    {
                        if (habitat.Ruin.Type == RuinType.UnlockResearchProject)
                        {
                            if (habitat.Ruin.ResearchProjectId >= 0 && empire.Research != null && empire.Research.TechTree != null && empire.Research.TechTree.Count > habitat.Ruin.ResearchProjectId && empire.Research.TechTree[habitat.Ruin.ResearchProjectId] != null && !empire.Research.TechTree[habitat.Ruin.ResearchProjectId].IsEnabled)
                            {
                                flag = true;
                            }
                        }
                        else if (CheckRuinsHaveBenefit(habitat.Ruin, empire) && (habitat.Ruin.StoryClueLevel < 0 || (habitat.Ruin.StoryClueLevel >= 0 && empire == PlayerEmpire)))
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        double num3 = CalculateDistanceSquared(x, y, habitat.Xpos, habitat.Ypos);
                        if (habitat.Category == HabitatCategoryType.Asteroid)
                        {
                            num3 *= num2;
                        }
                        if (num3 < num)
                        {
                            result = habitat;
                            num = num3;
                        }
                    }
                }
            }
            return result;
        }

        private SystemInfo FastFindNearestUnexploredSystemInfo(double x, double y, Empire empire)
        {
            double num = double.MaxValue;
            SystemInfo result = null;
            for (int i = 0; i < Systems.Count; i++)
            {
                SystemInfo systemInfo = Systems[i];
                SystemVisibilityStatus systemVisibilityStatus = SystemVisibilityStatus.Unexplored;
                if (systemInfo.SystemStar != null)
                {
                    systemVisibilityStatus = empire.CheckSystemVisibilityStatus(systemInfo.SystemStar.SystemIndex);
                }
                if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored || systemVisibilityStatus == SystemVisibilityStatus.Undefined)
                {
                    double num2 = CalculateDistanceSquared(x, y, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
                    if (num2 < num)
                    {
                        result = systemInfo;
                        num = num2;
                    }
                }
            }
            return result;
        }

        private SystemInfoDistanceList GenerateDistanceOrderedSystemListUnexplored(double x, double y, Empire empire)
        {
            SystemInfoDistanceList systemInfoDistanceList = new SystemInfoDistanceList();
            for (int i = 0; i < Systems.Count; i++)
            {
                SystemInfo systemInfo = Systems[i];
                SystemVisibilityStatus systemVisibilityStatus = SystemVisibilityStatus.Unexplored;
                if (systemInfo.SystemStar != null)
                {
                    systemVisibilityStatus = empire.CheckSystemVisibilityStatus(systemInfo.SystemStar.SystemIndex);
                }
                if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored || systemVisibilityStatus == SystemVisibilityStatus.Undefined)
                {
                    double distance = CalculateDistanceSquared(x, y, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
                    SystemInfoDistance systemInfoDistance = new SystemInfoDistance();
                    systemInfoDistance.SystemInfo = systemInfo;
                    systemInfoDistance.Distance = distance;
                    systemInfoDistanceList.Add(systemInfoDistance);
                }
            }
            systemInfoDistanceList.Sort();
            return systemInfoDistanceList;
        }

        public Habitat FastFindNearestUnexploredSystem(double x, double y, Empire empire)
        {
            SystemInfoDistanceList systemInfoDistanceList = GenerateDistanceOrderedSystemListUnexplored(x, y, empire);
            if (systemInfoDistanceList != null && systemInfoDistanceList.Count > 0 && systemInfoDistanceList[0].SystemInfo != null && systemInfoDistanceList[0].SystemInfo.SystemStar != null)
            {
                return systemInfoDistanceList[0].SystemInfo.SystemStar;
            }
            return null;
        }

        public Habitat UltraFastFindNearestUnexploredSystem(double x, double y, Empire empire)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    SystemInfo systemInfo = UltraFastFindNearestUnexploredSystemInIndex(x, y, index, out distance, empire);
                    if (distance < num)
                    {
                        result = systemInfo.SystemStar;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private SystemInfo UltraFastFindNearestUnexploredSystemInIndex(double x, double y, GalaxyIndex index, out double distance, Empire empire)
        {
            SystemInfo systemInfo = null;
            SystemInfoList systemInfoList = SystemsIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < systemInfoList.Count; i++)
            {
                SystemVisibilityStatus systemVisibilityStatus = SystemVisibilityStatus.Unexplored;
                if (systemInfoList[i].SystemStar != null)
                {
                    systemVisibilityStatus = empire.CheckSystemVisibilityStatus(systemInfoList[i].SystemStar.SystemIndex);
                }
                if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored || systemVisibilityStatus == SystemVisibilityStatus.Undefined)
                {
                    double num = CalculateDistanceSquared(x, y, systemInfoList[i].SystemStar.Xpos, systemInfoList[i].SystemStar.Ypos);
                    if (num < distance)
                    {
                        systemInfo = systemInfoList[i];
                        distance = num;
                    }
                }
            }
            if (systemInfo != null)
            {
                distance = CalculateDistance(x, y, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
            }
            return systemInfo;
        }

        public Habitat FindNextSystemToScout(Empire empire, BuiltObject explorationShip, out Point location)
        {
            location = Point.Empty;
            if (explorationShip.NearestSystemStar != null)
            {
                SystemInfo systemInfo = Systems[explorationShip.NearestSystemStar.SystemIndex];
                if (systemInfo.HasRuins)
                {
                    for (int i = 0; i < systemInfo.Habitats.Count; i++)
                    {
                        Habitat habitat = systemInfo.Habitats[i];
                        if (habitat.Ruin == null || empire.Reclusive)
                        {
                            continue;
                        }
                        bool flag = false;
                        if (habitat.Ruin.Type == RuinType.UnlockResearchProject)
                        {
                            if (!empire.ResourceMap.CheckResourcesKnown(habitat))
                            {
                                flag = true;
                            }
                        }
                        else if (CheckRuinsHaveBenefit(habitat.Ruin, empire))
                        {
                            flag = true;
                        }
                        if (flag)
                        {
                            return habitat;
                        }
                    }
                }
                GalaxyLocationList galaxyLocationList = DetermineGalaxyLocationsInRangeAtPoint(explorationShip.NearestSystemStar.Xpos, explorationShip.NearestSystemStar.Ypos, (double)MaxSolarSystemSize * 2.1, GalaxyLocationType.Undefined);
                for (int j = 0; j < galaxyLocationList.Count; j++)
                {
                    GalaxyLocation galaxyLocation = galaxyLocationList[j];
                    if ((galaxyLocation.Type == GalaxyLocationType.DebrisField || galaxyLocation.Type == GalaxyLocationType.PlanetDestroyer || galaxyLocation.Type == GalaxyLocationType.RestrictedArea) && !empire.KnownGalaxyLocations.Contains(galaxyLocation))
                    {
                        location = new Point((int)((double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0), (int)((double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0));
                        return null;
                    }
                }
                if (!empire.Reclusive)
                {
                    BuiltObject builtObject = FindUnownedBuiltObjectInSystem(explorationShip.NearestSystemStar);
                    if (builtObject != null)
                    {
                        location = new Point((int)builtObject.Xpos, (int)builtObject.Ypos);
                        return null;
                    }
                }
            }
            Habitat habitat2 = UltraFastFindNearestUnexploredSystem(explorationShip.Xpos, explorationShip.Ypos, empire);
            bool flag2 = false;
            if (habitat2 != null)
            {
                double num = CalculateDistance(habitat2.Xpos, habitat2.Ypos, explorationShip.Xpos, explorationShip.Ypos);
                if (num > (double)SectorSize * 0.6 || Rnd.Next(0, 20) == 1)
                {
                    flag2 = true;
                }
            }
            if (flag2)
            {
                habitat2 = UltraFastFindNearestUnexploredSystem(empire.Capital.Xpos, empire.Capital.Ypos, empire);
            }
            return habitat2;
        }

        public Habitat FindUnexploredRuinsOrLocations(double x, double y, Empire empire, out GalaxyLocation location)
        {
            location = null;
            Habitat habitat = null;
            double num = double.MaxValue;
            if (!empire.Reclusive)
            {
                for (int i = 0; i < RuinsHabitats.Count; i++)
                {
                    Habitat habitat2 = RuinsHabitats[i];
                    if (habitat2 == null || habitat2.Ruin == null)
                    {
                        continue;
                    }
                    bool flag = false;
                    if (habitat2.Ruin.Type == RuinType.UnlockResearchProject)
                    {
                        if (habitat2.Ruin.ResearchProjectId >= 0 && empire.Research != null && empire.Research.TechTree != null && empire.Research.TechTree.Count > habitat2.Ruin.ResearchProjectId && empire.Research.TechTree[habitat2.Ruin.ResearchProjectId] != null && !empire.Research.TechTree[habitat2.Ruin.ResearchProjectId].IsEnabled)
                        {
                            flag = true;
                        }
                    }
                    else if (CheckRuinsHaveBenefit(habitat2.Ruin, empire) && (habitat2.Ruin.StoryClueLevel < 0 || (habitat2.Ruin.StoryClueLevel >= 0 && empire == PlayerEmpire)))
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        double num2 = CalculateDistanceSquared(x, y, habitat2.Xpos, habitat2.Ypos);
                        if (num2 < num)
                        {
                            habitat = habitat2;
                            num = num2;
                        }
                    }
                }
                if (habitat != null)
                {
                    return habitat;
                }
            }
            for (int j = 0; j < _GalaxyLocations.Count; j++)
            {
                GalaxyLocation galaxyLocation = _GalaxyLocations[j];
                if ((galaxyLocation.Type != GalaxyLocationType.DebrisField && galaxyLocation.Type != GalaxyLocationType.PlanetDestroyer && galaxyLocation.Type != GalaxyLocationType.RestrictedArea) || empire.KnownGalaxyLocations.Contains(galaxyLocation))
                {
                    continue;
                }
                galaxyLocation.ResolveLocationCenter(out var x2, out var y2);
                Habitat habitat3 = FastFindNearestSystem(x, y);
                if (habitat3 != null)
                {
                    double num3 = CalculateDistance(habitat3.Xpos, habitat3.Ypos, x2, y2);
                    if (num3 < 25000.0)
                    {
                        location = galaxyLocation;
                        return null;
                    }
                }
            }
            return null;
        }

        public Habitat FindNextHabitatToExplore(double x, double y, Empire empire, BuiltObject explorationShip, out Point location)
        {
            location = Point.Empty;
            double num = 0.0;
            if (empire == null || explorationShip == null)
            {
                return null;
            }
            Habitat habitat = null;
            if (explorationShip.NearestSystemStar != null)
            {
                habitat = FindNearestUnexploredHabitatInSystem(x, y, explorationShip.NearestSystemStar, empire, 3.0);
            }
            if (habitat == null && explorationShip.NearestSystemStar != null)
            {
                GalaxyLocationList galaxyLocationList = DetermineGalaxyLocationsInRangeAtPoint(explorationShip.NearestSystemStar.Xpos, explorationShip.NearestSystemStar.Ypos, (double)MaxSolarSystemSize * 2.1, GalaxyLocationType.Undefined);
                for (int i = 0; i < galaxyLocationList.Count; i++)
                {
                    GalaxyLocation galaxyLocation = galaxyLocationList[i];
                    if ((galaxyLocation.Type == GalaxyLocationType.DebrisField || galaxyLocation.Type == GalaxyLocationType.PlanetDestroyer || galaxyLocation.Type == GalaxyLocationType.RestrictedArea) && !empire.KnownGalaxyLocations.Contains(galaxyLocation))
                    {
                        location = new Point((int)((double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0), (int)((double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0));
                        return null;
                    }
                }
                if (!empire.Reclusive)
                {
                    BuiltObject builtObject = FindUnownedBuiltObjectInSystem(explorationShip.NearestSystemStar);
                    if (builtObject != null)
                    {
                        location = new Point((int)builtObject.Xpos, (int)builtObject.Ypos);
                        return null;
                    }
                }
            }
            if (habitat == null)
            {
                habitat = FastFindNearestUnexploredHabitat(x, y, empire);
            }
            if (habitat == null)
            {
                return null;
            }
            double num2 = CalculateDistance(explorationShip.Xpos, explorationShip.Ypos, habitat.Xpos, habitat.Ypos);
            if (num2 > (double)MaxSolarSystemSize * 2.1)
            {
                x += Rnd.NextDouble() * 400000.0 - 200000.0;
                y += Rnd.NextDouble() * 400000.0 - 200000.0;
                habitat = FastFindNearestUnexploredHabitat(x, y, empire);
            }
            if (habitat != null && empire.BuiltObjects != null)
            {
                double range = (double)MaxSolarSystemSize * 2.1 * 2.0;
                if (explorationShip.WarpSpeed <= 0)
                {
                    range = 2000.0;
                }
                HabitatList habitatList = new HabitatList();
                for (int j = 0; j < empire.BuiltObjects.Count; j++)
                {
                    BuiltObject builtObject2 = empire.BuiltObjects[j];
                    if (builtObject2 != explorationShip && builtObject2 != null && builtObject2.Role == BuiltObjectRole.Exploration && builtObject2.Mission != null && (builtObject2.Mission.Type == BuiltObjectMissionType.Explore || builtObject2.Mission.Type == BuiltObjectMissionType.Move) && builtObject2.Mission.TargetHabitat != null && !habitatList.Contains(builtObject2.Mission.TargetHabitat))
                    {
                        habitatList.Add(builtObject2.Mission.TargetHabitat);
                    }
                }
                int num3 = 0;
                Habitat firstHabitatWithinRange = habitatList.GetFirstHabitatWithinRange(habitat.Xpos, habitat.Ypos, range);
                while (firstHabitatWithinRange != null && num3 < 30)
                {
                    double num4 = DetermineAngle(firstHabitatWithinRange.Xpos, firstHabitatWithinRange.Ypos, habitat.Xpos, habitat.Ypos);
                    double num5 = Math.PI * 2.0 / 5.0;
                    if (Rnd.Next(0, 2) == 1)
                    {
                        num5 *= -1.0;
                    }
                    double num6 = num4 + num5 + (0.5 - Rnd.NextDouble() * 1.0);
                    if (explorationShip.WarpSpeed <= 0)
                    {
                        num = ((!(num <= 0.0)) ? (num + 5000.0) : 5000.0);
                        num = Math.Min(num, 50000.0);
                    }
                    else
                    {
                        num += 1000000.0;
                    }
                    x += Math.Cos(num6) * num;
                    y += Math.Sin(num6) * num;
                    habitat = FastFindNearestUnexploredHabitat(x, y, empire);
                    if (habitat == null)
                    {
                        break;
                    }
                    firstHabitatWithinRange = habitatList.GetFirstHabitatWithinRange(habitat.Xpos, habitat.Ypos, range);
                    num3++;
                }
                if (firstHabitatWithinRange != null)
                {
                    return null;
                }
            }
            return habitat;
        }

        public Habitat FastFindNearestUnexploredHabitatInSector(double x, double y, Empire empire, Sector sector)
        {
            HabitatList habitatList = new HabitatList();
            List<double> list = new List<double>();
            double num = double.MaxValue;
            for (int i = 0; i < Systems.Count; i++)
            {
                SystemInfo systemInfo = Systems[i];
                Sector sector2 = ResolveSector(systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
                if (sector.X != sector2.X || sector.Y != sector2.Y)
                {
                    continue;
                }
                SystemVisibility systemVisibility = empire.SystemVisibility[systemInfo.SystemStar.SystemIndex];
                if (systemVisibility.Status == SystemVisibilityStatus.Unexplored || systemVisibility.Status == SystemVisibilityStatus.Undefined)
                {
                    double num2 = CalculateDistanceSquared(x, y, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
                    if (num2 < num)
                    {
                        num = num2;
                        list.Add(num2);
                        habitatList.Add(systemInfo.SystemStar);
                    }
                }
                else if (!systemVisibility.TotallyExplored)
                {
                    double item = CalculateDistanceSquared(x, y, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
                    list.Add(item);
                    habitatList.Add(systemInfo.SystemStar);
                }
            }
            Habitat[] array = habitatList.ToArray();
            double[] keys = list.ToArray();
            Array.Sort(keys, array);
            for (int j = 0; j < array.Length; j++)
            {
                Habitat habitat = null;
                double num3 = double.MaxValue;
                if (Systems[array[j].SystemIndex].SystemStar.Category == HabitatCategoryType.Star && Systems[array[j].SystemIndex].Habitats.Count == 0)
                {
                    SystemVisibilityStatus status = empire.SystemVisibility[array[j].SystemIndex].Status;
                    if (status == SystemVisibilityStatus.Unexplored || status == SystemVisibilityStatus.Undefined)
                    {
                        Habitat systemStar = Systems[array[j].SystemIndex].SystemStar;
                        double num4 = CalculateDistanceSquared(x, y, systemStar.Xpos, systemStar.Ypos);
                        if (num4 < num3)
                        {
                            habitat = systemStar;
                            num3 = num4;
                        }
                    }
                    else
                    {
                        empire.SystemVisibility[array[j].SystemIndex].TotallyExplored = true;
                    }
                }
                else if (Systems[array[j].SystemIndex].SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    SystemVisibilityStatus status2 = empire.SystemVisibility[array[j].SystemIndex].Status;
                    if (status2 == SystemVisibilityStatus.Unexplored || status2 == SystemVisibilityStatus.Undefined)
                    {
                        Habitat systemStar2 = Systems[array[j].SystemIndex].SystemStar;
                        double num5 = CalculateDistanceSquared(x, y, systemStar2.Xpos, systemStar2.Ypos);
                        if (num5 < num3)
                        {
                            habitat = systemStar2;
                            num3 = num5;
                        }
                    }
                    else
                    {
                        empire.SystemVisibility[array[j].SystemIndex].TotallyExplored = true;
                    }
                }
                else
                {
                    bool flag = false;
                    for (int k = 0; k < Systems[array[j].SystemIndex].Habitats.Count; k++)
                    {
                        if ((empire.ResourceMap != null && !empire.ResourceMap.CheckResourcesKnown(Systems[array[j].SystemIndex].Habitats[k])) || CheckRuinsHaveBenefit(Systems[array[j].SystemIndex].Habitats[k].Ruin, empire))
                        {
                            flag = true;
                            Habitat habitat2 = Systems[array[j].SystemIndex].Habitats[k];
                            double num6 = CalculateDistanceSquared(x, y, habitat2.Xpos, habitat2.Ypos);
                            if (num6 < num3)
                            {
                                habitat = habitat2;
                                num3 = num6;
                            }
                        }
                    }
                    if (!flag)
                    {
                        empire.SystemVisibility[array[j].SystemIndex].TotallyExplored = true;
                    }
                }
                if (habitat != null)
                {
                    return habitat;
                }
            }
            return null;
        }

        public Habitat FastFindNearestUnexploredHabitat(double x, double y, Empire empire)
        {
            if (empire != null)
            {
                HabitatList habitatList = new HabitatList();
                List<double> list = new List<double>();
                double num = double.MaxValue;
                for (int i = 0; i < Systems.Count; i++)
                {
                    SystemInfo systemInfo = Systems[i];
                    if (systemInfo == null || systemInfo.SystemStar == null)
                    {
                        continue;
                    }
                    SystemVisibility systemVisibility = empire.SystemVisibility[systemInfo.SystemStar.SystemIndex];
                    if (systemVisibility == null)
                    {
                        continue;
                    }
                    if (systemVisibility.Status == SystemVisibilityStatus.Unexplored || systemVisibility.Status == SystemVisibilityStatus.Undefined)
                    {
                        double num2 = CalculateDistanceSquared(x, y, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
                        if (num2 < num)
                        {
                            num = num2;
                            list.Add(num2);
                            habitatList.Add(systemInfo.SystemStar);
                        }
                    }
                    else if (!systemVisibility.TotallyExplored)
                    {
                        double item = CalculateDistanceSquared(x, y, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
                        list.Add(item);
                        habitatList.Add(systemInfo.SystemStar);
                    }
                }
                Habitat[] array = habitatList.ToArray();
                double[] keys = list.ToArray();
                Array.Sort(keys, array);
                for (int j = 0; j < array.Length; j++)
                {
                    Habitat habitat = null;
                    double num3 = double.MaxValue;
                    Habitat habitat2 = array[j];
                    if (habitat2 == null)
                    {
                        continue;
                    }
                    SystemInfo systemInfo2 = Systems[habitat2.SystemIndex];
                    if (systemInfo2 == null || systemInfo2.SystemStar == null || systemInfo2.Habitats == null)
                    {
                        continue;
                    }
                    if (systemInfo2.SystemStar.Category == HabitatCategoryType.Star && systemInfo2.Habitats.Count == 0)
                    {
                        SystemVisibilityStatus status = empire.SystemVisibility[habitat2.SystemIndex].Status;
                        if (status == SystemVisibilityStatus.Unexplored || status == SystemVisibilityStatus.Undefined)
                        {
                            Habitat systemStar = systemInfo2.SystemStar;
                            double num4 = CalculateDistanceSquared(x, y, systemStar.Xpos, systemStar.Ypos);
                            if (num4 < num3)
                            {
                                habitat = systemStar;
                                num3 = num4;
                            }
                        }
                        else
                        {
                            empire.SystemVisibility[habitat2.SystemIndex].TotallyExplored = true;
                        }
                    }
                    else if (systemInfo2.SystemStar.Category == HabitatCategoryType.GasCloud)
                    {
                        SystemVisibilityStatus status2 = empire.SystemVisibility[habitat2.SystemIndex].Status;
                        if (status2 == SystemVisibilityStatus.Unexplored || status2 == SystemVisibilityStatus.Undefined)
                        {
                            Habitat systemStar2 = systemInfo2.SystemStar;
                            double num5 = CalculateDistanceSquared(x, y, systemStar2.Xpos, systemStar2.Ypos);
                            if (num5 < num3)
                            {
                                habitat = systemStar2;
                                num3 = num5;
                            }
                        }
                        else
                        {
                            empire.SystemVisibility[habitat2.SystemIndex].TotallyExplored = true;
                        }
                    }
                    else
                    {
                        bool flag = false;
                        for (int k = 0; k < systemInfo2.Habitats.Count; k++)
                        {
                            Habitat habitat3 = systemInfo2.Habitats[k];
                            if (habitat3 != null && ((empire.ResourceMap != null && !empire.ResourceMap.CheckResourcesKnown(habitat3) && habitat3.Ruin == null) || (!empire.Reclusive && habitat3.Ruin != null && ((habitat3.Ruin.Type == RuinType.UnlockResearchProject && !empire.ResourceMap.CheckResourcesKnown(habitat3)) || (habitat3.Ruin.Type != RuinType.UnlockResearchProject && CheckRuinsHaveBenefit(habitat3.Ruin, empire))))))
                            {
                                flag = true;
                                Habitat habitat4 = habitat3;
                                double num6 = CalculateDistanceSquared(x, y, habitat4.Xpos, habitat4.Ypos);
                                if (num6 < num3)
                                {
                                    habitat = habitat4;
                                    num3 = num6;
                                }
                            }
                        }
                        if (!flag)
                        {
                            empire.SystemVisibility[habitat2.SystemIndex].TotallyExplored = true;
                        }
                    }
                    if (habitat != null)
                    {
                        return habitat;
                    }
                }
            }
            return null;
        }

        public Habitat FindNearestUnexploredSystem(double x, double y, Empire empire)
        {
            double num = double.MaxValue;
            Habitat result = null;
            for (int i = 0; i < empire.SystemVisibility.Count; i++)
            {
                SystemVisibility systemVisibility = empire.SystemVisibility[i];
                if (!empire.CheckSystemExplored(systemVisibility.SystemStar.SystemIndex))
                {
                    double num2 = CalculateDistanceSquared(x, y, systemVisibility.SystemStar.Xpos, systemVisibility.SystemStar.Ypos);
                    if (num2 < num)
                    {
                        result = systemVisibility.SystemStar;
                        num = num2;
                    }
                }
            }
            return result;
        }

        public Habitat FindNearestUnexploredHabitat(double x, double y, Empire empire, bool includeAsteroids)
        {
            return FindNearestUnexploredHabitat((int)x, (int)y, empire, includeAsteroids);
        }

        public Habitat FindNearestUnexploredHabitat(int x, int y, Empire empire, bool includeAsteroids)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex(x, y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, x, y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    Habitat habitat = FindNearestUnexploredHabitatInIndex(x, y, index, out distance, empire, includeAsteroids);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private Habitat FindNearestUnexploredHabitatInIndex(int x, int y, GalaxyIndex index, out double distance, Empire empire, bool includeAsteroids)
        {
            Habitat habitat = null;
            distance = double.MaxValue;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            for (int i = 0; i < habitatList.Count; i++)
            {
                Habitat habitat2 = habitatList[i];
                if (includeAsteroids || habitat2.Category != HabitatCategoryType.Asteroid)
                {
                    double num = CalculateDistanceSquared(x, y, habitat2.Xpos, habitat2.Ypos);
                    if (num < distance && empire.ResourceMap != null && !empire.ResourceMap.CheckResourcesKnown(habitat2))
                    {
                        habitat = habitat2;
                        distance = num;
                    }
                }
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        public Habitat FindNearestUnexploredHabitatInSystem(int x, int y, Habitat sun, Empire empire, bool includeAsteroids)
        {
            Habitat habitat = null;
            int x2 = (int)(sun.Xpos / (double)IndexSize);
            int y2 = (int)(sun.Ypos / (double)IndexSize);
            CorrectIndexCoords(ref x2, ref y2);
            HabitatList habitatList = HabitatIndex[x2][y2];
            int num = habitatList.IndexOf(sun);
            num++;
            Habitat habitat2 = null;
            if (num < habitatList.Count)
            {
                habitat2 = habitatList[num];
            }
            int iterationCount = 0;
            while (ConditionCheckLimit(habitat2 != null && habitat2.Parent != null, 2000, ref iterationCount))
            {
                bool flag = true;
                if (!includeAsteroids && habitat2.Category == HabitatCategoryType.Asteroid)
                {
                    flag = false;
                }
                if (flag)
                {
                    if (habitat != null)
                    {
                        int num2 = (int)CalculateDistance(x, y, habitat2.Xpos, habitat2.Ypos);
                        int num3 = (int)CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
                        if (num2 < num3 && empire.ResourceMap != null && !empire.ResourceMap.CheckResourcesKnown(habitat2))
                        {
                            habitat = habitat2;
                        }
                    }
                    else if (empire.ResourceMap != null && !empire.ResourceMap.CheckResourcesKnown(habitat2))
                    {
                        habitat = habitat2;
                    }
                }
                num++;
                habitat2 = ((num >= habitatList.Count) ? null : habitatList[num]);
            }
            return habitat;
        }

        public HabitatList DetermineHabitatsInSystem(Habitat systemStar)
        {
            HabitatList habitatList = new HabitatList();
            int num = Habitats.IndexOf(systemStar);
            int count = Habitats.Count;
            if (num >= 0 && num < Habitats.Count + 1)
            {
                for (int i = num + 1; i < count && Habitats[i].Parent != null; i++)
                {
                    habitatList.Add(Habitats[i]);
                }
            }
            return habitatList;
        }

        public StellarObject[] SortStellarObjectsByDistanceThreadsafe(double x, double y, StellarObjectList stellarObjects)
        {
            StellarObject[] array = ListHelper.ToArrayThreadSafe(stellarObjects);
            int num = array.Length;
            double[] array2 = new double[num];
            for (int i = 0; i < num; i++)
            {
                array2[i] = CalculateDistanceSquared(x, y, array[i].Xpos, array[i].Ypos);
            }
            Array.Sort(array2, array);
            return array;
        }

        public BuiltObject[] SortBuiltObjectsByDistanceThreadsafe(double x, double y, BuiltObjectList builtObjects)
        {
            BuiltObject[] array = ListHelper.ToArrayThreadSafe(builtObjects);
            double[] array2 = new double[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array2[i] = CalculateDistanceSquared(x, y, array[i].Xpos, array[i].Ypos);
            }
            Array.Sort(array2, array);
            return array;
        }

        public Habitat[] SortHabitatsByDistanceThreadsafe(double x, double y, HabitatList habitats)
        {
            Habitat[] array = habitats.ToArray();
            double[] array2 = new double[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array2[i] = CalculateDistanceSquared(x, y, array[i].Xpos, array[i].Ypos);
            }
            Array.Sort(array2, array);
            return array;
        }

        public int DetermineDefendingStrength(ShipGroup fleet, Empire empire)
        {
            int num = 0;
            if (fleet != null && fleet.LeadShip != null && fleet.LeadShip.NearestSystemStar != null)
            {
                BuiltObjectList ships = null;
                num += DetermineShipStrengthInSystem(fleet.LeadShip.NearestSystemStar, empire, out ships);
            }
            return num;
        }

        public int DetermineDefendingStrength(BuiltObject builtObject, Empire empire)
        {
            int num = 0;
            if (builtObject != null)
            {
                if (builtObject.NearestSystemStar != null)
                {
                    BuiltObjectList ships = null;
                    num += DetermineShipStrengthInSystem(builtObject.NearestSystemStar, builtObject.Xpos, builtObject.Ypos, empire, out ships);
                }
                if (builtObject.ParentHabitat != null)
                {
                    num += DetermineBaseStrengthAtHabitat(builtObject.ParentHabitat, empire);
                }
                else if (builtObject.Role == BuiltObjectRole.Base)
                {
                    num += builtObject.CalculateOverallStrengthFactor();
                }
            }
            return num;
        }

        public int DetermineDefendingFirepower(Habitat habitat, Empire empire)
        {
            int num = 0;
            if (habitat != null)
            {
                BuiltObjectList ships = null;
                num += DetermineShipFirepowerNearHabitat(habitat, empire, out ships);
                num += DetermineBaseFirepowerAtHabitat(habitat, empire);
            }
            return num;
        }

        public int DetermineBaseFirepowerAtHabitat(Habitat habitat, Empire empire)
        {
            int num = 0;
            if (habitat.BasesAtHabitat != null && habitat.BasesAtHabitat.Count > 0)
            {
                for (int i = 0; i < habitat.BasesAtHabitat.Count; i++)
                {
                    num += habitat.BasesAtHabitat[i].FirepowerRaw;
                }
            }
            return num;
        }

        private int DetermineShipFirepowerNearHabitat(Habitat habitat, Empire empire, out BuiltObjectList ships)
        {
            int num = 0;
            ships = new BuiltObjectList();
            float num2 = 4000000f;
            Habitat habitat2 = DetermineHabitatSystemStar(habitat);
            if (empire != null && empire.BuiltObjects != null)
            {
                for (int i = 0; i < empire.BuiltObjects.Count; i++)
                {
                    if (empire.BuiltObjects[i].NearestSystemStar == habitat2 && empire.BuiltObjects[i].Role != BuiltObjectRole.Base && empire.BuiltObjects[i].BuiltAt == null)
                    {
                        BuiltObject builtObject = empire.BuiltObjects[i];
                        if (builtObject.ParentHabitat == habitat || builtObject.AttackRangeSquared > num2)
                        {
                            num += builtObject.FirepowerRaw;
                            ships.Add(builtObject);
                        }
                    }
                }
            }
            return num;
        }

        public int DetermineDefendingStrength(Habitat habitat, Empire empire)
        {
            int num = 0;
            if (habitat != null)
            {
                BuiltObjectList ships = null;
                num += DetermineShipStrengthNearHabitat(habitat, empire, out ships);
                num += DetermineBaseStrengthAtHabitat(habitat, empire);
            }
            return num;
        }

        public int DetermineBaseStrengthAtHabitat(Habitat habitat, Empire empire)
        {
            int num = 0;
            if (habitat != null && habitat.BasesAtHabitat != null && habitat.BasesAtHabitat.Count > 0)
            {
                for (int i = 0; i < habitat.BasesAtHabitat.Count; i++)
                {
                    BuiltObject builtObject = habitat.BasesAtHabitat[i];
                    if (builtObject != null)
                    {
                        num += builtObject.CalculateOverallStrengthFactor();
                    }
                }
            }
            return num;
        }

    }
}
