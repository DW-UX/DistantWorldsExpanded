// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResearchNodeDefinition
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResearchNodeDefinition : IComparable<ResearchNodeDefinition>
  {
    public int ResearchNodeId;
    public string Name;
    public IndustryType Industry;
    public ComponentCategoryType Category;
    public int TechLevel;
    public int Row;
    public float Cost;
    public RaceList AllowedRaces;
    public RaceList DisallowedRaces;
    public double BaseCostMultiplierOverride;
    public int SpecialFunctionCode;
    public ResearchNodeDefinitionList ParentNodes = new ResearchNodeDefinitionList();
    public List<bool> ParentIsRequired = new List<bool>();
    public ComponentList Components = new ComponentList();
    public ResearchAbilityList Abilities = new ResearchAbilityList();
    public ComponentImprovementList ComponentImprovements = new ComponentImprovementList();
    public PlanetaryFacilityDefinition PlanetaryFacility;
    public FighterSpecificationList Fighters = new FighterSpecificationList();
    public Plague PlagueChange;
    [OptionalField]
    public RaceList SpecifiedRaces = new RaceList();
    [NonSerialized]
    public float SortTag;

    public ResearchNodeDefinition(
      int researchNodeId,
      string name,
      IndustryType industry,
      ComponentCategoryType category,
      int techLevel,
      float cost)
      : this(researchNodeId, name, industry, category, techLevel, cost, 0)
    {
    }

    public ResearchNodeDefinition(
      int researchNodeId,
      string name,
      IndustryType industry,
      ComponentCategoryType category,
      int techLevel,
      float cost,
      int row)
    {
      this.ResearchNodeId = researchNodeId;
      this.Name = name;
      this.Industry = industry;
      this.Category = category;
      this.TechLevel = techLevel;
      this.Row = row;
      this.Cost = cost;
    }

    public int BenefitCount
    {
      get
      {
        int benefitCount = 0;
        if (this.Components != null)
          benefitCount += this.Components.Count;
        if (this.ComponentImprovements != null)
          benefitCount += this.ComponentImprovements.Count;
        if (this.Abilities != null)
          benefitCount += this.Abilities.Count;
        if (this.Fighters != null)
          benefitCount += this.Fighters.Count;
        if (this.PlanetaryFacility != null)
          ++benefitCount;
        if (this.PlagueChange != null)
          ++benefitCount;
        return benefitCount;
      }
    }

    public bool IsEquivalent(ResearchNodeDefinition researchNodeDefinition) => researchNodeDefinition.Name == this.Name && researchNodeDefinition.Industry == this.Industry && researchNodeDefinition.Category == this.Category && researchNodeDefinition.TechLevel == this.TechLevel && (double) researchNodeDefinition.Cost == (double) this.Cost;

    int IComparable<ResearchNodeDefinition>.CompareTo(ResearchNodeDefinition other) => (double) this.SortTag > 0.0 || (double) other.SortTag > 0.0 ? this.SortTag.CompareTo(other.SortTag) : this.ResearchNodeId.CompareTo(other.ResearchNodeId);
  }
}
