// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResearchNodeList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResearchNodeList : SyncList<ResearchNode>
  {
    public float CalculateTotalCostResearchedProjects()
    {
      float researchedProjects = 0.0f;
      for (int index = 0; index < this.Count; ++index)
      {
        ResearchNode researchNode = this[index];
        if (researchNode != null && researchNode.IsResearched)
          researchedProjects += researchNode.Cost;
      }
      return researchedProjects;
    }

    public int CountCompletedCategories()
    {
      int num = 0;
      List<ComponentCategoryType> componentCategoryTypeList = new List<ComponentCategoryType>();
      componentCategoryTypeList.Add(ComponentCategoryType.Armor);
      componentCategoryTypeList.Add(ComponentCategoryType.Computer);
      componentCategoryTypeList.Add(ComponentCategoryType.Construction);
      componentCategoryTypeList.Add(ComponentCategoryType.EnergyCollector);
      componentCategoryTypeList.Add(ComponentCategoryType.Engine);
      componentCategoryTypeList.Add(ComponentCategoryType.Extractor);
      componentCategoryTypeList.Add(ComponentCategoryType.Fighter);
      componentCategoryTypeList.Add(ComponentCategoryType.Habitation);
      componentCategoryTypeList.Add(ComponentCategoryType.HyperDisrupt);
      componentCategoryTypeList.Add(ComponentCategoryType.HyperDrive);
      componentCategoryTypeList.Add(ComponentCategoryType.Labs);
      componentCategoryTypeList.Add(ComponentCategoryType.Manufacturer);
      componentCategoryTypeList.Add(ComponentCategoryType.Reactor);
      componentCategoryTypeList.Add(ComponentCategoryType.Sensor);
      componentCategoryTypeList.Add(ComponentCategoryType.ShieldRecharge);
      componentCategoryTypeList.Add(ComponentCategoryType.Shields);
      componentCategoryTypeList.Add(ComponentCategoryType.Storage);
      componentCategoryTypeList.Add(ComponentCategoryType.WeaponArea);
      componentCategoryTypeList.Add(ComponentCategoryType.WeaponBeam);
      componentCategoryTypeList.Add(ComponentCategoryType.WeaponIon);
      componentCategoryTypeList.Add(ComponentCategoryType.WeaponPointDefense);
      componentCategoryTypeList.Add(ComponentCategoryType.WeaponTorpedo);
      componentCategoryTypeList.Add(ComponentCategoryType.WeaponGravity);
      componentCategoryTypeList.Add(ComponentCategoryType.AssaultPod);
      for (int index = 0; index < componentCategoryTypeList.Count; ++index)
      {
        if (this.CheckCategoryComplete(componentCategoryTypeList[index]))
          ++num;
      }
      return num;
    }

    public int CountCompletedCategories(IndustryType industry)
    {
      int num = 0;
      List<ComponentCategoryType> componentCategoryTypeList = new List<ComponentCategoryType>();
      componentCategoryTypeList.Add(ComponentCategoryType.Armor);
      componentCategoryTypeList.Add(ComponentCategoryType.Computer);
      componentCategoryTypeList.Add(ComponentCategoryType.Construction);
      componentCategoryTypeList.Add(ComponentCategoryType.EnergyCollector);
      componentCategoryTypeList.Add(ComponentCategoryType.Engine);
      componentCategoryTypeList.Add(ComponentCategoryType.Extractor);
      componentCategoryTypeList.Add(ComponentCategoryType.Fighter);
      componentCategoryTypeList.Add(ComponentCategoryType.Habitation);
      componentCategoryTypeList.Add(ComponentCategoryType.HyperDisrupt);
      componentCategoryTypeList.Add(ComponentCategoryType.HyperDrive);
      componentCategoryTypeList.Add(ComponentCategoryType.Labs);
      componentCategoryTypeList.Add(ComponentCategoryType.Manufacturer);
      componentCategoryTypeList.Add(ComponentCategoryType.Reactor);
      componentCategoryTypeList.Add(ComponentCategoryType.Sensor);
      componentCategoryTypeList.Add(ComponentCategoryType.ShieldRecharge);
      componentCategoryTypeList.Add(ComponentCategoryType.Shields);
      componentCategoryTypeList.Add(ComponentCategoryType.Storage);
      componentCategoryTypeList.Add(ComponentCategoryType.WeaponArea);
      componentCategoryTypeList.Add(ComponentCategoryType.WeaponBeam);
      componentCategoryTypeList.Add(ComponentCategoryType.WeaponIon);
      componentCategoryTypeList.Add(ComponentCategoryType.WeaponPointDefense);
      componentCategoryTypeList.Add(ComponentCategoryType.WeaponTorpedo);
      componentCategoryTypeList.Add(ComponentCategoryType.WeaponGravity);
      componentCategoryTypeList.Add(ComponentCategoryType.AssaultPod);
      for (int index = 0; index < componentCategoryTypeList.Count; ++index)
      {
        if (ComponentDefinition.ResolveIndustry(componentCategoryTypeList[index]) == industry && this.CheckCategoryComplete(componentCategoryTypeList[index]))
          ++num;
      }
      return num;
    }

    public bool CheckCategoryComplete(ComponentCategoryType category)
    {
      ResearchNodeList projectsByCategory = this.GetProjectsByCategory(category);
      ResearchNode researchNode = (ResearchNode) null;
      if (projectsByCategory != null)
      {
        for (int index = 0; index < projectsByCategory.Count; ++index)
        {
          if (researchNode == null || projectsByCategory[index].TechLevel >= researchNode.TechLevel && (double) projectsByCategory[index].Cost > (double) researchNode.Cost)
            researchNode = projectsByCategory[index];
        }
      }
      return researchNode != null && researchNode.IsResearched;
    }

    public void StripProjectsById(List<int> researchProjectIds)
    {
      if (researchProjectIds == null || researchProjectIds.Count <= 0)
        return;
      ResearchNodeList researchNodeList = new ResearchNodeList();
      for (int index = 0; index < this.Count; ++index)
      {
        ResearchNode researchNode = this[index];
        if (researchNode != null && researchProjectIds.Contains(researchNode.ResearchNodeId))
          researchNodeList.Add(researchNode);
      }
      for (int index = 0; index < researchNodeList.Count; ++index)
        this.Remove(researchNodeList[index]);
    }

    public void StripProjectsAboveTechLevel(int techLevel)
    {
      ResearchNodeList projectsAboveTechLevel = this.GetProjectsAboveTechLevel(techLevel);
      for (int index = 0; index < projectsAboveTechLevel.Count; ++index)
        this.Remove(projectsAboveTechLevel[index]);
    }

    public void StripProjectsByType(ComponentType type)
    {
      ResearchNodeList projectsByTypeAny = this.GetProjectsByTypeAny(type);
      for (int index = 0; index < projectsByTypeAny.Count; ++index)
        this.Remove(projectsByTypeAny[index]);
    }

    public void StripProjectsByAbility(ResearchAbilityType abilityType)
    {
      ResearchNodeList projectsByAbility = this.GetProjectsByAbility(abilityType);
      for (int index = 0; index < projectsByAbility.Count; ++index)
        this.Remove(projectsByAbility[index]);
    }

    public ResearchNodeList GetProjectsByAbility(ResearchAbilityType abilityType)
    {
      ResearchNodeList projectsByAbility = new ResearchNodeList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ResolveResearchAbilityType() == abilityType)
          projectsByAbility.Add(this[index]);
      }
      return projectsByAbility;
    }

    public ResearchNodeList GetProjectsByType(ComponentType type)
    {
      ResearchNodeList projectsByType = new ResearchNodeList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ResolveComponentType() == type)
          projectsByType.Add(this[index]);
      }
      return projectsByType;
    }

    public ResearchNodeList GetProjectsByTypeAny(ComponentType type)
    {
      ResearchNodeList projectsByTypeAny = new ResearchNodeList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].CheckAnyComponentTypeMatches(type))
          projectsByTypeAny.Add(this[index]);
      }
      return projectsByTypeAny;
    }

    public ResearchNodeList GetUnresearchedProjectsByTypeAny(ComponentType type)
    {
      ResearchNodeList projectsByTypeAny = new ResearchNodeList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (!this[index].IsResearched && this[index].CheckAnyComponentTypeMatches(type))
          projectsByTypeAny.Add(this[index]);
      }
      return projectsByTypeAny;
    }

    public ResearchNodeList GetProjectsByCategory(ComponentCategoryType category)
    {
      ResearchNodeList projectsByCategory = new ResearchNodeList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Category == category)
          projectsByCategory.Add(this[index]);
      }
      return projectsByCategory;
    }

    public ResearchNodeList GetProjectsByIndustry(IndustryType industry)
    {
      ResearchNodeList projectsByIndustry = new ResearchNodeList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Industry == industry)
          projectsByIndustry.Add(this[index]);
      }
      return projectsByIndustry;
    }

    public ResearchNodeList FindNodesByIdsUnresearched(List<int> researchNodeIds)
    {
      ResearchNodeList byIdsUnresearched = new ResearchNodeList();
      for (int index = 0; index < researchNodeIds.Count; ++index)
      {
        ResearchNode nodeById = this.FindNodeById(researchNodeIds[index]);
        if (nodeById != null && !nodeById.IsResearched && nodeById.IsEnabled)
          byIdsUnresearched.Add(nodeById);
      }
      return byIdsUnresearched;
    }

    public ResearchNodeList FindNodesByIds(List<int> researchNodeIds)
    {
      ResearchNodeList nodesByIds = new ResearchNodeList();
      for (int index = 0; index < researchNodeIds.Count; ++index)
      {
        ResearchNode nodeById = this.FindNodeById(researchNodeIds[index]);
        if (nodeById != null)
          nodesByIds.Add(nodeById);
      }
      return nodesByIds;
    }

    public bool ContainsBySpecialFunctionCode(int specialFunctionCode)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].SpecialFunctionCode == specialFunctionCode)
          return true;
      }
      return false;
    }

    public int IndexBySpecialFunctionCode(int specialFunctionCode)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].SpecialFunctionCode == specialFunctionCode)
          return index;
      }
      return -1;
    }

    public bool ContainsById(int researchNodeId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ResearchNodeId == researchNodeId)
          return true;
      }
      return false;
    }

    public ResearchNode FindNodeById(int researchNodeId)
    {
      ResearchNode nodeById = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ResearchNodeId == researchNodeId)
          return this[index];
      }
      return nodeById;
    }

    public ResearchNode FindNodeBySpecialFunctionCode(int specialFunctionCode)
    {
      ResearchNode specialFunctionCode1 = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].SpecialFunctionCode == specialFunctionCode)
          return this[index];
      }
      return specialFunctionCode1;
    }

    public void FindAndResearchLowestProject(ComponentType type, Race race) => this.FindAndResearchLowestProject(type, race, -1);

    public void FindAndResearchLowestProject(ComponentType type, Race race, int minimumTechLevel)
    {
      double num = double.MaxValue;
      ResearchNode researchNode = (ResearchNode) null;
      ResearchNodeList researchNodeList = new ResearchNodeList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ResolveComponentTypesAll().Contains(type) && (this[index].AllowedRaces == null || this[index].AllowedRaces.Count == 0 || this[index].AllowedRaces.Contains(race)))
        {
          if ((double) this[index].TechLevel < num)
          {
            researchNode = this[index];
            num = (double) researchNode.TechLevel;
          }
          if (this[index].TechLevel <= minimumTechLevel)
            researchNodeList.Add(this[index]);
        }
      }
      for (int index = 0; index < researchNodeList.Count; ++index)
      {
        researchNodeList[index].IsResearched = true;
        researchNodeList[index].IsEnabled = true;
      }
      if (researchNode == null)
        return;
      researchNode.IsResearched = true;
      researchNode.IsEnabled = true;
    }

    public void FindAndResearchLowestProject(
      ComponentCategoryType category,
      Race race,
      int minimumTechLevel)
    {
      double num = double.MaxValue;
      ResearchNode researchNode = (ResearchNode) null;
      ResearchNodeList researchNodeList = new ResearchNodeList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Category == category && (this[index].AllowedRaces == null || this[index].AllowedRaces.Count == 0 || this[index].AllowedRaces.Contains(race)))
        {
          if ((double) this[index].TechLevel < num)
          {
            researchNode = this[index];
            num = (double) researchNode.TechLevel;
          }
          if (this[index].TechLevel <= minimumTechLevel)
            researchNodeList.Add(this[index]);
        }
      }
      for (int index = 0; index < researchNodeList.Count; ++index)
      {
        researchNodeList[index].IsResearched = true;
        researchNodeList[index].IsEnabled = true;
      }
      if (researchNode == null)
        return;
      researchNode.IsResearched = true;
      researchNode.IsEnabled = true;
    }

    public ResearchNodeList GetRequiredParents(
      ResearchNode startingNode,
      bool includeResearchedNodes)
    {
      ResearchNodeList requiredParents1 = new ResearchNodeList();
      if (startingNode.ParentNodes != null)
      {
        for (int index = 0; index < startingNode.ParentNodes.Count; ++index)
        {
          ResearchNode parentNode = startingNode.ParentNodes[index];
          bool flag = false;
          if (startingNode.ParentIsRequired.Count > index)
            flag = startingNode.ParentIsRequired[index];
          if (parentNode != null && (startingNode.ParentNodes != null && startingNode.ParentNodes.Count == 1 || flag) && (includeResearchedNodes || !parentNode.IsResearched))
          {
            requiredParents1.Add(parentNode);
            ResearchNodeList requiredParents2 = this.GetRequiredParents(parentNode, includeResearchedNodes);
            if (requiredParents2.Count > 0)
              requiredParents1 = requiredParents1.Merge(requiredParents2);
          }
        }
      }
      return requiredParents1;
    }

    public ResearchNodeList GetCurrentPath(ResearchNode startingNode, Race race)
    {
      ResearchNodeList currentPath1 = new ResearchNodeList();
      if (startingNode.ParentNodes != null)
      {
        bool requiredParentsNeedResearching = false;
        bool optionalParentsNeedResearching = false;
        if (!this.CheckProjectIsReachableWithAllNeededParentResearch(startingNode, out requiredParentsNeedResearching, out optionalParentsNeedResearching))
        {
          for (int index = 0; index < startingNode.ParentNodes.Count; ++index)
          {
            ResearchNode parentNode = startingNode.ParentNodes[index];
            bool flag = false;
            if (startingNode.ParentIsRequired.Count > index)
              flag = startingNode.ParentIsRequired[index];
            if (parentNode != null && (requiredParentsNeedResearching && flag || optionalParentsNeedResearching && !flag) && !parentNode.IsResearched && (parentNode.AllowedRaces == null || parentNode.AllowedRaces.Count == 0 || parentNode.AllowedRaces.Contains(race)))
            {
              currentPath1.Add(parentNode);
              ResearchNodeList currentPath2 = this.GetCurrentPath(parentNode, race);
              if (currentPath2.Count > 0)
                currentPath1 = currentPath1.Merge(currentPath2);
            }
          }
        }
      }
      return currentPath1;
    }

    public bool CheckProjectIsReachableWithAllNeededParentResearch(
      ResearchNode project,
      out bool requiredParentsNeedResearching,
      out bool optionalParentsNeedResearching)
    {
      requiredParentsNeedResearching = false;
      optionalParentsNeedResearching = false;
      if (project.ParentNodes.Count <= 0)
        return true;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      for (int index = 0; index < project.ParentNodes.Count; ++index)
      {
        ResearchNode parentNode = project.ParentNodes[index];
        bool flag = false;
        if (project.ParentIsRequired.Count > index)
          flag = project.ParentIsRequired[index];
        if (parentNode != null)
        {
          if (flag)
          {
            ++num1;
            if (!parentNode.IsResearched)
              ++num2;
          }
          else
          {
            ++num3;
            if (parentNode.IsResearched)
              ++num4;
          }
        }
      }
      if (num1 > 0)
      {
        if (num2 == 0)
          return true;
        requiredParentsNeedResearching = true;
      }
      else
      {
        if (num3 <= 0 || num4 > 0)
          return true;
        optionalParentsNeedResearching = true;
      }
      return false;
    }

    public ResearchAbility CheckAncestorsForAbility(
      ResearchNode startingNode,
      ResearchAbilityType abilityType)
    {
      if (startingNode.Abilities != null && startingNode.Abilities.Count > 0)
      {
        for (int index = 0; index < startingNode.Abilities.Count; ++index)
        {
          if (startingNode.Abilities[index].Type == abilityType)
            return startingNode.Abilities[index];
        }
      }
      ResearchAbility researchAbility = (ResearchAbility) null;
      if (startingNode.ParentNodes != null && startingNode.ParentNodes.Count > 0)
      {
        for (int index = 0; index < startingNode.ParentNodes.Count; ++index)
        {
          researchAbility = this.CheckAncestorsForAbility(startingNode.ParentNodes[index], abilityType);
          if (researchAbility != null)
            return researchAbility;
        }
      }
      return researchAbility;
    }

    public ResearchNodeList Clone()
    {
      ResearchNodeList researchNodeList = new ResearchNodeList();
      for (int index = 0; index < this.Count; ++index)
      {
        ResearchNode researchNode = this[index].Clone();
        researchNodeList.Add(researchNode);
      }
      return researchNodeList;
    }

    public ResearchNodeList Merge(ResearchNodeList projects)
    {
      ResearchNodeList researchNodeList = new ResearchNodeList();
      researchNodeList.AddRange((IEnumerable<ResearchNode>) this);
      for (int index = 0; index < projects.Count; ++index)
      {
        ResearchNode project = projects[index];
        if (project != null && !researchNodeList.ContainsById(project.ResearchNodeId))
          researchNodeList.Add(project);
      }
      return researchNodeList;
    }

    public ResearchNodeList Intersect(ResearchNodeList projects)
    {
      ResearchNodeList researchNodeList = new ResearchNodeList();
      for (int index = 0; index < projects.Count; ++index)
      {
        ResearchNode project = projects[index];
        if (project != null && this.ContainsById(project.ResearchNodeId))
          researchNodeList.Add(project);
      }
      return researchNodeList;
    }

    public ResearchNodeList NotIntersect(ResearchNodeList projects)
    {
      ResearchNodeList researchNodeList = new ResearchNodeList();
      for (int index = 0; index < this.Count; ++index)
      {
        ResearchNode researchNode = this[index];
        if (researchNode != null && !projects.ContainsById(researchNode.ResearchNodeId))
          researchNodeList.Add(researchNode);
      }
      return researchNodeList;
    }

    public int GetLowestTechLevel()
    {
      int lowestTechLevel = int.MaxValue;
      for (int index = 0; index < this.Count; ++index)
      {
        ResearchNode researchNode = this[index];
        if (researchNode != null && researchNode.TechLevel < lowestTechLevel)
          lowestTechLevel = researchNode.TechLevel;
      }
      return lowestTechLevel;
    }

    public void GetTechLevelRange(out int lowest, out int highest)
    {
      lowest = int.MaxValue;
      highest = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        ResearchNode researchNode = this[index];
        if (researchNode != null)
        {
          if (researchNode.TechLevel < lowest)
            lowest = researchNode.TechLevel;
          if (researchNode.TechLevel > highest)
            highest = researchNode.TechLevel;
        }
      }
    }

    public ResearchNodeList GetProjectsAtTechLevel(int techLevel)
    {
      ResearchNodeList projectsAtTechLevel = new ResearchNodeList();
      for (int index = 0; index < this.Count; ++index)
      {
        ResearchNode researchNode = this[index];
        if (researchNode != null && researchNode.TechLevel == techLevel)
          projectsAtTechLevel.Add(researchNode);
      }
      return projectsAtTechLevel;
    }

    public ResearchNodeList GetProjectsAboveTechLevel(int techLevel)
    {
      ResearchNodeList projectsAboveTechLevel = new ResearchNodeList();
      for (int index = 0; index < this.Count; ++index)
      {
        ResearchNode researchNode = this[index];
        if (researchNode != null && researchNode.TechLevel > techLevel)
          projectsAboveTechLevel.Add(researchNode);
      }
      return projectsAboveTechLevel;
    }

    public ResearchNodeList RemoveProjectsWithTechLevelHigherThan(int techLevel)
    {
      ResearchNodeList researchNodeList = new ResearchNodeList();
      for (int index = 0; index < this.Count; ++index)
      {
        ResearchNode researchNode = this[index];
        if (researchNode != null && researchNode.TechLevel <= techLevel)
          researchNodeList.Add(researchNode);
      }
      return researchNodeList;
    }

    public ResearchNodeList OrderByTechLevel()
    {
      ResearchNodeList researchNodeList = new ResearchNodeList();
      researchNodeList.AddRange((IEnumerable<ResearchNode>) this);
      for (int index = 0; index < researchNodeList.Count; ++index)
        researchNodeList[index].SortTag = (float) researchNodeList[index].TechLevel;
      researchNodeList.Sort();
      return researchNodeList;
    }

    public ResearchNodeList GetRangeTypedList(int index, int count)
    {
      ResearchNodeList rangeTypedList = new ResearchNodeList();
      List<ResearchNode> range = this.GetRange(index, count);
      rangeTypedList.AddRange((IEnumerable<ResearchNode>) range);
      return rangeTypedList;
    }

    public ResearchNode SelectRandomLowestProject(Galaxy galaxy)
    {
      if (galaxy != null && Galaxy.Rnd != null && this.Count > 0)
      {
        double num = double.MaxValue;
        ResearchNodeList researchNodeList = new ResearchNodeList();
        for (int index = 0; index < this.Count; ++index)
        {
          ResearchNode researchNode = this[index];
          if (researchNode != null)
          {
            if ((double) researchNode.TechLevel < num)
            {
              num = (double) researchNode.TechLevel;
              researchNodeList.Clear();
              researchNodeList.Add(researchNode);
            }
            else if ((double) researchNode.TechLevel == num)
              researchNodeList.Add(researchNode);
          }
        }
        if (researchNodeList != null && researchNodeList.Count > 0)
        {
          int index = Galaxy.Rnd.Next(0, researchNodeList.Count);
          return researchNodeList[index];
        }
      }
      return (ResearchNode) null;
    }

    public ResearchNode SelectRandomProject(Galaxy galaxy) => galaxy != null && Galaxy.Rnd != null && this.Count > 0 ? this[Galaxy.Rnd.Next(0, this.Count)] : (ResearchNode) null;

    public double GetCategoryTechLevel(ComponentCategoryType category)
    {
      double categoryTechLevel = 0.0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].IsResearched && this[index].Category == category && (double) this[index].TechLevel > categoryTechLevel)
          categoryTechLevel = (double) this[index].TechLevel;
      }
      return categoryTechLevel;
    }

    public ResearchNode GetProjectByFacility(PlanetaryFacilityDefinition facility)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        ResearchNode projectByFacility = this[index];
        if (projectByFacility != null && projectByFacility.PlanetaryFacility != null && projectByFacility.PlanetaryFacility == facility)
          return projectByFacility;
      }
      return (ResearchNode) null;
    }

    public ResearchNode GetHighestProjectForComponent(Component component)
    {
      double num = 0.0;
      ResearchNode projectForComponent = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Components != null && this[index].Components.Contains(component))
        {
          if ((double) this[index].TechLevel > num)
          {
            projectForComponent = this[index];
            num = (double) projectForComponent.TechLevel;
          }
        }
        else if (this[index].ComponentImprovements != null && this[index].ComponentImprovements.ContainsComponent(component) && (double) this[index].TechLevel > num)
        {
          projectForComponent = this[index];
          num = (double) projectForComponent.TechLevel;
        }
      }
      return projectForComponent;
    }

    public ResearchNode GetHighestResearchedProjectForIndustry(IndustryType industry)
    {
      double num = 0.0;
      ResearchNode projectForIndustry = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].IsResearched && this[index].Industry == industry && (double) this[index].TechLevel > num)
        {
          projectForIndustry = this[index];
          num = (double) projectForIndustry.TechLevel;
        }
      }
      return projectForIndustry;
    }

    public ResearchNode GetHighestProjectForCategory(ComponentCategoryType category)
    {
      double num = 0.0;
      ResearchNode projectForCategory = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Category == category && (double) this[index].TechLevel > num)
        {
          projectForCategory = this[index];
          num = (double) projectForCategory.TechLevel;
        }
      }
      return projectForCategory;
    }

    public ResearchNode GetLowestUnresearchedProjectForRaceForCategory(
      ComponentCategoryType category,
      Race race)
    {
      double num = double.MaxValue;
      ResearchNode forRaceForCategory = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Category == category && this[index].AllowedRaces != null && this[index].AllowedRaces.Contains(race) && (double) this[index].TechLevel < num)
        {
          forRaceForCategory = this[index];
          num = (double) forRaceForCategory.TechLevel;
        }
      }
      return forRaceForCategory;
    }

    public ResearchNode GetHighestProjectForTypeAny(ComponentType type)
    {
      double num = 0.0;
      ResearchNode projectForTypeAny = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].CheckAnyComponentTypeMatches(type) && (double) this[index].TechLevel > num)
        {
          projectForTypeAny = this[index];
          num = (double) projectForTypeAny.TechLevel;
        }
      }
      return projectForTypeAny;
    }

    public ResearchNode GetHighestProjectForType(ComponentType type)
    {
      double num = 0.0;
      ResearchNode highestProjectForType = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ResolveComponentType() == type && (double) this[index].TechLevel > num)
        {
          highestProjectForType = this[index];
          num = (double) highestProjectForType.TechLevel;
        }
      }
      return highestProjectForType;
    }

    public ResearchNode GetLowestUnresearchedProjectForRaceForTypeAny(ComponentType type, Race race)
    {
      double num = double.MaxValue;
      ResearchNode forRaceForTypeAny = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].CheckAnyComponentTypeMatches(type) && this[index].AllowedRaces != null && this[index].AllowedRaces.Contains(race) && (double) this[index].TechLevel < num)
        {
          forRaceForTypeAny = this[index];
          num = (double) forRaceForTypeAny.TechLevel;
        }
      }
      return forRaceForTypeAny;
    }

    public ResearchNode GetLowestProjectForTypeAny(ComponentType type)
    {
      double num = double.MaxValue;
      ResearchNode projectForTypeAny = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].CheckAnyComponentTypeMatches(type) && (double) this[index].TechLevel < num)
        {
          projectForTypeAny = this[index];
          num = (double) projectForTypeAny.TechLevel;
        }
      }
      return projectForTypeAny;
    }

    public ResearchNode GetLowestUnresearchedProjectForTypeAny(ComponentType type, Race race)
    {
      double num = double.MaxValue;
      ResearchNode projectForTypeAny = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (!this[index].IsResearched && this[index].CheckAnyComponentTypeMatches(type) && (double) this[index].TechLevel < num && (race == null || this[index].AllowedRaces == null || this[index].AllowedRaces.Count <= 0 || this[index].AllowedRaces.IndexOf(race) >= 0))
        {
          projectForTypeAny = this[index];
          num = (double) projectForTypeAny.TechLevel;
        }
      }
      return projectForTypeAny;
    }

    public ResearchNodeList GetLowestUnresearchedProjectsForTypeAny(ComponentType type, Race race)
    {
      ResearchNodeList projectsForTypeAny = new ResearchNodeList();
      ResearchNode projectForTypeAny = this.GetLowestUnresearchedProjectForTypeAny(type, race);
      if (projectForTypeAny != null)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (!this[index].IsResearched && this[index].CheckAnyComponentTypeMatches(type) && (double) this[index].TechLevel <= (double) projectForTypeAny.TechLevel)
            projectsForTypeAny.Add(this[index]);
        }
      }
      return projectsForTypeAny;
    }

    public ResearchNode GetSecondLowestProjectForTypeAny(ComponentType type)
    {
      double num = double.MaxValue;
      ResearchNode researchNode = (ResearchNode) null;
      ResearchNode projectForTypeAny = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].CheckAnyComponentTypeMatches(type) && (double) this[index].TechLevel < num)
        {
          if (researchNode != null)
            projectForTypeAny = researchNode;
          researchNode = this[index];
          num = (double) researchNode.TechLevel;
        }
      }
      return projectForTypeAny;
    }

    public ResearchNode GetLowestProjectForType(ComponentType type)
    {
      double num = double.MaxValue;
      ResearchNode lowestProjectForType = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ResolveComponentType() == type && (double) this[index].TechLevel < num)
        {
          lowestProjectForType = this[index];
          num = (double) lowestProjectForType.TechLevel;
        }
      }
      return lowestProjectForType;
    }

    public ResearchNode GetSecondLowestProjectForType(ComponentType type)
    {
      double num = double.MaxValue;
      ResearchNode researchNode = (ResearchNode) null;
      ResearchNode lowestProjectForType = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ResolveComponentType() == type && (double) this[index].TechLevel < num)
        {
          if (researchNode != null)
            lowestProjectForType = researchNode;
          researchNode = this[index];
          num = (double) researchNode.TechLevel;
        }
      }
      return lowestProjectForType;
    }

    public ResearchNode GetLowestProjectForPlanetaryFacilityType(PlanetaryFacilityType facilityType)
    {
      double num = double.MaxValue;
      ResearchNode planetaryFacilityType = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].PlanetaryFacility != null && this[index].PlanetaryFacility.Type == facilityType && (double) this[index].TechLevel < num)
        {
          planetaryFacilityType = this[index];
          num = (double) planetaryFacilityType.TechLevel;
        }
      }
      return planetaryFacilityType;
    }

    public ResearchNode GetLowestUnresearchedProjectForPlanetaryFacilityType(
      PlanetaryFacilityType facilityType)
    {
      double num = double.MaxValue;
      ResearchNode planetaryFacilityType = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (!this[index].IsResearched && this[index].PlanetaryFacility != null && this[index].PlanetaryFacility.Type == facilityType && (double) this[index].TechLevel < num)
        {
          planetaryFacilityType = this[index];
          num = (double) planetaryFacilityType.TechLevel;
        }
      }
      return planetaryFacilityType;
    }

    public ResearchNode GetLowestProjectForWonderType(WonderType wonderType)
    {
      double num = double.MaxValue;
      ResearchNode projectForWonderType = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].PlanetaryFacility != null && this[index].PlanetaryFacility.Type == PlanetaryFacilityType.Wonder && this[index].PlanetaryFacility.WonderType == wonderType && (double) this[index].TechLevel < num)
        {
          projectForWonderType = this[index];
          num = (double) projectForWonderType.TechLevel;
        }
      }
      return projectForWonderType;
    }

    public ResearchNode GetLowestUnresearchedProjectForWonderType(WonderType wonderType)
    {
      double num = double.MaxValue;
      ResearchNode projectForWonderType = (ResearchNode) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (!this[index].IsResearched && this[index].PlanetaryFacility != null && this[index].PlanetaryFacility.Type == PlanetaryFacilityType.Wonder && this[index].PlanetaryFacility.WonderType == wonderType && (double) this[index].TechLevel < num)
        {
          projectForWonderType = this[index];
          num = (double) projectForWonderType.TechLevel;
        }
      }
      return projectForWonderType;
    }

    public ResearchNode GetLowestProjectForTroopType(TroopType troopType)
    {
      double num = double.MaxValue;
      ResearchNode projectForTroopType = (ResearchNode) null;
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        if (this[index1].Abilities != null)
        {
          for (int index2 = 0; index2 < this[index1].Abilities.Count; ++index2)
          {
            ResearchAbility ability = this[index1].Abilities[index2];
            if (ability != null && ability.Type == ResearchAbilityType.Troop && ability.RelatedObject != null && ability.RelatedObject is TroopType && (TroopType) ability.RelatedObject == troopType && (double) this[index1].TechLevel < num)
            {
              projectForTroopType = this[index1];
              num = (double) projectForTroopType.TechLevel;
            }
          }
        }
      }
      return projectForTroopType;
    }

    public ResearchNode GetLowestUnresearchedProjectForTroopType(TroopType troopType)
    {
      double num = double.MaxValue;
      ResearchNode projectForTroopType = (ResearchNode) null;
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        if (!this[index1].IsResearched && this[index1].Abilities != null)
        {
          for (int index2 = 0; index2 < this[index1].Abilities.Count; ++index2)
          {
            ResearchAbility ability = this[index1].Abilities[index2];
            if (ability != null && ability.Type == ResearchAbilityType.Troop && ability.RelatedObject != null && ability.RelatedObject is TroopType && (TroopType) ability.RelatedObject == troopType && (double) this[index1].TechLevel < num)
            {
              projectForTroopType = this[index1];
              num = (double) projectForTroopType.TechLevel;
            }
          }
        }
      }
      return projectForTroopType;
    }

    public ResearchNode GetLowestProjectForDedicatedCarriers()
    {
      double num = double.MaxValue;
      ResearchNode dedicatedCarriers = (ResearchNode) null;
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        if (this[index1].Abilities != null)
        {
          for (int index2 = 0; index2 < this[index1].Abilities.Count; ++index2)
          {
            ResearchAbility ability = this[index1].Abilities[index2];
            if (ability != null && ability.Type == ResearchAbilityType.EnableShipSubRole && ability.RelatedObject != null && ability.RelatedObject is BuiltObjectSubRole && (BuiltObjectSubRole) ability.RelatedObject == BuiltObjectSubRole.Carrier && (double) this[index1].TechLevel < num)
            {
              dedicatedCarriers = this[index1];
              num = (double) dedicatedCarriers.TechLevel;
            }
          }
        }
      }
      return dedicatedCarriers;
    }

    public ResearchNode GetLowestProjectForResupplyShips()
    {
      double num = double.MaxValue;
      ResearchNode forResupplyShips = (ResearchNode) null;
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        if (this[index1].Abilities != null)
        {
          for (int index2 = 0; index2 < this[index1].Abilities.Count; ++index2)
          {
            ResearchAbility ability = this[index1].Abilities[index2];
            if (ability != null && ability.Type == ResearchAbilityType.EnableShipSubRole && ability.RelatedObject != null && ability.RelatedObject is BuiltObjectSubRole && (BuiltObjectSubRole) ability.RelatedObject == BuiltObjectSubRole.ResupplyShip && (double) this[index1].TechLevel < num)
            {
              forResupplyShips = this[index1];
              num = (double) forResupplyShips.TechLevel;
            }
          }
        }
      }
      return forResupplyShips;
    }

    public ResearchNode GetLowestProjectForColonization(HabitatType habitatType)
    {
      double num = double.MaxValue;
      ResearchNode projectForColonization = (ResearchNode) null;
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        if (this[index1].Abilities != null)
        {
          for (int index2 = 0; index2 < this[index1].Abilities.Count; ++index2)
          {
            ResearchAbility ability = this[index1].Abilities[index2];
            if (ability != null && ability.Type == ResearchAbilityType.ColonizeHabitatType && Galaxy.ResolveColonyHabitatTypeByIndexIncludingUndefined(ability.Value) == habitatType && (double) this[index1].TechLevel < num)
            {
              projectForColonization = this[index1];
              num = (double) projectForColonization.TechLevel;
            }
          }
        }
      }
      return projectForColonization;
    }

    public ResearchNode GetEquivalent(ResearchNode researchNode) => researchNode != null && this.Count > researchNode.ResearchNodeId ? this[researchNode.ResearchNodeId] : (ResearchNode) null;

    public bool CheckContainsAnyNodeId(List<int> nodeIds)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        ResearchNode researchNode = this[index];
        if (researchNode != null && nodeIds.Contains(researchNode.ResearchNodeId))
          return true;
      }
      return false;
    }

    public new int IndexOf(ResearchNode researchNode)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].IsEquivalent(researchNode))
          return index;
      }
      return -1;
    }

    public bool ContainsEquivalent(ResearchNode researchNode)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].IsEquivalent(researchNode))
          return true;
      }
      return false;
    }
  }
}
