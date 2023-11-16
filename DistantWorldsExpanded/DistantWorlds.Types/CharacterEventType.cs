// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.CharacterEventType
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public enum CharacterEventType : byte
  {
    Undefined,
    TreatySigned,
    WarStarted,
    WarEnded,
    TradeIncome,
    TourismIncome,
    ColonyDevelopmentIncrease,
    ColonyDevelopmentDecrease,
    CashNegative,
    CashPositive,
    TroopComplete,
    IntelligenceMissionSucceedEspionage,
    IntelligenceMissionSucceedSabotage,
    IntelligenceMissionFailEspionage,
    IntelligenceMissionFailSabotage,
    IntelligenceMissionInterceptEnemy,
    IntelligenceAgentOursCaptured,
    IntelligenceAgentRecruited,
    ResearchAdvanceWeapons,
    ResearchAdvanceEnergy,
    ResearchAdvanceHighTech,
    BuildMilitaryShip,
    BuildCivilianShip,
    BuildColonyShip,
    BuildMilitaryBase,
    BuildSpaceport,
    BuildResearchStationWeapons,
    BuildResearchStationEnergy,
    BuildResearchStationHighTech,
    BuildMiningStation,
    BuildResortBase,
    BuildOtherBase,
    BuildFacility,
    BuildWonder,
    HyperjumpExit,
    SpaceBattle,
    GroundInvasion,
    TargetOfFailedAssassination,
    Subjugated,
    TreatyBroken,
    AmbassadorAssignedToEmpire,
    CriticalResearchSuccess,
    CriticalResearchFailure,
    CharacterStart,
    CharacterTraitGain,
    CharacterSkillGain,
    CharacterSkillProgress,
    CharacterTransferLocation,
    Boarding,
    Raid,
    SmugglingSuccess,
    SmugglingDetection,
  }
}
