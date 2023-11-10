// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PirateEconomy
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class PirateEconomy
  {
    private object _LockObject = new object();
    private PirateEconomyYear _ThisYear;
    private PirateEconomyYear _LastYear;

    public PirateEconomy(long starDate)
    {
      this._ThisYear = new PirateEconomyYear(starDate);
      this._LastYear = (PirateEconomyYear) null;
    }

    public PirateEconomyYear ThisYear => this._ThisYear;

    public PirateEconomyYear LastYear => this._LastYear;

    public void PerformExpense(double amount, PirateExpenseType type, long starDate)
    {
      lock (this._LockObject)
      {
        this.CheckSwitchYears(starDate);
        this._ThisYear.PerformExpense(amount, type);
      }
    }

    public void PerformIncome(double amount, PirateIncomeType type, long starDate)
    {
      lock (this._LockObject)
      {
        this.CheckSwitchYears(starDate);
        this._ThisYear.PerformIncome(amount, type);
      }
    }

    private bool CheckSwitchYears(long starDate)
    {
      long num = Galaxy.CalculateStartOfYear(starDate) - this.ThisYear.YearStartDate;
      if (num <= 0L)
        return false;
      if (num > Galaxy.YearLength)
      {
        this._LastYear = (PirateEconomyYear) null;
        this._ThisYear = new PirateEconomyYear(starDate);
      }
      else
      {
        this._LastYear = this.ThisYear;
        this._ThisYear = new PirateEconomyYear(starDate);
      }
      return true;
    }
  }
}
