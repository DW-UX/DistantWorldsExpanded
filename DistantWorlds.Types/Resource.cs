// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Resource
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Resource : IComparable<Resource>
  {
    public byte ResourceID;
    [NonSerialized]
    public double SortTag = double.MinValue;

    public Resource()
    {
    }

    public Resource(byte resourceId) => this.ResourceID = resourceId;

    public ResourceGroup Group => Galaxy.ResourceSystemStatic.Resources[(int) this.ResourceID].Group;

    public string Name => Galaxy.ResourceSystemStatic.Resources[(int) this.ResourceID].Name;

    public bool IsFuel => Galaxy.ResourceSystemStatic.Resources[(int) this.ResourceID].IsFuel;

    public float RelativeImportance => Galaxy.ResourceSystemStatic.Resources[(int) this.ResourceID].RelativeImportance;

    public bool IsLuxuryResource => Galaxy.ResourceSystemStatic.Resources[(int) this.ResourceID].Group == ResourceGroup.Luxury;

    public bool IsRestrictedResource => Galaxy.ResourceSystemStatic.Resources[(int) this.ResourceID].SuperLuxuryBonusAmount > 0;

    public int ColonyManufacturingLevel => Galaxy.ResourceSystemStatic.Resources[(int) this.ResourceID].ColonyManufacturingLevel;

    public ResourcePrevalanceList Prevalence => Galaxy.ResourceSystemStatic.Resources[(int) this.ResourceID].Prevalence;

    public int BasePrice => (int) Galaxy.ResourceSystemStatic.Resources[(int) this.ResourceID].BasePrice;

    public int PictureRef => Galaxy.ResourceSystemStatic.Resources[(int) this.ResourceID].PictureRef;

    public int CompareTo(Resource other) => this.SortTag > double.MinValue ? this.SortTag.CompareTo(other.SortTag) : this.Name.CompareTo(other.Name);
  }
}
