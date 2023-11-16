// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.VictoryConditionProgress
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class VictoryConditionProgress : IComparable<VictoryConditionProgress>
  {
    public Empire Empire;
    public double TerritoryProgress;
    public double EconomyProgress;
    public double PopulationProgress;
    public double TerritoryPercent;
    public double EconomyPercent;
    public double PopulationPercent;
    public bool TerritoryEnabled;
    public bool EconomyEnabled;
    public bool PopulationEnabled;
    [OptionalField]
    public double PirateBonusAmount;
    [OptionalField]
    public double BonusAmount;
    [OptionalField]
    public double StandingWonderBonusAmount;
    private RaceVictoryConditionProgressList _RaceVictoryConditionsProgress = new RaceVictoryConditionProgressList();

    public RaceVictoryConditionProgressList RaceVictoryConditionsProgress
    {
      get => this._RaceVictoryConditionsProgress;
      set => this._RaceVictoryConditionsProgress = value;
    }

    public VictoryConditionProgress(
      Empire empire,
      bool territoryEnabled,
      bool economyEnabled,
      bool populationEnabled,
      double territoryProgress,
      double economyProgress,
      double populationProgress,
      RaceVictoryConditionProgressList raceVictoryConditionsProgress)
    {
      this.Empire = empire;
      this.TerritoryEnabled = territoryEnabled;
      this.EconomyEnabled = economyEnabled;
      this.PopulationEnabled = populationEnabled;
      this.TerritoryProgress = territoryProgress;
      this.EconomyProgress = economyProgress;
      this.PopulationProgress = populationProgress;
      this._RaceVictoryConditionsProgress = raceVictoryConditionsProgress;
    }

    public double TotalProgress
    {
      get
      {
        int num1 = 0;
        if (this.TerritoryEnabled)
          ++num1;
        if (this.EconomyEnabled)
          ++num1;
        if (this.PopulationEnabled)
          ++num1;
        if (this._RaceVictoryConditionsProgress != null && this._RaceVictoryConditionsProgress.Count > 0)
          ++num1;
        double num2 = 1.0 / (double) num1;
        double num3 = 0.0;
        if (this.TerritoryEnabled)
          num3 += this.TerritoryProgress * num2;
        if (this.EconomyEnabled)
          num3 += this.EconomyProgress * num2;
        if (this.PopulationEnabled)
          num3 += this.PopulationProgress * num2;
        if (this._RaceVictoryConditionsProgress != null && this._RaceVictoryConditionsProgress.Count > 0)
          num3 += this._RaceVictoryConditionsProgress.TotalProgress * num2;
        return num3 + this.BonusAmount + this.PirateBonusAmount;
      }
    }

    public int GetPortionCount()
    {
      int portionCount = 0;
      if (this.TerritoryEnabled)
        ++portionCount;
      if (this.EconomyEnabled)
        ++portionCount;
      if (this.PopulationEnabled)
        ++portionCount;
      if (this._RaceVictoryConditionsProgress != null && this._RaceVictoryConditionsProgress.Count > 0)
        ++portionCount;
      return portionCount;
    }

    public void GetProgressAll(
      out double territoryProgress,
      out double economyProgress,
      out double populationProgress,
      out double raceProgress)
    {
      territoryProgress = 0.0;
      economyProgress = 0.0;
      populationProgress = 0.0;
      raceProgress = 0.0;
      double num = 1.0 / (double) this.GetPortionCount();
      if (this.TerritoryEnabled)
        territoryProgress = this.TerritoryProgress * num;
      if (this.EconomyEnabled)
        economyProgress = this.EconomyProgress * num;
      if (this.PopulationEnabled)
        populationProgress = this.PopulationProgress * num;
      if (this._RaceVictoryConditionsProgress == null || this._RaceVictoryConditionsProgress.Count <= 0)
        return;
      raceProgress = this._RaceVictoryConditionsProgress.TotalProgress * num;
    }

    int IComparable<VictoryConditionProgress>.CompareTo(VictoryConditionProgress other)
    {
      int num = this.TotalProgress.CompareTo(other.TotalProgress);
      if (num != 0 || this.Empire == null || other.Empire == null)
        return num;
      int colonyStrategicValue1 = this.Empire.TotalColonyStrategicValue;
      int colonyStrategicValue2 = other.Empire.TotalColonyStrategicValue;
      return colonyStrategicValue1 == colonyStrategicValue2 ? this.Empire.Name.CompareTo(other.Empire.Name) : colonyStrategicValue1.CompareTo(colonyStrategicValue2);
    }
  }
}
