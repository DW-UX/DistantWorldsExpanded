// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconGalaxy
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds;
using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaconDistantWorlds
{
  public static class BaconGalaxy
  {
    public static int priceReductionFactor = 1;
    public static bool tradeEverything = false;
    public static bool buildAnywhere = false;
    public static List<BuiltObject> manualTradeItems = (List<BuiltObject>) new BuiltObjectList();

    public static void GenerateEmpire(Galaxy galaxy, Empire empire, bool isPlayerEmpire)
    {
      if (isPlayerEmpire)
        ;
    }

    public static void RemoveAllGasClouds(Galaxy galaxy)
    {
      foreach (SystemInfo system in (SyncList<SystemInfo>) galaxy.Systems)
      {
        if (system.SystemStar != null && system.SystemStar.Category == HabitatCategoryType.GasCloud)
          galaxy.RemoveSystem(system);
      }
    }

    public static void GenerateRandomAsteroidsWithResource(
      Galaxy galaxy,
      Habitat sun,
      int count,
      bool withResources = false)
    {
      if (withResources)
      {
        for (int index = 0; index < count; ++index)
          BaconGalaxy.GenerateAsteroidWithResource(galaxy, sun);
      }
      else
      {
        for (int index = 0; index < count; ++index)
          BaconGalaxy.GeneratePlainAsteroid(galaxy, sun);
      }
    }

    public static void GeneratePlainAsteroid(
      Galaxy galaxy,
      Habitat sunOrPlanet,
      HabitatType rockType = HabitatType.Undefined)
    {
      if (rockType == HabitatType.Undefined)
      {
        double num = new Random().NextDouble();
        rockType = num >= 0.7 ? (num >= 0.9 ? HabitatType.Metal : HabitatType.Ice) : HabitatType.BarrenRock;
      }
      Habitat nearestSystemStar = sunOrPlanet;
      while (nearestSystemStar.Parent != null)
        nearestSystemStar = nearestSystemStar.Parent;
      double num1 = (double) Galaxy.Rnd.Next(6000, 14000);
      if (nearestSystemStar != sunOrPlanet)
      {
        num1 /= 10.0;
        while (num1 > (double) sunOrPlanet.Diameter * 2.5)
          num1 /= 1.5;
        while (num1 < (double) sunOrPlanet.Diameter * 1.5)
          num1 *= 1.2000000476837158;
      }
      double num2 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
      double num3 = sunOrPlanet.Xpos + Math.Cos(num2) * num1;
      double num4 = sunOrPlanet.Ypos + Math.Sin(num2) * num1;
      Habitat asteroid = galaxy.GenerateAsteroid(galaxy, sunOrPlanet, rockType);
      asteroid.OrbitDistance /= (short) 10;
      galaxy.AddHabitat(asteroid, nearestSystemStar);
      BaconBuiltObject.myMain._Game.PlayerEmpire.ResourceMap.SetResourcesKnown(asteroid, false);
    }

    public static void GenerateAsteroidWithResource(Galaxy galaxy, Habitat sun)
    {
      double num1 = (double) Galaxy.Rnd.Next(6000, 14000);
      double num2 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
      double num3 = sun.Xpos + Math.Cos(num2) * num1;
      double num4 = sun.Ypos + Math.Sin(num2) * num1;
      Habitat asteroid = galaxy.GenerateAsteroid(galaxy, sun, HabitatType.Metal);
      byte resourceId = (byte) Galaxy.Rnd.Next(0, 18);
      while (asteroid.Resources.ContainsId(resourceId))
        resourceId = (byte) Galaxy.Rnd.Next(0, 18);
      asteroid.Resources.Add(new HabitatResource(resourceId, Galaxy.Rnd.Next(50, 950)));
      galaxy.AddHabitat(asteroid, sun);
      BaconBuiltObject.myMain._Game.PlayerEmpire.ResourceMap.SetResourcesKnown(asteroid, false);
    }

    public static void GenerateAsteroids(
      Galaxy galaxy,
      Empire empire,
      Habitat habitat,
      int minAsteroids = 80,
      int maxAsteroids = 150)
    {
      double orbitDistance = (double) Galaxy.Rnd.Next(6000, 14000);
      double num1 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
      double x = habitat.Xpos + Math.Cos(num1) * orbitDistance;
      double y = habitat.Ypos + Math.Sin(num1) * orbitDistance;
      HabitatList asteroidField = galaxy.GenerateAsteroidField(Galaxy.Rnd.Next(minAsteroids, maxAsteroids), x, y, habitat, true, 2, (int) orbitDistance, 1.0, 1.0, HabitatType.BarrenRock);
      for (int index1 = 0; index1 < asteroidField.Count; ++index1)
      {
        int num2 = asteroidField[index1].Resources.Count<HabitatResource>();
        int num3 = Galaxy.Rnd.Next(0, 100);
        int num4 = Math.Max(0, (num3 >= 50 ? (num3 >= 75 ? (num3 >= 90 ? 3 : 2) : 1) : 0) - num2);
        for (int index2 = 0; index2 < num4; ++index2)
        {
          byte resourceId = (byte) Galaxy.Rnd.Next(0, 18);
          while (asteroidField[index1].Resources.ContainsId(resourceId))
            resourceId = (byte) Galaxy.Rnd.Next(0, 18);
          asteroidField[index1].Resources.Add(new HabitatResource(resourceId, Galaxy.Rnd.Next(50, 950)));
        }
      }
      galaxy.AddAsteroidField(asteroidField, habitat);
    }

    public static void SetEmpireDifficultyFactors(Empire empire)
    {
      if (empire.Name.Contains("Romulan"))
      {
        empire.ColonyCorruptionFactor = Galaxy.ColonyCorruptionFactorDefault * empire.DifficultyLevel;
        empire.ResearchRate = Galaxy.ResearchRateDefault / empire.DifficultyLevel;
        empire.PopulationGrowthRate = Galaxy.PopulationGrowthRateDefault / empire.DifficultyLevel;
        empire.MiningRate = Galaxy.MiningRateDefault / empire.DifficultyLevel * 5.0;
        empire.TargettingFactor = Math.Min(1.0, Galaxy.TargettingFactorDefault / Math.Sqrt(empire.DifficultyLevel));
        empire.CountermeasuresFactor = Math.Min(1.0, Galaxy.CountermeasuresFactorDefault / Math.Sqrt(empire.DifficultyLevel));
        empire.ColonyShipBuildSpeedRate = Galaxy.ColonyShipBuildSpeedRateDefault / empire.DifficultyLevel;
        empire.WarWearinessFactor = Galaxy.WarWearinessFactorDefault * empire.DifficultyLevel;
        empire.ColonyIncomeFactor = Galaxy.ColonyIncomeFactorDefault / empire.DifficultyLevel * 2.0;
      }
      else
      {
        empire.ColonyCorruptionFactor = Galaxy.ColonyCorruptionFactorDefault * empire.DifficultyLevel;
        empire.ResearchRate = Galaxy.ResearchRateDefault / empire.DifficultyLevel;
        empire.PopulationGrowthRate = Galaxy.PopulationGrowthRateDefault / empire.DifficultyLevel;
        empire.MiningRate = Galaxy.MiningRateDefault / empire.DifficultyLevel;
        empire.TargettingFactor = Galaxy.TargettingFactorDefault / Math.Sqrt(empire.DifficultyLevel);
        empire.CountermeasuresFactor = Galaxy.CountermeasuresFactorDefault / Math.Sqrt(empire.DifficultyLevel);
        empire.ColonyShipBuildSpeedRate = Galaxy.ColonyShipBuildSpeedRateDefault / empire.DifficultyLevel;
        empire.WarWearinessFactor = Galaxy.WarWearinessFactorDefault * empire.DifficultyLevel;
        empire.ColonyIncomeFactor = Galaxy.ColonyIncomeFactorDefault / empire.DifficultyLevel;
      }
    }

    public static bool CheckEmpireTerritoryCanBuildAtHabitat(
      Galaxy galaxy,
      Empire empire,
      Habitat habitat)
    {
      return BaconGalaxy.buildAnywhere || empire == galaxy.PlayerEmpire && galaxy.DetermineDefendingFirepower(habitat, empire) > 300;
    }

    public static TradeableItemList ResolveTradeableItemsColoniesBases(
      Galaxy galaxy,
      Empire giver,
      Empire receiver,
      bool refactorValuesForEmpire)
    {
      TradeableItemList list = new TradeableItemList();
      if (giver != null && receiver != null)
      {
        Habitat habitat = (Habitat) null;
        if (receiver.Capital != null)
          habitat = galaxy.FastFindNearestColony((double) (int) receiver.Capital.Xpos, (double) (int) receiver.Capital.Ypos, giver, 0);
        else if (receiver.PirateEmpireBaseHabitat != null)
          habitat = receiver.PirateEmpireBaseHabitat;
        HabitatList habitatList1 = new HabitatList();
        HabitatList habitatList2 = new HabitatList();
        if (giver.Colonies != null)
        {
          for (int index = 0; index < giver.Colonies.Count; ++index)
          {
            Habitat colony = giver.Colonies[index];
            if (colony != null && colony.Owner == giver && receiver.IsObjectAreaKnownToThisEmpire((StellarObject) colony))
            {
              Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(colony);
              if (BaconGalaxy.tradeEverything)
                habitatList2.Add(colony);
              else if (galaxy.Systems[habitatSystemStar.SystemIndex].IsDisputed && galaxy.Systems[habitatSystemStar.SystemIndex].DominantEmpire != null && galaxy.Systems[habitatSystemStar.SystemIndex].DominantEmpire.Empire == receiver)
              {
                if (!habitatList1.Contains(habitatSystemStar))
                  habitatList1.Add(habitatSystemStar);
                if (!habitatList2.Contains(colony))
                  habitatList2.Add(colony);
              }
            }
          }
        }
        for (int index = 0; index < habitatList2.Count; ++index)
        {
          Habitat colony = habitatList2[index];
          int num = galaxy.ValueColonyForEmpire(colony, giver);
          if (num >= 0 & refactorValuesForEmpire)
            num = galaxy.RefactorValueForEmpire(num, receiver, giver);
          if (num >= 0)
            list.Add(new TradeableItem(TradeableItemType.Colony, (object) colony, num));
        }
        HabitatList dominatedSystems = receiver.DetermineEmpireDominatedSystems(receiver, true);
        BuiltObjectList builtObjectList = new BuiltObjectList();
        builtObjectList.AddRange(giver.BuiltObjects.Where<BuiltObject>((Func<BuiltObject, bool>) (x =>
        {
          if (x.Role != BuiltObjectRole.Base)
            return false;
          return x.ParentHabitat == null || x.ParentHabitat.Population == null || x.ParentHabitat.Population.TotalAmount == 0L;
        })));
        builtObjectList.AddRange(giver.PrivateBuiltObjects.Where<BuiltObject>((Func<BuiltObject, bool>) (x =>
        {
          if (x.Role != BuiltObjectRole.Base)
            return false;
          return x.ParentHabitat == null || x.ParentHabitat.Population == null || x.ParentHabitat.Population.TotalAmount == 0L;
        })));
        for (int index = 0; index < builtObjectList.Count; ++index)
        {
          BuiltObject station = builtObjectList[index];
          if ((BaconGalaxy.tradeEverything || habitatList1.Contains(station.NearestSystemStar) || dominatedSystems.Contains(station.NearestSystemStar)) && receiver.IsObjectAreaKnownToThisEmpire((StellarObject) station))
          {
            int num = galaxy.ValueBaseForEmpire(station, giver);
            if (num >= 0 & refactorValuesForEmpire)
              num = galaxy.RefactorValueForEmpire(num, receiver, giver);
            if (num >= 0)
              list.Add(new TradeableItem(TradeableItemType.Base, (object) station, num));
          }
        }
        if (giver == galaxy.PlayerEmpire)
        {
          for (int index = 0; index < BaconGalaxy.manualTradeItems.Count; ++index)
          {
            BuiltObject manualTradeItem = BaconGalaxy.manualTradeItems[index];
            int num = manualTradeItem.Role != BuiltObjectRole.Base ? BaconBuiltObject.FindResalePriceOfShip(manualTradeItem, receiver) : galaxy.ValueBaseForEmpire(manualTradeItem, giver);
            if (num >= 0 & refactorValuesForEmpire)
              num = galaxy.RefactorValueForEmpire(num, receiver, giver);
            if (num >= 0)
              list.Add(new TradeableItem(TradeableItemType.Base, (object) manualTradeItem, num));
          }
          BaconGalaxy.manualTradeItems.Clear();
        }
        list.Add(new TradeableItem(TradeableItemType.Money, (object) 1000000.0, 1000000));
        list.Add(new TradeableItem(TradeableItemType.Money, (object) 10000000.0, 10000000));
        list.Add(new TradeableItem(TradeableItemType.Money, (object) 100000000.0, 100000000));
      }
      return BaconGalaxy.SortList(list);
    }

    public static TradeableItemList SortList(TradeableItemList list)
    {
      List<TradeableItem> source = new List<TradeableItem>();
      source.AddRange((IEnumerable<TradeableItem>) list);
      List<TradeableItem> list1 = source.OrderBy<TradeableItem, string>((Func<TradeableItem, string>) (x => x.Item.ToString())).ToList<TradeableItem>();
      list.Clear();
      list.AddRange((IEnumerable<TradeableItem>) list1);
      return list;
    }

    public static int RefactorValueForEmpire(
      Galaxy galaxy,
      int value,
      Empire requestingEmpire,
      Empire offeringEmpire)
    {
      double refactorForEmpire = galaxy.GetRefactorForEmpire(requestingEmpire, offeringEmpire);
      long num = (long) ((double) value * refactorForEmpire);
      if (num > 1073741823L)
        num = 1073741823L;
      return (int) num / BaconGalaxy.priceReductionFactor;
    }

    public static void SetShakturiBeacon(Main main)
    {
      if (main._Game.Galaxy.ShakturiTriggerHabitat != null)
      {
        main._Game.Galaxy.ShakturiTriggerHabitat.Ruin = (Ruin) null;
        main._Game.Galaxy.ShakturiTriggerHabitat = (Habitat) null;
      }
      main._Game.Galaxy.GenerateShakturiReturnTriggerRuins();
    }

    public static void SpawnShakturi(Main main)
    {
      BaconGalaxy.SetShakturiBeacon(main);
      main._Game.Galaxy.GenerateShakturi(main._Game.Galaxy.ShakturiTriggerHabitat);
    }

    public static void SpawnShakturiHere(Main main)
    {
      if (!(main._Game.SelectedObject is Habitat) || (main._Game.SelectedObject as Habitat).Type != HabitatType.Continental && (main._Game.SelectedObject as Habitat).Type != HabitatType.Desert && (main._Game.SelectedObject as Habitat).Type != HabitatType.Ice && (main._Game.SelectedObject as Habitat).Type != HabitatType.MarshySwamp && (main._Game.SelectedObject as Habitat).Type != HabitatType.Ocean && (main._Game.SelectedObject as Habitat).Type != HabitatType.BarrenRock && (main._Game.SelectedObject as Habitat).Type != HabitatType.Volcanic)
        return;
      if (main._Game.Galaxy.ShakturiTriggerHabitat != null)
      {
        main._Game.Galaxy.ShakturiTriggerHabitat.Ruin = (Ruin) null;
        main._Game.Galaxy.ShakturiTriggerHabitat = (Habitat) null;
      }
      main._Game.Galaxy.ShakturiTriggerHabitat = main._Game.SelectedObject as Habitat;
      main._Game.Galaxy.GenerateShakturi(main._Game.SelectedObject as Habitat);
    }

    public static bool ExecuteEventAction(
      Galaxy galaxy,
      EventAction eventAction,
      Empire targetEmpire,
      GameEvent gameEvent,
      bool flag)
    {
      if (eventAction.MessageTitle == "SaveStats" && BaconBuiltObject.myMain != null)
      {
        if (BaconBuiltObject.myMain != null)
          BaconMain.ProcessGameStats(BaconBuiltObject.myMain);
        flag = false;
        eventAction.ExecutionDate = BaconBuiltObject.myMain._Game.Galaxy.CurrentStarDate + (long) (Galaxy.RealSecondsInGalacticYear * 1000 / 360 * (int) BaconMain.statSaveIntervalInGameDays);
        EventActionExecutionPackage executionPackage = new EventActionExecutionPackage(eventAction, gameEvent, BaconBuiltObject.myMain._Game.PlayerEmpire);
        BaconBuiltObject.myMain._Game.Galaxy.DelayedActions.Add(executionPackage);
      }
      else if (eventAction.MessageTitle == "ProcessEmpireScienceShips" && BaconBuiltObject.myMain != null)
      {
        BaconEmpire.ProcessScienceShips(BaconBuiltObject.myMain);
        flag = false;
        eventAction.ExecutionDate = BaconBuiltObject.myMain._Game.Galaxy.CurrentStarDate + (long) (Galaxy.RealSecondsInGalacticYear * 1000 / 360 * Galaxy.Rnd.Next(26, 35));
        EventActionExecutionPackage executionPackage = new EventActionExecutionPackage(eventAction, gameEvent, BaconBuiltObject.myMain._Game.PlayerEmpire);
        BaconBuiltObject.myMain._Game.Galaxy.DelayedActions.Add(executionPackage);
      }
      else if (eventAction.MessageTitle == "missionExploreRuins" && BaconBuiltObject.myMain != null)
      {
        flag = false;
        BaconHabitat.ResolveScientificMissionExploreRuins(eventAction.Target as Habitat);
      }
      else if (eventAction.MessageTitle == "missionProspectForResources" && BaconBuiltObject.myMain != null)
      {
        flag = false;
        BaconHabitat.ResolveScientificMissionProspectForResources(eventAction.Target as Habitat);
      }
      else if (eventAction.MessageTitle.Contains("loan") && BaconBuiltObject.myMain != null)
      {
        BaconEmpire.MakeLoanPayment(eventAction, gameEvent);
        flag = false;
      }
      else if (eventAction.MessageTitle.Contains("ClearShipsAboutToBeDestroyed") && BaconBuiltObject.myMain != null)
      {
        BaconGalaxy.ClearShipsAboutToBeDestroyed(eventAction, gameEvent);
        flag = false;
      }
      return flag;
    }

    public static void ClearShipsAboutToBeDestroyed(EventAction eventAction, GameEvent gameEvent)
    {
      if (BaconBuiltObject.shipsToBeDestroyed != null)
        BaconBuiltObject.shipsToBeDestroyed.Clear();
      eventAction.ExecutionDate = BaconBuiltObject.myMain._Game.Galaxy.CurrentStarDate + (long) (Galaxy.RealSecondsInGalacticYear * 1000 / 360 * 10);
      EventActionExecutionPackage executionPackage = new EventActionExecutionPackage(eventAction, gameEvent, BaconBuiltObject.myMain._Game.PlayerEmpire);
      BaconBuiltObject.myMain._Game.Galaxy.DelayedActions.Add(executionPackage);
    }

    public static string ResolveDescription(CharacterEvent characterEvent, out string title)
    {
      string str = "";
      title = "";
      if (characterEvent.EventData is Dictionary<string, object>)
      {
        Dictionary<string, object> eventData = (Dictionary<string, object>) characterEvent.EventData;
        if (eventData.ContainsKey("spyMessage"))
        {
          List<string> stringList = (List<string>) eventData["spyMessage"];
          title = stringList[0];
          str = stringList[1];
        }
      }
      else
        str = TextResolver.GetText("Character Event Description Trade Income");
      return str;
    }

    public static Habitat FastFindNearestColonyAnyEmpire(
      double Xpos,
      double Ypos,
      bool includeIndependant,
      Func<Habitat, bool> condition = null)
    {
      if (BaconBuiltObject.myMain == null)
        return (Habitat) null;
      List<Habitat> habitatList = new List<Habitat>();
      if (includeIndependant)
      {
        foreach (Habitat independentColony in (SyncList<Habitat>) BaconBuiltObject.myMain._Game.Galaxy.IndependentColonies)
          habitatList.Add(independentColony);
      }
      foreach (Empire empire in (SyncList<Empire>) BaconBuiltObject.myMain._Game.Galaxy.Empires)
      {
        foreach (Habitat colony in (SyncList<Habitat>) empire.Colonies)
        {
          if (condition == null || condition(colony))
            habitatList.Add(colony);
        }
      }
      foreach (Empire pirateEmpire in (SyncList<Empire>) BaconBuiltObject.myMain._Game.Galaxy.PirateEmpires)
      {
        foreach (Habitat colony in (SyncList<Habitat>) pirateEmpire.Colonies)
        {
          if ((condition == null || condition(colony)) && !habitatList.Contains(colony))
            habitatList.Add(colony);
        }
      }
      double num = double.MaxValue;
      Habitat nearestColonyAnyEmpire = (Habitat) null;
      for (int index = 0; index < habitatList.Count; ++index)
      {
        double distanceSquaredStatic = Galaxy.CalculateDistanceSquaredStatic(Xpos, Ypos, habitatList[index].Xpos, habitatList[index].Ypos);
        if (distanceSquaredStatic < num)
        {
          nearestColonyAnyEmpire = habitatList[index];
          num = distanceSquaredStatic;
        }
      }
      return nearestColonyAnyEmpire;
    }

    public static void ResolveComponentDescriptionDetailed(
      Galaxy galaxy,
      Empire empire,
      Component component,
      ComponentImprovement improvement,
      ResearchNode project,
      out string[] descriptions,
      out string[] values)
    {
      descriptions = new string[8];
      values = new string[8];
      if (improvement == null)
        improvement = new ComponentImprovement(component);
      switch (improvement.ImprovedComponent.Type)
      {
        case ComponentType.WeaponBeam:
        case ComponentType.WeaponSuperBeam:
          descriptions[0] = TextResolver.GetText("Damage");
          values[0] = improvement.Value1.ToString();
          descriptions[1] = TextResolver.GetText("Range");
          values[1] = improvement.Value2.ToString();
          descriptions[2] = TextResolver.GetText("Energy Used");
          values[2] = improvement.Value3.ToString();
          descriptions[3] = TextResolver.GetText("Speed");
          values[3] = improvement.Value4.ToString();
          descriptions[4] = TextResolver.GetText("Damage Loss");
          values[4] = string.Format(TextResolver.GetText("X per 100 distance"), (object) improvement.Value5.ToString());
          descriptions[5] = TextResolver.GetText("Fire Rate");
          double num1 = (double) improvement.Value6 / 1000.0;
          values[5] = num1.ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
          break;
        case ComponentType.WeaponTorpedo:
        case ComponentType.WeaponBombard:
        case ComponentType.WeaponSuperTorpedo:
          descriptions[0] = TextResolver.GetText("Damage");
          values[0] = improvement.Value1.ToString();
          descriptions[1] = TextResolver.GetText("Range");
          values[1] = improvement.Value2.ToString();
          descriptions[2] = TextResolver.GetText("Energy Used");
          values[2] = improvement.Value3.ToString();
          descriptions[3] = TextResolver.GetText("Speed");
          values[3] = improvement.Value4.ToString();
          descriptions[4] = TextResolver.GetText("Damage Loss");
          values[4] = string.Format(TextResolver.GetText("X per 100 distance"), (object) improvement.Value5.ToString());
          descriptions[5] = TextResolver.GetText("Fire Rate");
          double num2 = (double) improvement.Value6 / 1000.0;
          values[5] = num2.ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
          descriptions[6] = TextResolver.GetText("Bombard Damage");
          double num3 = (double) improvement.Value7 * 1000000.0;
          values[6] = num3.ToString("0,,M");
          break;
        case ComponentType.WeaponMissile:
        case ComponentType.WeaponSuperMissile:
          descriptions[0] = TextResolver.GetText("Damage");
          values[0] = improvement.Value1.ToString();
          descriptions[1] = TextResolver.GetText("Range");
          values[1] = improvement.Value2.ToString();
          descriptions[2] = TextResolver.GetText("Energy Used");
          values[2] = improvement.Value3.ToString();
          descriptions[3] = TextResolver.GetText("Speed");
          values[3] = improvement.Value4.ToString();
          descriptions[4] = TextResolver.GetText("Damage Loss");
          values[4] = string.Format(TextResolver.GetText("X per 100 distance"), (object) improvement.Value5.ToString());
          descriptions[5] = TextResolver.GetText("Fire Rate");
          double num4 = (double) improvement.Value6 / 1000.0;
          values[5] = num4.ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
          descriptions[6] = TextResolver.GetText("Bombard Damage");
          double num5 = (double) improvement.Value7 * 1000000.0;
          values[6] = num5.ToString("0,,M");
          descriptions[7] = string.Format(TextResolver.GetText("Missiles are X less effective against armor"), (object) "50%");
          break;
        case ComponentType.WeaponPointDefense:
          descriptions[0] = TextResolver.GetText("Close-in weapons system that fires deadly bursts at enemy fighters, disabling or destroying them");
          descriptions[1] = TextResolver.GetText("Damage");
          string[] strArray1 = values;
          int num6 = improvement.Value1;
          string str1 = num6.ToString();
          strArray1[1] = str1;
          descriptions[2] = TextResolver.GetText("Range");
          string[] strArray2 = values;
          num6 = improvement.Value2;
          string str2 = num6.ToString();
          strArray2[2] = str2;
          descriptions[3] = TextResolver.GetText("Energy Used");
          string[] strArray3 = values;
          num6 = improvement.Value3;
          string str3 = num6.ToString();
          strArray3[3] = str3;
          descriptions[4] = TextResolver.GetText("Speed");
          string[] strArray4 = values;
          num6 = improvement.Value4;
          string str4 = num6.ToString();
          strArray4[4] = str4;
          descriptions[5] = TextResolver.GetText("Damage Loss");
          string[] strArray5 = values;
          string text1 = TextResolver.GetText("X per 100 distance");
          num6 = improvement.Value5;
          string str5 = num6.ToString();
          string str6 = string.Format(text1, (object) str5);
          strArray5[5] = str6;
          descriptions[6] = TextResolver.GetText("Fire Rate");
          double num7 = (double) improvement.Value6 / 1000.0;
          values[6] = num7.ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
          break;
        case ComponentType.WeaponIonCannon:
          descriptions[0] = TextResolver.GetText("Fires a bolt of ionized particles that disables weapons and engines of the target ship or base");
          descriptions[1] = TextResolver.GetText("Disabling Power");
          string[] strArray6 = values;
          int num8 = improvement.Value1;
          string str7 = num8.ToString();
          strArray6[1] = str7;
          descriptions[2] = TextResolver.GetText("Range");
          string[] strArray7 = values;
          num8 = improvement.Value2;
          string str8 = num8.ToString();
          strArray7[2] = str8;
          descriptions[3] = TextResolver.GetText("Energy Used");
          string[] strArray8 = values;
          num8 = improvement.Value3;
          string str9 = num8.ToString();
          strArray8[3] = str9;
          descriptions[4] = TextResolver.GetText("Speed");
          string[] strArray9 = values;
          num8 = improvement.Value4;
          string str10 = num8.ToString();
          strArray9[4] = str10;
          descriptions[5] = TextResolver.GetText("Power Loss");
          string[] strArray10 = values;
          string text2 = TextResolver.GetText("X per 100 distance");
          num8 = improvement.Value5;
          string str11 = num8.ToString();
          string str12 = string.Format(text2, (object) str11);
          strArray10[5] = str12;
          descriptions[6] = TextResolver.GetText("Fire Rate");
          double num9 = (double) improvement.Value6 / 1000.0;
          values[6] = num9.ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
          break;
        case ComponentType.WeaponIonPulse:
          descriptions[0] = TextResolver.GetText("Fires an omni-directional shockwave that disables weapons and engines of nearby ships and bases");
          descriptions[1] = TextResolver.GetText("Disabling Power");
          string[] strArray11 = values;
          int num10 = improvement.Value1;
          string str13 = num10.ToString();
          strArray11[1] = str13;
          descriptions[2] = TextResolver.GetText("Range");
          string[] strArray12 = values;
          num10 = improvement.Value2;
          string str14 = num10.ToString();
          strArray12[2] = str14;
          descriptions[3] = TextResolver.GetText("Energy Used");
          string[] strArray13 = values;
          num10 = improvement.Value3;
          string str15 = num10.ToString();
          strArray13[3] = str15;
          descriptions[4] = TextResolver.GetText("Speed");
          string[] strArray14 = values;
          num10 = improvement.Value4;
          string str16 = num10.ToString();
          strArray14[4] = str16;
          descriptions[5] = TextResolver.GetText("Power Loss");
          string[] strArray15 = values;
          string text3 = TextResolver.GetText("X per 100 distance");
          num10 = improvement.Value5;
          string str17 = num10.ToString();
          string str18 = string.Format(text3, (object) str17);
          strArray15[5] = str18;
          descriptions[6] = TextResolver.GetText("Fire Rate");
          double num11 = (double) improvement.Value6 / 1000.0;
          values[6] = num11.ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
          break;
        case ComponentType.WeaponIonDefense:
          descriptions[0] = TextResolver.GetText("Protects a ship or base against the disabling effects of Ion weapons");
          descriptions[1] = TextResolver.GetText("Ion Defense Strength");
          values[1] = improvement.Value1.ToString();
          break;
        case ComponentType.WeaponTractorBeam:
          descriptions[0] = TextResolver.GetText("Gravitic beam that pulls enemy ships towards you for capture or attack");
          descriptions[1] = TextResolver.GetText("Pulling Power");
          string[] strArray16 = values;
          int num12 = improvement.Value1;
          string str19 = num12.ToString();
          strArray16[1] = str19;
          descriptions[2] = TextResolver.GetText("Range");
          string[] strArray17 = values;
          num12 = improvement.Value2;
          string str20 = num12.ToString();
          strArray17[2] = str20;
          descriptions[3] = TextResolver.GetText("Energy Used");
          string[] strArray18 = values;
          num12 = improvement.Value3;
          string str21 = num12.ToString();
          strArray18[3] = str21;
          descriptions[4] = TextResolver.GetText("Speed");
          string[] strArray19 = values;
          num12 = improvement.Value4;
          string str22 = num12.ToString();
          strArray19[4] = str22;
          descriptions[5] = TextResolver.GetText("Power Loss");
          string[] strArray20 = values;
          string text4 = TextResolver.GetText("X per 100 distance");
          num12 = improvement.Value5;
          string str23 = num12.ToString();
          string str24 = string.Format(text4, (object) str23);
          strArray20[5] = str24;
          descriptions[6] = TextResolver.GetText("Fire Rate");
          double num13 = (double) improvement.Value6 / 1000.0;
          values[6] = num13.ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
          break;
        case ComponentType.WeaponGravityBeam:
          descriptions[0] = TextResolver.GetText("Gravitic beam that damages enemy ships with blasts of powerful gravity waves");
          descriptions[1] = TextResolver.GetText("Damage");
          string[] strArray21 = values;
          int num14 = improvement.Value1;
          string str25 = num14.ToString();
          strArray21[1] = str25;
          descriptions[2] = TextResolver.GetText("Range");
          string[] strArray22 = values;
          num14 = improvement.Value2;
          string str26 = num14.ToString();
          strArray22[2] = str26;
          descriptions[3] = TextResolver.GetText("Energy Used");
          string[] strArray23 = values;
          num14 = improvement.Value3;
          string str27 = num14.ToString();
          strArray23[3] = str27;
          descriptions[4] = TextResolver.GetText("Speed");
          string[] strArray24 = values;
          num14 = improvement.Value4;
          string str28 = num14.ToString();
          strArray24[4] = str28;
          descriptions[5] = TextResolver.GetText("Power Loss");
          string[] strArray25 = values;
          string text5 = TextResolver.GetText("X per 100 distance");
          num14 = improvement.Value5;
          string str29 = num14.ToString();
          string str30 = string.Format(text5, (object) str29);
          strArray25[5] = str30;
          descriptions[6] = TextResolver.GetText("Fire Rate");
          double num15 = (double) improvement.Value6 / 1000.0;
          values[6] = num15.ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
          break;
        case ComponentType.WeaponAreaGravity:
          descriptions[0] = TextResolver.GetText("Sends out a blast of pulsing gravity waves from a targetted point in space");
          descriptions[1] = TextResolver.GetText("Power");
          string[] strArray26 = values;
          int num16 = improvement.Value1;
          string str31 = num16.ToString();
          strArray26[1] = str31;
          descriptions[2] = TextResolver.GetText("Firing Range");
          string[] strArray27 = values;
          num16 = improvement.Value2;
          string str32 = num16.ToString();
          strArray27[2] = str32;
          descriptions[3] = TextResolver.GetText("Pull Range");
          string[] strArray28 = values;
          num16 = improvement.Value5;
          string str33 = num16.ToString();
          strArray28[3] = str33;
          descriptions[4] = TextResolver.GetText("Damage Range");
          string[] strArray29 = values;
          num16 = improvement.Value7;
          string str34 = num16.ToString("0");
          strArray29[4] = str34;
          descriptions[5] = TextResolver.GetText("Duration");
          double num17 = (double) improvement.Value2 / (double) improvement.Value4;
          string[] strArray30 = values;
          string text6 = TextResolver.GetText("X secs");
          double num18 = num17;
          string str35 = num18.ToString("0.00");
          string str36 = string.Format(text6, (object) str35);
          strArray30[5] = str36;
          descriptions[6] = TextResolver.GetText("Energy Used");
          string[] strArray31 = values;
          num16 = improvement.Value3;
          string str37 = num16.ToString();
          strArray31[6] = str37;
          descriptions[7] = TextResolver.GetText("Fire Rate");
          double num19 = (double) improvement.Value6 / 1000.0;
          string[] strArray32 = values;
          num18 = num19;
          string str38 = num18.ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
          strArray32[7] = str38;
          break;
        case ComponentType.AssaultPod:
          descriptions[0] = TextResolver.GetText("Short-range shuttles that allow boarding and capture of enemy ships or bases");
          descriptions[1] = TextResolver.GetText("Assault Strength");
          string[] strArray33 = values;
          int num20 = improvement.Value1;
          string str39 = num20.ToString();
          strArray33[1] = str39;
          descriptions[2] = TextResolver.GetText("Boarding Range");
          string[] strArray34 = values;
          num20 = improvement.Value2;
          string str40 = num20.ToString();
          strArray34[2] = str40;
          descriptions[3] = TextResolver.GetText("Energy Used");
          string[] strArray35 = values;
          num20 = improvement.Value3;
          string str41 = num20.ToString();
          strArray35[3] = str41;
          descriptions[4] = TextResolver.GetText("Speed");
          string[] strArray36 = values;
          num20 = improvement.Value4;
          string str42 = num20.ToString();
          strArray36[4] = str42;
          descriptions[5] = TextResolver.GetText("Shield Penetration");
          string[] strArray37 = values;
          num20 = improvement.Value5;
          string str43 = num20.ToString();
          strArray37[5] = str43;
          descriptions[6] = TextResolver.GetText("Launch Rate");
          double num21 = (double) improvement.Value6 / 1000.0;
          values[6] = num21.ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
          break;
        case ComponentType.HyperDeny:
          descriptions[0] = TextResolver.GetText("Projects a powerful gravity well that prevents nearby ships from initiating a hyperjump");
          descriptions[1] = TextResolver.GetText("Range");
          string[] strArray38 = values;
          int num22 = improvement.Value2;
          string str44 = num22.ToString();
          strArray38[1] = str44;
          descriptions[2] = TextResolver.GetText("Energy Used");
          string[] strArray39 = values;
          num22 = improvement.Value3;
          string str45 = num22.ToString();
          strArray39[2] = str45;
          break;
        case ComponentType.HyperStop:
          descriptions[0] = TextResolver.GetText("Projects a powerful gravity well that pulls enemy ships out of hyperspace within a defined range");
          descriptions[1] = TextResolver.GetText("Hyper Stop Range");
          values[1] = improvement.Value2.ToString();
          break;
        case ComponentType.WeaponAreaDestruction:
        case ComponentType.WeaponSuperArea:
          descriptions[0] = TextResolver.GetText("Damage");
          values[0] = improvement.Value1.ToString();
          descriptions[1] = TextResolver.GetText("Range");
          values[1] = improvement.Value2.ToString();
          descriptions[2] = TextResolver.GetText("Energy Used");
          values[2] = improvement.Value3.ToString();
          descriptions[3] = TextResolver.GetText("Speed");
          values[3] = improvement.Value4.ToString();
          descriptions[4] = TextResolver.GetText("Damage Loss");
          values[4] = string.Format(TextResolver.GetText("X per 100 distance"), (object) improvement.Value5.ToString());
          descriptions[5] = TextResolver.GetText("Fire Rate");
          double num23 = (double) improvement.Value6 / 1000.0;
          values[5] = num23.ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
          break;
        case ComponentType.FighterBay:
          descriptions[0] = TextResolver.GetText("Provides facilities for manufacture, storage and repair of star fighters and bombers aboard ships or bases");
          descriptions[1] = TextResolver.GetText("Fighter Capacity");
          int num24 = improvement.Value1 / 10;
          string[] strArray40 = values;
          int num25 = num24;
          string str46 = num25.ToString();
          strArray40[1] = str46;
          descriptions[2] = TextResolver.GetText("Repair Rate");
          string[] strArray41 = values;
          num25 = improvement.Value2;
          string str47 = num25.ToString();
          strArray41[2] = str47;
          break;
        case ComponentType.Armor:
          descriptions[0] = TextResolver.GetText("Rating");
          string[] strArray42 = values;
          int num26 = improvement.Value1;
          string str48 = num26.ToString();
          strArray42[0] = str48;
          descriptions[1] = TextResolver.GetText("Reactive Rating");
          string[] strArray43 = values;
          num26 = improvement.Value2;
          string str49 = num26.ToString();
          strArray43[1] = str49;
          break;
        case ComponentType.Shields:
          descriptions[0] = TextResolver.GetText("Strength");
          values[0] = improvement.Value1.ToString();
          descriptions[1] = TextResolver.GetText("Recharge Rate");
          double num27 = (double) improvement.Value2 / 10.0;
          values[1] = num27.ToString("0.#");
          break;
        case ComponentType.ShieldRecharge:
          descriptions[0] = TextResolver.GetText("Restores the shield levels of nearby friendly ships when their shields drop below 50%");
          descriptions[1] = TextResolver.GetText("Recharge Range");
          string[] strArray44 = values;
          int num28 = improvement.Value1;
          string str50 = num28.ToString();
          strArray44[1] = str50;
          descriptions[2] = TextResolver.GetText("Max Recharge Amount");
          string[] strArray45 = values;
          num28 = improvement.Value2;
          string str51 = num28.ToString();
          strArray45[2] = str51;
          descriptions[3] = TextResolver.GetText("Energy Required");
          string[] strArray46 = values;
          num28 = improvement.Value3;
          string str52 = num28.ToString();
          strArray46[3] = str52;
          break;
        case ComponentType.EngineMainThrust:
          descriptions[0] = TextResolver.GetText("Maximum Thrust");
          string[] strArray47 = values;
          int num29 = improvement.Value1;
          string str53 = num29.ToString();
          strArray47[0] = str53;
          descriptions[1] = TextResolver.GetText("Max Energy Usage");
          string[] strArray48 = values;
          num29 = improvement.Value2;
          string str54 = num29.ToString();
          strArray48[1] = str54;
          descriptions[2] = TextResolver.GetText("Cruise Thrust");
          string[] strArray49 = values;
          num29 = improvement.Value3;
          string str55 = num29.ToString();
          strArray49[2] = str55;
          descriptions[3] = TextResolver.GetText("Cruise Energy Usage");
          string[] strArray50 = values;
          num29 = improvement.Value4;
          string str56 = num29.ToString();
          strArray50[3] = str56;
          break;
        case ComponentType.EngineVectoring:
          descriptions[0] = TextResolver.GetText("Thrust");
          string[] strArray51 = values;
          int num30 = improvement.Value1;
          string str57 = num30.ToString();
          strArray51[0] = str57;
          descriptions[1] = TextResolver.GetText("Energy Usage");
          string[] strArray52 = values;
          num30 = improvement.Value2;
          string str58 = num30.ToString();
          strArray52[1] = str58;
          break;
        case ComponentType.HyperDrive:
          descriptions[0] = TextResolver.GetText("Speed");
          string[] strArray53 = values;
          int num31 = improvement.Value1;
          string str59 = num31.ToString();
          strArray53[0] = str59;
          descriptions[1] = TextResolver.GetText("Energy Usage");
          string[] strArray54 = values;
          num31 = improvement.Value2;
          string str60 = num31.ToString();
          strArray54[1] = str60;
          descriptions[2] = TextResolver.GetText("Typical Jump Initiation");
          string[] strArray55 = values;
          num31 = improvement.Value3;
          string str61 = num31.ToString() + " " + TextResolver.GetText("seconds abbreviation");
          strArray55[2] = str61;
          int num32 = improvement.Value4;
          int num33 = improvement.Value5;
          if (num32 <= 0 || num33 <= 0)
            break;
          double num34 = Convert.ToDouble(num32) / Convert.ToDouble(num33);
          descriptions[3] = "Gravity well effect";
          values[3] = string.Format("{0:P2}.", (object) num34);
          break;
        case ComponentType.Reactor:
          descriptions[0] = TextResolver.GetText("Energy Output");
          values[0] = improvement.Value1.ToString();
          descriptions[1] = TextResolver.GetText("Storage Capacity");
          values[1] = improvement.Value2.ToString();
          descriptions[2] = TextResolver.GetText("Fuel Type");
          values[2] = galaxy.ResourceSystem.Resources[(int) (byte) improvement.Value4].Name;
          double num35 = (double) improvement.Value3 / 1000.0 / (double) improvement.Value2 * 1000.0;
          string str62 = string.Format(TextResolver.GetText("X fuel units per 1000 energy units"), (object) num35.ToString("0.00"));
          descriptions[3] = str62;
          values[3] = string.Empty;
          break;
        case ComponentType.EnergyCollector:
          descriptions[0] = TextResolver.GetText("Potential Energy");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.ExtractorMine:
        case ComponentType.ExtractorGasExtractor:
        case ComponentType.ExtractorLuxury:
          descriptions[0] = TextResolver.GetText("Extraction");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.ManufacturerWeaponsPlant:
        case ComponentType.ManufacturerEnergyPlant:
        case ComponentType.ManufacturerHighTechPlant:
          descriptions[0] = TextResolver.GetText("Capacity");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.StorageFuel:
          descriptions[0] = TextResolver.GetText("Capacity");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.StorageCargo:
          descriptions[0] = TextResolver.GetText("Capacity");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.StorageTroop:
          descriptions[0] = TextResolver.GetText("Capacity");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.StoragePassenger:
          descriptions[0] = TextResolver.GetText("Capacity");
          values[0] = improvement.Value1.ToString("0,K");
          break;
        case ComponentType.StorageDockingBay:
          descriptions[0] = TextResolver.GetText("Cargo Throughput");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.SensorProximityArray:
          descriptions[0] = TextResolver.GetText("Range");
          string[] strArray56 = values;
          int num36 = improvement.Value1;
          string str63 = num36.ToString();
          strArray56[0] = str63;
          descriptions[1] = TextResolver.GetText("Hyperjump Tracking");
          string[] strArray57 = values;
          num36 = improvement.Value2;
          string str64 = num36.ToString("0") + "%";
          strArray57[1] = str64;
          break;
        case ComponentType.SensorResourceProfileSensor:
          descriptions[0] = TextResolver.GetText("Range");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.SensorLongRange:
          descriptions[0] = TextResolver.GetText("Range");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.SensorTraceScanner:
          descriptions[0] = TextResolver.GetText("Allows scanning another ship or base to determine its cargo, onboard troops and component status");
          descriptions[1] = TextResolver.GetText("Scan Range");
          string[] strArray58 = values;
          int num37 = improvement.Value1;
          string str65 = num37.ToString();
          strArray58[1] = str65;
          descriptions[2] = TextResolver.GetText("Scan Power");
          string[] strArray59 = values;
          num37 = improvement.Value2;
          string str66 = num37.ToString();
          strArray59[2] = str66;
          break;
        case ComponentType.SensorScannerJammer:
          descriptions[0] = TextResolver.GetText("Jams enemy trace scanners, preventing them from scanning the contents of a ship");
          descriptions[1] = TextResolver.GetText("Jamming Power");
          values[1] = improvement.Value2.ToString();
          break;
        case ComponentType.SensorStealth:
          descriptions[0] = TextResolver.GetText("Stealth Rating");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.ComputerTargetting:
          descriptions[0] = TextResolver.GetText("Effectiveness");
          values[0] = "+" + improvement.Value1.ToString() + "%";
          break;
        case ComponentType.ComputerTargettingFleet:
          descriptions[0] = TextResolver.GetText("Fleet Bonus");
          values[0] = "+" + improvement.Value2.ToString() + "%";
          break;
        case ComponentType.ComputerCountermeasures:
          descriptions[0] = TextResolver.GetText("Effectiveness");
          values[0] = "+" + improvement.Value1.ToString() + "%";
          break;
        case ComponentType.ComputerCountermeasuresFleet:
          descriptions[0] = TextResolver.GetText("Fleet Bonus");
          values[0] = "+" + improvement.Value2.ToString() + "%";
          break;
        case ComponentType.ComputerCommandCenter:
          descriptions[0] = TextResolver.GetText("Maintenance savings");
          values[0] = improvement.Value1.ToString() + "%";
          break;
        case ComponentType.ComputerCommerceCenter:
          descriptions[0] = TextResolver.GetText("Trade bonuses");
          double num38 = (double) improvement.Value1 / 10.0;
          values[0] = num38.ToString("0.0") + "%";
          break;
        case ComponentType.LabsWeaponsLab:
        case ComponentType.LabsEnergyLab:
        case ComponentType.LabsHighTechLab:
          descriptions[0] = TextResolver.GetText("Research Output");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.ConstructionBuild:
          descriptions[0] = TextResolver.GetText("Construction Speed");
          values[0] = improvement.Value1.ToString();
          int constructionSizeForYard = galaxy.DetermineMaximumConstructionSizeForYard(empire.Research.TechTree, project);
          int num39 = constructionSizeForYard * 3;
          descriptions[1] = TextResolver.GetText("Maximum ship size");
          values[1] = constructionSizeForYard.ToString();
          descriptions[2] = TextResolver.GetText("Maximum base size");
          values[2] = num39.ToString();
          break;
        case ComponentType.HabitationLifeSupport:
          descriptions[0] = TextResolver.GetText("Support Size");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.HabitationHabModule:
          descriptions[0] = TextResolver.GetText("Support Size");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.DamageControl:
          descriptions[0] = TextResolver.GetText("Damage Reduction") + " %";
          double num40 = (double) improvement.Value1 / 10.0;
          values[0] = num40.ToString("0.0") + "%";
          descriptions[1] = TextResolver.GetText("Repair Component");
          if (improvement.Value2 <= 0)
          {
            values[1] = "(" + TextResolver.GetText("None") + ")";
            break;
          }
          double num41 = (double) improvement.Value2;
          values[1] = num41.ToString() + " " + TextResolver.GetText("seconds abbreviation");
          break;
        case ComponentType.HabitationMedicalCenter:
          descriptions[0] = TextResolver.GetText("Effectiveness");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.HabitationRecreationCenter:
          descriptions[0] = TextResolver.GetText("Value");
          values[0] = improvement.Value1.ToString();
          break;
        case ComponentType.HabitationColonization:
          descriptions[0] = TextResolver.GetText("Population Amount");
          values[0] = improvement.Value1.ToString("0,,M");
          break;
        case ComponentType.WeaponPhaser:
        case ComponentType.WeaponSuperPhaser:
          descriptions[0] = TextResolver.GetText("Damage");
          values[0] = improvement.Value1.ToString();
          descriptions[1] = TextResolver.GetText("Range");
          values[1] = improvement.Value2.ToString();
          descriptions[2] = TextResolver.GetText("Energy Used");
          values[2] = improvement.Value3.ToString();
          descriptions[3] = TextResolver.GetText("Speed");
          values[3] = improvement.Value4.ToString();
          descriptions[4] = TextResolver.GetText("Damage Loss");
          values[4] = string.Format(TextResolver.GetText("X per 100 distance"), (object) improvement.Value5.ToString());
          descriptions[5] = TextResolver.GetText("Fire Rate");
          double num42 = (double) improvement.Value6 / 1000.0;
          values[5] = num42.ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
          descriptions[6] = TextResolver.GetText("Phasers Description");
          break;
        case ComponentType.WeaponRailGun:
        case ComponentType.WeaponSuperRailGun:
          descriptions[0] = TextResolver.GetText("Damage");
          values[0] = improvement.Value1.ToString();
          descriptions[1] = TextResolver.GetText("Range");
          values[1] = improvement.Value2.ToString();
          descriptions[2] = TextResolver.GetText("Energy Used");
          values[2] = improvement.Value3.ToString();
          descriptions[3] = TextResolver.GetText("Speed");
          values[3] = improvement.Value4.ToString();
          descriptions[4] = TextResolver.GetText("Damage Loss");
          values[4] = string.Format(TextResolver.GetText("X per 100 distance"), (object) improvement.Value5.ToString());
          descriptions[5] = TextResolver.GetText("Fire Rate");
          double num43 = (double) improvement.Value6 / 1000.0;
          values[5] = num43.ToString("0.00") + " " + TextResolver.GetText("seconds abbreviation");
          descriptions[6] = TextResolver.GetText("Bombard Damage");
          double num44 = (double) improvement.Value7 * 1000000.0;
          values[6] = num44.ToString("0,,M");
          string text7 = TextResolver.GetText("Rail Guns Description");
          descriptions[7] = text7;
          break;
        case ComponentType.EnergyToFuel:
          descriptions[0] = TextResolver.GetText("Fuel Production Rate");
          values[0] = improvement.Value1.ToString();
          break;
      }
    }

    public static double CalculateBuiltObjectLootingValue(BuiltObject builtObject)
    {
      double objectLootingValue = 0.0;
      if (builtObject != null)
        objectLootingValue = 1.0 * (double) builtObject.Size;
      if (builtObject.Empire != null && builtObject.Empire.PirateEmpireBaseHabitat != null)
        objectLootingValue *= Galaxy.ShipMarkupFactorPirates / 2.5;
      else if (builtObject.Empire != null)
        objectLootingValue *= Galaxy.ShipMarkupFactor / 5.0;
      return objectLootingValue;
    }

    public static Empire GenerateEmpire(Main main, Race race = null, int governmentId = -1)
    {
      Empire empire = main._Game.Galaxy.Empires[0];
      Habitat selectedObject = main._Game.SelectedObject as Habitat;
      if (selectedObject.Empire != null)
        empire = selectedObject.Empire;
      if (!(main._Game.SelectedObject is Habitat))
        return empire;
      Galaxy galaxy = main._Game.Galaxy;
      bool isPlayerEmpire = false;
      string empireName = "";
      if (race == null)
        race = selectedObject.Population == null || selectedObject.Population.Count <= 0 ? empire.DominantRace : selectedObject.Population[0].Race;
      int designNameIndex = race.DesignNameIndex;
      if (governmentId == -1)
        governmentId = empire.GovernmentId;
      double homeSystemFactor = 1.0;
      string homeSystemDescription = "Normal";
      int age = 0;
      double techLevel = 1.0;
      double corruptionMultiplier = 1.0;
      double expansion = 1.0;
      GameOptions gameOptions = (GameOptions) null;
      VictoryConditions globalVictoryConditions = (VictoryConditions) null;
      string name = race.Name;
      return main._Game.Galaxy.GenerateEmpire(main._Game.Galaxy, isPlayerEmpire, empireName, selectedObject, race, designNameIndex, governmentId, homeSystemFactor, homeSystemDescription, age, techLevel, corruptionMultiplier, out expansion, gameOptions, globalVictoryConditions);
    }
  }
}
