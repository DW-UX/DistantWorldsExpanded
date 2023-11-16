// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DesignList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DesignList : SyncList<Design>
  {
    public bool ContainsSubRole(BuiltObjectSubRole subRole)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].SubRole == subRole)
          return true;
      }
      return false;
    }

    public bool StripInvalidComponents()
    {
      bool flag1 = false;
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        Design design = this[index1];
        if (design != null && design.Components != null)
        {
          ComponentList componentList = new ComponentList();
          for (int index2 = 0; index2 < design.Components.Count; ++index2)
          {
            Component component = design.Components[index2];
            if (component != null)
            {
              bool flag2 = false;
              for (int index3 = 0; index3 < Galaxy.ComponentDefinitionsStatic.Length; ++index3)
              {
                if (Galaxy.ComponentDefinitionsStatic[index3].ComponentID == component.ComponentID)
                {
                  flag2 = true;
                  break;
                }
              }
              if (!flag2)
                componentList.Add(component);
            }
          }
          for (int index4 = 0; index4 < componentList.Count; ++index4)
            design.Components.Remove(componentList[index4]);
          if (componentList.Count > 0)
          {
            design.ReDefine();
            flag1 = true;
          }
        }
      }
      return flag1;
    }

    public Design FindNewestIncludingObsolete(BuiltObjectSubRole subRole) => this.FindNewestIncludingObsolete(subRole, true);

    public Design FindNewestIncludingObsolete(
      BuiltObjectSubRole subRole,
      bool includePlanetDestroyers)
    {
      long num = 0;
      Design includingObsolete = (Design) null;
      foreach (Design design in (SyncList<Design>) this)
      {
        if (design.SubRole == subRole && (includePlanetDestroyers || !design.IsPlanetDestroyer) && design.DateCreated > num)
        {
          num = design.DateCreated;
          includingObsolete = design;
        }
      }
      return includingObsolete;
    }

    public Design FindNewest(BuiltObjectSubRole subRole)
    {
      long num = 0;
      Design newest = (Design) null;
      foreach (Design design in (SyncList<Design>) this)
      {
        if (design.SubRole == subRole && design.DateCreated > num && !design.IsObsolete)
        {
          num = design.DateCreated;
          newest = design;
        }
      }
      return newest;
    }

    public Design FindNewestPlanetDestroyer()
    {
      long num = 0;
      Design newestPlanetDestroyer = (Design) null;
      foreach (Design design in (SyncList<Design>) this)
      {
        if (design.Role != BuiltObjectRole.Base && design.DateCreated > num && !design.IsObsolete && design.IsPlanetDestroyer)
        {
          num = design.DateCreated;
          newestPlanetDestroyer = design;
        }
      }
      return newestPlanetDestroyer;
    }

    public Design FindNewestNonPlanetDestroyer(BuiltObjectSubRole subRole)
    {
      long num = 0;
      Design nonPlanetDestroyer = (Design) null;
      foreach (Design design in (SyncList<Design>) this)
      {
        if (design.SubRole == subRole && design.DateCreated > num && !design.IsObsolete && !design.IsPlanetDestroyer)
        {
          num = design.DateCreated;
          nonPlanetDestroyer = design;
        }
      }
      return nonPlanetDestroyer;
    }

    public void StripUnbuildableDesigns(Empire empire)
    {
      DesignList designList = new DesignList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (!empire.CanBuildDesign(this[index]))
          designList.Add(this[index]);
      }
      for (int index = 0; index < designList.Count; ++index)
        this.Remove(designList[index]);
    }

    public Design FindNewestCanBuild(BuiltObjectSubRole subRole)
    {
      Empire empire = (Empire) null;
      if (this.Count > 0 && this[0] != null)
        empire = this[0].Empire;
      return this.FindNewestCanBuild(subRole, empire);
    }

    public Design FindNewestCanBuild(BuiltObjectSubRole subRole, Habitat colony)
    {
      Empire empire = (Empire) null;
      if (this.Count > 0 && this[0] != null)
        empire = this[0].Empire;
      return this.FindNewestCanBuild(subRole, empire, colony);
    }

    public Design FindNewestCanBuild(BuiltObjectSubRole subRole, Empire empire) => this.FindNewestCanBuild(subRole, empire, (Habitat) null);

    public Design FindNewestCanBuild(BuiltObjectSubRole subRole, Empire empire, Habitat colony) => this.FindNewestCanBuild(subRole, empire, colony, false);

    public Design FindNewestCanBuild(
      BuiltObjectSubRole subRole,
      Empire empire,
      Habitat colony,
      bool includePlanetDestroyers)
    {
      Design design = (Design) null;
      if (this.Count > 0 && empire != null)
      {
        design = empire.LatestDesigns[(int) subRole];
        if (design != null && !empire.CheckDesignWithinConstructionSize(design, colony))
          design = (Design) null;
        if (subRole == BuiltObjectSubRole.Carrier && !empire.CanBuildCarriers)
          design = (Design) null;
        else if (subRole == BuiltObjectSubRole.ResupplyShip && !empire.CanBuildResupplyShips)
          design = (Design) null;
        if (!includePlanetDestroyers && design != null && design.IsPlanetDestroyer)
          design = (Design) null;
      }
      if (design == null)
        design = this.FindNewestCanBuildFullEvaluate(subRole, colony);
      return design;
    }

    public Design FindNewestCanBuildFullEvaluate(BuiltObjectSubRole subRole) => this.FindNewestCanBuildFullEvaluate(subRole, (Habitat) null);

    public Design FindNewestCanBuildFullEvaluate(BuiltObjectSubRole subRole, Habitat colony)
    {
      bool reasonCannotBuildMissingTech = false;
      bool reasonCannotBuildSizeTooBig = false;
      return this.FindNewestCanBuildFullEvaluate(subRole, colony, out reasonCannotBuildMissingTech, out reasonCannotBuildSizeTooBig);
    }

    public Design FindNewestCanBuildFullEvaluate(
      BuiltObjectSubRole subRole,
      Habitat colony,
      bool includePlanetDestroyers)
    {
      bool reasonCannotBuildMissingTech = false;
      bool reasonCannotBuildSizeTooBig = false;
      return this.FindNewestCanBuildFullEvaluate(subRole, colony, out reasonCannotBuildMissingTech, out reasonCannotBuildSizeTooBig, includePlanetDestroyers);
    }

    public Design FindNewestCanBuildFullEvaluate(
      BuiltObjectSubRole subRole,
      Habitat colony,
      out bool reasonCannotBuildMissingTech,
      out bool reasonCannotBuildSizeTooBig)
    {
      reasonCannotBuildMissingTech = false;
      reasonCannotBuildSizeTooBig = false;
      return this.FindNewestCanBuildFullEvaluate(subRole, colony, out reasonCannotBuildMissingTech, out reasonCannotBuildSizeTooBig, true);
    }

    public Design FindNewestCanBuildFullEvaluate(
      BuiltObjectSubRole subRole,
      Habitat colony,
      out bool reasonCannotBuildMissingTech,
      out bool reasonCannotBuildSizeTooBig,
      bool includePlanetDestroyers)
    {
      long num1 = 0;
      reasonCannotBuildMissingTech = false;
      reasonCannotBuildSizeTooBig = false;
      double num2 = 0.0;
      Design design1 = (Design) null;
      double num3 = 0.0;
      Design design2 = (Design) null;
      for (int index = 0; index < this.Count; ++index)
      {
        Design design3 = this[index];
        if (design3 != null && design3.SubRole == subRole && !design3.IsObsolete && (design3.DateCreated > num1 || design3.OptimizedDesign > 0) && design3.Empire != null && design3.Empire.CanBuildDesign(design3, true, colony, out reasonCannotBuildMissingTech, out reasonCannotBuildSizeTooBig) && (includePlanetDestroyers || !design3.IsPlanetDestroyer))
        {
          if (design3.OptimizedDesign > 0)
          {
            double num4 = 0.0;
            if (design3.Empire != null && design3.Empire.Galaxy != null)
              num4 = design3.CalculateTechLevel(design3.Empire, design3.Empire.Galaxy);
            if (num4 > num2)
            {
              num2 = num4;
              design1 = design3;
            }
          }
          else
          {
            num1 = design3.DateCreated;
            double num5 = 0.0;
            if (design3.Empire != null && design3.Empire.Galaxy != null)
              num5 = design3.CalculateTechLevel(design3.Empire, design3.Empire.Galaxy);
            num3 = num5;
            design2 = design3;
          }
        }
      }
      Design buildFullEvaluate = design2;
      if (design1 != null && design2 != design1 && num3 / num2 < 1.5)
        buildFullEvaluate = design1;
      return buildFullEvaluate;
    }

    public DesignList GetCurrentDesigns()
    {
      DesignList currentDesigns = new DesignList();
      foreach (Design design in (SyncList<Design>) this)
      {
        if (!design.IsObsolete)
          currentDesigns.Add(design);
      }
      return currentDesigns;
    }

    public DesignList GetCurrentDesignsBuildable(Habitat colony)
    {
      DesignList designsBuildable = new DesignList();
      foreach (Design design in (SyncList<Design>) this)
      {
        if (!design.IsObsolete && design.Empire != null && design.Empire.CanBuildDesign(design, true, colony))
          designsBuildable.Add(design);
      }
      return designsBuildable;
    }

    public DesignList GetDesignsByRolesNoObsoleteFilter(List<BuiltObjectRole> roles)
    {
      DesignList noObsoleteFilter = new DesignList();
      foreach (Design design in (SyncList<Design>) this)
      {
        if (roles.Contains(design.Role))
          noObsoleteFilter.Add(design);
      }
      return noObsoleteFilter;
    }

    public DesignList GetDesignsByRoles(List<BuiltObjectRole> roles)
    {
      DesignList designsByRoles = new DesignList();
      foreach (Design design in (SyncList<Design>) this)
      {
        if (roles.Contains(design.Role) && !design.IsObsolete)
          designsByRoles.Add(design);
      }
      return designsByRoles;
    }

    public DesignList ResolveOptimizedDesigns()
    {
      DesignList designList = new DesignList();
      for (int index = 0; index < this.Count; ++index)
      {
        Design design = this[index];
        if (design != null && design.OptimizedDesign > 0)
          designList.Add(design);
      }
      return designList;
    }

    public DesignList GetUnbuildableNonObsoleteDesigns(Empire empire)
    {
      DesignList nonObsoleteDesigns = new DesignList();
      for (int index = 0; index < this.Count; ++index)
      {
        Design design = this[index];
        if (design != null && !design.IsObsolete && !empire.CanBuildDesign(design))
          nonObsoleteDesigns.Add(design);
      }
      return nonObsoleteDesigns;
    }

    public ComponentList DetermineUniqueComponents()
    {
      ComponentList uniqueComponents = new ComponentList();
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        Design design = this[index1];
        if (design != null && design.Components != null)
        {
          for (int index2 = 0; index2 < design.Components.Count; ++index2)
          {
            Component component = design.Components[index2];
            if (component != null && !uniqueComponents.Contains(component))
              uniqueComponents.Add(component);
          }
        }
      }
      return uniqueComponents;
    }

    public ComponentList DetermineUniqueUnbuildableComponents(Empire empire)
    {
      ComponentList unbuildableComponents = new ComponentList();
      if (empire != null)
      {
        ComponentList uniqueComponents = this.DetermineUniqueComponents();
        for (int index = 0; index < uniqueComponents.Count; ++index)
        {
          Component component = uniqueComponents[index];
          if (component != null && !empire.Research.CheckComponentResearched(component) && !unbuildableComponents.Contains(component))
            unbuildableComponents.Add(component);
        }
      }
      return unbuildableComponents;
    }

    public DesignList GetBuildablePlanetDestroyerDesigns(Empire empire)
    {
      DesignList destroyerDesigns = new DesignList();
      foreach (Design design in (SyncList<Design>) this)
      {
        if (design.Role != BuiltObjectRole.Base && design.IsPlanetDestroyer && !design.IsObsolete && empire.CanBuildDesign(design))
          destroyerDesigns.Add(design);
      }
      return destroyerDesigns;
    }

    public DesignList GetBuildableDesignsBySubRoles(
      List<BuiltObjectSubRole> subRoles,
      Empire empire)
    {
      DesignList designsBySubRoles = new DesignList();
      foreach (Design design in (SyncList<Design>) this)
      {
        if (subRoles.Contains(design.SubRole) && !design.IsObsolete && empire.CanBuildDesign(design))
          designsBySubRoles.Add(design);
      }
      return designsBySubRoles;
    }

    public DesignList GetBuildableDesignsBySubRoles(
      List<BuiltObjectSubRole> subRoles,
      Empire empire,
      Habitat colony)
    {
      DesignList designsBySubRoles = new DesignList();
      foreach (Design design in (SyncList<Design>) this)
      {
        if (subRoles.Contains(design.SubRole) && !design.IsObsolete && empire.CanBuildDesign(design, true, colony))
          designsBySubRoles.Add(design);
      }
      return designsBySubRoles;
    }

    public DesignList GetDesignsBySubRolesNoObsoleteFilter(List<BuiltObjectSubRole> subRoles)
    {
      DesignList noObsoleteFilter = new DesignList();
      foreach (Design design in (SyncList<Design>) this)
      {
        if (subRoles.Contains(design.SubRole))
          noObsoleteFilter.Add(design);
      }
      return noObsoleteFilter;
    }

    public DesignList GetDesignsBySubRoles(List<BuiltObjectSubRole> subRoles)
    {
      DesignList designsBySubRoles = new DesignList();
      foreach (Design design in (SyncList<Design>) this)
      {
        if (subRoles.Contains(design.SubRole) && !design.IsObsolete)
          designsBySubRoles.Add(design);
      }
      return designsBySubRoles;
    }
  }
}
