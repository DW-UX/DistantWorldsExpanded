// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.BuiltObjectList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using BaconDistantWorlds;

using System;
using System.Collections.Generic;
using System.Drawing;

namespace DistantWorlds.Types
{
    [Serializable]
    public class BuiltObjectList : SyncList<BuiltObject>
    {
        public bool StripInvalidComponents()
        {
            bool result = false;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject == null || builtObject.Components == null)
                {
                    continue;
                }
                BuiltObjectComponentList builtObjectComponentList = new BuiltObjectComponentList();
                for (int j = 0; j < builtObject.Components.Count; j++)
                {
                    BuiltObjectComponent builtObjectComponent = builtObject.Components[j];
                    if (builtObjectComponent == null)
                    {
                        continue;
                    }
                    bool flag = false;
                    for (int k = 0; k < Galaxy.ComponentDefinitionsStatic.Length; k++)
                    {
                        if (Galaxy.ComponentDefinitionsStatic[k].ComponentID == builtObjectComponent.ComponentID)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        builtObjectComponentList.Add(builtObjectComponent);
                    }
                }
                for (int l = 0; l < builtObjectComponentList.Count; l++)
                {
                    builtObject.Components.Remove(builtObjectComponentList[l]);
                }
                if (builtObjectComponentList.Count > 0)
                {
                    result = true;
                    builtObject.ReDefine();
                }
            }
            return result;
        }

        public new BuiltObjectList GetRange(int index, int count)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            if (index >= 0 && index < base.Count)
            {
                for (int i = index; i < index + count; i++)
                {
                    if (i < base.Count)
                    {
                        BuiltObject builtObject = base[i];
                        if (builtObject != null)
                        {
                            builtObjectList.Add(builtObject);
                        }
                    }
                }
            }
            return builtObjectList;
        }

        public BuiltObjectList GenerateDistanceOrderedList(double x, double y)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(this);
            for (int i = 0; i < builtObjectList.Count; i++)
            {
                BuiltObject builtObject = builtObjectList[i];
                builtObject.SortTag = Galaxy.CalculateDistanceStatic(builtObject.Xpos, builtObject.Ypos, x, y);
            }
            builtObjectList.Sort();
            return builtObjectList;
        }

        public BuiltObjectList GenerateDistanceOrderedList(double x, double y, HabitatList systemPriorities)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(this);
            for (int i = 0; i < builtObjectList.Count; i++)
            {
                BuiltObject builtObject = builtObjectList[i];
                double num = Galaxy.CalculateDistanceStatic(builtObject.Xpos, builtObject.Ypos, x, y);
                if (builtObject.NearestSystemStar != null && systemPriorities.Contains(builtObject.NearestSystemStar))
                {
                    num /= 3.0;
                }
                builtObject.SortTag = num;
            }
            builtObjectList.Sort();
            return builtObjectList;
        }

        public BuiltObject GetFirstAvailableWithinRange(BuiltObjectRole role, double x, double y, double fuelPortionMargin, bool includeLowAndNormalPriorityMissions)
        {
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Role == role && builtObject.BuiltAt == null && builtObject.UnbuiltComponentCount <= 0 && (builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined || (includeLowAndNormalPriorityMissions && (builtObject.Mission.Priority == BuiltObjectMissionPriority.Low || builtObject.Mission.Priority == BuiltObjectMissionPriority.Normal))) && builtObject.WithinFuelRangeAndRefuel(x, y, fuelPortionMargin))
                {
                    return builtObject;
                }
            }
            return null;
        }

        public int TotalMobileMilitaryFirepower()
        {
            return TotalMobileMilitaryFirepower(null);
        }

        public int TotalMobileMilitaryFirepower(Empire empire)
        {
            int num = 0;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Role == BuiltObjectRole.Military && builtObject.UnbuiltComponentCount <= 0 && builtObject.TopSpeed > 0 && (empire == null || builtObject.Empire == empire))
                {
                    num += builtObject.FirepowerRaw;
                }
            }
            return num;
        }

        public int TotalMobileMilitaryFirepowerNotAttackingDefending(out int shipCount)
        {
            int num = 0;
            shipCount = 0;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Role == BuiltObjectRole.Military && builtObject.UnbuiltComponentCount <= 0 && builtObject.TopSpeed > 0 && (builtObject.Mission == null || (builtObject.Mission.Type != BuiltObjectMissionType.Attack && builtObject.Mission.Type != BuiltObjectMissionType.Bombard && builtObject.Mission.Type != BuiltObjectMissionType.WaitAndAttack && builtObject.Mission.Type != BuiltObjectMissionType.WaitAndBombard && builtObject.Mission.Type != BuiltObjectMissionType.Capture && builtObject.Mission.Type != BuiltObjectMissionType.Raid && builtObject.Mission.Type != BuiltObjectMissionType.MoveAndWait)))
                {
                    num += builtObject.FirepowerRaw;
                    shipCount++;
                }
            }
            return num;
        }

        public int CountSpaceports()
        {
            int num = 0;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && (builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort))
                {
                    num++;
                }
            }
            return num;
        }

        public int CountResearchStations()
        {
            int num = 0;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && (builtObject.SubRole == BuiltObjectSubRole.EnergyResearchStation || builtObject.SubRole == BuiltObjectSubRole.HighTechResearchStation || builtObject.SubRole == BuiltObjectSubRole.WeaponsResearchStation))
                {
                    num++;
                }
            }
            return num;
        }

        public int CountBySubRole(BuiltObjectSubRole subRole)
        {
            int num = 0;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && builtObject.SubRole == subRole)
                {
                    num++;
                }
            }
            return num;
        }

        public int CountCompletedBySubRole(BuiltObjectSubRole subRole)
        {
            int num = 0;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && builtObject.SubRole == subRole && builtObject.BuiltAt == null && builtObject.UnbuiltComponentCount <= 0 && !builtObject.HasBeenDestroyed)
                {
                    num++;
                }
            }
            return num;
        }

        public int CountByRole(BuiltObjectRole role)
        {
            int num = 0;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && builtObject.Role == role)
                {
                    num++;
                }
            }
            return num;
        }

        public BuiltObject FindShortestConstructionWaitQueueCloseToBuiltObject(BuiltObject builtObject, out double shortestWaitQueueTime)
        {
            shortestWaitQueueTime = double.MaxValue;
            BuiltObject result = null;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject2 = base[i];
                if (builtObject2 != null && builtObject2.IsSpacePort && builtObject2.IsShipYard && builtObject2.Empire.CanBuildBuiltObject(builtObject))
                {
                    double num = double.MaxValue;
                    if (builtObject2.ConstructionQueue != null)
                    {
                        num = builtObject2.ConstructionQueue.EstimateCurrentWaitQueueTime();
                    }
                    double num2 = builtObject.Empire.Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, builtObject2.Xpos, builtObject2.Ypos);
                    if (num2 > 0.0)
                    {
                        num *= Math.Sqrt(num2);
                    }
                    if (num < shortestWaitQueueTime)
                    {
                        shortestWaitQueueTime = num;
                        result = builtObject2;
                    }
                }
            }
            return result;
        }

        public BuiltObject FindShortestConstructionWaitQueue(BuiltObject builtObject, out double shortestWaitQueueTime)
        {
            return FindShortestConstructionWaitQueue(builtObject, out shortestWaitQueueTime, includeVerySmallYards: true);
        }

        public BuiltObject FindShortestConstructionWaitQueue(BuiltObject builtObject, out double shortestWaitQueueTime, bool includeVerySmallYards)
        {
            return FindShortestConstructionWaitQueue(builtObject, out shortestWaitQueueTime, includeVerySmallYards, int.MaxValue);
        }

        public BuiltObject FindShortestConstructionWaitQueue(BuiltObject builtObject, out double shortestWaitQueueTime, bool includeVerySmallYards, int maximumQueueDepth)
        {
            return BaconBuiltObjectList.FindShortestConstructionWaitQueue(this, builtObject, out shortestWaitQueueTime, includeVerySmallYards, maximumQueueDepth);
        }

        public BuiltObject FindBuiltObjectById(int builtObjectId)
        {
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && builtObject.BuiltObjectID == builtObjectId)
                {
                    return builtObject;
                }
            }
            return null;
        }

        public BuiltObjectList GetBuiltObjectsBySubRole(BuiltObjectSubRole subRole)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && builtObject.SubRole == subRole)
                {
                    builtObjectList.Add(builtObject);
                }
            }
            return builtObjectList;
        }

        public BuiltObjectList GetBuiltObjectsBySubRole(List<BuiltObjectSubRole> subRoles)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && subRoles.Contains(builtObject.SubRole))
                {
                    builtObjectList.Add(builtObject);
                }
            }
            return builtObjectList;
        }

        public BuiltObjectList GetBuiltObjectsByRole(List<BuiltObjectRole> roles)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && roles.Contains(builtObject.Role))
                {
                    builtObjectList.Add(builtObject);
                }
            }
            return builtObjectList;
        }

        public BuiltObject FindFirstBuiltObject(BuiltObjectRole role)
        {
            for (int i = 0; i < base.Count; i++)
            {
                if (base[i].Role == role)
                {
                    return base[i];
                }
            }
            return null;
        }

        public BuiltObject GetNearestBuiltObjectWithinRange(double x, double y, double fuelPortionMargin, out int index)
        {
            return GetNearestBuiltObjectWithinRange(x, y, fuelPortionMargin, mustBeAvailable: false, out index);
        }

        public BuiltObject GetNearestBuiltObjectWithinRange(double x, double y, double fuelPortionMargin, bool mustBeAvailable, out int index)
        {
            BuiltObject result = null;
            index = -1;
            double num = double.MaxValue;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null)
                {
                    double num2 = Galaxy.CalculateDistanceSquaredStatic(x, y, builtObject.Xpos, builtObject.Ypos);
                    double num3 = builtObject.CurrentRange(fuelPortionMargin);
                    double num4 = num3 * num3;
                    if (num2 <= num4 && num2 < num && (!mustBeAvailable || builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined))
                    {
                        num = num2;
                        result = builtObject;
                        index = i;
                    }
                }
            }
            return result;
        }

        public int CountBuiltObjectsWithTargetHabitat(Habitat habitat)
        {
            int num = 0;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null)
                {
                    if (builtObject.ParentHabitat == habitat)
                    {
                        num++;
                    }
                    else if (builtObject.Mission != null && builtObject.Mission.TargetHabitat != null && builtObject.Mission.TargetHabitat == habitat)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public BuiltObjectList GetShipsWithoutWarpDrives()
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && builtObject.Role != BuiltObjectRole.Base && builtObject.WarpSpeed <= 0 && builtObject.UnbuiltComponentCount <= 0 && builtObject.BuiltAt == null)
                {
                    builtObjectList.Add(builtObject);
                }
            }
            return builtObjectList;
        }

        public int CountPlanetDestroyers()
        {
            int num = 0;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && builtObject.Design != null && builtObject.Design.IsPlanetDestroyer)
                {
                    num++;
                }
            }
            return num;
        }

        public int CountConstructionShipsBuildingPlanetDestroyers()
        {
            BuiltObjectList constructionShipsBuildingPlanetDestroyers = GetConstructionShipsBuildingPlanetDestroyers();
            return constructionShipsBuildingPlanetDestroyers.Count;
        }

        public BuiltObjectList GetConstructionShipsBuildingPlanetDestroyers()
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && builtObject.SubRole == BuiltObjectSubRole.ConstructionShip && builtObject.ConstructionQueue != null && builtObject.ConstructionQueue.ConstructionYards != null && builtObject.ConstructionQueue.ConstructionWaitQueue != null)
                {
                    if (builtObject.ConstructionQueue.ConstructionWaitQueue.Count > 0 && builtObject.ConstructionQueue.ConstructionWaitQueue.CountPlanetDestroyers() > 0)
                    {
                        builtObjectList.Add(builtObject);
                    }
                    else if (builtObject.ConstructionQueue.ConstructionYards.CountPlanetDestroyersUnderConstruction > 0)
                    {
                        builtObjectList.Add(builtObject);
                    }
                    else if (builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Build && builtObject.Mission.Design != null && builtObject.Mission.Design.IsPlanetDestroyer)
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
            }
            return builtObjectList;
        }

        public int CountBuiltObjectsWithTargetHabitat(Habitat habitat, List<BuiltObjectSubRole> subRoles)
        {
            int num = 0;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject == null)
                {
                    continue;
                }
                if (builtObject.ParentHabitat == habitat)
                {
                    if (subRoles.Contains(builtObject.SubRole))
                    {
                        num++;
                    }
                }
                else if (builtObject.Mission != null && builtObject.Mission.TargetHabitat != null && builtObject.Mission.TargetHabitat == habitat && subRoles.Contains(builtObject.SubRole))
                {
                    num++;
                }
            }
            return num;
        }

        public BuiltObject GetNearestBuiltObjectCompleteUndamaged(double x, double y, BuiltObjectRole role, BuiltObject builtObjectToExclude)
        {
            BuiltObject result = null;
            double num = double.MaxValue;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && builtObject != builtObjectToExclude && builtObject.Role == role && builtObject.UnbuiltOrDamagedComponentCount <= 0 && builtObject.BuiltAt == null)
                {
                    double num2 = Galaxy.CalculateDistanceSquaredStatic(x, y, builtObject.Xpos, builtObject.Ypos);
                    if (num2 < num)
                    {
                        num = num2;
                        result = builtObject;
                    }
                }
            }
            return result;
        }

        public BuiltObject GetNearestBuiltObject(double x, double y, BuiltObjectRole role, BuiltObject builtObjectToExclude)
        {
            BuiltObject result = null;
            double num = double.MaxValue;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && builtObject != builtObjectToExclude && builtObject.Role == role)
                {
                    double num2 = Galaxy.CalculateDistanceSquaredStatic(x, y, builtObject.Xpos, builtObject.Ypos);
                    if (num2 < num)
                    {
                        num = num2;
                        result = builtObject;
                    }
                }
            }
            return result;
        }

        public BuiltObject GetNearestBuiltObject(double x, double y, out int index)
        {
            BuiltObject result = null;
            index = -1;
            double num = double.MaxValue;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null)
                {
                    double num2 = Galaxy.CalculateDistanceSquaredStatic(x, y, builtObject.Xpos, builtObject.Ypos);
                    if (num2 < num)
                    {
                        num = num2;
                        result = builtObject;
                        index = i;
                    }
                }
            }
            return result;
        }

        public BuiltObjectList GetShipsAtHabitatNotLeaving(Habitat habitat, double range)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            double num = range * range;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject == null || builtObject.HasBeenDestroyed || builtObject.Role == BuiltObjectRole.Base)
                {
                    continue;
                }
                double num2 = Galaxy.CalculateDistanceSquaredStatic(habitat.Xpos, habitat.Ypos, builtObject.Xpos, builtObject.Ypos);
                if (!(num2 < num))
                {
                    continue;
                }
                if (builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined)
                {
                    builtObjectList.Add(builtObject);
                    continue;
                }
                Point point = builtObject.Mission.ResolveTargetCoordinates(builtObject.Mission);
                double num3 = Galaxy.CalculateDistanceSquaredStatic(habitat.Xpos, habitat.Ypos, point.X, point.Y);
                if (num3 < num)
                {
                    builtObjectList.Add(builtObject);
                }
            }
            return builtObjectList;
        }

        public int CalculateAttackingFirepowerNearEmpireTargets(Empire targetEmpire)
        {
            int num = 0;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject == null || builtObject.HasBeenDestroyed)
                {
                    continue;
                }
                BuiltObjectMission mission = builtObject.Mission;
                if (mission == null)
                {
                    continue;
                }
                bool flag = false;
                StellarObject stellarObject = null;
                Empire empire = null;
                switch (mission.Type)
                {
                    case BuiltObjectMissionType.Attack:
                    case BuiltObjectMissionType.WaitAndAttack:
                    case BuiltObjectMissionType.WaitAndBombard:
                    case BuiltObjectMissionType.Bombard:
                    case BuiltObjectMissionType.Capture:
                    case BuiltObjectMissionType.Raid:
                        flag = true;
                        if (mission.TargetBuiltObject != null)
                        {
                            stellarObject = mission.TargetBuiltObject;
                            empire = stellarObject.Empire;
                        }
                        else if (mission.TargetHabitat != null)
                        {
                            stellarObject = mission.TargetHabitat;
                            empire = stellarObject.Empire;
                        }
                        else if (mission.TargetShipGroup != null && mission.TargetShipGroup.LeadShip != null)
                        {
                            stellarObject = mission.TargetShipGroup.LeadShip;
                            empire = stellarObject.Empire;
                        }
                        break;
                }
                if (flag && stellarObject != null && empire != null && empire == targetEmpire)
                {
                    double num2 = Galaxy.CalculateDistanceSquaredStatic(builtObject.Xpos, builtObject.Ypos, stellarObject.Xpos, stellarObject.Ypos);
                    if (num2 < 2500000000.0)
                    {
                        num += builtObject.FirepowerRaw;
                    }
                }
            }
            return num;
        }

        public int CountNonPirates()
        {
            int num = 0;
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && builtObject.PirateEmpireId <= 0)
                {
                    num++;
                }
            }
            return num;
        }

        public BuiltObjectList GetFirepowerGreaterThan(int firepower)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                if (builtObject != null && builtObject.FirepowerRaw > firepower)
                {
                    builtObjectList.Add(builtObject);
                }
            }
            return builtObjectList;
        }

        public ResourceList DetermineFuelRequired(bool setFuelLevelToZero)
        {
            ResourceList resourceList = new ResourceList();
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                int num = 1;
                if (!setFuelLevelToZero)
                {
                    num = builtObject.FuelCapacity - (int)builtObject.CurrentFuel;
                }
                int num2 = resourceList.IndexOf(builtObject.FuelType.ResourceID);
                if (num2 >= 0)
                {
                    resourceList[num2].SortTag += num;
                    continue;
                }
                Resource resource = new Resource(builtObject.FuelType.ResourceID);
                resource.SortTag = num;
                resourceList.Add(resource);
            }
            return resourceList;
        }

        public BuiltObjectList OrderByName()
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            BuiltObjectList builtObjectList2 = new BuiltObjectList();
            List<string> list = new List<string>();
            for (int i = 0; i < base.Count; i++)
            {
                BuiltObject builtObject = base[i];
                list.Add(builtObject.Name);
                builtObjectList2.Add(builtObject);
            }
            BuiltObject[] items = builtObjectList2.ToArray();
            string[] keys = list.ToArray();
            Array.Sort(keys, items);
            builtObjectList.AddRange(items);
            return builtObjectList;
        }
    }
}
