// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.CommandAction
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public enum CommandAction : byte
  {
    Hold,
    ImpulseTo,
    MoveTo,
    SprintTo,
    HyperTo,
    ConditionalHyperTo,
    Escort,
    Dock,
    Undock,
    Load,
    Unload,
    Attack,
    Refuel,
    Build,
    Scrap,
    Retrofit,
    Repair,
    SelfDestruct,
    RepeatSubsequentCommands,
    EvaluateThreats,
    SelectTargetToAttack,
    ReassignMission,
    SetParent,
    ClearParent,
    ClearAttackers,
    Blockade,
    Colonize,
    ExtractResources,
    ScanArea,
    Deploy,
    Undeploy,
    Bombard,
    Capture,
    Raid,
    HoldSyncFleet,
  }
}
