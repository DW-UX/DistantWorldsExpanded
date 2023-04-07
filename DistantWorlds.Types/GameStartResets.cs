// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GameStartResets
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class GameStartResets
  {
    public string GalaxyFilepath = string.Empty;
    public bool ResetResources;
    public bool ResetSceneryResearch;
    public bool ResetCreatures;
    public bool ResetRuins;
    public bool ResetSpecialLocationsAndAbandonedShips;
  }
}
