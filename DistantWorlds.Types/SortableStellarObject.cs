// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.SortableStellarObject
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class SortableStellarObject : IComparable<SortableStellarObject>
  {
    public StellarObject StellarObject;
    public double SortTag;

    public SortableStellarObject(StellarObject stellarObject)
    {
      this.StellarObject = stellarObject;
      this.SortTag = 0.0;
    }

    public SortableStellarObject(StellarObject stellarObject, double sortTag)
    {
      this.StellarObject = stellarObject;
      this.SortTag = sortTag;
    }

    int IComparable<SortableStellarObject>.CompareTo(SortableStellarObject other) => this.SortTag.CompareTo(other.SortTag);
  }
}
