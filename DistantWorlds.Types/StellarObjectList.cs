// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.StellarObjectList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class StellarObjectList : SyncList<StellarObject>
  {
    public static readonly int HabitatIndexRangeIncrease = 100000000;

    public void AddRange(BuiltObjectList builtObjects)
    {
      if (builtObjects == null)
        return;
      lock (this._LockObject)
      {
        foreach (StellarObject builtObject in (SyncList<BuiltObject>) builtObjects)
          this.Add(builtObject);
      }
    }

    public StellarObject FindStellarObjectById(int stellarObjectId)
    {
      foreach (StellarObject stellarObject in (SyncList<StellarObject>) this)
      {
        if (stellarObject != null)
        {
          if (stellarObject is BuiltObject)
          {
            BuiltObject stellarObjectById = (BuiltObject) stellarObject;
            if (stellarObjectById.BuiltObjectID == stellarObjectId)
              return (StellarObject) stellarObjectById;
          }
          else if (stellarObject is Habitat)
          {
            Habitat stellarObjectById = (Habitat) stellarObject;
            if (stellarObjectById.HabitatIndex + StellarObjectList.HabitatIndexRangeIncrease == stellarObjectId)
              return (StellarObject) stellarObjectById;
          }
        }
      }
      return (StellarObject) null;
    }

    public StellarObject FindNearest(double x, double y)
    {
      StellarObject nearest = (StellarObject) null;
      double num = double.MaxValue;
      for (int index = 0; index < this.Count; ++index)
      {
        StellarObject stellarObject = this[index];
        if (stellarObject != null)
        {
          double distanceSquaredStatic = Galaxy.CalculateDistanceSquaredStatic(x, y, stellarObject.Xpos, stellarObject.Ypos);
          if (nearest == null || distanceSquaredStatic < num)
          {
            nearest = stellarObject;
            num = distanceSquaredStatic;
          }
        }
      }
      return nearest;
    }

    public int TotalMobileMilitaryFirepower() => this.TotalMobileMilitaryFirepower((Empire) null);

    public int TotalMobileMilitaryFirepower(Empire empire)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index] is BuiltObject)
        {
          BuiltObject builtObject = (BuiltObject) this[index];
          if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Role == BuiltObjectRole.Military && builtObject.UnbuiltComponentCount <= 0 && builtObject.TopSpeed > (short) 0 && (empire == null || builtObject.Empire == empire))
            num += builtObject.FirepowerRaw;
        }
      }
      return num;
    }

    public bool CheckForNonDestroyedObjects()
    {
      for (int index = 0; index < this.Count; ++index)
      {
        StellarObject stellarObject = this[index];
        if (stellarObject != null && !stellarObject.HasBeenDestroyed)
          return true;
      }
      return false;
    }

    public bool ContainsFighterOrBuiltObject(BuiltObject builtObject)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index] is BuiltObject)
        {
          if (this[index] == builtObject)
            return true;
        }
        else if (this[index] is Fighter && this[index].ParentBuiltObject == builtObject)
          return true;
      }
      return false;
    }
  }
}
