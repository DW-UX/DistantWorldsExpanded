// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.BuiltObjectMissionType
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public enum BuiltObjectMissionType : byte
  {
    Undefined,
    Explore,
    Build,
    BuildRepair,
    Transport,
    Patrol,
    Escort,
    Rescue,
    Blockade,
    Attack,
    Escape,
    Retire,
    Retrofit,
    Colonize,
    Waypoint,
    Hold,
    WaitAndAttack,
    WaitAndBombard,
    MoveAndWait,
    Refuel,
    ExtractResources,
    LoadTroops,
    UnloadTroops,
    Deploy,
    Undeploy,
    Repair,
    Move,
    Bombard,
    Capture,
    Reinforce,
    Raid,
  }
}
