﻿// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.TacticType
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using System.ComponentModel.DataAnnotations;

namespace BaconDistantWorlds
{
  public enum TacticType
  {
    Advance,
    Defend,
    [Display(Name = "Defence in depth")] Withdraw,
    Feign,
    Bombard,
    Encircle,
  }
}
