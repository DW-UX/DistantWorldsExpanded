using BaconDistantWorlds;

using DistantWorlds.Types.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace DistantWorlds.Types
{
    public partial class Empire
    {
        private IntelligenceMissionOutcome DetermineIntelligenceMissionOutcome(IntelligenceMission mission, Character agent)
        {
            return BaconEmpire.DetermineIntelligenceMissionOutcome(this, mission, agent);
        }

        public double CalculateIntelligenceMissionSuccessChance(IntelligenceMission mission, Character agent)
        {
            double result = 0.0;
            if (mission != null && agent != null)
            {
                long num = Galaxy.RealSecondsInGalacticYear * 1000 / 12;
                long num2 = Galaxy.RealSecondsInGalacticYear * 1000 / 4;
                long num3 = Galaxy.RealSecondsInGalacticYear * 1000;
                int num4 = 1;
                if (mission.TimeLength <= num)
                {
                    num4 = 1;
                }
                else if (mission.TimeLength <= num2)
                {
                    num4 = 2;
                }
                else if (mission.TimeLength <= num3)
                {
                    num4 = 4;
                }
                int num5 = 0;
                switch (mission.Type)
                {
                    case IntelligenceMissionType.CounterIntelligence:
                        num5 = agent.CounterEspionageFactored;
                        break;
                    case IntelligenceMissionType.DeepCover:
                        num5 = agent.ConcealmentFactored;
                        break;
                    case IntelligenceMissionType.StealGalaxyMap:
                    case IntelligenceMissionType.StealOperationsMap:
                    case IntelligenceMissionType.StealTechData:
                    case IntelligenceMissionType.StealTerritoryMap:
                        num5 = agent.EspionageFactored;
                        if (mission.Type == IntelligenceMissionType.StealTechData && mission.TargetEmpire != null && mission.TargetEmpire.Characters != null)
                        {
                            if (mission.TargetEmpire.Characters.CheckCharactersForTrait(CharacterRole.Scientist, CharacterTraitType.ForeignSpy))
                            {
                                num5 *= 2;
                            }
                            else if (mission.TargetEmpire.Characters.CheckCharactersForTrait(CharacterRole.Scientist, CharacterTraitType.Patriot))
                            {
                                num5 /= 2;
                            }
                        }
                        break;
                    case IntelligenceMissionType.InciteRevolution:
                        num5 = agent.PsyOpsFactored;
                        break;
                    case IntelligenceMissionType.SabotageConstruction:
                    case IntelligenceMissionType.SabotageColony:
                    case IntelligenceMissionType.DestroyBase:
                        num5 = agent.SabotageFactored;
                        break;
                    case IntelligenceMissionType.AssassinateCharacter:
                        num5 = agent.AssassinationFactored;
                        break;
                }
                double num6 = CalculateIntelligenceMissionBonusFromLeaderAndAmbassador(mission.Type, mission.TargetEmpire);
                num5 = (int)((double)num5 * num6);
                double num7 = (double)num5 * (double)num4;
                num7 *= 1.0 + EspionageBonus;
                double num8 = num7 / (double)mission.Difficulty;
                result = ((!(num8 > 1.0)) ? (0.7 * num8) : (1.0 - 0.3 / num8));
            }
            return result;
        }

        public void CancelIntelligenceMission(IntelligenceMission mission)
        {
            IntelligenceMissionType type = mission.Type;
            if (type != IntelligenceMissionType.DeepCover)
            {
                return;
            }
            int num = 0;
            bool flag = false;
            int num2 = _EmpiresViewable.IndexOf(mission.TargetEmpire);
            while (num2 >= 0 && _EmpiresViewableExpiry.Count > num2 && !flag && num < 10)
            {
                long num3 = _EmpiresViewableExpiry[num2];
                if (num3 == long.MaxValue)
                {
                    _EmpiresViewable.RemoveAt(num2);
                    _EmpiresViewableExpiry.RemoveAt(num2);
                    flag = true;
                }
                else if (_EmpiresViewable.Count > num2 + 1)
                {
                    num2 = _EmpiresViewable.IndexOf(mission.TargetEmpire, num2 + 1);
                }
                num++;
            }
        }

        private void CompleteIntelligenceMission(IntelligenceMission mission)
        {
            switch (mission.Type)
            {
                case IntelligenceMissionType.DeepCover:
                    {
                        long item = long.MaxValue;
                        _EmpiresViewable.Add(mission.TargetEmpire);
                        _EmpiresViewableExpiry.Add(item);
                        break;
                    }
                case IntelligenceMissionType.InciteRevolution:
                    if (mission.TargetEmpire.PirateEmpireBaseHabitat == null)
                    {
                        mission.TargetEmpire.HaveRevolution(mission.TargetEmpire.DominantRace);
                    }
                    break;
                case IntelligenceMissionType.AssassinateCharacter:
                    if (mission.Target is Character)
                    {
                        Character character = (Character)mission.Target;
                        if (character != null && character.Active)
                        {
                            character.SendDeathMessage(CharacterDeathType.Assassination, _Galaxy);
                            character.Kill(_Galaxy);
                        }
                    }
                    break;
                case IntelligenceMissionType.DestroyBase:
                    {
                        if (!(mission.Target is BuiltObject))
                        {
                            break;
                        }
                        BuiltObject builtObject2 = (BuiltObject)mission.Target;
                        if (builtObject2 == null || builtObject2.HasBeenDestroyed)
                        {
                            break;
                        }
                        if (builtObject2.Empire != null)
                        {
                            Habitat habitat4 = null;
                            if (builtObject2.ParentHabitat != null)
                            {
                                habitat4 = Galaxy.DetermineHabitatSystemStar(builtObject2.ParentHabitat);
                            }
                            else if (builtObject2.NearestSystemStar != null)
                            {
                                habitat4 = builtObject2.NearestSystemStar;
                            }
                            string arg2 = string.Empty;
                            if (habitat4 != null)
                            {
                                arg2 = habitat4.Name;
                            }
                            string description2 = string.Format(TextResolver.GetText("Base Destroyed Sabotage Description"), builtObject2.Name, arg2);
                            string title2 = TextResolver.GetText("Base Destroyed Sabotage") + "!";
                            builtObject2.Empire.SendMessageToEmpire(builtObject2.Empire, EmpireMessageType.BattleUnderAttack, builtObject2, description2, new Point((int)builtObject2.Xpos, (int)builtObject2.Ypos), string.Empty, title2);
                        }
                        builtObject2.InflictDamage(builtObject2, null, 100000.0, _Galaxy.CurrentDateTime, _Galaxy, 0f, allowRecursion: true, 0.0, allowArmorInvulnerability: false);
                        break;
                    }
                case IntelligenceMissionType.SabotageColony:
                    {
                        if (mission.TargetEmpire == null || mission.TargetEmpire.PirateEmpireBaseHabitat != null)
                        {
                            break;
                        }
                        Habitat habitat = (Habitat)mission.Target;
                        if (habitat.Population != null && habitat.Population.Count > 0)
                        {
                            int num3 = Galaxy.Rnd.Next(0, 20);
                            long num4 = habitat.Population.TotalAmount / 15;
                            if (num4 > 2000000000)
                            {
                                num4 = 2000000000L;
                            }
                            int num5 = Galaxy.Rnd.Next(0, (int)num4);
                            int val = habitat.GetDevelopmentLevel() - num3;
                            val = Math.Max(val, 0);
                            habitat.SetDevelopmentLevel(val);
                            habitat.Population[0].Amount -= num5;
                            if (habitat.Population[0].Amount < 10000000)
                            {
                                habitat.Population[0].Amount = 10000000L;
                            }
                            habitat.Population.RecalculateTotalAmount();
                            habitat.HappinessModifier = (float)(-15.0 + Galaxy.Rnd.NextDouble() * -10.0);
                            habitat.StartRebelling();
                            _Galaxy.DoCharacterEvent(CharacterEventType.ColonyDevelopmentDecrease, habitat, habitat.Characters, includeLeader: true, habitat.Empire);
                        }
                        break;
                    }
                case IntelligenceMissionType.SabotageConstruction:
                    {
                        ConstructionQueue constructionQueue = null;
                        Empire empire = null;
                        Habitat habitat2 = null;
                        StellarObject stellarObject = null;
                        if (mission.Target is Habitat)
                        {
                            Habitat habitat3 = (Habitat)mission.Target;
                            constructionQueue = habitat3.ConstructionQueue;
                            empire = habitat3.Empire;
                            habitat2 = Galaxy.DetermineHabitatSystemStar(habitat3);
                            stellarObject = habitat3;
                        }
                        else if (mission.Target is BuiltObject)
                        {
                            BuiltObject builtObject = (BuiltObject)mission.Target;
                            constructionQueue = builtObject.ConstructionQueue;
                            empire = builtObject.Empire;
                            habitat2 = builtObject.NearestSystemStar;
                            stellarObject = builtObject;
                        }
                        bool flag = false;
                        if (constructionQueue != null && constructionQueue.ConstructionYards != null && constructionQueue.ConstructionYards.Count > 0)
                        {
                            for (int i = 0; i < constructionQueue.ConstructionYards.Count; i++)
                            {
                                ConstructionYard constructionYard = constructionQueue.ConstructionYards[i];
                                if (constructionYard.ShipUnderConstruction == null)
                                {
                                    continue;
                                }
                                if (Galaxy.Rnd.Next(0, 4) == 1)
                                {
                                    for (int j = 0; j < constructionYard.ShipUnderConstruction.Components.Count; j++)
                                    {
                                        BuiltObjectComponent builtObjectComponent = constructionYard.ShipUnderConstruction.Components[j];
                                        if (builtObjectComponent.Status == ComponentStatus.Normal && Galaxy.Rnd.Next(0, 6) > 1)
                                        {
                                            builtObjectComponent.Status = ComponentStatus.Damaged;
                                            flag = true;
                                        }
                                    }
                                }
                                else
                                {
                                    BuiltObject shipUnderConstruction = constructionYard.ShipUnderConstruction;
                                    shipUnderConstruction.InflictDamage(shipUnderConstruction, null, 10000.0, _Galaxy.CurrentDateTime, _Galaxy, 0f, allowRecursion: false, double.MinValue, allowArmorInvulnerability: false);
                                    flag = true;
                                }
                            }
                        }
                        if (flag && empire != null && stellarObject != null)
                        {
                            string arg = string.Empty;
                            if (habitat2 != null)
                            {
                                arg = habitat2.Name;
                            }
                            string description = string.Format(TextResolver.GetText("Construction Sabotaged Description"), stellarObject.Name, arg);
                            string title = TextResolver.GetText("Construction Sabotaged") + "!";
                            empire.SendMessageToEmpire(empire, EmpireMessageType.BattleUnderAttack, stellarObject, description, new Point((int)stellarObject.Xpos, (int)stellarObject.Ypos), string.Empty, title);
                        }
                        break;
                    }
                case IntelligenceMissionType.StealTerritoryMap:
                    _Galaxy.GiveTerritoryMap(mission.TargetEmpire, this);
                    break;
                case IntelligenceMissionType.StealGalaxyMap:
                    _Galaxy.MergeGalaxyMap(mission.TargetEmpire, this);
                    break;
                case IntelligenceMissionType.StealOperationsMap:
                    {
                        long item2 = _Galaxy.CurrentStarDate + 30000;
                        _EmpiresViewable.Add(mission.TargetEmpire);
                        _EmpiresViewableExpiry.Add(item2);
                        break;
                    }
                case IntelligenceMissionType.StealTechData:
                    {
                        ResearchNode researchNode = null;
                        if (mission.Target is ResearchNode)
                        {
                            researchNode = (ResearchNode)mission.Target;
                            if (researchNode == null)
                            {
                                ResearchNodeList researchNodeList = Research.ResolveMoreAdvancedProjects(mission.TargetEmpire, includeSpecialTech: false);
                                if (researchNodeList != null && researchNodeList.Count > 0)
                                {
                                    int index = Galaxy.Rnd.Next(0, researchNodeList.Count);
                                    researchNode = researchNodeList[index];
                                }
                            }
                        }
                        else
                        {
                            ResearchNodeList researchNodeList2 = Research.ResolveMoreAdvancedProjects(mission.TargetEmpire, includeSpecialTech: false);
                            if (researchNodeList2 != null && researchNodeList2.Count > 0)
                            {
                                int index2 = Galaxy.Rnd.Next(0, researchNodeList2.Count);
                                researchNode = researchNodeList2[index2];
                            }
                        }
                        if (researchNode == null)
                        {
                            break;
                        }
                        ResearchNode equivalent = Research.TechTree.GetEquivalent(researchNode);
                        if (equivalent != null)
                        {
                            float num = (float)((double)_Galaxy.BaseTechCost * 0.5 * ((double)mission.Agent.EspionageFactored / 25.0));
                            if (mission.Agent != null)
                            {
                                num *= (float)((double)mission.Agent.EspionageFactored / 25.0);
                            }
                            float num2 = 1f;
                            if (equivalent.AllowedRaces != null && equivalent.AllowedRaces.Count > 0 && !equivalent.AllowedRaces.Contains(DominantRace))
                            {
                                num2 = 2f;
                            }
                            num /= num2;
                            equivalent.Progress += num;
                            if (equivalent.Progress >= equivalent.Cost)
                            {
                                DoResearchBreakthrough(equivalent, selfResearched: true, blockMessages: false, suppressUpdate: false);
                            }
                            mission.ResetResearchProject(equivalent);
                        }
                        break;
                    }
                case IntelligenceMissionType.CounterIntelligence:
                    break;
            }
        }

        public int NewAgentsCanRecruit()
        {
            int num = 0;
            int desiredIntelligenceAgentAmount = DesiredIntelligenceAgentAmount;
            CharacterList characterList = new CharacterList();
            for (int i = 0; i < Characters.Count; i++)
            {
                Character character = Characters[i];
                if (character.Role == CharacterRole.IntelligenceAgent)
                {
                    characterList.Add(character);
                }
            }
            if (characterList.Count < desiredIntelligenceAgentAmount)
            {
                double num2 = CalculateAccurateAnnualIncome();
                double num3 = Math.Max(AnnualStateMaintenance, MinimumShipSpending);
                double num4 = Math.Max(AnnualTroopMaintenanceIncludeRecruiting, MinimumTroopSpending);
                double num5 = AnnualAgentMaintenance + num3 + num4 + AnnualSubjugationTribute + AnnualPirateProtection;
                double num6 = num2 - num5;
                if (num6 > 0.0)
                {
                    num = (int)(num6 / Galaxy.AgentAnnualMaintenance);
                }
                num = Math.Min(desiredIntelligenceAgentAmount - characterList.Count, num);
                double num7 = Galaxy.AgentAnnualMaintenance * 5.0;
                if (num7 * (double)num > StateMoney)
                {
                    num = (int)(StateMoney / num7);
                }
            }
            return num;
        }

        private void GenerateIntelligenceAgents()
        {
            int num = NewAgentsCanRecruit();
            bool flag = false;
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                if (DiplomaticRelations[i].Type != 0)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                num = 0;
            }
            double num2 = Galaxy.AgentAnnualMaintenance * 5.0;
            if (num > 0)
            {
                for (int j = 0; j < num; j++)
                {
                    bool isRandomCharacter = false;
                    Character character = GenerateNewCharacter(CharacterRole.IntelligenceAgent, Capital, out isRandomCharacter);
                    StateMoney -= num2;
                    _Galaxy.DoCharacterEventLeader(CharacterEventType.IntelligenceAgentRecruited, character, this);
                    IntelligenceMission intelligenceMission = new IntelligenceMission(this, character, _Galaxy.CurrentStarDate);
                    intelligenceMission.TimeLength = Galaxy.RealSecondsInGalacticYear * 1000 / 4;
                    character.Mission = intelligenceMission;
                }
            }
        }

        public string GenerateAgentName()
        {
            return GenerateAgentName(DominantRace);
        }

        public string GenerateAgentName(Race race)
        {
            string result = string.Empty;
            if (race != null)
            {
                result = _Galaxy.GenerateUniqueAgentName(race.FamilyId);
            }
            return result;
        }

        private bool AssignAgentForEspionageMission(Empire targetEmpire, EmpireEvaluation evaluation, DiplomaticRelation relation, PirateRelation pirateRelation, ref int refusalCount)
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                Character character = Characters[i];
                if (character.Role != CharacterRole.IntelligenceAgent || (character.Mission != null && character.Mission.Type != 0 && character.Mission.Type != IntelligenceMissionType.CounterIntelligence))
                {
                    continue;
                }
                IntelligenceMission mission = DetermineEspionageMission(targetEmpire, evaluation, relation, pirateRelation, character);
                mission = BaconEmpire.OverrideTimeForMission(this, mission);
                if (mission != null)
                {
                    mission.Agent = character;
                    if (CheckTaskAuthorized(_ControlAgentAssignment, ref refusalCount, GenerateAutomationMessageAgentMission(mission), mission, AdvisorMessageType.IntelligenceMission, mission.TargetEmpire, character, null))
                    {
                        character.Mission = mission;
                        return true;
                    }
                    mission.Agent = null;
                }
            }
            return false;
        }

        private bool CheckForIntelligenceMissionsOfTypeAgainstEmpire(Empire targetEmpire, IntelligenceMissionType missionType)
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                Character character = Characters[i];
                if (character.Mission != null && character.Mission.Type != 0 && character.Mission.TargetEmpire == targetEmpire && character.Mission.Type == missionType)
                {
                    return true;
                }
            }
            return false;
        }

        private bool AssignAgentForSabotageMission(Empire targetEmpire, EmpireEvaluation evaluation, DiplomaticRelation relation, PirateRelation pirateRelation, ref int refusalCount)
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                Character character = Characters[i];
                if (character.Role != CharacterRole.IntelligenceAgent || (character.Mission != null && character.Mission.Type != 0 && character.Mission.Type != IntelligenceMissionType.CounterIntelligence))
                {
                    continue;
                }
                IntelligenceMission intelligenceMission = DetermineSabotageMission(targetEmpire, evaluation, relation, pirateRelation, character);
                if (intelligenceMission != null)
                {
                    intelligenceMission.Agent = character;
                    if (CheckTaskAuthorized(_ControlAgentAssignment, ref refusalCount, GenerateAutomationMessageAgentMission(intelligenceMission), intelligenceMission, AdvisorMessageType.IntelligenceMission, intelligenceMission.TargetEmpire, character, null))
                    {
                        character.Mission = intelligenceMission;
                        return true;
                    }
                    intelligenceMission.Agent = null;
                }
            }
            return false;
        }

        public bool AssignScrapMission(BuiltObject builtObject)
        {
            return AssignScrapMission(builtObject, allowImmediateScrappingIfYardsFull: true);
        }

        public bool AssignScrapMission(BuiltObject builtObject, bool allowImmediateScrappingIfYardsFull)
        {
            return AssignScrapMission(builtObject, allowImmediateScrappingIfYardsFull, allowImmediateScrappingIfNoWarpSpeed: true);
        }

        public bool AssignScrapMission(BuiltObject builtObject, bool allowImmediateScrappingIfYardsFull, bool allowImmediateScrappingIfNoWarpSpeed)
        {
            builtObject.RetireForNextMission = false;
            if (builtObject.Empire == null || (builtObject.Empire == _Galaxy.IndependentEmpire && builtObject.PirateEmpireId == 0))
            {
                builtObject.CompleteTeardown(_Galaxy);
                return true;
            }
            if (builtObject.Role != BuiltObjectRole.Base && builtObject.TopSpeed <= 0)
            {
                builtObject.CompleteTeardown(_Galaxy);
                return true;
            }
            if (allowImmediateScrappingIfNoWarpSpeed && builtObject.Role != BuiltObjectRole.Base && builtObject.WarpSpeed <= 0)
            {
                builtObject.CompleteTeardown(_Galaxy);
                return true;
            }
            if (builtObject.Role == BuiltObjectRole.Base && builtObject.SubRole != BuiltObjectSubRole.Outpost && builtObject.SubRole != BuiltObjectSubRole.SmallSpacePort && builtObject.SubRole != BuiltObjectSubRole.MediumSpacePort && builtObject.SubRole != BuiltObjectSubRole.LargeSpacePort)
            {
                builtObject.InflictDamage(builtObject, null, 100000.0, _Galaxy.CurrentDateTime, _Galaxy, 0f, allowRecursion: false, 0.0, allowArmorInvulnerability: false);
                return true;
            }
            double shortestWaitQueueTime;
            BuiltObject builtObject2 = SpacePorts.FindShortestConstructionWaitQueueCloseToBuiltObject(builtObject, out shortestWaitQueueTime);
            if (builtObject2 != null)
            {
                bool flag = false;
                for (int i = 0; i < builtObject2.ConstructionQueue.ConstructionYards.Count; i++)
                {
                    if (builtObject2.ConstructionQueue.ConstructionYards[i].ShipUnderConstruction == null)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag || !allowImmediateScrappingIfYardsFull)
                {
                    builtObject.ClearPreviousMissionRequirements();
                    builtObject.AssignMission(BuiltObjectMissionType.Retire, builtObject2, null, BuiltObjectMissionPriority.Normal);
                    return true;
                }
                builtObject.ClearPreviousMissionRequirements();
                builtObject.CompleteTeardown(_Galaxy);
                return true;
            }
            return false;
        }

        public double GetPrivateAnnualRevenue()
        {
            if (GovernmentAttributes != null && GovernmentAttributes.SpecialFunctionCode == 1)
            {
                return AnnualTaxRevenue;
            }
            return PrivateAnnualRevenue;
        }

        public double GetPrivateAnnualCashflow()
        {
            return GetPrivateAnnualCashflow(excludeRetirees: false);
        }

        public double GetPrivateAnnualCashflow(bool excludeRetirees)
        {
            if (GovernmentAttributes != null && GovernmentAttributes.SpecialFunctionCode == 1)
            {
                double num = 0.0;
                double num2 = 0.0;
                if (excludeRetirees)
                {
                    num = AnnualStateMaintenanceWithoutRetirees;
                    num2 = AnnualPrivateMaintenanceWithoutRetirees;
                }
                else
                {
                    num = AnnualStateMaintenance;
                    num2 = AnnualPrivateMaintenance;
                }
                double num3 = num + AnnualTroopMaintenance;
                double num4 = num2;
                double annualTaxRevenue = AnnualTaxRevenue;
                return annualTaxRevenue - (num3 + num4);
            }
            double num5 = 0.0;
            num5 = ((!excludeRetirees) ? (AnnualPrivateMaintenance + AnnualTaxRevenue) : (AnnualPrivateMaintenanceWithoutRetirees + AnnualTaxRevenue));
            return PrivateAnnualRevenue - num5;
        }

        private void RetireOldBuiltObjects()
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            _ = Galaxy.RealSecondsInGalacticYear;
            _ = Galaxy.RetirementYears;
            long num = Galaxy.RealSecondsInGalacticYear * 1000 * Galaxy.RetirementYears;
            int num2 = 0;
            double num3 = GetPrivateFunds() / (AnnualPrivateMaintenance + 1.0);
            double num4 = StateMoney / (AnnualStateMaintenance + 1.0);
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(PrivateBuiltObjects);
            BuiltObjectList builtObjectList2 = new BuiltObjectList();
            builtObjectList2.AddRange(BuiltObjects);
            int num5 = Math.Min(700, 1 + (int)((double)Colonies.Count * 1.5));
            int num6 = Math.Min(100, 1 + (int)((double)Colonies.Count * 0.5));
            int num7 = Math.Min(50, 2 + (int)((double)Colonies.Count * 0.3));
            int num8 = Math.Min(100, 2 + (int)((double)Colonies.Count * 0.5));
            if (num3 < Galaxy.AllowableYearsMaintenanceFromCashOnHand)
            {
                double privateAnnualCashflow = GetPrivateAnnualCashflow(excludeRetirees: true);
                if (privateAnnualCashflow < 0.0)
                {
                    double val = 1.0 - Math.Abs(privateAnnualCashflow) / AnnualPrivateMaintenanceWithoutRetirees;
                    val = Math.Max(0.0, Math.Min(1.0, val));
                    num2 = (int)((double)PrivateBuiltObjects.Count - (double)PrivateBuiltObjects.Count * val);
                    for (int i = 0; i < builtObjectList.Count; i++)
                    {
                        if (builtObjectList[i] != null && builtObjectList[i].Design != null)
                        {
                            builtObjectList[i].SortTag = builtObjectList[i].Design.DateCreated;
                        }
                    }
                    builtObjectList.Sort();
                }
            }
            if (num4 < Galaxy.AllowableYearsMaintenanceFromCashOnHand)
            {
                double num9 = CalculateSpareAnnualRevenueComplete();
                if (num9 < 0.0)
                {
                    double val2 = 1.0 - Math.Abs(num9) / AnnualStateMaintenanceWithoutRetirees;
                    val2 = Math.Max(0.0, Math.Min(1.0, val2));
                    _ = BuiltObjects.Count;
                    _ = BuiltObjects.Count;
                    for (int j = 0; j < builtObjectList2.Count; j++)
                    {
                        if (builtObjectList2[j] != null && builtObjectList2[j].Design != null)
                        {
                            builtObjectList2[j].SortTag = builtObjectList2[j].Design.DateCreated;
                        }
                    }
                    builtObjectList2.Sort();
                }
            }
            BuiltObjectList builtObjectList3 = new BuiltObjectList();
            builtObjectList3.AddRange(BuiltObjects);
            builtObjectList3.AddRange(builtObjectList);
            int empireOrderCount = 0;
            OrderList orders = _Galaxy.Orders.GetOrders(this);
            if (orders != null)
            {
                empireOrderCount = orders.Count;
            }
            int num10 = 0;
            int num11 = 0;
            int num12 = 0;
            int num13 = 0;
            for (int k = 0; k < PrivateBuiltObjects.Count; k++)
            {
                BuiltObject builtObject = PrivateBuiltObjects[k];
                if (builtObject.UnbuiltComponentCount <= 0)
                {
                    if (builtObject.Role == BuiltObjectRole.Freight)
                    {
                        num10++;
                    }
                    else if (builtObject.SubRole == BuiltObjectSubRole.PassengerShip)
                    {
                        num11++;
                    }
                    else if (builtObject.SubRole == BuiltObjectSubRole.MiningShip || builtObject.SubRole == BuiltObjectSubRole.GasMiningShip)
                    {
                        num12++;
                    }
                    else if (builtObject.SubRole == BuiltObjectSubRole.MiningStation || builtObject.SubRole == BuiltObjectSubRole.GasMiningStation)
                    {
                        num13++;
                    }
                }
            }
            CheckAtWar();
            int num14 = 0;
            for (int l = 0; l < builtObjectList.Count; l++)
            {
                BuiltObject builtObject2 = builtObjectList[l];
                if (!builtObject2.IsAutoControlled || builtObject2.RetireForNextMission || builtObject2.Scrap || (currentStarDate - builtObject2.DateBuilt <= num && num14 >= num2) || !CanBuildBuiltObject(builtObject2))
                {
                    continue;
                }
                if (builtObject2.Role == BuiltObjectRole.Freight)
                {
                    if (num10 > 0 && num10 > num5 && ShouldRetireFreighter(builtObject2, empireOrderCount, num10))
                    {
                        builtObject2.CompleteTeardown(_Galaxy, removeFromEmpire: true);
                        num10--;
                        num14++;
                    }
                }
                else if ((builtObject2.SubRole != BuiltObjectSubRole.MiningStation && builtObject2.SubRole != BuiltObjectSubRole.GasMiningStation) || num13 <= num8)
                {
                    if ((builtObject2.SubRole == BuiltObjectSubRole.MiningShip || builtObject2.SubRole == BuiltObjectSubRole.GasMiningShip) && num12 > num7)
                    {
                        builtObject2.CompleteTeardown(_Galaxy, removeFromEmpire: true);
                        num12--;
                        num14++;
                    }
                    else if (builtObject2.SubRole == BuiltObjectSubRole.PassengerShip && num11 > num6)
                    {
                        builtObject2.CompleteTeardown(_Galaxy, removeFromEmpire: true);
                        num11--;
                        num14++;
                    }
                    else if (builtObject2.Role != BuiltObjectRole.Base || builtObject2.IsShipYard || builtObject2.IsResourceExtractor)
                    {
                        builtObject2.RetireForNextMission = true;
                        num14++;
                    }
                }
            }
        }

        private bool ShouldRetireFreighter(BuiltObject builtObject, int empireOrderCount, int empireFreighterCount)
        {
            if (builtObject.Role == BuiltObjectRole.Freight && (double)empireOrderCount / (double)empireFreighterCount > 1.5)
            {
                return false;
            }
            return true;
        }

        private BuiltObject GenerateNewShip(BuiltObjectSubRole subRole, Habitat location)
        {
            Design design = Designs.FindNewestCanBuild(subRole, location);
            if (design != null)
            {
                double purchasePrice = design.CalculateCurrentPurchasePrice(_Galaxy);
                design.BuildCount++;
                BuiltObject builtObject = new BuiltObject(design, _Galaxy.GenerateBuiltObjectName(design), _Galaxy);
                builtObject.PurchasePrice = purchasePrice;
                return GenerateNewBuiltObject(design, location);
            }
            return null;
        }

        private void DirectPrivateConstruction()
        {
            List<CargoList> list = new List<CargoList>();
            BuiltObjectList builtObjectList = new BuiltObjectList();
            CargoList resourcesToOrder = null;
            double privateAnnualCashflow = GetPrivateAnnualCashflow();
            double annualSupportCosts = 0.0;
            ForceStructureProjectionList forceStructureProjectionList = CurrentPrivateForceStructure(out annualSupportCosts);
            ForceStructureProjectionList forceStructureProjectionList2 = _PrivateForceStructureProjections.Diff(forceStructureProjectionList);
            forceStructureProjectionList2.Sort();
            double num = 0.0;
            double num2 = 0.0;
            foreach (ForceStructureProjection item2 in forceStructureProjectionList2)
            {
                Design design = Designs.FindNewestCanBuild(item2.SubRole);
                if (design == null || item2.Amount <= 0)
                {
                    continue;
                }
                for (int i = 0; i < item2.Amount; i++)
                {
                    double num3 = design.CalculateCurrentPurchasePrice(_Galaxy);
                    double num4 = design.CalculateMaintenanceCosts(_Galaxy, this);
                    if (!(num2 + num4 <= privateAnnualCashflow) || !(num + num3 <= GetPrivateFunds()))
                    {
                        continue;
                    }
                    design.BuildCount++;
                    BuiltObject builtObject = new BuiltObject(design, _Galaxy.GenerateBuiltObjectName(design), _Galaxy);
                    builtObject.PurchasePrice = num3;
                    double shortestWaitQueueTime;
                    BuiltObject builtObject2 = SpacePorts.FindShortestConstructionWaitQueue(builtObject, out shortestWaitQueueTime);
                    double num5 = shortestWaitQueueTime / (double)Galaxy.RealSecondsInGalacticYear;
                    if (builtObject2 != null && num5 < Galaxy.MaximumConstructionQueueWaitTimeYears)
                    {
                        if (builtObject2.ConstructionQueue != null)
                        {
                            if (builtObject2.ConstructionQueue.AddBuiltObjectToConstruct(builtObject))
                            {
                                if (builtObject2.ParentHabitat != null)
                                {
                                    Habitat habitat = Galaxy.DetermineHabitatSystemStar(builtObject2.ParentHabitat);
                                    builtObject.Name = _Galaxy.GenerateBuiltObjectName(design, habitat);
                                }
                                AddBuiltObjectToGalaxy(builtObject, builtObject2, offsetLocationFromParent: false, isStateOwned: false);
                                num += num3;
                                num2 += num4;
                                builtObject2.PerformFinancialTransaction(num3, _Galaxy.CurrentStarDate, incomeFromTax: false);
                                builtObject.BuiltAt = builtObject2;
                                ProcureConstructionComponents(builtObject, builtObject2, orderPreciseResourceAmounts: true, out resourcesToOrder);
                                list.Add(resourcesToOrder);
                                builtObjectList.Add(builtObject2);
                            }
                            else
                            {
                                design.BuildCount--;
                            }
                        }
                        else
                        {
                            design.BuildCount--;
                        }
                    }
                    else
                    {
                        design.BuildCount--;
                    }
                }
            }
            BuiltObjectList builtObjectList2 = new BuiltObjectList();
            for (int j = 0; j < builtObjectList.Count; j++)
            {
                BuiltObject item = builtObjectList[j];
                if (!builtObjectList2.Contains(item))
                {
                    builtObjectList2.Add(item);
                }
            }
            foreach (BuiltObject item3 in builtObjectList2)
            {
                CargoList cargoList = new CargoList();
                for (int k = 0; k < builtObjectList.Count; k++)
                {
                    if (builtObjectList[k] != item3)
                    {
                        continue;
                    }
                    foreach (Cargo item4 in list[k])
                    {
                        cargoList.Add(item4);
                    }
                }
                foreach (Cargo item5 in cargoList)
                {
                    CreateOrder(item3, item5.CommodityResource, item5.Amount, isState: false, OrderType.ConstructionShortage);
                }
            }
            _StateMoney += BaconEmpire.PrivateConstructionAddToInfrastructure(this, num);
            PerformPrivateTransaction(0.0 - num);
        }

        public void ObtainBuildResourcesForConstructionShip(BuiltObject constructionShip, BuiltObject newBuiltObject)
        {
            if (constructionShip.SubRole == BuiltObjectSubRole.ConstructionShip && constructionShip.Cargo != null)
            {
                foreach (Cargo item in constructionShip.Cargo)
                {
                    item.Reserved = 0;
                }
            }
            CargoList resourcesToOrder = null;
            ProcureConstructionComponents(newBuiltObject, constructionShip, orderPreciseResourceAmounts: true, out resourcesToOrder);
            foreach (Cargo item2 in resourcesToOrder)
            {
                CreateOrder(constructionShip, item2.CommodityResource, item2.Amount, isState: false, OrderType.ConstructionShortageMobile);
            }
        }

        public void ProcureConstructionComponents(BuiltObject builtObjectToBuild, BuiltObject constructionYard, bool orderPreciseResourceAmounts, out CargoList resourcesToOrder)
        {
            ComponentList componentList = new ComponentList();
            if (builtObjectToBuild != null && builtObjectToBuild.Components != null)
            {
                foreach (BuiltObjectComponent component in builtObjectToBuild.Components)
                {
                    if (component.Status == ComponentStatus.Unbuilt)
                    {
                        componentList.Add(component);
                    }
                }
            }
            ProcureConstructionComponents(builtObjectToBuild, constructionYard, orderPreciseResourceAmounts, out resourcesToOrder, componentList);
        }

        public void ProcureConstructionComponents(BuiltObject builtObjectToBuild, BuiltObject constructionYard, bool orderPreciseResourceAmounts, out CargoList resourcesToOrder, ComponentList components)
        {
            ProcureConstructionComponents(builtObjectToBuild, constructionYard, orderPreciseResourceAmounts, out resourcesToOrder, components, forBaseRetrofit: false);
        }

        public void ProcureConstructionComponents(BuiltObject builtObjectToBuild, BuiltObject constructionYard, bool orderPreciseResourceAmounts, out CargoList resourcesToOrder, ComponentList components, bool forBaseRetrofit)
        {
            resourcesToOrder = new CargoList();
            ComponentList componentsToFind = components;
            if (constructionYard == null)
            {
                return;
            }
            if (constructionYard.SubRole == BuiltObjectSubRole.ConstructionShip)
            {
                CheckCargoForComponentSupply(builtObjectToBuild, ref componentsToFind, constructionYard);
            }
            if (forBaseRetrofit)
            {
                if (constructionYard.RetrofitBaseManufacturingQueue == null || constructionYard.RetrofitBaseManufacturingQueue.ComponentWaitQueue == null)
                {
                    return;
                }
                CheckCargoForComponentResourceSupply(builtObjectToBuild, componentsToFind, constructionYard, orderPreciseResourceAmounts, out resourcesToOrder);
                {
                    foreach (Component item in componentsToFind)
                    {
                        constructionYard.RetrofitBaseManufacturingQueue.ComponentWaitQueue.Add(item);
                    }
                    return;
                }
            }
            if (constructionYard.ManufacturingQueue == null || constructionYard.ManufacturingQueue.ComponentWaitQueue == null)
            {
                return;
            }
            CheckCargoForComponentResourceSupply(builtObjectToBuild, componentsToFind, constructionYard, orderPreciseResourceAmounts, out resourcesToOrder);
            foreach (Component item2 in componentsToFind)
            {
                constructionYard.ManufacturingQueue.ComponentWaitQueue.Add(item2);
            }
        }

        public void ProcureConstructionComponents(BuiltObject builtObjectToBuild, Habitat constructionColony, out CargoList resourcesToOrder)
        {
            ComponentList components = builtObjectToBuild.Design.Components.Clone();
            ProcureConstructionComponents(builtObjectToBuild, constructionColony, out resourcesToOrder, components);
        }

        public void ProcureConstructionComponents(BuiltObject builtObjectToBuild, Habitat constructionColony, out CargoList resourcesToOrder, ComponentList components)
        {
            CheckCargoForComponentResourceSupply(builtObjectToBuild, components, constructionColony, orderPreciseAmount: true, out resourcesToOrder);
            if (constructionColony == null || constructionColony.ManufacturingQueue == null || constructionColony.ManufacturingQueue.ComponentWaitQueue == null)
            {
                return;
            }
            foreach (Component component in components)
            {
                constructionColony.ManufacturingQueue.ComponentWaitQueue.Add(component);
            }
        }

        private void CheckCargoForComponentResourceSupply(BuiltObject builtObjectToBuild, ComponentList componentsToFind, Habitat constructionColony, bool orderPreciseAmount, out CargoList resourcesToOrder)
        {
            resourcesToOrder = new CargoList();
            if (builtObjectToBuild == null || componentsToFind == null || constructionColony == null)
            {
                return;
            }
            new ComponentResourceList();
            for (int i = 0; i < componentsToFind.Count; i++)
            {
                Component component = componentsToFind[i];
                for (int j = 0; j < component.RequiredResources.Count; j++)
                {
                    ComponentResource componentResource = component.RequiredResources[j];
                    Cargo cargo = null;
                    int num = 0;
                    if (constructionColony.Cargo == null)
                    {
                        continue;
                    }
                    int num2 = constructionColony.Cargo.IndexOf(componentResource, this);
                    if (num2 >= 0)
                    {
                        cargo = constructionColony.Cargo[num2];
                        num = Math.Max(0, componentResource.Quantity - Math.Max(0, cargo.Available));
                        cargo.Reserved += componentResource.Quantity;
                    }
                    if (cargo == null)
                    {
                        Cargo cargo2 = new Cargo(amount: (!orderPreciseAmount) ? Math.Max(Galaxy.MinimumOrderAmount, componentResource.Quantity) : componentResource.Quantity, resource: new Resource(componentResource.ResourceID), empire: this);
                        resourcesToOrder.Add(cargo2);
                        cargo = new Cargo(componentResource, 0, this, componentResource.Quantity);
                        constructionColony.Cargo.Add(cargo);
                    }
                    else if (num > 0)
                    {
                        if (!orderPreciseAmount)
                        {
                            num = Math.Max(num, Galaxy.MinimumOrderAmount);
                        }
                        Cargo cargo3 = new Cargo(new Resource(componentResource.ResourceID), num, this);
                        resourcesToOrder.Add(cargo3);
                    }
                }
            }
        }

        public ComponentResourceList ResolveResourcesFromComponents(ComponentList components)
        {
            ComponentResourceList componentResourceList = new ComponentResourceList();
            foreach (Component component in components)
            {
                foreach (ComponentResource requiredResource in component.RequiredResources)
                {
                    int num = componentResourceList.IndexOf(requiredResource);
                    if (num >= 0)
                    {
                        componentResourceList[num].Quantity += requiredResource.Quantity;
                    }
                    else
                    {
                        componentResourceList.Add(new ComponentResource(requiredResource.ResourceID, requiredResource.Quantity));
                    }
                }
            }
            return componentResourceList;
        }

        private void CheckCargoForComponentResourceSupply(BuiltObject builtObjectToBuild, ComponentList componentsToFind, BuiltObject constructionYard, bool orderPreciseAmount, out CargoList resourcesToOrder)
        {
            resourcesToOrder = new CargoList();
            if (builtObjectToBuild == null || componentsToFind == null || constructionYard == null)
            {
                return;
            }
            new ComponentResourceList();
            for (int i = 0; i < componentsToFind.Count; i++)
            {
                Component component = componentsToFind[i];
                for (int j = 0; j < component.RequiredResources.Count; j++)
                {
                    ComponentResource componentResource = component.RequiredResources[j];
                    Cargo cargo = null;
                    int num = 0;
                    if (constructionYard.Cargo != null)
                    {
                        int num2 = constructionYard.Cargo.IndexOf(componentResource, this);
                        if (num2 >= 0)
                        {
                            cargo = constructionYard.Cargo[num2];
                            num = Math.Max(0, componentResource.Quantity - Math.Max(0, cargo.Available));
                            cargo.Reserved += componentResource.Quantity;
                        }
                        if (cargo == null)
                        {
                            int quantity = componentResource.Quantity;
                            Cargo cargo2 = new Cargo(new Resource(componentResource.ResourceID), quantity, this);
                            resourcesToOrder.Add(cargo2);
                            cargo = new Cargo(componentResource, 0, this, componentResource.Quantity);
                            constructionYard.Cargo.Add(cargo);
                        }
                        else if (num > 0)
                        {
                            Cargo cargo3 = new Cargo(new Resource(componentResource.ResourceID), num, this);
                            resourcesToOrder.Add(cargo3);
                        }
                    }
                }
            }
            if (orderPreciseAmount)
            {
                return;
            }
            foreach (Cargo item in resourcesToOrder)
            {
                item.Amount = Math.Max(item.Amount, Galaxy.MinimumOrderAmount);
            }
        }

        private bool CheckCargoForComponentSupply(BuiltObject builtObjectToBuild, ref ComponentList componentsToFind, Habitat cargoProvider)
        {
            bool result = false;
            ComponentList componentList = new ComponentList();
            if (builtObjectToBuild == null || componentsToFind == null || cargoProvider == null || cargoProvider.Cargo == null)
            {
                return false;
            }
            for (int i = 0; i < componentsToFind.Count; i++)
            {
                Component component = componentsToFind[i];
                Component component2 = null;
                int num = cargoProvider.Cargo.IndexOf(component, this);
                if (num >= 0 && cargoProvider.Cargo[num].Available > 0)
                {
                    cargoProvider.Cargo[num].Reserved++;
                    component2 = cargoProvider.Cargo[num].CommodityComponent;
                    result = true;
                }
                if (component2 != null)
                {
                    componentList.Add(component2);
                }
            }
            foreach (Component item in componentList)
            {
                componentsToFind.Remove(item);
            }
            return result;
        }

        private bool CheckCargoForComponentSupply(BuiltObject builtObjectToBuild, ref ComponentList componentsToFind, BuiltObject cargoProvider)
        {
            bool result = false;
            ComponentList componentList = new ComponentList();
            if (builtObjectToBuild == null || componentsToFind == null || cargoProvider == null || cargoProvider.Cargo == null)
            {
                return false;
            }
            for (int i = 0; i < componentsToFind.Count; i++)
            {
                Component component = componentsToFind[i];
                Component component2 = null;
                int num = cargoProvider.Cargo.IndexOf(component, this);
                if (num >= 0 && cargoProvider.Cargo[num].Available > 0)
                {
                    cargoProvider.Cargo[num].Reserved++;
                    component2 = cargoProvider.Cargo[num].CommodityComponent;
                    result = true;
                }
                if (component2 != null)
                {
                    componentList.Add(component2);
                }
            }
            foreach (Component item in componentList)
            {
                int num2 = componentsToFind.IndexById(item);
                if (num2 >= 0)
                {
                    componentsToFind.RemoveAt(num2);
                }
            }
            return result;
        }

        public StellarObjectList DetermineRestrictedResourceSupplyLocations()
        {
            StellarObjectList stellarObjectList = new StellarObjectList();
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                HabitatResourceList habitatResourceList = new HabitatResourceList();
                if (habitat.Resources != null)
                {
                    habitatResourceList = habitat.Resources.Clone();
                }
                for (int j = 0; j < habitatResourceList.Count; j++)
                {
                    if (habitatResourceList[j].IsRestrictedResource)
                    {
                        stellarObjectList.Add(habitat);
                    }
                }
            }
            for (int k = 0; k < MiningStations.Count; k++)
            {
                BuiltObject builtObject = MiningStations[k];
                if (builtObject.ParentHabitat == null)
                {
                    continue;
                }
                HabitatResourceList habitatResourceList2 = new HabitatResourceList();
                if (builtObject.ParentHabitat.Resources != null)
                {
                    habitatResourceList2 = builtObject.ParentHabitat.Resources.Clone();
                }
                for (int l = 0; l < habitatResourceList2.Count; l++)
                {
                    if (habitatResourceList2[l].IsRestrictedResource)
                    {
                        stellarObjectList.Add(builtObject);
                    }
                }
            }
            return stellarObjectList;
        }

        public BuiltObjectList DetermineShipsMovingToDestination(StellarObject destination)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            if (destination != null)
            {
                for (int i = 0; i < BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = BuiltObjects[i];
                    if (CheckShipTravellingToDestination(builtObject, destination))
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
                for (int j = 0; j < PrivateBuiltObjects.Count; j++)
                {
                    BuiltObject builtObject2 = PrivateBuiltObjects[j];
                    if (CheckShipTravellingToDestination(builtObject2, destination))
                    {
                        builtObjectList.Add(builtObject2);
                    }
                }
            }
            return builtObjectList;
        }

        public bool CheckShipTravellingToDestination(BuiltObject builtObject, StellarObject destination)
        {
            if (builtObject != null && !builtObject.HasBeenDestroyed && destination != null && !destination.HasBeenDestroyed && builtObject.Mission != null && builtObject.Mission.Type != 0)
            {
                StellarObject stellarObject = null;
                if (builtObject.Mission.Target != null && builtObject.Mission.Target is StellarObject)
                {
                    stellarObject = (StellarObject)builtObject.Mission.Target;
                }
                StellarObject stellarObject2 = null;
                if (builtObject.Mission.SecondaryTarget != null && builtObject.Mission.SecondaryTarget is StellarObject)
                {
                    stellarObject2 = (StellarObject)builtObject.Mission.SecondaryTarget;
                }
                if (stellarObject == destination)
                {
                    BuiltObjectMissionType type = builtObject.Mission.Type;
                    if (type == BuiltObjectMissionType.Transport)
                    {
                        return false;
                    }
                    return true;
                }
                if (stellarObject2 == destination)
                {
                    BuiltObjectMissionType type2 = builtObject.Mission.Type;
                    if (type2 == BuiltObjectMissionType.Transport)
                    {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public int CountShipsAssignedToMission(EmpireActivity mission, out int firepowerAssigned)
        {
            BuiltObjectList builtObjectList = DetermineShipsAssignedToMission(mission);
            firepowerAssigned = builtObjectList.TotalMobileMilitaryFirepower();
            return builtObjectList.Count;
        }

        public BuiltObjectList DetermineShipsAssignedToMission(EmpireActivity mission)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            if (mission != null)
            {
                for (int i = 0; i < BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = BuiltObjects[i];
                    if (CheckShipPerformingMission(builtObject, mission))
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
                for (int j = 0; j < PrivateBuiltObjects.Count; j++)
                {
                    BuiltObject builtObject2 = PrivateBuiltObjects[j];
                    if (CheckShipPerformingMission(builtObject2, mission))
                    {
                        builtObjectList.Add(builtObject2);
                    }
                }
            }
            return builtObjectList;
        }

        public bool CheckShipPerformingMission(BuiltObject builtObject, EmpireActivity mission)
        {
            if (builtObject != null && !builtObject.HasBeenDestroyed && mission != null)
            {
                switch (mission.Type)
                {
                    case EmpireActivityType.Smuggle:
                        if (builtObject.Role == BuiltObjectRole.Freight && builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Transport && builtObject.Mission.SecondaryTargetHabitat == mission.Target && (mission.ResourceId == byte.MaxValue || builtObject.Mission.Cargo.IndexOf(mission.ResourceId) >= 0))
                        {
                            return true;
                        }
                        break;
                    case EmpireActivityType.Attack:
                        if (builtObject.Role == BuiltObjectRole.Military && builtObject.Mission != null && (builtObject.Mission.Type == BuiltObjectMissionType.Attack || builtObject.Mission.Type == BuiltObjectMissionType.Capture) && builtObject.Mission.Target == mission.Target)
                        {
                            return true;
                        }
                        break;
                    case EmpireActivityType.Defend:
                        if (builtObject.Role != BuiltObjectRole.Military)
                        {
                            break;
                        }
                        if (builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.MoveAndWait && builtObject.Mission.Target == mission.Target)
                        {
                            return true;
                        }
                        if (builtObject.BuiltAt == null)
                        {
                            double num = _Galaxy.CalculateDistanceSquared(mission.Target.Xpos, mission.Target.Ypos, builtObject.Xpos, builtObject.Ypos);
                            if (num < 2250000.0)
                            {
                                return true;
                            }
                        }
                        break;
                }
            }
            return false;
        }

        public int CountIdleFreighters()
        {
            int num = 0;
            for (int i = 0; i < PrivateBuiltObjects.Count; i++)
            {
                BuiltObject builtObject = PrivateBuiltObjects[i];
                if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.UnbuiltComponentCount <= 0 && builtObject.Role == BuiltObjectRole.Freight && (builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined))
                {
                    num++;
                }
            }
            return num;
        }

        public int CountResourceSupplyLocations(byte resourceId, bool includeIndependentColonies)
        {
            int num = 0;
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(BuiltObjects);
            builtObjectList.AddRange(PrivateBuiltObjects);
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat.Empire != this)
                {
                    continue;
                }
                foreach (HabitatResource resource in habitat.Resources)
                {
                    if (resource.ResourceID == resourceId)
                    {
                        num++;
                    }
                }
            }
            for (int j = 0; j < builtObjectList.Count; j++)
            {
                BuiltObject builtObject = builtObjectList[j];
                if (builtObject.SubRole == BuiltObjectSubRole.GasMiningStation || builtObject.SubRole == BuiltObjectSubRole.MiningStation)
                {
                    if (builtObject.ParentHabitat == null)
                    {
                        continue;
                    }
                    foreach (HabitatResource resource2 in builtObject.ParentHabitat.Resources)
                    {
                        if (resource2.ResourceID == resourceId)
                        {
                            num++;
                        }
                    }
                }
                else
                {
                    if (PirateEmpireBaseHabitat == null || (builtObject.SubRole != BuiltObjectSubRole.Outpost && builtObject.SubRole != BuiltObjectSubRole.SmallSpacePort && builtObject.SubRole != BuiltObjectSubRole.MediumSpacePort && builtObject.SubRole != BuiltObjectSubRole.LargeSpacePort) || builtObject.ParentHabitat == null || builtObject.ParentHabitat.Empire == this)
                    {
                        continue;
                    }
                    foreach (HabitatResource resource3 in builtObject.ParentHabitat.Resources)
                    {
                        if (resource3.ResourceID == resourceId)
                        {
                            num++;
                        }
                    }
                }
            }
            if (includeIndependentColonies)
            {
                for (int k = 0; k < _Galaxy.IndependentColonies.Count; k++)
                {
                    Habitat habitat2 = _Galaxy.IndependentColonies[k];
                    if (habitat2.Empire != _Galaxy.IndependentEmpire || !CheckSystemExplored(habitat2.SystemIndex))
                    {
                        continue;
                    }
                    foreach (HabitatResource resource4 in habitat2.Resources)
                    {
                        if (resource4.ResourceID == resourceId)
                        {
                            num++;
                        }
                    }
                }
            }
            return num;
        }

        public ResourceList DetermineResourcesEmpireSupplies()
        {
            ResourceList resourceList = new ResourceList();
            Resource resource = null;
            if (Colonies != null)
            {
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat = Colonies[i];
                    if (habitat == null || habitat.Resources == null)
                    {
                        continue;
                    }
                    foreach (HabitatResource resource2 in habitat.Resources)
                    {
                        int num = resourceList.IndexOf(resource2.ResourceID);
                        if (num < 0)
                        {
                            resource = new Resource(resource2.ResourceID);
                            resourceList.Add(resource);
                        }
                    }
                }
            }
            for (int j = 0; j < PrivateBuiltObjects.Count; j++)
            {
                BuiltObject builtObject = PrivateBuiltObjects[j];
                if (builtObject == null || (builtObject.SubRole != BuiltObjectSubRole.GasMiningStation && builtObject.SubRole != BuiltObjectSubRole.MiningStation) || builtObject.ParentHabitat == null || builtObject.ParentHabitat.Resources == null)
                {
                    continue;
                }
                foreach (HabitatResource resource3 in builtObject.ParentHabitat.Resources)
                {
                    int num2 = resourceList.IndexOf(resource3.ResourceID);
                    if (num2 < 0)
                    {
                        resource = new Resource(resource3.ResourceID);
                        resourceList.Add(resource);
                    }
                }
            }
            return resourceList;
        }

        private ResourceList ResolveOrderedNonFuelNonCriticalResources(ResourceList criticalResources)
        {
            ResourceList resourceList = new ResourceList();
            Resource resource = null;
            for (int i = 0; i < _Galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; i++)
            {
                ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance[i];
                if (resourceDefinition != null && !resourceDefinition.IsFuel)
                {
                    resource = new Resource(resourceDefinition.ResourceID);
                    if (!criticalResources.Contains(resource))
                    {
                        resourceList.Add(resource);
                    }
                }
            }
            return resourceList;
        }

        public bool BuildStrategicResourceSupply(BuiltObject constructionShip, HabitatList empireHabitatsBeingMined)
        {
            StellarObject stellarObject = _Galaxy.FastFindNearestSpacePort(constructionShip.Xpos, constructionShip.Ypos, this, false);
            if (stellarObject == null)
            {
                stellarObject = ((PirateEmpireBaseHabitat != null) ? PirateEmpireBaseHabitat : Capital);
            }
            for (int i = 0; i < _Galaxy.ResourceSystem.FuelResources.Count; i++)
            {
                ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.FuelResources[i];
                if (resourceDefinition != null && CheckResourceAssignBuild(constructionShip, new Resource(resourceDefinition.ResourceID), empireHabitatsBeingMined, stellarObject))
                {
                    return true;
                }
            }
            ResourceList resourceList = new ResourceList();
            if (PirateEmpireBaseHabitat == null && DominantRace != null && DominantRace.CriticalResources.Count > 0)
            {
                resourceList.AddRange(DominantRace.CriticalResources.ResolveResources());
            }
            for (int j = 0; j < resourceList.Count; j++)
            {
                if (CheckResourceAssignBuild(constructionShip, resourceList[j], empireHabitatsBeingMined, stellarObject))
                {
                    return true;
                }
            }
            ResourceList resourceList2 = ResolveOrderedNonFuelNonCriticalResources(resourceList);
            for (int k = 0; k < resourceList2.Count; k++)
            {
                if (CheckResourceAssignBuild(constructionShip, resourceList2[k], empireHabitatsBeingMined, stellarObject))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckResourceAssignBuild(BuiltObject constructionShip, Resource resource, HabitatList empireHabitatsBeingMined, StellarObject buildResourcePickupPoint)
        {
            return CheckResourceAssignBuild(constructionShip, resource, empireHabitatsBeingMined, buildResourcePickupPoint, isCriticalEmpireResource: false);
        }

        private bool CheckResourceAssignBuild(BuiltObject constructionShip, Resource resource, HabitatList empireHabitatsBeingMined, StellarObject buildResourcePickupPoint, bool isCriticalEmpireResource)
        {
            Habitat habitat = CheckResourceSupplyMeetsExpected(resource, isCriticalEmpireResource, empireHabitatsBeingMined);
            if (habitat != null && buildResourcePickupPoint != null && constructionShip.DistanceWithinRange(buildResourcePickupPoint.Xpos, buildResourcePickupPoint.Ypos, habitat.Xpos, habitat.Ypos, 0.1) && !empireHabitatsBeingMined.Contains(habitat))
            {
                HabitatResourceList habitatResourceList = new HabitatResourceList();
                if (habitat.Resources != null)
                {
                    habitatResourceList = habitat.Resources.Clone();
                }
                if (constructionShip.SubRole == BuiltObjectSubRole.ConstructionShip && constructionShip.IsAutoControlled && constructionShip.IsShipYard && (constructionShip.Mission == null || constructionShip.Mission.Type == BuiltObjectMissionType.Undefined))
                {
                    Design design = null;
                    if (habitatResourceList.ContainsGroup(ResourceGroup.Gas))
                    {
                        design = Designs.FindNewestCanBuild(BuiltObjectSubRole.GasMiningStation);
                    }
                    if (habitatResourceList.ContainsGroup(ResourceGroup.Mineral))
                    {
                        design = Designs.FindNewestCanBuild(BuiltObjectSubRole.MiningStation);
                    }
                    if (design == null && habitat.Resources.ContainsGroup(ResourceGroup.Luxury))
                    {
                        design = Designs.FindNewestCanBuild(BuiltObjectSubRole.MiningStation);
                    }
                    if (design != null)
                    {
                        _Galaxy.SelectRelativeHabitatSurfacePoint(habitat, out var x, out var y);
                        constructionShip.AssignMission(BuiltObjectMissionType.Build, habitat, null, design, x, y, BuiltObjectMissionPriority.Normal);
                        int num = _ResourceTargets.IndexOf(habitat);
                        if (num >= 0)
                        {
                            _ResourceTargets.RemoveAt(num);
                        }
                        empireHabitatsBeingMined.Add(habitat);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckResourceSupplyMeetsExpected(Resource resource, bool isCriticalEmpireResource)
        {
            return CheckResourceSupplyMeetsExpected(resource, isCriticalEmpireResource, 1.0);
        }

        public bool CheckResourceSupplyMeetsExpected(Resource resource, bool isCriticalEmpireResource, double oversupplyFactor)
        {
            int num = 1;
            if (PirateEmpireBaseHabitat == null)
            {
                int num2 = 3;
                int num3 = 3;
                int num4 = 2;
                if (!CheckEmpireHasHyperDriveTech(this))
                {
                    num2 = 2;
                    num3 = 1;
                    num4 = 1;
                }
                if (resource.IsFuel)
                {
                    num = num2 + (int)((double)Colonies.Count / 2.0);
                    num = Math.Min(50, num);
                }
                else if ((double)resource.RelativeImportance > 0.5)
                {
                    num = num3 + (int)((double)Colonies.Count / 3.0);
                    num = Math.Min(50, num);
                }
                else if ((double)resource.RelativeImportance > 0.25)
                {
                    num = num4 + (int)((double)Colonies.Count / 4.0);
                    num = Math.Min(50, num);
                }
                else
                {
                    num = 1 + Colonies.Count / 6;
                }
                if (isCriticalEmpireResource)
                {
                    num = Math.Max(num, num4 + Colonies.Count / 3);
                    num = Math.Min(40, num);
                }
            }
            else if (resource.IsFuel)
            {
                num = 2 + (int)((double)SpacePorts.Count / 2.0);
                num = Math.Min(50, num);
            }
            else if ((double)resource.RelativeImportance > 0.5)
            {
                num = 1 + (int)((double)SpacePorts.Count / 4.0);
                num = Math.Min(50, num);
            }
            else
            {
                num = 1;
            }
            num = (int)((double)num * oversupplyFactor);
            int num5 = _Galaxy.CountResourceSourcesForEmpire(this, resource.ResourceID, includeConstructionShipsBuildingMiningStations: true);
            if (num5 < num)
            {
                return false;
            }
            return true;
        }

        public Habitat CheckResourceSupplyMeetsExpected(Resource resource)
        {
            return CheckResourceSupplyMeetsExpected(resource, isCriticalEmpireResource: false, null);
        }

        public Habitat CheckResourceSupplyMeetsExpected(Resource resource, bool isCriticalEmpireResource, HabitatList empireHabitatsBeingMined)
        {
            Habitat habitat = null;
            if (!CheckResourceSupplyMeetsExpected(resource, isCriticalEmpireResource))
            {
                int num = 0;
                while (habitat == null && num < 50)
                {
                    habitat = IdentifyStrategicResourceSupplySource(resource);
                    if (habitat == null)
                    {
                        break;
                    }
                    if (CheckNearPirateBase(habitat, habitat.Xpos, habitat.Ypos, this))
                    {
                        int num2 = _ResourceTargets.IndexOf(habitat);
                        if (num2 >= 0)
                        {
                            _ResourceTargets.RemoveAt(num2);
                        }
                        habitat = null;
                    }
                    else if (empireHabitatsBeingMined == null)
                    {
                        if (_Galaxy.DetermineMiningStationAtHabitatForEmpire(habitat, this) != null)
                        {
                            int num3 = _ResourceTargets.IndexOf(habitat);
                            if (num3 >= 0)
                            {
                                _ResourceTargets.RemoveAt(num3);
                            }
                            habitat = null;
                        }
                    }
                    else if (empireHabitatsBeingMined.Contains(habitat))
                    {
                        int num4 = _ResourceTargets.IndexOf(habitat);
                        if (num4 >= 0)
                        {
                            _ResourceTargets.RemoveAt(num4);
                        }
                        habitat = null;
                    }
                    num++;
                }
            }
            return habitat;
        }

        public void EnsureStrategicResourceSupply()
        {
            ResourceList resourcesAlreadySupplied = DetermineResourcesEmpireSupplies();
            HabitatList habitatList = new HabitatList();
            for (int i = 0; i < _Galaxy.ResourceSystem.FuelResources.Count; i++)
            {
                ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.FuelResources[i];
                if (resourceDefinition != null)
                {
                    int num = 2 + Colonies.Count / 3;
                    int num2 = _Galaxy.CountResourceSourcesForEmpire(this, resourceDefinition.ResourceID);
                    if (num2 < num)
                    {
                        ForceResourceSupply(new Resource(resourceDefinition.ResourceID), habitatList);
                    }
                }
            }
            for (int j = 0; j < _Galaxy.ResourceSystem.FuelResources.Count; j++)
            {
                ResourceDefinition resourceDefinition2 = _Galaxy.ResourceSystem.FuelResources[j];
                if (resourceDefinition2 != null && !resourceDefinition2.IsFuel)
                {
                    CheckResourceSupply(new Resource(resourceDefinition2.ResourceID), resourcesAlreadySupplied, habitatList);
                }
            }
            for (int k = 0; k < ConstructionShips.Count; k++)
            {
                BuiltObject builtObject = ConstructionShips[k];
                if (builtObject.SubRole == BuiltObjectSubRole.ConstructionShip && builtObject.IsShipYard && builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Build && builtObject.Mission.TargetHabitat != null)
                {
                    Habitat targetHabitat = builtObject.Mission.TargetHabitat;
                    while (habitatList.Contains(targetHabitat))
                    {
                        habitatList.Remove(targetHabitat);
                    }
                }
            }
            for (int l = 0; l < MiningStations.Count; l++)
            {
                BuiltObject builtObject2 = MiningStations[l];
                if ((builtObject2.SubRole == BuiltObjectSubRole.GasMiningStation || builtObject2.SubRole == BuiltObjectSubRole.MiningStation) && builtObject2.ParentHabitat != null)
                {
                    while (habitatList.Contains(builtObject2.ParentHabitat))
                    {
                        habitatList.Remove(builtObject2.ParentHabitat);
                    }
                }
            }
            int num3 = 0;
            for (int m = 0; m < ConstructionShips.Count; m++)
            {
                BuiltObject builtObject3 = ConstructionShips[m];
                if (builtObject3.SubRole == BuiltObjectSubRole.ConstructionShip && builtObject3.IsAutoControlled && builtObject3.IsShipYard && (builtObject3.Mission == null || builtObject3.Mission.Type == BuiltObjectMissionType.Undefined) && num3 < habitatList.Count)
                {
                    Design design = null;
                    HabitatResourceList habitatResourceList = new HabitatResourceList();
                    if (habitatList[num3].Resources != null)
                    {
                        habitatResourceList = habitatList[num3].Resources.Clone();
                    }
                    if (habitatResourceList.ContainsGroup(ResourceGroup.Gas))
                    {
                        design = Designs.FindNewestCanBuild(BuiltObjectSubRole.GasMiningStation);
                    }
                    if (habitatResourceList.ContainsGroup(ResourceGroup.Mineral))
                    {
                        design = Designs.FindNewestCanBuild(BuiltObjectSubRole.MiningStation);
                    }
                    if (design != null)
                    {
                        _Galaxy.SelectRelativeHabitatSurfacePoint(habitatList[num3], out var x, out var y);
                        builtObject3.AssignMission(BuiltObjectMissionType.Build, habitatList[num3], null, design, x, y, BuiltObjectMissionPriority.Normal);
                    }
                    num3++;
                }
            }
        }

        private void ForceResourceSupply(Resource resource, HabitatList resourceHabitats)
        {
            Habitat habitat = IdentifyStrategicResourceSupplySource(resource);
            if (habitat != null && !resourceHabitats.Contains(habitat))
            {
                resourceHabitats.Add(habitat);
            }
        }

        private void CheckResourceSupply(Resource resource, ResourceList resourcesAlreadySupplied, HabitatList resourceHabitats)
        {
            if (!resourcesAlreadySupplied.Contains(resource))
            {
                Habitat habitat = IdentifyStrategicResourceSupplySource(resource);
                if (habitat != null && !resourceHabitats.Contains(habitat))
                {
                    resourceHabitats.Add(habitat);
                }
            }
        }

        private Habitat IdentifyStrategicResourceSupplySource(Resource resource)
        {
            int num = 0;
            Habitat result = null;
            for (int i = 0; i < _ResourceTargets.Count; i++)
            {
                HabitatPrioritization habitatPrioritization = _ResourceTargets[i];
                if (habitatPrioritization != null && habitatPrioritization.Habitat != null && habitatPrioritization.Habitat.Resources != null)
                {
                    int num2 = habitatPrioritization.Habitat.Resources.IndexOf(resource.ResourceID, 0);
                    if (num2 >= 0 && habitatPrioritization.Priority > num)
                    {
                        num = habitatPrioritization.Priority;
                        result = habitatPrioritization.Habitat;
                        break;
                    }
                }
            }
            return result;
        }

        public void WarnOfIncomingEnemyFleetsAndPlanetDestroyers(Empire attackedEmpire)
        {
            if (ObtainDiplomaticRelation(attackedEmpire).Type != DiplomaticRelationType.War)
            {
                return;
            }
            if (PlanetDestroyers != null)
            {
                for (int i = 0; i < PlanetDestroyers.Count; i++)
                {
                    BuiltObject builtObject = PlanetDestroyers[i];
                    bool flag = false;
                    if (builtObject.Mission != null)
                    {
                        if (builtObject.Mission.Type == BuiltObjectMissionType.Attack)
                        {
                            flag = true;
                        }
                        else if (builtObject.Mission.Type == BuiltObjectMissionType.WaitAndAttack)
                        {
                            Command command = builtObject.Mission.FastPeekCurrentCommand();
                            if (command != null && command.Action == CommandAction.Attack)
                            {
                                flag = true;
                            }
                        }
                    }
                    if (!flag)
                    {
                        continue;
                    }
                    Empire empire = BuiltObjectMission.ResolveMissionTargetEmpire(builtObject.Mission);
                    if (empire != attackedEmpire)
                    {
                        continue;
                    }
                    bool flag2 = false;
                    if (_Galaxy.GlobalVictoryConditions != null)
                    {
                        if (_Galaxy.GlobalVictoryConditions.DefendHabitat != null && _Galaxy.GlobalVictoryConditions.DefendHabitat == builtObject.Mission.Target)
                        {
                            flag2 = true;
                        }
                        else if (_Galaxy.GlobalVictoryConditions.TargetHabitat != null && _Galaxy.GlobalVictoryConditions.TargetHabitat == builtObject.Mission.Target)
                        {
                            flag2 = true;
                        }
                    }
                    int num = empire.IncomingEnemyFleetsAndPlanetDestroyers.IndexOf(builtObject);
                    if (num >= 0)
                    {
                        FleetAttack fleetAttack = empire.IncomingEnemyFleetsAndPlanetDestroyers[num];
                        if (fleetAttack.Target == builtObject.Mission.Target || builtObject.Mission.Target == null || empire.ObtainDiplomaticRelation(builtObject.Empire).Type != DiplomaticRelationType.War || !empire.IsObjectVisibleToThisEmpire(builtObject))
                        {
                            continue;
                        }
                        fleetAttack.Target = builtObject.Mission.Target;
                        fleetAttack.WarningDate = _Galaxy.CurrentStarDate;
                        string description = ResolveAttackWarningDescription(fleetAttack, empire);
                        empire.SendMessageToEmpire(empire, EmpireMessageType.IncomingEnemyFleet, builtObject, description);
                        if (!flag2)
                        {
                            continue;
                        }
                        for (int j = 0; j < empire.DiplomaticRelations.Count; j++)
                        {
                            DiplomaticRelation diplomaticRelation = empire.DiplomaticRelations[j];
                            if (diplomaticRelation.OtherEmpire != empire && diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact)
                            {
                                if (diplomaticRelation.OtherEmpire.IncomingEnemyFleetsAndPlanetDestroyers.IndexOf(builtObject) < 0)
                                {
                                    FleetAttack item = new FleetAttack(builtObject, builtObject.Mission.Target, _Galaxy.CurrentStarDate);
                                    diplomaticRelation.OtherEmpire.IncomingEnemyFleetsAndPlanetDestroyers.Add(item);
                                }
                                empire.SendMessageToEmpire(diplomaticRelation.OtherEmpire, EmpireMessageType.IncomingEnemyFleet, builtObject, description);
                            }
                        }
                    }
                    else
                    {
                        if (empire.ObtainDiplomaticRelation(builtObject.Empire).Type != DiplomaticRelationType.War || !empire.IsObjectVisibleToThisEmpire(builtObject))
                        {
                            continue;
                        }
                        FleetAttack fleetAttack2 = new FleetAttack(builtObject, builtObject.Mission.Target, _Galaxy.CurrentStarDate);
                        empire.IncomingEnemyFleetsAndPlanetDestroyers.Add(fleetAttack2);
                        string description2 = ResolveAttackWarningDescription(fleetAttack2, empire);
                        empire.SendMessageToEmpire(empire, EmpireMessageType.IncomingEnemyFleet, builtObject, description2);
                        if (!flag2)
                        {
                            continue;
                        }
                        for (int k = 0; k < empire.DiplomaticRelations.Count; k++)
                        {
                            DiplomaticRelation diplomaticRelation2 = empire.DiplomaticRelations[k];
                            if (diplomaticRelation2.OtherEmpire != empire && diplomaticRelation2.Type == DiplomaticRelationType.MutualDefensePact)
                            {
                                if (diplomaticRelation2.OtherEmpire.IncomingEnemyFleetsAndPlanetDestroyers.IndexOf(builtObject) < 0)
                                {
                                    FleetAttack item2 = new FleetAttack(builtObject, builtObject.Mission.Target, _Galaxy.CurrentStarDate);
                                    diplomaticRelation2.OtherEmpire.IncomingEnemyFleetsAndPlanetDestroyers.Add(item2);
                                }
                                empire.SendMessageToEmpire(diplomaticRelation2.OtherEmpire, EmpireMessageType.IncomingEnemyFleet, builtObject, description2);
                            }
                        }
                    }
                }
            }
            for (int l = 0; l < ShipGroups.Count; l++)
            {
                ShipGroup shipGroup = ShipGroups[l];
                if (shipGroup.Mission == null || shipGroup.LeadShip == null)
                {
                    continue;
                }
                bool flag3 = false;
                if (shipGroup.Mission.Type == BuiltObjectMissionType.Attack || shipGroup.Mission.Type == BuiltObjectMissionType.Bombard)
                {
                    flag3 = true;
                }
                else if (shipGroup.Mission.Type == BuiltObjectMissionType.WaitAndAttack || shipGroup.Mission.Type == BuiltObjectMissionType.WaitAndBombard)
                {
                    Command command2 = shipGroup.LeadShip.Mission.FastPeekCurrentCommand();
                    if (command2 != null && command2.Action == CommandAction.Attack)
                    {
                        flag3 = true;
                    }
                }
                if (!flag3)
                {
                    continue;
                }
                _ = shipGroup.LeadShip.CurrentSpeed;
                _ = (float)shipGroup.LeadShip.TopSpeed;
                Empire empire2 = BuiltObjectMission.ResolveMissionTargetEmpire(shipGroup.Mission);
                if (empire2 != attackedEmpire)
                {
                    continue;
                }
                int num2 = empire2.IncomingEnemyFleetsAndPlanetDestroyers.IndexOf(shipGroup);
                if (num2 >= 0)
                {
                    FleetAttack fleetAttack3 = empire2.IncomingEnemyFleetsAndPlanetDestroyers[num2];
                    if (fleetAttack3.Target != shipGroup.Mission.Target && shipGroup.Mission.Target != null && empire2.ObtainDiplomaticRelation(shipGroup.Empire).Type == DiplomaticRelationType.War && empire2.IsObjectVisibleToThisEmpire(shipGroup.LeadShip))
                    {
                        fleetAttack3.Target = shipGroup.Mission.Target;
                        fleetAttack3.WarningDate = _Galaxy.CurrentStarDate;
                        string description3 = ResolveAttackWarningDescription(fleetAttack3, empire2);
                        empire2.SendMessageToEmpire(empire2, EmpireMessageType.IncomingEnemyFleet, shipGroup, description3);
                    }
                }
                else if (empire2.ObtainDiplomaticRelation(shipGroup.Empire).Type == DiplomaticRelationType.War && empire2.IsObjectVisibleToThisEmpire(shipGroup.LeadShip))
                {
                    FleetAttack fleetAttack4 = new FleetAttack(shipGroup, shipGroup.Mission.Target, _Galaxy.CurrentStarDate);
                    empire2.IncomingEnemyFleetsAndPlanetDestroyers.Add(fleetAttack4);
                    string description4 = ResolveAttackWarningDescription(fleetAttack4, empire2);
                    empire2.SendMessageToEmpire(empire2, EmpireMessageType.IncomingEnemyFleet, shipGroup, description4);
                }
            }
        }

        private string ResolveAttackWarningDescription(FleetAttack fleetAttack, Empire targetEmpire)
        {
            string result = string.Empty;
            if (fleetAttack.Fleet != null && fleetAttack.Fleet.Mission != null)
            {
                string arg = string.Empty;
                if (fleetAttack.Fleet.Mission.TargetBuiltObject != null)
                {
                    arg = fleetAttack.Fleet.Mission.TargetBuiltObject.Name;
                }
                else if (fleetAttack.Fleet.Mission.TargetCreature != null)
                {
                    arg = fleetAttack.Fleet.Mission.TargetCreature.Name;
                }
                else if (fleetAttack.Fleet.Mission.TargetHabitat != null)
                {
                    Habitat targetHabitat = fleetAttack.Fleet.Mission.TargetHabitat;
                    Habitat habitat = Galaxy.DetermineHabitatSystemStar(targetHabitat);
                    string text = Galaxy.ResolveSectorDescription(Galaxy.ResolveSector(targetHabitat.Xpos, targetHabitat.Ypos));
                    arg = string.Format(TextResolver.GetText("Location Planet"), Galaxy.ResolveDescription(targetHabitat.Type).ToLower(CultureInfo.InvariantCulture), Galaxy.ResolveDescription(targetHabitat.Category).ToLower(CultureInfo.InvariantCulture), targetHabitat.Name, habitat.Name, text);
                }
                else if (fleetAttack.Fleet.Mission.TargetShipGroup != null)
                {
                    arg = fleetAttack.Fleet.Mission.TargetShipGroup.Name;
                }
                result = string.Format(TextResolver.GetText("Incoming Enemy Fleet"), fleetAttack.Fleet.Name, fleetAttack.Fleet.Empire.Name, arg);
            }
            else if (fleetAttack.PlanetDestroyer != null && fleetAttack.PlanetDestroyer.Mission != null)
            {
                string arg2 = string.Empty;
                if (fleetAttack.PlanetDestroyer.Mission.TargetBuiltObject != null)
                {
                    arg2 = fleetAttack.PlanetDestroyer.Mission.TargetBuiltObject.Name;
                }
                else if (fleetAttack.PlanetDestroyer.Mission.TargetCreature != null)
                {
                    arg2 = fleetAttack.PlanetDestroyer.Mission.TargetCreature.Name;
                }
                else if (fleetAttack.PlanetDestroyer.Mission.TargetHabitat != null)
                {
                    Habitat targetHabitat2 = fleetAttack.PlanetDestroyer.Mission.TargetHabitat;
                    Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(targetHabitat2);
                    string text2 = Galaxy.ResolveSectorDescription(Galaxy.ResolveSector(targetHabitat2.Xpos, targetHabitat2.Ypos));
                    arg2 = string.Format(TextResolver.GetText("Location Planet"), Galaxy.ResolveDescription(targetHabitat2.Type).ToLower(CultureInfo.InvariantCulture), Galaxy.ResolveDescription(targetHabitat2.Category).ToLower(CultureInfo.InvariantCulture), targetHabitat2.Name, habitat2.Name, text2);
                }
                else if (fleetAttack.PlanetDestroyer.Mission.TargetShipGroup != null)
                {
                    arg2 = fleetAttack.PlanetDestroyer.Mission.TargetShipGroup.Name;
                }
                result = string.Format(TextResolver.GetText("Incoming Enemy Planet Destroyer"), fleetAttack.PlanetDestroyer.Name, fleetAttack.PlanetDestroyer.Empire.Name, arg2);
            }
            return result;
        }

        public BuiltObject PurchaseNewBuiltObject(Design design, Habitat constructionYard, bool isStateOwned, bool isAutoControlled)
        {
            return PurchaseNewBuiltObject(design, constructionYard, (int)constructionYard.Xpos, (int)constructionYard.Ypos, isStateOwned, isAutoControlled);
        }

        public BuiltObject PurchaseNewBuiltObject(Design design, Habitat constructionYard, int x, int y, bool isStateOwned, bool isAutoControlled)
        {
            BuiltObject builtObject = null;
            CargoList resourcesToOrder = new CargoList();
            double num = design.CalculateCurrentPurchasePrice(_Galaxy);
            bool flag = false;
            if (isStateOwned)
            {
                if (num <= StateMoney)
                {
                    flag = true;
                }
            }
            else if (num <= GetPrivateFunds())
            {
                flag = true;
            }
            if (flag)
            {
                design.BuildCount++;
                builtObject = ((design.SubRole != BuiltObjectSubRole.Outpost && design.SubRole != BuiltObjectSubRole.SmallSpacePort && design.SubRole != BuiltObjectSubRole.MediumSpacePort && design.SubRole != BuiltObjectSubRole.LargeSpacePort) ? new BuiltObject(design, _Galaxy.GenerateBuiltObjectName(design, constructionYard), _Galaxy) : new BuiltObject(design, constructionYard.Name + " " + TextResolver.GetText("Space Port"), _Galaxy));
                builtObject.IsAutoControlled = isAutoControlled;
                builtObject.PurchasePrice = num;
                if (constructionYard.ConstructionQueue != null && constructionYard.ConstructionQueue.AddBuiltObjectToConstruct(builtObject))
                {
                    double x2 = 0.0;
                    double y2 = 0.0;
                    if (builtObject.SubRole == BuiltObjectSubRole.Outpost || builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort || builtObject.SubRole == BuiltObjectSubRole.EnergyResearchStation || builtObject.SubRole == BuiltObjectSubRole.WeaponsResearchStation || builtObject.SubRole == BuiltObjectSubRole.HighTechResearchStation || builtObject.SubRole == BuiltObjectSubRole.MonitoringStation || builtObject.SubRole == BuiltObjectSubRole.DefensiveBase || builtObject.SubRole == BuiltObjectSubRole.GenericBase)
                    {
                        builtObject.ParentHabitat = constructionYard;
                        if (Math.Abs((double)x - constructionYard.Xpos) < 3.0 && Math.Abs((double)y - constructionYard.Ypos) < 3.0)
                        {
                            _Galaxy.SelectRelativeHabitatSurfacePoint(constructionYard, out x2, out y2);
                            switch (builtObject.SubRole)
                            {
                                case BuiltObjectSubRole.Outpost:
                                case BuiltObjectSubRole.SmallSpacePort:
                                case BuiltObjectSubRole.MediumSpacePort:
                                case BuiltObjectSubRole.LargeSpacePort:
                                    {
                                        double range = (double)(constructionYard.Diameter / 6) + 15.0;
                                        _Galaxy.SelectRelativePoint(range, out x2, out y2);
                                        break;
                                    }
                                default:
                                    DetermineOrbitalBaseLocation(constructionYard, out x2, out y2);
                                    break;
                            }
                        }
                        else
                        {
                            x2 = (double)x - constructionYard.Xpos;
                            y2 = (double)y - constructionYard.Ypos;
                            double num2 = (double)(constructionYard.Diameter / 2) + 250.0;
                            if (builtObject.SubRole == BuiltObjectSubRole.Outpost || builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort)
                            {
                                num2 = (double)(constructionYard.Diameter / 8) + 10.0;
                            }
                            double num3 = _Galaxy.CalculateDistance(x2, y2, 0.0, 0.0);
                            if (num3 > num2)
                            {
                                double num4 = _Galaxy.CalculateAngleFromCoords(x2, y2, 0.0, 0.0, num3);
                                x2 = Math.Cos(num4) * num2;
                                y2 = Math.Sin(num4) * num2;
                            }
                        }
                        builtObject.ParentOffsetX = x2;
                        builtObject.ParentOffsetY = y2;
                        builtObject.Heading = _Galaxy.SelectRandomHeading();
                        builtObject.TargetHeading = builtObject.Heading;
                        builtObject.NearestSystemStar = Galaxy.DetermineHabitatSystemStar(constructionYard);
                    }
                    AddBuiltObjectToGalaxy(builtObject, constructionYard, offsetLocationFromParent: false, isStateOwned, (int)x2, (int)y2);
                    builtObject.BuiltAt = constructionYard;
                    ProcureConstructionComponents(builtObject, constructionYard, out resourcesToOrder);
                    if (PirateEmpireBaseHabitat != null)
                    {
                        StateMoney -= num;
                        PirateEconomy.PerformExpense(num, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                    }
                    else if (isStateOwned)
                    {
                        StateMoney -= num;
                        PirateEconomy.PerformExpense(num, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                    }
                    else
                    {
                        PerformPrivateTransaction(0.0 - num);
                    }
                }
                else
                {
                    design.BuildCount--;
                    builtObject = null;
                }
            }
            foreach (Cargo item in resourcesToOrder)
            {
                CreateOrder(constructionYard, item.CommodityResource, item.Amount, isState: false, OrderType.ConstructionShortage);
            }
            return builtObject;
        }

        public BuiltObject PurchaseNewBuiltObject(Design design, BuiltObject constructionYard, bool isStateOwned, bool isAutoControlled)
        {
            BuiltObject builtObject = null;
            CargoList resourcesToOrder = new CargoList();
            if (design.SubRole == BuiltObjectSubRole.Outpost || design.SubRole == BuiltObjectSubRole.SmallSpacePort || design.SubRole == BuiltObjectSubRole.MediumSpacePort || design.SubRole == BuiltObjectSubRole.LargeSpacePort)
            {
                return null;
            }
            double num = design.CalculateCurrentPurchasePrice(_Galaxy);
            bool flag = false;
            if (isStateOwned)
            {
                if (num <= StateMoney)
                {
                    flag = true;
                }
            }
            else if (num <= GetPrivateFunds())
            {
                flag = true;
            }
            if (flag)
            {
                design.BuildCount++;
                if (design.SubRole == BuiltObjectSubRole.MiningStation)
                {
                    builtObject = new BuiltObject(design, constructionYard.Name + " " + TextResolver.GetText("Mining Station"), _Galaxy);
                }
                else if (design.SubRole == BuiltObjectSubRole.GasMiningStation)
                {
                    builtObject = new BuiltObject(design, constructionYard.Name + " " + TextResolver.GetText("Gas Mining Station"), _Galaxy);
                }
                else
                {
                    builtObject = new BuiltObject(design, _Galaxy.GenerateBuiltObjectName(design), _Galaxy);
                    if (constructionYard.ParentHabitat != null)
                    {
                        builtObject.Name = _Galaxy.GenerateBuiltObjectName(design, constructionYard.ParentHabitat);
                    }
                }
                builtObject.IsAutoControlled = isAutoControlled;
                builtObject.PurchasePrice = num;
                if (constructionYard.ConstructionQueue != null && constructionYard.ConstructionQueue.AddBuiltObjectToConstruct(builtObject))
                {
                    if (builtObject.SubRole == BuiltObjectSubRole.MiningStation || builtObject.SubRole == BuiltObjectSubRole.GasMiningStation)
                    {
                        builtObject.ParentHabitat = constructionYard.ParentHabitat;
                        _Galaxy.SelectRelativeHabitatSurfacePoint(constructionYard.ParentHabitat, out var x, out var y);
                        builtObject.ParentOffsetX = x;
                        builtObject.ParentOffsetY = y;
                        builtObject.Heading = _Galaxy.SelectRandomHeading();
                        builtObject.TargetHeading = builtObject.Heading;
                        builtObject.NearestSystemStar = Galaxy.DetermineHabitatSystemStar(constructionYard.ParentHabitat);
                    }
                    AddBuiltObjectToGalaxy(builtObject, constructionYard, offsetLocationFromParent: false, isStateOwned);
                    builtObject.BuiltAt = constructionYard;
                    ProcureConstructionComponents(builtObject, constructionYard, orderPreciseResourceAmounts: true, out resourcesToOrder);
                    if (PirateEmpireBaseHabitat != null)
                    {
                        StateMoney -= num;
                        PirateEconomy.PerformExpense(num, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                    }
                    else if (isStateOwned)
                    {
                        StateMoney -= num;
                        PirateEconomy.PerformExpense(num, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                    }
                    else
                    {
                        PerformPrivateTransaction(0.0 - num);
                    }
                }
                else
                {
                    design.BuildCount--;
                    builtObject = null;
                }
            }
            foreach (Cargo item in resourcesToOrder)
            {
                CreateOrder(constructionYard, item.CommodityResource, item.Amount, isState: false, OrderType.ConstructionShortage);
            }
            return builtObject;
        }

        public void AddResortIncome(double amount)
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            long num = currentStarDate % (Galaxy.RealSecondsInGalacticYear * 1000);
            long num2 = currentStarDate - num;
            if (_LastResortIncomeAddDate < num2)
            {
                _ThisYearsResortIncome = 0.0;
            }
            _ThisYearsResortIncome += amount;
            _LastResortIncomeAddDate = currentStarDate;
        }

        public void CheckAgeVariableIncome()
        {
            _UseAveragedVariableIncome = true;
            long currentStarDate = _Galaxy.CurrentStarDate;
            long num = currentStarDate % (Galaxy.RealSecondsInGalacticYear * 1000);
            long num2 = currentStarDate - num;
            if (_LastVariableIncomeUpdate < num2)
            {
                AgeVariableIncomeValues(ThisYearsSpacePortIncome + ThisYearsResortIncome);
                ResetYearlyIncome();
                _LastVariableIncomeUpdate = num2;
            }
        }

        private void ResetYearlyIncome()
        {
            for (int i = 0; i < SpacePorts.Count; i++)
            {
                SpacePorts[i].CurrentYearsIncome = 0.0;
            }
            for (int j = 0; j < MiningStations.Count; j++)
            {
                MiningStations[j].CurrentYearsIncome = 0.0;
            }
            for (int k = 0; k < ResortBases.Count; k++)
            {
                ResortBases[k].CurrentYearsIncome = 0.0;
            }
        }

        private void AgeVariableIncomeValues(double thisYearsVariableIncome)
        {
            if (_VariableIncome == null)
            {
                _VariableIncome = new List<double>();
            }
            int num = 3;
            _VariableIncome.Insert(0, thisYearsVariableIncome);
            if (_VariableIncome.Count > num)
            {
                _VariableIncome.RemoveAt(_VariableIncome.Count - 1);
            }
        }

        public double ObtainAveragedVariableIncome()
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.7;
            for (int i = 0; i < _VariableIncome.Count; i++)
            {
                num += _VariableIncome[i] * num3;
                num2 += num3;
                num3 *= 0.7;
            }
            return num / num2;
        }

        public void PurchaseStateFuel(double fuelCost)
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            long num = currentStarDate % (Galaxy.RealSecondsInGalacticYear * 1000);
            long num2 = currentStarDate - num;
            if (_DateOfLastStateFuelCost < num2)
            {
                _ThisYearsStateFuelCosts = 0.0;
            }
            _DateOfLastStateFuelCost = currentStarDate;
            _ThisYearsStateFuelCosts += fuelCost;
        }

        public void PurchasePrivateFuel(double fuelCost)
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            long num = currentStarDate % (Galaxy.RealSecondsInGalacticYear * 1000);
            long num2 = currentStarDate - num;
            if (_DateOfLastPrivateFuelCost < num2)
            {
                _ThisYearsPrivateFuelCosts = 0.0;
            }
            _DateOfLastPrivateFuelCost = currentStarDate;
            _ThisYearsPrivateFuelCosts += fuelCost;
        }

        public bool CheckShouldAttemptColonization(Habitat habitat)
        {
            bool result = true;
            EmpireSystemSummary dominantEmpire = _Galaxy.Systems[habitat.SystemIndex].DominantEmpire;
            if (dominantEmpire != null && dominantEmpire.Empire != null && dominantEmpire.Empire != this && dominantEmpire.TotalStrategicValue > 100000 && (habitat.Population == null || habitat.Population.TotalAmount < 20000000))
            {
                result = false;
            }
            if (!DetermineColonizeLowQualityHabitat(habitat))
            {
                result = false;
            }
            int num = _Galaxy.CheckColonizationLikeliness(habitat, DominantRace);
            if (num < -3)
            {
                result = false;
            }
            return result;
        }

        public ForceStructureProjectionList RefactorForceStructureProjectionsToCosts(ForceStructureProjectionList projections)
        {
            return RefactorForceStructureProjectionsToCosts(projections, includeCashflowCheck: true);
        }

        public ForceStructureProjectionList RefactorForceStructureProjectionsToCosts(ForceStructureProjectionList projections, bool includeCashflowCheck)
        {
            double availableRevenue = CalculateAccurateAnnualCashflow();
            return RefactorForceStructureProjectionsToCosts(projections, availableRevenue, 0.0, 0.0, includeCashflowCheck);
        }

        public ForceStructureProjectionList RefactorForceStructureProjectionsToCosts(ForceStructureProjectionList projections, bool includeCashflowCheck, out double totalSupportCosts, out double totalPurchaseCosts, bool randomizedOrder)
        {
            double availableRevenue = CalculateAccurateAnnualCashflow();
            return RefactorForceStructureProjectionsToCosts(projections, availableRevenue, 0.0, 0.0, includeCashflowCheck, out totalSupportCosts, out totalPurchaseCosts, randomizedOrder);
        }

        public ForceStructureProjectionList RefactorForceStructureProjectionsToCosts(ForceStructureProjectionList projections, double availableRevenue, double otherSupportCosts, double otherPurchaseCosts, bool includeCashflowCheck)
        {
            double totalSupportCosts = 0.0;
            double totalPurchaseCosts = 0.0;
            return RefactorForceStructureProjectionsToCosts(projections, availableRevenue, otherSupportCosts, otherPurchaseCosts, includeCashflowCheck, out totalSupportCosts, out totalPurchaseCosts, randomizedOrder: true);
        }

        public ForceStructureProjectionList RefactorForceStructureProjectionsToCosts(ForceStructureProjectionList projections, double availableRevenue, double otherSupportCosts, double otherPurchaseCosts, bool includeCashflowCheck, out double totalSupportCosts, out double totalPurchaseCosts, bool randomizedOrder)
        {
            ForceStructureProjectionList forceStructureProjectionList = new ForceStructureProjectionList();
            totalSupportCosts = otherSupportCosts;
            totalPurchaseCosts = otherPurchaseCosts;
            ForceStructureProjectionList forceStructureProjectionList2 = new ForceStructureProjectionList();
            forceStructureProjectionList2.AddRange(projections);
            int iterationCount = 0;
            while (Galaxy.ConditionCheckLimit(forceStructureProjectionList2.Count > 0, 100, ref iterationCount))
            {
                ForceStructureProjection forceStructureProjection = null;
                forceStructureProjection = ((!randomizedOrder) ? forceStructureProjectionList2[0] : forceStructureProjectionList2[Galaxy.Rnd.Next(0, forceStructureProjectionList2.Count)]);
                if (forceStructureProjection != null)
                {
                    Design design = Designs.FindNewestCanBuild(forceStructureProjection.SubRole);
                    if (design != null)
                    {
                        int num = 0;
                        if (forceStructureProjection.Amount > 0)
                        {
                            double num2 = design.CalculateCurrentPurchasePrice(_Galaxy);
                            double num3 = CalculateSupportCost(design);
                            for (int i = 0; i < forceStructureProjection.Amount; i++)
                            {
                                if (!(totalPurchaseCosts + num2 <= StateMoney))
                                {
                                    break;
                                }
                                if (includeCashflowCheck && !(availableRevenue - totalSupportCosts >= num3))
                                {
                                    break;
                                }
                                totalPurchaseCosts += num2;
                                totalSupportCosts += num3;
                                num++;
                            }
                        }
                        if (num > 0)
                        {
                            ForceStructureProjection item = new ForceStructureProjection(forceStructureProjection.SubRole, num, forceStructureProjection.ProjectionDate);
                            forceStructureProjectionList.Add(item);
                        }
                    }
                }
                forceStructureProjectionList2.Remove(forceStructureProjection);
            }
            return forceStructureProjectionList;
        }

        private void DirectConstruction()
        {
            int refusalCount = 0;
            List<CargoList> list = new List<CargoList>();
            BuiltObjectList builtObjectList = new BuiltObjectList();
            List<CargoList> list2 = new List<CargoList>();
            HabitatList habitatList = new HabitatList();
            CargoList resourcesToOrder = null;
            int num = 0;
            int num2 = 0;
            BuiltObjectList builtObjectList2 = new BuiltObjectList();
            builtObjectList2.AddRange(BuiltObjects);
            builtObjectList2.AddRange(PrivateBuiltObjects);
            for (int i = 0; i < builtObjectList2.Count; i++)
            {
                BuiltObject builtObject = builtObjectList2[i];
                if (builtObject.Role == BuiltObjectRole.Military)
                {
                    if (builtObject.UnbuiltOrDamagedComponentCount == 0)
                    {
                        num2++;
                    }
                }
                else if (builtObject.Role == BuiltObjectRole.Freight && builtObject.UnbuiltOrDamagedComponentCount == 0)
                {
                    num++;
                }
            }
            int num3 = 0;
            if (_PrivateForceStructureProjections != null)
            {
                ForceStructureProjection bySubRole = _PrivateForceStructureProjections.GetBySubRole(BuiltObjectSubRole.SmallFreighter);
                ForceStructureProjection bySubRole2 = _PrivateForceStructureProjections.GetBySubRole(BuiltObjectSubRole.MediumFreighter);
                ForceStructureProjection bySubRole3 = _PrivateForceStructureProjections.GetBySubRole(BuiltObjectSubRole.LargeFreighter);
                if (bySubRole != null)
                {
                    num3 += bySubRole.Amount;
                }
                if (bySubRole2 != null)
                {
                    num3 += bySubRole2.Amount;
                }
                if (bySubRole3 != null)
                {
                    num3 += bySubRole3.Amount;
                }
            }
            int num4 = 0;
            if (_StateForceStructureProjections != null)
            {
                ForceStructureProjection bySubRole4 = _StateForceStructureProjections.GetBySubRole(BuiltObjectSubRole.Escort);
                ForceStructureProjection bySubRole5 = _StateForceStructureProjections.GetBySubRole(BuiltObjectSubRole.Frigate);
                ForceStructureProjection bySubRole6 = _StateForceStructureProjections.GetBySubRole(BuiltObjectSubRole.Destroyer);
                ForceStructureProjection bySubRole7 = _StateForceStructureProjections.GetBySubRole(BuiltObjectSubRole.Cruiser);
                ForceStructureProjection bySubRole8 = _StateForceStructureProjections.GetBySubRole(BuiltObjectSubRole.CapitalShip);
                ForceStructureProjection bySubRole9 = _StateForceStructureProjections.GetBySubRole(BuiltObjectSubRole.Carrier);
                if (bySubRole4 != null)
                {
                    num4 += bySubRole4.Amount;
                }
                if (bySubRole5 != null)
                {
                    num4 += bySubRole5.Amount;
                }
                if (bySubRole6 != null)
                {
                    num4 += bySubRole6.Amount;
                }
                if (bySubRole7 != null)
                {
                    num4 += bySubRole7.Amount;
                }
                if (bySubRole8 != null)
                {
                    num4 += bySubRole8.Amount;
                }
                if (bySubRole9 != null)
                {
                    num4 += bySubRole9.Amount;
                }
            }
            int num5 = (int)((double)num3 * 0.4);
            int num6 = (int)((double)num4 * 0.25);
            bool flag = false;
            if (num >= num5 && num2 >= num6)
            {
                flag = true;
            }
            if (DominantRace != null && !DominantRace.Expanding)
            {
                flag = false;
            }
            _ = ThisYearsSpacePortIncome;
            CalculateAccurateAnnualIncome();
            double annualSupportCosts = 0.0;
            ForceStructureProjectionList forceStructureProjectionList = CurrentStateForceStructure(out annualSupportCosts);
            double num7 = CalculateAccurateAnnualCashflow();
            ForceStructureProjectionList forceStructureProjectionList2 = _StateForceStructureProjections.Diff(forceStructureProjectionList);
            forceStructureProjectionList2.Sort();
            double num8 = EstimateForceStructureSupportCost(forceStructureProjectionList2);
            double num9 = annualSupportCosts + num8;
            double num10 = StateMoney / num9;
            double num11 = 5.0;
            if (CheckAtWar())
            {
                num11 = 2.0;
            }
            if (num10 > num11)
            {
                num7 = Math.Max(num7, num8);
            }
            double num12 = 0.0;
            double num13 = 0.0;
            if (_ControlStateConstruction != 0)
            {
                int num14 = 0;
                for (int j = 0; j < BuiltObjects.Count; j++)
                {
                    BuiltObject builtObject2 = BuiltObjects[j];
                    if (builtObject2.SubRole == BuiltObjectSubRole.Outpost || builtObject2.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject2.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject2.SubRole == BuiltObjectSubRole.LargeSpacePort)
                    {
                        num14++;
                    }
                }
                int val = 1 + (int)((double)Colonies.Count / 3.0);
                int val2 = 1 + (int)(TotalPopulation / 5000000000L);
                val = Math.Min(val, val2);
                int newSpacePortAmount = val - num14;
                HabitatList habitatList2 = DetermineNewSpacePortLocations(Colonies, newSpacePortAmount, excludeColoniesWithEnemiesPresent: true);
                long num15 = (long)Policy.ConstructionSpaceportLargeColonyPopulationThreshold * 1000000L;
                long num16 = (long)Policy.ConstructionSpaceportMediumColonyPopulationThreshold * 1000000L;
                long num17 = (long)Policy.ConstructionSpaceportSmallColonyPopulationThreshold * 1000000L;
                long num171 = (long)Policy.ConstructionOutpostColonyPopulationThreshold * 1000000L;
                foreach (Habitat item in habitatList2)
                {
                    if (!CheckSafeToBuildAtLocation(item))
                    {
                        continue;
                    }
                    Design design = null;
                    Design design2 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.SmallSpacePort);
                    Design design21 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.Outpost);
                    Design design3 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.MediumSpacePort);
                    Design design4 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.LargeSpacePort);
                    if (design4 != null && item.Population.TotalAmount > num15)
                    {
                        design = design4;
                    }
                    else if (design3 != null && item.Population.TotalAmount > num16)
                    {
                        design = design3;
                    }
                    else if (design2 != null && item.Population.TotalAmount > num17)
                    {
                        design = design2;
                    }
                    else if (design21 != null && item.Population.TotalAmount > num171)
                    {
                        design = design21;
                    }
                    if (design == null)
                    {
                        continue;
                    }
                    double num18 = design.CalculateCurrentPurchasePrice(_Galaxy);
                    double num19 = design.CalculateMaintenanceCosts(_Galaxy, this);
                    if (num13 + num19 > num7)
                    {
                        if (design.SubRole == BuiltObjectSubRole.LargeSpacePort)
                        {
                            design = _Designs.FindNewestCanBuild(BuiltObjectSubRole.MediumSpacePort);
                        }
                        else if (design.SubRole == BuiltObjectSubRole.MediumSpacePort)
                        {
                            design = _Designs.FindNewestCanBuild(BuiltObjectSubRole.SmallSpacePort);
                        }
                        else if (design.SubRole == BuiltObjectSubRole.SmallSpacePort)
                        {
                            design = _Designs.FindNewestCanBuild(BuiltObjectSubRole.Outpost);
                        }
                        if (design != null)
                        {
                            num18 = design.CalculateCurrentPurchasePrice(_Galaxy);
                            num19 = design.CalculateMaintenanceCosts(_Galaxy, this);
                        }
                    }
                    if (design == null || !(num13 + num19 <= num7) || !(num12 + num18 <= StateMoney))
                    {
                        continue;
                    }
                    design.BuildCount++;
                    BuiltObject builtObject3 = new BuiltObject(design, item.Name + " " + TextResolver.GetText("Space Port"), _Galaxy);
                    builtObject3.PurchasePrice = num18;
                    if (CheckTaskAuthorized(_ControlStateConstruction, ref refusalCount, GenerateAutomationMessageConstruction(builtObject3, item, num18), item, AdvisorMessageType.BuildOneOff, design, null))
                    {
                        if (item.ConstructionQueue != null && item.ConstructionQueue.AddBuiltObjectToConstruct(builtObject3))
                        {
                            builtObject3.ParentHabitat = item;
                            double range = (double)(item.Diameter / 6) + 15.0;
                            _Galaxy.SelectRelativePoint(range, out var x, out var y);
                            builtObject3.ParentOffsetX = x;
                            builtObject3.ParentOffsetY = y;
                            builtObject3.Heading = _Galaxy.SelectRandomHeading();
                            builtObject3.TargetHeading = builtObject3.Heading;
                            builtObject3.NearestSystemStar = Galaxy.DetermineHabitatSystemStar(item);
                            AddBuiltObjectToGalaxy(builtObject3, item, offsetLocationFromParent: false, isStateOwned: true);
                            builtObject3.BuiltAt = item;
                            num12 += num18;
                            num13 += num19;
                            ProcureConstructionComponents(builtObject3, item, out resourcesToOrder);
                            list2.Add(resourcesToOrder);
                            habitatList.Add(item);
                        }
                        else
                        {
                            design.BuildCount--;
                        }
                    }
                    else
                    {
                        design.BuildCount--;
                    }
                }
            }
            Design researchStationDesignToBuild = null;
            Habitat colonyToBuildAt = null;
            if (CheckBuildoutResearchCapacityAtColonies(out researchStationDesignToBuild, out colonyToBuildAt) && researchStationDesignToBuild != null && colonyToBuildAt != null)
            {
                double num20 = researchStationDesignToBuild.CalculateCurrentPurchasePrice(_Galaxy);
                double num21 = researchStationDesignToBuild.CalculateMaintenanceCosts(_Galaxy, this);
                if (num13 + num21 <= num7 && num12 + num20 <= StateMoney && CheckSafeToBuildAtLocation(colonyToBuildAt))
                {
                    researchStationDesignToBuild.BuildCount++;
                    string name = _Galaxy.SelectUniqueBuiltObjectName(researchStationDesignToBuild, colonyToBuildAt);
                    BuiltObject builtObject4 = new BuiltObject(researchStationDesignToBuild, name, _Galaxy);
                    builtObject4.PurchasePrice = num20;
                    if (CheckTaskAuthorized(_ControlStateConstruction, ref refusalCount, GenerateAutomationMessageConstruction(builtObject4, colonyToBuildAt, num20), colonyToBuildAt, AdvisorMessageType.BuildOneOff, researchStationDesignToBuild, null))
                    {
                        if (colonyToBuildAt.ConstructionQueue != null && colonyToBuildAt.ConstructionQueue.AddBuiltObjectToConstruct(builtObject4))
                        {
                            builtObject4.ParentHabitat = colonyToBuildAt;
                            DetermineOrbitalBaseLocation(colonyToBuildAt, out var offsetX, out var offsetY);
                            builtObject4.ParentOffsetX = offsetX;
                            builtObject4.ParentOffsetY = offsetY;
                            builtObject4.Heading = _Galaxy.SelectRandomHeading();
                            builtObject4.TargetHeading = builtObject4.Heading;
                            builtObject4.NearestSystemStar = Galaxy.DetermineHabitatSystemStar(colonyToBuildAt);
                            AddBuiltObjectToGalaxy(builtObject4, colonyToBuildAt, offsetLocationFromParent: false, isStateOwned: true, (int)offsetX, (int)offsetY);
                            builtObject4.BuiltAt = colonyToBuildAt;
                            num12 += num20;
                            num13 += num21;
                            ProcureConstructionComponents(builtObject4, colonyToBuildAt, out resourcesToOrder);
                            list2.Add(resourcesToOrder);
                            habitatList.Add(colonyToBuildAt);
                        }
                        else
                        {
                            researchStationDesignToBuild.BuildCount--;
                        }
                    }
                    else
                    {
                        researchStationDesignToBuild.BuildCount--;
                    }
                }
            }
            if (flag && _ControlColonization != 0)
            {
                HabitatList habitatList3 = DetermineHabitatsBeingColonized();
                _ColonizationTargets.Sort();
                _ColonizationTargets.Reverse();
                List<HabitatType> list3 = ColonizableHabitatTypesForEmpire(this);
                for (int k = 0; k < _ColonizationTargets.Count; k++)
                {
                    HabitatPrioritization habitatPrioritization = _ColonizationTargets[k];
                    if (!CheckShouldAttemptColonization(habitatPrioritization.Habitat) || habitatPrioritization.AssignedShip != null || habitatList3.Contains(habitatPrioritization.Habitat) || (habitatPrioritization.Habitat.Empire != null && habitatPrioritization.Habitat.Empire != _Galaxy.IndependentEmpire))
                    {
                        continue;
                    }
                    bool flag2 = false;
                    for (int l = 0; l < BuiltObjects.Count; l++)
                    {
                        BuiltObject builtObject5 = BuiltObjects[l];
                        if (builtObject5.Role == BuiltObjectRole.Colony && (builtObject5.Mission == null || builtObject5.Mission.Type == BuiltObjectMissionType.Undefined))
                        {
                            int newPopulationAmount = 0;
                            if (CanBuiltObjectColonizeHabitat(builtObject5, habitatPrioritization.Habitat, out newPopulationAmount) && habitatPrioritization.Priority >= Galaxy.HabitatColonizationThreshhold && builtObject5.WithinFuelRange(habitatPrioritization.Habitat.Xpos, habitatPrioritization.Habitat.Ypos, 0.0) && CheckTaskAuthorized(_ControlColonization, ref refusalCount, GenerateAutomationMessageColonization(habitatPrioritization.Habitat, builtObject5, null), habitatPrioritization.Habitat, AdvisorMessageType.Colonization, builtObject5, null))
                            {
                                habitatPrioritization.AssignedShip = builtObject5;
                                builtObject5.AssignMission(BuiltObjectMissionType.Colonize, habitatPrioritization.Habitat, null, BuiltObjectMissionPriority.Normal);
                                flag2 = true;
                                break;
                            }
                        }
                    }
                    if (flag2)
                    {
                        continue;
                    }
                    Design design5 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
                    if (design5 == null)
                    {
                        continue;
                    }
                    bool flag3 = CanDesignColonizeHabitat(design5, habitatPrioritization.Habitat);
                    if ((!flag3 && !list3.Contains(habitatPrioritization.Habitat.Type)) || habitatPrioritization.Priority < Galaxy.HabitatColonizationThreshhold)
                    {
                        continue;
                    }
                    double num22 = design5.CalculateCurrentPurchasePrice(_Galaxy);
                    if (!(num12 + num22 <= StateMoney))
                    {
                        continue;
                    }
                    design5.BuildCount++;
                    BuiltObject builtObject6 = new BuiltObject(design5, _Galaxy.GenerateBuiltObjectName(design5), _Galaxy);
                    builtObject6.PurchasePrice = num22;
                    Habitat habitat = null;
                    double shortestWaitQueueTime;
                    if (flag3)
                    {
                        habitat = Colonies.FindShortestConstructionWaitQueue(builtObject6, out shortestWaitQueueTime, allowLongWaitQueues: false, allowUnsafeLocations: false);
                    }
                    else
                    {
                        HabitatList habitatList4 = new HabitatList();
                        foreach (Habitat colony in Colonies)
                        {
                            Race dominantRace = colony.Population.DominantRace;
                            if (dominantRace != null && dominantRace.NativeHabitatType == habitatPrioritization.Habitat.Type)
                            {
                                habitatList4.Add(colony);
                            }
                        }
                        habitat = habitatList4.FindShortestConstructionWaitQueue(builtObject6, out shortestWaitQueueTime, allowLongWaitQueues: false, allowUnsafeLocations: false);
                    }
                    double num23 = shortestWaitQueueTime / (double)Galaxy.RealSecondsInGalacticYear;
                    if (habitat != null && num23 < Galaxy.MaximumConstructionQueueWaitTimeYears)
                    {
                        double num24 = _Galaxy.CalculateDistance(habitatPrioritization.Habitat.Xpos, habitatPrioritization.Habitat.Ypos, habitat.Xpos, habitat.Ypos);
                        if (num24 <= design5.MaximumRange())
                        {
                            if (CheckTaskAuthorized(_ControlColonization, ref refusalCount, GenerateAutomationMessageColonization(habitatPrioritization.Habitat, null, habitat), habitatPrioritization.Habitat, AdvisorMessageType.Colonization, habitat, null))
                            {
                                if (habitat.ConstructionQueue != null && habitat.ConstructionQueue.AddBuiltObjectToConstruct(builtObject6))
                                {
                                    builtObject6.Name = _Galaxy.GenerateBuiltObjectName(design5, habitat);
                                    habitatPrioritization.AssignedShip = builtObject6;
                                    AddBuiltObjectToGalaxy(builtObject6, habitat, offsetLocationFromParent: false, isStateOwned: true);
                                    num12 += num22;
                                    builtObject6.AssignMission(BuiltObjectMissionType.Colonize, habitatPrioritization.Habitat, null, BuiltObjectMissionPriority.Normal);
                                    builtObject6.BuiltAt = habitat;
                                    ProcureConstructionComponents(builtObject6, habitat, out resourcesToOrder);
                                    list2.Add(resourcesToOrder);
                                    habitatList.Add(habitat);
                                }
                                else
                                {
                                    design5.BuildCount--;
                                }
                            }
                            else
                            {
                                design5.BuildCount--;
                            }
                        }
                        else
                        {
                            design5.BuildCount--;
                        }
                    }
                    else
                    {
                        design5.BuildCount--;
                    }
                }
            }
            if (_ControlStateConstruction != 0)
            {
                ForceStructureProjectionList forceStructureProjectionList3 = RefactorForceStructureProjectionsToCosts(forceStructureProjectionList2, num7, num13, num12, includeCashflowCheck: true);
                double num25 = 0.0;
                foreach (ForceStructureProjection item2 in forceStructureProjectionList3)
                {
                    Design design6 = Designs.FindNewestCanBuild(item2.SubRole, this, null, includePlanetDestroyers: false);
                    if (design6 != null && ((ConstructionYards != null && ConstructionYards.Count > 0) || design6.SubRole == BuiltObjectSubRole.ColonyShip || design6.SubRole == BuiltObjectSubRole.ConstructionShip || design6.SubRole == BuiltObjectSubRole.ResupplyShip))
                    {
                        double num26 = design6.CalculateCurrentPurchasePrice(_Galaxy);
                        num25 += num26 * (double)item2.Amount;
                    }
                }
                if (forceStructureProjectionList3.Count > 0 && forceStructureProjectionList3.TotalAmount > 0)
                {
                    if (_ControlStateConstruction == AutomationLevel.SemiAutomated)
                    {
                        EmpireMessage empireMessage = new EmpireMessage(this, EmpireMessageType.AdvisorSuggestion, null);
                        empireMessage.AdvisorMessageType = AdvisorMessageType.BuildOrder;
                        empireMessage.Description = string.Format(TextResolver.GetText("Build new ships for X credits"), num25.ToString("###,###,###,##0"));
                        empireMessage.StarDate = _Galaxy.CurrentStarDate;
                        SendMessageToEmpire(empireMessage, this);
                    }
                    else if (_ControlStateConstruction == AutomationLevel.FullyAutomated)
                    {
                        foreach (ForceStructureProjection item3 in forceStructureProjectionList3)
                        {
                            Design design7 = Designs.FindNewestCanBuild(item3.SubRole, this, null, includePlanetDestroyers: false);
                            if (design7 == null || item3.Amount <= 0)
                            {
                                continue;
                            }
                            for (int m = 0; m < item3.Amount; m++)
                            {
                                double num27 = design7.CalculateCurrentPurchasePrice(_Galaxy);
                                double num28 = design7.CalculateMaintenanceCosts(_Galaxy, this);
                                if (!(num13 + num28 <= num7) || !(num12 + num27 <= StateMoney))
                                {
                                    continue;
                                }
                                design7.BuildCount++;
                                BuiltObject builtObject7 = new BuiltObject(design7, _Galaxy.GenerateBuiltObjectName(design7), _Galaxy);
                                builtObject7.PurchasePrice = num27;
                                double shortestWaitQueueTime2;
                                if (builtObject7.SubRole == BuiltObjectSubRole.ConstructionShip || builtObject7.SubRole == BuiltObjectSubRole.ResupplyShip)
                                {
                                    Habitat habitat2 = Colonies.FindShortestConstructionWaitQueue(builtObject7, out shortestWaitQueueTime2);
                                    double num29 = shortestWaitQueueTime2 / (double)Galaxy.RealSecondsInGalacticYear;
                                    if (habitat2 != null && num29 < Galaxy.MaximumConstructionQueueWaitTimeYears)
                                    {
                                        if (habitat2.ConstructionQueue != null && habitat2.ConstructionQueue.AddBuiltObjectToConstruct(builtObject7))
                                        {
                                            num12 += num27;
                                            num13 += num28;
                                            builtObject7.Name = _Galaxy.GenerateBuiltObjectName(design7, habitat2);
                                            AddBuiltObjectToGalaxy(builtObject7, habitat2, offsetLocationFromParent: false, isStateOwned: true);
                                            builtObject7.BuiltAt = habitat2;
                                            builtObject7.IsAutoControlled = NewBuiltObjectShouldBeAutomated(builtObject7.SubRole);
                                            ProcureConstructionComponents(builtObject7, habitat2, out resourcesToOrder);
                                            list2.Add(resourcesToOrder);
                                            habitatList.Add(habitat2);
                                        }
                                        else
                                        {
                                            design7.BuildCount--;
                                        }
                                    }
                                    else
                                    {
                                        design7.BuildCount--;
                                    }
                                    continue;
                                }
                                BuiltObject builtObject8 = SpacePorts.FindShortestConstructionWaitQueue(builtObject7, out shortestWaitQueueTime2);
                                double num30 = shortestWaitQueueTime2 / (double)Galaxy.RealSecondsInGalacticYear;
                                if (builtObject8 != null && num30 < Galaxy.MaximumConstructionQueueWaitTimeYears)
                                {
                                    if (builtObject8.ConstructionQueue != null && builtObject8.ConstructionQueue.AddBuiltObjectToConstruct(builtObject7))
                                    {
                                        num12 += num27;
                                        num13 += num28;
                                        if (builtObject8.ParentHabitat != null)
                                        {
                                            builtObject7.Name = _Galaxy.GenerateBuiltObjectName(design7, builtObject8.ParentHabitat);
                                        }
                                        AddBuiltObjectToGalaxy(builtObject7, builtObject8, offsetLocationFromParent: false, isStateOwned: true);
                                        builtObject7.BuiltAt = builtObject8;
                                        builtObject7.IsAutoControlled = NewBuiltObjectShouldBeAutomated(builtObject7.SubRole);
                                        ProcureConstructionComponents(builtObject7, builtObject8, orderPreciseResourceAmounts: true, out resourcesToOrder);
                                        list.Add(resourcesToOrder);
                                        builtObjectList.Add(builtObject8);
                                    }
                                    else
                                    {
                                        design7.BuildCount--;
                                    }
                                }
                                else
                                {
                                    design7.BuildCount--;
                                }
                            }
                        }
                    }
                }
            }
            StateMoney -= num12;
            PirateEconomy.PerformExpense(num12, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
            BuiltObjectList builtObjectList3 = new BuiltObjectList();
            foreach (BuiltObject item4 in builtObjectList)
            {
                if (!builtObjectList3.Contains(item4))
                {
                    builtObjectList3.Add(item4);
                }
            }
            foreach (BuiltObject item5 in builtObjectList3)
            {
                CargoList cargoList = new CargoList();
                for (int n = 0; n < builtObjectList.Count; n++)
                {
                    if (builtObjectList[n] != item5)
                    {
                        continue;
                    }
                    foreach (Cargo item6 in list[n])
                    {
                        cargoList.Add(item6);
                    }
                }
                foreach (Cargo item7 in cargoList)
                {
                    CreateOrder(item5, item7.CommodityResource, item7.Amount, isState: false, OrderType.ConstructionShortage);
                }
            }
            HabitatList habitatList5 = new HabitatList();
            foreach (Habitat item8 in habitatList)
            {
                if (!habitatList5.Contains(item8))
                {
                    habitatList5.Add(item8);
                }
            }
            foreach (Habitat item9 in habitatList5)
            {
                CargoList cargoList2 = new CargoList();
                for (int num31 = 0; num31 < habitatList.Count; num31++)
                {
                    if (habitatList[num31] != item9)
                    {
                        continue;
                    }
                    foreach (Cargo item10 in list2[num31])
                    {
                        cargoList2.Add(item10);
                    }
                }
                foreach (Cargo item11 in cargoList2)
                {
                    CreateOrder(item9, item11.CommodityResource, item11.Amount, isState: false, OrderType.ConstructionShortage);
                }
            }
        }

        public void BuildNewShipsPirate(DesignList designs, List<int> amounts)
        {
            if (designs == null || designs.Count <= 0 || amounts == null || amounts.Count <= 0 || designs.Count != amounts.Count)
            {
                return;
            }
            if (DominantRace != null)
            {
                _ = DominantRace.Expanding;
            }
            HabitatList habitatList = new HabitatList();
            HabitatList habitatList2 = new HabitatList();
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat != null && !habitat.HasBeenDestroyed && habitat.Empire == this)
                {
                    habitatList.Add(habitat);
                    if (habitat.Population != null && habitat.Population.TotalAmount > Galaxy.BuildColonyShipPopulationRequirement && habitat.ConstructionQueue != null && habitat.ConstructionQueue.ConstructionWaitQueue != null && habitat.ConstructionQueue.ConstructionWaitQueue.Count <= 0)
                    {
                        habitatList2.Add(habitat);
                    }
                }
            }
            _ = habitatList2.Count;
            _ = 0;
            double purchaseCost = 0.0;
            List<CargoList> builtObjectResourcesToOrder = new List<CargoList>();
            BuiltObjectList builtObjectConstructionYards = new BuiltObjectList();
            List<CargoList> colonyResourcesToOrder = new List<CargoList>();
            HabitatList colonyConstructionYards = new HabitatList();
            CargoList resourcesToOrder = null;
            for (int j = 0; j < designs.Count; j++)
            {
                Design design = designs[j];
                if (design == null)
                {
                    continue;
                }
                int num = amounts[j];
                if (num > 0)
                {
                    switch (design.SubRole)
                    {
                        case BuiltObjectSubRole.ResupplyShip:
                        case BuiltObjectSubRole.ColonyShip:
                        case BuiltObjectSubRole.ConstructionShip:
                            PirateBuildNewShipsAtColony(design, num, StateMoney, ref purchaseCost, habitatList, ref resourcesToOrder, ref colonyResourcesToOrder, ref colonyConstructionYards);
                            break;
                        default:
                            PirateBuildNewShips(design, num, StateMoney, ref purchaseCost, ref resourcesToOrder, ref builtObjectResourcesToOrder, ref builtObjectConstructionYards);
                            break;
                    }
                }
            }
            StateMoney -= purchaseCost;
            PirateEconomy.PerformExpense(purchaseCost, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
            BuiltObjectList builtObjectList = new BuiltObjectList();
            foreach (BuiltObject item in builtObjectConstructionYards)
            {
                if (!builtObjectList.Contains(item))
                {
                    builtObjectList.Add(item);
                }
            }
            foreach (BuiltObject item2 in builtObjectList)
            {
                CargoList cargoList = new CargoList();
                for (int k = 0; k < builtObjectConstructionYards.Count; k++)
                {
                    if (builtObjectConstructionYards[k] != item2)
                    {
                        continue;
                    }
                    foreach (Cargo item3 in builtObjectResourcesToOrder[k])
                    {
                        cargoList.Add(item3);
                    }
                }
                foreach (Cargo item4 in cargoList)
                {
                    CreateOrder(item2, item4.CommodityResource, item4.Amount, isState: false, OrderType.ConstructionShortage);
                }
            }
            HabitatList habitatList3 = new HabitatList();
            foreach (Habitat item5 in colonyConstructionYards)
            {
                if (!habitatList3.Contains(item5))
                {
                    habitatList3.Add(item5);
                }
            }
            foreach (Habitat item6 in habitatList3)
            {
                CargoList cargoList2 = new CargoList();
                for (int l = 0; l < colonyConstructionYards.Count; l++)
                {
                    if (colonyConstructionYards[l] != item6)
                    {
                        continue;
                    }
                    foreach (Cargo item7 in colonyResourcesToOrder[l])
                    {
                        cargoList2.Add(item7);
                    }
                }
                foreach (Cargo item8 in cargoList2)
                {
                    CreateOrder(item6, item8.CommodityResource, item8.Amount, isState: false, OrderType.ConstructionShortage);
                }
            }
        }

        public void BuildNewShips(DesignList designs, List<int> amounts)
        {
            if (designs == null || designs.Count <= 0 || amounts == null || amounts.Count <= 0 || designs.Count != amounts.Count)
            {
                return;
            }
            double num = 0.0;
            List<CargoList> list = new List<CargoList>();
            BuiltObjectList builtObjectList = new BuiltObjectList();
            List<CargoList> list2 = new List<CargoList>();
            HabitatList habitatList = new HabitatList();
            CargoList resourcesToOrder = null;
            for (int i = 0; i < designs.Count; i++)
            {
                Design design = designs[i];
                if (design == null)
                {
                    continue;
                }
                int num2 = amounts[i];
                if (num2 <= 0)
                {
                    continue;
                }
                for (int j = 0; j < num2; j++)
                {
                    double num3 = design.CalculateCurrentPurchasePrice(_Galaxy);
                    if (!(num + num3 <= StateMoney))
                    {
                        continue;
                    }
                    design.BuildCount++;
                    BuiltObject builtObject = new BuiltObject(design, _Galaxy.GenerateBuiltObjectName(design), _Galaxy);
                    builtObject.PurchasePrice = num3;
                    double shortestWaitQueueTime;
                    if (builtObject.SubRole == BuiltObjectSubRole.ConstructionShip || builtObject.SubRole == BuiltObjectSubRole.ResupplyShip)
                    {
                        Habitat habitat = Colonies.FindShortestConstructionWaitQueue(builtObject, out shortestWaitQueueTime, allowLongWaitQueues: true);
                        if (habitat != null)
                        {
                            if (habitat.ConstructionQueue != null && habitat.ConstructionQueue.AddBuiltObjectToConstruct(builtObject))
                            {
                                num += num3;
                                builtObject.Name = _Galaxy.GenerateBuiltObjectName(design, habitat);
                                AddBuiltObjectToGalaxy(builtObject, habitat, offsetLocationFromParent: false, isStateOwned: true);
                                builtObject.BuiltAt = habitat;
                                builtObject.IsAutoControlled = NewBuiltObjectShouldBeAutomated(builtObject.SubRole);
                                ProcureConstructionComponents(builtObject, habitat, out resourcesToOrder);
                                list2.Add(resourcesToOrder);
                                habitatList.Add(habitat);
                            }
                            else
                            {
                                design.BuildCount--;
                            }
                        }
                        else
                        {
                            design.BuildCount--;
                        }
                        continue;
                    }
                    BuiltObject builtObject2 = SpacePorts.FindShortestConstructionWaitQueue(builtObject, out shortestWaitQueueTime, includeVerySmallYards: false);
                    if (builtObject2 != null)
                    {
                        if (builtObject2.ConstructionQueue != null && builtObject2.ConstructionQueue.AddBuiltObjectToConstruct(builtObject))
                        {
                            num += num3;
                            if (builtObject2.ParentHabitat != null)
                            {
                                builtObject.Name = _Galaxy.GenerateBuiltObjectName(design, builtObject2.ParentHabitat);
                            }
                            AddBuiltObjectToGalaxy(builtObject, builtObject2, offsetLocationFromParent: false, isStateOwned: true);
                            builtObject.BuiltAt = builtObject2;
                            builtObject.IsAutoControlled = NewBuiltObjectShouldBeAutomated(builtObject.SubRole);
                            ProcureConstructionComponents(builtObject, builtObject2, orderPreciseResourceAmounts: true, out resourcesToOrder);
                            list.Add(resourcesToOrder);
                            builtObjectList.Add(builtObject2);
                        }
                        else
                        {
                            design.BuildCount--;
                        }
                    }
                    else
                    {
                        design.BuildCount--;
                    }
                }
            }
            StateMoney -= num;
            PirateEconomy.PerformExpense(num, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
            BuiltObjectList builtObjectList2 = new BuiltObjectList();
            foreach (BuiltObject item in builtObjectList)
            {
                if (!builtObjectList2.Contains(item))
                {
                    builtObjectList2.Add(item);
                }
            }
            foreach (BuiltObject item2 in builtObjectList2)
            {
                CargoList cargoList = new CargoList();
                for (int k = 0; k < builtObjectList.Count; k++)
                {
                    if (builtObjectList[k] != item2)
                    {
                        continue;
                    }
                    foreach (Cargo item3 in list[k])
                    {
                        cargoList.Add(item3);
                    }
                }
                foreach (Cargo item4 in cargoList)
                {
                    CreateOrder(item2, item4.CommodityResource, item4.Amount, isState: false, OrderType.ConstructionShortage);
                }
            }
            HabitatList habitatList2 = new HabitatList();
            foreach (Habitat item5 in habitatList)
            {
                if (!habitatList2.Contains(item5))
                {
                    habitatList2.Add(item5);
                }
            }
            foreach (Habitat item6 in habitatList2)
            {
                CargoList cargoList2 = new CargoList();
                for (int l = 0; l < habitatList.Count; l++)
                {
                    if (habitatList[l] != item6)
                    {
                        continue;
                    }
                    foreach (Cargo item7 in list2[l])
                    {
                        cargoList2.Add(item7);
                    }
                }
                foreach (Cargo item8 in cargoList2)
                {
                    CreateOrder(item6, item8.CommodityResource, item8.Amount, isState: false, OrderType.ConstructionShortage);
                }
            }
        }

        public int CheckSystemEnemyShipLevel(SystemVisibility system)
        {
            int num = 0;
            if (system != null && system.Threats != null)
            {
                for (int i = 0; i < system.Threats.Count; i++)
                {
                    StellarObject stellarObject = system.Threats[i];
                    if (!(stellarObject is BuiltObject))
                    {
                        continue;
                    }
                    BuiltObject builtObject = (BuiltObject)stellarObject;
                    if (builtObject.Role == BuiltObjectRole.Base || builtObject.TopSpeed <= 0 || !builtObject.IsFunctional || stellarObject.Empire == null || stellarObject.Empire == this)
                    {
                        continue;
                    }
                    if (stellarObject.Empire.PirateEmpireBaseHabitat != null)
                    {
                        PirateRelation pirateRelation = ObtainPirateRelation(stellarObject.Empire);
                        if (pirateRelation != null && pirateRelation.Type != PirateRelationType.Protection)
                        {
                            num += stellarObject.FirepowerRaw;
                        }
                        continue;
                    }
                    DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(stellarObject.Empire);
                    if (diplomaticRelation != null)
                    {
                        DiplomaticRelationType type = diplomaticRelation.Type;
                        if (type == DiplomaticRelationType.War)
                        {
                            num += stellarObject.FirepowerRaw;
                        }
                    }
                }
            }
            return num;
        }

        public HabitatList DetermineNewSpacePortLocations(HabitatList colonies, int newSpacePortAmount, bool excludeColoniesWithEnemiesPresent)
        {
            HabitatList habitatList = new HabitatList();
            if (newSpacePortAmount > 0)
            {
                HabitatList habitatList2 = new HabitatList();
                habitatList2.AddRange(colonies);
                habitatList2.Sort();
                habitatList2.Reverse();
                HabitatList habitatList3 = new HabitatList();
                for (int i = 0; i < SpacePorts.Count; i++)
                {
                    BuiltObject builtObject = SpacePorts[i];
                    if (builtObject.ParentHabitat != null)
                    {
                        Habitat habitat = Galaxy.DetermineHabitatSystemStar(builtObject.ParentHabitat);
                        if (habitat != null && !habitatList3.Contains(habitat))
                        {
                            habitatList3.Add(habitat);
                        }
                    }
                }
                for (int j = 0; j < colonies.Count; j++)
                {
                    Habitat habitat2 = colonies[j];
                    foreach (BuiltObject item2 in habitat2.ConstructionQueue.ConstructionWaitQueue)
                    {
                        if (item2.SubRole == BuiltObjectSubRole.Outpost || item2.SubRole == BuiltObjectSubRole.MediumSpacePort || item2.SubRole == BuiltObjectSubRole.LargeSpacePort)
                        {
                            Habitat habitat3 = Galaxy.DetermineHabitatSystemStar(habitat2);
                            if (habitat3 != null && !habitatList3.Contains(habitat3))
                            {
                                habitatList3.Add(habitat3);
                            }
                        }
                    }
                }
                for (int k = 0; k < BuiltObjects.Count; k++)
                {
                    BuiltObject builtObject2 = BuiltObjects[k];
                    if (builtObject2.SubRole == BuiltObjectSubRole.Outpost || builtObject2.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject2.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject2.SubRole == BuiltObjectSubRole.LargeSpacePort)
                    {
                        Habitat habitat4 = Galaxy.DetermineHabitatSystemStar(builtObject2.ParentHabitat);
                        if (habitat4 != null && !habitatList3.Contains(habitat4))
                        {
                            habitatList3.Add(habitat4);
                        }
                    }
                }
                HabitatList habitatList4 = new HabitatList();
                foreach (Habitat item3 in habitatList2)
                {
                    Habitat item = Galaxy.DetermineHabitatSystemStar(item3);
                    if (habitatList3.Contains(item))
                    {
                        habitatList4.Add(item3);
                    }
                }
                foreach (Habitat item4 in habitatList4)
                {
                    habitatList2.Remove(item4);
                }
                int num = 0;
                double num2 = (double)Policy.ConstructionSpaceportMinimumDistance * 1000.0;
                {
                    foreach (Habitat item5 in habitatList2)
                    {
                        BuiltObject builtObject3 = _Galaxy.FastFindNearestSpacePort(item5.Xpos, item5.Ypos, this, true);
                        if (builtObject3 != null)
                        {
                            double num3 = _Galaxy.CalculateDistance(item5.Xpos, item5.Ypos, builtObject3.Xpos, builtObject3.Ypos);
                            if (!(num3 > num2))
                            {
                                continue;
                            }
                            bool flag = false;
                            foreach (Habitat item6 in habitatList)
                            {
                                num3 = _Galaxy.CalculateDistance(item5.Xpos, item5.Ypos, item6.Xpos, item6.Ypos);
                                if (num3 <= num2)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                bool flag2 = true;
                                if (excludeColoniesWithEnemiesPresent)
                                {
                                    flag2 = false;
                                    int num4 = CheckSystemEnemyShipLevel(SystemVisibility[item5.SystemIndex]);
                                    if (num4 <= 0 && (item5.InvadingTroops == null || item5.InvadingTroops.Count <= 0))
                                    {
                                        flag2 = true;
                                    }
                                }
                                if (flag2)
                                {
                                    habitatList.Add(item5);
                                    num++;
                                }
                            }
                            if (num >= newSpacePortAmount)
                            {
                                return habitatList;
                            }
                            continue;
                        }
                        bool flag3 = true;
                        if (excludeColoniesWithEnemiesPresent)
                        {
                            flag3 = false;
                            int num5 = CheckSystemEnemyShipLevel(SystemVisibility[item5.SystemIndex]);
                            if (num5 <= 0 && (item5.InvadingTroops == null || item5.InvadingTroops.Count <= 0))
                            {
                                flag3 = true;
                            }
                        }
                        if (flag3)
                        {
                            habitatList.Add(item5);
                            num++;
                        }
                        if (num >= newSpacePortAmount)
                        {
                            return habitatList;
                        }
                    }
                    return habitatList;
                }
            }
            return habitatList;
        }

        private void RetrofitBuiltObjects()
        {
            long stateRetrofitAge = Galaxy.RealSecondsInGalacticYear * 1000 * Galaxy.RetrofitYears;
            long privateRetrofitAge = Galaxy.RealSecondsInGalacticYear * 2000;
            RetrofitBuiltObjects(stateRetrofitAge, privateRetrofitAge, breakthroughInitiated: false);
        }

        private void RetrofitBuiltObjects(long stateRetrofitAge, long privateRetrofitAge, bool breakthroughInitiated)
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(PrivateBuiltObjects);
            BuiltObjectList builtObjectList2 = new BuiltObjectList();
            bool flag = true;
            if (this == _Galaxy.PlayerEmpire)
            {
                flag = false;
            }
            if (breakthroughInitiated && Policy.ResearchDesignAutoRetrofit)
            {
                flag = false;
                BuiltObjectList builtObjectList3 = BuiltObjects;
                double num = StateMoney * 0.7;
                if (Research != null && Research.RecentProjects != null && Research.RecentProjects.ContainsBySpecialFunctionCode(2))
                {
                    num = StateMoney;
                    builtObjectList3 = BuiltObjects.GetBuiltObjectsBySubRole(new List<BuiltObjectSubRole>
                {
                    BuiltObjectSubRole.ConstructionShip,
                    BuiltObjectSubRole.ExplorationShip,
                    BuiltObjectSubRole.Escort,
                    BuiltObjectSubRole.Frigate,
                    BuiltObjectSubRole.Destroyer
                });
                }
                else if (CheckEmpireHasHyperDriveTech(this))
                {
                    builtObjectList3 = BuiltObjects.GetShipsWithoutWarpDrives();
                    if (builtObjectList3.Count > 0)
                    {
                        num = StateMoney;
                    }
                    else
                    {
                        builtObjectList3 = BuiltObjects;
                    }
                }
                double num2 = CalculateRetrofitCosts(builtObjectList3);
                if (num2 > 0.0 && num > 0.0 && num2 > num)
                {
                    double num3 = num2 / num;
                    if (num3 < 10.0)
                    {
                        double num4 = 0.0;
                        int num5 = 1 + Math.Max(1, (int)(num3 * 8.0 + 0.999));
                        int num6 = Math.Max(1, builtObjectList3.Count / num5);
                        int i = 0;
                        for (int iterationCount = 0; Galaxy.ConditionCheckLimit(i < builtObjectList3.Count, 200, ref iterationCount); i += num6)
                        {
                            int count = Math.Min(num6, builtObjectList3.Count - i);
                            BuiltObjectList range = builtObjectList3.GetRange(i, count);
                            double num7 = CalculateRetrofitCosts(range);
                            if (!(num4 + num7 < num))
                            {
                                break;
                            }
                            num4 += num7;
                            builtObjectList2.AddRange(range);
                        }
                        num2 = num4;
                    }
                }
                else
                {
                    builtObjectList2.AddRange(builtObjectList3);
                }
                if (this == _Galaxy.PlayerEmpire)
                {
                    if (StateMoney >= num2)
                    {
                        double num8 = Math.Max(0.0, num2);
                        ComponentList componentList = new ComponentList();
                        if (Research != null && Research.RecentProjects != null && Research.RecentProjects.Count > 0)
                        {
                            for (int j = 0; j < Research.RecentProjects.Count; j++)
                            {
                                ResearchNode researchNode = Research.RecentProjects[j];
                                if (researchNode.Components != null && researchNode.Components.Count > 0)
                                {
                                    for (int k = 0; k < researchNode.Components.Count; k++)
                                    {
                                        componentList.Add(researchNode.Components[k]);
                                    }
                                }
                            }
                        }
                        string empty = string.Empty;
                        if (componentList.Count > 0)
                        {
                            string text = string.Empty;
                            for (int l = 0; l < componentList.Count; l++)
                            {
                                text = text + "    " + componentList[l].Name + "\n";
                            }
                            empty = string.Format(TextResolver.GetText("Retrofit Recommendation Message Components"), text);
                        }
                        else
                        {
                            empty = TextResolver.GetText("Retrofit Recommendation Message");
                        }
                        if (CheckTaskAuthorized(taskDescription: (builtObjectList2.Count == BuiltObjects.Count) ? (empty + string.Format(TextResolver.GetText("Retrofit Recommendation Explanation"), num8.ToString("###,###,###,##0"))) : (empty + string.Format(TextResolver.GetText("Retrofit Recommendation Explanation Partial"), num8.ToString("###,###,###,##0"))), automationLevel: _ControlStateConstruction, taskTarget: builtObjectList2, advisorMessageType: AdvisorMessageType.Retrofit))
                        {
                            flag = true;
                            if (_ControlStateConstruction != AutomationLevel.FullyAutomated)
                            {
                                stateRetrofitAge = 0L;
                            }
                        }
                        if (Research != null && Research.RecentProjects != null)
                        {
                            Research.RecentProjects.Clear();
                        }
                    }
                }
                else
                {
                    if (num2 < StateMoney * 0.7)
                    {
                        flag = true;
                        stateRetrofitAge = Math.Max(stateRetrofitAge, Galaxy.RealSecondsInGalacticYear * 2000);
                    }
                    if (Research != null && Research.RecentProjects != null)
                    {
                        Research.RecentProjects.Clear();
                    }
                }
            }
            if (flag)
            {
                builtObjectList.AddRange(builtObjectList2);
            }
            DoRetrofit(builtObjectList, currentStarDate, privateRetrofitAge, stateRetrofitAge, flag, breakthroughInitiated, manuallyInitiated: false);
        }

        public void DoRetrofit(BuiltObjectList builtObjects, long starDate, long privateRetrofitAge, long stateRetrofitAge, bool stateRetrofitPermitted, bool breakthroughInitiated, bool manuallyInitiated)
        {
            bool flag = CheckAtWar();
            long num = (long)Policy.ConstructionSpaceportLargeColonyPopulationThreshold * 1000000L;
            long num2 = (long)Policy.ConstructionSpaceportMediumColonyPopulationThreshold * 1000000L;
            long num3 = (long)Policy.ConstructionSpaceportSmallColonyPopulationThreshold * 1000000L;
            long num34 = (long)Policy.ConstructionOutpostColonyPopulationThreshold * 1000000L;
            if (!flag && stateRetrofitPermitted)
            {
                for (int i = 0; i < ShipGroups.Count; i++)
                {
                    ShipGroup shipGroup = ShipGroups[i];
                    if (shipGroup == null || shipGroup.LeadShip == null || (!breakthroughInitiated && !shipGroup.LeadShip.IsAutoControlled) || starDate - shipGroup.LeadShip.DateRetrofit <= stateRetrofitAge || !CheckFleetNeedsRetrofit(shipGroup, !manuallyInitiated))
                    {
                        continue;
                    }
                    BuiltObjectMission mission = shipGroup.Mission;
                    if (mission == null || mission.Type == BuiltObjectMissionType.Undefined || mission.Priority == BuiltObjectMissionPriority.Low)
                    {
                        if (mission == null || mission.Type != BuiltObjectMissionType.Retrofit)
                        {
                            AssignFleetRetrofit(shipGroup, !manuallyInitiated);
                        }
                    }
                    else if ((mission == null || mission.Type != BuiltObjectMissionType.Retrofit) && !shipGroup.SubsequentMissions.ContainsType(BuiltObjectMissionType.Retrofit))
                    {
                        shipGroup.QueueMission(BuiltObjectMissionType.Retrofit, null, null, BuiltObjectMissionPriority.Normal);
                    }
                }
            }
            for (int j = 0; j < builtObjects.Count; j++)
            {
                BuiltObject builtObject = builtObjects[j];
                if ((!builtObject.IsAutoControlled && !breakthroughInitiated) || builtObject.RetrofitForNextMission || builtObject.RetrofitDesign != null)
                {
                    continue;
                }
                long num4 = stateRetrofitAge;
                if (builtObject.Owner == null)
                {
                    num4 = privateRetrofitAge;
                }
                switch (builtObject.SubRole)
                {
                    case BuiltObjectSubRole.GasMiningStation:
                    case BuiltObjectSubRole.MiningStation:
                    case BuiltObjectSubRole.ResortBase:
                    case BuiltObjectSubRole.GenericBase:
                    case BuiltObjectSubRole.EnergyResearchStation:
                    case BuiltObjectSubRole.WeaponsResearchStation:
                    case BuiltObjectSubRole.HighTechResearchStation:
                    case BuiltObjectSubRole.MonitoringStation:
                        num4 = privateRetrofitAge * 2;
                        break;
                }
                if (starDate - builtObject.DateRetrofit <= num4 || builtObject.BuiltAt != null || builtObject.ShipGroup != null || (builtObject.Role == BuiltObjectRole.Military && flag))
                {
                    continue;
                }
                double num5 = Galaxy.ResolveBuildSpeed(this, _Galaxy, builtObject);
                bool flag2 = true;
                if (num5 > 1.0)
                {
                    Design design = _Designs.FindNewestCanBuild(builtObject.SubRole);
                    if (design == null || design.WarpSpeed <= 0 || builtObject.Design == null || builtObject.Design.WarpSpeed > 0)
                    {
                        flag2 = false;
                    }
                }
                if (!flag2 || builtObject.SuppressAutoRetrofit)
                {
                    continue;
                }
                if (builtObject.ParentHabitat != null)
                {
                    if (!CanBuildBuiltObject(builtObject, builtObject.ParentHabitat))
                    {
                        continue;
                    }
                    builtObject.RetrofitForNextMission = true;
                    if (builtObject.Role == BuiltObjectRole.Base)
                    {
                        if (builtObject.SubRole == BuiltObjectSubRole.Outpost || builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort)
                        {
                            if (PirateEmpireBaseHabitat != null)
                            {
                                Design design2 = _Designs.FindNewestCanBuild(builtObject.SubRole);
                                BuiltObject builtObject2 = _Galaxy.IdentifyPirateSpaceport(this);
                                if (builtObject == builtObject2)
                                {
                                    if (builtObject.SubRole == BuiltObjectSubRole.Outpost)
                                    {
                                        Design design3 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.SmallSpacePort);
                                        if (design3 != null && CanBuildDesign(design3))
                                        {
                                            design2 = design3;
                                        }
                                    }
                                    else if (builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort)
                                    {
                                        Design design3 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.MediumSpacePort);
                                        if (design3 != null && CanBuildDesign(design3))
                                        {
                                            design2 = design3;
                                        }
                                    }
                                    else if (builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort)
                                    {
                                        Design design4 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.LargeSpacePort);
                                        if (design4 != null && CanBuildDesign(design4))
                                        {
                                            design2 = design4;
                                        }
                                    }
                                    if (design2 != null && design2 != builtObject.Design && AssignRetrofitMission(builtObject, design2, null, forceUseOfYard: true))
                                    {
                                        builtObject.DateRetrofit = _Galaxy.CurrentStarDate;
                                    }
                                    builtObject.RetrofitForNextMission = false;
                                }
                                else
                                {
                                    if (design2 != null && design2 != builtObject.Design && AssignRetrofitMission(builtObject, design2, null, forceUseOfYard: true))
                                    {
                                        builtObject.DateRetrofit = _Galaxy.CurrentStarDate;
                                    }
                                    builtObject.RetrofitForNextMission = false;
                                }
                                continue;
                            }
                            Design design5 = _Designs.FindNewestCanBuild(builtObject.SubRole);
                            if (builtObject.ParentHabitat.Population.TotalAmount > num)
                            {
                                design5 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.LargeSpacePort);
                            }
                            else if (builtObject.ParentHabitat.Population.TotalAmount > num2)
                            {
                                if (builtObject.SubRole != BuiltObjectSubRole.LargeSpacePort)
                                {
                                    design5 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.MediumSpacePort);
                                }
                            }
                            else if (builtObject.ParentHabitat.Population.TotalAmount > num3 && builtObject.SubRole != BuiltObjectSubRole.LargeSpacePort && builtObject.SubRole != BuiltObjectSubRole.MediumSpacePort)
                            {
                                design5 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.SmallSpacePort);
                            }
                            else if (builtObject.ParentHabitat.Population.TotalAmount > num34 && builtObject.SubRole != BuiltObjectSubRole.LargeSpacePort && builtObject.SubRole != BuiltObjectSubRole.MediumSpacePort && builtObject.SubRole != BuiltObjectSubRole.SmallSpacePort)
                            {
                                design5 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.Outpost);
                            }
                            if (design5 != null && design5 != builtObject.Design && AssignRetrofitMission(builtObject, design5, null, forceUseOfYard: true))
                            {
                                builtObject.DateRetrofit = _Galaxy.CurrentStarDate;
                            }
                            builtObject.RetrofitForNextMission = false;
                        }
                        else
                        {
                            Design design6 = _Designs.FindNewestCanBuild(builtObject.SubRole, builtObject.ActualEmpire, builtObject.ParentHabitat);
                            if (design6 != null && design6 != builtObject.Design && AssignRetrofitMission(builtObject, design6, null, forceUseOfYard: true))
                            {
                                builtObject.DateRetrofit = _Galaxy.CurrentStarDate;
                            }
                        }
                    }
                    else if ((builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined) && AssignRetrofitMission(builtObject))
                    {
                        builtObject.DateRetrofit = _Galaxy.CurrentStarDate;
                    }
                    continue;
                }
                bool flag3 = true;
                switch (builtObject.SubRole)
                {
                    case BuiltObjectSubRole.ResupplyShip:
                    case BuiltObjectSubRole.ColonyShip:
                    case BuiltObjectSubRole.ConstructionShip:
                        flag3 = PirateEmpireBaseHabitat != null || CanBuildBuiltObject(builtObject, Capital);
                        break;
                    default:
                        flag3 = CanBuildBuiltObject(builtObject);
                        break;
                }
                if (builtObject.Role == BuiltObjectRole.Base)
                {
                    flag3 = CanBuildBuiltObject(builtObject, builtObject.ParentHabitat);
                }
                if (!flag3)
                {
                    continue;
                }
                builtObject.RetrofitForNextMission = true;
                if (builtObject.Role == BuiltObjectRole.Base)
                {
                    if (AssignRetrofitMission(builtObject))
                    {
                        builtObject.DateRetrofit = _Galaxy.CurrentStarDate;
                    }
                    continue;
                }
                BuiltObjectMission mission2 = builtObject.Mission;
                if (mission2 == null || mission2.Type == BuiltObjectMissionType.Undefined)
                {
                    if (AssignRetrofitMission(builtObject))
                    {
                        builtObject.DateRetrofit = _Galaxy.CurrentStarDate;
                    }
                }
                else
                {
                    if (mission2.Type == BuiltObjectMissionType.Retrofit || builtObject.SubsequentMissions.ContainsType(BuiltObjectMissionType.Retrofit))
                    {
                        continue;
                    }
                    Design design7 = _Designs.FindNewestCanBuild(builtObject.SubRole, builtObject.ActualEmpire);
                    if (design7 != null && design7 != builtObject.Design)
                    {
                        StellarObject stellarObject = null;
                        switch (builtObject.SubRole)
                        {
                            case BuiltObjectSubRole.ResupplyShip:
                            case BuiltObjectSubRole.ColonyShip:
                            case BuiltObjectSubRole.ConstructionShip:
                                stellarObject = FindNearestShipYard(builtObject, canRepairOrBuild: true, includeVerySmallYards: true);
                                break;
                            default:
                                stellarObject = FindNearestShipYard(builtObject, canRepairOrBuild: true, includeVerySmallYards: false);
                                break;
                        }
                        builtObject.QueueMission(BuiltObjectMissionType.Retrofit, stellarObject, null, design7, BuiltObjectMissionPriority.Normal);
                    }
                }
            }
        }

        private double CalculateRetrofitCosts(BuiltObjectList builtObjects)
        {
            double num = 0.0;
            if (builtObjects != null)
            {
                for (int i = 0; i < builtObjects.Count; i++)
                {
                    BuiltObject builtObject = builtObjects[i];
                    if (builtObject != null && builtObject.RetrofitDesign == null && builtObject.BuiltAt == null)
                    {
                        double num2 = Galaxy.ResolveBuildSpeed(this, _Galaxy, builtObject);
                        if (num2 <= 1.0 && !builtObject.SuppressAutoRetrofit)
                        {
                            num += CalculateRetrofitCost(builtObject);
                        }
                    }
                }
            }
            return num;
        }

        public StellarObject UltraFastFindNearestRefuellingLocation(double x, double y, ResourceList fuelTypes, BuiltObject shipToRefuel, bool mustHaveActualSupply)
        {
            return UltraFastFindNearestRefuellingLocation(x, y, fuelTypes, shipToRefuel, mustHaveActualSupply, includeResupplyShips: false);
        }

        public StellarObject UltraFastFindNearestRefuellingLocation(double x, double y, ResourceList fuelTypes, BuiltObject shipToRefuel, bool mustHaveActualSupply, bool includeResupplyShips)
        {
            return UltraFastFindNearestRefuellingLocation(x, y, fuelTypes, shipToRefuel, mustHaveActualSupply, includeResupplyShips, 1);
        }

        public StellarObject UltraFastFindNearestRefuellingLocation(double x, double y, ResourceList fuelTypes, BuiltObject shipToRefuel, bool mustHaveActualSupply, bool includeResupplyShips, int shipsToRefuel)
        {
            StellarObject result = null;
            double num = double.MaxValue;
            int num2 = 50;
            StellarObject stellarObject = null;
            if (includeResupplyShips)
            {
                StellarObject[] array = _Galaxy.SortStellarObjectsByDistanceThreadsafe(x, y, _RefuellingLocationsMilitaryOnly);
                foreach (StellarObject stellarObject2 in array)
                {
                    if (stellarObject2 == null || !(stellarObject2 is BuiltObject))
                    {
                        continue;
                    }
                    BuiltObject builtObject = (BuiltObject)stellarObject2;
                    if (_Galaxy.CheckFuelSuppliedAtLocation(fuelTypes, builtObject, this, mustHaveActualSupply))
                    {
                        if (builtObject.DockingBayWaitQueue != null && builtObject.DockingBayWaitQueue.Count < num2 && builtObject.DockingBays != null && builtObject.DockingBays.Count > 0)
                        {
                            result = builtObject;
                            num = _Galaxy.CalculateDistance(x, y, builtObject.Xpos, builtObject.Ypos);
                            break;
                        }
                        if (stellarObject == null)
                        {
                            stellarObject = builtObject;
                        }
                    }
                }
            }
            StellarObject[] array2 = _Galaxy.SortStellarObjectsByDistanceThreadsafe(x, y, _RefuellingLocations);
            foreach (StellarObject stellarObject3 in array2)
            {
                if (stellarObject3 == null || !_Galaxy.CheckEmpireCanRefuelAtEmpire(shipToRefuel, this, stellarObject3.Empire))
                {
                    continue;
                }
                if (stellarObject3 is BuiltObject)
                {
                    BuiltObject builtObject2 = (BuiltObject)stellarObject3;
                    if (!_Galaxy.CheckFuelSuppliedAtLocation(fuelTypes, builtObject2, this, mustHaveActualSupply))
                    {
                        continue;
                    }
                    bool flag = false;
                    if (shipsToRefuel <= 1)
                    {
                        if ((builtObject2.DockingBays != null && builtObject2.DockingBays.Count >= 4) || (builtObject2.DockingBayWaitQueue != null && builtObject2.DockingBayWaitQueue.Count <= 3))
                        {
                            flag = true;
                        }
                    }
                    else if (builtObject2.DockingBays != null && builtObject2.DockingBays.Count >= 4)
                    {
                        flag = true;
                    }
                    if (!flag)
                    {
                        continue;
                    }
                    if (builtObject2.DockingBayWaitQueue != null && builtObject2.DockingBayWaitQueue.Count < num2 && builtObject2.DockingBays != null && builtObject2.DockingBays.Count > 0)
                    {
                        double num3 = _Galaxy.CalculateDistance(x, y, builtObject2.Xpos, builtObject2.Ypos);
                        if (num3 < num)
                        {
                            return builtObject2;
                        }
                        return result;
                    }
                    if (stellarObject == null)
                    {
                        stellarObject = builtObject2;
                    }
                }
                else
                {
                    if (!(stellarObject3 is Habitat))
                    {
                        continue;
                    }
                    Habitat habitat = (Habitat)stellarObject3;
                    if (!_Galaxy.CheckEmpireCanRefuelAtEmpire(shipToRefuel, this, habitat.Empire) || !_Galaxy.CheckSufficientFuelAvailable(this, fuelTypes, habitat, habitat.Empire))
                    {
                        continue;
                    }
                    if (habitat.DockingBayWaitQueue != null && habitat.DockingBayWaitQueue.Count < num2)
                    {
                        double num4 = _Galaxy.CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
                        if (num4 < num)
                        {
                            return habitat;
                        }
                        return result;
                    }
                    if (stellarObject == null)
                    {
                        stellarObject = habitat;
                    }
                }
            }
            if (stellarObject != null)
            {
                return stellarObject;
            }
            return null;
        }

        public void UpdateEmpireRefuellingLocations()
        {
            StellarObjectList stellarObjectList = new StellarObjectList();
            StellarObjectList stellarObjectList2 = new StellarObjectList();
            for (int i = 0; i < ResupplyShips.Count; i++)
            {
                BuiltObject builtObject = ResupplyShips[i];
                if (builtObject != null && builtObject.IsFunctional && builtObject.IsDeployed && !builtObject.HasBeenDestroyed)
                {
                    stellarObjectList.Add(builtObject);
                }
            }
            for (int j = 0; j < RefuellingDepots.Count; j++)
            {
                BuiltObject builtObject2 = RefuellingDepots[j];
                if (builtObject2 != null && !builtObject2.HasBeenDestroyed && (builtObject2.SubRole != BuiltObjectSubRole.ResupplyShip || builtObject2.IsDeployed) && builtObject2.ParentHabitat == null && builtObject2.IsFunctional)
                {
                    stellarObjectList.Add(builtObject2);
                }
            }
            for (int k = 0; k < _Galaxy.Systems.Count; k++)
            {
                SystemInfo systemInfo = _Galaxy.Systems[k];
                if (systemInfo == null || systemInfo.SystemStar == null)
                {
                    continue;
                }
                SystemVisibilityStatus systemVisibilityStatus = SystemVisibilityStatus.Visible;
                if (this != _Galaxy.IndependentEmpire)
                {
                    systemVisibilityStatus = CheckSystemVisibilityStatus(systemInfo.SystemStar.SystemIndex);
                }
                if (systemVisibilityStatus != SystemVisibilityStatus.Explored && systemVisibilityStatus != SystemVisibilityStatus.Visible)
                {
                    continue;
                }
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    if (systemInfo.SystemStar.BasesAtHabitat.Count <= 0)
                    {
                        continue;
                    }
                    for (int l = 0; l < systemInfo.SystemStar.BasesAtHabitat.Count; l++)
                    {
                        BuiltObject builtObject3 = systemInfo.SystemStar.BasesAtHabitat[l];
                        if (builtObject3 != null && !builtObject3.HasBeenDestroyed && builtObject3.IsRefuellingDepot && builtObject3.Empire != null)
                        {
                            bool flag = true;
                            if (PirateEmpireBaseHabitat == null && this != _Galaxy.IndependentEmpire)
                            {
                                flag = IsObjectVisibleToThisEmpire(builtObject3, includeLongRangeScanners: true, includeShipsOutsideSystems: false);
                            }
                            if (flag && _Galaxy.IsStellarObjectDockable(builtObject3, this))
                            {
                                stellarObjectList2.Add(builtObject3);
                            }
                        }
                    }
                    continue;
                }
                for (int m = 0; m < systemInfo.Habitats.Count; m++)
                {
                    Habitat habitat = systemInfo.Habitats[m];
                    if (habitat == null)
                    {
                        continue;
                    }
                    if (habitat.BasesAtHabitat.Count > 0)
                    {
                        for (int n = 0; n < habitat.BasesAtHabitat.Count; n++)
                        {
                            BuiltObject builtObject4 = habitat.BasesAtHabitat[n];
                            if (builtObject4 != null && !builtObject4.HasBeenDestroyed && builtObject4.IsRefuellingDepot && builtObject4.Empire != null && _Galaxy.IsStellarObjectDockable(builtObject4, this))
                            {
                                bool flag2 = true;
                                if (this != _Galaxy.IndependentEmpire && builtObject4.SubRole != BuiltObjectSubRole.Outpost && builtObject4.SubRole != BuiltObjectSubRole.SmallSpacePort && builtObject4.SubRole != BuiltObjectSubRole.MediumSpacePort && builtObject4.SubRole != BuiltObjectSubRole.LargeSpacePort)
                                {
                                    flag2 = IsObjectVisibleToThisEmpire(builtObject4, includeLongRangeScanners: true, includeShipsOutsideSystems: false);
                                }
                                if (flag2)
                                {
                                    stellarObjectList2.Add(builtObject4);
                                }
                            }
                        }
                    }
                    if (habitat.Population.Count > 0 && habitat.IsRefuellingDepot && habitat.Empire != null && _Galaxy.IsStellarObjectDockable(habitat, this))
                    {
                        stellarObjectList2.Add(habitat);
                    }
                }
            }
            _RefuellingLocations = stellarObjectList2;
            _RefuellingLocationsMilitaryOnly = stellarObjectList;
        }

        private void ProcessCharacters(double timePassed)
        {
            if (Characters != null)
            {
                for (int i = 0; i < Characters.Count; i++)
                {
                    Character character = Characters[i];
                    character.DoTasks(_Galaxy);
                }
            }
        }

        public int CharactersCanGenerateAmountIntelligenceAgent()
        {
            int agentCount = 0;
            return CharactersCanGenerateAmountIntelligenceAgent(out agentCount);
        }

        public int CharactersCanGenerateAmountIntelligenceAgent(out int agentCount)
        {
            agentCount = Characters.CountCharactersByRole(CharacterRole.IntelligenceAgent);
            int maximumAgentCount = MaximumAgentCount;
            return maximumAgentCount - agentCount;
        }

        public int MaximumCharactersAllowedNonIntelligenceAgent()
        {
            if (PirateEmpireBaseHabitat == null)
            {
                return Math.Min(20, (int)(Math.Sqrt(TotalColonyStrategicValue) / 150.0));
            }
            return Math.Min(20, (int)(Math.Sqrt(BuiltObjects.Count) + 2.0));
        }

        public int CharactersCanGenerateAmountNonIntelligenceAgent()
        {
            int otherCharacterCount = 0;
            return CharactersCanGenerateAmountNonIntelligenceAgent(out otherCharacterCount);
        }

        public int CharactersCanGenerateAmountNonIntelligenceAgent(out int otherCharacterCount)
        {
            int num = Characters.CountCharactersByRole(CharacterRole.IntelligenceAgent);
            otherCharacterCount = Characters.Count - num;
            int num2 = MaximumCharactersAllowedNonIntelligenceAgent();
            return num2 - otherCharacterCount;
        }

        private void CheckForCharacterAppearance()
        {
            if (Characters == null || DominantRace == null || this == _Galaxy.IndependentEmpire || _Galaxy.DeferEventsForGameStart)
            {
                return;
            }
            int otherCharacterCount = 0;
            int agentCount = 0;
            int num = CharactersCanGenerateAmountNonIntelligenceAgent(out otherCharacterCount);
            CharactersCanGenerateAmountIntelligenceAgent(out agentCount);
            if (num <= 0 && agentCount <= 0)
            {
                return;
            }
            double num2 = 11.5;
            double num3 = 1.0;
            double num4 = 1.0;
            double num5 = 2.0;
            double num6 = 2.0;
            double num7 = 1.0;
            double num8 = 1.0;
            double num9 = 3.5;
            double num10 = 1.0;
            if (DominantRace != null)
            {
                num3 = ((PirateEmpireBaseHabitat != null) ? (num3 * DominantRace.CharacterRandomAppearanceChancePirateLeader) : (num3 * DominantRace.CharacterRandomAppearanceChanceLeader));
                num4 *= DominantRace.CharacterRandomAppearanceChanceAmbassador;
                num5 *= DominantRace.CharacterRandomAppearanceChanceGovernor;
                num6 *= DominantRace.CharacterRandomAppearanceChanceAdmiral;
                num7 *= DominantRace.CharacterRandomAppearanceChanceGeneral;
                num8 *= DominantRace.CharacterRandomAppearanceChanceScientist;
                num9 *= DominantRace.CharacterRandomAppearanceChanceIntelligenceAgent;
                num10 *= DominantRace.CharacterRandomAppearanceChanceShipCaptain;
            }
            double num11 = Math.Min(2.0, 1.0 + (double)CumulateFacilityValue1(PlanetaryFacilityType.MilitaryAcademy, mustBeCompleted: true) / 100.0);
            double num12 = Math.Min(2.0, 1.0 + (double)CumulateFacilityValue1(PlanetaryFacilityType.NavalAcademy, mustBeCompleted: true) / 100.0);
            double num13 = Math.Min(2.0, 1.0 + (double)CumulateFacilityValue1(PlanetaryFacilityType.ScienceAcademy, mustBeCompleted: true) / 100.0);
            double num14 = Math.Min(2.0, 1.0 + (double)CumulateFacilityValue1(PlanetaryFacilityType.SpyAcademy, mustBeCompleted: true) / 100.0);
            num7 *= num11;
            num6 *= num12;
            num8 *= num13;
            num9 *= num14;
            int num15 = 0;
            num15 = ((PirateEmpireBaseHabitat != null) ? Characters.CountCharactersByRole(CharacterRole.PirateLeader) : Characters.CountCharactersByRole(CharacterRole.Leader));
            if (num15 > 0)
            {
                num3 = 0.0;
            }
            if (Characters.CountCharactersByRole(CharacterRole.Ambassador) <= 0)
            {
                num4 *= 1.3;
            }
            if (Characters.CountCharactersByRole(CharacterRole.ColonyGovernor) <= 0)
            {
                num5 *= 1.3;
            }
            if (Characters.CountCharactersByRole(CharacterRole.FleetAdmiral) <= 0)
            {
                num6 *= 1.3;
            }
            if (Characters.CountCharactersByRole(CharacterRole.TroopGeneral) <= 0)
            {
                num7 *= 1.3;
            }
            if (Characters.CountCharactersByRole(CharacterRole.Scientist) <= 0)
            {
                num8 *= 1.3;
            }
            if (Characters.CountCharactersByRole(CharacterRole.IntelligenceAgent) <= 0)
            {
                num9 *= 1.8;
            }
            bool flag = false;
            if (DiplomaticRelations != null)
            {
                for (int i = 0; i < DiplomaticRelations.Count; i++)
                {
                    if (DiplomaticRelations[i].Type != 0)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            if (PirateRelations != null)
            {
                for (int j = 0; j < PirateRelations.Count; j++)
                {
                    if (PirateRelations[j].Type != 0)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag)
            {
                num4 = 0.0;
                num9 = 0.0;
                num2 -= 4.5;
            }
            if (BuiltObjects.CountByRole(BuiltObjectRole.Military) <= 0)
            {
                num6 = 0.0;
                num2 -= 2.0;
            }
            if (Colonies != null && Colonies.Count < 2)
            {
                num5 = 0.0;
                num2 -= 2.0;
            }
            if (PirateEmpireBaseHabitat != null)
            {
                num3 = 0.0;
                num4 = 0.0;
                num5 = 0.0;
                num6 = 2.0;
                num7 = 0.0;
                num8 = 1.0;
                num9 = 5.0;
                num10 = 5.0;
                num6 *= DominantRace.CharacterRandomAppearanceChanceAdmiral;
                num8 *= DominantRace.CharacterRandomAppearanceChanceScientist;
                num9 *= DominantRace.CharacterRandomAppearanceChanceIntelligenceAgent;
                num10 *= DominantRace.CharacterRandomAppearanceChanceShipCaptain;
                num2 = 13.0;
            }
            double num16 = num3 + num4 + num5 + num6 + num7 + num8 + num9 + num10;
            int num17 = 25;
            num17 = Math.Min(num17, Math.Max(2, (int)((double)num17 / (num16 / num2))));
            if (Galaxy.Rnd.Next(0, num17) != 1)
            {
                return;
            }
            CharacterRole characterRole = CharacterRole.Undefined;
            int num18 = 0;
            int num19 = 20;
            while (characterRole == CharacterRole.Undefined && num18 < num19)
            {
                double num20 = 0.0;
                double num21 = num3;
                double num22 = num21;
                double num23 = num22 + num4;
                double num24 = num23;
                double num25 = num24 + num5;
                double num26 = num25;
                double num27 = num26 + num6;
                double num28 = num27;
                double num29 = num28 + num7;
                double num30 = num29;
                double num31 = num30 + num8;
                double num32 = num31;
                double num33 = num32 + num9;
                double num34 = num33;
                double num35 = num34 + num10;
                double num36 = Galaxy.Rnd.NextDouble() * num16;
                int num37 = 0;
                if (num36 >= num20 && num36 < num21)
                {
                    characterRole = CharacterRole.Leader;
                    num37 = 1;
                }
                else if (num36 >= num22 && num36 < num23)
                {
                    characterRole = CharacterRole.Ambassador;
                    num37 = Math.Max(1, (int)((double)CountEmpiresWeHaveMet() * 0.2));
                }
                else if (num36 >= num24 && num36 < num25)
                {
                    characterRole = CharacterRole.ColonyGovernor;
                    num37 = Math.Max(1, (int)((double)Colonies.Count * 0.34));
                }
                else if (num36 >= num26 && num36 < num27)
                {
                    characterRole = CharacterRole.FleetAdmiral;
                    num37 = Math.Max(1, (int)((double)Colonies.Count * 0.34));
                }
                else if (num36 >= num28 && num36 < num29)
                {
                    characterRole = CharacterRole.TroopGeneral;
                    num37 = Math.Max(1, (int)((double)Colonies.Count * 0.34));
                }
                else if (num36 >= num30 && num36 < num31)
                {
                    characterRole = CharacterRole.Scientist;
                    num37 = Math.Max(1, (int)((double)Colonies.Count * 0.25));
                }
                else if (num36 >= num32 && num36 < num33)
                {
                    characterRole = CharacterRole.IntelligenceAgent;
                    num37 = MaximumAgentCount;
                }
                else if (num36 >= num34 && num36 < num35)
                {
                    characterRole = CharacterRole.ShipCaptain;
                    num37 = Math.Max(1, (int)(Math.Sqrt(BuiltObjects.Count) + 1.0));
                }
                int num38 = Characters.CountCharactersByRole(characterRole);
                if (num38 >= num37)
                {
                    characterRole = CharacterRole.Undefined;
                }
                else
                {
                    double num39 = (double)num38 / (double)otherCharacterCount;
                    if (num39 > 0.4)
                    {
                        characterRole = CharacterRole.Undefined;
                    }
                }
                num18++;
            }
            if (characterRole == CharacterRole.Undefined)
            {
                return;
            }
            bool isRandomCharacter = false;
            Character character = GenerateNewCharacter(characterRole, null, activate: false, out isRandomCharacter);
            StellarObject stellarObject = Capital;
            switch (characterRole)
            {
                case CharacterRole.Leader:
                    stellarObject = Capital;
                    break;
                case CharacterRole.Ambassador:
                    stellarObject = Capital;
                    break;
                case CharacterRole.ColonyGovernor:
                    stellarObject = ReviewCharacterLocation(character, transferToLocation: false);
                    break;
                case CharacterRole.FleetAdmiral:
                    stellarObject = ReviewCharacterLocation(character, transferToLocation: false);
                    break;
                case CharacterRole.TroopGeneral:
                    stellarObject = ReviewCharacterLocation(character, transferToLocation: false);
                    break;
                case CharacterRole.IntelligenceAgent:
                    {
                        if (PirateEmpireBaseHabitat == null)
                        {
                            stellarObject = Capital;
                            break;
                        }
                        BuiltObject builtObject2 = _Galaxy.IdentifyPirateBase(this);
                        if (builtObject2 != null && !builtObject2.HasBeenDestroyed)
                        {
                            stellarObject = builtObject2;
                            break;
                        }
                        for (int k = 0; k < BuiltObjects.Count; k++)
                        {
                            BuiltObject builtObject3 = BuiltObjects[k];
                            if (builtObject3 != null && !builtObject3.HasBeenDestroyed && builtObject3.Role == BuiltObjectRole.Base)
                            {
                                stellarObject = builtObject3;
                                break;
                            }
                        }
                        break;
                    }
                case CharacterRole.Scientist:
                    stellarObject = ReviewCharacterLocation(character, transferToLocation: false);
                    break;
                case CharacterRole.PirateLeader:
                    {
                        BuiltObject builtObject = _Galaxy.IdentifyPirateBase(this);
                        if (builtObject != null && !builtObject.HasBeenDestroyed)
                        {
                            stellarObject = builtObject;
                        }
                        break;
                    }
                case CharacterRole.ShipCaptain:
                    stellarObject = ReviewCharacterLocation(character, transferToLocation: false);
                    break;
            }
            if (stellarObject == null)
            {
                if (PirateEmpireBaseHabitat == null)
                {
                    stellarObject = Capital;
                }
                else
                {
                    if (BuiltObjects.Count > 0)
                    {
                        stellarObject = BuiltObjects[0];
                    }
                    if (stellarObject == null && PrivateBuiltObjects.Count > 0)
                    {
                        stellarObject = PrivateBuiltObjects[0];
                    }
                }
            }
            if (stellarObject != null)
            {
                character.Activate(_Galaxy, this, stellarObject);
                _Galaxy.DoCharacterEvent(CharacterEventType.CharacterStart, character, character);
                if (character.Role == CharacterRole.IntelligenceAgent)
                {
                    _Galaxy.DoCharacterEventLeader(CharacterEventType.IntelligenceAgentRecruited, character, this);
                    IntelligenceMission intelligenceMission = new IntelligenceMission(this, character, _Galaxy.CurrentStarDate);
                    intelligenceMission.TimeLength = Galaxy.RealSecondsInGalacticYear * 1000 / 4;
                    character.Mission = intelligenceMission;
                }
                string description = string.Format(TextResolver.GetText("New Character Appeared Message ROLE NAME LOCATION"), Galaxy.ResolveDescription(character.Role), character.Name, stellarObject.Name);
                SendMessageToEmpire(this, EmpireMessageType.CharacterAppearance, character, description);
            }
            else
            {
                character.Kill(_Galaxy);
            }
        }

        public void GenerateStartingCharacters()
        {
            GenerateStartingCharacters(null);
        }

        public void GenerateStartingCharacters(StellarObject startLocation)
        {
            if (startLocation == null)
            {
                startLocation = Capital;
            }
            CharacterList characterList = new CharacterList();
            if (PirateEmpireBaseHabitat == null)
            {
                characterList = AvailableCharacters.ObtainStartingCharacters();
                if (_Galaxy.AllowRaceStartingCharacters && DominantRace != null && DominantRace.AvailableCharacters != null)
                {
                    characterList.AddRange(DominantRace.AvailableCharacters.ObtainStartingCharacters());
                }
            }
            else
            {
                List<CharacterRole> list = new List<CharacterRole>();
                list.Add(CharacterRole.Ambassador);
                list.Add(CharacterRole.ColonyGovernor);
                list.Add(CharacterRole.Leader);
                list.Add(CharacterRole.TroopGeneral);
                List<CharacterRole> rolesToExclude = list;
                characterList = AvailableCharacters.ObtainStartingCharactersExcludingRoles(rolesToExclude);
                if (_Galaxy.AllowRaceStartingCharacters && DominantRace != null && DominantRace.AvailableCharacters != null)
                {
                    characterList.AddRange(DominantRace.AvailableCharacters.ObtainStartingCharactersExcludingRoles(rolesToExclude));
                }
            }
            if (PirateEmpireBaseHabitat == null)
            {
                if (characterList.CountCharactersByRole(CharacterRole.Leader) <= 0)
                {
                    bool isRandomCharacter = false;
                    Character item = GenerateNewCharacter(CharacterRole.Leader, startLocation, activate: false, out isRandomCharacter);
                    characterList.Add(item);
                }
            }
            else if (characterList.CountCharactersByRole(CharacterRole.PirateLeader) <= 0)
            {
                bool isRandomCharacter2 = false;
                Character item2 = GenerateNewCharacter(CharacterRole.PirateLeader, startLocation, activate: false, out isRandomCharacter2);
                characterList.Add(item2);
            }
            int num = 1;
            if (DominantRace != null)
            {
                num = 1 + DominantRace.IntelligenceAgentAdditional;
            }
            int num2 = characterList.CountCharactersByRole(CharacterRole.IntelligenceAgent);
            if (num2 < num)
            {
                int num3 = num - num2;
                for (int i = 0; i < num3; i++)
                {
                    bool isRandomCharacter3 = false;
                    Character item3 = GenerateNewCharacter(CharacterRole.IntelligenceAgent, startLocation, activate: false, out isRandomCharacter3);
                    characterList.Add(item3);
                }
            }
            for (int j = 0; j < characterList.Count; j++)
            {
                Character character = characterList[j];
                StellarObject stellarObject = ReviewCharacterLocation(character, transferToLocation: false);
                if (stellarObject == null)
                {
                    stellarObject = startLocation;
                }
                character.Activate(_Galaxy, this, stellarObject);
                _Galaxy.DoCharacterEvent(CharacterEventType.CharacterStart, character, character);
                if (character.Role == CharacterRole.Leader || character.Role == CharacterRole.Scientist || character.Role == CharacterRole.PirateLeader)
                {
                    character.BonusesKnown = true;
                }
            }
        }

        public bool GenerateNewCharacterFromCustom(Character character, StellarObject location)
        {
            if (DominantRace != null && DominantRace.AvailableCharacters != null && DominantRace.AvailableCharacters.ActivateAndRemoveCharacter(character, _Galaxy, this, location))
            {
                if (character.Role == CharacterRole.Leader || character.Role == CharacterRole.Scientist || character.Role == CharacterRole.PirateLeader)
                {
                    character.BonusesKnown = true;
                }
                _Galaxy.DoCharacterEvent(CharacterEventType.CharacterStart, character, character);
                short matchingGameEventIdCharacterAppears = _Galaxy.GetMatchingGameEventIdCharacterAppears(character);
                _Galaxy.CheckTriggerEvent(matchingGameEventIdCharacterAppears, this, EventTriggerType.CharacterAppears, character);
                return true;
            }
            return false;
        }

        public Character GenerateNewCharacter(CharacterRole role, StellarObject location)
        {
            bool isRandomCharacter = false;
            return GenerateNewCharacter(role, location, activate: true, out isRandomCharacter);
        }

        public Character GenerateNewCharacter(CharacterRole role, StellarObject location, out bool isRandomCharacter)
        {
            return GenerateNewCharacter(role, location, activate: true, out isRandomCharacter);
        }

        public Character GenerateNewCharacter(CharacterRole role, StellarObject location, bool activate, out bool isRandomCharacter)
        {
            isRandomCharacter = false;
            Character character = AvailableCharacters.ObtainNextCharacter(role);
            if (character == null && _Galaxy.AllowRaceStartingCharacters && DominantRace != null && DominantRace.AvailableCharacters != null)
            {
                character = DominantRace.AvailableCharacters.ObtainNextCharacter(role);
            }
            if (character == null)
            {
                Race race = DominantRace;
                if (role != CharacterRole.Leader && role != CharacterRole.PirateLeader && Galaxy.Rnd.Next(0, 4) == 1)
                {
                    race = SelectRandomRaceFromColoniesPreferNonDominant();
                    if (race == null)
                    {
                        race = DominantRace;
                    }
                }
                if (race == null)
                {
                    race = _Galaxy.SelectRandomRace(0);
                }
                string name = GenerateAgentName(race);
                character = new Character(name, role, string.Empty, race, this, location, 0);
                isRandomCharacter = true;
            }
            if (character != null)
            {
                if (isRandomCharacter)
                {
                    ApplyRandomCharacterSkillsTraits(character, boostSkillLevels: false);
                }
                if (character.Role == CharacterRole.Leader || character.Role == CharacterRole.Scientist || character.Role == CharacterRole.PirateLeader)
                {
                    character.BonusesKnown = true;
                }
                if (activate && location != null)
                {
                    character.Activate(_Galaxy, this, location);
                    _Galaxy.DoCharacterEvent(CharacterEventType.CharacterStart, character, character);
                    short matchingGameEventIdCharacterAppears = _Galaxy.GetMatchingGameEventIdCharacterAppears(character);
                    _Galaxy.CheckTriggerEvent(matchingGameEventIdCharacterAppears, this, EventTriggerType.CharacterAppears, character);
                }
            }
            BaconEmpire.EnhanceCharacter(this, character);
            return character;
        }

        public Character GenerateNewCharacterRandom(CharacterRole role, StellarObject location, bool activate)
        {
            Race race = DominantRace;
            if (role != CharacterRole.Leader && role != CharacterRole.PirateLeader && Galaxy.Rnd.Next(0, 4) == 1)
            {
                race = SelectRandomRaceFromColoniesPreferNonDominant();
                if (race == null)
                {
                    race = DominantRace;
                }
            }
            if (race == null)
            {
                race = _Galaxy.SelectRandomRace(0);
            }
            string name = GenerateAgentName(race);
            Character character = new Character(name, role, string.Empty, race, this, location, 0);
            ApplyRandomCharacterSkillsTraits(character, boostSkillLevels: false);
            if (character.Role == CharacterRole.Leader || character.Role == CharacterRole.Scientist || character.Role == CharacterRole.PirateLeader)
            {
                character.BonusesKnown = true;
            }
            if (activate && location != null)
            {
                character.Activate(_Galaxy, this, location);
                _Galaxy.DoCharacterEvent(CharacterEventType.CharacterStart, character, character);
                short matchingGameEventIdCharacterAppears = _Galaxy.GetMatchingGameEventIdCharacterAppears(character);
                _Galaxy.CheckTriggerEvent(matchingGameEventIdCharacterAppears, this, EventTriggerType.CharacterAppears, character);
            }
            return character;
        }

        public Race SelectRandomRaceFromColoniesPreferNonDominant()
        {
            Race race = null;
            if (Colonies != null && Colonies.Count > 0 && DominantRace != null)
            {
                int num = 0;
                while ((race == null || race == DominantRace) && num < 10)
                {
                    int index = Galaxy.Rnd.Next(0, Colonies.Count);
                    Habitat habitat = Colonies[index];
                    if (habitat != null && habitat.Population != null && habitat.Population.Count > 0)
                    {
                        int index2 = Galaxy.Rnd.Next(0, habitat.Population.Count);
                        Population population = habitat.Population[index2];
                        if (population != null)
                        {
                            race = population.Race;
                            if (race != null)
                            {
                                if (race == DominantRace)
                                {
                                    race = null;
                                }
                                else if (race.FamilyId == DominantRace.FamilyId)
                                {
                                    if (habitat.ColonyPopulationPolicyRaceFamily != 0)
                                    {
                                        race = null;
                                    }
                                }
                                else if (habitat.ColonyPopulationPolicy != 0)
                                {
                                    race = null;
                                }
                            }
                        }
                    }
                    num++;
                }
            }
            return race;
        }

        public RaceList DetermineEmpireRaces(out List<long> populationAmounts)
        {
            RaceList raceList = new RaceList();
            populationAmounts = new List<long>();
            if (Colonies != null)
            {
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat = Colonies[i];
                    if (habitat == null || habitat.Population == null)
                    {
                        continue;
                    }
                    for (int j = 0; j < habitat.Population.Count; j++)
                    {
                        Population population = habitat.Population[j];
                        if (population != null)
                        {
                            int num = raceList.IndexOf(population.Race);
                            if (num >= 0)
                            {
                                populationAmounts[num] += population.Amount;
                                continue;
                            }
                            raceList.Add(population.Race);
                            populationAmounts.Add(population.Amount);
                        }
                    }
                }
            }
            return raceList;
        }

        public void ApplyRandomCharacterSkillsTraits(Character character, bool boostSkillLevels)
        {
            if (character == null)
            {
                return;
            }
            CharacterRole role = character.Role;
            List<CharacterSkillType> list = Character.DetermineValidSkillsForRole(role, primarySkills: true, secondarySkills: false);
            List<CharacterSkillType> list2 = Character.DetermineValidSkillsForRole(role, primarySkills: false, secondarySkills: true);
            int num = 4;
            int num2 = 1;
            if (PirateEmpireBaseHabitat != null && role == CharacterRole.IntelligenceAgent)
            {
                num2++;
                boostSkillLevels = true;
            }
            int iterationCount = 0;
            while (Galaxy.ConditionCheckLimit(Galaxy.Rnd.Next(0, 2) == 1, 50, ref iterationCount))
            {
                num2++;
                if (num2 >= list.Count + list2.Count || num2 >= num)
                {
                    break;
                }
            }
            List<CharacterSkillType> list3 = new List<CharacterSkillType>();
            for (int i = 0; i < num2; i++)
            {
                CharacterSkillType characterSkillType = CharacterSkillType.Undefined;
                int num3 = 0;
                while (characterSkillType == CharacterSkillType.Undefined && num3 < 20)
                {
                    if (list2.Count > 0 && Galaxy.Rnd.Next(0, 3) == 1)
                    {
                        int index = Galaxy.Rnd.Next(0, list2.Count);
                        characterSkillType = list2[index];
                    }
                    else if (list.Count > 0)
                    {
                        int index2 = Galaxy.Rnd.Next(0, list.Count);
                        characterSkillType = list[index2];
                    }
                    if (list3.Contains(characterSkillType))
                    {
                        characterSkillType = CharacterSkillType.Undefined;
                    }
                    num3++;
                }
                if (characterSkillType != 0 && !list3.Contains(characterSkillType))
                {
                    list3.Add(characterSkillType);
                }
            }
            for (int j = 0; j < list3.Count; j++)
            {
                int maxValue = 4;
                if (boostSkillLevels)
                {
                    maxValue = 6;
                }
                character.AddSkill(level: (character.Role != CharacterRole.Leader && character.Role != CharacterRole.PirateLeader) ? ((Galaxy.Rnd.Next(0, maxValue) != 1) ? ((!boostSkillLevels) ? Galaxy.Rnd.Next(5, 16) : Galaxy.Rnd.Next(7, 21)) : ((!boostSkillLevels) ? Galaxy.Rnd.Next(-5, -1) : Galaxy.Rnd.Next(-3, -1))) : ((Galaxy.Rnd.Next(0, maxValue) != 1) ? ((!boostSkillLevels) ? Galaxy.Rnd.Next(2, 10) : Galaxy.Rnd.Next(5, 13)) : ((!boostSkillLevels) ? Galaxy.Rnd.Next(-5, -1) : Galaxy.Rnd.Next(-3, -1))), skillType: list3[j], galaxy: null);
            }
            List<CharacterTraitType> list4 = Character.DetermineValidTraitsForRole(role, includeStartingTraits: true, includeHighlyNegativeTraits: false);
            int num5 = 2;
            int num6 = 1;
            if (PirateEmpireBaseHabitat != null && role == CharacterRole.IntelligenceAgent)
            {
                num6++;
            }
            int iterationCount2 = 0;
            while (Galaxy.ConditionCheckLimit(Galaxy.Rnd.Next(0, 2) == 1, 50, ref iterationCount2))
            {
                num6++;
                if (num6 >= list4.Count || num6 >= num5)
                {
                    break;
                }
            }
            List<CharacterTraitType> list5 = new List<CharacterTraitType>();
            for (int k = 0; k < num6; k++)
            {
                CharacterTraitType characterTraitType = CharacterTraitType.Undefined;
                int num7 = 0;
                while (characterTraitType == CharacterTraitType.Undefined && num7 < 20)
                {
                    if (list4.Count > 0)
                    {
                        int index3 = Galaxy.Rnd.Next(0, list4.Count);
                        characterTraitType = list4[index3];
                    }
                    if (list5.Contains(characterTraitType))
                    {
                        characterTraitType = CharacterTraitType.Undefined;
                    }
                    num7++;
                }
                if (characterTraitType != 0 && !list5.Contains(characterTraitType))
                {
                    list5.Add(characterTraitType);
                }
            }
            for (int l = 0; l < list5.Count; l++)
            {
                character.AddTrait(list5[l], starting: true, null);
            }
            if (DominantRace != null)
            {
                CharacterTraitType characterTraitType2 = CharacterTraitType.Undefined;
                switch (role)
                {
                    case CharacterRole.Ambassador:
                        characterTraitType2 = DominantRace.CharacterStartingTraitAmbassador;
                        break;
                    case CharacterRole.ColonyGovernor:
                        characterTraitType2 = DominantRace.CharacterStartingTraitGovernor;
                        break;
                    case CharacterRole.FleetAdmiral:
                        characterTraitType2 = DominantRace.CharacterStartingTraitAdmiral;
                        break;
                    case CharacterRole.IntelligenceAgent:
                        characterTraitType2 = DominantRace.CharacterStartingTraitIntelligenceAgent;
                        break;
                    case CharacterRole.Leader:
                        characterTraitType2 = DominantRace.CharacterStartingTraitLeader;
                        break;
                    case CharacterRole.Scientist:
                        characterTraitType2 = DominantRace.CharacterStartingTraitScientist;
                        break;
                    case CharacterRole.TroopGeneral:
                        characterTraitType2 = DominantRace.CharacterStartingTraitGeneral;
                        break;
                    case CharacterRole.PirateLeader:
                        characterTraitType2 = DominantRace.CharacterStartingTraitPirateLeader;
                        break;
                    case CharacterRole.ShipCaptain:
                        characterTraitType2 = DominantRace.CharacterStartingTraitShipCaptain;
                        break;
                }
                if (characterTraitType2 != 0 && !character.Traits.Contains(characterTraitType2))
                {
                    character.AddTrait(characterTraitType2, starting: true, null);
                }
            }
        }

        private void ReviewCharacterBonusesKnown()
        {
            if (Characters == null)
            {
                return;
            }
            long currentStarDate = _Galaxy.CurrentStarDate;
            long num = currentStarDate - Galaxy.RealSecondsInGalacticYear * 1000;
            for (int i = 0; i < Characters.Count; i++)
            {
                Character character = Characters[i];
                if (character == null || character.BonusesKnown)
                {
                    continue;
                }
                switch (character.Role)
                {
                    case CharacterRole.Ambassador:
                        if (character.Location != null && character.Location is Habitat)
                        {
                            Habitat habitat2 = (Habitat)character.Location;
                            if (habitat2.Empire != character.Empire && habitat2.Empire != null && habitat2.Empire.Capital != null && habitat2.Empire.Capital == habitat2 && character.TransferArrivalDate < num)
                            {
                                character.BonusesKnown = true;
                            }
                        }
                        break;
                    case CharacterRole.ColonyGovernor:
                        if (character.Location != null && character.Location is Habitat)
                        {
                            Habitat habitat = (Habitat)character.Location;
                            if (habitat.Empire == character.Empire && character.TransferArrivalDate < num)
                            {
                                character.BonusesKnown = true;
                            }
                        }
                        break;
                }
            }
        }

        public long NextAllowableLeaderChangeDatePortion(double portion)
        {
            long nextAllowableLeaderChangeDate = NextAllowableLeaderChangeDate;
            long num = nextAllowableLeaderChangeDate - LastLeaderChangeDate;
            if (num > 0 && portion > 0.0)
            {
                long num2 = (long)((double)num * portion);
                return LastLeaderChangeDate + num2;
            }
            return nextAllowableLeaderChangeDate;
        }

        private void ReviewCharacterLeaderChange(double timePassed)
        {
            if (Characters == null || _Galaxy.DeferEventsForGameStart)
            {
                return;
            }
            if (Leader == null)
            {
                if (DominantRace != null && DominantRace.VictoryConditions != null && !DominantRace.VictoryConditions.ContainsConditionType(RaceVictoryConditionType.KeepLeaderAlive))
                {
                    double num = 0.0;
                    double leaderChangeInfluence = 0.0;
                    if (_GovernmentAttributes != null)
                    {
                        leaderChangeInfluence = _GovernmentAttributes.LeaderReplacementBoost;
                        num = _GovernmentAttributes.LeaderReplacementDisruptionLevel;
                    }
                    if (num > 0.0)
                    {
                        _LeaderChangeInfluence = num * -1.0;
                    }
                    else
                    {
                        _LeaderChangeInfluence = leaderChangeInfluence;
                    }
                    PerformChangeLeader(2);
                }
            }
            else
            {
                if (DominantRace == null || DominantRace.VictoryConditions == null || DominantRace.VictoryConditions.ContainsConditionType(RaceVictoryConditionType.KeepLeaderAlive) || _LeaderChangeInfluence != 0.0)
                {
                    return;
                }
                double num2 = 1.0;
                int num3 = 0;
                if (_GovernmentAttributes != null)
                {
                    num2 = _GovernmentAttributes.LeaderReplacementLikeliness;
                    num3 = _GovernmentAttributes.LeaderReplacementCharacterPool;
                }
                _ = 1.0 / Math.Max(0.001, num2);
                num2 *= 0.2;
                num2 *= timePassed / (double)Galaxy.RealSecondsInGalacticYear;
                switch (num3)
                {
                    case 1:
                        {
                            double num9 = Math.Max(0.1, (double)Colonies.Count * 0.34);
                            double num10 = Math.Max(0.1, (double)Characters.CountCharactersByRole(CharacterRole.ColonyGovernor) / num9);
                            num2 *= num10;
                            break;
                        }
                    case 2:
                        {
                            double num6 = Math.Max(0.1, (double)Colonies.Count * 0.4);
                            double num7 = (double)Characters.CountCharactersByRole(CharacterRole.FleetAdmiral) + (double)Characters.CountCharactersByRole(CharacterRole.TroopGeneral);
                            double num8 = Math.Max(0.1, num7 / num6);
                            num2 *= num8;
                            break;
                        }
                    case 3:
                        {
                            double num4 = Math.Max(0.1, (double)Colonies.Count * 0.34);
                            double num5 = Math.Max(0.1, (double)Characters.CountCharactersByRole(CharacterRole.Scientist) / num4);
                            num2 *= num5;
                            break;
                        }
                }
                if (WarWeariness > 0.0)
                {
                    double num11 = 1.0 + WarWeariness / 15.0;
                    num2 *= num11;
                }
                int num12 = Math.Max(1, Leader.GetSkillLevelTotal());
                double num13 = Math.Min(2.5, 50.0 / (double)num12);
                num2 *= num13;
                if (Leader.Traits.Contains(CharacterTraitType.Demoralizing))
                {
                    num2 *= 3.0;
                }
                if (Galaxy.Rnd.NextDouble() * 2.0 < num2 && _Galaxy.CurrentStarDate > NextAllowableLeaderChangeDate)
                {
                    PerformChangeLeader();
                    double num14 = 0.0;
                    double leaderChangeInfluence2 = 0.0;
                    if (_GovernmentAttributes != null)
                    {
                        num14 = _GovernmentAttributes.LeaderReplacementDisruptionLevel;
                        leaderChangeInfluence2 = _GovernmentAttributes.LeaderReplacementBoost;
                    }
                    if (num14 > 0.0)
                    {
                        _LeaderChangeInfluence = num14 * -1.0;
                    }
                    else
                    {
                        _LeaderChangeInfluence = leaderChangeInfluence2;
                    }
                }
            }
        }

        public Character PerformChangeLeader()
        {
            return PerformChangeLeader(int.MinValue);
        }

        public Character PerformChangeLeader(int changeTypeOverride)
        {
            CharacterList characterList = new CharacterList();
            CharacterTraitType characterTraitType = CharacterTraitType.Undefined;
            int num = 1;
            if (GovernmentAttributes != null)
            {
                switch (GovernmentAttributes.LeaderReplacementTypicalManner)
                {
                    case 0:
                        num = 0;
                        break;
                    case 1:
                        num = -1;
                        break;
                    case 2:
                        num = 1;
                        break;
                }
                switch (GovernmentAttributes.LeaderReplacementCharacterPool)
                {
                    case 1:
                        characterList = Characters.GetCharactersByRole(CharacterRole.ColonyGovernor);
                        break;
                    case 2:
                        characterList = Characters.GetCharactersByRole(CharacterRole.FleetAdmiral);
                        characterList.AddRange(Characters.GetCharactersByRole(CharacterRole.TroopGeneral));
                        break;
                    case 3:
                        characterList = Characters.GetCharactersByRole(CharacterRole.Scientist);
                        break;
                }
            }
            if (num == -1 && Galaxy.Rnd.Next(0, 3) == 1)
            {
                characterTraitType = CharacterTraitType.Courageous;
            }
            if (PirateEmpireBaseHabitat != null)
            {
                characterList = Characters.GetCharactersByRole(CharacterRole.FleetAdmiral);
                characterList.AddRange(Characters.GetCharactersByRole(CharacterRole.ShipCaptain));
                num = -1;
            }
            if (changeTypeOverride != int.MinValue)
            {
                num = changeTypeOverride;
            }
            Character character = ChangeLeader(characterList, num);
            if (character != null && characterTraitType != 0)
            {
                character.AddTrait(characterTraitType, starting: true, null);
            }
            LastLeaderChangeDate = _Galaxy.CurrentStarDate;
            return character;
        }

        private Character ChangeLeader(CharacterList newLeaderPool, int changeType)
        {
            Character leader = Leader;
            string text = string.Empty;
            if (leader != null)
            {
                text = leader.Name;
            }
            if (Leader != null)
            {
                Leader.Kill(_Galaxy);
                Leader = null;
            }
            if (newLeaderPool != null && newLeaderPool.Count > 0)
            {
                Character character = newLeaderPool[Galaxy.Rnd.Next(0, newLeaderPool.Count)];
                CharacterRole role = character.Role;
                if (PirateEmpireBaseHabitat != null)
                {
                    BuiltObject builtObject = _Galaxy.IdentifyPirateBase(this);
                    if (builtObject != null)
                    {
                        character.CompleteLocationTransfer(builtObject, _Galaxy);
                    }
                    else if (BuiltObjects.Count > 0)
                    {
                        character.CompleteLocationTransfer(BuiltObjects[0], _Galaxy);
                    }
                    else if (PrivateBuiltObjects.Count > 0)
                    {
                        character.CompleteLocationTransfer(PrivateBuiltObjects[0], _Galaxy);
                    }
                }
                else
                {
                    character.CompleteLocationTransfer(Capital, _Galaxy);
                }
                if (PirateEmpireBaseHabitat == null || role != CharacterRole.FleetAdmiral)
                {
                    character.RemoveAllSkillsAndTraits();
                }
                if (PirateEmpireBaseHabitat != null)
                {
                    character.Role = CharacterRole.PirateLeader;
                }
                else
                {
                    character.Role = CharacterRole.Leader;
                }
                ApplyRandomCharacterSkillsTraits(character, boostSkillLevels: true);
                character.BonusesKnown = true;
                Leader = character;
                switch (changeType)
                {
                    case -1:
                        {
                            string text5 = TextResolver.GetText("Leader Change Coup Title");
                            string message4 = string.Format(TextResolver.GetText("Leader Change Coup From Existing"), text, character.Name, Galaxy.ResolveDescription(role));
                            SendEventMessageToEmpire(EventMessageType.LeaderChange, text5, message4, character, Capital);
                            break;
                        }
                    case 0:
                        {
                            string text4 = TextResolver.GetText("Leader Change Replaced Title");
                            string message3 = string.Format(TextResolver.GetText("Leader Change Replaced From Existing"), text, character.Name, Galaxy.ResolveDescription(role));
                            SendEventMessageToEmpire(EventMessageType.LeaderChange, text4, message3, character, Capital);
                            break;
                        }
                    case 1:
                        {
                            string text3 = TextResolver.GetText("Leader Change Election Title");
                            string message2 = string.Format(TextResolver.GetText("Leader Change Election From Existing"), text, character.Name, Galaxy.ResolveDescription(role));
                            SendEventMessageToEmpire(EventMessageType.LeaderChange, text3, message2, character, Capital);
                            break;
                        }
                    default:
                        {
                            string text2 = TextResolver.GetText("Leader New Title");
                            string message = string.Format(TextResolver.GetText("Leader New From Existing"), character.Name, Galaxy.ResolveDescription(role));
                            SendEventMessageToEmpire(EventMessageType.LeaderChange, text2, message, character, Capital);
                            break;
                        }
                }
                return character;
            }
            Character character2 = null;
            if (PirateEmpireBaseHabitat != null)
            {
                BuiltObject builtObject2 = _Galaxy.IdentifyPirateBase(this);
                if (builtObject2 != null)
                {
                    character2 = GenerateNewCharacter(CharacterRole.PirateLeader, builtObject2);
                }
                else if (BuiltObjects.Count > 0)
                {
                    character2 = GenerateNewCharacter(CharacterRole.PirateLeader, BuiltObjects[0]);
                }
                else if (PrivateBuiltObjects.Count > 0)
                {
                    character2 = GenerateNewCharacter(CharacterRole.PirateLeader, PrivateBuiltObjects[0]);
                }
            }
            else
            {
                character2 = GenerateNewCharacter(CharacterRole.Leader, Capital);
            }
            if (character2 != null)
            {
                int num = 0;
                while (character2.Name == text && num < 20)
                {
                    character2.Name = GenerateAgentName();
                    num++;
                }
                Leader = character2;
                switch (changeType)
                {
                    case -1:
                        {
                            string text9 = TextResolver.GetText("Leader Change Coup Title");
                            string message8 = string.Format(TextResolver.GetText("Leader Change Coup"), text, character2.Name);
                            SendEventMessageToEmpire(EventMessageType.LeaderChange, text9, message8, character2, Capital);
                            break;
                        }
                    case 0:
                        {
                            string text8 = TextResolver.GetText("Leader Change Replaced Title");
                            string message7 = string.Format(TextResolver.GetText("Leader Change Replaced"), text, character2.Name);
                            SendEventMessageToEmpire(EventMessageType.LeaderChange, text8, message7, character2, Capital);
                            break;
                        }
                    case 1:
                        {
                            string text7 = TextResolver.GetText("Leader Change Election Title");
                            string message6 = string.Format(TextResolver.GetText("Leader Change Election"), text, character2.Name);
                            SendEventMessageToEmpire(EventMessageType.LeaderChange, text7, message6, character2, Capital);
                            break;
                        }
                    default:
                        {
                            string text6 = TextResolver.GetText("Leader New Title");
                            string message5 = string.Format(TextResolver.GetText("Leader New"), character2.Name);
                            SendEventMessageToEmpire(EventMessageType.LeaderChange, text6, message5, character2, Capital);
                            break;
                        }
                }
                return character2;
            }
            return null;
        }

        private void ProcessLeaderChangeInfluence(double timePassed)
        {
            double num = timePassed / ((double)Galaxy.RealSecondsInGalacticYear / 2.0);
            if (_LeaderChangeInfluence < 0.0)
            {
                if (PirateEmpireBaseHabitat == null && Colonies != null && Colonies.Count > 1)
                {
                    for (int i = 0; i < Colonies.Count; i++)
                    {
                        Habitat habitat = Colonies[i];
                        if (habitat != null && !habitat.Rebelling && Galaxy.Rnd.NextDouble() * 2.0 < Math.Abs(_LeaderChangeInfluence) && Galaxy.Rnd.Next(0, 2) == 1)
                        {
                            habitat.HappinessModifier = (float)(-15.0 + Galaxy.Rnd.NextDouble() * -15.0);
                            habitat.StartRebelling();
                            string description = string.Format(TextResolver.GetText("Leader Change Disruption Colony Rebel"), habitat.Name);
                            SendMessageToEmpire(this, EmpireMessageType.ColonyRebelling, habitat, description);
                        }
                    }
                }
                long num2 = LastLeaderChangeDate + (long)((double)Galaxy.RealSecondsInGalacticYear * 1000.0 * 0.5);
                if (_Galaxy.CurrentStarDate > num2 && Galaxy.Rnd.NextDouble() * 10.0 < Math.Abs(_LeaderChangeInfluence))
                {
                    ChangeLeader(null, -1);
                }
                _LeaderChangeInfluence += num;
                if (_LeaderChangeInfluence >= 0.0)
                {
                    _LeaderChangeInfluence = 0.0;
                }
            }
            else
            {
                if (!(_LeaderChangeInfluence > 0.0))
                {
                    return;
                }
                if (WarWearinessRaw > 0.0)
                {
                    double num3 = timePassed / (double)Galaxy.RealSecondsInGalacticYear * 15.0 * _LeaderChangeInfluence;
                    WarWearinessRaw -= num3;
                    if (WarWearinessRaw < 0.0)
                    {
                        WarWearinessRaw = 0.0;
                    }
                }
                _LeaderChangeInfluence -= num;
                if (_LeaderChangeInfluence <= 0.0)
                {
                    _LeaderChangeInfluence = 0.0;
                }
            }
        }

    }
}
