// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResearchNode
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResearchNode : IComparable<ResearchNode>
  {
    private int _ResearchNodeId;
    public float Progress;
    public bool IsRushing;
    public bool IsResearched;
    public bool SelfResearched;
    public float Cost;
    [OptionalField]
    public bool IsEnabled = true;
    public float SortTag;
    public ResearchNodeList ParentNodes;
    public List<bool> ParentIsRequired;

    public ResearchNode(int researchNodeId)
    {
      this._ResearchNodeId = researchNodeId;
      this.Cost = Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].Cost;
    }

    public int ResearchNodeId => this._ResearchNodeId;

    public string Name => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].Name;

    public IndustryType Industry => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].Industry;

    public ComponentCategoryType Category => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].Category;

    public int TechLevel => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].TechLevel;

    public int Row => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].Row;

    public int SpecialFunctionCode => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].SpecialFunctionCode;

    public RaceList AllowedRaces => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].AllowedRaces;

    public RaceList DisallowedRaces => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].DisallowedRaces;

    public ComponentList Components => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].Components;

    public ResearchAbilityList Abilities => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].Abilities;

    public ComponentImprovementList ComponentImprovements => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].ComponentImprovements;

    public PlanetaryFacilityDefinition PlanetaryFacility => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].PlanetaryFacility;

    public FighterSpecificationList Fighters => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].Fighters;

    public Plague PlagueChange => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].PlagueChange;

    public int BenefitCount => Galaxy.ResearchNodeDefinitionsStatic[this._ResearchNodeId].BenefitCount;

    public ResearchNode Clone() => new ResearchNode(this._ResearchNodeId)
    {
      IsResearched = this.IsResearched,
      IsRushing = this.IsRushing,
      Progress = this.Progress
    };

    public bool CheckAnyComponentTypeMatches(ComponentType type)
    {
      if (this.Components != null)
      {
        for (int index = 0; index < this.Components.Count; ++index)
        {
          Component component = this.Components[index];
          if (component != null && component.Type == type)
            return true;
        }
        for (int index = 0; index < this.ComponentImprovements.Count; ++index)
        {
          ComponentImprovement componentImprovement = this.ComponentImprovements[index];
          if (componentImprovement != null && componentImprovement.ImprovedComponent != null && componentImprovement.ImprovedComponent.Type == type)
            return true;
        }
      }
      return false;
    }

    public List<ComponentType> ResolveComponentTypesAll()
    {
      List<ComponentType> componentTypeList = new List<ComponentType>();
      if (this.Components != null)
      {
        for (int index = 0; index < this.Components.Count; ++index)
        {
          Component component = this.Components[index];
          if (component != null && !componentTypeList.Contains(component.Type))
            componentTypeList.Add(component.Type);
        }
      }
      if (this.ComponentImprovements != null)
      {
        for (int index = 0; index < this.ComponentImprovements.Count; ++index)
        {
          ComponentImprovement componentImprovement = this.ComponentImprovements[index];
          if (componentImprovement != null && componentImprovement.ImprovedComponent != null && !componentTypeList.Contains(componentImprovement.ImprovedComponent.Type))
            componentTypeList.Add(componentImprovement.ImprovedComponent.Type);
        }
      }
      return componentTypeList;
    }

    public ComponentType ResolveComponentType()
    {
      if (this.Components != null && this.Components.Count > 0)
        return this.Components[0].Type;
      return this.ComponentImprovements != null && this.ComponentImprovements.Count > 0 && this.ComponentImprovements[0].ImprovedComponent != null ? this.ComponentImprovements[0].ImprovedComponent.Type : ComponentType.Undefined;
    }

    public ResearchAbilityType ResolveResearchAbilityType() => this.Abilities != null && this.Abilities.Count > 0 ? this.Abilities[0].Type : ResearchAbilityType.Undefined;

    public int CountRequiredParents()
    {
      int num = 0;
      if (this.ParentIsRequired != null && this.ParentIsRequired.Count > 0)
      {
        for (int index = 0; index < this.ParentIsRequired.Count; ++index)
        {
          if (this.ParentIsRequired[index])
            ++num;
        }
      }
      return num;
    }

    public bool IsEquivalent(ResearchNode researchNode) => researchNode.Name == this.Name && researchNode.Industry == this.Industry && researchNode.Category == this.Category && researchNode.TechLevel == this.TechLevel && researchNode.Row == this.Row;

    public override string ToString() => this.Name;

    int IComparable<ResearchNode>.CompareTo(ResearchNode other) => this.SortTag.CompareTo(other.SortTag);
  }
}
