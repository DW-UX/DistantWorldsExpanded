// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.VictoryConditions
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class VictoryConditions
  {
    private bool _Territory;
    private double _TerritoryPercent;
    private bool _Population;
    private double _PopulationPercent;
    private bool _Economy;
    private double _EconomyPercent;
    private bool _TimeLimit;
    private long _TimeLimitDate;
    private long _StartDate;
    private bool _EnableStoryEvents;
    private Habitat _DefendHabitat;
    private Empire _DefendHabitatEmpire;
    private Habitat _TargetHabitat;
    private Empire _TargetHabitatEmpire;
    public bool EnableDisasterEvents = true;
    public bool EnableRaceSpecificEvents = true;
    public bool EnableRaceSpecificVictoryConditions = true;
    [OptionalField]
    public bool EnableStoryEventsShadows = true;
    public double VictoryThresholdPercentage = 1.0;

    public Habitat DefendHabitat
    {
      get => this._DefendHabitat;
      set => this._DefendHabitat = value;
    }

    public Empire DefendHabitatEmpire
    {
      get => this._DefendHabitatEmpire;
      set => this._DefendHabitatEmpire = value;
    }

    public Habitat TargetHabitat
    {
      get => this._TargetHabitat;
      set => this._TargetHabitat = value;
    }

    public Empire TargetHabitatEmpire
    {
      get => this._TargetHabitatEmpire;
      set => this._TargetHabitatEmpire = value;
    }

    public bool EnableStoryEvents
    {
      get => this._EnableStoryEvents;
      set => this._EnableStoryEvents = value;
    }

    public double TerritoryPercent
    {
      get => this._TerritoryPercent;
      set => this._TerritoryPercent = value;
    }

    public double PopulationPercent
    {
      get => this._PopulationPercent;
      set => this._PopulationPercent = value;
    }

    public double EconomyPercent
    {
      get => this._EconomyPercent;
      set => this._EconomyPercent = value;
    }

    public bool Territory
    {
      get => this._Territory;
      set => this._Territory = value;
    }

    public bool Population
    {
      get => this._Population;
      set => this._Population = value;
    }

    public bool Economy
    {
      get => this._Economy;
      set => this._Economy = value;
    }

    public bool TimeLimit
    {
      get => this._TimeLimit;
      set => this._TimeLimit = value;
    }

    public long TimeLimitDate
    {
      get => this._TimeLimitDate;
      set => this._TimeLimitDate = value;
    }

    public long StartDate
    {
      get => this._StartDate;
      set => this._StartDate = value;
    }
  }
}
