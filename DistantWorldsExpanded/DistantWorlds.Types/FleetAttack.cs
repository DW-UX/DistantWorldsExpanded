// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.FleetAttack
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class FleetAttack : IComparable<FleetAttack>
  {
    public ShipGroup Fleet;
    public long WarningDate;
    private object _Target;
    [OptionalField]
    public BuiltObject PlanetDestroyer;

    public FleetAttack(ShipGroup fleet, object target, long starDate)
    {
      this.Fleet = fleet;
      this.Target = target;
      this.WarningDate = starDate;
    }

    public FleetAttack(BuiltObject planetDestroyer, object target, long starDate)
    {
      this.PlanetDestroyer = planetDestroyer;
      this.Target = target;
      this.WarningDate = starDate;
    }

    public object Target
    {
      get => this._Target;
      set
      {
        switch (value)
        {
          case BuiltObject _:
            this._Target = value;
            break;
          case Habitat _:
            this._Target = value;
            break;
          case ShipGroup _:
            this._Target = value;
            break;
          case Creature _:
            this._Target = value;
            break;
          default:
            this._Target = (object) null;
            break;
        }
      }
    }

    int IComparable<FleetAttack>.CompareTo(FleetAttack other)
    {
      if (this.PlanetDestroyer != null && other.PlanetDestroyer == null)
        return -1;
      return this.PlanetDestroyer == null && other.PlanetDestroyer != null ? 1 : 0;
    }
  }
}
