// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResourceDefinitionList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResourceDefinitionList : List<ResourceDefinition>
  {
    public ResourceDefinition GetByName(string resourceName)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        ResourceDefinition byName = this[index];
        if (byName != null && byName.Name == resourceName)
          return byName;
      }
      return (ResourceDefinition) null;
    }

    public ResourceDefinition GetByHighestPrevalence()
    {
      float num = 0.0f;
      ResourceDefinition highestPrevalence = (ResourceDefinition) null;
      for (int index = 0; index < this.Count; ++index)
      {
        ResourceDefinition resourceDefinition = this[index];
        if (resourceDefinition != null)
        {
          float totalPrevalence = resourceDefinition.TotalPrevalence;
          if ((double) totalPrevalence > (double) num)
          {
            highestPrevalence = resourceDefinition;
            num = totalPrevalence;
          }
        }
      }
      return highestPrevalence;
    }

    public void SortByRelativeImportance()
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].SortTag = this[index].RelativeImportance;
      this.Sort();
      this.Reverse();
      for (int index = 0; index < this.Count; ++index)
        this[index].SortTag = 0.0f;
    }

    public bool CheckPrevalenceValidForHabitat(Habitat habitat, ResourcePrevalence prevalence)
    {
      if (prevalence != null && prevalence.HabitatType == habitat.Type)
      {
        bool flag = false;
        switch (habitat.Category)
        {
          case HabitatCategoryType.Planet:
          case HabitatCategoryType.Moon:
            if (!prevalence.HabitatIsAsteroid && !prevalence.HabitatIsGasCloud)
            {
              flag = true;
              break;
            }
            break;
          case HabitatCategoryType.Asteroid:
            if (prevalence.HabitatIsAsteroid && !prevalence.HabitatIsGasCloud)
            {
              flag = true;
              break;
            }
            break;
          case HabitatCategoryType.GasCloud:
            if (prevalence.HabitatIsGasCloud && !prevalence.HabitatIsAsteroid)
            {
              flag = true;
              break;
            }
            break;
        }
        if (flag)
          return true;
      }
      return false;
    }

    public ResourcePrevalanceList ResolveResourcePrevalencesForHabitat(Habitat habitat)
    {
      ResourcePrevalanceList resourcePrevalanceList = new ResourcePrevalanceList();
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        ResourceDefinition resourceDefinition = this[index1];
        if (resourceDefinition != null && resourceDefinition.Prevalence != null && resourceDefinition.Prevalence.Count > 0)
        {
          for (int index2 = 0; index2 < resourceDefinition.Prevalence.Count; ++index2)
          {
            ResourcePrevalence prevalence = resourceDefinition.Prevalence[index2];
            if (this.CheckPrevalenceValidForHabitat(habitat, prevalence))
              resourcePrevalanceList.Add(prevalence);
          }
        }
      }
      return resourcePrevalanceList;
    }

    public ResourceList ResolveValidResourcesForHabitatExcludeManufactured(Habitat habitat)
    {
      ResourceList resourceList = new ResourceList();
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        ResourceDefinition resourceDefinition = this[index1];
        if (resourceDefinition != null && resourceDefinition.ColonyManufacturingLevel <= 0 && resourceDefinition.Prevalence != null && resourceDefinition.Prevalence.Count > 0)
        {
          for (int index2 = 0; index2 < resourceDefinition.Prevalence.Count; ++index2)
          {
            ResourcePrevalence resourcePrevalence = resourceDefinition.Prevalence[index2];
            if (resourcePrevalence != null && resourcePrevalence.HabitatType == habitat.Type)
            {
              bool flag = false;
              switch (habitat.Category)
              {
                case HabitatCategoryType.Planet:
                case HabitatCategoryType.Moon:
                  if (!resourcePrevalence.HabitatIsAsteroid && !resourcePrevalence.HabitatIsGasCloud)
                  {
                    flag = true;
                    break;
                  }
                  break;
                case HabitatCategoryType.Asteroid:
                  if (resourcePrevalence.HabitatIsAsteroid && !resourcePrevalence.HabitatIsGasCloud)
                  {
                    flag = true;
                    break;
                  }
                  break;
                case HabitatCategoryType.GasCloud:
                  if (resourcePrevalence.HabitatIsGasCloud && !resourcePrevalence.HabitatIsAsteroid)
                  {
                    flag = true;
                    break;
                  }
                  break;
              }
              if (flag)
              {
                resourceList.Add(new Resource(resourceDefinition.ResourceID));
                break;
              }
            }
          }
        }
      }
      return resourceList;
    }

    public ResourceDefinitionList GetResourcesByGroup(ResourceGroup resourceGroup)
    {
      ResourceDefinitionList resourcesByGroup = new ResourceDefinitionList();
      for (int index = 0; index < this.Count; ++index)
      {
        ResourceDefinition resourceDefinition = this[index];
        if (resourceDefinition != null && resourceDefinition.Group == resourceGroup)
          resourcesByGroup.Add(resourceDefinition);
      }
      return resourcesByGroup;
    }
  }
}
