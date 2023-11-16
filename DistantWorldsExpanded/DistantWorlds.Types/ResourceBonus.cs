// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResourceBonus
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResourceBonus
  {
    public byte ResourceId;
    public ColonyResourceEffect Effect;
    public double Value;
    public bool AppliesOnlyToSources;

    public ResourceBonus(byte resourceId, ColonyResourceEffect effect, double value)
      : this(resourceId, effect, value, false)
    {
    }

    public ResourceBonus(
      byte resourceId,
      ColonyResourceEffect effect,
      double value,
      bool appliesOnlyToSource)
    {
      this.ResourceId = resourceId;
      this.Effect = effect;
      this.Value = value;
      this.AppliesOnlyToSources = appliesOnlyToSource;
    }
  }
}
