// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconEmpire
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds;
using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BaconDistantWorlds
{
    public static class BaconEmpire
    {
        public static double myStrengthMultipler = 3.0;
        public static float myTroopMaintenancehMultipler = 0.05f;
        public static double myResourceExtractionBonusMultipler = 5.0;
        public static float researchPerLab = 0.0f;
        public static int warWearinessReduction = -2;
        public static double SubjugationTributePercentage = 0.1;

        public static double MultiplyTroopRecruitment(Empire empire)
        {
            double num = 1.0;
            if (empire != null && empire.Name.Contains("Romulan"))
                num = BaconEmpire.myStrengthMultipler;
            return num;
        }

        public static float MultiplyTroopMaintenance(Empire empire)
        {
            float num = 1f;
            if (empire != null && empire.Name.Contains("Romulan"))
                num = BaconEmpire.myTroopMaintenancehMultipler;
            return num;
        }

        public static bool PerformIntelligenceMission(Empire empire)
        {
            bool flag = true;
            if (empire != null && empire.Name.Contains("Romulan"))
                flag = true;
            return flag;
        }

        public static double MultiplyResourceExtractionBonus(Empire empire)
        {
            double num = 1.0;
            if (empire != null && empire.Name.Contains("Romulan"))
                num = BaconEmpire.myResourceExtractionBonusMultipler;
            return num;
        }

        public static void SetPirateFactionModifiers(Empire empire, PiratePlayStyle piratePlayStyle)
        {
            double smugglingIncomeFactor = 1.0;
            double raidStrengthFactor = 1.0;
            double raidBonusFactor = 1.0;
            double shipMaintenancePrivateFactor = 1.0;
            double shipMaintenanceStateFactor = 1.0;
            double researchWeaponsFactor = 1.0;
            double researchEnergyFactor = 1.0;
            double researchHighTechFactor = 1.0;
            double planetaryFacilityEliminationFactor = 1.0;
            double lootingFactor = 1.0;
            double planetaryFacilityBuildFactor = 1.0;
            double planetaryWonderBuildFactor = 1.0;
            Galaxy.SetPirateFactionModifiers(piratePlayStyle, out smugglingIncomeFactor, out raidStrengthFactor, out raidBonusFactor, out shipMaintenancePrivateFactor, out shipMaintenanceStateFactor, out researchWeaponsFactor, out researchEnergyFactor, out researchHighTechFactor, out planetaryFacilityEliminationFactor, out lootingFactor, out planetaryFacilityBuildFactor, out planetaryWonderBuildFactor);
            empire.SmugglingIncomeFactor = smugglingIncomeFactor;
            empire.RaidStrengthFactor = raidStrengthFactor;
            empire.RaidBonusFactor = raidBonusFactor;
            empire.ShipMaintenancePrivateFactor = shipMaintenancePrivateFactor;
            empire.ShipMaintenanceStateFactor = shipMaintenanceStateFactor;
            empire.ResearchWeaponsFactor = researchWeaponsFactor;
            empire.ResearchEnergyFactor = researchEnergyFactor;
            empire.ResearchHighTechFactor = researchHighTechFactor;
            empire.PlanetaryFacilityEliminationFactor = planetaryFacilityEliminationFactor;
            empire.LootingFactor = lootingFactor;
            empire.PlanetaryFacilityBuildFactor = planetaryFacilityBuildFactor;
            empire.PlanetaryWonderBuildFactor = planetaryWonderBuildFactor;
        }

        public static IntelligenceMissionOutcome DetermineIntelligenceMissionOutcome(
          Empire empire,
          IntelligenceMission mission,
          Character agent)
        {
            bool flag = empire != null && empire.Name.Contains("Romulan");
            if (mission.TargetEmpire.Name.Contains("Romulan"))
                return IntelligenceMissionOutcome.Capture;
            double missionSuccessChance = empire.CalculateIntelligenceMissionSuccessChance(mission, agent);
            double num1 = Galaxy.Rnd.NextDouble();
            IntelligenceMissionOutcome intelligenceMissionOutcome;
            if (num1 >= missionSuccessChance)
            {
                double num2 = missionSuccessChance + (1.0 - missionSuccessChance) * 0.9;
                double num3 = missionSuccessChance + (1.0 - missionSuccessChance) * 0.6;
                double num4 = missionSuccessChance + (1.0 - missionSuccessChance) * 0.25;
                intelligenceMissionOutcome = num1 <= num2 ? (num1 <= num3 ? (num1 <= num4 ? IntelligenceMissionOutcome.FailNotDetect : IntelligenceMissionOutcome.SucceedDetect) : IntelligenceMissionOutcome.FailDetect) : (flag ? IntelligenceMissionOutcome.FailDetect : IntelligenceMissionOutcome.Capture);
            }
            else
                intelligenceMissionOutcome = IntelligenceMissionOutcome.SucceedNotDetect;
            if (mission.Type == IntelligenceMissionType.DeepCover && intelligenceMissionOutcome == IntelligenceMissionOutcome.SucceedDetect)
                intelligenceMissionOutcome = IntelligenceMissionOutcome.FailNotDetect;
            return intelligenceMissionOutcome;
        }

        public static IntelligenceMission OverrideTimeForMission(
          Empire originEmpire,
          IntelligenceMission mission)
        {
            if (mission == null || originEmpire == null || !originEmpire.Name.Contains("Romulan"))
                return mission;
            mission.TimeLength = (long)(Galaxy.RealSecondsInGalacticYear * 1000 / 24);
            return mission;
        }

        public static void EnhanceCharacter(Empire empire, Character character)
        {
            if (empire == null || !empire.Name.Contains("Romulan"))
                return;
            switch (character.Role)
            {
                case CharacterRole.Leader:
                    if (!character.CheckHasSkill(CharacterSkillType.ColonyIncome))
                        character.AddSkill(CharacterSkillType.ColonyIncome, 6, (Galaxy)null);
                    if (!character.CheckHasSkill(CharacterSkillType.ColonyHappiness))
                        character.AddSkill(CharacterSkillType.ColonyHappiness, 6, (Galaxy)null);
                    if (!character.CheckHasSkill(CharacterSkillType.PopulationGrowth))
                    {
                        character.AddSkill(CharacterSkillType.PopulationGrowth, 6, (Galaxy)null);
                        break;
                    }
                    break;
                case CharacterRole.ColonyGovernor:
                    if (!character.CheckHasSkill(CharacterSkillType.ColonyIncome))
                        character.AddSkill(CharacterSkillType.ColonyIncome, 6, (Galaxy)null);
                    if (!character.CheckHasSkill(CharacterSkillType.ColonyHappiness))
                        character.AddSkill(CharacterSkillType.ColonyHappiness, 6, (Galaxy)null);
                    if (!character.CheckHasSkill(CharacterSkillType.PopulationGrowth))
                        character.AddSkill(CharacterSkillType.PopulationGrowth, 6, (Galaxy)null);
                    if (!character.CheckHasSkill(CharacterSkillType.TradeIncome))
                    {
                        character.AddSkill(CharacterSkillType.TradeIncome, 6, (Galaxy)null);
                        break;
                    }
                    break;
                case CharacterRole.ShipCaptain:
                    if (!character.CheckHasSkill(CharacterSkillType.Targeting))
                        character.AddSkill(CharacterSkillType.Targeting, 6, (Galaxy)null);
                    if (!character.CheckHasSkill(CharacterSkillType.ShieldRechargeRate))
                        character.AddSkill(CharacterSkillType.ShieldRechargeRate, 6, (Galaxy)null);
                    if (!character.CheckHasSkill(CharacterSkillType.WeaponsDamage))
                        character.AddSkill(CharacterSkillType.WeaponsDamage, 6, (Galaxy)null);
                    if (!character.CheckHasSkill(CharacterSkillType.WeaponsRange))
                    {
                        character.AddSkill(CharacterSkillType.WeaponsRange, 6, (Galaxy)null);
                        break;
                    }
                    break;
            }
            character.BonusesKnown = true;
        }

        public static void ProcessScienceShips(Main main)
        {
            BaconEmpire.ProcessScienceShips(main, main._Game.Galaxy.Empires);
            BaconEmpire.ProcessScienceShips(main, main._Game.Galaxy.PirateEmpires);
        }

        public static void ProcessScienceShips(Main main, EmpireList empiresAndPirates)
        {
            List<IndustryType> industryTypeList = new List<IndustryType>();
            industryTypeList.Add(IndustryType.Weapon);
            industryTypeList.Add(IndustryType.Energy);
            industryTypeList.Add(IndustryType.HighTech);
            try
            {
                foreach (Empire empiresAndPirate in (SyncList<Empire>)empiresAndPirates)
                {
                    foreach (BuiltObject ship in empiresAndPirate.BuiltObjects.Where<BuiltObject>((Func<BuiltObject, bool>)(x => x.SubRole == BuiltObjectSubRole.ExplorationShip)))
                    {
                        if (ship.BaconValues != null)
                        {
                            if (!ship.BaconValues.ContainsKey("scientificData") && (ship.BaconValues.ContainsKey("lab0") || ship.BaconValues.ContainsKey("lab1") || ship.BaconValues.ContainsKey("lab2")))
                                ship.BaconValues["scientificData"] = (object)0;
                            int baconValue = (int)ship.BaconValues["scientificData"];
                            if (baconValue > 110)
                                BaconEmpire.StoreScientificData(main, ship);
                            if (baconValue < 3)
                                BaconEmpire.TryRefillScientifiData(main, ship);
                        }
                        List<bool> boolList = new List<bool>(3);
                        boolList.Add(ship.Components.Exists((Predicate<BuiltObjectComponent>)(x => x.Type == ComponentType.LabsWeaponsLab && x.Status == ComponentStatus.Normal)));
                        boolList.Add(ship.Components.Exists((Predicate<BuiltObjectComponent>)(x => x.Type == ComponentType.LabsEnergyLab && x.Status == ComponentStatus.Normal)));
                        boolList.Add(ship.Components.Exists((Predicate<BuiltObjectComponent>)(x => x.Type == ComponentType.LabsHighTechLab && x.Status == ComponentStatus.Normal)));
                        for (int index = 0; index < boolList.Count; ++index)
                        {
                            if (boolList[index])
                            {
                                ResearchNode subject = BaconEmpire.GetCurrentResearchNode(ship, index);
                                if (subject != null && subject.IsResearched)
                                    subject = (ResearchNode)null;
                                if (subject == null)
                                    subject = BaconEmpire.GetRandomResearchNode(empiresAndPirate, empiresAndPirate.Research.TechTree, industryTypeList[index]);
                                if (subject != null)
                                {
                                    if (ship.BaconValues.ContainsKey("scientificData") && (int)ship.BaconValues["scientificData"] > 0)
                                    {
                                        subject.Progress += BaconEmpire.researchPerLab * (float)(1.0 + empiresAndPirate.ResearchBonus);
                                        ship.BaconValues["scientificData"] = (object)((int)ship.BaconValues["scientificData"] - 1);
                                    }
                                    if (!ship.BaconValues.ContainsKey("lab" + index.ToString()))
                                        ship.BaconValues.Add("lab" + index.ToString(), (object)subject);
                                    else
                                        ship.BaconValues["lab" + index.ToString()] = (object)subject;
                                    if ((double)subject.Progress >= (double)subject.Cost)
                                    {
                                        if (!subject.IsResearched)
                                            empiresAndPirate.SendMessageToEmpire(empiresAndPirate, EmpireMessageType.ResearchBreakthrough, (object)subject, ship.Name + " completed research on " + subject.Name, Point.Empty, "scienceResearchComplete");
                                        subject.IsResearched = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static ResearchNode GetCurrentResearchNode(BuiltObject ship, int industryType)
        {
            ResearchNode currentResearchNode = (ResearchNode)null;
            try
            {
                if (ship.BaconValues == null)
                    ship.BaconValues = new Dictionary<string, object>();
                currentResearchNode = (ResearchNode)ship.BaconValues["lab" + industryType.ToString()];
            }
            catch (Exception ex)
            {
            }
            return currentResearchNode;
        }

        public static ResearchNode GetRandomResearchNode(
          Empire empire,
          ResearchNodeList researchQueue,
          IndustryType industryType)
        {
            try
            {
                ResearchNodeList researchNodeList = new ResearchNodeList();
                ResearchNode randomResearchNode = (ResearchNode)null;
                ResearchNodeList nodesForIndustryType = BaconEmpire.GetAllPotentialResearchNodesForIndustryType(empire, researchQueue, industryType);
                if (nodesForIndustryType.Count > 0)
                    randomResearchNode = nodesForIndustryType[Galaxy.Rnd.Next(0, nodesForIndustryType.Count)];
                return randomResearchNode;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static ResearchNodeList GetAllPotentialResearchNodesForIndustryType(
          Empire empire,
          ResearchNodeList researchQueue,
          IndustryType industryType)
        {
            try
            {
                ResearchNodeList nodesForIndustryType = new ResearchNodeList();
                foreach (ResearchNode research in (SyncList<ResearchNode>)researchQueue)
                {
                    bool flag1 = true;
                    bool flag2 = false;
                    if (empire.Research.CanResearchNode(research) && !research.IsResearched && research.Industry == industryType && empire.Research.CanResearchNode(research))
                    {
                        if (research.ParentNodes != null && research.ParentNodes.Count > 0)
                        {
                            flag1 = true;
                            for (int index = 0; index < research.ParentNodes.Count; ++index)
                            {
                                if (!research.ParentNodes[index].IsResearched)
                                    flag1 = false;
                            }
                        }
                        if (research.AllowedRaces != null && !research.AllowedRaces.Contains(empire.DominantRace))
                            flag2 = true;
                        if (research.DisallowedRaces != null && research.DisallowedRaces.Contains(empire.DominantRace))
                            flag2 = true;
                        if (flag1 && (empire.Name.ToLower().Contains("romulan") || !flag2))
                            nodesForIndustryType.Add(research);
                    }
                }
                return nodesForIndustryType;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void DestroyUnreservedCargoOfEmpire(CargoList cargoList, Empire empire)
        {
        }

        public static BuiltObjectList RemoveStateShips(Empire empire, BuiltObjectList ships)
        {
            foreach (BuiltObject builtObject in ships.Where<BuiltObject>((Func<BuiltObject, bool>)(x => x.Owner != null && !x.IsAutoControlled)).ToList<BuiltObject>())
                ships.Remove(builtObject);
            return ships;
        }

        public static void SetRetreatWhenAttacked()
        {
            foreach (Empire empire in (SyncList<Empire>)BaconBuiltObject.myMain._Game.Galaxy.Empires)
            {
                foreach (Design design in empire.Designs.Where<Design>((Func<Design, bool>)(x => x.FleeWhen == BuiltObjectFleeWhen.EnemyMilitarySighted)))
                    design.FleeWhen = BuiltObjectFleeWhen.Attacked;
            }
            foreach (Empire pirateEmpire in (SyncList<Empire>)BaconBuiltObject.myMain._Game.Galaxy.PirateEmpires)
            {
                foreach (Design design in pirateEmpire.Designs.Where<Design>((Func<Design, bool>)(x => x.FleeWhen == BuiltObjectFleeWhen.EnemyMilitarySighted)))
                    design.FleeWhen = BuiltObjectFleeWhen.Attacked;
            }
        }

        public static double AdjustWarWearinessWhenAtPeace(Empire empire) => (double)BaconEmpire.warWearinessReduction;

        public static double MaxResourceExtractionRate(BuiltObject ship, int resourceType)
        {
            if (ship.ActualEmpire != null && ship.ActualEmpire.Name.Contains("Romulan"))
                return 40000.0;
            double num = 4.0;
            switch (resourceType)
            {
                case 0:
                    ComponentImprovement componentImprovement1 = ((IEnumerable<ComponentImprovement>)ship.Empire.Research.ComponentImprovements).FirstOrDefault<ComponentImprovement>((Func<ComponentImprovement, bool>)(x => x != null && x.ImprovedComponent != null && x.ImprovedComponent.Type == ComponentType.ExtractorMine));
                    if (componentImprovement1 != null)
                        num = (double)componentImprovement1.Value2;
                    if (num < 12.0)
                    {
                        num = 12.0;
                        break;
                    }
                    break;
                case 1:
                    ComponentImprovement componentImprovement2 = ((IEnumerable<ComponentImprovement>)ship.Empire.Research.ComponentImprovements).FirstOrDefault<ComponentImprovement>((Func<ComponentImprovement, bool>)(x => x != null && x.ImprovedComponent != null && x.ImprovedComponent.Type == ComponentType.ExtractorLuxury));
                    if (componentImprovement2 != null)
                        num = (double)componentImprovement2.Value2;
                    if (num < 12.0)
                    {
                        num = 12.0;
                        break;
                    }
                    break;
                case 2:
                    ComponentImprovement componentImprovement3 = ((IEnumerable<ComponentImprovement>)ship.Empire.Research.ComponentImprovements).FirstOrDefault<ComponentImprovement>((Func<ComponentImprovement, bool>)(x => x != null && x.ImprovedComponent != null && x.ImprovedComponent.Type == ComponentType.ExtractorGasExtractor));
                    if (componentImprovement3 != null)
                        num = (double)componentImprovement3.Value2;
                    if (num < 12.0)
                    {
                        num = 40.0;
                        break;
                    }
                    break;
            }
            return num;
        }

        public static void ShowPrisonForm(Main main) => BaconMain.prisonForm = new prisonForm(main);

        public static void ShowCustomBomberForm(Main main) => BaconMain.CustomBomberForm = new CustomBomberForm(main);

        public static List<object> GetOutstandingLoans()
        {
            Main main = BaconBuiltObject.myMain;
            List<object> outstandingLoans = new List<object>();
            if (main._Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
            {
                if (main._Game.PlayerEmpire.Capital.BaconValues == null)
                    main._Game.PlayerEmpire.Capital.BaconValues = new Dictionary<string, object>();
                outstandingLoans = main._Game.PlayerEmpire.Capital.BaconValues.Where<KeyValuePair<string, object>>((Func<KeyValuePair<string, object>, bool>)(x => x.Key.Contains("loan"))).Select<KeyValuePair<string, object>, object>((Func<KeyValuePair<string, object>, object>)(y => y.Value)).ToList<object>();
            }
            else if (main._Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
            {
                if (main._Game.PlayerEmpire.BuiltObjects[0].BaconValues == null)
                    main._Game.PlayerEmpire.BuiltObjects[0].BaconValues = new Dictionary<string, object>();
                outstandingLoans = main._Game.PlayerEmpire.BuiltObjects[0].BaconValues.Where<KeyValuePair<string, object>>((Func<KeyValuePair<string, object>, bool>)(x => x.Key.Contains("loan"))).Select<KeyValuePair<string, object>, object>((Func<KeyValuePair<string, object>, object>)(y => y.Value)).ToList<object>();
            }
            return outstandingLoans;
        }

        public static int GetTotalLoanDebt()
        {
            List<object> outstandingLoans = BaconEmpire.GetOutstandingLoans();
            int totalLoanDebt = 0;
            foreach (object obj in outstandingLoans)
            {
                if (obj is List<int>)
                {
                    int num1 = ((List<int>)obj)[0];
                    int num2 = ((List<int>)obj)[1];
                    totalLoanDebt += num1 * num2;
                }
            }
            return totalLoanDebt;
        }

        public static void Loan(Main main, string input)
        {
            try
            {
                string[] strArray = input.Split(' ');
                if (strArray.Length == 1)
                {
                    if (main._Game.Galaxy.DelayedActions.FirstOrDefault<EventActionExecutionPackage>((Func<EventActionExecutionPackage, bool>)(x => x.Action.MessageTitle.Contains("loan"))) == null)
                        return;
                    List<object> outstandingLoans = BaconEmpire.GetOutstandingLoans();
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("Current outstanding loans:").Append(Environment.NewLine).Append(Environment.NewLine);
                    foreach (object obj in outstandingLoans)
                    {
                        if (obj is List<int>)
                        {
                            int num1 = ((List<int>)obj)[0];
                            int num2 = ((List<int>)obj)[1];
                            stringBuilder.Append(num1).Append(" credits for ").Append(num2).Append(" months.").Append(Environment.NewLine);
                        }
                    }
                    BaconBuiltObject.ShowMessageBox(main, stringBuilder.ToString(), "Outstanding Loans");
                }
                else
                {
                    if (strArray.Length != 2)
                        return;
                    if (strArray[0] == "!loan")
                    {
                        int result;
                        if (int.TryParse(strArray[1], out result))
                        {
                            int creditLineRemaining = BaconEmpire.GetMaxCreditLineRemaining(main);
                            if (result > creditLineRemaining)
                            {
                                StringBuilder stringBuilder = new StringBuilder();
                                stringBuilder.Append("This loan would exceed your available credit.").Append(Environment.NewLine);
                                if (creditLineRemaining > 0)
                                    stringBuilder.Append("Remaining available credit: ").Append(creditLineRemaining);
                                else
                                    stringBuilder.Append("You have no credit remaining.");
                                BaconBuiltObject.ShowMessageBox(main, stringBuilder.ToString(), "Credit Rating Exceeded");
                            }
                            else
                            {
                                double loanRate = BaconEmpire.CalculateLoanRate(main, result);
                                double num3 = loanRate / 12.0;
                                double y = 120.0;
                                int num4 = (int)Math.Round((double)result * (num3 * Math.Pow(1.0 + num3, y) / (Math.Pow(1.0 + num3, y) - 1.0)) + 0.5);
                                string message = "Amount: " + result.ToString() + Environment.NewLine + "rate: " + loanRate.ToString("N6") + Environment.NewLine + "Length: " + y.ToString() + " months" + Environment.NewLine + "Monthly payments: " + num4.ToString() + Environment.NewLine + Environment.NewLine + "Do you agree to these terms?";
                                if (BaconBuiltObject.PauseAndShowYesNoMessageBox(main, message, "Loan terms") == "Yes")
                                {
                                    int num5 = 1;
                                    List<int> intList = new List<int>();
                                    intList.Add(num4);
                                    intList.Add((int)y);
                                    string key;
                                    if (main._Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
                                    {
                                        if (main._Game.PlayerEmpire.Capital.BaconValues == null)
                                            main._Game.PlayerEmpire.Capital.BaconValues = new Dictionary<string, object>();
                                        while (main._Game.PlayerEmpire.Capital.BaconValues.ContainsKey("loan" + num5.ToString()))
                                            ++num5;
                                        key = "loan" + num5.ToString();
                                        main._Game.PlayerEmpire.Capital.BaconValues.Add(key, (object)intList);
                                    }
                                    else
                                    {
                                        if (main._Game.PlayerEmpire.BuiltObjects[0].BaconValues == null)
                                            main._Game.PlayerEmpire.BuiltObjects[0].BaconValues = new Dictionary<string, object>();
                                        while (main._Game.PlayerEmpire.BuiltObjects[0].BaconValues.ContainsKey("loan" + num5.ToString()))
                                            ++num5;
                                        key = "loan" + num5.ToString();
                                        main._Game.PlayerEmpire.BuiltObjects[0].BaconValues.Add(key, (object)intList);
                                    }
                                    main._Game.PlayerEmpire.StateMoney += (double)result;
                                    EventAction action = new EventAction((StellarObject)null, EventActionType.StartPlague);
                                    action.MessageTitle = key;
                                    action.ExecutionDate = main._Game.Galaxy.CurrentStarDate + (long)(Galaxy.RealSecondsInGalacticYear * 1000 / 360 * 30);
                                    GameEvent gameEvent = new GameEvent(main._Game.Galaxy, (short)0, (StellarObject)null);
                                    EventActionExecutionPackage executionPackage = new EventActionExecutionPackage(action, gameEvent, main._Game.PlayerEmpire);
                                    main._Game.Galaxy.DelayedActions.Add(executionPackage);
                                }
                            }
                        }
                    }
                    else if (!(strArray[0] == "!loanpayment") || main._Game.Galaxy.DelayedActions.FirstOrDefault<EventActionExecutionPackage>((Func<EventActionExecutionPackage, bool>)(x => x.Action.MessageTitle.Contains("loan"))) == null)
                        ;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void SetResourcesForAllCapitals(Main main, string input, bool pirates = false)
        {
            int result = 100000;
            string[] strArray = input.Split(' ');
            if (strArray.Length > 1)
                int.TryParse(strArray[1], out result);
            if (pirates)
            {
                foreach (Empire pirateEmpire in (SyncList<Empire>)main._Game.Galaxy.PirateEmpires)
                {
                    if (pirateEmpire.BuiltObjects != null && pirateEmpire.BuiltObjects.Count > 0)
                        BaconBuiltObject.GiveAllStrategicResourcesCargoToBase(pirateEmpire.BuiltObjects[0], result);
                }
            }
            else
            {
                foreach (Empire empire in (SyncList<Empire>)main._Game.Galaxy.Empires)
                    BaconHabitat.GiveAllStrategicResourcesCargoToPlanet(empire.Capital, result);
            }
        }

        public static double CalculateLoanRate(Main main, int loanAmount)
        {
            Empire playerEmpire = main._Game.PlayerEmpire;
            int num1 = BaconEmpire.GetTotalLoanDebt() + loanAmount;
            if (num1 == 0)
                num1 = 1;
            double num2 = 6.0;
            double num3 = playerEmpire.CivilityRating / -10.0;
            double num4 = playerEmpire.CalculateSpareAnnualRevenue(0.0);
            if (num4 == 0.0)
                num4 = 1.0;
            if (main._Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
            {
                if (num4 < -50000.0)
                    num2 += 6.0;
                else if (num4 < -25000.0)
                    num2 += 3.0;
                else if (num4 < 0.0)
                    ++num2;
                if ((double)num1 / num4 > 3.0)
                    num2 += 3.0;
                else if ((double)num1 / num4 > 1.0)
                    ++num2;
                else if ((double)num1 / num4 > 0.5)
                    --num2;
                else if ((double)num1 / num4 > 0.1)
                    num2 -= 3.0;
            }
            return Math.Max(2.0, num2 + num3) / 100.0;
        }

        public static int GetMaxCreditLineRemaining(Main main) => Math.Max(0, 5000000 - BaconEmpire.GetTotalLoanDebt());

        public static void MakeLoanPayment(EventAction eventAction, GameEvent gameEvent)
        {
            try
            {
                string messageTitle = eventAction.MessageTitle;
                List<int> source = new List<int>();
                if (BaconBuiltObject.myMain._Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
                {
                    if (BaconBuiltObject.myMain._Game.PlayerEmpire.Capital != null && BaconBuiltObject.myMain._Game.PlayerEmpire.Capital.BaconValues != null)
                        source = (List<int>)BaconBuiltObject.myMain._Game.PlayerEmpire.Capital.BaconValues[messageTitle];
                }
                else if (BaconBuiltObject.myMain._Game.PlayerEmpire.BuiltObjects[0].BaconValues != null)
                    source = (List<int>)BaconBuiltObject.myMain._Game.PlayerEmpire.BuiltObjects[0].BaconValues[messageTitle];
                if (source.Count<int>() != 2)
                    return;
                int num1 = source[0];
                int num2 = source[1];
                BaconBuiltObject.myMain._Game.PlayerEmpire.StateMoney -= (double)num1;
                int num3 = num2 - 1;
                BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, "A loan payment of " + num1.ToString() + " has been made." + Environment.NewLine + "There are " + num3.ToString() + " payments remaining.", Point.Empty, "loanPayment");
                if (num3 > 0)
                {
                    List<int> intList = new List<int>();
                    intList.Add(num1);
                    intList.Add(num3);
                    if (BaconBuiltObject.myMain._Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
                        BaconBuiltObject.myMain._Game.PlayerEmpire.Capital.BaconValues[messageTitle] = (object)intList;
                    else
                        BaconBuiltObject.myMain._Game.PlayerEmpire.BuiltObjects[0].BaconValues[messageTitle] = (object)intList;
                    eventAction.ExecutionDate = BaconBuiltObject.myMain._Game.Galaxy.CurrentStarDate + (long)(Galaxy.RealSecondsInGalacticYear * 1000 / 360 * 30);
                    EventActionExecutionPackage executionPackage = new EventActionExecutionPackage(eventAction, gameEvent, BaconBuiltObject.myMain._Game.PlayerEmpire);
                    BaconBuiltObject.myMain._Game.Galaxy.DelayedActions.Add(executionPackage);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static double AnnualStateMaintenanceExcludingUnderConstruction(Empire empire)
        {
            double num1 = 0.0;
            for (int index = 0; index < empire.BuiltObjects.Count<BuiltObject>(); ++index)
            {
                BuiltObject builtObject = empire.BuiltObjects[index];
                if (builtObject.UnbuiltComponentCount <= 0 && (builtObject.BaconValues == null || !builtObject.BaconValues.ContainsKey("cash") || builtObject.IsAutoControlled))
                    num1 += (double)builtObject.AnnualSupportCost;
            }
            double num2 = num1 * empire.ShipMaintenanceSavings;
            return num1 - num2;
        }

        public static void PayAnnualMaintenanceCostForFreeTraders(Empire empire, double timePassed)
        {
            Main main = BaconBuiltObject.myMain;
            if (main == null)
                return;
            double num1 = timePassed / (double)Galaxy.RealSecondsInGalacticYear;
            for (int index = 0; index < empire.BuiltObjects.Count<BuiltObject>(); ++index)
            {
                BuiltObject builtObject = empire.BuiltObjects[index];
                if (builtObject.UnbuiltComponentCount <= 0 && builtObject.BaconValues != null && builtObject.BaconValues.ContainsKey("cash") && !builtObject.IsAutoControlled)
                {
                    int annualSupportCost = builtObject.AnnualSupportCost;
                    double num2 = (double)annualSupportCost * empire.ShipMaintenanceSavings;
                    int int32 = Convert.ToInt32(((double)annualSupportCost - num2) * num1);
                    int baconValue = (int)builtObject.BaconValues["cash"];
                    builtObject.BaconValues["cash"] = (object)((int)builtObject.BaconValues["cash"] - int32);
                    List<object[]> objArrayList = builtObject.BaconValues.ContainsKey("tradeHistory") ? (List<object[]>)builtObject.BaconValues["tradeHistory"] : new List<object[]>();
                    long currentStarDate = main._Game.Galaxy.CurrentStarDate;
                    int num3 = Galaxy.RealSecondsInGalacticYear * 1000;
                    string str = Galaxy.ResolveStarDateDescription(currentStarDate);
                    object[] objArray = new object[5]
                    {
            (object) "maintenance",
            (object) "maintenance",
            (object) "0",
            (object) int32,
            (object) str
                    };
                    objArrayList.Add(objArray);
                    builtObject.BaconValues["tradeHistory"] = (object)objArrayList;
                }
            }
        }

        public static void ResetSpyMission(CharacterList spiesToBeKilledOrCaptured, Character spy)
        {
            if (BaconBuiltObject.myMain == null || spiesToBeKilledOrCaptured.Contains(spy))
                return;
            spy.Mission = (IntelligenceMission)null;
            IntelligenceMission intelligenceMission = new IntelligenceMission(spy.Empire, spy, BaconBuiltObject.myMain._Game.Galaxy.CurrentStarDate)
            {
                TimeLength = (long)(Galaxy.RealSecondsInGalacticYear * 1000 / 4)
            };
            spy.Mission = intelligenceMission;
        }

        public static bool CheckResourceMeetsMinimumLevelBaseNotAtColony(
          Empire empire,
          Resource resource,
          int minimumResourceLevel,
          int maximumResourceLevel,
          BuiltObject baseNotAtColony,
          OrderList baseOrders,
          out int amountToOrder)
        {
            maximumResourceLevel = BaconMain.maximumResourceLevelToStockAtBaseNotAtColony;
            bool flag = false;
            int num = 0;
            int index1 = -1;
            if (baseNotAtColony.Cargo != null && baseNotAtColony.Cargo.GetExists(resource))
                index1 = baseNotAtColony.Cargo.IndexOf(resource, baseNotAtColony.Empire);
            if (index1 >= 0)
                num = Math.Max(0, baseNotAtColony.Cargo[index1].Available);
            int startIndex;
            for (int index2 = baseOrders.IndexOf(resource.ResourceID, 0); index2 >= 0; index2 = baseOrders.IndexOf(resource.ResourceID, startIndex))
            {
                int amountRequested = baseOrders[index2].AmountRequested;
                num += amountRequested;
                startIndex = index2 + 1;
            }
            amountToOrder = Math.Max(0, maximumResourceLevel - num);
            if (num >= minimumResourceLevel)
                flag = true;
            return flag;
        }

        public static void CreateNewDesignsForSelectedShipTypes(Main main, Empire empire)
        {
            DesignList selectedDesigns = main.ctlDesignsList.SelectedDesigns;
            if (selectedDesigns == null || selectedDesigns.Count<Design>() <= 0)
                return;
            foreach (Design design in (SyncList<Design>)selectedDesigns)
            {
                BuiltObjectSubRole subrole = design.SubRole;
                int designRoleToChange = empire._DesignSpecifications.IndexOf(empire._DesignSpecifications.FirstOrDefault<DesignSpecification>((Func<DesignSpecification, bool>)(x => x.SubRole == subrole)));
                if (designRoleToChange >= 0)
                    BaconEmpire.CreateNewDesigns(empire, main._Game.Galaxy.CurrentStarDate, true, designRoleToChange);
            }
        }

        public static void CreateNewDesigns(Empire empire, long designDate, bool forceUpdate) => BaconEmpire.CreateNewDesigns(empire, designDate, forceUpdate, -1);

        public static void CreateNewDesigns(
          Empire empire,
          long designDate,
          bool forceUpdate,
          int designRoleToChange = -1)
        {
            int num1 = 0;
            int num2 = empire._DesignSpecifications.Count<DesignSpecification>();
            if (designRoleToChange > -1)
            {
                num1 = designRoleToChange;
                num2 = designRoleToChange + 1;
            }
            long num3 = (long)(Galaxy.MinimumDesignReviewIntervalYears * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
            if (!empire.InitiateConstruction | forceUpdate)
                num3 = 0L;
            empire.Research.Update(empire.DominantRace);
            empire.ReviewDesignComponentsAvailable();
            BuiltObjectFleeWhen militaryFleeWhen = empire.Policy.DefaultMilitaryFleeWhen;
            BuiltObjectStance builtObjectStance1 = BuiltObjectStance.AttackEnemies;
            BuiltObjectFleeWhen builtObjectFleeWhen1 = BuiltObjectFleeWhen.EnemyMilitarySighted;
            BuiltObjectStance builtObjectStance2 = BuiltObjectStance.DoNotAttack;
            BuiltObjectFleeWhen builtObjectFleeWhen2 = BuiltObjectFleeWhen.EnemyMilitarySighted;
            BuiltObjectStance builtObjectStance3 = BuiltObjectStance.DoNotAttack;
            BuiltObjectFleeWhen builtObjectFleeWhen3 = BuiltObjectFleeWhen.EnemyMilitarySighted;
            BuiltObjectStance builtObjectStance4 = BuiltObjectStance.AttackIfAttacked;
            BuiltObjectFleeWhen builtObjectFleeWhen4 = BuiltObjectFleeWhen.EnemyMilitarySighted;
            BuiltObjectStance builtObjectStance5 = BuiltObjectStance.DoNotAttack;
            BuiltObjectFleeWhen builtObjectFleeWhen5 = BuiltObjectFleeWhen.EnemyMilitarySighted;
            BuiltObjectStance builtObjectStance6 = BuiltObjectStance.DoNotAttack;
            BuiltObjectFleeWhen builtObjectFleeWhen6 = BuiltObjectFleeWhen.Shields50;
            BuiltObjectStance builtObjectStance7 = BuiltObjectStance.AttackEnemies;
            if (empire.DominantRace.CautionLevel < 80)
                builtObjectFleeWhen6 = BuiltObjectFleeWhen.Shields20;
            int cautionLevel = empire.DominantRace.CautionLevel;
            ComponentImprovementList componentImprovementList = Galaxy.GenerateOrderedComponentImprovementList(ComponentCategoryType.WeaponTorpedo, 1);
            DesignList source1 = empire.Designs.ResolveOptimizedDesigns();
            for (int index1 = num1; index1 < num2; ++index1)
            {
                DesignSpecification designSpecification = empire._DesignSpecifications[index1];
                if (empire._ComponentsAvailable[(int)designSpecification.SubRole])
                {
                    bool flag1 = true;
                    if (designSpecification.SubRole == BuiltObjectSubRole.Carrier && !empire.CanBuildCarriers)
                        flag1 = false;
                    else if (designSpecification.SubRole == BuiltObjectSubRole.ResupplyShip && !empire.CanBuildResupplyShips)
                        flag1 = false;
                    if (flag1 && empire.CheckDesignSubRoleShouldBeUpgraded(designSpecification.SubRole))
                    {
                        Design design1 = empire.PirateEmpireBaseHabitat == null ? empire._Designs.FindNewestCanBuild(designSpecification.SubRole, empire, empire.Capital, false) : empire._Designs.FindNewestCanBuild(designSpecification.SubRole, (Empire)null, (Habitat)null, false);
                        Design design2 = (Design)null;
                        double num4 = 0.0;
                        if (source1.Count<Design>() > 0)
                        {
                            DesignList designList = source1;
                            List<BuiltObjectSubRole> subRoles = new List<BuiltObjectSubRole>();
                            subRoles.Add(designSpecification.SubRole);
                            Empire empire1 = empire;
                            DesignList designsBySubRoles = designList.GetBuildableDesignsBySubRoles(subRoles, empire1);
                            if (designsBySubRoles != null && designsBySubRoles.Count<Design>() > 0)
                            {
                                design2 = designsBySubRoles[0];
                                if (source1.Count<Design>() > 1)
                                {
                                    for (int index2 = 0; index2 < designsBySubRoles.Count<Design>(); ++index2)
                                    {
                                        Design design3 = designsBySubRoles[index2];
                                        if (design3 != null)
                                        {
                                            double techLevel = design3.CalculateTechLevel(empire, empire._Galaxy);
                                            if (techLevel > num4)
                                            {
                                                design2 = design3;
                                                num4 = techLevel;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        bool flag2 = true;
                        if (design2 != null)
                            design1 = design2;
                        long num5 = 0;
                        if (design1 != null && !design1.IsObsolete)
                        {
                            num5 = design1.DateCreated + num3;
                            empire._LatestDesigns[(int)design1.SubRole] = design1;
                        }
                        if (flag2 && empire._Galaxy.CurrentStarDate >= num5)
                        {
                            string str = Galaxy.ResolveDescription(designSpecification.SubRole);
                            string name = empire.DominantRace.Name + " " + str;
                            if ((designSpecification.SubRole != BuiltObjectSubRole.MonitoringStation || empire.Research.EvaluateDesiredComponent(ComponentType.SensorLongRange, ShipDesignFocus.Balanced) != null) && (designSpecification.SubRole != BuiltObjectSubRole.Carrier || empire.Research.EvaluateDesiredComponent(ComponentType.FighterBay, ShipDesignFocus.Balanced) != null))
                            {
                                Design design4 = new Design(name)
                                {
                                    Role = designSpecification.Role,
                                    SubRole = designSpecification.SubRole,
                                    ImageScalingType = designSpecification.ImageScalingMode,
                                    ImageScalingFactor = designSpecification.ImageScalingFactor
                                };
                                int maxShipSize = empire.MaximumConstructionSize(design4.SubRole);
                                int maxBaseSize = empire.MaximumConstructionSizeBase(design4.SubRole);
                                Design design5 = empire.PlaceComponentsOnDesign(design4, designSpecification, componentImprovementList, maxShipSize, maxBaseSize, design1);
                                if (design5 != null)
                                {
                                    switch (designSpecification.SubRole)
                                    {
                                        case BuiltObjectSubRole.Escort:
                                        case BuiltObjectSubRole.Frigate:
                                        case BuiltObjectSubRole.Destroyer:
                                        case BuiltObjectSubRole.Cruiser:
                                        case BuiltObjectSubRole.CapitalShip:
                                            design5.Stance = builtObjectStance1;
                                            design5.FleeWhen = militaryFleeWhen;
                                            design5.TacticsStrongerShips = BattleTactics.Standoff;
                                            design5.TacticsWeakerShips = BattleTactics.AllWeapons;
                                            design5.TacticsInvasion = InvasionTactics.InvadeWhenClear;
                                            break;
                                        case BuiltObjectSubRole.TroopTransport:
                                            design5.Stance = builtObjectStance7;
                                            design5.FleeWhen = builtObjectFleeWhen6;
                                            design5.TacticsStrongerShips = BattleTactics.Evade;
                                            design5.TacticsWeakerShips = BattleTactics.AllWeapons;
                                            design5.TacticsInvasion = InvasionTactics.InvadeImmediately;
                                            break;
                                        case BuiltObjectSubRole.Carrier:
                                            design5.Stance = builtObjectStance7;
                                            design5.FleeWhen = builtObjectFleeWhen6;
                                            design5.TacticsStrongerShips = BattleTactics.Evade;
                                            design5.TacticsWeakerShips = BattleTactics.AllWeapons;
                                            design5.TacticsInvasion = InvasionTactics.InvadeWhenClear;
                                            break;
                                        case BuiltObjectSubRole.ResupplyShip:
                                            design5.Stance = BuiltObjectStance.AttackEnemies;
                                            design5.FleeWhen = builtObjectFleeWhen6;
                                            design5.TacticsStrongerShips = BattleTactics.Evade;
                                            design5.TacticsWeakerShips = BattleTactics.AllWeapons;
                                            design5.TacticsInvasion = InvasionTactics.DoNotInvade;
                                            break;
                                        case BuiltObjectSubRole.ExplorationShip:
                                            design5.Stance = builtObjectStance4;
                                            design5.FleeWhen = builtObjectFleeWhen3;
                                            design5.TacticsStrongerShips = BattleTactics.Evade;
                                            design5.TacticsWeakerShips = BattleTactics.Evade;
                                            design5.TacticsInvasion = InvasionTactics.DoNotInvade;
                                            break;
                                        case BuiltObjectSubRole.SmallFreighter:
                                        case BuiltObjectSubRole.MediumFreighter:
                                        case BuiltObjectSubRole.LargeFreighter:
                                            design5.Stance = builtObjectStance2;
                                            if (design5.FirepowerRaw > 0)
                                                design5.Stance = BuiltObjectStance.AttackIfAttacked;
                                            design5.FleeWhen = builtObjectFleeWhen1;
                                            design5.TacticsStrongerShips = BattleTactics.Evade;
                                            design5.TacticsWeakerShips = BattleTactics.Evade;
                                            design5.TacticsInvasion = InvasionTactics.DoNotInvade;
                                            break;
                                        case BuiltObjectSubRole.ColonyShip:
                                        case BuiltObjectSubRole.PassengerShip:
                                            design5.Stance = builtObjectStance5;
                                            design5.FleeWhen = builtObjectFleeWhen4;
                                            design5.TacticsStrongerShips = BattleTactics.Evade;
                                            design5.TacticsWeakerShips = BattleTactics.Evade;
                                            design5.TacticsInvasion = InvasionTactics.DoNotInvade;
                                            break;
                                        case BuiltObjectSubRole.ConstructionShip:
                                            design5.Stance = builtObjectStance6;
                                            design5.FleeWhen = builtObjectFleeWhen5;
                                            design5.TacticsStrongerShips = BattleTactics.Evade;
                                            design5.TacticsWeakerShips = BattleTactics.Evade;
                                            design5.TacticsInvasion = InvasionTactics.DoNotInvade;
                                            break;
                                        case BuiltObjectSubRole.GasMiningShip:
                                        case BuiltObjectSubRole.MiningShip:
                                            design5.Stance = builtObjectStance3;
                                            design5.FleeWhen = builtObjectFleeWhen2;
                                            design5.TacticsStrongerShips = BattleTactics.Evade;
                                            design5.TacticsWeakerShips = BattleTactics.Evade;
                                            design5.TacticsInvasion = InvasionTactics.DoNotInvade;
                                            break;
                                        case BuiltObjectSubRole.GasMiningStation:
                                        case BuiltObjectSubRole.MiningStation:
                                            design5.Stance = BuiltObjectStance.AttackIfAttacked;
                                            design5.FleeWhen = BuiltObjectFleeWhen.Never;
                                            design5.TacticsStrongerShips = BattleTactics.PointBlank;
                                            design5.TacticsWeakerShips = BattleTactics.PointBlank;
                                            design5.TacticsInvasion = InvasionTactics.DoNotInvade;
                                            break;
                                        case BuiltObjectSubRole.SmallSpacePort:
                                        case BuiltObjectSubRole.MediumSpacePort:
                                        case BuiltObjectSubRole.LargeSpacePort:
                                        case BuiltObjectSubRole.ResortBase:
                                        case BuiltObjectSubRole.GenericBase:
                                        case BuiltObjectSubRole.EnergyResearchStation:
                                        case BuiltObjectSubRole.WeaponsResearchStation:
                                        case BuiltObjectSubRole.HighTechResearchStation:
                                        case BuiltObjectSubRole.MonitoringStation:
                                        case BuiltObjectSubRole.DefensiveBase:
                                            design5.Stance = BuiltObjectStance.AttackEnemies;
                                            design5.FleeWhen = BuiltObjectFleeWhen.Never;
                                            design5.TacticsStrongerShips = BattleTactics.PointBlank;
                                            design5.TacticsWeakerShips = BattleTactics.PointBlank;
                                            design5.TacticsInvasion = InvasionTactics.DoNotInvade;
                                            break;
                                        case BuiltObjectSubRole.Outpost:
                                            design5.Stance = BuiltObjectStance.DoNotAttack;
                                            design5.FleeWhen = BuiltObjectFleeWhen.Never;
                                            design5.TacticsStrongerShips = BattleTactics.PointBlank;
                                            design5.TacticsWeakerShips = BattleTactics.PointBlank;
                                            design5.TacticsInvasion = InvasionTactics.DoNotInvade;
                                            break;
                                        default:
                                            design5.Stance = BuiltObjectStance.DoNotAttack;
                                            design5.FleeWhen = BuiltObjectFleeWhen.Attacked;
                                            design5.TacticsStrongerShips = BattleTactics.Standoff;
                                            design5.TacticsWeakerShips = BattleTactics.AllWeapons;
                                            design5.TacticsInvasion = InvasionTactics.DoNotInvade;
                                            break;
                                    }
                                    if (designSpecification.TacticsStronger != 0)
                                        design5.TacticsStrongerShips = designSpecification.TacticsStronger;
                                    if (designSpecification.TacticsWeaker != 0)
                                        design5.TacticsWeakerShips = designSpecification.TacticsWeaker;
                                    if (designSpecification.TacticsInvasion != 0)
                                        design5.TacticsInvasion = designSpecification.TacticsInvasion;
                                    if (designSpecification.FleeWhen != 0)
                                        design5.FleeWhen = designSpecification.FleeWhen;
                                    Design design6 = design1;
                                    if (design1 == null)
                                        design1 = empire._Designs.FindNewest(designSpecification.SubRole);
                                    if (!design5.IsEquivalent(design1))
                                    {
                                        bool flag3 = true;
                                        if (design2 != null)
                                        {
                                            flag3 = false;
                                            if (design5.CalculateTechLevel(empire, empire._Galaxy) / num4 >= 1.5)
                                                flag3 = true;
                                        }
                                        if (design6 != null)
                                        {
                                            int size = design5.QuickCalculateSize();
                                            design5.Size = size;
                                            if (!(empire.PirateEmpireBaseHabitat == null ? empire.CanBuildDesign(design5, true, empire.Capital) : empire.CanBuildDesign(design5)))
                                                flag3 = false;
                                        }
                                        if (flag3)
                                        {
                                            string designName = empire.GenerateDesignName(designSpecification.SubRole, design1);
                                            design5.Name = designName;
                                            design5.DateCreated = designDate;
                                            design5.Empire = empire;
                                            if (empire.PirateEmpireBaseHabitat == null)
                                            {
                                                design5.PictureRef = (int)((ShipImageHelper.StandardShipImageStartIndex + empire.DesignPictureFamilyIndex * ShipImageHelper.ShipSetImageCount) + ((int)Galaxy.ResolveLegacySubRole(design5.SubRole) - (byte)1));
                                            }
                                            else
                                            {
                                                int num6 = -1;
                                                if (empire.DominantRace != null)
                                                    num6 = empire.DominantRace.DesignPictureFamilyIndexPirates;
                                                if (num6 >= 0)
                                                {
                                                    design5.PictureRef = (int)((ShipImageHelper.StandardShipImageStartIndex + num6 * ShipImageHelper.ShipSetImageCount) + ((int)Galaxy.ResolveLegacySubRole(design5.SubRole) - (byte)1));
                                                }
                                                else
                                                {
                                                    BuiltObjectSubRole subRole = design5.SubRole;
                                                    if ((uint)subRole <= 13U)
                                                    {
                                                        switch (subRole)
                                                        {
                                                            case BuiltObjectSubRole.Escort:
                                                            case BuiltObjectSubRole.Frigate:
                                                            case BuiltObjectSubRole.Destroyer:
                                                            case BuiltObjectSubRole.Cruiser:
                                                            case BuiltObjectSubRole.CapitalShip:
                                                            case BuiltObjectSubRole.ColonyShip:
                                                                break;
                                                            default:
                                                                goto label_77;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (subRole)
                                                        {
                                                            case BuiltObjectSubRole.GasMiningStation:
                                                            case BuiltObjectSubRole.MiningStation:
                                                            case BuiltObjectSubRole.SmallSpacePort:
                                                            case BuiltObjectSubRole.GenericBase:
                                                                break;
                                                            default:
                                                                goto label_77;
                                                        }
                                                    }
                                                    design5.PictureRef = ShipImageHelper.ResolveMinorShipImageIndex(design5.SubRole, false);
                                                    goto label_86;
                                                label_77:
                                                    int num7 = 0;
                                                    RaceList source2 = new RaceList();
                                                    source2.AddRange((IEnumerable<Race>)empire._Galaxy.Races.ResolvePlayableRaces());
                                                    if (empire._Galaxy.PlayerEmpire != null && empire._Galaxy.PlayerEmpire.DominantRace != null)
                                                        source2.Remove(empire._Galaxy.PlayerEmpire.DominantRace);
                                                    if (source2.Count<Race>() > 0)
                                                    {
                                                        Race race = source2[Galaxy.Rnd.Next(0, source2.Count<Race>())];
                                                        if (race != null)
                                                            num7 = race.DesignPictureFamilyIndex;
                                                        design5.PictureRef = (int)((ShipImageHelper.StandardShipImageStartIndex + num7 * ShipImageHelper.ShipSetImageCount) + ((int)Galaxy.ResolveLegacySubRole(design5.SubRole) - (byte)1));
                                                    }
                                                    else
                                                        design5.PictureRef = 0;
                                                }
                                            }
                                        label_86:
                                            design5.Role = designSpecification.Role;
                                            design5.SubRole = designSpecification.SubRole;
                                            design5.ReDefine();
                                            empire.ReviewRemoveObsoleteDesignsForSubRole(designSpecification.SubRole, (Design)null, false);
                                            empire.Designs.Add(design5);
                                            if (!design5.IsPlanetDestroyer)
                                                empire._LatestDesigns[(int)design5.SubRole] = design5;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (!empire.Policy.BuildPlanetDestroyers || !empire.CheckDesignSubRoleShouldBeUpgraded(BuiltObjectSubRole.CapitalShip))
                return;
            List<ComponentCategoryType> techFocusCategories = new List<ComponentCategoryType>();
            List<ComponentType> techFocusTypes = new List<ComponentType>();
            Galaxy.ResolveTechFocuses(empire, out techFocusCategories, out techFocusTypes);
            if (empire.SelectPreferredSuperWeapon(techFocusCategories, techFocusTypes, true) != null)
            {
                Design design7;
                if (empire.PlanetDestroyerDesignSpecification != null)
                {
                    design7 = empire.GenerateDesignFromSpec(empire.PlanetDestroyerDesignSpecification, 0.0);
                    if (design7 != null)
                    {
                        design7.Name = TextResolver.GetText("World Destroyer");
                        design7.Stance = BuiltObjectStance.AttackEnemies;
                        design7.FleeWhen = BuiltObjectFleeWhen.Shields20;
                        design7.TacticsStrongerShips = BattleTactics.Standoff;
                        design7.TacticsWeakerShips = BattleTactics.AllWeapons;
                        design7.TacticsInvasion = InvasionTactics.DoNotInvade;
                        design7.PictureRef = ShipImageHelper.PlanetDestroyer;
                    }
                }
                else
                    design7 = empire._Galaxy.GeneratePlanetDestroyerDesign(1.0, empire);
                if (design7 != null)
                {
                    bool flag = false;
                    Design design8 = (Design)null;
                    if (empire.CanBuildDesign(design7))
                    {
                        design8 = empire.Designs.FindNewestPlanetDestroyer();
                        if (design8 != null)
                        {
                            flag = false;
                            if (empire.CanBuildDesign(design8))
                            {
                                if (!design7.IsEquivalent(design8))
                                    flag = true;
                            }
                            else
                                flag = true;
                        }
                        else
                            flag = true;
                    }
                    if (flag)
                    {
                        if (design8 != null)
                        {
                            if (design8.BuildCount <= 0)
                                empire.Designs.Remove(design8);
                            else
                                design8.IsObsolete = true;
                        }
                        empire.Designs.Add(design7);
                    }
                }
            }
        }

        public static void DoTaskPiratesLongInterval(Empire empire)
        {
            Main main = BaconBuiltObject.myMain;
            if (main == null || empire == main._Game.PlayerEmpire)
                return;
            BaconEmpire.CheckPiratesBuildConstructionShipsAtIndependentPlanet(main, empire);
            BaconEmpire.CheckPirateReinforcePirateBases(main, empire);
        }

        public static void CheckPiratesBuildConstructionShipsAtIndependentPlanet(
          Main main,
          Empire empire)
        {
            if ((double)BaconHabitat.pirateControlLevelToBuildShipsAtIndependentPlanets > 100.0 || empire.BuiltObjects.Count<BuiltObject>((Func<BuiltObject, bool>)(x => x.SubRole == BuiltObjectSubRole.ConstructionShip && x.WarpSpeed >= 1000 && x.DamagedComponentCount < 10)) > 20)
                return;
            double stateMoney = empire.StateMoney;
            Design newestCanBuild = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ConstructionShip);
            if (newestCanBuild == null)
                return;
            double num1 = newestCanBuild.CalculateCurrentPurchasePrice(main._Game.Galaxy);
            if (num1 < 1000.0)
                num1 = 1000.0;
            double num2 = stateMoney / num1;
            Random random = new Random();
            if ((double)random.Next(0, 100) >= num2)
                return;
            List<Habitat> habitatList = new List<Habitat>();
            foreach (Habitat colony in (SyncList<Habitat>)empire.Colonies)
            {
                PirateColonyControl pirateColonyControl = colony.GetPirateControl().FirstOrDefault<PirateColonyControl>((Func<PirateColonyControl, bool>)(x => (int)x.EmpireId == empire.EmpireId));
                if (pirateColonyControl != null && (double)pirateColonyControl.ControlLevel > (double)BaconHabitat.pirateControlLevelToBuildShipsAtIndependentPlanets && main._Game.Galaxy.DetermineDefendingFirepower(colony, empire) > 250)
                    habitatList.Add(colony);
            }
            if (habitatList.Count > 0)
            {
                int index = random.Next(0, habitatList.Count);
                Habitat planet = habitatList[index];
                BaconHabitat.BuildShipForPirate(planet, empire, BuiltObjectSubRole.ConstructionShip);
                if (BaconMain.isDebugging)
                    main._Game.PlayerEmpire.SendMessageToEmpire(main._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, "*** " + empire.Name + " is building a ship + at " + planet.Name);
            }
        }

        public static void CheckPirateReinforcePirateBases(Main main, Empire pirateEmpire)
        {
            if (pirateEmpire == main._Game.PlayerEmpire)
                return;
            double stateMoney = pirateEmpire.StateMoney;
            List<Habitat> habitatList = new List<Habitat>();
            foreach (Habitat colony in (SyncList<Habitat>)pirateEmpire.Colonies)
            {
                if (colony.Owner != pirateEmpire && colony.Facilities != null && colony.Facilities.FindBestPirateFacility(true) != null && colony.GetPirateControl().FirstOrDefault<PirateColonyControl>((Func<PirateColonyControl, bool>)(x => (int)x.EmpireId == pirateEmpire.EmpireId)) != null)
                    habitatList.Add(colony);
            }
            if (habitatList.Count <= 0)
                return;
            Random random = new Random();
            int index = random.Next(0, habitatList.Count);
            Habitat habitat = habitatList[index];
            int val1 = 125000;
            int num1;
            try
            {
                num1 = Math.Max(0, Math.Min(val1, (int)(random.NextDouble() * pirateEmpire.StateMoney / 10.0)));
            }
            catch (Exception ex)
            {
                num1 = 0;
            }
            if (num1 <= 0)
                return;
            int num2 = 0;
            if (habitat.BaconValues == null)
                habitat.BaconValues = new Dictionary<string, object>();
            if (habitat.BaconValues.ContainsKey("piratebase"))
                num2 = (int)habitat.BaconValues["piratebase"];
            else
                habitat.BaconValues.Add("piratebase", (object)0);
            habitat.BaconValues["piratebase"] = (object)(num2 + num1);
        }

        public static void ChangePirateEmpireToRegularEmpire(Main main, Empire empire)
        {
            if (empire.PirateEmpireBaseHabitat == null)
                BaconBuiltObject.ShowMessageBox(main, empire.Name + " is not a pirate empire.", "Not a pirate");
            else if (!empire.CheckPirateEmpireHasCriminalNetwork(empire))
            {
                BaconBuiltObject.ShowMessageBox(main, "You do not have a criminal network.", "No network");
            }
            else
            {
                main._Game.PlayAsAPirate = false;
                empire.PirateEmpireBaseHabitat = (Habitat)null;
                main._Game.Galaxy.Empires.Add(empire);
                main._Game.Galaxy.PirateEmpires.RemoveAt(0);
                List<Habitat> habitatList = new List<Habitat>();
                foreach (Habitat colony in (SyncList<Habitat>)empire.Colonies)
                {
                    List<PlanetaryFacility> planetaryFacilityList = new List<PlanetaryFacility>();
                    foreach (PlanetaryFacility facility in (SyncList<PlanetaryFacility>)colony.Facilities)
                    {
                        if (facility.Type == PlanetaryFacilityType.PirateBase || facility.Type == PlanetaryFacilityType.PirateFortress || facility.Type == PlanetaryFacilityType.PirateCriminalNetwork)
                            planetaryFacilityList.Add(facility);
                    }
                    for (int index = 0; index < planetaryFacilityList.Count; ++index)
                    {
                        if (colony.Facilities.Contains(planetaryFacilityList[index]))
                            colony.Facilities.Remove(planetaryFacilityList[index]);
                    }
                    if (colony.Owner != empire)
                        habitatList.Add(colony);
                }
                for (int index = 0; index < habitatList.Count; ++index)
                {
                    if (empire.Colonies.Contains(habitatList[index]))
                        empire.Colonies.Remove(habitatList[index]);
                }
                foreach (Habitat colony in (SyncList<Habitat>)empire.Colonies)
                {
                    colony.RecalculateColonyInfluenceRadius(true);
                    colony.RecalculateDistanceFactor();
                    colony.RecalculateAnnualTaxRevenue();
                }
                foreach (BuiltObject builtObject in (SyncList<BuiltObject>)empire.BuiltObjects)
                {
                    builtObject.Empire = builtObject.ActualEmpire;
                    builtObject.PirateEmpireId = (byte)0;
                }
                foreach (BuiltObject privateBuiltObject in (SyncList<BuiltObject>)empire.PrivateBuiltObjects)
                {
                    privateBuiltObject.Empire = privateBuiltObject.ActualEmpire;
                    privateBuiltObject.PirateEmpireId = (byte)0;
                }
                empire.CivilityRating += 50.0;
                empire.ChangeGovernment(0);
                if (empire.Capital != null && empire.Capital.BaconValues == null)
                    empire.Capital.BaconValues = new Dictionary<string, object>();
                if (empire.BuiltObjects != null && empire.BuiltObjects.Count > 0 && empire.BuiltObjects[0].BaconValues != null)
                {
                    foreach (KeyValuePair<string, object> keyValuePair in new Dictionary<string, object>((IDictionary<string, object>)empire.BuiltObjects[0].BaconValues))
                    {
                        if (BaconMain.baconValuesToCopyOnChangingCapital.Contains(keyValuePair.Key) && !empire.Capital.BaconValues.ContainsKey(keyValuePair.Key))
                        {
                            empire.Capital.BaconValues.Add(keyValuePair.Key, keyValuePair.Value);
                            empire.BuiltObjects[0].BaconValues.Remove(keyValuePair.Key);
                        }
                    }
                }
                empire.Characters.FirstOrDefault<Character>((Func<Character, bool>)(x => x.Role == CharacterRole.PirateLeader)).Role = CharacterRole.Leader;
                main._Game.Victor = (Empire)null;
                main._Game.IsFinished = false;
            }
        }

        public static void PirateRecalculateEmpireCorruption(Empire empire)
        {
            double pirateIncome = empire.CalculatePirateIncome();
            double num1 = pirateIncome;
            if (num1 > 25000.0)
                num1 = Math.Min(pirateIncome, Math.Sqrt(pirateIncome / 1000.0) * 5000.0);
            double num2 = num1 + num1 * (0.05 * (Galaxy.Rnd.NextDouble() - 1.0));
            double num3 = 1.0;
            if (pirateIncome > 0.0)
                num3 = Math.Min(1.0, Math.Max(0.0, num2 / pirateIncome));
            double num4 = (1.0 - num3) * empire.ColonyCorruptionFactor;
            if (num4 > 0.9)
                num4 = 0.9;
            empire.Corruption = num4;
        }

        public static Habitat DetermineResettleDestination(
          Empire empireBeingProcessed,
          Race race,
          BuiltObject passengerShip,
          Habitat sourceColony)
        {
            PrioritizedTargetList source = new PrioritizedTargetList();
            bool flag = empireBeingProcessed.CheckShipCanSurviveStorms(passengerShip);
            double num1 = passengerShip.MaximumFuelRange();
            double num2 = num1 * num1;
            for (int index1 = 0; index1 < empireBeingProcessed._Galaxy.Empires.Count<Empire>(); ++index1)
            {
                Empire empire = empireBeingProcessed._Galaxy.Empires[index1];
                DiplomaticRelation diplomaticRelation = empireBeingProcessed.ObtainDiplomaticRelation(empire);
                if ((empire == empireBeingProcessed || diplomaticRelation.Type != DiplomaticRelationType.NotMet && diplomaticRelation.Type != DiplomaticRelationType.War && diplomaticRelation.Type != DiplomaticRelationType.TradeSanctions) && empire.DominantRace != null && empire.DominantRace.Expanding)
                {
                    for (int index2 = 0; index2 < empire.Colonies.Count<Habitat>(); ++index2)
                    {
                        Habitat colony = empire.Colonies[index2];
                        if (colony.Population != null)
                        {
                            long totalAmount = colony.Population.TotalAmount;
                            if (colony.MaximumPopulation == totalAmount)
                                continue;
                        }
                        if (empireBeingProcessed.CheckSystemExplored(colony.SystemIndex) && colony.AcceptsPopulation(empireBeingProcessed, race) && (flag || !empireBeingProcessed._Galaxy.CheckInStorm(colony.Xpos, colony.Ypos)))
                        {
                            double distanceSquared = empireBeingProcessed._Galaxy.CalculateDistanceSquared(colony.Xpos, colony.Ypos, sourceColony.Xpos, sourceColony.Ypos);
                            if (distanceSquared < num2)
                                source.Add(new PrioritizedTarget(colony, (int)(distanceSquared / 1000000.0)));
                        }
                    }
                }
            }
            for (int index = 0; index < empireBeingProcessed._Galaxy.IndependentColonies.Count<Habitat>(); ++index)
            {
                Habitat independentColony = empireBeingProcessed._Galaxy.IndependentColonies[index];
                if (empireBeingProcessed.CheckSystemExplored(independentColony.SystemIndex) && independentColony.AcceptsPopulation(empireBeingProcessed, race) && (flag || !empireBeingProcessed._Galaxy.CheckInStorm(independentColony.Xpos, independentColony.Ypos)))
                {
                    double distanceSquared = empireBeingProcessed._Galaxy.CalculateDistanceSquared(independentColony.Xpos, independentColony.Ypos, sourceColony.Xpos, sourceColony.Ypos);
                    if (distanceSquared < num2)
                        source.Add(new PrioritizedTarget(independentColony, (int)(distanceSquared / 1000000.0)));
                }
            }
            source.Sort();
            return source.Count<PrioritizedTarget>() > 0 ? (Habitat)source[0].Target : (Habitat)null;
        }

        public static PrioritizedTargetList DetermineMigrationDestinations(Empire empire)
        {
            PrioritizedTargetList migrationDestinations = new PrioritizedTargetList();
            for (int index = 0; index < empire.Colonies.Count<Habitat>(); ++index)
            {
                Habitat colony = empire.Colonies[index];
                if (colony != null && colony.Empire == empire)
                {
                    if (colony.Population != null)
                    {
                        long totalAmount = colony.Population.TotalAmount;
                        if (colony.MaximumPopulation == totalAmount)
                            continue;
                    }
                    if ((double)colony.MigrationFactor > 0.0)
                        migrationDestinations.Add(new PrioritizedTarget(colony, (int)((double)colony.MigrationFactor * 1000.0)));
                    else if (empire.PenalColonies.Contains(colony))
                        migrationDestinations.Add(new PrioritizedTarget(colony, 500));
                }
            }
            migrationDestinations.Sort();
            migrationDestinations.Reverse();
            return migrationDestinations;
        }

        public static void TryRefillScientifiData(Main main, BuiltObject ship)
        {
            Empire actualEmpire = ship.ActualEmpire;
            if (actualEmpire == null)
                return;
            if (actualEmpire.PirateEmpireBaseHabitat == null)
            {
                Habitat capital = actualEmpire.Capital;
                if (capital == null || !BaconHabitat.CheckAndCreateBaconValuesKey(capital, "scientificData"))
                    return;
                int num = Math.Min((int)capital.BaconValues["scientificData"], 100);
                if (num <= 0)
                    return;
                ship.BaconValues["scientificData"] = (object)((int)ship.BaconValues["scientificData"] + num);
                capital.BaconValues["scientificData"] = (object)((int)capital.BaconValues["scientificData"] - num);
            }
            else
            {
                if (actualEmpire.BuiltObjects == null || actualEmpire.BuiltObjects.Count < 1)
                    return;
                BuiltObject builtObject = actualEmpire.BuiltObjects[0];
                if (builtObject == null || !BaconBuiltObject.CheckAndCreateBaconValuesKey(builtObject, "scientificData"))
                    return;
                int num = Math.Min((int)builtObject.BaconValues["scientificData"], 100);
                if (num <= 0)
                    return;
                ship.BaconValues["scientificData"] = (object)((int)ship.BaconValues["scientificData"] + num);
                builtObject.BaconValues["scientificData"] = (object)((int)builtObject.BaconValues["scientificData"] - num);
            }
        }

        public static void StoreScientificData(Main main, BuiltObject ship)
        {
            Empire actualEmpire = ship.ActualEmpire;
            if (actualEmpire == null)
                return;
            if (actualEmpire.PirateEmpireBaseHabitat == null)
            {
                Habitat capital = actualEmpire.Capital;
                if (capital == null)
                    return;
                BaconHabitat.CheckAndCreateBaconValuesKey(capital, "scientificData");
                int num = Math.Max((int)ship.BaconValues["scientificData"] - 100, 0);
                if (num <= 0)
                    return;
                ship.BaconValues["scientificData"] = (object)((int)ship.BaconValues["scientificData"] - num);
                capital.BaconValues["scientificData"] = (object)((int)capital.BaconValues["scientificData"] + num);
            }
            else
            {
                if (actualEmpire.BuiltObjects == null || actualEmpire.BuiltObjects.Count < 1)
                    return;
                BuiltObject builtObject = actualEmpire.BuiltObjects[0];
                if (builtObject == null)
                    return;
                BaconBuiltObject.CheckAndCreateBaconValuesKey(builtObject, "scientificData");
                int num = Math.Max((int)ship.BaconValues["scientificData"] - 100, 0);
                if (num <= 0)
                    return;
                ship.BaconValues["scientificData"] = (object)((int)ship.BaconValues["scientificData"] - num);
                builtObject.BaconValues["scientificData"] = (object)((int)builtObject.BaconValues["scientificData"] + num);
            }
        }

        public static double PrivateConstructionAddToInfrastructure(Empire empire, double cost)
        {
            double num = (1.0 - BaconBuiltObject.privateBuildCostToStateMoney) * cost;
            double infrastructure = BaconBuiltObject.privateBuildCostToStateMoney * cost;
            try
            {
                if (BaconMain.isDebugging)
                    BaconBuiltObject.myMain._Game.Galaxy.Pause();
                StellarObjectList coloniesToExclude = new StellarObjectList();
                foreach (BuiltObject spacePort in (SyncList<BuiltObject>)empire.SpacePorts)
                {
                    if (spacePort.BaseShipyardDisabled)
                        coloniesToExclude.Add((StellarObject)spacePort);
                }
                Habitat habitat = empire.SelectRandomSpacePortColony(coloniesToExclude);
                if (habitat != null)
                {
                    if (habitat.BaconValues == null)
                        habitat.BaconValues = new Dictionary<string, object>();
                    if (habitat.BaconValues.ContainsKey("infrastructure"))
                    {
                        int baconValue = (int)habitat.BaconValues["infrastructure"];
                        habitat.BaconValues["infrastructure"] = (object)(baconValue + (int)num);
                    }
                    else
                        habitat.BaconValues.Add("infrastructure", (object)(int)num);
                }
            }
            catch (Exception ex)
            {
                if (BaconMain.isDebugging)
                    BaconBuiltObject.myMain._Game.Galaxy.Pause();
            }
            return infrastructure;
        }

        public static void TakePossessionOfBuiltObject(
          Empire empire,
          BuiltObject itemReceived,
          Empire receivingEmpire)
        {
            if (itemReceived == null || !(new StackFrame(2).GetMethod().Name == "GiveTradeableItem") || itemReceived.Role != BuiltObjectRole.Base)
                return;
            if (itemReceived.BaconValues == null)
                itemReceived.BaconValues = new Dictionary<string, object>();
            if (!itemReceived.BaconValues.ContainsKey("givenInTrade"))
                itemReceived.BaconValues.Add("givenInTrade", (object)receivingEmpire.Name);
        }

        public static int CalculateSystemCompetition(
          Empire myEmpire,
          Empire otherEmpire,
          HabitatList ourSystemStars)
        {
            int num1 = 0;
            double num2 = 1.0 + Math.Max(0.0, myEmpire.ObtainEmpireEvaluation(otherEmpire).Bias / -10.0);
            foreach (Habitat empireSystem in (SyncList<Habitat>)myEmpire.DetermineEmpireSystems(otherEmpire))
            {
                if (ourSystemStars.IndexOf(empireSystem) >= 0)
                    num1 -= Galaxy.SystemCompetitionColonyFactor;
            }
            if (!myEmpire.ObtainDiplomaticRelation(otherEmpire).MiningRightsToOther)
            {
                for (int index = 0; index < otherEmpire.MiningStations.Count; ++index)
                {
                    BuiltObject miningStation = otherEmpire.MiningStations[index];
                    if (miningStation.ParentHabitat != null && (miningStation.BaconValues == null || !miningStation.BaconValues.ContainsKey("givenInTrade")))
                    {
                        Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(miningStation.ParentHabitat);
                        if (ourSystemStars.IndexOf(habitatSystemStar) >= 0)
                            num1 -= Galaxy.SystemCompetitionMiningStationFactor;
                    }
                }
            }
            double num3 = Math.Max((double)myEmpire.DominantRace.CautionLevel / 100.0, 0.9);
            return Math.Max((int)((double)num1 * (num3 * num3) * num2), -20);
        }

        public static int CalculateEnvy(Empire thisEmpire, Empire otherEmpire, int envy)
        {
            try
            {
                if (otherEmpire.ObtainDiplomaticRelation(thisEmpire).Type == DiplomaticRelationType.MutualDefensePact)
                    envy = 0;
            }
            catch (Exception ex)
            {
                if (BaconBuiltObject.myMain != null)
                    BaconBuiltObject.myMain._Game.Galaxy.Pause();
            }
            return envy;
        }

        public static void CheckColoniesForPirateFacilitiesAndAttack(Empire empire)
        {
            if (empire == empire._Galaxy.IndependentEmpire || empire.PirateEmpireBaseHabitat != null || empire.Colonies == null)
                return;
            for (int index = 0; index < empire.Colonies.Count; ++index)
            {
                Habitat colony = empire.Colonies[index];
                if (colony != null && !colony.HasBeenDestroyed && colony.Empire == empire)
                {
                    Empire pirateFaction = (Empire)null;
                    PlanetaryFacility attack = colony.CheckPirateFacilityToAttack(out pirateFaction);
                    if (attack != null && pirateFaction != null)
                    {
                        int num1 = 0;
                        switch (attack.Type)
                        {
                            case PlanetaryFacilityType.PirateBase:
                                num1 = 1 + (int)((double)BaconHabitat.pirateBaseTroops * 0.67000001668930054);
                                break;
                            case PlanetaryFacilityType.PirateFortress:
                                num1 = 1 + (int)((double)BaconHabitat.pirateFortressTroops * 0.67000001668930054);
                                break;
                            case PlanetaryFacilityType.PirateCriminalNetwork:
                                num1 = 1 + (int)((double)BaconHabitat.pirateCriminalNetworkTroops * 0.67000001668930054);
                                break;
                        }
                        int num2 = num1 * 50 * 100;
                        if (colony.Troops != null && colony.Troops.TotalAttackStrength > num2)
                        {
                            int refusalCount = 0;
                            if (empire.CheckTaskAuthorized(empire.ControlMilitaryAttacks, ref refusalCount, empire.GenerateAutomationMessageAttackPirateFacility(colony, pirateFaction, attack), (object)colony, AdvisorMessageType.PirateFacilityEradicate, (object)attack, (object)pirateFaction))
                                colony.InitiateAttackAgainstPirateFacilities(empire, attack);
                        }
                        else if (empire.ControlMilitaryAttacks == AutomationLevel.FullyAutomated && !empire.CheckAtWar())
                        {
                            if (empire.ColoniesNeedingTroops == null)
                                empire.ColoniesNeedingTroops = new HabitatList();
                            if (!empire.ColoniesNeedingTroops.Contains(colony))
                                empire.ColoniesNeedingTroops.Add(colony);
                        }
                    }
                }
            }
        }
    }
}
