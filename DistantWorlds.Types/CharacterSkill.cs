// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.CharacterSkill
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class CharacterSkill
  {
    public CharacterSkillType Type;
    public int Level;
    public float Progress;
    public float NextProgressThreshold = 1f;

    public CharacterSkill(CharacterSkillType skillType, int level)
    {
      this.Type = skillType;
      this.Level = level;
    }
  }
}
