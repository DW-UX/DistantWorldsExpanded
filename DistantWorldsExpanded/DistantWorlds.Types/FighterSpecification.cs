// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.FighterSpecification
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class FighterSpecification : IComparable<FighterSpecification>
  {
    public int FighterSpecificationId;
    public string Name;
    public FighterType Type;
    public short Size = 10;
    public double TechLevel;
    public short EnergyCapacity;
    public float EnergyRechargeRate;
    public short TopSpeed;
    public float TopSpeedEnergyConsumptionRate;
    public float AccelerationRate;
    public float TurnRate;
    public int EngineExhaustImageIndex;
    public short ShieldsCapacity;
    public float ShieldRechargeRate;
    public short DamageRepairRate;
    public short CountermeasureModifier;
    public short TargettingModifier;
    public ComponentType WeaponType;
    public int WeaponImageIndex;
    public short WeaponDamage;
    public short WeaponRange;
    public short WeaponEnergyRequired;
    public short WeaponSpeed;
    public short WeaponDamageLoss;
    public short WeaponFireRate;
    public string WeaponSoundEffectFilename;
    [NonSerialized]
    public float SortTag;

    int IComparable<FighterSpecification>.CompareTo(FighterSpecification other) => (double) this.SortTag > 0.0 || (double) other.SortTag > 0.0 ? this.SortTag.CompareTo(other.SortTag) : this.FighterSpecificationId.CompareTo(other.FighterSpecificationId);
  }
}
