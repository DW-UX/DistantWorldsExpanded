// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.SpaceBattleStats
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll


using System;

namespace DistantWorlds.Types
{
    [Serializable]
    public class SpaceBattleStats
    {
        public Habitat Location;

        public bool NearLocation;

        public float WeaponsDamageToEnemy;

        public int WeaponsHits;

        public int WeaponsHitsLongRange;

        public int WeaponsMisses;

        public float ShieldsDamageAbsorbed;

        public int DamageToUs;

        public int DamageRepaired;

        public int DestroyedEnemyShipBaseSize;

        public int DestroyedFriendlyShipBaseSize;

        public int DestroyedEnemyShipBaseSizeByFighters;

        public int DestroyedFriendlyShipBaseSizeByFighters;

        public int DestroyedEnemyShipsSpaceport;

        public int DestroyedEnemyShipsDefensiveBase;

        public int DestroyedEnemyShipsOtherBase;

        public int DestroyedEnemyShipsCarrier;

        public int DestroyedEnemyShipsTroopTransport;

        public int DestroyedEnemyShipsCapitalShip;

        public int DestroyedEnemyShipsCruiser;

        public int DestroyedEnemyShipsDestroyer;

        public int DestroyedEnemyShipsFrigate;

        public int DestroyedEnemyShipsEscort;

        public int DestroyedEnemyShipsResupplyShip;

        public int DestroyedEnemyShipsOtherShips;

        public int DestroyedEnemyFighters;

        public int DestroyedFriendlyShipsSpaceport;

        public int DestroyedFriendlyShipsDefensiveBase;

        public int DestroyedFriendlyShipsOtherBase;

        public int DestroyedFriendlyShipsCarrier;

        public int DestroyedFriendlyShipsTroopTransport;

        public int DestroyedFriendlyShipsCapitalShip;

        public int DestroyedFriendlyShipsCruiser;

        public int DestroyedFriendlyShipsDestroyer;

        public int DestroyedFriendlyShipsFrigate;

        public int DestroyedFriendlyShipsEscort;

        public int DestroyedFriendlyShipsResupplyShip;

        public int DestroyedFriendlyShipsOtherShips;

        public int DestroyedFriendlyFighters;

        public void ShieldsStruckUs(float damageAmount)
        {
            ShieldsDamageAbsorbed += damageAmount;
        }

        public void DamageHullUs(int damageAmount)
        {
            DamageToUs += damageAmount;
        }

        public void DamageRepairedUs(int repairAmount)
        {
            DamageRepaired += repairAmount;
        }

        public void WeaponHitEnemy(float damageAmount, double hitRange)
        {
            WeaponsHits++;
            WeaponsDamageToEnemy += damageAmount;
            if (hitRange >= 350.0)
            {
                WeaponsHitsLongRange++;
            }
        }

        public void WeaponMissEnemy()
        {
            WeaponsMisses++;
        }

        public void FighterDestroyedEnemy()
        {
            DestroyedEnemyFighters++;
        }

        public void FighterDestroyedFriendly()
        {
            DestroyedFriendlyFighters++;
        }

        public void TargetDestroyedEnemy(BuiltObject target)
        {
            if (target != null)
            {
                DestroyedEnemyShipBaseSize += target.Size;
                TargetDestroyedEnemyCore(target);
            }
        }

        public void TargetDestroyedEnemyByFighter(BuiltObject target)
        {
            if (target != null)
            {
                DestroyedEnemyShipBaseSizeByFighters += target.Size;
                TargetDestroyedEnemyCore(target);
            }
        }

        private void TargetDestroyedEnemyCore(BuiltObject target)
        {
            switch (target.SubRole)
            {
                case BuiltObjectSubRole.Escort:
                    DestroyedEnemyShipsEscort++;
                    return;
                case BuiltObjectSubRole.Frigate:
                    DestroyedEnemyShipsFrigate++;
                    return;
                case BuiltObjectSubRole.Destroyer:
                    DestroyedEnemyShipsDestroyer++;
                    return;
                case BuiltObjectSubRole.Cruiser:
                    DestroyedEnemyShipsCruiser++;
                    return;
                case BuiltObjectSubRole.CapitalShip:
                    DestroyedEnemyShipsCapitalShip++;
                    return;
                case BuiltObjectSubRole.Carrier:
                    DestroyedEnemyShipsCarrier++;
                    return;
                case BuiltObjectSubRole.TroopTransport:
                    DestroyedEnemyShipsTroopTransport++;
                    return;
                case BuiltObjectSubRole.ResupplyShip:
                    DestroyedEnemyShipsResupplyShip++;
                    return;
                case BuiltObjectSubRole.SmallSpacePort:
                case BuiltObjectSubRole.MediumSpacePort:
                case BuiltObjectSubRole.LargeSpacePort:
                    DestroyedEnemyShipsSpaceport++;
                    return;
                case BuiltObjectSubRole.DefensiveBase:
                    DestroyedEnemyShipsDefensiveBase++;
                    return;
            }
            if (target.Role == BuiltObjectRole.Base)
            {
                DestroyedEnemyShipsOtherBase++;
            }
            else
            {
                DestroyedEnemyShipsOtherShips++;
            }
        }

        public void TargetDestroyedFriendly(BuiltObject target)
        {
            if (target != null)
            {
                DestroyedFriendlyShipBaseSize += target.Size;
                TargetDestroyedFriendlyCore(target);
            }
        }

        public void TargetDestroyedFriendlyByFighter(BuiltObject target)
        {
            if (target != null)
            {
                DestroyedFriendlyShipBaseSizeByFighters += target.Size;
                TargetDestroyedFriendlyCore(target);
            }
        }

        private void TargetDestroyedFriendlyCore(BuiltObject target)
        {
            switch (target.SubRole)
            {
                case BuiltObjectSubRole.Escort:
                    DestroyedFriendlyShipsEscort++;
                    return;
                case BuiltObjectSubRole.Frigate:
                    DestroyedFriendlyShipsFrigate++;
                    return;
                case BuiltObjectSubRole.Destroyer:
                    DestroyedFriendlyShipsDestroyer++;
                    return;
                case BuiltObjectSubRole.Cruiser:
                    DestroyedFriendlyShipsCruiser++;
                    return;
                case BuiltObjectSubRole.CapitalShip:
                    DestroyedFriendlyShipsCapitalShip++;
                    return;
                case BuiltObjectSubRole.Carrier:
                    DestroyedFriendlyShipsCarrier++;
                    return;
                case BuiltObjectSubRole.TroopTransport:
                    DestroyedFriendlyShipsTroopTransport++;
                    return;
                case BuiltObjectSubRole.ResupplyShip:
                    DestroyedFriendlyShipsResupplyShip++;
                    return;
                case BuiltObjectSubRole.SmallSpacePort:
                case BuiltObjectSubRole.MediumSpacePort:
                case BuiltObjectSubRole.LargeSpacePort:
                    DestroyedFriendlyShipsSpaceport++;
                    return;
                case BuiltObjectSubRole.DefensiveBase:
                    DestroyedFriendlyShipsDefensiveBase++;
                    return;
            }
            if (target.Role == BuiltObjectRole.Base)
            {
                DestroyedFriendlyShipsOtherBase++;
            }
            else
            {
                DestroyedFriendlyShipsOtherShips++;
            }
        }
    }
}
