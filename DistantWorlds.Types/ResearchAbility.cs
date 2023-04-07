// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResearchAbility
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResearchAbility
  {
    public string Name;
    public ResearchAbilityType Type;
    public int Level;
    public int Value;
    public object RelatedObject;

    public ResearchAbility(
      string name,
      ResearchAbilityType type,
      int level,
      int value,
      object relatedObject)
    {
      this.Name = name;
      this.Type = type;
      this.Level = level;
      this.Value = value;
      this.RelatedObject = relatedObject;
    }
  }
}
