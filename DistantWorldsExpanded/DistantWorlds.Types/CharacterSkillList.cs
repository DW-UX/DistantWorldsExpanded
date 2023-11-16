// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.CharacterSkillList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class CharacterSkillList : List<CharacterSkill>
  {
    public void CombineSkillList(CharacterSkillList newSkills)
    {
      if (newSkills == null)
        return;
      for (int index = 0; index < newSkills.Count; ++index)
      {
        CharacterSkill newSkill = newSkills[index];
        if (newSkill != null)
        {
          CharacterSkill skillByType = this.GetSkillByType(newSkill.Type);
          if (skillByType != null)
            skillByType.Level += newSkill.Level;
          else
            this.Add(newSkill);
        }
      }
    }

    public CharacterSkill GetSkillByType(CharacterSkillType skillType)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        CharacterSkill skillByType = this[index];
        if (skillByType != null && skillByType.Type == skillType)
          return skillByType;
      }
      return (CharacterSkill) null;
    }
  }
}
