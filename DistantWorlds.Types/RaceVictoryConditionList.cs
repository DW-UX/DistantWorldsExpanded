// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.RaceVictoryConditionList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class RaceVictoryConditionList : List<RaceVictoryCondition>
  {
    public bool ContainsConditionType(RaceVictoryConditionType type)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type)
          return true;
      }
      return false;
    }

    public RaceVictoryCondition GetRaceVictoryConditionByType(RaceVictoryConditionType type)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type)
          return this[index];
      }
      return (RaceVictoryCondition) null;
    }
  }
}
