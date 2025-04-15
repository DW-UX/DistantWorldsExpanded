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
        public BuiltObjectMissionType DetermineDestroyOrCaptureTarget(BuiltObject attacker, BuiltObject target, bool attackingAsGroup)
        {
            if (attacker != null)
            {
                return DetermineDestroyOrCaptureTarget(attacker.CalculateAvailableAssaultPodAttackStrength(_Galaxy.CurrentDateTime), attacker.CalculateOverallStrengthFactor(), attacker.Empire, target, attackingAsGroup, attacker);
            }
            return BuiltObjectMissionType.Attack;
        }

        public BuiltObjectMissionType DetermineDestroyOrCaptureTarget(ShipGroup attackingFleet, BuiltObject target)
        {
            if (attackingFleet != null)
            {
                int assaultPodCount = 0;
                int assaultPodsAvailable = 0;
                int assaultStrength = attackingFleet.CalculateAssaultPodAttackValues(_Galaxy.CurrentDateTime, out assaultPodCount, out assaultPodsAvailable);
                return DetermineDestroyOrCaptureTarget(assaultStrength, attackingFleet.TotalOverallStrengthFactor, attackingFleet.Empire, target, attackingAsGroup: true, attackingFleet.LeadShip);
            }
            return BuiltObjectMissionType.Attack;
        }

        public BuiltObjectMissionType DetermineDestroyOrCaptureTarget(int assaultStrength, int attackingOverallStrength, Empire attackingEmpire, BuiltObject target, bool attackingAsGroup, BuiltObject builtObjectToExclude)
        {
            if (target != null && attackingEmpire != null)
            {
                if (assaultStrength > 0)
                {
                    bool flag = false;
                    int fixedDefenseValue = 0;
                    int num = target.CalculateBoardingDefenseValue(_Galaxy.CurrentDateTime, out fixedDefenseValue);
                    if (num <= assaultStrength || attackingAsGroup)
                    {
                        flag = true;
                    }
                    if (target.Role == BuiltObjectRole.Base)
                    {
                        int num2 = _Galaxy.CheckSystemOwnershipId(target.NearestSystemStar);
                        if ((target.SubRole == BuiltObjectSubRole.Outpost || target.SubRole == BuiltObjectSubRole.SmallSpacePort || target.SubRole == BuiltObjectSubRole.MediumSpacePort || target.SubRole == BuiltObjectSubRole.LargeSpacePort || target.SubRole == BuiltObjectSubRole.DefensiveBase || target.SubRole == BuiltObjectSubRole.EnergyResearchStation || target.SubRole == BuiltObjectSubRole.HighTechResearchStation || target.SubRole == BuiltObjectSubRole.WeaponsResearchStation) && target.ParentHabitat != null && target.ParentHabitat.Empire != null)
                        {
                            return BuiltObjectMissionType.Attack;
                        }
                        if (CheckOurEmpireBoarding(target, builtObjectToExclude))
                        {
                            return BuiltObjectMissionType.Capture;
                        }
                        if (this != _Galaxy.PlayerEmpire && target.UnbuiltComponentCount > 0)
                        {
                            double num3 = (double)target.UnbuiltComponentCount / (double)target.Components.Count;
                            if (num3 > 0.25)
                            {
                                return BuiltObjectMissionType.Attack;
                            }
                        }
                        switch (Policy.CaptureTargetConditionBase)
                        {
                            case 0:
                                return BuiltObjectMissionType.Attack;
                            case 1:
                                if (flag && num2 == attackingEmpire.EmpireId)
                                {
                                    return BuiltObjectMissionType.Capture;
                                }
                                break;
                            case 2:
                                if (flag && (num2 == attackingEmpire.EmpireId || num2 < 0))
                                {
                                    return BuiltObjectMissionType.Capture;
                                }
                                break;
                            case 3:
                                if (flag)
                                {
                                    return BuiltObjectMissionType.Capture;
                                }
                                break;
                            case 4:
                                return BuiltObjectMissionType.Capture;
                        }
                    }
                    else
                    {
                        if (CheckOurEmpireBoarding(target, builtObjectToExclude))
                        {
                            return BuiltObjectMissionType.Capture;
                        }
                        if (builtObjectToExclude != null && builtObjectToExclude.Role == BuiltObjectRole.Base)
                        {
                            double num4 = _Galaxy.CalculateDistanceSquared(builtObjectToExclude.Xpos, builtObjectToExclude.Ypos, target.Xpos, target.Ypos);
                            if (num4 > (double)builtObjectToExclude.AssaultRange * (double)builtObjectToExclude.AssaultRange)
                            {
                                return BuiltObjectMissionType.Attack;
                            }
                        }
                        if (target.WarpSpeed <= 0 && Policy.CaptureTargetConditionShip < 2)
                        {
                            return BuiltObjectMissionType.Attack;
                        }
                        switch (Policy.CaptureTargetConditionShip)
                        {
                            case 0:
                                return BuiltObjectMissionType.Attack;
                            case 1:
                                if (flag && (target.Size > MaximumConstructionSize(target.SubRole) || Galaxy.ResolveTechBonusFactor(this, _Galaxy, target) > 1.0))
                                {
                                    return BuiltObjectMissionType.Capture;
                                }
                                break;
                            case 2:
                                if (flag)
                                {
                                    return BuiltObjectMissionType.Capture;
                                }
                                break;
                            case 3:
                                return BuiltObjectMissionType.Capture;
                        }
                    }
                }
                else if (CheckOurEmpireBoarding(target, builtObjectToExclude))
                {
                    return BuiltObjectMissionType.Capture;
                }
            }
            return BuiltObjectMissionType.Attack;
        }

        public bool CheckOurEmpireOverwhelmingBoarding(BuiltObject target)
        {
            if (target != null && target.AssaultAttackValue > 0 && target.AssaultAttackEmpireId == EmpireId)
            {
                float num = (float)target.AssaultAttackValue / (float)target.AssaultDefenseValue;
                if (num > 2f)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckOurEmpireBoarding(BuiltObject target)
        {
            return CheckOurEmpireBoarding(target, null);
        }

        public bool CheckOurEmpireBoarding(BuiltObject target, BuiltObject builtObjectToExclude)
        {
            if (target != null && target.AssaultAttackValue > 0 && target.AssaultAttackEmpireId == EmpireId)
            {
                return true;
            }
            if (target.Threats != null && target.Threats.Length > 0)
            {
                for (int i = 0; i < target.Threats.Length; i++)
                {
                    StellarObject stellarObject = target.Threats[i];
                    if (stellarObject == null || !(stellarObject is BuiltObject))
                    {
                        continue;
                    }
                    BuiltObject builtObject = (BuiltObject)stellarObject;
                    if (builtObject == null || builtObject.HasBeenDestroyed || builtObject.Empire != this)
                    {
                        continue;
                    }
                    if (builtObjectToExclude == null || builtObject != builtObjectToExclude)
                    {
                        BuiltObjectMission mission = builtObject.Mission;
                        if (mission != null && mission.Type == BuiltObjectMissionType.Capture && mission.TargetBuiltObject == target)
                        {
                            return true;
                        }
                    }
                    if (builtObject.AssaultStrength <= 0 || builtObject.AssaultRange <= 0 || builtObject.Weapons == null)
                    {
                        continue;
                    }
                    for (int j = 0; j < builtObject.Weapons.Count; j++)
                    {
                        Weapon weapon = builtObject.Weapons[j];
                        if (weapon != null && weapon.Component != null && weapon.Component.Type == ComponentType.AssaultPod && weapon.Target != null && weapon.Target == target)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private HabitatList PirateDetermineOwnedColonies()
        {
            HabitatList habitatList = new HabitatList();
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat != null && !habitat.HasBeenDestroyed && habitat.Empire == this)
                {
                    habitatList.Add(habitat);
                }
            }
            return habitatList;
        }

        private void PirateDoConstruction()
        {
            int refusalCount = 0;
            List<CargoList> builtObjectResourcesToOrder = new List<CargoList>();
            BuiltObjectList builtObjectConstructionYards = new BuiltObjectList();
            List<CargoList> colonyResourcesToOrder = new List<CargoList>();
            HabitatList colonyConstructionYards = new HabitatList();
            CargoList resourcesToOrder = null;
            double purchaseCost = 0.0;
            double stateMoney = _StateMoney;
            stateMoney -= CalculateCashReservesForNewPirateFacilities();
            stateMoney -= CalculateCashReservesForNewMiningStation();
            double num = CalculatePirateIncome();
            double num2 = CalculatePirateExpenses(includeShipsUnderConstruction: true);
            double num3 = 2.0;
            double num4 = 0.0;
            if (stateMoney > 0.0)
            {
                num4 = stateMoney / num3;
            }
            double num5 = num + num4 - num2;
            bool flag = false;
            if (DominantRace != null && !DominantRace.Expanding)
            {
                flag = false;
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
            if (habitatList2.Count <= 0)
            {
                flag = false;
            }
            if (flag && _ControlColonization != 0)
            {
                HabitatList habitatList3 = DetermineHabitatsBeingColonized();
                _ColonizationTargets.Sort();
                _ColonizationTargets.Reverse();
                List<HabitatType> list = ColonizableHabitatTypesForEmpire(this);
                for (int j = 0; j < _ColonizationTargets.Count; j++)
                {
                    HabitatPrioritization habitatPrioritization = _ColonizationTargets[j];
                    if (!CheckShouldAttemptColonization(habitatPrioritization.Habitat) || habitatPrioritization.AssignedShip != null || habitatList3.Contains(habitatPrioritization.Habitat) || (habitatPrioritization.Habitat.Empire != null && habitatPrioritization.Habitat.Empire != _Galaxy.IndependentEmpire))
                    {
                        continue;
                    }
                    bool flag2 = false;
                    for (int k = 0; k < BuiltObjects.Count; k++)
                    {
                        BuiltObject builtObject = BuiltObjects[k];
                        if (builtObject.Role == BuiltObjectRole.Colony && (builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined))
                        {
                            int newPopulationAmount = 0;
                            if (CanBuiltObjectColonizeHabitat(builtObject, habitatPrioritization.Habitat, out newPopulationAmount) && habitatPrioritization.Priority >= Galaxy.HabitatColonizationThreshhold && CheckTaskAuthorized(_ControlColonization, ref refusalCount, GenerateAutomationMessageColonization(habitatPrioritization.Habitat, builtObject, null), habitatPrioritization.Habitat, AdvisorMessageType.Colonization, builtObject, null))
                            {
                                habitatPrioritization.AssignedShip = builtObject;
                                builtObject.AssignMission(BuiltObjectMissionType.Colonize, habitatPrioritization.Habitat, null, BuiltObjectMissionPriority.Normal);
                                flag2 = true;
                                break;
                            }
                        }
                    }
                    if (flag2)
                    {
                        continue;
                    }
                    Design design = _Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
                    if (design == null)
                    {
                        design = _Designs.FindNewest(BuiltObjectSubRole.ColonyShip);
                    }
                    if (design == null)
                    {
                        continue;
                    }
                    bool flag3 = CanDesignColonizeHabitat(design, habitatPrioritization.Habitat);
                    if ((!flag3 && !list.Contains(habitatPrioritization.Habitat.Type)) || habitatPrioritization.Priority < Galaxy.HabitatColonizationThreshhold)
                    {
                        continue;
                    }
                    double num6 = design.CalculateCurrentPurchasePrice(_Galaxy);
                    if (!(purchaseCost + num6 <= StateMoney))
                    {
                        continue;
                    }
                    design.BuildCount++;
                    BuiltObject builtObject2 = new BuiltObject(design, _Galaxy.GenerateBuiltObjectName(design), _Galaxy);
                    builtObject2.PurchasePrice = num6;
                    Habitat habitat2 = null;
                    double shortestWaitQueueTime;
                    if (flag3)
                    {
                        habitat2 = habitatList2.FindShortestConstructionWaitQueue(builtObject2, out shortestWaitQueueTime);
                    }
                    else
                    {
                        HabitatList habitatList4 = new HabitatList();
                        foreach (Habitat colony in Colonies)
                        {
                            Race dominantRace = colony.Population.DominantRace;
                            if (dominantRace != null && dominantRace.NativeHabitatType == habitatPrioritization.Habitat.Type && colony.Empire == this && colony.Population != null && colony.Population.TotalAmount >= Galaxy.BuildColonyShipPopulationRequirement)
                            {
                                habitatList4.Add(colony);
                            }
                        }
                        habitat2 = habitatList4.FindShortestConstructionWaitQueue(builtObject2, out shortestWaitQueueTime);
                    }
                    double num7 = shortestWaitQueueTime / (double)Galaxy.RealSecondsInGalacticYear;
                    if (habitat2 != null && num7 < Galaxy.MaximumConstructionQueueWaitTimeYears)
                    {
                        if (CheckTaskAuthorized(_ControlColonization, ref refusalCount, GenerateAutomationMessageColonization(habitatPrioritization.Habitat, null, habitat2), habitatPrioritization.Habitat, AdvisorMessageType.Colonization, habitat2, null))
                        {
                            if (habitat2.ConstructionQueue != null && habitat2.ConstructionQueue.AddBuiltObjectToConstruct(builtObject2))
                            {
                                builtObject2.Name = _Galaxy.GenerateBuiltObjectName(design, habitat2);
                                habitatPrioritization.AssignedShip = builtObject2;
                                AddBuiltObjectToGalaxy(builtObject2, habitat2, offsetLocationFromParent: false, isStateOwned: true);
                                purchaseCost += num6;
                                builtObject2.AssignMission(BuiltObjectMissionType.Colonize, habitatPrioritization.Habitat, null, BuiltObjectMissionPriority.Normal);
                                builtObject2.BuiltAt = habitat2;
                                ProcureConstructionComponents(builtObject2, habitat2, out resourcesToOrder);
                                colonyResourcesToOrder.Add(resourcesToOrder);
                                colonyConstructionYards.Add(habitat2);
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
            double num8 = 0.0;
            if (_ControlStateConstruction != 0 && habitatList.Count > 0)
            {
                int num9 = 0;
                for (int l = 0; l < BuiltObjects.Count; l++)
                {
                    BuiltObject builtObject3 = BuiltObjects[l];
                    if (builtObject3.SubRole == BuiltObjectSubRole.Outpost || builtObject3.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject3.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject3.SubRole == BuiltObjectSubRole.LargeSpacePort)
                    {
                        num9++;
                    }
                }
                int num10 = 1 + (int)((double)Colonies.Count / 3.0);
                int newSpacePortAmount = num10 - num9;
                HabitatList habitatList5 = DetermineNewSpacePortLocations(habitatList, newSpacePortAmount, excludeColoniesWithEnemiesPresent: true);
                long num11 = (long)Policy.ConstructionSpaceportLargeColonyPopulationThreshold * 1000000L;
                long num12 = (long)Policy.ConstructionSpaceportMediumColonyPopulationThreshold * 1000000L;
                long num13 = (long)Policy.ConstructionSpaceportSmallColonyPopulationThreshold * 1000000L;
                long num131 = (long)Policy.ConstructionOutpostColonyPopulationThreshold * 1000000L;
                foreach (Habitat item in habitatList5)
                {
                    Design design2 = null;
                    if (item.Population.TotalAmount > num11)
                    {
                        design2 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.LargeSpacePort);
                    }
                    else if (item.Population.TotalAmount > num12)
                    {
                        design2 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.MediumSpacePort);
                    }
                    else if (item.Population.TotalAmount > num13)
                    {
                        design2 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.SmallSpacePort);
                    }
                    else if (item.Population.TotalAmount > num131)
                    {
                        design2 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.Outpost);
                    }
                    if (design2 == null)
                    {
                        continue;
                    }
                    double num14 = design2.CalculateCurrentPurchasePrice(_Galaxy);
                    if (!((purchaseCost + num14) / 8.0 <= num5) || !(purchaseCost + num14 <= StateMoney))
                    {
                        continue;
                    }
                    design2.BuildCount++;
                    BuiltObject builtObject4 = new BuiltObject(design2, item.Name + " " + TextResolver.GetText("Space Port"), _Galaxy);
                    builtObject4.PurchasePrice = num14;
                    if (CheckTaskAuthorized(_ControlStateConstruction, ref refusalCount, GenerateAutomationMessageConstruction(builtObject4, item, num14), item, AdvisorMessageType.BuildOneOff, design2, null))
                    {
                        if (item.ConstructionQueue != null && item.ConstructionQueue.AddBuiltObjectToConstruct(builtObject4))
                        {
                            builtObject4.ParentHabitat = item;
                            _Galaxy.SelectRelativeHabitatSurfacePoint(item, out var x, out var y);
                            builtObject4.ParentOffsetX = x;
                            builtObject4.ParentOffsetY = y;
                            builtObject4.Heading = _Galaxy.SelectRandomHeading();
                            builtObject4.TargetHeading = builtObject4.Heading;
                            builtObject4.NearestSystemStar = Galaxy.DetermineHabitatSystemStar(item);
                            AddBuiltObjectToGalaxy(builtObject4, item, offsetLocationFromParent: false, isStateOwned: true);
                            builtObject4.BuiltAt = item;
                            purchaseCost += num14;
                            num8 += CalculateSupportCost(design2);
                            ProcureConstructionComponents(builtObject4, item, out resourcesToOrder);
                            colonyResourcesToOrder.Add(resourcesToOrder);
                            colonyConstructionYards.Add(item);
                        }
                        else
                        {
                            design2.BuildCount--;
                        }
                    }
                    else
                    {
                        design2.BuildCount--;
                    }
                }
            }
            double num15 = 0.0;
            ForceStructureProjectionList stateForceStructureProjections = StateForceStructureProjections;
            num15 += num8;
            ForceStructureProjectionList forceStructureProjectionList = new ForceStructureProjectionList();
            forceStructureProjectionList.AddRange(stateForceStructureProjections);
            if (_ControlStateConstruction != 0)
            {
                ForceStructureProjectionList projections = PrivateForceStructureProjections.Clone();
                projections = RefactorForceStructureProjectionsToCosts(projections, includeCashflowCheck: false);
                forceStructureProjectionList.AddRange(projections);
            }
            double num16 = 0.0;
            foreach (ForceStructureProjection item2 in forceStructureProjectionList)
            {
                Design design3 = Designs.FindNewestCanBuild(item2.SubRole);
                if (design3 != null)
                {
                    double num17 = design3.CalculateCurrentPurchasePrice(_Galaxy);
                    num16 += num17 * (double)item2.Amount;
                }
            }
            if (num16 > 0.0)
            {
                if (_ControlStateConstruction == AutomationLevel.SemiAutomated)
                {
                    EmpireMessage empireMessage = new EmpireMessage(this, EmpireMessageType.AdvisorSuggestion, null);
                    empireMessage.AdvisorMessageType = AdvisorMessageType.BuildOrder;
                    empireMessage.Description = string.Format(TextResolver.GetText("Build new ships for X credits"), num16.ToString("###,###,###,##0"));
                    empireMessage.StarDate = _Galaxy.CurrentStarDate;
                    SendMessageToEmpire(empireMessage, this);
                }
                else if (_ControlStateConstruction == AutomationLevel.FullyAutomated)
                {
                    for (int m = 0; m < forceStructureProjectionList.Count; m++)
                    {
                        ForceStructureProjection forceStructureProjection = forceStructureProjectionList[m];
                        if (forceStructureProjection == null || forceStructureProjection.Amount <= 0)
                        {
                            continue;
                        }
                        Design design4 = Designs.FindNewestCanBuild(forceStructureProjection.SubRole);
                        if (design4 != null)
                        {
                            if (design4.SubRole == BuiltObjectSubRole.ConstructionShip || design4.SubRole == BuiltObjectSubRole.ColonyShip || design4.SubRole == BuiltObjectSubRole.ResupplyShip)
                            {
                                PirateBuildNewShipsAtColony(design4, forceStructureProjection.Amount, stateMoney, ref purchaseCost, habitatList, ref resourcesToOrder, ref colonyResourcesToOrder, ref colonyConstructionYards);
                            }
                            else
                            {
                                PirateBuildNewShips(design4, forceStructureProjection.Amount, stateMoney, ref purchaseCost, ref resourcesToOrder, ref builtObjectResourcesToOrder, ref builtObjectConstructionYards);
                            }
                        }
                    }
                    StateMoney -= purchaseCost;
                    PirateEconomy.PerformExpense(purchaseCost, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                }
            }
            if (builtObjectResourcesToOrder.Count <= 0 && colonyResourcesToOrder.Count <= 0)
            {
                return;
            }
            BuiltObjectList builtObjectList = new BuiltObjectList();
            foreach (BuiltObject item3 in builtObjectConstructionYards)
            {
                if (!builtObjectList.Contains(item3))
                {
                    builtObjectList.Add(item3);
                }
            }
            foreach (BuiltObject item4 in builtObjectList)
            {
                CargoList cargoList = new CargoList();
                for (int n = 0; n < builtObjectConstructionYards.Count; n++)
                {
                    if (builtObjectConstructionYards[n] != item4)
                    {
                        continue;
                    }
                    foreach (Cargo item5 in builtObjectResourcesToOrder[n])
                    {
                        cargoList.Add(item5);
                    }
                }
                foreach (Cargo item6 in cargoList)
                {
                    CreateOrder(item4, item6.CommodityResource, item6.Amount, isState: false, OrderType.ConstructionShortage);
                }
            }
            HabitatList habitatList6 = new HabitatList();
            foreach (Habitat item7 in colonyConstructionYards)
            {
                if (!habitatList6.Contains(item7))
                {
                    habitatList6.Add(item7);
                }
            }
            foreach (Habitat item8 in habitatList6)
            {
                CargoList cargoList2 = new CargoList();
                for (int num18 = 0; num18 < colonyConstructionYards.Count; num18++)
                {
                    if (colonyConstructionYards[num18] != item8)
                    {
                        continue;
                    }
                    foreach (Cargo item9 in colonyResourcesToOrder[num18])
                    {
                        cargoList2.Add(item9);
                    }
                }
                foreach (Cargo item10 in cargoList2)
                {
                    CreateOrder(item8, item10.CommodityResource, item10.Amount, isState: false, OrderType.ConstructionShortage);
                }
            }
        }

        private void PirateBuildNewShips(Design design, int amount, double availableCash, ref double purchaseCost, ref CargoList resourcesToOrder, ref List<CargoList> builtObjectResourcesToOrder, ref BuiltObjectList builtObjectConstructionYards)
        {
            double num = design.CalculateCurrentPurchasePrice(_Galaxy);
            for (int i = 0; i < amount; i++)
            {
                if (!(purchaseCost + num < availableCash))
                {
                    continue;
                }
                design.BuildCount++;
                BuiltObject builtObject = new BuiltObject(design, _Galaxy.GenerateBuiltObjectName(design, null, uniqueNamesForSmallMilitaryShips: true), _Galaxy);
                builtObject.PurchasePrice = num;
                double shortestWaitQueueTime;
                BuiltObject builtObject2 = ConstructionYards.FindShortestConstructionWaitQueue(builtObject, out shortestWaitQueueTime, includeVerySmallYards: true, 2);
                double num2 = shortestWaitQueueTime / (double)Galaxy.RealSecondsInGalacticYear;
                if (builtObject2 != null && num2 < Galaxy.MaximumConstructionQueueWaitTimeYears)
                {
                    if (builtObject2.ConstructionQueue != null && builtObject2.ConstructionQueue.AddBuiltObjectToConstruct(builtObject))
                    {
                        purchaseCost += num;
                        if (builtObject2.ParentHabitat != null)
                        {
                            builtObject.Name = _Galaxy.GenerateBuiltObjectName(design, builtObject2.ParentHabitat, uniqueNamesForSmallMilitaryShips: true);
                        }
                        bool flag = true;
                        switch (design.SubRole)
                        {
                            case BuiltObjectSubRole.SmallFreighter:
                            case BuiltObjectSubRole.MediumFreighter:
                            case BuiltObjectSubRole.LargeFreighter:
                            case BuiltObjectSubRole.PassengerShip:
                            case BuiltObjectSubRole.GasMiningShip:
                            case BuiltObjectSubRole.MiningShip:
                                flag = false;
                                break;
                        }
                        AddBuiltObjectToGalaxy(builtObject, builtObject2, offsetLocationFromParent: false, flag);
                        builtObject.BuiltAt = builtObject2;
                        builtObject.IsAutoControlled = NewBuiltObjectShouldBeAutomated(builtObject.SubRole);
                        if (!flag || design.SubRole == BuiltObjectSubRole.ResortBase)
                        {
                            builtObject.Empire = _Galaxy.IndependentEmpire;
                            if (!_Galaxy.IndependentEmpire.PrivateBuiltObjects.Contains(builtObject))
                            {
                                _Galaxy.IndependentEmpire.PrivateBuiltObjects.Add(builtObject);
                            }
                        }
                        ProcureConstructionComponents(builtObject, builtObject2, orderPreciseResourceAmounts: true, out resourcesToOrder);
                        builtObjectResourcesToOrder.Add(resourcesToOrder);
                        builtObjectConstructionYards.Add(builtObject2);
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

        public bool NewBuiltObjectShouldBeAutomated(BuiltObjectSubRole subRole)
        {
            switch (subRole)
            {
                case BuiltObjectSubRole.Escort:
                case BuiltObjectSubRole.Frigate:
                case BuiltObjectSubRole.Destroyer:
                case BuiltObjectSubRole.Cruiser:
                case BuiltObjectSubRole.CapitalShip:
                case BuiltObjectSubRole.TroopTransport:
                case BuiltObjectSubRole.Carrier:
                case BuiltObjectSubRole.ResupplyShip:
                case BuiltObjectSubRole.ExplorationShip:
                case BuiltObjectSubRole.ColonyShip:
                case BuiltObjectSubRole.ConstructionShip:
                    return NewShipsAutomated;
                default:
                    return true;
            }
        }

        private void PirateBuildNewShipsAtColony(Design design, int amount, double availableCash, ref double purchaseCost, HabitatList colonies, ref CargoList resourcesToOrder, ref List<CargoList> colonyResourcesToOrder, ref HabitatList colonyConstructionYards)
        {
            double num = design.CalculateCurrentPurchasePrice(_Galaxy);
            for (int i = 0; i < amount; i++)
            {
                if (!(purchaseCost + num < availableCash))
                {
                    continue;
                }
                design.BuildCount++;
                BuiltObject builtObject = new BuiltObject(design, _Galaxy.GenerateBuiltObjectName(design, null, uniqueNamesForSmallMilitaryShips: true), _Galaxy);
                builtObject.PurchasePrice = num;
                double shortestWaitQueueTime;
                Habitat habitat = colonies.FindShortestConstructionWaitQueue(builtObject, out shortestWaitQueueTime);
                double num2 = shortestWaitQueueTime / (double)Galaxy.RealSecondsInGalacticYear;
                if (habitat != null && num2 < Galaxy.MaximumConstructionQueueWaitTimeYears)
                {
                    if (habitat.ConstructionQueue != null && habitat.ConstructionQueue.AddBuiltObjectToConstruct(builtObject))
                    {
                        purchaseCost += num;
                        if (habitat.ParentHabitat != null)
                        {
                            builtObject.Name = _Galaxy.GenerateBuiltObjectName(design, habitat, uniqueNamesForSmallMilitaryShips: true);
                        }
                        bool flag = true;
                        switch (design.SubRole)
                        {
                            case BuiltObjectSubRole.SmallFreighter:
                            case BuiltObjectSubRole.MediumFreighter:
                            case BuiltObjectSubRole.LargeFreighter:
                            case BuiltObjectSubRole.PassengerShip:
                            case BuiltObjectSubRole.GasMiningShip:
                            case BuiltObjectSubRole.MiningShip:
                                flag = false;
                                break;
                        }
                        AddBuiltObjectToGalaxy(builtObject, habitat, offsetLocationFromParent: false, flag);
                        builtObject.BuiltAt = habitat;
                        builtObject.IsAutoControlled = NewBuiltObjectShouldBeAutomated(builtObject.SubRole);
                        if (!flag)
                        {
                            builtObject.Empire = _Galaxy.IndependentEmpire;
                            if (!_Galaxy.IndependentEmpire.PrivateBuiltObjects.Contains(builtObject))
                            {
                                _Galaxy.IndependentEmpire.PrivateBuiltObjects.Add(builtObject);
                            }
                        }
                        ProcureConstructionComponents(builtObject, habitat, out resourcesToOrder);
                        colonyResourcesToOrder.Add(resourcesToOrder);
                        colonyConstructionYards.Add(habitat);
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

        private double CalculateCashReservesForNewMiningStation()
        {
            double result = 0.0;
            if (Designs != null)
            {
                Design design = Designs.FindNewestCanBuild(BuiltObjectSubRole.MiningStation);
                if (design != null)
                {
                    result = design.CalculateCurrentPurchasePrice(_Galaxy);
                }
            }
            return result;
        }

        private double CalculateCashReservesForNewPirateFacilities()
        {
            double num = 0.0;
            if (PirateEmpireBaseHabitat != null)
            {
                PlanetaryFacilityDefinition planetaryFacility = Galaxy.PlanetaryFacilityDefinitionsStatic[25];
                PlanetaryFacilityDefinition planetaryFacility2 = Galaxy.PlanetaryFacilityDefinitionsStatic[26];
                PlanetaryFacilityDefinition planetaryFacility3 = Galaxy.PlanetaryFacilityDefinitionsStatic[32];
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat = Colonies[i];
                    if (habitat == null || habitat.HasBeenDestroyed || habitat.Empire == this)
                    {
                        continue;
                    }
                    PirateColonyControl byFacilityControl = habitat.GetPirateControl().GetByFacilityControl();
                    if (byFacilityControl == null)
                    {
                        PirateColonyControl byFaction = habitat.GetPirateControl().GetByFaction(this);
                        if (byFaction != null && byFaction.ControlLevel >= 0.5f)
                        {
                            num = Math.Max(num, Galaxy.CalculatePlanetaryFacilityCost(planetaryFacility, this));
                        }
                    }
                    else if (byFacilityControl.EmpireId == EmpireId)
                    {
                        int num2 = habitat.Facilities.CountCompletedByType(PlanetaryFacilityType.PirateBase);
                        int num3 = habitat.Facilities.CountCompletedByType(PlanetaryFacilityType.PirateFortress);
                        int num4 = habitat.Facilities.CountCompletedByType(PlanetaryFacilityType.PirateCriminalNetwork);
                        if (num2 > 0 && num3 <= 0)
                        {
                            num = Math.Max(num, Galaxy.CalculatePlanetaryFacilityCost(planetaryFacility2, this));
                        }
                        else if (num3 > 0 && num4 <= 0 && CountPirateCriminalNetworks() <= 0)
                        {
                            num = Math.Max(num, Galaxy.CalculatePlanetaryFacilityCost(planetaryFacility3, this));
                        }
                    }
                }
            }
            return num;
        }

        private void PirateProjectForces(long starDate)
        {
            double stateMoney = _StateMoney;
            stateMoney -= CalculateCashReservesForNewPirateFacilities();
            stateMoney -= CalculateCashReservesForNewMiningStation();
            double num = stateMoney * 0.6;
            double num2 = stateMoney - num;
            double num3 = CalculatePirateIncome();
            double num4 = CalculatePirateExpenses(includeShipsUnderConstruction: true);
            double num5 = 2.0;
            double num6 = 0.0;
            if (stateMoney > 0.0)
            {
                num6 = stateMoney / num5;
            }
            double num7 = num3 + num6 - num4;
            double num8 = 40000.0;
            double num9 = 100000.0;
            double num10 = 0.8;
            if (num7 > num8)
            {
                double num11 = num9 - num8;
                _ = (num9 - num7) / num11;
                num7 = Math.Max(num8, num7 * num10);
            }
            HabitatList habitatList = PirateDetermineOwnedColonies();
            int num12 = BuiltObjects.CountBySubRole(BuiltObjectSubRole.Escort);
            int num13 = BuiltObjects.CountBySubRole(BuiltObjectSubRole.Frigate);
            int num14 = BuiltObjects.CountBySubRole(BuiltObjectSubRole.Destroyer);
            int num15 = BuiltObjects.CountBySubRole(BuiltObjectSubRole.Cruiser);
            int num16 = BuiltObjects.CountBySubRole(BuiltObjectSubRole.CapitalShip);
            int num17 = BuiltObjects.CountBySubRole(BuiltObjectSubRole.Carrier);
            int num18 = BuiltObjects.CountBySubRole(BuiltObjectSubRole.TroopTransport);
            int num19 = BuiltObjects.CountBySubRole(BuiltObjectSubRole.ExplorationShip);
            int num20 = BuiltObjects.CountBySubRole(BuiltObjectSubRole.ConstructionShip);
            int num21 = num12 + num13 + num14 + num15 + num16 + num17 + num18;
            Design design = Designs.FindNewestCanBuild(BuiltObjectSubRole.Escort);
            Design design2 = Designs.FindNewestCanBuild(BuiltObjectSubRole.Frigate);
            Design design3 = Designs.FindNewestCanBuild(BuiltObjectSubRole.Destroyer);
            Design design4 = Designs.FindNewestCanBuild(BuiltObjectSubRole.Cruiser);
            Design design5 = Designs.FindNewestCanBuild(BuiltObjectSubRole.CapitalShip);
            Design design6 = Designs.FindNewestCanBuild(BuiltObjectSubRole.Carrier);
            Design design7 = Designs.FindNewestCanBuild(BuiltObjectSubRole.TroopTransport);
            Design design8 = Designs.FindNewestCanBuild(BuiltObjectSubRole.ExplorationShip);
            Design design9 = Designs.FindNewestCanBuild(BuiltObjectSubRole.ConstructionShip);
            if (habitatList.Count <= 0)
            {
                design7 = null;
            }
            float num22 = 0f;
            double num23 = 0.0;
            double num24 = 0.0;
            if (design != null)
            {
                num22 += Policy.ConstructionMilitaryEscort;
                num23 += (double)Policy.ConstructionMilitaryEscort * design.CalculateCurrentPurchasePrice(_Galaxy);
                num24 += (double)Policy.ConstructionMilitaryEscort * design.CalculateMaintenanceCosts(_Galaxy, this);
            }
            if (design2 != null)
            {
                num22 += Policy.ConstructionMilitaryFrigate;
                num23 += (double)Policy.ConstructionMilitaryFrigate * design2.CalculateCurrentPurchasePrice(_Galaxy);
                num24 += (double)Policy.ConstructionMilitaryFrigate * design2.CalculateMaintenanceCosts(_Galaxy, this);
            }
            if (design3 != null)
            {
                num22 += Policy.ConstructionMilitaryDestroyer;
                num23 += (double)Policy.ConstructionMilitaryDestroyer * design3.CalculateCurrentPurchasePrice(_Galaxy);
                num24 += (double)Policy.ConstructionMilitaryDestroyer * design3.CalculateMaintenanceCosts(_Galaxy, this);
            }
            if (design4 != null)
            {
                num22 += Policy.ConstructionMilitaryCruiser;
                num23 += (double)Policy.ConstructionMilitaryCruiser * design4.CalculateCurrentPurchasePrice(_Galaxy);
                num24 += (double)Policy.ConstructionMilitaryCruiser * design4.CalculateMaintenanceCosts(_Galaxy, this);
            }
            if (design5 != null)
            {
                num22 += Policy.ConstructionMilitaryCapitalShip;
                num23 += (double)Policy.ConstructionMilitaryCapitalShip * design5.CalculateCurrentPurchasePrice(_Galaxy);
                num24 += (double)Policy.ConstructionMilitaryCapitalShip * design5.CalculateMaintenanceCosts(_Galaxy, this);
            }
            if (design6 != null)
            {
                num22 += Policy.ConstructionMilitaryCarrier;
                num23 += (double)Policy.ConstructionMilitaryCarrier * design6.CalculateCurrentPurchasePrice(_Galaxy);
                num24 += (double)Policy.ConstructionMilitaryCarrier * design6.CalculateMaintenanceCosts(_Galaxy, this);
            }
            if (design7 != null)
            {
                num22 += Policy.ConstructionMilitaryTroopTransport;
                num23 += (double)Policy.ConstructionMilitaryTroopTransport * design7.CalculateCurrentPurchasePrice(_Galaxy);
                num24 += (double)Policy.ConstructionMilitaryTroopTransport * design7.CalculateMaintenanceCosts(_Galaxy, this);
            }
            ForceStructureProjectionList forceStructureProjectionList = new ForceStructureProjectionList();
            if (num7 > 0.0)
            {
                int num25 = Math.Min(10, 1 + BuiltObjects.Count / 5);
                int num26 = Math.Max(0, num25 - num19);
                if (num26 > 0 && design8 != null)
                {
                    double num27 = design8.CalculateMaintenanceCosts(_Galaxy, this);
                    double num28 = design8.CalculateCurrentPurchasePrice(_Galaxy);
                    int val = (int)(stateMoney / num28);
                    int val2 = (int)(num7 / num27);
                    int num29 = Math.Min(num26, Math.Min(val2, val));
                    if (num29 > 0)
                    {
                        double num30 = (double)num29 * num27;
                        forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.ExplorationShip, num29, starDate));
                        num7 -= num30;
                        stateMoney -= num28 * (double)num29;
                    }
                }
                if (habitatList.Count > 0)
                {
                    int num31 = Math.Min(5, 1 + BuiltObjects.Count / 20);
                    int num32 = Math.Max(0, num31 - num20);
                    if (num32 > 0 && design9 != null)
                    {
                        double num33 = design9.CalculateMaintenanceCosts(_Galaxy, this);
                        double num34 = design9.CalculateCurrentPurchasePrice(_Galaxy);
                        int val3 = (int)(stateMoney / num34);
                        int val4 = (int)(num7 / num33);
                        int num35 = Math.Min(num32, Math.Min(val4, val3));
                        if (num35 > 0)
                        {
                            double num36 = (double)num35 * num33;
                            forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.ConstructionShip, num35, starDate));
                            num7 -= num36;
                        }
                    }
                }
                if (num7 > 0.0)
                {
                    num23 /= (double)num22;
                    num24 /= (double)num22;
                    double val5 = num / num23;
                    double val6 = num7 / num24;
                    float num37 = (float)Math.Min(val5, val6);
                    int num38 = num21 + (int)num37;
                    if (design != null)
                    {
                        float num39 = Policy.ConstructionMilitaryEscort / num22;
                        int num40 = (int)(0.5f + (float)num38 * num39);
                        if (num12 < num40)
                        {
                            forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.Escort, (int)(0.5 + (double)(num39 * num37)), starDate));
                        }
                    }
                    if (design2 != null)
                    {
                        float num41 = Policy.ConstructionMilitaryFrigate / num22;
                        int num42 = (int)(0.5f + (float)num38 * num41);
                        if (num13 < num42)
                        {
                            forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.Frigate, (int)(0.5 + (double)(num41 * num37)), starDate));
                        }
                    }
                    if (design3 != null)
                    {
                        float num43 = Policy.ConstructionMilitaryDestroyer / num22;
                        int num44 = (int)(0.5f + (float)num38 * num43);
                        if (num14 < num44)
                        {
                            forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.Destroyer, (int)(0.5 + (double)(num43 * num37)), starDate));
                        }
                    }
                    if (design4 != null)
                    {
                        float num45 = Policy.ConstructionMilitaryCruiser / num22;
                        int num46 = (int)(0.5f + (float)num38 * num45);
                        if (num15 < num46)
                        {
                            forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.Cruiser, (int)(0.5 + (double)(num45 * num37)), starDate));
                        }
                    }
                    if (design5 != null)
                    {
                        float num47 = Policy.ConstructionMilitaryCapitalShip / num22;
                        int num48 = (int)(0.5f + (float)num38 * num47);
                        if (num16 < num48)
                        {
                            forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.CapitalShip, (int)(0.5 + (double)(num47 * num37)), starDate));
                        }
                    }
                    if (design6 != null)
                    {
                        float num49 = Policy.ConstructionMilitaryCarrier / num22;
                        int num50 = (int)(0.5f + (float)num38 * num49);
                        if (num17 < num50)
                        {
                            forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.Carrier, (int)(0.5 + (double)(num49 * num37)), starDate));
                        }
                    }
                    if (design7 != null)
                    {
                        float num51 = Policy.ConstructionMilitaryTroopTransport / num22;
                        int num52 = (int)(0.5f + (float)num38 * num51);
                        if (num18 < num52)
                        {
                            forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.TroopTransport, (int)(0.5 + (double)(num51 * num37)), starDate));
                        }
                    }
                }
            }
            _StateForceStructureProjections = forceStructureProjectionList;
            forceStructureProjectionList = new ForceStructureProjectionList();
            int freightLevel = 1 + (int)((double)BuiltObjects.Count * 1.0 * Policy.PirateSmugglerFreighterLevel);
            int passengerLevel = (int)((double)BuiltObjects.Count * 0.07 * Policy.PirateSmugglerPassengerLevel);
            int miningLevel = 1 + (int)((double)BuiltObjects.Count * 0.35 * Policy.PirateSmugglerMiningLevel);
            if (habitatList.Count <= 0 && ResortBases.Count <= 0)
            {
                passengerLevel = 0;
            }
            switch (PiratePlayStyle)
            {
                case PiratePlayStyle.Balanced:
                    freightLevel = (int)((double)freightLevel * 1.3);
                    break;
                case PiratePlayStyle.Mercenary:
                    freightLevel = (int)((double)freightLevel * 0.5);
                    miningLevel = (int)((double)miningLevel * 0.5);
                    break;
                case PiratePlayStyle.Smuggler:
                    freightLevel = (int)((double)freightLevel * 2.0);
                    miningLevel = (int)((double)miningLevel * 1.5);
                    break;
                case PiratePlayStyle.Legendary:
                    freightLevel = (int)((double)freightLevel * 2.0);
                    miningLevel = (int)((double)miningLevel * 1.5);
                    break;
            }
            int num56 = PrivateBuiltObjects.CountBySubRole(BuiltObjectSubRole.SmallFreighter);
            int num57 = PrivateBuiltObjects.CountBySubRole(BuiltObjectSubRole.MediumFreighter);
            int num58 = PrivateBuiltObjects.CountBySubRole(BuiltObjectSubRole.LargeFreighter);
            int num59 = PrivateBuiltObjects.CountBySubRole(BuiltObjectSubRole.PassengerShip);
            int num60 = PrivateBuiltObjects.CountBySubRole(BuiltObjectSubRole.MiningShip);
            int num61 = PrivateBuiltObjects.CountBySubRole(BuiltObjectSubRole.GasMiningShip);
            Design design10 = Designs.FindNewestCanBuild(BuiltObjectSubRole.SmallFreighter);
            Design design11 = Designs.FindNewestCanBuild(BuiltObjectSubRole.MediumFreighter);
            Design design12 = Designs.FindNewestCanBuild(BuiltObjectSubRole.LargeFreighter);
            Design design13 = Designs.FindNewestCanBuild(BuiltObjectSubRole.PassengerShip);
            Design design14 = Designs.FindNewestCanBuild(BuiltObjectSubRole.MiningShip);
            Design design15 = Designs.FindNewestCanBuild(BuiltObjectSubRole.GasMiningShip);
            int num62 = Math.Max(0, freightLevel - (num56 + num57 + num58));
            int num63 = Math.Max(0, miningLevel - (num60 + num61));
            int num64 = Math.Max(0, passengerLevel - num59);
            int num65 = (int)((double)num62 * 0.25);
            int num66 = (int)((double)num62 * 0.35);
            int num67 = num62 - (num66 + num65);
            int num68 = (int)((double)num63 * 0.5);
            int num69 = num63 - num68;
            int num70 = num64;
            int num71 = Math.Max(num67, Math.Max(num66, Math.Max(num65, Math.Max(num69, Math.Max(num68, num70)))));
            int num72 = 0;
            int num73 = 0;
            int num74 = 0;
            int num75 = 0;
            int num76 = 0;
            int num77 = 0;
            double num78 = 0.0;
            double num79 = 0.0;
            double num80 = 0.0;
            double num81 = 0.0;
            double num82 = 0.0;
            double num83 = 0.0;
            if (design10 != null)
            {
                num78 = design10.CalculateCurrentPurchasePrice(_Galaxy);
            }
            if (design11 != null)
            {
                num79 = design11.CalculateCurrentPurchasePrice(_Galaxy);
            }
            if (design12 != null)
            {
                num80 = design12.CalculateCurrentPurchasePrice(_Galaxy);
            }
            if (design14 != null)
            {
                num81 = design14.CalculateCurrentPurchasePrice(_Galaxy);
            }
            if (design15 != null)
            {
                num82 = design15.CalculateCurrentPurchasePrice(_Galaxy);
            }
            if (design13 != null)
            {
                num83 = design13.CalculateCurrentPurchasePrice(_Galaxy);
            }
            double num84 = 0.0;
            for (int i = 0; i < num71; i++)
            {
                if (num67 > 0 && num78 > 0.0)
                {
                    if (num2 > num84 + num78)
                    {
                        num72++;
                        num84 += num78;
                    }
                    num67--;
                }
                if (num66 > 0 && num79 > 0.0)
                {
                    if (num2 > num84 + num79)
                    {
                        num73++;
                        num84 += num79;
                    }
                    num66--;
                }
                if (num65 > 0 && num80 > 0.0)
                {
                    if (num2 > num84 + num80)
                    {
                        num74++;
                        num84 += num80;
                    }
                    num65--;
                }
                if (num69 > 0 && num81 > 0.0)
                {
                    if (num2 > num84 + num81)
                    {
                        num75++;
                        num84 += num81;
                    }
                    num69--;
                }
                if (num68 > 0 && num82 > 0.0)
                {
                    if (num2 > num84 + num82)
                    {
                        num76++;
                        num84 += num82;
                    }
                    num68--;
                }
                if (num70 > 0 && num83 > 0.0)
                {
                    if (num2 > num84 + num83)
                    {
                        num77++;
                        num84 += num83;
                    }
                    num70--;
                }
            }
            if (num72 > 0)
            {
                forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.SmallFreighter, num72, starDate));
            }
            if (num73 > 0)
            {
                forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.MediumFreighter, num73, starDate));
            }
            if (num74 > 0)
            {
                forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.LargeFreighter, num74, starDate));
            }
            if (num75 > 0)
            {
                forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.MiningShip, num75, starDate));
            }
            if (num76 > 0)
            {
                forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.GasMiningShip, num76, starDate));
            }
            if (num77 > 0)
            {
                forceStructureProjectionList.Add(new ForceStructureProjection(BuiltObjectSubRole.PassengerShip, num77, starDate));
            }
            _PrivateForceStructureProjections = forceStructureProjectionList;
        }

        private void MakeDefendOffersToPirates(long starDate)
        {
            bool flag = false;
            switch (Policy.OfferDefensivePirateMissionsSituation)
            {
                case 0:
                    flag = false;
                    break;
                case 1:
                    if (CheckAtWar())
                    {
                        flag = true;
                    }
                    break;
                case 2:
                    flag = true;
                    break;
            }
            if (!flag || Policy.OfferDefensivePirateMissions <= 0)
            {
                return;
            }
            PirateRelationList relationsByType = PirateRelations.GetRelationsByType(PirateRelationType.Protection);
            PirateRelationList relationsAboveThresholdAndByType = PirateRelations.GetRelationsAboveThresholdAndByType(15f, PirateRelationType.Protection);
            PirateRelationList relationsAboveThresholdAndByType2 = PirateRelations.GetRelationsAboveThresholdAndByType(30f, PirateRelationType.Protection);
            if (relationsByType.Count <= 0)
            {
                return;
            }
            StellarObjectList stellarObjectList = IdentifyMostAtRiskColoniesBases();
            double num = _PirateMissions.CalculateTotalDefendCosts(this);
            if (stellarObjectList.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < stellarObjectList.Count; i++)
            {
                StellarObject stellarObject = stellarObjectList[i];
                if (stellarObject == null || stellarObject.HasBeenDestroyed || stellarObject.Empire != this)
                {
                    continue;
                }
                double num2 = CalculatePirateDefendPrice(stellarObject);
                if (!(num + num2 < _StateMoney))
                {
                    continue;
                }
                Habitat systemStar = null;
                PirateRelationList pirateRelationList = new PirateRelationList();
                if (stellarObject is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)stellarObject;
                    systemStar = builtObject.NearestSystemStar;
                    switch (builtObject.SubRole)
                    {
                        case BuiltObjectSubRole.GasMiningStation:
                        case BuiltObjectSubRole.MiningStation:
                        case BuiltObjectSubRole.ResortBase:
                            pirateRelationList = relationsByType;
                            break;
                        case BuiltObjectSubRole.Outpost:
                        case BuiltObjectSubRole.SmallSpacePort:
                        case BuiltObjectSubRole.MediumSpacePort:
                        case BuiltObjectSubRole.LargeSpacePort:
                            pirateRelationList = ((builtObject.ParentHabitat == null || builtObject.ParentHabitat != Capital) ? relationsAboveThresholdAndByType : relationsAboveThresholdAndByType2);
                            break;
                        case BuiltObjectSubRole.EnergyResearchStation:
                        case BuiltObjectSubRole.WeaponsResearchStation:
                        case BuiltObjectSubRole.HighTechResearchStation:
                            pirateRelationList = relationsAboveThresholdAndByType;
                            break;
                    }
                }
                else if (stellarObject is Habitat)
                {
                    Habitat habitat = (Habitat)stellarObject;
                    systemStar = Galaxy.DetermineHabitatSystemStar(habitat);
                    pirateRelationList = ((habitat != Capital) ? relationsAboveThresholdAndByType : relationsAboveThresholdAndByType2);
                }
                if (pirateRelationList.Count <= 0)
                {
                    continue;
                }
                long expiryDate = starDate + (long)(1.0 * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
                EmpireActivity empireActivity = new EmpireActivity(this, stellarObject, num2, this, expiryDate, EmpireActivityType.Defend);
                if (_PirateMissions.ContainsEquivalent(empireActivity.Target, empireActivity.Type))
                {
                    continue;
                }
                int num3 = _PirateMissions.CountMissionsInSameSystem(systemStar, EmpireActivityType.Defend, this);
                if (num3 > 2)
                {
                    continue;
                }
                int refusalCount = 0;
                if (!CheckTaskAuthorized(_ControlOfferPirateMissions, ref refusalCount, GenerateAutomationMessageOfferPirateDefendMission(empireActivity), empireActivity, AdvisorMessageType.OfferPirateDefendMission, empireActivity, null))
                {
                    continue;
                }
                _PirateMissions.Add(empireActivity);
                _Galaxy.PirateMissions.Add(empireActivity);
                num += num2;
                for (int j = 0; j < pirateRelationList.Count; j++)
                {
                    PirateRelation pirateRelation = pirateRelationList[j];
                    if (pirateRelation != null && pirateRelation.OtherEmpire != null && pirateRelation.OtherEmpire.PirateEmpireBaseHabitat != null && pirateRelation.OtherEmpire.IsObjectAreaKnownToThisEmpire(empireActivity.Target))
                    {
                        string description = string.Format(TextResolver.GetText("Pirate Defend Mission Available"), empireActivity.RequestingEmpire.Name, empireActivity.Target.Name);
                        SendMessageToEmpire(pirateRelation.OtherEmpire, EmpireMessageType.PirateDefendMissionAvailable, empireActivity, description);
                    }
                }
            }
        }

        public bool DetermineOfferPirateDefendMissionToPirateFaction(Empire pirateFaction)
        {
            if (this == _Galaxy.IndependentEmpire)
            {
                return true;
            }
            if (pirateFaction != null && pirateFaction.PirateEmpireBaseHabitat != null)
            {
                switch (Policy.OfferDefensivePirateMissions)
                {
                    case 0:
                        return false;
                    case 1:
                        {
                            PirateRelation pirateRelation2 = ObtainPirateRelation(pirateFaction);
                            if (pirateRelation2.Type == PirateRelationType.Protection && pirateRelation2.Evaluation >= 15f)
                            {
                                return true;
                            }
                            break;
                        }
                    case 2:
                        {
                            PirateRelation pirateRelation = ObtainPirateRelation(pirateFaction);
                            if (pirateRelation.Type == PirateRelationType.Protection)
                            {
                                return true;
                            }
                            break;
                        }
                }
            }
            return false;
        }

        public StellarObjectList IdentifyMostAtRiskColoniesBases()
        {
            StellarObjectList stellarObjectList = new StellarObjectList();
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation != null && diplomaticRelation.Type != 0)
                {
                    DiplomaticRelation diplomaticRelation2 = diplomaticRelation.OtherEmpire.ObtainDiplomaticRelation(this);
                    if (diplomaticRelation2.Strategy == DiplomaticStrategy.Conquer)
                    {
                        stellarObjectList.AddRange(ListHelper.ToArrayThreadSafe(diplomaticRelation2.WarObjectiveBases));
                        stellarObjectList.AddRange(ListHelper.ToArrayThreadSafe(diplomaticRelation2.WarObjectiveColonies));
                    }
                }
            }
            if (stellarObjectList.Count <= 0 && !CheckEmpireHasHyperDriveTech(this) && Capital != null)
            {
                BuiltObject builtObject = _Galaxy.DetermineSpacePortAtColonyIncludingUnderConstruction(Capital);
                if (builtObject != null)
                {
                    stellarObjectList.Add(builtObject);
                }
            }
            return stellarObjectList;
        }

        public void ReviewPirateDefendMissions(long starDate)
        {
            EmpireActivityList empireActivityList = _PirateMissions.ResolveActivitiesByType(EmpireActivityType.Defend);
            EmpireActivityList empireActivityList2 = new EmpireActivityList();
            if (empireActivityList.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < empireActivityList.Count; i++)
            {
                EmpireActivity empireActivity = empireActivityList[i];
                if (empireActivity != null && empireActivity.AssignedEmpire != null && empireActivity.RequestingEmpire == this && empireActivity.Target != null && empireActivity.BidTimeRemaining == 0 && starDate >= empireActivity.ExpiryDate)
                {
                    bool flag = true;
                    if (empireActivity.Target.HasBeenDestroyed)
                    {
                        flag = false;
                    }
                    if (empireActivity.Target.Empire != empireActivity.RequestingEmpire)
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        empireActivity.AssignedEmpire.CompletePirateMission(empireActivity);
                    }
                    else
                    {
                        PirateRelation pirateRelation = empireActivity.RequestingEmpire.ObtainPirateRelation(this);
                        pirateRelation.EvaluationPirateMissionsFail -= 20f;
                        string description = string.Format(TextResolver.GetText("Pirate Defend Mission Failed Pirate"), empireActivity.RequestingEmpire.Name, empireActivity.Target.Name, empireActivity.Price.ToString("0"));
                        empireActivity.AssignedEmpire.SendMessageToEmpire(empireActivity.AssignedEmpire, EmpireMessageType.PirateDefendMissionFailed, empireActivity.Target, description);
                        description = string.Format(TextResolver.GetText("Pirate Defend Mission Failed Other"), empireActivity.AssignedEmpire.Name, empireActivity.Target.Name, empireActivity.Price.ToString("0"));
                        empireActivity.RequestingEmpire.SendMessageToEmpire(empireActivity.RequestingEmpire, EmpireMessageType.PirateDefendMissionFailed, empireActivity.Target, description);
                    }
                    empireActivityList2.Add(empireActivity);
                }
            }
            for (int j = 0; j < empireActivityList2.Count; j++)
            {
                empireActivityList2[j].AssignedEmpire.PirateMissions.RemoveEquivalent(empireActivityList2[j]);
                empireActivityList2[j].RequestingEmpire.PirateMissions.RemoveEquivalent(empireActivityList2[j]);
            }
        }

        public void ReviewPirateSmugglingMissions(long starDate)
        {
            if (ControlOfferPirateMissions != AutomationLevel.FullyAutomated)
            {
                return;
            }
            OrderList orders = _Galaxy.Orders.GetOrders(this);
            double num = PrivateAnnualRevenue - (AnnualPrivateMaintenanceExcludingUnderConstruction + AnnualTaxRevenue + ThisYearsPrivateFuelCosts);
            EmpireActivityList empireActivityList = _PirateMissions.ResolveActivitiesByType(EmpireActivityType.Smuggle);
            EmpireActivityList empireActivityList2 = new EmpireActivityList();
            if (empireActivityList.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < empireActivityList.Count; i++)
            {
                EmpireActivity empireActivity = empireActivityList[i];
                if (empireActivity == null || empireActivity.Target == null || !(empireActivity.Target is Habitat) || empireActivity.BidTimeRemaining > 0)
                {
                    continue;
                }
                bool flag = false;
                Habitat colony = (Habitat)empireActivity.Target;
                if (empireActivity.RequestingEmpire != _Galaxy.IndependentEmpire)
                {
                    bool flag2 = true;
                    if (empireActivity.ResourceId == byte.MaxValue)
                    {
                        byte deficientResourceId = byte.MaxValue;
                        flag2 = DetermineColonyDeficientInResources(colony, orders, checkForExistingSmugglingMission: false, 0L, 0, out deficientResourceId);
                    }
                    else
                    {
                        flag2 = DetermineColonyDeficientInResource(colony, empireActivity.ResourceId, 0L, 0, empireActivity.RelatedOrder);
                    }
                    if (!flag2)
                    {
                        flag = true;
                    }
                    else if (StateMoney < 0.0 || PrivateMoney < 0.0 || PrivateMoney < num * 2.0)
                    {
                        flag = true;
                    }
                    else if (empireActivity.ExpiryDate < starDate)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    empireActivityList2.Add(empireActivity);
                }
            }
            for (int j = 0; j < empireActivityList2.Count; j++)
            {
                _Galaxy.RemovePirateSmugglingMissionFromAllEmpires(empireActivityList2[j]);
                _PirateMissions.RemoveEquivalent(empireActivityList2[j]);
                if (_Galaxy.PirateMissions.ContainsEquivalent(empireActivityList2[j].Target, empireActivityList2[j].Type))
                {
                    _Galaxy.PirateMissions.RemoveEquivalent(empireActivityList2[j].Target, empireActivityList2[j].Type);
                }
                if (empireActivityList2[j].RelatedOrder != null)
                {
                    empireActivityList2[j].RelatedOrder.ExpiryDate = _Galaxy.CurrentStarDate;
                }
            }
        }

        public void MakeSmugglingOffersToPirates(long starDate)
        {
            bool flag = false;
            if (this == _Galaxy.IndependentEmpire)
            {
                flag = true;
            }
            else
            {
                switch (Policy.OfferSmugglingPirateMissions)
                {
                    case 1:
                        if (CheckAtWar())
                        {
                            flag = true;
                        }
                        break;
                    case 2:
                        flag = true;
                        break;
                }
            }
            if (!flag)
            {
                return;
            }
            double num = PrivateAnnualRevenue - (AnnualPrivateMaintenanceExcludingUnderConstruction + AnnualTaxRevenue + ThisYearsPrivateFuelCosts);
            int deficientResourceCount = 0;
            IdentifyResourceDeficientColony(out var deficientColony, out var deficientResourceId, out deficientResourceCount);
            if (deficientColony == null)
            {
                return;
            }
            double num2 = 1.0;
            if (deficientResourceCount == 1)
            {
                num2 = CalculatePirateSmugglePricePerUnit(deficientColony, deficientResourceId);
            }
            double num3 = num2 * 500.0;
            double num4 = _PrivateMoney;
            if (this == _Galaxy.IndependentEmpire)
            {
                num4 = double.MaxValue;
            }
            else if (GovernmentAttributes != null && GovernmentAttributes.SpecialFunctionCode == 1)
            {
                num4 = _StateMoney;
            }
            if (!(num4 > num3) || !(PrivateMoney > num * 2.0))
            {
                return;
            }
            long expiryDate = starDate + (long)(3.0 * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
            EmpireActivity empireActivity = new EmpireActivity(this, deficientColony, num2, this, expiryDate, EmpireActivityType.Smuggle);
            if (deficientResourceCount > 1)
            {
                empireActivity.ResourceId = byte.MaxValue;
            }
            else
            {
                empireActivity.ResourceId = deficientResourceId;
            }
            if (_PirateMissions.ContainsEquivalent(empireActivity.Target, empireActivity.Type))
            {
                return;
            }
            int refusalCount = 0;
            if (this != _Galaxy.IndependentEmpire && !CheckTaskAuthorized(_ControlOfferPirateMissions, ref refusalCount, GenerateAutomationMessageOfferPirateSmuggleMission(empireActivity), empireActivity, AdvisorMessageType.OfferPirateSmuggleMission, empireActivity, null))
            {
                return;
            }
            if (empireActivity.ResourceId != byte.MaxValue)
            {
                Order order = (empireActivity.RelatedOrder = CreateOrder(deficientColony, new Resource(empireActivity.ResourceId), 10000, isState: true, OrderType.Standard, expiryDate));
            }
            _PirateMissions.Add(empireActivity);
            _Galaxy.PirateMissions.Add(empireActivity);
            if (this == _Galaxy.IndependentEmpire)
            {
                for (int i = 0; i < _Galaxy.PirateEmpires.Count; i++)
                {
                    Empire empire = _Galaxy.PirateEmpires[i];
                    if (empire != null && empire.PirateEmpireBaseHabitat != null && empire.IsObjectAreaKnownToThisEmpire(empireActivity.Target))
                    {
                        string empty = string.Empty;
                        empty = ((empireActivity.ResourceId != byte.MaxValue) ? string.Format(TextResolver.GetText("Pirate Smuggle Mission Available Independent"), empireActivity.Target.Name, new Resource(empireActivity.ResourceId).Name) : string.Format(TextResolver.GetText("Pirate Smuggle Mission Available Independent All Resources"), empireActivity.Target.Name));
                        SendMessageToEmpire(empire, EmpireMessageType.PirateSmugglingMissionAvailable, empireActivity, empty);
                    }
                }
                return;
            }
            for (int j = 0; j < PirateRelations.Count; j++)
            {
                PirateRelation pirateRelation = PirateRelations[j];
                if (pirateRelation != null && pirateRelation.OtherEmpire != null && pirateRelation.OtherEmpire.PirateEmpireBaseHabitat != null && pirateRelation.Type == PirateRelationType.Protection && pirateRelation.OtherEmpire.IsObjectAreaKnownToThisEmpire(empireActivity.Target))
                {
                    string description = string.Format(TextResolver.GetText("Pirate Smuggle Mission Available"), empireActivity.RequestingEmpire.Name, empireActivity.Target.Name);
                    SendMessageToEmpire(pirateRelation.OtherEmpire, EmpireMessageType.PirateSmugglingMissionAvailable, empireActivity, description);
                }
            }
        }

        private void IdentifyResourceDeficientColony(out Habitat deficientColony, out byte deficientResourceId, out int deficientResourceCount)
        {
            deficientColony = null;
            deficientResourceId = byte.MaxValue;
            deficientResourceCount = 0;
            long num = (long)(0.5 * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
            OrderList orders = _Galaxy.Orders.GetOrders(this);
            Habitat habitat = null;
            byte b = 0;
            double num2 = 0.0;
            HabitatList habitatList = new HabitatList();
            habitatList = ((this != _Galaxy.IndependentEmpire) ? Colonies : _Galaxy.IndependentColonies);
            for (int i = 0; i < habitatList.Count; i++)
            {
                Habitat habitat2 = habitatList[i];
                if (habitat2 == null || habitat2.HasBeenDestroyed || habitat2.Empire != this || _PirateMissions.ContainsEquivalent(habitat2, EmpireActivityType.Smuggle))
                {
                    continue;
                }
                BuiltObject builtObject = null;
                if (habitat2.HasSpacePort)
                {
                    builtObject = _Galaxy.DetermineSpacePortAtHabitat(habitat2);
                }
                ResourceList resourceList = habitat2.DetermineCriticalResources();
                OrderList orders2 = orders.GetOrders(habitat2);
                if (builtObject != null && builtObject.IsSpacePort)
                {
                    OrderList orders3 = orders.GetOrders(builtObject);
                    if (orders3.Count > 0)
                    {
                        orders2.AddRange(orders3);
                    }
                }
                for (int j = 0; j < orders2.Count; j++)
                {
                    Order order = orders2[j];
                    if (order == null || order.CommodityResource == null)
                    {
                        continue;
                    }
                    int amountOutstandingToContract = order.AmountOutstandingToContract;
                    if (amountOutstandingToContract <= 100)
                    {
                        continue;
                    }
                    long timeSinceOrderPlacement = 0L;
                    _Galaxy.CalculateOrderPlacementDate(order, out timeSinceOrderPlacement);
                    if (timeSinceOrderPlacement <= num)
                    {
                        continue;
                    }
                    int num3 = 0;
                    Cargo cargo = habitat2.Cargo.GetCargo(order.CommodityResource, EmpireId);
                    if (cargo != null)
                    {
                        num3 = cargo.Available;
                    }
                    bool isCriticalResource = resourceList.Contains(order.CommodityResource);
                    int num4 = Galaxy.CalculateResourceLevel(order.CommodityResource, habitat2, isMiningStation: false, isIndependent: false, isCriticalResource, 0);
                    int num5 = (int)((double)num4 * 0.6);
                    if (num3 < num5)
                    {
                        deficientResourceCount++;
                        double num6 = (double)timeSinceOrderPlacement / 1000.0 * (double)amountOutstandingToContract;
                        if (num6 > num2)
                        {
                            habitat = habitat2;
                            b = order.CommodityResource.ResourceID;
                            num2 = num6;
                        }
                    }
                }
            }
            if (num2 > 0.0 && habitat != null)
            {
                deficientColony = habitat;
                deficientResourceId = b;
            }
        }

        public bool DetermineColonyDeficientInResources(Habitat colony, OrderList empireOrders, bool checkForExistingSmugglingMission, long maximumOrderTimeLength, int maximumAmountOutstanding, out byte deficientResourceId)
        {
            deficientResourceId = byte.MaxValue;
            if (colony != null && !colony.HasBeenDestroyed && colony.Empire == this && (!checkForExistingSmugglingMission || !_PirateMissions.ContainsEquivalent(colony, EmpireActivityType.Smuggle)))
            {
                BuiltObject builtObject = null;
                if (colony.HasSpacePort)
                {
                    builtObject = _Galaxy.DetermineSpacePortAtHabitat(colony);
                }
                OrderList orders = empireOrders.GetOrders(colony);
                if (builtObject != null && builtObject.IsSpacePort)
                {
                    OrderList orders2 = empireOrders.GetOrders(builtObject);
                    if (orders2.Count > 0)
                    {
                        orders.AddRange(orders2);
                    }
                }
                int num = Galaxy.Rnd.Next(0, orders.Count);
                for (int i = num; i < orders.Count; i++)
                {
                    Order order = orders[i];
                    if (order == null || order.CommodityResource == null || colony.Resources.ContainsId(order.CommodityResource.ResourceID))
                    {
                        continue;
                    }
                    int amountOutstandingToContract = order.AmountOutstandingToContract;
                    if (amountOutstandingToContract > maximumAmountOutstanding)
                    {
                        long timeSinceOrderPlacement = 0L;
                        _Galaxy.CalculateOrderPlacementDate(order, out timeSinceOrderPlacement);
                        if (timeSinceOrderPlacement > maximumOrderTimeLength)
                        {
                            deficientResourceId = order.CommodityResource.ResourceID;
                            return true;
                        }
                    }
                }
                for (int j = 0; j < num; j++)
                {
                    Order order2 = orders[j];
                    if (order2 == null || order2.CommodityResource == null || colony.Resources.ContainsId(order2.CommodityResource.ResourceID))
                    {
                        continue;
                    }
                    int amountOutstandingToContract2 = order2.AmountOutstandingToContract;
                    if (amountOutstandingToContract2 > maximumAmountOutstanding)
                    {
                        long timeSinceOrderPlacement2 = 0L;
                        _Galaxy.CalculateOrderPlacementDate(order2, out timeSinceOrderPlacement2);
                        if (timeSinceOrderPlacement2 > maximumOrderTimeLength)
                        {
                            deficientResourceId = order2.CommodityResource.ResourceID;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool DetermineColonyDeficientInResource(Habitat colony, byte resourceId, long maximumOrderTimeLength, int maximumAmountOutstanding, Order relatedOrder)
        {
            return DetermineColonyDeficientInResource(colony, resourceId, maximumOrderTimeLength, maximumAmountOutstanding, relatedOrder, _Galaxy.Orders);
        }

        private bool DetermineColonyDeficientInResource(Habitat colony, byte resourceId, long maximumOrderTimeLength, int maximumAmountOutstanding, Order relatedOrder, OrderList orders)
        {
            BuiltObject builtObject = null;
            if (colony.HasSpacePort)
            {
                builtObject = _Galaxy.DetermineSpacePortAtHabitat(colony);
            }
            OrderList orders2 = orders.GetOrders(colony);
            if (builtObject != null && builtObject.IsSpacePort)
            {
                OrderList orders3 = orders.GetOrders(builtObject);
                if (orders3.Count > 0)
                {
                    orders2.AddRange(orders3);
                }
            }
            int num = 0;
            int num2 = 0;
            bool flag = false;
            for (int i = 0; i < orders2.Count; i++)
            {
                Order order = orders2[i];
                if (order != null && order.CommodityResource != null && order.CommodityResource.ResourceID == resourceId && order != relatedOrder)
                {
                    int amountOutstandingToContract = order.AmountOutstandingToContract;
                    num += amountOutstandingToContract;
                    int amountStillToArrive = order.AmountStillToArrive;
                    num2 += amountStillToArrive;
                    long timeSinceOrderPlacement = 0L;
                    _Galaxy.CalculateOrderPlacementDate(order, out timeSinceOrderPlacement);
                    if (timeSinceOrderPlacement > maximumOrderTimeLength)
                    {
                        flag = true;
                    }
                }
            }
            if (flag && (num >= maximumAmountOutstanding || num2 >= maximumAmountOutstanding))
            {
                return true;
            }
            return false;
        }

        private void MakeAttackOffersToPirates(long starDate)
        {
            StellarObjectList stellarObjectList = new StellarObjectList();
            PirateRelationList relationsAboveThreshold = PirateRelations.GetRelationsAboveThreshold(-50f);
            int num = 100;
            if (ShipGroups != null)
            {
                ShipGroup shipGroup = ShipGroups.IdentifyLargestFleet();
                if (shipGroup != null)
                {
                    num = shipGroup.TotalOverallStrengthFactor;
                }
            }
            EmpireList empireList = _PirateMissions.ResolveAttackTargettedEmpires();
            Empire empire = _Galaxy.IdentifyMechanoidEmpire();
            if (empire != null && !empireList.Contains(empire))
            {
                empireList.Add(empire);
            }
            EmpireEvaluation lowestEvaluationKnownEmpire = EmpireEvaluations.GetLowestEvaluationKnownEmpire(this, empireList);
            bool flag = false;
            if (lowestEvaluationKnownEmpire != null)
            {
                switch (Policy.OfferPirateAttackMissions)
                {
                    case 0:
                        flag = false;
                        break;
                    case 1:
                        if (PirateEmpireBaseHabitat == null && lowestEvaluationKnownEmpire.Empire.PirateEmpireBaseHabitat == null)
                        {
                            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(lowestEvaluationKnownEmpire.Empire);
                            if (diplomaticRelation.Type == DiplomaticRelationType.War)
                            {
                                flag = true;
                            }
                        }
                        break;
                    case 2:
                        if (lowestEvaluationKnownEmpire.OverallAttitude <= -5)
                        {
                            flag = true;
                        }
                        break;
                    case 3:
                        flag = true;
                        break;
                }
                if (flag)
                {
                    DiplomaticRelation relation = ObtainDiplomaticRelation(lowestEvaluationKnownEmpire.Empire);
                    StellarObject stellarObject = IdentifyPirateAttackTargetForRelation(relation, num);
                    if (stellarObject != null)
                    {
                        stellarObjectList.Add(stellarObject);
                    }
                }
            }
            PirateRelation relationWithLowestEvaluation = PirateRelations.GetRelationWithLowestEvaluation();
            if (relationWithLowestEvaluation != null)
            {
                flag = false;
                switch (Policy.OfferPirateAttackMissions)
                {
                    case 0:
                        flag = false;
                        break;
                    case 1:
                        flag = false;
                        break;
                    case 2:
                        if (relationWithLowestEvaluation.Evaluation <= -5f)
                        {
                            flag = true;
                        }
                        break;
                    case 3:
                        flag = true;
                        break;
                }
                if (flag)
                {
                    StellarObject stellarObject2 = IdentifyPirateAttackTargetForRelation(relationWithLowestEvaluation, num);
                    if (stellarObject2 != null)
                    {
                        stellarObjectList.Add(stellarObject2);
                    }
                }
            }
            double num2 = _PirateMissions.CalculateTotalAttackCosts(this);
            for (int i = 0; i < stellarObjectList.Count; i++)
            {
                StellarObject stellarObject3 = stellarObjectList[i];
                if (stellarObject3 == null || stellarObject3.HasBeenDestroyed || stellarObject3.Empire == this)
                {
                    continue;
                }
                double num3 = CalculatePirateAttackPrice(stellarObject3);
                if (!(num2 + num3 < _StateMoney))
                {
                    continue;
                }
                long expiryDate = starDate + (long)(1.0 * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
                EmpireActivity empireActivity = new EmpireActivity(stellarObject3.Empire, stellarObject3, num3, this, expiryDate, EmpireActivityType.Attack);
                if (_PirateMissions.ContainsEquivalent(empireActivity.Target, empireActivity.Type))
                {
                    continue;
                }
                int refusalCount = 0;
                if (!CheckTaskAuthorized(_ControlOfferPirateMissions, ref refusalCount, GenerateAutomationMessageOfferPirateAttackMission(empireActivity), empireActivity, AdvisorMessageType.OfferPirateAttackMission, empireActivity, null))
                {
                    continue;
                }
                _PirateMissions.Add(empireActivity);
                _Galaxy.PirateMissions.Add(empireActivity);
                num2 += num3;
                for (int j = 0; j < relationsAboveThreshold.Count; j++)
                {
                    PirateRelation pirateRelation = relationsAboveThreshold[j];
                    if (pirateRelation != null && pirateRelation.OtherEmpire != null && pirateRelation.OtherEmpire.PirateEmpireBaseHabitat != null && pirateRelation.OtherEmpire.IsObjectAreaKnownToThisEmpire(empireActivity.Target))
                    {
                        string description = string.Format(TextResolver.GetText("Pirate Attack Mission Available"), empireActivity.RequestingEmpire.Name, empireActivity.Target.Name);
                        SendMessageToEmpire(pirateRelation.OtherEmpire, EmpireMessageType.PirateAttackMissionAvailable, empireActivity, description);
                    }
                }
            }
        }

        private void PiratesMakeAttackOffers(long starDate)
        {
            StellarObjectList stellarObjectList = new StellarObjectList();
            PirateRelationList relationsAboveThreshold = PirateRelations.GetRelationsAboveThreshold(-50f);
            PirateRelation relationWithLowestEvaluation = PirateRelations.GetRelationWithLowestEvaluation();
            bool flag = false;
            if (relationWithLowestEvaluation != null)
            {
                switch (Policy.OfferPirateAttackMissions)
                {
                    case 0:
                        flag = false;
                        break;
                    case 1:
                        if (relationWithLowestEvaluation.Type == PirateRelationType.None && relationWithLowestEvaluation.Evaluation < 0f)
                        {
                            flag = true;
                        }
                        break;
                    case 2:
                        if (relationWithLowestEvaluation.Evaluation <= -5f)
                        {
                            flag = true;
                        }
                        break;
                    case 3:
                        flag = true;
                        break;
                }
                if (flag)
                {
                    int maximumFirepower = 100;
                    if (ShipGroups != null)
                    {
                        ShipGroup shipGroup = ShipGroups.IdentifyLargestFleet();
                        if (shipGroup != null)
                        {
                            maximumFirepower = shipGroup.TotalOverallStrengthFactor;
                        }
                    }
                    StellarObject stellarObject = IdentifyPirateAttackTargetForRelation(relationWithLowestEvaluation, maximumFirepower);
                    if (stellarObject != null)
                    {
                        stellarObjectList.Add(stellarObject);
                    }
                }
            }
            double num = _PirateMissions.CalculateTotalAttackCosts(this);
            for (int i = 0; i < stellarObjectList.Count; i++)
            {
                StellarObject stellarObject2 = stellarObjectList[i];
                if (stellarObject2 == null || stellarObject2.HasBeenDestroyed || stellarObject2.Empire == this)
                {
                    continue;
                }
                Empire empire = stellarObject2.Empire;
                if (stellarObject2 is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)stellarObject2;
                    empire = builtObject.ActualEmpire;
                }
                if (empire == this)
                {
                    continue;
                }
                double num2 = CalculatePirateAttackPrice(stellarObject2);
                if (!(num + num2 < _StateMoney))
                {
                    continue;
                }
                long expiryDate = starDate + (long)(1.0 * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
                EmpireActivity empireActivity = new EmpireActivity(empire, stellarObject2, num2, this, expiryDate, EmpireActivityType.Attack);
                if (_PirateMissions.ContainsEquivalent(empireActivity.Target, empireActivity.Type))
                {
                    continue;
                }
                int refusalCount = 0;
                if (!CheckTaskAuthorized(_ControlOfferPirateMissions, ref refusalCount, GenerateAutomationMessageOfferPirateAttackMission(empireActivity), empireActivity, AdvisorMessageType.OfferPirateAttackMission, empireActivity, null))
                {
                    continue;
                }
                _PirateMissions.Add(empireActivity);
                _Galaxy.PirateMissions.Add(empireActivity);
                num += num2;
                for (int j = 0; j < relationsAboveThreshold.Count; j++)
                {
                    PirateRelation pirateRelation = relationsAboveThreshold[j];
                    if (pirateRelation != null && pirateRelation.OtherEmpire != null && pirateRelation.OtherEmpire.PirateEmpireBaseHabitat != null && pirateRelation.OtherEmpire.IsObjectAreaKnownToThisEmpire(empireActivity.Target))
                    {
                        string description = string.Format(TextResolver.GetText("Pirate Attack Mission Available"), empireActivity.RequestingEmpire.Name, empireActivity.Target.Name);
                        SendMessageToEmpire(pirateRelation.OtherEmpire, EmpireMessageType.PirateAttackMissionAvailable, empireActivity, description);
                    }
                }
            }
        }

        private void PirateCheckMissionsOnOffer(long starDate)
        {
            if (!Policy.BidOnPirateAttackMissions && !Policy.BidOnPirateDefendMissions && !Policy.AcceptPirateSmugglingMissions)
            {
                return;
            }
            int shipCount = 0;
            int pirateEmpireStrength = BuiltObjects.TotalMobileMilitaryFirepowerNotAttackingDefending(out shipCount);
            int num = PirateMissions.CountByType(EmpireActivityType.Attack);
            int num2 = PirateMissions.CountByType(EmpireActivityType.Defend);
            int num3 = 0;
            if (ShipGroups != null)
            {
                num3 = ShipGroups.Count;
            }
            for (int i = 0; i < _Galaxy.PirateMissions.Count; i++)
            {
                EmpireActivity empireActivity = _Galaxy.PirateMissions[i];
                if (empireActivity == null || empireActivity.RequestingEmpire == this || PirateMissions.ContainsEquivalent(empireActivity.Target, empireActivity.Type))
                {
                    continue;
                }
                switch (empireActivity.Type)
                {
                    case EmpireActivityType.Attack:
                        {
                            if (!Policy.BidOnPirateAttackMissions || num + num2 >= num3 || empireActivity.AssignedEmpire == this || !PirateCheckAcceptAttackMission(empireActivity, pirateEmpireStrength))
                            {
                                break;
                            }
                            double num8 = empireActivity.Price * 0.9;
                            double num9 = CalculatePirateMissionPriceWillingToBidFor(empireActivity);
                            if (!(num9 <= num8))
                            {
                                break;
                            }
                            empireActivity.AssignedEmpire = this;
                            if (empireActivity.BidTimeRemaining < 0)
                            {
                                empireActivity.BidTimeRemaining = 60000L;
                            }
                            else
                            {
                                empireActivity.Price = num8;
                                if (empireActivity.BidTimeRemaining < 10000)
                                {
                                    empireActivity.BidTimeRemaining += 10000L;
                                }
                            }
                            num++;
                            break;
                        }
                    case EmpireActivityType.Defend:
                        {
                            if (!Policy.BidOnPirateDefendMissions || num + num2 >= num3 || empireActivity.AssignedEmpire == this || !PirateCheckAcceptDefendMission(empireActivity, pirateEmpireStrength))
                            {
                                break;
                            }
                            double num6 = empireActivity.Price * 0.9;
                            double num7 = CalculatePirateMissionPriceWillingToBidFor(empireActivity);
                            if (!(num7 <= num6))
                            {
                                break;
                            }
                            empireActivity.AssignedEmpire = this;
                            if (empireActivity.BidTimeRemaining < 0)
                            {
                                empireActivity.BidTimeRemaining = 60000L;
                            }
                            else
                            {
                                empireActivity.Price = num6;
                                if (empireActivity.BidTimeRemaining < 10000)
                                {
                                    empireActivity.BidTimeRemaining += 10000L;
                                }
                            }
                            num2++;
                            break;
                        }
                    case EmpireActivityType.Smuggle:
                        {
                            if (!Policy.AcceptPirateSmugglingMissions || !IsObjectAreaKnownToThisEmpire(empireActivity.Target))
                            {
                                break;
                            }
                            int num4 = CountIdleFreighters();
                            int num5 = 1;
                            if (empireActivity.ResourceId != byte.MaxValue)
                            {
                                num5 = CountResourceSupplyLocations(empireActivity.ResourceId, includeIndependentColonies: true);
                            }
                            if (num4 > 0 && num5 > 0)
                            {
                                int refusalCount = 0;
                                if (CheckTaskAuthorized(_ControlOfferPirateMissions, ref refusalCount, GenerateAutomationMessageAcceptPirateSmuggleMission(empireActivity), empireActivity, AdvisorMessageType.AcceptPirateSmugglingMission, empireActivity, null))
                                {
                                    _PirateMissions.Add(empireActivity);
                                }
                            }
                            break;
                        }
                }
            }
        }

        public bool PirateCheckAcceptDefendMission(EmpireActivity defendMission, int pirateEmpireStrength)
        {
            if (PirateEmpireBaseHabitat != null && defendMission != null && defendMission.RequestingEmpire != null && defendMission.Target != null && ShipGroups.Count > 0 && defendMission.RequestingEmpire.DetermineOfferPirateDefendMissionToPirateFaction(this))
            {
                double num = Math.Max((double)Galaxy.SectorSize * 2.0, (double)Galaxy.SizeX * 0.2);
                if (PiratePlayStyle == PiratePlayStyle.Mercenary || PiratePlayStyle == PiratePlayStyle.Legendary)
                {
                    num *= 1.5;
                }
                if (defendMission.Target.Empire != this)
                {
                    PirateRelation pirateRelation = null;
                    if (defendMission.RequestingEmpire != _Galaxy.IndependentEmpire && defendMission.RequestingEmpire != null)
                    {
                        pirateRelation = ObtainPirateRelation(defendMission.RequestingEmpire);
                    }
                    if (defendMission.RequestingEmpire == _Galaxy.IndependentEmpire || (pirateRelation != null && pirateRelation.Type != 0 && pirateRelation.Evaluation >= -30f))
                    {
                        PirateRelation pirateRelation2 = null;
                        if (defendMission.TargetEmpire != _Galaxy.IndependentEmpire && defendMission.TargetEmpire != null)
                        {
                            pirateRelation2 = ObtainPirateRelation(defendMission.TargetEmpire);
                        }
                        if ((defendMission.TargetEmpire == _Galaxy.IndependentEmpire || (pirateRelation2 != null && pirateRelation2.Type == PirateRelationType.Protection)) && defendMission.ResolveTargetCoordinates(out var x, out var y))
                        {
                            double num2 = _Galaxy.CalculateDistance(PirateEmpireBaseHabitat.Xpos, PirateEmpireBaseHabitat.Ypos, x, y);
                            if (num2 < num)
                            {
                                int num3 = 0;
                                if (defendMission.Target != null)
                                {
                                    if (defendMission.Target is BuiltObject)
                                    {
                                        BuiltObject builtObject = (BuiltObject)defendMission.Target;
                                        num3 = builtObject.Size / 20;
                                    }
                                    else if (defendMission.Target is Habitat)
                                    {
                                        Habitat habitat = (Habitat)defendMission.Target;
                                        num3 = habitat.EstimatedDefensiveForceRequired(atWar: false);
                                    }
                                }
                                if (pirateEmpireStrength > num3)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool PirateCheckAcceptAttackMission(EmpireActivity attackMission, int pirateEmpireStrength)
        {
            if (PirateEmpireBaseHabitat != null && attackMission != null && attackMission.Target != null && ShipGroups.Count > 0)
            {
                double num = Math.Max((double)Galaxy.SectorSize * 3.0, (double)Galaxy.SizeX * 0.3);
                if (PiratePlayStyle == PiratePlayStyle.Mercenary || PiratePlayStyle == PiratePlayStyle.Legendary)
                {
                    num *= 1.5;
                }
                Empire empire = attackMission.Target.Empire;
                if (attackMission.Target is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)attackMission.Target;
                    empire = builtObject.ActualEmpire;
                }
                if (empire != this && attackMission.RequestingEmpire != null)
                {
                    PirateRelation pirateRelation = ObtainPirateRelation(attackMission.RequestingEmpire);
                    if (pirateRelation.Type != 0 && pirateRelation.Evaluation >= -50f && empire != null)
                    {
                        PirateRelation pirateRelation2 = ObtainPirateRelation(empire);
                        if (pirateRelation2.Type != PirateRelationType.Protection && attackMission.ResolveTargetCoordinates(out var x, out var y))
                        {
                            double num2 = _Galaxy.CalculateDistance(PirateEmpireBaseHabitat.Xpos, PirateEmpireBaseHabitat.Ypos, x, y);
                            if (num2 < num)
                            {
                                int num3 = 0;
                                if (attackMission.Target != null && attackMission.Target is BuiltObject)
                                {
                                    BuiltObject builtObject2 = (BuiltObject)attackMission.Target;
                                    num3 = builtObject2.FirepowerRaw + builtObject2.CurrentEscortForceAssigned;
                                }
                                if (pirateEmpireStrength > num3)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private StellarObject IdentifyPirateAttackTargetForRelation(PirateRelation relation, int maximumFirepower)
        {
            if (relation != null && relation.Type != 0)
            {
                if (KnownPirateBases != null)
                {
                    for (int i = 0; i < KnownPirateBases.Count; i++)
                    {
                        BuiltObject builtObject = KnownPirateBases[i];
                        if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.CalculateOverallStrengthFactor() <= maximumFirepower && builtObject.Empire == relation.OtherEmpire)
                        {
                            return builtObject;
                        }
                    }
                }
                if (PirateEmpireBaseHabitat != null)
                {
                    return _Galaxy.FindNearestKnownBase(this, PirateEmpireBaseHabitat.Xpos, PirateEmpireBaseHabitat.Ypos, relation.OtherEmpire, maximumFirepower);
                }
                if (Capital != null)
                {
                    return _Galaxy.FindNearestKnownBase(this, Capital.Xpos, Capital.Ypos, relation.OtherEmpire, maximumFirepower);
                }
            }
            return null;
        }

        private StellarObject IdentifyPirateAttackTargetForRelation(DiplomaticRelation relation, int maximumOverallStrength)
        {
            if (relation != null && relation.OtherEmpire != this && relation.Type != 0)
            {
                BuiltObject builtObject = null;
                switch (relation.Strategy)
                {
                    case DiplomaticStrategy.Conquer:
                        if (relation.WarObjectiveBases != null)
                        {
                            for (int i = 0; i < relation.WarObjectiveBases.Count; i++)
                            {
                                BuiltObject builtObject2 = relation.WarObjectiveBases[i];
                                if (builtObject2 != null && !builtObject2.HasBeenDestroyed && builtObject2.Empire == relation.OtherEmpire && builtObject2.CalculateOverallStrengthFactor() <= maximumOverallStrength && (builtObject2.ParentHabitat == null || builtObject2.ParentHabitat.Empire != relation.OtherEmpire))
                                {
                                    return builtObject2;
                                }
                            }
                        }
                        builtObject = _Galaxy.FindNearestKnownBase(this, Capital.Xpos, Capital.Ypos, relation.OtherEmpire, maximumOverallStrength);
                        if (builtObject != null)
                        {
                            return builtObject;
                        }
                        break;
                    case DiplomaticStrategy.Undermine:
                    case DiplomaticStrategy.DefendUndermine:
                    case DiplomaticStrategy.Punish:
                        builtObject = _Galaxy.FindNearestKnownBase(this, Capital.Xpos, Capital.Ypos, relation.OtherEmpire, maximumOverallStrength);
                        if (builtObject != null)
                        {
                            return builtObject;
                        }
                        break;
                    default:
                        builtObject = _Galaxy.FindNearestKnownBase(this, Capital.Xpos, Capital.Ypos, relation.OtherEmpire, maximumOverallStrength);
                        if (builtObject != null)
                        {
                            return builtObject;
                        }
                        break;
                }
            }
            return null;
        }

        public double CalculatePirateMissionPriceWillingToBidFor(EmpireActivity mission)
        {
            double result = 0.0;
            if (mission != null && mission.Target != null)
            {
                double num = 0.0;
                Random random = new Random((int)mission.Target.Xpos + EmpireId);
                switch (mission.Type)
                {
                    case EmpireActivityType.Attack:
                        num = CalculatePirateAttackPrice(mission.Target);
                        break;
                    case EmpireActivityType.Defend:
                        num = CalculatePirateDefendPrice(mission.Target);
                        break;
                }
                result = num * 0.4 + random.NextDouble() * num * 0.35;
            }
            return result;
        }

        public double CalculatePirateAttackPrice(StellarObject target)
        {
            double result = double.MaxValue;
            if (target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)target;
                if (IsObjectVisibleToThisEmpire(builtObject))
                {
                    int num = builtObject.FirepowerRaw + builtObject.CurrentEscortForceAssigned;
                    result = 100.0 * Math.Max(num, (double)builtObject.Size * 0.1);
                }
                else
                {
                    result = 100.0 * Math.Max(builtObject.FirepowerRaw, (double)builtObject.Size * 0.1);
                }
            }
            return result;
        }

        public double CalculatePirateDefendPrice(StellarObject target)
        {
            double result = 0.0;
            if (target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)target;
                int firepowerRaw = builtObject.FirepowerRaw;
                int num = builtObject.Size / 10;
                int num2 = Math.Max(5, num - firepowerRaw);
                if (num2 > 0)
                {
                    result = Math.Max(1000.0, Math.Min(8000.0, (double)num2 * 200.0));
                }
            }
            else if (target is Habitat)
            {
                Habitat habitat = (Habitat)target;
                int num3 = habitat.EstimatedDefensiveForceRequired(CheckAtWar());
                int num4 = num3;
                if (num4 > 0)
                {
                    result = Math.Max(5000.0, Math.Min(30000.0, 200.0 * (double)num4));
                }
            }
            return result;
        }

        public double CalculatePirateSmugglePricePerUnit(Habitat colony, byte resourceId)
        {
            //double num = 0.0;
            double num2 = _Galaxy.ResourceCurrentPrices[resourceId];
            return Math.Min(5.0, Math.Max(0.1, num2 * 0.5));
        }

        public float CalculatePirateDesireControlColoniesCommon(Empire otherEmpire, int colonyTargetListDepth)
        {
            float num = 0f;
            if (PirateEmpireBaseHabitat != null && ColonizationTargets != null && Colonies != null)
            {
                HabitatList habitatList = new HabitatList();
                habitatList.AddRange(Colonies);
                habitatList.AddRange(ColonizationTargets.ResolveHabitats());
                if (otherEmpire != null)
                {
                    if (otherEmpire.PirateEmpireBaseHabitat != null)
                    {
                        if (otherEmpire.ColonizationTargets != null && otherEmpire.Colonies != null)
                        {
                            HabitatList habitatList2 = new HabitatList();
                            habitatList2.AddRange(otherEmpire.Colonies);
                            habitatList2.AddRange(otherEmpire.ColonizationTargets.ResolveHabitats());
                            for (int i = 0; i < colonyTargetListDepth; i++)
                            {
                                if (habitatList.Count <= i)
                                {
                                    continue;
                                }
                                Habitat habitat = habitatList[i];
                                if (habitat != null)
                                {
                                    int num2 = habitatList2.IndexOf(habitat);
                                    if (num2 >= 0 && num2 < colonyTargetListDepth)
                                    {
                                        num -= 10f;
                                    }
                                }
                            }
                        }
                    }
                    else if (otherEmpire.ColonizationTargets != null)
                    {
                        for (int j = 0; j < colonyTargetListDepth; j++)
                        {
                            if (habitatList.Count > j)
                            {
                                Habitat habitat2 = habitatList[j];
                                if (habitat2 != null && otherEmpire.Colonies.Contains(habitat2))
                                {
                                    num -= 10f;
                                }
                            }
                        }
                    }
                }
            }
            return num;
        }

        public bool CheckPirateDesireControlColoniesCommon(Empire otherEmpire, int depth)
        {
            if (PirateEmpireBaseHabitat != null && ColonizationTargets != null && Colonies != null)
            {
                HabitatList habitatList = new HabitatList();
                habitatList.AddRange(Colonies);
                habitatList.AddRange(ColonizationTargets.ResolveHabitats());
                if (otherEmpire != null)
                {
                    if (otherEmpire.PirateEmpireBaseHabitat != null)
                    {
                        if (otherEmpire.ColonizationTargets != null && otherEmpire.Colonies != null)
                        {
                            HabitatList habitatList2 = new HabitatList();
                            habitatList2.AddRange(otherEmpire.Colonies);
                            habitatList2.AddRange(otherEmpire.ColonizationTargets.ResolveHabitats());
                            for (int i = 0; i < depth; i++)
                            {
                                if (habitatList.Count <= i)
                                {
                                    continue;
                                }
                                Habitat habitat = habitatList[i];
                                if (habitat != null)
                                {
                                    int num = habitatList2.IndexOf(habitat);
                                    if (num >= 0 && num < depth)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    else if (otherEmpire.ColonizationTargets != null)
                    {
                        for (int j = 0; j < depth; j++)
                        {
                            if (habitatList.Count > j)
                            {
                                Habitat habitat2 = habitatList[j];
                                if (habitat2 != null && otherEmpire.Colonies.Contains(habitat2))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void ReviewPirateRelations(long starDate, double timePassed)
        {
            if (PirateRelations == null)
            {
                return;
            }
            bool flag = false;
            Design design = _LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.Escort, this);
            if (design != null && design.FirepowerRaw > 0 && design.ShieldsCapacity > 0)
            {
                flag = true;
            }
            double num = 0.5;
            if (!flag)
            {
                num = 0.85;
            }
            long num2 = (long)((double)Galaxy.RealSecondsInGalacticYear * 1.0 * 1000.0);
            int num3 = _Galaxy.PirateEmpires.Count;
            if (PirateRelations != null)
            {
                num3 = PirateRelations.CountKnownPirateFactions();
            }
            double num4 = Math.Min(7.0, Math.Max(1.0, (double)num3 / 3.0));
            num2 = (long)((double)num2 * num4);
            long num5 = (long)((double)Galaxy.RealSecondsInGalacticYear * 0.25);
            long num6 = (long)(((double)num5 * 0.5 + (double)num5 * 0.5 * Galaxy.Rnd.NextDouble()) * num4);
            num2 += num6;
            long num7 = starDate - num2;
            CharacterList charactersByRole = Characters.GetCharactersByRole(CharacterRole.Leader);
            int num8 = -100;
            for (int i = 0; i < charactersByRole.Count; i++)
            {
                Character character = charactersByRole[i];
                if (character != null && character.Role == CharacterRole.Leader)
                {
                    num8 = Math.Max(num8, character.Diplomacy);
                }
            }
            if (num8 <= -100)
            {
                num8 = 0;
            }
            float diplomacyFactor = 1f + (float)num8 / 100f;
            PirateRelationList pirateRelationList = new PirateRelationList();
            for (int j = 0; j < PirateRelations.Count; j++)
            {
                PirateRelation pirateRelation = PirateRelations[j];
                if (pirateRelation == null || pirateRelation.Type == PirateRelationType.NotMet)
                {
                    continue;
                }
                if (pirateRelation.OtherEmpire == null || !pirateRelation.OtherEmpire.Active || pirateRelation.ThisEmpire == null || !pirateRelation.ThisEmpire.Active)
                {
                    pirateRelationList.AddRaw(pirateRelation);
                    continue;
                }
                pirateRelation.DiplomacyFactor = diplomacyFactor;
                float neutralizationAmount = (float)(5.0 * (timePassed / ((double)Galaxy.RealSecondsInGalacticYear * 1000.0)));
                pirateRelation.NeutralizeEvaluation(neutralizationAmount);
                if (_ControlDiplomacyTreaties == AutomationLevel.Manual)
                {
                    continue;
                }
                PirateRelationType type = pirateRelation.Type;
                if (type != PirateRelationType.Protection || PirateEmpireBaseHabitat != null)
                {
                    continue;
                }
                double num9 = 0.0;
                PirateRelation pirateRelation2 = pirateRelation.OtherEmpire.ObtainPirateRelation(this);
                double monthlyFee = 0.0;
                if (pirateRelation2 != null)
                {
                    monthlyFee = pirateRelation2.MonthlyProtectionFeeToThisEmpire;
                    num9 = pirateRelation2.MonthlyProtectionFeeToThisEmpire * 12.0;
                }
                double num10 = CalculateAnnualCashflow();
                double num11 = num10 * num;
                if (num9 > num11)
                {
                    if (pirateRelation.LastChangeDate < num7)
                    {
                        int refusalCount = 0;
                        if (CheckTaskAuthorized(_ControlDiplomacyTreaties, ref refusalCount, GenerateAutomationMessageCancelPirateProtection(pirateRelation.OtherEmpire, monthlyFee), pirateRelation.OtherEmpire, AdvisorMessageType.TreatyOffer, PirateRelationType.None, null))
                        {
                            ChangePirateRelation(pirateRelation.OtherEmpire, PirateRelationType.None, starDate);
                            string text = TextResolver.GetText("Cancel Pirate Protection");
                            SendMessageToEmpire(pirateRelation.OtherEmpire, EmpireMessageType.CancelPirateProtection, this, text);
                        }
                    }
                }
                else if (!DetermineDesirePirateProtection(pirateRelation.OtherEmpire) && pirateRelation.LastChangeDate < num7)
                {
                    int refusalCount2 = 0;
                    if (CheckTaskAuthorized(_ControlDiplomacyTreaties, ref refusalCount2, GenerateAutomationMessageCancelPirateProtection(pirateRelation.OtherEmpire, monthlyFee), pirateRelation.OtherEmpire, AdvisorMessageType.TreatyOffer, PirateRelationType.None, null))
                    {
                        ChangePirateRelation(pirateRelation.OtherEmpire, PirateRelationType.None, starDate);
                        string text2 = TextResolver.GetText("Cancel Pirate Protection");
                        SendMessageToEmpire(pirateRelation.OtherEmpire, EmpireMessageType.CancelPirateProtection, this, text2);
                    }
                }
            }
            for (int k = 0; k < pirateRelationList.Count; k++)
            {
                PirateRelations.Remove(pirateRelationList[k]);
            }
        }

        private void PirateReviewEmpireRelations(long starDate, double timePassed)
        {
            if (PirateRelations == null)
            {
                return;
            }
            double num = Math.Max((double)Galaxy.SectorSize * 2.0, (double)Galaxy.SizeX * 0.2);
            PiratePlayStyle piratePlayStyle = PiratePlayStyle;
            if (piratePlayStyle == PiratePlayStyle.Balanced || piratePlayStyle == PiratePlayStyle.Smuggler || piratePlayStyle == PiratePlayStyle.Legendary)
            {
                num *= 2.0;
            }
            long num2 = (long)((double)Galaxy.RealSecondsInGalacticYear * 1.0 * 1000.0);
            CharacterList charactersByRole = Characters.GetCharactersByRole(CharacterRole.PirateLeader);
            int num3 = -100;
            for (int i = 0; i < charactersByRole.Count; i++)
            {
                Character character = charactersByRole[i];
                if (character != null && character.Role == CharacterRole.PirateLeader)
                {
                    num3 = Math.Max(num3, character.Diplomacy);
                }
            }
            if (num3 <= -100)
            {
                num3 = 0;
            }
            float diplomacyFactor = 1f + (float)num3 / 100f;
            PirateRelationList pirateRelationList = new PirateRelationList();
            for (int j = 0; j < PirateRelations.Count; j++)
            {
                PirateRelation pirateRelation = PirateRelations[j];
                if (pirateRelation == null || pirateRelation.Type == PirateRelationType.NotMet)
                {
                    continue;
                }
                if (pirateRelation.OtherEmpire == null || !pirateRelation.OtherEmpire.Active || pirateRelation.ThisEmpire == null || !pirateRelation.ThisEmpire.Active)
                {
                    pirateRelationList.AddRaw(pirateRelation);
                    continue;
                }
                long num4 = CalculateNextAllowableProposalDate(pirateRelation);
                long num5 = CalculateNextAllowableChangeDate(pirateRelation);
                if (pirateRelation.Type == PirateRelationType.Protection && pirateRelation.ThisEmpire == this)
                {
                    double num6 = 12.0 * ((double)(starDate - pirateRelation.LastProtectionFeePaymentDate) / ((double)Galaxy.RealSecondsInGalacticYear * 1000.0));
                    double num7 = num6 * pirateRelation.MonthlyProtectionFeeToThisEmpire;
                    pirateRelation.ThisEmpire.StateMoney += num7;
                    pirateRelation.ThisEmpire.PirateEconomy.PerformIncome(num7, PirateIncomeType.ProtectionAgreement, starDate);
                    pirateRelation.ThisEmpire.Counters.PirateProtectionIncome += num7;
                    pirateRelation.OtherEmpire.StateMoney -= num7;
                    pirateRelation.LastProtectionFeePaymentDate = starDate;
                }
                pirateRelation.DiplomacyFactor = diplomacyFactor;
                float neutralizationAmount = (float)(5.0 * (timePassed / (double)Galaxy.RealSecondsInGalacticYear));
                pirateRelation.NeutralizeEvaluation(neutralizationAmount);
                float evaluationLongRelationship = 0f;
                if (pirateRelation.Type == PirateRelationType.Protection)
                {
                    long num8 = pirateRelation.RelationshipLength(starDate);
                    long num9 = (long)((double)Galaxy.RealSecondsInGalacticYear * 1000.0 * 0.5);
                    if (num8 > num9)
                    {
                        double num10 = (double)Galaxy.RealSecondsInGalacticYear * 1000.0 * 0.16667;
                        evaluationLongRelationship = (float)((double)(num8 - num9) / num10);
                        evaluationLongRelationship = Math.Max(0f, Math.Min(20f, evaluationLongRelationship));
                    }
                }
                PiratePlayStyle piratePlayStyle2 = PiratePlayStyle;
                if (piratePlayStyle2 != PiratePlayStyle.Pirate)
                {
                    pirateRelation.EvaluationLongRelationship = evaluationLongRelationship;
                }
                pirateRelation.EvaluationCovetedColonies = CalculatePirateDesireControlColoniesCommon(pirateRelation.OtherEmpire, 3);
                if (_ControlDiplomacyTreaties == AutomationLevel.Manual)
                {
                    continue;
                }
                switch (pirateRelation.Type)
                {
                    case PirateRelationType.None:
                        if (PirateEmpireBaseHabitat == null || !DetermineDesirePirateProtection(pirateRelation.OtherEmpire))
                        {
                            break;
                        }
                        if (pirateRelation.OtherEmpire.PirateEmpireBaseHabitat != null)
                        {
                            if (starDate >= num4)
                            {
                                int refusalCount2 = 0;
                                if (CheckTaskAuthorized(_ControlDiplomacyTreaties, ref refusalCount2, GenerateAutomationMessagePirateProtectionToPirates(pirateRelation.OtherEmpire), pirateRelation.OtherEmpire, AdvisorMessageType.TreatyOffer, PirateRelationType.Protection, null))
                                {
                                    string text = TextResolver.GetText("Pirate Offer Protection Other Pirate");
                                    EmpireMessage empireMessage = new EmpireMessage(this, EmpireMessageType.PirateOfferProtection, null);
                                    empireMessage.Description = text;
                                    empireMessage.Money = 0;
                                    SendMessageToEmpire(empireMessage, pirateRelation.OtherEmpire);
                                    pirateRelation.LastOfferDate = starDate;
                                }
                            }
                        }
                        else if (CheckWithinProximityOfNearestColony(pirateRelation.OtherEmpire, this, num) && starDate >= num4)
                        {
                            double num11 = CalculatePirateProtectionPricePerMonth(pirateRelation.OtherEmpire);
                            int refusalCount3 = 0;
                            if (CheckTaskAuthorized(_ControlDiplomacyTreaties, ref refusalCount3, GenerateAutomationMessagePirateProtection(pirateRelation.OtherEmpire, num11), pirateRelation.OtherEmpire, AdvisorMessageType.TreatyOffer, PirateRelationType.Protection, null))
                            {
                                string text2 = TextResolver.GetText("Pirate Offer Protection");
                                EmpireMessage empireMessage2 = new EmpireMessage(this, EmpireMessageType.PirateOfferProtection, null);
                                empireMessage2.Description = text2;
                                empireMessage2.Money = (int)num11;
                                SendMessageToEmpire(empireMessage2, pirateRelation.OtherEmpire);
                                pirateRelation.LastOfferDate = starDate;
                            }
                        }
                        break;
                    case PirateRelationType.Protection:
                        if (PirateEmpireBaseHabitat != null && !DetermineDesirePirateProtection(pirateRelation.OtherEmpire) && starDate >= num5)
                        {
                            PirateRelation pirateRelation2 = pirateRelation.OtherEmpire.ObtainPirateRelation(this);
                            int refusalCount = 0;
                            if (CheckTaskAuthorized(_ControlDiplomacyTreaties, ref refusalCount, GenerateAutomationMessageCancelPirateProtection(pirateRelation.OtherEmpire, pirateRelation2.MonthlyProtectionFeeToThisEmpire), pirateRelation.OtherEmpire, AdvisorMessageType.TreatyOffer, PirateRelationType.None, null))
                            {
                                ChangePirateRelation(pirateRelation.OtherEmpire, PirateRelationType.None, starDate);
                                string empty = string.Empty;
                                SendMessageToEmpire(description: (pirateRelation.OtherEmpire.PirateEmpireBaseHabitat == null) ? TextResolver.GetText("Pirates Cancel Pirate Protection Normal") : TextResolver.GetText("Pirates Cancel Pirate Protection Pirates"), recipientEmpire: pirateRelation.OtherEmpire, messageType: EmpireMessageType.CancelPirateProtection, subject: this);
                            }
                        }
                        break;
                }
            }
            for (int k = 0; k < pirateRelationList.Count; k++)
            {
                PirateRelations.Remove(pirateRelationList[k]);
            }
        }

        public double CalculatePirateProtectionPricePerMonth(Empire empireToProtect)
        {
            double pirateAttackForcesFactor = 0.0;
            return CalculatePirateProtectionPricePerMonth(empireToProtect, out pirateAttackForcesFactor);
        }

        public double CalculatePirateProtectionPricePerMonth(Empire empireToProtect, out double pirateAttackForcesFactor)
        {
            pirateAttackForcesFactor = 0.0;
            double result = 0.0;
            if (PirateEmpireBaseHabitat != null)
            {
                if (empireToProtect.PirateEmpireBaseHabitat != null)
                {
                    result = 0.0;
                }
                else
                {
                    double num = Math.Sqrt((double)empireToProtect.TotalColonyStrategicValue / 1000000.0);
                    double val = ((double)BuiltObjects.TotalMobileMilitaryFirepower() + 1.0) / ((double)empireToProtect.BuiltObjects.TotalMobileMilitaryFirepower() + 1.0);
                    val = Math.Max(0.05, Math.Min(0.4, val));
                    double num2 = 1.0;
                    PirateRelation pirateRelation = ObtainPirateRelation(empireToProtect);
                    if (pirateRelation != null)
                    {
                        float evaluation = pirateRelation.Evaluation;
                        if (evaluation < 0f)
                        {
                            num2 = Math.Min(4.0, Math.Max(1.0, 1f + Math.Abs(evaluation) / 15f));
                        }
                    }
                    double num3 = 1.0;
                    int num4 = BuiltObjects.CalculateAttackingFirepowerNearEmpireTargets(empireToProtect);
                    if (num4 > 0)
                    {
                        int num5 = 1 + empireToProtect.BuiltObjects.TotalMobileMilitaryFirepower();
                        num3 = Math.Min(4.0, 2.0 + (double)num4 / (double)num5);
                        pirateAttackForcesFactor = num3 - 2.0;
                    }
                    double num6 = 1000.0 * val * num * num2 * num3;
                    double annualEmpireExpenses = 0.0;
                    double num7 = empireToProtect.CalculateAccurateAnnualCashflowIncludingUnderConstruction(out annualEmpireExpenses);
                    double num8 = num7 / 12.0;
                    double num9 = val * num2 * num3 / num;
                    result = Math.Min(num6, Math.Max(num6 * 0.4, num8 * num9));
                    result = Math.Round(result, 0);
                }
            }
            return result;
        }

        private double CalculateDistanceToNearestColony(Empire normalEmpire, Empire pirateEmpire)
        {
            double result = double.MaxValue;
            Habitat habitat = null;
            if (pirateEmpire.PirateEmpireBaseHabitat != null)
            {
                habitat = _Galaxy.FastFindNearestColony(pirateEmpire.PirateEmpireBaseHabitat.Xpos, pirateEmpire.PirateEmpireBaseHabitat.Ypos, normalEmpire, 0);
            }
            else if (pirateEmpire.Capital != null)
            {
                habitat = _Galaxy.FastFindNearestColony(pirateEmpire.Capital.Xpos, pirateEmpire.Capital.Ypos, normalEmpire, 0);
            }
            if (habitat != null)
            {
                if (pirateEmpire.PirateEmpireBaseHabitat != null)
                {
                    result = _Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, pirateEmpire.PirateEmpireBaseHabitat.Xpos, pirateEmpire.PirateEmpireBaseHabitat.Ypos);
                }
                else if (pirateEmpire.Capital != null)
                {
                    result = _Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, pirateEmpire.Capital.Xpos, pirateEmpire.Capital.Ypos);
                }
            }
            return result;
        }

        private bool CheckWithinProximityOfNearestColony(Empire normalEmpire, Empire pirateEmpire, double range)
        {
            Habitat habitat = null;
            if (pirateEmpire.PirateEmpireBaseHabitat != null)
            {
                habitat = _Galaxy.FastFindNearestColony(pirateEmpire.PirateEmpireBaseHabitat.Xpos, pirateEmpire.PirateEmpireBaseHabitat.Ypos, normalEmpire, 0);
            }
            else if (pirateEmpire.Capital != null)
            {
                habitat = _Galaxy.FastFindNearestColony(pirateEmpire.Capital.Xpos, pirateEmpire.Capital.Ypos, normalEmpire, 0);
            }
            if (habitat != null)
            {
                double num = double.MaxValue;
                if (pirateEmpire.PirateEmpireBaseHabitat != null)
                {
                    num = _Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, pirateEmpire.PirateEmpireBaseHabitat.Xpos, pirateEmpire.PirateEmpireBaseHabitat.Ypos);
                }
                else if (pirateEmpire.Capital != null)
                {
                    num = _Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, pirateEmpire.Capital.Xpos, pirateEmpire.Capital.Ypos);
                }
                if (num < range)
                {
                    return true;
                }
            }
            return false;
        }

        public bool DetermineDesirePirateProtection(Empire otherEmpire)
        {
            if (otherEmpire != null)
            {
                if (PirateEmpireBaseHabitat == null)
                {
                    if (otherEmpire.PirateEmpireBaseHabitat == null)
                    {
                        return false;
                    }
                    double pirateAttackForcesFactor = 0.0;
                    double num = otherEmpire.CalculatePirateProtectionPricePerMonth(this, out pirateAttackForcesFactor);
                    PirateRelation pirateRelation = ObtainPirateRelation(otherEmpire);
                    double num2 = CalculateDistanceToNearestColony(this, otherEmpire);
                    bool flag = false;
                    bool flag2 = false;
                    Design design = _LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.Escort, this);
                    Design design2 = _LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.Escort, this);
                    if (design != null && design2 != null && (design.ShieldsCapacity > 0 || design2.ShieldsCapacity > 0))
                    {
                        flag2 = true;
                    }
                    int num3 = BuiltObjects.TotalMobileMilitaryFirepower();
                    if (num3 > 0)
                    {
                        flag = true;
                    }
                    double num4 = 0.4;
                    if (pirateAttackForcesFactor > 0.0)
                    {
                        num4 *= 1.5 + pirateAttackForcesFactor;
                    }
                    if (!flag || !flag2)
                    {
                        num4 = 0.9;
                    }
                    num4 = Math.Min(num4, 0.9);
                    int num5 = otherEmpire.BuiltObjects.TotalMobileMilitaryFirepower();
                    int num6 = BuiltObjects.TotalMobileMilitaryFirepower();
                    float num7 = ((float)num5 + 0.05f) / ((float)num6 + 0.05f);
                    if (num7 > 1f)
                    {
                        float num8 = (float)Math.Sqrt(Math.Min(10.0, num7));
                        num4 *= (double)num8;
                    }
                    if (CheckSufficientCashflow(num * 12.0, num4) && (num == 0.0 || StateMoney >= num))
                    {
                        if ((double)pirateRelation.Evaluation >= 5.0 && num2 < (double)Galaxy.SectorSize * 2.0)
                        {
                            return true;
                        }
                        float num9 = Math.Min(0f, Math.Max(-2000f, num7 * -10f + 5f));
                        if (pirateRelation.Evaluation > num9)
                        {
                            return true;
                        }
                    }
                }
                else if (otherEmpire.PirateEmpireBaseHabitat == null)
                {
                    PirateRelation pirateRelation2 = ObtainPirateRelation(otherEmpire);
                    if (pirateRelation2.Evaluation >= 0f)
                    {
                        return true;
                    }
                    int num10 = otherEmpire.BuiltObjects.TotalMobileMilitaryFirepower();
                    int num11 = BuiltObjects.TotalMobileMilitaryFirepower();
                    float num12 = ((float)num10 + 0.05f) / ((float)num11 + 0.05f);
                    float num13 = Math.Min(0f, Math.Max(-35f, num12 * -10f + 5f));
                    if (pirateRelation2.Evaluation > num13)
                    {
                        return true;
                    }
                }
                else
                {
                    PirateRelation pirateRelation3 = ObtainPirateRelation(otherEmpire);
                    if (pirateRelation3.Evaluation >= 0f && !CheckPirateDesireControlColoniesCommon(otherEmpire, 3))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void PirateCollectTaxes(double timePassed)
        {
            double privateAnnualRevenue = PrivateAnnualRevenue;
            double num = privateAnnualRevenue * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
            if (double.IsNaN(num))
            {
                num = 0.0;
            }
            Counters.ProcessColonyRevenue(num);
            _PrivateMoney += num;
            double annualTaxRevenue = AnnualTaxRevenue;
            double val = annualTaxRevenue * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
            val = Math.Max(0.0, val);
            if (double.IsNaN(val))
            {
                val = 0.0;
            }
            val = ApplyCorruptionToIncome(val);
            _StateMoney += val;
            _PrivateMoney -= val;
            PirateEconomy.PerformIncome(val, PirateIncomeType.ControlColony, _Galaxy.CurrentStarDate);
            if (double.IsNaN(_StateMoney))
            {
                _StateMoney = 0.0;
            }
            if (double.IsNaN(_PrivateMoney))
            {
                _PrivateMoney = 0.0;
            }
        }

        public int CountPirateCriminalNetworks()
        {
            int num = 0;
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat == null || habitat.HasBeenDestroyed || habitat.Facilities == null || habitat.Facilities.Count <= 0)
                {
                    continue;
                }
                if (habitat.Empire == this)
                {
                    num += habitat.Facilities.CountByType(PlanetaryFacilityType.PirateCriminalNetwork);
                    continue;
                }
                PirateColonyControl byFacilityControl = habitat.GetPirateControl().GetByFacilityControl();
                if (byFacilityControl != null && byFacilityControl.EmpireId == EmpireId)
                {
                    num += habitat.Facilities.CountByType(PlanetaryFacilityType.PirateCriminalNetwork);
                }
            }
            return num;
        }

        private void PirateCollectIncomeFromControlledColonies(double timePassed)
        {
            double num = 0.0;
            double num2 = timePassed / (double)Galaxy.RealSecondsInGalacticYear;
            bool flag = false;
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat == null || habitat.HasBeenDestroyed)
                {
                    continue;
                }
                if (habitat.Empire == this && !habitat.CheckColonyRevenueFromPirateControl(this))
                {
                    flag = true;
                    continue;
                }
                PirateColonyControl byFaction = habitat.GetPirateControl().GetByFaction(this);
                if (byFaction == null)
                {
                    continue;
                }
                double num3 = 1.0;
                if (habitat.Facilities != null)
                {
                    PlanetaryFacility planetaryFacility = habitat.Facilities.FindBestCompletedPirateFacility(includeCriminalNetwork: true);
                    if (planetaryFacility != null)
                    {
                        num3 += (double)planetaryFacility.Value2 / 100.0;
                    }
                }
                double num4 = num2 * Math.Max(0.0, habitat.AnnualRevenue) * (double)byFaction.ControlLevel * 1.2 * num3;
                num4 *= ColonyIncomeFactor;
                num += num4;
            }
            num = ApplyCorruptionToIncome(num);
            StateMoney += num;
            PirateEconomy.PerformIncome(num, PirateIncomeType.ControlColony, _Galaxy.CurrentStarDate);
            if (flag)
            {
                RecalculateColonyTaxRevenues();
                if (_ControlColonyTaxRates)
                {
                    ReviewTaxes();
                }
                PirateCollectTaxes(timePassed);
            }
        }

        private void ReviewFleetAdmiralBonuses()
        {
            if (ShipGroups != null)
            {
                for (int i = 0; i < ShipGroups.Count; i++)
                {
                    ShipGroups[i].ReviewAdmiralBonuses();
                }
            }
        }

        public double AverageStateCashPerPopulation()
        {
            return StateMoney / (double)Math.Max(1L, TotalPopulation);
        }

        public double AverageCashflowPerPopulation()
        {
            double num = CalculateAccurateAnnualCashflow();
            return num / (double)Math.Max(1L, TotalPopulation);
        }

        public double AverageShipMaintenancePerPopulation()
        {
            double annualStateMaintenanceExcludingUnderConstruction = AnnualStateMaintenanceExcludingUnderConstruction;
            return annualStateMaintenanceExcludingUnderConstruction / (double)Math.Max(1L, TotalPopulation);
        }

        public double AverageMilitaryStrengthPerPopulation()
        {
            double num = MilitaryPotency;
            return num / (double)Math.Max(1L, TotalPopulation);
        }

        public double AverageSpaceportsPerColony()
        {
            double num = 0.0;
            if (SpacePorts != null)
            {
                num = SpacePorts.Count;
            }
            double val = 0.01;
            if (Colonies != null)
            {
                val = Colonies.Count;
            }
            return num / Math.Max(1.0, val);
        }

        public double AverageResearchStationsPerColony()
        {
            double num = 0.0;
            if (ResearchFacilities != null)
            {
                num = ResearchFacilities.Count;
            }
            double val = 0.01;
            if (Colonies != null)
            {
                val = Colonies.Count;
            }
            return num / Math.Max(1.0, val);
        }

        public double AverageMiningStationsPerColony()
        {
            double num = 0.0;
            if (MiningStations != null)
            {
                num = MiningStations.Count;
            }
            double val = 0.01;
            if (Colonies != null)
            {
                val = Colonies.Count;
            }
            return num / Math.Max(1.0, val);
        }

        public double AverageCapitalShipsPerColony()
        {
            double num = 0.0;
            if (BuiltObjects != null)
            {
                int num2 = 0;
                for (int i = 0; i < BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = BuiltObjects[i];
                    if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.UnbuiltComponentCount <= 0 && builtObject.SubRole == BuiltObjectSubRole.CapitalShip)
                    {
                        num2++;
                    }
                }
                num = num2;
            }
            double val = 0.01;
            if (Colonies != null)
            {
                val = Colonies.Count;
            }
            return num / Math.Max(1.0, val);
        }

        public double AverageConstructionShipsPerColony()
        {
            double num = 0.0;
            if (BuiltObjects != null)
            {
                int num2 = 0;
                for (int i = 0; i < BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = BuiltObjects[i];
                    if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.UnbuiltComponentCount <= 0 && builtObject.SubRole == BuiltObjectSubRole.ConstructionShip)
                    {
                        num2++;
                    }
                }
                num = num2;
            }
            double val = 0.01;
            if (Colonies != null)
            {
                val = Colonies.Count;
            }
            return num / Math.Max(1.0, val);
        }

        public double AverageHappiness()
        {
            double num = 0.0;
            long num2 = 0L;
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat != null && habitat.Population != null)
                {
                    if (i > 0)
                    {
                        long totalAmount = habitat.Population.TotalAmount;
                        num2 += totalAmount;
                        double empireApprovalRating = habitat.EmpireApprovalRating;
                        double num3 = totalAmount / Math.Max(1L, num2);
                        num += (empireApprovalRating - num) * num3;
                    }
                    else
                    {
                        num2 = habitat.Population.TotalAmount;
                        num = habitat.EmpireApprovalRating;
                    }
                }
            }
            return num;
        }

        public HabitatList GetHomeworldsOwned()
        {
            HabitatList habitatList = new HabitatList();
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire != null && empire.HomeWorld != null && Colonies.Contains(empire.HomeWorld) && !habitatList.Contains(empire.HomeWorld))
                {
                    habitatList.Add(empire.HomeWorld);
                }
            }
            for (int j = 0; j < _Galaxy.DefeatedEmpires.Count; j++)
            {
                Empire empire2 = _Galaxy.DefeatedEmpires[j];
                if (empire2 != null && empire2.HomeWorld != null && Colonies.Contains(empire2.HomeWorld) && !habitatList.Contains(empire2.HomeWorld))
                {
                    habitatList.Add(empire2.HomeWorld);
                }
            }
            return habitatList;
        }

        public int CountHomeworldsOwned()
        {
            int num = 0;
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire != null && empire.HomeWorld != null && Colonies.Contains(empire.HomeWorld))
                {
                    num++;
                }
            }
            for (int j = 0; j < _Galaxy.DefeatedEmpires.Count; j++)
            {
                Empire empire2 = _Galaxy.DefeatedEmpires[j];
                if (empire2 != null && empire2.HomeWorld != null && Colonies.Contains(empire2.HomeWorld))
                {
                    num++;
                }
            }
            return num;
        }

        public BuiltObject LargestCapitalShip()
        {
            BuiltObject builtObject = null;
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject2 = BuiltObjects[i];
                if (builtObject2 != null && !builtObject2.HasBeenDestroyed && builtObject2.UnbuiltComponentCount == 0 && builtObject2.SubRole == BuiltObjectSubRole.CapitalShip && (builtObject == null || builtObject2.Size > builtObject.Size))
                {
                    builtObject = builtObject2;
                }
            }
            return builtObject;
        }

        public int CalculateMilitaryShipSizeTotal()
        {
            int num = 0;
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = BuiltObjects[i];
                if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.UnbuiltComponentCount == 0 && builtObject.Role == BuiltObjectRole.Military)
                {
                    num += builtObject.Size;
                }
            }
            return num;
        }

        public long CalculateEnslavedPopulationAmount()
        {
            long num = 0L;
            Race dominantRace = DominantRace;
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                for (int j = 0; j < habitat.Population.Count; j++)
                {
                    Population population = habitat.Population[j];
                    ColonyPopulationPolicy colonyPopulationPolicy = ColonyPopulationPolicy.Assimilate;
                    if (population.Race != dominantRace)
                    {
                        colonyPopulationPolicy = habitat.ColonyPopulationPolicy;
                        if (population.Race.FamilyId == dominantRace.FamilyId)
                        {
                            colonyPopulationPolicy = habitat.ColonyPopulationPolicyRaceFamily;
                        }
                    }
                    if (colonyPopulationPolicy == ColonyPopulationPolicy.Enslave)
                    {
                        num += population.Amount;
                    }
                }
            }
            return num;
        }

        private void ReviewColonyPopulationPolicy(double timePassed)
        {
            Race dominantRace = DominantRace;
            if (dominantRace == null)
            {
                return;
            }
            long num = (long)(timePassed / (double)Galaxy.RealSecondsInGalacticYear * 500000000.0);
            PopulationList populationList = new PopulationList();
            PopulationList populationList2 = new PopulationList();
            HabitatList habitatList = new HabitatList();
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                long totalAmount = habitat.Population.TotalAmount;
                long num2 = 0L;
                PopulationList populationList3 = new PopulationList();
                for (int j = 0; j < habitat.Population.Count; j++)
                {
                    Population population = habitat.Population[j];
                    ColonyPopulationPolicy colonyPopulationPolicy = ColonyPopulationPolicy.Assimilate;
                    if (population.Race != dominantRace)
                    {
                        colonyPopulationPolicy = habitat.ColonyPopulationPolicy;
                        if (population.Race.FamilyId == dominantRace.FamilyId)
                        {
                            colonyPopulationPolicy = habitat.ColonyPopulationPolicyRaceFamily;
                        }
                    }
                    switch (colonyPopulationPolicy)
                    {
                        case ColonyPopulationPolicy.Enslave:
                            population.GrowthRate = 1f;
                            num2 += population.Amount;
                            populationList.Add(new Population(population.Race, population.Amount));
                            break;
                        case ColonyPopulationPolicy.Exterminate:
                            if (totalAmount > 30000000)
                            {
                                long num3 = Math.Min(population.Amount, num);
                                long num4 = Math.Max(0L, population.Amount - num3);
                                populationList2.Add(new Population(population.Race, num));
                                if (num4 <= 0)
                                {
                                    populationList3.Add(population);
                                }
                                else
                                {
                                    population.Amount = num4;
                                }
                            }
                            break;
                    }
                }
                for (int k = 0; k < populationList3.Count; k++)
                {
                    habitat.Population.Remove(populationList3[k]);
                }
                habitat.Population.RecalculateTotalAmount();
                if (num2 > 0)
                {
                    double num5 = (double)num2 / (double)habitat.Population.TotalAmount;
                    habitat.SlaveryBonusFactor = (float)(1.0 + num5 * 0.5);
                }
                else
                {
                    habitat.SlaveryBonusFactor = 1f;
                }
                if (habitat.Population.Count == 0 || habitat.Population.TotalAmount <= 0)
                {
                    habitatList.Add(habitat);
                }
            }
            for (int l = 0; l < habitatList.Count; l++)
            {
                habitatList[l].ClearColony(null);
            }
            for (int m = 0; m < EmpireEvaluations.Count; m++)
            {
                EmpireEvaluation empireEvaluation = EmpireEvaluations[m];
                if (empireEvaluation != null && empireEvaluation.RacialOffense < 0.0)
                {
                    double num6 = timePassed / (double)Galaxy.RealSecondsInGalacticYear * 3.0;
                    empireEvaluation.RacialOffense += num6;
                    if (empireEvaluation.RacialOffense > 0.0)
                    {
                        empireEvaluation.RacialOffense = 0.0;
                    }
                }
            }
            for (int n = 0; n < _Galaxy.Empires.Count; n++)
            {
                Empire empire = _Galaxy.Empires[n];
                if (empire != null && empire.Active && empire != this && empire != _Galaxy.IndependentEmpire)
                {
                    EmpireEvaluation empireEvaluation2 = empire.ObtainEmpireEvaluation(this);
                    empireEvaluation2.SetSlaveryOffense(0.0);
                }
            }
            for (int num7 = 0; num7 < populationList.Count; num7++)
            {
                Population population2 = populationList[num7];
                EmpireList empireList = DetermineEmpiresWithDominantRace(population2.Race);
                for (int num8 = 0; num8 < empireList.Count; num8++)
                {
                    Empire empire2 = empireList[num8];
                    if (empire2 != null)
                    {
                        EmpireEvaluation empireEvaluation3 = empire2.ObtainEmpireEvaluation(this);
                        double slaveryOffense = Math.Sqrt(Math.Sqrt((double)population2.Amount / 600000.0)) * -1.0;
                        empireEvaluation3.SetSlaveryOffense(slaveryOffense);
                        if (empireEvaluation3.SlaveryOffense < -30.0)
                        {
                            empireEvaluation3.SetSlaveryOffense(-30.0);
                        }
                        if (empireEvaluation3.SlaveryOffense > 0.0)
                        {
                            empireEvaluation3.SetSlaveryOffense(0.0);
                        }
                    }
                }
            }
            for (int num9 = 0; num9 < populationList2.Count; num9++)
            {
                Population population3 = populationList2[num9];
                Counters.ProcessExterminatedPopulation(population3.Amount);
                EmpireList empireList2 = DetermineEmpiresWithDominantRace(population3.Race);
                double num10 = 0.0;
                for (int num11 = 0; num11 < empireList2.Count; num11++)
                {
                    Empire empire3 = empireList2[num11];
                    if (empire3 != null)
                    {
                        EmpireEvaluation empireEvaluation4 = empire3.ObtainEmpireEvaluation(this);
                        double num12 = Math.Sqrt((double)population3.Amount / 2000000.0) * -1.0;
                        empireEvaluation4.RacialOffense += num12;
                        if (empireEvaluation4.RacialOffense < -50.0)
                        {
                            empireEvaluation4.RacialOffense = -50.0;
                        }
                        num10 += empire3.CivilityRating;
                    }
                }
                double num13 = (double)population3.Amount / 500000000.0;
                if (empireList2 != null && empireList2.Count > 0)
                {
                    num10 /= (double)empireList2.Count;
                }
                if (num10 > 0.0)
                {
                    double num14 = 1.0 + num10 / 30.0;
                    num13 *= num14;
                }
                else
                {
                    double val = 1.0 + num10 / 50.0;
                    val = Math.Max(0.01, val);
                    num13 *= val;
                }
                CivilityRating -= num13;
            }
            if (!_ControlPopulationPolicy)
            {
                return;
            }
            HabitatList habitatList2 = new HabitatList();
            for (int num15 = 0; num15 < _PenalColonies.Count; num15++)
            {
                Habitat habitat2 = _PenalColonies[num15];
                if (habitat2.ColonyPopulationPolicy != ColonyPopulationPolicy.Enslave && habitat2.ColonyPopulationPolicyRaceFamily != ColonyPopulationPolicy.Enslave)
                {
                    habitatList2.Add(habitat2);
                }
            }
            for (int num16 = 0; num16 < habitatList2.Count; num16++)
            {
                _PenalColonies.Remove(habitatList2[num16]);
            }
            if (!Policy.ImplementEnslavementWithPenalColonies)
            {
                return;
            }
            HabitatList habitatList3 = IdentifyBestNewPenalColonies();
            HabitatList habitatList4 = new HabitatList();
            int num17 = 0;
            for (int num18 = 0; num18 < _PenalColonies.Count; num18++)
            {
                Habitat habitat3 = _PenalColonies[num18];
                if (habitat3 != null && habitat3.Population != null)
                {
                    double num19 = (double)habitat3.Population.TotalAmount / (double)habitat3.MaximumPopulation;
                    if (num19 < 0.8)
                    {
                        num17++;
                    }
                }
            }
            if (_PenalColonies.Count >= 5)
            {
                num17 = 1;
            }
            if (num17 <= 0 && habitatList3.Count > 0)
            {
                habitatList4.Add(habitatList3[0]);
            }
            int num20 = 0;
            for (int num21 = 0; num21 < Colonies.Count; num21++)
            {
                Habitat habitat4 = Colonies[num21];
                if (habitat4 == null || (habitat4.ColonyPopulationPolicy != ColonyPopulationPolicy.Enslave && habitat4.ColonyPopulationPolicyRaceFamily != ColonyPopulationPolicy.Enslave))
                {
                    continue;
                }
                num20++;
                if (!_PenalColonies.Contains(habitat4) && !habitatList4.Contains(habitat4) && habitat4.RaceEventType != RaceEventType.AntiXenoRiotsExterminate && habitat4.RaceEventType != RaceEventType.DeathCultExterminate)
                {
                    if (habitat4.ColonyPopulationPolicy == ColonyPopulationPolicy.Enslave)
                    {
                        habitat4.ColonyPopulationPolicy = ColonyPopulationPolicy.Resettle;
                    }
                    if (habitat4.ColonyPopulationPolicyRaceFamily == ColonyPopulationPolicy.Enslave)
                    {
                        habitat4.ColonyPopulationPolicyRaceFamily = ColonyPopulationPolicy.Resettle;
                    }
                }
            }
            if (num20 <= 0)
            {
                return;
            }
            for (int num22 = 0; num22 < habitatList4.Count; num22++)
            {
                Habitat habitat5 = habitatList4[num22];
                if (!_PenalColonies.Contains(habitat5))
                {
                    Habitat habitat6 = Galaxy.DetermineHabitatSystemStar(habitat5);
                    habitat5.Name = string.Format(TextResolver.GetText("SYSTEM Penal Colony"), habitat6.Name);
                    _PenalColonies.Add(habitat5);
                }
            }
        }

        private HabitatList IdentifyBestNewPenalColonies()
        {
            HabitatList habitatList = new HabitatList();
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat != null && !_PenalColonies.Contains(habitat))
                {
                    habitat.SortTag = habitat.CalculatePenalColonyValue();
                    if (habitat.SortTag > 0.0)
                    {
                        habitatList.Add(habitat);
                    }
                }
            }
            habitatList.Sort();
            habitatList.Reverse();
            return habitatList;
        }

        public EmpireList DetermineEmpiresWithDominantRace(Race race)
        {
            EmpireList empireList = new EmpireList();
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire != null && empire.DominantRace == race)
                {
                    empireList.Add(empire);
                }
            }
            return empireList;
        }

        public bool CheckLocationHintExistsAtPoint(double x, double y, double range)
        {
            for (int i = 0; i < LocationHints.Count; i++)
            {
                double num = _Galaxy.CalculateDistance(x, y, LocationHints[i].X, LocationHints[i].Y);
                if (num <= range)
                {
                    return true;
                }
            }
            return false;
        }

        private void ShakturiSendConvoy()
        {
            Empire empire = _Galaxy.IdentifyShakturiEmpire();
            if (empire == null || this != empire || Galaxy.Rnd.Next(0, 3) != 1)
            {
                return;
            }
            double num = (double)empire.BuiltObjects.Count / (double)empire.Colonies.Count;
            if (num < 40.0 && !_Galaxy.ShakturiDefeated)
            {
                if (_Galaxy.StoryShakturiEnraged)
                {
                    _Galaxy.GenerateShakturiMilitaryConvoy(this);
                }
                else
                {
                    _Galaxy.GenerateShakturiColonyConvoy(this);
                }
            }
        }

        private void CheckOfferStoryHint()
        {
            if ((!_Galaxy.StoryReturnOfTheShakturiEnabled && !_Galaxy.StoryDistantWorldsEnabled) || _Galaxy.PlayerEmpire == null || DominantRace == null)
            {
                return;
            }
            int num = -1;
            bool flag = false;
            bool flag2 = false;
            Empire empire = _Galaxy.IdentifyShakturiEmpire();
            Empire empire2 = _Galaxy.IdentifyMechanoidEmpire();
            DiplomaticRelation diplomaticRelation = null;
            DiplomaticRelation diplomaticRelation2 = null;
            Race shakturiActualRace = _Galaxy.ShakturiActualRace;
            Race race = _Galaxy.Races["Mechanoid"];
            if (_Galaxy.StoryReturnOfTheShakturiEnabled)
            {
                for (int i = 0; i < _Galaxy.Empires.Count; i++)
                {
                    if (_Galaxy.Empires[i].DominantRace == shakturiActualRace && _Galaxy.Empires[i] != _Galaxy.PlayerEmpire)
                    {
                        empire = _Galaxy.Empires[i];
                        diplomaticRelation = _Galaxy.PlayerEmpire.ObtainDiplomaticRelation(empire);
                    }
                    else if (_Galaxy.Empires[i].DominantRace == race && _Galaxy.Empires[i] != _Galaxy.PlayerEmpire)
                    {
                        empire2 = _Galaxy.Empires[i];
                        diplomaticRelation2 = _Galaxy.PlayerEmpire.ObtainDiplomaticRelation(empire2);
                    }
                }
            }
            if ((DominantRace.Name.ToLower(CultureInfo.InvariantCulture) == "zenox" || DominantRace.Name.ToLower(CultureInfo.InvariantCulture) == "quameno") && PirateEmpireBaseHabitat == null)
            {
                bool flag3 = false;
                if (_Galaxy.PlayerEmpire.PirateEmpireBaseHabitat == null)
                {
                    DiplomaticRelation diplomaticRelation3 = ObtainDiplomaticRelation(_Galaxy.PlayerEmpire);
                    EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(_Galaxy.PlayerEmpire);
                    if (diplomaticRelation3 != null && diplomaticRelation3.Type != 0 && empireEvaluation != null && empireEvaluation.OverallAttitude > 10 && diplomaticRelation3.Strategy != DiplomaticStrategy.Conquer && diplomaticRelation3.Strategy != DiplomaticStrategy.Punish && diplomaticRelation3.Strategy != DiplomaticStrategy.Undermine)
                    {
                        flag3 = true;
                    }
                }
                else
                {
                    PirateRelation pirateRelation = ObtainPirateRelation(_Galaxy.PlayerEmpire);
                    if (pirateRelation.Type == PirateRelationType.Protection && pirateRelation.Evaluation > 10f)
                    {
                        flag3 = true;
                    }
                }
                if (flag3)
                {
                    if (Galaxy.Rnd.Next(0, 4) == 1)
                    {
                        int num2 = _Galaxy.SelectUnusedSecondaryStoryClueIndex();
                        if (num2 >= 0)
                        {
                            string text = TextResolver.GetText("We offer to reveal secret history");
                            SendMessageToEmpire(_Galaxy.PlayerEmpire, EmpireMessageType.HistoryOfferStoryClue, null, text);
                            return;
                        }
                        if (_Galaxy.CheckStoryLocationHintExists())
                        {
                            int num3 = _Galaxy.SelectUnusedStoryClue();
                            if (num3 >= 0)
                            {
                                StellarObject stellarObject = null;
                                if (_Galaxy.StoryClueLocations != null && _Galaxy.StoryClueLocations.Count > num3)
                                {
                                    stellarObject = _Galaxy.StoryClueLocations[num3];
                                }
                                if (stellarObject != null && !_Galaxy.PlayerEmpire.CheckLocationHintExistsAtPoint(stellarObject.Xpos, stellarObject.Ypos, 5000.0))
                                {
                                    string text2 = TextResolver.GetText("We offer to reveal a secret location");
                                    SendMessageToEmpire(_Galaxy.PlayerEmpire, EmpireMessageType.HistoryOfferLocationHint, null, text2);
                                    return;
                                }
                            }
                        }
                    }
                    else if (_Galaxy.StoryReturnOfTheShakturiEnabled && (diplomaticRelation2 == null || diplomaticRelation2.Type == DiplomaticRelationType.NotMet))
                    {
                        num = 2;
                        flag = true;
                    }
                }
            }
            else if (DominantRace.Name.ToLower(CultureInfo.InvariantCulture) == "mechanoid")
            {
                DiplomaticRelation diplomaticRelation4 = ObtainDiplomaticRelation(_Galaxy.PlayerEmpire);
                if (diplomaticRelation4 != null && diplomaticRelation4.Type != 0 && _Galaxy.StoryReturnOfTheShakturiEnabled)
                {
                    flag2 = true;
                    num = 3;
                    int num4 = 0;
                    for (int j = 0; j < _Galaxy.Empires.Count; j++)
                    {
                        if (_Galaxy.Empires[j] != null && _Galaxy.Empires[j].Active && _Galaxy.Empires[j].Colonies != null)
                        {
                            num4 += _Galaxy.Empires[j].Colonies.Count;
                        }
                    }
                    int num5 = (int)(0.5 * (double)_Galaxy.ExpectedMaximumColoniesInGalaxy);
                    double d = (double)_Galaxy.BaseTechCost / 120000.0;
                    d = Math.Max(1.0, Math.Sqrt(d));
                    int num6 = 25;
                    if (_Galaxy.Age == 0)
                    {
                        num6 += 15;
                    }
                    long val = _Galaxy.ActualStartDate + Galaxy.RealSecondsInGalacticYear * 1000 * num6;
                    int val2 = (int)((double)num6 * d);
                    val2 = Math.Min(val2, 120);
                    int num7 = Math.Max(val2, 80);
                    long num8 = _Galaxy.ActualStartDate + Galaxy.RealSecondsInGalacticYear * 1000 * num7;
                    long val3 = _Galaxy.ActualStartDate + Galaxy.RealSecondsInGalacticYear * 1000 * val2;
                    val3 = Math.Max(val3, val);
                    lock (_Galaxy.StoryLock)
                    {
                        if (_Galaxy.ShakturiTriggerHabitat == null && ((num4 >= num5 && _Galaxy.CurrentStarDate >= val3) || _Galaxy.CurrentStarDate >= num8))
                        {
                            _Galaxy.GenerateShakturiReturnTriggerRuins();
                        }
                        else if (_Galaxy.ShakturiTriggerHabitat == null)
                        {
                            num = 0;
                        }
                    }
                }
            }
            else if (DominantRace == _Galaxy.ShakturiActualRace)
            {
                num = 0;
            }
            if (num < 0 || !_Galaxy.StoryReturnOfTheShakturiEnabled)
            {
                return;
            }
            _ = string.Empty;
            bool flag4 = false;
            switch (_Galaxy.StoryReturnOfTheShakturiEventLevel)
            {
                case 0:
                    if (empire != null)
                    {
                        _Galaxy.StoryReturnOfTheShakturiEventLevel = 1;
                        goto case 1;
                    }
                    flag4 = true;
                    break;
                case 1:
                    if (empire == null || DominantRace == _Galaxy.ShakturiActualRace)
                    {
                        break;
                    }
                    if (diplomaticRelation != null && diplomaticRelation.Type != 0)
                    {
                        _Galaxy.StoryReturnOfTheShakturiEventLevel = 2;
                        goto case 2;
                    }
                    flag4 = true;
                    break;
                case 2:
                    {
                        if (empire == null)
                        {
                            break;
                        }
                        if (this == empire && _Galaxy.CurrentStarDate >= _Galaxy.StoryShakturiEnrageTimer)
                        {
                            _ = Math.Sqrt(_Galaxy.StarCount) / 5.0;
                            lock (_Galaxy.StoryLock)
                            {
                                if (!_Galaxy.StoryShakturiEnraged)
                                {
                                    _Galaxy.GenerateShakturiAggression(this);
                                }
                            }
                        }
                        DiplomaticRelation diplomaticRelation7 = empire.ObtainDiplomaticRelation(empire2);
                        if (diplomaticRelation == null || diplomaticRelation.Type != DiplomaticRelationType.War || diplomaticRelation7 == null || diplomaticRelation7.Type != DiplomaticRelationType.War || empire2.Reclusive)
                        {
                            if (flag2)
                            {
                                DiplomaticRelation diplomaticRelation8 = empire2.ObtainDiplomaticRelation(empire);
                                if (empire2.Reclusive && diplomaticRelation != null && diplomaticRelation.Type != 0 && diplomaticRelation8 != null && diplomaticRelation8.Type != 0 && _Galaxy.StoryShakturiEnraged)
                                {
                                    _Galaxy.StoryReturnOfTheShakturiEventLevel = 2;
                                    flag4 = true;
                                }
                            }
                            else if (flag && empire2 != null && empire2.Capital != null && !_Galaxy.PlayerEmpire.CheckLocationHintExistsAtPoint(empire2.Capital.Xpos, empire2.Capital.Ypos, 48000.0))
                            {
                                string text3 = TextResolver.GetText("We offer to reveal a secret location");
                                SendMessageToEmpire(_Galaxy.PlayerEmpire, EmpireMessageType.HistoryOfferLocationHint, null, text3);
                                return;
                            }
                            break;
                        }
                        goto case 3;
                    }
                case 3:
                    {
                        if (DominantRace != _Galaxy.ShakturiActualRace)
                        {
                            break;
                        }
                        DiplomaticRelation diplomaticRelation9 = empire.ObtainDiplomaticRelation(empire2);
                        if (((diplomaticRelation != null && diplomaticRelation.Type == DiplomaticRelationType.War) || empire2 == null || !empire2.Reclusive) && diplomaticRelation9 != null && diplomaticRelation9.Type == DiplomaticRelationType.War)
                        {
                            if (_Galaxy.StoryReturnOfTheShakturiEventLevel < 3)
                            {
                                _Galaxy.GenerateShakturiInvasion(empire, empire2);
                            }
                            _Galaxy.StoryReturnOfTheShakturiEventLevel = 3;
                            if (diplomaticRelation != null && diplomaticRelation.Type == DiplomaticRelationType.War)
                            {
                                flag4 = true;
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        if (!flag2)
                        {
                            break;
                        }
                        bool flag5 = false;
                        if (empire != null)
                        {
                            DiplomaticRelation diplomaticRelation5 = _Galaxy.PlayerEmpire.ObtainDiplomaticRelation(empire);
                            if (diplomaticRelation5 != null)
                            {
                                int builtObjectWarValue = 0;
                                int colonyWarValue = 0;
                                _Galaxy.CalculateEmpireWarValue(_Galaxy.PlayerEmpire, out builtObjectWarValue, out colonyWarValue);
                                int num9 = (int)((double)builtObjectWarValue * 0.6);
                                int num10 = (int)((double)colonyWarValue * 0.4);
                                if (diplomaticRelation5.WarDamageBuiltObject > num9 || diplomaticRelation5.WarDamageColony > num10)
                                {
                                    flag5 = true;
                                }
                            }
                        }
                        if (!flag5 && empire2 != null && empire != null)
                        {
                            DiplomaticRelation diplomaticRelation6 = empire2.ObtainDiplomaticRelation(empire);
                            if (diplomaticRelation6 != null)
                            {
                                int builtObjectWarValue2 = 0;
                                int colonyWarValue2 = 0;
                                _Galaxy.CalculateEmpireWarValue(_Galaxy.PlayerEmpire, out builtObjectWarValue2, out colonyWarValue2);
                                int num11 = (int)((double)builtObjectWarValue2 * 0.8);
                                if (diplomaticRelation6.WarDamageBuiltObject > num11)
                                {
                                    flag5 = true;
                                }
                            }
                        }
                        int num12 = _Galaxy.StarCount / 35;
                        if (empire != null && empire.Colonies != null && empire.Colonies.Count > num12 && flag5)
                        {
                            _Galaxy.StoryReturnOfTheShakturiEventLevel = 4;
                            flag4 = true;
                        }
                        break;
                    }
            }
            if (flag4)
            {
                SendMessageToEmpire(_Galaxy.PlayerEmpire, EmpireMessageType.StoryMessage, null, TextResolver.GetText("We have an important warning that you need to hear"));
            }
        }

        public void UpdateSystemFuelSourceStatus()
        {
            FuelSystemsUpdating = true;
            if (FuelSystemsSources == null || FuelSystemsSources.Count == 0)
            {
                FuelSystemsSources = new List<FuelSourceSystemList>();
                for (int i = 0; i < _Galaxy.ResourceSystem.FuelResources.Count; i++)
                {
                    ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.FuelResources[i];
                    if (resourceDefinition != null)
                    {
                        FuelSourceSystemList fuelSourceSystemList = new FuelSourceSystemList();
                        fuelSourceSystemList.ResourceId = resourceDefinition.ResourceID;
                        FuelSystemsSources.Add(fuelSourceSystemList);
                    }
                }
            }
            for (int j = 0; j < FuelSystemsSources.Count; j++)
            {
                FuelSystemsSources[j].Clear();
            }
            for (int k = 0; k < SystemVisibility.Count; k++)
            {
                SystemVisibility systemVisibility = SystemVisibility[k];
                if ((systemVisibility.Status != SystemVisibilityStatus.Explored && systemVisibility.Status != SystemVisibilityStatus.Visible) || systemVisibility.FuelSourcesFinalized)
                {
                    continue;
                }
                HabitatList[] array = new HabitatList[_Galaxy.ResourceSystem.FuelResources.Count];
                for (int l = 0; l < array.Length; l++)
                {
                    array[l] = new HabitatList();
                }
                if (systemVisibility.SystemStar.Category == HabitatCategoryType.GasCloud && (systemVisibility.TotallyExplored || (ResourceMap != null && ResourceMap.CheckResourcesKnown(systemVisibility.SystemStar))))
                {
                    for (int m = 0; m < _Galaxy.ResourceSystem.FuelResources.Count; m++)
                    {
                        int num = systemVisibility.SystemStar.Resources.IndexOf(_Galaxy.ResourceSystem.FuelResources[m].ResourceID, 0);
                        if (num >= 0 && !array[m].Contains(systemVisibility.SystemStar))
                        {
                            array[m].Add(systemVisibility.SystemStar);
                        }
                    }
                }
                for (int n = 0; n < _Galaxy.Systems[systemVisibility.SystemStar.SystemIndex].Habitats.Count; n++)
                {
                    Habitat habitat = _Galaxy.Systems[systemVisibility.SystemStar.SystemIndex].Habitats[n];
                    if ((habitat.Category != HabitatCategoryType.GasCloud && habitat.Type != HabitatType.GasGiant) || (!systemVisibility.TotallyExplored && (ResourceMap == null || !ResourceMap.CheckResourcesKnown(habitat))))
                    {
                        continue;
                    }
                    for (int num2 = 0; num2 < _Galaxy.ResourceSystem.FuelResources.Count; num2++)
                    {
                        int num3 = habitat.Resources.IndexOf(_Galaxy.ResourceSystem.FuelResources[num2].ResourceID, 0);
                        if (num3 >= 0 && !array[num2].Contains(habitat))
                        {
                            array[num2].Add(habitat);
                        }
                    }
                }
                for (int num4 = 0; num4 < _Galaxy.ResourceSystem.FuelResources.Count; num4++)
                {
                    FuelSystemsSources[num4].Add(new FuelSourceSystem(systemVisibility.SystemStar, array[num4]));
                }
                if (systemVisibility.TotallyExplored)
                {
                    systemVisibility.FuelSourcesFinalized = true;
                }
            }
            FuelSystemsUpdating = false;
        }

        private void UpdateSystemRefuellingStatus()
        {
            Resource fuelType = new Resource(_Galaxy.ResourceSystem.FuelResources[0].ResourceID);
            Design design = _Designs.FindNewestCanBuild(BuiltObjectSubRole.Frigate);
            if (design != null)
            {
                fuelType = design.FuelType;
            }
            BuiltObject testMilitaryShip = null;
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                if (BuiltObjects[i].Role == BuiltObjectRole.Military)
                {
                    testMilitaryShip = BuiltObjects[i];
                    break;
                }
            }
            for (int j = 0; j < SystemVisibility.Count; j++)
            {
                SystemVisibility systemVisibility = SystemVisibility[j];
                systemVisibility.IsRefuellingPoint = _Galaxy.IdentifyWhetherSystemIsRefuellingPointForEmpire(systemVisibility.SystemStar, this, fuelType, testMilitaryShip);
            }
        }

        private void UpdateSystemExplorationStatus()
        {
            int num = 0;
            for (int i = 0; i < SystemVisibility.Count; i++)
            {
                SystemVisibility systemVisibility = SystemVisibility[i];
                if (systemVisibility.Status == SystemVisibilityStatus.Explored || systemVisibility.Status == SystemVisibilityStatus.Visible)
                {
                    num++;
                }
                if (systemVisibility.TotallyExplored || (systemVisibility.Status != SystemVisibilityStatus.Visible && systemVisibility.Status != SystemVisibilityStatus.Explored))
                {
                    continue;
                }
                SystemInfo systemInfo = _Galaxy.Systems[systemVisibility.SystemStar.SystemIndex];
                bool flag = true;
                if (ResourceMap != null && !ResourceMap.CheckResourcesKnown(systemInfo.SystemStar))
                {
                    flag = false;
                }
                if (flag)
                {
                    for (int j = 0; j < systemInfo.Habitats.Count; j++)
                    {
                        Habitat habitat = systemInfo.Habitats[j];
                        if (ResourceMap != null && !ResourceMap.CheckResourcesKnown(habitat))
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                systemVisibility.TotallyExplored = flag;
            }
            _SystemExploredCount = num;
            _ExplorationShipCount = CountExplorationShips();
        }

        private void ClearExpiredViewableEmpires()
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            List<int> list = new List<int>();
            for (int i = 0; i < _EmpiresViewable.Count; i++)
            {
                if (_EmpiresViewableExpiry[i] < currentStarDate)
                {
                    list.Add(i);
                }
            }
            list.Sort();
            list.Reverse();
            foreach (int item in list)
            {
                _EmpiresViewable.RemoveAt(item);
                _EmpiresViewableExpiry.RemoveAt(item);
            }
        }

        private void CancelInactiveBlockades()
        {
            BlockadeList blockadesForEmpire = _Galaxy.Blockades.GetBlockadesForEmpire(this);
            foreach (Blockade item in blockadesForEmpire)
            {
                bool flag = false;
                for (int i = 0; i < BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = BuiltObjects[i];
                    if (builtObject.Mission == null || builtObject.Mission.Type != BuiltObjectMissionType.Blockade)
                    {
                        continue;
                    }
                    if (item.TargetIsColony)
                    {
                        if (builtObject.Mission.TargetHabitat != null && builtObject.Mission.TargetHabitat == item.Colony)
                        {
                            flag = true;
                            break;
                        }
                        continue;
                    }
                    if (builtObject.Mission.TargetBuiltObject != null && builtObject.Mission.TargetBuiltObject == item.BuiltObject)
                    {
                        flag = true;
                        break;
                    }
                    if (builtObject.Mission.TargetHabitat != null && item.BuiltObject.ParentHabitat != null && item.BuiltObject.ParentHabitat == builtObject.Mission.TargetHabitat)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    if (item.TargetIsColony)
                    {
                        CancelBlockade(item.Colony);
                    }
                    else
                    {
                        CancelBlockade(item.BuiltObject);
                    }
                }
            }
        }

        private void ClearOldDistressSignals()
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            long num = currentStarDate - Galaxy.DistressSignalDateRange;
            DistressSignalList distressSignalList = new DistressSignalList();
            for (int i = 0; i < _DistressSignals.Count; i++)
            {
                DistressSignal distressSignal = _DistressSignals[i];
                if (distressSignal.Date < num)
                {
                    distressSignalList.Add(distressSignal);
                }
            }
            foreach (DistressSignal item in distressSignalList)
            {
                _DistressSignals.Remove(item);
            }
        }

        private void PayForPlanetaryFacilities(double timePassed)
        {
            double num = AnnualFacilityMaintenance * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
            StateMoney -= num;
        }

        private void PayForTroops(double timePassed)
        {
            double num = AnnualTroopMaintenance * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
            StateMoney -= num;
            PirateEconomy.PerformExpense(num, PirateExpenseType.Undefined, _Galaxy.CurrentStarDate);
        }

        private void PayForAgents(double timePassed)
        {
            double num = AnnualAgentMaintenance * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
            StateMoney -= num;
        }

        public HabitatPrioritizationList PrioritizeEmpireResourceNeeds()
        {
            return PrioritizeEmpireResourceNeeds(includeLuxuryResources: false, 5, 1.0);
        }

        public HabitatPrioritizationList PrioritizeEmpireResourceNeeds(bool includeLuxuryResources, int topResourceCount, double minimumValue)
        {
            return PrioritizeEmpireResourceNeeds(includeLuxuryResources, topResourceCount, minimumValue, filterOutHabitatsWithMiningStationsUnderConstruction: true);
        }

        public HabitatPrioritizationList PrioritizeEmpireResourceNeeds(bool includeLuxuryResources, int topResourceCount, double minimumValue, bool filterOutHabitatsWithMiningStationsUnderConstruction)
        {
            return PrioritizeEmpireResourceNeeds(includeLuxuryResources, topResourceCount, minimumValue, filterOutHabitatsWithMiningStationsUnderConstruction: true, includeAsteroids: true);
        }

        public HabitatPrioritizationList PrioritizeEmpireResourceNeeds(bool includeLuxuryResources, int topResourceCount, double minimumValue, bool filterOutHabitatsWithMiningStationsUnderConstruction, bool includeAsteroids)
        {
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            ResourceList resourceList = IdentifyDeficientEmpireResources(includeLuxuryResources, minimumValue);
            bool flag = true;
            bool flag2 = true;
            if (Research != null && Research.ResearchedComponents != null && Research.ResearchedComponents.CountByCategory(ComponentCategoryType.HyperDrive) <= 0)
            {
                flag2 = false;
                topResourceCount = Math.Max(10, resourceList.Count);
            }
            HabitatList minedHabitats = new HabitatList();
            minedHabitats = DetermineHabitatsBeingMined(minedHabitats, BuiltObjects);
            minedHabitats = DetermineHabitatsBeingMined(minedHabitats, PrivateBuiltObjects);
            HabitatPrioritizationList habitatPrioritizationList2 = new HabitatPrioritizationList();
            if (includeLuxuryResources)
            {
                habitatPrioritizationList2 = DetermineHabitatsBuildingMiningStations();
            }
            for (int i = 0; i < SystemVisibility.Count; i++)
            {
                if (!CheckSystemExplored(i))
                {
                    continue;
                }
                BuiltObject builtObject = _Galaxy.FastFindNearestSpacePort(SystemVisibility[i].SystemStar.Xpos, SystemVisibility[i].SystemStar.Ypos, this, false);
                for (int j = 0; j < _Galaxy.Systems[SystemVisibility[i].SystemStar.SystemIndex].Habitats.Count; j++)
                {
                    Habitat habitat = _Galaxy.Systems[SystemVisibility[i].SystemStar.SystemIndex].Habitats[j];
                    if (habitat.Resources.Count <= 0)
                    {
                        continue;
                    }
                    double num = 0.0;
                    if (!_ResourceMap.CheckResourcesKnown(habitat) || (!includeAsteroids && habitat.Category == HabitatCategoryType.Asteroid) || (habitat.Owner != null && habitat.Owner != _Galaxy.IndependentEmpire))
                    {
                        continue;
                    }
                    bool flag3 = true;
                    BuiltObject assignedShip = null;
                    int num2 = habitatPrioritizationList2.IndexOf(habitat);
                    if (num2 >= 0)
                    {
                        if (filterOutHabitatsWithMiningStationsUnderConstruction)
                        {
                            flag3 = false;
                        }
                        else
                        {
                            assignedShip = habitatPrioritizationList2[num2].AssignedShip;
                        }
                    }
                    if (flag3)
                    {
                        if (PirateEmpireBaseHabitat == null)
                        {
                            flag3 = _Galaxy.CheckEmpireTerritoryCanBuildAtHabitat(this, habitat);
                        }
                        else
                        {
                            SystemInfo bySystemIndex = _Galaxy.Systems.GetBySystemIndex(habitat.SystemIndex);
                            if (bySystemIndex != null && bySystemIndex.DominantEmpire != null && bySystemIndex.DominantEmpire.Empire != null && bySystemIndex.DominantEmpire.Empire != this)
                            {
                                flag3 = false;
                            }
                        }
                    }
                    if (!flag3)
                    {
                        continue;
                    }
                    Habitat habitat2 = habitat;
                    for (int k = 0; k < topResourceCount && k < resourceList.Count; k++)
                    {
                        int num3 = habitat2.Resources.IndexOf(resourceList[k].ResourceID, 0);
                        if (num3 < 0)
                        {
                            continue;
                        }
                        if (resourceList[k].IsLuxuryResource)
                        {
                            if (flag)
                            {
                                num += resourceList[k].SortTag * 1000.0;
                            }
                        }
                        else
                        {
                            num += resourceList[k].SortTag * 1000.0;
                        }
                    }
                    if (!flag2)
                    {
                        if (builtObject != null)
                        {
                            num /= Math.Sqrt(_Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, habitat2.Xpos, habitat2.Ypos));
                            num *= 100.0;
                        }
                        else if (Capital != null)
                        {
                            num /= Math.Sqrt(_Galaxy.CalculateDistance(Capital.Xpos, Capital.Ypos, habitat2.Xpos, habitat2.Ypos));
                            num *= 100.0;
                        }
                    }
                    else if (builtObject != null)
                    {
                        num /= Math.Sqrt(Math.Sqrt(_Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, habitat2.Xpos, habitat2.Ypos)));
                    }
                    else if (Capital != null)
                    {
                        num /= Math.Sqrt(Math.Sqrt(_Galaxy.CalculateDistance(Capital.Xpos, Capital.Ypos, habitat2.Xpos, habitat2.Ypos)));
                    }
                    if (num > 1.0)
                    {
                        HabitatPrioritization habitatPrioritization = new HabitatPrioritization(habitat2, (int)num);
                        habitatPrioritization.AssignedShip = assignedShip;
                        habitatPrioritizationList.Add(habitatPrioritization);
                    }
                }
            }
            HabitatPrioritizationList habitatPrioritizationList3 = new HabitatPrioritizationList();
            foreach (HabitatPrioritization item in habitatPrioritizationList)
            {
                if (minedHabitats.Contains(item.Habitat))
                {
                    habitatPrioritizationList3.Add(item);
                }
            }
            foreach (HabitatPrioritization item2 in habitatPrioritizationList3)
            {
                habitatPrioritizationList.Remove(item2);
            }
            habitatPrioritizationList.Sort();
            habitatPrioritizationList.Reverse();
            return habitatPrioritizationList;
        }

        public HabitatPrioritizationList IdentifyDesiredEnemyMiningStations(int topResourceCount, bool excludeRecentRaids, double valueThreshold)
        {
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            ResourceList resourceList = IdentifyDeficientEmpireResources(includeLuxuryResources: false, 0.0);
            for (int i = 0; i < SystemVisibility.Count; i++)
            {
                if (!CheckSystemExplored(i))
                {
                    continue;
                }
                BuiltObject builtObject = _Galaxy.FastFindNearestSpacePort(SystemVisibility[i].SystemStar.Xpos, SystemVisibility[i].SystemStar.Ypos, this, false);
                for (int j = 0; j < _Galaxy.Systems[SystemVisibility[i].SystemStar.SystemIndex].Habitats.Count; j++)
                {
                    Habitat habitat = _Galaxy.Systems[SystemVisibility[i].SystemStar.SystemIndex].Habitats[j];
                    if (habitat.Resources.Count <= 0 || !_ResourceMap.CheckResourcesKnown(habitat))
                    {
                        continue;
                    }
                    BuiltObject builtObject2 = _Galaxy.DetermineMiningStationAtHabitat(habitat);
                    if (builtObject2 == null)
                    {
                        continue;
                    }
                    Empire actualEmpire = builtObject2.ActualEmpire;
                    if (actualEmpire == this)
                    {
                        continue;
                    }
                    bool flag = true;
                    if (this == _Galaxy.PlayerEmpire)
                    {
                        flag = IsObjectVisibleToThisEmpire(builtObject2, includeLongRangeScanners: true, includeShipsOutsideSystems: false);
                    }
                    if (!flag)
                    {
                        continue;
                    }
                    bool flag2 = true;
                    if (PirateEmpireBaseHabitat == null && actualEmpire.PirateEmpireBaseHabitat == null)
                    {
                        DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(actualEmpire);
                        if (diplomaticRelation.Strategy == DiplomaticStrategy.Ally || diplomaticRelation.Strategy == DiplomaticStrategy.Befriend || diplomaticRelation.Strategy == DiplomaticStrategy.DefendPlacate || diplomaticRelation.Strategy == DiplomaticStrategy.Placate || diplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement || diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || diplomaticRelation.Type == DiplomaticRelationType.Protectorate)
                        {
                            flag2 = false;
                        }
                    }
                    else if (actualEmpire != null)
                    {
                        PirateRelation pirateRelation = ObtainPirateRelation(actualEmpire);
                        if (pirateRelation.Type == PirateRelationType.Protection)
                        {
                            flag2 = false;
                        }
                    }
                    if (!flag2 || (excludeRecentRaids && builtObject2.RaidCountdown > 0))
                    {
                        continue;
                    }
                    double num = 0.0;
                    for (int k = 0; k < Math.Min(topResourceCount, resourceList.Count); k++)
                    {
                        if (resourceList[k].SortTag > 0.1 && habitat.Resources.ContainsId(resourceList[k].ResourceID))
                        {
                            num += 1000.0;
                        }
                    }
                    if (num > 0.0)
                    {
                        if (builtObject != null)
                        {
                            num /= Math.Sqrt(Math.Sqrt(_Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, habitat.Xpos, habitat.Ypos)));
                        }
                        else if (Capital != null)
                        {
                            num /= Math.Sqrt(Math.Sqrt(_Galaxy.CalculateDistance(Capital.Xpos, Capital.Ypos, habitat.Xpos, habitat.Ypos)));
                        }
                        if (num >= valueThreshold)
                        {
                            num = Math.Max(1.0, num);
                            HabitatPrioritization item = new HabitatPrioritization(habitat, (int)num);
                            habitatPrioritizationList.Add(item);
                        }
                    }
                }
            }
            habitatPrioritizationList.Sort();
            habitatPrioritizationList.Reverse();
            return habitatPrioritizationList;
        }

        public HabitatPrioritizationList IdentifyRaidableColonies(int maximumDefenseStrength, double valueThreshold)
        {
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            for (int i = 0; i < SystemVisibility.Count; i++)
            {
                if (!CheckSystemExplored(i) || _Galaxy.Systems == null || _Galaxy.Systems.Count <= i)
                {
                    continue;
                }
                SystemInfo systemInfo = _Galaxy.Systems[i];
                if (systemInfo == null || ((systemInfo.DominantEmpire == null || systemInfo.DominantEmpire.Empire == null) && systemInfo.IndependentColonyCount <= 0))
                {
                    continue;
                }
                HabitatList habitats = _Galaxy.Systems[i].Habitats;
                if (habitats == null)
                {
                    continue;
                }
                for (int j = 0; j < habitats.Count; j++)
                {
                    Habitat habitat = habitats[j];
                    if (habitat == null || habitat.Population == null || habitat.Population.Count <= 0)
                    {
                        continue;
                    }
                    Empire empire = habitat.Empire;
                    if (empire == null || empire == this || habitat.RaidCountdown > 0)
                    {
                        continue;
                    }
                    bool flag = true;
                    PirateColonyControl byFaction = habitat.GetPirateControl().GetByFaction(this);
                    if (byFaction != null && (byFaction.HasFacilityControl || byFaction.ControlLevel >= 0.5f))
                    {
                        flag = false;
                    }
                    if (!flag)
                    {
                        continue;
                    }
                    int num = 0;
                    if (CheckSystemVisible(i))
                    {
                        if (habitat.Troops != null)
                        {
                            num = habitat.Troops.TotalDefendStrength;
                        }
                    }
                    else
                    {
                        num = habitat.EstimatedDefensiveForceRequired(atWar: false);
                    }
                    if (num > maximumDefenseStrength)
                    {
                        continue;
                    }
                    BuiltObject builtObject = _Galaxy.FastFindNearestSpacePort(habitat.Xpos, habitat.Ypos, this, true);
                    bool flag2 = true;
                    if (PirateEmpireBaseHabitat == null && empire.PirateEmpireBaseHabitat == null)
                    {
                        DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                        if (diplomaticRelation.Strategy == DiplomaticStrategy.Ally || diplomaticRelation.Strategy == DiplomaticStrategy.Befriend || diplomaticRelation.Strategy == DiplomaticStrategy.DefendPlacate || diplomaticRelation.Strategy == DiplomaticStrategy.Placate || diplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement || diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || diplomaticRelation.Type == DiplomaticRelationType.Protectorate)
                        {
                            flag2 = false;
                        }
                    }
                    else
                    {
                        PirateRelation pirateRelation = ObtainPirateRelation(habitat.Empire);
                        if (pirateRelation.Type == PirateRelationType.Protection)
                        {
                            flag2 = false;
                        }
                    }
                    if (!flag2)
                    {
                        continue;
                    }
                    double num2 = 0.0;
                    if (habitat.Population != null)
                    {
                        num2 = (double)habitat.Population.TotalAmount / 1000.0;
                    }
                    if (num2 > 0.0)
                    {
                        if (builtObject != null)
                        {
                            num2 /= Math.Sqrt(Math.Sqrt(_Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, habitat.Xpos, habitat.Ypos)));
                        }
                        else if (Capital != null)
                        {
                            num2 /= Math.Sqrt(Math.Sqrt(_Galaxy.CalculateDistance(Capital.Xpos, Capital.Ypos, habitat.Xpos, habitat.Ypos)));
                        }
                        if (num2 >= valueThreshold)
                        {
                            num2 = Math.Max(1.0, num2);
                            HabitatPrioritization item = new HabitatPrioritization(habitat, (int)num2);
                            habitatPrioritizationList.Add(item);
                        }
                    }
                }
            }
            habitatPrioritizationList.Sort();
            habitatPrioritizationList.Reverse();
            return habitatPrioritizationList;
        }

        private double CalculateAggressionFactor()
        {
            double num = (double)DominantRace.AggressionLevel / 100.0;
            num *= num;
            return num * _Galaxy.AggressionLevel;
        }

        public double CalculateCautionFactor()
        {
            double num = (double)DominantRace.CautionLevel / 100.0;
            num *= num;
            return Math.Max(1.0, num);
        }

        private bool ShouldProvoke(Empire targetEmpire, int ourMilitaryPotency, double aggressionFactor, double cautionFactor, double galaxyIntoleranceLevel, bool provokeWithRaids)
        {
            bool flag = false;
            if (targetEmpire.Reclusive || Reclusive)
            {
                return false;
            }
            int val = Math.Max(1, (int)aggressionFactor);
            val = Math.Min(val, 2);
            int num = CountEmpiresWeDeclaredWarOn() + CountEmpiresWhoDeclaredWarOnUs();
            if (num >= 1)
            {
                return false;
            }
            EmpireList empireList = ResolveEmpiresAtWarWithOrPreparingToConquer();
            if (empireList.Count > 1 && !empireList.Contains(targetEmpire))
            {
                return false;
            }
            if (!CheckWhetherHaveEnoughFleetsForWar(num))
            {
                return false;
            }
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(targetEmpire);
            switch (diplomaticRelation.Strategy)
            {
                case DiplomaticStrategy.Undefined:
                case DiplomaticStrategy.Befriend:
                case DiplomaticStrategy.Placate:
                case DiplomaticStrategy.Defend:
                case DiplomaticStrategy.Ally:
                case DiplomaticStrategy.Undermine:
                case DiplomaticStrategy.DefendPlacate:
                case DiplomaticStrategy.DefendUndermine:
                    return false;
                default:
                    {
                        EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(targetEmpire);
                        double num2 = -45.0 / aggressionFactor;
                        num2 *= cautionFactor;
                        if ((double)empireEvaluation.OverallAttitude <= num2)
                        {
                            flag = true;
                        }
                        if (flag)
                        {
                            int num3 = (int)((double)ourMilitaryPotency * aggressionFactor);
                            num3 = (int)((double)num3 / cautionFactor);
                            double num4 = 0.95 + Galaxy.Rnd.NextDouble() * (1.4 * galaxyIntoleranceLevel * aggressionFactor);
                            num3 = (int)((double)num3 * num4);
                            num3 = (int)((double)num3 * (_Galaxy.AggressionLevel * _Galaxy.AggressionLevel));
                            if (num3 >= targetEmpire.WeightedMilitaryPotency)
                            {
                                double num5 = ((double)DominantRace.LoyaltyLevel / 100.0 + (Galaxy.Rnd.NextDouble() - 0.5) * 0.2) * 100.0;
                                DiplomaticRelation diplomaticRelation2 = DiplomaticRelations[targetEmpire];
                                if (diplomaticRelation2 != null)
                                {
                                    switch (diplomaticRelation2.Type)
                                    {
                                        case DiplomaticRelationType.None:
                                            if (Galaxy.Rnd.Next(0, 4) > 0)
                                            {
                                                return true;
                                            }
                                            break;
                                        case DiplomaticRelationType.TradeSanctions:
                                        case DiplomaticRelationType.War:
                                            return true;
                                        case DiplomaticRelationType.FreeTradeAgreement:
                                            if (num5 < 100.0)
                                            {
                                                return true;
                                            }
                                            break;
                                        case DiplomaticRelationType.MutualDefensePact:
                                        case DiplomaticRelationType.Protectorate:
                                            if (num5 < 95.0)
                                            {
                                                return true;
                                            }
                                            break;
                                        case DiplomaticRelationType.SubjugatedDominion:
                                        case DiplomaticRelationType.Truce:
                                            if (num5 < 110.0)
                                            {
                                                return true;
                                            }
                                            break;
                                    }
                                }
                                else if (Galaxy.Rnd.Next(0, 4) > 0)
                                {
                                    return true;
                                }
                            }
                        }
                        return false;
                    }
            }
        }

        private bool DetermineAttackOnSingleEmpire(Empire empire, int ourMilitaryPotency, double aggression, double caution, double galaxyIntoleranceLevel, ref int refusalCount)
        {
            bool result = false;
            if (Policy.WarAttacksHarassEnemies && ShouldProvoke(empire, ourMilitaryPotency, aggression, caution, galaxyIntoleranceLevel, provokeWithRaids: true) && !_EmpiresToAttack.Contains(empire))
            {
                bool flag = false;
                for (int i = 0; i < empire.ProposedDiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = empire.ProposedDiplomaticRelations[i];
                    if (diplomaticRelation.Initiator == this)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageRaid(empire), empire, AdvisorMessageType.PrepareRaid))
                {
                    _EmpiresToAttack.Add(empire);
                    result = true;
                }
            }
            return result;
        }

        private void DetermineRandomAttacks()
        {
            IdentifyDesiredForeignColonies();
            int refusalCount = 0;
            int weightedMilitaryPotency = WeightedMilitaryPotency;
            int num = 0;
            double aggression = CalculateAggressionFactor();
            double caution = CalculateCautionFactor();
            double intoleranceLevel = _Galaxy.IntoleranceLevel;
            int num2 = Galaxy.Rnd.Next(0, _EmpiresWithDesiredColonies.Count);
            for (int i = num2; i < _EmpiresWithDesiredColonies.Count; i++)
            {
                if (num >= 1)
                {
                    break;
                }
                DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(_EmpiresWithDesiredColonies[i]);
                if (diplomaticRelation.Type != DiplomaticRelationType.War)
                {
                    bool flag = false;
                    DiplomaticStrategy strategy = diplomaticRelation.Strategy;
                    if (strategy == DiplomaticStrategy.Conquer || strategy == DiplomaticStrategy.Punish)
                    {
                        flag = true;
                    }
                    if (flag && DetermineAttackOnSingleEmpire(_EmpiresWithDesiredColonies[i], weightedMilitaryPotency, aggression, caution, intoleranceLevel, ref refusalCount))
                    {
                        num++;
                    }
                }
            }
            for (int j = 0; j < num2; j++)
            {
                if (num >= 1)
                {
                    break;
                }
                DiplomaticRelation diplomaticRelation2 = ObtainDiplomaticRelation(_EmpiresWithDesiredColonies[j]);
                if (diplomaticRelation2.Type != DiplomaticRelationType.War)
                {
                    bool flag2 = false;
                    DiplomaticStrategy strategy2 = diplomaticRelation2.Strategy;
                    if (strategy2 == DiplomaticStrategy.Conquer || strategy2 == DiplomaticStrategy.Punish)
                    {
                        flag2 = true;
                    }
                    if (flag2 && DetermineAttackOnSingleEmpire(_EmpiresWithDesiredColonies[j], weightedMilitaryPotency, aggression, caution, intoleranceLevel, ref refusalCount))
                    {
                        num++;
                    }
                }
            }
        }

        private void IdentifyDesiredForeignColonies()
        {
            _DesiredForeignColonies.Clear();
            _EmpiresWithDesiredColonies.Clear();
            ResourceList resourceList = IdentifyDeficientEmpireResources();
            double num = Galaxy.SizeX;
            double num2 = 0.0;
            double num3 = Galaxy.SizeY;
            double num4 = 0.0;
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat != null)
                {
                    if (habitat.Xpos < num)
                    {
                        num = habitat.Xpos;
                    }
                    if (habitat.Xpos > num2)
                    {
                        num2 = habitat.Xpos;
                    }
                    if (habitat.Ypos < num3)
                    {
                        num3 = habitat.Ypos;
                    }
                    if (habitat.Ypos > num4)
                    {
                        num4 = habitat.Ypos;
                    }
                }
            }
            num -= (double)Galaxy.IndexSize;
            num2 += (double)Galaxy.IndexSize;
            num3 -= (double)Galaxy.IndexSize;
            num4 += (double)Galaxy.IndexSize;
            double num5 = CalculateAggressionFactor();
            CalculateCautionFactor();
            double intoleranceLevel = _Galaxy.IntoleranceLevel;
            for (int j = 0; j < _Galaxy.Empires.Count; j++)
            {
                if (_Galaxy.Empires[j] == this)
                {
                    continue;
                }
                EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(_Galaxy.Empires[j]);
                double num6 = 1.0 + Math.Max(0.0, empireEvaluation.Bias / -10.0);
                for (int k = 0; k < _Galaxy.Empires[j].Colonies.Count; k++)
                {
                    if (_Galaxy.Empires[j].Colonies[k] == null || !(_Galaxy.Empires[j].Colonies[k].Xpos > num) || !(_Galaxy.Empires[j].Colonies[k].Xpos < num2) || !(_Galaxy.Empires[j].Colonies[k].Ypos > num3) || !(_Galaxy.Empires[j].Colonies[k].Ypos < num4))
                    {
                        continue;
                    }
                    double num7 = 0.0;
                    Habitat habitat2 = _Galaxy.Empires[j].Colonies[k];
                    if (!CheckSystemExplored(habitat2.SystemIndex))
                    {
                        continue;
                    }
                    Habitat habitat3 = habitat2;
                    double d = habitat3.StrategicValue;
                    d = Math.Sqrt(d);
                    d *= 220.0;
                    if (_ResourceMap.CheckResourcesKnown(habitat2))
                    {
                        for (int l = 0; l < 5 && l < resourceList.Count; l++)
                        {
                            int num8 = habitat3.Resources.IndexOf(resourceList[l].ResourceID, 0);
                            if (num8 >= 0)
                            {
                                num7 += resourceList[l].SortTag * 10000.0;
                            }
                        }
                    }
                    num7 = Math.Min(num7, 220000.0);
                    if (habitat2.Ruin != null && (habitat2.Ruin.BonusDefensive > 0.0 || habitat2.Ruin.BonusDiplomacy > 0.0 || habitat2.Ruin.BonusHappiness > 0.0 || habitat2.Ruin.BonusResearchEnergy > 0.0 || habitat2.Ruin.BonusResearchHighTech > 0.0 || habitat2.Ruin.BonusResearchWeapons > 0.0 || habitat2.Ruin.BonusWealth > 0.0))
                    {
                        d *= 10.0;
                    }
                    Habitat habitat4 = _Galaxy.FastFindNearestColony((int)habitat3.Xpos, (int)habitat3.Ypos, this, Galaxy.MajorColonyStrategicThreshhold);
                    if (habitat4 != null)
                    {
                        double num9 = Math.Sqrt(_Galaxy.CalculateDistance(habitat4.Xpos, habitat4.Ypos, habitat3.Xpos, habitat3.Ypos));
                        num9 /= num6;
                        num9 /= num5;
                        num9 /= 1.0 + 3.0 * intoleranceLevel;
                        d /= num9;
                        num7 /= num9;
                    }
                    else
                    {
                        double num10 = Math.Sqrt(_Galaxy.CalculateDistance(Capital.Xpos, Capital.Ypos, habitat3.Xpos, habitat3.Ypos));
                        num10 /= num6;
                        num10 /= num5;
                        num10 /= 1.0 + 3.0 * intoleranceLevel;
                        d /= num10;
                        num7 /= num10;
                    }
                    d *= _Galaxy.AggressionLevel;
                    num7 *= _Galaxy.AggressionLevel;
                    d = d * 0.3 + d * 0.7 * intoleranceLevel;
                    num7 = num7 * 0.3 + num7 * 0.7 * intoleranceLevel;
                    d = Math.Min(d, 1000.0);
                    num7 = Math.Min(num7, 1000.0);
                    if (d > (double)Galaxy.DesiredForeignColonyStrategicThreshhold)
                    {
                        HabitatPrioritization item = new HabitatPrioritization(habitat3, (int)d);
                        _DesiredForeignColonies.Add(item);
                    }
                    else if (num7 > (double)Galaxy.DesiredForeignColonyResourceThreshhold)
                    {
                        HabitatPrioritization item2 = new HabitatPrioritization(habitat3, (int)num7);
                        _DesiredForeignColonies.Add(item2);
                    }
                }
            }
            _DesiredForeignColonies.Sort();
            _DesiredForeignColonies.Reverse();
            for (int m = 0; m < _DesiredForeignColonies.Count; m++)
            {
                HabitatPrioritization habitatPrioritization = _DesiredForeignColonies[m];
                Empire owner = habitatPrioritization.Habitat.Owner;
                if (owner != null && !_EmpiresWithDesiredColonies.Contains(owner))
                {
                    _EmpiresWithDesiredColonies.Add(owner);
                }
            }
        }

    }
}
