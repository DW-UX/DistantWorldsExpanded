// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconHabitat
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds;
using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BaconDistantWorlds
{
    public static class BaconHabitat
    {
        public static float myTerritoryMultiplier = 2f;
        public static bool alwaysShowAsteroidColonies = true;
        public static bool allowAsteroidColonies = false;
        public static bool allowInfrastructureImprovements = false;
        public static int asteroidColonyCost = 50000;
        public static float asteroidColonyPrevalenceDivisor = 100f;
        public static int infrastructureSpendingPerDevelopmentLevel = 50000;
        public static int maxInfrastructureInvestmentAllowed = 1000000;
        public static float infrasetuctureDurability = 0.9f;
        public static long colonyInfrastructureSpendingPopulationFactor = 300000000;
        public static double marketPriceUpdateChance = 1.0;
        public static bool addSalesTax = true;
        public static float pirateControlLevelToBuildShipsAtIndependentPlanets = 0.9f;
        public static int pirateBaseTroops = 7;
        public static int pirateFortressTroops = 12;
        public static int pirateCriminalNetworkTroops = 18;
        public static long pirateMaxPopulationInfluence = 1500000000;

        public static float TerritoryMultipler(Habitat planet)
        {
            float num = 1f;
            if (planet != null && planet.Empire != null && planet.Empire.Name.Contains("Romulan"))
                num = BaconHabitat.myTerritoryMultiplier;
            if (planet != null && planet.Population != null && planet.Population.TotalAmount < 1000000L)
                num = 0.0f;
            return num;
        }

        public static void CreateNewEmpire(Habitat habitat, Galaxy galaxy)
        {
            GovernmentAttributesList suitableGovermentTypes = Empire.DetermineMostSuitableGovermentTypes(habitat.Population.DominantRace, Empire.ResolveDefaultAllowableGovernmentTypes(habitat.Population.DominantRace));
            EmpirePolicy policy = galaxy.LoadEmpirePolicy(habitat.Population.DominantRace, false);
            Empire newEmpire = new Empire(galaxy, "", habitat, habitat.Population.DominantRace, suitableGovermentTypes[0].GovernmentId, 1.0, policy);
            habitat.IsRefuellingDepot = true;
            int num1 = 1;
            int num2 = 2;
            int num3 = 1;
            int num4 = 5;
            int num5 = 0;
            int num6 = 1;
            EmpireList empiresToExclude = new EmpireList();
            Empire nearestEmpireCapital = galaxy.FindNearestEmpireCapital(habitat.Xpos, habitat.Ypos, empiresToExclude);
            if (nearestEmpireCapital != null && nearestEmpireCapital.Research != null && !nearestEmpireCapital.CheckEmpireHasHyperDriveTech(nearestEmpireCapital))
            {
                newEmpire.Research.TechTree = Galaxy.ResearchNodeDefinitionsStatic.SetTechTreeLevel(galaxy, newEmpire.Research.TechTree, newEmpire.DominantRace, 0.0, false);
                ResearchNode specialFunctionCode = newEmpire.Research.TechTree.FindNodeBySpecialFunctionCode(2);
                if (specialFunctionCode != null)
                    specialFunctionCode.IsEnabled = true;
                newEmpire.Research.Update(newEmpire.DominantRace);
                newEmpire.ReviewResearchAbilities();
                newEmpire.ReviewDesignsBuiltObjectsImprovedComponents();
                num1 = 0;
                num2 = 0;
                num3 = 0;
                num4 = 0;
                num5 = 0;
                num6 = 0;
            }
            newEmpire.TakeOwnershipOfColony(habitat, newEmpire);
            newEmpire.ControlColonization = AutomationLevel.FullyAutomated;
            newEmpire.ControlColonyDevelopment = true;
            newEmpire.ControlColonyStockLevels = true;
            newEmpire.ControlColonyTaxRates = true;
            newEmpire.ControlDesigns = true;
            newEmpire.ControlDiplomacyGifts = AutomationLevel.FullyAutomated;
            newEmpire.ControlDiplomacyOffense = AutomationLevel.FullyAutomated;
            newEmpire.ControlDiplomacyTreaties = AutomationLevel.FullyAutomated;
            newEmpire.ControlMilitaryAttacks = AutomationLevel.FullyAutomated;
            newEmpire.ControlMilitaryFleets = true;
            newEmpire.ControlStateConstruction = AutomationLevel.FullyAutomated;
            newEmpire.ControlTroopGeneration = true;
            newEmpire.ControlAgentAssignment = AutomationLevel.FullyAutomated;
            newEmpire.ControlResearch = true;
            newEmpire.ControlPopulationPolicy = true;
            newEmpire.ControlColonyFacilities = AutomationLevel.FullyAutomated;
            newEmpire.ControlCharacterLocations = true;
            newEmpire.ControlOfferPirateMissions = AutomationLevel.FullyAutomated;
            if (habitat.Population.DominantRace != null)
                newEmpire.DesignPictureFamilyIndex = habitat.Population.DominantRace.DesignPictureFamilyIndex;
            newEmpire.GenerateDesignSpecifications(galaxy, habitat.Population.DominantRace, false, habitat.Population.DominantRace.Name);
            newEmpire.InitiateConstruction = false;
            newEmpire.DoTasks();
            newEmpire.InitiateConstruction = true;
            double x;
            double y;
            for (int index = 0; index < num1; ++index)
            {
                Design newest = newEmpire.Designs.FindNewest(BuiltObjectSubRole.GasMiningShip);
                if (newest != null)
                {
                    ++newest.BuildCount;
                    BuiltObject objectFromDesign = newEmpire.GenerateBuiltObjectFromDesign(newest, galaxy.GenerateBuiltObjectName(newest, habitat), false, habitat.Xpos + 100.0, habitat.Ypos - 50.0);
                    objectFromDesign.ParentHabitat = habitat;
                    objectFromDesign.DateBuilt = galaxy.CurrentStarDate;
                    objectFromDesign.DateRetrofit = galaxy.CurrentStarDate;
                    galaxy.SelectRelativeParkingPoint(out x, out y);
                    objectFromDesign.ParentOffsetX = x;
                    objectFromDesign.ParentOffsetY = y;
                    objectFromDesign.Heading = galaxy.SelectRandomHeading();
                }
            }
            for (int index = 0; index < num1; ++index)
            {
                Design newest = newEmpire.Designs.FindNewest(BuiltObjectSubRole.MiningShip);
                if (newest != null)
                {
                    ++newest.BuildCount;
                    BuiltObject objectFromDesign = newEmpire.GenerateBuiltObjectFromDesign(newest, galaxy.GenerateBuiltObjectName(newest, habitat), false, habitat.Xpos + 50.0, habitat.Ypos + 100.0);
                    objectFromDesign.ParentHabitat = habitat;
                    objectFromDesign.DateBuilt = galaxy.CurrentStarDate;
                    objectFromDesign.DateRetrofit = galaxy.CurrentStarDate;
                    galaxy.SelectRelativeParkingPoint(out x, out y);
                    objectFromDesign.ParentOffsetX = x;
                    objectFromDesign.ParentOffsetY = y;
                    objectFromDesign.Heading = galaxy.SelectRandomHeading();
                }
            }
            for (int index = 0; index < num2; ++index)
            {
                Design newest = newEmpire.Designs.FindNewest(BuiltObjectSubRole.ConstructionShip);
                if (newest != null)
                {
                    ++newest.BuildCount;
                    BuiltObject objectFromDesign = newEmpire.GenerateBuiltObjectFromDesign(newest, galaxy.GenerateBuiltObjectName(newest, habitat), true, habitat.Xpos, habitat.Ypos);
                    objectFromDesign.ParentHabitat = habitat;
                    objectFromDesign.DateBuilt = galaxy.CurrentStarDate;
                    objectFromDesign.DateRetrofit = galaxy.CurrentStarDate;
                    galaxy.SelectRelativeParkingPoint(out x, out y);
                    objectFromDesign.ParentOffsetX = x;
                    objectFromDesign.ParentOffsetY = y;
                    objectFromDesign.Heading = galaxy.SelectRandomHeading();
                }
            }
            for (int index = 0; index < num3; ++index)
            {
                Design newest = newEmpire.Designs.FindNewest(BuiltObjectSubRole.ExplorationShip);
                if (newest != null)
                {
                    ++newest.BuildCount;
                    BuiltObject objectFromDesign = newEmpire.GenerateBuiltObjectFromDesign(newest, galaxy.GenerateBuiltObjectName(newest, habitat), true, habitat.Xpos, habitat.Ypos);
                    objectFromDesign.ParentHabitat = habitat;
                    objectFromDesign.DateBuilt = galaxy.CurrentStarDate;
                    objectFromDesign.DateRetrofit = galaxy.CurrentStarDate;
                    galaxy.SelectRelativeParkingPoint(out x, out y);
                    objectFromDesign.ParentOffsetX = x;
                    objectFromDesign.ParentOffsetY = y;
                    objectFromDesign.Heading = galaxy.SelectRandomHeading();
                }
            }
            for (int index = 0; index < num4; ++index)
            {
                Design newest = newEmpire.Designs.FindNewest(BuiltObjectSubRole.SmallFreighter);
                if (newest != null)
                {
                    ++newest.BuildCount;
                    BuiltObject objectFromDesign = newEmpire.GenerateBuiltObjectFromDesign(newest, galaxy.GenerateBuiltObjectName(newest, habitat), false, habitat.Xpos - 50.0, habitat.Ypos - 100.0);
                    objectFromDesign.ParentHabitat = habitat;
                    objectFromDesign.DateBuilt = galaxy.CurrentStarDate;
                    objectFromDesign.DateRetrofit = galaxy.CurrentStarDate;
                    galaxy.SelectRelativeParkingPoint(out x, out y);
                    objectFromDesign.ParentOffsetX = x;
                    objectFromDesign.ParentOffsetY = y;
                    objectFromDesign.Heading = galaxy.SelectRandomHeading();
                }
            }
            for (int index = 0; index < num5; ++index)
            {
                Design newest = newEmpire.Designs.FindNewest(BuiltObjectSubRole.MediumFreighter);
                if (newest != null)
                {
                    ++newest.BuildCount;
                    BuiltObject objectFromDesign = newEmpire.GenerateBuiltObjectFromDesign(newest, galaxy.GenerateBuiltObjectName(newest, habitat), false, habitat.Xpos - 50.0, habitat.Ypos - 100.0);
                    objectFromDesign.ParentHabitat = habitat;
                    objectFromDesign.DateBuilt = galaxy.CurrentStarDate;
                    objectFromDesign.DateRetrofit = galaxy.CurrentStarDate;
                    galaxy.SelectRelativeParkingPoint(out x, out y);
                    objectFromDesign.ParentOffsetX = x;
                    objectFromDesign.ParentOffsetY = y;
                    objectFromDesign.Heading = galaxy.SelectRandomHeading();
                }
            }
            if (num6 > 0)
            {
                Design newest = newEmpire.Designs.FindNewest(BuiltObjectSubRole.MediumSpacePort);
                if (newest != null)
                {
                    ++newest.BuildCount;
                    BuiltObject objectFromDesign = newEmpire.GenerateBuiltObjectFromDesign(newest, newEmpire.Capital.Name + " " + TextResolver.GetText("Space Port"), true, habitat.Xpos, habitat.Ypos);
                    objectFromDesign.ParentHabitat = habitat;
                    objectFromDesign.DateBuilt = galaxy.CurrentStarDate;
                    objectFromDesign.DateRetrofit = galaxy.CurrentStarDate;
                    galaxy.SelectRelativeHabitatSurfacePoint(habitat, out x, out y);
                    objectFromDesign.ParentOffsetX = x;
                    objectFromDesign.ParentOffsetY = y;
                    objectFromDesign.Heading = galaxy.SelectRandomHeading();
                    objectFromDesign.ReDefine();
                    if (!newEmpire.SpacePorts.Contains(objectFromDesign))
                        newEmpire.SpacePorts.Add(objectFromDesign);
                    if (!newEmpire.ConstructionYards.Contains(objectFromDesign))
                        newEmpire.ConstructionYards.Add(objectFromDesign);
                    if (!newEmpire.Manufacturers.Contains(objectFromDesign))
                        newEmpire.Manufacturers.Add(objectFromDesign);
                    if (!newEmpire.RefuellingDepots.Contains(objectFromDesign))
                        newEmpire.RefuellingDepots.Add(objectFromDesign);
                    objectFromDesign.ReDefine();
                }
            }
            newEmpire.EnsureStrategicResourceSupply();
            newEmpire.ResolveSystemVisibility(habitat.Xpos, habitat.Ypos, (BuiltObject)null, (Habitat)null);
            galaxy.Empires.Add(newEmpire);
        }

        public static void RecalculateMaximumPopulation(Habitat planet)
        {
            double num = (double)Math.Max(0.01f, planet.BaseQuality * (1f - planet.Damage));
            double diameter = (double)planet._Diameter;
            planet._MaxPopulation = (long)(diameter * diameter * 250000.0 * (num * num));
            if (planet.Population != null && planet.Population.DominantRace != null && planet.Population.DominantRace.NativeHabitatType == planet.Type)
                planet._MaxPopulation = (long)((double)planet._MaxPopulation * 1.1);
            planet._MaxPopulation = Math.Max(planet._MaxPopulation, 100L);
        }

        public static void TerraformColony(Habitat habitat, double timePassed)
        {
            float num1 = 0.0f;
            float num2 = 0.0f;
            float num3 = 0.0f;
            if (habitat.Facilities != null)
            {
                for (int index = 0; index < habitat.Facilities.Count<PlanetaryFacility>(); ++index)
                {
                    if ((double)habitat.Facilities[index].ConstructionProgress >= 1.0 && habitat.Facilities[index].Type == PlanetaryFacilityType.TerraformingFacility)
                    {
                        num1 = (float)habitat.Facilities[index].Value1 / 1000f;
                        num2 = (float)habitat.Facilities[index].Value2 / 1000f;
                        num3 = (float)habitat.Facilities[index].Value3;
                        break;
                    }
                }
            }
            if ((double)num1 > 0.0)
            {
                float num4 = (float)timePassed / (float)Galaxy.RealSecondsInGalacticYear * num1;
                habitat.Damage -= num4;
                if ((double)habitat.Damage < 0.0)
                    habitat.Damage = 0.0f;
            }
            if ((double)num3 > (double)habitat.BaseQuality * 100.0 && (double)num2 > 0.0)
            {
                if ((double)num3 > 100.0)
                    num3 = 100f;
                float num5 = (float)timePassed / (float)Galaxy.RealSecondsInGalacticYear;
                double num6 = Math.Pow((double)num3 / ((double)num3 + (double)habitat.BaseQuality * 100.0), 4.0);
                habitat.BaseQuality += num5 * (float)num6 * num2;
                if ((double)habitat.BaseQuality > (double)num3)
                    habitat.BaseQuality = num3;
            }
            habitat.RecalculateQuality();
            BaconHabitat.RecalculateMaximumPopulation(habitat);
        }

        public static bool IsAsteroidColony(Habitat habitat)
        {
            bool flag = false;
            if (habitat != null && habitat.Empire != null && habitat.Population != null && habitat.Population.TotalAmount > 0L && habitat.Category == HabitatCategoryType.Asteroid)
                flag = true;
            return flag;
        }

        public static bool IsIndependentColony(Habitat habitat)
        {
            bool flag = false;
            if (BaconBuiltObject.myMain != null && BaconBuiltObject.myMain._Game.Galaxy.IndependentColonies.Contains(habitat))
                flag = true;
            return flag;
        }

        public static void DeployAsteroidColony(BuiltObject ship, Habitat asteroid)
        {
            try
            {
                if (asteroid.Population != null && asteroid.Population.TotalAmount > 0L)
                    return;
                ship.Empire.StateMoney -= (double)BaconHabitat.asteroidColonyCost;
                ship.Empire.TakeOwnershipOfColony(asteroid, ship.Empire);
                asteroid.Population.Add(new Population(ship.NativeRace != null ? ship.NativeRace : ship.ActualEmpire.DominantRace, 30000L));
                if (BaconBuiltObject.myMain != null && ship.Empire != BaconBuiltObject.myMain._Game.PlayerEmpire && BaconBuiltObject.myMain._Game.PlayerEmpire.ResourceMap.CheckResourcesKnown(asteroid))
                    BaconBuiltObject.myMain._Game.PlayerEmpire.ResourceMap.SetResourcesKnown(asteroid, false);
                if (asteroid.Troops == null)
                    asteroid.Troops = new TroopList();
                Troop troop1 = new Troop("asteroid cops", TroopType.Infantry, 100, 1000, 100, 100f, asteroid.Empire, asteroid.Empire.DominantRace);
                Troop troop2 = new Troop("asteroid cops", TroopType.Infantry, 100, 1000, 100, 100f, asteroid.Empire, asteroid.Empire.DominantRace);
                troop1.Colony = asteroid;
                troop2.Colony = asteroid;
                asteroid.Troops.Add(troop1);
                asteroid.Troops.Add(troop2);
                if (asteroid.Cargo == null)
                    asteroid.Cargo = new CargoList();
                if (asteroid.Facilities == null)
                    asteroid.Facilities = new PlanetaryFacilityList();
                asteroid.Cargo.Clear();
                BaconHabitat.GiveAllStrategicResourcesCargoToPlanet(asteroid, 250);
                if ((double)asteroid.BaseQuality >= 1.0)
                    return;
                asteroid.BaseQuality = 1f;
            }
            catch (Exception ex)
            {
            }
        }

        public static void AddTroopsToHabitat(Habitat planet, string troopName, int amount)
        {
            if (BaconBuiltObject.myMain == null || BaconBuiltObject.myMain._Game.SelectedObject == null || !(BaconBuiltObject.myMain._Game.SelectedObject is Habitat))
                return;
            Race race;
            if (planet.Empire.DominantRace != null)
            {
                race = planet.Empire.DominantRace;
            }
            else
            {
                if (planet.Population == null || planet.Population.Count <= 0)
                    return;
                race = planet.Population[0].Race;
            }
            for (int index = 0; index < amount; ++index)
                planet.Troops.Add(new Troop(troopName, TroopType.Infantry, 100, 100, 100, 100f, planet.Empire, race)
                {
                    Colony = planet
                });
        }

        public static void ImproveAsteroidColonyToMinPopulation(Habitat planet, long minPopulation)
        {
            while (planet._MaxPopulation < minPopulation)
            {
                planet.BaseQuality += 0.01f;
                BaconHabitat.RecalculateMaximumPopulation(planet);
            }
        }

        public static void GiveAllStrategicResourcesCargoToPlanet(Habitat planet, int amount)
        {
            foreach (ResourceDefinition resourceDefinition in BaconBuiltObject.myMain._Game.Galaxy.ResourceSystem.StrategicResources.ToList<ResourceDefinition>())
                planet.Cargo.Add(new Cargo(new Resource(resourceDefinition.ResourceID), amount, planet.Empire));
        }

        public static bool ShouldShowAsteroidColonyInInfoPanel(Habitat habitat)
        {
            if (BaconBuiltObject.myMain == null)
                return false;
            Main main = BaconBuiltObject.myMain;
            Empire playerEmpire = main._Game.PlayerEmpire;
            return !BaconHabitat.IsAsteroidColony(habitat) || BaconHabitat.IsAsteroidColony(habitat) && habitat.Empire == main._Game.PlayerEmpire || playerEmpire.ResourceMap.CheckResourcesKnown(habitat) || BaconHabitat.alwaysShowAsteroidColonies;
        }

        public static void CheckIfShouldBuildAsteroidColony(BuiltObjectMission bom)
        {
            if (!BaconHabitat.allowAsteroidColonies)
                return;
            BuiltObject builtObject = bom._BuiltObject;
            Habitat targetHabitat = bom.TargetHabitat;
            if (builtObject == null || targetHabitat == null || targetHabitat.Category != HabitatCategoryType.Asteroid)
                return;
            Empire actualEmpire = builtObject.ActualEmpire;
            if (actualEmpire == null || BaconBuiltObject.myMain == null || actualEmpire == BaconBuiltObject.myMain._Game.Galaxy.IndependentEmpire || actualEmpire.PirateEmpireBaseHabitat != null || actualEmpire == BaconBuiltObject.myMain._Game.PlayerEmpire || !actualEmpire.PreWarpProgressEventOccurredFirstHyperjump)
                return;
            int num1 = actualEmpire.Colonies.Count<Habitat>((Func<Habitat, bool>)(x => x.Category != HabitatCategoryType.Asteroid));
            int num2 = actualEmpire.Colonies.Count<Habitat>((Func<Habitat, bool>)(x => x.Category == HabitatCategoryType.Asteroid));
            if (num1 > 0 && num2 >= num1)
                return;
            float num3 = (float)(1.0 / ((double)(num1 + num2) * (double)BaconHabitat.asteroidColonyPrevalenceDivisor));
            if (new Random().NextDouble() >= (double)num3)
                return;
            BaconHabitat.DeployAsteroidColony(builtObject, targetHabitat);
        }

        public static void GiveCargoToPlanet(
          Habitat planet,
          Empire empire,
          byte startCargo,
          byte lastCargo,
          int amount)
        {
            for (byte resourceId = startCargo; (int)resourceId <= (int)lastCargo; ++resourceId)
                planet.Cargo.Add(new Cargo(new Resource(resourceId), amount, empire));
        }

        public static void DeserializeExtraFields(Habitat planet, SerializationInfo info)
        {
            try
            {
                planet.BaconValues = (Dictionary<string, object>)info.GetValue("baconHabitatSettings", typeof(Dictionary<string, object>));
            }
            catch (Exception ex)
            {
                if (!(ex.GetType() == typeof(SerializationException)))
                    ;
            }
        }

        public static void SerializeExtraFields(Habitat planet, SerializationInfo info)
        {
            try
            {
                info.AddValue("baconHabitatSettings", (object)planet.BaconValues);
            }
            catch (Exception ex)
            {
            }
        }

        public static void HugeProcessingSpanActions(Habitat planet)
        {
            if (BaconBuiltObject.myMain == null)
                return;
            if (planet.Empire != null && BaconBuiltObject.myMain._Game != null && planet.Empire == BaconBuiltObject.myMain._Game.PlayerEmpire)
                BaconHabitat.HandlePlayerPrisoners(planet);
            else if (planet.Empire != null && BaconBuiltObject.myMain._Game != null && BaconBuiltObject.myMain._Game.Galaxy != null && planet.Empire != BaconBuiltObject.myMain._Game.Galaxy.IndependentEmpire)
            {
                BaconHabitat.HandleAIPrisoners(planet);
                BaconHabitat.ChechAIShouldInvestInInfrastructure(planet);
            }
            if (planet.Empire != null)
            {
                if (new Random().NextDouble() < BaconHabitat.marketPriceUpdateChance)
                    BaconHabitat.UpdatePlanetResourcePrices(planet);
                BaconHabitat.UpdatePlanetMarketCash(planet);
            }
            BaconHabitat.DecayInfrastructure(planet);
            BaconHabitat.DecayPirateBase(planet);
        }

        public static void UpdatePlanetMarketCash(Habitat planet)
        {
            if (planet.BaconValues == null)
                planet.BaconValues = new Dictionary<string, object>();
            if (!planet.BaconValues.ContainsKey("marketcash"))
            {
                int num = 10000;
                planet.BaconValues.Add("marketcash", (object)num);
            }
            Main main = BaconBuiltObject.myMain;
            if (main == null)
                return;
            double baconValue = (double)(int)planet.BaconValues["marketcash"];
            Empire empire = planet.Empire;
            if (empire == null)
                return;
            long num1 = planet.Population.Sum<Population>((Func<Population, long>)(x => x.Amount));
            long num2;
            double num3;
            if (empire == main._Game.Galaxy.IndependentEmpire || main._Game.Galaxy.PirateEmpires.Contains(empire))
            {
                num2 = num1;
                num3 = planet.AnnualRevenue;
                if (num3 < 2500.0)
                    num3 = 2500.0;
            }
            else
            {
                if (empire.TotalPopulation == 0L)
                    empire.RecalculateEmpirePopulation();
                num2 = empire.TotalPopulation;
                num3 = empire.PirateEmpireBaseHabitat == null && (empire.GovernmentAttributes == null || empire.GovernmentAttributes.SpecialFunctionCode != 1) ? empire.PrivateMoney : empire.StateMoney;
            }
            double num4 = Math.Max(0.01, (double)num1 / (double)num2) * num3 * 0.25;
            if (num4 < 0.0)
                num4 = 0.0;
            double val2 = baconValue + num4;
            if (val2 > num4 * (double)BaconMain.quartersOfCashAvailable)
                val2 = Convert.ToDouble(val2 * 0.8);
            planet.BaconValues["marketcash"] = (object)Convert.ToInt32(Math.Min((double)int.MaxValue, val2));
        }

        public static void UpdatePlanetResourcePrices(Habitat planet)
        {
            if (planet.BaconValues == null)
                planet.BaconValues = new Dictionary<string, object>();
            if (!planet.BaconValues.ContainsKey("resourcePriceList"))
            {
                Dictionary<string, double> resourcePriceList = BaconHabitat.CreatePlanetResourcePriceList(planet);
                if (resourcePriceList.Count == 0)
                    return;
                planet.BaconValues.Add("resourcePriceList", (object)resourcePriceList);
            }
            Dictionary<string, double> baconValue = (Dictionary<string, double>)planet.BaconValues["resourcePriceList"];
            Dictionary<string, double> dictionary = new Dictionary<string, double>();
            Random random = new Random();
            int resourceID = 0;
            foreach (KeyValuePair<string, double> keyValuePair in baconValue)
            {
                double num1 = (1.0 - baconValue[keyValuePair.Key]) / 2.0;
                double num2 = random.NextDouble() - 0.5 + num1;
                if (num2 < 0.02)
                    num2 = 0.02;
                double environmentalFactors = BaconHabitat.CalculateResourcePriceEnvironmentalFactors(planet, resourceID);
                double num3 = num2 * environmentalFactors;
                dictionary.Add(keyValuePair.Key, baconValue[keyValuePair.Key] + num3 / 2.0);
                ++resourceID;
            }
            planet.BaconValues["resourcePriceList"] = (object)dictionary;
        }

        public static Dictionary<string, double> CreatePlanetResourcePriceList(Habitat planet)
        {
            Dictionary<string, double> resourcePriceList = new Dictionary<string, double>();
            Main main = BaconBuiltObject.myMain;
            if (main == null || main._Game == null || main._Game.Galaxy == null || main._Game.Galaxy.ResourceSystem == null)
                return resourcePriceList;
            ResourceDefinitionList resources = main._Game.Galaxy.ResourceSystem.Resources;
            for (int index = 0; index < resources.Count; ++index)
                resourcePriceList.Add(resources[index].Name, 1.0);
            return resourcePriceList;
        }

        public static double CalculateResourcePriceEnvironmentalFactors(Habitat planet, int resourceID)
        {
            double environmentalFactors = 1.0;
            if (BaconBuiltObject.myMain == null)
                return 1.0;
            ResourceDefinition resource = BaconBuiltObject.myMain._Game.Galaxy.ResourceSystem.Resources[resourceID];
            Empire empire = planet.Empire;
            if (empire != null && empire != BaconBuiltObject.myMain._Game.Galaxy.IndependentEmpire)
            {
                int num1 = empire.CountResourceSupplyLocations(resource.ResourceID, false);
                if (num1 == 0)
                    environmentalFactors += 0.2;
                else if (num1 > 10)
                    environmentalFactors -= 0.1;
                DiplomaticRelationList diplomaticRelations = empire.DiplomaticRelations;
                int num2 = diplomaticRelations.CountRelationsByType(DiplomaticRelationType.War);
                double num3 = 1.0;
                for (int index = 0; index < num2; ++index)
                    num3 *= 1.2;
                double num4 = environmentalFactors * num3;
                int num5 = diplomaticRelations.CountRelationsByType(DiplomaticRelationType.TradeSanctions);
                double num6 = 1.0;
                for (int index = 0; index < num5; ++index)
                    num6 *= 1.1;
                environmentalFactors = num4 * num6;
            }
            return environmentalFactors;
        }

        public static List<Character> GetSpiesInPrison(Habitat planet)
        {
            if (planet.BaconValues == null)
                return (List<Character>)null;
            Dictionary<string, object> baconValues = planet.BaconValues;
            return !baconValues.ContainsKey("capturedSpies") ? (List<Character>)null : (List<Character>)baconValues["capturedSpies"];
        }

        public static void HandlePlayerPrisoners(Habitat planet)
        {
            try
            {
                if (planet.BaconValues == null)
                    return;
                List<Character> spiesInPrison = BaconHabitat.GetSpiesInPrison(planet);
                if (spiesInPrison == null)
                    return;
                foreach (Character character1 in spiesInPrison)
                {
                    if (character1.Empire != BaconBuiltObject.myMain._Game.PlayerEmpire)
                    {
                        if (BaconHabitat.SpyEscaped(planet, character1))
                        {
                            BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, character1.Name + " has escaped from " + planet.Name, Point.Empty, "prisonbreak");
                            CharacterEvent character2 = BaconCharacter.AddEventToCharacter("Escaped", character1.Name + " escaped from " + planet.Name, character1);
                            character1.EventHistory.Add(character2);
                        }
                        else if (BaconHabitat.SpyDefected(planet, character1))
                        {
                            BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, character1.Name + " has agreed to join our empire.", Point.Empty, "defect");
                            CharacterEvent character3 = BaconCharacter.AddEventToCharacter("Defected", character1.Name + " defected to " + planet.Name, character1);
                            character1.EventHistory.Add(character3);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void HandleAIPrisoners(Habitat planet)
        {
            try
            {
                if (planet.Empire == BaconBuiltObject.myMain._Game.PlayerEmpire || planet.BaconValues == null)
                    return;
                List<Character> spiesInPrison = BaconHabitat.GetSpiesInPrison(planet);
                if (spiesInPrison == null || spiesInPrison.Count == 0)
                    return;
                Habitat capital = BaconBuiltObject.myMain._Game.PlayerEmpire.Capital;
                if (capital.BaconValues == null)
                    capital.BaconValues = new Dictionary<string, object>();
                List<Character> characterList1 = new List<Character>();
                if (capital.BaconValues.ContainsKey("capturedSpies"))
                    characterList1 = (List<Character>)capital.BaconValues["capturedSpies"];
                List<Character> characterList2 = new List<Character>();
                foreach (Character character1 in spiesInPrison)
                {
                    if (BaconHabitat.SpyEscaped(planet, character1))
                    {
                        characterList2.Add(character1);
                        BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, character1.Name + " has escaped from " + planet.Name, Point.Empty, "prisonbreak");
                        CharacterEvent character2 = BaconCharacter.AddEventToCharacter("Escaped", character1.Name + " escaped from " + planet.Name, character1);
                        character1.EventHistory.Add(character2);
                    }
                    else if (BaconHabitat.SpyDefected(planet, character1))
                    {
                        characterList2.Add(character1);
                        BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, character1.Name + " has joined " + planet.Empire.Name, Point.Empty, "defect");
                        CharacterEvent character3 = BaconCharacter.AddEventToCharacter("Escaped", character1.Name + " defected to " + planet.Name, character1);
                        character1.EventHistory.Add(character3);
                    }
                    else if (BaconHabitat.ShouldRansomSpy(planet, character1))
                    {
                        characterList1.Add(character1);
                        BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, planet.Empire.Name + " is willing to ransom our agent " + character1.Name);
                        characterList2.Add(character1);
                    }
                }
                foreach (Character character in characterList2)
                    spiesInPrison.Remove(character);
                capital.BaconValues["capturedSpies"] = (object)characterList1;
            }
            catch (Exception ex)
            {
            }
        }

        public static bool ShouldRansomSpy(Habitat planet, Character spy)
        {
            bool flag = false;
            if (planet.Empire.ObtainDiplomaticRelation(BaconBuiltObject.myMain._Game.PlayerEmpire).Type != DiplomaticRelationType.War)
                flag = true;
            return flag;
        }

        public static bool SpyEscaped(Habitat planet, Character spy)
        {
            try
            {
                if (BaconBuiltObject.myMain == null)
                    return false;
                double baseEscapeChance = BaconCharacter.spyBaseEscapeChance;
                if (new Random().NextDouble() > baseEscapeChance)
                    return false;
                if (spy.Empire.Characters == null)
                    spy.Empire.Characters = new CharacterList();
                spy.Empire.Characters.Add(spy);
                if (spy.Empire.PirateEmpireBaseHabitat == null)
                {
                    if (spy.Empire.Capital.Characters == null)
                        spy.Empire.Capital.Characters = new CharacterList();
                    spy.Empire.Capital.Characters.Add(spy);
                }
                else
                {
                    if (spy.Empire.BuiltObjects[0].Characters == null)
                        spy.Empire.BuiltObjects[0].Characters = new CharacterList();
                    spy.Empire.BuiltObjects[0].Characters.Add(spy);
                }
                IntelligenceMission intelligenceMission = new IntelligenceMission(spy.Empire, spy, BaconBuiltObject.myMain._Game.Galaxy.CurrentStarDate)
                {
                    TimeLength = (long)(Galaxy.RealSecondsInGalacticYear * 1000 / 4)
                };
                spy.Mission = intelligenceMission;
                ((List<Character>)planet.Empire.Capital.BaconValues["capturedSpies"]).Remove(spy);
                if (spy.Empire == BaconBuiltObject.myMain._Game.PlayerEmpire || planet.Empire == BaconBuiltObject.myMain._Game.PlayerEmpire)
                {
                    string description = spy.Name + " has escaped from " + planet.Name;
                    BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, description, Point.Empty, "prisonbreak");
                }
            }
            catch (Exception ex)
            {
            }
            return true;
        }

        public static bool SpyDefected(Habitat planet, Character spy)
        {
            try
            {
                if (BaconBuiltObject.myMain == null)
                    return false;
                double baseDefectChance = BaconCharacter.spyBaseDefectChance;
                if (new Random().NextDouble() > baseDefectChance)
                    return false;
                if (planet.Empire.Characters == null)
                    planet.Empire.Characters = new CharacterList();
                planet.Empire.Characters.Add(spy);
                if (planet.Empire.Capital.Characters == null)
                    planet.Empire.Capital.Characters = new CharacterList();
                planet.Empire.Capital.Characters.Add(spy);
                spy.Location = (StellarObject)planet.Empire.Capital;
                spy.Empire = planet.Empire;
                IntelligenceMission intelligenceMission = new IntelligenceMission(spy.Empire, spy, BaconBuiltObject.myMain._Game.Galaxy.CurrentStarDate)
                {
                    TimeLength = (long)(Galaxy.RealSecondsInGalacticYear * 1000 / 4)
                };
                spy.Mission = intelligenceMission;
                ((List<Character>)planet.Empire.Capital.BaconValues["capturedSpies"]).Remove(spy);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void BeginScientificMissionProspectForResources(
          BuiltObject ship,
          Character explorer,
          Habitat planet)
        {
            if (planet.BaconValues == null)
                planet.BaconValues = new Dictionary<string, object>();
            if (ship.Characters.Contains(explorer))
                ship.Characters.Remove(explorer);
            if (ship.ActualEmpire.Characters.Contains(explorer))
                ship.ActualEmpire.Characters.Remove(explorer);
            List<object> objectList = new List<object>();
            objectList.Add((object)ship);
            objectList.Add((object)explorer);
            objectList.Add((object)planet);
            if (!planet.BaconValues.ContainsKey("missionProspectForResources"))
                planet.BaconValues.Add("missionProspectForResources", (object)objectList);
            EventAction action = new EventAction((StellarObject)null, EventActionType.StartPlague);
            action.MessageTitle = "missionProspectForResources";
            action.ExecutionDate = BaconBuiltObject.myMain._Game.Galaxy.CurrentStarDate + (long)(Galaxy.RealSecondsInGalacticYear * 1000 / 360 * Galaxy.Rnd.Next(85, 95));
            action.Target = (StellarObject)planet;
            GameEvent gameEvent = new GameEvent(BaconBuiltObject.myMain._Game.Galaxy, (short)0, (StellarObject)null);
            EventActionExecutionPackage executionPackage = new EventActionExecutionPackage(action, gameEvent, BaconBuiltObject.myMain._Game.PlayerEmpire);
            BaconBuiltObject.myMain._Game.Galaxy.DelayedActions.Add(executionPackage);
        }

        public static void ResolveScientificMissionProspectForResources(Habitat planet)
        {
            if (planet.BaconValues == null || !planet.BaconValues.ContainsKey("missionProspectForResources"))
                return;
            List<object> baconValue = (List<object>)planet.BaconValues["missionProspectForResources"];
            BuiltObject builtObject = (BuiltObject)baconValue[0];
            Character character = (Character)baconValue[1];
            bool flag = false;
            if (new Random().NextDouble() < 0.05 && character.Empire != null && !character.Empire.Name.Contains("Romulan"))
                flag = true;
            string description;
            if (!flag)
            {
                if (Galaxy.CalculateDistanceSquaredStatic(builtObject.Xpos, builtObject.Ypos, planet.Xpos, planet.Ypos) < 100000000.0 || planet.Parent == builtObject.NearestSystemStar)
                {
                    if (builtObject.Characters == null)
                        builtObject.Characters = new CharacterList();
                    builtObject.Characters.Add(character);
                    character.Location = (StellarObject)builtObject;
                }
                else if (character.Empire.Capital != null)
                    character.Location = (StellarObject)character.Empire.Capital;
                else if (character.Empire.BuiltObjects.Any<BuiltObject>())
                    character.Location = (StellarObject)character.Empire.BuiltObjects[0];
                character.Empire.Characters.Add(character);
                int num = planet.Resources != null ? planet.Resources.Count : 0;
                if (BaconBuiltObject.myMain != null)
                {
                    BaconBuiltObject.myMain._Game.Galaxy.SelectResources(planet, planet.Resources.Count + 1);
                    description = planet.Resources.Count <= num ? character.Name + " has finished prospecting on " + planet.Name + " but has discovered nothing new." : character.Name + " has finished prospecting on " + planet.Name + " and haas found new resources.";
                }
                else
                    description = character.Name + " has finished prospecting on " + planet.Name + " but has discovered nothing new.";
            }
            else
                description = character.Name + " was killed while prospecting on " + planet.Name + ".";
            if (description != "")
                BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, description, Point.Empty, "prospectForResources");
            planet.BaconValues.Remove("missionProspectForResources");
            if (planet.BaconValues.Count == 0)
                planet.BaconValues = (Dictionary<string, object>)null;
        }

        public static void BeginScientificMissionExploreRuins(
          BuiltObject ship,
          Character explorer,
          Habitat planet)
        {
            if (planet.BaconValues == null)
                planet.BaconValues = new Dictionary<string, object>();
            if (ship.Characters.Contains(explorer))
                ship.Characters.Remove(explorer);
            if (ship.ActualEmpire.Characters.Contains(explorer))
                ship.ActualEmpire.Characters.Remove(explorer);
            planet.BaconValues.Add("missionExploreRuins", (object)new List<object>()
      {
        (object) ship,
        (object) explorer,
        (object) planet
      });
            EventAction action = new EventAction((StellarObject)null, EventActionType.StartPlague);
            action.MessageTitle = "missionExploreRuins";
            action.ExecutionDate = BaconBuiltObject.myMain._Game.Galaxy.CurrentStarDate + (long)(Galaxy.RealSecondsInGalacticYear * 1000 / 360 * Galaxy.Rnd.Next(85, 95));
            action.Target = (StellarObject)planet;
            GameEvent gameEvent = new GameEvent(BaconBuiltObject.myMain._Game.Galaxy, (short)0, (StellarObject)null);
            EventActionExecutionPackage executionPackage = new EventActionExecutionPackage(action, gameEvent, BaconBuiltObject.myMain._Game.PlayerEmpire);
            BaconBuiltObject.myMain._Game.Galaxy.DelayedActions.Add(executionPackage);
        }

        public static void ResolveScientificMissionExploreRuins(Habitat planet)
        {
            if (planet.BaconValues == null || !planet.BaconValues.ContainsKey("missionExploreRuins"))
                return;
            List<object> baconValue = (List<object>)planet.BaconValues["missionExploreRuins"];
            BuiltObject shipObject = (BuiltObject)baconValue[0];
            Character character = (Character)baconValue[1];
            bool flag = false;
            if (new Random().NextDouble() < 0.05 && character.Empire != null && !character.Empire.Name.Contains("Romulan"))
                flag = true;
            string description;
            if (!flag)
            {
                if (Galaxy.CalculateDistanceSquaredStatic(shipObject.Xpos, shipObject.Ypos, planet.Xpos, planet.Ypos) < 100000000.0 || planet.Parent == shipObject.NearestSystemStar)
                {
                    if (shipObject.Characters == null)
                        shipObject.Characters = new CharacterList();
                    shipObject.Characters.Add(character);
                    character.Location = (StellarObject)shipObject;
                }
                else if (character.Empire.Capital != null)
                    character.Location = (StellarObject)character.Empire.Capital;
                else if (character.Empire.BuiltObjects.Any<BuiltObject>())
                    character.Location = (StellarObject)character.Empire.BuiltObjects[0];
                character.Empire.Characters.Add(character);
                description = character.Name + " has finished exploring " + planet.Name + " and has returned with valuable scient5ific data.";
            }
            else
                description = character.Name + " was killed while exploring " + planet.Name + " but at least we acquired some valuable scient5ific data.";
            int num1 = 0;
            if (BaconMain.IsScienceShip(BaconBuiltObject.myMain, (object)shipObject))
            {
                if (shipObject.BaconValues == null)
                    shipObject.BaconValues = new Dictionary<string, object>();
                if (shipObject.BaconValues.ContainsKey("scientificData"))
                    num1 = (int)shipObject.BaconValues["scientificData"];
                int num2 = Math.Max(50, character.GetSkillLevelTotal());
                int num3 = num1 + num2;
                shipObject.BaconValues["scientificData"] = (object)num3;
            }
            if (description != "")
                BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, description, Point.Empty, "exploreRuins");
            planet.BaconValues.Remove("missionExploreRuins");
            if (planet.BaconValues.Count == 0)
                planet.BaconValues = (Dictionary<string, object>)null;
        }

        public static int GetDevelopmentLevel(Habitat planet)
        {
            int developmentLevel = 0;
            if (planet.BaconValues != null)
                developmentLevel = BaconHabitat.DetermineDevelopmentLevelBonusFromInfrastructureSpending(planet);
            return developmentLevel;
        }

        public static void ChechAIShouldInvestInInfrastructure(Habitat planet)
        {
            if (BaconBuiltObject.myMain == null || !BaconHabitat.allowInfrastructureImprovements || planet.Empire.PrivateMoney < (double)(BaconHabitat.infrastructureSpendingPerDevelopmentLevel * 5) || planet.AnnualTaxRevenue < 30000.0)
                return;
            Random random = new Random();
            double num1 = planet.Empire.PrivateMoney / Convert.ToDouble(BaconHabitat.infrastructureSpendingPerDevelopmentLevel);
            double num2 = random.NextDouble() * 100.0;
            if (num2 >= num1)
                return;
            double num3 = Math.Min(planet.Empire.PrivateMoney * 0.8, Math.Max(planet.Empire.PrivateMoney * 0.1, planet.Empire.PrivateMoney * (num1 - num2)));
            BaconHabitat.InvestInInfastructure(BaconBuiltObject.myMain, "Invest " + Convert.ToInt32(num3).ToString(), false, planet);
            if (BaconMain.isDebugging)
            {
                string description = "*** " + planet.Empire.Name + " invested " + num3.ToString() + " in infrastructure projects on " + planet.Name + ".";
                BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object)null, description, Point.Empty, "Infrastructure");
            }
        }

        public static int DetermineDevelopmentLevelBonusFromInfrastructureSpending(Habitat planet)
        {
            int infrastructureSpending = 0;
            if (planet.BaconValues != null && planet.BaconValues.ContainsKey("infrastructure"))
            {
                int baconValue = (int)planet.BaconValues["infrastructure"];
                long num1 = 1;
                if (planet.Population != null && planet.Population.TotalAmount > 0L)
                    num1 = planet.Population.TotalAmount;
                double num2 = (double)(num1 + BaconHabitat.colonyInfrastructureSpendingPopulationFactor) / ((double)num1 + (double)BaconHabitat.colonyInfrastructureSpendingPopulationFactor / 3.0);
                infrastructureSpending = Convert.ToInt32((double)baconValue * num2 / (double)BaconHabitat.infrastructureSpendingPerDevelopmentLevel);
            }
            return infrastructureSpending;
        }

        public static void InvestInInfastructure(
          Main main,
          string input,
          bool useStateFunds = true,
          Habitat planetParameter = null)
        {
            if (!BaconHabitat.allowInfrastructureImprovements)
                return;
            Habitat habitat = planetParameter != null ? planetParameter : main._Game.SelectedObject as Habitat;
            if (habitat == null)
                return;
            bool flag = false;
            int result = 1000000;
            string[] strArray = input.Split(' ');
            if (strArray.Length > 1)
                flag = int.TryParse(strArray[1], out result);
            if (!flag)
                result = 1000000;
            double num1 = !useStateFunds ? habitat.Empire.PrivateMoney : habitat.Empire.StateMoney;
            if ((double)result > num1)
                result = Convert.ToInt32(num1);
            if (result <= 0)
                return;
            if (habitat.BaconValues == null)
                habitat.BaconValues = new Dictionary<string, object>();
            int num2 = 0;
            if (habitat.BaconValues.ContainsKey("infrastructure"))
                num2 += (int)habitat.BaconValues["infrastructure"];
            if (num2 + result > BaconHabitat.maxInfrastructureInvestmentAllowed)
            {
                if (habitat.Empire != null && habitat.Empire == main._Game.PlayerEmpire && !habitat.Empire.Name.Contains("Romulan"))
                    BaconBuiltObject.ShowMessageBox(main, "Maximum investment is " + (BaconHabitat.maxInfrastructureInvestmentAllowed - num2).ToString(), "Maximum infrastructure investment reached.");
                if (!habitat.Empire.Name.Contains("Romulan"))
                    result = BaconHabitat.maxInfrastructureInvestmentAllowed - num2;
            }
            if (useStateFunds)
                habitat.Empire.StateMoney -= (double)result;
            else
                habitat.Empire.PrivateMoney -= (double)result;
            if (habitat.BaconValues.ContainsKey("infrastructure"))
                result += (int)habitat.BaconValues["infrastructure"];
            habitat.BaconValues["infrastructure"] = (object)result;
        }

        public static void DecayInfrastructure(Habitat planet)
        {
            if (planet.BaconValues == null || !planet.BaconValues.ContainsKey("infrastructure") || !BaconHabitat.allowInfrastructureImprovements)
                return;
            int val1 = (int)planet.BaconValues["infrastructure"];
            long num1 = 1;
            if (planet.Population != null && planet.Population.TotalAmount > 0L)
                num1 = planet.Population.TotalAmount;
            double num2 = (double)(num1 + BaconHabitat.colonyInfrastructureSpendingPopulationFactor) / ((double)num1 + (double)BaconHabitat.colonyInfrastructureSpendingPopulationFactor / 3.0);
            double num3 = (double)BaconHabitat.infrasetuctureDurability + num2 / 90.0;
            try
            {
                val1 = Math.Min(val1, Convert.ToInt32((double)val1 * num3));
            }
            catch (Exception ex)
            {
            }
            planet.BaconValues["infrastructure"] = (object)val1;
        }

        public static void DecayPirateBase(Habitat planet)
        {
            if (planet.BaconValues == null || !planet.BaconValues.ContainsKey("piratebase") || planet.Facilities == null)
                return;
            if (planet.Facilities.FindBestPirateFacility(true) == null)
            {
                planet.BaconValues.Remove("piratebase");
            }
            else
            {
                int baconValue = (int)planet.BaconValues["piratebase"];
                planet.BaconValues["piratebase"] = (object)(int)((double)baconValue * 0.89999997615814209);
            }
        }

        public static void ShowDetailedInformation(Main main, Habitat planet)
        {
            if (planet.BaconValues == null)
                return;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(planet.Name + Environment.NewLine + Environment.NewLine);
            if (planet.BaconValues.ContainsKey("infrastructure"))
            {
                int baconValue = (int)planet.BaconValues["infrastructure"];
                stringBuilder.Append("Infrastructure spending: " + baconValue.ToString() + Environment.NewLine);
                stringBuilder.Append("resulting in a development level bonuse of " + BaconHabitat.DetermineDevelopmentLevelBonusFromInfrastructureSpending(planet).ToString());
            }
            if (planet.BaconValues.ContainsKey("scientificData"))
            {
                int baconValue = (int)planet.BaconValues["scientificData"];
                stringBuilder.Append(Environment.NewLine + "Scientific Data: " + baconValue.ToString() + Environment.NewLine);
            }
            if (planet.BaconValues.ContainsKey("piratebase"))
            {
                int baconValue = (int)planet.BaconValues["piratebase"];
                stringBuilder.Append(Environment.NewLine + "pirate base funding: " + baconValue.ToString() + Environment.NewLine);
            }
            string caption = "Planet Information";
            BaconBuiltObject.ShowMessageBox(main, stringBuilder.ToString(), caption);
        }

        public static HabitatList FilterHabitatListByResource(
          Main main,
          HabitatList habitats,
          int resourceID)
        {
            if (resourceID < 0)
                return habitats;
            HabitatList habitatList = new HabitatList();
            for (int index = 0; index < habitats.Count; ++index)
            {
                if (habitats[index].Resources.ContainsId((byte)resourceID))
                    habitatList.Add(habitats[index]);
            }
            return habitatList;
        }

        public static BuiltObjectList FilterParentHabitatListByResource(
          Main main,
          BuiltObjectList builtObjects,
          int resourceID)
        {
            if (resourceID < 0)
                return builtObjects;
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int index = 0; index < builtObjects.Count; ++index)
            {
                if (builtObjects[index].ParentHabitat != null && builtObjects[index].ParentHabitat.Resources.ContainsId((byte)resourceID))
                    builtObjectList.Add(builtObjects[index]);
            }
            return builtObjectList;
        }

        public static void BuildShipForPirate(
          Habitat planet,
          Empire pirateEmpire,
          BuiltObjectSubRole shipType)
        {
            Design newestCanBuild = pirateEmpire.Designs.FindNewestCanBuild(shipType);
            BaconHabitat.BuildShipForPirate(planet, pirateEmpire, newestCanBuild, shipType);
        }

        public static void BuildShipForPirate(
          Habitat planet,
          Empire pirateEmpire,
          Design design = null,
          BuiltObjectSubRole shipType = BuiltObjectSubRole.ConstructionShip)
        {
            Main main = BaconBuiltObject.myMain;
            if (main == null || planet.Empire == null || planet.Empire != main._Game.Galaxy.IndependentEmpire)
                return;
            PirateColonyControlList pirateControl = planet.GetPirateControl();
            string str = BaconHabitat.pirateControlLevelToBuildShipsAtIndependentPlanets.ToString("P");
            if (pirateControl == null || pirateControl.Count<PirateColonyControl>() < 1 || !pirateControl.CheckFactionHasControl(pirateEmpire))
            {
                BaconBuiltObject.PauseAndShowMessageBox(main, "You must have at least " + str + " control of the planet to construct ships.", "Insufficient control");
            }
            else
            {
                PirateColonyControl pirateColonyControl = pirateControl.FirstOrDefault<PirateColonyControl>((Func<PirateColonyControl, bool>)(x => (int)x.EmpireId == pirateEmpire.EmpireId));
                if (pirateColonyControl == null || (double)pirateColonyControl.ControlLevel < (double)BaconHabitat.pirateControlLevelToBuildShipsAtIndependentPlanets)
                {
                    BaconBuiltObject.PauseAndShowMessageBox(main, "You must have at least " + str + " control of the planet to construct ships.", "Insufficient control");
                }
                else
                {
                    if (design == null)
                        design = pirateEmpire.Designs.FindNewestCanBuild(shipType);
                    if (design == null)
                    {
                        BaconBuiltObject.PauseAndShowMessageBox(main, "No designs found for " + design.SubRole.ToString() + ".", "Insufficient control");
                    }
                    else
                    {
                        double currentPurchasePrice = design.CalculateCurrentPurchasePrice(pirateEmpire._Galaxy);
                        BuiltObject builtObject = new BuiltObject(design, pirateEmpire._Galaxy.GenerateBuiltObjectName(design), pirateEmpire._Galaxy)
                        {
                            PurchasePrice = currentPurchasePrice
                        };
                        builtObject.IsAutoControlled = true;
                        builtObject.ParentHabitat = planet;
                        builtObject.ParentOffsetX = 0.0;
                        builtObject.ParentOffsetY = 0.0;
                        builtObject.Heading = main._Game.Galaxy.SelectRandomHeading();
                        builtObject.TargetHeading = builtObject.Heading;
                        builtObject.NearestSystemStar = Galaxy.DetermineHabitatSystemStar(planet);
                        if (pirateEmpire == main._Game.PlayerEmpire)
                            builtObject.Components.ForEach((Action<BuiltObjectComponent>)(x => x.Status = ComponentStatus.Damaged));
                        else
                            builtObject.Components.ForEach((Action<BuiltObjectComponent>)(x => x.Status = ComponentStatus.Normal));
                        planet.ConstructionQueue.AddBuiltObjectToRepair(builtObject);
                        pirateEmpire.StateMoney -= currentPurchasePrice;
                        pirateEmpire.AddBuiltObjectToGalaxy(builtObject, (object)planet, true, true);
                    }
                }
            }
        }

        public static void ExplorePlanetForMoreResources(Habitat planet)
        {
        }

        public static void LeaveEmpire(Main main, bool grantingIndependance = true)
        {
            if (!(main._Game.SelectedObject is Habitat))
                return;
            Habitat selectedObject = main._Game.SelectedObject as Habitat;
            if (selectedObject.Empire != main._Game.PlayerEmpire)
                return;
            Race dominantRace = selectedObject.Empire.DominantRace;
            List<int> allowableGovernmentTypes = Empire.ResolveDefaultAllowableGovernmentTypes(dominantRace);
            int governmentId = Empire.SelectSuitableGovernment(dominantRace, -1, allowableGovernmentTypes);
            EmpirePolicy policy = main._Game.Galaxy.LoadEmpirePolicy(dominantRace, false);
            Empire empire = new Empire(main._Game.Galaxy, "", selectedObject, dominantRace, governmentId, 1.0, policy);
            if (!grantingIndependance)
            {
                DiplomaticRelation diplomaticRelation = selectedObject.Empire.ObtainDiplomaticRelation(empire);
                diplomaticRelation.Type = DiplomaticRelationType.SubjugatedDominion;
                selectedObject.Empire.ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.SubjugatedDominion);
                empire.DetermineEmpireRelationshipFactors(selectedObject.Empire);
            }
            empire.TakeOwnershipOfColony(selectedObject, empire, false);
            empire.Colonies.Add(selectedObject);
            main._Game.Galaxy.Empires.Add(empire);
            if (grantingIndependance)
            {
                (main._Game.SelectedObject as Habitat).Empire.ObtainEmpireEvaluation(main._Game.PlayerEmpire).FirstContactPenalty = 0.0;
                (main._Game.SelectedObject as Habitat).Empire.ObtainEmpireEvaluation(main._Game.PlayerEmpire).IncidentEvaluation += 100.0;
                (main._Game.SelectedObject as Habitat).Empire.ObtainEmpireEvaluation(main._Game.PlayerEmpire).Bias = 45.0;
                main._Game.PlayerEmpire.CivilityRating += 5.0;
            }
            else
            {
                (main._Game.SelectedObject as Habitat).Empire.ObtainEmpireEvaluation(main._Game.PlayerEmpire).FirstContactPenalty = 0.0;
                (main._Game.SelectedObject as Habitat).Empire.ObtainEmpireEvaluation(main._Game.PlayerEmpire).IncidentEvaluation += 60.0;
            }
        }

        public static void CalculateSpaceControlStrengths(
          Habitat planet,
          Empire defendingEmpire,
          Empire attackingEmpire,
          out int spaceControlStrengthDefenders,
          out int spaceControlStrengthAttackers)
        {
            planet.ResolveInvasionEmpires(out defendingEmpire, out attackingEmpire);
            spaceControlStrengthDefenders = 0;
            spaceControlStrengthAttackers = 0;
            if (BaconBuiltObject.myMain == null)
                return;
            BuiltObjectList nearbyBuiltObjects = BaconBuiltObject.myMain._Game.Galaxy.GetNearbyBuiltObjects(planet.Xpos, planet.Ypos, 2000.0);
            for (int index = 0; index < nearbyBuiltObjects.Count; ++index)
            {
                BuiltObject builtObject = nearbyBuiltObjects[index];
                if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.FirepowerRaw > 0)
                {
                    if (builtObject.Owner == defendingEmpire)
                        spaceControlStrengthDefenders += builtObject.FirepowerRaw;
                    else if (builtObject.Owner == attackingEmpire)
                        spaceControlStrengthAttackers += builtObject.FirepowerRaw;
                }
            }
        }

        public static void CalculateForceStrengths(
          Habitat planet,
          Empire defender,
          Empire attacker,
          TroopList defendingTroops,
          CharacterList defendingCharacters,
          TroopList attackingTroops,
          CharacterList attackingCharacters,
          out int defendingStrength,
          out int attackingStrength,
          out double totalDefendModifier,
          out double totalAttackModifier,
          out List<double> modifierAmountsDefense,
          out List<string> modifierReasonsDefense,
          out List<double> modifierAmountsAttack,
          out List<string> modifierReasonsAttack)
        {
            defendingStrength = 0;
            attackingStrength = 0;
            try
            {
                planet.DetermineTroopModifiers(defender, attacker, defendingTroops, attackingTroops, defendingCharacters, attackingCharacters, out modifierAmountsDefense, out modifierReasonsDefense, out modifierAmountsAttack, out modifierReasonsAttack);
                if (attacker != null && defender != null && BaconMain.useInvasionModifierReputation && attackingTroops != null && attackingTroops.Count > 0 && attackingTroops[0].Type != TroopType.PirateRaider && defendingTroops != null && defendingTroops.Count > 0 && defendingTroops[0].Empire.PirateEmpireBaseHabitat == null)
                {
                    double num = defender.CivilityRating - attacker.CivilityRating;
                    if (num < -5.0)
                    {
                        modifierAmountsDefense.Add(Math.Round(num / 200.0, 2));
                        modifierReasonsDefense.Add("Reputation");
                    }
                    else if (num > 5.0)
                    {
                        modifierAmountsDefense.Add(Math.Round(num / 200.0, 2));
                        modifierReasonsDefense.Add("Reputation");
                    }
                }
                totalDefendModifier = 0.0;
                for (int index = 0; index < modifierAmountsDefense.Count; ++index)
                    totalDefendModifier += modifierAmountsDefense[index];
                totalAttackModifier = 0.0;
                for (int index = 0; index < modifierAmountsAttack.Count; ++index)
                    totalAttackModifier += modifierAmountsAttack[index];
                defendingStrength = (int)((1.0 + totalDefendModifier) * (double)defendingTroops.TotalDefendStrength);
                attackingStrength = (int)((1.0 + totalAttackModifier) * (double)attackingTroops.TotalAttackStrength);
            }
            catch (Exception ex)
            {
                modifierAmountsDefense = new List<double>();
                modifierReasonsDefense = new List<string>();
                modifierAmountsAttack = new List<double>();
                modifierReasonsAttack = new List<string>();
                totalDefendModifier = 0.0;
                totalAttackModifier = 0.0;
            }
        }

        public static bool MakeAllPopulationOfAnEmpireToThisRace(Empire empire, Race race)
        {
            bool thisRace = false;
            foreach (Habitat habitat in empire != BaconBuiltObject.myMain._Game.Galaxy.IndependentEmpire ? (SyncList<Habitat>)empire.Colonies : (SyncList<Habitat>)BaconBuiltObject.myMain._Game.Galaxy.IndependentColonies)
            {
                long amount = 0;
                foreach (Population population in (SyncList<Population>)habitat.Population)
                    amount += population.Amount;
                Population population1 = new Population(race, amount);
                habitat.Population.Clear();
                habitat.Population.Add(population1);
            }
            return thisRace;
        }

        public static bool CheckAndCreateBaconValuesKey(Habitat planet, string keyToFind)
        {
            if (planet.BaconValues == null)
                planet.BaconValues = new Dictionary<string, object>();
            if (planet.BaconValues.ContainsKey(keyToFind))
                return true;
            planet.BaconValues["scientificData"] = (object)0;
            return false;
        }

        public static void GenerateDefensivePirateRaiders(
          Habitat planet,
          Empire defendingPirateFaction,
          bool currentDefendingTroopsInvade)
        {
            float num1 = 50f;
            int d = 0;
            if (defendingPirateFaction == null || defendingPirateFaction.DominantRace == null)
                return;
            if (currentDefendingTroopsInvade)
            {
                TroopList items1 = new TroopList();
                CharacterList items2 = new CharacterList();
                items1.AddRange((IEnumerable<Troop>)planet.Troops);
                items2.AddRange((IEnumerable<Character>)planet.Characters);
                planet.Troops.Clear();
                planet.Characters.Clear();
                planet.InvadingTroops.AddRange((IEnumerable<Troop>)items1);
                planet.InvadingCharacters.AddRange((IEnumerable<Character>)items2);
            }
            int num2 = 1;
            PlanetaryFacility planetaryFacility = (PlanetaryFacility)null;
            if (planet.Facilities != null)
                planetaryFacility = planet.Facilities.FindBestCompletedPirateFacility(true);
            if (planetaryFacility != null)
            {
                switch (planetaryFacility.Type)
                {
                    case PlanetaryFacilityType.PirateBase:
                        num2 = BaconHabitat.pirateBaseTroops;
                        break;
                    case PlanetaryFacilityType.PirateFortress:
                        num2 = BaconHabitat.pirateFortressTroops;
                        break;
                    case PlanetaryFacilityType.PirateCriminalNetwork:
                        num2 = BaconHabitat.pirateCriminalNetworkTroops;
                        break;
                }
            }
            if (planet.BaconValues != null && planet.BaconValues.ContainsKey("piratebase"))
            {
                d = (int)planet.BaconValues["piratebase"] / 1000;
                int num3 = d <= 100 ? (int)Math.Sqrt((double)d) : 10 + d / 20;
                num2 += num3;
                num1 = (float)Math.Max(100.0, 60.0 + (double)num3);
            }
            int num4 = num2 + Galaxy.Rnd.Next(0, 3);
            double num5 = (double)defendingPirateFaction.DominantRace.TroopStrength / 100.0;
            double raidStrengthFactor = defendingPirateFaction.RaidStrengthFactor;
            int num6 = (int)((double)num1 * num5 * raidStrengthFactor);
            for (int index = 0; index < num4; ++index)
            {
                Troop troop = new Troop(defendingPirateFaction.GenerateTroopDescription(string.Format(TextResolver.GetText("RACE Pirate Raider"), (object)defendingPirateFaction.DominantRace.Name)), TroopType.PirateRaider, num6, num6, 100, 100f, defendingPirateFaction, defendingPirateFaction.DominantRace);
                if (defendingPirateFaction.DominantRace != null)
                    troop.PictureRef = defendingPirateFaction.DominantRace.PictureRef;
                if (troop != null)
                {
                    planet.Troops.Add(troop);
                    defendingPirateFaction.Troops.Add(troop);
                }
            }
            if (d > 0 && BaconBuiltObject.myMain != null && defendingPirateFaction == BaconBuiltObject.myMain._Game.PlayerEmpire && planet.Empire != null && planet.Empire != BaconBuiltObject.myMain._Game.Galaxy.IndependentEmpire)
            {
                string message = "Your pirate base at " + planet.Name + " is under attack" + Environment.NewLine + "Do you wish to view it?";
                if (BaconBuiltObject.PauseAndShowYesNoMessageBox(BaconBuiltObject.myMain, message, "Pirate base under attack") == "Yes")
                {
                    BaconBuiltObject.myMain._Game.SelectedObject = (object)planet;
                    if (BaconBuiltObject.myMain._Game.SelectedObject != null)
                        BaconBuiltObject.myMain.method_208(BaconBuiltObject.myMain._Game.SelectedObject);
                }
            }
        }

        public static void AddFundsToPirateBase(Main main, string input)
        {
            if (main._Game.SelectedObject == null || !(main._Game.SelectedObject is Habitat))
                return;
            Habitat selectedObject = main._Game.SelectedObject as Habitat;
            if (selectedObject.Facilities == null)
                return;
            PlanetaryFacility bestPirateFacility = selectedObject.Facilities.FindBestPirateFacility(true);
            if (bestPirateFacility == null)
                return;
            Empire empire = selectedObject.CheckFacilityOwner(bestPirateFacility);
            if (empire == null || empire != main._Game.PlayerEmpire)
                return;
            int result = 0;
            int num = 0;
            if (main._Game.SelectedObject == null)
                return;
            string[] strArray = input.Split(' ');
            if (strArray.Length > 1)
            {
                if (!int.TryParse(strArray[1], out result))
                {
                    BaconBuiltObject.ShowMessageBox(main, "\"piratebase x\", where x is an integer. Leave blank to check on the funds at this pirate base.", "Format");
                }
                else
                {
                    if (selectedObject.BaconValues == null)
                        selectedObject.BaconValues = new Dictionary<string, object>();
                    if (selectedObject.BaconValues.ContainsKey("piratebase"))
                        num = (int)selectedObject.BaconValues["piratebase"];
                    else
                        selectedObject.BaconValues.Add("piratebase", (object)0);
                    if (result <= 0)
                        return;
                    selectedObject.BaconValues["piratebase"] = (object)(num + result);
                    main._Game.PlayerEmpire.StateMoney -= (double)result;
                }
            }
            else
            {
                if (selectedObject.BaconValues != null && selectedObject.BaconValues.ContainsKey("piratebase"))
                    num = (int)selectedObject.BaconValues["piratebase"];
                BaconBuiltObject.ShowMessageBox(main, "Current funds in pirate base: " + num.ToString(), "Funding level");
            }
        }

        public static void ReviewPirateControl(Habitat planet, double timePassed)
        {
            if (BaconBuiltObject.myMain?._Game == null || planet.Population == null || planet.Population.TotalAmount <= 0L)
                return;
            float val1_1 = 1f;
            if (planet.Population.TotalAmount > BaconHabitat.pirateMaxPopulationInfluence)
                val1_1 = Math.Min(1f, Math.Max(0.0f, 1f - (float)(planet.Population.TotalAmount - BaconHabitat.pirateMaxPopulationInfluence) / 2E+09f));
            PirateColonyControlList colonyControlList1 = new PirateColonyControlList();
            BuiltObjectList nearbyBuiltObjects = BaconBuiltObject.myMain._Game.Galaxy.GetNearbyBuiltObjects(planet.Xpos, planet.Ypos, 1500.0);
            for (int index1 = 0; index1 < nearbyBuiltObjects.Count; ++index1)
            {
                BuiltObject builtObject = nearbyBuiltObjects[index1];
                if (builtObject != null && builtObject.Role != BuiltObjectRole.Base && builtObject.Empire != null && builtObject.Empire.PirateEmpireBaseHabitat != null)
                {
                    int index2 = colonyControlList1.IndexOf(builtObject.Empire.EmpireId);
                    if (index2 >= 0)
                    {
                        float num = Math.Max(1f, (float)builtObject.FirepowerRaw);
                        colonyControlList1[index2].ControlLevel += num;
                    }
                    else
                        colonyControlList1.Add(new PirateColonyControl(builtObject.Empire.EmpireId, (float)builtObject.FirepowerRaw));
                }
            }
            PirateColonyControl byFacilityControl = planet.GetPirateControl().GetByFacilityControl();
            if (byFacilityControl != null)
            {
                PlanetaryFacility completedPirateFacility = planet.Facilities.FindBestCompletedPirateFacility(true);
                if (completedPirateFacility != null)
                {
                    float num = 0.5f;
                    float controlLevel = 0.0f;
                    switch (completedPirateFacility.Type)
                    {
                        case PlanetaryFacilityType.PirateBase:
                            num = 0.5f;
                            controlLevel = 35f;
                            break;
                        case PlanetaryFacilityType.PirateFortress:
                            num = 1f;
                            controlLevel = 70f;
                            break;
                        case PlanetaryFacilityType.PirateCriminalNetwork:
                            num = 1f;
                            controlLevel = 100f;
                            break;
                    }
                    if ((double)byFacilityControl.ControlLevel < (double)num)
                    {
                        int index = colonyControlList1.IndexOf((int)byFacilityControl.EmpireId);
                        if (index >= 0)
                            colonyControlList1[index].ControlLevel += controlLevel;
                        else
                            colonyControlList1.Add(new PirateColonyControl((int)byFacilityControl.EmpireId, controlLevel));
                    }
                }
            }
            colonyControlList1.Sort();
            colonyControlList1.Reverse();
            PirateColonyControlList colonyControlList2 = new PirateColonyControlList();
            for (int index = 0; index < planet.GetPirateControl().Count; ++index)
            {
                PirateColonyControl pirateColonyControl = planet.GetPirateControl()[index];
                if (pirateColonyControl != null)
                {
                    int num1 = colonyControlList1.IndexOf((int)pirateColonyControl.EmpireId);
                    if (num1 < 0 || num1 >= 3)
                    {
                        float val1_2 = 0.0f;
                        if (pirateColonyControl.HasFacilityControl && planet.Facilities != null)
                        {
                            int num2 = planet.Facilities.CountByType(PlanetaryFacilityType.PirateBase);
                            int num3 = planet.Facilities.CountByType(PlanetaryFacilityType.PirateFortress);
                            int num4 = planet.Facilities.CountByType(PlanetaryFacilityType.PirateCriminalNetwork);
                            if (num4 > 0)
                                val1_2 = 1f;
                            else if (num3 > 0)
                                val1_2 = 1f;
                            else if (num2 > 0)
                                val1_2 = 0.5f;
                            if (num2 <= 0 && num3 <= 0 && num4 <= 0)
                                pirateColonyControl.HasFacilityControl = false;
                        }
                        float num5 = (float)(-0.5 * (timePassed / (double)Galaxy.RealSecondsInGalacticYear));
                        float num6 = Math.Max(val1_2, pirateColonyControl.ControlLevel + num5);
                        pirateColonyControl.ControlLevel = num6;
                        if ((double)num6 <= 0.0)
                            colonyControlList2.Add(pirateColonyControl);
                    }
                }
            }
            for (int index = 0; index < colonyControlList2.Count; ++index)
            {
                Empire byEmpireId = BaconBuiltObject.myMain._Game.Galaxy.PirateEmpires.GetByEmpireId((int)colonyControlList2[index].EmpireId);
                if (byEmpireId != null)
                {
                    Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(planet);
                    string description = string.Format(TextResolver.GetText("We have lost control of the colony X"), (object)Galaxy.ResolveDescription(planet.Type).ToLower(CultureInfo.InvariantCulture), (object)planet.Name, (object)habitatSystemStar.Name);
                    byEmpireId.SendMessageToEmpire(byEmpireId, EmpireMessageType.ColonyLost, (object)planet, description);
                }
                planet.GetPirateControl().Remove(colonyControlList2[index]);
                byEmpireId?.ResolveSystemVisibility(planet.Xpos, planet.Ypos, (BuiltObject)null, (Habitat)null);
            }
            int num7 = Math.Min(3, colonyControlList1.Count);
            for (int index3 = 0; index3 < num7; ++index3)
            {
                PirateColonyControl pirateColonyControl1 = colonyControlList1[index3];
                if (pirateColonyControl1 != null)
                {
                    int index4 = planet.GetPirateControl().IndexOf((int)pirateColonyControl1.EmpireId);
                    if (index4 >= 0)
                    {
                        PirateColonyControl pirateColonyControl2 = planet.GetPirateControl()[index4];
                        if (pirateColonyControl2 != null && (double)pirateColonyControl2.ControlLevel < (double)val1_1)
                        {
                            float val1_3 = 0.0f;
                            if (pirateColonyControl2.HasFacilityControl && planet.Facilities != null)
                            {
                                int num8 = planet.Facilities.CountByType(PlanetaryFacilityType.PirateBase);
                                int num9 = planet.Facilities.CountByType(PlanetaryFacilityType.PirateFortress);
                                int num10 = planet.Facilities.CountByType(PlanetaryFacilityType.PirateCriminalNetwork);
                                if (num10 > 0)
                                    val1_3 = 1f;
                                else if (num9 > 0)
                                    val1_3 = 1f;
                                else if (num8 > 0)
                                    val1_3 = 0.5f;
                                if (num8 <= 0 && num9 <= 0 && num10 <= 0)
                                    pirateColonyControl2.HasFacilityControl = false;
                            }
                            float num11 = Math.Max(0.5f, Math.Min(3f, pirateColonyControl1.ControlLevel / 20f)) * (float)(0.5 * (timePassed / (double)Galaxy.RealSecondsInGalacticYear));
                            float num12 = Math.Max(val1_3, Math.Min(val1_1, planet.GetPirateControl()[index4].ControlLevel + num11));
                            planet.GetPirateControl()[index4].ControlLevel = num12;
                        }
                    }
                    else if (planet.GetPirateControl().Count < 3 && (double)val1_1 >= 0.0099999997764825821)
                    {
                        planet.GetPirateControl().Add(new PirateColonyControl((int)pirateColonyControl1.EmpireId, 0.01f));
                        Empire byEmpireId = BaconBuiltObject.myMain._Game.Galaxy.PirateEmpires.GetByEmpireId((int)pirateColonyControl1.EmpireId);
                        if (byEmpireId != null)
                        {
                            Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(planet);
                            string description = string.Format(TextResolver.GetText("We have gained control of the colony X"), (object)Galaxy.ResolveDescription(planet.Type).ToLower(CultureInfo.InvariantCulture), (object)planet.Name, (object)habitatSystemStar.Name);
                            byEmpireId.SendMessageToEmpire(byEmpireId, EmpireMessageType.ColonyGained, (object)planet, description);
                        }
                    }
                }
            }
            planet.GetPirateControl().Sort();
            planet.GetPirateControl().Reverse();
        }
    }
}
