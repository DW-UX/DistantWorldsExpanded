// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EventActionType
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public enum EventActionType : byte
  {
    Undefined,
    AcquireBuiltObject,
    AcquireHabitat,
    DestroyBuiltObject,
    FindMoneyTreasure,
    LearnExplorationInfo,
    LearnTech,
    UnlockTech,
    LearnGovernmentType,
    LearnAboutSpecialLocation,
    LearnAboutLostColony,
    SleepingRaceAwokenAtHabitat,
    SplitEmpirePeacefully,
    SplitEmpireCivilWar,
    EnemyFleetDefectsToTriggerEmpire,
    PirateFactionJoinsTriggerEmpire,
    EmpireDeclaresWarOnTriggerEmpire,
    ChangeEmpireGovernment,
    StartPlague,
    EndPlague,
    GenerateBuiltObject,
    GenerateCreatureSwarm,
    GeneratePirateAmbush,
    GenerateRefugeeFleet,
    GenerateNewEmpire,
    GenerateNewPirateFaction,
    GenerateErutkah,
    MakeEmpireContact,
    InterceptResource,
    GenerateResourceAtHabitat,
    RemoveResourceAtHabitat,
    DisasterAtColony,
    BuildPlanetaryFacility,
    DestroyPlanetaryFacility,
    RevealObject,
    ChangeRaceBias,
    ChangeEmpireReputation,
    ChangeEmpireEvaluation,
    InitiateTreaty,
    BreakTreaty,
    StartTradingSuperLuxuryResources,
    StopTradingSuperLuxuryResources,
    GeneralMessageToEmpire,
    EmpireMessageToEmpire,
    ResearchBonusInProject,
    UnlockTechForEmpire,
    EmpireDeclaresWarOnOtherEmpire,
    VictoryConditionBonus,
    SendFleetAttack,
    SendPlanetDestroyerAttack,
    IntergalacticConvoyMilitary,
    IntergalacticConvoyCivilian,
    CharacterGenerate,
    CharacterKill,
    CharacterChangeEmpire,
    CharacterChangeRole,
    CharacterChangeImage,
  }
}
