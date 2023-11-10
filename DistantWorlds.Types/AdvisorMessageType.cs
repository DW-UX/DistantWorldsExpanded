// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.AdvisorMessageType
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public enum AdvisorMessageType : byte
  {
    Undefined,
    BuildOrder,
    BuildOneOff,
    Colonization,
    IntelligenceMission,
    EnemyAttack,
    EnemyBombard,
    EnemyBlockade,
    EnemyAttackPlanetDestroyer,
    InvadeIndependent,
    PrepareRaid,
    DiplomaticGift,
    TreatyOffer,
    WarTradeSanctions,
    ColonyFacility,
    OfferMilitaryRefueling,
    CancelMilitaryRefueling,
    OfferMiningRights,
    CancelMiningRights,
    AllowTradeRestrictedResources,
    DisallowTradeRestrictedResources,
    ComplyTradeSanctionsOther,
    ComplyWarOther,
    DefendTerritory,
    Retrofit,
    RequestLiftTradeSanctionsOther,
    RequestEndWarOther,
    OfferPirateAttackMission,
    OfferPirateDefendMission,
    OfferPirateSmuggleMission,
    PirateRaid,
    PirateFacilityEradicate,
    AcceptPirateSmugglingMission,
    DefendTarget,
  }
}
