// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.YearlyTradeValue
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class YearlyTradeValue
  {
    private long _Year;
    private double _Value;

    public YearlyTradeValue(long year)
    {
      this._Year = year;
      this._Value = -1.0;
    }

    public YearlyTradeValue(long year, double value)
    {
      this._Year = year;
      this._Value = value;
    }

    public long Year
    {
      get => this._Year;
      set => this._Year = value;
    }

    public double Value
    {
      get => this._Value;
      set => this._Value = value;
    }
  }
}
