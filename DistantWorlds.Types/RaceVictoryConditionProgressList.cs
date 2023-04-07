// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.RaceVictoryConditionProgressList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class RaceVictoryConditionProgressList : SyncList<RaceVictoryConditionProgress>
  {
    public double TotalProgress
    {
      get
      {
        double totalProgress = 0.0;
        for (int index = 0; index < this.Count; ++index)
          totalProgress += this[index].ProgressTotalPortion;
        return totalProgress;
      }
    }
  }
}
