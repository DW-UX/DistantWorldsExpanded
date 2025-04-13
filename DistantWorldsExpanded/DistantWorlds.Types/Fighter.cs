// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Fighter
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using BaconDistantWorlds;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
    [Serializable]
    public class Fighter : StellarObject, ISerializable
    {
        [OptionalField]
        public bool BaconFighterOutOfAmmo = false;
        [OptionalField]
        public bool BaconAutomationEnabled = false;

        public int FighterID;

        public FighterSpecification Specification;

        public float CurrentEnergy;

        public float CurrentShields;

        private float _TargetSpeed;

        public bool TargetSpeedChanged;

        public TurnDirection TurnDirection;

        public bool HeadingChanged;

        public float Health;

        public float Heading;

        public bool OnboardCarrier;

        public bool UnderConstruction;

        public FighterMissionType MissionType;

        public short PictureRef;

        public bool OverlayChanged = true;

        public DateTime LastShieldStrike;

        public float LastShieldStrikeDirection;

        public bool InView;

        public bool InBattle;

        public DateTime _LastTouch;

        public DateTime _LastLongTouch;

        private DateTime _LastLocationEffectTouch;

        private bool _MovementSlowedLocation;

        private bool _ShieldsReducedLocation;

        private float _ShipPullAmountLocation;

        private float _ShipPullAngleLocation;

        private float _ShipDamageAmountLocation;

        public FighterWeaponList Weapons = new FighterWeaponList();

        public ExplosionList Explosions = new ExplosionList();

        public override int CargoSpace => 0;

        public override int TroopCapacityRemaining => 0;

        public bool ShieldsReducedLocation => _ShieldsReducedLocation;

        public bool MovementSlowedLocation => _MovementSlowedLocation;

        public float TargetSpeed
        {
            get
            {
                return _TargetSpeed;
            }
            set
            {
                if (_TargetSpeed != value)
                {
                    TargetSpeedChanged = true;
                }
                _TargetSpeed = value;
            }
        }

        public Fighter(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            byte[] buffer = (byte[])info.GetValue("Ft_D", typeof(byte[]));
            using (MemoryStream input = new MemoryStream(buffer))
            {
                using BinaryReader binaryReader = new BinaryReader(input);
                FighterID = binaryReader.ReadInt32();
                int index = binaryReader.ReadInt32();
                Specification = Galaxy.FighterSpecificationsStatic[index];
                CurrentEnergy = binaryReader.ReadSingle();
                CurrentShields = binaryReader.ReadSingle();
                _TargetSpeed = binaryReader.ReadSingle();
                TargetSpeedChanged = binaryReader.ReadBoolean();
                TurnDirection = (TurnDirection)binaryReader.ReadByte();
                HeadingChanged = binaryReader.ReadBoolean();
                Health = binaryReader.ReadSingle();
                Heading = binaryReader.ReadSingle();
                OnboardCarrier = binaryReader.ReadBoolean();
                UnderConstruction = binaryReader.ReadBoolean();
                MissionType = (FighterMissionType)binaryReader.ReadByte();
                PictureRef = binaryReader.ReadInt16();
                OverlayChanged = binaryReader.ReadBoolean();
                LastShieldStrike = new DateTime(binaryReader.ReadInt64());
                LastShieldStrikeDirection = binaryReader.ReadSingle();
                InView = binaryReader.ReadBoolean();
                InBattle = binaryReader.ReadBoolean();
                _LastTouch = new DateTime(binaryReader.ReadInt64());
                _LastLongTouch = new DateTime(binaryReader.ReadInt64());
                _LastLocationEffectTouch = new DateTime(binaryReader.ReadInt64());
                _MovementSlowedLocation = binaryReader.ReadBoolean();
                _ShieldsReducedLocation = binaryReader.ReadBoolean();
                _ShipPullAmountLocation = binaryReader.ReadSingle();
                _ShipPullAngleLocation = binaryReader.ReadSingle();
                _ShipDamageAmountLocation = binaryReader.ReadSingle();
                binaryReader.Close();
            }
            Weapons = (FighterWeaponList)info.GetValue("Wpn", typeof(FighterWeaponList));
            Explosions = (ExplosionList)info.GetValue("Exp", typeof(ExplosionList));
            BaconFighter.DeserializeExtraFields(this, info);
        }

        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(FighterID);
                binaryWriter.Write(Specification.FighterSpecificationId);
                binaryWriter.Write(CurrentEnergy);
                binaryWriter.Write(CurrentShields);
                binaryWriter.Write(_TargetSpeed);
                binaryWriter.Write(TargetSpeedChanged);
                binaryWriter.Write((byte)TurnDirection);
                binaryWriter.Write(HeadingChanged);
                binaryWriter.Write(Health);
                binaryWriter.Write(Heading);
                binaryWriter.Write(OnboardCarrier);
                binaryWriter.Write(UnderConstruction);
                binaryWriter.Write((byte)MissionType);
                binaryWriter.Write(PictureRef);
                binaryWriter.Write(OverlayChanged);
                binaryWriter.Write(LastShieldStrike.Ticks);
                binaryWriter.Write(LastShieldStrikeDirection);
                binaryWriter.Write(InView);
                binaryWriter.Write(InBattle);
                binaryWriter.Write(_LastTouch.Ticks);
                binaryWriter.Write(_LastLongTouch.Ticks);
                binaryWriter.Write(_LastLocationEffectTouch.Ticks);
                binaryWriter.Write(_MovementSlowedLocation);
                binaryWriter.Write(_ShieldsReducedLocation);
                binaryWriter.Write(_ShipPullAmountLocation);
                binaryWriter.Write(_ShipPullAngleLocation);
                binaryWriter.Write(_ShipDamageAmountLocation);
                binaryWriter.Flush();
                binaryWriter.Close();
                info.AddValue("Ft_D", memoryStream.ToArray());
            }
            info.AddValue("Wpn", Weapons);
            info.AddValue("Exp", Explosions);
            BaconFighter.SerializeExtraFields(this, info);
        }

        public Fighter(Galaxy galaxy, FighterSpecification specification, BuiltObject carrier)
        {
            FighterID = galaxy.GetNextFighterID();
            Specification = specification;
            Name = specification.Name;
            CurrentEnergy = specification.EnergyCapacity;
            CurrentShields = specification.ShieldsCapacity;
            TargetSpeed = 0f;
            TurnDirection = TurnDirection.StraightAhead;
            HeadingChanged = false;
            Health = 0f;
            Size = specification.Size;
            TopSpeed = specification.TopSpeed;
            ParentBuiltObject = carrier;
            Heading = carrier.Heading;
            OnboardCarrier = true;
            UnderConstruction = true;
            ParentBuiltObject.Fighters.Add(this);
            Empire = ParentBuiltObject.Empire;
            Owner = Empire;
            if (Empire != null)
            {
                bool isPirates = false;
                if (Empire.PirateEmpireBaseHabitat != null)
                {
                    isPirates = true;
                }
                if (specification.Type == FighterType.Bomber)
                {
                    PictureRef = (short)BaconShipImageHelper.ResolveNewBomberImageIndex(specification, Empire.DominantRace, isPirates);
                }
                else
                {
                    PictureRef = (short)BaconShipImageHelper.ResolveNewFighterImageIndex(specification, Empire.DominantRace, isPirates);
                }
            }
            MissionType = FighterMissionType.Undefined;
            InView = false;
            _LastTouch = galaxy.CurrentDateTime;
            _LastLongTouch = galaxy.CurrentDateTime;
            Weapons = new FighterWeaponList();
            if (specification.WeaponType != 0)
            {
                FighterWeapon fighterWeapon = new FighterWeapon
                {
                    Category = ComponentDefinition.ResolveComponentCategory(specification.WeaponType),
                    Type = specification.WeaponType,
                    RawDamage = specification.WeaponDamage,
                    Range = specification.WeaponRange,
                    EnergyRequired = specification.WeaponEnergyRequired,
                    Speed = specification.WeaponSpeed,
                    DamageLoss = specification.WeaponDamageLoss,
                    FireRate = specification.WeaponFireRate
                };
                Weapons.Add(fighterWeapon);
                if (fighterWeapon.RawDamage > FirepowerRaw)
                {
                    FirepowerRaw = fighterWeapon.RawDamage;
                }
            }
            Attackers = new StellarObjectList();
            Pursuers = new StellarObjectList();
        }

        public void DoTasks(Galaxy galaxy, DateTime time)
        {
            DoTasks(galaxy, time, inView: false);
        }

        public void DoTasks(Galaxy galaxy, DateTime time, bool inView)
        {
            BaconFighter.DoTasks(this, galaxy, time, inView);
        }

        public void ApplyLocationEffectsNEW(Galaxy galaxy, double timePassed, DateTime time)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            double num = 0.0;
            bool flag5 = false;
            double num2 = 0.0;
            double num3 = 0.0;
            if (!OnboardCarrier && ParentBuiltObject != null)
            {
                flag = ParentBuiltObject.LocationEffects.Contains(GalaxyLocationEffectType.LightningDamage);
                flag2 = ParentBuiltObject.MovementSlowedLocation;
                flag3 = ParentBuiltObject.ShieldsReducedLocation;
                num = ParentBuiltObject.ShipDamageAmountLocation;
                if (num > 0.0)
                {
                    flag4 = true;
                }
                num2 = ParentBuiltObject.ShipDamageAmountLocation;
                num3 = ParentBuiltObject.ShipPullAngleLocation;
                if (num2 > 0.0)
                {
                    flag5 = true;
                }
            }
            if (flag && !OnboardCarrier)
            {
                TimeSpan timeSpan = time.Subtract(_LastLocationEffectTouch);
                double num4 = Galaxy.Rnd.NextDouble() * timeSpan.TotalSeconds;
                if (num4 > 7.0)
                {
                    double num5 = 20.0 + Galaxy.Rnd.NextDouble() * 70.0;
                    if ((double)CurrentShields <= num5)
                    {
                        CurrentShields = 0f;
                        num5 = Galaxy.Rnd.NextDouble() * 5.0;
                    }
                    InflictDamage(this, num5, time, galaxy, 0f, allowRecursion: false, double.MinValue, allowArmorInvulnerability: true);
                    _LastLocationEffectTouch = time;
                }
            }
            if (flag4)
            {
                _ShipDamageAmountLocation = (float)num;
            }
            else
            {
                _ShipDamageAmountLocation = 0f;
            }
            if (flag5)
            {
                _ShipPullAmountLocation = (float)num2;
                _ShipPullAngleLocation = (float)num3;
            }
            else
            {
                _ShipPullAmountLocation = 0f;
                _ShipPullAngleLocation = 0f;
            }
            if (flag2)
            {
                TopSpeed = (short)((double)Specification.TopSpeed * 0.75);
            }
            else if (!flag2 && _MovementSlowedLocation)
            {
                TopSpeed = Specification.TopSpeed;
            }
            _MovementSlowedLocation = flag2;
            if (flag3)
            {
                double val = (3.0 + Galaxy.Rnd.NextDouble() * 0.5) * timePassed;
                val = Math.Min(CurrentShields, val);
                CurrentShields -= (float)val;
            }
            _ShieldsReducedLocation = flag3;
        }

        private void ApplyLocationEffects(Galaxy galaxy, double timePassed, DateTime time)
        {
            GalaxyLocationList galaxyLocationList = galaxy.DetermineGalaxyLocationsAtPoint(Xpos, Ypos);
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            double num = 0.0;
            bool flag5 = false;
            double num2 = 0.0;
            double num3 = 0.0;
            for (int i = 0; i < galaxyLocationList.Count; i++)
            {
                GalaxyLocation galaxyLocation = galaxyLocationList[i];
                switch (galaxyLocation.Effect)
                {
                    case GalaxyLocationEffectType.LightningDamage:
                        flag = true;
                        break;
                    case GalaxyLocationEffectType.MovementSlowed:
                        flag2 = true;
                        break;
                    case GalaxyLocationEffectType.ShieldReduction:
                        flag3 = true;
                        break;
                    case GalaxyLocationEffectType.ShipDamage:
                        flag4 = true;
                        num = galaxyLocation.EffectAmount;
                        break;
                    case GalaxyLocationEffectType.ShipPull:
                        {
                            flag5 = true;
                            double x = (double)galaxyLocation.Xpos + (double)galaxyLocation.Width / 2.0;
                            double y = (double)galaxyLocation.Ypos + (double)galaxyLocation.Height / 2.0;
                            double num4 = galaxy.CalculateDistance(Xpos, Ypos, x, y);
                            double num5 = (double)galaxyLocation.Width / 2.0 / num4;
                            num2 = galaxyLocation.EffectAmount * num5;
                            double num6 = Galaxy.DetermineAngle(Xpos, Ypos, x, y);
                            num3 = num6;
                            break;
                        }
                }
            }
            if (flag && !OnboardCarrier)
            {
                TimeSpan timeSpan = time.Subtract(_LastLocationEffectTouch);
                double num7 = Galaxy.Rnd.NextDouble() * timeSpan.TotalSeconds;
                if (num7 > 7.0)
                {
                    double num8 = 20.0 + Galaxy.Rnd.NextDouble() * 70.0;
                    if ((double)CurrentShields <= num8)
                    {
                        CurrentShields = 0f;
                        num8 = Galaxy.Rnd.NextDouble() * 5.0;
                    }
                    InflictDamage(this, num8, time, galaxy, 0f, allowRecursion: false, double.MinValue, allowArmorInvulnerability: true);
                    _LastLocationEffectTouch = time;
                }
            }
            if (flag4)
            {
                _ShipDamageAmountLocation = (float)num;
            }
            else
            {
                _ShipDamageAmountLocation = 0f;
            }
            if (flag5)
            {
                _ShipPullAmountLocation = (float)num2;
                _ShipPullAngleLocation = (float)num3;
            }
            else
            {
                _ShipPullAmountLocation = 0f;
                _ShipPullAngleLocation = 0f;
            }
            if (flag2)
            {
                TopSpeed = (short)((double)Specification.TopSpeed * 0.75);
            }
            else if (!flag2 && _MovementSlowedLocation)
            {
                TopSpeed = Specification.TopSpeed;
            }
            _MovementSlowedLocation = flag2;
            if (flag3)
            {
                double val = (3.0 + Galaxy.Rnd.NextDouble() * 0.5) * timePassed;
                val = Math.Min(CurrentShields, val);
                CurrentShields -= (float)val;
            }
            _ShieldsReducedLocation = flag3;
        }

        public void CheckReturnToCarrier(Galaxy galaxy)
        {
            BaconFighter.CheckReturnToCarrier(this, galaxy);
        }

        public void CheckCarrierDestroyed(Galaxy galaxy)
        {
            if (ParentBuiltObject == null || ParentBuiltObject.HasBeenDestroyed)
            {
                CompleteTeardown(galaxy);
            }
        }

        public void CompleteTeardown(Galaxy galaxy)
        {
            HasBeenDestroyed = true;
            if (CurrentTarget != null)
            {
                int num = CurrentTarget.Attackers.IndexOf(this);
                if (num >= 0)
                {
                    CurrentTarget.Attackers.RemoveAt(num);
                }
                num = CurrentTarget.Pursuers.IndexOf(this);
                if (num >= 0)
                {
                    CurrentTarget.Pursuers.RemoveAt(num);
                }
                CurrentTarget = null;
            }
            ClearAllFighterMissionsForTarget(galaxy, this);
            if (ParentBuiltObject != null)
            {
                if (ParentBuiltObject.Fighters != null && ParentBuiltObject.Fighters.Contains(this))
                {
                    ParentBuiltObject.Fighters.Remove(this);
                }
                ParentBuiltObject = null;
            }
        }

        private void ClearAllFighterMissionsForTarget(Galaxy galaxy, Fighter fighter)
        {
            BuiltObjectList builtObjectsAtLocation = galaxy.GetBuiltObjectsAtLocation(fighter.Xpos, fighter.Ypos, 20000);
            for (int i = 0; i < builtObjectsAtLocation.Count; i++)
            {
                BuiltObject builtObject = builtObjectsAtLocation[i];
                if (builtObject == null || builtObject.Fighters == null || builtObject.Fighters.Count <= 0)
                {
                    continue;
                }
                for (int j = 0; j < builtObject.Fighters.Count; j++)
                {
                    Fighter fighter2 = builtObject.Fighters[j];
                    if (fighter2.CurrentTarget == fighter)
                    {
                        fighter2.AbandonAttackTarget();
                        fighter2.EvaluateThreats(galaxy);
                        if (fighter2.MissionType == FighterMissionType.Undefined)
                        {
                            fighter2.MissionType = FighterMissionType.Patrol;
                        }
                    }
                }
            }
        }

        public void ReturnToCarrierForRepairs()
        {
            if (Health < 1f)
            {
                bool flag = false;
                if (Specification.DamageRepairRate > 0 && Health > 0.75f)
                {
                    flag = true;
                }
                if (!flag)
                {
                    ReturnToCarrier();
                }
            }
        }

        public void EvaluateThreats(Galaxy galaxy)
        {
            BaconFighter.EvaluateThreats(this, galaxy);
        }

        public bool EvaluateAdequateAttackers(Galaxy galaxy, StellarObject potentialTarget)
        {
            return BaconFighter.EvaluateAdequateAttackers(this, galaxy, potentialTarget);
        }

        public int DetermineAttackingFirepower(Galaxy galaxy, StellarObject potentialTarget, out double closestAttackerDistance)
        {
            int num = 0;
            closestAttackerDistance = 536870911.0;
            if (potentialTarget != null && potentialTarget.Pursuers != null)
            {
                for (int i = 0; i < potentialTarget.Pursuers.Count; i++)
                {
                    StellarObject stellarObject = potentialTarget.Pursuers[i];
                    if (stellarObject == null || stellarObject.FirepowerRaw <= 0 || stellarObject.TopSpeed <= 0 || !stellarObject.IsFunctional)
                    {
                        continue;
                    }
                    double num2 = galaxy.CalculateDistance(stellarObject.Xpos, stellarObject.Ypos, potentialTarget.Xpos, potentialTarget.Ypos);
                    if (num2 < Galaxy.AttackEvaluationRangeFactor)
                    {
                        int num3 = stellarObject.FirepowerRaw;
                        if (stellarObject is BuiltObject)
                        {
                            BuiltObject builtObject = (BuiltObject)stellarObject;
                            num3 = builtObject.CalculateOverallStrengthFactor();
                        }
                        else if (stellarObject is Creature)
                        {
                            Creature creature = (Creature)stellarObject;
                            num3 = creature.AttackStrength * 5;
                            if (creature.Type == CreatureType.SilverMist)
                            {
                                num3 *= 4;
                            }
                        }
                        num += num3;
                    }
                    if (num2 < closestAttackerDistance)
                    {
                        closestAttackerDistance = num2;
                    }
                }
            }
            return num;
        }

        public bool ShouldAttack(StellarObject potentialTarget, double carrierX, double carrierY, double maximumTargetDistanceSquared, Galaxy galaxy)
        {
            double num = 0.0;
            if (Empire == null)
            {
                return false;
            }
            if (MissionType == FighterMissionType.ReturnToCarrier)
            {
                return false;
            }
            if (potentialTarget is Creature)
            {
                Creature creature = (Creature)potentialTarget;
                if (creature.IsVisible)
                {
                    num = galaxy.CalculateDistanceSquared(carrierX, carrierY, creature.Xpos, creature.Ypos);
                    if (num <= maximumTargetDistanceSquared)
                    {
                        return true;
                    }
                }
                return false;
            }
            BuiltObject builtObject = (BuiltObject)potentialTarget;
            if (Empire == builtObject.Empire || builtObject.Empire == null)
            {
                return false;
            }
            if (builtObject.Empire == galaxy.IndependentEmpire)
            {
                return false;
            }
            if (builtObject.PirateEmpireId > 0 && ParentBuiltObject != null && builtObject.PirateEmpireId == ParentBuiltObject.PirateEmpireId)
            {
                return false;
            }
            if (builtObject.Empire.PirateEmpireBaseHabitat != null && builtObject.Empire.ObtainPirateRelation(Empire).Type == PirateRelationType.Protection)
            {
                return false;
            }
            if (Empire.PirateEmpireBaseHabitat != null && Empire.ObtainPirateRelation(builtObject.Empire).Type == PirateRelationType.Protection)
            {
                return false;
            }
            num = galaxy.CalculateDistanceSquared(carrierX, carrierY, builtObject.Xpos, builtObject.Ypos);
            if (num <= maximumTargetDistanceSquared)
            {
                if (Empire.Outlaws.Contains(builtObject))
                {
                    if (Empire == builtObject.Empire)
                    {
                        Empire.Outlaws.Remove(builtObject);
                        return false;
                    }
                    return true;
                }
                if (Attackers.Contains(builtObject))
                {
                    return true;
                }
                DiplomaticRelation diplomaticRelation = Empire.DiplomaticRelations[builtObject.Empire];
                if (builtObject.Empire.PirateEmpireBaseHabitat != null)
                {
                    return true;
                }
                if (Empire.PirateEmpireBaseHabitat != null && builtObject.Empire != Empire)
                {
                    return true;
                }
                if (diplomaticRelation != null)
                {
                    if (diplomaticRelation.Type == DiplomaticRelationType.War)
                    {
                        return true;
                    }
                    if (MissionType == FighterMissionType.Attack)
                    {
                        Empire empire = null;
                        if (CurrentTarget != null)
                        {
                            empire = CurrentTarget.Empire;
                        }
                        if (potentialTarget.Empire == empire)
                        {
                            return true;
                        }
                    }
                    if (ParentBuiltObject != null && ParentBuiltObject.CurrentTarget != null && ParentBuiltObject.CurrentTarget == potentialTarget)
                    {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public void HandleWeaponsFiring(Galaxy galaxy, DateTime time, double timePassed)
        {
            for (int i = 0; i < Weapons.Count; i++)
            {
                if (HasBeenDestroyed)
                {
                    break;
                }
                FighterWeapon fighterWeapon = Weapons[i];
                if (fighterWeapon == null)
                {
                    continue;
                }
                if (fighterWeapon.ResetNext)
                {
                    fighterWeapon.Reset();
                    continue;
                }
                if (CurrentTarget == null || CurrentTarget.HasBeenDestroyed)
                {
                    fighterWeapon.ResetNext = true;
                }
                StellarObject currentTarget = CurrentTarget;
                if (!(fighterWeapon.DistanceTravelled >= 0f))
                {
                    continue;
                }
                float val = (float)((double)time.Subtract(fighterWeapon.LastFired).Ticks / 10000000.0);
                val = Math.Min(val, (float)timePassed);
                bool flag = false;
                double num = Galaxy.TorpedoWeaponHitRange * 4.0;
                if (InView)
                {
                    num = Galaxy.TorpedoWeaponHitRange;
                }
                switch (fighterWeapon.Category)
                {
                    case ComponentCategoryType.WeaponBeam:
                        {
                            float num2;
                            if (fighterWeapon.DistanceTravelled <= 1f)
                            {
                                flag = true;
                                num2 = 2f;
                            }
                            else
                            {
                                num2 = (float)fighterWeapon.Speed * val;
                            }
                            fighterWeapon.DistanceTravelled += num2;
                            float distanceFromTarget = fighterWeapon.DistanceFromTarget;
                            fighterWeapon.X += (float)Math.Cos(fighterWeapon.Heading) * num2;
                            fighterWeapon.Y += (float)Math.Sin(fighterWeapon.Heading) * num2;
                            if (currentTarget != null)
                            {
                                fighterWeapon.DistanceFromTarget = (float)galaxy.CalculateDistance(fighterWeapon.X, fighterWeapon.Y, currentTarget.Xpos, currentTarget.Ypos);
                            }
                            fighterWeapon.Power = (float)fighterWeapon.RawDamage - fighterWeapon.DistanceTravelled / 100f * (float)fighterWeapon.DamageLoss;
                            if (fighterWeapon.WillHitTarget && !flag)
                            {
                                bool flag4 = false;
                                if (InView)
                                {
                                    if ((double)fighterWeapon.DistanceFromTarget <= num)
                                    {
                                        flag4 = true;
                                    }
                                }
                                else if (distanceFromTarget < fighterWeapon.DistanceFromTarget || (double)fighterWeapon.DistanceFromTarget <= num)
                                {
                                    flag4 = true;
                                }
                                if (flag4)
                                {
                                    if (currentTarget != null && InflictDamage(currentTarget, fighterWeapon.Power, time, galaxy, fighterWeapon.DistanceTravelled, fighterWeapon.Heading))
                                    {
                                        if (currentTarget is BuiltObject)
                                        {
                                            BuiltObject builtObject2 = (BuiltObject)currentTarget;
                                            if (builtObject2.Empire != null && builtObject2.Empire.PirateEmpireBaseHabitat != null && Empire != null && Empire.PirateEmpireBaseHabitat != null && Empire.PirateEmpireSuperPirates)
                                            {
                                                bool flag5 = false;
                                                switch (builtObject2.SubRole)
                                                {
                                                    case BuiltObjectSubRole.SmallSpacePort:
                                                    case BuiltObjectSubRole.MediumSpacePort:
                                                    case BuiltObjectSubRole.LargeSpacePort:
                                                        flag5 = true;
                                                        break;
                                                }
                                                if (flag5 && Galaxy.Rnd.Next(0, 4) == 1)
                                                {
                                                    galaxy.FearfulPirateFactionJoinsPlayer(Empire, builtObject2.Empire);
                                                }
                                            }
                                            ProvideBonusFromPirateBase(galaxy, Empire, builtObject2);
                                            if (Empire != null && Empire.PirateEmpireBaseHabitat != null)
                                            {
                                                double num4 = Galaxy.CalculateBuiltObjectLootingValue(builtObject2);
                                                num4 *= Empire.ColonyIncomeFactor;
                                                num4 *= Empire.LootingFactor;
                                                num4 = Empire.ApplyCorruptionToIncome(num4);
                                                Empire.StateMoney += num4;
                                                Empire.PirateEconomy.PerformIncome(num4, PirateIncomeType.Looting, galaxy.CurrentStarDate);
                                                EmpireActivity byAttackTarget2 = Empire.PirateMissions.GetByAttackTarget(builtObject2, Empire);
                                                if (byAttackTarget2 != null)
                                                {
                                                    Empire.CompletePirateMission(byAttackTarget2);
                                                }
                                            }
                                        }
                                        CurrentTarget = null;
                                    }
                                    fighterWeapon.ResetNext = true;
                                }
                            }
                            if (fighterWeapon.DistanceTravelled > (float)fighterWeapon.Range)
                            {
                                if (ParentBuiltObject != null && ParentBuiltObject.BattleStats != null)
                                {
                                    ParentBuiltObject.BattleStats.WeaponMissEnemy();
                                }
                                if (ParentBuiltObject != null && ParentBuiltObject.ShipGroup != null && ParentBuiltObject.ShipGroup.BattleStats != null)
                                {
                                    ParentBuiltObject.ShipGroup.BattleStats.WeaponMissEnemy();
                                }
                                fighterWeapon.ResetNext = true;
                            }
                            break;
                        }
                    case ComponentCategoryType.WeaponTorpedo:
                        {
                            float num2;
                            if (fighterWeapon.DistanceTravelled <= 1f)
                            {
                                flag = true;
                                num2 = 10f;
                            }
                            else
                            {
                                num2 = (float)fighterWeapon.Speed * val;
                            }
                            float heading = fighterWeapon.Heading;
                            if (!fighterWeapon.HasMissed && currentTarget != null)
                            {
                                fighterWeapon.Heading = fighterWeapon.HeadingMissFactor + (float)Galaxy.DetermineAngle(fighterWeapon.X, fighterWeapon.Y, currentTarget.Xpos, currentTarget.Ypos);
                            }
                            fighterWeapon.DistanceTravelled += num2;
                            float distanceFromTarget = fighterWeapon.DistanceFromTarget;
                            fighterWeapon.X += (float)Math.Cos(fighterWeapon.Heading) * num2;
                            fighterWeapon.Y += (float)Math.Sin(fighterWeapon.Heading) * num2;
                            if (currentTarget != null)
                            {
                                fighterWeapon.DistanceFromTarget = (float)galaxy.CalculateDistance(fighterWeapon.X, fighterWeapon.Y, currentTarget.Xpos, currentTarget.Ypos);
                            }
                            fighterWeapon.Power = (float)fighterWeapon.RawDamage - fighterWeapon.DistanceTravelled / 100f * (float)fighterWeapon.DamageLoss;
                            if (fighterWeapon.WillHitTarget)
                            {
                                if (!flag)
                                {
                                    bool flag2 = false;
                                    if (InView && (double)fighterWeapon.DistanceFromTarget <= num)
                                    {
                                        flag2 = true;
                                    }
                                    else if (distanceFromTarget < fighterWeapon.DistanceFromTarget || num2 > distanceFromTarget)
                                    {
                                        flag2 = true;
                                    }
                                    if (flag2)
                                    {
                                        if (currentTarget != null && InflictDamage(currentTarget, fighterWeapon.Power, time, galaxy, fighterWeapon.DistanceTravelled, fighterWeapon.Heading))
                                        {
                                            if (currentTarget is BuiltObject)
                                            {
                                                BuiltObject builtObject = (BuiltObject)currentTarget;
                                                if (builtObject.Empire != null && builtObject.Empire.PirateEmpireBaseHabitat != null && Empire != null && Empire.PirateEmpireBaseHabitat != null && Empire.PirateEmpireSuperPirates)
                                                {
                                                    bool flag3 = false;
                                                    switch (builtObject.SubRole)
                                                    {
                                                        case BuiltObjectSubRole.SmallSpacePort:
                                                        case BuiltObjectSubRole.MediumSpacePort:
                                                        case BuiltObjectSubRole.LargeSpacePort:
                                                            flag3 = true;
                                                            break;
                                                    }
                                                    if (flag3 && Galaxy.Rnd.Next(0, 4) == 1)
                                                    {
                                                        galaxy.FearfulPirateFactionJoinsPlayer(Empire, builtObject.Empire);
                                                    }
                                                }
                                                ProvideBonusFromPirateBase(galaxy, Empire, builtObject);
                                                if (Empire != null && Empire.PirateEmpireBaseHabitat != null)
                                                {
                                                    double num3 = Galaxy.CalculateBuiltObjectLootingValue(builtObject);
                                                    num3 *= Empire.ColonyIncomeFactor;
                                                    num3 *= Empire.LootingFactor;
                                                    num3 = Empire.ApplyCorruptionToIncome(num3);
                                                    Empire.StateMoney += num3;
                                                    Empire.PirateEconomy.PerformIncome(num3, PirateIncomeType.Looting, galaxy.CurrentStarDate);
                                                    EmpireActivity byAttackTarget = Empire.PirateMissions.GetByAttackTarget(builtObject, Empire);
                                                    if (byAttackTarget != null)
                                                    {
                                                        Empire.CompletePirateMission(byAttackTarget);
                                                    }
                                                }
                                            }
                                            CurrentTarget = null;
                                        }
                                        fighterWeapon.ResetNext = true;
                                    }
                                }
                            }
                            else if (fighterWeapon.HasMissed)
                            {
                                fighterWeapon.Heading = heading;
                            }
                            else if (distanceFromTarget < fighterWeapon.DistanceFromTarget || num2 > distanceFromTarget)
                            {
                                fighterWeapon.HasMissed = true;
                                fighterWeapon.Heading = heading;
                            }
                            if (fighterWeapon.DistanceTravelled > (float)fighterWeapon.Range)
                            {
                                if (ParentBuiltObject != null && ParentBuiltObject.BattleStats != null)
                                {
                                    ParentBuiltObject.BattleStats.WeaponMissEnemy();
                                }
                                if (ParentBuiltObject != null && ParentBuiltObject.ShipGroup != null && ParentBuiltObject.ShipGroup.BattleStats != null)
                                {
                                    ParentBuiltObject.ShipGroup.BattleStats.WeaponMissEnemy();
                                }
                                fighterWeapon.ResetNext = true;
                            }
                            break;
                        }
                }
            }
        }

        private void ProvideBonusFromPirateBase(Galaxy galaxy, Empire destroyingEmpire, BuiltObject pirateBase)
        {
            if (destroyingEmpire == null || destroyingEmpire.PirateEmpireBaseHabitat != null || pirateBase.Role != BuiltObjectRole.Base || pirateBase.Empire == null || pirateBase.Empire.PirateEmpireBaseHabitat == null)
            {
                return;
            }
            if (pirateBase.SubRole == BuiltObjectSubRole.GenericBase && pirateBase.ParentHabitat != null && pirateBase.ParentHabitat == pirateBase.Empire.PirateEmpireBaseHabitat && pirateBase.Empire.PirateEmpireSuperPirates)
            {
                string text = string.Format(TextResolver.GetText("Phantom Pirate Base Destroyed"), pirateBase.Name, pirateBase.Empire.Name);
                text = text + "\n\n" + string.Format(TextResolver.GetText("Destroyed Ship Acquire Tech Multiple"), TextResolver.GetText("Base").ToLower(CultureInfo.InvariantCulture));
                text += " ";
                for (int i = 0; i < 6; i++)
                {
                    ResearchNode researchNode = Empire.Research.SelectRandomNextResearchProjectExcludeSuperWeapons(galaxy);
                    if (researchNode != null)
                    {
                        Empire.DoResearchBreakthrough(researchNode, selfResearched: true, blockMessages: true, suppressUpdate: false);
                        text = text + researchNode.Name + ", ";
                    }
                }
                Empire.Research.Update(Empire.DominantRace);
                Empire.ReviewDesignsBuiltObjectsImprovedComponents();
                Empire.ReviewResearchAbilities();
                text = text.Substring(0, text.Length - 2);
                text = text + "\n\n" + string.Format(TextResolver.GetText("Great Victory"), pirateBase.Empire.Name);
                string title = string.Format(TextResolver.GetText("TARGET Destroyed"), pirateBase.Name) + "!";
                destroyingEmpire.SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, title, text, pirateBase, pirateBase.Empire.PirateEmpireBaseHabitat);
                destroyingEmpire.DefeatedLegendaryPiratesCount++;
            }
            else
            {
                if (Galaxy.Rnd.Next(0, 5) <= 1 || !destroyingEmpire.CheckEmpireHasHyperDriveTech(destroyingEmpire))
                {
                    return;
                }
                string empty = string.Empty;
                string empty2 = string.Empty;
                string empty3 = string.Empty;
                switch (Galaxy.Rnd.Next(0, 4))
                {
                    case 0:
                        {
                            Habitat habitat3 = galaxy.FindLonelyColonyLocation(destroyingEmpire);
                            Habitat habitat4 = Galaxy.DetermineHabitatSystemStar(habitat3);
                            int num = Galaxy.Rnd.Next(0, 4);
                            DesignSpecification designSpecification = null;
                            Design design = null;
                            switch (num)
                            {
                                case 0:
                                    designSpecification = destroyingEmpire.ObtainDesignSpec(BuiltObjectSubRole.Destroyer);
                                    design = destroyingEmpire.GenerateDesignFromSpec(designSpecification, 4.0);
                                    break;
                                case 1:
                                    designSpecification = destroyingEmpire.ObtainDesignSpec(BuiltObjectSubRole.Cruiser);
                                    design = destroyingEmpire.GenerateDesignFromSpec(designSpecification, 4.0);
                                    break;
                                case 2:
                                    designSpecification = destroyingEmpire.GetMonitoringStationDesignSpec();
                                    design = destroyingEmpire.GenerateDesignFromSpec(designSpecification, 4.0);
                                    break;
                                case 3:
                                    designSpecification = destroyingEmpire.ObtainDesignSpec(BuiltObjectSubRole.CapitalShip);
                                    design = destroyingEmpire.GenerateDesignFromSpec(designSpecification, 4.0);
                                    break;
                            }
                            if (design != null && habitat3 != null && habitat4 != null)
                            {
                                design.PictureRef = ShipImageHelper.ResolveMinorShipImageIndex(design.SubRole, largeShips: true);
                                BuiltObject builtObject = galaxy.GenerateAbandonedBuiltObject(habitat3, design);
                                empty3 = galaxy.ResolveSectorDescription(builtObject.Xpos, builtObject.Ypos);
                                empty2 = string.Format(TextResolver.GetText("Pirate Base Bonus Abandoned Ship"), pirateBase.Name, Galaxy.ResolveDescription(builtObject.SubRole).ToLower(CultureInfo.InvariantCulture), builtObject.Name, habitat4.Name, empty3);
                                empty = TextResolver.GetText("Lost Ship Location Revealed");
                                if (destroyingEmpire == galaxy.PlayerEmpire)
                                {
                                    Point location2 = new Point((int)builtObject.Xpos, (int)builtObject.Ypos);
                                    galaxy.PlayerEmpire.AddLocationHint(location2);
                                }
                                destroyingEmpire.SendEventMessageToEmpire(EventMessageType.LostBuiltObjectCoordinates, empty, empty2, pirateBase, pirateBase.Empire.PirateEmpireBaseHabitat);
                            }
                            break;
                        }
                    case 1:
                        {
                            double num5 = 2000.0 + Galaxy.Rnd.NextDouble() * 6000.0;
                            num5 *= destroyingEmpire.ColonyIncomeFactor;
                            num5 *= destroyingEmpire.LootingFactor;
                            num5 = destroyingEmpire.ApplyCorruptionToIncome(num5);
                            destroyingEmpire.StateMoney += num5;
                            destroyingEmpire.PirateEconomy.PerformIncome(num5, PirateIncomeType.Looting, galaxy.CurrentStarDate);
                            empty2 = string.Format(TextResolver.GetText("Pirate Base Bonus Money"), pirateBase.Name, num5.ToString("#0"));
                            empty = TextResolver.GetText("Valuable Treasure Discovered");
                            destroyingEmpire.SendEventMessageToEmpire(EventMessageType.TreasureFound, empty, empty2, pirateBase, pirateBase.Empire.PirateEmpireBaseHabitat);
                            break;
                        }
                    case 2:
                        {
                            if (pirateBase.Empire == null || Empire == null)
                            {
                                break;
                            }
                            bool flag = false;
                            switch (pirateBase.SubRole)
                            {
                                case BuiltObjectSubRole.SmallSpacePort:
                                case BuiltObjectSubRole.MediumSpacePort:
                                case BuiltObjectSubRole.LargeSpacePort:
                                    flag = true;
                                    break;
                            }
                            if (flag)
                            {
                                Empire empire = pirateBase.Empire;
                                int num2 = Math.Max(1, Empire.BuiltObjects.TotalMobileMilitaryFirepower());
                                int num3 = Math.Max(1, empire.BuiltObjects.TotalMobileMilitaryFirepower());
                                double num4 = (double)num2 / (double)num3;
                                if (num4 > 2.0 && num3 < 400 && empire.SpacePorts.Count <= 1 && empire != null && !empire.PirateEmpireSuperPirates && empire != galaxy.PlayerEmpire)
                                {
                                    galaxy.PirateFactionJoinsEmpire(destroyingEmpire, empire);
                                    empty2 = string.Format(TextResolver.GetText("Pirate Base Bonus Targeted Faction Joins"), pirateBase.Name, empire.Name);
                                    empty = TextResolver.GetText("Pirate Faction Joins Your Empire");
                                    destroyingEmpire.SendEventMessageToEmpire(EventMessageType.PirateFactionJoinsYou, empty, empty2, pirateBase, pirateBase.Empire.PirateEmpireBaseHabitat);
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            Habitat habitat = galaxy.FastFindNearestIndependentHabitat(pirateBase.Xpos, pirateBase.Ypos);
                            SystemVisibilityStatus systemVisibilityStatus = SystemVisibilityStatus.Undefined;
                            if (habitat != null)
                            {
                                systemVisibilityStatus = destroyingEmpire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                            }
                            if (systemVisibilityStatus != SystemVisibilityStatus.Unexplored)
                            {
                                break;
                            }
                            Race race = null;
                            if (habitat.Population != null && habitat.Population.Count > 0 && habitat.Population.DominantRace != null)
                            {
                                race = habitat.Population.DominantRace;
                            }
                            if (race != null)
                            {
                                Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                                empty3 = galaxy.ResolveSectorDescription(habitat.Xpos, habitat.Ypos);
                                empty2 = string.Format(TextResolver.GetText("Pirate Base Bonus Exploration"), pirateBase.Name, race.Name, habitat2.Name, empty3);
                                empty = string.Format(TextResolver.GetText("Independent Colony of RACE"), race.Name);
                                if (destroyingEmpire == galaxy.PlayerEmpire)
                                {
                                    Point location = new Point((int)habitat.Xpos, (int)habitat.Ypos);
                                    galaxy.PlayerEmpire.AddLocationHint(location);
                                }
                                destroyingEmpire.SendEventMessageToEmpire(EventMessageType.IndependentPopulation, empty, empty2, race, pirateBase.Empire.PirateEmpireBaseHabitat);
                            }
                            break;
                        }
                }
            }
        }

        public void DoExplosions(Galaxy galaxy, DateTime time)
        {
            ExplosionList explosionList = new ExplosionList();
            for (int i = 0; i < Explosions.Count; i++)
            {
                Explosion explosion = Explosions[i];
                double num = (double)time.Subtract(explosion.ExplosionStart).Ticks / 10000000.0;
                explosion.ExplosionProgression = (float)Math.Max(0.0, num * 60.0);
                double num2 = Math.Min(100.0, Math.Max(50.0, explosion.ExplosionSize / 2));
                explosion.ExplosionCurrentImage = Math.Min((short)(Galaxy.ExplosionImageCount - 1), (short)((double)explosion.ExplosionProgression / num2 * (double)Galaxy.ExplosionImageCount));
                if ((double)explosion.ExplosionProgression > num2)
                {
                    explosion.ExplosionSize = 0;
                    explosion.ExplosionProgression = 0f;
                    explosion.ExplosionSoundPlayed = false;
                    explosionList.Add(explosion);
                    if (explosion.ExplosionWillDestroy)
                    {
                        CompleteTeardown(galaxy);
                    }
                }
            }
            foreach (Explosion item in explosionList)
            {
                Explosions.Remove(item);
            }
        }

        private bool InflictDamage(StellarObject target, double hitPower, DateTime time, Galaxy galaxy, float weaponDistanceTravelled, double strikeAngle)
        {
            return InflictDamage(target, hitPower, time, galaxy, weaponDistanceTravelled, allowRecursion: true, strikeAngle, allowArmorInvulnerability: false);
        }

        public bool InflictDamage(StellarObject abstractTarget, double hitPower, DateTime time, Galaxy galaxy, float weaponDistanceTravelled, bool allowRecursion, double strikeAngle, bool allowArmorInvulnerability)
        {
            hitPower *= BaconFighter.InflictDamageFighter(this);
            BaconFighter.CheckForLevelGain(this, hitPower);
            if (ParentBuiltObject != null)
            {
                hitPower *= ParentBuiltObject.CaptainFightersBonus;
                if (ParentBuiltObject.ShipGroup != null)
                {
                    hitPower *= ParentBuiltObject.ShipGroup.FightersBonus;
                }
            }
            if (abstractTarget is Creature)
            {
                Creature creature = (Creature)abstractTarget;
                if (creature.DamageCreature(this, (int)hitPower, null))
                {
                    if (creature.Type == CreatureType.SilverMist && Empire != null)
                    {
                        Empire.CivilityRating += Galaxy.DestroySilverMistReputationBonus;
                    }
                    creature.CompleteTeardown();
                    return true;
                }
            }
            else if (abstractTarget is Fighter)
            {
                Fighter fighter = (Fighter)abstractTarget;
                if (ParentBuiltObject != null && ParentBuiltObject.BattleStats != null)
                {
                    ParentBuiltObject.BattleStats.WeaponHitEnemy((float)hitPower, weaponDistanceTravelled);
                }
                if (ParentBuiltObject != null && ParentBuiltObject.ShipGroup != null && ParentBuiltObject.ShipGroup.BattleStats != null)
                {
                    ParentBuiltObject.ShipGroup.BattleStats.WeaponHitEnemy((float)hitPower, weaponDistanceTravelled);
                }
                if ((double)fighter.CurrentShields >= hitPower)
                {
                    fighter.CurrentShields -= (float)hitPower;
                    fighter.LastShieldStrike = time;
                    fighter.LastShieldStrikeDirection = (float)strikeAngle;
                    if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.BattleStats != null)
                    {
                        fighter.ParentBuiltObject.BattleStats.ShieldsStruckUs((float)hitPower);
                    }
                    if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.ShipGroup != null && fighter.ParentBuiltObject.ShipGroup.BattleStats != null)
                    {
                        fighter.ParentBuiltObject.ShipGroup.BattleStats.ShieldsStruckUs((float)hitPower);
                    }
                }
                else
                {
                    int num = (int)((float)hitPower - fighter.CurrentShields + 0.5f);
                    if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.BattleStats != null)
                    {
                        fighter.ParentBuiltObject.BattleStats.ShieldsStruckUs(fighter.CurrentShields);
                    }
                    if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.ShipGroup != null && fighter.ParentBuiltObject.ShipGroup.BattleStats != null)
                    {
                        fighter.ParentBuiltObject.ShipGroup.BattleStats.ShieldsStruckUs(fighter.CurrentShields);
                    }
                    fighter.CurrentShields = 0f;
                    if (num > fighter.Size)
                    {
                        num = fighter.Size;
                    }
                    if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.BattleStats != null)
                    {
                        fighter.ParentBuiltObject.BattleStats.DamageHullUs(num);
                    }
                    if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.ShipGroup != null && fighter.ParentBuiltObject.ShipGroup.BattleStats != null)
                    {
                        fighter.ParentBuiltObject.ShipGroup.BattleStats.DamageHullUs(num);
                    }
                    if (fighter.Size <= num && !fighter.HasBeenDestroyed)
                    {
                        fighter.Health = 0f;
                        fighter.HasBeenDestroyed = true;
                        if (ParentBuiltObject != null && ParentBuiltObject.BattleStats != null)
                        {
                            ParentBuiltObject.BattleStats.FighterDestroyedEnemy();
                        }
                        if (ParentBuiltObject != null && ParentBuiltObject.ShipGroup != null && ParentBuiltObject.ShipGroup.BattleStats != null)
                        {
                            ParentBuiltObject.ShipGroup.BattleStats.FighterDestroyedEnemy();
                        }
                        if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.BattleStats != null)
                        {
                            fighter.ParentBuiltObject.BattleStats.FighterDestroyedFriendly();
                        }
                        if (fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.ShipGroup != null && fighter.ParentBuiltObject.ShipGroup.BattleStats != null)
                        {
                            fighter.ParentBuiltObject.ShipGroup.BattleStats.FighterDestroyedFriendly();
                        }
                        if (Empire != null && Empire != galaxy.IndependentEmpire && fighter.Empire != null && fighter.Empire.PirateEmpireBaseHabitat != null)
                        {
                            double num2 = 0.015;
                            Empire.CivilityRating += num2;
                        }
                        Explosion explosion = new Explosion();
                        explosion.ExplosionStart = time;
                        explosion.ExplosionSize = (short)(Math.Sqrt((double)fighter.Size * 0.3) * (Math.PI / 4.0) * 30.0);
                        explosion.ExplosionProgression = 0f;
                        explosion.ExplosionOffsetX = 0;
                        explosion.ExplosionOffsetY = 0;
                        explosion.ExplosionImageIndex = (short)Galaxy.Rnd.Next(10, 20);
                        explosion.ExplosionWillDestroy = true;
                        fighter.Explosions.Add(explosion);
                        galaxy.InflictWarDamage(Empire, fighter);
                        if (fighter.Empire != null)
                        {
                            fighter.Empire.ResolveSystemVisibility(fighter.Xpos, fighter.Ypos, null, null);
                        }
                        return true;
                    }
                    fighter.Health -= (float)((double)num / (double)fighter.Size);
                    fighter.OverlayChanged = true;
                    Explosion explosion2 = new Explosion();
                    explosion2.ExplosionStart = time;
                    explosion2.ExplosionSize = (short)(Math.Sqrt((double)num * 0.3) * (Math.PI / 4.0) * 30.0);
                    if (explosion2.ExplosionSize < 5)
                    {
                        explosion2.ExplosionSize = 5;
                    }
                    explosion2.ExplosionProgression = 0f;
                    explosion2.ExplosionImageIndex = (short)Galaxy.Rnd.Next(0, 10);
                    int num3 = Galaxy.Rnd.Next(0, (int)(Math.Sqrt(fighter.Size) * 0.7));
                    if (Galaxy.Rnd.Next(0, 2) == 0)
                    {
                        num3 *= -1;
                    }
                    int num4 = Galaxy.Rnd.Next(0, (int)(Math.Sqrt(fighter.Size) * 0.7));
                    if (Galaxy.Rnd.Next(0, 2) == 0)
                    {
                        num4 *= -1;
                    }
                    explosion2.ExplosionOffsetX = (short)num3;
                    explosion2.ExplosionOffsetY = (short)num4;
                    explosion2.ExplosionWillDestroy = false;
                    fighter.Explosions.Add(explosion2);
                }
            }
            else if (abstractTarget is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)abstractTarget;
                if (ParentBuiltObject != null && ParentBuiltObject.BattleStats != null)
                {
                    ParentBuiltObject.BattleStats.WeaponHitEnemy((float)hitPower, weaponDistanceTravelled);
                }
                if (ParentBuiltObject != null && ParentBuiltObject.ShipGroup != null && ParentBuiltObject.ShipGroup.BattleStats != null)
                {
                    ParentBuiltObject.ShipGroup.BattleStats.WeaponHitEnemy((float)hitPower, weaponDistanceTravelled);
                }
                if ((double)builtObject.CurrentShields >= hitPower)
                {
                    builtObject.CurrentShields -= (float)hitPower;
                    builtObject.LastShieldStrike = time;
                    builtObject.LastShieldStrikeDirection = (float)strikeAngle;
                    if (builtObject.BattleStats != null)
                    {
                        builtObject.BattleStats.ShieldsStruckUs((float)hitPower);
                    }
                    if (builtObject.ShipGroup != null && builtObject.ShipGroup.BattleStats != null)
                    {
                        builtObject.ShipGroup.BattleStats.ShieldsStruckUs((float)hitPower);
                    }
                    galaxy.ChanceAttackedPirateFactionJoinsPhantomPirates(Empire, builtObject);
                }
                else
                {
                    int num5 = (int)((float)hitPower - builtObject.CurrentShields + 0.5f);
                    if (builtObject.BattleStats != null)
                    {
                        builtObject.BattleStats.ShieldsStruckUs(builtObject.CurrentShields);
                    }
                    if (builtObject.ShipGroup != null && builtObject.ShipGroup.BattleStats != null)
                    {
                        builtObject.ShipGroup.BattleStats.ShieldsStruckUs(builtObject.CurrentShields);
                    }
                    builtObject.CurrentShields = 0f;
                    if (builtObject.DamageRepair > 0 && builtObject.DamagedComponentCount == 0)
                    {
                        long num6 = (builtObject.LastRepair = galaxy.CurrentStarDate);
                    }
                    if (builtObject.Armor > 0)
                    {
                        BuiltObjectComponent builtObjectComponent = builtObject.Components[ComponentCategoryType.Armor, ComponentStatus.Normal];
                        int iterationCount = 0;
                        while (Galaxy.ConditionCheckLimit(builtObjectComponent != null && num5 > 0, 200, ref iterationCount))
                        {
                            if (builtObjectComponent.Value2 > 0)
                            {
                                int num7 = builtObjectComponent.Value2;
                                if (builtObject.ArmorReinforcingFactor > 0)
                                {
                                    num7 = (int)((double)num7 * ((double)builtObject.ArmorReinforcingFactor / 100.0));
                                }
                                if (num5 <= num7)
                                {
                                    if (allowArmorInvulnerability)
                                    {
                                        num5 = 0;
                                    }
                                    else
                                    {
                                        double num8 = (double)num7 / (double)num5;
                                        double num9 = Galaxy.Rnd.NextDouble() * num8;
                                        num5 = ((num9 < 0.2) ? 1 : 0);
                                    }
                                }
                                else
                                {
                                    num5 -= num7;
                                }
                            }
                            if (num5 > 0)
                            {
                                int num10 = builtObjectComponent.Value1;
                                if (builtObject.ArmorReinforcingFactor > 0)
                                {
                                    num10 = (int)((double)num10 * ((double)builtObject.ArmorReinforcingFactor / 100.0));
                                }
                                double val = (double)num5 / (double)num10;
                                val = Math.Max(0.1, val);
                                if (Galaxy.Rnd.NextDouble() < val)
                                {
                                    builtObjectComponent.Status = ComponentStatus.Damaged;
                                }
                                num5 -= num10;
                                builtObjectComponent = builtObject.Components[ComponentCategoryType.Armor, ComponentStatus.Normal];
                            }
                        }
                    }
                    double num11 = builtObject.DamageReduction;
                    if (builtObject.ShipGroup != null)
                    {
                        num11 *= builtObject.ShipGroup.DamageControlBonus;
                    }
                    num11 *= builtObject.CaptainDamageControlBonus;
                    num5 = (int)((double)num5 + 0.49 - (double)num5 * num11);
                    if (num5 > builtObject.Size)
                    {
                        num5 = builtObject.Size;
                    }
                    if (builtObject.BattleStats != null)
                    {
                        builtObject.BattleStats.DamageHullUs(num5);
                    }
                    if (builtObject.ShipGroup != null && builtObject.ShipGroup.BattleStats != null)
                    {
                        builtObject.ShipGroup.BattleStats.DamageHullUs(num5);
                    }
                    if (builtObject.UndamagedComponentSize <= num5 && !builtObject.HasBeenDestroyed)
                    {
                        if (ParentBuiltObject != null && ParentBuiltObject.BattleStats != null)
                        {
                            ParentBuiltObject.BattleStats.TargetDestroyedEnemyByFighter(builtObject);
                        }
                        if (ParentBuiltObject != null && ParentBuiltObject.ShipGroup != null && ParentBuiltObject.ShipGroup.BattleStats != null)
                        {
                            ParentBuiltObject.ShipGroup.BattleStats.TargetDestroyedEnemyByFighter(builtObject);
                        }
                        if (builtObject.BattleStats != null)
                        {
                            builtObject.BattleStats.TargetDestroyedFriendlyByFighter(builtObject);
                        }
                        if (builtObject.ShipGroup != null && builtObject.ShipGroup.BattleStats != null)
                        {
                            builtObject.ShipGroup.BattleStats.TargetDestroyedFriendlyByFighter(builtObject);
                        }
                        galaxy.CheckTriggerEvent(builtObject.GameEventId, Empire, EventTriggerType.Destroy, null);
                        if (Empire != null && Empire != galaxy.IndependentEmpire && builtObject.Empire != null && builtObject.Empire.PirateEmpireBaseHabitat != null)
                        {
                            double num12 = 0.05;
                            switch (builtObject.SubRole)
                            {
                                case BuiltObjectSubRole.SmallSpacePort:
                                    num12 = 0.25;
                                    break;
                                case BuiltObjectSubRole.MediumSpacePort:
                                    num12 = 0.35;
                                    break;
                                case BuiltObjectSubRole.LargeSpacePort:
                                    num12 = 0.5;
                                    break;
                            }
                            Empire.CivilityRating += num12;
                        }
                        galaxy.ChanceRaceEvent(builtObject, ParentBuiltObject);
                        if (!galaxy.ChanceNewShipCaptain(builtObject, Empire, ParentBuiltObject))
                        {
                            galaxy.ChanceNewFleetAdmiral(builtObject, Empire, ParentBuiltObject);
                        }
                        if (Empire != null && Empire.Counters != null)
                        {
                            Empire.Counters.ProcessBuiltObjectDestruction(builtObject);
                        }
                        Explosion explosion3 = new Explosion();
                        explosion3.ExplosionStart = time;
                        explosion3.ExplosionSize = (short)(Math.Sqrt(builtObject.Components.Count) * (Math.PI / 4.0) * 30.0);
                        explosion3.ExplosionProgression = 0f;
                        explosion3.ExplosionOffsetX = 0;
                        explosion3.ExplosionOffsetY = 0;
                        explosion3.ExplosionImageIndex = (short)Galaxy.Rnd.Next(10, 20);
                        explosion3.ExplosionWillDestroy = true;
                        builtObject.Explosions.Add(explosion3);
                        builtObject.HasBeenDestroyed = true;
                        galaxy.InflictWarDamage(Empire, builtObject);
                        if (allowRecursion)
                        {
                            int num13 = (int)(builtObject.Xpos / (double)Galaxy.IndexSize);
                            int num14 = (int)(builtObject.Ypos / (double)Galaxy.IndexSize);
                            for (int i = 0; i < galaxy.BuiltObjectIndex[num13][num14].Count; i++)
                            {
                                BuiltObject builtObject2 = galaxy.BuiltObjectIndex[num13][num14][i];
                                if (builtObject2 != builtObject && galaxy.CheckWithinDistancePotential(400.0, builtObject.Xpos, builtObject.Ypos, builtObject2.Xpos, builtObject2.Ypos))
                                {
                                    double num15 = galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, builtObject2.Xpos, builtObject2.Ypos);
                                    double num16 = (double)builtObject.Size * 0.25 - num15 * 2.0;
                                    if (num16 > 0.0)
                                    {
                                        InflictDamage(builtObject2, num16, time, galaxy, weaponDistanceTravelled, allowRecursion: false, double.MinValue, allowArmorInvulnerability: false);
                                    }
                                }
                            }
                        }
                        if (builtObject.Empire != null)
                        {
                            builtObject.Empire.ResolveSystemVisibility(builtObject.Xpos, builtObject.Ypos, builtObject, null);
                        }
                        if (builtObject.ConstructionQueue != null && builtObject.ConstructionQueue.ConstructionYards.CountUnderConstruction > 0)
                        {
                            foreach (ConstructionYard constructionYard in builtObject.ConstructionQueue.ConstructionYards)
                            {
                                BuiltObject shipUnderConstruction = constructionYard.ShipUnderConstruction;
                                shipUnderConstruction?.InflictDamage(shipUnderConstruction, null, double.MaxValue, time, galaxy, weaponDistanceTravelled, allowRecursion: false, double.MinValue, allowArmorInvulnerability: false);
                            }
                        }
                        builtObject.ReDefine();
                        return true;
                    }
                    int num17 = num5;
                    int iterationCount2 = 0;
                    while (Galaxy.ConditionCheckLimit(num5 > 0, 200, ref iterationCount2))
                    {
                        int num18 = 0;
                        int num19 = 0;
                        do
                        {
                            num18 = Galaxy.Rnd.Next(0, builtObject.Components.Count);
                            num19++;
                        }
                        while (num19 <= 30 && builtObject.Components[num18].Status == ComponentStatus.Damaged);
                        if (builtObject.Components[num18].Status == ComponentStatus.Damaged)
                        {
                            galaxy.ReseedRandom();
                        }
                        builtObject.Components[num18].Status = ComponentStatus.Damaged;
                        if (builtObject.Role != BuiltObjectRole.Base)
                        {
                            switch (builtObject.Components[num18].Type)
                            {
                                case ComponentType.StorageCargo:
                                    {
                                        int num21 = builtObject.Components[num18].Value1;
                                        if (builtObject.Cargo == null || builtObject.Cargo.Count <= 0)
                                        {
                                            break;
                                        }
                                        CargoList cargoList = new CargoList();
                                        for (int j = 0; j < builtObject.Cargo.Count; j++)
                                        {
                                            Cargo cargo = builtObject.Cargo[j];
                                            if (num21 <= 0)
                                            {
                                                break;
                                            }
                                            if (cargo.Amount > num21)
                                            {
                                                cargo.Amount -= num21;
                                                num21 = 0;
                                                break;
                                            }
                                            if (cargo.Amount > 0)
                                            {
                                                num21 -= cargo.Amount;
                                                cargoList.Add(cargo);
                                            }
                                        }
                                        foreach (Cargo item in cargoList)
                                        {
                                            builtObject.Cargo.Remove(item);
                                        }
                                        break;
                                    }
                                case ComponentType.StorageFuel:
                                    {
                                        int value2 = builtObject.Components[num18].Value1;
                                        if (builtObject.CurrentFuel > 0.0)
                                        {
                                            builtObject.CurrentFuel -= value2;
                                            if (builtObject.CurrentFuel < 0.0)
                                            {
                                                builtObject.CurrentFuel = 0.0;
                                            }
                                        }
                                        break;
                                    }
                                case ComponentType.StorageTroop:
                                    {
                                        int value = builtObject.Components[num18].Value1;
                                        if (builtObject.Troops == null || builtObject.TroopCapacity <= 0 || builtObject.Troops.TotalSize <= 0)
                                        {
                                            break;
                                        }
                                        builtObject.TroopCapacity -= value;
                                        if (builtObject.Troops.TotalSize <= builtObject.TroopCapacity)
                                        {
                                            break;
                                        }
                                        int num20 = Galaxy.Rnd.Next(0, builtObject.Troops.Count);
                                        if (num20 < builtObject.Troops.Count)
                                        {
                                            if (builtObject.Empire != null && builtObject.Empire.Troops != null)
                                            {
                                                builtObject.Empire.Troops.Remove(builtObject.Troops[num20]);
                                            }
                                            builtObject.Troops.RemoveAt(num20);
                                        }
                                        break;
                                    }
                            }
                        }
                        num5 -= builtObject.Components[num18].Size;
                    }
                    builtObject.ReDefine();
                    if (builtObject.Role != BuiltObjectRole.Base && builtObject.DamagedComponentCount > 0)
                    {
                        builtObject.RepairForNextMission = true;
                    }
                    Explosion explosion4 = new Explosion();
                    explosion4.ExplosionStart = time;
                    explosion4.ExplosionSize = (short)(Math.Sqrt(num17) * (Math.PI / 4.0) * 30.0);
                    if (explosion4.ExplosionSize < 10)
                    {
                        explosion4.ExplosionSize = 10;
                    }
                    explosion4.ExplosionProgression = 0f;
                    explosion4.ExplosionImageIndex = (short)Galaxy.Rnd.Next(0, 10);
                    int num22 = Galaxy.Rnd.Next(0, (int)(Math.Sqrt(builtObject.Size) * 0.7));
                    if (Galaxy.Rnd.Next(0, 2) == 0)
                    {
                        num22 *= -1;
                    }
                    int num23 = Galaxy.Rnd.Next(0, (int)(Math.Sqrt(builtObject.Size) * 0.7));
                    if (Galaxy.Rnd.Next(0, 2) == 0)
                    {
                        num23 *= -1;
                    }
                    explosion4.ExplosionOffsetX = (short)num22;
                    explosion4.ExplosionOffsetY = (short)num23;
                    explosion4.ExplosionWillDestroy = false;
                    builtObject.Explosions.Add(explosion4);
                }
            }
            return false;
        }

        private void FireWeaponsAtTarget(Galaxy galaxy, StellarObject target, double distanceToTarget, DateTime time)
        {
            BaconFighter.FireWeaponsAtTarget(this, galaxy, target, distanceToTarget, time);
        }

        public bool DetermineHitTarget(Galaxy galaxy, FighterWeapon weapon, StellarObject target, double distanceToTarget, out double hitRangeChance)
        {
            double num = (double)weapon.Range - distanceToTarget;
            hitRangeChance = Math.Max(0.0, num / (double)weapon.Range);
            double val = 10.0 / Math.Max(1.0, target.CurrentSpeed);
            val = Math.Max(0.7, Math.Min(val, 3.0));
            double num2 = 0.0;
            ShipGroup shipGroup = null;
            double num3 = 1.0;
            if (target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)target;
                num2 = builtObject.CountermeasureModifier + builtObject.FleetCountermeasureBonus;
                num3 = builtObject.CaptainCountermeasuresBonus;
                if (builtObject.Empire != null)
                {
                    num2 += (builtObject.Empire.CountermeasuresFactor - 1.0) * 100.0;
                    if (builtObject.Empire.RaceEventType == RaceEventType.PredictiveHistory)
                    {
                        num2 += 20.0;
                    }
                }
                shipGroup = builtObject.ShipGroup;
            }
            else if (target is Fighter)
            {
                Fighter fighter = (Fighter)target;
                num2 = fighter.Specification.CountermeasureModifier;
                if (fighter.ParentBuiltObject != null && !fighter.ParentBuiltObject.HasBeenDestroyed)
                {
                    num2 += (double)fighter.ParentBuiltObject.FleetCountermeasureBonus;
                }
                if (fighter.Empire != null)
                {
                    num2 += (fighter.Empire.CountermeasuresFactor - 1.0) * 100.0;
                    if (fighter.Empire.RaceEventType == RaceEventType.PredictiveHistory)
                    {
                        num2 += 20.0;
                    }
                }
            }
            double num4 = Specification.TargettingModifier;
            if (Empire != null)
            {
                num4 += (Empire.TargettingFactor - 1.0) * 100.0;
                if (Empire.RaceEventType == RaceEventType.PredictiveHistory)
                {
                    num4 += 20.0;
                }
            }
            double num5 = (num4 - num2) / 100.0;
            double num6 = val * (hitRangeChance + Galaxy.Rnd.NextDouble() + num5);
            if (ParentBuiltObject != null)
            {
                num6 *= ParentBuiltObject.CaptainFightersBonus;
                if (ParentBuiltObject.ShipGroup != null)
                {
                    num6 *= ParentBuiltObject.ShipGroup.FightersBonus;
                }
            }
            if (shipGroup != null)
            {
                num6 /= shipGroup.CountermeasuresBonus;
            }
            num6 /= num3;
            if (num6 > 0.5 && Galaxy.Rnd.Next(0, 12) == 0)
            {
                num6 = 0.0;
            }
            else if (num6 <= 0.5 && num > 0.0 && Galaxy.Rnd.Next(0, 12) == 0)
            {
                num6 = 1.0;
            }
            if (num6 > 0.5)
            {
                return true;
            }
            return false;
        }

        public void UpdateMissionParameters(Galaxy galaxy, DateTime time)
        {
            switch (MissionType)
            {
                case FighterMissionType.Attack:
                    PursueTarget(galaxy, time);
                    break;
                case FighterMissionType.Patrol:
                    PerformPatrol(galaxy);
                    break;
                case FighterMissionType.ReturnToCarrier:
                    if (ParentBuiltObject != null)
                    {
                        double num = Galaxy.DetermineAngle(Xpos, Ypos, ParentBuiltObject.Xpos, ParentBuiltObject.Ypos);
                        TargetHeading = (float)num;
                        CheckForCarrierBoarding(galaxy);
                    }
                    break;
            }
        }

        private void PerformPatrol(Galaxy galaxy)
        {
            if (ParentBuiltObject == null)
            {
                return;
            }
            double num = galaxy.CalculateDistance(Xpos, Ypos, ParentBuiltObject.Xpos, ParentBuiltObject.Ypos);
            if ((double)ParentBuiltObject.CurrentSpeed > 6.0)
            {
                if (num > 300.0)
                {
                    double num2 = Galaxy.DetermineAngle(Xpos, Ypos, ParentBuiltObject.Xpos, ParentBuiltObject.Ypos);
                    float num3 = (TargetHeading = (float)num2 + RandomHeadingOffset(galaxy));
                    TargetSpeed = (float)TopSpeed * 0.5f;
                }
                else if (num < 150.0)
                {
                    double num4 = Galaxy.DetermineAngle(Xpos, Ypos, ParentBuiltObject.Xpos, ParentBuiltObject.Ypos);
                    float num5 = (TargetHeading = (float)(num4 + Math.PI + (double)RandomHeadingOffset(galaxy)));
                    TargetSpeed = (float)TopSpeed * 0.5f;
                }
                else
                {
                    TargetHeading = ParentBuiltObject.Heading;
                    TargetSpeed = ParentBuiltObject.CurrentSpeed;
                }
            }
            else
            {
                if (num > 500.0)
                {
                    double num6 = Galaxy.DetermineAngle(Xpos, Ypos, ParentBuiltObject.Xpos, ParentBuiltObject.Ypos);
                    float num7 = Math.Abs((float)num6 - TargetHeading);
                    if (num7 > 0.7f)
                    {
                        float num8 = (float)(num6 + Galaxy.Rnd.NextDouble() * 0.65);
                        if (Galaxy.Rnd.Next(0, 2) == 1)
                        {
                            num8 *= -1f;
                        }
                        TargetHeading = num8;
                    }
                }
                TargetSpeed = (float)TopSpeed * 0.5f;
            }
            while (!WillMeetDestination(galaxy, ParentBuiltObject.Xpos, ParentBuiltObject.Ypos, TargetSpeed))
            {
                TargetSpeed /= 1.5f;
                if (TargetSpeed < 1f)
                {
                    break;
                }
            }
        }

        private void BoardCarrier()
        {
            if (ParentBuiltObject != null && !ParentBuiltObject.HasBeenDestroyed)
            {
                TargetSpeed = 0f;
                CurrentSpeed = 0f;
                OnboardCarrier = true;
                MissionType = FighterMissionType.Undefined;
                Xpos = ParentBuiltObject.Xpos;
                Ypos = ParentBuiltObject.Ypos;
            }
        }

        private void CheckForCarrierBoarding(Galaxy galaxy)
        {
            if (MissionType != FighterMissionType.ReturnToCarrier || ParentBuiltObject == null || ParentBuiltObject.HasBeenDestroyed || ParentBuiltObject.FighterCapacity <= 0)
            {
                return;
            }
            double num = galaxy.CalculateDistance(Xpos, Ypos, ParentBuiltObject.Xpos, ParentBuiltObject.Ypos);
            double num2 = 30.0;
            double num3 = 200.0;
            if (!InView)
            {
                num2 *= 4.0;
                num3 *= 2.0;
            }
            if (num < num2)
            {
                BoardCarrier();
            }
            else if (num < num3)
            {
                TargetSpeed = (float)((double)TopSpeed * 0.3) + ParentBuiltObject.CurrentSpeed;
                TargetSpeed = Math.Min(TargetSpeed, TopSpeed);
            }
            else
            {
                TargetSpeed = TopSpeed;
            }
            while (!WillMeetDestination(galaxy, ParentBuiltObject.Xpos, ParentBuiltObject.Ypos, TargetSpeed))
            {
                TargetSpeed /= 1.5f;
                if (TargetSpeed < 1f)
                {
                    break;
                }
            }
        }

        public void DoMovement(Galaxy galaxy, double timePassed)
        {
            CalculateCurrentHeading(timePassed);
            AccelerateToTargetSpeed(timePassed);
            ConsumeEnergy(timePassed);
            double num = (double)CurrentSpeed * timePassed;
            if (!InView && !OnboardCarrier && MissionType == FighterMissionType.ReturnToCarrier && ParentBuiltObject != null)
            {
                double num2 = galaxy.CalculateDistance(Xpos, Ypos, ParentBuiltObject.Xpos, ParentBuiltObject.Ypos);
                if (num > num2 * 0.6)
                {
                    BoardCarrier();
                    return;
                }
            }
            Xpos += Math.Cos(Heading) * num;
            Ypos += Math.Sin(Heading) * num;
            if (OnboardCarrier && ParentBuiltObject != null)
            {
                Xpos = ParentBuiltObject.Xpos;
                Ypos = ParentBuiltObject.Ypos;
            }
            else if (!InView && ParentBuiltObject != null)
            {
                double num3 = galaxy.CalculateDistanceSquared(Xpos, Ypos, ParentBuiltObject.Xpos, ParentBuiltObject.Ypos);
                double num4 = 0.0;
                double num5 = 0.0;
                switch (MissionType)
                {
                    case FighterMissionType.Patrol:
                        num4 = 360000.0;
                        num5 = 600.0;
                        break;
                    case FighterMissionType.Attack:
                        num4 = 2250000.0;
                        num5 = 1500.0;
                        break;
                    default:
                        num4 = 2250000.0;
                        num5 = 1500.0;
                        break;
                }
                if (num3 > num4)
                {
                    double num6 = Galaxy.DetermineAngle(ParentBuiltObject.Xpos, ParentBuiltObject.Ypos, Xpos, Ypos);
                    Xpos = ParentBuiltObject.Xpos + Math.Cos(num6) * num5;
                    Ypos = ParentBuiltObject.Ypos + Math.Sin(num6) * num5;
                }
            }
        }

        private void ConsumeEnergy(double timePassed)
        {
            if (TargetSpeed == (float)TopSpeed)
            {
                CurrentEnergy -= (float)((double)Specification.TopSpeedEnergyConsumptionRate * timePassed);
            }
            else
            {
                CurrentEnergy -= (float)((double)Specification.TopSpeedEnergyConsumptionRate * timePassed * 0.5);
            }
        }

        private void CalculateCurrentHeading(double timePassed)
        {
            if (Heading == TargetHeading)
            {
                return;
            }
            HeadingChanged = true;
            double num = GetCurrentTurnRate() * timePassed;
            double num2 = TargetHeading - Heading;
            if (num2 > Math.PI)
            {
                num2 -= Math.PI * 2.0;
            }
            else if (num2 < -Math.PI)
            {
                num2 += Math.PI * 2.0;
            }
            if ((num2 < 0.0 && num2 > -Math.PI) || (num2 >= Math.PI && num2 < Math.PI * 2.0))
            {
                if (Math.Abs(num2) < Math.Abs(num))
                {
                    Heading = TargetHeading;
                    TurnDirection = TurnDirection.StraightAhead;
                }
                else
                {
                    Heading -= (float)num;
                    TurnDirection = TurnDirection.Left;
                }
                int iterationCount = 0;
                while (Galaxy.ConditionCheckLimit((double)Heading <= -Math.PI, 50, ref iterationCount))
                {
                    Heading = (float)IncreaseAngle(Heading);
                }
            }
            else
            {
                if (Math.Abs(num2) < Math.Abs(num))
                {
                    Heading = TargetHeading;
                    TurnDirection = TurnDirection.StraightAhead;
                }
                else
                {
                    Heading += (float)num;
                    TurnDirection = TurnDirection.Right;
                }
                int iterationCount2 = 0;
                while (Galaxy.ConditionCheckLimit((double)Heading >= Math.PI, 50, ref iterationCount2))
                {
                    Heading = (float)ReduceAngle(Heading);
                }
            }
        }

        private double ReduceAngle(double currentangle)
        {
            if (currentangle >= Math.PI)
            {
                currentangle -= Math.PI * 2.0;
            }
            return currentangle;
        }

        private double IncreaseAngle(double currentangle)
        {
            if (currentangle <= -Math.PI)
            {
                currentangle += Math.PI * 2.0;
            }
            return currentangle;
        }

        public void ReturnToCarrier()
        {
            if (ParentBuiltObject != null && !ParentBuiltObject.HasBeenDestroyed && ParentBuiltObject.FighterCapacity > 0 && MissionType != FighterMissionType.ReturnToCarrier)
            {
                AbandonAttackTarget();
                MissionType = FighterMissionType.ReturnToCarrier;
                double num = Galaxy.DetermineAngle(Xpos, Ypos, ParentBuiltObject.Xpos, ParentBuiltObject.Ypos);
                TargetHeading = (float)num;
                TargetSpeed = TopSpeed;
            }
        }

        public float RandomHeadingOffset(Galaxy galaxy)
        {
            return RandomHeadingOffset(galaxy, 0.3f);
        }

        public float RandomHeadingOffset(Galaxy galaxy, float maxAmount)
        {
            return (float)((double)(maxAmount * -1f) + Galaxy.Rnd.NextDouble() * (double)maxAmount * 2.0);
        }

        private void PursueTarget(Galaxy galaxy, DateTime time)
        {
            BaconFighter.PursueTarget(this, galaxy, time);
        }

        public bool CheckShouldFireAtCaptureTarget(StellarObject target)
        {
            bool result = true;
            bool flag = false;
            if (target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)target;
                if (builtObject != null && builtObject.AssaultAttackValue > 0 && Empire != null && builtObject.AssaultAttackEmpireId == Empire.EmpireId)
                {
                    flag = true;
                }
            }
            if (ParentBuiltObject != null && ParentBuiltObject.Mission != null && ParentBuiltObject.Mission.Type == BuiltObjectMissionType.Capture && ParentBuiltObject.Mission.Target == target)
            {
                flag = true;
            }
            if (flag && target is BuiltObject)
            {
                BuiltObject builtObject2 = (BuiltObject)target;
                if ((double)builtObject2.CurrentShields <= 30.0)
                {
                    result = false;
                }
                else if (Empire.CheckOurEmpireOverwhelmingBoarding(builtObject2))
                {
                    result = false;
                }
            }
            return result;
        }

        public void AssignAttackTarget(StellarObject target)
        {
            AbandonAttackTarget();
            if (target != null)
            {
                CurrentTarget = target;
                StellarObjectList pursuers = target.Pursuers;
                if (pursuers != null && !pursuers.Contains(this))
                {
                    pursuers.Add(this);
                }
                MissionType = FighterMissionType.Attack;
            }
        }

        public void AbandonAttackTarget()
        {
            BaconFighter.AbandonAttackTarget(this);
        }

        public bool WillMeetDestination(Galaxy galaxy, double destinationX, double destinationY, double speed)
        {
            if (TurnDirection == TurnDirection.StraightAhead)
            {
                return true;
            }
            double currentTurnRate = GetCurrentTurnRate(speed);
            double num = Math.PI / currentTurnRate * speed;
            double num2 = num / Math.PI;
            double num3 = 0.0;
            switch (TurnDirection)
            {
                case TurnDirection.Left:
                    num3 = (double)Heading - Math.PI / 2.0;
                    break;
                case TurnDirection.Right:
                    num3 = (double)Heading + Math.PI / 2.0;
                    break;
            }
            double num4 = Math.Cos(num3) * num2;
            double num5 = Math.Tan(num3) * num4;
            double x = Xpos + num4;
            double y = Ypos + num5;
            double num6 = galaxy.CalculateDistance(x, y, destinationX, destinationY);
            if (num6 < num2)
            {
                return false;
            }
            double num7 = Galaxy.DetermineAngle(Xpos, Ypos, destinationX, destinationY);
            double num8 = (double)Heading - num7;
            double num9 = Math.Abs(num8 / currentTurnRate * speed);
            double num10 = galaxy.CalculateDistance(Xpos, Ypos, destinationX, destinationY);
            if (num9 > num10 * 1.2)
            {
                return false;
            }
            return true;
        }

        private void AccelerateToTargetSpeed(double timePassed)
        {
            if (TargetSpeed > CurrentSpeed)
            {
                float num = Specification.AccelerationRate * (float)timePassed;
                if ((double)(CurrentSpeed + num) >= (double)TargetSpeed)
                {
                    CurrentSpeed = TargetSpeed;
                }
                else
                {
                    CurrentSpeed += num;
                }
            }
            else if (TargetSpeed < CurrentSpeed)
            {
                double num2 = Math.Max(1.0, Specification.AccelerationRate);
                double num3 = num2 * timePassed;
                if ((double)CurrentSpeed - num3 < (double)TargetSpeed)
                {
                    CurrentSpeed = TargetSpeed;
                }
                else
                {
                    CurrentSpeed -= (float)num3;
                }
            }
            if (CurrentSpeed < 0f)
            {
                CurrentSpeed = 0f;
            }
        }

        private double GetCurrentTurnRate()
        {
            return GetCurrentTurnRate(CurrentSpeed);
        }

        private double GetCurrentTurnRate(double speed)
        {
            float num = Specification.TurnRate;
            if (speed <= (double)TopSpeed * 0.12)
            {
                num *= 4f;
            }
            if (speed < (double)TopSpeed * 0.25)
            {
                num *= 2.6f;
            }
            if (speed < (double)TopSpeed * 0.5)
            {
                num *= 1.6f;
            }
            return num;
        }

        public void RechargeEnergy(double timePassed)
        {
            if (Specification != null)
            {
                CurrentEnergy += (float)(timePassed * (double)Specification.EnergyRechargeRate);
                CurrentEnergy = Math.Min(CurrentEnergy, Specification.EnergyCapacity);
            }
        }

        public void RechargeShields(double timePassed)
        {
            if (Specification != null)
            {
                float val = (float)(timePassed * (double)Specification.ShieldRechargeRate);
                val = Math.Min(val, CurrentEnergy);
                val = Math.Min(val, (float)Specification.ShieldsCapacity - CurrentShields);
                CurrentShields += val;
                CurrentEnergy -= val;
                CurrentShields = Math.Max(0f, Math.Min(CurrentShields, Specification.ShieldsCapacity));
            }
        }

        public void RepairDamage(double timePassed)
        {
            if (Health < 1f && Specification != null && Specification.DamageRepairRate > 0)
            {
                float num = (float)(timePassed * (double)Specification.DamageRepairRate);
                Health += num;
                Health = Math.Min(Health, 1f);
            }
        }
    }
}
