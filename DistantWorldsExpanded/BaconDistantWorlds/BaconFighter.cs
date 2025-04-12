// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconFighter
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;

namespace BaconDistantWorlds
{
    public static class BaconFighter
    {
        public static double maximumTargetDistanceSquared = 225.0;
        public static double fighterRangeMultiple = 1.0;
        public static double starbaseFighterRangeMultiplier = 2.0;
        public static double myDamageMultiplier = 3.0;
        public static float ammoExhaustChanceMissile = 0.2f;
        public static float ammoExhaustChanceTorpedo = 0.25f;
        public static float fighterBuildSpeedDivisor = 2f;
        public static int fighterBuildCost = 0;
        public static float fighterOnBomberDamageMultiplier = 1f;

        public static bool IsMyFighter(Fighter fighter) => fighter != null && fighter.Empire != null && fighter.Empire.Name.Contains("Romulan") || fighter != null && fighter.Owner != null && fighter.Owner.Name.Contains("Romulan");

        public static double CalculateMaximumTargetRange(Fighter fighter)
        {
            BuiltObject parentBuiltObject = fighter.ParentBuiltObject;
            return parentBuiltObject != null && parentBuiltObject.Role == BuiltObjectRole.Base ? BaconFighter.maximumTargetDistanceSquared * BaconFighter.fighterRangeMultiple * (double)fighter.TopSpeed * (double)fighter.TopSpeed * BaconFighter.starbaseFighterRangeMultiplier : BaconFighter.maximumTargetDistanceSquared * BaconFighter.fighterRangeMultiple * (double)fighter.TopSpeed * (double)fighter.TopSpeed;
        }

        public static double InflictDamageFighter(Fighter fighter)
        {
            double num = 1.0;
            if (BaconFighter.IsMyFighter(fighter))
                num = BaconFighter.myDamageMultiplier;
            try
            {
                if (fighter.Specification.Type == FighterType.Interceptor && fighter.CurrentTarget is Fighter && ((Fighter)fighter.CurrentTarget).Specification.Type == FighterType.Bomber)
                    num *= (double)BaconFighter.fighterOnBomberDamageMultiplier;
            }
            catch (Exception ex)
            {
                throw;
            }
            return num;
        }

        public static void CheckForLevelGain(Fighter fighter, double damageInflicted)
        {
            if ((double)new Random().Next(1000) >= damageInflicted)
                return;
            BaconFighter.GainFighterLevel(fighter);
        }

        public static void GainFighterLevel(Fighter fighter)
        {
            FighterSpecification fighterSpecification = BaconFighter.CloneFighterSpecification(fighter.Specification);
            if (!fighter.Name.Substring(0, 2).Contains(" "))
            {
                bool flag = fighter.BaconAutomationEnabled;
                string str = " Ace";
                if (BaconBuiltObject.myMain != null)
                    str = BaconBuiltObject.myMain._Game.Galaxy.GenerateUniqueAgentName((byte)0);
                fighter.Name = (flag ? "!" : "") + " " + str;
            }
            switch (new Random(fighter.Name.GetHashCode()).Next(5))
            {
                case 0:
                    fighterSpecification.TopSpeed += (short)10;
                    fighter.TopSpeed += (short)10;
                    fighterSpecification.TurnRate += 0.05f;
                    break;
                case 1:
                    fighterSpecification.ShieldsCapacity += (short)5;
                    fighterSpecification.ShieldRechargeRate += fighterSpecification.ShieldRechargeRate / 10f;
                    break;
                case 2:
                    fighterSpecification.EnergyCapacity += (short)Math.Max(1, (int)fighterSpecification.EnergyCapacity / 10);
                    fighterSpecification.EnergyRechargeRate += Math.Max(1f, fighterSpecification.EnergyRechargeRate / 10f);
                    fighterSpecification.CountermeasureModifier += (short)3;
                    fighterSpecification.TargettingModifier += (short)5;
                    break;
                case 3:
                    fighter.FirepowerRaw += (int)Math.Max((short)1, (short)((int)fighterSpecification.WeaponDamage / 10));
                    fighterSpecification.WeaponDamage += Math.Max((short)1, (short)((int)fighterSpecification.WeaponDamage / 10));
                    fighter.Weapons[0].RawDamage += Math.Max((short)1, (short)((int)fighterSpecification.WeaponDamage / 10));
                    break;
                case 4:
                    fighterSpecification.WeaponSpeed += (short)((int)fighterSpecification.WeaponSpeed / 10);
                    fighterSpecification.WeaponRange += (short)((int)fighterSpecification.WeaponRange / 10);
                    break;
            }
            fighter.Specification = fighterSpecification;
        }

        public static void PayWhenFighterIsBuilt(Fighter fighter)
        {
            if (fighter.ParentBuiltObject == null || fighter.Empire == null)
                return;
            if (!fighter.ParentBuiltObject.Fighters.Any<Fighter>((Func<Fighter, bool>)(x => x.UnderConstruction)) && BaconBuiltObject.myMain != null && fighter.Empire == BaconBuiltObject.myMain._Game.PlayerEmpire)
                fighter.Empire.SendMessageToEmpire(fighter.Empire, EmpireMessageType.Undefined, (object)null, "All Fighters and bombers on " + fighter.ParentBuiltObject.Name + " have been repaired.", Point.Empty, "fighterRepaired");
            float num = 1f;
            if (BaconFighter.fighterBuildCost != 0 && BaconBuiltObject.myMain != null && fighter.Empire == BaconBuiltObject.myMain._Game.PlayerEmpire)
                num = BaconFighter.GetCustomBomberPriceMultiplier(fighter);
            fighter.Empire.StateMoney -= (double)(BaconFighter.fighterBuildCost * fighter.Size) * (double)num;
        }

        public static float GetCustomBomberPriceMultiplier(Fighter fighter)
        {
            float bomberPriceMultiplier = 1f;
            Empire empire = fighter.Empire;
            List<Tuple<FighterSpecification, float, float, short>> tupleList = new List<Tuple<FighterSpecification, float, float, short>>();
            if (empire != null)
            {
                List<Tuple<FighterSpecification, float, float, short>> customFighterDesigns = BaconBuiltObject.GetCustomFighterDesigns(empire);
                if (customFighterDesigns != null)
                {
                    Tuple<FighterSpecification, float, float, short> tuple = customFighterDesigns.FirstOrDefault<Tuple<FighterSpecification, float, float, short>>((Func<Tuple<FighterSpecification, float, float, short>, bool>)(x => x.Item1.Name == fighter.Name));
                    if (tuple != null)
                        return tuple.Item2;
                }
            }
            return bomberPriceMultiplier;
        }

        public static void CheckReturnToCarrier(Fighter fighter, Galaxy galaxy)
        {
            if (fighter.OnboardCarrier || fighter.MissionType == FighterMissionType.ReturnToCarrier || fighter.ParentBuiltObject == null)
                return;
            if (fighter.ParentBuiltObject.CheckHyperjumpPending() && fighter.MissionType == FighterMissionType.Undefined)
            {
                fighter.Name = fighter.Name.Replace("!", "");
                fighter.BaconAutomationEnabled = false;
                fighter.ReturnToCarrier();
            }
            else if (fighter.MissionType == FighterMissionType.Attack && fighter.CurrentTarget != null)
            {
                if (galaxy.CalculateDistanceSquared(fighter.CurrentTarget.Xpos, fighter.CurrentTarget.Ypos, fighter.ParentBuiltObject.Xpos, fighter.ParentBuiltObject.Ypos) > BaconFighter.CalculateMaximumTargetRange(fighter))
                {
                    fighter.AbandonAttackTarget();
                    fighter.EvaluateThreats(galaxy);
                }
            }
            else if (galaxy.CalculateDistanceSquared(fighter.Xpos, fighter.Ypos, fighter.ParentBuiltObject.Xpos, fighter.ParentBuiltObject.Ypos) > BaconFighter.CalculateMaximumTargetRange(fighter))
            {
                fighter.Name = fighter.Name.Replace("!", "");
                fighter.BaconAutomationEnabled = false;
                fighter.ReturnToCarrier();
            }
        }

        public static void DoTasks(Fighter fighter, Galaxy galaxy, DateTime time, bool inView)
        {
            double timePassed1 = (double)time.Subtract(fighter._LastTouch).Ticks / 10000000.0;
            fighter.InView = inView;
            fighter.RechargeEnergy(timePassed1);
            fighter.RechargeShields(timePassed1);
            fighter.RepairDamage(timePassed1);
            fighter.DoMovement(galaxy, timePassed1);
            fighter.UpdateMissionParameters(galaxy, time);
            BaconFighter.CheckBomberDefensiveFire(fighter, galaxy, time);
            fighter.HandleWeaponsFiring(galaxy, time, timePassed1);
            if (fighter.Explosions.Count > 0)
                fighter.DoExplosions(galaxy, time);
            int num = fighter.MissionType == FighterMissionType.Attack ? 1 : (fighter.Attackers == null ? 0 : (fighter.Attackers.Count > 0 ? 1 : 0));
            fighter.InBattle = num != 0;
            if (fighter.OnboardCarrier)
                fighter.MissionType = FighterMissionType.Undefined;
            if (time.Subtract(fighter._LastLongTouch) >= Galaxy.IntermediateProcessingSpan)
            {
                double timePassed2 = (double)time.Subtract(fighter._LastLongTouch).Ticks / 10000000.0;
                fighter.ReturnToCarrierForRepairs();
                fighter.EvaluateThreats(galaxy);
                fighter.CheckCarrierDestroyed(galaxy);
                fighter.CheckReturnToCarrier(galaxy);
                fighter.ApplyLocationEffectsNEW(galaxy, timePassed2, time);
                fighter._LastLongTouch = time;
            }
            fighter._LastTouch = time;
            BaconFighter.ReturnToCarrierIfOutOfAmmo(fighter);
        }

        public static void CheckOutOfAmmo(Fighter fighter, int i)
        {
            if (i != 0 || fighter.BaconFighterOutOfAmmo)
                return;
            float num1 = 1f;
            Empire empire = fighter.Empire;
            List<Tuple<FighterSpecification, float, float, short>> tupleList = new List<Tuple<FighterSpecification, float, float, short>>();
            if (empire != null && BaconBuiltObject.myMain != null && empire == BaconBuiltObject.myMain._Game.PlayerEmpire)
            {
                List<Tuple<FighterSpecification, float, float, short>> customFighterDesigns = BaconBuiltObject.GetCustomFighterDesigns(empire);
                if (customFighterDesigns != null)
                {
                    Tuple<FighterSpecification, float, float, short> tuple = customFighterDesigns.FirstOrDefault<Tuple<FighterSpecification, float, float, short>>((Func<Tuple<FighterSpecification, float, float, short>, bool>)(x => x.Item1.Name == fighter.Name));
                    if (tuple != null)
                        num1 = tuple.Item3;
                }
            }
            double num2 = Galaxy.Rnd.NextDouble();
            if (fighter.Weapons.Count<FighterWeapon>((Func<FighterWeapon, bool>)(x => x.Type == ComponentType.WeaponMissile)) > 0)
            {
                if (num2 * (double)num1 >= (double)BaconFighter.ammoExhaustChanceMissile)
                    return;
                fighter.Name += "*";
                fighter.BaconFighterOutOfAmmo = true;
            }
            else
            {
                if (fighter.Weapons.Count<FighterWeapon>((Func<FighterWeapon, bool>)(x => x.Type == ComponentType.WeaponTorpedo)) <= 0 || num2 * (double)num1 >= (double)BaconFighter.ammoExhaustChanceTorpedo)
                    return;
                fighter.Name += "*";
                fighter.BaconFighterOutOfAmmo = true;
            }
        }

        public static void ReturnToCarrierIfOutOfAmmo(Fighter fighter)
        {
            if (!fighter.BaconFighterOutOfAmmo || (double)fighter.Weapons[0].DistanceTravelled > 0.0)
                return;
            fighter.Name = fighter.Name.Replace("*", "");
            fighter.Name = fighter.Name.Replace("!", "");
            fighter.BaconAutomationEnabled = false;
            fighter.BaconFighterOutOfAmmo = false;
            fighter.ReturnToCarrier();
        }

        public static void AddWeaponToFighter(Fighter fighter, string weaponName)
        {
            FighterWeapon fighterWeapon = new FighterWeapon();
            switch (weaponName)
            {
                case "defensiveGun":
                    fighterWeapon.Category = ComponentCategoryType.WeaponBeam;
                    fighterWeapon.DamageLoss = (short)1;
                    fighterWeapon.EnergyRequired = (short)0;
                    fighterWeapon.FireRate = (short)700;
                    fighterWeapon.Power = 0.0f;
                    fighterWeapon.Range = (short)100;
                    fighterWeapon.RawDamage = (short)2;
                    fighterWeapon.Speed = (short)700;
                    fighterWeapon.SpecialImageIndex = (byte)0;
                    fighterWeapon.Type = ComponentType.WeaponBeam;
                    fighter.Weapons.Add(fighterWeapon);
                    break;
            }
        }

        public static FighterSpecification CloneFighterSpecification(
          FighterSpecification fighterSpecification)
        {
            return new FighterSpecification()
            {
                AccelerationRate = fighterSpecification.AccelerationRate,
                CountermeasureModifier = fighterSpecification.CountermeasureModifier,
                DamageRepairRate = fighterSpecification.DamageRepairRate,
                EnergyCapacity = fighterSpecification.EnergyCapacity,
                EnergyRechargeRate = fighterSpecification.EnergyRechargeRate,
                EngineExhaustImageIndex = fighterSpecification.EngineExhaustImageIndex,
                FighterSpecificationId = fighterSpecification.FighterSpecificationId,
                Name = fighterSpecification.Name,
                ShieldRechargeRate = fighterSpecification.ShieldRechargeRate,
                ShieldsCapacity = fighterSpecification.ShieldsCapacity,
                Size = fighterSpecification.Size,
                SortTag = fighterSpecification.SortTag,
                TargettingModifier = fighterSpecification.TargettingModifier,
                TechLevel = fighterSpecification.TechLevel,
                TopSpeed = fighterSpecification.TopSpeed,
                TopSpeedEnergyConsumptionRate = fighterSpecification.TopSpeedEnergyConsumptionRate,
                TurnRate = fighterSpecification.TurnRate,
                Type = fighterSpecification.Type,
                WeaponDamage = fighterSpecification.WeaponDamage,
                WeaponDamageLoss = fighterSpecification.WeaponDamageLoss,
                WeaponEnergyRequired = fighterSpecification.WeaponEnergyRequired,
                WeaponFireRate = fighterSpecification.WeaponFireRate,
                WeaponImageIndex = fighterSpecification.WeaponImageIndex,
                WeaponRange = fighterSpecification.WeaponRange,
                WeaponSoundEffectFilename = fighterSpecification.WeaponSoundEffectFilename,
                WeaponSpeed = fighterSpecification.WeaponSpeed,
                WeaponType = fighterSpecification.WeaponType
            };
        }

        public static void EvaluateThreats(Fighter theFighter, Galaxy galaxy)
        {
            if (!theFighter.HasBeenDestroyed && (double)theFighter.Health <= 0.0 && !theFighter.UnderConstruction && BaconBuiltObject.myMain != null)
            {
                theFighter.ParentBuiltObject.InflictDamage((StellarObject)theFighter, (Weapon)null, 1000.0, BaconBuiltObject.myMain._Game.Galaxy.CurrentDateTime, galaxy, 0.0f, false, 0.0, false);
                foreach (StellarObject pursuer in (SyncList<StellarObject>)theFighter.Pursuers)
                {
                    if (pursuer.CurrentTarget == theFighter && pursuer is Fighter)
                    {
                        foreach (FighterWeapon weapon in (SyncList<FighterWeapon>)(pursuer as Fighter).Weapons)
                            weapon.ResetNext = true;
                    }
                }
            }
            if ((theFighter.OnboardCarrier || theFighter.HasBeenDestroyed || theFighter.MissionType == FighterMissionType.ReturnToCarrier) && (!theFighter.OnboardCarrier || theFighter.HasBeenDestroyed || theFighter.ParentBuiltObject.BaconCarrierEnabled))
                return;
            if ((theFighter.MissionType == FighterMissionType.Patrol || theFighter.MissionType == FighterMissionType.ReturnToCarrier) && theFighter.BaconAutomationEnabled)
            {
                theFighter.Name = theFighter.Name.Replace("!", "");
                theFighter.BaconAutomationEnabled = false;
            }
            if (theFighter.ParentBuiltObject.BaconCarrierEnabled || theFighter.BaconAutomationEnabled)
                return;
            double maximumTargetRange = BaconFighter.CalculateMaximumTargetRange(theFighter);
            BuiltObject parentBuiltObject = theFighter.ParentBuiltObject;
            if (parentBuiltObject != null && !parentBuiltObject.HasBeenDestroyed)
            {
                if (theFighter.CurrentTarget != null && theFighter.CurrentTarget is BuiltObject && theFighter.CurrentTarget.Empire != null && theFighter.CurrentTarget.Empire == theFighter.Empire)
                    theFighter.CurrentTarget = (StellarObject)null;
                if (theFighter.CurrentTarget != null && theFighter.MissionType == FighterMissionType.Patrol)
                {
                    BaconFighter.AssignCAP(theFighter);
                    theFighter.MissionType = FighterMissionType.Attack;
                }
                double xpos = parentBuiltObject.Xpos;
                double ypos = parentBuiltObject.Ypos;
                if (parentBuiltObject.Threats != null && parentBuiltObject.Threats.Length != 0)
                {
                    if (theFighter.CurrentTarget == null)
                    {
                        bool flag = false;
                        if (theFighter.Specification.Type == FighterType.Interceptor)
                        {
                            for (int index1 = 0; index1 < parentBuiltObject.Threats.Length; ++index1)
                            {
                                StellarObject threat = parentBuiltObject.Threats[index1];
                                if (threat != null && !threat.HasBeenDestroyed && threat is BuiltObject)
                                {
                                    BuiltObject builtObject = (BuiltObject)threat;
                                    if (builtObject.Fighters != null && builtObject.Fighters.Count > 0 && BaconFighter.ShouldAttack(theFighter, threat, xpos, ypos, maximumTargetRange, galaxy))
                                    {
                                        for (int index2 = 0; index2 < builtObject.Fighters.Count; ++index2)
                                        {
                                            Fighter fighter = builtObject.Fighters[index2];
                                            if (fighter != null && !fighter.OnboardCarrier && fighter.Pursuers.Count < 2 && !fighter.HasBeenDestroyed)
                                            {
                                                flag = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        for (int index3 = 0; index3 < parentBuiltObject.Threats.Length; ++index3)
                        {
                            StellarObject threat = parentBuiltObject.Threats[index3];
                            if (threat != null && !threat.HasBeenDestroyed && (theFighter.Specification.Type == FighterType.Interceptor || !(threat is Fighter)) && BaconFighter.ShouldAttack(theFighter, threat, xpos, ypos, maximumTargetRange, galaxy))
                            {
                                if (theFighter.Specification.Type == FighterType.Interceptor & flag)
                                {
                                    if (threat is BuiltObject)
                                    {
                                        BuiltObject builtObject = (BuiltObject)threat;
                                        if (builtObject.Fighters != null && builtObject.Fighters.Count > 0)
                                        {
                                            for (int index4 = 0; index4 < builtObject.Fighters.Count; ++index4)
                                            {
                                                Fighter fighter = builtObject.Fighters[index4];
                                                if (fighter != null && !fighter.OnboardCarrier && fighter.Pursuers.Count < 2 && !fighter.HasBeenDestroyed)
                                                {
                                                    theFighter.AssignAttackTarget((StellarObject)fighter);
                                                    BaconFighter.AssignCAP(theFighter);
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (theFighter.Specification.Type == FighterType.Interceptor && theFighter.ParentBuiltObject.BaconCarrierEnabled)
                                        break;
                                    if (parentBuiltObject.CurrentTarget == threat)
                                    {
                                        theFighter.AssignAttackTarget(threat);
                                        break;
                                    }
                                    if (!BaconFighter.EvaluateAdequateAttackers(theFighter, galaxy, threat))
                                    {
                                        theFighter.AssignAttackTarget(threat);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        bool flag = false;
                        if (theFighter.CurrentTarget is BuiltObject)
                        {
                            BuiltObject currentTarget = (BuiltObject)theFighter.CurrentTarget;
                            if ((double)currentTarget.CurrentShields <= (double)((float)currentTarget.ShieldsCapacity * 0.5f))
                                flag = true;
                        }
                        else if (theFighter.CurrentTarget is Fighter)
                        {
                            Fighter currentTarget = (Fighter)theFighter.CurrentTarget;
                            if ((double)currentTarget.CurrentShields <= (double)((float)currentTarget.Specification.ShieldsCapacity * 0.5f))
                                flag = true;
                        }
                        else if (theFighter.CurrentTarget is Creature)
                        {
                            Creature currentTarget = (Creature)theFighter.CurrentTarget;
                            if (currentTarget.Damage >= (double)currentTarget.DamageKillThreshhold * 0.5)
                                flag = true;
                        }
                        if (!flag)
                        {
                            int threatLevel1 = galaxy.DetermineThreatLevel(theFighter.CurrentTarget, (object)theFighter.ParentBuiltObject);
                            double num = 4.0;
                            if (theFighter.CurrentTarget.TopSpeed <= (short)0)
                                num = 6.0;
                            else if (theFighter.CurrentTarget is Fighter)
                                num = 10.0;
                            for (int index5 = 0; index5 < parentBuiltObject.Threats.Length; ++index5)
                            {
                                StellarObject threat = parentBuiltObject.Threats[index5];
                                int threatLevel2 = parentBuiltObject.ThreatLevels[index5];
                                if (threat != null && !threat.HasBeenDestroyed && (theFighter.Specification.Type == FighterType.Interceptor || !(threat is Fighter)) && (double)threatLevel1 * num < (double)threatLevel2 && theFighter.CurrentTarget != threat && BaconFighter.ShouldAttack(theFighter, threat, xpos, ypos, maximumTargetRange, galaxy))
                                {
                                    if (theFighter.Specification.Type == FighterType.Interceptor && threat is BuiltObject)
                                    {
                                        BuiltObject builtObject = (BuiltObject)threat;
                                        if (builtObject.Fighters != null && builtObject.Fighters.Count > 0)
                                        {
                                            for (int index6 = 0; index6 < builtObject.Fighters.Count; ++index6)
                                            {
                                                Fighter fighter = builtObject.Fighters[index6];
                                                if (fighter != null && !fighter.OnboardCarrier && fighter.Pursuers.Count < 2 && !fighter.HasBeenDestroyed)
                                                {
                                                    theFighter.AssignAttackTarget((StellarObject)fighter);
                                                    BaconFighter.AssignCAP(theFighter);
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    if (!BaconFighter.EvaluateAdequateAttackers(theFighter, galaxy, threat))
                                    {
                                        theFighter.AssignAttackTarget(threat);
                                        BaconFighter.AssignCAP(theFighter);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (parentBuiltObject != null && parentBuiltObject.CurrentTarget != null && theFighter.CurrentTarget == null)
                {
                    theFighter.AssignAttackTarget(parentBuiltObject.CurrentTarget);
                    BaconFighter.AssignCAP(theFighter);
                }
            }
        }

        public static void PursueTarget(Fighter fighter, Galaxy galaxy, DateTime time)
        {
            StellarObject currentTarget = fighter.CurrentTarget;
            if (currentTarget == null)
            {
                fighter.MissionType = FighterMissionType.Undefined;
                fighter.EvaluateThreats(galaxy);
                if (fighter.MissionType != FighterMissionType.Undefined)
                    return;
                fighter.MissionType = FighterMissionType.Patrol;
            }
            else if (currentTarget.HasBeenDestroyed || currentTarget.Empire == fighter.Empire)
            {
                fighter.MissionType = FighterMissionType.Undefined;
                fighter.EvaluateThreats(galaxy);
                if (fighter.MissionType != FighterMissionType.Undefined)
                    return;
                fighter.MissionType = FighterMissionType.Patrol;
            }
            else
            {
                double num1 = 0.35;
                if (fighter.InView)
                    num1 = 1.4;
                fighter.TargetSpeed = (float)fighter.TopSpeed;
                if (currentTarget is BuiltObject || currentTarget is Creature)
                {
                    double num2 = 0.0;
                    double num3 = 0.0;
                    double num4 = 0.0;
                    switch (currentTarget)
                    {
                        case BuiltObject _:
                            BuiltObject builtObject = (BuiltObject)currentTarget;
                            num2 = builtObject.Xpos;
                            num3 = builtObject.Ypos;
                            num4 = (double)builtObject.Heading;
                            break;
                        case Creature _:
                            Creature creature = (Creature)currentTarget;
                            num2 = creature.Xpos;
                            num3 = creature.Ypos;
                            num4 = (double)creature.CurrentHeading;
                            break;
                    }
                    int weaponRange = (int)fighter.Specification.WeaponRange;
                    if (weaponRange > 0)
                    {
                        double distance = galaxy.CalculateDistance(fighter.Xpos, fighter.Ypos, num2, num3);
                        double angle = Galaxy.DetermineAngle(fighter.Xpos, fighter.Ypos, num2, num3);
                        if (distance < 100.0)
                        {
                            double num5 = (double)fighter.TargetHeading - angle;
                            if (num5 < 0.6 && num5 > -0.6)
                            {
                                if (num5 < 0.0)
                                    fighter.TargetHeading -= 0.7f + fighter.RandomHeadingOffset(galaxy);
                                else
                                    fighter.TargetHeading += 0.7f + fighter.RandomHeadingOffset(galaxy);
                            }
                            fighter.TargetSpeed = (float)fighter.TopSpeed;
                        }
                        else if (distance < (double)weaponRange)
                        {
                            if ((double)currentTarget.CurrentSpeed >= 12.0)
                            {
                                double num6 = (double)weaponRange;
                                if (Math.Abs(num4 - angle) < 1.57)
                                {
                                    double weaponSpeed = (double)fighter.Specification.WeaponSpeed;
                                    double num7 = (double)weaponRange / weaponSpeed;
                                    num6 = (weaponSpeed - (double)currentTarget.CurrentSpeed) * num7 * 0.9;
                                }
                                if (distance < num6)
                                {
                                    fighter.TargetHeading = (float)angle;
                                    fighter.TargetSpeed = currentTarget.CurrentSpeed + 2f;
                                    fighter.TargetSpeed = Math.Min(fighter.TargetSpeed, (float)fighter.TopSpeed);
                                }
                                else
                                    fighter.TargetHeading = (float)angle;
                            }
                            else if (Math.Abs((double)fighter.Heading - angle) > 1.57)
                            {
                                fighter.TargetSpeed = (float)fighter.TopSpeed;
                            }
                            else
                            {
                                double num8 = (double)weaponRange;
                                if (Math.Abs(num4 - angle) < 1.57)
                                {
                                    double weaponSpeed = (double)fighter.Specification.WeaponSpeed;
                                    double num9 = (double)weaponRange / weaponSpeed;
                                    num8 = (weaponSpeed - (double)currentTarget.CurrentSpeed) * num9 * 0.9;
                                }
                                if (distance < num8)
                                {
                                    fighter.TargetSpeed = (float)fighter.TopSpeed * 0.5f;
                                    fighter.TargetSpeed = Math.Max(fighter.TargetSpeed, currentTarget.CurrentSpeed + 2f);
                                    fighter.TargetSpeed = Math.Min(fighter.TargetSpeed, (float)fighter.TopSpeed);
                                }
                            }
                            if (fighter.CheckShouldFireAtCaptureTarget(currentTarget) && Math.Abs((double)fighter.Heading - angle) < num1)
                                BaconFighter.FireWeaponsAtTarget(fighter, galaxy, currentTarget, distance, time);
                        }
                        else
                        {
                            if (distance > 15000.0)
                            {
                                fighter.AbandonAttackTarget();
                                fighter.EvaluateThreats(galaxy);
                                if (fighter.MissionType != FighterMissionType.Undefined)
                                    return;
                                fighter.MissionType = FighterMissionType.Patrol;
                                return;
                            }
                            fighter.TargetHeading = (float)angle;
                            if ((double)currentTarget.CurrentSpeed > (double)fighter.TopSpeed && Math.Abs(num4 - angle) < num1)
                            {
                                fighter.AbandonAttackTarget();
                                fighter.EvaluateThreats(galaxy);
                                if (fighter.MissionType == FighterMissionType.Undefined)
                                    fighter.MissionType = FighterMissionType.Patrol;
                            }
                        }
                        while (!fighter.WillMeetDestination(galaxy, num2, num3, (double)fighter.TargetSpeed))
                        {
                            fighter.TargetSpeed /= 1.5f;
                            if ((double)fighter.TargetSpeed < 1.0)
                                break;
                        }
                    }
                    else
                        fighter.ReturnToCarrier();
                }
                else if (currentTarget is Fighter)
                {
                    Fighter target = (Fighter)currentTarget;
                    int weaponRange = (int)fighter.Specification.WeaponRange;
                    if (target.OnboardCarrier)
                    {
                        fighter.AbandonAttackTarget();
                        fighter.EvaluateThreats(galaxy);
                        if (fighter.MissionType == FighterMissionType.Undefined)
                            fighter.MissionType = FighterMissionType.Patrol;
                    }
                    else if (weaponRange > 0)
                    {
                        double distance = galaxy.CalculateDistance(fighter.Xpos, fighter.Ypos, target.Xpos, target.Ypos);
                        double angle = Galaxy.DetermineAngle(fighter.Xpos, fighter.Ypos, target.Xpos, target.Ypos);
                        if (distance < 40.0)
                        {
                            fighter.TargetSpeed = (double)target.CurrentSpeed > (double)fighter.TopSpeed ? (float)fighter.TopSpeed : target.CurrentSpeed;
                            if (Math.Abs((double)fighter.Heading - angle) < num1)
                                BaconFighter.FireWeaponsAtTarget(fighter, galaxy, (StellarObject)target, distance, time);
                            fighter.TargetHeading = (float)angle;
                        }
                        else if (distance < (double)weaponRange)
                        {
                            if (Math.Abs((double)fighter.Heading - angle) < num1)
                                BaconFighter.FireWeaponsAtTarget(fighter, galaxy, (StellarObject)target, distance, time);
                            fighter.TargetHeading = (float)angle;
                            fighter.TargetSpeed = (float)fighter.TopSpeed;
                        }
                        else
                        {
                            if (distance > 15000.0)
                            {
                                fighter.AbandonAttackTarget();
                                fighter.EvaluateThreats(galaxy);
                                if (fighter.MissionType != FighterMissionType.Undefined)
                                    return;
                                fighter.MissionType = FighterMissionType.Patrol;
                                return;
                            }
                            fighter.TargetHeading = (float)angle;
                            fighter.TargetSpeed = (float)fighter.TopSpeed;
                            if ((double)target.CurrentSpeed > (double)fighter.TopSpeed && Math.Abs((double)target.Heading - angle) < num1)
                            {
                                fighter.AbandonAttackTarget();
                                fighter.EvaluateThreats(galaxy);
                                if (fighter.MissionType == FighterMissionType.Undefined)
                                    fighter.MissionType = FighterMissionType.Patrol;
                            }
                        }
                        while (!fighter.WillMeetDestination(galaxy, target.Xpos, target.Ypos, (double)fighter.TargetSpeed))
                        {
                            fighter.TargetSpeed /= 1.5f;
                            if ((double)fighter.TargetSpeed < 1.0)
                                break;
                        }
                    }
                    else
                        fighter.ReturnToCarrier();
                }
            }
        }

        public static void CheckBomberDefensiveFire(
          Fighter defendingBomber,
          Galaxy galaxy,
          DateTime time)
        {
            if (defendingBomber.HasBeenDestroyed || defendingBomber.OnboardCarrier || defendingBomber.UnderConstruction || defendingBomber.Weapons.Count < 2 || time < defendingBomber.Weapons[1].LastFired.AddSeconds((double)defendingBomber.Weapons[1].FireRate / 1000.0))
                return;
            List<Fighter> source = new List<Fighter>();
            if (defendingBomber.ParentBuiltObject != null && !defendingBomber.ParentBuiltObject.HasBeenDestroyed)
            {
                List<Fighter> fighterList = new List<Fighter>();
                foreach (Fighter fighter in defendingBomber.ParentBuiltObject.Fighters.Where<Fighter>((Func<Fighter, bool>)(x => !x.UnderConstruction && !x.OnboardCarrier && x.Specification.Type == FighterType.Bomber)).ToList<Fighter>())
                {
                    if (fighter.Attackers != null && fighter.Attackers.Count > 0)
                    {
                        foreach (StellarObject attacker in (SyncList<StellarObject>)fighter.Attackers)
                        {
                            if (attacker is Fighter && (attacker as Fighter).Specification.Type == FighterType.Interceptor)
                            {
                                double range = (double)defendingBomber.Weapons[1].Range;
                                double num = range * range;
                                if (galaxy.CalculateDistanceSquared(attacker.Xpos, attacker.Ypos, defendingBomber.Xpos, defendingBomber.Ypos) <= num && !source.Contains(attacker as Fighter))
                                    source.Add(attacker as Fighter);
                            }
                        }
                    }
                }
            }
            if (!source.Any<Fighter>())
                return;
            Fighter fighter1 = source[Galaxy.Rnd.Next(0, source.Count)];
            for (int index = 1; index < defendingBomber.Weapons.Count; ++index)
            {
                double hitRangeChance = 0.0;
                bool hitTarget = defendingBomber.DetermineHitTarget(galaxy, defendingBomber.Weapons[index], (StellarObject)fighter1, (double)defendingBomber.Weapons[index].Range * 0.89999997615814209, out hitRangeChance);
                defendingBomber.Weapons[index].Fire(galaxy, defendingBomber, (StellarObject)fighter1, time, hitTarget, hitRangeChance);
            }
            for (int index = 1; index < defendingBomber.Weapons.Count; ++index)
            {
                float rawDamage = (float)defendingBomber.Weapons[index].RawDamage;
                if (defendingBomber.Empire != null && defendingBomber.Empire.Name.Contains("Romulan"))
                    rawDamage *= 3f;
                if (defendingBomber.Weapons[index].WillHitTarget)
                    defendingBomber.InflictDamage((StellarObject)fighter1, (double)rawDamage, time, galaxy, (float)defendingBomber.Weapons[index].Range * 0.1f, false, (double)defendingBomber.Weapons[index].Heading, false);
                defendingBomber.Weapons[0].ResetNext = true;
            }
        }

        public static void FireWeaponsAtTarget(
          Fighter firingFighter,
          Galaxy galaxy,
          StellarObject target,
          double distanceToTarget,
          DateTime time)
        {
            if (target == null)
                return;
            bool flag = false;
            for (int index = 0; index < firingFighter.Weapons.Count; ++index)
            {
                FighterWeapon weapon = firingFighter.Weapons[index];
                if (weapon.Category != ComponentCategoryType.WeaponBeam && weapon.Category == ComponentCategoryType.WeaponTorpedo)
                    flag = true;
            }
            for (int index = 0; index < firingFighter.Weapons.Count; ++index)
            {
                FighterWeapon weapon = firingFighter.Weapons[index];
                if ((double)weapon.DistanceTravelled < 0.0 && distanceToTarget <= (double)weapon.Range && (double)firingFighter.CurrentEnergy >= (double)weapon.EnergyRequired)
                {
                    int num;
                    switch (target)
                    {
                        case BuiltObject _:
                            if ((weapon.Category == ComponentCategoryType.WeaponTorpedo || !flag) && time >= weapon.LastFired.AddSeconds((double)weapon.FireRate / 1000.0))
                            {
                                double hitRangeChance = 0.0;
                                bool hitTarget = firingFighter.DetermineHitTarget(galaxy, weapon, target, distanceToTarget, out hitRangeChance);
                                weapon.Fire(galaxy, firingFighter, target, time, hitTarget, hitRangeChance);
                                BaconFighter.CheckOutOfAmmo(firingFighter, index);
                                if (firingFighter.ParentBuiltObject != null)
                                {
                                    BuiltObject targetBuiltObject = (BuiltObject)target;
                                    firingFighter.ParentBuiltObject.ModifyDiplomacyFromAttack(targetBuiltObject);
                                }
                                goto label_21;
                            }
                            else
                                goto label_21;
                        case Creature _:
                            if ((weapon.Category == ComponentCategoryType.WeaponTorpedo || !flag) && time >= weapon.LastFired.AddSeconds((double)weapon.FireRate / 1000.0))
                            {
                                double hitRangeChance = 0.0;
                                bool hitTarget = firingFighter.DetermineHitTarget(galaxy, weapon, target, distanceToTarget, out hitRangeChance);
                                weapon.Fire(galaxy, firingFighter, target, time, hitTarget, hitRangeChance);
                                goto label_21;
                            }
                            else
                                goto label_21;
                        case Fighter _:
                            if (weapon.Category == ComponentCategoryType.WeaponBeam)
                            {
                                num = time >= weapon.LastFired.AddSeconds((double)weapon.FireRate / 1000.0) ? 1 : 0;
                                break;
                            }
                            goto default;
                        default:
                            num = 0;
                            break;
                    }
                    if (num != 0)
                    {
                        double hitRangeChance = 0.0;
                        bool hitTarget = firingFighter.DetermineHitTarget(galaxy, weapon, target, distanceToTarget, out hitRangeChance);
                        weapon.Fire(galaxy, firingFighter, target, time, hitTarget, hitRangeChance);
                    }
                label_21:;
                }
            }
        }

        public static void AssignCAP(Fighter fighter)
        {
            try
            {
                if (fighter.Specification.Type != FighterType.Interceptor || fighter.ParentBuiltObject == null || fighter.ParentBuiltObject.HasBeenDestroyed || !fighter.ParentBuiltObject.Pursuers.Any<StellarObject>())
                    return;
                List<StellarObject> list = fighter.ParentBuiltObject.Pursuers.Where<StellarObject>((Func<StellarObject, bool>)(x => x is Fighter && (x as Fighter).Specification.Type == FighterType.Bomber)).ToList<StellarObject>();
                if (list == null || list.Count == 0)
                    return;
                Random random = new Random();
                BuiltObject parentBuiltObject = fighter.ParentBuiltObject;
                if (!list.Contains(fighter.CurrentTarget))
                    fighter.AssignAttackTarget(list[random.Next(list.Count)]);
            }
            catch (Exception ex)
            {
                BaconBuiltObject.myMain._Game.Galaxy.Pause();
            }
        }

        public static bool ShouldAttack(
          Fighter fighter,
          StellarObject potentialTarget,
          double carrierX,
          double carrierY,
          double maximumTargetDistanceSquared,
          Galaxy galaxy)
        {
            if (fighter.Empire != null && fighter.MissionType != FighterMissionType.ReturnToCarrier)
            {
                if (potentialTarget is Creature)
                {
                    Creature creature = (Creature)potentialTarget;
                    return creature.IsVisible && galaxy.CalculateDistanceSquared(carrierX, carrierY, creature.Xpos, creature.Ypos) <= maximumTargetDistanceSquared;
                }
                BuiltObject builtObject = (BuiltObject)potentialTarget;
                if (fighter.Empire == builtObject.Empire || builtObject.Empire == null || builtObject.Empire == galaxy.IndependentEmpire || builtObject.PirateEmpireId > (byte)0 && fighter.ParentBuiltObject != null && (int)builtObject.PirateEmpireId == (int)fighter.ParentBuiltObject.PirateEmpireId || builtObject.Empire.PirateEmpireBaseHabitat != null && builtObject.Empire.ObtainPirateRelation(fighter.Empire).Type == PirateRelationType.Protection || fighter.Empire.PirateEmpireBaseHabitat != null && fighter.Empire.ObtainPirateRelation(builtObject.Empire).Type == PirateRelationType.Protection || galaxy.CalculateDistanceSquared(carrierX, carrierY, builtObject.Xpos, builtObject.Ypos) > maximumTargetDistanceSquared)
                    return false;
                if (fighter.Empire.Outlaws.Contains(builtObject))
                {
                    if (fighter.Empire != builtObject.Empire)
                        return true;
                    fighter.Empire.Outlaws.Remove(builtObject);
                    return false;
                }
                if (fighter.Attackers.Contains((StellarObject)builtObject))
                    return true;
                DiplomaticRelation diplomaticRelation = fighter.Empire.DiplomaticRelations[builtObject.Empire];
                if (builtObject.Empire.PirateEmpireBaseHabitat != null || fighter.Empire.PirateEmpireBaseHabitat != null && builtObject.Empire != fighter.Empire)
                    return true;
                if (diplomaticRelation != null)
                {
                    if (diplomaticRelation.Type == DiplomaticRelationType.War)
                        return true;
                    if (fighter.MissionType == FighterMissionType.Attack)
                    {
                        Empire empire = (Empire)null;
                        if (fighter.CurrentTarget != null)
                            empire = fighter.CurrentTarget.Empire;
                        if (potentialTarget.Empire == empire)
                            return true;
                    }
                    return fighter.ParentBuiltObject != null && fighter.ParentBuiltObject.CurrentTarget != null && fighter.ParentBuiltObject.CurrentTarget == potentialTarget;
                }
            }
            return false;
        }

        public static bool EvaluateAdequateAttackers(
          Fighter fighter,
          Galaxy galaxy,
          StellarObject potentialTarget)
        {
            double closestAttackerDistance = 0.0;
            int attackingFirepower = fighter.DetermineAttackingFirepower(galaxy, potentialTarget, out closestAttackerDistance);
            int num1 = potentialTarget.FirepowerRaw;
            switch (potentialTarget)
            {
                case BuiltObject _:
                    num1 = ((BuiltObject)potentialTarget).CalculateOverallStrengthFactor();
                    break;
                case Creature _:
                    Creature creature = (Creature)potentialTarget;
                    num1 = creature.AttackStrength * 5;
                    if (creature.Type == CreatureType.SilverMist)
                        num1 *= 4;
                    break;
            }
            int num2 = fighter.Empire == null ? (int)((double)num1 * Galaxy.AttackOvermatchFactor) + 1 : (int)((double)num1 * (double)fighter.Empire.AttackOvermatchFactor) + 1;
            if (attackingFirepower < num2 || closestAttackerDistance * closestAttackerDistance > Galaxy.StrikeRangeSquared && galaxy.CalculateDistanceSquared(potentialTarget.Xpos, potentialTarget.Ypos, fighter.Xpos, fighter.Ypos) < Galaxy.StrikeRangeSquared)
                return false;
            double num3 = 900000.0;
            if (potentialTarget is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)potentialTarget;
                if (builtObject.Pursuers != null)
                {
                    bool flag = false;
                    foreach (StellarObject pursuer in (SyncList<StellarObject>)builtObject.Pursuers)
                    {
                        if (pursuer is BuiltObject)
                        {
                            if (((BuiltObject)pursuer).Mission.Type == BuiltObjectMissionType.Capture)
                            {
                                flag = true;
                                break;
                            }
                            double num4 = Galaxy.CalculateDistanceSquaredStatic(builtObject.Xpos, builtObject.Ypos, pursuer.Xpos, pursuer.Ypos) / (double)Math.Max(10f, pursuer.CurrentSpeed);
                            if (num4 < num3)
                                num3 = num4;
                        }
                    }
                    double num5 = Galaxy.CalculateDistanceSquaredStatic(builtObject.Xpos, builtObject.Ypos, fighter.Xpos, fighter.Ypos) / (double)Math.Max(10f, fighter.CurrentSpeed);
                    if (!flag && num5 < num3)
                        return false;
                }
            }
            return true;
        }

        public static void DeserializeExtraFields(Fighter fighter, SerializationInfo info)
        {
            try
            {
                fighter.Specification = (FighterSpecification)info.GetValue("spec", typeof(FighterSpecification));
            }
            catch (Exception ex)
            {
            }
        }

        public static void SerializeExtraFields(Fighter fighter, SerializationInfo info)
        {
            try
            {
                info.AddValue("spec", (object)fighter.Specification);
            }
            catch (Exception ex)
            {
            }
        }

        public static void AbandonAttackTarget(Fighter fighter)
        {
            StellarObject currentTarget = fighter.CurrentTarget;
            if (currentTarget != null)
            {
                if (currentTarget.Pursuers != null && currentTarget.Pursuers.Contains((StellarObject)fighter))
                    currentTarget.Pursuers.Remove((StellarObject)fighter);
                if (currentTarget.Attackers != null && currentTarget.Attackers.Contains((StellarObject)fighter))
                    currentTarget.Attackers.Remove((StellarObject)fighter);
                fighter.CurrentTarget = (StellarObject)null;
            }
            fighter.MissionType = FighterMissionType.Undefined;
        }
    }
}
