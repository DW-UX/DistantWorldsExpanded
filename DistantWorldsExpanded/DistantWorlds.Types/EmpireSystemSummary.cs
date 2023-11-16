// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireSystemSummary
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EmpireSystemSummary
  {
    private Empire _Empire;
    private int _TotalStrategicValue;
    private int _ColonyCount;
    private int _Firepower;

    public int ColonyCount
    {
      get => this._ColonyCount;
      set => this._ColonyCount = value;
    }

    public int Firepower
    {
      get => this._Firepower;
      set => this._Firepower = value;
    }

    public Empire Empire
    {
      get => this._Empire;
      set => this._Empire = value;
    }

    public int TotalStrategicValue
    {
      get => this._TotalStrategicValue;
      set => this._TotalStrategicValue = value;
    }
  }
}
