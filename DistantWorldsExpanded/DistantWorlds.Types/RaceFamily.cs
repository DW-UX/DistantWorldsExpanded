// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.RaceFamily
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class RaceFamily : IComparable<RaceFamily>
  {
    public byte RaceFamilyId;
    public string Name;
    public int SpecialFunctionCode;
    public RaceFamilyBiasList Biases = new RaceFamilyBiasList();
    [NonSerialized]
    public float SortTag;

    public RaceFamily(byte raceFamilyId, string name, int specialFunctionCode)
    {
      this.RaceFamilyId = raceFamilyId;
      this.Name = name;
      this.SpecialFunctionCode = specialFunctionCode;
    }

    int IComparable<RaceFamily>.CompareTo(RaceFamily other) => (double) this.SortTag > 0.0 || (double) other.SortTag > 0.0 ? this.SortTag.CompareTo(other.SortTag) : this.RaceFamilyId.CompareTo(other.RaceFamilyId);
  }
}
