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
        private int DetermineShipStrengthNearHabitat(Habitat habitat, Empire empire, out BuiltObjectList ships)
        {
            int num = 0;
            ships = new BuiltObjectList();
            if (empire != null && empire.BuiltObjects != null && habitat != null)
            {
                float num2 = 4000000f;
                Habitat habitat2 = DetermineHabitatSystemStar(habitat);
                if (empire.BuiltObjects != null)
                {
                    for (int i = 0; i < empire.BuiltObjects.Count; i++)
                    {
                        BuiltObject builtObject = empire.BuiltObjects[i];
                        if (builtObject != null && builtObject.NearestSystemStar == habitat2 && builtObject.Role == BuiltObjectRole.Military && builtObject.BuiltAt == null)
                        {
                            double num3 = 0.0;
                            if (builtObject.WarpSpeed <= 0)
                            {
                                num3 = CalculateDistanceSquared(habitat.Xpos, habitat.Ypos, builtObject.Xpos, builtObject.Ypos);
                            }
                            if (num3 < (double)num2)
                            {
                                num += builtObject.CalculateOverallStrengthFactor();
                                ships.Add(builtObject);
                            }
                        }
                    }
                }
            }
            return num;
        }

        private int DetermineShipStrengthInSystem(Habitat systemStar, Empire empire, out BuiltObjectList ships)
        {
            int num = 0;
            ships = new BuiltObjectList();
            if (empire != null && empire.BuiltObjects != null && systemStar != null)
            {
                for (int i = 0; i < empire.BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = empire.BuiltObjects[i];
                    if (builtObject != null && builtObject.NearestSystemStar == systemStar && builtObject.Role == BuiltObjectRole.Military && builtObject.BuiltAt == null && builtObject.WarpSpeed > 0)
                    {
                        num += builtObject.CalculateOverallStrengthFactor();
                        ships.Add(builtObject);
                    }
                }
            }
            return num;
        }

        private int DetermineShipStrengthInSystem(Habitat systemStar, double x, double y, Empire empire, out BuiltObjectList ships)
        {
            int num = 0;
            ships = new BuiltObjectList();
            if (empire != null && empire.BuiltObjects != null && systemStar != null)
            {
                float num2 = 4000000f;
                for (int i = 0; i < empire.BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = empire.BuiltObjects[i];
                    if (builtObject != null && builtObject.NearestSystemStar == systemStar && builtObject.Role == BuiltObjectRole.Military && builtObject.BuiltAt == null)
                    {
                        double num3 = 0.0;
                        if (builtObject.WarpSpeed <= 0)
                        {
                            num3 = CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                        }
                        if (num3 < (double)num2)
                        {
                            num += builtObject.CalculateOverallStrengthFactor();
                            ships.Add(builtObject);
                        }
                    }
                }
            }
            return num;
        }

        public int DetermineBuiltObjectStrengthInSystem(Habitat systemStar, Empire empire, int unarmedStrength, bool includeAllies, out BuiltObjectList ships)
        {
            ships = new BuiltObjectList();
            int num = DetermineBuiltObjectStrengthInSystem(systemStar, empire, unarmedStrength, ref ships);
            if (includeAllies && empire != null && empire.DiplomaticRelations != null)
            {
                for (int i = 0; i < empire.DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = empire.DiplomaticRelations[i];
                    if (diplomaticRelation != null && (diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || (diplomaticRelation.Type == DiplomaticRelationType.Protectorate && diplomaticRelation.Initiator != empire)))
                    {
                        num += DetermineBuiltObjectStrengthInSystem(systemStar, diplomaticRelation.OtherEmpire, unarmedStrength, ref ships);
                    }
                }
            }
            return num;
        }

        public int DetermineBuiltObjectStrengthAtLocation(int x, int y, Empire empire, int unarmedStrength, bool includeAllies, out BuiltObjectList ships)
        {
            ships = new BuiltObjectList();
            int num = DetermineBuiltObjectStrengthAtLocation(x, y, empire, unarmedStrength, ref ships);
            if (includeAllies && empire != null && empire.DiplomaticRelations != null)
            {
                for (int i = 0; i < empire.DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = empire.DiplomaticRelations[i];
                    if (diplomaticRelation != null && (diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || (diplomaticRelation.Type == DiplomaticRelationType.Protectorate && diplomaticRelation.Initiator != empire)))
                    {
                        num += DetermineBuiltObjectStrengthAtLocation(x, y, diplomaticRelation.OtherEmpire, unarmedStrength, ref ships);
                    }
                }
            }
            return num;
        }

        public int DetermineShipStrengthInSystemExcludeLowEngagementRange(Habitat systemStar, Empire empire, out BuiltObjectList ships)
        {
            int num = 0;
            ships = new BuiltObjectList();
            float num2 = 4000000f;
            if (empire != null && empire.BuiltObjects != null && systemStar != null)
            {
                for (int i = 0; i < empire.BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = empire.BuiltObjects[i];
                    if (builtObject != null && builtObject.NearestSystemStar == systemStar && builtObject.AttackRangeSquared > num2)
                    {
                        int num3 = builtObject.CalculateOverallStrengthFactor();
                        num += num3;
                        if (num3 > 0)
                        {
                            ships.Add(builtObject);
                        }
                    }
                }
            }
            return num;
        }

        private int DetermineBuiltObjectStrengthInSystem(Habitat systemStar, Empire empire, int unarmedStrength, ref BuiltObjectList ships)
        {
            int num = 0;
            if (empire != null && empire.BuiltObjects != null && systemStar != null)
            {
                for (int i = 0; i < empire.BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = empire.BuiltObjects[i];
                    if (builtObject != null && builtObject.NearestSystemStar == systemStar)
                    {
                        int firepowerRaw = builtObject.FirepowerRaw;
                        num += Math.Max(unarmedStrength, firepowerRaw);
                        if (firepowerRaw > unarmedStrength)
                        {
                            ships.Add(builtObject);
                        }
                    }
                }
            }
            if (unarmedStrength > 0 && empire != null && empire.PrivateBuiltObjects != null && systemStar != null)
            {
                for (int j = 0; j < empire.PrivateBuiltObjects.Count; j++)
                {
                    BuiltObject builtObject2 = empire.PrivateBuiltObjects[j];
                    if (builtObject2 != null && builtObject2.NearestSystemStar == systemStar)
                    {
                        int firepowerRaw2 = builtObject2.FirepowerRaw;
                        num += Math.Max(unarmedStrength, firepowerRaw2);
                        if (firepowerRaw2 > unarmedStrength)
                        {
                            ships.Add(builtObject2);
                        }
                    }
                }
            }
            return num;
        }

        private int DetermineBuiltObjectStrengthAtLocation(int x, int y, Empire empire, int unarmedStrength, ref BuiltObjectList ships)
        {
            int num = 0;
            if (empire != null)
            {
                int threatRange = ThreatRange;
                double num2 = (double)threatRange * (double)threatRange;
                if (empire.BuiltObjects != null)
                {
                    for (int i = 0; i < empire.BuiltObjects.Count; i++)
                    {
                        BuiltObject builtObject = empire.BuiltObjects[i];
                        if (builtObject != null && CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos) <= num2)
                        {
                            int firepowerRaw = builtObject.FirepowerRaw;
                            num += Math.Max(unarmedStrength, firepowerRaw);
                            if (firepowerRaw > unarmedStrength)
                            {
                                ships.Add(builtObject);
                            }
                        }
                    }
                }
                if (unarmedStrength > 0 && empire.PrivateBuiltObjects != null)
                {
                    for (int j = 0; j < empire.PrivateBuiltObjects.Count; j++)
                    {
                        BuiltObject builtObject2 = empire.PrivateBuiltObjects[j];
                        if (builtObject2 != null && CalculateDistanceSquared(x, y, builtObject2.Xpos, builtObject2.Ypos) <= num2)
                        {
                            int firepowerRaw2 = builtObject2.FirepowerRaw;
                            num += Math.Max(unarmedStrength, firepowerRaw2);
                            if (firepowerRaw2 > unarmedStrength)
                            {
                                ships.Add(builtObject2);
                            }
                        }
                    }
                }
            }
            return num;
        }

        public int DetermineDefendingBaseStrengthAtColony(Habitat colony)
        {
            int num = 0;
            if (colony != null && colony.Empire != null && colony.Empire != IndependentEmpire && colony.BasesAtHabitat != null)
            {
                for (int i = 0; i < colony.BasesAtHabitat.Count; i++)
                {
                    BuiltObject builtObject = colony.BasesAtHabitat[i];
                    if (builtObject != null)
                    {
                        num += builtObject.CalculateOverallStrengthFactor();
                    }
                }
            }
            return num;
        }

        public void DetermineColonyBaseInfo(Habitat colony, out bool hasSpacePort, out double happinessModifier)
        {
            hasSpacePort = false;
            happinessModifier = 0.0;
            if (colony.BasesAtHabitat == null || colony.BasesAtHabitat.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < colony.BasesAtHabitat.Count; i++)
            {
                BuiltObject builtObject = colony.BasesAtHabitat[i];
                if (builtObject != null)
                {
                    happinessModifier = Math.Max(happinessModifier, (double)(builtObject.MedicalCapacity + builtObject.RecreationCapacity) / 30.0);
                    if (builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort)
                    {
                        hasSpacePort = true;
                    }
                }
            }
        }

        public BuiltObject DetermineSpacePortAtColonyIncludingUnderConstruction(Habitat colony)
        {
            if (colony.BasesAtHabitat != null && colony.BasesAtHabitat.Count > 0)
            {
                for (int i = 0; i < colony.BasesAtHabitat.Count; i++)
                {
                    BuiltObject builtObject = colony.BasesAtHabitat[i];
                    if (builtObject != null && (builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort))
                    {
                        return builtObject;
                    }
                }
            }
            return null;
        }

        public BuiltObject DetermineSpacePortAtColony(Habitat colony)
        {
            if (colony.Empire != null && colony.Empire != IndependentEmpire)
            {
                for (int i = 0; i < colony.Empire.SpacePorts.Count; i++)
                {
                    BuiltObject builtObject = colony.Empire.SpacePorts[i];
                    if (builtObject != null && builtObject.ParentHabitat == colony)
                    {
                        return builtObject;
                    }
                }
            }
            return null;
        }

        public BuiltObject DetermineSpacePortAtHabitat(Habitat habitat)
        {
            if (habitat != null && habitat.BasesAtHabitat != null)
            {
                for (int i = 0; i < habitat.BasesAtHabitat.Count; i++)
                {
                    BuiltObject builtObject = habitat.BasesAtHabitat[i];
                    if (builtObject != null && builtObject.ParentHabitat == habitat && (builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort))
                    {
                        return builtObject;
                    }
                }
            }
            return null;
        }

        public BuiltObject DetermineNonMiningBaseAtHabitat(Habitat habitat)
        {
            for (int i = 0; i < habitat.BasesAtHabitat.Count; i++)
            {
                BuiltObject builtObject = habitat.BasesAtHabitat[i];
                if (builtObject != null && (builtObject.SubRole == BuiltObjectSubRole.GenericBase || builtObject.SubRole == BuiltObjectSubRole.EnergyResearchStation || builtObject.SubRole == BuiltObjectSubRole.WeaponsResearchStation || builtObject.SubRole == BuiltObjectSubRole.HighTechResearchStation || builtObject.SubRole == BuiltObjectSubRole.ResortBase || builtObject.SubRole == BuiltObjectSubRole.MonitoringStation || builtObject.SubRole == BuiltObjectSubRole.DefensiveBase))
                {
                    return builtObject;
                }
            }
            return null;
        }

        public BuiltObject DetermineMiningStationAtHabitatForEmpire(Habitat habitat, Empire empire)
        {
            for (int i = 0; i < habitat.BasesAtHabitat.Count; i++)
            {
                BuiltObject builtObject = habitat.BasesAtHabitat[i];
                if (builtObject == null || builtObject.Empire != empire)
                {
                    continue;
                }
                switch (builtObject.SubRole)
                {
                    case BuiltObjectSubRole.GasMiningStation:
                    case BuiltObjectSubRole.MiningStation:
                        return builtObject;
                    case BuiltObjectSubRole.SmallSpacePort:
                    case BuiltObjectSubRole.MediumSpacePort:
                    case BuiltObjectSubRole.LargeSpacePort:
                        if (builtObject.ExtractionGas > 0 || builtObject.ExtractionMine > 0)
                        {
                            return builtObject;
                        }
                        break;
                }
            }
            return null;
        }

        public bool CheckForeignBaseAtHabitat(Habitat habitat, Empire empire)
        {
            if (habitat != null && habitat.BasesAtHabitat != null && empire != null)
            {
                for (int i = 0; i < habitat.BasesAtHabitat.Count; i++)
                {
                    BuiltObject builtObject = habitat.BasesAtHabitat[i];
                    if (builtObject != null && builtObject.Empire != null && builtObject.Empire != empire)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckAlreadyHaveMiningStationAtHabitat(Habitat habitat, Empire empire)
        {
            if (habitat != null)
            {
                if (habitat.Category == HabitatCategoryType.GasCloud)
                {
                    if (DetermineMiningStationAtHabitatForEmpire(habitat, empire) == null)
                    {
                        return false;
                    }
                    return true;
                }
                if (DetermineMiningStationAtHabitat(habitat) == null)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public BuiltObject DetermineResortBaseAtHabitat(Habitat habitat)
        {
            for (int i = 0; i < habitat.BasesAtHabitat.Count; i++)
            {
                BuiltObject builtObject = habitat.BasesAtHabitat[i];
                if (builtObject.SubRole == BuiltObjectSubRole.ResortBase)
                {
                    return builtObject;
                }
            }
            return null;
        }

        public BuiltObject DetermineMiningStationAtHabitat(Habitat habitat)
        {
            for (int i = 0; i < habitat.BasesAtHabitat.Count; i++)
            {
                BuiltObject builtObject = habitat.BasesAtHabitat[i];
                switch (builtObject.SubRole)
                {
                    case BuiltObjectSubRole.GasMiningStation:
                    case BuiltObjectSubRole.MiningStation:
                        return builtObject;
                    case BuiltObjectSubRole.SmallSpacePort:
                    case BuiltObjectSubRole.MediumSpacePort:
                    case BuiltObjectSubRole.LargeSpacePort:
                        if (builtObject.ExtractionGas > 0 || builtObject.ExtractionMine > 0)
                        {
                            return builtObject;
                        }
                        break;
                }
            }
            return null;
        }

        public int CountResourceSourcesForEmpire(Empire empire, byte resourceId)
        {
            return CountResourceSourcesForEmpire(empire, resourceId, includeConstructionShipsBuildingMiningStations: false);
        }

        public int CountResourceSourcesForEmpire(Empire empire, byte resourceId, bool includeConstructionShipsBuildingMiningStations)
        {
            int num = 0;
            HabitatList habitatList = new HabitatList();
            if (empire != null)
            {
                if (empire.Colonies != null)
                {
                    for (int i = 0; i < empire.Colonies.Count; i++)
                    {
                        Habitat habitat = empire.Colonies[i];
                        if (habitat != null && habitat.Resources != null)
                        {
                            int num2 = habitat.Resources.IndexOf(resourceId, 0);
                            if (num2 >= 0)
                            {
                                habitatList.Add(habitat);
                                num++;
                            }
                        }
                    }
                }
                if (empire.MiningStations != null)
                {
                    for (int j = 0; j < empire.MiningStations.Count; j++)
                    {
                        BuiltObject builtObject = empire.MiningStations[j];
                        if (builtObject == null)
                        {
                            continue;
                        }
                        Habitat parentHabitat = builtObject.ParentHabitat;
                        if (parentHabitat != null && parentHabitat.Resources != null)
                        {
                            int num3 = parentHabitat.Resources.IndexOf(resourceId, 0);
                            if (num3 >= 0)
                            {
                                habitatList.Add(parentHabitat);
                                num++;
                            }
                        }
                    }
                }
                if (includeConstructionShipsBuildingMiningStations && empire.ConstructionShips != null)
                {
                    for (int k = 0; k < empire.ConstructionShips.Count; k++)
                    {
                        BuiltObject builtObject2 = empire.ConstructionShips[k];
                        if (builtObject2 == null)
                        {
                            continue;
                        }
                        BuiltObjectMission mission = builtObject2.Mission;
                        if (mission != null && mission.Type == BuiltObjectMissionType.Build && mission.TargetHabitat != null)
                        {
                            Habitat targetHabitat = mission.TargetHabitat;
                            if (targetHabitat != null && !habitatList.Contains(targetHabitat) && targetHabitat.Resources != null && targetHabitat.Resources.ContainsId(resourceId))
                            {
                                num++;
                            }
                        }
                    }
                }
            }
            return num;
        }

        public double CountResourceSupplyForGalaxy(byte resourceId)
        {
            double num = 0.0;
            EmpireList empireList = new EmpireList();
            empireList.AddRange(Empires);
            empireList.AddRange(PirateEmpires);
            for (int i = 0; i < empireList.Count; i++)
            {
                Empire empire = empireList[i];
                num += CountResourceSupplyForEmpire(empire, resourceId);
            }
            return num;
        }

        public double CountResourceSupplyForEmpire(Empire empire, byte resourceId)
        {
            double num = 0.0;
            Resource resource = new Resource(resourceId);
            for (int i = 0; i < empire.Colonies.Count; i++)
            {
                Habitat habitat = empire.Colonies[i];
                if (habitat.Cargo != null && habitat.Empire == empire && habitat.Cargo.GetExists(resource))
                {
                    int num2 = habitat.Cargo.IndexOf(resource, empire);
                    if (num2 >= 0)
                    {
                        num += (double)habitat.Cargo[num2].Available;
                    }
                }
            }
            for (int j = 0; j < empire.SpacePorts.Count; j++)
            {
                BuiltObject builtObject = empire.SpacePorts[j];
                if (builtObject.Cargo != null && (builtObject.ParentHabitat == null || builtObject.ParentHabitat.Empire != empire) && builtObject.Cargo.GetExists(resource))
                {
                    int num3 = builtObject.Cargo.IndexOf(resource, empire);
                    if (num3 >= 0)
                    {
                        num += (double)builtObject.Cargo[num3].Available;
                    }
                }
            }
            for (int k = 0; k < empire.MiningStations.Count; k++)
            {
                BuiltObject builtObject2 = empire.MiningStations[k];
                if (builtObject2.Cargo != null && builtObject2.Cargo.GetExists(resource))
                {
                    int num4 = builtObject2.Cargo.IndexOf(resource, empire);
                    if (num4 >= 0)
                    {
                        num += (double)builtObject2.Cargo[num4].Available;
                    }
                }
            }
            return num;
        }

        public static bool ConditionCheckLimit(bool condition, int maximumIterations, ref int iterationCount)
        {
            if (iterationCount >= maximumIterations)
            {
                return false;
            }
            iterationCount++;
            return condition;
        }

        public static StellarObjectList EnsureSingleStellarObjectPerSystem(StellarObjectList stellarObjects)
        {
            HabitatList habitatList = DetermineSystemsForStellarObjects(stellarObjects);
            List<(double value, StellarObject obj)> list = new List<(double value, StellarObject obj)>();
            for (int i = 0; i < habitatList.Count; i++)
            {
                Habitat systemStar = habitatList[i];
                StellarObjectList stellarObjectList = ExtractStellarObjectsForSystem(stellarObjects, systemStar);
                if (stellarObjectList.Count <= 1)
                {
                    continue;
                }
                for (int j = 0; j < stellarObjectList.Count; j++)
                {
                    StellarObject stellarObject = stellarObjectList[j];
                    if (stellarObject is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)stellarObject;
                        list.Add((builtObject.Size, builtObject));
                        //stellarObject.SortTag = builtObject.Size;
                    }
                    else if (stellarObject is Habitat)
                    {
                        Habitat habitat = (Habitat)stellarObject;
                        //stellarObject.SortTag = Math.Max((double)habitat.StrategicValue / 10.0, habitat.Size);
                        list.Add((Math.Max((double)habitat.StrategicValue / 10.0, habitat.Size), stellarObject));
                    }
                    else
                    {
                        //stellarObject.SortTag = 1.0;
                        list.Add((1.0, stellarObject));
                    }
                }
                //StellarObject.SortStellarObject comparer = new StellarObject.SortStellarObject();
                //stellarObjectList.Sort(comparer);
                //stellarObjectList.Reverse();
                list.Sort((x, y) => x.value.CompareTo(y.value));
                list.Reverse();
                if (stellarObjectList.Count > 1)
                {
                    for (int k = 1; k < list.Count; k++)
                    {
                        stellarObjects.Remove(list[k].obj);
                    }

                }
            }
            return stellarObjects;
        }

        public static StellarObjectList ExtractStellarObjectsForSystem(StellarObjectList stellarObjects, Habitat systemStar)
        {
            StellarObjectList stellarObjectList = new StellarObjectList();
            for (int i = 0; i < stellarObjects.Count; i++)
            {
                StellarObject stellarObject = stellarObjects[i];
                Habitat habitat = DetermineHabitatSystemStarForStellarObject(stellarObject);
                if (habitat == systemStar)
                {
                    stellarObjectList.Add(stellarObject);
                }
            }
            return stellarObjectList;
        }

        public static StellarObjectList RemoveObjectsWithSystemStar(StellarObjectList stellarObjects, Habitat systemStar)
        {
            StellarObjectList stellarObjectList = new StellarObjectList();
            for (int i = 0; i < stellarObjects.Count; i++)
            {
                Habitat habitat = DetermineHabitatSystemStarForStellarObject(stellarObjects[i]);
                if (habitat != systemStar)
                {
                    stellarObjectList.Add(stellarObjects[i]);
                }
            }
            return stellarObjectList;
        }

        public static HabitatList DetermineSystemsForStellarObjects(StellarObjectList stellarObjects)
        {
            HabitatList habitatList = new HabitatList();
            for (int i = 0; i < stellarObjects.Count; i++)
            {
                Habitat habitat = DetermineHabitatSystemStarForStellarObject(stellarObjects[i]);
                if (habitat != null && !habitatList.Contains(habitat))
                {
                    habitatList.Add(habitat);
                }
            }
            return habitatList;
        }

        public static Habitat DetermineHabitatSystemStarForStellarObject(StellarObject stellarObject)
        {
            Habitat result = null;
            if (stellarObject is Habitat)
            {
                result = DetermineHabitatSystemStar((Habitat)stellarObject);
            }
            else if (stellarObject is BuiltObject)
            {
                result = ((BuiltObject)stellarObject).NearestSystemStar;
            }
            return result;
        }

        public static Habitat DetermineHabitatSystemStar(Habitat habitat)
        {
            Habitat result = null;
            if (habitat != null)
            {
                switch (habitat.Category)
                {
                    case HabitatCategoryType.Planet:
                    case HabitatCategoryType.Asteroid:
                        result = habitat.Parent;
                        break;
                    case HabitatCategoryType.Moon:
                        result = habitat.Parent.Parent;
                        break;
                    case HabitatCategoryType.Star:
                    case HabitatCategoryType.GasCloud:
                        result = habitat;
                        break;
                    default:
                        result = null;
                        break;
                }
            }
            return result;
        }

        public BuiltObject FastFindBestConstructionShip(double x, double y, Empire empire)
        {
            BuiltObject result = null;
            double num = double.MaxValue;
            if (empire != null)
            {
                for (int i = 0; i < empire.ConstructionShips.Count; i++)
                {
                    BuiltObject builtObject = empire.ConstructionShips[i];
                    if (builtObject == null || !builtObject.IsShipYard)
                    {
                        continue;
                    }
                    double num2 = CalculateDistance(x, y, builtObject.Xpos, builtObject.Ypos);
                    double num3 = 1000.0;
                    if (builtObject.ConstructionQueue != null && builtObject.ConstructionQueue.ConstructionWaitQueue != null)
                    {
                        num3 = 100.0;
                        if (builtObject.Mission != null && builtObject.Mission.Type != 0)
                        {
                            num3 += 500.0;
                            if (builtObject.SubsequentMissions != null && builtObject.SubsequentMissions.Count > 0)
                            {
                                num3 += (double)builtObject.SubsequentMissions.Count * 500.0;
                            }
                            if (builtObject.ConstructionQueue.ConstructionWaitQueue.Count > 0)
                            {
                                num3 += (double)builtObject.ConstructionQueue.ConstructionWaitQueue.Count * 500.0;
                            }
                        }
                    }
                    double num4 = num3 * num2;
                    if (num4 < num)
                    {
                        result = builtObject;
                        num = num4;
                    }
                }
            }
            return result;
        }

        public bool CheckWithinDistancePotential(double distance, double x1, double y1, double x2, double y2)
        {
            distance += distance;
            if (Math.Abs(x1 - x2) < distance || Math.Abs(y1 - y2) < distance)
            {
                return true;
            }
            return false;
        }

        public bool CheckWithinDistancePotentialUnmodified(double distance, double x1, double y1, double x2, double y2)
        {
            if (Math.Abs(x1 - x2) < distance || Math.Abs(y1 - y2) < distance)
            {
                return true;
            }
            return false;
        }

        public BuiltObject FindBuiltObjectWithScanRange(double x, double y, Empire empire)
        {
            for (int i = 0; i < empire.BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = empire.BuiltObjects[i];
                if (builtObject != null && builtObject.CurrentSpeed <= (float)builtObject.TopSpeed && CheckWithinDistancePotentialUnmodified(Math.Max(ThreatRange, builtObject.SensorProximityArrayRange), x, y, builtObject.Xpos, builtObject.Ypos))
                {
                    double num = CalculateDistance(x, y, builtObject.Xpos, builtObject.Ypos);
                    if ((int)num <= Math.Max(ThreatRange, builtObject.SensorProximityArrayRange))
                    {
                        return builtObject;
                    }
                }
            }
            for (int j = 0; j < empire.PrivateBuiltObjects.Count; j++)
            {
                BuiltObject builtObject2 = empire.PrivateBuiltObjects[j];
                if (builtObject2 != null && builtObject2.CurrentSpeed <= (float)builtObject2.TopSpeed && CheckWithinDistancePotentialUnmodified(Math.Max(ThreatRange, builtObject2.SensorProximityArrayRange), x, y, builtObject2.Xpos, builtObject2.Ypos))
                {
                    double num2 = CalculateDistance(x, y, builtObject2.Xpos, builtObject2.Ypos);
                    if ((int)num2 <= Math.Max(ThreatRange, builtObject2.SensorProximityArrayRange))
                    {
                        return builtObject2;
                    }
                }
            }
            return null;
        }

        public BuiltObject FindNearestBuiltObject(int x, int y, Empire empire)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            GalaxyIndex galaxyIndex = ResolveIndex(x, y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, x, y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    BuiltObject builtObject = FindNearestBuiltObjectInIndex(x, y, index, out distance, empire);
                    if (distance < num)
                    {
                        result = builtObject;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        public double CalculateDistanceSquared(double x1, double y1, double x2, double y2)
        {
            double num = x1 - x2;
            double num2 = y1 - y2;
            return num2 * num2 + num * num;
        }

        private BuiltObject FindNearestBuiltObjectInIndex(int x, int y, GalaxyIndex index, out double distance, Empire empire)
        {
            BuiltObject builtObject = null;
            BuiltObjectList builtObjectList = BuiltObjectIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < builtObjectList.Count; i++)
            {
                BuiltObject builtObject2 = builtObjectList[i];
                if (builtObject2 == null)
                {
                    continue;
                }
                double num = CalculateDistanceSquared(x, y, builtObject2.Xpos, builtObject2.Ypos);
                if (!(num < distance))
                {
                    continue;
                }
                if (empire != null)
                {
                    if (builtObject2.Empire == empire)
                    {
                        builtObject = builtObject2;
                        distance = num;
                    }
                }
                else
                {
                    builtObject = builtObject2;
                    distance = num;
                }
            }
            if (builtObject != null)
            {
                distance = CalculateDistance(x, y, builtObject.Xpos, builtObject.Ypos);
            }
            return builtObject;
        }

        public BuiltObject FindNearestBuiltObject(int x, int y, BuiltObjectRole role)
        {
            return FindNearestBuiltObject(x, y, role, includeIndependentBuiltObjects: true);
        }

        public BuiltObject FindNearestBuiltObject(int x, int y, BuiltObjectRole role, bool includeIndependentBuiltObjects)
        {
            return FindNearestBuiltObject(x, y, role, includeIndependentBuiltObjects, null);
        }

        public BuiltObject FindNearestBuiltObject(int x, int y, BuiltObjectRole role, bool includeIndependentBuiltObjects, Empire empireToExclude)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            GalaxyIndex galaxyIndex = ResolveIndex(x, y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, x, y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    BuiltObject builtObject = FindNearestBuiltObjectInIndex(x, y, index, out distance, role, includeIndependentBuiltObjects, empireToExclude);
                    if (distance < num)
                    {
                        result = builtObject;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private BuiltObject FindNearestBuiltObjectInIndex(int x, int y, GalaxyIndex index, out double distance, BuiltObjectRole role, bool includeIndependentBuiltObjects, Empire empireToExclude)
        {
            BuiltObject builtObject = null;
            BuiltObjectList builtObjectList = BuiltObjectIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < builtObjectList.Count; i++)
            {
                BuiltObject builtObject2 = builtObjectList[i];
                if (builtObject2 == null || (!includeIndependentBuiltObjects && builtObject2.Empire == IndependentEmpire) || (empireToExclude != null && builtObject2.Empire == empireToExclude))
                {
                    continue;
                }
                double num = CalculateDistanceSquared(x, y, builtObject2.Xpos, builtObject2.Ypos);
                if (!(num < distance))
                {
                    continue;
                }
                if (role != 0)
                {
                    if (builtObject2.Role == role)
                    {
                        builtObject = builtObject2;
                        distance = num;
                    }
                }
                else
                {
                    builtObject = builtObject2;
                    distance = num;
                }
            }
            if (builtObject != null)
            {
                distance = CalculateDistance(x, y, builtObject.Xpos, builtObject.Ypos);
            }
            return builtObject;
        }

        public BuiltObject FindNearestKnownBase(Empire requester, double x, double y, Empire targetEmpire, int maximumOverallStrength)
        {
            BuiltObject result = null;
            if (requester != null && targetEmpire != null)
            {
                BuiltObject builtObject = null;
                BuiltObject builtObject2 = null;
                BuiltObject builtObject3 = null;
                BuiltObject builtObject4 = null;
                double nearestDistanceSquared = double.MaxValue;
                double nearestDistanceSquared2 = double.MaxValue;
                double nearestDistanceSquared3 = double.MaxValue;
                double nearestDistanceSquared4 = double.MaxValue;
                if (targetEmpire.SpacePorts != null)
                {
                    builtObject = FindNearestKnownBaseInSet(requester, x, y, targetEmpire.SpacePorts, out nearestDistanceSquared, maximumOverallStrength);
                }
                if (targetEmpire.MiningStations != null)
                {
                    builtObject2 = FindNearestKnownBaseInSet(requester, x, y, targetEmpire.MiningStations, out nearestDistanceSquared2, maximumOverallStrength);
                }
                if (targetEmpire.ResortBases != null)
                {
                    builtObject3 = FindNearestKnownBaseInSet(requester, x, y, targetEmpire.ResortBases, out nearestDistanceSquared3, maximumOverallStrength);
                }
                if (targetEmpire.ResearchFacilities != null)
                {
                    builtObject4 = FindNearestKnownBaseInSet(requester, x, y, targetEmpire.ResearchFacilities, out nearestDistanceSquared4, maximumOverallStrength);
                }
                if (builtObject != null && nearestDistanceSquared < nearestDistanceSquared2 && nearestDistanceSquared < nearestDistanceSquared3 && nearestDistanceSquared < nearestDistanceSquared4)
                {
                    result = builtObject;
                }
                else if (builtObject2 != null && nearestDistanceSquared2 < nearestDistanceSquared && nearestDistanceSquared2 < nearestDistanceSquared3 && nearestDistanceSquared2 < nearestDistanceSquared4)
                {
                    result = builtObject2;
                }
                else if (builtObject3 != null && nearestDistanceSquared3 < nearestDistanceSquared && nearestDistanceSquared3 < nearestDistanceSquared2 && nearestDistanceSquared3 < nearestDistanceSquared4)
                {
                    result = builtObject3;
                }
                else if (builtObject4 != null && nearestDistanceSquared4 < nearestDistanceSquared && nearestDistanceSquared4 < nearestDistanceSquared2 && nearestDistanceSquared4 < nearestDistanceSquared3)
                {
                    result = builtObject4;
                }
            }
            return result;
        }

        public BuiltObject FindNearestKnownBaseInSet(Empire requester, double x, double y, BuiltObjectList builtObjects, int maximumOverallStrength)
        {
            double nearestDistanceSquared = 0.0;
            return FindNearestKnownBaseInSet(requester, x, y, builtObjects, out nearestDistanceSquared, maximumOverallStrength);
        }

        public BuiltObject FindNearestKnownBaseInSet(Empire requester, double x, double y, BuiltObjectList builtObjects, out double nearestDistanceSquared, int maximumOverallStrength)
        {
            BuiltObject builtObject = null;
            nearestDistanceSquared = double.MaxValue;
            if (requester != null && builtObjects != null)
            {
                for (int i = 0; i < builtObjects.Count; i++)
                {
                    BuiltObject builtObject2 = builtObjects[i];
                    if (builtObject2 == null || builtObject2.HasBeenDestroyed || builtObject2.CalculateOverallStrengthFactor() > maximumOverallStrength)
                    {
                        continue;
                    }
                    double num = CalculateDistanceSquared(x, y, builtObject2.Xpos, builtObject2.Ypos);
                    if (builtObject != null && !(num < nearestDistanceSquared))
                    {
                        continue;
                    }
                    if (builtObject2.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject2.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject2.SubRole == BuiltObjectSubRole.LargeSpacePort)
                    {
                        if (requester.IsObjectAreaKnownToThisEmpire(builtObject2))
                        {
                            builtObject = builtObject2;
                            nearestDistanceSquared = num;
                        }
                    }
                    else if (requester.IsObjectVisibleToThisEmpire(builtObject2, includeLongRangeScanners: true, includeShipsOutsideSystems: false))
                    {
                        builtObject = builtObject2;
                        nearestDistanceSquared = num;
                    }
                }
            }
            return builtObject;
        }

        public BuiltObject FindNearestKnownBaseOfEmpireForPirateAttack(Empire attackingPirateEmpire, double x, double y, Empire targetEmpire)
        {
            return FindNearestKnownBaseOfEmpireForPirateAttack(attackingPirateEmpire, x, y, targetEmpire, int.MaxValue);
        }

        public BuiltObject FindNearestKnownBaseOfEmpireForPirateAttack(Empire attackingPirateEmpire, double x, double y, Empire targetEmpire, int attackStrength)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    BuiltObject builtObject = FindNearestKnownBaseOfEmpireForPirateAttackInIndex(x, y, index, out distance, attackingPirateEmpire, targetEmpire, attackStrength);
                    if (distance < num)
                    {
                        result = builtObject;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private BuiltObject FindNearestKnownBaseOfEmpireForPirateAttackInIndex(double x, double y, GalaxyIndex index, out double distance, Empire attackingEmpire, Empire targetEmpire, int attackStrength)
        {
            BuiltObject builtObject = null;
            BuiltObjectList builtObjectList = BuiltObjectIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < builtObjectList.Count; i++)
            {
                BuiltObject builtObject2 = builtObjectList[i];
                if (builtObject2 == null || builtObject2.HasBeenDestroyed || builtObject2.Role != BuiltObjectRole.Base || builtObject2.Empire != targetEmpire)
                {
                    continue;
                }
                double num = CalculateDistanceSquared(x, y, builtObject2.Xpos, builtObject2.Ypos);
                if (num < distance && attackingEmpire.IsObjectVisibleToThisEmpire(builtObject2, includeLongRangeScanners: true, includeShipsOutsideSystems: false))
                {
                    int num2 = 0;
                    if (attackStrength < int.MaxValue)
                    {
                        num2 = attackingEmpire.CalculateDefendingStrength(builtObject2);
                    }
                    if (attackStrength >= num2)
                    {
                        builtObject = builtObject2;
                        distance = num;
                    }
                }
            }
            if (builtObject != null)
            {
                distance = CalculateDistance(x, y, builtObject.Xpos, builtObject.Ypos);
            }
            return builtObject;
        }

        public BuiltObject FindNearestKnownBaseForPirateAttack(Empire attackingPirateEmpire, double x, double y)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            EmpireList empireList = attackingPirateEmpire.PirateRelations.ResolveEmpiresWithProtection();
            if (!empireList.Contains(attackingPirateEmpire))
            {
                empireList.Add(attackingPirateEmpire);
            }
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    BuiltObject builtObject = FindNearestKnownBaseForPirateAttackInIndex(x, y, index, out distance, attackingPirateEmpire, empireList);
                    if (distance < num)
                    {
                        result = builtObject;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private BuiltObject FindNearestKnownBaseForPirateAttackInIndex(double x, double y, GalaxyIndex index, out double distance, Empire attackingEmpire, EmpireList empiresToExclude)
        {
            BuiltObject builtObject = null;
            BuiltObject[] array = ListHelper.ToArrayThreadSafe(BuiltObjectIndex[index.X][index.Y]);
            distance = double.MaxValue;
            foreach (BuiltObject builtObject2 in array)
            {
                if (builtObject2 == null || builtObject2.HasBeenDestroyed || builtObject2.Role != BuiltObjectRole.Base || empiresToExclude.Contains(builtObject2.Empire) || builtObject2.Empire == IndependentEmpire || builtObject2.Empire == null)
                {
                    continue;
                }
                double num = CalculateDistanceSquared(x, y, builtObject2.Xpos, builtObject2.Ypos);
                if (!(num < distance) || !attackingEmpire.IsObjectVisibleToThisEmpire(builtObject2, includeLongRangeScanners: true, includeShipsOutsideSystems: false))
                {
                    continue;
                }
                EmpireActivity firstByTargetAndType = attackingEmpire.PirateMissions.GetFirstByTargetAndType(builtObject2, EmpireActivityType.Defend);
                if (firstByTargetAndType != null)
                {
                    continue;
                }
                bool flag = true;
                if (builtObject2.Empire != null && builtObject2.Empire != attackingEmpire && attackingEmpire.PirateEmpireBaseHabitat != null)
                {
                    PirateRelation pirateRelation = attackingEmpire.ObtainPirateRelation(builtObject2.Empire);
                    if (pirateRelation.Type == PirateRelationType.Protection)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    builtObject = builtObject2;
                    distance = num;
                }
            }
            if (builtObject != null)
            {
                distance = CalculateDistance(x, y, builtObject.Xpos, builtObject.Ypos);
            }
            return builtObject;
        }

        public BuiltObject FindNearestBaseForPirateAttack(double x, double y, Empire empireToExclude)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    BuiltObject builtObject = FindNearestBaseForPirateAttackInIndex(x, y, index, out distance, empireToExclude);
                    if (distance < num)
                    {
                        result = builtObject;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private BuiltObject FindNearestBaseForPirateAttackInIndex(double x, double y, GalaxyIndex index, out double distance, Empire empireToExclude)
        {
            BuiltObject builtObject = null;
            BuiltObjectList builtObjectList = BuiltObjectIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < builtObjectList.Count; i++)
            {
                BuiltObject builtObject2 = builtObjectList[i];
                if (builtObject2 != null && builtObject2.Role == BuiltObjectRole.Base && builtObject2.Empire != empireToExclude && builtObject2.Empire != IndependentEmpire && builtObject2.Empire != null)
                {
                    double num = CalculateDistanceSquared(x, y, builtObject2.Xpos, builtObject2.Ypos);
                    if (num < distance)
                    {
                        builtObject = builtObject2;
                        distance = num;
                    }
                }
            }
            if (builtObject != null)
            {
                distance = CalculateDistance(x, y, builtObject.Xpos, builtObject.Ypos);
            }
            return builtObject;
        }

        public StellarObject FindNearestStationaryStellarObject(double x, double y, Empire empire)
        {
            if (empire != null)
            {
                double num = double.MaxValue;
                double num2 = double.MaxValue;
                double num3 = double.MaxValue;
                BuiltObject builtObject = null;
                BuiltObject builtObject2 = null;
                Habitat habitat = FindNearestColony(x, y, empire, 0, includeIndependentColonies: false);
                if (habitat != null)
                {
                    num3 = CalculateDistanceSquared(x, y, habitat.Xpos, habitat.Ypos);
                }
                if (empire.BuiltObjects != null && empire.PrivateBuiltObjects != null)
                {
                    List<BuiltObjectRole> list = new List<BuiltObjectRole>();
                    list.Add(BuiltObjectRole.Base);
                    List<BuiltObjectRole> roles = list;
                    BuiltObjectList builtObjectsByRole = empire.BuiltObjects.GetBuiltObjectsByRole(roles);
                    BuiltObjectList builtObjectsByRole2 = empire.PrivateBuiltObjects.GetBuiltObjectsByRole(roles);
                    builtObject = FindNearestBuiltObjectInSet(x, y, builtObjectsByRole);
                    if (builtObject != null)
                    {
                        num = CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                    }
                    builtObject2 = FindNearestBuiltObjectInSet(x, y, builtObjectsByRole2);
                    if (builtObject2 != null)
                    {
                        num2 = CalculateDistanceSquared(x, y, builtObject2.Xpos, builtObject2.Ypos);
                    }
                }
                if (num3 < num2 && num3 < num)
                {
                    return habitat;
                }
                if (num2 < num3 && num2 < num)
                {
                    return builtObject2;
                }
                if (num < num3 && num < num2)
                {
                    return builtObject;
                }
            }
            return null;
        }

        public BuiltObject FindNearestBuiltObject(int x, int y, Empire empire, BuiltObjectSubRole subRole, bool fullyFunctional)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            GalaxyIndex galaxyIndex = ResolveIndex(x, y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, x, y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    BuiltObject builtObject = FindNearestBuiltObjectInIndex(x, y, index, out distance, empire, subRole, fullyFunctional);
                    if (distance < num)
                    {
                        result = builtObject;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private BuiltObject FindNearestBuiltObjectInIndex(int x, int y, GalaxyIndex index, out double distance, Empire empire, BuiltObjectSubRole subRole, bool fullyFunctional)
        {
            BuiltObject builtObject = null;
            BuiltObjectList builtObjectList = BuiltObjectIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < builtObjectList.Count; i++)
            {
                BuiltObject builtObject2 = builtObjectList[i];
                if (builtObject2 == null)
                {
                    continue;
                }
                double num = CalculateDistanceSquared(x, y, builtObject2.Xpos, builtObject2.Ypos);
                if (!(num < distance))
                {
                    continue;
                }
                bool flag = true;
                if (fullyFunctional && (builtObject2.BuiltAt != null || builtObject2.UnbuiltOrDamagedComponentCount > 0))
                {
                    flag = false;
                }
                if (!flag)
                {
                    continue;
                }
                bool flag2 = true;
                if (empire != null && builtObject2.Empire != empire)
                {
                    flag2 = false;
                }
                if (!flag2)
                {
                    continue;
                }
                if (subRole != 0)
                {
                    if (builtObject2.SubRole == subRole)
                    {
                        builtObject = builtObject2;
                        distance = num;
                    }
                }
                else
                {
                    builtObject = builtObject2;
                    distance = num;
                }
            }
            if (builtObject != null)
            {
                distance = CalculateDistance(x, y, builtObject.Xpos, builtObject.Ypos);
            }
            return builtObject;
        }

        public BuiltObject FindNearestBuiltObject(int x, int y, BuiltObjectSubRole subRole, bool includeSecondaryEmpires)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            GalaxyIndex galaxyIndex = ResolveIndex(x, y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, x, y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    BuiltObject builtObject = FindNearestBuiltObjectInIndex(x, y, index, out distance, subRole, includeSecondaryEmpires);
                    if (distance < num)
                    {
                        result = builtObject;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private BuiltObject FindNearestBuiltObjectInIndex(int x, int y, GalaxyIndex index, out double distance, BuiltObjectSubRole subRole, bool includeSecondaryEmpires)
        {
            BuiltObject builtObject = null;
            BuiltObject[] array = ListHelper.ToArrayThreadSafe(BuiltObjectIndex[index.X][index.Y]);
            distance = double.MaxValue;
            if (array != null)
            {
                foreach (BuiltObject builtObject2 in array)
                {
                    if (builtObject2 == null)
                    {
                        continue;
                    }
                    double num = CalculateDistanceSquared(x, y, builtObject2.Xpos, builtObject2.Ypos);
                    if (!(num < distance))
                    {
                        continue;
                    }
                    bool flag = true;
                    if (!includeSecondaryEmpires && (builtObject2.Empire == null || builtObject2.Empire == IndependentEmpire || builtObject2.Empire.PirateEmpireBaseHabitat != null))
                    {
                        flag = false;
                    }
                    if (!flag)
                    {
                        continue;
                    }
                    if (subRole != 0)
                    {
                        if (builtObject2.SubRole == subRole)
                        {
                            builtObject = builtObject2;
                            distance = num;
                        }
                    }
                    else
                    {
                        builtObject = builtObject2;
                        distance = num;
                    }
                }
            }
            if (builtObject != null)
            {
                distance = CalculateDistance(x, y, builtObject.Xpos, builtObject.Ypos);
            }
            return builtObject;
        }

        public string ResolveSectorDescription(double x, double y)
        {
            Sector sector = ResolveSector(x, y);
            return ResolveSectorDescription(sector);
        }

        public static string ResolveSectorDescriptionStatic(double x, double y)
        {
            Sector sector = ResolveSectorStatic((int)x, (int)y);
            return ResolveSectorDescription(sector);
        }

        public static string ResolveSectorDescription(Sector sector)
        {
            string text = ((char)(sector.X + 65)).ToString();
            return text + (sector.Y + 1);
        }

        public GalaxyIndex ResolveIndex(double x, double y)
        {
            return ResolveIndex((int)x, (int)y);
        }

        public GalaxyIndex ResolveIndex(int x, int y)
        {
            int x2 = x / IndexSize;
            int y2 = y / IndexSize;
            CorrectIndexCoords(ref x2, ref y2);
            return new GalaxyIndex(x2, y2);
        }

        public Sector ResolveSector(double x, double y)
        {
            return ResolveSector((int)x, (int)y);
        }

        public Sector ResolveSector(int x, int y)
        {
            int x2 = x / SectorSize;
            int y2 = y / SectorSize;
            CorrectSectorCoords(ref x2, ref y2);
            return new Sector(x2, y2);
        }

        public static Sector ResolveSectorStatic(int x, int y)
        {
            int x2 = x / SectorSize;
            int y2 = y / SectorSize;
            CorrectSectorCoords(ref x2, ref y2);
            return new Sector(x2, y2);
        }

        public Habitat FindNearestUncolonizedExploredSystem(double x, double y, Empire empire)
        {
            double num = double.MaxValue;
            Habitat result = null;
            for (int i = 0; i < empire.SystemVisibility.Count; i++)
            {
                SystemVisibility systemVisibility = empire.SystemVisibility[i];
                if (empire.CheckSystemVisibilityStatus(systemVisibility.SystemStar.SystemIndex) == SystemVisibilityStatus.Explored && Systems[systemVisibility.SystemStar.SystemIndex].DominantEmpire == null)
                {
                    double num2 = CalculateDistanceSquared(x, y, systemVisibility.SystemStar.Xpos, systemVisibility.SystemStar.Ypos);
                    if (num2 < num)
                    {
                        result = systemVisibility.SystemStar;
                        num = num2;
                    }
                }
            }
            return result;
        }

        public Habitat FindNearestLonelyHabitat(double x, double y, Empire empire)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    Habitat habitat = FindNearestLonelyHabitatInIndex((int)x, (int)y, empire, index, out distance);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private Habitat FindNearestLonelyHabitatInIndex(int x, int y, Empire empire, GalaxyIndex index, out double distance)
        {
            Habitat habitat = null;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            distance = double.MaxValue;
            int num = -1;
            bool flag = false;
            for (int i = 0; i < habitatList.Count; i++)
            {
                if (habitatList[i].Category != 0 && habitatList[i].Category != HabitatCategoryType.Planet && habitatList[i].Category != HabitatCategoryType.Moon)
                {
                    continue;
                }
                if (num != habitatList[i].SystemIndex)
                {
                    EmpireSystemSummary dominantEmpire = Systems[habitatList[i].SystemIndex].DominantEmpire;
                    flag = ((dominantEmpire != null && dominantEmpire.Empire != null) ? true : false);
                    num = habitatList[i].SystemIndex;
                }
                if (flag)
                {
                    continue;
                }
                int num2 = CheckEmpireTerritoryIdAtLocation(x, y);
                if ((num2 >= 0 && num2 != empire.EmpireId) || (habitatList[i].Empire != null && habitatList[i].Empire != IndependentEmpire && habitatList[i].Empire != empire))
                {
                    continue;
                }
                double num3 = CalculateDistanceSquared(x, y, habitatList[i].Xpos, habitatList[i].Ypos);
                if (!(num3 < distance))
                {
                    continue;
                }
                BuiltObject builtObject = FindNearestBuiltObject((int)Systems[habitatList[i].SystemIndex].SystemStar.Xpos, (int)Systems[habitatList[i].SystemIndex].SystemStar.Ypos, null);
                if (builtObject != null && builtObject.Empire != null)
                {
                    double num4 = CalculateDistance(Systems[habitatList[i].SystemIndex].SystemStar.Xpos, Systems[habitatList[i].SystemIndex].SystemStar.Ypos, builtObject.Xpos, builtObject.Ypos);
                    if (num4 < (double)MaxSolarSystemSize * 2.1)
                    {
                        continue;
                    }
                }
                habitat = habitatList[i];
                distance = num3;
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        public Habitat FindNearestHabitatEmptySystem(double x, double y)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    Habitat habitat = FindNearestHabitatEmptySystemInIndex((int)x, (int)y, index, out distance);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        public Habitat FindNearestHabitatEmptySystem(double x, double y, HabitatType type)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    Habitat habitat = FindNearestHabitatEmptySystemInIndex((int)x, (int)y, type, index, out distance);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private Habitat FindNearestHabitatEmptySystemInIndex(int x, int y, HabitatType type, GalaxyIndex index, out double distance)
        {
            Habitat habitat = null;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            distance = double.MaxValue;
            int num = -1;
            bool flag = false;
            for (int i = 0; i < habitatList.Count; i++)
            {
                if (habitatList[i].Category != HabitatCategoryType.Planet && habitatList[i].Category != HabitatCategoryType.Moon)
                {
                    continue;
                }
                if (num != habitatList[i].SystemIndex)
                {
                    EmpireSystemSummary dominantEmpire = Systems[habitatList[i].SystemIndex].DominantEmpire;
                    flag = ((dominantEmpire != null && dominantEmpire.Empire != null) ? true : false);
                    num = habitatList[i].SystemIndex;
                }
                if (flag || (habitatList[i].Empire != null && habitatList[i].Empire != IndependentEmpire))
                {
                    continue;
                }
                double num2 = CalculateDistanceSquared(x, y, habitatList[i].Xpos, habitatList[i].Ypos);
                if (!(num2 < distance))
                {
                    continue;
                }
                BuiltObject builtObject = FindNearestBuiltObject((int)Systems[habitatList[i].SystemIndex].SystemStar.Xpos, (int)Systems[habitatList[i].SystemIndex].SystemStar.Ypos, null);
                if (builtObject != null && builtObject.Empire != null)
                {
                    double num3 = CalculateDistance(Systems[habitatList[i].SystemIndex].SystemStar.Xpos, Systems[habitatList[i].SystemIndex].SystemStar.Ypos, builtObject.Xpos, builtObject.Ypos);
                    if (num3 < (double)MaxSolarSystemSize * 2.1)
                    {
                        continue;
                    }
                }
                habitat = habitatList[i];
                distance = num2;
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        public Habitat FindNearestHabitatUnoccupiedSystemWithResourceNotVisibleToPlayer(double x, double y, byte resourceId)
        {
            SystemInfoDistanceList systemInfoDistanceList = GenerateDistanceOrderedSystemList(x, y);
            for (int i = 0; i < systemInfoDistanceList.Count; i++)
            {
                if ((systemInfoDistanceList[i].SystemInfo.DominantEmpire != null && systemInfoDistanceList[i].SystemInfo.DominantEmpire.Empire != null) || systemInfoDistanceList[i].SystemInfo.SystemStar == null)
                {
                    continue;
                }
                Empire empire = CheckSystemOwnership(systemInfoDistanceList[i].SystemInfo.SystemStar);
                if (empire != null || PlayerEmpire.CheckSystemVisible(systemInfoDistanceList[i].SystemInfo.SystemStar.SystemIndex))
                {
                    continue;
                }
                for (int j = 0; j < systemInfoDistanceList[i].SystemInfo.Habitats.Count; j++)
                {
                    if (systemInfoDistanceList[i].SystemInfo.Habitats[j].Resources != null && systemInfoDistanceList[i].SystemInfo.Habitats[j].Resources.IndexOf(resourceId, 0) >= 0)
                    {
                        return systemInfoDistanceList[i].SystemInfo.Habitats[j];
                    }
                }
            }
            return null;
        }

        public Habitat FindNearestHabitatUnoccupiedSystem(double x, double y, HabitatType type)
        {
            SystemInfoDistanceList systemInfoDistanceList = GenerateDistanceOrderedSystemList(x, y);
            for (int i = 0; i < systemInfoDistanceList.Count; i++)
            {
                if (systemInfoDistanceList[i].SystemInfo.DominantEmpire != null && systemInfoDistanceList[i].SystemInfo.DominantEmpire.Empire != null)
                {
                    continue;
                }
                for (int j = 0; j < systemInfoDistanceList[i].SystemInfo.Habitats.Count; j++)
                {
                    if (systemInfoDistanceList[i].SystemInfo.Habitats[j].Type == type)
                    {
                        return systemInfoDistanceList[i].SystemInfo.Habitats[j];
                    }
                }
            }
            return null;
        }

        public Habitat FindNearestColonizableHabitatEmptySystem(double x, double y, Empire empire)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            Design design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
            if (design == null)
            {
                return null;
            }
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    Habitat habitat = FindNearestColonizableHabitatEmptySystemInIndex((int)x, (int)y, index, out distance, empire, design);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        public Habitat FindNearestColonizableHabitatUnoccupiedSystem(double x, double y, Empire empire)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            Design design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
            List<HabitatType> colonizableHabitatTypes = empire.ColonizableHabitatTypesForEmpire(empire);
            if (design == null)
            {
                return null;
            }
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    Habitat habitat = FindNearestColonizableHabitatUnoccupiedSystemInIndex((int)x, (int)y, index, out distance, empire, design, colonizableHabitatTypes);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        public Habitat FindNearestColonizableHabitat(double x, double y, Empire empire)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            Design design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
            if (design == null)
            {
                return null;
            }
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    Habitat habitat = FindNearestColonizableHabitatInIndex((int)x, (int)y, index, out distance, empire, design);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        public Habitat FastFindNearestIndependentHabitat(double x, double y)
        {
            double num = double.MaxValue;
            Habitat result = null;
            for (int i = 0; i < IndependentColonies.Count; i++)
            {
                Habitat habitat = IndependentColonies[i];
                double num2 = CalculateDistanceSquared(x, y, habitat.Xpos, habitat.Ypos);
                if (num2 < num)
                {
                    result = habitat;
                    num = num2;
                }
            }
            return result;
        }

        public Habitat FindNearestIndependentHabitat(double x, double y)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    Habitat habitat = FindNearestIndependentHabitatInIndex((int)x, (int)y, index, out distance);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private Habitat FindNearestIndependentHabitatInIndex(int x, int y, GalaxyIndex index, out double distance)
        {
            Habitat habitat = null;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < habitatList.Count; i++)
            {
                if (habitatList[i].Population.Count > 0 && (habitatList[i].Empire == null || habitatList[i].Empire == IndependentEmpire))
                {
                    double num = CalculateDistanceSquared(x, y, habitatList[i].Xpos, habitatList[i].Ypos);
                    if (num < distance)
                    {
                        habitat = habitatList[i];
                        distance = num;
                    }
                }
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        public Habitat FindNearestPlanetMoonOfTypeUnoccupiedSystem(double x, double y, Empire empire, HabitatType type)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    Habitat habitat = FindNearestPlanetMoonOfTypeUnoccupiedSystemInIndex((int)x, (int)y, index, out distance, empire, type);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private Habitat FindNearestPlanetMoonOfTypeUnoccupiedSystemInIndex(int x, int y, GalaxyIndex index, out double distance, Empire empire, HabitatType type)
        {
            Habitat habitat = null;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            distance = double.MaxValue;
            int num = -1;
            bool flag = false;
            for (int i = 0; i < habitatList.Count; i++)
            {
                if ((habitatList[i].Category != HabitatCategoryType.Moon && habitatList[i].Category != HabitatCategoryType.Planet) || (type != 0 && habitatList[i].Type != type))
                {
                    continue;
                }
                if (num != habitatList[i].SystemIndex)
                {
                    EmpireSystemSummary dominantEmpire = Systems[habitatList[i].SystemIndex].DominantEmpire;
                    flag = ((dominantEmpire != null && dominantEmpire.Empire != null && dominantEmpire.Empire != empire) ? true : false);
                    num = habitatList[i].SystemIndex;
                }
                if (!flag && (habitatList[i].Empire == null || habitatList[i].Empire == IndependentEmpire))
                {
                    double num2 = CalculateDistanceSquared(x, y, habitatList[i].Xpos, habitatList[i].Ypos);
                    if (num2 < distance)
                    {
                        habitat = habitatList[i];
                        distance = num2;
                    }
                }
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        private Habitat FindNearestColonizableHabitatUnoccupiedSystemInIndex(int x, int y, GalaxyIndex index, out double distance, Empire empire, Design latestColonyShip, List<HabitatType> colonizableHabitatTypes)
        {
            Habitat habitat = null;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            distance = double.MaxValue;
            int num = -1;
            bool flag = false;
            for (int i = 0; i < habitatList.Count; i++)
            {
                if (num != habitatList[i].SystemIndex)
                {
                    EmpireSystemSummary dominantEmpire = Systems[habitatList[i].SystemIndex].DominantEmpire;
                    flag = ((dominantEmpire != null && dominantEmpire.Empire != null && dominantEmpire.Empire != empire) ? true : false);
                    num = habitatList[i].SystemIndex;
                }
                if (!flag && (habitatList[i].Empire == null || habitatList[i].Empire == IndependentEmpire) && CheckEmpireTerritoryCanColonizeHabitat(empire, habitatList[i]))
                {
                    double num2 = CalculateDistanceSquared(x, y, habitatList[i].Xpos, habitatList[i].Ypos);
                    if (num2 < distance && empire.CanDesignColonizeHabitat(latestColonyShip, habitatList[i]) && empire.DetermineColonizeLowQualityHabitat(habitatList[i]))
                    {
                        habitat = habitatList[i];
                        distance = num2;
                    }
                }
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        private Habitat FindNearestHabitatEmptySystemInIndex(int x, int y, GalaxyIndex index, out double distance)
        {
            Habitat habitat = null;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            distance = double.MaxValue;
            int num = -1;
            bool flag = false;
            for (int i = 0; i < habitatList.Count; i++)
            {
                if ((habitatList[i].Category != HabitatCategoryType.Planet && habitatList[i].Category != HabitatCategoryType.Moon) || habitatList[i].Type == HabitatType.FrozenGasGiant || habitatList[i].Type == HabitatType.GasGiant || habitatList[i].Diameter < 60)
                {
                    continue;
                }
                if (num != habitatList[i].SystemIndex)
                {
                    EmpireSystemSummary dominantEmpire = Systems[habitatList[i].SystemIndex].DominantEmpire;
                    flag = ((dominantEmpire != null && dominantEmpire.Empire != null) ? true : false);
                    num = habitatList[i].SystemIndex;
                }
                if (flag || (habitatList[i].Empire != null && habitatList[i].Empire != IndependentEmpire))
                {
                    continue;
                }
                double num2 = CalculateDistanceSquared(x, y, habitatList[i].Xpos, habitatList[i].Ypos);
                if (!(num2 < distance))
                {
                    continue;
                }
                BuiltObject builtObject = FindNearestBuiltObject((int)Systems[habitatList[i].SystemIndex].SystemStar.Xpos, (int)Systems[habitatList[i].SystemIndex].SystemStar.Ypos, null);
                if (builtObject != null && builtObject.Empire != null && builtObject.Empire != IndependentEmpire)
                {
                    double num3 = CalculateDistance(Systems[habitatList[i].SystemIndex].SystemStar.Xpos, Systems[habitatList[i].SystemIndex].SystemStar.Ypos, builtObject.Xpos, builtObject.Ypos);
                    if (num3 < (double)MaxSolarSystemSize * 2.1)
                    {
                        continue;
                    }
                }
                habitat = habitatList[i];
                distance = num2;
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        private Habitat FindNearestColonizableHabitatEmptySystemInIndex(int x, int y, GalaxyIndex index, out double distance, Empire empire, Design latestColonyShip)
        {
            Habitat habitat = null;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            distance = double.MaxValue;
            int num = -1;
            bool flag = false;
            for (int i = 0; i < habitatList.Count; i++)
            {
                if (num != habitatList[i].SystemIndex)
                {
                    EmpireSystemSummary dominantEmpire = Systems[habitatList[i].SystemIndex].DominantEmpire;
                    flag = ((dominantEmpire != null && dominantEmpire.Empire != null && dominantEmpire.Empire != empire) ? true : false);
                    num = habitatList[i].SystemIndex;
                }
                if (flag || (habitatList[i].Empire != null && habitatList[i].Empire != IndependentEmpire))
                {
                    continue;
                }
                double num2 = CalculateDistanceSquared(x, y, habitatList[i].Xpos, habitatList[i].Ypos);
                if (!(num2 < distance) || !empire.CanDesignColonizeHabitat(latestColonyShip, habitatList[i]))
                {
                    continue;
                }
                BuiltObject builtObject = FindNearestBuiltObject((int)Systems[habitatList[i].SystemIndex].SystemStar.Xpos, (int)Systems[habitatList[i].SystemIndex].SystemStar.Ypos, null);
                if (builtObject != null)
                {
                    double num3 = CalculateDistance(Systems[habitatList[i].SystemIndex].SystemStar.Xpos, Systems[habitatList[i].SystemIndex].SystemStar.Ypos, builtObject.Xpos, builtObject.Ypos);
                    if (num3 < (double)MaxSolarSystemSize * 2.1)
                    {
                        continue;
                    }
                }
                habitat = habitatList[i];
                distance = num2;
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        private Habitat FindNearestColonizableHabitatInIndex(int x, int y, GalaxyIndex index, out double distance, Empire empire, Design latestColonyShip)
        {
            Habitat habitat = null;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < habitatList.Count; i++)
            {
                if ((habitatList[i].Empire == null || habitatList[i].Empire == IndependentEmpire) && CheckEmpireTerritoryCanColonizeHabitat(empire, habitatList[i]))
                {
                    double num = CalculateDistanceSquared(x, y, habitatList[i].Xpos, habitatList[i].Ypos);
                    if (num < distance && empire.CanDesignColonizeHabitat(latestColonyShip, habitatList[i]) && empire.DetermineColonizeLowQualityHabitat(habitatList[i]))
                    {
                        habitat = habitatList[i];
                        distance = num;
                    }
                }
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        public Habitat FindNearestUncolonizedHabitat(double x, double y, HabitatType habitatType)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    Habitat habitat = FindNearestUncolonizedHabitatInIndex((int)x, (int)y, index, out distance, habitatType);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private Habitat FindNearestUncolonizedHabitatInIndex(int x, int y, GalaxyIndex index, out double distance, HabitatType habitatType)
        {
            Habitat habitat = null;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < habitatList.Count; i++)
            {
                if (habitatList[i].Empire != null && habitatList[i].Empire != IndependentEmpire)
                {
                    continue;
                }
                double num = CalculateDistanceSquared(x, y, habitatList[i].Xpos, habitatList[i].Ypos);
                if (!(num < distance) || (habitatList[i].Category != HabitatCategoryType.Planet && habitatList[i].Category != HabitatCategoryType.Moon))
                {
                    continue;
                }
                if (habitatType != 0)
                {
                    if (habitatList[i].Type == habitatType)
                    {
                        habitat = habitatList[i];
                        distance = num;
                    }
                }
                else
                {
                    habitat = habitatList[i];
                    distance = num;
                }
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        public Habitat FindNearestHabitat(int x, int y)
        {
            return FindNearestHabitat((double)x, (double)y, HabitatType.Undefined);
        }

        public Habitat FindNearestHabitat(double x, double y)
        {
            return FindNearestHabitat(x, y, HabitatType.Undefined);
        }

        public Habitat FindNearestHabitat(int x, int y, HabitatType habitatType)
        {
            return FindNearestHabitat((double)x, (double)y, habitatType);
        }

        public Habitat FindNearestHabitat(double x, double y, HabitatType habitatType)
        {
            return FindNearestHabitat(x, y, habitatType, null);
        }

        public Habitat FindNearestHabitat(double x, double y, HabitatType habitatType, Habitat habitatToExclude)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    Habitat habitat = FindNearestHabitatInIndex((int)x, (int)y, index, out distance, habitatType, habitatToExclude);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        public Habitat FindNearestHabitat(double x, double y, HabitatCategoryType habitatCategoryType)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    Habitat habitat = FindNearestHabitatInIndex((int)x, (int)y, index, out distance, habitatCategoryType);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        public Habitat FastFindNearestFuelHabitatAlternate(double x, double y, byte resourceId, Habitat habitatToExclude, Empire empire)
        {
            return FastFindNearestFuelHabitatAlternate(x, y, resourceId, habitatToExclude, empire, null, allowBases: true);
        }

        public Habitat FastFindNearestFuelHabitatAlternate(double x, double y, byte resourceId, Habitat habitatToExclude, Empire empire, Habitat systemToExclude, bool allowBases)
        {
            Habitat result = null;
            double num = double.MaxValue;
            if (empire != null && empire.FuelSystemsSources != null)
            {
                FuelSourceSystemList fuelSourceSystemList = null;
                for (int i = 0; i < empire.FuelSystemsSources.Count; i++)
                {
                    FuelSourceSystemList fuelSourceSystemList2 = empire.FuelSystemsSources[i];
                    if (fuelSourceSystemList2 != null && fuelSourceSystemList2.ResourceId == resourceId)
                    {
                        fuelSourceSystemList = fuelSourceSystemList2;
                        break;
                    }
                }
                if (fuelSourceSystemList != null)
                {
                    for (int j = 0; j < fuelSourceSystemList.Count; j++)
                    {
                        FuelSourceSystem fuelSourceSystem = fuelSourceSystemList[j];
                        if (fuelSourceSystem == null)
                        {
                            continue;
                        }
                        for (int k = 0; k < fuelSourceSystem.KnownFuelSources.Count; k++)
                        {
                            Habitat habitat = fuelSourceSystem.KnownFuelSources[k];
                            if (habitat == habitatToExclude || (!allowBases && habitat.BasesAtHabitat != null && habitat.BasesAtHabitat.Count > 0))
                            {
                                continue;
                            }
                            double num2 = CalculateDistanceSquared(x, y, habitat.Xpos, habitat.Ypos);
                            if (num2 < num)
                            {
                                bool flag = true;
                                if (systemToExclude != null && habitat.SystemIndex == systemToExclude.SystemIndex)
                                {
                                    flag = false;
                                }
                                if (flag)
                                {
                                    result = habitat;
                                    num = num2;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public Habitat FindNearestHabitatWithResource(double x, double y, byte resourceId)
        {
            return FindNearestHabitatWithResource(x, y, resourceId, null, null);
        }

        public Habitat FindNearestHabitatWithResource(double x, double y, byte resourceId, Habitat habitatToExclude, Empire empire)
        {
            return FindNearestHabitatWithResource(x, y, resourceId, habitatToExclude, empire, null);
        }

        public Habitat FindNearestHabitatWithResource(double x, double y, byte resourceId, Habitat habitatToExclude, Empire empire, Habitat systemToExclude)
        {
            return FindNearestHabitatWithResource(x, y, resourceId, habitatToExclude, empire, systemToExclude, allowBases: true);
        }

        public Habitat FindNearestHabitatWithResource(double x, double y, byte resourceId, Habitat habitatToExclude, Empire empire, Habitat systemToExclude, bool allowBases)
        {
            double num = double.MaxValue;
            Habitat result = null;
            GalaxyIndex galaxyIndex = ResolveIndex((int)x, (int)y);
            int sectorBoundLeft = galaxyIndex.X;
            int sectorBoundRight = galaxyIndex.X;
            int sectorBoundTop = galaxyIndex.Y;
            int sectorBoundBottom = galaxyIndex.Y;
            int num2 = 0;
            int num3 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(num > (double)num3, 10000, ref iterationCount))
            {
                num3 = DetermineSectorBoundaries(num2, (int)x, (int)y, galaxyIndex.X, galaxyIndex.Y, ref sectorBoundLeft, ref sectorBoundRight, ref sectorBoundTop, ref sectorBoundBottom, out var sectorColumn, out var sectorRow);
                GalaxyIndexList galaxyIndexList = BuildIndexListForSearching(sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, sectorColumn, sectorRow);
                for (int i = 0; i < galaxyIndexList.Count; i++)
                {
                    GalaxyIndex index = galaxyIndexList[i];
                    double distance;
                    Habitat habitat = FindNearestHabitatWithResourceInIndex(x, y, index, out distance, resourceId, habitatToExclude, empire, systemToExclude, allowBases);
                    if (distance < num)
                    {
                        result = habitat;
                        num = distance;
                    }
                }
                num2++;
                if (num2 > IndexMaxX)
                {
                    break;
                }
            }
            return result;
        }

        private int DetermineSectorBoundaries(int stepCount, int x, int y, int startSectorX, int startSectorY, ref int sectorBoundLeft, ref int sectorBoundRight, ref int sectorBoundTop, ref int sectorBoundBottom, out int sectorColumn, out int sectorRow)
        {
            int nearestX;
            int nearestY;
            int result = DetermineClosestIndexEdges(x, y, sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, out nearestX, out nearestY);
            sectorRow = -1;
            sectorColumn = -1;
            if (stepCount == 0)
            {
                sectorRow = sectorBoundTop;
                sectorColumn = sectorBoundLeft;
                return result;
            }
            switch (nearestX)
            {
                case -1:
                    sectorBoundLeft--;
                    if (sectorBoundLeft < 0)
                    {
                        sectorBoundRight++;
                        sectorBoundLeft = 0;
                        if (sectorBoundRight > IndexMaxX - 1)
                        {
                            sectorBoundRight = IndexMaxX - 1;
                        }
                        sectorColumn = sectorBoundRight;
                    }
                    else
                    {
                        sectorColumn = sectorBoundLeft;
                    }
                    break;
                case 1:
                    sectorBoundRight++;
                    if (sectorBoundRight > IndexMaxX - 1)
                    {
                        sectorBoundLeft--;
                        sectorBoundRight = IndexMaxX - 1;
                        if (sectorBoundLeft < 0)
                        {
                            sectorBoundLeft = 0;
                        }
                        sectorColumn = sectorBoundLeft;
                    }
                    else
                    {
                        sectorColumn = sectorBoundRight;
                    }
                    break;
            }
            switch (nearestY)
            {
                case -1:
                    sectorBoundTop--;
                    if (sectorBoundTop < 0)
                    {
                        sectorBoundBottom++;
                        sectorBoundTop = 0;
                        if (sectorBoundBottom > IndexMaxY - 1)
                        {
                            sectorBoundBottom = IndexMaxY - 1;
                        }
                        sectorRow = sectorBoundBottom;
                    }
                    else
                    {
                        sectorRow = sectorBoundTop;
                    }
                    break;
                case 1:
                    sectorBoundBottom++;
                    if (sectorBoundBottom > IndexMaxY - 1)
                    {
                        sectorBoundTop--;
                        sectorBoundBottom = IndexMaxY - 1;
                        if (sectorBoundTop < 0)
                        {
                            sectorBoundTop = 0;
                        }
                        sectorRow = sectorBoundTop;
                    }
                    else
                    {
                        sectorRow = sectorBoundBottom;
                    }
                    break;
            }
            return DetermineClosestIndexEdges(x, y, sectorBoundLeft, sectorBoundRight, sectorBoundTop, sectorBoundBottom, out nearestX, out nearestY);
        }

        private GalaxyIndexList BuildIndexListForSearching(int indexBoundLeft, int indexBoundRight, int indexBoundTop, int indexBoundBottom, int indexColumn, int indexRow)
        {
            GalaxyIndexList galaxyIndexList = new GalaxyIndexList();
            for (int i = indexBoundLeft; i <= indexBoundRight; i++)
            {
                galaxyIndexList.Add(new GalaxyIndex(i, indexRow));
            }
            for (int j = indexBoundTop; j <= indexBoundBottom; j++)
            {
                if (j != indexRow)
                {
                    galaxyIndexList.Add(new GalaxyIndex(indexColumn, j));
                }
            }
            return galaxyIndexList;
        }

        public int DetermineClosestIndexEdges(int x, int y, int indexBoundLeft, int indexBoundRight, int indexBoundTop, int indexBoundBottom, out int nearestX, out int nearestY)
        {
            int num = x - IndexSize * indexBoundLeft;
            if (num < 0)
            {
                num = 536870911;
            }
            int num2 = IndexSize * (indexBoundRight + 1) - x;
            if (num2 > IndexMaxX * IndexSize)
            {
                num2 = 536870911;
            }
            int num3 = y - IndexSize * indexBoundTop;
            if (num3 < 0)
            {
                num3 = 536870911;
            }
            int num4 = IndexSize * (indexBoundBottom + 1) - y;
            if (num4 > IndexMaxY * IndexSize)
            {
                num4 = 536870911;
            }
            int val;
            if (num < num2)
            {
                val = num;
                nearestX = -1;
            }
            else
            {
                val = num2;
                nearestX = 1;
            }
            int val2;
            if (num3 < num4)
            {
                val2 = num3;
                nearestY = -1;
            }
            else
            {
                val2 = num4;
                nearestY = 1;
            }
            return Math.Min(val, val2);
        }

        public int DetermineClosestIndexEdgesCustom(int x, int y, int indexBoundLeft, int indexBoundRight, int indexBoundTop, int indexBoundBottom, out int nearestX, out int nearestY)
        {
            int num = x - IndexSize * indexBoundLeft;
            if (num < 1 || IndexSize * indexBoundLeft <= 0)
            {
                num = 536870911;
            }
            int num2 = IndexSize * (indexBoundRight + 1) - x;
            if (num2 > IndexMaxX * IndexSize || IndexSize * (indexBoundRight + 1) >= IndexMaxX * IndexSize)
            {
                num2 = 536870911;
            }
            int num3 = y - IndexSize * indexBoundTop;
            if (num3 < 1 || IndexSize * indexBoundTop <= 0)
            {
                num3 = 536870911;
            }
            int num4 = IndexSize * (indexBoundBottom + 1) - y;
            if (num4 > IndexMaxY * IndexSize || IndexSize * (indexBoundBottom + 1) >= IndexMaxY * IndexSize)
            {
                num4 = 536870911;
            }
            int val;
            if (num < num2)
            {
                val = num;
                nearestX = -1;
            }
            else
            {
                val = num2;
                nearestX = 1;
            }
            int val2;
            if (num3 < num4)
            {
                val2 = num3;
                nearestY = -1;
            }
            else
            {
                val2 = num4;
                nearestY = 1;
            }
            if (indexBoundLeft <= 0 && nearestX == -1)
            {
                nearestX = 1;
            }
            if (indexBoundLeft >= IndexMaxX && nearestX == 1)
            {
                nearestX = -1;
            }
            if (indexBoundBottom <= 0 && nearestY == -1)
            {
                nearestY = 1;
            }
            if (indexBoundBottom >= IndexMaxY && nearestY == 1)
            {
                nearestY = 1;
            }
            return Math.Min(val, val2);
        }

        private Habitat FindNearestHabitatInIndex(int x, int y, GalaxyIndex index, out double distance, HabitatType habitatType, Habitat habitatToExclude)
        {
            Habitat habitat = null;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < habitatList.Count; i++)
            {
                if (habitatList[i] == habitatToExclude)
                {
                    continue;
                }
                double num = CalculateDistanceSquared(x, y, habitatList[i].Xpos, habitatList[i].Ypos);
                if (!(num < distance))
                {
                    continue;
                }
                if (habitatType != 0)
                {
                    if (habitatList[i].Type == habitatType)
                    {
                        habitat = habitatList[i];
                        distance = num;
                    }
                }
                else
                {
                    habitat = habitatList[i];
                    distance = num;
                }
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        private Habitat FindNearestHabitatInIndex(int x, int y, GalaxyIndex index, out double distance, HabitatCategoryType habitatCategoryType)
        {
            Habitat habitat = null;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < habitatList.Count; i++)
            {
                double num = CalculateDistanceSquared(x, y, habitatList[i].Xpos, habitatList[i].Ypos);
                if (num < distance && habitatList[i].Category == habitatCategoryType)
                {
                    habitat = habitatList[i];
                    distance = num;
                }
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        private Habitat FindNearestHabitatWithResourceInIndex(double x, double y, GalaxyIndex index, out double distance, byte resourceId, Habitat habitatToExclude, Empire empire, Habitat systemToExclude, bool allowBases)
        {
            Habitat habitat = null;
            GalaxyResourceMap galaxyResourceMap = null;
            if (empire != null)
            {
                galaxyResourceMap = empire.ResourceMap;
            }
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < habitatList.Count; i++)
            {
                if (habitatList[i].Resources.Count <= 0)
                {
                    continue;
                }
                int num = -1;
                if (systemToExclude != null)
                {
                    num = habitatList[i].SystemIndex;
                }
                if ((systemToExclude != null && num == systemToExclude.SystemIndex) || (habitatToExclude != null && habitatToExclude == habitatList[i]))
                {
                    continue;
                }
                bool flag = true;
                if (galaxyResourceMap != null && !galaxyResourceMap.CheckResourcesKnown(habitatList[i]))
                {
                    flag = false;
                }
                if (flag && habitatList[i].Resources.IndexOf(resourceId, 0) >= 0 && (allowBases || habitatList[i].BasesAtHabitat == null || habitatList[i].BasesAtHabitat.Count <= 0))
                {
                    double num2 = CalculateDistanceSquared(x, y, habitatList[i].Xpos, habitatList[i].Ypos);
                    if (num2 < distance)
                    {
                        habitat = habitatList[i];
                        distance = num2;
                    }
                }
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        public bool DetermineScrapDamagedShip(BuiltObject builtObject)
        {
            if (builtObject.Role != BuiltObjectRole.Base && builtObject.IsAutoControlled && builtObject.DamageRepair <= 0 && builtObject.WarpSpeed <= 0 && (builtObject.Characters == null || builtObject.Characters.Count <= 0) && (builtObject.Troops == null || builtObject.Troops.Count <= 0) && builtObject.Empire != PlayerEmpire && builtObject.PirateEmpireId != PlayerEmpire.EmpireId)
            {
                double num = ResolveTechBonusFactor(builtObject.Empire, this, builtObject);
                if (num <= 1.0)
                {
                    int num2 = builtObject.Components.CountNormalComponentsByCategory(ComponentCategoryType.HyperDrive);
                    if (num2 <= 0)
                    {
                        bool flag = true;
                        if (builtObject.TopSpeed <= 0)
                        {
                            int num3 = builtObject.Components.CountNormalComponentsByType(ComponentType.EngineMainThrust);
                            int num4 = builtObject.Components.CountNormalComponentsByCategory(ComponentCategoryType.Reactor);
                            if (num3 <= 0 || num4 <= 0)
                            {
                                flag = false;
                            }
                        }
                        if (flag)
                        {
                            StellarObject stellarObject = builtObject.Empire.FindNearestShipYard(builtObject, canRepairOrBuild: false, includeVerySmallYards: true);
                            if (stellarObject != null)
                            {
                                double num5 = CalculateDistance(builtObject.Xpos, builtObject.Ypos, stellarObject.Xpos, stellarObject.Ypos);
                                if (num5 < (double)MaxSolarSystemSize)
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                        if (builtObject.NearestSystemStar != null)
                        {
                            Habitat systemStar = DetermineHabitatSystemStar(builtObject.NearestSystemStar);
                            SystemInfo systemInfo = Systems[systemStar];
                            Empire actualEmpire = builtObject.ActualEmpire;
                            if (actualEmpire != null && systemInfo != null && systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire != null && systemInfo.DominantEmpire.Empire == actualEmpire)
                            {
                                BuiltObject builtObject2 = actualEmpire.FindNearestAvailableConstructionShip(builtObject.Xpos, builtObject.Ypos);
                                if (builtObject2 != null)
                                {
                                    double num6 = CalculateDistance(builtObject.Xpos, builtObject.Ypos, builtObject2.Xpos, builtObject2.Ypos);
                                    double num7 = (double)SectorSize * 3.0;
                                    if (builtObject2.WarpSpeed <= 0)
                                    {
                                        num7 = 2000.0;
                                    }
                                    else if (builtObject2.WarpSpeed < 3000)
                                    {
                                        num7 = (double)SectorSize * 0.5;
                                    }
                                    if (num6 < num7)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        private DistressSignal CheckForMatchingSignal(Empire targetEmpire, Empire attacker, double x, double y)
        {
            return CheckForMatchingSignal(targetEmpire, attacker, x, y, DistressSignalType.UnderAttack);
        }

        private DistressSignal CheckForMatchingSignal(Empire targetEmpire, Empire attacker, double x, double y, DistressSignalType type)
        {
            long currentStarDate = CurrentStarDate;
            if (targetEmpire != null && targetEmpire.DistressSignals != null)
            {
                for (int i = 0; i < targetEmpire.DistressSignals.Count; i++)
                {
                    DistressSignal distressSignal = targetEmpire.DistressSignals[i];
                    if (distressSignal != null && distressSignal.Type == type && distressSignal.Attacker == attacker && distressSignal.Date > currentStarDate - DistressSignalDateRange)
                    {
                        double x2 = 0.0;
                        double y2 = 0.0;
                        if (distressSignal.Source is BuiltObject)
                        {
                            BuiltObject builtObject = (BuiltObject)distressSignal.Source;
                            x2 = builtObject.Xpos;
                            y2 = builtObject.Ypos;
                        }
                        else if (distressSignal.Source is Habitat)
                        {
                            Habitat habitat = (Habitat)distressSignal.Source;
                            x2 = habitat.Xpos;
                            y2 = habitat.Ypos;
                        }
                        double num = CalculateDistanceSquared(x, y, x2, y2);
                        if (num < DistressSignalLocationOverlapRangeSquared)
                        {
                            return distressSignal;
                        }
                    }
                }
            }
            return null;
        }

        private DistressSignal CheckForMatchingSignalSameTargetType(Empire targetEmpire, Empire attacker, StellarObject target, DistressSignalType type)
        {
            long currentStarDate = CurrentStarDate;
            if (target != null && targetEmpire != null && targetEmpire.DistressSignals != null)
            {
                for (int i = 0; i < targetEmpire.DistressSignals.Count; i++)
                {
                    DistressSignal distressSignal = targetEmpire.DistressSignals[i];
                    if (distressSignal == null || distressSignal.Type != type || distressSignal.Attacker != attacker || distressSignal.Date <= currentStarDate - DistressSignalDateRange)
                    {
                        continue;
                    }
                    bool flag = false;
                    double x = 0.0;
                    double y = 0.0;
                    if (target is BuiltObject)
                    {
                        if (distressSignal.Source is BuiltObject)
                        {
                            BuiltObject builtObject = (BuiltObject)distressSignal.Source;
                            x = builtObject.Xpos;
                            y = builtObject.Ypos;
                            flag = true;
                        }
                    }
                    else if (target is Habitat && distressSignal.Source is Habitat)
                    {
                        Habitat habitat = (Habitat)distressSignal.Source;
                        x = habitat.Xpos;
                        y = habitat.Ypos;
                        flag = true;
                    }
                    if (flag)
                    {
                        double num = CalculateDistanceSquared(target.Xpos, target.Ypos, x, y);
                        if (num < DistressSignalLocationOverlapRangeSquared)
                        {
                            return distressSignal;
                        }
                    }
                }
            }
            return null;
        }

        public void NotifyOfAttack(StellarObject attacker, Empire attackingEmpire, BuiltObject builtObjectUnderAttack, bool isNewAttack)
        {
            if (builtObjectUnderAttack == null || builtObjectUnderAttack.Empire == null || attacker == null)
            {
                return;
            }
            DistressSignal distressSignal = CheckForMatchingSignalSameTargetType(builtObjectUnderAttack.Empire, attackingEmpire, builtObjectUnderAttack, DistressSignalType.UnderAttack);
            if (distressSignal == null)
            {
                DistressSignal distressSignal2 = new DistressSignal(builtObjectUnderAttack, DistressSignalType.UnderAttack, CurrentStarDate);
                distressSignal2.Attacker = attackingEmpire;
                if (attacker is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)attacker;
                    distressSignal2.AttackStrength = builtObject.CalculateOverallStrengthFactor();
                }
                else if (attacker is Creature)
                {
                    Creature creature = (Creature)attacker;
                    distressSignal2.AttackStrength = creature.AttackStrength * 5;
                }
                else
                {
                    distressSignal2.AttackStrength = attacker.FirepowerRaw;
                }
                builtObjectUnderAttack.Empire.DistressSignals.Add(distressSignal2);
                string text = string.Empty;
                if (attacker is BuiltObject)
                {
                    BuiltObject builtObject2 = (BuiltObject)attacker;
                    if (builtObject2.Empire.PirateEmpireBaseHabitat != null)
                    {
                        text = text + TextResolver.GetText("Pirate").ToLower(CultureInfo.InvariantCulture) + " ";
                    }
                    text += ResolveDescription(builtObject2.SubRole).ToLower(CultureInfo.InvariantCulture);
                    text = text + " (" + builtObject2.Name + ")";
                }
                else if (attacker is Creature)
                {
                    Creature creature2 = (Creature)attacker;
                    distressSignal2.AttackStrength = creature2.AttackStrength * 5;
                    string text2 = ResolveDescription(creature2.Type);
                    text += text2;
                    if (creature2.Type == CreatureType.Kaltor)
                    {
                        builtObjectUnderAttack.Empire.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.EncounterFirstKaltor, creature2);
                    }
                }
                string empty = string.Empty;
                empty = ((builtObjectUnderAttack.Role != BuiltObjectRole.Base) ? string.Format(TextResolver.GetText("X Y is under attack from ATTACKER"), ResolveDescription(builtObjectUnderAttack.SubRole), builtObjectUnderAttack.Name, text) : string.Format(TextResolver.GetText("X is under attack from ATTACKER"), builtObjectUnderAttack.Name, text));
                builtObjectUnderAttack.Empire.SendMessageToEmpire(builtObjectUnderAttack.Empire, EmpireMessageType.BattleUnderAttack, builtObjectUnderAttack, empty);
            }
            else if (isNewAttack)
            {
                if (attacker is BuiltObject)
                {
                    BuiltObject builtObject3 = (BuiltObject)attacker;
                    distressSignal.AttackStrength += builtObject3.CalculateOverallStrengthFactor();
                }
                else if (attacker is Creature)
                {
                    Creature creature3 = (Creature)attacker;
                    distressSignal.AttackStrength += creature3.AttackStrength * 5;
                }
                else
                {
                    distressSignal.AttackStrength += attacker.FirepowerRaw;
                }
            }
        }

        public void NotifyOfAttack(StellarObject attacker, Empire attackingEmpire, Habitat habitatUnderAttack, bool bombarded, bool isNewAttack, bool notifyIndependent)
        {
            if (habitatUnderAttack != null && habitatUnderAttack.Empire != null && habitatUnderAttack.Empire != IndependentEmpire && attackingEmpire != null && attackingEmpire.DominantRace != null && attacker != null)
            {
                DistressSignalType distressSignalType = DistressSignalType.UnderAttack;
                if (bombarded)
                {
                    distressSignalType = DistressSignalType.ColonyBombarded;
                }
                DistressSignal distressSignal = CheckForMatchingSignalSameTargetType(habitatUnderAttack.Empire, attackingEmpire, habitatUnderAttack, distressSignalType);
                if (distressSignal == null || distressSignal.Source is BuiltObject)
                {
                    DistressSignal distressSignal2 = new DistressSignal(habitatUnderAttack, distressSignalType, CurrentStarDate);
                    distressSignal2.Attacker = attackingEmpire;
                    if (attacker is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)attacker;
                        distressSignal2.AttackStrength = builtObject.CalculateOverallStrengthFactor();
                    }
                    else if (attacker is Creature)
                    {
                        Creature creature = (Creature)attacker;
                        distressSignal2.AttackStrength = creature.AttackStrength * 5;
                    }
                    else
                    {
                        distressSignal2.AttackStrength = attacker.FirepowerRaw;
                    }
                    habitatUnderAttack.Empire.DistressSignals.Add(distressSignal2);
                    string empty = string.Empty;
                    if (distressSignalType == DistressSignalType.ColonyBombarded)
                    {
                        empty = string.Format(TextResolver.GetText("Colony Bombardment"), attackingEmpire.DominantRace.Name, attackingEmpire.Name, habitatUnderAttack.Name);
                    }
                    else
                    {
                        string text = string.Empty;
                        if (attacker is BuiltObject)
                        {
                            BuiltObject builtObject2 = (BuiltObject)attacker;
                            if (builtObject2.Empire.PirateEmpireBaseHabitat != null)
                            {
                                text = text + TextResolver.GetText("Pirate") + " ";
                            }
                            text += ResolveDescription(builtObject2.SubRole).ToLower(CultureInfo.InvariantCulture);
                            text = text + " (" + builtObject2.Name + ")";
                        }
                        else if (attacker is Creature)
                        {
                            Creature creature2 = (Creature)attacker;
                            text += ResolveDescription(creature2.Type);
                            if (creature2.Type == CreatureType.Kaltor)
                            {
                                habitatUnderAttack.Empire.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.EncounterFirstKaltor, creature2);
                            }
                        }
                        empty = string.Format(TextResolver.GetText("X is under attack from ATTACKER"), habitatUnderAttack.Name, text);
                    }
                    habitatUnderAttack.Empire.SendMessageToEmpire(habitatUnderAttack.Empire, EmpireMessageType.BattleUnderAttack, habitatUnderAttack, empty);
                    string empty2 = string.Empty;
                    empty2 = ((distressSignalType != DistressSignalType.ColonyBombarded) ? string.Format(TextResolver.GetText("Our forces are invading COLONY of EMPIRE"), habitatUnderAttack.Name, habitatUnderAttack.Empire.Name) : string.Format(TextResolver.GetText("Our forces are bombarding COLONY of EMPIRE"), habitatUnderAttack.Name, habitatUnderAttack.Empire.Name));
                    attackingEmpire.SendMessageToEmpire(attackingEmpire, EmpireMessageType.BattleUnderAttack, habitatUnderAttack, empty2);
                }
                else if (distressSignalType == DistressSignalType.UnderAttack && isNewAttack)
                {
                    if (attacker is BuiltObject)
                    {
                        BuiltObject builtObject3 = (BuiltObject)attacker;
                        distressSignal.AttackStrength += builtObject3.CalculateOverallStrengthFactor();
                    }
                    else if (attacker is Creature)
                    {
                        Creature creature3 = (Creature)attacker;
                        distressSignal.AttackStrength += creature3.AttackStrength * 5;
                    }
                    else
                    {
                        distressSignal.AttackStrength += attacker.FirepowerRaw;
                    }
                }
            }
            else
            {
                if (habitatUnderAttack.Empire != IndependentEmpire || !notifyIndependent)
                {
                    return;
                }
                DistressSignal distressSignal3 = CheckForMatchingSignal(habitatUnderAttack.Empire, attackingEmpire, habitatUnderAttack.Xpos, habitatUnderAttack.Ypos);
                if (distressSignal3 == null)
                {
                    DistressSignal distressSignal4 = new DistressSignal(habitatUnderAttack, DistressSignalType.UnderAttack, CurrentStarDate);
                    distressSignal4.Attacker = attackingEmpire;
                    if (attacker is BuiltObject)
                    {
                        BuiltObject builtObject4 = (BuiltObject)attacker;
                        distressSignal4.AttackStrength = builtObject4.CalculateOverallStrengthFactor();
                    }
                    else if (attacker is Creature)
                    {
                        Creature creature4 = (Creature)attacker;
                        distressSignal4.AttackStrength = creature4.AttackStrength * 5;
                    }
                    else
                    {
                        distressSignal4.AttackStrength = attacker.FirepowerRaw;
                    }
                    habitatUnderAttack.Empire.DistressSignals.Add(distressSignal4);
                    if (attackingEmpire == null)
                    {
                        return;
                    }
                    if (!bombarded)
                    {
                        attackingEmpire.CivilityRating -= IndependentColonyInvadeReputationImpact;
                    }
                    int num = _EmpireTerritory.CheckLocationOwnership(habitatUnderAttack.Xpos, habitatUnderAttack.Ypos);
                    if (num >= 0 && num != attackingEmpire.EmpireId)
                    {
                        Empire byEmpireId = Empires.GetByEmpireId(num);
                        if (byEmpireId != null)
                        {
                            byEmpireId.ObtainEmpireEvaluation(attackingEmpire).IncidentEvaluation -= 50.0;
                        }
                    }
                }
                else if (isNewAttack)
                {
                    if (attacker is BuiltObject)
                    {
                        BuiltObject builtObject5 = (BuiltObject)attacker;
                        distressSignal3.AttackStrength += builtObject5.CalculateOverallStrengthFactor();
                    }
                    else if (attacker is Creature)
                    {
                        Creature creature5 = (Creature)attacker;
                        distressSignal3.AttackStrength += creature5.AttackStrength * 5;
                    }
                    else
                    {
                        distressSignal3.AttackStrength += attacker.FirepowerRaw;
                    }
                }
            }
        }

        public bool CheckWithinCreatureAttackRange(double x, double y, Creature creature)
        {
            if (creature != null)
            {
                double val = double.MaxValue;
                if (creature.ParentHabitat != null)
                {
                    val = CalculateDistance(x, y, creature.ParentHabitat.Xpos, creature.ParentHabitat.Ypos);
                }
                double val2 = CalculateDistance(x, y, creature.Xpos, creature.Ypos);
                double num = Math.Min(val, val2);
                double num2 = Math.Min(1000.0, creature.AttackRange);
                if (num < num2)
                {
                    return true;
                }
            }
            return false;
        }

        public BuiltObjectList EvaluateSystemThreats(Habitat systemStar, Empire targetEmpire, out List<int> threatLevels)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            threatLevels = new List<int>();
            List<BuiltObject[]> builtObjectsAtLocationByArrays = GetBuiltObjectsAtLocationByArrays(systemStar.Xpos, systemStar.Ypos, MaxSolarSystemSize * 2);
            _ = targetEmpire.PirateEmpireBaseHabitat;
            for (int i = 0; i < builtObjectsAtLocationByArrays.Count; i++)
            {
                int num = builtObjectsAtLocationByArrays[i].Length;
                for (int j = 0; j < num; j++)
                {
                    BuiltObject builtObject = builtObjectsAtLocationByArrays[i][j];
                    if (builtObject != null && builtObject.NearestSystemStar == systemStar && builtObject.Empire != targetEmpire)
                    {
                        int num2 = DetermineRawThreatLevel(builtObject, targetEmpire);
                        if (num2 > 10 && !builtObjectList.Contains(builtObject))
                        {
                            builtObjectList.Add(builtObject);
                            threatLevels.Add(num2);
                        }
                    }
                }
            }
            return builtObjectList;
        }

        public StellarObject[] EvaluateThreats(object target, out int[] threatLevel)
        {
            return EvaluateThreats(target, out threatLevel, 20);
        }

        public StellarObject[] EvaluateThreats(object target, out int[] threatLevel, int maximumThreatCount)
        {
            StellarObjectList stellarObjectList = new StellarObjectList();
            List<int> list = new List<int>();
            double num = -1.0;
            double num2 = -1.0;
            double num3 = ThreatRange;
            SystemInfo systemInfo = null;
            Empire empire;
            if (target is Habitat)
            {
                Habitat habitat = (Habitat)target;
                num = habitat.Xpos;
                num2 = habitat.Ypos;
                empire = habitat.Owner;
                num3 = MaxSolarSystemSize;
                systemInfo = Systems[habitat.SystemIndex];
            }
            else if (target is ShipGroup)
            {
                double num4 = 0.0;
                double num5 = 0.0;
                double num6 = 0.0;
                double num7 = 0.0;
                ShipGroup shipGroup = (ShipGroup)target;
                for (int i = 0; i < shipGroup.Ships.Count; i++)
                {
                    BuiltObject builtObject = shipGroup.Ships[i];
                    if (builtObject != null)
                    {
                        if ((double)builtObject.SensorProximityArrayRange > num3)
                        {
                            num3 = builtObject.SensorProximityArrayRange;
                        }
                        if (builtObject.Xpos < num4)
                        {
                            num4 = builtObject.Xpos;
                        }
                        if (builtObject.Xpos > num5)
                        {
                            num5 = builtObject.Xpos;
                        }
                        if (builtObject.Ypos < num6)
                        {
                            num6 = builtObject.Ypos;
                        }
                        if (builtObject.Ypos > num7)
                        {
                            num7 = builtObject.Ypos;
                        }
                    }
                }
                num = num4 + (num5 - num4) / 2.0;
                num2 = num6 + (num7 - num6) / 2.0;
                empire = shipGroup.Empire;
                if (shipGroup.LeadShip != null && shipGroup.LeadShip.NearestSystemStar != null)
                {
                    systemInfo = Systems[shipGroup.LeadShip.NearestSystemStar.SystemIndex];
                }
            }
            else
            {
                if (!(target is BuiltObject))
                {
                    throw new ApplicationException("Target is of wrong type");
                }
                BuiltObject builtObject2 = (BuiltObject)target;
                if ((double)builtObject2.SensorProximityArrayRange > num3)
                {
                    num3 = builtObject2.SensorProximityArrayRange;
                }
                num = builtObject2.Xpos;
                num2 = builtObject2.Ypos;
                empire = builtObject2.Empire;
                if (builtObject2.NearestSystemStar != null)
                {
                    systemInfo = Systems[builtObject2.NearestSystemStar.SystemIndex];
                }
            }
            int left = (int)num - (int)num3;
            int right = (int)num + (int)num3;
            int top = (int)num2 - (int)num3;
            int bottom = (int)num2 + (int)num3;
            int x = (int)num / IndexSize;
            int y = (int)num2 / IndexSize;
            CorrectIndexCoords(ref x, ref y);
            num3 += (double)(MaxSolarSystemSize * 2);
            List<BuiltObject[]> builtObjectsAtLocationByArrays = GetBuiltObjectsAtLocationByArrays(num, num2, (int)num3);
            num3 *= num3;
            int num8 = 10;
            if (empire == null)
            {
                num8 = 0;
            }
            for (int j = 0; j < builtObjectsAtLocationByArrays.Count; j++)
            {
                for (int k = 0; k < builtObjectsAtLocationByArrays[j].Length; k++)
                {
                    BuiltObject builtObject3 = builtObjectsAtLocationByArrays[j][k];
                    if (builtObject3 != null)
                    {
                        int num9 = DetermineThreatLevel(builtObject3, target, empire, (int)num, (int)num2, num3, left, right, top, bottom);
                        if (num9 > num8 && !stellarObjectList.Contains(builtObject3))
                        {
                            stellarObjectList.Add(builtObject3);
                            list.Add(num9);
                        }
                    }
                }
            }
            if (systemInfo != null)
            {
                for (int l = 0; l < systemInfo.Creatures.Count; l++)
                {
                    Creature creature = systemInfo.Creatures[l];
                    if (creature != null)
                    {
                        int num10 = DetermineThreatLevel(creature, target, empire, (int)num, (int)num2, num3, left, right, top, bottom);
                        if (num10 > 10)
                        {
                            stellarObjectList.Add(creature);
                            list.Add(num10);
                        }
                    }
                }
            }
            else
            {
                GalaxyLocationList galaxyLocationList = DetermineGalaxyLocationsInRangeAtPoint(num, num2, num3, GalaxyLocationType.RestrictedArea);
                if (galaxyLocationList != null && galaxyLocationList.Count > 0)
                {
                    for (int m = 0; m < galaxyLocationList.Count; m++)
                    {
                        GalaxyLocation galaxyLocation = galaxyLocationList[m];
                        if (galaxyLocation == null || galaxyLocation.RelatedCreatures == null || galaxyLocation.RelatedCreatures.Count <= 0)
                        {
                            continue;
                        }
                        for (int n = 0; n < galaxyLocation.RelatedCreatures.Count; n++)
                        {
                            Creature creature2 = galaxyLocation.RelatedCreatures[n];
                            if (creature2 != null)
                            {
                                int num11 = DetermineThreatLevel(creature2, target, empire, (int)num, (int)num2, num3, left, right, top, bottom);
                                if (num11 > 10)
                                {
                                    stellarObjectList.Add(creature2);
                                    list.Add(num11);
                                }
                            }
                        }
                    }
                }
            }
            StellarObject[] array = stellarObjectList.ToArray();
            int[] array2 = list.ToArray();
            Array.Sort(array2, array);
            Array.Reverse(array);
            Array.Reverse(array2);
            if (array.Length < maximumThreatCount)
            {
                maximumThreatCount = array.Length;
            }
            StellarObject[] array3 = new StellarObject[maximumThreatCount];
            Array.Copy(array, 0, array3, 0, maximumThreatCount);
            int[] array4 = new int[maximumThreatCount];
            Array.Copy(array2, 0, array4, 0, maximumThreatCount);
            threatLevel = array4;
            return array3;
        }

        public int DetermineThreatLevel(StellarObject threat, object target)
        {
            double num = -1.0;
            double num2 = -1.0;
            int num3 = ThreatRange;
            Empire empire;
            if (target is Habitat)
            {
                num = ((Habitat)target).Xpos;
                num2 = ((Habitat)target).Ypos;
                empire = ((Habitat)target).Owner;
                num3 = MaxSolarSystemSize;
            }
            else if (target is ShipGroup)
            {
                double num4 = 0.0;
                double num5 = 0.0;
                double num6 = 0.0;
                double num7 = 0.0;
                ShipGroup shipGroup = (ShipGroup)target;
                for (int i = 0; i < shipGroup.Ships.Count; i++)
                {
                    if (shipGroup.Ships[i].SensorProximityArrayRange > num3)
                    {
                        num3 = shipGroup.Ships[i].SensorProximityArrayRange;
                    }
                    if (shipGroup.Ships[i].Xpos < num4)
                    {
                        num4 = shipGroup.Ships[i].Xpos;
                    }
                    if (shipGroup.Ships[i].Xpos > num5)
                    {
                        num5 = shipGroup.Ships[i].Xpos;
                    }
                    if (shipGroup.Ships[i].Ypos < num6)
                    {
                        num6 = shipGroup.Ships[i].Ypos;
                    }
                    if (shipGroup.Ships[i].Ypos > num7)
                    {
                        num7 = shipGroup.Ships[i].Ypos;
                    }
                }
                num = num4 + (num5 - num4) / 2.0;
                num2 = num6 + (num7 - num6) / 2.0;
                empire = shipGroup.Empire;
            }
            else
            {
                if (!(target is BuiltObject))
                {
                    throw new ApplicationException("Target is of wrong type");
                }
                if (((BuiltObject)target).SensorProximityArrayRange > num3)
                {
                    num3 = ((BuiltObject)target).SensorProximityArrayRange;
                }
                num = ((BuiltObject)target).Xpos;
                num2 = ((BuiltObject)target).Ypos;
                empire = ((BuiltObject)target).Empire;
            }
            int left = (int)num - num3;
            int right = (int)num + num3;
            int top = (int)num2 - num3;
            int bottom = (int)num2 + num3;
            double scanRangeSquared = (double)num3 * (double)num3;
            if (threat is BuiltObject)
            {
                return DetermineThreatLevel((BuiltObject)threat, target, empire, (int)num, (int)num2, scanRangeSquared, left, right, top, bottom);
            }
            if (threat is Fighter)
            {
                return DetermineThreatLevel((Fighter)threat, target, empire, (int)num, (int)num2, scanRangeSquared, left, right, top, bottom);
            }
            if (threat is Creature)
            {
                return DetermineThreatLevel((Creature)threat, target, empire, (int)num, (int)num2, scanRangeSquared, left, right, top, bottom);
            }
            return 0;
        }

        private int DetermineThreatLevel(Fighter fighter, object target, Empire empire, int targetX, int targetY, double scanRangeSquared, int left, int right, int top, int bottom)
        {
            if (empire != fighter.Empire)
            {
                double num = CalculateDistanceSquared(fighter.Xpos, fighter.Ypos, targetX, targetY);
                if (num <= scanRangeSquared)
                {
                    double num2 = Math.Sqrt(num);
                    double num3 = (double)ThreatRange / 2.0;
                    double num4 = Math.Max(1.0, num3 - num2);
                    num4 *= num4;
                    num4 /= 1000000.0;
                    int num5 = 1;
                    int num6 = 0;
                    if (fighter.Empire == null)
                    {
                        num5 = 0;
                    }
                    else if (fighter.Empire == IndependentEmpire)
                    {
                        num5 = 1;
                    }
                    else if (empire == null)
                    {
                        num5 = 1;
                    }
                    else if (empire.PirateEmpireBaseHabitat != null && fighter.Empire != empire && fighter.Empire != null)
                    {
                        num5 = 50;
                        num6 = 1;
                        PirateRelation pirateRelation = empire.ObtainPirateRelation(fighter.Empire);
                        switch (pirateRelation.Type)
                        {
                            case PirateRelationType.NotMet:
                                DoEmpireEncounter(empire, fighter.Empire, fighter);
                                break;
                            case PirateRelationType.Protection:
                                num5 = 0;
                                break;
                        }
                    }
                    else if (fighter.Empire.PirateEmpireBaseHabitat != null)
                    {
                        num5 = 50;
                        if (!empire.KnownPirateEmpires.Contains(fighter.Empire))
                        {
                            DoSuperPirateEmpireEncounter(empire, fighter.Empire, targetX, targetY);
                            empire.KnownPirateEmpires.Add(fighter.Empire);
                        }
                        if (fighter.Empire != null)
                        {
                            PirateRelation pirateRelation2 = empire.ObtainPirateRelation(fighter.Empire);
                            switch (pirateRelation2.Type)
                            {
                                case PirateRelationType.NotMet:
                                    DoEmpireEncounter(empire, fighter.Empire, fighter);
                                    break;
                                case PirateRelationType.Protection:
                                    num5 = 0;
                                    break;
                            }
                        }
                        if (empire == IndependentEmpire)
                        {
                            num5 = 0;
                        }
                    }
                    else
                    {
                        DiplomaticRelation diplomaticRelation = empire.DiplomaticRelations[fighter.Empire];
                        num5 = 1;
                        if (diplomaticRelation != null)
                        {
                            switch (diplomaticRelation.Type)
                            {
                                case DiplomaticRelationType.FreeTradeAgreement:
                                case DiplomaticRelationType.MutualDefensePact:
                                case DiplomaticRelationType.SubjugatedDominion:
                                case DiplomaticRelationType.Protectorate:
                                    num5 = 0;
                                    break;
                                case DiplomaticRelationType.None:
                                    num5 = 1;
                                    break;
                                case DiplomaticRelationType.TradeSanctions:
                                    num5 = 5;
                                    break;
                                case DiplomaticRelationType.Truce:
                                    num5 = 10;
                                    break;
                                case DiplomaticRelationType.War:
                                    num5 = 100;
                                    break;
                                case DiplomaticRelationType.NotMet:
                                    num5 = 1;
                                    diplomaticRelation.Type = DiplomaticRelationType.None;
                                    DoEmpireEncounter(empire, fighter.Empire, fighter);
                                    break;
                            }
                        }
                        else
                        {
                            if (fighter.Empire != IndependentEmpire)
                            {
                                num5 = 1;
                            }
                            DoEmpireEncounter(empire, fighter.Empire, fighter);
                        }
                    }
                    num6 = 1;
                    if (fighter.FirepowerRaw > 0)
                    {
                        num6 = Math.Max(5, fighter.Size / 10);
                    }
                    int num7 = (int)(num4 * (double)num5 * (double)num6);
                    if (num6 > 0)
                    {
                        num7 = Math.Max(1, num7);
                    }
                    return num7;
                }
            }
            return 0;
        }

        private int DetermineThreatLevel(Creature creature, object target, Empire empire, int targetX, int targetY, double scanRangeSquared, int left, int right, int top, int bottom)
        {
            double num = CalculateDistanceSquared(creature.Xpos, creature.Ypos, targetX, targetY);
            if (num <= scanRangeSquared)
            {
                double num2 = Math.Sqrt(num);
                double num3 = (double)ThreatRange / 2.0;
                double num4 = Math.Max(1.0, num3 - num2);
                num4 *= num4;
                num4 /= 1000000.0;
                int num5 = 100;
                int num6 = 0;
                switch (creature.Type)
                {
                    case CreatureType.RockSpaceSlug:
                    case CreatureType.DesertSpaceSlug:
                        num6 = 10;
                        break;
                    case CreatureType.Kaltor:
                        num6 = 20;
                        break;
                    case CreatureType.Ardilus:
                        num6 = 15;
                        break;
                    case CreatureType.SilverMist:
                        num6 = 100;
                        if (empire != null && empire != IndependentEmpire && !empire.EncounteredSilverMistCreature)
                        {
                            empire.EncounteredSilverMistCreature = true;
                            Habitat habitat = FastFindNearestSystem(targetX, targetY);
                            if (habitat != null)
                            {
                                string arg = ResolveSectorDescription(targetX, targetY);
                                string text = TextResolver.GetText("SilverMist Encountered");
                                string message = string.Format(TextResolver.GetText("SilverMist Encountered Detail"), habitat.Name, arg);
                                empire.SendEventMessageToEmpire(EventMessageType.CreatureOutbreak, text, message, creature, target);
                            }
                        }
                        break;
                }
                int num7 = (int)(num4 * (double)num5 * (double)num6);
                if (num6 > 0)
                {
                    num7 = Math.Max(1, num7);
                }
                return num7;
            }
            return 0;
        }

        private int DetermineThreatLevel(BuiltObject builtObject, object target, Empire empire, int targetX, int targetY, double scanRangeSquared, int left, int right, int top, int bottom)
        {
            if (empire != builtObject.Empire)
            {
                double num = CalculateDistanceSquared(builtObject.Xpos, builtObject.Ypos, targetX, targetY);
                if (num <= scanRangeSquared)
                {
                    double num2 = Math.Sqrt(num);
                    double num3 = (double)ThreatRange / 2.0;
                    double num4 = Math.Max(1.0, num3 - num2);
                    num4 *= num4;
                    num4 /= 1000000.0;
                    int num5 = 1;
                    int num6 = 0;
                    if (builtObject.Empire == null)
                    {
                        num5 = 0;
                    }
                    else if (builtObject.Empire == IndependentEmpire)
                    {
                        num5 = 1;
                        empire?.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.FirstContactPirateOrIndependent, builtObject, builtObject.Empire);
                    }
                    else if (empire == null)
                    {
                        num5 = 1;
                    }
                    else if (empire.PirateEmpireBaseHabitat != null && builtObject.Empire != empire && builtObject.Empire != null)
                    {
                        num5 = 50;
                        num6 = 1;
                        PirateRelation pirateRelation = empire.ObtainPirateRelation(builtObject.Empire);
                        switch (pirateRelation.Type)
                        {
                            case PirateRelationType.NotMet:
                                DoEmpireEncounter(empire, builtObject.Empire, builtObject);
                                break;
                            case PirateRelationType.Protection:
                                num5 = 0;
                                break;
                        }
                    }
                    else if (builtObject.Empire.PirateEmpireBaseHabitat != null)
                    {
                        num5 = 50;
                        if (!empire.KnownPirateEmpires.Contains(builtObject.Empire))
                        {
                            DoSuperPirateEmpireEncounter(empire, builtObject.Empire, targetX, targetY);
                            empire.KnownPirateEmpires.Add(builtObject.Empire);
                        }
                        if (builtObject.Empire != null)
                        {
                            PirateRelation pirateRelation2 = empire.ObtainPirateRelation(builtObject.Empire);
                            switch (pirateRelation2.Type)
                            {
                                case PirateRelationType.NotMet:
                                    DoEmpireEncounter(empire, builtObject.Empire, builtObject);
                                    break;
                                case PirateRelationType.Protection:
                                    num5 = 0;
                                    break;
                            }
                        }
                        if (empire == IndependentEmpire)
                        {
                            num5 = 0;
                        }
                    }
                    else
                    {
                        DiplomaticRelation diplomaticRelation = empire.DiplomaticRelations[builtObject.Empire];
                        num5 = 1;
                        if (diplomaticRelation != null)
                        {
                            switch (diplomaticRelation.Type)
                            {
                                case DiplomaticRelationType.FreeTradeAgreement:
                                case DiplomaticRelationType.MutualDefensePact:
                                case DiplomaticRelationType.SubjugatedDominion:
                                case DiplomaticRelationType.Protectorate:
                                    num5 = 0;
                                    break;
                                case DiplomaticRelationType.None:
                                    num5 = 1;
                                    break;
                                case DiplomaticRelationType.TradeSanctions:
                                    num5 = 5;
                                    break;
                                case DiplomaticRelationType.Truce:
                                    num5 = 10;
                                    break;
                                case DiplomaticRelationType.War:
                                    num5 = 100;
                                    break;
                                case DiplomaticRelationType.NotMet:
                                    num5 = 1;
                                    diplomaticRelation.Type = DiplomaticRelationType.None;
                                    DoEmpireEncounter(empire, builtObject.Empire, builtObject);
                                    break;
                            }
                        }
                        else
                        {
                            if (builtObject.Empire != IndependentEmpire)
                            {
                                num5 = 1;
                            }
                            DoEmpireEncounter(empire, builtObject.Empire, builtObject);
                        }
                        if (empire.Outlaws.Contains(builtObject))
                        {
                            num5 = 100;
                        }
                    }
                    num6 = 1;
                    if (builtObject.FirepowerRaw > 0)
                    {
                        num6 = Math.Max(10, builtObject.Size / 10);
                    }
                    if (builtObject.Fighters != null && builtObject.Fighters.Count > 0)
                    {
                        num6 = Math.Max(num6, builtObject.Fighters.Count * 10);
                    }
                    int num7 = (int)(num4 * (double)num5 * (double)num6);
                    if (num6 > 0)
                    {
                        num7 = Math.Max(1, num7);
                    }
                    return num7;
                }
            }
            return 0;
        }

        public int DetermineRawThreatLevel(BuiltObject threat, Empire targetEmpire)
        {
            if (threat.Empire != null && threat.Empire != targetEmpire)
            {
                int num = 1;
                int num2 = 0;
                if (threat.Empire == IndependentEmpire)
                {
                    num = 1;
                    targetEmpire?.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.FirstContactPirateOrIndependent, threat, threat.Empire);
                }
                else if (targetEmpire.PirateEmpireBaseHabitat != null && threat.Empire != targetEmpire && threat.Empire != null)
                {
                    PirateRelation pirateRelation = targetEmpire.ObtainPirateRelation(threat.Empire);
                    switch (pirateRelation.Type)
                    {
                        case PirateRelationType.NotMet:
                            DoEmpireEncounter(targetEmpire, threat.Empire, threat);
                            num = 50;
                            num2 = 1;
                            break;
                        case PirateRelationType.Protection:
                            num = 0;
                            num2 = 1;
                            break;
                        case PirateRelationType.None:
                            num = 50;
                            num2 = 1;
                            break;
                    }
                }
                else if (threat.Empire.PirateEmpireBaseHabitat != null)
                {
                    num = 50;
                    if (!targetEmpire.KnownPirateEmpires.Contains(threat.Empire))
                    {
                        DoSuperPirateEmpireEncounter(targetEmpire, threat.Empire, threat.Xpos, threat.Ypos);
                        targetEmpire.KnownPirateEmpires.Add(threat.Empire);
                    }
                    if (threat.Empire != null)
                    {
                        PirateRelation pirateRelation2 = targetEmpire.ObtainPirateRelation(threat.Empire);
                        switch (pirateRelation2.Type)
                        {
                            case PirateRelationType.NotMet:
                                DoEmpireEncounter(targetEmpire, threat.Empire, threat);
                                num = 50;
                                num2 = 1;
                                break;
                            case PirateRelationType.Protection:
                                num = 0;
                                num2 = 1;
                                break;
                            case PirateRelationType.None:
                                num = 50;
                                num2 = 1;
                                break;
                        }
                    }
                    if (targetEmpire == IndependentEmpire)
                    {
                        num = 0;
                    }
                }
                else
                {
                    DiplomaticRelation diplomaticRelation = targetEmpire.ObtainDiplomaticRelation(threat.Empire);
                    if (diplomaticRelation != null)
                    {
                        switch (diplomaticRelation.Type)
                        {
                            case DiplomaticRelationType.FreeTradeAgreement:
                            case DiplomaticRelationType.MutualDefensePact:
                            case DiplomaticRelationType.SubjugatedDominion:
                            case DiplomaticRelationType.Protectorate:
                                num = 0;
                                break;
                            case DiplomaticRelationType.None:
                                num = 1;
                                break;
                            case DiplomaticRelationType.TradeSanctions:
                                num = 1;
                                break;
                            case DiplomaticRelationType.Truce:
                                num = 10;
                                break;
                            case DiplomaticRelationType.War:
                                num = 100;
                                break;
                            case DiplomaticRelationType.NotMet:
                                num = 1;
                                DoEmpireEncounter(targetEmpire, threat.Empire, threat);
                                break;
                        }
                    }
                    else
                    {
                        if (threat.Empire != IndependentEmpire)
                        {
                            num = 1;
                        }
                        DoEmpireEncounter(targetEmpire, threat.Empire, threat);
                    }
                    if (targetEmpire.Outlaws.Contains(threat))
                    {
                        num = 100;
                    }
                }
                if (num >= 50)
                {
                    num2 = 1;
                }
                if (threat.FirepowerRaw > 0)
                {
                    num2 = Math.Max(10, threat.Size / 10);
                }
                return (int)((double)num * (double)num2);
            }
            return 0;
        }

        public void DoSuperPirateEmpireEncounter(Empire discoverer, Empire pirateEmpire, double x, double y)
        {
            if (discoverer == null || pirateEmpire == null || pirateEmpire.PirateEmpireBaseHabitat == null || !pirateEmpire.PirateEmpireSuperPirates || discoverer.KnownPirateEmpires.Contains(pirateEmpire))
            {
                return;
            }
            Habitat habitat = null;
            habitat = FastFindNearestSystem(x, y);
            if (habitat != null)
            {
                double num = CalculateDistance(habitat.Xpos, habitat.Ypos, x, y);
                if (num > (double)MaxSolarSystemSize * 2.1)
                {
                    habitat = null;
                }
            }
            string empty = string.Empty;
            discoverer.SendEventMessageToEmpire(message: (habitat == null) ? string.Format(TextResolver.GetText("Encounter Message Phantom Pirates"), pirateEmpire.Name, string.Empty) : string.Format(TextResolver.GetText("Encounter Message Phantom Pirates"), pirateEmpire.Name, habitat.Name), eventMessageType: EventMessageType.PhantomPirates, title: TextResolver.GetText("Phantom Pirates Encountered") + "!", additionalData: pirateEmpire, location: habitat);
            discoverer.SendNewsBroadcast(EventMessageType.PhantomPirates, pirateEmpire);
        }

        public void DoEmpireEncounter(Empire discoverer, Empire otherEmpire, StellarObject discoveryLocation)
        {
            if (otherEmpire == IndependentEmpire)
            {
                discoverer?.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.FirstContactPirateOrIndependent, discoveryLocation, otherEmpire);
            }
            else if (discoverer == IndependentEmpire)
            {
                otherEmpire?.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.FirstContactPirateOrIndependent, discoveryLocation, discoverer);
            }
            else if (discoverer != IndependentEmpire && otherEmpire != IndependentEmpire)
            {
                DoSingleEmpireEncounter(discoverer, otherEmpire, discoveryLocation);
                DoSingleEmpireEncounter(otherEmpire, discoverer, discoveryLocation);
                if (otherEmpire.PirateEmpireBaseHabitat != null)
                {
                    discoverer?.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.FirstContactPirateOrIndependent, discoveryLocation, otherEmpire);
                }
                if (discoverer.PirateEmpireBaseHabitat != null)
                {
                    otherEmpire?.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.FirstContactPirateOrIndependent, discoveryLocation, discoverer);
                }
            }
        }

        private void DoSingleEmpireEncounter(Empire discoverer, Empire otherEmpire, StellarObject discoveryLocation)
        {
            if (discoverer == null || otherEmpire == null)
            {
                return;
            }
            if (discoverer.PirateEmpireBaseHabitat == null && otherEmpire.PirateEmpireBaseHabitat == null)
            {
                if (discoverer.DiplomaticRelations == null)
                {
                    return;
                }
                DiplomaticRelation diplomaticRelation = discoverer.DiplomaticRelations[otherEmpire];
                if (diplomaticRelation == null)
                {
                    diplomaticRelation = new DiplomaticRelation(DiplomaticRelationType.NotMet, discoverer, discoverer, otherEmpire, tradeRestrictedResources: false);
                    discoverer.DiplomaticRelations.Add(diplomaticRelation);
                }
                if (diplomaticRelation.Type != 0 || PirateEmpires.Contains(otherEmpire) || otherEmpire == IndependentEmpire)
                {
                    return;
                }
                Habitat habitat = null;
                if (discoveryLocation != null)
                {
                    habitat = FastFindNearestSystem(discoveryLocation.Xpos, discoveryLocation.Ypos);
                    if (habitat != null)
                    {
                        double num = CalculateDistance(habitat.Xpos, habitat.Ypos, discoveryLocation.Xpos, discoveryLocation.Ypos);
                        if (num > (double)MaxSolarSystemSize * 2.1)
                        {
                            habitat = null;
                        }
                    }
                }
                bool flag = true;
                if (StoryReturnOfTheShakturiEnabled)
                {
                    if (discoverer.DominantRace != null && discoverer.DominantRace.Name.ToLower(CultureInfo.InvariantCulture) == "mechanoid")
                    {
                        flag = false;
                    }
                    if (discoverer == PlayerEmpire && otherEmpire.DominantRace != null && otherEmpire.DominantRace.Name.ToLower(CultureInfo.InvariantCulture) == "mechanoid")
                    {
                        string empty = string.Empty;
                        string arg = string.Empty;
                        if (habitat != null)
                        {
                            arg = habitat.Name;
                        }
                        empty += string.Format(TextResolver.GetText("MechanoidEncounter"), arg);
                        empty += "\n\n";
                        Habitat habitat2 = null;
                        if (otherEmpire.Capital != null)
                        {
                            habitat2 = DetermineHabitatSystemStar(otherEmpire.Capital);
                        }
                        string arg2 = string.Empty;
                        if (habitat2 != null)
                        {
                            arg2 = habitat2.Name;
                        }
                        empty += string.Format(TextResolver.GetText("MechanoidEncounterDetail"), arg2);
                        if (otherEmpire.Capital != null && discoverer.CheckSystemExplored(otherEmpire.Capital.SystemIndex))
                        {
                            discoverer.SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, TextResolver.GetText("Ancient Guardians Encountered"), empty, otherEmpire.DominantRace, otherEmpire.Capital);
                        }
                        else if (discoveryLocation != null)
                        {
                            discoverer.SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, TextResolver.GetText("Ancient Guardians Encountered"), empty, otherEmpire.DominantRace, new Point((int)discoveryLocation.Xpos, (int)discoveryLocation.Ypos));
                        }
                        else
                        {
                            discoverer.SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, TextResolver.GetText("Ancient Guardians Encountered"), empty, otherEmpire.DominantRace, null);
                        }
                    }
                    else if (discoverer == PlayerEmpire && otherEmpire.DominantRace != null && otherEmpire.DominantRace == ShakturiActualRace)
                    {
                        string empty2 = string.Empty;
                        string arg3 = string.Empty;
                        if (habitat != null)
                        {
                            arg3 = habitat.Name;
                        }
                        empty2 += string.Format(TextResolver.GetText("ErutkahEncounter"), arg3);
                        if (discoveryLocation != null)
                        {
                            discoverer.SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, TextResolver.GetText("Erutkah Refugees Encountered"), empty2, otherEmpire.DominantRace, new Point((int)discoveryLocation.Xpos, (int)discoveryLocation.Ypos));
                        }
                        else
                        {
                            discoverer.SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, TextResolver.GetText("Erutkah Refugees Encountered"), empty2, otherEmpire.DominantRace, null);
                        }
                        PlayerEmpire.StateMoney += 10000.0;
                        PlayerEmpire.PirateEconomy.PerformIncome(10000.0, PirateIncomeType.Undefined, CurrentStarDate);
                    }
                    if (StoryShakturiEnraged)
                    {
                        if (discoverer.DominantRace != null && discoverer.DominantRace == ShakturiActualRace)
                        {
                            EmpireEvaluation empireEvaluation = discoverer.ObtainEmpireEvaluation(otherEmpire);
                            if (otherEmpire.DominantRace != null && empireEvaluation != null)
                            {
                                if (RaceFamilies.GetIdsBySpecialFunctionCode(1).Contains(otherEmpire.DominantRace.FamilyId))
                                {
                                    empireEvaluation.Bias += 30.0;
                                }
                                else if (RaceFamilies.GetIdsBySpecialFunctionCode(2).Contains(otherEmpire.DominantRace.FamilyId))
                                {
                                    empireEvaluation.Bias -= 30.0;
                                }
                            }
                        }
                        else if (otherEmpire.DominantRace != null && otherEmpire.DominantRace == ShakturiActualRace)
                        {
                            EmpireEvaluation empireEvaluation2 = discoverer.ObtainEmpireEvaluation(otherEmpire);
                            if (discoverer.DominantRace != null && empireEvaluation2 != null)
                            {
                                if (RaceFamilies.GetIdsBySpecialFunctionCode(1).Contains(discoverer.DominantRace.FamilyId))
                                {
                                    empireEvaluation2.Bias += 30.0;
                                }
                                else if (RaceFamilies.GetIdsBySpecialFunctionCode(2).Contains(discoverer.DominantRace.FamilyId))
                                {
                                    empireEvaluation2.Bias -= 30.0;
                                }
                            }
                        }
                    }
                    else if (discoverer.DominantRace != null && discoverer.DominantRace == ShakturiActualRace)
                    {
                        EmpireEvaluation empireEvaluation3 = discoverer.ObtainEmpireEvaluation(otherEmpire);
                        if (otherEmpire.DominantRace != null && empireEvaluation3 != null)
                        {
                            if (RaceFamilies.GetIdsBySpecialFunctionCode(2).Contains(otherEmpire.DominantRace.FamilyId))
                            {
                                empireEvaluation3.Bias = Math.Min(empireEvaluation3.Bias, 0.0);
                            }
                            else if (RaceFamilies.GetIdsBySpecialFunctionCode(1).Contains(otherEmpire.DominantRace.FamilyId))
                            {
                                empireEvaluation3.Bias = Math.Max(empireEvaluation3.Bias, 30.0);
                            }
                        }
                    }
                    else if (otherEmpire.DominantRace != null && otherEmpire.DominantRace == ShakturiActualRace)
                    {
                        EmpireEvaluation empireEvaluation4 = discoverer.ObtainEmpireEvaluation(otherEmpire);
                        if (discoverer.DominantRace != null && empireEvaluation4 != null)
                        {
                            if (RaceFamilies.GetIdsBySpecialFunctionCode(2).Contains(discoverer.DominantRace.FamilyId))
                            {
                                empireEvaluation4.Bias = Math.Min(empireEvaluation4.Bias, 0.0);
                            }
                            else if (RaceFamilies.GetIdsBySpecialFunctionCode(1).Contains(discoverer.DominantRace.FamilyId))
                            {
                                empireEvaluation4.Bias = Math.Max(empireEvaluation4.Bias, 30.0);
                            }
                        }
                    }
                }
                diplomaticRelation.Type = DiplomaticRelationType.None;
                short matchingGameEventIdEmpireEncounter = GetMatchingGameEventIdEmpireEncounter(discoverer, otherEmpire);
                CheckTriggerEvent(matchingGameEventIdEmpireEncounter, discoverer, EventTriggerType.EmpireEncounter, null);
                if (flag)
                {
                    if (discoverer.DominantRace != otherEmpire.DominantRace)
                    {
                        EmpireEvaluation empireEvaluation5 = discoverer.ObtainEmpireEvaluation(otherEmpire);
                        empireEvaluation5.FirstContactPenalty = EmpireEvaluation.FirstContactPenaltyStartAmount * AggressionLevel;
                    }
                }
                else
                {
                    EmpireEvaluation empireEvaluation6 = discoverer.ObtainEmpireEvaluation(otherEmpire);
                    empireEvaluation6.FirstContactPenalty = 0.0;
                }
                string arg4 = string.Empty;
                if (habitat != null)
                {
                    arg4 = habitat.Name;
                }
                string text = string.Format(TextResolver.GetText("We have encountered a new empire"), arg4, otherEmpire.Name);
                if (otherEmpire.DominantRace != null && discoverer.DominantRace != null && otherEmpire.DominantRace == discoverer.DominantRace)
                {
                    text += ". ";
                    text += string.Format(TextResolver.GetText("This empire is predominantly composed of SAME RACE"), otherEmpire.DominantRace.Name);
                }
                else if (otherEmpire.DominantRace != null && discoverer.DominantRace != null && otherEmpire.DominantRace.FamilyId == discoverer.DominantRace.FamilyId)
                {
                    text += ". ";
                    text += string.Format(TextResolver.GetText("This empire is predominantly composed of SAME RACE FAMILY"), otherEmpire.DominantRace.Name);
                }
                if (discoveryLocation != null)
                {
                    discoverer.SendMessageToEmpire(discoverer, EmpireMessageType.EmpireDiscovered, otherEmpire, text, new Point((int)discoveryLocation.Xpos, (int)discoveryLocation.Ypos), string.Empty);
                }
                else
                {
                    discoverer.SendMessageToEmpire(discoverer, EmpireMessageType.EmpireDiscovered, otherEmpire, text);
                }
            }
            else
            {
                if (discoverer.PirateRelations == null)
                {
                    return;
                }
                PirateRelation pirateRelation = discoverer.ObtainPirateRelation(otherEmpire);
                if (pirateRelation.Type != 0)
                {
                    return;
                }
                if (discoverer.PirateEmpireBaseHabitat != null && otherEmpire.PirateEmpireBaseHabitat != null && !discoverer.PirateEmpireSuperPirates && !otherEmpire.PirateEmpireSuperPirates)
                {
                    discoverer.ChangePirateRelationThisSideOnly(otherEmpire, PirateRelationType.None, CurrentStarDate);
                }
                else
                {
                    discoverer.ChangePirateRelationThisSideOnly(otherEmpire, PirateRelationType.None, CurrentStarDate);
                }
                if (otherEmpire.PirateEmpireBaseHabitat != null && discoverer.KnownPirateEmpires != null && !discoverer.KnownPirateEmpires.Contains(otherEmpire))
                {
                    discoverer.KnownPirateEmpires.Add(otherEmpire);
                }
                if (discoverer.PirateEmpireBaseHabitat != null && otherEmpire.KnownPirateEmpires != null && !otherEmpire.KnownPirateEmpires.Contains(discoverer))
                {
                    otherEmpire.KnownPirateEmpires.Add(discoverer);
                }
                Habitat habitat3 = null;
                if (discoveryLocation != null)
                {
                    habitat3 = FastFindNearestSystem(discoveryLocation.Xpos, discoveryLocation.Ypos);
                    if (habitat3 != null)
                    {
                        double num2 = CalculateDistance(habitat3.Xpos, habitat3.Ypos, discoveryLocation.Xpos, discoveryLocation.Ypos);
                        if (num2 > (double)MaxSolarSystemSize * 2.1)
                        {
                            habitat3 = null;
                        }
                    }
                }
                string arg5 = string.Empty;
                if (habitat3 != null)
                {
                    arg5 = habitat3.Name;
                }
                string description = string.Format(TextResolver.GetText("We have encountered a new empire"), arg5, otherEmpire.Name);
                if (discoveryLocation != null)
                {
                    discoverer.SendMessageToEmpire(discoverer, EmpireMessageType.EmpireDiscovered, otherEmpire, description, new Point((int)discoveryLocation.Xpos, (int)discoveryLocation.Ypos), string.Empty);
                }
                else
                {
                    discoverer.SendMessageToEmpire(discoverer, EmpireMessageType.EmpireDiscovered, otherEmpire, description);
                }
            }
        }

        public static string ResolveDifficultyDescription(double difficultyLevel)
        {
            string empty = string.Empty;
            if (difficultyLevel <= 0.7)
            {
                return TextResolver.GetText("Easy");
            }
            if (difficultyLevel <= 1.0)
            {
                return TextResolver.GetText("Normal");
            }
            if (difficultyLevel <= 1.25)
            {
                return TextResolver.GetText("Hard");
            }
            if (difficultyLevel <= 1.6)
            {
                return TextResolver.GetText("Very Hard");
            }
            return TextResolver.GetText("Extreme");
        }

        public void ClearFleetHomeBases(StellarObject currentBase)
        {
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Active && empire.ShipGroups != null)
                {
                    empire.ClearFleetHomeBases(currentBase);
                }
            }
        }

        public void ReevaluateMissionsAgainstHabitat(Habitat habitat, Empire newEmpire)
        {
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                DiplomaticRelationType diplomaticRelationType = DiplomaticRelationType.None;
                if (newEmpire != null && newEmpire != IndependentEmpire)
                {
                    DiplomaticRelation diplomaticRelation = empire.ObtainDiplomaticRelation(newEmpire);
                    diplomaticRelationType = diplomaticRelation.Type;
                }
                if (diplomaticRelationType == DiplomaticRelationType.War || diplomaticRelationType == DiplomaticRelationType.TradeSanctions)
                {
                    continue;
                }
                for (int j = 0; j < empire.ShipGroups.Count; j++)
                {
                    ShipGroup shipGroup = empire.ShipGroups[j];
                    if (shipGroup.Mission != null && shipGroup.Mission.Type != 0 && shipGroup.Mission.TargetHabitat == habitat && (shipGroup.Mission.Type == BuiltObjectMissionType.WaitAndAttack || shipGroup.Mission.Type == BuiltObjectMissionType.Attack || shipGroup.Mission.Type == BuiltObjectMissionType.Blockade || shipGroup.Mission.Type == BuiltObjectMissionType.Bombard || shipGroup.Mission.Type == BuiltObjectMissionType.WaitAndBombard))
                    {
                        shipGroup.CompleteMission();
                    }
                }
            }
        }

        public string DisplayBuiltObjectSubRole(BuiltObjectRole role, BuiltObjectSubRole subRole)
        {
            role.ToString();
            subRole.ToString();
            string empty = string.Empty;
            return empty + ResolveDescription(subRole);
        }

        public static double DetermineAngle(double x1, double y1, double x2, double y2)
        {
            double num = Math.Atan2(y2 - y1, x2 - x1);
            if (double.IsNaN(num))
            {
                num = 0.0;
            }
            return num;
        }

        public Color SelectDiplomaticRelationColor(DiplomaticRelationType diplomaticRelationType)
        {
            Color result = Color.White;
            switch (diplomaticRelationType)
            {
                case DiplomaticRelationType.FreeTradeAgreement:
                    result = Color.FromArgb(0, 255, 0);
                    break;
                case DiplomaticRelationType.MutualDefensePact:
                    result = Color.FromArgb(48, 48, 216);
                    break;
                case DiplomaticRelationType.None:
                    result = Color.FromArgb(128, 128, 128);
                    break;
                case DiplomaticRelationType.Protectorate:
                    result = Color.FromArgb(96, 96, 255);
                    break;
                case DiplomaticRelationType.SubjugatedDominion:
                    result = Color.LightPink;
                    break;
                case DiplomaticRelationType.TradeSanctions:
                    result = Color.Orange;
                    break;
                case DiplomaticRelationType.Truce:
                    result = Color.Yellow;
                    break;
                case DiplomaticRelationType.War:
                    result = Color.FromArgb(255, 0, 0);
                    break;
                case DiplomaticRelationType.NotMet:
                    result = Color.Tan;
                    break;
            }
            return result;
        }

        public void GenerateIndependentTraders()
        {
            long num = 0L;
            for (int i = 0; i < IndependentColonies.Count; i++)
            {
                Habitat habitat = IndependentColonies[i];
                if (habitat.Population.Count > 0)
                {
                    num += habitat.Population.TotalAmount;
                }
            }
            long val = num / 20000000;
            val = Math.Min(val, IndependentColonies.Count * 15);
            int num2 = (int)val - IndependentEmpire.PrivateBuiltObjects.CountNonPirates();
            if (num2 < 0)
            {
                return;
            }
            DesignList designList = new DesignList();
            DesignList designList2 = new DesignList();
            for (int j = 0; j < PopularDesigns.Count; j++)
            {
                Design design = PopularDesigns[j];
                if (design.WarpSpeed > 5000)
                {
                    if (design.SubRole == BuiltObjectSubRole.SmallFreighter)
                    {
                        designList.Add(design);
                    }
                    else if (design.SubRole == BuiltObjectSubRole.MediumFreighter)
                    {
                        designList2.Add(design);
                    }
                }
            }
            if (designList.Count <= 0)
            {
                DesignList designList3 = new DesignList();
                if (IndependentEmpire.Designs != null)
                {
                    designList3 = IndependentEmpire.Designs.GetDesignsBySubRoles(new List<BuiltObjectSubRole> { BuiltObjectSubRole.SmallFreighter });
                }
                if (designList3.Count > 0)
                {
                    designList.AddRange(designList3);
                }
                else
                {
                    DesignSpecification bySubRole = IndependentEmpire.DesignSpecifications.GetBySubRole(BuiltObjectSubRole.SmallFreighter);
                    if (bySubRole != null)
                    {
                        RaceList raceList = new RaceList();
                        raceList.AddRange(Races.ResolvePlayableRaces());
                        raceList.Remove(PlayerEmpire.DominantRace);
                        for (int k = 0; k < raceList.Count; k++)
                        {
                            Design design2 = IndependentEmpire.GenerateDesignFromSpec(bySubRole, 0.0);
                            if (design2 == null)
                            {
                                continue;
                            }
                            int num3 = 0;
                            Race race = raceList[k];
                            if (PlayerEmpire == null || PlayerEmpire.DominantRace == null || race == null || race.DesignPictureFamilyIndex != PlayerEmpire.DominantRace.DesignPictureFamilyIndex)
                            {
                                if (race != null)
                                {
                                    num3 = race.DesignPictureFamilyIndex;
                                }
                                design2.PictureRef = ShipImageHelper.StandardShipImageStartIndex + num3 * ShipImageHelper.ShipSetImageCount + (int)(ResolveLegacySubRole(design2.SubRole) - 1);
                                IndependentEmpire.Designs.Add(design2);
                                designList.Add(design2);
                            }
                        }
                    }
                }
            }
            if (designList2.Count <= 0)
            {
                DesignList designList4 = new DesignList();
                if (IndependentEmpire.Designs != null)
                {
                    designList4 = IndependentEmpire.Designs.GetDesignsBySubRoles(new List<BuiltObjectSubRole> { BuiltObjectSubRole.MediumFreighter });
                }
                if (designList4.Count > 0)
                {
                    designList2.AddRange(designList4);
                }
                else
                {
                    DesignSpecification bySubRole2 = IndependentEmpire.DesignSpecifications.GetBySubRole(BuiltObjectSubRole.MediumFreighter);
                    if (bySubRole2 != null)
                    {
                        RaceList raceList2 = new RaceList();
                        raceList2.AddRange(Races.ResolvePlayableRaces());
                        raceList2.Remove(PlayerEmpire.DominantRace);
                        for (int l = 0; l < raceList2.Count; l++)
                        {
                            Design design3 = IndependentEmpire.GenerateDesignFromSpec(bySubRole2, 0.0);
                            if (design3 == null)
                            {
                                continue;
                            }
                            int num4 = 0;
                            Race race2 = raceList2[l];
                            if (PlayerEmpire == null || PlayerEmpire.DominantRace == null || race2 == null || race2.DesignPictureFamilyIndex != PlayerEmpire.DominantRace.DesignPictureFamilyIndex)
                            {
                                if (race2 != null)
                                {
                                    num4 = race2.DesignPictureFamilyIndex;
                                }
                                design3.PictureRef = ShipImageHelper.StandardShipImageStartIndex + num4 * ShipImageHelper.ShipSetImageCount + (int)(ResolveLegacySubRole(design3.SubRole) - 1);
                                IndependentEmpire.Designs.Add(design3);
                                designList2.Add(design3);
                            }
                        }
                    }
                }
            }
            for (int m = 0; m < num2; m++)
            {
                Design design4 = null;
                if (Rnd.Next(0, 3) == 1)
                {
                    if (designList2.Count > 0)
                    {
                        int index = Rnd.Next(0, designList2.Count);
                        design4 = designList2[index];
                    }
                }
                else if (designList.Count > 0)
                {
                    int index2 = Rnd.Next(0, designList.Count);
                    design4 = designList[index2];
                }
                if (design4 == null)
                {
                    continue;
                }
                bool flag = false;
                int num5 = Rnd.Next(0, IndependentColonies.Count);
                for (int n = num5; n < IndependentColonies.Count; n++)
                {
                    if (!PlayerEmpire.IsObjectVisibleToThisEmpire(IndependentColonies[n]))
                    {
                        design4.BuildCount++;
                        string text = design4.Name + " " + design4.BuildCount.ToString("000");
                        text = SelectRandomUniqueStandardShipName(IndependentColonies[n]);
                        BuiltObject builtObject = new BuiltObject(design4, text, this, fullyBuilt: true);
                        builtObject.Empire = IndependentEmpire;
                        builtObject.Heading = SelectRandomHeading();
                        builtObject.TargetHeading = builtObject.Heading;
                        builtObject.ReDefine();
                        builtObject.CurrentFuel = builtObject.FuelCapacity;
                        IndependentEmpire.AddBuiltObjectToGalaxy(builtObject, IndependentColonies[n], offsetLocationFromParent: false, isStateOwned: false);
                        SelectRelativeParkingPoint(out var x, out var y);
                        builtObject.ParentOffsetX = x;
                        builtObject.ParentOffsetY = y;
                        builtObject.Xpos = IndependentColonies[n].Xpos + x;
                        builtObject.Ypos = IndependentColonies[n].Ypos + y;
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    continue;
                }
                for (int num6 = 0; num6 < num5; num6++)
                {
                    if (!PlayerEmpire.IsObjectVisibleToThisEmpire(IndependentColonies[num6]))
                    {
                        design4.BuildCount++;
                        string text2 = design4.Name + " " + design4.BuildCount.ToString("000");
                        text2 = SelectRandomUniqueStandardShipName(IndependentColonies[num6]);
                        BuiltObject builtObject2 = new BuiltObject(design4, text2, this, fullyBuilt: true);
                        builtObject2.Empire = IndependentEmpire;
                        builtObject2.Heading = SelectRandomHeading();
                        builtObject2.TargetHeading = builtObject2.Heading;
                        builtObject2.ReDefine();
                        builtObject2.CurrentFuel = builtObject2.FuelCapacity;
                        IndependentEmpire.AddBuiltObjectToGalaxy(builtObject2, IndependentColonies[num6], offsetLocationFromParent: false, isStateOwned: false);
                        SelectRelativeParkingPoint(out var x2, out var y2);
                        builtObject2.ParentOffsetX = x2;
                        builtObject2.ParentOffsetY = y2;
                        builtObject2.Xpos = IndependentColonies[num6].Xpos + x2;
                        builtObject2.Ypos = IndependentColonies[num6].Ypos + y2;
                        flag = true;
                        break;
                    }
                }
            }
        }

        public void AssignIndependentTraderMissions()
        {
            long num = CurrentStarDate - (long)RetirementYears * (long)RealSecondsInGalacticYear * 1000;
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < IndependentEmpire.PrivateBuiltObjects.Count; i++)
            {
                BuiltObject builtObject = IndependentEmpire.PrivateBuiltObjects[i];
                if (builtObject.PirateEmpireId > 0 || builtObject.Role != BuiltObjectRole.Freight || (builtObject.Mission != null && builtObject.Mission.Type != 0))
                {
                    continue;
                }
                if (builtObject.RetireForNextMission || builtObject.DateBuilt <= num)
                {
                    if (!PlayerEmpire.IsObjectVisibleToThisEmpire(builtObject))
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
                else
                {
                    if (!builtObject.RefuelForNextMission)
                    {
                        continue;
                    }
                    ResourceList fuelTypes = builtObject.DetermineFuelRequired();
                    StellarObject stellarObject = FastFindNearestRefuellingPoint(builtObject.Xpos, builtObject.Ypos, fuelTypes, builtObject.ActualEmpire, builtObject);
                    if (stellarObject != null)
                    {
                        if (stellarObject is BuiltObject)
                        {
                            BuiltObject target = (BuiltObject)stellarObject;
                            builtObject.AssignMission(BuiltObjectMissionType.Refuel, target, null, BuiltObjectMissionPriority.Normal);
                            builtObject.RefuelForNextMission = false;
                        }
                        else if (stellarObject is Habitat)
                        {
                            Habitat target2 = (Habitat)stellarObject;
                            builtObject.AssignMission(BuiltObjectMissionType.Refuel, target2, null, BuiltObjectMissionPriority.Normal);
                            builtObject.RefuelForNextMission = false;
                        }
                    }
                }
            }
            foreach (BuiltObject item in builtObjectList)
            {
                item.CompleteTeardown(this);
            }
        }

        public bool AssignFleetWaypointMission(BuiltObject builtObject, bool allowMissionOverride, StellarObject waypoint)
        {
            if (builtObject != null && builtObject.ShipGroup != null && builtObject.TopSpeed > 0 && builtObject.Role != BuiltObjectRole.Base && (allowMissionOverride || builtObject.Mission == null || (builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Undefined)))
            {
                object obj = waypoint;
                Point point = Point.Empty;
                if (obj == null)
                {
                    if (builtObject.ShipGroup.LeadShip != null && builtObject.ShipGroup.LeadShip != builtObject)
                    {
                        BuiltObject leadShip = builtObject.ShipGroup.LeadShip;
                        if (leadShip != null)
                        {
                            if (leadShip.Mission != null && (leadShip.Mission.Type == BuiltObjectMissionType.Blockade || leadShip.Mission.Type == BuiltObjectMissionType.Move || leadShip.Mission.Type == BuiltObjectMissionType.MoveAndWait || leadShip.Mission.Type == BuiltObjectMissionType.Hold || leadShip.Mission.Type == BuiltObjectMissionType.Retrofit || leadShip.Mission.Type == BuiltObjectMissionType.Patrol || leadShip.Mission.Type == BuiltObjectMissionType.Refuel || leadShip.Mission.Type == BuiltObjectMissionType.Repair || leadShip.Mission.Type == BuiltObjectMissionType.Waypoint))
                            {
                                if (leadShip.Mission.Target != null)
                                {
                                    obj = leadShip.Mission.Target;
                                }
                                else
                                {
                                    point = new Point((int)leadShip.Xpos, (int)leadShip.Ypos);
                                }
                            }
                            else if (leadShip.ParentHabitat != null)
                            {
                                obj = leadShip.ParentHabitat;
                            }
                            else if (leadShip.ParentBuiltObject != null)
                            {
                                obj = leadShip.ParentBuiltObject;
                            }
                            else if ((leadShip.Mission == null || leadShip.Mission.Type == BuiltObjectMissionType.Undefined) && leadShip.CurrentSpeed <= 0f)
                            {
                                point = new Point((int)leadShip.Xpos, (int)leadShip.Ypos);
                            }
                            else
                            {
                                obj = builtObject.ShipGroup.GatherPoint;
                            }
                        }
                        else
                        {
                            obj = builtObject.ShipGroup.GatherPoint;
                        }
                    }
                    else
                    {
                        obj = builtObject.ShipGroup.GatherPoint;
                    }
                }
                SelectRelativeParkingPoint(out var x, out var y);
                builtObject.ClearPreviousMissionRequirements();
                if (obj != null)
                {
                    builtObject.AssignMission(BuiltObjectMissionType.Move, obj, null, x, y, BuiltObjectMissionPriority.Low, manuallyAssigned: false);
                }
                else if (!point.IsEmpty)
                {
                    builtObject.AssignMission(BuiltObjectMissionType.Move, null, null, (double)point.X + x, (double)point.Y + y, BuiltObjectMissionPriority.Low, manuallyAssigned: false);
                }
                return true;
            }
            return false;
        }

        private DesignList FindPopularDesigns(DesignList designs, Empire empire, BuiltObjectSubRole subRole, ref int lowestDesignIndex, ref int lowestDesignAmount)
        {
            if (empire.DominantRace != null && (empire.DominantRace.Name.ToLower(CultureInfo.InvariantCulture) == "mechanoid" || empire.DominantRace == ShakturiActualRace))
            {
                return designs;
            }
            for (int i = 0; i < empire.Designs.Count; i++)
            {
                Design design = empire.Designs[i];
                if (design.IsObsolete || design.SubRole != subRole || (design.BuildCount <= lowestDesignAmount && (designs.Count >= 3 || design.BuildCount < lowestDesignAmount || design.OptimizedDesign != 0)))
                {
                    continue;
                }
                if (designs.Count < 3)
                {
                    designs.Add(design);
                    lowestDesignAmount = 0;
                }
                else
                {
                    designs[lowestDesignIndex] = design;
                    lowestDesignAmount = 536870911;
                }
                for (int j = 0; j < designs.Count; j++)
                {
                    if (designs[j].BuildCount < lowestDesignAmount)
                    {
                        lowestDesignAmount = designs[j].BuildCount;
                        lowestDesignIndex = j;
                    }
                }
            }
            return designs;
        }

        public void SelectPopularDesignCandidates()
        {
            int lowestDesignIndex = 0;
            int lowestDesignAmount = 0;
            DesignList designList = new DesignList();
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                designList = FindPopularDesigns(designList, empire, BuiltObjectSubRole.SmallFreighter, ref lowestDesignIndex, ref lowestDesignAmount);
            }
            lowestDesignIndex = 0;
            lowestDesignAmount = 0;
            DesignList designList2 = new DesignList();
            for (int j = 0; j < Empires.Count; j++)
            {
                Empire empire2 = Empires[j];
                designList2 = FindPopularDesigns(designList2, empire2, BuiltObjectSubRole.MediumFreighter, ref lowestDesignIndex, ref lowestDesignAmount);
            }
            lowestDesignIndex = 0;
            lowestDesignAmount = 0;
            DesignList designList3 = new DesignList();
            for (int k = 0; k < Empires.Count; k++)
            {
                Empire empire3 = Empires[k];
                designList3 = FindPopularDesigns(designList3, empire3, BuiltObjectSubRole.Escort, ref lowestDesignIndex, ref lowestDesignAmount);
            }
            lowestDesignIndex = 0;
            lowestDesignAmount = 0;
            DesignList designList4 = new DesignList();
            for (int l = 0; l < Empires.Count; l++)
            {
                Empire empire4 = Empires[l];
                designList4 = FindPopularDesigns(designList4, empire4, BuiltObjectSubRole.Frigate, ref lowestDesignIndex, ref lowestDesignAmount);
            }
            lowestDesignIndex = 0;
            lowestDesignAmount = 0;
            DesignList designList5 = new DesignList();
            for (int m = 0; m < Empires.Count; m++)
            {
                Empire empire5 = Empires[m];
                designList5 = FindPopularDesigns(designList5, empire5, BuiltObjectSubRole.Destroyer, ref lowestDesignIndex, ref lowestDesignAmount);
            }
            lowestDesignIndex = 0;
            lowestDesignAmount = 0;
            DesignList designList6 = new DesignList();
            for (int n = 0; n < Empires.Count; n++)
            {
                Empire empire6 = Empires[n];
                designList6 = FindPopularDesigns(designList6, empire6, BuiltObjectSubRole.Cruiser, ref lowestDesignIndex, ref lowestDesignAmount);
            }
            PopularDesigns.Clear();
            PopularDesigns = AppendNonNullDesigns(PopularDesigns, designList);
            PopularDesigns = AppendNonNullDesigns(PopularDesigns, designList2);
            PopularDesigns = AppendNonNullDesigns(PopularDesigns, designList3);
            PopularDesigns = AppendNonNullDesigns(PopularDesigns, designList4);
            PopularDesigns = AppendNonNullDesigns(PopularDesigns, designList5);
            PopularDesigns = AppendNonNullDesigns(PopularDesigns, designList6);
        }

        private bool AddNewDesignToPirateEmpire(Empire empire, Design newDesign)
        {
            List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
            list.Add(newDesign.SubRole);
            DesignList designsBySubRoles = empire.Designs.GetDesignsBySubRoles(list);
            if (designsBySubRoles == null || designsBySubRoles.Count == 0)
            {
                Design design = newDesign.Clone();
                design.IsObsolete = false;
                design.Empire = empire;
                if (design.Role == BuiltObjectRole.Military)
                {
                    design.FleeWhen = BuiltObjectFleeWhen.Shields50;
                }
                empire.Designs.Add(design);
                return true;
            }
            foreach (Design item in designsBySubRoles)
            {
                if (!item.IsEquivalent(newDesign))
                {
                    item.IsObsolete = true;
                    Design design2 = newDesign.Clone();
                    design2.IsObsolete = false;
                    if (design2.Role == BuiltObjectRole.Military)
                    {
                        design2.FleeWhen = BuiltObjectFleeWhen.Shields50;
                    }
                    design2.Empire = empire;
                    empire.Designs.Add(design2);
                    return true;
                }
            }
            return false;
        }

        private DesignList AppendNonNullDesigns(DesignList masterDesigns, DesignList childDesigns)
        {
            foreach (Design childDesign in childDesigns)
            {
                if (childDesign != null)
                {
                    Design design = childDesign.Clone();
                    design.Empire = IndependentEmpire;
                    if (design.Role == BuiltObjectRole.Military)
                    {
                        design.Stance = BuiltObjectStance.AttackUnallied;
                    }
                    design.BuildCount = 0;
                    design.DateCreated = CurrentStarDate;
                    design.ReDefine();
                    masterDesigns.Add(design);
                }
            }
            return masterDesigns;
        }

        public void DamageBuiltObjectComponents(BuiltObject builtObject, double damagePortion)
        {
            damagePortion = Math.Min(1.0, Math.Max(damagePortion, 0.0));
            int num = (int)((double)builtObject.Components.Count * damagePortion);
            if (num >= builtObject.Components.Count)
            {
                num = builtObject.Components.Count - 1;
            }
            int num2 = 0;
            for (int i = 0; i < builtObject.Components.Count; i++)
            {
                builtObject.Components[i].Status = ComponentStatus.Damaged;
                num2++;
                if (num2 >= num)
                {
                    break;
                }
            }
            builtObject.ReDefine();
        }

        public BuiltObject GenerateIncompletePlanetDestroyer(string name, Habitat parentHabitat)
        {
            Design design = GeneratePlanetDestroyerDesign();
            SelectRelativeParkingPoint(out var x, out var y);
            BuiltObject builtObject = GenerateUnownedBuiltObjectFromDesign(design, name, null, parentHabitat.Xpos + x, parentHabitat.Ypos + y);
            builtObject.PlayerEmpireEncounterAction = BuiltObjectEncounterAction.Notify;
            for (int i = 0; i < builtObject.Components.Count; i++)
            {
                if (Rnd.Next(0, 3) < 2)
                {
                    builtObject.Components[i].Status = ComponentStatus.Unbuilt;
                }
            }
            builtObject.ReDefine();
            return builtObject;
        }

        public Design GeneratePlanetDestroyerDesign()
        {
            return GeneratePlanetDestroyerDesign(1.0);
        }

        public Design GeneratePlanetDestroyerDesign(double overpowerFactor)
        {
            return GeneratePlanetDestroyerDesign(overpowerFactor, null);
        }

        public Design GeneratePlanetDestroyerDesign(double overpowerFactor, Empire empire)
        {
            ComponentList planetDestroyerComponents = GetPlanetDestroyerComponents(overpowerFactor, empire);
            string text = TextResolver.GetText("World Destroyer");
            Design design = new Design(text);
            design.Role = BuiltObjectRole.Military;
            design.SubRole = BuiltObjectSubRole.CapitalShip;
            design = AddComponentsToDesign(design, planetDestroyerComponents, null);
            design.Stance = BuiltObjectStance.AttackEnemies;
            design.FleeWhen = BuiltObjectFleeWhen.Shields20;
            design.TacticsStrongerShips = BattleTactics.Standoff;
            design.TacticsWeakerShips = BattleTactics.AllWeapons;
            design.TacticsInvasion = InvasionTactics.DoNotInvade;
            design.Name = text;
            design.DateCreated = CurrentStarDate;
            design.Empire = empire;
            design.PictureRef = ShipImageHelper.PlanetDestroyer;
            design.ReDefine();
            return design;
        }

        public void SetEmpireKnownGalacticHistoryLocations(Empire empire, int amount, double x, double y, GalaxyLocationList locationsToExclude)
        {
            if (amount <= 0)
            {
                return;
            }
            GalaxyLocationList galaxyLocationList = GalaxyLocations.FindLocations(GalaxyLocationType.RestrictedArea);
            if (galaxyLocationList.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < amount; i++)
            {
                GalaxyLocation galaxyLocation = null;
                double num = double.MaxValue;
                for (int j = 0; j < galaxyLocationList.Count; j++)
                {
                    GalaxyLocation galaxyLocation2 = galaxyLocationList[j];
                    if (galaxyLocation2 != null && !locationsToExclude.Contains(galaxyLocation2) && !empire.KnownGalaxyLocations.Contains(galaxyLocation2))
                    {
                        double num2 = CalculateDistance(x, y, galaxyLocation2.Xpos, galaxyLocation2.Ypos);
                        if (num2 < num)
                        {
                            galaxyLocation = galaxyLocation2;
                            num = num2;
                        }
                    }
                }
                if (galaxyLocation != null)
                {
                    if (!empire.KnownGalaxyLocations.Contains(galaxyLocation))
                    {
                        empire.KnownGalaxyLocations.Add(galaxyLocation);
                    }
                    galaxyLocationList.Remove(galaxyLocation);
                }
            }
        }

        public void SetEmpireExplorationAmount(Empire empire, int systemAmount)
        {
            if (empire.ResourceMap != null)
            {
                for (int i = 0; i < Habitats.Count; i++)
                {
                    if (Habitats[i].Parent == null)
                    {
                        empire.ResourceMap.SetResourcesKnown(Habitats[i], known: false);
                    }
                }
            }
            int val = systemAmount;
            val = Math.Min(val, StarCount);
            for (int j = 0; j < val; j++)
            {
                Habitat habitat = FindNearestUnexploredHabitat(empire.Capital.Xpos, empire.Capital.Ypos, empire, includeAsteroids: true);
                if (habitat == null)
                {
                    continue;
                }
                Habitat habitat2 = DetermineHabitatSystemStar(habitat);
                int num = Habitats.IndexOf(habitat2);
                if (num < 0)
                {
                    continue;
                }
                if (empire.ResourceMap != null)
                {
                    empire.ResourceMap.SetResourcesKnown(Habitats[num], known: true);
                }
                SystemVisibilityStatus systemVisibilityStatus = empire.CheckSystemVisibilityStatus(habitat2);
                if (habitat2.Category == HabitatCategoryType.GasCloud)
                {
                    if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored)
                    {
                        empire.SetSystemVisibility(habitat2, SystemVisibilityStatus.Explored);
                    }
                    j--;
                    continue;
                }
                if (habitat2.Category == HabitatCategoryType.Asteroid)
                {
                    if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored)
                    {
                        empire.SetSystemVisibility(habitat2, SystemVisibilityStatus.Explored);
                    }
                    j--;
                    continue;
                }
                int num2 = num + 1;
                if (num2 < Habitats.Count)
                {
                    while (Habitats[num2].Parent != null && num2 < Habitats.Count)
                    {
                        if (empire.ResourceMap != null)
                        {
                            empire.ResourceMap.SetResourcesKnown(Habitats[num2], known: true);
                        }
                        num2++;
                        if (num2 >= Habitats.Count)
                        {
                            break;
                        }
                    }
                }
                if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored)
                {
                    empire.SetSystemVisibility(habitat2, SystemVisibilityStatus.Explored);
                }
            }
            if (empire.ResourceMap == null)
            {
                return;
            }
            for (int k = 0; k < Habitats.Count; k++)
            {
                if (Habitats[k].Parent == null && Habitats[k].Category != HabitatCategoryType.GasCloud)
                {
                    empire.ResourceMap.SetResourcesKnown(Habitats[k], known: true);
                }
            }
        }

        public static void ApplyDesignUpgradePoliciesToGameOptions(GameOptions gameOptions, EmpirePolicy policy)
        {
            if (gameOptions != null && policy != null)
            {
                gameOptions.DesignUpgradeCapitalShip = policy.DesignUpgradeCapitalShip;
                gameOptions.DesignUpgradeCarrier = policy.DesignUpgradeCarrier;
                gameOptions.DesignUpgradeColonyShip = policy.DesignUpgradeColonyShip;
                gameOptions.DesignUpgradeConstructionShip = policy.DesignUpgradeConstructionShip;
                gameOptions.DesignUpgradeCruiser = policy.DesignUpgradeCruiser;
                gameOptions.DesignUpgradeDefensiveBase = policy.DesignUpgradeDefensiveBase;
                gameOptions.DesignUpgradeDestroyer = policy.DesignUpgradeDestroyer;
                gameOptions.DesignUpgradeEnergyResearchStation = policy.DesignUpgradeEnergyResearchStation;
                gameOptions.DesignUpgradeEscort = policy.DesignUpgradeEscort;
                gameOptions.DesignUpgradeExplorationShip = policy.DesignUpgradeExplorationShip;
                gameOptions.DesignUpgradeFrigate = policy.DesignUpgradeFrigate;
                gameOptions.DesignUpgradeGasMiningShip = policy.DesignUpgradeGasMiningShip;
                gameOptions.DesignUpgradeGasMiningStation = policy.DesignUpgradeGasMiningStation;
                gameOptions.DesignUpgradeGenericBase = policy.DesignUpgradeGenericBase;
                gameOptions.DesignUpgradeHighTechResearchStation = policy.DesignUpgradeHighTechResearchStation;
                gameOptions.DesignUpgradeLargeFreighter = policy.DesignUpgradeLargeFreighter;
                gameOptions.DesignUpgradeLargeSpacePort = policy.DesignUpgradeLargeSpacePort;
                gameOptions.DesignUpgradeMediumFreighter = policy.DesignUpgradeMediumFreighter;
                gameOptions.DesignUpgradeMediumSpacePort = policy.DesignUpgradeMediumSpacePort;
                gameOptions.DesignUpgradeMiningShip = policy.DesignUpgradeMiningShip;
                gameOptions.DesignUpgradeMiningStation = policy.DesignUpgradeMiningStation;
                gameOptions.DesignUpgradeMonitoringStation = policy.DesignUpgradeMonitoringStation;
                gameOptions.DesignUpgradePassengerShip = policy.DesignUpgradePassengerShip;
                gameOptions.DesignUpgradeResortBase = policy.DesignUpgradeResortBase;
                gameOptions.DesignUpgradeResupplyShip = policy.DesignUpgradeResupplyShip;
                gameOptions.DesignUpgradeSmallFreighter = policy.DesignUpgradeSmallFreighter;
                gameOptions.DesignUpgradeSmallSpacePort = policy.DesignUpgradeSmallSpacePort;
                gameOptions.DesignUpgradeTroopTransport = policy.DesignUpgradeTroopTransport;
                gameOptions.DesignUpgradeWeaponsResearchStation = policy.DesignUpgradeWeaponsResearchStation;
            }
        }

        public static void ApplyDesignUpgradeGameOptionsToPolicies(GameOptions gameOptions, EmpirePolicy policy)
        {
            if (gameOptions != null && policy != null)
            {
                policy.DesignUpgradeCapitalShip = gameOptions.DesignUpgradeCapitalShip;
                policy.DesignUpgradeCarrier = gameOptions.DesignUpgradeCarrier;
                policy.DesignUpgradeColonyShip = gameOptions.DesignUpgradeColonyShip;
                policy.DesignUpgradeConstructionShip = gameOptions.DesignUpgradeConstructionShip;
                policy.DesignUpgradeCruiser = gameOptions.DesignUpgradeCruiser;
                policy.DesignUpgradeDefensiveBase = gameOptions.DesignUpgradeDefensiveBase;
                policy.DesignUpgradeDestroyer = gameOptions.DesignUpgradeDestroyer;
                policy.DesignUpgradeEnergyResearchStation = gameOptions.DesignUpgradeEnergyResearchStation;
                policy.DesignUpgradeEscort = gameOptions.DesignUpgradeEscort;
                policy.DesignUpgradeExplorationShip = gameOptions.DesignUpgradeExplorationShip;
                policy.DesignUpgradeFrigate = gameOptions.DesignUpgradeFrigate;
                policy.DesignUpgradeGasMiningShip = gameOptions.DesignUpgradeGasMiningShip;
                policy.DesignUpgradeGasMiningStation = gameOptions.DesignUpgradeGasMiningStation;
                policy.DesignUpgradeGenericBase = gameOptions.DesignUpgradeGenericBase;
                policy.DesignUpgradeHighTechResearchStation = gameOptions.DesignUpgradeHighTechResearchStation;
                policy.DesignUpgradeLargeFreighter = gameOptions.DesignUpgradeLargeFreighter;
                policy.DesignUpgradeLargeSpacePort = gameOptions.DesignUpgradeLargeSpacePort;
                policy.DesignUpgradeMediumFreighter = gameOptions.DesignUpgradeMediumFreighter;
                policy.DesignUpgradeMediumSpacePort = gameOptions.DesignUpgradeMediumSpacePort;
                policy.DesignUpgradeMiningShip = gameOptions.DesignUpgradeMiningShip;
                policy.DesignUpgradeMiningStation = gameOptions.DesignUpgradeMiningStation;
                policy.DesignUpgradeMonitoringStation = gameOptions.DesignUpgradeMonitoringStation;
                policy.DesignUpgradePassengerShip = gameOptions.DesignUpgradePassengerShip;
                policy.DesignUpgradeResortBase = gameOptions.DesignUpgradeResortBase;
                policy.DesignUpgradeResupplyShip = gameOptions.DesignUpgradeResupplyShip;
                policy.DesignUpgradeSmallFreighter = gameOptions.DesignUpgradeSmallFreighter;
                policy.DesignUpgradeSmallSpacePort = gameOptions.DesignUpgradeSmallSpacePort;
                policy.DesignUpgradeTroopTransport = gameOptions.DesignUpgradeTroopTransport;
                policy.DesignUpgradeWeaponsResearchStation = gameOptions.DesignUpgradeWeaponsResearchStation;
            }
        }

        public Empire GenerateEmpire(Galaxy galaxy, bool isPlayerEmpire, string empireName, Habitat capital, Race race, int designPictureFamilyIndex, int governmentId, double homeSystemFactor, string homeSystemDescription, int age, double techLevel, double corruptionMultiplier, out double expansion, GameOptions gameOptions, VictoryConditions globalVictoryConditions)
        {
            double actualTechLevel = 1.0;
            string raceNameOverride = string.Empty;
            if (race != null)
            {
                raceNameOverride = race.Name;
            }
            return GenerateEmpire(galaxy, isPlayerEmpire, empireName, capital, race, designPictureFamilyIndex, governmentId, homeSystemFactor, homeSystemDescription, age, techLevel, corruptionMultiplier, out expansion, gameOptions, globalVictoryConditions, out actualTechLevel, raceNameOverride);
        }

        public Empire GenerateEmpire(Galaxy galaxy, bool isPlayerEmpire, string empireName, Habitat capital, Race race, int designPictureFamilyIndex, int governmentId, double homeSystemFactor, string homeSystemDescription, int age, double techLevel, double corruptionMultiplier, out double expansion, GameOptions gameOptions, VictoryConditions globalVictoryConditions, out double actualTechLevel, string raceNameOverride)
        {
            actualTechLevel = 1.0;
            EmpirePolicy empirePolicy = LoadEmpirePolicy(race, isPirate: false);
            if (isPlayerEmpire)
            {
                empirePolicy.ImplementEnslavementWithPenalColonies = false;
            }
            Empire empire = new Empire(galaxy, empireName, capital, race, governmentId, corruptionMultiplier, empirePolicy, isPlayerEmpire);
            if (techLevel < 0.0)
            {
                double num = 0.5;
                double num2 = 5.99;
                switch (galaxy.Age)
                {
                    case 0:
                        num = 0.0;
                        num2 = 0.0;
                        break;
                    case 1:
                        num = 0.5;
                        num2 = 0.99;
                        break;
                    case 2:
                        num = 0.5;
                        num2 = 1.99;
                        break;
                    case 3:
                        num = 1.0;
                        num2 = 2.99;
                        break;
                    case 4:
                        num = 2.0;
                        num2 = 3.99;
                        break;
                    case 5:
                        num = 3.0;
                        num2 = 4.99;
                        break;
                    case 6:
                        num = 4.0;
                        num2 = 5.99;
                        break;
                }
                techLevel = num + Rnd.NextDouble() * (num2 - num);
            }
            actualTechLevel = techLevel;
            if (age < 0)
            {
                int minValue = 0;
                int num3 = 6;
                switch (galaxy.Age)
                {
                    case 0:
                        minValue = 0;
                        num3 = 0;
                        break;
                    case 1:
                        minValue = 1;
                        num3 = 2;
                        break;
                    case 2:
                        minValue = 1;
                        num3 = 2;
                        break;
                    case 3:
                        minValue = 2;
                        num3 = 3;
                        break;
                    case 4:
                        minValue = 3;
                        num3 = 4;
                        break;
                    case 5:
                        minValue = 3;
                        num3 = 5;
                        break;
                    case 6:
                        minValue = 4;
                        num3 = 6;
                        break;
                }
                age = Rnd.Next(minValue, num3 + 1);
                age = Math.Max(0, Math.Min(6, age));
            }
            if (designPictureFamilyIndex >= 0)
            {
                empire.DesignPictureFamilyIndex = designPictureFamilyIndex;
            }
            if (capital.Ruin != null && (capital.Ruin.Type == RuinType.Standard || capital.Ruin.Type == RuinType.CreatureSwarm || capital.Ruin.Type == RuinType.PirateAmbush))
            {
                capital.Ruin = null;
                if (galaxy.RuinsHabitats.Contains(capital))
                {
                    galaxy.RuinsHabitats.Remove(capital);
                }
            }
            empire.TakeOwnershipOfColony(capital, empire);
            if (techLevel > 0.0 || (globalVictoryConditions != null && !globalVictoryConditions.EnableStoryEventsShadows))
            {
                empire.PreWarpProgressEventOccurredSendPirateRaid = true;
                empire.PreWarpProgressEventOccurredExperienceFirstPirateRaid = true;
                empire.PreWarpProgressEventOccurredFirstContactPirateOrIndependent = true;
                empire.PreWarpProgressEventOccurredFirstContactNormalEmpire = true;
                empire.PreWarpProgressEventOccurredBuildFirstShip = true;
                empire.PreWarpProgressEventOccurredBuildFirstSpaceport = true;
                empire.PreWarpProgressEventOccurredBuildFirstMiningStation = true;
                empire.PreWarpProgressEventOccurredBuildFirstResearchStation = true;
                empire.PreWarpProgressEventOccurredDiscoverHyperspaceTech = true;
                empire.PreWarpProgressEventOccurredDiscoverColonizationTech = true;
                empire.PreWarpProgressEventOccurredFirstHyperjump = true;
                empire.PreWarpProgressEventOccurredEncounterFirstKaltor = true;
                empire.PreWarpProgressEventOccurredBuildFirstMilitaryShip = true;
            }
            int minimumResourceCount = 5;
            int minimumCriticalResourceCount = 3;
            int num4 = Rnd.Next(0, 8);
            if (homeSystemDescription == TextResolver.GetText("Harsh"))
            {
                minimumResourceCount = 3;
                minimumCriticalResourceCount = 1;
                capital.Diameter = (short)(260 + num4);
                capital.BaseQuality = (float)(0.65 + Rnd.NextDouble() * 0.06);
            }
            else if (homeSystemDescription == TextResolver.GetText("Trying"))
            {
                minimumResourceCount = 4;
                minimumCriticalResourceCount = 2;
                capital.Diameter = (short)(275 + num4);
                capital.BaseQuality = (float)(0.73 + Rnd.NextDouble() * 0.06);
            }
            else if (homeSystemDescription == TextResolver.GetText("Normal"))
            {
                minimumResourceCount = 5;
                minimumCriticalResourceCount = 3;
                capital.Diameter = (short)(290 + num4);
                capital.BaseQuality = (float)(0.82 + Rnd.NextDouble() * 0.06);
            }
            else if (homeSystemDescription == TextResolver.GetText("Agreeable"))
            {
                minimumResourceCount = 5;
                minimumCriticalResourceCount = 3;
                capital.Diameter = (short)(305 + num4);
                capital.BaseQuality = (float)(0.9 + Rnd.NextDouble() * 0.05);
            }
            else if (homeSystemDescription == TextResolver.GetText("Excellent"))
            {
                minimumResourceCount = 5;
                minimumCriticalResourceCount = 3;
                capital.Diameter = (short)(320 + num4);
                capital.BaseQuality = (float)(0.97 + Rnd.NextDouble() * 0.03);
            }
            capital.IsRefuellingDepot = true;
            _ = capital.Population.Count;
            _ = 0;
            long num5 = (long)(homeSystemFactor * 2200000000.0 + homeSystemFactor * Rnd.NextDouble() * 500000000.0);
            if (age > 0)
            {
                num5 = (long)((double)num5 * Math.Pow(1.7, age));
            }
            long val = (long)(homeSystemFactor * 10000000000.0 + homeSystemFactor * Rnd.NextDouble() * 1000000000.0);
            if (age == 0)
            {
                val = (long)(homeSystemFactor * 2200000000.0 + homeSystemFactor * Rnd.NextDouble() * 500000000.0);
            }
            num5 = Math.Max(num5, val);
            Population population = new Population(race, num5);
            capital.Population.Add(population);
            capital.Population.TotalAmount = num5;
            capital.GrowPopulation(new TimeSpan(0L));
            empire.ControlColonization = AutomationLevel.FullyAutomated;
            empire.ControlColonyDevelopment = true;
            empire.ControlColonyStockLevels = true;
            empire.ControlColonyTaxRates = true;
            empire.ControlDesigns = true;
            empire.ControlDiplomacyGifts = AutomationLevel.FullyAutomated;
            empire.ControlDiplomacyOffense = AutomationLevel.FullyAutomated;
            empire.ControlDiplomacyTreaties = AutomationLevel.FullyAutomated;
            empire.ControlMilitaryAttacks = AutomationLevel.FullyAutomated;
            empire.ControlMilitaryFleets = true;
            empire.ControlStateConstruction = AutomationLevel.FullyAutomated;
            empire.ControlTroopGeneration = true;
            empire.ControlAgentAssignment = AutomationLevel.FullyAutomated;
            empire.ControlResearch = true;
            empire.ControlPopulationPolicy = true;
            empire.ControlColonyFacilities = AutomationLevel.FullyAutomated;
            empire.ControlCharacterLocations = true;
            empire.ControlOfferPirateMissions = AutomationLevel.FullyAutomated;
            galaxy.Empires.Add(empire);
            empire.ResolveSystemVisibility(capital.Xpos, capital.Ypos, null, null);
            expansion = DetermineEmpireExpansion(Rnd, age);
            empire.PrivateMoney = 40000.0 + (expansion + 2.0) * 3000.0;
            empire.StateMoney = 15000.0 + (expansion + 2.0) * 1500.0;
            empire.GenerateDesignSpecifications(galaxy, race, isPirate: false, raceNameOverride);
            empire.Research.TechTree = ResearchNodeDefinitionsStatic.SetTechTreeLevel(galaxy, empire.Research.TechTree, race, techLevel, isPirate: false);
            empire.Research.Update(race);
            empire.ReviewResearchAbilities();
            LoadOptimizedDesignsForEmpire(empire, galaxy.ApplicationStartupPath, galaxy.CustomizationSetPath, galaxy.CurrentStarDate);
            empire.ReviewDesignsBuiltObjectsImprovedComponents();
            empire.ReviewTroopTypes();
            empire.SetStartupColonyResourceCargo(capital);
            capital.SetDevelopmentLevel(10);
            capital.DoTasks(galaxy.CurrentDateTime);
            int num6 = capital.EstimatedDefensiveForceRequired(atWar: false) * 2;
            if (num6 > ColonyMaximumTroopStrength / 100)
            {
                num6 = ColonyMaximumTroopStrength / 100;
            }
            int num7 = (int)((double)num6 * Rnd.NextDouble());
            int num8 = num7 / 100;
            if (techLevel == 0.0)
            {
                num8 = Math.Min(1, num8);
            }
            int troopStrength = race.TroopStrength;
            if (empire.TroopCanRecruitInfantry)
            {
                for (int i = 0; i < num8; i++)
                {
                    Troop troop = GenerateNewTroop(empire.GenerateTroopDescription(), TroopType.Infantry, troopStrength, empire, capital.Population.DominantRace);
                    troop.Colony = capital;
                    capital.Troops.Add(troop);
                    empire.Troops.Add(troop);
                }
            }
            else
            {
                empire.Troops.Clear();
                capital.Troops.Clear();
            }
            if (empire.ResourceMap != null)
            {
                for (int j = 0; j < galaxy.Habitats.Count; j++)
                {
                    if (galaxy.Habitats[j].Parent == null)
                    {
                        empire.ResourceMap.SetResourcesKnown(galaxy.Habitats[j], known: false);
                    }
                }
            }
            int val2 = (int)(expansion * 3.5);
            val2 = Math.Min(val2, (int)((double)galaxy.StarCount * 0.85));
            if (val2 > galaxy.StarCount)
            {
                val2 = galaxy.StarCount;
            }
            if (age == 0)
            {
                val2 = 0;
                for (int k = 0; k < galaxy.Systems[capital.SystemIndex].Habitats.Count; k++)
                {
                    empire.ResourceMap.SetResourcesKnown(galaxy.Systems[capital.SystemIndex].Habitats[k], known: false);
                }
            }
            SetEmpireExplorationAmount(empire, val2);
            empire.ResourceMap.SetResourcesKnown(capital, known: true);
            empire.InitiateConstruction = false;
            empire.DoTasks();
            empire.InitiateConstruction = true;
            Habitat systemStar = DetermineHabitatSystemStar(capital);
            if (homeSystemDescription == TextResolver.GetText("Harsh"))
            {
                SetColonizableHabitatsInSystem(galaxy, systemStar, race, 0);
                SetResourceLevelsInSystem(galaxy, systemStar, 0, 1);
            }
            else if (homeSystemDescription == TextResolver.GetText("Trying"))
            {
                SetColonizableHabitatsInSystem(galaxy, systemStar, race, 0);
                SetResourceLevelsInSystem(galaxy, systemStar, 0, 2);
            }
            else if (homeSystemDescription == TextResolver.GetText("Normal"))
            {
                SetColonizableHabitatsInSystem(galaxy, systemStar, race, 0);
                SetResourceLevelsInSystem(galaxy, systemStar, 1, 4);
            }
            else if (homeSystemDescription == TextResolver.GetText("Agreeable"))
            {
                SetColonizableHabitatsInSystem(galaxy, systemStar, race, 1);
                SetResourceLevelsInSystem(galaxy, systemStar, 1, 5);
            }
            else if (homeSystemDescription == TextResolver.GetText("Excellent"))
            {
                SetColonizableHabitatsInSystem(galaxy, systemStar, race, 2);
                SetResourceLevelsInSystem(galaxy, systemStar, 2, 5);
            }
            capital.Resources.Clear();
            galaxy.SelectResources(capital, minimumResourceCount, race, minimumCriticalResourceCount);
            return empire;
        }

        public void UpdateEmpireResearch()
        {
            if (Empires != null)
            {
                for (int i = 0; i < Empires.Count; i++)
                {
                    Empire empire = Empires[i];
                    if (empire != null && empire.Research != null)
                    {
                        empire.Research.Update(empire.DominantRace);
                    }
                }
            }
            if (PirateEmpires == null)
            {
                return;
            }
            for (int j = 0; j < PirateEmpires.Count; j++)
            {
                Empire empire2 = PirateEmpires[j];
                if (empire2 != null && empire2.Research != null)
                {
                    empire2.Research.Update(empire2.DominantRace);
                }
            }
        }

        public static string ResolveDescription(TroopType troopType)
        {
            string result = string.Empty;
            switch (troopType)
            {
                case TroopType.Infantry:
                    result = TextResolver.GetText("TroopType Infantry");
                    break;
                case TroopType.Armored:
                    result = TextResolver.GetText("TroopType Armored");
                    break;
                case TroopType.Artillery:
                    result = TextResolver.GetText("TroopType Artillery");
                    break;
                case TroopType.SpecialForces:
                    result = TextResolver.GetText("TroopType SpecialForces");
                    break;
                case TroopType.PirateRaider:
                    result = TextResolver.GetText("TroopType PirateRaider");
                    break;
            }
            return result;
        }

        public static string ResolveTroopStrengthDescription(Troop troop)
        {
            string result = string.Empty;
            if (troop != null && troop.Race != null)
            {
                double num = (double)troop.Race.TroopStrength * 1.0;
                double num2 = (double)troop.Race.TroopStrength * 1.5;
                double num3 = (double)troop.Race.TroopStrength * 2.5;
                _ = troop.Race.TroopStrength;
                double num4 = 0.0;
                double num5 = 1.0;
                switch (troop.Type)
                {
                    case TroopType.Infantry:
                        num4 = troop.DefendStrength;
                        break;
                    case TroopType.Artillery:
                        num4 = troop.DefendStrength;
                        num5 = 0.75;
                        break;
                    case TroopType.PirateRaider:
                        num4 = troop.AttackStrength;
                        break;
                    case TroopType.Armored:
                        num4 = troop.AttackStrength;
                        num5 = 3.0;
                        break;
                    case TroopType.SpecialForces:
                        num4 = troop.AttackStrength;
                        num5 = 2.0;
                        break;
                }
                result = ((num4 <= num * num5) ? TextResolver.GetText("Troop Strength Level Green") : ((num4 <= num2 * num5) ? TextResolver.GetText("Troop Strength Level Experienced") : ((!(num4 <= num3 * num5)) ? TextResolver.GetText("Troop Strength Level Elite") : TextResolver.GetText("Troop Strength Level Veteran"))));
            }
            return result;
        }

        public static string ResolveTroopCompositionDescription(int infantryCount, int artilleryCount, int armoredCount, int specialForcesCount)
        {
            string text = string.Empty;
            if (infantryCount > 0)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    text += ", ";
                }
                text = text + infantryCount + " " + TextResolver.GetText("TroopType Infantry Abbreviation");
            }
            if (artilleryCount > 0)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    text += ", ";
                }
                text = text + artilleryCount + " " + TextResolver.GetText("TroopType Artillery Abbreviation");
            }
            if (armoredCount > 0)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    text += ", ";
                }
                text = text + armoredCount + " " + TextResolver.GetText("TroopType Armored Abbreviation");
            }
            if (specialForcesCount > 0)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    text += ", ";
                }
                text = text + specialForcesCount + " " + TextResolver.GetText("TroopType SpecialForces Abbreviation");
            }
            return text;
        }

        public static double CalculateDefaultTroopMaintenanceMultiplier(TroopType troopType)
        {
            double result = 1.0;
            switch (troopType)
            {
                case TroopType.Infantry:
                    result = 1.0;
                    break;
                case TroopType.PirateRaider:
                    result = 0.0;
                    break;
                case TroopType.Armored:
                    result = 2.0;
                    break;
                case TroopType.Artillery:
                    result = 4.0;
                    break;
                case TroopType.SpecialForces:
                    result = 2.0;
                    break;
            }
            return result;
        }

        public static TroopList GenerateDefaultTroops(Galaxy galaxy)
        {
            TroopList troopList = new TroopList();
            if (galaxy != null)
            {
                for (int i = 0; i < galaxy.Races.Count; i++)
                {
                    Race race = galaxy.Races[i];
                    if (race != null)
                    {
                        double num = race.TroopStrength;
                        Troop item = GenerateNewTroop(race.TroopName, TroopType.Infantry, (int)num, null, race);
                        troopList.Add(item);
                        Troop item2 = GenerateNewTroop(race.TroopNameArmored, TroopType.Armored, (int)num, null, race);
                        troopList.Add(item2);
                        Troop item3 = GenerateNewTroop(race.TroopNameArtillery, TroopType.Artillery, (int)num, null, race);
                        troopList.Add(item3);
                        Troop item4 = GenerateNewTroop(race.TroopNameSpecialForces, TroopType.SpecialForces, (int)num, null, race);
                        troopList.Add(item4);
                        double num2 = (double)race.TroopStrength / 100.0;
                        int naturalStrength = (int)(60.0 * num2);
                        string name = string.Format(TextResolver.GetText("RACE Pirate Raider"), race.Name);
                        Troop item5 = GenerateNewTroop(name, TroopType.PirateRaider, naturalStrength, null, race);
                        troopList.Add(item5);
                    }
                }
            }
            return troopList;
        }

        public static Troop GenerateNewTroop(string name, TroopType troopType, int naturalStrength, Empire empire, Race race)
        {
            return GenerateNewTroop(name, troopType, naturalStrength, empire, race, applyBonusFactors: true);
        }

    }
}
