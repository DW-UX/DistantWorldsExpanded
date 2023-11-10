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
        public Habitat CheckEmpireBuildingVictoryWonder(Empire empire)
        {
            if (_Galaxy.GameRaceSpecificVictoryConditionsEnabled && empire != null && empire.Colonies != null)
            {
                for (int i = 0; i < empire.Colonies.Count; i++)
                {
                    Habitat habitat = empire.Colonies[i];
                    if (habitat.Facilities != null && habitat.Facilities.Count > 0)
                    {
                        PlanetaryFacility planetaryFacility = habitat.Facilities[habitat.Facilities.Count - 1];
                        if (planetaryFacility.ConstructionProgress < 1f && planetaryFacility.Type == PlanetaryFacilityType.Wonder && planetaryFacility.WonderType == WonderType.RaceAchievement)
                        {
                            return habitat;
                        }
                    }
                }
            }
            return null;
        }

        private bool EvaluateShouldAttackWonderBuildingEmpire(Empire empire, Habitat wonderBuildingColony, DiplomaticRelationType relationType, DiplomaticStrategy strategy)
        {
            if (empire != null && wonderBuildingColony != null)
            {
                bool flag = true;
                if (Reclusive)
                {
                    flag = false;
                    Empire empire2 = _Galaxy.IdentifyShakturiEmpire();
                    if (empire == empire2)
                    {
                        flag = true;
                    }
                }
                if (flag && strategy != DiplomaticStrategy.Ally && relationType != DiplomaticRelationType.FreeTradeAgreement && relationType != DiplomaticRelationType.Protectorate && relationType != DiplomaticRelationType.MutualDefensePact)
                {
                    Habitat habitat = _Galaxy.FastFindNearestColony(wonderBuildingColony.Xpos, wonderBuildingColony.Ypos, this, 0);
                    if (habitat != null)
                    {
                        double num = _Galaxy.CalculateDistance(wonderBuildingColony.Xpos, wonderBuildingColony.Ypos, habitat.Xpos, habitat.Ypos);
                        if (num < (double)Galaxy.SectorSize * 3.0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void ReviewDiplomaticStrategies()
        {
            int militaryPotency = MilitaryPotency;
            bool flag = false;
            int num = DominantRace.AggressionLevel - DominantRace.FriendlinessLevel;
            if (num > 10 && DominantRace.AggressionLevel >= 100)
            {
                flag = true;
            }
            Empire empire = IdentifyTopCompetitor();
            int num2 = DominantRace.TradeBonus + DominantRace.ResourceExtractionBonus + DominantRace.SatisfactionModifier;
            int num3 = DominantRace.AggressionLevel - 100 + (DominantRace.CautionLevel - 100) + DominantRace.WarWearinessAttenuation;
            int num4 = DominantRace.EspionageBonus * 2 + (DominantRace.IntelligenceLevel - 100);
            DiplomaticRelation[] array = DiplomaticRelations.ToArray();
            DiplomaticRelationList diplomaticRelationList = SummaryCopy(array);
            foreach (DiplomaticRelation diplomaticRelation in array)
            {
                Empire otherEmpire = diplomaticRelation.OtherEmpire;
                EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(otherEmpire);
                EmpireEvaluation empireEvaluation2 = otherEmpire.ObtainEmpireEvaluation(this);
                if (diplomaticRelation.Type != 0 && otherEmpire != null && otherEmpire.Active)
                {
                    double bias = empireEvaluation.Bias;
                    double num5 = 0.0;
                    if (_GovernmentAttributes != null)
                    {
                        num5 = _GovernmentAttributes.NaturalAffinity(otherEmpire.GovernmentId);
                    }
                    int overallAttitude = empireEvaluation2.OverallAttitude;
                    double incidentEvaluation = empireEvaluation.IncidentEvaluation;
                    int overallAttitude2 = empireEvaluation.OverallAttitude;
                    double num6 = bias / 2.0 + num5 * 0.5 + (double)overallAttitude / 5.0 + (double)overallAttitude2 / 8.0 + incidentEvaluation / 5.0;
                    int num7 = DetermineRelativeStrength(militaryPotency, otherEmpire);
                    double num8 = 12.0;
                    double num9 = -10.0;
                    double num10 = 18.0;
                    double num11 = -17.0;
                    if (Policy != null)
                    {
                        num8 /= Math.Sqrt(Policy.TradePriority);
                        num10 /= Math.Sqrt(Policy.AlliancePriority);
                        num11 /= Math.Sqrt(Policy.WarWillingness);
                    }
                    if (DominantRace != null)
                    {
                        double num12 = (double)DominantRace.AggressionLevel / 100.0;
                        _ = (double)DominantRace.CautionLevel / 100.0;
                        double num13 = (double)DominantRace.FriendlinessLevel / 100.0;
                        double num14 = (double)DominantRace.LoyaltyLevel / 100.0;
                        if (diplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement)
                        {
                            if (num14 > 1.0)
                            {
                                num8 /= num14;
                            }
                            if (Policy.BreakTreatyWillingness < 1.0)
                            {
                                num8 *= Math.Sqrt(Policy.BreakTreatyWillingness);
                            }
                        }
                        else if (diplomaticRelation.Type == DiplomaticRelationType.Protectorate || diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact)
                        {
                            if (num14 > 1.0)
                            {
                                num10 /= num14;
                            }
                            if (Policy.BreakTreatyWillingness < 1.0)
                            {
                                num10 *= Math.Sqrt(Policy.BreakTreatyWillingness);
                            }
                        }
                        else
                        {
                            num9 /= num12;
                            num11 /= num12;
                            num8 /= num13;
                            num10 /= num13;
                        }
                    }
                    DiplomaticStrategy diplomaticStrategy = DiplomaticStrategy.Undefined;
                    switch (num7)
                    {
                        case -1:
                            if (flag)
                            {
                                if (num6 > num8)
                                {
                                    diplomaticStrategy = ((!(num6 > num10)) ? DiplomaticStrategy.Befriend : DiplomaticStrategy.Ally);
                                }
                                else if (num6 < num9)
                                {
                                    if (num2 >= num4)
                                    {
                                        diplomaticStrategy = DiplomaticStrategy.DefendPlacate;
                                    }
                                    else if (num4 > num2)
                                    {
                                        diplomaticStrategy = DiplomaticStrategy.DefendUndermine;
                                    }
                                }
                                else
                                {
                                    diplomaticStrategy = DiplomaticStrategy.Undefined;
                                }
                            }
                            else
                            {
                                diplomaticStrategy = ((!(num6 > num8)) ? ((!(num6 < num9)) ? ((DominantRace.FriendlinessLevel > 110 || overallAttitude2 > 20) ? DiplomaticStrategy.Befriend : DiplomaticStrategy.Undefined) : ((num2 <= num3) ? DiplomaticStrategy.Defend : DiplomaticStrategy.DefendPlacate)) : ((!(num6 > num10)) ? DiplomaticStrategy.Befriend : DiplomaticStrategy.Ally));
                            }
                            break;
                        case 0:
                            if (flag)
                            {
                                if (num6 > num8)
                                {
                                    diplomaticStrategy = ((!(num6 > num10)) ? DiplomaticStrategy.Befriend : DiplomaticStrategy.Ally);
                                }
                                else if (num6 < num9)
                                {
                                    if ((num3 > num2 && num3 > num4) || DominantRace.AggressionLevel > 115 || overallAttitude2 < -5)
                                    {
                                        diplomaticStrategy = DiplomaticStrategy.Conquer;
                                    }
                                    else if (incidentEvaluation < -5.0 && DominantRace.FriendlinessLevel - DominantRace.AggressionLevel + overallAttitude2 < -10)
                                    {
                                        diplomaticStrategy = DiplomaticStrategy.Punish;
                                    }
                                    else if (num2 >= num4)
                                    {
                                        diplomaticStrategy = DiplomaticStrategy.DefendPlacate;
                                    }
                                    else if (num4 > num2)
                                    {
                                        diplomaticStrategy = DiplomaticStrategy.DefendUndermine;
                                    }
                                }
                                else
                                {
                                    diplomaticStrategy = (((DominantRace.FriendlinessLevel < 100 || DominantRace.EspionageBonus > 0) && overallAttitude2 < 0) ? DiplomaticStrategy.Undermine : DiplomaticStrategy.Undefined);
                                }
                            }
                            else if (num6 > num8)
                            {
                                diplomaticStrategy = ((!(num6 > num10)) ? DiplomaticStrategy.Befriend : DiplomaticStrategy.Ally);
                            }
                            else if (num6 < num9)
                            {
                                if ((num3 > num2 && num3 > num4) || DominantRace.AggressionLevel > 115 || overallAttitude2 < -10)
                                {
                                    diplomaticStrategy = DiplomaticStrategy.Conquer;
                                }
                                else if (num2 >= num4)
                                {
                                    diplomaticStrategy = DiplomaticStrategy.DefendPlacate;
                                }
                                else if (num4 > num2)
                                {
                                    diplomaticStrategy = DiplomaticStrategy.DefendUndermine;
                                }
                            }
                            else
                            {
                                diplomaticStrategy = ((DominantRace.FriendlinessLevel > 110 || overallAttitude2 > 20) ? DiplomaticStrategy.Befriend : DiplomaticStrategy.Undefined);
                            }
                            break;
                        case 1:
                            diplomaticStrategy = ((!flag) ? ((!(num6 > num8)) ? ((!(num6 < num9)) ? ((DominantRace.FriendlinessLevel > 110 || overallAttitude2 > 20) ? DiplomaticStrategy.Befriend : DiplomaticStrategy.Undefined) : ((DominantRace.AggressionLevel > 115 || overallAttitude2 < -5) ? DiplomaticStrategy.Conquer : ((!(incidentEvaluation < 0.0) || DominantRace.FriendlinessLevel - DominantRace.AggressionLevel + overallAttitude2 >= -10) ? DiplomaticStrategy.Defend : DiplomaticStrategy.Punish))) : ((!(num6 > num10)) ? DiplomaticStrategy.Befriend : DiplomaticStrategy.Ally)) : ((!(num6 > num8)) ? ((!(num6 < num9)) ? ((DominantRace.AggressionLevel > 115 && overallAttitude2 < -5) ? DiplomaticStrategy.Conquer : ((otherEmpire == empire && overallAttitude2 < -5) ? DiplomaticStrategy.Undermine : DiplomaticStrategy.Undefined)) : ((DominantRace.AggressionLevel > 110 || overallAttitude2 < 0) ? DiplomaticStrategy.Conquer : ((!(incidentEvaluation < -5.0) || DominantRace.FriendlinessLevel - DominantRace.AggressionLevel + overallAttitude2 >= -10) ? DiplomaticStrategy.Defend : DiplomaticStrategy.Punish))) : ((!(num6 > num10)) ? DiplomaticStrategy.Befriend : DiplomaticStrategy.Ally)));
                            break;
                    }
                    if (Reclusive)
                    {
                        switch (diplomaticStrategy)
                        {
                            case DiplomaticStrategy.Conquer:
                            case DiplomaticStrategy.Befriend:
                            case DiplomaticStrategy.Placate:
                            case DiplomaticStrategy.Ally:
                            case DiplomaticStrategy.Undermine:
                            case DiplomaticStrategy.Punish:
                                diplomaticStrategy = DiplomaticStrategy.Undefined;
                                break;
                            case DiplomaticStrategy.DefendPlacate:
                            case DiplomaticStrategy.DefendUndermine:
                                diplomaticStrategy = DiplomaticStrategy.Defend;
                                break;
                        }
                    }
                    switch (diplomaticStrategy)
                    {
                        case DiplomaticStrategy.Ally:
                            if (overallAttitude2 < 15)
                            {
                                diplomaticStrategy = DiplomaticStrategy.Befriend;
                            }
                            if (overallAttitude2 < 5)
                            {
                                diplomaticStrategy = DiplomaticStrategy.Undefined;
                            }
                            break;
                        case DiplomaticStrategy.Befriend:
                            if (overallAttitude2 < 5)
                            {
                                diplomaticStrategy = DiplomaticStrategy.Undefined;
                            }
                            break;
                    }
                    if (diplomaticStrategy == DiplomaticStrategy.Conquer)
                    {
                        if (!CheckDesireToConquer(diplomaticRelation.OtherEmpire))
                        {
                            diplomaticStrategy = DiplomaticStrategy.Undefined;
                        }
                    }
                    else if (CheckMustConquer(diplomaticRelation.OtherEmpire))
                    {
                        diplomaticStrategy = DiplomaticStrategy.Conquer;
                    }
                    if (diplomaticStrategy == DiplomaticStrategy.Punish && num6 >= num11 && !CheckDesireToConquer(diplomaticRelation.OtherEmpire))
                    {
                        diplomaticStrategy = DiplomaticStrategy.Undermine;
                    }
                    diplomaticRelation.SortTag = num6;
                    diplomaticRelation.Strategy = diplomaticStrategy;
                    Habitat habitat = CheckEmpireBuildingVictoryWonderAtKnownColony(diplomaticRelation.OtherEmpire);
                    if (habitat != null && EvaluateShouldAttackWonderBuildingEmpire(diplomaticRelation.OtherEmpire, habitat, diplomaticRelation.Type, diplomaticRelation.Strategy))
                    {
                        diplomaticRelation.Strategy = DiplomaticStrategy.Conquer;
                    }
                    if (diplomaticRelation.Type == DiplomaticRelationType.War && diplomaticRelation.Locked)
                    {
                        diplomaticRelation.Strategy = DiplomaticStrategy.Conquer;
                    }
                }
                if (diplomaticRelation.Locked)
                {
                    switch (diplomaticRelation.Type)
                    {
                        case DiplomaticRelationType.None:
                            diplomaticRelation.Strategy = DiplomaticStrategy.Undefined;
                            break;
                        case DiplomaticRelationType.FreeTradeAgreement:
                            diplomaticRelation.Strategy = DiplomaticStrategy.Befriend;
                            break;
                        case DiplomaticRelationType.Protectorate:
                            diplomaticRelation.Strategy = DiplomaticStrategy.Ally;
                            break;
                        case DiplomaticRelationType.MutualDefensePact:
                            diplomaticRelation.Strategy = DiplomaticStrategy.Ally;
                            break;
                        case DiplomaticRelationType.SubjugatedDominion:
                            diplomaticRelation.Strategy = DiplomaticStrategy.Undefined;
                            break;
                        case DiplomaticRelationType.TradeSanctions:
                            diplomaticRelation.Strategy = DiplomaticStrategy.Punish;
                            break;
                        case DiplomaticRelationType.War:
                            diplomaticRelation.Strategy = DiplomaticStrategy.Conquer;
                            break;
                    }
                }
            }
            int num15 = 0;
            int num16 = 0;
            int num17 = 0;
            double num18 = 25.0 * ((double)DominantRace.AggressionLevel / 100.0);
            DiplomaticRelationList diplomaticRelationList2 = new DiplomaticRelationList();
            DiplomaticRelationList diplomaticRelationList3 = new DiplomaticRelationList();
            foreach (DiplomaticRelation diplomaticRelation2 in array)
            {
                if (diplomaticRelation2.Type != 0)
                {
                    diplomaticRelationList2.Add(diplomaticRelation2);
                    if (diplomaticRelation2.Type == DiplomaticRelationType.War)
                    {
                        num17++;
                    }
                    else if (diplomaticRelation2.Strategy == DiplomaticStrategy.Conquer)
                    {
                        diplomaticRelationList3.Add(diplomaticRelation2);
                        num15++;
                    }
                    else if (diplomaticRelation2.Strategy == DiplomaticStrategy.Befriend || diplomaticRelation2.Strategy == DiplomaticStrategy.Ally)
                    {
                        num16++;
                    }
                }
            }
            int num19 = 1;
            if ((num17 > 0 && num15 > 0) || WarWeariness >= num18 || ShipGroups.Count <= 0)
            {
                num19 = 0;
            }
            if (num15 > num19)
            {
                diplomaticRelationList3.Sort();
                for (int k = 0; k < diplomaticRelationList3.Count; k++)
                {
                    if (k >= num19)
                    {
                        int num20 = DetermineRelativeStrength(militaryPotency, diplomaticRelationList3[k].OtherEmpire);
                        if (num20 == 1)
                        {
                            diplomaticRelationList3[k].Strategy = DiplomaticStrategy.Undermine;
                        }
                        else if (num4 > num3 || DominantRace.EspionageBonus > 0)
                        {
                            diplomaticRelationList3[k].Strategy = DiplomaticStrategy.DefendUndermine;
                        }
                        else
                        {
                            diplomaticRelationList3[k].Strategy = DiplomaticStrategy.Defend;
                        }
                    }
                }
            }
            int num21 = 0;
            int num22 = 0;
            DiplomaticRelationList diplomaticRelationList4 = new DiplomaticRelationList();
            foreach (DiplomaticRelation diplomaticRelation3 in array)
            {
                if (diplomaticRelation3.Type != 0)
                {
                    if (diplomaticRelation3.Type == DiplomaticRelationType.TradeSanctions)
                    {
                        num22++;
                    }
                    else if (diplomaticRelation3.Strategy == DiplomaticStrategy.Punish)
                    {
                        diplomaticRelationList4.Add(diplomaticRelation3);
                        num21++;
                    }
                }
            }
            int num23 = 1;
            if (num22 > 0 && num21 > 0)
            {
                num23 = 0;
            }
            if (num21 > num23)
            {
                diplomaticRelationList4.Sort();
                for (int m = 0; m < diplomaticRelationList4.Count; m++)
                {
                    if (m >= num23)
                    {
                        int num24 = DetermineRelativeStrength(militaryPotency, diplomaticRelationList4[m].OtherEmpire);
                        if (num24 == 1)
                        {
                            diplomaticRelationList4[m].Strategy = DiplomaticStrategy.Undermine;
                        }
                        else
                        {
                            diplomaticRelationList4[m].Strategy = DiplomaticStrategy.Undefined;
                        }
                    }
                }
            }
            if (!Reclusive && num16 <= 0 && diplomaticRelationList2.Count > 1)
            {
                diplomaticRelationList2.Sort();
                diplomaticRelationList2.Reverse();
                if (diplomaticRelationList2[0].SortTag > 2.0 && diplomaticRelationList2[0].Type != DiplomaticRelationType.War && diplomaticRelationList2[0].Type != DiplomaticRelationType.TradeSanctions && diplomaticRelationList2[0].Type != 0)
                {
                    EmpireEvaluation empireEvaluation3 = ObtainEmpireEvaluation(diplomaticRelationList2[0].OtherEmpire);
                    if (empireEvaluation3.OverallAttitude >= 5)
                    {
                        diplomaticRelationList2[0].Strategy = DiplomaticStrategy.Befriend;
                    }
                }
            }
            foreach (DiplomaticRelation diplomaticRelation4 in array)
            {
                DiplomaticRelation diplomaticRelation5 = diplomaticRelationList[diplomaticRelation4.OtherEmpire];
                if (diplomaticRelation4.Strategy == DiplomaticStrategy.Conquer)
                {
                    SetWarObjectives(diplomaticRelation4);
                }
                if (diplomaticRelation5.Strategy == DiplomaticStrategy.Conquer && diplomaticRelation4.Strategy != DiplomaticStrategy.Conquer)
                {
                    ClearAttackFleetAssignments(diplomaticRelation4.OtherEmpire);
                }
                else if (diplomaticRelation4.Strategy == DiplomaticStrategy.Conquer && diplomaticRelation5.Strategy != DiplomaticStrategy.Conquer)
                {
                    if (CheckCanConductNewWar(diplomaticRelation4.OtherEmpire, clearColonyTargetsIfNecessary: true))
                    {
                        SetDefendFleets();
                        int num25 = PrepareFleetsForWar(diplomaticRelation4.OtherEmpire);
                        if (num25 > 0)
                        {
                            if (_ControlMilitaryAttacks != 0 && Policy.UseExplorationShipsToScoutEnemySystems)
                            {
                                EmpireList empireList = new EmpireList();
                                empireList.Add(diplomaticRelation4.OtherEmpire);
                                SendScoutShipsToEnemyLocations(empireList);
                            }
                        }
                        else
                        {
                            diplomaticRelation4.Strategy = DiplomaticStrategy.DefendUndermine;
                        }
                    }
                    else
                    {
                        diplomaticRelation4.Strategy = DiplomaticStrategy.DefendUndermine;
                    }
                }
                else if ((diplomaticRelation4.Strategy == DiplomaticStrategy.Defend || diplomaticRelation4.Strategy == DiplomaticStrategy.DefendPlacate || diplomaticRelation4.Strategy == DiplomaticStrategy.DefendUndermine) && diplomaticRelation5.Strategy != DiplomaticStrategy.Defend && diplomaticRelation5.Strategy != DiplomaticStrategy.DefendPlacate && diplomaticRelation5.Strategy != DiplomaticStrategy.DefendUndermine)
                {
                    SetDefendFleets();
                }
            }
            EmpireList targetEmpires = DetermineEmpiresWarOrConquer();
            CheckAttackFleetTargets(targetEmpires);
        }

        private bool CheckCanConductNewWar(Empire otherEmpire, bool clearColonyTargetsIfNecessary)
        {
            EmpireList empireList = DetermineEmpiresAtWarWith();
            if (empireList.Count > 0)
            {
                return false;
            }
            if (ShipGroups.Count <= 0)
            {
                return false;
            }
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            if (diplomaticRelation.WarObjective == WarObjective.CaptureObjectives && diplomaticRelation.WarObjectiveColonies.Count > 0)
            {
                bool flag = false;
                bool flag2 = false;
                for (int i = 0; i < ShipGroups.Count; i++)
                {
                    ShipGroup shipGroup = ShipGroups[i];
                    if (shipGroup.TotalTroopAttackStrength > 0)
                    {
                        flag = true;
                    }
                    if (shipGroup.Ships.Count >= 10)
                    {
                        flag2 = true;
                    }
                }
                if (!flag || !flag2)
                {
                    if (clearColonyTargetsIfNecessary)
                    {
                        if (diplomaticRelation.WarObjectiveBases.Count > 0 && ShipGroups.Count > 0)
                        {
                            diplomaticRelation.WarObjectiveColonies.Clear();
                        }
                        return true;
                    }
                    return false;
                }
                return true;
            }
            return true;
        }

        private bool CheckMustConquer(Empire otherEmpire)
        {
            EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(otherEmpire);
            double num = -80.0;
            num /= Math.Sqrt(Policy.WarWillingness);
            if (empireEvaluation.IncidentEvaluation < num)
            {
                return true;
            }
            return false;
        }

        private bool CheckDesireToConquer(Empire otherEmpire)
        {
            HabitatList targetedColonies = new HabitatList();
            BuiltObjectList targetedBases = new BuiltObjectList();
            IdentifyEmpireWarObjectives(otherEmpire, out targetedColonies, out targetedBases);
            if (targetedColonies.Count > 0 || targetedBases.Count > 0)
            {
                return true;
            }
            EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(otherEmpire);
            double num = -50.0;
            num /= Math.Sqrt(Policy.WarWillingness);
            if (empireEvaluation.IncidentEvaluation < num)
            {
                return true;
            }
            return false;
        }

        private bool CheckOfferMilitaryRefueling(DiplomaticRelation relation)
        {
            if (!relation.MilitaryRefuelingToOther && relation.Strategy == DiplomaticStrategy.Ally)
            {
                EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(relation.OtherEmpire);
                if (empireEvaluation.IncidentEvaluation >= 10.0 && CheckTaskAuthorized(_ControlDiplomacyTreaties, GenerateAutomationMessageMilitaryRefueling(relation.OtherEmpire, refuel: true), relation.OtherEmpire, AdvisorMessageType.OfferMilitaryRefueling))
                {
                    OfferMilitaryRefueling(relation.OtherEmpire);
                    return true;
                }
            }
            return false;
        }

        private bool CheckOfferMiningRights(DiplomaticRelation relation)
        {
            if (!relation.MiningRightsToOther && (relation.Strategy == DiplomaticStrategy.Ally || relation.Strategy == DiplomaticStrategy.Befriend))
            {
                EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(relation.OtherEmpire);
                if (empireEvaluation.IncidentEvaluation >= 5.0 && CheckTaskAuthorized(_ControlDiplomacyTreaties, GenerateAutomationMessageMiningRights(relation.OtherEmpire, allowMining: true), relation.OtherEmpire, AdvisorMessageType.OfferMiningRights))
                {
                    OfferMiningRights(relation.OtherEmpire);
                    return true;
                }
            }
            return false;
        }

        private void ImplementDiplomaticStrategy(Empire otherEmpire, DiplomaticStrategy strategy)
        {
            if (otherEmpire == null)
            {
                return;
            }
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(otherEmpire);
            EmpireEvaluation empireEvaluation2 = otherEmpire.ObtainEmpireEvaluation(this);
            if (empireEvaluation.IncidentEvaluation < -5.0)
            {
                CheckCancelMilitaryRefueling(otherEmpire);
            }
            if (empireEvaluation.IncidentEvaluation < -10.0)
            {
                CheckCancelMiningRights(otherEmpire);
            }
            switch (strategy)
            {
                case DiplomaticStrategy.Ally:
                    if (diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || diplomaticRelation.Type == DiplomaticRelationType.Protectorate)
                    {
                        if (Galaxy.Rnd.Next(0, 2) == 1)
                        {
                            if (empireEvaluation2.OverallAttitude < 50)
                            {
                                GiveGiftSmallWhenSufficientTimePassed(otherEmpire);
                            }
                        }
                        else if (Galaxy.Rnd.Next(0, 2) == 1)
                        {
                            OfferTrade(otherEmpire);
                        }
                    }
                    else if (Galaxy.Rnd.Next(0, 2) == 1)
                    {
                        if (empireEvaluation2.OverallAttitude < 50)
                        {
                            GiveGiftWhenSufficientTimePassed(otherEmpire);
                        }
                    }
                    else
                    {
                        OfferMutualDefense(otherEmpire);
                    }
                    if (Galaxy.Rnd.Next(0, 2) == 1)
                    {
                        CheckOfferMilitaryRefueling(diplomaticRelation);
                    }
                    else
                    {
                        CheckOfferMiningRights(diplomaticRelation);
                    }
                    break;
                case DiplomaticStrategy.Befriend:
                    if (diplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement || diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || diplomaticRelation.Type == DiplomaticRelationType.Protectorate)
                    {
                        if (Galaxy.Rnd.Next(0, 2) == 1 && empireEvaluation2.OverallAttitude < 25)
                        {
                            GiveGiftSmallWhenSufficientTimePassed(otherEmpire);
                        }
                    }
                    else if (Galaxy.Rnd.Next(0, 2) == 1)
                    {
                        if (empireEvaluation2.OverallAttitude < 25)
                        {
                            GiveGiftWhenSufficientTimePassed(otherEmpire);
                        }
                    }
                    else
                    {
                        OfferFreeTrade(otherEmpire);
                    }
                    CheckOfferMiningRights(diplomaticRelation);
                    break;
                case DiplomaticStrategy.Conquer:
                    if (diplomaticRelation.Type != DiplomaticRelationType.War && CheckReadyForWar(otherEmpire))
                    {
                        StartWar(otherEmpire);
                    }
                    CheckCancelMilitaryRefueling(otherEmpire);
                    CheckCancelMiningRights(otherEmpire);
                    CheckCancelRestrictedResourceTrading(otherEmpire);
                    break;
                case DiplomaticStrategy.Defend:
                    CheckCancelMilitaryRefueling(otherEmpire);
                    break;
                case DiplomaticStrategy.DefendPlacate:
                    if (diplomaticRelation.Type != DiplomaticRelationType.War && Galaxy.Rnd.Next(0, 2) == 1 && empireEvaluation2.OverallAttitude < 0)
                    {
                        GiveGiftWhenSufficientTimePassed(otherEmpire);
                    }
                    break;
                case DiplomaticStrategy.DefendUndermine:
                    CheckCancelMilitaryRefueling(otherEmpire);
                    break;
                case DiplomaticStrategy.Placate:
                    if (diplomaticRelation.Type != DiplomaticRelationType.War && Galaxy.Rnd.Next(0, 2) == 1 && empireEvaluation2.OverallAttitude < 0)
                    {
                        GiveGiftWhenSufficientTimePassed(otherEmpire);
                    }
                    break;
                case DiplomaticStrategy.Punish:
                    if (diplomaticRelation.Type != DiplomaticRelationType.TradeSanctions && diplomaticRelation.Type != DiplomaticRelationType.War)
                    {
                        StartTradeSanctionsIfTimePassed(otherEmpire);
                    }
                    CancelTreatiesIfTimePassed(otherEmpire);
                    CheckCancelMilitaryRefueling(otherEmpire);
                    CheckCancelMiningRights(otherEmpire);
                    CheckCancelRestrictedResourceTrading(otherEmpire);
                    break;
                case DiplomaticStrategy.Undermine:
                    if (diplomaticRelation.Type != DiplomaticRelationType.TradeSanctions && diplomaticRelation.Type != DiplomaticRelationType.War)
                    {
                        StartTradeSanctionsIfTimePassed(otherEmpire);
                    }
                    CancelTreatiesIfTimePassed(otherEmpire);
                    CheckCancelMilitaryRefueling(otherEmpire);
                    CheckCancelMiningRights(otherEmpire);
                    CheckCancelRestrictedResourceTrading(otherEmpire);
                    break;
            }
        }

        private void OfferTrade(Empire otherEmpire)
        {
        }

        public DiplomaticRelationType DetermineDesiredDiplomaticRelationTypical(DiplomaticStrategy strategy, DiplomaticRelationType currentRelationType)
        {
            switch (strategy)
            {
                case DiplomaticStrategy.Ally:
                    return DiplomaticRelationType.MutualDefensePact;
                case DiplomaticStrategy.Befriend:
                    return DiplomaticRelationType.FreeTradeAgreement;
                case DiplomaticStrategy.Conquer:
                    return DiplomaticRelationType.War;
                case DiplomaticStrategy.Placate:
                case DiplomaticStrategy.Defend:
                case DiplomaticStrategy.DefendPlacate:
                    return DiplomaticRelationType.None;
                case DiplomaticStrategy.DefendUndermine:
                    if (currentRelationType == DiplomaticRelationType.TradeSanctions)
                    {
                        return DiplomaticRelationType.TradeSanctions;
                    }
                    return DiplomaticRelationType.None;
                case DiplomaticStrategy.Undermine:
                case DiplomaticStrategy.Punish:
                    return DiplomaticRelationType.TradeSanctions;
                default:
                    return DiplomaticRelationType.None;
            }
        }

        private void ReviewDiplomaticSituations()
        {
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation != null && diplomaticRelation.Type != 0)
                {
                    ImplementDiplomaticStrategy(diplomaticRelation.OtherEmpire, diplomaticRelation.Strategy);
                    ApplyDiplomaticStrategyToRelation(diplomaticRelation, diplomaticRelation.Strategy);
                }
            }
        }

        private void ApplyDiplomaticStrategyToRelation(DiplomaticRelation relation, DiplomaticStrategy strategy)
        {
            if (relation == null)
            {
                return;
            }
            DiplomaticRelationType diplomaticRelationType = DetermineDesiredDiplomaticRelationTypical(strategy, relation.Type);
            switch (relation.Type)
            {
                case DiplomaticRelationType.War:
                    {
                        if (relation.Initiator == this)
                        {
                            WarEndReason endReason = WarEndReason.Undefined;
                            if (!ConsiderEndWar(relation.OtherEmpire, out endReason))
                            {
                                break;
                            }
                            double num2 = (double)MilitaryPotency / (double)relation.OtherEmpire.MilitaryPotency;
                            switch (endReason)
                            {
                                case WarEndReason.AtWarWithOtherEmpires:
                                    if (num2 > 3.0 && Galaxy.Rnd.Next(0, 2) == 1)
                                    {
                                        SubjugateRequest(relation.OtherEmpire);
                                    }
                                    else
                                    {
                                        EndWarRequest(relation.OtherEmpire);
                                    }
                                    break;
                                case WarEndReason.WarWearinessExceeded:
                                case WarEndReason.HeavyLosses:
                                case WarEndReason.NoAttackFleets:
                                    if (DominantRace.AggressionLevel >= 115 && Galaxy.Rnd.Next(0, 3) == 1)
                                    {
                                        SubjugateRequest(relation.OtherEmpire);
                                    }
                                    else
                                    {
                                        EndWarRequest(relation.OtherEmpire);
                                    }
                                    break;
                                case WarEndReason.ObjectivesMet:
                                    if (num2 > 3.0 && Galaxy.Rnd.Next(0, 2) == 1)
                                    {
                                        SubjugateRequest(relation.OtherEmpire);
                                    }
                                    else
                                    {
                                        EndWarRequest(relation.OtherEmpire);
                                    }
                                    break;
                                case WarEndReason.WantEnd:
                                    EndWarRequest(relation.OtherEmpire);
                                    break;
                            }
                            break;
                        }
                        WarEndReason endReason2 = WarEndReason.Undefined;
                        if (ConsiderEndWar(relation.OtherEmpire, out endReason2))
                        {
                            double winningRatio = 0.0;
                            int loserRawDamageBuiltObject = 0;
                            int loserRawDamageColony = 0;
                            int winnerRawDamageBuiltObject = 0;
                            int winnerRawDamageColony = 0;
                            Empire loser = null;
                            Empire empire = DetermineVictorInWar(relation, out winningRatio, out loser, out loserRawDamageBuiltObject, out loserRawDamageColony, out winnerRawDamageBuiltObject, out winnerRawDamageColony);
                            if (empire == this && DetermineWhetherWantToOfferSubjugation(this) && DetermineSubjugationOfLoserInWar(empire, loser, winningRatio, empire.MilitaryPotency, loser.MilitaryPotency))
                            {
                                SubjugateRequest(relation.OtherEmpire);
                            }
                            else
                            {
                                EndWarRequest(relation.OtherEmpire);
                            }
                        }
                        break;
                    }
                case DiplomaticRelationType.TradeSanctions:
                    if (relation.Initiator == this && diplomaticRelationType != DiplomaticRelationType.TradeSanctions && diplomaticRelationType != DiplomaticRelationType.War)
                    {
                        EndTradeSanctionsIfTimePassed(relation.OtherEmpire);
                    }
                    break;
                case DiplomaticRelationType.SubjugatedDominion:
                    if (relation.Initiator == this)
                    {
                        if (diplomaticRelationType == DiplomaticRelationType.FreeTradeAgreement || diplomaticRelationType == DiplomaticRelationType.MutualDefensePact || diplomaticRelationType == DiplomaticRelationType.Protectorate)
                        {
                            EndSubjugation(relation.OtherEmpire);
                        }
                    }
                    else
                    {
                        if (_ControlDiplomacyTreaties == AutomationLevel.Manual)
                        {
                            break;
                        }
                        Empire otherEmpire = relation.OtherEmpire;
                        int num = DetermineRelativeStrength(MilitaryPotency, otherEmpire);
                        DiplomaticRelation diplomaticRelation = new DiplomaticRelation(DiplomaticRelationType.None, this, this, otherEmpire, _Galaxy.CurrentStarDate, relation.SupplyRestrictedResources);
                        int refusalCount = 0;
                        switch (num)
                        {
                            case -1:
                                if (Galaxy.Rnd.Next(0, 8) == 2 && CheckTaskAuthorized(_ControlDiplomacyTreaties, ref refusalCount, GenerateAutomationMessageTreaty(otherEmpire, DiplomaticRelationType.None), otherEmpire, AdvisorMessageType.TreatyOffer, DiplomaticRelationType.None, null))
                                {
                                    otherEmpire.ProposedDiplomaticRelations.Add(diplomaticRelation);
                                    string description2 = GenerateMessageDescription(relation, DiplomaticRelationType.None, num);
                                    relation.LastDiplomacyTradeOfferDate = _Galaxy.CurrentStarDate;
                                    SendMessageToEmpire(otherEmpire, EmpireMessageType.ProposeDiplomaticRelation, DiplomaticRelationType.None, description2);
                                }
                                break;
                            case 0:
                                if (Galaxy.Rnd.Next(0, 3) == 2 && CheckTaskAuthorized(_ControlDiplomacyTreaties, ref refusalCount, GenerateAutomationMessageTreaty(otherEmpire, DiplomaticRelationType.None), otherEmpire, AdvisorMessageType.TreatyOffer, DiplomaticRelationType.None, null))
                                {
                                    otherEmpire.ProposedDiplomaticRelations.Add(diplomaticRelation);
                                    string description3 = GenerateMessageDescription(relation, DiplomaticRelationType.None, num);
                                    relation.LastDiplomacyTradeOfferDate = _Galaxy.CurrentStarDate;
                                    SendMessageToEmpire(otherEmpire, EmpireMessageType.ProposeDiplomaticRelation, DiplomaticRelationType.None, description3);
                                }
                                break;
                            case 1:
                                if (CheckTaskAuthorized(_ControlDiplomacyTreaties, ref refusalCount, GenerateAutomationMessageTreaty(otherEmpire, DiplomaticRelationType.None), otherEmpire, AdvisorMessageType.TreatyOffer, DiplomaticRelationType.None, null))
                                {
                                    otherEmpire.ProposedDiplomaticRelations.Add(diplomaticRelation);
                                    string description = GenerateMessageDescription(relation, DiplomaticRelationType.None, num);
                                    relation.LastDiplomacyTradeOfferDate = _Galaxy.CurrentStarDate;
                                    SendMessageToEmpire(otherEmpire, EmpireMessageType.ProposeDiplomaticRelation, DiplomaticRelationType.None, description);
                                }
                                break;
                        }
                    }
                    break;
                case DiplomaticRelationType.None:
                case DiplomaticRelationType.FreeTradeAgreement:
                case DiplomaticRelationType.MutualDefensePact:
                case DiplomaticRelationType.Protectorate:
                    break;
            }
        }

        public bool ConsiderEndWar(Empire otherEmpire, out WarEndReason endReason)
        {
            return ConsiderEndWar(otherEmpire, out endReason, otherEmpireOfferingToBeSubjugated: false);
        }

        public bool ConsiderEndWar(Empire otherEmpire, out WarEndReason endReason, bool otherEmpireOfferingToBeSubjugated)
        {
            bool flag = true;
            endReason = WarEndReason.Undefined;
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            if (diplomaticRelation != null && diplomaticRelation.Type == DiplomaticRelationType.War)
            {
                if (diplomaticRelation.Locked)
                {
                    return false;
                }
                switch (diplomaticRelation.WarObjective)
                {
                    case WarObjective.CaptureObjectives:
                        {
                            for (int i = 0; i < diplomaticRelation.WarObjectiveColonies.Count; i++)
                            {
                                Habitat habitat = diplomaticRelation.WarObjectiveColonies[i];
                                if (habitat != null && !habitat.HasBeenDestroyed && habitat.Empire != null && habitat.Empire != _Galaxy.IndependentEmpire && habitat.Empire != this && habitat.Empire == otherEmpire)
                                {
                                    flag = false;
                                }
                            }
                            for (int j = 0; j < diplomaticRelation.WarObjectiveBases.Count; j++)
                            {
                                BuiltObject builtObject = diplomaticRelation.WarObjectiveBases[j];
                                if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Empire != null && builtObject.Empire != _Galaxy.IndependentEmpire && builtObject.Empire != this && builtObject.Empire == otherEmpire)
                                {
                                    flag = false;
                                }
                            }
                            endReason = WarEndReason.ObjectivesMet;
                            break;
                        }
                    case WarObjective.EndWar:
                        flag = true;
                        endReason = WarEndReason.WantEnd;
                        break;
                    case WarObjective.TotalConquest:
                        if (otherEmpire.Colonies.Count > 0 && otherEmpire.Active)
                        {
                            flag = false;
                        }
                        break;
                    default:
                        flag = true;
                        break;
                }
                double num = (double)DominantRace.AggressionLevel / 100.0;
                double num2 = 25.0 * num;
                if (otherEmpireOfferingToBeSubjugated)
                {
                    num2 *= 0.6;
                }
                if (WarWeariness > num2)
                {
                    flag = true;
                    endReason = WarEndReason.WarWearinessExceeded;
                }
                EmpireList empireList = DetermineEmpiresAtWarWith();
                if (empireList.Count > 1)
                {
                    int num3 = 0;
                    for (int k = 0; k < empireList.Count; k++)
                    {
                        num3 += empireList[k].BuiltObjects.TotalMobileMilitaryFirepower();
                    }
                    double num4 = (double)BuiltObjects.TotalMobileMilitaryFirepower() / (double)num3;
                    double num5 = 1.0;
                    if (otherEmpireOfferingToBeSubjugated)
                    {
                        num5 = 2.0;
                    }
                    if (num4 < num5)
                    {
                        flag = true;
                        endReason = WarEndReason.AtWarWithOtherEmpires;
                    }
                }
                if (CheckWhetherWarDamageExceedsLimit(this, diplomaticRelation.WarDamageBuiltObject, diplomaticRelation.WarDamageColony))
                {
                    flag = true;
                    endReason = WarEndReason.HeavyLosses;
                }
                int num6 = 0;
                for (int l = 0; l < ShipGroups.Count; l++)
                {
                    ShipGroup shipGroup = ShipGroups[l];
                    if (shipGroup != null && shipGroup.Posture == FleetPosture.Attack && shipGroup.Ships.Count > 0)
                    {
                        num6++;
                    }
                }
                if (num6 <= 0)
                {
                    flag = true;
                    endReason = WarEndReason.NoAttackFleets;
                }
                if (flag)
                {
                    long num7 = diplomaticRelation.StartDateOfLastChange + (long)(Galaxy.MinimumWarLengthPeriodYears * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
                    if (_Galaxy.CurrentStarDate < num7)
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        public int PrepareFleetsForWar(Empire otherEmpire)
        {
            int num = 0;
            if (_ControlMilitaryFleets)
            {
                DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
                if (diplomaticRelation.WarObjective == WarObjective.TotalConquest)
                {
                    IdentifyMilitaryObjectives();
                }
                else if (diplomaticRelation.WarObjective == WarObjective.CaptureObjectives)
                {
                    for (int i = 0; i < diplomaticRelation.WarObjectiveColonies.Count; i++)
                    {
                        Habitat habitat = diplomaticRelation.WarObjectiveColonies[i];
                        if (habitat == null)
                        {
                            continue;
                        }
                        int num2 = habitat.EstimatedDefensiveForceRequired(atWar: true);
                        int num3 = Galaxy.DetermineRequiredTroopStrength(this, habitat);
                        if (CheckSystemVisible(habitat.SystemIndex))
                        {
                            num2 = _Galaxy.DetermineDefendingStrength(habitat, otherEmpire);
                        }
                        ShipGroupList shipGroupList = GenerateOrderedFleetsForTarget(habitat.Xpos, habitat.Ypos, includeSmallFleets: false);
                        if (shipGroupList.Count <= 0)
                        {
                            continue;
                        }
                        int num4 = 0;
                        int num5 = 0;
                        for (int j = 0; j < shipGroupList.Count; j++)
                        {
                            ShipGroup shipGroup = shipGroupList[j];
                            if (shipGroup != null && shipGroup.LeadShip != null && shipGroup.LeadShip.IsAutoControlled && shipGroup.Ships.Count >= 10 && shipGroup.Posture == FleetPosture.Attack && shipGroup.AttackPoint == null && shipGroup.TotalTroopAttackStrength >= num3 / 2)
                            {
                                bool flag = false;
                                if (shipGroup.Mission == null || shipGroup.Mission.Type == BuiltObjectMissionType.Undefined || shipGroup.Mission.Priority == BuiltObjectMissionPriority.Low)
                                {
                                    flag = true;
                                }
                                ResourceList requiredFuel = DetermineFuelRequiredForFleet(shipGroup);
                                StellarObject stellarObject = DecideBestFleetRefuelPoint(habitat.Xpos, habitat.Ypos, this, requiredFuel, otherEmpire);
                                if (stellarObject != null && shipGroup.CheckFleetTargetWithinFuelRangeAndRefuel(stellarObject.Xpos, stellarObject.Ypos, 0.0))
                                {
                                    double num6 = shipGroup.MaximumRange();
                                    double num7 = _Galaxy.CalculateDistance(stellarObject.Xpos, stellarObject.Ypos, habitat.Xpos, habitat.Ypos);
                                    if (num7 < num6 * 0.45)
                                    {
                                        shipGroup.GatherPoint = stellarObject;
                                        shipGroup.AttackPoint = habitat;
                                        shipGroup.PostureRangeSquared = 2304000000.0;
                                        if (flag && stellarObject != null)
                                        {
                                            shipGroup.AssignMission(BuiltObjectMissionType.Refuel, stellarObject, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                                        }
                                        num4 += shipGroup.TotalOverallStrengthFactor;
                                        num5 += shipGroup.TotalTroopAttackStrength;
                                        num++;
                                    }
                                }
                            }
                            if (num4 >= num2 && num5 >= num3)
                            {
                                break;
                            }
                        }
                    }
                    for (int k = 0; k < diplomaticRelation.WarObjectiveBases.Count; k++)
                    {
                        BuiltObject builtObject = diplomaticRelation.WarObjectiveBases[k];
                        if (builtObject == null)
                        {
                            continue;
                        }
                        int val = builtObject.CalculateOverallStrengthFactor();
                        if (IsObjectVisibleToThisEmpire(builtObject))
                        {
                            val = _Galaxy.DetermineDefendingStrength(builtObject, otherEmpire);
                        }
                        val = Math.Max(1, val);
                        int num8 = 0;
                        ShipGroupList shipGroupList2 = GenerateOrderedFleetsForTarget(builtObject.Xpos, builtObject.Ypos, includeSmallFleets: true);
                        if (shipGroupList2.Count <= 0)
                        {
                            continue;
                        }
                        int num9 = 0;
                        int num10 = 0;
                        for (int l = 0; l < shipGroupList2.Count; l++)
                        {
                            ShipGroup shipGroup2 = shipGroupList2[l];
                            if (shipGroup2 != null && shipGroup2.LeadShip != null && shipGroup2.LeadShip.IsAutoControlled && shipGroup2.Posture == FleetPosture.Attack && shipGroup2.AttackPoint == null)
                            {
                                bool flag2 = false;
                                if (shipGroup2.Mission == null || shipGroup2.Mission.Type == BuiltObjectMissionType.Undefined || shipGroup2.Mission.Priority == BuiltObjectMissionPriority.Low)
                                {
                                    flag2 = true;
                                }
                                ResourceList requiredFuel2 = DetermineFuelRequiredForFleet(shipGroup2);
                                StellarObject stellarObject2 = DecideBestFleetRefuelPoint(builtObject.Xpos, builtObject.Ypos, this, requiredFuel2, otherEmpire);
                                if (stellarObject2 != null && shipGroup2.CheckFleetTargetWithinFuelRangeAndRefuel(stellarObject2.Xpos, stellarObject2.Ypos, 0.0))
                                {
                                    double num11 = shipGroup2.MaximumRange();
                                    double num12 = _Galaxy.CalculateDistance(stellarObject2.Xpos, stellarObject2.Ypos, builtObject.Xpos, builtObject.Ypos);
                                    if (num12 < num11 * 0.45)
                                    {
                                        shipGroup2.GatherPoint = stellarObject2;
                                        shipGroup2.AttackPoint = builtObject;
                                        shipGroup2.PostureRangeSquared = 2304000000.0;
                                        if (flag2 && stellarObject2 != null)
                                        {
                                            shipGroup2.AssignMission(BuiltObjectMissionType.Refuel, stellarObject2, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                                        }
                                        num9 += shipGroup2.TotalOverallStrengthFactor;
                                        num10 += shipGroup2.TotalTroopAttackStrength;
                                        num++;
                                    }
                                }
                            }
                            if (num9 >= val && num10 >= num8)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return num;
        }

        public StellarObject SelectFleetWarAttackTarget(ShipGroup fleet, Empire otherEmpire, out bool waypointing)
        {
            bool attackPointClearedForReassignment = false;
            return SelectFleetWarAttackTarget(fleet, otherEmpire, out waypointing, out attackPointClearedForReassignment);
        }

        public StellarObject SelectFleetWarAttackTarget(ShipGroup fleet, Empire otherEmpire, out bool waypointing, out bool attackPointClearedForReassignment)
        {
            waypointing = false;
            attackPointClearedForReassignment = false;
            if (fleet.LeadShip != null && fleet.LeadShip.IsAutoControlled && fleet.Posture == FleetPosture.Attack)
            {
                DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
                if (diplomaticRelation != null && diplomaticRelation.WarObjective == WarObjective.CaptureObjectives)
                {
                    if (fleet.AttackPoint == null)
                    {
                        if ((fleet.ShipTargetAmount >= 10 || fleet.Ships.Count >= 10) && fleet.TotalTroopAttackStrength > 0)
                        {
                            Habitat[] array = _Galaxy.SortHabitatsByDistanceThreadsafe(fleet.LeadShip.Xpos, fleet.LeadShip.Ypos, diplomaticRelation.WarObjectiveColonies);
                            foreach (Habitat habitat in array)
                            {
                                if (!CheckWarObjectiveStillValid(habitat, otherEmpire))
                                {
                                    continue;
                                }
                                bool fleetAlreadyAssigned = false;
                                if (!CheckTargetRequiresMoreAttackFleets(habitat, otherEmpire, out fleetAlreadyAssigned) || !CheckFleetCanAttackTarget(fleet, habitat, fleetAlreadyAssigned))
                                {
                                    continue;
                                }
                                if (fleet.CheckFleetTargetWithinFuelRangeAndRefuel(habitat.Xpos, habitat.Ypos, 0.0))
                                {
                                    fleet.AttackPoint = habitat;
                                    return habitat;
                                }
                                ResourceList requiredFuel = DetermineFuelRequiredForFleet(fleet);
                                StellarObject stellarObject = DecideBestFleetRefuelPoint(habitat.Xpos, habitat.Ypos, this, requiredFuel, otherEmpire);
                                if (stellarObject != null)
                                {
                                    double num = fleet.MaximumRange();
                                    double num2 = _Galaxy.CalculateDistance(stellarObject.Xpos, stellarObject.Ypos, habitat.Xpos, habitat.Ypos);
                                    if (num2 < num * 0.45 && fleet.CheckFleetTargetWithinFuelRange(stellarObject.Xpos, stellarObject.Ypos, 0.1))
                                    {
                                        waypointing = true;
                                        fleet.GatherPoint = stellarObject;
                                        fleet.AttackPoint = habitat;
                                        fleet.PostureRangeSquared = 2304000000.0;
                                        fleet.AssignMission(BuiltObjectMissionType.Refuel, stellarObject, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                                        return habitat;
                                    }
                                }
                            }
                        }
                        BuiltObject[] array2 = _Galaxy.SortBuiltObjectsByDistanceThreadsafe(fleet.LeadShip.Xpos, fleet.LeadShip.Ypos, diplomaticRelation.WarObjectiveBases);
                        foreach (BuiltObject builtObject in array2)
                        {
                            if (!CheckWarObjectiveStillValid(builtObject, otherEmpire))
                            {
                                continue;
                            }
                            bool fleetAlreadyAssigned2 = false;
                            if (!CheckTargetRequiresMoreAttackFleets(builtObject, otherEmpire, out fleetAlreadyAssigned2) || !CheckFleetCanAttackTarget(fleet, builtObject, fleetAlreadyAssigned2))
                            {
                                continue;
                            }
                            if (fleet.CheckFleetTargetWithinFuelRangeAndRefuel(builtObject.Xpos, builtObject.Ypos, 0.0))
                            {
                                fleet.AttackPoint = builtObject;
                                return builtObject;
                            }
                            ResourceList requiredFuel2 = DetermineFuelRequiredForFleet(fleet);
                            StellarObject stellarObject2 = DecideBestFleetRefuelPoint(builtObject.Xpos, builtObject.Ypos, this, requiredFuel2, otherEmpire);
                            if (stellarObject2 != null)
                            {
                                double num3 = fleet.MaximumRange();
                                double num4 = _Galaxy.CalculateDistance(stellarObject2.Xpos, stellarObject2.Ypos, builtObject.Xpos, builtObject.Ypos);
                                if (num4 < num3 * 0.45 && fleet.CheckFleetTargetWithinFuelRange(stellarObject2.Xpos, stellarObject2.Ypos, 0.1))
                                {
                                    waypointing = true;
                                    fleet.GatherPoint = stellarObject2;
                                    fleet.AttackPoint = builtObject;
                                    fleet.PostureRangeSquared = 2304000000.0;
                                    fleet.AssignMission(BuiltObjectMissionType.Refuel, stellarObject2, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                                    return builtObject;
                                }
                            }
                        }
                        fleet.AttackPoint = null;
                    }
                    else if (CheckWarObjectiveStillValid(fleet.AttackPoint, otherEmpire))
                    {
                        if (CheckFleetCanAttackTarget(fleet, fleet.AttackPoint))
                        {
                            return fleet.AttackPoint;
                        }
                        fleet.AttackPoint = null;
                        attackPointClearedForReassignment = true;
                    }
                    else
                    {
                        Habitat[] array3 = new Habitat[0];
                        if ((fleet.ShipTargetAmount >= 10 || fleet.Ships.Count >= 10) && fleet.TotalTroopAttackStrength > 0)
                        {
                            array3 = _Galaxy.SortHabitatsByDistanceThreadsafe(fleet.LeadShip.Xpos, fleet.LeadShip.Ypos, diplomaticRelation.WarObjectiveColonies);
                        }
                        if ((fleet.ShipTargetAmount >= 10 || fleet.Ships.Count >= 10) && fleet.TotalTroopAttackStrength > 0)
                        {
                            foreach (Habitat habitat2 in array3)
                            {
                                if (!CheckWarObjectiveStillValid(habitat2, otherEmpire))
                                {
                                    continue;
                                }
                                double num5 = _Galaxy.CalculateDistanceSquared(fleet.AttackPoint.Xpos, fleet.AttackPoint.Ypos, habitat2.Xpos, habitat2.Ypos);
                                if (num5 <= fleet.PostureRangeSquared)
                                {
                                    bool fleetAlreadyAssigned3 = false;
                                    if (CheckTargetRequiresMoreAttackFleets(habitat2, otherEmpire, out fleetAlreadyAssigned3) && CheckFleetCanAttackTarget(fleet, habitat2, fleetAlreadyAssigned3) && fleet.CheckFleetTargetWithinFuelRangeAndRefuel(habitat2.Xpos, habitat2.Ypos, 0.0))
                                    {
                                        return habitat2;
                                    }
                                }
                            }
                        }
                        BuiltObject[] array4 = _Galaxy.SortBuiltObjectsByDistanceThreadsafe(fleet.LeadShip.Xpos, fleet.LeadShip.Ypos, diplomaticRelation.WarObjectiveBases);
                        foreach (BuiltObject builtObject2 in array4)
                        {
                            if (!CheckWarObjectiveStillValid(builtObject2, otherEmpire))
                            {
                                continue;
                            }
                            double num6 = _Galaxy.CalculateDistanceSquared(fleet.AttackPoint.Xpos, fleet.AttackPoint.Ypos, builtObject2.Xpos, builtObject2.Ypos);
                            if (num6 <= fleet.PostureRangeSquared)
                            {
                                bool fleetAlreadyAssigned4 = false;
                                if (CheckTargetRequiresMoreAttackFleets(builtObject2, otherEmpire, out fleetAlreadyAssigned4) && CheckFleetCanAttackTarget(fleet, builtObject2, fleetAlreadyAssigned4) && fleet.CheckFleetTargetWithinFuelRangeAndRefuel(builtObject2.Xpos, builtObject2.Ypos, 0.0))
                                {
                                    return builtObject2;
                                }
                            }
                        }
                        if ((fleet.ShipTargetAmount >= 10 || fleet.Ships.Count >= 10) && fleet.TotalTroopAttackStrength > 0)
                        {
                            foreach (Habitat habitat3 in array3)
                            {
                                if (CheckWarObjectiveStillValid(habitat3, otherEmpire))
                                {
                                    bool fleetAlreadyAssigned5 = false;
                                    if (CheckTargetRequiresMoreAttackFleets(habitat3, otherEmpire, out fleetAlreadyAssigned5) && CheckFleetCanAttackTarget(fleet, habitat3, fleetAlreadyAssigned5) && fleet.CheckFleetTargetWithinFuelRangeAndRefuel(habitat3.Xpos, habitat3.Ypos, 0.0))
                                    {
                                        fleet.AttackPoint = habitat3;
                                        return habitat3;
                                    }
                                }
                            }
                        }
                        foreach (BuiltObject builtObject3 in array4)
                        {
                            if (CheckWarObjectiveStillValid(builtObject3, otherEmpire))
                            {
                                bool fleetAlreadyAssigned6 = false;
                                if (CheckTargetRequiresMoreAttackFleets(builtObject3, otherEmpire, out fleetAlreadyAssigned6) && CheckFleetCanAttackTarget(fleet, builtObject3, fleetAlreadyAssigned6) && fleet.CheckFleetTargetWithinFuelRangeAndRefuel(builtObject3.Xpos, builtObject3.Ypos, 0.0))
                                {
                                    fleet.AttackPoint = builtObject3;
                                    return builtObject3;
                                }
                            }
                        }
                        fleet.AttackPoint = null;
                    }
                }
            }
            return null;
        }

        private bool CheckFleetCanAttackTarget(ShipGroup fleet, StellarObject target)
        {
            return CheckFleetCanAttackTarget(fleet, target, supportingOtherAttack: false);
        }

        private bool CheckFleetCanAttackTarget(ShipGroup fleet, StellarObject target, bool supportingOtherAttack)
        {
            double num = 0.85;
            if (supportingOtherAttack)
            {
                num = 0.35;
            }
            int troopStrength = 0;
            int num2 = CalculateDefendingStrength(target, out troopStrength);
            double num3 = (double)fleet.TotalOverallStrengthFactor / (double)num2;
            if (num3 < num)
            {
                return false;
            }
            if (target is Habitat)
            {
                double num4 = 1.2;
                if (supportingOtherAttack)
                {
                    num4 = 0.5;
                }
                int totalTroopAttackStrength = fleet.TotalTroopAttackStrength;
                if (totalTroopAttackStrength > (int)((double)troopStrength * num4))
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        public int CalculateDefendingStrength(StellarObject target)
        {
            int troopStrength = 0;
            return CalculateDefendingStrength(target, out troopStrength);
        }

        public int CalculateDefendingStrength(StellarObject target, out int troopStrength)
        {
            int result = 0;
            troopStrength = 0;
            if (target is Habitat)
            {
                Habitat habitat = (Habitat)target;
                result = habitat.EstimatedDefensiveForceRequired(atWar: true);
                troopStrength = Galaxy.DetermineRequiredTroopStrength(this, habitat);
                if (CheckSystemVisible(habitat.SystemIndex))
                {
                    result = _Galaxy.DetermineDefendingStrength(habitat, target.Empire);
                }
                else if (CheckSystemExplored(habitat.SystemIndex))
                {
                    result = _Galaxy.DetermineBaseStrengthAtHabitat(habitat, target.Empire);
                }
            }
            else if (target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)target;
                result = builtObject.CalculateOverallStrengthFactor();
                troopStrength = 0;
                if (IsObjectVisibleToThisEmpire(builtObject))
                {
                    result = _Galaxy.DetermineDefendingStrength(builtObject, target.Empire);
                }
                result = Math.Max(1, result);
            }
            else if (target is Creature)
            {
                Creature creature = (Creature)target;
                result = creature.AttackStrength * 5;
            }
            return result;
        }

        private bool CheckTargetRequiresMoreAttackFleets(StellarObject target, Empire targetEmpire, out bool fleetAlreadyAssigned)
        {
            fleetAlreadyAssigned = false;
            int troopStrengthAssigned = 0;
            int num = CountFleetAttackStrengthAssignedToTarget(target, out troopStrengthAssigned);
            if (num > 0)
            {
                fleetAlreadyAssigned = true;
            }
            int troopStrength = 0;
            int num2 = CalculateDefendingStrength(target, out troopStrength);
            if (num < num2 || troopStrengthAssigned < (int)((double)troopStrength * 1.7))
            {
                return true;
            }
            return false;
        }

        private bool CheckWarObjectiveStillValid(StellarObject target, Empire targetEmpire)
        {
            if (target == null || target.HasBeenDestroyed || target.Empire != targetEmpire)
            {
                return false;
            }
            return true;
        }

        private bool CheckReadyForWar(Empire otherEmpire)
        {
            bool result = true;
            if (!CheckEmpireHasHyperDriveTech(this))
            {
                return false;
            }
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            int num = 0;
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (shipGroup != null && shipGroup.Posture == FleetPosture.Attack && shipGroup.Ships.Count > 0)
                {
                    num++;
                }
            }
            if (num <= 0)
            {
                return false;
            }
            switch (diplomaticRelation.WarObjective)
            {
                case WarObjective.CaptureObjectives:
                    {
                        for (int j = 0; j < ShipGroups.Count; j++)
                        {
                            ShipGroup shipGroup2 = ShipGroups[j];
                            if (shipGroup2.Posture != 0 || shipGroup2.AttackPoint == null || shipGroup2.LeadShip == null || !shipGroup2.LeadShip.IsAutoControlled || shipGroup2.GatherPoint == null)
                            {
                                continue;
                            }
                            if (shipGroup2.Mission != null && shipGroup2.Mission.Type == BuiltObjectMissionType.Refuel)
                            {
                                result = false;
                            }
                            else if (shipGroup2.Mission == null || shipGroup2.Mission.Type == BuiltObjectMissionType.Undefined)
                            {
                                double num2 = _Galaxy.CalculateDistance(shipGroup2.LeadShip.Xpos, shipGroup2.LeadShip.Ypos, shipGroup2.GatherPoint.Xpos, shipGroup2.GatherPoint.Ypos);
                                int fleetFuelCapacity = 0;
                                ResourceList resourceList = DetermineFuelRequiredForFleet(shipGroup2, out fleetFuelCapacity);
                                int num3 = 0;
                                for (int k = 0; k < resourceList.Count; k++)
                                {
                                    num3 += (int)resourceList[k].SortTag;
                                }
                                double num4 = (double)(fleetFuelCapacity - num3) / (double)fleetFuelCapacity;
                                if (num2 > 48000.0 && shipGroup2.GatherPoint != null)
                                {
                                    shipGroup2.AssignMission(BuiltObjectMissionType.Refuel, shipGroup2.GatherPoint, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                                    result = false;
                                }
                                else if ((!(num2 <= 48000.0) || !(num4 > 0.75)) && shipGroup2.GatherPoint != null)
                                {
                                    shipGroup2.AssignMission(BuiltObjectMissionType.Refuel, shipGroup2.GatherPoint, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                                    result = false;
                                }
                            }
                            else if (shipGroup2.Mission != null && shipGroup2.Mission.Priority == BuiltObjectMissionPriority.Low && shipGroup2.Mission.Type == BuiltObjectMissionType.Move && shipGroup2.Mission.Target == shipGroup2.GatherPoint)
                            {
                                result = false;
                            }
                            else if (shipGroup2.Mission != null && shipGroup2.Mission.Type == BuiltObjectMissionType.Blockade && shipGroup2.Mission.Target != null)
                            {
                                Point point = shipGroup2.Mission.ResolveTargetCoordinates(shipGroup2.Mission);
                                double num5 = _Galaxy.CalculateDistance(shipGroup2.LeadShip.Xpos, shipGroup2.LeadShip.Ypos, point.X, point.Y);
                                if (num5 > 48000.0)
                                {
                                    result = false;
                                }
                            }
                            else if (shipGroup2.GatherPoint != null && this != _Galaxy.PlayerEmpire)
                            {
                                result = false;
                            }
                        }
                        break;
                    }
                default:
                    result = false;
                    break;
                case WarObjective.TotalConquest:
                    break;
            }
            return result;
        }

        private void StartWar(Empire otherEmpire)
        {
            if (_ControlDiplomacyOffense != 0)
            {
                int refusalCount = 0;
                if (CheckTaskAuthorized(_ControlDiplomacyOffense, ref refusalCount, GenerateAutomationMessageWarTradeSanctions(otherEmpire, DiplomaticRelationType.War), otherEmpire, AdvisorMessageType.WarTradeSanctions, DiplomaticRelationType.War, null))
                {
                    DeclareWar(otherEmpire);
                }
            }
        }

        private void SubjugateRequest(Empire otherEmpire)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            if (diplomaticRelation == null || diplomaticRelation.Type != DiplomaticRelationType.War)
            {
                return;
            }
            long num = CalculateNextAllowableProposalDate(diplomaticRelation);
            if (_Galaxy.CurrentStarDate >= num)
            {
                DiplomaticRelation diplomaticRelation2 = new DiplomaticRelation(DiplomaticRelationType.SubjugatedDominion, this, this, otherEmpire, _Galaxy.CurrentStarDate, diplomaticRelation.SupplyRestrictedResources);
                int refusalCount = 0;
                if (CheckTaskAuthorized(_ControlDiplomacyOffense, ref refusalCount, GenerateAutomationMessageWarTradeSanctions(otherEmpire, DiplomaticRelationType.SubjugatedDominion), otherEmpire, AdvisorMessageType.WarTradeSanctions, DiplomaticRelationType.SubjugatedDominion, null))
                {
                    otherEmpire.ProposedDiplomaticRelations.Add(diplomaticRelation2);
                    int ourPotencyVersusThem = DetermineRelativeStrength(MilitaryPotency, otherEmpire);
                    string description = GenerateMessageDescription(diplomaticRelation, DiplomaticRelationType.SubjugatedDominion, ourPotencyVersusThem);
                    diplomaticRelation.LastDiplomacyTradeOfferDate = _Galaxy.CurrentStarDate;
                    SendMessageToEmpire(otherEmpire, EmpireMessageType.ProposeDiplomaticRelation, DiplomaticRelationType.None, description, "");
                }
            }
        }

        private void EndWarRequest(Empire otherEmpire)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            if (diplomaticRelation == null || diplomaticRelation.Type != DiplomaticRelationType.War)
            {
                return;
            }
            long num = CalculateNextAllowableProposalDate(diplomaticRelation);
            if (_Galaxy.CurrentStarDate >= num)
            {
                DiplomaticRelation diplomaticRelation2 = new DiplomaticRelation(DiplomaticRelationType.None, this, this, otherEmpire, _Galaxy.CurrentStarDate, diplomaticRelation.SupplyRestrictedResources);
                int refusalCount = 0;
                if (CheckTaskAuthorized(_ControlDiplomacyOffense, ref refusalCount, GenerateAutomationMessageWarTradeSanctions(otherEmpire, DiplomaticRelationType.None), otherEmpire, AdvisorMessageType.WarTradeSanctions, DiplomaticRelationType.None, null))
                {
                    otherEmpire.ProposedDiplomaticRelations.Add(diplomaticRelation2);
                    string description = GenerateMessageDescriptionEndWarRequest();
                    diplomaticRelation.LastDiplomacyTradeOfferDate = _Galaxy.CurrentStarDate;
                    SendMessageToEmpire(otherEmpire, EmpireMessageType.ProposeDiplomaticRelation, DiplomaticRelationType.None, description, "");
                }
            }
        }

        private void StartTradeSanctionsIfTimePassed(Empire otherEmpire)
        {
            if (_ControlDiplomacyOffense != 0)
            {
                long currentStarDate = _Galaxy.CurrentStarDate;
                DiplomaticRelation relation = ObtainDiplomaticRelation(otherEmpire);
                long num = CalculateNextAllowableProposalDate(relation);
                if (currentStarDate >= num)
                {
                    StartTradeSanctions(otherEmpire);
                }
            }
        }

        private void StartTradeSanctions(Empire otherEmpire)
        {
            if (_ControlDiplomacyOffense == AutomationLevel.Manual)
            {
                return;
            }
            EmpireEvaluation empireEvaluation = otherEmpire.ObtainEmpireEvaluation(this);
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            if (diplomaticRelation.Type != DiplomaticRelationType.War && diplomaticRelation.Type != DiplomaticRelationType.SubjugatedDominion)
            {
                int refusalCount = 0;
                if (CheckTaskAuthorized(_ControlDiplomacyOffense, ref refusalCount, GenerateAutomationMessageWarTradeSanctions(otherEmpire, DiplomaticRelationType.TradeSanctions), otherEmpire, AdvisorMessageType.WarTradeSanctions, DiplomaticRelationType.TradeSanctions, null))
                {
                    ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.TradeSanctions);
                    empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - 20.0;
                    diplomaticRelation.LastDiplomacyTradeOfferDate = _Galaxy.CurrentStarDate;
                    int ourPotencyVersusThem = DetermineRelativeStrength(MilitaryPotency, otherEmpire);
                    string description = GenerateMessageDescription(diplomaticRelation, DiplomaticRelationType.TradeSanctions, ourPotencyVersusThem);
                    SendMessageToEmpire(otherEmpire, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.TradeSanctions, description);
                }
            }
        }

        private void EndTradeSanctionsIfTimePassed(Empire otherEmpire)
        {
            if (_ControlDiplomacyOffense != 0)
            {
                long currentStarDate = _Galaxy.CurrentStarDate;
                DiplomaticRelation relation = ObtainDiplomaticRelation(otherEmpire);
                long num = CalculateNextAllowableProposalDate(relation);
                if (currentStarDate >= num)
                {
                    EndTradeSanctions(otherEmpire);
                }
            }
        }

        private void EndTradeSanctions(Empire otherEmpire)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            if (diplomaticRelation != null && diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions && diplomaticRelation.Initiator == this)
            {
                int refusalCount = 0;
                if (CheckTaskAuthorized(_ControlDiplomacyOffense, ref refusalCount, GenerateAutomationMessageWarTradeSanctions(otherEmpire, DiplomaticRelationType.None), otherEmpire, AdvisorMessageType.WarTradeSanctions, DiplomaticRelationType.None, null))
                {
                    ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.None);
                    CancelBlockades(otherEmpire);
                    otherEmpire.CancelBlockades(this);
                    diplomaticRelation.LastDiplomacyTradeOfferDate = _Galaxy.CurrentStarDate;
                    string description = GenerateMessageDescriptionLiftTradeSanctions();
                    SendMessageToEmpire(otherEmpire, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.None, description, Galaxy.ResolveDescription(DiplomaticRelationType.TradeSanctions));
                }
            }
        }

        private void EndSubjugation(Empire otherEmpire)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            if (diplomaticRelation != null && diplomaticRelation.Type == DiplomaticRelationType.SubjugatedDominion && diplomaticRelation.Initiator == this)
            {
                int refusalCount = 0;
                if (CheckTaskAuthorized(_ControlDiplomacyTreaties, ref refusalCount, GenerateAutomationMessageTreaty(otherEmpire, DiplomaticRelationType.None), otherEmpire, AdvisorMessageType.TreatyOffer, DiplomaticRelationType.None, null))
                {
                    ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.None);
                    diplomaticRelation.LastDiplomacyTradeOfferDate = _Galaxy.CurrentStarDate;
                    string description = GenerateMessageDescriptionEndSubjugation();
                    SendMessageToEmpire(otherEmpire, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.None, description, Galaxy.ResolveDescription(DiplomaticRelationType.SubjugatedDominion));
                }
            }
        }

        private void CancelTreatiesIfTimePassed(Empire otherEmpire)
        {
            if (_ControlDiplomacyOffense != 0)
            {
                long currentStarDate = _Galaxy.CurrentStarDate;
                DiplomaticRelation relation = ObtainDiplomaticRelation(otherEmpire);
                long num = CalculateNextAllowableProposalDate(relation);
                if (currentStarDate >= num)
                {
                    CancelTreaties(otherEmpire);
                }
            }
        }

        private void CancelTreaties(Empire otherEmpire)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            if (diplomaticRelation.Type != DiplomaticRelationType.FreeTradeAgreement && diplomaticRelation.Type != DiplomaticRelationType.MutualDefensePact && diplomaticRelation.Type != DiplomaticRelationType.Protectorate)
            {
                return;
            }
            int refusalCount = 0;
            if (CheckTaskAuthorized(_ControlDiplomacyTreaties, ref refusalCount, GenerateAutomationMessageTreaty(otherEmpire, DiplomaticRelationType.None), otherEmpire, AdvisorMessageType.TreatyOffer, DiplomaticRelationType.None, null))
            {
                if (diplomaticRelation.Type != 0)
                {
                    SendMessageToEmpire(otherEmpire, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.None, TextResolver.GetText("We cancel our treaty with you."), Galaxy.ResolveDescription(diplomaticRelation.Type));
                }
                diplomaticRelation.LastDiplomacyTradeOfferDate = _Galaxy.CurrentStarDate;
                ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.None);
            }
        }

        private void CheckCancelMilitaryRefueling(Empire otherEmpire)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            if (diplomaticRelation.MilitaryRefuelingToOther && diplomaticRelation.Type != DiplomaticRelationType.MutualDefensePact && diplomaticRelation.Type != DiplomaticRelationType.Protectorate && CheckTaskAuthorized(_ControlDiplomacyTreaties, GenerateAutomationMessageMilitaryRefueling(diplomaticRelation.OtherEmpire, refuel: false), diplomaticRelation.OtherEmpire, AdvisorMessageType.CancelMilitaryRefueling))
            {
                diplomaticRelation.MilitaryRefuelingToOther = false;
                string text = TextResolver.GetText("Military Refueling Blocked");
                SendMessageToEmpire(otherEmpire, EmpireMessageType.MilitaryRefuelingBlocked, this, text);
            }
        }

        private void CheckCancelMiningRights(Empire otherEmpire)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            if (diplomaticRelation.MiningRightsToOther && CheckTaskAuthorized(_ControlDiplomacyTreaties, GenerateAutomationMessageMiningRights(diplomaticRelation.OtherEmpire, allowMining: false), diplomaticRelation.OtherEmpire, AdvisorMessageType.CancelMiningRights))
            {
                diplomaticRelation.MiningRightsToOther = false;
                string text = TextResolver.GetText("Mining Rights Blocked");
                SendMessageToEmpire(otherEmpire, EmpireMessageType.MiningRightsBlocked, this, text);
            }
        }

        private void CheckCancelRestrictedResourceTrading(Empire otherEmpire)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            if (diplomaticRelation.SupplyRestrictedResources && CheckTaskAuthorized(_ControlDiplomacyTreaties, GenerateAutomationMessageTradeRestrictedResources(diplomaticRelation.OtherEmpire, trade: false), diplomaticRelation.OtherEmpire, AdvisorMessageType.DisallowTradeRestrictedResources))
            {
                diplomaticRelation.SupplyRestrictedResources = false;
                string description = string.Format(TextResolver.GetText("Trade Restricted Resource Refuse EMPIRE"), Name);
                SendMessageToEmpire(otherEmpire, EmpireMessageType.RestrictedResourceTradingBlocked, this, description);
            }
        }

        private void GiveGiftSmallWhenSufficientTimePassed(Empire otherEmpire)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            long num = diplomaticRelation.LastGiftDate + Galaxy.IdealTimeBetweenGifts;
            if (_Galaxy.CurrentStarDate < num)
            {
                return;
            }
            EmpireMessage empireMessage = new EmpireMessage(this, EmpireMessageType.GiveGift, null);
            double num2 = StateMoney;
            if (double.IsInfinity(num2) || double.IsNaN(num2))
            {
                num2 = 0.0;
            }
            if (!(num2 > 8400.0))
            {
                return;
            }
            int num3 = Math.Max(105, (int)(num2 / 40.0));
            if (num3 <= 100)
            {
                return;
            }
            double val = Galaxy.Rnd.Next(100, num3);
            val = Math.Min(val, Policy.DiplomacySendGiftsUpToAmount);
            if (val > 0.0)
            {
                int refusalCount = 0;
                if (CheckTaskAuthorized(_ControlDiplomacyGifts, ref refusalCount, GenerateAutomationMessageDiplomaticGift(otherEmpire, val), otherEmpire, AdvisorMessageType.DiplomaticGift, val, null))
                {
                    empireMessage.Money = (int)val;
                    StateMoney -= val;
                    PirateEconomy.PerformExpense(val, PirateExpenseType.Undefined, _Galaxy.CurrentStarDate);
                    empireMessage.Description = string.Format(TextResolver.GetText("Please accept our gift of X credits"), val.ToString("###,###,###,##0"));
                    SendMessageToEmpire(empireMessage, otherEmpire);
                }
            }
        }

        private void GiveGiftWhenSufficientTimePassed(Empire otherEmpire)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            long num = diplomaticRelation.LastGiftDate + Galaxy.IdealTimeBetweenGifts;
            if (_Galaxy.CurrentStarDate < num)
            {
                return;
            }
            EmpireMessage empireMessage = new EmpireMessage(this, EmpireMessageType.GiveGift, null);
            double num2 = StateMoney;
            if (double.IsInfinity(num2) || double.IsNaN(num2))
            {
                num2 = 0.0;
            }
            if (!(num2 > 8400.0))
            {
                return;
            }
            int num3 = Math.Max(105, (int)(num2 / 10.0));
            if (num3 <= 100)
            {
                return;
            }
            double val = Galaxy.Rnd.Next(100, num3);
            val = Math.Min(val, Policy.DiplomacySendGiftsUpToAmount);
            if (val > 0.0)
            {
                int refusalCount = 0;
                if (CheckTaskAuthorized(_ControlDiplomacyGifts, ref refusalCount, GenerateAutomationMessageDiplomaticGift(otherEmpire, val), otherEmpire, AdvisorMessageType.DiplomaticGift, val, null))
                {
                    empireMessage.Money = (int)val;
                    StateMoney -= val;
                    PirateEconomy.PerformExpense(val, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                    empireMessage.Description = string.Format(TextResolver.GetText("Please accept our gift of X credits"), val.ToString("###,###,###,##0"));
                    SendMessageToEmpire(empireMessage, otherEmpire);
                }
            }
        }

        private void OfferFreeTrade(Empire otherEmpire)
        {
            if (otherEmpire.Reclusive || (!CheckEmpireHasHyperDriveTech(this) && !CheckEmpireHasHyperDriveTech(otherEmpire)))
            {
                return;
            }
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            long num = CalculateNextAllowableProposalDate(diplomaticRelation);
            if (_Galaxy.CurrentStarDate >= num)
            {
                DiplomaticRelation diplomaticRelation2 = new DiplomaticRelation(DiplomaticRelationType.FreeTradeAgreement, this, this, otherEmpire, _Galaxy.CurrentStarDate, diplomaticRelation.SupplyRestrictedResources);
                int refusalCount = 0;
                if (CheckTaskAuthorized(_ControlDiplomacyTreaties, ref refusalCount, GenerateAutomationMessageTreaty(otherEmpire, DiplomaticRelationType.FreeTradeAgreement), otherEmpire, AdvisorMessageType.TreatyOffer, DiplomaticRelationType.FreeTradeAgreement, null))
                {
                    otherEmpire.ProposedDiplomaticRelations.Add(diplomaticRelation2);
                    int ourPotencyVersusThem = DetermineRelativeStrength(MilitaryPotency, otherEmpire);
                    string description = GenerateMessageDescription(diplomaticRelation, DiplomaticRelationType.FreeTradeAgreement, ourPotencyVersusThem);
                    diplomaticRelation.LastDiplomacyTradeOfferDate = _Galaxy.CurrentStarDate;
                    SendMessageToEmpire(otherEmpire, EmpireMessageType.ProposeDiplomaticRelation, DiplomaticRelationType.FreeTradeAgreement, description);
                }
            }
        }

        private void OfferMutualDefense(Empire otherEmpire)
        {
            if (otherEmpire.Reclusive || (!CheckEmpireHasHyperDriveTech(this) && !CheckEmpireHasHyperDriveTech(otherEmpire)))
            {
                return;
            }
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            long num = CalculateNextAllowableProposalDate(diplomaticRelation);
            if (_Galaxy.CurrentStarDate >= num)
            {
                DiplomaticRelationType diplomaticRelationType = DiplomaticRelationType.MutualDefensePact;
                double num2 = (double)TotalColonyStrategicValue / (double)otherEmpire.TotalColonyStrategicValue;
                if (num2 > 4.0)
                {
                    diplomaticRelationType = DiplomaticRelationType.Protectorate;
                }
                DiplomaticRelation diplomaticRelation2 = new DiplomaticRelation(diplomaticRelationType, this, this, otherEmpire, _Galaxy.CurrentStarDate, diplomaticRelation.SupplyRestrictedResources);
                int refusalCount = 0;
                if (CheckTaskAuthorized(_ControlDiplomacyTreaties, ref refusalCount, GenerateAutomationMessageTreaty(otherEmpire, diplomaticRelationType), otherEmpire, AdvisorMessageType.TreatyOffer, diplomaticRelationType, null))
                {
                    otherEmpire.ProposedDiplomaticRelations.Add(diplomaticRelation2);
                    int ourPotencyVersusThem = DetermineRelativeStrength(MilitaryPotency, otherEmpire);
                    string description = GenerateMessageDescription(diplomaticRelation, diplomaticRelationType, ourPotencyVersusThem);
                    diplomaticRelation.LastDiplomacyTradeOfferDate = _Galaxy.CurrentStarDate;
                    SendMessageToEmpire(otherEmpire, EmpireMessageType.ProposeDiplomaticRelation, diplomaticRelationType, description);
                }
            }
        }

        public void OfferMilitaryRefueling(Empire otherEmpire)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            if (!diplomaticRelation.MilitaryRefuelingToOther)
            {
                diplomaticRelation.MilitaryRefuelingToOther = true;
                string text = TextResolver.GetText("Military Refueling Allowed");
                SendMessageToEmpire(otherEmpire, EmpireMessageType.MilitaryRefuelingAllowed, this, text);
            }
        }

        public void OfferMiningRights(Empire otherEmpire)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            if (!diplomaticRelation.MiningRightsToOther)
            {
                diplomaticRelation.MiningRightsToOther = true;
                string text = TextResolver.GetText("Mining Rights Allowed");
                SendMessageToEmpire(otherEmpire, EmpireMessageType.MiningRightsAllowed, this, text);
            }
        }

        public void ClearInvalidDiplomaticRelations()
        {
            DiplomaticRelationList diplomaticRelationList = new DiplomaticRelationList();
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation != null && (diplomaticRelation.OtherEmpire == null || diplomaticRelation.OtherEmpire == this || !diplomaticRelation.OtherEmpire.Active))
                {
                    diplomaticRelationList.Add(diplomaticRelation);
                }
            }
            for (int j = 0; j < diplomaticRelationList.Count; j++)
            {
                DiplomaticRelations.Remove(diplomaticRelationList[j]);
            }
            List<PirateRelation> list = new PirateRelationList();
            for (int k = 0; k < PirateRelations.Count; k++)
            {
                PirateRelation pirateRelation = PirateRelations[k];
                if (pirateRelation != null && (pirateRelation.OtherEmpire == null || pirateRelation.OtherEmpire == this || !pirateRelation.OtherEmpire.Active))
                {
                    list.Add(pirateRelation);
                }
            }
            for (int l = 0; l < list.Count; l++)
            {
                PirateRelations.Remove(list[l]);
            }
        }

        private void EvaluatePoliticalSituation(TimeSpan timePassedSpan)
        {
            int refusalCount = 0;
            _ = string.Empty;
            _TopCompetitor = IdentifyTopCompetitor();
            _ = _Galaxy.IntoleranceLevel;
            double num = (double)Galaxy.CivilityRatingAnnualNeutralizationAmount * (timePassedSpan.TotalSeconds / (double)Galaxy.RealSecondsInGalacticYear);
            if (CivilityRating > 0.0)
            {
                CivilityRating -= num;
                if (CivilityRating < 0.0)
                {
                    CivilityRating = 0.0;
                }
            }
            else
            {
                CivilityRating += num;
                if (CivilityRating > 0.0)
                {
                    CivilityRating = 0.0;
                }
            }
            DetermineFriendsAndEnemies(this, out var friends, out var closeFriends, out var _, out var _);
            HabitatList ourSystemStars = DetermineEmpireDominatedSystems(this, includeAllTerritory: true);
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire == this)
                {
                    continue;
                }
                DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                if (diplomaticRelation.Type == DiplomaticRelationType.NotMet)
                {
                    continue;
                }
                int positiveRelationship = 0;
                int negativeRelationship = 0;
                CalculateRelationshipWithFriends(empire, friends, closeFriends, out positiveRelationship, out negativeRelationship);
                int systemCompetition = CalculateSystemCompetition(empire, ourSystemStars);
                int tradeVolume = CalculateTradeVolume(empire);
                int envy = CalculateEnvy(empire);
                int num2 = 0;
                if (empire.CheckEmpireSuppliesRestrictedResources())
                {
                    DiplomaticRelation diplomaticRelation2 = empire.ObtainDiplomaticRelation(this);
                    num2 = ((!diplomaticRelation2.SupplyRestrictedResources) ? (-5) : 10);
                }
                EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(empire);
                if (diplomaticRelation.Type != 0 && empireEvaluation.FirstContactPenalty < 0.0)
                {
                    double num3 = EmpireEvaluation.FirstContactPenaltyAnnualReductionAmount * (timePassedSpan.TotalSeconds / (double)Galaxy.RealSecondsInGalacticYear);
                    double val = empireEvaluation.FirstContactPenalty + num3;
                    val = (empireEvaluation.FirstContactPenalty = Math.Min(0.0, val));
                }
                double num4 = 1.0;
                int num5 = -100;
                CharacterList characterList = empire.Characters.FindCharactersAtLocationNotTransferring(Capital, CharacterRole.Ambassador);
                for (int j = 0; j < characterList.Count; j++)
                {
                    Character character = characterList[j];
                    if (character != null && character.Role == CharacterRole.Ambassador)
                    {
                        num5 = Math.Max(num5, character.Diplomacy);
                    }
                }
                if (num5 <= -100)
                {
                    num5 = 0;
                }
                num4 *= 1.0 + (double)num5 / 100.0;
                CharacterList charactersByRole = empire.Characters.GetCharactersByRole(CharacterRole.Leader);
                int num6 = -100;
                for (int k = 0; k < charactersByRole.Count; k++)
                {
                    Character character2 = charactersByRole[k];
                    if (character2 != null && character2.Role == CharacterRole.Leader && character2.Location != null && character2.Location is Habitat && empire.Capitals != null && empire.Capitals.Contains((Habitat)character2.Location))
                    {
                        num6 = Math.Max(num6, character2.Diplomacy);
                    }
                }
                if (num6 <= -100)
                {
                    num6 = 0;
                }
                num4 *= 1.0 + (double)num6 / 100.0;
                if (empireEvaluation.DiplomacyFactor == 1.0 || RaceEventType != RaceEventType.GrandPerformanceDiplomacyBonus)
                {
                    empireEvaluation.DiplomacyFactor = num4;
                }
                if (empire.DominantRace != null && empire.DominantRace.Name.ToLower(CultureInfo.InvariantCulture) == "mechanoid")
                {
                    envy = 0;
                }
                empireEvaluation.RelationshipWithFriendsPositive = positiveRelationship;
                empireEvaluation.RelationshipWithFriendsNegative = negativeRelationship;
                empireEvaluation.SystemCompetition = systemCompetition;
                empireEvaluation.TradeVolume = tradeVolume;
                empireEvaluation.RestrictedResourceTrading = num2;
                empireEvaluation.Envy = envy;
                DiplomaticRelation diplomaticRelation3 = empire.ObtainDiplomaticRelation(this);
                if (diplomaticRelation3.MilitaryRefuelingToOther)
                {
                    empireEvaluation.MilitaryRefueling = 5;
                }
                else
                {
                    empireEvaluation.MilitaryRefueling = 0;
                }
                if (diplomaticRelation3.MiningRightsToOther)
                {
                    empireEvaluation.MiningRights = 4;
                }
                else
                {
                    empireEvaluation.MiningRights = 0;
                }
                if (GovernmentAttributes != null)
                {
                    empireEvaluation.CivilityRatingWeight = GovernmentAttributes.ImportanceOfOthersReputations;
                }
                else
                {
                    empireEvaluation.CivilityRatingWeight = 1.0;
                }
                empireEvaluation.Covetousness = 0;
                if (_EmpiresWithDesiredColonies.Count > 0 && _EmpiresWithDesiredColonies.Contains(empire))
                {
                    int num7 = _DesiredForeignColonies.TotalPriorityValueForEmpire(empire);
                    double num8 = (double)DominantRace.AggressionLevel / 100.0;
                    num8 *= num8;
                    num8 = Math.Max(num8, 1.0);
                    double num9 = 60.0 / num8;
                    empireEvaluation.Covetousness = (int)((double)num7 / num9 * -1.0);
                }
                empireEvaluation.Blockades = 0;
                BlockadeList blockadesAgainstEmpire = _Galaxy.Blockades.GetBlockadesAgainstEmpire(this);
                foreach (Blockade item in blockadesAgainstEmpire)
                {
                    if (item.Initiator == empire)
                    {
                        empireEvaluation.Blockades += Galaxy.BlockadeEmpireEvaluationValue;
                    }
                }
                double num10 = (double)Galaxy.IncidentEvaluationAnnualNeutralizationAmount * (timePassedSpan.TotalSeconds / (double)Galaxy.RealSecondsInGalacticYear);
                if (empireEvaluation.IncidentEvaluation > 0.0)
                {
                    empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - num10;
                    if (empireEvaluation.IncidentEvaluation < 0.0)
                    {
                        empireEvaluation.IncidentEvaluation = 0.0;
                    }
                }
                else
                {
                    empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw + num10;
                    if (empireEvaluation.IncidentEvaluation > 0.0)
                    {
                        empireEvaluation.IncidentEvaluation = 0.0;
                    }
                }
                if (GovernmentAttributes != null)
                {
                    empireEvaluation.GovernmentStyleAffinity = (int)GovernmentAttributes.NaturalAffinity(empire.GovernmentId);
                }
                else
                {
                    empireEvaluation.GovernmentStyleAffinity = 0;
                }
                double num11 = (double)empireEvaluation.SystemCompetition * timePassedSpan.TotalSeconds / Galaxy.EmpireEvaluationTrendingFactor;
                if (empireEvaluation.SystemCompetition >= 0)
                {
                    num11 = 20.0 * timePassedSpan.TotalSeconds / Galaxy.EmpireEvaluationTrendingFactor;
                }
                empireEvaluation.SystemCompetitionCumulative += num11;
                double num12 = (double)empireEvaluation.RelationshipWithFriendsPositive * timePassedSpan.TotalSeconds / Galaxy.EmpireEvaluationTrendingFactor;
                if (empireEvaluation.RelationshipWithFriendsPositive <= 0)
                {
                    num12 = -5.0 * timePassedSpan.TotalSeconds / Galaxy.EmpireEvaluationTrendingFactor;
                }
                empireEvaluation.RelationshipWithFriendsPositiveCumulative += num12;
                double num13 = (double)empireEvaluation.RelationshipWithFriendsNegative * timePassedSpan.TotalSeconds / Galaxy.EmpireEvaluationTrendingFactor;
                if (empireEvaluation.RelationshipWithFriendsNegative >= 0)
                {
                    num13 = 5.0 * timePassedSpan.TotalSeconds / Galaxy.EmpireEvaluationTrendingFactor;
                }
                empireEvaluation.RelationshipWithFriendsNegativeCumulative += num13;
                double num14 = (double)empireEvaluation.Covetousness * timePassedSpan.TotalSeconds / Galaxy.EmpireEvaluationTrendingFactor;
                if (empireEvaluation.Covetousness >= 0 && empireEvaluation.CovetousnessCumulative < 0.0)
                {
                    num14 = 10.0 * timePassedSpan.TotalSeconds / Galaxy.EmpireEvaluationTrendingFactor;
                }
                empireEvaluation.CovetousnessCumulative += num14;
                double num15 = (double)empireEvaluation.GovernmentStyleAffinity * timePassedSpan.TotalSeconds / Galaxy.EmpireEvaluationTrendingFactor;
                if (empireEvaluation.GovernmentStyleAffinity == 0)
                {
                    if (empireEvaluation.GovernmentStyleAffinityCumulative > 0.0)
                    {
                        num15 = -10.0 * timePassedSpan.TotalSeconds / Galaxy.EmpireEvaluationTrendingFactor;
                        if (empireEvaluation.GovernmentStyleAffinityCumulative - num15 < 0.0)
                        {
                            num15 = empireEvaluation.GovernmentStyleAffinityCumulative * -1.0;
                        }
                    }
                    else
                    {
                        num15 = 10.0 * timePassedSpan.TotalSeconds / Galaxy.EmpireEvaluationTrendingFactor;
                        if (empireEvaluation.GovernmentStyleAffinityCumulative + num15 > 0.0)
                        {
                            num15 = empireEvaluation.GovernmentStyleAffinityCumulative * -1.0;
                        }
                    }
                }
                empireEvaluation.GovernmentStyleAffinityCumulative += num15;
                double num16 = GovernmentAttributes.NaturalAffinity(empire.GovernmentId);
                if (empireEvaluation.GovernmentStyleAffinity > 0)
                {
                    if (num16 > 0.0)
                    {
                        empireEvaluation.GovernmentStyleAffinityCumulative = Math.Min(empireEvaluation.GovernmentStyleAffinityCumulative, num16);
                    }
                }
                else if (num16 < 0.0)
                {
                    empireEvaluation.GovernmentStyleAffinityCumulative = Math.Max(empireEvaluation.GovernmentStyleAffinityCumulative, num16);
                }
                if (empireEvaluation.RelationshipWithFriendsPositiveCumulative > 0.0)
                {
                    empireEvaluation.RelationshipWithFriendsPositiveCumulative = Math.Min(empireEvaluation.RelationshipWithFriendsPositiveCumulative, EmpireEvaluation.RelationshipWithFriendsCap);
                }
                else
                {
                    empireEvaluation.RelationshipWithFriendsPositiveCumulative = 0.0;
                }
                if (empireEvaluation.RelationshipWithFriendsNegativeCumulative < 0.0)
                {
                    empireEvaluation.RelationshipWithFriendsNegativeCumulative = Math.Max(empireEvaluation.RelationshipWithFriendsNegativeCumulative, EmpireEvaluation.RelationshipWithFriendsCap * -1.0);
                }
                else
                {
                    empireEvaluation.RelationshipWithFriendsNegativeCumulative = 0.0;
                }
                if (empireEvaluation.CovetousnessCumulative < 0.0)
                {
                    empireEvaluation.CovetousnessCumulative = Math.Max(empireEvaluation.CovetousnessCumulative, EmpireEvaluation.CovetousnessCap * -1.0);
                }
                else
                {
                    empireEvaluation.CovetousnessCumulative = 0.0;
                }
                if (empireEvaluation.OverallAttitudeWithoutSystemCompetition < 0)
                {
                    if (empireEvaluation.SystemCompetitionCumulative < 0.0)
                    {
                        empireEvaluation.SystemCompetitionCumulative = Math.Max(empireEvaluation.SystemCompetitionCumulative, EmpireEvaluation.SystemCompetitionCapExtended * -1.0);
                    }
                    else
                    {
                        empireEvaluation.SystemCompetitionCumulative = 0.0;
                    }
                }
                else if (empireEvaluation.SystemCompetitionCumulative < 0.0)
                {
                    empireEvaluation.SystemCompetitionCumulative = Math.Max(empireEvaluation.SystemCompetitionCumulative, EmpireEvaluation.SystemCompetitionCap * -1.0);
                }
                else
                {
                    empireEvaluation.SystemCompetitionCumulative = 0.0;
                }
            }
            int weightedMilitaryPotency = WeightedMilitaryPotency;
            int num17 = CountEmpiresWeDeclaredWarOn();
            CountEmpiresWhoDeclaredWarOnUs();
            int num18 = CountEmpiresWeDeclaredWarOnNonLocked();
            int num19 = CountEmpiresWhoDeclaredWarOnUsNonLocked();
            double num20 = 2.0 - (double)(DominantRace.AggressionLevel + DominantRace.LoyaltyLevel) / 200.0;
            if (num18 > 0 || num19 > 0)
            {
                double num21 = 0.0;
                double num22 = 0.0;
                if (num18 > 0)
                {
                    num21 = num20 * ((double)num18 / 1.0);
                }
                if (num19 > 0)
                {
                    num22 = num20 * ((double)num19 / 4.0);
                }
                num20 = num21 + num22;
                num20 *= GovernmentAttributes.WarWeariness;
                if (DominantRace != null && DominantRace.WarWearinessAttenuation > 0)
                {
                    double num23 = (double)DominantRace.WarWearinessAttenuation / 100.0;
                    num20 *= 1.0 - num23;
                }
            }
            else
            {
                num20 *= BaconEmpire.AdjustWarWearinessWhenAtPeace(this);
            }
            num20 *= timePassedSpan.TotalMilliseconds / 60000.0;
            num20 *= WarWearinessFactor;
            WarWearinessRaw += num20;
            if (WarWeariness < 0.0)
            {
                WarWearinessRaw = 0.0;
            }
            else if (WarWeariness > Galaxy.WarWearinessMaximum)
            {
                WarWearinessRaw = Galaxy.WarWearinessMaximum;
            }
            if (num17 == 0 && CivilityRating < 10.0)
            {
                double num24 = Galaxy.CivilityRatingAnnualRiseAmount * (timePassedSpan.TotalSeconds / (double)Galaxy.RealSecondsInGalacticYear);
                CivilityRating += num24;
            }
            for (int l = 0; l < EmpireEvaluations.Count; l++)
            {
                EmpireEvaluation empireEvaluation2 = EmpireEvaluations[l];
                DiplomaticRelationType diplomaticRelationType = DiplomaticRelationType.None;
                DiplomaticRelation diplomaticRelation4 = ObtainDiplomaticRelation(empireEvaluation2.Empire);
                if (diplomaticRelation4.Type == DiplomaticRelationType.NotMet)
                {
                    continue;
                }
                if (diplomaticRelation4 != null)
                {
                    diplomaticRelationType = diplomaticRelation4.Type;
                }
                int weightedMilitaryPotency2 = empireEvaluation2.Empire.WeightedMilitaryPotency;
                int num25 = 0;
                num25 = EvaluateMilitaryPotency(weightedMilitaryPotency, weightedMilitaryPotency2, empireEvaluation2.Empire);
                if (_ControlDiplomacyTreaties == AutomationLevel.Manual || Reclusive || diplomaticRelationType == DiplomaticRelationType.NotMet || diplomaticRelationType == DiplomaticRelationType.War)
                {
                    continue;
                }
                if (num25 == 1 || num25 == 0)
                {
                    foreach (Empire item2 in closeFriends)
                    {
                        DiplomaticRelation diplomaticRelation5 = empireEvaluation2.Empire.DiplomaticRelations[item2];
                        if (diplomaticRelation5 != null && !diplomaticRelation5.Locked && diplomaticRelation5.Type == DiplomaticRelationType.TradeSanctions && diplomaticRelation5.Initiator != item2 && Galaxy.Rnd.Next(0, 3) == 1 && CheckTaskAuthorized(_ControlDiplomacyTreaties, ref refusalCount, GenerateAutomationMessageRequestLiftTradeSanctions(empireEvaluation2.Empire, item2), empireEvaluation2.Empire, AdvisorMessageType.RequestLiftTradeSanctionsOther, item2, null))
                        {
                            string description = GenerateMessageDescription(EmpireMessageType.RequestLiftTradeSanctions, num25, item2);
                            SendMessageToEmpire(empireEvaluation2.Empire, EmpireMessageType.RequestLiftTradeSanctions, item2, description);
                        }
                    }
                }
                if (num25 != 1)
                {
                    continue;
                }
                foreach (Empire item3 in friends)
                {
                    DiplomaticRelation diplomaticRelation6 = empireEvaluation2.Empire.DiplomaticRelations[item3];
                    if (diplomaticRelation6 != null && !diplomaticRelation6.Locked && diplomaticRelation6.Type == DiplomaticRelationType.TradeSanctions && diplomaticRelation6.Initiator != item3 && Galaxy.Rnd.Next(0, 3) == 1 && CheckTaskAuthorized(_ControlDiplomacyTreaties, ref refusalCount, GenerateAutomationMessageRequestLiftTradeSanctions(empireEvaluation2.Empire, item3), empireEvaluation2.Empire, AdvisorMessageType.RequestLiftTradeSanctionsOther, item3, null))
                    {
                        string description2 = GenerateMessageDescription(EmpireMessageType.RequestLiftTradeSanctions, num25, item3);
                        SendMessageToEmpire(empireEvaluation2.Empire, EmpireMessageType.RequestLiftTradeSanctions, item3, description2);
                    }
                }
            }
            if (_ControlDiplomacyTreaties != 0)
            {
                for (int m = 0; m < RecentSpyingEmpires.Count; m++)
                {
                    Empire empire2 = RecentSpyingEmpires[m];
                    DiplomaticRelation diplomaticRelation7 = ObtainDiplomaticRelation(empire2);
                    if (diplomaticRelation7.Type != DiplomaticRelationType.War)
                    {
                        EmpireEvaluation empireEvaluation3 = ObtainEmpireEvaluation(empire2);
                        int num26 = DominantRace.CautionLevel - DominantRace.AggressionLevel;
                        if (empireEvaluation3.OverallAttitude < num26 && Galaxy.Rnd.Next(0, 3) > 0)
                        {
                            EmpireMessage empireMessage = new EmpireMessage(this, EmpireMessageType.StopMissionsAgainstUs, null);
                            empireMessage.Description = TextResolver.GetText("We warn you to stop your covert missions against us!");
                            SendMessageToEmpire(empireMessage, empire2);
                        }
                    }
                }
                for (int n = 0; n < RecentAttackingEmpires.Count; n++)
                {
                    Empire empire3 = RecentAttackingEmpires[n];
                    DiplomaticRelation diplomaticRelation8 = ObtainDiplomaticRelation(empire3);
                    if (diplomaticRelation8.Type != DiplomaticRelationType.War)
                    {
                        EmpireEvaluation empireEvaluation4 = ObtainEmpireEvaluation(empire3);
                        int num27 = DominantRace.CautionLevel - DominantRace.AggressionLevel;
                        if (empireEvaluation4.OverallAttitude < num27 && Galaxy.Rnd.Next(0, 3) > 0)
                        {
                            EmpireMessage empireMessage2 = new EmpireMessage(this, EmpireMessageType.StopAttacks, null);
                            empireMessage2.Description = TextResolver.GetText("We warn you to stop your attacks against us!");
                            SendMessageToEmpire(empireMessage2, empire3);
                        }
                    }
                }
            }
            RecentAttackingEmpires.Clear();
            RecentSpyingEmpires.Clear();
        }

        private bool CheckCancelTreaty(int overallAttitude, int lowerLevel, int loyaltyLevel, int modifier)
        {
            lowerLevel -= modifier;
            if (overallAttitude < lowerLevel)
            {
                overallAttitude += (loyaltyLevel - 100) / 3;
                if (overallAttitude < lowerLevel)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckDeclareWar(int overallAttitude, int lowerLevel, int loyaltyLevel, DiplomaticRelation currentDiplomaticRelation, int aggressionLevel, int modifier)
        {
            lowerLevel -= modifier;
            overallAttitude -= (aggressionLevel - 100) / 3;
            if (currentDiplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || currentDiplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement || (currentDiplomaticRelation.Type == DiplomaticRelationType.Protectorate && currentDiplomaticRelation.Initiator == this))
            {
                overallAttitude += (loyaltyLevel - 100) / 3;
            }
            if (overallAttitude < lowerLevel)
            {
                return true;
            }
            return false;
        }

        private bool CheckWhetherKnowAnySystemsOfOtherEmpire(Empire empire)
        {
            for (int i = 0; i < empire.Colonies.Count; i++)
            {
                Habitat habitat = empire.Colonies[i];
                Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                SystemVisibilityStatus status = SystemVisibility[habitat2.SystemIndex].Status;
                if (status == SystemVisibilityStatus.Explored || status == SystemVisibilityStatus.Visible)
                {
                    return true;
                }
            }
            return false;
        }

        public PirateRelation ObtainPirateRelation(Empire otherEmpire)
        {
            if (otherEmpire == null)
            {
                return new PirateRelation(this, otherEmpire, PirateRelationType.None);
            }
            if (otherEmpire == this)
            {
                return new PirateRelation(this, otherEmpire, PirateRelationType.Protection);
            }
            PirateRelation pirateRelation = PirateRelations.GetRelationByOtherEmpire(otherEmpire);
            if (pirateRelation == null)
            {
                pirateRelation = AddPirateRelation(otherEmpire, _Galaxy.CurrentStarDate);
            }
            return pirateRelation;
        }

        public PirateRelation AddPirateRelation(Empire otherEmpire, long starDate)
        {
            return AddPirateRelation(otherEmpire, PirateRelationType.NotMet, starDate);
        }

        public PirateRelation AddPirateRelation(Empire otherEmpire, PirateRelationType relationType, long starDate)
        {
            PirateRelation pirateRelation = new PirateRelation(this, otherEmpire, relationType);
            pirateRelation.LastChangeDate = starDate;
            pirateRelation.LastProtectionFeePaymentDate = starDate;
            return PirateRelations.Add(pirateRelation);
        }

        public void CancelPirateDefendMissions(Empire otherEmpire, bool evaluationPenaltyIfPirate)
        {
            ShipGroupList shipGroupList = new ShipGroupList();
            PirateRelation pirateRelation = otherEmpire.ObtainPirateRelation(this);
            EmpireActivityList empireActivityList = PirateMissions.ResolveByTypeAndRequester(EmpireActivityType.Defend, otherEmpire);
            for (int i = 0; i < empireActivityList.Count; i++)
            {
                EmpireActivity empireActivity = empireActivityList[i];
                if (empireActivity == null || empireActivity.AssignedEmpire != this || empireActivity.BidTimeRemaining > 0)
                {
                    continue;
                }
                empireActivity.RequestingEmpire.PirateMissions.RemoveEquivalent(empireActivity);
                PirateMissions.RemoveEquivalent(empireActivity);
                if (empireActivity.AssignedEmpire != null && empireActivity.AssignedEmpire.PirateMissions != null)
                {
                    empireActivity.AssignedEmpire.PirateMissions.RemoveEquivalent(empireActivity);
                }
                if (_Galaxy.PirateMissions.ContainsEquivalent(empireActivity))
                {
                    _Galaxy.PirateMissions.RemoveEquivalent(empireActivity);
                }
                if (evaluationPenaltyIfPirate && PirateEmpireBaseHabitat != null)
                {
                    pirateRelation.EvaluationPirateMissionsFail -= 20f;
                }
                ShipGroupList shipGroupList2 = ShipGroups.ResolveFleetsWithWaitTarget(empireActivity.Target);
                for (int j = 0; j < shipGroupList2.Count; j++)
                {
                    ShipGroup shipGroup = shipGroupList2[j];
                    if (shipGroup != null && !shipGroupList.Contains(shipGroup))
                    {
                        shipGroupList.Add(shipGroup);
                    }
                }
                ShipGroupList shipGroupList3 = ShipGroups.ResolveFleetsWithAttackTarget(empireActivity.Target);
                for (int k = 0; k < shipGroupList3.Count; k++)
                {
                    ShipGroup shipGroup2 = shipGroupList3[k];
                    if (shipGroup2 != null && !shipGroupList.Contains(shipGroup2))
                    {
                        shipGroupList.Add(shipGroup2);
                    }
                }
            }
            for (int l = 0; l < _Galaxy.PirateMissions.Count; l++)
            {
                EmpireActivity empireActivity2 = _Galaxy.PirateMissions[l];
                if (empireActivity2 != null && empireActivity2.Type == EmpireActivityType.Defend && empireActivity2.RequestingEmpire == otherEmpire && empireActivity2.BidTimeRemaining > 0 && empireActivity2.AssignedEmpire == this)
                {
                    empireActivity2.AssignedEmpire = null;
                    empireActivity2.BidTimeRemaining += 10000L;
                }
            }
            if (shipGroupList != null && shipGroupList.Count > 0)
            {
                for (int m = 0; m < shipGroupList.Count; m++)
                {
                    shipGroupList[m]?.CompleteMission();
                }
            }
        }

        public void ChangePirateRelation(Empire otherEmpire, PirateRelationType relationType, long starDate)
        {
            ChangePirateRelation(otherEmpire, relationType, starDate, 0.0);
        }

        public void ChangePirateRelation(Empire otherEmpire, PirateRelationType relationType, long starDate, double monthlyFeeToThisEmpire)
        {
            if (otherEmpire == null)
            {
                return;
            }
            PirateRelation pirateRelation = ObtainPirateRelation(otherEmpire);
            PirateRelation pirateRelation2 = otherEmpire.ObtainPirateRelation(this);
            if (pirateRelation.Type == relationType)
            {
                return;
            }
            bool flag = false;
            if ((pirateRelation.Type == PirateRelationType.NotMet || pirateRelation.Type == PirateRelationType.None) && relationType == PirateRelationType.None)
            {
                flag = true;
            }
            if (pirateRelation.Type == PirateRelationType.Protection && relationType != PirateRelationType.Protection)
            {
                bool evaluationPenaltyIfPirate = false;
                if (PirateEmpireBaseHabitat != null)
                {
                    evaluationPenaltyIfPirate = true;
                }
                CancelPirateDefendMissions(otherEmpire, evaluationPenaltyIfPirate);
                otherEmpire.CancelPirateDefendMissions(this, evaluationPenaltyIfPirate: false);
            }
            pirateRelation.Type = relationType;
            pirateRelation.LastChangeDate = starDate;
            pirateRelation.LastOfferDate = starDate;
            pirateRelation.LastProtectionFeePaymentDate = starDate;
            pirateRelation.EvaluationLongRelationship = 0f;
            pirateRelation2.Type = relationType;
            pirateRelation2.LastChangeDate = starDate;
            pirateRelation2.LastOfferDate = starDate;
            pirateRelation2.LastProtectionFeePaymentDate = starDate;
            pirateRelation2.EvaluationLongRelationship = 0f;
            if (flag)
            {
                pirateRelation.LastOfferDate = 0L;
                pirateRelation.LastInfoDate = 0L;
                pirateRelation2.LastOfferDate = 0L;
                pirateRelation2.LastInfoDate = 0L;
            }
            pirateRelation.MonthlyProtectionFeeToThisEmpire = monthlyFeeToThisEmpire;
        }

        public void ChangePirateRelationThisSideOnly(Empire otherEmpire, PirateRelationType relationType, long starDate)
        {
            if (otherEmpire != null)
            {
                PirateRelation pirateRelation = ObtainPirateRelation(otherEmpire);
                if (pirateRelation.Type != relationType)
                {
                    pirateRelation.Type = relationType;
                    pirateRelation.LastChangeDate = starDate;
                    pirateRelation.LastOfferDate = starDate;
                    pirateRelation.LastProtectionFeePaymentDate = starDate;
                }
            }
        }

        public void ChangePirateEvaluation(Empire otherEmpire, float evaluationChangeAmount, PirateRelationEvaluationType evaluationType)
        {
            if (otherEmpire != null)
            {
                PirateRelation pirateRelation = ObtainPirateRelation(otherEmpire);
                switch (evaluationType)
                {
                    case PirateRelationEvaluationType.DetectedIntelligenceMissions:
                        pirateRelation.EvaluationDetectedIntelligenceMissions += evaluationChangeAmount;
                        break;
                    case PirateRelationEvaluationType.Gifts:
                        pirateRelation.EvaluationGifts += evaluationChangeAmount;
                        break;
                    case PirateRelationEvaluationType.OffenseOverRequests:
                        pirateRelation.EvaluationOffenseOverRequests += evaluationChangeAmount;
                        break;
                    case PirateRelationEvaluationType.PirateMissionsFail:
                        pirateRelation.EvaluationPirateMissionsFail += evaluationChangeAmount;
                        break;
                    case PirateRelationEvaluationType.PirateMissionsSucceed:
                        pirateRelation.EvaluationPirateMissionsSucceed += evaluationChangeAmount;
                        break;
                    case PirateRelationEvaluationType.ProtectionCancelled:
                        pirateRelation.EvaluationProtectionCancelled += evaluationChangeAmount;
                        break;
                    case PirateRelationEvaluationType.ShipAttacks:
                        pirateRelation.EvaluationShipAttacks += evaluationChangeAmount;
                        break;
                    case PirateRelationEvaluationType.CovetedColony:
                        pirateRelation.EvaluationCovetedColonies += evaluationChangeAmount;
                        break;
                    case PirateRelationEvaluationType.LongRelationship:
                        pirateRelation.EvaluationLongRelationship += evaluationChangeAmount;
                        break;
                    case PirateRelationEvaluationType.RaidsAgainstOurColonies:
                        pirateRelation.EvaluationRaidsAgainstOurColonies += evaluationChangeAmount;
                        break;
                }
            }
        }

        public bool ChangeDiplomaticRelation(DiplomaticRelation currentDiplomaticRelation, DiplomaticRelationType newDiplomaticRelationType)
        {
            return ChangeDiplomaticRelation(currentDiplomaticRelation, newDiplomaticRelationType, blockFlowonEffects: false);
        }

        public bool ChangeDiplomaticRelation(DiplomaticRelation currentDiplomaticRelation, DiplomaticRelationType newDiplomaticRelationType, bool blockFlowonEffects)
        {
            return ChangeDiplomaticRelation(currentDiplomaticRelation, newDiplomaticRelationType, blockFlowonEffects, locked: false);
        }

        public bool ChangeDiplomaticRelation(DiplomaticRelation currentDiplomaticRelation, DiplomaticRelationType newDiplomaticRelationType, bool blockFlowonEffects, bool locked)
        {
            return ChangeDiplomaticRelation(currentDiplomaticRelation, newDiplomaticRelationType, blockFlowonEffects, locked, string.Empty);
        }

        public bool ChangeDiplomaticRelation(DiplomaticRelation currentDiplomaticRelation, DiplomaticRelationType newDiplomaticRelationType, bool blockFlowonEffects, bool locked, string allianceName)
        {
            switch (newDiplomaticRelationType)
            {
                case DiplomaticRelationType.War:
                    {
                        CharacterList ambassadorsForEmpire5 = Characters.GetAmbassadorsForEmpire(currentDiplomaticRelation.OtherEmpire);
                        if (currentDiplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement || currentDiplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || currentDiplomaticRelation.Type == DiplomaticRelationType.Protectorate)
                        {
                            _Galaxy.DoCharacterEvent(CharacterEventType.TreatyBroken, currentDiplomaticRelation.OtherEmpire, ambassadorsForEmpire5, includeLeader: true, this);
                        }
                        _Galaxy.DoCharacterEvent(CharacterEventType.WarStarted, currentDiplomaticRelation.OtherEmpire, ambassadorsForEmpire5, includeLeader: true, this);
                        if (currentDiplomaticRelation != null && currentDiplomaticRelation.OtherEmpire != null)
                        {
                            CharacterList ambassadorsForEmpire6 = currentDiplomaticRelation.OtherEmpire.Characters.GetAmbassadorsForEmpire(this);
                            if (currentDiplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement || currentDiplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || currentDiplomaticRelation.Type == DiplomaticRelationType.Protectorate)
                            {
                                _Galaxy.DoCharacterEvent(CharacterEventType.TreatyBroken, this, ambassadorsForEmpire6, includeLeader: true, currentDiplomaticRelation.OtherEmpire);
                            }
                            _Galaxy.DoCharacterEvent(CharacterEventType.WarStarted, this, ambassadorsForEmpire6, includeLeader: true, currentDiplomaticRelation.OtherEmpire);
                        }
                        break;
                    }
                case DiplomaticRelationType.FreeTradeAgreement:
                case DiplomaticRelationType.MutualDefensePact:
                case DiplomaticRelationType.Protectorate:
                    {
                        CharacterList ambassadorsForEmpire3 = Characters.GetAmbassadorsForEmpire(currentDiplomaticRelation.OtherEmpire);
                        _Galaxy.DoCharacterEvent(CharacterEventType.TreatySigned, currentDiplomaticRelation.CloneLightWeight(newDiplomaticRelationType), ambassadorsForEmpire3, includeLeader: true, this);
                        if (currentDiplomaticRelation != null && currentDiplomaticRelation.OtherEmpire != null)
                        {
                            _Galaxy.ChanceNewAmbassador(this, newDiplomaticRelationType, currentDiplomaticRelation.OtherEmpire);
                            CharacterList ambassadorsForEmpire4 = currentDiplomaticRelation.OtherEmpire.Characters.GetAmbassadorsForEmpire(this);
                            DiplomaticRelation diplomaticRelation = currentDiplomaticRelation.OtherEmpire.ObtainDiplomaticRelation(this);
                            _Galaxy.DoCharacterEvent(CharacterEventType.TreatySigned, diplomaticRelation.CloneLightWeight(newDiplomaticRelationType), ambassadorsForEmpire4, includeLeader: true, currentDiplomaticRelation.OtherEmpire);
                        }
                        break;
                    }
                case DiplomaticRelationType.None:
                case DiplomaticRelationType.TradeSanctions:
                    if (currentDiplomaticRelation != null && currentDiplomaticRelation.OtherEmpire != null && (currentDiplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement || currentDiplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || currentDiplomaticRelation.Type == DiplomaticRelationType.Protectorate))
                    {
                        CharacterList ambassadorsForEmpire = Characters.GetAmbassadorsForEmpire(currentDiplomaticRelation.OtherEmpire);
                        _Galaxy.DoCharacterEvent(CharacterEventType.TreatyBroken, currentDiplomaticRelation.OtherEmpire, ambassadorsForEmpire, includeLeader: true, this);
                        CharacterList ambassadorsForEmpire2 = currentDiplomaticRelation.OtherEmpire.Characters.GetAmbassadorsForEmpire(this);
                        _Galaxy.DoCharacterEvent(CharacterEventType.TreatyBroken, this, ambassadorsForEmpire2, includeLeader: true, currentDiplomaticRelation.OtherEmpire);
                    }
                    break;
            }
            DiplomaticRelation diplomaticRelation2 = DiplomaticRelations[currentDiplomaticRelation.OtherEmpire];
            if (diplomaticRelation2 == null)
            {
                DiplomaticRelations.Add(currentDiplomaticRelation);
                diplomaticRelation2 = currentDiplomaticRelation;
            }
            DiplomaticRelation diplomaticRelation3 = diplomaticRelation2.OtherEmpire.DiplomaticRelations[diplomaticRelation2.ThisEmpire];
            if (diplomaticRelation3 == null)
            {
                DiplomaticRelation diplomaticRelation4 = new DiplomaticRelation(DiplomaticRelationType.NotMet, this, diplomaticRelation2.OtherEmpire, this, tradeRestrictedResources: false);
                diplomaticRelation2.OtherEmpire.DiplomaticRelations.Add(diplomaticRelation4);
                diplomaticRelation3 = diplomaticRelation4;
            }
            long currentStarDate = _Galaxy.CurrentStarDate;
            Counters.ProcessRelationChange(currentDiplomaticRelation, this, newDiplomaticRelationType, currentStarDate);
            diplomaticRelation2.OtherEmpire.Counters.ProcessRelationChange(diplomaticRelation3, this, newDiplomaticRelationType, currentStarDate);
            if (newDiplomaticRelationType == DiplomaticRelationType.MutualDefensePact || newDiplomaticRelationType == DiplomaticRelationType.Protectorate)
            {
                currentDiplomaticRelation.OtherEmpire.SetEmpireSharedVisibility(this);
                SetEmpireSharedVisibility(currentDiplomaticRelation.OtherEmpire);
                currentDiplomaticRelation.MilitaryRefuelingToOther = true;
                diplomaticRelation3.MilitaryRefuelingToOther = true;
            }
            else if ((diplomaticRelation2.Type == DiplomaticRelationType.MutualDefensePact || diplomaticRelation2.Type == DiplomaticRelationType.Protectorate) && newDiplomaticRelationType != DiplomaticRelationType.Protectorate && newDiplomaticRelationType != DiplomaticRelationType.MutualDefensePact)
            {
                currentDiplomaticRelation.OtherEmpire.ClearEmpireSharedVisibility(this);
                ClearEmpireSharedVisibility(currentDiplomaticRelation.OtherEmpire);
                currentDiplomaticRelation.MilitaryRefuelingToOther = false;
                diplomaticRelation3.MilitaryRefuelingToOther = false;
            }
            if (newDiplomaticRelationType == DiplomaticRelationType.FreeTradeAgreement)
            {
                if (!currentDiplomaticRelation.OtherEmpire.CheckWhetherKnowAnySystemsOfOtherEmpire(this))
                {
                    Habitat habitat = _Galaxy.FastFindNearestColony((int)currentDiplomaticRelation.OtherEmpire.Capital.Xpos, (int)currentDiplomaticRelation.OtherEmpire.Capital.Ypos, this, 0);
                    if (habitat != null)
                    {
                        SystemVisibilityStatus status = currentDiplomaticRelation.OtherEmpire.SystemVisibility[habitat.SystemIndex].Status;
                        if (status != SystemVisibilityStatus.Visible)
                        {
                            currentDiplomaticRelation.OtherEmpire.SetSystemVisibility(habitat, SystemVisibilityStatus.Explored);
                        }
                    }
                }
                if (!CheckWhetherKnowAnySystemsOfOtherEmpire(currentDiplomaticRelation.OtherEmpire))
                {
                    Habitat habitat2 = _Galaxy.FastFindNearestColony((int)Capital.Xpos, (int)Capital.Ypos, currentDiplomaticRelation.OtherEmpire, 0);
                    if (habitat2 != null)
                    {
                        SystemVisibilityStatus status2 = SystemVisibility[habitat2.SystemIndex].Status;
                        if (status2 != SystemVisibilityStatus.Visible)
                        {
                            SetSystemVisibility(habitat2, SystemVisibilityStatus.Explored);
                        }
                    }
                }
            }
            if (currentDiplomaticRelation.Type == DiplomaticRelationType.SubjugatedDominion)
            {
                int num = currentDiplomaticRelation.Initiator.EmpiresViewable.IndexOf(diplomaticRelation2.OtherEmpire);
                if (num >= 0)
                {
                    currentDiplomaticRelation.Initiator.EmpiresViewable.RemoveAt(num);
                    currentDiplomaticRelation.Initiator.EmpiresViewableExpiry.RemoveAt(num);
                }
            }
            if (newDiplomaticRelationType == DiplomaticRelationType.SubjugatedDominion)
            {
                int num2 = _EmpiresViewable.IndexOf(currentDiplomaticRelation.OtherEmpire);
                if (num2 >= 0)
                {
                    _EmpiresViewableExpiry[num2] = long.MaxValue;
                }
                else
                {
                    _EmpiresViewable.Add(currentDiplomaticRelation.OtherEmpire);
                    _EmpiresViewableExpiry.Add(long.MaxValue);
                }
                DiplomaticRelation diplomaticRelation5 = ProposedDiplomaticRelations[currentDiplomaticRelation.OtherEmpire];
                if (diplomaticRelation5 != null && diplomaticRelation5.Type == DiplomaticRelationType.None)
                {
                    ProposedDiplomaticRelations.Remove(diplomaticRelation5);
                }
            }
            if (newDiplomaticRelationType == DiplomaticRelationType.War)
            {
                EmpireEvaluation empireEvaluation = currentDiplomaticRelation.OtherEmpire.EmpireEvaluations[this];
                if (empireEvaluation != null)
                {
                    empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - (double)Galaxy.IncidentImpactWhenDeclareWar;
                }
                currentDiplomaticRelation.MilitaryRefuelingToOther = false;
                currentDiplomaticRelation.MiningRightsToOther = false;
                diplomaticRelation3.MilitaryRefuelingToOther = false;
                diplomaticRelation3.MiningRightsToOther = false;
                CivilityRating -= Galaxy.DeclareWarReputationImpact;
            }
            if (newDiplomaticRelationType == DiplomaticRelationType.TradeSanctions)
            {
                EmpireEvaluation empireEvaluation2 = currentDiplomaticRelation.OtherEmpire.EmpireEvaluations[this];
                if (empireEvaluation2 != null)
                {
                    empireEvaluation2.IncidentEvaluation = empireEvaluation2.IncidentEvaluationRaw - 15.0;
                }
                currentDiplomaticRelation.MilitaryRefuelingToOther = false;
                currentDiplomaticRelation.MiningRightsToOther = false;
                diplomaticRelation3.MilitaryRefuelingToOther = false;
                diplomaticRelation3.MiningRightsToOther = false;
            }
            if (newDiplomaticRelationType != DiplomaticRelationType.FreeTradeAgreement && newDiplomaticRelationType != DiplomaticRelationType.MutualDefensePact && newDiplomaticRelationType != DiplomaticRelationType.Protectorate)
            {
                diplomaticRelation2.TradeBonus = 0.0;
            }
            diplomaticRelation2.Type = newDiplomaticRelationType;
            diplomaticRelation2.Initiator = this;
            diplomaticRelation2.WarDamageBuiltObject = 0;
            diplomaticRelation2.WarDamageColony = 0;
            diplomaticRelation2.StartDateOfLastChange = _Galaxy.CurrentStarDate;
            diplomaticRelation2.LastDiplomacyTradeOfferDate = _Galaxy.CurrentStarDate;
            diplomaticRelation2.AllianceName = allianceName;
            diplomaticRelation2.Locked = locked;
            diplomaticRelation3.Locked = locked;
            if (!blockFlowonEffects && newDiplomaticRelationType == DiplomaticRelationType.War)
            {
                for (int i = 0; i < diplomaticRelation2.OtherEmpire.DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation6 = diplomaticRelation2.OtherEmpire.DiplomaticRelations[i];
                    if ((diplomaticRelation6.Type != DiplomaticRelationType.MutualDefensePact && (diplomaticRelation6.Type != DiplomaticRelationType.Protectorate || diplomaticRelation6.Initiator == diplomaticRelation2.OtherEmpire)) || diplomaticRelation6.OtherEmpire == this)
                    {
                        continue;
                    }
                    if (diplomaticRelation6.OtherEmpire != null)
                    {
                        DiplomaticRelation diplomaticRelation7 = diplomaticRelation6.OtherEmpire.ObtainDiplomaticRelation(this);
                        if (diplomaticRelation7 != null && diplomaticRelation7.Type == DiplomaticRelationType.War)
                        {
                            continue;
                        }
                    }
                    if (diplomaticRelation6.OtherEmpire == _Galaxy.PlayerEmpire)
                    {
                        diplomaticRelation2.OtherEmpire.SendMessageToEmpire(diplomaticRelation6.OtherEmpire, EmpireMessageType.RequestHonorMutualDefense, this, string.Format(TextResolver.GetText("We are under attack from the EMPIRE"), Name));
                        continue;
                    }
                    bool flag = true;
                    DiplomaticRelation diplomaticRelation8 = diplomaticRelation6.OtherEmpire.ObtainDiplomaticRelation(this);
                    if (diplomaticRelation8.Type == DiplomaticRelationType.MutualDefensePact || (diplomaticRelation8.Type == DiplomaticRelationType.Protectorate && diplomaticRelation8.Initiator == this))
                    {
                        flag = false;
                    }
                    else if (diplomaticRelation8.Locked && diplomaticRelation8.Type != DiplomaticRelationType.War)
                    {
                        flag = false;
                    }
                    if (!flag)
                    {
                        continue;
                    }
                    double num3 = (double)diplomaticRelation6.OtherEmpire.WeightedMilitaryPotency / (double)WeightedMilitaryPotency;
                    double num4 = (double)diplomaticRelation6.OtherEmpire.DominantRace.LoyaltyLevel / 100.0;
                    EmpireEvaluation empireEvaluation3 = diplomaticRelation6.OtherEmpire.EmpireEvaluations[this];
                    double num5 = 1.0;
                    if (empireEvaluation3 != null)
                    {
                        num5 = 1.0 + (double)empireEvaluation3.OverallAttitude / 100.0;
                        num5 = Math.Max(0.5, Math.Min(num5, 1.5));
                    }
                    double num6 = num3 * num4 * num4 * num5 * num5;
                    if (num6 > 0.3 + Galaxy.Rnd.NextDouble() * 0.1)
                    {
                        EmpireEvaluation empireEvaluation4 = diplomaticRelation2.OtherEmpire.EmpireEvaluations[diplomaticRelation6.OtherEmpire];
                        if (empireEvaluation4 != null)
                        {
                            empireEvaluation4.IncidentEvaluation = empireEvaluation4.IncidentEvaluationRaw + 15.0;
                        }
                        diplomaticRelation6.OtherEmpire.CivilityRating += 6.0;
                        diplomaticRelation6.OtherEmpire.DeclareWar(this);
                    }
                    else
                    {
                        EmpireEvaluation empireEvaluation5 = diplomaticRelation2.OtherEmpire.EmpireEvaluations[diplomaticRelation6.OtherEmpire];
                        if (empireEvaluation5 != null)
                        {
                            empireEvaluation5.IncidentEvaluation = empireEvaluation5.IncidentEvaluationRaw - 22.0;
                        }
                        diplomaticRelation6.OtherEmpire.CivilityRating -= 6.0;
                    }
                }
            }
            if (newDiplomaticRelationType != DiplomaticRelationType.FreeTradeAgreement && newDiplomaticRelationType != DiplomaticRelationType.MutualDefensePact && newDiplomaticRelationType != DiplomaticRelationType.Protectorate)
            {
                diplomaticRelation3.TradeBonus = 0.0;
            }
            diplomaticRelation3.Type = newDiplomaticRelationType;
            diplomaticRelation3.WarDamageBuiltObject = 0;
            diplomaticRelation3.WarDamageColony = 0;
            diplomaticRelation3.Initiator = this;
            diplomaticRelation3.StartDateOfLastChange = _Galaxy.CurrentStarDate;
            diplomaticRelation3.LastDiplomacyTradeOfferDate = _Galaxy.CurrentStarDate;
            diplomaticRelation3.AllianceName = allianceName;
            short matchingGameEventIdDiplomaticRelationChange = _Galaxy.GetMatchingGameEventIdDiplomaticRelationChange(currentDiplomaticRelation.ThisEmpire, currentDiplomaticRelation.OtherEmpire, newDiplomaticRelationType);
            _Galaxy.CheckTriggerEvent(matchingGameEventIdDiplomaticRelationChange, this, EventTriggerType.DiplomaticRelationChange, null);
            matchingGameEventIdDiplomaticRelationChange = _Galaxy.GetMatchingGameEventIdDiplomaticRelationChange(currentDiplomaticRelation.OtherEmpire, currentDiplomaticRelation.ThisEmpire, newDiplomaticRelationType);
            _Galaxy.CheckTriggerEvent(matchingGameEventIdDiplomaticRelationChange, this, EventTriggerType.DiplomaticRelationChange, null);
            return true;
        }

        public Empire DetermineVictorInWar(DiplomaticRelation diplomaticRelation, out double winningRatio, out Empire loser, out int loserRawDamageBuiltObject, out int loserRawDamageColony, out int winnerRawDamageBuiltObject, out int winnerRawDamageColony)
        {
            Empire empire = null;
            loser = null;
            winningRatio = 1.0;
            loserRawDamageBuiltObject = 0;
            loserRawDamageColony = 0;
            winnerRawDamageBuiltObject = 0;
            winnerRawDamageColony = 0;
            DiplomaticRelation diplomaticRelation2 = diplomaticRelation.OtherEmpire.DiplomaticRelations[diplomaticRelation.ThisEmpire];
            if (diplomaticRelation.WarDamageTotal > diplomaticRelation2.WarDamageTotal)
            {
                empire = diplomaticRelation.OtherEmpire;
                loser = diplomaticRelation.ThisEmpire;
                winningRatio = (diplomaticRelation.WarDamageTotal + 1) / (diplomaticRelation2.WarDamageTotal + 1);
                loserRawDamageBuiltObject = diplomaticRelation.WarDamageBuiltObject;
                loserRawDamageColony = diplomaticRelation.WarDamageColony;
                winnerRawDamageBuiltObject = diplomaticRelation2.WarDamageBuiltObject;
                winnerRawDamageColony = diplomaticRelation2.WarDamageColony;
            }
            else
            {
                empire = diplomaticRelation.ThisEmpire;
                loser = diplomaticRelation.OtherEmpire;
                winningRatio = (diplomaticRelation2.WarDamageTotal + 1) / (diplomaticRelation.WarDamageTotal + 1);
                loserRawDamageBuiltObject = diplomaticRelation2.WarDamageBuiltObject;
                loserRawDamageColony = diplomaticRelation2.WarDamageColony;
                winnerRawDamageBuiltObject = diplomaticRelation.WarDamageBuiltObject;
                winnerRawDamageColony = diplomaticRelation.WarDamageColony;
            }
            return empire;
        }

        public bool DetermineSubjugationOfLoserInWar(Empire winnerEmpire, Empire loserEmpire, double winningRatio, int winnerStrength, int loserStrength)
        {
            if (winningRatio > 3.0)
            {
                double num = Math.Pow((double)winnerEmpire.DominantRace.AggressionLevel / 100.0, 2.0);
                double num2 = Math.Pow((double)loserEmpire.DominantRace.AggressionLevel / 100.0, 2.0);
                double num3 = (double)winnerStrength * num;
                double num4 = (double)loserStrength * num2 * 3.0;
                if (num3 > num4)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckWhetherWarDamageExceedsLimit(Empire empire, int damageBuiltObject, int damageColony)
        {
            bool result = false;
            int builtObjectWarValue = 0;
            int colonyWarValue = 0;
            _Galaxy.CalculateEmpireWarValue(empire, out builtObjectWarValue, out colonyWarValue);
            double num = Math.Pow(1.0 + (double)(empire.DominantRace.AggressionLevel - empire.DominantRace.CautionLevel) / 100.0, 2.0);
            int num2 = (int)((double)(builtObjectWarValue + damageBuiltObject) * Galaxy.AcceptableWarValueLossesBuiltObject * num);
            int num3 = (int)((double)(colonyWarValue + damageColony) * Galaxy.AcceptableWarValueLossesColony * num);
            if (damageBuiltObject > num2 || damageColony > num3)
            {
                result = true;
            }
            return result;
        }

        private double CalculateSupportableForceFunds()
        {
            double num = CalculateAccurateAnnualIncome();
            double val = StateMoney / Galaxy.AllowableYearsMaintenanceFromCashOnHand;
            return Math.Max(0.0, num + Math.Max(0.0, val));
        }

        private void CleanupInvalidShips()
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            BuiltObjectList builtObjectList2 = new BuiltObjectList();
            builtObjectList2.AddRange(BuiltObjects);
            builtObjectList2.AddRange(PrivateBuiltObjects);
            for (int i = 0; i < builtObjectList2.Count; i++)
            {
                BuiltObject builtObject = builtObjectList2[i];
                if (builtObject == null || builtObject.InView || !builtObject.IsAutoControlled)
                {
                    continue;
                }
                if (builtObject.HasBeenDestroyed)
                {
                    builtObjectList.Add(builtObject);
                }
                if (builtObject.Owner != null && builtObject.Owner == _Galaxy.PlayerEmpire && !builtObject.HasBeenDestroyed)
                {
                    continue;
                }
                if (!builtObject.IsFunctional && builtObject.BuiltAt == null && builtObject.Role != BuiltObjectRole.Base)
                {
                    builtObjectList.Add(builtObject);
                }
                if (builtObject.Role != BuiltObjectRole.Base && builtObject.BuiltAt == null && builtObject.TopSpeed <= 0)
                {
                    builtObjectList.Add(builtObject);
                }
                if (builtObject.DockingBays != null && builtObject.DockingBays.Count > 0)
                {
                    foreach (DockingBay dockingBay in builtObject.DockingBays)
                    {
                        if (dockingBay.DockedShip != null && dockingBay.DockedShip.DockedAt == null)
                        {
                            dockingBay.DockedShip = null;
                        }
                    }
                    if (builtObject.DockingBayWaitQueue != null)
                    {
                        foreach (BuiltObject item in builtObject.DockingBayWaitQueue)
                        {
                            if (item.Mission != null && item.Mission.Type != 0)
                            {
                                if (!item.IsFunctional && !builtObjectList.Contains(item))
                                {
                                    builtObjectList.Add(item);
                                }
                                if (item.TopSpeed <= 0 && !builtObjectList.Contains(item))
                                {
                                    builtObjectList.Add(item);
                                }
                            }
                            else
                            {
                                item.ClearPreviousMissionRequirements();
                            }
                        }
                    }
                }
                if (builtObject.ConstructionQueue == null || builtObject.ConstructionQueue.ConstructionYards.Count <= 0)
                {
                    continue;
                }
                foreach (ConstructionYard constructionYard in builtObject.ConstructionQueue.ConstructionYards)
                {
                    if (constructionYard.ShipUnderConstruction != null && constructionYard.ShipUnderConstruction.BuiltAt == null)
                    {
                        constructionYard.ShipUnderConstruction = null;
                    }
                }
            }
            foreach (BuiltObject item2 in builtObjectList)
            {
                item2.CompleteTeardown(_Galaxy, removeFromEmpire: true);
            }
        }

        private bool CheckWhetherHaveEnoughFleetsForWar(int empiresAtWarWith)
        {
            bool result = false;
            if (ShipGroups != null)
            {
                switch (empiresAtWarWith)
                {
                    case 0:
                        if (ShipGroups.Count > 0)
                        {
                            result = true;
                        }
                        break;
                    case 1:
                        if (ShipGroups.Count > 2)
                        {
                            result = true;
                        }
                        break;
                }
            }
            return result;
        }

        private bool DetermineWhetherWantToEmancipate(DiplomaticRelation relation, int overallAttitude)
        {
            int num = (int)((double)(DominantRace.FriendlinessLevel - DominantRace.AggressionLevel) / 5.0);
            int num2 = 10 - num;
            if (overallAttitude > num2)
            {
                switch (relation.Strategy)
                {
                    case DiplomaticStrategy.Conquer:
                    case DiplomaticStrategy.Defend:
                    case DiplomaticStrategy.Undermine:
                    case DiplomaticStrategy.DefendUndermine:
                    case DiplomaticStrategy.Punish:
                        return false;
                    default:
                        return true;
                }
            }
            return false;
        }

        private DiplomaticRelationType ResolveDesiredDiplomaticRelationType(DiplomaticRelation currentDiplomaticRelation, int overallAttitude, int intelligenceLevel, int friendlinessLevel, int loyaltyLevel, int aggressionLevel, double warWeariness, double galaxyIntoleranceLevel)
        {
            return ResolveDesiredDiplomaticRelationType(currentDiplomaticRelation, overallAttitude, intelligenceLevel, friendlinessLevel, loyaltyLevel, aggressionLevel, warWeariness, galaxyIntoleranceLevel, 1.0);
        }

        private DiplomaticRelationType ResolveDesiredDiplomaticRelationType(DiplomaticRelation currentDiplomaticRelation, int overallAttitude, int intelligenceLevel, int friendlinessLevel, int loyaltyLevel, int aggressionLevel, double warWeariness, double galaxyIntoleranceLevel, double diplomacyFactor)
        {
            if (currentDiplomaticRelation.Locked)
            {
                return currentDiplomaticRelation.Type;
            }
            DiplomaticRelationType diplomaticRelationType = currentDiplomaticRelation.Type;
            if (overallAttitude >= 0)
            {
                int num = (int)Math.Max(0.0, ((double)friendlinessLevel - 95.0) / 1.8);
                int num2 = Math.Max(0, (int)(10.0 * galaxyIntoleranceLevel));
                num += num2;
                overallAttitude += num;
            }
            if (overallAttitude > 0)
            {
                overallAttitude = (int)((double)overallAttitude * diplomacyFactor);
            }
            else if (overallAttitude < 0)
            {
                overallAttitude = (int)((double)overallAttitude / diplomacyFactor);
            }
            ResolveTypicalAttitudeLevel(currentDiplomaticRelation.Type, out var lowerLevel, out var upperLevel);
            if (currentDiplomaticRelation.Type == DiplomaticRelationType.War)
            {
                overallAttitude += (int)warWeariness;
            }
            if (currentDiplomaticRelation.Type == DiplomaticRelationType.SubjugatedDominion && currentDiplomaticRelation.Initiator == this)
            {
                int overallAttitude2 = overallAttitude - 20;
                if (DetermineWhetherWantToEmancipate(currentDiplomaticRelation, overallAttitude2))
                {
                    return DiplomaticRelationType.None;
                }
                return DiplomaticRelationType.SubjugatedDominion;
            }
            if (currentDiplomaticRelation.Type == DiplomaticRelationType.War)
            {
                ResolveTypicalAttitudeLevel(DiplomaticRelationType.None, out var lowerLevel2, out var _);
                int num3 = lowerLevel2 + 15;
                if (overallAttitude >= num3)
                {
                    return DiplomaticRelationType.None;
                }
                double winningRatio = 0.0;
                int loserRawDamageBuiltObject = 0;
                int loserRawDamageColony = 0;
                int winnerRawDamageBuiltObject = 0;
                int winnerRawDamageColony = 0;
                Empire loser = null;
                Empire empire = DetermineVictorInWar(currentDiplomaticRelation, out winningRatio, out loser, out loserRawDamageBuiltObject, out loserRawDamageColony, out winnerRawDamageBuiltObject, out winnerRawDamageColony);
                if (empire != null)
                {
                    double num4 = (double)DominantRace.AggressionLevel / 100.0;
                    int num5 = CountEmpiresWeDeclaredWarOn() + CountEmpiresWhoDeclaredWarOnUs();
                    double num6 = 20.0 * num4 * num4;
                    if (empire == this)
                    {
                        if (num5 > 2 && Galaxy.Rnd.Next(0, 3) == 1)
                        {
                            return DiplomaticRelationType.None;
                        }
                        if (CheckWhetherWarDamageExceedsLimit(empire, winnerRawDamageBuiltObject, winnerRawDamageColony) || warWeariness > num6)
                        {
                            if (DetermineWhetherWantToOfferSubjugation(this) && DetermineSubjugationOfLoserInWar(empire, loser, winningRatio, empire.MilitaryPotency, loser.MilitaryPotency))
                            {
                                return DiplomaticRelationType.SubjugatedDominion;
                            }
                            return DiplomaticRelationType.None;
                        }
                        if (CheckWhetherWarDamageExceedsLimit(loser, loserRawDamageBuiltObject, loserRawDamageColony))
                        {
                            if (DetermineWhetherWantToOfferSubjugation(this) && DetermineSubjugationOfLoserInWar(empire, loser, winningRatio, empire.MilitaryPotency, loser.MilitaryPotency))
                            {
                                return DiplomaticRelationType.SubjugatedDominion;
                            }
                            if (warWeariness > num6)
                            {
                                return DiplomaticRelationType.None;
                            }
                        }
                        else if (warWeariness > num6)
                        {
                            return DiplomaticRelationType.None;
                        }
                    }
                    else
                    {
                        if (num5 > 1 && Galaxy.Rnd.Next(0, 2) == 1)
                        {
                            return DiplomaticRelationType.None;
                        }
                        if (CheckWhetherWarDamageExceedsLimit(loser, loserRawDamageBuiltObject, loserRawDamageColony))
                        {
                            return DiplomaticRelationType.None;
                        }
                        if (warWeariness > num6)
                        {
                            return DiplomaticRelationType.None;
                        }
                    }
                }
            }
            if (currentDiplomaticRelation.Type == DiplomaticRelationType.TradeSanctions)
            {
                double num7 = AnnualStateMaintenance + AnnualTroopMaintenance + AnnualSubjugationTribute + ThisYearsStateFuelCosts + AnnualPirateProtection + AnnualFacilityMaintenance;
                double num8 = CalculateSupportableForceFunds();
                if (num7 > num8)
                {
                    return DiplomaticRelationType.None;
                }
            }
            int num9 = overallAttitude;
            if (overallAttitude >= lowerLevel)
            {
                diplomaticRelationType = ((overallAttitude <= upperLevel) ? currentDiplomaticRelation.Type : ResolveTypicalDiplomaticRelationType(overallAttitude));
            }
            else
            {
                int num10 = 0;
                EmpireEvaluation empireEvaluation = currentDiplomaticRelation.OtherEmpire.ObtainEmpireEvaluation(this);
                if (empireEvaluation.OverallAttitude >= 4)
                {
                    num10 = (int)((double)empireEvaluation.OverallAttitude * 0.75);
                }
                if (currentDiplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || currentDiplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement || (currentDiplomaticRelation.Type == DiplomaticRelationType.Protectorate && currentDiplomaticRelation.Initiator == this))
                {
                    num9 += Math.Max(0, (loyaltyLevel - 100) / 3);
                }
                else if (currentDiplomaticRelation.Type == DiplomaticRelationType.None)
                {
                    num9 -= Math.Min(0, aggressionLevel - 100) / 3;
                }
                if (num9 + num10 < lowerLevel)
                {
                    int val = (intelligenceLevel - 100) / 2;
                    val = Math.Max(0, Math.Min(val, 8));
                    ResolveTypicalAttitudeLevel(DiplomaticRelationType.None, out var lowerLevel3, out var _);
                    lowerLevel3 /= 3;
                    diplomaticRelationType = ((currentDiplomaticRelation.Type != DiplomaticRelationType.MutualDefensePact && currentDiplomaticRelation.Type != DiplomaticRelationType.Protectorate) ? ((currentDiplomaticRelation.Type != DiplomaticRelationType.FreeTradeAgreement) ? ResolveTypicalDiplomaticRelationType(overallAttitude) : ((num9 + val >= lowerLevel3) ? currentDiplomaticRelation.Type : ResolveTypicalDiplomaticRelationType(overallAttitude))) : ((num9 + val >= 0) ? currentDiplomaticRelation.Type : ResolveTypicalDiplomaticRelationType(overallAttitude)));
                }
            }
            if (diplomaticRelationType == DiplomaticRelationType.TradeSanctions)
            {
                double num11 = AnnualStateMaintenance + AnnualTroopMaintenance + AnnualSubjugationTribute + ThisYearsStateFuelCosts + AnnualPirateProtection + AnnualFacilityMaintenance;
                double num12 = CalculateSupportableForceFunds();
                if (num11 > num12)
                {
                    return DiplomaticRelationType.None;
                }
            }
            if (_Galaxy.StoryReturnOfTheShakturiEnabled && diplomaticRelationType == DiplomaticRelationType.War)
            {
                string text = string.Empty;
                if (currentDiplomaticRelation != null && currentDiplomaticRelation.ThisEmpire != null && currentDiplomaticRelation.ThisEmpire.DominantRace != null)
                {
                    text = currentDiplomaticRelation.ThisEmpire.DominantRace.Name;
                }
                if (text.ToLower(CultureInfo.InvariantCulture) == "mechanoid" && !currentDiplomaticRelation.ThisEmpire.DominantRace.Expanding && currentDiplomaticRelation.ThisEmpire.Reclusive)
                {
                    return DiplomaticRelationType.TradeSanctions;
                }
            }
            if (diplomaticRelationType == DiplomaticRelationType.War && overallAttitude > -80)
            {
                double num13 = (double)aggressionLevel * (double)aggressionLevel * _Galaxy.AggressionLevel;
                int val2 = Math.Max(1, (int)num13);
                val2 = Math.Min(val2, 2);
                int num14 = CountEmpiresWeDeclaredWarOn() + CountEmpiresWhoDeclaredWarOnUs();
                if (num14 >= val2)
                {
                    if (currentDiplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement || currentDiplomaticRelation.Type == DiplomaticRelationType.Protectorate || currentDiplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact)
                    {
                        return DiplomaticRelationType.None;
                    }
                    if (currentDiplomaticRelation.Type == DiplomaticRelationType.SubjugatedDominion)
                    {
                        return DiplomaticRelationType.SubjugatedDominion;
                    }
                    return DiplomaticRelationType.TradeSanctions;
                }
                if (!CheckWhetherHaveEnoughFleetsForWar(num14))
                {
                    if (currentDiplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement || currentDiplomaticRelation.Type == DiplomaticRelationType.Protectorate || currentDiplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact)
                    {
                        return DiplomaticRelationType.None;
                    }
                    if (currentDiplomaticRelation.Type == DiplomaticRelationType.SubjugatedDominion)
                    {
                        return DiplomaticRelationType.SubjugatedDominion;
                    }
                    return DiplomaticRelationType.TradeSanctions;
                }
                EmpireEvaluation empireEvaluation2 = ObtainEmpireEvaluation(currentDiplomaticRelation.OtherEmpire);
                if (empireEvaluation2.ReputationWeighted < 0.0)
                {
                    double num15 = empireEvaluation2.ReputationWeighted / (double)overallAttitude;
                    if (num15 > 0.5)
                    {
                        if (currentDiplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement || currentDiplomaticRelation.Type == DiplomaticRelationType.Protectorate || currentDiplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact)
                        {
                            return DiplomaticRelationType.None;
                        }
                        if (currentDiplomaticRelation.Type == DiplomaticRelationType.SubjugatedDominion)
                        {
                            return DiplomaticRelationType.SubjugatedDominion;
                        }
                        return DiplomaticRelationType.TradeSanctions;
                    }
                }
            }
            return diplomaticRelationType;
        }

        public int CalculateTotalMobileFirepowerAtWarWithUs(Empire empireToExclude)
        {
            int num = 0;
            if (DiplomaticRelations != null)
            {
                for (int i = 0; i < DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                    if (diplomaticRelation != null && diplomaticRelation.OtherEmpire != null && diplomaticRelation.OtherEmpire.BuiltObjects != null && diplomaticRelation.Type == DiplomaticRelationType.War && (empireToExclude == null || diplomaticRelation.OtherEmpire != empireToExclude))
                    {
                        num += diplomaticRelation.OtherEmpire.BuiltObjects.TotalMobileMilitaryFirepower();
                    }
                }
            }
            return num;
        }

        public bool DetermineWhetherWantToOfferSubjugation(Empire empire)
        {
            bool result = false;
            double num = Math.Sqrt(Policy.SubjugationPriority);
            int num2 = (int)(115.0 * num + 15.0 * Galaxy.Rnd.NextDouble());
            int num3 = (int)(100.0 / num + 15.0 * Galaxy.Rnd.NextDouble());
            if (empire.DominantRace.AggressionLevel < num2 && empire.DominantRace.IntelligenceLevel > num3)
            {
                result = true;
            }
            return result;
        }

        private DiplomaticRelationType ResolveTypicalDiplomaticRelationType(int overallAttitude)
        {
            DiplomaticRelationType result = DiplomaticRelationType.None;
            if (overallAttitude <= -50)
            {
                result = DiplomaticRelationType.War;
            }
            if (overallAttitude >= -49 && overallAttitude <= -25)
            {
                result = DiplomaticRelationType.TradeSanctions;
            }
            if (overallAttitude >= -24 && overallAttitude <= 24)
            {
                result = DiplomaticRelationType.None;
            }
            if (overallAttitude >= 25 && overallAttitude <= 49)
            {
                result = DiplomaticRelationType.FreeTradeAgreement;
            }
            if (overallAttitude >= 50)
            {
                result = DiplomaticRelationType.MutualDefensePact;
            }
            return result;
        }

        public void ResolveTypicalAttitudeLevel(DiplomaticRelationType currentDiplomaticRelationType, out int lowerLevel, out int upperLevel)
        {
            lowerLevel = -2147483647;
            upperLevel = int.MaxValue;
            switch (currentDiplomaticRelationType)
            {
                case DiplomaticRelationType.War:
                    lowerLevel = int.MinValue;
                    upperLevel = -50;
                    break;
                case DiplomaticRelationType.TradeSanctions:
                    lowerLevel = -49;
                    upperLevel = -25;
                    break;
                case DiplomaticRelationType.Truce:
                    lowerLevel = -49;
                    upperLevel = -25;
                    break;
                case DiplomaticRelationType.SubjugatedDominion:
                    lowerLevel = -49;
                    upperLevel = 0;
                    break;
                case DiplomaticRelationType.None:
                    lowerLevel = -24;
                    upperLevel = 24;
                    break;
                case DiplomaticRelationType.FreeTradeAgreement:
                    lowerLevel = 25;
                    upperLevel = 49;
                    break;
                case DiplomaticRelationType.MutualDefensePact:
                    lowerLevel = 50;
                    upperLevel = int.MaxValue;
                    break;
                case DiplomaticRelationType.Protectorate:
                    lowerLevel = 50;
                    upperLevel = int.MaxValue;
                    break;
            }
        }

        public bool CheckAssignFleetWaitAndAttackMission(ShipGroup fleet, ref BuiltObjectMissionType missionType, object target, BuiltObjectMissionPriority priority)
        {
            if (missionType == BuiltObjectMissionType.WaitAndAttack || missionType == BuiltObjectMissionType.WaitAndBombard)
            {
                double targetX = 0.0;
                double targetY = 0.0;
                int targetGatherRange = 0;
                if (fleet.CheckNeedRefuelBeforeAttack(target))
                {
                    if (fleet.Empire.AssignFleetWaypointAttackMission(fleet, target, missionType))
                    {
                        return true;
                    }
                }
                else if (fleet.CheckNeedGatherBeforeAttack(target, out targetGatherRange, out targetX, out targetY))
                {
                    double gatherX = 0.0;
                    double gatherY = 0.0;
                    fleet.IdentifyFleetGatherLocation(targetX, targetY, targetGatherRange, out gatherX, out gatherY);
                    if (fleet.Empire.AssignFleetGatherAttackMission(fleet, target, gatherX, gatherY, missionType))
                    {
                        return true;
                    }
                }
                else if (missionType == BuiltObjectMissionType.WaitAndAttack)
                {
                    missionType = BuiltObjectMissionType.Attack;
                }
                else if (missionType == BuiltObjectMissionType.WaitAndBombard)
                {
                    missionType = BuiltObjectMissionType.Bombard;
                }
            }
            if (missionType == BuiltObjectMissionType.WaitAndAttack)
            {
                missionType = BuiltObjectMissionType.Attack;
            }
            else if (missionType == BuiltObjectMissionType.WaitAndBombard)
            {
                missionType = BuiltObjectMissionType.Bombard;
            }
            return false;
        }

        public bool AssignFleetGatherAttackMission(ShipGroup fleet, object target, double gatherX, double gatherY, BuiltObjectMissionType missionType)
        {
            bool result = false;
            if (target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)target;
                _ = builtObject.Xpos;
                _ = builtObject.Ypos;
            }
            else if (target is Habitat)
            {
                Habitat habitat = (Habitat)target;
                _ = habitat.Xpos;
                _ = habitat.Ypos;
            }
            else if (target is Creature)
            {
                Creature creature = (Creature)target;
                _ = creature.Xpos;
                _ = creature.Ypos;
            }
            else if (target is ShipGroup)
            {
                ShipGroup shipGroup = (ShipGroup)target;
                if (shipGroup.LeadShip != null)
                {
                    _ = shipGroup.LeadShip.Xpos;
                    _ = shipGroup.LeadShip.Ypos;
                }
            }
            long num = 30000L;
            GalaxyLocationList galaxyLocationList = _Galaxy.DetermineGalaxyLocationsAtPoint(gatherX, gatherY, GalaxyLocationType.NebulaCloud);
            if (galaxyLocationList.Count > 0 && galaxyLocationList[0].Effect == GalaxyLocationEffectType.MovementSlowed)
            {
                num = (long)((double)num * 1.333);
            }
            long starDate = DetermineLatestArrivalAtDestination(fleet, gatherX, gatherY) + num;
            if (target is Habitat)
            {
                Habitat target2 = (Habitat)target;
                fleet.ForceCompleteMission();
                switch (missionType)
                {
                    case BuiltObjectMissionType.Attack:
                    case BuiltObjectMissionType.WaitAndAttack:
                        result = fleet.AssignMission(BuiltObjectMissionType.WaitAndAttack, target2, null, null, null, gatherX, gatherY, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        break;
                    case BuiltObjectMissionType.WaitAndBombard:
                    case BuiltObjectMissionType.Bombard:
                        result = fleet.AssignMission(BuiltObjectMissionType.WaitAndBombard, target2, null, null, null, gatherX, gatherY, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        break;
                }
            }
            else if (target is BuiltObject)
            {
                BuiltObject target3 = (BuiltObject)target;
                fleet.ForceCompleteMission();
                switch (missionType)
                {
                    case BuiltObjectMissionType.Attack:
                    case BuiltObjectMissionType.WaitAndAttack:
                        result = fleet.AssignMission(BuiltObjectMissionType.WaitAndAttack, target3, null, null, null, gatherX, gatherY, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        break;
                    case BuiltObjectMissionType.WaitAndBombard:
                    case BuiltObjectMissionType.Bombard:
                        result = fleet.AssignMission(BuiltObjectMissionType.WaitAndBombard, target3, null, null, null, gatherX, gatherY, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        break;
                }
            }
            else if (target is ShipGroup)
            {
                ShipGroup shipGroup2 = (ShipGroup)target;
                if (shipGroup2.LeadShip.CurrentSpeed < (float)shipGroup2.LeadShip.WarpSpeed && !shipGroup2.LeadShip.HyperjumpPrepare)
                {
                    fleet.ForceCompleteMission();
                    switch (missionType)
                    {
                        case BuiltObjectMissionType.Attack:
                        case BuiltObjectMissionType.WaitAndAttack:
                            result = fleet.AssignMission(BuiltObjectMissionType.WaitAndAttack, shipGroup2, null, null, null, gatherX, gatherY, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            break;
                        case BuiltObjectMissionType.WaitAndBombard:
                        case BuiltObjectMissionType.Bombard:
                            result = fleet.AssignMission(BuiltObjectMissionType.WaitAndBombard, shipGroup2, null, null, null, gatherX, gatherY, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            break;
                    }
                }
            }
            return result;
        }

        public bool AssignFleetWaypointAttackMission(ShipGroup fleet, object target, BuiltObjectMissionType missionType)
        {
            bool result = false;
            double x = 0.0;
            double y = 0.0;
            Empire empireToExclude = null;
            if (target != null)
            {
                if (target is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)target;
                    x = builtObject.Xpos;
                    y = builtObject.Ypos;
                    empireToExclude = builtObject.Empire;
                }
                else if (target is Habitat)
                {
                    Habitat habitat = (Habitat)target;
                    x = habitat.Xpos;
                    y = habitat.Ypos;
                    empireToExclude = habitat.Empire;
                }
                else if (target is Creature)
                {
                    Creature creature = (Creature)target;
                    x = creature.Xpos;
                    y = creature.Ypos;
                }
                else if (target is ShipGroup)
                {
                    ShipGroup shipGroup = (ShipGroup)target;
                    if (shipGroup.LeadShip != null)
                    {
                        x = shipGroup.LeadShip.Xpos;
                        y = shipGroup.LeadShip.Ypos;
                        empireToExclude = shipGroup.Empire;
                    }
                }
            }
            ResourceList requiredFuel = DetermineFuelRequiredForFleet(fleet);
            StellarObject stellarObject = DecideBestFleetRefuelPoint(x, y, this, requiredFuel, empireToExclude);
            if (stellarObject != null)
            {
                int num = 2;
                if (stellarObject.DockingBays != null)
                {
                    num = stellarObject.DockingBays.Count;
                }
                long val = fleet.Ships.Count * Galaxy.FleetAssembleAttackWaitPeriodPerShip / num;
                val = Math.Max(val, Galaxy.FleetAssembleAttackWaitPeriodPerShip);
                long num2 = 45000L;
                GalaxyLocationList galaxyLocationList = _Galaxy.DetermineGalaxyLocationsAtPoint(stellarObject.Xpos, stellarObject.Ypos, GalaxyLocationType.NebulaCloud);
                if (galaxyLocationList.Count > 0 && galaxyLocationList[0].Effect == GalaxyLocationEffectType.MovementSlowed)
                {
                    num2 *= (long)((double)num2 * 1.333);
                }
                long starDate = DetermineLatestArrivalAtDestination(fleet, stellarObject) + num2 + val;
                if (target is Habitat)
                {
                    Habitat target2 = (Habitat)target;
                    fleet.ForceCompleteMission();
                    switch (missionType)
                    {
                        case BuiltObjectMissionType.Attack:
                        case BuiltObjectMissionType.WaitAndAttack:
                            result = fleet.AssignMission(BuiltObjectMissionType.WaitAndAttack, target2, stellarObject, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            break;
                        case BuiltObjectMissionType.WaitAndBombard:
                        case BuiltObjectMissionType.Bombard:
                            result = fleet.AssignMission(BuiltObjectMissionType.WaitAndBombard, target2, stellarObject, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            break;
                    }
                }
                else if (target is BuiltObject)
                {
                    BuiltObject target3 = (BuiltObject)target;
                    fleet.ForceCompleteMission();
                    switch (missionType)
                    {
                        case BuiltObjectMissionType.Attack:
                        case BuiltObjectMissionType.WaitAndAttack:
                            result = fleet.AssignMission(BuiltObjectMissionType.WaitAndAttack, target3, stellarObject, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            break;
                        case BuiltObjectMissionType.WaitAndBombard:
                        case BuiltObjectMissionType.Bombard:
                            result = fleet.AssignMission(BuiltObjectMissionType.WaitAndBombard, target3, stellarObject, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            break;
                    }
                }
                else if (target is ShipGroup)
                {
                    ShipGroup shipGroup2 = (ShipGroup)target;
                    if (shipGroup2.LeadShip.CurrentSpeed < (float)shipGroup2.LeadShip.WarpSpeed && !shipGroup2.LeadShip.HyperjumpPrepare)
                    {
                        fleet.ForceCompleteMission();
                        switch (missionType)
                        {
                            case BuiltObjectMissionType.Attack:
                            case BuiltObjectMissionType.WaitAndAttack:
                                result = fleet.AssignMission(BuiltObjectMissionType.WaitAndAttack, shipGroup2, stellarObject, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                break;
                            case BuiltObjectMissionType.WaitAndBombard:
                            case BuiltObjectMissionType.Bombard:
                                result = fleet.AssignMission(BuiltObjectMissionType.WaitAndBombard, shipGroup2, stellarObject, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                break;
                        }
                    }
                }
            }
            return result;
        }

        public long DetermineLatestArrivalAtDestination(ShipGroup fleet, StellarObject destination)
        {
            return DetermineLatestArrivalAtDestination(fleet, destination.Xpos, destination.Ypos);
        }

        public long DetermineLatestArrivalAtDestination(ShipGroup fleet, double x, double y)
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            long num = currentStarDate;
            double val = fleet.WarpSpeed;
            for (int i = 0; i < fleet.Ships.Count; i++)
            {
                BuiltObject builtObject = fleet.Ships[i];
                if (fleet.IsShipAvailable(builtObject))
                {
                    double num2 = _Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, x, y);
                    long num3 = (long)(num2 / Math.Min(builtObject.WarpSpeedWithBonuses, val) * 1000.0);
                    long num4 = 12000L;
                    long num5 = currentStarDate + num3 + num4;
                    if (num5 > num)
                    {
                        num = num5;
                    }
                }
            }
            return num;
        }

        private bool ImplementBlockade(Habitat colony, ref int refusalCount)
        {
            return ImplementBlockade(colony, sendFleet: true, performAuthorizationCheck: true, ref refusalCount);
        }

        public bool CanSendShipToBlockadeColony(Habitat colony)
        {
            if (!colony.IsBlockaded)
            {
                BuiltObject builtObject = _Galaxy.DetermineSpacePortAtColony(colony);
                if (builtObject != null)
                {
                    if (!builtObject.IsBlockaded)
                    {
                        return true;
                    }
                    Blockade blockade = _Galaxy.Blockades[builtObject];
                    if (blockade.Initiator == this)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            Blockade blockade2 = _Galaxy.Blockades[colony];
            if (blockade2.Initiator == this)
            {
                return true;
            }
            return false;
        }

        public bool CanSendShipToBlockadeBuiltObject(BuiltObject builtObject)
        {
            Blockade blockade = _Galaxy.Blockades[builtObject];
            if (blockade == null)
            {
                return true;
            }
            if (blockade.Initiator == this)
            {
                return true;
            }
            return false;
        }

        public bool SetupBlockade(Habitat colony)
        {
            if (colony != null && !colony.IsBlockaded)
            {
                Blockade blockade = _Galaxy.Blockades[colony];
                if (blockade == null)
                {
                    blockade = new Blockade(colony, this, _Galaxy.CurrentStarDate);
                    BuiltObjectList builtObjectList = new BuiltObjectList();
                    if (colony.Empire != null && colony.Empire != _Galaxy.IndependentEmpire && colony.BasesAtHabitat != null)
                    {
                        for (int i = 0; i < colony.BasesAtHabitat.Count; i++)
                        {
                            BuiltObject builtObject = colony.BasesAtHabitat[i];
                            if (builtObject != null && builtObject.ParentHabitat == colony)
                            {
                                builtObjectList.Add(builtObject);
                            }
                        }
                    }
                    BlockadeList blockadeList = new BlockadeList();
                    if (builtObjectList.Count > 0)
                    {
                        for (int j = 0; j < builtObjectList.Count; j++)
                        {
                            BuiltObject builtObject2 = builtObjectList[j];
                            if (builtObject2 != null)
                            {
                                Blockade blockade2 = _Galaxy.Blockades[builtObject2];
                                if (blockade2 != null)
                                {
                                    return false;
                                }
                                blockade2 = new Blockade(builtObject2, this, _Galaxy.CurrentStarDate);
                                blockadeList.Add(blockade2);
                            }
                        }
                    }
                    _Galaxy.Blockades.Add(blockade);
                    colony.IsBlockaded = true;
                    if (blockadeList.Count > 0)
                    {
                        for (int k = 0; k < blockadeList.Count; k++)
                        {
                            _Galaxy.Blockades.Add(blockadeList[k]);
                            blockadeList[k].BuiltObject.IsBlockaded = true;
                        }
                    }
                    string description = string.Format(TextResolver.GetText("We are initiating a general blockade of your colony at X"), colony.Name);
                    SendMessageToEmpire(colony.Empire, EmpireMessageType.BlockadeInitiated, colony, description);
                    return true;
                }
            }
            return false;
        }

        public bool SetupBlockade(BuiltObject builtObject)
        {
            if (builtObject != null && !builtObject.IsBlockaded)
            {
                Blockade blockade = _Galaxy.Blockades[builtObject];
                if (blockade == null)
                {
                    blockade = new Blockade(builtObject, this, _Galaxy.CurrentStarDate);
                    _Galaxy.Blockades.Add(blockade);
                    builtObject.IsBlockaded = true;
                    string description = string.Format(TextResolver.GetText("We are initiating a general blockade of SPACEPORT"), builtObject.Name);
                    SendMessageToEmpire(builtObject.Empire, EmpireMessageType.BlockadeInitiated, builtObject, description);
                    return true;
                }
            }
            return false;
        }

        public bool ImplementBlockade(Habitat colony, bool sendFleet, bool performAuthorizationCheck)
        {
            int refusalCount = 0;
            return ImplementBlockade(colony, sendFleet, performAuthorizationCheck, ref refusalCount);
        }

        public bool ImplementBlockade(Habitat colony, bool performAuthorizationCheck, ShipGroup fleet)
        {
            int refusalCount = 0;
            return ImplementBlockade(colony, sendFleet: true, performAuthorizationCheck, ref refusalCount, fleet);
        }

        public bool ImplementBlockade(Habitat colony, bool sendFleet, bool performAuthorizationCheck, ref int refusalCount)
        {
            return ImplementBlockade(colony, sendFleet, performAuthorizationCheck, ref refusalCount, null);
        }

        public bool ImplementBlockade(Habitat colony, bool sendFleet, bool performAuthorizationCheck, ref int refusalCount, ShipGroup fleet)
        {
            if (!colony.IsBlockaded)
            {
                if (sendFleet)
                {
                    if (fleet == null)
                    {
                        fleet = FindNearestAvailableFleet(colony.Xpos, colony.Ypos, BuiltObjectMissionPriority.Normal, 0, FleetPosture.Attack, mustBeWithinFuelRange: true);
                    }
                    if (fleet == null)
                    {
                        return false;
                    }
                    bool flag = true;
                    if (performAuthorizationCheck && (fleet.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated))
                    {
                        flag = CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageAttackEnemy(colony, blockade: true, fleet), colony, AdvisorMessageType.EnemyBlockade, fleet, null);
                    }
                    if (flag)
                    {
                        fleet.AssignMission(BuiltObjectMissionType.Blockade, colony, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        return true;
                    }
                }
                else
                {
                    bool flag2 = true;
                    if (performAuthorizationCheck)
                    {
                        flag2 = CheckTaskAuthorized(_ControlMilitaryAttacks, GenerateAutomationMessageAttackEnemy(colony, blockade: true, null), colony, AdvisorMessageType.EnemyBlockade);
                    }
                    if (flag2)
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        private bool ImplementBlockade(BuiltObject builtObject, ref int refusalCount)
        {
            return ImplementBlockade(builtObject, sendFleet: true, performAuthorizationCheck: true, ref refusalCount);
        }

        private bool ImplementBlockade(BuiltObject builtObject, ShipGroup fleet)
        {
            int refusalCount = 0;
            return ImplementBlockade(builtObject, sendFleet: true, performAuthorizationCheck: true, ref refusalCount, fleet);
        }

        public bool ImplementBlockade(BuiltObject builtObject, bool sendFleet, bool performAuthorizationCheck)
        {
            int refusalCount = 0;
            return ImplementBlockade(builtObject, sendFleet, performAuthorizationCheck, ref refusalCount);
        }

        public bool ImplementBlockade(BuiltObject builtObject, bool sendFleet, bool performAuthorizationCheck, ref int refusalCount)
        {
            return ImplementBlockade(builtObject, sendFleet, performAuthorizationCheck, ref refusalCount, null);
        }

        public bool ImplementBlockade(BuiltObject builtObject, bool sendFleet, bool performAuthorizationCheck, ref int refusalCount, ShipGroup fleet)
        {
            Blockade blockade = _Galaxy.Blockades[builtObject];
            if (blockade == null)
            {
                if (sendFleet)
                {
                    if (fleet == null)
                    {
                        fleet = FindNearestAvailableFleet(builtObject.Xpos, builtObject.Ypos, BuiltObjectMissionPriority.Normal, 0, FleetPosture.Attack, mustBeWithinFuelRange: true);
                    }
                    if (fleet == null)
                    {
                        return false;
                    }
                    bool flag = true;
                    if (performAuthorizationCheck && (fleet.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated))
                    {
                        flag = CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageAttackEnemy(builtObject, blockade: true, fleet), builtObject, AdvisorMessageType.EnemyBlockade, fleet, null);
                    }
                    if (flag)
                    {
                        fleet.AssignMission(BuiltObjectMissionType.Blockade, builtObject, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        return true;
                    }
                }
                else
                {
                    bool flag2 = true;
                    if (performAuthorizationCheck)
                    {
                        flag2 = CheckTaskAuthorized(_ControlMilitaryAttacks, GenerateAutomationMessageAttackEnemy(builtObject, blockade: true, null), builtObject, AdvisorMessageType.EnemyBlockade);
                    }
                    if (flag2)
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        public void CancelBlockades(Empire targetEmpire)
        {
            BlockadeList blockadesForEmpire = _Galaxy.Blockades.GetBlockadesForEmpire(this);
            BlockadeList blockadeList = new BlockadeList();
            foreach (Blockade item in blockadesForEmpire)
            {
                if (item.BlockadedEmpire == targetEmpire)
                {
                    blockadeList.Add(item);
                }
            }
            foreach (Blockade item2 in blockadeList)
            {
                for (int i = 0; i < ShipGroups.Count; i++)
                {
                    ShipGroup shipGroup = ShipGroups[i];
                    shipGroup.ClearAllMissionsForTarget(shipGroup, item2.BlockadedEmpire, BuiltObjectMissionType.Blockade);
                }
                for (int j = 0; j < BuiltObjects.Count; j++)
                {
                    BuiltObject builtObject = BuiltObjects[j];
                    if (builtObject != null && !builtObject.HasBeenDestroyed)
                    {
                        builtObject.ClearAllMissionsForTarget(builtObject, item2.BlockadedEmpire, BuiltObjectMissionType.Blockade, dropOutOfHyperspace: true);
                    }
                }
                if (item2.TargetIsColony)
                {
                    item2.Colony.IsBlockaded = false;
                    string description = string.Format(TextResolver.GetText("We are lifting our blockade of your colony at X"), item2.Colony.Name);
                    SendMessageToEmpire(item2.Colony.Empire, EmpireMessageType.BlockadeCancelled, item2.Colony, description);
                }
                else
                {
                    item2.BuiltObject.IsBlockaded = false;
                    string description2 = string.Format(TextResolver.GetText("We are lifting our blockade of SPACEPORT"), item2.BuiltObject.Name);
                    SendMessageToEmpire(item2.BuiltObject.Empire, EmpireMessageType.BlockadeCancelled, item2.BuiltObject, description2);
                }
                _Galaxy.Blockades.Remove(item2);
            }
        }

        public void CancelBlockade(BuiltObject builtObject)
        {
            Blockade blockade = _Galaxy.Blockades[builtObject];
            int iterationCount = 0;
            while (Galaxy.ConditionCheckLimit(blockade != null, 50, ref iterationCount))
            {
                if (blockade.Initiator != null && blockade.Initiator.ShipGroups != null)
                {
                    foreach (ShipGroup shipGroup in blockade.Initiator.ShipGroups)
                    {
                        shipGroup.ClearAllMissionsForTarget(shipGroup, builtObject, BuiltObjectMissionType.Blockade);
                    }
                }
                if (blockade.BuiltObject != null)
                {
                    blockade.BuiltObject.IsBlockaded = false;
                    string description = string.Format(TextResolver.GetText("The blockade of X has ended"), blockade.BuiltObject.Name);
                    if (blockade.BuiltObject.Empire != null)
                    {
                        blockade.BuiltObject.Empire.SendMessageToEmpire(blockade.BuiltObject.Empire, EmpireMessageType.BlockadeCancelled, blockade.BuiltObject, description);
                    }
                }
                _Galaxy.Blockades.Remove(blockade);
                blockade = _Galaxy.Blockades[builtObject];
            }
        }

        public void CancelBlockade(Habitat colony)
        {
            Blockade blockade = _Galaxy.Blockades[colony];
            int iterationCount = 0;
            while (Galaxy.ConditionCheckLimit(blockade != null, 50, ref iterationCount))
            {
                BuiltObject builtObject = _Galaxy.DetermineSpacePortAtColony(colony);
                if (builtObject != null)
                {
                    CancelBlockade(builtObject);
                }
                if (blockade.Initiator != null && blockade.Initiator.ShipGroups != null && blockade.Initiator.ShipGroups != null)
                {
                    foreach (ShipGroup shipGroup in blockade.Initiator.ShipGroups)
                    {
                        shipGroup.ClearAllMissionsForTarget(shipGroup, colony, BuiltObjectMissionType.Blockade);
                    }
                }
                if (blockade.Colony != null)
                {
                    blockade.Colony.IsBlockaded = false;
                    string description = string.Format(TextResolver.GetText("The blockade of X has ended"), blockade.Colony.Name);
                    if (blockade.Colony.Empire != null)
                    {
                        blockade.Colony.Empire.SendMessageToEmpire(blockade.Colony.Empire, EmpireMessageType.BlockadeCancelled, blockade.Colony, description);
                    }
                }
                _Galaxy.Blockades.Remove(blockade);
                blockade = _Galaxy.Blockades[colony];
            }
        }

        public void CancelAttacks(BuiltObject builtObject)
        {
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                for (int j = 0; j < empire.ShipGroups.Count; j++)
                {
                    ShipGroup shipGroup = empire.ShipGroups[j];
                    shipGroup.ClearAllMissionsForTarget(shipGroup, builtObject, BuiltObjectMissionType.Attack);
                    shipGroup.ClearAllMissionsForTarget(shipGroup, builtObject, BuiltObjectMissionType.WaitAndAttack);
                    shipGroup.ClearAllMissionsForTarget(shipGroup, builtObject, BuiltObjectMissionType.Capture);
                    shipGroup.ClearAllMissionsForTarget(shipGroup, builtObject, BuiltObjectMissionType.Raid);
                }
            }
        }

        public void CancelAllShipAttacksNonEnemies(BuiltObject builtObject)
        {
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                bool flag = true;
                if (empire != this && empire.PirateEmpireBaseHabitat == null && PirateEmpireBaseHabitat == null)
                {
                    DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                    if (diplomaticRelation.Type == DiplomaticRelationType.War)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    continue;
                }
                foreach (BuiltObject builtObject2 in empire.BuiltObjects)
                {
                    builtObject2.ClearAllMissionsForTarget(builtObject2, builtObject, BuiltObjectMissionType.Attack, dropOutOfHyperspace: true);
                    builtObject2.ClearAllMissionsForTarget(builtObject2, builtObject, BuiltObjectMissionType.WaitAndAttack, dropOutOfHyperspace: true);
                    builtObject2.ClearAllMissionsForTarget(builtObject2, builtObject, BuiltObjectMissionType.Capture, dropOutOfHyperspace: true);
                    builtObject2.ClearAllMissionsForTarget(builtObject2, builtObject, BuiltObjectMissionType.Raid, dropOutOfHyperspace: true);
                }
            }
        }

        public void CancelAllShipAttacksNonEnemies(Habitat colony)
        {
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                bool flag = true;
                if (empire != this && PirateEmpireBaseHabitat == null && empire.PirateEmpireBaseHabitat == null)
                {
                    DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                    if (diplomaticRelation.Type == DiplomaticRelationType.War)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    continue;
                }
                foreach (BuiltObject builtObject in empire.BuiltObjects)
                {
                    builtObject.ClearAllMissionsForTarget(builtObject, colony, BuiltObjectMissionType.Attack, dropOutOfHyperspace: true);
                    builtObject.ClearAllMissionsForTarget(builtObject, colony, BuiltObjectMissionType.WaitAndAttack, dropOutOfHyperspace: true);
                    builtObject.ClearAllMissionsForTarget(builtObject, colony, BuiltObjectMissionType.Bombard, dropOutOfHyperspace: true);
                    builtObject.ClearAllMissionsForTarget(builtObject, colony, BuiltObjectMissionType.WaitAndBombard, dropOutOfHyperspace: true);
                }
            }
        }

        public void CancelAllShipAttacks(BuiltObject builtObject)
        {
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                for (int j = 0; j < empire.BuiltObjects.Count; j++)
                {
                    BuiltObject builtObject2 = empire.BuiltObjects[j];
                    builtObject2.ClearAllMissionsForTarget(builtObject2, builtObject, BuiltObjectMissionType.Attack, dropOutOfHyperspace: true);
                    builtObject2.ClearAllMissionsForTarget(builtObject2, builtObject, BuiltObjectMissionType.WaitAndAttack, dropOutOfHyperspace: true);
                    builtObject2.ClearAllMissionsForTarget(builtObject2, builtObject, BuiltObjectMissionType.Capture, dropOutOfHyperspace: true);
                    builtObject2.ClearAllMissionsForTarget(builtObject2, builtObject, BuiltObjectMissionType.Raid, dropOutOfHyperspace: true);
                }
            }
        }

        public void CancelAllShipAttacks(Habitat colony, bool alsoCancelAttackForBases)
        {
            if (alsoCancelAttackForBases)
            {
                BuiltObject builtObject = _Galaxy.DetermineSpacePortAtColony(colony);
                if (builtObject != null)
                {
                    CancelAllShipAttacks(builtObject);
                }
                if (colony.BasesAtHabitat != null)
                {
                    for (int i = 0; i < colony.BasesAtHabitat.Count; i++)
                    {
                        BuiltObject builtObject2 = colony.BasesAtHabitat[i];
                        CancelAllShipAttacks(builtObject2);
                    }
                }
            }
            for (int j = 0; j < _Galaxy.Empires.Count; j++)
            {
                Empire empire = _Galaxy.Empires[j];
                for (int k = 0; k < empire.BuiltObjects.Count; k++)
                {
                    BuiltObject builtObject3 = empire.BuiltObjects[k];
                    builtObject3.ClearAllMissionsForTarget(builtObject3, colony, BuiltObjectMissionType.Attack, dropOutOfHyperspace: true);
                    builtObject3.ClearAllMissionsForTarget(builtObject3, colony, BuiltObjectMissionType.WaitAndAttack, dropOutOfHyperspace: true);
                    builtObject3.ClearAllMissionsForTarget(builtObject3, colony, BuiltObjectMissionType.Bombard, dropOutOfHyperspace: true);
                    builtObject3.ClearAllMissionsForTarget(builtObject3, colony, BuiltObjectMissionType.WaitAndBombard, dropOutOfHyperspace: true);
                    builtObject3.ClearAllMissionsForTarget(builtObject3, colony, BuiltObjectMissionType.Raid, dropOutOfHyperspace: true);
                }
            }
        }

        public void CancelAllShipUnloadTroops(Habitat colony)
        {
            if (colony == null)
            {
                return;
            }
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire != null && empire.BuiltObjects != null)
                {
                    for (int j = 0; j < empire.BuiltObjects.Count; j++)
                    {
                        BuiltObject builtObject = empire.BuiltObjects[j];
                        builtObject?.ClearAllMissionsForTarget(builtObject, colony, BuiltObjectMissionType.UnloadTroops, dropOutOfHyperspace: true);
                    }
                }
            }
        }

        public void CancelAttacks(Habitat colony)
        {
            BuiltObject builtObject = _Galaxy.DetermineSpacePortAtColony(colony);
            if (builtObject != null)
            {
                CancelAttacks(builtObject);
            }
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                for (int j = 0; j < empire.ShipGroups.Count; j++)
                {
                    ShipGroup shipGroup = empire.ShipGroups[j];
                    shipGroup.ClearAllMissionsForTarget(shipGroup, colony, BuiltObjectMissionType.Attack);
                    shipGroup.ClearAllMissionsForTarget(shipGroup, colony, BuiltObjectMissionType.WaitAndAttack);
                    shipGroup.ClearAllMissionsForTarget(shipGroup, colony, BuiltObjectMissionType.Bombard);
                    shipGroup.ClearAllMissionsForTarget(shipGroup, colony, BuiltObjectMissionType.WaitAndBombard);
                    shipGroup.ClearAllMissionsForTarget(shipGroup, colony, BuiltObjectMissionType.Raid);
                }
            }
        }

        public void CancelUnloadTroops(Habitat colony)
        {
            if (colony == null)
            {
                return;
            }
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire != null && empire.ShipGroups != null)
                {
                    for (int j = 0; j < empire.ShipGroups.Count; j++)
                    {
                        ShipGroup shipGroup = empire.ShipGroups[j];
                        shipGroup?.ClearAllMissionsForTarget(shipGroup, colony, BuiltObjectMissionType.UnloadTroops);
                    }
                }
            }
        }

        public void CancelAllCharacterTransfers(Habitat colony)
        {
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire == null || empire.Characters == null)
                {
                    continue;
                }
                for (int j = 0; j < empire.Characters.Count; j++)
                {
                    Character character = empire.Characters[j];
                    if (character != null && character.TransferDestination == colony && character.TransferTimeRemaining > 0f && character.Location != character.TransferDestination)
                    {
                        character.ResetTransfer();
                    }
                }
            }
        }

        private int CountFleetAttackStrengthAssignedToTarget(StellarObject target, out int troopStrengthAssigned)
        {
            int num = 0;
            troopStrengthAssigned = 0;
            if (target != null)
            {
                for (int i = 0; i < ShipGroups.Count; i++)
                {
                    ShipGroup shipGroup = ShipGroups[i];
                    if (shipGroup.Mission == null || shipGroup.Mission.Type == BuiltObjectMissionType.Undefined || (shipGroup.Mission.Type != BuiltObjectMissionType.Attack && shipGroup.Mission.Type != BuiltObjectMissionType.Blockade && shipGroup.Mission.Type != BuiltObjectMissionType.WaitAndAttack && shipGroup.Mission.Type != BuiltObjectMissionType.Bombard && shipGroup.Mission.Type != BuiltObjectMissionType.WaitAndBombard))
                    {
                        continue;
                    }
                    if (shipGroup.Mission.TargetBuiltObject != null && target is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)target;
                        if (builtObject == shipGroup.Mission.TargetBuiltObject)
                        {
                            num += shipGroup.TotalOverallStrengthFactor;
                            troopStrengthAssigned += shipGroup.TotalTroopAttackStrength;
                        }
                    }
                    else if (shipGroup.Mission.TargetHabitat != null && target is Habitat)
                    {
                        Habitat habitat = (Habitat)target;
                        if (habitat == shipGroup.Mission.TargetHabitat)
                        {
                            num += shipGroup.TotalOverallStrengthFactor;
                            troopStrengthAssigned += shipGroup.TotalTroopAttackStrength;
                        }
                    }
                }
            }
            return num;
        }

        private int CountFleetAttackStrengthAssignedToTarget(PrioritizedTarget target)
        {
            int num = 0;
            if (target.Target != null)
            {
                for (int i = 0; i < ShipGroups.Count; i++)
                {
                    ShipGroup shipGroup = ShipGroups[i];
                    if (shipGroup.Mission == null || shipGroup.Mission.Type == BuiltObjectMissionType.Undefined || (shipGroup.Mission.Type != BuiltObjectMissionType.Attack && shipGroup.Mission.Type != BuiltObjectMissionType.Blockade && shipGroup.Mission.Type != BuiltObjectMissionType.WaitAndAttack && shipGroup.Mission.Type != BuiltObjectMissionType.Bombard && shipGroup.Mission.Type != BuiltObjectMissionType.WaitAndBombard))
                    {
                        continue;
                    }
                    if (shipGroup.Mission.TargetBuiltObject != null && target.Target is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)target.Target;
                        if (builtObject == shipGroup.Mission.TargetBuiltObject)
                        {
                            num += shipGroup.TotalOverallStrengthFactor;
                        }
                    }
                    else if (shipGroup.Mission.TargetHabitat != null && target.Target is Habitat)
                    {
                        Habitat habitat = (Habitat)target.Target;
                        if (habitat == shipGroup.Mission.TargetHabitat)
                        {
                            num += shipGroup.TotalOverallStrengthFactor;
                        }
                    }
                    else if (shipGroup.Mission.TargetShipGroup != null && target.Target is ShipGroup)
                    {
                        ShipGroup shipGroup2 = (ShipGroup)target.Target;
                        if (shipGroup2 == shipGroup.Mission.TargetShipGroup)
                        {
                            num += shipGroup.TotalOverallStrengthFactor;
                        }
                    }
                }
            }
            return num;
        }

        private int CountShipGroupsAssignedToEmpire(Empire empire, bool includeSmallFleets)
        {
            int num = 0;
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if ((includeSmallFleets || shipGroup.ShipTargetAmount >= 10 || shipGroup.Ships.Count >= 10) && shipGroup.Mission != null && shipGroup.Mission.Type != 0)
                {
                    Empire empire2 = null;
                    if (shipGroup.Mission.TargetBuiltObject != null)
                    {
                        empire2 = shipGroup.Mission.TargetBuiltObject.Empire;
                    }
                    else if (shipGroup.Mission.TargetHabitat != null)
                    {
                        empire2 = shipGroup.Mission.TargetHabitat.Empire;
                    }
                    else if (shipGroup.Mission.TargetShipGroup != null)
                    {
                        empire2 = shipGroup.Mission.TargetShipGroup.Empire;
                    }
                    if (empire2 == empire)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public void IdentifyMilitaryObjectives()
        {
            int refusalCount = 0;
            double intoleranceLevel = _Galaxy.IntoleranceLevel;
            int num = 0;
            int num2 = 0;
            EmpireList empireList = new EmpireList();
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation.Type == DiplomaticRelationType.War)
                {
                    empireList.Add(diplomaticRelation.OtherEmpire);
                    num2++;
                    if (diplomaticRelation.Initiator == this)
                    {
                        num++;
                    }
                }
            }
            if (num2 > 3)
            {
                num2 = 3;
            }
            int num3 = ShipGroups.CountLargeFleets();
            int num4 = 0;
            if (PlanetDestroyers != null)
            {
                num4 = PlanetDestroyers.Count;
            }
            int maximumAttacksForEmpireWeDeclaredWarOn = num3;
            int maximumAttacksForEmpire = num3;
            int maximumBlockadesForEmpire = num3;
            int maximumPreparationsForEmpire = num3;
            int maximumPlanetDestroyerAttacksForEmpire = num4;
            if (num2 > 0)
            {
                maximumAttacksForEmpire = 1 + num3 / num2;
                maximumPlanetDestroyerAttacksForEmpire = 1 + num4 / num2;
                maximumBlockadesForEmpire = 0;
                maximumPreparationsForEmpire = 0;
                if (num > 0)
                {
                    maximumAttacksForEmpireWeDeclaredWarOn = 1 + num3 / num;
                    if (num3 > num)
                    {
                        maximumAttacksForEmpire = 1;
                        if (num3 > num * 2)
                        {
                            maximumBlockadesForEmpire = 0;
                            maximumPreparationsForEmpire = 0;
                        }
                        else
                        {
                            maximumBlockadesForEmpire = 0;
                            maximumPreparationsForEmpire = 0;
                        }
                    }
                    else
                    {
                        maximumAttacksForEmpire = 0;
                        maximumBlockadesForEmpire = 0;
                        maximumPreparationsForEmpire = 0;
                    }
                }
            }
            if (_ControlMilitaryAttacks != 0 && Policy.UseExplorationShipsToScoutEnemySystems)
            {
                SendScoutShipsToEnemyLocations(empireList);
            }
            if (empireList != null && empireList.Count > 0)
            {
                empireList = _Galaxy.SortEmpiresByMilitaryPriority(this, empireList);
                for (int j = 0; j < empireList.Count; j++)
                {
                    EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(empireList[j]);
                    if (!IdentifyMilitaryObjectivesForSingleEmpire(empireList[j], empireEvaluation.OverallAttitude, maximumAttacksForEmpireWeDeclaredWarOn, maximumAttacksForEmpire, maximumPlanetDestroyerAttacksForEmpire, maximumBlockadesForEmpire, maximumPreparationsForEmpire, intoleranceLevel, ref refusalCount))
                    {
                        break;
                    }
                }
            }
            int num5 = Galaxy.Rnd.Next(0, EmpireEvaluations.Count);
            for (int k = num5; k < EmpireEvaluations.Count && (empireList.Contains(EmpireEvaluations[k].Empire) || IdentifyMilitaryObjectivesForSingleEmpire(EmpireEvaluations[k].Empire, EmpireEvaluations[k].OverallAttitude, maximumAttacksForEmpireWeDeclaredWarOn, maximumAttacksForEmpire, maximumPlanetDestroyerAttacksForEmpire, maximumBlockadesForEmpire, maximumPreparationsForEmpire, intoleranceLevel, ref refusalCount)); k++)
            {
            }
            for (int l = 0; l < num5 && (empireList.Contains(EmpireEvaluations[l].Empire) || IdentifyMilitaryObjectivesForSingleEmpire(EmpireEvaluations[l].Empire, EmpireEvaluations[l].OverallAttitude, maximumAttacksForEmpireWeDeclaredWarOn, maximumAttacksForEmpire, maximumPlanetDestroyerAttacksForEmpire, maximumBlockadesForEmpire, maximumPreparationsForEmpire, intoleranceLevel, ref refusalCount)); l++)
            {
            }
        }

        private void ClearExpiredDeclinedTasks()
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            DeclinedTaskList declinedTaskList = new DeclinedTaskList();
            for (int i = 0; i < _DeclinedTasks.Count; i++)
            {
                if (_DeclinedTasks[i].ExpiryDate < currentStarDate)
                {
                    declinedTaskList.Add(_DeclinedTasks[i]);
                }
            }
            foreach (DeclinedTask item in declinedTaskList)
            {
                _DeclinedTasks.Remove(item);
            }
        }

        private bool CheckTaskAuthorized(AutomationLevel automationLevel, string taskDescription, object taskTarget, AdvisorMessageType advisorMessageType)
        {
            int refusalCount = 0;
            return CheckTaskAuthorized(automationLevel, ref refusalCount, taskDescription, taskTarget, advisorMessageType, null);
        }

        private bool CheckTaskAuthorized(AutomationLevel automationLevel, ref int refusalCount, string taskDescription, object taskTarget, AdvisorMessageType advisorMessageType)
        {
            return CheckTaskAuthorized(automationLevel, ref refusalCount, taskDescription, taskTarget, advisorMessageType, null);
        }

        private bool CheckTaskAuthorized(AutomationLevel automationLevel, ref int refusalCount, string taskDescription, object taskTarget, AdvisorMessageType advisorMessageType, Empire attackEmpireTarget)
        {
            return CheckTaskAuthorized(automationLevel, ref refusalCount, taskDescription, taskTarget, advisorMessageType, attackEmpireTarget, null, null);
        }

        public bool CheckTaskAuthorized(AutomationLevel automationLevel, ref int refusalCount, string taskDescription, object taskTarget, AdvisorMessageType advisorMessageType, object advisorMessageData, object advisorMessageData2)
        {
            return CheckTaskAuthorized(automationLevel, ref refusalCount, taskDescription, taskTarget, advisorMessageType, null, advisorMessageData, advisorMessageData2);
        }

        private bool CheckTaskAuthorized(AutomationLevel automationLevel, ref int refusalCount, string taskDescription, object taskTarget, AdvisorMessageType advisorMessageType, Empire attackEmpireTarget, object advisorMessageData, object advisorMessageData2)
        {
            bool result = true;
            switch (automationLevel)
            {
                case AutomationLevel.SemiAutomated:
                    {
                        result = false;
                        long currentStarDate = _Galaxy.CurrentStarDate;
                        if (refusalCount >= Galaxy.MaximumMissionRefusals || !_DeclinedTasks.CheckTaskTargetValid(taskTarget, currentStarDate) || (attackEmpireTarget != null && !_DeclinedTasks.CheckAttackEmpireTargetValid(attackEmpireTarget, currentStarDate)))
                        {
                            break;
                        }
                        _AutomationResponse = AutomationResponse.Undefined;
                        if (this == _Galaxy.PlayerEmpire)
                        {
                            refusalCount++;
                            EmpireMessage empireMessage = new EmpireMessage(this, EmpireMessageType.AdvisorSuggestion, taskTarget);
                            empireMessage.StarDate = currentStarDate;
                            empireMessage.AdvisorMessageType = advisorMessageType;
                            empireMessage.Description = taskDescription;
                            if (advisorMessageType == AdvisorMessageType.DiplomaticGift)
                            {
                                empireMessage.Money = (int)(double)advisorMessageData;
                            }
                            else
                            {
                                empireMessage.AdvisorMessageData = advisorMessageData;
                            }
                            empireMessage.AdvisorMessageData2 = advisorMessageData2;
                            PromptPlayerForAuthorization(taskDescription, taskTarget, empireMessage);
                            if (attackEmpireTarget != null)
                            {
                                long expiryDate = currentStarDate + 240000;
                                _DeclinedTasks.Add(new DeclinedTask(expiryDate, attackEmpireTarget));
                            }
                            if (taskTarget != null)
                            {
                                long expiryDate2 = currentStarDate + 600000;
                                if (taskTarget is BuiltObject)
                                {
                                    _DeclinedTasks.Add(new DeclinedTask((BuiltObject)taskTarget, expiryDate2));
                                }
                                else if (taskTarget is Habitat)
                                {
                                    _DeclinedTasks.Add(new DeclinedTask((Habitat)taskTarget, expiryDate2));
                                }
                                else if (taskTarget is IntelligenceMission)
                                {
                                    _DeclinedTasks.Add(new DeclinedTask((IntelligenceMission)taskTarget, expiryDate2));
                                }
                                else if (taskTarget is Empire)
                                {
                                    _DeclinedTasks.Add(new DeclinedTask((Empire)taskTarget, expiryDate2));
                                }
                            }
                            return false;
                        }
                        _AutomationResponse = AutomationResponse.Yes;
                        while (_AutomationResponse == AutomationResponse.Undefined)
                        {
                            Thread.Sleep(50);
                            if (this != _Galaxy.PlayerEmpire)
                            {
                                _AutomationResponse = AutomationResponse.Yes;
                            }
                        }
                        if (_AutomationResponse == AutomationResponse.Yes)
                        {
                            result = true;
                        }
                        else if (_AutomationResponse == AutomationResponse.No)
                        {
                            if (attackEmpireTarget != null)
                            {
                                long expiryDate3 = currentStarDate + 240000;
                                _DeclinedTasks.Add(new DeclinedTask(expiryDate3, attackEmpireTarget));
                            }
                            if (taskTarget != null)
                            {
                                long expiryDate4 = currentStarDate + 600000;
                                if (taskTarget is BuiltObject)
                                {
                                    _DeclinedTasks.Add(new DeclinedTask((BuiltObject)taskTarget, expiryDate4));
                                }
                                else if (taskTarget is Habitat)
                                {
                                    _DeclinedTasks.Add(new DeclinedTask((Habitat)taskTarget, expiryDate4));
                                }
                                else if (taskTarget is IntelligenceMission)
                                {
                                    _DeclinedTasks.Add(new DeclinedTask((IntelligenceMission)taskTarget, expiryDate4));
                                }
                                else if (taskTarget is Empire)
                                {
                                    _DeclinedTasks.Add(new DeclinedTask((Empire)taskTarget, expiryDate4));
                                }
                            }
                        }
                        _AutomationResponse = AutomationResponse.Undefined;
                        break;
                    }
                case AutomationLevel.Manual:
                    result = false;
                    break;
            }
            return result;
        }

        public bool CheckBombardEnemyColony(Habitat enemyColony, ShipGroup attackFleet)
        {
            bool result = false;
            if (enemyColony.Empire != this && enemyColony.Population != null && enemyColony.Population.TotalAmount > 0)
            {
                if (attackFleet.TotalBombardPower > 0)
                {
                    result = Galaxy.CheckUseBombardmentAgainstEmpire(this, enemyColony.Empire);
                }
                if (enemyColony.PlanetaryShieldPresent)
                {
                    result = false;
                }
            }
            return result;
        }

        public bool AssignFleetAttackMission(ShipGroup fleet, ref PrioritizedTargetList targets, ref int refusalCount)
        {
            bool flag = false;
            bool flag2 = false;
            if (IsShipGroupAvailableWithAttackStrength(fleet, BuiltObjectMissionPriority.Normal, 0))
            {
                PrioritizedTargetList prioritizedTargetList = new PrioritizedTargetList();
                PrioritizedTarget prioritizedTarget = null;
                int iterationCount = 0;
                while (Galaxy.ConditionCheckLimit(!flag && !flag2, 200, ref iterationCount))
                {
                    prioritizedTarget = targets.IdentifyBestTargetFromLocation(this, fleet.LeadShip.Xpos, fleet.LeadShip.Ypos, fleet.TotalTroopAttackStrengthNearby(0.3), fleet.TotalOverallStrengthFactor, prioritizedTargetList);
                    if (prioritizedTarget != null)
                    {
                        prioritizedTargetList.Add(prioritizedTarget);
                        int num = CountFleetAttackStrengthAssignedToTarget(prioritizedTarget);
                        if (prioritizedTarget.LocationStrength < num)
                        {
                            continue;
                        }
                        if (prioritizedTarget.Target is ShipGroup)
                        {
                            ShipGroup shipGroup = (ShipGroup)prioritizedTarget.Target;
                            if (shipGroup.LeadShip.CurrentSpeed < (float)shipGroup.LeadShip.WarpSpeed && !shipGroup.LeadShip.HyperjumpPrepare)
                            {
                                int locationStrength = prioritizedTarget.LocationStrength;
                                if (fleet.TotalOverallStrengthFactor > locationStrength && fleet.CheckFleetTargetWithinFuelRangeAndRefuel(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, 0.0) && (fleet.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageAttackEnemy(shipGroup, fleet), shipGroup, AdvisorMessageType.EnemyAttack, fleet, null))
                                {
                                    fleet.ForceCompleteMission();
                                    fleet.AssignMission(BuiltObjectMissionType.Attack, shipGroup, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                    flag = true;
                                }
                            }
                            continue;
                        }
                        int num2 = Galaxy.DetermineRequiredTroopStrength(this, prioritizedTarget.Target);
                        if (fleet.TotalTroopAttackStrengthNearby(0.3) < num2)
                        {
                            continue;
                        }
                        BuiltObjectMissionType builtObjectMissionType = BuiltObjectMissionType.Attack;
                        AdvisorMessageType advisorMessageType = AdvisorMessageType.EnemyAttack;
                        double num3 = -1.0;
                        double num4 = -1.0;
                        if (prioritizedTarget.Target is BuiltObject)
                        {
                            BuiltObject builtObject = (BuiltObject)prioritizedTarget.Target;
                            num3 = builtObject.Xpos;
                            num4 = builtObject.Ypos;
                        }
                        else if (prioritizedTarget.Target is Habitat)
                        {
                            Habitat habitat = (Habitat)prioritizedTarget.Target;
                            num3 = habitat.Xpos;
                            num4 = habitat.Ypos;
                            if (CheckBombardEnemyColony(habitat, fleet))
                            {
                                builtObjectMissionType = BuiltObjectMissionType.Bombard;
                                advisorMessageType = AdvisorMessageType.EnemyBombard;
                            }
                        }
                        if (fleet.CheckFleetTargetWithinFuelRangeAndRefuel(num3, num4, 0.0))
                        {
                            if (prioritizedTarget.Target is Habitat)
                            {
                                Habitat habitat2 = (Habitat)prioritizedTarget.Target;
                                string taskDescription = GenerateAutomationMessageAttackEnemy(habitat2, fleet);
                                if (builtObjectMissionType == BuiltObjectMissionType.Bombard)
                                {
                                    taskDescription = GenerateAutomationMessageBombardColony(habitat2, fleet);
                                }
                                if ((fleet.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, taskDescription, habitat2, advisorMessageType, fleet, null))
                                {
                                    fleet.ForceCompleteMission();
                                    fleet.AssignMission(builtObjectMissionType, habitat2, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                    flag = true;
                                }
                            }
                            else if (prioritizedTarget.Target is BuiltObject)
                            {
                                BuiltObject builtObject2 = (BuiltObject)prioritizedTarget.Target;
                                if ((fleet.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageAttackEnemy(builtObject2, fleet), builtObject2, advisorMessageType, fleet, null))
                                {
                                    fleet.ForceCompleteMission();
                                    fleet.AssignMission(builtObjectMissionType, builtObject2, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                    flag = true;
                                }
                            }
                            else if (prioritizedTarget.Target is ShipGroup)
                            {
                                ShipGroup shipGroup2 = (ShipGroup)prioritizedTarget.Target;
                                if (shipGroup2.LeadShip.CurrentSpeed < (float)shipGroup2.LeadShip.WarpSpeed && !shipGroup2.LeadShip.HyperjumpPrepare && (fleet.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageAttackEnemy(shipGroup2, fleet), shipGroup2, advisorMessageType, fleet, null))
                                {
                                    fleet.ForceCompleteMission();
                                    fleet.AssignMission(builtObjectMissionType, shipGroup2, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                    flag = true;
                                }
                            }
                        }
                        else
                        {
                            if (fleet.WarpSpeed <= 0)
                            {
                                continue;
                            }
                            ResourceList requiredFuel = DetermineFuelRequiredForFleet(fleet);
                            StellarObject stellarObject = SelectWayPoint(prioritizedTarget, requiredFuel);
                            if (stellarObject == null || stellarObject.DockingBays == null || stellarObject.DockingBays.Count <= 0)
                            {
                                continue;
                            }
                            double num5 = fleet.MaximumRange();
                            double num6 = _Galaxy.CalculateDistance(stellarObject.Xpos, stellarObject.Ypos, num3, num4);
                            if (!(num6 < num5 * 0.45))
                            {
                                continue;
                            }
                            int num7 = 2;
                            if (stellarObject.DockingBays != null)
                            {
                                num7 = Math.Max(1, stellarObject.DockingBays.Count);
                            }
                            long num8 = fleet.Ships.Count * Galaxy.FleetAssembleAttackWaitPeriodPerShip / num7;
                            long starDate = DetermineLatestArrivalAtDestination(fleet, stellarObject) + num8;
                            if (!fleet.CheckFleetTargetWithinFuelRange(stellarObject.Xpos, stellarObject.Ypos, 0.1))
                            {
                                stellarObject = null;
                            }
                            if (stellarObject == null)
                            {
                                continue;
                            }
                            if (prioritizedTarget.Target is Habitat)
                            {
                                Habitat habitat3 = (Habitat)prioritizedTarget.Target;
                                if ((fleet.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageAttackEnemyWithWaypoint(habitat3, blockade: false, fleet, stellarObject), habitat3, AdvisorMessageType.EnemyAttack, fleet, stellarObject))
                                {
                                    fleet.ForceCompleteMission();
                                    fleet.AssignMission(BuiltObjectMissionType.WaitAndAttack, habitat3, stellarObject, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                    flag = true;
                                }
                            }
                            else if (prioritizedTarget.Target is BuiltObject)
                            {
                                BuiltObject builtObject3 = (BuiltObject)prioritizedTarget.Target;
                                if ((fleet.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageAttackEnemyWithWaypoint(builtObject3, blockade: false, fleet, stellarObject), builtObject3, AdvisorMessageType.EnemyAttack, fleet, stellarObject))
                                {
                                    fleet.ForceCompleteMission();
                                    fleet.AssignMission(BuiltObjectMissionType.WaitAndAttack, builtObject3, stellarObject, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                    flag = true;
                                }
                            }
                            else if (prioritizedTarget.Target is ShipGroup)
                            {
                                ShipGroup shipGroup3 = (ShipGroup)prioritizedTarget.Target;
                                if (shipGroup3.LeadShip.CurrentSpeed < (float)shipGroup3.LeadShip.WarpSpeed && !shipGroup3.LeadShip.HyperjumpPrepare && (fleet.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageAttackEnemy(shipGroup3, fleet), shipGroup3, AdvisorMessageType.EnemyAttack, fleet, stellarObject))
                                {
                                    fleet.ForceCompleteMission();
                                    fleet.AssignMission(BuiltObjectMissionType.WaitAndAttack, shipGroup3, stellarObject, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                    flag = true;
                                }
                            }
                            else if ((fleet.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, "", null, AdvisorMessageType.EnemyAttack, prioritizedTarget.Empire))
                            {
                                fleet.ForceCompleteMission();
                                fleet.AssignMission(BuiltObjectMissionType.WaitAndAttack, prioritizedTarget.Target, stellarObject, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                flag = true;
                            }
                            if (flag && fleet.LeadShip.IsAutoControlled && stellarObject is BuiltObject)
                            {
                                BuiltObject builtObject4 = (BuiltObject)stellarObject;
                                if (builtObject4.ParentHabitat != null && (builtObject4.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject4.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject4.SubRole == BuiltObjectSubRole.LargeSpacePort))
                                {
                                    fleet.GatherPoint = builtObject4.ParentHabitat;
                                }
                            }
                        }
                    }
                    else
                    {
                        flag2 = true;
                    }
                }
                if (flag && !flag2 && prioritizedTarget != null && prioritizedTarget.LocationStrength < fleet.TotalOverallStrengthFactor)
                {
                    targets.Remove(prioritizedTarget);
                }
            }
            return flag;
        }

        public StellarObject SelectWayPointOnTheWay(ShipGroup fleet, double targetX, double targetY, Empire targetEmpire)
        {
            if (fleet.LeadShip != null)
            {
                double maxFuelRange = fleet.LeadShip.CurrentRange();
                return SelectWayPointOnTheWay(fleet.LeadShip.Xpos, fleet.LeadShip.Ypos, targetX, targetY, fleet.Empire, targetEmpire, fleet.LeadShip.FuelType, maxFuelRange, fleet);
            }
            return null;
        }

        public StellarObject SelectWayPointOnTheWay(double x, double y, double targetX, double targetY, Empire empire, Empire empireToExclude, Resource fuelType, double maxFuelRange, ShipGroup fleet)
        {
            StellarObject stellarObject = null;
            ResourceList requiredFuel = DetermineFuelRequiredForFleet(fleet);
            double num = maxFuelRange;
            double num2 = Galaxy.DetermineAngle(x, y, targetX, targetY);
            int num3 = 0;
            while (stellarObject == null && num3 < 5)
            {
                num *= 0.67;
                double x2 = x + Math.Cos(num2) * num;
                double y2 = y + Math.Sin(num2) * num;
                stellarObject = DecideBestFleetRefuelPoint(x2, y2, empire, requiredFuel, empireToExclude);
                if (stellarObject != null)
                {
                    double num4 = _Galaxy.CalculateDistance(x, y, stellarObject.Xpos, stellarObject.Ypos);
                    if (num4 < maxFuelRange)
                    {
                        if (fleet != null && !fleet.CheckFleetTargetWithinFuelRange(stellarObject.Xpos, stellarObject.Ypos, 0.0))
                        {
                            stellarObject = null;
                        }
                    }
                    else
                    {
                        stellarObject = null;
                    }
                }
                num3++;
            }
            return stellarObject;
        }

        private ShipGroupList GenerateOrderedFleetsByOverallStrength()
        {
            ShipGroupList shipGroupList = new ShipGroupList();
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                shipGroup.SortTag = shipGroup.TotalOverallStrengthFactor;
                shipGroupList.Add(shipGroup);
            }
            shipGroupList.Sort();
            shipGroupList.Reverse();
            shipGroupList.ClearSortTags();
            return shipGroupList;
        }

        private ShipGroupList GenerateOrderedFleetsByFighterStrength()
        {
            ShipGroupList shipGroupList = new ShipGroupList();
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                shipGroup.SortTag = shipGroup.TotalFighterCount;
                shipGroupList.Add(shipGroup);
            }
            shipGroupList.Sort();
            shipGroupList.Reverse();
            shipGroupList.ClearSortTags();
            return shipGroupList;
        }

        private ShipGroupList GenerateOrderedFleetsByTroopAttackStrength()
        {
            ShipGroupList shipGroupList = new ShipGroupList();
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                shipGroup.SortTag = shipGroup.TotalTroopAttackStrength;
                shipGroupList.Add(shipGroup);
            }
            shipGroupList.Sort();
            shipGroupList.Reverse();
            shipGroupList.ClearSortTags();
            return shipGroupList;
        }

        private ShipGroupList GenerateOrderedFleetsByTroopDefendStrength()
        {
            ShipGroupList shipGroupList = new ShipGroupList();
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                shipGroup.SortTag = shipGroup.TotalTroopDefendStrength;
                shipGroupList.Add(shipGroup);
            }
            shipGroupList.Sort();
            shipGroupList.Reverse();
            shipGroupList.ClearSortTags();
            return shipGroupList;
        }

        private ShipGroupList GenerateOrderedFleetsForEmpireTargets(Empire targetEmpire, bool includeSmallFleets)
        {
            if (targetEmpire.Capital != null)
            {
                return GenerateOrderedFleetsForTarget(targetEmpire.Capital.Xpos, targetEmpire.Capital.Ypos, includeSmallFleets);
            }
            if (targetEmpire.PirateEmpireBaseHabitat != null)
            {
                return GenerateOrderedFleetsForTarget(targetEmpire.PirateEmpireBaseHabitat.Xpos, targetEmpire.PirateEmpireBaseHabitat.Ypos, includeSmallFleets);
            }
            return GenerateOrderedFleetsForTarget(Galaxy.SizeX / 2, Galaxy.SizeY / 2, includeSmallFleets);
        }

        private ShipGroupList GenerateOrderedFleetsForTarget(double x, double y, bool includeSmallFleets)
        {
            ShipGroupList shipGroupList = new ShipGroupList();
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                if ((includeSmallFleets || ShipGroups[i].ShipTargetAmount >= 10 || ShipGroups[i].Ships.Count >= 10) && ShipGroups[i].LeadShip != null)
                {
                    double sortTag = _Galaxy.CalculateDistance(x, y, ShipGroups[i].LeadShip.Xpos, ShipGroups[i].LeadShip.Ypos);
                    ShipGroups[i].SortTag = sortTag;
                    shipGroupList.Add(ShipGroups[i]);
                }
            }
            shipGroupList.Sort();
            shipGroupList.ClearSortTags();
            return shipGroupList;
        }

        private bool IdentifyMilitaryObjectivesForSingleEmpire(Empire empire, int overallAttitude, int maximumAttacksForEmpireWeDeclaredWarOn, int maximumAttacksForEmpire, int maximumPlanetDestroyerAttacksForEmpire, int maximumBlockadesForEmpire, int maximumPreparationsForEmpire, double galaxyIntoleranceLevel, ref int refusalCount)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
            if (diplomaticRelation.Type == DiplomaticRelationType.War)
            {
                bool flag = false;
                if (diplomaticRelation.Initiator == this)
                {
                    flag = true;
                }
                PrioritizedTargetList targets = IdentifyEmpireStrikePoints(empire);
                PrioritizedTargetList prioritizedTargetList = new PrioritizedTargetList();
                if (PlanetDestroyers.Count > 0 && Galaxy.CheckUsePlanetDestroyerAgainstEmpire(this, empire))
                {
                    int num = 0;
                    for (int i = 0; i < targets.Count; i++)
                    {
                        PrioritizedTarget prioritizedTarget = targets[i];
                        if (prioritizedTarget == null || !(prioritizedTarget.Target is Habitat))
                        {
                            continue;
                        }
                        Habitat habitat = (Habitat)prioritizedTarget.Target;
                        for (int j = 0; j < PlanetDestroyers.Count; j++)
                        {
                            BuiltObject builtObject = PlanetDestroyers[j];
                            if (builtObject != null && builtObject.BuiltAt == null)
                            {
                                bool flag2 = false;
                                if (builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined || builtObject.Mission.Priority == BuiltObjectMissionPriority.Low)
                                {
                                    flag2 = true;
                                }
                                if (flag2 && builtObject.WithinFuelRangeAndRefuel(habitat.Xpos, habitat.Ypos, 0.1) && (builtObject.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageDestroyPlanet(habitat, builtObject), habitat, AdvisorMessageType.EnemyAttackPlanetDestroyer, builtObject, null))
                                {
                                    builtObject.AssignMission(BuiltObjectMissionType.Attack, prioritizedTarget.Target, null, BuiltObjectMissionPriority.Normal);
                                    prioritizedTargetList.Add(prioritizedTarget);
                                    num++;
                                    break;
                                }
                            }
                        }
                        if (num >= maximumPlanetDestroyerAttacksForEmpire)
                        {
                            break;
                        }
                    }
                }
                foreach (PrioritizedTarget item in prioritizedTargetList)
                {
                    targets.Remove(item);
                }
                prioritizedTargetList.Clear();
                if (diplomaticRelation.WarObjective == WarObjective.CaptureObjectives)
                {
                    for (int k = 0; k < ShipGroups.Count; k++)
                    {
                        ShipGroup shipGroup = ShipGroups[k];
                        if (shipGroup.LeadShip == null || !shipGroup.LeadShip.IsAutoControlled || shipGroup.Posture != 0)
                        {
                            continue;
                        }
                        if (shipGroup.AttackPoint == null)
                        {
                            if (shipGroup.Mission != null && shipGroup.Mission.Type != 0 && shipGroup.Mission.Priority != BuiltObjectMissionPriority.Low)
                            {
                                continue;
                            }
                            bool waypointing = false;
                            StellarObject stellarObject = SelectFleetWarAttackTarget(shipGroup, empire, out waypointing);
                            if (stellarObject == null || waypointing)
                            {
                                continue;
                            }
                            BuiltObjectMissionType missionType = BuiltObjectMissionType.Attack;
                            if (stellarObject is Habitat)
                            {
                                Habitat enemyColony = (Habitat)stellarObject;
                                if (CheckBombardEnemyColony(enemyColony, shipGroup))
                                {
                                    missionType = BuiltObjectMissionType.Bombard;
                                }
                            }
                            shipGroup.AssignMission(missionType, stellarObject, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        }
                        else
                        {
                            if (shipGroup.AttackPoint == null || shipGroup.AttackPoint.Empire != empire || (shipGroup.Mission != null && shipGroup.Mission.Type != 0 && shipGroup.Mission.Priority != BuiltObjectMissionPriority.Low))
                            {
                                continue;
                            }
                            if (CheckFleetCanAttackTarget(shipGroup, shipGroup.AttackPoint))
                            {
                                BuiltObjectMissionType missionType2 = BuiltObjectMissionType.Attack;
                                if (shipGroup.AttackPoint is Habitat)
                                {
                                    Habitat enemyColony2 = (Habitat)shipGroup.AttackPoint;
                                    if (CheckBombardEnemyColony(enemyColony2, shipGroup))
                                    {
                                        missionType2 = BuiltObjectMissionType.Bombard;
                                    }
                                }
                                shipGroup.AssignMission(missionType2, shipGroup.AttackPoint, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                continue;
                            }
                            bool waypointing2 = false;
                            StellarObject stellarObject2 = SelectFleetWarAttackTarget(shipGroup, empire, out waypointing2);
                            if (stellarObject2 == null || waypointing2)
                            {
                                continue;
                            }
                            BuiltObjectMissionType missionType3 = BuiltObjectMissionType.Attack;
                            if (stellarObject2 is Habitat)
                            {
                                Habitat enemyColony3 = (Habitat)stellarObject2;
                                if (CheckBombardEnemyColony(enemyColony3, shipGroup))
                                {
                                    missionType3 = BuiltObjectMissionType.Bombard;
                                }
                            }
                            shipGroup.AssignMission(missionType3, stellarObject2, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        }
                    }
                }
                int num2 = maximumAttacksForEmpire;
                if (flag)
                {
                    num2 = maximumAttacksForEmpireWeDeclaredWarOn;
                }
                ShipGroupList shipGroupList = GenerateOrderedFleetsForEmpireTargets(empire, includeSmallFleets: false);
                for (int l = 0; l < shipGroupList.Count; l++)
                {
                    ShipGroup shipGroup2 = shipGroupList[l];
                    if (shipGroup2.Posture == FleetPosture.Attack)
                    {
                        if (CountShipGroupsAssignedToEmpire(empire, includeSmallFleets: false) >= num2)
                        {
                            return true;
                        }
                        AssignFleetAttackMission(shipGroup2, ref targets, ref refusalCount);
                    }
                }
            }
            else if (diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions && Policy.DiplomacyTradeSanctionsUseBlockades && maximumBlockadesForEmpire > 0 && (diplomaticRelation.Strategy == DiplomaticStrategy.Conquer || diplomaticRelation.Strategy == DiplomaticStrategy.Punish || diplomaticRelation.Strategy == DiplomaticStrategy.Undermine))
            {
                if (diplomaticRelation.WarObjective == WarObjective.Undefined)
                {
                    IdentifyEmpireWarObjectives(diplomaticRelation.OtherEmpire, out var targetedColonies, out var targetedBases);
                    if (targetedColonies.Count > 0 || targetedBases.Count > 0)
                    {
                        diplomaticRelation.WarObjective = WarObjective.CaptureObjectives;
                        diplomaticRelation.WarObjectiveColonies = targetedColonies;
                        diplomaticRelation.WarObjectiveBases = targetedBases;
                    }
                    else
                    {
                        diplomaticRelation.WarObjective = WarObjective.EndWar;
                        diplomaticRelation.WarObjectiveColonies = new HabitatList();
                        diplomaticRelation.WarObjectiveBases = new BuiltObjectList();
                    }
                }
                if (diplomaticRelation.WarObjectiveColonies.Count > 0 || diplomaticRelation.WarObjectiveBases.Count > 0)
                {
                    for (int m = 0; m < diplomaticRelation.WarObjectiveColonies.Count; m++)
                    {
                        Habitat habitat2 = diplomaticRelation.WarObjectiveColonies[m];
                        if (habitat2 != null && !habitat2.HasBeenDestroyed && habitat2.Empire != null && habitat2.Empire == empire)
                        {
                            ShipGroupList shipGroupList2 = GenerateOrderedFleetsForTarget(habitat2.Xpos, habitat2.Ypos, includeSmallFleets: false);
                            if (shipGroupList2.Count > 0)
                            {
                                for (int n = 0; n < shipGroupList2.Count; n++)
                                {
                                    ShipGroup shipGroup3 = shipGroupList2[n];
                                    if (shipGroup3.LeadShip != null && shipGroup3.LeadShip.IsAutoControlled && shipGroup3.Ships.Count >= 10 && shipGroup3.Posture == FleetPosture.Attack && shipGroup3.AttackPoint == null && (shipGroup3.Mission == null || shipGroup3.Mission.Type == BuiltObjectMissionType.Undefined || shipGroup3.Mission.Priority == BuiltObjectMissionPriority.Low) && shipGroup3.CheckFleetTargetWithinFuelRangeAndRefuel(habitat2.Xpos, habitat2.Ypos, 0.2))
                                    {
                                        ResourceList requiredFuel = DetermineFuelRequiredForFleet(shipGroup3);
                                        StellarObject stellarObject4 = (shipGroup3.GatherPoint = DecideBestFleetRefuelPoint(habitat2.Xpos, habitat2.Ypos, this, requiredFuel, empire));
                                        shipGroup3.AttackPoint = habitat2;
                                        shipGroup3.PostureRangeSquared = 2304000000.0;
                                        if (ImplementBlockade(habitat2, performAuthorizationCheck: true, shipGroup3))
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (CountShipGroupsAssignedToEmpire(empire, includeSmallFleets: true) >= maximumBlockadesForEmpire)
                        {
                            break;
                        }
                    }
                    for (int num3 = 0; num3 < diplomaticRelation.WarObjectiveBases.Count; num3++)
                    {
                        BuiltObject builtObject2 = diplomaticRelation.WarObjectiveBases[num3];
                        if (builtObject2 != null && !builtObject2.HasBeenDestroyed && builtObject2.Empire != null && builtObject2.Empire == empire && (builtObject2.ParentHabitat == null || builtObject2.ParentHabitat.Empire == null || builtObject2.ParentHabitat.Empire == _Galaxy.IndependentEmpire))
                        {
                            ShipGroupList shipGroupList3 = GenerateOrderedFleetsForTarget(builtObject2.Xpos, builtObject2.Ypos, includeSmallFleets: false);
                            if (shipGroupList3.Count > 0)
                            {
                                for (int num4 = 0; num4 < shipGroupList3.Count; num4++)
                                {
                                    ShipGroup shipGroup4 = shipGroupList3[num4];
                                    if (shipGroup4.LeadShip != null && shipGroup4.LeadShip.IsAutoControlled && shipGroup4.Posture == FleetPosture.Attack && shipGroup4.AttackPoint == null && (shipGroup4.Mission == null || shipGroup4.Mission.Type == BuiltObjectMissionType.Undefined || shipGroup4.Mission.Priority == BuiltObjectMissionPriority.Low) && shipGroup4.CheckFleetTargetWithinFuelRangeAndRefuel(builtObject2.Xpos, builtObject2.Ypos, 0.2))
                                    {
                                        ResourceList requiredFuel2 = DetermineFuelRequiredForFleet(shipGroup4);
                                        StellarObject stellarObject6 = (shipGroup4.GatherPoint = DecideBestFleetRefuelPoint(builtObject2.Xpos, builtObject2.Ypos, this, requiredFuel2, empire));
                                        shipGroup4.AttackPoint = builtObject2;
                                        shipGroup4.PostureRangeSquared = 2304000000.0;
                                        if (ImplementBlockade(builtObject2, shipGroup4))
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (CountShipGroupsAssignedToEmpire(empire, includeSmallFleets: true) >= maximumBlockadesForEmpire)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    ShipGroupList shipGroupList4 = GenerateOrderedFleetsForEmpireTargets(empire, includeSmallFleets: false);
                    for (int num5 = 0; num5 < shipGroupList4.Count; num5++)
                    {
                        ShipGroup shipGroup5 = shipGroupList4[num5];
                        if (shipGroup5.Posture != 0 || !IsShipGroupAvailable(shipGroup5, BuiltObjectMissionPriority.Normal, 0) || shipGroup5.LeadShip == null || !shipGroup5.LeadShip.IsAutoControlled)
                        {
                            continue;
                        }
                        if (CountShipGroupsAssignedToEmpire(empire, includeSmallFleets: true) >= maximumBlockadesForEmpire)
                        {
                            return true;
                        }
                        if (DominantRace.AggressionLevel <= 105 + Galaxy.Rnd.Next(0, 30))
                        {
                            continue;
                        }
                        double num6 = 0.0;
                        Habitat habitat3 = null;
                        for (int num7 = 0; num7 < empire.Colonies.Count; num7++)
                        {
                            Habitat habitat4 = empire.Colonies[num7];
                            if (CheckSystemExplored(habitat4.SystemIndex))
                            {
                                BuiltObject builtObject3 = _Galaxy.DetermineSpacePortAtColony(habitat4);
                                if (builtObject3 != null && builtObject3.CurrentYearsIncome > num6)
                                {
                                    num6 = builtObject3.CurrentYearsIncome;
                                    habitat3 = habitat4;
                                }
                            }
                        }
                        bool flag3 = false;
                        if (habitat3 != null && !habitat3.HasBeenDestroyed && habitat3.Empire != null && habitat3.Empire == empire && shipGroup5.CheckFleetTargetWithinFuelRangeAndRefuel(habitat3.Xpos, habitat3.Ypos, 0.2))
                        {
                            flag3 = ImplementBlockade(habitat3, performAuthorizationCheck: true, shipGroup5);
                        }
                        if (!flag3)
                        {
                            for (int num8 = 0; num8 < empire.Colonies.Count; num8++)
                            {
                                Habitat habitat5 = empire.Colonies[num8];
                                if (habitat5 == null || habitat5.HasBeenDestroyed || habitat5.Empire == null || habitat5.Empire != empire || !CheckSystemExplored(habitat5.SystemIndex))
                                {
                                    continue;
                                }
                                BuiltObject builtObject4 = _Galaxy.DetermineSpacePortAtColony(habitat5);
                                if (builtObject4 != null && Galaxy.Rnd.Next(0, 3) == 1 && shipGroup5.CheckFleetTargetWithinFuelRangeAndRefuel(habitat5.Xpos, habitat5.Ypos, 0.2))
                                {
                                    flag3 = ImplementBlockade(habitat5, performAuthorizationCheck: true, shipGroup5);
                                    if (flag3)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        if (flag3)
                        {
                            continue;
                        }
                        for (int num9 = 0; num9 < empire.MiningStations.Count; num9++)
                        {
                            BuiltObject builtObject5 = empire.MiningStations[num9];
                            if (builtObject5 != null && !builtObject5.HasBeenDestroyed && builtObject5.Empire != null && builtObject5.Empire == empire && builtObject5.ParentHabitat != null && CheckSystemExplored(builtObject5.ParentHabitat.SystemIndex) && shipGroup5.CheckFleetTargetWithinFuelRangeAndRefuel(builtObject5.Xpos, builtObject5.Ypos, 0.2) && ImplementBlockade(builtObject5, shipGroup5))
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void DisbandShipGroup(ShipGroup shipGroup)
        {
            for (int i = 0; i < shipGroup.Ships.Count; i++)
            {
                BuiltObject builtObject = shipGroup.Ships[i];
                builtObject.ShipGroup = null;
            }
            shipGroup.Ships.Clear();
            if (ShipGroups.Contains(shipGroup))
            {
                ShipGroups.Remove(shipGroup);
            }
        }

        private StellarObject SelectWayPoint(PrioritizedTarget target, ResourceList requiredFuel)
        {
            double x = 0.0;
            double y = 0.0;
            target.ResolveTargetCoordinates(out x, out y);
            return DecideBestFleetRefuelPoint(x, y, this, requiredFuel, target.Empire);
        }

        private void WaypointShipGroup(ShipGroup shipGroup, BuiltObject wayPoint)
        {
            for (int i = 0; i < shipGroup.Ships.Count; i++)
            {
                BuiltObject builtObject = shipGroup.Ships[i];
                builtObject.AssignMission(BuiltObjectMissionType.Waypoint, wayPoint, null, BuiltObjectMissionPriority.High);
            }
        }

        private void WaypointShipGroup(ShipGroup shipGroup, Habitat wayPoint)
        {
            for (int i = 0; i < shipGroup.Ships.Count; i++)
            {
                BuiltObject builtObject = shipGroup.Ships[i];
                builtObject.AssignMission(BuiltObjectMissionType.Waypoint, wayPoint, null, BuiltObjectMissionPriority.High);
            }
        }

        private void WaypointShipGroup(ShipGroup shipGroup, double x, double y)
        {
            if (x <= 2000000000.0 || y <= 2000000000.0)
            {
                throw new ArgumentException("Invalid waypoint location");
            }
            foreach (BuiltObject ship in shipGroup.Ships)
            {
                ship.AssignMission(BuiltObjectMissionType.Waypoint, null, null, x, y, BuiltObjectMissionPriority.High);
            }
        }

        public BuiltObject FindNearestTroopShipWithSpace(double x, double y, int capacityRequired)
        {
            BuiltObject builtObject = null;
            double num = double.MaxValue;
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject2 = BuiltObjects[i];
                if (builtObject2 != null && !builtObject2.HasBeenDestroyed && builtObject2.Role == BuiltObjectRole.Military && builtObject2.TroopCapacity >= capacityRequired && builtObject2.TroopCapacityRemaining >= capacityRequired)
                {
                    double num2 = _Galaxy.CalculateDistanceSquared(x, y, builtObject2.Xpos, builtObject2.Ypos);
                    if (builtObject == null || num > num2)
                    {
                        builtObject = builtObject2;
                        num = num2;
                    }
                }
            }
            return builtObject;
        }

        private BuiltObject FindAvailableMilitaryShip(double x, double y, BuiltObjectList militaryShips, bool mustCarryTroopsIfHaveTroopStorage, bool mustBeFullOfTroops, float minimumFuelPortion, bool? mustHaveHyperdrive)
        {
            for (int i = 0; i < militaryShips.Count; i++)
            {
                BuiltObject builtObject = militaryShips[i];
                if (builtObject.ShipGroup != null || builtObject.SubRole == BuiltObjectSubRole.Escort || builtObject.SubRole == BuiltObjectSubRole.ResupplyShip || builtObject.BuiltAt != null || (builtObject.Mission != null && builtObject.Mission.Type != 0 && builtObject.Mission.Priority != 0 && builtObject.Mission.Priority != BuiltObjectMissionPriority.Low && builtObject.Mission.Priority != BuiltObjectMissionPriority.Normal) || !builtObject.IsAutoControlled)
                {
                    continue;
                }
                bool flag = true;
                if (mustHaveHyperdrive.HasValue)
                {
                    if (mustHaveHyperdrive.Value)
                    {
                        if (builtObject.WarpSpeed <= 0)
                        {
                            flag = false;
                        }
                    }
                    else if (builtObject.WarpSpeed > 0)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    continue;
                }
                float num = (float)(builtObject.CurrentFuel / (double)builtObject.FuelCapacity);
                if (!(num >= minimumFuelPortion))
                {
                    continue;
                }
                bool flag2 = true;
                if (x >= 0.0 && y >= 0.0)
                {
                    flag2 = builtObject.WithinFuelRange(x, y, 0.0);
                }
                if (!flag2)
                {
                    continue;
                }
                if (mustBeFullOfTroops)
                {
                    if (builtObject.Troops != null && builtObject.Troops.TotalAttackStrength > 0 && builtObject.TroopCapacityRemaining < 100)
                    {
                        return builtObject;
                    }
                    continue;
                }
                if (mustCarryTroopsIfHaveTroopStorage)
                {
                    if (builtObject.TroopCapacity <= 0)
                    {
                        return builtObject;
                    }
                    if (builtObject.Troops == null || builtObject.Troops.TotalAttackStrength <= 0)
                    {
                        continue;
                    }
                    int num2 = builtObject.TroopCapacity / 2;
                    if (builtObject.TroopCapacityRemaining <= num2)
                    {
                        return builtObject;
                    }
                }
                else if (builtObject.SubRole == BuiltObjectSubRole.TroopTransport)
                {
                    continue;
                }
                return builtObject;
            }
            return null;
        }
    }
}
