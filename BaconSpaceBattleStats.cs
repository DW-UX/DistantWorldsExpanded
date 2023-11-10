// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconSpaceBattleStats
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds.Types;
using System;

namespace BaconDistantWorlds
{
  public static class BaconSpaceBattleStats
  {
    public static void AddLatestCombatStats(BuiltObject ship, SpaceBattleStats battleStats)
    {
      if (ship.BaconValues != null && ship.BaconValues.ContainsKey("cash") && ship.ActualEmpire != null)
        ship.Empire = ship.ActualEmpire;
      if (ship.Role != BuiltObjectRole.Military || battleStats == null)
        return;
      if (ship.CareerBattleStats == null)
        ship.CareerBattleStats = new SpaceBattleStats();
      ship.CareerBattleStats.DamageRepaired += battleStats.DamageRepaired;
      ship.CareerBattleStats.DamageToUs += Math.Max(battleStats.DamageToUs, 0);
      ship.CareerBattleStats.DestroyedEnemyFighters += battleStats.DestroyedEnemyFighters;
      ship.CareerBattleStats.DestroyedEnemyShipBaseSize += battleStats.DestroyedEnemyShipBaseSize;
      ship.CareerBattleStats.DestroyedEnemyShipBaseSizeByFighters += battleStats.DestroyedEnemyShipBaseSizeByFighters;
      ship.CareerBattleStats.DestroyedEnemyShipsCapitalShip += battleStats.DestroyedEnemyShipsCapitalShip;
      ship.CareerBattleStats.DestroyedEnemyShipsCarrier += battleStats.DestroyedEnemyShipsCarrier;
      ship.CareerBattleStats.DestroyedEnemyShipsCruiser += battleStats.DestroyedEnemyShipsCruiser;
      ship.CareerBattleStats.DestroyedEnemyShipsDefensiveBase += battleStats.DestroyedEnemyShipsDefensiveBase;
      ship.CareerBattleStats.DestroyedEnemyShipsDestroyer += battleStats.DestroyedEnemyShipsDestroyer;
      ship.CareerBattleStats.DestroyedEnemyShipsEscort += battleStats.DestroyedEnemyShipsEscort;
      ship.CareerBattleStats.DestroyedEnemyShipsFrigate += battleStats.DestroyedEnemyShipsFrigate;
      ship.CareerBattleStats.DestroyedEnemyShipsOtherBase += battleStats.DestroyedEnemyShipsOtherBase;
      ship.CareerBattleStats.DestroyedEnemyShipsOtherShips += battleStats.DestroyedEnemyShipsOtherShips;
      ship.CareerBattleStats.DestroyedEnemyShipsResupplyShip += battleStats.DestroyedEnemyShipsResupplyShip;
      ship.CareerBattleStats.DestroyedEnemyShipsSpaceport += battleStats.DestroyedEnemyShipsSpaceport;
      ship.CareerBattleStats.DestroyedEnemyShipsTroopTransport += battleStats.DestroyedEnemyShipsTroopTransport;
      ship.CareerBattleStats.ShieldsDamageAbsorbed += battleStats.ShieldsDamageAbsorbed;
      ship.CareerBattleStats.WeaponsDamageToEnemy += battleStats.WeaponsDamageToEnemy;
      ship.CareerBattleStats.WeaponsHits += battleStats.WeaponsHits;
      ship.CareerBattleStats.WeaponsHitsLongRange += battleStats.WeaponsHitsLongRange;
      ship.CareerBattleStats.WeaponsMisses += battleStats.WeaponsMisses;
    }
  }
}
