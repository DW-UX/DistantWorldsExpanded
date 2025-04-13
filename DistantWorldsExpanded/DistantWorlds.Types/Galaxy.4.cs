using BaconDistantWorlds;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DistantWorlds.Types
{
    public partial class Galaxy
    {
        public static ComponentList GenerateOrderedComponentList(ComponentType type, int valueBase, int valueDivisor)
        {
            ComponentList componentList = new ComponentList();
            List<double> list = new List<double>();
            ComponentDefinition[] componentDefinitionsStatic = ComponentDefinitionsStatic;
            foreach (ComponentDefinition componentDefinition in componentDefinitionsStatic)
            {
                if (componentDefinition.Type == type)
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

        public static ComponentImprovementList GenerateOrderedComponentImprovementList(ComponentCategoryType category, int valueType)
        {
            ComponentList components = GenerateOrderedComponentList(category, valueType, orderHighestToLowest: true);
            return new ComponentImprovementList(components);
        }

        public static ComponentList GenerateOrderedComponentList(ComponentCategoryType category, int valueType)
        {
            return GenerateOrderedComponentList(category, valueType, orderHighestToLowest: true);
        }

        public static ComponentList GenerateOrderedComponentList(ComponentCategoryType category, int valueType, bool orderHighestToLowest)
        {
            ComponentList componentList = new ComponentList();
            List<int> list = new List<int>();
            ComponentDefinition[] componentDefinitionsStatic = ComponentDefinitionsStatic;
            foreach (ComponentDefinition componentDefinition in componentDefinitionsStatic)
            {
                if (CheckComponentMatchesCategoryStrict(componentDefinition, category))
                {
                    componentList.Add(new Component(componentDefinition.ComponentID));
                    switch (valueType)
                    {
                        case 1:
                            list.Add(componentDefinition.Value1);
                            break;
                        case 2:
                            list.Add(componentDefinition.Value2);
                            break;
                        case 3:
                            list.Add(componentDefinition.Value3);
                            break;
                        case 4:
                            list.Add(componentDefinition.Value4);
                            break;
                        case 5:
                            list.Add(componentDefinition.Value5);
                            break;
                        case 6:
                            list.Add(componentDefinition.Value6);
                            break;
                    }
                }
            }
            Component[] array = componentList.ToArray();
            int[] keys = list.ToArray();
            Array.Sort(keys, array);
            if (orderHighestToLowest)
            {
                Array.Reverse(array);
            }
            componentList.Clear();
            componentList.AddRange(array);
            return componentList;
        }

        public static bool CheckComponentMatchesCategoryStrict(ComponentDefinition component, ComponentCategoryType category)
        {
            return CheckComponentMatchesCategoryStrict(component.Category, component.Type, category);
        }

        public static bool CheckComponentMatchesCategoryStrict(ComponentCategoryType componentCategory, ComponentType componentType, ComponentCategoryType category)
        {
            switch (category)
            {
                case ComponentCategoryType.WeaponTorpedo:
                    switch (componentType)
                    {
                        case ComponentType.WeaponBombard:
                        case ComponentType.WeaponMissile:
                            return false;
                        case ComponentType.WeaponTorpedo:
                            return true;
                    }
                    break;
                case ComponentCategoryType.WeaponBeam:
                    switch (componentType)
                    {
                        case ComponentType.WeaponPhaser:
                        case ComponentType.WeaponRailGun:
                            return false;
                        case ComponentType.WeaponBeam:
                            return true;
                    }
                    break;
                default:
                    if (componentCategory == category)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        public static ComponentList GenerateOrderedComponentList(ComponentType type, int valueType)
        {
            ComponentList componentList = new ComponentList();
            List<int> list = new List<int>();
            ComponentDefinition[] componentDefinitionsStatic = ComponentDefinitionsStatic;
            foreach (ComponentDefinition componentDefinition in componentDefinitionsStatic)
            {
                if (componentDefinition.Type == type)
                {
                    componentList.Add(new Component(componentDefinition.ComponentID));
                    switch (valueType)
                    {
                        case 1:
                            list.Add(componentDefinition.Value1);
                            break;
                        case 2:
                            list.Add(componentDefinition.Value2);
                            break;
                        case 3:
                            list.Add(componentDefinition.Value3);
                            break;
                        case 4:
                            list.Add(componentDefinition.Value4);
                            break;
                        case 5:
                            list.Add(componentDefinition.Value5);
                            break;
                        case 6:
                            list.Add(componentDefinition.Value6);
                            break;
                    }
                }
            }
            Component[] array = componentList.ToArray();
            int[] keys = list.ToArray();
            Array.Sort(keys, array);
            Array.Reverse(array);
            componentList.Clear();
            componentList.AddRange(array);
            return componentList;
        }

        public static float CalculateTroopMaintenanceMultiplier(Race race)
        {
            float result = 1f;
            if (race != null && race.TroopMaintenanceSavings > 0)
            {
                result = (float)(1.0 - (double)race.TroopMaintenanceSavings / 100.0);
            }
            return result;
        }

        public static List<string> LoadColonyNames(string applicationStartupPath, string customizationSetName)
        {
            List<string> list = new List<string>();
            int num = 0;
            string text = applicationStartupPath + "\\colonyNames.txt";
            if (!string.IsNullOrEmpty(customizationSetName) && customizationSetName.ToLower(CultureInfo.InvariantCulture) != "default")
            {
                text = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\colonyNames.txt";
            }
            try
            {
                if (!File.Exists(text))
                {
                    return list;
                }
                using FileStream stream = File.OpenRead(text);
                using StreamReader streamReader = new StreamReader(stream);
                while (!streamReader.EndOfStream)
                {
                    string validFileLine = GetValidFileLine(streamReader);
                    num++;
                    if (string.IsNullOrEmpty(validFileLine))
                    {
                        continue;
                    }
                    string[] array = validFileLine.Replace(" ", "").Split(',');
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(array[i]))
                        {
                            list.Add(array[i]);
                        }
                    }
                }
                return list;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error at line " + num + " reading file " + text);
            }
        }

        public static SubRoleNameSet LoadShipNames(string applicationStartupPath, string customizationSetName)
        {
            int num = 0;
            string text = applicationStartupPath + "\\shipNames.txt";
            if (!string.IsNullOrEmpty(customizationSetName) && customizationSetName.ToLower(CultureInfo.InvariantCulture) != "default")
            {
                text = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\shipNames.txt";
            }
            string[] names = Enum.GetNames(typeof(BuiltObjectSubRole));
            SubRoleNameSet subRoleNameSet = new SubRoleNameSet();
            try
            {
                if (!File.Exists(text))
                {
                    return subRoleNameSet;
                }
                FileStream fileStream = File.OpenRead(text);
                StreamReader streamReader = new StreamReader(fileStream);
                while (!streamReader.EndOfStream)
                {
                    string text2 = GetValidFileLine(streamReader);
                    num++;
                    if (string.IsNullOrEmpty(text2))
                    {
                        continue;
                    }
                    string text3 = string.Empty;
                    int num2 = text2.IndexOf(":");
                    if (num2 > 0)
                    {
                        text3 = text2.Substring(0, num2);
                        text3 = text3.Trim();
                    }
                    if (string.IsNullOrEmpty(text3))
                    {
                        continue;
                    }
                    BuiltObjectSubRole builtObjectSubRole = BuiltObjectSubRole.Undefined;
                    for (int i = 0; i < names.Length; i++)
                    {
                        if (names[i].ToLower(CultureInfo.InvariantCulture) == text3.ToLower(CultureInfo.InvariantCulture))
                        {
                            builtObjectSubRole = (BuiltObjectSubRole)Enum.Parse(typeof(BuiltObjectSubRole), text3, ignoreCase: true);
                            break;
                        }
                    }
                    if (builtObjectSubRole == BuiltObjectSubRole.Undefined)
                    {
                        continue;
                    }
                    if (text2.Length > num2)
                    {
                        text2 = text2.Substring(num2 + 1, text2.Length - (num2 + 1));
                    }
                    if (!string.IsNullOrEmpty(text2) && text2.Length > 0)
                    {
                        string[] array = text2.Split(',');
                        if (array.Length == 1 && string.IsNullOrEmpty(array[0].Trim()))
                        {
                            array = new string[0];
                        }
                        for (int j = 0; j < array.Length; j++)
                        {
                            string text4 = (array[j] = array[j].Trim());
                        }
                        SubRoleNameList subRoleNameList = new SubRoleNameList(builtObjectSubRole);
                        subRoleNameList.Names.AddRange(array);
                        subRoleNameSet.SubRoleNames.Add(subRoleNameList);
                    }
                }
                streamReader.Close();
                fileStream.Close();
                return subRoleNameSet;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error at line " + num + " reading file " + text);
            }
        }

        private static string GetValidFileLine(StreamReader reader)
        {
            string text = string.Empty;
            while (!reader.EndOfStream && (string.IsNullOrEmpty(text) || text.Trim() == string.Empty || text.Trim().Substring(0, 1) == "'"))
            {
                text = reader.ReadLine();
            }
            return text;
        }

        public void LoadAgentNames(string applicationStartupPath, string customizationSetName)
        {
            if (!Main._ExpModMain.GetSettings().UseDbFiles)
            {
                int num = 0;
                string text = applicationStartupPath + "\\characterNames.txt";
                if (!string.IsNullOrEmpty(customizationSetName) && customizationSetName.ToLower(CultureInfo.InvariantCulture) != "default")
                {
                    text = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\characterNames.txt";
                }
                if (!File.Exists(text))
                {
                    text = applicationStartupPath + "\\characterNames.txt";
                }
                try
                {
                    if (!File.Exists(text))
                    {
                        throw new ApplicationException("Missing file: " + text);
                    }
                    FileStream fileStream = File.OpenRead(text);
                    StreamReader streamReader = new StreamReader(fileStream);
                    for (int i = 0; i < RaceFamilies.Count; i++)
                    {
                        string[] item = GetValidFileLine(streamReader).Replace(" ", "").Split(',');
                        string[] item2 = GetValidFileLine(streamReader).Replace(" ", "").Split(',');
                        _AgentFirstNames.Add(item);
                        _AgentLastNames.Add(item2);
                    }
                    streamReader.Close();
                    fileStream.Close();
                }
                catch (ApplicationException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw new ApplicationException("Error at line " + num + " reading file " + text);
                }
            }
            else
            {
                var reader = Main._FileDB.GetAgentNamesReader();
                while (reader.Read())
                {
                    _AgentFirstNames.Add(reader.GetString(reader.GetOrdinal("FirstName")).Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
                    _AgentLastNames.Add(reader.GetString(reader.GetOrdinal("LastName")).Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
                }
            }
        }

        public static List<Bitmap> LoadFlagShapes(string applicationStartupPath, string customizationSetName)
        {
            List<Bitmap> list = new List<Bitmap>();
            string path = applicationStartupPath + "\\images\\ui\\flagshapes\\";
            string path2 = applicationStartupPath + "\\customization\\" + customizationSetName + "\\images\\ui\\flagshapes\\";
            string[] array = new string[0];
            if (Directory.Exists(path2))
            {
                array = Directory.GetFiles(path2, "*.png", SearchOption.TopDirectoryOnly);
            }
            else if (Directory.Exists(path))
            {
                array = Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly);
            }
            Array.Sort(array);
            foreach (string text in array)
            {
                Bitmap bitmap = null;
                if (File.Exists(text))
                {
                    bitmap = new Bitmap(text);
                    list.Add(bitmap);
                }
            }
            return list;
        }

        public static List<Bitmap> LoadFlagShapesPirates(string applicationStartupPath, string customizationSetName)
        {
            List<Bitmap> list = new List<Bitmap>();
            string path = applicationStartupPath + "\\images\\ui\\flagshapes\\pirate\\";
            string path2 = applicationStartupPath + "\\customization\\" + customizationSetName + "\\images\\ui\\flagshapes\\pirate\\";
            string[] array = new string[0];
            if (Directory.Exists(path2))
            {
                array = Directory.GetFiles(path2, "*.png", SearchOption.TopDirectoryOnly);
            }
            else if (Directory.Exists(path))
            {
                array = Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly);
            }
            Array.Sort(array);
            foreach (string text in array)
            {
                Bitmap bitmap = null;
                if (File.Exists(text))
                {
                    bitmap = new Bitmap(text);
                    list.Add(bitmap);
                }
            }
            return list;
        }

        public ContractList DetermineContractsWithSupplier(StellarObject supplier)
        {
            ContractList contractList = new ContractList();
            for (int i = 0; i < Orders.Count; i++)
            {
                Order order = Orders[i];
                if (order == null || order.Contracts == null)
                {
                    continue;
                }
                for (int j = 0; j < order.Contracts.Count; j++)
                {
                    Contract contract = order.Contracts[j];
                    if (contract != null && contract.Supplier == supplier)
                    {
                        contractList.Add(contract);
                    }
                }
            }
            return contractList;
        }

        public static int ResolveTechFocusIndex(ComponentType type)
        {
            int result = 0;
            switch (type)
            {
                case ComponentType.WeaponPhaser:
                    result = 2;
                    break;
                case ComponentType.WeaponRailGun:
                    result = 3;
                    break;
                case ComponentType.WeaponBombard:
                    result = 5;
                    break;
                case ComponentType.WeaponMissile:
                    result = 6;
                    break;
                case ComponentType.Armor:
                    result = 10;
                    break;
                case ComponentType.EngineMainThrust:
                    result = 13;
                    break;
                case ComponentType.EngineVectoring:
                    result = 14;
                    break;
                case ComponentType.DamageControl:
                    result = 18;
                    break;
                case ComponentType.ComputerTargetting:
                    result = 19;
                    break;
                case ComponentType.ComputerCountermeasures:
                    result = 20;
                    break;
                case ComponentType.HabitationMedicalCenter:
                    result = 22;
                    break;
                case ComponentType.HabitationRecreationCenter:
                    result = 23;
                    break;
                case ComponentType.WeaponTractorBeam:
                    result = 24;
                    break;
                case ComponentType.AssaultPod:
                    result = 25;
                    break;
                case ComponentType.WeaponGravityBeam:
                    result = 26;
                    break;
                case ComponentType.WeaponAreaGravity:
                    result = 27;
                    break;
            }
            return result;
        }

        public static int ResolveTechFocusIndex(ComponentCategoryType category)
        {
            int result = 0;
            switch (category)
            {
                case ComponentCategoryType.WeaponBeam:
                    result = 1;
                    break;
                case ComponentCategoryType.WeaponTorpedo:
                    result = 4;
                    break;
                case ComponentCategoryType.WeaponArea:
                    result = 7;
                    break;
                case ComponentCategoryType.WeaponIon:
                    result = 8;
                    break;
                case ComponentCategoryType.Fighter:
                    result = 9;
                    break;
                case ComponentCategoryType.Shields:
                    result = 11;
                    break;
                case ComponentCategoryType.Reactor:
                    result = 12;
                    break;
                case ComponentCategoryType.HyperDrive:
                    result = 15;
                    break;
                case ComponentCategoryType.HyperDisrupt:
                    result = 16;
                    break;
                case ComponentCategoryType.Construction:
                    result = 17;
                    break;
                case ComponentCategoryType.Sensor:
                    result = 21;
                    break;
                case ComponentCategoryType.AssaultPod:
                    result = 25;
                    break;
            }
            return result;
        }

        public static ComponentCategoryType ResolveTechDisallow(int index)
        {
            return index switch
            {
                1 => ComponentCategoryType.WeaponTorpedo,
                2 => ComponentCategoryType.WeaponPointDefense,
                3 => ComponentCategoryType.WeaponArea,
                4 => ComponentCategoryType.WeaponIon,
                5 => ComponentCategoryType.Fighter,
                6 => ComponentCategoryType.Armor,
                7 => ComponentCategoryType.HyperDisrupt,
                8 => ComponentCategoryType.Sensor,
                _ => ComponentCategoryType.Undefined,
            };
        }

        public static bool CheckUsePlanetDestroyerAgainstEmpire(Empire attacker, Empire target)
        {
            bool result = false;
            if (attacker != null && attacker.Policy != null && target != null)
            {
                switch (attacker.Policy.WarAttacksAllowPlanetDestroying)
                {
                    case 0:
                        result = true;
                        break;
                    case 1:
                        {
                            EmpireEvaluation empireEvaluation = attacker.ObtainEmpireEvaluation(target);
                            if (empireEvaluation.OverallAttitude <= -80)
                            {
                                result = true;
                            }
                            break;
                        }
                    case 2:
                        if (target.CivilityRating <= -50.0)
                        {
                            result = true;
                        }
                        break;
                    case 3:
                        result = false;
                        break;
                }
            }
            return result;
        }

        public static bool CheckUseBombardmentAgainstEmpire(Empire attacker, Empire target)
        {
            bool result = false;
            if (attacker != null && attacker.Policy != null && target != null)
            {
                switch (attacker.Policy.WarAttacksAllowColonyBombardment)
                {
                    case 0:
                        result = true;
                        break;
                    case 1:
                        {
                            EmpireEvaluation empireEvaluation = attacker.ObtainEmpireEvaluation(target);
                            if (empireEvaluation.OverallAttitude <= -80)
                            {
                                result = true;
                            }
                            break;
                        }
                    case 2:
                        if (target.CivilityRating <= -50.0)
                        {
                            result = true;
                        }
                        break;
                    case 3:
                        result = false;
                        break;
                }
            }
            return result;
        }

        public EmpireList SortEmpiresByMilitaryPriority(Empire empire, EmpireList targetEmpires)
        {
            double[] array = new double[targetEmpires.Count];
            Empire[] array2 = new Empire[targetEmpires.Count];
            for (int i = 0; i < targetEmpires.Count; i++)
            {
                Empire empire2 = targetEmpires[i];
                if (empire2 != null)
                {
                    double num = empire2.TotalColonyStrategicValue / 1000;
                    EmpireEvaluation empireEvaluation = empire.ObtainEmpireEvaluation(empire2);
                    num *= (double)(empireEvaluation.OverallAttitude * -1);
                    if (empire2 == PlayerEmpire && empire != PlayerEmpire)
                    {
                        num *= 2.0;
                    }
                    array[i] = num;
                    array2[i] = targetEmpires[i];
                }
            }
            Array.Sort(array, array2);
            Array.Reverse(array2);
            EmpireList empireList = new EmpireList();
            empireList.AddRange(array2);
            return empireList;
        }

        public static HabitatType ResolveColonyHabitatTypeByIndexIncludingUndefinedDesertBeforeOcean(int index)
        {
            return index switch
            {
                0 => HabitatType.Undefined,
                1 => HabitatType.Continental,
                2 => HabitatType.MarshySwamp,
                3 => HabitatType.Desert,
                4 => HabitatType.Ocean,
                5 => HabitatType.Ice,
                6 => HabitatType.Volcanic,
                _ => HabitatType.Undefined,
            };
        }

        public static HabitatType ResolveColonyHabitatTypeByIndexIncludingUndefined(int index)
        {
            return index switch
            {
                0 => HabitatType.Undefined,
                1 => HabitatType.Continental,
                2 => HabitatType.MarshySwamp,
                3 => HabitatType.Ocean,
                4 => HabitatType.Desert,
                5 => HabitatType.Ice,
                6 => HabitatType.Volcanic,
                _ => HabitatType.Undefined,
            };
        }

        public static HabitatType ResolveColonyHabitatTypeByIndex(int index)
        {
            return index switch
            {
                0 => HabitatType.Continental,
                1 => HabitatType.MarshySwamp,
                2 => HabitatType.Ocean,
                3 => HabitatType.Desert,
                4 => HabitatType.Ice,
                5 => HabitatType.Volcanic,
                _ => HabitatType.Continental,
            };
        }

        public static HabitatType ResolveColonyHabitatTypeByIndexDesertBeforeOcean(int index)
        {
            return index switch
            {
                0 => HabitatType.Continental,
                1 => HabitatType.MarshySwamp,
                2 => HabitatType.Desert,
                3 => HabitatType.Ocean,
                4 => HabitatType.Ice,
                5 => HabitatType.Volcanic,
                _ => HabitatType.Continental,
            };
        }

        public static int ResolveColonyIndexByHabitatTypeDesertBeforeOcean(HabitatType type)
        {
            return type switch
            {
                HabitatType.Continental => 0,
                HabitatType.MarshySwamp => 1,
                HabitatType.Desert => 2,
                HabitatType.Ocean => 3,
                HabitatType.Ice => 4,
                HabitatType.Volcanic => 5,
                _ => -1,
            };
        }

        public static HabitatType ResolveHabitatTypeByIndexIncludeGasClouds(int index)
        {
            return index switch
            {
                0 => HabitatType.Continental,
                1 => HabitatType.MarshySwamp,
                2 => HabitatType.Ocean,
                3 => HabitatType.Desert,
                4 => HabitatType.Ice,
                5 => HabitatType.Volcanic,
                6 => HabitatType.BarrenRock,
                7 => HabitatType.GasGiant,
                8 => HabitatType.FrozenGasGiant,
                9 => HabitatType.Metal,
                10 => HabitatType.Ammonia,
                11 => HabitatType.Argon,
                12 => HabitatType.CarbonDioxide,
                13 => HabitatType.Chlorine,
                14 => HabitatType.Helium,
                15 => HabitatType.Hydrogen,
                16 => HabitatType.NitrogenOxygen,
                17 => HabitatType.Oxygen,
                _ => HabitatType.Continental,
            };
        }

        public static CreatureType ResolveCreatureTypeByIndex(int index)
        {
            CreatureType result = CreatureType.Undefined;
            switch (index)
            {
                case 1:
                    result = CreatureType.Kaltor;
                    break;
                case 2:
                    result = CreatureType.RockSpaceSlug;
                    break;
                case 3:
                    result = CreatureType.DesertSpaceSlug;
                    break;
                case 4:
                    result = CreatureType.Ardilus;
                    break;
                case 5:
                    result = CreatureType.SilverMist;
                    break;
            }
            return result;
        }

        public static Component ResolveSpecialComponent(int specialComponentCode)
        {
            Component result = null;
            if (specialComponentCode >= 0 && specialComponentCode < ComponentDefinitionsStatic.Length)
            {
                result = new Component(specialComponentCode);
            }
            return result;
        }

        public static PlanetaryFacilityDefinition ResolveRaceWonder(int index)
        {
            return index switch
            {
                1 => PlanetaryFacilityDefinitionsStatic[21],
                2 => PlanetaryFacilityDefinitionsStatic[22],
                3 => PlanetaryFacilityDefinitionsStatic[23],
                4 => PlanetaryFacilityDefinitionsStatic[24],
                _ => null,
            };
        }

        public static int DetermineIndexOfComponentCategory(ComponentCategoryType category)
        {
            return category switch
            {
                ComponentCategoryType.Armor => 0,
                ComponentCategoryType.AssaultPod => 1,
                ComponentCategoryType.Computer => 2,
                ComponentCategoryType.Construction => 3,
                ComponentCategoryType.EnergyCollector => 4,
                ComponentCategoryType.Engine => 5,
                ComponentCategoryType.Extractor => 6,
                ComponentCategoryType.Fighter => 7,
                ComponentCategoryType.Habitation => 8,
                ComponentCategoryType.HyperDisrupt => 9,
                ComponentCategoryType.HyperDrive => 10,
                ComponentCategoryType.Labs => 11,
                ComponentCategoryType.Manufacturer => 12,
                ComponentCategoryType.Reactor => 13,
                ComponentCategoryType.Sensor => 14,
                ComponentCategoryType.ShieldRecharge => 15,
                ComponentCategoryType.Shields => 16,
                ComponentCategoryType.Storage => 17,
                ComponentCategoryType.WeaponArea => 18,
                ComponentCategoryType.WeaponBeam => 19,
                ComponentCategoryType.WeaponGravity => 20,
                ComponentCategoryType.WeaponIon => 21,
                ComponentCategoryType.WeaponPointDefense => 22,
                ComponentCategoryType.WeaponSuperArea => 23,
                ComponentCategoryType.WeaponSuperBeam => 24,
                ComponentCategoryType.WeaponTorpedo => 25,
                ComponentCategoryType.WeaponSuperTorpedo => 26,
                _ => -1,
            };
        }

        public static ComponentCategoryType DetermineComponentCategoryByIndex(int index)
        {
            return index switch
            {
                0 => ComponentCategoryType.Armor,
                1 => ComponentCategoryType.AssaultPod,
                2 => ComponentCategoryType.Computer,
                3 => ComponentCategoryType.Construction,
                4 => ComponentCategoryType.EnergyCollector,
                5 => ComponentCategoryType.Engine,
                6 => ComponentCategoryType.Extractor,
                7 => ComponentCategoryType.Fighter,
                8 => ComponentCategoryType.Habitation,
                9 => ComponentCategoryType.HyperDisrupt,
                10 => ComponentCategoryType.HyperDrive,
                11 => ComponentCategoryType.Labs,
                12 => ComponentCategoryType.Manufacturer,
                13 => ComponentCategoryType.Reactor,
                14 => ComponentCategoryType.Sensor,
                15 => ComponentCategoryType.ShieldRecharge,
                16 => ComponentCategoryType.Shields,
                17 => ComponentCategoryType.Storage,
                18 => ComponentCategoryType.WeaponArea,
                19 => ComponentCategoryType.WeaponBeam,
                20 => ComponentCategoryType.WeaponGravity,
                21 => ComponentCategoryType.WeaponIon,
                22 => ComponentCategoryType.WeaponPointDefense,
                23 => ComponentCategoryType.WeaponSuperArea,
                24 => ComponentCategoryType.WeaponSuperBeam,
                25 => ComponentCategoryType.WeaponTorpedo,
                26 => ComponentCategoryType.WeaponSuperTorpedo,
                _ => ComponentCategoryType.Undefined,
            };
        }

        public static void ResolveTechFocus(int index, out ComponentCategoryType category, out ComponentType type)
        {
            category = ComponentCategoryType.Undefined;
            type = ComponentType.Undefined;
            switch (index)
            {
                case 1:
                    category = ComponentCategoryType.WeaponBeam;
                    break;
                case 2:
                    type = ComponentType.WeaponPhaser;
                    break;
                case 3:
                    type = ComponentType.WeaponRailGun;
                    break;
                case 4:
                    category = ComponentCategoryType.WeaponTorpedo;
                    break;
                case 5:
                    type = ComponentType.WeaponBombard;
                    break;
                case 6:
                    type = ComponentType.WeaponMissile;
                    break;
                case 7:
                    category = ComponentCategoryType.WeaponArea;
                    break;
                case 8:
                    category = ComponentCategoryType.WeaponIon;
                    break;
                case 9:
                    category = ComponentCategoryType.Fighter;
                    break;
                case 10:
                    type = ComponentType.Armor;
                    break;
                case 11:
                    category = ComponentCategoryType.Shields;
                    break;
                case 12:
                    category = ComponentCategoryType.Reactor;
                    break;
                case 13:
                    type = ComponentType.EngineMainThrust;
                    break;
                case 14:
                    type = ComponentType.EngineVectoring;
                    break;
                case 15:
                    category = ComponentCategoryType.HyperDrive;
                    break;
                case 16:
                    category = ComponentCategoryType.HyperDisrupt;
                    break;
                case 17:
                    category = ComponentCategoryType.Construction;
                    break;
                case 18:
                    type = ComponentType.DamageControl;
                    break;
                case 19:
                    type = ComponentType.ComputerTargetting;
                    break;
                case 20:
                    type = ComponentType.ComputerCountermeasures;
                    break;
                case 21:
                    category = ComponentCategoryType.Sensor;
                    break;
                case 22:
                    type = ComponentType.HabitationMedicalCenter;
                    break;
                case 23:
                    type = ComponentType.HabitationRecreationCenter;
                    break;
                case 24:
                    type = ComponentType.WeaponTractorBeam;
                    break;
                case 25:
                    type = ComponentType.AssaultPod;
                    break;
                case 26:
                    type = ComponentType.WeaponGravityBeam;
                    break;
                case 27:
                    type = ComponentType.WeaponAreaGravity;
                    break;
                case 28:
                    type = ComponentType.WeaponSuperBeam;
                    break;
                case 29:
                    type = ComponentType.WeaponSuperArea;
                    break;
                case 30:
                    type = ComponentType.WeaponSuperTorpedo;
                    break;
                case 31:
                    type = ComponentType.WeaponSuperMissile;
                    break;
                case 32:
                    type = ComponentType.WeaponSuperRailGun;
                    break;
                case 33:
                    type = ComponentType.WeaponSuperPhaser;
                    break;
            }
        }

        public static void ResolveTechFocuses(Empire empire, out List<ComponentCategoryType> techFocusCategories, out List<ComponentType> techFocusTypes)
        {
            ResolveTechFocuses(empire.Policy, out techFocusCategories, out techFocusTypes);
        }

        public static void ResolveTechFocuses(EmpirePolicy policy, out List<ComponentCategoryType> techFocusCategories, out List<ComponentType> techFocusTypes)
        {
            techFocusCategories = new List<ComponentCategoryType>();
            techFocusTypes = new List<ComponentType>();
            if (policy != null)
            {
                if (policy.ResearchDesignTechFocus1 != 0)
                {
                    techFocusCategories.Add(policy.ResearchDesignTechFocus1);
                }
                else if (policy.ResearchDesignTechFocusType1 != 0)
                {
                    techFocusTypes.Add(policy.ResearchDesignTechFocusType1);
                }
                if (policy.ResearchDesignTechFocus2 != 0)
                {
                    techFocusCategories.Add(policy.ResearchDesignTechFocus2);
                }
                else if (policy.ResearchDesignTechFocusType2 != 0)
                {
                    techFocusTypes.Add(policy.ResearchDesignTechFocusType2);
                }
                if (policy.ResearchDesignTechFocus3 != 0)
                {
                    techFocusCategories.Add(policy.ResearchDesignTechFocus3);
                }
                else if (policy.ResearchDesignTechFocusType3 != 0)
                {
                    techFocusTypes.Add(policy.ResearchDesignTechFocusType3);
                }
                if (policy.ResearchDesignTechFocus4 != 0)
                {
                    techFocusCategories.Add(policy.ResearchDesignTechFocus4);
                }
                else if (policy.ResearchDesignTechFocusType4 != 0)
                {
                    techFocusTypes.Add(policy.ResearchDesignTechFocusType4);
                }
                if (policy.ResearchDesignTechFocus5 != 0)
                {
                    techFocusCategories.Add(policy.ResearchDesignTechFocus5);
                }
                else if (policy.ResearchDesignTechFocusType5 != 0)
                {
                    techFocusTypes.Add(policy.ResearchDesignTechFocusType5);
                }
                if (policy.ResearchDesignTechFocus6 != 0)
                {
                    techFocusCategories.Add(policy.ResearchDesignTechFocus6);
                }
                else if (policy.ResearchDesignTechFocusType6 != 0)
                {
                    techFocusTypes.Add(policy.ResearchDesignTechFocusType6);
                }
            }
        }

        public static DesignList LoadDesigns(Stream stream, Empire empire, bool markLoadedDesignsAsOptimized, long starDate)
        {
            DesignList designList = new DesignList();
            if (empire != null && stream != null && stream.CanRead)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                DesignList designList2 = (DesignList)binaryFormatter.Deserialize(stream);
                foreach (Design item in designList2)
                {
                    item.Empire = empire;
                }
                {
                    foreach (Design item2 in designList2)
                    {
                        bool flag = true;
                        foreach (Design design in empire.Designs)
                        {
                            if (item2.IsEquivalent(design) && item2.Name == design.Name)
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            if (markLoadedDesignsAsOptimized)
                            {
                                item2.OptimizedDesign = 1;
                            }
                            item2.BuildCount = 0;
                            item2.DateCreated = starDate;
                            empire.Designs.Add(item2);
                            designList.Add(item2);
                        }
                    }
                    return designList;
                }
            }
            return designList;
        }

        public static DesignList LoadOptimizedDesignsForEmpire(Empire empire, string applicationStartupPath, string customizationSetPath, long starDate)
        {
            DesignList designList = new DesignList();
            if (empire != null && empire.DominantRace != null)
            {
                string path = applicationStartupPath + "\\designs\\" + empire.DominantRace.Name + "\\";
                if (!string.IsNullOrEmpty(customizationSetPath))
                {
                    path = customizationSetPath + "\\designs\\" + empire.DominantRace.Name + "\\";
                }
                if (!Directory.Exists(path))
                {
                    path = applicationStartupPath + "\\designs\\" + empire.DominantRace.Name + "\\";
                }
                if (Directory.Exists(path))
                {
                    try
                    {
                        string[] files = Directory.GetFiles(path, "*.dwd", SearchOption.TopDirectoryOnly);
                        foreach (string path2 in files)
                        {
                            if (File.Exists(path2))
                            {
                                try
                                {
                                    using FileStream stream = new FileStream(path2, FileMode.Open, FileAccess.Read);
                                    DesignList items = LoadDesigns(stream, empire, markLoadedDesignsAsOptimized: true, starDate);
                                    designList.AddRange(items);
                                }
                                catch (ApplicationException)
                                {
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                        return designList;
                    }
                    catch (ApplicationException)
                    {
                        return designList;
                    }
                }
            }
            return designList;
        }

        public static RaceList LoadRaces(string applicationStartupPath, string customizationSetName)
        {
            RaceList raceList = new RaceList();
            if (!Main._ExpModMain.GetSettings().UseDbFiles)
            {

                List<Task<Race>> taskList = new List<Task<Race>>();
                string text = string.Empty;
                string path = applicationStartupPath + "\\races\\";
                if (!string.IsNullOrEmpty(customizationSetName) && customizationSetName.ToLower(CultureInfo.InvariantCulture) != "default")
                {
                    path = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\races\\";
                }
                if (!Directory.Exists(path))
                {
                    path = applicationStartupPath + "\\races\\";
                }
                try
                {
                    string[] files = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < files.Length; i++)
                    {
                        text = files[i];
                        if (File.Exists(text))
                        {
                            string localText = text;
                            //Race item = Race.LoadFromFile(text);
                            //raceList.Add(item);
                            taskList.Add(Task.Run(() => Race.LoadFromFile(localText)));
                        }
                    }
                    Task.WaitAll(taskList.ToArray());
                    foreach (var item in taskList)
                    {
                        if (item.Result != null)
                        { raceList.Add(item.Result); }
                    }
                }
                catch (ApplicationException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw new ApplicationException("Error reading txt file of some race");
                }
            }
            else
            {
                raceList = Race.LoadFromFile(Main._FileDB.GetRaceReader());
            }
            LoadRaceBiases(applicationStartupPath, customizationSetName, raceList);
            return raceList;
        }

        public static void SetRaceStartupCharacters(string applicationStartupPath, string customizationSetName, RaceList races)
        {
            for (int i = 0; i < races.Count; i++)
            {
                races[i].AvailableCharacters = LoadCharacters(applicationStartupPath, customizationSetName, races[i], races);
            }
        }

        public static CharacterList LoadCharacters(string applicationStartupPath, string customizationSetName, Race race, RaceList allRaces)
        {
            return LoadCharacters(applicationStartupPath, customizationSetName, race.Name, race, allRaces);
        }

        public static CharacterList LoadCharacters(string applicationStartupPath, string customizationSetName, string filename, Race race, RaceList allRaces)
        {
            string text = applicationStartupPath + "\\characters\\" + filename + ".txt";
            if (!string.IsNullOrEmpty(customizationSetName) && customizationSetName.ToLower(CultureInfo.InvariantCulture) != "default")
            {
                text = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\characters\\" + filename + ".txt";
            }
            if (!File.Exists(text))
            {
                text = applicationStartupPath + "\\characters\\" + filename + ".txt";
            }
            return LoadCharactersCompleteFilePath(text, race, allRaces);
        }

        public static CharacterList LoadCharactersCompleteFilePath(string filePath, Race race, RaceList allRaces)
        {
            CharacterList characterList = new CharacterList();
            string value = ",";
            int num = 0;
            try
            {
                if (File.Exists(filePath))
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using StreamReader streamReader = new StreamReader(stream);
                        CharacterList characterList2 = new CharacterList();
                        while (!streamReader.EndOfStream)
                        {
                            num++;
                            int appearanceOrder = 0;
                            string empty = string.Empty;
                            CharacterRole role = CharacterRole.Undefined;
                            string empty2 = string.Empty;
                            string empty3 = string.Empty;
                            CharacterSkillType characterSkillType = CharacterSkillType.Undefined;
                            CharacterSkillType characterSkillType2 = CharacterSkillType.Undefined;
                            CharacterSkillType characterSkillType3 = CharacterSkillType.Undefined;
                            CharacterSkillType characterSkillType4 = CharacterSkillType.Undefined;
                            int num2 = 0;
                            int num3 = 0;
                            int num4 = 0;
                            int num5 = 0;
                            CharacterTraitType characterTraitType = CharacterTraitType.Undefined;
                            CharacterTraitType characterTraitType2 = CharacterTraitType.Undefined;
                            CharacterTraitType characterTraitType3 = CharacterTraitType.Undefined;
                            string text = streamReader.ReadLine();
                            if (!string.IsNullOrEmpty(text) && text.Trim() != string.Empty && text.Trim().Substring(0, 1) != "'")
                            {
                                int num6 = 0;
                                bool flag = false;
                                bool flag2 = false;
                                int num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Appearance Order at line " + num + " of file " + filePath);
                                }
                                string text2 = text.Substring(num6, num7 - num6);
                                text2 = text2.Trim();
                                if (text2 == "?")
                                {
                                    flag = true;
                                }
                                else if (text2 == "-")
                                {
                                    flag2 = true;
                                }
                                else
                                {
                                    appearanceOrder = int.Parse(text2);
                                }
                                num6 = num7 + 1;
                                num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Name at line " + num + " of file " + filePath);
                                }
                                string text3 = text.Substring(num6, num7 - num6);
                                text3 = text3.Trim();
                                empty = text3;
                                num6 = num7 + 1;
                                num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Role at line " + num + " of file " + filePath);
                                }
                                string text4 = text.Substring(num6, num7 - num6);
                                text4 = text4.Trim();
                                byte b = (byte)int.Parse(text4);
                                if (Enum.IsDefined(typeof(CharacterRole), b))
                                {
                                    role = (CharacterRole)b;
                                }
                                num6 = num7 + 1;
                                num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Picture Filename at line " + num + " of file " + filePath);
                                }
                                string text5 = text.Substring(num6, num7 - num6);
                                text5 = text5.Trim();
                                empty2 = text5;
                                num6 = num7 + 1;
                                num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Race Name at line " + num + " of file " + filePath);
                                }
                                string text6 = text.Substring(num6, num7 - num6);
                                text6 = text6.Trim();
                                empty3 = text6;
                                num6 = num7 + 1;
                                List<CharacterSkillType> list = new List<CharacterSkillType>();
                                num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Skill Type 1 at line " + num + " of file " + filePath);
                                }
                                string text7 = text.Substring(num6, num7 - num6);
                                text7 = text7.Trim();
                                if (text7 == "?")
                                {
                                    characterSkillType = SelectRandomSkillForRole(role, list);
                                    if (characterSkillType != 0)
                                    {
                                        list.Add(characterSkillType);
                                    }
                                }
                                else
                                {
                                    byte b2 = (byte)int.Parse(text7);
                                    if (Enum.IsDefined(typeof(CharacterSkillType), b2))
                                    {
                                        characterSkillType = (CharacterSkillType)b2;
                                        list.Add(characterSkillType);
                                    }
                                }
                                num6 = num7 + 1;
                                num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Skill Level 1 at line " + num + " of file " + filePath);
                                }
                                string text8 = text.Substring(num6, num7 - num6);
                                text8 = text8.Trim();
                                num2 = ((!(text8 == "?")) ? int.Parse(text8) : RndStatic.Next(1, 21));
                                num2 = Math.Max(-100, Math.Min(100, num2));
                                num6 = num7 + 1;
                                num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Skill Type 2 at line " + num + " of file " + filePath);
                                }
                                string text9 = text.Substring(num6, num7 - num6);
                                text9 = text9.Trim();
                                if (text9 == "?")
                                {
                                    characterSkillType2 = SelectRandomSkillForRole(role, list);
                                    if (characterSkillType2 != 0)
                                    {
                                        list.Add(characterSkillType2);
                                    }
                                }
                                else
                                {
                                    byte b3 = (byte)int.Parse(text9);
                                    if (Enum.IsDefined(typeof(CharacterSkillType), b3))
                                    {
                                        characterSkillType2 = (CharacterSkillType)b3;
                                        list.Add(characterSkillType2);
                                    }
                                }
                                num6 = num7 + 1;
                                num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Skill Level 2 at line " + num + " of file " + filePath);
                                }
                                string text10 = text.Substring(num6, num7 - num6);
                                text10 = text10.Trim();
                                num3 = ((!(text10 == "?")) ? int.Parse(text10) : RndStatic.Next(1, 21));
                                num3 = Math.Max(-100, Math.Min(100, num3));
                                num6 = num7 + 1;
                                num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Skill Type 3 at line " + num + " of file " + filePath);
                                }
                                string text11 = text.Substring(num6, num7 - num6);
                                text11 = text11.Trim();
                                if (text11 == "?")
                                {
                                    characterSkillType3 = SelectRandomSkillForRole(role, list);
                                    if (characterSkillType3 != 0)
                                    {
                                        list.Add(characterSkillType3);
                                    }
                                }
                                else
                                {
                                    byte b4 = (byte)int.Parse(text11);
                                    if (Enum.IsDefined(typeof(CharacterSkillType), b4))
                                    {
                                        characterSkillType3 = (CharacterSkillType)b4;
                                        list.Add(characterSkillType3);
                                    }
                                }
                                num6 = num7 + 1;
                                num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Skill Level 3 at line " + num + " of file " + filePath);
                                }
                                string text12 = text.Substring(num6, num7 - num6);
                                text12 = text12.Trim();
                                num4 = ((!(text12 == "?")) ? int.Parse(text12) : RndStatic.Next(1, 21));
                                num4 = Math.Max(-100, Math.Min(100, num4));
                                num6 = num7 + 1;
                                num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Skill Type 4 at line " + num + " of file " + filePath);
                                }
                                string text13 = text.Substring(num6, num7 - num6);
                                text13 = text13.Trim();
                                if (text13 == "?")
                                {
                                    characterSkillType4 = SelectRandomSkillForRole(role, list);
                                    if (characterSkillType4 != 0)
                                    {
                                        list.Add(characterSkillType4);
                                    }
                                }
                                else
                                {
                                    byte b5 = (byte)int.Parse(text13);
                                    if (Enum.IsDefined(typeof(CharacterSkillType), b5))
                                    {
                                        characterSkillType4 = (CharacterSkillType)b5;
                                        list.Add(characterSkillType4);
                                    }
                                }
                                num6 = num7 + 1;
                                num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Skill Level 4 at line " + num + " of file " + filePath);
                                }
                                string text14 = text.Substring(num6, num7 - num6);
                                text14 = text14.Trim();
                                num5 = ((!(text14 == "?")) ? int.Parse(text14) : RndStatic.Next(1, 21));
                                num5 = Math.Max(-100, Math.Min(100, num5));
                                num6 = num7 + 1;
                                List<CharacterTraitType> list2 = new List<CharacterTraitType>();
                                num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Trait Type 1 at line " + num + " of file " + filePath);
                                }
                                string text15 = text.Substring(num6, num7 - num6);
                                text15 = text15.Trim();
                                if (text15 == "?")
                                {
                                    characterTraitType = SelectRandomTraitForRole(role, list2);
                                    if (characterTraitType != 0)
                                    {
                                        list2.Add(characterTraitType);
                                    }
                                }
                                else
                                {
                                    byte b6 = (byte)int.Parse(text15);
                                    if (Enum.IsDefined(typeof(CharacterTraitType), b6))
                                    {
                                        characterTraitType = (CharacterTraitType)b6;
                                        list2.Add(characterTraitType);
                                    }
                                }
                                num6 = num7 + 1;
                                num7 = text.IndexOf(value, num6);
                                if (num7 < 0)
                                {
                                    throw new ApplicationException("Could not read Trait Type 2 at line " + num + " of file " + filePath);
                                }
                                string text16 = text.Substring(num6, num7 - num6);
                                text16 = text16.Trim();
                                if (text16 == "?")
                                {
                                    characterTraitType2 = SelectRandomTraitForRole(role, list2);
                                    if (characterTraitType2 != 0)
                                    {
                                        list2.Add(characterTraitType2);
                                    }
                                }
                                else
                                {
                                    byte b7 = (byte)int.Parse(text16);
                                    if (Enum.IsDefined(typeof(CharacterTraitType), b7))
                                    {
                                        characterTraitType2 = (CharacterTraitType)b7;
                                        list2.Add(characterTraitType2);
                                    }
                                }
                                num6 = num7 + 1;
                                string text17 = text.Substring(num6);
                                text17 = text17.Trim();
                                if (text17 == "?")
                                {
                                    characterTraitType3 = SelectRandomTraitForRole(role, list2);
                                    if (characterTraitType3 != 0)
                                    {
                                        list2.Add(characterTraitType3);
                                    }
                                }
                                else
                                {
                                    byte b8 = (byte)int.Parse(text17);
                                    if (Enum.IsDefined(typeof(CharacterTraitType), b8))
                                    {
                                        characterTraitType3 = (CharacterTraitType)b8;
                                        list2.Add(characterTraitType3);
                                    }
                                }
                                num6 = num7 + 1;
                                Race race2 = race;
                                if (!string.IsNullOrEmpty(empty3) && allRaces != null)
                                {
                                    race2 = allRaces[empty3];
                                    if (race2 == null)
                                    {
                                        race2 = race;
                                    }
                                }
                                if (flag2)
                                {
                                    appearanceOrder = int.MinValue;
                                }
                                Character character = new Character(empty, role, empty2, race2, null, null, appearanceOrder);
                                if (flag)
                                {
                                    characterList2.Add(character);
                                }
                                if (characterSkillType != 0)
                                {
                                    character.AddSkill(characterSkillType, num2, null);
                                }
                                if (characterSkillType2 != 0)
                                {
                                    character.AddSkill(characterSkillType2, num3, null);
                                }
                                if (characterSkillType3 != 0)
                                {
                                    character.AddSkill(characterSkillType3, num4, null);
                                }
                                if (characterSkillType4 != 0)
                                {
                                    character.AddSkill(characterSkillType4, num5, null);
                                }
                                if (characterTraitType != 0)
                                {
                                    character.AddTrait(characterTraitType, starting: true, null);
                                }
                                if (characterTraitType2 != 0)
                                {
                                    character.AddTrait(characterTraitType2, starting: true, null);
                                }
                                if (characterTraitType3 != 0)
                                {
                                    character.AddTrait(characterTraitType3, starting: true, null);
                                }
                                characterList.Add(character);
                            }
                        }
                        if (characterList2.Count <= 0)
                        {
                            return characterList;
                        }
                        Random random = new Random();
                        int num8 = characterList.GetHighestAppearanceOrder();
                        if (num8 < 1)
                        {
                            num8 = characterList.Count;
                        }
                        else if (num8 < characterList.Count / 3)
                        {
                            num8 = characterList.Count;
                        }
                        for (int i = 0; i < characterList2.Count; i++)
                        {
                            Character character2 = characterList2[i];
                            if (character2 != null)
                            {
                                int num9 = 1;
                                int maxValue = Math.Max(num9 + 1, num8 + 2);
                                character2.AppearanceOrder = random.Next(num9, maxValue);
                            }
                        }
                        return characterList;
                    }
                }
                return characterList;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error at line " + num + " reading file " + filePath);
            }
        }

        public static CharacterTraitType SelectRandomTraitForRole(CharacterRole role, List<CharacterTraitType> traitsToExclude)
        {
            List<CharacterTraitType> list = Character.DetermineValidTraitsForRole(role, includeStartingTraits: true, includeHighlyNegativeTraits: false);
            if (list.Count > 0)
            {
                for (int i = 0; i < traitsToExclude.Count; i++)
                {
                    list.Remove(traitsToExclude[i]);
                }
                if (list.Count > 0)
                {
                    int index = RndStatic.Next(0, list.Count);
                    return list[index];
                }
            }
            return CharacterTraitType.Undefined;
        }

        public static CharacterSkillType SelectRandomSkillForRole(CharacterRole role, List<CharacterSkillType> skillsToExclude)
        {
            List<CharacterSkillType> list = Character.DetermineValidSkillsForRole(role);
            if (list.Count > 0)
            {
                for (int i = 0; i < skillsToExclude.Count; i++)
                {
                    list.Remove(skillsToExclude[i]);
                }
                if (list.Count > 0)
                {
                    int index = RndStatic.Next(0, list.Count);
                    return list[index];
                }
            }
            return CharacterSkillType.Undefined;
        }

        public EmpirePolicy LoadEmpirePolicy(Race race, bool isPirate)
        {
            return LoadEmpirePolicy(ApplicationStartupPath, CustomizationSetPath, race.Name, isPirate);
        }

        public EmpirePolicy LoadEmpirePolicy(string name, bool isPirate)
        {
            return LoadEmpirePolicy(ApplicationStartupPath, CustomizationSetPath, name, isPirate);
        }

        public static EmpirePolicy LoadEmpirePolicy(string applicationPath, string customPath, string name, bool isPirate)
        {
            EmpirePolicy empirePolicy = new EmpirePolicy();
            try
            {
                if (!Main._ExpModMain.GetSettings().UseDbFiles)
                {
                    string text = applicationPath + "\\Policy\\";
                    if (isPirate)
                    {
                        text += "pirate\\";
                    }
                    string text2 = text;
                    string text3 = string.Empty;
                    if (!string.IsNullOrEmpty(customPath))
                    {
                        text3 = customPath + "Policy\\";
                        if (isPirate)
                        {
                            text3 += "pirate\\";
                        }
                    }
                    string text4 = name + ".txt";
                    if (!string.IsNullOrEmpty(text3) && File.Exists(text3 + text4))
                    {
                        empirePolicy.LoadFromFile(text3 + text4);
                        return empirePolicy;
                    }
                    if (File.Exists(text2 + text4))
                    {
                        empirePolicy.LoadFromFile(text2 + text4);
                        return empirePolicy;
                    }
                }
                else
                {
                    empirePolicy.LoadFromFile(Main._FileDB.GetEmpirePolicyReader(name), name);
                }
                return empirePolicy;
            }
            catch (Exception)
            {
                return empirePolicy;
            }
        }

        public Habitat GetHabitat(int index)
        {
            if (index >= 0)
            {
                return Habitats[index];
            }
            return null;
        }

        public int SetHabitat(Habitat habitat)
        {
            if (habitat == null)
            {
                return -1;
            }
            return Habitats.IndexOf(habitat);
        }

        public BuiltObject GetBuiltObject(int index)
        {
            if (index >= 0)
            {
                return BuiltObjects[index];
            }
            return null;
        }

        public int SetBuiltObject(BuiltObject builtObject)
        {
            if (builtObject == null)
            {
                return -1;
            }
            return BuiltObjects.IndexOf(builtObject);
        }

        public static int SelectCharacterLandscapeImageIndex(Character character)
        {
            if (character != null && character.Location != null && character.Location is Habitat)
            {
                Habitat habitat = (Habitat)character.Location;
                return habitat.LandscapePictureRef;
            }
            return -1;
        }

        public static int ResolveGovernmentId(string governmentName, Race race)
        {
            int result = -1;
            if (governmentName == "(" + TextResolver.GetText("Random") + ")")
            {
                result = ((race.PreferredStartingGovernmentId < 0) ? Empire.SelectSuitableGovernment(race, -1, Empire.ResolveDefaultAllowableGovernmentTypes(race)) : race.PreferredStartingGovernmentId);
            }
            else
            {
                GovernmentAttributes byName = GovernmentsStatic.GetByName(governmentName);
                if (byName != null)
                {
                    result = byName.GovernmentId;
                }
            }
            return result;
        }

        public static double ResolveTechBonusFactor(Empire empire, Galaxy galaxy, BuiltObject builtObject)
        {
            double result = 1.0;
            int num = 0;
            double num2 = 0.0;
            for (int i = 0; i < builtObject.Components.Count; i++)
            {
                int minTechPoints = ResearchSystem.GetMinTechPoints(builtObject.Components[i]);
                int num3 = galaxy.BaseTechCost;
                if (empire != null && empire.Research != null && empire.Research.CheckComponentResearched(builtObject.Components[i]))
                {
                    num3 = minTechPoints;
                }
                double val = (double)minTechPoints / (double)num3;
                val = Math.Max(1.0, val);
                val -= 1.0;
                if (val > 0.0)
                {
                    num2 += (double)builtObject.Components[i].Size * val;
                    num += builtObject.Components[i].Size;
                }
            }
            if (num2 > 0.0)
            {
                result = 1.0 + num2 / (double)num;
            }
            return result;
        }

        public static double ResolveBuildSpeed(Empire buildingEmpire, Galaxy galaxy, BuiltObject builtObject)
        {
            ComponentCategoryType researchCategory = ComponentCategoryType.Undefined;
            return ResolveBuildSpeed(buildingEmpire, galaxy, builtObject, out researchCategory);
        }

        public static double ResolveBuildSpeed(Empire buildingEmpire, Galaxy galaxy, BuiltObject builtObject, bool considerAllComponents)
        {
            ComponentCategoryType researchCategory = ComponentCategoryType.Undefined;
            return ResolveBuildSpeed(buildingEmpire, galaxy, builtObject, considerAllComponents, out researchCategory);
        }

        public static double ResolveBuildSpeed(Empire buildingEmpire, Galaxy galaxy, BuiltObject builtObject, out ComponentCategoryType researchCategory)
        {
            return ResolveBuildSpeed(buildingEmpire, galaxy, builtObject, considerAllComponents: true, out researchCategory);
        }

        public static double ResolveBuildSpeed(Empire buildingEmpire, Galaxy galaxy, BuiltObject builtObject, bool considerAllComponents, out ComponentCategoryType researchCategory)
        {
            double result = 1.0;
            int num = 0;
            double num2 = 0.0;
            double num3 = 0.0;
            ComponentCategoryType componentCategoryType = ComponentCategoryType.Undefined;
            for (int i = 0; i < builtObject.Components.Count; i++)
            {
                BuiltObjectComponent builtObjectComponent = builtObject.Components[i];
                if (!considerAllComponents && builtObjectComponent.Status == ComponentStatus.Normal)
                {
                    continue;
                }
                int minTechPoints = ResearchSystem.GetMinTechPoints(builtObjectComponent);
                int num4 = (int)((double)galaxy.BaseTechCost * 0.5);
                if (buildingEmpire != null && buildingEmpire.Research != null && buildingEmpire.Research.CheckComponentResearched(builtObjectComponent))
                {
                    num4 = minTechPoints;
                }
                double val = ((double)minTechPoints + 1.0) / ((double)num4 + 1.0);
                val = Math.Max(1.0, val);
                val -= 1.0;
                val = Math.Min(val, 20.0);
                if (val > 0.0)
                {
                    num2 += (double)builtObjectComponent.Size * val;
                    num += builtObjectComponent.Size;
                    if (val > num3)
                    {
                        num3 = val;
                        componentCategoryType = builtObjectComponent.Category;
                    }
                }
            }
            if (num2 > 0.0)
            {
                result = 1.0 + num2 / (double)num;
            }
            researchCategory = componentCategoryType;
            return result;
        }

        public static Bitmap CreateBitmapSafely(int requestedWidth, int requestedHeight, PixelFormat pixelFormat)
        {
            Bitmap result = null;
            int num = 0;
            bool flag = false;
            while (!flag && num < 10)
            {
                try
                {
                    result = new Bitmap(requestedWidth, requestedHeight, pixelFormat);
                    flag = true;
                }
                catch
                {
                    requestedWidth /= 2;
                    requestedHeight /= 2;
                    requestedWidth = Math.Max(1, requestedWidth);
                    requestedHeight = Math.Max(1, requestedHeight);
                }
                num++;
            }
            return result;
        }

        public Bitmap GenerateNebulae(bool generateImage, Bitmap galaxyBackground, Bitmap[] cloudImages, out GalaxyLocationList locations)
        {
            List<Color> list = new List<Color>();
            list.Add(Color.FromArgb(17, 13, 20));
            list.Add(Color.FromArgb(14, 13, 24));
            GalaxyNebulaeGenerator galaxyNebulaeGenerator = new GalaxyNebulaeGenerator(cloudImages, SystemNames, list);
            return galaxyNebulaeGenerator.GenerateGalaxyNebulae(generateImage, RandomSeed, StarCount, GalaxyShape, galaxyBackground.Width, galaxyBackground.Height, galaxyBackground, out locations);
        }

        public GalaxyLocation DetermineNearestRaceRegion(double x, double y)
        {
            GalaxyLocation result = null;
            double num = double.MaxValue;
            for (int i = 0; i < GalaxyLocations.Count; i++)
            {
                if (GalaxyLocations[i].Type == GalaxyLocationType.RaceRegion)
                {
                    GalaxyLocations[i].ResolveLocationCenter(out var x2, out var y2);
                    double num2 = CalculateDistanceSquared(x, y, x2, y2);
                    if (num2 < num)
                    {
                        result = GalaxyLocations[i];
                        num = num2;
                    }
                }
            }
            return result;
        }

        public GalaxyLocationList DetermineGalaxyLocationsAtPoint(double x, double y)
        {
            return DetermineGalaxyLocationsAtPoint(x, y, GalaxyLocationType.Undefined);
        }

        public GalaxyLocationList DetermineGalaxyLocationsAtPoint(double x, double y, GalaxyLocationType type)
        {
            GalaxyLocationList galaxyLocationList = new GalaxyLocationList();
            Point point = ResolveGalaxyLocationIndexes(x, y);
            for (int i = 0; i < GalaxyLocationIndex[point.X][point.Y].Count; i++)
            {
                GalaxyLocation galaxyLocation = GalaxyLocationIndex[point.X][point.Y][i];
                double num = (double)galaxyLocation.Width / 2.0;
                double num2 = num * num;
                if (type == GalaxyLocationType.Undefined || galaxyLocation.Type == type)
                {
                    double num3 = CalculateDistanceSquared(x, y, (double)galaxyLocation.Xpos + num, (double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0);
                    if (num3 < num2)
                    {
                        galaxyLocationList.Add(galaxyLocation);
                    }
                }
            }
            return galaxyLocationList;
        }

        public bool DetermineGalaxyLocationsAtPoint(double x, double y, GalaxyLocationType type, ref GalaxyLocationList locations)
        {
            bool result = false;
            Point point = ResolveGalaxyLocationIndexes(x, y);
            for (int i = 0; i < GalaxyLocationIndex[point.X][point.Y].Count; i++)
            {
                GalaxyLocation galaxyLocation = GalaxyLocationIndex[point.X][point.Y][i];
                double num = (double)galaxyLocation.Width / 2.0;
                double num2 = num * num;
                if (type != 0 && galaxyLocation.Type != type)
                {
                    continue;
                }
                double num3 = CalculateDistanceSquared(x, y, (double)galaxyLocation.Xpos + num, (double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0);
                if (num3 < num2)
                {
                    if (locations == null)
                    {
                        locations = new GalaxyLocationList();
                    }
                    locations.Add(galaxyLocation);
                    result = true;
                }
            }
            return result;
        }

        public void DetermineGalaxyLocationsAtPointSuppliedList(double x, double y, GalaxyLocationType type, ref GalaxyLocationList locations)
        {
            locations.Clear();
            Point point = ResolveGalaxyLocationIndexes(x, y);
            for (int i = 0; i < GalaxyLocationIndex[point.X][point.Y].Count; i++)
            {
                GalaxyLocation galaxyLocation = GalaxyLocationIndex[point.X][point.Y][i];
                double num = (double)galaxyLocation.Width / 2.0;
                double num2 = num * num;
                if (type == GalaxyLocationType.Undefined || galaxyLocation.Type == type)
                {
                    double num3 = CalculateDistanceSquared(x, y, (double)galaxyLocation.Xpos + num, (double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0);
                    if (num3 < num2)
                    {
                        locations.Add(galaxyLocation);
                    }
                }
            }
        }

        public GalaxyLocationList DetermineGalaxyLocationsInRangeAtPoint(double x, double y, double range, GalaxyLocationType type)
        {
            GalaxyLocationList galaxyLocationList = new GalaxyLocationList();
            Point point = ResolveGalaxyLocationIndexes(x, y);
            for (int i = 0; i < GalaxyLocationIndex[point.X][point.Y].Count; i++)
            {
                GalaxyLocation galaxyLocation = GalaxyLocationIndex[point.X][point.Y][i];
                double num = (double)galaxyLocation.Width / 2.0;
                double num2 = (num + range) * (num + range);
                if (type == GalaxyLocationType.Undefined || galaxyLocation.Type == type)
                {
                    double num3 = CalculateDistanceSquared(x, y, (double)galaxyLocation.Xpos + num, (double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0);
                    if (num3 < num2)
                    {
                        galaxyLocationList.Add(galaxyLocation);
                    }
                }
            }
            return galaxyLocationList;
        }

        public void AddGalaxyLocationIndex(GalaxyLocation location)
        {
            Point point = ResolveGalaxyLocationIndexes(location.Xpos, location.Ypos);
            Point point2 = ResolveGalaxyLocationIndexes(location.Xpos + location.Width, location.Ypos + location.Height);
            for (int i = point.X; i <= point2.X; i++)
            {
                for (int j = point.Y; j <= point2.Y; j++)
                {
                    if (!GalaxyLocationIndex[i][j].Contains(location))
                    {
                        GalaxyLocationIndex[i][j].Add(location);
                    }
                }
            }
        }

        public void RemoveGalaxyLocationIndex(GalaxyLocation location)
        {
            Point point = ResolveGalaxyLocationIndexes(location.Xpos, location.Ypos);
            Point point2 = ResolveGalaxyLocationIndexes(location.Xpos + location.Width, location.Ypos + location.Height);
            for (int i = point.X; i <= point2.X; i++)
            {
                for (int j = point.Y; j <= point2.Y; j++)
                {
                    if (GalaxyLocationIndex[i][j].Contains(location))
                    {
                        GalaxyLocationIndex[i][j].Remove(location);
                    }
                }
            }
        }

        public Point ResolveGalaxyLocationIndexes(double x, double y)
        {
            int x2 = (int)x / IndexSize;
            int y2 = (int)y / IndexSize;
            CorrectIndexCoords(ref x2, ref y2);
            return new Point(x2, y2);
        }

        public static double ResolveColonyRevenueFactorFromDifficultyForNonPlayer(int difficultyLevel)
        {
            double result = 1.0;
            switch (difficultyLevel)
            {
                case 3:
                    result = 1.2;
                    break;
                case 4:
                    result = 1.4;
                    break;
            }
            return result;
        }

        public Galaxy(int galaxyRandomSeed, GalaxyShape galaxyShape, int starCount, double colonyPrevalence, int lifePrevalence, double creaturePrevalence, double piratePrevalence, int pirateProximity, long startStarDate, double baseTechCost, double hyperdriveSpeedMultiplier, int age, double aggressionLevel, int maximumEmpireAmount, bool spawnNewEmpires, EmpireStartList empireStarts, string applicationStartupPath, string customizationSetName, Bitmap galaxyBackground, Bitmap[] cloudImages, double difficultyLevel, int sectorWidth, int sectorHeight, ResourceSystem resourceSystem, bool allowGiantKaltorGeneration)
        {
            _StopWatch = new BasicStopWatch();
            _StopWatch.Start();
            _StartDateTime = DateTime.Now.ToUniversalTime();
            _TrackedDateTime = DateTime.Now.ToUniversalTime();
            _StartStarDate = startStarDate;
            sectorWidth = Math.Max(4, Math.Min(15, sectorWidth));
            sectorHeight = Math.Max(4, Math.Min(15, sectorHeight));
            SectorWidth = sectorWidth;
            SectorHeight = sectorHeight;
            SetGalaxyPhysicalDimensions(sectorWidth, sectorHeight);
            AssignGalaxyStaticDataToInstance();
            ResourceSystem.Clear();
            ResourceSystem.Initialize(resourceSystem);
            DifficultyLevel = difficultyLevel;
            _RandomSeed = galaxyRandomSeed;
            HabitatIndex = new HabitatList[IndexMaxX][];
            for (int i = 0; i < IndexMaxX; i++)
            {
                HabitatIndex[i] = new HabitatList[IndexMaxY];
            }
            BuiltObjectIndex = new BuiltObjectList[IndexMaxX][];
            for (int j = 0; j < IndexMaxX; j++)
            {
                BuiltObjectIndex[j] = new BuiltObjectList[IndexMaxY];
            }
            GalaxyLocationIndex = new GalaxyLocationList[IndexMaxX][];
            for (int k = 0; k < IndexMaxX; k++)
            {
                GalaxyLocationIndex[k] = new GalaxyLocationList[IndexMaxY];
            }
            SystemsIndex = new SystemInfoList[IndexMaxX][];
            for (int l = 0; l < IndexMaxX; l++)
            {
                SystemsIndex[l] = new SystemInfoList[IndexMaxY];
            }
            GalaxyShape = galaxyShape;
            _StarCount = starCount;
            ApplicationStartupPath = applicationStartupPath;
            if (!string.IsNullOrEmpty(customizationSetName))
            {
                CustomizationSetPath = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\";
            }
            Races = LoadRaces(applicationStartupPath, customizationSetName);
            LoadAgentNames(applicationStartupPath, customizationSetName);
            SetRaceStartupCharacters(applicationStartupPath, customizationSetName, Races);
            SetResearchRaceSpecialProjects(Races);
            SetResearchComponentMaxTechPoints((int)baseTechCost);
            if (lifePrevalence > 2500 || lifePrevalence <= 0)
            {
                throw new ArgumentOutOfRangeException("lifePrevalence", "Must be greater than zero and less than 2500");
            }
            _LifePrevalence = lifePrevalence;
            _CreaturePrevalence = creaturePrevalence;
            AllowGiantKaltorGeneration = allowGiantKaltorGeneration;
            _PiratePrevalence = piratePrevalence;
            _PirateProximity = pirateProximity;
            _ColonyPrevalence = colonyPrevalence;
            _ResearchSpeedModifier = 1.0;
            _BaseTechCost = (int)baseTechCost;
            _HyperdriveSpeedMultiplier = hyperdriveSpeedMultiplier;
            _Age = age;
            _AggressionLevel = aggressionLevel;
            _MaximumEmpireAmount = maximumEmpireAmount;
            _SpawnNewEmpires = spawnNewEmpires;
            for (int m = 0; m < IndexMaxX; m++)
            {
                for (int n = 0; n < IndexMaxY; n++)
                {
                    HabitatIndex[m][n] = new HabitatList();
                }
            }
            for (int num = 0; num < IndexMaxX; num++)
            {
                for (int num2 = 0; num2 < IndexMaxY; num2++)
                {
                    BuiltObjectIndex[num][num2] = new BuiltObjectList();
                }
            }
            for (int num3 = 0; num3 < IndexMaxX; num3++)
            {
                for (int num4 = 0; num4 < IndexMaxY; num4++)
                {
                    SystemsIndex[num3][num4] = new SystemInfoList();
                }
            }
            foreach (ResourceDefinition resource in ResourceSystem.Resources)
            {
                ResourceCurrentPrices.Add(resource.BasePrice);
            }
            ComponentDefinition[] componentDefinitionsStatic = ComponentDefinitionsStatic;
            foreach (ComponentDefinition componentDefinition in componentDefinitionsStatic)
            {
                int count = ComponentCurrentPrices.Count;
                ComponentCurrentPrices.Add(0.0);
                foreach (ComponentResource requiredResource in componentDefinition.RequiredResources)
                {
                    ComponentCurrentPrices[count] += (double)requiredResource.BasePrice * (double)requiredResource.Quantity;
                }
            }
            LoadDesignNames(applicationStartupPath, customizationSetName);
            LoadSystemNames(applicationStartupPath, customizationSetName);
            GalaxyLocationList locations = null;
            GenerateNebulae(generateImage: true, galaxyBackground, cloudImages, out locations);
            _GalaxyLocations = locations;
            for (int num6 = 0; num6 < IndexMaxX; num6++)
            {
                for (int num7 = 0; num7 < IndexMaxY; num7++)
                {
                    GalaxyLocationIndex[num6][num7] = new GalaxyLocationList();
                }
            }
            foreach (GalaxyLocation galaxyLocation in _GalaxyLocations)
            {
                AddGalaxyLocationIndex(galaxyLocation);
            }
            empireStarts.Update(Races);
            int aggressiveRacesRequired = 0;
            if (aggressionLevel >= 1.5)
            {
                aggressiveRacesRequired = 3;
            }
            else if (aggressionLevel >= 1.3)
            {
                aggressiveRacesRequired = 2;
            }
            else if (aggressionLevel >= 1.1)
            {
                aggressiveRacesRequired = 1;
            }
            SetupAlienRacePopulations(empireStarts, aggressiveRacesRequired);
            _LifePrevalenceMultiplier = 0.8 * (Math.Sqrt(1400.0) / Math.Sqrt(_StarCount));
            if (galaxyShape == GalaxyShape.ClustersEven || galaxyShape == GalaxyShape.ClustersVaried)
            {
                int num8 = Math.Min(20, Math.Max(5, starCount / 55));
                double num9 = (double)SizeX / (Math.Sqrt(num8) * 3.0);
                double num10 = 0.0;
                for (int num11 = 0; num11 < num8; num11++)
                {
                    bool flag = false;
                    int num12 = 0;
                    while (!flag && num12 < 50)
                    {
                        flag = true;
                        double num13 = 0.0;
                        if (galaxyShape == GalaxyShape.ClustersEven)
                        {
                            num13 = 1.0 / (double)num8;
                        }
                        else
                        {
                            num13 = 1.0 / (double)num8;
                            double val = num13 / 2.0;
                            if (Rnd.Next(0, 2) != 1)
                            {
                                double num14 = 1.0 + Rnd.NextDouble() * 4.0;
                                num13 *= num14;
                                num13 = Math.Max(num13, val);
                            }
                        }
                        double num15 = (double)SizeX * 0.1 + Rnd.NextDouble() * ((double)SizeX * 0.8);
                        double num16 = (double)SizeY * 0.1 + Rnd.NextDouble() * ((double)SizeY * 0.8);
                        for (int num17 = 0; num17 < _StarClusterLocations.Count; num17++)
                        {
                            double num18 = CalculateDistance(num15, num16, _StarClusterLocations[num17].X, _StarClusterLocations[num17].Y);
                            double num19 = Math.Sqrt(_StarClusterPortions[num17]) * (double)SizeX * 0.4;
                            if (num18 < num9 + num19)
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            num10 += num13;
                            _StarClusterPortions.Add(num13);
                            _StarClusterLocations.Add(new Point((int)num15, (int)num16));
                        }
                        num12++;
                    }
                }
                if (num10 > 1.0)
                {
                    for (int num20 = 0; num20 < _StarClusterPortions.Count; num20++)
                    {
                        _StarClusterPortions[num20] /= num10;
                    }
                }
            }
            List<HabitatList> list = new List<HabitatList>();
            for (int num21 = 0; num21 < starCount; num21++)
            {
                HabitatList asteroidField = null;
                HabitatList habitatList = SetupSolarSystem(galaxyShape, out asteroidField);
                list.Add(habitatList);
                if (asteroidField != null)
                {
                    _AsteroidFields.Add(asteroidField);
                }
                for (int num22 = 0; num22 < habitatList.Count; num22++)
                {
                    Habitats.Add(habitatList[num22]);
                    int x = (int)(habitatList[num22].Xpos / (double)IndexSize);
                    int y = (int)(habitatList[num22].Ypos / (double)IndexSize);
                    CorrectIndexCoords(ref x, ref y);
                    HabitatIndex[x][y].Add(habitatList[num22]);
                }
            }
            int num23 = Rnd.Next(starCount / 5, starCount / 2);
            for (int num24 = 0; num24 < num23; num24++)
            {
                HabitatList habitatList2 = new HabitatList();
                Habitat habitat = GenerateGasCloud();
                SelectCreatures(habitat);
                habitatList2.Add(habitat);
                list.Add(habitatList2);
                for (int num25 = 0; num25 < habitatList2.Count; num25++)
                {
                    Habitats.Add(habitatList2[num25]);
                    int x2 = (int)(habitatList2[num25].Xpos / (double)IndexSize);
                    int y2 = (int)(habitatList2[num25].Ypos / (double)IndexSize);
                    CorrectIndexCoords(ref x2, ref y2);
                    HabitatIndex[x2][y2].Add(habitatList2[num25]);
                }
            }
            list.Sort();
            Habitats.Clear();
            for (int num26 = 0; num26 < IndexMaxX; num26++)
            {
                for (int num27 = 0; num27 < IndexMaxY; num27++)
                {
                    HabitatIndex[num26][num27].Clear();
                }
            }
            for (int num28 = 0; num28 < list.Count; num28++)
            {
                int x3 = (int)(list[num28][0].Xpos / (double)IndexSize);
                int y3 = (int)(list[num28][0].Ypos / (double)IndexSize);
                CorrectIndexCoords(ref x3, ref y3);
                for (int num29 = 0; num29 < list[num28].Count; num29++)
                {
                    list[num28][num29].HabitatIndex = Habitats.Count;
                    Habitats.Add(list[num28][num29]);
                    HabitatIndex[x3][y3].Add(list[num28][num29]);
                }
            }
            for (int num30 = 0; num30 < list.Count; num30++)
            {
                SystemInfo systemInfo = new SystemInfo();
                systemInfo.SystemStar = list[num30][0];
                systemInfo.Sector = ResolveSector(systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
                systemInfo.Habitats = DetermineHabitatsInSystem(list[num30][0]);
                systemInfo.SystemStar.SystemIndex = Systems.Count;
                foreach (Habitat habitat2 in systemInfo.Habitats)
                {
                    habitat2.SystemIndex = Systems.Count;
                }
                Systems.Add(systemInfo);
            }
            UpdateSystemInfo(null);
            FillSystemInfoByDistance();
            //FillShipNearSystems();
            for (int num31 = 0; num31 < Creatures.Count; num31++)
            {
                if (!Systems[Creatures[num31].ParentHabitat.SystemIndex].Creatures.Contains(Creatures[num31]))
                {
                    Systems[Creatures[num31].ParentHabitat.SystemIndex].Creatures.Add(Creatures[num31]);
                }
            }
            Orders.EnableIndexing();
            GameSummary = DetermineGameSummary();
            list = null;
        }

        public void FillSystemInfoByDistance()
        {
            _SystemInfoByDistance = new Dictionary<SystemInfo, SystemInfo[]>(Systems.Count);
            for (int i = 0; i < Systems.Count; i++)
            {
                _SystemInfoByDistance.Add(Systems[i], Systems.Select(p => new
                {
                    SystemInfo = p,
                    Distance = Math.Sqrt(
                           Math.Pow(Math.Abs(p.SystemStar.Xpos - Systems[i].SystemStar.Xpos), 2) +
                           Math.Pow(Math.Abs(p.SystemStar.Ypos - Systems[i].SystemStar.Ypos), 2)
                       )
                }).OrderBy(p => p.Distance).Select(x => x.SystemInfo).ToArray());
                foreach (var hab in Systems[i].Habitats)
                {
                    hab.SystemInfo = Systems[i];
                }
                Systems[i].SystemStar.SystemInfo = Systems[i];
            }
        }

        //public void FillShipNearSystems()
        //{
        //    foreach (var item in Empires)
        //    {
        //        foreach (var ship in item.BuiltObjects)
        //            BaconBuiltObject.AssignNearestSystemStarIfNull(ship);
        //        foreach (var ship in item.PrivateBuiltObjects)
        //            BaconBuiltObject.AssignNearestSystemStarIfNull(ship);
        //    }
        //}

        public string GenerateBuiltObjectName(Design design)
        {
            return GenerateBuiltObjectName(design, null);
        }

        public string GenerateBuiltObjectName(Design design, Habitat habitat)
        {
            return GenerateBuiltObjectName(design, habitat, uniqueNamesForSmallMilitaryShips: false);
        }

        public string GenerateBuiltObjectName(Design design, Habitat habitat, bool uniqueNamesForSmallMilitaryShips)
        {
            string empty = string.Empty;
            bool flag = false;
            empty = GetCustomName(design);
            if (string.IsNullOrEmpty(empty))
            {
                switch (design.SubRole)
                {
                    case BuiltObjectSubRole.Escort:
                    case BuiltObjectSubRole.Frigate:
                    case BuiltObjectSubRole.Destroyer:
                    case BuiltObjectSubRole.TroopTransport:
                        if (!uniqueNamesForSmallMilitaryShips)
                        {
                            flag = true;
                        }
                        break;
                    case BuiltObjectSubRole.ResupplyShip:
                    case BuiltObjectSubRole.ExplorationShip:
                    case BuiltObjectSubRole.SmallFreighter:
                    case BuiltObjectSubRole.MediumFreighter:
                    case BuiltObjectSubRole.LargeFreighter:
                    case BuiltObjectSubRole.ColonyShip:
                    case BuiltObjectSubRole.PassengerShip:
                    case BuiltObjectSubRole.ConstructionShip:
                    case BuiltObjectSubRole.GasMiningShip:
                    case BuiltObjectSubRole.MiningShip:
                    case BuiltObjectSubRole.GasMiningStation:
                    case BuiltObjectSubRole.MiningStation:
                    case BuiltObjectSubRole.Outpost:
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
                        flag = false;
                        break;
                    default:
                        if (design.BuildCount <= 1 && design.SubRole != BuiltObjectSubRole.Carrier)
                        {
                            flag = true;
                        }
                        break;
                }
            }
            if (flag)
            {
                switch (design.SubRole)
                {
                    case BuiltObjectSubRole.GenericBase:
                        empty = design.Name + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.Escort:
                    case BuiltObjectSubRole.Frigate:
                    case BuiltObjectSubRole.Destroyer:
                    case BuiltObjectSubRole.TroopTransport:
                        empty = design.Name + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.Cruiser:
                    case BuiltObjectSubRole.CapitalShip:
                    case BuiltObjectSubRole.Carrier:
                    case BuiltObjectSubRole.ResupplyShip:
                        empty = design.Name;
                        break;
                    case BuiltObjectSubRole.MonitoringStation:
                        empty = design.Name + " " + ResolveDescription(BuiltObjectSubRole.MonitoringStation) + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.DefensiveBase:
                        empty = design.Name + " " + ResolveDescription(BuiltObjectSubRole.DefensiveBase) + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.EnergyResearchStation:
                        empty = design.Name + " " + ResolveDescription(BuiltObjectSubRole.EnergyResearchStation) + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.WeaponsResearchStation:
                        empty = design.Name + " " + ResolveDescription(BuiltObjectSubRole.WeaponsResearchStation) + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.HighTechResearchStation:
                        empty = design.Name + " " + ResolveDescription(BuiltObjectSubRole.HighTechResearchStation) + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.Outpost:
                        empty = design.Name + " " + TextResolver.GetText("Outpost") + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.SmallSpacePort:
                    case BuiltObjectSubRole.MediumSpacePort:
                    case BuiltObjectSubRole.LargeSpacePort:
                        empty = design.Name + " " + TextResolver.GetText("Space Port") + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.MiningShip:
                        empty = design.Name + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.GasMiningShip:
                        empty = design.Name + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.MiningStation:
                        empty = design.Name + " " + ResolveDescription(BuiltObjectSubRole.MiningStation) + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.GasMiningStation:
                        empty = design.Name + " " + ResolveDescription(BuiltObjectSubRole.GasMiningStation) + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.SmallFreighter:
                    case BuiltObjectSubRole.MediumFreighter:
                    case BuiltObjectSubRole.LargeFreighter:
                        empty = design.Name + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.ColonyShip:
                        empty = design.Name + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.ConstructionShip:
                        empty = design.Name + " " + design.BuildCount.ToString("000");
                        break;
                    case BuiltObjectSubRole.ExplorationShip:
                        empty = design.Name + " " + design.BuildCount.ToString("000");
                        break;
                }
            }
            else
            {
                empty = SelectUniqueBuiltObjectName(design, habitat);
            }
            return empty;
        }

        public bool DetermineBuiltObjectIsState(BuiltObjectSubRole subRole)
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
                case BuiltObjectSubRole.Outpost:
                case BuiltObjectSubRole.SmallSpacePort:
                case BuiltObjectSubRole.MediumSpacePort:
                case BuiltObjectSubRole.LargeSpacePort:
                case BuiltObjectSubRole.GenericBase:
                case BuiltObjectSubRole.EnergyResearchStation:
                case BuiltObjectSubRole.WeaponsResearchStation:
                case BuiltObjectSubRole.HighTechResearchStation:
                case BuiltObjectSubRole.MonitoringStation:
                case BuiltObjectSubRole.DefensiveBase:
                    return true;
                case BuiltObjectSubRole.SmallFreighter:
                case BuiltObjectSubRole.MediumFreighter:
                case BuiltObjectSubRole.LargeFreighter:
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                case BuiltObjectSubRole.GasMiningStation:
                case BuiltObjectSubRole.MiningStation:
                    return false;
                default:
                    return false;
            }
        }

        public string GenerateMoonName(Habitat moon)
        {
            string text = GenerateCodeName();
            _ = moon.Parent;
            DetermineHabitatSystemStar(moon);
            return GenerateRandomNameAlt();
        }

        public string GenerateRandomName()
        {
            string text = string.Empty;
            string[] array = new string[6] { "a", "e", "i", "o", "u", "y" };
            string[] array2 = new string[21]
            {
            "b", "c", "d", "f", "g", "h", "j", "k", "l", "m",
            "n", "p", "q", "r", "s", "t", "v", "w", "x", "y",
            "z"
            };
            int num = 7;
            int num2 = Rnd.Next(2, 5);
            for (int i = 0; i < num2; i++)
            {
                int num3 = 0;
                int num4 = 0;
                switch (Rnd.Next(0, 4))
                {
                    case 0:
                        num3 = Rnd.Next(0, array2.Length);
                        text += array2[num3];
                        num4 = Rnd.Next(0, array.Length);
                        text += array[num4];
                        break;
                    case 1:
                        {
                            num4 = Rnd.Next(0, array.Length);
                            int iterationCount2 = 0;
                            while (ConditionCheckLimit(CheckForIllegalVowelCombination(text, array[num4]), 50, ref iterationCount2))
                            {
                                num4 = Rnd.Next(0, array.Length);
                            }
                            text += array[num4];
                            num3 = Rnd.Next(0, array2.Length);
                            text += array2[num3];
                            break;
                        }
                    case 2:
                        num3 = Rnd.Next(0, array2.Length);
                        text += array2[num3];
                        num4 = Rnd.Next(0, array.Length);
                        text += array[num4];
                        num3 = Rnd.Next(0, array2.Length);
                        text += array2[num3];
                        break;
                    case 3:
                        {
                            num4 = Rnd.Next(0, array.Length);
                            int iterationCount = 0;
                            while (ConditionCheckLimit(CheckForIllegalVowelCombination(text, array[num4]), 50, ref iterationCount))
                            {
                                num4 = Rnd.Next(0, array.Length);
                            }
                            text += array[num4];
                            num3 = Rnd.Next(0, array2.Length);
                            text += array2[num3];
                            num4 = Rnd.Next(0, array.Length);
                            text += array[num4];
                            break;
                        }
                }
                if (text.Length > num)
                {
                    break;
                }
            }
            return text.Substring(0, 1).ToUpper(CultureInfo.InvariantCulture) + text.Substring(1, text.Length - 1);
        }

        private string GenerateRandomNameAlt()
        {
            string text = string.Empty;
            int num = Rnd.Next(4, 9);
            int num2 = Rnd.Next(0, 2);
            int num3 = 0;
            int num4 = 0;
            int iterationCount = 0;
            while (ConditionCheckLimit(text.Length < num, 50, ref iterationCount))
            {
                switch (num2)
                {
                    case 0:
                        if (Rnd.Next(0, 2) == 0 && text.Length > 0 && num3 == 0)
                        {
                            text = ((text.Length < num - 2) ? AddVowelCombination(text) : AddVowelCombinationEnd(text));
                            num3++;
                        }
                        else
                        {
                            text = AddVowel(text);
                        }
                        num2 = 1;
                        break;
                    case 1:
                        if (Rnd.Next(0, 2) != 0 || num4 != 0)
                        {
                            text = ((text.Length < num - 1) ? AddConsonant(text) : AddConsonantEnd(text));
                        }
                        else
                        {
                            text = ((text.Length <= 0) ? AddConsonantCombinationStart(text) : ((text.Length < num - 2) ? AddConsonantCombination(text) : AddConsonantCombinationEnd(text)));
                            num4++;
                        }
                        num2 = 0;
                        break;
                }
            }
            return text.Substring(0, 1).ToUpper(CultureInfo.InvariantCulture) + text.Substring(1, text.Length - 1);
        }

        private string AddVowel(string word)
        {
            string[] array = new string[12]
            {
            "a", "a", "a", "e", "e", "e", "e", "i", "i", "o",
            "o", "u"
            };
            int num = Rnd.Next(0, array.Length);
            word += array[num];
            return word;
        }

        private string AddVowelEnd(string word)
        {
            string[] array = new string[6] { "a", "a", "o", "o", "u", "y" };
            int num = Rnd.Next(0, array.Length);
            word += array[num];
            return word;
        }

        private string AddVowelCombination(string word)
        {
            string[] array = new string[12]
            {
            "ai", "au", "ea", "ee", "ei", "eu", "ey", "oa", "oi", "oo",
            "ou", "ui"
            };
            int num = Rnd.Next(0, array.Length);
            word += array[num];
            return word;
        }

        private string AddVowelCombinationEnd(string word)
        {
            string[] array = new string[10] { "ai", "au", "ea", "eu", "ie", "oa", "oi", "oo", "oy", "ui" };
            int num = Rnd.Next(0, array.Length);
            word += array[num];
            return word;
        }

        private string AddConsonant(string word)
        {
            string[] array = new string[65]
            {
            "b", "b", "c", "c", "c", "d", "d", "d", "d", "f",
            "f", "g", "g", "h", "h", "h", "h", "h", "h", "j",
            "k", "l", "l", "l", "l", "m", "m", "m", "n", "n",
            "n", "n", "n", "n", "n", "p", "p", "r", "r", "r",
            "r", "r", "r", "s", "s", "s", "s", "s", "s", "t",
            "t", "t", "t", "t", "t", "t", "t", "t", "v", "w",
            "w", "x", "y", "y", "z"
            };
            int num = Rnd.Next(0, array.Length);
            word += array[num];
            return word;
        }

        private string AddConsonantEnd(string word)
        {
            string[] array = new string[35]
            {
            "b", "d", "d", "d", "d", "d", "f", "f", "g", "k",
            "l", "l", "m", "n", "n", "n", "n", "p", "r", "r",
            "r", "s", "s", "s", "s", "s", "s", "s", "t", "t",
            "t", "t", "v", "x", "z"
            };
            int num = Rnd.Next(0, array.Length);
            word += array[num];
            return word;
        }

        private string AddConsonantCombinationStart(string word)
        {
            string[] array = new string[29]
            {
            "bl", "br", "ch", "cl", "cr", "dr", "fl", "fr", "gh", "gl",
            "gr", "kl", "kr", "ph", "pl", "pr", "qu", "rh", "ry", "sc",
            "sh", "sk", "sl", "sm", "sn", "sp", "st", "th", "tr"
            };
            int num = Rnd.Next(0, array.Length);
            word += array[num];
            return word;
        }

        private string AddConsonantCombinationEnd(string word)
        {
            string[] array = new string[36]
            {
            "ff", "gh", "ld", "lf", "lg", "lk", "ll", "lm", "lt", "ms",
            "nc", "nd", "ng", "nk", "ns", "nt", "ny", "ph", "rc", "rd",
            "rf", "rg", "rk", "rl", "rm", "rn", "rp", "rs", "rt", "ry",
            "sc", "sh", "sk", "ss", "st", "th"
            };
            int num = Rnd.Next(0, array.Length);
            word += array[num];
            return word;
        }

        private string AddConsonantCombination(string word)
        {
            string[] array = new string[73]
            {
            "bb", "bl", "br", "ch", "cl", "cr", "dd", "dr", "ff", "fl",
            "fr", "gg", "gl", "gr", "kl", "kr", "lc", "ld", "lf", "lg",
            "lk", "ll", "lm", "ln", "lp", "ls", "lt", "mb", "mm", "mn",
            "mp", "ms", "nc", "nd", "ng", "nk", "nn", "ns", "nt", "ph",
            "pl", "pp", "pr", "ps", "qu", "rb", "rc", "rd", "rf", "rg",
            "rh", "rk", "rl", "rm", "rn", "rp", "rr", "rs", "rt", "ry",
            "sc", "sh", "sk", "sl", "sm", "sn", "sp", "ss", "st", "th",
            "tr", "wl", "xx"
            };
            int num = Rnd.Next(0, array.Length);
            word += array[num];
            return word;
        }

        private bool CheckForIllegalVowelCombination(string word, string newChar)
        {
            string[] array = new string[13]
            {
            "aa", "ae", "ao", "eo", "ia", "ii", "io", "iu", "iy", "ua",
            "uo", "uu", "uy"
            };
            if (word.Length > 0)
            {
                string text = word.Substring(word.Length - 1, 1);
                for (int i = 0; i < array.Length; i++)
                {
                    if (text + newChar == array[i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public string GenerateCodeName()
        {
            string text = ((char)Rnd.Next(65, 91)).ToString() + (char)Rnd.Next(65, 91);
            return text + Rnd.Next(1, 1000);
        }

        private Habitat GenerateGasCloud()
        {
            HabitatType habitatType = HabitatType.Ammonia;
            switch (Rnd.Next(0, 15))
            {
                case 0:
                    habitatType = HabitatType.Ammonia;
                    break;
                case 1:
                    habitatType = HabitatType.Argon;
                    break;
                case 2:
                    habitatType = HabitatType.CarbonDioxide;
                    break;
                case 3:
                    habitatType = HabitatType.Chlorine;
                    break;
                case 4:
                case 5:
                    habitatType = HabitatType.Helium;
                    break;
                case 6:
                case 7:
                case 8:
                    habitatType = HabitatType.Hydrogen;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                    habitatType = HabitatType.NitrogenOxygen;
                    break;
                case 13:
                case 14:
                    habitatType = HabitatType.Oxygen;
                    break;
            }
            double num = 0.0;
            int num2 = 0;
            Habitat habitat;
            do
            {
                int index = Rnd.Next(0, _GalaxyLocations.Count);
                int iterationCount = 0;
                while (ConditionCheckLimit(_GalaxyLocations[index].Type != GalaxyLocationType.NebulaCloud, 200, ref iterationCount))
                {
                    index = Rnd.Next(0, _GalaxyLocations.Count);
                }
                double num3 = (double)_GalaxyLocations[index].Width * 0.15 + Rnd.NextDouble() * (double)_GalaxyLocations[index].Width * 0.7;
                double num4 = (double)_GalaxyLocations[index].Height * 0.15 + Rnd.NextDouble() * (double)_GalaxyLocations[index].Height * 0.7;
                double x = (double)_GalaxyLocations[index].Xpos + num3;
                double y = (double)_GalaxyLocations[index].Ypos + num4;
                habitat = new Habitat(this, HabitatCategoryType.GasCloud, habitatType, GenerateCodeName(), x, y);
                habitat.Diameter = (short)Rnd.Next(8000, 32000);
                Habitat habitat2 = FindNearestSystemGasCloudAsteroid(habitat.Xpos, habitat.Ypos);
                num = ((habitat2 == null) ? double.MaxValue : CalculateDistance(habitat.Xpos, habitat.Ypos, habitat2.Xpos, habitat2.Ypos));
                num2++;
            }
            while (num < (double)(MaxSolarSystemSize * 4) && num2 < 200);
            byte solarRadiation = (byte)Rnd.Next(40, 60);
            byte microwaveRadiation = (byte)Rnd.Next(1, 5);
            byte xrayRadiation = (byte)Rnd.Next(0, 3);
            habitat.SolarRadiation = solarRadiation;
            habitat.MicrowaveRadiation = microwaveRadiation;
            habitat.XrayRadiation = xrayRadiation;
            habitat = SelectResources(habitat);
            switch (habitat.Type)
            {
                case HabitatType.Hydrogen:
                case HabitatType.Helium:
                    if (habitat.Diameter >= 0 && habitat.Diameter < 1750)
                    {
                        habitat.PictureRef = 79;
                    }
                    else if (habitat.Diameter >= 1750 && habitat.Diameter < 3000)
                    {
                        habitat.PictureRef = 80;
                    }
                    else if (habitat.Diameter >= 3000 && habitat.Diameter < 4250)
                    {
                        habitat.PictureRef = 81;
                    }
                    else
                    {
                        habitat.PictureRef = 82;
                    }
                    break;
                case HabitatType.Argon:
                case HabitatType.Ammonia:
                case HabitatType.CarbonDioxide:
                    if (habitat.Diameter >= 0 && habitat.Diameter < 1750)
                    {
                        habitat.PictureRef = 75;
                    }
                    else if (habitat.Diameter >= 1750 && habitat.Diameter < 3000)
                    {
                        habitat.PictureRef = 76;
                    }
                    else if (habitat.Diameter >= 3000 && habitat.Diameter < 4250)
                    {
                        habitat.PictureRef = 77;
                    }
                    else
                    {
                        habitat.PictureRef = 78;
                    }
                    break;
                case HabitatType.Oxygen:
                case HabitatType.NitrogenOxygen:
                case HabitatType.Chlorine:
                    if (habitat.Diameter >= 0 && habitat.Diameter < 1750)
                    {
                        habitat.PictureRef = 71;
                    }
                    else if (habitat.Diameter >= 1750 && habitat.Diameter < 3000)
                    {
                        habitat.PictureRef = 72;
                    }
                    else if (habitat.Diameter >= 3000 && habitat.Diameter < 4250)
                    {
                        habitat.PictureRef = 73;
                    }
                    else
                    {
                        habitat.PictureRef = 74;
                    }
                    break;
            }
            switch (habitat.Type)
            {
                case HabitatType.Ammonia:
                    habitat.MapPictureRef = 16;
                    break;
                case HabitatType.Argon:
                    habitat.MapPictureRef = 17;
                    break;
                case HabitatType.CarbonDioxide:
                    habitat.MapPictureRef = 18;
                    break;
                case HabitatType.Chlorine:
                    habitat.MapPictureRef = 19;
                    break;
                case HabitatType.Helium:
                    habitat.MapPictureRef = 20;
                    break;
                case HabitatType.Hydrogen:
                    habitat.MapPictureRef = 21;
                    break;
                case HabitatType.NitrogenOxygen:
                    habitat.MapPictureRef = 22;
                    break;
                case HabitatType.Oxygen:
                    habitat.MapPictureRef = 23;
                    break;
            }
            habitat.LandscapePictureRef = -1;
            if (Rnd.Next(0, 5) == 2)
            {
                habitat.OrbitDirection = false;
            }
            return habitat;
        }

        public Habitat GenerateGasCloud(HabitatType habitatType, double x, double y)
        {
            Habitat habitat = null;
            Rnd.Next(0, 7);
            double num = 0.0;
            habitat = new Habitat(this, HabitatCategoryType.GasCloud, habitatType, GenerateCodeName(), x, y);
            habitat.Diameter = (short)Rnd.Next(8000, 32000);
            Habitat habitat2 = FindNearestSystemGasCloudAsteroid(habitat.Xpos, habitat.Ypos);
            num = ((habitat2 == null) ? double.MaxValue : CalculateDistance(habitat.Xpos, habitat.Ypos, habitat2.Xpos, habitat2.Ypos));
            if (num < (double)(MaxSolarSystemSize * 4))
            {
                return null;
            }
            byte solarRadiation = (byte)Rnd.Next(40, 60);
            byte microwaveRadiation = (byte)Rnd.Next(1, 5);
            byte xrayRadiation = (byte)Rnd.Next(0, 3);
            habitat.SolarRadiation = solarRadiation;
            habitat.MicrowaveRadiation = microwaveRadiation;
            habitat.XrayRadiation = xrayRadiation;
            habitat = SelectResources(habitat);
            switch (habitat.Type)
            {
                case HabitatType.Hydrogen:
                case HabitatType.Helium:
                    if (habitat.Diameter >= 0 && habitat.Diameter < 1750)
                    {
                        habitat.PictureRef = 79;
                    }
                    else if (habitat.Diameter >= 1750 && habitat.Diameter < 3000)
                    {
                        habitat.PictureRef = 80;
                    }
                    else if (habitat.Diameter >= 3000 && habitat.Diameter < 4250)
                    {
                        habitat.PictureRef = 81;
                    }
                    else
                    {
                        habitat.PictureRef = 82;
                    }
                    break;
                case HabitatType.Argon:
                case HabitatType.Ammonia:
                case HabitatType.CarbonDioxide:
                    if (habitat.Diameter >= 0 && habitat.Diameter < 1750)
                    {
                        habitat.PictureRef = 75;
                    }
                    else if (habitat.Diameter >= 1750 && habitat.Diameter < 3000)
                    {
                        habitat.PictureRef = 76;
                    }
                    else if (habitat.Diameter >= 3000 && habitat.Diameter < 4250)
                    {
                        habitat.PictureRef = 77;
                    }
                    else
                    {
                        habitat.PictureRef = 78;
                    }
                    break;
                case HabitatType.Oxygen:
                case HabitatType.NitrogenOxygen:
                case HabitatType.Chlorine:
                    if (habitat.Diameter >= 0 && habitat.Diameter < 1750)
                    {
                        habitat.PictureRef = 71;
                    }
                    else if (habitat.Diameter >= 1750 && habitat.Diameter < 3000)
                    {
                        habitat.PictureRef = 72;
                    }
                    else if (habitat.Diameter >= 3000 && habitat.Diameter < 4250)
                    {
                        habitat.PictureRef = 73;
                    }
                    else
                    {
                        habitat.PictureRef = 74;
                    }
                    break;
            }
            switch (habitat.Type)
            {
                case HabitatType.Ammonia:
                    habitat.MapPictureRef = 16;
                    break;
                case HabitatType.Argon:
                    habitat.MapPictureRef = 17;
                    break;
                case HabitatType.CarbonDioxide:
                    habitat.MapPictureRef = 18;
                    break;
                case HabitatType.Chlorine:
                    habitat.MapPictureRef = 19;
                    break;
                case HabitatType.Helium:
                    habitat.MapPictureRef = 20;
                    break;
                case HabitatType.Hydrogen:
                    habitat.MapPictureRef = 21;
                    break;
                case HabitatType.NitrogenOxygen:
                    habitat.MapPictureRef = 22;
                    break;
                case HabitatType.Oxygen:
                    habitat.MapPictureRef = 23;
                    break;
            }
            habitat.LandscapePictureRef = -1;
            if (Rnd.Next(0, 5) == 2)
            {
                habitat.OrbitDirection = false;
            }
            return habitat;
        }

        public void SetRestrictedResources()
        {
            for (int i = 0; i < ResourceSystem.SuperLuxuryResources.Count; i++)
            {
                ResourceDefinition resourceDefinition = ResourceSystem.SuperLuxuryResources[i];
                if (resourceDefinition == null || resourceDefinition.Prevalence == null)
                {
                    continue;
                }
                for (int j = 0; j < resourceDefinition.Prevalence.Count; j++)
                {
                    ResourcePrevalence resourcePrevalence = resourceDefinition.Prevalence[j];
                    if (resourcePrevalence == null)
                    {
                        continue;
                    }
                    int num = Math.Max(1, (int)(resourcePrevalence.Prevalence * ((float)StarCount / 700f)));
                    bool nameUsed = false;
                    string nameIfMoon = string.Empty;
                    if (!resourcePrevalence.HabitatIsAsteroid && !resourcePrevalence.HabitatIsGasCloud)
                    {
                        switch (resourceDefinition.Name)
                        {
                            case "Korabbian Spice":
                                nameIfMoon = "Korabbia";
                                break;
                            case "Loros Fruit":
                                nameIfMoon = "Loros";
                                break;
                            case "Zentabia Fluid":
                                nameIfMoon = "Zentabia";
                                break;
                        }
                    }
                    for (int k = 0; k < num; k++)
                    {
                        if (nameUsed)
                        {
                            nameIfMoon = string.Empty;
                        }
                        SetSingleRestrictedResource(resourceDefinition.ResourceID, resourcePrevalence.HabitatType, nameIfMoon, out nameUsed);
                    }
                }
            }
        }

        private void SetSingleRestrictedResource(byte resourceId, HabitatType habitatType, string nameIfMoon, out bool nameUsed)
        {
            nameUsed = false;
            FindLonelyDeepSpaceLocation(out var x, out var y);
            Habitat habitat = FindNearestHabitat(x, y, habitatType);
            bool flag = false;
            int num = 0;
            while (!flag && num < 100)
            {
                if (habitat != null)
                {
                    bool flag2 = true;
                    Habitat habitat2 = FindNearestColony(habitat.Xpos, habitat.Ypos, null, 0, includeIndependentColonies: false);
                    if (habitat2 != null)
                    {
                        double num2 = CalculateDistance(habitat2.Xpos, habitat2.Ypos, habitat.Xpos, habitat.Ypos);
                        if (num2 < (double)MaxSolarSystemSize * 2.1)
                        {
                            flag2 = false;
                        }
                    }
                    if (habitat.Resources.Count >= 5)
                    {
                        flag2 = false;
                    }
                    int num3 = habitat.Resources.IndexOf(resourceId, 0);
                    if (num3 >= 0)
                    {
                        flag2 = false;
                    }
                    if (flag2)
                    {
                        habitat.Resources.Add(new HabitatResource(resourceId, Rnd.Next(700, 1000)));
                        flag = true;
                        if (!string.IsNullOrEmpty(nameIfMoon) && habitat.Category == HabitatCategoryType.Moon)
                        {
                            habitat.Name = nameIfMoon;
                            nameUsed = true;
                        }
                    }
                    else
                    {
                        FindLonelyDeepSpaceLocation(out x, out y);
                        habitat = FindNearestHabitat(x, y, habitatType);
                    }
                }
                num++;
            }
        }

        public float SelectHabitatQuality(Habitat habitat, float colonyPrevalence)
        {
            float num = 1f;
            float num2 = 0f;
            float num3 = 0f;
            float num4 = 0f;
            float num5 = 0f;
            float num6 = 0f;
            switch (habitat.Type)
            {
                case HabitatType.BarrenRock:
                    num2 = 0f;
                    num3 = 0.02f;
                    num4 = 0f;
                    break;
                case HabitatType.Continental:
                    num2 = 1f;
                    num3 = 0.2f;
                    num4 = 0f;
                    break;
                case HabitatType.Ice:
                    num2 = 0.3f;
                    num3 = 0.18f;
                    num4 = 0.11f;
                    num4 *= colonyPrevalence;
                    break;
                case HabitatType.MarshySwamp:
                    num2 = 0.85f;
                    num3 = 0.15f;
                    num4 = 0f;
                    break;
                case HabitatType.Ocean:
                    num2 = 0.8f;
                    num3 = 0.75f;
                    num5 = num2 - 0.5f;
                    num6 = num5 * colonyPrevalence;
                    num2 = num6 + 0.5f;
                    num4 = 0.03f;
                    num4 *= colonyPrevalence;
                    break;
                case HabitatType.Desert:
                    num2 = 0.75f;
                    num3 = 0.72f;
                    num5 = num2 - 0.5f;
                    num6 = num5 * colonyPrevalence;
                    num2 = num6 + 0.5f;
                    num4 = 0.08f;
                    num4 *= colonyPrevalence;
                    break;
                case HabitatType.Volcanic:
                    num2 = 0.25f;
                    num3 = 0.18f;
                    num4 = 0.18f;
                    num4 *= colonyPrevalence;
                    break;
                default:
                    num2 = 0f;
                    num3 = 0f;
                    num4 = 0f;
                    break;
            }
            num = num2 - (float)Rnd.NextDouble() * num3;
            if ((float)Rnd.NextDouble() < num4)
            {
                num = 0.5f + (float)Rnd.NextDouble() * 0.5f;
            }
            if (num >= 0.5f && num < 0.6f && Rnd.Next(0, 5) > 0)
            {
                num = 0.6f + (float)Rnd.NextDouble() * 0.12f;
            }
            return Math.Min(1f, Math.Max(0f, num));
        }

        public static List<byte> ResolveValidResourcesForHabitatTypeExcludeManufactured(HabitatType habitatType)
        {
            return ResolveValidResourcesForHabitatTypeExcludeManufactured(habitatType, allowSuperLuxuryResources: true);
        }

        public static List<byte> ResolveValidResourcesForHabitatTypeExcludeManufactured(HabitatType habitatType, bool allowSuperLuxuryResources)
        {
            List<byte> list = new List<byte>();
            for (int i = 0; i < ResourceSystemStatic.Resources.Count; i++)
            {
                ResourceDefinition resourceDefinition = ResourceSystemStatic.Resources[i];
                if (resourceDefinition == null || (!allowSuperLuxuryResources && resourceDefinition.SuperLuxuryBonusAmount > 0) || resourceDefinition.ColonyManufacturingLevel > 0 || resourceDefinition.Prevalence == null || resourceDefinition.Prevalence.Count <= 0)
                {
                    continue;
                }
                for (int j = 0; j < resourceDefinition.Prevalence.Count; j++)
                {
                    ResourcePrevalence resourcePrevalence = resourceDefinition.Prevalence[j];
                    if (resourcePrevalence != null && resourcePrevalence.HabitatType == habitatType && !list.Contains(resourceDefinition.ResourceID))
                    {
                        list.Add(resourceDefinition.ResourceID);
                    }
                }
            }
            return list;
        }

        public Habitat SelectResources(Habitat habitat)
        {
            return SelectResources(habitat, 0);
        }

        public Habitat SelectResources(Habitat habitat, int minimumResourceCount)
        {
            return SelectResources(habitat, minimumResourceCount, null, 0);
        }

        public Habitat SelectResources(Habitat habitat, int minimumResourceCount, Race dominantRace, int minimumCriticalResourceCount)
        {
            return SelectResources(habitat, minimumResourceCount, null, minimumCriticalResourceCount, null);
        }

        public Habitat SelectResources(Habitat habitat, int minimumResourceCount, Race dominantRace, int minimumCriticalResourceCount, ResourceDefinitionList randomOrderedResources)
        {
            int num = 0;
            if (_StarCount <= 250)
            {
                num = 1;
            }
            int num2;
            if (habitat.Diameter >= 85)
            {
                num2 = ((habitat.Diameter < 130) ? Rnd.Next(0, 3 + num) : ((habitat.Diameter >= 170) ? Rnd.Next(1 + num, 6) : Rnd.Next(0, 4 + num)));
            }
            else
            {
                num2 = Rnd.Next(0, 2 + num);
                if (habitat.Category == HabitatCategoryType.Asteroid)
                {
                    num2 = 0;
                    if (Rnd.Next(0, 3) == 1)
                    {
                        num2 = Rnd.Next(0, 2 + num);
                    }
                }
            }
            if (num2 < minimumResourceCount)
            {
                num2 = minimumResourceCount;
            }
            minimumCriticalResourceCount = Math.Min(minimumCriticalResourceCount, num2);
            ResourceList resourceList = new ResourceList();
            if (dominantRace != null && minimumResourceCount > 0)
            {
                ResourceList resourceList2 = dominantRace.CriticalResources.ResolveResources();
                for (int i = 0; i < resourceList2.Count && i < minimumCriticalResourceCount; i++)
                {
                    resourceList.Add(resourceList2[i]);
                }
                ResourceList resourceList3 = ResourceSystem.Resources.ResolveValidResourcesForHabitatExcludeManufactured(habitat);
                for (int j = 0; j < resourceList.Count; j++)
                {
                    if (resourceList3.Contains(resourceList[j]))
                    {
                        habitat.Resources.Add(new HabitatResource(resourceList[j].ResourceID, Rnd.Next(200, 800)));
                    }
                }
            }
            if (randomOrderedResources == null || randomOrderedResources.Count <= 0)
            {
                randomOrderedResources = ResourceSystem.GenerateRandomOrderedResources();
            }
            for (int k = 0; k < randomOrderedResources.Count; k++)
            {
                if (habitat.Resources.Count >= 5)
                {
                    break;
                }
                ResourceDefinition resourceDefinition = randomOrderedResources[k];
                if (resourceDefinition == null || resourceDefinition.ColonyManufacturingLevel > 0 || resourceDefinition.Prevalence == null || resourceDefinition.Prevalence.Count <= 0 || resourceDefinition.SuperLuxuryBonusAmount > 0)
                {
                    continue;
                }
                for (int l = 0; l < resourceDefinition.Prevalence.Count; l++)
                {
                    ResourcePrevalence resourcePrevalence = resourceDefinition.Prevalence[l];
                    if (resourcePrevalence == null || !ResourceSystem.Resources.CheckPrevalenceValidForHabitat(habitat, resourcePrevalence))
                    {
                        continue;
                    }
                    float num3 = (float)CryptoRnd.NextDouble();
                    if (num3 < resourcePrevalence.Prevalence)
                    {
                        int val = (int)(resourcePrevalence.AbundanceMinimum * 1000f);
                        int val2 = (int)(resourcePrevalence.AbundanceMaximum * 1000f);
                        val = Math.Max(0, Math.Min(1000, val));
                        val2 = Math.Max(0, Math.Min(1000, val2));
                        if (val > val2)
                        {
                            val = val2;
                        }
                        habitat.Resources.Add(new HabitatResource(resourceDefinition.ResourceID, CryptoRnd.Next(val, val2)));
                    }
                }
            }
            return habitat;
        }

        public void LoadDesignNames(string applicationStartupPath, string customizationSetName)
        {
            int num = 0;
            string text = applicationStartupPath + "\\designNames.txt";
            if (!string.IsNullOrEmpty(customizationSetName) && customizationSetName.ToLower(CultureInfo.InvariantCulture) != "default")
            {
                text = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\designNames.txt";
            }
            if (!File.Exists(text))
            {
                text = applicationStartupPath + "\\designNames.txt";
            }
            try
            {
                if (!File.Exists(text))
                {
                    throw new ApplicationException("Missing file: " + text);
                }
                FileStream fileStream = File.OpenRead(text);
                StreamReader streamReader = new StreamReader(fileStream);
                while (!streamReader.EndOfStream)
                {
                    num++;
                    List<string> list = new List<string>();
                    string text2 = streamReader.ReadLine();
                    if (string.IsNullOrEmpty(text2) || !(text2.Trim() != string.Empty) || !(text2.Trim().Substring(0, 1) != "'"))
                    {
                        continue;
                    }
                    int num2 = 0;
                    int num3 = 0;
                    while (num3 >= 0)
                    {
                        num3 = text2.IndexOf(",", num2);
                        string empty = string.Empty;
                        empty = ((num3 < 0) ? text2.Substring(num2, text2.Length - num2) : text2.Substring(num2, num3 - num2));
                        empty = empty.Trim();
                        if (!string.IsNullOrEmpty(empty))
                        {
                            list.Add(empty);
                        }
                        num2 = num3 + 1;
                    }
                    if (list.Count == 0)
                    {
                        throw new ApplicationException("No design names at line " + num + " in file " + text);
                    }
                    _DesignNames.Add(list.ToArray());
                }
                streamReader.Close();
                fileStream.Close();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error at line " + num + " reading file " + text);
            }
            if (_DesignNames.Count < 14)
            {
                throw new ApplicationException("Must be at least 14 design name families in designs.txt");
            }
        }

        private void LoadSystemNames(string applicationStartupPath, string customizationSetName)
        {
            int num = 0;
            string text = applicationStartupPath + "\\systemNames.txt";
            if (!string.IsNullOrEmpty(customizationSetName) && customizationSetName.ToLower(CultureInfo.InvariantCulture) != "default")
            {
                text = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\systemNames.txt";
            }
            if (!File.Exists(text))
            {
                text = applicationStartupPath + "\\systemNames.txt";
            }
            try
            {
                if (!File.Exists(text))
                {
                    throw new ApplicationException("Missing file: " + text);
                }
                FileStream fileStream = File.OpenRead(text);
                StreamReader streamReader = new StreamReader(fileStream);
                while (!streamReader.EndOfStream)
                {
                    num++;
                    string text2 = streamReader.ReadLine();
                    if (string.IsNullOrEmpty(text2) || !(text2.Trim() != string.Empty) || !(text2.Trim().Substring(0, 1) != "'"))
                    {
                        continue;
                    }
                    int num2 = 0;
                    int num3 = 0;
                    while (num3 >= 0)
                    {
                        num3 = text2.IndexOf(",", num2);
                        string empty = string.Empty;
                        empty = ((num3 < 0) ? text2.Substring(num2, text2.Length - num2) : text2.Substring(num2, num3 - num2));
                        empty = empty.Trim();
                        if (!string.IsNullOrEmpty(empty))
                        {
                            SystemNames.Add(empty);
                            SystemNamesUsedPlain.Add(item: false);
                            SystemNamesUsedAlternative.Add(item: false);
                        }
                        num2 = num3 + 1;
                    }
                }
                streamReader.Close();
                fileStream.Close();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error at line " + num + " reading file " + text);
            }
        }

        public bool AssignSystemName(Habitat habitat, int PlanetCount)
        {
            string empty = string.Empty;
            if (PlanetCount <= 0)
            {
                empty = ((habitat.Type != HabitatType.BlackHole) ? GenerateCodeName() : GenerateBlackHoleName());
            }
            else
            {
                int num = 0;
                int index = Rnd.Next(0, SystemNames.Count);
                while (SystemNamesUsedPlain[index] && num < 100)
                {
                    index = Rnd.Next(0, SystemNames.Count);
                    num++;
                }
                if (SystemNamesUsedPlain[index])
                {
                    if (SystemNamesUsedAlternative[index])
                    {
                        return false;
                    }
                    empty = SystemNames[index];
                    switch (Rnd.Next(0, 4))
                    {
                        case 0:
                            empty += " Major";
                            break;
                        case 1:
                            empty += " Minor";
                            break;
                        case 2:
                            empty += " Junction";
                            break;
                        case 3:
                            empty += " Prime";
                            break;
                    }
                    SystemNamesUsedAlternative[index] = true;
                }
                else
                {
                    empty = SystemNames[index];
                    SystemNamesUsedPlain[index] = true;
                }
            }
            habitat.Name = empty;
            return true;
        }

        public static double CalculateDistanceFactor(double distance)
        {
            return Math.Max(1000000000.0, Math.Pow(distance, 1.8)) / 1000000000.0;
        }

        public double GetRefactorForEmpire(Empire requestingEmpire, Empire offeringEmpire)
        {
            double num = 1.0;
            int num2 = 0;
            if (requestingEmpire.PirateEmpireBaseHabitat == null && offeringEmpire.PirateEmpireBaseHabitat == null)
            {
                EmpireEvaluation empireEvaluation = offeringEmpire.ObtainEmpireEvaluation(requestingEmpire);
                num2 = empireEvaluation.OverallAttitude;
            }
            else
            {
                PirateRelation pirateRelation = offeringEmpire.ObtainPirateRelation(requestingEmpire);
                num2 = (int)pirateRelation.Evaluation;
            }
            num = ((num2 > 0) ? Math.Min(1.0, 10.0 / (double)Math.Min(50, num2)) : ((num2 != 0) ? Math.Max(1.0, Math.Abs((double)Math.Max(-50, num2)) / 10.0) : 1.0));
            double num3 = 0.7 * ((5.0 + (5.0 - DifficultyLevel)) / 10.0);
            if (requestingEmpire == PlayerEmpire)
            {
                num /= num3;
            }
            return num;
        }

        public int RefactorValueForEmpire(int value, Empire requestingEmpire, Empire offeringEmpire)
        {
            return BaconGalaxy.RefactorValueForEmpire(this, value, requestingEmpire, offeringEmpire);
        }

        public double GetMoneyRate()
        {
            return TotalMoneyInGalaxy / 1000000.0;
        }

        public int ValueMoney(double moneyAmount)
        {
            return (int)moneyAmount;
        }

        public int ValueColonyForEmpire(Habitat colony, Empire empire)
        {
            long num = -1L;
            long num2 = empire.DetermineColonizationValue(colony);
            long num3 = (long)((double)colony.StrategicValue * 1.5);
            num = num3 + num2 * 100;
            if (empire == PlayerEmpire)
            {
                num = (long)((double)num * (PlayerEmpire.DifficultyLevel * PlayerEmpire.DifficultyLevel));
            }
            num = (long)((double)num * GetMoneyRate());
            if (num > 1073741823)
            {
                num = 1073741823L;
            }
            return (int)num;
        }

        public int ValueBaseForEmpire(BuiltObject station, Empire empire)
        {
            int result = -1;
            if (station.Role == BuiltObjectRole.Base)
            {
                double num = 1.0;
                switch (station.SubRole)
                {
                    case BuiltObjectSubRole.GasMiningStation:
                    case BuiltObjectSubRole.MiningStation:
                        {
                            Habitat parentHabitat2 = station.ParentHabitat;
                            if (parentHabitat2 != null)
                            {
                                num = empire.DetermineResourceValue(parentHabitat2) * 2000.0;
                            }
                            break;
                        }
                    case BuiltObjectSubRole.EnergyResearchStation:
                    case BuiltObjectSubRole.WeaponsResearchStation:
                    case BuiltObjectSubRole.HighTechResearchStation:
                        {
                            double num3 = station.ResearchEnergy + station.ResearchHighTech + station.ResearchWeapons;
                            num = Math.Max(100.0, num3 / 5.0);
                            break;
                        }
                    case BuiltObjectSubRole.MonitoringStation:
                        num = 20000.0;
                        break;
                    case BuiltObjectSubRole.DefensiveBase:
                        num = 5000.0;
                        break;
                    case BuiltObjectSubRole.GenericBase:
                        num = 5000.0;
                        if (station.Design != null && station.Design.Name.ToLower(CultureInfo.InvariantCulture).Contains(TextResolver.GetText("Research").ToLower(CultureInfo.InvariantCulture)))
                        {
                            double num2 = station.ResearchEnergy + station.ResearchHighTech + station.ResearchWeapons;
                            num = Math.Max(100.0, num2 / 5.0);
                        }
                        else if (station.SensorLongRange > 0)
                        {
                            num = 20000.0;
                        }
                        break;
                    case BuiltObjectSubRole.Outpost:
                    case BuiltObjectSubRole.SmallSpacePort:
                    case BuiltObjectSubRole.MediumSpacePort:
                    case BuiltObjectSubRole.LargeSpacePort:
                        {
                            num = 15000.0;
                            Habitat parentHabitat = station.ParentHabitat;
                            if (parentHabitat != null)
                            {
                                num = Math.Max(num, (double)parentHabitat.StrategicValue * 0.5);
                            }
                            break;
                        }
                    case BuiltObjectSubRole.ResortBase:
                        num = 15000.0;
                        break;
                }
                double num4 = empire.DetermineProximityFromCapital(station);
                result = (int)(num * num4);
                int val = (int)(1000000.0 * ((double)station.AnnualSupportCost / TotalMoneyInGalaxy));
                if (empire == PlayerEmpire)
                {
                    result = (int)((double)result * (PlayerEmpire.DifficultyLevel * PlayerEmpire.DifficultyLevel));
                }
                result = Math.Max(result, val);
                result = (int)((double)result * GetMoneyRate());
            }
            return result;
        }

        public int ValueTerritoryMapForEmpire(Empire mapEmpire, Empire requestingEmpire)
        {
            int num = -1;
            int num2 = 0;
            HabitatList habitatList = mapEmpire.DetermineEmpireSystems(mapEmpire, mustOwnColonies: true);
            foreach (Habitat item in habitatList)
            {
                if (!requestingEmpire.CheckSystemExplored(item.SystemIndex))
                {
                    num2 += Systems[item.SystemIndex].PlanetCount + Systems[item.SystemIndex].MoonCount;
                }
            }
            num = num2 * 100;
            if (requestingEmpire == PlayerEmpire)
            {
                num = (int)((double)num * (PlayerEmpire.DifficultyLevel * PlayerEmpire.DifficultyLevel));
            }
            return num;
        }

        public void MergeGalaxyMap(Empire giver, Empire receiver)
        {
            if (giver == null || receiver == null || giver.ResourceMap == null || receiver.ResourceMap == null || giver.SystemVisibility == null || receiver.SystemVisibility == null || giver.KnownGalaxyLocations == null || receiver.KnownGalaxyLocations == null)
            {
                return;
            }
            receiver.ResourceMap.MergeMap(giver.ResourceMap._ResourcesKnown);
            for (int i = 0; i < giver.SystemVisibility.Count; i++)
            {
                SystemVisibilityStatus status = giver.SystemVisibility[i].Status;
                if (status != SystemVisibilityStatus.Explored && status != SystemVisibilityStatus.Visible)
                {
                    continue;
                }
                SystemVisibilityStatus status2 = receiver.SystemVisibility[i].Status;
                if (status2 != SystemVisibilityStatus.Unexplored && status2 != 0)
                {
                    continue;
                }
                receiver.SystemVisibility[i].Status = SystemVisibilityStatus.Explored;
                SystemInfo systemInfo = Systems[receiver.SystemVisibility[i].SystemStar];
                if (systemInfo != null)
                {
                    if (systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire != null)
                    {
                        if (receiver.PirateEmpireBaseHabitat != null)
                        {
                            PirateRelation pirateRelation = receiver.ObtainPirateRelation(systemInfo.DominantEmpire.Empire);
                            if (pirateRelation.Type == PirateRelationType.NotMet)
                            {
                                receiver.ChangePirateRelation(systemInfo.DominantEmpire.Empire, PirateRelationType.None, CurrentStarDate);
                                string description = string.Format(TextResolver.GetText("Empire Contact From Galaxy Map"), receiver.Name);
                                systemInfo.DominantEmpire.Empire.SendMessageToEmpire(systemInfo.DominantEmpire.Empire, EmpireMessageType.EmpireDiscovered, receiver, description);
                            }
                        }
                        else
                        {
                            DiplomaticRelation diplomaticRelation = receiver.ObtainDiplomaticRelation(systemInfo.DominantEmpire.Empire);
                            if (diplomaticRelation.Type == DiplomaticRelationType.NotMet)
                            {
                                receiver.ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.None);
                                string description2 = string.Format(TextResolver.GetText("Empire Contact From Galaxy Map"), receiver.Name);
                                systemInfo.DominantEmpire.Empire.SendMessageToEmpire(systemInfo.DominantEmpire.Empire, EmpireMessageType.EmpireDiscovered, receiver, description2);
                            }
                        }
                    }
                    if (systemInfo.OtherEmpires != null && systemInfo.OtherEmpires.Count > 0)
                    {
                        for (int j = 0; j < systemInfo.OtherEmpires.Count; j++)
                        {
                            EmpireSystemSummary empireSystemSummary = systemInfo.OtherEmpires[j];
                            if (empireSystemSummary == null || empireSystemSummary.Empire == null)
                            {
                                continue;
                            }
                            if (receiver.PirateEmpireBaseHabitat != null)
                            {
                                PirateRelation pirateRelation2 = receiver.ObtainPirateRelation(empireSystemSummary.Empire);
                                if (pirateRelation2.Type == PirateRelationType.NotMet)
                                {
                                    receiver.ChangePirateRelation(systemInfo.DominantEmpire.Empire, PirateRelationType.None, CurrentStarDate);
                                    string description3 = string.Format(TextResolver.GetText("Empire Contact From Galaxy Map"), receiver.Name);
                                    systemInfo.DominantEmpire.Empire.SendMessageToEmpire(systemInfo.DominantEmpire.Empire, EmpireMessageType.EmpireDiscovered, receiver, description3);
                                }
                            }
                            else
                            {
                                DiplomaticRelation diplomaticRelation2 = receiver.ObtainDiplomaticRelation(empireSystemSummary.Empire);
                                if (diplomaticRelation2.Type == DiplomaticRelationType.NotMet)
                                {
                                    receiver.ChangeDiplomaticRelation(diplomaticRelation2, DiplomaticRelationType.None);
                                    string description4 = string.Format(TextResolver.GetText("Empire Contact From Galaxy Map"), receiver.Name);
                                    systemInfo.DominantEmpire.Empire.SendMessageToEmpire(systemInfo.DominantEmpire.Empire, EmpireMessageType.EmpireDiscovered, receiver, description4);
                                }
                            }
                        }
                    }
                }
                if (!receiver.SystemsVisible.Contains(receiver.SystemVisibility[i].SystemStar))
                {
                    receiver.SystemsVisible.Add(receiver.SystemVisibility[i].SystemStar);
                }
            }
            for (int k = 0; k < giver.KnownGalaxyLocations.Count; k++)
            {
                GalaxyLocation item = giver.KnownGalaxyLocations[k];
                if (!receiver.KnownGalaxyLocations.Contains(item))
                {
                    receiver.KnownGalaxyLocations.Add(item);
                }
            }
            if (giver.KnownPirateBases != null && receiver.KnownPirateBases != null)
            {
                for (int l = 0; l < giver.KnownPirateBases.Count; l++)
                {
                    BuiltObject builtObject = giver.KnownPirateBases[l];
                    if (builtObject != null && !builtObject.HasBeenDestroyed && !receiver.KnownPirateBases.Contains(builtObject))
                    {
                        receiver.KnownPirateBases.Add(builtObject);
                    }
                }
            }
            if (receiver == PlayerEmpire)
            {
                ReviewEmpireTerritory(onlySystems: true);
            }
        }

        public void GiveTerritoryMap(Empire giver, Empire receiver)
        {
            if (giver == null || receiver == null)
            {
                return;
            }
            HabitatList habitatList = giver.DetermineEmpireSystems(giver);
            foreach (Habitat item in habitatList)
            {
                HabitatList habitatList2 = DetermineHabitatsInSystem(item);
                foreach (Habitat item2 in habitatList2)
                {
                    _ = item2;
                    SystemVisibilityStatus status = receiver.SystemVisibility[item.SystemIndex].Status;
                    if (status != SystemVisibilityStatus.Visible)
                    {
                        receiver.SetSystemVisibility(item, SystemVisibilityStatus.Explored);
                    }
                }
            }
            if (giver.PirateEmpireBaseHabitat != null && giver.SpacePorts != null && receiver.KnownPirateBases != null)
            {
                for (int i = 0; i < giver.SpacePorts.Count; i++)
                {
                    BuiltObject builtObject = giver.SpacePorts[i];
                    if (builtObject == null || builtObject.HasBeenDestroyed)
                    {
                        continue;
                    }
                    if (builtObject.NearestSystemStar != null)
                    {
                        SystemVisibilityStatus status2 = receiver.SystemVisibility[builtObject.NearestSystemStar.SystemIndex].Status;
                        if (status2 != SystemVisibilityStatus.Visible)
                        {
                            receiver.SetSystemVisibility(builtObject.NearestSystemStar, SystemVisibilityStatus.Explored);
                        }
                    }
                    if (!receiver.KnownPirateBases.Contains(builtObject))
                    {
                        receiver.KnownPirateBases.Add(builtObject);
                    }
                }
            }
            if (receiver == PlayerEmpire)
            {
                ReviewEmpireTerritory(onlySystems: true);
            }
        }

        public void GiveTradeableItem(Empire giver, Empire receiver, TradeableItem item, TradeableItemList exchangedItems)
        {
            DiplomaticRelation diplomaticRelation = null;
            DiplomaticRelation diplomaticRelation2 = null;
            PirateRelation pirateRelation = null;
            switch (item.Type)
            {
                case TradeableItemType.Money:
                    {
                        double num2 = 0.0;
                        if (item.Item is double)
                        {
                            num2 = (double)item.Item;
                        }
                        giver.StateMoney -= num2;
                        bool flag = false;
                        if (exchangedItems != null && exchangedItems.Count > 0 && (exchangedItems.ContainsType(TradeableItemType.ContactEmpire) || exchangedItems.ContainsType(TradeableItemType.IndependentColonyLocation) || exchangedItems.ContainsType(TradeableItemType.SecretLocation) || exchangedItems.ContainsType(TradeableItemType.SystemMap)))
                        {
                            flag = true;
                        }
                        giver.PirateEconomy.PerformExpense(num2, PirateExpenseType.Undefined, CurrentStarDate);
                        receiver.StateMoney += num2;
                        if (flag)
                        {
                            receiver.PirateEconomy.PerformIncome(num2, PirateIncomeType.SellInfo, CurrentStarDate);
                        }
                        else
                        {
                            receiver.PirateEconomy.PerformIncome(num2, PirateIncomeType.Undefined, CurrentStarDate);
                        }
                        break;
                    }
                case TradeableItemType.Base:
                    {
                        BuiltObject builtObject = null;
                        if (item.Item is BuiltObject)
                        {
                            builtObject = (BuiltObject)item.Item;
                        }
                        if (builtObject != null)
                        {
                            receiver.TakeOwnershipOfBuiltObject(builtObject, receiver, setDesignAsObsolete: true);
                        }
                        break;
                    }
                case TradeableItemType.Colony:
                    {
                        Habitat habitat2 = null;
                        if (item.Item is Habitat)
                        {
                            habitat2 = (Habitat)item.Item;
                        }
                        if (habitat2 != null)
                        {
                            receiver.TakeOwnershipOfColony(habitat2, receiver);
                        }
                        break;
                    }
                case TradeableItemType.TerritoryMap:
                    GiveTerritoryMap(giver, receiver);
                    break;
                case TradeableItemType.GalaxyMap:
                    MergeGalaxyMap(giver, receiver);
                    break;
                case TradeableItemType.ContactEmpire:
                    {
                        if (!(item.Item is Empire))
                        {
                            break;
                        }
                        Empire empire3 = (Empire)item.Item;
                        if (receiver.PirateEmpireBaseHabitat != null && empire3 != null)
                        {
                            pirateRelation = receiver.ObtainPirateRelation(empire3);
                            if (pirateRelation.Type == PirateRelationType.NotMet)
                            {
                                pirateRelation.Type = PirateRelationType.None;
                            }
                            string title = string.Format(TextResolver.GetText("Inform Empire Their Contact Details Sold Title"), receiver.Name);
                            string description = string.Format(TextResolver.GetText("Inform Empire Their Contact Details Sold"), giver.Name, receiver.Name);
                            empire3.SendMessageToEmpireWithTitle(empire3, EmpireMessageType.GeneralNeutralEvent, null, description, title);
                            PirateRelation pirateRelation2 = empire3.ObtainPirateRelation(receiver);
                            if (pirateRelation2.Type == PirateRelationType.NotMet)
                            {
                                pirateRelation2.Type = PirateRelationType.None;
                            }
                        }
                        else
                        {
                            diplomaticRelation = receiver.ObtainDiplomaticRelation(empire3);
                            if (diplomaticRelation.Type == DiplomaticRelationType.NotMet)
                            {
                                diplomaticRelation.Type = DiplomaticRelationType.None;
                            }
                            string title2 = string.Format(TextResolver.GetText("Inform Empire Their Contact Details Sold Title"), receiver.Name);
                            string description2 = string.Format(TextResolver.GetText("Inform Empire Their Contact Details Sold"), giver.Name, receiver.Name);
                            empire3.SendMessageToEmpireWithTitle(empire3, EmpireMessageType.GeneralNeutralEvent, null, description2, title2);
                            DiplomaticRelation diplomaticRelation5 = empire3.ObtainDiplomaticRelation(receiver);
                            if (diplomaticRelation5.Type == DiplomaticRelationType.NotMet)
                            {
                                diplomaticRelation5.Type = DiplomaticRelationType.None;
                            }
                        }
                        break;
                    }
                case TradeableItemType.SystemMap:
                    if (item.Item is Habitat)
                    {
                        Habitat systemStar = (Habitat)item.Item;
                        if (receiver.CheckSystemVisibilityStatus(systemStar) == SystemVisibilityStatus.Unexplored)
                        {
                            receiver.SetSystemVisibility(systemStar, SystemVisibilityStatus.Explored);
                        }
                    }
                    break;
                case TradeableItemType.IndependentColonyLocation:
                    if (item.Item is Habitat)
                    {
                        Habitat habitat3 = (Habitat)item.Item;
                        Habitat systemStar3 = DetermineHabitatSystemStar(habitat3);
                        if (receiver.CheckSystemVisibilityStatus(systemStar3) == SystemVisibilityStatus.Unexplored)
                        {
                            receiver.SetSystemVisibility(systemStar3, SystemVisibilityStatus.Explored);
                        }
                        receiver.AddLocationHint(new Point((int)habitat3.Xpos, (int)habitat3.Ypos));
                    }
                    break;
                case TradeableItemType.SecretLocation:
                    if (item.Item is GalaxyLocation)
                    {
                        GalaxyLocation galaxyLocation = (GalaxyLocation)item.Item;
                        if (!receiver.KnownGalaxyLocations.Contains(galaxyLocation))
                        {
                            receiver.KnownGalaxyLocations.Add(galaxyLocation);
                        }
                        receiver.AddLocationHint(new Point((int)galaxyLocation.Xpos, (int)galaxyLocation.Ypos));
                    }
                    else if (item.Item is Habitat)
                    {
                        Habitat habitat = (Habitat)item.Item;
                        Habitat systemStar2 = DetermineHabitatSystemStar(habitat);
                        if (receiver.CheckSystemVisibilityStatus(systemStar2) == SystemVisibilityStatus.Unexplored)
                        {
                            receiver.SetSystemVisibility(systemStar2, SystemVisibilityStatus.Explored);
                        }
                        receiver.AddLocationHint(new Point((int)habitat.Xpos, (int)habitat.Ypos));
                    }
                    break;
                case TradeableItemType.ResearchProject:
                    {
                        ResearchNode researchNode = null;
                        if (item.Item is ResearchNode)
                        {
                            researchNode = (ResearchNode)item.Item;
                        }
                        if (researchNode != null)
                        {
                            int num = receiver.Research.ResearchQueueEnergy.IndexOf(researchNode);
                            if (num >= 0)
                            {
                                receiver.Research.ResearchQueueEnergy.RemoveAt(num);
                            }
                            num = receiver.Research.ResearchQueueHighTech.IndexOf(researchNode);
                            if (num >= 0)
                            {
                                receiver.Research.ResearchQueueHighTech.RemoveAt(num);
                            }
                            num = receiver.Research.ResearchQueueWeapons.IndexOf(researchNode);
                            if (num >= 0)
                            {
                                receiver.Research.ResearchQueueWeapons.RemoveAt(num);
                            }
                            num = receiver.Research.TechTree.IndexOf(researchNode);
                            if (num >= 0)
                            {
                                receiver.DoResearchBreakthrough(receiver.Research.TechTree[num], selfResearched: false, blockMessages: true, suppressUpdate: true);
                            }
                            receiver.Research.Update(receiver.DominantRace);
                        }
                        break;
                    }
                case TradeableItemType.AdoptGovernmentStyle:
                    if (giver.PirateEmpireBaseHabitat == null)
                    {
                        GovernmentAttributes governmentAttributes = null;
                        if (item.Item is GovernmentAttributes)
                        {
                            governmentAttributes = (GovernmentAttributes)item.Item;
                        }
                        if (governmentAttributes != null)
                        {
                            giver.HaveRevolution(giver.DominantRace, governmentAttributes.GovernmentId);
                        }
                    }
                    break;
                case TradeableItemType.DeclareWarOther:
                    {
                        Empire empire4 = null;
                        if (item.Item is Empire)
                        {
                            empire4 = (Empire)item.Item;
                        }
                        if (empire4 != null && giver.PirateEmpireBaseHabitat == null && empire4.PirateEmpireBaseHabitat == null)
                        {
                            giver.DeclareWar(empire4, receiver);
                        }
                        break;
                    }
                case TradeableItemType.EndWar:
                    if (giver.PirateEmpireBaseHabitat == null && receiver.PirateEmpireBaseHabitat == null)
                    {
                        diplomaticRelation = giver.ObtainDiplomaticRelation(receiver);
                        giver.ResetAttitudeLevelsAtEndOfWar(diplomaticRelation);
                        diplomaticRelation.Type = DiplomaticRelationType.None;
                        diplomaticRelation.LastDiplomacyTradeOfferDate = CurrentStarDate;
                        diplomaticRelation2 = receiver.ObtainDiplomaticRelation(giver);
                        diplomaticRelation2.Type = DiplomaticRelationType.None;
                        diplomaticRelation2.LastDiplomacyTradeOfferDate = CurrentStarDate;
                        giver.ProcessEndOfWarWithEmpire(receiver);
                        receiver.ProcessEndOfWarWithEmpire(giver);
                        giver.ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.None);
                        giver.SendNewsBroadcastWarStartEnd(diplomaticRelation);
                    }
                    break;
                case TradeableItemType.LiftTradeSanctions:
                    if (giver.PirateEmpireBaseHabitat == null && receiver.PirateEmpireBaseHabitat == null)
                    {
                        diplomaticRelation = giver.ObtainDiplomaticRelation(receiver);
                        if (diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions)
                        {
                            giver.ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.None);
                            giver.SendMessageToEmpire(receiver, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.None, TextResolver.GetText("Our trade sanctions against you have been lifted - we will now resume trade."), ResolveDescription(DiplomaticRelationType.TradeSanctions));
                            giver.CancelBlockades(receiver);
                            receiver.CancelBlockades(giver);
                        }
                    }
                    break;
                case TradeableItemType.EndWarOther:
                    {
                        Empire empire5 = null;
                        if (item.Item is Empire)
                        {
                            empire5 = (Empire)item.Item;
                        }
                        if (empire5 != null && giver.PirateEmpireBaseHabitat == null && empire5.PirateEmpireBaseHabitat == null)
                        {
                            diplomaticRelation = giver.ObtainDiplomaticRelation(empire5);
                            if (diplomaticRelation.Type == DiplomaticRelationType.War)
                            {
                                giver.ResetAttitudeLevelsAtEndOfWar(diplomaticRelation);
                                diplomaticRelation.Type = DiplomaticRelationType.None;
                                diplomaticRelation.LastDiplomacyTradeOfferDate = CurrentStarDate;
                                diplomaticRelation2 = empire5.ObtainDiplomaticRelation(giver);
                                diplomaticRelation2.Type = DiplomaticRelationType.None;
                                diplomaticRelation2.LastDiplomacyTradeOfferDate = CurrentStarDate;
                                giver.ProcessEndOfWarWithEmpire(empire5);
                                empire5.ProcessEndOfWarWithEmpire(giver);
                                giver.ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.None);
                                giver.SendMessageToEmpire(empire5, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.None, TextResolver.GetText("We are ending our war with you"), ResolveDescription(DiplomaticRelationType.War));
                                giver.SendNewsBroadcastWarStartEnd(diplomaticRelation);
                            }
                        }
                        break;
                    }
                case TradeableItemType.InitiateTradeSanctionsOther:
                    {
                        Empire empire2 = null;
                        if (item.Item is Empire)
                        {
                            empire2 = (Empire)item.Item;
                        }
                        if (empire2 != null && giver.PirateEmpireBaseHabitat == null && empire2.PirateEmpireBaseHabitat == null)
                        {
                            DiplomaticRelation diplomaticRelation4 = giver.DiplomaticRelations[empire2];
                            if (diplomaticRelation4 != null)
                            {
                                giver.ChangeDiplomaticRelation(diplomaticRelation4, DiplomaticRelationType.TradeSanctions);
                                giver.SendMessageToEmpire(empire2, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.TradeSanctions, TextResolver.GetText("Our blockade is perfectly legal..."), "PERSUADED");
                            }
                        }
                        break;
                    }
                case TradeableItemType.LiftTradeSanctionsOther:
                    {
                        Empire empire = null;
                        if (item.Item is Empire)
                        {
                            empire = (Empire)item.Item;
                        }
                        if (empire != null && giver.PirateEmpireBaseHabitat == null && empire.PirateEmpireBaseHabitat == null)
                        {
                            DiplomaticRelation diplomaticRelation3 = giver.DiplomaticRelations[empire];
                            if (diplomaticRelation3 != null)
                            {
                                giver.ChangeDiplomaticRelation(diplomaticRelation3, DiplomaticRelationType.None);
                                giver.SendMessageToEmpire(empire, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.None, TextResolver.GetText("Our trade sanctions against you have been lifted - we will now resume trade."), ResolveDescription(DiplomaticRelationType.TradeSanctions));
                                giver.CancelBlockades(empire);
                                empire.CancelBlockades(giver);
                            }
                        }
                        break;
                    }
                case TradeableItemType.ThreatenWar:
                case TradeableItemType.ThreatenTradeSanctions:
                    break;
            }
        }

        public int UpdateValueDeclareWarOnEmpire(int value, Empire requester, Empire giver, Empire targetEmpire)
        {
            DiplomaticRelation diplomaticRelation = giver.ObtainDiplomaticRelation(targetEmpire);
            DiplomaticRelation diplomaticRelation2 = giver.ObtainDiplomaticRelation(requester);
            if ((diplomaticRelation2.Type == DiplomaticRelationType.MutualDefensePact || (diplomaticRelation2.Type == DiplomaticRelationType.Protectorate && diplomaticRelation2.Initiator == giver)) && diplomaticRelation.Type != DiplomaticRelationType.MutualDefensePact && diplomaticRelation.Type != DiplomaticRelationType.Protectorate)
            {
                value = 0;
            }
            return value;
        }

        public TradeableItemList ResolveTradeableItemsDiplomacy(Empire giver, Empire receiver, bool refactorValuesForEmpire)
        {
            TradeableItemList tradeableItemList = new TradeableItemList();
            EmpireList empireList = new EmpireList();
            EmpireList empireList2 = new EmpireList();
            for (int i = 0; i < giver.DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = giver.DiplomaticRelations[i];
                if (diplomaticRelation.OtherEmpire != receiver && diplomaticRelation.Initiator == giver && !diplomaticRelation.Locked)
                {
                    if (diplomaticRelation.Type == DiplomaticRelationType.War)
                    {
                        empireList.Add(diplomaticRelation.OtherEmpire);
                    }
                    else if (diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions)
                    {
                        empireList2.Add(diplomaticRelation.OtherEmpire);
                    }
                }
            }
            for (int j = 0; j < receiver.DiplomaticRelations.Count; j++)
            {
                DiplomaticRelation diplomaticRelation2 = receiver.DiplomaticRelations[j];
                if (diplomaticRelation2.OtherEmpire == giver)
                {
                    continue;
                }
                DiplomaticRelation diplomaticRelation3 = giver.DiplomaticRelations[diplomaticRelation2.OtherEmpire];
                if (diplomaticRelation3 != null && diplomaticRelation3.Type != 0 && diplomaticRelation3.Type != DiplomaticRelationType.War && diplomaticRelation2.Type == DiplomaticRelationType.War && !diplomaticRelation3.Locked)
                {
                    int value = ValueDeclareWarOnEmpire(giver, diplomaticRelation2.OtherEmpire);
                    value = UpdateValueDeclareWarOnEmpire(value, receiver, giver, diplomaticRelation2.OtherEmpire);
                    if (value >= 0 && refactorValuesForEmpire)
                    {
                        value = RefactorValueForEmpire(value, receiver, giver);
                    }
                    tradeableItemList.Add(new TradeableItem(TradeableItemType.DeclareWarOther, diplomaticRelation2.OtherEmpire, value));
                }
            }
            for (int k = 0; k < receiver.DiplomaticRelations.Count; k++)
            {
                DiplomaticRelation diplomaticRelation4 = receiver.DiplomaticRelations[k];
                if (diplomaticRelation4.OtherEmpire == giver)
                {
                    continue;
                }
                DiplomaticRelation diplomaticRelation5 = giver.DiplomaticRelations[diplomaticRelation4.OtherEmpire];
                if (diplomaticRelation5 != null && diplomaticRelation5.Type != 0 && diplomaticRelation5.Type != DiplomaticRelationType.TradeSanctions && diplomaticRelation5.Type != DiplomaticRelationType.War && diplomaticRelation4.Type == DiplomaticRelationType.TradeSanctions && !diplomaticRelation5.Locked)
                {
                    int num = ValueInitiateTradeSanctionsAgainstEmpire(giver, diplomaticRelation4.OtherEmpire);
                    if (num >= 0 && refactorValuesForEmpire)
                    {
                        num = RefactorValueForEmpire(num, receiver, giver);
                    }
                    tradeableItemList.Add(new TradeableItem(TradeableItemType.InitiateTradeSanctionsOther, diplomaticRelation4.OtherEmpire, num));
                }
            }
            foreach (Empire item in empireList)
            {
                int num2 = ValueEndWarWithEmpire(receiver, giver, item);
                if (num2 >= 0 && refactorValuesForEmpire)
                {
                    num2 = RefactorValueForEmpire(num2, receiver, giver);
                }
                if (num2 >= 0)
                {
                    tradeableItemList.Add(new TradeableItem(TradeableItemType.EndWarOther, item, num2));
                }
            }
            foreach (Empire item2 in empireList2)
            {
                int num3 = ValueLiftTradeSanctionsAgainstEmpire(receiver, giver, item2);
                if (num3 >= 0 && refactorValuesForEmpire)
                {
                    num3 = RefactorValueForEmpire(num3, receiver, giver);
                }
                if (num3 >= 0)
                {
                    tradeableItemList.Add(new TradeableItem(TradeableItemType.LiftTradeSanctionsOther, item2, num3));
                }
            }
            DiplomaticRelation diplomaticRelation6 = giver.ObtainDiplomaticRelation(receiver);
            if (diplomaticRelation6.Type == DiplomaticRelationType.War && !diplomaticRelation6.Locked)
            {
                int num4 = ValueEndWarAgainstUs(giver, receiver);
                if (num4 >= 0 && refactorValuesForEmpire)
                {
                    num4 = RefactorValueForEmpire(num4, receiver, giver);
                }
                if (num4 >= 0)
                {
                    tradeableItemList.Add(new TradeableItem(TradeableItemType.EndWar, giver, num4));
                }
            }
            else if (diplomaticRelation6.Type == DiplomaticRelationType.TradeSanctions && !diplomaticRelation6.Locked)
            {
                int num5 = ValueLiftTradeSanctionsAgainstUs(giver, receiver);
                if (num5 >= 0 && refactorValuesForEmpire)
                {
                    num5 = RefactorValueForEmpire(num5, receiver, giver);
                }
                if (num5 >= 0)
                {
                    tradeableItemList.Add(new TradeableItem(TradeableItemType.LiftTradeSanctions, receiver, num5));
                }
            }
            if (diplomaticRelation6.Type != DiplomaticRelationType.War && !diplomaticRelation6.Locked)
            {
                int value2 = 0;
                tradeableItemList.Add(new TradeableItem(TradeableItemType.ThreatenWar, receiver, value2));
            }
            if (diplomaticRelation6.Type != DiplomaticRelationType.War && diplomaticRelation6.Type != DiplomaticRelationType.TradeSanctions && !diplomaticRelation6.Locked)
            {
                int value3 = 0;
                tradeableItemList.Add(new TradeableItem(TradeableItemType.ThreatenTradeSanctions, receiver, value3));
            }
            return tradeableItemList;
        }

        public TradeableItemList ResolveTradeableItemsColoniesBases(Empire giver, Empire receiver)
        {
            return ResolveTradeableItemsColoniesBases(giver, receiver, includeNearestColony: false, refactorValuesForEmpire: true);
        }

        public TradeableItemList ResolveTradeableItemsColoniesBases(Empire giver, Empire receiver, bool includeNearestColony, bool refactorValuesForEmpire)
        {
            return BaconGalaxy.ResolveTradeableItemsColoniesBases(this, giver, receiver, refactorValuesForEmpire);
        }

        public TradeableItemList ResolveTradeableItemsResearchProjects(Empire giver, Empire receiver, bool refactorValuesForEmpire, bool includeSpecialTech)
        {
            return ResolveTradeableItemsResearchProjects(giver, receiver, refactorValuesForEmpire, includeSpecialTech, includeWarpColonizationWeapons: true);
        }

        public TradeableItemList ResolveTradeableItemsResearchProjects(Empire giver, Empire receiver, bool refactorValuesForEmpire, bool includeSpecialTech, bool includeWarpColonizationWeapons)
        {
            TradeableItemList tradeableItemList = new TradeableItemList();
            ResearchNodeList researchNodeList = receiver.Research.ResolveMoreAdvancedProjects(giver, includeSpecialTech);
            for (int i = 0; i < researchNodeList.Count; i++)
            {
                ResearchNode researchNode = researchNodeList[i];
                if (!researchNode.SelfResearched)
                {
                    continue;
                }
                bool flag = true;
                if (!includeWarpColonizationWeapons)
                {
                    switch (researchNode.Category)
                    {
                        case ComponentCategoryType.WeaponBeam:
                        case ComponentCategoryType.WeaponTorpedo:
                        case ComponentCategoryType.WeaponArea:
                        case ComponentCategoryType.WeaponPointDefense:
                        case ComponentCategoryType.WeaponIon:
                        case ComponentCategoryType.WeaponGravity:
                        case ComponentCategoryType.AssaultPod:
                        case ComponentCategoryType.Fighter:
                        case ComponentCategoryType.HyperDrive:
                        case ComponentCategoryType.WeaponSuperBeam:
                        case ComponentCategoryType.WeaponSuperArea:
                        case ComponentCategoryType.WeaponSuperTorpedo:
                            flag = false;
                            break;
                    }
                    ResearchAbilityType researchAbilityType = researchNode.ResolveResearchAbilityType();
                    ResearchAbilityType researchAbilityType2 = researchAbilityType;
                    if (researchAbilityType2 == ResearchAbilityType.ColonizeHabitatType)
                    {
                        flag = false;
                    }
                    if (researchNode.CheckAnyComponentTypeMatches(ComponentType.HabitationColonization))
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    int num = ValueResearchProjectForEmpire(researchNode, receiver);
                    double num2 = 0.67 / DifficultyLevel;
                    if (giver == PlayerEmpire)
                    {
                        num = (int)((double)num * num2);
                    }
                    else if (receiver == PlayerEmpire)
                    {
                        num = (int)((double)num / num2);
                    }
                    if (num >= 0 && refactorValuesForEmpire)
                    {
                        num = RefactorValueForEmpire(num, receiver, giver);
                    }
                    if (num > 0)
                    {
                        tradeableItemList.Add(new TradeableItem(TradeableItemType.ResearchProject, researchNode, num));
                    }
                }
            }
            return tradeableItemList;
        }

        public TradeableItemList ResolveTradeableItemsMaps(Empire giver, Empire receiver, bool refactorValuesForEmpire)
        {
            TradeableItemList tradeableItemList = new TradeableItemList();
            int num = ValueTerritoryMapForEmpire(giver, receiver);
            if (num >= 0 && refactorValuesForEmpire)
            {
                num = RefactorValueForEmpire(num, receiver, giver);
            }
            if (num >= 0)
            {
                tradeableItemList.Add(new TradeableItem(TradeableItemType.TerritoryMap, null, num));
            }
            int num2 = ValueGalaxyMapForEmpire(giver, receiver);
            if (num2 >= 0 && refactorValuesForEmpire)
            {
                num2 = RefactorValueForEmpire(num2, receiver, giver);
            }
            if (num2 >= 0)
            {
                tradeableItemList.Add(new TradeableItem(TradeableItemType.GalaxyMap, null, num2));
            }
            return tradeableItemList;
        }

        public TradeableItemList ResolveTradeableItems(Empire giver, Empire receiver, bool includeNearestColony, bool refactorValuesForEmpire)
        {
            return ResolveTradeableItems(giver, receiver, includeNearestColony, refactorValuesForEmpire, includeAllItems: false);
        }

        public TradeableItemList ResolveTradeableItems(Empire giver, Empire receiver, bool includeNearestColony, bool refactorValuesForEmpire, bool includeAllItems)
        {
            TradeableItemList tradeableItemList = new TradeableItemList();
            int num = 0;
            if (giver.PirateEmpireBaseHabitat == null && receiver.PirateEmpireBaseHabitat == null)
            {
                EmpireEvaluation empireEvaluation = giver.ObtainEmpireEvaluation(receiver);
                if (empireEvaluation != null)
                {
                    num = empireEvaluation.OverallAttitude;
                }
            }
            else
            {
                PirateRelation pirateRelation = giver.ObtainPirateRelation(receiver);
                num = (int)pirateRelation.Evaluation;
            }
            int value = ValueMoney(10.0);
            tradeableItemList.Add(new TradeableItem(TradeableItemType.Money, 10.0, value));
            value = ValueMoney(100.0);
            tradeableItemList.Add(new TradeableItem(TradeableItemType.Money, 100.0, value));
            value = ValueMoney(1000.0);
            tradeableItemList.Add(new TradeableItem(TradeableItemType.Money, 1000.0, value));
            value = ValueMoney(10000.0);
            tradeableItemList.Add(new TradeableItem(TradeableItemType.Money, 10000.0, value));
            value = ValueMoney(100000.0);
            tradeableItemList.Add(new TradeableItem(TradeableItemType.Money, 100000.0, value));
            if (receiver.PirateEmpireBaseHabitat == null)
            {
                tradeableItemList.AddRange(ResolveTradeableItemsColoniesBases(giver, receiver, includeNearestColony, refactorValuesForEmpire));
            }
            if (includeAllItems || num >= TradeTerritoryMapThreshhold)
            {
                tradeableItemList.AddRange(ResolveTradeableItemsMaps(giver, receiver, refactorValuesForEmpire));
            }
            if (AllowTechTrading)
            {
                int num2 = TradeResearchThreshhold;
                if (receiver == PlayerEmpire)
                {
                    num2 = (int)((double)TradeResearchThreshhold * DifficultyLevel);
                }
                if (includeAllItems || num >= num2)
                {
                    int num3 = TradeResearchSpecialThreshhold;
                    if (receiver == PlayerEmpire)
                    {
                        num3 = (int)((double)TradeResearchSpecialThreshhold * DifficultyLevel);
                    }
                    bool includeSpecialTech = false;
                    if (includeAllItems || num >= num3)
                    {
                        includeSpecialTech = true;
                    }
                    tradeableItemList.AddRange(ResolveTradeableItemsResearchProjects(giver, receiver, refactorValuesForEmpire, includeSpecialTech));
                }
            }
            if (giver.PirateEmpireBaseHabitat == null && receiver.PirateEmpireBaseHabitat == null)
            {
                tradeableItemList.AddRange(ResolveTradeableItemsDiplomacy(giver, receiver, refactorValuesForEmpire));
            }
            if (giver.PirateEmpireBaseHabitat != null)
            {
                tradeableItemList.AddRange(ResolveTradeableItemsPirateInfo(giver, receiver, refactorValuesForEmpire));
            }
            return tradeableItemList;
        }

        public TradeableItemList ResolveTradeableItemsPirateInfo(Empire pirateGiver, Empire receiver, bool refactorValuesForEmpire)
        {
            TradeableItemList tradeableItemList = new TradeableItemList();
            pirateGiver.GenerateSaleableInfoForEmpire(pirateGiver, receiver, out var unmetEmpires, out var unexploredSystems, out var independentColonies, out var ruinHabitats, out var debrisFieldLocations, out var planetDestroyerLocations, out var restrictedAreaLocations);
            for (int i = 0; i < unmetEmpires.Count; i++)
            {
                double value = (double)unmetEmpires[i].TotalColonyStrategicValue / 200.0;
                value = Math.Round(value, 0);
                value = Math.Min(value, 10000.0);
                TradeableItem item = new TradeableItem(TradeableItemType.ContactEmpire, unmetEmpires[i], (int)value);
                tradeableItemList.Add(item);
            }
            for (int j = 0; j < unexploredSystems.Count; j++)
            {
                TradeableItem item2 = new TradeableItem(TradeableItemType.SystemMap, unexploredSystems[j], 2000);
                tradeableItemList.Add(item2);
            }
            for (int k = 0; k < independentColonies.Count; k++)
            {
                TradeableItem item3 = new TradeableItem(TradeableItemType.IndependentColonyLocation, independentColonies[k], 20000);
                tradeableItemList.Add(item3);
            }
            for (int l = 0; l < ruinHabitats.Count; l++)
            {
                TradeableItem item4 = new TradeableItem(TradeableItemType.SecretLocation, ruinHabitats[l], 30000);
                tradeableItemList.Add(item4);
            }
            for (int m = 0; m < debrisFieldLocations.Count; m++)
            {
                TradeableItem item5 = new TradeableItem(TradeableItemType.SecretLocation, debrisFieldLocations[m], 30000);
                tradeableItemList.Add(item5);
            }
            for (int n = 0; n < planetDestroyerLocations.Count; n++)
            {
                TradeableItem item6 = new TradeableItem(TradeableItemType.SecretLocation, planetDestroyerLocations[n], 30000);
                tradeableItemList.Add(item6);
            }
            for (int num = 0; num < restrictedAreaLocations.Count; num++)
            {
                TradeableItem item7 = new TradeableItem(TradeableItemType.SecretLocation, restrictedAreaLocations[num], 30000);
                tradeableItemList.Add(item7);
            }
            return tradeableItemList;
        }

        public int ValueThreatenWar(Empire threateningEmpire, Empire targetEmpire)
        {
            int result = -1;
            if (threateningEmpire != targetEmpire)
            {
                DiplomaticRelation diplomaticRelation = threateningEmpire.ObtainDiplomaticRelation(targetEmpire);
                if (diplomaticRelation.Type != DiplomaticRelationType.War)
                {
                    double val = (double)threateningEmpire.WeightedMilitaryPotency / (double)targetEmpire.WeightedMilitaryPotency;
                    val = Math.Min(Math.Max(val, 0.2), 5.0);
                    result = (int)(val * 10000.0);
                }
            }
            return result;
        }

        public int ValueThreatenTradeSanctions(Empire threateningEmpire, Empire targetEmpire)
        {
            int result = -1;
            if (threateningEmpire != targetEmpire)
            {
                DiplomaticRelation diplomaticRelation = threateningEmpire.ObtainDiplomaticRelation(targetEmpire);
                if (diplomaticRelation.Type != DiplomaticRelationType.War && diplomaticRelation.Type != DiplomaticRelationType.TradeSanctions)
                {
                    double val = (double)threateningEmpire.WeightedMilitaryPotency / (double)targetEmpire.WeightedMilitaryPotency;
                    val = Math.Min(Math.Max(val, 0.2), 5.0);
                    result = (int)(val * 5000.0);
                }
            }
            return result;
        }

        public int ValueResearchProjectForEmpire(ResearchNode project, Empire requestingEmpire)
        {
            long num = -1L;
            if (project != null)
            {
                int num2 = (int)(project.Cost - requestingEmpire.Research.TechTree.GetEquivalent(project).Progress);
                int num3 = 0;
                for (int i = 0; i < Empires.Count; i++)
                {
                    Empire empire = Empires[i];
                    if (empire.Research.TechTree.GetEquivalent(project).IsResearched)
                    {
                        num3++;
                    }
                }
                num = (long)((double)num2 * 3.0);
                if (project.AllowedRaces != null && project.AllowedRaces.Count > 0)
                {
                    num *= 5;
                }
                if (num3 > 1)
                {
                    num = (long)((double)num / Math.Sqrt(num3));
                }
                bool flag = false;
                if (((project.Components != null && project.Components.Count > 0) || (project.ComponentImprovements != null && project.ComponentImprovements.Count > 0)) && (project.Abilities == null || project.Abilities.Count <= 0) && (project.Fighters == null || project.Fighters.Count <= 0) && project.PlanetaryFacility == null)
                {
                    if (project.Components != null && project.Components.Count > 0)
                    {
                        for (int j = 0; j < project.Components.Count; j++)
                        {
                            Component component = project.Components[j];
                            if (component != null && requestingEmpire.DesignSpecifications != null && requestingEmpire.DesignSpecifications.CheckAnyDesignSpecificationsUseComponent(component))
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (project.ComponentImprovements != null && project.ComponentImprovements.Count > 0)
                    {
                        for (int k = 0; k < project.ComponentImprovements.Count; k++)
                        {
                            ComponentImprovement componentImprovement = project.ComponentImprovements[k];
                            if (componentImprovement != null)
                            {
                                Component improvedComponent = componentImprovement.ImprovedComponent;
                                if (improvedComponent != null && requestingEmpire.DesignSpecifications != null && requestingEmpire.DesignSpecifications.CheckAnyDesignSpecificationsUseComponent(improvedComponent))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (requestingEmpire != PlayerEmpire && !flag)
                {
                    num = 0L;
                }
                if (requestingEmpire == PlayerEmpire)
                {
                    num = (long)((double)num * (PlayerEmpire.DifficultyLevel * PlayerEmpire.DifficultyLevel));
                }
                if (num > 1073741823)
                {
                    num = 1073741823L;
                }
            }
            return (int)num;
        }

        public int ValueGalaxyMapForEmpire(Empire mapEmpire, Empire requestingEmpire)
        {
            int num = -1;
            int num2 = 0;
            if (mapEmpire.ResourceMap != null)
            {
                for (int i = 0; i < mapEmpire.SystemVisibility.Count; i++)
                {
                    if (!mapEmpire.CheckSystemExplored(i))
                    {
                        continue;
                    }
                    for (int j = 0; j < Systems[i].Habitats.Count; j++)
                    {
                        if (Systems[i].Habitats[j].Category != HabitatCategoryType.Asteroid && mapEmpire.ResourceMap.CheckResourcesKnown(Systems[i].Habitats[j]) && !requestingEmpire.ResourceMap.CheckResourcesKnown(Systems[i].Habitats[j]))
                        {
                            num2++;
                        }
                    }
                }
            }
            num = num2 * 200;
            if (requestingEmpire == PlayerEmpire)
            {
                num = (int)((double)num * (PlayerEmpire.DifficultyLevel * PlayerEmpire.DifficultyLevel));
            }
            return num;
        }

        public int ValueDeclareWarOnEmpire(Empire empire, Empire targetEmpire)
        {
            long num = -1L;
            DiplomaticRelation diplomaticRelation = empire.ObtainDiplomaticRelation(targetEmpire);
            if (diplomaticRelation != null && diplomaticRelation.Type != DiplomaticRelationType.War && diplomaticRelation.Type != 0)
            {
                double num2 = 1.0;
                empire.ResolveTypicalAttitudeLevel(DiplomaticRelationType.War, out var _, out var upperLevel);
                EmpireEvaluation empireEvaluation = empire.ObtainEmpireEvaluation(targetEmpire);
                if (empireEvaluation != null)
                {
                    num2 = ((empireEvaluation.OverallAttitude <= upperLevel) ? 1.0 : ((double)(empireEvaluation.OverallAttitude - upperLevel)));
                }
                if (diplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement)
                {
                    double num3 = Math.Max(1.0, (double)empire.DominantRace.LoyaltyLevel / 100.0);
                    num2 = num2 * 2.0 * (num3 * num3);
                }
                else if (diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || diplomaticRelation.Type == DiplomaticRelationType.Protectorate)
                {
                    double num4 = Math.Max(1.0, (double)empire.DominantRace.LoyaltyLevel / 100.0);
                    num2 = num2 * 3.5 * (num4 * num4 * num4);
                }
                num2 = Math.Pow(num2, 0.75);
                double num5 = (double)targetEmpire.WeightedMilitaryPotency / (double)empire.WeightedMilitaryPotency;
                if (targetEmpire == PlayerEmpire)
                {
                    num5 /= PlayerEmpire.DifficultyLevel * PlayerEmpire.DifficultyLevel;
                }
                num5 = Math.Min(Math.Max(num5, 0.5), 10.0);
                num = (long)(num2 * num5 * 10000.0);
            }
            num = (long)((double)num * GetMoneyRate());
            if (num > 1073741823)
            {
                num = 1073741823L;
            }
            return (int)num;
        }

        public int ValueInitiateTradeSanctionsAgainstEmpire(Empire empire, Empire targetEmpire)
        {
            long num = -1L;
            DiplomaticRelation diplomaticRelation = empire.ObtainDiplomaticRelation(targetEmpire);
            if (diplomaticRelation != null && diplomaticRelation.Type != DiplomaticRelationType.TradeSanctions && diplomaticRelation.Type != DiplomaticRelationType.War && diplomaticRelation.Type != 0)
            {
                double num2 = 1.0;
                empire.ResolveTypicalAttitudeLevel(DiplomaticRelationType.TradeSanctions, out var _, out var upperLevel);
                EmpireEvaluation empireEvaluation = empire.ObtainEmpireEvaluation(targetEmpire);
                if (empireEvaluation != null)
                {
                    num2 = ((empireEvaluation.OverallAttitude <= upperLevel) ? 1.0 : ((double)(empireEvaluation.OverallAttitude - upperLevel)));
                }
                if (diplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement)
                {
                    double num3 = Math.Max(1.0, (double)empire.DominantRace.LoyaltyLevel / 100.0);
                    num2 = num2 * 2.0 * (num3 * num3);
                }
                else if (diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || diplomaticRelation.Type == DiplomaticRelationType.Protectorate)
                {
                    double num4 = Math.Max(1.0, (double)empire.DominantRace.LoyaltyLevel / 100.0);
                    num2 = num2 * 3.5 * (num4 * num4 * num4);
                }
                num2 = Math.Pow(num2, 0.75);
                double num5 = (double)targetEmpire.WeightedMilitaryPotency / (double)empire.WeightedMilitaryPotency;
                if (targetEmpire == PlayerEmpire)
                {
                    num5 /= PlayerEmpire.DifficultyLevel * PlayerEmpire.DifficultyLevel;
                }
                num5 = Math.Min(Math.Max(num5, 0.5), 10.0);
                num = (long)(num2 * num5 * 4000.0);
            }
            num = (long)((double)num * GetMoneyRate());
            if (num > 1073741823)
            {
                num = 1073741823L;
            }
            return (int)num;
        }

        public int ValueEndWarAgainstUs(Empire attackingEmpire, Empire targetEmpire)
        {
            long num = -1L;
            if (attackingEmpire != targetEmpire)
            {
                DiplomaticRelation diplomaticRelation = attackingEmpire.ObtainDiplomaticRelation(targetEmpire);
                if (diplomaticRelation.Type == DiplomaticRelationType.War)
                {
                    num = 10000L;
                    double winningRatio = 1.0;
                    int loserRawDamageBuiltObject = 0;
                    int loserRawDamageColony = 0;
                    int winnerRawDamageBuiltObject = 0;
                    int winnerRawDamageColony = 0;
                    Empire loser = null;
                    Empire empire = attackingEmpire.DetermineVictorInWar(diplomaticRelation, out winningRatio, out loser, out loserRawDamageBuiltObject, out loserRawDamageColony, out winnerRawDamageBuiltObject, out winnerRawDamageColony);
                    if (empire == attackingEmpire)
                    {
                        double num2 = (double)empire.WeightedMilitaryPotency / (double)loser.WeightedMilitaryPotency;
                        if (loser == PlayerEmpire)
                        {
                            num2 *= PlayerEmpire.DifficultyLevel * PlayerEmpire.DifficultyLevel;
                        }
                        num2 = Math.Min(Math.Max(num2, 0.5), 5.0);
                        num = (long)(num2 * 80000.0 * winningRatio);
                    }
                    else
                    {
                        double val = (double)loser.WeightedMilitaryPotency / (double)empire.WeightedMilitaryPotency;
                        val = Math.Min(Math.Max(val, 0.5), 5.0);
                        num = (long)(val * 40000.0 / winningRatio);
                    }
                }
            }
            if (num > 1073741823)
            {
                num = 1073741823L;
            }
            return (int)num;
        }

    }
}
