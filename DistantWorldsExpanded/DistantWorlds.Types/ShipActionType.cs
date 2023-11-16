// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ShipActionType
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public enum ShipActionType
  {
    Undefined,
    RecruitTroops,
    AutomateShip,
    JoinShipGroup,
    LeaveShipGroup,
    SetAsLeadShipInGroup,
    AssignShipGroupHomeColony,
    ClearQueuedMissions,
    InvestigateRuins,
    InvestigateBuiltObject,
    ColonyTaxUp1,
    ColonyTaxUp5,
    ColonyTaxDown1,
    ColonyTaxDown5,
    BuildColonize,
    FighterOptions,
    FighterBuildFighter,
    FighterBuildBomber,
    FighterLaunchFighters,
    FighterLaunchBombers,
    FighterRetrieveFighters,
    FighterRetrieveBombers,
    BuildOptions,
    ReturnToTop,
    UnautomateShip,
    CreateNewFleet,
    ColonyBuildOptions,
    BuildPlanetaryFacility,
    AssignAttack,
    FighterUpgradeAll,
    SetFleetPosture,
    SetFleetRange,
    SetFleetAttackPoint,
    SetFleetHomeBase,
    TransferCharacter,
    ColonyBuildWonder,
    BuildOptionsPrivate,
    GeneratePirateMissionAttack,
    GeneratePirateMissionDefend,
    GeneratePirateMissionSmuggling,
    GiveBuiltObject,
    DeployVirus,
    DisbandShipGroup,
    ChangePirateHomeBase,
  }
}
