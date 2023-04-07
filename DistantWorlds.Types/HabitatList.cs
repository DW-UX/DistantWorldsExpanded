// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.HabitatList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class HabitatList : SyncList<Habitat>, IComparable
  {
    public Habitat GetRandomHabitatPopulationAnotherRace(Galaxy galaxy, Race race)
    {
      HabitatList populationAnotherRace = this.GetHabitatsPopulationAnotherRace(race);
      if (populationAnotherRace.Count <= 0)
        return (Habitat) null;
      int index = Galaxy.Rnd.Next(0, populationAnotherRace.Count);
      return populationAnotherRace[index];
    }

    public long TotalPopulation()
    {
      long num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null && habitat.Population != null)
          num += habitat.Population.TotalAmount;
      }
      return num;
    }

    public long TotalPopulationOwnedColonies(Empire empire)
    {
      long num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null && habitat.Empire == empire && habitat.Population != null)
          num += habitat.Population.TotalAmount;
      }
      return num;
    }

    public HabitatList GetHabitatsPopulationAnotherRace(Race race)
    {
      HabitatList populationAnotherRace = new HabitatList();
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        Habitat habitat = this[index1];
        for (int index2 = 0; index2 < habitat.Population.Count; ++index2)
        {
          Population population = habitat.Population[index2];
          if (population != null && population.Race != race)
          {
            populationAnotherRace.Add(habitat);
            break;
          }
        }
      }
      return populationAnotherRace;
    }

    public Habitat FindNearest(double x, double y)
    {
      Habitat nearest = (Habitat) null;
      double num = double.MaxValue;
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null)
        {
          double distanceSquaredStatic = Galaxy.CalculateDistanceSquaredStatic(x, y, habitat.Xpos, habitat.Ypos);
          if (nearest == null || distanceSquaredStatic < num)
          {
            nearest = habitat;
            num = distanceSquaredStatic;
          }
        }
      }
      return nearest;
    }

    public bool StripInvalidFacilities()
    {
      bool flag = false;
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        Habitat habitat = this[index1];
        if (habitat != null && habitat.Facilities != null)
        {
          PlanetaryFacilityList planetaryFacilityList = new PlanetaryFacilityList();
          for (int index2 = 0; index2 < habitat.Facilities.Count; ++index2)
          {
            PlanetaryFacility facility = habitat.Facilities[index2];
            if (facility != null && Galaxy.PlanetaryFacilityDefinitionsStatic.GetById(facility.PlanetaryFacilityDefinitionId) == null)
              planetaryFacilityList.Add(facility);
          }
          for (int index3 = 0; index3 < planetaryFacilityList.Count; ++index3)
          {
            habitat.Facilities.Remove(planetaryFacilityList[index3]);
            habitat.CheckRemoveFacilityTracking(planetaryFacilityList[index3]);
          }
          if (planetaryFacilityList.Count > 0)
          {
            habitat.ReviewPlanetaryFacilities(habitat.Empire);
            flag = true;
          }
        }
      }
      return flag;
    }

    public Habitat FindColonyWithFacilityId(int planetaryFacilityDefinitionId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat colonyWithFacilityId = this[index];
        if (colonyWithFacilityId != null && !colonyWithFacilityId.HasBeenDestroyed && colonyWithFacilityId.Facilities != null && colonyWithFacilityId.Facilities.GetById(planetaryFacilityDefinitionId) != null)
          return colonyWithFacilityId;
      }
      return (Habitat) null;
    }

    public Habitat FindColonyWithFacility(PlanetaryFacility facility)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat colonyWithFacility = this[index];
        if (colonyWithFacility != null && !colonyWithFacility.HasBeenDestroyed && colonyWithFacility.Facilities != null && colonyWithFacility.Facilities.Contains(facility))
          return colonyWithFacility;
      }
      return (Habitat) null;
    }

    public Habitat FindHabitatWithRuin(Ruin ruin)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitatWithRuin = this[index];
        if (habitatWithRuin != null && !habitatWithRuin.HasBeenDestroyed && habitatWithRuin.Ruin == ruin)
          return habitatWithRuin;
      }
      return (Habitat) null;
    }

    public int CountHabitatWithRuins()
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Ruin != null)
          ++num;
      }
      return num;
    }

    public int CountPirateControlledColonies(Empire pirateEmpire, out int ownedColonyCount)
    {
      int num = 0;
      ownedColonyCount = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null && !habitat.HasBeenDestroyed && habitat.Facilities != null)
        {
          if (habitat.Empire == pirateEmpire)
          {
            ++ownedColonyCount;
          }
          else
          {
            PirateColonyControl byFaction = habitat.GetPirateControl().GetByFaction(pirateEmpire);
            if (byFaction != null && (double) byFaction.ControlLevel > 0.0)
              ++num;
          }
        }
      }
      return num;
    }

    public HabitatList GetPirateControlledColonies(
      Empire pirateEmpire,
      out HabitatList ownedColonies)
    {
      HabitatList controlledColonies = new HabitatList();
      ownedColonies = new HabitatList();
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null && !habitat.HasBeenDestroyed && habitat.Facilities != null)
        {
          if (habitat.Empire == pirateEmpire)
          {
            ownedColonies.Add(habitat);
          }
          else
          {
            PirateColonyControl byFaction = habitat.GetPirateControl().GetByFaction(pirateEmpire);
            if (byFaction != null && (double) byFaction.ControlLevel > 0.0)
              controlledColonies.Add(habitat);
          }
        }
      }
      return controlledColonies;
    }

    public int CountColoniesWithFacilityType(
      PlanetaryFacilityType type,
      WonderType wonderType,
      int value2)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null && !habitat.HasBeenDestroyed && habitat.Facilities != null)
        {
          if (type == PlanetaryFacilityType.Wonder)
            num += habitat.Facilities.CountCompletedWonderByType(wonderType, value2);
          else
            num += habitat.Facilities.CountCompletedByType(type, value2);
        }
      }
      return num;
    }

    public int CountPirateControlledColoniesWithHiddenPirateBase(Empire pirateEmpire)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null && !habitat.HasBeenDestroyed && habitat.Facilities != null)
        {
          PirateColonyControl byFaction = habitat.GetPirateControl().GetByFaction(pirateEmpire);
          if (byFaction != null && byFaction.HasFacilityControl)
            num += habitat.Facilities.CountCompletedByType(PlanetaryFacilityType.PirateBase);
        }
      }
      return num;
    }

    public int CountByType(HabitatType type)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type)
          ++num;
      }
      return num;
    }

    public HabitatList GetHabitatsPopulationBelowThreshold(
      long maxPopulationAmount,
      HabitatType type)
    {
      HabitatList populationBelowThreshold = new HabitatList();
      for (int index = 0; index < this.Count; ++index)
      {
        if ((type == HabitatType.Undefined || this[index].Type == type) && (this[index].Population == null || this[index].Population.TotalAmount < maxPopulationAmount))
          populationBelowThreshold.Add(this[index]);
      }
      return populationBelowThreshold;
    }

    public HabitatList GetHabitatsPopulationAboveThreshold(
      long minPopulationAmount,
      HabitatType type)
    {
      HabitatList populationAboveThreshold = new HabitatList();
      for (int index = 0; index < this.Count; ++index)
      {
        if ((type == HabitatType.Undefined || this[index].Type == type) && (this[index].Population == null || this[index].Population.TotalAmount >= minPopulationAmount))
          populationAboveThreshold.Add(this[index]);
      }
      return populationAboveThreshold;
    }

    public Habitat GetRandomHabitatPopulationBelowThreshold(
      Galaxy galaxy,
      long maxPopulationAmount,
      HabitatType type)
    {
      HabitatList populationBelowThreshold = this.GetHabitatsPopulationBelowThreshold(maxPopulationAmount, type);
      if (populationBelowThreshold.Count <= 0)
        return (Habitat) null;
      int index = Galaxy.Rnd.Next(0, populationBelowThreshold.Count);
      return populationBelowThreshold[index];
    }

    public HabitatList GetHabitatsWithRestrictedResources()
    {
      HabitatList restrictedResources = new HabitatList();
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        Habitat habitat = this[index1];
        if (habitat != null && habitat.Resources != null)
        {
          HabitatResourceList habitatResourceList = habitat.Resources.Clone();
          for (int index2 = 0; index2 < habitatResourceList.Count; ++index2)
          {
            if (habitatResourceList[index2].IsRestrictedResource)
            {
              restrictedResources.Add(habitat);
              break;
            }
          }
        }
      }
      return restrictedResources;
    }

    public HabitatList GetHabitatsWithResource(byte resourceId)
    {
      HabitatList habitatsWithResource = new HabitatList();
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null && habitat.Resources != null && habitat.Resources.Clone().IndexOf(resourceId, 0) >= 0)
          habitatsWithResource.Add(habitat);
      }
      return habitatsWithResource;
    }

    public Habitat GetRandomHabitatWithResource(Galaxy galaxy, byte resourceId)
    {
      HabitatList habitatsWithResource = this.GetHabitatsWithResource(resourceId);
      if (habitatsWithResource.Count <= 0)
        return (Habitat) null;
      int index = Galaxy.Rnd.Next(0, habitatsWithResource.Count);
      return habitatsWithResource[index];
    }

    public HabitatList GetHabitatsWithCompletedFacilities(PlanetaryFacilityType facilityType)
    {
      HabitatList completedFacilities = new HabitatList();
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null && habitat.Facilities != null && habitat.Facilities.FindCompletedByType(facilityType) != null)
          completedFacilities.Add(habitat);
      }
      return completedFacilities;
    }

    public HabitatList OrderByName()
    {
      HabitatList habitatList1 = new HabitatList();
      HabitatList habitatList2 = new HabitatList();
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        stringList.Add(habitat.Name);
        habitatList2.Add(habitat);
      }
      Habitat[] array = habitatList2.ToArray();
      Array.Sort<string, Habitat>(stringList.ToArray(), array);
      habitatList1.AddRange((IEnumerable<Habitat>) array);
      return habitatList1;
    }

    public HabitatList OrderByPopulationProportion(
      double populationProportionThresholdRatio,
      long minimumPopulation)
    {
      HabitatList habitatList1 = new HabitatList();
      HabitatList habitatList2 = new HabitatList();
      List<double> doubleList = new List<double>();
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat.Population != null)
        {
          long totalAmount = habitat.Population.TotalAmount;
          if (totalAmount >= minimumPopulation)
          {
            double num = (double) totalAmount / (double) habitat.MaximumPopulation;
            if (num <= populationProportionThresholdRatio)
            {
              doubleList.Add(num);
              habitatList2.Add(habitat);
            }
          }
        }
      }
      Habitat[] array = habitatList2.ToArray();
      Array.Sort<double, Habitat>(doubleList.ToArray(), array);
      Array.Reverse((Array) array);
      habitatList1.AddRange((IEnumerable<Habitat>) array);
      return habitatList1;
    }

    public HabitatList OrderByRevenue()
    {
      HabitatList habitatList = new HabitatList();
      List<double> doubleList = new List<double>();
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        doubleList.Add(habitat.AnnualRevenue);
      }
      Habitat[] array = this.ToArray();
      Array.Sort<double, Habitat>(doubleList.ToArray(), array);
      Array.Reverse((Array) array);
      habitatList.AddRange((IEnumerable<Habitat>) array);
      return habitatList;
    }

    public int GetCount(HabitatType habitatType)
    {
      int count = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == habitatType)
          ++count;
      }
      return count;
    }

    public Habitat FindShortestConstructionWaitQueueCloseToBuiltObject(
      BuiltObject builtObject,
      out double shortestWaitQueueTime)
    {
      shortestWaitQueueTime = double.MaxValue;
      Habitat closeToBuiltObject = (Habitat) null;
      foreach (Habitat colony in (SyncList<Habitat>) this)
      {
        if (colony.Empire != null && (colony.Empire != builtObject.Empire.Galaxy.IndependentEmpire || builtObject.Empire.PirateEmpireBaseHabitat != null) && colony.ConstructionQueue != null && colony.Empire.CanBuildBuiltObject(builtObject, colony) && (builtObject.Empire == null || builtObject.Empire.PirateEmpireBaseHabitat == null || colony.Empire == builtObject.Empire.Galaxy.IndependentEmpire || colony.Empire == builtObject.Empire))
        {
          double num = double.MaxValue;
          if (colony.ConstructionQueue != null)
            num = colony.ConstructionQueue.EstimateCurrentWaitQueueTime();
          double distance = builtObject.Empire.Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, colony.Xpos, colony.Ypos);
          if (distance > 0.0)
            num *= Math.Sqrt(distance);
          if (num < shortestWaitQueueTime)
          {
            shortestWaitQueueTime = num;
            closeToBuiltObject = colony;
          }
        }
      }
      if (closeToBuiltObject != null && closeToBuiltObject.ConstructionQueue != null && closeToBuiltObject.ConstructionQueue.ConstructionWaitQueue != null && closeToBuiltObject.ConstructionQueue.ConstructionWaitQueue.Count > 0)
        closeToBuiltObject = (Habitat) null;
      return closeToBuiltObject;
    }

    public Habitat FindShortestConstructionWaitQueue(
      BuiltObject builtObject,
      out double shortestWaitQueueTime)
    {
      return this.FindShortestConstructionWaitQueue(builtObject, out shortestWaitQueueTime, false);
    }

    public Habitat FindShortestConstructionWaitQueue(
      BuiltObject builtObject,
      out double shortestWaitQueueTime,
      bool allowLongWaitQueues)
    {
      return this.FindShortestConstructionWaitQueue(builtObject, out shortestWaitQueueTime, allowLongWaitQueues, true);
    }

    public Habitat FindShortestConstructionWaitQueue(
      BuiltObject builtObject,
      out double shortestWaitQueueTime,
      bool allowLongWaitQueues,
      bool allowUnsafeLocations)
    {
      shortestWaitQueueTime = double.MaxValue;
      Habitat constructionWaitQueue = (Habitat) null;
      int num1 = 3;
      Habitat habitat1 = (Habitat) null;
      foreach (Habitat habitat2 in (SyncList<Habitat>) this)
      {
        if (habitat2.Empire != null && habitat2.Empire != builtObject.Empire.Galaxy.IndependentEmpire && habitat2.ConstructionQueue != null && habitat2.Empire.CanBuildBuiltObject(builtObject, habitat2) && (allowUnsafeLocations || habitat2.Empire.CheckSafeToBuildAtLocation(habitat2)))
        {
          double num2 = double.MaxValue;
          if (habitat2.ConstructionQueue != null)
            num2 = habitat2.ConstructionQueue.EstimateCurrentWaitQueueTime();
          if (num2 < shortestWaitQueueTime)
          {
            bool flag = false;
            if (habitat2.ManufacturingQueue != null && habitat2.ManufacturingQueue.DeficientResources != null && habitat2.ManufacturingQueue.DeficientResources.Count >= num1)
              flag = true;
            if (!flag)
            {
              shortestWaitQueueTime = num2;
              constructionWaitQueue = habitat2;
            }
            else
              habitat1 = habitat2;
          }
        }
      }
      if (constructionWaitQueue == null && habitat1 != null)
        constructionWaitQueue = habitat1;
      if (!allowLongWaitQueues && constructionWaitQueue != null && constructionWaitQueue.ConstructionQueue != null && constructionWaitQueue.ConstructionQueue.ConstructionWaitQueue != null && constructionWaitQueue.ConstructionQueue.ConstructionWaitQueue.Count > 0)
        constructionWaitQueue = (Habitat) null;
      return constructionWaitQueue;
    }

    public Habitat GetFirstHabitatWithinRange(double x, double y, double range)
    {
      double num = range * range;
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitatWithinRange = this[index];
        if (habitatWithinRange != null && Galaxy.CalculateDistanceSquaredStatic(x, y, habitatWithinRange.Xpos, habitatWithinRange.Ypos) < num)
          return habitatWithinRange;
      }
      return (Habitat) null;
    }

    public Habitat GetFurthestHabitat(double x, double y, HabitatType type, double minimumRange)
    {
      double num1 = minimumRange * minimumRange;
      Habitat furthestHabitat = (Habitat) null;
      double num2 = 0.0;
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null && habitat.Type == type)
        {
          double distanceSquaredStatic = Galaxy.CalculateDistanceSquaredStatic(x, y, habitat.Xpos, habitat.Ypos);
          if (distanceSquaredStatic >= num1 && (furthestHabitat == null || distanceSquaredStatic > num2))
          {
            num2 = distanceSquaredStatic;
            furthestHabitat = habitat;
          }
        }
      }
      return furthestHabitat;
    }

    public HabitatList GetOwnedColonies(Empire empire)
    {
      HabitatList ownedColonies = new HabitatList();
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null && habitat.Owner == empire)
          ownedColonies.Add(habitat);
      }
      return ownedColonies;
    }

    public int CountMigrationFactorBelow(float migrationFactor)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null && !habitat.HasBeenDestroyed && (double) habitat.MigrationFactor < (double) migrationFactor)
          ++num;
      }
      return num;
    }

    public int CountPopulationAbove(long populationAmount)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null && !habitat.HasBeenDestroyed && habitat.Population != null && habitat.Population.TotalAmount > populationAmount)
          ++num;
      }
      return num;
    }

    public Habitat GetHighestHabitatIndex()
    {
      Habitat highestHabitatIndex = (Habitat) null;
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null && (highestHabitatIndex == null || habitat.HabitatIndex > highestHabitatIndex.HabitatIndex))
          highestHabitatIndex = habitat;
      }
      return highestHabitatIndex;
    }

    public HabitatList GetHabitatIndexHigherThan(int habitatIndex)
    {
      HabitatList habitatIndexHigherThan = new HabitatList();
      for (int index = 0; index < this.Count; ++index)
      {
        Habitat habitat = this[index];
        if (habitat != null && habitat.HabitatIndex > habitatIndex)
          habitatIndexHigherThan.Add(habitat);
      }
      return habitatIndexHigherThan;
    }

    public int CompareTo(object compareTo)
    {
      if (!(compareTo is HabitatList))
        throw new ApplicationException("Invalid object to compare to: must be a HabitatList");
      return ((List<Habitat>) compareTo).Count > 0 ? (this.Count > 0 ? this[0].Name.CompareTo(((List<Habitat>) compareTo)[0].Name) : -1) : (this.Count > 0 ? 1 : 0);
    }
  }
}
