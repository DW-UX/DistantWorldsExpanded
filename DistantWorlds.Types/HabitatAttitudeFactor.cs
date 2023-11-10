// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.HabitatAttitudeFactor
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class HabitatAttitudeFactor : IComparable<HabitatAttitudeFactor>
  {
    private double _Value;
    private string _Description;

    public HabitatAttitudeFactor(double value, string description)
    {
      this._Value = value;
      this._Description = description;
    }

    public double Value
    {
      get => this._Value;
      set => this._Value = value;
    }

    public string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    int IComparable<HabitatAttitudeFactor>.CompareTo(HabitatAttitudeFactor other) => this.Value.CompareTo(other.Value);
  }
}
