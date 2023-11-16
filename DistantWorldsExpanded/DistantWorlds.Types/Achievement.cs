// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Achievement
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Achievement
  {
    public AchievementType Type;
    public int Value;
    public object AdditionalData;

    public Achievement(AchievementType type, int value, object additionalData)
    {
      this.Type = type;
      this.Value = value;
      this.AdditionalData = additionalData;
    }
  }
}
