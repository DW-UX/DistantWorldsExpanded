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
        public static string ResolveDescription(Empire empire, BuiltObjectMission mission)
        {
            string empty = string.Empty;
            if (empire == null || mission == null || mission.Type == BuiltObjectMissionType.Undefined)
            {
                return "(" + TextResolver.GetText("No mission") + ")";
            }
            string text = string.Empty;
            string text2 = string.Empty;
            if (mission.TargetSector != null)
            {
                text = string.Format(TextResolver.GetText("Sector X"), ResolveSectorDescription(mission.TargetSector));
            }
            else if (mission.TargetShipGroup != null)
            {
                text = mission.TargetShipGroup.Name;
            }
            else if (mission.TargetBuiltObject != null)
            {
                text = mission.TargetBuiltObject.Name;
            }
            else if (mission.TargetHabitat != null)
            {
                Habitat targetHabitat = mission.TargetHabitat;
                if (targetHabitat != null)
                {
                    text2 = ResolveDescription(targetHabitat.Category);
                    SystemVisibilityStatus systemVisibilityStatus = empire.CheckSystemVisibilityStatus(targetHabitat.SystemIndex);
                    text = ((systemVisibilityStatus != SystemVisibilityStatus.Unexplored) ? targetHabitat.Name : string.Format(TextResolver.GetText("Unknown X"), text2));
                }
            }
            else if (mission.TargetCreature != null)
            {
                text = mission.TargetCreature.Name;
            }
            switch (mission.Type)
            {
                case BuiltObjectMissionType.Undeploy:
                    empty = TextResolver.GetText("Undeploy");
                    break;
                case BuiltObjectMissionType.Deploy:
                    empty = string.Format(TextResolver.GetText("Deploy at X"), text);
                    break;
                case BuiltObjectMissionType.Attack:
                    empty = string.Format(TextResolver.GetText("Attack X"), text);
                    break;
                case BuiltObjectMissionType.Capture:
                    empty = string.Format(TextResolver.GetText("Capture X"), text);
                    break;
                case BuiltObjectMissionType.Raid:
                    empty = string.Format(TextResolver.GetText("Raid X"), text);
                    break;
                case BuiltObjectMissionType.Bombard:
                    empty = string.Format(TextResolver.GetText("Bombard X"), text);
                    break;
                case BuiltObjectMissionType.Blockade:
                    empty = string.Format(TextResolver.GetText("Blockade X"), text);
                    break;
                case BuiltObjectMissionType.Build:
                case BuiltObjectMissionType.BuildRepair:
                    if (mission.Design != null)
                    {
                        string text8 = string.Empty;
                        if (mission.TargetHabitat == null && mission.TargetBuiltObject == null && mission.TargetShipGroup == null)
                        {
                            if (mission.X > -2E+09f && mission.Y > -2E+09f)
                            {
                                text8 = mission.X.ToString("0,K") + "," + mission.Y.ToString("0,K");
                            }
                        }
                        else
                        {
                            if (mission.TargetHabitat != null)
                            {
                                text8 = text2 + " ";
                            }
                            text8 += text;
                        }
                        empty = string.Format(TextResolver.GetText("Build BASETYPE at X"), ResolveDescription(mission.Design.SubRole), text8);
                    }
                    else
                    {
                        BuiltObject secondaryTargetBuiltObject = mission.SecondaryTargetBuiltObject;
                        empty = string.Format(TextResolver.GetText("Repair X"), ResolveDescription(secondaryTargetBuiltObject.SubRole));
                    }
                    break;
                case BuiltObjectMissionType.Colonize:
                    {
                        string text9 = string.Empty;
                        if (mission.TargetHabitat != null)
                        {
                            text9 = text2 + " ";
                        }
                        text9 += text;
                        empty = string.Format(TextResolver.GetText("Colonize X"), text9);
                        break;
                    }
                case BuiltObjectMissionType.Escape:
                    empty = string.Format(TextResolver.GetText("Escape from X"), text);
                    break;
                case BuiltObjectMissionType.Escort:
                    empty = string.Format(TextResolver.GetText("Escort X"), text);
                    break;
                case BuiltObjectMissionType.Explore:
                    {
                        string text7 = string.Empty;
                        if (mission.TargetHabitat != null)
                        {
                            text7 = text2 + " ";
                        }
                        text7 += text;
                        empty = string.Format(TextResolver.GetText("Explore X"), text7);
                        break;
                    }
                case BuiltObjectMissionType.ExtractResources:
                    {
                        string text6 = string.Empty;
                        if (mission.TargetHabitat != null)
                        {
                            text6 = text6 + text2 + " ";
                        }
                        text6 += text;
                        empty = string.Format(TextResolver.GetText("Mine X"), text6);
                        break;
                    }
                case BuiltObjectMissionType.Hold:
                    {
                        string text4 = string.Empty;
                        if (mission.TargetHabitat == null && mission.TargetBuiltObject == null && mission.TargetShipGroup == null)
                        {
                            if (mission.X > -2E+09f && mission.Y > -2E+09f)
                            {
                                text4 = text4 + mission.X.ToString("0,K") + "," + mission.Y.ToString("0,K");
                            }
                        }
                        else
                        {
                            if (mission.TargetHabitat != null)
                            {
                                text4 = text4 + text2 + " ";
                            }
                            text4 += text;
                        }
                        empty = string.Format(TextResolver.GetText("Hold at X"), text4);
                        break;
                    }
                case BuiltObjectMissionType.LoadTroops:
                    empty = string.Format(TextResolver.GetText("Load Troops at X"), text);
                    if (string.IsNullOrEmpty(text))
                    {
                        empty = TextResolver.GetText("Load Troops");
                    }
                    break;
                case BuiltObjectMissionType.Patrol:
                    {
                        string empty3 = string.Empty;
                        if (mission.TargetHabitat != null)
                        {
                            if (mission.TargetHabitat.Category == HabitatCategoryType.Star || mission.TargetHabitat.Category == HabitatCategoryType.GasCloud)
                            {
                                SystemVisibilityStatus systemVisibilityStatus2 = empire.CheckSystemVisibilityStatus(mission.TargetHabitat.SystemIndex);
                                empty3 = ((systemVisibilityStatus2 != SystemVisibilityStatus.Unexplored) ? (mission.TargetHabitat.Name + " " + TextResolver.GetText("system")) : TextResolver.GetText("Unknown system"));
                            }
                            else
                            {
                                empty3 = text;
                            }
                        }
                        else
                        {
                            empty3 = text;
                        }
                        empty = string.Format(TextResolver.GetText("Patrol X"), empty3);
                        break;
                    }
                case BuiltObjectMissionType.Refuel:
                    empty = string.Format(TextResolver.GetText("Refuel at X"), text);
                    break;
                case BuiltObjectMissionType.Rescue:
                    empty = string.Format(TextResolver.GetText("Rescue X"), text);
                    break;
                case BuiltObjectMissionType.Repair:
                    empty = string.Format(TextResolver.GetText("Repair at X"), text);
                    break;
                case BuiltObjectMissionType.Retire:
                    empty = string.Format(TextResolver.GetText("Retire at X"), text);
                    break;
                case BuiltObjectMissionType.Retrofit:
                    empty = string.Format(TextResolver.GetText("Retrofit at X"), text);
                    break;
                case BuiltObjectMissionType.Transport:
                    if (mission.Cargo != null && mission.Cargo.Count > 0)
                    {
                        string text5 = string.Empty;
                        for (int i = 0; i < mission.Cargo.Count; i++)
                        {
                            Cargo cargo = mission.Cargo[i];
                            text5 = ((cargo.CommodityResource != null) ? (text5 + cargo.CommodityResource.Name) : ((cargo.CommodityComponent == null) ? (text5 + TextResolver.GetText("unknown cargo")) : (text5 + cargo.CommodityComponent.Name)));
                            if (i < mission.Cargo.Count - 1)
                            {
                                text5 += ", ";
                            }
                        }
                        empty = string.Format(TextResolver.GetText("Transport X"), text5);
                    }
                    else if (mission.Population != null && mission.Population.Count > 0)
                    {
                        string empty2 = string.Empty;
                        empty = string.Format(arg0: (mission.Population.TotalAmount < 1000000) ? TextResolver.GetText("Tourists") : ((mission.SecondaryTargetHabitat == null) ? TextResolver.GetText("Passengers") : TextResolver.GetText("Migrants")), format: TextResolver.GetText("Transport X"));
                    }
                    else
                    {
                        empty = string.Format(TextResolver.GetText("Transport X"), TextResolver.GetText("nothing"));
                    }
                    break;
                case BuiltObjectMissionType.UnloadTroops:
                    empty = string.Format(TextResolver.GetText("Unload Troops at X"), text);
                    break;
                case BuiltObjectMissionType.Waypoint:
                    {
                        string arg = string.Empty;
                        if (mission.TargetHabitat == null && mission.TargetBuiltObject == null && mission.TargetShipGroup == null)
                        {
                            if (mission.X > -2E+09f && mission.Y > -2E+09f)
                            {
                                arg = mission.X.ToString("0,K") + "," + mission.Y.ToString("0,K");
                            }
                        }
                        else
                        {
                            arg = text;
                        }
                        empty = string.Format(TextResolver.GetText("Waypoint at X"), arg);
                        break;
                    }
                case BuiltObjectMissionType.MoveAndWait:
                    {
                        string arg5 = string.Empty;
                        if (mission.TargetBuiltObject == null && mission.TargetHabitat == null && mission.TargetShipGroup == null)
                        {
                            if (mission.X > -2E+09f && mission.Y > -2E+09f)
                            {
                                arg5 = mission.X.ToString("0,K") + "," + mission.Y.ToString("0,K");
                            }
                        }
                        else
                        {
                            arg5 = text;
                        }
                        empty = string.Format(TextResolver.GetText("Wait at X"), arg5);
                        break;
                    }
                case BuiltObjectMissionType.WaitAndAttack:
                    {
                        string arg4 = string.Empty;
                        if (mission.TargetHabitat == null && mission.TargetBuiltObject == null && mission.TargetShipGroup == null)
                        {
                            if (mission.X > -2E+09f && mission.Y > -2E+09f)
                            {
                                arg4 = mission.X.ToString("0,K") + "," + mission.Y.ToString("0,K");
                            }
                        }
                        else
                        {
                            arg4 = text;
                        }
                        empty = string.Format(TextResolver.GetText("Assemble and attack X"), arg4);
                        break;
                    }
                case BuiltObjectMissionType.WaitAndBombard:
                    {
                        string arg2 = string.Empty;
                        if (mission.TargetHabitat == null && mission.TargetBuiltObject == null && mission.TargetShipGroup == null)
                        {
                            if (mission.X > -2E+09f && mission.Y > -2E+09f)
                            {
                                arg2 = mission.X.ToString("0,K") + "," + mission.Y.ToString("0,K");
                            }
                        }
                        else
                        {
                            arg2 = text;
                        }
                        empty = string.Format(TextResolver.GetText("Assemble and bombard X"), arg2);
                        break;
                    }
                case BuiltObjectMissionType.Move:
                    {
                        string text3 = string.Empty;
                        if (mission.TargetHabitat == null && mission.TargetBuiltObject == null && mission.TargetShipGroup == null)
                        {
                            if (mission.X > -2E+09f && mission.Y > -2E+09f)
                            {
                                text3 = mission.X.ToString("0,K") + "," + mission.Y.ToString("0,K");
                            }
                        }
                        else
                        {
                            if (mission.TargetHabitat != null)
                            {
                                text3 = text2 + " ";
                            }
                            text3 += text;
                        }
                        empty = string.Format(TextResolver.GetText("Move to X"), text3);
                        break;
                    }
                default:
                    empty = "(" + TextResolver.GetText("No mission") + ")";
                    break;
            }
            return empty;
        }

        public ComponentList ResolveComponentsThatUseResource(Resource resource)
        {
            ComponentList componentList = new ComponentList();
            ComponentDefinition[] componentDefinitionsStatic = ComponentDefinitionsStatic;
            foreach (ComponentDefinition componentDefinition in componentDefinitionsStatic)
            {
                if (componentDefinition.ComponentID == 106)
                {
                    continue;
                }
                foreach (ComponentResource requiredResource in componentDefinition.RequiredResources)
                {
                    if (requiredResource.ResourceID == resource.ResourceID)
                    {
                        componentList.Add(new Component(componentDefinition.ComponentID));
                    }
                }
            }
            return componentList;
        }

        public ResourceList ShowAvailableRestrictedResourcesForEmpireSelfSupplied(Empire empire)
        {
            ResourceList resourceList = new ResourceList();
            ResourceList supplied = empire.DetermineResourcesEmpireSupplies();
            for (int i = 0; i < ResourceSystem.SuperLuxuryResources.Count; i++)
            {
                Resource resource = new Resource(ResourceSystem.SuperLuxuryResources[i].ResourceID);
                resourceList = UpdateResourcesFromSuppliedResources(resourceList, resource, supplied);
            }
            return resourceList;
        }

        public ResourceList ShowAvailableRestrictedResourcesForEmpire(Empire empire)
        {
            ResourceList resourceList = new ResourceList();
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire2 = Empires[i];
                if (empire2 == empire)
                {
                    ResourceList supplied = empire.DetermineResourcesEmpireSupplies();
                    for (int j = 0; j < ResourceSystem.SuperLuxuryResources.Count; j++)
                    {
                        Resource resource = new Resource(ResourceSystem.SuperLuxuryResources[j].ResourceID);
                        resourceList = UpdateResourcesFromSuppliedResources(resourceList, resource, supplied);
                    }
                }
                else
                {
                    DiplomaticRelation diplomaticRelation = empire2.ObtainDiplomaticRelation(empire);
                    if (diplomaticRelation.Type != 0 && diplomaticRelation.SupplyRestrictedResources)
                    {
                        ResourceList supplied2 = empire2.DetermineResourcesEmpireSupplies();
                        for (int k = 0; k < ResourceSystem.SuperLuxuryResources.Count; k++)
                        {
                            Resource resource2 = new Resource(ResourceSystem.SuperLuxuryResources[k].ResourceID);
                            resourceList = UpdateResourcesFromSuppliedResources(resourceList, resource2, supplied2);
                        }
                    }
                }
                if (resourceList.Count >= 3)
                {
                    break;
                }
            }
            return resourceList;
        }

        private ResourceList UpdateResourcesFromSuppliedResources(ResourceList resources, Resource resource, ResourceList supplied)
        {
            if (supplied.Contains(resource) && !resources.Contains(resource))
            {
                resources.Add(resource);
            }
            return resources;
        }

        public ResourceList ShowCheapestLuxuryResources()
        {
            ResourceList resourceList = new ResourceList();
            for (int i = 0; i < ResourceSystem.Resources.Count; i++)
            {
                ResourceDefinition resourceDefinition = ResourceSystem.Resources[i];
                if (resourceDefinition.Group == ResourceGroup.Luxury && resourceDefinition.SuperLuxuryBonusAmount <= 0)
                {
                    Resource resource = new Resource(resourceDefinition.ResourceID);
                    resource.SortTag = ResourceCurrentPrices[i];
                    resourceList.Add(resource);
                }
            }
            resourceList.Sort();
            return resourceList;
        }

        public Resource SelectRandomLuxuryResource()
        {
            Resource resource = new Resource(0);
            bool flag = false;
            int iterationCount = 0;
            while (ConditionCheckLimit(!flag, 50, ref iterationCount))
            {
                int index = Rnd.Next(0, ResourceSystem.LuxuryResources.Count);
                resource = new Resource(ResourceSystem.LuxuryResources[index].ResourceID);
                flag = true;
                if (resource.IsRestrictedResource || resource.ColonyManufacturingLevel > 0)
                {
                    flag = false;
                }
            }
            return resource;
        }

        public void CalculateEmpireWarValue(Empire empire, out int builtObjectWarValue, out int colonyWarValue)
        {
            builtObjectWarValue = 0;
            colonyWarValue = 0;
            for (int i = 0; i < empire.BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = empire.BuiltObjects[i];
                builtObjectWarValue += CalculateWarValue(builtObject);
            }
            for (int j = 0; j < empire.PrivateBuiltObjects.Count; j++)
            {
                BuiltObject builtObject2 = empire.PrivateBuiltObjects[j];
                builtObjectWarValue += CalculateWarValue(builtObject2);
            }
            for (int k = 0; k < empire.Colonies.Count; k++)
            {
                Habitat habitat = empire.Colonies[k];
                colonyWarValue += CalculateWarValue(habitat);
            }
        }

        public int CalculateWarValue(Fighter fighter)
        {
            return 1;
        }

        public int CalculateWarValue(BuiltObject builtObject)
        {
            int num = 0;
            switch (builtObject.Role)
            {
                case BuiltObjectRole.Military:
                    num = builtObject.Design.FirepowerRaw;
                    break;
                case BuiltObjectRole.Base:
                    switch (builtObject.SubRole)
                    {
                        case BuiltObjectSubRole.SmallSpacePort:
                        case BuiltObjectSubRole.MediumSpacePort:
                        case BuiltObjectSubRole.LargeSpacePort:
                            num = ((builtObject.ParentHabitat == null) ? (builtObject.Design.Size / 5) : (builtObject.ParentHabitat.StrategicValue / 200));
                            break;
                        default:
                            num = builtObject.Design.Size / 5;
                            break;
                    }
                    break;
                default:
                    num = builtObject.Design.Size / 20;
                    break;
            }
            if (builtObject.UnbuiltComponentCount > 0)
            {
                double num2 = (double)builtObject.UnbuiltComponentCount / (double)builtObject.Components.Count;
                num = Math.Max(1, (int)((double)num / 2.0 - (double)num * num2));
            }
            return num;
        }

        public int CalculateWarValue(Habitat habitat)
        {
            int result = 0;
            if (habitat.Empire != null && habitat.Empire != IndependentEmpire)
            {
                result = habitat.StrategicValue / 50;
            }
            return result;
        }

        public void InflictWarDamage(Empire inflictingEmpire, Fighter target)
        {
            if (target.Empire != null)
            {
                DiplomaticRelation diplomaticRelation = target.Empire.DiplomaticRelations[inflictingEmpire];
                if (diplomaticRelation != null)
                {
                    diplomaticRelation.WarDamageBuiltObject += CalculateWarValue(target);
                }
            }
        }

        public void InflictWarDamage(Empire inflictingEmpire, BuiltObject target)
        {
            if (target.Empire != null)
            {
                DiplomaticRelation diplomaticRelation = target.Empire.DiplomaticRelations[inflictingEmpire];
                if (diplomaticRelation != null)
                {
                    diplomaticRelation.WarDamageBuiltObject += CalculateWarValue(target);
                }
            }
        }

        public void InflictWarDamage(Empire inflictingEmpire, Habitat target)
        {
            if (target.Empire != null && target.Empire != IndependentEmpire)
            {
                DiplomaticRelation diplomaticRelation = target.Empire.DiplomaticRelations[inflictingEmpire];
                if (diplomaticRelation != null)
                {
                    diplomaticRelation.WarDamageColony += CalculateWarValue(target);
                }
            }
        }

        private string GenerateResortBaseName(Habitat habitat)
        {
            string empty = string.Empty;
            string[] array = new string[5] { "Royal", "Holiday", "Luxury", "Grand", "Horizon" };
            string[] array2 = new string[10] { "Resort", "Hotel", "Encounter", "Casino", "Retreat", "Stopover", "Lounge", "Lodge", "Club", "Palace" };
            if (habitat != null && Rnd.Next(0, 3) > 0)
            {
                if (!string.IsNullOrEmpty(habitat.ScenicFeature))
                {
                    if (habitat.ScenicFeature.Length < 26)
                    {
                        return habitat.ScenicFeature + " " + array2[Rnd.Next(0, array2.Length)];
                    }
                    return array[Rnd.Next(0, array.Length)] + " " + array2[Rnd.Next(0, array2.Length)];
                }
                Habitat habitat2 = DetermineHabitatSystemStar(habitat);
                return habitat2.Name + " " + array2[Rnd.Next(0, array2.Length)];
            }
            return array[Rnd.Next(0, array.Length)] + " " + array2[Rnd.Next(0, array2.Length)];
        }

        public BuiltObjectList FastFindBasesInSystem(Empire empire, Habitat systemStar)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            if (systemStar != null && empire != null)
            {
                if (empire.BuiltObjects != null)
                {
                    for (int i = 0; i < empire.BuiltObjects.Count; i++)
                    {
                        BuiltObject builtObject = empire.BuiltObjects[i];
                        if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Role == BuiltObjectRole.Base && builtObject.NearestSystemStar == systemStar)
                        {
                            builtObjectList.Add(builtObject);
                        }
                    }
                }
                if (empire.PrivateBuiltObjects != null)
                {
                    for (int j = 0; j < empire.PrivateBuiltObjects.Count; j++)
                    {
                        BuiltObject builtObject2 = empire.PrivateBuiltObjects[j];
                        if (builtObject2 != null && !builtObject2.HasBeenDestroyed && builtObject2.Role == BuiltObjectRole.Base && builtObject2.NearestSystemStar == systemStar)
                        {
                            builtObjectList.Add(builtObject2);
                        }
                    }
                }
            }
            return builtObjectList;
        }

        public bool FastTestShipInOwnSystem(BuiltObject builtObject)
        {
            if (builtObject.NearestSystemStar != null && Systems[builtObject.NearestSystemStar.SystemIndex].DominantEmpire != null && Systems[builtObject.NearestSystemStar.SystemIndex].DominantEmpire.Empire == builtObject.Empire)
            {
                return true;
            }
            return false;
        }

        public bool FastTestShipInColonizedSystem(BuiltObject builtObject)
        {
            if (builtObject.NearestSystemStar != null && Systems[builtObject.NearestSystemStar.SystemIndex].DominantEmpire != null && Systems[builtObject.NearestSystemStar.SystemIndex].DominantEmpire.Empire != null)
            {
                return true;
            }
            return false;
        }

        public BuiltObject FastFindNearestResearchFacility(int x, int y, Empire empire)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            for (int i = 0; i < empire.ResearchFacilities.Count; i++)
            {
                BuiltObject builtObject = empire.ResearchFacilities[i];
                if (builtObject != null)
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

        public BuiltObject FastFindNearestMonitoringStation(int x, int y, Empire empire)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            for (int i = 0; i < empire.LongRangeScanners.Count; i++)
            {
                BuiltObject builtObject = empire.LongRangeScanners[i];
                if (builtObject != null && builtObject.Role == BuiltObjectRole.Base && (builtObject.ParentHabitat == null || builtObject.ParentHabitat.Empire == null || builtObject.ParentHabitat.Empire == IndependentEmpire))
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

        public BuiltObject FastFindNearestLongRangeScannerBase(int x, int y, Empire empire)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            for (int i = 0; i < empire.LongRangeScanners.Count; i++)
            {
                BuiltObject builtObject = empire.LongRangeScanners[i];
                if (builtObject != null && builtObject.Role == BuiltObjectRole.Base)
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

        public BuiltObject FastFindNearestLongRangeScanner(int x, int y, Empire empire)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            for (int i = 0; i < empire.LongRangeScanners.Count; i++)
            {
                BuiltObject builtObject = empire.LongRangeScanners[i];
                if (builtObject != null)
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

        public BuiltObject FastFindNearestSpacePort(double x, double y, Empire empire)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            for (int i = 0; i < empire.SpacePorts.Count; i++)
            {
                BuiltObject builtObject = empire.SpacePorts[i];
                if (builtObject != null)
                {
                    double num2 = CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                    if (num2 < num && builtObject.IsSpacePort)
                    {
                        result = builtObject;
                        num = num2;
                    }
                }
            }
            return result;
        }

        public void DetermineResupplyShipLocationByDestination(BuiltObject resupplyShip, out double x, out double y)
        {
            x = resupplyShip.Xpos;
            y = resupplyShip.Ypos;
            if (resupplyShip.IsFunctional && resupplyShip.Mission != null && resupplyShip.Mission.Type == BuiltObjectMissionType.Deploy)
            {
                Point point = resupplyShip.Mission.ResolveTargetCoordinates(resupplyShip.Mission);
                x = point.X;
                y = point.Y;
            }
            else if (resupplyShip.IsDeployed && resupplyShip.IsFunctional)
            {
                x = resupplyShip.Xpos;
                y = resupplyShip.Ypos;
            }
        }

        public BuiltObject FastFindNearestResupplyShipByDestination(double x, double y, Empire empire, BuiltObject resupplyShipToExclude)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            double x2 = 0.0;
            double y2 = 0.0;
            for (int i = 0; i < empire.ResupplyShips.Count; i++)
            {
                BuiltObject builtObject = empire.ResupplyShips[i];
                if (builtObject != null && builtObject != resupplyShipToExclude && (builtObject.IsDeployed || (builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Deploy)))
                {
                    DetermineResupplyShipLocationByDestination(builtObject, out x2, out y2);
                    double num2 = CalculateDistanceSquared(x, y, x2, y2);
                    if (num2 < num)
                    {
                        result = builtObject;
                        num = num2;
                    }
                }
            }
            return result;
        }

        public BuiltObject FastFindNearestResupplyShip(double x, double y, Empire empire)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            for (int i = 0; i < empire.ResupplyShips.Count; i++)
            {
                BuiltObject builtObject = empire.ResupplyShips[i];
                if (builtObject != null && builtObject.IsDeployed && builtObject.IsFunctional)
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

        public BuiltObject FastFindNearestOtherSpacePort(int x, int y, Empire empire, BuiltObjectList spacePortsToExclude)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            for (int i = 0; i < empire.SpacePorts.Count; i++)
            {
                BuiltObject builtObject = empire.SpacePorts[i];
                if (builtObject != null && !spacePortsToExclude.Contains(builtObject))
                {
                    double num2 = CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                    if (num2 < num && builtObject.IsSpacePort)
                    {
                        result = builtObject;
                        num = num2;
                    }
                }
            }
            return result;
        }

        public BuiltObject FastFindNearestOtherSpacePort(int x, int y, Empire empire, BuiltObject spacePortToExclude)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            for (int i = 0; i < empire.SpacePorts.Count; i++)
            {
                BuiltObject builtObject = empire.SpacePorts[i];
                if (builtObject != null && builtObject != spacePortToExclude)
                {
                    double num2 = CalculateDistanceSquared(x, y, builtObject.Xpos, builtObject.Ypos);
                    if (num2 < num && builtObject.IsSpacePort)
                    {
                        result = builtObject;
                        num = num2;
                    }
                }
            }
            return result;
        }

        public BuiltObject FastFindNearestAvailableMilitaryShip(double x, double y, Empire empire)
        {
            return FastFindNearestAvailableMilitaryShip(x, y, empire, 1, includeUnAutomatedShips: true);
        }

        public BuiltObject FastFindNearestAvailableMilitaryShip(double x, double y, Empire empire, int minimumFirepower, bool includeUnAutomatedShips)
        {
            return FastFindNearestAvailableMilitaryShip(x, y, empire, minimumFirepower, includeUnAutomatedShips, allowShipsInFleets: true);
        }

        public BuiltObject FastFindNearestAvailableMilitaryShip(double x, double y, Empire empire, int minimumFirepower, bool includeUnAutomatedShips, bool allowShipsInFleets)
        {
            return FastFindNearestAvailableMilitaryShip(x, y, empire, minimumFirepower, includeUnAutomatedShips, allowShipsInFleets, includeBusyShips: true);
        }

        public BuiltObject FastFindNearestAvailableMilitaryShip(double x, double y, Empire empire, int minimumFirepower, bool includeUnAutomatedShips, bool allowShipsInFleets, bool includeBusyShips)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            for (int i = 0; i < empire.BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = empire.BuiltObjects[i];
                if (builtObject == null || builtObject.Role != BuiltObjectRole.Military || builtObject.FirepowerRaw < minimumFirepower || builtObject.UnbuiltComponentCount > 0 || builtObject.BuiltAt != null || builtObject.SubRole == BuiltObjectSubRole.TroopTransport || builtObject.SubRole == BuiltObjectSubRole.ResupplyShip || !builtObject.IsFunctional || (!allowShipsInFleets && builtObject.ShipGroup != null) || (!builtObject.IsAutoControlled && !includeUnAutomatedShips))
                {
                    continue;
                }
                bool flag = false;
                if (builtObject.Mission != null && (builtObject.Mission.Type == BuiltObjectMissionType.Attack || builtObject.Mission.Priority == BuiltObjectMissionPriority.VeryHigh || builtObject.Mission.Priority == BuiltObjectMissionPriority.High))
                {
                    flag = true;
                }
                if (!includeBusyShips && builtObject.Mission != null && builtObject.Mission.Type != 0)
                {
                    if (builtObject.Mission.Type == BuiltObjectMissionType.Refuel || builtObject.Mission.Type == BuiltObjectMissionType.Repair || builtObject.Mission.Type == BuiltObjectMissionType.Retrofit)
                    {
                        flag = true;
                    }
                    if (builtObject.Mission.Priority == BuiltObjectMissionPriority.High || builtObject.Mission.Priority == BuiltObjectMissionPriority.VeryHigh || builtObject.Mission.Priority == BuiltObjectMissionPriority.Unavailable)
                    {
                        flag = true;
                    }
                }
                if (!flag)
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

        public BuiltObjectList ObtainAvailableMilitaryShips(Empire empire, int minimumFirepower, bool includeUnAutomatedShips, bool allowShipsInFleets, bool includeBusyShips)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < empire.BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = empire.BuiltObjects[i];
                if (builtObject.Role != BuiltObjectRole.Military || builtObject.FirepowerRaw < minimumFirepower || builtObject.UnbuiltComponentCount > 0 || builtObject.BuiltAt != null || builtObject.SubRole == BuiltObjectSubRole.TroopTransport || builtObject.SubRole == BuiltObjectSubRole.ResupplyShip || !builtObject.IsFunctional || (!allowShipsInFleets && builtObject.ShipGroup != null) || (!builtObject.IsAutoControlled && !includeUnAutomatedShips))
                {
                    continue;
                }
                bool flag = false;
                if (builtObject.Mission != null && (builtObject.Mission.Type == BuiltObjectMissionType.Attack || builtObject.Mission.Priority == BuiltObjectMissionPriority.VeryHigh || builtObject.Mission.Priority == BuiltObjectMissionPriority.High))
                {
                    flag = true;
                }
                if (!includeBusyShips && builtObject.Mission != null && builtObject.Mission.Type != 0)
                {
                    if (builtObject.Mission.Type == BuiltObjectMissionType.Refuel || builtObject.Mission.Type == BuiltObjectMissionType.Repair || builtObject.Mission.Type == BuiltObjectMissionType.Retrofit)
                    {
                        flag = true;
                    }
                    if (builtObject.Mission.Priority == BuiltObjectMissionPriority.High || builtObject.Mission.Priority == BuiltObjectMissionPriority.VeryHigh || builtObject.Mission.Priority == BuiltObjectMissionPriority.Unavailable)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    builtObjectList.Add(builtObject);
                }
            }
            return builtObjectList;
        }

        public BuiltObject FastFindNearestMiningStation(int x, int y, Empire empire)
        {
            double num = double.MaxValue;
            BuiltObject result = null;
            for (int i = 0; i < empire.MiningStations.Count; i++)
            {
                BuiltObject builtObject = empire.MiningStations[i];
                if (builtObject != null)
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

        public bool DetermineEmpireColonyNearPoint(double x, double y, Empire empire, double maximumDistance)
        {
            Habitat habitat = FastFindNearestColony((int)x, (int)y, empire, 0);
            if (habitat != null)
            {
                double num = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
                if (num < maximumDistance)
                {
                    return true;
                }
            }
            return false;
        }

        public void FindLonelyNebulaLocation(out double x, out double y, GalaxyLocationEffectType effect)
        {
            FindLonelyNebulaLocation(out x, out y, effect, GalaxyLocationEffectType.LightningDamage);
        }

        public void FindLonelyNebulaLocation(out double x, out double y, GalaxyLocationEffectType effect, GalaxyLocationEffectType effectToExclude)
        {
            x = 100000.0 + Rnd.NextDouble() * (double)(SizeX - 200000);
            y = 100000.0 + Rnd.NextDouble() * (double)(SizeY - 200000);
            int num = 0;
            bool flag = false;
            while (!flag && num < 100)
            {
                int index = Rnd.Next(0, GalaxyLocations.Count);
                GalaxyLocation galaxyLocation = GalaxyLocations[index];
                if (effectToExclude != 0 && galaxyLocation.Effect == effectToExclude)
                {
                    flag = false;
                    continue;
                }
                if (effect != 0)
                {
                    if (galaxyLocation.Effect == effect && CheckNebulaLocation(galaxyLocation, out x, out y))
                    {
                        flag = true;
                    }
                }
                else if (CheckNebulaLocation(galaxyLocation, out x, out y))
                {
                    flag = true;
                }
                num++;
            }
        }

        private bool CheckNebulaLocation(GalaxyLocation location, out double x, out double y)
        {
            x = 100000.0 + Rnd.NextDouble() * (double)(SizeX - 200000);
            y = 100000.0 + Rnd.NextDouble() * (double)(SizeY - 200000);
            double num = (double)location.Xpos + (double)location.Width / 2.0;
            double num2 = (double)location.Ypos + (double)location.Height / 2.0;
            if (!CheckNearEmpireColony(num, num2, 250000.0) && !CheckNearBuiltObject(num, num2, 150000.0) && !CheckNearSystem(num, num2, 60000.0) && !CheckNearSpecialGalaxyLocation(num, num2, 1000000.0))
            {
                SelectRelativeParkingPoint(100000.0, out x, out y);
                x += num;
                y += num2;
                return true;
            }
            return false;
        }

        private bool CheckNearSpecialGalaxyLocation(double x, double y, double minimumRange)
        {
            for (int i = 0; i < GalaxyLocations.Count; i++)
            {
                if (GalaxyLocations[i].Type == GalaxyLocationType.DebrisField || GalaxyLocations[i].Type == GalaxyLocationType.PlanetDestroyer || GalaxyLocations[i].Type == GalaxyLocationType.RestrictedArea)
                {
                    double x2 = (double)GalaxyLocations[i].Xpos + (double)(GalaxyLocations[i].Width / 2f);
                    double y2 = (double)GalaxyLocations[i].Ypos + (double)(GalaxyLocations[i].Height / 2f);
                    double num = CalculateDistance(x, y, x2, y2);
                    if (num < minimumRange)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckNearSystem(double x, double y, double minimumRange)
        {
            Habitat habitat = FastFindNearestSystemWithPlanets(x, y);
            if (habitat != null)
            {
                double num = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
                if (num < minimumRange)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckNearBuiltObject(double x, double y, double minimumRange)
        {
            BuiltObject builtObject = FindNearestBuiltObject((int)x, (int)y, null);
            if (builtObject != null)
            {
                double num = CalculateDistance(x, y, builtObject.Xpos, builtObject.Ypos);
                if (num < minimumRange)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckInStorm(double x, double y)
        {
            GalaxyLocationList galaxyLocationList = DetermineGalaxyLocationsAtPoint(x, y, GalaxyLocationType.NebulaCloud);
            for (int i = 0; i < galaxyLocationList.Count; i++)
            {
                GalaxyLocation galaxyLocation = galaxyLocationList[i];
                if (galaxyLocation != null && galaxyLocation.Effect == GalaxyLocationEffectType.LightningDamage)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckNearEmpireColony(double x, double y, double minimumRange)
        {
            Habitat habitat = FindNearestColony(x, y, null, 0);
            if (habitat != null)
            {
                double num = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
                if (num < minimumRange)
                {
                    return true;
                }
            }
            return false;
        }

        public void GenerateSpecialZoneResearchFacility()
        {
            FindLonelyNebulaLocation(out var x, out var y, GalaxyLocationEffectType.None);
            int num = Rnd.Next(0, 3);
            ComponentType labComponentType = ComponentType.LabsWeaponsLab;
            string namePart = string.Empty;
            switch (num)
            {
                case 0:
                    labComponentType = ComponentType.LabsWeaponsLab;
                    namePart = TextResolver.GetText("Weapons");
                    break;
                case 1:
                    labComponentType = ComponentType.LabsHighTechLab;
                    namePart = TextResolver.GetText("HighTech");
                    break;
                case 2:
                    labComponentType = ComponentType.LabsEnergyLab;
                    namePart = TextResolver.GetText("Energy");
                    break;
            }
            Design design = PlayerEmpire.GenerateResearchStationDesign(CurrentStarDate, labComponentType);
            int family = 0;
            switch (Rnd.Next(0, 2))
            {
                case 0:
                    family = ShipImageHelper.AncientHelpersFamily;
                    break;
                case 1:
                    family = ShipImageHelper.ShakturiAlliesFamily;
                    break;
            }
            design.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(family, design.SubRole, aged: false);
            string name = GenerateResearchStationName(x, y, namePart);
            BuiltObject builtObject = GenerateStoryAbandonedBuiltObject(x, y, design, name);
            builtObject.EncounterTechAdvanceCount = 1;
            string name2 = GenerateRestrictedZoneName(x, y, new string[4]
            {
            TextResolver.GetText("Test Site"),
            TextResolver.GetText("Special Projects Area"),
            TextResolver.GetText("Research Zone"),
            TextResolver.GetText("Experimental Area")
            });
            string text = TextResolver.GetText("You have entered a restricted area");
            int soundScheme = 2;
            if (Rnd.Next(0, 2) == 1)
            {
                soundScheme = 0;
            }
            GalaxyLocation galaxyLocation = GenerateRestrictedZone(name2, text, 2000.0, x, y, soundScheme);
            galaxyLocation.RelatedBuiltObject = builtObject;
        }

        private string GenerateResearchStationName(double x, double y, string namePart)
        {
            string empty = string.Empty;
            Habitat habitat = FastFindNearestSystemWithPlanets(x, y);
            empty = habitat.Name;
            if (!string.IsNullOrEmpty(namePart))
            {
                empty = empty + " " + namePart;
            }
            string[] array = new string[7]
            {
            TextResolver.GetText("Research Station"),
            TextResolver.GetText("Research Facility"),
            TextResolver.GetText("Research Outpost"),
            TextResolver.GetText("Station"),
            TextResolver.GetText("Facility"),
            TextResolver.GetText("Projects Facility"),
            TextResolver.GetText("Research Installation")
            };
            int num = Rnd.Next(0, array.Length);
            return empty + " " + array[num];
        }

        public void GenerateSpecialZoneSupplyDepot()
        {
            FindLonelyNebulaLocation(out var x, out var y, GalaxyLocationEffectType.None);
            int family = 0;
            switch (Rnd.Next(0, 2))
            {
                case 0:
                    family = ShipImageHelper.AncientHelpersFamily;
                    break;
                case 1:
                    family = ShipImageHelper.FreedomAllianceFamily;
                    break;
            }
            DesignSpecification designSpecification = DesignSpecifications.GetBySubRole(BuiltObjectSubRole.MediumSpacePort).Clone();
            bool flag = false;
            foreach (DesignSpecificationComponentRule componentRule in designSpecification.ComponentRules)
            {
                if (componentRule.ComponentCategory == ComponentCategoryType.Reactor)
                {
                    componentRule.Amount = 3;
                }
                if (componentRule.ComponentType == ComponentType.StorageCargo)
                {
                    componentRule.Amount = 75;
                    flag = true;
                }
            }
            if (!flag)
            {
                designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 75));
            }
            Design design = PlayerEmpire.GenerateDesignFromSpec(designSpecification, 5.0);
            design.Empire = null;
            DesignSpecification bySubRole = DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Frigate);
            Design design2 = PlayerEmpire.GenerateDesignFromSpec(bySubRole, 4.0);
            DesignSpecification bySubRole2 = DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Destroyer);
            Design design3 = PlayerEmpire.GenerateDesignFromSpec(bySubRole2, 4.0);
            DesignSpecification bySubRole3 = DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Cruiser);
            Design design4 = PlayerEmpire.GenerateDesignFromSpec(bySubRole3, 4.0);
            design.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(family, design.SubRole, aged: false);
            design2.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(family, design2.SubRole, aged: false);
            design3.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(family, design3.SubRole, aged: false);
            design4.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(family, design4.SubRole, aged: false);
            double num = 0.0;
            double num2 = 0.0;
            string name = GenerateRestrictedZoneName(x, y, new string[2]
            {
            TextResolver.GetText("Supply Depot"),
            TextResolver.GetText("Supply Outpost")
            });
            BuiltObject builtObject = GenerateStoryAbandonedBuiltObject(x + num, y + num2, design, name);
            for (int i = 0; i < ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; i++)
            {
                ResourceDefinition resourceDefinition = ResourceSystem.StrategicResourcesOrderedByRelativeImportance[i];
                if (resourceDefinition != null)
                {
                    int amount = (int)(10000f * resourceDefinition.RelativeImportance);
                    builtObject.Cargo.Add(new Cargo(new Resource(resourceDefinition.ResourceID), amount, IndependentEmpire));
                }
            }
            builtObject.EncounterMoneyBonus = 20000;
            int num3 = Rnd.Next(3, 6);
            for (int j = 0; j < num3; j++)
            {
                int num4 = Rnd.Next(0, 3);
                Design design5 = null;
                switch (num4)
                {
                    case 0:
                        design5 = design2;
                        name = SelectRandomUniqueMilitaryShipName(null);
                        break;
                    case 1:
                        design5 = design3;
                        name = SelectRandomUniqueMilitaryShipName(null);
                        break;
                    case 2:
                        design5 = design4;
                        name = SelectRandomUniqueMilitaryShipName(null);
                        break;
                }
                num = Rnd.Next(-600, 600);
                num2 = Rnd.Next(-600, 600);
                GenerateStoryAbandonedBuiltObject(x + num, y + num2, design5, name);
            }
            string text = GenerateRestrictedZoneName(x, y, new string[3]
            {
            TextResolver.GetText("Supply Outpost"),
            TextResolver.GetText("Forward Supply Zone"),
            TextResolver.GetText("Strategic Reserve")
            });
            string message = string.Format(TextResolver.GetText("Welcome to the Supply Depot"), text);
            GalaxyLocation galaxyLocation = GenerateRestrictedZone(text, message, 2000.0, x, y, 3);
            galaxyLocation.RelatedBuiltObject = builtObject;
        }

        public void GenerateSpecialZoneWeaponsTestingRange()
        {
            FindLonelyNebulaLocation(out var x, out var y, GalaxyLocationEffectType.None);
            int family = 0;
            switch (Rnd.Next(0, 2))
            {
                case 0:
                    family = ShipImageHelper.ShakturiAlliesFamily;
                    break;
                case 1:
                    family = ShipImageHelper.FreedomAllianceFamily;
                    break;
            }
            DesignSpecification bySubRole = DesignSpecifications.GetBySubRole(BuiltObjectSubRole.CapitalShip);
            Design design = PlayerEmpire.GenerateDesignFromSpec(bySubRole, 4.0);
            DesignSpecification bySubRole2 = DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Cruiser);
            Design design2 = PlayerEmpire.GenerateDesignFromSpec(bySubRole2, 4.0);
            DesignSpecification bySubRole3 = DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Destroyer);
            Design design3 = PlayerEmpire.GenerateDesignFromSpec(bySubRole3, 4.0);
            DesignSpecification bySubRole4 = DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Frigate);
            Design design4 = PlayerEmpire.GenerateDesignFromSpec(bySubRole4, 4.0);
            design4.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(family, design4.SubRole, aged: false);
            design3.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(family, design3.SubRole, aged: false);
            design2.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(family, design2.SubRole, aged: false);
            design.PictureRef = ShipImageHelper.ResolveMajorShipImageIndex(family, design.SubRole, aged: false);
            BuiltObject builtObject = null;
            int num = Rnd.Next(5, 9);
            for (int i = 0; i < num; i++)
            {
                int num2 = Rnd.Next(0, 4);
                Design design5 = null;
                switch (num2)
                {
                    case 0:
                        design5 = design;
                        break;
                    case 1:
                        design5 = design2;
                        break;
                    case 2:
                        design5 = design3;
                        break;
                    case 3:
                        design5 = design4;
                        break;
                }
                double num3 = Rnd.Next(-1000, 1000);
                double num4 = Rnd.Next(-1000, 1000);
                string name = SelectRandomUniqueMilitaryShipName();
                builtObject = GenerateStoryAbandonedBuiltObject(x + num3, y + num4, design5, name);
                if (Rnd.Next(0, 4) > 0)
                {
                    int num5 = Rnd.Next(5, builtObject.Components.Count - 1);
                    num5 = Math.Max(1, (int)((double)num5 * 0.7));
                    for (int j = 0; j < num5; j++)
                    {
                        int index = Rnd.Next(0, builtObject.Components.Count);
                        builtObject.Components[index].Status = ComponentStatus.Damaged;
                    }
                    builtObject.ReDefine();
                }
                builtObject.CurrentFuel = (double)builtObject.FuelCapacity * 0.2 + Rnd.NextDouble() * 0.7 * (double)builtObject.FuelCapacity;
            }
            string name2 = GenerateRestrictedZoneName(x, y, new string[3]
            {
            TextResolver.GetText("Weapons Testing Range"),
            TextResolver.GetText("Test Zone"),
            TextResolver.GetText("Military Test Site")
            });
            string text = TextResolver.GetText("You have entered a military weapons test zone");
            GalaxyLocation galaxyLocation = GenerateRestrictedZone(name2, text, 3000.0, x, y, 3);
            galaxyLocation.RelatedBuiltObject = builtObject;
        }

        private string GenerateRestrictedZoneName(double x, double y, string[] suffixes)
        {
            string text = string.Empty;
            GalaxyLocationList galaxyLocationList = DetermineGalaxyLocationsAtPoint(x, y, GalaxyLocationType.NebulaCloud);
            foreach (GalaxyLocation item in galaxyLocationList)
            {
                if (!string.IsNullOrEmpty(item.Name))
                {
                    text = item.Name;
                    break;
                }
            }
            if (string.IsNullOrEmpty(text))
            {
                Habitat habitat = FastFindNearestSystemWithPlanets(x, y);
                text = habitat.Name;
            }
            if (suffixes != null)
            {
                string text2 = suffixes[Rnd.Next(0, suffixes.Length)];
                text = text + " " + text2;
            }
            return text;
        }

        public GalaxyLocation GenerateRestrictedZone(string name, string message, double size, double x, double y, int soundScheme)
        {
            double num = size / 2.0;
            GalaxyLocation galaxyLocation = new GalaxyLocation(name, GalaxyLocationType.RestrictedArea, x - num, y - num, size, size, -1);
            galaxyLocation.ShowName = true;
            galaxyLocation.Effect = GalaxyLocationEffectType.HyperjumpDisabled;
            galaxyLocation.Shape = GalaxyLocationShape.Circular;
            galaxyLocation.Message = message;
            galaxyLocation.SoundScheme = (short)soundScheme;
            GalaxyLocations.Add(galaxyLocation);
            AddGalaxyLocationIndex(galaxyLocation);
            return galaxyLocation;
        }

        public void FindLonelyDeepSpaceLocation(out double x, out double y)
        {
            Habitat habitat = FindLonelyHabitat();
            int iterationCount = 0;
            while (ConditionCheckLimit(habitat == null, 200, ref iterationCount))
            {
                habitat = FindLonelyHabitat();
            }
            if (habitat != null)
            {
                int num = 0;
                while (CheckNearEmpireColony(habitat.Xpos, habitat.Ypos, 300000.0) && num < 50)
                {
                    habitat = FindLonelyHabitat();
                    num++;
                }
                SelectRelativeParkingPoint(150000.0, out x, out y);
                x += habitat.Xpos;
                y += habitat.Ypos;
            }
            else
            {
                x = Rnd.NextDouble() * (double)SizeX;
                y = Rnd.NextDouble() * (double)SizeY;
            }
        }

        public Habitat FindLonelyHabitatGalacticEdge(RuinType ruinTypeToExclude)
        {
            return FindLonelyHabitat(0.85, 1.0, ruinTypeToExclude, HabitatType.Undefined);
        }

        public Habitat FindLonelyHabitatGalacticEdge(RuinType ruinTypeToExclude, HabitatType habitatTypeToExclude)
        {
            return FindLonelyHabitat(0.85, 1.0, ruinTypeToExclude, habitatTypeToExclude);
        }

        public Habitat FindLonelyHabitat(HabitatType habitatTypeToExclude)
        {
            Habitat habitat = null;
            int num = 0;
            bool flag = true;
            while ((habitat == null || !flag) && num < 50)
            {
                habitat = FindLonelyHabitat();
                if (habitatTypeToExclude != 0 && habitat.Type == habitatTypeToExclude)
                {
                    flag = false;
                }
                num++;
            }
            return habitat;
        }

        public Habitat FindLonelyHabitat()
        {
            return FindLonelyHabitat(0.0, 1.0, RuinType.Undefined, HabitatType.Undefined);
        }

        public Habitat FindLonelyHabitat(RuinType ruinTypeToExclude)
        {
            return FindLonelyHabitat(0.0, 1.0, ruinTypeToExclude, HabitatType.Undefined);
        }

        public Habitat FindLonelyHabitat(RuinType ruinTypeToExclude, HabitatType habitatTypeToExclude)
        {
            return FindLonelyHabitat(0.0, 1.0, ruinTypeToExclude, habitatTypeToExclude);
        }

        public Habitat FindLonelyHabitat(double galaxyRadiusMinimum, double galaxyRadiusMaximum, RuinType ruinTypeToExclude, HabitatType habitatTypeToExclude)
        {
            Habitat habitat = null;
            int num = 0;
            double range = 800000.0;
            double val = Math.Sqrt(1400.0) / Math.Sqrt(StarCount);
            val = Math.Max(1.0, Math.Min(val, 2.5));
            double num2 = 5000000.0;
            ObtainRandomGalaxyCoordinates(galaxyRadiusMinimum, galaxyRadiusMaximum, out var x, out var y);
            double num3 = 0.0;
            double x2 = x;
            double y2 = y;
            Habitat habitat2 = FindNearestColony(x, y, null, 0, includeIndependentColonies: false);
            if (habitat2 != null)
            {
                double num4 = CalculateDistance(x, y, habitat2.Xpos, habitat2.Ypos);
                bool flag = false;
                while ((num4 < num2 || !flag) && num < 120)
                {
                    ObtainRandomGalaxyCoordinates(galaxyRadiusMinimum, galaxyRadiusMaximum, out x, out y);
                    habitat = FindNearestHabitatEmptySystem(x, y);
                    if (habitat != null)
                    {
                        x = habitat.Xpos;
                        y = habitat.Ypos;
                    }
                    habitat2 = FindNearestColony(x, y, null, 0, includeIndependentColonies: false);
                    num4 = CalculateDistance(x, y, habitat2.Xpos, habitat2.Ypos);
                    flag = true;
                    if (ruinTypeToExclude != 0)
                    {
                        Habitat habitat3 = FindNearestRuin(x, y, ruinTypeToExclude);
                        if (habitat3 != null && habitat3.Ruin != null && habitat3.Ruin.Type == ruinTypeToExclude)
                        {
                            double num5 = CalculateDistance(x, y, habitat3.Xpos, habitat3.Ypos);
                            if (num5 < 2000000.0)
                            {
                                flag = false;
                            }
                        }
                    }
                    if (flag && num4 > num3)
                    {
                        GalaxyLocationList galaxyLocationList = DetermineGalaxyLocationsInRangeAtPoint(x, y, range, GalaxyLocationType.DebrisField);
                        GalaxyLocationList galaxyLocationList2 = DetermineGalaxyLocationsInRangeAtPoint(x, y, range, GalaxyLocationType.PlanetDestroyer);
                        if ((galaxyLocationList == null || galaxyLocationList.Count == 0) && (galaxyLocationList2 == null || galaxyLocationList2.Count == 0))
                        {
                            num3 = num4;
                            x2 = x;
                            y2 = y;
                        }
                    }
                    num++;
                    num2 *= 0.97;
                }
            }
            return FindNearestUncolonizedHabitatNonBarrenRock(x2, y2);
        }

        private Habitat FindNearestUncolonizedHabitatNonBarrenRock(double x, double y)
        {
            double num = double.MaxValue;
            double num2 = double.MaxValue;
            double num3 = double.MaxValue;
            Habitat habitat = FindNearestUncolonizedHabitat(x, y, HabitatType.Desert);
            Habitat habitat2 = FindNearestUncolonizedHabitat(x, y, HabitatType.Ice);
            Habitat habitat3 = FindNearestUncolonizedHabitat(x, y, HabitatType.Volcanic);
            if (habitat != null)
            {
                num = CalculateDistanceSquared(x, y, habitat.Xpos, habitat.Ypos);
            }
            if (habitat2 != null)
            {
                num2 = CalculateDistanceSquared(x, y, habitat2.Xpos, habitat2.Ypos);
            }
            if (habitat3 != null)
            {
                num3 = CalculateDistanceSquared(x, y, habitat3.Xpos, habitat3.Ypos);
            }
            if (habitat3 != null && num3 < num && num3 < num2)
            {
                return habitat3;
            }
            if (habitat != null && num < num3 && num < num2)
            {
                return habitat;
            }
            if (habitat2 != null && num2 < num && num2 < num3)
            {
                return habitat2;
            }
            return FindLonelyHabitat(x, y);
        }

        public Habitat FindLonelyHabitat(double x, double y)
        {
            return FindLonelyHabitat(x, y, HabitatType.Undefined);
        }

        public Habitat FindLonelyHabitat(double x, double y, HabitatType habitatTypeToExclude)
        {
            Habitat habitat = FindNearestHabitatEmptySystem(x, y);
            if (habitat == null || (habitatTypeToExclude != 0 && habitat.Type == habitatTypeToExclude))
            {
                habitat = FindNearestUncolonizedHabitat(x, y, HabitatType.Desert);
            }
            if (habitat == null || (habitatTypeToExclude != 0 && habitat.Type == habitatTypeToExclude))
            {
                habitat = FindNearestUncolonizedHabitat(x, y, HabitatType.Ice);
            }
            if (habitat == null || (habitatTypeToExclude != 0 && habitat.Type == habitatTypeToExclude))
            {
                habitat = FindNearestUncolonizedHabitat(x, y, HabitatType.Volcanic);
            }
            return habitat;
        }

        public Habitat FindLonelyColonyLocation(Empire empire)
        {
            double xpos = empire.Capital.Xpos;
            double ypos = empire.Capital.Ypos;
            double num = Rnd.NextDouble() * 600000.0 - 300000.0;
            double num2 = Rnd.NextDouble() * 600000.0 - 300000.0;
            xpos += num;
            ypos += num2;
            xpos = Math.Max(0.0, Math.Min(SizeX, xpos));
            ypos = Math.Max(0.0, Math.Min(SizeY, ypos));
            Habitat habitat = FindNearestColonizableHabitatEmptySystem(xpos, ypos, empire);
            if (habitat == null)
            {
                habitat = FindNearestColonizableHabitatUnoccupiedSystem(xpos, ypos, empire);
            }
            if (habitat == null)
            {
                habitat = FindNearestColonizableHabitat(xpos, ypos, empire);
            }
            if (habitat == null)
            {
                habitat = FindNearestUncolonizedHabitat(xpos, ypos, HabitatType.Ice);
            }
            return habitat;
        }

        public Habitat FindNearestColonyInSystem(SystemInfo system, double x, double y)
        {
            double num = double.MaxValue;
            Habitat result = null;
            if (system != null && system.Habitats != null)
            {
                for (int i = 0; i < system.Habitats.Count; i++)
                {
                    Habitat habitat = system.Habitats[i];
                    if (habitat != null && !habitat.HasBeenDestroyed && habitat.Empire != null && habitat.Population != null && habitat.Population.Count > 0)
                    {
                        double num2 = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
                        if (num2 < num)
                        {
                            result = habitat;
                            num = num2;
                        }
                    }
                }
            }
            return result;
        }

        public Habitat FastFindNearestColonyNotInSystem(double x, double y, Empire empire, int strategicValueThreshhold, Habitat colonyToExclude)
        {
            double num = double.MaxValue;
            Habitat result = null;
            Habitat habitat = DetermineHabitatSystemStar(colonyToExclude);
            for (int i = 0; i < empire.Colonies.Count; i++)
            {
                if (empire.Colonies[i].StrategicValue < strategicValueThreshhold)
                {
                    continue;
                }
                Habitat habitat2 = DetermineHabitatSystemStar(empire.Colonies[i]);
                if (colonyToExclude == null || habitat2 != habitat)
                {
                    double num2 = CalculateDistanceSquared(x, y, empire.Colonies[i].Xpos, empire.Colonies[i].Ypos);
                    if (num2 < num)
                    {
                        result = empire.Colonies[i];
                        num = num2;
                    }
                }
            }
            return result;
        }

        public Habitat FastFindNearestColony(double x, double y, Empire empire, int strategicValueThreshhold)
        {
            return FastFindNearestColony(x, y, empire, strategicValueThreshhold, null);
        }

        public Habitat FastFindNearestColony(double x, double y, Empire empire, int strategicValueThreshhold, Habitat colonyToExclude)
        {
            double num = double.MaxValue;
            Habitat result = null;
            for (int i = 0; i < empire.Colonies.Count; i++)
            {
                if (empire.Colonies[i].StrategicValue >= strategicValueThreshhold && empire.Colonies[i] != colonyToExclude)
                {
                    double num2 = CalculateDistanceSquared(x, y, empire.Colonies[i].Xpos, empire.Colonies[i].Ypos);
                    if (num2 < num)
                    {
                        result = empire.Colonies[i];
                        num = num2;
                    }
                }
            }
            return result;
        }

        public Habitat FastFindNearestColonyBelowApproval(double x, double y, Empire empire, double empireApprovalThreshold)
        {
            double num = double.MaxValue;
            Habitat result = null;
            for (int i = 0; i < empire.Colonies.Count; i++)
            {
                if (empire.Colonies[i].EmpireApprovalRating < empireApprovalThreshold)
                {
                    double num2 = CalculateDistanceSquared(x, y, empire.Colonies[i].Xpos, empire.Colonies[i].Ypos);
                    if (num2 < num)
                    {
                        result = empire.Colonies[i];
                        num = num2;
                    }
                }
            }
            return result;
        }

        public Habitat FindNearestInfectableColonyWithNoPlague(double x, double y)
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
                    Habitat habitat = FindNearestInfectableColonyInIndexWithNoPlague(x, y, index, out distance);
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

        private Habitat FindNearestInfectableColonyInIndexWithNoPlague(double x, double y, GalaxyIndex index, out double distance)
        {
            Habitat habitat = null;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < habitatList.Count; i++)
            {
                Habitat habitat2 = habitatList[i];
                if (habitat2 == null || habitat2.Population == null || habitat2.Population.Count <= 0)
                {
                    continue;
                }
                bool flag = false;
                for (int j = 0; j < habitat2.Population.Count; j++)
                {
                    Population population = habitat2.Population[j];
                    if (population != null && population.Race != null && !population.Race.ImmuneToPlagues)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag && habitat2.PlagueId < 0 && habitat2.PlagueTimeRemaining <= 0f)
                {
                    double num = CalculateDistanceSquared(x, y, habitat2.Xpos, habitat2.Ypos);
                    if (num < distance)
                    {
                        distance = num;
                        habitat = habitat2;
                    }
                }
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        public Habitat FindNearestColony(double x, double y, Empire empire, int strategicValueThreshhold)
        {
            return FindNearestColony(x, y, empire, strategicValueThreshhold, includeIndependentColonies: true);
        }

        public Habitat FindNearestColony(double x, double y, Empire empire, int strategicValueThreshhold, bool includeIndependentColonies)
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
                    Habitat habitat = FindNearestColonyInIndex(x, y, index, out distance, empire, strategicValueThreshhold, includeIndependentColonies);
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

        private Habitat FindNearestColonyInIndex(double x, double y, GalaxyIndex index, out double distance, Empire empire, int strategicValueThreshhold, bool includeIndependentColonies)
        {
            Habitat habitat = null;
            HabitatList habitatList = HabitatIndex[index.X][index.Y];
            distance = double.MaxValue;
            for (int i = 0; i < habitatList.Count; i++)
            {
                if ((includeIndependentColonies || habitatList[i].Empire != IndependentEmpire) && (habitatList[i].Owner == empire || empire == null) && habitatList[i].Population.Count > 0 && habitatList[i].StrategicValue >= strategicValueThreshhold)
                {
                    double num = CalculateDistanceSquared(x, y, habitatList[i].Xpos, habitatList[i].Ypos);
                    if (num < distance)
                    {
                        distance = num;
                        habitat = habitatList[i];
                    }
                }
            }
            if (habitat != null)
            {
                distance = CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
            }
            return habitat;
        }

        public bool IsStellarObjectDockable(StellarObject stellarObject, Empire dockingEmpire)
        {
            if (stellarObject is Habitat)
            {
                Habitat habitat = (Habitat)stellarObject;
                if (habitat.Population == null || habitat.Population.Count == 0)
                {
                    return false;
                }
            }
            Empire empire = stellarObject.Empire;
            if (empire != null && dockingEmpire != null)
            {
                if (empire.PirateEmpireBaseHabitat != null && dockingEmpire != empire && dockingEmpire != IndependentEmpire)
                {
                    return false;
                }
                if (empire == IndependentEmpire && dockingEmpire.PirateEmpireBaseHabitat != null)
                {
                    return true;
                }
                if (dockingEmpire == IndependentEmpire && empire.PirateEmpireBaseHabitat != null)
                {
                    return true;
                }
                if (dockingEmpire.PirateEmpireBaseHabitat != null || empire.PirateEmpireBaseHabitat != null)
                {
                    PirateRelation pirateRelation = dockingEmpire.ObtainPirateRelation(empire);
                    if (pirateRelation.Type != PirateRelationType.Protection)
                    {
                        return false;
                    }
                }
                else
                {
                    DiplomaticRelation diplomaticRelation = dockingEmpire.DiplomaticRelations[empire];
                    if (diplomaticRelation != null && (diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions || diplomaticRelation.Type == DiplomaticRelationType.War))
                    {
                        return false;
                    }
                }
            }
            else if (empire == null)
            {
                return false;
            }
            if (stellarObject is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)stellarObject;
                if (builtObject.IsBlockaded)
                {
                    return false;
                }
            }
            else if (stellarObject is Habitat)
            {
                Habitat habitat2 = (Habitat)stellarObject;
                if (habitat2.IsBlockaded)
                {
                    return false;
                }
            }
            return true;
        }

        public int CalculateMaximumOrderFulfillmentDistance(Habitat habitat)
        {
            return CalculateMaximumOrderFulfillmentDistance(habitat.Xpos, habitat.Ypos);
        }

        public int CalculateMaximumOrderFulfillmentDistance(BuiltObject builtObject)
        {
            return CalculateMaximumOrderFulfillmentDistance(builtObject.Xpos, builtObject.Ypos);
        }

        public int CalculateMaximumOrderFulfillmentDistance(double xPos, double yPos)
        {
            int num = 0;
            switch (GalaxyShape)
            {
                case GalaxyShape.Elliptical:
                    {
                        int x = (int)xPos / SectorSize;
                        int y = (int)yPos / SectorSize;
                        CorrectSectorCoords(ref x, ref y);
                        num = ((x <= 3 || x >= 8 || y <= 3 || y >= 8) ? (TypicalMaximumOrderFulfillmentDistance * 2) : TypicalMaximumOrderFulfillmentDistance);
                        break;
                    }
                case GalaxyShape.Irregular:
                case GalaxyShape.ClustersEven:
                case GalaxyShape.ClustersVaried:
                    num = TypicalMaximumOrderFulfillmentDistance;
                    break;
                case GalaxyShape.Ring:
                    num = TypicalMaximumOrderFulfillmentDistance * 2;
                    break;
                case GalaxyShape.Spiral:
                    {
                        int num2 = (int)CalculateDistance(SizeX / 2, SizeY / 2, yPos, yPos);
                        num = Math.Min(1, num2 / (SizeX / 4)) * TypicalMaximumOrderFulfillmentDistance;
                        break;
                    }
            }
            if (SectorWidth > 10 || SectorHeight > 10)
            {
                num = (int)((double)num * ((double)Math.Max(SectorWidth, SectorHeight) / 10.0));
            }
            return num;
        }

        public static double ReduceAngle(double currentangle)
        {
            if (currentangle >= Math.PI * 2.0)
            {
                currentangle -= Math.PI * 2.0;
            }
            return currentangle;
        }

        public static double IncreaseAngle(double currentangle)
        {
            if (currentangle <= Math.PI * 2.0)
            {
                currentangle += Math.PI * 2.0;
            }
            return currentangle;
        }

        public static void SetResearchComponentMaxTechPoints(int baseTechCost)
        {
            ResearchSystem.CalculateComponentMinMaxTechPoints(baseTechCost, ResearchNodeDefinitionsStatic);
        }

        public static void SetResearchRaceSpecialProjects(RaceList races)
        {
            for (int i = 0; i < ResearchNodeDefinitionsStatic.Count; i++)
            {
                if (ResearchNodeDefinitionsStatic[i].AllowedRaces != null)
                {
                    ResearchNodeDefinitionsStatic[i].AllowedRaces.Clear();
                }
                if (ResearchNodeDefinitionsStatic[i].DisallowedRaces != null)
                {
                    ResearchNodeDefinitionsStatic[i].DisallowedRaces.Clear();
                }
            }
            for (int j = 0; j < ResearchNodeDefinitionsStatic.Count; j++)
            {
                if (ResearchNodeDefinitionsStatic[j].SpecifiedRaces == null || ResearchNodeDefinitionsStatic[j].SpecifiedRaces.Count <= 0)
                {
                    continue;
                }
                for (int k = 0; k < ResearchNodeDefinitionsStatic[j].SpecifiedRaces.Count; k++)
                {
                    if (ResearchNodeDefinitionsStatic[j].SpecifiedRaces[k] == null)
                    {
                        continue;
                    }
                    Race race = races[ResearchNodeDefinitionsStatic[j].SpecifiedRaces[k].Name];
                    if (race != null)
                    {
                        if (ResearchNodeDefinitionsStatic[j].AllowedRaces == null)
                        {
                            ResearchNodeDefinitionsStatic[j].AllowedRaces = new RaceList();
                        }
                        if (!ResearchNodeDefinitionsStatic[j].AllowedRaces.Contains(race))
                        {
                            ResearchNodeDefinitionsStatic[j].AllowedRaces.Add(race);
                        }
                    }
                }
            }
            for (int l = 0; l < races.Count; l++)
            {
                if (races[l].SpecialComponent != null)
                {
                    for (int m = 0; m < ResearchNodeDefinitionsStatic.Count; m++)
                    {
                        if (ResearchNodeDefinitionsStatic[m].Components != null && ResearchNodeDefinitionsStatic[m].Components.Count > 0)
                        {
                            for (int n = 0; n < ResearchNodeDefinitionsStatic[m].Components.Count; n++)
                            {
                                if (ResearchNodeDefinitionsStatic[m].Components[n].ComponentID == races[l].SpecialComponent.ComponentID)
                                {
                                    if (ResearchNodeDefinitionsStatic[m].AllowedRaces == null)
                                    {
                                        ResearchNodeDefinitionsStatic[m].AllowedRaces = new RaceList();
                                    }
                                    if (!ResearchNodeDefinitionsStatic[m].AllowedRaces.Contains(races[l]))
                                    {
                                        ResearchNodeDefinitionsStatic[m].AllowedRaces.Add(races[l]);
                                    }
                                    break;
                                }
                            }
                        }
                        if (ResearchNodeDefinitionsStatic[m].ComponentImprovements == null || ResearchNodeDefinitionsStatic[m].ComponentImprovements.Count <= 0)
                        {
                            continue;
                        }
                        for (int num = 0; num < ResearchNodeDefinitionsStatic[m].ComponentImprovements.Count; num++)
                        {
                            if (ResearchNodeDefinitionsStatic[m].ComponentImprovements[num].ImprovedComponent.ComponentID == races[l].SpecialComponent.ComponentID)
                            {
                                if (ResearchNodeDefinitionsStatic[m].AllowedRaces == null)
                                {
                                    ResearchNodeDefinitionsStatic[m].AllowedRaces = new RaceList();
                                }
                                if (!ResearchNodeDefinitionsStatic[m].AllowedRaces.Contains(races[l]))
                                {
                                    ResearchNodeDefinitionsStatic[m].AllowedRaces.Add(races[l]);
                                }
                                break;
                            }
                        }
                    }
                }
                if (races[l].VictoryConditions != null)
                {
                    for (int num2 = 0; num2 < races[l].VictoryConditions.Count; num2++)
                    {
                        RaceVictoryCondition raceVictoryCondition = races[l].VictoryConditions[num2];
                        if (raceVictoryCondition == null || raceVictoryCondition.Type != RaceVictoryConditionType.BuildWonder || !(raceVictoryCondition.AdditionalData is PlanetaryFacilityDefinition))
                        {
                            continue;
                        }
                        PlanetaryFacilityDefinition planetaryFacilityDefinition = (PlanetaryFacilityDefinition)raceVictoryCondition.AdditionalData;
                        if (planetaryFacilityDefinition.Type != PlanetaryFacilityType.Wonder || planetaryFacilityDefinition.WonderType != WonderType.RaceAchievement)
                        {
                            continue;
                        }
                        for (int num3 = 0; num3 < ResearchNodeDefinitionsStatic.Count; num3++)
                        {
                            if (ResearchNodeDefinitionsStatic[num3].PlanetaryFacility != null && ResearchNodeDefinitionsStatic[num3].PlanetaryFacility.PlanetaryFacilityDefinitionId == planetaryFacilityDefinition.PlanetaryFacilityDefinitionId)
                            {
                                if (ResearchNodeDefinitionsStatic[num3].AllowedRaces == null)
                                {
                                    ResearchNodeDefinitionsStatic[num3].AllowedRaces = new RaceList();
                                }
                                if (!ResearchNodeDefinitionsStatic[num3].AllowedRaces.Contains(races[l]))
                                {
                                    ResearchNodeDefinitionsStatic[num3].AllowedRaces.Add(races[l]);
                                }
                                break;
                            }
                        }
                    }
                }
                if (races[l].DisallowedResearchAreas != null && races[l].DisallowedResearchAreas.Count > 0)
                {
                    for (int num4 = 0; num4 < ResearchNodeDefinitionsStatic.Count; num4++)
                    {
                        if (races[l].DisallowedResearchAreas.Contains(ResearchNodeDefinitionsStatic[num4].Category))
                        {
                            if (ResearchNodeDefinitionsStatic[num4].DisallowedRaces == null)
                            {
                                ResearchNodeDefinitionsStatic[num4].DisallowedRaces = new RaceList();
                            }
                            if (!ResearchNodeDefinitionsStatic[num4].DisallowedRaces.Contains(races[l]))
                            {
                                ResearchNodeDefinitionsStatic[num4].DisallowedRaces.Add(races[l]);
                            }
                            if (ResearchNodeDefinitionsStatic[num4].AllowedRaces != null && ResearchNodeDefinitionsStatic[num4].AllowedRaces.Contains(races[l]))
                            {
                                ResearchNodeDefinitionsStatic[num4].AllowedRaces.Remove(races[l]);
                            }
                        }
                    }
                }
                if (races[l].DisallowedComponents == null || races[l].DisallowedComponents.Count <= 0)
                {
                    continue;
                }
                for (int num5 = 0; num5 < ResearchNodeDefinitionsStatic.Count; num5++)
                {
                    for (int num6 = 0; num6 < ResearchNodeDefinitionsStatic[num5].Components.Count; num6++)
                    {
                        if (races[l].DisallowedComponents.Contains(ResearchNodeDefinitionsStatic[num5].Components[num6]))
                        {
                            if (ResearchNodeDefinitionsStatic[num5].DisallowedRaces == null)
                            {
                                ResearchNodeDefinitionsStatic[num5].DisallowedRaces = new RaceList();
                            }
                            if (!ResearchNodeDefinitionsStatic[num5].DisallowedRaces.Contains(races[l]))
                            {
                                ResearchNodeDefinitionsStatic[num5].DisallowedRaces.Add(races[l]);
                            }
                            if (ResearchNodeDefinitionsStatic[num5].AllowedRaces != null && ResearchNodeDefinitionsStatic[num5].AllowedRaces.Contains(races[l]))
                            {
                                ResearchNodeDefinitionsStatic[num5].AllowedRaces.Remove(races[l]);
                            }
                        }
                    }
                    for (int num7 = 0; num7 < ResearchNodeDefinitionsStatic[num5].ComponentImprovements.Count; num7++)
                    {
                        if (races[l].DisallowedComponents.Contains(ResearchNodeDefinitionsStatic[num5].ComponentImprovements[num7].ImprovedComponent))
                        {
                            if (ResearchNodeDefinitionsStatic[num5].DisallowedRaces == null)
                            {
                                ResearchNodeDefinitionsStatic[num5].DisallowedRaces = new RaceList();
                            }
                            if (!ResearchNodeDefinitionsStatic[num5].DisallowedRaces.Contains(races[l]))
                            {
                                ResearchNodeDefinitionsStatic[num5].DisallowedRaces.Add(races[l]);
                            }
                            if (ResearchNodeDefinitionsStatic[num5].AllowedRaces != null && ResearchNodeDefinitionsStatic[num5].AllowedRaces.Contains(races[l]))
                            {
                                ResearchNodeDefinitionsStatic[num5].AllowedRaces.Remove(races[l]);
                            }
                        }
                    }
                }
            }
        }

        public static void InitializeRaceFamilyBiases(string applicationStartupPath, string customizationSetName, ref RaceFamilyList raceFamilies)
        {
            if (!Main._ExpModMain.GetSettings().UseDbFiles)
            {
                string text = "raceFamilyBiases.txt";
                string filePath = applicationStartupPath + "\\" + text;
                if (!string.IsNullOrEmpty(customizationSetName))
                {
                    string text2 = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\" + text;
                    if (File.Exists(text2))
                    {
                        filePath = text2;
                    }
                }
                RaceFamilyBiasList.LoadFromFile(filePath, ref raceFamilies);
            }
            else
            {
                RaceFamilyBiasList.LoadFromFile(Main._FileDB.GetRaceFamilyBiasReader(), ref raceFamilies);
            }
        }

        public static RaceFamilyList InitializeRaceFamilies(string applicationStartupPath, string customizationSetName)
        {
            RaceFamilyList raceFamilyList = new RaceFamilyList();
            if (!Main._ExpModMain.GetSettings().UseDbFiles)
            {
                string text = "raceFamilies.txt";
                string filePath = applicationStartupPath + "\\" + text;
                if (!string.IsNullOrEmpty(customizationSetName))
                {
                    string text2 = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\" + text;
                    if (File.Exists(text2))
                    {
                        filePath = text2;
                    }
                }
                raceFamilyList.LoadFromFile(filePath);
            }
            else
            {
                raceFamilyList.LoadFromFile(Main._FileDB.GetRaceFamilyReader());
            }
            return raceFamilyList;
        }

        public static PlagueList InitializePlagues(string applicationStartupPath, string customizationSetName)
        {
            PlagueList plagueList = new PlagueList();
            if (!Main._ExpModMain.GetSettings().UseDbFiles)
            {
                string text = "plagues.txt";
                string filePath = applicationStartupPath + "\\" + text;
                if (!string.IsNullOrEmpty(customizationSetName))
                {
                    string text2 = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\" + text;
                    if (File.Exists(text2))
                    {
                        filePath = text2;
                    }
                }
                plagueList.LoadFromFile(filePath);
            }
            {
                plagueList.LoadFromFile(Main._FileDB.GetPlaguesReader());
            }
            return plagueList;
        }

        public static void InitializeGovernmentBiases(string applicationStartupPath, string customizationSetName, ref GovernmentAttributesList governments)
        {
            if (!Main._ExpModMain.GetSettings().UseDbFiles)
            {
                string text = "governmentBiases.txt";
                string filePath = applicationStartupPath + "\\" + text;
                if (!string.IsNullOrEmpty(customizationSetName))
                {
                    string text2 = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\" + text;
                    if (File.Exists(text2))
                    {
                        filePath = text2;
                    }
                }
                GovernmentBiasList.LoadFromFile(filePath, ref governments);
            }
            else
            {
                GovernmentBiasList.LoadFromFile(Main._FileDB.GetGovernmentsBiasReader(), ref governments);
            }
        }

        public static GovernmentAttributesList InitializeGovernments(string applicationStartupPath, string customizationSetName)
        {
            GovernmentAttributesList governmentAttributesList = new GovernmentAttributesList();
            if (!Main._ExpModMain.GetSettings().UseDbFiles)
            {
                string text = "governments.txt";
                string filePath = applicationStartupPath + "\\" + text;
                if (!string.IsNullOrEmpty(customizationSetName))
                {
                    string text2 = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\" + text;
                    if (File.Exists(text2))
                    {
                        filePath = text2;
                    }
                }
                governmentAttributesList.LoadFromFile(filePath);
            }
            else
            { governmentAttributesList.LoadFromFile(Main._FileDB.GetGovernmentsReader()); }
            return governmentAttributesList;
        }

        public static PlanetaryFacilityDefinitionList InitializePlanetaryFacilityDefinitions(string applicationStartupPath, string customizationSetName)
        {
            PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList = new PlanetaryFacilityDefinitionList();
            if (!Main._ExpModMain.GetSettings().UseDbFiles)
            {
                string text = "facilities.txt";
                string filePath = applicationStartupPath + "\\" + text;
                if (!string.IsNullOrEmpty(customizationSetName))
                {
                    string text2 = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\" + text;
                    if (File.Exists(text2))
                    {
                        filePath = text2;
                    }
                }
                planetaryFacilityDefinitionList.LoadFromFile(filePath);
            }
            else
            { planetaryFacilityDefinitionList.LoadFromFile(Main._FileDB.GetFacilitiesReader()); }
            return planetaryFacilityDefinitionList;
        }

        public static ResourceSystem InitializeResourceDefinitions(string applicationStartupPath, string customizationSetName)
        {
            ResourceSystem resourceSystem = new ResourceSystem();

            if (!Main._ExpModMain.GetSettings().UseDbFiles)
            {
                string text = "resources.txt";
                string filePath = applicationStartupPath + "\\" + text;
                if (!string.IsNullOrEmpty(customizationSetName))
                {
                    string text2 = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\" + text;
                    if (File.Exists(text2))
                    {
                        filePath = text2;
                    }
                }
                resourceSystem.LoadFromFile(filePath);
            }
            else
            {
                resourceSystem.LoadFromFile(Main._FileDB.GetResourcesReader());
            }
            return resourceSystem;
        }

        public static void GenerateOrderedComponentLists()
        {
            ComponentsWeaponBeamOrderedByRange = GenerateOrderedComponentList(ComponentCategoryType.WeaponBeam, 2);
            ComponentsWeaponTorpedoOrderedByRange = GenerateOrderedComponentList(ComponentCategoryType.WeaponTorpedo, 2);
            ComponentsWeaponAreaOrderedByRange = GenerateOrderedComponentList(ComponentType.WeaponAreaDestruction, 2);
            ComponentsWeaponBeamOrderedByPower = GenerateOrderedComponentList(ComponentCategoryType.WeaponBeam, 1);
            ComponentsWeaponTorpedoOrderedByPower = GenerateOrderedComponentList(ComponentCategoryType.WeaponTorpedo, 1);
            ComponentsWeaponAreaOrderedByPower = GenerateOrderedComponentList(ComponentType.WeaponAreaDestruction, 1);
            ComponentsReactorOrderedByEfficiency = GenerateOrderedComponentList(ComponentCategoryType.Reactor, 3, 2);
            ComponentsReactorOrderedByEfficiency.Reverse();
            ComponentsReactorOrderedByPower = GenerateOrderedComponentList(ComponentCategoryType.Reactor, 1);
            ComponentsEngineMainThrustOrderedByPower = GenerateOrderedComponentList(ComponentType.EngineMainThrust, 1);
            ComponentsEngineVectoringOrderedByPower = GenerateOrderedComponentList(ComponentType.EngineVectoring, 1);
            ComponentsEngineMainThrustOrderedByEfficiency = GenerateOrderedComponentList(ComponentType.EngineMainThrust, 1, 2);
            ComponentsEngineVectoringOrderedByEfficiency = GenerateOrderedComponentList(ComponentType.EngineVectoring, 1, 2);
            ComponentsHyperdriveOrderedByPower = GenerateOrderedComponentList(ComponentCategoryType.HyperDrive, 1);
            ComponentsHyperdriveOrderedByEfficiency = GenerateOrderedComponentList(ComponentCategoryType.HyperDrive, 1, 2);
            ComponentsHyperdriveOrderedByJumpInitiation = GenerateOrderedComponentList(ComponentCategoryType.HyperDrive, 3, orderHighestToLowest: false);
        }

        public static ComponentDefinition[] InitializeComponentDefinitions(ResourceSystem resourceSystem, string applicationStartupPath, string customizationSetName)
        {
            ComponentDefinitionList componentDefinitionList = new ComponentDefinitionList();
            string text = "components.txt";
            string filePath = applicationStartupPath + "\\" + text;
            if (!string.IsNullOrEmpty(customizationSetName))
            {
                string text2 = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\" + text;
                if (File.Exists(text2))
                {
                    filePath = text2;
                }
            }
            componentDefinitionList.LoadFromFile(filePath);
            return componentDefinitionList.ToArray();
        }

        public static void InitializeComponentDefinitions_OLD(ResourceSystem resourceSystem)
        {
            object[] array = new object[130];
            object[] array2 = new object[130];
            array[0] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponBeam,
            ComponentType.WeaponBeam,
            0,
            "Maxos Blaster",
            5,
            48000,
            0,
            5,
            190,
            12,
            360,
            1,
            1240
            };
            array2[0] = new int[10] { 1, 2, 4, 4, 14, 3, 0, 0, 0, 0 };
            array[1] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponBeam,
            ComponentType.WeaponBeam,
            0,
            "Shatterforce Laser",
            4,
            140000,
            0,
            7,
            320,
            20,
            310,
            1,
            1500
            };
            array2[1] = new int[10] { 0, 3, 6, 5, 15, 5, 0, 0, 0, 0 };
            array[2] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponBeam,
            ComponentType.WeaponBeam,
            0,
            "Impact Assault Blaster",
            5,
            220000,
            0,
            12,
            220,
            38,
            260,
            3,
            1700
            };
            array2[2] = new int[10] { 2, 3, 5, 4, 14, 5, 12, 4, 0, 0 };
            array[3] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponBeam,
            ComponentType.WeaponBeam,
            0,
            "Titan Beam",
            6,
            830000,
            0,
            20,
            390,
            28,
            330,
            4,
            1400
            };
            array2[3] = new int[10] { 3, 4, 7, 3, 15, 5, 12, 5, 0, 0 };
            array[4] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponBeam,
            ComponentType.WeaponBeam,
            0,
            "PulseWave Cannon",
            5,
            1900000,
            0,
            13,
            310,
            24,
            350,
            3,
            1400
            };
            array2[4] = new int[10] { 3, 4, 7, 3, 15, 5, 12, 5, 0, 0 };
            array[5] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponTorpedo,
            ComponentType.WeaponTorpedo,
            0,
            "Epsilon Torpedo",
            15,
            64000,
            0,
            11,
            300,
            30,
            60,
            3,
            2900
            };
            array2[5] = new int[10] { 1, 2, 4, 4, 14, 3, 12, 7, 0, 0 };
            array[6] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponTorpedo,
            ComponentType.WeaponTorpedo,
            0,
            "Velocity Shard",
            11,
            190000,
            0,
            16,
            630,
            44,
            120,
            2,
            3300
            };
            array2[6] = new int[10] { 0, 2, 6, 4, 15, 5, 12, 9, 0, 0 };
            array[7] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponTorpedo,
            ComponentType.WeaponTorpedo,
            0,
            "Shockwave Torpedo",
            12,
            280000,
            0,
            24,
            430,
            60,
            75,
            4,
            3800
            };
            array2[7] = new int[10] { 2, 2, 5, 4, 14, 4, 12, 8, 0, 0 };
            array[8] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponTorpedo,
            ComponentType.WeaponTorpedo,
            0,
            "Plasma Thunderbolt",
            12,
            980000,
            0,
            36,
            690,
            64,
            125,
            4,
            3200
            };
            array2[8] = new int[10] { 3, 3, 7, 3, 15, 3, 12, 8, 0, 0 };
            array[9] = new object[15]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponTorpedo,
            ComponentType.WeaponTorpedo,
            0,
            "Shaktur FireStorm",
            12,
            1560000,
            0,
            36,
            295,
            52,
            65,
            10,
            2900,
            6
            };
            array2[9] = new int[10] { 3, 4, 7, 4, 15, 4, 12, 10, 0, 0 };
            array[10] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponTorpedo,
            ComponentType.WeaponMissile,
            0,
            "Concussion Missile",
            10,
            240000,
            0,
            6,
            520,
            18,
            120,
            0,
            2700
            };
            array2[10] = new int[10] { 11, 3, 17, 2, 6, 5, 0, 0, 0, 0 };
            array[11] = new object[15]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponTorpedo,
            ComponentType.WeaponBombard,
            0,
            "Nuclear Devastator",
            8,
            128000,
            0,
            0,
            210,
            15,
            50,
            0,
            6000,
            3
            };
            array2[11] = new int[10] { 3, 4, 7, 4, 15, 4, 12, 10, 0, 0 };
            array[12] = new object[15]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponTorpedo,
            ComponentType.WeaponBombard,
            0,
            "Nuclear Exterminator",
            14,
            1090000,
            0,
            0,
            270,
            44,
            60,
            13,
            6700,
            8
            };
            array2[12] = new int[10] { 3, 4, 7, 5, 15, 6, 12, 11, 0, 0 };
            array[13] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponPointDefense,
            ComponentType.WeaponPointDefense,
            0,
            "Point Defense Cannon",
            3,
            60000,
            0,
            3,
            140,
            4,
            430,
            1,
            540
            };
            array2[13] = new int[10] { 1, 2, 8, 4, 13, 3, 15, 2, 0, 0 };
            array[14] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponPointDefense,
            ComponentType.WeaponPointDefense,
            0,
            "Terminator AutoCannon",
            3,
            370000,
            0,
            6,
            190,
            6,
            550,
            1,
            480
            };
            array2[14] = new int[10] { 0, 2, 8, 4, 13, 4, 15, 3, 0, 0 };
            array[15] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponIon,
            ComponentType.WeaponIonCannon,
            0,
            "Ion Cannon",
            12,
            310000,
            0,
            20,
            260,
            80,
            230,
            5,
            3300
            };
            array2[15] = new int[10] { 13, 6, 14, 4, 5, 5, 12, 2, 0, 0 };
            array[16] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponIon,
            ComponentType.WeaponIonPulse,
            0,
            "Ion Pulse",
            20,
            570000,
            0,
            24,
            210,
            125,
            120,
            7,
            6600
            };
            array2[16] = new int[10] { 13, 9, 14, 6, 5, 6, 12, 3, 0, 0 };
            array[17] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponIon,
            ComponentType.WeaponIonDefense,
            0,
            "Ion Defense",
            2,
            430000,
            0,
            18,
            0,
            0,
            0,
            0,
            0
            };
            array2[17] = new int[10] { 15, 5, 9, 6, 3, 4, 0, 0, 0, 0 };
            array[18] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.HyperDisrupt,
            ComponentType.HyperDeny,
            0,
            "HyperDeny GW1000",
            12,
            125000,
            5,
            3,
            340,
            2,
            0,
            0,
            0
            };
            array2[18] = new int[10] { 1, 3, 4, 6, 14, 3, 11, 2, 0, 0 };
            array[19] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponArea,
            ComponentType.WeaponAreaDestruction,
            0,
            "Intimidator Surgewave",
            16,
            190000,
            0,
            35,
            220,
            54,
            120,
            13,
            8200
            };
            array2[19] = new int[10] { 1, 4, 5, 7, 14, 5, 11, 3, 0, 0 };
            array[20] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.HyperDisrupt,
            ComponentType.HyperDeny,
            0,
            "HyperDeny GW4000",
            14,
            670000,
            6,
            6,
            1020,
            2,
            0,
            0,
            0
            };
            array2[20] = new int[10] { 0, 4, 5, 7, 15, 4, 11, 3, 0, 0 };
            array[21] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponArea,
            ComponentType.WeaponAreaDestruction,
            0,
            "Derasian Shockwave",
            18,
            270000,
            0,
            74,
            300,
            90,
            150,
            21,
            9000
            };
            array2[21] = new int[10] { 0, 5, 6, 7, 15, 5, 11, 4, 3, 2 };
            array[22] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.HyperDisrupt,
            ComponentType.HyperStop,
            0,
            "Gravity Well Projector",
            52,
            790000,
            1,
            6,
            1800,
            92,
            0,
            0,
            0
            };
            array2[22] = new int[10] { 0, 28, 5, 50, 15, 22, 11, 24, 0, 0 };
            array[23] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponSuperBeam,
            ComponentType.WeaponSuperBeam,
            0,
            "Death Ray",
            140,
            9999999,
            0,
            1800,
            440,
            400,
            370,
            270,
            8500
            };
            array2[23] = new int[10] { 3, 180, 7, 120, 15, 190, 12, 110, 0, 0 };
            array[24] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponSuperArea,
            ComponentType.WeaponSuperArea,
            0,
            "Devastator Pulse",
            170,
            9999999,
            0,
            1200,
            520,
            470,
            125,
            210,
            12000
            };
            array2[24] = new int[10] { 3, 150, 7, 130, 15, 120, 11, 80, 0, 0 };
            array[25] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponSuperBeam,
            ComponentType.WeaponSuperBeam,
            0,
            "Super Laser",
            640,
            19999999,
            0,
            30000,
            700,
            800,
            450,
            380,
            32000
            };
            array2[25] = new int[10] { 3, 680, 7, 460, 15, 320, 12, 240, 0, 0 };
            array[26] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.Fighter,
            ComponentType.FighterBay,
            0,
            "Standard Fighter Bay",
            50,
            100000,
            4,
            40,
            4,
            0,
            0,
            0,
            0
            };
            array2[26] = new int[10] { 10, 20, 17, 7, 16, 4, 0, 0, 0, 0 };
            array[27] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.Fighter,
            ComponentType.FighterBay,
            0,
            "Advanced Fighter Bay",
            45,
            450000,
            5,
            40,
            7,
            0,
            0,
            0,
            0
            };
            array2[27] = new int[10] { 10, 16, 17, 5, 16, 3, 0, 0, 0, 0 };
            array[28] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.Armor,
            ComponentType.Armor,
            0,
            "Standard Armor",
            1,
            54000,
            0,
            10,
            2,
            0,
            0,
            0,
            0
            };
            array2[28] = new int[10] { 10, 5, 0, 0, 0, 0, 0, 0, 0, 0 };
            array[29] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.Armor,
            ComponentType.Armor,
            0,
            "Enhanced Armor",
            1,
            170000,
            0,
            18,
            4,
            0,
            0,
            0,
            0
            };
            array2[29] = new int[10] { 17, 1, 10, 4, 0, 0, 0, 0, 0, 0 };
            array[30] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.Armor,
            ComponentType.Armor,
            0,
            "Reactive Armor",
            1,
            380000,
            0,
            25,
            7,
            0,
            0,
            0,
            0
            };
            array2[30] = new int[10] { 17, 1, 11, 3, 12, 2, 0, 0, 0, 0 };
            array[31] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.Armor,
            ComponentType.Armor,
            0,
            "UltraDense Armor",
            1,
            710000,
            0,
            40,
            10,
            0,
            0,
            0,
            0
            };
            array2[31] = new int[10] { 17, 1, 11, 4, 12, 3, 0, 0, 0, 0 };
            array[32] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Shields,
            ComponentType.Shields,
            0,
            "Corvidian Shields",
            10,
            50000,
            0,
            100,
            3,
            0,
            0,
            0,
            0
            };
            array2[32] = new int[10] { 4, 3, 14, 4, 13, 8, 0, 0, 0, 0 };
            array[33] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Shields,
            ComponentType.Shields,
            0,
            "Talassos Shields",
            10,
            90000,
            0,
            130,
            8,
            0,
            0,
            0,
            0
            };
            array2[33] = new int[10] { 5, 3, 14, 5, 13, 7, 0, 0, 0, 0 };
            array[34] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Shields,
            ComponentType.Shields,
            0,
            "Deucalios Shields",
            10,
            170000,
            0,
            180,
            4,
            0,
            0,
            0,
            0
            };
            array2[34] = new int[10] { 6, 4, 14, 4, 13, 8, 0, 0, 0, 0 };
            array[35] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Shields,
            ComponentType.Shields,
            0,
            "Meridian Shields",
            10,
            470000,
            0,
            220,
            10,
            0,
            0,
            0,
            0
            };
            array2[35] = new int[10] { 7, 4, 14, 5, 13, 9, 3, 3, 0, 0 };
            array[36] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Shields,
            ComponentType.Shields,
            0,
            "Megatron Z4",
            9,
            2000000,
            0,
            155,
            12,
            0,
            0,
            0,
            0
            };
            array2[36] = new int[10] { 6, 4, 14, 4, 13, 8, 3, 3, 7, 2 };
            array[37] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.ShieldRecharge,
            ComponentType.ShieldRecharge,
            0,
            "Area Shield Recharge",
            20,
            560000,
            0,
            250,
            400,
            600,
            0,
            0,
            0
            };
            array2[37] = new int[10] { 4, 20, 14, 10, 13, 6, 3, 12, 12, 8 };
            array[38] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Engine,
            ComponentType.EngineMainThrust,
            0,
            "Proton Thruster",
            7,
            40000,
            0,
            1000,
            5,
            560,
            2,
            0,
            0
            };
            array2[38] = new int[10] { 17, 1, 10, 4, 0, 0, 0, 0, 0, 0 };
            array[39] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Engine,
            ComponentType.EngineMainThrust,
            0,
            "Quantum Engine",
            8,
            175000,
            0,
            1230,
            5,
            620,
            2,
            0,
            0
            };
            array2[39] = new int[10] { 17, 2, 11, 3, 0, 0, 0, 0, 0, 0 };
            array[40] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Engine,
            ComponentType.EngineMainThrust,
            0,
            "Acceleros Engine",
            8,
            230000,
            0,
            1540,
            8,
            720,
            3,
            0,
            0
            };
            array2[40] = new int[10] { 17, 2, 11, 3, 0, 0, 0, 0, 0, 0 };
            array[41] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Engine,
            ComponentType.EngineMainThrust,
            0,
            "Vortex Engine",
            8,
            620000,
            0,
            1630,
            6,
            950,
            3,
            0,
            0
            };
            array2[41] = new int[10] { 17, 2, 11, 4, 0, 0, 0, 0, 0, 0 };
            array[42] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Engine,
            ComponentType.EngineMainThrust,
            0,
            "TurboThruster",
            7,
            1900000,
            0,
            1380,
            3,
            850,
            1,
            0,
            0
            };
            array2[42] = new int[10] { 17, 1, 11, 3, 0, 0, 0, 0, 0, 0 };
            array[43] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Engine,
            ComponentType.EngineMainThrust,
            0,
            "StarBurner",
            7,
            1900000,
            0,
            1880,
            7,
            1180,
            4,
            0,
            0
            };
            array2[43] = new int[10] { 17, 2, 11, 3, 0, 0, 0, 0, 0, 0 };
            array[44] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Engine,
            ComponentType.EngineVectoring,
            0,
            "Thrust Vector",
            2,
            70000,
            0,
            6,
            1,
            0,
            0,
            0,
            0
            };
            array2[44] = new int[10] { 17, 1, 10, 2, 0, 0, 0, 0, 0, 0 };
            array[45] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Engine,
            ComponentType.EngineVectoring,
            0,
            "Multi Vector",
            2,
            560000,
            0,
            12,
            1,
            0,
            0,
            0,
            0
            };
            array2[45] = new int[10] { 17, 1, 11, 3, 0, 0, 0, 0, 0, 0 };
            array[46] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Engine,
            ComponentType.EngineVectoring,
            0,
            "Swift Vector",
            2,
            1900000,
            0,
            10,
            1,
            0,
            0,
            0,
            0
            };
            array2[46] = new int[10] { 17, 1, 11, 3, 0, 0, 0, 0, 0, 0 };
            array[47] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.HyperDrive,
            ComponentType.HyperDrive,
            0,
            "Gerax HyperDrive",
            11,
            75000,
            0,
            12500,
            78,
            15,
            0,
            0,
            0
            };
            array2[47] = new int[10] { 17, 5, 4, 9, 10, 7, 0, 0, 0, 0 };
            array[48] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.HyperDrive,
            ComponentType.HyperDrive,
            0,
            "Kaldos HyperDrive",
            9,
            160000,
            0,
            13750,
            94,
            7,
            0,
            0,
            0
            };
            array2[48] = new int[10] { 17, 4, 5, 9, 10, 8, 0, 0, 0, 0 };
            array[49] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.HyperDrive,
            ComponentType.HyperDrive,
            0,
            "Equinox JumpDrive",
            9,
            220000,
            0,
            18750,
            88,
            13,
            0,
            0,
            0
            };
            array2[49] = new int[10] { 17, 4, 6, 10, 12, 5, 10, 4, 0, 0 };
            array[50] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.HyperDrive,
            ComponentType.HyperDrive,
            0,
            "Calista-Dal WarpDrive",
            9,
            190000,
            0,
            15000,
            60,
            12,
            0,
            0,
            0
            };
            array2[50] = new int[10] { 17, 5, 7, 7, 12, 5, 10, 6, 0, 0 };
            array[51] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.HyperDrive,
            ComponentType.HyperDrive,
            0,
            "Torrent Drive",
            9,
            1270000,
            0,
            25000,
            83,
            6,
            0,
            0,
            0
            };
            array2[51] = new int[10] { 17, 5, 7, 8, 12, 6, 10, 6, 0, 0 };
            array[52] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.HyperDrive,
            ComponentType.HyperDrive,
            0,
            "VelocityDrive",
            8,
            1500000,
            0,
            23500,
            64,
            6,
            0,
            0,
            0
            };
            array2[52] = new int[10] { 17, 4, 7, 7, 12, 5, 10, 6, 0, 0 };
            array[53] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Reactor,
            ComponentType.Reactor,
            0,
            "Fission Reactor",
            22,
            35000,
            0,
            60,
            105,
            400,
            18,
            0,
            0
            };
            array2[53] = new int[10] { 15, 6, 4, 9, 10, 5, 13, 4, 0, 0 };
            array[54] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Reactor,
            ComponentType.Reactor,
            0,
            "Fusion Reactor",
            15,
            190000,
            0,
            84,
            180,
            520,
            8,
            0,
            0
            };
            array2[54] = new int[10] { 15, 7, 5, 10, 10, 6, 0, 0, 0, 0 };
            array[55] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Reactor,
            ComponentType.Reactor,
            0,
            "Quantum Reactor",
            18,
            240000,
            0,
            120,
            230,
            800,
            18,
            0,
            0
            };
            array2[55] = new int[10] { 15, 6, 6, 11, 10, 4, 17, 2, 7, 2 };
            array[56] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Reactor,
            ComponentType.Reactor,
            0,
            "HyperFusion Reactor",
            16,
            960000,
            0,
            180,
            350,
            975,
            8,
            0,
            0
            };
            array2[56] = new int[10] { 15, 8, 4, 12, 10, 6, 11, 4, 7, 4 };
            array[57] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Reactor,
            ComponentType.Reactor,
            0,
            "NovaCore Reactor",
            20,
            1500000,
            0,
            120,
            240,
            480,
            8,
            0,
            0
            };
            array2[57] = new int[10] { 15, 6, 6, 9, 10, 4, 11, 3, 7, 2 };
            array[58] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.EnergyCollector,
            ComponentType.EnergyCollector,
            0,
            "Energy Collector",
            8,
            30000,
            0,
            24,
            0,
            0,
            0,
            0,
            0
            };
            array2[58] = new int[10] { 16, 2, 9, 7, 14, 4, 12, 5, 0, 0 };
            array[59] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Extractor,
            ComponentType.ExtractorMine,
            0,
            "Mining Engine",
            14,
            25000,
            3,
            3,
            0,
            0,
            0,
            0,
            0
            };
            array2[59] = new int[10] { 10, 5, 11, 5, 0, 0, 0, 0, 0, 0 };
            array[60] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Extractor,
            ComponentType.ExtractorGasExtractor,
            0,
            "Gas Extractor",
            16,
            31000,
            2,
            20,
            0,
            0,
            0,
            0,
            0
            };
            array2[60] = new int[10] { 10, 4, 16, 3, 0, 0, 0, 0, 0, 0 };
            array[61] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Extractor,
            ComponentType.ExtractorLuxury,
            0,
            "Luxury Resource Extractor",
            22,
            50000,
            3,
            3,
            0,
            0,
            0,
            0,
            0
            };
            array2[61] = new int[10] { 10, 3, 11, 3, 0, 0, 0, 0, 0, 0 };
            array[62] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Manufacturer,
            ComponentType.ManufacturerWeaponsPlant,
            0,
            "Weapons Plant",
            35,
            28000,
            2,
            20000,
            0,
            0,
            0,
            0,
            0
            };
            array2[62] = new int[10] { 10, 3, 4, 4, 12, 4, 0, 0, 0, 0 };
            array[63] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Manufacturer,
            ComponentType.ManufacturerEnergyPlant,
            0,
            "Energy Plant",
            35,
            24000,
            2,
            20000,
            0,
            0,
            0,
            0,
            0
            };
            array2[63] = new int[10] { 10, 3, 16, 3, 13, 4, 0, 0, 0, 0 };
            array[64] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Manufacturer,
            ComponentType.ManufacturerHighTechPlant,
            0,
            "HighTech Plant",
            28,
            26000,
            2,
            20000,
            0,
            0,
            0,
            0,
            0
            };
            array2[64] = new int[10] { 10, 3, 9, 6, 0, 0, 0, 0, 0, 0 };
            array[65] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Storage,
            ComponentType.StorageFuel,
            0,
            "Standard Fuel Cell",
            6,
            28000,
            0,
            65,
            0,
            0,
            0,
            0,
            0
            };
            array2[65] = new int[10] { 10, 2, 16, 1, 0, 0, 0, 0, 0, 0 };
            array[66] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Storage,
            ComponentType.StorageFuel,
            0,
            "UltraDense Fuel Cell",
            6,
            650000,
            0,
            100,
            0,
            0,
            0,
            0,
            0
            };
            array2[66] = new int[10] { 10, 3, 16, 2, 0, 0, 0, 0, 0, 0 };
            array[67] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Storage,
            ComponentType.StorageFuel,
            0,
            "Mega-Density Fuel Cell",
            6,
            1060000,
            0,
            140,
            0,
            0,
            0,
            0,
            0
            };
            array2[67] = new int[10] { 10, 3, 16, 2, 0, 0, 0, 0, 0, 0 };
            array[68] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Storage,
            ComponentType.StorageCargo,
            0,
            "Standard Cargo Bay",
            8,
            20000,
            0,
            500,
            0,
            0,
            0,
            0,
            0
            };
            array2[68] = new int[10] { 10, 3, 16, 1, 0, 0, 0, 0, 0, 0 };
            array[69] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Storage,
            ComponentType.StorageCargo,
            0,
            "Massive Cargo Bay",
            8,
            490000,
            0,
            800,
            0,
            0,
            0,
            0,
            0
            };
            array2[69] = new int[10] { 10, 4, 16, 2, 0, 0, 0, 0, 0, 0 };
            array[70] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Storage,
            ComponentType.StorageTroop,
            0,
            "Standard Troop Compartment",
            8,
            20000,
            0,
            100,
            0,
            0,
            0,
            0,
            0
            };
            array2[70] = new int[10] { 10, 4, 16, 2, 0, 0, 0, 0, 0, 0 };
            array[71] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Storage,
            ComponentType.StorageTroop,
            0,
            "Massive Troop Compartment",
            8,
            240000,
            0,
            160,
            0,
            0,
            0,
            0,
            0
            };
            array2[71] = new int[10] { 10, 5, 16, 3, 0, 0, 0, 0, 0, 0 };
            array[72] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Storage,
            ComponentType.StoragePassenger,
            0,
            "Standard Passenger Compartment",
            10,
            15000,
            0,
            1200000,
            0,
            0,
            0,
            0,
            0
            };
            array2[72] = new int[10] { 10, 4, 16, 2, 0, 0, 0, 0, 0, 0 };
            array[73] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Storage,
            ComponentType.StoragePassenger,
            0,
            "Massive Passenger Compartment",
            10,
            384000,
            0,
            2400000,
            0,
            0,
            0,
            0,
            0
            };
            array2[73] = new int[10] { 10, 5, 16, 3, 0, 0, 0, 0, 0, 0 };
            array[74] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Storage,
            ComponentType.StorageDockingBay,
            0,
            "Docking Bay",
            4,
            30000,
            0,
            150,
            0,
            0,
            0,
            0,
            0
            };
            array2[74] = new int[10] { 10, 4, 0, 0, 0, 0, 0, 0, 0, 0 };
            array[75] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Sensor,
            ComponentType.SensorProximityArray,
            0,
            "Basic Proximity Array",
            3,
            40000,
            1,
            48000,
            1,
            0,
            0,
            0,
            0
            };
            array2[75] = new int[10] { 9, 5, 14, 3, 0, 0, 0, 0, 0, 0 };
            array[76] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Sensor,
            ComponentType.SensorProximityArray,
            0,
            "Advanced Proximity Array",
            3,
            340000,
            1,
            54000,
            10,
            0,
            0,
            0,
            0
            };
            array2[76] = new int[10] { 9, 5, 14, 3, 0, 0, 0, 0, 0, 0 };
            array[77] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Sensor,
            ComponentType.SensorResourceProfileSensor,
            0,
            "Resource Profile Sensor",
            2,
            35000,
            1,
            500,
            0,
            0,
            0,
            0,
            0
            };
            array2[77] = new int[10] { 9, 6, 14, 4, 0, 0, 0, 0, 0, 0 };
            array[78] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Sensor,
            ComponentType.SensorLongRange,
            0,
            "Long Range Scanner",
            72,
            56000,
            60,
            450000,
            0,
            0,
            0,
            0,
            0
            };
            array2[78] = new int[10] { 9, 36, 15, 14, 0, 0, 0, 0, 0, 0 };
            array[79] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Sensor,
            ComponentType.SensorLongRange,
            0,
            "Ultra Long Range Scanner",
            98,
            630000,
            110,
            1100000,
            0,
            0,
            0,
            0,
            0
            };
            array2[79] = new int[10] { 9, 55, 15, 20, 0, 0, 0, 0, 0, 0 };
            array[80] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Sensor,
            ComponentType.SensorTraceScanner,
            0,
            "Trace Scanner",
            2,
            390000,
            1,
            500,
            10,
            0,
            0,
            0,
            0
            };
            array2[80] = new int[10] { 9, 4, 14, 3, 0, 0, 0, 0, 0, 0 };
            array[81] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Sensor,
            ComponentType.SensorScannerJammer,
            0,
            "Scanner Jammer",
            2,
            510000,
            1,
            0,
            10,
            0,
            0,
            0,
            0
            };
            array2[81] = new int[10] { 9, 4, 14, 3, 0, 0, 0, 0, 0, 0 };
            array[82] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Sensor,
            ComponentType.SensorStealth,
            0,
            "Stealth Cloak",
            60,
            275000,
            10,
            500,
            0,
            0,
            0,
            0,
            0
            };
            array2[82] = new int[10] { 17, 5, 11, 3, 12, 6, 10, 7, 7, 8 };
            array[83] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Computer,
            ComponentType.ComputerTargetting,
            0,
            "Combat Targetting System",
            1,
            55000,
            1,
            10,
            0,
            0,
            0,
            0,
            0
            };
            array2[83] = new int[10] { 9, 3, 14, 3, 16, 1, 0, 0, 0, 0 };
            array[84] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Computer,
            ComponentType.ComputerCountermeasures,
            0,
            "Countermeasures System",
            1,
            75000,
            1,
            10,
            0,
            0,
            0,
            0,
            0
            };
            array2[84] = new int[10] { 9, 3, 14, 3, 16, 1, 0, 0, 0, 0 };
            array[85] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Computer,
            ComponentType.ComputerCommandCenter,
            0,
            "Command Center",
            2,
            20000,
            2,
            0,
            0,
            0,
            0,
            0,
            0
            };
            array2[85] = new int[10] { 9, 5, 14, 4, 16, 2, 0, 0, 0, 0 };
            array[86] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Computer,
            ComponentType.ComputerCommerceCenter,
            0,
            "Commerce Center",
            3,
            25000,
            2,
            50,
            0,
            0,
            0,
            0,
            0
            };
            array2[86] = new int[10] { 9, 7, 14, 5, 16, 3, 0, 0, 0, 0 };
            array[87] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Computer,
            ComponentType.ComputerCountermeasures,
            0,
            "ShadowGhost ECM",
            1,
            2000000,
            1,
            40,
            0,
            0,
            0,
            0,
            0
            };
            array2[87] = new int[10] { 9, 4, 14, 3, 16, 2, 0, 0, 0, 0 };
            array[88] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Computer,
            ComponentType.ComputerTargetting,
            0,
            "Raptor Targetting System",
            1,
            1900000,
            1,
            40,
            0,
            0,
            0,
            0,
            0
            };
            array2[88] = new int[10] { 9, 3, 14, 4, 16, 2, 0, 0, 0, 0 };
            array[89] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Computer,
            ComponentType.ComputerTargettingFleet,
            0,
            "Fleet Targetting System",
            3,
            55000,
            5,
            25,
            10,
            0,
            0,
            0,
            0
            };
            array2[89] = new int[10] { 9, 5, 14, 4, 16, 3, 0, 0, 0, 0 };
            array[90] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Computer,
            ComponentType.ComputerCountermeasuresFleet,
            0,
            "Fleet Countermeasures System",
            3,
            75000,
            5,
            25,
            10,
            0,
            0,
            0,
            0
            };
            array2[90] = new int[10] { 9, 5, 14, 4, 16, 3, 0, 0, 0, 0 };
            array[91] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Labs,
            ComponentType.LabsWeaponsLab,
            0,
            "Weapons Lab",
            20,
            44000,
            5,
            20000,
            0,
            0,
            0,
            0,
            0
            };
            array2[91] = new int[10] { 16, 5, 14, 6, 9, 7, 0, 0, 0, 0 };
            array[92] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Labs,
            ComponentType.LabsEnergyLab,
            0,
            "Energy Lab",
            20,
            38000,
            5,
            20000,
            0,
            0,
            0,
            0,
            0
            };
            array2[92] = new int[10] { 16, 5, 14, 6, 9, 7, 0, 0, 0, 0 };
            array[93] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Labs,
            ComponentType.LabsHighTechLab,
            0,
            "HighTech Lab",
            20,
            32000,
            5,
            20000,
            0,
            0,
            0,
            0,
            0
            };
            array2[93] = new int[10] { 16, 5, 14, 6, 9, 8, 0, 0, 0, 0 };
            array[94] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Construction,
            ComponentType.ConstructionBuild,
            0,
            "Construction Yard",
            10,
            45000,
            2,
            200,
            100,
            0,
            0,
            0,
            0
            };
            array2[94] = new int[10] { 10, 8, 17, 3, 0, 0, 0, 0, 0, 0 };
            array[95] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Construction,
            ComponentType.DamageControl,
            0,
            "Damage Control Unit",
            4,
            96000,
            1,
            400,
            0,
            0,
            0,
            0,
            0
            };
            array2[95] = new int[10] { 10, 5, 17, 2, 0, 0, 0, 0, 0, 0 };
            array[96] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Construction,
            ComponentType.DamageControl,
            0,
            "S2F4 RepairBot",
            3,
            520000,
            1,
            500,
            5,
            0,
            0,
            0,
            0
            };
            array2[96] = new int[10] { 10, 6, 17, 1, 0, 0, 0, 0, 0, 0 };
            array[97] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Construction,
            ComponentType.DamageControl,
            0,
            "S2F7 RepairBot",
            2,
            890000,
            1,
            650,
            4,
            0,
            0,
            0,
            0
            };
            array2[97] = new int[10] { 10, 5, 17, 2, 0, 0, 0, 0, 0, 0 };
            array[98] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Habitation,
            ComponentType.HabitationLifeSupport,
            0,
            "Life Support",
            1,
            15000,
            1,
            60,
            0,
            0,
            0,
            0,
            0
            };
            array2[98] = new int[10] { 16, 1, 14, 2, 0, 0, 0, 0, 0, 0 };
            array[99] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Habitation,
            ComponentType.HabitationHabModule,
            0,
            "Hab Module",
            2,
            17000,
            1,
            60,
            0,
            0,
            0,
            0,
            0
            };
            array2[99] = new int[10] { 10, 3, 16, 2, 0, 0, 0, 0, 0, 0 };
            array[100] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Habitation,
            ComponentType.HabitationMedicalCenter,
            0,
            "Medical Center",
            4,
            37000,
            3,
            100,
            0,
            0,
            0,
            0,
            0
            };
            array2[100] = new int[10] { 10, 6, 16, 4, 11, 5, 0, 0, 0, 0 };
            array[101] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Habitation,
            ComponentType.HabitationRecreationCenter,
            0,
            "Recreation Center",
            20,
            42000,
            15,
            100,
            0,
            0,
            0,
            0,
            0
            };
            array2[101] = new int[10] { 10, 18, 16, 10, 0, 0, 0, 0, 0, 0 };
            array[102] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Habitation,
            ComponentType.HabitationColonization,
            0,
            "Basic Colonization Module",
            360,
            68000,
            6,
            30000000,
            0,
            0,
            0,
            0,
            0
            };
            array2[102] = new int[10] { 8, 400, 10, 180, 16, 100, 0, 0, 0, 0 };
            array[103] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Habitation,
            ComponentType.HabitationColonization,
            0,
            "Enhanced Colonization Module",
            390,
            195000,
            7,
            60000000,
            0,
            0,
            0,
            0,
            0
            };
            array2[103] = new int[10] { 8, 460, 10, 200, 16, 120, 0, 0, 0, 0 };
            array[104] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Habitation,
            ComponentType.HabitationColonization,
            0,
            "Massive Colonization Module",
            440,
            315000,
            7,
            100000000,
            0,
            0,
            0,
            0,
            0
            };
            array2[104] = new int[10] { 8, 520, 10, 230, 16, 130, 0, 0, 0, 0 };
            array[106] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponIon,
            ComponentType.WeaponIonCannon,
            0,
            "Giant Ion Cannon",
            12,
            99999999,
            0,
            220,
            400,
            0,
            220,
            4,
            8000
            };
            array2[106] = new int[10] { 13, 6, 14, 4, 5, 5, 12, 2, 0, 0 };
            array[107] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponBeam,
            ComponentType.WeaponPhaser,
            0,
            "Phaser Cannon",
            7,
            48000,
            0,
            9,
            200,
            32,
            500,
            0,
            2200
            };
            array2[107] = new int[10] { 1, 3, 4, 6, 14, 4, 0, 0, 0, 0 };
            array[108] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponBeam,
            ComponentType.WeaponRailGun,
            0,
            "Rail Gun",
            7,
            60000,
            0,
            6,
            120,
            6,
            120,
            0,
            1000
            };
            array2[108] = new int[10] { 1, 3, 8, 6, 13, 4, 15, 3, 0, 0 };
            array[109] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponTorpedo,
            ComponentType.WeaponMissile,
            0,
            "Assault Missile",
            14,
            240000,
            0,
            20,
            740,
            18,
            110,
            0,
            4200
            };
            array2[109] = new int[10] { 11, 4, 17, 2, 6, 7, 0, 0, 0, 0 };
            array[110] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.EnergyCollector,
            ComponentType.EnergyToFuel,
            0,
            "Energy To Fuel Converter",
            280,
            30000,
            0,
            50,
            0,
            0,
            0,
            0,
            0
            };
            array2[110] = new int[10] { 10, 74, 16, 48, 9, 28, 12, 37, 7, 42 };
            array[111] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponBeam,
            ComponentType.WeaponPhaser,
            0,
            "Phaser Lance",
            9,
            48000,
            0,
            20,
            300,
            50,
            520,
            0,
            4000
            };
            array2[111] = new int[10] { 1, 3, 4, 7, 14, 5, 0, 0, 0, 0 };
            array[112] = new object[15]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponBeam,
            ComponentType.WeaponRailGun,
            0,
            "Heavy Rail Gun",
            12,
            60000,
            0,
            10,
            180,
            8,
            180,
            0,
            2000,
            1
            };
            array2[112] = new int[10] { 1, 4, 8, 8, 13, 4, 15, 4, 0, 0 };
            array[113] = new object[15]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponBeam,
            ComponentType.WeaponRailGun,
            0,
            "Massive Rail Gun",
            18,
            120000,
            0,
            16,
            280,
            12,
            200,
            0,
            2800,
            2
            };
            array2[113] = new int[10] { 1, 5, 8, 10, 13, 5, 15, 5, 0, 0 };
            array[114] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponGravity,
            ComponentType.WeaponTractorBeam,
            0,
            "Tractor Beam",
            10,
            310000,
            0,
            10,
            320,
            28,
            800,
            5,
            4000
            };
            array2[114] = new int[10] { 13, 5, 15, 3, 6, 4, 12, 3, 0, 0 };
            array[115] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.AssaultPod,
            ComponentType.AssaultPod,
            0,
            "Assault Pod",
            8,
            240000,
            0,
            50,
            140,
            6,
            50,
            20,
            120000
            };
            array2[115] = new int[10] { 10, 6, 16, 3, 17, 1, 11, 2, 0, 0 };
            array[116] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponGravity,
            ComponentType.WeaponGravityBeam,
            0,
            "Graviton Beam",
            20,
            310000,
            0,
            15,
            150,
            40,
            400,
            4,
            7000
            };
            array2[116] = new int[10] { 0, 3, 12, 2, 15, 2, 0, 0, 0, 0 };
            array[117] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponGravity,
            ComponentType.WeaponGravityBeam,
            0,
            "Resonant Graviton Beam",
            30,
            310000,
            0,
            28,
            240,
            60,
            400,
            3,
            7000
            };
            array2[117] = new int[10] { 0, 4, 12, 3, 15, 4, 3, 2, 0, 0 };
            array[118] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponGravity,
            ComponentType.WeaponTractorBeam,
            0,
            "High Power Tractor Beam",
            10,
            310000,
            0,
            16,
            400,
            34,
            400,
            2,
            4000
            };
            array2[118] = new int[10] { 13, 6, 15, 4, 6, 5, 12, 4, 0, 0 };
            array[119] = new object[15]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponGravity,
            ComponentType.WeaponAreaGravity,
            0,
            "Area Graviton Pulse",
            40,
            310000,
            0,
            40,
            360,
            100,
            60,
            240,
            9000,
            80
            };
            array2[119] = new int[10] { 0, 5, 6, 6, 15, 5, 11, 4, 3, 5 };
            array[120] = new object[15]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponGravity,
            ComponentType.WeaponAreaGravity,
            0,
            "Area Transient Singularity",
            50,
            310000,
            0,
            100,
            520,
            200,
            65,
            350,
            12000,
            120
            };
            array2[120] = new int[10] { 0, 8, 6, 9, 15, 8, 11, 5, 3, 10 };
            array[121] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Reactor,
            ComponentType.Reactor,
            0,
            "Basic Space Reactor",
            18,
            35000,
            0,
            46,
            90,
            370,
            18,
            0,
            0
            };
            array2[121] = new int[10] { 10, 5, 4, 7, 13, 3, 0, 0, 0, 0 };
            array[122] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Engine,
            ComponentType.EngineMainThrust,
            0,
            "Ion Thruster",
            6,
            40000,
            0,
            600,
            4,
            520,
            2,
            0,
            0
            };
            array2[122] = new int[10] { 17, 1, 10, 4, 0, 0, 0, 0, 0, 0 };
            array[123] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.Engine,
            ComponentType.EngineVectoring,
            0,
            "Directional Thruster",
            2,
            70000,
            0,
            3,
            1,
            0,
            0,
            0,
            0
            };
            array2[123] = new int[10] { 17, 1, 10, 2, 0, 0, 0, 0, 0, 0 };
            array[124] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Storage,
            ComponentType.StorageFuel,
            0,
            "Small Fuel Cell",
            6,
            28000,
            0,
            55,
            0,
            0,
            0,
            0,
            0
            };
            array2[124] = new int[10] { 10, 2, 16, 1, 0, 0, 0, 0, 0, 0 };
            array[125] = new object[14]
            {
            IndustryType.HighTech,
            ComponentCategoryType.Storage,
            ComponentType.StorageCargo,
            0,
            "Small Cargo Bay",
            8,
            20000,
            0,
            350,
            0,
            0,
            0,
            0,
            0
            };
            array2[125] = new int[10] { 10, 3, 16, 1, 0, 0, 0, 0, 0, 0 };
            array[126] = new object[14]
            {
            IndustryType.Energy,
            ComponentCategoryType.HyperDrive,
            ComponentType.HyperDrive,
            0,
            "Warp Bubble Generator",
            10,
            75000,
            0,
            2000,
            132,
            18,
            0,
            0,
            0
            };
            array2[126] = new int[10] { 17, 4, 4, 7, 10, 5, 0, 0, 0, 0 };
            array[127] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponBeam,
            ComponentType.WeaponBeam,
            0,
            "Pulse Blaster",
            4,
            40000,
            0,
            4,
            150,
            13,
            360,
            1,
            1240
            };
            array2[127] = new int[10] { 1, 2, 4, 3, 14, 3, 0, 0, 0, 0 };
            array[128] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponBeam,
            ComponentType.WeaponRailGun,
            0,
            "Long Range Gun",
            6,
            50000,
            0,
            5,
            100,
            7,
            100,
            0,
            1000
            };
            array2[128] = new int[10] { 1, 3, 8, 5, 13, 3, 15, 3, 0, 0 };
            array[129] = new object[14]
            {
            IndustryType.Weapon,
            ComponentCategoryType.WeaponTorpedo,
            ComponentType.WeaponMissile,
            0,
            "Seeking Missile",
            8,
            120000,
            0,
            5,
            400,
            20,
            80,
            0,
            2700
            };
            array2[129] = new int[10] { 11, 3, 17, 1, 6, 4, 0, 0, 0, 0 };
            for (int i = 0; i < array.Length; i++)
            {
                object[] array3 = (object[])array[i];
                ComponentDefinition componentDefinition = new ComponentDefinition((ComponentType)array3[2], i, (string)array3[4], (int)array3[5], (int)array3[6]);
                if (array3.Length == 15)
                {
                    componentDefinition.Value7 = (int)array3[14];
                }
                int[] array4 = (int[])array2[i];
                for (int j = 0; j < 5; j++)
                {
                    if (array4[j * 2 + 1] != 0)
                    {
                        ComponentResource resource = new ComponentResource(resourceSystem.Resources[array4[j * 2]].ResourceID, (short)array4[j * 2 + 1]);
                        if (componentDefinition.RequiredResources == null)
                        {
                            componentDefinition.RequiredResources = new ComponentResourceList();
                        }
                        componentDefinition.RequiredResources.Add(resource);
                    }
                }
                componentDefinition.ComponentID = i;
                componentDefinition.Value1 = (int)array3[8];
                componentDefinition.Value2 = (int)array3[9];
                componentDefinition.Value3 = (int)array3[10];
                componentDefinition.Value4 = (int)array3[11];
                componentDefinition.Value5 = (int)array3[12];
                componentDefinition.Value6 = (int)array3[13];
                ComponentDefinitionsStatic[i] = componentDefinition;
            }
            ComponentsWeaponBeamOrderedByRange = GenerateOrderedComponentList(ComponentCategoryType.WeaponBeam, 2);
            ComponentsWeaponTorpedoOrderedByRange = GenerateOrderedComponentList(ComponentCategoryType.WeaponTorpedo, 2);
            ComponentsWeaponAreaOrderedByRange = GenerateOrderedComponentList(ComponentType.WeaponAreaDestruction, 2);
            ComponentsWeaponBeamOrderedByPower = GenerateOrderedComponentList(ComponentCategoryType.WeaponBeam, 1);
            ComponentsWeaponTorpedoOrderedByPower = GenerateOrderedComponentList(ComponentCategoryType.WeaponTorpedo, 1);
            ComponentsWeaponAreaOrderedByPower = GenerateOrderedComponentList(ComponentType.WeaponAreaDestruction, 1);
            ComponentsReactorOrderedByEfficiency = GenerateOrderedComponentList(ComponentCategoryType.Reactor, 3, 2);
            ComponentsReactorOrderedByEfficiency.Reverse();
            ComponentsReactorOrderedByPower = GenerateOrderedComponentList(ComponentCategoryType.Reactor, 1);
            ComponentsEngineMainThrustOrderedByPower = GenerateOrderedComponentList(ComponentType.EngineMainThrust, 1);
            ComponentsEngineVectoringOrderedByPower = GenerateOrderedComponentList(ComponentType.EngineVectoring, 1);
            ComponentsEngineMainThrustOrderedByEfficiency = GenerateOrderedComponentList(ComponentType.EngineMainThrust, 1, 2);
            ComponentsEngineVectoringOrderedByEfficiency = GenerateOrderedComponentList(ComponentType.EngineVectoring, 1, 2);
            ComponentsHyperdriveOrderedByPower = GenerateOrderedComponentList(ComponentCategoryType.HyperDrive, 1);
            ComponentsHyperdriveOrderedByEfficiency = GenerateOrderedComponentList(ComponentCategoryType.HyperDrive, 1, 2);
            ComponentsHyperdriveOrderedByJumpInitiation = GenerateOrderedComponentList(ComponentCategoryType.HyperDrive, 3, orderHighestToLowest: false);
        }

        public static ResearchNodeDefinitionList InitializeResearchNodeDefinitions(string applicationStartupPath, string customizationSetName)
        {
            string text = "research.txt";
            string filePath = applicationStartupPath + "\\" + text;
            if (!string.IsNullOrEmpty(customizationSetName))
            {
                string text2 = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\" + text;
                if (File.Exists(text2))
                {
                    filePath = text2;
                }
            }
            RaceList races = LoadRaces(applicationStartupPath, customizationSetName);
            ResearchNodeDefinitionList researchNodeDefinitionList = new ResearchNodeDefinitionList();
            if (!Main._ExpModMain.GetSettings().UseDbFiles)
            {
                researchNodeDefinitionList.LoadFromFile(filePath, races);
            }
            else
            {
                researchNodeDefinitionList.LoadFromFile(Main._FileDB.GetResearchReader(), races, Main._FileDB.GetProjIdCOunt());
            }
            SetResearchCosts(120000, researchNodeDefinitionList);
            return researchNodeDefinitionList;
        }

        public static void WriteResearchNodesToFile(ResearchNodeDefinitionList researchNodes, string applicationStartupPath)
        {
            try
            {
                string text = "research_output.txt";
                string path = applicationStartupPath + "\\" + text;
                using FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                using StreamWriter streamWriter = new StreamWriter(stream);
                for (int i = 0; i < researchNodes.Count; i++)
                {
                    ResearchNodeDefinition researchNodeDefinition = researchNodes[i];
                    string empty = string.Empty;
                    empty += "PROJECT\t\t\t;";
                    empty = empty + researchNodeDefinition.ResearchNodeId + ", ";
                    empty = empty + researchNodeDefinition.Name + ", ";
                    empty = empty + researchNodeDefinition.TechLevel + ", ";
                    empty = empty + researchNodeDefinition.Row + ", ";
                    empty += "\t\t";
                    int num = 0;
                    switch (researchNodeDefinition.Industry)
                    {
                        case IndustryType.Weapon:
                            num = 0;
                            break;
                        case IndustryType.Energy:
                            num = 1;
                            break;
                        case IndustryType.HighTech:
                            num = 2;
                            break;
                    }
                    empty = empty + num + ", ";
                    empty = empty + DetermineIndexOfComponentCategory(researchNodeDefinition.Category) + ", ";
                    empty = empty + researchNodeDefinition.SpecialFunctionCode + ", ";
                    empty = empty + researchNodeDefinition.BaseCostMultiplierOverride.ToString("0.0") + ", ";
                    streamWriter.WriteLine(empty);
                    if (researchNodeDefinition.Components != null && researchNodeDefinition.Components.Count > 0)
                    {
                        empty = "COMPONENTS\t\t;";
                        for (int j = 0; j < researchNodeDefinition.Components.Count; j++)
                        {
                            Component component = researchNodeDefinition.Components[j];
                            if (component != null)
                            {
                                empty += component.ComponentID;
                                empty += ", ";
                            }
                        }
                        empty = empty.Substring(0, empty.Length - 2);
                        streamWriter.WriteLine(empty);
                    }
                    if (researchNodeDefinition.ComponentImprovements != null && researchNodeDefinition.ComponentImprovements.Count > 0)
                    {
                        empty = "COMPONENT IMPROVEMENTS\t;";
                        for (int k = 0; k < researchNodeDefinition.ComponentImprovements.Count; k++)
                        {
                            ComponentImprovement componentImprovement = researchNodeDefinition.ComponentImprovements[k];
                            if (componentImprovement != null)
                            {
                                empty = empty + componentImprovement.ImprovedComponent.ComponentID + ", ";
                                empty = empty + componentImprovement.TechLevel + ", ";
                                empty = empty + componentImprovement.Value1 + ", ";
                                empty = empty + componentImprovement.Value2 + ", ";
                                empty = empty + componentImprovement.Value3 + ", ";
                                empty = empty + componentImprovement.Value4 + ", ";
                                empty = empty + componentImprovement.Value5 + ", ";
                                empty = empty + componentImprovement.Value6 + ", ";
                                empty = empty + componentImprovement.Value7 + ",";
                                empty += "\t\t";
                            }
                        }
                        empty = empty.Substring(0, empty.Length - 2);
                        streamWriter.WriteLine(empty);
                    }
                    if (researchNodeDefinition.Fighters != null && researchNodeDefinition.Fighters.Count > 0)
                    {
                        empty = "FIGHTERS\t\t;";
                        for (int l = 0; l < researchNodeDefinition.Fighters.Count; l++)
                        {
                            FighterSpecification fighterSpecification = researchNodeDefinition.Fighters[l];
                            if (fighterSpecification != null)
                            {
                                empty += fighterSpecification.FighterSpecificationId;
                                empty += ", ";
                            }
                        }
                        empty = empty.Substring(0, empty.Length - 2);
                        streamWriter.WriteLine(empty);
                    }
                    if (researchNodeDefinition.PlanetaryFacility != null)
                    {
                        empty = "FACILITY\t\t;";
                        empty += researchNodeDefinition.PlanetaryFacility.PlanetaryFacilityDefinitionId;
                        streamWriter.WriteLine(empty);
                    }
                    if (researchNodeDefinition.Abilities != null && researchNodeDefinition.Abilities.Count > 0)
                    {
                        empty = "ABILITIES\t\t;";
                        for (int m = 0; m < researchNodeDefinition.Abilities.Count; m++)
                        {
                            ResearchAbility researchAbility = researchNodeDefinition.Abilities[m];
                            if (researchAbility == null)
                            {
                                continue;
                            }
                            empty = empty + researchAbility.Name + ", ";
                            int num2 = 0;
                            int num3 = 0;
                            switch (researchAbility.Type)
                            {
                                case ResearchAbilityType.Boarding:
                                    num2 = 0;
                                    break;
                                case ResearchAbilityType.ColonizeHabitatType:
                                    num2 = 1;
                                    break;
                                case ResearchAbilityType.ConstructionSize:
                                    num2 = 2;
                                    break;
                                case ResearchAbilityType.EnableShipSubRole:
                                    num2 = 3;
                                    if (researchAbility.RelatedObject is BuiltObjectSubRole)
                                    {
                                        switch ((BuiltObjectSubRole)researchAbility.RelatedObject)
                                        {
                                            case BuiltObjectSubRole.Carrier:
                                                num3 = 0;
                                                break;
                                            case BuiltObjectSubRole.ResupplyShip:
                                                num3 = 1;
                                                break;
                                        }
                                    }
                                    break;
                                case ResearchAbilityType.PopulationGrowthRate:
                                    num2 = 4;
                                    break;
                                case ResearchAbilityType.Troop:
                                    num2 = 5;
                                    if (researchAbility.RelatedObject is TroopType)
                                    {
                                        switch ((TroopType)researchAbility.RelatedObject)
                                        {
                                            case TroopType.Infantry:
                                                num3 = 0;
                                                break;
                                            case TroopType.Armored:
                                                num3 = 1;
                                                break;
                                            case TroopType.Artillery:
                                                num3 = 2;
                                                break;
                                            case TroopType.SpecialForces:
                                                num3 = 3;
                                                break;
                                        }
                                    }
                                    break;
                            }
                            empty = empty + num2 + ", ";
                            empty = empty + researchAbility.Level + ", ";
                            empty = empty + researchAbility.Value + ", ";
                            empty = empty + num3 + ",";
                            empty += "\t\t";
                        }
                        empty = empty.Substring(0, empty.Length - 3);
                        streamWriter.WriteLine(empty);
                    }
                    if (researchNodeDefinition.ParentNodes != null && researchNodeDefinition.ParentNodes.Count > 0)
                    {
                        empty = "PARENTS\t\t\t;";
                        for (int n = 0; n < researchNodeDefinition.ParentNodes.Count; n++)
                        {
                            ResearchNodeDefinition researchNodeDefinition2 = researchNodeDefinition.ParentNodes[n];
                            if (researchNodeDefinition2 != null)
                            {
                                empty += researchNodeDefinition2.ResearchNodeId;
                                empty += ", ";
                                bool flag = false;
                                if (researchNodeDefinition.ParentIsRequired.Count > n)
                                {
                                    flag = researchNodeDefinition.ParentIsRequired[n];
                                }
                                empty = ((!flag) ? (empty + "N, ") : (empty + "Y, "));
                            }
                        }
                        empty = empty.Substring(0, empty.Length - 2);
                        streamWriter.WriteLine(empty);
                    }
                    streamWriter.WriteLine();
                }
            }
            catch (Exception)
            {
            }
        }

        public static void InitializeData(string applicationStartupPath, string customizationSetName, out ResourceSystem resourceSystem)
        {
            InitializeData(applicationStartupPath, customizationSetName, out resourceSystem, out var _, out var _, out var _, out var _, out var _, out var _, out var _);
        }

        public static void InitializeData(string applicationStartupPath, string customizationSetName, out ResourceSystem resourceSystem, out PlanetaryFacilityDefinitionList facilityDefinitions, out ComponentDefinition[] componentDefinitions, out FighterSpecificationList fighterSpecifications, out ResearchNodeDefinitionList researchNodeDefinitions, out GovernmentAttributesList governments, out RaceFamilyList raceFamilies, out PlagueList plagues)
        {
            facilityDefinitions = InitializePlanetaryFacilityDefinitions(applicationStartupPath, customizationSetName);
            PlanetaryFacilityDefinitionsStatic = facilityDefinitions.Clone();
            resourceSystem = InitializeResourceDefinitions(applicationStartupPath, customizationSetName);
            ResourceSystemStatic.Initialize(resourceSystem);
            componentDefinitions = InitializeComponentDefinitions(resourceSystem, applicationStartupPath, customizationSetName);
            ComponentDefinition[] array = new ComponentDefinition[componentDefinitions.Length];
            Array.Copy(componentDefinitions, array, componentDefinitions.Length);
            ComponentDefinitionsStatic = array;
            GenerateOrderedComponentLists();
            resourceSystem.Update(ComponentDefinitionsStatic);
            ResourceSystemStatic.Initialize(resourceSystem, ComponentDefinitionsStatic);
            fighterSpecifications = GenerateFighterSpecifications(applicationStartupPath, customizationSetName);
            FighterSpecificationsStatic = fighterSpecifications.Clone();
            plagues = InitializePlagues(applicationStartupPath, customizationSetName);
            PlaguesStatic = plagues.Clone();
            governments = InitializeGovernments(applicationStartupPath, customizationSetName);
            InitializeGovernmentBiases(applicationStartupPath, customizationSetName, ref governments);
            GovernmentsStatic = governments.Clone();
            researchNodeDefinitions = InitializeResearchNodeDefinitions(applicationStartupPath, customizationSetName);
            ResearchNodeDefinitionsStatic = researchNodeDefinitions.Clone();
            raceFamilies = InitializeRaceFamilies(applicationStartupPath, customizationSetName);
            InitializeRaceFamilyBiases(applicationStartupPath, customizationSetName, ref raceFamilies);
            RaceFamiliesStatic = raceFamilies.Clone();
            CopyGalaxyStaticDataToBackup();
        }

        public static void CopyBackupGalaxyStaticDataToStatic()
        {
            ResourceSystemStatic.Initialize(BackupResourceSystemStatic);
            PlanetaryFacilityDefinitionsStatic = BackupPlanetaryFacilityDefinitionsStatic.Clone();
            ComponentDefinition[] array = new ComponentDefinition[BackupComponentDefinitionsStatic.Length];
            Array.Copy(BackupComponentDefinitionsStatic, array, BackupComponentDefinitionsStatic.Length);
            ComponentDefinitionsStatic = array;
            GenerateOrderedComponentLists();
            FighterSpecificationsStatic = BackupFighterSpecificationsStatic.Clone();
            PlaguesStatic = BackupPlaguesStatic.Clone();
            ResearchNodeDefinitionsStatic = BackupResearchNodeDefinitionsStatic.Clone();
            GovernmentsStatic = BackupGovernmentsStatic.Clone();
            RaceFamiliesStatic = BackupRaceFamiliesStatic.Clone();
        }

        private static void CopyGalaxyStaticDataToBackup()
        {
            BackupResourceSystemStatic.Initialize(ResourceSystemStatic);
            BackupPlanetaryFacilityDefinitionsStatic = PlanetaryFacilityDefinitionsStatic.Clone();
            ComponentDefinition[] array = new ComponentDefinition[ComponentDefinitionsStatic.Length];
            Array.Copy(ComponentDefinitionsStatic, array, ComponentDefinitionsStatic.Length);
            BackupComponentDefinitionsStatic = array;
            BackupFighterSpecificationsStatic = FighterSpecificationsStatic.Clone();
            BackupPlaguesStatic = PlaguesStatic.Clone();
            BackupResearchNodeDefinitionsStatic = ResearchNodeDefinitionsStatic.Clone();
            BackupGovernmentsStatic = GovernmentsStatic.Clone();
            BackupRaceFamiliesStatic = RaceFamiliesStatic.Clone();
        }

        public static void AssignGalaxyDataToStatic(ResourceSystem resourceSystem, PlanetaryFacilityDefinitionList facilityDefinitions, ComponentDefinition[] componentDefinitions, FighterSpecificationList fighterSpecifications, ResearchNodeDefinitionList researchNodeDefinitions, GovernmentAttributesList governments, RaceFamilyList raceFamilies, PlagueList plagues)
        {
            CopyGalaxyStaticDataToBackup();
            ResourceSystemStatic.Initialize(resourceSystem);
            PlanetaryFacilityDefinitionsStatic = facilityDefinitions.Clone();
            ComponentDefinition[] array = new ComponentDefinition[componentDefinitions.Length];
            Array.Copy(componentDefinitions, array, componentDefinitions.Length);
            ComponentDefinitionsStatic = array;
            GenerateOrderedComponentLists();
            FighterSpecificationsStatic = fighterSpecifications.Clone();
            PlaguesStatic = plagues.Clone();
            ResearchNodeDefinitionsStatic = researchNodeDefinitions.Clone();
            GovernmentsStatic = governments.Clone();
            RaceFamiliesStatic = raceFamilies.Clone();
        }

        public void AssignGalaxyStaticDataToInstance()
        {
            ResourceSystem.Initialize(ResourceSystemStatic);
            PlanetaryFacilityDefinitions = PlanetaryFacilityDefinitionsStatic.Clone();
            ComponentDefinition[] array = new ComponentDefinition[ComponentDefinitionsStatic.Length];
            Array.Copy(ComponentDefinitionsStatic, array, ComponentDefinitionsStatic.Length);
            ComponentDefinitions = array;
            FighterSpecifications = FighterSpecificationsStatic.Clone();
            Plagues = PlaguesStatic.Clone();
            ResearchNodeDefinitions = ResearchNodeDefinitionsStatic.Clone();
            Governments = GovernmentsStatic.Clone();
            RaceFamilies = RaceFamiliesStatic.Clone();
        }

        static Galaxy()
        {
            FlagShapes = new List<Bitmap>();
            FlagShapesPirates = new List<Bitmap>();
            _CharacterList = new CharacterList();
            DesignSpecifications = new DesignSpecificationList();
            IndexMaxX = 50;
            IndexMaxY = 50;
            SizeX = 20000000;
            SizeY = 20000000;
            IndexSize = 400000;
            SectorMaxX = 10;
            SectorMaxY = 10;
            SectorSizeX = 2000000;
            SectorSizeY = 2000000;
            SectorSize = 2000000;
            HyperJumpThreshhold = 12000;
            BaseHyperJumpAccuracy = 3000.0;
            HyperJumpKickout = 10000;
            ThreatRange = 40000;
            StrikeRangeSquared = 90000.0;
            PatrolOrbitDistance = 400;
            EscortRange = 200;
            MaxSolarSystemSize = 23000;
            MaxMoonOrbitSize = 1600;
            MovementPrecision = 30;
            InvasionDropoffRange = 15;
            MovementDecelerationRangeInvasion = 50;
            MovementDecelerationRange = 150;
            MovementImpulseSpeed = 3;
            UndockRange = 30;
            RefuelRate = 40;
            ImpulseMargin = 5;
            ParentRelativeRange = 700;
            ParentRelativeRangeSquared = 490000;
            EscapeSprintDistance = 300;
            EscapeHyperDistance = 200000;
            ExplosionExpansionRate = 100;
            ExplosionMinimumLifetime = 55;
            ExplosionImageCount = 20;
            ExplosionHabitatImageCount = 120;
            HabitatColonizationThreshhold = 5;
            MiningStationResourceThreshhold = 10;
            HabitatSmallSpacePortPopulationRequirement = 1000000L;
            HabitatMediumSpacePortPopulationRequirement = 500000000L;
            HabitatLargeSpacePortPopulationRequirement = 3000000000L;
            BuildColonyShipPopulationRequirement = 500000000L;
            FreightBaseCharge = 20.0;
            FreightChargePerUnitPerDistance = 1E-05;
            ColonyAnnualResourceConsumptionRate = 1E-08;
            ColonyAnnualLuxuryResourceConsumptionRate = 2E-08;
            TypicalMaximumOrderFulfillmentDistance = 8000000;
            HabitatToEmpireThreshhold = 270000000000L;
            HabitatToEmpireMinimumIntelligence = 69;
            IncidentEvaluationAnnualNeutralizationAmount = 3;
            CivilityRatingAnnualNeutralizationAmount = 3;
            CivilityRatingAnnualRiseAmount = 1.5;
            TroopStrengthAnnualNeutralizationAmount = 1;
            TroopSizeAnnualRegenerationAmount = 50;
            TroopAnnualRecruitmentAmount = 150;
            OrderExpiryYears = 1.5;
            OrderExpiryYearsLuxury = 0.6;
            MinimumDiplomacyTradeProposalIntervalYears = 1.25;
            MinimumLevelForRefuellingPoint = 600;
            RefuelThreshholdPercentage = 0.3;
            MinimumHabitatPopulationAmount = 1000000L;
            MinimumDesignReviewIntervalYears = 0.5;
            MinimumContractSize = 300;
            MiningStationResourceTransportThreshhold = 1000;
            ColonyResourceTransportThreshhold = 300;
            ColonyMinimumResourceReorderAmount = 500;
            MinimumLuxuryResourceReorderAmount = 100;
            MinimumRestrictedResourceReorderAmount = 10;
            MinimumOrderAmount = 60;
            MinimumDistanceBetweenBases = 120;
            DistressSignalResponseMaximumDistance = SectorSize;
            RetirementYears = 20;
            RetrofitYears = 1;
            MaximumConstructionQueueWaitTimeYears = 2.5;
            MaximumEmpireCount = 255;
            MajorColonyStrategicThreshhold = 20000;
            TorpedoWeaponHitRange = 25.0;
            AttackOvermatchFactor = 2.0;
            AttackEvaluationRangeFactor = 20000.0;
            ColonyMaximumTroopStrength = 150000;
            TroopAnnualMaintenance = 1000.0;
            AgentAnnualMaintenance = 1000.0;
            BlockadeEmpireEvaluationValue = -20;
            GovernmentStyleAffinityFactor = 10.0;
            SystemCompetitionColonyFactor = 20;
            SystemCompetitionMiningStationFactor = 4;
            AcceptableWarValueLossesBuiltObject = 0.35;
            AcceptableWarValueLossesColony = 0.2;
            DistressSignalLocationOverlapRangeSquared = 1000000.0;
            DistressSignalDateRange = 90000L;
            AllowableYearsMaintenanceFromCashOnHand = 3.0;
            FleetMaximumCount = 30;
            MouseHoverHabitatProximityRange = 120.0;
            EmpireAgeExpansionRateMinimum = 2.3;
            EmpireAgeExpansionRateMaximum = 2.7;
            PointBlankWeaponsRange = 50;
            EmpireEvaluationTrendingFactor = 300.0;
            IncidentImpactWhenDeclareWar = 40;
            DeclareWarReputationImpact = 1.0;
            TreatyOfferValidYears = 0.2;
            AttackOnPiratesRange = 6000000.0;
            EspionageStealResearchMaxAmount = 80000;
            DestroySilverMistReputationBonus = 1.5;
            TradeBonusAnnualIncrease = 0.1;
            TradeBonusMaximumFreeTrade = 0.2;
            TradeBonusMaximumFreeTradeAmount = 20000.0;
            TradeBonusMaximumMutualDefense = 0.3;
            TradeBonusMaximumMutualDefenseAmount = 30000.0;
            SubjugationTributePercentage = 0.1;
            IndependentTraderFreightRange = 5000000;
            ShipMarkupFactor = 5.0;
            ShipMarkupFactorPirates = 2.5;
            PirateEmpireMaxShips = 20;
            PirateEmpireMaxShipsSuper = 60;
            BuiltObjectDrawResizeFactor = 8.0;
            CreatureDrawResizeFactor = 8.0;
            TradeColonyThreshhold = 30;
            TradeMiningStationThreshhold = 10;
            TradeTerritoryMapThreshhold = 5;
            TradeGalaxyMapThreshhold = 15;
            TradeResearchThreshhold = 25;
            TradeResearchSpecialThreshhold = 50;
            FleetTypicalSize = 15;
            StrikeForceTypicalSize = 4;
            ShipMaintenanceCostPerSizeUnit = 1.0;
            ColonyStrategicResourceConsumptionPerMillionPerYear = 0.33;
            ColonyShipBuildFactor = 10.0;
            ColonyStateSupportCost = 1000.0;
            ColonyRevenueDivisor = 3500000.0;
            RevenueDropoffPopulationThreshholdMin = 20000000000L;
            RevenueDropoffPopulationThreshholdMax = 200000000000L;
            RevenueDropoffRate = 0.5;
            ColonyTaxResistanceThreshhold = 200000000000L;
            ColonyTaxResistanceRate = 1.5;
            ColonyDevelopmentLevelMaximumAnnualChange = 25;
            ColonyAnnualRestrictedResourceConsumptionRate = 1E-08;
            ColonyDevelopmentBaseline = 50;
            ColonyCorruptionPopulationThreshhold = 100000000L;
            MainViewShipHighlightDistance = 100;
            WarWhenStrongerAngerLevel = -80;
            WarWhenEvenAngerLevel = -60;
            WarIncidentLevel = -60;
            SpendingAgentPercentage = 0.05;
            SpendingTroopPercentage = 0.3;
            SpendingShipPercentage = 0.5;
            DesiredForeignColonyStrategicThreshhold = 60;
            DesiredForeignColonyResourceThreshhold = 40;
            WarWearinessMaximum = 40.0;
            SpacePortMinimumDistance = 700000.0;
            ResupplyShipMinimumDistance = 1000000.0;
            ColonyResourceLimit = 20000;
            RaceFamilyAffinityBias = 10.0;
            PlanetDestroyReputationImpact = 20;
            IndependentColonyInvadeReputationImpact = 4.0;
            PirateEmpireAttackDistance = 2000000.0;
            PirateEmpireAttackExpiryDateLength = 300000L;
            FleetAssembleAttackWaitPeriodPerShip = 25000L;
            MaximumMissionRefusals = 1;
            IdealTimeBetweenGifts = 1200000L;
            AdvancedTechBonusFactor = 200.0;
            ColonyBuildSpeedIdealPopulation = 10000000000.0;
            HabitatDamageAnnualRegeneration = 0.02;
            MinimumWarLengthPeriodYears = 0.5;
            ResourceLevelOneQuantity = 4000.0;
            ResourceLevelTwoQuantity = 2000.0;
            ResourceLevelThreeQuantity = 1000.0;
            ResourceLevelFourQuantity = 500.0;
            ResourceLevelFiveQuantity = 300.0;
            ResourceLevelSixQuantity = 150.0;
            ColonyCorruptionFactorDefault = 1.0;
            ResearchRateDefault = 1.0;
            PopulationGrowthRateDefault = 1.0;
            MiningRateDefault = 1.0;
            TargettingFactorDefault = 1.0;
            CountermeasuresFactorDefault = 1.0;
            ColonyShipBuildSpeedRateDefault = 1.0;
            WarWearinessFactorDefault = 1.0;
            ColonyIncomeFactorDefault = 1.0;
            RealSecondsInGalacticYear = 600;
            IntermediateProcessingSpan = new TimeSpan(0, 0, 3);
            PeriodicProcessingSpan = new TimeSpan(0, 0, 10);
            LongProcessingSpan = new TimeSpan(0, 0, 60);
            HugeProcessingSpan = new TimeSpan(0, 0, 240);
            YearLength = RealSecondsInGalacticYear * 1000;
            RndStatic = new Random();
            StartStarDate = 1260000000L;
            ResourceSystemStatic = new ResourceSystem();
            ComponentDefinitionsStatic = new ComponentDefinition[130];
            ResearchNodeDefinitionsStatic = new ResearchNodeDefinitionList();
            PlanetaryFacilityDefinitionsStatic = new PlanetaryFacilityDefinitionList();
            FighterSpecificationsStatic = new FighterSpecificationList();
            GovernmentsStatic = new GovernmentAttributesList();
            RaceFamiliesStatic = new RaceFamilyList();
            PlaguesStatic = new PlagueList();
            BackupResourceSystemStatic = new ResourceSystem();
            BackupComponentDefinitionsStatic = new ComponentDefinition[130];
            BackupResearchNodeDefinitionsStatic = new ResearchNodeDefinitionList();
            BackupPlanetaryFacilityDefinitionsStatic = new PlanetaryFacilityDefinitionList();
            BackupFighterSpecificationsStatic = new FighterSpecificationList();
            BackupGovernmentsStatic = new GovernmentAttributesList();
            BackupRaceFamiliesStatic = new RaceFamilyList();
            BackupPlaguesStatic = new PlagueList();
            DesignSpecificationComponentRuleList items = new DesignSpecificationComponentRuleList
        {
            new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.HyperDrive, 1),
            new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommandCenter, 1)
        };
            DesignSpecificationComponentRuleList items2 = new DesignSpecificationComponentRuleList
        {
            new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommandCenter, 1)
        };
            DesignSpecification designSpecification = new DesignSpecification(BuiltObjectSubRole.SmallSpacePort, mobile: false);
            designSpecification.ComponentRules.AddRange(items2);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommerceCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ConstructionBuild, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.HabitationMedicalCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.HabitationRecreationCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.LabsEnergyLab, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.LabsHighTechLab, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.LabsWeaponsLab, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ManufacturerEnergyPlant, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ManufacturerHighTechPlant, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ManufacturerWeaponsPlant, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.SensorProximityArray, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 15));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 12));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentCategoryType.WeaponTorpedo, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponPointDefense, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonDefense, 1));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.MediumSpacePort, mobile: false);
            designSpecification.ComponentRules.AddRange(items2);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 12));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommerceCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ConstructionBuild, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.HabitationMedicalCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.HabitationRecreationCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.LabsEnergyLab, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.LabsHighTechLab, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.LabsWeaponsLab, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ManufacturerEnergyPlant, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ManufacturerHighTechPlant, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ManufacturerWeaponsPlant, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorTraceScanner, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorLongRange, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.EnergyToFuel, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HyperStop, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 30));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 20));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.ShieldRecharge, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.FighterBay, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 20));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponPointDefense, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonCannon, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonDefense, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponTorpedo, 12));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponAreaDestruction, 1));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.LargeSpacePort, mobile: false);
            designSpecification.ComponentRules.AddRange(items2);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 24));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommerceCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ConstructionBuild, 12));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.HabitationMedicalCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.HabitationRecreationCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.LabsEnergyLab, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.LabsHighTechLab, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.LabsWeaponsLab, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ManufacturerEnergyPlant, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ManufacturerHighTechPlant, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ManufacturerWeaponsPlant, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorLongRange, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorTraceScanner, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.EnergyToFuel, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HyperStop, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 60));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 32));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ShieldRecharge, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.FighterBay, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 30));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponPointDefense, 12));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonCannon, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonDefense, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponTorpedo, 20));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponAreaDestruction, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.WeaponSuperArea, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.WeaponSuperBeam, 1));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.ResortBase, mobile: false);
            designSpecification.ComponentRules.AddRange(items2);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StoragePassenger, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 5));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommerceCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HabitationMedicalCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.HabitationRecreationCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponPointDefense, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentCategoryType.WeaponTorpedo, 4));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.EnergyResearchStation, mobile: false)
            {
                ComponentRules =
            {
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommandCenter, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 2),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 4),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 4),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.LabsEnergyLab, 6),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HabitationMedicalCenter, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HabitationRecreationCenter, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 4),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 4),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentCategoryType.WeaponBeam, 2),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.SensorStealth, 1)
            }
            };
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.WeaponsResearchStation, mobile: false)
            {
                ComponentRules =
            {
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommandCenter, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 2),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 4),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 4),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.LabsWeaponsLab, 6),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HabitationMedicalCenter, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HabitationRecreationCenter, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 4),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 4),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentCategoryType.WeaponBeam, 2),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.SensorStealth, 1)
            }
            };
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.HighTechResearchStation, mobile: false)
            {
                ComponentRules =
            {
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommandCenter, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 2),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 4),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 4),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.LabsHighTechLab, 6),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HabitationMedicalCenter, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HabitationRecreationCenter, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 4),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 4),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentCategoryType.WeaponBeam, 2),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.SensorStealth, 1)
            }
            };
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.MonitoringStation, mobile: false)
            {
                ComponentRules =
            {
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommandCenter, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 2),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 2),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 4),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 3),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommerceCenter, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorLongRange, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 6),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 6),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentCategoryType.WeaponBeam, 6),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.SensorStealth, 1)
            }
            };
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.DefensiveBase, mobile: false)
            {
                ComponentRules =
            {
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommandCenter, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 3),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 4),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 6),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 2),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.SensorTraceScanner, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 30),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 16),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.ShieldRecharge, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.FighterBay, 2),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 20),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponPointDefense, 10),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonCannon, 2),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonDefense, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponTorpedo, 12),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentCategoryType.WeaponArea, 1),
                new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 1)
            }
            };
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.ResupplyShip, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 16));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 20));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ExtractorGasExtractor, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.SensorResourceProfileSensor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorLongRange, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.FighterBay, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 12));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponPointDefense, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.WeaponIonPulse, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonDefense, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponTorpedo, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Armor, 20));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.ColonyShip, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.HabitationColonization, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.SensorResourceProfileSensor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 2));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.PassengerShip, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 7));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StoragePassenger, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 2));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.ConstructionShip, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ConstructionBuild, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ManufacturerEnergyPlant, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ManufacturerHighTechPlant, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ManufacturerWeaponsPlant, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Armor, 2));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.ExplorationShip, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 5));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorResourceProfileSensor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 1));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.SmallFreighter, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 1));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.MediumFreighter, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 5));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 1));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.LargeFreighter, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 5));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 1));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.Escort, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Armor, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonDefense, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.Frigate, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 5));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Armor, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonDefense, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponPointDefense, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.Destroyer, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Armor, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 7));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonDefense, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponPointDefense, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponTorpedo, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageTroop, 1));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.Cruiser, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 5));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Armor, 20));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 5));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponPointDefense, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponTorpedo, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargettingFleet, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasuresFleet, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageTroop, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonDefense, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.FighterBay, 1));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.CapitalShip, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ShieldRecharge, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 12));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Armor, 30));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 12));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponPointDefense, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonCannon, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponTorpedo, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.HyperDeny, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargettingFleet, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasuresFleet, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorTraceScanner, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageTroop, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonDefense, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HabitationMedicalCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.FighterBay, 2));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.TroopTransport, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Armor, 20));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 5));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponPointDefense, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageTroop, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HabitationMedicalCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonDefense, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorScannerJammer, 1));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.Carrier, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Armor, 20));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 12));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 5));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponPointDefense, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.ComputerTargettingFleet, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.ComputerCountermeasuresFleet, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.SensorTraceScanner, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.DamageControl, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponIonDefense, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.FighterBay, 5));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.GasMiningStation, mobile: false);
            designSpecification.ComponentRules.AddRange(items2);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommerceCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 20));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ExtractorGasExtractor, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ExtractorLuxury, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 2));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.MiningStation, mobile: false);
            designSpecification.ComponentRules.AddRange(items2);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommerceCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 20));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ExtractorMine, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ExtractorLuxury, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 2));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.GasMiningShip, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ExtractorGasExtractor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ExtractorLuxury, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Armor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 1));
            DesignSpecifications.Add(designSpecification);
            designSpecification = new DesignSpecification(BuiltObjectSubRole.MiningShip, mobile: true);
            designSpecification.ComponentRules.AddRange(items);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EnergyCollector, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ExtractorMine, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ExtractorLuxury, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Armor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 1));
            DesignSpecifications.Add(designSpecification);
        }

        public static void SetGalaxyPhysicalDimensions(int sectorWidth, int sectorHeight)
        {
            sectorWidth = Math.Max(4, Math.Min(15, sectorWidth));
            sectorHeight = Math.Max(4, Math.Min(15, sectorHeight));
            SectorMaxX = sectorWidth;
            SectorMaxY = sectorHeight;
            SizeX = sectorWidth * SectorSizeX;
            SizeY = sectorHeight * SectorSizeY;
            IndexMaxX = SizeX / IndexSize;
            IndexMaxY = SizeY / IndexSize;
        }

        public static void SetHyperDriveSpeeds(ResourceSystem resourceSystem, double multiplier, int baseTechCost, string applicationStartupPath, string customizationSetName)
        {
            ComponentDefinitionsStatic = InitializeComponentDefinitions(resourceSystem, applicationStartupPath, customizationSetName);
            SetResearchCosts(baseTechCost, ResearchNodeDefinitionsStatic = InitializeResearchNodeDefinitions(applicationStartupPath, customizationSetName));
            for (int i = 0; i < ComponentDefinitionsStatic.Length; i++)
            {
                ComponentDefinition componentDefinition = ComponentDefinitionsStatic[i];
                if (componentDefinition.Category == ComponentCategoryType.HyperDrive)
                {
                    double num = (double)componentDefinition.Value1 * multiplier;
                    componentDefinition.Value1 = (int)num;
                }
            }
            for (int j = 0; j < ResearchNodeDefinitionsStatic.Count; j++)
            {
                ResearchNodeDefinition researchNodeDefinition = ResearchNodeDefinitionsStatic[j];
                if (researchNodeDefinition.ComponentImprovements == null || researchNodeDefinition.ComponentImprovements.Count <= 0)
                {
                    continue;
                }
                for (int k = 0; k < researchNodeDefinition.ComponentImprovements.Count; k++)
                {
                    if (researchNodeDefinition.ComponentImprovements[k].ImprovedComponent.Category == ComponentCategoryType.HyperDrive)
                    {
                        double num2 = (double)researchNodeDefinition.ComponentImprovements[k].Value1 * multiplier;
                        researchNodeDefinition.ComponentImprovements[k].Value1 = (int)num2;
                    }
                }
            }
        }

        public static void SetResearchCosts(int baseTechCost, ResearchNodeDefinitionList researchNodeDefinitions)
        {
            for (int i = 0; i < researchNodeDefinitions.Count; i++)
            {
                ResearchNodeDefinition researchNodeDefinition = researchNodeDefinitions[i];
                double num = 1.0;
                num = ((researchNodeDefinition.TechLevel >= 100) ? 256.0 : Math.Pow(2.0, (double)researchNodeDefinition.TechLevel - 1.0));
                if (researchNodeDefinition.BaseCostMultiplierOverride > 0.0)
                {
                    num = researchNodeDefinition.BaseCostMultiplierOverride;
                }
                researchNodeDefinition.Cost = (float)(num * (double)baseTechCost);
                if (researchNodeDefinition.Components != null && researchNodeDefinition.Components.Count > 0)
                {
                    for (int j = 0; j < researchNodeDefinition.Components.Count; j++)
                    {
                        ComponentDefinitionsStatic[researchNodeDefinition.Components[j].ComponentID].TechLevel = researchNodeDefinition.TechLevel;
                    }
                }
                if (researchNodeDefinition.ComponentImprovements != null && researchNodeDefinition.ComponentImprovements.Count > 0)
                {
                    for (int k = 0; k < researchNodeDefinition.ComponentImprovements.Count; k++)
                    {
                        researchNodeDefinition.ComponentImprovements[k].TechLevel = researchNodeDefinition.TechLevel;
                    }
                }
            }
        }

        public static FighterSpecificationList GenerateFighterSpecifications(string applicationStartupPath, string customizationSetName)
        {
            FighterSpecificationList fighterSpecificationList = new FighterSpecificationList();
            if (!Main._ExpModMain.GetSettings().UseDbFiles)
            {
                string text = "fighters.txt";
                string filePath = applicationStartupPath + "\\" + text;
                if (!string.IsNullOrEmpty(customizationSetName))
                {
                    string text2 = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\" + text;
                    if (File.Exists(text2))
                    {
                        filePath = text2;
                    }
                }
                fighterSpecificationList.LoadFromFile(filePath);
            }
            else
            {
                fighterSpecificationList.LoadFromFile(Main._FileDB.GetFightersReader());
            }
            return fighterSpecificationList;
        }

        public static ComponentList GenerateOrderedComponentList(ComponentCategoryType category, int valueBase, int valueDivisor)
        {
            ComponentList componentList = new ComponentList();
            List<double> list = new List<double>();
            ComponentDefinition[] componentDefinitionsStatic = ComponentDefinitionsStatic;
            foreach (ComponentDefinition componentDefinition in componentDefinitionsStatic)
            {
                if (componentDefinition.Category == category)
                {
                    componentList.Add(new Component(componentDefinition.ComponentID));
                    int num = 0;
                    int num2 = 0;
                    switch (valueBase)
                    {
                        case 1:
                            num = componentDefinition.Value1;
                            break;
                        case 2:
                            num = componentDefinition.Value2;
                            break;
                        case 3:
                            num = componentDefinition.Value3;
                            break;
                        case 4:
                            num = componentDefinition.Value4;
                            break;
                        case 5:
                            num = componentDefinition.Value5;
                            break;
                        case 6:
                            num = componentDefinition.Value6;
                            break;
                    }
                    switch (valueDivisor)
                    {
                        case 1:
                            num2 = componentDefinition.Value1;
                            break;
                        case 2:
                            num2 = componentDefinition.Value2;
                            break;
                        case 3:
                            num2 = componentDefinition.Value3;
                            break;
                        case 4:
                            num2 = componentDefinition.Value4;
                            break;
                        case 5:
                            num2 = componentDefinition.Value5;
                            break;
                        case 6:
                            num2 = componentDefinition.Value6;
                            break;
                    }
                    double item = (double)num / (double)num2;
                    list.Add(item);
                }
            }
            Component[] array = componentList.ToArray();
            double[] keys = list.ToArray();
            Array.Sort(keys, array);
            Array.Reverse(array);
            componentList.Clear();
            componentList.AddRange(array);
            return componentList;
        }

    }
}
