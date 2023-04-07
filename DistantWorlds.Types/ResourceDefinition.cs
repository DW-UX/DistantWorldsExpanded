// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResourceDefinition
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResourceDefinition : IComparable<ResourceDefinition>
  {
    public byte ResourceID;
    public string Name;
    public int PictureRef;
    public float BasePrice;
    public ResourceGroup Group;
    public int SuperLuxuryBonusAmount;
    public bool IsFuel;
    public bool IsImportantPreWarpResource;
    public float ColonyGrowthResourceLevel;
    [OptionalField]
    public int ColonyManufacturingLevel;
    public float RelativeImportance;
    public ResourcePrevalanceList Prevalence = new ResourcePrevalanceList();
    [NonSerialized]
    public float SortTag;

    public ResourceDefinition(
      byte resourceID,
      string name,
      int pictureRef,
      float basePrice,
      ResourceGroup group,
      int superLuxuryBonusAmount,
      bool isFuel,
      bool isImportantPreWarpResource,
      float colonyGrowthResourceLevel,
      int colonyManufacturingLevel)
    {
      this.ResourceID = resourceID;
      this.Name = name;
      this.PictureRef = pictureRef;
      this.BasePrice = basePrice;
      this.Group = group;
      this.SuperLuxuryBonusAmount = superLuxuryBonusAmount;
      this.IsFuel = isFuel;
      this.IsImportantPreWarpResource = isImportantPreWarpResource;
      this.ColonyGrowthResourceLevel = colonyGrowthResourceLevel;
      this.ColonyManufacturingLevel = colonyManufacturingLevel;
    }

    public ResourcePrevalence GetMostTerrestrialResourcePrevalance()
    {
      ResourcePrevalence resourcePrevalance = (ResourcePrevalence) null;
      if (this.Prevalence != null)
      {
        for (int index = 0; index < this.Prevalence.Count; ++index)
        {
          ResourcePrevalence resourcePrevalence = this.Prevalence[index];
          if (resourcePrevalence != null)
          {
            if (resourcePrevalance == null)
              resourcePrevalance = resourcePrevalence;
            else if (resourcePrevalance.HabitatIsGasCloud && !resourcePrevalence.HabitatIsGasCloud)
              resourcePrevalance = resourcePrevalence;
            else if (resourcePrevalance.HabitatIsAsteroid && !resourcePrevalence.HabitatIsAsteroid && !resourcePrevalence.HabitatIsGasCloud)
              resourcePrevalance = resourcePrevalence;
          }
        }
      }
      return resourcePrevalance;
    }

    public float TotalPrevalence
    {
      get
      {
        float totalPrevalence = 0.0f;
        if (this.Prevalence != null)
        {
          for (int index = 0; index < this.Prevalence.Count; ++index)
          {
            ResourcePrevalence resourcePrevalence = this.Prevalence[index];
            if (resourcePrevalence != null)
              totalPrevalence += resourcePrevalence.Prevalence;
          }
        }
        return totalPrevalence;
      }
    }

    int IComparable<ResourceDefinition>.CompareTo(ResourceDefinition other) => (double) this.SortTag > 0.0 || (double) other.SortTag > 0.0 ? this.SortTag.CompareTo(other.SortTag) : this.ResourceID.CompareTo(other.ResourceID);
  }
}
