// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.RaceList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Globalization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class RaceList : SyncList<Race>
  {
    public Race this[string raceName]
    {
      get
      {
        foreach (Race race in (SyncList<Race>) this)
        {
          if (race.Name.ToLower(CultureInfo.InvariantCulture) == raceName.ToLower(CultureInfo.InvariantCulture))
            return race;
        }
        return (Race) null;
      }
    }

    public int IndexOf(string raceName)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Name == raceName)
          return index;
      }
      return -1;
    }

    public RaceList ResolveNormalEmpireRaces()
    {
      RaceList raceList = new RaceList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].CanBeNormalEmpire)
          raceList.Add(this[index]);
      }
      return raceList;
    }

    public RaceList ResolvePlayableRaces()
    {
      RaceList raceList = new RaceList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Playable)
          raceList.Add(this[index]);
      }
      return raceList;
    }
  }
}
