// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DesignSpecificationComponentRuleList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DesignSpecificationComponentRuleList : SyncList<DesignSpecificationComponentRule>
  {
    public bool CheckAnyRulesUseComponent(Component component)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        DesignSpecificationComponentRule specificationComponentRule = this[index];
        if (specificationComponentRule != null && (specificationComponentRule.ComponentRuleType == DesignSpecificationComponentRuleType.MustHave || specificationComponentRule.ComponentRuleType == DesignSpecificationComponentRuleType.ShouldHave))
        {
          if (specificationComponentRule.ComponentType != ComponentType.Undefined)
          {
            if (component.Type == specificationComponentRule.ComponentType)
              return true;
          }
          else if (specificationComponentRule.ComponentCategory != ComponentCategoryType.Undefined && component.Category == specificationComponentRule.ComponentCategory)
            return true;
        }
      }
      return false;
    }
  }
}
