// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.AchievementList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class AchievementList : List<Achievement>
  {
    public bool ContainsType(AchievementType type)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        Achievement achievement = this[index];
        if (achievement != null && achievement.Type == type)
          return true;
      }
      return false;
    }

    public Achievement GetFirstByType(AchievementType type)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        Achievement firstByType = this[index];
        if (firstByType != null && firstByType.Type == type)
          return firstByType;
      }
      return (Achievement) null;
    }

    public void AddIfNotExistsOrBetter(Achievement achievement)
    {
      bool flag = true;
      for (int index = 0; index < this.Count; ++index)
      {
        Achievement achievement1 = this[index];
        if (achievement1 != null && achievement1.Type == achievement.Type && achievement1.Value >= achievement.Value)
        {
          flag = false;
          break;
        }
      }
      if (!flag)
        return;
      this.Add(achievement);
    }
  }
}
