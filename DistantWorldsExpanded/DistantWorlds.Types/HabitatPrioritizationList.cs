// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.HabitatPrioritizationList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class HabitatPrioritizationList : SyncList<HabitatPrioritization>
  {
    public HabitatList ResolveSystems()
    {
      HabitatList habitatList = new HabitatList();
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(this[index].Habitat);
        if (!habitatList.Contains(habitatSystemStar))
          habitatList.Add(habitatSystemStar);
      }
      return habitatList;
    }

    public HabitatList ResolveHabitats()
    {
      HabitatList habitatList = new HabitatList();
      for (int index = 0; index < this.Count; ++index)
        habitatList.Add(this[index].Habitat);
      return habitatList;
    }

    public int IndexOf(Habitat habitat)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Habitat == habitat)
            return index;
        }
      }
      return -1;
    }

    public int TotalPriorityValueForEmpire(Empire empire)
    {
      int num = 0;
      foreach (HabitatPrioritization habitatPrioritization in (SyncList<HabitatPrioritization>) this)
      {
        if (habitatPrioritization.Habitat.Owner == empire)
          num += habitatPrioritization.Priority;
      }
      return num;
    }

    public Habitat FindNearestHabitat(double x, double y)
    {
      Habitat nearestHabitat = (Habitat) null;
      double num = double.MaxValue;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index] != null)
        {
          Habitat habitat = this[index].Habitat;
          if (habitat != null && !habitat.HasBeenDestroyed)
          {
            double distanceSquaredStatic = Galaxy.CalculateDistanceSquaredStatic(x, y, habitat.Xpos, habitat.Ypos);
            if (nearestHabitat == null || distanceSquaredStatic < num)
            {
              nearestHabitat = habitat;
              num = distanceSquaredStatic;
            }
          }
        }
      }
      return nearestHabitat;
    }
  }
}
