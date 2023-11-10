// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PrioritizedTarget
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class PrioritizedTarget : IComparable<PrioritizedTarget>
  {
    private int _Priority;
    private int _LocationStrength;
    private int _WeightedPriority;
    private Habitat _Habitat;
    private BuiltObject _BuiltObject;
    private ShipGroup _ShipGroup;
    private double _DistanceFromAttackingEmpire = 100000.0;

    public PrioritizedTarget(Habitat habitat, int priority)
    {
      this._Habitat = habitat;
      this._Priority = priority;
      this._LocationStrength = 0;
      this._WeightedPriority = this._Priority;
    }

    public PrioritizedTarget(BuiltObject builtObject, int priority)
    {
      this._BuiltObject = builtObject;
      this._Priority = priority;
      this._LocationStrength = 0;
      this._WeightedPriority = this._Priority;
    }

    public PrioritizedTarget(ShipGroup shipGroup, int priority)
    {
      this._ShipGroup = shipGroup;
      this._Priority = priority;
      this._LocationStrength = 0;
      this._WeightedPriority = this._Priority;
    }

    public void ResolveTargetCoordinates(out double x, out double y)
    {
      x = 0.0;
      y = 0.0;
      if (this._Habitat != null)
      {
        x = this._Habitat.Xpos;
        y = this._Habitat.Ypos;
      }
      else if (this._BuiltObject != null)
      {
        x = this._BuiltObject.Xpos;
        y = this._BuiltObject.Ypos;
      }
      else
      {
        if (this._ShipGroup == null || this._ShipGroup.LeadShip == null)
          return;
        x = this._ShipGroup.LeadShip.Xpos;
        y = this._ShipGroup.LeadShip.Ypos;
      }
    }

    public object Target
    {
      get
      {
        if (this._Habitat != null)
          return (object) this._Habitat;
        return this._ShipGroup != null ? (object) this._ShipGroup : (object) this._BuiltObject;
      }
    }

    public string Name
    {
      get
      {
        if (this._Habitat != null)
          return this._Habitat.Name;
        return this._ShipGroup != null ? this._ShipGroup.Name : this._BuiltObject.Name;
      }
    }

    public Empire Empire
    {
      get
      {
        if (this._Habitat != null)
          return this._Habitat.Owner;
        return this._ShipGroup != null ? this._ShipGroup.Empire : this._BuiltObject.Empire;
      }
    }

    public int Priority
    {
      get => this._Priority;
      set
      {
        this._Priority = value;
        if (this._LocationStrength > 0)
          this._WeightedPriority = this._Priority / this._LocationStrength;
        else
          this._WeightedPriority = this._Priority;
      }
    }

    private void CalculateWeightedPriority() => this._WeightedPriority = Math.Max(1, (int) ((double) this._Priority / Galaxy.CalculateDistanceFactor(this._DistanceFromAttackingEmpire)));

    public double DistanceFromAttackingEmpire
    {
      get => this._DistanceFromAttackingEmpire;
      set
      {
        this._DistanceFromAttackingEmpire = value;
        this.CalculateWeightedPriority();
      }
    }

    public int LocationStrength
    {
      get => this._LocationStrength;
      set
      {
        this._LocationStrength = value;
        this.CalculateWeightedPriority();
      }
    }

    public int WeightedPriority => this._WeightedPriority;

    public int CompareTo(PrioritizedTarget other) => this.WeightedPriority.CompareTo(other.WeightedPriority);
  }
}
