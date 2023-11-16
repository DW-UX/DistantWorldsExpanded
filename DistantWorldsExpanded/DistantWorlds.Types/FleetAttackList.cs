// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.FleetAttackList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class FleetAttackList : SyncList<FleetAttack>
  {
    public int IndexOf(ShipGroup fleet)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        FleetAttack fleetAttack = this[index];
        if (fleetAttack != null && fleetAttack.Fleet == fleet)
          return index;
      }
      return -1;
    }

    public int IndexOf(BuiltObject planetDestroyer)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        FleetAttack fleetAttack = this[index];
        if (fleetAttack != null && fleetAttack.PlanetDestroyer == planetDestroyer)
          return index;
      }
      return -1;
    }
  }
}
