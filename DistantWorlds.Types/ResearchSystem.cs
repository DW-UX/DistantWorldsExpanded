// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResearchSystem
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResearchSystem
  {
    public ResearchNodeList TechTree = new ResearchNodeList();
    public ComponentList ResearchedComponents = new ComponentList();
    [NonSerialized]
    private bool[] _ResearchedComponentState = new bool[0];
    public ComponentImprovement[] ComponentImprovements = new ComponentImprovement[Galaxy.ComponentDefinitionsStatic.Length];
    public PlanetaryFacilityDefinitionList BuildablePlanetaryFacilities = new PlanetaryFacilityDefinitionList();
    public PlagueList EnabledPlagues = new PlagueList();
    public ResearchAbilityList Abilities = new ResearchAbilityList();
    public FighterSpecificationList ResearchedFighters = new FighterSpecificationList();
    private Component[] _LatestComponentsByType;
    private Component[] _LatestComponentsByCategory;
    public ResearchNodeList LatestProjects;
    public ResearchNodeList NextProjects;
    [OptionalField]
    private Component[] _BestComponentsByType;
    [OptionalField]
    private Component[] _BestComponentsByCategory;
    [OptionalField]
    public ResearchNodeList RecentProjects = new ResearchNodeList();
    private ComponentImprovementList ComponentsWeaponBeamOrderedByRange = new ComponentImprovementList();
    private ComponentImprovementList ComponentsWeaponTorpedoOrderedByRange = new ComponentImprovementList();
    private ComponentImprovementList ComponentsWeaponAreaOrderedByRange = new ComponentImprovementList();
    private ComponentImprovementList ComponentsWeaponBeamOrderedByPower = new ComponentImprovementList();
    private ComponentImprovementList ComponentsWeaponTorpedoOrderedByPower = new ComponentImprovementList();
    private ComponentImprovementList ComponentsWeaponAreaOrderedByPower = new ComponentImprovementList();
    private ComponentImprovementList ComponentsReactorOrderedByEfficiency = new ComponentImprovementList();
    private ComponentImprovementList ComponentsReactorOrderedByPower = new ComponentImprovementList();
    private ComponentImprovementList ComponentsEngineMainThrustOrderedByPower = new ComponentImprovementList();
    private ComponentImprovementList ComponentsEngineVectoringOrderedByPower = new ComponentImprovementList();
    private ComponentImprovementList ComponentsEngineMainThrustOrderedByEfficiency = new ComponentImprovementList();
    private ComponentImprovementList ComponentsEngineVectoringOrderedByEfficiency = new ComponentImprovementList();
    private ComponentImprovementList ComponentsHyperdriveOrderedByPower = new ComponentImprovementList();
    private ComponentImprovementList ComponentsHyperdriveOrderedByEfficiency = new ComponentImprovementList();
    private ComponentImprovementList ComponentsHyperdriveOrderedByJumpInitiation = new ComponentImprovementList();
    public ResearchNodeList ResearchQueueWeapons = new ResearchNodeList();
    public ResearchNodeList ResearchQueueEnergy = new ResearchNodeList();
    public ResearchNodeList ResearchQueueHighTech = new ResearchNodeList();
    public static int[] ComponentMaxTechPoints = new int[Galaxy.ComponentDefinitionsStatic.Length];
    public static int[] ComponentMinTechPoints = new int[Galaxy.ComponentDefinitionsStatic.Length];
    [NonSerialized]
    public ResearchNodeList[] ResearchProjectsPerComponent;
    [NonSerialized]
    public ResearchNodeList[] ResearchProjectsPerComponentImprovement;

    public ResearchSystem Clone(Race race)
    {
      ResearchSystem researchSystem = new ResearchSystem();
      researchSystem.TechTree = this.TechTree.Clone();
      researchSystem.Update(race);
      return researchSystem;
    }

    public void Update(Race race)
    {
      bool[] researchedComponentState;
      this.ResearchedComponents = this.DetermineResearchedComponents(out researchedComponentState);
      this._ResearchedComponentState = researchedComponentState;
      this.ComponentImprovements = this.DetermineComponentImprovements();
      this.Abilities = this.DetermineResearchAbilities();
      this.BuildablePlanetaryFacilities = this.DetermineBuildablePlanetaryFacilities();
      this.ResearchedFighters = this.DetermineResearchedFighters();
      this.EnabledPlagues = this.ReviewPlagues();
      this._LatestComponentsByType = this.DetermineLatestComponentsByType(this.ResearchedComponents);
      this._LatestComponentsByCategory = this.DetermineLatestComponentsByCategory(this.ResearchedComponents);
      this._BestComponentsByType = this.DetermineBestComponentsByType(this.ResearchedComponents);
      this._BestComponentsByCategory = this.DetermineBestComponentsByCategory(this.ResearchedComponents);
      this.RefreshLatestNextProjects(race);
      this.ReviewOrderedComponents();
    }

    public bool CheckComponentResearched(Component component)
    {
      if (component == null)
        return false;
      return this._ResearchedComponentState != null ? this._ResearchedComponentState[component.ComponentID] : this.ResearchedComponents.Contains(component);
    }

    public bool MergeNewDefinitions(ResearchNodeDefinitionList definitions, Race empireRace)
    {
      bool flag = false;
      ResearchNodeList researchNodeList = new ResearchNodeList();
      for (int index = 0; index < this.TechTree.Count; ++index)
      {
        ResearchNode researchNode = this.TechTree[index];
        if (researchNode != null && definitions.FindNodeById(researchNode.ResearchNodeId) == null)
          researchNodeList.Add(researchNode);
      }
      for (int index = 0; index < researchNodeList.Count; ++index)
      {
        this.TechTree.Remove(researchNodeList[index]);
        flag = true;
      }
      if (this.TechTree.Count != definitions.Count)
        flag = true;
      ResearchNodeList techTree = definitions.ObtainTechTree(empireRace);
      for (int index = 0; index < techTree.Count; ++index)
      {
        ResearchNode researchNode = techTree[index];
        if (researchNode != null)
        {
          ResearchNode nodeById = this.TechTree.FindNodeById(researchNode.ResearchNodeId);
          if (nodeById != null)
          {
            researchNode.IsEnabled = nodeById.IsEnabled;
            researchNode.IsResearched = nodeById.IsResearched;
            researchNode.IsRushing = nodeById.IsRushing;
            researchNode.Progress = nodeById.Progress;
            researchNode.SelfResearched = nodeById.SelfResearched;
          }
        }
      }
      this.TechTree = techTree;
      this.Update(empireRace);
      return flag;
    }

    public void DetermineResearchProjectsPerComponent(ResearchNodeList projects)
    {
      this.ResearchProjectsPerComponent = new ResearchNodeList[Galaxy.ComponentDefinitionsStatic.Length];
      this.ResearchProjectsPerComponentImprovement = new ResearchNodeList[Galaxy.ComponentDefinitionsStatic.Length];
      for (int index1 = 0; index1 < projects.Count; ++index1)
      {
        ResearchNode project = projects[index1];
        if (project != null)
        {
          if (project.Components != null && project.Components.Count > 0)
          {
            for (int index2 = 0; index2 < project.Components.Count; ++index2)
            {
              Component component = project.Components[index2];
              if (component != null)
              {
                if (this.ResearchProjectsPerComponent[component.ComponentID] == null)
                  this.ResearchProjectsPerComponent[component.ComponentID] = new ResearchNodeList();
                this.ResearchProjectsPerComponent[component.ComponentID].Add(project);
              }
            }
          }
          if (project.ComponentImprovements != null && project.ComponentImprovements.Count > 0)
          {
            for (int index3 = 0; index3 < project.ComponentImprovements.Count; ++index3)
            {
              ComponentImprovement componentImprovement = project.ComponentImprovements[index3];
              if (componentImprovement != null && componentImprovement.ImprovedComponent != null)
              {
                if (this.ResearchProjectsPerComponentImprovement[componentImprovement.ImprovedComponent.ComponentID] == null)
                  this.ResearchProjectsPerComponentImprovement[componentImprovement.ImprovedComponent.ComponentID] = new ResearchNodeList();
                this.ResearchProjectsPerComponentImprovement[componentImprovement.ImprovedComponent.ComponentID].Add(project);
              }
            }
          }
        }
      }
    }

    public void RefreshLatestNextProjects(Race race) => this.LatestProjects = this.DetermineLatestResearchProjects(this.TechTree, race, out this.NextProjects);

    public void ReviewOrderedComponents()
    {
      this.ComponentsWeaponBeamOrderedByRange = this.FilterUnresearchedComponents(Galaxy.ComponentsWeaponBeamOrderedByRange);
      this.ComponentsWeaponTorpedoOrderedByRange = this.FilterUnresearchedComponents(Galaxy.ComponentsWeaponTorpedoOrderedByRange);
      this.ComponentsWeaponAreaOrderedByRange = this.FilterUnresearchedComponents(Galaxy.ComponentsWeaponAreaOrderedByRange);
      this.ComponentsWeaponBeamOrderedByPower = this.FilterUnresearchedComponents(Galaxy.ComponentsWeaponBeamOrderedByPower);
      this.ComponentsWeaponTorpedoOrderedByPower = this.FilterUnresearchedComponents(Galaxy.ComponentsWeaponTorpedoOrderedByPower);
      this.ComponentsWeaponAreaOrderedByPower = this.FilterUnresearchedComponents(Galaxy.ComponentsWeaponAreaOrderedByPower);
      this.ComponentsReactorOrderedByEfficiency = this.FilterUnresearchedComponents(Galaxy.ComponentsReactorOrderedByEfficiency);
      this.ComponentsReactorOrderedByPower = this.FilterUnresearchedComponents(Galaxy.ComponentsReactorOrderedByPower);
      this.ComponentsEngineMainThrustOrderedByPower = this.FilterUnresearchedComponents(Galaxy.ComponentsEngineMainThrustOrderedByPower);
      this.ComponentsEngineVectoringOrderedByPower = this.FilterUnresearchedComponents(Galaxy.ComponentsEngineVectoringOrderedByPower);
      this.ComponentsEngineMainThrustOrderedByEfficiency = this.FilterUnresearchedComponents(Galaxy.ComponentsEngineMainThrustOrderedByEfficiency);
      this.ComponentsEngineVectoringOrderedByEfficiency = this.FilterUnresearchedComponents(Galaxy.ComponentsEngineVectoringOrderedByEfficiency);
      this.ComponentsHyperdriveOrderedByPower = this.FilterUnresearchedComponents(Galaxy.ComponentsHyperdriveOrderedByPower);
      this.ComponentsHyperdriveOrderedByEfficiency = this.FilterUnresearchedComponents(Galaxy.ComponentsHyperdriveOrderedByEfficiency);
      this.ComponentsHyperdriveOrderedByJumpInitiation = this.FilterUnresearchedComponents(Galaxy.ComponentsHyperdriveOrderedByJumpInitiation);
    }

    private ComponentImprovementList FilterUnresearchedComponents(ComponentList components)
    {
      ComponentImprovementList componentImprovementList = new ComponentImprovementList();
      for (int index = 0; index < components.Count; ++index)
      {
        if (this.ResearchedComponents.Contains(components[index]))
        {
          ComponentImprovement componentImprovement = this.ResolveImprovedComponentValues(components[index]);
          componentImprovementList.Add(componentImprovement);
        }
      }
      return componentImprovementList;
    }

    public ResearchNode FindProjectForComponent(Component component)
    {
      for (int index1 = 0; index1 < this.TechTree.Count; ++index1)
      {
        if (this.TechTree[index1].Components != null && this.TechTree[index1].Components.Count > 0)
        {
          for (int index2 = 0; index2 < this.TechTree[index1].Components.Count; ++index2)
          {
            if (this.TechTree[index1].Components[index2].ComponentID == component.ComponentID)
              return this.TechTree[index1];
          }
        }
      }
      return (ResearchNode) null;
    }

    public ResearchNode FindProjectForComponentImprovement(ComponentImprovement componentImprovement)
    {
      for (int index1 = 0; index1 < this.TechTree.Count; ++index1)
      {
        if (this.TechTree[index1].ComponentImprovements != null && this.TechTree[index1].ComponentImprovements.Count > 0)
        {
          for (int index2 = 0; index2 < this.TechTree[index1].ComponentImprovements.Count; ++index2)
          {
            if (this.TechTree[index1].ComponentImprovements[index2].ImprovedComponent.ComponentID == componentImprovement.ImprovedComponent.ComponentID && this.TechTree[index1].ComponentImprovements[index2].TechLevel == componentImprovement.TechLevel)
              return this.TechTree[index1];
          }
        }
      }
      return (ResearchNode) null;
    }

    public ResearchNodeList ResolveMoreAdvancedProjects(Empire giverEmpire) => this.ResolveMoreAdvancedProjects(giverEmpire, true);

    public ResearchNodeList ResolveMoreAdvancedProjects(Empire giverEmpire, bool includeSpecialTech)
    {
      ResearchNodeList researchNodeList = new ResearchNodeList();
      foreach (ResearchNode researchNode in ListHelper.ToArrayThreadSafe(this.NextProjects))
      {
        if (researchNode != null)
        {
          ResearchNode equivalent = giverEmpire.Research.TechTree.GetEquivalent(researchNode);
          if (equivalent.IsResearched)
          {
            if (!includeSpecialTech)
            {
              if (equivalent.AllowedRaces == null || equivalent.AllowedRaces.Count <= 0)
                researchNodeList.Add(equivalent);
            }
            else
              researchNodeList.Add(equivalent);
          }
        }
      }
      if (includeSpecialTech)
      {
        for (int index = 0; index < giverEmpire.Research.TechTree.Count; ++index)
        {
          ResearchNode researchNode = giverEmpire.Research.TechTree[index];
          if (researchNode.IsResearched && researchNode.AllowedRaces != null && researchNode.AllowedRaces.Count > 0)
          {
            ResearchNode equivalent = this.TechTree.GetEquivalent(researchNode);
            if (!equivalent.IsResearched && !researchNodeList.Contains(researchNode) && this.CanResearchNode(equivalent))
              researchNodeList.Add(researchNode);
          }
        }
      }
      return researchNodeList;
    }

    public ResearchNodeList ResolvePathProjectsToHighestProjectOfType(
      ComponentCategoryType category,
      bool includeResearchedNodes)
    {
      return this.TechTree.GetCurrentPath(this.TechTree.GetHighestProjectForCategory(category), (Race) null);
    }

    public ResearchNodeList ResolvePathProjectsToHighestProjectOfType(
      ComponentType type,
      bool includeResearchedNodes)
    {
      return this.TechTree.GetCurrentPath(this.TechTree.GetHighestProjectForTypeAny(type), (Race) null);
    }

    public ResearchNodeList ResolveEssentialProjects_NEW(
      Empire empire,
      Galaxy galaxy,
      IndustryType industry,
      ResearchNodeList industryNextProjects,
      List<ComponentType> techComponentFocuses,
      List<ComponentCategoryType> techCategoryFocuses)
    {
      ResearchNodeList projects = new ResearchNodeList();
      ResearchNode projectForTypeAny1 = this.TechTree.GetLowestProjectForTypeAny(ComponentType.WeaponPointDefense);
      ResearchNode projectForTypeAny2 = this.TechTree.GetLowestProjectForTypeAny(ComponentType.ComputerCountermeasures);
      ResearchNode projectForTypeAny3 = this.TechTree.GetLowestProjectForTypeAny(ComponentType.ComputerTargetting);
      ResearchNode projectForTypeAny4 = this.TechTree.GetLowestProjectForTypeAny(ComponentType.SensorLongRange);
      ResearchNode projectForTypeAny5 = this.TechTree.GetLowestProjectForTypeAny(ComponentType.Armor);
      ResearchNode projectForTypeAny6 = this.TechTree.GetLowestProjectForTypeAny(ComponentType.FighterBay);
      if (projectForTypeAny1 != null)
        projects.Add(projectForTypeAny1);
      if (projectForTypeAny2 != null)
        projects.Add(projectForTypeAny2);
      if (projectForTypeAny3 != null)
        projects.Add(projectForTypeAny3);
      if (projectForTypeAny4 != null)
        projects.Add(projectForTypeAny4);
      if (projectForTypeAny5 != null && !projects.ContainsById(projectForTypeAny5.ResearchNodeId))
        projects.Add(projectForTypeAny5);
      if (projectForTypeAny6 != null)
        projects.Add(projectForTypeAny6);
      if (empire != null)
      {
        if (empire.PirateEmpireBaseHabitat != null)
        {
          projects.Add(this.TechTree.GetHighestProjectForTypeAny(ComponentType.AssaultPod));
          projects.Add(this.TechTree.GetHighestProjectForTypeAny(ComponentType.WeaponTractorBeam));
          projects.Add(this.TechTree.GetHighestProjectForTypeAny(ComponentType.SensorScannerJammer));
        }
        else
        {
          ResearchNode planetaryFacilityType1 = this.TechTree.GetLowestProjectForPlanetaryFacilityType(PlanetaryFacilityType.ArmoredFactory);
          ResearchNode dedicatedCarriers = this.TechTree.GetLowestProjectForDedicatedCarriers();
          ResearchNode projectForWonderType1 = this.TechTree.GetLowestProjectForWonderType(WonderType.EmpireIncome);
          ResearchNode projectForWonderType2 = this.TechTree.GetLowestProjectForWonderType(WonderType.ColonyIncome);
          ResearchNode projectForWonderType3 = this.TechTree.GetLowestProjectForWonderType(WonderType.EmpireHappiness);
          ResearchNode projectForWonderType4 = this.TechTree.GetLowestProjectForWonderType(WonderType.ColonyHappiness);
          ResearchNode projectForWonderType5 = this.TechTree.GetLowestProjectForWonderType(WonderType.EmpireResearchEnergy);
          ResearchNode projectForWonderType6 = this.TechTree.GetLowestProjectForWonderType(WonderType.EmpireResearchHighTech);
          ResearchNode projectForWonderType7 = this.TechTree.GetLowestProjectForWonderType(WonderType.EmpireResearchWeapons);
          ResearchNode planetaryFacilityType2 = this.TechTree.GetLowestProjectForPlanetaryFacilityType(PlanetaryFacilityType.RegionalCapital);
          ResearchNode projectForWonderType8 = this.TechTree.GetLowestProjectForWonderType(WonderType.ColonyConstructionSpeed);
          if (planetaryFacilityType1 != null)
            projects.Add(planetaryFacilityType1);
          int maximumAllowableTechLevelGap = 2;
          ResearchNode projectForIndustry = this.TechTree.GetHighestResearchedProjectForIndustry(industry);
          int highestIndustryTechLevel = 0;
          if (projectForIndustry != null)
            highestIndustryTechLevel = projectForIndustry.TechLevel;
          this.IdentifyLaggingProjectAndAdd(ComponentType.Shields, empire.DominantRace, highestIndustryTechLevel, maximumAllowableTechLevelGap, empire.Policy.ResearchDesignOverallFocus, industryNextProjects, ref projects);
          this.IdentifyLaggingProjectAndAdd(ComponentType.EngineMainThrust, empire.DominantRace, highestIndustryTechLevel, maximumAllowableTechLevelGap, empire.Policy.ResearchDesignOverallFocus, industryNextProjects, ref projects);
          this.IdentifyLaggingProjectAndAdd(ComponentType.EngineVectoring, empire.DominantRace, highestIndustryTechLevel, maximumAllowableTechLevelGap, empire.Policy.ResearchDesignOverallFocus, industryNextProjects, ref projects);
          this.IdentifyLaggingProjectAndAdd(ComponentType.HyperDrive, empire.DominantRace, highestIndustryTechLevel, maximumAllowableTechLevelGap, empire.Policy.ResearchDesignOverallFocus, industryNextProjects, ref projects);
          this.IdentifyLaggingProjectAndAdd(ComponentType.Reactor, empire.DominantRace, highestIndustryTechLevel, maximumAllowableTechLevelGap, empire.Policy.ResearchDesignOverallFocus, industryNextProjects, ref projects);
          this.IdentifyLaggingProjectAndAdd(ComponentType.Armor, empire.DominantRace, highestIndustryTechLevel, maximumAllowableTechLevelGap, empire.Policy.ResearchDesignOverallFocus, industryNextProjects, ref projects);
          this.IdentifyLaggingProjectAndAdd(ComponentType.DamageControl, empire.DominantRace, highestIndustryTechLevel, maximumAllowableTechLevelGap, empire.Policy.ResearchDesignOverallFocus, industryNextProjects, ref projects);
          if (empire.DominantRace != null)
          {
            if (empire.DominantRace.CautionLevel >= 100)
            {
              this.IdentifyLaggingProjectAndAdd(PlanetaryFacilityType.FortifiedBunker, highestIndustryTechLevel, maximumAllowableTechLevelGap, ref projects);
              this.IdentifyLaggingProjectAndAdd(TroopType.Artillery, highestIndustryTechLevel, maximumAllowableTechLevelGap, ref projects);
              if (empire.DominantRace.CautionLevel >= 110)
              {
                this.IdentifyLaggingProjectAndAdd(PlanetaryFacilityType.PlanetaryShield, highestIndustryTechLevel, maximumAllowableTechLevelGap, ref projects);
                this.IdentifyLaggingProjectAndAdd(ComponentType.ComputerCountermeasuresFleet, empire.DominantRace, highestIndustryTechLevel, maximumAllowableTechLevelGap, empire.Policy.ResearchDesignOverallFocus, industryNextProjects, ref projects);
              }
            }
            if (empire.DominantRace.AggressionLevel >= 110)
            {
              this.IdentifyLaggingProjectAndAdd(TroopType.SpecialForces, highestIndustryTechLevel, maximumAllowableTechLevelGap, ref projects);
              if (empire.DominantRace.AggressionLevel >= 115)
              {
                this.IdentifyLaggingProjectAndAdd(ComponentType.WeaponBombard, empire.DominantRace, highestIndustryTechLevel, maximumAllowableTechLevelGap, empire.Policy.ResearchDesignOverallFocus, industryNextProjects, ref projects);
                this.IdentifyLaggingProjectAndAdd(ComponentType.ComputerTargettingFleet, empire.DominantRace, highestIndustryTechLevel, maximumAllowableTechLevelGap, empire.Policy.ResearchDesignOverallFocus, industryNextProjects, ref projects);
              }
            }
            if (empire.DominantRace.IntelligenceLevel >= 100)
            {
              PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.ColonyConstructionSpeed);
              if (!galaxy.CheckWonderBuilt(wonderByType))
              {
                if (projectForWonderType8 != null && (techComponentFocuses.Contains(ComponentType.ConstructionBuild) || techCategoryFocuses.Contains(ComponentCategoryType.Construction)))
                  projects.Add(projectForWonderType8);
                else
                  this.IdentifyLaggingProjectAndAdd(WonderType.ColonyConstructionSpeed, highestIndustryTechLevel, maximumAllowableTechLevelGap, ref projects);
              }
              if (dedicatedCarriers != null)
                projects.Add(dedicatedCarriers);
              if (planetaryFacilityType2 != null && empire.Colonies != null && empire.Colonies.Count > 1)
                projects.Add(planetaryFacilityType2);
            }
            if (empire.DominantRace.ResearchBonus > 0 || empire.GovernmentAttributes.ResearchSpeed > 1.0)
            {
              switch (empire.Policy.ResearchIndustryFocus)
              {
                case IndustryType.Weapon:
                  if (projectForWonderType7 != null)
                  {
                    PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.EmpireResearchWeapons);
                    if (!galaxy.CheckWonderBuilt(wonderByType))
                    {
                      projects.Add(projectForWonderType7);
                      break;
                    }
                    break;
                  }
                  break;
                case IndustryType.Energy:
                  if (projectForWonderType5 != null)
                  {
                    PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.EmpireResearchEnergy);
                    if (!galaxy.CheckWonderBuilt(wonderByType))
                    {
                      projects.Add(projectForWonderType5);
                      break;
                    }
                    break;
                  }
                  break;
                case IndustryType.HighTech:
                  if (projectForWonderType6 != null)
                  {
                    PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.EmpireResearchHighTech);
                    if (!galaxy.CheckWonderBuilt(wonderByType))
                    {
                      projects.Add(projectForWonderType6);
                      break;
                    }
                    break;
                  }
                  break;
              }
            }
            if (empire.DominantRace.SpecialComponent != null)
              projects.Add(this.TechTree.GetHighestProjectForComponent(empire.DominantRace.SpecialComponent));
            if (empire.DominantRace.VictoryConditions != null)
            {
              RaceVictoryCondition victoryConditionByType = empire.DominantRace.VictoryConditions.GetRaceVictoryConditionByType(RaceVictoryConditionType.BuildWonder);
              if (victoryConditionByType != null && victoryConditionByType.AdditionalData is PlanetaryFacilityDefinition)
              {
                PlanetaryFacilityDefinition additionalData = (PlanetaryFacilityDefinition) victoryConditionByType.AdditionalData;
                if (additionalData != null)
                {
                  ResearchNode projectByFacility = this.TechTree.GetProjectByFacility(additionalData);
                  if (projectByFacility != null && !galaxy.CheckWonderBuilt(additionalData))
                    projects.Add(projectByFacility);
                }
              }
            }
          }
          if (empire.GovernmentAttributes.TradeBonus > 1.0 || empire.DominantRace.TradeBonus > 0)
          {
            if (projectForWonderType2 != null)
            {
              PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.ColonyIncome);
              if (!galaxy.CheckWonderBuilt(wonderByType))
                projects.Add(projectForWonderType2);
            }
            if ((empire.GovernmentAttributes.TradeBonus >= 1.15 || empire.DominantRace.TradeBonus >= 15) && projectForWonderType1 != null)
            {
              PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.EmpireIncome);
              if (!galaxy.CheckWonderBuilt(wonderByType))
                projects.Add(projectForWonderType1);
            }
          }
          if (empire.GovernmentAttributes.ApprovalRating > 1.0 || empire.DominantRace.SatisfactionModifier > 0)
          {
            if (projectForWonderType4 != null)
            {
              PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.ColonyHappiness);
              if (!galaxy.CheckWonderBuilt(wonderByType))
                projects.Add(projectForWonderType4);
            }
            if ((empire.GovernmentAttributes.ApprovalRating >= 1.15 || empire.DominantRace.SatisfactionModifier >= 15) && projectForWonderType3 != null)
            {
              PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.EmpireHappiness);
              if (!galaxy.CheckWonderBuilt(wonderByType))
                projects.Add(projectForWonderType3);
            }
          }
        }
      }
      return projects;
    }

    public ResearchNodeList ResolveEssentialProjects(Empire empire, Galaxy galaxy)
    {
      ResearchNodeList researchNodeList = new ResearchNodeList();
      ResearchNode projectForTypeAny1 = this.TechTree.GetLowestProjectForTypeAny(ComponentType.WeaponPointDefense);
      ResearchNode projectForTypeAny2 = this.TechTree.GetLowestProjectForTypeAny(ComponentType.ComputerCountermeasures);
      ResearchNode projectForTypeAny3 = this.TechTree.GetLowestProjectForTypeAny(ComponentType.ComputerTargetting);
      ResearchNode projectForTypeAny4 = this.TechTree.GetLowestProjectForTypeAny(ComponentType.SensorLongRange);
      ResearchNode projectForTypeAny5 = this.TechTree.GetLowestProjectForTypeAny(ComponentType.Armor);
      this.TechTree.GetLowestProjectForTypeAny(ComponentType.AssaultPod);
      this.TechTree.GetLowestProjectForTypeAny(ComponentType.WeaponTractorBeam);
      this.TechTree.GetLowestProjectForTypeAny(ComponentType.SensorScannerJammer);
      this.TechTree.GetLowestProjectForTypeAny(ComponentType.ComputerCountermeasuresFleet);
      this.TechTree.GetLowestProjectForPlanetaryFacilityType(PlanetaryFacilityType.FortifiedBunker);
      this.TechTree.GetLowestProjectForTroopType(TroopType.Artillery);
      this.TechTree.GetLowestProjectForPlanetaryFacilityType(PlanetaryFacilityType.PlanetaryShield);
      this.TechTree.GetHighestProjectForTypeAny(ComponentType.Shields);
      this.TechTree.GetLowestProjectForTypeAny(ComponentType.WeaponBombard);
      this.TechTree.GetLowestProjectForTypeAny(ComponentType.ComputerTargettingFleet);
      ResearchNode planetaryFacilityType = this.TechTree.GetLowestProjectForPlanetaryFacilityType(PlanetaryFacilityType.ArmoredFactory);
      this.TechTree.GetLowestProjectForTroopType(TroopType.SpecialForces);
      this.TechTree.GetHighestProjectForTypeAny(ComponentType.EngineMainThrust);
      this.TechTree.GetHighestProjectForTypeAny(ComponentType.Reactor);
      this.TechTree.GetHighestProjectForTypeAny(ComponentType.HyperDrive);
      this.TechTree.GetLowestProjectForWonderType(WonderType.ColonyConstructionSpeed);
      this.TechTree.GetLowestProjectForDedicatedCarriers();
      ResearchNode projectForWonderType1 = this.TechTree.GetLowestProjectForWonderType(WonderType.EmpireIncome);
      ResearchNode projectForWonderType2 = this.TechTree.GetLowestProjectForWonderType(WonderType.ColonyIncome);
      ResearchNode projectForWonderType3 = this.TechTree.GetLowestProjectForWonderType(WonderType.EmpireHappiness);
      ResearchNode projectForWonderType4 = this.TechTree.GetLowestProjectForWonderType(WonderType.ColonyHappiness);
      this.TechTree.GetHighestProjectForTypeAny(ComponentType.EngineVectoring);
      ResearchNode projectForTypeAny6 = this.TechTree.GetLowestProjectForTypeAny(ComponentType.FighterBay);
      ResearchNode projectForWonderType5 = this.TechTree.GetLowestProjectForWonderType(WonderType.EmpireResearchEnergy);
      ResearchNode projectForWonderType6 = this.TechTree.GetLowestProjectForWonderType(WonderType.EmpireResearchHighTech);
      ResearchNode projectForWonderType7 = this.TechTree.GetLowestProjectForWonderType(WonderType.EmpireResearchWeapons);
      this.TechTree.GetLowestProjectForPlanetaryFacilityType(PlanetaryFacilityType.RegionalCapital);
      if (projectForTypeAny1 != null)
        researchNodeList.Add(projectForTypeAny1);
      if (projectForTypeAny2 != null)
        researchNodeList.Add(projectForTypeAny2);
      if (projectForTypeAny3 != null)
        researchNodeList.Add(projectForTypeAny3);
      if (projectForTypeAny4 != null)
        researchNodeList.Add(projectForTypeAny4);
      if (projectForTypeAny5 != null && !researchNodeList.ContainsById(projectForTypeAny5.ResearchNodeId))
        researchNodeList.Add(projectForTypeAny5);
      if (projectForTypeAny6 != null)
        researchNodeList.Add(projectForTypeAny6);
      if (empire != null)
      {
        if (empire.PirateEmpireBaseHabitat != null)
        {
          researchNodeList.Add(this.TechTree.GetHighestProjectForTypeAny(ComponentType.AssaultPod));
          researchNodeList.Add(this.TechTree.GetHighestProjectForTypeAny(ComponentType.WeaponTractorBeam));
          researchNodeList.Add(this.TechTree.GetHighestProjectForTypeAny(ComponentType.SensorScannerJammer));
        }
        else
        {
          if (planetaryFacilityType != null)
            researchNodeList.Add(planetaryFacilityType);
          if (empire.DominantRace != null)
          {
            if (empire.DominantRace.ResearchBonus > 0 || empire.GovernmentAttributes.ResearchSpeed > 1.0)
            {
              switch (empire.Policy.ResearchIndustryFocus)
              {
                case IndustryType.Weapon:
                  if (projectForWonderType7 != null)
                  {
                    PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.EmpireResearchWeapons);
                    if (!galaxy.CheckWonderBuilt(wonderByType))
                    {
                      researchNodeList.Add(projectForWonderType7);
                      break;
                    }
                    break;
                  }
                  break;
                case IndustryType.Energy:
                  if (projectForWonderType5 != null)
                  {
                    PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.EmpireResearchEnergy);
                    if (!galaxy.CheckWonderBuilt(wonderByType))
                    {
                      researchNodeList.Add(projectForWonderType5);
                      break;
                    }
                    break;
                  }
                  break;
                case IndustryType.HighTech:
                  if (projectForWonderType6 != null)
                  {
                    PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.EmpireResearchHighTech);
                    if (!galaxy.CheckWonderBuilt(wonderByType))
                    {
                      researchNodeList.Add(projectForWonderType6);
                      break;
                    }
                    break;
                  }
                  break;
              }
            }
            if (empire.DominantRace.SpecialComponent != null)
              researchNodeList.Add(this.TechTree.GetHighestProjectForComponent(empire.DominantRace.SpecialComponent));
            if (empire.DominantRace.VictoryConditions != null)
            {
              RaceVictoryCondition victoryConditionByType = empire.DominantRace.VictoryConditions.GetRaceVictoryConditionByType(RaceVictoryConditionType.BuildWonder);
              if (victoryConditionByType != null && victoryConditionByType.AdditionalData is PlanetaryFacilityDefinition)
              {
                PlanetaryFacilityDefinition additionalData = (PlanetaryFacilityDefinition) victoryConditionByType.AdditionalData;
                if (additionalData != null)
                {
                  ResearchNode projectByFacility = this.TechTree.GetProjectByFacility(additionalData);
                  if (projectByFacility != null && !galaxy.CheckWonderBuilt(additionalData))
                    researchNodeList.Add(projectByFacility);
                }
              }
            }
          }
          if (empire.GovernmentAttributes.TradeBonus > 1.0 || empire.DominantRace.TradeBonus > 0)
          {
            if (projectForWonderType2 != null)
            {
              PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.ColonyIncome);
              if (!galaxy.CheckWonderBuilt(wonderByType))
                researchNodeList.Add(projectForWonderType2);
            }
            if ((empire.GovernmentAttributes.TradeBonus >= 1.15 || empire.DominantRace.TradeBonus >= 15) && projectForWonderType1 != null)
            {
              PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.EmpireIncome);
              if (!galaxy.CheckWonderBuilt(wonderByType))
                researchNodeList.Add(projectForWonderType1);
            }
          }
          if (empire.GovernmentAttributes.ApprovalRating > 1.0 || empire.DominantRace.SatisfactionModifier > 0)
          {
            if (projectForWonderType4 != null)
            {
              PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.ColonyHappiness);
              if (!galaxy.CheckWonderBuilt(wonderByType))
                researchNodeList.Add(projectForWonderType4);
            }
            if ((empire.GovernmentAttributes.ApprovalRating >= 1.15 || empire.DominantRace.SatisfactionModifier >= 15) && projectForWonderType3 != null)
            {
              PlanetaryFacilityDefinition wonderByType = galaxy.PlanetaryFacilityDefinitions.FindWonderByType(WonderType.EmpireHappiness);
              if (!galaxy.CheckWonderBuilt(wonderByType))
                researchNodeList.Add(projectForWonderType3);
            }
          }
        }
      }
      return researchNodeList;
    }

    public ResearchNode SelectRandomNextResearchProject(
      Galaxy galaxy,
      ComponentCategoryType category)
    {
      ResearchNode researchNode = (ResearchNode) null;
      ResearchNodeList researchNodeList = new ResearchNodeList();
      if (this.NextProjects != null)
        researchNodeList = this.NextProjects.GetProjectsByCategory(category);
      if (researchNodeList != null && researchNodeList.Count > 0)
      {
        int index = Galaxy.Rnd.Next(0, researchNodeList.Count);
        researchNode = researchNodeList[index];
      }
      return researchNode;
    }

    public ResearchNode SelectRandomNextResearchProject(Galaxy galaxy, IndustryType industry)
    {
      ResearchNode researchNode = (ResearchNode) null;
      ResearchNodeList projectsByIndustry = this.NextProjects.GetProjectsByIndustry(industry);
      if (projectsByIndustry.Count > 0)
      {
        int index = Galaxy.Rnd.Next(0, projectsByIndustry.Count);
        researchNode = projectsByIndustry[index];
      }
      return researchNode;
    }

    public ResearchNode SelectRandomNextResearchProject(Galaxy galaxy)
    {
      ResearchNode researchNode = (ResearchNode) null;
      return this.NextProjects != null && this.NextProjects.Count > 0 ? this.NextProjects[Galaxy.Rnd.Next(0, this.NextProjects.Count)] : researchNode;
    }

    public ResearchNode SelectRandomNextResearchProjectExcludeSuperWeapons(
      Galaxy galaxy,
      ComponentCategoryType category)
    {
      ResearchNode researchNode = (ResearchNode) null;
      ResearchNodeList researchNodeList = new ResearchNodeList();
      if (this.NextProjects != null)
      {
        researchNodeList = this.NextProjects.GetProjectsByCategory(category);
        List<int> researchProjectIds = new List<int>()
        {
          272,
          273,
          274
        };
        researchNodeList.StripProjectsById(researchProjectIds);
      }
      if (researchNodeList != null && researchNodeList.Count > 0)
      {
        int index = Galaxy.Rnd.Next(0, researchNodeList.Count);
        researchNode = researchNodeList[index];
      }
      return researchNode;
    }

    public ResearchNode SelectRandomNextResearchProjectExcludeSuperWeapons(
      Galaxy galaxy,
      IndustryType industry)
    {
      ResearchNode researchNode = (ResearchNode) null;
      ResearchNodeList projectsByIndustry = this.NextProjects.GetProjectsByIndustry(industry);
      List<int> researchProjectIds = new List<int>()
      {
        272,
        273,
        274
      };
      projectsByIndustry.StripProjectsById(researchProjectIds);
      if (projectsByIndustry.Count > 0)
      {
        int index = Galaxy.Rnd.Next(0, projectsByIndustry.Count);
        researchNode = projectsByIndustry[index];
      }
      return researchNode;
    }

    public ResearchNode SelectRandomNextResearchProjectExcludeSuperWeapons(Galaxy galaxy)
    {
      ResearchNode researchNode1 = (ResearchNode) null;
      if (this.NextProjects != null && this.NextProjects.Count > 0)
      {
        ResearchNodeList researchNodeList = this.NextProjects.Clone();
        List<int> researchProjectIds = new List<int>()
        {
          272,
          273,
          274
        };
        researchNodeList.StripProjectsById(researchProjectIds);
        if (researchNodeList.Count > 0)
        {
          int index = Galaxy.Rnd.Next(0, researchNodeList.Count);
          ResearchNode researchNode2 = researchNodeList[index];
          if (researchNode2 != null && this.NextProjects != null)
            return this.NextProjects.FindNodeById(researchNode2.ResearchNodeId);
        }
      }
      return researchNode1;
    }

    public ResearchNode SelectRandomNextResearchProject(
      Galaxy galaxy,
      Empire empire,
      IndustryType industry,
      ResearchNodeList availableProjects)
    {
      ResearchNode researchNode = (ResearchNode) null;
      if (availableProjects != null && availableProjects.Count > 0)
      {
        if (empire != null && empire.Policy != null && empire.Policy.PrioritizeBuildWonderId >= 0)
        {
          for (int index = 0; index < availableProjects.Count; ++index)
          {
            if (availableProjects[index].PlanetaryFacility != null && availableProjects[index].PlanetaryFacility.PlanetaryFacilityDefinitionId == empire.Policy.PrioritizeBuildWonderId)
              return availableProjects[index];
          }
        }
        int index1 = Galaxy.Rnd.Next(0, availableProjects.Count);
        researchNode = availableProjects[index1];
      }
      return researchNode;
    }

    public ResearchNodeList ResolveNextProjects(Galaxy galaxy, Race race, IndustryType industry)
    {
      List<ComponentCategoryType> optimizedDesignCategories = new List<ComponentCategoryType>();
      List<ComponentType> optimizedDesignTypes = new List<ComponentType>();
      return this.ResolveNextProjects(galaxy, race, industry, optimizedDesignCategories, optimizedDesignTypes);
    }

    public ResearchNodeList ResolveNextProjects(
      Galaxy galaxy,
      Race race,
      IndustryType industry,
      List<ComponentCategoryType> optimizedDesignCategories,
      List<ComponentType> optimizedDesignTypes)
    {
      return this.ResolveNextProjects(galaxy, race, industry, optimizedDesignCategories, optimizedDesignTypes, -1);
    }

    public ResearchNodeList ResolveNextProjects(
      Galaxy galaxy,
      Race race,
      IndustryType industry,
      List<ComponentCategoryType> optimizedDesignCategories,
      List<ComponentType> optimizedDesignTypes,
      int maxTechGap)
    {
      ResearchNodeList researchNodeList = new ResearchNodeList();
      int num1 = 0;
      int num2 = int.MaxValue;
      ResearchNode lowestNextProject = this.DetermineLowestNextProject(galaxy, race, industry, optimizedDesignCategories, optimizedDesignTypes);
      if (lowestNextProject != null)
      {
        num1 = lowestNextProject.TechLevel;
        if (maxTechGap > 0)
          num2 = lowestNextProject.TechLevel + maxTechGap;
      }
      if (this.NextProjects != null)
      {
        for (int index = 0; index < this.NextProjects.Count; ++index)
        {
          if (this.NextProjects[index].Industry == industry)
          {
            if (this.NextProjects[index].AllowedRaces != null && this.NextProjects[index].AllowedRaces.Count > 0)
            {
              if (this.NextProjects[index].TechLevel <= num1 + 2)
                researchNodeList.Add(this.NextProjects[index]);
            }
            else if (this.NextProjects[index].TechLevel <= num2 && this.NextProjects[index].Category != ComponentCategoryType.WeaponSuperArea && this.NextProjects[index].Category != ComponentCategoryType.WeaponSuperBeam && this.NextProjects[index].Category != ComponentCategoryType.WeaponSuperTorpedo)
              researchNodeList.Add(this.NextProjects[index]);
          }
        }
      }
      return researchNodeList;
    }

    public static void DetermineRaceSpecialTechCategoryType(
      Race race,
      out ComponentCategoryType category,
      out ComponentType type)
    {
      category = ComponentCategoryType.Undefined;
      type = ComponentType.Undefined;
      if (race == null || race.SpecialComponent == null)
        return;
      ResearchSystem.DetermineTechCategoryType(race.SpecialComponent.Type, out category, out type);
    }

    public static void DetermineAllowedRaceTechCategoryTypes(
      ComponentList raceAllowedComponents,
      out List<ComponentCategoryType> categories,
      out List<ComponentType> types)
    {
      categories = new List<ComponentCategoryType>();
      types = new List<ComponentType>();
      if (raceAllowedComponents == null || raceAllowedComponents.Count <= 0)
        return;
      for (int index = 0; index < raceAllowedComponents.Count; ++index)
      {
        Component allowedComponent = raceAllowedComponents[index];
        if (allowedComponent != null)
        {
          ComponentCategoryType category = ComponentCategoryType.Undefined;
          ComponentType type = ComponentType.Undefined;
          ResearchSystem.DetermineTechCategoryType(allowedComponent.Type, out category, out type);
          if (category != ComponentCategoryType.Undefined && !categories.Contains(category))
            categories.Add(category);
          else if (type != ComponentType.Undefined && !types.Contains(type))
            types.Add(type);
        }
      }
    }

    public static void DetermineTechCategoryType(
      ComponentType componentType,
      out ComponentCategoryType category,
      out ComponentType type)
    {
      type = ComponentType.Undefined;
      category = ComponentCategoryType.Undefined;
      switch (componentType)
      {
        case ComponentType.WeaponBeam:
        case ComponentType.WeaponTorpedo:
        case ComponentType.Shields:
        case ComponentType.HyperDrive:
        case ComponentType.Reactor:
          category = ComponentDefinition.ResolveComponentCategory(componentType);
          break;
        default:
          type = componentType;
          break;
      }
    }

    public static ResearchNodeList RemoveNonRaceSpecificProjectTypes(
      Race race,
      ResearchNodeList techTree,
      ResearchNodeList projects,
      List<ComponentCategoryType> optimizedDesignCategories,
      List<ComponentType> optimizedDesignTypes,
      ComponentList raceAllowedComponents)
    {
      if (race != null && projects != null && (race.SpecialComponent != null || raceAllowedComponents != null && raceAllowedComponents.Count > 0))
      {
        ComponentType type1 = ComponentType.Undefined;
        ComponentCategoryType category1 = ComponentCategoryType.Undefined;
        ResearchSystem.DetermineRaceSpecialTechCategoryType(race, out category1, out type1);
        List<ComponentCategoryType> categories = new List<ComponentCategoryType>();
        List<ComponentType> types = new List<ComponentType>();
        ResearchSystem.DetermineAllowedRaceTechCategoryTypes(raceAllowedComponents, out categories, out types);
        if (type1 != ComponentType.Undefined && !types.Contains(type1))
          types.Add(type1);
        if (category1 != ComponentCategoryType.Undefined && !categories.Contains(category1))
          categories.Add(category1);
        for (int index = 0; index < optimizedDesignTypes.Count; ++index)
        {
          ComponentCategoryType category2 = ComponentCategoryType.Undefined;
          ComponentType type2 = ComponentType.Undefined;
          ResearchSystem.DetermineTechCategoryType(optimizedDesignTypes[index], out category2, out type2);
          if (category2 != ComponentCategoryType.Undefined && !optimizedDesignCategories.Contains(category2))
            optimizedDesignCategories.Add(category2);
        }
        ResearchNodeList researchNodeList = new ResearchNodeList();
        for (int index1 = 0; index1 < projects.Count; ++index1)
        {
          ResearchNode project = projects[index1];
          if (project != null)
          {
            bool flag1 = false;
            if (project.AllowedRaces != null && project.AllowedRaces.Count > 0 && project.AllowedRaces.Contains(race))
              flag1 = true;
            if (!flag1)
            {
              if (types.Count > 0)
              {
                List<ComponentType> types2 = project.ResolveComponentTypesAll();
                if (types2.Count > 0 && Component.TypesIntersect(types, types2) && !Component.TypesIntersect(optimizedDesignTypes, types2))
                {
                  bool flag2 = false;
                  for (int index2 = 0; index2 < types.Count; ++index2)
                  {
                    ComponentType type3 = types[index2];
                    if (type3 != ComponentType.Undefined)
                    {
                      ResearchNode forRaceForTypeAny = techTree.GetLowestUnresearchedProjectForRaceForTypeAny(type3, race);
                      if (forRaceForTypeAny != null)
                      {
                        ResearchNodeList currentPath = techTree.GetCurrentPath(forRaceForTypeAny, race);
                        if (currentPath != null && currentPath.ContainsById(project.ResearchNodeId))
                        {
                          flag2 = true;
                          break;
                        }
                      }
                    }
                  }
                  if (!flag2)
                    researchNodeList.Add(project);
                }
              }
              else if (categories.Count > 0 && categories.Contains(project.Category) && !Component.CategoriesIntersect(optimizedDesignCategories, categories))
              {
                bool flag3 = false;
                for (int index3 = 0; index3 < categories.Count; ++index3)
                {
                  ComponentCategoryType category3 = categories[index3];
                  if (category3 != ComponentCategoryType.Undefined)
                  {
                    ResearchNode forRaceForCategory = techTree.GetLowestUnresearchedProjectForRaceForCategory(category3, race);
                    if (forRaceForCategory != null)
                    {
                      ResearchNodeList currentPath = techTree.GetCurrentPath(forRaceForCategory, race);
                      if (currentPath != null && currentPath.ContainsById(project.ResearchNodeId))
                      {
                        flag3 = true;
                        break;
                      }
                    }
                  }
                }
                if (!flag3)
                  researchNodeList.Add(project);
              }
            }
          }
        }
        for (int index = 0; index < researchNodeList.Count; ++index)
          projects.Remove(researchNodeList[index]);
      }
      return projects;
    }

    public ResearchNode DetermineLowestNextProject(
      Galaxy galaxy,
      Race race,
      IndustryType industry,
      List<ComponentCategoryType> optimizedDesignCategories,
      List<ComponentType> optimizedDesignTypes)
    {
      ResearchNode lowestNextProject = (ResearchNode) null;
      if (this.NextProjects != null && this.NextProjects.Count > 0)
      {
        ResearchNode[] arrayThreadSafe = ListHelper.ToArrayThreadSafe(this.NextProjects);
        ComponentList raceAllowedComponents = galaxy.ResearchNodeDefinitions.ResolveRaceSpecificComponents(race, true);
        ResearchNodeList projects = new ResearchNodeList();
        projects.AddRange((IEnumerable<ResearchNode>) arrayThreadSafe);
        ResearchNodeList researchNodeList = ResearchSystem.RemoveNonRaceSpecificProjectTypes(race, this.TechTree, projects, optimizedDesignCategories, optimizedDesignTypes, raceAllowedComponents);
        if (researchNodeList.Count > 0)
        {
          int num = Galaxy.Rnd.Next(0, researchNodeList.Count);
          for (int index = num; index < researchNodeList.Count; ++index)
          {
            ResearchNode researchNode = researchNodeList[index];
            if (researchNode != null && researchNode.Industry == industry && (lowestNextProject == null || researchNode.TechLevel < lowestNextProject.TechLevel))
              lowestNextProject = researchNode;
          }
          for (int index = 0; index < num; ++index)
          {
            ResearchNode researchNode = researchNodeList[index];
            if (researchNode != null && researchNode.Industry == industry && (lowestNextProject == null || researchNode.TechLevel < lowestNextProject.TechLevel))
              lowestNextProject = researchNode;
          }
        }
      }
      return lowestNextProject;
    }

    private ResearchNodeList DetermineLatestResearchProjects(
      ResearchNodeList techTree,
      Race race,
      out ResearchNodeList nextProjects)
    {
      ResearchNodeList researchNodeList = new ResearchNodeList();
      ResearchNodeList researchProjects = new ResearchNodeList();
      nextProjects = new ResearchNodeList();
      for (int index1 = 0; index1 < techTree.Count; ++index1)
      {
        if (!techTree[index1].IsResearched)
        {
          bool flag1 = true;
          if (techTree[index1].AllowedRaces != null && techTree[index1].AllowedRaces.Count > 0)
          {
            flag1 = false;
            if (race != null && techTree[index1].AllowedRaces.Contains(race))
              flag1 = true;
          }
          if (techTree[index1].DisallowedRaces != null && techTree[index1].DisallowedRaces.Count > 0 && race != null && techTree[index1].DisallowedRaces.Contains(race))
            flag1 = false;
          bool flag2 = true;
          if (flag1)
          {
            researchNodeList.Add(techTree[index1]);
            bool flag3 = false;
            if (techTree[index1].ParentNodes != null && techTree[index1].ParentNodes.Count > 0)
            {
              for (int index2 = 0; index2 < techTree[index1].ParentNodes.Count; ++index2)
              {
                if (techTree[index1].ParentIsRequired[index2] && !techTree[index1].ParentNodes[index2].IsResearched)
                  flag2 = false;
                else if (techTree[index1].ParentNodes[index2].IsResearched)
                {
                  flag3 = true;
                  if (!researchProjects.Contains(techTree[index1].ParentNodes[index2]))
                    researchProjects.Add(techTree[index1].ParentNodes[index2]);
                }
              }
            }
            else
            {
              flag2 = true;
              flag3 = true;
            }
            if (flag2 && flag3 && techTree[index1].IsEnabled && !nextProjects.Contains(techTree[index1]))
              nextProjects.Add(techTree[index1]);
          }
        }
      }
      return researchProjects;
    }

    public FighterSpecification IdentifyLatestFighterSpecification()
    {
      FighterSpecification fighterSpecification = (FighterSpecification) null;
      for (int index = 0; index < this.ResearchedFighters.Count; ++index)
      {
        if (this.ResearchedFighters[index].Type == FighterType.Interceptor && (fighterSpecification == null || this.ResearchedFighters[index].TechLevel > fighterSpecification.TechLevel))
          fighterSpecification = this.ResearchedFighters[index];
      }
      return fighterSpecification;
    }

    public FighterSpecification IdentifyLatestBomberSpecification()
    {
      FighterSpecification fighterSpecification = (FighterSpecification) null;
      for (int index = 0; index < this.ResearchedFighters.Count; ++index)
      {
        if (this.ResearchedFighters[index].Type == FighterType.Bomber && (fighterSpecification == null || this.ResearchedFighters[index].TechLevel > fighterSpecification.TechLevel))
          fighterSpecification = this.ResearchedFighters[index];
      }
      return fighterSpecification;
    }

    private FighterSpecificationList DetermineResearchedFighters()
    {
      FighterSpecificationList researchedFighters = new FighterSpecificationList();
      for (int index1 = 0; index1 < this.TechTree.Count; ++index1)
      {
        if (this.TechTree[index1].IsResearched && this.TechTree[index1].Fighters != null && this.TechTree[index1].Fighters.Count > 0)
        {
          for (int index2 = 0; index2 < this.TechTree[index1].Fighters.Count; ++index2)
            researchedFighters.Add(this.TechTree[index1].Fighters[index2]);
        }
      }
      return researchedFighters;
    }

    private ResearchAbilityList DetermineResearchAbilities()
    {
      ResearchAbilityList researchAbilities = new ResearchAbilityList();
      for (int index1 = 0; index1 < this.TechTree.Count; ++index1)
      {
        if (this.TechTree[index1].IsResearched && this.TechTree[index1].Abilities != null && this.TechTree[index1].Abilities.Count > 0)
        {
          for (int index2 = 0; index2 < this.TechTree[index1].Abilities.Count; ++index2)
            researchAbilities.Add(this.TechTree[index1].Abilities[index2]);
        }
      }
      return researchAbilities;
    }

    private PlagueList ReviewPlagues()
    {
      PlagueList plagueList = new PlagueList();
      for (int index = 0; index < this.TechTree.Count; ++index)
      {
        if (this.TechTree[index].IsResearched && this.TechTree[index].PlagueChange != null)
        {
          Plague plague = Galaxy.PlaguesStatic[(int) this.TechTree[index].PlagueChange.PlagueId];
          if (!plagueList.ContainsById(this.TechTree[index].PlagueChange.PlagueId))
            plagueList.Add(plague);
          if (plague.LatestTechLevelUpdate < this.TechTree[index].TechLevel)
          {
            plague.LatestTechLevelUpdate = this.TechTree[index].TechLevel;
            plague.MortalityRate = this.TechTree[index].PlagueChange.MortalityRate;
            plague.InfectionChance = this.TechTree[index].PlagueChange.InfectionChance;
            plague.Duration = this.TechTree[index].PlagueChange.Duration;
            plague.ExceptionMortalityRate = this.TechTree[index].PlagueChange.ExceptionMortalityRate;
            plague.ExceptionInfectionChance = this.TechTree[index].PlagueChange.ExceptionInfectionChance;
            plague.ExceptionDuration = this.TechTree[index].PlagueChange.ExceptionDuration;
          }
        }
      }
      return plagueList;
    }

    private PlanetaryFacilityDefinitionList DetermineBuildablePlanetaryFacilities()
    {
      PlanetaryFacilityDefinitionList planetaryFacilities = new PlanetaryFacilityDefinitionList();
      for (int index = 0; index < this.TechTree.Count; ++index)
      {
        if (this.TechTree[index].IsResearched && this.TechTree[index].PlanetaryFacility != null && planetaryFacilities.GetById(this.TechTree[index].PlanetaryFacility.PlanetaryFacilityDefinitionId) == null)
          planetaryFacilities.Add(this.TechTree[index].PlanetaryFacility);
      }
      return planetaryFacilities;
    }

    private ComponentList DetermineResearchedComponents(out bool[] researchedComponentState)
    {
      researchedComponentState = new bool[Galaxy.ComponentDefinitionsStatic.Length];
      ComponentList researchedComponents = new ComponentList();
      for (int index1 = 0; index1 < this.TechTree.Count; ++index1)
      {
        if (this.TechTree[index1].IsResearched && this.TechTree[index1].Components != null && this.TechTree[index1].Components.Count > 0)
        {
          for (int index2 = 0; index2 < this.TechTree[index1].Components.Count; ++index2)
          {
            if (!researchedComponents.Contains(this.TechTree[index1].Components[index2]))
            {
              researchedComponents.Add(this.TechTree[index1].Components[index2]);
              researchedComponentState[this.TechTree[index1].Components[index2].ComponentID] = true;
            }
          }
        }
      }
      return researchedComponents;
    }

    private ComponentImprovement[] DetermineComponentImprovements()
    {
      ComponentImprovement[] componentImprovements = new ComponentImprovement[Galaxy.ComponentDefinitionsStatic.Length];
      for (int index1 = 0; index1 < this.TechTree.Count; ++index1)
      {
        if (this.TechTree[index1].IsResearched && this.TechTree[index1].ComponentImprovements != null && this.TechTree[index1].ComponentImprovements.Count > 0)
        {
          for (int index2 = 0; index2 < this.TechTree[index1].ComponentImprovements.Count; ++index2)
          {
            if (this.TechTree[index1].ComponentImprovements[index2].ImprovedComponent != null)
            {
              if (componentImprovements[this.TechTree[index1].ComponentImprovements[index2].ImprovedComponent.ComponentID] == null)
                componentImprovements[this.TechTree[index1].ComponentImprovements[index2].ImprovedComponent.ComponentID] = this.TechTree[index1].ComponentImprovements[index2];
              else if (this.TechTree[index1].ComponentImprovements[index2].TechLevel > componentImprovements[this.TechTree[index1].ComponentImprovements[index2].ImprovedComponent.ComponentID].TechLevel)
                componentImprovements[this.TechTree[index1].ComponentImprovements[index2].ImprovedComponent.ComponentID] = this.TechTree[index1].ComponentImprovements[index2];
            }
          }
        }
      }
      return componentImprovements;
    }

    public static int CalculateMinTechPoints(
      Component component,
      int baseTechCost,
      ResearchNodeDefinitionList techTree)
    {
      int num1 = 100;
      bool flag = false;
      int val1 = 0;
      for (int index1 = 0; index1 < techTree.Count; ++index1)
      {
        if (techTree[index1].TechLevel < 100)
          val1 = Math.Max(val1, techTree[index1].TechLevel);
        if (techTree[index1].Components != null && techTree[index1].Components.Count > 0)
        {
          for (int index2 = 0; index2 < techTree[index1].Components.Count; ++index2)
          {
            if (techTree[index1].Components[index2].ComponentID == component.ComponentID && techTree[index1].TechLevel < num1)
            {
              num1 = techTree[index1].TechLevel;
              flag = false;
              if (techTree[index1].AllowedRaces != null && techTree[index1].AllowedRaces.Count > 0)
                flag = true;
            }
          }
        }
        if (techTree[index1].ComponentImprovements != null && techTree[index1].ComponentImprovements.Count > 0)
        {
          for (int index3 = 0; index3 < techTree[index1].ComponentImprovements.Count; ++index3)
          {
            if (techTree[index1].ComponentImprovements[index3].ImprovedComponent != null && techTree[index1].ComponentImprovements[index3].ImprovedComponent.ComponentID == component.ComponentID && techTree[index1].TechLevel < num1)
            {
              num1 = techTree[index1].TechLevel;
              flag = false;
              if (techTree[index1].AllowedRaces != null && techTree[index1].AllowedRaces.Count > 0)
                flag = true;
            }
          }
        }
      }
      if (num1 >= 100)
        num1 = val1 + 1;
      double minTechPoints = 0.0;
      for (int index = num1; index > 0; --index)
      {
        double num2 = Math.Pow(2.0, (double) (index - 1));
        minTechPoints += num2 * (double) baseTechCost;
      }
      if (flag)
        ++minTechPoints;
      return (int) minTechPoints;
    }

    public static int GetMinTechPoints(Component component) => component != null && ResearchSystem.ComponentMinTechPoints.Length > component.ComponentID ? ResearchSystem.ComponentMinTechPoints[component.ComponentID] : 0;

    public int CalculateMinTechPoints(Component component, Galaxy galaxy)
    {
      int num1 = 0;
      bool flag = false;
      for (int index1 = 0; index1 < this.TechTree.Count; ++index1)
      {
        if (this.TechTree[index1].Components != null && this.TechTree[index1].Components.Count > 0)
        {
          for (int index2 = 0; index2 < this.TechTree[index1].Components.Count; ++index2)
          {
            if (this.TechTree[index1].Components[index2].ComponentID == component.ComponentID && this.TechTree[index1].TechLevel < num1)
            {
              num1 = this.TechTree[index1].TechLevel;
              flag = false;
              if (this.TechTree[index1].AllowedRaces != null && this.TechTree[index1].AllowedRaces.Count > 0)
                flag = true;
            }
          }
        }
        if (this.TechTree[index1].ComponentImprovements != null && this.TechTree[index1].ComponentImprovements.Count > 0)
        {
          for (int index3 = 0; index3 < this.TechTree[index1].ComponentImprovements.Count; ++index3)
          {
            if (this.TechTree[index1].ComponentImprovements[index3].ImprovedComponent != null && this.TechTree[index1].ComponentImprovements[index3].ImprovedComponent.ComponentID == component.ComponentID && this.TechTree[index1].TechLevel < num1)
            {
              num1 = this.TechTree[index1].TechLevel;
              flag = false;
              if (this.TechTree[index1].AllowedRaces != null && this.TechTree[index1].AllowedRaces.Count > 0)
                flag = true;
            }
          }
        }
      }
      double minTechPoints = 1.0;
      for (int index = num1; index > 0; --index)
      {
        double num2 = Math.Pow(2.0, (double) (index - 1));
        minTechPoints += num2 * (double) galaxy.BaseTechCost;
      }
      if (flag)
        ++minTechPoints;
      return (int) minTechPoints;
    }

    public static void CalculateComponentMinMaxTechPoints(
      int baseTechCost,
      ResearchNodeDefinitionList techTree)
    {
      ResearchSystem.ComponentMaxTechPoints = new int[Galaxy.ComponentDefinitionsStatic.Length];
      ResearchSystem.ComponentMinTechPoints = new int[Galaxy.ComponentDefinitionsStatic.Length];
      for (int componentID = 0; componentID < Galaxy.ComponentDefinitionsStatic.Length; ++componentID)
      {
        int maxTechPoints = ResearchSystem.CalculateMaxTechPoints(new Component(componentID), baseTechCost, techTree);
        ResearchSystem.ComponentMaxTechPoints[componentID] = maxTechPoints;
        int minTechPoints = ResearchSystem.CalculateMinTechPoints(new Component(componentID), baseTechCost, techTree);
        ResearchSystem.ComponentMinTechPoints[componentID] = minTechPoints;
      }
    }

    public static int CalculateMaxTechPoints(
      Component component,
      int baseTechCost,
      ResearchNodeDefinitionList techTree)
    {
      int num1 = 0;
      bool flag = false;
      int val1 = 0;
      for (int index1 = 0; index1 < techTree.Count; ++index1)
      {
        if (techTree[index1].TechLevel < 100)
          val1 = Math.Max(val1, techTree[index1].TechLevel);
        if (techTree[index1].Components != null && techTree[index1].Components.Count > 0)
        {
          for (int index2 = 0; index2 < techTree[index1].Components.Count; ++index2)
          {
            if (techTree[index1].Components[index2].ComponentID == component.ComponentID && techTree[index1].TechLevel > num1)
            {
              num1 = techTree[index1].TechLevel;
              flag = false;
              if (techTree[index1].AllowedRaces != null && techTree[index1].AllowedRaces.Count > 0)
                flag = true;
            }
          }
        }
        if (techTree[index1].ComponentImprovements != null && techTree[index1].ComponentImprovements.Count > 0)
        {
          for (int index3 = 0; index3 < techTree[index1].ComponentImprovements.Count; ++index3)
          {
            if (techTree[index1].ComponentImprovements[index3].ImprovedComponent != null && techTree[index1].ComponentImprovements[index3].ImprovedComponent.ComponentID == component.ComponentID && techTree[index1].TechLevel > num1)
            {
              num1 = techTree[index1].TechLevel;
              flag = false;
              if (techTree[index1].AllowedRaces != null && techTree[index1].AllowedRaces.Count > 0)
                flag = true;
            }
          }
        }
      }
      if (num1 >= 100)
        num1 = val1 + 1;
      double maxTechPoints = 0.0;
      for (int index = num1; index > 0; --index)
      {
        double num2 = Math.Pow(2.0, (double) (index - 1));
        maxTechPoints += num2 * (double) baseTechCost;
      }
      if (flag)
        ++maxTechPoints;
      return (int) maxTechPoints;
    }

    public int CalculateCurrentTechPoints(Component component, Galaxy galaxy)
    {
      int num1 = -1;
      bool flag = false;
      if (this.ResearchProjectsPerComponent == null || this.ResearchProjectsPerComponentImprovement == null)
        this.DetermineResearchProjectsPerComponent(this.TechTree);
      ResearchNodeList researchNodeList1 = this.ResearchProjectsPerComponent[component.ComponentID];
      if (researchNodeList1 != null && researchNodeList1.Count > 0)
      {
        for (int index = 0; index < researchNodeList1.Count; ++index)
        {
          ResearchNode researchNode = researchNodeList1[index];
          if (researchNode != null && researchNode.IsResearched && researchNode.TechLevel > num1)
          {
            num1 = researchNode.TechLevel;
            flag = false;
            if (researchNode.AllowedRaces != null && researchNode.AllowedRaces.Count > 0)
              flag = true;
          }
        }
      }
      ResearchNodeList researchNodeList2 = this.ResearchProjectsPerComponentImprovement[component.ComponentID];
      if (researchNodeList2 != null && researchNodeList2.Count > 0)
      {
        for (int index = 0; index < researchNodeList2.Count; ++index)
        {
          ResearchNode researchNode = researchNodeList2[index];
          if (researchNode != null && researchNode.IsResearched && researchNode.TechLevel > num1)
          {
            num1 = researchNode.TechLevel;
            flag = false;
            if (researchNode.AllowedRaces != null && researchNode.AllowedRaces.Count > 0)
              flag = true;
          }
        }
      }
      double currentTechPoints = 1.0;
      for (int index = num1; index > -1; --index)
      {
        double num2 = Math.Pow(2.0, (double) (index - 1));
        currentTechPoints += num2 * (double) galaxy.BaseTechCost;
      }
      if (flag)
        ++currentTechPoints;
      return (int) currentTechPoints;
    }

    private Component[] DetermineBestComponentsByType(ComponentList researchedComponents)
    {
      ComponentType[] values = (ComponentType[]) Enum.GetValues(typeof (ComponentType));
      Component[] componentsByType = new Component[values.Length];
      for (int index = 0; index < values.Length; ++index)
        componentsByType[index] = this.DetermineBestComponent(values[index], researchedComponents);
      return componentsByType;
    }

    private Component[] DetermineBestComponentsByCategory(ComponentList researchedComponents)
    {
      Array values = Enum.GetValues(typeof (ComponentCategoryType));
      Component[] componentsByCategory = new Component[values.Length];
      for (int index = 0; index < values.Length; ++index)
        componentsByCategory[index] = this.DetermineBestComponentBySelectedCategories((ComponentCategoryType) values.GetValue(index), researchedComponents);
      return componentsByCategory;
    }

    private Component[] DetermineLatestComponentsByType(ComponentList researchedComponents)
    {
      Component[] componentsByType = new Component[Enum.GetValues(typeof (ComponentType)).Length];
      for (int index1 = 0; index1 < researchedComponents.Count; ++index1)
      {
        int type1 = (int) researchedComponents[index1].Type;
        if (componentsByType[type1] == null)
        {
          componentsByType[type1] = researchedComponents[index1];
        }
        else
        {
          ComponentImprovement componentImprovement1 = this.ResolveImprovedComponentValues(componentsByType[type1]);
          ComponentImprovement componentImprovement2 = this.ResolveImprovedComponentValues(researchedComponents[index1]);
          if (componentImprovement2.TechLevel > componentImprovement1.TechLevel)
            componentsByType[type1] = researchedComponents[index1];
          else if (componentImprovement2.TechLevel == componentImprovement1.TechLevel)
          {
            Component[] componentArray = componentsByType;
            int index2 = type1;
            int type2 = (int) researchedComponents[index1].Type;
            ComponentList componentList = new ComponentList();
            componentList.Add(componentImprovement2.ImprovedComponent);
            componentList.Add(componentImprovement1.ImprovedComponent);
            ComponentList researchedComponents1 = componentList;
            Component bestComponent = this.DetermineBestComponent((ComponentType) type2, researchedComponents1);
            componentArray[index2] = bestComponent;
          }
        }
      }
      return componentsByType;
    }

    private Component[] DetermineLatestComponentsByCategory(ComponentList researchedComponents)
    {
      Component[] componentsByCategory = new Component[Enum.GetValues(typeof (ComponentCategoryType)).Length];
      for (int index1 = 0; index1 < researchedComponents.Count; ++index1)
      {
        int category1 = (int) researchedComponents[index1].Category;
        if (componentsByCategory[category1] == null)
        {
          if (researchedComponents[index1].Type != ComponentType.WeaponBombard)
            componentsByCategory[category1] = researchedComponents[index1];
        }
        else
        {
          bool flag1 = Galaxy.CheckComponentMatchesCategoryStrict(componentsByCategory[category1].Category, componentsByCategory[category1].Type, componentsByCategory[category1].Category);
          bool flag2 = Galaxy.CheckComponentMatchesCategoryStrict(researchedComponents[index1].Category, researchedComponents[index1].Type, researchedComponents[index1].Category);
          ComponentImprovement componentImprovement1 = this.ResolveImprovedComponentValues(researchedComponents[index1]);
          ComponentImprovement componentImprovement2 = this.ResolveImprovedComponentValues(componentsByCategory[category1]);
          int num = componentImprovement1.TechLevel;
          int techLevel = componentImprovement2.TechLevel;
          if (researchedComponents[index1].Category == ComponentCategoryType.Shields && num == techLevel)
          {
            num = componentImprovement1.Value1;
            techLevel = componentImprovement2.Value1;
          }
          if (researchedComponents[index1].Type == ComponentType.WeaponBombard)
            num = 0;
          if (num > techLevel || !flag1 && flag2)
          {
            if (!flag1 || flag2)
              componentsByCategory[category1] = researchedComponents[index1];
          }
          else if (num == techLevel)
          {
            Component[] componentArray = componentsByCategory;
            int index2 = category1;
            int category2 = (int) researchedComponents[index1].Category;
            ComponentList componentList = new ComponentList();
            componentList.Add(componentImprovement1.ImprovedComponent);
            componentList.Add(componentImprovement2.ImprovedComponent);
            ComponentList researchedComponents1 = componentList;
            Component selectedCategories = this.DetermineBestComponentBySelectedCategories((ComponentCategoryType) category2, researchedComponents1);
            componentArray[index2] = selectedCategories;
          }
        }
      }
      return componentsByCategory;
    }

    public bool CanResearchNode(ResearchNode node)
    {
      if (!node.IsEnabled)
        return false;
      ResearchNodeList researchNodeList = (ResearchNodeList) null;
      switch (node.Industry)
      {
        case IndustryType.Weapon:
          researchNodeList = this.ResearchQueueWeapons;
          break;
        case IndustryType.Energy:
          researchNodeList = this.ResearchQueueEnergy;
          break;
        case IndustryType.HighTech:
          researchNodeList = this.ResearchQueueHighTech;
          break;
      }
      bool flag = false;
      if (node.ParentNodes == null || node.ParentNodes.Count <= 0)
        return true;
      for (int index = 0; index < node.ParentNodes.Count; ++index)
      {
        if (node.ParentIsRequired[index])
        {
          if (!node.ParentNodes[index].IsResearched && !researchNodeList.Contains(node.ParentNodes[index]))
            return false;
          if (researchNodeList.Contains(node.ParentNodes[index]))
            flag = true;
          else if (node.ParentNodes[index].IsResearched)
            flag = true;
        }
        else if (node.ParentNodes[index].IsResearched)
          flag = true;
        else if (researchNodeList.Contains(node.ParentNodes[index]))
          flag = true;
      }
      return flag;
    }

    public ResearchNodeList ResolveResearchPathToNode(ResearchNode researchNode, Race race)
    {
      ResearchNodeList bestPath = (ResearchNodeList) null;
      ResearchNodeList currentPath = (ResearchNodeList) null;
      this.ResolveShortestPathToNode(researchNode, false, ref bestPath, currentPath, race);
      return bestPath;
    }

    private void ResolveShortestPathToNode(
      ResearchNode node,
      bool isRequiredParent,
      ref ResearchNodeList bestPath,
      ResearchNodeList currentPath,
      Race race)
    {
      ResearchNodeList researchNodeList = new ResearchNodeList();
      if (currentPath != null && currentPath.Count > 0)
        researchNodeList.AddRange((IEnumerable<ResearchNode>) currentPath);
      currentPath = researchNodeList;
      bool flag = false;
      switch (node.Industry)
      {
        case IndustryType.Weapon:
          if (this.ResearchQueueWeapons.Contains(node))
          {
            flag = true;
            break;
          }
          break;
        case IndustryType.Energy:
          if (this.ResearchQueueEnergy.Contains(node))
          {
            flag = true;
            break;
          }
          break;
        case IndustryType.HighTech:
          if (this.ResearchQueueHighTech.Contains(node))
          {
            flag = true;
            break;
          }
          break;
      }
      if (!this.CheckNodeForUnresearchedRequiredParents(node) && (flag || node.IsResearched || node.ParentNodes == null || node.ParentNodes.Count <= 0 || !this.CheckNodeValidForRace(node, race)))
      {
        if (!flag && !node.IsResearched && (node.ParentNodes == null || node.ParentNodes.Count <= 0) && this.CheckNodeValidForRace(node, race) && !currentPath.Contains(node))
          currentPath.Add(node);
        if (bestPath != null && bestPath.Count <= currentPath.Count)
          return;
        if (this.CheckNodeValidForRace(node, race))
          bestPath = currentPath;
        currentPath = (ResearchNodeList) null;
      }
      else
      {
        if (!currentPath.Contains(node))
          currentPath.Add(node);
        for (int index = 0; index < node.ParentNodes.Count; ++index)
          this.ResolveShortestPathToNode(node.ParentNodes[index], node.ParentIsRequired[index], ref bestPath, currentPath, race);
      }
    }

    private bool CheckNodeForUnresearchedRequiredParents(ResearchNode node)
    {
      ResearchNodeList requiredParents = new ResearchNodeList();
      if (this.CheckNodeForRequiredParents(node, out requiredParents))
      {
        for (int index = 0; index < requiredParents.Count; ++index)
        {
          if (!requiredParents[index].IsResearched)
            return true;
        }
      }
      return false;
    }

    private bool CheckNodeForRequiredParents(
      ResearchNode node,
      out ResearchNodeList requiredParents)
    {
      requiredParents = new ResearchNodeList();
      if (node.ParentNodes != null && node.ParentNodes.Count > 0)
      {
        for (int index = 0; index < node.ParentNodes.Count; ++index)
        {
          if (node.ParentIsRequired[index])
            requiredParents.Add(node.ParentNodes[index]);
        }
      }
      return requiredParents.Count > 0;
    }

    public bool CheckNodeValidForRace(ResearchNode node, Race race)
    {
      bool flag = true;
      if (node.AllowedRaces != null && node.AllowedRaces.Count > 0)
        flag = race != null && node.AllowedRaces.Contains(race);
      if (node.DisallowedRaces != null && node.DisallowedRaces.Count > 0 && race != null && node.DisallowedRaces.Contains(race))
        flag = false;
      return flag;
    }

    public ComponentImprovement ResolveImprovedComponentValues(Component component) => this.ComponentImprovements[component.ComponentID] == null ? new ComponentImprovement(component) : this.ComponentImprovements[component.ComponentID];

    public ComponentList GetLatestComponents(ComponentType type)
    {
      ComponentList latestComponents = new ComponentList();
      Component latestComponent = this.GetLatestComponent(type);
      int num = 0;
      if (latestComponent != null)
      {
        latestComponents.Add(latestComponent);
        num = latestComponent.TechLevel;
      }
      ComponentImprovement componentImprovement = (ComponentImprovement) null;
      if (latestComponent != null)
      {
        for (int index = 0; index < this.ComponentImprovements.Length; ++index)
        {
          if (this.ComponentImprovements[index] != null && this.ComponentImprovements[index].ImprovedComponent.ComponentID == latestComponent.ComponentID && (componentImprovement == null || this.ComponentImprovements[index].TechLevel > componentImprovement.TechLevel))
            componentImprovement = this.ComponentImprovements[index];
        }
      }
      if (componentImprovement != null && componentImprovement.TechLevel > num)
        num = componentImprovement.TechLevel;
      if (latestComponent != null)
      {
        for (int index = 0; index < this.ComponentImprovements.Length; ++index)
        {
          if (this.ComponentImprovements[index] != null && this.ComponentImprovements[index].ImprovedComponent.Type == latestComponent.Type && this.ComponentImprovements[index].ImprovedComponent.ComponentID != latestComponent.ComponentID && this.ComponentImprovements[index].TechLevel >= num && !latestComponents.Contains(this.ComponentImprovements[index].ImprovedComponent))
            latestComponents.Add(this.ComponentImprovements[index].ImprovedComponent);
        }
      }
      if (this.ResearchedComponents != null && latestComponent != null)
      {
        for (int index = 0; index < this.ResearchedComponents.Count; ++index)
        {
          Component researchedComponent = this.ResearchedComponents[index];
          if (researchedComponent.Type == type && researchedComponent.ComponentID != latestComponent.ComponentID && researchedComponent.TechLevel == latestComponent.TechLevel && !latestComponents.Contains(researchedComponent))
            latestComponents.Add(researchedComponent);
        }
      }
      return latestComponents;
    }

    public ComponentList GetLatestComponents(ComponentCategoryType category)
    {
      ComponentList latestComponents = new ComponentList();
      Component latestComponent = this.GetLatestComponent(category);
      int num = 0;
      if (latestComponent != null)
      {
        latestComponents.Add(latestComponent);
        num = latestComponent.TechLevel;
      }
      ComponentImprovement componentImprovement = (ComponentImprovement) null;
      if (latestComponent != null)
      {
        for (int index = 0; index < this.ComponentImprovements.Length; ++index)
        {
          if (this.ComponentImprovements[index] != null && this.ComponentImprovements[index].ImprovedComponent.ComponentID == latestComponent.ComponentID && (componentImprovement == null || this.ComponentImprovements[index].TechLevel > componentImprovement.TechLevel))
            componentImprovement = this.ComponentImprovements[index];
        }
      }
      if (componentImprovement != null && componentImprovement.TechLevel > num)
        num = componentImprovement.TechLevel;
      if (latestComponent != null)
      {
        for (int index = 0; index < this.ComponentImprovements.Length; ++index)
        {
          if (this.ComponentImprovements[index] != null && this.ComponentImprovements[index].ImprovedComponent.Category == latestComponent.Category && this.ComponentImprovements[index].ImprovedComponent.ComponentID != latestComponent.ComponentID && this.ComponentImprovements[index].TechLevel >= num && !latestComponents.Contains(this.ComponentImprovements[index].ImprovedComponent))
            latestComponents.Add(this.ComponentImprovements[index].ImprovedComponent);
        }
      }
      if (this.ResearchedComponents != null && latestComponent != null)
      {
        for (int index = 0; index < this.ResearchedComponents.Count; ++index)
        {
          Component researchedComponent = this.ResearchedComponents[index];
          if (researchedComponent.Category == category && researchedComponent.ComponentID != latestComponent.ComponentID && researchedComponent.TechLevel == latestComponent.TechLevel && !latestComponents.Contains(researchedComponent))
            latestComponents.Add(researchedComponent);
        }
      }
      return latestComponents;
    }

    public Component GetLatestComponent(ComponentType type)
    {
      int index = (int) type;
      return this._LatestComponentsByType != null && this._LatestComponentsByType.Length > index ? this._LatestComponentsByType[index] : (Component) null;
    }

    public Component GetLatestComponent(ComponentCategoryType category)
    {
      int index = (int) category;
      return this._LatestComponentsByCategory != null && this._LatestComponentsByCategory.Length > index ? this._LatestComponentsByCategory[index] : (Component) null;
    }

    public ResearchNode IdentifyBestProject(
      Galaxy galaxy,
      Empire empire,
      IndustryType industry,
      ResearchNode proposedProject,
      ResearchNodeList levelLimitedProjects,
      ResearchNodeList allNextProjects,
      ShipDesignFocus designFocus,
      int maxTechGap)
    {
      if (proposedProject == null)
        return (ResearchNode) null;
      switch (proposedProject.Category)
      {
        case ComponentCategoryType.WeaponBeam:
        case ComponentCategoryType.WeaponArea:
        case ComponentCategoryType.Shields:
        case ComponentCategoryType.Engine:
        case ComponentCategoryType.HyperDrive:
        case ComponentCategoryType.Reactor:
          ResearchNode researchNode1 = this.SelectBestProject(proposedProject, levelLimitedProjects, designFocus);
          ResearchNode researchNode2 = this.SelectBestProject(proposedProject, allNextProjects, designFocus);
          if (researchNode1 == researchNode2)
            return researchNode1;
          int num1 = 0;
          int num2 = 0;
          if (researchNode2 != null)
            num1 = researchNode2.TechLevel;
          if (researchNode1 != null)
            num2 = researchNode1.TechLevel;
          if (num1 - num2 <= maxTechGap)
            return researchNode2;
          int num3 = 0;
          for (ComponentCategoryType category = proposedProject.Category; proposedProject.Category == category && num3 < 20; ++num3)
            proposedProject = this.SelectRandomNextResearchProject(galaxy, empire, industry, levelLimitedProjects);
          return this.SelectBestProject(proposedProject, levelLimitedProjects, designFocus);
        case ComponentCategoryType.WeaponTorpedo:
          bool flag = false;
          if (proposedProject != null && proposedProject.Components != null && proposedProject.Components.Count > 0)
          {
            for (int index = 0; index < proposedProject.Components.Count; ++index)
            {
              if (proposedProject.Components[index].Type == ComponentType.WeaponMissile)
              {
                flag = true;
                break;
              }
            }
          }
          if (!flag && proposedProject != null && proposedProject.ComponentImprovements != null && proposedProject.ComponentImprovements.Count > 0)
          {
            for (int index = 0; index < proposedProject.ComponentImprovements.Count; ++index)
            {
              if (proposedProject.ComponentImprovements[index].ImprovedComponent.Type == ComponentType.WeaponMissile)
              {
                flag = true;
                break;
              }
            }
          }
          if (flag && (proposedProject != null && (proposedProject.ParentNodes == null || proposedProject.ParentNodes.Count <= 0) || designFocus == ShipDesignFocus.SpeedAgility))
            return proposedProject;
          ResearchNode researchNode3 = this.SelectBestProject(proposedProject, levelLimitedProjects, designFocus);
          ResearchNode researchNode4 = this.SelectBestProject(proposedProject, allNextProjects, designFocus);
          if (researchNode3 == researchNode4)
            return researchNode3;
          int num4 = 0;
          int num5 = 0;
          if (researchNode4 != null)
            num4 = researchNode4.TechLevel;
          if (researchNode3 != null)
            num5 = researchNode3.TechLevel;
          if (num4 - num5 <= maxTechGap)
            return researchNode4;
          int num6 = 0;
          for (ComponentCategoryType category = proposedProject.Category; proposedProject.Category == category && num6 < 20; ++num6)
            proposedProject = this.SelectRandomNextResearchProject(galaxy, empire, industry, levelLimitedProjects);
          return this.SelectBestProject(proposedProject, levelLimitedProjects, designFocus);
        default:
          return proposedProject;
      }
    }

    private ResearchNode SelectBestProject(
      ResearchNode proposedProject,
      ResearchNodeList availableProjects,
      ShipDesignFocus designFocus)
    {
      List<ComponentType> types1 = proposedProject.ResolveComponentTypesAll();
      ResearchNodeList researchNodeList = new ResearchNodeList();
      if (proposedProject != null && designFocus == ShipDesignFocus.Balanced)
      {
        researchNodeList.Add(proposedProject);
        proposedProject.SortTag = 1f;
      }
      else
      {
        for (int index = 0; index < availableProjects.Count; ++index)
        {
          List<ComponentType> types2 = availableProjects[index].ResolveComponentTypesAll();
          if (types2.Count <= 0)
          {
            if (availableProjects[index].Category == proposedProject.Category)
            {
              researchNodeList.Add(availableProjects[index]);
              float orderedValue = this.CalculateOrderedValue(availableProjects[index], designFocus);
              availableProjects[index].SortTag = orderedValue;
            }
          }
          else if (Component.TypesIntersect(types1, types2))
          {
            researchNodeList.Add(availableProjects[index]);
            float orderedValueByType = this.CalculateOrderedValueByType(availableProjects[index], designFocus);
            availableProjects[index].SortTag = orderedValueByType;
          }
        }
      }
      if (researchNodeList.Count <= 0)
        return proposedProject;
      researchNodeList.Sort();
      researchNodeList.Reverse();
      return researchNodeList[0];
    }

    private void IdentifyLaggingProjectAndAdd(
      PlanetaryFacilityType type,
      int highestIndustryTechLevel,
      int maximumAllowableTechLevelGap,
      ref ResearchNodeList projects)
    {
      ResearchNode researchNode = this.IdentifyLaggingProject(type, highestIndustryTechLevel, maximumAllowableTechLevelGap);
      if (researchNode == null || projects == null)
        return;
      projects.Add(researchNode);
    }

    private ResearchNode IdentifyLaggingProject(
      PlanetaryFacilityType type,
      int highestIndustryTechLevel,
      int maximumAllowableTechLevelGap)
    {
      ResearchNode planetaryFacilityType = this.TechTree.GetLowestUnresearchedProjectForPlanetaryFacilityType(type);
      return planetaryFacilityType != null && highestIndustryTechLevel - planetaryFacilityType.TechLevel > maximumAllowableTechLevelGap ? planetaryFacilityType : (ResearchNode) null;
    }

    private void IdentifyLaggingProjectAndAdd(
      WonderType type,
      int highestIndustryTechLevel,
      int maximumAllowableTechLevelGap,
      ref ResearchNodeList projects)
    {
      ResearchNode researchNode = this.IdentifyLaggingProject(type, highestIndustryTechLevel, maximumAllowableTechLevelGap);
      if (researchNode == null || projects == null)
        return;
      projects.Add(researchNode);
    }

    private ResearchNode IdentifyLaggingProject(
      WonderType type,
      int highestIndustryTechLevel,
      int maximumAllowableTechLevelGap)
    {
      ResearchNode projectForWonderType = this.TechTree.GetLowestUnresearchedProjectForWonderType(type);
      return projectForWonderType != null && highestIndustryTechLevel - projectForWonderType.TechLevel > maximumAllowableTechLevelGap ? projectForWonderType : (ResearchNode) null;
    }

    private void IdentifyLaggingProjectAndAdd(
      TroopType type,
      int highestIndustryTechLevel,
      int maximumAllowableTechLevelGap,
      ref ResearchNodeList projects)
    {
      ResearchNode researchNode = this.IdentifyLaggingProject(type, highestIndustryTechLevel, maximumAllowableTechLevelGap);
      if (researchNode == null || projects == null)
        return;
      projects.Add(researchNode);
    }

    private ResearchNode IdentifyLaggingProject(
      TroopType type,
      int highestIndustryTechLevel,
      int maximumAllowableTechLevelGap)
    {
      ResearchNode projectForTroopType = this.TechTree.GetLowestUnresearchedProjectForTroopType(type);
      return projectForTroopType != null && highestIndustryTechLevel - projectForTroopType.TechLevel > maximumAllowableTechLevelGap ? projectForTroopType : (ResearchNode) null;
    }

    private void IdentifyLaggingProjectAndAdd(
      ComponentType type,
      Race race,
      int highestIndustryTechLevel,
      int maximumAllowableTechLevelGap,
      ShipDesignFocus focus,
      ResearchNodeList industryNextProjects,
      ref ResearchNodeList projects)
    {
      ResearchNode researchNode = this.IdentifyLaggingProject(type, race, highestIndustryTechLevel, maximumAllowableTechLevelGap, industryNextProjects, focus);
      if (researchNode == null || projects == null)
        return;
      projects.Add(researchNode);
    }

    private ResearchNode IdentifyLaggingProject(
      ComponentType type,
      Race race,
      int highestIndustryTechLevel,
      int maximumAllowableTechLevelGap,
      ResearchNodeList industryNextProjects,
      ShipDesignFocus focus)
    {
      ResearchNode projectForTypeAny = this.TechTree.GetLowestUnresearchedProjectForTypeAny(type, race);
      if (projectForTypeAny != null && industryNextProjects.ContainsById(projectForTypeAny.ResearchNodeId) && highestIndustryTechLevel - projectForTypeAny.TechLevel >= maximumAllowableTechLevelGap)
      {
        ResearchNode researchNode = this.IdentifyBestNextProject(type, focus, industryNextProjects);
        if (researchNode != null && !researchNode.IsResearched)
          return researchNode;
      }
      return (ResearchNode) null;
    }

    private ResearchNode IdentifyBestNextProject(
      ComponentType type,
      ShipDesignFocus designFocus,
      ResearchNodeList industryNextProjects)
    {
      ResearchNodeList projectsByType = industryNextProjects.GetProjectsByType(type);
      return this.SelectBestProject(type, projectsByType, designFocus);
    }

    private ResearchNode SelectBestProject(
      ComponentType type,
      ResearchNodeList availableProjects,
      ShipDesignFocus designFocus)
    {
      ResearchNodeList researchNodeList = new ResearchNodeList();
      for (int index = 0; index < availableProjects.Count; ++index)
      {
        List<ComponentType> componentTypeList = availableProjects[index].ResolveComponentTypesAll();
        if (componentTypeList.Count >= 0 && componentTypeList.Contains(type))
        {
          researchNodeList.Add(availableProjects[index]);
          float orderedValueByType = this.CalculateOrderedValueByType(availableProjects[index], designFocus);
          availableProjects[index].SortTag = orderedValueByType;
        }
      }
      if (researchNodeList.Count <= 0)
        return (ResearchNode) null;
      researchNodeList.Sort();
      researchNodeList.Reverse();
      return researchNodeList[0];
    }

    private float CalculateOrderedValueByType(
      ResearchNode researchProject,
      ShipDesignFocus designFocus)
    {
      float orderedValueByType = 0.0f;
      ComponentImprovement componentImprovement = (ComponentImprovement) null;
      if (researchProject.Components != null && researchProject.Components.Count > 0)
        componentImprovement = new ComponentImprovement(researchProject.Components[0]);
      else if (researchProject.ComponentImprovements != null && researchProject.ComponentImprovements.Count > 0)
        componentImprovement = researchProject.ComponentImprovements[0];
      if (componentImprovement != null)
      {
        switch (designFocus)
        {
          case ShipDesignFocus.Balanced:
            switch (componentImprovement.ImprovedComponent.Type)
            {
              case ComponentType.WeaponBeam:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponTorpedo:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponMissile:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponIonCannon:
              case ComponentType.WeaponIonPulse:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponAreaDestruction:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.Shields:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.EngineMainThrust:
              case ComponentType.EngineVectoring:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.HyperDrive:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.Reactor:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponPhaser:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponRailGun:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
            }
            break;
          case ShipDesignFocus.SpeedAgility:
            switch (componentImprovement.ImprovedComponent.Type)
            {
              case ComponentType.WeaponBeam:
                orderedValueByType = (float) componentImprovement.Value2;
                break;
              case ComponentType.WeaponTorpedo:
                orderedValueByType = (float) componentImprovement.Value2;
                break;
              case ComponentType.WeaponMissile:
                orderedValueByType = (float) componentImprovement.Value2;
                break;
              case ComponentType.WeaponIonCannon:
              case ComponentType.WeaponIonPulse:
                orderedValueByType = (float) componentImprovement.Value2;
                break;
              case ComponentType.WeaponAreaDestruction:
                orderedValueByType = (float) componentImprovement.Value2;
                break;
              case ComponentType.Shields:
                orderedValueByType = (float) componentImprovement.Value2;
                break;
              case ComponentType.EngineMainThrust:
              case ComponentType.EngineVectoring:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.HyperDrive:
                orderedValueByType = 100f / (float) componentImprovement.Value3;
                break;
              case ComponentType.Reactor:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponPhaser:
                orderedValueByType = (float) componentImprovement.Value2;
                break;
              case ComponentType.WeaponRailGun:
                orderedValueByType = (float) componentImprovement.Value2;
                break;
            }
            break;
          case ShipDesignFocus.Power:
            switch (componentImprovement.ImprovedComponent.Type)
            {
              case ComponentType.WeaponBeam:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponTorpedo:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponMissile:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponIonCannon:
              case ComponentType.WeaponIonPulse:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponAreaDestruction:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.Shields:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.EngineMainThrust:
              case ComponentType.EngineVectoring:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.HyperDrive:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.Reactor:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponPhaser:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponRailGun:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
            }
            break;
          case ShipDesignFocus.Efficiency:
            switch (componentImprovement.ImprovedComponent.Type)
            {
              case ComponentType.WeaponBeam:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponTorpedo:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponMissile:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponIonCannon:
              case ComponentType.WeaponIonPulse:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponAreaDestruction:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.Shields:
                orderedValueByType = (float) componentImprovement.Value2;
                break;
              case ComponentType.EngineMainThrust:
              case ComponentType.EngineVectoring:
                orderedValueByType = (float) (100.0 / ((double) componentImprovement.Value1 / (double) componentImprovement.Value2));
                break;
              case ComponentType.HyperDrive:
                orderedValueByType = (float) (100.0 / ((double) componentImprovement.Value1 / (double) componentImprovement.Value2));
                break;
              case ComponentType.Reactor:
                orderedValueByType = (float) (100.0 / ((double) componentImprovement.Value3 / (double) componentImprovement.Value2));
                break;
              case ComponentType.WeaponPhaser:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
              case ComponentType.WeaponRailGun:
                orderedValueByType = (float) componentImprovement.Value1;
                break;
            }
            break;
        }
      }
      return orderedValueByType;
    }

    private float CalculateOrderedValue(ResearchNode researchProject, ShipDesignFocus designFocus)
    {
      float orderedValue = 0.0f;
      ComponentImprovement componentImprovement = (ComponentImprovement) null;
      if (researchProject.Components != null && researchProject.Components.Count > 0)
        componentImprovement = new ComponentImprovement(researchProject.Components[0]);
      else if (researchProject.ComponentImprovements != null && researchProject.ComponentImprovements.Count > 0)
        componentImprovement = researchProject.ComponentImprovements[0];
      if (componentImprovement != null)
      {
        switch (designFocus)
        {
          case ShipDesignFocus.Balanced:
            switch (componentImprovement.ImprovedComponent.Category)
            {
              case ComponentCategoryType.WeaponBeam:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.WeaponTorpedo:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.WeaponArea:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.Shields:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.Engine:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.HyperDrive:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.Reactor:
                orderedValue = (float) componentImprovement.Value1;
                break;
            }
            break;
          case ShipDesignFocus.SpeedAgility:
            switch (componentImprovement.ImprovedComponent.Category)
            {
              case ComponentCategoryType.WeaponBeam:
                orderedValue = (float) componentImprovement.Value2;
                break;
              case ComponentCategoryType.WeaponTorpedo:
                orderedValue = (float) componentImprovement.Value2;
                break;
              case ComponentCategoryType.WeaponArea:
                orderedValue = (float) componentImprovement.Value2;
                break;
              case ComponentCategoryType.Shields:
                orderedValue = (float) componentImprovement.Value2;
                break;
              case ComponentCategoryType.Engine:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.HyperDrive:
                orderedValue = 100f / (float) componentImprovement.Value3;
                break;
              case ComponentCategoryType.Reactor:
                orderedValue = (float) componentImprovement.Value1;
                break;
            }
            break;
          case ShipDesignFocus.Power:
            switch (componentImprovement.ImprovedComponent.Category)
            {
              case ComponentCategoryType.WeaponBeam:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.WeaponTorpedo:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.WeaponArea:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.Shields:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.Engine:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.HyperDrive:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.Reactor:
                orderedValue = (float) componentImprovement.Value1;
                break;
            }
            break;
          case ShipDesignFocus.Efficiency:
            switch (componentImprovement.ImprovedComponent.Category)
            {
              case ComponentCategoryType.WeaponBeam:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.WeaponTorpedo:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.WeaponArea:
                orderedValue = (float) componentImprovement.Value1;
                break;
              case ComponentCategoryType.Shields:
                orderedValue = (float) componentImprovement.Value2;
                break;
              case ComponentCategoryType.Engine:
                orderedValue = (float) (100.0 / ((double) componentImprovement.Value1 / (double) componentImprovement.Value2));
                break;
              case ComponentCategoryType.HyperDrive:
                orderedValue = (float) (100.0 / ((double) componentImprovement.Value1 / (double) componentImprovement.Value2));
                break;
              case ComponentCategoryType.Reactor:
                orderedValue = (float) (100.0 / ((double) componentImprovement.Value3 / (double) componentImprovement.Value2));
                break;
            }
            break;
        }
      }
      return orderedValue;
    }

    public ComponentImprovement EvaluateDesiredComponentImprovement(
      ComponentType componentType,
      ShipDesignFocus designFocus)
    {
      Component desiredComponent = this.EvaluateDesiredComponent(componentType, designFocus);
      ComponentImprovement componentImprovement = (ComponentImprovement) null;
      if (desiredComponent != null)
        componentImprovement = this.ResolveImprovedComponentValues(desiredComponent);
      return componentImprovement;
    }

    public Component DetermineBestComponent(ComponentType type, ComponentList researchedComponents)
    {
      ComponentList byType = researchedComponents.GetByType(type);
      switch (type)
      {
        case ComponentType.WeaponBeam:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponTorpedo:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponBombard:
          return this.IdentifyComponentHighestValue7(byType);
        case ComponentType.WeaponMissile:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponPointDefense:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponIonCannon:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponIonPulse:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponIonDefense:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponTractorBeam:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponGravityBeam:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponAreaGravity:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.AssaultPod:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.HyperDeny:
          return this.IdentifyComponentHighestValue2(byType);
        case ComponentType.HyperStop:
          return this.IdentifyComponentHighestValue2(byType);
        case ComponentType.WeaponAreaDestruction:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponSuperBeam:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponSuperArea:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.FighterBay:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.Armor:
          return this.IdentifyComponentHighestValue2(byType);
        case ComponentType.Shields:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.ShieldRecharge:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.EngineMainThrust:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.EngineVectoring:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.HyperDrive:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.Reactor:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.EnergyCollector:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.ExtractorMine:
        case ComponentType.ExtractorGasExtractor:
        case ComponentType.ExtractorLuxury:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.ManufacturerWeaponsPlant:
        case ComponentType.ManufacturerEnergyPlant:
        case ComponentType.ManufacturerHighTechPlant:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.StorageFuel:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.StorageCargo:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.StorageTroop:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.StoragePassenger:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.StorageDockingBay:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.SensorProximityArray:
          return this.IdentifyComponentHighestValue2(byType);
        case ComponentType.SensorResourceProfileSensor:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.SensorLongRange:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.SensorTraceScanner:
          return this.IdentifyComponentHighestValue2(byType);
        case ComponentType.SensorScannerJammer:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.SensorStealth:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.ComputerTargetting:
        case ComponentType.ComputerTargettingFleet:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.ComputerCountermeasures:
        case ComponentType.ComputerCountermeasuresFleet:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.ComputerCommandCenter:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.ComputerCommerceCenter:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.LabsWeaponsLab:
        case ComponentType.LabsEnergyLab:
        case ComponentType.LabsHighTechLab:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.ConstructionBuild:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.HabitationLifeSupport:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.HabitationHabModule:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.DamageControl:
          return this.IdentifyComponentLowestValue2WithMinimumThreshold(byType, 1) ?? this.IdentifyComponentHighestValue1(byType);
        case ComponentType.HabitationMedicalCenter:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.HabitationRecreationCenter:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.HabitationColonization:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponPhaser:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponRailGun:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.EnergyToFuel:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponSuperTorpedo:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponSuperMissile:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponSuperPhaser:
          return this.IdentifyComponentHighestValue1(byType);
        case ComponentType.WeaponSuperRailGun:
          return this.IdentifyComponentHighestValue1(byType);
        default:
          return (Component) null;
      }
    }

    public Component DetermineBestComponentBySelectedCategories(
      ComponentCategoryType category,
      ComponentList researchedComponents)
    {
      ComponentType type = ComponentType.Undefined;
      switch (category)
      {
        case ComponentCategoryType.WeaponBeam:
          type = ComponentType.WeaponBeam;
          break;
        case ComponentCategoryType.WeaponTorpedo:
          type = ComponentType.WeaponTorpedo;
          break;
        case ComponentCategoryType.WeaponPointDefense:
          type = ComponentType.WeaponPointDefense;
          break;
        case ComponentCategoryType.AssaultPod:
          type = ComponentType.AssaultPod;
          break;
        case ComponentCategoryType.Shields:
          type = ComponentType.Shields;
          break;
        case ComponentCategoryType.ShieldRecharge:
          type = ComponentType.ShieldRecharge;
          break;
        case ComponentCategoryType.HyperDrive:
          type = ComponentType.HyperDrive;
          break;
        case ComponentCategoryType.Reactor:
          type = ComponentType.Reactor;
          break;
        case ComponentCategoryType.WeaponSuperBeam:
          type = ComponentType.WeaponSuperBeam;
          break;
        case ComponentCategoryType.WeaponSuperTorpedo:
          type = ComponentType.WeaponSuperTorpedo;
          break;
      }
      return this.DetermineBestComponent(type, researchedComponents);
    }

    public Component IdentifyComponentHighestValue1(ComponentList components)
    {
      ComponentImprovement componentImprovement1 = (ComponentImprovement) null;
      for (int index = 0; index < components.Count; ++index)
      {
        ComponentImprovement componentImprovement2 = this.ResolveImprovedComponentValues(components[index]);
        if (componentImprovement2 != null && (componentImprovement1 == null || componentImprovement2.Value1 > componentImprovement1.Value1 || componentImprovement2.Value1 == componentImprovement1.Value1 && componentImprovement2.ImprovedComponent != null && componentImprovement1.ImprovedComponent != null && componentImprovement2.ImprovedComponent.Size < componentImprovement1.ImprovedComponent.Size))
          componentImprovement1 = componentImprovement2;
      }
      return componentImprovement1?.ImprovedComponent;
    }

    public Component IdentifyComponentLowestValue1(ComponentList components)
    {
      ComponentImprovement componentImprovement1 = (ComponentImprovement) null;
      for (int index = 0; index < components.Count; ++index)
      {
        ComponentImprovement componentImprovement2 = this.ResolveImprovedComponentValues(components[index]);
        if (componentImprovement2 != null && (componentImprovement1 == null || componentImprovement2.Value1 < componentImprovement1.Value1 || componentImprovement2.Value1 == componentImprovement1.Value1 && componentImprovement2.ImprovedComponent != null && componentImprovement1.ImprovedComponent != null && componentImprovement2.ImprovedComponent.Size < componentImprovement1.ImprovedComponent.Size))
          componentImprovement1 = componentImprovement2;
      }
      return componentImprovement1?.ImprovedComponent;
    }

    public Component IdentifyComponentHighestValue2(ComponentList components)
    {
      ComponentImprovement componentImprovement1 = (ComponentImprovement) null;
      for (int index = 0; index < components.Count; ++index)
      {
        ComponentImprovement componentImprovement2 = this.ResolveImprovedComponentValues(components[index]);
        if (componentImprovement2 != null && (componentImprovement1 == null || componentImprovement2.Value2 > componentImprovement1.Value2 || componentImprovement2.Value2 == componentImprovement1.Value2 && componentImprovement2.ImprovedComponent != null && componentImprovement1.ImprovedComponent != null && componentImprovement2.ImprovedComponent.Size < componentImprovement1.ImprovedComponent.Size))
          componentImprovement1 = componentImprovement2;
      }
      return componentImprovement1?.ImprovedComponent;
    }

    public Component IdentifyComponentLowestValue2(ComponentList components) => this.IdentifyComponentLowestValue2WithMinimumThreshold(components, int.MinValue);

    public Component IdentifyComponentLowestValue2WithMinimumThreshold(
      ComponentList components,
      int minimumThreshold)
    {
      ComponentImprovement componentImprovement1 = (ComponentImprovement) null;
      for (int index = 0; index < components.Count; ++index)
      {
        ComponentImprovement componentImprovement2 = this.ResolveImprovedComponentValues(components[index]);
        if (componentImprovement1 == null && componentImprovement2 != null && componentImprovement2.Value2 >= minimumThreshold)
          componentImprovement1 = componentImprovement2;
        else if (componentImprovement1 != null && componentImprovement2 != null && (componentImprovement2.Value2 < componentImprovement1.Value2 && componentImprovement2.Value2 >= minimumThreshold || componentImprovement2.Value2 == componentImprovement1.Value2 && componentImprovement2.Value2 >= minimumThreshold && componentImprovement2.ImprovedComponent != null && componentImprovement1.ImprovedComponent != null && componentImprovement2.ImprovedComponent.Size < componentImprovement1.ImprovedComponent.Size))
          componentImprovement1 = componentImprovement2;
      }
      return componentImprovement1?.ImprovedComponent;
    }

    public Component IdentifyComponentHighestValue3(ComponentList components)
    {
      ComponentImprovement componentImprovement1 = (ComponentImprovement) null;
      for (int index = 0; index < components.Count; ++index)
      {
        ComponentImprovement componentImprovement2 = this.ResolveImprovedComponentValues(components[index]);
        if (componentImprovement2 != null && (componentImprovement1 == null || componentImprovement2.Value3 > componentImprovement1.Value3 || componentImprovement2.Value3 == componentImprovement1.Value3 && componentImprovement2.ImprovedComponent != null && componentImprovement1.ImprovedComponent != null && componentImprovement2.ImprovedComponent.Size < componentImprovement1.ImprovedComponent.Size))
          componentImprovement1 = componentImprovement2;
      }
      return componentImprovement1?.ImprovedComponent;
    }

    public Component IdentifyComponentHighestValue4(ComponentList components)
    {
      ComponentImprovement componentImprovement1 = (ComponentImprovement) null;
      for (int index = 0; index < components.Count; ++index)
      {
        ComponentImprovement componentImprovement2 = this.ResolveImprovedComponentValues(components[index]);
        if (componentImprovement2 != null && (componentImprovement1 == null || componentImprovement2.Value4 > componentImprovement1.Value4 || componentImprovement2.Value4 == componentImprovement1.Value4 && componentImprovement2.ImprovedComponent != null && componentImprovement1.ImprovedComponent != null && componentImprovement2.ImprovedComponent.Size < componentImprovement1.ImprovedComponent.Size))
          componentImprovement1 = componentImprovement2;
      }
      return componentImprovement1?.ImprovedComponent;
    }

    public Component IdentifyComponentHighestValue5(ComponentList components)
    {
      ComponentImprovement componentImprovement1 = (ComponentImprovement) null;
      for (int index = 0; index < components.Count; ++index)
      {
        ComponentImprovement componentImprovement2 = this.ResolveImprovedComponentValues(components[index]);
        if (componentImprovement2 != null && (componentImprovement1 == null || componentImprovement2.Value5 > componentImprovement1.Value5 || componentImprovement2.Value5 == componentImprovement1.Value5 && componentImprovement2.ImprovedComponent != null && componentImprovement1.ImprovedComponent != null && componentImprovement2.ImprovedComponent.Size < componentImprovement1.ImprovedComponent.Size))
          componentImprovement1 = componentImprovement2;
      }
      return componentImprovement1?.ImprovedComponent;
    }

    public Component IdentifyComponentHighestValue6(ComponentList components)
    {
      ComponentImprovement componentImprovement1 = (ComponentImprovement) null;
      for (int index = 0; index < components.Count; ++index)
      {
        ComponentImprovement componentImprovement2 = this.ResolveImprovedComponentValues(components[index]);
        if (componentImprovement2 != null && (componentImprovement1 == null || componentImprovement2.Value6 > componentImprovement1.Value6 || componentImprovement2.Value6 == componentImprovement1.Value6 && componentImprovement2.ImprovedComponent != null && componentImprovement1.ImprovedComponent != null && componentImprovement2.ImprovedComponent.Size < componentImprovement1.ImprovedComponent.Size))
          componentImprovement1 = componentImprovement2;
      }
      return componentImprovement1?.ImprovedComponent;
    }

    public Component IdentifyComponentHighestValue7(ComponentList components)
    {
      ComponentImprovement componentImprovement1 = (ComponentImprovement) null;
      for (int index = 0; index < components.Count; ++index)
      {
        ComponentImprovement componentImprovement2 = this.ResolveImprovedComponentValues(components[index]);
        if (componentImprovement2 != null && (componentImprovement1 == null || componentImprovement2.Value7 > componentImprovement1.Value7 || componentImprovement2.Value7 == componentImprovement1.Value7 && componentImprovement2.ImprovedComponent != null && componentImprovement1.ImprovedComponent != null && componentImprovement2.ImprovedComponent.Size < componentImprovement1.ImprovedComponent.Size))
          componentImprovement1 = componentImprovement2;
      }
      return componentImprovement1?.ImprovedComponent;
    }

    public Component EvaluateDesiredComponent(
      ComponentType componentType,
      ShipDesignFocus designFocus)
    {
      return this.EvaluateDesiredComponent(componentType, designFocus, false);
    }

    public Component EvaluateDesiredComponent(
      ComponentType componentType,
      ShipDesignFocus designFocus,
      bool preferLatest)
    {
      int index = (int) componentType;
      switch (designFocus)
      {
        case ShipDesignFocus.Balanced:
          return preferLatest ? this._LatestComponentsByType[index] : this._BestComponentsByType[index];
        case ShipDesignFocus.SpeedAgility:
          switch (componentType)
          {
            case ComponentType.WeaponBeam:
              return this.IdentifyBestComponent(this.ComponentsWeaponBeamOrderedByRange);
            case ComponentType.WeaponTorpedo:
              return this.IdentifyBestComponent(this.ComponentsWeaponTorpedoOrderedByRange);
            case ComponentType.WeaponAreaDestruction:
              return this.IdentifyBestComponentPreferSmallSize(this.ComponentsWeaponAreaOrderedByRange);
            case ComponentType.EngineMainThrust:
              return this.IdentifyBestComponentPreferSmallSize(this.ComponentsEngineMainThrustOrderedByPower);
            case ComponentType.EngineVectoring:
              return this.IdentifyBestComponent(this.ComponentsEngineVectoringOrderedByPower);
            case ComponentType.HyperDrive:
              return this.IdentifyBestComponent(this.ComponentsHyperdriveOrderedByJumpInitiation);
            case ComponentType.Reactor:
              return this.IdentifyBestComponent(this.ComponentsReactorOrderedByPower);
            default:
              return preferLatest ? this._LatestComponentsByType[index] : this._BestComponentsByType[index];
          }
        case ShipDesignFocus.Power:
          switch (componentType)
          {
            case ComponentType.WeaponBeam:
              return this.IdentifyBestComponent(this.ComponentsWeaponBeamOrderedByPower);
            case ComponentType.WeaponTorpedo:
              return this.IdentifyBestComponent(this.ComponentsWeaponTorpedoOrderedByPower);
            case ComponentType.WeaponAreaDestruction:
              return this.IdentifyBestComponent(this.ComponentsWeaponAreaOrderedByPower);
            case ComponentType.EngineMainThrust:
              return this.IdentifyBestComponent(this.ComponentsEngineMainThrustOrderedByPower);
            case ComponentType.EngineVectoring:
              return this.IdentifyBestComponent(this.ComponentsEngineVectoringOrderedByPower);
            case ComponentType.HyperDrive:
              return this.IdentifyBestComponent(this.ComponentsHyperdriveOrderedByPower);
            case ComponentType.Reactor:
              return this.IdentifyBestComponent(this.ComponentsReactorOrderedByPower);
            default:
              return preferLatest ? this._LatestComponentsByType[index] : this._BestComponentsByType[index];
          }
        case ShipDesignFocus.Efficiency:
          switch (componentType)
          {
            case ComponentType.WeaponBeam:
              return this.IdentifyBestComponentPreferLowEnergyUse(this.ComponentsWeaponBeamOrderedByPower);
            case ComponentType.WeaponTorpedo:
              return this.IdentifyBestComponentPreferLowEnergyUse(this.ComponentsWeaponTorpedoOrderedByPower);
            case ComponentType.WeaponAreaDestruction:
              return this.IdentifyBestComponentPreferLowEnergyUse(this.ComponentsWeaponAreaOrderedByPower);
            case ComponentType.EngineMainThrust:
              return this.IdentifyBestComponentPreferLowEnergyUse(this.ComponentsEngineMainThrustOrderedByEfficiency);
            case ComponentType.EngineVectoring:
              return this.IdentifyBestComponentPreferLowEnergyUse(this.ComponentsEngineVectoringOrderedByEfficiency);
            case ComponentType.HyperDrive:
              return this.IdentifyBestComponentPreferLowEnergyUse(this.ComponentsHyperdriveOrderedByEfficiency);
            case ComponentType.Reactor:
              return this.IdentifyBestComponent(this.ComponentsReactorOrderedByEfficiency);
            default:
              return preferLatest ? this._LatestComponentsByType[index] : this._BestComponentsByType[index];
          }
        default:
          return preferLatest ? this._LatestComponentsByType[index] : this._BestComponentsByType[index];
      }
    }

    private Component IdentifyBestComponent(ComponentImprovementList orderedComponents) => orderedComponents != null && orderedComponents.Count > 0 ? orderedComponents[0].ImprovedComponent : (Component) null;

    private Component IdentifyBestComponentPreferSmallSize(
      ComponentImprovementList orderedComponents)
    {
      int index = 0;
      if (index >= orderedComponents.Count)
        return (Component) null;
      return index < orderedComponents.Count - 1 && orderedComponents[index + 1].ImprovedComponent.Size < orderedComponents[index].ImprovedComponent.Size ? orderedComponents[index + 1].ImprovedComponent : orderedComponents[index].ImprovedComponent;
    }

    private Component IdentifyBestComponentPreferLowEnergyUse(
      ComponentImprovementList orderedComponents)
    {
      int index = 0;
      if (index >= orderedComponents.Count)
        return (Component) null;
      return index < orderedComponents.Count - 1 && orderedComponents[index + 1].ImprovedComponent.EnergyUsed < orderedComponents[index].ImprovedComponent.EnergyUsed ? orderedComponents[index + 1].ImprovedComponent : orderedComponents[index].ImprovedComponent;
    }

    public ComponentImprovement EvaluateDesiredComponentImprovement(
      ComponentCategoryType componentCategory,
      ShipDesignFocus designFocus)
    {
      Component component = this.EvaluateDesiredComponent(componentCategory, designFocus) ?? this._LatestComponentsByCategory[(int) componentCategory];
      ComponentImprovement componentImprovement = (ComponentImprovement) null;
      if (component != null)
        componentImprovement = this.ResolveImprovedComponentValues(component);
      return componentImprovement;
    }

    public Component EvaluateDesiredComponent(
      ComponentCategoryType componentCategory,
      ShipDesignFocus designFocus)
    {
      return this.EvaluateDesiredComponent(componentCategory, designFocus, false);
    }

    public Component EvaluateDesiredComponent(
      ComponentCategoryType componentCategory,
      ShipDesignFocus designFocus,
      bool preferLatest)
    {
      int index = (int) componentCategory;
      try
      {
        switch (designFocus)
        {
          case ShipDesignFocus.Balanced:
            return preferLatest ? this._LatestComponentsByCategory[index] : this._BestComponentsByCategory[index];
          case ShipDesignFocus.SpeedAgility:
            switch (componentCategory)
            {
              case ComponentCategoryType.WeaponBeam:
                return this.IdentifyBestComponent(this.ComponentsWeaponBeamOrderedByRange);
              case ComponentCategoryType.WeaponTorpedo:
                return this.IdentifyBestComponent(this.ComponentsWeaponTorpedoOrderedByRange);
              case ComponentCategoryType.WeaponArea:
                return this.IdentifyBestComponentPreferSmallSize(this.ComponentsWeaponAreaOrderedByRange);
              case ComponentCategoryType.HyperDrive:
                return this.IdentifyBestComponent(this.ComponentsHyperdriveOrderedByJumpInitiation);
              case ComponentCategoryType.Reactor:
                return this.IdentifyBestComponent(this.ComponentsReactorOrderedByPower);
              default:
                return preferLatest ? this._LatestComponentsByCategory[index] : this._BestComponentsByCategory[index];
            }
          case ShipDesignFocus.Power:
            switch (componentCategory)
            {
              case ComponentCategoryType.WeaponBeam:
                return this.IdentifyBestComponent(this.ComponentsWeaponBeamOrderedByPower);
              case ComponentCategoryType.WeaponTorpedo:
                return this.IdentifyBestComponent(this.ComponentsWeaponTorpedoOrderedByPower);
              case ComponentCategoryType.WeaponArea:
                return this.IdentifyBestComponent(this.ComponentsWeaponAreaOrderedByPower);
              case ComponentCategoryType.HyperDrive:
                return this.IdentifyBestComponent(this.ComponentsHyperdriveOrderedByPower);
              case ComponentCategoryType.Reactor:
                return this.IdentifyBestComponent(this.ComponentsReactorOrderedByPower);
              default:
                return preferLatest ? this._LatestComponentsByCategory[index] : this._BestComponentsByCategory[index];
            }
          case ShipDesignFocus.Efficiency:
            switch (componentCategory)
            {
              case ComponentCategoryType.WeaponBeam:
                return this.IdentifyBestComponentPreferLowEnergyUse(this.ComponentsWeaponBeamOrderedByPower);
              case ComponentCategoryType.WeaponTorpedo:
                return this.IdentifyBestComponentPreferLowEnergyUse(this.ComponentsWeaponTorpedoOrderedByPower);
              case ComponentCategoryType.WeaponArea:
                return this.IdentifyBestComponentPreferLowEnergyUse(this.ComponentsWeaponAreaOrderedByPower);
              case ComponentCategoryType.HyperDrive:
                return this.IdentifyBestComponentPreferLowEnergyUse(this.ComponentsHyperdriveOrderedByEfficiency);
              case ComponentCategoryType.Reactor:
                return this.IdentifyBestComponent(this.ComponentsReactorOrderedByEfficiency);
              default:
                return preferLatest ? this._LatestComponentsByCategory[index] : this._BestComponentsByCategory[index];
            }
        }
      }
      catch (Exception ex)
      {
      }
      return preferLatest ? this._LatestComponentsByCategory[index] : this._BestComponentsByCategory[index];
    }
  }
}
