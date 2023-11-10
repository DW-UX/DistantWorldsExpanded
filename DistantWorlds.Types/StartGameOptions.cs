// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.StartGameOptions
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class StartGameOptions
  {
    public GalaxyShape GalaxyShape;
    public int GalaxySize;
    public int GalaxyHabitatQuality;
    public int GalaxyAlienLifePrevalence;
    public int GalaxyExpansion;
    public int GalaxyAggression;
    public int GalaxyResearchSpeed;
    public int GalaxySpaceCreatures;
    public int GalaxyPirates;
    public int GalaxyPirateProximity;
    public string YourEmpireName;
    public int YourEmpireMainColor;
    public int YourEmpireSecondaryColor;
    public int YourEmpireFlagShape;
    public int YourEmpireGalaxyStartLocation;
    public int YourEmpireHomeSystem;
    public int YourEmpireExpansion;
    public int YourEmpireTechLevel;
    public int YourEmpireCorruption;
    public int YourEmpireGovernmentStyle;
    public int YourEmpireRace;
    public bool OtherEmpiresAutoGen;
    public int OtherEmpiresAutoGenAmount;
    public bool OtherEmpiresAllowNewEmpiresFromIndependentColonies;
    public EmpireStartList OtherEmpires = new EmpireStartList();
    public bool VictoryConditionsTerritory;
    public int VictoryConditionsTerritoryPercent;
    public bool VictoryConditionsPopulation;
    public int VictoryConditionsPopulationPercent;
    public bool VictoryConditionsEconomy;
    public int VictoryConditionsEconomyPercent;
    public bool VictoryConditionsTimeLimit;
    public int VictoryConditionsTimeLimitYears;
    public bool VictoryConditionsApplyWhen;
    public int VictoryConditionsApplyWhenYears;
    public bool VictoryConditionsStoryEvents;
    public bool VictoryConditionsStoryEventsOriginal;
    public bool VictoryConditionsRaceSpecific;
    public bool VictoryConditionsDisasterEvents;
    public int GalaxyDifficulty;
    public bool GalaxyDifficultyScaling;
    public int GalaxyDimensions;
    public bool VictoryConditionsRaceSpecificEvents;
    public int VictoryConditionsVictoryThresholdPercent;
    [OptionalField]
    public bool DestroyedPiratesDoNotRespawn;
    [OptionalField]
    public int GalaxyPirateStrength;
    [OptionalField]
    public bool AllowTechTrading = true;
    [OptionalField]
    public bool AllowGiantKaltorGeneration = true;
    [OptionalField]
    public bool VictoryConditionsStoryEventsShadows = true;
    public int PiratePlayStyle;
    [OptionalField]
    public float ColonizationInfluenceRangeFactor = 1f;
    [OptionalField]
    public bool ColonizationRangeEnforceLimit;
    [OptionalField]
    public float ColonizationRange = 2f;
  }
}
