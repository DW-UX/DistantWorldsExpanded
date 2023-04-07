// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.FighterWeapon
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class FighterWeapon
  {
    public float Power;
    public float HeadingMissFactor;
    public DateTime LastFired;
    public float DistanceTravelled = -1f;
    public float DistanceFromTarget = 2E+09f;
    public bool WillHitTarget;
    public float X = -1f;
    public float Y = -1f;
    public float Heading;
    public bool HasMissed;
    public bool SoundEffectPlayed;
    public bool ResetNext;
    public ComponentCategoryType Category = ComponentCategoryType.WeaponBeam;
    public ComponentType Type = ComponentType.WeaponBeam;
    public byte SpecialImageIndex;
    public short RawDamage;
    public short Range;
    public short EnergyRequired;
    public short Speed;
    public short DamageLoss;
    public short FireRate;

    public void Fire(
      Galaxy galaxy,
      Fighter firer,
      StellarObject target,
      DateTime time,
      bool willHit,
      double hitRangeChance)
    {
      this.LastFired = time;
      this.DistanceTravelled = 1f;
      this.X = (float) firer.Xpos;
      this.Y = (float) firer.Ypos;
      this.HasMissed = false;
      this.SoundEffectPlayed = false;
      this.ResetNext = false;
      this.DistanceFromTarget = (float) galaxy.CalculateDistance(firer.Xpos, firer.Ypos, target.Xpos, target.Ypos);
      this.WillHitTarget = willHit;
      if (willHit)
      {
        this.HeadingMissFactor = 0.0f;
        this.Heading = (float) Galaxy.DetermineAngle(firer.Xpos, firer.Ypos, target.Xpos, target.Ypos);
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
        this.HeadingMissFactor = (float) num;
        this.Heading = (float) num + (float) Galaxy.DetermineAngle(firer.Xpos, firer.Ypos, target.Xpos, target.Ypos);
      }
      firer.CurrentEnergy -= (float) this.EnergyRequired;
      if (target.Attackers.Contains((StellarObject) firer))
        return;
      target.Attackers.Add((StellarObject) firer);
    }

    public void Reset()
    {
      this.DistanceTravelled = -1f;
      this.X = -2.00000013E+09f;
      this.Y = -2.00000013E+09f;
      this.Heading = 0.0f;
      this.HeadingMissFactor = 0.0f;
      this.DistanceFromTarget = 2E+09f;
      this.WillHitTarget = false;
      this.HasMissed = false;
      this.Power = 0.0f;
      this.SoundEffectPlayed = false;
      this.ResetNext = false;
    }
  }
}
