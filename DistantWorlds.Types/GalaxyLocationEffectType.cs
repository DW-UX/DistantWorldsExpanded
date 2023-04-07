// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GalaxyLocationEffectType
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public enum GalaxyLocationEffectType : byte
  {
    None = 0,
    HyperjumpDisabled = 1,
    MovementSlowed = 2,
    LightningDamage = 4,
    ShieldReduction = 8,
    ShipDamage = 16, // 0x10
    ShipPull = 32, // 0x20
  }
}
