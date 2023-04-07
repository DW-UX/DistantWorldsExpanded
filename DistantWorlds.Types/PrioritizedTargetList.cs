// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PrioritizedTargetList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class PrioritizedTargetList : SyncList<PrioritizedTarget>
  {
    public HabitatList ResolveHabitats()
    {
      HabitatList habitatList = new HabitatList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Target is Habitat)
          habitatList.Add((Habitat) this[index].Target);
      }
      return habitatList;
    }

    public StellarObjectList ResolveStellarObjects()
    {
      StellarObjectList stellarObjectList = new StellarObjectList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Target is Habitat)
          stellarObjectList.Add((StellarObject) this[index].Target);
        else if (this[index].Target is BuiltObject)
          stellarObjectList.Add((StellarObject) this[index].Target);
        else if (this[index].Target is Creature)
          stellarObjectList.Add((StellarObject) this[index].Target);
      }
      return stellarObjectList;
    }

    public bool CheckEquivalent(PrioritizedTargetList otherTargets)
    {
      if (otherTargets == null || otherTargets.Count != this.Count)
        return false;
      for (int index = 0; index < this.Count; ++index)
      {
        if (otherTargets.Count <= index || this[index].Target != otherTargets[index].Target)
          return false;
      }
      return true;
    }

    public new void Add(PrioritizedTarget prioritizedTarget)
    {
      lock (this._LockObject)
      {
        foreach (PrioritizedTarget prioritizedTarget1 in (SyncList<PrioritizedTarget>) this)
        {
          if (prioritizedTarget1.Target == prioritizedTarget.Target)
            return;
        }
        base.Add(prioritizedTarget);
      }
    }

    private double CalculateDistance(double x1, double y1, double x2, double y2)
    {
      double num1 = x1 - x2;
      double num2 = y1 - y2;
      return Math.Sqrt(num2 * num2 + num1 * num1);
    }

    public PrioritizedTarget IdentifyBestTargetFromLocation(
      Empire empire,
      double x,
      double y,
      int troopStrengthAvailable,
      int firePowerAvailable,
      PrioritizedTargetList targetsToExclude)
    {
      int sizeX = Galaxy.SizeX;
      PrioritizedTarget prioritizedTarget1 = (PrioritizedTarget) null;
      double num1 = 0.0;
      if (targetsToExclude == null)
        targetsToExclude = new PrioritizedTargetList();
      int num2 = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        PrioritizedTarget prioritizedTarget2 = this[index];
        if (!targetsToExclude.Contains(prioritizedTarget2))
        {
          double x2 = -1.0;
          double y2 = -1.0;
          if (prioritizedTarget2.Target is BuiltObject)
          {
            BuiltObject target = (BuiltObject) prioritizedTarget2.Target;
            x2 = target.Xpos;
            y2 = target.Ypos;
            num2 = 0;
          }
          else if (prioritizedTarget2.Target is Habitat)
          {
            Habitat target = (Habitat) prioritizedTarget2.Target;
            x2 = target.Xpos;
            y2 = target.Ypos;
            num2 = Galaxy.DetermineRequiredTroopStrength(empire, (object) target);
          }
          else if (prioritizedTarget2.Target is ShipGroup)
          {
            ShipGroup target = (ShipGroup) prioritizedTarget2.Target;
            if (target.LeadShip != null)
            {
              x2 = target.LeadShip.Xpos;
              y2 = target.LeadShip.Ypos;
            }
            num2 = 0;
          }
          if (x2 >= 0.0 && y2 >= 0.0 && troopStrengthAvailable >= num2 && firePowerAvailable >= (int) ((double) prioritizedTarget2.LocationStrength * 0.75))
          {
            double distanceFactor = Galaxy.CalculateDistanceFactor(this.CalculateDistance(x, y, x2, y2));
            double num3 = (double) prioritizedTarget2.Priority / distanceFactor;
            if (num3 > num1)
            {
              num1 = num3;
              prioritizedTarget1 = prioritizedTarget2;
            }
          }
        }
      }
      return prioritizedTarget1;
    }
  }
}
