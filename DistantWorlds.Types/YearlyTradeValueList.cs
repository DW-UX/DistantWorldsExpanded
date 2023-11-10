// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.YearlyTradeValueList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class YearlyTradeValueList : SyncList<YearlyTradeValue>
  {
    public YearlyTradeValue GetByYear(long year)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        YearlyTradeValue byYear = this[index];
        if (byYear != null && byYear.Year == year)
          return byYear;
      }
      return (YearlyTradeValue) null;
    }

    public double TotalValue
    {
      get
      {
        double totalValue = 0.0;
        for (int index = 0; index < this.Count; ++index)
        {
          YearlyTradeValue yearlyTradeValue = this[index];
          if (yearlyTradeValue != null)
            totalValue += yearlyTradeValue.Value;
        }
        return totalValue;
      }
    }
  }
}
