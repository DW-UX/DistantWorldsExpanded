// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.InvasionStats
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class InvasionStats
  {
    public Habitat Colony;
    public float TroopsDamageToInvaders;
    public float TroopsDamageToDefenders;
    public int DestroyedInvadingTroops;
    public int DestroyedDefendingTroops;
    public bool InvasionSucceeded;
    public Empire InvadingEmpire;
    public Empire DefendingEmpire;

    public InvasionStats(Habitat colony, Empire invadingEmpire, Empire defendingEmpire)
    {
      this.Colony = colony;
      this.InvadingEmpire = invadingEmpire;
      this.DefendingEmpire = defendingEmpire;
    }
  }
}
