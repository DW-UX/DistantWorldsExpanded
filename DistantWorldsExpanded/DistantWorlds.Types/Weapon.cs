// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Weapon
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Weapon : ISerializable
  {
    public float Power;
    public float HeadingMissFactor;
    public DateTime LastFired;
    public float DistanceTravelled = -1f;
    public float DistanceFromTarget = 2E+09f;
    public bool WillHitTarget;
    public double X = -1.0;
    public double Y = -1.0;
    public float Heading;
    public bool HasMissed;
    private bool _IsPlanetDestroyer;
    private bool _SoundEffectPlayed;
    private bool _ResetNext;
    private BuiltObjectComponent _BuiltObjectComponent;
    public StellarObject Target;
    public Weapon TargetWeapon;
    [NonSerialized]
    public ComponentImprovement _ImprovedComponent;

    public Weapon()
    {
    }

    public Weapon(SerializationInfo info, StreamingContext context)
      : this()
    {
      using (MemoryStream input = new MemoryStream((byte[]) info.GetValue("D", typeof (byte[]))))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          this.Power = binaryReader.ReadSingle();
          this.HeadingMissFactor = binaryReader.ReadSingle();
          this.LastFired = new DateTime(binaryReader.ReadInt64());
          this.DistanceTravelled = binaryReader.ReadSingle();
          this.DistanceFromTarget = binaryReader.ReadSingle();
          this.WillHitTarget = binaryReader.ReadBoolean();
          this.X = binaryReader.ReadDouble();
          this.Y = binaryReader.ReadDouble();
          this.Heading = binaryReader.ReadSingle();
          this.HasMissed = binaryReader.ReadBoolean();
          this._IsPlanetDestroyer = binaryReader.ReadBoolean();
          this._SoundEffectPlayed = binaryReader.ReadBoolean();
          this._ResetNext = binaryReader.ReadBoolean();
          byte componentID = binaryReader.ReadByte();
          byte num = binaryReader.ReadByte();
          this._BuiltObjectComponent = new BuiltObjectComponent((int) componentID, (ComponentStatus) num);
          this._ImprovedComponent = new ComponentImprovement(new DistantWorlds.Types.Component((int) componentID));
          binaryReader.Close();
        }
      }
      this.Target = (StellarObject) info.GetValue("Tg", typeof (StellarObject));
      this.TargetWeapon = (Weapon) info.GetValue("TgW", typeof (Weapon));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write(this.Power);
          binaryWriter.Write(this.HeadingMissFactor);
          binaryWriter.Write(this.LastFired.Ticks);
          binaryWriter.Write(this.DistanceTravelled);
          binaryWriter.Write(this.DistanceFromTarget);
          binaryWriter.Write(this.WillHitTarget);
          binaryWriter.Write(this.X);
          binaryWriter.Write(this.Y);
          binaryWriter.Write(this.Heading);
          binaryWriter.Write(this.HasMissed);
          binaryWriter.Write(this._IsPlanetDestroyer);
          binaryWriter.Write(this._SoundEffectPlayed);
          binaryWriter.Write(this._ResetNext);
          if (this._BuiltObjectComponent != null)
          {
            binaryWriter.Write((byte) this._BuiltObjectComponent.ComponentID);
            binaryWriter.Write((byte) this._BuiltObjectComponent.Status);
          }
          else
          {
            binaryWriter.Write((byte) 0);
            binaryWriter.Write((byte) 0);
          }
          binaryWriter.Flush();
          binaryWriter.Close();
          byte[] array = output.ToArray();
          info.AddValue("D", (object) array);
        }
      }
      info.AddValue("Tg", (object) this.Target);
      info.AddValue("TgW", (object) this.TargetWeapon);
    }

    public Weapon(BuiltObjectComponent builtObjectComponent)
    {
      if (builtObjectComponent.Category != ComponentCategoryType.WeaponTorpedo && builtObjectComponent.Category != ComponentCategoryType.WeaponBeam && builtObjectComponent.Category != ComponentCategoryType.WeaponSuperArea && builtObjectComponent.Category != ComponentCategoryType.WeaponSuperBeam && builtObjectComponent.Category != ComponentCategoryType.WeaponSuperTorpedo && builtObjectComponent.Category != ComponentCategoryType.WeaponArea && builtObjectComponent.Category != ComponentCategoryType.WeaponPointDefense && builtObjectComponent.Category != ComponentCategoryType.WeaponIon && builtObjectComponent.Category != ComponentCategoryType.WeaponGravity && builtObjectComponent.Category != ComponentCategoryType.AssaultPod)
        throw new ApplicationException("This component is not a weapon.");
      switch (builtObjectComponent.Type)
      {
        case ComponentType.WeaponSuperBeam:
        case ComponentType.WeaponSuperTorpedo:
        case ComponentType.WeaponSuperMissile:
        case ComponentType.WeaponSuperPhaser:
        case ComponentType.WeaponSuperRailGun:
          if (builtObjectComponent.Value1 >= 10000)
          {
            this._IsPlanetDestroyer = true;
            break;
          }
          break;
      }
      this._BuiltObjectComponent = builtObjectComponent;
      this._ImprovedComponent = new ComponentImprovement(new DistantWorlds.Types.Component(this._BuiltObjectComponent.ComponentID));
    }

    public Weapon(ComponentImprovement componentImprovement)
    {
      DistantWorlds.Types.Component improvedComponent = componentImprovement.ImprovedComponent;
      ComponentCategoryType category = improvedComponent.Category;
      ComponentType type = improvedComponent.Type;
      int num = improvedComponent.Value1;
      int componentId = improvedComponent.ComponentID;
      switch (category)
      {
        case ComponentCategoryType.WeaponBeam:
        case ComponentCategoryType.WeaponTorpedo:
        case ComponentCategoryType.WeaponArea:
        case ComponentCategoryType.WeaponPointDefense:
        case ComponentCategoryType.WeaponIon:
        case ComponentCategoryType.WeaponGravity:
        case ComponentCategoryType.AssaultPod:
        case ComponentCategoryType.WeaponSuperBeam:
        case ComponentCategoryType.WeaponSuperArea:
        case ComponentCategoryType.WeaponSuperTorpedo:
          switch (type)
          {
            case ComponentType.WeaponSuperBeam:
            case ComponentType.WeaponSuperTorpedo:
            case ComponentType.WeaponSuperMissile:
            case ComponentType.WeaponSuperPhaser:
            case ComponentType.WeaponSuperRailGun:
              if (num >= 10000)
              {
                this._IsPlanetDestroyer = true;
                break;
              }
              break;
          }
          this._BuiltObjectComponent = new BuiltObjectComponent(componentId, ComponentStatus.Normal);
          this._ImprovedComponent = componentImprovement;
          break;
        default:
          throw new ApplicationException("This component is not a weapon.");
      }
    }

    public void ReviewValues(Empire empire)
    {
      DistantWorlds.Types.Component component = new DistantWorlds.Types.Component(this._BuiltObjectComponent.ComponentID);
      if (empire != null && empire.Research != null)
        this._ImprovedComponent = empire.Research.ResolveImprovedComponentValues(component);
      else
        this._ImprovedComponent = new ComponentImprovement(component);
    }

    public bool IsAvailableWithoutEnergyConsideration(DateTime time) => (double) this.DistanceTravelled < 0.0 && this.LastFired.AddMilliseconds((double) this.FireRate) <= time;

    public bool IsAvailable(BuiltObject firer, DateTime time) => (double) this.DistanceTravelled < 0.0 && firer.CurrentEnergy >= (double) this.EnergyRequired && this.LastFired.AddMilliseconds((double) this.FireRate) <= time;

    public void Fire(
      Galaxy galaxy,
      StellarObject firer,
      StellarObject target,
      double distanceToTarget,
      DateTime time,
      bool willHit,
      double hitRangeChance)
    {
      this.LastFired = time;
      bool flag = false;
      if (this._BuiltObjectComponent != null)
      {
        switch (this._BuiltObjectComponent.Type)
        {
          case ComponentType.WeaponIonPulse:
          case ComponentType.WeaponAreaDestruction:
          case ComponentType.WeaponSuperArea:
            flag = true;
            break;
        }
      }
      this.DistanceTravelled = !flag ? 1f : 0.0f;
      this.X = firer.Xpos;
      this.Y = firer.Ypos;
      this.HasMissed = false;
      this.SoundEffectPlayed = false;
      this.ResetNext = false;
      this.DistanceFromTarget = (float) distanceToTarget;
      this.Target = target;
      this.WillHitTarget = willHit;
      this.FireInternal(galaxy, firer, target.Xpos, target.Ypos, (object) target, distanceToTarget, time, willHit, hitRangeChance);
    }

    public void Fire(
      Galaxy galaxy,
      StellarObject firer,
      Weapon targetWeaponBlast,
      double distanceToTarget,
      DateTime time,
      bool willHit,
      double hitRangeChance)
    {
      this.LastFired = time;
      bool flag = false;
      if (this._BuiltObjectComponent != null)
      {
        switch (this._BuiltObjectComponent.Type)
        {
          case ComponentType.WeaponIonPulse:
          case ComponentType.WeaponAreaDestruction:
          case ComponentType.WeaponSuperArea:
            flag = true;
            break;
        }
      }
      this.DistanceTravelled = !flag ? 1f : 0.0f;
      this.X = firer.Xpos;
      this.Y = firer.Ypos;
      this.HasMissed = false;
      this.SoundEffectPlayed = false;
      this.ResetNext = false;
      this.DistanceFromTarget = (float) distanceToTarget;
      this.TargetWeapon = targetWeaponBlast;
      this.WillHitTarget = willHit;
      this.FireInternal(galaxy, firer, targetWeaponBlast.X, targetWeaponBlast.Y, (object) targetWeaponBlast, distanceToTarget, time, willHit, hitRangeChance);
    }

    private void FireInternal(
      Galaxy galaxy,
      StellarObject firer,
      double targetX,
      double targetY,
      object target,
      double distanceToTarget,
      DateTime time,
      bool willHit,
      double hitRangeChance)
    {
      if (willHit)
      {
        this.Heading = (float) Galaxy.DetermineAngle(firer.Xpos, firer.Ypos, targetX, targetY);
        double num = Galaxy.Rnd.NextDouble() * 0.15;
        if (Galaxy.Rnd.Next(0, 2) == 0)
          num *= -1.0;
        this.Heading += (float) num;
      }
      else
      {
        double num = (0.5 - hitRangeChance) * Galaxy.Rnd.NextDouble() * 0.4;
        if (num < 0.03)
          num += 0.03;
        if (Galaxy.Rnd.Next(0, 2) == 0)
          num *= -1.0;
        if (this.Component != null && (this.Component.Type == ComponentType.WeaponPhaser || this.Component.Type == ComponentType.WeaponSuperPhaser))
          num /= 1.5;
        this.HeadingMissFactor = (float) num;
        this.Heading = (float) num + (float) Galaxy.DetermineAngle(firer.Xpos, firer.Ypos, targetX, targetY);
      }
      if (!(firer is BuiltObject))
        return;
      BuiltObject builtObject1 = (BuiltObject) firer;
      double energyRequired = (double) this.EnergyRequired;
      if (builtObject1.ShipGroup != null)
        energyRequired /= builtObject1.ShipGroup.ShipEnergyUsageBonus;
      double num1 = energyRequired / builtObject1.CaptainShipEnergyUsageBonus;
      builtObject1.CurrentEnergy -= num1;
      switch (target)
      {
        case BuiltObject _:
          BuiltObject builtObject2 = (BuiltObject) target;
          if (builtObject2.Attackers.Contains(firer))
            break;
          builtObject2.Attackers.Add(firer);
          break;
        case Creature _:
          Creature creature = (Creature) target;
          if (creature.Attackers.Contains(firer))
            break;
          creature.Attackers.Add(firer);
          break;
      }
    }

    public void Reset()
    {
      this.DistanceTravelled = -1f;
      this.Target = (StellarObject) null;
      this.TargetWeapon = (Weapon) null;
      this.X = -2000000100.0;
      this.Y = -2000000100.0;
      this.Heading = 0.0f;
      this.HeadingMissFactor = 0.0f;
      this.DistanceFromTarget = 2E+09f;
      this.WillHitTarget = false;
      this.HasMissed = false;
      this.Power = 0.0f;
      this._SoundEffectPlayed = false;
      this._ResetNext = false;
    }

    public bool IsPlanetDestroyer
    {
      get => this._IsPlanetDestroyer;
      set => this._IsPlanetDestroyer = value;
    }

    public BuiltObjectComponent Component => this._BuiltObjectComponent;

    public int RawDamage => this._ImprovedComponent.Value1;

    public int Range => this._ImprovedComponent.Value2;

    public int EnergyRequired => this._ImprovedComponent.Value3;

    public int Speed => this._ImprovedComponent.Value4;

    public int DamageLoss => this._ImprovedComponent.Value5;

    public int FireRate => this._ImprovedComponent.Value6;

    public int BombardDamage => this._ImprovedComponent.Value7;

    public bool SoundEffectPlayed
    {
      get => this._SoundEffectPlayed;
      set => this._SoundEffectPlayed = value;
    }

    public bool ResetNext
    {
      get => this._ResetNext;
      set => this._ResetNext = value;
    }
  }
}
