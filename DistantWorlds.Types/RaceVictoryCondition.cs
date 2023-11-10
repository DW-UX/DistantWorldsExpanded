// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.RaceVictoryCondition
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class RaceVictoryCondition : IComparable<RaceVictoryCondition>
  {
    public RaceVictoryConditionType Type;
    public float Proportion;
    public double Amount;
    public object AdditionalData;

    public RaceVictoryCondition(RaceVictoryConditionType type, double amount, float proportion)
      : this(type, amount, proportion, (object) null)
    {
    }

    public RaceVictoryCondition(
      RaceVictoryConditionType type,
      double amount,
      float proportion,
      object additionalData)
    {
      this.Type = type;
      this.Amount = amount;
      this.Proportion = proportion;
      this.AdditionalData = additionalData;
    }

    int IComparable<RaceVictoryCondition>.CompareTo(RaceVictoryCondition other) => this.Proportion.CompareTo(other.Proportion);
  }
}
