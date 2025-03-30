// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ComponentDefinitionList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace DistantWorlds.Types
{
    [Serializable]
    public class ComponentDefinitionList : List<ComponentDefinition>
    {
        public ComponentDefinition GetById(int componentID)
        {
            foreach (ComponentDefinition byId in (List<ComponentDefinition>)this)
            {
                if (byId.ComponentID == componentID)
                    return byId;
            }
            return (ComponentDefinition)null;
        }

        public new void Add(ComponentDefinition component)
        {
            if (this.GetById(component.ComponentID) != null)
                throw new ApplicationException("This component type already exists in this component list.");
            base.Add(component);
        }

        public ComponentDefinitionList GetByType(ComponentType type)
        {
            ComponentDefinitionList byType = new ComponentDefinitionList();
            for (int index = 0; index < this.Count; ++index)
            {
                ComponentDefinition component = this[index];
                if (component != null && component.Type == type)
                    byType.Add(component);
            }
            return byType;
        }

        public static List<string> ResolveWeaponSoundEffectFilenames(ComponentDefinition[] components)
        {
            List<string> stringList = new List<string>();
            for (int index = 0; index < components.Length; ++index)
            {
                ComponentDefinition component = components[index];
                if (component != null && !string.IsNullOrEmpty(component.SoundEffectFilename))
                    stringList.Add(component.SoundEffectFilename);
            }
            return stringList;
        }

        private bool CheckSequentialIds(ComponentDefinitionList definitions)
        {
            int num = 0;
            for (int index = 0; index < definitions.Count; ++index)
            {
                ComponentDefinition definition = definitions[index];
                if (definition != null)
                {
                    if (definition.ComponentID != num)
                        return false;
                    ++num;
                }
            }
            return true;
        }

        public void LoadFromFile(string filePath)
        {
            this.Clear();
            ComponentDefinitionList componentDefinitionList = new ComponentDefinitionList();
            int num1 = 0;
            //int num2 = 500;
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)fileStream))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            ++num1;
                            string str = streamReader.ReadLine();
                            if (!string.IsNullOrEmpty(str) && str.Trim() != string.Empty && str.Trim().Substring(0, 1) != "'")
                            {
                                //if (componentDefinitionList.Count > num2)
                                //  throw new ApplicationException("Exceeded maximum component count in " + filePath + ". Cannot define more than " + num2.ToString() + " components.");
                                int result1 = 0;
                                string empty1 = string.Empty;
                                int result2 = 0;
                                int result3 = 0;
                                string empty2 = string.Empty;
                                int result4 = 0;
                                int result5 = 0;
                                int result6 = 0;
                                int result7 = 0;
                                int result8 = 0;
                                int result9 = 0;
                                int result10 = 0;
                                int result11 = 0;
                                int result12 = 0;
                                int startIndex1 = 0;
                                int startIndex2;
                                try
                                {
                                    int num3 = str.IndexOf(",", startIndex1);
                                    if (num3 < 0)
                                        throw new ApplicationException("Could not read ComponentId at line " + num1.ToString() + " of file " + filePath);
                                    if (!int.TryParse(str.Substring(startIndex1, num3 - startIndex1).Trim(), out result1))
                                        throw new ApplicationException("Could not read ComponentId at line " + num1.ToString() + " of file " + filePath);
                                    startIndex2 = num3 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read ComponentId at line " + num1.ToString() + " of file " + filePath);
                                }
                                string name;
                                int startIndex3;
                                try
                                {
                                    int num4 = str.IndexOf(",", startIndex2);
                                    if (num4 < 0)
                                        throw new ApplicationException("Could not read Name at line " + num1.ToString() + " of file " + filePath);
                                    name = str.Substring(startIndex2, num4 - startIndex2).Trim();
                                    startIndex3 = num4 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read Name at line " + num1.ToString() + " of file " + filePath);
                                }
                                int startIndex4;
                                try
                                {
                                    int num5 = str.IndexOf(",", startIndex3);
                                    if (num5 < 0)
                                        throw new ApplicationException("Could not read PictureRef at line " + num1.ToString() + " of file " + filePath);
                                    if (!int.TryParse(str.Substring(startIndex3, num5 - startIndex3).Trim(), out result2))
                                        throw new ApplicationException("Could not read PictureRef at line " + num1.ToString() + " of file " + filePath);
                                    startIndex4 = num5 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read PictureRef at line " + num1.ToString() + " of file " + filePath);
                                }
                                int startIndex5;
                                try
                                {
                                    int num6 = str.IndexOf(",", startIndex4);
                                    if (num6 < 0)
                                        throw new ApplicationException("Could not read SpecialImageIndex at line " + num1.ToString() + " of file " + filePath);
                                    if (!int.TryParse(str.Substring(startIndex4, num6 - startIndex4).Trim(), out result3))
                                        throw new ApplicationException("Could not read SpecialImageIndex at line " + num1.ToString() + " of file " + filePath);
                                    startIndex5 = num6 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read SpecialImageIndex at line " + num1.ToString() + " of file " + filePath);
                                }
                                string soundEffectFilename;
                                int startIndex6;
                                try
                                {
                                    int num7 = str.IndexOf(",", startIndex5);
                                    if (num7 < 0)
                                        throw new ApplicationException("Could not read SoundEffectFilename at line " + num1.ToString() + " of file " + filePath);
                                    soundEffectFilename = str.Substring(startIndex5, num7 - startIndex5).Trim();
                                    startIndex6 = num7 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read SoundEffectFilename at line " + num1.ToString() + " of file " + filePath);
                                }
                                ComponentType componentType;
                                int startIndex7;
                                try
                                {
                                    int num8 = str.IndexOf(",", startIndex6);
                                    if (num8 < 0)
                                        throw new ApplicationException("Could not read Type at line " + num1.ToString() + " of file " + filePath);
                                    byte result13;
                                    if (!byte.TryParse(str.Substring(startIndex6, num8 - startIndex6).Trim(), out result13))
                                        throw new ApplicationException("Could not read Type at line " + num1.ToString() + " of file " + filePath);
                                    componentType = ComponentDefinition.ResolveComponentTypeFromCode((int)result13);
                                    if (componentType == ComponentType.Undefined)
                                        throw new ApplicationException("Invalid Type at line " + num1.ToString() + " of file " + filePath);
                                    startIndex7 = num8 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read Type at line " + num1.ToString() + " of file " + filePath);
                                }
                                int startIndex8;
                                try
                                {
                                    int num9 = str.IndexOf(",", startIndex7);
                                    if (num9 < 0)
                                        throw new ApplicationException("Could not read Size at line " + num1.ToString() + " of file " + filePath);
                                    if (!int.TryParse(str.Substring(startIndex7, num9 - startIndex7).Trim(), out result4))
                                        throw new ApplicationException("Could not read Size at line " + num1.ToString() + " of file " + filePath);
                                    startIndex8 = num9 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read Size at line " + num1.ToString() + " of file " + filePath);
                                }
                                int startIndex9;
                                try
                                {
                                    int num10 = str.IndexOf(",", startIndex8);
                                    if (num10 < 0)
                                        throw new ApplicationException("Could not read EnergyUsed at line " + num1.ToString() + " of file " + filePath);
                                    if (!int.TryParse(str.Substring(startIndex8, num10 - startIndex8).Trim(), out result5))
                                        throw new ApplicationException("Could not read EnergyUsed at line " + num1.ToString() + " of file " + filePath);
                                    startIndex9 = num10 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read EnergyUsed at line " + num1.ToString() + " of file " + filePath);
                                }
                                int startIndex10;
                                try
                                {
                                    int num11 = str.IndexOf(",", startIndex9);
                                    if (num11 < 0)
                                        throw new ApplicationException("Could not read Value1 at line " + num1.ToString() + " of file " + filePath);
                                    if (!int.TryParse(str.Substring(startIndex9, num11 - startIndex9).Trim(), out result6))
                                        throw new ApplicationException("Could not read Value1 at line " + num1.ToString() + " of file " + filePath);
                                    startIndex10 = num11 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read Value1 at line " + num1.ToString() + " of file " + filePath);
                                }
                                int startIndex11;
                                try
                                {
                                    int num12 = str.IndexOf(",", startIndex10);
                                    if (num12 < 0)
                                        throw new ApplicationException("Could not read Value2 at line " + num1.ToString() + " of file " + filePath);
                                    if (!int.TryParse(str.Substring(startIndex10, num12 - startIndex10).Trim(), out result7))
                                        throw new ApplicationException("Could not read Value2 at line " + num1.ToString() + " of file " + filePath);
                                    startIndex11 = num12 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read Value2 at line " + num1.ToString() + " of file " + filePath);
                                }
                                int startIndex12;
                                try
                                {
                                    int num13 = str.IndexOf(",", startIndex11);
                                    if (num13 < 0)
                                        throw new ApplicationException("Could not read Value3 at line " + num1.ToString() + " of file " + filePath);
                                    if (!int.TryParse(str.Substring(startIndex11, num13 - startIndex11).Trim(), out result8))
                                        throw new ApplicationException("Could not read Value3 at line " + num1.ToString() + " of file " + filePath);
                                    startIndex12 = num13 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read Value3 at line " + num1.ToString() + " of file " + filePath);
                                }
                                int startIndex13;
                                try
                                {
                                    int num14 = str.IndexOf(",", startIndex12);
                                    if (num14 < 0)
                                        throw new ApplicationException("Could not read Value4 at line " + num1.ToString() + " of file " + filePath);
                                    if (!int.TryParse(str.Substring(startIndex12, num14 - startIndex12).Trim(), out result9))
                                        throw new ApplicationException("Could not read Value4 at line " + num1.ToString() + " of file " + filePath);
                                    startIndex13 = num14 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read Value4 at line " + num1.ToString() + " of file " + filePath);
                                }
                                int startIndex14;
                                try
                                {
                                    int num15 = str.IndexOf(",", startIndex13);
                                    if (num15 < 0)
                                        throw new ApplicationException("Could not read Value5 at line " + num1.ToString() + " of file " + filePath);
                                    if (!int.TryParse(str.Substring(startIndex13, num15 - startIndex13).Trim(), out result10))
                                        throw new ApplicationException("Could not read Value5 at line " + num1.ToString() + " of file " + filePath);
                                    startIndex14 = num15 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read Value5 at line " + num1.ToString() + " of file " + filePath);
                                }
                                int startIndex15;
                                try
                                {
                                    int num16 = str.IndexOf(",", startIndex14);
                                    if (num16 < 0)
                                        throw new ApplicationException("Could not read Value6 at line " + num1.ToString() + " of file " + filePath);
                                    if (!int.TryParse(str.Substring(startIndex14, num16 - startIndex14).Trim(), out result11))
                                        throw new ApplicationException("Could not read Value6 at line " + num1.ToString() + " of file " + filePath);
                                    startIndex15 = num16 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read Value6 at line " + num1.ToString() + " of file " + filePath);
                                }
                                int num17;
                                int startIndex16;
                                try
                                {
                                    num17 = str.IndexOf(",", startIndex15);
                                    if (num17 < 0)
                                        throw new ApplicationException("Could not read Value7 at line " + num1.ToString() + " of file " + filePath);
                                    if (!int.TryParse(str.Substring(startIndex15, num17 - startIndex15).Trim(), out result12))
                                        throw new ApplicationException("Could not read Value7 at line " + num1.ToString() + " of file " + filePath);
                                    startIndex16 = num17 + 1;
                                }
                                catch
                                {
                                    throw new ApplicationException("Could not read Value7 at line " + num1.ToString() + " of file " + filePath);
                                }
                                ComponentDefinition component = new ComponentDefinition(result1, name, componentType, result2, result3, soundEffectFilename, result4, result5);
                                component.Value1 = result6;
                                component.Value2 = result7;
                                component.Value3 = result8;
                                component.Value4 = result9;
                                component.Value5 = result10;
                                component.Value6 = result11;
                                component.Value7 = result12;
                                for (; num17 >= 0; num17 = startIndex16 >= str.Length ? -1 : str.IndexOf(",", startIndex16))
                                {
                                    byte result14 = byte.MaxValue;
                                    int result15 = 0;
                                    int startIndex17;
                                    try
                                    {
                                        int num18 = str.IndexOf(",", startIndex16);
                                        if (num18 >= 0)
                                        {
                                            if (!byte.TryParse(str.Substring(startIndex16, num18 - startIndex16).Trim(), out result14))
                                                throw new ApplicationException("Could not read ResourceId in Required Resource #" + (component.RequiredResources.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                                            if ((int)result14 >= Galaxy.ResourceSystemStatic.Resources.Count)
                                                throw new ApplicationException("Invalid ResourceId in Required Resource #" + (component.RequiredResources.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath + ". ResourceId must match a defined resource.");
                                            startIndex17 = num18 + 1;
                                        }
                                        else
                                            throw new ApplicationException("Could not read ResourceId in Required Resource #" + (component.RequiredResources.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                                    }
                                    catch
                                    {
                                        throw new ApplicationException("Could not read ResourceId in Prevalence #" + (component.RequiredResources.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                                    }
                                    try
                                    {
                                        int num19 = str.IndexOf(",", startIndex17);
                                        if (num19 >= 0)
                                        {
                                            if (!int.TryParse(str.Substring(startIndex17, num19 - startIndex17).Trim(), out result15))
                                                throw new ApplicationException("Could not read Amount in Required Resource #" + (component.RequiredResources.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                                            if (result15 <= 0 || result15 > (int)short.MaxValue)
                                                throw new ApplicationException("Invalid Amount in Required Resource #" + (component.RequiredResources.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath + ". Amount must be greater than zero and less than 32767.");
                                            startIndex16 = num19 + 1;
                                        }
                                        else
                                            throw new ApplicationException("Could not read Amount in Required Resource #" + (component.RequiredResources.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                                    }
                                    catch
                                    {
                                        throw new ApplicationException("Could not read Amount in Required Resource #" + (component.RequiredResources.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                                    }
                                    ComponentResource resource = new ComponentResource(result14, (short)result15);
                                    component.RequiredResources.Add(resource);
                                }
                                componentDefinitionList.Add(component);
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
            componentDefinitionList.Sort();
            if (!this.CheckSequentialIds(componentDefinitionList))
                throw new ApplicationException("Non-sequential Component IDs detected in file " + filePath + ". Component ID values must start at 0 (zero) and be sequential.");
            this.AddRange((IEnumerable<ComponentDefinition>)componentDefinitionList);
        }


        public void LoadFromFile(SQLiteDataReader reader)
        {
            this.Clear();
            ComponentDefinitionList componentDefinitionList = new ComponentDefinitionList();
            int id = 0;
            try
            {
                while (reader.Read())
                {
                    id = reader.GetInt32(reader.GetOrdinal("ID"));
                    if (id < 0)
                        throw new ApplicationException($"Could not read ComponentId at ID {id} at Components");

                    string name = reader.GetString(reader.GetOrdinal("Name"));
                    if (string.IsNullOrWhiteSpace(name))
                        throw new ApplicationException($"Could not read Name at ID {id} at Components");

                    int pictureRef = reader.GetInt32(reader.GetOrdinal("PictureRef"));
                    if (pictureRef < 0)
                        throw new ApplicationException($"Could not read PictureRef at ID {id} at Components");

                    int specialImageIndex = reader.GetInt32(reader.GetOrdinal("SpecialImageIndex"));
                    if (specialImageIndex < 0)
                        throw new ApplicationException($"Could not read SpecialImageIndex at ID {id} at Components");

                    string soundEffectFilename = reader.GetString(reader.GetOrdinal("SoundEffectFilename"));
                    //if (string.IsNullOrWhiteSpace(soundEffectFilename))
                    //    throw new ApplicationException($"Could not read SoundEffectFilename at ID {id} at Components");

                    ComponentType componentType;
                    int typeVal = reader.GetInt32(reader.GetOrdinal("Type"));
                    if (typeVal < 0)
                        throw new ApplicationException($"Could not read Type at ID {id} at Components");
                    componentType = ComponentDefinition.ResolveComponentTypeFromCode(typeVal);
                    if (componentType == ComponentType.Undefined)
                        throw new ApplicationException($"Invalid Type at ID {id} at Components");

                    int size = reader.GetInt32(reader.GetOrdinal("Size"));
                    if (size < 0)
                        throw new ApplicationException($"Could not read Size at ID {id} at Components");

                    int staticEnergyUsed = reader.GetInt32(reader.GetOrdinal("StaticEnergyUsed"));
                    if (staticEnergyUsed < 0)
                        throw new ApplicationException($"Could not read StaticEnergyUsed at ID {id} at Components");

                    int value1 = reader.GetInt32(reader.GetOrdinal("Value1"));
                    //if (value1 < 0)
                    //    throw new ApplicationException($"Could not read Value1 at ID {id} at Components");
                    int value2 = reader.GetInt32(reader.GetOrdinal("Value2"));
                    //if (value2 < 0)
                    //    throw new ApplicationException($"Could not read Value2 at ID {id} at Components");
                    int value3 = reader.GetInt32(reader.GetOrdinal("Value3"));
                    //if (value3 < 0)
                    //    throw new ApplicationException($"Could not read Value3 at ID {id} at Components");
                    int value4 = reader.GetInt32(reader.GetOrdinal("Value4"));
                    //if (value4 < 0)
                    //    throw new ApplicationException($"Could not read Value4 at ID {id} at Components");
                    int value5 = reader.GetInt32(reader.GetOrdinal("Value5"));
                    //if (value5 < 0)
                    //    throw new ApplicationException($"Could not read Value5 at ID {id} at Components");
                    int value6 = reader.GetInt32(reader.GetOrdinal("Value6"));
                    //if (value6 < 0)
                    //    throw new ApplicationException($"Could not read Value6 at ID {id} at Components");
                    int value7 = reader.GetInt32(reader.GetOrdinal("Value7"));
                    //if (value7 < 0)
                    //    throw new ApplicationException($"Could not read Value7 at ID {id} at Components");

                    ComponentDefinition component = new ComponentDefinition(id, name, componentType, pictureRef, specialImageIndex, soundEffectFilename, size, staticEnergyUsed);
                    component.Value1 = value1;
                    component.Value2 = value2;
                    component.Value3 = value3;
                    component.Value4 = value4;
                    component.Value5 = value5;
                    component.Value6 = value6;
                    component.Value7 = value7;

                    using var resourceReqReader = Main._FileDB.GetComponentResourseRequiredReader(id);
                    if (resourceReqReader.HasRows)
                    {
                        while (resourceReqReader.Read())
                        {
                            byte resId = resourceReqReader.GetByte(resourceReqReader.GetOrdinal("ResourceID"));
                            if ((int)resId >= Galaxy.ResourceSystemStatic.Resources.Count)
                                throw new ApplicationException($"Invalid ResourceId in Required Resource #{resId} at ID {id} of Components. ResourceId must match a defined resource.");

                            int resCount = resourceReqReader.GetByte(resourceReqReader.GetOrdinal("ResourceCount"));
                            if (resCount <= 0 || resCount > (int)short.MaxValue)
                                throw new ApplicationException($"Invalid Amount in Required Resource #{resCount} at ID {id} of Components. Amount must be greater than zero and less than 32767.");

                            ComponentResource resource = new ComponentResource(resId, (short)resCount);
                            component.RequiredResources.Add(resource);
                        }
                    }

                    componentDefinitionList.Add(component);


                }
            }
            catch (ApplicationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error at ID {id} of Components");
            }
            componentDefinitionList.Sort();
            if (!this.CheckSequentialIds(componentDefinitionList))
                throw new ApplicationException("Non-sequential Component IDs detected in file. Component ID values must start at 0 (zero) and be sequential.");
            this.AddRange((IEnumerable<ComponentDefinition>)componentDefinitionList);
        }
    }
}
