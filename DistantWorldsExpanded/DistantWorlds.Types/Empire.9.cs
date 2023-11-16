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
        public ResourceList DetermineFuelRequiredForFleet(ShipGroup fleet, bool setFuelLevelToZero, out int fleetFuelCapacity)
        {
            ResourceList resourceList = new ResourceList();
            int num = 0;
            fleetFuelCapacity = 0;
            if (fleet != null && fleet.Ships != null)
            {
                for (int i = 0; i < fleet.Ships.Count; i++)
                {
                    BuiltObject builtObject = fleet.Ships[i];
                    if (builtObject != null && builtObject.FuelType != null)
                    {
                        int num2 = 1;
                        if (!setFuelLevelToZero)
                        {
                            num2 = builtObject.FuelCapacity - (int)builtObject.CurrentFuel;
                        }
                        int num3 = resourceList.IndexOf(builtObject.FuelType.ResourceID);
                        if (num3 >= 0)
                        {
                            resourceList[num3].SortTag += num2;
                        }
                        else
                        {
                            Resource resource = new Resource(builtObject.FuelType.ResourceID);
                            resource.SortTag = num2;
                            resourceList.Add(resource);
                        }
                        num += num2;
                        fleetFuelCapacity += builtObject.FuelCapacity;
                    }
                }
            }
            return resourceList;
        }

        private void TaskResupplyShips()
        {
            EmpireList targetEmpires = IdentifyTargetEmpires();
            for (int i = 0; i < ResupplyShips.Count; i++)
            {
                TaskResupplyShip(ResupplyShips[i], targetEmpires);
            }
        }

        private void TaskResupplyShip(BuiltObject resupplyShip, EmpireList targetEmpires)
        {
            if (!resupplyShip.IsFunctional || !resupplyShip.IsAutoControlled || (resupplyShip.Mission != null && resupplyShip.Mission.Type != 0) || resupplyShip.UnbuiltOrDamagedComponentCount != 0 || resupplyShip.BuiltAt != null)
            {
                return;
            }
            double num = 2500000.0;
            if (resupplyShip.IsDeployed)
            {
                bool flag = true;
                for (int i = 0; i < ShipGroups.Count; i++)
                {
                    ShipGroup shipGroup = ShipGroups[i];
                    if (shipGroup.Mission != null && (shipGroup.Mission.TargetBuiltObject == resupplyShip || shipGroup.Mission.SecondaryTargetBuiltObject == resupplyShip))
                    {
                        flag = false;
                        break;
                    }
                }
                if (!flag)
                {
                    return;
                }
                bool flag2 = true;
                double num2 = double.MaxValue;
                if (targetEmpires.Count > 0)
                {
                    for (int j = 0; j < targetEmpires.Count; j++)
                    {
                        StellarObjectList stellarObjectList = new StellarObjectList();
                        if (PirateEmpireBaseHabitat != null)
                        {
                            PrioritizedTargetList prioritizedTargetList = IdentifyEmpireStrikePoints(targetEmpires[j]);
                            stellarObjectList = prioritizedTargetList.ResolveStellarObjects();
                        }
                        else
                        {
                            DiplomaticRelation relation = ObtainDiplomaticRelation(targetEmpires[j]);
                            AddWarObjectivesToList(relation, stellarObjectList, calculateWarObjectivesIfNotPresent: false);
                        }
                        if (stellarObjectList.Count <= 0)
                        {
                            continue;
                        }
                        int count = stellarObjectList.Count;
                        for (int k = 0; k < count; k++)
                        {
                            double num3 = _Galaxy.CalculateDistance(resupplyShip.Xpos, resupplyShip.Ypos, stellarObjectList[k].Xpos, stellarObjectList[k].Ypos);
                            if (num3 < num2)
                            {
                                num2 = num3;
                            }
                            if (num2 < num)
                            {
                                flag2 = false;
                                break;
                            }
                        }
                        if (!flag2)
                        {
                            break;
                        }
                    }
                }
                if (flag2)
                {
                    resupplyShip.InitiateUndeploy();
                }
            }
            else
            {
                if (resupplyShip.DeployProgress != 0.0)
                {
                    return;
                }
                Habitat habitat = null;
                if (targetEmpires.Count > 0)
                {
                    for (int l = 0; l < targetEmpires.Count; l++)
                    {
                        StellarObjectList stellarObjectList2 = new StellarObjectList();
                        if (PirateEmpireBaseHabitat != null)
                        {
                            PrioritizedTargetList prioritizedTargetList2 = IdentifyEmpireStrikePoints(targetEmpires[l]);
                            stellarObjectList2 = prioritizedTargetList2.ResolveStellarObjects();
                        }
                        else
                        {
                            DiplomaticRelation relation2 = ObtainDiplomaticRelation(targetEmpires[l]);
                            AddWarObjectivesToList(relation2, stellarObjectList2, calculateWarObjectivesIfNotPresent: false);
                        }
                        if (stellarObjectList2.Count > 0)
                        {
                            for (int m = 0; m < stellarObjectList2.Count; m++)
                            {
                                double xpos = stellarObjectList2[m].Xpos;
                                double ypos = stellarObjectList2[m].Ypos;
                                BuiltObject builtObject = _Galaxy.FastFindNearestResupplyShipByDestination(xpos, ypos, this, resupplyShip);
                                BuiltObject builtObject2 = _Galaxy.FastFindNearestSpacePort(xpos, ypos, this);
                                double num4 = double.MaxValue;
                                if (builtObject != null)
                                {
                                    double x = 0.0;
                                    double y = 0.0;
                                    _Galaxy.DetermineResupplyShipLocationByDestination(builtObject, out x, out y);
                                    num4 = _Galaxy.CalculateDistance(xpos, ypos, x, y);
                                }
                                double num5 = double.MaxValue;
                                if (builtObject2 != null)
                                {
                                    num5 = _Galaxy.CalculateDistance(xpos, ypos, builtObject2.Xpos, builtObject2.Ypos);
                                }
                                if (num4 > Galaxy.ResupplyShipMinimumDistance && num5 > Galaxy.ResupplyShipMinimumDistance)
                                {
                                    double num6 = xpos;
                                    double num7 = ypos;
                                    byte resourceId = 0;
                                    if (ShipGroups != null && ShipGroups.Count > 0 && ShipGroups[0].LeadShip != null && ShipGroups[0].LeadShip.FuelType != null)
                                    {
                                        resourceId = ShipGroups[0].LeadShip.FuelType.ResourceID;
                                    }
                                    int num8 = 0;
                                    while (habitat == null && num8 < 50)
                                    {
                                        Habitat habitat2 = _Galaxy.FastFindNearestFuelHabitatAlternate(num6, num7, resourceId, null, this);
                                        if (habitat2 != null && !targetEmpires[l].IsObjectVisibleToThisEmpire(habitat2) && (_Galaxy.Systems[habitat2.SystemIndex].DominantEmpire == null || _Galaxy.Systems[habitat2.SystemIndex].DominantEmpire.Empire == null))
                                        {
                                            builtObject = _Galaxy.FastFindNearestResupplyShipByDestination(habitat2.Xpos, habitat2.Ypos, this, resupplyShip);
                                            num4 = double.MaxValue;
                                            if (builtObject != null)
                                            {
                                                double x2 = 0.0;
                                                double y2 = 0.0;
                                                _Galaxy.DetermineResupplyShipLocationByDestination(builtObject, out x2, out y2);
                                                num4 = _Galaxy.CalculateDistance(habitat2.Xpos, habitat2.Ypos, x2, y2);
                                            }
                                            if (num4 > Galaxy.ResupplyShipMinimumDistance)
                                            {
                                                double num9 = _Galaxy.CalculateDistance(xpos, ypos, habitat2.Xpos, habitat2.Ypos);
                                                if (num9 < num && num5 > num9)
                                                {
                                                    habitat = habitat2;
                                                    break;
                                                }
                                            }
                                        }
                                        double x3 = 0.0;
                                        double y3 = 0.0;
                                        _Galaxy.SelectRelativePoint(300000.0, out x3, out y3);
                                        num6 += x3;
                                        num7 += y3;
                                        num8++;
                                    }
                                }
                                if (habitat != null)
                                {
                                    break;
                                }
                            }
                        }
                        if (habitat != null)
                        {
                            break;
                        }
                    }
                }
                if (habitat != null)
                {
                    resupplyShip.AssignMission(BuiltObjectMissionType.Deploy, habitat, null, BuiltObjectMissionPriority.High);
                    return;
                }
                bool flag3 = false;
                if (resupplyShip.ParentHabitat != null && resupplyShip.ParentHabitat.BasesAtHabitat != null && resupplyShip.ParentHabitat.BasesAtHabitat.Count > 0)
                {
                    foreach (BuiltObject item in resupplyShip.ParentHabitat.BasesAtHabitat)
                    {
                        if (item.SubRole == BuiltObjectSubRole.SmallSpacePort || item.SubRole == BuiltObjectSubRole.MediumSpacePort || item.SubRole == BuiltObjectSubRole.LargeSpacePort)
                        {
                            flag3 = true;
                            break;
                        }
                    }
                }
                if (SpacePorts.Count <= 0 || flag3)
                {
                    return;
                }
                BuiltObject builtObject3 = _Galaxy.FastFindNearestSpacePort(resupplyShip.Xpos, resupplyShip.Ypos, this);
                if (builtObject3 != null)
                {
                    double num10 = resupplyShip.CurrentFuel / Math.Max(1.0, resupplyShip.FuelCapacity);
                    if (num10 < 0.5)
                    {
                        resupplyShip.AssignMission(BuiltObjectMissionType.Refuel, builtObject3, null, BuiltObjectMissionPriority.Unavailable);
                        return;
                    }
                    double x4 = -2000000001.0;
                    double y4 = -2000000001.0;
                    _Galaxy.SelectRelativeParkingPoint(out x4, out y4);
                    resupplyShip.AssignMission(BuiltObjectMissionType.Move, builtObject3, null, x4, y4, BuiltObjectMissionPriority.Low);
                }
            }
        }

        public StellarObject DecideBestFleetRefuelPoint(double x, double y, Empire empire, ResourceList requiredFuel, Empire empireToExclude)
        {
            BuiltObject shipToRefuel = null;
            for (int i = 0; i < empire.BuiltObjects.Count; i++)
            {
                if (empire.BuiltObjects[i].Role == BuiltObjectRole.Military)
                {
                    shipToRefuel = empire.BuiltObjects[i];
                    break;
                }
            }
            return UltraFastFindNearestRefuellingLocation(x, y, requiredFuel, shipToRefuel, mustHaveActualSupply: false, includeResupplyShips: true, 10);
        }

        public bool AssignShipSystemPatrol(BuiltObject ship, SystemInfo system)
        {
            return AssignShipSystemPatrol(ship, system, manuallyAssigned: false);
        }

        public bool AssignShipSystemPatrol(BuiltObject ship, SystemInfo system, bool manuallyAssigned)
        {
            StellarObjectList stellarObjectList = IdentifyEmpireAssetsInSystem(system);
            if (stellarObjectList.Count > 0)
            {
                StellarObject stellarObject = stellarObjectList[Galaxy.Rnd.Next(0, stellarObjectList.Count)];
                if (stellarObject is BuiltObject)
                {
                    ship.ClearPreviousMissionRequirements();
                    ship.AssignMission(BuiltObjectMissionType.Patrol, (BuiltObject)stellarObject, null, BuiltObjectMissionPriority.Normal, manuallyAssigned);
                    return true;
                }
                if (stellarObject is Habitat)
                {
                    ship.ClearPreviousMissionRequirements();
                    ship.AssignMission(BuiltObjectMissionType.Patrol, (Habitat)stellarObject, null, BuiltObjectMissionPriority.Normal, manuallyAssigned);
                    return true;
                }
                return false;
            }
            ship.ClearPreviousMissionRequirements();
            ship.AssignMission(BuiltObjectMissionType.Move, system.SystemStar, null, BuiltObjectMissionPriority.Normal, manuallyAssigned);
            return true;
        }

        private StellarObjectList IdentifyEmpireAssetsInSystem(SystemInfo system)
        {
            StellarObjectList stellarObjectList = new StellarObjectList();
            for (int i = 0; i < system.Habitats.Count; i++)
            {
                Habitat habitat = system.Habitats[i];
                if (habitat.Owner == this)
                {
                    stellarObjectList.Add(habitat);
                }
                else
                {
                    if (habitat.BasesAtHabitat == null || habitat.BasesAtHabitat.Count <= 0)
                    {
                        continue;
                    }
                    for (int j = 0; j < habitat.BasesAtHabitat.Count; j++)
                    {
                        BuiltObject builtObject = habitat.BasesAtHabitat[j];
                        if (builtObject.Empire == this)
                        {
                            stellarObjectList.Add(builtObject);
                        }
                    }
                }
            }
            return stellarObjectList;
        }

        public bool AssignFleetSystemPatrol(ShipGroup fleet, SystemInfo system)
        {
            if (fleet.Ships != null && fleet.Ships.Count > 0 && system != null)
            {
                StellarObjectList stellarObjectList = IdentifyEmpireAssetsInSystem(system);
                int num = fleet.Ships.Count;
                if (stellarObjectList.Count > 1)
                {
                    num = Math.Max(1, fleet.Ships.Count / stellarObjectList.Count);
                }
                if (stellarObjectList.Count > 0)
                {
                    if (system.SystemStar != null)
                    {
                        fleet.AssignMission(BuiltObjectMissionType.Patrol, system.SystemStar, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                    }
                    int num2 = 0;
                    for (int i = 0; i < stellarObjectList.Count; i++)
                    {
                        int num3 = num2 + num;
                        if (num3 > fleet.Ships.Count)
                        {
                            num3 = fleet.Ships.Count;
                        }
                        if (i == stellarObjectList.Count - 1)
                        {
                            num3 = fleet.Ships.Count;
                        }
                        for (int j = num2; j < num3; j++)
                        {
                            if (fleet.IsShipAvailable(fleet.Ships[j]))
                            {
                                fleet.Ships[j].ClearPreviousMissionRequirements();
                                if (stellarObjectList[i] is Habitat)
                                {
                                    fleet.Ships[j].AssignMission(BuiltObjectMissionType.Patrol, (Habitat)stellarObjectList[i], null, BuiltObjectMissionPriority.Normal);
                                }
                                else if (stellarObjectList[i] is BuiltObject)
                                {
                                    fleet.Ships[j].AssignMission(BuiltObjectMissionType.Patrol, (BuiltObject)stellarObjectList[i], null, BuiltObjectMissionPriority.Normal);
                                }
                                else
                                {
                                    fleet.Ships[j].AssignMission(BuiltObjectMissionType.Patrol, stellarObjectList[i], null, BuiltObjectMissionPriority.Normal);
                                }
                            }
                        }
                        num2 += num;
                        if (num2 >= fleet.Ships.Count)
                        {
                            break;
                        }
                    }
                    return true;
                }
                if (system.SystemStar != null)
                {
                    if (system.SystemStar.Category == HabitatCategoryType.GasCloud || system.SystemStar.Type == HabitatType.BlackHole || system.SystemStar.Type == HabitatType.SuperNova)
                    {
                        fleet.AssignMission(BuiltObjectMissionType.Move, system.SystemStar, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                        return true;
                    }
                    fleet.AssignMission(BuiltObjectMissionType.Patrol, system.SystemStar, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                    return true;
                }
            }
            return false;
        }

        public bool AssignFleetLoadTroops(ShipGroup fleet, bool manuallyAssigned)
        {
            return AssignFleetLoadTroops(fleet, null, manuallyAssigned);
        }

        public bool AssignFleetLoadTroops(ShipGroup fleet, Habitat colony, bool manuallyAssigned)
        {
            return AssignFleetLoadTroops(fleet, colony, manuallyAssigned, !manuallyAssigned);
        }

        public bool AssignFleetLoadTroops(ShipGroup fleet, Habitat colony, bool manuallyAssigned, bool enforceMinimumTroopLimitsAtColonies)
        {
            bool result = false;
            if (fleet != null)
            {
                Habitat habitat = colony;
                if (habitat == null)
                {
                    habitat = _Galaxy.FastFindNearestColony((int)fleet.LeadShip.Xpos, (int)fleet.LeadShip.Ypos, fleet.Empire, 0);
                }
                if (habitat != null)
                {
                    fleet.AssignMission(BuiltObjectMissionType.LoadTroops, habitat, null, BuiltObjectMissionPriority.Normal, manuallyAssigned);
                    if (colony == null && fleet.Mission != null)
                    {
                        fleet.Mission.TargetHabitat = null;
                    }
                    bool flag = false;
                    if (colony != null && colony.Empire != fleet.Empire && colony.InvadingTroops != null && colony.InvadingTroops.Count > 0 && colony.InvadingTroops[0].Empire == fleet.Empire)
                    {
                        flag = true;
                    }
                    for (int i = 0; i < fleet.Ships.Count; i++)
                    {
                        BuiltObject builtObject = fleet.Ships[i];
                        Habitat habitat2 = colony;
                        if (builtObject == null || builtObject.Empire == null || !fleet.IsShipAvailable(builtObject) || builtObject.TroopCapacityRemaining < 100)
                        {
                            continue;
                        }
                        TroopList prefilteredTroopsNotBeingPickedUp = new TroopList();
                        if (habitat2 == null)
                        {
                            habitat2 = builtObject.Empire.FindNearestColonyWithExcessTroops(builtObject, enforceMinimumTroopLimitsAtColonies, out prefilteredTroopsNotBeingPickedUp, allowTroopTypeFallback: false);
                        }
                        if (habitat2 == null)
                        {
                            return result;
                        }
                        TroopList troopList = null;
                        if (habitat2 != null)
                        {
                            troopList = habitat2.Troops;
                            if (flag && colony == habitat2)
                            {
                                troopList = habitat2.InvadingTroops;
                            }
                        }
                        if (habitat2 == null || troopList == null)
                        {
                            continue;
                        }
                        TroopList troopList2 = prefilteredTroopsNotBeingPickedUp;
                        if (flag && colony == habitat2)
                        {
                            troopList2 = new TroopList();
                            for (int j = 0; j < troopList.Count; j++)
                            {
                                if (troopList[j] != null && troopList[j].Empire == builtObject.Empire)
                                {
                                    troopList2.Add(troopList[j]);
                                }
                            }
                        }
                        else if (prefilteredTroopsNotBeingPickedUp == null || prefilteredTroopsNotBeingPickedUp.Count <= 0)
                        {
                            troopList2 = habitat2.Troops.GetTroopsNotGarrisonedNotAwaitingPickup();
                        }
                        if (troopList2.Count <= 0)
                        {
                            continue;
                        }
                        int num = builtObject.TroopCapacityRemaining;
                        int num2 = troopList.TotalDefendStrength;
                        if (enforceMinimumTroopLimitsAtColonies)
                        {
                            int num3 = habitat2.TroopLevelMinimum * 100;
                            num2 -= num3;
                        }
                        TroopList troopList3 = new TroopList();
                        bool flag2 = false;
                        for (int k = 0; k < troopList2.Count; k++)
                        {
                            Troop troop = troopList2[k];
                            if (troop != null && num2 - (int)((float)troop.DefendStrength * troop.Readiness) >= 0 && num >= troop.Size)
                            {
                                troopList3.Add(troop);
                                flag2 = true;
                                num -= troop.Size;
                                num2 -= (int)((float)troop.DefendStrength * troop.Readiness);
                                if (num < 100 || num2 <= 0)
                                {
                                    break;
                                }
                            }
                        }
                        if (flag2)
                        {
                            builtObject.ClearPreviousMissionRequirements();
                            builtObject.AssignMission(BuiltObjectMissionType.LoadTroops, habitat2, null, troopList3, BuiltObjectMissionPriority.Normal, manuallyAssigned);
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public bool AssignFleetUnloadTroops(ShipGroup fleet, Habitat colony, bool manuallyAssigned)
        {
            bool result = false;
            fleet.AssignMission(BuiltObjectMissionType.UnloadTroops, colony, null, BuiltObjectMissionPriority.Normal, manuallyAssigned);
            for (int i = 0; i < fleet.Ships.Count; i++)
            {
                BuiltObject builtObject = fleet.Ships[i];
                if (fleet.IsShipAvailable(builtObject))
                {
                    builtObject.ClearPreviousMissionRequirements();
                    if (builtObject.Troops != null && builtObject.Troops.Count > 0)
                    {
                        TroopList troopList = new TroopList();
                        troopList.AddRange(builtObject.Troops);
                        builtObject.AssignMission(BuiltObjectMissionType.UnloadTroops, colony, null, troopList, BuiltObjectMissionPriority.Normal, manuallyAssigned);
                        result = true;
                    }
                    else
                    {
                        builtObject.AssignMission(BuiltObjectMissionType.Move, colony, null, BuiltObjectMissionPriority.Normal, manuallyAssigned);
                    }
                }
            }
            return result;
        }

        public bool AssignFleetRepair(ShipGroup fleet)
        {
            return AssignFleetRepair(fleet, null);
        }

        public bool AssignFleetRepair(ShipGroup fleet, StellarObject shipYard)
        {
            if (fleet != null)
            {
                if (shipYard == null)
                {
                    shipYard = fleet.Empire.FindNearestShipYard(fleet.LeadShip, canRepairOrBuild: true, includeVerySmallYards: false);
                }
                if (shipYard != null && shipYard is BuiltObject)
                {
                    fleet.AssignMission(BuiltObjectMissionType.Repair, shipYard, null, BuiltObjectMissionPriority.High, manuallyAssigned: true);
                    for (int i = 0; i < fleet.Ships.Count; i++)
                    {
                        BuiltObject builtObject = fleet.Ships[i];
                        if (fleet.IsShipAvailable(builtObject) || (builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Repair && builtObject.BuiltAt == null))
                        {
                            if (builtObject.DamagedComponentCount > 0)
                            {
                                builtObject.ClearPreviousMissionRequirements();
                                builtObject.AssignMission(BuiltObjectMissionType.Repair, (BuiltObject)shipYard, null, BuiltObjectMissionPriority.Unavailable);
                            }
                            else
                            {
                                builtObject.ClearPreviousMissionRequirements();
                                builtObject.AssignMission(BuiltObjectMissionType.Refuel, (BuiltObject)shipYard, null, BuiltObjectMissionPriority.Unavailable);
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public bool CheckFleetNeedsRetrofit(ShipGroup fleet, bool isAutoRetrofit)
        {
            if (fleet != null && fleet.Empire != null && fleet.Empire.Designs != null && fleet.Ships != null)
            {
                for (int i = 0; i < fleet.Ships.Count; i++)
                {
                    BuiltObject builtObject = fleet.Ships[i];
                    if (builtObject != null)
                    {
                        bool flag = true;
                        Design design = fleet.Empire.Designs.FindNewestCanBuildFullEvaluate(builtObject.SubRole);
                        double num = Galaxy.ResolveBuildSpeed(this, _Galaxy, builtObject);
                        if (num > 1.0 && (design == null || design.WarpSpeed <= 0 || builtObject.Design == null || builtObject.Design.WarpSpeed > 0))
                        {
                            flag = false;
                        }
                        if (isAutoRetrofit && builtObject.SuppressAutoRetrofit)
                        {
                            flag = false;
                        }
                        if (design != null && design != builtObject.Design && flag)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool AssignFleetRetrofit(ShipGroup fleet, bool isAutoRetrofit)
        {
            return AssignFleetRetrofit(fleet, null, isAutoRetrofit);
        }

        public bool AssignFleetRetrofit(ShipGroup fleet, StellarObject shipYard, bool isAutoRetrofit)
        {
            if (fleet != null)
            {
                if (shipYard == null)
                {
                    shipYard = fleet.Empire.FindNearestShipYard(fleet.LeadShip, canRepairOrBuild: true, includeVerySmallYards: false);
                }
                if (shipYard != null && shipYard is BuiltObject)
                {
                    Design design = fleet.Empire.Designs.FindNewestCanBuildFullEvaluate(fleet.LeadShip.SubRole);
                    fleet.AssignMission(BuiltObjectMissionType.Retrofit, shipYard, null, design, BuiltObjectMissionPriority.High, manuallyAssigned: true);
                    for (int i = 0; i < fleet.Ships.Count; i++)
                    {
                        BuiltObject builtObject = fleet.Ships[i];
                        if (builtObject.BuiltAt == null && builtObject.RetrofitDesign == null)
                        {
                            bool flag = true;
                            design = fleet.Empire.Designs.FindNewestCanBuildFullEvaluate(builtObject.SubRole);
                            double num = Galaxy.ResolveBuildSpeed(this, _Galaxy, builtObject);
                            if (num > 1.0 && (design == null || design.WarpSpeed <= 0 || builtObject.Design == null || builtObject.Design.WarpSpeed > 0))
                            {
                                flag = false;
                            }
                            if (isAutoRetrofit && builtObject.SuppressAutoRetrofit)
                            {
                                flag = false;
                            }
                            if (design != null && design != builtObject.Design && flag)
                            {
                                builtObject.ClearPreviousMissionRequirements();
                                builtObject.Empire.AssignRetrofitMission(builtObject, design, (BuiltObject)shipYard, forceUseOfYard: true);
                                builtObject.DateRetrofit = _Galaxy.CurrentStarDate;
                            }
                            else
                            {
                                builtObject.ClearPreviousMissionRequirements();
                                builtObject.AssignMission(BuiltObjectMissionType.Refuel, (BuiltObject)shipYard, null, BuiltObjectMissionPriority.High);
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public bool AssignFleetRefuelling(ShipGroup refuelFleet, ResourceList requiredFuel)
        {
            if (requiredFuel != null && requiredFuel.Count > 0)
            {
                for (int i = 0; i < requiredFuel.Count; i++)
                {
                    requiredFuel[i].SortTag = (int)(requiredFuel[i].SortTag * 1.2);
                }
            }
            Point point = refuelFleet.DetermineApproximateActualFleetLocation();
            StellarObject stellarObject = DecideBestFleetRefuelPoint(point.X, point.Y, refuelFleet.Empire, requiredFuel, null);
            if (stellarObject != null && refuelFleet.CheckRefuelLocationRangeAcceptable(stellarObject))
            {
                refuelFleet.AssignMission(BuiltObjectMissionType.Refuel, stellarObject, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                return true;
            }
            return false;
        }

        public bool AssignFleetRefuellingExcludeGatheringShips(ShipGroup refuelFleet, ResourceList requiredFuel)
        {
            for (int i = 0; i < requiredFuel.Count; i++)
            {
                requiredFuel[i].SortTag = (int)(requiredFuel[i].SortTag * 1.2);
            }
            Point point = refuelFleet.DetermineApproximateActualFleetLocation();
            StellarObject stellarObject = DecideBestFleetRefuelPoint(point.X, point.Y, refuelFleet.Empire, requiredFuel, null);
            if (stellarObject != null)
            {
                for (int j = 0; j > refuelFleet.Ships.Count; j++)
                {
                    BuiltObject builtObject = refuelFleet.Ships[j];
                    if (refuelFleet.IsShipAvailable(builtObject) && (builtObject.Mission == null || (builtObject.Mission.Type != BuiltObjectMissionType.MoveAndWait && builtObject.Mission.Type != BuiltObjectMissionType.Move) || builtObject.Mission.TargetHabitat != refuelFleet.GatherPoint) && (builtObject.Mission == null || builtObject.Mission.Type != BuiltObjectMissionType.Refuel))
                    {
                        builtObject.AssignMission(BuiltObjectMissionType.Refuel, stellarObject, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: true);
                    }
                }
                return true;
            }
            return false;
        }

        public void ClearAttackFleetAssignments()
        {
            ClearAttackFleetAssignments(null);
        }

        public void ClearAttackFleetAssignments(Empire targetEmpire)
        {
            if (!_ControlMilitaryFleets || ShipGroups == null)
            {
                return;
            }
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (shipGroup != null && shipGroup.LeadShip != null && shipGroup.Posture == FleetPosture.Attack && shipGroup.AttackPoint != null && shipGroup.LeadShip.IsAutoControlled && (targetEmpire == null || shipGroup.AttackPoint.Empire == targetEmpire))
                {
                    shipGroup.AttackPoint = null;
                    shipGroup.PostureRangeSquared = double.MaxValue;
                }
            }
        }

        private void CheckAttackFleetTargets(EmpireList targetEmpires)
        {
            if (!_ControlMilitaryFleets || ShipGroups == null)
            {
                return;
            }
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (shipGroup != null && shipGroup.LeadShip != null && shipGroup.Posture == FleetPosture.Attack && shipGroup.AttackPoint != null && shipGroup.LeadShip.IsAutoControlled && (shipGroup.AttackPoint.HasBeenDestroyed || shipGroup.AttackPoint.Empire == null || !targetEmpires.Contains(shipGroup.AttackPoint.Empire)))
                {
                    shipGroup.AttackPoint = null;
                    shipGroup.PostureRangeSquared = double.MaxValue;
                    StellarObject stellarObject = SelectFleetBase(shipGroup);
                    if (stellarObject != null)
                    {
                        shipGroup.GatherPoint = stellarObject;
                    }
                }
            }
        }

        private void ClearDefendFleets()
        {
            if (!_ControlMilitaryFleets)
            {
                return;
            }
            EmpireList empireList = ResolveEmpiresAtWarWithOrPreparingToConquer();
            if (empireList.Count > 0)
            {
                return;
            }
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (shipGroup == null || shipGroup.LeadShip == null || !shipGroup.LeadShip.IsAutoControlled || shipGroup.Posture != FleetPosture.Defend)
                {
                    continue;
                }
                shipGroup.Posture = FleetPosture.Attack;
                StellarObject stellarObject = FindNearestRefuellingPoint(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Xpos, shipGroup.LeadShip.FuelType, 4);
                if (shipGroup.GatherPoint == null || shipGroup.GatherPoint != stellarObject)
                {
                    shipGroup.GatherPoint = stellarObject;
                    if (shipGroup.GatherPoint != null && (shipGroup.Mission == null || shipGroup.Mission.Type == BuiltObjectMissionType.Undefined || shipGroup.Mission.Priority == BuiltObjectMissionPriority.Low))
                    {
                        shipGroup.AssignMission(BuiltObjectMissionType.Move, shipGroup.GatherPoint, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                    }
                }
            }
        }

        private void SetDefendFleets()
        {
            SetDefendFleets(defendingFromAttack: false, assignMovement: true);
        }

        private void SetDefendFleets(bool defendingFromAttack, bool assignMovement)
        {
            if (!_ControlMilitaryFleets)
            {
                return;
            }
            int num = 10;
            int val = 1;
            if (ShipGroups.Count <= 1)
            {
                val = 0;
            }
            if (defendingFromAttack)
            {
                num = 1000;
                val = 1;
            }
            StellarObjectList stellarObjects = ResolveLocationsToDefend();
            stellarObjects = Galaxy.EnsureSingleStellarObjectPerSystem(stellarObjects);
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (shipGroup != null && shipGroup.LeadShip != null && shipGroup.LeadShip.IsAutoControlled && shipGroup.GatherPoint != null && shipGroup.Posture == FleetPosture.Defend)
                {
                    while (stellarObjects.Contains(shipGroup.GatherPoint))
                    {
                        stellarObjects.Remove(shipGroup.GatherPoint);
                    }
                    Habitat systemStar = Galaxy.DetermineHabitatSystemStarForStellarObject(shipGroup.GatherPoint);
                    stellarObjects = Galaxy.RemoveObjectsWithSystemStar(stellarObjects, systemStar);
                }
            }
            ShipGroupList shipGroupList = new ShipGroupList();
            int num2 = 0;
            for (int j = 0; j < ShipGroups.Count; j++)
            {
                if (ShipGroups[j].Ships.Count < num)
                {
                    shipGroupList.Add(ShipGroups[j]);
                }
                if (ShipGroups[j].Posture == FleetPosture.Defend)
                {
                    num2++;
                }
            }
            int num3 = shipGroupList.Count - (int)((double)shipGroupList.Count * 0.25);
            if (shipGroupList.Count > 0)
            {
                num3 = Math.Max(val, num3);
            }
            if (num2 >= num3)
            {
                return;
            }
            for (int k = 0; k < stellarObjects.Count; k++)
            {
                StellarObject stellarObject = stellarObjects[k];
                bool flag = assignMovement;
                ShipGroup shipGroup2 = FindNearestAvailableFleet(stellarObject.Xpos, stellarObject.Ypos, BuiltObjectMissionPriority.Low, 0, FleetPosture.Attack, mustBeWithinFuelRange: false, 0.0, mustBeAutomated: true, shouldBeSmallFleet: true);
                if (shipGroup2 == null)
                {
                    shipGroup2 = FindNearestAvailableFleet(stellarObject.Xpos, stellarObject.Ypos, BuiltObjectMissionPriority.Unavailable, 0, FleetPosture.Attack, mustBeWithinFuelRange: false, 0.0, mustBeAutomated: true, shouldBeSmallFleet: true);
                    flag = false;
                }
                if (shipGroup2 != null)
                {
                    shipGroup2.GatherPoint = stellarObject;
                    shipGroup2.Posture = FleetPosture.Defend;
                    shipGroup2.PostureRangeSquared = 250000000000.0;
                    if (flag && shipGroup2.GatherPoint != null)
                    {
                        shipGroup2.AssignMission(BuiltObjectMissionType.Move, shipGroup2.GatherPoint, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                    }
                    num2++;
                }
                if (num2 >= num3)
                {
                    break;
                }
            }
        }

        private bool SetDefendFleetForLocation(double x, double y)
        {
            bool result = false;
            bool flag = true;
            ShipGroup shipGroup = FindNearestAvailableFleet(x, y, BuiltObjectMissionPriority.Low, 0, FleetPosture.Attack, mustBeWithinFuelRange: false, 0.0, mustBeAutomated: true, shouldBeSmallFleet: true);
            if (shipGroup == null)
            {
                shipGroup = FindNearestAvailableFleet(x, y, BuiltObjectMissionPriority.Unavailable, 0, FleetPosture.Attack, mustBeWithinFuelRange: false, 0.0, mustBeAutomated: true, shouldBeSmallFleet: true);
                flag = false;
            }
            if (shipGroup != null)
            {
                StellarObject stellarObject = FindNearestRefuellingPoint(x, y, shipGroup.LeadShip.FuelType, 3);
                if (stellarObject != null)
                {
                    shipGroup.GatherPoint = stellarObject;
                    shipGroup.Posture = FleetPosture.Defend;
                    shipGroup.PostureRangeSquared = 2304000000.0;
                    if (flag)
                    {
                        shipGroup.AssignMission(BuiltObjectMissionType.Move, null, null, x, y, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                    }
                    result = true;
                }
            }
            return result;
        }

        private void PirateTaskFleets()
        {
            int refusalCount = 0;
            if (ShipGroups == null)
            {
                return;
            }
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (shipGroup != null && IsShipGroupAvailable(shipGroup, BuiltObjectMissionPriority.Low, 0) && shipGroup.LeadShip != null && shipGroup.LeadShip.IsAutoControlled && (shipGroup.WarpSpeed <= 0 || shipGroup.LeadShip.CurrentSpeed < (float)shipGroup.WarpSpeed))
                {
                    double num = shipGroup.CalculateRefuellingPortion();
                    if (shipGroup.LeadShip.ParentHabitat != null && shipGroup.LeadShip.ParentHabitat == shipGroup.GatherPoint)
                    {
                        num = Math.Max(num, 0.7);
                    }
                    ResourceList requiredFuel = new ResourceList();
                    int num2 = shipGroup.CheckShipsRequiringRefuelling(num, out requiredFuel);
                    if (num2 > (int)((double)shipGroup.Ships.Count * 0.0))
                    {
                        requiredFuel = shipGroup.CalculateRequiredFuel();
                        AssignFleetRefuellingExcludeGatheringShips(shipGroup, requiredFuel);
                    }
                }
            }
            if (PirateMissions != null && PirateMissions.Count > 0)
            {
                for (int j = 0; j < PirateMissions.Count; j++)
                {
                    EmpireActivity empireActivity = PirateMissions[j];
                    if (empireActivity == null || empireActivity.AssignedEmpire != this || empireActivity.Target == null)
                    {
                        continue;
                    }
                    switch (empireActivity.Type)
                    {
                        case EmpireActivityType.Attack:
                            {
                                ShipGroupList shipGroupList = ShipGroups.ResolveFleetsWithAttackTarget(empireActivity.Target);
                                if (shipGroupList.Count > 0)
                                {
                                    break;
                                }
                                int overallStrength = empireActivity.Target.FirepowerRaw;
                                if (empireActivity.Target is BuiltObject)
                                {
                                    BuiltObject builtObject = (BuiltObject)empireActivity.Target;
                                    overallStrength = builtObject.CalculateOverallStrengthFactor();
                                }
                                ShipGroup shipGroup3 = FindNearestAvailableFleet(empireActivity.Target.Xpos, empireActivity.Target.Ypos, BuiltObjectMissionPriority.Normal, overallStrength, FleetPosture.Attack, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: false, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: true, 0);
                                if (shipGroup3 != null && shipGroup3.LeadShip != null)
                                {
                                    BuiltObjectMissionType missionType2 = BuiltObjectMissionType.Attack;
                                    if (empireActivity.Target is BuiltObject)
                                    {
                                        BuiltObject target = (BuiltObject)empireActivity.Target;
                                        missionType2 = DetermineDestroyOrCaptureTarget(shipGroup3, target);
                                    }
                                    if ((shipGroup3.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessagePiratesAttackMission(empireActivity, shipGroup3), empireActivity.Target, AdvisorMessageType.EnemyAttack, shipGroup3, null))
                                    {
                                        shipGroup3.AssignMission(missionType2, empireActivity.Target, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                    }
                                }
                                break;
                            }
                        case EmpireActivityType.Defend:
                            {
                                ShipGroupList shipGroupList = ShipGroups.ResolveFleetsWithWaitTarget(empireActivity.Target);
                                if (shipGroupList.Count > 0)
                                {
                                    break;
                                }
                                ShipGroup shipGroup2 = FindNearestAvailableFleet(empireActivity.Target.Xpos, empireActivity.Target.Ypos, BuiltObjectMissionPriority.Normal, 1, FleetPosture.Defend, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: false, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: true, 0);
                                if (shipGroup2 == null)
                                {
                                    shipGroup2 = FindNearestAvailableFleet(empireActivity.Target.Xpos, empireActivity.Target.Ypos, BuiltObjectMissionPriority.Normal, 1, FleetPosture.Attack, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: false, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: true, 0);
                                }
                                if (shipGroup2 != null && shipGroup2.LeadShip != null)
                                {
                                    BuiltObjectMissionType missionType = BuiltObjectMissionType.MoveAndWait;
                                    if (shipGroup2.LeadShip.IsAutoControlled && _ControlMilitaryFleets)
                                    {
                                        long expiryDate = empireActivity.ExpiryDate;
                                        shipGroup2.AssignMission(missionType, empireActivity.Target, null, expiryDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                    }
                                    else if (CheckTaskAuthorized(AutomationLevel.SemiAutomated, ref refusalCount, GenerateAutomationMessagePiratesDefendMission(empireActivity, shipGroup2), empireActivity.Target, AdvisorMessageType.DefendTarget, shipGroup2, null))
                                    {
                                        long expiryDate2 = empireActivity.ExpiryDate;
                                        shipGroup2.AssignMission(missionType, empireActivity.Target, null, expiryDate2, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                    }
                                }
                                break;
                            }
                    }
                }
            }
            if (Troops.Count > 0 && _ColonizationTargets != null && _ColonizationTargets.Count > 0)
            {
                for (int k = 0; k < _ColonizationTargets.Count; k++)
                {
                    Habitat habitat = _ColonizationTargets[k].Habitat;
                    if (habitat == null || habitat.HasBeenDestroyed)
                    {
                        continue;
                    }
                    bool flag = true;
                    PirateColonyControl byFaction = habitat.GetPirateControl().GetByFaction(this);
                    PirateColonyControl byFacilityControl = habitat.GetPirateControl().GetByFacilityControl();
                    if (byFaction != null && (byFaction.HasFacilityControl || byFacilityControl == null))
                    {
                        flag = false;
                    }
                    if (habitat.Empire == this)
                    {
                        flag = false;
                    }
                    if (habitat.Empire != this && habitat.Empire != _Galaxy.IndependentEmpire && habitat.Empire != null)
                    {
                        PirateRelation pirateRelation = ObtainPirateRelation(habitat.Empire);
                        if (pirateRelation.Type == PirateRelationType.Protection)
                        {
                            flag = false;
                        }
                    }
                    if (!flag)
                    {
                        continue;
                    }
                    ShipGroup shipGroup4 = FindNearestAvailableFleet(habitat.Xpos, habitat.Ypos, BuiltObjectMissionPriority.Normal, 0, FleetPosture.Attack, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: false, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: true, habitat.TroopLevelRequired * 100);
                    if (shipGroup4 != null && (shipGroup4.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated))
                    {
                        string empty = string.Empty;
                        if (CheckTaskAuthorized(taskDescription: (habitat.Empire != _Galaxy.IndependentEmpire) ? GenerateAutomationMessageAttackEnemy(habitat, shipGroup4) : GenerateAutomationMessageInvadeIndependent(habitat, shipGroup4), automationLevel: _ControlMilitaryAttacks, refusalCount: ref refusalCount, taskTarget: habitat, advisorMessageType: AdvisorMessageType.EnemyAttack, advisorMessageData: shipGroup4, advisorMessageData2: null))
                        {
                            shipGroup4.AssignMission(BuiltObjectMissionType.Attack, habitat, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        }
                    }
                }
            }
            double valueThreshold = 1.0;
            int maximumDefenseStrength = 40000;
            switch (PiratePlayStyle)
            {
                case PiratePlayStyle.Mercenary:
                    valueThreshold = 0.0;
                    maximumDefenseStrength = 120000;
                    break;
                case PiratePlayStyle.Pirate:
                    valueThreshold = 0.5;
                    maximumDefenseStrength = 80000;
                    break;
            }
            HabitatPrioritizationList habitatPrioritizationList = IdentifyDesiredEnemyMiningStations(5, excludeRecentRaids: true, valueThreshold);
            if (habitatPrioritizationList.Count > 0)
            {
                for (int l = 0; l < habitatPrioritizationList.Count; l++)
                {
                    HabitatPrioritization habitatPrioritization = habitatPrioritizationList[l];
                    if (habitatPrioritization == null || habitatPrioritization.Habitat == null)
                    {
                        continue;
                    }
                    BuiltObject builtObject2 = _Galaxy.DetermineMiningStationAtHabitat(habitatPrioritization.Habitat);
                    if (builtObject2 == null)
                    {
                        continue;
                    }
                    int overallStrength2 = CalculateDefendingStrength(builtObject2);
                    ShipGroup shipGroup5 = FindNearestAvailableFleet(builtObject2.Xpos, builtObject2.Ypos, BuiltObjectMissionPriority.Normal, overallStrength2, FleetPosture.Attack, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: false, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: true, 0);
                    if (shipGroup5 == null || !shipGroup5.CheckFleetTargetWithinFuelRangeAndRefuel(builtObject2.Xpos, builtObject2.Ypos, 0.1) || (!shipGroup5.LeadShip.IsAutoControlled && _ControlMilitaryAttacks != AutomationLevel.SemiAutomated))
                    {
                        continue;
                    }
                    EmpireActivity firstByTargetAndType = PirateMissions.GetFirstByTargetAndType(builtObject2, EmpireActivityType.Defend);
                    if (firstByTargetAndType == null)
                    {
                        if (CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageRaidBase(builtObject2, shipGroup5), builtObject2, AdvisorMessageType.PirateRaid, shipGroup5, null))
                        {
                            shipGroup5.AssignMission(BuiltObjectMissionType.Raid, builtObject2, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        }
                        break;
                    }
                }
            }
            HabitatPrioritizationList habitatPrioritizationList2 = IdentifyRaidableColonies(maximumDefenseStrength, valueThreshold);
            if (habitatPrioritizationList2.Count > 0)
            {
                for (int m = 0; m < habitatPrioritizationList2.Count; m++)
                {
                    HabitatPrioritization habitatPrioritization2 = habitatPrioritizationList2[m];
                    if (habitatPrioritization2 == null || habitatPrioritization2.Habitat == null)
                    {
                        continue;
                    }
                    int troopStrength = 0;
                    int overallStrength3 = CalculateDefendingStrength(habitatPrioritization2.Habitat, out troopStrength);
                    ShipGroup shipGroup6 = FindNearestAvailableFleet(habitatPrioritization2.Habitat.Xpos, habitatPrioritization2.Habitat.Ypos, BuiltObjectMissionPriority.Normal, overallStrength3, FleetPosture.Attack, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: false, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: true, 0, (int)((double)troopStrength * 0.5));
                    if (shipGroup6 == null || !shipGroup6.CheckFleetTargetWithinFuelRangeAndRefuel(habitatPrioritization2.Habitat.Xpos, habitatPrioritization2.Habitat.Ypos, 0.1) || (!shipGroup6.LeadShip.IsAutoControlled && _ControlMilitaryAttacks != AutomationLevel.SemiAutomated))
                    {
                        continue;
                    }
                    EmpireActivity firstByTargetAndType2 = PirateMissions.GetFirstByTargetAndType(habitatPrioritization2.Habitat, EmpireActivityType.Defend);
                    if (firstByTargetAndType2 == null)
                    {
                        if (CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageRaidColony(habitatPrioritization2.Habitat, shipGroup6), habitatPrioritization2.Habitat, AdvisorMessageType.PirateRaid, shipGroup6, null))
                        {
                            shipGroup6.AssignMission(BuiltObjectMissionType.Raid, habitatPrioritization2.Habitat, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        }
                        break;
                    }
                }
            }
            PirateRelation relationWithLowestEvaluation = PirateRelations.GetRelationWithLowestEvaluation();
            if (relationWithLowestEvaluation != null && relationWithLowestEvaluation.Type == PirateRelationType.None && relationWithLowestEvaluation.Evaluation < -15f && relationWithLowestEvaluation.OtherEmpire != null)
            {
                BuiltObject builtObject3 = null;
                if (relationWithLowestEvaluation.OtherEmpire.PirateEmpireBaseHabitat != null)
                {
                    builtObject3 = _Galaxy.FindNearestKnownBaseForPirateAttack(this, relationWithLowestEvaluation.OtherEmpire.PirateEmpireBaseHabitat.Xpos, relationWithLowestEvaluation.OtherEmpire.PirateEmpireBaseHabitat.Ypos);
                }
                else if (relationWithLowestEvaluation.OtherEmpire.Capital != null)
                {
                    builtObject3 = _Galaxy.FindNearestKnownBaseForPirateAttack(this, relationWithLowestEvaluation.OtherEmpire.Capital.Xpos, relationWithLowestEvaluation.OtherEmpire.Capital.Ypos);
                }
                if (builtObject3 != null && !builtObject3.HasBeenDestroyed)
                {
                    int overallStrength4 = CalculateDefendingStrength(builtObject3);
                    ShipGroup shipGroup7 = FindNearestAvailableFleet(builtObject3.Xpos, builtObject3.Ypos, BuiltObjectMissionPriority.Normal, overallStrength4, FleetPosture.Attack, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: false, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: true, 0);
                    if (shipGroup7 != null && shipGroup7.LeadShip != null)
                    {
                        BuiltObjectMissionType missionType3 = DetermineDestroyOrCaptureTarget(shipGroup7, builtObject3);
                        if ((shipGroup7.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessagePiratesAttackPirates(builtObject3, shipGroup7), builtObject3, AdvisorMessageType.EnemyAttack, shipGroup7, null))
                        {
                            shipGroup7.AssignMission(missionType3, builtObject3, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        }
                    }
                }
            }
            if (SpacePorts == null)
            {
                return;
            }
            int num3 = Math.Max(1, (int)((double)ShipGroups.Count * 0.2));
            for (int n = 0; n < SpacePorts.Count; n++)
            {
                BuiltObject builtObject4 = SpacePorts[n];
                if (builtObject4 != null && !builtObject4.HasBeenDestroyed && builtObject4.Empire == this)
                {
                    if (n >= num3)
                    {
                        break;
                    }
                    EnsureBaseDefendedByFleet(builtObject4);
                }
            }
        }

        private void TaskShipGroups()
        {
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (IsShipGroupAvailable(shipGroup, BuiltObjectMissionPriority.Low, 0) && shipGroup.LeadShip.IsAutoControlled && (shipGroup.WarpSpeed <= 0 || shipGroup.LeadShip.CurrentSpeed < (float)shipGroup.WarpSpeed))
                {
                    double num = shipGroup.CalculateRefuellingPortion();
                    if (shipGroup.LeadShip.ParentHabitat != null && shipGroup.LeadShip.ParentHabitat == shipGroup.GatherPoint)
                    {
                        num = Math.Max(num, 0.7);
                    }
                    ResourceList requiredFuel = new ResourceList();
                    int num2 = shipGroup.CheckShipsRequiringRefuelling(num, out requiredFuel);
                    if (num2 > (int)((double)shipGroup.Ships.Count * 0.0))
                    {
                        requiredFuel = shipGroup.CalculateRequiredFuel();
                        AssignFleetRefuellingExcludeGatheringShips(shipGroup, requiredFuel);
                    }
                }
            }
            HabitatList habitatList = new HabitatList();
            if (_Galaxy.GlobalVictoryConditions != null)
            {
                if (_Galaxy.GlobalVictoryConditions.DefendHabitat != null && _Galaxy.GlobalVictoryConditions.DefendHabitatEmpire == this)
                {
                    habitatList.Add(_Galaxy.GlobalVictoryConditions.DefendHabitat);
                }
                else if (_Galaxy.GlobalVictoryConditions.TargetHabitat != null && _Galaxy.GlobalVictoryConditions.TargetHabitatEmpire == this)
                {
                    habitatList.Add(_Galaxy.GlobalVictoryConditions.TargetHabitat);
                }
            }
            if (CheckAtWar())
            {
                HabitatList habitatList2 = new HabitatList();
                habitatList2.AddRange(Colonies);
                habitatList2.Sort();
                habitatList2.Reverse();
                int num3 = 500000;
                for (int j = 0; j < habitatList2.Count; j++)
                {
                    if (habitatList2[j].StrategicValue > num3 && !habitatList.Contains(habitatList2[j]))
                    {
                        habitatList.Add(habitatList2[j]);
                    }
                }
                if (habitatList.Count == 0)
                {
                    if (HomeWorld != null && HomeWorld.Empire == this && Policy.HomeworldDefensePriority > 1.0)
                    {
                        habitatList.Add(HomeWorld);
                    }
                    if (!habitatList.Contains(Capital) && Capital != null)
                    {
                        habitatList.Add(Capital);
                    }
                }
            }
            int num4 = Math.Max(1, (int)((double)ShipGroups.Count * 0.2));
            for (int k = 0; k < habitatList.Count; k++)
            {
                if (habitatList[k] != null)
                {
                    if (k >= num4)
                    {
                        break;
                    }
                    EnsureColonyDefendedByFleet(habitatList[k]);
                }
            }
            if (KnownPirateBases.Count > 0)
            {
                HuntPirates();
            }
            ShipGroupList shipGroupList = new ShipGroupList();
            for (int l = 0; l < ShipGroups.Count; l++)
            {
                ShipGroup shipGroup2 = ShipGroups[l];
                if (shipGroup2 != null && (shipGroup2.Mission == null || shipGroup2.Mission.Type == BuiltObjectMissionType.Undefined || shipGroup2.Mission.Priority == BuiltObjectMissionPriority.Low))
                {
                    shipGroupList.Add(shipGroup2);
                }
            }
            if (shipGroupList.Count <= 0)
            {
                return;
            }
            int num5 = shipGroupList.Count;
            HabitatPrioritizationList habitatPrioritizationList = IdentifyThreatenedSystemsPrioritized(Capital.Xpos, Capital.Ypos, includePirateBaseSystems: false, excludeSystemsWithFleetsPresentOrEnRoute: true, excludeSystemsOfOtherEmpires: true);
            if (habitatPrioritizationList.Count <= 0)
            {
                return;
            }
            for (int m = 0; m < habitatPrioritizationList.Count; m++)
            {
                HabitatPrioritization habitatPrioritization = habitatPrioritizationList[m];
                if (habitatPrioritization == null || habitatPrioritization.Habitat == null)
                {
                    continue;
                }
                ShipGroup shipGroup3 = IdentifyNearestResponseFleet(habitatPrioritization.Habitat.Xpos, habitatPrioritization.Habitat.Ypos, mustBeWithinFuelRange: true, 0.1, 50000.0);
                if (shipGroup3 != null && shipGroup3.LeadShip != null && shipGroup3.LeadShip.IsAutoControlled)
                {
                    shipGroup3.AssignMission(BuiltObjectMissionType.Move, habitatPrioritization.Habitat, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                    num5--;
                    if (num5 <= 0)
                    {
                        break;
                    }
                }
            }
        }

        private void EnsureBaseDefendedByFleet(BuiltObject builtObject)
        {
            bool flag = false;
            ShipGroup shipGroup = FindNearestDefensiveFleet(builtObject.Xpos, builtObject.Ypos);
            if (shipGroup != null)
            {
                double num = _Galaxy.CalculateDistance(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, builtObject.Xpos, builtObject.Ypos);
                if (num > 5000.0)
                {
                    bool flag2 = false;
                    if (ShipGroups != null)
                    {
                        for (int i = 0; i < ShipGroups.Count; i++)
                        {
                            ShipGroup shipGroup2 = ShipGroups[i];
                            if (shipGroup2.Mission != null && shipGroup2.Mission.Type != 0 && shipGroup2.Mission.TargetBuiltObject != null && shipGroup2.Mission.TargetBuiltObject == builtObject)
                            {
                                flag2 = true;
                                break;
                            }
                        }
                    }
                    if (!flag2)
                    {
                        flag = true;
                    }
                }
            }
            else
            {
                flag = true;
            }
            if (flag)
            {
                FindNearestAvailableFleet(builtObject.Xpos, builtObject.Ypos, BuiltObjectMissionPriority.Normal, 0, FleetPosture.Defend, mustBeWithinFuelRange: false, 0.0, mustBeAutomated: true)?.AssignMission(BuiltObjectMissionType.Move, builtObject, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
            }
        }

        private void EnsureColonyDefendedByFleet(Habitat colony)
        {
            bool flag = false;
            ShipGroup shipGroup = FindNearestDefensiveFleet(colony.Xpos, colony.Ypos);
            if (shipGroup != null)
            {
                double num = _Galaxy.CalculateDistance(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, colony.Xpos, colony.Ypos);
                if (num > 5000.0)
                {
                    bool flag2 = false;
                    if (ShipGroups != null)
                    {
                        for (int i = 0; i < ShipGroups.Count; i++)
                        {
                            ShipGroup shipGroup2 = ShipGroups[i];
                            if (shipGroup2.Mission != null && shipGroup2.Mission.Type != 0 && shipGroup2.Mission.TargetHabitat != null && shipGroup2.Mission.TargetHabitat == colony)
                            {
                                flag2 = true;
                                break;
                            }
                        }
                    }
                    if (!flag2)
                    {
                        flag = true;
                    }
                }
            }
            else
            {
                flag = true;
            }
            if (flag)
            {
                FindNearestAvailableFleet(colony.Xpos, colony.Ypos, BuiltObjectMissionPriority.Normal, 0, FleetPosture.Defend, mustBeWithinFuelRange: false, 0.0, mustBeAutomated: true)?.AssignMission(BuiltObjectMissionType.Move, colony, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
            }
        }

        public bool CheckAtWarWithEmpire(Empire empire)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
            if (diplomaticRelation.Type == DiplomaticRelationType.War)
            {
                return true;
            }
            return false;
        }

        public bool CheckAtWar(Empire excludeEmpire)
        {
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation.Type == DiplomaticRelationType.War && diplomaticRelation.OtherEmpire != excludeEmpire)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckAtWar()
        {
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation.Type == DiplomaticRelationType.War)
                {
                    return true;
                }
            }
            return false;
        }

        public BuiltObject FindNearestResortBase(double x, double y)
        {
            BuiltObject result = null;
            double num = double.MaxValue;
            if (ResortBases != null)
            {
                for (int i = 0; i < ResortBases.Count; i++)
                {
                    BuiltObject builtObject = ResortBases[i];
                    if (builtObject != null && !builtObject.HasBeenDestroyed)
                    {
                        double num2 = _Galaxy.CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                        if (num2 < num)
                        {
                            result = builtObject;
                            num = num2;
                        }
                    }
                }
            }
            return result;
        }

        private ShipGroup FindNearestFleet(double x, double y)
        {
            ShipGroup result = null;
            double num = double.MaxValue;
            if (ShipGroups != null)
            {
                for (int i = 0; i < ShipGroups.Count; i++)
                {
                    ShipGroup shipGroup = ShipGroups[i];
                    if (shipGroup.LeadShip != null)
                    {
                        double num2 = _Galaxy.CalculateDistance(x, y, shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos);
                        if (num2 < num)
                        {
                            result = shipGroup;
                            num = num2;
                        }
                    }
                }
            }
            return result;
        }

        private ShipGroup FindNearestDefensiveFleet(double x, double y)
        {
            ShipGroup result = null;
            double num = double.MaxValue;
            if (ShipGroups != null)
            {
                for (int i = 0; i < ShipGroups.Count; i++)
                {
                    ShipGroup shipGroup = ShipGroups[i];
                    if (shipGroup.LeadShip != null && shipGroup.Posture == FleetPosture.Defend)
                    {
                        double num2 = _Galaxy.CalculateDistance(x, y, shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos);
                        if (num2 < num)
                        {
                            result = shipGroup;
                            num = num2;
                        }
                    }
                }
            }
            return result;
        }

        private ShipGroup FindNearestAvailableFleet(double x, double y, BuiltObjectMissionPriority maximumPriority, int overallStrength, FleetPosture posture)
        {
            return FindNearestAvailableFleet(x, y, maximumPriority, overallStrength, posture, mustBeWithinFuelRange: false);
        }

        private ShipGroup FindNearestAvailableFleet(double x, double y, BuiltObjectMissionPriority maximumPriority, int overallStrength, FleetPosture posture, bool mustBeWithinFuelRange)
        {
            return FindNearestAvailableFleet(x, y, maximumPriority, overallStrength, posture, mustBeWithinFuelRange, 0.0, mustBeAutomated: false);
        }

        private ShipGroup FindNearestAvailableFleet(double x, double y, BuiltObjectMissionPriority maximumPriority, int overallStrength, FleetPosture posture, bool mustBeWithinFuelRange, double fuelPortionMargin, bool mustBeAutomated)
        {
            return FindNearestAvailableFleet(x, y, maximumPriority, overallStrength, posture, mustBeWithinFuelRange, 0.0, mustBeAutomated, shouldBeSmallFleet: false);
        }

        private ShipGroup FindNearestAvailableFleet(double x, double y, BuiltObjectMissionPriority maximumPriority, int overallStrength, FleetPosture posture, bool mustBeWithinFuelRange, double fuelPortionMargin, bool mustBeAutomated, bool shouldBeSmallFleet)
        {
            return FindNearestAvailableFleet(x, y, maximumPriority, overallStrength, posture, mustBeWithinFuelRange, 0.0, mustBeAutomated, shouldBeSmallFleet, gatherPointMustBeBlank: false);
        }

        private ShipGroup FindNearestAvailableFleet(double x, double y, BuiltObjectMissionPriority maximumPriority, int overallStrength, FleetPosture posture, bool mustBeWithinFuelRange, double fuelPortionMargin, bool mustBeAutomated, bool shouldBeSmallFleet, bool gatherPointMustBeBlank)
        {
            return FindNearestAvailableFleet(x, y, maximumPriority, overallStrength, posture, mustBeWithinFuelRange, 0.0, mustBeAutomated, shouldBeSmallFleet, gatherPointMustBeBlank, mustBeWithinPostureRange: false);
        }

        private ShipGroup FindNearestAvailableFleet(double x, double y, BuiltObjectMissionPriority maximumPriority, int overallStrength, FleetPosture posture, bool mustBeWithinFuelRange, double fuelPortionMargin, bool mustBeAutomated, bool shouldBeSmallFleet, bool gatherPointMustBeBlank, bool mustBeWithinPostureRange)
        {
            return FindNearestAvailableFleet(x, y, maximumPriority, overallStrength, posture, mustBeWithinFuelRange, 0.0, mustBeAutomated, shouldBeSmallFleet, gatherPointMustBeBlank, mustBeWithinPostureRange, 0);
        }

        private ShipGroup FindNearestAvailableFleet(double x, double y, BuiltObjectMissionPriority maximumPriority, int overallStrength, FleetPosture posture, bool mustBeWithinFuelRange, double fuelPortionMargin, bool mustBeAutomated, bool shouldBeSmallFleet, bool gatherPointMustBeBlank, bool mustBeWithinPostureRange, int minimumTroopStrength)
        {
            return FindNearestAvailableFleet(x, y, maximumPriority, overallStrength, posture, mustBeWithinFuelRange, 0.0, mustBeAutomated, shouldBeSmallFleet, gatherPointMustBeBlank, mustBeWithinPostureRange, minimumTroopStrength, 0);
        }

        private ShipGroup FindNearestAvailableFleet(double x, double y, BuiltObjectMissionPriority maximumPriority, int overallStrength, FleetPosture posture, bool mustBeWithinFuelRange, double fuelPortionMargin, bool mustBeAutomated, bool shouldBeSmallFleet, bool gatherPointMustBeBlank, bool mustBeWithinPostureRange, int minimumTroopStrength, int minimumBoardingStrength)
        {
            ShipGroup result = null;
            double num = double.MaxValue;
            if (ShipGroups != null)
            {
                for (int i = 0; i < ShipGroups.Count; i++)
                {
                    ShipGroup shipGroup = ShipGroups[i];
                    if (shipGroup.LeadShip == null || !IsShipGroupAvailableWithAttackStrength(shipGroup, maximumPriority, overallStrength) || (gatherPointMustBeBlank && shipGroup.GatherPoint != null) || (shouldBeSmallFleet && (shipGroup.ShipTargetAmount >= 10 || shipGroup.Ships.Count >= 10)))
                    {
                        continue;
                    }
                    if (mustBeWithinFuelRange)
                    {
                        if (!shipGroup.CheckFleetTargetWithinFuelRangeAndRefuel(x, y, fuelPortionMargin) || (mustBeAutomated && !shipGroup.LeadShip.IsAutoControlled) || shipGroup.Posture != posture || shipGroup.TotalTroopAttackStrength < minimumTroopStrength)
                        {
                            continue;
                        }
                        int num2 = shipGroup.TotalAssaultPodCount * 6000;
                        if (num2 < minimumBoardingStrength)
                        {
                            continue;
                        }
                        bool flag = true;
                        if (mustBeWithinPostureRange)
                        {
                            flag = false;
                            if (shipGroup.Posture == FleetPosture.Defend && shipGroup.GatherPoint != null)
                            {
                                double num3 = _Galaxy.CalculateDistanceSquared(x, y, shipGroup.GatherPoint.Xpos, shipGroup.GatherPoint.Ypos);
                                if (shipGroup.PostureRangeSquared >= num3)
                                {
                                    flag = true;
                                }
                            }
                            else if (shipGroup.Posture == FleetPosture.Attack)
                            {
                                if (shipGroup.AttackPoint != null)
                                {
                                    double num4 = _Galaxy.CalculateDistanceSquared(x, y, shipGroup.AttackPoint.Xpos, shipGroup.AttackPoint.Ypos);
                                    if (shipGroup.PostureRangeSquared >= num4)
                                    {
                                        flag = true;
                                    }
                                }
                                else
                                {
                                    flag = true;
                                }
                            }
                        }
                        if (flag)
                        {
                            double num5 = _Galaxy.CalculateDistance(x, y, shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos);
                            if (num5 < num)
                            {
                                result = shipGroup;
                                num = num5;
                            }
                        }
                    }
                    else
                    {
                        if ((mustBeAutomated && !shipGroup.LeadShip.IsAutoControlled) || shipGroup.Posture != posture || shipGroup.TotalTroopAttackStrength < minimumTroopStrength)
                        {
                            continue;
                        }
                        bool flag2 = true;
                        if (mustBeWithinPostureRange)
                        {
                            flag2 = false;
                            if (shipGroup.Posture == FleetPosture.Defend && shipGroup.GatherPoint != null)
                            {
                                double num6 = _Galaxy.CalculateDistanceSquared(x, y, shipGroup.GatherPoint.Xpos, shipGroup.GatherPoint.Ypos);
                                if (shipGroup.PostureRangeSquared >= num6)
                                {
                                    flag2 = true;
                                }
                            }
                            else if (shipGroup.Posture == FleetPosture.Attack)
                            {
                                if (shipGroup.AttackPoint != null)
                                {
                                    double num7 = _Galaxy.CalculateDistanceSquared(x, y, shipGroup.AttackPoint.Xpos, shipGroup.AttackPoint.Ypos);
                                    if (shipGroup.PostureRangeSquared >= num7)
                                    {
                                        flag2 = true;
                                    }
                                }
                                else
                                {
                                    flag2 = true;
                                }
                            }
                        }
                        if (flag2)
                        {
                            double num8 = _Galaxy.CalculateDistance(x, y, shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos);
                            if (num8 < num)
                            {
                                result = shipGroup;
                                num = num8;
                            }
                        }
                    }
                }
            }
            return result;
        }

        private void HuntPirates()
        {
            int refusalCount = 0;
            if (KnownPirateBases.Count <= 0 || Reclusive || Galaxy.Rnd.Next(0, 3) <= 0 || CheckAtWar())
            {
                return;
            }
            ShipGroup shipGroup = FindAvailableShipGroup(BuiltObjectMissionPriority.Low, 0, FleetPosture.Attack);
            if (shipGroup == null)
            {
                return;
            }
            HabitatList systemPriorities = new HabitatList();
            HabitatPrioritizationList habitatPrioritizationList = IdentifyColonizationTargets(_Galaxy, filterOutDangerousTargets: false, 0, int.MaxValue, includeLowQualityTargets: false, includeDistantTargets: false);
            if (habitatPrioritizationList != null)
            {
                systemPriorities = habitatPrioritizationList.ResolveSystems();
            }
            BuiltObjectList builtObjectList = KnownPirateBases.GenerateDistanceOrderedList(Capital.Xpos, Capital.Ypos, systemPriorities);
            for (int i = 0; i < builtObjectList.Count; i++)
            {
                BuiltObject builtObject = builtObjectList[i];
                PirateRelation pirateRelation = builtObject.Empire.ObtainPirateRelation(this);
                int num = builtObject.Empire.PirateMissions.IndexOfRequester(this, EmpireActivityType.Attack);
                if (num >= 0 || pirateRelation.Type == PirateRelationType.Protection)
                {
                    continue;
                }
                int num2 = builtObject.CalculateOverallStrengthFactor();
                if (CheckSystemVisible(builtObject.NearestSystemStar))
                {
                    num2 = CalculateDefendingStrength(builtObject);
                }
                ShipGroup shipGroup2 = FindNearestAvailableFleet(builtObject.Xpos, builtObject.Ypos, BuiltObjectMissionPriority.Low, num2, FleetPosture.Attack, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: false, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: true);
                if (shipGroup2 == null)
                {
                    shipGroup2 = FindNearestAvailableFleet(builtObject.Xpos, builtObject.Ypos, BuiltObjectMissionPriority.Low, num2, FleetPosture.Defend, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: false, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: true);
                }
                if (shipGroup2 == null)
                {
                    continue;
                }
                double num3 = _Galaxy.CalculateDistance(shipGroup2.LeadShip.Xpos, shipGroup2.LeadShip.Ypos, builtObject.Xpos, builtObject.Ypos);
                if (!(num3 < Galaxy.AttackOnPiratesRange) || !shipGroup2.CheckFleetTargetWithinFuelRangeAndRefuel(builtObject.Xpos, builtObject.Ypos, 0.1))
                {
                    continue;
                }
                int num4 = 0;
                for (int j = 0; j < BuiltObjects.Count; j++)
                {
                    BuiltObject builtObject2 = BuiltObjects[j];
                    if (builtObject2.Role == BuiltObjectRole.Military && builtObject2.Mission != null && builtObject2.Mission.Type == BuiltObjectMissionType.Attack && builtObject2.Mission.TargetBuiltObject != null)
                    {
                        BuiltObject targetBuiltObject = builtObject2.Mission.TargetBuiltObject;
                        if (targetBuiltObject == builtObject)
                        {
                            num4 += builtObject2.CalculateOverallStrengthFactor();
                        }
                    }
                }
                if (num4 < (int)((double)num2 * 1.5) && (shipGroup2.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageAttackPirateBase(builtObject, shipGroup2), builtObject, AdvisorMessageType.EnemyAttack, shipGroup2, null))
                {
                    BuiltObjectMissionType missionType = DetermineDestroyOrCaptureTarget(shipGroup2, builtObject);
                    shipGroup2.AssignMission(missionType, builtObject, null, BuiltObjectMissionPriority.High, manuallyAssigned: true);
                    break;
                }
            }
        }

        private EmpireList ResolveEmpiresAtWarWithOrPreparingToConquer()
        {
            EmpireList empireList = new EmpireList();
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation == null)
                {
                    continue;
                }
                if (diplomaticRelation.Type == DiplomaticRelationType.War)
                {
                    empireList.Add(diplomaticRelation.OtherEmpire);
                    continue;
                }
                DiplomaticStrategy strategy = diplomaticRelation.Strategy;
                if (strategy == DiplomaticStrategy.Conquer)
                {
                    empireList.Add(diplomaticRelation.OtherEmpire);
                }
            }
            return empireList;
        }

        private EmpireList ResolveEmpiresToDefendAgainst()
        {
            EmpireList empireList = new EmpireList();
            if (PirateEmpireBaseHabitat != null)
            {
                for (int i = 0; i < PirateRelations.Count; i++)
                {
                    PirateRelation pirateRelation = PirateRelations[i];
                    if (pirateRelation != null && pirateRelation.OtherEmpire != null && pirateRelation.Type == PirateRelationType.None)
                    {
                        empireList.Add(pirateRelation.OtherEmpire);
                    }
                }
            }
            else
            {
                for (int j = 0; j < DiplomaticRelations.Count; j++)
                {
                    DiplomaticRelation diplomaticRelation = DiplomaticRelations[j];
                    if (diplomaticRelation == null)
                    {
                        continue;
                    }
                    if (diplomaticRelation.Type == DiplomaticRelationType.War)
                    {
                        empireList.Add(diplomaticRelation.OtherEmpire);
                        continue;
                    }
                    switch (diplomaticRelation.Strategy)
                    {
                        case DiplomaticStrategy.Conquer:
                        case DiplomaticStrategy.Defend:
                        case DiplomaticStrategy.DefendPlacate:
                        case DiplomaticStrategy.DefendUndermine:
                        case DiplomaticStrategy.Punish:
                            if (!diplomaticRelation.OtherEmpire.Reclusive)
                            {
                                empireList.Add(diplomaticRelation.OtherEmpire);
                            }
                            break;
                    }
                }
            }
            return empireList;
        }

        public StellarObjectList ResolveLocationsToDefend()
        {
            return ResolveLocationsToDefend(includeBases: true);
        }

        public StellarObjectList ResolveLocationsToDefend(bool includeBases)
        {
            StellarObjectList stellarObjectList = new StellarObjectList();
            if (PirateEmpireBaseHabitat != null)
            {
                if (SpacePorts != null)
                {
                    for (int i = 0; i < SpacePorts.Count; i++)
                    {
                        BuiltObject builtObject = SpacePorts[i];
                        if (builtObject != null && !builtObject.HasBeenDestroyed && !stellarObjectList.Contains(builtObject))
                        {
                            stellarObjectList.Add(builtObject);
                        }
                    }
                }
                for (int j = 0; j < Colonies.Count; j++)
                {
                    Habitat habitat = Colonies[j];
                    if (habitat != null && !habitat.HasBeenDestroyed && habitat.GetPirateControl().GetByFaction(this) != null && !stellarObjectList.Contains(habitat))
                    {
                        stellarObjectList.Add(habitat);
                    }
                }
            }
            else
            {
                EmpireList empireList = ResolveEmpiresToDefendAgainst();
                for (int k = 0; k < empireList.Count; k++)
                {
                    Empire empire = empireList[k];
                    DiplomaticRelation diplomaticRelation = empire.ObtainDiplomaticRelation(this);
                    if (diplomaticRelation.Type == DiplomaticRelationType.War)
                    {
                        stellarObjectList = AddWarObjectivesToList(diplomaticRelation, stellarObjectList, calculateWarObjectivesIfNotPresent: false, includeBases);
                    }
                }
                for (int l = 0; l < empireList.Count; l++)
                {
                    Empire empire2 = empireList[l];
                    DiplomaticRelation diplomaticRelation2 = ObtainDiplomaticRelation(empire2);
                    if (diplomaticRelation2.Type != DiplomaticRelationType.War)
                    {
                        DiplomaticRelation relation = empire2.ObtainDiplomaticRelation(this);
                        switch (diplomaticRelation2.Strategy)
                        {
                            case DiplomaticStrategy.Defend:
                            case DiplomaticStrategy.DefendPlacate:
                            case DiplomaticStrategy.DefendUndermine:
                                stellarObjectList = AddWarObjectivesToList(relation, stellarObjectList, calculateWarObjectivesIfNotPresent: true, includeBases);
                                break;
                        }
                    }
                }
                for (int m = 0; m < empireList.Count; m++)
                {
                    Empire empire3 = empireList[m];
                    DiplomaticRelation diplomaticRelation3 = ObtainDiplomaticRelation(empire3);
                    if (diplomaticRelation3.Type != DiplomaticRelationType.War)
                    {
                        DiplomaticRelation relation2 = empire3.ObtainDiplomaticRelation(this);
                        DiplomaticStrategy strategy = diplomaticRelation3.Strategy;
                        if (strategy == DiplomaticStrategy.Conquer)
                        {
                            stellarObjectList = AddWarObjectivesToList(relation2, stellarObjectList, calculateWarObjectivesIfNotPresent: true, includeBases);
                        }
                    }
                }
                if (HomeWorld != null && HomeWorld.Empire == this && Policy.HomeworldDefensePriority > 1.0 && !stellarObjectList.Contains(HomeWorld))
                {
                    stellarObjectList.Add(HomeWorld);
                }
                if (Capitals != null)
                {
                    for (int n = 0; n < Capitals.Count; n++)
                    {
                        Habitat habitat2 = Capitals[n];
                        if (habitat2 != null && !habitat2.HasBeenDestroyed)
                        {
                            BuiltObject builtObject2 = Galaxy.DetermineSpacePortAtColony(habitat2);
                            if (!stellarObjectList.Contains(habitat2) && (builtObject2 == null || !stellarObjectList.Contains(builtObject2)))
                            {
                                stellarObjectList.Add(habitat2);
                            }
                        }
                    }
                }
                if (SpacePorts != null)
                {
                    for (int num = 0; num < SpacePorts.Count; num++)
                    {
                        BuiltObject builtObject3 = SpacePorts[num];
                        if (builtObject3 != null && !builtObject3.HasBeenDestroyed)
                        {
                            Habitat parentHabitat = builtObject3.ParentHabitat;
                            if (parentHabitat != null && !stellarObjectList.Contains(builtObject3) && !stellarObjectList.Contains(parentHabitat))
                            {
                                stellarObjectList.Add(parentHabitat);
                            }
                        }
                    }
                }
                StellarObject[] array = stellarObjectList.ToArray();
                for (int num2 = 0; num2 < array.Length; num2++)
                {
                    if (array[num2].Empire != this)
                    {
                        stellarObjectList.Remove(array[num2]);
                    }
                }
            }
            return stellarObjectList;
        }

        private StellarObjectList AddWarObjectivesToList(DiplomaticRelation relation, StellarObjectList objectives, bool calculateWarObjectivesIfNotPresent)
        {
            return AddWarObjectivesToList(relation, objectives, calculateWarObjectivesIfNotPresent, includeBases: true);
        }

        private StellarObjectList AddWarObjectivesToList(DiplomaticRelation relation, StellarObjectList objectives, bool calculateWarObjectivesIfNotPresent, bool includeBases)
        {
            if (relation != null)
            {
                if (relation.WarObjective == WarObjective.CaptureObjectives)
                {
                    for (int i = 0; i < relation.WarObjectiveColonies.Count; i++)
                    {
                        if (!objectives.Contains(relation.WarObjectiveColonies[i]))
                        {
                            objectives.Add(relation.WarObjectiveColonies[i]);
                        }
                    }
                    if (includeBases)
                    {
                        for (int j = 0; j < relation.WarObjectiveBases.Count; j++)
                        {
                            if (!objectives.Contains(relation.WarObjectiveBases[j]) && relation.WarObjectiveBases[j].ParentHabitat != null && relation.WarObjectiveBases[j].ParentHabitat.Resources != null && relation.WarObjectiveBases[j].ParentHabitat.Resources.HasSuperLuxuryResources())
                            {
                                objectives.Add(relation.WarObjectiveBases[j]);
                            }
                        }
                    }
                }
                else if (calculateWarObjectivesIfNotPresent && relation.ThisEmpire != null && relation.OtherEmpire != null)
                {
                    HabitatList targetedColonies = new HabitatList();
                    BuiltObjectList targetedBases = new BuiltObjectList();
                    relation.ThisEmpire.IdentifyEmpireWarObjectives(relation.OtherEmpire, out targetedColonies, out targetedBases);
                    for (int k = 0; k < targetedColonies.Count; k++)
                    {
                        if (!objectives.Contains(targetedColonies[k]))
                        {
                            objectives.Add(targetedColonies[k]);
                        }
                    }
                    if (includeBases)
                    {
                        for (int l = 0; l < targetedBases.Count; l++)
                        {
                            if (!objectives.Contains(targetedBases[l]) && targetedBases[l].ParentHabitat != null && targetedBases[l].ParentHabitat.Resources != null && targetedBases[l].ParentHabitat.Resources.HasSuperLuxuryResources())
                            {
                                objectives.Add(targetedBases[l]);
                            }
                        }
                    }
                }
            }
            return objectives;
        }

        public void ReviewDefensiveFleetLocations()
        {
            if (!_ControlMilitaryFleets || ShipGroups == null)
            {
                return;
            }
            StellarObjectList stellarObjectList = new StellarObjectList();
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                stellarObjectList.Add(shipGroup.GatherPoint);
                if (shipGroup != null && shipGroup.LeadShip != null && shipGroup.Posture == FleetPosture.Defend && shipGroup.LeadShip.IsAutoControlled)
                {
                    shipGroup.GatherPoint = null;
                }
            }
            StellarObjectList stellarObjects = ResolveLocationsToDefend();
            stellarObjects = Galaxy.EnsureSingleStellarObjectPerSystem(stellarObjects);
            for (int j = 0; j < stellarObjects.Count; j++)
            {
                StellarObject stellarObject = stellarObjects[j];
                if (stellarObject == null || stellarObject.HasBeenDestroyed)
                {
                    continue;
                }
                ShipGroup shipGroup2 = FindNearestAvailableFleet(stellarObject.Xpos, stellarObject.Ypos, BuiltObjectMissionPriority.Unavailable, 0, FleetPosture.Defend, mustBeWithinFuelRange: false, 0.0, mustBeAutomated: true, shouldBeSmallFleet: true, gatherPointMustBeBlank: true);
                if (shipGroup2 != null)
                {
                    shipGroup2.GatherPoint = stellarObject;
                    shipGroup2.PostureRangeSquared = 250000000000.0;
                    StellarObject stellarObject2 = null;
                    int num = ShipGroups.IndexOf(shipGroup2);
                    if (num >= 0 && num < stellarObjectList.Count)
                    {
                        stellarObject2 = stellarObjectList[num];
                    }
                    if (shipGroup2.GatherPoint != stellarObject2 && shipGroup2.Mission != null && shipGroup2.Mission.Type == BuiltObjectMissionType.Move && shipGroup2.Mission.Target == stellarObject2)
                    {
                        shipGroup2.CompleteMission(clearShipMissions: true);
                    }
                }
            }
            for (int k = 0; k < ShipGroups.Count; k++)
            {
                ShipGroup shipGroup3 = ShipGroups[k];
                if (shipGroup3 == null || shipGroup3.LeadShip == null || shipGroup3.Posture != FleetPosture.Defend || shipGroup3.GatherPoint != null || !shipGroup3.LeadShip.IsAutoControlled)
                {
                    continue;
                }
                shipGroup3.Posture = FleetPosture.Attack;
                shipGroup3.PostureRangeSquared = double.MaxValue;
                StellarObject stellarObject3 = SelectFleetBase(shipGroup3);
                if (stellarObject3 == null)
                {
                    continue;
                }
                shipGroup3.GatherPoint = stellarObject3;
                if ((shipGroup3.Mission != null && shipGroup3.Mission.Type != 0 && shipGroup3.Mission.Priority != BuiltObjectMissionPriority.Low) || (shipGroup3.Mission != null && shipGroup3.Mission.Type == BuiltObjectMissionType.Move && shipGroup3.Mission.Target == shipGroup3.GatherPoint))
                {
                    continue;
                }
                double num2 = _Galaxy.CalculateDistance(shipGroup3.LeadShip.Xpos, shipGroup3.LeadShip.Ypos, stellarObject3.Xpos, stellarObject3.Ypos);
                if (!(num2 > 2000.0))
                {
                    continue;
                }
                StellarObject stellarObject4 = stellarObject3;
                if (stellarObject3 is Habitat)
                {
                    Habitat habitat = (Habitat)stellarObject3;
                    if (habitat != null)
                    {
                        BuiltObject builtObject = Galaxy.DetermineSpacePortAtColony(habitat);
                        if (builtObject != null)
                        {
                            stellarObject4 = builtObject;
                        }
                    }
                }
                if (stellarObject4 != null && stellarObject4.IsRefuellingDepot)
                {
                    shipGroup3.AssignMission(BuiltObjectMissionType.Refuel, stellarObject4, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: false);
                }
                else
                {
                    shipGroup3.AssignMission(BuiltObjectMissionType.Move, stellarObject3, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                }
            }
        }

        private StellarObject SelectDefensiveFleetBase(ShipGroup fleet, StellarObjectList defendLocations, bool moveToLocationIfAvailable)
        {
            StellarObject stellarObject = null;
            int num = 0;
            bool flag = false;
            int iterationCount = 0;
            while (Galaxy.ConditionCheckLimit(!flag, 100, ref iterationCount) && num < defendLocations.Count)
            {
                if (fleet.CheckFleetTargetWithinFuelRangeAndRefuel(defendLocations[num].Xpos, defendLocations[num].Ypos, 0.0))
                {
                    stellarObject = defendLocations[num];
                    defendLocations.RemoveAt(num);
                    num--;
                    if (stellarObject != null && moveToLocationIfAvailable && (fleet.Mission == null || fleet.Mission.Type == BuiltObjectMissionType.Undefined || fleet.Mission.Priority == BuiltObjectMissionPriority.Low) && (fleet.Mission == null || fleet.Mission.Type != BuiltObjectMissionType.Move || fleet.Mission.Target != stellarObject))
                    {
                        if (stellarObject.IsRefuellingDepot)
                        {
                            fleet.AssignMission(BuiltObjectMissionType.Refuel, stellarObject, null, BuiltObjectMissionPriority.Unavailable, manuallyAssigned: false);
                        }
                        else
                        {
                            fleet.AssignMission(BuiltObjectMissionType.Move, stellarObject, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        }
                    }
                    flag = true;
                }
                num++;
            }
            return stellarObject;
        }

        public StellarObject SelectFleetBase(ShipGroup fleet)
        {
            if (fleet.Posture == FleetPosture.Defend)
            {
                StellarObjectList stellarObjects = ResolveLocationsToDefend();
                stellarObjects = Galaxy.EnsureSingleStellarObjectPerSystem(stellarObjects);
                for (int i = 0; i < ShipGroups.Count; i++)
                {
                    ShipGroup shipGroup = ShipGroups[i];
                    if (shipGroup != null && shipGroup.GatherPoint != null && shipGroup.Posture == FleetPosture.Defend)
                    {
                        while (stellarObjects.Contains(shipGroup.GatherPoint))
                        {
                            stellarObjects.Remove(shipGroup.GatherPoint);
                        }
                        Habitat systemStar = Galaxy.DetermineHabitatSystemStarForStellarObject(shipGroup.GatherPoint);
                        stellarObjects = Galaxy.RemoveObjectsWithSystemStar(stellarObjects, systemStar);
                    }
                }
                return SelectDefensiveFleetBase(fleet, stellarObjects, moveToLocationIfAvailable: false);
            }
            StellarObjectList stellarObjectList = new StellarObjectList();
            for (int j = 0; j < ShipGroups.Count; j++)
            {
                ShipGroup shipGroup2 = ShipGroups[j];
                if (shipGroup2.GatherPoint != null && !stellarObjectList.Contains(shipGroup2.GatherPoint))
                {
                    stellarObjectList.Add(shipGroup2.GatherPoint);
                }
            }
            StellarObjectList stellarObjectList2 = new StellarObjectList();
            if (Capital != null && !stellarObjectList.Contains(Capital))
            {
                stellarObjectList2.Add(Capital);
            }
            if (PirateEmpireBaseHabitat != null)
            {
                Habitat habitat = _ColonizationTargets.FindNearestHabitat(PirateEmpireBaseHabitat.Xpos, PirateEmpireBaseHabitat.Ypos);
                if (habitat != null && fleet.LeadShip != null)
                {
                    StellarObject stellarObject = FindNearestRefuellingPoint(habitat.Xpos, habitat.Ypos, fleet.LeadShip.FuelType, 4);
                    if (stellarObject != null)
                    {
                        stellarObjectList2.Add(stellarObject);
                    }
                }
            }
            else
            {
                EmpireList empireList = IdentifyTargetEmpires();
                foreach (Empire item in empireList)
                {
                    Habitat habitat2 = _Galaxy.FastFindNearestColony((int)Capital.Xpos, (int)Capital.Ypos, item, 0);
                    if (habitat2 != null && CheckSystemExplored(habitat2.SystemIndex) && fleet.LeadShip != null)
                    {
                        StellarObject stellarObject2 = FindNearestRefuellingPoint(habitat2.Xpos, habitat2.Ypos, fleet.LeadShip.FuelType, 4);
                        if (stellarObject2 != null)
                        {
                            stellarObjectList2.Add(stellarObject2);
                        }
                    }
                }
            }
            for (int k = 0; k < stellarObjectList2.Count; k++)
            {
                if (!stellarObjectList.Contains(stellarObjectList2[k]) && (fleet.GatherPoint == null || !stellarObjectList2.Contains(fleet.GatherPoint)))
                {
                    return stellarObjectList2[k];
                }
            }
            if (fleet.GatherPoint == null)
            {
                if (PirateEmpireBaseHabitat == null)
                {
                    Habitat habitat3 = SelectRandomSpacePortColony(stellarObjectList);
                    if (habitat3 == null)
                    {
                        return SelectRandomColony();
                    }
                    return habitat3;
                }
                BuiltObject builtObject = SelectRandomSpacePort();
                if (builtObject != null)
                {
                    return builtObject;
                }
            }
            return fleet.GatherPoint;
        }

        public int CountFleetsUsingColonyAsBase(Habitat colony)
        {
            int num = 0;
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (shipGroup.GatherPoint == colony)
                {
                    num++;
                }
            }
            return num;
        }

        public StellarObject IdentifyNewFleetDefendLocation()
        {
            StellarObjectList stellarObjectList = IdentifyDefendLocations();
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                if (ShipGroups[i] != null)
                {
                    StellarObject stellarObject = ShipGroups[i].IdentifyFleetLocation();
                    if (stellarObject != null && stellarObjectList.Contains(stellarObject))
                    {
                        stellarObjectList.Remove(stellarObject);
                    }
                }
            }
            if (stellarObjectList != null && stellarObjectList.Count > 0)
            {
                return stellarObjectList[0];
            }
            return null;
        }

        public StellarObjectList IdentifyDefendLocations()
        {
            StellarObjectList stellarObjectList = new StellarObjectList();
            HabitatList habitatList = IdentifyEmpireCapitals();
            if (Colonies != null)
            {
                for (int i = 0; i < Colonies.Count; i++)
                {
                    double num = Colonies[i].AnnualRevenue;
                    if (habitatList.Contains(Colonies[i]))
                    {
                        num = Math.Max(num, 1000000.0);
                    }
                    Colonies[i].SortTag = num;
                    stellarObjectList.Add(Colonies[i]);
                }
            }
            for (int j = 0; j < SpacePorts.Count; j++)
            {
                bool flag = true;
                if (SpacePorts[j].ParentHabitat != null && stellarObjectList.Contains(SpacePorts[j].ParentHabitat))
                {
                    flag = false;
                }
                if (flag)
                {
                    double sortTag = 0.0;
                    switch (SpacePorts[j].SubRole)
                    {
                        case BuiltObjectSubRole.SmallSpacePort:
                            sortTag = 200000.0;
                            break;
                        case BuiltObjectSubRole.MediumSpacePort:
                            sortTag = 600000.0;
                            break;
                        case BuiltObjectSubRole.LargeSpacePort:
                            sortTag = 2000000.0;
                            break;
                    }
                    SpacePorts[j].SortTag = sortTag;
                    stellarObjectList.Add(SpacePorts[j]);
                }
            }
            StellarObjectList stellarObjectList2 = DetermineRestrictedResourceSupplyLocations();
            for (int k = 0; k < stellarObjectList2.Count; k++)
            {
                bool flag2 = true;
                if (stellarObjectList2[k].ParentHabitat != null)
                {
                    if (stellarObjectList.Contains(stellarObjectList2[k].ParentHabitat))
                    {
                        flag2 = false;
                    }
                    else if (stellarObjectList.Contains(stellarObjectList2[k]))
                    {
                        flag2 = false;
                    }
                }
                if (flag2)
                {
                    stellarObjectList2[k].SortTag = 3000000.0;
                    stellarObjectList.Add(stellarObjectList2[k]);
                }
            }
            stellarObjectList.Sort();
            stellarObjectList.Reverse();
            return stellarObjectList;
        }

        public StellarObject FindNearestRefuellingPoint(double x, double y, Resource fuelType, int minimumDockingBays)
        {
            StellarObject result = null;
            double num = double.MaxValue;
            if (minimumDockingBays <= 3 && PirateEmpireBaseHabitat == null)
            {
                for (int i = 0; i < Colonies.Count; i++)
                {
                    if (fuelType == null)
                    {
                        continue;
                    }
                    int num2 = Colonies[i].Cargo.IndexOf(fuelType, this);
                    if (num2 >= 0)
                    {
                        double num3 = _Galaxy.CalculateDistanceSquared(x, y, Colonies[i].Xpos, Colonies[i].Ypos);
                        if (num3 < num)
                        {
                            result = Colonies[i];
                            num = num3;
                        }
                    }
                }
            }
            for (int j = 0; j < SpacePorts.Count; j++)
            {
                if (SpacePorts[j].DockingBays != null && SpacePorts[j].DockingBays.Count >= minimumDockingBays)
                {
                    double num4 = _Galaxy.CalculateDistanceSquared(x, y, SpacePorts[j].Xpos, SpacePorts[j].Ypos);
                    if (num4 < num)
                    {
                        result = SpacePorts[j];
                        num = num4;
                    }
                }
            }
            for (int k = 0; k < MiningStations.Count; k++)
            {
                if (MiningStations[k].DockingBays != null && MiningStations[k].DockingBays.Count >= minimumDockingBays && fuelType != null && MiningStations[k].ParentHabitat != null && MiningStations[k].ParentHabitat.Resources.IndexOf(fuelType.ResourceID, 0) >= 0)
                {
                    double num5 = _Galaxy.CalculateDistanceSquared(x, y, MiningStations[k].Xpos, MiningStations[k].Ypos);
                    if (num5 < num)
                    {
                        result = MiningStations[k];
                        num = num5;
                    }
                }
            }
            return result;
        }

        public EmpireList IdentifyTargetEmpires()
        {
            EmpireList empireList = new EmpireList();
            if (PirateEmpireBaseHabitat != null)
            {
                for (int i = 0; i < PirateRelations.Count; i++)
                {
                    PirateRelation pirateRelation = PirateRelations[i];
                    if (pirateRelation != null && pirateRelation.OtherEmpire != null && pirateRelation.Type == PirateRelationType.None)
                    {
                        empireList.Add(pirateRelation.OtherEmpire);
                    }
                }
            }
            else
            {
                for (int j = 0; j < DiplomaticRelations.Count; j++)
                {
                    DiplomaticRelation diplomaticRelation = DiplomaticRelations[j];
                    if (diplomaticRelation.Type == DiplomaticRelationType.War && !empireList.Contains(diplomaticRelation.OtherEmpire))
                    {
                        empireList.Add(diplomaticRelation.OtherEmpire);
                    }
                }
                for (int k = 0; k < DiplomaticRelations.Count; k++)
                {
                    DiplomaticRelation diplomaticRelation2 = DiplomaticRelations[k];
                    if (diplomaticRelation2.Type != DiplomaticRelationType.War && diplomaticRelation2.Strategy == DiplomaticStrategy.Conquer && !empireList.Contains(diplomaticRelation2.OtherEmpire))
                    {
                        empireList.Add(diplomaticRelation2.OtherEmpire);
                    }
                }
            }
            return empireList;
        }

        public BuiltObject SelectRandomSpacePort()
        {
            return SelectRandomSpacePort(new StellarObjectList());
        }

        public BuiltObject SelectRandomSpacePort(StellarObjectList spacePortsToExclude)
        {
            int num = Galaxy.Rnd.Next(0, SpacePorts.Count);
            for (int i = num; i < SpacePorts.Count; i++)
            {
                if (SpacePorts[i] != null && !spacePortsToExclude.Contains(SpacePorts[i]))
                {
                    return SpacePorts[i];
                }
            }
            for (int j = 0; j < num; j++)
            {
                if (SpacePorts[j] != null && !spacePortsToExclude.Contains(SpacePorts[j]))
                {
                    return SpacePorts[j];
                }
            }
            return null;
        }

        public Habitat SelectRandomSpacePortColony(StellarObjectList coloniesToExclude)
        {
            int num = Galaxy.Rnd.Next(0, SpacePorts.Count);
            for (int i = num; i < SpacePorts.Count; i++)
            {
                if (SpacePorts[i].ParentHabitat != null && !coloniesToExclude.Contains(SpacePorts[i].ParentHabitat))
                {
                    return SpacePorts[i].ParentHabitat;
                }
            }
            for (int j = 0; j < num; j++)
            {
                if (SpacePorts[j].ParentHabitat != null && !coloniesToExclude.Contains(SpacePorts[j].ParentHabitat))
                {
                    return SpacePorts[j].ParentHabitat;
                }
            }
            return null;
        }

        public Habitat SelectRandomColony()
        {
            int index = Galaxy.Rnd.Next(0, Colonies.Count);
            return Colonies[index];
        }

        private bool IsShipGroupAvailable(ShipGroup shipGroup, BuiltObjectMissionPriority maximumPriority, int minimumFirepower, Point locationFuelCheck)
        {
            if (shipGroup.LeadShip.WithinFuelRangeAndRefuel(locationFuelCheck.X, locationFuelCheck.Y, 0.0))
            {
                return IsShipGroupAvailableWithAttackStrength(shipGroup, maximumPriority, minimumFirepower);
            }
            return false;
        }

        private bool IsShipGroupAvailableWithAttackStrength(ShipGroup shipGroup, BuiltObjectMissionPriority maximumPriority, int overallStrength)
        {
            if (shipGroup.TotalOverallStrengthFactor >= overallStrength && (shipGroup.Mission == null || shipGroup.Mission.Type == BuiltObjectMissionType.Undefined || (int)shipGroup.Mission.Priority <= (int)maximumPriority))
            {
                return true;
            }
            return false;
        }

        private ShipGroup FindAvailableShipGroup(BuiltObjectMissionPriority maximumPriority, int minimumFirepower, Point locationFuelCheck)
        {
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (IsShipGroupAvailable(shipGroup, maximumPriority, minimumFirepower, locationFuelCheck))
                {
                    return shipGroup;
                }
            }
            return null;
        }

        private bool IsShipGroupAvailable(ShipGroup shipGroup, BuiltObjectMissionPriority maximumPriorityToInclude, int minimumTroopLevel)
        {
            if ((minimumTroopLevel <= 0 || shipGroup.TotalTroopAttackStrengthNearby(0.3) >= minimumTroopLevel) && (shipGroup.Mission == null || shipGroup.Mission.Type == BuiltObjectMissionType.Undefined || (int)shipGroup.Mission.Priority <= (int)maximumPriorityToInclude))
            {
                return true;
            }
            return false;
        }

        private ShipGroup FindAvailableShipGroup(BuiltObjectMissionPriority maximumPriorityToInclude, int minimumTroopLevel, FleetPosture posture)
        {
            return FindAvailableShipGroup(maximumPriorityToInclude, minimumTroopLevel, posture, shipGroupMustBeAutomated: false);
        }

        private ShipGroup FindAvailableShipGroup(BuiltObjectMissionPriority maximumPriorityToInclude, int minimumTroopLevel, FleetPosture posture, bool shipGroupMustBeAutomated)
        {
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if ((!shipGroupMustBeAutomated || shipGroup.LeadShip.IsAutoControlled) && shipGroup.Posture == posture && IsShipGroupAvailable(shipGroup, maximumPriorityToInclude, minimumTroopLevel))
                {
                    return shipGroup;
                }
            }
            return null;
        }

        private ShipGroup FindAvailableShipGroupAutomated(BuiltObjectMissionPriority maximumPriorityToInclude, int minimumTroopLevel, FleetPosture posture)
        {
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (IsShipGroupAvailable(shipGroup, maximumPriorityToInclude, minimumTroopLevel) && shipGroup.Posture == posture && shipGroup.LeadShip.IsAutoControlled)
                {
                    return shipGroup;
                }
            }
            return null;
        }

        private ShipGroup FindAvailableShipGroupNotTravellingToGatherPoint(BuiltObjectMissionPriority maximumPriorityToInclude, int minimumTroopLevel, FleetPosture posture)
        {
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (IsShipGroupAvailable(shipGroup, maximumPriorityToInclude, minimumTroopLevel) && shipGroup.LeadShip.IsAutoControlled && shipGroup.Posture == posture && (shipGroup.Mission == null || shipGroup.Mission.TargetHabitat != shipGroup.GatherPoint || (shipGroup.Mission.Type != BuiltObjectMissionType.Move && shipGroup.Mission.Type != BuiltObjectMissionType.MoveAndWait)))
                {
                    return shipGroup;
                }
            }
            return null;
        }

        private void ReviewFleetPostures()
        {
            SetDefendFleets(defendingFromAttack: false, assignMovement: false);
            ReviewDefensiveFleetLocations();
        }

        private void UpdateFleetLeadShips()
        {
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (shipGroup != null && shipGroup.LeadShip != null && shipGroup.LeadShip.IsAutoControlled)
                {
                    shipGroup.Update();
                }
            }
        }

        private void MaintainShipGroups()
        {
            List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
            list.Add(BuiltObjectSubRole.Frigate);
            list.Add(BuiltObjectSubRole.Destroyer);
            list.Add(BuiltObjectSubRole.Cruiser);
            list.Add(BuiltObjectSubRole.CapitalShip);
            list.Add(BuiltObjectSubRole.TroopTransport);
            list.Add(BuiltObjectSubRole.Carrier);
            BuiltObjectList builtObjectList = BuiltObjects.GetBuiltObjectsBySubRole(list);
            BuiltObjectList builtObjectList2 = new BuiltObjectList();
            foreach (BuiltObject item in builtObjectList)
            {
                if (item.IsPlanetDestroyer)
                {
                    builtObjectList2.Add(item);
                }
                else if (!item.IsAutoControlled)
                {
                    builtObjectList2.Add(item);
                }
                else if (item.ShipGroup != null)
                {
                    builtObjectList2.Add(item);
                }
            }
            foreach (BuiltObject item2 in builtObjectList2)
            {
                builtObjectList.Remove(item2);
            }
            int num = 0;
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                if (BuiltObjects[i].Role == BuiltObjectRole.Military)
                {
                    num++;
                }
            }
            double val = ((double)DominantRace.AggressionLevel + (double)DominantRace.CautionLevel) / 200.0;
            val = Math.Max(0.8, Math.Min(1.2, val));
            double val2 = 0.6 * val;
            val2 = Math.Max(0.5, Math.Min(0.7, val2));
            val2 = Policy.FleetMilitaryProportionForFleets / 100f;
            int num2 = (int)((double)num * val2);
            if (num2 < 1)
            {
                num2 = 1;
            }
            int num3 = (int)((double)Galaxy.FleetTypicalSize * val);
            int num4 = (int)((double)Galaxy.StrikeForceTypicalSize * val);
            num3 = Policy.FleetTypicalSize;
            num4 = Policy.FleetStrikeForceTypicalSize;
            if (num3 < 1)
            {
                num3 = 1;
            }
            if (num4 < 1)
            {
                num4 = 1;
            }
            int num5 = (int)(_Galaxy.DifficultyLevel * (double)Galaxy.ColonyMaximumTroopStrength);
            int num6 = (int)((double)num5 * val * 2.2);
            int troopTargetStrength = 0;
            if (PirateEmpireBaseHabitat != null && Troops.Count <= 0)
            {
                num6 = 0;
            }
            int num7 = 0;
            foreach (BuiltObject item3 in builtObjectList)
            {
                if (item3.ShipGroup == null && (item3.Mission == null || item3.Mission.Type == BuiltObjectMissionType.Undefined || item3.Mission.Priority == BuiltObjectMissionPriority.Low || item3.Mission.Priority == BuiltObjectMissionPriority.Normal))
                {
                    num7++;
                }
            }
            int num8 = 0;
            int num9 = 0;
            for (int j = 0; j < ShipGroups.Count; j++)
            {
                if (ShipGroups[j].ShipTargetAmount >= 10)
                {
                    num8++;
                }
                else
                {
                    num9++;
                }
            }
            int num10 = num3;
            int num11 = num4;
            double num12 = (double)num2 * 0.75 / (double)num3;
            double num13 = (double)num2 * 0.25 / (double)num4;
            if (num12 > 0.1 && num12 < 1.0)
            {
                num12 = 1.0;
                num13 = 0.0;
                num10 = Math.Min(num3, num7);
                num6 /= 2;
            }
            int num14 = (int)(num12 + 0.5);
            int num15 = (int)(num13 + 0.5);
            double num16 = num12 / (num12 + num13);
            int num17 = Math.Max(1, (int)((double)FleetMaximumCount * num16));
            int num18 = Math.Max(1, FleetMaximumCount - num17);
            if (num14 > num17)
            {
                num14 = num17;
            }
            if (num15 > num18)
            {
                num15 = num18;
            }
            if (num8 > num14 && num8 + num9 > num14 + num15)
            {
                int num19 = num8 - num14;
                ShipGroupList shipGroupList = new ShipGroupList();
                for (int k = 0; k < ShipGroups.Count; k++)
                {
                    ShipGroup shipGroup = ShipGroups[k];
                    if (shipGroup.ShipTargetAmount >= 10 && shipGroup.LeadShip.IsAutoControlled && (shipGroup.Mission == null || shipGroup.Mission.Type == BuiltObjectMissionType.Undefined))
                    {
                        shipGroupList.Add(shipGroup);
                        if (shipGroupList.Count >= num19)
                        {
                            break;
                        }
                    }
                }
                foreach (ShipGroup item4 in shipGroupList)
                {
                    DisbandShipGroup(item4);
                }
            }
            if (num9 > num15 && num8 + num9 > num14 + num15)
            {
                int num20 = num9 - num15;
                ShipGroupList shipGroupList2 = new ShipGroupList();
                for (int l = 0; l < ShipGroups.Count; l++)
                {
                    ShipGroup shipGroup2 = ShipGroups[l];
                    if (shipGroup2.ShipTargetAmount < 10 && shipGroup2.LeadShip.IsAutoControlled && (shipGroup2.Mission == null || shipGroup2.Mission.Type == BuiltObjectMissionType.Undefined))
                    {
                        shipGroupList2.Add(shipGroup2);
                        if (shipGroupList2.Count >= num20)
                        {
                            break;
                        }
                    }
                }
                foreach (ShipGroup item5 in shipGroupList2)
                {
                    DisbandShipGroup(item5);
                }
            }
            BuiltObjectList builtObjectList3 = new BuiltObjectList();
            for (int m = 0; m < ShipGroups.Count; m++)
            {
                ShipGroup shipGroup3 = ShipGroups[m];
                if (!shipGroup3.LeadShip.IsAutoControlled)
                {
                    continue;
                }
                if (shipGroup3.Mission == null || (shipGroup3.Mission.Type != BuiltObjectMissionType.Attack && shipGroup3.Mission.Type != BuiltObjectMissionType.WaitAndAttack))
                {
                    for (int n = 0; n < shipGroup3.Ships.Count; n++)
                    {
                        BuiltObject builtObject = shipGroup3.Ships[n];
                        if (builtObject.SubRole == BuiltObjectSubRole.TroopTransport && builtObject.IsAutoControlled && builtObject.TroopCapacityRemaining >= 100 && (builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined || builtObject.Mission.Priority == BuiltObjectMissionPriority.Undefined || builtObject.Mission.Priority == BuiltObjectMissionPriority.Low))
                        {
                            builtObjectList3.Add(builtObject);
                        }
                    }
                }
                if (shipGroup3.Mission == null || shipGroup3.Mission.Type == BuiltObjectMissionType.Undefined || shipGroup3.Mission.Type == BuiltObjectMissionType.Hold || shipGroup3.Mission.Type == BuiltObjectMissionType.MoveAndWait || shipGroup3.Mission.Type == BuiltObjectMissionType.Refuel || shipGroup3.Mission.Type == BuiltObjectMissionType.Retrofit)
                {
                    int targetShipAmount = num10;
                    if (shipGroup3.ShipTargetAmount < 10)
                    {
                        targetShipAmount = num11;
                    }
                    StellarObject stellarObject = shipGroup3.IdentifyFleetLocation();
                    if (stellarObject == null)
                    {
                        stellarObject = shipGroup3.LeadShip;
                    }
                    if (stellarObject != null)
                    {
                        builtObjectList = SortBuiltObjectsByDistance(builtObjectList, stellarObject.Xpos, stellarObject.Ypos);
                    }
                    AddShipsToShipGroup(shipGroup3, builtObjectList, targetShipAmount, isNew: false, null, 0.6f);
                }
            }
            foreach (BuiltObject item6 in builtObjectList3)
            {
                if (AssignLoadTroopsMission(item6))
                {
                    item6.LeaveShipGroup();
                }
            }
            int num21 = 0;
            while (num8 < num14 && num7 >= num10 / 2 && num21 < 10)
            {
                ShipGroup shipGroup4 = new ShipGroup(_Galaxy);
                shipGroup4.Empire = this;
                shipGroup4.ShipTargetAmount = num3;
                shipGroup4.TroopTargetStrength = num6;
                shipGroup4.GatherPoint = SelectFleetBase(shipGroup4);
                if (shipGroup4.GatherPoint != null)
                {
                    builtObjectList = SortBuiltObjectsByDistance(builtObjectList, shipGroup4.GatherPoint.Xpos, shipGroup4.GatherPoint.Ypos);
                }
                AddShipsToShipGroup(shipGroup4, builtObjectList, num10, isNew: true, shipGroup4.GatherPoint, 0.6f);
                if (shipGroup4.Ships.Count <= 0)
                {
                    break;
                }
                string nextFleetNumberDescription = GetNextFleetNumberDescription();
                shipGroup4.Name = string.Format(TextResolver.GetText("Nth Fleet"), nextFleetNumberDescription);
                ShipGroups.Add(shipGroup4);
                ShipGroups.Sort();
                num7 -= shipGroup4.Ships.Count;
                num8++;
                num21++;
                if (num8 >= num14)
                {
                    break;
                }
            }
            num21 = 0;
            while (num9 < num15 && num7 >= num11 && num21 < 10)
            {
                ShipGroup shipGroup5 = new ShipGroup(_Galaxy);
                shipGroup5.Empire = this;
                shipGroup5.ShipTargetAmount = num4;
                shipGroup5.TroopTargetStrength = troopTargetStrength;
                shipGroup5.GatherPoint = SelectFleetBase(shipGroup5);
                if (shipGroup5.GatherPoint != null)
                {
                    builtObjectList = SortBuiltObjectsByDistance(builtObjectList, shipGroup5.GatherPoint.Xpos, shipGroup5.GatherPoint.Ypos);
                }
                AddShipsToShipGroup(shipGroup5, builtObjectList, num11, isNew: true, shipGroup5.GatherPoint, 0.6f);
                if (shipGroup5.Ships.Count > 0)
                {
                    string nextFleetNumberDescription2 = GetNextFleetNumberDescription();
                    shipGroup5.Name = string.Format(TextResolver.GetText("Nth Strike Force"), nextFleetNumberDescription2);
                    ShipGroups.Add(shipGroup5);
                    ShipGroups.Sort();
                    num7 -= shipGroup5.Ships.Count;
                    num9++;
                    num21++;
                    if (num9 >= num15)
                    {
                        break;
                    }
                    continue;
                }
                break;
            }
        }

        private BuiltObjectList SortBuiltObjectsByDistance(BuiltObjectList builtObjects, double x, double y)
        {
            return SortBuiltObjectsByDistance(builtObjects, x, y, 0.0);
        }

        private BuiltObjectList SortBuiltObjectsByDistance(BuiltObjectList builtObjects, double x, double y, double minimumFuelPortionFilter)
        {
            if (builtObjects != null)
            {
                for (int i = 0; i < builtObjects.Count; i++)
                {
                    if (minimumFuelPortionFilter > 0.0)
                    {
                        double num = _Galaxy.CalculateDistanceSquared(builtObjects[i].Xpos, builtObjects[i].Ypos, x, y);
                        double num2 = builtObjects[i].CurrentFuel / (double)Math.Max(1, builtObjects[i].FuelCapacity);
                        if (num2 < minimumFuelPortionFilter)
                        {
                            num *= 100.0;
                        }
                        builtObjects[i].SortTag = num;
                    }
                    else
                    {
                        builtObjects[i].SortTag = _Galaxy.CalculateDistanceSquared(builtObjects[i].Xpos, builtObjects[i].Ypos, x, y);
                    }
                }
                builtObjects.Sort();
            }
            return builtObjects;
        }

        public void AddShipsToShipGroup(ShipGroup shipGroup, BuiltObjectList militaryShips, int targetShipAmount, bool isNew, StellarObject waypoint)
        {
            AddShipsToShipGroup(shipGroup, militaryShips, targetShipAmount, isNew, waypoint, 0f);
        }

        public void AddShipsToShipGroup(ShipGroup shipGroup, BuiltObjectList militaryShips, int targetShipAmount, bool isNew, StellarObject waypoint, float minimumFuelPortion)
        {
            _ = _Galaxy.CurrentStarDate;
            bool? mustHaveHyperdrive = null;
            if (shipGroup.Ships.Count > 0)
            {
                mustHaveHyperdrive = ((shipGroup.WarpSpeed <= 0) ? new bool?(false) : new bool?(true));
            }
            if (CheckEmpireHasHyperDriveTech(this))
            {
                mustHaveHyperdrive = true;
            }
            StellarObject stellarObject = shipGroup.IdentifyFleetLocation();
            double x;
            double y;
            if (stellarObject != null)
            {
                x = stellarObject.Xpos;
                y = stellarObject.Ypos;
            }
            else if (shipGroup.LeadShip != null)
            {
                x = shipGroup.LeadShip.Xpos;
                y = shipGroup.LeadShip.Ypos;
            }
            else
            {
                x = double.MinValue;
                y = double.MinValue;
            }
            if (shipGroup.Ships.Count < targetShipAmount)
            {
                int iterationCount = 0;
                while (Galaxy.ConditionCheckLimit(shipGroup.Ships.Count < targetShipAmount, 200, ref iterationCount))
                {
                    BuiltObject builtObject = null;
                    builtObject = ((shipGroup.TotalTroopAttackStrength >= shipGroup.TroopTargetStrength) ? FindAvailableMilitaryShip(x, y, militaryShips, mustCarryTroopsIfHaveTroopStorage: false, mustBeFullOfTroops: false, minimumFuelPortion, mustHaveHyperdrive) : FindAvailableMilitaryShip(x, y, militaryShips, mustCarryTroopsIfHaveTroopStorage: true, mustBeFullOfTroops: false, minimumFuelPortion, mustHaveHyperdrive));
                    if (builtObject == null)
                    {
                        break;
                    }
                    shipGroup.AddShipToFleet(builtObject);
                    _Galaxy.AssignFleetWaypointMission(builtObject, allowMissionOverride: true, waypoint);
                    if (shipGroup.Ships.Count > 0)
                    {
                        mustHaveHyperdrive = ((shipGroup.WarpSpeed <= 0) ? new bool?(false) : new bool?(true));
                    }
                }
                if (isNew)
                {
                    shipGroup.Update();
                }
            }
            if (shipGroup.TotalTroopAttackStrength >= shipGroup.TroopTargetStrength)
            {
                return;
            }
            int iterationCount2 = 0;
            while (Galaxy.ConditionCheckLimit(shipGroup.TotalTroopAttackStrength < shipGroup.TroopTargetStrength, 100, ref iterationCount2))
            {
                BuiltObject builtObject2 = FindAvailableMilitaryShip(x, y, militaryShips, mustCarryTroopsIfHaveTroopStorage: false, mustBeFullOfTroops: true, minimumFuelPortion, mustHaveHyperdrive);
                if (builtObject2 == null)
                {
                    break;
                }
                shipGroup.AddShipToFleet(builtObject2);
                _Galaxy.AssignFleetWaypointMission(builtObject2, allowMissionOverride: true, waypoint);
                if (shipGroup.Ships.Count > 0)
                {
                    mustHaveHyperdrive = ((shipGroup.WarpSpeed <= 0) ? new bool?(false) : new bool?(true));
                }
            }
            if (isNew)
            {
                shipGroup.Update();
            }
        }

        private ShipGroup AssembleStrikeGroup(PrioritizedTarget target, int availableStrikeForce, out int strikeForceStrength)
        {
            ShipGroup shipGroup = new ShipGroup(_Galaxy);
            strikeForceStrength = 0;
            double num = Math.Pow(1.0 + ((double)DominantRace.AggressionLevel - (double)DominantRace.CautionLevel) / 100.0, 2.0);
            int num2 = (int)(1.2 * ((double)target.LocationStrength / num));
            if (availableStrikeForce < num2)
            {
                return shipGroup;
            }
            int num3 = 0;
            int num4 = 0;
            if (target.Target is BuiltObject)
            {
                Habitat parentHabitat = ((BuiltObject)target.Target).ParentHabitat;
                if (parentHabitat != null && parentHabitat.Owner == target.Empire)
                {
                    if (parentHabitat.Troops != null)
                    {
                        num4 = parentHabitat.Troops.TotalDefendStrength;
                    }
                    num4 += (int)(parentHabitat.Population.TotalAmount / 20000000) * parentHabitat.Owner.DominantRace.AggressionLevel;
                    num4 = (int)((double)num4 * 1.25);
                }
            }
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = BuiltObjects[i];
                if (builtObject.Role == BuiltObjectRole.Military && (builtObject.Mission == null || builtObject.Mission.Priority == BuiltObjectMissionPriority.Low || builtObject.Mission.Priority == BuiltObjectMissionPriority.Undefined) && builtObject.BuiltAt == null && builtObject.UnbuiltOrDamagedComponentCount <= 0)
                {
                    strikeForceStrength += builtObject.FirepowerRaw;
                    if (builtObject.Troops != null)
                    {
                        num3 += builtObject.Troops.TotalAttackStrength;
                    }
                    shipGroup.AddShipToFleet(builtObject);
                    if (strikeForceStrength >= num2)
                    {
                        break;
                    }
                }
            }
            if (num4 > 0 && (double)num3 < (double)num4 * 0.8)
            {
                for (int j = 0; j < BuiltObjects.Count; j++)
                {
                    BuiltObject builtObject2 = BuiltObjects[j];
                    if (builtObject2.SubRole == BuiltObjectSubRole.TroopTransport && (builtObject2.Mission == null || builtObject2.Mission.Priority == BuiltObjectMissionPriority.Low || builtObject2.Mission.Priority == BuiltObjectMissionPriority.Undefined) && builtObject2.Troops != null && builtObject2.Troops.TotalAttackStrength > 0)
                    {
                        strikeForceStrength += builtObject2.FirepowerRaw;
                        num3 += builtObject2.Troops.TotalAttackStrength;
                        shipGroup.AddShipToFleet(builtObject2);
                        if ((double)num3 >= (double)num4 * 0.8)
                        {
                            break;
                        }
                    }
                }
            }
            return shipGroup;
        }

        public bool CheckSystemVisible(Habitat systemStar)
        {
            if (systemStar != null)
            {
                return CheckSystemVisible(systemStar.SystemIndex);
            }
            return false;
        }

        public bool CheckSystemVisible(int systemIndex)
        {
            if (SystemVisibility.Count > systemIndex)
            {
                SystemVisibilityStatus status = SystemVisibility[systemIndex].Status;
                if (status == SystemVisibilityStatus.Visible)
                {
                    return true;
                }
                if (_EmpiresSharedVisibility != null && _EmpiresSharedVisibility.Count > 0)
                {
                    for (int i = 0; i < _EmpiresSharedVisibility.Count; i++)
                    {
                        Empire empire = _EmpiresSharedVisibility[i];
                        if (empire != null && empire.SystemVisibility != null && empire.SystemVisibility.Count > systemIndex)
                        {
                            status = empire.SystemVisibility[systemIndex].Status;
                            if (status == SystemVisibilityStatus.Visible)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public SystemVisibilityStatus CheckSystemVisibilityStatus(Habitat systemStar)
        {
            return CheckSystemVisibilityStatus(systemStar.SystemIndex);
        }

        public SystemVisibilityStatus CheckSystemVisibilityStatus(int systemIndex)
        {
            SystemVisibilityStatus systemVisibilityStatus = SystemVisibility[systemIndex].Status;
            if (systemVisibilityStatus == SystemVisibilityStatus.Visible)
            {
                return systemVisibilityStatus;
            }
            if (_EmpiresSharedVisibility.Count > 0)
            {
                for (int i = 0; i < _EmpiresSharedVisibility.Count; i++)
                {
                    SystemVisibilityStatus status = _EmpiresSharedVisibility[i].SystemVisibility[systemIndex].Status;
                    switch (status)
                    {
                        case SystemVisibilityStatus.Visible:
                            return status;
                        case SystemVisibilityStatus.Explored:
                            systemVisibilityStatus = SystemVisibilityStatus.Explored;
                            break;
                    }
                }
            }
            return systemVisibilityStatus;
        }

        public void SetEmpireSharedVisibility(Empire otherEmpire)
        {
            if (!_EmpiresSharedVisibility.Contains(otherEmpire))
            {
                _Galaxy.MergeGalaxyMap(otherEmpire, this);
                _EmpiresSharedVisibility.Add(otherEmpire);
            }
        }

        public void ClearEmpireSharedVisibility(Empire otherEmpire)
        {
            if (_EmpiresSharedVisibility.Contains(otherEmpire))
            {
                _Galaxy.MergeGalaxyMap(otherEmpire, this);
                _EmpiresSharedVisibility.Remove(otherEmpire);
            }
        }

        public bool CheckSystemExplored(Habitat systemStar)
        {
            return CheckSystemExplored(systemStar.SystemIndex);
        }

        public bool CheckSystemExplored(int systemIndex)
        {
            SystemVisibilityStatus status = SystemVisibility[systemIndex].Status;
            if (status == SystemVisibilityStatus.Visible || status == SystemVisibilityStatus.Explored)
            {
                return true;
            }
            if (_EmpiresSharedVisibility.Count > 0)
            {
                for (int i = 0; i < _EmpiresSharedVisibility.Count; i++)
                {
                    status = _EmpiresSharedVisibility[i].SystemVisibility[systemIndex].Status;
                    if (status == SystemVisibilityStatus.Visible || status == SystemVisibilityStatus.Explored)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsObjectAreaKnownToThisEmpire(StellarObject stellarObject)
        {
            Habitat habitat = _Galaxy.FindNearestHabitat(stellarObject.Xpos, stellarObject.Ypos);
            if (habitat != null)
            {
                double num = _Galaxy.CalculateDistance(stellarObject.Xpos, stellarObject.Ypos, habitat.Xpos, habitat.Ypos);
                if (num <= (double)Galaxy.MaxSolarSystemSize * 2.1 && CheckSystemExplored(habitat.SystemIndex))
                {
                    return true;
                }
            }
            if (IsObjectVisibleToThisEmpire(stellarObject))
            {
                return true;
            }
            return false;
        }

        public bool IsObjectVisibleToThisEmpire(Creature objectToTest)
        {
            if (!objectToTest.IsVisible)
            {
                return false;
            }
            if (objectToTest.NearestSystemStar != null && CheckSystemVisible(objectToTest.NearestSystemStar.SystemIndex))
            {
                return true;
            }
            for (int i = 0; i < LongRangeScanners.Count; i++)
            {
                BuiltObject builtObject = LongRangeScanners[i];
                double num = (double)builtObject.SensorLongRange * (double)builtObject.SensorLongRange;
                double num2 = _Galaxy.CalculateDistanceSquared(builtObject.Xpos, builtObject.Ypos, objectToTest.Xpos, objectToTest.Ypos);
                if (num2 <= num)
                {
                    return true;
                }
            }
            BuiltObject builtObject2 = FindShipOutsideSystemWithScanRange((int)objectToTest.Xpos, (int)objectToTest.Ypos, 1.0);
            if (builtObject2 != null)
            {
                return true;
            }
            return false;
        }

        public bool IsObjectVisibleToThisEmpireImprecise(StellarObject objectToTest)
        {
            if (objectToTest.Empire == this)
            {
                return true;
            }
            if (_EmpiresViewable.Contains(objectToTest.Empire) || _EmpiresSharedVisibility.Contains(objectToTest.Empire))
            {
                return true;
            }
            if (objectToTest is Habitat)
            {
                Habitat habitat = (Habitat)objectToTest;
                if (CheckSystemVisible(habitat.SystemIndex))
                {
                    return true;
                }
                for (int i = 0; i < LongRangeScanners.Count; i++)
                {
                    BuiltObject builtObject = LongRangeScanners[i];
                    double num = (double)builtObject.SensorLongRange * (double)builtObject.SensorLongRange;
                    double num2 = _Galaxy.CalculateDistanceSquared(builtObject.Xpos, builtObject.Ypos, objectToTest.Xpos, objectToTest.Ypos);
                    if (num2 <= num)
                    {
                        return true;
                    }
                }
            }
            if (objectToTest is Fighter)
            {
                Fighter fighter = (Fighter)objectToTest;
                if (fighter.OnboardCarrier)
                {
                    return false;
                }
                if (fighter.ParentBuiltObject != null)
                {
                    bool visibleKnownPirateBase = false;
                    if (IsBuiltObjectVisibleToThisEmpire(fighter.ParentBuiltObject, out visibleKnownPirateBase) && !visibleKnownPirateBase)
                    {
                        return true;
                    }
                }
                return false;
            }
            if (objectToTest is BuiltObject)
            {
                BuiltObject builtObject2 = (BuiltObject)objectToTest;
                return IsBuiltObjectVisibleToThisEmpire(builtObject2);
            }
            return false;
        }

        private bool IsBuiltObjectVisibleToThisEmpire(BuiltObject builtObject)
        {
            bool visibleKnownPirateBase = false;
            return IsBuiltObjectVisibleToThisEmpire(builtObject, out visibleKnownPirateBase);
        }

        private bool IsBuiltObjectVisibleToThisEmpire(BuiltObject builtObject, out bool visibleKnownPirateBase)
        {
            visibleKnownPirateBase = false;
            if (builtObject != null)
            {
                if (PirateEmpireBaseHabitat != null && builtObject.PirateEmpireId > 0 && builtObject.PirateEmpireId == EmpireId)
                {
                    return true;
                }
                if (builtObject.NearestSystemStar != null)
                {
                    if (CheckSystemVisible(builtObject.NearestSystemStar.SystemIndex))
                    {
                        return true;
                    }
                }
                else if (builtObject.Role != BuiltObjectRole.Base && builtObject.CurrentSpeed > (float)builtObject.TopSpeed)
                {
                    _ = builtObject.CurrentSpeed;
                    _ = 0f;
                }
                for (int i = 0; i < LongRangeScanners.Count; i++)
                {
                    BuiltObject builtObject2 = LongRangeScanners[i];
                    if (builtObject2 != null)
                    {
                        double num = (float)builtObject2.SensorLongRange * builtObject.Stealth;
                        double num2 = num * num;
                        double num3 = _Galaxy.CalculateDistanceSquared(builtObject2.Xpos, builtObject2.Ypos, builtObject.Xpos, builtObject.Ypos);
                        if (num3 <= num2)
                        {
                            return true;
                        }
                    }
                }
                if (_EmpiresSharedVisibility.Count > 0)
                {
                    for (int j = 0; j < _EmpiresSharedVisibility.Count; j++)
                    {
                        Empire empire = _EmpiresSharedVisibility[j];
                        if (empire == null)
                        {
                            continue;
                        }
                        for (int k = 0; k < empire.LongRangeScanners.Count; k++)
                        {
                            BuiltObject builtObject3 = empire.LongRangeScanners[k];
                            if (builtObject3 != null)
                            {
                                double num4 = (float)builtObject3.SensorLongRange * builtObject.Stealth;
                                double num5 = num4 * num4;
                                double num6 = _Galaxy.CalculateDistanceSquared(builtObject3.Xpos, builtObject3.Ypos, builtObject.Xpos, builtObject.Ypos);
                                if (num6 <= num5)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                if (KnownPirateBases != null && KnownPirateBases.Contains(builtObject))
                {
                    visibleKnownPirateBase = true;
                    return true;
                }
            }
            return false;
        }

        public bool IsObjectVisibleToThisEmpire(StellarObject objectToTest)
        {
            return IsObjectVisibleToThisEmpire(objectToTest, includeLongRangeScanners: true, includeShipsOutsideSystems: true);
        }

        public bool IsObjectVisibleToThisEmpire(StellarObject objectToTest, bool includeLongRangeScanners, bool includeShipsOutsideSystems)
        {
            bool flag = IsObjectVisibleToThisEmpireImprecise(objectToTest);
            if (flag)
            {
                return flag;
            }
            BuiltObject builtObject = FindShipOutsideSystemWithScanRange((int)objectToTest.Xpos, (int)objectToTest.Ypos, objectToTest.Stealth, includeLongRangeScanners, includeShipsOutsideSystems);
            if (builtObject != null)
            {
                return true;
            }
            if (_EmpiresSharedVisibility.Count > 0)
            {
                for (int i = 0; i < _EmpiresSharedVisibility.Count; i++)
                {
                    Empire empire = _EmpiresSharedVisibility[i];
                    builtObject = empire.FindShipOutsideSystemWithScanRange((int)objectToTest.Xpos, (int)objectToTest.Ypos, objectToTest.Stealth, includeLongRangeScanners, includeShipsOutsideSystems);
                    if (builtObject != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckSystemLinkedToCapital(Habitat systemStar, Habitat capitalSystemStar, ref HabitatList unlinkedSystemStars)
        {
            unlinkedSystemStars.Add(systemStar);
            if (systemStar != null && SystemVisibility != null && SystemVisibility[systemStar.SystemIndex].LinkSystemStars != null)
            {
                if (SystemVisibility[systemStar.SystemIndex].LinkSystemStars.Contains(capitalSystemStar) || systemStar == capitalSystemStar)
                {
                    return true;
                }
                for (int i = 0; i < SystemVisibility[systemStar.SystemIndex].LinkSystemStars.Count; i++)
                {
                    Habitat habitat = SystemVisibility[systemStar.SystemIndex].LinkSystemStars[i];
                    if (!unlinkedSystemStars.Contains(habitat) && CheckSystemLinkedToCapital(habitat, capitalSystemStar, ref unlinkedSystemStars))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private Habitat DetermineSpacePortSystemLink(BuiltObject spacePort, Habitat capitalSystemStar, BuiltObjectList spacePortsToExclude)
        {
            if (spacePort != null)
            {
                BuiltObject builtObject = _Galaxy.FastFindNearestOtherSpacePort((int)spacePort.Xpos, (int)spacePort.Ypos, this, spacePortsToExclude);
                if (builtObject == null)
                {
                    return capitalSystemStar;
                }
                if (builtObject.NearestSystemStar == spacePort.NearestSystemStar)
                {
                    spacePortsToExclude.Add(builtObject);
                    return DetermineSpacePortSystemLink(spacePort, capitalSystemStar, spacePortsToExclude);
                }
                if (builtObject.NearestSystemStar != null && SystemVisibility[builtObject.NearestSystemStar.SystemIndex].LinkSystemStars.Contains(spacePort.NearestSystemStar))
                {
                    spacePortsToExclude.Add(builtObject);
                    return DetermineSpacePortSystemLink(spacePort, capitalSystemStar, spacePortsToExclude);
                }
                if (builtObject.NearestSystemStar != null)
                {
                    return builtObject.NearestSystemStar;
                }
            }
            return capitalSystemStar;
        }

        public void EvaluateSystemLinks()
        {
            if (this == _Galaxy.IndependentEmpire || PirateEmpireBaseHabitat != null)
            {
                return;
            }
            for (int i = 0; i < SystemVisibility.Count; i++)
            {
                SystemVisibility systemVisibility = SystemVisibility[i];
                if (systemVisibility != null)
                {
                    if (systemVisibility.LinkSystemStars != null)
                    {
                        systemVisibility.LinkSystemStars.Clear();
                    }
                    if (systemVisibility.ReciprocalLinkSystemStars != null)
                    {
                        systemVisibility.ReciprocalLinkSystemStars.Clear();
                    }
                }
            }
            if (_Capital == null)
            {
                return;
            }
            Habitat habitat = Galaxy.DetermineHabitatSystemStar(_Capital);
            HabitatList habitatList = DetermineEmpireSystems(this);
            for (int j = 0; j < habitatList.Count; j++)
            {
                Habitat habitat2 = habitatList[j];
                if (habitat2 == null)
                {
                    continue;
                }
                BuiltObject builtObject = _Galaxy.FastFindNearestSpacePort(habitat2.Xpos, habitat2.Ypos, this);
                if (builtObject != null)
                {
                    if (builtObject.NearestSystemStar != habitat2)
                    {
                        SystemVisibility[habitat2.SystemIndex].LinkSystemStars.Add(builtObject.NearestSystemStar);
                        continue;
                    }
                    BuiltObjectList builtObjectList = new BuiltObjectList();
                    builtObjectList.Add(builtObject);
                    SystemVisibility[habitat2.SystemIndex].LinkSystemStars.Add(DetermineSpacePortSystemLink(builtObject, habitat, builtObjectList));
                }
                else
                {
                    SystemVisibility[habitat2.SystemIndex].LinkSystemStars.Add(habitat);
                }
            }
            for (int k = 0; k < habitatList.Count; k++)
            {
                Habitat habitat3 = habitatList[k];
                if (habitat3 == null)
                {
                    continue;
                }
                HabitatList unlinkedSystemStars = new HabitatList();
                if (CheckSystemLinkedToCapital(habitat3, habitat, ref unlinkedSystemStars))
                {
                    continue;
                }
                Habitat habitat4 = null;
                double num = double.MaxValue;
                for (int l = 0; l < unlinkedSystemStars.Count; l++)
                {
                    Habitat habitat5 = unlinkedSystemStars[l];
                    if (habitat5 == null)
                    {
                        continue;
                    }
                    BuiltObject builtObject2 = _Galaxy.FastFindNearestSpacePort(habitat5.Xpos, habitat5.Ypos, this);
                    if (builtObject2 != null && builtObject2.NearestSystemStar == habitat5)
                    {
                        double num2 = _Galaxy.CalculateDistanceSquared(habitat5.Xpos, habitat5.Ypos, habitat.Xpos, habitat.Ypos);
                        if (num2 < num)
                        {
                            habitat4 = habitat5;
                            num = num2;
                        }
                    }
                }
                if (habitat4 == null)
                {
                    habitat4 = unlinkedSystemStars[Galaxy.Rnd.Next(0, unlinkedSystemStars.Count)];
                }
                if (habitat4 != null)
                {
                    SystemVisibility[habitat4.SystemIndex].LinkSystemStars.Add(habitat);
                }
            }
            for (int m = 0; m < habitatList.Count; m++)
            {
                Habitat habitat6 = habitatList[m];
                if (habitat6 == null)
                {
                    continue;
                }
                for (int n = 0; n < SystemVisibility[habitat6.SystemIndex].LinkSystemStars.Count; n++)
                {
                    Habitat habitat7 = SystemVisibility[habitat6.SystemIndex].LinkSystemStars[n];
                    if (habitat7 != null && SystemVisibility[habitat7.SystemIndex].ReciprocalLinkSystemStars != null)
                    {
                        SystemVisibility[habitat7.SystemIndex].ReciprocalLinkSystemStars.Add(habitat6);
                    }
                }
            }
        }

        public BuiltObject FindShipInSystemWithScanRange(double x, double y, Habitat systemStar, double rangeModifier)
        {
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = BuiltObjects[i];
                if (builtObject == null || builtObject.NearestSystemStar != systemStar || !(builtObject.CurrentSpeed <= (float)builtObject.TopSpeed))
                {
                    continue;
                }
                int num = (int)((double)Math.Max(Galaxy.ThreatRange, builtObject.SensorProximityArrayRange) * rangeModifier);
                if (_Galaxy.CheckWithinDistancePotentialUnmodified(num, x, y, builtObject.Xpos, builtObject.Ypos))
                {
                    double num2 = _Galaxy.CalculateDistance(x, y, builtObject.Xpos, builtObject.Ypos);
                    if ((int)num2 <= num)
                    {
                        return builtObject;
                    }
                }
            }
            for (int j = 0; j < PrivateBuiltObjects.Count; j++)
            {
                BuiltObject builtObject2 = PrivateBuiltObjects[j];
                if (builtObject2 == null || builtObject2.NearestSystemStar != systemStar || !(builtObject2.CurrentSpeed <= (float)builtObject2.TopSpeed))
                {
                    continue;
                }
                int num3 = (int)((double)Math.Max(Galaxy.ThreatRange, builtObject2.SensorProximityArrayRange) * rangeModifier);
                if (_Galaxy.CheckWithinDistancePotentialUnmodified(num3, x, y, builtObject2.Xpos, builtObject2.Ypos))
                {
                    double num4 = _Galaxy.CalculateDistance(x, y, builtObject2.Xpos, builtObject2.Ypos);
                    if ((int)num4 <= num3)
                    {
                        return builtObject2;
                    }
                }
            }
            return null;
        }

        public BuiltObject FindLongRangeScannerThatCanSeePoint(double x, double y, double rangeModifier)
        {
            if (LongRangeScanners != null)
            {
                for (int i = 0; i < LongRangeScanners.Count; i++)
                {
                    BuiltObject builtObject = LongRangeScanners[i];
                    if (builtObject != null && builtObject.SensorLongRange > 0 && builtObject.CurrentSpeed == 0f)
                    {
                        double num = (double)builtObject.SensorLongRange * rangeModifier;
                        num *= num;
                        double num2 = _Galaxy.CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                        if (num2 <= num)
                        {
                            return builtObject;
                        }
                    }
                }
            }
            return null;
        }

        public BuiltObject FindShipOutsideSystemWithScanRange(int x, int y, double rangeModifier)
        {
            return FindShipOutsideSystemWithScanRange(x, y, rangeModifier, includeLongRangeScanners: true, includeShipsOutsideSystems: true);
        }

        public BuiltObject FindShipOutsideSystemWithScanRange(double x, double y, double rangeModifier, bool includeLongRangeScanners, bool includeShipsOutsideSystems)
        {
            if (includeLongRangeScanners && LongRangeScanners != null)
            {
                for (int i = 0; i < LongRangeScanners.Count; i++)
                {
                    BuiltObject builtObject = LongRangeScanners[i];
                    if (builtObject != null && builtObject.SensorLongRange > 0 && builtObject.CurrentSpeed == 0f)
                    {
                        double num = (double)builtObject.SensorLongRange * rangeModifier;
                        num *= num;
                        double num2 = _Galaxy.CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                        if (num2 <= num)
                        {
                            return builtObject;
                        }
                    }
                }
            }
            if (includeShipsOutsideSystems)
            {
                GalaxyIndex galaxyIndex = Galaxy.ResolveIndex(x, y);
                BuiltObject[] array = ListHelper.ToArrayThreadSafe(_Galaxy.BuiltObjectIndex[galaxyIndex.X][galaxyIndex.Y]);
                foreach (BuiltObject builtObject2 in array)
                {
                    if (builtObject2 == null || builtObject2.NearestSystemStar != null || ((builtObject2.WarpSpeed <= 0 || !(builtObject2.CurrentSpeed < (float)builtObject2.WarpSpeed)) && !(builtObject2.CurrentSpeed <= (float)builtObject2.TopSpeed)) || builtObject2.Empire != this)
                    {
                        continue;
                    }
                    int num3 = (int)((double)Math.Max(Galaxy.ThreatRange, Math.Max(builtObject2.SensorLongRange, builtObject2.SensorProximityArrayRange)) * rangeModifier);
                    if (_Galaxy.CheckWithinDistancePotentialUnmodified(num3, x, y, builtObject2.Xpos, builtObject2.Ypos))
                    {
                        double num4 = _Galaxy.CalculateDistance(x, y, builtObject2.Xpos, builtObject2.Ypos);
                        if ((int)num4 <= num3)
                        {
                            return builtObject2;
                        }
                    }
                }
            }
            return null;
        }

        public ShipGroupList GenerateDistanceOrderedFleetList(PrioritizedTarget target, ShipGroupList fleets)
        {
            ShipGroupList result = new ShipGroupList();
            if (target != null)
            {
                double x = 0.0;
                double y = 0.0;
                target.ResolveTargetCoordinates(out x, out y);
                result = GenerateDistanceOrderedFleetList(x, y, fleets);
            }
            return result;
        }

        public ShipGroupList GenerateDistanceOrderedFleetList(double targetX, double targetY, ShipGroupList fleets)
        {
            ShipGroupList shipGroupList = new ShipGroupList();
            shipGroupList.AddRange(fleets);
            for (int i = 0; i < shipGroupList.Count; i++)
            {
                ShipGroup shipGroup = shipGroupList[i];
                shipGroup.SortTag = _Galaxy.CalculateDistance(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, targetX, targetY);
            }
            shipGroupList.Sort();
            shipGroupList.ClearSortTags();
            return shipGroupList;
        }

        public HabitatList IdentifyOurDisputedColonies(Empire empire)
        {
            HabitatList habitatList = new HabitatList();
            HabitatList habitatList2 = DetermineEmpireSystems(empire);
            for (int i = 0; i < habitatList2.Count; i++)
            {
                Habitat habitat = habitatList2[i];
                SystemInfo bySystemIndex = _Galaxy.Systems.GetBySystemIndex(habitat.SystemIndex);
                if (bySystemIndex.Habitats == null)
                {
                    continue;
                }
                for (int j = 0; j < bySystemIndex.Habitats.Count; j++)
                {
                    Habitat habitat2 = bySystemIndex.Habitats[j];
                    if (habitat2.Empire != null && habitat2.Empire != _Galaxy.IndependentEmpire && habitat2.Empire != empire && (CheckSystemExplored(habitat2.SystemIndex) || IsObjectVisibleToThisEmpire(habitat2)))
                    {
                        habitatList.Add(habitat2);
                    }
                }
            }
            return habitatList;
        }

        private BuiltObjectList IdentifyDisputedBases(Empire otherEmpire)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < otherEmpire.DisputedBases.Count; i++)
            {
                BuiltObject builtObject = otherEmpire.DisputedBases[i];
                if (builtObject.HasBeenDestroyed)
                {
                    continue;
                }
                if (builtObject.NearestSystemStar != null)
                {
                    SystemInfo bySystemIndex = _Galaxy.Systems.GetBySystemIndex(builtObject.NearestSystemStar.SystemIndex);
                    if (bySystemIndex != null && bySystemIndex.DominantEmpire != null && bySystemIndex.DominantEmpire.Empire != null && bySystemIndex.DominantEmpire.Empire == this && (CheckSystemExplored(builtObject.NearestSystemStar.SystemIndex) || IsObjectVisibleToThisEmpire(builtObject)))
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
                else
                {
                    int num = _Galaxy.CheckEmpireTerritoryIdAtLocation(builtObject.Xpos, builtObject.Ypos);
                    if (num == EmpireId && IsObjectVisibleToThisEmpire(builtObject))
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
            }
            return builtObjectList;
        }

        private HabitatPrioritizationList IdentifyDesiredForeignColonies(Empire empire)
        {
            return IdentifyDesiredForeignColonies(empire, 2.0);
        }

        private HabitatPrioritizationList IdentifyDesiredForeignColonies(Empire empire, double proximityValueThreshold)
        {
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            double num = 1.5;
            double num2 = (double)Galaxy.SectorSize * num * ((double)Galaxy.SectorSize * num);
            for (int i = 0; i < empire.Colonies.Count; i++)
            {
                Habitat habitat = empire.Colonies[i];
                if (habitat == null || habitat.HasBeenDestroyed || (!CheckSystemExplored(habitat.SystemIndex) && !IsObjectVisibleToThisEmpire(habitat)))
                {
                    continue;
                }
                Habitat habitat2 = _Galaxy.FastFindNearestColony(habitat.Xpos, habitat.Ypos, this, 0);
                if (habitat2 == null)
                {
                    continue;
                }
                double num3 = _Galaxy.CalculateDistanceSquared(habitat.Xpos, habitat.Ypos, habitat2.Xpos, habitat2.Ypos);
                if (num3 < num2)
                {
                    double num4 = _Galaxy.CalculateEmpireColonyProximityValueAtPoint(this, habitat.Xpos, habitat.Ypos, num2);
                    if (num4 > proximityValueThreshold)
                    {
                        HabitatPrioritization item = new HabitatPrioritization(habitat, (int)num4);
                        habitatPrioritizationList.Add(item);
                    }
                }
            }
            habitatPrioritizationList.Sort();
            habitatPrioritizationList.Reverse();
            return habitatPrioritizationList;
        }

        private BuiltObjectList IdentifyDesiredForeignBases(Empire empire)
        {
            return IdentifyDesiredForeignBases(empire, 10.0);
        }

        private BuiltObjectList IdentifyDesiredForeignBases(Empire empire, double proximityValueThreshold)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            double num = 1.0;
            double num2 = (double)Galaxy.SectorSize * num * ((double)Galaxy.SectorSize * num);
            BuiltObjectList builtObjectList2 = new BuiltObjectList();
            List<double> list = new List<double>();
            if (empire.BuiltObjects != null)
            {
                builtObjectList2.AddRange(empire.BuiltObjects);
            }
            if (empire.PrivateBuiltObjects != null)
            {
                builtObjectList2.AddRange(empire.PrivateBuiltObjects);
            }
            for (int i = 0; i < builtObjectList2.Count; i++)
            {
                BuiltObject builtObject = builtObjectList2[i];
                if (builtObject == null || builtObject.Role != BuiltObjectRole.Base || builtObject.HasBeenDestroyed || !IsObjectVisibleToThisEmpire(builtObject))
                {
                    continue;
                }
                Habitat habitat = _Galaxy.FastFindNearestColony(builtObject.Xpos, builtObject.Ypos, this, 0);
                if (habitat == null)
                {
                    continue;
                }
                double num3 = _Galaxy.CalculateDistanceSquared(builtObject.Xpos, builtObject.Ypos, habitat.Xpos, habitat.Ypos);
                if (num3 < num2)
                {
                    double num4 = _Galaxy.CalculateEmpireColonyProximityValueAtPoint(this, builtObject.Xpos, builtObject.Ypos, num2);
                    if (num4 > proximityValueThreshold)
                    {
                        builtObjectList.Add(builtObject);
                        list.Add(num4);
                    }
                }
            }
            BuiltObject[] array = builtObjectList.ToArray();
            double[] keys = list.ToArray();
            Array.Sort(keys, array);
            Array.Reverse(array);
            BuiltObjectList builtObjectList3 = new BuiltObjectList();
            builtObjectList3.AddRange(array);
            return builtObjectList3;
        }

        public void IdentifyEmpireWarObjectives(Empire empire, out HabitatList targetedColonies, out BuiltObjectList targetedBases)
        {
            double proximityValueThreshold = 2.0 / Policy.WarWillingness;
            double proximityValueThreshold2 = 10.0 / Policy.WarWillingness;
            HabitatList items = new HabitatList();
            if (!Reclusive)
            {
                items = IdentifyOurDisputedColonies(this);
            }
            BuiltObjectList items2 = IdentifyDisputedBases(empire);
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            if (!Reclusive)
            {
                habitatPrioritizationList = IdentifyDesiredForeignColonies(empire, proximityValueThreshold);
            }
            BuiltObjectList items3 = IdentifyDesiredForeignBases(empire, proximityValueThreshold2);
            Habitat habitat = null;
            if (!Reclusive)
            {
                habitat = CheckEmpireBuildingVictoryWonderAtKnownColony(empire);
                if (habitat != null)
                {
                    DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                    if (!EvaluateShouldAttackWonderBuildingEmpire(empire, habitat, diplomaticRelation.Type, diplomaticRelation.Strategy))
                    {
                        habitat = null;
                    }
                }
            }
            targetedColonies = new HabitatList();
            if (habitat != null)
            {
                targetedColonies.Add(habitat);
            }
            targetedColonies.AddRange(items);
            targetedColonies.AddRange(habitatPrioritizationList.ResolveHabitats());
            targetedBases = new BuiltObjectList();
            targetedBases.AddRange(items2);
            targetedBases.AddRange(items3);
        }

        public PrioritizedTargetList IdentifyEmpireStrikePoints(Empire empire)
        {
            PrioritizedTargetList prioritizedTargetList = new PrioritizedTargetList();
            double num = 1.0;
            double num2 = 1.0;
            double num3 = 1.0;
            double num4 = 1.0;
            if (TargetHabitat != null && CheckSystemExplored(TargetHabitat.SystemIndex) && TargetHabitat.Empire != null && TargetHabitat.Empire == empire && !TargetHabitat.HasBeenDestroyed)
            {
                PrioritizedTarget prioritizedTarget = new PrioritizedTarget(TargetHabitat, 1000000000);
                prioritizedTargetList.Add(prioritizedTarget);
            }
            if (!Reclusive && empire.Colonies != null)
            {
                for (int i = 0; i < empire.Colonies.Count; i++)
                {
                    Habitat habitat = empire.Colonies[i];
                    if (CheckSystemExplored(habitat.SystemIndex) || IsObjectVisibleToThisEmpire(habitat))
                    {
                        int priority = (int)(1050000.0 * num);
                        PrioritizedTarget prioritizedTarget2 = new PrioritizedTarget(habitat, priority);
                        prioritizedTargetList.Add(prioritizedTarget2);
                    }
                }
            }
            if (empire.SpacePorts != null)
            {
                for (int j = 0; j < empire.SpacePorts.Count; j++)
                {
                    BuiltObject builtObject = empire.SpacePorts[j];
                    if (IsObjectVisibleToThisEmpire(builtObject) && builtObject.ParentHabitat != null)
                    {
                        int priority2 = (int)(1000000.0 * num2);
                        PrioritizedTarget prioritizedTarget3 = new PrioritizedTarget(builtObject.ParentHabitat, priority2);
                        if (Reclusive)
                        {
                            prioritizedTarget3 = new PrioritizedTarget(builtObject, priority2);
                        }
                        if (builtObject.ParentHabitat.Empire == null)
                        {
                            priority2 = (int)(20000.0 * num2);
                            prioritizedTarget3 = new PrioritizedTarget(builtObject, priority2);
                        }
                        prioritizedTargetList.Add(prioritizedTarget3);
                    }
                }
            }
            if (empire.MiningStations != null)
            {
                for (int k = 0; k < empire.MiningStations.Count; k++)
                {
                    BuiltObject builtObject2 = empire.MiningStations[k];
                    if (IsObjectVisibleToThisEmpire(builtObject2))
                    {
                        int priority3 = (int)(1000000.0 * num3);
                        PrioritizedTarget prioritizedTarget4 = new PrioritizedTarget(builtObject2, priority3);
                        prioritizedTargetList.Add(prioritizedTarget4);
                    }
                }
            }
            if (empire.ResearchFacilities != null)
            {
                for (int l = 0; l < empire.ResearchFacilities.Count; l++)
                {
                    BuiltObject builtObject3 = empire.ResearchFacilities[l];
                    if (IsObjectVisibleToThisEmpire(builtObject3))
                    {
                        int priority4 = 500000;
                        PrioritizedTarget prioritizedTarget5 = new PrioritizedTarget(builtObject3, priority4);
                        prioritizedTargetList.Add(prioritizedTarget5);
                    }
                }
            }
            if (DominantRace.AggressionLevel > 115 && Galaxy.Rnd.Next(0, 3) == 1 && empire.ResortBases != null)
            {
                for (int m = 0; m < empire.ResortBases.Count; m++)
                {
                    BuiltObject builtObject4 = empire.ResortBases[m];
                    if (IsObjectVisibleToThisEmpire(builtObject4))
                    {
                        int priority5 = 500000;
                        PrioritizedTarget prioritizedTarget6 = new PrioritizedTarget(builtObject4, priority5);
                        prioritizedTargetList.Add(prioritizedTarget6);
                    }
                }
            }
            if (empire.LongRangeScanners != null)
            {
                for (int n = 0; n < empire.LongRangeScanners.Count; n++)
                {
                    BuiltObject builtObject5 = empire.LongRangeScanners[n];
                    if (builtObject5.SubRole != BuiltObjectSubRole.SmallSpacePort && builtObject5.SubRole != BuiltObjectSubRole.MediumSpacePort && builtObject5.SubRole != BuiltObjectSubRole.LargeSpacePort && IsObjectVisibleToThisEmpire(builtObject5))
                    {
                        int priority6 = 500000;
                        PrioritizedTarget prioritizedTarget7 = new PrioritizedTarget(builtObject5, priority6);
                        prioritizedTargetList.Add(prioritizedTarget7);
                    }
                }
            }
            if (empire.ShipGroups != null)
            {
                for (int num5 = 0; num5 < empire.ShipGroups.Count; num5++)
                {
                    ShipGroup shipGroup = empire.ShipGroups[num5];
                    if (shipGroup.LeadShip != null && IsObjectVisibleToThisEmpire(shipGroup.LeadShip) && shipGroup.LeadShip.CurrentSpeed < (float)shipGroup.LeadShip.WarpSpeed)
                    {
                        int priority7 = (int)(1000000.0 * num4);
                        PrioritizedTarget prioritizedTarget8 = new PrioritizedTarget(shipGroup, priority7);
                        prioritizedTargetList.Add(prioritizedTarget8);
                    }
                }
            }
            prioritizedTargetList.Sort();
            prioritizedTargetList.Reverse();
            for (int num6 = 0; num6 < prioritizedTargetList.Count; num6++)
            {
                PrioritizedTarget prioritizedTarget9 = prioritizedTargetList[num6];
                Habitat habitat2 = null;
                int num7 = 0;
                double distanceFromAttackingEmpire = 1000000.0;
                Habitat habitat3 = null;
                int strategicValueThreshhold = 1000000;
                if (prioritizedTarget9.Target is Habitat)
                {
                    Habitat habitat4 = (Habitat)prioritizedTarget9.Target;
                    habitat3 = _Galaxy.FastFindNearestColony(habitat4.Xpos, habitat4.Ypos, this, strategicValueThreshhold);
                    if (habitat3 == null)
                    {
                        habitat3 = Capital;
                    }
                    if (habitat3 != null)
                    {
                        distanceFromAttackingEmpire = _Galaxy.CalculateDistance(habitat3.Xpos, habitat3.Ypos, habitat4.Xpos, habitat4.Ypos);
                        habitat2 = Galaxy.DetermineHabitatSystemStar(habitat4);
                        if (habitat2 != null)
                        {
                            if (CheckSystemVisible(habitat2))
                            {
                                num7 += _Galaxy.DetermineDefendingStrength(habitat4, empire);
                            }
                            else
                            {
                                num7 = (int)((double)habitat4.EstimatedDefensiveForceRequired(atWar: true) * 1.5);
                                BuiltObject builtObject6 = _Galaxy.DetermineSpacePortAtColony(habitat4);
                                if (builtObject6 != null)
                                {
                                    num7 = (int)((double)habitat4.EstimatedDefensiveForceRequired(atWar: true) * 1.0);
                                    num7 += builtObject6.CalculateOverallStrengthFactor();
                                }
                            }
                        }
                        else
                        {
                            num7 = (int)((double)habitat4.EstimatedDefensiveForceRequired(atWar: true) * 1.5);
                        }
                    }
                    else
                    {
                        num7 = (int)((double)habitat4.EstimatedDefensiveForceRequired(atWar: true) * 1.5);
                    }
                }
                else if (prioritizedTarget9.Target is BuiltObject)
                {
                    BuiltObject builtObject7 = (BuiltObject)prioritizedTarget9.Target;
                    habitat3 = _Galaxy.FastFindNearestColony(builtObject7.Xpos, builtObject7.Ypos, this, strategicValueThreshhold);
                    if (habitat3 == null)
                    {
                        habitat3 = Capital;
                    }
                    if (habitat3 != null)
                    {
                        distanceFromAttackingEmpire = _Galaxy.CalculateDistance(habitat3.Xpos, habitat3.Ypos, builtObject7.Xpos, builtObject7.Ypos);
                        num7 = ((builtObject7.NearestSystemStar == null) ? builtObject7.CalculateOverallStrengthFactor() : ((!CheckSystemVisible(builtObject7.NearestSystemStar.SystemIndex)) ? ((builtObject7.ParentHabitat == null) ? builtObject7.CalculateOverallStrengthFactor() : ((int)((double)builtObject7.ParentHabitat.EstimatedDefensiveForceRequired(atWar: true) * 1.5))) : _Galaxy.DetermineDefendingStrength(builtObject7, empire)));
                    }
                    else
                    {
                        num7 = builtObject7.CalculateOverallStrengthFactor();
                    }
                }
                else if (prioritizedTarget9.Target is ShipGroup)
                {
                    ShipGroup shipGroup2 = (ShipGroup)prioritizedTarget9.Target;
                    if (shipGroup2.LeadShip != null)
                    {
                        habitat3 = _Galaxy.FastFindNearestColony(shipGroup2.LeadShip.Xpos, shipGroup2.LeadShip.Ypos, this, strategicValueThreshhold);
                        if (habitat3 == null)
                        {
                            habitat3 = Capital;
                        }
                        if (habitat3 != null)
                        {
                            distanceFromAttackingEmpire = _Galaxy.CalculateDistance(habitat3.Xpos, habitat3.Ypos, shipGroup2.LeadShip.Xpos, shipGroup2.LeadShip.Ypos);
                            num7 = ((shipGroup2.LeadShip.NearestSystemStar == null) ? shipGroup2.TotalOverallStrengthFactor : ((!CheckSystemVisible(shipGroup2.LeadShip.NearestSystemStar.SystemIndex)) ? shipGroup2.TotalOverallStrengthFactor : _Galaxy.DetermineDefendingStrength(shipGroup2, empire)));
                        }
                        else
                        {
                            num7 = shipGroup2.TotalOverallStrengthFactor;
                        }
                    }
                }
                num7 = (prioritizedTarget9.LocationStrength = Math.Max(50, num7));
                prioritizedTarget9.DistanceFromAttackingEmpire = distanceFromAttackingEmpire;
            }
            prioritizedTargetList.Sort();
            prioritizedTargetList.Reverse();
            return prioritizedTargetList;
        }

        public HabitatList DetermineEmpireSystemsWithOtherMilitaryForcesPresent(Empire otherEmpire)
        {
            HabitatList habitatList = new HabitatList();
            HabitatList habitatList2 = DetermineEmpireDominatedSystems(this, includeAllTerritory: true);
            foreach (Habitat item in habitatList2)
            {
                if (SystemVisibility[item.SystemIndex].Threats == null || SystemVisibility[item.SystemIndex].ThreatLevels == null || SystemVisibility[item.SystemIndex].Threats.Count <= 0)
                {
                    continue;
                }
                BuiltObjectList threats = SystemVisibility[item.SystemIndex].Threats;
                for (int i = 0; i < threats.Count; i++)
                {
                    BuiltObject builtObject = threats[i];
                    if (builtObject.Role == BuiltObjectRole.Military && builtObject.FirepowerRaw > 0 && builtObject.Empire == otherEmpire && !habitatList.Contains(item))
                    {
                        habitatList.Add(item);
                    }
                }
            }
            return habitatList;
        }

        public HabitatPrioritizationList IdentifyThreatenedSystemsPrioritized(double x, double y, bool includePirateBaseSystems, bool excludeSystemsWithFleetsPresentOrEnRoute, bool excludeSystemsOfOtherEmpires)
        {
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            HabitatList habitatList = new HabitatList();
            if (!includePirateBaseSystems)
            {
                for (int i = 0; i < _KnownPirateBases.Count; i++)
                {
                    BuiltObject builtObject = _KnownPirateBases[i];
                    if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.NearestSystemStar != null && !habitatList.Contains(builtObject.NearestSystemStar))
                    {
                        habitatList.Add(builtObject.NearestSystemStar);
                    }
                }
            }
            HabitatList habitatList2 = new HabitatList();
            if (excludeSystemsWithFleetsPresentOrEnRoute)
            {
                for (int j = 0; j < ShipGroups.Count; j++)
                {
                    ShipGroup shipGroup = ShipGroups[j];
                    if (shipGroup != null)
                    {
                        Habitat habitat = shipGroup.IdentifyFleetSystem();
                        if (habitat != null && !habitatList2.Contains(habitat))
                        {
                            habitatList2.Add(habitat);
                        }
                    }
                }
            }
            for (int k = 0; k < SystemVisibility.Count; k++)
            {
                SystemVisibility systemVisibility = SystemVisibility[k];
                if (systemVisibility == null || systemVisibility.Status == SystemVisibilityStatus.Unexplored || systemVisibility.SystemStar == null || (!includePirateBaseSystems && habitatList.Contains(systemVisibility.SystemStar)))
                {
                    continue;
                }
                int num = _Galaxy.CheckSystemOwnershipId(systemVisibility.SystemStar);
                if (excludeSystemsOfOtherEmpires && num >= 0 && num != EmpireId)
                {
                    continue;
                }
                int num2 = 0;
                if (systemVisibility.Threats != null && systemVisibility.Threats.Count > 0)
                {
                    for (int l = 0; l < systemVisibility.Threats.Count; l++)
                    {
                        BuiltObject builtObject2 = systemVisibility.Threats[l];
                        if (builtObject2 == null || builtObject2.HasBeenDestroyed || builtObject2.Role != BuiltObjectRole.Military)
                        {
                            continue;
                        }
                        Empire actualEmpire = builtObject2.ActualEmpire;
                        if (actualEmpire == null || actualEmpire == _Galaxy.IndependentEmpire)
                        {
                            continue;
                        }
                        if (PirateEmpireBaseHabitat != null || actualEmpire.PirateEmpireBaseHabitat != null)
                        {
                            PirateRelation pirateRelation = ObtainPirateRelation(actualEmpire);
                            if (pirateRelation.Type != PirateRelationType.Protection)
                            {
                                num2 += builtObject2.FirepowerRaw;
                            }
                        }
                        else
                        {
                            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(actualEmpire);
                            if (diplomaticRelation.Type == DiplomaticRelationType.War)
                            {
                                num2 += builtObject2.FirepowerRaw;
                            }
                        }
                    }
                }
                if (systemVisibility.Status == SystemVisibilityStatus.Visible)
                {
                    for (int m = 0; m < _Galaxy.Systems[systemVisibility.SystemStar.SystemIndex].Creatures.Count; m++)
                    {
                        Creature creature = _Galaxy.Systems[systemVisibility.SystemStar.SystemIndex].Creatures[m];
                        if (creature.IsVisible && creature.AttackStrength > 0)
                        {
                            num2 += creature.AttackStrength;
                        }
                    }
                }
                if (num2 > 0 && (!excludeSystemsWithFleetsPresentOrEnRoute || !habitatList2.Contains(systemVisibility.SystemStar)))
                {
                    double num3 = _Galaxy.CalculateDistance(x, y, systemVisibility.SystemStar.Xpos, systemVisibility.SystemStar.Ypos);
                    int priority = (int)((double)num2 * 1000.0 / (num3 / 10000.0));
                    HabitatPrioritization item = new HabitatPrioritization(systemVisibility.SystemStar, priority);
                    habitatPrioritizationList.Add(item);
                }
            }
            habitatPrioritizationList.Sort();
            habitatPrioritizationList.Reverse();
            return habitatPrioritizationList;
        }

        public bool CheckWhetherHabitatIsDangerous(Habitat habitat)
        {
            if (habitat != null)
            {
                Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                if (habitat2 != null && SystemVisibility[habitat2.SystemIndex].Threats != null && SystemVisibility[habitat2.SystemIndex].Threats.Count > 0)
                {
                    for (int i = 0; i < SystemVisibility[habitat2.SystemIndex].Threats.Count; i++)
                    {
                        BuiltObject builtObject = SystemVisibility[habitat2.SystemIndex].Threats[i];
                        if (builtObject == null || builtObject.Empire == null || builtObject.Empire.PirateEmpireBaseHabitat == null || builtObject.Role != BuiltObjectRole.Military)
                        {
                            continue;
                        }
                        PirateRelation pirateRelation = ObtainPirateRelation(builtObject.Empire);
                        if (pirateRelation.Type == PirateRelationType.Protection)
                        {
                            continue;
                        }
                        if (builtObject.Role == BuiltObjectRole.Base)
                        {
                            double num = _Galaxy.CalculateDistanceSquared(habitat.Xpos, habitat.Ypos, builtObject.Xpos, builtObject.Ypos);
                            if (num < 1000000.0)
                            {
                                return true;
                            }
                            continue;
                        }
                        if (builtObject.WarpSpeed > 0)
                        {
                            return true;
                        }
                        if (builtObject.TopSpeed > 0)
                        {
                            double num2 = _Galaxy.CalculateDistanceSquared(habitat.Xpos, habitat.Ypos, builtObject.Xpos, builtObject.Ypos);
                            if (num2 < 4000000.0)
                            {
                                return true;
                            }
                        }
                    }
                }
                if (habitat2 != null && CheckSystemVisible(habitat2.SystemIndex))
                {
                    for (int j = 0; j < _Galaxy.Systems[habitat2.SystemIndex].Creatures.Count; j++)
                    {
                        Creature creature = _Galaxy.Systems[habitat2.SystemIndex].Creatures[j];
                        if (creature.IsVisible && creature.AttackStrength > 0)
                        {
                            double num3 = _Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, creature.Xpos, creature.Ypos);
                            if (num3 < (double)(creature.AttackRange * 2))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public Habitat DetermineTopThreatenedSystem(Empire empire)
        {
            HabitatList systemStars = DetermineEmpireDominatedSystems(this, includeAllTerritory: true);
            return DetermineTopThreatenedSystem(systemStars, empire);
        }

        public Habitat DetermineTopThreatenedSystem(HabitatList systemStars, Empire empire)
        {
            Habitat result = null;
            int num = 0;
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
            if (!diplomaticRelation.MilitaryRefuelingToOther)
            {
                foreach (Habitat systemStar in systemStars)
                {
                    int num2 = 0;
                    if (DefendHabitat != null && !DefendHabitat.HasBeenDestroyed && DefendHabitat.Empire != null && DefendHabitat.Empire == this)
                    {
                        _ = systemStar.SystemIndex;
                        _ = DefendHabitat.SystemIndex;
                    }
                    if (SystemVisibility[systemStar.SystemIndex].Threats != null && SystemVisibility[systemStar.SystemIndex].ThreatLevels != null && SystemVisibility[systemStar.SystemIndex].Threats.Count > 0)
                    {
                        BuiltObjectList threats = SystemVisibility[systemStar.SystemIndex].Threats;
                        for (int i = 0; i < threats.Count; i++)
                        {
                            if (threats[i].Role != BuiltObjectRole.Base && threats[i].Empire != _Galaxy.IndependentEmpire && threats[i].Empire.PirateEmpireBaseHabitat == null && threats[i].Empire == empire && threats[i].FirepowerRaw > 0 && (threats[i].Owner != null || (threats[i].Weapons != null && threats[i].Weapons.Count > 1)) && (threats[i].Mission == null || threats[i].Mission.Type != BuiltObjectMissionType.Blockade) && diplomaticRelation.Type != DiplomaticRelationType.War && !diplomaticRelation.MilitaryRefuelingToOther && threats[i].SubRole != BuiltObjectSubRole.ResortBase)
                            {
                                num2 += threats[i].FirepowerRaw;
                            }
                        }
                    }
                    if (num2 > num)
                    {
                        result = systemStar;
                        num = num2;
                    }
                }
                return result;
            }
            return result;
        }

        private void PirateReviewSystemThreats()
        {
            if (SystemVisibility == null || BuiltObjects == null || DominantRace == null)
            {
                return;
            }
            for (int i = 0; i < SystemVisibility.Count; i++)
            {
                SystemVisibility systemVisibility = SystemVisibility[i];
                if (systemVisibility != null)
                {
                    systemVisibility.EmpireStrength = 0;
                }
            }
            for (int j = 0; j < BuiltObjects.Count; j++)
            {
                BuiltObject builtObject = BuiltObjects[j];
                if (builtObject != null && builtObject.FirepowerRaw > 0 && builtObject.NearestSystemStar != null && builtObject.NearestSystemStar.SystemIndex >= 0 && builtObject.NearestSystemStar.SystemIndex < SystemVisibility.Count)
                {
                    SystemVisibility[builtObject.NearestSystemStar.SystemIndex].EmpireStrength += builtObject.FirepowerRaw;
                }
            }
        }

        private void ReviewSystemThreats()
        {
            if (SystemVisibility == null || BuiltObjects == null || DominantRace == null)
            {
                return;
            }
            for (int i = 0; i < SystemVisibility.Count; i++)
            {
                SystemVisibility systemVisibility = SystemVisibility[i];
                if (systemVisibility != null)
                {
                    systemVisibility.EmpireStrength = 0;
                }
            }
            for (int j = 0; j < BuiltObjects.Count; j++)
            {
                BuiltObject builtObject = BuiltObjects[j];
                if (builtObject != null && builtObject.FirepowerRaw > 0 && builtObject.NearestSystemStar != null && builtObject.NearestSystemStar.SystemIndex >= 0 && builtObject.NearestSystemStar.SystemIndex < SystemVisibility.Count)
                {
                    SystemVisibility[builtObject.NearestSystemStar.SystemIndex].EmpireStrength += builtObject.FirepowerRaw;
                }
            }
            if (EmpireEvaluations != null)
            {
                for (int k = 0; k < EmpireEvaluations.Count; k++)
                {
                    EmpireEvaluation empireEvaluation = EmpireEvaluations[k];
                    empireEvaluation.MilitaryForcesInSystems = 0;
                }
            }
            long currentStarDate = _Galaxy.CurrentStarDate;
            int refusalCount = 0;
            double num = Galaxy.SectorSize * Galaxy.SectorSize;
            int num2 = 30;
            double val = 3.33 * (1.5 - (double)Math.Max(DominantRace.AggressionLevel, DominantRace.CautionLevel) / 100.0);
            val = Math.Min(1.0, Math.Max(0.0, val));
            int num3 = (int)Math.Max(0.0, (double)num2 * val);
            int militaryPotency = MilitaryPotency;
            double num4 = 1.0 + Math.Max(0.0, (double)(DominantRace.AggressionLevel - DominantRace.CautionLevel) / 100.0);
            HabitatList habitatList = DetermineEmpireDominatedSystems(this, includeAllTerritory: true);
            BuiltObjectList builtObjectList = _Galaxy.ObtainAvailableMilitaryShips(this, 1, includeUnAutomatedShips: false, allowShipsInFleets: false, includeBusyShips: false);
            foreach (Habitat item in habitatList)
            {
                bool flag = false;
                if (DefendHabitat != null && !DefendHabitat.HasBeenDestroyed && DefendHabitat.Empire != null && DefendHabitat.Empire == this && item.SystemIndex == DefendHabitat.SystemIndex)
                {
                    flag = true;
                }
                SystemVisibility systemVisibility2 = null;
                if (item.SystemIndex >= 0 && item.SystemIndex < SystemVisibility.Count)
                {
                    systemVisibility2 = SystemVisibility[item.SystemIndex];
                }
                if (systemVisibility2 == null || systemVisibility2.Threats == null || systemVisibility2.ThreatLevels == null || systemVisibility2.Threats.Count <= 0)
                {
                    continue;
                }
                BuiltObjectList threats = systemVisibility2.Threats;
                int[] array = new int[_Galaxy.NextEmpireID + 1];
                int num5 = -1;
                BuiltObject builtObject2 = null;
                for (int l = 0; l < threats.Count; l++)
                {
                    BuiltObject builtObject3 = threats[l];
                    if (builtObject3 == null || builtObject3.Role != BuiltObjectRole.Military || builtObject3.Empire == null || builtObject3.Empire == _Galaxy.IndependentEmpire || builtObject3.Empire.PirateEmpireBaseHabitat != null || (builtObject3.FirepowerRaw <= 0 && builtObject3.FighterCapacity <= 0) || (builtObject3.Owner == null && (builtObject3.Weapons == null || builtObject3.Weapons.Count <= 1)) || builtObject3.HasBeenDestroyed || builtObject3.NearestSystemStar != item || (builtObject3.Mission != null && builtObject3.Mission.Type == BuiltObjectMissionType.Blockade))
                    {
                        continue;
                    }
                    DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(threats[l].Empire);
                    if (diplomaticRelation == null || diplomaticRelation.Type == DiplomaticRelationType.War || diplomaticRelation.MilitaryRefuelingToOther || builtObject3.SubRole == BuiltObjectSubRole.ResortBase)
                    {
                        continue;
                    }
                    int num6 = Math.Max(builtObject3.FirepowerRaw, builtObject3.FighterCapacity * 10);
                    array[builtObject3.Empire.EmpireId] += num6;
                    if (num5 < 0)
                    {
                        num5 = builtObject3.Empire.EmpireId;
                        builtObject2 = builtObject3;
                    }
                    else
                    {
                        if (array[builtObject3.Empire.EmpireId] < array[num5])
                        {
                            continue;
                        }
                        if (num5 != builtObject3.Empire.EmpireId)
                        {
                            builtObject2 = builtObject3;
                        }
                        else if (builtObject2 == null)
                        {
                            builtObject2 = builtObject3;
                        }
                        else
                        {
                            int num7 = Math.Max(builtObject2.FirepowerRaw, builtObject2.FighterCapacity * 10);
                            if (num6 > num7)
                            {
                                builtObject2 = builtObject3;
                            }
                        }
                        num5 = builtObject3.Empire.EmpireId;
                    }
                }
                if (num5 < 0 || (array[num5] < num3 && !flag))
                {
                    continue;
                }
                Empire byEmpireId = _Galaxy.Empires.GetByEmpireId(num5);
                EmpireEvaluation empireEvaluation2 = ObtainEmpireEvaluation(byEmpireId);
                int num8 = Math.Max(1, Math.Min(20, (array[num5] - num3) / 5));
                empireEvaluation2.MilitaryForcesInSystems -= num8;
                long num9 = currentStarDate - empireEvaluation2.LastSystemWarningDate;
                if (num9 < 90000 && byEmpireId != null)
                {
                    int militaryPotency2 = byEmpireId.MilitaryPotency;
                    double num10 = (double)militaryPotency / (double)militaryPotency2;
                    num10 *= num4;
                    if (num10 >= 0.85)
                    {
                        if (builtObject2 == null)
                        {
                            continue;
                        }
                        if (builtObject2.ShipGroup != null && builtObject2.ShipGroup.LeadShip != null)
                        {
                            double num11 = _Galaxy.CalculateDistanceSquared(builtObject2.Xpos, builtObject2.Ypos, builtObject2.ShipGroup.LeadShip.Xpos, builtObject2.ShipGroup.LeadShip.Ypos);
                            if (num11 < 9000000.0)
                            {
                                int overallStrength = (int)((double)builtObject2.ShipGroup.TotalOverallStrengthFactor * 0.75);
                                ShipGroup shipGroup = FindNearestAvailableFleet(builtObject2.ShipGroup.LeadShip.Xpos, builtObject2.ShipGroup.LeadShip.Ypos, BuiltObjectMissionPriority.Low, overallStrength, FleetPosture.Attack, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: false);
                                ShipGroup shipGroup2 = FindNearestAvailableFleet(builtObject2.ShipGroup.LeadShip.Xpos, builtObject2.ShipGroup.LeadShip.Ypos, BuiltObjectMissionPriority.Low, overallStrength, FleetPosture.Defend, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: false, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: true);
                                ShipGroup shipGroup3 = null;
                                double num12 = double.MaxValue;
                                double num13 = double.MaxValue;
                                if (shipGroup != null && shipGroup.LeadShip != null)
                                {
                                    num12 = _Galaxy.CalculateDistanceSquared(builtObject2.ShipGroup.LeadShip.Xpos, builtObject2.ShipGroup.LeadShip.Ypos, shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos);
                                }
                                if (shipGroup2 != null && shipGroup2.LeadShip != null)
                                {
                                    num13 = _Galaxy.CalculateDistanceSquared(builtObject2.ShipGroup.LeadShip.Xpos, builtObject2.ShipGroup.LeadShip.Ypos, shipGroup2.LeadShip.Xpos, shipGroup2.LeadShip.Ypos);
                                }
                                if (num13 < num12 && shipGroup2 != null)
                                {
                                    shipGroup3 = shipGroup2;
                                }
                                else if (num12 < num13 && shipGroup != null)
                                {
                                    shipGroup3 = shipGroup;
                                }
                                if (shipGroup3 != null && shipGroup3.LeadShip != null && (shipGroup3.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageAttackForcesInOurSystem(item, byEmpireId, builtObject2.ShipGroup, shipGroup3), builtObject2.ShipGroup, AdvisorMessageType.DefendTerritory, builtObject2.Empire, shipGroup3, null))
                                {
                                    shipGroup3.AssignMission(BuiltObjectMissionType.Attack, builtObject2.ShipGroup, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                }
                            }
                        }
                        else
                        {
                            int index = -1;
                            BuiltObject nearestBuiltObjectWithinRange = builtObjectList.GetNearestBuiltObjectWithinRange(builtObject2.Xpos, builtObject2.Ypos, 0.2, out index);
                            if (nearestBuiltObjectWithinRange != null && nearestBuiltObjectWithinRange.WithinFuelRangeAndRefuel(builtObject2.Xpos, builtObject2.Ypos, 0.1) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageAttackForcesInOurSystem(item, byEmpireId), builtObject2, AdvisorMessageType.DefendTerritory, builtObject2.Empire, nearestBuiltObjectWithinRange, null))
                            {
                                BuiltObjectMissionType builtObjectMissionType = DetermineDestroyOrCaptureTarget(nearestBuiltObjectWithinRange, builtObject2, attackingAsGroup: true);
                                nearestBuiltObjectWithinRange.RecordRevertMission(builtObjectMissionType, evenWhenAutomated: true);
                                nearestBuiltObjectWithinRange.AssignMission(builtObjectMissionType, builtObject2, null, BuiltObjectMissionPriority.High);
                                builtObjectList.RemoveAt(index);
                            }
                        }
                        continue;
                    }
                    int num14 = 0;
                    if (systemVisibility2.EmpireStrength >= array[num5])
                    {
                        continue;
                    }
                    int num15 = array[num5] - systemVisibility2.EmpireStrength;
                    int iterationCount = 0;
                    while (Galaxy.ConditionCheckLimit(num15 > 0 && builtObject2 != null && num14 < 3, 100, ref iterationCount))
                    {
                        int index2 = -1;
                        BuiltObject nearestBuiltObjectWithinRange2 = builtObjectList.GetNearestBuiltObjectWithinRange(builtObject2.Xpos, builtObject2.Ypos, 0.2, out index2);
                        if (nearestBuiltObjectWithinRange2 == null || !nearestBuiltObjectWithinRange2.WithinFuelRangeAndRefuel(builtObject2.Xpos, builtObject2.Ypos, 0.1))
                        {
                            break;
                        }
                        double num16 = _Galaxy.CalculateDistanceSquared(nearestBuiltObjectWithinRange2.Xpos, nearestBuiltObjectWithinRange2.Ypos, builtObject2.Xpos, builtObject2.Ypos);
                        if (!(num16 < num))
                        {
                            break;
                        }
                        object obj = null;
                        _Galaxy.SelectRelativeParkingPoint(out var x, out var y);
                        if (builtObject2.ParentHabitat != null)
                        {
                            obj = builtObject2.ParentHabitat;
                        }
                        else
                        {
                            obj = null;
                            x += builtObject2.Xpos;
                            y += builtObject2.Ypos;
                        }
                        nearestBuiltObjectWithinRange2.AssignMission(BuiltObjectMissionType.Move, obj, null, x, y, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        num15 -= nearestBuiltObjectWithinRange2.FirepowerRaw;
                        num14++;
                        builtObjectList.RemoveAt(index2);
                    }
                    continue;
                }
                if (num9 > 240000 && _ControlDiplomacyTreaties != 0)
                {
                    string description = string.Format(TextResolver.GetText("Your forces intrude in our territory"), item.Name);
                    SendMessageToEmpire(byEmpireId, EmpireMessageType.RemoveForcesFromSystem, item, description);
                    empireEvaluation2.LastSystemWarningDate = currentStarDate;
                    empireEvaluation2.LastSystemWarningIndex = item.SystemIndex;
                }
                int num17 = 0;
                if (systemVisibility2.EmpireStrength >= array[num5])
                {
                    continue;
                }
                int num18 = array[num5] - systemVisibility2.EmpireStrength;
                int iterationCount2 = 0;
                while (Galaxy.ConditionCheckLimit(num18 > 0 && builtObject2 != null && num17 < 3, 100, ref iterationCount2))
                {
                    int index3 = -1;
                    BuiltObject nearestBuiltObjectWithinRange3 = builtObjectList.GetNearestBuiltObjectWithinRange(builtObject2.Xpos, builtObject2.Ypos, 0.2, out index3);
                    if (nearestBuiltObjectWithinRange3 == null || !nearestBuiltObjectWithinRange3.WithinFuelRangeAndRefuel(builtObject2.Xpos, builtObject2.Ypos, 0.1))
                    {
                        break;
                    }
                    double num19 = _Galaxy.CalculateDistanceSquared(nearestBuiltObjectWithinRange3.Xpos, nearestBuiltObjectWithinRange3.Ypos, builtObject2.Xpos, builtObject2.Ypos);
                    if (!(num19 < num))
                    {
                        break;
                    }
                    object obj2 = null;
                    _Galaxy.SelectRelativeParkingPoint(out var x2, out var y2);
                    if (builtObject2.ParentHabitat != null)
                    {
                        obj2 = builtObject2.ParentHabitat;
                    }
                    else
                    {
                        obj2 = null;
                        x2 += builtObject2.Xpos;
                        y2 += builtObject2.Ypos;
                    }
                    nearestBuiltObjectWithinRange3.AssignMission(BuiltObjectMissionType.Move, obj2, null, x2, y2, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                    num18 -= nearestBuiltObjectWithinRange3.FirepowerRaw;
                    num17++;
                    builtObjectList.RemoveAt(index3);
                }
            }
        }

        public int EvaluateMilitaryPotency(int ourWeightedMilitaryPotency, int theirWeightedMilitaryPotency, Empire otherEmpire)
        {
            int num = 0;
            double num2 = (double)ourWeightedMilitaryPotency * 2.0;
            double num3 = (double)ourWeightedMilitaryPotency * 0.5;
            if (otherEmpire == _Galaxy.PlayerEmpire)
            {
                num2 *= _Galaxy.PlayerEmpire.DifficultyLevel;
                num3 *= _Galaxy.PlayerEmpire.DifficultyLevel;
            }
            if ((double)theirWeightedMilitaryPotency > num2)
            {
                return -1;
            }
            if ((double)theirWeightedMilitaryPotency > num3 && (double)theirWeightedMilitaryPotency < num2)
            {
                return 0;
            }
            if (theirWeightedMilitaryPotency == ourWeightedMilitaryPotency)
            {
                return 0;
            }
            return 1;
        }

        private void SendScoutShipsToEnemyLocations(EmpireList enemyEmpires)
        {
            if (enemyEmpires.Count <= 0)
            {
                return;
            }
            int totalExplorationShips = 0;
            int val = CountAvailableScoutShips(out totalExplorationShips);
            val = Math.Min(val, totalExplorationShips / 2);
            int num = val / enemyEmpires.Count;
            if (num == 0 && val > 0)
            {
                num = 1;
            }
            if (enemyEmpires.Count <= 0)
            {
                return;
            }
            int num2 = Galaxy.Rnd.Next(0, enemyEmpires.Count);
            for (int i = num2; i < enemyEmpires.Count; i++)
            {
                if (val > 0)
                {
                    val -= SendScoutsToSingleEnemyEmpire(enemyEmpires[i], num);
                }
            }
            for (int j = 0; j < num2; j++)
            {
                if (val > 0)
                {
                    val -= SendScoutsToSingleEnemyEmpire(enemyEmpires[j], num);
                }
            }
        }

        private int SendScoutsToSingleEnemyEmpire(Empire enemyEmpire, int availableScouts)
        {
            int num = 0;
            HabitatList habitatList = enemyEmpire.DetermineEmpireSystems(enemyEmpire);
            SystemInfoDistanceList systemInfoDistanceList = new SystemInfoDistanceList();
            foreach (Habitat item in habitatList)
            {
                SystemInfo systemInfo = _Galaxy.Systems[item.SystemIndex];
                SystemInfoDistance systemInfoDistance = new SystemInfoDistance();
                systemInfoDistance.SystemInfo = systemInfo;
                double distance = 0.0;
                if (systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire != null && systemInfo.DominantEmpire.Empire == enemyEmpire)
                {
                    distance = systemInfo.DominantEmpire.TotalStrategicValue;
                }
                else if (systemInfo.OtherEmpires != null && systemInfo.OtherEmpires.Count > 0)
                {
                    for (int i = 0; i < systemInfo.OtherEmpires.Count; i++)
                    {
                        if (systemInfo.OtherEmpires[i].Empire == enemyEmpire)
                        {
                            distance = systemInfo.OtherEmpires[i].TotalStrategicValue;
                            break;
                        }
                    }
                }
                systemInfoDistance.Distance = distance;
                systemInfoDistanceList.Add(systemInfoDistance);
            }
            systemInfoDistanceList.Sort();
            systemInfoDistanceList.Reverse();
            for (int j = 0; j < systemInfoDistanceList.Count; j++)
            {
                if (availableScouts <= 0)
                {
                    break;
                }
                if (CheckSystemVisibilityStatus(systemInfoDistanceList[j].SystemInfo.SystemStar.SystemIndex) != SystemVisibilityStatus.Explored)
                {
                    continue;
                }
                BuiltObject builtObject = FindAvailableExplorationShip();
                if (builtObject == null)
                {
                    continue;
                }
                Habitat habitat = null;
                if (systemInfoDistanceList[j].SystemInfo.Habitats == null || systemInfoDistanceList[j].SystemInfo.Habitats.Count <= 0)
                {
                    continue;
                }
                int num2 = 0;
                int num3 = systemInfoDistanceList[j].SystemInfo.Habitats.Count - 1;
                while (habitat == null && num2 < 30 && num3 >= 0)
                {
                    habitat = systemInfoDistanceList[j].SystemInfo.Habitats[num3];
                    bool flag = true;
                    if (habitat != null)
                    {
                        BuiltObject builtObject2 = _Galaxy.FindNearestBuiltObject((int)habitat.Xpos, (int)habitat.Ypos, enemyEmpire);
                        if (builtObject2 != null)
                        {
                            double num4 = _Galaxy.CalculateDistance(builtObject2.Xpos, builtObject2.Ypos, habitat.Xpos, habitat.Ypos);
                            if (num4 < 2500.0)
                            {
                                flag = false;
                            }
                        }
                        if (!flag)
                        {
                            habitat = null;
                        }
                    }
                    num3--;
                    num2++;
                }
                if (habitat != null && builtObject.WithinFuelRangeAndRefuel(habitat.Xpos, habitat.Ypos, 0.1))
                {
                    long starDate = _Galaxy.CurrentStarDate + 180000;
                    builtObject.AssignMission(BuiltObjectMissionType.MoveAndWait, habitat, null, -2000000001.0, -2000000001.0, starDate, BuiltObjectMissionPriority.High, allowReprocessing: true);
                    availableScouts--;
                    num++;
                }
            }
            return num;
        }

        private int CountExplorationShips()
        {
            int num = 0;
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                if (BuiltObjects[i].SubRole == BuiltObjectSubRole.ExplorationShip && BuiltObjects[i].BuiltAt == null)
                {
                    num++;
                }
            }
            return num;
        }

        private int CountExploredSystems()
        {
            int num = 0;
            for (int i = 0; i < SystemVisibility.Count; i++)
            {
                if (SystemVisibility[i].SystemStar != null && SystemVisibility[i].SystemStar.Category == HabitatCategoryType.Star && CheckSystemExplored(SystemVisibility[i].SystemStar))
                {
                    num++;
                }
            }
            return num;
        }

        private int CountAvailableScoutShips(out int totalExplorationShips)
        {
            int num = 0;
            totalExplorationShips = 0;
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                if (BuiltObjects[i].SubRole == BuiltObjectSubRole.ExplorationShip)
                {
                    totalExplorationShips++;
                    BuiltObject builtObject = BuiltObjects[i];
                    if (builtObject.BuiltAt == null && builtObject.IsAutoControlled && (builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined || builtObject.Mission.Priority == BuiltObjectMissionPriority.Undefined || builtObject.Mission.Priority == BuiltObjectMissionPriority.Low || builtObject.Mission.Priority == BuiltObjectMissionPriority.Normal))
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        private BuiltObject FindAvailableExplorationShip()
        {
            BuiltObject result = null;
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                if (BuiltObjects[i].SubRole == BuiltObjectSubRole.ExplorationShip)
                {
                    BuiltObject builtObject = BuiltObjects[i];
                    if (builtObject.BuiltAt == null && builtObject.IsAutoControlled && (builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined || builtObject.Mission.Priority == BuiltObjectMissionPriority.Undefined || builtObject.Mission.Priority == BuiltObjectMissionPriority.Low || builtObject.Mission.Priority == BuiltObjectMissionPriority.Normal))
                    {
                        result = builtObject;
                        break;
                    }
                }
            }
            return result;
        }

        public BuiltObject FindNearestAvailableConstructionShip(double x, double y)
        {
            BuiltObject result = null;
            double num = double.MaxValue;
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = BuiltObjects[i];
                if (builtObject != null && builtObject.SubRole == BuiltObjectSubRole.ConstructionShip && builtObject.BuiltAt == null && (builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined))
                {
                    double num2 = _Galaxy.CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                    if (num2 < num)
                    {
                        result = builtObject;
                        num = num2;
                    }
                }
            }
            return result;
        }

        public void ResolveSystemVisibility(BuiltObject builtObject, bool excludeBuiltObject)
        {
            SystemVisibilityStatus systemVisibilityStatus = SystemVisibilityStatus.Unexplored;
            Habitat nearestSystemStar = builtObject.NearestSystemStar;
            if (nearestSystemStar == null || SystemVisibility == null || this == _Galaxy.IndependentEmpire)
            {
                return;
            }
            SystemVisibilityStatus systemVisibilityStatus2 = SystemVisibilityStatus.Unexplored;
            int systemIndex = nearestSystemStar.SystemIndex;
            if (systemIndex >= 0 && systemIndex < SystemVisibility.Count)
            {
                systemVisibilityStatus2 = SystemVisibility[systemIndex].Status;
            }
            systemVisibilityStatus = ((!excludeBuiltObject) ? CheckSystemVisible(nearestSystemStar, this, null, null) : CheckSystemVisible(nearestSystemStar, this, builtObject, null));
            if ((systemVisibilityStatus2 == SystemVisibilityStatus.Explored || systemVisibilityStatus2 == SystemVisibilityStatus.Visible) && systemVisibilityStatus == SystemVisibilityStatus.Unexplored)
            {
                systemVisibilityStatus = SystemVisibilityStatus.Explored;
            }
            SetSystemVisibility(nearestSystemStar, systemVisibilityStatus);
            if (systemVisibilityStatus == SystemVisibilityStatus.Visible)
            {
                if (!_SystemsVisible.Contains(nearestSystemStar))
                {
                    _SystemsVisible.Add(nearestSystemStar);
                }
                return;
            }
            int num = _SystemsVisible.IndexOf(nearestSystemStar);
            if (num >= 0 && _SystemsVisible.Count > num)
            {
                _SystemsVisible.RemoveAt(num);
            }
        }

        public void ResolveSystemVisibility(double x, double y, BuiltObject excludeBuiltObject, Habitat excludeHabitat)
        {
            SystemVisibilityStatus systemVisibilityStatus = SystemVisibilityStatus.Unexplored;
            Habitat habitat = _Galaxy.FastFindNearestSystem(x, y);
            if (habitat == null)
            {
                return;
            }
            double num = _Galaxy.CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            if (!(num <= (double)(Galaxy.MaxSolarSystemSize + 500)))
            {
                return;
            }
            SystemVisibilityStatus status = SystemVisibility[habitat.SystemIndex].Status;
            systemVisibilityStatus = CheckSystemVisible(habitat, this, excludeBuiltObject, excludeHabitat);
            if ((status == SystemVisibilityStatus.Explored || status == SystemVisibilityStatus.Visible) && systemVisibilityStatus == SystemVisibilityStatus.Unexplored)
            {
                systemVisibilityStatus = SystemVisibilityStatus.Explored;
            }
            SetSystemVisibility(habitat, systemVisibilityStatus);
            if (systemVisibilityStatus == SystemVisibilityStatus.Visible)
            {
                if (!_SystemsVisible.Contains(habitat))
                {
                    _SystemsVisible.Add(habitat);
                }
                return;
            }
            int num2 = _SystemsVisible.IndexOf(habitat);
            if (num2 >= 0 && _SystemsVisible.Count > num2)
            {
                _SystemsVisible.RemoveAt(num2);
            }
        }

        public void SetSystemVisibility(Habitat systemStar, SystemVisibilityStatus status)
        {
            if (systemStar.SystemIndex >= 0 && systemStar.SystemIndex < SystemVisibility.Count)
            {
                SystemVisibility[systemStar.SystemIndex].Status = status;
            }
        }

        public SystemVisibilityStatus CheckSystemVisible(Habitat systemStar, Empire ourEmpire, BuiltObject excludeBuiltObject, Habitat excludeHabitat)
        {
            bool flag = false;
            if (ourEmpire != null && ourEmpire.PirateEmpireBaseHabitat != null)
            {
                flag = true;
            }
            SystemInfo systemInfo = _Galaxy.Systems[systemStar.SystemIndex];
            for (int i = 0; i < systemInfo.Habitats.Count; i++)
            {
                if ((systemInfo.Habitats[i].Owner == this || (flag && systemInfo.Habitats[i].GetPirateControl().GetByFaction(ourEmpire) != null)) && systemInfo.Habitats[i] != excludeHabitat)
                {
                    return SystemVisibilityStatus.Visible;
                }
            }
            for (int j = 0; j < BuiltObjects.Count; j++)
            {
                BuiltObject builtObject = BuiltObjects[j];
                if (builtObject != null && builtObject.NearestSystemStar == systemStar && builtObject != excludeBuiltObject)
                {
                    return SystemVisibilityStatus.Visible;
                }
            }
            for (int k = 0; k < PrivateBuiltObjects.Count; k++)
            {
                BuiltObject builtObject2 = PrivateBuiltObjects[k];
                if (builtObject2 != null && builtObject2.NearestSystemStar == systemStar && builtObject2 != excludeBuiltObject)
                {
                    return SystemVisibilityStatus.Visible;
                }
            }
            if (SystemVisibility[systemStar.SystemIndex].Status == SystemVisibilityStatus.Visible || SystemVisibility[systemStar.SystemIndex].Status == SystemVisibilityStatus.Explored)
            {
                return SystemVisibilityStatus.Explored;
            }
            return SystemVisibilityStatus.Unexplored;
        }

        private void ProjectPrivateForceStructure()
        {
            _PrivateForceStructureProjections = new ForceStructureProjectionList();
            long currentStarDate = _Galaxy.CurrentStarDate;
            HabitatList habitatList = DetermineLargestColonyInEachSystem();
            OrderList orders = _Galaxy.Orders.GetOrders(this);
            int num = 1 + (int)((double)orders.Count * 0.45 * 0.85);
            int num2 = 1 + (int)((double)orders.Count * 0.35 * 0.85);
            int num3 = 1 + (int)((double)orders.Count * 0.2 * 0.85);
            double num4 = Math.Max(Colonies.Count, (double)MiningStations.Count / 7.0);
            int num5 = (int)(num4 * 10.0);
            int num6 = (int)(num4 * 8.0);
            int num7 = (int)(num4 * 5.0);
            if (Colonies.Count == 1)
            {
                num5 = (int)((double)num5 * 1.5);
                num6 = (int)((double)num6 * 1.5);
                num7 = (int)((double)num7 * 1.5);
                if (!CheckEmpireHasHyperDriveTech(this))
                {
                    num *= 2;
                    num2 *= 2;
                    num3 *= 2;
                }
            }
            else if (Colonies.Count < 4)
            {
                num5 = (int)(num4 * 14.0);
                num6 = (int)(num4 * 11.0);
                num7 = (int)(num4 * 7.0);
            }
            int num8 = 0;
            if (MiningStations != null)
            {
                num8 = MiningStations.Count;
                if (num8 < 5)
                {
                    double num9 = 5 - num8;
                    num5 = (int)((double)num5 / num9);
                    num6 = (int)((double)num6 / num9);
                    num7 = (int)((double)num7 / num9);
                }
            }
            if (DominantRace != null && DominantRace.Expanding)
            {
                num = Math.Max(num, (int)((double)Colonies.Count * 2.0));
                num2 = Math.Max(num2, (int)((double)Colonies.Count * 1.7));
                num3 = Math.Max(num3, (int)((double)Colonies.Count * 1.3));
            }
            double num10 = 3.0;
            if (Colonies.Count < 2 && (ResortBases == null || ResortBases.Count <= 0))
            {
                num10 = 0.0;
            }
            int num11 = Colonies.CountMigrationFactorBelow(0f);
            int num12 = Colonies.CountPopulationAbove(8000000000L);
            double num13 = 0.2;
            _ = (double)num11 / Math.Max(1.0, Colonies.Count);
            double num14 = 0.1;
            double num15 = (double)num12 / Math.Max(1.0, Colonies.Count);
            if (num15 < num14)
            {
                num10 /= 2.0;
            }
            if ((double)num11 < num13)
            {
                num10 /= 1.5;
            }
            int num16 = (int)((double)Colonies.Count * num10);
            Design design = Designs.FindNewestCanBuild(BuiltObjectSubRole.LargeFreighter);
            if (design == null || !CanBuildBuiltObject(new BuiltObject(design, string.Empty, _Galaxy)))
            {
                num += num3;
                num5 += num7;
                num3 = 0;
                num7 = 0;
            }
            design = Designs.FindNewestCanBuild(BuiltObjectSubRole.MediumFreighter);
            if (design == null || !CanBuildBuiltObject(new BuiltObject(design, string.Empty, _Galaxy)))
            {
                num += num2;
                num5 += num6;
                num2 = 0;
                num6 = 0;
            }
            num = Math.Min(num, num5);
            num2 = Math.Min(num2, num6);
            num3 = Math.Min(num3, num7);
            bool flag = true;
            if (Capital != null)
            {
                Habitat habitat = _Galaxy.FindNearestHabitat(Capital.Xpos, Capital.Ypos, HabitatType.Undefined, Capital);
                if (habitat != null && ResourceMap != null && !ResourceMap.CheckResourcesKnown(habitat))
                {
                    flag = false;
                }
            }
            if (num8 > 0)
            {
                flag = true;
            }
            if (flag)
            {
                if (num > 0)
                {
                    _PrivateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.SmallFreighter, num, currentStarDate));
                }
                if (num2 > 0)
                {
                    _PrivateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.MediumFreighter, num2, currentStarDate));
                }
                if (num3 > 0)
                {
                    _PrivateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.LargeFreighter, num3, currentStarDate));
                }
                if (num16 > 0)
                {
                    _PrivateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.PassengerShip, num16, currentStarDate));
                }
            }
            int num17 = Math.Max(1, (int)((double)habitatList.Count * 1.5));
            if (Colonies.Count == 1)
            {
                num17 *= 2;
                if (!CheckEmpireHasHyperDriveTech(this))
                {
                    num17 *= 2;
                }
            }
            num17 = Math.Min(num17, 40);
            if (flag)
            {
                _PrivateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.GasMiningShip, num17, currentStarDate));
                _PrivateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.MiningShip, num17, currentStarDate));
            }
            double privateAnnualRevenue = GetPrivateAnnualRevenue();
            double annualSupportCosts = 0.0;
            ForceStructureProjectionList forceStructureProjectionList = CurrentPrivateForceStructure(out annualSupportCosts);
            ForceStructureProjectionList forceStructure = _PrivateForceStructureProjections.Diff(forceStructureProjectionList);
            double num18 = EstimateForceStructureSupportCost(forceStructure);
            if (num18 < 0.0)
            {
                num18 = 0.0;
            }
            double num19 = annualSupportCosts + num18;
            double num20 = GetPrivateFunds() / num19;
            double num21 = 1.0;
            if (num20 < Galaxy.AllowableYearsMaintenanceFromCashOnHand)
            {
                privateAnnualRevenue *= 0.8;
                num21 = (privateAnnualRevenue - annualSupportCosts) / num18;
                num21 = Math.Max(0.0, num21);
            }
            if (!(num21 < 1.0))
            {
                return;
            }
            foreach (ForceStructureProjection privateForceStructureProjection in _PrivateForceStructureProjections)
            {
                int amount = privateForceStructureProjection.Amount;
                if (amount > 0)
                {
                    amount = (privateForceStructureProjection.Amount = Math.Max(1, (int)((double)amount * num21)));
                }
            }
        }

        public void CalculateStateExpenditureBalance(out double shipMaintenancePortion, out double troopMaintenancePortion, out double facilityMaintenancePortion)
        {
            double annualIncome = CalculateAccurateAnnualIncome();
            CalculateStateExpenditureBalance(annualIncome, out shipMaintenancePortion, out troopMaintenancePortion, out facilityMaintenancePortion);
        }

        public void CalculateStateExpenditureBalance(double annualIncome, out double shipMaintenancePortion, out double troopMaintenancePortion, out double facilityMaintenancePortion)
        {
            shipMaintenancePortion = 0.63;
            troopMaintenancePortion = 0.25;
            facilityMaintenancePortion = 0.12;
            shipMaintenancePortion *= Math.Sqrt(Math.Sqrt(0.5 * (1.0 + (double)Policy.ConstructionMilitary)));
            double num = 0.0;
            int num2 = 0;
            double num3 = Math.Sqrt(Policy.InvasionOverkillFactor);
            if (TroopCanRecruitInfantry)
            {
                num += Policy.TroopRecruitInfantryLevel;
                num2++;
            }
            if (TroopCanRecruitArmored)
            {
                num += Policy.TroopRecruitArmorLevel * num3;
                num2++;
            }
            if (TroopCanRecruitArtillery)
            {
                num += Policy.TroopRecruitArtilleryLevel;
                num2++;
            }
            if (TroopCanRecruitSpecialForces)
            {
                num += Policy.TroopRecruitSpecialForcesLevel * num3;
                num2++;
            }
            num /= (double)num2;
            troopMaintenancePortion *= Math.Sqrt(num);
            troopMaintenancePortion *= Math.Sqrt(0.5 * (1.0 + Policy.TroopGarrisonLevel));
            shipMaintenancePortion = Math.Min(0.7, Math.Max(0.55, shipMaintenancePortion));
            troopMaintenancePortion = Math.Min(0.3, Math.Max(0.2, troopMaintenancePortion));
            facilityMaintenancePortion = Math.Min(0.15, Math.Max(0.08, facilityMaintenancePortion));
            double num4 = 0.17;
            if (annualIncome > 100000.0)
            {
                double num5 = Math.Min(1.0, (annualIncome - 100000.0) / 200000.0);
                num4 -= num5 * 0.07;
            }
            num4 *= Math.Sqrt(Policy.ResearchPriority);
            num4 = Math.Min(0.2, Math.Max(0.06, num4));
            double num6 = 1.0 - num4;
            double num7 = shipMaintenancePortion + troopMaintenancePortion + facilityMaintenancePortion;
            shipMaintenancePortion *= num6 / num7;
            troopMaintenancePortion *= num6 / num7;
            facilityMaintenancePortion *= num6 / num7;
        }

        private void ProjectForceStructure()
        {
            double num = CalculateAccurateAnnualIncome();
            CalculateStateExpenditureBalance(num, out var shipMaintenancePortion, out var _, out var _);
            double annualStateMaintenance = AnnualStateMaintenance;
            double num2 = num * shipMaintenancePortion;
            double num3 = Math.Max(1.0, num2 / annualStateMaintenance);
            _StateForceStructureProjections = new ForceStructureProjectionList();
            long currentStarDate = _Galaxy.CurrentStarDate;
            _ = _Galaxy.IntoleranceLevel;
            int num4 = BuiltObjects.TotalMobileMilitaryFirepower();
            int num5 = 0;
            double num6 = 1.0;
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation.OtherEmpire == _Galaxy.IndependentEmpire || diplomaticRelation.OtherEmpire.PirateEmpireBaseHabitat != null)
                {
                    continue;
                }
                EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(diplomaticRelation.OtherEmpire);
                if (diplomaticRelation.Type == DiplomaticRelationType.War)
                {
                    int num7 = empireEvaluation.Empire.BuiltObjects.TotalMobileMilitaryFirepower();
                    num5 += num7;
                    num6 += (double)num7 / (double)num4 * 0.6;
                    continue;
                }
                bool flag = false;
                switch (diplomaticRelation.Strategy)
                {
                    case DiplomaticStrategy.Conquer:
                    case DiplomaticStrategy.Defend:
                    case DiplomaticStrategy.DefendPlacate:
                    case DiplomaticStrategy.DefendUndermine:
                        flag = true;
                        break;
                }
                if (flag)
                {
                    int num8 = empireEvaluation.Empire.BuiltObjects.TotalMobileMilitaryFirepower();
                    num5 += num8;
                    num6 += (double)num8 / (double)num4 * 0.4;
                }
            }
            if (!CheckEmpireHasHyperDriveTech(this))
            {
                for (int j = 0; j < PirateRelations.Count; j++)
                {
                    PirateRelation pirateRelation = PirateRelations[j];
                    if (pirateRelation.OtherEmpire != _Galaxy.IndependentEmpire && pirateRelation.Type == PirateRelationType.None)
                    {
                        int num9 = 1;
                        if (pirateRelation.OtherEmpire != null && pirateRelation.OtherEmpire.BuiltObjects != null)
                        {
                            num9 = pirateRelation.OtherEmpire.BuiltObjects.TotalMobileMilitaryFirepower();
                        }
                        num5 += num9;
                        num6 += (double)num9 / (double)num4 * 0.6;
                    }
                }
            }
            double num10 = (double)Math.Max(1, num5) / (double)Math.Max(1, num4);
            if (num10 < 1.0)
            {
                num10 = 1.0;
            }
            num10 = Math.Max(num10, num6);
            num10 = Math.Min(4.0, Math.Max(1.0, num10));
            HabitatList habitatList = DetermineLargestColonyInEachSystem();
            double num11 = 80000.0;
            double num12 = 5.0;
            switch (Policy.ConstructionMilitary)
            {
                case 0:
                    if (Colonies.Count < 3)
                    {
                        num11 = 70000.0;
                        num12 = 10.0;
                    }
                    else if (Colonies.Count < 5)
                    {
                        num11 = 80000.0;
                        num12 = 8.0;
                    }
                    else if (Colonies.Count < 10)
                    {
                        num11 = 90000.0;
                        num12 = 6.5;
                    }
                    else if (Colonies.Count < 20)
                    {
                        num11 = 100000.0;
                        num12 = 5.5;
                    }
                    else
                    {
                        num11 = 110000.0;
                        num12 = 4.5;
                    }
                    break;
                case 1:
                    if (Colonies.Count < 3)
                    {
                        num11 = 50000.0;
                        num12 = 11.0;
                    }
                    else if (Colonies.Count < 5)
                    {
                        num11 = 57000.0;
                        num12 = 9.5;
                    }
                    else if (Colonies.Count < 10)
                    {
                        num11 = 65000.0;
                        num12 = 8.0;
                    }
                    else if (Colonies.Count < 20)
                    {
                        num11 = 70000.0;
                        num12 = 7.0;
                    }
                    else
                    {
                        num11 = 80000.0;
                        num12 = 6.0;
                    }
                    break;
                case 2:
                    if (Colonies.Count < 3)
                    {
                        num11 = 25000.0;
                        num12 = 14.0;
                    }
                    else if (Colonies.Count < 5)
                    {
                        num11 = 30000.0;
                        num12 = 11.5;
                    }
                    else if (Colonies.Count < 10)
                    {
                        num11 = 40000.0;
                        num12 = 10.0;
                    }
                    else if (Colonies.Count < 20)
                    {
                        num11 = 50000.0;
                        num12 = 9.0;
                    }
                    else
                    {
                        num11 = 60000.0;
                        num12 = 8.0;
                    }
                    break;
            }
            if (Colonies.Count == 1 && (!CheckEmpireHasHyperDriveTech(this) || !CheckEmpireHasColonizationTech(this)))
            {
                num12 *= 1.5;
            }
            double num13 = Math.Max(1.0, (double)TotalColonyStrategicValue / num11 * num10 * num3);
            int num14 = 0;
            num14 += BuiltObjects.CountBySubRole(BuiltObjectSubRole.Escort);
            num14 += BuiltObjects.CountBySubRole(BuiltObjectSubRole.Frigate);
            num14 += BuiltObjects.CountBySubRole(BuiltObjectSubRole.Destroyer);
            num14 += BuiltObjects.CountBySubRole(BuiltObjectSubRole.Cruiser);
            num14 += BuiltObjects.CountBySubRole(BuiltObjectSubRole.CapitalShip);
            num14 += BuiltObjects.CountBySubRole(BuiltObjectSubRole.Carrier);
            num14 += BuiltObjects.CountBySubRole(BuiltObjectSubRole.TroopTransport);
            double val = (double)Colonies.Count * num12 * num10;
            val = Math.Max(val, (double)num14 * num3);
            if (DominantRace != null && DominantRace.Expanding)
            {
                num13 = Math.Min(num13, val);
            }
            float num15 = Policy.ConstructionMilitaryEscort + Policy.ConstructionMilitaryFrigate + Policy.ConstructionMilitaryDestroyer + Policy.ConstructionMilitaryCruiser + Policy.ConstructionMilitaryCapitalShip + Policy.ConstructionMilitaryTroopTransport;
            double num16 = 1.0 / (double)num15 * 100.0;
            double num17 = Math.Max(1.0, num13 * (double)(Policy.ConstructionMilitaryEscort / 100f) * num16);
            double num18 = Math.Max(0.0, num13 * (double)(Policy.ConstructionMilitaryFrigate / 100f) * num16);
            double num19 = Math.Max(0.0, num13 * (double)(Policy.ConstructionMilitaryDestroyer / 100f) * num16);
            double num20 = Math.Max(0.0, num13 * (double)(Policy.ConstructionMilitaryCruiser / 100f) * num16);
            double num21 = Math.Max(0.0, num13 * (double)(Policy.ConstructionMilitaryCapitalShip / 100f) * num16);
            double num22 = Math.Max(0.0, num13 * (double)(Policy.ConstructionMilitaryTroopTransport / 100f) * num16);
            double num23 = Math.Max(0.0, num13 * (double)(Policy.ConstructionMilitaryCarrier / 100f) * num16);
            double val2 = Math.Max(0.0, num13 / 30.0);
            if (Colonies.Count < 5)
            {
                val2 = 0.0;
            }
            val2 = Math.Min(val2, 20.0);
            Design design = Designs.FindNewest(BuiltObjectSubRole.ResupplyShip);
            if (design != null && Capital != null && !CanBuildDesign(design))
            {
                val2 = 0.0;
            }
            design = Designs.FindNewest(BuiltObjectSubRole.CapitalShip);
            if (design != null && !CanBuildDesign(design))
            {
                num17 += num21 / 3.0;
                num18 += num21 / 3.0;
                num19 += num21 / 3.0;
                num21 = 0.0;
            }
            design = Designs.FindNewest(BuiltObjectSubRole.Cruiser);
            if (design != null && !CanBuildDesign(design))
            {
                num17 += num20 / 3.0;
                num18 += num20 / 3.0;
                num19 += num20 / 3.0;
                num20 = 0.0;
            }
            design = Designs.FindNewest(BuiltObjectSubRole.Carrier);
            if (design != null && !CanBuildDesign(design))
            {
                num17 += num23 / 3.0;
                num18 += num23 / 3.0;
                num19 += num23 / 3.0;
                num23 = 0.0;
            }
            design = Designs.FindNewest(BuiltObjectSubRole.Destroyer);
            if (design != null && !CanBuildDesign(design))
            {
                num17 += num19 / 2.0;
                num18 += num19 / 2.0;
                num19 = 0.0;
            }
            design = Designs.FindNewest(BuiltObjectSubRole.TroopTransport);
            if (design != null && !CanBuildDesign(design))
            {
                num22 = 0.0;
            }
            if (!CheckEmpireHasHyperDriveTech(this))
            {
                int num24 = BuiltObjects.CountCompletedBySubRole(BuiltObjectSubRole.ExplorationShip);
                int num25 = BuiltObjects.CountCompletedBySubRole(BuiltObjectSubRole.ConstructionShip);
                if (num24 <= 0 || num25 <= 0)
                {
                    num17 = 0.0;
                    num18 = 0.0;
                    num19 = 0.0;
                    num20 = 0.0;
                    num21 = 0.0;
                    num22 = 0.0;
                    num23 = 0.0;
                    val2 = 0.0;
                }
            }
            double num26 = 1.0;
            double num27 = 1.0;
            if (val2 > 0.0)
            {
                _StateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.ResupplyShip, (int)val2, currentStarDate));
            }
            int num28 = (int)(num17 * num26);
            if (num28 > 0)
            {
                _StateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.Escort, num28, currentStarDate));
            }
            num28 = (int)(num18 * num26);
            if (num28 > 0)
            {
                _StateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.Frigate, num28, currentStarDate));
            }
            num28 = (int)(num19 * Math.Max(num26, num27));
            if (num28 > 0)
            {
                _StateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.Destroyer, num28, currentStarDate));
            }
            num28 = (int)(num23 * Math.Max(num26, num27));
            if (num28 > 0)
            {
                _StateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.Carrier, num28, currentStarDate));
            }
            num28 = (int)(num20 * num27);
            if (num28 > 0)
            {
                _StateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.Cruiser, num28, currentStarDate));
            }
            num28 = ((habitatList.Count >= 3) ? ((int)(num21 * num27)) : 0);
            if (num28 > 0)
            {
                _StateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.CapitalShip, num28, currentStarDate));
            }
            num28 = (int)(num22 * num27);
            if (num28 > 0)
            {
                _StateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.TroopTransport, num28, currentStarDate));
            }
            num28 = ((Colonies.Count > 1) ? (Colonies.Count + 6) : ((!CheckEmpireHasHyperDriveTech(this)) ? 2 : 7));
            if (num28 < 1)
            {
                num28 = 1;
            }
            if (num28 > 12)
            {
                num28 = 12;
            }
            num28 = (int)((double)num28 * Policy.ExplorationPriority);
            if (annualStateMaintenance < num2 * 0.95 && this != _Galaxy.PlayerEmpire && _Galaxy.DifficultyLevel > 1.0 && CheckEmpireHasHyperDriveTech(this))
            {
                num28 = (int)((double)num28 * _Galaxy.DifficultyLevel);
            }
            _StateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.ExplorationShip, num28, currentStarDate));
            num28 = (int)((double)Colonies.Count / 0.9);
            if (Colonies.Count > 1 && Colonies.Count <= 2)
            {
                num28 = 4;
            }
            else if (Colonies.Count > 2 && Colonies.Count <= 5)
            {
                num28 = 6;
            }
            if (num28 < 3)
            {
                num28 = 3;
            }
            if (Colonies.Count < 2 && !CheckEmpireHasHyperDriveTech(this))
            {
                num28 = 2;
            }
            num28 = Math.Min(num28, 12);
            if (annualStateMaintenance < num2 * 0.95 && this != _Galaxy.PlayerEmpire && _Galaxy.DifficultyLevel > 1.0 && CheckEmpireHasHyperDriveTech(this))
            {
                num28 = (int)((double)num28 * _Galaxy.DifficultyLevel);
            }
            _StateForceStructureProjections.Add(new ForceStructureProjection(BuiltObjectSubRole.ConstructionShip, num28, currentStarDate));
            double num29 = CalculateSupportabilityFactor(_StateForceStructureProjections);
            if (!(num29 < 1.0))
            {
                return;
            }
            int num30 = 0;
            for (int k = 0; k < _StateForceStructureProjections.Count; k++)
            {
                ForceStructureProjection forceStructureProjection = _StateForceStructureProjections[k];
                BuiltObjectSubRole subRole = forceStructureProjection.SubRole;
                if (subRole == BuiltObjectSubRole.ExplorationShip || subRole == BuiltObjectSubRole.ConstructionShip)
                {
                    if (BuildFactor < 1.0)
                    {
                        num30 = forceStructureProjection.Amount;
                        num30 = (forceStructureProjection.Amount = Math.Max(1, (int)((double)num30 * num29)));
                    }
                }
                else
                {
                    num30 = forceStructureProjection.Amount;
                    num30 = (forceStructureProjection.Amount = Math.Max(1, (int)((double)num30 * num29)));
                }
            }
        }

        public double CalculateSpareAnnualRevenueComplete()
        {
            return CalculateSpareAnnualRevenue(AnnualStateMaintenance);
        }

        public double CalculateSpareAnnualRevenue(double newCosts)
        {
            double num = CalculateAccurateAnnualIncome();
            num -= Math.Max(AnnualTroopMaintenanceIncludeRecruiting, MinimumTroopSpending);
            num -= AnnualSubjugationTribute;
            num -= AnnualPirateProtection;
            num -= ThisYearsStateFuelCosts;
            num -= AnnualFacilityMaintenance;
            return num - newCosts;
        }

        private double CalculateSupportabilityFactor(ForceStructureProjectionList forceProjections)
        {
            double num = CalculateSpareAnnualRevenue(0.0);
            double annualSupportCosts = 0.0;
            ForceStructureProjectionList forceStructureProjectionList = CurrentStateForceStructure(out annualSupportCosts);
            ForceStructureProjectionList forceStructure = forceProjections.Diff(forceStructureProjectionList);
            double num2 = EstimateForceStructureSupportCost(forceStructure);
            double num3 = annualSupportCosts + num2;
            double num4 = StateMoney / num3;
            double num5 = 1.0;
            double num6 = Galaxy.AllowableYearsMaintenanceFromCashOnHand;
            if (CheckAtWar())
            {
                num6 = 2.0;
            }
            if (num4 < num6)
            {
                num *= 0.95;
                num5 = num / num3;
            }
            if (BuildFactor <= 0.0)
            {
                BuildFactor = 1.0;
            }
            return num5 * BuildFactor;
        }

        private double EstimateForceStructureSupportCost(ForceStructureProjection projection)
        {
            double num = EstimateForceStructureSupportCostSingleItem(projection);
            return (double)projection.Amount * num;
        }

        public double CalculateSupportCost(Design design)
        {
            double result = 0.0;
            if (design != null)
            {
                double num = design.CalculateCurrentPurchasePrice(_Galaxy) / Galaxy.ShipMarkupFactor + 1.0;
                double num2 = Galaxy.ShipMaintenanceCostPerSizeUnit * (double)design.Size;
                double num3 = num + num2;
                double num4 = design.MaintenanceSavings * num3;
                double num5 = 1.0;
                if (GovernmentAttributes != null)
                {
                    num5 = GovernmentAttributes.MaintenanceCosts;
                }
                if (PirateEmpireBaseHabitat != null)
                {
                    double d = (double)_Galaxy.BaseTechCost / 120000.0;
                    d = Math.Sqrt(d);
                    num5 *= Galaxy.PirateShipMaintenanceFactor * d;
                    switch (design.SubRole)
                    {
                        case BuiltObjectSubRole.SmallFreighter:
                        case BuiltObjectSubRole.MediumFreighter:
                        case BuiltObjectSubRole.LargeFreighter:
                        case BuiltObjectSubRole.PassengerShip:
                        case BuiltObjectSubRole.GasMiningShip:
                        case BuiltObjectSubRole.MiningShip:
                            num5 = 0.0;
                            break;
                    }
                }
                result = (num3 - num4) * num5;
            }
            return result;
        }

        private double EstimateForceStructureSupportCostSingleItem(ForceStructureProjection projection)
        {
            double result = 0.0;
            Design design = Designs.FindNewestCanBuild(projection.SubRole);
            if (design != null)
            {
                result = CalculateSupportCost(design);
            }
            return result;
        }

        private double EstimateForceStructureSupportCost(ForceStructureProjectionList forceStructure)
        {
            double num = 0.0;
            foreach (ForceStructureProjection item in forceStructure)
            {
                num += EstimateForceStructureSupportCost(item);
            }
            return num;
        }

        public void SetColonyTaxRate(Habitat colony, bool atWar)
        {
            double num = 1.0;
            if (DominantRace != null)
            {
                num += (1.0 - ((double)DominantRace.FriendlinessLevel + (double)DominantRace.IntelligenceLevel) / 200.0) / 2.0;
            }
            double num2 = 0.15;
            double num3 = colony.TaxRate;
            if (num3 <= 0.0)
            {
                num3 = num2 * num;
            }
            double num4 = 16.0;
            int num5 = 1;
            if (colony.Population != null)
            {
                num5 = ((colony.Population.TotalAmount > 2000000000) ? Policy.ColonyTaxRateLargeColony : ((colony.Population.TotalAmount <= 200000000) ? Policy.ColonyTaxRateSmallColony : Policy.ColonyTaxRateMediumColony));
            }
            double num6 = -1.0;
            switch (num5)
            {
                case 0:
                    num6 = 0.0;
                    break;
                case 1:
                    num4 = 25.0;
                    break;
                case 2:
                    num4 = 16.0;
                    break;
                case 3:
                    num4 = 10.0;
                    break;
            }
            if (atWar && Policy.ColonyTaxRateIncreaseWhenAtWar && colony.Population.TotalAmount > 500000000)
            {
                num4 = 8.0;
            }
            int num7 = 0;
            if (colony.Characters != null && colony.Characters.Count > 0)
            {
                num7 += colony.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ColonyHappiness);
            }
            if (colony.Empire != null && colony.Empire.Leader != null)
            {
                num7 += colony.Empire.Leader.ColonyHappiness;
            }
            _ = (double)num7 / 100.0;
            if (colony.Population != null)
            {
                double empireApprovalRating = colony.EmpireApprovalRating;
                if (empireApprovalRating < num4 && colony.TaxRate <= 0f)
                {
                    num3 = 0.0;
                }
                else if (empireApprovalRating > num4 || colony.TaxRate > 0f)
                {
                    double num8 = empireApprovalRating - num4;
                    double num9 = colony.TaxApproval - num8;
                    num9 *= -1.0;
                    double num10 = colony.CalculateUnmodifiedApproval(num8, subtractAdditives: false);
                    num3 = (double)colony.TaxRate + num10 / 100.0;
                }
                if (num6 >= 0.0)
                {
                    num3 = num6;
                }
                _ = colony.EmpireApprovalRating;
                _ = 0.0;
                if (GovernmentAttributes != null && GovernmentAttributes.SpecialFunctionCode == 1)
                {
                    num3 = 1.0;
                }
            }
            num3 = ((GovernmentAttributes == null || GovernmentAttributes.SpecialFunctionCode != 1) ? Math.Max(0.0, Math.Min(0.5, num3)) : Math.Max(0.0, num3));
            num3 = Math.Round(num3, 2);
            if (double.IsNaN(num3))
            {
                num3 = 0.0;
            }
            colony.TaxRate = (float)num3;
            colony.RecalculateAnnualTaxRevenue();
        }
    }
}
