// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.RaceVictoryConditionProgress
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class RaceVictoryConditionProgress : IComparable<RaceVictoryConditionProgress>
  {
    public RaceVictoryConditionType Type;
    public double ProgressTotalPortion;
    public double ThisProgress;
    public Empire BestEmpire;
    public string Detail;
    public RaceVictoryCondition Condition;

    public RaceVictoryConditionProgress(
      RaceVictoryConditionType type,
      double progressTotalPortion,
      double thisProgress,
      Empire bestEmpire,
      string detail,
      RaceVictoryCondition condition)
    {
      this.Type = type;
      this.ProgressTotalPortion = progressTotalPortion;
      this.ThisProgress = thisProgress;
      this.BestEmpire = bestEmpire;
      this.Detail = detail;
      this.Condition = condition;
    }

    int IComparable<RaceVictoryConditionProgress>.CompareTo(RaceVictoryConditionProgress other) => this.ProgressTotalPortion.CompareTo(other.ProgressTotalPortion);
  }
}
