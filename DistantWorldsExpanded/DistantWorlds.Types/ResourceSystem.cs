// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResourceSystem
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResourceSystem
  {
    public ResourceDefinitionList Resources = new ResourceDefinitionList();
    public ResourceDefinitionList StrategicResources = new ResourceDefinitionList();
    public ResourceDefinitionList LuxuryResources = new ResourceDefinitionList();
    public ResourceDefinitionList SuperLuxuryResources = new ResourceDefinitionList();
    public ResourceDefinitionList GasStrategicResources = new ResourceDefinitionList();
    public ResourceDefinitionList MineralStrategicResources = new ResourceDefinitionList();
    public ResourceDefinitionList FuelResources = new ResourceDefinitionList();
    [OptionalField]
    public ResourceDefinitionList ColonyManufacturedResources = new ResourceDefinitionList();
    public ResourceDefinitionList StrategicResourcesOrderedByRelativeImportance = new ResourceDefinitionList();

    public void CheckResourcesForConflictingTypesAtHabitats(string filepath)
    {
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.Ammonia, false, true);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.Argon, false, true);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.BarrenRock, false, false);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.BarrenRock, true, false);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.CarbonDioxide, false, true);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.Chlorine, false, true);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.Continental, false, false);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.Desert, false, false);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.FrozenGasGiant, false, false);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.GasGiant, false, false);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.Helium, false, true);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.Hydrogen, false, true);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.Ice, false, false);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.Ice, true, false);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.MarshySwamp, false, false);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.Metal, true, false);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.NitrogenOxygen, false, true);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.Ocean, false, false);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.Oxygen, false, true);
      this.CheckHabitatTypeForMineralAndGasResources(filepath, HabitatType.Volcanic, false, false);
    }

    private string GenerateHabitatResourceConflictMessage(
      string filepath,
      HabitatType habitatType,
      bool habitatIsAsteroid,
      bool habitatIsGasCloud,
      ResourceDefinitionList mineralResources,
      ResourceDefinitionList gasResources)
    {
      string empty = string.Empty;
      string str1 = string.Empty;
      for (int index = 0; index < gasResources.Count; ++index)
        str1 = str1 + gasResources[index].Name + ", ";
      for (int index = 0; index < mineralResources.Count; ++index)
        str1 = str1 + mineralResources[index].Name + ", ";
      string str2 = str1.Substring(0, str1.Length - 2);
      return !habitatIsAsteroid ? (!habitatIsGasCloud ? string.Format("File {0} incorrectly defines {1} Planets/Moons with both gas and mineral resources: {2}", (object) filepath, (object) Galaxy.ResolveDescription(habitatType), (object) str2) : string.Format("File {0} incorrectly defines {1} Gas Clouds with both gas and mineral resources: {2}", (object) filepath, (object) Galaxy.ResolveDescription(habitatType), (object) str2)) : string.Format("File {0} incorrectly defines {1} Asteroids with both gas and mineral resources: {2}", (object) filepath, (object) Galaxy.ResolveDescription(habitatType), (object) str2);
    }

    private void CheckHabitatTypeForMineralAndGasResources(
      string filepath,
      HabitatType habitatType,
      bool habitatIsAsteroid,
      bool habitatIsGasCloud)
    {
      ResourceDefinitionList resourceDefinitionList = new ResourceDefinitionList();
      for (int index1 = 0; index1 < this.Resources.Count; ++index1)
      {
        ResourceDefinition resource = this.Resources[index1];
        if (resource != null)
        {
          for (int index2 = 0; index2 < resource.Prevalence.Count; ++index2)
          {
            ResourcePrevalence resourcePrevalence = resource.Prevalence[index2];
            if (resourcePrevalence != null && resourcePrevalence.HabitatType == habitatType && resourcePrevalence.HabitatIsAsteroid == habitatIsAsteroid && resourcePrevalence.HabitatIsGasCloud == habitatIsGasCloud)
              resourceDefinitionList.Add(resource);
          }
        }
      }
      ResourceDefinitionList mineralResources = new ResourceDefinitionList();
      ResourceDefinitionList gasResources = new ResourceDefinitionList();
      for (int index = 0; index < resourceDefinitionList.Count; ++index)
      {
        ResourceDefinition resourceDefinition = resourceDefinitionList[index];
        if (resourceDefinition != null)
        {
          if (resourceDefinition.Group == ResourceGroup.Mineral)
            mineralResources.Add(resourceDefinition);
          else if (resourceDefinition.Group == ResourceGroup.Gas)
            gasResources.Add(resourceDefinition);
        }
      }
      if (mineralResources.Count > 0 && gasResources.Count > 0)
        throw new ApplicationException(this.GenerateHabitatResourceConflictMessage(filepath, habitatType, habitatIsAsteroid, habitatIsGasCloud, mineralResources, gasResources));
    }

    public void Initialize(ResourceSystem resourceSystem) => this.Initialize(resourceSystem, Galaxy.ComponentDefinitionsStatic);

    public void Initialize(ResourceSystem resourceSystem, ComponentDefinition[] components)
    {
      this.Clear();
      this.Resources.AddRange((IEnumerable<ResourceDefinition>) resourceSystem.Resources);
      this.Update(components);
    }

    public void Clear()
    {
      this.Resources.Clear();
      this.StrategicResources.Clear();
      this.LuxuryResources.Clear();
      this.SuperLuxuryResources.Clear();
      this.GasStrategicResources.Clear();
      this.MineralStrategicResources.Clear();
      this.FuelResources.Clear();
      this.StrategicResourcesOrderedByRelativeImportance.Clear();
      if (this.ColonyManufacturedResources == null)
        this.ColonyManufacturedResources = new ResourceDefinitionList();
      this.ColonyManufacturedResources.Clear();
    }

    public void Update() => this.Update(Galaxy.ComponentDefinitionsStatic);

    public void Update(ComponentDefinition[] components)
    {
      this.StrategicResources.Clear();
      this.LuxuryResources.Clear();
      this.SuperLuxuryResources.Clear();
      this.GasStrategicResources.Clear();
      this.MineralStrategicResources.Clear();
      this.FuelResources.Clear();
      if (this.ColonyManufacturedResources == null)
        this.ColonyManufacturedResources = new ResourceDefinitionList();
      this.ColonyManufacturedResources.Clear();
      for (int index = 0; index < this.Resources.Count; ++index)
      {
        ResourceDefinition resource = this.Resources[index];
        if (resource != null)
        {
          switch (resource.Group)
          {
            case ResourceGroup.Mineral:
              this.StrategicResources.Add(resource);
              this.MineralStrategicResources.Add(resource);
              break;
            case ResourceGroup.Gas:
              this.StrategicResources.Add(resource);
              this.GasStrategicResources.Add(resource);
              break;
            case ResourceGroup.Luxury:
              this.LuxuryResources.Add(resource);
              break;
          }
          if (resource.SuperLuxuryBonusAmount > 0)
            this.SuperLuxuryResources.Add(resource);
          if (resource.IsFuel)
            this.FuelResources.Add(resource);
          if (resource.ColonyManufacturingLevel > 0)
            this.ColonyManufacturedResources.Add(resource);
        }
      }
      this.CalculateRelativeImportanceLevels(this.Resources, components);
      this.StrategicResourcesOrderedByRelativeImportance.Clear();
      this.StrategicResourcesOrderedByRelativeImportance.AddRange((IEnumerable<ResourceDefinition>) this.StrategicResources);
      this.StrategicResourcesOrderedByRelativeImportance.SortByRelativeImportance();
    }

    public void CalculateRelativeImportanceLevels(
      ResourceDefinitionList resources,
      ComponentDefinition[] components)
    {
      if (components == null || components.Length <= 0)
        return;
      int[] numArray = new int[resources.Count];
      for (int index1 = 0; index1 < components.Length; ++index1)
      {
        ComponentDefinition componentDefinition = (ComponentDefinition) null;
        if (index1 >= 0 && index1 < components.Length)
          componentDefinition = components[index1];
        if (componentDefinition != null && componentDefinition.RequiredResources != null && componentDefinition.Size < 100)
        {
          for (int index2 = 0; index2 < componentDefinition.RequiredResources.Count; ++index2)
          {
            ComponentResource requiredResource = componentDefinition.RequiredResources[index2];
            if (requiredResource != null && numArray.Length > (int) requiredResource.ResourceID)
              numArray[(int) requiredResource.ResourceID] += (int) requiredResource.Quantity;
          }
        }
      }
      int num1 = 0;
      for (int index = 0; index < numArray.Length; ++index)
      {
        if (numArray[index] > num1)
          num1 = numArray[index];
      }
      for (int index = 0; index < resources.Count; ++index)
      {
        ResourceDefinition resource = resources[index];
        if (resource != null)
        {
          resource.RelativeImportance = 0.0f;
          if (resource.IsFuel)
            ++resource.RelativeImportance;
          if ((double) resource.ColonyGrowthResourceLevel > 0.0)
            resource.RelativeImportance += resource.ColonyGrowthResourceLevel * 0.5f;
          if (numArray.Length > (int) resource.ResourceID)
          {
            float num2 = (float) numArray[(int) resource.ResourceID] / (float) num1;
            resource.RelativeImportance += num2;
          }
        }
      }
    }

    public ResourceDefinitionList GenerateRandomOrderedResources()
    {
      ResourceDefinitionList orderedResources = new ResourceDefinitionList();
      ResourceDefinitionList resourceDefinitionList = new ResourceDefinitionList();
      resourceDefinitionList.AddRange((IEnumerable<ResourceDefinition>) this.Resources);
      while (resourceDefinitionList.Count > 0)
      {
        int index = Galaxy.CryptoRnd.Next(0, resourceDefinitionList.Count);
        if (index >= 0 && index < resourceDefinitionList.Count)
        {
          ResourceDefinition resourceDefinition = resourceDefinitionList[index];
          if (resourceDefinition != null)
            orderedResources.Add(resourceDefinition);
          resourceDefinitionList.RemoveAt(index);
        }
      }
      return orderedResources;
    }

    private bool CheckSequentialIds(ResourceDefinitionList definitions)
    {
      byte num = 0;
      for (int index = 0; index < definitions.Count; ++index)
      {
        ResourceDefinition definition = definitions[index];
        if (definition != null)
        {
          if ((int) definition.ResourceID != (int) num)
            return false;
          ++num;
        }
      }
      return true;
    }

    public void LoadFromFile(string filePath)
    {
      ResourceDefinitionList resourceDefinitionList = new ResourceDefinitionList();
      int num1 = 0;
      try
      {
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
          using (StreamReader streamReader = new StreamReader((Stream) fileStream))
          {
            while (!streamReader.EndOfStream)
            {
              ++num1;
              string str = streamReader.ReadLine();
              if (!string.IsNullOrEmpty(str) && str.Trim() != string.Empty && str.Trim().Substring(0, 1) != "'")
              {
                //if (resourceDefinitionList.Count > 80)
                //  throw new ApplicationException("Exceeded maximum resource count in " + filePath + ". Cannot define more than 80 resources.");
                byte result1 = 0;
                string empty = string.Empty;
                int result2 = 0;
                float result3 = 0.0f;
                int result4 = 0;
                float result5 = 0.0f;
                int result6 = 0;
                int startIndex1 = 0;
                int startIndex2;
                try
                {
                  int num2 = str.IndexOf(",", startIndex1);
                  if (num2 < 0)
                    throw new ApplicationException("Could not read ResourceId at line " + num1.ToString() + " of file " + filePath);
                  if (!byte.TryParse(str.Substring(startIndex1, num2 - startIndex1).Trim(), out result1))
                    throw new ApplicationException("Could not read ResourceId at line " + num1.ToString() + " of file " + filePath);
                  startIndex2 = num2 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read ResourceId at line " + num1.ToString() + " of file " + filePath);
                }
                string name;
                int startIndex3;
                try
                {
                  int num3 = str.IndexOf(",", startIndex2);
                  if (num3 < 0)
                    throw new ApplicationException("Could not read Name at line " + num1.ToString() + " of file " + filePath);
                  name = str.Substring(startIndex2, num3 - startIndex2).Trim();
                  startIndex3 = num3 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Name at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex4;
                try
                {
                  int num4 = str.IndexOf(",", startIndex3);
                  if (num4 < 0)
                    throw new ApplicationException("Could not read PictureRef at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str.Substring(startIndex3, num4 - startIndex3).Trim(), out result2))
                    throw new ApplicationException("Could not read PictureRef at line " + num1.ToString() + " of file " + filePath);
                  startIndex4 = num4 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read PictureRef at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex5;
                try
                {
                  int num5 = str.IndexOf(",", startIndex4);
                  if (num5 < 0)
                    throw new ApplicationException("Could not read BasePrice at line " + num1.ToString() + " of file " + filePath);
                  if (!float.TryParse(str.Substring(startIndex4, num5 - startIndex4).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result3))
                    throw new ApplicationException("Could not read BasePrice at line " + num1.ToString() + " of file " + filePath);
                  startIndex5 = num5 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read BasePrice at line " + num1.ToString() + " of file " + filePath);
                }
                ResourceGroup group;
                int startIndex6;
                try
                {
                  int num6 = str.IndexOf(",", startIndex5);
                  if (num6 < 0)
                    throw new ApplicationException("Could not read Resource Group at line " + num1.ToString() + " of file " + filePath);
                  byte result7;
                  if (!byte.TryParse(str.Substring(startIndex5, num6 - startIndex5).Trim(), out result7))
                    throw new ApplicationException("Could not read Resource Group at line " + num1.ToString() + " of file " + filePath);
                  switch (result7)
                  {
                    case 0:
                      group = ResourceGroup.Mineral;
                      break;
                    case 1:
                      group = ResourceGroup.Gas;
                      break;
                    case 2:
                      group = ResourceGroup.Luxury;
                      break;
                    default:
                      throw new ApplicationException("Invalid Resource Group at line " + num1.ToString() + " of file " + filePath);
                  }
                  startIndex6 = num6 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Resource Group at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex7;
                try
                {
                  int num7 = str.IndexOf(",", startIndex6);
                  if (num7 < 0)
                    throw new ApplicationException("Could not read SuperLuxuryBonusAmount at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str.Substring(startIndex6, num7 - startIndex6).Trim(), out result4))
                    throw new ApplicationException("Could not read SuperLuxuryBonusAmount at line " + num1.ToString() + " of file " + filePath);
                  startIndex7 = num7 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read SuperLuxuryBonusAmount at line " + num1.ToString() + " of file " + filePath);
                }
                bool isFuel;
                int startIndex8;
                try
                {
                  int num8 = str.IndexOf(",", startIndex7);
                  if (num8 < 0)
                    throw new ApplicationException("Could not read IsFuel at line " + num1.ToString() + " of file " + filePath);
                  switch (str.Substring(startIndex7, num8 - startIndex7).Trim().ToLower(CultureInfo.InvariantCulture))
                  {
                    case "y":
                      isFuel = true;
                      break;
                    case "n":
                      isFuel = false;
                      break;
                    default:
                      throw new ApplicationException("Invalid IsFuel at line " + num1.ToString() + " of file " + filePath);
                  }
                  startIndex8 = num8 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read IsFuel at line " + num1.ToString() + " of file " + filePath);
                }
                bool isImportantPreWarpResource;
                int startIndex9;
                try
                {
                  int num9 = str.IndexOf(",", startIndex8);
                  if (num9 < 0)
                    throw new ApplicationException("Could not read IsImportantPreWarpResource at line " + num1.ToString() + " of file " + filePath);
                  switch (str.Substring(startIndex8, num9 - startIndex8).Trim().ToLower(CultureInfo.InvariantCulture))
                  {
                    case "y":
                      isImportantPreWarpResource = true;
                      break;
                    case "n":
                      isImportantPreWarpResource = false;
                      break;
                    default:
                      throw new ApplicationException("Invalid IsImportantPreWarpResource at line " + num1.ToString() + " of file " + filePath);
                  }
                  startIndex9 = num9 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read IsImportantPreWarpResource at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex10;
                try
                {
                  int num10 = str.IndexOf(",", startIndex9);
                  if (num10 < 0)
                    throw new ApplicationException("Could not read ColonyGrowthResourceLevel at line " + num1.ToString() + " of file " + filePath);
                  if (!float.TryParse(str.Substring(startIndex9, num10 - startIndex9).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result5))
                    throw new ApplicationException("Could not read ColonyGrowthResourceLevel at line " + num1.ToString() + " of file " + filePath);
                  if ((double) result5 < 0.0 || (double) result5 > 1.0)
                    throw new ApplicationException("Invalid ColonyGrowthResourceLevel at line " + num1.ToString() + " of file " + filePath);
                  startIndex10 = num10 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read ColonyGrowthResourceLevel at line " + num1.ToString() + " of file " + filePath);
                }
                int num11;
                int startIndex11;
                try
                {
                  num11 = str.IndexOf(",", startIndex10);
                  if (num11 < 0)
                    throw new ApplicationException("Could not read ColonyManufacturingLevel at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str.Substring(startIndex10, num11 - startIndex10).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result6))
                    throw new ApplicationException("Could not read ColonyManufacturingLevel at line " + num1.ToString() + " of file " + filePath);
                  if (result6 < 0 || result6 > 10000)
                    throw new ApplicationException("Invalid ColonyManufacturingLevel at line " + num1.ToString() + " of file " + filePath);
                  startIndex11 = num11 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read ColonyGrowthResourceLevel at line " + num1.ToString() + " of file " + filePath);
                }
                ResourceDefinition resourceDefinition = new ResourceDefinition(result1, name, result2, result3, group, result4, isFuel, isImportantPreWarpResource, result5, result6);
                for (; num11 >= 0; num11 = startIndex11 >= str.Length ? -1 : str.IndexOf(",", startIndex11))
                {
                  int result8 = 0;
                  float result9 = 0.0f;
                  float result10 = 0.0f;
                  float result11 = 1f;
                  int startIndex12;
                  try
                  {
                    int num12 = str.IndexOf(",", startIndex11);
                    if (num12 >= 0)
                    {
                      if (!int.TryParse(str.Substring(startIndex11, num12 - startIndex11).Trim(), out result8))
                        throw new ApplicationException("Could not read Type in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                      if (result8 < 0 || result8 > 2)
                        throw new ApplicationException("Invalid Type in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                      startIndex12 = num12 + 1;
                    }
                    else
                      throw new ApplicationException("Could not read Type in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                  }
                  catch
                  {
                    throw new ApplicationException("Could not read Type in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                  }
                  HabitatType type;
                  int startIndex13;
                  try
                  {
                    int num13 = str.IndexOf(",", startIndex12);
                    if (num13 >= 0)
                    {
                      int result12;
                      if (!int.TryParse(str.Substring(startIndex12, num13 - startIndex12).Trim(), out result12))
                        throw new ApplicationException("Could not read Sub Type in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                      type = Galaxy.ResolveHabitatTypeByIndexIncludeGasClouds(result12);
                      if (type == HabitatType.Undefined)
                        throw new ApplicationException("Invalid Sub Type in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                      startIndex13 = num13 + 1;
                    }
                    else
                      throw new ApplicationException("Could not read Sub Type in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                  }
                  catch
                  {
                    throw new ApplicationException("Could not read Sub Type in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                  }
                  int startIndex14;
                  try
                  {
                    int num14 = str.IndexOf(",", startIndex13);
                    if (num14 >= 0)
                    {
                      if (!float.TryParse(str.Substring(startIndex13, num14 - startIndex13).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result9))
                        throw new ApplicationException("Could not read prevalence value in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                      if (result4 == 0 && ((double) result9 <= 0.0 || (double) result9 > 1.0))
                        throw new ApplicationException("Invalid prevalence value in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                      startIndex14 = num14 + 1;
                    }
                    else
                      throw new ApplicationException("Could not read prevalence value in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                  }
                  catch
                  {
                    throw new ApplicationException("Could not read prevalence value in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                  }
                  int startIndex15;
                  try
                  {
                    int num15 = str.IndexOf(",", startIndex14);
                    if (num15 >= 0)
                    {
                      if (!float.TryParse(str.Substring(startIndex14, num15 - startIndex14).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result10))
                        throw new ApplicationException("Could not read Abundance Minimum in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                      if ((double) result10 <= 0.0 || (double) result10 > 1.0)
                        throw new ApplicationException("Invalid Abundance Minimum in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                      startIndex15 = num15 + 1;
                    }
                    else
                      throw new ApplicationException("Could not read Abundance Minimum in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                  }
                  catch
                  {
                    throw new ApplicationException("Could not read Abundance Minimum in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                  }
                  try
                  {
                    int num16 = str.IndexOf(",", startIndex15);
                    if (num16 >= 0)
                    {
                      if (!float.TryParse(str.Substring(startIndex15, num16 - startIndex15).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result11))
                        throw new ApplicationException("Could not read Abundance Maximum in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                      if ((double) result11 <= 0.0 || (double) result11 > 1.0 || (double) result11 <= (double) result10)
                        throw new ApplicationException("Invalid Abundance Maximum in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                      startIndex11 = num16 + 1;
                    }
                    else
                      throw new ApplicationException("Could not read Abundance Maximum in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                  }
                  catch
                  {
                    throw new ApplicationException("Could not read Abundance Maximum in Prevalence #" + (resourceDefinition.Prevalence.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                  }
                  bool isAsteroid = false;
                  bool isGasCloud = false;
                  switch (result8)
                  {
                    case 0:
                      isAsteroid = false;
                      isGasCloud = false;
                      break;
                    case 1:
                      isAsteroid = true;
                      isGasCloud = false;
                      break;
                    case 2:
                      isAsteroid = false;
                      isGasCloud = true;
                      break;
                  }
                  ResourcePrevalence resourcePrevalence = new ResourcePrevalence(isAsteroid, isGasCloud, type, result9, result10, result11);
                  resourceDefinition.Prevalence.Add(resourcePrevalence);
                }
                resourceDefinitionList.Add(resourceDefinition);
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
      resourceDefinitionList.Sort();
      if (!this.CheckSequentialIds(resourceDefinitionList))
        throw new ApplicationException("Non-sequential Resource IDs detected in file " + filePath + ". Resource ID values must start at 0 (zero) and be sequential.");
      this.Resources.AddRange((IEnumerable<ResourceDefinition>) resourceDefinitionList);
      this.Update();
      if (this.FuelResources.Count <= 0)
      {
        this.Clear();
        this.Update();
        throw new ApplicationException("Must specify at least one fuel resource in file " + filePath);
      }
      this.CheckResourcesForConflictingTypesAtHabitats(filePath);
    }
  }
}
