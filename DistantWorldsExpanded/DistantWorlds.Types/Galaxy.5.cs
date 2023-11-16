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
        public int ValueEndWarWithEmpire(Empire requestingEmpire, Empire empire, Empire targetEmpire)
        {
            long num = -1L;
            DiplomaticRelation diplomaticRelation = empire.ObtainDiplomaticRelation(targetEmpire);
            if (diplomaticRelation.OtherEmpire != empire && diplomaticRelation.Type != 0)
            {
                DiplomaticRelation diplomaticRelation2 = requestingEmpire.ObtainDiplomaticRelation(diplomaticRelation.OtherEmpire);
                if (diplomaticRelation2 != null && diplomaticRelation2.Type != 0 && diplomaticRelation.Type == DiplomaticRelationType.War)
                {
                    EmpireEvaluation empireEvaluation = requestingEmpire.ObtainEmpireEvaluation(targetEmpire);
                    int num2 = 30;
                    if (empireEvaluation.OverallAttitude > num2)
                    {
                        int num3 = 0;
                        EmpireEvaluation empireEvaluation2 = empire.ObtainEmpireEvaluation(requestingEmpire);
                        if (empireEvaluation2 != null)
                        {
                            num3 = empireEvaluation2.OverallAttitude;
                        }
                        num3 = 60 - num3;
                        if (num3 < 20)
                        {
                            num3 = 20;
                        }
                        double val = (double)empire.WeightedMilitaryPotency / (double)requestingEmpire.WeightedMilitaryPotency;
                        double num4 = (double)(empireEvaluation.OverallAttitude - num2) / 20.0;
                        val = Math.Min(Math.Max(val, 0.5), 5.0);
                        num = (long)((double)num3 * val * num4 * 2000.0);
                    }
                }
            }
            num = (long)((double)num * GetMoneyRate());
            if (num > 1073741823)
            {
                num = 1073741823L;
            }
            return (int)num;
        }

        public int ValueLiftTradeSanctionsAgainstUs(Empire attackingEmpire, Empire targetEmpire)
        {
            long num = -1L;
            if (attackingEmpire != targetEmpire)
            {
                DiplomaticRelation diplomaticRelation = attackingEmpire.ObtainDiplomaticRelation(targetEmpire);
                if (diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions)
                {
                    num = 5000L;
                    double num2 = (double)attackingEmpire.WeightedMilitaryPotency / (double)targetEmpire.WeightedMilitaryPotency;
                    if (targetEmpire == PlayerEmpire)
                    {
                        num2 *= PlayerEmpire.DifficultyLevel * PlayerEmpire.DifficultyLevel;
                    }
                    num2 = Math.Min(Math.Max(num2, 0.5), 5.0);
                    num = (long)(num2 * 15000.0);
                }
            }
            if (num > 1073741823)
            {
                num = 1073741823L;
            }
            return (int)num;
        }

        public int ValueLiftTradeSanctionsAgainstEmpire(Empire requestingEmpire, Empire empire, Empire targetEmpire)
        {
            long num = -1L;
            DiplomaticRelation diplomaticRelation = empire.ObtainDiplomaticRelation(targetEmpire);
            if (diplomaticRelation.OtherEmpire != empire && diplomaticRelation.Type != 0)
            {
                DiplomaticRelation diplomaticRelation2 = requestingEmpire.ObtainDiplomaticRelation(diplomaticRelation.OtherEmpire);
                if (diplomaticRelation2 != null && diplomaticRelation2.Type != 0 && diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions)
                {
                    EmpireEvaluation empireEvaluation = requestingEmpire.ObtainEmpireEvaluation(targetEmpire);
                    int num2 = 30;
                    if (empireEvaluation.OverallAttitude > num2)
                    {
                        int num3 = 0;
                        EmpireEvaluation empireEvaluation2 = empire.EmpireEvaluations[requestingEmpire];
                        if (empireEvaluation2 != null)
                        {
                            num3 = empireEvaluation2.OverallAttitude;
                        }
                        num3 = 60 - num3;
                        if (num3 < 20)
                        {
                            num3 = 20;
                        }
                        double val = (double)empire.WeightedMilitaryPotency / (double)requestingEmpire.WeightedMilitaryPotency;
                        double num4 = (double)(empireEvaluation.OverallAttitude - num2) / 20.0;
                        val = Math.Min(Math.Max(val, 0.5), 5.0);
                        num = (long)((double)num3 * val * num4 * 900.0);
                    }
                }
            }
            num = (long)((double)num * GetMoneyRate());
            if (num > 1073741823)
            {
                num = 1073741823L;
            }
            return (int)num;
        }

        public static int DetermineRequiredTroopStrength(Empire empire, object target)
        {
            int num = 0;
            int num2 = 35000;
            int num3 = 0;
            if (target != null && target is Habitat)
            {
                Habitat habitat = (Habitat)target;
                bool isDefending = false;
                num3 = habitat.CalculatePopulationStrength(out isDefending);
                if (empire.IsObjectVisibleToThisEmpire(habitat))
                {
                    if (habitat.Troops != null)
                    {
                        num = habitat.Troops.TotalDefendStrength + num2 + num3;
                        if (habitat.DefensiveFortressBonus > 0)
                        {
                            num = (int)((double)num * (1.0 + (double)(int)habitat.DefensiveFortressBonus / 10.0));
                        }
                    }
                }
                else
                {
                    num = habitat.TroopLevelRequired * 100 + num2 + num3;
                    if (habitat.DefensiveFortressBonus > 0)
                    {
                        num = (int)((double)num * (1.0 + (double)(int)habitat.DefensiveFortressBonus / 10.0));
                    }
                }
            }
            if (empire != null)
            {
                num = (int)((double)num * empire.Policy.InvasionOverkillFactor);
            }
            return num;
        }

        public int DetermineMaximumConstructionSizeForYard(ResearchNodeList researchProjects, ResearchNode researchProject)
        {
            return researchProjects.CheckAncestorsForAbility(researchProject, ResearchAbilityType.ConstructionSize)?.Value ?? 160;
        }

        public void CheckCancelWonderBuilding(PlanetaryFacility completedWonder)
        {
            lock (_WonderLockObject)
            {
                for (int i = 0; i < Empires.Count; i++)
                {
                    Empire empire = Empires[i];
                    if (empire == null || !empire.Active)
                    {
                        continue;
                    }
                    for (int j = 0; j < empire.Colonies.Count; j++)
                    {
                        Habitat habitat = empire.Colonies[j];
                        if (habitat == null || habitat.HasBeenDestroyed)
                        {
                            continue;
                        }
                        PlanetaryFacilityList planetaryFacilityList = new PlanetaryFacilityList();
                        for (int k = 0; k < habitat.Facilities.Count; k++)
                        {
                            PlanetaryFacility planetaryFacility = habitat.Facilities[k];
                            if (planetaryFacility == null || !(planetaryFacility.ConstructionProgress < 1f) || planetaryFacility.Type != PlanetaryFacilityType.Wonder || planetaryFacility.PlanetaryFacilityDefinitionId != completedWonder.PlanetaryFacilityDefinitionId)
                            {
                                continue;
                            }
                            ResearchNode researchNode = null;
                            double num = 0.0;
                            string text = string.Empty;
                            switch (planetaryFacility.WonderType)
                            {
                                case WonderType.ColonyConstructionSpeed:
                                    text = TextResolver.GetText("Wonder Cancel Substitute Research");
                                    researchNode = empire.Research.SelectRandomNextResearchProjectExcludeSuperWeapons(this, ComponentCategoryType.Construction);
                                    if (researchNode != null)
                                    {
                                        text = string.Format(TextResolver.GetText("Wonder Cancel Substitute Research"), researchNode.Name);
                                        float num5 = (float)(60000.0 + Rnd.NextDouble() * 20000.0);
                                        researchNode.Progress += num5;
                                        if (researchNode.Progress >= researchNode.Cost)
                                        {
                                            empire.DoResearchBreakthrough(researchNode, selfResearched: true);
                                        }
                                    }
                                    break;
                                case WonderType.ColonyDefense:
                                    {
                                        PlanetaryFacility planetaryFacility2 = habitat.Facilities.FindByType(PlanetaryFacilityType.FortifiedBunker);
                                        if (planetaryFacility2 == null)
                                        {
                                            habitat.QueueFacilityConstruction(PlanetaryFacilityType.FortifiedBunker);
                                            PlanetaryFacility planetaryFacility3 = habitat.Facilities.FindByType(PlanetaryFacilityType.FortifiedBunker);
                                            if (planetaryFacility3 != null)
                                            {
                                                planetaryFacility3.ConstructionProgress = 1f;
                                                habitat.ReviewPlanetaryFacilities(habitat.Empire);
                                                habitat.Empire.RefreshColonyFacilityInfo();
                                                text = string.Format(TextResolver.GetText("Wonder Cancel Substitute Facility"), planetaryFacility3.Name);
                                            }
                                        }
                                        else
                                        {
                                            num = 25000.0;
                                            empire.StateMoney += num;
                                            text = string.Format(TextResolver.GetText("Wonder Cancel Substitute Money"), num.ToString("###,###,###,##0"));
                                        }
                                        break;
                                    }
                                case WonderType.ColonyHappiness:
                                    num = 25000.0;
                                    empire.StateMoney += num;
                                    text = string.Format(TextResolver.GetText("Wonder Cancel Substitute Money"), num.ToString("###,###,###,##0"));
                                    break;
                                case WonderType.ColonyIncome:
                                    num = 25000.0;
                                    empire.StateMoney += num;
                                    text = string.Format(TextResolver.GetText("Wonder Cancel Substitute Money"), num.ToString("###,###,###,##0"));
                                    break;
                                case WonderType.ColonyPopulationGrowth:
                                    num = 25000.0;
                                    empire.StateMoney += num;
                                    text = string.Format(TextResolver.GetText("Wonder Cancel Substitute Money"), num.ToString("###,###,###,##0"));
                                    break;
                                case WonderType.EmpireHappiness:
                                    num = 50000.0;
                                    empire.StateMoney += num;
                                    text = string.Format(TextResolver.GetText("Wonder Cancel Substitute Money"), num.ToString("###,###,###,##0"));
                                    break;
                                case WonderType.EmpireIncome:
                                    num = 50000.0;
                                    empire.StateMoney += num;
                                    text = string.Format(TextResolver.GetText("Wonder Cancel Substitute Money"), num.ToString("###,###,###,##0"));
                                    break;
                                case WonderType.EmpirePopulationGrowth:
                                    num = 50000.0;
                                    empire.StateMoney += num;
                                    text = string.Format(TextResolver.GetText("Wonder Cancel Substitute Money"), num.ToString("###,###,###,##0"));
                                    break;
                                case WonderType.EmpireResearchEnergy:
                                    researchNode = empire.Research.SelectRandomNextResearchProjectExcludeSuperWeapons(this, IndustryType.Energy);
                                    if (researchNode != null)
                                    {
                                        text = string.Format(TextResolver.GetText("Wonder Cancel Substitute Research"), researchNode.Name);
                                        float num4 = (float)(100000.0 + Rnd.NextDouble() * 40000.0);
                                        researchNode.Progress += num4;
                                        if (researchNode.Progress >= researchNode.Cost)
                                        {
                                            empire.DoResearchBreakthrough(researchNode, selfResearched: true);
                                        }
                                    }
                                    break;
                                case WonderType.EmpireResearchHighTech:
                                    researchNode = empire.Research.SelectRandomNextResearchProjectExcludeSuperWeapons(this, IndustryType.HighTech);
                                    if (researchNode != null)
                                    {
                                        text = string.Format(TextResolver.GetText("Wonder Cancel Substitute Research"), researchNode.Name);
                                        float num3 = (float)(100000.0 + Rnd.NextDouble() * 40000.0);
                                        researchNode.Progress += num3;
                                        if (researchNode.Progress >= researchNode.Cost)
                                        {
                                            empire.DoResearchBreakthrough(researchNode, selfResearched: true);
                                        }
                                    }
                                    break;
                                case WonderType.EmpireResearchWeapons:
                                    researchNode = empire.Research.SelectRandomNextResearchProjectExcludeSuperWeapons(this, IndustryType.Weapon);
                                    if (researchNode != null)
                                    {
                                        text = string.Format(TextResolver.GetText("Wonder Cancel Substitute Research"), researchNode.Name);
                                        float num2 = (float)(100000.0 + Rnd.NextDouble() * 40000.0);
                                        researchNode.Progress += num2;
                                        if (researchNode.Progress >= researchNode.Cost)
                                        {
                                            empire.DoResearchBreakthrough(researchNode, selfResearched: true);
                                        }
                                    }
                                    break;
                            }
                            PlanetaryFacilityDefinition byId = PlanetaryFacilityDefinitionsStatic.GetById(planetaryFacility.PlanetaryFacilityDefinitionId);
                            string text2 = string.Format(TextResolver.GetText("Construction of the WONDER at COLONY has been cancelled"), planetaryFacility.Name, habitat.Name);
                            if (!string.IsNullOrEmpty(text))
                            {
                                text2 = text2 + ". " + text;
                            }
                            empire.SendMessageToEmpire(empire, EmpireMessageType.ColonyFacilityCancelled, byId, text2, new Point((int)habitat.Xpos, (int)habitat.Ypos), string.Empty);
                            planetaryFacilityList.Add(planetaryFacility);
                        }
                        for (int l = 0; l < planetaryFacilityList.Count; l++)
                        {
                            habitat.Facilities.Remove(planetaryFacilityList[l]);
                            habitat.CheckRemoveFacilityTracking(planetaryFacilityList[l]);
                        }
                    }
                }
            }
        }

        public bool CheckWonderBuilt(PlanetaryFacilityDefinition wonder)
        {
            if (wonder != null)
            {
                lock (_WonderLockObject)
                {
                    return _WondersBuilt[wonder.PlanetaryFacilityDefinitionId];
                }
            }
            return false;
        }

        public void ReviewWondersBuilt()
        {
            lock (_WonderLockObject)
            {
                _WondersBuilt = new bool[PlanetaryFacilityDefinitionsStatic.Count];
                for (int i = 0; i < Empires.Count; i++)
                {
                    Empire empire = Empires[i];
                    if (empire == null || !empire.Active)
                    {
                        continue;
                    }
                    for (int j = 0; j < empire.Colonies.Count; j++)
                    {
                        Habitat habitat = empire.Colonies[j];
                        if (habitat == null || habitat.HasBeenDestroyed)
                        {
                            continue;
                        }
                        for (int k = 0; k < habitat.Facilities.Count; k++)
                        {
                            PlanetaryFacility planetaryFacility = habitat.Facilities[k];
                            if (planetaryFacility != null && planetaryFacility.ConstructionProgress >= 1f && planetaryFacility.Type == PlanetaryFacilityType.Wonder)
                            {
                                _WondersBuilt[planetaryFacility.PlanetaryFacilityDefinitionId] = true;
                            }
                        }
                    }
                }
                for (int l = 0; l < PirateEmpires.Count; l++)
                {
                    Empire empire2 = PirateEmpires[l];
                    if (empire2 == null || !empire2.Active)
                    {
                        continue;
                    }
                    for (int m = 0; m < empire2.Colonies.Count; m++)
                    {
                        Habitat habitat2 = empire2.Colonies[m];
                        if (habitat2 == null || habitat2.HasBeenDestroyed || habitat2.Owner != empire2)
                        {
                            continue;
                        }
                        for (int n = 0; n < habitat2.Facilities.Count; n++)
                        {
                            PlanetaryFacility planetaryFacility2 = habitat2.Facilities[n];
                            if (planetaryFacility2 != null && planetaryFacility2.ConstructionProgress >= 1f && planetaryFacility2.Type == PlanetaryFacilityType.Wonder)
                            {
                                _WondersBuilt[planetaryFacility2.PlanetaryFacilityDefinitionId] = true;
                            }
                        }
                    }
                }
            }
        }

        public void ResolveFighterDescription(Empire empire, FighterSpecification fighter, ResearchNode project, out string[] descriptions, out string[] values)
        {
            descriptions = new string[9];
            values = new string[9];
            descriptions[0] = fighter.Name;
            descriptions[1] = TextResolver.GetText("Top Speed");
            descriptions[2] = TextResolver.GetText("Turn Rate");
            descriptions[3] = TextResolver.GetText("Shields");
            descriptions[4] = TextResolver.GetText("Targetting");
            descriptions[5] = TextResolver.GetText("Countermeasures");
            values[1] = fighter.TopSpeed.ToString();
            string text = ((double)fighter.TurnRate * (180.0 / Math.PI)).ToString("##0") + "°/" + TextResolver.GetText("second abbreviation");
            values[2] = text;
            values[3] = fighter.ShieldsCapacity.ToString();
            values[4] = fighter.TargettingModifier + "%";
            values[5] = fighter.CountermeasureModifier + "%";
            if (fighter.Type == FighterType.Bomber)
            {
                descriptions[6] = TextResolver.GetText("Bombing Damage");
                descriptions[7] = TextResolver.GetText("Bombing Range");
                descriptions[8] = TextResolver.GetText("Bombing Fire Rate");
                values[6] = fighter.WeaponDamage.ToString();
                values[7] = fighter.WeaponRange.ToString();
                values[8] = ((double)fighter.WeaponFireRate / 1000.0).ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
            }
            else if (fighter.Type == FighterType.Interceptor)
            {
                descriptions[6] = TextResolver.GetText("Weapons Damage");
                descriptions[7] = TextResolver.GetText("Weapons Range");
                descriptions[8] = TextResolver.GetText("Weapons Fire Rate");
                values[6] = fighter.WeaponDamage.ToString();
                values[7] = fighter.WeaponRange.ToString();
                values[8] = ((double)fighter.WeaponFireRate / 1000.0).ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
            }
        }

        public string ResolveWonderDescriptionShort(PlanetaryFacilityDefinition planetaryFacility)
        {
            string result = string.Empty;
            if (planetaryFacility != null)
            {
                string empty = string.Empty;
                switch (planetaryFacility.WonderType)
                {
                    case WonderType.ColonyConstructionSpeed:
                        empty = "+" + planetaryFacility.Value2 + "% " + TextResolver.GetText("Construction Speed");
                        result = string.Format(TextResolver.GetText("Wonder Description Colony"), planetaryFacility.Name, empty);
                        break;
                    case WonderType.ColonyDefense:
                        empty = "+" + planetaryFacility.Value2 * 10 + "% " + TextResolver.GetText("Colony Defense");
                        result = string.Format(TextResolver.GetText("Wonder Description Colony"), planetaryFacility.Name, empty);
                        break;
                    case WonderType.ColonyHappiness:
                        empty = "+" + planetaryFacility.Value2 + "% " + TextResolver.GetText("Colony Happiness");
                        result = string.Format(TextResolver.GetText("Wonder Description Colony"), planetaryFacility.Name, empty);
                        break;
                    case WonderType.ColonyIncome:
                        empty = "+" + planetaryFacility.Value2 + "% " + TextResolver.GetText("Colony Income");
                        result = string.Format(TextResolver.GetText("Wonder Description Colony"), planetaryFacility.Name, empty);
                        break;
                    case WonderType.ColonyPopulationGrowth:
                        empty = "+" + planetaryFacility.Value2 + "% " + TextResolver.GetText("Population Growth");
                        result = string.Format(TextResolver.GetText("Wonder Description Colony"), planetaryFacility.Name, empty);
                        break;
                    case WonderType.EmpireHappiness:
                        empty = "+" + planetaryFacility.Value2 + "% " + TextResolver.GetText("Empire Happiness");
                        result = string.Format(TextResolver.GetText("Wonder Description Empire"), planetaryFacility.Name, empty);
                        break;
                    case WonderType.EmpireIncome:
                        empty = "+" + planetaryFacility.Value2 + "% " + TextResolver.GetText("Empire Income");
                        result = string.Format(TextResolver.GetText("Wonder Description Empire"), planetaryFacility.Name, empty);
                        break;
                    case WonderType.EmpirePopulationGrowth:
                        empty = "+" + planetaryFacility.Value2 + "% " + TextResolver.GetText("Population Growth");
                        result = string.Format(TextResolver.GetText("Wonder Description Empire"), planetaryFacility.Name, empty);
                        break;
                    case WonderType.EmpireResearchEnergy:
                        empty = "+" + planetaryFacility.Value2 + "% " + TextResolver.GetText("Energy Research");
                        result = string.Format(TextResolver.GetText("Wonder Description Empire"), planetaryFacility.Name, empty);
                        break;
                    case WonderType.EmpireResearchHighTech:
                        empty = "+" + planetaryFacility.Value2 + "% " + TextResolver.GetText("HighTech Research");
                        result = string.Format(TextResolver.GetText("Wonder Description Empire"), planetaryFacility.Name, empty);
                        break;
                    case WonderType.EmpireResearchWeapons:
                        empty = "+" + planetaryFacility.Value2 + "% " + TextResolver.GetText("Weapons Research");
                        result = string.Format(TextResolver.GetText("Wonder Description Empire"), planetaryFacility.Name, empty);
                        break;
                    case WonderType.RaceAchievement:
                        empty = "+" + planetaryFacility.Value1 + "% " + TextResolver.GetText("Colony Development Bonus");
                        result = string.Format(TextResolver.GetText("Wonder Description Colony"), planetaryFacility.Name, empty);
                        break;
                }
            }
            return result;
        }

        public string ResolveWonderDescription(PlanetaryFacilityDefinition planetaryFacility)
        {
            string text = string.Empty;
            if (planetaryFacility != null && planetaryFacility.Type == PlanetaryFacilityType.Wonder)
            {
                ResolvePlanetaryFacilityLines(planetaryFacility, out var descriptions, out var values, includeExtraWonderNotes: false);
                for (int i = 1; i < descriptions.Length; i++)
                {
                    if (!string.IsNullOrEmpty(descriptions[i]))
                    {
                        text += descriptions[i];
                        if (!string.IsNullOrEmpty(values[i]))
                        {
                            text = text + ": " + values[i];
                        }
                        text += "\n";
                    }
                }
            }
            if (!string.IsNullOrEmpty(text) && text.Length >= 1)
            {
                text = text.Substring(0, text.Length - 1);
            }
            return text;
        }

        public string ResolvePirateFacilityDescription(PlanetaryFacilityDefinition planetaryFacility)
        {
            string text = string.Empty;
            if (planetaryFacility != null && (planetaryFacility.Type == PlanetaryFacilityType.PirateBase || planetaryFacility.Type == PlanetaryFacilityType.PirateFortress || planetaryFacility.Type == PlanetaryFacilityType.PirateCriminalNetwork))
            {
                switch (planetaryFacility.Type)
                {
                    case PlanetaryFacilityType.PirateBase:
                        text = planetaryFacility.Description + "\n\n";
                        text += "\n\n";
                        break;
                    case PlanetaryFacilityType.PirateFortress:
                        text = planetaryFacility.Description + "\n\n";
                        text += "\n\n";
                        break;
                    case PlanetaryFacilityType.PirateCriminalNetwork:
                        text = planetaryFacility.Description + "\n\n";
                        text += "\n\n";
                        break;
                }
                ResolvePlanetaryFacilityLines(planetaryFacility, out var descriptions, out var values, includeExtraWonderNotes: false);
                for (int i = 1; i < descriptions.Length; i++)
                {
                    if (!string.IsNullOrEmpty(descriptions[i]))
                    {
                        text += descriptions[i];
                        if (!string.IsNullOrEmpty(values[i]))
                        {
                            text = text + ": " + values[i];
                        }
                        text += "\n";
                    }
                }
            }
            if (!string.IsNullOrEmpty(text) && text.Length >= 1)
            {
                text = text.Substring(0, text.Length - 1);
            }
            return text;
        }

        public void ResolvePlanetaryFacilityLines(PlanetaryFacilityDefinition planetaryFacility, out string[] descriptions, out string[] values)
        {
            ResolvePlanetaryFacilityLines(planetaryFacility, out descriptions, out values, includeExtraWonderNotes: true);
        }

        public void ResolvePlanetaryFacilityLines(PlanetaryFacilityDefinition planetaryFacility, out string[] descriptions, out string[] values, bool includeExtraWonderNotes)
        {
            descriptions = new string[6];
            values = new string[6];
            descriptions[0] = planetaryFacility.Name;
            switch (planetaryFacility.Type)
            {
                case PlanetaryFacilityType.Wonder:
                    {
                        string text = "\n" + TextResolver.GetText("Note that each wonder may only be built once in the galaxy");
                        string empty = string.Empty;
                        if (planetaryFacility.Value3 > 0)
                        {
                            HabitatType type = ResolveColonyHabitatTypeByIndexDesertBeforeOcean(planetaryFacility.Value3 - 1);
                            empty = ". " + string.Format(TextResolver.GetText("Wonder Build Location Requirement"), ResolveDescription(type).ToUpper(CultureInfo.InvariantCulture));
                            text += empty;
                        }
                        if (!includeExtraWonderNotes)
                        {
                            text = string.Empty;
                            empty = string.Empty;
                        }
                        switch (planetaryFacility.WonderType)
                        {
                            case WonderType.ColonyConstructionSpeed:
                                descriptions[1] = planetaryFacility.Description + text;
                                descriptions[2] = TextResolver.GetText("Colony Development Bonus");
                                values[2] = "+" + planetaryFacility.Value1 + "%";
                                descriptions[3] = TextResolver.GetText("Construction Speed");
                                values[3] = string.Format(TextResolver.GetText("X% faster"), "+" + planetaryFacility.Value2);
                                descriptions[4] = TextResolver.GetText("Facility Maintenance Cost");
                                values[4] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                                break;
                            case WonderType.ColonyDefense:
                                descriptions[1] = planetaryFacility.Description + text;
                                descriptions[2] = TextResolver.GetText("Colony Development Bonus");
                                values[2] = "+" + planetaryFacility.Value1 + "%";
                                descriptions[3] = TextResolver.GetText("Colony Defense");
                                values[3] = "+" + planetaryFacility.Value2 * 10 + "%";
                                descriptions[4] = TextResolver.GetText("Facility Maintenance Cost");
                                values[4] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                                break;
                            case WonderType.ColonyHappiness:
                                descriptions[1] = planetaryFacility.Description + text;
                                descriptions[2] = TextResolver.GetText("Colony Development Bonus");
                                values[2] = "+" + planetaryFacility.Value1 + "%";
                                descriptions[3] = TextResolver.GetText("Colony Happiness");
                                values[3] = "+" + planetaryFacility.Value2 + "%";
                                descriptions[4] = TextResolver.GetText("Facility Maintenance Cost");
                                values[4] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                                break;
                            case WonderType.ColonyIncome:
                                descriptions[1] = planetaryFacility.Description + text;
                                descriptions[2] = TextResolver.GetText("Colony Development Bonus");
                                values[2] = "+" + planetaryFacility.Value1 + "%";
                                descriptions[3] = TextResolver.GetText("Colony Income");
                                values[3] = "+" + planetaryFacility.Value2 + "%";
                                descriptions[4] = TextResolver.GetText("Facility Maintenance Cost");
                                values[4] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                                break;
                            case WonderType.ColonyPopulationGrowth:
                                descriptions[1] = planetaryFacility.Description + text;
                                descriptions[2] = TextResolver.GetText("Colony Development Bonus");
                                values[2] = "+" + planetaryFacility.Value1 + "%";
                                descriptions[3] = TextResolver.GetText("Colony Population");
                                values[3] = string.Format(TextResolver.GetText("X% faster growth"), "+" + planetaryFacility.Value2);
                                descriptions[4] = TextResolver.GetText("Facility Maintenance Cost");
                                values[4] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                                break;
                            case WonderType.EmpireHappiness:
                                descriptions[1] = planetaryFacility.Description + text;
                                descriptions[2] = TextResolver.GetText("Colony Development Bonus");
                                values[2] = "+" + planetaryFacility.Value1 + "%";
                                descriptions[3] = TextResolver.GetText("Empire Happiness");
                                values[3] = "+" + planetaryFacility.Value2 + "%";
                                descriptions[4] = TextResolver.GetText("Facility Maintenance Cost");
                                values[4] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                                break;
                            case WonderType.EmpireIncome:
                                descriptions[1] = planetaryFacility.Description + text;
                                descriptions[2] = TextResolver.GetText("Colony Development Bonus");
                                values[2] = "+" + planetaryFacility.Value1 + "%";
                                descriptions[3] = TextResolver.GetText("Empire Income");
                                values[3] = "+" + planetaryFacility.Value2 + "%";
                                descriptions[4] = TextResolver.GetText("Facility Maintenance Cost");
                                values[4] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                                break;
                            case WonderType.EmpirePopulationGrowth:
                                descriptions[1] = planetaryFacility.Description + text;
                                descriptions[2] = TextResolver.GetText("Colony Development Bonus");
                                values[2] = "+" + planetaryFacility.Value1 + "%";
                                descriptions[3] = TextResolver.GetText("Empire Population");
                                values[3] = string.Format(TextResolver.GetText("X% faster growth"), "+" + planetaryFacility.Value2);
                                descriptions[4] = TextResolver.GetText("Facility Maintenance Cost");
                                values[4] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                                break;
                            case WonderType.EmpireResearchEnergy:
                                descriptions[1] = planetaryFacility.Description + text;
                                descriptions[2] = TextResolver.GetText("Colony Development Bonus");
                                values[2] = "+" + planetaryFacility.Value1 + "%";
                                descriptions[3] = TextResolver.GetText("Energy Research");
                                values[3] = "+" + planetaryFacility.Value2 + "%";
                                descriptions[4] = TextResolver.GetText("Facility Maintenance Cost");
                                values[4] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                                break;
                            case WonderType.EmpireResearchHighTech:
                                descriptions[1] = planetaryFacility.Description + text;
                                descriptions[2] = TextResolver.GetText("Colony Development Bonus");
                                values[2] = "+" + planetaryFacility.Value1 + "%";
                                descriptions[3] = TextResolver.GetText("HighTech Research");
                                values[3] = "+" + planetaryFacility.Value2 + "%";
                                descriptions[4] = TextResolver.GetText("Facility Maintenance Cost");
                                values[4] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                                break;
                            case WonderType.EmpireResearchWeapons:
                                descriptions[1] = TextResolver.GetText("Wonder Description Empire Research Weapons") + text;
                                descriptions[1] = planetaryFacility.Description + text;
                                descriptions[2] = TextResolver.GetText("Colony Development Bonus");
                                values[2] = "+" + planetaryFacility.Value1 + "%";
                                descriptions[3] = TextResolver.GetText("Weapons Research");
                                values[3] = "+" + planetaryFacility.Value2 + "%";
                                descriptions[4] = TextResolver.GetText("Facility Maintenance Cost");
                                values[4] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                                break;
                            case WonderType.RaceAchievement:
                                descriptions[1] = planetaryFacility.Description + text;
                                descriptions[2] = TextResolver.GetText("Colony Development Bonus");
                                values[2] = "+" + planetaryFacility.Value1 + "%";
                                descriptions[3] = TextResolver.GetText("Facility Maintenance Cost");
                                values[3] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                                break;
                        }
                        break;
                    }
                case PlanetaryFacilityType.TerraformingFacility:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Facility Maintenance Cost");
                    values[2] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                    break;
                case PlanetaryFacilityType.TroopTrainingCenter:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Facility Maintenance Cost");
                    values[2] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                    break;
                case PlanetaryFacilityType.RoboticTroopFoundry:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Facility Maintenance Cost");
                    values[2] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                    break;
                case PlanetaryFacilityType.CloningFacility:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Facility Maintenance Cost");
                    values[2] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                    break;
                case PlanetaryFacilityType.FortifiedBunker:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Defensive bonus");
                    values[2] = planetaryFacility.Value1 * 10 + "%";
                    break;
                case PlanetaryFacilityType.ArmoredFactory:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Facility Maintenance Cost");
                    values[2] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                    break;
                case PlanetaryFacilityType.SpyAcademy:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Facility Maintenance Cost");
                    values[2] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                    break;
                case PlanetaryFacilityType.ScienceAcademy:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Facility Maintenance Cost");
                    values[2] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                    break;
                case PlanetaryFacilityType.NavalAcademy:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Facility Maintenance Cost");
                    values[2] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                    break;
                case PlanetaryFacilityType.MilitaryAcademy:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Facility Maintenance Cost");
                    values[2] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                    break;
                case PlanetaryFacilityType.IonCannon:
                    {
                        descriptions[1] = planetaryFacility.Description;
                        descriptions[2] = TextResolver.GetText("Facility Maintenance Cost");
                        values[2] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                        ComponentDefinition componentDefinition = ComponentDefinitionsStatic[planetaryFacility.Value1];
                        descriptions[3] = TextResolver.GetText("Power");
                        values[3] = componentDefinition.Value1.ToString();
                        descriptions[4] = TextResolver.GetText("Range");
                        values[4] = componentDefinition.Value2.ToString();
                        descriptions[5] = TextResolver.GetText("Rate of fire");
                        values[5] = ((double)componentDefinition.Value6 / 1000.0).ToString("0.0") + " " + TextResolver.GetText("seconds abbreviation");
                        break;
                    }
                case PlanetaryFacilityType.PlanetaryShield:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Facility Maintenance Cost");
                    values[2] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                    break;
                case PlanetaryFacilityType.RegionalCapital:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Facility Maintenance Cost");
                    values[2] = planetaryFacility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                    break;
                case PlanetaryFacilityType.PirateBase:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Empire Research Bonus");
                    values[2] = "+" + planetaryFacility.Value1.ToString("0") + "%";
                    descriptions[3] = TextResolver.GetText("Colony Income Bonus");
                    values[3] = "+" + planetaryFacility.Value2.ToString("0") + "%";
                    descriptions[4] = TextResolver.GetText("Colony Corruption");
                    values[4] = "+" + planetaryFacility.Value3.ToString("0") + "%";
                    break;
                case PlanetaryFacilityType.PirateFortress:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Empire Research Bonus");
                    values[2] = "+" + planetaryFacility.Value1.ToString("0") + "%";
                    descriptions[3] = TextResolver.GetText("Colony Income Bonus");
                    values[3] = "+" + planetaryFacility.Value2.ToString("0") + "%";
                    descriptions[4] = TextResolver.GetText("Colony Corruption");
                    values[4] = "+" + planetaryFacility.Value3.ToString("0") + "%";
                    break;
                case PlanetaryFacilityType.PirateCriminalNetwork:
                    descriptions[1] = planetaryFacility.Description;
                    descriptions[2] = TextResolver.GetText("Empire Research Bonus");
                    values[2] = "+" + planetaryFacility.Value1.ToString("0") + "%";
                    descriptions[3] = TextResolver.GetText("Colony Corruption");
                    values[3] = "+" + planetaryFacility.Value3.ToString("0") + "%";
                    break;
            }
        }

        public void ResolveResearchAbilityLines(ResearchAbility ability, out string[] descriptions, out string[] values)
        {
            descriptions = new string[4];
            values = new string[4];
            switch (ability.Type)
            {
                case ResearchAbilityType.Troop:
                    {
                        TroopType troopType = TroopType.Undefined;
                        if (ability.RelatedObject != null && ability.RelatedObject is TroopType)
                        {
                            troopType = (TroopType)ability.RelatedObject;
                        }
                        if (troopType != 0)
                        {
                            descriptions[0] = ability.Name;
                            values[0] = string.Empty;
                            if (ability.Value > 0)
                            {
                                descriptions[1] = string.Format(TextResolver.GetText("Increases the Attack Strength of newly recruited TROOPTYPE"), ResolveDescription(troopType));
                                descriptions[2] = TextResolver.GetText("Bonus");
                                values[2] = Math.Abs(ability.Value).ToString("+0") + "%";
                                break;
                            }
                            if (ability.Value < 0)
                            {
                                if (troopType == TroopType.Artillery)
                                {
                                    descriptions[1] = string.Format(TextResolver.GetText("Increases the interception accuracy of all TROOPTYPE against invaders"), ResolveDescription(troopType));
                                    descriptions[2] = TextResolver.GetText("Bonus");
                                    values[2] = Math.Abs(ability.Value).ToString("+0") + "%";
                                }
                                else
                                {
                                    descriptions[1] = string.Format(TextResolver.GetText("Increases the Defend Strength of newly recruited TROOPTYPE"), ResolveDescription(troopType));
                                    descriptions[2] = TextResolver.GetText("Bonus");
                                    values[2] = Math.Abs(ability.Value).ToString("+0") + "%";
                                }
                                break;
                            }
                            descriptions[1] = string.Format(TextResolver.GetText("Enable recruiting TROOPTYPE"), ResolveDescription(troopType));
                            string text = string.Empty;
                            switch (troopType)
                            {
                                case TroopType.Infantry:
                                    text = TextResolver.GetText("Troop Type Description Infantry");
                                    break;
                                case TroopType.Armored:
                                    text = TextResolver.GetText("Troop Type Description Armored");
                                    break;
                                case TroopType.Artillery:
                                    text = TextResolver.GetText("Troop Type Description Artillery");
                                    break;
                                case TroopType.SpecialForces:
                                    text = TextResolver.GetText("Troop Type Description SpecialForces");
                                    break;
                            }
                            string[] array;
                            (array = descriptions)[1] = array[1] + "\n" + text;
                        }
                        else
                        {
                            descriptions[0] = ability.Name;
                            values[0] = string.Empty;
                            descriptions[1] = TextResolver.GetText("Lowers maintenance costs of all troops");
                            descriptions[2] = TextResolver.GetText("Bonus");
                            values[2] = Math.Abs(ability.Value).ToString("-0") + "%";
                        }
                        break;
                    }
                case ResearchAbilityType.Boarding:
                    descriptions[0] = ability.Name;
                    values[0] = string.Empty;
                    if (ability.Value > 0)
                    {
                        descriptions[1] = TextResolver.GetText("Improved Boarding attack strength");
                        descriptions[2] = TextResolver.GetText("Bonus");
                        values[2] = Math.Abs(ability.Value).ToString("+0") + "%";
                    }
                    else if (ability.Value < 0)
                    {
                        descriptions[1] = TextResolver.GetText("Improved Boarding defense strength");
                        descriptions[2] = TextResolver.GetText("Bonus");
                        values[2] = Math.Abs(ability.Value).ToString("+0") + "%";
                    }
                    break;
                case ResearchAbilityType.EnableShipSubRole:
                    {
                        BuiltObjectSubRole builtObjectSubRole = BuiltObjectSubRole.Undefined;
                        if (ability.RelatedObject != null && ability.RelatedObject is BuiltObjectSubRole)
                        {
                            builtObjectSubRole = (BuiltObjectSubRole)ability.RelatedObject;
                        }
                        if (builtObjectSubRole != 0)
                        {
                            descriptions[0] = ability.Name;
                            values[0] = string.Empty;
                            descriptions[1] = string.Format(TextResolver.GetText("Enable building of SHIPTYPE"), ResolveDescription(builtObjectSubRole)) + "  (" + TextResolver.GetText("When construction size allows").ToLower(CultureInfo.InvariantCulture) + ")";
                            values[1] = string.Empty;
                            if (builtObjectSubRole == BuiltObjectSubRole.Carrier)
                            {
                                descriptions[2] = TextResolver.GetText("Note that Carriers can be built 50% larger than current maximum ship construction size");
                                values[2] = string.Empty;
                            }
                        }
                        break;
                    }
                case ResearchAbilityType.ColonizeHabitatType:
                    descriptions[0] = ability.Name;
                    values[0] = "";
                    break;
                case ResearchAbilityType.ConstructionSize:
                    descriptions[0] = ability.Name;
                    values[0] = string.Empty;
                    descriptions[1] = TextResolver.GetText("Maximum ship size");
                    values[1] = ability.Value.ToString();
                    descriptions[2] = TextResolver.GetText("Maximum base size");
                    values[2] = (ability.Value * 3).ToString();
                    descriptions[3] = "(" + TextResolver.GetText("Bases built at colonies have unlimited size") + ")";
                    break;
                case ResearchAbilityType.PopulationGrowthRate:
                    descriptions[0] = ability.Name;
                    values[0] = "";
                    break;
            }
        }

        public void ResolveComponentDescriptionLines(Empire empire, Component component, ComponentImprovement improvement, ResearchNode project, out string[] descriptions, out string[] values)
        {
            descriptions = new string[10];
            values = new string[10];
            if (improvement != null && improvement.TechLevel != component.TechLevel)
            {
                descriptions[0] = string.Format(TextResolver.GetText("Improvements to COMPONENT"), component.Name);
            }
            else
            {
                descriptions[0] = component.Name;
            }
            descriptions[1] = TextResolver.GetText("Size") + ": " + component.Size + ",   " + TextResolver.GetText("Static Energy Used") + ": " + component.EnergyUsed;
            string[] descriptions2 = new string[8];
            string[] values2 = new string[8];
            ResolveComponentDescriptionDetailed(empire, component, improvement, project, out descriptions2, out values2);
            for (int i = 0; i < descriptions2.Length; i++)
            {
                descriptions[i + 2] = descriptions2[i];
                values[i + 2] = values2[i];
            }
        }

        public void ResolveComponentDescriptionDetailed(Empire empire, Component component, ComponentImprovement improvement, ResearchNode project, out string[] descriptions, out string[] values)
        {
            BaconGalaxy.ResolveComponentDescriptionDetailed(this, empire, component, improvement, project, out descriptions, out values);
        }

        public void ResolveComponentDescriptionComplete(Empire empire, Component component, ComponentImprovement improvement, out string title, out string type, out string sizeCost, out string staticEnergy, out string[] descriptions, out string[] values)
        {
            ResearchNode researchNode = null;
            if (empire != null)
            {
                if (improvement == null)
                {
                    improvement = empire.Research.ResolveImprovedComponentValues(component);
                    researchNode = ((improvement.TechLevel != component.TechLevel) ? empire.Research.FindProjectForComponentImprovement(improvement) : empire.Research.FindProjectForComponent(component));
                }
                else
                {
                    researchNode = empire.Research.FindProjectForComponentImprovement(improvement);
                }
            }
            title = component.Name;
            type = ResolveDescription(component.Type) + " (" + component.Industry.ToString() + ")";
            if (component.Category == ComponentCategoryType.WeaponTorpedo && component.Value7 > 0)
            {
                type = TextResolver.GetText("Bombarding Torpedo Weapon") + " (" + component.Industry.ToString() + ")";
            }
            sizeCost = TextResolver.GetText("Size") + ":" + component.Size;
            if (researchNode != null)
            {
                string text = sizeCost;
                sizeCost = text + ", " + TextResolver.GetText("Cost") + ":" + researchNode.Cost.ToString("0,K");
            }
            staticEnergy = component.EnergyUsed.ToString();
            descriptions = new string[8];
            values = new string[8];
            ResolveComponentDescriptionDetailed(empire, component, improvement, researchNode, out descriptions, out values);
        }

        public void GenerateBenefitDetail(ResearchNode project, Empire empire, out List<string[]> allDescriptions, out List<string[]> allValues)
        {
            allDescriptions = new List<string[]>();
            allValues = new List<string[]>();
            if (project == null)
            {
                return;
            }
            if (project.Components != null && project.Components.Count > 0)
            {
                for (int i = 0; i < project.Components.Count; i++)
                {
                    string[] descriptions = new string[8];
                    string[] values = new string[8];
                    ResolveComponentDescriptionLines(empire, project.Components[i], null, project, out descriptions, out values);
                    allDescriptions.Add(descriptions);
                    allValues.Add(values);
                }
            }
            if (project.ComponentImprovements != null && project.ComponentImprovements.Count > 0)
            {
                for (int j = 0; j < project.ComponentImprovements.Count; j++)
                {
                    string[] descriptions2 = new string[8];
                    string[] values2 = new string[8];
                    ResolveComponentDescriptionLines(empire, project.ComponentImprovements[j].ImprovedComponent, project.ComponentImprovements[j], project, out descriptions2, out values2);
                    allDescriptions.Add(descriptions2);
                    allValues.Add(values2);
                }
            }
            if (project.Fighters != null && project.Fighters.Count > 0)
            {
                for (int k = 0; k < project.Fighters.Count; k++)
                {
                    string[] descriptions3 = new string[7];
                    string[] values3 = new string[7];
                    ResolveFighterDescription(empire, project.Fighters[k], project, out descriptions3, out values3);
                    allDescriptions.Add(descriptions3);
                    allValues.Add(values3);
                }
            }
            if (project.Abilities != null && project.Abilities.Count > 0)
            {
                for (int l = 0; l < project.Abilities.Count; l++)
                {
                    string[] descriptions4 = new string[7];
                    string[] values4 = new string[7];
                    ResolveResearchAbilityLines(project.Abilities[l], out descriptions4, out values4);
                    allDescriptions.Add(descriptions4);
                    allValues.Add(values4);
                }
            }
            if (project.PlanetaryFacility != null)
            {
                string[] descriptions5 = new string[7];
                string[] values5 = new string[7];
                ResolvePlanetaryFacilityLines(project.PlanetaryFacility, out descriptions5, out values5);
                double num = CalculatePlanetaryFacilityCost(project.PlanetaryFacility, empire);
                if (project.PlanetaryFacility.Type == PlanetaryFacilityType.Wonder)
                {
                    descriptions5[0] = TextResolver.GetText("Wonder") + ": " + descriptions5[0] + "  (" + string.Format(TextResolver.GetText("X credits"), num.ToString("#,###,##0")) + ")";
                    if (CheckWonderBuilt(project.PlanetaryFacility))
                    {
                        descriptions5[0] = TextResolver.GetText("Wonder Already Built").ToUpper(CultureInfo.InvariantCulture) + "\n" + descriptions5[0];
                    }
                }
                else
                {
                    descriptions5[0] = TextResolver.GetText("Planetary Facility") + ": " + descriptions5[0] + "  (" + string.Format(TextResolver.GetText("X credits"), num.ToString("#,###,##0")) + ")";
                }
                allDescriptions.Add(descriptions5);
                allValues.Add(values5);
            }
            if (project.PlagueChange == null)
            {
                return;
            }
            Plague plague = Plagues[project.PlagueChange.PlagueId];
            string[] array = new string[8];
            string[] array2 = new string[8];
            array[0] = plague.Name;
            array[1] = project.PlagueChange.Description;
            array[2] = TextResolver.GetText("Mortality Rate");
            array2[2] = project.PlagueChange.MortalityRate.ToString("0.000");
            array[3] = TextResolver.GetText("Infection");
            array2[3] = project.PlagueChange.InfectionChance.ToString("0");
            array[4] = TextResolver.GetText("Duration");
            array2[4] = string.Format(arg0: (project.PlagueChange.Duration / (float)RealSecondsInGalacticYear * 12f).ToString("0.0"), format: TextResolver.GetText("X months"));
            if (!string.IsNullOrEmpty(plague.ExceptionRaceName))
            {
                Race race = Races[plague.ExceptionRaceName];
                if (race != null)
                {
                    array[5] = plague.ExceptionRaceName + " " + TextResolver.GetText("Mortality Rate");
                    array2[5] = project.PlagueChange.ExceptionMortalityRate.ToString("0.000");
                    array[6] = plague.ExceptionRaceName + " " + TextResolver.GetText("Infection");
                    array2[6] = project.PlagueChange.ExceptionInfectionChance.ToString("0");
                    array[7] = plague.ExceptionRaceName + " " + TextResolver.GetText("Duration");
                    array2[7] = string.Format(arg0: (project.PlagueChange.ExceptionDuration / (float)RealSecondsInGalacticYear * 12f).ToString("0.0"), format: TextResolver.GetText("X months"));
                }
            }
            allDescriptions.Add(array);
            allValues.Add(array2);
        }

        private int SelectClusterIndex(double selection)
        {
            double num = 0.0;
            for (int i = 0; i < _StarClusterPortions.Count; i++)
            {
                if (selection >= num && selection < num + _StarClusterPortions[i])
                {
                    return i;
                }
                num += _StarClusterPortions[i];
            }
            return -1;
        }

        private Habitat SetupSun(GalaxyShape galaxyShape)
        {
            int num = 0;
            double num2 = 0.0;
            double num3 = 0.0;
            double val = 300000.0 + 175000000.0 / Math.Sqrt(StarCount);
            double num4 = 4000000.0;
            num4 = ((StarCount >= 1400) ? 5000000.0 : ((StarCount >= 1000) ? 4250000.0 : ((StarCount >= 700) ? 3600000.0 : ((StarCount < 400) ? 2000000.0 : 2700000.0))));
            val = Math.Min(val, num4);
            int num5 = 0;
            double num6 = 0.0;
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            do
            {
                switch (galaxyShape)
                {
                    case GalaxyShape.ClustersEven:
                    case GalaxyShape.ClustersVaried:
                        {
                            if (Rnd.Next(0, 10) == 1)
                            {
                                num2 = (double)SizeX * 0.02 + Rnd.NextDouble() * ((double)SizeX * 0.96);
                                num3 = (double)SizeY * 0.02 + Rnd.NextDouble() * ((double)SizeY * 0.96);
                                break;
                            }
                            int num9 = SelectClusterIndex(Rnd.NextDouble());
                            if (num9 >= 0)
                            {
                                _ = 1.0 / (double)_StarClusterPortions.Count;
                                double num10 = Math.Sqrt(_StarClusterPortions[num9]) * val * 3.0;
                                double num11 = Rnd.NextDouble() * (num10 / 2.0);
                                double num12 = Rnd.NextDouble() * Math.PI * 2.0;
                                double num13 = Math.Sin(num12) * num11;
                                double num14 = Math.Cos(num12) * num11;
                                num2 = (double)_StarClusterLocations[num9].X + num13;
                                num3 = (double)_StarClusterLocations[num9].Y + num14;
                            }
                            else
                            {
                                num2 = (double)SizeX * 0.02 + Rnd.NextDouble() * ((double)SizeX * 0.96);
                                num3 = (double)SizeY * 0.02 + Rnd.NextDouble() * ((double)SizeY * 0.96);
                            }
                            break;
                        }
                    case GalaxyShape.Ring:
                        {
                            int num16 = Rnd.Next(0, 20);
                            if (num16 >= 3)
                            {
                                double num11 = (double)(SizeX / 2) - (double)(SizeX / 2) * Rnd.NextDouble() * 0.15;
                                double num12 = Rnd.NextDouble() * Math.PI * 2.0;
                                double num13 = Math.Sin(num12) * num11;
                                double num14 = Math.Cos(num12) * num11;
                                num2 = (double)(SizeY / 2) + num13;
                                num3 = (double)(SizeX / 2) + num14;
                                break;
                            }
                            double num7 = (double)Rnd.Next(2, 10) / 10.0 * ((double)Rnd.Next(2, 10) / 10.0) * Rnd.NextDouble() * (double)SizeX / 2.0;
                            double num8 = (double)Rnd.Next(2, 10) / 10.0 * ((double)Rnd.Next(2, 10) / 10.0) * Rnd.NextDouble() * (double)SizeY / 2.0;
                            switch (Rnd.Next(1, 3))
                            {
                                case 1:
                                    num2 = (double)(SizeX / 2) + num7;
                                    break;
                                case 2:
                                    num2 = (double)(SizeX / 2) - num7;
                                    break;
                            }
                            switch (Rnd.Next(1, 3))
                            {
                                case 1:
                                    num3 = (double)(SizeY / 2) + num8;
                                    break;
                                case 2:
                                    num3 = (double)(SizeY / 2) - num8;
                                    break;
                            }
                            break;
                        }
                    case GalaxyShape.Elliptical:
                        {
                            int num15 = Rnd.Next(0, 16);
                            if (num15 >= 10)
                            {
                                double num7 = (double)Rnd.Next(2, 10) / 10.0 * ((double)Rnd.Next(2, 10) / 10.0) * Rnd.NextDouble() * (double)SizeX / 2.0;
                                double num8 = (double)Rnd.Next(2, 10) / 10.0 * ((double)Rnd.Next(2, 10) / 10.0) * Rnd.NextDouble() * (double)SizeY / 2.0;
                                switch (Rnd.Next(1, 3))
                                {
                                    case 1:
                                        num2 = (double)(SizeX / 2) + num7;
                                        break;
                                    case 2:
                                        num2 = (double)(SizeX / 2) - num7;
                                        break;
                                }
                                switch (Rnd.Next(1, 3))
                                {
                                    case 1:
                                        num3 = (double)(SizeY / 2) + num8;
                                        break;
                                    case 2:
                                        num3 = (double)(SizeY / 2) - num8;
                                        break;
                                }
                            }
                            else if (num15 >= 5)
                            {
                                double num11 = (double)(SizeX / 2) - (double)(SizeX / 2) * Rnd.NextDouble() * 0.1;
                                double num12 = Rnd.NextDouble() * Math.PI * 2.0;
                                double num13 = Math.Sin(num12) * num11;
                                double num14 = Math.Cos(num12) * num11;
                                num2 = (double)(SizeY / 2) + num13;
                                num3 = (double)(SizeX / 2) + num14;
                            }
                            else
                            {
                                double num11 = (double)(SizeX / 2) * (0.25 + Rnd.NextDouble() * 0.6);
                                double num12 = Rnd.NextDouble() * Math.PI * 2.0;
                                double num13 = Math.Sin(num12) * num11;
                                double num14 = Math.Cos(num12) * num11;
                                num2 = (double)(SizeY / 2) + num13;
                                num3 = (double)(SizeX / 2) + num14;
                            }
                            break;
                        }
                    case GalaxyShape.Spiral:
                        {
                            double num7 = Rnd.NextDouble() * Rnd.NextDouble() * (double)SizeX / 2.0;
                            double num8 = Rnd.NextDouble() * Rnd.NextDouble() * (double)SizeY / 2.0;
                            switch (Rnd.Next(1, 3))
                            {
                                case 1:
                                    num2 = (double)(SizeX / 2) + num7;
                                    break;
                                case 2:
                                    num2 = (double)(SizeX / 2) - num7;
                                    break;
                            }
                            switch (Rnd.Next(1, 3))
                            {
                                case 1:
                                    num3 = (double)(SizeY / 2) + num8;
                                    break;
                                case 2:
                                    num3 = (double)(SizeY / 2) - num8;
                                    break;
                            }
                            break;
                        }
                    case GalaxyShape.Irregular:
                        num2 = Rnd.NextDouble() * (double)SizeX;
                        num3 = Rnd.NextDouble() * (double)SizeY;
                        break;
                }
                flag2 = true;
                double num17 = (double)MaxSolarSystemSize + 500.0;
                if (num2 < num17 || num2 > (double)SizeX - num17 || num3 < num17 || num3 > (double)SizeY - num17)
                {
                    flag2 = false;
                }
                Habitat habitat = FindNearestSystemGasCloudAsteroid(num2, num3);
                num6 = ((habitat == null) ? double.MaxValue : CalculateDistance(num2, num3, habitat.Xpos, habitat.Ypos));
                GalaxyLocationList galaxyLocationList = DetermineGalaxyLocationsAtPoint(num2, num3, GalaxyLocationType.NebulaCloud);
                if (galaxyLocationList.Count > 0)
                {
                    if (Rnd.Next(0, 15) == 1)
                    {
                        flag = true;
                        foreach (GalaxyLocation item in galaxyLocationList)
                        {
                            if (item.Effect == GalaxyLocationEffectType.LightningDamage)
                            {
                                flag3 = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else
                {
                    flag = true;
                }
                num5++;
            }
            while ((num6 < (double)(MaxSolarSystemSize * 4) || !flag || !flag2) && num5 < 100);
            HabitatType type = HabitatType.MainSequence;
            int pictureRef = 0;
            int mapPictureRef = 0;
            int diameter = 0;
            short solarRadiation = 0;
            short microwaveRadiation = 0;
            short xrayRadiation = 0;
            bool flag4 = false;
            while (!flag4)
            {
                SelectStar(out type, out diameter, out pictureRef, out mapPictureRef, out solarRadiation, out microwaveRadiation, out xrayRadiation);
                flag4 = true;
                if (flag3)
                {
                    switch (type)
                    {
                        case HabitatType.MainSequence:
                        case HabitatType.RedGiant:
                        case HabitatType.SuperGiant:
                            flag4 = false;
                            break;
                        default:
                            flag4 = true;
                            break;
                    }
                }
            }
            Habitat habitat2 = new Habitat(this, HabitatCategoryType.Star, type, GenerateCodeName(), num2, num3);
            habitat2.Diameter = (short)diameter;
            habitat2.PictureRef = (short)pictureRef;
            habitat2.MapPictureRef = (byte)mapPictureRef;
            habitat2.LandscapePictureRef = -1;
            habitat2.SolarRadiation = (byte)solarRadiation;
            habitat2.MicrowaveRadiation = (byte)microwaveRadiation;
            habitat2.XrayRadiation = (byte)xrayRadiation;
            SelectHabitatPictures(habitat2);
            if (habitat2.Type == HabitatType.BlackHole)
            {
                string text = (habitat2.Name = GenerateBlackHoleName());
                double num18 = (double)habitat2.Diameter * 1.1;
                double num19 = (double)habitat2.Diameter * 1.1;
                double x = num2 - num18 / 2.0;
                double y = num3 - num19 / 2.0;
                GalaxyLocation galaxyLocation = new GalaxyLocation(text + " Pull", GalaxyLocationType.BlackHole, x, y, num18, num19, -1);
                galaxyLocation.ShowName = false;
                galaxyLocation.Effect = GalaxyLocationEffectType.ShipPull;
                galaxyLocation.EffectAmount = (float)habitat2.Diameter / 600f;
                _GalaxyLocations.Add(galaxyLocation);
                AddGalaxyLocationIndex(galaxyLocation);
                num18 = (double)habitat2.Diameter * 0.04;
                num19 = (double)habitat2.Diameter * 0.04;
                x = num2 - num18 / 2.0;
                y = num3 - num19 / 2.0;
                galaxyLocation = new GalaxyLocation(text + " Event Horizon", GalaxyLocationType.BlackHole, x, y, num18, num19, -1);
                galaxyLocation.ShowName = false;
                galaxyLocation.Effect = GalaxyLocationEffectType.ShipDamage;
                galaxyLocation.EffectAmount = (float)habitat2.Diameter;
                _GalaxyLocations.Add(galaxyLocation);
                AddGalaxyLocationIndex(galaxyLocation);
            }
            else if (habitat2.Type == HabitatType.SuperNova)
            {
                string name = (habitat2.Name = TextResolver.GetText("HabitatType SuperNova") + " " + GenerateCodeName());
                habitat2.NovaProgression = (float)(30000.0 + Rnd.NextDouble() * 60000.0);
                habitat2.NovaImageIndexMajor = (short)Rnd.Next(0, GalaxyImages.NovaImageCountMajor);
                habitat2.NovaImageIndexMinor = (short)Rnd.Next(0, GalaxyImages.NovaImageCountMinor);
                int num20 = (int)((double)habitat2.NovaProgression * 2.0);
                habitat2.Diameter = (short)(num20 / 10);
                double num21 = num20;
                double num22 = num20;
                double x2 = num2 - num21 / 2.0;
                double y2 = num3 - num22 / 2.0;
                GalaxyLocation galaxyLocation2 = new GalaxyLocation(name, GalaxyLocationType.SuperNova, x2, y2, num21, num22, -1);
                galaxyLocation2.ShowName = false;
                galaxyLocation2.Shape = GalaxyLocationShape.Circular;
                galaxyLocation2.Effect = GalaxyLocationEffectType.ShieldReduction;
                _GalaxyLocations.Add(galaxyLocation2);
                AddGalaxyLocationIndex(galaxyLocation2);
            }
            return habitat2;
        }

        private HabitatList SetupSolarSystem(GalaxyShape galaxyShape, out HabitatList asteroidField)
        {
            return SetupSolarSystem(galaxyShape, null, out asteroidField);
        }

        public HabitatList SetupSolarSystem(GalaxyShape galaxyShape, Habitat sunHabitat)
        {
            HabitatList asteroidField = null;
            return SetupSolarSystem(galaxyShape, sunHabitat, out asteroidField);
        }

        public HabitatList SetupSolarSystem(GalaxyShape galaxyShape, Habitat sunHabitat, out HabitatList asteroidField)
        {
            asteroidField = null;
            int num = 0;
            int num2 = 0;
            bool flag = true;
            HabitatList habitatList = new HabitatList();
            HabitatList habitatList2 = new HabitatList();
            int minValue = 0;
            if (sunHabitat == null)
            {
                sunHabitat = SetupSun(galaxyShape);
            }
            else
            {
                switch (sunHabitat.Type)
                {
                    case HabitatType.MainSequence:
                    case HabitatType.RedGiant:
                    case HabitatType.SuperGiant:
                        minValue = 1;
                        break;
                    case HabitatType.WhiteDwarf:
                        minValue = 4;
                        break;
                    case HabitatType.Neutron:
                        minValue = 4;
                        break;
                }
            }
            int maxValue = 0;
            switch (sunHabitat.Type)
            {
                case HabitatType.MainSequence:
                case HabitatType.RedGiant:
                case HabitatType.SuperGiant:
                    maxValue = 12;
                    if (StarCount <= 400)
                    {
                        switch (Rnd.Next(minValue, 8))
                        {
                            case 0:
                                num2 = 0;
                                break;
                            case 1:
                            case 2:
                                num2 = Rnd.Next(1, 4);
                                break;
                            case 3:
                            case 4:
                                num2 = Rnd.Next(2, 7);
                                break;
                            case 5:
                            case 6:
                                num2 = Rnd.Next(5, 10);
                                break;
                            case 7:
                                num2 = Rnd.Next(6, 16);
                                break;
                        }
                    }
                    else if (StarCount <= 1000)
                    {
                        switch (Rnd.Next(minValue, 7))
                        {
                            case 0:
                                num2 = 0;
                                break;
                            case 1:
                            case 2:
                                num2 = Rnd.Next(1, 4);
                                break;
                            case 3:
                            case 4:
                                num2 = Rnd.Next(3, 7);
                                break;
                            case 5:
                                num2 = Rnd.Next(4, 9);
                                break;
                            case 6:
                                num2 = Rnd.Next(5, 15);
                                break;
                        }
                    }
                    else
                    {
                        switch (Rnd.Next(minValue, 5))
                        {
                            case 0:
                                num2 = 0;
                                break;
                            case 1:
                                num2 = Rnd.Next(1, 3);
                                break;
                            case 2:
                                num2 = Rnd.Next(2, 5);
                                break;
                            case 3:
                                num2 = Rnd.Next(3, 7);
                                break;
                            case 4:
                                num2 = Rnd.Next(4, 11);
                                break;
                        }
                    }
                    break;
                case HabitatType.WhiteDwarf:
                    maxValue = 3;
                    switch (Rnd.Next(minValue, 6))
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            num2 = 0;
                            break;
                        case 4:
                        case 5:
                            num2 = Rnd.Next(1, 3);
                            break;
                    }
                    break;
                case HabitatType.Neutron:
                    maxValue = 24;
                    switch (Rnd.Next(minValue, 6))
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            num2 = 0;
                            break;
                        case 4:
                        case 5:
                            num2 = 1;
                            break;
                    }
                    break;
                case HabitatType.SuperNova:
                    maxValue = 2;
                    flag = false;
                    num2 = 0;
                    break;
            }
            if (num2 > 0)
            {
                int num3 = 0;
                while (true)
                {
                    if (num3 < 20)
                    {
                        if (AssignSystemName(sunHabitat, num2))
                        {
                            break;
                        }
                        num3++;
                        continue;
                    }
                    sunHabitat.Name = GenerateCodeName();
                    break;
                }
            }
            SetScenicFactor(sunHabitat);
            SetResearchBonus(sunHabitat);
            habitatList2.Add(sunHabitat);
            Habitat habitat;
            if (num2 > 0)
            {
                for (int i = 0; i < num2; i++)
                {
                    habitat = sunHabitat;
                    SelectPlanetType(habitat, out var type, out var pictureRef, out var diameter, out var minOrbitDistance, out var maxOrbitDistance, out var landscapePictureRef);
                    int num4 = diameter / 4;
                    int num5 = 0;
                    int num6 = Rnd.Next(minOrbitDistance, maxOrbitDistance);
                    int newMin = num6 - (diameter / 2 + num4);
                    int newMax = num6 + (diameter / 2 + num4);
                    bool flag2 = true;
                    while (flag2 && num5 < 50)
                    {
                        flag2 = false;
                        foreach (Habitat item3 in habitatList)
                        {
                            int existingMin = item3.OrbitDistance - (item3.Diameter / 2 + num4);
                            int existingMax = item3.OrbitDistance + (item3.Diameter / 2 + num4);
                            if (CheckOrbitOverlap(existingMin, existingMax, newMin, newMax))
                            {
                                num6 = Rnd.Next(minOrbitDistance, maxOrbitDistance);
                                newMin = num6 - (diameter / 2 + num4);
                                newMax = num6 + (diameter / 2 + num4);
                                flag2 = true;
                                break;
                            }
                        }
                        num5++;
                    }
                    Habitat habitat2 = new Habitat(this, HabitatCategoryType.Planet, type, "Planet", habitat, Rnd.NextDouble() * Math.PI * 2.0, orbitdirection: true, num6, Rnd.Next(2, 5));
                    habitat2.Diameter = (short)diameter;
                    habitat2.PictureRef = (short)pictureRef;
                    habitat2.LandscapePictureRef = (short)landscapePictureRef;
                    habitat2.BaseQuality = SelectHabitatQuality(habitat2, (float)_ColonyPrevalence);
                    habitat2.DoTasks(CurrentDateTime);
                    habitat2 = SelectResources(habitat2);
                    SetScenicFactor(habitat2);
                    SetResearchBonus(habitat2);
                    SelectHabitatPictures(habitat2);
                    if (Rnd.Next(0, 5) == 2)
                    {
                        habitat2.OrbitDirection = false;
                    }
                    if (habitat2.Type == HabitatType.GasGiant && habitat2.Diameter < 760)
                    {
                        habitat2.HasRings = true;
                    }
                    int num7 = 1;
                    if (Rnd.Next(0, 4) == 1)
                    {
                        num7++;
                    }
                    for (int j = 0; j < num7; j++)
                    {
                        SelectPopulation(habitat2, sunHabitat);
                    }
                    if (habitat2.Population.Count > 0)
                    {
                        habitat2.Cargo = new CargoList();
                        habitat2.Troops = new TroopList();
                        habitat2.TroopsToRecruit = new TroopList();
                        habitat2.InvadingTroops = new TroopList();
                        habitat2.Characters = new CharacterList();
                        habitat2.InvadingCharacters = new CharacterList();
                        habitat2.ConstructionQueue = new ConstructionQueue(habitat2, this);
                        habitat2.ManufacturingQueue = new ManufacturingQueue(habitat2, this);
                        habitat2.DockingBays = new DockingBayList();
                        int num8 = 1;
                        switch (habitat2.Type)
                        {
                            case HabitatType.Volcanic:
                            case HabitatType.Desert:
                            case HabitatType.MarshySwamp:
                            case HabitatType.Continental:
                            case HabitatType.Ocean:
                            case HabitatType.BarrenRock:
                            case HabitatType.Ice:
                            case HabitatType.Metal:
                                num8 = ((habitat2.Category == HabitatCategoryType.Asteroid || habitat2.Type == HabitatType.BarrenRock) ? 1 : 20);
                                break;
                            default:
                                num8 = 1;
                                break;
                        }
                        for (int k = 0; k < num8; k++)
                        {
                            BuiltObjectComponent builtObjectComponent = new BuiltObjectComponent(74, ComponentStatus.Normal);
                            int capacity = 100;
                            DockingBay item = new DockingBay(builtObjectComponent.ComponentID, builtObjectComponent.BuiltObjectComponentId, capacity);
                            habitat2.DockingBays.Add(item);
                        }
                        habitat2.DockingBayWaitQueue = new BuiltObjectList();
                    }
                    else
                    {
                        SelectCreatures(habitat2);
                    }
                    habitatList.Add(habitat2);
                    habitat = habitat2;
                    if (StarCount <= 400)
                    {
                        if (habitat2.Diameter <= 370)
                        {
                            num = ((habitat2.Diameter > 260) ? Rnd.Next(0, 3) : ((habitat2.Diameter > 150) ? Rnd.Next(0, 2) : 0));
                        }
                        else
                        {
                            int maxValue2 = Math.Min(5, habitat2.Diameter / 165);
                            num = Rnd.Next(0, maxValue2);
                        }
                    }
                    else if (StarCount <= 1000)
                    {
                        if (habitat2.Diameter <= 370)
                        {
                            num = ((habitat2.Diameter > 260) ? Rnd.Next(0, 3) : ((habitat2.Diameter > 165) ? Rnd.Next(0, 2) : 0));
                        }
                        else
                        {
                            int maxValue3 = Math.Min(5, habitat2.Diameter / 180);
                            num = Rnd.Next(0, maxValue3);
                        }
                    }
                    else if (habitat2.Diameter <= 370)
                    {
                        num = ((habitat2.Diameter > 260) ? Rnd.Next(0, 3) : ((habitat2.Diameter > 180) ? Rnd.Next(0, 2) : 0));
                    }
                    else
                    {
                        int maxValue4 = Math.Min(5, habitat2.Diameter / 180);
                        num = Rnd.Next(0, maxValue4);
                    }
                    HabitatList habitatList3 = new HabitatList();
                    for (int l = 0; l < num; l++)
                    {
                        SelectMoonType(habitat, out diameter, out type, out var _, out var _, out pictureRef, out landscapePictureRef);
                        habitat2 = new Habitat(this, HabitatCategoryType.Moon, type, GenerateCodeName(), habitat, Rnd.NextDouble() * Math.PI * 2.0, orbitdirection: true, Rnd.Next(5, 32), Rnd.Next(4, 9));
                        if (diameter < 15)
                        {
                            diameter = 15;
                        }
                        habitat2.Diameter = (short)diameter;
                        habitat2.PictureRef = (short)pictureRef;
                        habitat2.LandscapePictureRef = (short)landscapePictureRef;
                        habitat2.BaseQuality = SelectHabitatQuality(habitat2, (float)_ColonyPrevalence);
                        int val = (int)((double)habitat.Diameter * 0.75);
                        val = Math.Max(150, val);
                        int num9 = (int)((double)habitat.Diameter * 3.3);
                        if (num9 > MaxMoonOrbitSize)
                        {
                            num9 = MaxMoonOrbitSize;
                        }
                        int num10 = 5;
                        num5 = 0;
                        num6 = Rnd.Next(val, num9);
                        newMin = num6 - (diameter / 2 + num10);
                        newMax = num6 + (diameter / 2 + num10);
                        flag2 = true;
                        while (flag2 && num5 < 50)
                        {
                            flag2 = false;
                            foreach (Habitat item4 in habitatList3)
                            {
                                int existingMin2 = item4.OrbitDistance - (item4.Diameter / 2 + num10);
                                int existingMax2 = item4.OrbitDistance + (item4.Diameter / 2 + num10);
                                if (CheckOrbitOverlap(existingMin2, existingMax2, newMin, newMax))
                                {
                                    num6 = Rnd.Next(val, num9);
                                    newMin = num6 - (diameter / 2 + num10);
                                    newMax = num6 + (diameter / 2 + num10);
                                    flag2 = true;
                                    break;
                                }
                            }
                            num5++;
                        }
                        habitatList3.Add(habitat2);
                        habitat2.OrbitDistance = (short)num6;
                        habitat2.DoTasks(CurrentDateTime);
                        habitat2 = SelectResources(habitat2);
                        SetScenicFactor(habitat2);
                        SetResearchBonus(habitat2);
                        SelectHabitatPictures(habitat2);
                        if (Rnd.Next(0, 5) == 2)
                        {
                            habitat2.OrbitDirection = false;
                        }
                        num7 = 1;
                        if (Rnd.Next(0, 4) == 1 && habitat2.Type != HabitatType.BarrenRock)
                        {
                            num7++;
                        }
                        for (int m = 0; m < num7; m++)
                        {
                            SelectPopulation(habitat2, sunHabitat);
                        }
                        if (habitat2.Population.Count > 0)
                        {
                            habitat2.Cargo = new CargoList();
                            habitat2.Troops = new TroopList();
                            habitat2.TroopsToRecruit = new TroopList();
                            habitat2.InvadingTroops = new TroopList();
                            habitat2.ConstructionQueue = new ConstructionQueue(habitat2, this);
                            habitat2.ManufacturingQueue = new ManufacturingQueue(habitat2, this);
                            habitat2.DockingBays = new DockingBayList();
                            int num11 = 1;
                            switch (habitat2.Type)
                            {
                                case HabitatType.Volcanic:
                                case HabitatType.Desert:
                                case HabitatType.MarshySwamp:
                                case HabitatType.Continental:
                                case HabitatType.Ocean:
                                case HabitatType.BarrenRock:
                                case HabitatType.Ice:
                                case HabitatType.Metal:
                                    num11 = ((habitat2.Category == HabitatCategoryType.Asteroid || habitat2.Type == HabitatType.BarrenRock) ? 1 : 20);
                                    break;
                                default:
                                    num11 = 1;
                                    break;
                            }
                            for (int n = 0; n < num11; n++)
                            {
                                BuiltObjectComponent builtObjectComponent2 = new BuiltObjectComponent(74, ComponentStatus.Normal);
                                DockingBay item2 = new DockingBay(builtObjectComponent2.ComponentID, builtObjectComponent2.BuiltObjectComponentId, 100);
                                habitat2.DockingBays.Add(item2);
                            }
                            habitat2.DockingBayWaitQueue = new BuiltObjectList();
                        }
                        else
                        {
                            SelectCreatures(habitat2);
                        }
                        habitatList.Add(habitat2);
                    }
                }
                habitat = sunHabitat;
                int num12 = Rnd.Next(0, (int)((double)num2 * 4.5));
                for (int num13 = 0; num13 < num12; num13++)
                {
                    string name = GenerateCodeName();
                    int diameter = Rnd.Next(20, 35);
                    int pictureRef = GalaxyImages.HabitatImageOffsetAsteroidsNormal + Rnd.Next(0, GalaxyImages.HabitatImageCountAsteroidsNormal);
                    double orbitangle = Rnd.NextDouble() * Math.PI * 2.0;
                    Habitat habitat2 = new Habitat(this, HabitatCategoryType.Asteroid, HabitatType.BarrenRock, name, habitat, orbitangle, orbitdirection: true, Rnd.Next(10500, 11500), Rnd.Next(2, 8));
                    habitat2.Diameter = (short)diameter;
                    habitat2.PictureRef = (short)pictureRef;
                    habitat2.LandscapePictureRef = -1;
                    habitat2.BaseQuality = SelectHabitatQuality(habitat2, (float)_ColonyPrevalence);
                    habitat2 = SelectResources(habitat2);
                    SelectHabitatPictures(habitat2);
                    if (Rnd.Next(0, 5) == 2)
                    {
                        habitat2.OrbitDirection = false;
                    }
                    SelectCreatures(habitat2);
                    habitatList.Add(habitat2);
                }
            }
            habitat = sunHabitat;
            if (Rnd.Next(0, maxValue) == 1)
            {
                int num14 = 1;
                if (sunHabitat.Type == HabitatType.SuperNova && Rnd.Next(0, 2) == 1)
                {
                    num14 = 2;
                }
                for (int num15 = 0; num15 < num14; num15++)
                {
                    asteroidField = new HabitatList();
                    int num16 = Rnd.Next(80, 350);
                    double num17 = Rnd.NextDouble() * Math.PI * 2.0;
                    int num18 = Rnd.Next(9500, 10500);
                    HabitatType habitatType = HabitatType.BarrenRock;
                    switch (Rnd.Next(0, 4))
                    {
                        case 1:
                            habitatType = HabitatType.Metal;
                            break;
                        case 2:
                            habitatType = HabitatType.Ice;
                            break;
                    }
                    if (sunHabitat.Type == HabitatType.SuperNova)
                    {
                        habitatType = HabitatType.Metal;
                    }
                    if (habitatType == HabitatType.Ice)
                    {
                        num18 = Rnd.Next(17200, 22200);
                    }
                    int num19 = Rnd.Next(1, 4);
                    bool flag3 = true;
                    if (Rnd.Next(0, 4) == 2)
                    {
                        flag3 = false;
                    }
                    double num20 = Math.Max(0.06, 0.13 * ((double)num16 / 350.0));
                    double num21 = Math.Max(250.0, 500.0 * ((double)num16 / 350.0));
                    double num22 = 0.4;
                    double num23 = num22 * -1.0;
                    double num24 = (double)num18 + num23 * num21;
                    double num25 = (double)num18 + num22 * num21;
                    double num26 = num17 + num23 * num20;
                    double num27 = num17 + num22 * num20;
                    if (num26 > num27)
                    {
                        double num28 = num27;
                        num27 = num26;
                        num26 = num28;
                    }
                    for (int num29 = 0; num29 < num16; num29++)
                    {
                        string name = GenerateCodeName();
                        name = name + ", " + TextResolver.GetText("Asteroid Field");
                        int diameter = Rnd.Next(10, 25);
                        if (Rnd.Next(0, 30) == 5)
                        {
                            diameter = Rnd.Next(26, 45);
                        }
                        int pictureRef = GalaxyImages.HabitatImageOffsetAsteroidsNormal + Rnd.Next(0, GalaxyImages.HabitatImageCountAsteroidsNormal);
                        int num30 = num18 + (int)((Rnd.NextDouble() - 0.5) * Rnd.NextDouble() * 2.0 * num21);
                        double num31 = num17 + (Rnd.NextDouble() - 0.5) * Rnd.NextDouble() * 2.0 * num20;
                        if ((double)num30 > num24 && (double)num30 < num25 && num31 > num26 && num31 < num27)
                        {
                            double num32 = Rnd.NextDouble() * 0.8 - 0.4;
                            double num33 = Rnd.NextDouble() * 0.8 - 0.4;
                            num30 = num18 + (int)(num32 * num21);
                            num31 = num17 + num33 * num20;
                        }
                        Habitat habitat3 = new Habitat(this, HabitatCategoryType.Asteroid, HabitatType.BarrenRock, name, habitat, num31, flag3, num30, num19);
                        habitat3.Diameter = (short)diameter;
                        habitat3.PictureRef = (short)pictureRef;
                        habitat3.LandscapePictureRef = -1;
                        habitat3.BaseQuality = SelectHabitatQuality(habitat3, (float)_ColonyPrevalence);
                        int minimumResourceCount = 0;
                        if (habitatType == HabitatType.Metal && Rnd.Next(0, 3) > 0)
                        {
                            minimumResourceCount = 1;
                        }
                        habitat3.Type = habitatType;
                        habitat3 = SelectResources(habitat3, minimumResourceCount);
                        SelectHabitatPictures(habitat3);
                        if (flag)
                        {
                            SelectCreatures(habitat3);
                        }
                        if (Rnd.Next(0, 1300) == 1)
                        {
                            habitat3 = GenerateTreasureAsteroid(habitat, num31, num30, flag3, num19, doInitialMove: true);
                        }
                        asteroidField.Add(habitat3);
                        habitatList.Add(habitat3);
                    }
                }
            }
            List<int> list = new List<int>();
            HabitatList habitatList4 = new HabitatList();
            for (int num34 = 0; num34 < habitatList.Count; num34++)
            {
                if (habitatList[num34].Category == HabitatCategoryType.Planet || habitatList[num34].Category == HabitatCategoryType.Asteroid)
                {
                    list.Add(habitatList[num34].OrbitDistance);
                    habitatList4.Add(habitatList[num34]);
                }
            }
            int[] keys = list.ToArray();
            Habitat[] array = habitatList4.ToArray();
            Array.Sort(keys, array);
            int num35 = 1;
            for (int num36 = 0; num36 < array.Length; num36++)
            {
                if (array[num36].Category == HabitatCategoryType.Planet)
                {
                    array[num36].Name = sunHabitat.Name + " " + num35;
                    num35++;
                }
                if (!habitatList2.Contains(array[num36]))
                {
                    habitatList2.Add(array[num36]);
                }
                for (int num37 = 0; num37 < habitatList.Count; num37++)
                {
                    if (habitatList[num37].Parent == array[num36])
                    {
                        habitatList[num37].Name = GenerateMoonName(habitatList[num37]);
                        habitatList2.Add(habitatList[num37]);
                    }
                }
            }
            return habitatList2;
        }

        public void SetResearchBonus(Habitat habitat)
        {
            SetResearchBonus(habitat, definitelySet: false);
        }

        public void SetResearchBonus(Habitat habitat, bool definitelySet)
        {
            switch (habitat.Type)
            {
                case HabitatType.Neutron:
                case HabitatType.BlackHole:
                case HabitatType.SuperNova:
                    if (definitelySet || Rnd.Next(0, 4) > 0)
                    {
                        int num2 = Rnd.Next(5, 16);
                        IndustryType researchBonusIndustry2 = IndustryType.Undefined;
                        switch (Rnd.Next(0, 3))
                        {
                            case 0:
                                researchBonusIndustry2 = IndustryType.Weapon;
                                break;
                            case 1:
                                researchBonusIndustry2 = IndustryType.Energy;
                                break;
                            case 2:
                                researchBonusIndustry2 = IndustryType.HighTech;
                                break;
                        }
                        habitat.ResearchBonus = (byte)num2;
                        habitat.ResearchBonusIndustry = researchBonusIndustry2;
                    }
                    break;
                case HabitatType.Volcanic:
                case HabitatType.GasGiant:
                case HabitatType.FrozenGasGiant:
                    if (definitelySet || Rnd.Next(0, 40) == 1)
                    {
                        int num = Rnd.Next(10, 31);
                        IndustryType researchBonusIndustry = IndustryType.Undefined;
                        switch (Rnd.Next(0, 3))
                        {
                            case 0:
                                researchBonusIndustry = IndustryType.Weapon;
                                break;
                            case 1:
                                researchBonusIndustry = IndustryType.Energy;
                                break;
                            case 2:
                                researchBonusIndustry = IndustryType.HighTech;
                                break;
                        }
                        habitat.ResearchBonus = (byte)num;
                        habitat.ResearchBonusIndustry = researchBonusIndustry;
                    }
                    break;
            }
        }

        public void SetScenicFactor(Habitat habitat)
        {
            SetScenicFactor(habitat, definitelySet: false);
        }

        public void SetScenicFactor(Habitat habitat, bool definitelySet)
        {
            Habitat habitat2 = DetermineHabitatSystemStar(habitat);
            switch (habitat.Type)
            {
                case HabitatType.BarrenRock:
                    if ((habitat.Category == HabitatCategoryType.Planet || habitat.Category == HabitatCategoryType.Moon) && (definitelySet || Rnd.Next(0, 600) == 1))
                    {
                        habitat.ScenicFactor = (float)(0.1 + Rnd.NextDouble() * 0.3);
                        habitat.ScenicFeature = string.Format(TextResolver.GetText("Ancient Monolith of X"), habitat2.Name);
                    }
                    break;
                case HabitatType.MarshySwamp:
                case HabitatType.Continental:
                    if (definitelySet || Rnd.Next(0, 70) == 1)
                    {
                        habitat.ScenicFactor = (float)(0.2 + Rnd.NextDouble() * 0.4);
                        switch (Rnd.Next(0, 2))
                        {
                            case 0:
                                habitat.ScenicFeature = string.Format(TextResolver.GetText("Rings of X"), habitat2.Name);
                                habitat.HasRings = true;
                                break;
                            case 1:
                                habitat.ScenicFeature = string.Format(TextResolver.GetText("X Falls"), habitat2.Name);
                                break;
                        }
                    }
                    break;
                case HabitatType.Ocean:
                    if (definitelySet || Rnd.Next(0, 100) == 1)
                    {
                        habitat.ScenicFactor = (float)(0.1 + Rnd.NextDouble() * 0.3);
                        habitat.ScenicFeature = string.Format(TextResolver.GetText("Undersea Ruins of X"), habitat2.Name);
                    }
                    break;
                case HabitatType.Ice:
                    if (definitelySet || Rnd.Next(0, 200) == 1)
                    {
                        habitat.ScenicFactor = (float)(0.2 + Rnd.NextDouble() * 0.4);
                        habitat.ScenicFeature = string.Format(TextResolver.GetText("Ice Rings of X"), habitat2.Name);
                        habitat.HasRings = true;
                    }
                    break;
                case HabitatType.Volcanic:
                case HabitatType.Desert:
                    if (!definitelySet && Rnd.Next(0, 200) != 1)
                    {
                        break;
                    }
                    habitat.ScenicFactor = (float)(0.2 + Rnd.NextDouble() * 0.4);
                    switch (Rnd.Next(0, 2))
                    {
                        case 0:
                            {
                                string scenicFeature2 = string.Format(TextResolver.GetText("Rings of X"), habitat2.Name);
                                if (habitat.Type == HabitatType.Volcanic)
                                {
                                    scenicFeature2 = string.Format(TextResolver.GetText("Fire Rings of X"), habitat2.Name);
                                }
                                habitat.ScenicFeature = scenicFeature2;
                                habitat.HasRings = true;
                                break;
                            }
                        case 1:
                            {
                                string scenicFeature = TextResolver.GetText("Great Canyon");
                                if (habitat2.Name.Length < 15)
                                {
                                    scenicFeature = string.Format(TextResolver.GetText("X Canyon"), habitat2.Name);
                                }
                                habitat.ScenicFeature = scenicFeature;
                                break;
                            }
                    }
                    break;
                case HabitatType.BlackHole:
                    habitat.ScenicFactor = (float)(0.3 + Rnd.NextDouble() * 0.6);
                    break;
                case HabitatType.GasGiant:
                    if (definitelySet || Rnd.Next(0, 100) == 1)
                    {
                        habitat.ScenicFactor = (float)(0.2 + Rnd.NextDouble() * 0.2);
                    }
                    break;
                case HabitatType.Neutron:
                    if (definitelySet || Rnd.Next(0, 2) > 0)
                    {
                        habitat.ScenicFactor = (float)(0.3 + Rnd.NextDouble() * 0.3);
                    }
                    break;
                case HabitatType.SuperNova:
                    break;
            }
        }

        private bool CheckOrbitOverlap(int existingMin, int existingMax, int newMin, int newMax)
        {
            if (newMin >= existingMin && newMin <= existingMax)
            {
                return true;
            }
            if (newMax >= existingMin && newMax <= existingMax)
            {
                return true;
            }
            if (newMin < existingMin && newMax > existingMax)
            {
                return true;
            }
            return false;
        }

        public void AddCargoBaysToDesign(Design design, int cargoBayAmount)
        {
            if (design == null)
            {
                return;
            }
            ComponentDefinition lowestTechByType = ComponentDefinition.GetLowestTechByType(ComponentType.StorageCargo, ComponentDefinitionsStatic);
            if (lowestTechByType != null)
            {
                for (int i = 0; i < cargoBayAmount; i++)
                {
                    design.Components.Add(new Component(lowestTechByType.ComponentID));
                }
            }
            design.ReDefine();
        }

        public BuiltObject GenerateAbandonedBuiltObject(Habitat habitat, Design design)
        {
            return GenerateAbandonedBuiltObject(habitat, design, allowCreatures: true);
        }

        public BuiltObject GenerateAbandonedBuiltObject(Habitat habitat, Design design, bool allowCreatures)
        {
            return GenerateAbandonedBuiltObject(habitat, design, allowCreatures, allowNegativeEffects: true, BuiltObjectEncounterAction.Prompt);
        }

        public BuiltObject GenerateAbandonedBuiltObject(Habitat habitat, Design design, bool allowCreatures, bool allowNegativeEffects, BuiltObjectEncounterAction encounterAction)
        {
            string name = SelectUniqueBuiltObjectName(design, habitat);
            BuiltObject builtObject = GenerateUnownedBuiltObjectFromDesign(design, name, habitat);
            builtObject.IsAutoControlled = false;
            builtObject.PlayerEmpireEncounterAction = encounterAction;
            BuiltObjectEncounterEventType builtObjectEncounterEventType = BuiltObjectEncounterEventType.Acquire;
            string encounterDescription = string.Empty;
            bool flag = false;
            if (design.SubRole == BuiltObjectSubRole.ColonyShip || design.SubRole == BuiltObjectSubRole.ConstructionShip || design.SubRole == BuiltObjectSubRole.ExplorationShip || design.SubRole == BuiltObjectSubRole.GasMiningShip || design.SubRole == BuiltObjectSubRole.MiningShip || design.SubRole == BuiltObjectSubRole.CapitalShip || design.SubRole == BuiltObjectSubRole.ResupplyShip || design.Role == BuiltObjectRole.Base)
            {
                flag = true;
            }
            if (allowNegativeEffects && Rnd.Next(0, 3) == 1)
            {
                switch (Rnd.Next(0, 2))
                {
                    case 0:
                        builtObjectEncounterEventType = BuiltObjectEncounterEventType.Explodes;
                        encounterDescription = "";
                        break;
                    case 1:
                        if (!flag && _PiratePrevalence > 0.0)
                        {
                            builtObjectEncounterEventType = BuiltObjectEncounterEventType.PirateAmbush;
                            encounterDescription = "";
                        }
                        break;
                }
            }
            if (builtObject.Role == BuiltObjectRole.Base && builtObjectEncounterEventType == BuiltObjectEncounterEventType.PirateAmbush)
            {
                builtObjectEncounterEventType = BuiltObjectEncounterEventType.Acquire;
                encounterDescription = string.Empty;
            }
            builtObject.EncounterEventType = builtObjectEncounterEventType;
            builtObject.EncounterDescription = encounterDescription;
            if (allowCreatures && Rnd.Next(0, 3) == 1 && _CreaturePrevalence > 0.0 && AllowGiantKaltorGeneration)
            {
                GenerateCreatureAtHabitat(CreatureType.Kaltor, habitat, lockLocation: true);
            }
            if ((builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort) && builtObject.Cargo != null)
            {
                for (int i = 0; i < ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; i++)
                {
                    ResourceDefinition resourceDefinition = ResourceSystem.StrategicResourcesOrderedByRelativeImportance[i];
                    if (resourceDefinition != null)
                    {
                        int amount = 300;
                        if (resourceDefinition.IsFuel)
                        {
                            amount = 3000;
                        }
                        builtObject.Cargo.Add(new Cargo(new Resource(resourceDefinition.ResourceID), amount, IndependentEmpire));
                    }
                }
            }
            AbandonedBuiltObjects.Add(builtObject);
            AbandonedShipCount++;
            return builtObject;
        }

        public BuiltObject GenerateStoryAbandonedBuiltObject(double x, double y, Design design, string name)
        {
            BuiltObject builtObject = GenerateUnownedBuiltObjectFromDesign(design, name, null, x, y);
            builtObject.IsAutoControlled = false;
            builtObject.IsAutoControlled = true;
            builtObject.PlayerEmpireEncounterAction = BuiltObjectEncounterAction.Prompt;
            builtObject.EncounterEventType = BuiltObjectEncounterEventType.Acquire;
            AbandonedShipCount++;
            return builtObject;
        }

        private string GetCustomName(Design design)
        {
            string text = string.Empty;
            List<string> list = null;
            if (design.Empire == PlayerEmpire && _SubRoleNameSet != null)
            {
                list = _SubRoleNameSet.GetNames(design.SubRole);
            }
            if (list != null && list.Count > 0)
            {
                bool flag = false;
                BuiltObjectList builtObjectList = new BuiltObjectList();
                builtObjectList.AddRange(design.Empire.BuiltObjects);
                builtObjectList.AddRange(design.Empire.PrivateBuiltObjects);
                for (int num = builtObjectList.Count - 1; num >= 0; num--)
                {
                    if (builtObjectList[num].SubRole == design.SubRole)
                    {
                        int num2 = list.IndexOf(builtObjectList[num].Name);
                        if (num2 >= 0)
                        {
                            if (num2 == list.Count - 1)
                            {
                                flag = true;
                            }
                            else
                            {
                                text = list[num2 + 1];
                            }
                            break;
                        }
                    }
                }
                if (string.IsNullOrEmpty(text) && !flag)
                {
                    text = list[0];
                }
            }
            return text;
        }

        public string SelectUniqueBuiltObjectName(Design design, Habitat parentHabitat)
        {
            string empty = string.Empty;
            empty = GetCustomName(design);
            if (!string.IsNullOrEmpty(empty))
            {
                return empty;
            }
            string text = string.Empty;
            string text2 = string.Empty;
            if (parentHabitat != null)
            {
                text = parentHabitat.Name;
                Habitat habitat = DetermineHabitatSystemStar(parentHabitat);
                text2 = habitat.Name;
            }
            switch (design.SubRole)
            {
                case BuiltObjectSubRole.MiningStation:
                    empty = text + " " + ResolveDescription(BuiltObjectSubRole.MiningStation);
                    break;
                case BuiltObjectSubRole.GasMiningStation:
                    empty = text + " " + ResolveDescription(BuiltObjectSubRole.GasMiningStation);
                    break;
                case BuiltObjectSubRole.Escort:
                case BuiltObjectSubRole.Frigate:
                case BuiltObjectSubRole.Destroyer:
                case BuiltObjectSubRole.Cruiser:
                case BuiltObjectSubRole.CapitalShip:
                case BuiltObjectSubRole.TroopTransport:
                case BuiltObjectSubRole.Carrier:
                case BuiltObjectSubRole.ResupplyShip:
                    empty = SelectRandomUniqueMilitaryShipName(parentHabitat);
                    break;
                case BuiltObjectSubRole.ResortBase:
                    empty = GenerateResortBaseName(parentHabitat);
                    break;
                case BuiltObjectSubRole.MonitoringStation:
                    {
                        string[] array = new string[4] { "Beacon", "Sentinel", "Station", "Monitoring Facility" };
                        empty = ((!string.IsNullOrEmpty(text2)) ? (text2 + " " + array[Rnd.Next(0, array.Length)]) : (array[Rnd.Next(0, array.Length)] + " " + design.BuildCount.ToString("000")));
                        break;
                    }
                case BuiltObjectSubRole.EnergyResearchStation:
                case BuiltObjectSubRole.WeaponsResearchStation:
                case BuiltObjectSubRole.HighTechResearchStation:
                    {
                        string[] array = new string[4] { "Research Center", "Station", "Research Station", "Research Facility" };
                        empty = ((!string.IsNullOrEmpty(text2)) ? (text2 + " " + array[Rnd.Next(0, array.Length)]) : (array[Rnd.Next(0, array.Length)] + " " + design.BuildCount.ToString("000")));
                        break;
                    }
                case BuiltObjectSubRole.DefensiveBase:
                    {
                        string[] array = new string[4]
                        {
                TextResolver.GetText("Defensive Base"),
                TextResolver.GetText("Weapons Platform"),
                TextResolver.GetText("Defense Battery"),
                TextResolver.GetText("Orbital Battery")
                        };
                        empty = ((!string.IsNullOrEmpty(text)) ? (text + " " + array[Rnd.Next(0, array.Length)]) : (array[Rnd.Next(0, array.Length)] + " " + design.BuildCount.ToString("000")));
                        break;
                    }
                case BuiltObjectSubRole.GenericBase:
                    {
                        string[] array = new string[2] { "Base", "Station" };
                        string text3 = array[Rnd.Next(0, array.Length)];
                        if (string.IsNullOrEmpty(empty))
                        {
                            empty = text2 + " " + text3;
                        }
                        break;
                    }
                case BuiltObjectSubRole.ExplorationShip:
                case BuiltObjectSubRole.SmallFreighter:
                case BuiltObjectSubRole.MediumFreighter:
                case BuiltObjectSubRole.LargeFreighter:
                case BuiltObjectSubRole.ColonyShip:
                case BuiltObjectSubRole.PassengerShip:
                case BuiltObjectSubRole.ConstructionShip:
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                    empty = SelectRandomUniqueStandardShipName(parentHabitat);
                    break;
                case BuiltObjectSubRole.SmallSpacePort:
                case BuiltObjectSubRole.MediumSpacePort:
                case BuiltObjectSubRole.LargeSpacePort:
                    empty = text + " " + TextResolver.GetText("Space Port");
                    break;
                default:
                    empty = design.Name + " X";
                    break;
            }
            return empty;
        }

        public string SelectRandomUniqueStandardShipName(Habitat habitat)
        {
            string empty = string.Empty;
            string[] array = new string[127]
            {
            "Lucky", "Grand", "Bright", "Sublime", "Lonesome", "Charming", "Enchanted", "Brazen", "Serene", "Placid",
            "Quiet", "Friendly", "Happy", "Fortunate", "Merry", "Smiling", "Cautious", "Idle", "Brisk", "Bold",
            "Solitary", "Radiant", "Lavish", "Handsome", "Majestic", "Bountiful", "Gallant", "Intrepid", "Valiant", "Stout",
            "Superb", "Regal", "Noble", "Hardy", "Strange", "Shining", "Glowing", "Lively", "Daunting", "Slippery",
            "Crafty", "Risky", "Sneaky", "Lone", "Arduous", "Tenacious", "Outrageous", "Distant", "Doubtful", "Jubilant",
            "Cheerful", "Adamant", "Resolute", "Curious", "Extravagant", "Audacious", "Futile", "Vain", "Aimless", "Cryptic",
            "Prudent", "Worthy", "Honest", "Venerable", "Precious", "Celestial", "Foolish", "Roaming", "Blind", "Dusty",
            "Lost", "Solar", "Swift", "Stellar", "Last", "Wild", "Express", "Rusty", "Far", "Broken",
            "Fading", "Silent", "Ancient", "Pristine", "Shabby", "Tired", "Weary", "Secretive", "Conspicuous", "Hidden",
            "Dubious", "Devious", "Elusive", "Shady", "Wily", "Lawless", "Crooked", "Forbidden", "Wry", "Cowering",
            "Muffled", "Grasping", "Hasty", "Mocking", "Humble", "Sombre", "Solemn", "Eager", "Deep", "Meagre",
            "Frugal", "Daring", "Nimble", "Feeble", "Arcane", "Profound", "Obscure", "Graceful", "Vanishing", "Trusty",
            "Late", "Decrepit", "Grimy", "Surly", "Dire", "Tarnished", "Galactic"
            };
            string[] array2 = new string[125]
            {
            "Queen", "Princess", "Sun", "Star", "Hope", "Chance", "Gamble", "Aspiration", "Traveller", "Voyager",
            "Wayfarer", "Scoundrel", "Wanderer", "Trader", "Merchant", "Encounter", "Scout", "Obsession", "Moon", "Empress",
            "Dream", "Fantasy", "Illusion", "Mirage", "Ruse", "Bluff", "Miracle", "Novelty", "Wonder", "Scheme",
            "Impulse", "Venture", "Wager", "Adventure", "Intrigue", "Luxury", "Challenge", "Maneuver", "Smuggler", "Lurker",
            "Prowler", "Imposter", "Subterfuge", "Mystery", "Enterprise", "Escapade", "Peril", "Ploy", "Quest", "Force",
            "Whim", "Adversity", "Navigator", "Ruse", "Gambit", "Subterfuge", "Pearl", "Jewel", "Treasure", "Prize",
            "Hoard", "Rogue", "Agent", "Envoy", "Guide", "Lady", "Pathfinder", "Expedition", "Journey", "Odyssey",
            "Errand", "Sojourn", "Bargain", "Way", "Guardian", "Dawn", "Echo", "Interlude", "Ranger", "Victory",
            "Renegade", "Starseeker", "Starwind", "Solace", "Pride", "Rimrunner", "Starway", "Beggar", "Rover", "Starfire",
            "Raider", "Deal", "Rendezvous", "Twilight", "Courage", "Burden", "Spirit", "Nightstar", "Profit", "Relic",
            "Bootlegger", "Shroud", "Remorse", "Disturbance", "Trailblazer", "Resolution", "Decoy", "Culprit", "Destiny", "Tramp",
            "Vagrant", "Splendor", "Starrider", "Negotiator", "Partisan", "Discovery", "Distress", "Rebel", "Evasion", "Pathway",
            "Endeavour", "Memory", "Orbit", "Impasse", "Nova"
            };
            int num = Rnd.Next(0, array.Length);
            string text = array[num];
            num = Rnd.Next(0, array2.Length);
            string text2 = array2[num];
            empty = text + " " + text2;
            if (Rnd.Next(0, 7) < 2 && habitat != null)
            {
                Habitat habitat2 = DetermineHabitatSystemStar(habitat);
                if (habitat2.Category == HabitatCategoryType.Star && habitat2.Name.Length < 16 && habitat2.Name.Length > 1)
                {
                    string text3 = habitat2.Name.Substring(1, 1);
                    if (text3.ToLower(CultureInfo.InvariantCulture) == text3)
                    {
                        empty = ((Rnd.Next(0, 3) != 1) ? (habitat2.Name + " " + text2) : (text2 + " of " + habitat2.Name));
                    }
                }
            }
            return empty;
        }

        private string GenerateFleetName()
        {
            string[] array = new string[20]
            {
            "Fleet", "Armada", "Strike Force", "Battle Group", "Task Force", "Strike Group", "Starfleet", "Flotilla", "", "",
            "", "", "", "", "", "", "", "", "", ""
            };
            return array[Rnd.Next(0, array.Length)];
        }

        public string SelectRandomUniqueMilitaryShipName()
        {
            return SelectRandomUniqueMilitaryShipName(null);
        }

        public string SelectRandomUniqueMilitaryShipName(Habitat habitat)
        {
            string empty = string.Empty;
            string empty2 = string.Empty;
            string empty3 = string.Empty;
            string[] array = new string[76]
            {
            "Grievous", "Prime", "Deadly", "Grand", "Black", "Swift", "Mighty", "Dreadful", "Crushing", "Shattering",
            "Silent", "Dark", "Supreme", "Ultimate", "Lethal", "Implacable", "Immortal", "Majestic", "Forceful", "Potent",
            "Great", "Ruinous", "Sinister", "Bleak", "Grim", "Devious", "Overwhelming", "Merciless", "Fearsome", "Cruel",
            "Iron", "Cunning", "Sly", "Fearless", "Insidious", "Evil", "Fearless", "Eternal", "Terrible", "Looming",
            "Overpowering", "Smashing", "Angry", "Raging", "Relentless", "Intrepid", "Wrathful", "Bitter", "Evasive", "Decisive",
            "Proud", "Indomitable", "Elusive", "Inevitable", "Belligerent", "Courageous", "Invincible", "Shrouded", "Growling", "Elite",
            "Final", "Assured", "Lamented", "Wailing", "Banished", "Discarded", "Worthy", "Desperate", "Reckless", "Fatal",
            "Hostile", "Tenacious", "Crimson", "Red", "Scarlet", "Formidable"
            };
            string[] array2 = new string[162]
            {
            "Zenith", "Hand", "Vengeance", "Axe", "Dagger", "Eclipse", "Moon", "Sun", "Phantom", "Executioner",
            "Revenge", "Horizon", "Star", "Crucible", "Action", "Devastation", "Shadow", "Exploit", "Reprisal", "Surprise",
            "Strike", "Judgment", "Courage", "Stealth", "Enigma", "Mystery", "Fist", "Death", "Warrior", "Assassin",
            "Rendezvous", "Fate", "Destiny", "Doom", "Despair", "Curse", "Thunder", "Demise", "Revolution", "Annihilation",
            "Dominator", "Triumph", "Victory", "Conquest", "Invader", "Downfall", "Chaos", "Turmoil", "Anarchy", "Rebellion",
            "Sting", "Leader", "Master", "Victor", "Assault", "Cataclysm", "Tyrant", "Plague", "Fury", "Justice",
            "Reckoning", "Emancipator", "Defender", "Defiance", "Liberty", "Retribution", "Adversary", "Sentinel", "Sentry", "Ravager",
            "Subjugator", "Starfall", "Vigilance", "Starstream", "Inquisitor", "Swarm", "Intruder", "Bandit", "Allegiance", "Behemoth",
            "Emperor", "Firestorm", "Nemesis", "Onslaught", "Predator", "Rampage", "Stalker", "Trap", "Arrow", "Skirmish",
            "Spectre", "Hero", "Verdict", "Mandate", "Dictator", "Decree", "Revolt", "Protector", "Bastion", "Vindication",
            "Guardian", "Shield", "Champion", "Advocate", "Challenger", "Provocation", "Spite", "Mutiny", "Repulser", "Resistance",
            "Liberator", "Deception", "Exile", "Outcast", "Fugitive", "Renegade", "Cutlass", "Affliction", "Conflict", "Aggressor",
            "Banshee", "Battle", "Firelance", "Chariot", "Conqueror", "Demolisher", "Desolation", "Eminence", "Encounter", "Enforcer",
            "Eviscerator", "Exactor", "Fireclaw", "Firestorm", "Dragon", "Gauntlet", "Claw", "Hammer", "Hunter", "Hydra",
            "Intimidator", "Mauler", "Mayhem", "Monarch", "Nexus", "Rage", "Sovereign", "Scorpion", "Scourge", "Serpent",
            "Terror", "Vendetta", "Warlord", "Wolf", "Nightfall", "Night", "Legacy", "Backstab", "Fire", "Marauder",
            "Nova", "Raider"
            };
            int num = Rnd.Next(0, array.Length);
            empty2 = array[num];
            num = Rnd.Next(0, array2.Length);
            empty3 = array2[num];
            empty = empty2 + " " + empty3;
            if (Rnd.Next(0, 5) < 2 && habitat != null)
            {
                Habitat habitat2 = DetermineHabitatSystemStar(habitat);
                if (habitat2.Category == HabitatCategoryType.Star && habitat2.Name.Length < 16 && habitat2.Name.Length > 1)
                {
                    string text = habitat2.Name.Substring(1, 1);
                    if (text.ToLower(CultureInfo.InvariantCulture) == text)
                    {
                        empty = ((Rnd.Next(0, 3) != 1) ? (empty3 + " of " + habitat2.Name) : (habitat2.Name + " " + empty3));
                    }
                }
            }
            return empty;
        }

        public string GenerateBlackHoleName()
        {
            string empty = string.Empty;
            string empty2 = string.Empty;
            string empty3 = string.Empty;
            string[] array = new string[9] { "Devil's", "Dark", "Ravenous", "Deadly", "Perilous", "Traitor's", "Wretched", "Devouring", "Destroyer's" };
            string[] array2 = new string[16]
            {
            "Gate", "Vortex", "Whirlpool", "Wheel", "Lair", "Snare", "Desolation", "End", "Mouth", "Cauldron",
            "Pit", "Abyss", "Chasm", "Dungeon", "Inferno", "Void"
            };
            int num = Rnd.Next(0, array.Length);
            empty2 = array[num];
            num = Rnd.Next(0, array2.Length);
            empty3 = array2[num];
            return empty2 + " " + empty3;
        }

        public string GenerateUniqueAgentName(byte raceFamilyId)
        {
            string[] array = null;
            string[] array2 = null;
            if (raceFamilyId >= 0 && _AgentFirstNames.Count > raceFamilyId && _AgentLastNames.Count > raceFamilyId)
            {
                array = _AgentFirstNames[raceFamilyId];
                array2 = _AgentLastNames[raceFamilyId];
            }
            int num = Rnd.Next(0, array.Length);
            int num2 = Rnd.Next(0, array2.Length);
            return array[num] + " " + array2[num2];
        }

        private BuiltObject GenerateUnownedBuiltObjectFromDesign(Design design, string name, Habitat parentHabitat)
        {
            return GenerateUnownedBuiltObjectFromDesign(design, name, parentHabitat, -2000000001.0, -2000000001.0);
        }

        private BuiltObject GenerateUnownedBuiltObjectFromDesign(Design design, string name, Habitat parentHabitat, double x, double y)
        {
            BuiltObject builtObject = new BuiltObject(design, name, this, fullyBuilt: true, doNotAssignEmpire: true);
            for (int i = 0; i < builtObject.Components.Count; i++)
            {
                builtObject.Components[i].Status = ComponentStatus.Normal;
            }
            builtObject.BuiltObjectID = GetNextBuiltObjectID();
            builtObject.Empire = null;
            builtObject.Owner = null;
            builtObject.DateBuilt = CurrentStarDate;
            builtObject.ParentHabitat = parentHabitat;
            if (parentHabitat != null)
            {
                double x2;
                double y2;
                if (design.Role == BuiltObjectRole.Base)
                {
                    if (parentHabitat.Category == HabitatCategoryType.Star)
                    {
                        double minimumDistance = parentHabitat.Diameter;
                        if (parentHabitat.Type == HabitatType.BlackHole)
                        {
                            minimumDistance = (double)parentHabitat.Diameter * 0.6;
                        }
                        else if (parentHabitat.Type == HabitatType.SuperNova)
                        {
                            minimumDistance = (double)parentHabitat.Diameter * 0.55;
                        }
                        SelectRelativeParkingPoint(minimumDistance, out x2, out y2);
                    }
                    else
                    {
                        SelectRelativeHabitatSurfacePoint(parentHabitat, out x2, out y2);
                        if (parentHabitat.BasesAtHabitat == null)
                        {
                            parentHabitat.BasesAtHabitat = new BuiltObjectList();
                        }
                        parentHabitat.BasesAtHabitat.Add(builtObject);
                    }
                }
                else if (parentHabitat.Category == HabitatCategoryType.Star)
                {
                    double minimumDistance2 = parentHabitat.Diameter;
                    if (parentHabitat.Type == HabitatType.BlackHole)
                    {
                        minimumDistance2 = (double)parentHabitat.Diameter * 0.6;
                    }
                    SelectRelativeParkingPoint(minimumDistance2, out x2, out y2);
                }
                else
                {
                    SelectRelativeParkingPoint(out x2, out y2);
                }
                builtObject.ParentOffsetX = x2;
                builtObject.ParentOffsetY = y2;
                builtObject.Xpos = parentHabitat.Xpos + x2;
                builtObject.Ypos = parentHabitat.Ypos + y2;
            }
            else
            {
                builtObject.Xpos = x;
                builtObject.Ypos = y;
            }
            builtObject.Heading = SelectRandomHeading();
            builtObject.TargetHeading = builtObject.Heading;
            builtObject.SupportCostFactor = 0.5f;
            if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip)
            {
                if (parentHabitat != null && parentHabitat.Population != null && parentHabitat.Population.DominantRace != null)
                {
                    builtObject.NativeRace = parentHabitat.Population.DominantRace;
                }
                else
                {
                    builtObject.NativeRace = SelectRandomRace(0);
                }
            }
            builtObject.ReDefine();
            builtObject.CurrentFuel = builtObject.FuelCapacity;
            builtObject.CurrentShields = builtObject.ShieldsCapacity;
            BuiltObjects.Add(builtObject);
            int x3 = (int)builtObject.Xpos / IndexSize;
            int y3 = (int)builtObject.Ypos / IndexSize;
            CorrectIndexCoords(ref x3, ref y3);
            BuiltObjectIndex[x3][y3].Add(builtObject);
            Habitat habitat = FastFindNearestSystem(builtObject.Xpos, builtObject.Ypos);
            if (habitat != null)
            {
                double num = CalculateDistance(builtObject.Xpos, builtObject.Ypos, habitat.Xpos, habitat.Ypos);
                if (num < (double)MaxSolarSystemSize + 1000.0)
                {
                    builtObject.NearestSystemStar = habitat;
                }
            }
            builtObject.ReDefine();
            return builtObject;
        }

        private string GenerateRuinName(Habitat habitat, out int pictureRef)
        {
            pictureRef = 0;
            List<int> list = new List<int>();
            string empty = string.Empty;
            string text = string.Empty;
            string text2 = string.Empty;
            Habitat habitat2 = DetermineHabitatSystemStar(habitat);
            Race race = null;
            if (habitat.Population != null && habitat.Population.DominantRace != null)
            {
                race = habitat.Population.DominantRace;
            }
            string[] array = new string[15]
            {
            "Hidden", "Great", "Grand", "Forgotten", "Granite", "Lofty", "High", "Exalted", "Stone", "Secluded",
            "", "", "", "", ""
            };
            empty = array[Rnd.Next(0, array.Length)];
            switch (Rnd.Next(0, 19))
            {
                case 0:
                    text = "Hall";
                    list.Add(0);
                    list.Add(10);
                    list.Add(7);
                    list.Add(8);
                    list.Add(14);
                    list.Add(15);
                    break;
                case 1:
                    text = "Temple";
                    list.Add(1);
                    list.Add(2);
                    list.Add(3);
                    list.Add(9);
                    list.Add(10);
                    break;
                case 2:
                    text = "Pyramid";
                    list.Add(1);
                    list.Add(2);
                    break;
                case 3:
                    text = "Citadel";
                    list.Add(3);
                    list.Add(7);
                    list.Add(8);
                    list.Add(14);
                    list.Add(15);
                    break;
                case 4:
                    text = "Fortress";
                    list.Add(7);
                    list.Add(8);
                    list.Add(14);
                    list.Add(15);
                    break;
                case 5:
                    text = "Tower";
                    list.Add(5);
                    list.Add(6);
                    break;
                case 6:
                    text = "Tomb";
                    list.Add(1);
                    list.Add(2);
                    list.Add(9);
                    break;
                case 7:
                    text = "Sanctuary";
                    list.Add(3);
                    list.Add(9);
                    list.Add(10);
                    break;
                case 8:
                    text = "Library";
                    list.Add(0);
                    list.Add(10);
                    break;
                case 9:
                    text = "Palace";
                    list.Add(3);
                    list.Add(7);
                    list.Add(8);
                    break;
                case 10:
                    text = "Archives";
                    list.Add(0);
                    list.Add(10);
                    break;
                case 11:
                    text = "Monastery";
                    list.Add(7);
                    list.Add(8);
                    list.Add(14);
                    list.Add(15);
                    break;
                case 12:
                    text = "Retreat";
                    list.Add(14);
                    list.Add(15);
                    break;
                case 13:
                    text = "Nexus";
                    list.Add(7);
                    list.Add(8);
                    list.Add(13);
                    break;
                case 14:
                    text = "Chamber";
                    list.Add(1);
                    list.Add(2);
                    list.Add(9);
                    break;
                case 15:
                    text = "Pillar";
                    list.Add(5);
                    list.Add(6);
                    break;
                case 16:
                    text = "Obelisk";
                    list.Add(5);
                    list.Add(6);
                    break;
                case 17:
                    text = "Gate";
                    list.Add(4);
                    break;
                case 18:
                    text = "City";
                    list.Add(13);
                    break;
            }
            switch (Rnd.Next(0, 5))
            {
                case 0:
                    text2 = "of " + habitat2.Name;
                    break;
                case 1:
                case 2:
                    text2 = ((race == null) ? ("of " + habitat2.Name) : ("of the " + race.Name + "s"));
                    break;
                case 3:
                case 4:
                    text2 = (string.IsNullOrEmpty(empty) ? ("of " + habitat2.Name) : string.Empty);
                    break;
            }
            List<int> list2 = new List<int>();
            List<int> list3 = new List<int>();
            List<int> list4 = new List<int>();
            list2.Add(15);
            list3.Add(2);
            list3.Add(10);
            list4.Add(1);
            list4.Add(8);
            list4.Add(9);
            if (habitat.Type == HabitatType.MarshySwamp || habitat.Type == HabitatType.Desert || habitat.Type == HabitatType.Volcanic || habitat.Type == HabitatType.Ocean)
            {
                foreach (int item in list2)
                {
                    if (list.Contains(item))
                    {
                        list.Remove(item);
                    }
                }
            }
            if (habitat.Type == HabitatType.MarshySwamp || habitat.Type == HabitatType.Continental || habitat.Type == HabitatType.Ice || habitat.Type == HabitatType.BarrenRock || habitat.Type == HabitatType.Ocean)
            {
                foreach (int item2 in list3)
                {
                    if (list.Contains(item2))
                    {
                        list.Remove(item2);
                    }
                }
            }
            if (habitat.Type == HabitatType.Desert || habitat.Type == HabitatType.Volcanic || habitat.Type == HabitatType.Ice || habitat.Type == HabitatType.BarrenRock)
            {
                foreach (int item3 in list4)
                {
                    if (list.Contains(item3))
                    {
                        list.Remove(item3);
                    }
                }
            }
            if (list.Count > 0)
            {
                pictureRef = list[Rnd.Next(0, list.Count)];
            }
            else
            {
                pictureRef = 0;
            }
            string text3 = string.Empty;
            if (!string.IsNullOrEmpty(empty))
            {
                text3 = text3 + empty + " ";
            }
            text3 += text;
            if (!string.IsNullOrEmpty(text2))
            {
                text3 = text3 + " " + text2;
            }
            return text3;
        }

        public void ObtainRandomGalaxyCoordinates(out double x, out double y)
        {
            x = Rnd.NextDouble() * (double)SizeX;
            y = Rnd.NextDouble() * (double)SizeY;
        }

        public void ObtainRandomGalaxyCoordinatesFromPoint(double startX, double startY, double distance, out double x, out double y)
        {
            int num = 0;
            x = -1.0;
            y = -1.0;
            while ((x < 0.0 || x >= (double)SizeX || y < 0.0 || y >= (double)SizeY) && num < 200)
            {
                double num2 = Rnd.NextDouble() * Math.PI * 2.0;
                double num3 = Math.Cos(num2) * distance;
                double num4 = Math.Sin(num2) * distance;
                x = startX + num3;
                y = startY + num4;
                num++;
            }
        }

        private void ObtainRandomGalaxyCoordinates(double radiusFromCenterMinimum, double radiusFromCenterMaximum, out double x, out double y)
        {
            double num = (double)SizeX / 2.0;
            double num2 = (double)SizeY / 2.0;
            double num3 = (double)SizeX / 2.0;
            double num4 = num3 * radiusFromCenterMinimum;
            double num5 = Rnd.NextDouble() * num3 * (radiusFromCenterMaximum - radiusFromCenterMinimum);
            double num6 = num4 + num5;
            double num7 = Rnd.NextDouble() * Math.PI * 2.0;
            double num8 = Math.Cos(num7) * num6;
            double num9 = Math.Sin(num7) * num6;
            x = num + num8;
            y = num2 + num9;
        }

        private void ClearCompletedPlanetDestroyerProjects()
        {
            GalaxyLocationList galaxyLocationList = new GalaxyLocationList();
            for (int i = 0; i < GalaxyLocations.Count; i++)
            {
                GalaxyLocation galaxyLocation = GalaxyLocations[i];
                if (galaxyLocation.Type == GalaxyLocationType.PlanetDestroyer && galaxyLocation.RelatedBuiltObject != null && (galaxyLocation.RelatedBuiltObject.UnbuiltComponentCount == 0 || galaxyLocation.RelatedBuiltObject.HasBeenDestroyed))
                {
                    galaxyLocationList.Add(galaxyLocation);
                }
            }
            foreach (GalaxyLocation item in galaxyLocationList)
            {
                for (int j = 0; j < Empires.Count; j++)
                {
                    Empire empire = Empires[j];
                    if (empire.KnownGalaxyLocations.Contains(item))
                    {
                        empire.KnownGalaxyLocations.Remove(item);
                    }
                }
                RemoveGalaxyLocationIndex(item);
                GalaxyLocations.Remove(item);
            }
        }

        private void ClearEmptyDebrisFields()
        {
            GalaxyLocationList galaxyLocationList = new GalaxyLocationList();
            for (int i = 0; i < GalaxyLocations.Count; i++)
            {
                GalaxyLocation galaxyLocation = GalaxyLocations[i];
                if (galaxyLocation.Type == GalaxyLocationType.DebrisField)
                {
                    BuiltObjectList builtObjectList = FindAbandonedShipsInDebrisField(galaxyLocation);
                    if (builtObjectList.Count == 0)
                    {
                        galaxyLocationList.Add(galaxyLocation);
                    }
                }
            }
            foreach (GalaxyLocation item in galaxyLocationList)
            {
                for (int j = 0; j < Empires.Count; j++)
                {
                    Empire empire = Empires[j];
                    if (empire.KnownGalaxyLocations.Contains(item))
                    {
                        empire.KnownGalaxyLocations.Remove(item);
                    }
                }
                RemoveGalaxyLocationIndex(item);
                GalaxyLocations.Remove(item);
            }
        }

        public bool CheckShouldExplore(Empire empire, Habitat habitat)
        {
            if (habitat.Ruin != null && (CheckRuinsHaveBenefit(habitat.Ruin, empire) || (empire == PlayerEmpire && (habitat.Ruin.StoryClueLevel >= 0 || !habitat.Ruin.PlayerEmpireEncountered))))
            {
                return true;
            }
            if (empire.ResourceMap != null && empire.ResourceMap.CheckResourcesKnown(habitat))
            {
                return false;
            }
            return true;
        }

        public bool CheckBuiltObjectScanned(BuiltObject builtObject)
        {
            if (builtObject.CurrentSpeed <= (float)builtObject.TopSpeed)
            {
                BuiltObjectList builtObjectsAtLocation = GetBuiltObjectsAtLocation(builtObject.Xpos, builtObject.Ypos, 1000);
                for (int i = 0; i < builtObjectsAtLocation.Count; i++)
                {
                    BuiltObject builtObject2 = builtObjectsAtLocation[i];
                    if (builtObject2 != null && builtObject2.Empire == PlayerEmpire && builtObject2.SensorTraceScannerRange > 0 && builtObject2.SensorTraceScannerPower > builtObject.SensorTraceScannerJamming)
                    {
                        double num = CalculateDistance(builtObject.Xpos, builtObject.Ypos, builtObject2.Xpos, builtObject2.Ypos);
                        if (num < (double)builtObject2.SensorTraceScannerRange)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public FighterList GetFightersForBuiltObjects(BuiltObjectList builtObjects)
        {
            FighterList fighterList = new FighterList();
            for (int i = 0; i < builtObjects.Count; i++)
            {
                BuiltObject builtObject = builtObjects[i];
                if (builtObject != null && builtObject.Fighters != null && builtObject.Fighters.Count > 0)
                {
                    fighterList.AddRange(ListHelper.ToArrayThreadSafe(builtObject.Fighters));
                }
            }
            return fighterList;
        }

        public BuiltObjectList SortBuiltObjectsByDistance(double x, double y, BuiltObjectList builtObjects)
        {
            if (builtObjects != null)
            {
                for (int i = 0; i < builtObjects.Count; i++)
                {
                    double sortTag = CalculateDistanceSquared(x, y, builtObjects[i].Xpos, builtObjects[i].Ypos);
                    builtObjects[i].SortTag = sortTag;
                }
                builtObjects.Sort();
            }
            return builtObjects;
        }

        public BuiltObject FindNearestBuiltObjectInSet(double x, double y, BuiltObjectList builtObjects)
        {
            BuiltObject result = null;
            double num = double.MaxValue;
            if (builtObjects != null)
            {
                for (int i = 0; i < builtObjects.Count; i++)
                {
                    BuiltObject builtObject = builtObjects[i];
                    if (builtObject != null)
                    {
                        double num2 = CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                        if (num2 < num)
                        {
                            num = num2;
                            result = builtObject;
                        }
                    }
                }
            }
            return result;
        }

        public int CalculateNearbyOverallStrength(double x, double y, Empire empire, double range)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            int x2 = (int)x / IndexSize;
            int y2 = (int)y / IndexSize;
            CorrectIndexCoords(ref x2, ref y2);
            builtObjectList.AddRange(ListHelper.ToArrayThreadSafe(BuiltObjectIndex[x2][y2]));
            return CalculateNearbyOverallStrength(x, y, empire, range, builtObjectList);
        }

        public int CalculateNearbyOverallStrength(double x, double y, Empire empire, double range, BuiltObjectList builtObjects)
        {
            int num = 0;
            double num2 = range * range;
            for (int i = 0; i < builtObjects.Count; i++)
            {
                BuiltObject builtObject = builtObjects[i];
                if (builtObject == null || builtObject.HasBeenDestroyed || builtObject.Empire != empire || builtObject.BuiltAt != null)
                {
                    continue;
                }
                if (builtObject.Role == BuiltObjectRole.Base)
                {
                    double num3 = CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                    if (num3 < num2)
                    {
                        num += builtObject.CalculateOverallStrengthFactor();
                    }
                }
                else
                {
                    if (builtObject.Role != BuiltObjectRole.Military)
                    {
                        continue;
                    }
                    bool flag = true;
                    BuiltObjectMission mission = builtObject.Mission;
                    if (mission != null && (mission.Type == BuiltObjectMissionType.Escape || mission.Type == BuiltObjectMissionType.Refuel || mission.Type == BuiltObjectMissionType.Repair || mission.Type == BuiltObjectMissionType.Retire || mission.Type == BuiltObjectMissionType.Retrofit))
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        double num4 = num2;
                        if (builtObject.WarpSpeed >= 0)
                        {
                            num4 = 2304000000.0;
                        }
                        double num5 = CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                        if (num5 < num4)
                        {
                            num += builtObject.CalculateOverallStrengthFactor();
                        }
                    }
                }
            }
            return num;
        }

        public BuiltObjectList GetNearbyBuiltObjects(double x, double y, double range)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            BuiltObjectList builtObjectsAtLocation = GetBuiltObjectsAtLocation(x, y, (int)range);
            double num = range * range;
            for (int i = 0; i < builtObjectsAtLocation.Count; i++)
            {
                BuiltObject builtObject = builtObjectsAtLocation[i];
                if (builtObject != null)
                {
                    double num2 = CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                    if (num2 < num)
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
            }
            return builtObjectList;
        }

        public List<BuiltObject[]> GetBuiltObjectsAtLocationByArrays(double x, double y, int range)
        {
            List<BuiltObject[]> list = new List<BuiltObject[]>();
            int x2 = (int)x / IndexSize;
            int y2 = (int)y / IndexSize;
            CorrectIndexCoords(ref x2, ref y2);
            list.Add(ListHelper.ToArrayThreadSafe(BuiltObjectIndex[x2][y2]));
            int nearestX;
            int nearestY;
            int num = DetermineClosestIndexEdgesCustom((int)x, (int)y, x2, x2, y2, y2, out nearestX, out nearestY);
            if (num < range)
            {
                int num2 = x2 + nearestX;
                int num3 = y2 + nearestY;
                if (num3 < IndexMaxY && num3 >= 0)
                {
                    list.Add(ListHelper.ToArrayThreadSafe(BuiltObjectIndex[x2][num3]));
                }
                if (num2 < IndexMaxX && num2 >= 0)
                {
                    list.Add(ListHelper.ToArrayThreadSafe(BuiltObjectIndex[num2][y2]));
                }
                if (num2 < IndexMaxX && num2 >= 0 && num3 < IndexMaxY && num3 >= 0)
                {
                    list.Add(ListHelper.ToArrayThreadSafe(BuiltObjectIndex[num2][num3]));
                }
            }
            return list;
        }

        public BuiltObjectList GetBuiltObjectsAtLocation(double x, double y, int range)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            int x2 = (int)x / IndexSize;
            int y2 = (int)y / IndexSize;
            CorrectIndexCoords(ref x2, ref y2);
            builtObjectList.AddRange(ListHelper.ToArrayThreadSafe(BuiltObjectIndex[x2][y2]));
            int nearestX;
            int nearestY;
            int num = DetermineClosestIndexEdgesCustom((int)x, (int)y, x2, x2, y2, y2, out nearestX, out nearestY);
            if (num < range)
            {
                int num2 = x2 + nearestX;
                int num3 = y2 + nearestY;
                if (num3 < IndexMaxY && num3 >= 0)
                {
                    builtObjectList.AddRange(ListHelper.ToArrayThreadSafe(BuiltObjectIndex[x2][num3]));
                }
                if (num2 < IndexMaxX && num2 >= 0)
                {
                    builtObjectList.AddRange(ListHelper.ToArrayThreadSafe(BuiltObjectIndex[num2][y2]));
                }
                if (num2 < IndexMaxX && num2 >= 0 && num3 < IndexMaxY && num3 >= 0)
                {
                    builtObjectList.AddRange(ListHelper.ToArrayThreadSafe(BuiltObjectIndex[num2][num3]));
                }
            }
            return builtObjectList;
        }

        public BuiltObjectList GetBuiltObjectsInRectangle(int x, int y, int width, int height)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            int x2 = x / IndexSize;
            int y2 = y / IndexSize;
            CorrectIndexCoords(ref x2, ref y2);
            int x3 = (x + width) / IndexSize;
            int y3 = (y + height) / IndexSize;
            CorrectIndexCoords(ref x3, ref y3);
            for (int i = x2; i <= x3; i++)
            {
                for (int j = y2; j <= y3; j++)
                {
                    builtObjectList.AddRange(ListHelper.ToArrayThreadSafe(BuiltObjectIndex[i][j]));
                }
            }
            return builtObjectList;
        }

        public HabitatList GetHabitatsAtLocation(double x, double y, int range)
        {
            int x2 = (int)x / IndexSize;
            int y2 = (int)y / IndexSize;
            CorrectIndexCoords(ref x2, ref y2);
            HabitatList habitatList = HabitatIndex[x2][y2];
            int nearestX;
            int nearestY;
            int num = DetermineClosestIndexEdgesCustom((int)x, (int)y, x2, x2, y2, y2, out nearestX, out nearestY);
            if (num < range)
            {
                int num2 = x2 + nearestX;
                int num3 = y2 + nearestY;
                habitatList = new HabitatList();
                habitatList.AddRange(HabitatIndex[x2][y2]);
                if (num3 < IndexMaxY && num3 >= 0)
                {
                    habitatList.AddRange(HabitatIndex[x2][num3]);
                }
                if (num2 < IndexMaxX && num2 >= 0)
                {
                    habitatList.AddRange(HabitatIndex[num2][y2]);
                }
                if (num2 < IndexMaxX && num2 >= 0 && num3 < IndexMaxY && num3 >= 0)
                {
                    habitatList.AddRange(HabitatIndex[num2][num3]);
                }
            }
            return habitatList;
        }

        public void RebuildIndexes()
        {
            RebuildHabitatIndexes();
            RebuildSystemIndexes();
            RebuildGalaxyLocationIndexes();
            RebuildBuiltObjectIndexes();
        }

        public void RebuildBuiltObjectIndexes()
        {
            BuiltObjectIndex = new BuiltObjectList[IndexMaxX][];
            for (int i = 0; i < IndexMaxX; i++)
            {
                BuiltObjectIndex[i] = new BuiltObjectList[IndexMaxY];
                for (int j = 0; j < BuiltObjectIndex[i].Length; j++)
                {
                    BuiltObjectIndex[i][j] = new BuiltObjectList();
                }
            }
            for (int k = 0; k < BuiltObjects.Count; k++)
            {
                BuiltObject builtObject = BuiltObjects[k];
                if (builtObject != null && !builtObject.HasBeenDestroyed)
                {
                    GalaxyIndex galaxyIndex = ResolveIndex(builtObject.Xpos, builtObject.Ypos);
                    BuiltObjectIndex[galaxyIndex.X][galaxyIndex.Y].Add(builtObject);
                }
            }
        }

        public void RebuildGalaxyLocationIndexes()
        {
            GalaxyLocationIndex = new GalaxyLocationList[IndexMaxX][];
            for (int i = 0; i < IndexMaxX; i++)
            {
                GalaxyLocationIndex[i] = new GalaxyLocationList[IndexMaxY];
                for (int j = 0; j < GalaxyLocationIndex[i].Length; j++)
                {
                    GalaxyLocationIndex[i][j] = new GalaxyLocationList();
                }
            }
            for (int k = 0; k < GalaxyLocations.Count; k++)
            {
                GalaxyLocation galaxyLocation = GalaxyLocations[k];
                if (galaxyLocation == null)
                {
                    continue;
                }
                GalaxyIndex galaxyIndex = ResolveIndex(galaxyLocation.Xpos, galaxyLocation.Ypos);
                GalaxyIndex galaxyIndex2 = ResolveIndex(galaxyLocation.Xpos + galaxyLocation.Width, galaxyLocation.Ypos + galaxyLocation.Height);
                for (int l = galaxyIndex.X; l <= galaxyIndex2.X; l++)
                {
                    for (int m = galaxyIndex.Y; m <= galaxyIndex2.Y; m++)
                    {
                        if (!GalaxyLocationIndex[l][m].Contains(galaxyLocation))
                        {
                            GalaxyLocationIndex[l][m].Add(galaxyLocation);
                        }
                    }
                }
            }
        }

        public void RebuildSystemIndexes()
        {
            SystemsIndex = new SystemInfoList[IndexMaxX][];
            for (int i = 0; i < IndexMaxX; i++)
            {
                SystemsIndex[i] = new SystemInfoList[IndexMaxY];
                for (int j = 0; j < SystemsIndex[i].Length; j++)
                {
                    SystemsIndex[i][j] = new SystemInfoList();
                }
            }
            for (int k = 0; k < Systems.Count; k++)
            {
                SystemInfo systemInfo = Systems[k];
                if (systemInfo != null && systemInfo.SystemStar != null)
                {
                    GalaxyIndex galaxyIndex = ResolveIndex(systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
                    SystemsIndex[galaxyIndex.X][galaxyIndex.Y].Add(systemInfo);
                }
            }
        }

        public void RebuildHabitatIndexes()
        {
            HabitatIndex = new HabitatList[IndexMaxX][];
            for (int i = 0; i < IndexMaxX; i++)
            {
                HabitatIndex[i] = new HabitatList[IndexMaxY];
                for (int j = 0; j < HabitatIndex[i].Length; j++)
                {
                    HabitatIndex[i][j] = new HabitatList();
                }
            }
            for (int k = 0; k < Habitats.Count; k++)
            {
                Habitat habitat = Habitats[k];
                if (habitat != null && !habitat.HasBeenDestroyed)
                {
                    GalaxyIndex galaxyIndex = ResolveIndex(habitat.Xpos, habitat.Ypos);
                    HabitatIndex[galaxyIndex.X][galaxyIndex.Y].Add(habitat);
                }
            }
        }

        public static string OrderedNumberDescription(int number)
        {
            string text = number.ToString();
            switch (text.Substring(text.Length - 1, 1))
            {
                case "0":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    text += "th";
                    break;
                case "1":
                    text = ((number % 100 != 11) ? (text + "st") : (text + "th"));
                    break;
                case "2":
                    text = ((number % 100 != 12) ? (text + "nd") : (text + "th"));
                    break;
                case "3":
                    text = ((number % 100 != 13) ? (text + "rd") : (text + "th"));
                    break;
            }
            return text;
        }

        public EmpirePriorityList DetermineOrderedKnownEmpires(Empire empire, EmpireComparisonType comparisonType)
        {
            EmpireList empireList = new EmpireList();
            empireList.Add(empire);
            foreach (DiplomaticRelation diplomaticRelation in empire.DiplomaticRelations)
            {
                if (diplomaticRelation.Type != 0)
                {
                    empireList.Add(diplomaticRelation.OtherEmpire);
                }
            }
            EmpirePriorityList empirePriorityList = new EmpirePriorityList();
            foreach (Empire item2 in empireList)
            {
                double priority = 0.0;
                switch (comparisonType)
                {
                    case EmpireComparisonType.Population:
                        priority = item2.TotalPopulation;
                        break;
                    case EmpireComparisonType.Territory:
                        priority = item2.Colonies.Count;
                        break;
                    case EmpireComparisonType.Economy:
                        priority = item2.PrivateAnnualRevenue;
                        break;
                    case EmpireComparisonType.StrategicValue:
                        priority = item2.TotalColonyStrategicValue;
                        break;
                    case EmpireComparisonType.MilitaryStrength:
                        priority = item2.MilitaryPotency;
                        break;
                }
                EmpirePriority item = new EmpirePriority(item2, priority);
                empirePriorityList.Add(item);
            }
            empirePriorityList.Sort();
            empirePriorityList.Reverse();
            return empirePriorityList;
        }

        public BuiltObjectList FindAbandonedShipsInDebrisField(GalaxyLocation location)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            if (location != null && location.Type == GalaxyLocationType.DebrisField)
            {
                double num = (double)location.Width / 2.0;
                double num2 = (double)location.Height / 2.0;
                int range = (int)Math.Max((double)location.Width / 2.0, (double)location.Height / 2.0);
                BuiltObjectList builtObjectsAtLocation = GetBuiltObjectsAtLocation((double)location.Xpos + num, (double)location.Ypos + num2, range);
                double num3 = (double)location.Xpos - (double)location.Width / 2.0;
                double num4 = (double)location.Xpos + (double)location.Width / 2.0;
                double num5 = (double)location.Ypos - (double)location.Height / 2.0;
                double num6 = (double)location.Ypos + (double)location.Height / 2.0;
                for (int i = 0; i < builtObjectsAtLocation.Count; i++)
                {
                    BuiltObject builtObject = builtObjectsAtLocation[i];
                    if (builtObject != null && builtObject.Empire == null && builtObject.Xpos > num3 && builtObject.Xpos < num4 && builtObject.Ypos > num5 && builtObject.Ypos < num6 && !builtObjectList.Contains(builtObject))
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
            }
            return builtObjectList;
        }

        public string GeneratePlanetDestroyerName()
        {
            string[] array = new string[4] { "World Destroyer", "Devastation Moon", "Desolation Moon", "World Annihilator" };
            return array[Rnd.Next(0, array.Length)];
        }

        public void GeneratePlanetDestroyer()
        {
            Habitat habitat = FindLonelyHabitat();
            string name = GeneratePlanetDestroyerName();
            if (habitat == null)
            {
                return;
            }
            BuiltObject builtObject = GenerateIncompletePlanetDestroyer(name, habitat);
            BuiltObject builtObject2 = null;
            Design design = null;
            Design design2 = null;
            Design design3 = null;
            if (Empires.Count > 0)
            {
                int index = Rnd.Next(0, Empires.Count);
                double techAdvanceAmount = 6.0;
                design = Empires[index].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Frigate), techAdvanceAmount);
                design2 = Empires[index].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Destroyer), techAdvanceAmount);
                design3 = Empires[index].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Cruiser), techAdvanceAmount);
            }
            else if (PirateEmpires.Count > 0)
            {
                int index2 = Rnd.Next(0, PirateEmpires.Count);
                double techAdvanceAmount2 = 6.0;
                design = PirateEmpires[index2].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Frigate), techAdvanceAmount2);
                design2 = PirateEmpires[index2].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Destroyer), techAdvanceAmount2);
                design3 = PirateEmpires[index2].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Cruiser), techAdvanceAmount2);
            }
            if (design != null && design2 != null && design3 != null)
            {
                design.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(ShipImageHelper.ShakturiFamily, design.SubRole, aged: false);
                design2.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(ShipImageHelper.ShakturiFamily, design2.SubRole, aged: false);
                design3.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(ShipImageHelper.ShakturiFamily, design3.SubRole, aged: false);
                builtObject2 = GenerateUnownedShipAtLocation(design, habitat.Xpos, habitat.Ypos);
                DamageBuiltObjectComponents(builtObject2, 0.5);
                builtObject2 = GenerateUnownedShipAtLocation(design, habitat.Xpos, habitat.Ypos);
                DamageBuiltObjectComponents(builtObject2, 0.7);
                builtObject2 = GenerateUnownedShipAtLocation(design, habitat.Xpos, habitat.Ypos);
                DamageBuiltObjectComponents(builtObject2, 0.3);
                builtObject2 = GenerateUnownedShipAtLocation(design2, habitat.Xpos, habitat.Ypos);
                builtObject2 = GenerateUnownedShipAtLocation(design2, habitat.Xpos, habitat.Ypos);
                DamageBuiltObjectComponents(builtObject2, 0.6);
                builtObject2 = GenerateUnownedShipAtLocation(design3, habitat.Xpos, habitat.Ypos);
                DamageBuiltObjectComponents(builtObject2, 0.3);
                GalaxyLocation galaxyLocation = new GalaxyLocation(string.Format(TextResolver.GetText("X Project"), builtObject.Name), GalaxyLocationType.PlanetDestroyer, builtObject.Xpos - 600.0, builtObject.Ypos - 600.0, 1200.0, 1200.0, -1);
                galaxyLocation.RelatedBuiltObject = builtObject;
                int amount = Rnd.Next(5, 8);
                galaxyLocation.RelatedCreatures = GenerateCreaturesAtLocation(CreatureType.RockSpaceSlug, amount, builtObject.Xpos, builtObject.Ypos, 350, 280);
                CreatureList creatureList = GenerateCreaturesAtLocation(CreatureType.RockSpaceSlug, 1, builtObject.Xpos, builtObject.Ypos, 300, 150);
                Habitat habitat2 = DetermineHabitatSystemStar(habitat);
                if (creatureList != null && creatureList.Count > 0 && habitat2 != null)
                {
                    creatureList[0].Name = string.Format(TextResolver.GetText("Guardian of X"), habitat2.Name);
                    creatureList[0].Size = Rnd.Next(420, 520);
                    creatureList[0].MaxSize = 620;
                    creatureList[0].AttackStrength = (int)((double)creatureList[0].Size / 20.0);
                    creatureList[0].DamageKillThreshhold = (int)((double)creatureList[0].Size * 1.1);
                    creatureList[0].SetMovementSpeed(11);
                    galaxyLocation.RelatedCreatures.AddRange(creatureList);
                }
                galaxyLocation.ShowName = true;
                _GalaxyLocations.Add(galaxyLocation);
                AddGalaxyLocationIndex(galaxyLocation);
            }
        }

        public BuiltObject GenerateUnownedShipAtLocation(Design design, double x, double y)
        {
            string name = SelectUniqueBuiltObjectName(design, null);
            SelectRelativeParkingPoint(out var x2, out var y2);
            BuiltObject builtObject = GenerateUnownedBuiltObjectFromDesign(design, name, null, x + x2, y + y2);
            builtObject.IsAutoControlled = true;
            builtObject.CurrentFuel = (double)builtObject.FuelCapacity * 0.1 + Rnd.NextDouble() * 0.8 * (double)builtObject.FuelCapacity;
            builtObject.PlayerEmpireEncounterAction = BuiltObjectEncounterAction.Notify;
            return builtObject;
        }

        public BuiltObject GenerateUnownedShipAtHabitat(Design design, Habitat parentHabitat)
        {
            string name = SelectUniqueBuiltObjectName(design, null);
            BuiltObject builtObject = GenerateUnownedBuiltObjectFromDesign(design, name, parentHabitat);
            builtObject.IsAutoControlled = true;
            builtObject.CurrentFuel = (double)builtObject.FuelCapacity * 0.1 + Rnd.NextDouble() * 0.8 * (double)builtObject.FuelCapacity;
            return builtObject;
        }

        public void GenerateRavagerFleet()
        {
        }

        public void GenerateDMZ()
        {
        }

        public void GenerateDebrisFieldLarge()
        {
            int shipCount = Rnd.Next(15, 23);
            GenerateDebrisField(shipCount);
        }

        public void GenerateDebrisFieldSmall()
        {
            int shipCount = Rnd.Next(6, 10);
            GenerateDebrisField(shipCount);
        }

        private void GenerateDebrisField(int shipCount)
        {
            Habitat habitat = FindLonelyHabitat();
            if (habitat != null)
            {
                Habitat habitat2 = DetermineHabitatSystemStar(habitat);
                string name = string.Format(TextResolver.GetText("X Debris Field"), habitat2.Name);
                SelectRelativeParkingPoint(495.0, out var x, out var y);
                GenerateDebrisField(habitat.Xpos + x, habitat.Ypos + y, name, shipCount);
            }
        }

        public void GenerateDebrisField(double x, double y, string name, int shipCount)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = string.Format(TextResolver.GetText("X Debris Field"), string.Empty);
            }
            double width = Rnd.Next(1100, 1500);
            double height = Rnd.Next(1100, 1500);
            GalaxyLocation galaxyLocation = new GalaxyLocation(name, GalaxyLocationType.DebrisField, x, y, width, height, -1);
            galaxyLocation.ShowName = true;
            GalaxyLocations.Add(galaxyLocation);
            AddGalaxyLocationIndex(galaxyLocation);
            int family = Rnd.Next(0, 4);
            Design design = null;
            Design design2 = null;
            Design design3 = null;
            Design design4 = null;
            Design design5 = null;
            Design design6 = null;
            if (Empires.Count > 0)
            {
                int index = Rnd.Next(0, Empires.Count);
                double techAdvanceAmount = 3.0;
                design = Empires[index].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Frigate), techAdvanceAmount);
                design2 = Empires[index].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Destroyer), techAdvanceAmount);
                design3 = Empires[index].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Cruiser), techAdvanceAmount);
                design4 = Empires[index].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.CapitalShip), techAdvanceAmount);
                design5 = Empires[index].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.TroopTransport), techAdvanceAmount);
                design6 = Empires[index].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.CapitalShip), techAdvanceAmount);
            }
            else if (PirateEmpires.Count > 0)
            {
                int index2 = Rnd.Next(0, PirateEmpires.Count);
                double techAdvanceAmount2 = 3.0;
                design = PirateEmpires[index2].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Frigate), techAdvanceAmount2);
                design2 = PirateEmpires[index2].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Destroyer), techAdvanceAmount2);
                design3 = PirateEmpires[index2].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Cruiser), techAdvanceAmount2);
                design4 = PirateEmpires[index2].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.CapitalShip), techAdvanceAmount2);
                design5 = PirateEmpires[index2].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.TroopTransport), techAdvanceAmount2);
                design6 = PirateEmpires[index2].GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.CapitalShip), techAdvanceAmount2);
            }
            for (int i = 0; i < shipCount; i++)
            {
                Design design7 = null;
                switch (Rnd.Next(0, 15))
                {
                    case 0:
                    case 1:
                        design7 = design;
                        break;
                    case 2:
                    case 3:
                    case 4:
                        design7 = design2;
                        break;
                    case 5:
                    case 6:
                    case 7:
                        design7 = design3;
                        break;
                    case 8:
                    case 9:
                    case 10:
                        design7 = design4;
                        break;
                    case 11:
                    case 12:
                        design7 = design5;
                        break;
                    case 13:
                    case 14:
                        design7 = design6;
                        break;
                }
                if (design7 != null)
                {
                    design7.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(family, design7.SubRole, aged: false);
                    string name2 = SelectUniqueBuiltObjectName(design7, null);
                    SelectRelativePoint((double)Math.Min(galaxyLocation.Width, galaxyLocation.Height) / 2.0, out var x2, out var y2);
                    BuiltObject builtObject = GenerateUnownedBuiltObjectFromDesign(design7, name2, null, (double)(galaxyLocation.Xpos + galaxyLocation.Width / 2f) + x2, (double)(galaxyLocation.Ypos + galaxyLocation.Height / 2f) + y2);
                    builtObject.IsAutoControlled = true;
                    builtObject.PlayerEmpireEncounterAction = BuiltObjectEncounterAction.Notify;
                    int num = Rnd.Next(5, builtObject.Components.Count - 1);
                    for (int j = 0; j < num; j++)
                    {
                        int index3 = Rnd.Next(0, builtObject.Components.Count);
                        builtObject.Components[index3].Status = ComponentStatus.Damaged;
                    }
                    builtObject.ReDefine();
                    builtObject.CurrentFuel = (double)builtObject.FuelCapacity * 0.2 + Rnd.NextDouble() * 0.7 * (double)builtObject.FuelCapacity;
                    galaxyLocation.RelatedBuiltObject = builtObject;
                }
            }
            if (AllowGiantKaltorGeneration)
            {
                int amount = Math.Max(5, (int)((double)shipCount / 3.0));
                galaxyLocation.RelatedCreatures = GenerateCreaturesAtLocation(CreatureType.Kaltor, amount, (double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0, (double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0, (int)((double)galaxyLocation.Width / 2.0) + 150, (int)((double)galaxyLocation.Width / 2.0) - 50);
            }
        }

        private bool CheckAllStoryCluesUsed()
        {
            foreach (bool item in StoryClueUsed)
            {
                if (!item)
                {
                    return false;
                }
            }
            return true;
        }

        public int SelectUnusedStoryClue()
        {
            int num = 0;
            bool condition = true;
            int iterationCount = 0;
            while (ConditionCheckLimit(condition, 50, ref iterationCount))
            {
                num++;
                if (num < StoryClueUsed.Count)
                {
                    condition = StoryClueUsed[num];
                    if (StoryClueLocations.Count > num && StoryClueLocations[num].HasBeenDestroyed)
                    {
                        condition = true;
                    }
                }
                else
                {
                    num = -1;
                    condition = false;
                }
            }
            return num;
        }

        public bool CheckStoryLocationHintExists()
        {
            if (StoryCluesEnabled && !CheckAllStoryCluesUsed())
            {
                int num = SelectUnusedStoryClue();
                if (num >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public string CheckForStoryLocationHint()
        {
            string result = string.Empty;
            if (StoryCluesEnabled && !CheckAllStoryCluesUsed())
            {
                int num = SelectUnusedStoryClue();
                if (num >= 0)
                {
                    StellarObject stellarObject = StoryClueLocations[num];
                    if (!stellarObject.HasBeenDestroyed)
                    {
                        result = string.Format(TextResolver.GetText("coordinates X,Y"), stellarObject.Xpos.ToString("0,K"), stellarObject.Ypos.ToString("0,K"));
                        result = result + ", " + GenerateLocationDescription(stellarObject.Xpos, stellarObject.Ypos, prefixWithA: true);
                        PlayerEmpire.AddLocationHint(new Point((int)stellarObject.Xpos, (int)stellarObject.Ypos));
                    }
                }
            }
            return result;
        }

        public string GenerateIndependentColonyStoryClue(Habitat colony)
        {
            string result = string.Empty;
            int num = SelectUnusedStoryClue();
            num--;
            List<int> list = new List<int>();
            list.Add(0);
            list.Add(0);
            list.Add(1);
            list.Add(1);
            List<int> list2 = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] <= num && !StorySecondaryClueUsed[i])
                {
                    list2.Add(i);
                }
            }
            int num2 = -1;
            if (list2.Count > 0)
            {
                int index = Rnd.Next(0, list2.Count);
                StorySecondaryClueUsed[list2[index]] = true;
                num2 = list2[index];
            }
            if (num2 >= 0)
            {
                result = GenerateSecondaryStoryClue(num2, colony);
            }
            return result;
        }

        public int SelectUnusedSecondaryStoryClueIndex()
        {
            int result = -1;
            if (StoryDistantWorldsEnabled)
            {
                int num = SelectUnusedStoryClue();
                if (num < 0)
                {
                    num = 6;
                }
                num--;
                List<int> list = new List<int>();
                list.Add(0);
                list.Add(0);
                list.Add(1);
                list.Add(1);
                list.Add(2);
                list.Add(3);
                list.Add(4);
                list.Add(4);
                list.Add(5);
                List<int> list2 = new List<int>();
                for (int i = 4; i < list.Count; i++)
                {
                    if (list[i] <= num && !StorySecondaryClueUsed[i])
                    {
                        list2.Add(i);
                    }
                }
                if (list2.Count > 0)
                {
                    int index = Rnd.Next(0, list2.Count);
                    result = list2[index];
                }
            }
            return result;
        }

        public string GenerateBuiltObjectStoryClue(BuiltObject builtObject)
        {
            string result = string.Empty;
            int num = SelectUnusedSecondaryStoryClueIndex();
            if (num >= 0)
            {
                StorySecondaryClueUsed[num] = true;
                result = GenerateSecondaryStoryClue(num, builtObject);
            }
            return result;
        }

        public string GenerateMajorStoryItem(int storyLevel)
        {
            string text = string.Empty;
            switch (storyLevel)
            {
                case 0:
                    text += TextResolver.GetText("MajorStoryEvent1");
                    break;
                case 1:
                    text += TextResolver.GetText("MajorStoryEvent2");
                    break;
                case 2:
                    text += TextResolver.GetText("MajorStoryEvent3");
                    break;
                case 3:
                    text += TextResolver.GetText("MajorStoryEvent4");
                    break;
                case 4:
                    text += TextResolver.GetText("MajorStoryEvent5");
                    break;
            }
            return text;
        }

        public string GenerateMajorStoryVictoryMessage(GameEndOutcome outcome)
        {
            string text = string.Empty;
            switch (outcome)
            {
                case GameEndOutcome.Victory:
                    text += TextResolver.GetText("ShakturiPlayerVictory");
                    break;
                case GameEndOutcome.Defeat:
                    text += TextResolver.GetText("ShakturiPlayerDefeat");
                    break;
            }
            return text;
        }

        private string GenerateSecondaryStoryClue(int selectionValue, object clueLocation)
        {
            string result = string.Empty;
            string text = TextResolver.GetText("SecondaryStoryClue1");
            string text2 = TextResolver.GetText("SecondaryStoryClue2");
            string text3 = TextResolver.GetText("SecondaryStoryClue3");
            string text4 = TextResolver.GetText("SecondaryStoryClue4");
            string text5 = TextResolver.GetText("SecondaryStoryClue5");
            string text6 = TextResolver.GetText("SecondaryStoryClue6");
            string text7 = TextResolver.GetText("SecondaryStoryClue7");
            string text8 = TextResolver.GetText("SecondaryStoryClue8");
            string text9 = TextResolver.GetText("SecondaryStoryClue9");
            switch (selectionValue)
            {
                case 0:
                    result = text;
                    break;
                case 1:
                    result = text2;
                    break;
                case 2:
                    result = text3;
                    break;
                case 3:
                    result = text4;
                    break;
                case 4:
                    result = text5;
                    break;
                case 5:
                    result = text6;
                    break;
                case 6:
                    result = text7;
                    break;
                case 7:
                    result = text8;
                    break;
                case 8:
                    {
                        FindLonelyNebulaLocation(out var x, out var y, GalaxyLocationEffectType.MovementSlowed);
                        if (x != 175000.0 && y != 122000.0)
                        {
                            Design design = PlayerEmpire.GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.SmallSpacePort), 0.0);
                            string name = TextResolver.GetText("Archival Refuge Station");
                            Habitat habitat = FastFindNearestSystemWithPlanets(x, y);
                            if (habitat != null)
                            {
                                name = string.Format(TextResolver.GetText("X Archival Refuge"), habitat.Name);
                            }
                            design = design.Clone();
                            design.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(ShipImageHelper.FreedomAllianceFamily, design.SubRole, aged: false);
                            BuiltObject builtObject = GenerateStoryAbandonedBuiltObject(x, y, design, name);
                            builtObject.EncounterTechAdvanceCount = 8;
                            builtObject.EncounterMoneyBonus = Rnd.Next(110000, 200000);
                            int minValue = (int)((double)StarCount * 0.15);
                            int maxValue = (int)((double)StarCount * 0.25);
                            builtObject.EncounterExplorationBonus = (short)Rnd.Next(minValue, maxValue);
                            GovernmentAttributes firstByAvailability = Governments.GetFirstByAvailability(2);
                            if (firstByAvailability != null)
                            {
                                builtObject.EncounterGovernmentTypeId = (byte)firstByAvailability.GovernmentId;
                            }
                            Point location = new Point((int)builtObject.Xpos, (int)builtObject.Ypos);
                            PlayerEmpire.AddLocationHint(location);
                            string text10 = GenerateLocationDescription(x, y, prefixWithA: true);
                            text9 += string.Format(TextResolver.GetText("SecondaryStoryClue9 Location"), builtObject.Name, x.ToString("0,K"), y.ToString("0,K"), text10);
                        }
                        text9 += "========================================";
                        result = text9;
                        break;
                    }
            }
            return result;
        }

        public string GenerateStoryClue(StellarObject location)
        {
            string result = string.Empty;
            string text = string.Empty;
            if (location is BuiltObject && ((BuiltObject)location).Empire == null)
            {
                text = GenerateBuiltObjectStoryClue((BuiltObject)location);
            }
            if (!string.IsNullOrEmpty(text))
            {
                result = text;
            }
            int num = StoryClueLocations.IndexOf(location);
            if (num < 0)
            {
                return result;
            }
            string text2 = TextResolver.GetText("StoryClue1");
            string text3 = TextResolver.GetText("StoryClue2");
            string text4 = TextResolver.GetText("StoryClue3");
            string text5 = TextResolver.GetText("StoryClue4");
            string text6 = TextResolver.GetText("StoryClue5");
            string text7 = "";
            text7 = text7 ?? "";
            switch (num)
            {
                case 0:
                    result = text2;
                    break;
                case 1:
                    result = text3;
                    break;
                case 2:
                    result = text4;
                    break;
                case 3:
                    result = text5;
                    break;
                case 4:
                    result = text6;
                    break;
                case 5:
                    result = text7;
                    break;
            }
            if (!StoryCluesEnabled)
            {
                StoryCluesEnabled = true;
            }
            StoryClueUsed[num] = true;
            return result;
        }

        public bool CheckRuinBonuses(Ruin ruin)
        {
            if (ruin != null && (ruin.BonusDefensive > 0.0 || ruin.BonusDiplomacy > 0.0 || ruin.BonusHappiness > 0.0 || ruin.BonusResearchEnergy > 0.0 || ruin.BonusResearchHighTech > 0.0 || ruin.BonusResearchWeapons > 0.0 || ruin.BonusWealth > 0.0))
            {
                return true;
            }
            return false;
        }

        public bool CheckRuinsHaveBenefit(Ruin ruin, Empire empire)
        {
            bool result = false;
            if (ruin != null)
            {
                if (empire != null && empire.PirateEmpireBaseHabitat != null && empire != IndependentEmpire)
                {
                    return false;
                }
                if (ruin.GameEventId >= 0)
                {
                    return true;
                }
                if (ruin.ResearchBonus > 0 || ruin.MapSystemReveal > 0 || ruin.MoneyBonus > 0.0)
                {
                    result = true;
                }
                if (empire == PlayerEmpire && ruin.StoryClueLevel >= 0)
                {
                    result = true;
                }
                switch (ruin.Type)
                {
                    case RuinType.CreatureSwarm:
                    case RuinType.CreatureSwarmSilverMist:
                        if (!ruin.CreatureSwarmGenerated)
                        {
                            result = true;
                        }
                        break;
                    case RuinType.PirateAmbush:
                        if (!ruin.PirateAmbushGenerated)
                        {
                            result = true;
                        }
                        break;
                    case RuinType.Component:
                    case RuinType.UnlockResearchProject:
                        if (ruin.ResearchProjectId >= 0)
                        {
                            result = true;
                            if (ruin.Type == RuinType.UnlockResearchProject && empire != null && empire.Research != null && empire.Research.TechTree != null && empire.Research.TechTree[ruin.ResearchProjectId].IsEnabled)
                            {
                                result = false;
                            }
                        }
                        break;
                    case RuinType.Government:
                        if (ruin.SpecialGovernmentId >= 0)
                        {
                            result = true;
                        }
                        break;
                    case RuinType.NewPopulation:
                        if (ruin.HabitatNewRace != null)
                        {
                            result = true;
                        }
                        break;
                    case RuinType.Origins:
                        if (ruin.OriginsApprovalRatingBonus != 0)
                        {
                            result = true;
                        }
                        break;
                    case RuinType.Refugees:
                        if (!ruin.RefugeesGenerated)
                        {
                            result = true;
                        }
                        break;
                    case RuinType.LostBuiltObject:
                        if (!ruin.LostBuiltObjectGenerated)
                        {
                            result = true;
                        }
                        break;
                    case RuinType.LostColony:
                        if (!ruin.LostColonyGenerated)
                        {
                            result = true;
                        }
                        break;
                    case RuinType.StoryEvent:
                        if (ruin.StoryEventData > 0)
                        {
                            result = true;
                        }
                        break;
                }
            }
            return result;
        }

        public void InvestigateRuins(Empire investigatingEmpire, Habitat ruinsHabitat)
        {
            if (ruinsHabitat == null || investigatingEmpire == null)
            {
                return;
            }
            Ruin ruin = ruinsHabitat.Ruin;
            if (ruin == null)
            {
                return;
            }
            string text = "";
            string text2 = string.Empty;
            if (investigatingEmpire == PlayerEmpire)
            {
                string text3 = CheckForStoryLocationHint();
                if (!string.IsNullOrEmpty(text3) && Rnd.Next(0, 2) == 1)
                {
                    text2 = "\n\n";
                    text2 = text2 + "*** " + TextResolver.GetText("A datacore recovered from the ruins NAVIGATIONAL DIRECTIONS") + ":";
                    text2 += text3;
                    text2 += ". ***\n\n";
                    text2 += TextResolver.GetText("We should send a ship to investigate this location.");
                }
            }
            if (CheckRuinsHaveBenefit(ruin, investigatingEmpire) || CheckRuinBonuses(ruin))
            {
                if (StoryDistantWorldsEnabled && ruin.StoryClueLevel >= 0 && investigatingEmpire == PlayerEmpire)
                {
                    text = text + string.Format(TextResolver.GetText("Ruins Discovery Historical Details"), ruin.Name) + " ";
                    text += GenerateStoryClue(ruinsHabitat);
                    investigatingEmpire.SendEventMessageToEmpire(EventMessageType.StoryClue, TextResolver.GetText("Galactic History revealed"), text, ruin, ruinsHabitat);
                    if (investigatingEmpire == PlayerEmpire)
                    {
                        ruin.StoryClueLevel = -1;
                    }
                }
                if (ruin.GameEventId >= 0)
                {
                    CheckTriggerEvent(ruin.GameEventId, investigatingEmpire, EventTriggerType.Investigate, null);
                }
                if (ruin.MoneyBonus > 0.0)
                {
                    investigatingEmpire.StateMoney += ruin.MoneyBonus;
                    investigatingEmpire.PirateEconomy.PerformIncome(ruin.MoneyBonus, PirateIncomeType.Undefined, CurrentStarDate);
                    text += string.Format(TextResolver.GetText("Ruins Discovery Money"), ruin.Name, ruin.MoneyBonus.ToString());
                    text += text2;
                    investigatingEmpire.SendEventMessageToEmpire(EventMessageType.GeneralRuinsDiscovery, TextResolver.GetText("Treasure Recovered"), text, ruin, ruinsHabitat);
                }
                if (ruin.ResearchBonus > 0 && investigatingEmpire.Research != null)
                {
                    ResearchNode researchNode = investigatingEmpire.Research.SelectRandomNextResearchProjectExcludeSuperWeapons(this);
                    if (researchNode != null)
                    {
                        researchNode.Progress += ruin.ResearchBonus;
                        if (researchNode.Progress >= researchNode.Cost)
                        {
                            text += string.Format(TextResolver.GetText("Ruins Discovery Research"), ruin.Name, researchNode.Name);
                            investigatingEmpire.DoResearchBreakthrough(researchNode, selfResearched: true, blockMessages: true, suppressUpdate: true);
                            investigatingEmpire.Research.Update(investigatingEmpire.DominantRace);
                            investigatingEmpire.ReviewDesignsBuiltObjectsImprovedComponents();
                            investigatingEmpire.ReviewResearchAbilities();
                        }
                        else
                        {
                            text += string.Format(TextResolver.GetText("Ruins Discovery Research"), ruin.Name, researchNode.Name);
                        }
                        text += text2;
                        investigatingEmpire.SendEventMessageToEmpire(EventMessageType.GeneralRuinsDiscovery, TextResolver.GetText("Technology Recovered"), text, ruin, ruinsHabitat);
                    }
                }
                if (ruin.MapSystemReveal > 0)
                {
                    int mapSystemReveal = ruin.MapSystemReveal;
                    ruin.MapSystemReveal = 0;
                    if (investigatingEmpire.SystemVisibility != null && investigatingEmpire.ResourceMap != null)
                    {
                        for (int i = 0; i < mapSystemReveal; i++)
                        {
                            Habitat habitat = FastFindNearestUnexploredHabitat(ruinsHabitat.Xpos, ruinsHabitat.Ypos, investigatingEmpire);
                            if (habitat == null)
                            {
                                break;
                            }
                            SystemInfo systemInfo = Systems[habitat.SystemIndex];
                            if (systemInfo == null || systemInfo.Habitats == null)
                            {
                                continue;
                            }
                            investigatingEmpire.SystemVisibility[habitat.SystemIndex].TotallyExplored = true;
                            if (investigatingEmpire.ResourceMap != null)
                            {
                                for (int j = 0; j < systemInfo.Habitats.Count; j++)
                                {
                                    Habitat habitat2 = systemInfo.Habitats[j];
                                    if (habitat2 != null)
                                    {
                                        investigatingEmpire.ResourceMap.SetResourcesKnown(habitat2, known: true);
                                    }
                                }
                                if (systemInfo.SystemStar != null)
                                {
                                    investigatingEmpire.ResourceMap.SetResourcesKnown(systemInfo.SystemStar, known: true);
                                }
                            }
                            SystemVisibilityStatus status = investigatingEmpire.SystemVisibility[habitat.SystemIndex].Status;
                            if (status == SystemVisibilityStatus.Unexplored || status == SystemVisibilityStatus.Undefined)
                            {
                                investigatingEmpire.SystemVisibility[habitat.SystemIndex].Status = SystemVisibilityStatus.Explored;
                            }
                        }
                        text += string.Format(TextResolver.GetText("Ruins Discovery Maps"), ruin.Name, mapSystemReveal.ToString());
                        text += text2;
                        investigatingEmpire.SendEventMessageToEmpire(EventMessageType.GeneralRuinsDiscovery, TextResolver.GetText("System Maps Recovered"), text, ruin, ruinsHabitat);
                    }
                }
                string empty = string.Empty;
                string empty2 = string.Empty;
                switch (ruin.Type)
                {
                    case RuinType.EmpireBonus:
                        {
                            empty = TextResolver.GetText("Empire Bonus when Colonized");
                            text = string.Format(TextResolver.GetText("Ruins Empire Bonus"), ruin.Name, ResolveDescription(ruinsHabitat.Category).ToLower(CultureInfo.InvariantCulture));
                            text += ":\n\n";
                            string text4 = string.Empty;
                            if (ruin.BonusDefensive > 0.0)
                            {
                                text += string.Format(TextResolver.GetText("Ruins Bonus Defensive"), ResolveDescription(ruinsHabitat.Category).ToLower(CultureInfo.InvariantCulture), ruin.BonusDefensive.ToString("#%"));
                            }
                            else
                            {
                                if (ruin.BonusDiplomacy > 0.0)
                                {
                                    text4 = string.Format(TextResolver.GetText("Ruins Bonus Diplomacy"), ruin.BonusDiplomacy.ToString("#%"));
                                }
                                else if (ruin.BonusHappiness > 0.0)
                                {
                                    text4 = string.Format(TextResolver.GetText("Ruins Bonus Happiness"), ruin.BonusHappiness.ToString("#%"));
                                }
                                else if (ruin.BonusResearchEnergy > 0.0)
                                {
                                    text4 = string.Format(TextResolver.GetText("Ruins Bonus Energy Research"), ruin.BonusResearchEnergy.ToString("#%"));
                                }
                                else if (ruin.BonusResearchHighTech > 0.0)
                                {
                                    text4 = string.Format(TextResolver.GetText("Ruins Bonus HighTech Research"), ruin.BonusResearchHighTech.ToString("#%"));
                                }
                                else if (ruin.BonusResearchWeapons > 0.0)
                                {
                                    text4 = string.Format(TextResolver.GetText("Ruins Bonus Weapons Research"), ruin.BonusResearchWeapons.ToString("#%"));
                                }
                                else if (ruin.BonusWealth > 0.0)
                                {
                                    text4 = string.Format(TextResolver.GetText("Ruins Bonus Colony Income"), ruin.BonusWealth.ToString("#%"));
                                }
                                text += text4;
                                text += "\n\n";
                            }
                            text += TextResolver.GetText("We should immediately send a colony ship to colonize this extremely valuable world");
                            investigatingEmpire.SendEventMessageToEmpire(EventMessageType.RuinsEmpireBonus, empty, text, ruin, ruinsHabitat);
                            break;
                        }
                    case RuinType.CreatureSwarmSilverMist:
                        {
                            empty = TextResolver.GetText("SilverMist Released");
                            text = string.Format(TextResolver.GetText("Ruins SilverMist"), ruin.Name);
                            Creature creature = GenerateCreatureAtHabitat(CreatureType.SilverMist, ruinsHabitat, lockLocation: false);
                            investigatingEmpire.SendEventMessageToEmpire(EventMessageType.CreatureOutbreak, empty, text, creature, ruinsHabitat);
                            investigatingEmpire.SendNewsBroadcast(EventMessageType.CreatureOutbreak, creature, DisasterEventType.Undefined, warStartEnd: false, wonderBegun: false, ruinsHabitat);
                            break;
                        }
                    case RuinType.CreatureSwarm:
                        {
                            empty = TextResolver.GetText("Kaltor Swarm Released");
                            text = string.Format(TextResolver.GetText("Ruins Kaltor Swarm"), ruin.Name);
                            int num4 = Rnd.Next(3, 6);
                            for (int l = 0; l < num4; l++)
                            {
                                GenerateCreatureAtHabitat(CreatureType.Kaltor, ruinsHabitat, lockLocation: false);
                            }
                            investigatingEmpire.SendEventMessageToEmpire(EventMessageType.CreatureOutbreak, empty, text, ruin, ruinsHabitat);
                            break;
                        }
                    case RuinType.PirateAmbush:
                        {
                            empty = TextResolver.GetText("Pirate Ambush") + "!";
                            Empire empire2 = FindNearestPirateFaction(ruinsHabitat.Xpos, ruinsHabitat.Ypos, PlayerEmpire, includeSuperPirates: false);
                            if (empire2 != null)
                            {
                                text = string.Format(TextResolver.GetText("Ruins Pirate Ambush"), ruin.Name, empire2.Name);
                                int num5 = Rnd.Next(3, 5);
                                Habitat habitat10 = ruinsHabitat;
                                double num6 = ruinsHabitat.Xpos;
                                double num7 = ruinsHabitat.Ypos;
                                int num8 = 0;
                                Habitat habitat11 = DetermineHabitatSystemStar(ruinsHabitat);
                                while (habitat10 == habitat11 && num8 < 20)
                                {
                                    num6 += Rnd.NextDouble() * 200000.0 - 100000.0;
                                    num7 += Rnd.NextDouble() * 200000.0 - 100000.0;
                                    habitat10 = FindNearestSystemGasCloudAsteroid(num6, num7);
                                    num8++;
                                }
                                for (int m = 0; m < num5; m++)
                                {
                                    GeneratePirateShip(empire2, BuiltObjectSubRole.Frigate, habitat10)?.AssignMission(BuiltObjectMissionType.Move, ruinsHabitat, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                }
                                investigatingEmpire.SendEventMessageToEmpire(EventMessageType.PirateAmbush, empty, text, ruin, ruinsHabitat);
                            }
                            break;
                        }
                    case RuinType.UnlockResearchProject:
                        if (investigatingEmpire.Research != null && investigatingEmpire.Research.TechTree != null)
                        {
                            ResearchNode researchNode3 = investigatingEmpire.Research.TechTree.FindNodeById(ruin.ResearchProjectId);
                            if (researchNode3 != null && !researchNode3.IsEnabled)
                            {
                                empty = TextResolver.GetText("Ancient Knowledge Cache Discovered");
                                string name = researchNode3.Name;
                                text += string.Format(TextResolver.GetText("Ruins Ancient Knowledge Cache Discovered"), ruin.Name, name);
                                text += ".\n\n";
                                researchNode3.IsEnabled = true;
                                investigatingEmpire.SendEventMessageToEmpire(EventMessageType.ExoticTechDiscovered, empty, text, ruin, ruinsHabitat);
                            }
                            else
                            {
                                text = string.Format(TextResolver.GetText("Our survey team found nothing of interest in the RUINNAME"), ruin.Name);
                                investigatingEmpire.SendEventMessageToEmpire(EventMessageType.GeneralRuinsDiscovery, TextResolver.GetText("Ruins are Silent"), text, ruin, ruinsHabitat);
                            }
                        }
                        break;
                    case RuinType.Component:
                        {
                            if (investigatingEmpire.Research == null || investigatingEmpire.Research.TechTree == null)
                            {
                                break;
                            }
                            ResearchNode researchNode2 = investigatingEmpire.Research.TechTree.FindNodeById(ruin.ResearchProjectId);
                            empty = TextResolver.GetText("Secret Super Weapon Discovered");
                            string arg = string.Empty;
                            if (researchNode2 != null)
                            {
                                if (researchNode2.Components != null && researchNode2.Components.Count > 0)
                                {
                                    arg = researchNode2.Components[0].Name;
                                }
                                else if (researchNode2.ComponentImprovements != null && researchNode2.ComponentImprovements.Count > 0)
                                {
                                    if (researchNode2.ComponentImprovements[0].ImprovedComponent != null)
                                    {
                                        arg = researchNode2.ComponentImprovements[0].ImprovedComponent.Name;
                                    }
                                }
                                else
                                {
                                    arg = researchNode2.Name;
                                }
                            }
                            text += string.Format(TextResolver.GetText("Ruins Secret Super Weapon Discovered"), ruin.Name, arg);
                            text += ".\n\n";
                            investigatingEmpire.DoResearchBreakthrough(researchNode2, selfResearched: true, blockMessages: true, suppressUpdate: true);
                            investigatingEmpire.Research.Update(investigatingEmpire.DominantRace);
                            investigatingEmpire.ReviewDesignsBuiltObjectsImprovedComponents();
                            investigatingEmpire.ReviewResearchAbilities();
                            investigatingEmpire.SendEventMessageToEmpire(EventMessageType.ExoticTechDiscovered, empty, text, ruin, ruinsHabitat);
                            break;
                        }
                    case RuinType.Government:
                        {
                            if (investigatingEmpire.AllowableGovernmentTypes == null)
                            {
                                break;
                            }
                            empty = TextResolver.GetText("Secret Form of Government Revealed");
                            if (!investigatingEmpire.AllowableGovernmentTypes.Contains(ruin.SpecialGovernmentId))
                            {
                                investigatingEmpire.AllowableGovernmentTypes.Add(ruin.SpecialGovernmentId);
                            }
                            GovernmentAttributes governmentAttributes = Governments[ruin.SpecialGovernmentId];
                            text += string.Format(TextResolver.GetText("Ruins Secret Form of Government Revealed"), ruin.Name, governmentAttributes.Name);
                            string text6 = string.Empty;
                            switch (governmentAttributes.Availability)
                            {
                                case 3:
                                    text6 = TextResolver.GetText("Government Description Way of Darkness");
                                    text6 += "\n\n";
                                    break;
                                case 2:
                                    text6 = TextResolver.GetText("Government Description Way of the Ancients");
                                    text6 += "\n\n";
                                    break;
                            }
                            text += text6;
                            text += TextResolver.GetText("Ruins Secret Form of Government Revealed Adoption");
                            investigatingEmpire.SendEventMessageToEmpire(EventMessageType.SpecialGovernmentType, empty, text, ruin, ruinsHabitat);
                            if (investigatingEmpire == PlayerEmpire)
                            {
                                break;
                            }
                            GovernmentAttributesList governmentAttributesList = Empire.DetermineMostSuitableGovermentTypes(investigatingEmpire.DominantRace, investigatingEmpire.AllowableGovernmentTypes);
                            if (governmentAttributesList != null && governmentAttributesList.Count > 0)
                            {
                                int governmentId = governmentAttributesList[0].GovernmentId;
                                if (governmentId == ruin.SpecialGovernmentId)
                                {
                                    investigatingEmpire.HaveRevolution(investigatingEmpire.DominantRace, governmentId);
                                }
                            }
                            break;
                        }
                    case RuinType.LostBuiltObject:
                        {
                            DesignSpecification designSpecification4 = null;
                            BuiltObjectSubRole subRole = BuiltObjectSubRole.Undefined;
                            switch (Rnd.Next(0, 2))
                            {
                                case 0:
                                    subRole = BuiltObjectSubRole.Cruiser;
                                    break;
                                case 1:
                                    subRole = BuiltObjectSubRole.CapitalShip;
                                    break;
                            }
                            designSpecification4 = investigatingEmpire.ObtainDesignSpec(subRole);
                            if (designSpecification4 == null)
                            {
                                break;
                            }
                            Habitat habitat6 = FindLonelyColonyLocation(investigatingEmpire);
                            if (habitat6 == null)
                            {
                                break;
                            }
                            Empire empire = SelectRandomEmpire();
                            Design design4 = empire.GenerateDesignFromSpec(designSpecification4, 4.0);
                            if (design4 == null)
                            {
                                break;
                            }
                            design4.PictureRef = ShipImageHelper.ResolveMinorShipImageIndex(design4.SubRole, largeShips: true);
                            BuiltObject builtObject4 = GenerateAbandonedBuiltObject(habitat6, design4);
                            if (builtObject4 != null)
                            {
                                empty = TextResolver.GetText("Lost Ship Location Revealed");
                                Habitat habitat7 = DetermineHabitatSystemStar(habitat6);
                                empty2 = ResolveSectorDescription(habitat6.Xpos, habitat6.Ypos);
                                text += string.Format(TextResolver.GetText("Ruins Lost Ship Location"), ruin.Name, builtObject4.Name, ResolveDescription(habitat6.Category).ToLower(CultureInfo.InvariantCulture), habitat6.Name, habitat7.Name, empty2);
                                investigatingEmpire.SendEventMessageToEmpire(EventMessageType.LostBuiltObjectCoordinates, empty, text, ruin, ruinsHabitat);
                                if (investigatingEmpire == PlayerEmpire)
                                {
                                    PlayerEmpire.AddLocationHint(new Point((int)habitat6.Xpos, (int)habitat6.Ypos));
                                }
                            }
                            break;
                        }
                    case RuinType.LostColony:
                        {
                            empty = TextResolver.GetText("Lost Colony Location Revealed");
                            Habitat habitat8 = FindLonelyColonyLocation(investigatingEmpire);
                            if (habitat8 != null)
                            {
                                if (habitat8.Quality < 0.65f)
                                {
                                    habitat8.BaseQuality = (float)(0.65 + Rnd.NextDouble() * 0.35);
                                }
                                Race race2 = investigatingEmpire.DominantRace;
                                if (race2 == null || !race2.Playable)
                                {
                                    race2 = SelectRandomRace(75);
                                }
                                investigatingEmpire.MakeHabitatIntoColony(habitat8, null, race2, 2000000000L);
                                Habitat habitat9 = DetermineHabitatSystemStar(habitat8);
                                empty2 = ResolveSectorDescription(habitat8.Xpos, habitat8.Ypos);
                                text += string.Format(TextResolver.GetText("Ruins Lost Colony Location Revealed"), ruin.Name, ResolveDescription(habitat8.Category).ToLower(CultureInfo.InvariantCulture), habitat8.Name, habitat9.Name, empty2);
                                investigatingEmpire.SendEventMessageToEmpire(EventMessageType.LostColonyCoordinates, empty, text, habitat8, ruinsHabitat);
                                if (investigatingEmpire == PlayerEmpire)
                                {
                                    PlayerEmpire.AddLocationHint(new Point((int)habitat8.Xpos, (int)habitat8.Ypos));
                                }
                            }
                            break;
                        }
                    case RuinType.NewPopulation:
                        if (ruin.HabitatNewRace != null)
                        {
                            empty = TextResolver.GetText("Sleeping Alien Race Awoken");
                            Population population = new Population(ruin.HabitatNewRace, 200000000L);
                            if (ruinsHabitat.Population == null)
                            {
                                ruinsHabitat.Population = new PopulationList();
                            }
                            ruinsHabitat.Population.Add(population);
                            IndependentEmpire.TakeOwnershipOfColony(ruinsHabitat, IndependentEmpire);
                            text += string.Format(TextResolver.GetText("Ruins Sleeping Alien Race Awoken"), ruin.Name, ruin.HabitatNewRace.Name, ResolveDescription(ruinsHabitat.Category));
                            investigatingEmpire.SendEventMessageToEmpire(EventMessageType.SleepersAwake, empty, text, ruin.HabitatNewRace, ruinsHabitat);
                        }
                        break;
                    case RuinType.Origins:
                        {
                            string empty3 = string.Empty;
                            if (ruin.OriginsRace == null)
                            {
                                break;
                            }
                            empty3 = ((ruin.OriginsApprovalRatingBonus < 0) ? (empty3 + string.Format(TextResolver.GetText("Ruins Origins Negative"), ruin.OriginsRace.Name)) : (empty3 + string.Format(TextResolver.GetText("Ruins Origins Positive"), ruin.OriginsRace.Name)));
                            switch (ruin.OriginsRace.Name)
                            {
                                case "Human":
                                    empty3 = TextResolver.GetText("Ruins Origins Human");
                                    break;
                                case "Boskara":
                                    empty3 = string.Format(TextResolver.GetText("Ruins Origins Negative"), ruin.OriginsRace.Name);
                                    break;
                                case "Kiadian":
                                    empty3 = TextResolver.GetText("Ruins Origins Kiadian");
                                    break;
                                case "Sluken":
                                    empty3 = string.Format(TextResolver.GetText("Ruins Origins Negative"), ruin.OriginsRace.Name);
                                    break;
                                case "Ackdarian":
                                    empty3 = TextResolver.GetText("Ruins Origins Ackdarian");
                                    break;
                                case "Gizurean":
                                    empty3 = string.Format(TextResolver.GetText("Ruins Origins Negative"), ruin.OriginsRace.Name);
                                    break;
                            }
                            ruin.OriginsRace.SatisfactionModifier += ruin.OriginsApprovalRatingBonus;
                            text += string.Format(TextResolver.GetText("Ruins Origins"), ruin.Name, ruin.OriginsRace.Name);
                            text += "\n\n";
                            text += empty3;
                            text = ((ruin.OriginsApprovalRatingBonus < 0) ? (text + "\n\n" + string.Format(TextResolver.GetText("Ruins Origins Negative Effect"), ruin.OriginsRace.Name)) : (text + "\n\n" + string.Format(TextResolver.GetText("Ruins Origins Positive Effect"), ruin.OriginsRace.Name)));
                            empty = string.Format(TextResolver.GetText("History of the RACE"), ruin.OriginsRace.Name);
                            investigatingEmpire.SendEventMessageToEmpire(EventMessageType.OriginsDiscovery, empty, text, ruin.OriginsRace, ruinsHabitat);
                            for (int n = 0; n < Empires.Count; n++)
                            {
                                Empire empire3 = Empires[n];
                                if (empire3 != null && empire3 != investigatingEmpire && empire3.DominantRace != null && empire3.DominantRace == ruin.OriginsRace)
                                {
                                    string text5 = string.Format(TextResolver.GetText("Ruins Origins Discovery Other"), ruin.OriginsRace.Name);
                                    text5 += "\n\n";
                                    text5 += empty3;
                                    text5 = ((ruin.OriginsApprovalRatingBonus < 0) ? (text5 + "\n\n" + string.Format(TextResolver.GetText("Ruins Origins Negative Effect"), ruin.OriginsRace.Name)) : (text5 + "\n\n" + string.Format(TextResolver.GetText("Ruins Origins Positive Effect"), ruin.OriginsRace.Name)));
                                    empire3.SendEventMessageToEmpire(EventMessageType.OriginsDiscovery, empty, text5, ruin.OriginsRace, ruinsHabitat);
                                }
                            }
                            break;
                        }
                    case RuinType.Refugees:
                        {
                            Habitat habitat4 = null;
                            if (Systems[ruinsHabitat.SystemIndex].Habitats != null)
                            {
                                for (int k = 0; k < Systems[ruinsHabitat.SystemIndex].Habitats.Count; k++)
                                {
                                    Habitat habitat5 = Systems[ruinsHabitat.SystemIndex].Habitats[k];
                                    if (habitat5 != null)
                                    {
                                        double num3 = CalculateDistance(ruinsHabitat.Xpos, ruinsHabitat.Ypos, habitat5.Xpos, habitat5.Ypos);
                                        if (num3 > 400.0)
                                        {
                                            habitat4 = habitat5;
                                            break;
                                        }
                                    }
                                }
                            }
                            bool flag = true;
                            if (ruin.RefugeesGenerated || habitat4 == null)
                            {
                                flag = false;
                            }
                            if (flag)
                            {
                                DesignSpecification designSpecification = investigatingEmpire.ObtainDesignSpec(BuiltObjectSubRole.ColonyShip);
                                DesignSpecification designSpecification2 = investigatingEmpire.ObtainDesignSpec(BuiltObjectSubRole.Frigate);
                                DesignSpecification designSpecification3 = investigatingEmpire.ObtainDesignSpec(BuiltObjectSubRole.Cruiser);
                                if (designSpecification == null || designSpecification2 == null || designSpecification3 == null)
                                {
                                    break;
                                }
                                Design design = investigatingEmpire.GenerateDesignFromSpec(designSpecification, 3.0);
                                Design design2 = investigatingEmpire.GenerateDesignFromSpec(designSpecification2, 3.0);
                                Design design3 = investigatingEmpire.GenerateDesignFromSpec(designSpecification3, 3.0);
                                Race race = SelectRandomRace(75);
                                if (design != null && design2 != null && design3 != null)
                                {
                                    design.PictureRef = ShipImageHelper.ResolveNewShipImageIndex(BuiltObjectSubRole.ColonyShip, race, isPirates: false);
                                    design2.PictureRef = ShipImageHelper.ResolveNewShipImageIndex(BuiltObjectSubRole.Frigate, race, isPirates: false);
                                    design3.PictureRef = ShipImageHelper.ResolveNewShipImageIndex(BuiltObjectSubRole.Cruiser, race, isPirates: false);
                                    BuiltObject builtObject = GenerateAbandonedBuiltObject(habitat4, design, allowCreatures: false, allowNegativeEffects: false, BuiltObjectEncounterAction.Notify);
                                    if (builtObject != null)
                                    {
                                        builtObject.Name = string.Format(TextResolver.GetText("Refugee SHIPTYPE"), ResolveDescription(BuiltObjectSubRole.ColonyShip));
                                        builtObject.NativeRace = race;
                                    }
                                    BuiltObject builtObject2 = GenerateAbandonedBuiltObject(habitat4, design2, allowCreatures: false, allowNegativeEffects: false, BuiltObjectEncounterAction.Notify);
                                    if (builtObject2 != null)
                                    {
                                        builtObject2.Name = string.Format(TextResolver.GetText("Refugee SHIPTYPE"), ResolveDescription(BuiltObjectSubRole.Frigate));
                                    }
                                    BuiltObject builtObject3 = GenerateAbandonedBuiltObject(habitat4, design3, allowCreatures: false, allowNegativeEffects: false, BuiltObjectEncounterAction.Notify);
                                    if (builtObject3 != null)
                                    {
                                        builtObject3.Name = string.Format(TextResolver.GetText("Refugee SHIPTYPE"), ResolveDescription(BuiltObjectSubRole.Cruiser));
                                    }
                                    text += string.Format(TextResolver.GetText("Ruins Refugees"), ruin.Name, race.Name, ResolveDescription(habitat4.Category).ToLower(CultureInfo.InvariantCulture), habitat4.Name);
                                    empty = TextResolver.GetText("Galactic Refugees Encountered");
                                    investigatingEmpire.SendEventMessageToEmpire(EventMessageType.GalacticRefugees, empty, text, ruin, ruinsHabitat);
                                }
                            }
                            else
                            {
                                text = text + " " + TextResolver.GetText("Our survey team found nothing of interest in the ruins.");
                                investigatingEmpire.SendMessageToEmpire(investigatingEmpire, EmpireMessageType.ExplorationRuins, this, text);
                            }
                            break;
                        }
                    case RuinType.StoryEvent:
                        lock (StoryLock)
                        {
                            if (ruin.StoryEventData > 0)
                            {
                                ruin.StoryEventData = 0;
                                empty = TextResolver.GetText("Strange transmission from beyond our galaxy");
                                text += string.Format(TextResolver.GetText("Shakturi Beacon Trigger"), ruin.Name);
                                investigatingEmpire.SendEventMessageToEmpire(EventMessageType.GeneralRuinsDiscovery, empty, text, ruin, ruinsHabitat);
                                Habitat habitat3 = null;
                                if (ShakturiTriggerHabitat != null)
                                {
                                    double num = Rnd.NextDouble() * 3000000.0 - 1500000.0;
                                    double num2 = Rnd.NextDouble() * 3000000.0 - 1500000.0;
                                    if (Math.Abs(num) < 500000.0)
                                    {
                                        num = 600000.0 * (double)Math.Sign(num);
                                    }
                                    if (Math.Abs(num2) < 500000.0)
                                    {
                                        num2 = 600000.0 * (double)Math.Sign(num2);
                                    }
                                    habitat3 = FindLonelyHabitat(ShakturiTriggerHabitat.Xpos + num, ShakturiTriggerHabitat.Ypos + num2, HabitatType.BarrenRock);
                                }
                                if (habitat3 == null)
                                {
                                    habitat3 = FindLonelyHabitatGalacticEdge(RuinType.Undefined, HabitatType.Ice);
                                }
                                GenerateShakturi(habitat3);
                            }
                            else
                            {
                                text = text + " " + TextResolver.GetText("Our survey team found nothing of interest in the ruins.");
                                investigatingEmpire.SendMessageToEmpire(investigatingEmpire, EmpireMessageType.ExplorationRuins, this, text);
                            }
                        }
                        break;
                }
                ruin.ClearBonuses();
            }
            else if (investigatingEmpire.DominantRace != null && investigatingEmpire.DominantRace.RaceEvents != null && investigatingEmpire.DominantRace.RaceEvents.ContainsEventType(RaceEventType.HistoricalDiscoveryExploreRuinsForResearchBoost) && investigatingEmpire.RaceEventType == RaceEventType.Undefined && Rnd.Next(0, 2) == 1)
            {
                investigatingEmpire.RaceEventType = RaceEventType.HistoricalDiscoveryExploreRuinsForResearchBoost;
                investigatingEmpire.RaceEventEndDate = CurrentStarDate + RealSecondsInGalacticYear * 1000 / 2;
                text = string.Format(TextResolver.GetText("Our survey team made a discovery of galactic significance in the RUINNAME"), ruin.Name);
                if (string.IsNullOrEmpty(text2))
                {
                    text += text2;
                }
                investigatingEmpire.SendEventMessageToEmpire(EventMessageType.GeneralRuinsDiscovery, TextResolver.GetText("Unusual Technology Discovered"), text, ruin, ruinsHabitat);
            }
            else if (string.IsNullOrEmpty(text2))
            {
                text = string.Format(TextResolver.GetText("Our survey team found nothing of interest in the RUINNAME"), ruin.Name);
                investigatingEmpire.SendEventMessageToEmpire(EventMessageType.GeneralRuinsDiscovery, TextResolver.GetText("Ruins are Silent"), text, ruin, ruinsHabitat);
            }
            else
            {
                text = text2;
                investigatingEmpire.SendEventMessageToEmpire(EventMessageType.GeneralRuinsDiscovery, TextResolver.GetText("Navigational Coordinates"), text, ruin, ruinsHabitat);
            }
        }

        public Point FindNearestGalaxyEdgeCoordsMinimumRange(double x, double y, double minimumRange, double minimumRangeToColony)
        {
            Point result = FindNearestGalaxyEdgeCoords(x, y);
            double num = CalculateDistance(x, y, result.X, result.Y);
            Habitat habitat = FindNearestColony(result.X, result.Y, null, 0, includeIndependentColonies: false);
            double num2 = double.MaxValue;
            if (habitat != null)
            {
                num2 = CalculateDistance(result.X, result.Y, habitat.Xpos, habitat.Ypos);
            }
            int num3 = 0;
            while ((num < minimumRange || num2 < minimumRangeToColony) && num3 < 200)
            {
                double num4 = DetermineAngle(x, y, SizeX / 2, SizeY / 2);
                double num5 = CalculateDistance(x, y, SizeX / 2, SizeY / 2);
                double num6 = num4 - Math.PI;
                _ = SizeX / 2;
                num6 += Rnd.NextDouble() * 3.0 - 1.5;
                double num7 = Math.Cos(num6) * num5;
                double num8 = Math.Sin(num6) * num5;
                double x2 = (double)(SizeX / 2) + num7;
                double y2 = (double)(SizeY / 2) + num8;
                result = FindNearestGalaxyEdgeCoords(x2, y2);
                num = CalculateDistance(x, y, result.X, result.Y);
                habitat = FindNearestColony(result.X, result.Y, null, 0, includeIndependentColonies: false);
                num2 = ((habitat == null) ? double.MaxValue : CalculateDistance(result.X, result.Y, habitat.Xpos, habitat.Ypos));
                num3++;
            }
            return result;
        }

        public Point FindNearestGalaxyEdgeCoords(double x, double y)
        {
            double num = DetermineAngle(x, y, SizeX / 2, SizeY / 2);
            double num2 = CalculateDistance(x, y, SizeX / 2, SizeY / 2);
            double num3 = (double)(SizeX / 2) - num2;
            double num4 = num - Math.PI;
            double num5 = x + Math.Cos(num4) * num3;
            double num6 = y + Math.Sin(num4) * num3;
            int val = (int)num5;
            int val2 = (int)num6;
            val = Math.Min(SizeX - 1, Math.Max(0, val));
            val2 = Math.Min(SizeY - 1, Math.Max(0, val2));
            return new Point(val, val2);
        }

        public Empire SelectRandomEmpire()
        {
            Empire empire = null;
            int num = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(empire == null, 200, ref iterationCount))
            {
                num = Rnd.Next(0, Empires.Count);
                empire = Empires[num];
                if (empire.PirateEmpireBaseHabitat != null || empire == IndependentEmpire || !empire.Active)
                {
                    empire = null;
                }
            }
            return empire;
        }

        public Habitat FindNearestUnknownIndependentColony(double x, double y, Empire empire)
        {
            Habitat result = null;
            double num = double.MaxValue;
            for (int i = 0; i < IndependentColonies.Count; i++)
            {
                Habitat habitat = IndependentColonies[i];
                if (habitat != null && habitat.Empire == IndependentEmpire && !empire.CheckSystemExplored(habitat.SystemIndex))
                {
                    double num2 = CalculateDistanceSquared(x, y, habitat.Xpos, habitat.Ypos);
                    if (num2 < num)
                    {
                        num = num2;
                        result = habitat;
                    }
                }
            }
            return result;
        }

        public Habitat FindNearestRuin(double x, double y, RuinType ruinType)
        {
            Habitat result = null;
            double num = double.MaxValue;
            for (int i = 0; i < RuinsHabitats.Count; i++)
            {
                Habitat habitat = RuinsHabitats[i];
                if (habitat.Ruin != null && habitat.Ruin.Type == ruinType)
                {
                    double num2 = CalculateDistanceSquared(x, y, habitat.Xpos, habitat.Ypos);
                    if (num2 < num)
                    {
                        num = num2;
                        result = habitat;
                    }
                }
            }
            return result;
        }

        public Habitat FindNearestRuin(double x, double y)
        {
            Habitat result = null;
            double num = double.MaxValue;
            for (int i = 0; i < RuinsHabitats.Count; i++)
            {
                Habitat habitat = RuinsHabitats[i];
                if (habitat.Ruin != null)
                {
                    double num2 = CalculateDistanceSquared(x, y, habitat.Xpos, habitat.Ypos);
                    if (num2 < num)
                    {
                        num = num2;
                        result = habitat;
                    }
                }
            }
            return result;
        }

        public Habitat FindNearestUnknownRuin(double x, double y, Empire empire)
        {
            Habitat result = null;
            double num = double.MaxValue;
            for (int i = 0; i < RuinsHabitats.Count; i++)
            {
                Habitat habitat = RuinsHabitats[i];
                if (habitat != null && habitat.Ruin != null && (habitat.Empire == null || habitat.Empire == IndependentEmpire) && !empire.CheckSystemExplored(habitat.SystemIndex))
                {
                    double num2 = CalculateDistanceSquared(x, y, habitat.Xpos, habitat.Ypos);
                    if (num2 < num)
                    {
                        num = num2;
                        result = habitat;
                    }
                }
            }
            return result;
        }

        public BuiltObject FindUnownedBuiltObjectInSystem(Habitat systemStar)
        {
            for (int i = 0; i < AbandonedBuiltObjects.Count; i++)
            {
                BuiltObject builtObject = AbandonedBuiltObjects[i];
                if (builtObject != null && builtObject.NearestSystemStar == systemStar && builtObject.UnbuiltOrDamagedComponentCount <= 0 && builtObject.Empire == null && !builtObject.HasBeenDestroyed)
                {
                    return builtObject;
                }
            }
            return null;
        }

        public BuiltObject FindNearestUnownedBuiltObject(double x, double y)
        {
            BuiltObject result = null;
            double num = double.MaxValue;
            for (int i = 0; i < AbandonedBuiltObjects.Count; i++)
            {
                BuiltObject builtObject = AbandonedBuiltObjects[i];
                if (builtObject != null && builtObject.UnbuiltOrDamagedComponentCount <= 0)
                {
                    double num2 = CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                    if (num2 < num)
                    {
                        result = builtObject;
                        num = num2;
                    }
                }
            }
            return result;
        }

        public string GenerateLocationDescription(double x, double y)
        {
            return GenerateLocationDescription(x, y, prefixWithA: false);
        }

        public string GenerateLocationDescription(double x, double y, bool prefixWithA)
        {
            string empty = string.Empty;
            Habitat habitat = FindNearestHabitat(x, y);
            double num = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            if (num < 500.0)
            {
                return GenerateLocationDescription(habitat);
            }
            string text = string.Empty;
            GalaxyLocationList galaxyLocationList = DetermineGalaxyLocationsAtPoint(x, y);
            if (galaxyLocationList != null && galaxyLocationList.Count > 0)
            {
                for (int i = 0; i < galaxyLocationList.Count; i++)
                {
                    if ((galaxyLocationList[i].Type == GalaxyLocationType.NebulaCloud || galaxyLocationList[i].Type == GalaxyLocationType.RestrictedArea || galaxyLocationList[i].Type == GalaxyLocationType.SuperNova) && !string.IsNullOrEmpty(galaxyLocationList[i].Name))
                    {
                        text = galaxyLocationList[i].Name;
                        break;
                    }
                }
            }
            string text2 = string.Empty;
            Habitat habitat2 = DetermineHabitatSystemStar(habitat);
            num = CalculateDistance(x, y, habitat2.Xpos, habitat2.Ypos);
            if (num > (double)MaxSolarSystemSize * 2.1)
            {
                text2 = habitat2.Name;
            }
            if (!string.IsNullOrEmpty(text))
            {
                if (prefixWithA)
                {
                    return string.Format(TextResolver.GetText("A Location Description Nebula"), text, text2, ResolveSectorDescription(habitat2.Xpos, habitat2.Ypos));
                }
                return string.Format(TextResolver.GetText("Location Description Nebula"), text, text2, ResolveSectorDescription(habitat2.Xpos, habitat2.Ypos));
            }
            if (prefixWithA)
            {
                return string.Format(TextResolver.GetText("A Location Description"), text2, ResolveSectorDescription(habitat2.Xpos, habitat2.Ypos));
            }
            return string.Format(TextResolver.GetText("Location Description"), text2, ResolveSectorDescription(habitat2.Xpos, habitat2.Ypos));
        }

        public string GenerateLocationDescription(Habitat habitat)
        {
            Habitat habitat2 = DetermineHabitatSystemStar(habitat);
            return string.Format(TextResolver.GetText("Location Planet"), ResolveDescription(habitat.Type).ToLower(CultureInfo.InvariantCulture), ResolveDescription(habitat.Category).ToLower(CultureInfo.InvariantCulture), habitat.Name, habitat2.Name, ResolveSectorDescription(habitat.Xpos, habitat.Ypos));
        }

        public string GenerateNavigationalBonusMessage(double x, double y, Empire empire)
        {
            string text = string.Empty;
            switch (Rnd.Next(0, 11))
            {
                case 0:
                case 1:
                case 2:
                    {
                        Habitat habitat = FindNearestUnknownRuin(x, y, empire);
                        if (habitat != null)
                        {
                            text += GenerateLocationDescription(habitat);
                            if (empire == PlayerEmpire)
                            {
                                PlayerEmpire.AddLocationHint(new Point((int)habitat.Xpos, (int)habitat.Ypos));
                            }
                        }
                        break;
                    }
                case 3:
                case 4:
                    {
                        BuiltObject builtObject = FindNearestUnownedBuiltObject(x, y);
                        if (builtObject != null)
                        {
                            text += GenerateLocationDescription(builtObject.Xpos, builtObject.Ypos);
                            if (empire == PlayerEmpire)
                            {
                                PlayerEmpire.AddLocationHint(new Point((int)builtObject.Xpos, (int)builtObject.Ypos));
                            }
                        }
                        break;
                    }
                case 5:
                case 6:
                    {
                        Habitat habitat2 = FindNearestUnknownIndependentColony(x, y, empire);
                        if (habitat2 != null)
                        {
                            text += GenerateLocationDescription(habitat2);
                            if (empire == PlayerEmpire)
                            {
                                PlayerEmpire.AddLocationHint(new Point((int)habitat2.Xpos, (int)habitat2.Ypos));
                            }
                        }
                        break;
                    }
                case 7:
                case 8:
                    {
                        Empire empire2 = FindNearestPirateFactionBaseUnknown(empire, x, y, null);
                        if (empire2 != null && empire2.PirateEmpireBaseHabitat != null)
                        {
                            text += GenerateLocationDescription(empire2.PirateEmpireBaseHabitat);
                            if (empire == PlayerEmpire)
                            {
                                PlayerEmpire.AddLocationHint(new Point((int)empire2.PirateEmpireBaseHabitat.Xpos, (int)empire2.PirateEmpireBaseHabitat.Ypos));
                            }
                        }
                        break;
                    }
                case 9:
                case 10:
                    {
                        GalaxyLocationList galaxyLocationList = new GalaxyLocationList();
                        for (int i = 0; i < GalaxyLocations.Count; i++)
                        {
                            GalaxyLocation galaxyLocation = GalaxyLocations[i];
                            if (galaxyLocation != null && (galaxyLocation.Type == GalaxyLocationType.DebrisField || galaxyLocation.Type == GalaxyLocationType.PlanetDestroyer || (galaxyLocation.Type == GalaxyLocationType.RestrictedArea && galaxyLocation.Name != string.Format(TextResolver.GetText("NAME Weapons Testing Range"), "Pozdac") && galaxyLocation.Name != TextResolver.GetText("Dead Zone"))))
                            {
                                galaxyLocationList.Add(galaxyLocation);
                            }
                        }
                        {
                            foreach (GalaxyLocation item in galaxyLocationList)
                            {
                                if (!empire.KnownGalaxyLocations.Contains(item))
                                {
                                    text += GenerateLocationDescription(item.Xpos, item.Ypos);
                                    if (empire == PlayerEmpire)
                                    {
                                        Point location = new Point((int)((double)item.Xpos + (double)item.Width / 2.0), (int)((double)item.Ypos + (double)item.Height / 2.0));
                                        PlayerEmpire.AddLocationHint(location);
                                        return text;
                                    }
                                    return text;
                                }
                            }
                            return text;
                        }
                    }
            }
            return text;
        }

        public void DoRaidBonuses(Empire attackingEmpire, StellarObject target, double lootFactor)
        {
            if (target == null || target.Empire == null || attackingEmpire == null)
            {
                return;
            }
            double num = 1.0;
            if (target.RaidCountdown > 0)
            {
                num = (double)(int)target.RaidCountdown / 60.0 * 0.75;
                if (target.RaidCountdown > 55)
                {
                    num = 0.0;
                }
            }
            if (num == 0.0)
            {
                string description = string.Format(TextResolver.GetText("We have raided TARGET of the EMPIRE but failed to obtain any loot"), target.Name, target.Empire.Name);
                attackingEmpire.SendMessageToEmpire(attackingEmpire, EmpireMessageType.RaidBonuses, target, description);
                string empty = string.Empty;
                empty = ((!(target is Habitat)) ? string.Format(TextResolver.GetText("The EMPIRE have raided our base TARGET but failed to obtain any loot"), target.Name, attackingEmpire.Name) : string.Format(TextResolver.GetText("The EMPIRE have raided our colony TARGET but failed to obtain any loot"), target.Name, attackingEmpire.Name));
                target.Empire.SendMessageToEmpire(target.Empire, EmpireMessageType.RaidVictim, target, empty);
                return;
            }
            CargoList cargoList = null;
            Empire empire = null;
            int num2 = 0;
            int num3 = 0;
            IndustryType industryType = IndustryType.Undefined;
            if (target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)target;
                cargoList = builtObject.Cargo;
                empire = builtObject.Empire;
                num3 = builtObject.Size;
                switch (builtObject.SubRole)
                {
                    case BuiltObjectSubRole.EnergyResearchStation:
                    case BuiltObjectSubRole.WeaponsResearchStation:
                    case BuiltObjectSubRole.HighTechResearchStation:
                        num2 = 1;
                        industryType = builtObject.SubRole switch
                        {
                            BuiltObjectSubRole.EnergyResearchStation => IndustryType.Energy,
                            BuiltObjectSubRole.HighTechResearchStation => IndustryType.HighTech,
                            BuiltObjectSubRole.WeaponsResearchStation => IndustryType.Weapon,
                            _ => IndustryType.Undefined,
                        };
                        break;
                    case BuiltObjectSubRole.SmallSpacePort:
                    case BuiltObjectSubRole.MediumSpacePort:
                    case BuiltObjectSubRole.LargeSpacePort:
                        num2 = ((Rnd.Next(0, 3) != 1) ? ((builtObject.ResearchEnergy > 0 || builtObject.ResearchHighTech > 0 || builtObject.ResearchWeapons > 0) ? 1 : 2) : 0);
                        break;
                    case BuiltObjectSubRole.GasMiningStation:
                    case BuiltObjectSubRole.MiningStation:
                        num2 = ((Rnd.Next(0, 2) != 1) ? 2 : 0);
                        break;
                    default:
                        num2 = 0;
                        break;
                }
            }
            else if (target is Habitat)
            {
                Habitat habitat = (Habitat)target;
                cargoList = habitat.Cargo;
                empire = habitat.Empire;
                if (habitat.Population != null && habitat.Population.Count > 0)
                {
                    num3 = (int)(habitat.Population.TotalAmount / 10000);
                }
                num2 = Rnd.Next(0, 3);
            }
            string text = string.Empty;
            double num4 = 0.0;
            switch (num2)
            {
                case 0:
                    num4 = Math.Sqrt(num3) * (30.0 + Rnd.NextDouble() * 20.0);
                    num4 *= attackingEmpire.ColonyIncomeFactor;
                    num4 *= num;
                    num4 *= lootFactor;
                    num4 = Math.Max(100.0, Math.Min(empire.StateMoney * 0.5, num4));
                    attackingEmpire.StateMoney += num4;
                    empire.StateMoney -= num4;
                    if (attackingEmpire.PirateEmpireBaseHabitat != null)
                    {
                        attackingEmpire.PirateEconomy.PerformIncome(num4, PirateIncomeType.Undefined, CurrentStarDate);
                    }
                    text = string.Format(TextResolver.GetText("X credits"), num4.ToString("0"));
                    break;
                case 1:
                    {
                        ResearchNodeList researchNodeList = attackingEmpire.Research.ResolveMoreAdvancedProjects(empire);
                        if (researchNodeList.Count > 0)
                        {
                            ResearchNodeList researchNodeList2 = researchNodeList;
                            researchNodeList2 = industryType switch
                            {
                                IndustryType.Energy => researchNodeList.GetProjectsByIndustry(IndustryType.Energy),
                                IndustryType.HighTech => researchNodeList.GetProjectsByIndustry(IndustryType.HighTech),
                                IndustryType.Weapon => researchNodeList.GetProjectsByIndustry(IndustryType.Weapon),
                                _ => researchNodeList,
                            };
                            if (researchNodeList2.Count > 0 && num > 0.7)
                            {
                                ResearchNode researchNode = researchNodeList2[Rnd.Next(0, researchNodeList2.Count)];
                                if (researchNode != null)
                                {
                                    ResearchNode researchNode2 = attackingEmpire.Research.TechTree.FindNodeById(researchNode.ResearchNodeId);
                                    if (researchNode2 != null && !researchNode2.IsResearched)
                                    {
                                        attackingEmpire.DoResearchBreakthrough(researchNode2, selfResearched: false);
                                        text = string.Format(TextResolver.GetText("Research breakthrough in PROJECT"), researchNode2.Name);
                                    }
                                }
                                break;
                            }
                            ResearchNode researchNode3 = null;
                            switch (Rnd.Next(0, 3))
                            {
                                case 0:
                                    if (attackingEmpire.Research.ResearchQueueEnergy.Count > 0)
                                    {
                                        researchNode3 = attackingEmpire.Research.ResearchQueueEnergy[0];
                                    }
                                    break;
                                case 1:
                                    if (attackingEmpire.Research.ResearchQueueHighTech.Count > 0)
                                    {
                                        researchNode3 = attackingEmpire.Research.ResearchQueueHighTech[0];
                                    }
                                    break;
                                case 2:
                                    if (attackingEmpire.Research.ResearchQueueWeapons.Count > 0)
                                    {
                                        researchNode3 = attackingEmpire.Research.ResearchQueueWeapons[0];
                                    }
                                    break;
                            }
                            if (researchNode3 != null)
                            {
                                double num7 = (double)BaseTechCost / 120000.0;
                                double num8 = (40000.0 + 30000.0 * Rnd.NextDouble()) * num7;
                                num8 *= num;
                                num8 *= lootFactor;
                                researchNode3.Progress += (float)num8;
                                researchNode3.Progress = Math.Min(researchNode3.Cost - 1000f, researchNode3.Progress);
                                text = string.Format(TextResolver.GetText("Improved our understanding of PROJECT"), researchNode3.Name);
                            }
                            break;
                        }
                        ResearchNode researchNode4 = null;
                        switch (Rnd.Next(0, 3))
                        {
                            case 0:
                                if (attackingEmpire.Research.ResearchQueueEnergy.Count > 0)
                                {
                                    researchNode4 = attackingEmpire.Research.ResearchQueueEnergy[0];
                                }
                                break;
                            case 1:
                                if (attackingEmpire.Research.ResearchQueueHighTech.Count > 0)
                                {
                                    researchNode4 = attackingEmpire.Research.ResearchQueueHighTech[0];
                                }
                                break;
                            case 2:
                                if (attackingEmpire.Research.ResearchQueueWeapons.Count > 0)
                                {
                                    researchNode4 = attackingEmpire.Research.ResearchQueueWeapons[0];
                                }
                                break;
                        }
                        if (researchNode4 != null)
                        {
                            double num9 = (double)BaseTechCost / 120000.0;
                            double num10 = (40000.0 + 30000.0 * Rnd.NextDouble()) * num9;
                            num10 *= num;
                            num10 *= lootFactor;
                            researchNode4.Progress += (float)num10;
                            researchNode4.Progress = Math.Min(researchNode4.Cost - 1000f, researchNode4.Progress);
                            text = string.Format(TextResolver.GetText("Improved our understanding of PROJECT"), researchNode4.Name);
                        }
                        break;
                    }
                case 2:
                    {
                        if (cargoList == null || cargoList.Count <= 0)
                        {
                            break;
                        }
                        CargoList cargoList2 = new CargoList();
                        for (int i = 0; i < cargoList.Count; i++)
                        {
                            Cargo cargo = cargoList[i];
                            int num5 = (int)((double)cargo.Available * num * lootFactor);
                            if (cargo != null && cargo.EmpireId == empire.EmpireId && cargo.CommodityIsResource && num5 > 10 && (cargoList2.Count <= 0 || Rnd.Next(0, 2) == 1))
                            {
                                int num6 = Rnd.Next(5, num5);
                                if (num6 > 0)
                                {
                                    cargoList2.Add(new Cargo(cargo.Resource, num6, attackingEmpire));
                                    cargo.Amount -= num6;
                                }
                            }
                        }
                        if (cargoList2.Count > 0)
                        {
                            if (attackingEmpire.PirateEmpireBaseHabitat != null)
                            {
                                BuiltObject builtObject2 = IdentifyPirateBase(attackingEmpire);
                                if (builtObject2 != null && builtObject2.Cargo != null)
                                {
                                    for (int j = 0; j < cargoList2.Count; j++)
                                    {
                                        builtObject2.Cargo.Add(cargoList2[j]);
                                    }
                                }
                            }
                            else if (attackingEmpire.SpacePorts != null && attackingEmpire.SpacePorts.Count > 0)
                            {
                                BuiltObject builtObject3 = attackingEmpire.SpacePorts[0];
                                if (builtObject3 != null && builtObject3.Cargo != null)
                                {
                                    for (int k = 0; k < cargoList2.Count; k++)
                                    {
                                        builtObject3.Cargo.Add(cargoList2[k]);
                                    }
                                }
                            }
                            for (int l = 0; l < cargoList2.Count; l++)
                            {
                                if (l > 0)
                                {
                                    text += ", ";
                                }
                                text = text + cargoList2[l].Amount.ToString("0") + " " + cargoList2[l].Resource.Name;
                            }
                        }
                        else
                        {
                            num4 = Math.Sqrt(num3) * (30.0 + Rnd.NextDouble() * 20.0);
                            num4 *= attackingEmpire.ColonyIncomeFactor;
                            num4 *= num;
                            num4 *= lootFactor;
                            num4 = Math.Max(100.0, Math.Min(empire.StateMoney * 0.5, num4));
                            attackingEmpire.StateMoney += num4;
                            empire.StateMoney -= num4;
                            if (attackingEmpire.PirateEmpireBaseHabitat != null)
                            {
                                attackingEmpire.PirateEconomy.PerformIncome(num4, PirateIncomeType.Undefined, CurrentStarDate);
                            }
                            text = string.Format(TextResolver.GetText("X credits"), num4.ToString("0"));
                        }
                        break;
                    }
            }
            if (string.IsNullOrEmpty(text))
            {
                string description2 = string.Format(TextResolver.GetText("We have raided TARGET of the EMPIRE but failed to obtain any loot"), target.Name, empire.Name);
                attackingEmpire.SendMessageToEmpire(attackingEmpire, EmpireMessageType.RaidBonuses, target, description2);
                string empty2 = string.Empty;
                empty2 = ((!(target is Habitat)) ? string.Format(TextResolver.GetText("The EMPIRE have raided our base TARGET but failed to obtain any loot"), target.Name, attackingEmpire.Name) : string.Format(TextResolver.GetText("The EMPIRE have raided our colony TARGET but failed to obtain any loot"), target.Name, attackingEmpire.Name));
                empire.SendMessageToEmpire(empire, EmpireMessageType.RaidVictim, target, empty2);
                return;
            }
            if (attackingEmpire.Counters != null)
            {
                attackingEmpire.Counters.RaidSuccessCount++;
            }
            string empty3 = string.Empty;
            empty3 = ((target is BuiltObject) ? ((!(lootFactor < 1.0)) ? string.Format(TextResolver.GetText("We have raided base TARGET of the EMPIRE and pillaged the following"), target.Name, empire.Name, text) : string.Format(TextResolver.GetText("We have raided base TARGET of the EMPIRE and pillaged the following FAIL"), target.Name, empire.Name, text)) : ((!(lootFactor < 1.0)) ? string.Format(TextResolver.GetText("We have raided colony TARGET of the EMPIRE and pillaged the following"), target.Name, empire.Name, text) : string.Format(TextResolver.GetText("We have raided colony TARGET of the EMPIRE and pillaged the following FAIL"), target.Name, empire.Name, text)));
            attackingEmpire.SendMessageToEmpire(attackingEmpire, EmpireMessageType.RaidBonuses, target, empty3);
            if (target is BuiltObject)
            {
                string description3 = string.Format(TextResolver.GetText("The EMPIRE have raided our base TARGET and stolen the following"), target.Name, attackingEmpire.Name, text);
                empire.SendMessageToEmpire(empire, EmpireMessageType.RaidVictim, target, description3);
            }
            else
            {
                string description4 = string.Format(TextResolver.GetText("The EMPIRE have raided our colony TARGET and stolen the following"), target.Name, attackingEmpire.Name, text);
                empire.SendMessageToEmpire(empire, EmpireMessageType.RaidVictim, target, description4);
            }
        }

        public void InvestigateAbandonedBuiltObject(Empire investigatingEmpire, BuiltObject abandonedBuiltObject)
        {
            if (investigatingEmpire == null)
            {
                return;
            }
            string empty = string.Empty;
            string text = TextResolver.GetText("Ship");
            if (abandonedBuiltObject.Role == BuiltObjectRole.Base)
            {
                text = TextResolver.GetText("Base");
            }
            switch (abandonedBuiltObject.EncounterEventType)
            {
                case BuiltObjectEncounterEventType.Explodes:
                    {
                        empty = string.Format(TextResolver.GetText("Abandoned Ship Explodes"), text.ToLower(CultureInfo.InvariantCulture));
                        abandonedBuiltObject.InflictDamage(abandonedBuiltObject, null, 100000.0, CurrentDateTime, this, 0f, allowRecursion: false, double.MinValue, allowArmorInvulnerability: false);
                        BuiltObject builtObject = FindNearestBuiltObject((int)abandonedBuiltObject.Xpos, (int)abandonedBuiltObject.Ypos, investigatingEmpire);
                        if (builtObject != null)
                        {
                            int num2 = 300;
                            string text8 = TextResolver.GetText("Abandoned Ship Explodes Shields");
                            if (builtObject.CurrentShields < (float)num2)
                            {
                                text8 = TextResolver.GetText("Abandoned Ship Explodes Damage");
                                builtObject.CurrentShields = 0f;
                                num2 = 40;
                            }
                            empty += text8;
                            builtObject.InflictDamage(builtObject, null, num2, CurrentDateTime, this, 0f, allowRecursion: false, double.MinValue, allowArmorInvulnerability: false);
                            investigatingEmpire.SendEventMessageToEmpire(EventMessageType.BuiltObjectExplodes, string.Format(TextResolver.GetText("SHIPBASE Explodes"), text), empty, abandonedBuiltObject, abandonedBuiltObject);
                        }
                        break;
                    }
                case BuiltObjectEncounterEventType.PirateAmbush:
                    {
                        Empire empire = FindNearestPirateFaction(abandonedBuiltObject.Xpos, abandonedBuiltObject.Ypos, null, includeSuperPirates: false);
                        if (empire != null)
                        {
                            empty = string.Format(TextResolver.GetText("Abandoned Ship Pirate Ambush"), text.ToLower(CultureInfo.InvariantCulture), empire.Name);
                            empire.TakeOwnershipOfBuiltObject(abandonedBuiltObject, empire);
                            abandonedBuiltObject.IsAutoControlled = true;
                            BuiltObject builtObject2 = FindNearestBuiltObject((int)abandonedBuiltObject.Xpos, (int)abandonedBuiltObject.Ypos, investigatingEmpire);
                            if (builtObject2 != null)
                            {
                                BuiltObjectMissionType missionType = BuiltObjectMissionType.Attack;
                                if (empire != null)
                                {
                                    missionType = empire.DetermineDestroyOrCaptureTarget(abandonedBuiltObject, builtObject2, attackingAsGroup: false);
                                }
                                abandonedBuiltObject.AssignMission(missionType, builtObject2, null, BuiltObjectMissionPriority.High);
                                investigatingEmpire.SendEventMessageToEmpire(EventMessageType.PirateAmbush, TextResolver.GetText("Pirate Ambush") + "!", empty, abandonedBuiltObject, abandonedBuiltObject);
                            }
                            break;
                        }
                        goto case BuiltObjectEncounterEventType.Explodes;
                    }
                case BuiltObjectEncounterEventType.Acquire:
                    {
                        bool flag = false;
                        if (abandonedBuiltObject.Name.ToLower(CultureInfo.InvariantCulture).Contains(TextResolver.GetText("Refugee").ToLower(CultureInfo.InvariantCulture)))
                        {
                            flag = true;
                        }
                        string text2 = string.Empty;
                        if (investigatingEmpire == PlayerEmpire && !flag && StoryDistantWorldsEnabled)
                        {
                            text2 = GenerateStoryClue(abandonedBuiltObject);
                        }
                        investigatingEmpire.TakeOwnershipOfBuiltObject(abandonedBuiltObject, investigatingEmpire, setDesignAsObsolete: true);
                        abandonedBuiltObject.SupportCostFactor = 0.5f;
                        abandonedBuiltObject.IsAutoControlled = true;
                        bool flag2 = false;
                        if (abandonedBuiltObject.GameEventId >= 0 && CheckTriggerEvent(abandonedBuiltObject.GameEventId, investigatingEmpire, EventTriggerType.Investigate, null))
                        {
                            abandonedBuiltObject.PlayerEmpireEncounterAction = BuiltObjectEncounterAction.None;
                            flag2 = true;
                        }
                        if (flag2)
                        {
                            break;
                        }
                        string text3 = GenerateLocationDescription(abandonedBuiltObject.Xpos, abandonedBuiltObject.Ypos);
                        empty = ((!flag) ? string.Format(TextResolver.GetText("Abandoned Ship Acquire Intro"), ResolveDescription(abandonedBuiltObject.SubRole), abandonedBuiltObject.Name, text3) : string.Format(TextResolver.GetText("Abandoned Ship Acquire Intro Refugee"), ResolveDescription(abandonedBuiltObject.SubRole), text3));
                        if (flag)
                        {
                            empty = empty + ". " + TextResolver.GetText("Abandoned Ship Acquire Transfer Refugee");
                        }
                        else if (!string.IsNullOrEmpty(text2))
                        {
                            empty += ".\n\n";
                            empty += string.Format(TextResolver.GetText("Abandoned Ship Acquire Message"), text.ToLower(CultureInfo.InvariantCulture));
                            empty = empty + text2 + "\n\n";
                            empty += string.Format(TextResolver.GetText("Abandoned Ship Acquire Transfer X"), text.ToLower(CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            empty = empty + ". " + TextResolver.GetText("Abandoned Ship Acquire Transfer");
                        }
                        if (abandonedBuiltObject.Role == BuiltObjectRole.Military && investigatingEmpire == PlayerEmpire)
                        {
                            double num = ResolveTechBonusFactor(investigatingEmpire, this, abandonedBuiltObject);
                            if (num > 1.0)
                            {
                                empty = empty + "\n\n" + TextResolver.GetText("Abandoned Ship Acquire Tech Bonus");
                            }
                        }
                        if (abandonedBuiltObject.EncounterGovernmentTypeId < byte.MaxValue)
                        {
                            GovernmentAttributes governmentAttributes = Governments[abandonedBuiltObject.EncounterGovernmentTypeId];
                            if (investigatingEmpire.AllowableGovernmentTypes.Contains(abandonedBuiltObject.EncounterGovernmentTypeId))
                            {
                                empty += "\n\n";
                                empty += string.Format(TextResolver.GetText("Abandoned Ship Acquire Government Existing"), text.ToLower(CultureInfo.InvariantCulture), governmentAttributes.Name);
                                empty += "\n\n";
                                empty += TextResolver.GetText("Ruins Secret Form of Government Revealed Adoption");
                            }
                            else
                            {
                                empty += "\n\n";
                                empty += string.Format(TextResolver.GetText("Abandoned Ship Acquire Government"), text.ToLower(CultureInfo.InvariantCulture), governmentAttributes.Name);
                                string text4 = string.Empty;
                                switch (governmentAttributes.Availability)
                                {
                                    case 3:
                                        text4 = TextResolver.GetText("Government Description Way of Darkness");
                                        break;
                                    case 2:
                                        text4 = TextResolver.GetText("Government Description Way of the Ancients");
                                        break;
                                }
                                empty += text4;
                                empty += "\n\n";
                                empty += TextResolver.GetText("Ruins Secret Form of Government Revealed Adoption");
                                investigatingEmpire.AllowableGovernmentTypes.Add(abandonedBuiltObject.EncounterGovernmentTypeId);
                            }
                            abandonedBuiltObject.EncounterGovernmentTypeId = byte.MaxValue;
                        }
                        if (abandonedBuiltObject.EncounterExplorationBonus > 0)
                        {
                            int encounterExplorationBonus = abandonedBuiltObject.EncounterExplorationBonus;
                            abandonedBuiltObject.EncounterExplorationBonus = 0;
                            for (int i = 0; i < encounterExplorationBonus; i++)
                            {
                                Habitat habitat = FastFindNearestUnexploredHabitat(abandonedBuiltObject.Xpos, abandonedBuiltObject.Ypos, investigatingEmpire);
                                if (habitat == null)
                                {
                                    break;
                                }
                                investigatingEmpire.SystemVisibility[habitat.SystemIndex].TotallyExplored = true;
                                if (investigatingEmpire.ResourceMap != null)
                                {
                                    for (int j = 0; j < Systems[habitat.SystemIndex].Habitats.Count; j++)
                                    {
                                        Habitat habitat2 = Systems[habitat.SystemIndex].Habitats[j];
                                        investigatingEmpire.ResourceMap.SetResourcesKnown(habitat2, known: true);
                                    }
                                    if (Systems[habitat.SystemIndex].SystemStar != null)
                                    {
                                        investigatingEmpire.ResourceMap.SetResourcesKnown(Systems[habitat.SystemIndex].SystemStar, known: true);
                                    }
                                }
                                SystemVisibilityStatus status = investigatingEmpire.SystemVisibility[habitat.SystemIndex].Status;
                                if (status == SystemVisibilityStatus.Unexplored || status == SystemVisibilityStatus.Undefined)
                                {
                                    investigatingEmpire.SystemVisibility[habitat.SystemIndex].Status = SystemVisibilityStatus.Explored;
                                }
                            }
                            empty += "\n\n";
                            empty += string.Format(TextResolver.GetText("Abandoned Ship Acquire Maps"), text.ToLower(CultureInfo.InvariantCulture), encounterExplorationBonus.ToString());
                        }
                        if (abandonedBuiltObject.EncounterMoneyBonus > 0)
                        {
                            investigatingEmpire.StateMoney += abandonedBuiltObject.EncounterMoneyBonus;
                            investigatingEmpire.PirateEconomy.PerformIncome(abandonedBuiltObject.EncounterMoneyBonus, PirateIncomeType.Undefined, CurrentStarDate);
                            empty += "\n\n";
                            empty += string.Format(TextResolver.GetText("Abandoned Ship Acquire Money"), text.ToLower(CultureInfo.InvariantCulture), abandonedBuiltObject.EncounterMoneyBonus.ToString("###,###,##0"));
                            abandonedBuiltObject.EncounterMoneyBonus = 0;
                        }
                        if (abandonedBuiltObject.EncounterTechAdvanceCount > 0)
                        {
                            if (abandonedBuiltObject.EncounterTechAdvanceCount == 1)
                            {
                                ResearchNode researchNode = investigatingEmpire.Research.SelectRandomNextResearchProjectExcludeSuperWeapons(this);
                                if (researchNode != null)
                                {
                                    investigatingEmpire.DoResearchBreakthrough(researchNode, selfResearched: true, blockMessages: true, suppressUpdate: true);
                                    investigatingEmpire.Research.Update(investigatingEmpire.DominantRace);
                                    investigatingEmpire.ReviewDesignsBuiltObjectsImprovedComponents();
                                    investigatingEmpire.ReviewResearchAbilities();
                                    empty += "\n\n";
                                    empty += string.Format(TextResolver.GetText("Abandoned Ship Acquire Tech"), text.ToLower(CultureInfo.InvariantCulture), researchNode.Name);
                                }
                            }
                            else
                            {
                                empty += "\n\n";
                                empty += string.Format(TextResolver.GetText("Abandoned Ship Acquire Tech Multiple"), text.ToLower(CultureInfo.InvariantCulture));
                                for (int k = 0; k < abandonedBuiltObject.EncounterTechAdvanceCount; k++)
                                {
                                    ResearchNode researchNode2 = investigatingEmpire.Research.SelectRandomNextResearchProjectExcludeSuperWeapons(this);
                                    if (researchNode2 != null)
                                    {
                                        investigatingEmpire.DoResearchBreakthrough(researchNode2, selfResearched: true, blockMessages: true, suppressUpdate: false);
                                        empty = empty + researchNode2.Name + ", ";
                                    }
                                }
                                investigatingEmpire.Research.Update(investigatingEmpire.DominantRace);
                                investigatingEmpire.ReviewDesignsBuiltObjectsImprovedComponents();
                                investigatingEmpire.ReviewResearchAbilities();
                                empty = empty.Substring(0, empty.Length - 2);
                            }
                            abandonedBuiltObject.EncounterTechAdvanceCount = 0;
                        }
                        if (string.IsNullOrEmpty(text2) && investigatingEmpire == PlayerEmpire && !flag)
                        {
                            string text5 = CheckForStoryLocationHint();
                            if (!string.IsNullOrEmpty(text5))
                            {
                                empty += "\n\n";
                                empty += "*** ";
                                empty = empty + string.Format(TextResolver.GetText("Abandoned Ship Acquire NAVIGATIONAL DIRECTIONS"), text.ToLower(CultureInfo.InvariantCulture)) + " ";
                                empty += text5;
                                empty += ". ***\n\n";
                                empty += TextResolver.GetText("We should send a ship to investigate this location.");
                            }
                            else if (!flag && Rnd.Next(0, 2) == 1)
                            {
                                string text6 = GenerateNavigationalBonusMessage(abandonedBuiltObject.Xpos, abandonedBuiltObject.Ypos, investigatingEmpire);
                                if (!string.IsNullOrEmpty(text6))
                                {
                                    empty += "\n\n";
                                    empty += "*** ";
                                    empty = empty + string.Format(TextResolver.GetText("Abandoned Ship Acquire NAVIGATIONAL DIRECTIONS"), text.ToLower(CultureInfo.InvariantCulture)) + " ";
                                    empty += text6;
                                    empty += ". ***\n\n";
                                    empty += TextResolver.GetText("Maybe we should send a ship to investigate this location.");
                                }
                            }
                        }
                        if (abandonedBuiltObject.SubRole == BuiltObjectSubRole.ColonyShip && abandonedBuiltObject.NativeRace != null)
                        {
                            empty += "\n\n";
                            empty += string.Format(TextResolver.GetText("Abandoned Ship Acquire Colony Ship"), abandonedBuiltObject.NativeRace.Name);
                            empty += "\n\n";
                            empty += GenerateRaceReport(abandonedBuiltObject.NativeRace);
                        }
                        if (flag)
                        {
                            investigatingEmpire.SendMessageToEmpire(investigatingEmpire, EmpireMessageType.ExplorationBuiltObject, abandonedBuiltObject, empty);
                            break;
                        }
                        string text7 = TextResolver.GetText("Abandoned Ship Acquired");
                        if (abandonedBuiltObject.Role == BuiltObjectRole.Base)
                        {
                            text7 = TextResolver.GetText("Abandoned Base Acquired");
                        }
                        if (abandonedBuiltObject.PlayerEmpireEncounterAction == BuiltObjectEncounterAction.Notify)
                        {
                            investigatingEmpire.SendMessageToEmpire(investigatingEmpire, EmpireMessageType.ExplorationBuiltObject, abandonedBuiltObject, empty);
                        }
                        else
                        {
                            EventMessageType eventMessageType = EventMessageType.FreeSuperShip;
                            if (!string.IsNullOrEmpty(text2))
                            {
                                text7 = TextResolver.GetText("Galactic History Uncovered");
                                eventMessageType = EventMessageType.StoryClue;
                            }
                            investigatingEmpire.SendEventMessageToEmpire(eventMessageType, text7, empty, abandonedBuiltObject, abandonedBuiltObject);
                        }
                        abandonedBuiltObject.PlayerEmpireEncounterAction = BuiltObjectEncounterAction.None;
                        break;
                    }
            }
        }

        public void GenerateSpecialBonusRuins()
        {
            Habitat habitat = FindLonelyHabitat(RuinType.EmpireBonus, HabitatType.BarrenRock);
            if (habitat != null)
            {
                habitat.Ruin = GenerateRuin(habitat, TextResolver.GetText("Energy Engineering Facility"), 7, RuinType.EmpireBonus);
                habitat.Ruin.BonusResearchEnergy = 0.5;
                string text = "";
                text = text ?? "";
                text = text ?? "";
                text = text ?? "";
                habitat.Ruin.Description = text;
                if (!_RuinsHabitats.Contains(habitat))
                {
                    _RuinsHabitats.Add(habitat);
                }
            }
            habitat = FindLonelyHabitat(RuinType.EmpireBonus, HabitatType.BarrenRock);
            if (habitat != null)
            {
                habitat.Ruin = GenerateRuin(habitat, TextResolver.GetText("Techno Nexus"), 7, RuinType.EmpireBonus);
                habitat.Ruin.BonusResearchHighTech = 0.5;
                string text2 = "";
                text2 = text2 ?? "";
                text2 = text2 ?? "";
                text2 = text2 ?? "";
                habitat.Ruin.Description = text2;
                if (!_RuinsHabitats.Contains(habitat))
                {
                    _RuinsHabitats.Add(habitat);
                }
            }
            habitat = FindLonelyHabitat(RuinType.EmpireBonus, HabitatType.BarrenRock);
            if (habitat != null)
            {
                habitat.Ruin = GenerateRuin(habitat, TextResolver.GetText("Carida Armaments Installation"), 7, RuinType.EmpireBonus);
                habitat.Ruin.BonusResearchWeapons = 0.5;
                string text3 = "";
                text3 = text3 ?? "";
                text3 = text3 ?? "";
                text3 = text3 ?? "";
                habitat.Ruin.Description = text3;
                if (!_RuinsHabitats.Contains(habitat))
                {
                    _RuinsHabitats.Add(habitat);
                }
            }
            habitat = FindLonelyHabitat(RuinType.EmpireBonus, HabitatType.BarrenRock);
            if (habitat != null)
            {
                habitat.Ruin = GenerateRuin(habitat, TextResolver.GetText("Unity Forum"), 3, RuinType.EmpireBonus);
                habitat.Ruin.BonusDiplomacy = 0.2;
                string text4 = "";
                text4 = text4 ?? "";
                text4 = text4 ?? "";
                text4 = text4 ?? "";
                habitat.Ruin.Description = text4;
                if (!_RuinsHabitats.Contains(habitat))
                {
                    _RuinsHabitats.Add(habitat);
                }
            }
            habitat = FindLonelyHabitat(RuinType.EmpireBonus, HabitatType.BarrenRock);
            if (habitat != null)
            {
                habitat.Ruin = GenerateRuin(habitat, TextResolver.GetText("Garden of Arcadia"), 0, RuinType.EmpireBonus);
                habitat.Ruin.BonusHappiness = 0.1;
                string text5 = "";
                text5 = text5 ?? "";
                text5 = text5 ?? "";
                text5 = text5 ?? "";
                habitat.Ruin.Description = text5;
                if (!_RuinsHabitats.Contains(habitat))
                {
                    _RuinsHabitats.Add(habitat);
                }
            }
            habitat = FindLonelyHabitat(RuinType.EmpireBonus, HabitatType.BarrenRock);
            if (habitat != null)
            {
                habitat.Ruin = GenerateRuin(habitat, TextResolver.GetText("Great Mercantile Exchange"), 8, RuinType.EmpireBonus);
                habitat.Ruin.BonusWealth = 0.1;
                string text6 = "";
                text6 = text6 ?? "";
                text6 = text6 ?? "";
                text6 = text6 ?? "";
                habitat.Ruin.Description = text6;
                if (!_RuinsHabitats.Contains(habitat))
                {
                    _RuinsHabitats.Add(habitat);
                }
            }
            habitat = FindLonelyHabitat(RuinType.EmpireBonus, HabitatType.BarrenRock);
            if (habitat != null)
            {
                habitat.Ruin = GenerateRuin(habitat, TextResolver.GetText("Fortress of Torak"), 14, RuinType.EmpireBonus);
                habitat.Ruin.BonusDefensive = 1.0;
                string text7 = "";
                text7 = text7 ?? "";
                text7 = text7 ?? "";
                text7 = text7 ?? "";
                habitat.Ruin.Description = text7;
                if (!_RuinsHabitats.Contains(habitat))
                {
                    _RuinsHabitats.Add(habitat);
                }
            }
        }

    }
}
