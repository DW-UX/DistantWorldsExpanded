// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.BuiltObjectDict
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using BaconDistantWorlds;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DistantWorlds.Types
{
    [Serializable]
    public class BuiltObjectDict : ConcurrentDictionary<int, BuiltObject>
    {
        public bool StripInvalidComponents()
        {
            bool result = false;
            //for (int i = 0; i < base.Count; i++)
            foreach (BuiltObject builtObject in this.Values)
            {
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

        //public new BuiltObjectDict GetRange(int index, int count)
        //{
        //    BuiltObjectDict builtObjectList = new BuiltObjectDict();
        //    if (index >= 0 && index < base.Count)
        //    {
        //        for (int i = index; i < index + count; i++)
        //        {
        //            if (i < base.Count)
        //            {
        //                BuiltObject builtObject = base[i];
        //                if (builtObject != null)
        //                {
        //                    builtObjectList.Add(builtObject);
        //                }
        //            }
        //        }
        //    }
        //    return builtObjectList;
        //}

        //public BuiltObjectDict GenerateDistanceOrderedList(double x, double y)
        //{
        //    BuiltObjectDict builtObjectList = new BuiltObjectDict();
        //    builtObjectList.AddRange(this);
        //    for (int i = 0; i < builtObjectList.Count; i++)
        //    {
        //        BuiltObject builtObject = builtObjectList[i];
        //        builtObject.SortTag = Galaxy.CalculateDistanceStatic(builtObject.Xpos, builtObject.Ypos, x, y);
        //    }
        //    builtObjectList.Sort();
        //    return builtObjectList;
        //}

        public List<BuiltObject> GenerateDistanceOrderedList(double x, double y, HabitatList systemPriorities)
        {
            List<BuiltObject> builtObjectList = this.Values.OrderBy(builtObject =>
            {
                double num = Galaxy.CalculateDistanceStatic(builtObject.Xpos, builtObject.Ypos, x, y);
                if (builtObject.NearestSystemStar != null && systemPriorities.Contains(builtObject.NearestSystemStar))
                {
                    num /= 3.0;
                }
                builtObject.SortTag = num;
                return num;
            }).ToList();
            return builtObjectList;
        }

        public BuiltObject GetFirstAvailableWithinRange(BuiltObjectRole role, double x, double y, double fuelPortionMargin, bool includeLowAndNormalPriorityMissions)
        {
            //for (int i = 0; i < base.Count; i++)
            //{
            //    BuiltObject builtObject = base[i];
            //    if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Role == role && builtObject.BuiltAt == null && builtObject.UnbuiltComponentCount <= 0 && (builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined || (includeLowAndNormalPriorityMissions && (builtObject.Mission.Priority == BuiltObjectMissionPriority.Low || builtObject.Mission.Priority == BuiltObjectMissionPriority.Normal))) && builtObject.WithinFuelRangeAndRefuel(x, y, fuelPortionMargin))
            //    {
            //        return builtObject;
            //    }
            //}
            var res = this.Values.FirstOrDefault(builtObject =>

                builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Role == role && builtObject.BuiltAt == null && builtObject.UnbuiltComponentCount <= 0 && (builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined || (includeLowAndNormalPriorityMissions && (builtObject.Mission.Priority == BuiltObjectMissionPriority.Low || builtObject.Mission.Priority == BuiltObjectMissionPriority.Normal))) && builtObject.WithinFuelRangeAndRefuel(x, y, fuelPortionMargin)

            );
            return res;
        }

        public int TotalMobileMilitaryFirepower()
        {
            return TotalMobileMilitaryFirepower(null);
        }

        public int TotalMobileMilitaryFirepower(Empire empire)
        {
            int num = this.Values.Sum(builtObject =>
            {
                if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Role == BuiltObjectRole.Military && builtObject.UnbuiltComponentCount <= 0 && builtObject.TopSpeed > 0 && (empire == null || builtObject.Empire == empire))
                {
                    return builtObject.FirepowerRaw;
                }
                return 0;
            });
            return num;
        }

        public int TotalMobileMilitaryFirepowerNotAttackingDefending(out int shipCount)
        {
            shipCount = 0;
            int tempShipCount = 0;
            int num = this.Values.Sum(builtObject =>
            {
                if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Role == BuiltObjectRole.Military && builtObject.UnbuiltComponentCount <= 0 && builtObject.TopSpeed > 0 && (builtObject.Mission == null || (builtObject.Mission.Type != BuiltObjectMissionType.Attack && builtObject.Mission.Type != BuiltObjectMissionType.Bombard && builtObject.Mission.Type != BuiltObjectMissionType.WaitAndAttack && builtObject.Mission.Type != BuiltObjectMissionType.WaitAndBombard && builtObject.Mission.Type != BuiltObjectMissionType.Capture && builtObject.Mission.Type != BuiltObjectMissionType.Raid && builtObject.Mission.Type != BuiltObjectMissionType.MoveAndWait)))
                {
                    tempShipCount++;
                    return builtObject.FirepowerRaw;
                }
                return 0;
            });
            shipCount = tempShipCount;
            return num;
        }

        public int CountSpaceports()
        {
            int num = this.Values.Count(builtObject =>
            {
                if (builtObject != null && (builtObject.SubRole == BuiltObjectSubRole.Outpost || builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort))
                {
                    return true;
                }
                return false;
            });
            return num;
        }

        public int CountResearchStations()
        {
            int num = this.Values.Count(builtObject =>
            {
                if (builtObject != null && (builtObject.SubRole == BuiltObjectSubRole.EnergyResearchStation || builtObject.SubRole == BuiltObjectSubRole.HighTechResearchStation || builtObject.SubRole == BuiltObjectSubRole.WeaponsResearchStation))
                {
                    return true;
                }
                return false;
            });
            return num;
        }

        public int CountBySubRole(BuiltObjectSubRole subRole)
        {
            int num = this.Values.Count(builtObject =>
            {
                if (builtObject != null && builtObject.SubRole == subRole)
                {
                    return true;
                }
                return false;
            });
            return num;
        }

        public int CountCompletedBySubRole(BuiltObjectSubRole subRole)
        {
            int num = this.Values.Count(builtObject =>
            {
                if (builtObject != null && builtObject.SubRole == subRole && builtObject.BuiltAt == null && builtObject.UnbuiltComponentCount <= 0 && !builtObject.HasBeenDestroyed)
                {
                    return true;
                }
                return false;
            });
            return num;
        }

        public int CountByRole(BuiltObjectRole role)
        {
            int num = this.Values.Count(builtObject =>
            {
                if (builtObject != null && builtObject.Role == role)
                {
                    return true;
                }
                return false;
            });
            return num;
        }

        public BuiltObject FindShortestConstructionWaitQueueCloseToBuiltObject(BuiltObject builtObject, out double shortestWaitQueueTime)
        {
            shortestWaitQueueTime = double.MaxValue;
            double tempShortestWaitQueueTime = double.MaxValue;
            BuiltObject result = this.Values.MinBy(builtObject2 =>
            {
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
                    if (num < tempShortestWaitQueueTime)
                    {
                        tempShortestWaitQueueTime = num;
                        result = builtObject2;
                    }
                }
                return tempShortestWaitQueueTime;
            });
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
            BuiltObject result;
            this.TryGetValue(builtObjectId, out result);
            return result;
        }

        public List<BuiltObject> GetBuiltObjectsBySubRole(BuiltObjectSubRole subRole)
        {
            List<BuiltObject> builtObjectList = this.Values.Where(builtObject => builtObject != null && builtObject.SubRole == subRole).ToList();
            return builtObjectList;
        }

        public List<BuiltObject> GetBuiltObjectsBySubRole(List<BuiltObjectSubRole> subRoles)
        {
            List<BuiltObject> builtObjectList = this.Values.Where(builtObject => builtObject != null && subRoles.Contains(builtObject.SubRole)).ToList();
            return builtObjectList;
        }

        public List<BuiltObject> GetBuiltObjectsByRole(List<BuiltObjectRole> roles)
        {
            List<BuiltObject> builtObjectList = this.Values.Where(builtObject => builtObject != null && roles.Contains(builtObject.Role)).ToList();
            return builtObjectList;
        }

        public BuiltObject FindFirstBuiltObject(BuiltObjectRole role)
        {
            return this.Values.FirstOrDefault(builtObject => builtObject != null && builtObject.Role == role);
        }

        public BuiltObject GetNearestBuiltObjectWithinRange(double x, double y, double fuelPortionMargin)
        {
            return GetNearestBuiltObjectWithinRange(x, y, fuelPortionMargin, mustBeAvailable: false);
        }

        public BuiltObject GetNearestBuiltObjectWithinRange(double x, double y, double fuelPortionMargin, bool mustBeAvailable)
        {
            BuiltObject result = this.Values.MinBy(builtObject =>
            {
                double num2 = Galaxy.CalculateDistanceSquaredStatic(x, y, builtObject.Xpos, builtObject.Ypos);
                double num3 = builtObject.CurrentRange(fuelPortionMargin);
                double num4 = num3 * num3;
                if (num2 <= num4 && (!mustBeAvailable || builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined))
                {
                    return num2;
                }
                return double.MaxValue;
            });
            return result;
        }

        public int CountBuiltObjectsWithTargetHabitat(Habitat habitat)
        {
            int num = this.Values.Count(builtObject =>
            {
                if (builtObject.ParentHabitat == habitat)
                {
                    return true;
                }
                else if (builtObject.Mission != null && builtObject.Mission.TargetHabitat != null && builtObject.Mission.TargetHabitat == habitat)
                {
                    return true;
                }
                return false;
            });
            return num;
        }

        public List<BuiltObject> GetShipsWithoutWarpDrives()
        {
            List<BuiltObject> builtObjectList = this.Values.Where(builtObject => builtObject != null && builtObject.Role != BuiltObjectRole.Base && builtObject.WarpSpeed <= 0 && builtObject.UnbuiltComponentCount <= 0 && builtObject.BuiltAt == null).ToList();
            return builtObjectList;
        }

        public int CountPlanetDestroyers()
        {
            int num = this.Values.Count(builtObject =>
            {
                if (builtObject != null && builtObject.Design != null && builtObject.Design.IsPlanetDestroyer)
                {
                    return true;
                }
                return false;
            });
            return num;
        }

        public int CountConstructionShipsBuildingPlanetDestroyers()
        {
            List<BuiltObject> constructionShipsBuildingPlanetDestroyers = GetConstructionShipsBuildingPlanetDestroyers();
            return constructionShipsBuildingPlanetDestroyers.Count;
        }

        public List<BuiltObject> GetConstructionShipsBuildingPlanetDestroyers()
        {
            List<BuiltObject> builtObjectList = this.Values.Where(builtObject =>
            {
                if (builtObject != null && builtObject.SubRole == BuiltObjectSubRole.ConstructionShip && builtObject.ConstructionQueue != null && builtObject.ConstructionQueue.ConstructionYards != null && builtObject.ConstructionQueue.ConstructionWaitQueue != null)
                {
                    if (builtObject.ConstructionQueue.ConstructionWaitQueue.Count > 0 && builtObject.ConstructionQueue.ConstructionWaitQueue.CountPlanetDestroyers() > 0)
                    {
                        return true;
                    }
                    else if (builtObject.ConstructionQueue.ConstructionYards.CountPlanetDestroyersUnderConstruction > 0)
                    {
                        return true;
                    }
                    else if (builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Build && builtObject.Mission.Design != null && builtObject.Mission.Design.IsPlanetDestroyer)
                    {
                        return true;
                    }
                }
                return false;
            }).ToList();
            return builtObjectList;
        }

        public int CountBuiltObjectsWithTargetHabitat(Habitat habitat, List<BuiltObjectSubRole> subRoles)
        {
            int num = this.Values.Count(builtObject =>
            {
                if (builtObject.ParentHabitat == habitat)
                {
                    if (subRoles.Contains(builtObject.SubRole))
                    {
                        return true;
                    }
                }
                else if (builtObject.Mission != null && builtObject.Mission.TargetHabitat != null && builtObject.Mission.TargetHabitat == habitat && subRoles.Contains(builtObject.SubRole))
                {
                    return true;
                }
                return false;
            });
            return num;
        }

        public BuiltObject GetNearestBuiltObjectCompleteUndamaged(double x, double y, BuiltObjectRole role, BuiltObject builtObjectToExclude)
        {
            BuiltObject result = this.Values.MinBy(builtObject =>
            {
                if (builtObject != null && builtObject != builtObjectToExclude && builtObject.Role == role && builtObject.UnbuiltOrDamagedComponentCount <= 0 && builtObject.BuiltAt == null)
                {
                    return Galaxy.CalculateDistanceSquaredStatic(x, y, builtObject.Xpos, builtObject.Ypos);
                }
                return double.MaxValue;
            });
            return result;
        }

        public BuiltObject GetNearestBuiltObject(double x, double y, BuiltObjectRole role, BuiltObject builtObjectToExclude)
        {
            BuiltObject result = this.Values.MinBy(builtObject =>
            {
                if (builtObject != null && builtObject != builtObjectToExclude && builtObject.Role == role)
                {
                    return Galaxy.CalculateDistanceSquaredStatic(x, y, builtObject.Xpos, builtObject.Ypos);
                }
                return double.MaxValue;
            });
            return result;
        }

        //public BuiltObject GetNearestBuiltObject(double x, double y, out int index)
        //{
        //    BuiltObject result = null;
        //    index = -1;
        //    double num = double.MaxValue;
        //    for (int i = 0; i < base.Count; i++)
        //    {
        //        BuiltObject builtObject = base[i];
        //        if (builtObject != null)
        //        {
        //            double num2 = Galaxy.CalculateDistanceSquaredStatic(x, y, builtObject.Xpos, builtObject.Ypos);
        //            if (num2 < num)
        //            {
        //                num = num2;
        //                result = builtObject;
        //                index = i;
        //            }
        //        }
        //    }
        //    return result;
        //}

        public List<BuiltObject> GetShipsAtHabitatNotLeaving(Habitat habitat, double range)
        {
            double num = range * range;
            List<BuiltObject> builtObjectList = this.Values.Where(builtObject =>
            {
                if (builtObject == null || builtObject.HasBeenDestroyed || builtObject.Role == BuiltObjectRole.Base)
                {
                    return false;
                }
                double num2 = Galaxy.CalculateDistanceSquaredStatic(habitat.Xpos, habitat.Ypos, builtObject.Xpos, builtObject.Ypos);
                if (!(num2 < num))
                {
                    return false;
                }
                if (builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined)
                {
                    return true;
                }
                Point point = builtObject.Mission.ResolveTargetCoordinates(builtObject.Mission);
                double num3 = Galaxy.CalculateDistanceSquaredStatic(habitat.Xpos, habitat.Ypos, point.X, point.Y);
                if (num3 < num)
                {
                    return true;
                }
                return false;
            }).ToList();
            return builtObjectList;
        }

        public int CalculateAttackingFirepowerNearEmpireTargets(Empire targetEmpire)
        {
            int num = this.Values.Sum(builtObject =>
            {
                if (builtObject == null || builtObject.HasBeenDestroyed)
                {
                    return 0;
                }
                BuiltObjectMission mission = builtObject.Mission;
                if (mission == null)
                {
                    return 0;
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
                        return builtObject.FirepowerRaw;
                    }
                }
                return 0;
            });
            return num;
        }

        public int CountNonPirates()
        {
            return this.Values.Count(builtObject => builtObject != null && builtObject.PirateEmpireId <= 0);
        }

        public List<BuiltObject> GetFirepowerGreaterThan(int firepower)
        {
            List<BuiltObject> builtObjectList = this.Values.Where(builtObject => builtObject != null && builtObject.FirepowerRaw > firepower).ToList();
            return builtObjectList;
        }

        public ResourceList DetermineFuelRequired(bool setFuelLevelToZero)
        {
            ResourceList resourceList = new ResourceList();
            foreach(var builtObject in this.Values)
            {
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

        public BuiltObject[] OrderByName()
        {
            return this.Values.OrderBy(x=>x.Name).ToArray();
        }
    }
}
