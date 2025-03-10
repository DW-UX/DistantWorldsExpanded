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
        private double CalculateRetrofitCost(BuiltObject builtObject)
        {
            Design design = Designs.FindNewestCanBuild(builtObject.SubRole, builtObject.ParentHabitat);
            ComponentList componentsToProcure = null;
            double cost = 0.0;
            DetermineRetrofitAffordability(builtObject, design, out cost, out componentsToProcure);
            return cost;
        }

        public bool AssignRetrofitMission(BuiltObject builtObject)
        {
            Design design = Designs.FindNewestCanBuild(builtObject.SubRole, builtObject.ParentHabitat);
            return AssignRetrofitMission(builtObject, design);
        }

        public bool AssignRetrofitMission(BuiltObject builtObject, Design design)
        {
            return AssignRetrofitMission(builtObject, design, null);
        }

        public bool DetermineRetrofitAffordability(BuiltObject builtObject, Design design, out double cost, out ComponentList componentsToProcure)
        {
            cost = 0.0;
            componentsToProcure = null;
            if (design != null && builtObject != null && builtObject.Design != design)
            {
                componentsToProcure = builtObject.Components.ResolveComponentList().Diff(design.Components);
                foreach (Component item in componentsToProcure)
                {
                    cost += _Galaxy.ComponentCurrentPrices[item.ComponentID];
                }
                if (PirateEmpireBaseHabitat != null)
                {
                    cost *= Galaxy.ShipMarkupFactorPirates;
                }
                else
                {
                    cost *= Galaxy.ShipMarkupFactor;
                }
                if (builtObject.Owner == null)
                {
                    if (cost > builtObject.Empire.GetPrivateFunds())
                    {
                        return false;
                    }
                }
                else if (cost > builtObject.Owner.StateMoney)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public double CalculateRetrofitCost(BuiltObjectList builtObjects, Design design)
        {
            double num = 0.0;
            if (design != null)
            {
                List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
                list.AddRange(new BuiltObjectSubRole[3]
                {
                BuiltObjectSubRole.SmallSpacePort,
                BuiltObjectSubRole.MediumSpacePort,
                BuiltObjectSubRole.LargeSpacePort
                });
                for (int i = 0; i < builtObjects.Count; i++)
                {
                    BuiltObject builtObject = builtObjects[i];
                    double num2 = 0.0;
                    if (design != builtObject.Design && (design.SubRole == builtObject.SubRole || (list.Contains(builtObject.SubRole) && list.Contains(design.SubRole))))
                    {
                        ComponentList componentList = builtObject.Components.ResolveComponentList().Diff(design.Components);
                        foreach (Component item in componentList)
                        {
                            num2 += _Galaxy.ComponentCurrentPrices[item.ComponentID];
                        }
                        num2 = ((PirateEmpireBaseHabitat == null) ? (num2 * Galaxy.ShipMarkupFactor) : (num2 * Galaxy.ShipMarkupFactorPirates));
                    }
                    num += num2;
                }
            }
            else
            {
                for (int j = 0; j < builtObjects.Count; j++)
                {
                    BuiltObject builtObject2 = builtObjects[j];
                    design = Designs.FindNewestCanBuildFullEvaluate(builtObject2.SubRole);
                    double num3 = 0.0;
                    if (design != null && design != builtObject2.Design)
                    {
                        ComponentList componentList2 = builtObject2.Components.ResolveComponentList().Diff(design.Components);
                        foreach (Component item2 in componentList2)
                        {
                            num3 += _Galaxy.ComponentCurrentPrices[item2.ComponentID];
                        }
                        num3 = ((PirateEmpireBaseHabitat == null) ? (num3 * Galaxy.ShipMarkupFactor) : (num3 * Galaxy.ShipMarkupFactorPirates));
                    }
                    num += num3;
                }
            }
            return num;
        }

        public bool AssignRetrofitMission(BuiltObject builtObject, Design design, StellarObject location)
        {
            return AssignRetrofitMission(builtObject, design, location, forceUseOfYard: false);
        }

        public bool AssignRetrofitMission(BuiltObject builtObject, Design design, StellarObject location, bool forceUseOfYard)
        {
            //IL_0186: Expected O, but got F8
            builtObject.RetrofitForNextMission = false;
            if (builtObject.Empire == null || (builtObject.Empire == _Galaxy.IndependentEmpire && builtObject.PirateEmpireId == 0))
            {
                return false;
            }
            if (builtObject.Role != BuiltObjectRole.Base && builtObject.TopSpeed <= 0)
            {
                return false;
            }
            if (builtObject.BuiltAt != null)
            {
                return false;
            }
            CargoList resourcesToOrder = null;
            if (design != null && builtObject.Design != design)
            {
                double cost = 0.0;
                ComponentList componentsToProcure = null;
                if (DetermineRetrofitAffordability(builtObject, design, out cost, out componentsToProcure))
                {
                    if (builtObject.Role == BuiltObjectRole.Base)
                    {
                        if (builtObject.ParentHabitat == null || builtObject.ParentHabitat.Empire != builtObject.Empire)
                        {
                            if (builtObject.RetrofitBaseManufacturingQueue == null)
                            {
                                ManufacturingQueue manufacturingQueue = new ManufacturingQueue(builtObject, _Galaxy);
                                manufacturingQueue.Redefine(builtObject, forceSingleManufacturerOfEachType: true);
                                builtObject.RetrofitBaseManufacturingQueue = manufacturingQueue;
                            }
                            if (builtObject.RetrofitBaseConstructionQueue == null)
                            {
                                ConstructionQueue constructionQueue = new ConstructionQueue(builtObject, _Galaxy);
                                constructionQueue.Redefine(builtObject, forceSingleConstructionYard: true);
                                builtObject.RetrofitBaseConstructionQueue = constructionQueue;
                            }
                            if (builtObject.Empire != null && builtObject.Empire.PirateEmpireBaseHabitat != null)
                            {
                                builtObject.Empire.StateMoney -= cost;
                                builtObject.Empire.PirateEconomy.PerformExpense(cost, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                            }
                            else if (builtObject.Owner == null)
                            {
                                builtObject.Empire.PerformPrivateTransaction(0.0 - cost);
                                builtObject.Empire.StateMoney += BaconBuiltObject.PrivateSectorBuildOrRefitInvestInInfrastructure(builtObject, cost);
                            }
                            else
                            {
                                builtObject.Empire.StateMoney -= cost;
                                builtObject.Empire.PirateEconomy.PerformExpense(cost, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                            }
                            builtObject.PurchasePrice = cost;
                            ProcureConstructionComponents(builtObject, builtObject, orderPreciseResourceAmounts: true, out resourcesToOrder, componentsToProcure, forBaseRetrofit: true);
                            foreach (Cargo item in resourcesToOrder)
                            {
                                CreateOrder(builtObject, item.CommodityResource, item.Amount, isState: false, OrderType.RetrofitResourcesForBase);
                            }
                            if (builtObject.RetrofitBaseConstructionQueue != null && builtObject.RetrofitBaseConstructionQueue.AddBuiltObjectToRetrofit(builtObject, design))
                            {
                                design.BuildCount++;
                            }
                            return true;
                        }
                        Habitat parentHabitat = builtObject.ParentHabitat;
                        bool flag = false;
                        if (parentHabitat.ConstructionQueue != null && parentHabitat.ConstructionQueue.ConstructionYards != null)
                        {
                            for (int i = 0; i < parentHabitat.ConstructionQueue.ConstructionYards.Count; i++)
                            {
                                if (parentHabitat.ConstructionQueue.ConstructionYards[i].ShipUnderConstruction == null)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        }
                        if (flag || forceUseOfYard)
                        {
                            if (builtObject.Empire != null && builtObject.Empire.PirateEmpireBaseHabitat != null)
                            {
                                builtObject.Empire.StateMoney -= cost;
                                builtObject.Empire.PirateEconomy.PerformExpense(cost, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                            }
                            else if (builtObject.Owner == null)
                            {
                                builtObject.Empire.PerformPrivateTransaction(0.0 - cost);
                                builtObject.Empire.StateMoney += BaconBuiltObject.PrivateSectorBuildOrRefitInvestInInfrastructure(builtObject, cost);
                            }
                            else
                            {
                                builtObject.Empire.StateMoney -= cost;
                                builtObject.Empire.PirateEconomy.PerformExpense(cost, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                            }
                            builtObject.PurchasePrice = cost;
                            ProcureConstructionComponents(builtObject, parentHabitat, out resourcesToOrder, componentsToProcure);
                            foreach (Cargo item2 in resourcesToOrder)
                            {
                                CreateOrder(parentHabitat, item2.CommodityResource, item2.Amount, isState: false, OrderType.ConstructionShortage);
                            }
                            if (builtObject.ParentHabitat.ConstructionQueue != null && builtObject.ParentHabitat.ConstructionQueue.AddBuiltObjectToRetrofit(builtObject, design))
                            {
                                design.BuildCount++;
                            }
                            return true;
                        }
                    }
                    else if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip || builtObject.SubRole == BuiltObjectSubRole.ConstructionShip || builtObject.SubRole == BuiltObjectSubRole.ResupplyShip)
                    {
                        StellarObject stellarObject = null;
                        if (location is Habitat)
                        {
                            stellarObject = (Habitat)location;
                        }
                        else if (location is BuiltObject && PirateEmpireBaseHabitat != null)
                        {
                            stellarObject = location;
                        }
                        if (stellarObject == null)
                        {
                            stellarObject = Colonies.FindShortestConstructionWaitQueueCloseToBuiltObject(builtObject, out var shortestWaitQueueTime);
                            double num = shortestWaitQueueTime / (double)Galaxy.RealSecondsInGalacticYear;
                            num /= 2000.0;
                            if (num >= Galaxy.MaximumConstructionQueueWaitTimeYears && !forceUseOfYard)
                            {
                                stellarObject = null;
                            }
                        }
                        if (stellarObject != null)
                        {
                            bool flag2 = false;
                            if (stellarObject.ConstructionQueue != null && stellarObject.ConstructionQueue.ConstructionYards != null)
                            {
                                for (int j = 0; j < stellarObject.ConstructionQueue.ConstructionYards.Count; j++)
                                {
                                    if (stellarObject.ConstructionQueue.ConstructionYards[j].ShipUnderConstruction == null)
                                    {
                                        flag2 = true;
                                        break;
                                    }
                                }
                            }
                            if (flag2 || forceUseOfYard)
                            {
                                if (builtObject.Empire != null && builtObject.Empire.PirateEmpireBaseHabitat != null)
                                {
                                    builtObject.Empire.StateMoney -= cost;
                                    builtObject.Empire.PirateEconomy.PerformExpense(cost, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                                }
                                else if (builtObject.Owner == null)
                                {
                                    builtObject.Empire.PerformPrivateTransaction(0.0 - cost);
                                    builtObject.Empire.StateMoney += BaconBuiltObject.PrivateSectorBuildOrRefitInvestInInfrastructure(builtObject, cost);
                                }
                                else
                                {
                                    builtObject.Empire.StateMoney -= cost;
                                    builtObject.Empire.PirateEconomy.PerformExpense(cost, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                                }
                                builtObject.PurchasePrice = cost;
                                if (stellarObject is Habitat)
                                {
                                    Habitat habitat = (Habitat)stellarObject;
                                    ProcureConstructionComponents(builtObject, habitat, out resourcesToOrder, componentsToProcure);
                                    foreach (Cargo item3 in resourcesToOrder)
                                    {
                                        CreateOrder(habitat, item3.CommodityResource, item3.Amount, isState: false, OrderType.ConstructionShortage);
                                    }
                                }
                                else if (stellarObject is BuiltObject)
                                {
                                    BuiltObject builtObject2 = (BuiltObject)stellarObject;
                                    ProcureConstructionComponents(builtObject, builtObject2, orderPreciseResourceAmounts: true, out resourcesToOrder, componentsToProcure);
                                    foreach (Cargo item4 in resourcesToOrder)
                                    {
                                        CreateOrder(builtObject2, item4.CommodityResource, item4.Amount, isState: false, OrderType.ConstructionShortage);
                                    }
                                }
                                builtObject.ClearPreviousMissionRequirements();
                                builtObject.AssignMission(BuiltObjectMissionType.Retrofit, stellarObject, null, design, BuiltObjectMissionPriority.VeryHigh);
                                return true;
                            }
                        }
                    }
                    else
                    {
                        BuiltObject builtObject3 = null;
                        if (location is BuiltObject)
                        {
                            builtObject3 = (BuiltObject)location;
                        }
                        if (builtObject3 == null)
                        {
                            builtObject3 = SpacePorts.FindShortestConstructionWaitQueueCloseToBuiltObject(builtObject, out var shortestWaitQueueTime2);
                            double num2 = shortestWaitQueueTime2 / (double)Galaxy.RealSecondsInGalacticYear;
                            num2 /= 2000.0;
                            if (num2 >= Galaxy.MaximumConstructionQueueWaitTimeYears && !forceUseOfYard)
                            {
                                builtObject3 = null;
                            }
                        }
                        if (builtObject3 != null)
                        {
                            bool flag3 = false;
                            if (builtObject3.ConstructionQueue != null && builtObject3.ConstructionQueue.ConstructionYards != null)
                            {
                                for (int k = 0; k < builtObject3.ConstructionQueue.ConstructionYards.Count; k++)
                                {
                                    if (builtObject3.ConstructionQueue.ConstructionYards[k].ShipUnderConstruction == null)
                                    {
                                        flag3 = true;
                                        break;
                                    }
                                }
                            }
                            if (flag3 || forceUseOfYard)
                            {
                                if (builtObject.Empire != null && builtObject.Empire.PirateEmpireBaseHabitat != null)
                                {
                                    builtObject.Empire.StateMoney -= cost;
                                    builtObject.Empire.PirateEconomy.PerformExpense(cost, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                                }
                                else if (builtObject.Owner == null)
                                {
                                    builtObject.Empire.PerformPrivateTransaction(0.0 - cost);
                                    builtObject.Empire.StateMoney += BaconBuiltObject.PrivateSectorBuildOrRefitInvestInInfrastructure(builtObject, cost);
                                }
                                else
                                {
                                    builtObject.Empire.StateMoney -= cost;
                                    builtObject.Empire.PirateEconomy.PerformExpense(cost, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                                }
                                builtObject.PurchasePrice = cost;
                                ProcureConstructionComponents(builtObject, builtObject3, orderPreciseResourceAmounts: true, out resourcesToOrder, componentsToProcure);
                                foreach (Cargo item5 in resourcesToOrder)
                                {
                                    CreateOrder(builtObject3, item5.CommodityResource, item5.Amount, isState: false, OrderType.ConstructionShortage);
                                }
                                builtObject.ClearPreviousMissionRequirements();
                                builtObject.AssignMission(BuiltObjectMissionType.Retrofit, builtObject3, null, design, BuiltObjectMissionPriority.VeryHigh);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public BuiltObject FindNearestShipYardBase(BuiltObject ship)
        {
            BuiltObject result = null;
            double num = double.MaxValue;
            for (int i = 0; i < ConstructionYards.Count; i++)
            {
                BuiltObject builtObject = ConstructionYards[i];
                if (builtObject.Role == BuiltObjectRole.Base)
                {
                    double num2 = _Galaxy.CalculateDistanceSquared(ship.Xpos, ship.Ypos, builtObject.Xpos, builtObject.Ypos);
                    if (num2 < num)
                    {
                        result = builtObject;
                        num = num2;
                    }
                }
            }
            return result;
        }

        public StellarObject FindNearestShipYard(BuiltObject ship, bool canRepairOrBuild, bool includeVerySmallYards)
        {
            StellarObject result = null;
            double num = double.MaxValue;
            bool flag = false;
            Empire actualEmpire = ship.ActualEmpire;
            if (actualEmpire != null && actualEmpire.PirateEmpireBaseHabitat != null)
            {
                flag = true;
            }
            for (int i = 0; i < ConstructionYards.Count; i++)
            {
                BuiltObject builtObject = ConstructionYards[i];
                if (builtObject.Role != BuiltObjectRole.Base)
                {
                    continue;
                }
                bool flag2 = true;
                if (!includeVerySmallYards)
                {
                    int num2 = 0;
                    if (builtObject.ConstructionQueue != null && builtObject.ConstructionQueue.ConstructionYards != null)
                    {
                        num2 = builtObject.ConstructionQueue.ConstructionYards.Count;
                    }
                    if (builtObject.ExtractionGas > 0 && num2 <= 1 && !flag)
                    {
                        flag2 = false;
                    }
                }
                if (flag2)
                {
                    double num3 = _Galaxy.CalculateDistanceSquared(ship.Xpos, ship.Ypos, builtObject.Xpos, builtObject.Ypos);
                    if (num3 < num)
                    {
                        result = builtObject;
                        num = num3;
                    }
                }
            }
            if (canRepairOrBuild && (ship.SubRole == BuiltObjectSubRole.ColonyShip || ship.SubRole == BuiltObjectSubRole.ConstructionShip || ship.SubRole == BuiltObjectSubRole.ResupplyShip) && !flag)
            {
                result = null;
                num = double.MaxValue;
                for (int j = 0; j < Colonies.Count; j++)
                {
                    Habitat habitat = Colonies[j];
                    double num4 = _Galaxy.CalculateDistanceSquared(ship.Xpos, ship.Ypos, habitat.Xpos, habitat.Ypos);
                    if (num4 < num)
                    {
                        result = habitat;
                        num = num4;
                    }
                }
            }
            return result;
        }

        public BuiltObject FindNearestSpaceportWithCargo(double x, double y, CargoList cargoItems)
        {
            BuiltObject builtObject = null;
            BuiltObject builtObject2 = null;
            double num = double.MaxValue;
            double num2 = double.MaxValue;
            if (cargoItems != null && SpacePorts != null)
            {
                for (int i = 0; i < SpacePorts.Count; i++)
                {
                    BuiltObject builtObject3 = SpacePorts[i];
                    if (builtObject3 == null || builtObject3.Cargo == null)
                    {
                        continue;
                    }
                    double num3 = _Galaxy.CalculateDistanceSquared(x, y, builtObject3.Xpos, builtObject3.Ypos);
                    if (!(num3 < num2))
                    {
                        continue;
                    }
                    bool flag = true;
                    for (int j = 0; j < cargoItems.Count; j++)
                    {
                        Cargo cargo = cargoItems[j];
                        if (cargo == null)
                        {
                            continue;
                        }
                        bool flag2 = false;
                        Cargo cargo2 = null;
                        if (cargo.CommodityResource != null)
                        {
                            builtObject3.Cargo.GetCargo(cargo.CommodityResource, cargo.EmpireId);
                            if (cargo2 != null && cargo2.Available >= cargo.Amount)
                            {
                                flag2 = true;
                            }
                        }
                        else if (cargo.Component != null)
                        {
                            builtObject3.Cargo.GetCargo(cargo.Component, cargo.EmpireId);
                            if (cargo2 != null && cargo2.Available >= cargo.Amount)
                            {
                                flag2 = true;
                            }
                        }
                        if (!flag2)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        if (num3 < num2)
                        {
                            builtObject2 = builtObject3;
                            num2 = num3;
                        }
                    }
                    else if (num3 < num)
                    {
                        builtObject = builtObject3;
                        num = num3;
                    }
                }
            }
            if (builtObject2 == null)
            {
                builtObject2 = builtObject;
            }
            return builtObject2;
        }

        public Habitat FindNearestColonyWithExcessTroops(BuiltObject ship)
        {
            return FindNearestColonyWithExcessTroops(ship, enforceMinimumTroopLimits: false);
        }

        public Habitat FindNearestColonyWithExcessTroops(BuiltObject ship, bool enforceMinimumTroopLimits)
        {
            TroopList prefilteredTroopsNotBeingPickedUp = new TroopList();
            return FindNearestColonyWithExcessTroops(ship, enforceMinimumTroopLimits, out prefilteredTroopsNotBeingPickedUp);
        }

        public Habitat FindNearestColonyWithExcessTroops(BuiltObject ship, bool enforceMinimumTroopLimits, out TroopList prefilteredTroopsNotBeingPickedUp)
        {
            return FindNearestColonyWithExcessTroops(ship, enforceMinimumTroopLimits, out prefilteredTroopsNotBeingPickedUp, allowTroopTypeFallback: false);
        }

        public Habitat FindNearestColonyWithExcessTroops(BuiltObject ship, bool enforceMinimumTroopLimits, out TroopList prefilteredTroopsNotBeingPickedUp, bool allowTroopTypeFallback)
        {
            double num = double.MaxValue;
            Habitat habitat = null;
            prefilteredTroopsNotBeingPickedUp = new TroopList();
            if (ship != null && ship.Troops != null && ship.Empire != null)
            {
                int infantryAmount = 0;
                int armorAmount = 0;
                int artilleryAmount = 0;
                int specialForcesAmount = 0;
                if (ship.ShipGroup != null)
                {
                    ship.ShipGroup.GetTroopLoadoutTargetAmounts(out infantryAmount, out artilleryAmount, out armorAmount, out specialForcesAmount);
                }
                else
                {
                    ship.GetTroopLoadoutTargetAmounts(out infantryAmount, out artilleryAmount, out armorAmount, out specialForcesAmount);
                }
                int infantryCount = 0;
                int armorCount = 0;
                int artilleryCount = 0;
                int specialForcesCount = 0;
                if (ship.ShipGroup != null)
                {
                    ship.ShipGroup.GetTroopCountsByType(out infantryCount, out artilleryCount, out armorCount, out specialForcesCount);
                }
                else
                {
                    infantryCount = ship.Troops.CountByType(TroopType.Infantry);
                    artilleryCount = ship.Troops.CountByType(TroopType.Artillery);
                    armorCount = ship.Troops.CountByType(TroopType.Armored);
                    specialForcesCount = ship.Troops.CountByType(TroopType.SpecialForces);
                }
                bool flag = infantryAmount > 0 && infantryCount < infantryAmount;
                bool flag2 = armorAmount > 0 && armorCount < armorAmount;
                bool flag3 = artilleryAmount > 0 && artilleryCount < artilleryAmount;
                bool flag4 = specialForcesAmount > 0 && specialForcesCount < specialForcesAmount;
                if (ship.ShipGroup == null && ship.TroopLoadoutInfantry == byte.MaxValue && ship.TroopLoadoutArmored == byte.MaxValue && ship.TroopLoadoutArtillery == byte.MaxValue && ship.TroopLoadoutSpecialForces == byte.MaxValue)
                {
                    flag = true;
                    flag2 = true;
                    flag3 = true;
                    flag4 = true;
                }
                else if (ship.ShipGroup != null && ship.ShipGroup.TroopLoadoutInfantry == byte.MaxValue && ship.ShipGroup.TroopLoadoutArmored == byte.MaxValue && ship.ShipGroup.TroopLoadoutArtillery == byte.MaxValue && ship.ShipGroup.TroopLoadoutSpecialForces == byte.MaxValue)
                {
                    flag = true;
                    flag2 = true;
                    flag3 = true;
                    flag4 = true;
                }
                int num2 = (int)(100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.Infantry));
                int num3 = (int)(100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.Armored));
                int num4 = (int)(100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.Artillery));
                int num5 = (int)(100.0 * Galaxy.CalculateDefaultTroopMaintenanceMultiplier(TroopType.SpecialForces));
                int troopCapacityRemaining = ship.TroopCapacityRemaining;
                if (flag && troopCapacityRemaining < num2)
                {
                    flag = false;
                }
                if (flag3 && troopCapacityRemaining < num4)
                {
                    flag3 = false;
                }
                if (flag2 && troopCapacityRemaining < num3)
                {
                    flag2 = false;
                }
                if (flag4 && troopCapacityRemaining < num5)
                {
                    flag4 = false;
                }
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat2 = Colonies[i];
                    if (habitat2.InvadingTroops != null && habitat2.InvadingTroops.Count > 0)
                    {
                        continue;
                    }
                    int num6 = habitat2.TroopLevelMinimum * 100;
                    if (!enforceMinimumTroopLimits)
                    {
                        num6 = -1;
                    }
                    if (habitat2.Troops == null || habitat2.Troops.TotalDefendStrength <= num6 || habitat2.Troops.Count <= 0 || habitat2.Troops.TotalDefendStrengthNotGarrisonedNotAwaitingPickup <= 0)
                    {
                        continue;
                    }
                    TroopList remainingTroops = new TroopList();
                    int num7 = CalculateTroopStrengthToBePickedUp(habitat2, out remainingTroops);
                    int num8 = habitat2.Troops.TotalDefendStrengthNotGarrisoned;
                    if (enforceMinimumTroopLimits)
                    {
                        int totalDefendStrengthGarrisoned = habitat2.Troops.TotalDefendStrengthGarrisoned;
                        if (num6 > totalDefendStrengthGarrisoned)
                        {
                            num8 -= num6 - totalDefendStrengthGarrisoned;
                        }
                    }
                    num8 -= num7;
                    if (num8 <= 0)
                    {
                        continue;
                    }
                    bool flag5 = false;
                    Troop firstNonGarrisoned = habitat2.Troops.GetFirstNonGarrisoned(TroopType.SpecialForces);
                    Troop firstNonGarrisoned2 = habitat2.Troops.GetFirstNonGarrisoned(TroopType.Armored);
                    Troop firstNonGarrisoned3 = habitat2.Troops.GetFirstNonGarrisoned(TroopType.Artillery);
                    Troop firstNonGarrisoned4 = habitat2.Troops.GetFirstNonGarrisoned(TroopType.Infantry);
                    if (flag4 && firstNonGarrisoned != null)
                    {
                        flag5 = true;
                    }
                    if (flag2 && firstNonGarrisoned2 != null)
                    {
                        flag5 = true;
                    }
                    if (flag3 && firstNonGarrisoned3 != null)
                    {
                        flag5 = true;
                    }
                    if (flag && firstNonGarrisoned4 != null)
                    {
                        flag5 = true;
                    }
                    if (!flag5 && allowTroopTypeFallback && (firstNonGarrisoned4 != null || firstNonGarrisoned3 != null || firstNonGarrisoned2 != null || firstNonGarrisoned != null))
                    {
                        flag5 = true;
                    }
                    if (flag5)
                    {
                        double num9 = _Galaxy.CalculateDistanceSquared(ship.Xpos, ship.Ypos, habitat2.Xpos, habitat2.Ypos);
                        if (num9 < num)
                        {
                            habitat = habitat2;
                            num = num9;
                            prefilteredTroopsNotBeingPickedUp = remainingTroops;
                        }
                    }
                }
                if (habitat == null && !allowTroopTypeFallback)
                {
                    habitat = FindNearestColonyWithExcessTroops(ship, enforceMinimumTroopLimits, out prefilteredTroopsNotBeingPickedUp, allowTroopTypeFallback: true);
                }
            }
            return habitat;
        }

        public int CalculateTroopStrengthToBePickedUp(Habitat colony, out TroopList remainingTroops)
        {
            int num = 0;
            remainingTroops = new TroopList();
            if (colony != null && colony.Troops != null)
            {
                remainingTroops = colony.Troops.GetTroopsNotGarrisonedNotAwaitingPickup();
                for (int i = 0; i < BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = BuiltObjects[i];
                    if (builtObject == null || builtObject.TroopCapacity <= 0)
                    {
                        continue;
                    }
                    BuiltObjectMission mission = builtObject.Mission;
                    if (mission == null || mission.Type != BuiltObjectMissionType.LoadTroops || mission.TargetHabitat == null || mission.TargetHabitat != colony || mission.Troops == null)
                    {
                        continue;
                    }
                    num += mission.Troops.TotalDefendStrength;
                    for (int j = 0; j < mission.Troops.Count; j++)
                    {
                        Troop troop = mission.Troops[j];
                        if (troop != null && remainingTroops.Contains(troop))
                        {
                            remainingTroops.Remove(troop);
                        }
                    }
                }
            }
            return num;
        }

        public bool CheckAssignUnloadTroopsAtColonyNeedingThemMission(BuiltObject ship)
        {
            if (ColoniesNeedingTroops != null && ColoniesNeedingTroops.Count > 0 && ship != null && ship.Troops != null && ship.Troops.Count > 0 && !CheckAtWar())
            {
                int num = 65000;
                for (int i = 0; i < ColoniesNeedingTroops.Count; i++)
                {
                    Habitat habitat = ColoniesNeedingTroops[i];
                    if (habitat != null && habitat.Troops != null && habitat.Troops.TotalAttackStrength < num)
                    {
                        return AssignUnloadTroopsAtColonyNeedingThemMission(ship, habitat);
                    }
                }
            }
            return false;
        }

        public bool AssignUnloadTroopsAtColonyNeedingThemMission(BuiltObject ship, Habitat colony)
        {
            if (ship != null && colony != null && colony.Empire == this && ColoniesNeedingTroops != null && ColoniesNeedingTroops.Contains(colony) && ship.Troops.Count > 0)
            {
                ship.AssignMission(BuiltObjectMissionType.UnloadTroops, colony, null, BuiltObjectMissionPriority.Normal);
                ColoniesNeedingTroops.Remove(colony);
                return true;
            }
            return false;
        }

        public bool CheckAssignUnloadTroopsAtColonyNeedingThemMission(ShipGroup fleet)
        {
            if (ColoniesNeedingTroops != null && ColoniesNeedingTroops.Count > 0 && fleet != null && fleet.Ships != null && fleet.TotalTroopCount > 0 && !CheckAtWar())
            {
                int num = 65000;
                for (int i = 0; i < ColoniesNeedingTroops.Count; i++)
                {
                    Habitat habitat = ColoniesNeedingTroops[i];
                    if (habitat != null && habitat.Troops != null && habitat.Troops.TotalAttackStrength < num)
                    {
                        return AssignUnloadTroopsAtColonyNeedingThemMission(fleet, habitat);
                    }
                }
            }
            return false;
        }

        public bool AssignUnloadTroopsAtColonyNeedingThemMission(ShipGroup fleet, Habitat colony)
        {
            if (fleet != null && colony != null && colony.Empire == this && ColoniesNeedingTroops != null && ColoniesNeedingTroops.Contains(colony) && fleet.TotalTroopCount > 0 && AssignFleetUnloadTroops(fleet, colony, manuallyAssigned: false))
            {
                ColoniesNeedingTroops.Remove(colony);
                return true;
            }
            return false;
        }

        public bool CheckAssignGarrisonTroopsAtPenalColonyMission(BuiltObject ship)
        {
            if (ship != null && ship.Troops != null && ship.Troops.Count > 0 && PenalColonies != null)
            {
                for (int i = 0; i < PenalColonies.Count; i++)
                {
                    Habitat habitat = PenalColonies[i];
                    if (habitat != null && habitat.Troops != null && habitat.Troops.TotalDefendStrength / 100 < habitat.TroopLevelRequired)
                    {
                        return AssignGarrisonTroopsAtPenalColonyMission(ship, habitat);
                    }
                }
            }
            return false;
        }

        public bool AssignGarrisonTroopsAtPenalColonyMission(BuiltObject ship, Habitat penalColony)
        {
            if (ship != null && penalColony != null && penalColony.Empire == this && PenalColonies != null && PenalColonies.Contains(penalColony) && ship.Troops.Count > 0)
            {
                ship.AssignMission(BuiltObjectMissionType.UnloadTroops, penalColony, null, BuiltObjectMissionPriority.Normal);
                return true;
            }
            return false;
        }

        public bool AssignLoadTroopsMission(BuiltObject ship)
        {
            return AssignLoadTroopsMission(ship, null);
        }

        public bool AssignLoadTroopsMission(BuiltObject ship, Habitat colony)
        {
            return AssignLoadTroopsMission(ship, colony, queueMission: false, enforceMinimumTroopLimits: true);
        }

        public bool AssignLoadTroopsMission(BuiltObject ship, Habitat colony, bool queueMission, bool enforceMinimumTroopLimits)
        {
            return AssignLoadTroopsMission(ship, colony, queueMission, enforceMinimumTroopLimits, manuallyAssigned: false);
        }

        public bool AssignLoadTroopsMission(BuiltObject ship, Habitat colony, bool queueMission, bool enforceMinimumTroopLimits, bool manuallyAssigned)
        {
            if (ship.Troops != null && ship.TroopCapacity - ship.Troops.TotalSize >= 100)
            {
                Habitat habitat = colony;
                TroopList prefilteredTroopsNotBeingPickedUp = new TroopList();
                if (colony == null)
                {
                    habitat = FindNearestColonyWithExcessTroops(ship, enforceMinimumTroopLimits, out prefilteredTroopsNotBeingPickedUp, allowTroopTypeFallback: false);
                }
                if (habitat != null)
                {
                    bool flag = false;
                    if (habitat.Empire != ship.Empire && habitat.InvadingTroops != null && habitat.InvadingTroops.Count > 0 && habitat.InvadingTroops[0].Empire == ship.Empire)
                    {
                        flag = true;
                        enforceMinimumTroopLimits = false;
                    }
                    if (flag)
                    {
                        for (int i = 0; i < habitat.InvadingTroops.Count; i++)
                        {
                            Troop troop = habitat.InvadingTroops[i];
                            if (troop != null && troop.Empire == ship.Empire)
                            {
                                prefilteredTroopsNotBeingPickedUp.Add(troop);
                            }
                        }
                    }
                    else if (prefilteredTroopsNotBeingPickedUp == null || prefilteredTroopsNotBeingPickedUp.Count <= 0)
                    {
                        prefilteredTroopsNotBeingPickedUp = habitat.Troops.GetTroopsNotGarrisonedNotAwaitingPickup();
                    }
                    if (prefilteredTroopsNotBeingPickedUp.Count > 0)
                    {
                        int num = ship.TroopCapacityRemaining;
                        int num2 = 0;
                        if (flag)
                        {
                            num2 = habitat.InvadingTroops.TotalDefendStrength;
                        }
                        else
                        {
                            num2 = habitat.Troops.TotalDefendStrength;
                            if (enforceMinimumTroopLimits)
                            {
                                int num3 = habitat.TroopLevelMinimum * 100;
                                num2 -= num3;
                            }
                        }
                        TroopList troopList = new TroopList();
                        bool flag2 = false;
                        for (int j = 0; j < prefilteredTroopsNotBeingPickedUp.Count; j++)
                        {
                            Troop troop2 = prefilteredTroopsNotBeingPickedUp[j];
                            if (troop2 != null && num2 - (int)((float)troop2.DefendStrength * troop2.Readiness) >= 0 && num >= troop2.Size)
                            {
                                troopList.Add(troop2);
                                flag2 = true;
                                num -= troop2.Size;
                                num2 -= (int)((float)troop2.DefendStrength * troop2.Readiness);
                                if (num <= 0 || num2 <= 0)
                                {
                                    break;
                                }
                            }
                        }
                        if (flag2 && ship.WithinFuelRange(habitat.Xpos, habitat.Ypos, 0.0))
                        {
                            if (queueMission)
                            {
                                ship.QueueMission(BuiltObjectMissionType.LoadTroops, habitat, null, troopList, BuiltObjectMissionPriority.Normal);
                            }
                            else
                            {
                                ship.ClearPreviousMissionRequirements();
                                ship.AssignMission(BuiltObjectMissionType.LoadTroops, habitat, null, troopList, BuiltObjectMissionPriority.Normal, manuallyAssigned);
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private ShipGroup FindShipGroupAssignedNearPoint(double x, double y)
        {
            double num = 1000.0;
            double num2 = x - num;
            double num3 = x + num;
            double num4 = y - num;
            double num5 = y + num;
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (shipGroup.Mission != null && shipGroup.Mission.Type != 0)
                {
                    Point point = shipGroup.Mission.ResolveTargetCoordinates(shipGroup.Mission);
                    if ((double)point.X > num2 && (double)point.X < num3 && (double)point.Y > num4 && (double)point.Y < num5)
                    {
                        return shipGroup;
                    }
                }
            }
            return null;
        }

        private ShipGroup FindShipGroupAssignedToLocation(GalaxyLocation location)
        {
            double num = (double)location.Xpos - (double)location.Width / 2.0;
            double num2 = (double)location.Xpos + (double)location.Width / 2.0;
            double num3 = (double)location.Ypos - (double)location.Height / 2.0;
            double num4 = (double)location.Ypos + (double)location.Height / 2.0;
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (shipGroup.Mission != null && shipGroup.Mission.Type != 0 && (double)shipGroup.Mission.X > num && (double)shipGroup.Mission.X < num2 && (double)shipGroup.Mission.Y > num3 && (double)shipGroup.Mission.Y < num4)
                {
                    return shipGroup;
                }
            }
            return null;
        }

        private void SendAvailableFleetsToGuardSingleStrategicLocation(GalaxyLocation location)
        {
            location.ResolveLocationCenter(out var x, out var y);
            SendAvailableFleetsToGuardSingleStrategicLocation(x, y);
        }

        private void SendAvailableFleetsToGuardSingleStrategicLocation(double x, double y)
        {
            if (!_ControlMilitaryFleets)
            {
                return;
            }
            ShipGroup shipGroup = FindShipGroupAssignedNearPoint(x, y);
            if (shipGroup != null)
            {
                return;
            }
            Habitat habitat = _Galaxy.FastFindNearestColony(x, y, this, 0);
            if (habitat == null)
            {
                return;
            }
            double num = _Galaxy.CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            if (!(num < (double)(Galaxy.SectorSize * 2)))
            {
                return;
            }
            shipGroup = FindNearestAvailableFleet(x, y, BuiltObjectMissionPriority.Low, 0, FleetPosture.Defend, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: true, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: true, 0);
            if (shipGroup == null)
            {
                shipGroup = FindNearestAvailableFleet(x, y, BuiltObjectMissionPriority.Low, 0, FleetPosture.Defend, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: true, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: false, 0);
            }
            if (shipGroup != null)
            {
                Habitat habitat2 = _Galaxy.FindLonelyHabitat(x, y);
                if (habitat2 != null)
                {
                    double num2 = _Galaxy.CalculateDistance(habitat2.Xpos, habitat2.Ypos, x, y);
                    if (num2 > 1000.0)
                    {
                        habitat2 = null;
                    }
                }
                long starDate = _Galaxy.CurrentStarDate + Galaxy.Rnd.Next(400000, 550000);
                if (habitat2 != null)
                {
                    shipGroup.AssignMission(BuiltObjectMissionType.MoveAndWait, habitat2, null, starDate, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                }
                else
                {
                    shipGroup.AssignMission(BuiltObjectMissionType.MoveAndWait, null, null, null, null, x, y, starDate, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                }
            }
            else
            {
                SetDefendFleetForLocation(x, y);
            }
        }

        private void SendAvailableFleetsToGuardStrategicLocations()
        {
            if (ShipGroups != null && ShipGroups.Count > 3 && Policy.BuildPlanetDestroyers)
            {
                BuiltObjectList constructionShipsBuildingPlanetDestroyers = ConstructionShips.GetConstructionShipsBuildingPlanetDestroyers();
                for (int i = 0; i < constructionShipsBuildingPlanetDestroyers.Count; i++)
                {
                    BuiltObject builtObject = constructionShipsBuildingPlanetDestroyers[i];
                    if (builtObject != null && builtObject.Mission != null)
                    {
                        Point point = builtObject.Mission.ResolveTargetCoordinates(builtObject.Mission);
                        SendAvailableFleetsToGuardSingleStrategicLocation(point.X, point.Y);
                    }
                }
            }
            if (CheckAtWar())
            {
                return;
            }
            for (int j = 0; j < KnownGalaxyLocations.Count; j++)
            {
                if (KnownGalaxyLocations[j] != null && KnownGalaxyLocations[j].Type == GalaxyLocationType.DebrisField)
                {
                    SendAvailableFleetsToGuardSingleStrategicLocation(KnownGalaxyLocations[j]);
                }
            }
            for (int k = 0; k < KnownGalaxyLocations.Count; k++)
            {
                if (KnownGalaxyLocations[k] != null && KnownGalaxyLocations[k].Type == GalaxyLocationType.PlanetDestroyer)
                {
                    SendAvailableFleetsToGuardSingleStrategicLocation(KnownGalaxyLocations[k]);
                }
            }
        }

        private GalaxyLocation CheckWhetherAtLocation(double x, double y)
        {
            for (int i = 0; i < KnownGalaxyLocations.Count; i++)
            {
                double num = (double)KnownGalaxyLocations[i].Xpos - (double)KnownGalaxyLocations[i].Width / 2.0;
                double num2 = (double)KnownGalaxyLocations[i].Xpos + (double)KnownGalaxyLocations[i].Width / 2.0;
                double num3 = (double)KnownGalaxyLocations[i].Ypos - (double)KnownGalaxyLocations[i].Height / 2.0;
                double num4 = (double)KnownGalaxyLocations[i].Ypos + (double)KnownGalaxyLocations[i].Height / 2.0;
                if (x > num && x < num2 && y > num3 && y < num4)
                {
                    return KnownGalaxyLocations[i];
                }
            }
            return null;
        }

        private BuiltObject SelectBestSalvageableShip(GalaxyLocation location)
        {
            BuiltObject builtObject = null;
            if (location != null && location.Type == GalaxyLocationType.DebrisField)
            {
                BuiltObjectList builtObjectList = _Galaxy.FindAbandonedShipsInDebrisField(location);
                for (int i = 0; i < builtObjectList.Count; i++)
                {
                    if (builtObjectList[i].BuiltAt == null)
                    {
                        if (builtObject == null)
                        {
                            builtObject = builtObjectList[i];
                        }
                        else if (builtObjectList[i].Size > builtObject.Size)
                        {
                            builtObject = builtObjectList[i];
                        }
                    }
                }
            }
            return builtObject;
        }

        public EmpirePriorityList ResolveEnemyEmpires(int attitudeThreshold)
        {
            EmpirePriorityList empirePriorityList = new EmpirePriorityList();
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                bool flag = false;
                switch (diplomaticRelation.Strategy)
                {
                    case DiplomaticStrategy.Conquer:
                    case DiplomaticStrategy.Undermine:
                    case DiplomaticStrategy.DefendUndermine:
                    case DiplomaticStrategy.Punish:
                        flag = true;
                        break;
                }
                if (flag && diplomaticRelation.Type != 0)
                {
                    EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(diplomaticRelation.OtherEmpire);
                    double val = (double)(empireEvaluation.OverallAttitude - attitudeThreshold) * -1.0;
                    val = Math.Max(0.0, val);
                    double priority = (double)empireEvaluation.Empire.MilitaryPotency * val;
                    empirePriorityList.Add(new EmpirePriority(empireEvaluation.Empire, priority));
                }
            }
            empirePriorityList.Sort();
            empirePriorityList.Reverse();
            return empirePriorityList;
        }

        public double CalculatePirateAttackCost(Empire empireToAttack)
        {
            double value = (double)empireToAttack.TotalColonyStrategicValue / 100.0;
            value = Math.Round(value, 0);
            return Math.Max(2000.0, Math.Min(value, 15000.0));
        }

        public void DetermineEmpiresPirateWillingToAttack(Empire pirateFaction, Empire requestingEmpire, bool onlyUnfriendlyEmpires, out EmpireList empiresWillingToAttack, out List<double> attackCosts)
        {
            empiresWillingToAttack = new EmpireList();
            attackCosts = new List<double>();
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire == requestingEmpire)
                {
                    continue;
                }
                DiplomaticRelation diplomaticRelation = requestingEmpire.ObtainDiplomaticRelation(empire);
                if (diplomaticRelation.Type == DiplomaticRelationType.NotMet || pirateFaction.PirateMissions.Contains(empire, EmpireActivityType.Attack) || !_Galaxy.DetermineEmpireColonyNearPoint(pirateFaction.PirateEmpireBaseHabitat.Xpos, pirateFaction.PirateEmpireBaseHabitat.Ypos, empire, Galaxy.PirateEmpireAttackDistance))
                {
                    continue;
                }
                bool flag = true;
                if (onlyUnfriendlyEmpires)
                {
                    flag = false;
                    if (diplomaticRelation.Type == DiplomaticRelationType.War || diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions)
                    {
                        flag = true;
                    }
                    else
                    {
                        EmpireEvaluation empireEvaluation = requestingEmpire.ObtainEmpireEvaluation(empire);
                        int num = -6;
                        num -= (int)empireEvaluation.Bias;
                        if (empireEvaluation.OverallAttitude <= num)
                        {
                            flag = true;
                        }
                    }
                }
                if (flag)
                {
                    empiresWillingToAttack.Add(empire);
                    double item = CalculatePirateAttackCost(empire);
                    attackCosts.Add(item);
                }
            }
        }

        public void GenerateSaleableInfoForEmpire(Empire pirateFaction, Empire buyingEmpire, out EmpireList unmetEmpires, out HabitatList unexploredSystems, out HabitatList independentColonies, out HabitatList ruinHabitats, out GalaxyLocationList debrisFieldLocations, out GalaxyLocationList planetDestroyerLocations, out GalaxyLocationList restrictedAreaLocations)
        {
            unmetEmpires = new EmpireList();
            unexploredSystems = new HabitatList();
            independentColonies = new HabitatList();
            ruinHabitats = new HabitatList();
            debrisFieldLocations = new GalaxyLocationList();
            planetDestroyerLocations = new GalaxyLocationList();
            restrictedAreaLocations = new GalaxyLocationList();
            if (buyingEmpire == null || pirateFaction == null || pirateFaction.PirateEmpireBaseHabitat == null)
            {
                return;
            }
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire == null || empire == buyingEmpire)
                {
                    continue;
                }
                bool flag = false;
                if (pirateFaction.PirateEmpireBaseHabitat != null)
                {
                    PirateRelation pirateRelation = pirateFaction.ObtainPirateRelation(empire);
                    if (pirateRelation.Type != 0)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    continue;
                }
                if (buyingEmpire.PirateEmpireBaseHabitat != null)
                {
                    PirateRelation pirateRelation2 = buyingEmpire.ObtainPirateRelation(empire);
                    if (pirateRelation2.Type == PirateRelationType.NotMet)
                    {
                        unmetEmpires.Add(empire);
                    }
                    continue;
                }
                DiplomaticRelation diplomaticRelation = buyingEmpire.ObtainDiplomaticRelation(empire);
                if (diplomaticRelation.Type == DiplomaticRelationType.NotMet)
                {
                    PirateRelation pirateRelation3 = pirateFaction.ObtainPirateRelation(empire);
                    if (pirateRelation3.Type != 0)
                    {
                        unmetEmpires.Add(empire);
                    }
                }
            }
            if (buyingEmpire.Capital != null)
            {
                Habitat habitat = _Galaxy.FindNearestUnexploredSystem(buyingEmpire.Capital.Xpos, buyingEmpire.Capital.Ypos, buyingEmpire);
                if (habitat != null && pirateFaction.CheckSystemExplored(habitat.SystemIndex))
                {
                    unexploredSystems.Add(habitat);
                }
            }
            else if (buyingEmpire.PirateEmpireBaseHabitat != null)
            {
                Habitat habitat2 = _Galaxy.FindNearestUnexploredSystem(buyingEmpire.PirateEmpireBaseHabitat.Xpos, buyingEmpire.PirateEmpireBaseHabitat.Ypos, buyingEmpire);
                if (habitat2 != null && pirateFaction.CheckSystemExplored(habitat2.SystemIndex))
                {
                    unexploredSystems.Add(habitat2);
                }
            }
            for (int j = 0; j < _Galaxy.Habitats.Count; j++)
            {
                Habitat habitat3 = _Galaxy.Habitats[j];
                if (habitat3.Empire == _Galaxy.IndependentEmpire && pirateFaction.CheckSystemExplored(habitat3.SystemIndex) && !buyingEmpire.CheckSystemExplored(habitat3.SystemIndex))
                {
                    independentColonies.Add(habitat3);
                }
            }
            for (int k = 0; k < _Galaxy.Habitats.Count; k++)
            {
                Habitat habitat4 = _Galaxy.Habitats[k];
                if (habitat4.Ruin != null && (habitat4.Empire == null || habitat4.Empire == _Galaxy.IndependentEmpire) && pirateFaction.CheckSystemExplored(habitat4.SystemIndex))
                {
                    double num = _Galaxy.CalculateDistance(habitat4.Xpos, habitat4.Ypos, pirateFaction.PirateEmpireBaseHabitat.Xpos, pirateFaction.PirateEmpireBaseHabitat.Ypos);
                    if (num < (double)Galaxy.SectorSize * 2.0 && !buyingEmpire.CheckSystemExplored(habitat4.SystemIndex))
                    {
                        ruinHabitats.Add(habitat4);
                    }
                }
            }
            if (buyingEmpire.KnownGalaxyLocations == null)
            {
                return;
            }
            for (int l = 0; l < pirateFaction.KnownGalaxyLocations.Count; l++)
            {
                GalaxyLocation galaxyLocation = pirateFaction.KnownGalaxyLocations[l];
                if (galaxyLocation == null)
                {
                    continue;
                }
                if (galaxyLocation.Type == GalaxyLocationType.DebrisField)
                {
                    if (!buyingEmpire.KnownGalaxyLocations.Contains(galaxyLocation))
                    {
                        galaxyLocation.ResolveLocationCenter(out var x, out var y);
                        double num2 = _Galaxy.CalculateDistance(x, y, pirateFaction.PirateEmpireBaseHabitat.Xpos, pirateFaction.PirateEmpireBaseHabitat.Ypos);
                        if (num2 < (double)Galaxy.SectorSize * 3.0)
                        {
                            debrisFieldLocations.Add(galaxyLocation);
                        }
                    }
                }
                else if (galaxyLocation.Type == GalaxyLocationType.PlanetDestroyer)
                {
                    if (!buyingEmpire.KnownGalaxyLocations.Contains(galaxyLocation))
                    {
                        galaxyLocation.ResolveLocationCenter(out var x2, out var y2);
                        double num3 = _Galaxy.CalculateDistance(x2, y2, pirateFaction.PirateEmpireBaseHabitat.Xpos, pirateFaction.PirateEmpireBaseHabitat.Ypos);
                        if (num3 < (double)Galaxy.SectorSize * 3.0)
                        {
                            planetDestroyerLocations.Add(galaxyLocation);
                        }
                    }
                }
                else if (galaxyLocation.Type == GalaxyLocationType.RestrictedArea && (_Galaxy.StoryCluesEnabled || (galaxyLocation.Name != TextResolver.GetText("Dead Zone") && galaxyLocation.Name != string.Format(TextResolver.GetText("NAME Weapons Testing Range"), "Pozdac"))) && !buyingEmpire.KnownGalaxyLocations.Contains(galaxyLocation))
                {
                    galaxyLocation.ResolveLocationCenter(out var x3, out var y3);
                    double num4 = _Galaxy.CalculateDistance(x3, y3, pirateFaction.PirateEmpireBaseHabitat.Xpos, pirateFaction.PirateEmpireBaseHabitat.Ypos);
                    if (num4 < (double)Galaxy.SectorSize * 3.0)
                    {
                        restrictedAreaLocations.Add(galaxyLocation);
                    }
                }
            }
        }

        private void ReviewIndependentColonyTargets()
        {
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            HabitatList habitatList = DetermineHabitatsBeingColonized();
            for (int i = 0; i < habitatList.Count; i++)
            {
                Habitat habitat = habitatList[i];
                if (habitat.Empire == _Galaxy.IndependentEmpire)
                {
                    HabitatPrioritization habitatPrioritization = new HabitatPrioritization(habitat, 0);
                    BuiltObject builtObject = (habitatPrioritization.AssignedShip = DetermineShipGuarding(habitat));
                    habitatPrioritizationList.Add(habitatPrioritization);
                }
            }
            _IndependentColonyTargets = habitatPrioritizationList;
        }

        private BuiltObject DetermineShipGuarding(Habitat habitat)
        {
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = BuiltObjects[i];
                if (builtObject.Role == BuiltObjectRole.Military && builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.MoveAndWait && builtObject.Mission.TargetHabitat == habitat)
                {
                    return builtObject;
                }
            }
            return null;
        }

        public BuiltObjectList ResolvePrioritizedPatrolMiningStations()
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            bool flag = CheckEmpireHasHyperDriveTech(this);
            for (int i = 0; i < MiningStations.Count; i++)
            {
                BuiltObject builtObject = MiningStations[i];
                if (builtObject != null && (builtObject.SubRole == BuiltObjectSubRole.MiningStation || builtObject.SubRole == BuiltObjectSubRole.GasMiningStation) && builtObject.ParentHabitat != null)
                {
                    double num = 0.0;
                    num = ((builtObject.ExtractionLuxury <= 0) ? builtObject.ParentHabitat.CalculateCurrentStrategicResourceValue(_Galaxy) : builtObject.ParentHabitat.CalculateCurrentCompleteResourceValue(_Galaxy));
                    if (builtObject.ParentHabitat.Resources.HasFuelResources())
                    {
                        num *= 5.0;
                    }
                    else if (!flag)
                    {
                        num /= 5.0;
                    }
                    num /= 50.0;
                    num = Math.Min(num, 60.0);
                    if (builtObject.CurrentEscortForceAssigned < (int)num)
                    {
                        builtObject.SortTag = num;
                        builtObjectList.Add(builtObject);
                    }
                }
            }
            builtObjectList.Sort();
            builtObjectList.Reverse();
            return builtObjectList;
        }

        public void AssignMissionsToBuiltObjectList(BuiltObjectList builtObjectList, bool atWar, BuiltObjectList patrolMiningStations)
        {
            for (int i = 0; i < builtObjectList.Count; i++)
            {
                BuiltObject ship = builtObjectList[i];
                AssignMissionToBuiltObject(ship, atWar, patrolMiningStations);
            }
        }

        public void AssignMissionToBuiltObject(BuiltObject ship, bool atWar, BuiltObjectList patrolMiningStations)
        {
            int refusalCount = 0;
            if (ship == null || ship.TopSpeed <= 0 || ship.BuiltAt != null || !ship.IsAutoControlled || (ship.Mission != null && ship.Mission.Type != 0))
            {
                return;
            }
            if (ship.Empire != null && ship.Empire != _Galaxy.IndependentEmpire && !_Galaxy.PirateEmpires.Contains(ship.Empire))
            {
                if (ship.RetireForNextMission)
                {
                    if (ship.ShipGroup != null)
                    {
                        if (ship.ShipGroup.Mission == null || ship.ShipGroup.Mission.Type == BuiltObjectMissionType.Undefined || ship.ShipGroup.Mission.Priority == BuiltObjectMissionPriority.Undefined || ship.ShipGroup.Mission.Priority == BuiltObjectMissionPriority.Low)
                        {
                            ship.LeaveShipGroup();
                            if (AssignScrapMission(ship))
                            {
                                ship.RetireForNextMission = false;
                                return;
                            }
                        }
                    }
                    else if (AssignScrapMission(ship))
                    {
                        ship.RetireForNextMission = false;
                        return;
                    }
                }
                if (ship.RetrofitForNextMission)
                {
                    if (ship.ShipGroup != null)
                    {
                        if ((ship.ShipGroup.Mission == null || ship.ShipGroup.Mission.Type == BuiltObjectMissionType.Undefined || ship.ShipGroup.Mission.Priority == BuiltObjectMissionPriority.Undefined || ship.ShipGroup.Mission.Priority == BuiltObjectMissionPriority.Low) && AssignRetrofitMission(ship))
                        {
                            ship.RetrofitForNextMission = false;
                            return;
                        }
                    }
                    else if (AssignRetrofitMission(ship))
                    {
                        ship.RetrofitForNextMission = false;
                        return;
                    }
                }
                if (ship.RepairForNextMission)
                {
                    if (ship.DamagedComponentCount > 0)
                    {
                        if (AssignRepairMission(ship))
                        {
                            ship.RepairForNextMission = false;
                            return;
                        }
                    }
                    else
                    {
                        ship.RepairForNextMission = false;
                    }
                }
            }
            if (ship.RefuelForNextMission)
            {
                bool flag = true;
                if (ship.ShipGroup != null)
                {
                    flag = false;
                    double num = ship.CurrentFuel / Math.Max(1.0, ship.FuelCapacity);
                    if (num < 0.05)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    ship.SetupRefuelling();
                    return;
                }
            }
            if (ship.Empire == null || ship.Empire == _Galaxy.IndependentEmpire || _Galaxy.PirateEmpires.Contains(ship.Empire))
            {
                return;
            }
            bool flag2 = true;
            if (DominantRace != null)
            {
                flag2 = DominantRace.Expanding;
            }
            if (ship.ShipGroup != null)
            {
                return;
            }
            switch (ship.SubRole)
            {
                case BuiltObjectSubRole.SmallFreighter:
                case BuiltObjectSubRole.MediumFreighter:
                case BuiltObjectSubRole.LargeFreighter:
                    {
                        float num5 = 1f;
                        if (Freighters != null)
                        {
                            num5 = (float)Math.Max(1, Freighters.Count) / (float)Math.Max(1, _EmpireOrderCount);
                        }
                        double num6 = 0.67;
                        if (ship.WarpSpeed > 0)
                        {
                            num6 = 0.33;
                        }
                        num6 /= Math.Sqrt(Math.Max(0.25, num5));
                        num6 = Math.Max(0.25, Math.Min(0.8, num6));
                        bool flag4 = false;
                        if (Galaxy.Rnd.NextDouble() > num6)
                        {
                            flag4 = true;
                        }
                        bool flag5 = false;
                        if (flag4)
                        {
                            int num7 = 0;
                            HabitatList habitatList = new HabitatList();
                            HabitatList habitatList2 = new HabitatList();
                            for (int k = 0; k < SpacePorts.Count; k++)
                            {
                                BuiltObject builtObject3 = SpacePorts[k];
                                if (builtObject3 != null && builtObject3.IsSpacePort && builtObject3.ParentHabitat != null)
                                {
                                    habitatList2.Add(builtObject3.ParentHabitat);
                                }
                            }
                            for (int l = 0; l < Colonies.Count; l++)
                            {
                                Habitat item = Colonies[l];
                                if (!habitatList2.Contains(item))
                                {
                                    habitatList.Add(item);
                                }
                            }
                            num7 = Galaxy.Rnd.Next(0, habitatList.Count);
                            for (int m = num7; m < habitatList.Count; m++)
                            {
                                if (CheckColonyForResourceClearance(ship, habitatList[m]))
                                {
                                    flag5 = true;
                                    break;
                                }
                            }
                            if (!flag5)
                            {
                                for (int n = 0; n < num7; n++)
                                {
                                    if (CheckColonyForResourceClearance(ship, habitatList[n]))
                                    {
                                        flag5 = true;
                                        break;
                                    }
                                }
                            }
                            if (!flag5)
                            {
                                ResourceList empireDeficientResources = IdentifyDeficientEmpireResources();
                                num7 = Galaxy.Rnd.Next(0, MiningStations.Count);
                                for (int num8 = num7; num8 < MiningStations.Count; num8++)
                                {
                                    BuiltObject builtObject4 = MiningStations[num8];
                                    if (builtObject4 != null && builtObject4.IsSpacePort && builtObject4.IsResourceExtractor && CheckMiningStationForResourceClearance(ship, builtObject4, empireDeficientResources))
                                    {
                                        flag5 = true;
                                        break;
                                    }
                                }
                                if (!flag5)
                                {
                                    for (int num9 = 0; num9 < num7; num9++)
                                    {
                                        BuiltObject builtObject5 = MiningStations[num9];
                                        if (builtObject5 != null && builtObject5.IsSpacePort && builtObject5.IsResourceExtractor && CheckMiningStationForResourceClearance(ship, builtObject5, empireDeficientResources))
                                        {
                                            flag5 = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (flag5 || (ship.Mission != null && ship.Mission.Type != 0))
                        {
                            break;
                        }
                        StellarObject stellarObject2 = _Galaxy.FastFindNearestColony((int)ship.Xpos, (int)ship.Ypos, ship.Empire, 0);
                        if (stellarObject2 == null)
                        {
                            break;
                        }
                        double num10 = _Galaxy.CalculateDistance(ship.Xpos, ship.Ypos, stellarObject2.Xpos, stellarObject2.Ypos);
                        double num11 = (double)Galaxy.SectorSize * 2.5;
                        if (ship.WarpSpeed <= 0)
                        {
                            StellarObject stellarObject3 = _Galaxy.FindNearestStationaryStellarObject(ship.Xpos, ship.Ypos, ship.Empire);
                            num11 = 2000.0;
                            if (stellarObject3 != null)
                            {
                                num10 = _Galaxy.CalculateDistance(ship.Xpos, ship.Ypos, stellarObject3.Xpos, stellarObject3.Ypos);
                                stellarObject2 = stellarObject3;
                            }
                        }
                        if (num10 > num11 && stellarObject2 != null && ship.WithinFuelRange(stellarObject2.Xpos, stellarObject2.Ypos, 0.0))
                        {
                            ship.AssignMission(BuiltObjectMissionType.Move, stellarObject2, null, BuiltObjectMissionPriority.Normal);
                        }
                        break;
                    }
                case BuiltObjectSubRole.ColonyShip:
                    {
                        if (DominantRace == null || !DominantRace.Expanding)
                        {
                            break;
                        }
                        _ColonizationTargets.Sort();
                        _ColonizationTargets.Reverse();
                        for (int num12 = 0; num12 < _ColonizationTargets.Count; num12++)
                        {
                            HabitatPrioritization habitatPrioritization = _ColonizationTargets[num12];
                            if (habitatPrioritization.AssignedShip != null)
                            {
                                if (habitatPrioritization.AssignedShip.BuiltAt == null || habitatPrioritization.AssignedShip == ship)
                                {
                                    continue;
                                }
                                int newPopulationAmount = 0;
                                if (CanBuiltObjectColonizeHabitat(ship, habitatPrioritization.Habitat, out newPopulationAmount) && habitatPrioritization.Priority >= Galaxy.HabitatColonizationThreshhold && DetermineColonizeLowQualityHabitat(habitatPrioritization.Habitat) && ship.WithinFuelRange(habitatPrioritization.Habitat.Xpos, habitatPrioritization.Habitat.Ypos, 0.0) && _ControlColonization == AutomationLevel.FullyAutomated)
                                {
                                    if (habitatPrioritization.AssignedShip != null)
                                    {
                                        habitatPrioritization.AssignedShip.Mission.Clear();
                                    }
                                    ship.AssignMission(BuiltObjectMissionType.Colonize, habitatPrioritization.Habitat, null, BuiltObjectMissionPriority.Normal);
                                    habitatPrioritization.AssignedShip = ship;
                                    break;
                                }
                            }
                            else
                            {
                                int newPopulationAmount2 = 0;
                                if (CanBuiltObjectColonizeHabitat(ship, habitatPrioritization.Habitat, out newPopulationAmount2) && habitatPrioritization.Priority >= Galaxy.HabitatColonizationThreshhold && DetermineColonizeLowQualityHabitat(habitatPrioritization.Habitat) && ship.WithinFuelRange(habitatPrioritization.Habitat.Xpos, habitatPrioritization.Habitat.Ypos, 0.0) && CheckColonizingHabitat(habitatPrioritization.Habitat) == null && CheckTaskAuthorized(_ControlColonization, ref refusalCount, GenerateAutomationMessageColonization(habitatPrioritization.Habitat, ship, null), habitatPrioritization.Habitat, AdvisorMessageType.Colonization, ship, null))
                                {
                                    ship.AssignMission(BuiltObjectMissionType.Colonize, habitatPrioritization.Habitat, null, BuiltObjectMissionPriority.Normal);
                                    habitatPrioritization.AssignedShip = ship;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                case BuiltObjectSubRole.Escort:
                case BuiltObjectSubRole.Frigate:
                case BuiltObjectSubRole.Destroyer:
                case BuiltObjectSubRole.Cruiser:
                case BuiltObjectSubRole.CapitalShip:
                case BuiltObjectSubRole.Carrier:
                    {
                        if (ship.IsPlanetDestroyer && ship.ShipGroup == null)
                        {
                            if (Galaxy.Rnd.Next(0, 2) == 1)
                            {
                                Habitat habitat12 = _Galaxy.FastFindNearestColony((int)ship.Xpos, (int)ship.Ypos, ship.Empire, 0);
                                if (habitat12 != null)
                                {
                                    _Galaxy.SelectRelativeParkingPoint(250.0, out var x6, out var y6);
                                    ship.AssignMission(BuiltObjectMissionType.Move, habitat12, null, x6, y6, BuiltObjectMissionPriority.Low);
                                }
                                break;
                            }
                            Habitat habitat13 = _Galaxy.FindNearestUncolonizedExploredSystem(ship.Xpos, ship.Ypos, ship.Empire);
                            if (habitat13 != null)
                            {
                                Habitat habitat14 = null;
                                HabitatList habitats = _Galaxy.Systems[habitat13.SystemIndex].Habitats;
                                if (habitats != null && habitats.Count > 0)
                                {
                                    habitat14 = habitats[Galaxy.Rnd.Next(0, habitats.Count)];
                                }
                                if (habitat14 != null)
                                {
                                    _Galaxy.SelectRelativeParkingPoint(250.0, out var x7, out var y7);
                                    ship.AssignMission(BuiltObjectMissionType.Move, habitat14, null, x7, y7, BuiltObjectMissionPriority.Low);
                                }
                            }
                            break;
                        }
                        if (_DangerousHabitats != null && _DangerousHabitats.Count > 0 && Galaxy.Rnd.Next(0, 2) == 1)
                        {
                            int index = Galaxy.Rnd.Next(0, _DangerousHabitats.Count);
                            Habitat habitat15 = _DangerousHabitats[index];
                            if (habitat15 != null && ship.WithinFuelRangeAndRefuel(habitat15.Xpos, habitat15.Ypos, 0.1) && _Galaxy.CheckMilitaryShipWelcomeAtTerritoryLocation(habitat15.Xpos, habitat15.Ypos, ship.Empire))
                            {
                                long starDate = _Galaxy.CurrentStarDate + (long)((double)Galaxy.RealSecondsInGalacticYear * 0.25 * 1000.0);
                                ship.AssignMission(BuiltObjectMissionType.MoveAndWait, habitat15, null, -2000000001.0, -2000000001.0, starDate, BuiltObjectMissionPriority.Normal, allowReprocessing: false);
                                _DangerousHabitats.Remove(habitat15);
                                break;
                            }
                        }
                        if (_IndependentColonyTargets.Count > 0)
                        {
                            for (int num51 = 0; num51 < _IndependentColonyTargets.Count; num51++)
                            {
                                HabitatPrioritization habitatPrioritization4 = _IndependentColonyTargets[num51];
                                if (habitatPrioritization4.AssignedShip == null && habitatPrioritization4.Habitat != null && _Galaxy.CheckMilitaryShipWelcomeAtTerritoryLocation(habitatPrioritization4.Habitat.Xpos, habitatPrioritization4.Habitat.Ypos, ship.Empire) && ship.WithinFuelRangeAndRefuel(habitatPrioritization4.Habitat.Xpos, habitatPrioritization4.Habitat.Ypos, 0.1))
                                {
                                    long starDate2 = _Galaxy.CurrentStarDate + (long)((double)Galaxy.RealSecondsInGalacticYear * 0.5 * 1000.0);
                                    ship.AssignMission(BuiltObjectMissionType.MoveAndWait, habitatPrioritization4.Habitat, null, -2000000001.0, -2000000001.0, starDate2, BuiltObjectMissionPriority.Low, allowReprocessing: false);
                                    habitatPrioritization4.AssignedShip = ship;
                                    return;
                                }
                            }
                        }
                        if (_EmpiresToAttack.Count > 0)
                        {
                            Habitat habitat16 = _Galaxy.FastFindNearestColony((int)ship.Xpos, (int)ship.Ypos, _EmpiresToAttack[0], 100);
                            if (habitat16 != null && CheckSystemExplored(habitat16.SystemIndex) && ship.WithinFuelRangeAndRefuel(habitat16.Xpos, habitat16.Ypos, 0.1))
                            {
                                ship.AssignMission(BuiltObjectMissionType.Move, habitat16, null, BuiltObjectMissionPriority.Normal);
                                break;
                            }
                        }
                        if (!Reclusive && ship.Troops != null && ship.TroopCapacity - ship.Troops.TotalSize >= 100 && Galaxy.Rnd.Next(0, 2) == 1 && AssignLoadTroopsMission(ship))
                        {
                            break;
                        }
                        int num52 = Galaxy.Rnd.Next(0, 8);
                        if ((ship.SubRole == BuiltObjectSubRole.Cruiser || ship.SubRole == BuiltObjectSubRole.CapitalShip || ship.SubRole == BuiltObjectSubRole.Carrier) && num52 < 3)
                        {
                            num52 = 4;
                        }
                        bool flag15 = CheckEmpireHasHyperDriveTech(this);
                        if (!flag15)
                        {
                            num52 = 4;
                        }
                        if (atWar)
                        {
                            num52 += 2;
                            num52 = Math.Min(num52, 7);
                        }
                        switch (num52)
                        {
                            case 0:
                            case 1:
                            case 2:
                                {
                                    for (int num61 = 0; num61 < BuiltObjects.Count; num61++)
                                    {
                                        BuiltObject builtObject17 = BuiltObjects[num61];
                                        if (builtObject17.IsColony && builtObject17.Mission != null && builtObject17.Mission.Type != 0 && builtObject17.CurrentEscortForceAssigned < builtObject17.Size / 50 && builtObject17 != ship && ship.WithinFuelRangeAndRefuel(builtObject17.Xpos, builtObject17.Ypos, 0.1) && (ship.WarpSpeed > 0 || builtObject17.WarpSpeed <= 0))
                                        {
                                            Point point2 = builtObject17.Mission.ResolveTargetCoordinates(builtObject17.Mission);
                                            if (_Galaxy.CheckMilitaryShipWelcomeAtTerritoryLocation(point2.X, point2.Y, ship.Empire))
                                            {
                                                ship.AssignMission(BuiltObjectMissionType.Escort, builtObject17, null, BuiltObjectMissionPriority.Normal);
                                                builtObject17.CurrentEscortForceAssigned += ship.FirepowerRaw;
                                                break;
                                            }
                                        }
                                    }
                                    for (int num62 = 0; num62 < BuiltObjects.Count; num62++)
                                    {
                                        BuiltObject builtObject18 = BuiltObjects[num62];
                                        if (builtObject18.SubRole == BuiltObjectSubRole.ConstructionShip && builtObject18.Mission != null && builtObject18.Mission.Type != 0 && builtObject18.CurrentEscortForceAssigned < builtObject18.Size / 10 && builtObject18 != ship && ship.WithinFuelRangeAndRefuel(builtObject18.Xpos, builtObject18.Ypos, 0.1) && (ship.WarpSpeed > 0 || builtObject18.WarpSpeed <= 0))
                                        {
                                            Point point3 = builtObject18.Mission.ResolveTargetCoordinates(builtObject18.Mission);
                                            if (_Galaxy.CheckMilitaryShipWelcomeAtTerritoryLocation(point3.X, point3.Y, ship.Empire))
                                            {
                                                ship.AssignMission(BuiltObjectMissionType.Escort, builtObject18, null, BuiltObjectMissionPriority.Normal);
                                                builtObject18.CurrentEscortForceAssigned += ship.FirepowerRaw;
                                                break;
                                            }
                                        }
                                    }
                                    for (int num63 = 0; num63 < ResourceExtractors.Count; num63++)
                                    {
                                        BuiltObject builtObject19 = ResourceExtractors[num63];
                                        if (builtObject19.IsResourceExtractor && builtObject19.Mission != null && builtObject19.Mission.Type != 0 && builtObject19.CurrentEscortForceAssigned < builtObject19.Size / 50 && builtObject19 != ship && ship.WithinFuelRangeAndRefuel(builtObject19.Xpos, builtObject19.Ypos, 0.1) && (ship.WarpSpeed > 0 || builtObject19.WarpSpeed <= 0))
                                        {
                                            Point point4 = builtObject19.Mission.ResolveTargetCoordinates(builtObject19.Mission);
                                            if (_Galaxy.CheckMilitaryShipWelcomeAtTerritoryLocation(point4.X, point4.Y, ship.Empire))
                                            {
                                                ship.AssignMission(BuiltObjectMissionType.Escort, builtObject19, null, BuiltObjectMissionPriority.Normal);
                                                builtObject19.CurrentEscortForceAssigned += ship.FirepowerRaw;
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                                {
                                    if (ship.Mission != null && ship.Mission.Type == BuiltObjectMissionType.Patrol)
                                    {
                                        break;
                                    }
                                    bool flag16 = false;
                                    if (flag15)
                                    {
                                        if (patrolMiningStations == null)
                                        {
                                            patrolMiningStations = ResolvePrioritizedPatrolMiningStations();
                                        }
                                        for (int num53 = 0; num53 < patrolMiningStations.Count; num53++)
                                        {
                                            BuiltObject builtObject15 = patrolMiningStations[num53];
                                            if (builtObject15 != null && builtObject15.CurrentEscortForceAssigned < (int)builtObject15.SortTag && ship.WithinFuelRangeAndRefuel(builtObject15.Xpos, builtObject15.Ypos, 0.1) && _Galaxy.CheckMilitaryShipWelcomeAtTerritoryLocation(builtObject15.Xpos, builtObject15.Ypos, ship.Empire))
                                            {
                                                ship.AssignMission(BuiltObjectMissionType.Patrol, builtObject15, null, BuiltObjectMissionPriority.Low);
                                                builtObject15.CurrentEscortForceAssigned += ship.FirepowerRaw;
                                                flag16 = true;
                                                break;
                                            }
                                        }
                                        if (flag16)
                                        {
                                            break;
                                        }
                                        int num54 = Galaxy.Rnd.Next(0, Colonies.Count);
                                        for (int num55 = num54; num55 < Colonies.Count; num55++)
                                        {
                                            Habitat habitat17 = Colonies[num55];
                                            if (habitat17.CurrentDefensiveForceAssigned < habitat17.EstimatedDefensiveForceRequired(atWar) && ship.WithinFuelRangeAndRefuel(habitat17.Xpos, habitat17.Ypos, 0.1))
                                            {
                                                ship.AssignMission(BuiltObjectMissionType.Patrol, habitat17, null, BuiltObjectMissionPriority.Low);
                                                habitat17.CurrentDefensiveForceAssigned += ship.FirepowerRaw;
                                                flag16 = true;
                                                break;
                                            }
                                        }
                                        if (flag16)
                                        {
                                            break;
                                        }
                                        for (int num56 = 0; num56 < num54; num56++)
                                        {
                                            Habitat habitat18 = Colonies[num56];
                                            if (habitat18.CurrentDefensiveForceAssigned < habitat18.EstimatedDefensiveForceRequired(atWar) && ship.WithinFuelRangeAndRefuel(habitat18.Xpos, habitat18.Ypos, 0.1))
                                            {
                                                ship.AssignMission(BuiltObjectMissionType.Patrol, habitat18, null, BuiltObjectMissionPriority.Low);
                                                habitat18.CurrentDefensiveForceAssigned += ship.FirepowerRaw;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                    int num57 = Galaxy.Rnd.Next(0, Colonies.Count);
                                    for (int num58 = num57; num58 < Colonies.Count; num58++)
                                    {
                                        Habitat habitat19 = Colonies[num58];
                                        if (habitat19 != null && habitat19.CurrentDefensiveForceAssigned < habitat19.EstimatedDefensiveForceRequired(atWar) && ship.WithinFuelRangeAndRefuel(habitat19.Xpos, habitat19.Ypos, 0.1))
                                        {
                                            ship.AssignMission(BuiltObjectMissionType.Patrol, habitat19, null, BuiltObjectMissionPriority.Low);
                                            habitat19.CurrentDefensiveForceAssigned += ship.FirepowerRaw;
                                            flag16 = true;
                                            break;
                                        }
                                    }
                                    if (!flag16)
                                    {
                                        for (int num59 = 0; num59 < num57; num59++)
                                        {
                                            Habitat habitat20 = Colonies[num59];
                                            if (habitat20 != null && habitat20.CurrentDefensiveForceAssigned < habitat20.EstimatedDefensiveForceRequired(atWar) && ship.WithinFuelRangeAndRefuel(habitat20.Xpos, habitat20.Ypos, 0.1))
                                            {
                                                ship.AssignMission(BuiltObjectMissionType.Patrol, habitat20, null, BuiltObjectMissionPriority.Low);
                                                habitat20.CurrentDefensiveForceAssigned += ship.FirepowerRaw;
                                                flag16 = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (flag16)
                                    {
                                        break;
                                    }
                                    if (patrolMiningStations == null)
                                    {
                                        patrolMiningStations = ResolvePrioritizedPatrolMiningStations();
                                    }
                                    for (int num60 = 0; num60 < patrolMiningStations.Count; num60++)
                                    {
                                        BuiltObject builtObject16 = patrolMiningStations[num60];
                                        if (builtObject16 != null && builtObject16.CurrentEscortForceAssigned < (int)builtObject16.SortTag && ship.WithinFuelRangeAndRefuel(builtObject16.Xpos, builtObject16.Ypos, 0.1) && _Galaxy.CheckMilitaryShipWelcomeAtTerritoryLocation(builtObject16.Xpos, builtObject16.Ypos, ship.Empire))
                                        {
                                            ship.AssignMission(BuiltObjectMissionType.Patrol, builtObject16, null, BuiltObjectMissionPriority.Low);
                                            builtObject16.CurrentEscortForceAssigned += ship.FirepowerRaw;
                                            flag16 = true;
                                            break;
                                        }
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case BuiltObjectSubRole.TroopTransport:
                    if (!CheckAssignUnloadTroopsAtColonyNeedingThemMission(ship) && !CheckAssignGarrisonTroopsAtPenalColonyMission(ship) && ship.Troops != null && ship.TroopCapacity - ship.Troops.TotalSize >= 100)
                    {
                        AssignLoadTroopsMission(ship);
                    }
                    break;
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                    {
                        bool flag6 = CheckShipCanSurviveStorms(ship);
                        List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
                        list.Add(BuiltObjectSubRole.MiningShip);
                        list.Add(BuiltObjectSubRole.GasMiningShip);
                        List<BuiltObjectSubRole> subRoles = list;
                        if (_EmpireResourceTargets != null && _EmpireResourceTargets.Count > 0)
                        {
                            bool flag7 = false;
                            int num13 = 0;
                            for (int iterationCount = 0; Galaxy.ConditionCheckLimit(!flag7 && num13 < _EmpireResourceTargets.Count, 1000, ref iterationCount); num13++)
                            {
                                _ = _EmpireResourceTargets[num13];
                                if (!ship.IsResourceExtractor)
                                {
                                    continue;
                                }
                                Habitat habitat4 = _EmpireResourceTargets[num13].Habitat;
                                if (!flag6 && _Galaxy.CheckInStorm(habitat4.Xpos, habitat4.Ypos))
                                {
                                    continue;
                                }
                                switch (habitat4.Type)
                                {
                                    case HabitatType.BarrenRock:
                                        if (ship.ExtractionMine > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionGas > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag7 = true;
                                        }
                                        break;
                                    case HabitatType.GasGiant:
                                    case HabitatType.FrozenGasGiant:
                                    case HabitatType.Hydrogen:
                                    case HabitatType.Helium:
                                    case HabitatType.Argon:
                                    case HabitatType.Ammonia:
                                    case HabitatType.CarbonDioxide:
                                    case HabitatType.Oxygen:
                                    case HabitatType.NitrogenOxygen:
                                    case HabitatType.Chlorine:
                                        if (ship.ExtractionGas > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionMine > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag7 = true;
                                        }
                                        break;
                                    case HabitatType.Volcanic:
                                        if (ship.ExtractionMine > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionGas > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag7 = true;
                                        }
                                        break;
                                    case HabitatType.Continental:
                                        if (ship.ExtractionMine > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionGas > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag7 = true;
                                        }
                                        break;
                                    case HabitatType.Ice:
                                        if (ship.ExtractionGas > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionMine > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag7 = true;
                                        }
                                        break;
                                    case HabitatType.MarshySwamp:
                                        if (ship.ExtractionMine > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionGas > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag7 = true;
                                        }
                                        break;
                                    case HabitatType.Ocean:
                                        if (ship.ExtractionMine > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionGas > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag7 = true;
                                        }
                                        break;
                                    case HabitatType.Desert:
                                        if (ship.ExtractionMine > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionGas > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag7 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat4.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag7 = true;
                                        }
                                        break;
                                }
                                if (!flag7)
                                {
                                    continue;
                                }
                                if (ship.WithinFuelRangeAndRefuel(habitat4.Xpos, habitat4.Ypos, 0.1))
                                {
                                    int num14 = PrivateBuiltObjects.CountBuiltObjectsWithTargetHabitat(habitat4, subRoles);
                                    if (num14 < 3)
                                    {
                                        ship.AssignMission(BuiltObjectMissionType.ExtractResources, habitat4, null, BuiltObjectMissionPriority.Normal);
                                        _EmpireResourceTargets.RemoveAt(num13);
                                    }
                                }
                                else
                                {
                                    flag7 = false;
                                }
                            }
                        }
                        if ((ship.Mission != null && ship.Mission.Type != 0) || _ResourceTargets == null || _ResourceTargets.Count <= 0)
                        {
                            break;
                        }
                        bool flag8 = false;
                        int num15 = 0;
                        for (int iterationCount2 = 0; Galaxy.ConditionCheckLimit(!flag8 && num15 < _ResourceTargets.Count, 1000, ref iterationCount2); num15++)
                        {
                            HabitatPrioritization habitatPrioritization2 = _ResourceTargets[num15];
                            if (!ship.IsResourceExtractor || CheckNearPirateBase(habitatPrioritization2.Habitat, habitatPrioritization2.Habitat.Xpos, habitatPrioritization2.Habitat.Ypos) || (!flag6 && _Galaxy.CheckInStorm(habitatPrioritization2.Habitat.Xpos, habitatPrioritization2.Habitat.Ypos)))
                            {
                                continue;
                            }
                            Habitat habitat5 = _ResourceTargets[num15].Habitat;
                            if (habitat5 == null || _Galaxy.CheckAlreadyHaveMiningStationAtHabitat(habitat5, this))
                            {
                                continue;
                            }
                            switch (habitat5.Type)
                            {
                                case HabitatType.BarrenRock:
                                    if (ship.ExtractionMine > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionGas > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag8 = true;
                                    }
                                    break;
                                case HabitatType.GasGiant:
                                case HabitatType.FrozenGasGiant:
                                case HabitatType.Hydrogen:
                                case HabitatType.Helium:
                                case HabitatType.Argon:
                                case HabitatType.Ammonia:
                                case HabitatType.CarbonDioxide:
                                case HabitatType.Oxygen:
                                case HabitatType.NitrogenOxygen:
                                case HabitatType.Chlorine:
                                    if (ship.ExtractionMine > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionGas > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag8 = true;
                                    }
                                    break;
                                case HabitatType.Volcanic:
                                    if (ship.ExtractionMine > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionGas > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag8 = true;
                                    }
                                    break;
                                case HabitatType.Continental:
                                    if (ship.ExtractionMine > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionGas > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag8 = true;
                                    }
                                    break;
                                case HabitatType.Ice:
                                    if (ship.ExtractionMine > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionGas > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag8 = true;
                                    }
                                    break;
                                case HabitatType.MarshySwamp:
                                    if (ship.ExtractionMine > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionGas > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag8 = true;
                                    }
                                    break;
                                case HabitatType.Ocean:
                                    if (ship.ExtractionMine > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionGas > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag8 = true;
                                    }
                                    break;
                                case HabitatType.Desert:
                                    if (ship.ExtractionMine > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionGas > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag8 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat5.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag8 = true;
                                    }
                                    break;
                            }
                            if (!flag8)
                            {
                                continue;
                            }
                            if (ship.WithinFuelRangeAndRefuel(habitat5.Xpos, habitat5.Ypos, 0.1))
                            {
                                int num16 = PrivateBuiltObjects.CountBuiltObjectsWithTargetHabitat(habitat5, subRoles);
                                if (num16 < 3)
                                {
                                    ship.AssignMission(BuiltObjectMissionType.ExtractResources, habitat5, null, BuiltObjectMissionPriority.Normal);
                                }
                            }
                            else
                            {
                                flag8 = false;
                            }
                        }
                        break;
                    }
                case BuiltObjectSubRole.ConstructionShip:
                    {
                        if (flag2)
                        {
                            if (Policy.BuildPlanetDestroyers)
                            {
                                Design design = Designs.FindNewestPlanetDestroyer();
                                if (design != null && CanBuildDesign(design))
                                {
                                    int num17 = ConstructionShips.CountConstructionShipsBuildingPlanetDestroyers();
                                    int num18 = Math.Min(5, Math.Max(2, (int)((double)ConstructionShips.Count * 0.3)));
                                    if (num17 < num18)
                                    {
                                        _Galaxy.SelectRelativePoint(2000000.0, out var x2, out var y2);
                                        Habitat habitat6 = _Galaxy.FindNearestLonelyHabitat(Capital.Xpos + x2, Capital.Ypos + y2, this);
                                        if (habitat6 != null)
                                        {
                                            _Galaxy.SelectRelativeParkingPoint(Math.Max(200.0, (double)habitat6.Diameter * 0.7), out x2, out y2);
                                            ship.AssignMission(BuiltObjectMissionType.Build, habitat6, null, design, x2, y2, BuiltObjectMissionPriority.High);
                                            break;
                                        }
                                    }
                                }
                            }
                            GalaxyLocation galaxyLocation = CheckWhetherAtLocation(ship.Xpos, ship.Ypos);
                            if (galaxyLocation != null && galaxyLocation.Type == GalaxyLocationType.DebrisField && Galaxy.Rnd.Next(0, 2) == 1)
                            {
                                if (galaxyLocation == null)
                                {
                                    for (int num19 = 0; num19 < KnownGalaxyLocations.Count; num19++)
                                    {
                                        if (KnownGalaxyLocations[num19].Type == GalaxyLocationType.DebrisField)
                                        {
                                            galaxyLocation = KnownGalaxyLocations[num19];
                                            break;
                                        }
                                    }
                                }
                                if (galaxyLocation != null)
                                {
                                    BuiltObject builtObject6 = SelectBestSalvageableShip(galaxyLocation);
                                    if (builtObject6 != null && ship.WithinFuelRangeAndRefuel(builtObject6.Xpos, builtObject6.Ypos, 0.1))
                                    {
                                        ship.AssignMission(BuiltObjectMissionType.Build, null, builtObject6, builtObject6.Xpos, builtObject6.Ypos, BuiltObjectMissionPriority.High);
                                        break;
                                    }
                                }
                            }
                            GalaxyLocationList galaxyLocationList = KnownGalaxyLocations.FindLocations(GalaxyLocationType.PlanetDestroyer);
                            if (galaxyLocationList.Count > 0)
                            {
                                for (int num20 = 0; num20 < galaxyLocationList.Count; num20++)
                                {
                                    BuiltObject relatedBuiltObject = galaxyLocationList[num20].RelatedBuiltObject;
                                    if (relatedBuiltObject == null || relatedBuiltObject.UnbuiltComponentCount <= 0 || relatedBuiltObject.BuiltAt != null || relatedBuiltObject.Empire != null || relatedBuiltObject.HasBeenDestroyed)
                                    {
                                        continue;
                                    }
                                    bool flag12 = false;
                                    for (int num21 = 0; num21 < ConstructionShips.Count; num21++)
                                    {
                                        BuiltObject builtObject7 = ConstructionShips[num21];
                                        if (builtObject7.Mission != null && (builtObject7.Mission.Type == BuiltObjectMissionType.Build || builtObject7.Mission.Type == BuiltObjectMissionType.BuildRepair || builtObject7.Mission.Type == BuiltObjectMissionType.Repair) && builtObject7.Mission.SecondaryTargetBuiltObject == relatedBuiltObject)
                                        {
                                            flag12 = true;
                                            break;
                                        }
                                    }
                                    if (!flag12 && ship.WithinFuelRangeAndRefuel(relatedBuiltObject.Xpos, relatedBuiltObject.Ypos, 0.1))
                                    {
                                        ship.AssignMission(BuiltObjectMissionType.Build, null, relatedBuiltObject, relatedBuiltObject.Xpos, relatedBuiltObject.Ypos, BuiltObjectMissionPriority.High);
                                        return;
                                    }
                                }
                            }
                        }
                        BuiltObjectList builtObjectList2 = new BuiltObjectList();
                        builtObjectList2.AddRange(BuiltObjects);
                        builtObjectList2.AddRange(PrivateBuiltObjects);
                        if (Galaxy.Rnd.Next(0, 2) == 1)
                        {
                            int num22 = Galaxy.Rnd.Next(0, builtObjectList2.Count);
                            for (int num23 = num22; num23 < builtObjectList2.Count; num23++)
                            {
                                BuiltObject builtObject8 = builtObjectList2[num23];
                                if (DetermineWhetherShouldRepair(ship, builtObject8) && ship.WithinFuelRangeAndRefuel(builtObject8.Xpos, builtObject8.Ypos, 0.1))
                                {
                                    ship.AssignMission(BuiltObjectMissionType.BuildRepair, null, builtObject8, BuiltObjectMissionPriority.Normal);
                                    return;
                                }
                            }
                            for (int num24 = 0; num24 < num22; num24++)
                            {
                                BuiltObject builtObject9 = builtObjectList2[num24];
                                if (DetermineWhetherShouldRepair(ship, builtObject9) && ship.WithinFuelRangeAndRefuel(builtObject9.Xpos, builtObject9.Ypos, 0.1))
                                {
                                    ship.AssignMission(BuiltObjectMissionType.BuildRepair, null, builtObject9, BuiltObjectMissionPriority.Normal);
                                    return;
                                }
                            }
                        }
                        if (_ResearchHabitats != null && _ResearchHabitats.Count > 0)
                        {
                            bool flag13 = false;
                            if (Policy.ResearchPriority < 1.0)
                            {
                                if (Galaxy.Rnd.Next(0, 2) == 1)
                                {
                                    flag13 = true;
                                }
                            }
                            else if (Policy.ResearchPriority == 1.0)
                            {
                                if (Galaxy.Rnd.Next(0, 4) > 0)
                                {
                                    flag13 = true;
                                }
                            }
                            else if (Policy.ResearchPriority > 1.0)
                            {
                                flag13 = true;
                            }
                            if (flag13)
                            {
                                Design weaponsResearchStation;
                                Design energyResearchStation;
                                Design highTechResearchStation;
                                Design design2 = AnalyzeNewResearchFacilities(out weaponsResearchStation, out energyResearchStation, out highTechResearchStation);
                                if (design2 != null)
                                {
                                    IndustryType industryType = IndustryType.Undefined;
                                    if (design2.SubRole == BuiltObjectSubRole.WeaponsResearchStation)
                                    {
                                        industryType = IndustryType.Weapon;
                                    }
                                    else if (design2.SubRole == BuiltObjectSubRole.EnergyResearchStation)
                                    {
                                        industryType = IndustryType.Energy;
                                    }
                                    else if (design2.SubRole == BuiltObjectSubRole.HighTechResearchStation)
                                    {
                                        industryType = IndustryType.HighTech;
                                    }
                                    double num25 = CalculateSupportCost(design2);
                                    double num26 = design2.CalculateCurrentPurchasePrice(_Galaxy);
                                    if (num26 <= StateMoney && num25 <= CalculateSpareAnnualRevenueComplete())
                                    {
                                        Habitat habitat7 = null;
                                        for (int num27 = 0; num27 < _ResearchHabitats.Count; num27++)
                                        {
                                            if (_ResearchHabitats[num27].ResearchBonusIndustry == industryType && ship.WithinFuelRangeAndRefuel(_ResearchHabitats[num27].Xpos, _ResearchHabitats[num27].Ypos, 0.0))
                                            {
                                                habitat7 = _ResearchHabitats[num27];
                                                break;
                                            }
                                        }
                                        if (habitat7 != null && ship.WithinFuelRangeAndRefuel(habitat7.Xpos, habitat7.Ypos, 0.0))
                                        {
                                            float num28 = (float)(int)habitat7.ResearchBonus / 100f;
                                            switch (habitat7.ResearchBonusIndustry)
                                            {
                                                case IndustryType.Weapon:
                                                    if (num28 > ResearchBonusWeapons)
                                                    {
                                                        design2 = weaponsResearchStation;
                                                    }
                                                    break;
                                                case IndustryType.Energy:
                                                    if (num28 > ResearchBonusEnergy)
                                                    {
                                                        design2 = energyResearchStation;
                                                    }
                                                    break;
                                                case IndustryType.HighTech:
                                                    if (num28 > ResearchBonusHighTech)
                                                    {
                                                        design2 = highTechResearchStation;
                                                    }
                                                    break;
                                            }
                                            double x3;
                                            double y3;
                                            if (habitat7.Category == HabitatCategoryType.Star)
                                            {
                                                double num29 = 0.0;
                                                Habitat habitat8 = null;
                                                for (int num30 = 0; num30 < _Galaxy.AsteroidFields.Count; num30++)
                                                {
                                                    HabitatList habitatList3 = _Galaxy.AsteroidFields[num30];
                                                    if (habitatList3.Count <= 0 || habitatList3[0].Parent != habitat7)
                                                    {
                                                        continue;
                                                    }
                                                    int num31 = 0;
                                                    while (habitat8 == null && num31 < 20)
                                                    {
                                                        habitat8 = habitatList3[Galaxy.Rnd.Next(0, habitatList3.Count)];
                                                        BuiltObject builtObject10 = _Galaxy.FindNearestBuiltObject((int)habitat8.Xpos, (int)habitat8.Ypos, BuiltObjectRole.Base);
                                                        if (builtObject10 != null)
                                                        {
                                                            double num32 = _Galaxy.CalculateDistance(habitat8.Xpos, habitat8.Ypos, builtObject10.Xpos, builtObject10.Ypos);
                                                            if (num32 < 200.0)
                                                            {
                                                                habitat8 = null;
                                                            }
                                                        }
                                                        num31++;
                                                    }
                                                }
                                                if (habitat8 != null)
                                                {
                                                    habitat7 = habitat8;
                                                    _Galaxy.SelectRelativeHabitatSurfacePoint(habitat7, out x3, out y3);
                                                }
                                                else
                                                {
                                                    num29 = habitat7.Diameter;
                                                    if (habitat7.Type == HabitatType.BlackHole)
                                                    {
                                                        num29 = (double)habitat7.Diameter * 0.7;
                                                    }
                                                    else if (habitat7.Type == HabitatType.SuperNova)
                                                    {
                                                        num29 = (double)habitat7.Diameter * 0.1;
                                                    }
                                                    else if (habitat7.Type == HabitatType.Neutron)
                                                    {
                                                        num29 = (double)habitat7.Diameter * 2.0;
                                                    }
                                                    _Galaxy.SelectRelativeParkingPoint(num29, out x3, out y3);
                                                }
                                            }
                                            else
                                            {
                                                _Galaxy.SelectRelativeHabitatSurfacePoint(habitat7, out x3, out y3);
                                            }
                                            if (!CheckResearchStationAtLocation(habitat7))
                                            {
                                                bool flag14 = false;
                                                DesignList designList = CheckBasesToBeBuiltAtHabitat(habitat7);
                                                if (designList != null && designList.Count > 0)
                                                {
                                                    for (int num33 = 0; num33 < designList.Count; num33++)
                                                    {
                                                        if (designList[num33].SubRole == BuiltObjectSubRole.EnergyResearchStation || designList[num33].SubRole == BuiltObjectSubRole.HighTechResearchStation || designList[num33].SubRole == BuiltObjectSubRole.WeaponsResearchStation)
                                                        {
                                                            flag14 = true;
                                                            break;
                                                        }
                                                    }
                                                }
                                                _ResearchHabitats.Remove(habitat7);
                                                if (!flag14)
                                                {
                                                    ship.AssignMission(BuiltObjectMissionType.Build, habitat7, null, design2, x3, y3, BuiltObjectMissionPriority.Normal);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        HabitatList habitatList4 = DetermineHabitatsBeingMinedIncludingBuildingMiningStations(includeMiningShips: false);
                        if (_ResourceTargets != null && _ResourceTargets.Count > 0 && BuildStrategicResourceSupply(ship, habitatList4))
                        {
                            break;
                        }
                        if (_ResortBaseBuildLocations != null && _ResortBaseBuildLocations.Count > 0 && Policy.EngageInTourism && flag2 && Galaxy.Rnd.Next(0, 2) == 1)
                        {
                            int num34 = Math.Min(20, 1 + Colonies.Count / 6);
                            num34 = (int)((double)num34 * Policy.TourismPriority);
                            if (ResortBases.Count < num34)
                            {
                                Design design3 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.ResortBase);
                                if (design3 != null)
                                {
                                    double num35 = CalculateSupportCost(design3);
                                    double num36 = design3.CalculateCurrentPurchasePrice(_Galaxy);
                                    if (num36 <= StateMoney && num35 <= CalculateSpareAnnualRevenueComplete() && AssignBuildResortBaseMissionToBuiltObject(ship, design3))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        if (_MonitoringHabitats != null && _MonitoringPoints != null && (_MonitoringHabitats.Count > 0 || _MonitoringPoints.Count > 0))
                        {
                            Design design4 = Designs.FindNewestCanBuild(BuiltObjectSubRole.MonitoringStation);
                            if (design4 != null && Galaxy.Rnd.Next(0, 2) == 1)
                            {
                                double num37 = CalculateSupportCost(design4);
                                double num38 = design4.CalculateCurrentPurchasePrice(_Galaxy);
                                if (num38 <= StateMoney && num37 <= CalculateSpareAnnualRevenueComplete())
                                {
                                    int num39 = Math.Min(40, Math.Max(1, Colonies.Count / 3));
                                    if (LongRangeScanners.Count < num39)
                                    {
                                        if (_MonitoringHabitats.Count > _MonitoringPoints.Count)
                                        {
                                            Habitat habitat9 = null;
                                            for (int num40 = 0; num40 < _MonitoringHabitats.Count; num40++)
                                            {
                                                Habitat habitat10 = _MonitoringHabitats[num40];
                                                if (!ship.WithinFuelRangeAndRefuel(habitat10.Xpos, habitat10.Ypos, 0.0))
                                                {
                                                    continue;
                                                }
                                                double num41 = double.MaxValue;
                                                BuiltObject builtObject11 = _Galaxy.FastFindNearestLongRangeScannerBase((int)habitat10.Xpos, (int)habitat10.Ypos, this);
                                                if (builtObject11 != null)
                                                {
                                                    num41 = _Galaxy.CalculateDistance(habitat10.Xpos, habitat10.Ypos, builtObject11.Xpos, builtObject11.Ypos);
                                                }
                                                if (num41 > (double)Galaxy.MaxSolarSystemSize * 2.1)
                                                {
                                                    DesignList designList2 = CheckBasesToBeBuiltAtHabitat(habitat10);
                                                    if (designList2 == null || designList2.Count <= 0)
                                                    {
                                                        habitat9 = habitat10;
                                                        break;
                                                    }
                                                }
                                            }
                                            if (habitat9 != null)
                                            {
                                                double x4;
                                                double y4;
                                                if (habitat9.Category == HabitatCategoryType.Star)
                                                {
                                                    _Galaxy.SelectRelativeParkingPoint(habitat9.Diameter, out x4, out y4);
                                                }
                                                else
                                                {
                                                    _Galaxy.SelectRelativeHabitatSurfacePoint(habitat9, out x4, out y4);
                                                }
                                                BuiltObject builtObject12 = _Galaxy.FindNearestBuiltObject((int)(habitat9.Xpos + x4), (int)(habitat9.Ypos + y4), BuiltObjectRole.Base);
                                                double num42 = double.MaxValue;
                                                if (builtObject12 != null)
                                                {
                                                    num42 = _Galaxy.CalculateDistance(habitat9.Xpos + x4, habitat9.Ypos + y4, builtObject12.Xpos, builtObject12.Ypos);
                                                }
                                                int num43 = 0;
                                                while (num42 < (double)Galaxy.MinimumDistanceBetweenBases)
                                                {
                                                    _Galaxy.SelectRelativeHabitatSurfacePoint(habitat9, out x4, out y4);
                                                    builtObject12 = _Galaxy.FindNearestBuiltObject((int)(habitat9.Xpos + x4), (int)(habitat9.Ypos + y4), BuiltObjectRole.Base);
                                                    num42 = _Galaxy.CalculateDistance(habitat9.Xpos + x4, habitat9.Ypos + y4, builtObject12.Xpos, builtObject12.Ypos);
                                                    num43++;
                                                    if (num43 > 5)
                                                    {
                                                        break;
                                                    }
                                                }
                                                ship.AssignMission(BuiltObjectMissionType.Build, habitat9, null, design4, x4, y4, BuiltObjectMissionPriority.Normal);
                                                _MonitoringHabitats.Remove(habitat9);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Point item2 = Point.Empty;
                                            for (int num44 = 0; num44 < _MonitoringPoints.Count; num44++)
                                            {
                                                Point point = _MonitoringPoints[num44];
                                                if (ship.WithinFuelRangeAndRefuel(point.X, point.Y, 0.0))
                                                {
                                                    double num45 = double.MaxValue;
                                                    BuiltObject builtObject13 = _Galaxy.FastFindNearestLongRangeScannerBase(point.X, point.Y, this);
                                                    if (builtObject13 != null)
                                                    {
                                                        num45 = _Galaxy.CalculateDistance(point.X, point.Y, builtObject13.Xpos, builtObject13.Ypos);
                                                    }
                                                    if (num45 > (double)Galaxy.MaxSolarSystemSize * 2.1)
                                                    {
                                                        item2 = point;
                                                        break;
                                                    }
                                                }
                                            }
                                            if (!item2.IsEmpty)
                                            {
                                                ship.AssignMission(BuiltObjectMissionType.Build, null, null, design4, _MonitoringPoints[0].X, _MonitoringPoints[0].Y, BuiltObjectMissionPriority.Normal);
                                                _MonitoringPoints.Remove(item2);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (!flag2 || _ResourceTargets == null || _ResourceTargets.Count <= 0)
                        {
                            break;
                        }
                        StellarObject stellarObject4 = _Galaxy.FastFindNearestSpacePort(ship.Xpos, ship.Ypos, this);
                        if (stellarObject4 == null)
                        {
                            stellarObject4 = ((PirateEmpireBaseHabitat != null) ? PirateEmpireBaseHabitat : Capital);
                        }
                        double privateAnnualCashflow = GetPrivateAnnualCashflow();
                        int num46 = 0;
                        Design design5 = null;
                        for (int iterationCount3 = 0; Galaxy.ConditionCheckLimit(design5 == null && num46 < _ResourceTargets.Count, 1000, ref iterationCount3); num46++)
                        {
                            HabitatPrioritization habitatPrioritization3 = _ResourceTargets[num46];
                            Habitat habitat11 = _ResourceTargets[num46].Habitat;
                            if (habitatList4.Contains(habitat11) || (habitat11.Empire != null && habitat11.Empire != _Galaxy.IndependentEmpire) || !habitat11.Resources.HasLuxuryResources() || stellarObject4 == null || !ship.DistanceWithinRange(stellarObject4.Xpos, stellarObject4.Ypos, habitat11.Xpos, habitat11.Ypos, 0.1))
                            {
                                continue;
                            }
                            if (habitat11.Resources.ContainsGroup(ResourceGroup.Gas))
                            {
                                design5 = Designs.FindNewestCanBuild(BuiltObjectSubRole.GasMiningStation);
                            }
                            if (habitat11.Resources.ContainsGroup(ResourceGroup.Mineral))
                            {
                                design5 = Designs.FindNewestCanBuild(BuiltObjectSubRole.MiningStation);
                            }
                            if (design5 == null && habitat11.Resources.ContainsGroup(ResourceGroup.Luxury))
                            {
                                design5 = Designs.FindNewestCanBuild(BuiltObjectSubRole.MiningStation);
                            }
                            if (design5 == null)
                            {
                                continue;
                            }
                            double num47 = design5.CalculateMaintenanceCosts(_Galaxy, this);
                            double num48 = design5.CalculateCurrentPurchasePrice(_Galaxy);
                            if (!(PrivateMoney > num48) || !(privateAnnualCashflow > num47) || habitatPrioritization3.Priority <= Galaxy.MiningStationResourceThreshhold || CheckNearPirateBase(habitatPrioritization3.Habitat, habitatPrioritization3.Habitat.Xpos, habitatPrioritization3.Habitat.Ypos))
                            {
                                continue;
                            }
                            _Galaxy.SelectRelativeHabitatSurfacePoint(habitat11, out var x5, out var y5);
                            BuiltObject builtObject14 = _Galaxy.FindNearestBuiltObject((int)(habitat11.Xpos + x5), (int)(habitat11.Ypos + y5), BuiltObjectRole.Base);
                            double num49 = double.MaxValue;
                            if (builtObject14 != null)
                            {
                                num49 = _Galaxy.CalculateDistance(habitat11.Xpos + x5, habitat11.Ypos + y5, builtObject14.Xpos, builtObject14.Ypos);
                            }
                            int num50 = 0;
                            while (num49 < (double)Galaxy.MinimumDistanceBetweenBases)
                            {
                                _Galaxy.SelectRelativeHabitatSurfacePoint(habitat11, out x5, out y5);
                                builtObject14 = _Galaxy.FindNearestBuiltObject((int)(habitat11.Xpos + x5), (int)(habitat11.Ypos + y5), BuiltObjectRole.Base);
                                num49 = _Galaxy.CalculateDistance(habitat11.Xpos + x5, habitat11.Ypos + y5, builtObject14.Xpos, builtObject14.Ypos);
                                num50++;
                                if (num50 > 5)
                                {
                                    break;
                                }
                            }
                            ship.AssignMission(BuiltObjectMissionType.Build, habitat11, null, design5, x5, y5, BuiltObjectMissionPriority.Normal);
                            habitatList4.Add(habitat11);
                            _ResourceTargets.RemoveAt(num46);
                        }
                        break;
                    }
                case BuiltObjectSubRole.PassengerShip:
                    {
                        bool flag9 = false;
                        bool flag10 = false;
                        if (_MigrationDestinations != null && _MigrationSources != null && _MigrationDestinations.Count > 0 && _MigrationSources.Count > 0 && flag2)
                        {
                            flag9 = true;
                        }
                        if (_TourismDestinations != null && _TourismSources != null && _TourismDestinations.Count > 0 && _TourismSources.Count > 0 && Policy.EngageInTourism && flag2)
                        {
                            flag10 = true;
                        }
                        if ((!flag9 || (flag10 && Galaxy.Rnd.Next(0, 2) != 1) || !AssignMigrationMissionToBuiltObject(ship)) && flag10)
                        {
                            bool flag11 = false;
                            if ((!flag9 || Galaxy.Rnd.Next(0, 3) > 0) && AssignTourismMissionToBuiltObject(ship))
                            {
                                flag11 = true;
                            }
                            else if (!flag11 && flag9 && !AssignMigrationMissionToBuiltObject(ship))
                            {
                            }
                        }
                        break;
                    }
                case BuiltObjectSubRole.ExplorationShip:
                    {
                        if (_SystemScouts == null)
                        {
                            _SystemScouts = new BuiltObjectList();
                        }
                        int num2 = Math.Max(1, (int)((double)_ExplorationShipCount * 0.38));
                        if (_ExplorationShipCount <= 1)
                        {
                            num2 = 0;
                        }
                        BuiltObjectList builtObjectList = new BuiltObjectList();
                        for (int i = 0; i < _SystemScouts.Count; i++)
                        {
                            BuiltObject builtObject = _SystemScouts[i];
                            if (builtObject == null || builtObject.HasBeenDestroyed || !builtObject.IsFunctional || builtObject.TopSpeed <= 0 || builtObject.WarpSpeed <= 0 || !builtObject.IsAutoControlled)
                            {
                                builtObjectList.Add(builtObject);
                            }
                            else if (builtObject != null && _SystemScouts.Count - builtObjectList.Count > num2)
                            {
                                builtObjectList.Add(builtObject);
                            }
                        }
                        for (int j = 0; j < builtObjectList.Count; j++)
                        {
                            BuiltObject builtObject2 = builtObjectList[j];
                            if (builtObject2 != null)
                            {
                                _SystemScouts.Remove(builtObject2);
                            }
                        }
                        if (_SystemScouts.Count < num2 && !_SystemScouts.Contains(ship) && ship != null && !ship.HasBeenDestroyed && ship.IsFunctional && ship.TopSpeed > 0 && ship.WarpSpeed > 0 && ship.IsAutoControlled)
                        {
                            _SystemScouts.Add(ship);
                        }
                        Point location = Point.Empty;
                        if (_SystemScouts.Contains(ship))
                        {
                            double num3 = (double)_SystemExploredCount / (double)_Galaxy.StarCount;
                            if (num3 > 0.015 && num3 < 0.4 && _ExplorationShipCount > 1)
                            {
                                Habitat habitat = _Galaxy.FindNextSystemToScout(this, ship, out location);
                                if (!location.IsEmpty)
                                {
                                    if (ship.WithinFuelRangeAndRefuel(location.X, location.Y, 0.0))
                                    {
                                        ship.AssignMission(BuiltObjectMissionType.Move, null, null, location.X, location.Y, BuiltObjectMissionPriority.Normal);
                                        ship.Mission.AddCommandToEnd(new Command(CommandAction.ReassignMission));
                                        break;
                                    }
                                }
                                else if (habitat != null && ship.WithinFuelRangeAndRefuel(habitat.Xpos, habitat.Ypos, 0.0))
                                {
                                    ship.AssignMission(BuiltObjectMissionType.Move, habitat, null, BuiltObjectMissionPriority.Normal);
                                    ship.Mission.AddCommandToEnd(new Command(CommandAction.ReassignMission));
                                    break;
                                }
                            }
                        }
                        bool flag3 = false;
                        location = Point.Empty;
                        Habitat habitat2 = _Galaxy.FindNextHabitatToExplore(ship.Xpos, ship.Ypos, ship.Empire, ship, out location);
                        if (location != Point.Empty)
                        {
                            if (ship.WithinFuelRangeAndRefuel(location.X, location.Y, 0.0))
                            {
                                ship.AssignMission(BuiltObjectMissionType.Move, null, null, location.X, location.Y, BuiltObjectMissionPriority.Normal);
                                flag3 = true;
                            }
                        }
                        else if (habitat2 != null && ship.WithinFuelRangeAndRefuel(habitat2.Xpos, habitat2.Ypos, 0.0))
                        {
                            ship.AssignMission(BuiltObjectMissionType.Explore, habitat2, null, BuiltObjectMissionPriority.Normal);
                            flag3 = true;
                        }
                        if (habitat2 == null && location.IsEmpty && Galaxy.Rnd.Next(0, 10) == 1)
                        {
                            GalaxyLocation location2 = null;
                            Habitat habitat3 = _Galaxy.FindUnexploredRuinsOrLocations(ship.Xpos, ship.Ypos, ship.Empire, out location2);
                            if (habitat3 != null)
                            {
                                ship.AssignMission(BuiltObjectMissionType.Move, habitat3, null, BuiltObjectMissionPriority.Normal);
                                flag3 = true;
                            }
                            else if (location2 != null)
                            {
                                location2.ResolveLocationCenter(out var x, out var y);
                                ship.AssignMission(BuiltObjectMissionType.Move, null, null, x, y, BuiltObjectMissionPriority.Normal);
                                flag3 = true;
                            }
                        }
                        if (flag3)
                        {
                            break;
                        }
                        double num4 = ship.CurrentFuel / (double)ship.FuelCapacity;
                        if (num4 < 0.9)
                        {
                            ResourceList fuelTypes = ship.DetermineFuelRequired();
                            StellarObject stellarObject = _Galaxy.FastFindNearestRefuellingPoint(ship.Xpos, ship.Ypos, fuelTypes, this, ship);
                            if (stellarObject != null)
                            {
                                ship.AssignMission(BuiltObjectMissionType.Refuel, stellarObject, null, BuiltObjectMissionPriority.Unavailable);
                            }
                        }
                        break;
                    }
                case BuiltObjectSubRole.ResupplyShip:
                    break;
            }
        }

        public PrioritizedTargetList DetermineResortBaseBuildLocations()
        {
            PrioritizedTargetList prioritizedTargetList = new PrioritizedTargetList();
            bool flag = CheckConstructionShipAndMiningStationCanSurviveStorms();
            for (int i = 0; i < _Galaxy.Systems.Count; i++)
            {
                SystemInfo systemInfo = _Galaxy.Systems[i];
                if (!systemInfo.HasScenery || !CheckSystemExplored(systemInfo.SystemStar))
                {
                    continue;
                }
                HabitatList habitatList = new HabitatList();
                habitatList.Add(systemInfo.SystemStar);
                habitatList.AddRange(systemInfo.Habitats);
                for (int j = 0; j < habitatList.Count; j++)
                {
                    Habitat habitat = habitatList[j];
                    double num = habitat.CalculateScenicFactorIncludingRuinsWonders();
                    if (!(num > 0.0) || (!flag && _Galaxy.CheckInStorm(habitat.Xpos, habitat.Ypos)) || !_Galaxy.CheckEmpireTerritoryCanBuildAtHabitat(this, habitat))
                    {
                        continue;
                    }
                    if (habitat.Category == HabitatCategoryType.Star || habitat.Category == HabitatCategoryType.GasCloud)
                    {
                        bool flag2 = true;
                        for (int k = 0; k < _TourismDestinations.Count; k++)
                        {
                            PrioritizedTarget prioritizedTarget = _TourismDestinations[k];
                            if (prioritizedTarget.Target is BuiltObject)
                            {
                                BuiltObject builtObject = (BuiltObject)prioritizedTarget.Target;
                                if (builtObject.NearestSystemStar == habitat)
                                {
                                    flag2 = false;
                                    break;
                                }
                            }
                        }
                        if (_Galaxy.DetermineResortBaseAtHabitat(habitat) != null)
                        {
                            flag2 = false;
                        }
                        if (flag2)
                        {
                            prioritizedTargetList.Add(new PrioritizedTarget(habitat, (int)(num * 1000.0)));
                        }
                    }
                    else if ((habitat.Empire == null || habitat.Empire == _Galaxy.IndependentEmpire) && habitat.BasesAtHabitat.CountBySubRole(BuiltObjectSubRole.ResortBase) == 0 && !_Galaxy.CheckForeignBaseAtHabitat(habitat, this))
                    {
                        prioritizedTargetList.Add(new PrioritizedTarget(habitat, (int)(num * 1000.0)));
                    }
                }
            }
            return prioritizedTargetList;
        }

        private bool AssignBuildResortBaseMissionToBuiltObject(BuiltObject builtObject, Design resortBaseDesign)
        {
            if (builtObject.SubRole == BuiltObjectSubRole.ConstructionShip && resortBaseDesign != null && Policy.EngageInTourism && _ResortBaseBuildLocations.Count > 0 && _TourismSources.Count > 0)
            {
                int maxValue = Math.Min(5, _TourismSources.Count);
                int index = Galaxy.Rnd.Next(0, maxValue);
                Habitat habitat = (Habitat)_TourismSources[index].Target;
                PrioritizedTarget prioritizedTarget = null;
                HabitatPrioritizationList habitatPrioritizationList = DetermineHabitatsBuildingMiningStations();
                double num = (double)Galaxy.SectorSize * 3.0;
                double num2 = 0.0;
                for (int i = 0; i < 10 && i < _ResortBaseBuildLocations.Count; i++)
                {
                    double num3 = 0.0;
                    double num4 = 0.0;
                    Habitat habitat2 = null;
                    if (_ResortBaseBuildLocations[i].Target is Habitat)
                    {
                        habitat2 = (Habitat)_ResortBaseBuildLocations[i].Target;
                        num3 = habitat2.Xpos;
                        num4 = habitat2.Ypos;
                    }
                    double num5 = _Galaxy.CalculateDistance(num3, num4, habitat.Xpos, habitat.Ypos);
                    if (!(num5 <= num))
                    {
                        continue;
                    }
                    double num6 = Math.Sqrt(num - num5) * (double)_ResortBaseBuildLocations[i].Priority;
                    num6 *= 0.9 + Galaxy.Rnd.NextDouble() * 0.2;
                    if (!(num6 > num2))
                    {
                        continue;
                    }
                    bool flag = false;
                    if (habitat2 != null)
                    {
                        int num7 = habitatPrioritizationList.IndexOf(habitat2);
                        if (num7 >= 0)
                        {
                            flag = true;
                        }
                    }
                    if (!flag && builtObject.WithinFuelRangeAndRefuel(num3, num4, 0.1))
                    {
                        num6 = num2;
                        prioritizedTarget = _ResortBaseBuildLocations[i];
                    }
                }
                if (prioritizedTarget != null && prioritizedTarget.Target is Habitat)
                {
                    Habitat habitat3 = (Habitat)prioritizedTarget.Target;
                    double x;
                    double y;
                    if (habitat3.Category == HabitatCategoryType.Star)
                    {
                        double num8 = 0.0;
                        num8 = habitat3.Diameter;
                        if (habitat3.Type == HabitatType.BlackHole)
                        {
                            num8 = (double)habitat3.Diameter * 0.7;
                        }
                        else if (habitat3.Type == HabitatType.SuperNova)
                        {
                            num8 = (double)habitat3.Diameter * 0.1;
                        }
                        else if (habitat3.Type == HabitatType.Neutron)
                        {
                            num8 = (double)habitat3.Diameter * 2.0;
                        }
                        _Galaxy.SelectRelativeParkingPoint(num8, out x, out y);
                    }
                    else
                    {
                        _Galaxy.SelectRelativeHabitatSurfacePoint(habitat3, out x, out y);
                    }
                    builtObject.ClearPreviousMissionRequirements();
                    builtObject.AssignMission(BuiltObjectMissionType.Build, habitat3, null, resortBaseDesign, x, y, BuiltObjectMissionPriority.Normal);
                    _ResortBaseBuildLocations.Remove(prioritizedTarget);
                    return true;
                }
            }
            return false;
        }

        private bool AssignMigrationMissionToBuiltObject(BuiltObject builtObject)
        {
            if (builtObject.SubRole == BuiltObjectSubRole.PassengerShip)
            {
                int num = 0;
                if (_ResettleSources.Count > 0 && _MigrationSources.Count > 0 && _MigrationDestinations.Count > 0)
                {
                    num = Galaxy.Rnd.Next(0, 2);
                }
                else if (_MigrationSources.Count > 0 && _MigrationDestinations.Count > 0)
                {
                    num = 0;
                }
                else if (_ResettleSources.Count > 0)
                {
                    num = 1;
                }
                if (num == 1 && _ResettleSources.Count > 0)
                {
                    int index = Galaxy.Rnd.Next(0, _ResettleSources.Count);
                    Habitat habitat = null;
                    if (_ResettleSources[index].Target is Habitat)
                    {
                        habitat = (Habitat)_ResettleSources[index].Target;
                    }
                    if (habitat != null && builtObject.WithinFuelRangeAndRefuel(habitat.Xpos, habitat.Ypos, 0.0))
                    {
                        Race race = null;
                        long amount = 0L;
                        if (habitat.HasPopulationToResettle(out race, out amount))
                        {
                            Habitat habitat2 = DetermineResettleDestination(race, builtObject, habitat);
                            if (habitat2 != null)
                            {
                                long amount2 = Math.Min(builtObject.PopulationCapacity, amount);
                                PopulationList populationList = new PopulationList();
                                populationList.Add(new Population(race, amount2));
                                builtObject.AssignMission(BuiltObjectMissionType.Transport, habitat, habitat2, populationList, BuiltObjectMissionPriority.Normal);
                                return true;
                            }
                        }
                    }
                }
                else if (num == 0 && _MigrationDestinations.Count > 0 && _MigrationSources.Count > 0)
                {
                    PopulationList populationList2 = new PopulationList();
                    int index2 = Galaxy.Rnd.Next(0, _MigrationDestinations.Count);
                    Habitat habitat3 = (Habitat)_MigrationDestinations[index2].Target;
                    PrioritizedTarget prioritizedTarget = null;
                    double num2 = (double)Galaxy.SectorSize * 3.0;
                    double num3 = 0.0;
                    for (int i = 0; i < 10 && i < _MigrationSources.Count; i++)
                    {
                        double num4 = 0.0;
                        double num5 = 0.0;
                        if (_MigrationSources[i].Target is Habitat)
                        {
                            Habitat habitat4 = (Habitat)_MigrationSources[i].Target;
                            num4 = habitat4.Xpos;
                            num5 = habitat4.Ypos;
                            if (habitat4.Population != null && habitat4.Population.DominantRace != null && !habitat3.AcceptsPopulation(builtObject.Empire, habitat4.Population.DominantRace))
                            {
                                continue;
                            }
                        }
                        if (!builtObject.WithinFuelRangeAndRefuel(num4, num5, 0.0))
                        {
                            continue;
                        }
                        double num6 = _Galaxy.CalculateDistance(num4, num5, habitat3.Xpos, habitat3.Ypos);
                        if (num6 <= num2)
                        {
                            double num7 = Math.Sqrt(num2 - num6) * (double)_MigrationSources[i].Priority;
                            num7 *= 0.75 + Galaxy.Rnd.NextDouble() * 0.5;
                            if (num7 < num3)
                            {
                                num7 = num3;
                                prioritizedTarget = _MigrationSources[i];
                            }
                        }
                    }
                    if (prioritizedTarget != null && prioritizedTarget.Target is Habitat)
                    {
                        Habitat habitat5 = (Habitat)prioritizedTarget.Target;
                        if (habitat5 != null && habitat5.Population != null && habitat5.Population.Count > 0 && habitat5.Population.DominantRace != null)
                        {
                            Race dominantRace = habitat5.Population.DominantRace;
                            long amount3 = Math.Min(builtObject.PopulationCapacity, habitat5.Population[dominantRace].Amount / 50);
                            populationList2.Add(new Population(dominantRace, amount3));
                            builtObject.AssignMission(BuiltObjectMissionType.Transport, habitat5, habitat3, populationList2, BuiltObjectMissionPriority.Normal);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool AssignTourismMissionToBuiltObject(BuiltObject builtObject)
        {
            if (builtObject.SubRole == BuiltObjectSubRole.PassengerShip && _TourismDestinations.Count > 0 && _TourismSources.Count > 0)
            {
                PopulationList populationList = new PopulationList();
                int index = Galaxy.Rnd.Next(0, _TourismSources.Count);
                Habitat habitat = (Habitat)_TourismSources[index].Target;
                PrioritizedTarget prioritizedTarget = null;
                if (!habitat.HasBeenDestroyed)
                {
                    double num = (double)Galaxy.SectorSize * 5.0;
                    double num2 = 0.0;
                    for (int i = 0; i < 50 && i < _TourismDestinations.Count; i++)
                    {
                        StellarObject stellarObject = null;
                        if (_TourismDestinations[i].Target is BuiltObject)
                        {
                            stellarObject = (BuiltObject)_TourismDestinations[i].Target;
                        }
                        else if (_TourismDestinations[i].Target is Habitat)
                        {
                            stellarObject = (Habitat)_TourismDestinations[i].Target;
                        }
                        if (stellarObject == null || !builtObject.WithinFuelRangeAndRefuel(stellarObject.Xpos, stellarObject.Ypos, 0.0))
                        {
                            continue;
                        }
                        double num3 = _Galaxy.CalculateDistance(stellarObject.Xpos, stellarObject.Ypos, habitat.Xpos, habitat.Ypos);
                        if (!(num3 <= num))
                        {
                            continue;
                        }
                        bool flag = true;
                        if (stellarObject.DockingBays.CountDocked >= stellarObject.DockingBays.Count)
                        {
                            flag = false;
                        }
                        if (flag)
                        {
                            double num4 = (Math.Sqrt(num) - Math.Sqrt(num3)) * Math.Sqrt(_TourismDestinations[i].Priority);
                            num4 *= 0.1 + Galaxy.Rnd.NextDouble() * 1.8;
                            if (num4 > num2)
                            {
                                num2 = num4;
                                prioritizedTarget = _TourismDestinations[i];
                            }
                        }
                    }
                    if (prioritizedTarget != null)
                    {
                        if (prioritizedTarget.Target is BuiltObject)
                        {
                            BuiltObject builtObject2 = (BuiltObject)prioritizedTarget.Target;
                            if (builtObject2.IsFunctional && builtObject2.PopulationCapacity > 0)
                            {
                                Race dominantRace = habitat.Population.DominantRace;
                                if (dominantRace != null)
                                {
                                    long val = Math.Min(builtObject.PopulationCapacity / 100, habitat.Population[dominantRace].Amount / 50);
                                    val = Math.Min(20000L, val);
                                    populationList.Add(new Population(dominantRace, val));
                                    builtObject.ClearPreviousMissionRequirements();
                                    builtObject.AssignMission(BuiltObjectMissionType.Transport, habitat, builtObject2, populationList, BuiltObjectMissionPriority.Normal);
                                    return true;
                                }
                            }
                        }
                        else if (prioritizedTarget.Target is Habitat)
                        {
                            Habitat target = (Habitat)prioritizedTarget.Target;
                            Race dominantRace2 = habitat.Population.DominantRace;
                            if (dominantRace2 != null)
                            {
                                long val2 = Math.Min(builtObject.PopulationCapacity / 100, habitat.Population[dominantRace2].Amount / 50);
                                val2 = Math.Min(20000L, val2);
                                populationList.Add(new Population(dominantRace2, val2));
                                builtObject.ClearPreviousMissionRequirements();
                                builtObject.AssignMission(BuiltObjectMissionType.Transport, habitat, target, populationList, BuiltObjectMissionPriority.Normal);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void ReviewMigrationTourism()
        {
            _ResettleSources = DetermineResettleSources();
            _MigrationDestinations = DetermineMigrationDestinations();
            _MigrationSources = DetermineMigrationSources();
            _TourismDestinations = DetermineTourismDestinations();
            _TourismSources = DetermineTourismSources();
            _ResortBaseBuildLocations = DetermineResortBaseBuildLocations();
        }

        private PrioritizedTargetList DetermineTourismDestinations()
        {
            PrioritizedTargetList prioritizedTargetList = new PrioritizedTargetList();
            if (Policy.EngageInTourism)
            {
                bool flag = CheckPassengerShipsCanSurviveStorms();
                for (int i = 0; i < _Galaxy.Empires.Count; i++)
                {
                    Empire empire = _Galaxy.Empires[i];
                    DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                    if (empire != this && (diplomaticRelation.Type == DiplomaticRelationType.NotMet || diplomaticRelation.Type == DiplomaticRelationType.War || diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions))
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
                        Habitat systemStar = Galaxy.DetermineHabitatSystemStar(habitat);
                        if (!CheckSystemExplored(systemStar))
                        {
                            continue;
                        }
                        int num = 0;
                        double num2 = habitat.CalculateScenicFactorIncludingRuinsWonders();
                        if (num2 > 0.0)
                        {
                            if (flag || !_Galaxy.CheckInStorm(habitat.Xpos, habitat.Ypos))
                            {
                                num = (int)(num2 * 1000.0);
                            }
                            if (num > 0)
                            {
                                prioritizedTargetList.Add(new PrioritizedTarget(habitat, num));
                            }
                        }
                    }
                    for (int k = 0; k < empire.ResortBases.Count; k++)
                    {
                        BuiltObject builtObject = empire.ResortBases[k];
                        if (!builtObject.IsFunctional || builtObject.PopulationCapacity <= 0 || builtObject.NearestSystemStar == null || !CheckSystemExplored(builtObject.NearestSystemStar))
                        {
                            continue;
                        }
                        int num3 = 0;
                        if (builtObject.ParentHabitat != null)
                        {
                            double num4 = builtObject.ParentHabitat.CalculateScenicFactorIncludingRuinsWonders();
                            if (num4 > 0.0 && (flag || !_Galaxy.CheckInStorm(builtObject.Xpos, builtObject.Ypos)))
                            {
                                num3 = (int)(num4 * 1000.0);
                            }
                        }
                        else if (builtObject.NearestSystemStar.ScenicFactor > 0f && (flag || !_Galaxy.CheckInStorm(builtObject.Xpos, builtObject.Ypos)))
                        {
                            num3 = (int)((double)builtObject.NearestSystemStar.ScenicFactor * 1000.0);
                        }
                        if (num3 > 0)
                        {
                            prioritizedTargetList.Add(new PrioritizedTarget(builtObject, num3));
                        }
                    }
                }
                for (int l = 0; l < _Galaxy.PirateEmpires.Count; l++)
                {
                    Empire empire2 = _Galaxy.PirateEmpires[l];
                    if (empire2 == null || empire2.ResortBases == null)
                    {
                        continue;
                    }
                    for (int m = 0; m < empire2.ResortBases.Count; m++)
                    {
                        BuiltObject builtObject2 = empire2.ResortBases[m];
                        if (builtObject2 == null || !builtObject2.IsFunctional || builtObject2.PopulationCapacity <= 0 || builtObject2.NearestSystemStar == null || !CheckSystemExplored(builtObject2.NearestSystemStar))
                        {
                            continue;
                        }
                        int num5 = 0;
                        if (builtObject2.ParentHabitat != null)
                        {
                            double num6 = builtObject2.ParentHabitat.CalculateScenicFactorIncludingRuinsWonders();
                            if (num6 > 0.0 && (flag || !_Galaxy.CheckInStorm(builtObject2.Xpos, builtObject2.Ypos)))
                            {
                                num5 = (int)(num6 * 1000.0);
                            }
                        }
                        else if (builtObject2.NearestSystemStar.ScenicFactor > 0f && (flag || !_Galaxy.CheckInStorm(builtObject2.Xpos, builtObject2.Ypos)))
                        {
                            num5 = (int)((double)builtObject2.NearestSystemStar.ScenicFactor * 1000.0);
                        }
                        if (num5 > 0)
                        {
                            prioritizedTargetList.Add(new PrioritizedTarget(builtObject2, num5));
                        }
                    }
                }
                prioritizedTargetList.Sort();
                prioritizedTargetList.Reverse();
            }
            return prioritizedTargetList;
        }

        private PrioritizedTargetList DetermineTourismSources()
        {
            PrioritizedTargetList prioritizedTargetList = new PrioritizedTargetList();
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat != null && habitat.DevelopmentLevel >= 70 && habitat.Population.TotalAmount >= 300000000)
                {
                    int priority = habitat.DevelopmentLevel * (int)(habitat.Population.TotalAmount / 10000000);
                    prioritizedTargetList.Add(new PrioritizedTarget(habitat, priority));
                }
            }
            if (PirateEmpireBaseHabitat != null)
            {
                for (int j = 0; j < _Galaxy.IndependentColonies.Count; j++)
                {
                    Habitat habitat2 = _Galaxy.IndependentColonies[j];
                    if (habitat2 != null && CheckSystemExplored(habitat2.SystemIndex))
                    {
                        int priority2 = habitat2.DevelopmentLevel * (int)(habitat2.Population.TotalAmount / 10000000);
                        prioritizedTargetList.Add(new PrioritizedTarget(habitat2, priority2));
                    }
                }
            }
            prioritizedTargetList.Sort();
            prioritizedTargetList.Reverse();
            return prioritizedTargetList;
        }

        private PrioritizedTargetList DetermineMigrationSources()
        {
            PrioritizedTargetList prioritizedTargetList = new PrioritizedTargetList();
            bool flag = CheckPassengerShipsCanSurviveStorms();
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                if ((empire != this && (diplomaticRelation.Type == DiplomaticRelationType.NotMet || diplomaticRelation.Type == DiplomaticRelationType.War || diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions)) || empire.DominantRace == null || !empire.DominantRace.Expanding)
                {
                    continue;
                }
                for (int j = 0; j < empire.Colonies.Count; j++)
                {
                    Habitat habitat = empire.Colonies[j];
                    if (CheckSystemExplored(habitat.SystemIndex) && habitat.MigrationFactor < 0f && (flag || !_Galaxy.CheckInStorm(habitat.Xpos, habitat.Ypos)))
                    {
                        prioritizedTargetList.Add(new PrioritizedTarget(habitat, (int)((double)habitat.MigrationFactor * 1000.0)));
                    }
                }
            }
            for (int k = 0; k < _Galaxy.IndependentColonies.Count; k++)
            {
                Habitat habitat2 = _Galaxy.IndependentColonies[k];
                if (CheckSystemExplored(habitat2.SystemIndex) && habitat2.MigrationFactor < 0f && (flag || !_Galaxy.CheckInStorm(habitat2.Xpos, habitat2.Ypos)))
                {
                    prioritizedTargetList.Add(new PrioritizedTarget(habitat2, (int)((double)habitat2.MigrationFactor * 1000.0)));
                }
            }
            for (int l = 0; l < _Galaxy.PirateEmpires.Count; l++)
            {
                Empire empire2 = _Galaxy.PirateEmpires[l];
                if (empire2 == null)
                {
                    continue;
                }
                PirateRelation pirateRelation = ObtainPirateRelation(empire2);
                if ((empire2 != this && pirateRelation.Type != PirateRelationType.Protection) || empire2.DominantRace == null || !empire2.DominantRace.Expanding)
                {
                    continue;
                }
                for (int m = 0; m < empire2.Colonies.Count; m++)
                {
                    Habitat habitat3 = empire2.Colonies[m];
                    if (habitat3 != null && habitat3.Empire == empire2 && CheckSystemExplored(habitat3.SystemIndex) && habitat3.MigrationFactor < 0f && (flag || !_Galaxy.CheckInStorm(habitat3.Xpos, habitat3.Ypos)))
                    {
                        prioritizedTargetList.Add(new PrioritizedTarget(habitat3, (int)((double)habitat3.MigrationFactor * 1000.0)));
                    }
                }
            }
            prioritizedTargetList.Sort();
            return prioritizedTargetList;
        }

        public Habitat DetermineResettleDestination(Race race, BuiltObject passengerShip, Habitat sourceColony)
        {
            return BaconEmpire.DetermineResettleDestination(this, race, passengerShip, sourceColony);
        }

        private PrioritizedTargetList DetermineResettleSources()
        {
            PrioritizedTargetList prioritizedTargetList = new PrioritizedTargetList();
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat != null && habitat.Empire == this)
                {
                    Race race = null;
                    long amount = 0L;
                    if (habitat.HasPopulationToResettle(out race, out amount))
                    {
                        prioritizedTargetList.Add(new PrioritizedTarget(habitat, (int)(amount / 1000000)));
                    }
                }
            }
            prioritizedTargetList.Sort();
            prioritizedTargetList.Reverse();
            return prioritizedTargetList;
        }

        private PrioritizedTargetList DetermineMigrationDestinations()
        {
            return BaconEmpire.DetermineMigrationDestinations(this);
        }

        private bool DetermineWhetherShouldRepair(BuiltObject repairShip, BuiltObject builtObject)
        {
            if (builtObject.UnbuiltOrDamagedComponentCount > 0 && builtObject.Role == BuiltObjectRole.Base && builtObject.BuiltAt == null)
            {
                if (builtObject.ParentHabitat != null && builtObject.ParentHabitat.Owner == this)
                {
                    return false;
                }
                if (CheckTargetOfRepairMission(builtObject))
                {
                    return false;
                }
                if (CheckNearPirateBase(repairShip, builtObject.Xpos, builtObject.Ypos))
                {
                    return false;
                }
                if (!CheckShipCanSurviveStorms(repairShip) && _Galaxy.CheckInStorm(builtObject.Xpos, builtObject.Ypos))
                {
                    return false;
                }
                return true;
            }
            if (builtObject.DamagedComponentCount > 0 && builtObject.Role != BuiltObjectRole.Base && builtObject.BuiltAt == null && builtObject.WarpSpeed <= 0 && builtObject.CurrentSpeed <= 0f && (builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined))
            {
                if (CheckTargetOfRepairMission(builtObject))
                {
                    return false;
                }
                if (CheckNearPirateBase(repairShip, builtObject.Xpos, builtObject.Ypos))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool CheckTargetOfRepairMission(BuiltObject target)
        {
            for (int i = 0; i < ConstructionShips.Count; i++)
            {
                BuiltObject builtObject = ConstructionShips[i];
                if (builtObject.Mission != null)
                {
                    if (builtObject.Mission.Type == BuiltObjectMissionType.BuildRepair && builtObject.Mission.SecondaryTargetBuiltObject == target)
                    {
                        return true;
                    }
                    if (builtObject.Mission.Type == BuiltObjectMissionType.Build && builtObject.Mission.SecondaryTargetBuiltObject == target)
                    {
                        return true;
                    }
                }
                if (builtObject.SubsequentMissions == null || builtObject.SubsequentMissions.Count <= 0)
                {
                    continue;
                }
                for (int j = 0; j < builtObject.SubsequentMissions.Count; j++)
                {
                    BuiltObjectMission builtObjectMission = builtObject.SubsequentMissions[j];
                    if (builtObjectMission.Type == BuiltObjectMissionType.BuildRepair && builtObjectMission.SecondaryTargetBuiltObject == target)
                    {
                        return true;
                    }
                    if (builtObjectMission.Type == BuiltObjectMissionType.Build && builtObjectMission.SecondaryTargetBuiltObject == target)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckNearPirateBase(BuiltObject ship, double x, double y)
        {
            if (ship != null)
            {
                int scanRange = Math.Max(ship.SensorProximityArrayRange, (int)((double)Galaxy.MaxSolarSystemSize * 2.1));
                return CheckNearPirateBase(ship, scanRange, x, y);
            }
            return false;
        }

        public bool CheckNearPirateBase(Habitat habitat, double x, double y)
        {
            int scanRange = (int)((double)Galaxy.MaxSolarSystemSize * 2.1);
            return CheckNearPirateBase(habitat, scanRange, x, y);
        }

        private bool CheckNearPirateBase(StellarObject stellarObject, int scanRange, double x, double y)
        {
            return CheckNearPirateBase(stellarObject, scanRange, x, y, null);
        }

        private bool CheckNearPirateBase(StellarObject stellarObject, double x, double y, Empire empireToExclude)
        {
            int scanRange = (int)((double)Galaxy.MaxSolarSystemSize * 2.1);
            return CheckNearPirateBase(stellarObject, scanRange, x, y, empireToExclude);
        }

        private bool CheckNearPirateBase(StellarObject stellarObject, int scanRange, double x, double y, Empire empireToExclude)
        {
            Empire empire = _Galaxy.FindNearestPirateFaction(x, y, empireToExclude, includeSuperPirates: true);
            if (empire != null && empire.PirateEmpireBaseHabitat != null)
            {
                BuiltObject builtObject = null;
                if (empire.PirateEmpireBaseHabitat.BasesAtHabitat != null && empire.PirateEmpireBaseHabitat.BasesAtHabitat.Count > 0)
                {
                    for (int i = 0; i < empire.PirateEmpireBaseHabitat.BasesAtHabitat.Count; i++)
                    {
                        BuiltObject builtObject2 = empire.PirateEmpireBaseHabitat.BasesAtHabitat[i];
                        if (builtObject2 != null && builtObject2.Empire == empire && (builtObject2.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject2.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject2.SubRole == BuiltObjectSubRole.LargeSpacePort))
                        {
                            builtObject = builtObject2;
                            break;
                        }
                    }
                }
                if (KnownPirateBases != null && KnownPirateBases.Contains(builtObject) && stellarObject != null && builtObject != null)
                {
                    double num = _Galaxy.CalculateDistance(stellarObject.Xpos, stellarObject.Ypos, builtObject.Xpos, builtObject.Ypos);
                    if (num < (double)scanRange)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Design AnalyzeNewResearchFacilities()
        {
            Design weaponsResearchStation;
            Design energyResearchStation;
            Design highTechResearchStation;
            return AnalyzeNewResearchFacilities(out weaponsResearchStation, out energyResearchStation, out highTechResearchStation);
        }

        public Design AnalyzeNewResearchFacilities(out Design weaponsResearchStation, out Design energyResearchStation, out Design highTechResearchStation)
        {
            Design result = null;
            energyResearchStation = Designs.FindNewestCanBuild(BuiltObjectSubRole.EnergyResearchStation);
            weaponsResearchStation = Designs.FindNewestCanBuild(BuiltObjectSubRole.WeaponsResearchStation);
            highTechResearchStation = Designs.FindNewestCanBuild(BuiltObjectSubRole.HighTechResearchStation);
            double num = ResearchEnergyPotential;
            double num2 = ResearchHighTechPotential;
            double num3 = ResearchWeaponsPotential;
            for (int i = 0; i < ConstructionShips.Count; i++)
            {
                BuiltObject builtObject = ConstructionShips[i];
                if (builtObject.Mission != null && builtObject.Mission.Type != 0 && builtObject.Mission.Type == BuiltObjectMissionType.Build && builtObject.Mission.Design != null)
                {
                    Design design = builtObject.Mission.Design;
                    if (design.SubRole == BuiltObjectSubRole.GenericBase || design.SubRole == BuiltObjectSubRole.EnergyResearchStation || design.SubRole == BuiltObjectSubRole.WeaponsResearchStation || design.SubRole == BuiltObjectSubRole.HighTechResearchStation)
                    {
                        num += (double)design.ResearchEnergy;
                        num2 += (double)design.ResearchHighTech;
                        num3 += (double)design.ResearchWeapons;
                    }
                }
            }
            List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
            list.Add(BuiltObjectSubRole.EnergyResearchStation);
            list.Add(BuiltObjectSubRole.HighTechResearchStation);
            list.Add(BuiltObjectSubRole.WeaponsResearchStation);
            list.Add(BuiltObjectSubRole.GenericBase);
            list.Add(BuiltObjectSubRole.SmallSpacePort);
            list.Add(BuiltObjectSubRole.MediumSpacePort);
            list.Add(BuiltObjectSubRole.LargeSpacePort);
            List<BuiltObjectSubRole> subRoles = list;
            for (int j = 0; j < Colonies.Count; j++)
            {
                Habitat habitat = Colonies[j];
                if (habitat == null || habitat.ConstructionQueue == null)
                {
                    continue;
                }
                BuiltObjectList underConstruction = habitat.ConstructionQueue.GetUnderConstruction(subRoles);
                if (underConstruction == null || underConstruction.Count <= 0)
                {
                    continue;
                }
                for (int k = 0; k < underConstruction.Count; k++)
                {
                    BuiltObject builtObject2 = underConstruction[k];
                    if (builtObject2 != null && builtObject2.Design != null)
                    {
                        num += (double)builtObject2.Design.ResearchEnergy;
                        num2 += (double)builtObject2.Design.ResearchHighTech;
                        num3 += (double)builtObject2.Design.ResearchWeapons;
                    }
                }
            }
            double num4 = num + num2 + num3;
            double num5 = AnnualResearchPotential * 1.25;
            num5 *= Policy.ResearchPriority;
            if (num5 > num4)
            {
                if (Policy.ResearchIndustryFocus != 0)
                {
                    switch (Policy.ResearchIndustryFocus)
                    {
                        case IndustryType.Weapon:
                            num3 /= 2.0;
                            break;
                        case IndustryType.Energy:
                            num /= 2.0;
                            break;
                        case IndustryType.HighTech:
                            num2 /= 2.0;
                            break;
                    }
                }
                result = ((num <= num3 && num <= num2) ? energyResearchStation : ((!(num3 <= num) || !(num3 <= num2)) ? highTechResearchStation : weaponsResearchStation));
            }
            return result;
        }

        public HabitatList DetermineResearchStationLocation(bool allowOccupiedSystems, bool mustHaveBuildableResearchStationDesign)
        {
            return DetermineResearchStationLocation(allowOccupiedSystems, mustHaveBuildableResearchStationDesign, assignToResearchHabitats: true);
        }

        public HabitatList DetermineResearchStationLocation(bool allowOccupiedSystems, bool mustHaveBuildableResearchStationDesign, bool assignToResearchHabitats)
        {
            if (mustHaveBuildableResearchStationDesign)
            {
                Design design = Designs.FindNewestCanBuild(BuiltObjectSubRole.EnergyResearchStation);
                Design design2 = Designs.FindNewestCanBuild(BuiltObjectSubRole.WeaponsResearchStation);
                Design design3 = Designs.FindNewestCanBuild(BuiltObjectSubRole.HighTechResearchStation);
                if (design == null && design3 == null && design2 == null)
                {
                    return new HabitatList();
                }
            }
            bool flag = CheckConstructionShipAndMiningStationCanSurviveStorms();
            HabitatList habitatList = new HabitatList();
            List<double> list = new List<double>();
            for (int i = 0; i < _Galaxy.Systems.Count; i++)
            {
                SystemInfo systemInfo = _Galaxy.Systems[i];
                if (!systemInfo.HasResearchBonus || !CheckSystemExplored(systemInfo.SystemStar.SystemIndex))
                {
                    continue;
                }
                bool flag2 = true;
                Empire empire = _Galaxy.CheckSystemOwnership(systemInfo.SystemStar);
                if (empire != null && empire != this)
                {
                    flag2 = false;
                }
                if (allowOccupiedSystems)
                {
                    flag2 = true;
                }
                if (!flag2 || CheckNearPirateBase(systemInfo.SystemStar, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos) || (!flag && _Galaxy.CheckInStorm(systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos)))
                {
                    continue;
                }
                HabitatList habitatList2 = new HabitatList();
                List<double> list2 = new List<double>();
                for (int j = 0; j < systemInfo.Habitats.Count; j++)
                {
                    if (systemInfo.Habitats[j].ResearchBonus > 0)
                    {
                        habitatList2.Add(systemInfo.Habitats[j]);
                        list2.Add((double)(int)systemInfo.Habitats[j].ResearchBonus / 100.0);
                    }
                }
                if (habitatList2.Count <= 0 && systemInfo.SystemStar.ResearchBonus > 0)
                {
                    habitatList2.Add(systemInfo.SystemStar);
                    list2.Add((double)(int)systemInfo.SystemStar.ResearchBonus / 100.0);
                }
                for (int k = 0; k < habitatList2.Count; k++)
                {
                    Habitat habitat = habitatList2[k];
                    double num = list2[k];
                    if (habitat == null || CheckResearchStationAtLocation(habitat) || !_Galaxy.CheckEmpireTerritoryCanBuildAtHabitat(this, habitat))
                    {
                        continue;
                    }
                    if (Policy.ResearchIndustryFocus != 0)
                    {
                        switch (habitat.ResearchBonusIndustry)
                        {
                            case IndustryType.Weapon:
                                if (Policy.ResearchIndustryFocus == IndustryType.Weapon)
                                {
                                    num *= 1.5;
                                }
                                break;
                            case IndustryType.Energy:
                                if (Policy.ResearchIndustryFocus == IndustryType.Energy)
                                {
                                    num *= 1.5;
                                }
                                break;
                            case IndustryType.HighTech:
                                if (Policy.ResearchIndustryFocus == IndustryType.HighTech)
                                {
                                    num *= 1.5;
                                }
                                break;
                        }
                    }
                    double item = 1.0 - num;
                    list.Add(item);
                    habitatList.Add(habitat);
                }
            }
            Habitat[] items = habitatList.ToArray();
            double[] keys = list.ToArray();
            Array.Sort(keys, items);
            HabitatList habitatList3 = new HabitatList();
            habitatList3.AddRange(items);
            if (assignToResearchHabitats)
            {
                _ResearchHabitats = habitatList3;
            }
            return habitatList3;
        }

        public bool CheckResearchStationAtLocation(Habitat habitat)
        {
            if (habitat.Category == HabitatCategoryType.Asteroid)
            {
                habitat = Galaxy.DetermineHabitatSystemStar(habitat);
            }
            if (habitat.Category == HabitatCategoryType.Star)
            {
                BuiltObjectList builtObjectsAtLocation = _Galaxy.GetBuiltObjectsAtLocation(habitat.Xpos, habitat.Ypos, (int)((double)Galaxy.MaxSolarSystemSize * 2.1));
                for (int i = 0; i < builtObjectsAtLocation.Count; i++)
                {
                    BuiltObject builtObject = builtObjectsAtLocation[i];
                    if (((builtObject != null && builtObject.ResearchWeapons > 0) || builtObject.ResearchEnergy > 0 || builtObject.ResearchHighTech > 0) && builtObject.Role == BuiltObjectRole.Base && builtObject.SubRole != BuiltObjectSubRole.SmallSpacePort && builtObject.SubRole != BuiltObjectSubRole.MediumSpacePort && builtObject.SubRole != BuiltObjectSubRole.LargeSpacePort && builtObject.NearestSystemStar == habitat)
                    {
                        return true;
                    }
                }
            }
            else if (habitat.BasesAtHabitat != null && habitat.BasesAtHabitat.Count > 0)
            {
                for (int j = 0; j < habitat.BasesAtHabitat.Count; j++)
                {
                    if (habitat.BasesAtHabitat[j].ResearchWeapons > 0 || habitat.BasesAtHabitat[j].ResearchEnergy > 0 || habitat.BasesAtHabitat[j].ResearchHighTech > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public PrioritizedTargetList DetermineResortBaseLocation(bool allowAnyDistanceFromColonies)
        {
            PrioritizedTargetList prioritizedTargetList = new PrioritizedTargetList();
            Design design = _Designs.FindNewestCanBuild(BuiltObjectSubRole.ResortBase);
            if (design == null)
            {
                return prioritizedTargetList;
            }
            double num = Galaxy.SectorSize;
            double num2 = Math.Sqrt(num);
            for (int i = 0; i < _Galaxy.Systems.Count; i++)
            {
                SystemInfo systemInfo = _Galaxy.Systems[i];
                if (!CheckSystemExplored(systemInfo.SystemStar.SystemIndex) || CheckNearPirateBase(systemInfo.SystemStar, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos))
                {
                    continue;
                }
                for (int j = 0; j < systemInfo.Habitats.Count; j++)
                {
                    Habitat habitat = systemInfo.Habitats[j];
                    if (!(habitat.ScenicFactor > 0f) || !_ResourceMap.CheckResourcesKnown(habitat))
                    {
                        continue;
                    }
                    Habitat habitat2 = _Galaxy.FindNearestColony(habitat.Xpos, habitat.Ypos, null, 100000);
                    if (habitat2 == null)
                    {
                        continue;
                    }
                    double num3 = _Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, habitat2.Xpos, habitat2.Ypos);
                    if (!allowAnyDistanceFromColonies && !(num3 < num))
                    {
                        continue;
                    }
                    BuiltObject builtObject = _Galaxy.FindNearestBuiltObject((int)habitat.Xpos, (int)habitat.Ypos, BuiltObjectRole.Base);
                    double num4 = double.MaxValue;
                    if (builtObject != null)
                    {
                        num4 = _Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, builtObject.Xpos, builtObject.Ypos);
                    }
                    if (num4 > 1000.0)
                    {
                        double num5 = 1.0;
                        if (systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire != null && systemInfo.DominantEmpire.Empire != this)
                        {
                            num5 = 0.3;
                        }
                        double num6 = num2 - Math.Sqrt(num3);
                        int priority = (int)((double)habitat.ScenicFactor * num6 * num5);
                        prioritizedTargetList.Add(new PrioritizedTarget(habitat, priority));
                    }
                }
            }
            prioritizedTargetList.Sort();
            prioritizedTargetList.Reverse();
            return prioritizedTargetList;
        }

        private void DetermineMonitoringStationLocation()
        {
            Component component = Research.EvaluateDesiredComponent(ComponentType.SensorLongRange, ShipDesignFocus.Balanced);
            Design design = Designs.FindNewestCanBuild(BuiltObjectSubRole.MonitoringStation);
            if (design == null || design.SensorLongRange <= 0 || component == null)
            {
                return;
            }
            bool flag = CheckConstructionShipAndMiningStationCanSurviveStorms();
            HabitatList habitatList = new HabitatList();
            List<double> list = new List<double>();
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                if (diplomaticRelation.Type == DiplomaticRelationType.NotMet || empire == this)
                {
                    continue;
                }
                bool flag2 = false;
                switch (diplomaticRelation.Strategy)
                {
                    case DiplomaticStrategy.Conquer:
                    case DiplomaticStrategy.Defend:
                    case DiplomaticStrategy.DefendPlacate:
                    case DiplomaticStrategy.DefendUndermine:
                        flag2 = true;
                        break;
                }
                if (empire.Colonies.Count >= 5)
                {
                    EmpireEvaluation empireEvaluation = empire.ObtainEmpireEvaluation(this);
                    if (empireEvaluation.OverallAttitude < 0)
                    {
                        flag2 = true;
                    }
                }
                if (!flag2)
                {
                    continue;
                }
                Habitat habitat = _Galaxy.FastFindNearestColony((int)Capital.Xpos, (int)Capital.Ypos, empire, 0);
                if (habitat != null)
                {
                    SystemVisibilityStatus systemVisibilityStatus = CheckSystemVisibilityStatus(habitat.SystemIndex);
                    if (systemVisibilityStatus == SystemVisibilityStatus.Explored && !IsObjectVisibleToThisEmpire(habitat))
                    {
                        Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                        habitatList.Add(habitat2);
                        double item = _Galaxy.CalculateDistance(Capital.Xpos, Capital.Ypos, habitat2.Xpos, habitat2.Ypos);
                        list.Add(item);
                    }
                }
            }
            Habitat[] array = habitatList.ToArray();
            double[] keys = list.ToArray();
            Array.Sort(keys, array);
            HabitatList habitatList2 = new HabitatList();
            List<Point> list2 = new List<Point>();
            for (int j = 0; j < array.Length; j++)
            {
                bool flag3 = false;
                Habitat habitat3 = array[j];
                Habitat habitat4 = _Galaxy.FindNearestUncolonizedExploredSystem(habitat3.Xpos, habitat3.Ypos, this);
                if (habitat4 != null)
                {
                    double num = _Galaxy.CalculateDistance(habitat4.Xpos, habitat4.Ypos, habitat3.Xpos, habitat3.Ypos);
                    if (num < (double)design.SensorLongRange - (double)Galaxy.MaxSolarSystemSize * 2.1 && _Galaxy.Systems[habitat4.SystemIndex].Habitats.Count > 0 && !CheckNearPirateBase(habitat4, habitat4.Xpos, habitat4.Ypos) && (flag || !_Galaxy.CheckInStorm(habitat4.Xpos, habitat4.Ypos)))
                    {
                        bool flag4 = false;
                        BuiltObject builtObject = _Galaxy.FastFindNearestLongRangeScannerBase((int)habitat4.Xpos, (int)habitat4.Ypos, this);
                        if (builtObject != null)
                        {
                            double num2 = _Galaxy.CalculateDistance(habitat4.Xpos, habitat4.Ypos, builtObject.Xpos, builtObject.Ypos);
                            if (num2 < (double)Galaxy.MaxSolarSystemSize * 2.1)
                            {
                                flag4 = true;
                            }
                        }
                        if (!flag4)
                        {
                            bool flag5 = false;
                            SystemInfo systemInfo = _Galaxy.Systems[habitat3];
                            Empire empire2 = null;
                            if (systemInfo != null && systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire != null)
                            {
                                empire2 = systemInfo.DominantEmpire.Empire;
                            }
                            if (empire2 != null)
                            {
                                if (empire2.IsObjectVisibleToThisEmpire(habitat4, includeLongRangeScanners: false, includeShipsOutsideSystems: false))
                                {
                                    flag5 = true;
                                }
                                else if (empire2.FindLongRangeScannerThatCanSeePoint(habitat4.Xpos, habitat4.Ypos, design.Stealth) != null)
                                {
                                    flag5 = true;
                                }
                            }
                            if (!flag5)
                            {
                                flag3 = true;
                                int index = Galaxy.Rnd.Next(0, _Galaxy.Systems[habitat4.SystemIndex].Habitats.Count);
                                Habitat item2 = _Galaxy.Systems[habitat4.SystemIndex].Habitats[index];
                                habitatList2.Add(item2);
                            }
                        }
                    }
                }
                if (flag3)
                {
                    continue;
                }
                double angle = _Galaxy.SelectRandomHeading();
                double num3 = (double)design.SensorLongRange - (double)Galaxy.MaxSolarSystemSize * 2.1;
                num3 -= (double)Galaxy.MaxSolarSystemSize * 3.0;
                ObtainCoordinatesFromPoint(angle, habitat3.Xpos, habitat3.Ypos, num3, out var x, out var y);
                int num4 = 0;
                bool flag6 = false;
                while ((!CheckCoordsWithinGalaxy(x, y) || !flag6) && (flag || !_Galaxy.CheckInStorm(x, y)) && num4 < 20)
                {
                    angle = _Galaxy.SelectRandomHeading();
                    ObtainCoordinatesFromPoint(angle, habitat3.Xpos, habitat3.Ypos, num3, out x, out y);
                    num4++;
                    flag6 = true;
                    BuiltObject builtObject2 = _Galaxy.FastFindNearestLongRangeScannerBase((int)x, (int)y, this);
                    if (builtObject2 != null)
                    {
                        double num5 = _Galaxy.CalculateDistance(builtObject2.Xpos, builtObject2.Ypos, x, y);
                        if (num5 < (double)Galaxy.MaxSolarSystemSize * 2.1)
                        {
                            flag6 = false;
                        }
                    }
                    if (flag6)
                    {
                        SystemInfo systemInfo2 = _Galaxy.Systems[habitat3];
                        Empire empire3 = null;
                        if (systemInfo2 != null && systemInfo2.DominantEmpire != null && systemInfo2.DominantEmpire.Empire != null)
                        {
                            empire3 = systemInfo2.DominantEmpire.Empire;
                        }
                        if (empire3 != null && empire3.FindLongRangeScannerThatCanSeePoint(x, y, design.Stealth) != null)
                        {
                            flag6 = false;
                        }
                    }
                }
                if (CheckCoordsWithinGalaxy(x, y) && !flag6)
                {
                    list2.Add(new Point((int)x, (int)y));
                }
            }
            _MonitoringHabitats = habitatList2;
            _MonitoringPoints = list2;
        }

        private bool CheckCoordsWithinGalaxy(double x, double y)
        {
            if (x < 0.0 || y < 0.0)
            {
                return false;
            }
            if (x > (double)Galaxy.SizeX || y > (double)Galaxy.SizeY)
            {
                return false;
            }
            return true;
        }

        public void ObtainCoordinatesFromPoint(double angle, double startX, double startY, double distance, out double x, out double y)
        {
            double num = Math.Cos(angle) * distance;
            double num2 = Math.Sin(angle) * distance;
            if (Galaxy.Rnd.Next(0, 2) == 1)
            {
                num *= -1.0;
            }
            if (Galaxy.Rnd.Next(0, 2) == 1)
            {
                num2 *= -1.0;
            }
            x = startX + num;
            y = startY + num2;
        }

        private bool CheckMiningStationForResourceClearance(BuiltObject ship, BuiltObject miningStation, ResourceList empireDeficientResources)
        {
            if (miningStation.Empire == null)
            {
                return false;
            }
            bool result = false;
            if (miningStation != null && miningStation.Role == BuiltObjectRole.Base && miningStation.IsResourceExtractor && miningStation.SubRole != BuiltObjectSubRole.SmallSpacePort && miningStation.SubRole != BuiltObjectSubRole.MediumSpacePort && miningStation.SubRole != BuiltObjectSubRole.LargeSpacePort)
            {
                if (!ship.WithinFuelRangeAndRefuel(miningStation.Xpos, miningStation.Ypos, 0.0))
                {
                    return false;
                }
                HabitatResourceList habitatResourceList = ((miningStation.ParentHabitat == null || miningStation.ParentHabitat.Resources == null) ? new HabitatResourceList() : miningStation.ParentHabitat.Resources);
                CargoList cargo = miningStation.Cargo;
                int num = 0;
                if (cargo != null)
                {
                    num = cargo.GetTotalUnitsAvailable(this, habitatResourceList);
                }
                int num2 = Galaxy.MiningStationResourceTransportThreshhold;
                if (cargo != null)
                {
                    num2 = Math.Max(num2, miningStation.Cargo.GetTotalUnits(this) / 4);
                }
                if (num > 0 && num >= num2)
                {
                    double num3 = double.MaxValue;
                    BuiltObject builtObject = null;
                    for (int i = 0; i < miningStation.Empire.SpacePorts.Count; i++)
                    {
                        BuiltObject builtObject2 = miningStation.Empire.SpacePorts[i];
                        if (builtObject2 != null && builtObject2 != miningStation && builtObject2.IsSpacePort && builtObject2.Cargo != null && builtObject2.CargoSpace >= builtObject2.CargoCapacity / 4)
                        {
                            double num4 = _Galaxy.CalculateDistanceSquared(builtObject2.Xpos, builtObject2.Ypos, miningStation.Xpos, miningStation.Ypos);
                            if (num4 < num3)
                            {
                                num3 = num4;
                                builtObject = builtObject2;
                            }
                        }
                    }
                    if (builtObject != null && cargo != null)
                    {
                        CargoList cargoList = new CargoList();
                        Cargo cargo2 = null;
                        foreach (Cargo item2 in cargo)
                        {
                            if (item2.EmpireId == EmpireId && item2.CommodityResource != null && item2.Available > 0 && habitatResourceList.ContainsId(item2.CommodityResource.ResourceID))
                            {
                                int num5 = empireDeficientResources.IndexOf(item2.CommodityResource.ResourceID);
                                if (num5 >= 0 && num5 < 5)
                                {
                                    cargo2 = item2;
                                }
                            }
                        }
                        if (cargo2 != null)
                        {
                            Resource commodityResource = cargo2.CommodityResource;
                            int num6 = Math.Min(cargo2.Available, ship.CargoSpace);
                            if (num6 > 0)
                            {
                                Cargo cargo3 = new Cargo(commodityResource, num6, miningStation.Empire);
                                cargoList.Add(cargo3);
                                cargo2.Reserved += num6;
                            }
                        }
                        else
                        {
                            double num7 = (double)ship.CargoSpace / (double)cargo.GetTotalUnitsAvailable(this, habitatResourceList);
                            if (num7 > 1.0)
                            {
                                num7 = 1.0;
                            }
                            foreach (Cargo item3 in cargo)
                            {
                                if (item3.EmpireId == EmpireId && item3.CommodityResource != null && habitatResourceList.ContainsId(item3.CommodityResource.ResourceID))
                                {
                                    Resource commodityResource2 = item3.CommodityResource;
                                    int val = (int)((double)item3.Available * num7);
                                    val = Math.Min(val, item3.Available);
                                    if (val > 0)
                                    {
                                        Cargo cargo4 = new Cargo(commodityResource2, val, miningStation.Empire);
                                        cargoList.Add(cargo4);
                                        item3.Reserved += val;
                                    }
                                }
                            }
                        }
                        for (int j = 0; j < cargoList.Count; j++)
                        {
                            Cargo cargo5 = cargoList[j];
                            if (cargo5 != null)
                            {
                                Contract item = new Contract(miningStation, cargo5.Amount, cargo5.Resource.ResourceID, -1, EmpireId);
                                ship.ContractsToFulfill.Add(item);
                            }
                        }
                        ship.AssignMission(BuiltObjectMissionType.Transport, miningStation, builtObject, cargoList, BuiltObjectMissionPriority.Normal);
                        result = true;
                    }
                }
            }
            return result;
        }

        private bool CheckColonyForResourceClearance(BuiltObject ship, Habitat colony)
        {
            bool result = false;
            CargoList cargo = colony.Cargo;
            if (cargo != null)
            {
                if (!ship.WithinFuelRangeAndRefuel(colony.Xpos, colony.Ypos, 0.0, ship.CachedRefuellingLocation))
                {
                    return false;
                }
                {
                    foreach (Cargo item2 in cargo)
                    {
                        if (item2.EmpireId != EmpireId || item2.CommodityResource == null)
                        {
                            continue;
                        }
                        Resource commodityResource = item2.CommodityResource;
                        int num = 0;
                        num = ((!commodityResource.IsLuxuryResource) ? (item2.Available - Galaxy.CalculateResourceLevel(item2, colony)) : ((!commodityResource.IsRestrictedResource) ? (item2.Available - colony.CalculateMinimumLuxuryResourceLevel()) : (item2.Available - colony.CalculateMinimumLuxuryResourceLevelRestricted())));
                        if (num <= Galaxy.ColonyResourceTransportThreshhold)
                        {
                            continue;
                        }
                        int val = num;
                        int num2 = Math.Min(val, ship.CargoSpace);
                        if (num2 <= 0)
                        {
                            continue;
                        }
                        double num3 = double.MaxValue;
                        BuiltObject builtObject = null;
                        for (int i = 0; i < colony.Empire.SpacePorts.Count; i++)
                        {
                            BuiltObject builtObject2 = colony.Empire.SpacePorts[i];
                            if (builtObject2.IsSpacePort && builtObject2.Cargo != null && builtObject2.CargoSpace >= builtObject2.CargoCapacity / 4)
                            {
                                double num4 = _Galaxy.CalculateDistanceSquared(builtObject2.Xpos, builtObject2.Ypos, colony.Xpos, colony.Ypos);
                                if (num4 < num3)
                                {
                                    num3 = num4;
                                    builtObject = builtObject2;
                                }
                            }
                        }
                        if (builtObject != null)
                        {
                            CargoList cargoList = new CargoList();
                            cargoList.Add(new Cargo(commodityResource, num2, this));
                            item2.Reserved += num2;
                            Contract item = new Contract(colony, num2, commodityResource.ResourceID, -1, EmpireId);
                            ship.ContractsToFulfill.Add(item);
                            ship.AssignMission(BuiltObjectMissionType.Transport, colony, builtObject, cargoList, BuiltObjectMissionPriority.Normal);
                            return true;
                        }
                    }
                    return result;
                }
            }
            return result;
        }

        private int DetermineDiplomaticMissionDifficulty(Empire targetEmpire, DiplomaticRelationType desiredDiplomaticRelationType, Empire subjectEmpire)
        {
            int num2 = 0;
            int num3 = 0;
            EmpireEvaluation empireEvaluation = targetEmpire.EmpireEvaluations[subjectEmpire];
            if (empireEvaluation != null)
            {
                num3 = empireEvaluation.OverallAttitude;
            }
            ResolveTypicalAttitudeLevel(desiredDiplomaticRelationType, out var lowerLevel, out var upperLevel);
            if (num3 < lowerLevel)
            {
                num2 = (lowerLevel - num3) * 3;
            }
            else if (num3 > upperLevel)
            {
                num2 = (upperLevel - num3) * 3;
            }
            return 100 + num2;
        }

        public double CalculateIntelligenceMissionBonusFromLeaderAndAmbassador(IntelligenceMissionType missionType, Empire targetEmpire)
        {
            double num = 1.0;
            double num2 = 1.0;
            double num3 = 1.0;
            if (Characters != null)
            {
                CharacterList ambassadorsForEmpire = Characters.GetAmbassadorsForEmpire(targetEmpire);
                int highestSkillLevel = ambassadorsForEmpire.GetHighestSkillLevel(CharacterSkillType.Espionage);
                int highestSkillLevel2 = ambassadorsForEmpire.GetHighestSkillLevel(CharacterSkillType.CounterEspionage);
                num2 = 1.0 + (double)highestSkillLevel / 100.0;
                num3 = 1.0 + (double)highestSkillLevel2 / 100.0;
            }
            switch (missionType)
            {
                case IntelligenceMissionType.StealGalaxyMap:
                case IntelligenceMissionType.StealOperationsMap:
                case IntelligenceMissionType.StealTechData:
                case IntelligenceMissionType.StealTerritoryMap:
                    if (Leader != null)
                    {
                        num *= 1.0 + (double)Leader.Espionage / 100.0;
                    }
                    num *= num2;
                    break;
                case IntelligenceMissionType.CounterIntelligence:
                    if (Leader != null)
                    {
                        num *= 1.0 + (double)Leader.CounterEspionage / 100.0;
                    }
                    num *= num3;
                    break;
            }
            return num;
        }

        public void CalculateIntelligenceMissionSkill(Character agent, IntelligenceMissionType missionType, Empire targetEmpire, out double oneYearDifficulty, out double threeMonthDifficulty, out double oneMonthDifficulty)
        {
            double num = (double)DominantRace.CautionLevel / 100.0;
            double num2 = num * 1.4;
            int num3 = 0;
            switch (missionType)
            {
                case IntelligenceMissionType.CounterIntelligence:
                    num3 = agent.CounterEspionageFactored;
                    break;
                case IntelligenceMissionType.AssassinateCharacter:
                    num3 = agent.AssassinationFactored;
                    break;
                case IntelligenceMissionType.DeepCover:
                    num3 = agent.ConcealmentFactored;
                    break;
                case IntelligenceMissionType.InciteRevolution:
                    num3 = agent.PsyOpsFactored;
                    break;
                case IntelligenceMissionType.SabotageConstruction:
                case IntelligenceMissionType.SabotageColony:
                case IntelligenceMissionType.DestroyBase:
                    num3 = agent.SabotageFactored;
                    break;
                case IntelligenceMissionType.StealGalaxyMap:
                case IntelligenceMissionType.StealOperationsMap:
                case IntelligenceMissionType.StealTechData:
                case IntelligenceMissionType.StealTerritoryMap:
                    num3 = agent.EspionageFactored;
                    if (missionType == IntelligenceMissionType.StealTechData && targetEmpire != null && targetEmpire.Characters != null)
                    {
                        if (targetEmpire.Characters.CheckCharactersForTrait(CharacterRole.Scientist, CharacterTraitType.ForeignSpy))
                        {
                            num3 *= 2;
                        }
                        else if (targetEmpire.Characters.CheckCharactersForTrait(CharacterRole.Scientist, CharacterTraitType.Patriot))
                        {
                            num3 /= 2;
                        }
                    }
                    break;
            }
            double num4 = CalculateIntelligenceMissionBonusFromLeaderAndAmbassador(missionType, targetEmpire);
            num3 = (int)((double)num3 * num4);
            oneYearDifficulty = (double)num3 * 4.0 / num2;
            threeMonthDifficulty = (double)num3 * 2.0 / num2;
            oneMonthDifficulty = (double)num3 / num2;
        }

        private IntelligenceMission DetermineSabotageMission(Empire targetEmpire, EmpireEvaluation evaluation, DiplomaticRelation relation, PirateRelation pirateRelation, Character agent)
        {
            IntelligenceMission result = null;
            List<IntelligenceMissionType> list = new List<IntelligenceMissionType>();
            if (relation != null)
            {
                if (relation.Type == DiplomaticRelationType.War)
                {
                    list.AddRange(new IntelligenceMissionType[6]
                    {
                    IntelligenceMissionType.DeepCover,
                    IntelligenceMissionType.StealOperationsMap,
                    IntelligenceMissionType.DestroyBase,
                    IntelligenceMissionType.AssassinateCharacter,
                    IntelligenceMissionType.SabotageConstruction,
                    IntelligenceMissionType.SabotageColony
                    });
                }
                else
                {
                    switch (relation.Strategy)
                    {
                        case DiplomaticStrategy.Conquer:
                            list.AddRange(new IntelligenceMissionType[5]
                            {
                        IntelligenceMissionType.DeepCover,
                        IntelligenceMissionType.StealOperationsMap,
                        IntelligenceMissionType.SabotageConstruction,
                        IntelligenceMissionType.DestroyBase,
                        IntelligenceMissionType.AssassinateCharacter
                            });
                            break;
                        case DiplomaticStrategy.Undermine:
                            list.AddRange(new IntelligenceMissionType[8]
                            {
                        IntelligenceMissionType.StealTerritoryMap,
                        IntelligenceMissionType.StealOperationsMap,
                        IntelligenceMissionType.StealTechData,
                        IntelligenceMissionType.SabotageColony,
                        IntelligenceMissionType.DestroyBase,
                        IntelligenceMissionType.AssassinateCharacter,
                        IntelligenceMissionType.InciteRevolution,
                        IntelligenceMissionType.DeepCover
                            });
                            break;
                        case DiplomaticStrategy.DefendUndermine:
                            list.AddRange(new IntelligenceMissionType[4]
                            {
                        IntelligenceMissionType.StealTerritoryMap,
                        IntelligenceMissionType.StealGalaxyMap,
                        IntelligenceMissionType.StealTechData,
                        IntelligenceMissionType.DeepCover
                            });
                            break;
                        case DiplomaticStrategy.Punish:
                            list.AddRange(new IntelligenceMissionType[4]
                            {
                        IntelligenceMissionType.SabotageConstruction,
                        IntelligenceMissionType.SabotageColony,
                        IntelligenceMissionType.DestroyBase,
                        IntelligenceMissionType.InciteRevolution
                            });
                            break;
                    }
                }
            }
            else if (pirateRelation != null)
            {
                switch (pirateRelation.Type)
                {
                    case PirateRelationType.None:
                        list.AddRange(new IntelligenceMissionType[9]
                        {
                    IntelligenceMissionType.DeepCover,
                    IntelligenceMissionType.StealOperationsMap,
                    IntelligenceMissionType.StealTerritoryMap,
                    IntelligenceMissionType.StealGalaxyMap,
                    IntelligenceMissionType.StealTechData,
                    IntelligenceMissionType.DestroyBase,
                    IntelligenceMissionType.AssassinateCharacter,
                    IntelligenceMissionType.SabotageConstruction,
                    IntelligenceMissionType.SabotageColony
                        });
                        break;
                    case PirateRelationType.Protection:
                        list.AddRange(new IntelligenceMissionType[5]
                        {
                    IntelligenceMissionType.DeepCover,
                    IntelligenceMissionType.StealOperationsMap,
                    IntelligenceMissionType.StealTerritoryMap,
                    IntelligenceMissionType.StealGalaxyMap,
                    IntelligenceMissionType.StealTechData
                        });
                        break;
                }
            }
            long timeLength = Galaxy.RealSecondsInGalacticYear * 1000 / 12;
            long timeLength2 = Galaxy.RealSecondsInGalacticYear * 1000 / 4;
            long timeLength3 = Galaxy.RealSecondsInGalacticYear * 1000;
            double oneYearDifficulty = 0.0;
            double threeMonthDifficulty = 0.0;
            double oneMonthDifficulty = 0.0;
            if (CanAssignIntelligenceMissionAgainstEmpire(targetEmpire, evaluation, relation, pirateRelation, IntelligenceMissionType.InciteRevolution))
            {
                CalculateIntelligenceMissionSkill(agent, IntelligenceMissionType.InciteRevolution, targetEmpire, out oneYearDifficulty, out threeMonthDifficulty, out oneMonthDifficulty);
                IntelligenceMission intelligenceMission = new IntelligenceMission(this, null, IntelligenceMissionType.InciteRevolution, _Galaxy.CurrentStarDate, targetEmpire);
                if (!CheckWhetherTargetOfIntelligenceMission(targetEmpire, targetEmpire, IntelligenceMissionType.InciteRevolution) && !CheckForIntelligenceMissionsOfTypeAgainstEmpire(targetEmpire, IntelligenceMissionType.InciteRevolution) && list.Contains(IntelligenceMissionType.InciteRevolution) && intelligenceMission.Difficulty <= (int)oneYearDifficulty)
                {
                    result = intelligenceMission;
                    result.TimeLength = timeLength3;
                    if (intelligenceMission.Difficulty <= (int)threeMonthDifficulty)
                    {
                        result = intelligenceMission;
                        result.TimeLength = timeLength2;
                        if (intelligenceMission.Difficulty <= (int)oneMonthDifficulty)
                        {
                            result = intelligenceMission;
                            result.TimeLength = timeLength;
                        }
                    }
                    return result;
                }
            }
            CharacterList characterList = ResolveKnownCharacters(targetEmpire);
            if (characterList.Count > 0 && CanAssignIntelligenceMissionAgainstEmpire(targetEmpire, evaluation, relation, pirateRelation, IntelligenceMissionType.AssassinateCharacter))
            {
                CalculateIntelligenceMissionSkill(agent, IntelligenceMissionType.AssassinateCharacter, targetEmpire, out oneYearDifficulty, out threeMonthDifficulty, out oneMonthDifficulty);
                int num = Galaxy.Rnd.Next(0, characterList.Count);
                for (int i = num; i < characterList.Count; i++)
                {
                    Character character = characterList[i];
                    if (character == null || !character.Active)
                    {
                        continue;
                    }
                    IntelligenceMission intelligenceMission2 = new IntelligenceMission(this, null, IntelligenceMissionType.AssassinateCharacter, _Galaxy.CurrentStarDate, character);
                    if (CheckWhetherTargetOfIntelligenceMission(targetEmpire, character, IntelligenceMissionType.AssassinateCharacter) || !list.Contains(IntelligenceMissionType.AssassinateCharacter) || intelligenceMission2.Difficulty > (int)oneYearDifficulty)
                    {
                        continue;
                    }
                    result = intelligenceMission2;
                    result.TimeLength = timeLength3;
                    if (intelligenceMission2.Difficulty <= (int)threeMonthDifficulty)
                    {
                        result = intelligenceMission2;
                        result.TimeLength = timeLength2;
                        if (intelligenceMission2.Difficulty <= (int)oneMonthDifficulty)
                        {
                            result = intelligenceMission2;
                            result.TimeLength = timeLength;
                        }
                    }
                    return result;
                }
                for (int j = 0; j < num; j++)
                {
                    Character character2 = characterList[j];
                    if (character2 == null || !character2.Active)
                    {
                        continue;
                    }
                    IntelligenceMission intelligenceMission3 = new IntelligenceMission(this, null, IntelligenceMissionType.AssassinateCharacter, _Galaxy.CurrentStarDate, character2);
                    if (CheckWhetherTargetOfIntelligenceMission(targetEmpire, character2, IntelligenceMissionType.AssassinateCharacter) || !list.Contains(IntelligenceMissionType.AssassinateCharacter) || intelligenceMission3.Difficulty > (int)oneYearDifficulty)
                    {
                        continue;
                    }
                    result = intelligenceMission3;
                    result.TimeLength = timeLength3;
                    if (intelligenceMission3.Difficulty <= (int)threeMonthDifficulty)
                    {
                        result = intelligenceMission3;
                        result.TimeLength = timeLength2;
                        if (intelligenceMission3.Difficulty <= (int)oneMonthDifficulty)
                        {
                            result = intelligenceMission3;
                            result.TimeLength = timeLength;
                        }
                    }
                    return result;
                }
            }
            List<StellarObject> list2 = ResolveKnownBases(targetEmpire);
            if (list2.Count > 0 && CanAssignIntelligenceMissionAgainstEmpire(targetEmpire, evaluation, relation, pirateRelation, IntelligenceMissionType.DestroyBase))
            {
                CalculateIntelligenceMissionSkill(agent, IntelligenceMissionType.DestroyBase, targetEmpire, out oneYearDifficulty, out threeMonthDifficulty, out oneMonthDifficulty);
                int num2 = Galaxy.Rnd.Next(0, list2.Count);
                for (int k = num2; k < list2.Count; k++)
                {
                    BuiltObject builtObject = (BuiltObject)list2[k];
                    if (builtObject == null || builtObject.HasBeenDestroyed)
                    {
                        continue;
                    }
                    IntelligenceMission intelligenceMission4 = new IntelligenceMission(this, null, IntelligenceMissionType.DestroyBase, _Galaxy.CurrentStarDate, builtObject);
                    if (CheckWhetherTargetOfIntelligenceMission(targetEmpire, builtObject, IntelligenceMissionType.DestroyBase) || !list.Contains(IntelligenceMissionType.DestroyBase) || intelligenceMission4.Difficulty > (int)oneYearDifficulty)
                    {
                        continue;
                    }
                    result = intelligenceMission4;
                    result.TimeLength = timeLength3;
                    if (intelligenceMission4.Difficulty <= (int)threeMonthDifficulty)
                    {
                        result = intelligenceMission4;
                        result.TimeLength = timeLength2;
                        if (intelligenceMission4.Difficulty <= (int)oneMonthDifficulty)
                        {
                            result = intelligenceMission4;
                            result.TimeLength = timeLength;
                        }
                    }
                    return result;
                }
                for (int l = 0; l < num2; l++)
                {
                    BuiltObject builtObject2 = (BuiltObject)list2[l];
                    if (builtObject2 == null || builtObject2.HasBeenDestroyed)
                    {
                        continue;
                    }
                    IntelligenceMission intelligenceMission5 = new IntelligenceMission(this, null, IntelligenceMissionType.DestroyBase, _Galaxy.CurrentStarDate, builtObject2);
                    if (CheckWhetherTargetOfIntelligenceMission(targetEmpire, builtObject2, IntelligenceMissionType.DestroyBase) || !list.Contains(IntelligenceMissionType.DestroyBase) || intelligenceMission5.Difficulty > (int)oneYearDifficulty)
                    {
                        continue;
                    }
                    result = intelligenceMission5;
                    result.TimeLength = timeLength3;
                    if (intelligenceMission5.Difficulty <= (int)threeMonthDifficulty)
                    {
                        result = intelligenceMission5;
                        result.TimeLength = timeLength2;
                        if (intelligenceMission5.Difficulty <= (int)oneMonthDifficulty)
                        {
                            result = intelligenceMission5;
                            result.TimeLength = timeLength;
                        }
                    }
                    return result;
                }
            }
            List<StellarObject> list3 = ResolveKnownColonies(targetEmpire);
            if (list3.Count > 0 && CanAssignIntelligenceMissionAgainstEmpire(targetEmpire, evaluation, relation, pirateRelation, IntelligenceMissionType.SabotageColony))
            {
                CalculateIntelligenceMissionSkill(agent, IntelligenceMissionType.SabotageColony, targetEmpire, out oneYearDifficulty, out threeMonthDifficulty, out oneMonthDifficulty);
                int num3 = Galaxy.Rnd.Next(0, list3.Count);
                for (int m = num3; m < list3.Count; m++)
                {
                    Habitat habitat = (Habitat)list3[m];
                    if (habitat == null || habitat.HasBeenDestroyed || !(habitat.CulturalDistressFactor > 0f))
                    {
                        continue;
                    }
                    IntelligenceMission intelligenceMission6 = new IntelligenceMission(this, null, IntelligenceMissionType.SabotageColony, _Galaxy.CurrentStarDate, habitat);
                    if (CheckWhetherTargetOfIntelligenceMission(targetEmpire, habitat, IntelligenceMissionType.SabotageColony) || !list.Contains(IntelligenceMissionType.SabotageColony) || intelligenceMission6.Difficulty > (int)oneYearDifficulty)
                    {
                        continue;
                    }
                    result = intelligenceMission6;
                    result.TimeLength = timeLength3;
                    if (intelligenceMission6.Difficulty <= (int)threeMonthDifficulty)
                    {
                        result = intelligenceMission6;
                        result.TimeLength = timeLength2;
                        if (intelligenceMission6.Difficulty <= (int)oneMonthDifficulty)
                        {
                            result = intelligenceMission6;
                            result.TimeLength = timeLength;
                        }
                    }
                    return result;
                }
                for (int n = 0; n < num3; n++)
                {
                    Habitat habitat2 = (Habitat)list3[n];
                    if (habitat2 == null || habitat2.HasBeenDestroyed || !(habitat2.CulturalDistressFactor > 0f))
                    {
                        continue;
                    }
                    IntelligenceMission intelligenceMission7 = new IntelligenceMission(this, null, IntelligenceMissionType.SabotageColony, _Galaxy.CurrentStarDate, habitat2);
                    if (CheckWhetherTargetOfIntelligenceMission(targetEmpire, habitat2, IntelligenceMissionType.SabotageColony) || !list.Contains(IntelligenceMissionType.SabotageColony) || intelligenceMission7.Difficulty > (int)oneYearDifficulty)
                    {
                        continue;
                    }
                    result = intelligenceMission7;
                    result.TimeLength = timeLength3;
                    if (intelligenceMission7.Difficulty <= (int)threeMonthDifficulty)
                    {
                        result = intelligenceMission7;
                        result.TimeLength = timeLength2;
                        if (intelligenceMission7.Difficulty <= (int)oneMonthDifficulty)
                        {
                            result = intelligenceMission7;
                            result.TimeLength = timeLength;
                        }
                    }
                    return result;
                }
            }
            if (CanAssignIntelligenceMissionAgainstEmpire(targetEmpire, evaluation, relation, pirateRelation, IntelligenceMissionType.SabotageConstruction))
            {
                CalculateIntelligenceMissionSkill(agent, IntelligenceMissionType.SabotageConstruction, targetEmpire, out oneYearDifficulty, out threeMonthDifficulty, out oneMonthDifficulty);
                List<StellarObject> list4 = ResolveKnownBuiltObjectConstructionYards(targetEmpire);
                if (list4.Count > 0)
                {
                    int num4 = Galaxy.Rnd.Next(0, list4.Count);
                    for (int num5 = num4; num5 < list4.Count; num5++)
                    {
                        BuiltObject builtObject3 = (BuiltObject)list4[num5];
                        if (builtObject3 != null && !builtObject3.HasBeenDestroyed && builtObject3.ConstructionQueue != null && builtObject3.ConstructionQueue.ConstructionYards.CountUnderConstruction > 0)
                        {
                            IntelligenceMission intelligenceMission8 = new IntelligenceMission(this, null, IntelligenceMissionType.SabotageConstruction, _Galaxy.CurrentStarDate, builtObject3);
                            if (!CheckWhetherTargetOfIntelligenceMission(targetEmpire, builtObject3, IntelligenceMissionType.SabotageConstruction) && list.Contains(IntelligenceMissionType.SabotageConstruction) && intelligenceMission8.Difficulty <= (int)oneMonthDifficulty)
                            {
                                result = intelligenceMission8;
                                result.TimeLength = timeLength;
                                return result;
                            }
                        }
                    }
                    for (int num6 = 0; num6 < num4; num6++)
                    {
                        BuiltObject builtObject4 = (BuiltObject)list4[num6];
                        if (builtObject4 != null && !builtObject4.HasBeenDestroyed && builtObject4.ConstructionQueue != null && builtObject4.ConstructionQueue.ConstructionYards.CountUnderConstruction > 0)
                        {
                            IntelligenceMission intelligenceMission9 = new IntelligenceMission(this, null, IntelligenceMissionType.SabotageConstruction, _Galaxy.CurrentStarDate, builtObject4);
                            if (!CheckWhetherTargetOfIntelligenceMission(targetEmpire, builtObject4, IntelligenceMissionType.SabotageConstruction) && list.Contains(IntelligenceMissionType.SabotageConstruction) && intelligenceMission9.Difficulty <= (int)oneMonthDifficulty)
                            {
                                result = intelligenceMission9;
                                result.TimeLength = timeLength;
                                return result;
                            }
                        }
                    }
                }
                if (list3.Count > 0)
                {
                    int num7 = Galaxy.Rnd.Next(0, list3.Count);
                    for (int num8 = num7; num8 < list3.Count; num8++)
                    {
                        Habitat habitat3 = (Habitat)list3[num8];
                        if (habitat3 != null && !habitat3.HasBeenDestroyed && habitat3.ConstructionQueue != null && habitat3.ConstructionQueue.ConstructionYards.CountUnderConstruction > 0)
                        {
                            IntelligenceMission intelligenceMission10 = new IntelligenceMission(this, null, IntelligenceMissionType.SabotageConstruction, _Galaxy.CurrentStarDate, habitat3);
                            if (!CheckWhetherTargetOfIntelligenceMission(targetEmpire, habitat3, IntelligenceMissionType.SabotageConstruction) && list.Contains(IntelligenceMissionType.SabotageConstruction) && intelligenceMission10.Difficulty <= (int)oneMonthDifficulty)
                            {
                                result = intelligenceMission10;
                                result.TimeLength = timeLength;
                                return result;
                            }
                        }
                    }
                    for (int num9 = 0; num9 < num7; num9++)
                    {
                        Habitat habitat4 = (Habitat)list3[num9];
                        if (habitat4 != null && !habitat4.HasBeenDestroyed && habitat4.ConstructionQueue != null && habitat4.ConstructionQueue.ConstructionYards.CountUnderConstruction > 0)
                        {
                            IntelligenceMission intelligenceMission11 = new IntelligenceMission(this, null, IntelligenceMissionType.SabotageConstruction, _Galaxy.CurrentStarDate, habitat4);
                            if (!CheckWhetherTargetOfIntelligenceMission(targetEmpire, habitat4, IntelligenceMissionType.SabotageConstruction) && list.Contains(IntelligenceMissionType.SabotageConstruction) && intelligenceMission11.Difficulty <= (int)oneMonthDifficulty)
                            {
                                result = intelligenceMission11;
                                result.TimeLength = timeLength;
                                return result;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public List<StellarObject> ResolveKnownColonies(Empire targetEmpire)
        {
            List<StellarObject> list = new List<StellarObject>();
            for (int i = 0; i < targetEmpire.Colonies.Count; i++)
            {
                Habitat habitat = targetEmpire.Colonies[i];
                if (CheckSystemExplored(habitat.SystemIndex) && habitat.Owner == targetEmpire)
                {
                    list.Add(habitat);
                }
            }
            return list;
        }

        public CharacterList ResolveKnownCharacters(Empire targetEmpire)
        {
            CharacterList characterList = new CharacterList();
            for (int i = 0; i < targetEmpire.Characters.Count; i++)
            {
                Character character = targetEmpire.Characters[i];
                if (character == null || !character.Active || character.Role == CharacterRole.IntelligenceAgent)
                {
                    continue;
                }
                if (character.Role == CharacterRole.Leader || character.Role == CharacterRole.PirateLeader)
                {
                    if (IsObjectAreaKnownToThisEmpire(character.Location))
                    {
                        characterList.Add(character);
                    }
                }
                else if (character.Location != null && IsObjectVisibleToThisEmpire(character.Location))
                {
                    characterList.Add(character);
                }
            }
            return characterList;
        }

        public List<StellarObject> ResolveKnownBases(Empire targetEmpire)
        {
            List<StellarObject> list = new List<StellarObject>();
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(targetEmpire.BuiltObjects);
            builtObjectList.AddRange(targetEmpire.PrivateBuiltObjects);
            for (int i = 0; i < builtObjectList.Count; i++)
            {
                BuiltObject builtObject = builtObjectList[i];
                if (builtObject.Role != BuiltObjectRole.Base)
                {
                    continue;
                }
                if (builtObject.ParentHabitat != null)
                {
                    if (CheckSystemExplored(builtObject.ParentHabitat.SystemIndex))
                    {
                        list.Add(builtObject);
                    }
                }
                else if (IsObjectVisibleToThisEmpire(builtObject))
                {
                    list.Add(builtObject);
                }
            }
            return list;
        }

        public List<StellarObject> ResolveKnownBuiltObjectConstructionYards(Empire targetEmpire)
        {
            List<StellarObject> list = new List<StellarObject>();
            for (int i = 0; i < targetEmpire.ConstructionYards.Count; i++)
            {
                BuiltObject builtObject = targetEmpire.ConstructionYards[i];
                if (builtObject.ParentHabitat != null)
                {
                    if (CheckSystemExplored(builtObject.ParentHabitat.SystemIndex))
                    {
                        list.Add(builtObject);
                    }
                }
                else if (IsObjectVisibleToThisEmpire(builtObject))
                {
                    list.Add(builtObject);
                }
            }
            return list;
        }

        public List<StellarObject> ResolveKnownConstructionYards(Empire targetEmpire)
        {
            List<StellarObject> list = new List<StellarObject>();
            for (int i = 0; i < targetEmpire.Colonies.Count; i++)
            {
                Habitat habitat = targetEmpire.Colonies[i];
                if (CheckSystemExplored(habitat.SystemIndex) && habitat.Owner == targetEmpire)
                {
                    list.Add(habitat);
                }
            }
            for (int j = 0; j < targetEmpire.ConstructionYards.Count; j++)
            {
                BuiltObject builtObject = targetEmpire.ConstructionYards[j];
                if (builtObject.ParentHabitat != null)
                {
                    if (CheckSystemExplored(builtObject.ParentHabitat.SystemIndex))
                    {
                        list.Add(builtObject);
                    }
                }
                else if (IsObjectVisibleToThisEmpire(builtObject))
                {
                    list.Add(builtObject);
                }
            }
            return list;
        }

        private int DetermineEspionageMissionStrengthAssigned(Empire targetEmpire)
        {
            int num = 0;
            for (int i = 0; i < Characters.Count; i++)
            {
                Character character = Characters[i];
                if (character.Mission != null && character.Mission.Type != 0 && character.Mission.TargetEmpire == targetEmpire)
                {
                    switch (character.Mission.Type)
                    {
                        case IntelligenceMissionType.StealGalaxyMap:
                        case IntelligenceMissionType.StealOperationsMap:
                        case IntelligenceMissionType.StealTechData:
                        case IntelligenceMissionType.DeepCover:
                        case IntelligenceMissionType.StealTerritoryMap:
                            num += character.EspionageFactored;
                            break;
                    }
                }
            }
            return num;
        }

        private int DetermineSabotageMissionStrengthAssigned(Empire targetEmpire)
        {
            int num = 0;
            for (int i = 0; i < Characters.Count; i++)
            {
                Character character = Characters[i];
                if (character.Mission != null && character.Mission.Type != 0 && character.Mission.TargetEmpire == targetEmpire)
                {
                    switch (character.Mission.Type)
                    {
                        case IntelligenceMissionType.SabotageConstruction:
                        case IntelligenceMissionType.SabotageColony:
                        case IntelligenceMissionType.InciteRevolution:
                            num += character.SabotageFactored;
                            break;
                    }
                }
            }
            return num;
        }

        private bool CheckWhetherTargetOfIntelligenceMission(Empire targetEmpire, object potentialTarget, IntelligenceMissionType missionType)
        {
            if (potentialTarget is Habitat)
            {
                for (int i = 0; i < Characters.Count; i++)
                {
                    Character character = Characters[i];
                    if (character.Mission != null && character.Mission.Target is Habitat && character.Mission.Target == potentialTarget && character.Mission.Type == missionType)
                    {
                        return true;
                    }
                }
            }
            else if (potentialTarget is BuiltObject)
            {
                for (int j = 0; j < Characters.Count; j++)
                {
                    Character character2 = Characters[j];
                    if (character2.Mission != null && character2.Mission.Target is BuiltObject && character2.Mission.Target == potentialTarget && character2.Mission.Type == missionType)
                    {
                        return true;
                    }
                }
            }
            else if (potentialTarget is Character)
            {
                for (int k = 0; k < Characters.Count; k++)
                {
                    Character character3 = Characters[k];
                    if (character3.Mission != null && character3.Mission.Target is Character && character3.Mission.Target == potentialTarget && character3.Mission.Type == missionType)
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (missionType == IntelligenceMissionType.CounterIntelligence)
                {
                    return false;
                }
                for (int l = 0; l < Characters.Count; l++)
                {
                    Character character4 = Characters[l];
                    if (character4.Mission != null && character4.Mission.Type == missionType && character4.Mission.TargetEmpire == targetEmpire)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private IntelligenceMission DetermineEspionageMission(Empire targetEmpire, EmpireEvaluation evaluation, DiplomaticRelation relation, PirateRelation pirateRelation, Character agent)
        {
            IntelligenceMission result = null;
            List<IntelligenceMissionType> list = new List<IntelligenceMissionType>();
            if (relation != null)
            {
                if (relation.Type == DiplomaticRelationType.War)
                {
                    list.AddRange(new IntelligenceMissionType[6]
                    {
                    IntelligenceMissionType.DeepCover,
                    IntelligenceMissionType.StealOperationsMap,
                    IntelligenceMissionType.DestroyBase,
                    IntelligenceMissionType.AssassinateCharacter,
                    IntelligenceMissionType.SabotageConstruction,
                    IntelligenceMissionType.SabotageColony
                    });
                }
                else
                {
                    switch (relation.Strategy)
                    {
                        case DiplomaticStrategy.Conquer:
                            list.AddRange(new IntelligenceMissionType[5]
                            {
                        IntelligenceMissionType.DeepCover,
                        IntelligenceMissionType.StealOperationsMap,
                        IntelligenceMissionType.SabotageConstruction,
                        IntelligenceMissionType.DestroyBase,
                        IntelligenceMissionType.AssassinateCharacter
                            });
                            break;
                        case DiplomaticStrategy.Undermine:
                            list.AddRange(new IntelligenceMissionType[8]
                            {
                        IntelligenceMissionType.StealTerritoryMap,
                        IntelligenceMissionType.StealOperationsMap,
                        IntelligenceMissionType.StealTechData,
                        IntelligenceMissionType.SabotageColony,
                        IntelligenceMissionType.DestroyBase,
                        IntelligenceMissionType.AssassinateCharacter,
                        IntelligenceMissionType.InciteRevolution,
                        IntelligenceMissionType.DeepCover
                            });
                            break;
                        case DiplomaticStrategy.DefendUndermine:
                            list.AddRange(new IntelligenceMissionType[4]
                            {
                        IntelligenceMissionType.StealTerritoryMap,
                        IntelligenceMissionType.StealGalaxyMap,
                        IntelligenceMissionType.StealTechData,
                        IntelligenceMissionType.DeepCover
                            });
                            break;
                        case DiplomaticStrategy.Punish:
                            list.AddRange(new IntelligenceMissionType[4]
                            {
                        IntelligenceMissionType.SabotageConstruction,
                        IntelligenceMissionType.SabotageColony,
                        IntelligenceMissionType.DestroyBase,
                        IntelligenceMissionType.InciteRevolution
                            });
                            break;
                    }
                }
            }
            else if (pirateRelation != null)
            {
                switch (pirateRelation.Type)
                {
                    case PirateRelationType.None:
                        list.AddRange(new IntelligenceMissionType[9]
                        {
                    IntelligenceMissionType.DeepCover,
                    IntelligenceMissionType.StealOperationsMap,
                    IntelligenceMissionType.StealTerritoryMap,
                    IntelligenceMissionType.StealGalaxyMap,
                    IntelligenceMissionType.StealTechData,
                    IntelligenceMissionType.DestroyBase,
                    IntelligenceMissionType.AssassinateCharacter,
                    IntelligenceMissionType.SabotageConstruction,
                    IntelligenceMissionType.SabotageColony
                        });
                        break;
                    case PirateRelationType.Protection:
                        list.AddRange(new IntelligenceMissionType[5]
                        {
                    IntelligenceMissionType.DeepCover,
                    IntelligenceMissionType.StealOperationsMap,
                    IntelligenceMissionType.StealTerritoryMap,
                    IntelligenceMissionType.StealGalaxyMap,
                    IntelligenceMissionType.StealTechData
                        });
                        break;
                }
            }
            long timeLength = Galaxy.RealSecondsInGalacticYear * 1000 / 12;
            long timeLength2 = Galaxy.RealSecondsInGalacticYear * 1000 / 4;
            long timeLength3 = Galaxy.RealSecondsInGalacticYear * 1000;
            bool flag = false;
            for (int i = 0; i < Characters.Count; i++)
            {
                Character character = Characters[i];
                if (character.Role == CharacterRole.IntelligenceAgent && character.Mission != null && character.Mission.Type == IntelligenceMissionType.DeepCover && character.Mission.TargetEmpire == targetEmpire)
                {
                    flag = true;
                    break;
                }
            }
            IntelligenceMission intelligenceMission = new IntelligenceMission(this, null, IntelligenceMissionType.StealTerritoryMap, _Galaxy.CurrentStarDate, targetEmpire);
            IntelligenceMission intelligenceMission2 = new IntelligenceMission(this, null, IntelligenceMissionType.StealOperationsMap, _Galaxy.CurrentStarDate, targetEmpire);
            IntelligenceMission intelligenceMission3 = new IntelligenceMission(this, null, IntelligenceMissionType.StealGalaxyMap, _Galaxy.CurrentStarDate, targetEmpire);
            IntelligenceMission intelligenceMission4 = new IntelligenceMission(this, null, _Galaxy.CurrentStarDate, targetEmpire, null);
            ResearchNodeList researchNodeList = Research.ResolveMoreAdvancedProjects(targetEmpire, includeSpecialTech: false);
            if (researchNodeList != null && researchNodeList.Count > 0)
            {
                int index = Galaxy.Rnd.Next(0, researchNodeList.Count);
                intelligenceMission4.ResetResearchProject(researchNodeList[index]);
            }
            else
            {
                intelligenceMission4 = null;
            }
            IntelligenceMission intelligenceMission5 = new IntelligenceMission(this, null, IntelligenceMissionType.DeepCover, _Galaxy.CurrentStarDate, targetEmpire);
            double oneYearDifficulty = 0.0;
            double threeMonthDifficulty = 0.0;
            double oneMonthDifficulty = 0.0;
            if (!flag && !CheckForIntelligenceMissionsOfTypeAgainstEmpire(targetEmpire, IntelligenceMissionType.DeepCover) && list.Contains(IntelligenceMissionType.DeepCover) && CanAssignIntelligenceMissionAgainstEmpire(targetEmpire, evaluation, relation, pirateRelation, IntelligenceMissionType.DeepCover))
            {
                CalculateIntelligenceMissionSkill(agent, IntelligenceMissionType.DeepCover, targetEmpire, out oneYearDifficulty, out threeMonthDifficulty, out oneMonthDifficulty);
                if (intelligenceMission5.Difficulty <= (int)oneYearDifficulty)
                {
                    result = intelligenceMission5;
                    result.TimeLength = timeLength3;
                    if (intelligenceMission5.Difficulty <= (int)threeMonthDifficulty)
                    {
                        result = intelligenceMission5;
                        result.TimeLength = timeLength2;
                        if (intelligenceMission5.Difficulty <= (int)oneMonthDifficulty)
                        {
                            result = intelligenceMission5;
                            result.TimeLength = timeLength;
                        }
                    }
                    return result;
                }
            }
            if (intelligenceMission4 != null && !CheckForIntelligenceMissionsOfTypeAgainstEmpire(targetEmpire, IntelligenceMissionType.StealTechData) && list.Contains(IntelligenceMissionType.StealTechData) && CanAssignIntelligenceMissionAgainstEmpire(targetEmpire, evaluation, relation, pirateRelation, IntelligenceMissionType.StealTechData))
            {
                CalculateIntelligenceMissionSkill(agent, IntelligenceMissionType.StealTechData, targetEmpire, out oneYearDifficulty, out threeMonthDifficulty, out oneMonthDifficulty);
                if (intelligenceMission4.Difficulty <= (int)oneYearDifficulty)
                {
                    result = intelligenceMission4;
                    result.TimeLength = timeLength3;
                    if (intelligenceMission4.Difficulty <= (int)threeMonthDifficulty)
                    {
                        result = intelligenceMission4;
                        result.TimeLength = timeLength2;
                        if (intelligenceMission4.Difficulty <= (int)oneMonthDifficulty)
                        {
                            result = intelligenceMission4;
                            result.TimeLength = timeLength;
                        }
                    }
                    return result;
                }
            }
            if (!flag)
            {
                HabitatList habitatList = DetermineEmpireSystems(targetEmpire);
                int num = 0;
                foreach (Habitat item in habitatList)
                {
                    if (CheckSystemExplored(item.SystemIndex))
                    {
                        num++;
                    }
                }
                double num2 = (double)num / (double)habitatList.Count;
                if (num2 < 0.9 && !CheckForIntelligenceMissionsOfTypeAgainstEmpire(targetEmpire, IntelligenceMissionType.StealTerritoryMap) && list.Contains(IntelligenceMissionType.StealTerritoryMap) && CanAssignIntelligenceMissionAgainstEmpire(targetEmpire, evaluation, relation, pirateRelation, IntelligenceMissionType.StealTerritoryMap))
                {
                    CalculateIntelligenceMissionSkill(agent, IntelligenceMissionType.StealTerritoryMap, targetEmpire, out oneYearDifficulty, out threeMonthDifficulty, out oneMonthDifficulty);
                    if (intelligenceMission.Difficulty <= (int)oneYearDifficulty)
                    {
                        result = intelligenceMission;
                        result.TimeLength = timeLength3;
                        if (intelligenceMission.Difficulty <= (int)threeMonthDifficulty)
                        {
                            result = intelligenceMission;
                            result.TimeLength = timeLength2;
                            if (intelligenceMission.Difficulty <= (int)oneMonthDifficulty)
                            {
                                result = intelligenceMission;
                                result.TimeLength = timeLength;
                            }
                        }
                        return result;
                    }
                }
            }
            if (!flag && !CheckForIntelligenceMissionsOfTypeAgainstEmpire(targetEmpire, IntelligenceMissionType.StealOperationsMap) && list.Contains(IntelligenceMissionType.StealOperationsMap) && CanAssignIntelligenceMissionAgainstEmpire(targetEmpire, evaluation, relation, pirateRelation, IntelligenceMissionType.StealOperationsMap))
            {
                CalculateIntelligenceMissionSkill(agent, IntelligenceMissionType.StealOperationsMap, targetEmpire, out oneYearDifficulty, out threeMonthDifficulty, out oneMonthDifficulty);
                if (intelligenceMission2.Difficulty <= (int)oneYearDifficulty)
                {
                    result = intelligenceMission2;
                    result.TimeLength = timeLength3;
                    if (intelligenceMission2.Difficulty <= (int)threeMonthDifficulty)
                    {
                        result = intelligenceMission2;
                        result.TimeLength = timeLength2;
                        if (intelligenceMission2.Difficulty <= (int)oneMonthDifficulty)
                        {
                            result = intelligenceMission2;
                            result.TimeLength = timeLength;
                        }
                    }
                    return result;
                }
            }
            if (!flag && !CheckForIntelligenceMissionsOfTypeAgainstEmpire(targetEmpire, IntelligenceMissionType.StealGalaxyMap) && list.Contains(IntelligenceMissionType.StealGalaxyMap) && CanAssignIntelligenceMissionAgainstEmpire(targetEmpire, evaluation, relation, pirateRelation, IntelligenceMissionType.StealGalaxyMap))
            {
                CalculateIntelligenceMissionSkill(agent, IntelligenceMissionType.StealGalaxyMap, targetEmpire, out oneYearDifficulty, out threeMonthDifficulty, out oneMonthDifficulty);
                if (intelligenceMission3.Difficulty <= (int)oneYearDifficulty)
                {
                    result = intelligenceMission3;
                    result.TimeLength = timeLength3;
                    if (intelligenceMission3.Difficulty <= (int)threeMonthDifficulty)
                    {
                        result = intelligenceMission3;
                        result.TimeLength = timeLength2;
                        if (intelligenceMission3.Difficulty <= (int)oneMonthDifficulty)
                        {
                            result = intelligenceMission3;
                            result.TimeLength = timeLength;
                        }
                    }
                    return result;
                }
            }
            return result;
        }

        private bool CheckEspionageMissionAllowedAgainstEmpire(DiplomaticRelation relation, EmpireEvaluation evaluation, PirateRelation pirateRelation)
        {
            if (CheckEspionageMissionAllowedAgainstEmpirePolicy(relation, evaluation, pirateRelation))
            {
                if (pirateRelation == null)
                {
                    if (relation.Type == DiplomaticRelationType.War)
                    {
                        return true;
                    }
                    switch (relation.Strategy)
                    {
                        case DiplomaticStrategy.Conquer:
                        case DiplomaticStrategy.Undermine:
                        case DiplomaticStrategy.DefendUndermine:
                        case DiplomaticStrategy.Punish:
                            return true;
                        default:
                            return false;
                    }
                }
                if (pirateRelation.Type == PirateRelationType.None)
                {
                    return true;
                }
                if (pirateRelation.Evaluation < -15f)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckEspionageMissionAllowedAgainstEmpirePolicy(DiplomaticRelation relation, EmpireEvaluation evaluation, PirateRelation pirateRelation)
        {
            if (Policy != null)
            {
                if (pirateRelation != null)
                {
                    switch (Policy.IntelligenceUseEspionageAgainstEmpireWhen)
                    {
                        case 0:
                            return true;
                        case 1:
                            if (pirateRelation.Evaluation <= -10f)
                            {
                                return true;
                            }
                            break;
                        case 2:
                        case 3:
                        case 4:
                            if (pirateRelation.Type == PirateRelationType.None)
                            {
                                return true;
                            }
                            break;
                    }
                }
                else if (relation != null)
                {
                    switch (Policy.IntelligenceUseEspionageAgainstEmpireWhen)
                    {
                        case 0:
                            return true;
                        case 1:
                            if (evaluation != null && evaluation.OverallAttitude <= -10)
                            {
                                return true;
                            }
                            break;
                        case 2:
                            if (relation.Type == DiplomaticRelationType.None || relation.Type == DiplomaticRelationType.TradeSanctions || relation.Type == DiplomaticRelationType.War)
                            {
                                return true;
                            }
                            break;
                        case 3:
                            if (relation.Type == DiplomaticRelationType.TradeSanctions || relation.Type == DiplomaticRelationType.War)
                            {
                                return true;
                            }
                            break;
                        case 4:
                            if (relation.Type == DiplomaticRelationType.War)
                            {
                                return true;
                            }
                            break;
                    }
                }
            }
            return false;
        }

        private bool CheckSabotageMissionAllowedAgainstEmpire(DiplomaticRelation relation, EmpireEvaluation evaluation, PirateRelation pirateRelation)
        {
            if (CheckSabotageMissionAllowedAgainstEmpirePolicy(relation, evaluation, pirateRelation))
            {
                if (pirateRelation == null)
                {
                    if (relation.Type == DiplomaticRelationType.War)
                    {
                        return true;
                    }
                    DiplomaticStrategy strategy = relation.Strategy;
                    if (strategy == DiplomaticStrategy.Conquer || strategy == DiplomaticStrategy.Undermine || strategy == DiplomaticStrategy.Punish)
                    {
                        return true;
                    }
                    return false;
                }
                if (pirateRelation.Type == PirateRelationType.None)
                {
                    return true;
                }
                if (pirateRelation.Evaluation < -20f)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckSabotageMissionAllowedAgainstEmpirePolicy(DiplomaticRelation relation, EmpireEvaluation evaluation, PirateRelation pirateRelation)
        {
            if (Policy != null)
            {
                if (pirateRelation != null)
                {
                    switch (Policy.IntelligenceUseSabotageAgainstEmpireWhen)
                    {
                        case 0:
                            return true;
                        case 1:
                            if (pirateRelation.Evaluation <= -10f)
                            {
                                return true;
                            }
                            break;
                        case 2:
                        case 3:
                        case 4:
                            if (pirateRelation.Type == PirateRelationType.None)
                            {
                                return true;
                            }
                            break;
                    }
                }
                else if (relation != null)
                {
                    switch (Policy.IntelligenceUseSabotageAgainstEmpireWhen)
                    {
                        case 0:
                            return true;
                        case 1:
                            if (evaluation != null && evaluation.OverallAttitude <= -10)
                            {
                                return true;
                            }
                            break;
                        case 2:
                            if (relation.Type == DiplomaticRelationType.None || relation.Type == DiplomaticRelationType.TradeSanctions || relation.Type == DiplomaticRelationType.War)
                            {
                                return true;
                            }
                            break;
                        case 3:
                            if (relation.Type == DiplomaticRelationType.TradeSanctions || relation.Type == DiplomaticRelationType.War)
                            {
                                return true;
                            }
                            break;
                        case 4:
                            if (relation.Type == DiplomaticRelationType.War)
                            {
                                return true;
                            }
                            break;
                    }
                }
            }
            return false;
        }

        private bool CanAssignIntelligenceMissionAgainstEmpire(Empire empire, EmpireEvaluation evaluation, DiplomaticRelation relation, PirateRelation pirateRelation, IntelligenceMissionType missionType)
        {
            switch (missionType)
            {
                case IntelligenceMissionType.CounterIntelligence:
                    return true;
                case IntelligenceMissionType.DeepCover:
                    if (Policy.IntelligenceAllowMissionDeepCover)
                    {
                        return CheckEspionageMissionAllowedAgainstEmpire(relation, evaluation, pirateRelation);
                    }
                    break;
                case IntelligenceMissionType.AssassinateCharacter:
                    if (Policy.IntelligenceAllowMissionAssassinateCharacter)
                    {
                        return CheckSabotageMissionAllowedAgainstEmpire(relation, evaluation, pirateRelation);
                    }
                    break;
                case IntelligenceMissionType.DestroyBase:
                    if (Policy.IntelligenceAllowMissionDestroyBase)
                    {
                        return CheckSabotageMissionAllowedAgainstEmpire(relation, evaluation, pirateRelation);
                    }
                    break;
                case IntelligenceMissionType.InciteRevolution:
                    if (Policy.IntelligenceAllowMissionInciteRevolution)
                    {
                        return CheckSabotageMissionAllowedAgainstEmpire(relation, evaluation, pirateRelation);
                    }
                    break;
                case IntelligenceMissionType.SabotageColony:
                    if (Policy.IntelligenceAllowMissionSabotageColony)
                    {
                        return CheckSabotageMissionAllowedAgainstEmpire(relation, evaluation, pirateRelation);
                    }
                    break;
                case IntelligenceMissionType.SabotageConstruction:
                    if (Policy.IntelligenceAllowMissionSabotageConstruction)
                    {
                        return CheckSabotageMissionAllowedAgainstEmpire(relation, evaluation, pirateRelation);
                    }
                    break;
                case IntelligenceMissionType.StealGalaxyMap:
                    if (Policy.IntelligenceAllowMissionStealGalaxyMap)
                    {
                        return CheckEspionageMissionAllowedAgainstEmpire(relation, evaluation, pirateRelation);
                    }
                    break;
                case IntelligenceMissionType.StealOperationsMap:
                    if (Policy.IntelligenceAllowMissionStealOperationsMap)
                    {
                        return CheckEspionageMissionAllowedAgainstEmpire(relation, evaluation, pirateRelation);
                    }
                    break;
                case IntelligenceMissionType.StealTechData:
                    if (Policy.IntelligenceAllowMissionStealTechData)
                    {
                        return CheckEspionageMissionAllowedAgainstEmpire(relation, evaluation, pirateRelation);
                    }
                    break;
                case IntelligenceMissionType.StealTerritoryMap:
                    if (Policy.IntelligenceAllowMissionStealTerritoryMap)
                    {
                        return CheckEspionageMissionAllowedAgainstEmpire(relation, evaluation, pirateRelation);
                    }
                    break;
            }
            return false;
        }

        private void AssignSpecialMissionAgainstEmpire(Empire targetEmpire, EmpireEvaluation evaluation, out bool assignEspionageMission, out bool assignSabotageMission, double aggression, double caution, int empiresAtWarWith, double galaxyIntoleranceLevel, ref int refusalCount)
        {
            assignEspionageMission = false;
            assignSabotageMission = false;
            if (targetEmpire == this || targetEmpire == null)
            {
                return;
            }
            DiplomaticRelation diplomaticRelation = null;
            PirateRelation pirateRelation = null;
            if (PirateEmpireBaseHabitat == null && targetEmpire.PirateEmpireBaseHabitat == null)
            {
                diplomaticRelation = ObtainDiplomaticRelation(targetEmpire);
            }
            else
            {
                pirateRelation = ObtainPirateRelation(targetEmpire);
            }
            if ((diplomaticRelation != null && diplomaticRelation.Type == DiplomaticRelationType.War) || (pirateRelation != null && pirateRelation.Type == PirateRelationType.None && pirateRelation.Evaluation < -5f))
            {
                switch (Galaxy.Rnd.Next(0, 3))
                {
                    case 0:
                        if (AssignAgentForEspionageMission(targetEmpire, evaluation, diplomaticRelation, pirateRelation, ref refusalCount))
                        {
                            assignEspionageMission = true;
                        }
                        break;
                    case 1:
                        if (AssignAgentForSabotageMission(targetEmpire, evaluation, diplomaticRelation, pirateRelation, ref refusalCount))
                        {
                            assignSabotageMission = true;
                        }
                        break;
                }
            }
            else
            {
                if ((diplomaticRelation == null || (diplomaticRelation.Strategy != DiplomaticStrategy.Punish && diplomaticRelation.Strategy != DiplomaticStrategy.Conquer && diplomaticRelation.Strategy != DiplomaticStrategy.DefendUndermine && diplomaticRelation.Strategy != DiplomaticStrategy.Undermine)) && (pirateRelation == null || pirateRelation.Type != PirateRelationType.None || !(pirateRelation.Evaluation < -5f)))
                {
                    return;
                }
                switch (Galaxy.Rnd.Next(0, 3))
                {
                    case 0:
                        if (AssignAgentForEspionageMission(targetEmpire, evaluation, diplomaticRelation, pirateRelation, ref refusalCount))
                        {
                            assignEspionageMission = true;
                        }
                        break;
                    case 1:
                        if (diplomaticRelation != null && diplomaticRelation.Strategy != DiplomaticStrategy.DefendUndermine && AssignAgentForSabotageMission(targetEmpire, evaluation, diplomaticRelation, pirateRelation, ref refusalCount))
                        {
                            assignSabotageMission = true;
                        }
                        break;
                }
            }
        }

        private void CountAgentsAssigned(out int attackAgentsOnAssignment, out int defendAgentsOnAssignment, out int unassignedAgents)
        {
            attackAgentsOnAssignment = 0;
            defendAgentsOnAssignment = 0;
            unassignedAgents = 0;
            for (int i = 0; i < Characters.Count; i++)
            {
                Character character = Characters[i];
                if (character.Mission != null && character.Mission.Type != 0)
                {
                    if (character.Mission.Type == IntelligenceMissionType.CounterIntelligence)
                    {
                        defendAgentsOnAssignment++;
                    }
                    else
                    {
                        attackAgentsOnAssignment++;
                    }
                }
                else
                {
                    unassignedAgents++;
                }
            }
        }

        private void AssignSpecialMissions()
        {
            int refusalCount = 0;
            CharacterList characterList = new CharacterList();
            for (int i = 0; i < Characters.Count; i++)
            {
                Character character = Characters[i];
                if (character.Role == CharacterRole.IntelligenceAgent)
                {
                    characterList.Add(character);
                }
            }
            double num = Policy.IntelligenceCounterIntelligenceProportion / 100f;
            int num2 = (int)Math.Max(1.0, (double)characterList.Count * num);
            int num3 = characterList.Count - num2;
            double aggression = CalculateAggressionFactor();
            double caution = CalculateCautionFactor();
            double intoleranceLevel = _Galaxy.IntoleranceLevel;
            int attackAgentsOnAssignment = 0;
            int defendAgentsOnAssignment = 0;
            int unassignedAgents = 0;
            CountAgentsAssigned(out attackAgentsOnAssignment, out defendAgentsOnAssignment, out unassignedAgents);
            int num4 = num3 - attackAgentsOnAssignment;
            if (num4 > 0)
            {
                int empiresAtWarWith = CountEmpiresWeDeclaredWarOn() + CountEmpiresWhoDeclaredWarOnUs();
                if (PirateEmpireBaseHabitat == null)
                {
                    int num5 = Galaxy.Rnd.Next(0, EmpireEvaluations.Count);
                    for (int j = num5; j < EmpireEvaluations.Count; j++)
                    {
                        EmpireEvaluation empireEvaluation = EmpireEvaluations[j];
                        bool assignEspionageMission = false;
                        bool assignSabotageMission = false;
                        AssignSpecialMissionAgainstEmpire(empireEvaluation.Empire, empireEvaluation, out assignEspionageMission, out assignSabotageMission, aggression, caution, empiresAtWarWith, intoleranceLevel, ref refusalCount);
                        if (assignEspionageMission || assignSabotageMission)
                        {
                            num4--;
                        }
                        if (num4 <= 0)
                        {
                            break;
                        }
                    }
                    if (num4 > 0)
                    {
                        for (int k = 0; k < num5; k++)
                        {
                            EmpireEvaluation empireEvaluation2 = EmpireEvaluations[k];
                            bool assignEspionageMission2 = false;
                            bool assignSabotageMission2 = false;
                            AssignSpecialMissionAgainstEmpire(empireEvaluation2.Empire, empireEvaluation2, out assignEspionageMission2, out assignSabotageMission2, aggression, caution, empiresAtWarWith, intoleranceLevel, ref refusalCount);
                            if (assignEspionageMission2 || assignSabotageMission2)
                            {
                                num4--;
                            }
                            if (num4 <= 0)
                            {
                                break;
                            }
                        }
                    }
                    if (num4 > 0)
                    {
                        num5 = Galaxy.Rnd.Next(0, PirateRelations.Count);
                        for (int l = num5; l < PirateRelations.Count; l++)
                        {
                            PirateRelation pirateRelation = PirateRelations[l];
                            bool assignEspionageMission3 = false;
                            bool assignSabotageMission3 = false;
                            AssignSpecialMissionAgainstEmpire(pirateRelation.OtherEmpire, null, out assignEspionageMission3, out assignSabotageMission3, aggression, caution, empiresAtWarWith, intoleranceLevel, ref refusalCount);
                            if (assignEspionageMission3 || assignSabotageMission3)
                            {
                                num4--;
                            }
                            if (num4 <= 0)
                            {
                                break;
                            }
                        }
                        if (num4 > 0)
                        {
                            for (int m = 0; m < num5; m++)
                            {
                                PirateRelation pirateRelation2 = PirateRelations[m];
                                bool assignEspionageMission4 = false;
                                bool assignSabotageMission4 = false;
                                AssignSpecialMissionAgainstEmpire(pirateRelation2.OtherEmpire, null, out assignEspionageMission4, out assignSabotageMission4, aggression, caution, empiresAtWarWith, intoleranceLevel, ref refusalCount);
                                if (assignEspionageMission4 || assignSabotageMission4)
                                {
                                    num4--;
                                }
                                if (num4 <= 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    int num6 = Galaxy.Rnd.Next(0, PirateRelations.Count);
                    for (int n = num6; n < PirateRelations.Count; n++)
                    {
                        PirateRelation pirateRelation3 = PirateRelations[n];
                        bool assignEspionageMission5 = false;
                        bool assignSabotageMission5 = false;
                        AssignSpecialMissionAgainstEmpire(pirateRelation3.OtherEmpire, null, out assignEspionageMission5, out assignSabotageMission5, aggression, caution, empiresAtWarWith, intoleranceLevel, ref refusalCount);
                        if (assignEspionageMission5 || assignSabotageMission5)
                        {
                            num4--;
                        }
                        if (num4 <= 0)
                        {
                            break;
                        }
                    }
                    if (num4 > 0)
                    {
                        for (int num7 = 0; num7 < num6; num7++)
                        {
                            PirateRelation pirateRelation4 = PirateRelations[num7];
                            bool assignEspionageMission6 = false;
                            bool assignSabotageMission6 = false;
                            AssignSpecialMissionAgainstEmpire(pirateRelation4.OtherEmpire, null, out assignEspionageMission6, out assignSabotageMission6, aggression, caution, empiresAtWarWith, intoleranceLevel, ref refusalCount);
                            if (assignEspionageMission6 || assignSabotageMission6)
                            {
                                num4--;
                            }
                            if (num4 <= 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            for (int num8 = 0; num8 < Characters.Count; num8++)
            {
                Character character2 = Characters[num8];
                if (character2.Role == CharacterRole.IntelligenceAgent && (character2.Mission == null || character2.Mission.Type == IntelligenceMissionType.Undefined))
                {
                    IntelligenceMission intelligenceMission = new IntelligenceMission(this, character2, _Galaxy.CurrentStarDate);
                    intelligenceMission.TimeLength = Galaxy.RealSecondsInGalacticYear * 1000 / 4;
                    character2.Mission = intelligenceMission;
                }
            }
        }

        public int GetIntelligenceMissionSkillLevel(Character agent, IntelligenceMission mission)
        {
            int num = 0;
            if (agent != null && mission != null)
            {
                switch (mission.Type)
                {
                    case IntelligenceMissionType.CounterIntelligence:
                        num = agent.CounterEspionageFactored;
                        break;
                    case IntelligenceMissionType.DeepCover:
                        num = agent.ConcealmentFactored;
                        break;
                    case IntelligenceMissionType.AssassinateCharacter:
                        num = agent.AssassinationFactored;
                        break;
                    case IntelligenceMissionType.StealGalaxyMap:
                    case IntelligenceMissionType.StealOperationsMap:
                    case IntelligenceMissionType.StealTechData:
                    case IntelligenceMissionType.StealTerritoryMap:
                        num = agent.EspionageFactored;
                        if (mission.Type == IntelligenceMissionType.StealTechData && mission.TargetEmpire != null && mission.TargetEmpire.Characters != null)
                        {
                            if (mission.TargetEmpire.Characters.CheckCharactersForTrait(CharacterRole.Scientist, CharacterTraitType.ForeignSpy))
                            {
                                num *= 2;
                            }
                            else if (mission.TargetEmpire.Characters.CheckCharactersForTrait(CharacterRole.Scientist, CharacterTraitType.Patriot))
                            {
                                num /= 2;
                            }
                        }
                        break;
                    case IntelligenceMissionType.InciteRevolution:
                        num = agent.PsyOpsFactored;
                        break;
                    case IntelligenceMissionType.SabotageConstruction:
                    case IntelligenceMissionType.SabotageColony:
                    case IntelligenceMissionType.DestroyBase:
                        num = agent.SabotageFactored;
                        break;
                }
            }
            return num;
        }

        private void PerformIntelligenceMissions()
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            long num = Galaxy.RealSecondsInGalacticYear * 1000 / 12;
            long num2 = Galaxy.RealSecondsInGalacticYear * 1000 / 4;
            long num3 = Galaxy.RealSecondsInGalacticYear * 1000;
            CharacterList characterList = new CharacterList();
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire == this)
                {
                    continue;
                }
                for (int j = 0; j < empire.Characters.Count; j++)
                {
                    Character character = empire.Characters[j];
                    if (character != null && character.Role == CharacterRole.IntelligenceAgent)
                    {
                        IntelligenceMission mission = character.Mission;
                        if (mission != null && mission.Type != 0 && mission.TargetEmpire == this)
                        {
                            characterList.Add(character);
                        }
                    }
                }
            }
            for (int k = 0; k < _Galaxy.PirateEmpires.Count; k++)
            {
                Empire empire2 = _Galaxy.PirateEmpires[k];
                if (empire2 == this)
                {
                    continue;
                }
                for (int l = 0; l < empire2.Characters.Count; l++)
                {
                    Character character2 = empire2.Characters[l];
                    if (character2 != null && character2.Role == CharacterRole.IntelligenceAgent)
                    {
                        IntelligenceMission mission2 = character2.Mission;
                        if (mission2 != null && mission2.Type != 0 && mission2.TargetEmpire == this)
                        {
                            characterList.Add(character2);
                        }
                    }
                }
            }
            CharacterList characterList2 = new CharacterList();
            for (int m = 0; m < Characters.Count; m++)
            {
                Character character3 = Characters[m];
                if (character3 == null || character3.Role != CharacterRole.IntelligenceAgent)
                {
                    continue;
                }
                IntelligenceMission mission3 = character3.Mission;
                if (mission3 == null || mission3.Type == IntelligenceMissionType.Undefined)
                {
                    continue;
                }
                int num4 = 0;
                switch (mission3.Type)
                {
                    case IntelligenceMissionType.CounterIntelligence:
                        num4 = character3.CounterEspionageFactored;
                        break;
                    case IntelligenceMissionType.DeepCover:
                        num4 = character3.ConcealmentFactored;
                        break;
                    case IntelligenceMissionType.AssassinateCharacter:
                        num4 = character3.AssassinationFactored;
                        break;
                    case IntelligenceMissionType.StealGalaxyMap:
                    case IntelligenceMissionType.StealOperationsMap:
                    case IntelligenceMissionType.StealTechData:
                    case IntelligenceMissionType.StealTerritoryMap:
                        num4 = character3.EspionageFactored;
                        if (mission3.Type == IntelligenceMissionType.StealTechData && mission3.TargetEmpire != null && mission3.TargetEmpire.Characters != null)
                        {
                            if (mission3.TargetEmpire.Characters.CheckCharactersForTrait(CharacterRole.Scientist, CharacterTraitType.ForeignSpy))
                            {
                                num4 *= 2;
                            }
                            else if (mission3.TargetEmpire.Characters.CheckCharactersForTrait(CharacterRole.Scientist, CharacterTraitType.Patriot))
                            {
                                num4 /= 2;
                            }
                        }
                        break;
                    case IntelligenceMissionType.InciteRevolution:
                        num4 = character3.PsyOpsFactored;
                        break;
                    case IntelligenceMissionType.SabotageConstruction:
                    case IntelligenceMissionType.SabotageColony:
                    case IntelligenceMissionType.DestroyBase:
                        num4 = character3.SabotageFactored;
                        break;
                }
                double num5 = CalculateIntelligenceMissionBonusFromLeaderAndAmbassador(mission3.Type, mission3.TargetEmpire);
                num4 = (int)((double)num4 * num5);
                int num6 = num4;
                if (mission3.TimeLength <= num)
                {
                    num6 = num6;
                }
                else if (mission3.TimeLength <= num2)
                {
                    num6 *= 2;
                }
                else if (mission3.TimeLength <= num3)
                {
                    num6 *= 4;
                }
                num6 = (int)((double)num6 * (1.0 + EspionageBonus));
                long num7 = mission3.StartDate + mission3.TimeLength;
                bool flag = true;
                if (PirateEmpireBaseHabitat == null && mission3.TargetEmpire.PirateEmpireBaseHabitat == null)
                {
                    DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(mission3.TargetEmpire);
                    if (diplomaticRelation.Type == DiplomaticRelationType.War)
                    {
                        flag = false;
                    }
                }
                else
                {
                    PirateRelation pirateRelation = ObtainPirateRelation(mission3.TargetEmpire);
                    if (pirateRelation.Type == PirateRelationType.None)
                    {
                        flag = false;
                    }
                }
                IntelligenceMissionType type = mission3.Type;
                if (type == IntelligenceMissionType.CounterIntelligence)
                {
                    if (characterList.Count > 0)
                    {
                        int num8 = 150;
                        if (mission3.TimeLength >= num3)
                        {
                            num8 *= 12;
                        }
                        else if (mission3.TimeLength >= num2)
                        {
                            num8 *= 3;
                        }
                        int num9 = Galaxy.Rnd.Next(0, num8);
                        int num10 = (int)((double)num4 * (1.0 + EspionageBonus));
                        if (RaceEventType == RaceEventType.PredictiveHistory)
                        {
                            num9 = Galaxy.Rnd.Next(0, 100);
                        }
                        if (num10 > num9)
                        {
                            int index = Galaxy.Rnd.Next(0, characterList.Count);
                            Character character4 = characterList[index];
                            if (character4.Mission != null)
                            {
                                double num11 = CalculateIntelligenceMissionSuccessChance(character4.Mission, character4);
                                double num12 = 0.85;
                                if (RaceEventType == RaceEventType.PredictiveHistory)
                                {
                                    num12 = 1.15;
                                }
                                int intelligenceMissionSkillLevel = GetIntelligenceMissionSkillLevel(character4, character4.Mission);
                                double num13 = Math.Max(5.0, (double)intelligenceMissionSkillLevel - 10.0);
                                double num14 = Galaxy.Rnd.NextDouble() * num12 * Math.Sqrt((double)num10 / num13);
                                if (num14 > num11 && BaconEmpire.PerformIntelligenceMission(this))
                                {
                                    bool flag2 = false;
                                    bool flag3 = false;
                                    if (character4.Mission != null && character4.Mission.Type == IntelligenceMissionType.DeepCover && character4.Mission.Outcome == IntelligenceMissionOutcome.SucceedNotDetect)
                                    {
                                        int maxValue = 4;
                                        if (RaceEventType == RaceEventType.PredictiveHistory)
                                        {
                                            maxValue = 2;
                                        }
                                        if (Galaxy.Rnd.Next(0, maxValue) == 1)
                                        {
                                            flag2 = true;
                                        }
                                        flag3 = true;
                                    }
                                    else
                                    {
                                        flag2 = true;
                                    }
                                    if (!flag2)
                                    {
                                        continue;
                                    }
                                    int num15 = 5;
                                    if (character4.Mission != null)
                                    {
                                        num15 = Math.Min(30, character4.Mission.Difficulty / 30);
                                    }
                                    if (character4.Empire.PirateEmpireBaseHabitat == null && PirateEmpireBaseHabitat == null)
                                    {
                                        EmpireEvaluation empireEvaluation = EmpireEvaluations[character4.Empire];
                                        if (empireEvaluation != null)
                                        {
                                            empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - (double)num15;
                                        }
                                        DiplomaticRelation diplomaticRelation2 = ObtainDiplomaticRelation(character4.Empire);
                                        if (diplomaticRelation2.Type != DiplomaticRelationType.War && character4.Empire != null)
                                        {
                                            character4.Empire.CivilityRating -= 1 + num15 / 8;
                                        }
                                    }
                                    else if (character4.Empire != null)
                                    {
                                        PirateRelation pirateRelation2 = ObtainPirateRelation(character4.Empire);
                                        pirateRelation2.EvaluationDetectedIntelligenceMissions -= num15;
                                        if (pirateRelation2.Type == PirateRelationType.Protection && character4.Empire != null)
                                        {
                                            character4.Empire.CivilityRating -= 1 + num15 / 8;
                                        }
                                    }
                                    if (character4.Mission != null && character4.Mission.Type == IntelligenceMissionType.DeepCover)
                                    {
                                        int num16 = character4.Empire.EmpiresViewable.IndexOf(this);
                                        while (num16 >= 0 && character4.Empire.EmpiresViewableExpiry[num16] != long.MaxValue)
                                        {
                                            num16 = character4.Empire.EmpiresViewable.IndexOf(this, num16 + 1);
                                        }
                                        if (num16 >= 0)
                                        {
                                            character4.Empire.EmpiresViewable.RemoveAt(num16);
                                            character4.Empire.EmpiresViewableExpiry.RemoveAt(num16);
                                        }
                                    }
                                    if (character4.Mission != null && character4.Mission.Type == IntelligenceMissionType.AssassinateCharacter && character4.Mission.Target is Character)
                                    {
                                        Character character5 = (Character)character4.Mission.Target;
                                        if (character5 != null)
                                        {
                                            _Galaxy.DoCharacterEvent(CharacterEventType.TargetOfFailedAssassination, character4, character5);
                                        }
                                    }
                                    _Galaxy.DoCharacterEvent(CharacterEventType.IntelligenceMissionInterceptEnemy, character4, character3, includeLeader: true, character3.Empire);
                                    Counters.ProcessIntelligenceMissionOutcome(mission3, IntelligenceMissionOutcome.SucceedNotDetect);
                                    character4.Empire.Counters.ProcessIntelligenceMissionOutcome(character4.Mission, IntelligenceMissionOutcome.Capture);
                                    if (character4.Empire != null)
                                    {
                                        string arg = Galaxy.ResolveDescription(character4.Mission, character4.Empire);
                                        string description = string.Format(TextResolver.GetText("Our Agent Captured"), character4.Name, arg);
                                        character4.Empire.SendMessageToEmpire(character4.Empire, EmpireMessageType.CharacterDeath, character4, description);
                                        arg = Galaxy.ResolveDescription(character4.Mission, this);
                                        description = string.Format(TextResolver.GetText("Enemy Agent Captured"), character4.Name, character4.Empire.Name, arg);
                                        if (flag3)
                                        {
                                            description = string.Format(TextResolver.GetText("Enemy Deep Cover Agent Captured"), character4.Name, character4.Empire.Name);
                                        }
                                        SendMessageToEmpire(this, EmpireMessageType.CharacterDeath, character4, description);
                                    }
                                    if (Counters != null)
                                    {
                                        Counters.ProcessCharacterDeath(character4);
                                    }
                                    character4.Kill(_Galaxy);
                                    continue;
                                }
                            }
                        }
                    }
                    if (currentStarDate >= num7)
                    {
                        character3.Mission = null;
                        IntelligenceMission intelligenceMission = new IntelligenceMission(this, character3, _Galaxy.CurrentStarDate);
                        intelligenceMission.TimeLength = Galaxy.RealSecondsInGalacticYear * 1000 / 4;
                        character3.Mission = intelligenceMission;
                    }
                }
                else if (mission3.Type == IntelligenceMissionType.DeepCover && mission3.Outcome == IntelligenceMissionOutcome.SucceedNotDetect)
                {
                    _Galaxy.MergeGalaxyMap(mission3.TargetEmpire, this);
                }
                else
                {
                    if (currentStarDate < num7)
                    {
                        continue;
                    }
                    IntelligenceMissionOutcome intelligenceMissionOutcome = DetermineIntelligenceMissionOutcome(mission3, character3);
                    Counters.ProcessIntelligenceMissionOutcome(mission3, intelligenceMissionOutcome);
                    if (mission3.Type != IntelligenceMissionType.CounterIntelligence)
                    {
                        switch (intelligenceMissionOutcome)
                        {
                            case IntelligenceMissionOutcome.FailDetect:
                            case IntelligenceMissionOutcome.Capture:
                                if (mission3.TargetEmpire != null && mission3.TargetEmpire.Counters != null)
                                {
                                    mission3.TargetEmpire.Counters.IntelligenceMissionSuccessCounterIntelligenceCount++;
                                }
                                break;
                        }
                    }
                    int num17 = Math.Min(30, mission3.Difficulty / 8);
                    mission3.Outcome = intelligenceMissionOutcome;
                    EmpireEvaluation empireEvaluation2 = null;
                    PirateRelation pirateRelation3 = null;
                    if (mission3.TargetEmpire.PirateEmpireBaseHabitat == null && PirateEmpireBaseHabitat == null)
                    {
                        empireEvaluation2 = mission3.TargetEmpire.ObtainEmpireEvaluation(this);
                    }
                    else
                    {
                        pirateRelation3 = mission3.TargetEmpire.ObtainPirateRelation(this);
                    }
                    string empty = string.Empty;
                    Galaxy.Rnd.NextDouble();
                    _ = EspionageBonus;
                    _ = (double)mission3.Difficulty / 10.0;
                    switch (intelligenceMissionOutcome)
                    {
                        case IntelligenceMissionOutcome.Capture:
                            MarkEmpireAsRecentSpy(character3.Empire, mission3.TargetEmpire);
                            if (empireEvaluation2 != null)
                            {
                                empireEvaluation2.IncidentEvaluation = empireEvaluation2.IncidentEvaluationRaw - (double)num17;
                            }
                            else if (pirateRelation3 != null)
                            {
                                pirateRelation3.EvaluationDetectedIntelligenceMissions -= num17;
                            }
                            if (flag)
                            {
                                character3.Empire.CivilityRating -= 1 + num17 / 8;
                            }
                            characterList2.Add(character3);
                            _Galaxy.DoCharacterEvent(CharacterEventType.IntelligenceAgentOursCaptured, character3, character3, includeLeader: true, character3.Empire);
                            if (mission3 != null && mission3.Type == IntelligenceMissionType.AssassinateCharacter && mission3.Target is Character)
                            {
                                Character character7 = (Character)mission3.Target;
                                if (character7 != null)
                                {
                                    _Galaxy.DoCharacterEvent(CharacterEventType.TargetOfFailedAssassination, character3, character7);
                                }
                            }
                            empty = string.Format(TextResolver.GetText("Our Agent Captured In Act"), character3.Name, Galaxy.ResolveDescription(mission3, this));
                            SendMessageToEmpire(this, EmpireMessageType.CharacterDeath, character3, empty);
                            empty = string.Format(TextResolver.GetText("Enemy Agent Captured In Act"), character3.Name, Name, Galaxy.ResolveDescription(mission3, mission3.TargetEmpire));
                            mission3.TargetEmpire.SendMessageToEmpire(mission3.TargetEmpire, EmpireMessageType.CharacterDeath, character3, empty);
                            break;
                        case IntelligenceMissionOutcome.FailDetect:
                            MarkEmpireAsRecentSpy(character3.Empire, mission3.TargetEmpire);
                            if (empireEvaluation2 != null)
                            {
                                empireEvaluation2.IncidentEvaluation = empireEvaluation2.IncidentEvaluationRaw - (double)num17;
                            }
                            else if (pirateRelation3 != null)
                            {
                                pirateRelation3.EvaluationDetectedIntelligenceMissions -= num17;
                            }
                            if (flag)
                            {
                                character3.Empire.CivilityRating -= 1 + num17 / 8;
                            }
                            switch (mission3.Type)
                            {
                                case IntelligenceMissionType.StealGalaxyMap:
                                case IntelligenceMissionType.StealOperationsMap:
                                case IntelligenceMissionType.StealTechData:
                                case IntelligenceMissionType.DeepCover:
                                case IntelligenceMissionType.StealTerritoryMap:
                                    _Galaxy.DoCharacterEvent(CharacterEventType.IntelligenceMissionFailEspionage, mission3, character3, includeLeader: true, character3.Empire);
                                    break;
                                case IntelligenceMissionType.SabotageConstruction:
                                case IntelligenceMissionType.SabotageColony:
                                case IntelligenceMissionType.InciteRevolution:
                                case IntelligenceMissionType.AssassinateCharacter:
                                case IntelligenceMissionType.DestroyBase:
                                    _Galaxy.DoCharacterEvent(CharacterEventType.IntelligenceMissionFailSabotage, mission3, character3, includeLeader: true, character3.Empire);
                                    break;
                            }
                            if (mission3 != null && mission3.Type == IntelligenceMissionType.AssassinateCharacter && mission3.Target is Character)
                            {
                                Character character6 = (Character)mission3.Target;
                                if (character6 != null)
                                {
                                    _Galaxy.DoCharacterEvent(CharacterEventType.TargetOfFailedAssassination, character3, character6);
                                }
                            }
                            empty = string.Format(TextResolver.GetText("Our Agent Detect Fail"), character3.Name, Galaxy.ResolveDescription(mission3, this));
                            SendMessageToEmpire(this, EmpireMessageType.CharacterMissionFailure, character3, empty);
                            empty = string.Format(TextResolver.GetText("Enemy Agent Detect Fail"), character3.Name, Name, Galaxy.ResolveDescription(mission3, mission3.TargetEmpire));
                            mission3.TargetEmpire.SendMessageToEmpire(mission3.TargetEmpire, EmpireMessageType.CharacterMissionFailure, character3, empty);
                            break;
                        case IntelligenceMissionOutcome.SucceedDetect:
                            MarkEmpireAsRecentSpy(character3.Empire, mission3.TargetEmpire);
                            switch (mission3.Type)
                            {
                                case IntelligenceMissionType.StealGalaxyMap:
                                case IntelligenceMissionType.StealOperationsMap:
                                case IntelligenceMissionType.StealTechData:
                                case IntelligenceMissionType.DeepCover:
                                case IntelligenceMissionType.CounterIntelligence:
                                case IntelligenceMissionType.StealTerritoryMap:
                                    {
                                        CharacterList characterList3 = new CharacterList();
                                        characterList3.Add(character3);
                                        if (mission3.Type == IntelligenceMissionType.StealTechData && mission3.TargetEmpire != null && mission3.TargetEmpire.Characters != null)
                                        {
                                            characterList3.AddRange(mission3.TargetEmpire.Characters.GetCharactersByRole(CharacterRole.Scientist));
                                        }
                                        _Galaxy.DoCharacterEvent(CharacterEventType.IntelligenceMissionSucceedEspionage, mission3, characterList3, includeLeader: true, character3.Empire);
                                        break;
                                    }
                                case IntelligenceMissionType.SabotageConstruction:
                                case IntelligenceMissionType.SabotageColony:
                                case IntelligenceMissionType.InciteRevolution:
                                case IntelligenceMissionType.AssassinateCharacter:
                                case IntelligenceMissionType.DestroyBase:
                                    _Galaxy.DoCharacterEvent(CharacterEventType.IntelligenceMissionSucceedSabotage, mission3, character3);
                                    break;
                            }
                            if (empireEvaluation2 != null)
                            {
                                empireEvaluation2.IncidentEvaluation = empireEvaluation2.IncidentEvaluationRaw - (double)num17;
                            }
                            else if (pirateRelation3 != null)
                            {
                                pirateRelation3.EvaluationDetectedIntelligenceMissions -= num17;
                            }
                            if (flag)
                            {
                                character3.Empire.CivilityRating -= 1 + num17 / 8;
                            }
                            empty = string.Format(TextResolver.GetText("Our Agent Detect Succeed"), character3.Name, Galaxy.ResolveDescription(mission3, this));
                            CompleteIntelligenceMission(mission3);
                            SendMessageToEmpire(this, EmpireMessageType.CharacterMissionAccomplished, character3, empty);
                            empty = string.Format(TextResolver.GetText("Enemy Agent Detect Succeed"), character3.Name, Name, Galaxy.ResolveDescription(mission3, mission3.TargetEmpire));
                            mission3.TargetEmpire.SendMessageToEmpire(mission3.TargetEmpire, EmpireMessageType.CharacterMissionFailure, character3, empty);
                            break;
                        case IntelligenceMissionOutcome.FailNotDetect:
                            switch (mission3.Type)
                            {
                                case IntelligenceMissionType.StealGalaxyMap:
                                case IntelligenceMissionType.StealOperationsMap:
                                case IntelligenceMissionType.StealTechData:
                                case IntelligenceMissionType.DeepCover:
                                case IntelligenceMissionType.StealTerritoryMap:
                                    _Galaxy.DoCharacterEvent(CharacterEventType.IntelligenceMissionFailEspionage, mission3, character3, includeLeader: true, character3.Empire);
                                    break;
                                case IntelligenceMissionType.SabotageConstruction:
                                case IntelligenceMissionType.SabotageColony:
                                case IntelligenceMissionType.InciteRevolution:
                                case IntelligenceMissionType.AssassinateCharacter:
                                case IntelligenceMissionType.DestroyBase:
                                    _Galaxy.DoCharacterEvent(CharacterEventType.IntelligenceMissionFailSabotage, mission3, character3, includeLeader: true, character3.Empire);
                                    break;
                            }
                            empty = string.Format(TextResolver.GetText("Our Agent Fail"), character3.Name, Galaxy.ResolveDescription(mission3, this));
                            SendMessageToEmpire(this, EmpireMessageType.CharacterMissionFailure, character3, empty);
                            break;
                        case IntelligenceMissionOutcome.SucceedNotDetect:
                            switch (mission3.Type)
                            {
                                case IntelligenceMissionType.StealGalaxyMap:
                                case IntelligenceMissionType.StealOperationsMap:
                                case IntelligenceMissionType.StealTechData:
                                case IntelligenceMissionType.DeepCover:
                                case IntelligenceMissionType.CounterIntelligence:
                                case IntelligenceMissionType.StealTerritoryMap:
                                    _Galaxy.DoCharacterEvent(CharacterEventType.IntelligenceMissionSucceedEspionage, mission3, character3);
                                    break;
                                case IntelligenceMissionType.SabotageConstruction:
                                case IntelligenceMissionType.SabotageColony:
                                case IntelligenceMissionType.InciteRevolution:
                                case IntelligenceMissionType.AssassinateCharacter:
                                case IntelligenceMissionType.DestroyBase:
                                    _Galaxy.DoCharacterEvent(CharacterEventType.IntelligenceMissionSucceedSabotage, mission3, character3);
                                    break;
                            }
                            empty = string.Format(TextResolver.GetText("Our Agent Succeed"), character3.Name, Galaxy.ResolveDescription(mission3, this));
                            CompleteIntelligenceMission(mission3);
                            SendMessageToEmpire(this, EmpireMessageType.CharacterMissionAccomplished, character3, empty);
                            if (mission3.TargetEmpire.PirateEmpireBaseHabitat != null)
                            {
                                break;
                            }
                            if (mission3.Type == IntelligenceMissionType.InciteRevolution)
                            {
                                string description2 = string.Format(TextResolver.GetText("Agent Revolution"), mission3.TargetEmpire.GovernmentAttributes.Name);
                                mission3.TargetEmpire.SendMessageToEmpire(mission3.TargetEmpire, EmpireMessageType.Revolution, null, description2);
                            }
                            else if (mission3.Type == IntelligenceMissionType.SabotageColony && mission3.Target != null && mission3.Target is Habitat)
                            {
                                Habitat habitat = (Habitat)mission3.Target;
                                if (habitat != null)
                                {
                                    string description3 = string.Format(TextResolver.GetText("Agent Rebellion"), habitat.Name);
                                    mission3.TargetEmpire.SendMessageToEmpire(mission3.TargetEmpire, EmpireMessageType.ColonyRebelling, habitat, description3);
                                }
                            }
                            break;
                    }
                    if (mission3.Type != IntelligenceMissionType.DeepCover || mission3.Outcome != IntelligenceMissionOutcome.SucceedNotDetect)
                    {
                        BaconEmpire.ResetSpyMission(characterList2, character3);
                    }
                }
            }
            foreach (Character item in characterList2)
            {
                if (Counters != null)
                {
                    Counters.ProcessCharacterDeath(item);
                }
                item.Kill(_Galaxy);
            }
        }

    }
}
