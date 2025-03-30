// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResearchNodeDefinitionList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;

namespace DistantWorlds.Types
{
    [Serializable]
    public class ResearchNodeDefinitionList : SyncList<ResearchNodeDefinition>
    {
        public ResearchNodeDefinition[] SortByName()
        {
            ResearchNodeDefinition[] array = this.ToArray();
            string[] keys = new string[array.Length];
            for (int index = 0; index < array.Length; ++index)
                keys[index] = array[index].Name;
            Array.Sort<string, ResearchNodeDefinition>(keys, array);
            return array;
        }

        private bool CheckSequentialIds(ResearchNodeDefinitionList definitions)
        {
            int num = 0;
            for (int index = 0; index < definitions.Count; ++index)
            {
                ResearchNodeDefinition definition = definitions[index];
                if (definition != null)
                {
                    if (definition.ResearchNodeId != num)
                        return false;
                    ++num;
                }
            }
            return true;
        }

        public void LoadFromFile(string filePath, RaceList races)
        {
            this.Clear();
            ResearchNodeDefinitionList nodeDefinitionList = new ResearchNodeDefinitionList();
            List<int>[] intListArray;
            List<bool>[] boolListArray;

            int num1 = 0;
            int projCount;
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)fileStream))
                    {
                        GetProjIdCount(streamReader, out projCount);
                        fileStream.Seek(0, SeekOrigin.Begin);

                        intListArray = new List<int>[projCount];
                        boolListArray = new List<bool>[projCount];
                        for (int index = 0; index < projCount; ++index)
                        {
                            intListArray[index] = new List<int>();
                            boolListArray[index] = new List<bool>();
                        }

                        int researchNodeId = 0;
                        while (!streamReader.EndOfStream)
                        {
                            ++num1;
                            string str1 = streamReader.ReadLine();
                            if (!string.IsNullOrEmpty(str1) && str1.Trim() != string.Empty && str1.Trim().Substring(0, 1) != "'")
                            {
                                int startIndex1 = 0;
                                int num2 = 0;
                                string str2 = string.Empty;
                                int num3 = str1.IndexOf(";", 0);
                                if (num3 > 0)
                                {
                                    str2 = str1.Substring(0, num3 - 1).Trim();
                                    startIndex1 = num3 + 1;
                                }
                                ResearchNodeDefinition nodeById = nodeDefinitionList.FindNodeById(researchNodeId);
                                int num4;
                                int num5;
                                switch (str2.ToLower(CultureInfo.InvariantCulture))
                                {
                                    case "project":
                                        //if (nodeDefinitionList.Count > 1500)
                                        //  throw new ApplicationException("Exceeded maximum research project count in " + filePath + ". Cannot define more than 1500 research projects.");
                                        int result1;
                                        int startIndex2;
                                        try
                                        {
                                            int num6 = str1.IndexOf(",", startIndex1);
                                            if (num6 < 0)
                                                throw new ApplicationException("Could not read ResearchProjectId at line " + num1.ToString() + " of file " + filePath);
                                            if (!int.TryParse(str1.Substring(startIndex1, num6 - startIndex1).Trim(), out result1))
                                                throw new ApplicationException("Could not read ResearchProjectId at line " + num1.ToString() + " of file " + filePath);
                                            startIndex2 = num6 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read ResearchProjectId at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        string name1;
                                        int startIndex3;
                                        try
                                        {
                                            int num7 = str1.IndexOf(",", startIndex2);
                                            if (num7 < 0)
                                                throw new ApplicationException("Could not read Name at line " + num1.ToString() + " of file " + filePath);
                                            name1 = str1.Substring(startIndex2, num7 - startIndex2).Trim();
                                            startIndex3 = num7 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read Name at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        int result2;
                                        int startIndex4;
                                        try
                                        {
                                            int num8 = str1.IndexOf(",", startIndex3);
                                            if (num8 < 0)
                                                throw new ApplicationException("Could not read TechLevel at line " + num1.ToString() + " of file " + filePath);
                                            if (!int.TryParse(str1.Substring(startIndex3, num8 - startIndex3).Trim(), out result2))
                                                throw new ApplicationException("Could not read TechLevel at line " + num1.ToString() + " of file " + filePath);
                                            startIndex4 = num8 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read TechLevel at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        int result3;
                                        int startIndex5;
                                        try
                                        {
                                            int num9 = str1.IndexOf(",", startIndex4);
                                            if (num9 < 0)
                                                throw new ApplicationException("Could not read Row at line " + num1.ToString() + " of file " + filePath);
                                            if (!int.TryParse(str1.Substring(startIndex4, num9 - startIndex4).Trim(), out result3))
                                                throw new ApplicationException("Could not read Row at line " + num1.ToString() + " of file " + filePath);
                                            startIndex5 = num9 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read Row at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        IndustryType industry;
                                        int startIndex6;
                                        try
                                        {
                                            int num10 = str1.IndexOf(",", startIndex5);
                                            if (num10 < 0)
                                                throw new ApplicationException("Could not read Industry at line " + num1.ToString() + " of file " + filePath);
                                            int result4;
                                            if (!int.TryParse(str1.Substring(startIndex5, num10 - startIndex5).Trim(), out result4))
                                                throw new ApplicationException("Could not read Industry at line " + num1.ToString() + " of file " + filePath);
                                            num4 = result4;
                                            switch (num4)
                                            {
                                                case 0:
                                                    industry = IndustryType.Weapon;
                                                    break;
                                                case 1:
                                                    industry = IndustryType.Energy;
                                                    break;
                                                case 2:
                                                    industry = IndustryType.HighTech;
                                                    break;
                                                default:
                                                    throw new ApplicationException("Invalid Industry at line " + num1.ToString() + " of file " + filePath + ". Industry should be 0 (Weapons), 1 (Energy) or 2 (HighTech).");
                                            }
                                            startIndex6 = num10 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read Industry at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        ComponentCategoryType componentCategoryByIndex;
                                        int startIndex7;
                                        try
                                        {
                                            int num11 = str1.IndexOf(",", startIndex6);
                                            if (num11 < 0)
                                                throw new ApplicationException("Could not read Category at line " + num1.ToString() + " of file " + filePath);
                                            int result5;
                                            if (!int.TryParse(str1.Substring(startIndex6, num11 - startIndex6).Trim(), out result5))
                                                throw new ApplicationException("Could not read Category at line " + num1.ToString() + " of file " + filePath);
                                            componentCategoryByIndex = Galaxy.DetermineComponentCategoryByIndex(result5);
                                            if (componentCategoryByIndex == ComponentCategoryType.Undefined)
                                                throw new ApplicationException("Invalid Category at line " + num1.ToString() + " of file " + filePath + ". Category should be between 0 and 26.");
                                            startIndex7 = num11 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read Category at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        int result6;
                                        int startIndex8;
                                        try
                                        {
                                            int num12 = str1.IndexOf(",", startIndex7);
                                            if (num12 < 0)
                                                throw new ApplicationException("Could not read SpecialFunctionCode at line " + num1.ToString() + " of file " + filePath);
                                            if (!int.TryParse(str1.Substring(startIndex7, num12 - startIndex7).Trim(), out result6))
                                                throw new ApplicationException("Could not read SpecialFunctionCode at line " + num1.ToString() + " of file " + filePath);
                                            startIndex8 = num12 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read SpecialFunctionCode at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        double result7;
                                        try
                                        {
                                            int num13 = str1.IndexOf(",", startIndex8);
                                            string empty = string.Empty;
                                            if (!double.TryParse((num13 < 0 ? str1.Substring(startIndex8, str1.Length - startIndex8) : str1.Substring(startIndex8, num13 - startIndex8)).Trim(), NumberStyles.Float, (IFormatProvider)CultureInfo.InvariantCulture, out result7))
                                                throw new ApplicationException("Could not read BaseCostMultiplierOverride at line " + num1.ToString() + " of file " + filePath);
                                            num5 = num13 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read BaseCostMultiplierOverride at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        nodeDefinitionList.Add(new ResearchNodeDefinition(result1, name1, industry, componentCategoryByIndex, result2, 0.0f, result3)
                                        {
                                            BaseCostMultiplierOverride = result7,
                                            SpecialFunctionCode = result6
                                        });
                                        researchNodeId = result1;
                                        continue;
                                    case "allowed races":
                                        while (num2 >= 0)
                                        {
                                            Race race;
                                            try
                                            {
                                                int num14 = str1.IndexOf(",", startIndex1);
                                                string empty = string.Empty;
                                                string raceName = (num14 < 0 ? str1.Substring(startIndex1, str1.Length - startIndex1) : str1.Substring(startIndex1, num14 - startIndex1)).Trim();
                                                race = races[raceName];
                                                if (race == null)
                                                    throw new ApplicationException("Could not find matching race name '" + raceName + "' at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read race name at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            if (nodeById != null && race != null)
                                                nodeById.SpecifiedRaces.Add(race);
                                            num2 = startIndex1 >= str1.Length ? -1 : str1.IndexOf(",", startIndex1);
                                            startIndex1 = num2 + 1;
                                        }
                                        continue;
                                    case "components":
                                        while (num2 >= 0)
                                        {
                                            int result8;
                                            try
                                            {
                                                int num15 = str1.IndexOf(",", startIndex1);
                                                string empty = string.Empty;
                                                if (!int.TryParse((num15 < 0 ? str1.Substring(startIndex1, str1.Length - startIndex1) : str1.Substring(startIndex1, num15 - startIndex1)).Trim(), out result8))
                                                    throw new ApplicationException("Could not read ComponentId at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read ComponentId at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            nodeById?.Components.Add(new Component(result8));
                                            num2 = startIndex1 >= str1.Length ? -1 : str1.IndexOf(",", startIndex1);
                                            startIndex1 = num2 + 1;
                                        }
                                        continue;
                                    case "component improvements":
                                        for (; num2 >= 0; num2 = startIndex1 >= str1.Length ? -1 : str1.IndexOf(",", startIndex1))
                                        {
                                            int result9;
                                            int startIndex9;
                                            try
                                            {
                                                int num16 = str1.IndexOf(",", startIndex1);
                                                if (num16 < 0)
                                                    throw new ApplicationException("Could not read ComponentId at line " + num1.ToString() + " of file " + filePath);
                                                if (!int.TryParse(str1.Substring(startIndex1, num16 - startIndex1).Trim(), out result9))
                                                    throw new ApplicationException("Could not read ComponentId at line " + num1.ToString() + " of file " + filePath);
                                                startIndex9 = num16 + 1;
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read ComponentId at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            int result10;
                                            int startIndex10;
                                            try
                                            {
                                                int num17 = str1.IndexOf(",", startIndex9);
                                                if (num17 < 0)
                                                    throw new ApplicationException("Could not read TechLevel at line " + num1.ToString() + " of file " + filePath);
                                                if (!int.TryParse(str1.Substring(startIndex9, num17 - startIndex9).Trim(), out result10))
                                                    throw new ApplicationException("Could not read TechLevel at line " + num1.ToString() + " of file " + filePath);
                                                startIndex10 = num17 + 1;
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read TechLevel at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            int result11;
                                            int startIndex11;
                                            try
                                            {
                                                int num18 = str1.IndexOf(",", startIndex10);
                                                if (num18 < 0)
                                                    throw new ApplicationException("Could not read Value1 at line " + num1.ToString() + " of file " + filePath);
                                                if (!int.TryParse(str1.Substring(startIndex10, num18 - startIndex10).Trim(), out result11))
                                                    throw new ApplicationException("Could not read Value1 at line " + num1.ToString() + " of file " + filePath);
                                                startIndex11 = num18 + 1;
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read Value1 at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            int result12;
                                            int startIndex12;
                                            try
                                            {
                                                int num19 = str1.IndexOf(",", startIndex11);
                                                if (num19 < 0)
                                                    throw new ApplicationException("Could not read Value2 at line " + num1.ToString() + " of file " + filePath);
                                                if (!int.TryParse(str1.Substring(startIndex11, num19 - startIndex11).Trim(), out result12))
                                                    throw new ApplicationException("Could not read Value2 at line " + num1.ToString() + " of file " + filePath);
                                                startIndex12 = num19 + 1;
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read Value2 at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            int result13;
                                            int startIndex13;
                                            try
                                            {
                                                int num20 = str1.IndexOf(",", startIndex12);
                                                if (num20 < 0)
                                                    throw new ApplicationException("Could not read Value3 at line " + num1.ToString() + " of file " + filePath);
                                                if (!int.TryParse(str1.Substring(startIndex12, num20 - startIndex12).Trim(), out result13))
                                                    throw new ApplicationException("Could not read Value3 at line " + num1.ToString() + " of file " + filePath);
                                                startIndex13 = num20 + 1;
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read Value3 at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            int result14;
                                            int startIndex14;
                                            try
                                            {
                                                int num21 = str1.IndexOf(",", startIndex13);
                                                if (num21 < 0)
                                                    throw new ApplicationException("Could not read Value4 at line " + num1.ToString() + " of file " + filePath);
                                                if (!int.TryParse(str1.Substring(startIndex13, num21 - startIndex13).Trim(), out result14))
                                                    throw new ApplicationException("Could not read Value4 at line " + num1.ToString() + " of file " + filePath);
                                                startIndex14 = num21 + 1;
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read Value4 at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            int result15;
                                            int startIndex15;
                                            try
                                            {
                                                int num22 = str1.IndexOf(",", startIndex14);
                                                if (num22 < 0)
                                                    throw new ApplicationException("Could not read Value5 at line " + num1.ToString() + " of file " + filePath);
                                                if (!int.TryParse(str1.Substring(startIndex14, num22 - startIndex14).Trim(), out result15))
                                                    throw new ApplicationException("Could not read Value5 at line " + num1.ToString() + " of file " + filePath);
                                                startIndex15 = num22 + 1;
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read Value5 at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            int result16;
                                            int startIndex16;
                                            try
                                            {
                                                int num23 = str1.IndexOf(",", startIndex15);
                                                if (num23 < 0)
                                                    throw new ApplicationException("Could not read Value6 at line " + num1.ToString() + " of file " + filePath);
                                                if (!int.TryParse(str1.Substring(startIndex15, num23 - startIndex15).Trim(), out result16))
                                                    throw new ApplicationException("Could not read Value6 at line " + num1.ToString() + " of file " + filePath);
                                                startIndex16 = num23 + 1;
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read Value6 at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            int result17;
                                            try
                                            {
                                                int num24 = str1.IndexOf(",", startIndex16);
                                                string empty = string.Empty;
                                                if (!int.TryParse((num24 < 0 ? str1.Substring(startIndex16, str1.Length - startIndex16) : str1.Substring(startIndex16, num24 - startIndex16)).Trim(), out result17))
                                                    throw new ApplicationException("Could not read Value7 at line " + num1.ToString() + " of file " + filePath);
                                                startIndex1 = num24 + 1;
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read Value7 at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            if (nodeById != null)
                                            {
                                                ComponentImprovement componentImprovement = new ComponentImprovement(new Component(result9), result10, result11, result12, result13, result14, result15, result16, result17);
                                                nodeById.ComponentImprovements.Add(componentImprovement);
                                            }
                                        }
                                        continue;
                                    case "fighters":
                                        while (num2 >= 0)
                                        {
                                            int result18;
                                            try
                                            {
                                                int num25 = str1.IndexOf(",", startIndex1);
                                                string empty = string.Empty;
                                                if (!int.TryParse((num25 < 0 ? str1.Substring(startIndex1, str1.Length - startIndex1) : str1.Substring(startIndex1, num25 - startIndex1)).Trim(), out result18))
                                                    throw new ApplicationException("Could not read FighterId at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read FighterId at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            if (nodeById != null)
                                            {
                                                FighterSpecification byId = Galaxy.FighterSpecificationsStatic.GetById(result18);
                                                if (byId != null)
                                                    nodeById.Fighters.Add(byId);
                                            }
                                            num2 = startIndex1 >= str1.Length ? -1 : str1.IndexOf(",", startIndex1);
                                            startIndex1 = num2 + 1;
                                        }
                                        continue;
                                    case "facility":
                                        int result19;
                                        try
                                        {
                                            int num26 = str1.IndexOf(",", startIndex1);
                                            string empty = string.Empty;
                                            if (!int.TryParse((num26 < 0 ? str1.Substring(startIndex1, str1.Length - startIndex1) : str1.Substring(startIndex1, num26 - startIndex1)).Trim(), out result19))
                                                throw new ApplicationException("Could not read FacilityId at line " + num1.ToString() + " of file " + filePath);
                                            num5 = num26 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read FacilityId at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        if (nodeById != null)
                                        {
                                            PlanetaryFacilityDefinition byId = Galaxy.PlanetaryFacilityDefinitionsStatic.GetById(result19);
                                            if (byId != null)
                                            {
                                                nodeById.PlanetaryFacility = byId;
                                                continue;
                                            }
                                            continue;
                                        }
                                        continue;
                                    case "abilities":
                                        while (num2 >= 0)
                                        {
                                            ResearchAbilityType type = ResearchAbilityType.Undefined;
                                            object relatedObject = (object)null;
                                            string name2;
                                            int startIndex17;
                                            try
                                            {
                                                int num27 = str1.IndexOf(",", startIndex1);
                                                if (num27 < 0)
                                                    throw new ApplicationException("Could not read Name at line " + num1.ToString() + " of file " + filePath);
                                                name2 = str1.Substring(startIndex1, num27 - startIndex1).Trim();
                                                startIndex17 = num27 + 1;
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read Name at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            int startIndex18;
                                            try
                                            {
                                                int num28 = str1.IndexOf(",", startIndex17);
                                                if (num28 < 0)
                                                    throw new ApplicationException("Could not read Ability Type at line " + num1.ToString() + " of file " + filePath);
                                                int result20;
                                                if (!int.TryParse(str1.Substring(startIndex17, num28 - startIndex17).Trim(), out result20))
                                                    throw new ApplicationException("Could not read Ability Type at line " + num1.ToString() + " of file " + filePath);
                                                num4 = result20;
                                                switch (num4)
                                                {
                                                    case 0:
                                                        type = ResearchAbilityType.Boarding;
                                                        break;
                                                    case 1:
                                                        type = ResearchAbilityType.ColonizeHabitatType;
                                                        break;
                                                    case 2:
                                                        type = ResearchAbilityType.ConstructionSize;
                                                        break;
                                                    case 3:
                                                        type = ResearchAbilityType.EnableShipSubRole;
                                                        break;
                                                    case 4:
                                                        type = ResearchAbilityType.PopulationGrowthRate;
                                                        break;
                                                    case 5:
                                                        type = ResearchAbilityType.Troop;
                                                        break;
                                                }
                                                startIndex18 = num28 + 1;
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read FacilityId at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            int result21;
                                            int startIndex19;
                                            try
                                            {
                                                int num29 = str1.IndexOf(",", startIndex18);
                                                if (num29 < 0)
                                                    throw new ApplicationException("Could not read AbilityLevel at line " + num1.ToString() + " of file " + filePath);
                                                if (!int.TryParse(str1.Substring(startIndex18, num29 - startIndex18).Trim(), out result21))
                                                    throw new ApplicationException("Could not read AbilityLevel at line " + num1.ToString() + " of file " + filePath);
                                                startIndex19 = num29 + 1;
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read AbilityLevel at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            int result22;
                                            int startIndex20;
                                            try
                                            {
                                                int num30 = str1.IndexOf(",", startIndex19);
                                                if (num30 < 0)
                                                    throw new ApplicationException("Could not read AbilityValue at line " + num1.ToString() + " of file " + filePath);
                                                if (!int.TryParse(str1.Substring(startIndex19, num30 - startIndex19).Trim(), out result22))
                                                    throw new ApplicationException("Could not read AbilityValue at line " + num1.ToString() + " of file " + filePath);
                                                startIndex20 = num30 + 1;
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read AbilityValue at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            try
                                            {
                                                int num31 = str1.IndexOf(",", startIndex20);
                                                string empty = string.Empty;
                                                int result23;
                                                if (!int.TryParse((num31 < 0 ? str1.Substring(startIndex20, str1.Length - startIndex20) : str1.Substring(startIndex20, num31 - startIndex20)).Trim(), out result23))
                                                    throw new ApplicationException("Could not read AbilityRelatedObject at line " + num1.ToString() + " of file " + filePath);
                                                switch (type)
                                                {
                                                    case ResearchAbilityType.EnableShipSubRole:
                                                        num4 = result23;
                                                        switch (num4)
                                                        {
                                                            case 0:
                                                                relatedObject = (object)BuiltObjectSubRole.Carrier;
                                                                break;
                                                            case 1:
                                                                relatedObject = (object)BuiltObjectSubRole.ResupplyShip;
                                                                break;
                                                        }
                                                        break;
                                                    case ResearchAbilityType.Troop:
                                                        num4 = result23;
                                                        switch (num4)
                                                        {
                                                            case 0:
                                                                relatedObject = (object)TroopType.Undefined;
                                                                break;
                                                            case 1:
                                                                relatedObject = (object)TroopType.Infantry;
                                                                break;
                                                            case 2:
                                                                relatedObject = (object)TroopType.Armored;
                                                                break;
                                                            case 3:
                                                                relatedObject = (object)TroopType.Artillery;
                                                                break;
                                                            case 4:
                                                                relatedObject = (object)TroopType.SpecialForces;
                                                                break;
                                                        }
                                                        break;
                                                }
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read AbilityRelatedObject at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            if (nodeById != null)
                                            {
                                                ResearchAbility researchAbility = new ResearchAbility(name2, type, result21, result22, relatedObject);
                                                nodeById.Abilities.Add(researchAbility);
                                            }
                                            num2 = startIndex20 >= str1.Length ? -1 : str1.IndexOf(",", startIndex20);
                                            startIndex1 = num2 + 1;
                                        }
                                        continue;
                                    case "plague change":
                                        byte result24;
                                        int startIndex21;
                                        try
                                        {
                                            int num32 = str1.IndexOf(",", startIndex1);
                                            if (num32 < 0)
                                                throw new ApplicationException("Could not read PlagueId at line " + num1.ToString() + " of file " + filePath);
                                            if (!byte.TryParse(str1.Substring(startIndex1, num32 - startIndex1).Trim(), out result24))
                                                throw new ApplicationException("Could not read PlagueId at line " + num1.ToString() + " of file " + filePath);
                                            if (result24 < (byte)0 || (int)result24 >= Galaxy.PlaguesStatic.Count)
                                                throw new ApplicationException("Invalid PlagueId encountered. Must match PlagueId from plagues.txt file. Line " + num1.ToString() + " of file " + filePath);
                                            startIndex21 = num32 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read PlagueId at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        string str3;
                                        int startIndex22;
                                        try
                                        {
                                            int num33 = str1.IndexOf(",", startIndex21);
                                            if (num33 < 0)
                                                throw new ApplicationException("Could not read PlagueChangeDescription at line " + num1.ToString() + " of file " + filePath);
                                            str3 = str1.Substring(startIndex21, num33 - startIndex21).Trim();
                                            startIndex22 = num33 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read PlagueChangeDescription at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        double result25;
                                        int startIndex23;
                                        try
                                        {
                                            int num34 = str1.IndexOf(",", startIndex22);
                                            if (num34 < 0)
                                                throw new ApplicationException("Could not read MortalityRate at line " + num1.ToString() + " of file " + filePath);
                                            if (!double.TryParse(str1.Substring(startIndex22, num34 - startIndex22).Trim(), NumberStyles.Float, (IFormatProvider)CultureInfo.InvariantCulture, out result25))
                                                throw new ApplicationException("Could not read MortalityRate at line " + num1.ToString() + " of file " + filePath);
                                            if (result25 < 0.0 || result25 > 5.0)
                                                throw new ApplicationException("Invalid MortalityRate. Should be in range 0.000 to 5.0. Line " + num1.ToString() + " of file " + filePath);
                                            startIndex23 = num34 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read MortalityRate at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        int result26;
                                        int startIndex24;
                                        try
                                        {
                                            int num35 = str1.IndexOf(",", startIndex23);
                                            if (num35 < 0)
                                                throw new ApplicationException("Could not read InfectionChance at line " + num1.ToString() + " of file " + filePath);
                                            if (!int.TryParse(str1.Substring(startIndex23, num35 - startIndex23).Trim(), NumberStyles.Float, (IFormatProvider)CultureInfo.InvariantCulture, out result26))
                                                throw new ApplicationException("Could not read InfectionChance at line " + num1.ToString() + " of file " + filePath);
                                            startIndex24 = num35 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read InfectionChance at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        float result27;
                                        int startIndex25;
                                        try
                                        {
                                            int num36 = str1.IndexOf(",", startIndex24);
                                            if (num36 < 0)
                                                throw new ApplicationException("Could not read Length at line " + num1.ToString() + " of file " + filePath);
                                            if (!float.TryParse(str1.Substring(startIndex24, num36 - startIndex24).Trim(), NumberStyles.Float, (IFormatProvider)CultureInfo.InvariantCulture, out result27))
                                                throw new ApplicationException("Could not read Length at line " + num1.ToString() + " of file " + filePath);
                                            startIndex25 = num36 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read Length at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        double result28;
                                        int startIndex26;
                                        try
                                        {
                                            int num37 = str1.IndexOf(",", startIndex25);
                                            if (num37 < 0)
                                                throw new ApplicationException("Could not read ExceptionMortalityRate at line " + num1.ToString() + " of file " + filePath);
                                            if (!double.TryParse(str1.Substring(startIndex25, num37 - startIndex25).Trim(), NumberStyles.Float, (IFormatProvider)CultureInfo.InvariantCulture, out result28))
                                                throw new ApplicationException("Could not read ExceptionMortalityRate at line " + num1.ToString() + " of file " + filePath);
                                            if (result28 < 0.0 || result28 > 5.0)
                                                throw new ApplicationException("Invalid ExceptionMortalityRate. Should be in range 0.000 to 5.0. Line " + num1.ToString() + " of file " + filePath);
                                            startIndex26 = num37 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read ExceptionMortalityRate at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        int result29;
                                        int startIndex27;
                                        try
                                        {
                                            int num38 = str1.IndexOf(",", startIndex26);
                                            if (num38 < 0)
                                                throw new ApplicationException("Could not read ExceptionInfectionChance at line " + num1.ToString() + " of file " + filePath);
                                            if (!int.TryParse(str1.Substring(startIndex26, num38 - startIndex26).Trim(), NumberStyles.Float, (IFormatProvider)CultureInfo.InvariantCulture, out result29))
                                                throw new ApplicationException("Could not read ExceptionInfectionChance at line " + num1.ToString() + " of file " + filePath);
                                            startIndex27 = num38 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read ExceptionInfectionChance at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        float result30;
                                        try
                                        {
                                            int num39 = str1.IndexOf(",", startIndex27);
                                            string empty = string.Empty;
                                            if (!float.TryParse((num39 < 0 ? str1.Substring(startIndex27, str1.Length - startIndex27) : str1.Substring(startIndex27, num39 - startIndex27)).Trim(), NumberStyles.Float, (IFormatProvider)CultureInfo.InvariantCulture, out result30))
                                                throw new ApplicationException("Could not read ExceptionLength at line " + num1.ToString() + " of file " + filePath);
                                            num5 = num39 + 1;
                                        }
                                        catch
                                        {
                                            throw new ApplicationException("Could not read ExceptionLength at line " + num1.ToString() + " of file " + filePath);
                                        }
                                        Plague plague1 = Galaxy.PlaguesStatic[(int)result24];
                                        if (plague1 != null)
                                        {
                                            Plague plague2 = new Plague(result24, plague1.Name, plague1.PictureRef, result25, result26, result27);
                                            plague2.ExceptionMortalityRate = result28;
                                            plague2.ExceptionInfectionChance = result29;
                                            plague2.ExceptionDuration = result30;
                                            plague2.ExceptionRaceName = plague1.ExceptionRaceName;
                                            plague2.Description = str3;
                                            if (nodeById != null)
                                            {
                                                nodeById.PlagueChange = plague2;
                                                continue;
                                            }
                                            continue;
                                        }
                                        continue;
                                    case "parents":
                                        while (num2 >= 0)
                                        {
                                            bool flag = false;
                                            int result31;
                                            int startIndex28;
                                            try
                                            {
                                                int num40 = str1.IndexOf(",", startIndex1);
                                                if (num40 < 0)
                                                    throw new ApplicationException("Could not read ParentResearchProjectId at line " + num1.ToString() + " of file " + filePath);
                                                if (!int.TryParse(str1.Substring(startIndex1, num40 - startIndex1).Trim(), out result31))
                                                    throw new ApplicationException("Could not read ParentResearchProjectId at line " + num1.ToString() + " of file " + filePath);
                                                startIndex28 = num40 + 1;
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read ParentResearchProjectId at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            try
                                            {
                                                int num41 = str1.IndexOf(",", startIndex28);
                                                string empty = string.Empty;
                                                switch ((num41 < 0 ? str1.Substring(startIndex28, str1.Length - startIndex28) : str1.Substring(startIndex28, num41 - startIndex28)).Trim().ToLower(CultureInfo.InvariantCulture))
                                                {
                                                    case "y":
                                                        flag = true;
                                                        break;
                                                    case "n":
                                                        flag = false;
                                                        break;
                                                }
                                            }
                                            catch
                                            {
                                                throw new ApplicationException("Could not read ParentIsRequired at line " + num1.ToString() + " of file " + filePath);
                                            }
                                            if (nodeById != null)
                                            {
                                                intListArray[nodeById.ResearchNodeId].Add(result31);
                                                boolListArray[nodeById.ResearchNodeId].Add(flag);
                                            }
                                            num2 = startIndex28 >= str1.Length ? -1 : str1.IndexOf(",", startIndex28);
                                            startIndex1 = num2 + 1;
                                        }
                                        continue;
                                    default:
                                        continue;
                                }
                            }
                        }
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error at line " + num1.ToString() + " reading file " + filePath);
            }
            nodeDefinitionList.Sort();
            if (!this.CheckSequentialIds(nodeDefinitionList))
                throw new ApplicationException("Non-sequential Research Project IDs detected in file " + filePath + ". Research Project ID values must start at 0 (zero) and be sequential.");
            for (int index1 = 0; index1 < nodeDefinitionList.Count; ++index1)
            {
                nodeDefinitionList[index1].ParentNodes = new ResearchNodeDefinitionList();
                nodeDefinitionList[index1].ParentIsRequired = new List<bool>();
                if (intListArray[index1] != null)
                {
                    for (int index2 = 0; index2 < intListArray[index1].Count; ++index2)
                    {
                        nodeDefinitionList[index1].ParentNodes.Add(nodeDefinitionList[intListArray[index1][index2]]);
                        if (boolListArray[index1] != null && boolListArray[index1].Count > 0)
                            nodeDefinitionList[index1].ParentIsRequired.Add(boolListArray[index1][index2]);
                        else
                            nodeDefinitionList[index1].ParentIsRequired.Add(false);
                    }
                }
            }
            this.AddRange((IEnumerable<ResearchNodeDefinition>)nodeDefinitionList);
        }
        public void LoadFromFile(SQLiteDataReader reader, RaceList races, int projCount)
        {
            this.Clear();
            ResearchNodeDefinitionList nodeDefinitionList = new ResearchNodeDefinitionList();
            List<int>[] intListArray;
            List<bool>[] boolListArray;

            int id = 0;
            try
            {

                intListArray = new List<int>[projCount];
                boolListArray = new List<bool>[projCount];
                for (int index = 0; index < projCount; ++index)
                {
                    intListArray[index] = new List<int>();
                    boolListArray[index] = new List<bool>();
                }

                while (reader.Read())
                {
                    id = reader.GetInt32(reader.GetOrdinal("ID"));
                    if (id < 0)
                        throw new ApplicationException($"Wrong ProjectId {id}");
                    ResearchNodeDefinition node;
                    {//project
                        string projName = reader.GetString(reader.GetOrdinal("Name"));
                        if (string.IsNullOrWhiteSpace(projName))
                            throw new ApplicationException($"Could not read Name at ID {id} at Research");

                        int techLevel = reader.GetInt32(reader.GetOrdinal("TechLevel"));

                        int rowIdx = reader.GetInt32(reader.GetOrdinal("RowIdx"));

                        IndustryType industry;
                        int indTempVal = reader.GetInt32(reader.GetOrdinal("Industry"));
                        switch (indTempVal)
                        {
                            case 0:
                                industry = IndustryType.Weapon;
                                break;
                            case 1:
                                industry = IndustryType.Energy;
                                break;
                            case 2:
                                industry = IndustryType.HighTech;
                                break;
                            default:
                                throw new ApplicationException($"Invalid Industry at ID {id} of Research. Industry should be 0 (Weapons), 1 (Energy) or 2 (HighTech).");
                        }

                        ComponentCategoryType componentCategoryByIndex;
                        int categoryTempVal = reader.GetInt32(reader.GetOrdinal("Category"));
                        componentCategoryByIndex = Galaxy.DetermineComponentCategoryByIndex(categoryTempVal);
                        if (componentCategoryByIndex == ComponentCategoryType.Undefined)
                            throw new ApplicationException($"Invalid Category at ID {id} of Research. Category should be between 0 and 26.");

                        int specialFunctionCode = reader.GetInt32(reader.GetOrdinal("SpecialFunctionCode"));
                        double baseCostMultOverride = reader.GetInt32(reader.GetOrdinal("SpecialFunctionCode"));

                        node = new ResearchNodeDefinition(id, projName, industry, componentCategoryByIndex, techLevel, 0.0f, rowIdx)
                        {
                            BaseCostMultiplierOverride = baseCostMultOverride,
                            SpecialFunctionCode = specialFunctionCode
                        };
                        nodeDefinitionList.Add(node);
                    }
                    {//AllowedRaces
                        using var allowedRacesReader = Main._FileDB.GetResearchAllowedRacesReader(id);
                        if (allowedRacesReader.HasRows)
                        {
                            while (allowedRacesReader.Read())
                            {
                                string raceName = allowedRacesReader.GetString(allowedRacesReader.GetOrdinal("RaceName"));
                                Race race = races[raceName];
                                if (race == null)
                                    throw new ApplicationException($"Could not find matching race name '{raceName}' at ID {id} at ResearchAllowedRaces");
                                node.SpecifiedRaces.Add(race);
                            }
                        }
                    }
                    {//Components

                        using var componentReader = Main._FileDB.GetResearchComponentsReader(id);
                        if (componentReader.HasRows)
                        {
                            while (componentReader.Read())
                            {
                                node.Components.Add(new Component(componentReader.GetInt32(componentReader.GetOrdinal("ComponentID"))));
                            }
                        }
                    }
                    {//ComponentImprovements
                        using var componentImproveReader = Main._FileDB.GetResearchComponentImprovementsReader(id);
                        if (componentImproveReader.HasRows)
                        {
                            while (componentImproveReader.Read())
                            {
                                int componentId = componentImproveReader.GetInt32(componentImproveReader.GetOrdinal("ComponentID"));
                                int techLevel = componentImproveReader.GetInt32(componentImproveReader.GetOrdinal("TechLevel"));
                                int val1 = componentImproveReader.GetInt32(componentImproveReader.GetOrdinal("Value1"));
                                int val2 = componentImproveReader.GetInt32(componentImproveReader.GetOrdinal("Value2"));
                                int val3 = componentImproveReader.GetInt32(componentImproveReader.GetOrdinal("Value3"));
                                int val4 = componentImproveReader.GetInt32(componentImproveReader.GetOrdinal("Value4"));
                                int val5 = componentImproveReader.GetInt32(componentImproveReader.GetOrdinal("Value5"));
                                int val6 = componentImproveReader.GetInt32(componentImproveReader.GetOrdinal("Value6"));
                                int val7 = componentImproveReader.GetInt32(componentImproveReader.GetOrdinal("Value7"));

                                ComponentImprovement componentImprovement = new ComponentImprovement(new Component(componentId), techLevel, val1, val2, val3, val4, val5, val6, val7);
                                node.ComponentImprovements.Add(componentImprovement);
                            }
                        }
                    }
                    {//Fighters
                        using var fighterReader = Main._FileDB.GetResearchFightersReader(id);
                        if (fighterReader.HasRows)
                        {
                            while (fighterReader.Read())
                            {
                                int fighterId = fighterReader.GetInt32(fighterReader.GetOrdinal("FighterID"));
                                FighterSpecification byId = Galaxy.FighterSpecificationsStatic.GetById(fighterId);
                                if (byId != null)
                                {
                                    node.Fighters.Add(byId);
                                }
                            }
                        }
                    }
                    { //Facility

                        int facId = reader.GetInt32(reader.GetOrdinal("FacilityID"));
                        if (facId != -1)
                        {
                            PlanetaryFacilityDefinition byId = Galaxy.PlanetaryFacilityDefinitionsStatic.GetById(facId);
                            if (byId != null)
                            {
                                node.PlanetaryFacility = byId;
                            }
                        }
                    }
                    {//Abilities
                        using var abilityReader = Main._FileDB.GetResearchAbilitiesReader(id);
                        if (abilityReader.HasRows)
                        {
                            while (abilityReader.Read())
                            {
                                string abbName = abilityReader.GetString(abilityReader.GetOrdinal("Name"));
                                int abbType = abilityReader.GetInt32(abilityReader.GetOrdinal("Type"));
                                int abbLevel = abilityReader.GetInt32(abilityReader.GetOrdinal("Level"));
                                int abbValue = abilityReader.GetInt32(abilityReader.GetOrdinal("Value"));
                                int relatedObjIdx = abilityReader.GetInt32(abilityReader.GetOrdinal("RelatedObjectIndex"));

                                ResearchAbilityType type = abbType switch
                                {
                                    0 => ResearchAbilityType.Boarding,
                                    1 => ResearchAbilityType.ColonizeHabitatType,
                                    2 => ResearchAbilityType.ConstructionSize,
                                    3 => ResearchAbilityType.EnableShipSubRole,
                                    4 => ResearchAbilityType.PopulationGrowthRate,
                                    5 => ResearchAbilityType.Troop,
                                    _ => ResearchAbilityType.Undefined
                                };
                                ResearchAbility researchAbility = new ResearchAbility(abbName, type, abbLevel, abbValue, relatedObjIdx);
                                node.Abilities.Add(researchAbility);
                            }
                        }
                    }
                    {//Plague change
                        using var plagueReader = Main._FileDB.GetResearchPlagueChangeReader(id);
                        if (plagueReader.HasRows)
                        {
                            while (plagueReader.Read())
                            {
                                int plagId = plagueReader.GetInt32(plagueReader.GetOrdinal("PlagueID"));
                                if (plagId < (byte)0 || (int)plagId >= Galaxy.PlaguesStatic.Count)
                                    throw new ApplicationException($"Invalid PlagueId encountered. Must match PlagueId from Plagues. ID {id} at PlagueChange at Reseach");
                                string plagDesc = plagueReader.GetString(plagueReader.GetOrdinal("Description"));
                                if (string.IsNullOrWhiteSpace(plagDesc))
                                    throw new ApplicationException($"Could not read PlagueChange Description at ID {id} at PlagueChange in Research");
                                double plagMortRate = plagueReader.GetDouble(plagueReader.GetOrdinal("MortalityRate"));
                                if (plagMortRate < 0.0 || plagMortRate > 5.0)
                                    throw new ApplicationException($"Invalid MortalityRate. Should be in range 0.000 to 5.0. ID {id} at PlagueChange in Research");
                                int plagInfChance = plagueReader.GetInt32(plagueReader.GetOrdinal("InfectionChance"));
                                float plagDuration = plagueReader.GetFloat(plagueReader.GetOrdinal("Duration"));
                                double plagExMortRate = plagueReader.GetDouble(plagueReader.GetOrdinal("ExceptionMortalityRate"));
                                if (plagExMortRate < 0.0 || plagExMortRate > 5.0)
                                    throw new ApplicationException($"Invalid ExceptionMortalityRate. Should be in range 0.000 to 5.0. ID {id} at PlagueChange in Research");
                                int plagExInfChance = plagueReader.GetInt32(plagueReader.GetOrdinal("ExceptionInfectionChance"));
                                float plagExDuration = plagueReader.GetFloat(plagueReader.GetOrdinal("ExceptionDuration"));

                                Plague plague1 = Galaxy.PlaguesStatic[plagId];
                                if (plague1 != null)
                                {
                                    node.PlagueChange = new Plague((byte)plagId, plague1.Name, plague1.PictureRef, plagMortRate, plagInfChance, plagDuration)
                                    {
                                        ExceptionMortalityRate = plagExMortRate,
                                        ExceptionInfectionChance = plagExInfChance,
                                        ExceptionDuration = plagExDuration,
                                        ExceptionRaceName = plague1.ExceptionRaceName,
                                        Description = plagDesc
                                    };
                                }
                            }
                        }
                    }
                    {//Parents
                        using var parentReader = Main._FileDB.GetResearchParentReader(id);
                        if (parentReader.HasRows)
                        {
                            while (parentReader.Read())
                            {
                                int parentId = parentReader.GetInt32(parentReader.GetOrdinal("ParentProjectID"));
                                bool required = parentReader.GetBoolean(parentReader.GetOrdinal("RequiredParent"));

                                intListArray[node.ResearchNodeId].Add(parentId);
                                boolListArray[node.ResearchNodeId].Add(required);
                            }
                        }
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error at ID {id} at Research");
            }
            nodeDefinitionList.Sort();
            if (!this.CheckSequentialIds(nodeDefinitionList))
                throw new ApplicationException("Non-sequential Research Project IDs detected. Research Project ID values must start at 0 (zero) and be sequential.");
            for (int index1 = 0; index1 < nodeDefinitionList.Count; ++index1)
            {
                nodeDefinitionList[index1].ParentNodes = new ResearchNodeDefinitionList();
                nodeDefinitionList[index1].ParentIsRequired = new List<bool>();
                if (intListArray[index1] != null)
                {
                    for (int index2 = 0; index2 < intListArray[index1].Count; ++index2)
                    {
                        nodeDefinitionList[index1].ParentNodes.Add(nodeDefinitionList[intListArray[index1][index2]]);
                        if (boolListArray[index1] != null && boolListArray[index1].Count > 0)
                            nodeDefinitionList[index1].ParentIsRequired.Add(boolListArray[index1][index2]);
                        else
                            nodeDefinitionList[index1].ParentIsRequired.Add(false);
                    }
                }
            }
            this.AddRange((IEnumerable<ResearchNodeDefinition>)nodeDefinitionList);
        }

        public ResearchNodeList UpdateProjectCostsForRace(ResearchNodeList projects, Race race)
        {
            if (race != null)
            {
                ResearchNode projectForColonization1 = projects.GetLowestProjectForColonization(HabitatType.Continental);
                ResearchNode projectForColonization2 = projects.GetLowestProjectForColonization(HabitatType.MarshySwamp);
                ResearchNode projectForColonization3 = projects.GetLowestProjectForColonization(HabitatType.Ocean);
                ResearchNode projectForColonization4 = projects.GetLowestProjectForColonization(HabitatType.Desert);
                ResearchNode projectForColonization5 = projects.GetLowestProjectForColonization(HabitatType.Ice);
                ResearchNode projectForColonization6 = projects.GetLowestProjectForColonization(HabitatType.Volcanic);
                if (projectForColonization1 != null)
                    projectForColonization1.Cost *= (float)race.ResearchColonizationCostFactorContinental;
                if (projectForColonization2 != null)
                    projectForColonization2.Cost *= (float)race.ResearchColonizationCostFactorMarshySwamp;
                if (projectForColonization3 != null)
                    projectForColonization3.Cost *= (float)race.ResearchColonizationCostFactorOcean;
                if (projectForColonization4 != null)
                    projectForColonization4.Cost *= (float)race.ResearchColonizationCostFactorDesert;
                if (projectForColonization5 != null)
                    projectForColonization5.Cost *= (float)race.ResearchColonizationCostFactorIce;
                if (projectForColonization6 != null)
                    projectForColonization6.Cost *= (float)race.ResearchColonizationCostFactorVolcanic;
            }
            return projects;
        }

        public ResearchNodeList UpdateParentNodes(ResearchNodeList projects)
        {
            for (int index1 = 0; index1 < projects.Count; ++index1)
            {
                if (this[index1].ParentNodes != null && this[index1].ParentNodes.Count > 0 && this.Count > index1)
                {
                    projects[index1].ParentNodes = new ResearchNodeList();
                    projects[index1].ParentIsRequired = new List<bool>();
                    for (int index2 = 0; index2 < this[index1].ParentNodes.Count; ++index2)
                    {
                        ResearchNode nodeById = projects.FindNodeById(this[index1].ParentNodes[index2].ResearchNodeId);
                        if (nodeById != null)
                        {
                            projects[index1].ParentNodes.Add(nodeById);
                            projects[index1].ParentIsRequired.Add(this[index1].ParentIsRequired[index2]);
                        }
                    }
                }
            }
            return projects;
        }

        public ResearchNodeList ObtainTechTree(Race race)
        {
            ResearchNodeList projects = new ResearchNodeList();
            for (int index = 0; index < this.Count; ++index)
            {
                ResearchNode researchNode = new ResearchNode(this[index].ResearchNodeId);
                projects.Add(researchNode);
            }
            return this.UpdateParentNodes(this.UpdateProjectCostsForRace(projects, race));
        }

        public ResearchNodeList SetTechTreeStartingDefaultsPirates(
          ResearchNodeList techTree,
          Race race,
          EmpirePolicy policy)
        {
            for (int index = 0; index < techTree.Count; ++index)
            {
                techTree[index].IsResearched = false;
                techTree[index].Progress = 0.0f;
            }
            for (int index = 0; index < techTree.Count; ++index)
            {
                switch (techTree[index].SpecialFunctionCode)
                {
                    case 1:
                    case 2:
                        techTree[index].IsResearched = true;
                        techTree[index].IsEnabled = true;
                        break;
                    case 5:
                        techTree[index].IsEnabled = false;
                        break;
                }
            }
            if (policy != null)
            {
                List<ComponentCategoryType> techFocusCategories = new List<ComponentCategoryType>();
                List<ComponentType> techFocusTypes = new List<ComponentType>();
                Galaxy.ResolveTechFocuses(policy, out techFocusCategories, out techFocusTypes);
                if (techFocusCategories.Contains(ComponentCategoryType.WeaponBeam))
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponBeam, race, 1);
                if (techFocusCategories.Contains(ComponentCategoryType.WeaponArea))
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponAreaDestruction, race, 1);
                if (techFocusTypes.Contains(ComponentType.WeaponGravityBeam))
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponGravityBeam, race, 1);
                if (techFocusTypes.Contains(ComponentType.WeaponTractorBeam))
                {
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponGravityBeam, race, 1);
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponTractorBeam, race);
                }
                if (techFocusCategories.Contains(ComponentCategoryType.WeaponIon))
                {
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponBeam, race, 1);
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponIonCannon, race);
                }
                if (techFocusCategories.Contains(ComponentCategoryType.WeaponTorpedo))
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponTorpedo, race, 1);
                if (techFocusTypes.Contains(ComponentType.WeaponMissile))
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponMissile, race, 1);
                if (techFocusTypes.Contains(ComponentType.WeaponRailGun))
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponRailGun, race, 1);
                if (techFocusTypes.Contains(ComponentType.ComputerCountermeasures))
                    techTree.FindAndResearchLowestProject(ComponentType.ComputerCountermeasures, race);
                if (techFocusTypes.Contains(ComponentType.ComputerTargetting))
                    techTree.FindAndResearchLowestProject(ComponentType.ComputerTargetting, race);
                if (techFocusTypes.Contains(ComponentType.DamageControl))
                    techTree.FindAndResearchLowestProject(ComponentType.DamageControl, race);
                if (techFocusTypes.Contains(ComponentType.AssaultPod))
                    techTree.FindAndResearchLowestProject(ComponentType.AssaultPod, race);
                if (techFocusCategories.Contains(ComponentCategoryType.Fighter))
                    techTree.FindAndResearchLowestProject(ComponentType.FighterBay, race);
                if (techFocusCategories.Contains(ComponentCategoryType.Sensor))
                    techTree.FindAndResearchLowestProject(ComponentType.SensorProximityArray, race);
                ResearchNode projectForTypeAny1 = techTree.GetLowestProjectForTypeAny(ComponentType.WeaponBeam);
                if (projectForTypeAny1 != null && !projectForTypeAny1.IsResearched && !techFocusTypes.Contains(ComponentType.WeaponGravityBeam) && !techFocusTypes.Contains(ComponentType.WeaponRailGun))
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponBeam, race, 1);
                ResearchNode projectForTypeAny2 = techTree.GetLowestProjectForTypeAny(ComponentType.WeaponTorpedo);
                ResearchNode projectForTypeAny3 = techTree.GetLowestProjectForTypeAny(ComponentType.WeaponMissile);
                if (projectForTypeAny2 != null && !projectForTypeAny1.IsResearched && projectForTypeAny3 != null && !projectForTypeAny3.IsResearched)
                {
                    if (techFocusTypes.Contains(ComponentType.WeaponMissile))
                        techTree.FindAndResearchLowestProject(ComponentType.WeaponMissile, race, 1);
                    else
                        techTree.FindAndResearchLowestProject(ComponentType.WeaponTorpedo, race, 1);
                }
            }
            techTree.FindAndResearchLowestProject(ComponentType.Armor, race);
            techTree.FindAndResearchLowestProject(ComponentType.Reactor, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.Shields, race);
            techTree.FindAndResearchLowestProject(ComponentCategoryType.Construction, race, 2);
            techTree.FindAndResearchLowestProject(ComponentType.ExtractorMine, race);
            techTree.FindAndResearchLowestProject(ComponentType.EnergyCollector, race);
            techTree.FindAndResearchLowestProject(ComponentType.EngineMainThrust, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.EngineVectoring, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.HyperDrive, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.SensorResourceProfileSensor, race, 1);
            techTree.FindAndResearchLowestProject(ComponentCategoryType.Storage, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.StorageDockingBay, race);
            techTree.FindAndResearchLowestProject(ComponentType.HabitationLifeSupport, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.HabitationHabModule, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.HabitationMedicalCenter, race);
            techTree.FindAndResearchLowestProject(ComponentType.HabitationRecreationCenter, race);
            techTree.FindAndResearchLowestProject(ComponentType.LabsWeaponsLab, race);
            techTree.FindAndResearchLowestProject(ComponentType.LabsEnergyLab, race);
            techTree.FindAndResearchLowestProject(ComponentType.LabsHighTechLab, race);
            techTree.FindAndResearchLowestProject(ComponentType.ComputerCommandCenter, race);
            techTree.FindAndResearchLowestProject(ComponentType.ComputerCommerceCenter, race);
            ResearchNode projectForTroopType = techTree.GetLowestProjectForTroopType(TroopType.Infantry);
            if (projectForTroopType != null)
                projectForTroopType.IsResearched = true;
            techTree.FindAndResearchLowestProject(ComponentType.AssaultPod, race);
            techTree.FindAndResearchLowestProject(ComponentType.WeaponTractorBeam, race);
            ResearchNode forResupplyShips = techTree.GetLowestProjectForResupplyShips();
            if (forResupplyShips != null)
            {
                forResupplyShips.IsResearched = true;
                forResupplyShips.IsEnabled = true;
            }
            techTree.FindAndResearchLowestProject(ComponentCategoryType.Construction, race, 2);
            if (race != null)
            {
                for (int index = 0; index < techTree.Count; ++index)
                {
                    if (techTree[index].DisallowedRaces != null && techTree[index].DisallowedRaces.Count > 0 && techTree[index].DisallowedRaces.Contains(race))
                        techTree[index].IsResearched = false;
                }
            }
            for (int index = 0; index < techTree.Count; ++index)
                techTree[index].SelfResearched = techTree[index].IsResearched;
            return techTree;
        }

        public ResearchNodeList SetTechTreeStartingDefaults(
          ResearchNodeList techTree,
          Race race,
          EmpirePolicy policy)
        {
            for (int index = 0; index < techTree.Count; ++index)
            {
                techTree[index].IsResearched = false;
                techTree[index].Progress = 0.0f;
            }
            for (int index = 0; index < techTree.Count; ++index)
            {
                switch (techTree[index].SpecialFunctionCode)
                {
                    case 1:
                    case 2:
                        techTree[index].IsResearched = true;
                        techTree[index].IsEnabled = true;
                        break;
                    case 5:
                        techTree[index].IsEnabled = false;
                        break;
                }
            }
            if (policy != null)
            {
                List<ComponentCategoryType> techFocusCategories = new List<ComponentCategoryType>();
                List<ComponentType> techFocusTypes = new List<ComponentType>();
                Galaxy.ResolveTechFocuses(policy, out techFocusCategories, out techFocusTypes);
                if (techFocusCategories.Contains(ComponentCategoryType.WeaponBeam))
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponBeam, race, 1);
                if (techFocusCategories.Contains(ComponentCategoryType.WeaponArea))
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponAreaDestruction, race, 1);
                if (techFocusTypes.Contains(ComponentType.WeaponGravityBeam))
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponGravityBeam, race, 1);
                if (techFocusTypes.Contains(ComponentType.WeaponTractorBeam))
                {
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponGravityBeam, race, 1);
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponTractorBeam, race);
                }
                if (techFocusCategories.Contains(ComponentCategoryType.WeaponIon))
                {
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponBeam, race, 1);
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponIonCannon, race);
                }
                if (techFocusCategories.Contains(ComponentCategoryType.WeaponTorpedo))
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponTorpedo, race, 1);
                if (techFocusTypes.Contains(ComponentType.WeaponMissile))
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponMissile, race, 1);
                if (techFocusTypes.Contains(ComponentType.WeaponRailGun))
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponRailGun, race, 1);
                if (techFocusTypes.Contains(ComponentType.ComputerCountermeasures))
                    techTree.FindAndResearchLowestProject(ComponentType.ComputerCountermeasures, race);
                if (techFocusTypes.Contains(ComponentType.ComputerTargetting))
                    techTree.FindAndResearchLowestProject(ComponentType.ComputerTargetting, race);
                if (techFocusTypes.Contains(ComponentType.DamageControl))
                    techTree.FindAndResearchLowestProject(ComponentType.DamageControl, race);
                if (techFocusTypes.Contains(ComponentType.AssaultPod))
                    techTree.FindAndResearchLowestProject(ComponentType.AssaultPod, race);
                if (techFocusCategories.Contains(ComponentCategoryType.Fighter))
                    techTree.FindAndResearchLowestProject(ComponentType.FighterBay, race);
                if (techFocusCategories.Contains(ComponentCategoryType.Sensor))
                    techTree.FindAndResearchLowestProject(ComponentType.SensorProximityArray, race);
                ResearchNode projectForTypeAny = techTree.GetLowestProjectForTypeAny(ComponentType.WeaponBeam);
                if (projectForTypeAny != null && !projectForTypeAny.IsResearched && !techFocusTypes.Contains(ComponentType.WeaponGravityBeam) && !techFocusTypes.Contains(ComponentType.WeaponRailGun))
                    techTree.FindAndResearchLowestProject(ComponentType.WeaponBeam, race, 1);
            }
            else
                techTree.FindAndResearchLowestProject(ComponentType.WeaponBeam, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.Armor, race);
            techTree.FindAndResearchLowestProject(ComponentType.Reactor, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.Shields, race);
            techTree.FindAndResearchLowestProject(ComponentCategoryType.Construction, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.ExtractorMine, race);
            techTree.FindAndResearchLowestProject(ComponentType.EnergyCollector, race);
            techTree.FindAndResearchLowestProject(ComponentType.EngineMainThrust, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.EngineVectoring, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.HyperDrive, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.SensorResourceProfileSensor, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.HabitationColonization, race);
            techTree.FindAndResearchLowestProject(ComponentCategoryType.Storage, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.StorageDockingBay, race);
            techTree.FindAndResearchLowestProject(ComponentType.HabitationLifeSupport, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.HabitationHabModule, race, 1);
            techTree.FindAndResearchLowestProject(ComponentType.HabitationMedicalCenter, race);
            techTree.FindAndResearchLowestProject(ComponentType.HabitationRecreationCenter, race);
            techTree.FindAndResearchLowestProject(ComponentType.LabsWeaponsLab, race);
            techTree.FindAndResearchLowestProject(ComponentType.LabsEnergyLab, race);
            techTree.FindAndResearchLowestProject(ComponentType.LabsHighTechLab, race);
            techTree.FindAndResearchLowestProject(ComponentType.ComputerCommandCenter, race);
            techTree.FindAndResearchLowestProject(ComponentType.ComputerCommerceCenter, race);
            ResearchNode projectForTroopType = techTree.GetLowestProjectForTroopType(TroopType.Infantry);
            if (projectForTroopType != null)
                projectForTroopType.IsResearched = true;
            if (race != null && race.SpecialComponent != null)
            {
                ResearchNode researchNode = (ResearchNode)null;
                for (int index = 0; index < techTree.Count; ++index)
                {
                    if (techTree[index].Components != null && techTree[index].Components.Count > 0 && techTree[index].AllowedRaces != null && techTree[index].AllowedRaces.Contains(race) && (researchNode == null || techTree[index].TechLevel < researchNode.TechLevel))
                        researchNode = techTree[index];
                }
                if (researchNode != null)
                    researchNode.IsResearched = true;
            }
            if (race != null)
            {
                for (int index = 0; index < techTree.Count; ++index)
                {
                    if (techTree[index].DisallowedRaces != null && techTree[index].DisallowedRaces.Count > 0 && techTree[index].DisallowedRaces.Contains(race))
                        techTree[index].IsResearched = false;
                }
            }
            for (int index = 0; index < techTree.Count; ++index)
                techTree[index].SelfResearched = techTree[index].IsResearched;
            return techTree;
        }

        public ResearchNodeList SetTechTreeLevel(
          Galaxy galaxy,
          ResearchNodeList techTree,
          Race race,
          double techLevel,
          bool isPirate)
        {
            if (techLevel == 0.5)
            {
                EmpirePolicy policy = galaxy.LoadEmpirePolicy(race, isPirate);
                techTree = !isPirate ? this.SetTechTreeStartingDefaults(techTree, race, policy) : this.SetTechTreeStartingDefaultsPirates(techTree, race, policy);
            }
            else
            {
                for (int index = 0; index < techTree.Count; ++index)
                {
                    if (techTree[index].TechLevel <= (int)techLevel)
                        techTree[index].IsResearched = true;
                    else if ((techTree[index].Category == ComponentCategoryType.WeaponSuperArea || techTree[index].Category == ComponentCategoryType.WeaponSuperBeam || techTree[index].Category == ComponentCategoryType.WeaponSuperTorpedo) && techTree[index].IsResearched)
                    {
                        techTree[index].IsResearched = true;
                    }
                    else
                    {
                        techTree[index].IsResearched = false;
                        techTree[index].Progress = 0.0f;
                    }
                    if (techLevel > (double)(int)techLevel && techTree[index].TechLevel > (int)techLevel && (double)(techTree[index].TechLevel - 1) < techLevel && techLevel - (double)(int)techLevel > Galaxy.Rnd.NextDouble())
                        techTree[index].IsResearched = true;
                    if (race != null)
                    {
                        if (techTree[index].AllowedRaces != null && (isPirate || !techTree[index].AllowedRaces.Contains(race)))
                        {
                            techTree[index].IsResearched = false;
                            techTree[index].Progress = 0.0f;
                        }
                        if (techTree[index].DisallowedRaces != null && techTree[index].DisallowedRaces.Contains(race))
                        {
                            techTree[index].IsResearched = false;
                            techTree[index].Progress = 0.0f;
                        }
                    }
                }
                if (techLevel == 0.0)
                {
                    for (int index = 0; index < techTree.Count; ++index)
                    {
                        if (techTree[index].SpecialFunctionCode == 1)
                            techTree[index].IsResearched = true;
                        if (techTree[index].SpecialFunctionCode == 2)
                        {
                            techTree[index].IsResearched = false;
                            techTree[index].IsEnabled = false;
                            techTree[index].Progress = 0.0f;
                        }
                    }
                }
            }
            for (int index = 0; index < techTree.Count; ++index)
                techTree[index].SelfResearched = techTree[index].IsResearched;
            return techTree;
        }

        public ResearchNodeDefinitionList FindNodesByIds(List<int> researchNodeIds)
        {
            ResearchNodeDefinitionList nodesByIds = new ResearchNodeDefinitionList();
            for (int index = 0; index < researchNodeIds.Count; ++index)
            {
                ResearchNodeDefinition nodeById = this.FindNodeById(researchNodeIds[index]);
                if (nodeById != null)
                    nodesByIds.Add(nodeById);
            }
            return nodesByIds;
        }

        public static int IndexNodeById(ResearchNodeDefinition[] researchNodes, int researchNodeId)
        {
            for (int index = 0; index < researchNodes.Length; ++index)
            {
                if (researchNodes[index].ResearchNodeId == researchNodeId)
                    return index;
            }
            return -1;
        }

        public int IndexNodeById(int researchNodeId)
        {
            for (int index = 0; index < this.Count; ++index)
            {
                if (this[index].ResearchNodeId == researchNodeId)
                    return index;
            }
            return -1;
        }

        public ResearchNodeDefinition FindNodeById(int researchNodeId)
        {
            ResearchNodeDefinition nodeById = (ResearchNodeDefinition)null;
            for (int index = 0; index < this.Count; ++index)
            {
                if (this[index].ResearchNodeId == researchNodeId)
                    return this[index];
            }
            return nodeById;
        }

        public ResearchNodeDefinition FindNodeBySpecialFunctionCode(int specialFunctionCode)
        {
            ResearchNodeDefinition specialFunctionCode1 = (ResearchNodeDefinition)null;
            for (int index = 0; index < this.Count; ++index)
            {
                if (this[index].SpecialFunctionCode == specialFunctionCode)
                    return this[index];
            }
            return specialFunctionCode1;
        }

        public ResearchNodeDefinitionList FindAllNodesBySpecialFunctionCode(int specialFunctionCode)
        {
            ResearchNodeDefinitionList specialFunctionCode1 = new ResearchNodeDefinitionList();
            for (int index = 0; index < this.Count; ++index)
            {
                if (this[index].SpecialFunctionCode == specialFunctionCode)
                    specialFunctionCode1.Add(this[index]);
            }
            return specialFunctionCode1;
        }

        public ComponentList ResolveRaceSpecificComponents(Race race) => this.ResolveRaceSpecificComponents(race, false);

        public ComponentList ResolveRaceSpecificComponents(Race race, bool includeImprovements)
        {
            ComponentList componentList = new ComponentList();
            for (int index1 = 0; index1 < this.Count; ++index1)
            {
                if (this[index1].AllowedRaces != null && this[index1].AllowedRaces.Count > 0 && this[index1].AllowedRaces.Contains(race))
                {
                    if (this[index1].Components != null && this[index1].Components.Count > 0)
                        componentList.AddRange((IEnumerable<Component>)this[index1].Components);
                    if (includeImprovements && this[index1].ComponentImprovements != null && this[index1].ComponentImprovements.Count > 0)
                    {
                        for (int index2 = 0; index2 < this[index2].ComponentImprovements.Count; ++index2)
                        {
                            if (!componentList.Contains(this[index1].ComponentImprovements[index2].ImprovedComponent))
                                componentList.Add(this[index1].ComponentImprovements[index2].ImprovedComponent);
                        }
                    }
                }
            }
            return componentList;
        }

        public ResearchNodeDefinition ResolveResearchNodeForComponent(Component component)
        {
            if (component != null)
            {
                for (int index1 = 0; index1 < this.Count; ++index1)
                {
                    if (this[index1].Components != null && this[index1].Components.Count > 0)
                    {
                        for (int index2 = 0; index2 < this[index1].Components.Count; ++index2)
                        {
                            if (this[index1].Components[index2].ComponentID == component.ComponentID)
                                return this[index1];
                        }
                    }
                }
            }
            return (ResearchNodeDefinition)null;
        }

        public ResearchAbility CheckAncestorsForAbility(
          ResearchNodeDefinition startingNode,
          ResearchAbilityType abilityType)
        {
            if (startingNode.Abilities != null && startingNode.Abilities.Count > 0)
            {
                for (int index = 0; index < startingNode.Abilities.Count; ++index)
                {
                    if (startingNode.Abilities[index].Type == abilityType)
                        return startingNode.Abilities[index];
                }
            }
            ResearchAbility researchAbility = (ResearchAbility)null;
            if (startingNode.ParentNodes != null && startingNode.ParentNodes.Count > 0)
            {
                for (int index = 0; index < startingNode.ParentNodes.Count; ++index)
                {
                    researchAbility = this.CheckAncestorsForAbility(Galaxy.ResearchNodeDefinitionsStatic[startingNode.ParentNodes[index].ResearchNodeId], abilityType);
                    if (researchAbility != null)
                        return researchAbility;
                }
            }
            return researchAbility;
        }

        public ResearchNodeDefinitionList Clone()
        {
            ResearchNodeDefinitionList nodeDefinitionList = new ResearchNodeDefinitionList();
            nodeDefinitionList.AddRange((IEnumerable<ResearchNodeDefinition>)this);
            return nodeDefinitionList;
        }

        private void GetProjIdCount(StreamReader sR, out int projIdCount)
        {
            projIdCount = 0;
            ReadOnlySpan<char> temp;
            while (!sR.EndOfStream && (temp = sR.ReadLine()) != null)
            {
                if (temp.StartsWith("project", StringComparison.InvariantCultureIgnoreCase))
                {
                    int pos = temp.IndexOf(';');
                    if (pos != -1)
                    {
                        var values = temp.Slice(pos + 1);
                        int pos2 = values.IndexOf(",");
                        if (pos2 != -1)
                        {
                            projIdCount = Math.Max(projIdCount, int.Parse(values.Slice(0, pos2)));
                        }
                    }
                }
            }
            if (projIdCount != 0) { projIdCount++; }
        }
    }
}
