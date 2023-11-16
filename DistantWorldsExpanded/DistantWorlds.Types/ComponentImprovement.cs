// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ComponentImprovement
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ComponentImprovement
  {
    public int TechLevel;
    public Component ImprovedComponent;
    public int Value1;
    public int Value2;
    public int Value3;
    public int Value4;
    public int Value5;
    public int Value6;
    public int Value7;

    public ComponentImprovement(Component improvedComponent)
    {
      this.ImprovedComponent = improvedComponent;
      this.TechLevel = improvedComponent.TechLevel;
      this.Value1 = improvedComponent.Value1;
      this.Value2 = improvedComponent.Value2;
      this.Value3 = improvedComponent.Value3;
      this.Value4 = improvedComponent.Value4;
      this.Value5 = improvedComponent.Value5;
      this.Value6 = improvedComponent.Value6;
      this.Value7 = improvedComponent.Value7;
    }

    public ComponentImprovement(
      Component improvedComponent,
      int techLevel,
      int value1,
      int value2,
      int value3,
      int value4,
      int value5,
      int value6,
      int value7)
    {
      this.ImprovedComponent = improvedComponent;
      this.TechLevel = techLevel;
      this.Value1 = value1;
      this.Value2 = value2;
      this.Value3 = value3;
      this.Value4 = value4;
      this.Value5 = value5;
      this.Value6 = value6;
      this.Value7 = value7;
    }

    public bool IsPlanetDestroyer
    {
      get
      {
        if (this.ImprovedComponent != null)
        {
          switch (this.ImprovedComponent.Type)
          {
            case ComponentType.WeaponSuperBeam:
            case ComponentType.WeaponSuperTorpedo:
            case ComponentType.WeaponSuperMissile:
            case ComponentType.WeaponSuperPhaser:
            case ComponentType.WeaponSuperRailGun:
              if (this.Value1 >= 10000)
                return true;
              break;
          }
        }
        return false;
      }
    }
  }
}
