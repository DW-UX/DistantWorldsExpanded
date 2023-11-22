using BaconDistantWorlds;
using DistantWorlds.Controls;
using DistantWorlds.Types;
using ExpansionMod;
using Ionic.Zlib;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;
//using WMPLib;
using static System.Windows.Forms.AxHost;
using EmpireList = DistantWorlds.Types.EmpireList;
using LinearGradientMode = DistantWorlds.Controls.LinearGradientMode;

namespace DistantWorlds
{
    public partial class Start
    {
        private void method_76(Galaxy galaxy_0, Empire empire_0)
        {
            DateTime currentDateTime = galaxy_0.CurrentDateTime;
            long currentStarDate = galaxy_0.CurrentStarDate;
            for (int i = 0; i < empire_0.Colonies.Count; i++)
            {
                empire_0.Colonies[i].DoTasks(currentDateTime);
            }
            for (int j = 0; j < empire_0.BuiltObjects.Count; j++)
            {
                empire_0.BuiltObjects[j].DoTasks(currentDateTime, currentStarDate);
            }
            for (int k = 0; k < empire_0.PrivateBuiltObjects.Count; k++)
            {
                empire_0.PrivateBuiltObjects[k].DoTasks(currentDateTime, currentStarDate);
            }
        }

        private void method_77(Game game_1)
        {
            method_76(game_1.Galaxy, game_1.PlayerEmpire);
            main_0.Ignite(game_1, string.Empty);
            main_0.method_56(Application.StartupPath + "\\images\\", game_1.CustomizationSetName, game_1.PlayerEmpire.DominantRace);
            Cursor.Current = Cursors.Default;
            method_9();
            method_46();
            method_25();
            main_0.Location = base.Location;
            main_0.Visible = true;
            main_0.ProcessMain(game_1.Galaxy.CurrentDateTime, game_1.Galaxy.CurrentStarDate, null);
            main_0.mainView.Refresh();
            base.Enabled = true;
            Hide();
            if (!main_0.bool_7)
            {
                main_0.bool_8 = true;
            }
            main_0.Launch(launchFromLoad: false);
            Galaxy.SetResearchRaceSpecialProjects(raceList_1);
            Galaxy.SetResearchComponentMaxTechPoints(game_1.Galaxy.BaseTechCost);
            method_3(main_0.string_3);
            method_1(main_0.string_3);
            base.Location = main_0.Location;
            Show();
            main_0.Visible = false;
            game_0 = null;
        }

        private Galaxy method_78(Galaxy galaxy_0, ResourceSystem resourceSystem_1, int int_1, string string_2, string string_3, int int_2, double double_1, int int_3, double double_2, double double_3, int int_4, double double_4, double double_5, int int_5, double double_6, int int_6, bool bool_5, EmpireStartList empireStartList_0)
        {
            galaxy_0.ApplicationStartupPath = string_2;
            if (!string.IsNullOrEmpty(string_3))
            {
                galaxy_0.CustomizationSetPath = string_2 + "\\Customization\\" + string_3 + "\\";
            }
            galaxy_0.RandomSeed = int_2;
            galaxy_0.AssignGalaxyStaticDataToInstance();
            galaxy_0.ResourceSystem.Clear();
            galaxy_0.ResourceSystem.Initialize(resourceSystem_1);
            galaxy_0.DifficultyLevel = int_1;
            galaxy_0.Races = Galaxy.LoadRaces(string_2, string_3);
            galaxy_0.LoadAgentNames(string_2, string_3);
            Galaxy.SetRaceStartupCharacters(string_2, string_3, galaxy_0.Races);
            Galaxy.SetResearchCosts((int)double_1, Galaxy.ResearchNodeDefinitionsStatic);
            Galaxy.SetHyperDriveSpeeds(resourceSystem_1, double_5, (int)double_1, string_2, string_3);
            Galaxy.SetResearchRaceSpecialProjects(galaxy_0.Races);
            Galaxy.SetResearchComponentMaxTechPoints((int)double_1);
            galaxy_0.LifePrevalence = int_3;
            galaxy_0.CreaturePrevalence = double_2;
            galaxy_0.PiratePrevalence = double_3;
            galaxy_0.PirateProximity = int_4;
            galaxy_0.ColonyPrevalence = double_4;
            galaxy_0.ResearchSpeedModifier = 1.0;
            galaxy_0.BaseTechCost = (int)double_1;
            galaxy_0.HyperdriveSpeedMultiplier = double_5;
            galaxy_0.Age = int_5;
            galaxy_0.AggressionLevel = double_6;
            galaxy_0.MaximumEmpireAmount = int_6;
            galaxy_0.SpawnNewEmpires = bool_5;
            galaxy_0.LoadDesignNames(string_2, string_3);
            galaxy_0.RebuildIndexes();
            empireStartList_0.Update(galaxy_0.Races);
            int aggressiveRacesRequired = 0;
            if (double_6 >= 1.5)
            {
                aggressiveRacesRequired = 3;
            }
            else if (double_6 >= 1.3)
            {
                aggressiveRacesRequired = 2;
            }
            else if (double_6 >= 1.1)
            {
                aggressiveRacesRequired = 1;
            }
            galaxy_0.SetupAlienRacePopulations(empireStartList_0, aggressiveRacesRequired);
            galaxy_0.LifePrevalenceMultiplier = 0.8 * (Math.Sqrt(1400.0) / Math.Sqrt(galaxy_0.StarCount));
            galaxy_0.UpdateSystemInfo(null);
            galaxy_0.Orders.EnableIndexing();
            galaxy_0.GameSummary = galaxy_0.DetermineGameSummary();
            return galaxy_0;
        }

        private Galaxy method_79(Galaxy galaxy_0)
        {
            for (int i = 0; i < galaxy_0.Habitats.Count; i++)
            {
                Habitat habitat = galaxy_0.Habitats[i];
                if (habitat != null)
                {
                    Habitat sun = Galaxy.DetermineHabitatSystemStar(habitat);
                    galaxy_0.SelectPopulation(habitat, sun);
                }
            }
            return galaxy_0;
        }

        private Galaxy method_80(Galaxy galaxy_0, bool bool_5, bool bool_6, bool bool_7, bool bool_8, bool bool_9)
        {
            Empire[] array = galaxy_0.Empires.ToArray();
            foreach (Empire empire in array)
            {
                if (empire == null || empire == galaxy_0.IndependentEmpire)
                {
                    continue;
                }
                if (empire.Colonies != null)
                {
                    Habitat[] array2 = empire.Colonies.ToArray();
                    foreach (Habitat habitat in array2)
                    {
                        if (habitat != null && habitat.Owner == empire)
                        {
                            habitat.ClearColony(null, sendMessages: false, removeEmpireWhenNoColonies: false);
                        }
                    }
                }
                empire.CompleteTeardown(null, removeFromGalaxy: true, sendMessages: false);
            }
            Empire[] array3 = galaxy_0.PirateEmpires.ToArray();
            foreach (Empire empire2 in array3)
            {
                if (empire2 == null || empire2 == galaxy_0.IndependentEmpire)
                {
                    continue;
                }
                if (empire2.Colonies != null)
                {
                    Habitat[] array4 = empire2.Colonies.ToArray();
                    foreach (Habitat habitat2 in array4)
                    {
                        if (habitat2 != null && habitat2.Owner == empire2)
                        {
                            habitat2.ClearColony(null, sendMessages: false, removeEmpireWhenNoColonies: false);
                        }
                    }
                }
                empire2.CompleteTeardown(null, removeFromGalaxy: true, sendMessages: false);
            }
            galaxy_0.DefeatedEmpires.Clear();
            galaxy_0.ResetNextEmpireId();
            if (galaxy_0.IndependentEmpire != null)
            {
                if (galaxy_0.IndependentEmpire.Colonies != null)
                {
                    Habitat[] array5 = galaxy_0.IndependentEmpire.Colonies.ToArray();
                    foreach (Habitat habitat3 in array5)
                    {
                        if (habitat3 != null && habitat3.Owner == galaxy_0.IndependentEmpire)
                        {
                            habitat3.ClearColony(null, sendMessages: false, removeEmpireWhenNoColonies: false);
                        }
                    }
                }
                galaxy_0.IndependentColonies.Clear();
                galaxy_0.ClearRaceUsed();
                galaxy_0.IndependentCount = 0;
                BuiltObjectList builtObjectList = new BuiltObjectList();
                builtObjectList.AddRange(galaxy_0.IndependentEmpire.BuiltObjects);
                builtObjectList.AddRange(galaxy_0.IndependentEmpire.PrivateBuiltObjects);
                for (int n = 0; n < builtObjectList.Count; n++)
                {
                    builtObjectList[n]?.CompleteTeardown(galaxy_0, removeFromEmpire: true);
                }
                galaxy_0.IndependentEmpire.Research = new ResearchSystem();
                galaxy_0.IndependentEmpire.Research.TechTree = Galaxy.ResearchNodeDefinitionsStatic.ObtainTechTree(galaxy_0.IndependentEmpire.DominantRace);
                galaxy_0.IndependentEmpire.Research.TechTree = Galaxy.ResearchNodeDefinitionsStatic.SetTechTreeStartingDefaults(galaxy_0.IndependentEmpire.Research.TechTree, galaxy_0.IndependentEmpire.DominantRace, galaxy_0.IndependentEmpire.Policy);
                galaxy_0.IndependentEmpire.Research.Update(galaxy_0.IndependentEmpire.DominantRace);
                galaxy_0.IndependentEmpire.ReviewResearchAbilities();
                galaxy_0.IndependentEmpire.ReviewDesignsBuiltObjectsImprovedComponents();
                galaxy_0.IndependentEmpire.ReviewColonizationTypes();
                galaxy_0.IndependentEmpire.ReviewPopulationGrowthRates();
                int newSize = 0;
                galaxy_0.IndependentEmpire.ReviewMaximumConstructionSize(out newSize);
                galaxy_0.IndependentEmpire.ReviewCanBuildShipTypes();
                galaxy_0.IndependentEmpire.ReviewTroopTypes();
            }
            if (bool_5)
            {
                for (int num = 0; num < galaxy_0.Habitats.Count; num++)
                {
                    Habitat habitat4 = galaxy_0.Habitats[num];
                    if (habitat4 != null)
                    {
                        if (habitat4.Resources != null)
                        {
                            habitat4.Resources.Clear();
                        }
                        galaxy_0.SelectResources(habitat4);
                    }
                }
            }
            if (bool_6)
            {
                for (int num2 = 0; num2 < galaxy_0.Habitats.Count; num2++)
                {
                    Habitat habitat5 = galaxy_0.Habitats[num2];
                    if (habitat5 != null)
                    {
                        habitat5.ScenicFactor = 0f;
                        habitat5.ScenicFeature = string.Empty;
                        habitat5.ResearchBonus = 0;
                        habitat5.ResearchBonusIndustry = IndustryType.Undefined;
                        galaxy_0.SetScenicFactor(habitat5);
                        galaxy_0.SetResearchBonus(habitat5);
                    }
                }
            }
            if (bool_7)
            {
                for (int num3 = 0; num3 < galaxy_0.Creatures.Count; num3++)
                {
                    galaxy_0.Creatures[num3]?.CompleteTeardown();
                }
                for (int num4 = 0; num4 < galaxy_0.Habitats.Count; num4++)
                {
                    Habitat habitat6 = galaxy_0.Habitats[num4];
                    if (habitat6 != null)
                    {
                        galaxy_0.SelectCreatures(habitat6);
                    }
                }
                for (int num5 = 0; num5 < galaxy_0.Creatures.Count; num5++)
                {
                    Creature creature = galaxy_0.Creatures[num5];
                    if (creature != null && creature.ParentHabitat != null && !galaxy_0.Systems[creature.ParentHabitat.SystemIndex].Creatures.Contains(creature))
                    {
                        galaxy_0.Systems[creature.ParentHabitat.SystemIndex].Creatures.Add(creature);
                    }
                }
            }
            if (bool_8)
            {
                for (int num6 = 0; num6 < galaxy_0.RuinsHabitats.Count; num6++)
                {
                    Habitat habitat7 = galaxy_0.RuinsHabitats[num6];
                    if (habitat7 != null && habitat7.Ruin != null)
                    {
                        habitat7.Ruin = null;
                    }
                }
                galaxy_0.RuinsHabitats.Clear();
                galaxy_0.RuinCount = 0;
            }
            GalaxyLocationList galaxyLocationList = galaxy_0.GalaxyLocations.FindLocations(GalaxyLocationType.RaceRegion);
            for (int num7 = 0; num7 < galaxyLocationList.Count; num7++)
            {
                GalaxyLocation galaxyLocation = galaxyLocationList[num7];
                if (galaxyLocation != null)
                {
                    galaxy_0.GalaxyLocations.Remove(galaxyLocation);
                }
            }
            if (bool_9)
            {
                if (galaxy_0.StoryClueLocations != null && galaxy_0.StoryClueLocations.Count == 5)
                {
                    if (galaxy_0.StoryClueLocations[0] is Habitat)
                    {
                        Habitat habitat8 = (Habitat)galaxy_0.StoryClueLocations[0];
                        if (habitat8.Ruin != null)
                        {
                            habitat8.Ruin = null;
                            galaxy_0.RuinsHabitats.Remove(habitat8);
                            galaxy_0.RuinCount--;
                        }
                    }
                    if (galaxy_0.StoryClueLocations[1] is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)galaxy_0.StoryClueLocations[1];
                        builtObject.CompleteTeardown(galaxy_0, removeFromEmpire: true);
                    }
                    if (galaxy_0.StoryClueLocations[2] is BuiltObject)
                    {
                        BuiltObject builtObject2 = (BuiltObject)galaxy_0.StoryClueLocations[2];
                        builtObject2.CompleteTeardown(galaxy_0, removeFromEmpire: true);
                    }
                    if (galaxy_0.StoryClueLocations[3] is BuiltObject)
                    {
                        BuiltObject builtObject3 = (BuiltObject)galaxy_0.StoryClueLocations[3];
                        builtObject3.CompleteTeardown(galaxy_0, removeFromEmpire: true);
                    }
                    if (galaxy_0.StoryClueLocations[4] is BuiltObject)
                    {
                        BuiltObject builtObject4 = (BuiltObject)galaxy_0.StoryClueLocations[4];
                        builtObject4.CompleteTeardown(galaxy_0, removeFromEmpire: true);
                    }
                    galaxy_0.StoryClueLocations.Clear();
                    galaxy_0.StoryClueUsed.Clear();
                    galaxy_0.StorySecondaryClueUsed.Clear();
                }
                GalaxyLocationList galaxyLocationList2 = new GalaxyLocationList();
                for (int num8 = 0; num8 < galaxy_0.GalaxyLocations.Count; num8++)
                {
                    GalaxyLocation galaxyLocation2 = galaxy_0.GalaxyLocations[num8];
                    if (galaxyLocation2 == null)
                    {
                        continue;
                    }
                    GalaxyLocationType type = galaxyLocation2.Type;
                    if (type != GalaxyLocationType.DebrisField && type != GalaxyLocationType.PlanetDestroyer && type != GalaxyLocationType.RestrictedArea)
                    {
                        continue;
                    }
                    if (galaxyLocation2.RelatedBuiltObject != null)
                    {
                        galaxyLocation2.RelatedBuiltObject.CompleteTeardown(galaxy_0, removeFromEmpire: true);
                    }
                    if (galaxyLocation2.RelatedCreatures != null)
                    {
                        Creature[] array6 = galaxyLocation2.RelatedCreatures.ToArray();
                        for (int num9 = 0; num9 < array6.Length; num9++)
                        {
                            array6[num9].CompleteTeardown();
                        }
                    }
                    galaxyLocation2.RelatedRace = null;
                    galaxyLocationList2.Add(galaxyLocation2);
                }
                for (int num10 = 0; num10 < galaxyLocationList2.Count; num10++)
                {
                    galaxy_0.GalaxyLocations.Remove(galaxyLocationList2[num10]);
                }
            }
            galaxy_0.RebuildGalaxyLocationIndexes();
            return galaxy_0;
        }

        private Game method_81(string string_2, bool bool_5)
        {
            Game result = null;
            try
            {
                if (File.Exists(string_2))
                {
                    List<object> list = new List<object>();
                    using (FileStream stream_ = new FileStream(string_2, FileMode.Open, FileAccess.Read))
                    {
                        list = LoadFromFile(string_2, stream_, bool_5);
                    }
                    if (list != null)
                    {
                        List<object> list2 = list;
                        if (list2[0] is string)
                        {
                            _ = (string)list2[0];
                        }
                        if (list2[1] is Game)
                        {
                            result = (Game)list2[1];
                            return result;
                        }
                        return result;
                    }
                    return result;
                }
                return result;
            }
            catch (SerializationException)
            {
                string text = TextResolver.GetText("This is not a valid Distant Worlds game file");
                MessageBox.Show(text, TextResolver.GetText("Cannot load file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                bool_0 = true;
                method_12();
                return result;
            }
            catch (OutOfMemoryException)
            {
                string text2 = "There was not enough memory to load this Distant Worlds game. Please close all other open applications and try again.";
                text2 = text2 + "\n\nWorking Set: " + Environment.WorkingSet + " bytes";
                MessageBox.Show(text2, "Cannot load file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                bool_0 = true;
                method_12();
                return result;
            }
            catch (Exception ex3)
            {
                string text3 = "Distant Worlds could not load this game.";
                text3 += "\n\nError:\n\n";
                text3 += ex3.ToString();
                text3 = text3 + "\n\nWorking Set: " + Environment.WorkingSet + " bytes";
                MessageBox.Show(text3, "Cannot load file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                bool_0 = true;
                method_12();
                return result;
            }
        }

        private Game CreateGameFromSettings(GalaxyShape galaxyShape_0, int int_1, int int_2, bool bool_5, double double_1, int int_3, double double_2, double double_3, int int_4, double double_4, double double_5, int int_5, double double_6, EmpireStart empireStart_0, EmpireStartList empireStartList_0, VictoryConditions victoryConditions_0, EmpireVictoryConditions empireVictoryConditions_0, EmpireVictoryConditions empireVictoryConditions_1, bool bool_6, bool bool_7, GameStartResets gameStartResets_0)
        {
            try
            {
                long startStarDate = Galaxy.StartStarDate;
                startStarDate += int_5 * 30000000;
                int num = (int)DateTime.Now.Ticks;
                string empty = string.Empty;
                empty = main_0.string_3;
                List<Bitmap> list = new List<Bitmap>();
                list = ((!bool_2) ? Galaxy.FlagShapes : Galaxy.FlagShapesPirates);
                RaceList raceList = Galaxy.LoadRaces(Application.StartupPath, empty);
                raceList = raceList.ResolveNormalEmpireRaces();
                EmpireStartList empireStartList = new EmpireStartList();
                empireStartList.Add(empireStart_0);
                empireStartList.AddRange(empireStartList_0);
                empireStartList.Update(raceList);
                double difficultyLevel = empireStart_0.DifficultyLevel;
                Galaxy galaxy = null;
                bool flag = false;
                if (gameStartResets_0 != null && !string.IsNullOrEmpty(gameStartResets_0.GalaxyFilepath))
                {
                    bool bool_8 = true;
                    if (gameStartResets_0.ResetResources)
                    {
                        bool_8 = false;
                    }
                    Game game = method_81(gameStartResets_0.GalaxyFilepath, bool_8);
                    if (game == null || game.Galaxy == null)
                    {
                        return null;
                    }
                    galaxy = game.Galaxy;
                    Galaxy.SetGalaxyPhysicalDimensions(galaxy.SectorWidth, galaxy.SectorHeight);
                    galaxy = method_78(galaxy, resourceSystem_0, (int)difficultyLevel, Application.StartupPath, empty, num, double_4, int_3, double_2, double_3, int_4, double_1, double_5, int_5, double_6, int_2, bool_5, empireStartList);
                    galaxy = method_80(galaxy, gameStartResets_0.ResetResources, gameStartResets_0.ResetSceneryResearch, gameStartResets_0.ResetCreatures, gameStartResets_0.ResetRuins, gameStartResets_0.ResetSpecialLocationsAndAbandonedShips);
                    galaxy = method_79(galaxy);
                    galaxy.DelayedActions.Clear();
                    galaxy.GameEvents.ClearAndResetIdsToZero();
                    flag = true;
                }
                if (!flag)
                {
                    Galaxy.SetGalaxyPhysicalDimensions(empireStart_0.GalaxySectorX, empireStart_0.GalaxySectorY);
                    Galaxy.SetResearchCosts((int)double_4, Galaxy.ResearchNodeDefinitionsStatic);
                    Galaxy.SetHyperDriveSpeeds(resourceSystem_0, double_5, (int)double_4, Application.StartupPath, empty);
                    Galaxy.SetResearchRaceSpecialProjects(raceList_1);
                    Galaxy.SetResearchComponentMaxTechPoints((int)double_4);
                    galaxy = new Galaxy(num, galaxyShape_0, int_1, double_1, int_3, double_2, double_3, int_4, startStarDate, double_4, double_5, int_5, double_6, int_2, bool_5, empireStartList, Application.StartupPath, empty, main_0.bitmap_176, main_0.bitmap_205, difficultyLevel, empireStart_0.GalaxySectorX, empireStart_0.GalaxySectorY, resourceSystem_0, empireStart_0.AllowGiantKaltorGeneration);
                }
                galaxy.DifficultyLevelScalesAsPlayerApproachesVictory = empireStart_0.DifficultyScaling;
                galaxy.DestroyedPiratesDoNotRespawn = empireStart_0.DestroyedPiratesDoNotRespawn;
                galaxy.PirateShipMaintenanceFactor = empireStart_0.PirateShipMaintenanceFactor;
                galaxy.AllowTechTrading = empireStart_0.AllowTechTrading;
                galaxy.AllowRaceStartingCharacters = false;
                galaxy.StoryReturnOfTheShakturiEnabled = victoryConditions_0.EnableStoryEvents;
                galaxy.StoryDistantWorldsEnabled = bool_7;
                galaxy.GameDisasterEventsEnabled = victoryConditions_0.EnableDisasterEvents;
                galaxy.GameRaceSpecificEventsEnabled = victoryConditions_0.EnableRaceSpecificEvents;
                galaxy.GameRaceSpecificVictoryConditionsEnabled = victoryConditions_0.EnableRaceSpecificVictoryConditions;
                galaxy.StoryShadowsEnabled = victoryConditions_0.EnableStoryEventsShadows;
                galaxy.EmpireTerritoryColonyInfluenceRangeFactor = empireStart_0.EmpireTerritoryColonyInfluenceRangeFactor;
                galaxy.ColonizationRangeEnforceLimit = empireStart_0.ColonizationRangeEnforceLimit;
                galaxy.ColonizationRange = empireStart_0.ColonizationRange;
                galaxy.SubRoleNameSet = main_0.subRoleNameSet_0;
                galaxy.ColonyNames = main_0.list_1;
                galaxy.ColonyNameIndex = 0;
                galaxy.PirateFlagLarge = bitmap_12;
                galaxy.PirateFlagSmall = bitmap_13;
                Empire empire = galaxy.IndependentEmpire;
                if (!flag)
                {
                    empire = new Empire(galaxy, TextResolver.GetText("Independent"), isIndependentEmpire: true, null, null, null);
                    empire.StateMoney = 8.9884656743115785E+307;
                    empire.PrivateMoney = 8.9884656743115785E+307;
                    Bitmap bitmap = new Bitmap(100, 60, PixelFormat.Format32bppPArgb);
                    Graphics graphics = Graphics.FromImage(bitmap);
                    SolidBrush brush = new SolidBrush(Color.Transparent);
                    graphics.FillRectangle(brush, 0, 0, 100, 60);
                    empire.LargeFlagPicture = bitmap;
                    Bitmap bitmap2 = new Bitmap(12, 7, PixelFormat.Format32bppPArgb);
                    graphics = Graphics.FromImage(bitmap2);
                    brush = new SolidBrush(Color.Transparent);
                    graphics.FillRectangle(brush, 0, 0, 12, 7);
                    empire.SmallFlagPicture = bitmap2;
                    galaxy.IndependentEmpire = empire;
                }
                else if (empire.Research != null)
                {
                    empire.Research.Update(empire.DominantRace);
                }
                empire.GenerateDesignSpecifications(galaxy, null, isPirate: false, string.Empty);
                galaxy.SetNativeResourceCargoAndStartingStrategicCargoForAllIndependentHabitats();
                galaxy.SetEmpireForAllIndependentHabitats();
                galaxy.ReviewIndependentColonies();
                Race race = null;
                race = ((empireStart_0.RaceIndex < 0) ? method_48(galaxy, empireStart_0.Race, null, bool_2) : galaxy.Races[empireStart_0.RaceIndex]);
                int num2 = Galaxy.ResolveGovernmentId(empireStart_0.GovernmentStyle, race);
                GovernmentAttributes governmentAttributes = galaxy.Governments[num2];
                if (governmentAttributes.SpecialFunctionCode == 1 && empireStart_0.GovernmentStyle == "(" + TextResolver.GetText("Random") + ")" && race.PreferredStartingGovernmentId != num2)
                {
                    int num3 = 0;
                    while (governmentAttributes.SpecialFunctionCode == 1 && num3 < 10)
                    {
                        num2 = Galaxy.ResolveGovernmentId(empireStart_0.GovernmentStyle, race);
                        governmentAttributes = galaxy.Governments[num2];
                        num3++;
                    }
                }
                double homeSystemFactor = 0.0;
                HabitatType capitalHabitatType = HabitatType.Undefined;
                double expansion = 0.0;
                int designPictureFamilyIndex = race.DesignPictureFamilyIndex;
                if (empireStart_0.DesignPictureFamilyIndex >= 0)
                {
                    designPictureFamilyIndex = empireStart_0.DesignPictureFamilyIndex;
                }
                Empire empire2 = null;
                Habitat habitat = null;
                double xpos;
                double ypos;
                if (bool_2)
                {
                    Habitat habitat2 = null;
                    double double_7 = 0.0;
                    double double_8 = 0.0;
                    int num4 = 0;
                    double num5 = 0.0;
                    double num6 = 0.0;
                    byte resourceID = galaxy.ResourceSystem.FuelResources[0].ResourceID;
                    while (habitat2 == null)
                    {
                        switch (galaxyShape_0)
                        {
                            case GalaxyShape.Spiral:
                                if (empireStart_0.StartLocation == "(" + TextResolver.GetText("Random") + ")")
                                {
                                    method_83(galaxy, race, 0.0, 1.0, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Deep Core"))
                                {
                                    method_93(galaxy, 0.0, 0.29, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Outer Core"))
                                {
                                    method_93(galaxy, 0.29, 0.48, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Far Regions"))
                                {
                                    method_93(galaxy, 0.48, 1.0, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                break;
                            case GalaxyShape.Elliptical:
                                if (empireStart_0.StartLocation == "(" + TextResolver.GetText("Random") + ")")
                                {
                                    method_83(galaxy, race, 0.0, 1.0, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Deep Core"))
                                {
                                    method_93(galaxy, 0.0, 0.29, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Outer Core"))
                                {
                                    method_93(galaxy, 0.29, 0.48, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Inner Rim"))
                                {
                                    method_93(galaxy, 0.48, 0.86, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Outer Rim"))
                                {
                                    method_93(galaxy, 0.86, 1.0, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                break;
                            case GalaxyShape.Ring:
                                if (empireStart_0.StartLocation == "(" + TextResolver.GetText("Random") + ")")
                                {
                                    method_83(galaxy, race, 0.0, 1.0, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Core"))
                                {
                                    method_93(galaxy, 0.0, 0.29, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Void"))
                                {
                                    method_93(galaxy, 0.29, 0.82, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Rim"))
                                {
                                    method_93(galaxy, 0.82, 1.0, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                break;
                            case GalaxyShape.Irregular:
                            case GalaxyShape.ClustersEven:
                            case GalaxyShape.ClustersVaried:
                                if (empireStart_0.StartLocation == "(" + TextResolver.GetText("Random") + ")")
                                {
                                    method_83(galaxy, race, 0.0, 1.44, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Center"))
                                {
                                    method_93(galaxy, 0.0, 0.42, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Edge"))
                                {
                                    method_93(galaxy, 0.42, 1.44, out double_7, out double_8);
                                    habitat2 = galaxy.FindNearestHabitatWithResource(double_7 + num5, double_8 + num6, resourceID);
                                }
                                break;
                        }
                        GalaxyLocationList galaxyLocationList = galaxy.DetermineGalaxyLocationsAtPoint(habitat2.Xpos, habitat2.Ypos, GalaxyLocationType.NebulaCloud);
                        if (galaxyLocationList != null && galaxyLocationList.Count > 0)
                        {
                            habitat2 = null;
                        }
                        if (habitat2 != null)
                        {
                            for (int i = 0; i < galaxy.IndependentColonies.Count; i++)
                            {
                                Habitat habitat3 = galaxy.IndependentColonies[i];
                                if (habitat3 != null && habitat3.SystemIndex == habitat2.SystemIndex)
                                {
                                    habitat2 = null;
                                    break;
                                }
                            }
                        }
                        if (habitat2 != null && !galaxy.CheckNearIndependentColony(habitat2.Xpos, habitat2.Ypos, 2000000.0))
                        {
                            habitat2 = null;
                        }
                        num4++;
                        if (num4 > 50)
                        {
                            num4 = 0;
                            double num7 = 3000000.0;
                            num5 = num7 - Galaxy.Rnd.NextDouble() * num7 * 2.0;
                            num6 = num7 - Galaxy.Rnd.NextDouble() * num7 * 2.0;
                        }
                    }
                    designPictureFamilyIndex = race.DesignPictureFamilyIndexPirates;
                    if (empireStart_0.DesignPictureFamilyIndex >= 0)
                    {
                        designPictureFamilyIndex = empireStart_0.DesignPictureFamilyIndex;
                    }
                    galaxy.AllowRaceStartingCharacters = true;
                    galaxy.SelectRelativeHabitatSurfacePoint(habitat2, out var num8, out var num9);
                    empire2 = galaxy.GeneratePirateEmpire(habitat2, (int)num8, (int)num9, race, designPictureFamilyIndex, empireStart_0.TechLevel, empireStart_0.PiratePlayStyle, isPlayerEmpire: true, isSuperPirates: false);
                    if (!string.IsNullOrEmpty(empireStart_0.Name))
                    {
                        empire2.Name = empireStart_0.Name;
                    }
                    empire2.PiratePlayStyle = empireStart_0.PiratePlayStyle;
                    habitat = habitat2;
                    xpos = habitat2.Xpos;
                    ypos = habitat2.Ypos;
                    galaxy.AllowRaceStartingCharacters = false;
                    Habitat habitat4 = galaxy.FastFindNearestIndependentHabitat(xpos, ypos);
                    if (habitat4 != null && !empire2.CheckSystemExplored(habitat4.SystemIndex))
                    {
                        Habitat systemStar = Galaxy.DetermineHabitatSystemStar(habitat4);
                        empire2.SetSystemVisibility(systemStar, SystemVisibilityStatus.Explored);
                        empire2.ResourceMap.SetResourcesKnown(galaxy.Systems[systemStar].SystemStar, known: true);
                        for (int j = 0; j < galaxy.Systems[systemStar].Habitats.Count; j++)
                        {
                            empire2.ResourceMap.SetResourcesKnown(galaxy.Systems[systemStar].Habitats[j], known: true);
                        }
                    }
                }
                else
                {
                    homeSystemFactor = 0.0;
                    capitalHabitatType = HabitatType.Undefined;
                    Galaxy.ResolveHomeSystem(empireStart_0.HomeSystemFavourability, out capitalHabitatType, out homeSystemFactor);
                    capitalHabitatType = race.NativeHabitatType;
                    double double_9 = 0.0;
                    double double_10 = 0.0;
                    int num10 = 0;
                    double num11 = 0.0;
                    double num12 = 0.0;
                    while (habitat == null)
                    {
                        switch (galaxyShape_0)
                        {
                            case GalaxyShape.Spiral:
                                if (empireStart_0.StartLocation == "(" + TextResolver.GetText("Random") + ")")
                                {
                                    method_84(galaxy, race, 0.0, 1.0, out double_9, out double_10, bool_5: true, 0.5);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Deep Core"))
                                {
                                    method_93(galaxy, 0.0, 0.29, out double_9, out double_10);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Outer Core"))
                                {
                                    method_93(galaxy, 0.29, 0.48, out double_9, out double_10);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Far Regions"))
                                {
                                    method_93(galaxy, 0.48, 1.0, out double_9, out double_10);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                break;
                            case GalaxyShape.Elliptical:
                                if (empireStart_0.StartLocation == "(" + TextResolver.GetText("Random") + ")")
                                {
                                    method_84(galaxy, race, 0.0, 1.0, out double_9, out double_10, bool_5: true, 0.5);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Deep Core"))
                                {
                                    method_93(galaxy, 0.0, 0.29, out double_9, out double_10);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Outer Core"))
                                {
                                    method_93(galaxy, 0.29, 0.48, out double_9, out double_10);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Inner Rim"))
                                {
                                    method_93(galaxy, 0.48, 0.86, out double_9, out double_10);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Outer Rim"))
                                {
                                    method_93(galaxy, 0.86, 1.0, out double_9, out double_10);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                break;
                            case GalaxyShape.Ring:
                                if (empireStart_0.StartLocation == "(" + TextResolver.GetText("Random") + ")")
                                {
                                    method_84(galaxy, race, 0.0, 1.0, out double_9, out double_10, bool_5: true, 0.5);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Core"))
                                {
                                    method_93(galaxy, 0.0, 0.29, out double_9, out double_10);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Void"))
                                {
                                    method_93(galaxy, 0.29, 0.82, out double_9, out double_10);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Rim"))
                                {
                                    method_93(galaxy, 0.82, 1.0, out double_9, out double_10);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                break;
                            case GalaxyShape.Irregular:
                            case GalaxyShape.ClustersEven:
                            case GalaxyShape.ClustersVaried:
                                if (empireStart_0.StartLocation == "(" + TextResolver.GetText("Random") + ")")
                                {
                                    method_84(galaxy, race, 0.0, 1.44, out double_9, out double_10, bool_5: true, 0.5);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Center"))
                                {
                                    method_93(galaxy, 0.0, 0.42, out double_9, out double_10);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                else if (empireStart_0.StartLocation == TextResolver.GetText("Edge"))
                                {
                                    method_93(galaxy, 0.42, 1.44, out double_9, out double_10);
                                    habitat = galaxy.FindNearestUncolonizedHabitat(double_9 + num11, double_10 + num12, capitalHabitatType);
                                }
                                break;
                        }
                        GalaxyLocationList galaxyLocationList2 = galaxy.DetermineGalaxyLocationsAtPoint(habitat.Xpos, habitat.Ypos, GalaxyLocationType.NebulaCloud);
                        if (galaxyLocationList2 != null && galaxyLocationList2.Count > 0)
                        {
                            habitat = null;
                        }
                        num10++;
                        if (num10 > 50)
                        {
                            double num13 = 3000000.0;
                            if ((double)num10 > 1000.0)
                            {
                                num13 = 5000000.0;
                            }
                            num11 = num13 - Galaxy.Rnd.NextDouble() * num13 * 2.0;
                            num12 = num13 - Galaxy.Rnd.NextDouble() * num13 * 2.0;
                        }
                    }
                    expansion = 0.0;
                    designPictureFamilyIndex = race.DesignPictureFamilyIndex;
                    if (empireStart_0.DesignPictureFamilyIndex >= 0)
                    {
                        designPictureFamilyIndex = empireStart_0.DesignPictureFamilyIndex;
                    }
                    empire2 = galaxy.GenerateEmpire(galaxy, isPlayerEmpire: true, empireStart_0.Name, habitat, race, designPictureFamilyIndex, num2, homeSystemFactor, empireStart_0.HomeSystemFavourability, empireStart_0.Age, empireStart_0.TechLevel, empireStart_0.CorruptionMultiplier, out expansion, main_0.gameOptions_0, victoryConditions_0);
                    if (empireStart_0.Age == 0)
                    {
                        galaxy.ClearIndependentColoniesFromSystem(habitat.SystemIndex);
                    }
                    galaxy.PlayerEmpire = empire2;
                    galaxy.SetEmpireDifficultyFactors(empire2);
                    xpos = habitat.Xpos;
                    ypos = habitat.Ypos;
                }
                galaxy.PlayerEmpire = empire2;
                if (empireStart_0.FlagShape >= 0)
                {
                    Color primaryColor = empireStart_0.PrimaryColor;
                    Color secondaryColor = empireStart_0.SecondaryColor;
                    empire2.MainColor = primaryColor;
                    empire2.SecondaryColor = secondaryColor;
                    Bitmap smallFlagPicture = null;
                    Bitmap largeFlagPicture = null;
                    empire2.FlagShape = Galaxy.GenerateEmpireFlag(primaryColor, secondaryColor, empireStart_0.FlagShape, list, ref smallFlagPicture, ref largeFlagPicture);
                    empire2.SmallFlagPicture = smallFlagPicture;
                    empire2.LargeFlagPicture = largeFlagPicture;
                    Bitmap bitmap3 = (empire2.MediumFlagPicture = GraphicsHelper.ScaleImage(largeFlagPicture, 35, 21, 1f, lowQuality: false));
                    if (bool_2)
                    {
                        using Graphics graphics2 = Graphics.FromImage(largeFlagPicture);
                        GraphicsHelper.SetGraphicsQualityToHigh(graphics2);
                        graphics2.DrawImage(srcRect: new Rectangle(0, 0, galaxy.PirateFlagLarge.Width, galaxy.PirateFlagLarge.Height), destRect: new Rectangle(2, 2, 35, 22), image: galaxy.PirateFlagLarge, srcUnit: GraphicsUnit.Pixel);
                    }
                }
                galaxy.UpdateSystemInfo(empire2);
                int num14 = 0;
                DistantWorlds.Types.EmpireList empireList = new DistantWorlds.Types.EmpireList();
                List<HabitatList> list2 = new List<HabitatList>();
                List<double> list3 = new List<double>();
                List<int> list4 = new List<int>();
                List<int> list5 = new List<int>();
                List<int> list6 = new List<int>();
                EmpireStartList empireStartList2 = new EmpireStartList();
                if (!bool_2)
                {
                    empireList.Add(empire2);
                    list3.Add(expansion);
                    list6.Add(empireStart_0.Age);
                    empireStartList2.Add(empireStart_0);
                }
                num14 = empireStartList_0.Count;
                if (empire2 != null && empire2.DominantRace != null)
                {
                    empireStartList_0.Update(raceList, empire2.DominantRace.Name);
                }
                else
                {
                    empireStartList_0.Update(raceList);
                }
                empireStartList2.AddRange(empireStartList_0);
                int num15 = 0;
                object lockObject = default(object);
                Game game2;
                while (true)
                {
                    if (num15 >= num14)
                    {
                        int num16 = 0;
                        foreach (double item5 in list3)
                        {
                            double num17 = item5;
                            num16 += (int)num17;
                        }
                        int allowableMaximumStartingColonies = galaxy.AllowableMaximumStartingColonies;
                        if (num16 > allowableMaximumStartingColonies)
                        {
                            double num18 = (double)allowableMaximumStartingColonies / (double)num16;
                            for (int k = 0; k < list3.Count; k++)
                            {
                                list3[k] *= num18;
                            }
                        }
                        num16 = 0;
                        foreach (double item6 in list3)
                        {
                            double num19 = item6;
                            int num20 = Math.Max(0, (int)num19 - 1);
                            list4.Add(num20);
                            list5.Add(num20);
                            num16 += num20;
                        }
                        List<HabitatType> list7 = new List<HabitatType>();
                        list7.Add(HabitatType.BarrenRock);
                        list7.Add(HabitatType.Ice);
                        list7.Add(HabitatType.Volcanic);
                        list7.Add(HabitatType.Desert);
                        List<int> list8 = new List<int>();
                        int num21 = 0;
                        while (num21 < num16)
                        {
                            for (int l = 0; l < empireList.Count; l++)
                            {
                                if (list4[l] > 0)
                                {
                                    list8.Add(empireList[l].EmpireId);
                                    list4[l]--;
                                    num21++;
                                }
                            }
                        }
                        List<double> list9 = new List<double>();
                        List<HabitatList> list10 = new List<HabitatList>();
                        for (int m = 0; m < empireList.Count; m++)
                        {
                            galaxy.UpdateSystemInfo(empire2);
                            Habitat capital = empireList[m].Capital;
                            list2.Add(new HabitatList());
                            list2[m].Add(capital);
                            list10.Add(new HabitatList());
                            list10[m].Add(Galaxy.DetermineHabitatSystemStar(capital));
                            double num22 = 0.0;
                            if (num16 > 0)
                            {
                                num22 = (double)list5[m] / (double)num16;
                            }
                            Math.Min(1.0, (double)num16 / (double)allowableMaximumStartingColonies);
                            double item = 1400000000.0 / (double)galaxy.StarCount + num22 * 1.0 * (double)Galaxy.SizeX;
                            list9.Add(item);
                        }
                        galaxy.EmpireTerritory.ReviewEmpireTerritory(galaxy);
                        for (int n = 0; n < list8.Count; n++)
                        {
                            Empire byEmpireId = empireList.GetByEmpireId(list8[n]);
                            int num23 = byEmpireId.EmpireId - 1;
                            if (bool_2)
                            {
                                num23--;
                            }
                            double num24 = list9[num23];
                            galaxy.UpdateSystemInfo(empire2);
                            Habitat habitat5 = null;
                            habitat5 = ((!bool_6) ? galaxy.FindNearestColonizableHabitatUnoccupiedSystem(byEmpireId.Capital.Xpos, byEmpireId.Capital.Ypos, byEmpireId) : galaxy.FindNearestColonizableHabitat(byEmpireId.Capital.Xpos, byEmpireId.Capital.Ypos, byEmpireId));
                            double num25 = double.MaxValue;
                            if (habitat5 != null)
                            {
                                num25 = galaxy.CalculateDistance(byEmpireId.Capital.Xpos, byEmpireId.Capital.Ypos, habitat5.Xpos, habitat5.Ypos);
                            }
                            if (num25 > num24)
                            {
                                int num26 = 0;
                                while (num25 > num24)
                                {
                                    habitat5 = null;
                                    int num27 = 0;
                                    while (habitat5 == null)
                                    {
                                        method_91(galaxy, galaxy.SelectRandomHeading(), byEmpireId.Capital.Xpos, byEmpireId.Capital.Ypos, num24, out var double_11, out var double_12);
                                        habitat5 = galaxy.FastFindNearestPlanetMoonOfTypesUnoccupiedSystem(double_11, double_12, byEmpireId, list7);
                                        if (habitat5 != null)
                                        {
                                            if (habitat5.Category == HabitatCategoryType.Moon && habitat5.Parent.Type != HabitatType.GasGiant)
                                            {
                                                habitat5 = null;
                                            }
                                            else if (habitat5.Category == HabitatCategoryType.Planet && (habitat5.OrbitDistance < 4500 || habitat5.OrbitDistance > 11000))
                                            {
                                                habitat5 = null;
                                            }
                                        }
                                        num27++;
                                        if (num27 > 50)
                                        {
                                            num24 *= 1.2;
                                            num27 = 0;
                                        }
                                    }
                                    num25 = galaxy.CalculateDistance(byEmpireId.Capital.Xpos, byEmpireId.Capital.Ypos, habitat5.Xpos, habitat5.Ypos);
                                    num26++;
                                    if (num26 > 50)
                                    {
                                        num24 *= 1.2;
                                        num26 = 0;
                                    }
                                }
                                HabitatType type = HabitatType.Undefined;
                                int pictureRef = 0;
                                int landscapePictureRef = 0;
                                int diameter = 0;
                                int minOrbitDistance = 0;
                                int maxOrbitDistance = 0;
                                List<HabitatType> list11 = byEmpireId.ColonizableHabitatTypesForEmpire(byEmpireId);
                                list11.Add(byEmpireId.DominantRace.NativeHabitatType);
                                int index = Galaxy.Rnd.Next(0, list11.Count);
                                switch (list11[index])
                                {
                                    case HabitatType.Volcanic:
                                        galaxy.SelectVolcanicPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                                        break;
                                    case HabitatType.Desert:
                                        galaxy.SelectDesertPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                                        break;
                                    case HabitatType.MarshySwamp:
                                        galaxy.SelectMarshySwampPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                                        break;
                                    case HabitatType.Continental:
                                        galaxy.SelectContinentalPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                                        break;
                                    case HabitatType.Ocean:
                                        galaxy.SelectOceanPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                                        break;
                                    default:
                                        galaxy.SelectDesertPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                                        break;
                                    case HabitatType.Ice:
                                        galaxy.SelectIcePlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                                        break;
                                }
                                habitat5.Type = type;
                                habitat5.Diameter = (short)diameter;
                                habitat5.PictureRef = (short)pictureRef;
                                habitat5.LandscapePictureRef = (short)landscapePictureRef;
                                habitat5.BaseQuality = galaxy.SelectHabitatQuality(habitat5, (float)galaxy.ColonyPrevalence);
                                habitat5.Resources.Clear();
                                galaxy.SelectResources(habitat5);
                            }
                            if (habitat5 != null)
                            {
                                Habitat item2 = Galaxy.DetermineHabitatSystemStar(habitat5);
                                if (!list10[num23].Contains(item2))
                                {
                                    list10[num23].Add(item2);
                                    list2[num23].Add(habitat5);
                                    galaxy.MakeHabitatIntoColony(galaxy, habitat5, byEmpireId, list6[num23], byEmpireId.DominantRace, 1.0, hasSpacePort: false);
                                }
                                else
                                {
                                    galaxy.MakeHabitatIntoColony(galaxy, habitat5, byEmpireId, list6[num23], byEmpireId.DominantRace, 1.0, hasSpacePort: false);
                                }
                                if (galaxy.IndependentColonies.Contains(habitat5))
                                {
                                    galaxy.IndependentColonies.Remove(habitat5);
                                }
                                Rectangle galaxySection = new Rectangle((int)habitat5.Xpos - 1600000, (int)habitat5.Ypos - 1600000, 3200000, 3200000);
                                galaxy.EmpireTerritory.ReviewEmpireTerritoryUpdate(galaxy, galaxySection);
                            }
                        }
                        galaxy.ReviewEmpireTerritoryCore(false);
                        galaxy.UpdateSystemInfo(empire2);
                        for (int num28 = 0; num28 < 20; num28++)
                        {
                            galaxy.ReviewResourcePrices();
                            galaxy.ReviewComponentPrices();
                        }
                        galaxy.ResetLastTouchTimes();
                        galaxy.DoTasks(gameFinished: false, empire2, null, null, null);
                        galaxy.DeferEventsForGameStart = true;
                        for (int num29 = 0; num29 < empireList.Count; num29++)
                        {
                            Empire empire3 = empireList[num29];
                            empire3.RecalculateEmpirePopulation();
                            empire3.CheckColoniesForBaseFacilities();
                            empire3.RecalculateEmpireCorruption();
                            empire3.RecalculateColonyTaxRevenues();
                            if (int_5 > 0)
                            {
                                empire3.LastLongTouch = galaxy.CurrentDateTime.Subtract(new TimeSpan(0, 0, (int)empire3.LongProcessingInterval + 1));
                                empire3.LastIntermediateTouch = empire3.LastLongTouch;
                                empire3.LastPeriodicTouch = empire3.LastLongTouch;
                                empire3.LastRegularTouch = empire3.LastLongTouch;
                                empire3.LastShortTouch = empire3.LastLongTouch;
                                empire3.LastHugeTouch = empire3.LastLongTouch;
                            }
                            empire3.PreWarpProgressEventOccurredSendPirateRaid = true;
                            if (empireStartList2[num29].TechLevel > 0.0)
                            {
                                empire3.PreWarpProgressEventOccurredSendPirateRaid = true;
                                empire3.PreWarpProgressEventOccurredExperienceFirstPirateRaid = true;
                                empire3.PreWarpProgressEventOccurredFirstContactPirateOrIndependent = true;
                                empire3.PreWarpProgressEventOccurredFirstContactNormalEmpire = true;
                                empire3.PreWarpProgressEventOccurredBuildFirstShip = true;
                                empire3.PreWarpProgressEventOccurredBuildFirstSpaceport = true;
                                empire3.PreWarpProgressEventOccurredBuildFirstMiningStation = true;
                                empire3.PreWarpProgressEventOccurredBuildFirstResearchStation = true;
                                empire3.PreWarpProgressEventOccurredDiscoverHyperspaceTech = true;
                                empire3.PreWarpProgressEventOccurredDiscoverColonizationTech = true;
                                empire3.PreWarpProgressEventOccurredFirstHyperjump = true;
                                empire3.PreWarpProgressEventOccurredEncounterFirstKaltor = true;
                                empire3.PreWarpProgressEventOccurredBuildFirstMilitaryShip = true;
                            }
                            if (empireStartList2[num29].TechLevel > 0.0)
                            {
                                int newSpacePortAmount = 1 + (int)((double)empire3.Colonies.Count / 4.5);
                                HabitatList habitatList = empire3.DetermineNewSpacePortLocations(empire3.Colonies, newSpacePortAmount, excludeColoniesWithEnemiesPresent: false);
                                galaxy.CreateSpacePorts(galaxy, empire3, habitatList);
                                foreach (Habitat item7 in habitatList)
                                {
                                    galaxy.SetColonyResources(galaxy, item7, empire3, hasSpacePort: true);
                                }
                            }
                            empire3.CheckColoniesForBaseFacilities();
                            Habitat habitat6 = galaxy.FindNearestHabitatUnoccupiedSystem(empire3.Capital.Xpos, empire3.Capital.Ypos, HabitatType.GasGiant);
                            if (habitat6 != null && habitat6.ResearchBonus <= 0 && habitat6.ResearchBonusIndustry == IndustryType.Undefined)
                            {
                                IndustryType industryType = IndustryType.Undefined;
                                if (empire3.Policy != null)
                                {
                                    industryType = empire3.Policy.ResearchIndustryFocus;
                                }
                                if (industryType == IndustryType.Undefined)
                                {
                                    industryType = (IndustryType)Galaxy.Rnd.Next(1, 4);
                                }
                                habitat6.ResearchBonusIndustry = industryType;
                                habitat6.ResearchBonus = (byte)Galaxy.Rnd.Next(10, 31);
                                galaxy.Systems[habitat6.SystemIndex].HasResearchBonus = true;
                            }
                            if (empireStartList2[num29].TechLevel == 0.0)
                            {
                                for (int num30 = 0; num30 < galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; num30++)
                                {
                                    ResourceDefinition resourceDefinition = galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance[num30];
                                    if (resourceDefinition == null || !resourceDefinition.IsImportantPreWarpResource)
                                    {
                                        continue;
                                    }
                                    ResourcePrevalence mostTerrestrialResourcePrevalance = resourceDefinition.GetMostTerrestrialResourcePrevalance();
                                    if (mostTerrestrialResourcePrevalance == null)
                                    {
                                        continue;
                                    }
                                    Habitat habitat7 = galaxy.FindNearestHabitat(empire3.Capital.Xpos, empire3.Capital.Ypos, mostTerrestrialResourcePrevalance.HabitatType);
                                    if (habitat7 == null)
                                    {
                                        continue;
                                    }
                                    Habitat habitat8 = Galaxy.DetermineHabitatSystemStar(habitat7);
                                    Habitat habitat9 = Galaxy.DetermineHabitatSystemStar(empire3.Capital);
                                    if (habitat8 != habitat9 && !mostTerrestrialResourcePrevalance.HabitatIsGasCloud)
                                    {
                                        if (mostTerrestrialResourcePrevalance.HabitatIsAsteroid)
                                        {
                                            habitat7 = galaxy.GenerateAsteroid(galaxy, habitat9, mostTerrestrialResourcePrevalance.HabitatType);
                                        }
                                        else
                                        {
                                            switch (mostTerrestrialResourcePrevalance.HabitatType)
                                            {
                                                case HabitatType.Volcanic:
                                                    habitat7 = galaxy.GenerateVolcanicPlanet(galaxy, habitat9);
                                                    break;
                                                case HabitatType.Desert:
                                                    habitat7 = galaxy.GenerateDesertPlanet(galaxy, habitat9);
                                                    break;
                                                case HabitatType.MarshySwamp:
                                                    habitat7 = galaxy.GenerateSwampPlanet(galaxy, habitat9);
                                                    break;
                                                case HabitatType.Continental:
                                                    habitat7 = galaxy.GenerateContinentalPlanet(galaxy, habitat9);
                                                    break;
                                                case HabitatType.Ocean:
                                                    habitat7 = galaxy.GenerateOceanPlanet(galaxy, habitat9);
                                                    break;
                                                case HabitatType.BarrenRock:
                                                    habitat7 = galaxy.GenerateBarrenRockPlanet(galaxy, habitat9);
                                                    break;
                                                case HabitatType.Ice:
                                                    habitat7 = galaxy.GenerateIcePlanet(galaxy, habitat9);
                                                    break;
                                                case HabitatType.GasGiant:
                                                    habitat7 = galaxy.GenerateGasGiantPlanet(galaxy, habitat9);
                                                    break;
                                                case HabitatType.FrozenGasGiant:
                                                    habitat7 = galaxy.GenerateFrozenGasGiantPlanet(galaxy, habitat9);
                                                    break;
                                            }
                                        }
                                        if (habitat7 != null)
                                        {
                                            bool lockTaken = false;
                                            try
                                            {
                                                Monitor.Enter(lockObject = galaxy._LockObject, ref lockTaken);
                                                galaxy.AddHabitat(habitat7, habitat9);
                                            }
                                            finally
                                            {
                                                if (lockTaken)
                                                {
                                                    Monitor.Exit(lockObject);
                                                }
                                            }
                                            if (empireStartList2[num29].TechLevel == 0.0)
                                            {
                                                empire3.ResourceMap.SetResourcesKnown(habitat7, known: false);
                                            }
                                        }
                                    }
                                    if (habitat7 != null)
                                    {
                                        int num31 = Galaxy.Rnd.Next(150, 300);
                                        if (resourceDefinition.IsFuel)
                                        {
                                            num31 = Galaxy.Rnd.Next(400, 1000);
                                        }
                                        int num32 = habitat7.Resources.IndexOf(resourceDefinition.ResourceID, 0);
                                        if (num32 < 0)
                                        {
                                            habitat7.Resources.Add(new HabitatResource(resourceDefinition.ResourceID, num31));
                                        }
                                        else
                                        {
                                            habitat7.Resources[num32].Abundance = (short)num31;
                                        }
                                    }
                                }
                            }
                            if (empireStartList2[num29].TechLevel == 0.0)
                            {
                                ResearchNodeDefinition researchNodeDefinition = Galaxy.ResearchNodeDefinitionsStatic.FindNodeBySpecialFunctionCode(2);
                                if (researchNodeDefinition != null)
                                {
                                    HabitatList habitats = galaxy.Systems[empire3.Capital.SystemIndex].Habitats;
                                    double distance = 0.0;
                                    Habitat habitat10 = galaxy.FindPlanetMoonBeyondRangeOrFurthestNoRuins(empire3.Capital, habitats, 20000.0, out distance);
                                    if (habitat10 != null)
                                    {
                                        galaxy.SelectRuinsUnlockTech(habitat10, researchNodeDefinition.ResearchNodeId);
                                    }
                                    else
                                    {
                                        Habitat habitat11 = Galaxy.DetermineHabitatSystemStar(empire3.Capital);
                                        habitat10 = galaxy.GenerateBarrenRockPlanet(galaxy, habitat11);
                                        if (habitat10 != null)
                                        {
                                            bool lockTaken2 = false;
                                            try
                                            {
                                                Monitor.Enter(lockObject = galaxy._LockObject, ref lockTaken2);
                                                galaxy.AddHabitat(habitat10, habitat11);
                                            }
                                            finally
                                            {
                                                if (lockTaken2)
                                                {
                                                    Monitor.Exit(lockObject);
                                                }
                                            }
                                            empire3.ResourceMap.SetResourcesKnown(habitat10, known: false);
                                            galaxy.SelectRuinsUnlockTech(habitat10, researchNodeDefinition.ResearchNodeId);
                                        }
                                    }
                                }
                            }
                            empire3.DetermineResearchStationLocation(allowOccupiedSystems: false, mustHaveBuildableResearchStationDesign: true);
                            if (empireStartList2[num29].TechLevel > 0.0)
                            {
                                galaxy.CreateResearchStations(galaxy, empire3, bool_6);
                            }
                            bool_6 = false;
                            if (empireStartList2[num29].TechLevel > 0.0)
                            {
                                galaxy.CreateMiningStations(galaxy, empire3, bool_6);
                            }
                            galaxy.SetLuxuryResourcesAtColonies(galaxy, empire3);
                            empire3.InitiateConstruction = false;
                            for (int num33 = 0; num33 < 1; num33++)
                            {
                                empire3.ReviewTaxes();
                                empire3.RecalculateColonyTaxRevenues();
                            }
                            empire3.RecalculateEmpirePopulation();
                            foreach (Habitat colony in empire3.Colonies)
                            {
                                if (int_5 > 0)
                                {
                                    empire3.ProcessColonyTroops(colony, null, 0.0, 100.0, 100.0);
                                    empire3.ProcessColonyTroops(colony, null, 0.0, 300.0, 300.0);
                                    empire3.ProcessColonyTroops(colony, null, 0.0, 300.0, 300.0);
                                }
                                colony.RecalculateAnnualTaxRevenue();
                            }
                            empire3.ReviewTaxes();
                            foreach (Habitat colony2 in empire3.Colonies)
                            {
                                colony2.RecalculateAnnualTaxRevenue();
                            }
                            double num34 = (empire3.BuildFactor = 0.5 + Math.Min(0.2, (double)empire3.Colonies.Count / 20.0));
                            empire3.DoTasks();
                            empire3.BuildFactor = 1.0;
                            empire3.InitiateConstruction = true;
                            int seconds = Galaxy.Rnd.Next(1, (int)empire3.LongProcessingInterval);
                            empire3.LastLongTouch = galaxy.CurrentDateTime.Subtract(new TimeSpan(0, 0, seconds));
                            empire3.LastIntermediateTouch = empire3.LastLongTouch;
                            empire3.LastPeriodicTouch = empire3.LastLongTouch;
                            empire3.LastRegularTouch = empire3.LastLongTouch;
                            empire3.LastShortTouch = empire3.LastLongTouch;
                            empire3.LastHugeTouch = empire3.LastLongTouch;
                        }
                        if (main_0.gameOptions_0 != null)
                        {
                            empire2.AttackRangePatrol = main_0.gameOptions_0.AttackRangePatrol;
                            empire2.AttackRangeEscort = main_0.gameOptions_0.AttackRangeEscort;
                            empire2.AttackRangeOther = main_0.gameOptions_0.AttackRangeOther;
                            empire2.AttackRangeAttack = main_0.gameOptions_0.AttackRangeAttack;
                            empire2.AttackOvermatchFactor = main_0.gameOptions_0.AttackOverMatchFactor;
                            empire2.AttackRangePatrolManual = main_0.gameOptions_0.AttackRangePatrolManual;
                            empire2.AttackRangeEscortManual = main_0.gameOptions_0.AttackRangeEscortManual;
                            empire2.AttackRangeOtherManual = main_0.gameOptions_0.AttackRangeOtherManual;
                            empire2.AttackRangeAttackManual = main_0.gameOptions_0.AttackRangeAttackManual;
                        }
                        for (int num35 = 0; num35 < empireList.Count; num35++)
                        {
                            Empire empire4 = empireList[num35];
                            if (empireStartList2[num35].TechLevel > 0.0)
                            {
                                galaxy.CreateStateShips(galaxy, empire4);
                                galaxy.CreatePrivateShips(galaxy, empire4);
                                galaxy.FillShipsWithTroops(galaxy, empire4);
                                empire4.AssignMissionsToBuiltObjectList(empire4.BuiltObjects, atWar: false, null);
                                empire4.AssignMissionsToBuiltObjectList(empire4.PrivateBuiltObjects, atWar: false, null);
                            }
                        }
                        foreach (Empire item8 in empireList)
                        {
                            foreach (Empire item9 in empireList)
                            {
                                if (item9 == item8)
                                {
                                    continue;
                                }
                                for (int num36 = 0; num36 < galaxy.Systems.Count; num36++)
                                {
                                    SystemVisibilityStatus status = item9.SystemVisibility[galaxy.Systems[num36].SystemStar.SystemIndex].Status;
                                    SystemVisibilityStatus status2 = item8.SystemVisibility[galaxy.Systems[num36].SystemStar.SystemIndex].Status;
                                    bool flag2 = false;
                                    if ((status2 == SystemVisibilityStatus.Explored || status2 == SystemVisibilityStatus.Visible) && status == SystemVisibilityStatus.Visible)
                                    {
                                        flag2 = true;
                                    }
                                    if ((status == SystemVisibilityStatus.Explored || status == SystemVisibilityStatus.Visible) && (status2 == SystemVisibilityStatus.Explored || status2 == SystemVisibilityStatus.Visible) && Galaxy.Rnd.Next(0, 3) == 1)
                                    {
                                        flag2 = true;
                                    }
                                    if (!flag2)
                                    {
                                        continue;
                                    }
                                    DiplomaticRelation diplomaticRelation = item8.DiplomaticRelations[item9];
                                    if (diplomaticRelation == null)
                                    {
                                        diplomaticRelation = new DiplomaticRelation(DiplomaticRelationType.None, item8, item8, item9, tradeRestrictedResources: false);
                                        item8.DiplomaticRelations.Add(diplomaticRelation);
                                        diplomaticRelation = new DiplomaticRelation(DiplomaticRelationType.None, item8, item9, item8, tradeRestrictedResources: false);
                                        item9.DiplomaticRelations.Add(diplomaticRelation);
                                    }
                                    else if (diplomaticRelation.Type == DiplomaticRelationType.NotMet)
                                    {
                                        diplomaticRelation.Type = DiplomaticRelationType.None;
                                        DiplomaticRelation diplomaticRelation2 = item9.DiplomaticRelations[item8];
                                        if (diplomaticRelation2 == null)
                                        {
                                            diplomaticRelation2 = new DiplomaticRelation(DiplomaticRelationType.None, item8, item9, item8, tradeRestrictedResources: false);
                                            item9.DiplomaticRelations.Add(diplomaticRelation2);
                                        }
                                        else if (diplomaticRelation2.Type == DiplomaticRelationType.NotMet)
                                        {
                                            diplomaticRelation2.Type = DiplomaticRelationType.None;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        if (empire2.PirateEmpireBaseHabitat != null)
                        {
                            for (int num37 = 0; num37 < galaxy.Empires.Count; num37++)
                            {
                                Empire empire5 = galaxy.Empires[num37];
                                if (empire5 == null || !empire5.Active || empire5 == empire2)
                                {
                                    continue;
                                }
                                for (int num38 = 0; num38 < galaxy.Systems.Count; num38++)
                                {
                                    SystemInfo systemInfo = galaxy.Systems[num38];
                                    SystemVisibilityStatus status3 = empire5.SystemVisibility[systemInfo.SystemStar.SystemIndex].Status;
                                    SystemVisibilityStatus status4 = empire2.SystemVisibility[systemInfo.SystemStar.SystemIndex].Status;
                                    bool flag3 = false;
                                    if ((status4 == SystemVisibilityStatus.Explored || status4 == SystemVisibilityStatus.Visible) && status3 == SystemVisibilityStatus.Visible)
                                    {
                                        flag3 = true;
                                    }
                                    if ((status3 == SystemVisibilityStatus.Explored || status3 == SystemVisibilityStatus.Visible) && (status4 == SystemVisibilityStatus.Explored || status4 == SystemVisibilityStatus.Visible) && Galaxy.Rnd.Next(0, 3) == 1)
                                    {
                                        flag3 = true;
                                    }
                                    if (!flag3)
                                    {
                                        continue;
                                    }
                                    PirateRelation pirateRelation = empire2.ObtainPirateRelation(empire5);
                                    if (pirateRelation.Type == PirateRelationType.NotMet)
                                    {
                                        pirateRelation.Type = PirateRelationType.None;
                                        PirateRelation pirateRelation2 = empire5.ObtainPirateRelation(empire2);
                                        if (pirateRelation2.Type == PirateRelationType.NotMet)
                                        {
                                            pirateRelation2.Type = PirateRelationType.None;
                                        }
                                        if (empire2.PirateEmpireBaseHabitat != null && empire5.KnownPirateEmpires != null && !empire5.KnownPirateEmpires.Contains(empire2))
                                        {
                                            empire5.KnownPirateEmpires.Add(empire2);
                                        }
                                        if (empire5.PirateEmpireBaseHabitat != null && empire2.KnownPirateEmpires != null && !empire2.KnownPirateEmpires.Contains(empire5))
                                        {
                                            empire2.KnownPirateEmpires.Add(empire5);
                                        }
                                    }
                                    break;
                                }
                            }
                            empire2.ColonizationTargets = empire2.PirateReviewColoniesToControl();
                        }
                        galaxy.AllowRaceStartingCharacters = true;
                        for (int num39 = 0; num39 < empireList.Count; num39++)
                        {
                            if (empireList[num39].DominantRace != null)
                            {
                                empireList[num39].GenerateStartingCharacters();
                            }
                        }
                        galaxy.DoTasks(gameFinished: false, empire2, null, null, null);
                        galaxy.ReviewEmpireTerritoryCore(false);
                        galaxy.UpdateSystemInfo(empire2);
                        if (main_0 != null && empire2 != null)
                        {
                            Galaxy.ApplyDesignUpgradeGameOptionsToPolicies(main_0.gameOptions_0, empire2.Policy);
                        }
                        if (double_3 > 0.0 && int_4 == 0 && !bool_2)
                        {
                            Empire empire6 = galaxy.FindNearestPirateFaction(xpos, ypos, null, includeSuperPirates: true);
                            if (empire6 != null)
                            {
                                double num40 = galaxy.CalculateDistance(xpos, ypos, empire6.PirateEmpireBaseHabitat.Xpos, empire6.PirateEmpireBaseHabitat.Ypos);
                                if (num40 > (double)Galaxy.SectorSizeX)
                                {
                                    double num41 = xpos;
                                    double num42 = ypos;
                                    bool flag4 = false;
                                    int num43 = 0;
                                    while (!flag4 && num43 < 50)
                                    {
                                        num41 += 200000.0 - Galaxy.Rnd.NextDouble() * 400000.0;
                                        num42 += 200000.0 - Galaxy.Rnd.NextDouble() * 400000.0;
                                        byte resourceID2 = galaxy.ResourceSystem.FuelResources[0].ResourceID;
                                        Habitat habitat12 = galaxy.FindNearestHabitatWithResource(num41, num42, resourceID2);
                                        if (habitat12.BasesAtHabitat != null && habitat12.BasesAtHabitat.Count > 0)
                                        {
                                            for (int num44 = 0; num44 < habitat12.BasesAtHabitat.Count; num44++)
                                            {
                                                if (habitat12.BasesAtHabitat[num44].Empire != null && habitat12.BasesAtHabitat[num44].Empire != galaxy.IndependentEmpire && habitat12.BasesAtHabitat[num44].Empire.PirateEmpireBaseHabitat == null)
                                                {
                                                    habitat12 = null;
                                                    break;
                                                }
                                            }
                                        }
                                        if (habitat12 != null)
                                        {
                                            Habitat habitat13 = galaxy.FindNearestColony(habitat12.Xpos, habitat12.Ypos, null, 0, includeIndependentColonies: false);
                                            double num45 = galaxy.CalculateDistance(habitat13.Xpos, habitat13.Ypos, habitat12.Xpos, habitat12.Ypos);
                                            if (num45 > (double)Galaxy.MaxSolarSystemSize * 2.1 && habitat12 != null && habitat12.Category != 0 && galaxy.NextEmpireID < Galaxy.MaximumEmpireCount)
                                            {
                                                galaxy.SelectPopularDesignCandidates();
                                                galaxy.SelectRelativeHabitatSurfacePoint(habitat12, out var num46, out var num47);
                                                galaxy.GeneratePirateEmpire(habitat12, (int)num46, (int)num47, useRace: true);
                                                flag4 = true;
                                            }
                                        }
                                        num43++;
                                    }
                                }
                            }
                        }
                        if (gameStartResets_0 == null || string.IsNullOrEmpty(gameStartResets_0.GalaxyFilepath) || gameStartResets_0.ResetRuins)
                        {
                            foreach (Habitat habitat22 in galaxy.Habitats)
                            {
                                bool flag5 = true;
                                if (galaxy.Systems[habitat22.SystemIndex].DominantEmpire != null && galaxy.Systems[habitat22.SystemIndex].DominantEmpire.Empire != null)
                                {
                                    flag5 = false;
                                }
                                if (flag5)
                                {
                                    galaxy.SelectRuins(habitat22);
                                }
                            }
                        }
                        if (galaxy.Age > 0)
                        {
                            foreach (Habitat habitat23 in galaxy.Habitats)
                            {
                                if (habitat23.Ruin != null && habitat23.Ruin.Type != RuinType.UnlockResearchProject && galaxy.Systems[habitat23.SystemIndex].DominantEmpire != null && galaxy.Systems[habitat23.SystemIndex].DominantEmpire.Empire != null)
                                {
                                    habitat23.Ruin.ClearBonuses();
                                    if (galaxy.Systems[habitat23.SystemIndex].DominantEmpire.Empire == galaxy.PlayerEmpire)
                                    {
                                        habitat23.Ruin.PlayerEmpireEncountered = true;
                                    }
                                }
                            }
                        }
                        foreach (Empire empire9 in galaxy.Empires)
                        {
                            SystemInfo systemInfo2 = galaxy.Systems[empire9.Capital.SystemIndex];
                            if (systemInfo2.Creatures == null || systemInfo2.Creatures.Count <= 0)
                            {
                                continue;
                            }
                            CreatureList creatureList = new CreatureList();
                            foreach (Creature creature in systemInfo2.Creatures)
                            {
                                creatureList.Add(creature);
                            }
                            foreach (Creature item10 in creatureList)
                            {
                                item10.CompleteTeardown();
                            }
                        }
                        if (galaxy.Age == 0)
                        {
                            foreach (Empire empire10 in galaxy.Empires)
                            {
                                if (empire10.Capital == null)
                                {
                                    continue;
                                }
                                Habitat habitat14 = Galaxy.DetermineHabitatSystemStar(empire10.Capital);
                                double num48 = Galaxy.Rnd.Next(6000, 14000);
                                double num49 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                                double num50 = habitat14.Xpos + Math.Cos(num49) * num48;
                                double num51 = habitat14.Ypos + Math.Sin(num49) * num48;
                                HabitatList habitatList2 = galaxy.GenerateAsteroidField(Galaxy.Rnd.Next(80, 150), num50, num51, habitat14, orbitDirection: true, 2, (int)num48, 1.0, 1.0, HabitatType.BarrenRock);
                                bool lockTaken3 = false;
                                try
                                {
                                    Monitor.Enter(lockObject = galaxy._LockObject, ref lockTaken3);
                                    galaxy.AddAsteroidField(habitatList2, habitat14);
                                }
                                finally
                                {
                                    if (lockTaken3)
                                    {
                                        Monitor.Exit(lockObject);
                                    }
                                }
                                for (int num52 = 0; num52 < habitatList2.Count; num52++)
                                {
                                    empire10.ResourceMap.SetResourcesKnown(habitatList2[num52], known: false);
                                }
                                if (double_2 > 0.0)
                                {
                                    for (int num53 = 0; num53 < 2; num53++)
                                    {
                                        int index2 = Galaxy.Rnd.Next(0, habitatList2.Count);
                                        galaxy.GenerateCreatureAtHabitat(CreatureType.RockSpaceSlug, habitatList2[index2], lockLocation: true, 0, 0);
                                    }
                                }
                                int num54 = 0;
                                int num55 = 0;
                                SystemInfo systemInfo3 = galaxy.Systems[habitat14];
                                if (systemInfo3 != null && systemInfo3.Habitats != null)
                                {
                                    for (int num56 = 0; num56 < systemInfo3.Habitats.Count; num56++)
                                    {
                                        Habitat habitat15 = systemInfo3.Habitats[num56];
                                        if (habitat15 == null)
                                        {
                                            continue;
                                        }
                                        switch (habitat15.Type)
                                        {
                                            case HabitatType.BarrenRock:
                                                if (habitat15.Category == HabitatCategoryType.Planet && num55 <= 0)
                                                {
                                                    galaxy.SetScenicFactor(habitat15, definitelySet: true);
                                                    num55++;
                                                }
                                                break;
                                            case HabitatType.Volcanic:
                                            case HabitatType.Desert:
                                            case HabitatType.MarshySwamp:
                                            case HabitatType.Ocean:
                                            case HabitatType.Ice:
                                                if ((habitat15.Category == HabitatCategoryType.Planet || habitat15.Category == HabitatCategoryType.Moon) && num55 <= 0)
                                                {
                                                    galaxy.SetScenicFactor(habitat15, definitelySet: true);
                                                    num55++;
                                                }
                                                break;
                                            case HabitatType.FrozenGasGiant:
                                                if (num54 <= 0)
                                                {
                                                    galaxy.SetResearchBonus(habitat15, definitelySet: true);
                                                    num54++;
                                                }
                                                break;
                                        }
                                    }
                                }
                                Design design = empire10.GenerateDesignFromSpec(empire10.DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Frigate), 1.0);
                                design.PictureRef = ShipImageHelper.ResolveMinorShipImageIndex(design.SubRole, largeShips: false);
                                design.Components.RemoveAllByComponentCategory(ComponentCategoryType.HyperDrive);
                                design.ReDefine();
                                Habitat habitat16 = galaxy.FindNearestHabitat(empire10.Capital.Xpos, empire10.Capital.Ypos, HabitatType.FrozenGasGiant);
                                if (habitat16 == null || habitat16.SystemIndex != habitat14.SystemIndex)
                                {
                                    habitat16 = galaxy.FindNearestHabitat(empire10.Capital.Xpos, empire10.Capital.Ypos, HabitatType.GasGiant);
                                }
                                if (habitat16 != null && habitat16.SystemIndex == habitat14.SystemIndex)
                                {
                                    BuiltObject builtObject = galaxy.GenerateAbandonedBuiltObject(habitat16, design, allowCreatures: false, allowNegativeEffects: false, BuiltObjectEncounterAction.Prompt);
                                    builtObject.EncounterEventType = BuiltObjectEncounterEventType.Acquire;
                                    builtObject.EncounterDescription = TextResolver.GetText("PreWarp Abandoned Ship Encounter");
                                }
                                Design design2 = empire10.GenerateDesignFromSpec(empire10.DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Destroyer), 3.0);
                                design2.Components.RemoveAllByComponentCategory(ComponentCategoryType.HyperDrive);
                                design2.ReDefine();
                                habitat16 = galaxy.FindNearestRuin(empire10.Capital.Xpos, empire10.Capital.Ypos);
                                if (habitat16 != null && habitat16.SystemIndex == habitat14.SystemIndex)
                                {
                                    BuiltObject builtObject2 = galaxy.GenerateAbandonedBuiltObject(habitat16, design2, allowCreatures: false, allowNegativeEffects: false, BuiltObjectEncounterAction.Prompt);
                                    galaxy.DamageBuiltObjectComponents(builtObject2, 0.5);
                                    builtObject2.EncounterEventType = BuiltObjectEncounterEventType.Acquire;
                                    switch (Galaxy.Rnd.Next(0, 3))
                                    {
                                        case 0:
                                            builtObject2.EncounterExplorationBonus = (short)Galaxy.Rnd.Next(3, 5);
                                            break;
                                        case 1:
                                            builtObject2.EncounterMoneyBonus = (int)(2000.0 + Galaxy.Rnd.NextDouble() * 2000.0);
                                            break;
                                        case 2:
                                            builtObject2.EncounterTechAdvanceCount = 1;
                                            break;
                                    }
                                }
                                Habitat habitat17 = systemInfo3.Habitats.GetFurthestHabitat(empire10.Capital.Xpos, empire10.Capital.Ypos, HabitatType.Volcanic, 4000.0);
                                if (habitat17 == null)
                                {
                                    habitat17 = systemInfo3.Habitats.GetFurthestHabitat(empire10.Capital.Xpos, empire10.Capital.Ypos, HabitatType.Desert, 4000.0);
                                    if (habitat17 == null)
                                    {
                                        habitat17 = systemInfo3.Habitats.GetFurthestHabitat(empire10.Capital.Xpos, empire10.Capital.Ypos, HabitatType.Ice, 4000.0);
                                        if (habitat17 == null || habitat17.Category == HabitatCategoryType.Asteroid)
                                        {
                                            habitat17 = systemInfo3.Habitats.GetFurthestHabitat(empire10.Capital.Xpos, empire10.Capital.Ypos, HabitatType.MarshySwamp, 4000.0);
                                            if (habitat17 == null)
                                            {
                                                habitat17 = systemInfo3.Habitats.GetFurthestHabitat(empire10.Capital.Xpos, empire10.Capital.Ypos, HabitatType.BarrenRock, 4000.0);
                                                if (habitat17 == null || habitat17.Category == HabitatCategoryType.Asteroid)
                                                {
                                                    habitat17 = null;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (habitat17 != null && habitat17.Ruin == null)
                                {
                                    galaxy.SelectRuins(habitat17, definitePlacement: true, assignCreatures: false, allowNegativeEffects: false, allowMapReveal: false);
                                }
                            }
                        }
                        if (empireStart_0.TechLevel != 0.0 && galaxy.StoryReturnOfTheShakturiEnabled)
                        {
                            double num57 = 0.0;
                            double num58 = 0.0;
                            galaxy.ObtainRandomGalaxyCoordinatesFromPoint(xpos, ypos, (double)Galaxy.SectorSize * 2.0, out num57, out num58);
                            Habitat habitat18 = galaxy.FindNearestHabitatUnoccupiedSystem(num57, num58, HabitatType.Continental);
                            if (habitat18 == null)
                            {
                                habitat18 = galaxy.FindNearestHabitatUnoccupiedSystem(num57, num58, HabitatType.MarshySwamp);
                            }
                            if (habitat18 == null)
                            {
                                habitat18 = galaxy.FindNearestHabitatUnoccupiedSystem(num57, num58, HabitatType.Desert);
                            }
                            if (habitat18 == null)
                            {
                                habitat18 = galaxy.FindNearestHabitatUnoccupiedSystem(num57, num58, HabitatType.Ocean);
                            }
                            if (habitat18 == null)
                            {
                                habitat18 = galaxy.FindNearestHabitatUnoccupiedSystem(num57, num58, HabitatType.Ice);
                            }
                            if (habitat18 == null)
                            {
                                habitat18 = galaxy.FindNearestHabitatUnoccupiedSystem(num57, num58, HabitatType.Volcanic);
                            }
                            if (habitat18 == null)
                            {
                                habitat18 = galaxy.FindNearestHabitatUnoccupiedSystem(num57, num58, HabitatType.BarrenRock);
                            }
                            if (habitat18.Population != null && habitat18.Population.Count > 0)
                            {
                                habitat18.Population.Clear();
                                habitat18.Population.RecalculateTotalAmount();
                            }
                            habitat18.Name = "Utopia";
                            galaxy.GenerateAncientHelpers(habitat18);
                        }
                        GalaxyLocationList galaxyLocationList3 = new GalaxyLocationList();
                        if (galaxy.StoryDistantWorldsEnabled && (gameStartResets_0 == null || string.IsNullOrEmpty(gameStartResets_0.GalaxyFilepath) || galaxy.StoryClueLocations == null || galaxy.StoryClueLocations.Count == 0 || gameStartResets_0.ResetSpecialLocationsAndAbandonedShips))
                        {
                            int num59 = 0;
                            double num60 = 0.0;
                            double num61 = 0.0;
                            galaxy.SelectRelativeParkingPoint(2000000.0, out num60, out num61);
                            num60 += xpos;
                            num61 += ypos;
                            Habitat habitat19 = null;
                            while (habitat19 == null && num59 < 50)
                            {
                                habitat19 = galaxy.FindLonelyHabitat(num60, num61, HabitatType.BarrenRock);
                                method_93(galaxy, 0.0, 1.0, out num60, out num61);
                                num59++;
                            }
                            if (habitat19 != null)
                            {
                                galaxy.SelectRuins(habitat19, definitePlacement: true, assignCreatures: false, allowNegativeEffects: false);
                                if (habitat19.Ruin != null)
                                {
                                    habitat19.Ruin.ClearBonuses();
                                    habitat19.Ruin.StoryClueLevel = 0;
                                }
                            }
                            galaxy.StoryClueLocations.Add(habitat19);
                            galaxy.FindLonelyDeepSpaceLocation(out num60, out num61);
                            DesignSpecification monitoringStationDesignSpec = galaxy.PlayerEmpire.GetMonitoringStationDesignSpec();
                            Design design3 = galaxy.PlayerEmpire.GenerateDesignFromSpec(monitoringStationDesignSpec, 4.0);
                            design3.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(ShipImageHelper.FreedomAllianceFamily, design3.SubRole, aged: false);
                            BuiltObject item3 = galaxy.GenerateStoryAbandonedBuiltObject(num60, num61, design3, TextResolver.GetText("Signal Intercept Station") + " XL5");
                            galaxy.StoryClueLocations.Add(item3);
                            galaxy.FindLonelyNebulaLocation(out num60, out num61, GalaxyLocationEffectType.None);
                            DesignSpecification bySubRole = Galaxy.DesignSpecifications.GetBySubRole(BuiltObjectSubRole.CapitalShip);
                            Design design4 = galaxy.PlayerEmpire.GenerateDesignFromSpec(bySubRole, 4.0);
                            design4.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(ShipImageHelper.FreedomAllianceFamily, design4.SubRole, aged: false);
                            BuiltObject builtObject3 = galaxy.GenerateStoryAbandonedBuiltObject(num60, num61, design4, "Devastator");
                            string message = TextResolver.GetText("You have entered a restricted security zone. You must turn back and leave this area immediately.");
                            GalaxyLocation galaxyLocation = galaxy.GenerateRestrictedZone(string.Format(TextResolver.GetText("NAME Weapons Testing Range"), "Pozdac"), message, 3000.0, num60, num61, 3);
                            galaxyLocationList3.Add(galaxyLocation);
                            galaxy.StoryClueLocations.Add(builtObject3);
                            galaxyLocation.RelatedBuiltObject = builtObject3;
                            galaxy.FindLonelyNebulaLocation(out num60, out num61, GalaxyLocationEffectType.None);
                            Design design5 = galaxy.PlayerEmpire.GenerateResearchStationDesign(galaxy.CurrentStarDate, ComponentType.LabsWeaponsLab);
                            design5.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(ShipImageHelper.FreedomAllianceFamily, design5.SubRole, aged: false);
                            BuiltObject builtObject4 = galaxy.GenerateStoryAbandonedBuiltObject(num60, num61, design5, string.Format(TextResolver.GetText("NAME Special Projects Outpost"), "Ecatur"));
                            string message2 = TextResolver.GetText("You have entered a high-security area used for secret research experiments. Leave now, while you still can!");
                            GalaxyLocation galaxyLocation2 = galaxy.GenerateRestrictedZone(TextResolver.GetText("Dead Zone"), message2, 3000.0, num60, num61, 1);
                            galaxyLocationList3.Add(galaxyLocation2);
                            galaxy.StoryClueLocations.Add(builtObject4);
                            galaxyLocation2.RelatedCreatures = galaxy.GenerateCreaturesAtLocation(CreatureType.Kaltor, 40, num60, num61, 600, 300);
                            galaxyLocation2.RelatedBuiltObject = builtObject4;
                            galaxy.FindLonelyNebulaLocation(out num60, out num61, GalaxyLocationEffectType.LightningDamage, GalaxyLocationEffectType.None);
                            DesignSpecification bySubRole2 = Galaxy.DesignSpecifications.GetBySubRole(BuiltObjectSubRole.GasMiningStation);
                            Design design6 = galaxy.PlayerEmpire.GenerateDesignFromSpec(bySubRole2, 5.0);
                            design6 = galaxy.DesignPirateBase(design6, 5.0);
                            design6.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(ShipImageHelper.FreedomAllianceFamily, design6.SubRole, aged: false);
                            BuiltObject item4 = galaxy.GenerateStoryAbandonedBuiltObject(num60, num61, design6, TextResolver.GetText("Scoundrels Refuge"));
                            galaxy.StoryClueLocations.Add(item4);
                            galaxy.StoryClueUsed.AddRange(new bool[5]);
                            galaxy.StorySecondaryClueUsed.AddRange(new bool[9]);
                            int num62 = Math.Max(1, galaxy.StarCount / 200);
                            for (int num63 = 0; num63 < num62; num63++)
                            {
                                galaxy.GenerateSpecialZoneWeaponsTestingRange();
                            }
                            int num64 = Math.Max(1, galaxy.StarCount / 200);
                            for (int num65 = 0; num65 < num64; num65++)
                            {
                                galaxy.GenerateSpecialZoneResearchFacility();
                            }
                            int num66 = Math.Max(1, galaxy.StarCount / 200);
                            for (int num67 = 0; num67 < num66; num67++)
                            {
                                galaxy.GenerateSpecialZoneSupplyDepot();
                            }
                        }
                        for (int num68 = 0; num68 < empireList.Count; num68++)
                        {
                            Empire empire7 = empireList[num68];
                            if (empire7 != null && empire7.DominantRace != null && empire7.DominantRace.KnownStartingGalacticHistoryLocations > 0)
                            {
                                galaxy.SetEmpireKnownGalacticHistoryLocations(empire7, empire7.DominantRace.KnownStartingGalacticHistoryLocations, empire7.Capital.Xpos, empire7.Capital.Ypos, galaxyLocationList3);
                            }
                        }
                        if (gameStartResets_0 == null || string.IsNullOrEmpty(gameStartResets_0.GalaxyFilepath) || gameStartResets_0.ResetResources)
                        {
                            galaxy.SetRestrictedResources();
                        }
                        if (gameStartResets_0 == null || string.IsNullOrEmpty(gameStartResets_0.GalaxyFilepath) || gameStartResets_0.ResetRuins)
                        {
                            galaxy.GenerateSilverMistRuins();
                        }
                        if (gameStartResets_0 == null || string.IsNullOrEmpty(gameStartResets_0.GalaxyFilepath) || gameStartResets_0.ResetRuins)
                        {
                            galaxy.GenerateSpecialBonusRuins();
                        }
                        if (gameStartResets_0 == null || string.IsNullOrEmpty(gameStartResets_0.GalaxyFilepath) || gameStartResets_0.ResetRuins)
                        {
                            int num69 = Math.Max(1, (int)((double)int_1 / 270.0));
                            int num70 = Math.Max(1, (int)((double)int_1 / 110.0));
                            int num71 = Math.Max(1, (int)((double)int_1 / 170.0));
                            int num72 = Math.Min(6, Math.Max(1, (int)((double)int_1 / 160.0)));
                            int num73 = Math.Max(1, (int)((double)int_1 / 140.0));
                            int num74 = Math.Max(1, Math.Min(2, (int)((double)int_1 / 350.0)));
                            int num75 = Math.Max(1, Math.Min(2, (int)((double)int_1 / 350.0)));
                            Habitat habitat20 = null;
                            bool flag6 = false;
                            for (int num76 = 0; num76 < num74; num76++)
                            {
                                while (!flag6)
                                {
                                    habitat20 = galaxy.FindLonelyHabitat(RuinType.Government, HabitatType.BarrenRock);
                                    flag6 = galaxy.SelectSpecialRuins(habitat20, EventMessageType.SpecialGovernmentType);
                                }
                                flag6 = false;
                                while (!flag6)
                                {
                                    habitat20 = galaxy.FindLonelyHabitat(RuinType.Government, HabitatType.BarrenRock);
                                    flag6 = galaxy.SelectSpecialRuins(habitat20, EventMessageType.SpecialGovernmentType);
                                }
                            }
                            flag6 = false;
                            for (int num77 = 0; num77 < num75; num77++)
                            {
                                while (!flag6)
                                {
                                    habitat20 = galaxy.FindLonelyHabitat(RuinType.Component, HabitatType.BarrenRock);
                                    flag6 = galaxy.SelectSpecialRuins(habitat20, EventMessageType.ExoticTechDiscovered);
                                }
                                flag6 = false;
                                while (!flag6)
                                {
                                    habitat20 = galaxy.FindLonelyHabitat(RuinType.Component, HabitatType.BarrenRock);
                                    flag6 = galaxy.SelectSpecialRuins(habitat20, EventMessageType.ExoticTechDiscovered);
                                }
                            }
                            flag6 = false;
                            for (int num78 = 0; num78 < num69; num78++)
                            {
                                habitat20 = galaxy.FindLonelyHabitatGalacticEdge(RuinType.Refugees, HabitatType.BarrenRock);
                                galaxy.SelectSpecialRuins(habitat20, EventMessageType.GalacticRefugees);
                            }
                            for (int num79 = 0; num79 < num70; num79++)
                            {
                                habitat20 = galaxy.FindLonelyHabitat(RuinType.LostBuiltObject);
                                galaxy.SelectSpecialRuins(habitat20, EventMessageType.LostBuiltObjectCoordinates);
                            }
                            for (int num80 = 0; num80 < num71; num80++)
                            {
                                habitat20 = galaxy.FindLonelyHabitat(RuinType.LostColony);
                                galaxy.SelectSpecialRuins(habitat20, EventMessageType.LostColonyCoordinates);
                            }
                            if (galaxy.StoryDistantWorldsEnabled)
                            {
                                for (int num81 = 0; num81 < num72; num81++)
                                {
                                    Race race2 = null;
                                    int specialValue = Galaxy.Rnd.Next(10, 14);
                                    switch (num81)
                                    {
                                        case 0:
                                            race2 = galaxy.Races["Human"];
                                            specialValue = Galaxy.Rnd.Next(10, 14);
                                            break;
                                        case 1:
                                            race2 = galaxy.Races["Boskara"];
                                            specialValue = Galaxy.Rnd.Next(10, 14) * -1;
                                            break;
                                        case 2:
                                            race2 = galaxy.Races["Kiadian"];
                                            specialValue = Galaxy.Rnd.Next(10, 14);
                                            break;
                                        case 3:
                                            race2 = galaxy.Races["Sluken"];
                                            specialValue = Galaxy.Rnd.Next(10, 14) * -1;
                                            break;
                                        case 4:
                                            race2 = galaxy.Races["Ackdarian"];
                                            specialValue = Galaxy.Rnd.Next(10, 14);
                                            break;
                                        case 5:
                                            race2 = galaxy.Races["Gizurean"];
                                            specialValue = Galaxy.Rnd.Next(10, 14) * -1;
                                            break;
                                    }
                                    if (race2 == null)
                                    {
                                        race2 = galaxy.SelectRandomRace(75);
                                    }
                                    habitat20 = galaxy.FindLonelyHabitat(RuinType.Origins, HabitatType.BarrenRock);
                                    galaxy.SelectSpecialRuins(habitat20, EventMessageType.OriginsDiscovery, race2, specialValue);
                                }
                            }
                            for (int num82 = 0; num82 < num73; num82++)
                            {
                                habitat20 = galaxy.FindLonelyHabitat(RuinType.NewPopulation, HabitatType.BarrenRock);
                                galaxy.SelectSpecialRuins(habitat20, EventMessageType.SleepersAwake);
                            }
                        }
                        GalaxyLocationList galaxyLocationList4 = galaxy.GalaxyLocations.FindLocations(GalaxyLocationType.DebrisField);
                        if (galaxy.StoryDistantWorldsEnabled && (gameStartResets_0 == null || string.IsNullOrEmpty(gameStartResets_0.GalaxyFilepath) || galaxyLocationList4.Count == 0 || gameStartResets_0.ResetSpecialLocationsAndAbandonedShips))
                        {
                            int num83 = 1;
                            int num84 = 1;
                            int num85 = 1;
                            if (int_1 >= 1400)
                            {
                                num83 = 3;
                                num84 = 5;
                                num85 = 3;
                            }
                            else if (int_1 >= 700)
                            {
                                num83 = 2;
                                num84 = 3;
                                num85 = 2;
                            }
                            else if (int_1 >= 400)
                            {
                                num83 = 1;
                                num84 = 3;
                                num85 = 1;
                            }
                            else
                            {
                                num83 = 0;
                                num84 = 2;
                                num85 = 1;
                            }
                            for (int num86 = 0; num86 < num83; num86++)
                            {
                                galaxy.GenerateDebrisFieldLarge();
                            }
                            for (int num87 = 0; num87 < num84; num87++)
                            {
                                galaxy.GenerateDebrisFieldSmall();
                            }
                            for (int num88 = 0; num88 < num85; num88++)
                            {
                                galaxy.GeneratePlanetDestroyer();
                            }
                        }
                        galaxy = method_87(galaxy);
                        galaxy = method_85(galaxy);
                        if (galaxy.StoryReturnOfTheShakturiEnabled)
                        {
                            method_86(galaxy);
                        }
                        galaxy.DeferEventsForGameStart = true;
                        game2 = new Game();
                        game2.Version = Application.ProductVersion;
                        if (empireStart_0.Age == 0)
                        {
                            bool_3 = true;
                        }
                        game2.PlayAsAPirate = bool_2;
                        game2.AgeOfShadows = bool_3;
                        galaxy.GlobalVictoryConditions = victoryConditions_0;
                        game2.Galaxy = galaxy;
                        game2.CustomizationSetName = empty;
                        game2.PlayerEmpire = empire2;
                        game2.PlayerEmpire.PlayableInScenario = true;
                        if (!bool_2 && bool_3 && victoryConditions_0.EnableStoryEventsShadows)
                        {
                            game2.PlayerEmpire.PreWarpProgressEventOccurredSendPirateRaid = false;
                        }
                        if (game2.PlayerEmpire.Capital != null)
                        {
                            game2.PlayerEmpire.Capital.DoTasks(galaxy.CurrentDateTime);
                        }
                        break;
                    }
                    race = ((empireStartList_0[num15].RaceIndex >= 0) ? galaxy.Races[empireStartList_0[num15].RaceIndex] : ((empireStartList_0[num15].ResolvedRace == null) ? method_48(galaxy, empireStartList_0[num15].Race, empireList, bool_5: false) : method_48(galaxy, empireStartList_0[num15].ResolvedRace.Name, empireList, bool_5: false)));
                    if (empireStartList_0[num15].GovernmentStyle == "(" + TextResolver.GetText("Random") + ")")
                    {
                        List<int> list12 = Empire.ResolveDefaultAllowableGovernmentTypes(race);
                        GovernmentAttributesList governmentAttributesList = Empire.DetermineMostSuitableGovermentTypes(race, list12);
                        if (governmentAttributesList != null && governmentAttributesList.Count > 0)
                        {
                            int index3 = Galaxy.Rnd.Next(0, governmentAttributesList.Count);
                            governmentAttributes = governmentAttributesList[index3];
                            if (governmentAttributes.SpecialFunctionCode == 1)
                            {
                                int num89 = 0;
                                while (governmentAttributes.SpecialFunctionCode == 1 && num89 < 10)
                                {
                                    num2 = Galaxy.ResolveGovernmentId(empireStart_0.GovernmentStyle, race);
                                    governmentAttributes = galaxy.Governments[num2];
                                    num89++;
                                }
                            }
                            if (race.PreferredStartingGovernmentId >= 0 && list12.Contains(race.PreferredStartingGovernmentId))
                            {
                                governmentAttributes = Galaxy.GovernmentsStatic[race.PreferredStartingGovernmentId];
                            }
                        }
                    }
                    else
                    {
                        governmentAttributes = Galaxy.GovernmentsStatic.GetByName(empireStartList_0[num15].GovernmentStyle);
                    }
                    num2 = governmentAttributes.GovernmentId;
                    Sector sector_ = null;
                    method_53(galaxy, empireStartList_0[num15].ProximityDistance, out sector_);
                    Galaxy.Rnd.NextDouble();
                    Galaxy.ResolveHomeSystem(empireStartList_0[num15].HomeSystemFavourability, out capitalHabitatType, out homeSystemFactor);
                    capitalHabitatType = race.NativeHabitatType;
                    Habitat habitat21 = method_51(galaxy, race, empireStartList_0[num15].ProximityDistance, habitat, capitalHabitatType, bool_2, num14 + 1, sector_);
                    if (habitat21 != null)
                    {
                        designPictureFamilyIndex = race.DesignPictureFamilyIndex;
                        if (empireStartList_0[num15].DesignPictureFamilyIndex >= 0)
                        {
                            designPictureFamilyIndex = empireStartList_0[num15].DesignPictureFamilyIndex;
                        }
                        double expansion2 = 0.0;
                        double actualTechLevel = 1.0;
                        string raceNameOverride = string.Empty;
                        if (race != null)
                        {
                            raceNameOverride = race.Name;
                        }
                        Empire empire8 = galaxy.GenerateEmpire(galaxy, isPlayerEmpire: false, empireStartList_0[num15].Name, habitat21, race, designPictureFamilyIndex, num2, homeSystemFactor, empireStartList_0[num15].HomeSystemFavourability, empireStartList_0[num15].Age, empireStartList_0[num15].TechLevel, empireStartList_0[num15].CorruptionMultiplier, out expansion2, main_0.gameOptions_0, victoryConditions_0, out actualTechLevel, raceNameOverride);
                        empireStartList_0[num15].TechLevel = actualTechLevel;
                        if (empireStartList2[num15].Age == 0)
                        {
                            galaxy.ClearIndependentColoniesFromSystem(habitat21.SystemIndex);
                        }
                        galaxy.SetEmpireDifficultyFactors(empire8);
                        if (empireStartList_0[num15].FlagShape >= 0)
                        {
                            Bitmap smallFlagPicture2 = null;
                            Bitmap largeFlagPicture2 = null;
                            empire8.FlagShape = Galaxy.GenerateEmpireFlag(empire8.MainColor, empire8.SecondaryColor, empireStartList_0[num15].FlagShape, list, ref smallFlagPicture2, ref largeFlagPicture2);
                            empire8.SmallFlagPicture = smallFlagPicture2;
                            empire8.LargeFlagPicture = largeFlagPicture2;
                            Bitmap bitmap4 = (empire8.MediumFlagPicture = GraphicsHelper.ScaleImage(largeFlagPicture2, 35, 21, 1f, lowQuality: false));
                        }
                        empireList.Add(empire8);
                        list3.Add(expansion2);
                        list6.Add(empireStartList_0[num15].Age);
                        num15++;
                        continue;
                    }
                    throw new ApplicationException("Could not locate capital!");
                }
                game2.ViewX = xpos;
                game2.ViewY = ypos;
                game2.ZoomFactor = 1.0;
                game2.GlobalVictoryConditions = victoryConditions_0;
                game2.PlayerVictoryConditionsToAchieve = empireVictoryConditions_0;
                game2.PlayerVictoryConditionsToPrevent = empireVictoryConditions_1;
                game2.AutoPauseWhenInPopupWindow = main_0.gameOptions_0.AutoPauseWhenInPopupWindow;
                game2.PlayerEmpire.ControlColonization = main_0.gameOptions_0.ControlColonizationDefault;
                game2.PlayerEmpire.ControlColonyTaxRates = main_0.gameOptions_0.ControlColonyTaxRatesDefault;
                game2.PlayerEmpire.ControlDesigns = main_0.gameOptions_0.ControlShipDesignDefault;
                game2.PlayerEmpire.ControlDiplomacyGifts = main_0.gameOptions_0.ControlDiplomaticGiftsDefault;
                game2.PlayerEmpire.ControlDiplomacyOffense = main_0.gameOptions_0.ControlWarTradeSanctionsDefault;
                game2.PlayerEmpire.ControlDiplomacyTreaties = main_0.gameOptions_0.ControlTreatyNegotiationDefault;
                game2.PlayerEmpire.ControlMilitaryAttacks = main_0.gameOptions_0.ControlAttacksOnEnemiesDefault;
                game2.PlayerEmpire.ControlMilitaryFleets = main_0.gameOptions_0.ControlFleetFormationDefault;
                game2.PlayerEmpire.ControlStateConstruction = main_0.gameOptions_0.ControlShipBuildingDefault;
                game2.PlayerEmpire.ControlTroopGeneration = main_0.gameOptions_0.ControlTroopRecruitmentDefault;
                game2.PlayerEmpire.ControlAgentAssignment = main_0.gameOptions_0.ControlAgentAssignmentDefault;
                game2.PlayerEmpire.ControlResearch = main_0.gameOptions_0.ControlResearchDefault;
                game2.PlayerEmpire.ControlColonyFacilities = main_0.gameOptions_0.ControlColonyFacilitiesDefault;
                game2.PlayerEmpire.ControlCharacterLocations = main_0.gameOptions_0.ControlCharacterLocationsDefault;
                game2.PlayerEmpire.ControlPopulationPolicy = main_0.gameOptions_0.ControlPopulationPolicyDefault;
                game2.PlayerEmpire.ControlOfferPirateMissions = main_0.gameOptions_0.ControlOfferPirateMissionsDefault;
                game2.PlayerEmpire.AttackOvermatchFactor = main_0.gameOptions_0.AttackOverMatchFactor;
                game2.PlayerEmpire.AttackRangePatrol = main_0.gameOptions_0.AttackRangePatrol;
                game2.PlayerEmpire.AttackRangeEscort = main_0.gameOptions_0.AttackRangeEscort;
                game2.PlayerEmpire.AttackRangeOther = main_0.gameOptions_0.AttackRangeOther;
                game2.PlayerEmpire.FleetAttackRefuelPortion = main_0.gameOptions_0.FleetAttackRefuelPortion;
                game2.PlayerEmpire.FleetAttackGatherPortion = main_0.gameOptions_0.FleetAttackGatherPortion;
                game2.PlayerEmpire.DiscoveryActionRuin = main_0.gameOptions_0.DiscoveryActionRuin;
                game2.PlayerEmpire.DiscoveryActionAbandonedShipBase = main_0.gameOptions_0.DiscoveryActionAbandonedShipBase;
                game2.PlayerEmpire.NewShipsAutomated = main_0.gameOptions_0.NewShipsAutomated;
                game2.DisplayMessageBuiltObjectBuilt = main_0.gameOptions_0.DisplayMessageBuiltObjectBuilt;
                game2.DisplayMessageColonyInvaded = main_0.gameOptions_0.DisplayMessageColonyInvaded;
                game2.DisplayMessageDiplomacyEmpireMetDestroyed = main_0.gameOptions_0.DisplayMessageDiplomacyEmpireMetDestroyed;
                game2.DisplayMessageDiplomacyGift = main_0.gameOptions_0.DisplayMessageDiplomacyGift;
                game2.DisplayMessageDiplomacyRequestWarning = main_0.gameOptions_0.DisplayMessageDiplomacyRequestWarning;
                game2.DisplayMessageDiplomacyTreaty = main_0.gameOptions_0.DisplayMessageDiplomacyTreaty;
                game2.DisplayMessageDiplomacyWarTradeSanctions = main_0.gameOptions_0.DisplayMessageDiplomacyWarTradeSanctions;
                game2.DisplayMessageNewColony = main_0.gameOptions_0.DisplayMessageNewColony;
                game2.DisplayMessageResearchNewComponent = main_0.gameOptions_0.DisplayMessageResearchNewComponent;
                game2.DisplayMessageIntelligenceMissions = main_0.gameOptions_0.DisplayMessageIntelligenceMissions;
                game2.DisplayMessageExploration = main_0.gameOptions_0.DisplayMessageExploration;
                game2.DisplayMessageShipMissionComplete = main_0.gameOptions_0.DisplayMessageShipMissionComplete;
                game2.DisplayMessageShipNeedsRefuelling = main_0.gameOptions_0.DisplayMessageShipNeedsRefuelling;
                game2.DisplayMessageConstructionResourceShortage = main_0.gameOptions_0.DisplayMessageConstructionResourceShortage;
                game2.DisplayPopupBuiltObjectBuilt = main_0.gameOptions_0.DisplayPopupBuiltObjectBuilt;
                game2.DisplayPopupColonyInvaded = main_0.gameOptions_0.DisplayPopupColonyInvaded;
                game2.DisplayPopupDiplomacyEmpireMetDestroyed = main_0.gameOptions_0.DisplayPopupDiplomacyEmpireMetDestroyed;
                game2.DisplayPopupDiplomacyGift = main_0.gameOptions_0.DisplayPopupDiplomacyGift;
                game2.DisplayPopupDiplomacyRequestWarning = main_0.gameOptions_0.DisplayPopupDiplomacyRequestWarning;
                game2.DisplayPopupDiplomacyTreaty = main_0.gameOptions_0.DisplayPopupDiplomacyTreaty;
                game2.DisplayPopupDiplomacyWarTradeSanctions = main_0.gameOptions_0.DisplayPopupDiplomacyWarTradeSanctions;
                game2.DisplayPopupNewColony = main_0.gameOptions_0.DisplayPopupNewColony;
                game2.DisplayPopupResearchNewComponent = main_0.gameOptions_0.DisplayPopupResearchNewComponent;
                game2.DisplayPopupIntelligenceMissions = main_0.gameOptions_0.DisplayPopupIntelligenceMissions;
                game2.DisplayPopupExploration = main_0.gameOptions_0.DisplayPopupExploration;
                game2.DisplayPopupShipMissionComplete = main_0.gameOptions_0.DisplayPopupShipMissionComplete;
                game2.DisplayPopupShipNeedsRefuelling = main_0.gameOptions_0.DisplayPopupShipNeedsRefuelling;
                game2.DisplayPopupConstructionResourceShortage = main_0.gameOptions_0.DisplayPopupConstructionResourceShortage;
                game2.DisplayMessageUnderAttackCivilianBases = main_0.gameOptions_0.DisplayMessageUnderAttackCivilianBases;
                game2.DisplayMessageUnderAttackCivilianShips = main_0.gameOptions_0.DisplayMessageUnderAttackCivilianShips;
                game2.DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases = main_0.gameOptions_0.DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases;
                game2.DisplayMessageUnderAttackColonyConstructionShips = main_0.gameOptions_0.DisplayMessageUnderAttackColonyConstructionShips;
                game2.DisplayMessageUnderAttackExplorationShips = main_0.gameOptions_0.DisplayMessageUnderAttackExplorationShips;
                game2.DisplayMessageUnderAttackMilitaryShips = main_0.gameOptions_0.DisplayMessageUnderAttackMilitaryShips;
                game2.DisplayMessageUnderAttackOtherStateBases = main_0.gameOptions_0.DisplayMessageUnderAttackOtherStateBases;
                game2.DisplayPopupUnderAttackCivilianBases = main_0.gameOptions_0.DisplayPopupUnderAttackCivilianBases;
                game2.DisplayPopupUnderAttackCivilianShips = main_0.gameOptions_0.DisplayPopupUnderAttackCivilianShips;
                game2.DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases = main_0.gameOptions_0.DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases;
                game2.DisplayPopupUnderAttackColonyConstructionShips = main_0.gameOptions_0.DisplayPopupUnderAttackColonyConstructionShips;
                game2.DisplayPopupUnderAttackExplorationShips = main_0.gameOptions_0.DisplayPopupUnderAttackExplorationShips;
                game2.DisplayPopupUnderAttackMilitaryShips = main_0.gameOptions_0.DisplayPopupUnderAttackMilitaryShips;
                game2.DisplayPopupUnderAttackOtherStateBases = main_0.gameOptions_0.DisplayPopupUnderAttackOtherStateBases;
                game2.MainViewScrollSpeed = main_0.gameOptions_0.MainViewScrollSpeed;
                game2.MainViewZoomSpeed = main_0.gameOptions_0.MainViewZoomSpeed;
                game2.MusicVolume = main_0.gameOptions_0.MusicVolume;
                game2.SoundEffectsVolume = main_0.gameOptions_0.SoundEffectsVolume;
                game2.StarFieldSize = main_0.gameOptions_0.StarFieldSize;
                game2.ShowSystemNebulae = main_0.gameOptions_0.ShowSystemNebulae;
                game2.MouseScrollWheelBehaviour = main_0.gameOptions_0.MouseScrollWheelBehaviour;
                return game2;
            }
            catch (Exception ex)
            {
                Exception ex2 = (exception_0 = ex);
            }
            return null;
        }

        private void method_83(Galaxy galaxy_0, Race race_0, double double_1, double double_2, out double double_3, out double double_4)
        {
            method_84(galaxy_0, race_0, double_1, double_2, out double_3, out double_4, bool_5: true, 1.0);
        }

        private void method_84(Galaxy galaxy_0, Race race_0, double double_1, double double_2, out double double_3, out double double_4, bool bool_5, double double_5)
        {
            GalaxyLocation galaxyLocation = galaxy_0.DetermineRaceRegion(race_0);
            if (galaxyLocation != null)
            {
                galaxy_0.CalculateGalaxyLocationMidPoint(galaxyLocation, out double_3, out double_4);
                double num = (double)galaxyLocation.Width / 2.0;
                num *= double_5;
                if (bool_5)
                {
                    double_3 += num * 2.0 * Galaxy.Rnd.NextDouble() - num;
                    double_4 += num * 2.0 * Galaxy.Rnd.NextDouble() - num;
                }
            }
            else
            {
                method_93(galaxy_0, double_1, double_2, out double_3, out double_4);
            }
        }

        private Galaxy method_85(Galaxy galaxy_0)
        {
            int num = galaxy_0.AsteroidFields.Count / 4;
            bool[] array = new bool[galaxy_0.AsteroidFields.Count];
            int num2 = 0;
            int num3 = 0;
            Design design = null;
            for (int i = 0; i < num; i++)
            {
                bool flag = false;
                Habitat habitat = null;
                int num4 = 0;
                while (!flag && num4 < 50)
                {
                    int num5 = Galaxy.Rnd.Next(0, galaxy_0.AsteroidFields.Count);
                    habitat = null;
                    flag = false;
                    if (!array[num5])
                    {
                        int index = Galaxy.Rnd.Next(0, galaxy_0.AsteroidFields[num5].Count);
                        habitat = galaxy_0.AsteroidFields[num5][index];
                        flag = true;
                        array[num5] = true;
                    }
                    num4++;
                }
                if (habitat == null)
                {
                    continue;
                }
                num2 = Galaxy.Rnd.Next(0, galaxy_0.Empires.Count);
                num3 = Galaxy.Rnd.Next(0, 4);
                string text = string.Empty;
                Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                BuiltObjectSubRole subRole;
                if (galaxy_0.Empires.Count <= 0)
                {
                    num3 = 3;
                    subRole = BuiltObjectSubRole.Undefined;
                    //int num6 = 3;
                }
                else
                {
                    subRole = BuiltObjectSubRole.Undefined;
                    switch (num3)
                    {
                        case 0:
                            design = galaxy_0.Empires[num2].Designs.FindNewest(BuiltObjectSubRole.Frigate);
                            subRole = BuiltObjectSubRole.Frigate;
                            if (design != null && design.WarpSpeed <= 0)
                            {
                                design = null;
                            }
                            text = galaxy_0.SelectRandomUniqueMilitaryShipName(null);
                            goto IL_028e;
                        case 1:
                            design = galaxy_0.Empires[num2].Designs.FindNewest(BuiltObjectSubRole.MiningStation);
                            subRole = BuiltObjectSubRole.MiningStation;
                            text = string.Format(TextResolver.GetText("NAME Mining Station"), habitat2.Name);
                            goto IL_028e;
                        case 2:
                            {
                                design = galaxy_0.Empires[num2].Designs.FindNewest(BuiltObjectSubRole.SmallSpacePort);
                                subRole = BuiltObjectSubRole.SmallSpacePort;
                                text = habitat2.Name + " ";
                                string[] array2 = new string[5]
                                {
                        TextResolver.GetText("Outpost"),
                        TextResolver.GetText("Depot"),
                        TextResolver.GetText("Station"),
                        TextResolver.GetText("Base"),
                        TextResolver.GetText("Facility")
                                };
                                string text2 = array2[Galaxy.Rnd.Next(0, array2.Length)];
                                text += text2;
                                goto IL_028e;
                            }
                        case 3:
                            break;
                        default:
                            goto IL_028e;
                    }
                }
                if (galaxy_0.PirateEmpires != null && galaxy_0.PirateEmpires.Count > 0)
                {
                    int index2 = Galaxy.Rnd.Next(0, galaxy_0.PirateEmpires.Count);
                    design = galaxy_0.PirateEmpires[index2].Designs.FindNewest(BuiltObjectSubRole.SmallSpacePort);
                    text = galaxy_0.GeneratePirateBaseName(null);
                }
                goto IL_028e;
            IL_028e:
                if (design == null)
                {
                    Empire empire = null;
                    for (int j = 0; j < galaxy_0.PirateEmpires.Count; j++)
                    {
                        if (galaxy_0.PirateEmpires[j] != galaxy_0.PlayerEmpire)
                        {
                            empire = galaxy_0.PirateEmpires[j];
                            break;
                        }
                    }
                    if (empire != null)
                    {
                        design = empire.GenerateDesignFromSpec(Galaxy.DesignSpecifications.GetBySubRole(subRole), 2.0);
                    }
                }
                bool flag2 = false;
                BuiltObjectEncounterAction encounterAction = BuiltObjectEncounterAction.Prompt;
                if (Galaxy.Rnd.Next(0, 2) == 1)
                {
                    flag2 = true;
                    encounterAction = BuiltObjectEncounterAction.Notify;
                }
                if (design == null)
                {
                    continue;
                }
                design = design.Clone();
                if (design.SubRole == BuiltObjectSubRole.SmallSpacePort || design.SubRole == BuiltObjectSubRole.MediumSpacePort || design.SubRole == BuiltObjectSubRole.LargeSpacePort)
                {
                    galaxy_0.AddCargoBaysToDesign(design, 10);
                }
                design.PictureRef = ShipImageHelper.ResolveMinorShipImageIndex(design.SubRole, largeShips: true);
                BuiltObject builtObject = galaxy_0.GenerateAbandonedBuiltObject(habitat, design, allowCreatures: true, !flag2, encounterAction);
                builtObject.IsAutoControlled = true;
                if (!string.IsNullOrEmpty(text))
                {
                    builtObject.Name = text;
                }
                if (flag2)
                {
                    int num7 = Galaxy.Rnd.Next(4, builtObject.Components.Count - 1);
                    for (int k = 0; k < num7; k++)
                    {
                        int index3 = Galaxy.Rnd.Next(0, builtObject.Components.Count);
                        builtObject.Components[index3].Status = ComponentStatus.Damaged;
                        builtObject.ReDefine();
                        builtObject.CurrentFuel = (double)builtObject.FuelCapacity * 0.2 + Galaxy.Rnd.NextDouble() * 0.7 * (double)builtObject.FuelCapacity;
                    }
                }
                if (Galaxy.Rnd.Next(0, 3) == 1)
                {
                    switch (Galaxy.Rnd.Next(0, 3))
                    {
                        case 0:
                            builtObject.EncounterExplorationBonus = (short)Galaxy.Rnd.Next(4, 9);
                            break;
                        case 1:
                            builtObject.EncounterMoneyBonus = Galaxy.Rnd.Next(5000, 15000);
                            break;
                        case 2:
                            builtObject.EncounterTechAdvanceCount = 1;
                            break;
                    }
                }
            }
            return galaxy_0;
        }

        private void method_86(Galaxy galaxy_0)
        {
            int num = (int)(Math.Sqrt(galaxy_0.StarCount) * 0.3);
            int num2 = 0;
            int num3 = 0;
            while (num2 < num && num3 < 5000)
            {
                method_93(galaxy_0, 0.0, 1.05, out var double_, out var double_2);
                Habitat habitat = galaxy_0.FindNearestColony(double_, double_2, null, 0, includeIndependentColonies: false);
                Habitat habitat2 = null;
                Design design = null;
                int num4 = Galaxy.Rnd.Next(0, 3);
                DesignSpecification designSpecification = null;
                switch (num4)
                {
                    case 0:
                        designSpecification = Galaxy.DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Frigate);
                        break;
                    case 1:
                        designSpecification = Galaxy.DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Destroyer);
                        break;
                    case 2:
                        designSpecification = Galaxy.DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Cruiser);
                        break;
                }
                if (designSpecification != null)
                {
                    design = galaxy_0.PlayerEmpire.GenerateDesignFromSpec(designSpecification, 7.0);
                    design.Empire = null;
                }
                habitat2 = galaxy_0.FindNearestHabitat(double_, double_2);
                habitat = galaxy_0.FindNearestColony(habitat2.Xpos, habitat2.Xpos, null, 0, includeIndependentColonies: false);
                double num5 = galaxy_0.CalculateDistance(habitat2.Xpos, habitat2.Ypos, habitat.Xpos, habitat.Ypos);
                if (num5 > (double)Galaxy.MaxSolarSystemSize * 2.1 && design != null)
                {
                    bool flag = true;
                    if (design.Role == BuiltObjectRole.Base)
                    {
                        BuiltObject builtObject = galaxy_0.FindNearestBuiltObject((int)habitat2.Xpos, (int)habitat2.Ypos, BuiltObjectRole.Base);
                        double num6 = galaxy_0.CalculateDistance(habitat2.Xpos, habitat2.Ypos, builtObject.Xpos, builtObject.Ypos);
                        if (num6 < 500.0)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        design.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(ShipImageHelper.ShakturiFamily, design.SubRole, aged: false);
                        BuiltObject builtObject2 = galaxy_0.GenerateAbandonedBuiltObject(habitat2, design, allowCreatures: false, allowNegativeEffects: false, BuiltObjectEncounterAction.Prompt);
                        builtObject2.EncounterDescription = TextResolver.GetText("Shakturi Ship Encounter");
                        num2++;
                    }
                }
                num3++;
            }
        }

        private Galaxy method_87(Galaxy galaxy_0)
        {
            int num = (int)(Math.Sqrt(galaxy_0.StarCount) * 0.7);
            int num2 = 0;
            int num3 = 0;
            while (num2 < num && num3 < 5000)
            {
                method_93(galaxy_0, 0.0, 1.05, out var double_, out var double_2);
                Habitat habitat = galaxy_0.FindNearestColony(double_, double_2, null, 0, includeIndependentColonies: false);
                Habitat habitat2 = null;
                Design design = null;
                int num4 = 0;
                int num5 = Galaxy.Rnd.Next(0, 4);
                BuiltObjectSubRole subRole = BuiltObjectSubRole.Undefined;
                Empire empire = null;
                if (galaxy_0.Empires.Count > 0)
                {
                    num4 = Galaxy.Rnd.Next(0, galaxy_0.Empires.Count);
                    empire = galaxy_0.Empires[num4];
                }
                else if (galaxy_0.PirateEmpires.Count > 0)
                {
                    num4 = Galaxy.Rnd.Next(0, galaxy_0.PirateEmpires.Count);
                    empire = galaxy_0.PirateEmpires[num4];
                }
                if (empire != null && empire.Designs != null)
                {
                    switch (num5)
                    {
                        case 0:
                            {
                                Resource resource = null;
                                if (Galaxy.Rnd.Next(0, 2) == 1)
                                {
                                    if (galaxy_0.ResourceSystem.MineralStrategicResources.Count > 0)
                                    {
                                        int index = Galaxy.Rnd.Next(0, galaxy_0.ResourceSystem.MineralStrategicResources.Count);
                                        resource = new Resource(galaxy_0.ResourceSystem.MineralStrategicResources[index].ResourceID);
                                        design = empire.Designs.FindNewest(BuiltObjectSubRole.MiningStation);
                                        subRole = BuiltObjectSubRole.MiningStation;
                                    }
                                }
                                else if (galaxy_0.ResourceSystem.GasStrategicResources.Count > 0)
                                {
                                    int index2 = Galaxy.Rnd.Next(0, galaxy_0.ResourceSystem.GasStrategicResources.Count);
                                    resource = new Resource(galaxy_0.ResourceSystem.GasStrategicResources[index2].ResourceID);
                                    design = empire.Designs.FindNewest(BuiltObjectSubRole.GasMiningStation);
                                    subRole = BuiltObjectSubRole.GasMiningStation;
                                }
                                if (resource != null)
                                {
                                    habitat2 = galaxy_0.FindNearestHabitatWithResource(double_, double_2, resource.ResourceID);
                                }
                                break;
                            }
                        case 1:
                            design = empire.Designs.FindNewest(BuiltObjectSubRole.ColonyShip);
                            if (design != null && design.WarpSpeed <= 0)
                            {
                                design = null;
                            }
                            subRole = BuiltObjectSubRole.ColonyShip;
                            habitat2 = galaxy_0.FindNearestHabitat(double_, double_2);
                            break;
                        case 2:
                            switch (Galaxy.Rnd.Next(0, 3))
                            {
                                case 0:
                                    design = empire.Designs.FindNewest(BuiltObjectSubRole.Destroyer);
                                    subRole = BuiltObjectSubRole.Destroyer;
                                    break;
                                case 1:
                                    design = empire.Designs.FindNewest(BuiltObjectSubRole.Cruiser);
                                    subRole = BuiltObjectSubRole.Cruiser;
                                    break;
                                case 2:
                                    design = empire.Designs.FindNewest(BuiltObjectSubRole.CapitalShip);
                                    subRole = BuiltObjectSubRole.CapitalShip;
                                    break;
                            }
                            if (design != null && design.WarpSpeed <= 0)
                            {
                                design = null;
                            }
                            habitat2 = galaxy_0.FindNearestHabitat(double_, double_2);
                            break;
                        case 3:
                            {
                                DesignSpecification monitoringStationDesignSpec = empire.GetMonitoringStationDesignSpec();
                                design = empire.GenerateDesignFromSpec(monitoringStationDesignSpec, 3.0);
                                subRole = BuiltObjectSubRole.MonitoringStation;
                                design.Name = TextResolver.GetText("Monitoring Station");
                                habitat2 = galaxy_0.FindNearestHabitat(double_, double_2);
                                break;
                            }
                    }
                }
                if (design == null)
                {
                    Empire empire2 = null;
                    for (int i = 0; i < galaxy_0.PirateEmpires.Count; i++)
                    {
                        if (galaxy_0.PirateEmpires[i] != galaxy_0.PlayerEmpire)
                        {
                            empire2 = galaxy_0.PirateEmpires[i];
                            break;
                        }
                    }
                    if (empire2 != null)
                    {
                        design = empire2.GenerateDesignFromSpec(Galaxy.DesignSpecifications.GetBySubRole(subRole), 2.0);
                    }
                }
                if (design != null && habitat2 != null)
                {
                    habitat = galaxy_0.FindNearestColony(habitat2.Xpos, habitat2.Xpos, null, 0, includeIndependentColonies: false);
                    if (habitat != null)
                    {
                        double num6 = galaxy_0.CalculateDistance(habitat2.Xpos, habitat2.Ypos, habitat.Xpos, habitat.Ypos);
                        if (num6 > (double)Galaxy.MaxSolarSystemSize * 2.1)
                        {
                            bool flag = true;
                            if (design.Role == BuiltObjectRole.Base)
                            {
                                BuiltObject builtObject = galaxy_0.FindNearestBuiltObject((int)habitat2.Xpos, (int)habitat2.Ypos, BuiltObjectRole.Base);
                                if (builtObject != null)
                                {
                                    double num7 = galaxy_0.CalculateDistance(habitat2.Xpos, habitat2.Ypos, builtObject.Xpos, builtObject.Ypos);
                                    if (num7 < 500.0)
                                    {
                                        flag = false;
                                    }
                                }
                            }
                            if (flag)
                            {
                                design = design.Clone();
                                design.PictureRef = ShipImageHelper.ResolveMinorShipImageIndex(design.SubRole, largeShips: true);
                                galaxy_0.GenerateAbandonedBuiltObject(habitat2, design);
                                num2++;
                            }
                        }
                    }
                    else
                    {
                        num2++;
                    }
                }
                num3++;
            }
            return galaxy_0;
        }

        private HabitatList method_88(Galaxy galaxy_0, Empire empire_0, HabitatList habitatList_0)
        {
            HabitatList habitatList = new HabitatList();
            HabitatList habitatList2 = new HabitatList();
            habitatList.Add(empire_0.Capital);
            habitatList2.Add(Galaxy.DetermineHabitatSystemStar(empire_0.Capital));
            foreach (Habitat item2 in habitatList_0)
            {
                Habitat item = Galaxy.DetermineHabitatSystemStar(item2);
                if (!habitatList2.Contains(item))
                {
                    habitatList2.Add(item);
                }
            }
            foreach (Habitat item3 in habitatList2)
            {
                Habitat habitat = null;
                long num = 0L;
                foreach (Habitat item4 in habitatList_0)
                {
                    if (Galaxy.DetermineHabitatSystemStar(item4) == item3 && item4.MaximumPopulation > num)
                    {
                        habitat = item4;
                        num = item4.MaximumPopulation;
                    }
                }
                if (habitat != null)
                {
                    habitatList.Add(habitat);
                }
            }
            return habitatList;
        }

        private double method_89(int int_1)
        {
            double num = 0.0;
            Random random = new Random((int)DateTime.Now.Ticks);
            double num2 = 1.0;
            double num3 = 6.99;
            switch (int_1)
            {
                case 0:
                    num2 = 0.0;
                    num3 = 0.0;
                    break;
                case 1:
                    num2 = 0.5;
                    num3 = 0.5;
                    break;
                case 2:
                    num2 = 1.0;
                    num3 = 1.99;
                    break;
                case 3:
                    num2 = 1.0;
                    num3 = 2.99;
                    break;
                case 4:
                    num2 = 2.0;
                    num3 = 3.99;
                    break;
                case 5:
                    num2 = 3.0;
                    num3 = 4.99;
                    break;
                case 6:
                    num2 = 4.0;
                    num3 = 5.99;
                    break;
            }
            num = num2 + random.NextDouble() * (num3 - num2);
            if (int_1 == 1)
            {
                num = 0.5;
            }
            return num;
        }

        private bool method_90(double double_1, double double_2)
        {
            if (!(double_1 < 0.0) && double_2 >= 0.0)
            {
                if (!(double_1 > (double)Galaxy.SizeX) && double_2 <= (double)Galaxy.SizeY)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private void method_91(Galaxy galaxy_0, double double_1, double double_2, double double_3, double double_4, out double double_5, out double double_6)
        {
            double num = Galaxy.Rnd.NextDouble() * double_4;
            double num2 = Math.Cos(double_1) * num;
            double num3 = Math.Sin(double_1) * num;
            if (Galaxy.Rnd.Next(0, 2) == 1)
            {
                num2 *= -1.0;
            }
            if (Galaxy.Rnd.Next(0, 2) == 1)
            {
                num3 *= -1.0;
            }
            double_5 = double_2 + num2;
            double_6 = double_3 + num3;
        }

        private void method_92(Galaxy galaxy_0, double double_1, double double_2, double double_3, double double_4, out double double_5, out double double_6)
        {
            double num = Math.Cos(double_1) * double_4;
            double num2 = Math.Sin(double_1) * double_4;
            if (Galaxy.Rnd.Next(0, 2) == 1)
            {
                num *= -1.0;
            }
            if (Galaxy.Rnd.Next(0, 2) == 1)
            {
                num2 *= -1.0;
            }
            double_5 = double_2 + num;
            double_6 = double_3 + num2;
        }

        private void method_93(Galaxy galaxy_0, double double_1, double double_2, out double double_3, out double double_4)
        {
            double num = (double)Galaxy.SizeX / 2.0;
            double num2 = (double)Galaxy.SizeY / 2.0;
            double num3 = (double)Galaxy.SizeX / 2.0;
            double num4 = num3 * double_1;
            double num5 = Galaxy.Rnd.NextDouble() * num3 * (double_2 - double_1);
            double num6 = num4 + num5;
            double num7 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
            double num8 = Math.Cos(num7) * num6;
            double num9 = Math.Sin(num7) * num6;
            if (Galaxy.Rnd.Next(0, 2) == 1)
            {
                num8 *= -1.0;
            }
            if (Galaxy.Rnd.Next(0, 2) == 1)
            {
                num9 *= -1.0;
            }
            double_3 = num + num8;
            double_4 = num2 + num9;
        }

        private Bitmap method_94(string string_2)
        {
            Bitmap bitmap = null;
            if (string_2 == TextResolver.GetText("Elliptical"))
            {
                bitmap = bitmap_4;
            }
            else if (string_2 == TextResolver.GetText("Spiral"))
            {
                bitmap = bitmap_5;
            }
            else if (string_2 == TextResolver.GetText("Ring"))
            {
                bitmap = bitmap_6;
            }
            else if (string_2 == TextResolver.GetText("Irregular"))
            {
                bitmap = bitmap_7;
            }
            else if (string_2 == TextResolver.GetText("Even Clusters"))
            {
                bitmap = bitmap_8;
            }
            else if (string_2 == TextResolver.GetText("Varied Clusters"))
            {
                bitmap = bitmap_9;
            }
            Bitmap result = null;
            if (bitmap != null)
            {
                result = new Bitmap(bitmap);
            }
            return result;
        }

        private string method_95()
        {
            string result = string.Empty;
            if (cmbYourEmpireStartLocation.SelectedItem != null)
            {
                string text = cmbYourEmpireStartLocation.SelectedItem.ToString();
                result = ((!(text == "(" + TextResolver.GetText("Random") + ")")) ? text : TextResolver.GetText("random location"));
            }
            return result;
        }

        private void cmbYourEmpireStartLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = cmbYourEmpireStartLocation.SelectedItem.ToString();
            double num = 0.0;
            double num2 = 0.0;
            if (text == TextResolver.GetText("Deep Core"))
            {
                num = 0.0;
                num2 = 0.29;
            }
            else if (text == TextResolver.GetText("Outer Core"))
            {
                num = 0.29;
                num2 = 0.19;
            }
            else if (text == TextResolver.GetText("Inner Rim"))
            {
                num = 0.48;
                num2 = 0.38;
            }
            else if (text == TextResolver.GetText("Outer Rim"))
            {
                num = 0.86;
                num2 = 0.14;
            }
            else if (text == TextResolver.GetText("Far Regions"))
            {
                num = 0.48;
                num2 = 0.52;
            }
            else if (text == TextResolver.GetText("Core"))
            {
                num = 0.0;
                num2 = 0.29;
            }
            else if (text == TextResolver.GetText("Void"))
            {
                num = 0.29;
                num2 = 0.53;
            }
            else if (text == TextResolver.GetText("Rim"))
            {
                num = 0.82;
                num2 = 0.18;
            }
            else if (text == TextResolver.GetText("Center"))
            {
                num = 0.0;
                num2 = 0.48;
            }
            else if (text == TextResolver.GetText("Edge"))
            {
                num = 0.48;
                num2 = 0.96;
            }
            else if (text == "(" + TextResolver.GetText("Random") + ")")
            {
                num = 0.0;
                num2 = ((radStartNewGameGalaxyShapeIrregular.Checked || radStartNewGameGalaxyShapeClustersEven.Checked || radStartNewGameGalaxyShapeClustersVaried.Checked) ? 1.44 : 1.0);
            }
            picStartNewGameYourEmpireGalaxyLocation.SizeMode = PictureBoxSizeMode.Zoom;
            picStartNewGameYourEmpireGalaxyLocation.Image = method_94(method_96());
            if (picStartNewGameYourEmpireGalaxyLocation.Image != null)
            {
                Graphics graphics = Graphics.FromImage(picStartNewGameYourEmpireGalaxyLocation.Image);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int num3 = (int)(num2 * (double)(picStartNewGameYourEmpireGalaxyLocation.Image.Width / 2));
                int num4 = (int)(num * (double)(picStartNewGameYourEmpireGalaxyLocation.Image.Width / 2));
                int num5 = picStartNewGameYourEmpireGalaxyLocation.Image.Width / 2 - (num4 + num3);
                int num6 = (num4 + num3) * 2;
                num5 += num3 / 2;
                num6 -= num3;
                Rectangle rect = new Rectangle(num5, num5, num6, num6);
                Pen pen = new Pen(solidBrush_0, num3);
                graphics.DrawArc(pen, rect, 0f, 360f);
            }
        }

        private string method_96()
        {
            string result = TextResolver.GetText("Elliptical");
            if (radStartNewGameGalaxyShapeIrregular.Checked)
            {
                result = TextResolver.GetText("Irregular");
            }
            else if (radStartNewGameGalaxyShapeElliptical.Checked)
            {
                result = TextResolver.GetText("Elliptical");
            }
            else if (radStartNewGameGalaxyShapeRing.Checked)
            {
                result = TextResolver.GetText("Ring");
            }
            else if (radStartNewGameGalaxyShapeSpiral.Checked)
            {
                result = TextResolver.GetText("Spiral");
            }
            else if (radStartNewGameGalaxyShapeClustersEven.Checked)
            {
                result = TextResolver.GetText("Even Clusters");
            }
            else if (radStartNewGameGalaxyShapeClustersVaried.Checked)
            {
                result = TextResolver.GetText("Varied Clusters");
            }
            return result;
        }

        private string method_97()
        {
            string result = TextResolver.GetText("Elliptical");
            if (pnlStartNewGameJumpStartPanel.radJumpStartGalaxyShapeIrregular.Checked)
            {
                result = TextResolver.GetText("Irregular");
            }
            else if (pnlStartNewGameJumpStartPanel.radJumpStartGalaxyShapeElliptical.Checked)
            {
                result = TextResolver.GetText("Elliptical");
            }
            else if (pnlStartNewGameJumpStartPanel.radJumpStartGalaxyShapeRing.Checked)
            {
                result = TextResolver.GetText("Ring");
            }
            else if (pnlStartNewGameJumpStartPanel.radJumpStartGalaxyShapeSpiral.Checked)
            {
                result = TextResolver.GetText("Spiral");
            }
            else if (pnlStartNewGameJumpStartPanel.radJumpStartGalaxyShapeEvenClusters.Checked)
            {
                result = TextResolver.GetText("Even Clusters");
            }
            else if (pnlStartNewGameJumpStartPanel.radJumpStartGalaxyShapeVariedClusters.Checked)
            {
                result = TextResolver.GetText("Varied Clusters");
            }
            return result;
        }

        private void cmbFlagShape_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Bitmap smallFlagPicture = null;
            Bitmap largeFlagPicture = null;
            Color selectedColor = cmbPrimaryColor.SelectedColor;
            Color selectedColor2 = cmbSecondaryColor.SelectedColor;
            if (selectedColor == selectedColor2)
            {
                if (bool_2)
                {
                    Galaxy.GenerateEmpireFlag(Color.Gray, Color.White, e.Index, Galaxy.FlagShapesPirates, ref smallFlagPicture, ref largeFlagPicture);
                }
                else
                {
                    Galaxy.GenerateEmpireFlag(Color.Gray, Color.White, e.Index, Galaxy.FlagShapes, ref smallFlagPicture, ref largeFlagPicture);
                }
            }
            else if (bool_2)
            {
                Galaxy.GenerateEmpireFlag(selectedColor, selectedColor2, e.Index, Galaxy.FlagShapesPirates, ref smallFlagPicture, ref largeFlagPicture);
            }
            else
            {
                Galaxy.GenerateEmpireFlag(selectedColor, selectedColor2, e.Index, Galaxy.FlagShapes, ref smallFlagPicture, ref largeFlagPicture);
            }
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            int num = e.Bounds.Height - 2;
            int num2 = (int)((double)num / 0.6);
            int num3 = (e.Bounds.Width - num2) / 2;
            int num4 = e.Bounds.Y + 1;
            Rectangle rect = new Rectangle(num3, num4, num2, num);
            e.Graphics.DrawImage(largeFlagPicture, rect);
            e.DrawFocusRectangle();
            largeFlagPicture.Dispose();
            smallFlagPicture.Dispose();
        }

        private void cmbFlagShape_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 63;
        }

        private void method_98()
        {
            Color selectedColor = cmbPrimaryColor.SelectedColor;
            Color selectedColor2 = cmbSecondaryColor.SelectedColor;
            int selectedIndex = cmbFlagShape.SelectedIndex;
            Bitmap smallFlagPicture = null;
            Bitmap largeFlagPicture = null;
            if (bool_2)
            {
                Galaxy.GenerateEmpireFlag(selectedColor, selectedColor2, selectedIndex, Galaxy.FlagShapesPirates, ref smallFlagPicture, ref largeFlagPicture);
            }
            else
            {
                Galaxy.GenerateEmpireFlag(selectedColor, selectedColor2, selectedIndex, Galaxy.FlagShapes, ref smallFlagPicture, ref largeFlagPicture);
            }
        }

        private void method_99()
        {
            ctlStartingEmpiresList.Grid.Refresh();
            if (ctlStartingEmpiresList.Enabled)
            {
                ctlStartingEmpiresList.Grid.BackgroundColor = Color.FromArgb(48, 48, 64);
                foreach (DataGridViewRow item in (IEnumerable)ctlStartingEmpiresList.Grid.Rows)
                {
                    foreach (DataGridViewCell cell in item.Cells)
                    {
                        cell.Style = null;
                    }
                }
            }
            else
            {
                Color color = Color.FromArgb(60, 60, 80);
                Color color2 = Color.FromArgb(80, 80, 100);
                ctlStartingEmpiresList.Grid.BackgroundColor = color;
                foreach (DataGridViewRow item2 in (IEnumerable)ctlStartingEmpiresList.Grid.Rows)
                {
                    foreach (DataGridViewCell cell2 in item2.Cells)
                    {
                        cell2.Style.ForeColor = color;
                        cell2.Style.BackColor = color2;
                        cell2.Style.SelectionForeColor = color;
                        cell2.Style.SelectionBackColor = color2;
                    }
                }
            }
            ctlStartingEmpiresList.Grid.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
            ctlStartingEmpiresList.Grid.Refresh();
        }

        private void chkVictoryTimeLimit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVictoryTimeLimit.Checked)
            {
                numVictoryTimeLimitYears.Enabled = true;
            }
            else
            {
                numVictoryTimeLimitYears.Enabled = false;
            }
        }

        private void chkVictoryTimeStart_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVictoryTimeStart.Checked)
            {
                numVictoryTimeStartYears.Enabled = true;
            }
            else
            {
                numVictoryTimeStartYears.Enabled = false;
            }
        }

        private void method_100(string string_2, string string_3)
        {
            pnlStartNewGameYourEmpireType.lblHelpTitle.Text = string_2;
            pnlStartNewGameYourEmpireType.lblHelpDescription.Text = string_3;
        }

        private void cmbYourEmpireStartLocation_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Start Location"), TextResolver.GetText("The approximate starting location of your empire within the galaxy"));
        }

        private void cmbFlagShape_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Flag"), TextResolver.GetText("The design of your empire's flag"));
        }

        private void chkStoryReturnOfTheShakturi_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Story Events"), TextResolver.GetText("Enables story events in the game"));
        }

        private void chkVictoryTerritory_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Territory"), TextResolver.GetText("Victory Condition Target: Control the specified percentage of all colonies in the galaxy"));
        }

        private void chkVictoryPopulation_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Population"), TextResolver.GetText("Victory Condition Target: Control the specified percentage of the galaxy's population"));
        }

        private void chkVictoryEconomy_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Economy"), TextResolver.GetText("Victory Condition Target: Your empire's private economy must generate the specified percentage of the galaxy's total income"));
        }

        private void chkVictoryTimeLimit_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Time Limit"), TextResolver.GetText("The game will finish after the specified number of years have passed"));
        }

        private void chkVictoryTimeStart_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Start Time"), TextResolver.GetText("Victory Conditions will not apply until the specified number of years have passed"));
        }

        private void nOoJoHmYfo(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Time Limit Years"), TextResolver.GetText("The winning empire will be determined after this many years have passed"));
        }

        private void numVictoryTimeStartYears_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Start Time Years"), TextResolver.GetText("Victory Conditions will not apply until this many years have passed"));
        }

        private void ijvJztcJac(object sender, EventArgs e)
        {
            if (bool_2)
            {
                method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Pirate-specific Conditions"), TextResolver.GetText("Enables Pirate-specific Victory Conditions in the game"));
            }
            else
            {
                method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Race-specific Conditions"), TextResolver.GetText("Enables Race-specific Victory Conditions in the game"));
            }
        }

        private void cmbVictoryPiratePlayStyle_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Pirate Playstyle"), TextResolver.GetText("Determines the play focus and pirate victory conditions for your pirate empire"));
        }

        private void cmbVictoryPiratePlayStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            PiratePlayStyle piratePlayStyle_ = PiratePlayStyle.Balanced;
            switch (cmbVictoryPiratePlayStyle.SelectedIndex)
            {
                case 0:
                    piratePlayStyle_ = PiratePlayStyle.Balanced;
                    break;
                case 1:
                    piratePlayStyle_ = PiratePlayStyle.Pirate;
                    break;
                case 2:
                    piratePlayStyle_ = PiratePlayStyle.Mercenary;
                    break;
                case 3:
                    piratePlayStyle_ = PiratePlayStyle.Smuggler;
                    break;
            }
            method_101(piratePlayStyle_, bool_5: false);
        }

        private void method_101(PiratePlayStyle piratePlayStyle_0, bool bool_5)
        {
            string text = string.Empty;
            List<string> list = new List<string>();
            List<double> factorValues = new List<double>();
            List<bool> modifiersAreBonuses = new List<bool>();
            switch (piratePlayStyle_0)
            {
                case PiratePlayStyle.Balanced:
                    text = TextResolver.GetText("Pirate Playstyle Description Balanced");
                    list = Galaxy.ResolvePirateFactionModifierDescriptions(PiratePlayStyle.Balanced, out factorValues, out modifiersAreBonuses);
                    break;
                case PiratePlayStyle.Pirate:
                    text = TextResolver.GetText("Pirate Playstyle Description Pirate");
                    list = Galaxy.ResolvePirateFactionModifierDescriptions(PiratePlayStyle.Pirate, out factorValues, out modifiersAreBonuses);
                    break;
                case PiratePlayStyle.Mercenary:
                    text = TextResolver.GetText("Pirate Playstyle Description Mercenary");
                    list = Galaxy.ResolvePirateFactionModifierDescriptions(PiratePlayStyle.Mercenary, out factorValues, out modifiersAreBonuses);
                    break;
                case PiratePlayStyle.Smuggler:
                    text = TextResolver.GetText("Pirate Playstyle Description Smuggler");
                    list = Galaxy.ResolvePirateFactionModifierDescriptions(PiratePlayStyle.Smuggler, out factorValues, out modifiersAreBonuses);
                    break;
            }
            if (list.Count > 0)
            {
                text += "\n";
                for (int i = 0; i < list.Count; i++)
                {
                    text = text + "\n" + list[i];
                }
            }
            if (bool_5)
            {
                pnlStartNewGameJumpStartPanel.lblJumpStartPiratePlaystyleDescription.Text = text;
            }
            else
            {
                lblPiratePlaystyleDescription.Text = text;
            }
            if (bool_5)
            {
                Bitmap bitmap = (Bitmap)pnlStartNewGameJumpStartPanel.picJumpStartYourEmpirePiratePlaystyle.Image;
                Bitmap image = main_0.method_119(null, pnlStartNewGameJumpStartPanel.cmbJumpStartYourEmpireRace.SelectedRace, pnlStartNewGameJumpStartPanel.picJumpStartYourEmpirePiratePlaystyle.Width, pnlStartNewGameJumpStartPanel.picJumpStartYourEmpirePiratePlaystyle.Height, main_0.bitmap_31, 6, bool_28: true, piratePlayStyle_0);
                pnlStartNewGameJumpStartPanel.picJumpStartYourEmpirePiratePlaystyle.Image = image;
                if (bitmap != null && bitmap.PixelFormat != 0)
                {
                    bitmap.Dispose();
                }
            }
            else
            {
                Bitmap bitmap2 = (Bitmap)picStartNewGameYourEmpirePiratePlaystyle.Image;
                Bitmap image2 = main_0.method_119(null, cmbStartNewGameYourEmpireRace.SelectedRace, picStartNewGameYourEmpirePiratePlaystyle.Width, picStartNewGameYourEmpirePiratePlaystyle.Height, main_0.bitmap_31, 6, bool_28: true, piratePlayStyle_0);
                picStartNewGameYourEmpirePiratePlaystyle.Image = image2;
                if (bitmap2 != null && bitmap2.PixelFormat != 0)
                {
                    bitmap2.Dispose();
                }
            }
        }

        private void chkVictoryEnableDisasterEvents_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Disaster Events"), TextResolver.GetText("Enables various random game events, such as disasters at colonies"));
        }

        private void chkStoryShadows_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Shadows Events"), TextResolver.GetText("Enables pre-warp game events, such as triggered pirate attacks and creature outbreaks"));
        }

        private void chkStartNewGameEnableTechTrading_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Tech Trading"), TextResolver.GetText("Enables tech trading between empires in the Diplomacy screen"));
        }

        private void chkStartNewGameEnableGiantKaltors_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Giant Kaltors"), TextResolver.GetText("Allow Giant Kaltors to exist in the galaxy at the start of the game. These can be disabled to support storylines without them"));
        }

        private void chkVictoryEnableRaceSpecificEvents_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Race-specific Events"), TextResolver.GetText("Enables Race-specific Events in the game"));
        }

        private void cmbVictoryThresholdPercentage_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Victory") + ": " + TextResolver.GetText("Victory Threshold Percentage"), TextResolver.GetText("Sets the percentage of Victory Conditions that must be fulfilled to win the game"));
        }

        private void tbarStartNewGameTheGalaxyDifficulty_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Difficulty"), TextResolver.GetText("Determines difficulty and aggression of gameplay"));
        }

        private void lnkPlayScenario_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            method_24();
        }

        private void lnkCheckForUpdates_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            string text = "http://www.codeforce.co.nz/dwuniverse_versioncheck.asp";
            text = text + "?version=" + Application.ProductVersion;
            Process.Start(text);
        }

        private void lnkCopyright_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            Process.Start("http://www.codeforce.co.nz/");
        }

        private void btnQuickStartCancel_Click(object sender, EventArgs e)
        {
            method_25();
        }

        private int method_102(RaceList raceList_2, string string_2)
        {
            int num = 0;
            while (true)
            {
                if (num < raceList_2.Count)
                {
                    if (raceList_2[num].Name.ToLower(CultureInfo.InvariantCulture) == string_2.ToLower(CultureInfo.InvariantCulture))
                    {
                        break;
                    }
                    num++;
                    continue;
                }
                return -1;
            }
            return num;
        }

        private int method_103(Galaxy galaxy_0, string string_2)
        {
            return method_102(galaxy_0.Races, string_2);
        }

        private void btnQuickStart_Click(object sender, EventArgs e)
        {
            method_140();
            bool_3 = false;
            bool_2 = false;
            string text = "(" + TextResolver.GetText("Random") + ")";
            GalaxyShape galaxyShape = GalaxyShape.Elliptical;
            int num = 0;
            int num2 = 0;
            bool flag = true;
            double num3 = 0.65;
            int num4 = 400;
            double num5 = 0.6;
            double num6 = 0.6;
            int num7 = 1;
            double num8 = meEawywtba(2);
            string string_ = TextResolver.GetText("Starting");
            double num9 = 1.1;
            EmpireStart empireStart = null;
            EmpireStartList empireStartList = null;
            VictoryConditions victoryConditions = null;
            EmpireVictoryConditions item = null;
            EmpireVictoryConditions item2 = null;
            bool flag2 = false;
            string customizationSetName = string.Empty;
            if (main_0.gameOptions_0 != null)
            {
                customizationSetName = main_0.gameOptions_0.CustomizationSetName;
            }
            RaceList raceList = Galaxy.LoadRaces(Application.StartupPath, customizationSetName);
            raceList = raceList.ResolvePlayableRaces();
            string string_2 = text;
            Race selectedRace = cmbQuickStartRace.SelectedRace;
            if (selectedRace != null)
            {
                string_2 = selectedRace.Name;
            }
            Random random = new Random((int)DateTime.Now.Ticks);
            if (radioRandom.Checked)
            {
                num = random.Next(200, 700);
                num2 = 0;
                if (num <= 200)
                {
                    num2 = 12;
                    num3 = 0.82;
                }
                else if (num > 400)
                {
                    num2 = ((num <= 600) ? 18 : ((num > 800) ? 20 : 20));
                }
                else
                {
                    num2 = 15;
                    num3 = 0.74;
                }
                int num10 = random.Next(10, num2);
                string string_3 = method_58(method_57(text));
                string string_4 = method_55(method_54(text));
                empireStartList = new EmpireStartList();
                empireStart = method_114(null, empireStartList, method_109(string_3, random), string_2, method_108(string_4, random), text, text, random);
                for (int i = 0; i < num10; i++)
                {
                    EmpireStart item3 = method_112(empireStart, empireStartList, method_109(string_3, random), method_108(string_4, random), random);
                    empireStartList.Add(item3);
                }
                num8 = meEawywtba(2);
                num9 = 0.9 + random.NextDouble() * 0.8;
                EmpireStartList empireStartList2 = new EmpireStartList();
                empireStartList2.Add(empireStart);
                empireStartList2.AddRange(empireStartList);
                victoryConditions = method_107(num2, empireStartList2, random);
                num4 = random.Next(100, 500);
                string_ = text;
            }
            else if (radioConflict.Checked)
            {
                num = 500;
                num2 = 20;
                num3 = 0.74;
                int num11 = random.Next(15, 20);
                empireStartList = new EmpireStartList();
                empireStart = method_114(null, empireStartList, TextResolver.GetText("Expanding"), string_2, string.Format(TextResolver.GetText("Level X"), "2"), text, text, random);
                empireStart.StartLocation = TextResolver.GetText("Outer Core");
                for (int j = 0; j < num11; j++)
                {
                    if (random.Next(0, 3) == 1)
                    {
                        EmpireStart item4 = method_112(empireStart, empireStartList, method_56(TextResolver.GetText("Expanding"), random), string.Format(TextResolver.GetText("Level X"), "2"), random);
                        empireStartList.Add(item4);
                    }
                    else
                    {
                        EmpireStart item5 = method_113(empireStart, empireStartList, method_56(TextResolver.GetText("Expanding"), random), string.Format(TextResolver.GetText("Level X"), "2"), text, TextResolver.GetText("Nearby"), random);
                        empireStartList.Add(item5);
                    }
                }
                num8 = meEawywtba(1);
                num9 = 1.9;
                num6 = 1.0;
                victoryConditions = method_106(num2);
                num4 = random.Next(100, 500);
                string_ = TextResolver.GetText("Expanding");
                flag2 = true;
            }
            else if (radioRingRace.Checked)
            {
                num = 500;
                num2 = 20;
                num3 = 0.74;
                int num12 = random.Next(15, 20);
                empireStartList = new EmpireStartList();
                empireStart = method_114(null, empireStartList, TextResolver.GetText("Young"), string_2, string.Format(TextResolver.GetText("Level X"), "2"), TextResolver.GetText("Rim"), text, random);
                for (int k = 0; k < num12; k++)
                {
                    string string_5 = TextResolver.GetText("Rim");
                    if (random.Next(0, 10) == 1)
                    {
                        string_5 = TextResolver.GetText("Core");
                    }
                    EmpireStart item6 = method_113(empireStart, empireStartList, method_56(TextResolver.GetText("Young"), random), string.Format(TextResolver.GetText("Level X"), "2"), string_5, text, random);
                    empireStartList.Add(item6);
                }
                num8 = meEawywtba(2);
                num9 = 1.3;
                victoryConditions = method_106(num2);
                galaxyShape = GalaxyShape.Ring;
                num4 = random.Next(200, 500);
                string_ = TextResolver.GetText("Young");
            }
            else if (radioSmall.Checked)
            {
                num = 150;
                num2 = 15;
                num3 = 1.0;
                int num13 = random.Next(10, 15);
                empireStartList = new EmpireStartList();
                empireStart = method_114(null, empireStartList, TextResolver.GetText("Expanding"), string_2, string.Format(TextResolver.GetText("Level X"), "2"), text, text, random);
                for (int l = 0; l < num13; l++)
                {
                    EmpireStart item7 = method_112(empireStart, empireStartList, method_56(TextResolver.GetText("Expanding"), random), string.Format(TextResolver.GetText("Level X"), "2"), random);
                    empireStartList.Add(item7);
                }
                num8 = meEawywtba(2);
                num9 = 1.3;
                victoryConditions = method_106(num2);
                num4 = random.Next(200, 500);
                string_ = TextResolver.GetText("Expanding");
            }
            else if (radioExpandingFromTheCore.Checked)
            {
                num = 700;
                num2 = 20;
                int num14 = random.Next(15, 20);
                empireStartList = new EmpireStartList();
                empireStart = method_114(null, empireStartList, TextResolver.GetText("Expanding"), string_2, string.Format(TextResolver.GetText("Level X"), "2"), TextResolver.GetText("Deep Core"), text, random);
                for (int m = 0; m < num14; m++)
                {
                    EmpireStart item8 = method_113(empireStart, empireStartList, method_56(TextResolver.GetText("Expanding"), random), string.Format(TextResolver.GetText("Level X"), "2"), TextResolver.GetText("Deep Core"), TextResolver.GetText("Nearby"), random);
                    empireStartList.Add(item8);
                }
                num8 = meEawywtba(2);
                num9 = 1.1;
                victoryConditions = method_106(num2);
                num4 = 400;
                string_ = TextResolver.GetText("Expanding");
            }
            else if (radioExpandingSettlements.Checked)
            {
                num = 700;
                num2 = 20;
                int num15 = random.Next(15, 20);
                empireStartList = new EmpireStartList();
                empireStart = method_114(null, empireStartList, TextResolver.GetText("Mature"), string_2, string.Format(TextResolver.GetText("Level X"), "3"), text, text, random);
                for (int n = 0; n < num15; n++)
                {
                    EmpireStart item9 = method_112(empireStart, empireStartList, method_56(TextResolver.GetText("Mature"), random), string.Format(TextResolver.GetText("Level X"), "3"), random);
                    empireStartList.Add(item9);
                }
                num8 = meEawywtba(2);
                num9 = 1.3;
                victoryConditions = method_106(num2);
                num4 = 400;
                string_ = TextResolver.GetText("Mature");
            }
            else if (radioFullyDevelopedSmall.Checked)
            {
                num = 400;
                num2 = 20;
                num3 = 0.74;
                int num16 = random.Next(16, 20);
                empireStartList = new EmpireStartList();
                empireStart = method_114(null, empireStartList, TextResolver.GetText("Old"), string_2, string.Format(TextResolver.GetText("Level X"), "4"), text, text, random);
                for (int num17 = 0; num17 < num16; num17++)
                {
                    EmpireStart item10 = method_112(empireStart, empireStartList, method_56(TextResolver.GetText("Old"), random), string.Format(TextResolver.GetText("Level X"), "4"), random);
                    empireStartList.Add(item10);
                }
                num8 = meEawywtba(2);
                num9 = 1.1;
                victoryConditions = method_106(num2);
                num4 = 400;
                string_ = TextResolver.GetText("Old");
            }
            else if (radioFullyDevelopedStandard.Checked)
            {
                num = 700;
                num2 = 20;
                int num18 = random.Next(17, 20);
                empireStartList = new EmpireStartList();
                empireStart = method_114(null, empireStartList, TextResolver.GetText("Old"), string_2, string.Format(TextResolver.GetText("Level X"), "4"), text, text, random);
                for (int num19 = 0; num19 < num18; num19++)
                {
                    EmpireStart item11 = method_112(empireStart, empireStartList, method_56(TextResolver.GetText("Old"), random), string.Format(TextResolver.GetText("Level X"), "4"), random);
                    empireStartList.Add(item11);
                }
                num8 = meEawywtba(2);
                num9 = 1.1;
                victoryConditions = method_106(num2);
                num4 = 400;
                string_ = TextResolver.GetText("Old");
            }
            else if (radioFullyDevelopedLarge.Checked)
            {
                num = 1000;
                num2 = 20;
                int num20 = random.Next(17, 20);
                empireStartList = new EmpireStartList();
                empireStart = method_114(null, empireStartList, TextResolver.GetText("Old"), string_2, string.Format(TextResolver.GetText("Level X"), "4"), text, text, random);
                for (int num21 = 0; num21 < num20; num21++)
                {
                    EmpireStart item12 = method_112(empireStart, empireStartList, method_56(TextResolver.GetText("Old"), random), string.Format(TextResolver.GetText("Level X"), "4"), random);
                    empireStartList.Add(item12);
                }
                num8 = meEawywtba(2);
                num9 = 1.1;
                victoryConditions = method_106(num2);
                num4 = 400;
                string_ = TextResolver.GetText("Old");
            }
            else if (radioEpic.Checked)
            {
                num = 1000;
                num2 = 20;
                int num22 = random.Next(17, 20);
                empireStartList = new EmpireStartList();
                empireStart = method_114(null, empireStartList, TextResolver.GetText("Starting"), string_2, TextResolver.GetText("Normal"), text, text, random);
                for (int num23 = 0; num23 < num22; num23++)
                {
                    EmpireStart item13 = method_112(empireStart, empireStartList, TextResolver.GetText("Starting"), TextResolver.GetText("Normal"), random);
                    empireStartList.Add(item13);
                }
                num8 = meEawywtba(1);
                num9 = 1.1;
                victoryConditions = method_106(num2);
                num4 = 400;
                string_ = TextResolver.GetText("Starting");
            }
            else if (radioSovereignTerritoriesRegionalRuler.Checked)
            {
                num = 700;
                num2 = 20;
                int num24 = random.Next(3, 5);
                int num25 = random.Next(12, 16);
                empireStartList = new EmpireStartList();
                empireStart = method_114(null, empireStartList, TextResolver.GetText("Old"), string_2, string.Format(TextResolver.GetText("Level X"), "2"), text, text, random);
                for (int num26 = 0; num26 < num24; num26++)
                {
                    EmpireStart item14 = method_112(empireStart, empireStartList, TextResolver.GetText("Old"), string.Format(TextResolver.GetText("Level X"), "2"), random);
                    empireStartList.Add(item14);
                }
                for (int num27 = 0; num27 < num25; num27++)
                {
                    EmpireStart item15 = method_112(empireStart, empireStartList, method_56(TextResolver.GetText("Expanding"), random), string.Format(TextResolver.GetText("Level X"), "2"), random);
                    empireStartList.Add(item15);
                }
                num8 = meEawywtba(2);
                num9 = 1.1;
                victoryConditions = method_106(num2);
                num4 = 400;
                string_ = TextResolver.GetText("Expanding");
            }
            else if (radioSovereignTerritoriesMinorFaction.Checked)
            {
                num = 700;
                num2 = 20;
                int num28 = random.Next(4, 6);
                int num29 = random.Next(12, 15);
                empireStartList = new EmpireStartList();
                empireStart = method_114(null, empireStartList, TextResolver.GetText("Expanding"), string_2, string.Format(TextResolver.GetText("Level X"), "2"), text, text, random);
                for (int num30 = 0; num30 < num28; num30++)
                {
                    EmpireStart item16 = method_112(empireStart, empireStartList, TextResolver.GetText("Old"), string.Format(TextResolver.GetText("Level X"), "2"), random);
                    empireStartList.Add(item16);
                }
                for (int num31 = 0; num31 < num29; num31++)
                {
                    EmpireStart item17 = method_112(empireStart, empireStartList, method_56(TextResolver.GetText("Expanding"), random), string.Format(TextResolver.GetText("Level X"), "2"), random);
                    empireStartList.Add(item17);
                }
                num8 = meEawywtba(2);
                num9 = 1.1;
                victoryConditions = method_106(num2);
                num4 = 400;
                string_ = TextResolver.GetText("Expanding");
            }
            else if (radioGalacticRepublicSupremeRuler.Checked)
            {
                num = 700;
                num2 = 20;
                int num32 = random.Next(12, 16);
                empireStartList = new EmpireStartList();
                empireStart = method_114(null, empireStartList, TextResolver.GetText("Supersize"), string_2, string.Format(TextResolver.GetText("Level X"), "3"), text, text, random);
                empireStart.StartLocation = TextResolver.GetText("Deep Core");
                empireStart.GovernmentStyle = TextResolver.GetText("Republic");
                empireStart.Name = "Galactic Republic";
                for (int num33 = 0; num33 < num32; num33++)
                {
                    if (random.Next(0, 3) == 1)
                    {
                        EmpireStart empireStart2 = method_112(empireStart, empireStartList, method_56(TextResolver.GetText("Expanding"), random), string.Format(TextResolver.GetText("Level X"), "3"), random);
                        empireStart2.StartLocation = TextResolver.GetText("Inner Rim");
                        empireStartList.Add(empireStart2);
                    }
                    else
                    {
                        EmpireStart empireStart3 = method_112(empireStart, empireStartList, method_56(TextResolver.GetText("Expanding"), random), string.Format(TextResolver.GetText("Level X"), "3"), random);
                        empireStart3.StartLocation = TextResolver.GetText("Outer Rim");
                        empireStartList.Add(empireStart3);
                    }
                }
                num8 = meEawywtba(1);
                num9 = 1.1;
                victoryConditions = method_106(num32);
                victoryConditions.EconomyPercent = 95.0;
                victoryConditions.TerritoryPercent = 95.0;
                victoryConditions.PopulationPercent = 95.0;
                num4 = 400;
                string_ = TextResolver.GetText("Expanding");
            }
            else if (radioGalacticRepublicWildFrontiers.Checked)
            {
                num = 700;
                num2 = 20;
                int num34 = random.Next(11, 15);
                empireStartList = new EmpireStartList();
                empireStart = method_114(null, empireStartList, TextResolver.GetText("Expanding"), string_2, string.Format(TextResolver.GetText("Level X"), "3"), text, text, random);
                empireStart.StartLocation = TextResolver.GetText("Outer Rim");
                EmpireStart empireStart4 = method_112(empireStart, empireStartList, TextResolver.GetText("Supersize"), string.Format(TextResolver.GetText("Level X"), "3"), random);
                empireStart4.StartLocation = TextResolver.GetText("Deep Core");
                empireStart4.GovernmentStyle = TextResolver.GetText("Republic");
                empireStart4.Name = "Galactic Republic";
                empireStartList.Add(empireStart4);
                for (int num35 = 0; num35 < num34; num35++)
                {
                    if (random.Next(0, 3) == 1)
                    {
                        empireStart4 = method_112(empireStart, empireStartList, method_56(TextResolver.GetText("Expanding"), random), string.Format(TextResolver.GetText("Level X"), "3"), random);
                        empireStart4.StartLocation = TextResolver.GetText("Inner Rim");
                        empireStartList.Add(empireStart4);
                    }
                    else
                    {
                        empireStart4 = method_112(empireStart, empireStartList, method_56(TextResolver.GetText("Expanding"), random), string.Format(TextResolver.GetText("Level X"), "3"), random);
                        empireStart4.StartLocation = TextResolver.GetText("Outer Rim");
                        empireStartList.Add(empireStart4);
                    }
                }
                num8 = meEawywtba(1);
                num9 = 1.1;
                victoryConditions = method_106(num34);
                victoryConditions.EconomyPercent = 95.0;
                victoryConditions.TerritoryPercent = 95.0;
                victoryConditions.PopulationPercent = 95.0;
                num4 = 400;
                string_ = TextResolver.GetText("Expanding");
            }
            if (!method_186(num))
            {
                string text2 = string.Format(TextResolver.GetText("Your computer does not have enough memory to play a Quick Start galaxy of this size"), "700", "2Gb");
                MessageBox.Show(text2, TextResolver.GetText("Not Enough Memory for this Galaxy Size"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            victoryConditions.EnableStoryEvents = chkQuickStartReturnOfTheShakturiStoryEvents.Checked;
            victoryConditions.EnableDisasterEvents = true;
            victoryConditions.EnableRaceSpecificEvents = true;
            victoryConditions.EnableRaceSpecificVictoryConditions = true;
            empireStart.GalaxySectorX = 10;
            empireStart.GalaxySectorY = 10;
            string string_6 = string.Empty;
            if (selectedRace != null)
            {
                string_6 = selectedRace.Name;
            }
            empireStartList = method_104(empireStartList, raceList, num9, string_6);
            List<object> list = new List<object>();
            list.Add(galaxyShape);
            list.Add(num);
            list.Add(num2);
            list.Add(flag);
            list.Add(num3);
            list.Add(num4);
            list.Add(num5);
            list.Add(num6);
            list.Add(num8);
            list.Add(method_57(string_));
            list.Add(num9);
            list.Add(empireStart);
            list.Add(empireStartList);
            list.Add(victoryConditions);
            list.Add(item);
            list.Add(item2);
            list.Add(flag2);
            list.Add(num7);
            list.Add(double_0);
            list.Add(chkQuickStartDistantWorldsStoryEvents.Checked);
            list.Add(null);
            method_8(TextResolver.GetText("Creating new Galaxy..."));
            base.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            oyxRtRyAwjg.RunWorkerAsync(list);
            Cursor.Current = Cursors.WaitCursor;
            while (oyxRtRyAwjg.IsBusy)
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(30);
            }
            Cursor.Current = Cursors.Default;
            if (game_0 != null)
            {
                method_77(game_0);
            }
        }
        private EmpireStartList method_104(EmpireStartList empireStartList_0, RaceList raceList_2, double double_1, string string_2)
        {
            RaceList raceList = method_105(raceList_2, 115, 90);
            if (raceList.Count > 1 && empireStartList_0.Count > 4)
            {
                empireStartList_0.Update(raceList_2, string_2);
                int num = 1;
                if (double_1 >= 1.1)
                {
                    num = 2;
                }
                else if (double_1 >= 1.5)
                {
                    num = 3;
                }
                int num2 = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (raceList.Contains(empireStartList_0[i].ResolvedRace))
                    {
                        num2++;
                    }
                }
                if (num2 < num)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (!raceList.Contains(empireStartList_0[j].ResolvedRace))
                        {
                            int num3 = -1;
                            for (int k = 4; k < empireStartList_0.Count; k++)
                            {
                                if (raceList.Contains(empireStartList_0[k].ResolvedRace))
                                {
                                    num3 = k;
                                    break;
                                }
                            }
                            if (num3 >= 0)
                            {
                                EmpireStart value = empireStartList_0[num3];
                                int age = empireStartList_0[num3].Age;
                                empireStartList_0[num3] = empireStartList_0[j];
                                empireStartList_0[j] = value;
                                empireStartList_0[num3].Age = empireStartList_0[j].Age;
                                empireStartList_0[j].Age = age;
                                num2++;
                            }
                        }
                        if (num2 >= num)
                        {
                            break;
                        }
                    }
                    empireStartList_0.Update(raceList_2);
                }
            }
            return empireStartList_0;
        }

        private RaceList method_105(RaceList raceList_2, int int_1, int int_2)
        {
            RaceList raceList = new RaceList();
            List<int> list = new List<int>();
            for (int i = 0; i < raceList_2.Count; i++)
            {
                list.Add(raceList_2[i].AggressionLevel);
            }
            Race[] array = raceList_2.ToArray();
            int[] keys = list.ToArray();
            Array.Sort(keys, array);
            Array.Reverse(array);
            for (int j = 0; j < array.Length; j++)
            {
                if (array[j].AggressionLevel >= int_1 && array[j].IntelligenceLevel >= int_2)
                {
                    raceList.Add(array[j]);
                }
            }
            return raceList;
        }

        public override string ToString()
        {
            return "Disabled";
        }

        private VictoryConditions method_106(int int_1)
        {
            return method_107(int_1, null, new Random());
        }

        private VictoryConditions method_107(int int_1, EmpireStartList empireStartList_0, Random random_0)
        {
            double num = 50.0;
            if (empireStartList_0 != null)
            {
                int num2 = 0;
                int num3 = 0;
                for (int i = 0; i < empireStartList_0.Count; i++)
                {
                    double num4 = Galaxy.DetermineEmpireExpansion(random_0, empireStartList_0[i].Age);
                    int num5 = (int)num4;
                    num2 += num5;
                    if (num5 > num3)
                    {
                        num3 = num5;
                    }
                }
                double num6 = (double)num3 / (double)num2;
                num = num6 * 1.7;
                num *= 100.0;
            }
            else
            {
                double num7 = 5.0;
                num = 100.0 / (double)int_1 * num7;
            }
            VictoryConditions victoryConditions = new VictoryConditions();
            victoryConditions.Economy = true;
            victoryConditions.EconomyPercent = num;
            victoryConditions.EconomyPercent = Math.Min(victoryConditions.EconomyPercent, 66.0);
            victoryConditions.EconomyPercent = Math.Max(victoryConditions.EconomyPercent, 15.0);
            victoryConditions.Territory = true;
            victoryConditions.TerritoryPercent = num;
            victoryConditions.TerritoryPercent = Math.Min(victoryConditions.TerritoryPercent, 66.0);
            victoryConditions.TerritoryPercent = Math.Max(victoryConditions.TerritoryPercent, 15.0);
            victoryConditions.Population = true;
            victoryConditions.PopulationPercent = num;
            victoryConditions.PopulationPercent = Math.Min(victoryConditions.PopulationPercent, 66.0);
            victoryConditions.PopulationPercent = Math.Max(victoryConditions.PopulationPercent, 15.0);
            victoryConditions.VictoryThresholdPercentage = 0.8;
            return victoryConditions;
        }

        private string method_108(string string_2, Random random_0)
        {
            string result = "(" + TextResolver.GetText("Random") + ")";
            //int num = 0;
            if (string_2 == TextResolver.GetText("PreWarp"))
            {
                result = TextResolver.GetText("PreWarp");
            }
            else if (string_2 == TextResolver.GetText("Starting"))
            {
                result = TextResolver.GetText("Starting");
            }
            else if (string_2 == string.Format(TextResolver.GetText("Level X"), "1"))
            {
                switch (random_0.Next(0, 2))
                {
                    case 0:
                        result = TextResolver.GetText("Starting");
                        break;
                    case 1:
                        result = string.Format(TextResolver.GetText("Level X"), "1");
                        break;
                }
            }
            else if (string_2 == string.Format(TextResolver.GetText("Level X"), "2"))
            {
                switch (random_0.Next(0, 2))
                {
                    case 0:
                        result = string.Format(TextResolver.GetText("Level X"), "1");
                        break;
                    case 1:
                        result = string.Format(TextResolver.GetText("Level X"), "2");
                        break;
                }
            }
            else if (string_2 == string.Format(TextResolver.GetText("Level X"), "3"))
            {
                switch (random_0.Next(0, 2))
                {
                    case 0:
                        result = string.Format(TextResolver.GetText("Level X"), "2");
                        break;
                    case 1:
                        result = string.Format(TextResolver.GetText("Level X"), "3");
                        break;
                }
            }
            else if (string_2 == string.Format(TextResolver.GetText("Level X"), "4"))
            {
                switch (random_0.Next(0, 2))
                {
                    case 0:
                        result = string.Format(TextResolver.GetText("Level X"), "3");
                        break;
                    case 1:
                        result = string.Format(TextResolver.GetText("Level X"), "4");
                        break;
                }
            }
            else if (string_2 == string.Format(TextResolver.GetText("Level X"), "5"))
            {
                switch (random_0.Next(0, 2))
                {
                    case 0:
                        result = string.Format(TextResolver.GetText("Level X"), "4");
                        break;
                    case 1:
                        result = string.Format(TextResolver.GetText("Level X"), "5");
                        break;
                }
            }
            else if (string_2 == string.Format(TextResolver.GetText("Level X"), "6"))
            {
                switch (random_0.Next(0, 2))
                {
                    case 0:
                        result = string.Format(TextResolver.GetText("Level X"), "5");
                        break;
                    case 1:
                        result = string.Format(TextResolver.GetText("Level X"), "6");
                        break;
                }
            }
            else if (string_2 == string.Format(TextResolver.GetText("Level X"), "7"))
            {
                switch (random_0.Next(0, 2))
                {
                    case 0:
                        result = string.Format(TextResolver.GetText("Level X"), "6");
                        break;
                    case 1:
                        result = string.Format(TextResolver.GetText("Level X"), "7");
                        break;
                }
            }
            return result;
        }

        private string method_109(string string_2, Random random_0)
        {
            string result = "(" + TextResolver.GetText("Random") + ")";
            //int num = 0;
            if (string_2 == TextResolver.GetText("PreWarp"))
            {
                result = TextResolver.GetText("PreWarp");
            }
            else if (string_2 == TextResolver.GetText("Starting"))
            {
                result = TextResolver.GetText("Starting");
            }
            else if (string_2 == TextResolver.GetText("Young"))
            {
                switch (random_0.Next(0, 2))
                {
                    case 0:
                        result = TextResolver.GetText("Starting");
                        break;
                    case 1:
                        result = TextResolver.GetText("Young");
                        break;
                }
            }
            else if (string_2 == TextResolver.GetText("Expanding"))
            {
                switch (random_0.Next(0, 2))
                {
                    case 0:
                        result = TextResolver.GetText("Young");
                        break;
                    case 1:
                        result = TextResolver.GetText("Expanding");
                        break;
                }
            }
            else if (string_2 == TextResolver.GetText("Mature"))
            {
                switch (random_0.Next(0, 2))
                {
                    case 0:
                        result = TextResolver.GetText("Expanding");
                        break;
                    case 1:
                        result = TextResolver.GetText("Mature");
                        break;
                }
            }
            else if (string_2 == TextResolver.GetText("Old"))
            {
                switch (random_0.Next(0, 2))
                {
                    case 0:
                        result = TextResolver.GetText("Mature");
                        break;
                    case 1:
                        result = TextResolver.GetText("Old");
                        break;
                }
            }
            else if (string_2 == TextResolver.GetText("Supersize"))
            {
                switch (random_0.Next(0, 3))
                {
                    case 0:
                        result = TextResolver.GetText("Mature");
                        break;
                    case 1:
                        result = TextResolver.GetText("Old");
                        break;
                    case 2:
                        result = TextResolver.GetText("Supersize");
                        break;
                }
            }
            return result;
        }

        private EmpireStart method_110(EmpireStart empireStart_0, EmpireStartList empireStartList_0, Random random_0)
        {
            return method_112(empireStart_0, empireStartList_0, "(" + TextResolver.GetText("Random") + ")", TextResolver.GetText("Basic"), random_0);
        }

        private EmpireStart method_111(EmpireStart empireStart_0, EmpireStartList empireStartList_0, Random random_0, string string_2)
        {
            return method_114(empireStart_0, empireStartList_0, "(" + TextResolver.GetText("Random") + ")", string_2, TextResolver.GetText("Basic"), "(" + TextResolver.GetText("Random") + ")", "(" + TextResolver.GetText("Random") + ")", random_0);
        }

        private EmpireStart method_112(EmpireStart empireStart_0, EmpireStartList empireStartList_0, string string_2, string string_3, Random random_0)
        {
            return method_113(empireStart_0, empireStartList_0, string_2, string_3, "(" + TextResolver.GetText("Random") + ")", "(" + TextResolver.GetText("Random") + ")", random_0);
        }

        private EmpireStart method_113(EmpireStart empireStart_0, EmpireStartList empireStartList_0, string string_2, string string_3, string string_4, string string_5, Random random_0)
        {
            return method_114(empireStart_0, empireStartList_0, string_2, "(" + TextResolver.GetText("Random") + ")", string_3, string_4, string_5, random_0);
        }

        private EmpireStart method_114(EmpireStart empireStart_0, EmpireStartList empireStartList_0, string string_2, string string_3, string string_4, string string_5, string string_6, Random random_0)
        {
            EmpireStart empireStart = new EmpireStart();
            empireStart.Age = method_57(string_2);
            empireStart.GovernmentStyle = "(" + TextResolver.GetText("Random") + ")";
            empireStart.HomeSystemFavourability = TextResolver.GetText("Normal");
            empireStart.Name = "";
            if (string_3 == "(" + TextResolver.GetText("Random") + ")")
            {
                EmpireStartList empireStartList = new EmpireStartList();
                if (empireStart_0 != null)
                {
                    empireStartList.Add(empireStart_0);
                }
                if (empireStartList != null && empireStartList.Count > 0)
                {
                    empireStartList.AddRange(empireStartList_0);
                }
                string_3 = method_115(random_0, empireStartList);
            }
            empireStart.Race = string_3;
            empireStart.StartLocation = string_5;
            empireStart.ProximityDistance = string_6;
            empireStart.TechLevel = method_54(string_4);
            return empireStart;
        }

        private string method_115(Random random_0, EmpireStartList empireStartList_0)
        {
            string result = "(" + TextResolver.GetText("Random") + ")";
            List<string> list = method_116(empireStartList_0);
            if (list.Count > 0)
            {
                result = list[random_0.Next(0, list.Count)];
            }
            return result;
        }

        private List<string> method_116(EmpireStartList empireStartList_0)
        {
            List<string> list = new List<string>();
            int num = 70;
            foreach (Race item in raceList_0)
            {
                if (item.IntelligenceLevel >= num && !method_117(empireStartList_0, item.Name))
                {
                    list.Add(item.Name);
                }
            }
            return list;
        }

        private bool method_117(EmpireStartList empireStartList_0, string string_2)
        {
            foreach (EmpireStart item in empireStartList_0)
            {
                if (item.Race.ToLower(CultureInfo.InvariantCulture) == string_2.ToLower(CultureInfo.InvariantCulture))
                {
                    return true;
                }
            }
            return false;
        }

        private void radioClassicEmpire_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = "Classic Empire";
            string text = "You are an all-powerful empire bringing fear and trepidation to every corner of the galaxy. \n\n";
            text += "A small band of rag-tag renegades opposes your benevolent rule and threatens the galactic order. ";
            text += "These snivelling rebels will soon be crushed...";
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void radioClassicRebels_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = "Classic Renegades";
            string text = "You lead an alliance of freedom-loving renegades fighting against a corrupt galactic empire. \n\n";
            text += "This evil empire spreads its blanket of darkness across the galaxy, intimidating everyone and crushing all resistance. \n\n";
            text += "You begin with a small collection of remote colonies and many potential allies who long to overthrow their oppressors...";
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void radioEpic_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = TextResolver.GetText("QuickStart Title Epic");
            string text = TextResolver.GetText("Galaxy Size") + ": " + TextResolver.GetText("Large") + ", 1000 " + TextResolver.GetText("stars") + "\n";
            string text2 = text;
            text = text2 + TextResolver.GetText("Your Empire") + ": 1 " + TextResolver.GetText("colony") + "\n\n";
            text += TextResolver.GetText("QuickStart Epic");
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void radioRingRace_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = TextResolver.GetText("QuickStart Title Ring Race");
            string text = TextResolver.GetText("Galaxy Size") + ": " + TextResolver.GetText("Small") + ", 500 " + TextResolver.GetText("stars") + "\n";
            string text2 = text;
            text = text2 + TextResolver.GetText("Your Empire") + ": 1-2 " + TextResolver.GetText("colonies") + "\n\n";
            text += TextResolver.GetText("QuickStart Ring Race");
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void radioRandom_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = TextResolver.GetText("Random");
            string text = TextResolver.GetText("Galaxy Size") + ": " + TextResolver.GetText("Random") + "\n";
            string text2 = text;
            text = text2 + TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Random") + "\n\n";
            text += TextResolver.GetText("QuickStart Random");
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void radioConflict_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = TextResolver.GetText("QuickStart Title Conflict");
            string text = TextResolver.GetText("Galaxy Size") + ": " + TextResolver.GetText("Small") + ", 500 " + TextResolver.GetText("stars") + "\n";
            string text2 = text;
            text = text2 + TextResolver.GetText("Your Empire") + ": 5-8 " + TextResolver.GetText("colonies") + "\n\n";
            text += TextResolver.GetText("QuickStart Conflict");
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void chkVictoryTerritory_CheckedChanged(object sender, EventArgs e)
        {
            numVictoryTerritoryPercent.Enabled = chkVictoryTerritory.Checked;
        }

        private void chkVictoryPopulation_CheckedChanged(object sender, EventArgs e)
        {
            numVictoryPopulationPercent.Enabled = chkVictoryPopulation.Checked;
        }

        private void chkVictoryEconomy_CheckedChanged(object sender, EventArgs e)
        {
            numVictoryEconomyPercent.Enabled = chkVictoryEconomy.Checked;
        }

        private void method_118(object sender, EventArgs e)
        {
            int num = method_61(tbarStartNewGameTheGalaxyStarDensity.Value, raceList_0);
            numAutogenerateEmpiresAmount.Maximum = num - 1;
            if (ctlStartingEmpiresList.Grid.Rows.Count >= num - 1)
            {
                btnAddNewEmpire.Enabled = false;
            }
            else
            {
                btnAddNewEmpire.Enabled = true;
            }
        }

        private void radioExpandingSettlements_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = TextResolver.GetText("QuickStart Title Expanding Settlements");
            string text = TextResolver.GetText("Galaxy Size") + ": " + TextResolver.GetText("Standard") + ", 700 " + TextResolver.GetText("stars") + "\n";
            string text2 = text;
            text = text2 + TextResolver.GetText("Your Empire") + ": 16-20 " + TextResolver.GetText("colonies") + "\n\n";
            text += TextResolver.GetText("QuickStart Expanding Settlements");
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void radioExpandingFromTheCore_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = TextResolver.GetText("QuickStart Title Expanding From The Core");
            string text = TextResolver.GetText("Galaxy Size") + ": " + TextResolver.GetText("Standard") + ", 700 " + TextResolver.GetText("stars") + "\n";
            string text2 = text;
            text = text2 + TextResolver.GetText("Your Empire") + ": 5-7 " + TextResolver.GetText("colonies") + "\n\n";
            text += TextResolver.GetText("QuickStart Expanding from the Core");
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void radioSmall_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = TextResolver.GetText("QuickStart Title Fast");
            string text = TextResolver.GetText("Galaxy Size") + ": " + TextResolver.GetText("Tiny") + ", 150 " + TextResolver.GetText("stars") + "\n";
            string text2 = text;
            text = text2 + TextResolver.GetText("Your Empire") + ": 5-7 " + TextResolver.GetText("colonies") + "\n\n";
            text += TextResolver.GetText("QuickStart Fast");
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void radioFullyDevelopedSmall_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = TextResolver.GetText("QuickStart Title Fully Developed - Small");
            string text = TextResolver.GetText("Galaxy Size") + ": " + TextResolver.GetText("Small") + ", 400 " + TextResolver.GetText("stars") + "\n";
            string text2 = text;
            text = text2 + TextResolver.GetText("Your Empire") + ": 30-50 " + TextResolver.GetText("colonies") + "\n\n";
            text += TextResolver.GetText("QuickStart Fully Developed - Small");
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void radioFullyDevelopedStandard_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = TextResolver.GetText("QuickStart Title Fully Developed - Standard");
            string text = TextResolver.GetText("Galaxy Size") + ": " + TextResolver.GetText("Standard") + ", 700 " + TextResolver.GetText("stars") + "\n";
            string text2 = text;
            text = text2 + TextResolver.GetText("Your Empire") + ": 30-50 " + TextResolver.GetText("colonies") + "\n\n";
            text += TextResolver.GetText("QuickStart Fully Developed - Standard");
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void radioFullyDevelopedLarge_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = TextResolver.GetText("QuickStart Title Fully Developed - Large");
            string text = TextResolver.GetText("Galaxy Size") + ": " + TextResolver.GetText("Large") + ", 1000 " + TextResolver.GetText("stars") + "\n";
            string text2 = text;
            text = text2 + TextResolver.GetText("Your Empire") + ": 30-50 " + TextResolver.GetText("colonies") + "\n\n";
            text += TextResolver.GetText("QuickStart Fully Developed - Large");
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void radioGalacticRepublicSupremeRuler_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = TextResolver.GetText("QuickStart Title Galactic Republic - Supreme Ruler");
            string text = TextResolver.GetText("Galaxy Size") + ": " + TextResolver.GetText("Standard") + ", 700 " + TextResolver.GetText("stars") + "\n";
            string text2 = text;
            text = text2 + TextResolver.GetText("Your Empire") + ": 100-150 " + TextResolver.GetText("colonies") + "\n\n";
            text += TextResolver.GetText("QuickStart Galactic Republic - Supreme Ruler");
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void radioGalacticRepublicWildFrontiers_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = TextResolver.GetText("QuickStart Title Galactic Republic - Wild Frontiers");
            string text = TextResolver.GetText("Galaxy Size") + ": " + TextResolver.GetText("Standard") + ", 700 " + TextResolver.GetText("stars") + "\n";
            string text2 = text;
            text = text2 + TextResolver.GetText("Your Empire") + ": 5-7 " + TextResolver.GetText("colonies") + "\n\n";
            text += TextResolver.GetText("QuickStart Galactic Republic - Wild Frontiers");
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void radioSovereignTerritoriesRegionalRuler_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = TextResolver.GetText("QuickStart Title Sovereign Territories - Regional Ruler");
            string text = TextResolver.GetText("Galaxy Size") + ": " + TextResolver.GetText("Standard") + ", 700 " + TextResolver.GetText("stars") + "\n";
            string text2 = text;
            text = text2 + TextResolver.GetText("Your Empire") + ": 40-60 " + TextResolver.GetText("colonies") + "\n\n";
            text += TextResolver.GetText("QuickStart Sovereign Territories - Regional Ruler");
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void radioSovereignTerritoriesMinorFaction_CheckedChanged(object sender, EventArgs e)
        {
            lblQuickStartDescriptionTitle.Text = TextResolver.GetText("QuickStart Title Sovereign Territories - Minor Faction");
            string text = TextResolver.GetText("Galaxy Size") + ": " + TextResolver.GetText("Standard") + ", 700 " + TextResolver.GetText("stars") + "\n";
            string text2 = text;
            text = text2 + TextResolver.GetText("Your Empire") + ": 5-7 " + TextResolver.GetText("colonies") + "\n\n";
            text += TextResolver.GetText("QuickStart Sovereign Territories - Minor Faction");
            lblQuickStartDescriptionDetail.Text = text;
        }

        private void lnkTutorial_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            method_119();
        }

        private void method_119()
        {
            FtIzCrmve5.Size = new Size(385, 481);
            FtIzCrmve5.Location = new Point((base.Width - FtIzCrmve5.Width) / 2, (base.Height - FtIzCrmve5.Height) / 2);
            FtIzCrmve5.DoLayout();
            lnkTutorialFindingYourWayAround.Location = new Point(35, 20);
            lnkTutorialFindingYourWayAround.Text = TextResolver.GetText("Finding Your Way Around");
            lnkTutorialFindingYourWayAround.Font = new Font(lnkTutorialFindingYourWayAround.Font.FontFamily, 15f, FontStyle.Bold);
            lnkTutorialEmpireAndColonies.Location = new Point(35, 58);
            lnkTutorialEmpireAndColonies.Text = TextResolver.GetText("Your Empire and Colonies");
            lnkTutorialEmpireAndColonies.Font = new Font(lnkTutorialEmpireAndColonies.Font.FontFamily, 15f, FontStyle.Bold);
            lnkTutorialShipsMissions.Location = new Point(35, 96);
            lnkTutorialShipsMissions.Text = TextResolver.GetText("Ships and Bases");
            lnkTutorialShipsMissions.Font = new Font(lnkTutorialShipsMissions.Font.FontFamily, 15f, FontStyle.Bold);
            lnkTutorialExpansionDiplomacy.Location = new Point(35, 134);
            lnkTutorialExpansionDiplomacy.Text = TextResolver.GetText("Expansion and Diplomacy");
            lnkTutorialExpansionDiplomacy.Font = new Font(lnkTutorialExpansionDiplomacy.Font.FontFamily, 15f, FontStyle.Bold);
            lnkTutorialResearchDesign.Location = new Point(35, 172);
            lnkTutorialResearchDesign.Text = TextResolver.GetText("Research, Ship Design and Construction");
            lnkTutorialResearchDesign.Font = new Font(lnkTutorialResearchDesign.Font.FontFamily, 15f, FontStyle.Bold);
            lnkTutorialFleetsTroops.Location = new Point(35, 210);
            lnkTutorialFleetsTroops.Text = TextResolver.GetText("Fleets, Troops and Intelligence missions");
            lnkTutorialFleetsTroops.Font = new Font(lnkTutorialFleetsTroops.Font.FontFamily, 15f, FontStyle.Bold);
            lnkTutorialDealingWithPirates.Location = new Point(35, 248);
            lnkTutorialDealingWithPirates.Text = TextResolver.GetText("Dealing with Pirates");
            lnkTutorialDealingWithPirates.Font = new Font(lnkTutorialDealingWithPirates.Font.FontFamily, 15f, FontStyle.Bold);
            lnkTutorialPlayAsPirate.Location = new Point(35, 286);
            lnkTutorialPlayAsPirate.Text = TextResolver.GetText("Play as a Pirate Faction");
            lnkTutorialPlayAsPirate.Font = new Font(lnkTutorialPlayAsPirate.Font.FontFamily, 15f, FontStyle.Bold);
            lnkTutorialPreWarpEmpire.Location = new Point(35, 324);
            lnkTutorialPreWarpEmpire.Text = TextResolver.GetText("Play as a PreWarp Empire");
            lnkTutorialPreWarpEmpire.Font = new Font(lnkTutorialPreWarpEmpire.Font.FontFamily, 15f, FontStyle.Bold);
            btnTutorialStartCancel.Size = new Size(100, 25);
            btnTutorialStartCancel.Location = new Point(260, 384);
            FtIzCrmve5.Visible = true;
            FtIzCrmve5.BringToFront();
        }

        private void method_120()
        {
            FtIzCrmve5.Visible = false;
            FtIzCrmve5.SendToBack();
        }

        private void lnkTutorialDealingWithPirates_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_123();
        }

        private void lnkTutorialPlayAsPirate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_121();
        }

        private void lnkTutorialPreWarpEmpire_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_122();
        }

        private void method_121()
        {
            method_149();
            method_140();
            main_0.bool_7 = true;
            main_0.string_1 = "PlayAsPirate";
            GalaxyShape galaxyShape = GalaxyShape.Elliptical;
            bool flag = true;
            EmpireVictoryConditions item = null;
            EmpireVictoryConditions item2 = null;
            bool flag2 = false;
            int num = 0;
            Random random = new Random((int)DateTime.Now.Ticks);
            int num2 = 700;
            int num3 = 20;
            int num4 = random.Next(15, 20);
            EmpireStartList empireStartList = new EmpireStartList();
            EmpireStart empireStart = method_112(null, empireStartList, TextResolver.GetText("Starting"), TextResolver.GetText("Normal"), random);
            bool_2 = true;
            bool_3 = true;
            empireStart.PiratePlayStyle = PiratePlayStyle.Pirate;
            for (int i = 0; i < num4; i++)
            {
                EmpireStart item3 = method_112(empireStart, empireStartList, TextResolver.GetText("PreWarp"), TextResolver.GetText("PreWarp"), random);
                empireStartList.Add(item3);
            }
            double num5 = meEawywtba(2);
            double num6 = 1.1;
            VictoryConditions item4 = method_106(num3);
            double num7 = 1.0;
            int num8 = random.Next(200, 500);
            double num9 = 0.0;
            double num10 = 0.4;
            string string_ = TextResolver.GetText("PreWarp");
            List<object> list = new List<object>();
            list.Add(galaxyShape);
            list.Add(num2);
            list.Add(num3);
            list.Add(flag);
            list.Add(num7);
            list.Add(num8);
            list.Add(num9);
            list.Add(num10);
            list.Add(num5);
            list.Add(method_57(string_));
            list.Add(num6);
            list.Add(empireStart);
            list.Add(empireStartList);
            list.Add(item4);
            list.Add(item);
            list.Add(item2);
            list.Add(flag2);
            list.Add(num);
            list.Add(double_0);
            list.Add(true);
            list.Add(null);
            method_8(TextResolver.GetText("Creating new Galaxy..."));
            method_120();
            base.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            oyxRtRyAwjg.RunWorkerAsync(list);
            Cursor.Current = Cursors.WaitCursor;
            while (oyxRtRyAwjg.IsBusy)
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(30);
            }
            Cursor.Current = Cursors.Default;
            if (game_0 != null)
            {
                method_77(game_0);
            }
        }

        private void method_122()
        {
            method_149();
            method_140();
            main_0.bool_7 = true;
            main_0.string_1 = "PreWarpEmpire";
            GalaxyShape galaxyShape = GalaxyShape.Elliptical;
            bool flag = true;
            EmpireVictoryConditions item = null;
            EmpireVictoryConditions item2 = null;
            bool flag2 = false;
            int num = 0;
            Random random = new Random((int)DateTime.Now.Ticks);
            int num2 = 700;
            int num3 = 20;
            int num4 = random.Next(15, 20);
            EmpireStartList empireStartList = new EmpireStartList();
            EmpireStart empireStart = method_112(null, empireStartList, TextResolver.GetText("PreWarp"), TextResolver.GetText("PreWarp"), random);
            bool_2 = false;
            bool_3 = true;
            for (int i = 0; i < num4; i++)
            {
                EmpireStart item3 = method_112(empireStart, empireStartList, TextResolver.GetText("PreWarp"), TextResolver.GetText("PreWarp"), random);
                empireStartList.Add(item3);
            }
            double num5 = meEawywtba(2);
            double num6 = 1.1;
            VictoryConditions item4 = method_106(num3);
            double num7 = 1.0;
            int num8 = random.Next(200, 500);
            double num9 = 0.0;
            double num10 = 0.2;
            string string_ = TextResolver.GetText("PreWarp");
            List<object> list = new List<object>();
            list.Add(galaxyShape);
            list.Add(num2);
            list.Add(num3);
            list.Add(flag);
            list.Add(num7);
            list.Add(num8);
            list.Add(num9);
            list.Add(num10);
            list.Add(num5);
            list.Add(method_57(string_));
            list.Add(num6);
            list.Add(empireStart);
            list.Add(empireStartList);
            list.Add(item4);
            list.Add(item);
            list.Add(item2);
            list.Add(flag2);
            list.Add(num);
            list.Add(double_0);
            list.Add(true);
            list.Add(null);
            method_8(TextResolver.GetText("Creating new Galaxy..."));
            method_120();
            base.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            oyxRtRyAwjg.RunWorkerAsync(list);
            Cursor.Current = Cursors.WaitCursor;
            while (oyxRtRyAwjg.IsBusy)
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(30);
            }
            Cursor.Current = Cursors.Default;
            if (game_0 != null)
            {
                method_77(game_0);
            }
        }

        private void method_123()
        {
            method_149();
            method_140();
            main_0.bool_7 = true;
            main_0.string_1 = "DealingWithPirates";
            GalaxyShape galaxyShape = GalaxyShape.Elliptical;
            bool flag = true;
            EmpireVictoryConditions item = null;
            EmpireVictoryConditions item2 = null;
            bool flag2 = false;
            int num = 0;
            Random random = new Random((int)DateTime.Now.Ticks);
            int num2 = 700;
            int num3 = 20;
            int num4 = random.Next(15, 20);
            EmpireStartList empireStartList = new EmpireStartList();
            EmpireStart empireStart = method_112(null, empireStartList, TextResolver.GetText("PreWarp"), TextResolver.GetText("PreWarp"), random);
            bool_2 = false;
            bool_3 = true;
            for (int i = 0; i < num4; i++)
            {
                EmpireStart item3 = method_112(empireStart, empireStartList, TextResolver.GetText("PreWarp"), TextResolver.GetText("PreWarp"), random);
                empireStartList.Add(item3);
            }
            double num5 = meEawywtba(2);
            double num6 = 1.1;
            VictoryConditions item4 = method_106(num3);
            double num7 = 1.0;
            int num8 = random.Next(200, 500);
            double num9 = 0.0;
            double num10 = 0.4;
            string string_ = TextResolver.GetText("PreWarp");
            List<object> list = new List<object>();
            list.Add(galaxyShape);
            list.Add(num2);
            list.Add(num3);
            list.Add(flag);
            list.Add(num7);
            list.Add(num8);
            list.Add(num9);
            list.Add(num10);
            list.Add(num5);
            list.Add(method_57(string_));
            list.Add(num6);
            list.Add(empireStart);
            list.Add(empireStartList);
            list.Add(item4);
            list.Add(item);
            list.Add(item2);
            list.Add(flag2);
            list.Add(num);
            list.Add(double_0);
            list.Add(true);
            list.Add(null);
            method_8(TextResolver.GetText("Creating new Galaxy..."));
            method_120();
            base.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            oyxRtRyAwjg.RunWorkerAsync(list);
            Cursor.Current = Cursors.WaitCursor;
            while (oyxRtRyAwjg.IsBusy)
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(30);
            }
            Cursor.Current = Cursors.Default;
            if (game_0 != null)
            {
                method_77(game_0);
            }
        }

        private void lnkTutorialEmpireAndColonies_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            method_140();
            main_0.bool_7 = true;
            main_0.string_1 = "EmpireAndColonies";
            GalaxyShape galaxyShape = GalaxyShape.Elliptical;
            bool flag = true;
            EmpireVictoryConditions item = null;
            EmpireVictoryConditions item2 = null;
            bool flag2 = false;
            int num = 0;
            Random random = new Random((int)DateTime.Now.Ticks);
            int num2 = 700;
            int num3 = 20;
            int num4 = random.Next(15, 20);
            EmpireStartList empireStartList = new EmpireStartList();
            EmpireStart empireStart = method_112(null, empireStartList, TextResolver.GetText("Young"), string.Format(TextResolver.GetText("Level X"), "2"), random);
            for (int i = 0; i < num4; i++)
            {
                EmpireStart item3 = method_112(empireStart, empireStartList, TextResolver.GetText("Expanding"), string.Format(TextResolver.GetText("Level X"), "2"), random);
                empireStartList.Add(item3);
            }
            double num5 = meEawywtba(2);
            double num6 = 1.1;
            VictoryConditions item4 = method_106(num3);
            double num7 = 1.0;
            int num8 = random.Next(200, 500);
            double num9 = 0.0;
            double num10 = 0.4;
            string string_ = TextResolver.GetText("Expanding");
            List<object> list = new List<object>();
            list.Add(galaxyShape);
            list.Add(num2);
            list.Add(num3);
            list.Add(flag);
            list.Add(num7);
            list.Add(num8);
            list.Add(num9);
            list.Add(num10);
            list.Add(num5);
            list.Add(method_57(string_));
            list.Add(num6);
            list.Add(empireStart);
            list.Add(empireStartList);
            list.Add(item4);
            list.Add(item);
            list.Add(item2);
            list.Add(flag2);
            list.Add(num);
            list.Add(double_0);
            list.Add(true);
            list.Add(null);
            method_8(TextResolver.GetText("Creating new Galaxy..."));
            method_120();
            base.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            oyxRtRyAwjg.RunWorkerAsync(list);
            Cursor.Current = Cursors.WaitCursor;
            while (oyxRtRyAwjg.IsBusy)
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(30);
            }
            Cursor.Current = Cursors.Default;
            if (game_0 != null)
            {
                method_77(game_0);
            }
        }

        private void lnkTutorialShipsMissions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            method_140();
            main_0.bool_7 = true;
            main_0.string_1 = "ShipsAndBases";
            GalaxyShape galaxyShape = GalaxyShape.Elliptical;
            bool flag = true;
            EmpireVictoryConditions item = null;
            EmpireVictoryConditions item2 = null;
            bool flag2 = false;
            int num = 0;
            Random random = new Random((int)DateTime.Now.Ticks);
            int num2 = 700;
            int num3 = 20;
            int num4 = random.Next(15, 20);
            EmpireStartList empireStartList = new EmpireStartList();
            EmpireStart empireStart = method_112(null, empireStartList, TextResolver.GetText("Young"), string.Format(TextResolver.GetText("Level X"), "2"), random);
            for (int i = 0; i < num4; i++)
            {
                EmpireStart item3 = method_112(empireStart, empireStartList, TextResolver.GetText("Expanding"), string.Format(TextResolver.GetText("Level X"), "2"), random);
                empireStartList.Add(item3);
            }
            double num5 = meEawywtba(2);
            double num6 = 1.1;
            VictoryConditions item4 = method_106(num3);
            double num7 = 1.0;
            int num8 = random.Next(200, 500);
            double num9 = 0.0;
            double num10 = 0.4;
            string string_ = TextResolver.GetText("Expanding");
            List<object> list = new List<object>();
            list.Add(galaxyShape);
            list.Add(num2);
            list.Add(num3);
            list.Add(flag);
            list.Add(num7);
            list.Add(num8);
            list.Add(num9);
            list.Add(num10);
            list.Add(num5);
            list.Add(method_57(string_));
            list.Add(num6);
            list.Add(empireStart);
            list.Add(empireStartList);
            list.Add(item4);
            list.Add(item);
            list.Add(item2);
            list.Add(flag2);
            list.Add(num);
            list.Add(double_0);
            list.Add(true);
            list.Add(null);
            method_8(TextResolver.GetText("Creating new Galaxy..."));
            method_120();
            base.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            oyxRtRyAwjg.RunWorkerAsync(list);
            Cursor.Current = Cursors.WaitCursor;
            while (oyxRtRyAwjg.IsBusy)
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(30);
            }
            Cursor.Current = Cursors.Default;
            if (game_0 != null)
            {
                method_77(game_0);
            }
        }

        private void lnkTutorialFindingYourWayAround_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            method_140();
            main_0.bool_7 = true;
            main_0.string_1 = "FindingYourWayAround";
            GalaxyShape galaxyShape = GalaxyShape.Elliptical;
            bool flag = true;
            EmpireVictoryConditions item = null;
            EmpireVictoryConditions item2 = null;
            bool flag2 = false;
            int num = 0;
            Random random = new Random((int)DateTime.Now.Ticks);
            int num2 = 700;
            int num3 = 20;
            int num4 = random.Next(15, 20);
            EmpireStartList empireStartList = new EmpireStartList();
            EmpireStart empireStart = method_112(null, empireStartList, TextResolver.GetText("Young"), string.Format(TextResolver.GetText("Level X"), "2"), random);
            for (int i = 0; i < num4; i++)
            {
                EmpireStart item3 = method_112(empireStart, empireStartList, TextResolver.GetText("Expanding"), string.Format(TextResolver.GetText("Level X"), "2"), random);
                empireStartList.Add(item3);
            }
            double num5 = meEawywtba(2);
            double num6 = 1.1;
            VictoryConditions item4 = method_106(num3);
            double num7 = 1.0;
            int num8 = random.Next(200, 500);
            double num9 = 0.0;
            double num10 = 0.3;
            string string_ = TextResolver.GetText("Expanding");
            List<object> list = new List<object>();
            list.Add(galaxyShape);
            list.Add(num2);
            list.Add(num3);
            list.Add(flag);
            list.Add(num7);
            list.Add(num8);
            list.Add(num9);
            list.Add(num10);
            list.Add(num5);
            list.Add(method_57(string_));
            list.Add(num6);
            list.Add(empireStart);
            list.Add(empireStartList);
            list.Add(item4);
            list.Add(item);
            list.Add(item2);
            list.Add(flag2);
            list.Add(num);
            list.Add(double_0);
            list.Add(true);
            list.Add(null);
            method_8(TextResolver.GetText("Creating new Galaxy..."));
            method_120();
            base.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            oyxRtRyAwjg.RunWorkerAsync(list);
            Cursor.Current = Cursors.WaitCursor;
            while (oyxRtRyAwjg.IsBusy)
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(30);
            }
            Cursor.Current = Cursors.Default;
            if (game_0 != null)
            {
                method_77(game_0);
            }
        }

        private void lnkTutorialResearchDesign_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            method_140();
            main_0.bool_7 = true;
            main_0.string_1 = "ResearchDesign";
            GalaxyShape galaxyShape = GalaxyShape.Elliptical;
            bool flag = true;
            EmpireVictoryConditions item = null;
            EmpireVictoryConditions item2 = null;
            bool flag2 = false;
            int num = 0;
            Random random = new Random((int)DateTime.Now.Ticks);
            int num2 = 700;
            int num3 = 20;
            int num4 = random.Next(15, 20);
            EmpireStartList empireStartList = new EmpireStartList();
            EmpireStart empireStart = method_112(null, empireStartList, TextResolver.GetText("Expanding"), string.Format(TextResolver.GetText("Level X"), "2"), random);
            for (int i = 0; i < num4; i++)
            {
                EmpireStart item3 = method_112(empireStart, empireStartList, TextResolver.GetText("Expanding"), string.Format(TextResolver.GetText("Level X"), "2"), random);
                empireStartList.Add(item3);
            }
            double num5 = meEawywtba(2);
            double num6 = 1.1;
            VictoryConditions item4 = method_106(num3);
            double num7 = 1.0;
            int num8 = random.Next(200, 500);
            double num9 = 0.0;
            double num10 = 0.3;
            string string_ = TextResolver.GetText("Expanding");
            List<object> list = new List<object>();
            list.Add(galaxyShape);
            list.Add(num2);
            list.Add(num3);
            list.Add(flag);
            list.Add(num7);
            list.Add(num8);
            list.Add(num9);
            list.Add(num10);
            list.Add(num5);
            list.Add(method_57(string_));
            list.Add(num6);
            list.Add(empireStart);
            list.Add(empireStartList);
            list.Add(item4);
            list.Add(item);
            list.Add(item2);
            list.Add(flag2);
            list.Add(num);
            list.Add(double_0);
            list.Add(true);
            list.Add(null);
            method_8(TextResolver.GetText("Creating new Galaxy..."));
            method_120();
            base.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            oyxRtRyAwjg.RunWorkerAsync(list);
            Cursor.Current = Cursors.WaitCursor;
            while (oyxRtRyAwjg.IsBusy)
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(30);
            }
            Cursor.Current = Cursors.Default;
            if (game_0 == null)
            {
                return;
            }
            Habitat capital = game_0.PlayerEmpire.Capital;
            ShipGroup shipGroup = new ShipGroup(game_0.Galaxy);
            shipGroup.Name = string.Format(TextResolver.GetText("Nth Fleet"), game_0.PlayerEmpire.GetNextFleetNumberDescription());
            shipGroup.Empire = game_0.PlayerEmpire;
            shipGroup.GatherPoint = capital;
            Design design = game_0.PlayerEmpire.Designs.FindNewest(BuiltObjectSubRole.Frigate);
            Design design2 = game_0.PlayerEmpire.Designs.FindNewest(BuiltObjectSubRole.Destroyer);
            BuiltObjectList items = new BuiltObjectList();
            for (int j = 0; j < 5; j++)
            {
                double num11 = 0.0;
                double num12 = 0.0;
                design.BuildCount++;
                BuiltObject builtObject = game_0.PlayerEmpire.GenerateBuiltObjectFromDesign(design, game_0.Galaxy.GenerateBuiltObjectName(design), isState: true, capital.Xpos + 50.0, capital.Ypos + 100.0);
                builtObject.ParentHabitat = capital;
                builtObject.DateBuilt = game_0.Galaxy.CurrentStarDate;
                builtObject.DateRetrofit = game_0.Galaxy.CurrentStarDate;
                game_0.Galaxy.SelectRelativeParkingPoint(out num11, out num12);
                builtObject.ParentOffsetX = num11;
                builtObject.ParentOffsetY = num12;
                builtObject.Xpos = capital.Xpos + num11;
                builtObject.Ypos = capital.Ypos + num12;
                builtObject.Heading = game_0.Galaxy.SelectRandomHeading();
                builtObject.CurrentFuel = builtObject.FuelCapacity;
                builtObject.CurrentEnergy = builtObject.ReactorStorageCapacity;
                builtObject.CurrentShields = builtObject.ShieldsCapacity;
                shipGroup.AddShipToFleet(builtObject);
            }
            for (int k = 0; k < 3; k++)
            {
                double num13 = 0.0;
                double num14 = 0.0;
                design2.BuildCount++;
                BuiltObject builtObject2 = game_0.PlayerEmpire.GenerateBuiltObjectFromDesign(design2, game_0.Galaxy.GenerateBuiltObjectName(design2), isState: true, capital.Xpos + 50.0, capital.Ypos + 100.0);
                builtObject2.ParentHabitat = capital;
                builtObject2.DateBuilt = game_0.Galaxy.CurrentStarDate;
                builtObject2.DateRetrofit = game_0.Galaxy.CurrentStarDate;
                game_0.Galaxy.SelectRelativeParkingPoint(out num13, out num14);
                builtObject2.ParentOffsetX = num13;
                builtObject2.ParentOffsetY = num14;
                builtObject2.Xpos = capital.Xpos + num13;
                builtObject2.Ypos = capital.Ypos + num14;
                builtObject2.Heading = game_0.Galaxy.SelectRandomHeading();
                builtObject2.CurrentFuel = builtObject2.FuelCapacity;
                builtObject2.CurrentEnergy = builtObject2.ReactorStorageCapacity;
                builtObject2.CurrentShields = builtObject2.ShieldsCapacity;
                shipGroup.AddShipToFleet(builtObject2);
            }
            shipGroup.Ships.AddRange(items);
            shipGroup.Update();
            game_0.PlayerEmpire.ShipGroups.Add(shipGroup);
            for (int l = 0; l < 2; l++)
            {
                bool isRandomCharacter = false;
                game_0.PlayerEmpire.GenerateNewCharacter(CharacterRole.IntelligenceAgent, game_0.PlayerEmpire.Capital, out isRandomCharacter);
            }
            DiplomaticRelation diplomaticRelation = game_0.PlayerEmpire.DiplomaticRelations[game_0.Galaxy.Empires[1]];
            if (diplomaticRelation == null)
            {
                diplomaticRelation = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.PlayerEmpire, game_0.Galaxy.Empires[1], tradeRestrictedResources: false);
                game_0.PlayerEmpire.DiplomaticRelations.Add(diplomaticRelation);
                DiplomaticRelation diplomaticRelation2 = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.Galaxy.Empires[1], game_0.PlayerEmpire, tradeRestrictedResources: false);
                game_0.Galaxy.Empires[1].DiplomaticRelations.Add(diplomaticRelation2);
            }
            else if (diplomaticRelation.Type == DiplomaticRelationType.NotMet)
            {
                diplomaticRelation.Type = DiplomaticRelationType.None;
                DiplomaticRelation diplomaticRelation3 = game_0.Galaxy.Empires[1].DiplomaticRelations[game_0.PlayerEmpire];
                if (diplomaticRelation3 != null)
                {
                    diplomaticRelation3.Type = DiplomaticRelationType.None;
                }
                else
                {
                    diplomaticRelation3 = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.Galaxy.Empires[1], game_0.PlayerEmpire, tradeRestrictedResources: false);
                    game_0.Galaxy.Empires[1].DiplomaticRelations.Add(diplomaticRelation3);
                }
            }
            diplomaticRelation = game_0.PlayerEmpire.DiplomaticRelations[game_0.Galaxy.Empires[2]];
            if (diplomaticRelation == null)
            {
                diplomaticRelation = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.PlayerEmpire, game_0.Galaxy.Empires[2], tradeRestrictedResources: false);
                game_0.PlayerEmpire.DiplomaticRelations.Add(diplomaticRelation);
                DiplomaticRelation diplomaticRelation4 = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.Galaxy.Empires[2], game_0.PlayerEmpire, tradeRestrictedResources: false);
                game_0.Galaxy.Empires[2].DiplomaticRelations.Add(diplomaticRelation4);
            }
            else if (diplomaticRelation.Type == DiplomaticRelationType.NotMet)
            {
                diplomaticRelation.Type = DiplomaticRelationType.None;
                DiplomaticRelation diplomaticRelation5 = game_0.Galaxy.Empires[2].DiplomaticRelations[game_0.PlayerEmpire];
                if (diplomaticRelation5 != null)
                {
                    diplomaticRelation5.Type = DiplomaticRelationType.None;
                }
                else
                {
                    diplomaticRelation5 = new DiplomaticRelation(DiplomaticRelationType.None, game_0.PlayerEmpire, game_0.Galaxy.Empires[2], game_0.PlayerEmpire, tradeRestrictedResources: false);
                    game_0.Galaxy.Empires[2].DiplomaticRelations.Add(diplomaticRelation5);
                }
            }
            method_77(game_0);
        }
    }
}
