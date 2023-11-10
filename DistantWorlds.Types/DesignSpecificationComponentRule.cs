// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DesignSpecificationComponentRule
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DesignSpecificationComponentRule
  {
    private DesignSpecificationComponentRuleType _ComponentRuleType;
    private ComponentCategoryType _ComponentCategoryType;
    private ComponentType _ComponentType;
    private int _Amount;

    public DesignSpecificationComponentRule(
      DesignSpecificationComponentRuleType componentRuleType,
      ComponentCategoryType componentCategory,
      int amount)
    {
      this._ComponentRuleType = componentRuleType;
      this._ComponentCategoryType = componentCategory;
      this._ComponentType = ComponentType.Undefined;
      this._Amount = amount;
    }

    public DesignSpecificationComponentRule(
      DesignSpecificationComponentRuleType componentRuleType,
      ComponentType componentType,
      int amount)
    {
      this._ComponentRuleType = componentRuleType;
      this._ComponentType = componentType;
      this._ComponentCategoryType = ComponentDefinition.ResolveComponentCategory(componentType);
      this._Amount = amount;
    }

    public int Amount
    {
      get => this._Amount;
      set => this._Amount = value;
    }

    public DesignSpecificationComponentRuleType ComponentRuleType
    {
      get => this._ComponentRuleType;
      set => this._ComponentRuleType = value;
    }

    public ComponentCategoryType ComponentCategory => this._ComponentCategoryType;

    public ComponentType ComponentType => this._ComponentType;
  }
}
