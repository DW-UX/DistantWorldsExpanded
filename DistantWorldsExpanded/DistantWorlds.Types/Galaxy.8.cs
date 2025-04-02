using BaconDistantWorlds;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DistantWorlds.Types
{
    public partial class Galaxy
    {
        public static Troop GenerateNewTroop(string name, TroopType troopType, int naturalStrength, Empire empire, Race race, bool applyBonusFactors)
        {
            int attackStrength = 100;
            int defendStrength = 100;
            int size = 100;
            float num = 1f;
            switch (troopType)
            {
                case TroopType.Infantry:
                    if (applyBonusFactors && empire != null)
                    {
                        attackStrength = (int)((float)naturalStrength * empire.TroopAttackStrengthBonusFactorInfantry);
                        defendStrength = (int)((float)naturalStrength * empire.TroopDefendStrengthBonusFactorInfantry);
                    }
                    else
                    {
                        attackStrength = naturalStrength;
                        defendStrength = naturalStrength;
                    }
                    size = 100;
                    num = 1f;
                    break;
                case TroopType.Armored:
                    if (applyBonusFactors && empire != null)
                    {
                        attackStrength = (int)((float)naturalStrength * empire.TroopAttackStrengthBonusFactorArmored * 3f);
                        defendStrength = (int)((float)naturalStrength * empire.TroopDefendStrengthBonusFactorArmored * 1.5f);
                    }
                    else
                    {
                        attackStrength = (int)((float)naturalStrength * 3f);
                        defendStrength = (int)((float)naturalStrength * 1.5f);
                    }
                    size = 200;
                    num = 2f;
                    break;
                case TroopType.Artillery:
                    if (applyBonusFactors && empire != null)
                    {
                        attackStrength = (int)((float)naturalStrength * empire.TroopAttackStrengthBonusFactorArtillery * 0.5f);
                        defendStrength = (int)((double)(float)naturalStrength * 1.0 * 0.75);
                    }
                    else
                    {
                        attackStrength = (int)((float)naturalStrength * 0.5f);
                        defendStrength = (int)((float)naturalStrength * 0.75f);
                    }
                    size = 400;
                    num = 4f;
                    break;
                case TroopType.SpecialForces:
                    if (applyBonusFactors && empire != null)
                    {
                        attackStrength = (int)((float)naturalStrength * empire.TroopAttackStrengthBonusFactorSpecialForces * 2f);
                        defendStrength = (int)((float)naturalStrength * empire.TroopDefendStrengthBonusFactorSpecialForces);
                    }
                    else
                    {
                        attackStrength = (int)((float)naturalStrength * 2f);
                        defendStrength = naturalStrength;
                    }
                    size = 100;
                    num = 2f;
                    break;
            }
            Troop troop = new Troop(name, troopType, attackStrength, defendStrength, size, 100f, empire, race);
            float num2 = CalculateTroopMaintenanceMultiplier(race);
            troop.MaintenanceMultiplier = num2 * num;
            if (race != null)
            {
                troop.PictureRef = race.PictureRef;
            }
            return troop;
        }

        public void SetColonizableHabitatsInSystem(Galaxy galaxy, Habitat systemStar, Race race, int colonyCount)
        {
            HabitatList habitats = galaxy.Systems[systemStar].Habitats;
            HabitatList habitatList = new HabitatList();
            HabitatList habitatList2 = new HabitatList();
            foreach (Habitat item in habitats)
            {
                if ((item.Owner == null || item.Owner == galaxy.IndependentEmpire) && (item.Population.TotalAmount > 0 || item.Type == race.NativeHabitatType) && item.Category != HabitatCategoryType.Asteroid)
                {
                    habitatList.Add(item);
                }
                else if (item.Owner == null || item.Owner == galaxy.IndependentEmpire)
                {
                    if ((item.Category == HabitatCategoryType.Moon || item.Category == HabitatCategoryType.Planet) && (item.Type == HabitatType.MarshySwamp || item.Type == HabitatType.Ocean || item.Type == HabitatType.Desert))
                    {
                        habitatList2.Add(item);
                    }
                    if (item.Category == HabitatCategoryType.Planet && item.Type == HabitatType.BarrenRock)
                    {
                        habitatList2.Add(item);
                    }
                }
            }
            if (habitatList.Count > colonyCount)
            {
                int num = habitatList.Count - colonyCount;
                for (int i = 0; i < num; i++)
                {
                    if (habitatList[i].Population.TotalAmount <= 0)
                    {
                        HabitatType type = HabitatType.Undefined;
                        int pictureRef = 0;
                        int landscapePictureRef = 0;
                        int diameter = 0;
                        int minOrbitDistance = 0;
                        int maxOrbitDistance = 0;
                        galaxy.SelectBarrenRockPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                        habitatList[i].Type = HabitatType.BarrenRock;
                        habitatList[i].Diameter = (short)diameter;
                        habitatList[i].PictureRef = (short)pictureRef;
                        habitatList[i].LandscapePictureRef = (short)landscapePictureRef;
                        habitatList[i].BaseQuality = galaxy.SelectHabitatQuality(habitatList[i], (float)galaxy.ColonyPrevalence);
                        habitatList[i].Resources.Clear();
                        galaxy.SelectResources(habitatList[i]);
                    }
                }
            }
            else
            {
                if (habitatList.Count >= colonyCount)
                {
                    return;
                }
                int num2 = colonyCount - habitatList.Count;
                for (int j = 0; j < num2; j++)
                {
                    if (habitatList2.Count > j)
                    {
                        HabitatType type2 = HabitatType.Undefined;
                        int pictureRef2 = 0;
                        int landscapePictureRef2 = 0;
                        int diameter2 = 0;
                        int minOrbitDistance2 = 0;
                        int maxOrbitDistance2 = 0;
                        switch (race.NativeHabitatType)
                        {
                            case HabitatType.Continental:
                                galaxy.SelectContinentalPlanet(out type2, out pictureRef2, out diameter2, out minOrbitDistance2, out maxOrbitDistance2, out landscapePictureRef2);
                                break;
                            case HabitatType.MarshySwamp:
                                galaxy.SelectMarshySwampPlanet(out type2, out pictureRef2, out diameter2, out minOrbitDistance2, out maxOrbitDistance2, out landscapePictureRef2);
                                break;
                            case HabitatType.Ocean:
                                galaxy.SelectOceanPlanet(out type2, out pictureRef2, out diameter2, out minOrbitDistance2, out maxOrbitDistance2, out landscapePictureRef2);
                                break;
                            case HabitatType.Desert:
                                galaxy.SelectDesertPlanet(out type2, out pictureRef2, out diameter2, out minOrbitDistance2, out maxOrbitDistance2, out landscapePictureRef2);
                                break;
                            case HabitatType.Ice:
                                galaxy.SelectIcePlanet(out type2, out pictureRef2, out diameter2, out minOrbitDistance2, out maxOrbitDistance2, out landscapePictureRef2);
                                break;
                            case HabitatType.Volcanic:
                                galaxy.SelectVolcanicPlanet(out type2, out pictureRef2, out diameter2, out minOrbitDistance2, out maxOrbitDistance2, out landscapePictureRef2);
                                break;
                        }
                        habitatList2[j].Type = race.NativeHabitatType;
                        habitatList2[j].Diameter = (short)diameter2;
                        habitatList2[j].PictureRef = (short)pictureRef2;
                        habitatList2[j].LandscapePictureRef = (short)landscapePictureRef2;
                        habitatList2[j].BaseQuality = (float)(0.7 + Rnd.NextDouble() * 0.25);
                        habitatList2[j].Resources.Clear();
                        galaxy.SelectResources(habitatList2[j]);
                    }
                    else
                    {
                        Habitat habitat = GenerateContinentalPlanet(galaxy, systemStar);
                        lock (_LockObject)
                        {
                            galaxy.AddHabitat(habitat, systemStar);
                        }
                    }
                }
            }
        }

        public Habitat GenerateGasGiantPlanet(Galaxy galaxy, Habitat sun)
        {
            HabitatType type = HabitatType.Undefined;
            int pictureRef = 0;
            int landscapePictureRef = 0;
            int diameter = 0;
            int minOrbitDistance = 0;
            int maxOrbitDistance = 0;
            galaxy.SelectGasGiantPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
            int orbitdistance = GeneratePlanetaryOrbitDistance(sun, minOrbitDistance, maxOrbitDistance);
            Habitat habitat = new Habitat(galaxy, HabitatCategoryType.Planet, type, galaxy.GenerateRandomName(), sun, Rnd.NextDouble() * Math.PI * 2.0, orbitdirection: true, orbitdistance, Rnd.Next(2, 5));
            habitat.Diameter = (short)diameter;
            habitat.PictureRef = (short)pictureRef;
            habitat.LandscapePictureRef = (short)landscapePictureRef;
            habitat.BaseQuality = galaxy.SelectHabitatQuality(habitat, (float)galaxy.ColonyPrevalence);
            habitat.DoTasks(galaxy.CurrentDateTime);
            habitat = galaxy.SelectResources(habitat);
            if (Rnd.Next(0, 5) == 2)
            {
                habitat.OrbitDirection = false;
            }
            habitat.Cargo = new CargoList();
            return habitat;
        }

        public Habitat GenerateFrozenGasGiantPlanet(Galaxy galaxy, Habitat sun)
        {
            HabitatType type = HabitatType.Undefined;
            int pictureRef = 0;
            int landscapePictureRef = 0;
            int diameter = 0;
            int minOrbitDistance = 0;
            int maxOrbitDistance = 0;
            galaxy.SelectFrozenGasGiantPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
            int orbitdistance = GeneratePlanetaryOrbitDistance(sun, minOrbitDistance, maxOrbitDistance);
            Habitat habitat = new Habitat(galaxy, HabitatCategoryType.Planet, type, galaxy.GenerateRandomName(), sun, Rnd.NextDouble() * Math.PI * 2.0, orbitdirection: true, orbitdistance, Rnd.Next(2, 5));
            habitat.Diameter = (short)diameter;
            habitat.PictureRef = (short)pictureRef;
            habitat.LandscapePictureRef = (short)landscapePictureRef;
            habitat.BaseQuality = galaxy.SelectHabitatQuality(habitat, (float)galaxy.ColonyPrevalence);
            habitat.DoTasks(galaxy.CurrentDateTime);
            habitat = galaxy.SelectResources(habitat);
            if (Rnd.Next(0, 5) == 2)
            {
                habitat.OrbitDirection = false;
            }
            habitat.Cargo = new CargoList();
            return habitat;
        }

        public Habitat GenerateDesertPlanet(Galaxy galaxy, Habitat sun)
        {
            HabitatType type = HabitatType.Undefined;
            int pictureRef = 0;
            int landscapePictureRef = 0;
            int diameter = 0;
            int minOrbitDistance = 0;
            int maxOrbitDistance = 0;
            galaxy.SelectDesertPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
            int orbitdistance = GeneratePlanetaryOrbitDistance(sun, minOrbitDistance, maxOrbitDistance);
            Habitat habitat = new Habitat(galaxy, HabitatCategoryType.Planet, type, galaxy.GenerateRandomName(), sun, Rnd.NextDouble() * Math.PI * 2.0, orbitdirection: true, orbitdistance, Rnd.Next(2, 5));
            habitat.Diameter = (short)diameter;
            habitat.PictureRef = (short)pictureRef;
            habitat.LandscapePictureRef = (short)landscapePictureRef;
            habitat.BaseQuality = galaxy.SelectHabitatQuality(habitat, (float)galaxy.ColonyPrevalence);
            habitat.DoTasks(galaxy.CurrentDateTime);
            habitat = galaxy.SelectResources(habitat);
            if (Rnd.Next(0, 5) == 2)
            {
                habitat.OrbitDirection = false;
            }
            habitat.Cargo = new CargoList();
            habitat.Troops = new TroopList();
            habitat.TroopsToRecruit = new TroopList();
            habitat.InvadingTroops = new TroopList();
            habitat.ConstructionQueue = new ConstructionQueue(habitat, galaxy);
            habitat.ManufacturingQueue = new ManufacturingQueue(habitat, galaxy);
            habitat.DockingBays = new DockingBayList();
            int num = 20;
            for (int i = 0; i < num; i++)
            {
                BuiltObjectComponent builtObjectComponent = new BuiltObjectComponent(74, ComponentStatus.Normal);
                DockingBay item = new DockingBay(builtObjectComponent.ComponentID, builtObjectComponent.BuiltObjectComponentId, 100);
                habitat.DockingBays.Add(item);
            }
            habitat.DockingBayWaitQueue = new BuiltObjectList();
            return habitat;
        }

        public Habitat GenerateOceanPlanet(Galaxy galaxy, Habitat sun)
        {
            HabitatType type = HabitatType.Undefined;
            int pictureRef = 0;
            int landscapePictureRef = 0;
            int diameter = 0;
            int minOrbitDistance = 0;
            int maxOrbitDistance = 0;
            galaxy.SelectOceanPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
            int orbitdistance = GeneratePlanetaryOrbitDistance(sun, minOrbitDistance, maxOrbitDistance);
            Habitat habitat = new Habitat(galaxy, HabitatCategoryType.Planet, type, galaxy.GenerateRandomName(), sun, Rnd.NextDouble() * Math.PI * 2.0, orbitdirection: true, orbitdistance, Rnd.Next(2, 5));
            habitat.Diameter = (short)diameter;
            habitat.PictureRef = (short)pictureRef;
            habitat.LandscapePictureRef = (short)landscapePictureRef;
            habitat.BaseQuality = galaxy.SelectHabitatQuality(habitat, (float)galaxy.ColonyPrevalence);
            habitat.DoTasks(galaxy.CurrentDateTime);
            habitat = galaxy.SelectResources(habitat);
            if (Rnd.Next(0, 5) == 2)
            {
                habitat.OrbitDirection = false;
            }
            habitat.Cargo = new CargoList();
            habitat.Troops = new TroopList();
            habitat.TroopsToRecruit = new TroopList();
            habitat.InvadingTroops = new TroopList();
            habitat.ConstructionQueue = new ConstructionQueue(habitat, galaxy);
            habitat.ManufacturingQueue = new ManufacturingQueue(habitat, galaxy);
            habitat.DockingBays = new DockingBayList();
            int num = 20;
            for (int i = 0; i < num; i++)
            {
                BuiltObjectComponent builtObjectComponent = new BuiltObjectComponent(74, ComponentStatus.Normal);
                DockingBay item = new DockingBay(builtObjectComponent.ComponentID, builtObjectComponent.BuiltObjectComponentId, 100);
                habitat.DockingBays.Add(item);
            }
            habitat.DockingBayWaitQueue = new BuiltObjectList();
            return habitat;
        }

        public Habitat GenerateSwampPlanet(Galaxy galaxy, Habitat sun)
        {
            HabitatType type = HabitatType.Undefined;
            int pictureRef = 0;
            int landscapePictureRef = 0;
            int diameter = 0;
            int minOrbitDistance = 0;
            int maxOrbitDistance = 0;
            galaxy.SelectMarshySwampPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
            int orbitdistance = GeneratePlanetaryOrbitDistance(sun, minOrbitDistance, maxOrbitDistance);
            Habitat habitat = new Habitat(galaxy, HabitatCategoryType.Planet, type, galaxy.GenerateRandomName(), sun, Rnd.NextDouble() * Math.PI * 2.0, orbitdirection: true, orbitdistance, Rnd.Next(2, 5));
            habitat.Diameter = (short)diameter;
            habitat.PictureRef = (short)pictureRef;
            habitat.LandscapePictureRef = (short)landscapePictureRef;
            habitat.BaseQuality = galaxy.SelectHabitatQuality(habitat, (float)galaxy.ColonyPrevalence);
            habitat.DoTasks(galaxy.CurrentDateTime);
            habitat = galaxy.SelectResources(habitat);
            if (Rnd.Next(0, 5) == 2)
            {
                habitat.OrbitDirection = false;
            }
            habitat.Cargo = new CargoList();
            habitat.Troops = new TroopList();
            habitat.TroopsToRecruit = new TroopList();
            habitat.InvadingTroops = new TroopList();
            habitat.ConstructionQueue = new ConstructionQueue(habitat, galaxy);
            habitat.ManufacturingQueue = new ManufacturingQueue(habitat, galaxy);
            habitat.DockingBays = new DockingBayList();
            int num = 20;
            for (int i = 0; i < num; i++)
            {
                BuiltObjectComponent builtObjectComponent = new BuiltObjectComponent(74, ComponentStatus.Normal);
                DockingBay item = new DockingBay(builtObjectComponent.ComponentID, builtObjectComponent.BuiltObjectComponentId, 100);
                habitat.DockingBays.Add(item);
            }
            habitat.DockingBayWaitQueue = new BuiltObjectList();
            return habitat;
        }

        private bool CheckPlanetaryOrbitalOverlap(Habitat systemStar, int orbitDistance)
        {
            int num = 150;
            SystemInfo systemInfo = Systems[systemStar];
            if (systemInfo != null && systemInfo.Habitats != null)
            {
                for (int i = 0; i < systemInfo.Habitats.Count; i++)
                {
                    Habitat habitat = systemInfo.Habitats[i];
                    if (habitat != null)
                    {
                        int num2 = habitat.OrbitDistance - num;
                        int num3 = habitat.OrbitDistance + num;
                        if (orbitDistance >= num2 && orbitDistance <= num3)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private int GeneratePlanetaryOrbitDistance(Habitat systemStar, int minOrbitDistance, int maxOrbitDistance)
        {
            int num = Rnd.Next(minOrbitDistance, maxOrbitDistance);
            int num2 = 0;
            while (CheckPlanetaryOrbitalOverlap(systemStar, num) && num2 < 20)
            {
                num = Rnd.Next(minOrbitDistance, maxOrbitDistance);
                num2++;
            }
            return num;
        }

        public Habitat GenerateBarrenRockPlanet(Galaxy galaxy, Habitat sun)
        {
            HabitatType type = HabitatType.Undefined;
            int pictureRef = 0;
            int landscapePictureRef = 0;
            int diameter = 0;
            int minOrbitDistance = 0;
            int maxOrbitDistance = 0;
            galaxy.SelectBarrenRockPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
            int orbitdistance = GeneratePlanetaryOrbitDistance(sun, minOrbitDistance, maxOrbitDistance);
            Habitat habitat = new Habitat(galaxy, HabitatCategoryType.Planet, type, galaxy.GenerateRandomName(), sun, Rnd.NextDouble() * Math.PI * 2.0, orbitdirection: true, orbitdistance, Rnd.Next(2, 5));
            habitat.Diameter = (short)diameter;
            habitat.PictureRef = (short)pictureRef;
            habitat.LandscapePictureRef = (short)landscapePictureRef;
            habitat.BaseQuality = galaxy.SelectHabitatQuality(habitat, (float)galaxy.ColonyPrevalence);
            habitat.DoTasks(galaxy.CurrentDateTime);
            habitat = galaxy.SelectResources(habitat);
            if (Rnd.Next(0, 5) == 2)
            {
                habitat.OrbitDirection = false;
            }
            habitat.Cargo = new CargoList();
            return habitat;
        }

        public Habitat GenerateAsteroid(Galaxy galaxy, Habitat sun, HabitatType type)
        {
            int num = Rnd.Next(20, 35);
            int num2 = GalaxyImages.HabitatImageOffsetAsteroidsNormal + Rnd.Next(0, GalaxyImages.HabitatImageCountAsteroidsNormal);
            Rnd.Next(9500, 11500);
            switch (type)
            {
                case HabitatType.Ice:
                    Rnd.Next(17200, 22200);
                    break;
                case HabitatType.Metal:
                    Rnd.Next(10500, 11500);
                    break;
            }
            double orbitangle = Rnd.NextDouble() * Math.PI * 2.0;
            string name = GenerateCodeName();
            Habitat habitat = new Habitat(this, HabitatCategoryType.Asteroid, type, name, sun, orbitangle, orbitdirection: true, Rnd.Next(10500, 11500), Rnd.Next(2, 8));
            habitat.Diameter = (short)num;
            habitat.PictureRef = (short)num2;
            habitat.LandscapePictureRef = -1;
            int minimumResourceCount = 0;
            if (type == HabitatType.Metal && Rnd.Next(0, 3) > 0)
            {
                minimumResourceCount = 1;
            }
            habitat = SelectResources(habitat, minimumResourceCount);
            habitat.Type = type;
            SelectHabitatPictures(habitat);
            return habitat;
        }

        public Habitat GenerateContinentalPlanet(Galaxy galaxy, Habitat sun)
        {
            HabitatType type = HabitatType.Undefined;
            int pictureRef = 0;
            int landscapePictureRef = 0;
            int diameter = 0;
            int minOrbitDistance = 0;
            int maxOrbitDistance = 0;
            galaxy.SelectContinentalPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
            int orbitdistance = GeneratePlanetaryOrbitDistance(sun, minOrbitDistance, maxOrbitDistance);
            Habitat habitat = new Habitat(galaxy, HabitatCategoryType.Planet, type, galaxy.GenerateRandomName(), sun, Rnd.NextDouble() * Math.PI * 2.0, orbitdirection: true, orbitdistance, Rnd.Next(2, 5));
            habitat.Diameter = (short)diameter;
            habitat.PictureRef = (short)pictureRef;
            habitat.LandscapePictureRef = (short)landscapePictureRef;
            habitat.BaseQuality = galaxy.SelectHabitatQuality(habitat, (float)galaxy.ColonyPrevalence);
            habitat.DoTasks(galaxy.CurrentDateTime);
            habitat = galaxy.SelectResources(habitat);
            if (Rnd.Next(0, 5) == 2)
            {
                habitat.OrbitDirection = false;
            }
            habitat.Cargo = new CargoList();
            habitat.Troops = new TroopList();
            habitat.TroopsToRecruit = new TroopList();
            habitat.InvadingTroops = new TroopList();
            habitat.ConstructionQueue = new ConstructionQueue(habitat, galaxy);
            habitat.ManufacturingQueue = new ManufacturingQueue(habitat, galaxy);
            habitat.DockingBays = new DockingBayList();
            int num = 20;
            for (int i = 0; i < num; i++)
            {
                BuiltObjectComponent builtObjectComponent = new BuiltObjectComponent(74, ComponentStatus.Normal);
                DockingBay item = new DockingBay(builtObjectComponent.ComponentID, builtObjectComponent.BuiltObjectComponentId, 100);
                habitat.DockingBays.Add(item);
            }
            habitat.DockingBayWaitQueue = new BuiltObjectList();
            return habitat;
        }

        public Habitat GenerateVolcanicPlanet(Galaxy galaxy, Habitat sun)
        {
            HabitatType type = HabitatType.Undefined;
            int pictureRef = 0;
            int landscapePictureRef = 0;
            int diameter = 0;
            int minOrbitDistance = 0;
            int maxOrbitDistance = 0;
            galaxy.SelectVolcanicPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
            int orbitdistance = GeneratePlanetaryOrbitDistance(sun, minOrbitDistance, maxOrbitDistance);
            Habitat habitat = new Habitat(galaxy, HabitatCategoryType.Planet, type, galaxy.GenerateRandomName(), sun, Rnd.NextDouble() * Math.PI * 2.0, orbitdirection: true, orbitdistance, Rnd.Next(2, 5));
            habitat.Diameter = (short)diameter;
            habitat.PictureRef = (short)pictureRef;
            habitat.LandscapePictureRef = (short)landscapePictureRef;
            habitat.BaseQuality = galaxy.SelectHabitatQuality(habitat, (float)galaxy.ColonyPrevalence);
            habitat.DoTasks(galaxy.CurrentDateTime);
            habitat = galaxy.SelectResources(habitat);
            if (Rnd.Next(0, 5) == 2)
            {
                habitat.OrbitDirection = false;
            }
            habitat.Cargo = new CargoList();
            habitat.Troops = new TroopList();
            habitat.TroopsToRecruit = new TroopList();
            habitat.InvadingTroops = new TroopList();
            habitat.ConstructionQueue = new ConstructionQueue(habitat, galaxy);
            habitat.ManufacturingQueue = new ManufacturingQueue(habitat, galaxy);
            habitat.DockingBays = new DockingBayList();
            int num = 20;
            for (int i = 0; i < num; i++)
            {
                BuiltObjectComponent builtObjectComponent = new BuiltObjectComponent(74, ComponentStatus.Normal);
                DockingBay item = new DockingBay(builtObjectComponent.ComponentID, builtObjectComponent.BuiltObjectComponentId, 100);
                habitat.DockingBays.Add(item);
            }
            habitat.DockingBayWaitQueue = new BuiltObjectList();
            return habitat;
        }

        public Habitat GenerateIcePlanet(Galaxy galaxy, Habitat sun)
        {
            HabitatType type = HabitatType.Undefined;
            int pictureRef = 0;
            int landscapePictureRef = 0;
            int diameter = 0;
            int minOrbitDistance = 0;
            int maxOrbitDistance = 0;
            galaxy.SelectIcePlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
            int orbitdistance = GeneratePlanetaryOrbitDistance(sun, minOrbitDistance, maxOrbitDistance);
            Habitat habitat = new Habitat(galaxy, HabitatCategoryType.Planet, type, galaxy.GenerateRandomName(), sun, Rnd.NextDouble() * Math.PI * 2.0, orbitdirection: true, orbitdistance, Rnd.Next(2, 5));
            habitat.Diameter = (short)diameter;
            habitat.PictureRef = (short)pictureRef;
            habitat.LandscapePictureRef = (short)landscapePictureRef;
            habitat.BaseQuality = galaxy.SelectHabitatQuality(habitat, (float)galaxy.ColonyPrevalence);
            habitat.DoTasks(galaxy.CurrentDateTime);
            habitat = galaxy.SelectResources(habitat);
            if (Rnd.Next(0, 5) == 2)
            {
                habitat.OrbitDirection = false;
            }
            habitat.Cargo = new CargoList();
            habitat.Troops = new TroopList();
            habitat.TroopsToRecruit = new TroopList();
            habitat.InvadingTroops = new TroopList();
            habitat.ConstructionQueue = new ConstructionQueue(habitat, galaxy);
            habitat.ManufacturingQueue = new ManufacturingQueue(habitat, galaxy);
            habitat.DockingBays = new DockingBayList();
            int num = 20;
            for (int i = 0; i < num; i++)
            {
                BuiltObjectComponent builtObjectComponent = new BuiltObjectComponent(74, ComponentStatus.Normal);
                DockingBay item = new DockingBay(builtObjectComponent.ComponentID, builtObjectComponent.BuiltObjectComponentId, 100);
                habitat.DockingBays.Add(item);
            }
            habitat.DockingBayWaitQueue = new BuiltObjectList();
            return habitat;
        }

        private void SetResourceLevelsInSystem(Galaxy galaxy, Habitat systemStar, int resourceLevelMinimum, int resourceLevelMaximum)
        {
            HabitatList habitats = galaxy.Systems[systemStar].Habitats;
            foreach (Habitat item in habitats)
            {
                if (item.Category == HabitatCategoryType.Planet || item.Category == HabitatCategoryType.Moon)
                {
                    if (item.Resources.Count > resourceLevelMaximum)
                    {
                        item.Resources.Clear();
                    }
                    else if (item.Resources.Count < resourceLevelMinimum)
                    {
                        item.Resources.Clear();
                        galaxy.SelectResources(item, resourceLevelMinimum);
                    }
                }
            }
        }

        public static void ResolveHomeSystem(string homeSystemDescription, out HabitatType capitalHabitatType, out double homeSystemFactor)
        {
            capitalHabitatType = HabitatType.Undefined;
            homeSystemFactor = 0.0;
            if (homeSystemDescription == TextResolver.GetText("Harsh"))
            {
                capitalHabitatType = HabitatType.Desert;
                homeSystemFactor = 0.4;
            }
            else if (homeSystemDescription == TextResolver.GetText("Trying"))
            {
                capitalHabitatType = HabitatType.MarshySwamp;
                homeSystemFactor = 0.7;
            }
            else if (homeSystemDescription == TextResolver.GetText("Normal"))
            {
                capitalHabitatType = HabitatType.MarshySwamp;
                homeSystemFactor = 1.0;
            }
            else if (homeSystemDescription == TextResolver.GetText("Agreeable"))
            {
                capitalHabitatType = HabitatType.Continental;
                homeSystemFactor = 1.4;
            }
            else if (homeSystemDescription == TextResolver.GetText("Excellent"))
            {
                capitalHabitatType = HabitatType.Continental;
                homeSystemFactor = 2.0;
            }
        }

        public static double DetermineEmpireExpansion(Random rnd, int age)
        {
            double num = 1.0;
            double num2 = EmpireAgeExpansionRateMaximum - EmpireAgeExpansionRateMinimum;
            age--;
            for (int i = 0; i < age; i++)
            {
                double num3 = EmpireAgeExpansionRateMinimum + rnd.NextDouble() * num2;
                num *= num3;
            }
            return num;
        }

        public void MakeHabitatIntoColony(Galaxy galaxy, Habitat habitat, Empire empire, int age, Race race, double homeSystemFactor, bool hasSpacePort)
        {
            if (habitat.Ruin != null && (habitat.Ruin.Type == RuinType.Standard || habitat.Ruin.Type == RuinType.CreatureSwarm || habitat.Ruin.Type == RuinType.PirateAmbush))
            {
                habitat.Ruin = null;
                if (galaxy.RuinsHabitats.Contains(habitat))
                {
                    galaxy.RuinsHabitats.Remove(habitat);
                }
            }
            empire.TakeOwnershipOfColony(habitat, empire);
            if (habitat.Quality < 0.5f)
            {
                habitat.BaseQuality = 0.5f + (float)(Rnd.NextDouble() * 0.1);
            }
            habitat.IsRefuellingDepot = true;
            if (galaxy != null && galaxy.ColonyNames != null && empire != galaxy.IndependentEmpire && empire == galaxy.PlayerEmpire && galaxy.ColonyNames.Count > galaxy.ColonyNameIndex)
            {
                string name = galaxy.ColonyNames[galaxy.ColonyNameIndex];
                galaxy.ColonyNameIndex++;
                habitat.Name = name;
            }
            double num = habitat.Quality;
            if (habitat.Type == race.NativeHabitatType)
            {
                num *= 2.0;
            }
            long num2 = (long)(homeSystemFactor * num * 300000000.0 + homeSystemFactor * num * Rnd.NextDouble() * 400000000.0);
            if (age > 0)
            {
                num2 = (long)((double)num2 * Math.Pow(1.7, age));
            }
            Population population = new Population(race, num2);
            habitat.Population.Add(population);
            habitat.Population.TotalAmount += num2;
            habitat.GrowPopulation(new TimeSpan(0L));
            int num3 = SetColonyResources(galaxy, habitat, empire, hasSpacePort);
            habitat.SetDevelopmentLevel(Math.Min(50, Math.Max(0, num3 * 5 + Rnd.Next(0, 5))));
            habitat.RecalculateCriticalResourceSupplyBonuses();
            habitat.RecalculateDevelopmentLevelBaseline();
            habitat.RecalculateAnnualTaxRevenue();
            habitat.DoTasks(galaxy.CurrentDateTime);
            if (habitat.ConstructionQueue != null)
            {
                habitat.ConstructionQueue.ReviewConstructionSpeed();
            }
            int troopLevelRequired = habitat.TroopLevelRequired;
            int num4 = (int)((double)troopLevelRequired * (0.5 + Rnd.NextDouble()));
            int num5 = num4 / 100;
            int troopStrength = race.TroopStrength;
            for (int i = 0; i < num5; i++)
            {
                Troop troop = GenerateNewTroop(empire.GenerateTroopDescription(), TroopType.Infantry, troopStrength, empire, race);
                troop.Colony = habitat;
                habitat.Troops.Add(troop);
                empire.Troops.Add(troop);
            }
            empire.ResolveSystemVisibility(habitat.Xpos, habitat.Ypos, null, null);
        }

        public int SetColonyResources(Galaxy galaxy, Habitat habitat, Empire empire, bool hasSpacePort)
        {
            int val = 1 + (int)(habitat.Population.TotalAmount / 250000000);
            val = Math.Min(10, val);
            double num = 500.0;
            double num2 = ColonyAnnualResourceConsumptionRate * ((double)habitat.Population.TotalAmount / 20.0) * (double)(habitat.Population.DominantRace.CautionLevel / 100);
            if (num2 < 1.0)
            {
                num2 = 1.0;
            }
            else if (num2 > 4.0)
            {
                num2 = 4.0;
            }
            if (habitat.Cargo != null)
            {
                habitat.Cargo.Clear();
                for (int i = 0; i < ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; i++)
                {
                    ResourceDefinition resourceDefinition = ResourceSystem.StrategicResourcesOrderedByRelativeImportance[i];
                    if (resourceDefinition != null && resourceDefinition.ColonyGrowthResourceLevel > 0f)
                    {
                        Cargo cargo = new Cargo(new Resource(resourceDefinition.ResourceID), (int)((double)(resourceDefinition.RelativeImportance * 6000f) * num2), empire);
                        habitat.Cargo.Add(cargo);
                    }
                }
                if (hasSpacePort)
                {
                    for (int j = 0; j < ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; j++)
                    {
                        ResourceDefinition resourceDefinition2 = ResourceSystem.StrategicResourcesOrderedByRelativeImportance[j];
                        if (resourceDefinition2 != null && resourceDefinition2.ColonyGrowthResourceLevel <= 0f)
                        {
                            Cargo cargo2 = new Cargo(new Resource(resourceDefinition2.ResourceID), (int)((double)(resourceDefinition2.RelativeImportance * 1500f) * num2), empire);
                            habitat.Cargo.Add(cargo2);
                        }
                    }
                }
                long num3 = Math.Max(500000000L, habitat.Population.TotalAmount);
                int num4 = (int)(ColonyAnnualLuxuryResourceConsumptionRate * (double)num3 * ((double)habitat.Population.DominantRace.CautionLevel / 100.0) * 5.0);
                num4 = Math.Max(num4 * 3, MinimumLuxuryResourceReorderAmount);
                num4 = Math.Max(400, num4);
                num4 = (int)((double)num4 * 1.5);
                for (int k = 0; k < 4; k++)
                {
                    int index = Rnd.Next(0, ResourceSystem.StrategicResources.Count);
                    byte resourceID = ResourceSystem.StrategicResources[index].ResourceID;
                    Cargo cargo3 = new Cargo(new Resource(resourceID), 400, empire);
                    habitat.Cargo.Add(cargo3);
                }
                if (empire != null && empire.DominantRace != null && empire.DominantRace.CriticalResources != null)
                {
                    ResourceList resourceList = empire.DominantRace.CriticalResources.ResolveResources();
                    for (int l = 0; l < resourceList.Count; l++)
                    {
                        Resource resource = resourceList[l];
                        if (resource != null)
                        {
                            int amount = Rnd.Next(300, 500);
                            Cargo cargo4 = new Cargo(resource, amount, empire);
                            habitat.Cargo.Add(cargo4);
                        }
                    }
                }
                for (int m = 0; m < habitat.Resources.Count; m++)
                {
                    HabitatResource habitatResource = habitat.Resources[m];
                    if (habitatResource != null)
                    {
                        Cargo cargo5 = new Cargo(new Resource(habitatResource.ResourceID), (int)(num * num2), empire);
                        habitat.Cargo.Add(cargo5);
                    }
                }
            }
            return val;
        }

        public void CreatePirateMiningStations(Galaxy galaxy, Empire empire, int count, bool allowEmpiresToStartInSameSystem)
        {
            int num = 0;
            empire.ResourceTargets = empire.IdentifyResourceCentres(galaxy);
            int num2 = 0;
            for (int i = 0; i < ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; i++)
            {
                ResourceDefinition resourceDefinition = ResourceSystem.StrategicResourcesOrderedByRelativeImportance[i];
                if (resourceDefinition != null)
                {
                    num2 = 0;
                    while (num < count && CreateMiningStation(galaxy, empire.CheckResourceSupplyMeetsExpected(new Resource(resourceDefinition.ResourceID)), empire, allowEmpiresToStartInSameSystem) && num2 < 50)
                    {
                        num++;
                        num2++;
                    }
                }
            }
            num2 = 0;
            while (ConditionCheckLimit(empire.ResourceTargets.Count > 0 && num < count, 50, ref num2))
            {
                if (CreateMiningStation(galaxy, empire.ResourceTargets[0].Habitat, empire, allowEmpiresToStartInSameSystem))
                {
                    num++;
                }
            }
        }

        public void CreateMiningStations(Galaxy galaxy, Empire empire, bool allowEmpiresToStartInSameSystem)
        {
            int val = (int)((double)empire.Colonies.Count * 1.5);
            int num = 0;
            if (galaxy.StartingAge == 0 && empire.Colonies.Count == 1)
            {
                val = 6;
            }
            val = Math.Max(6, val);
            empire.ResourceTargets = empire.IdentifyResourceCentres(galaxy);
            int num2 = 0;
            for (int i = 0; i < galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; i++)
            {
                num2 = 0;
                Resource resource = new Resource(galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance[i].ResourceID);
                while (num < val && CreateMiningStation(galaxy, empire.CheckResourceSupplyMeetsExpected(resource), empire, allowEmpiresToStartInSameSystem) && num2 < 50)
                {
                    num++;
                    num2++;
                }
            }
            num2 = 0;
            while (ConditionCheckLimit(empire.ResourceTargets.Count > 0 && num < val, 50, ref num2))
            {
                if (CreateMiningStation(galaxy, empire.ResourceTargets[0].Habitat, empire, allowEmpiresToStartInSameSystem))
                {
                    num++;
                }
            }
        }

        private bool CreateMiningStation(Galaxy galaxy, Habitat habitat, Empire empire, bool allowEmpiresToStartInSameSystem)
        {
            if (habitat != null)
            {
                Habitat systemStar = DetermineHabitatSystemStar(habitat);
                bool disputed = false;
                Empire empire2 = galaxy.CheckSystemOwnership(systemStar, out disputed);
                if ((allowEmpiresToStartInSameSystem || empire2 == null || empire2 == empire || disputed) && galaxy.CheckEmpireTerritoryCanBuildAtHabitat(empire, habitat) && galaxy.DetermineMiningStationAtHabitatForEmpire(habitat, empire) == null && habitat.Empire == null)
                {
                    Design design = null;
                    if (habitat.Resources.ContainsGroup(ResourceGroup.Gas))
                    {
                        design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.GasMiningStation);
                    }
                    if (habitat.Resources.ContainsGroup(ResourceGroup.Mineral))
                    {
                        design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.MiningStation);
                    }
                    if (design != null)
                    {
                        galaxy.SelectRelativeHabitatSurfacePoint(habitat, out var x, out var y);
                        bool flag = false;
                        if (habitat.BasesAtHabitat != null && habitat.BasesAtHabitat.Count > 0)
                        {
                            flag = true;
                        }
                        if (!flag)
                        {
                            design.BuildCount++;
                            double purchasePrice = design.CalculateCurrentPurchasePrice(galaxy);
                            string name = galaxy.GenerateBuiltObjectName(design, habitat);
                            BuiltObject builtObject = new BuiltObject(design, name, galaxy, fullyBuilt: true);
                            builtObject.PurchasePrice = purchasePrice;
                            builtObject.ParentHabitat = habitat;
                            builtObject.ParentOffsetX = x;
                            builtObject.ParentOffsetY = y;
                            builtObject.Heading = galaxy.SelectRandomHeading();
                            builtObject.TargetHeading = builtObject.Heading;
                            builtObject.ReDefine();
                            builtObject.CurrentFuel = builtObject.FuelCapacity;
                            builtObject.CurrentShields = builtObject.ShieldsCapacity;
                            builtObject.NearestSystemStar = DetermineHabitatSystemStar(habitat);
                            empire.AddBuiltObjectToGalaxy(builtObject, habitat, offsetLocationFromParent: false, isStateOwned: false, (int)builtObject.ParentOffsetX, (int)builtObject.ParentOffsetY);
                            if ((habitat == null || habitat.Empire != empire) && builtObject.Cargo != null)
                            {
                                CargoList cargoList = ResolveRetrofitResourcesForBase(empire);
                                for (int i = 0; i < cargoList.Count; i++)
                                {
                                    builtObject.Cargo.Add(cargoList[i]);
                                }
                            }
                            return true;
                        }
                    }
                }
                int num = empire.ResourceTargets.IndexOf(habitat);
                if (num >= 0)
                {
                    empire.ResourceTargets.RemoveAt(num);
                }
            }
            return false;
        }

        public void SetLuxuryResourcesAtColonies(Galaxy galaxy, Empire empire)
        {
            empire.IdentifyUnavailableLuxuryResources();
            ResourceList selfSuppliedLuxuryResources = empire.SelfSuppliedLuxuryResources;
            for (int i = 0; i < empire.Colonies.Count; i++)
            {
                Habitat habitat = empire.Colonies[i];
                long num = Math.Max(500000000L, habitat.Population.TotalAmount);
                int num2 = (int)(ColonyAnnualLuxuryResourceConsumptionRate * (double)num * ((double)habitat.Population.DominantRace.CautionLevel / 100.0) * 5.0);
                num2 = Math.Max(num2 * 3, MinimumLuxuryResourceReorderAmount);
                num2 = Math.Max(400, num2);
                int val = 3 + (int)Math.Sqrt(habitat.Population.TotalAmount / 100000000);
                val = Math.Min(val, Math.Min(10, selfSuppliedLuxuryResources.Count));
                for (int j = 0; j < val; j++)
                {
                    Resource resource = selfSuppliedLuxuryResources[j];
                    Cargo cargo = new Cargo(new Resource(resource.ResourceID), num2, empire);
                    habitat.Cargo.Add(cargo);
                }
                habitat.RecalculateDevelopmentLevelBaseline();
                habitat.SetDevelopmentLevel(val * 5);
            }
        }

        public void CreateStateShips(Galaxy galaxy, Empire empire)
        {
            foreach (ForceStructureProjection stateForceStructureProjection in empire.StateForceStructureProjections)
            {
                Design design = empire.Designs.FindNewestCanBuildFullEvaluate(stateForceStructureProjection.SubRole);
                if (design != null && stateForceStructureProjection.Amount > 0)
                {
                    int num = stateForceStructureProjection.Amount;
                    if (galaxy.StartingAge == 0 && empire.Colonies.Count == 1 && stateForceStructureProjection.SubRole != BuiltObjectSubRole.ConstructionShip)
                    {
                        num = Math.Max(1, num / 3);
                        empire.StateMoney += 1000.0;
                    }
                    for (int i = 0; i < num; i++)
                    {
                        double purchasePrice = design.CalculateCurrentPurchasePrice(galaxy);
                        design.BuildCount++;
                        BuiltObject builtObject = new BuiltObject(design, galaxy.GenerateBuiltObjectName(design), galaxy, fullyBuilt: true);
                        builtObject.PurchasePrice = purchasePrice;
                        builtObject.ReDefine();
                        builtObject.CurrentFuel = builtObject.FuelCapacity;
                        builtObject.CurrentShields = builtObject.ShieldsCapacity;
                        builtObject.Heading = galaxy.SelectRandomHeading();
                        builtObject.TargetHeading = builtObject.Heading;
                        galaxy.SelectRelativeParkingPoint(out var x, out var y);
                        Habitat habitat = empire.SelectRandomColony();
                        builtObject.Name = galaxy.GenerateBuiltObjectName(design, habitat);
                        builtObject.NearestSystemStar = DetermineHabitatSystemStar(habitat);
                        empire.AddBuiltObjectToGalaxy(builtObject, habitat, offsetLocationFromParent: false, isStateOwned: true, (int)x, (int)y);
                    }
                }
            }
            empire.StateForceStructureProjections.Clear();
        }

        public void CreatePrivateShips(Galaxy galaxy, Empire empire)
        {
            foreach (ForceStructureProjection privateForceStructureProjection in empire.PrivateForceStructureProjections)
            {
                Design design = empire.Designs.FindNewestCanBuildFullEvaluate(privateForceStructureProjection.SubRole);
                if (design == null)
                {
                    continue;
                }
                switch (privateForceStructureProjection.SubRole)
                {
                    case BuiltObjectSubRole.SmallFreighter:
                    case BuiltObjectSubRole.MediumFreighter:
                    case BuiltObjectSubRole.LargeFreighter:
                        privateForceStructureProjection.Amount = (int)((double)privateForceStructureProjection.Amount * 0.6);
                        break;
                }
                if (privateForceStructureProjection.Amount > 0)
                {
                    for (int i = 0; i < privateForceStructureProjection.Amount; i++)
                    {
                        double purchasePrice = design.CalculateCurrentPurchasePrice(galaxy);
                        design.BuildCount++;
                        BuiltObject builtObject = new BuiltObject(design, galaxy.GenerateBuiltObjectName(design), galaxy, fullyBuilt: true);
                        builtObject.PurchasePrice = purchasePrice;
                        builtObject.ReDefine();
                        builtObject.CurrentFuel = builtObject.FuelCapacity;
                        builtObject.CurrentShields = builtObject.ShieldsCapacity;
                        builtObject.Heading = galaxy.SelectRandomHeading();
                        builtObject.TargetHeading = builtObject.Heading;
                        galaxy.SelectRelativeParkingPoint(out var x, out var y);
                        Habitat habitat = empire.SelectRandomColony();
                        builtObject.Name = galaxy.GenerateBuiltObjectName(design, habitat);
                        builtObject.NearestSystemStar = DetermineHabitatSystemStar(habitat);
                        empire.AddBuiltObjectToGalaxy(builtObject, habitat, offsetLocationFromParent: false, isStateOwned: false, (int)x, (int)y);
                    }
                }
            }
            empire.PrivateForceStructureProjections.Clear();
        }

        public void FillShipsWithTroops(Galaxy galaxy, Empire empire)
        {
            int troopStrength = empire.DominantRace.TroopStrength;
            foreach (BuiltObject builtObject in empire.BuiltObjects)
            {
                if (builtObject.SubRole == BuiltObjectSubRole.TroopTransport)
                {
                    int num = Rnd.Next(1, 9) * 100;
                    int iterationCount = 0;
                    while (ConditionCheckLimit(builtObject.Troops != null && builtObject.TroopCapacityRemaining >= num, 50, ref iterationCount))
                    {
                        Troop troop = GenerateNewTroop(empire.GenerateTroopDescription(), TroopType.Infantry, troopStrength, empire, empire.DominantRace);
                        troop.MaintenanceMultiplier = CalculateTroopMaintenanceMultiplier(empire.DominantRace);
                        troop.PictureRef = empire.DominantRace.PictureRef;
                        troop.BuiltObject = builtObject;
                        builtObject.Troops.Add(troop);
                        empire.Troops.Add(troop);
                    }
                }
                else if (Rnd.Next(0, 2) == 1)
                {
                    int num2 = Rnd.Next(1, 4) * 100;
                    int iterationCount2 = 0;
                    while (ConditionCheckLimit(builtObject.Troops != null && builtObject.TroopCapacityRemaining >= num2, 50, ref iterationCount2))
                    {
                        Troop troop2 = GenerateNewTroop(empire.GenerateTroopDescription(), TroopType.Infantry, troopStrength, empire, empire.DominantRace);
                        troop2.MaintenanceMultiplier = CalculateTroopMaintenanceMultiplier(empire.DominantRace);
                        troop2.PictureRef = empire.DominantRace.PictureRef;
                        troop2.BuiltObject = builtObject;
                        builtObject.Troops.Add(troop2);
                        empire.Troops.Add(troop2);
                    }
                }
            }
        }

        public void CreateResearchStations(Galaxy galaxy, Empire empire, bool allowEmpiresToStartInSameSystem)
        {
            int num = 1 + (int)((double)empire.Colonies.Count * 0.35);
            int num2 = 0;
            if (empire.ResearchHabitats.Count > 0 && num > 0)
            {
                int num3 = 0;
                Design design = null;
                int num4 = empire.ResearchHabitats.Count / num;
                int num5 = 0;
                while (design == null && num5 < empire.ResearchHabitats.Count && num5 < 200)
                {
                    num5++;
                    int num6 = 0;
                    while (num3 >= empire.ResearchHabitats.Count && num6 < 10)
                    {
                        num3 -= empire.ResearchHabitats.Count;
                    }
                    Habitat habitat = empire.ResearchHabitats[num3];
                    bool disputed = false;
                    Empire empire2 = galaxy.CheckSystemOwnership(galaxy.Systems[habitat.SystemIndex].SystemStar, out disputed);
                    if ((allowEmpiresToStartInSameSystem || empire2 == null || empire2 == empire || disputed) && galaxy.CheckEmpireTerritoryCanBuildAtHabitat(empire, habitat) && galaxy.DetermineMiningStationAtHabitatForEmpire(habitat, empire) == null && galaxy.DetermineNonMiningBaseAtHabitat(habitat) == null && (habitat.Empire == null || habitat.Empire == galaxy.IndependentEmpire))
                    {
                        _ = string.Empty;
                        design = empire.AnalyzeNewResearchFacilities(out var weaponsResearchStation, out var energyResearchStation, out var highTechResearchStation);
                        switch (habitat.ResearchBonusIndustry)
                        {
                            case IndustryType.Weapon:
                                design = weaponsResearchStation;
                                break;
                            case IndustryType.Energy:
                                design = energyResearchStation;
                                break;
                            case IndustryType.HighTech:
                                design = highTechResearchStation;
                                break;
                        }
                        if (design != null)
                        {
                            if (design == empire.EnergyResearchStation)
                            {
                                TextResolver.GetText("Energy Research Station");
                            }
                            else if (design == empire.HighTechResearchStation)
                            {
                                TextResolver.GetText("High Tech Research Station");
                            }
                            else if (design == empire.WeaponsResearchStation)
                            {
                                TextResolver.GetText("Weapons Research Station");
                            }
                        }
                        if (design != null)
                        {
                            double x;
                            double y;
                            if (habitat.Category == HabitatCategoryType.Star)
                            {
                                double num7 = 0.0;
                                Habitat habitat2 = null;
                                foreach (HabitatList asteroidField in galaxy.AsteroidFields)
                                {
                                    if (asteroidField.Count <= 0 || asteroidField[0].Parent != habitat)
                                    {
                                        continue;
                                    }
                                    int num8 = 0;
                                    while (habitat2 == null && num8 < 20)
                                    {
                                        habitat2 = asteroidField[Rnd.Next(0, asteroidField.Count)];
                                        BuiltObject builtObject = galaxy.FindNearestBuiltObject((int)habitat2.Xpos, (int)habitat2.Ypos, BuiltObjectRole.Base);
                                        if (builtObject != null)
                                        {
                                            double num9 = galaxy.CalculateDistance(habitat2.Xpos, habitat2.Ypos, builtObject.Xpos, builtObject.Ypos);
                                            if (num9 < 200.0)
                                            {
                                                habitat2 = null;
                                            }
                                        }
                                        num8++;
                                    }
                                }
                                if (habitat2 != null)
                                {
                                    habitat = habitat2;
                                    galaxy.SelectRelativeHabitatSurfacePoint(habitat, out x, out y);
                                }
                                else
                                {
                                    num7 = habitat.Diameter;
                                    if (habitat.Type == HabitatType.BlackHole)
                                    {
                                        num7 = (double)habitat.Diameter * 0.7;
                                    }
                                    else if (habitat.Type == HabitatType.SuperNova)
                                    {
                                        num7 = (double)habitat.Diameter * 0.1;
                                    }
                                    else if (habitat.Type == HabitatType.Neutron)
                                    {
                                        num7 = (double)habitat.Diameter * 2.0;
                                    }
                                    galaxy.SelectRelativeParkingPoint(num7, out x, out y);
                                }
                            }
                            else
                            {
                                galaxy.SelectRelativeHabitatSurfacePoint(habitat, out x, out y);
                            }
                            BuiltObject builtObject2 = galaxy.FindNearestBuiltObject((int)(habitat.Xpos + x), (int)(habitat.Ypos + y), BuiltObjectRole.Base);
                            double num10 = double.MaxValue;
                            if (builtObject2 != null)
                            {
                                num10 = galaxy.CalculateDistance(habitat.Xpos + x, habitat.Ypos + y, builtObject2.Xpos, builtObject2.Ypos);
                            }
                            int num11 = 0;
                            int iterationCount = 0;
                            while (ConditionCheckLimit(num10 < (double)MinimumDistanceBetweenBases, 50, ref iterationCount))
                            {
                                galaxy.SelectRelativeHabitatSurfacePoint(habitat, out x, out y);
                                builtObject2 = galaxy.FindNearestBuiltObject((int)(habitat.Xpos + x), (int)(habitat.Ypos + y), BuiltObjectRole.Base);
                                if (builtObject2 != null)
                                {
                                    num10 = galaxy.CalculateDistance(habitat.Xpos + x, habitat.Ypos + y, builtObject2.Xpos, builtObject2.Ypos);
                                    num11++;
                                    if (num11 > 5)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    num10 = double.MaxValue;
                                }
                            }
                            if (num10 >= (double)MinimumDistanceBetweenBases)
                            {
                                design.BuildCount++;
                                double purchasePrice = design.CalculateCurrentPurchasePrice(galaxy);
                                string name = galaxy.GenerateBuiltObjectName(design, habitat);
                                BuiltObject builtObject3 = new BuiltObject(design, name, galaxy, fullyBuilt: true);
                                builtObject3.PurchasePrice = purchasePrice;
                                builtObject3.ParentHabitat = habitat;
                                builtObject3.ParentOffsetX = x;
                                builtObject3.ParentOffsetY = y;
                                builtObject3.Heading = galaxy.SelectRandomHeading();
                                builtObject3.TargetHeading = builtObject3.Heading;
                                builtObject3.ReDefine();
                                builtObject3.CurrentFuel = builtObject3.FuelCapacity;
                                builtObject3.CurrentShields = builtObject3.ShieldsCapacity;
                                builtObject3.NearestSystemStar = DetermineHabitatSystemStar(habitat);
                                empire.AddBuiltObjectToGalaxy(builtObject3, habitat, offsetLocationFromParent: false, isStateOwned: true, (int)builtObject3.ParentOffsetX, (int)builtObject3.ParentOffsetY);
                                if ((habitat == null || habitat.Empire != empire) && builtObject3.Cargo != null)
                                {
                                    CargoList cargoList = ResolveRetrofitResourcesForBase(empire);
                                    for (int i = 0; i < cargoList.Count; i++)
                                    {
                                        builtObject3.Cargo.Add(cargoList[i]);
                                    }
                                }
                                num2++;
                                num3 += num4;
                                int num12 = 0;
                                while (num3 >= empire.ResearchHabitats.Count && num12 < 10)
                                {
                                    num3 -= empire.ResearchHabitats.Count;
                                }
                                if (num2 >= num)
                                {
                                    break;
                                }
                            }
                            design = null;
                        }
                    }
                    num3++;
                }
            }
            empire.DetermineResearchStationLocation(allowOccupiedSystems: false, mustHaveBuildableResearchStationDesign: true);
        }

        public void CreateSpacePorts(Galaxy galaxy, Empire empire, HabitatList spacePortColonies)
        {
            foreach (Habitat spacePortColony in spacePortColonies)
            {
                BuildSpacePortAtColony(galaxy, empire, spacePortColony);
            }
        }

        private void BuildSpacePortAtColony(Galaxy galaxy, Empire empire, Habitat colony)
        {
            Design design = null;
            long num = HabitatLargeSpacePortPopulationRequirement;
            long num2 = HabitatMediumSpacePortPopulationRequirement;
            long num3 = HabitatSmallSpacePortPopulationRequirement;
            if (empire != null && empire.Policy != null)
            {
                num = (long)empire.Policy.ConstructionSpaceportLargeColonyPopulationThreshold * 1000000L;
                num2 = (long)empire.Policy.ConstructionSpaceportMediumColonyPopulationThreshold * 1000000L;
                num3 = (long)empire.Policy.ConstructionSpaceportSmallColonyPopulationThreshold * 1000000L;
            }
            if (colony.Population.TotalAmount > num)
            {
                design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.LargeSpacePort);
            }
            else if (colony.Population.TotalAmount > num2)
            {
                design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.MediumSpacePort);
            }
            else if (colony.Population.TotalAmount > num3)
            {
                design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.SmallSpacePort);
            }
            if (empire.Colonies.Count == 1 && design != null && design.SubRole == BuiltObjectSubRole.LargeSpacePort)
            {
                design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.MediumSpacePort);
            }
            if (design != null)
            {
                design.BuildCount++;
                double purchasePrice = design.CalculateCurrentPurchasePrice(galaxy);
                string name = galaxy.GenerateBuiltObjectName(design, colony);
                BuiltObject builtObject = new BuiltObject(design, name, galaxy, fullyBuilt: true);
                builtObject.PurchasePrice = purchasePrice;
                builtObject.ParentHabitat = colony;
                double range = (double)(colony.Diameter / 6) + 15.0;
                galaxy.SelectRelativePoint(range, out var x, out var y);
                builtObject.ParentOffsetX = x;
                builtObject.ParentOffsetY = y;
                builtObject.Heading = galaxy.SelectRandomHeading();
                builtObject.TargetHeading = builtObject.Heading;
                builtObject.ReDefine();
                builtObject.CurrentFuel = builtObject.FuelCapacity;
                builtObject.CurrentShields = builtObject.ShieldsCapacity;
                builtObject.NearestSystemStar = DetermineHabitatSystemStar(colony);
                empire.AddBuiltObjectToGalaxy(builtObject, colony, offsetLocationFromParent: false, isStateOwned: true, (int)builtObject.ParentOffsetX, (int)builtObject.ParentOffsetY);
            }
        }

        public EmpireList ResolveEmpiresOfRaceFamily(byte raceFamilyId)
        {
            EmpireList empireList = new EmpireList();
            for (int i = 0; i < Empires.Count; i++)
            {
                if (Empires[i].Active && Empires[i].PirateEmpireBaseHabitat == null && Empires[i].DominantRace != null && Empires[i].DominantRace.FamilyId == raceFamilyId)
                {
                    empireList.Add(Empires[i]);
                }
            }
            return empireList;
        }

        public Empire FindNearestEmpireCapital(double x, double y, EmpireList empiresToExclude)
        {
            Empire result = null;
            double num = double.MaxValue;
            for (int i = 0; i < Empires.Count; i++)
            {
                if (Empires[i].Active && Empires[i].PirateEmpireBaseHabitat == null && (empiresToExclude == null || !empiresToExclude.Contains(Empires[i])) && Empires[i].Capital != null)
                {
                    double num2 = CalculateDistance(x, y, Empires[i].Capital.Xpos, Empires[i].Capital.Ypos);
                    if (num2 < num)
                    {
                        result = Empires[i];
                        num = num2;
                    }
                }
            }
            return result;
        }

        public void CheckAndFixGameFromStaticThemeDefinitions()
        {
            CheckAndFixEmpireFromStaticThemeDefinitions(IndependentEmpire);
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                CheckAndFixEmpireFromStaticThemeDefinitions(empire);
            }
            for (int j = 0; j < PirateEmpires.Count; j++)
            {
                Empire empire2 = PirateEmpires[j];
                CheckAndFixEmpireFromStaticThemeDefinitions(empire2);
            }
        }

        private void CheckAndFixEmpireFromStaticThemeDefinitions(Empire empire)
        {
            if (empire != null)
            {
                if (empire.Research != null)
                {
                    empire.Research.MergeNewDefinitions(ResearchNodeDefinitionsStatic, empire.DominantRace);
                }
                if (empire.Designs != null)
                {
                    empire.Designs.StripInvalidComponents();
                }
                if (empire.BuiltObjects != null)
                {
                    empire.BuiltObjects.StripInvalidComponents();
                }
                if (empire.PrivateBuiltObjects != null)
                {
                    empire.PrivateBuiltObjects.StripInvalidComponents();
                }
                if (empire.Colonies != null)
                {
                    empire.Colonies.StripInvalidFacilities();
                }
            }
        }

        public void GenerateShakturi(Habitat startingColony)
        {
            if (NextEmpireID >= MaximumEmpireCount)
            {
                return;
            }
            Race race = Races["Shakturi"];
            if (race == null)
            {
                return;
            }
            int age = 2;
            Race race2 = (ShakturiOriginalRace = new Race(race.Name, race.PictureRef, race.ReproductiveRate, race.IntelligenceLevel, race.AggressionLevel, race.CautionLevel, race.FriendlinessLevel, race.LoyaltyLevel, race.DesignPictureFamilyIndex, race.DesignNameIndex, race.TroopName));
            ShakturiActualRace = race;
            race.AggressionLevel = 75;
            race.CautionLevel = 105;
            race.FriendlinessLevel = 125;
            race.IntelligenceLevel = 128;
            race.LoyaltyLevel = 100;
            race.Name = "Erutkah";
            race.TroopName = "Erutkah Defender";
            StoryShakturiEnrageTimer = CurrentStarDate + Rnd.Next(7000, 10000) * RealSecondsInGalacticYear;
            string text = TextResolver.GetText("Normal");
            HabitatType capitalHabitatType = HabitatType.Undefined;
            double homeSystemFactor = 0.0;
            ResolveHomeSystem(text, out capitalHabitatType, out homeSystemFactor);
            double expansion = 0.0;
            string empireName = "Erutkah Refugees";
            int governmentId = 0;
            GovernmentAttributes firstByAvailability = Governments.GetFirstByAvailability(3);
            if (firstByAvailability != null)
            {
                governmentId = firstByAvailability.GovernmentId;
            }
            double actualTechLevel = 0.0;
            Empire empire = GenerateEmpire(this, isPlayerEmpire: false, empireName, startingColony, race, race.DesignPictureFamilyIndex, governmentId, homeSystemFactor, text, age, 7.0, 1.0, out expansion, null, null, out actualTechLevel, "Shakturi");
            startingColony.BaseQuality = 1f;
            Ruin ruin = new Ruin("Palace of Eternal Darkness", 12, 0.5, 0.0, 0.0, 0, 0, 0);
            ruin.BonusWealth = 2.0;
            ruin.PlayerEmpireEncountered = true;
            startingColony.Ruin = ruin;
            Habitat habitat = DetermineHabitatSystemStar(ShakturiTriggerHabitat);
            SystemVisibilityStatus status = empire.SystemVisibility[habitat.SystemIndex].Status;
            if (status == SystemVisibilityStatus.Explored)
            {
                empire.SystemVisibility[habitat.SystemIndex].Status = SystemVisibilityStatus.Unexplored;
            }
            if (empire.Policy != null)
            {
                empire.Policy.ConstructionMilitary = 2;
            }
            empire.RecalculateEmpirePopulation();
            empire.CheckColoniesForBaseFacilities();
            empire.RecalculateEmpireCorruption();
            empire.LastLongTouch = CurrentDateTime.Subtract(new TimeSpan(0, 0, (int)empire.LongProcessingInterval + 1));
            empire.LastIntermediateTouch = empire.LastLongTouch;
            empire.LastPeriodicTouch = empire.LastLongTouch;
            empire.LastRegularTouch = empire.LastLongTouch;
            empire.LastShortTouch = empire.LastLongTouch;
            empire.LastHugeTouch = empire.LastLongTouch;
            int newSpacePortAmount = 1 + (int)((double)empire.Colonies.Count / 3.5);
            HabitatList habitatList = empire.DetermineNewSpacePortLocations(empire.Colonies, newSpacePortAmount, excludeColoniesWithEnemiesPresent: false);
            CreateSpacePorts(this, empire, habitatList);
            foreach (Habitat item in habitatList)
            {
                SetColonyResources(this, item, empire, hasSpacePort: true);
            }
            empire.CheckColoniesForBaseFacilities();
            CreateMiningStations(this, empire, allowEmpiresToStartInSameSystem: true);
            SetLuxuryResourcesAtColonies(this, empire);
            for (int i = 0; i < 1; i++)
            {
                empire.ReviewTaxes();
            }
            empire.RecalculateEmpirePopulation();
            foreach (Habitat colony in empire.Colonies)
            {
                empire.ProcessColonyTroops(colony, null, 0.0, 100.0, 100.0);
                empire.ProcessColonyTroops(colony, null, 0.0, 300.0, 300.0);
                empire.ProcessColonyTroops(colony, null, 0.0, 300.0, 300.0);
                colony.RecalculateAnnualTaxRevenue();
            }
            empire.ReviewTaxes();
            foreach (Habitat colony2 in empire.Colonies)
            {
                colony2.RecalculateAnnualTaxRevenue();
            }
            empire.DetermineResearchStationLocation(allowOccupiedSystems: false, mustHaveBuildableResearchStationDesign: true);
            CreateResearchStations(this, empire, allowEmpiresToStartInSameSystem: true);
            CreateStateShips(this, empire);
            CreatePrivateShips(this, empire);
            FillShipsWithTroops(this, empire);
            empire.AssignMissionsToBuiltObjectList(empire.BuiltObjects, atWar: false, null);
            empire.AssignMissionsToBuiltObjectList(empire.PrivateBuiltObjects, atWar: false, null);
            DoTasks(gameFinished: false, PlayerEmpire, null, null, null);
            for (int j = 0; j < Empires.Count; j++)
            {
                Empire empire2 = Empires[j];
                if (empire2 != empire)
                {
                    EmpireEvaluation empireEvaluation = empire.ObtainEmpireEvaluation(empire2);
                    if (empireEvaluation != null)
                    {
                        empireEvaluation.Bias = Math.Max(0.0, empireEvaluation.Bias);
                    }
                }
            }
            for (int k = 0; k < Empires.Count; k++)
            {
                Empire empire3 = Empires[k];
                if (empire3 != empire)
                {
                    EmpireEvaluation empireEvaluation2 = empire3.ObtainEmpireEvaluation(empire);
                    if (empireEvaluation2 != null)
                    {
                        empireEvaluation2.Bias = Math.Max(0.0, empireEvaluation2.Bias);
                    }
                }
            }
        }

        public void GenerateShakturiMilitaryConvoy(Empire shakturiEmpire)
        {
            GenerateMilitaryConvoy(shakturiEmpire, 10, 0.2f);
        }

        public void GenerateMilitaryConvoy(Empire empire, int size, float supportCostFactor)
        {
            Habitat habitat = empire.Capital;
            if (empire.PirateEmpireBaseHabitat != null)
            {
                habitat = empire.PirateEmpireBaseHabitat;
            }
            if (habitat == null)
            {
                return;
            }
            Point point = FindNearestGalaxyEdgeCoords(habitat.Xpos, habitat.Ypos);
            double num = point.X;
            double num2 = point.Y;
            for (int i = 0; i < size; i++)
            {
                Design design = null;
                int num3 = Rnd.Next(0, 10);
                int num4 = 0;
                while (design == null && num4 < 50)
                {
                    switch (num3)
                    {
                        case 0:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.Escort);
                            break;
                        case 1:
                        case 2:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.Frigate);
                            break;
                        case 3:
                        case 4:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.Destroyer);
                            break;
                        case 5:
                        case 6:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.Cruiser);
                            break;
                        case 7:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.CapitalShip);
                            break;
                        case 8:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.TroopTransport);
                            break;
                        case 9:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.Carrier);
                            break;
                    }
                    num4++;
                }
                if (design != null)
                {
                    SelectRelativeParkingPoint(out var x, out var y);
                    BuiltObject builtObject = empire.GenerateNewBuiltObject(design, null, num + x, num2 + y);
                    if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip)
                    {
                        builtObject.Name = "Shakturi World Ship";
                        builtObject.NativeRace = empire.DominantRace;
                    }
                    empire.TakeOwnershipOfBuiltObject(builtObject, empire, setDesignAsObsolete: false);
                    builtObject.SupportCostFactor = supportCostFactor;
                    builtObject.IsAutoControlled = true;
                    builtObject.AssignMission(BuiltObjectMissionType.Move, habitat, null, BuiltObjectMissionPriority.Normal);
                }
            }
        }

        public void GenerateShakturiColonyConvoy(Empire shakturiEmpire)
        {
            GenerateCivilianConvoy(shakturiEmpire, 10, 0.2f, "Erutkah World Ship");
        }

        public void GenerateCivilianConvoy(Empire empire, int size, float supportCostFactor, string colonyShipNameOverride)
        {
            Habitat habitat = empire.Capital;
            if (empire.PirateEmpireBaseHabitat != null)
            {
                habitat = empire.PirateEmpireBaseHabitat;
            }
            if (habitat == null)
            {
                return;
            }
            Point point = FindNearestGalaxyEdgeCoords(habitat.Xpos, habitat.Ypos);
            double num = point.X;
            double num2 = point.Y;
            for (int i = 0; i < size; i++)
            {
                Design design = null;
                int num3 = Rnd.Next(0, 10);
                int num4 = 0;
                while (design == null && num4 < 50)
                {
                    switch (num3)
                    {
                        case 0:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ExplorationShip);
                            break;
                        case 1:
                        case 2:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
                            break;
                        case 3:
                        case 4:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.PassengerShip);
                            break;
                        case 5:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ConstructionShip);
                            break;
                        case 6:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.GasMiningShip);
                            break;
                        case 7:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.MiningShip);
                            break;
                        case 8:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.Escort);
                            break;
                        case 9:
                            design = empire.Designs.FindNewestCanBuild(BuiltObjectSubRole.SmallFreighter);
                            break;
                    }
                    num4++;
                }
                if (design == null)
                {
                    continue;
                }
                SelectRelativeParkingPoint(out var x, out var y);
                BuiltObject builtObject = empire.GenerateNewBuiltObject(design, null, num + x, num2 + y);
                if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip)
                {
                    if (!string.IsNullOrEmpty(colonyShipNameOverride))
                    {
                        builtObject.Name = colonyShipNameOverride;
                    }
                    builtObject.NativeRace = empire.DominantRace;
                }
                empire.TakeOwnershipOfBuiltObject(builtObject, empire, setDesignAsObsolete: false);
                builtObject.SupportCostFactor = supportCostFactor;
                builtObject.IsAutoControlled = true;
                builtObject.AssignMission(BuiltObjectMissionType.Move, habitat, null, BuiltObjectMissionPriority.Normal);
            }
        }

        public Empire IdentifyMechanoidEmpire()
        {
            Empire result = null;
            for (int i = 0; i < Empires.Count; i++)
            {
                if (Empires[i].PirateEmpireBaseHabitat == null && Empires[i].DominantRace != null && Empires[i].DominantRace.Name.ToLower(CultureInfo.InvariantCulture) == "mechanoid")
                {
                    result = Empires[i];
                    break;
                }
            }
            return result;
        }

        public Empire IdentifyShakturiEmpire()
        {
            Empire result = null;
            if (ShakturiActualRace != null)
            {
                for (int i = 0; i < Empires.Count; i++)
                {
                    if (Empires[i].PirateEmpireBaseHabitat == null && Empires[i].DominantRace != null && Empires[i].DominantRace == ShakturiActualRace)
                    {
                        result = Empires[i];
                        break;
                    }
                }
            }
            return result;
        }

        public void GenerateShakturiAggression(Empire shakturiEmpire)
        {
            if (shakturiEmpire == null || ShakturiOriginalRace == null)
            {
                return;
            }
            if (shakturiEmpire.DominantRace != null)
            {
                shakturiEmpire.DominantRace.AggressionLevel = ShakturiOriginalRace.AggressionLevel;
                shakturiEmpire.DominantRace.CautionLevel = ShakturiOriginalRace.CautionLevel;
                shakturiEmpire.DominantRace.FriendlinessLevel = ShakturiOriginalRace.FriendlinessLevel;
                shakturiEmpire.DominantRace.IntelligenceLevel = ShakturiOriginalRace.IntelligenceLevel;
                shakturiEmpire.DominantRace.LoyaltyLevel = ShakturiOriginalRace.LoyaltyLevel;
            }
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire == shakturiEmpire)
                {
                    continue;
                }
                EmpireEvaluation empireEvaluation = shakturiEmpire.ObtainEmpireEvaluation(empire);
                if (empire.DominantRace != null)
                {
                    if (RaceFamilies.GetIdsBySpecialFunctionCode(1).Contains(empire.DominantRace.FamilyId))
                    {
                        empireEvaluation.Bias = Math.Max(empireEvaluation.Bias, 40.0);
                    }
                    else if (RaceFamilies.GetIdsBySpecialFunctionCode(2).Contains(empire.DominantRace.FamilyId))
                    {
                        empireEvaluation.Bias = Math.Min(empireEvaluation.Bias, -30.0);
                    }
                }
            }
            for (int j = 0; j < Empires.Count; j++)
            {
                Empire empire2 = Empires[j];
                if (empire2 == shakturiEmpire)
                {
                    continue;
                }
                EmpireEvaluation empireEvaluation2 = empire2.ObtainEmpireEvaluation(shakturiEmpire);
                if (empire2.DominantRace != null)
                {
                    if (RaceFamilies.GetIdsBySpecialFunctionCode(1).Contains(empire2.DominantRace.FamilyId))
                    {
                        empireEvaluation2.Bias = Math.Max(empireEvaluation2.Bias, 40.0);
                    }
                    else if (RaceFamilies.GetIdsBySpecialFunctionCode(2).Contains(empire2.DominantRace.FamilyId))
                    {
                        empireEvaluation2.Bias = Math.Min(empireEvaluation2.Bias, -30.0);
                    }
                }
            }
            EmpirePolicy empirePolicy = (shakturiEmpire.Policy = LoadEmpirePolicy(ShakturiOriginalRace, isPirate: false));
            StoryShakturiEnraged = true;
        }

        public void GenerateShakturiInvasion(Empire shakturiEmpire, Empire mechanoidEmpire)
        {
            if (shakturiEmpire == null || mechanoidEmpire == null)
            {
                return;
            }
            Habitat parentHabitat = null;
            if (shakturiEmpire.Capital != null)
            {
                parentHabitat = shakturiEmpire.Capital;
            }
            else if (shakturiEmpire.Colonies.Count > 0)
            {
                parentHabitat = shakturiEmpire.Colonies[0];
            }
            if (shakturiEmpire.DominantRace != null)
            {
                shakturiEmpire.DominantRace.Name = ShakturiOriginalRace.Name;
                shakturiEmpire.DominantRace.TroopName = ShakturiOriginalRace.TroopName;
                shakturiEmpire.DominantRace.TroopNameArmored = ShakturiOriginalRace.TroopNameArmored;
                shakturiEmpire.DominantRace.TroopNameArtillery = ShakturiOriginalRace.TroopNameArtillery;
                shakturiEmpire.DominantRace.TroopNameSpecialForces = ShakturiOriginalRace.TroopNameSpecialForces;
            }
            shakturiEmpire.Name = "Shaktur Supremacy";
            int num = 1;
            num = ((StarCount >= 1000) ? 3 : ((StarCount < 700) ? 1 : 2));
            int num2 = num;
            Design design = GeneratePlanetDestroyerDesign();
            shakturiEmpire.Designs.Add(design);
            design.Empire = shakturiEmpire;
            string[] array = new string[4] { "Revenge of Shaktur", "Death of Worlds", "Dark Reaper", "Desolation of Utopia" };
            for (int i = 0; i < num2; i++)
            {
                BuiltObject builtObject = shakturiEmpire.GenerateNewBuiltObject(design, parentHabitat);
                builtObject.Name = array[i];
                builtObject.SupportCostFactor = 0.1f;
            }
            for (int j = 0; j < 10; j++)
            {
                Design design2 = null;
                int num3 = 0;
                switch (j)
                {
                    case 0:
                        design2 = shakturiEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.Escort);
                        num3 = 5 * num;
                        break;
                    case 1:
                        design2 = shakturiEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.Frigate);
                        num3 = 6 * num;
                        break;
                    case 2:
                        design2 = shakturiEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.Destroyer);
                        num3 = 4 * num;
                        break;
                    case 3:
                        design2 = shakturiEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.Cruiser);
                        num3 = 3 * num;
                        break;
                    case 4:
                        design2 = shakturiEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.CapitalShip);
                        num3 = 2 * num;
                        break;
                    case 5:
                        design2 = shakturiEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.TroopTransport);
                        num3 = 2 * num;
                        break;
                    case 6:
                        design2 = shakturiEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.Carrier);
                        num3 = 2 * num;
                        break;
                    case 7:
                        design2 = shakturiEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ResupplyShip);
                        num3 = num;
                        break;
                    case 8:
                        design2 = shakturiEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ExplorationShip);
                        num3 = 2 * num;
                        break;
                    case 9:
                        design2 = shakturiEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
                        num3 = 2 * num;
                        break;
                }
                if (design2 == null || num3 <= 0)
                {
                    continue;
                }
                for (int k = 0; k < num3; k++)
                {
                    BuiltObject builtObject2 = shakturiEmpire.GenerateNewBuiltObject(design2, parentHabitat);
                    if (builtObject2.SubRole == BuiltObjectSubRole.ColonyShip)
                    {
                        builtObject2.Name = "Shakturi World Ship";
                        builtObject2.NativeRace = shakturiEmpire.DominantRace;
                    }
                    shakturiEmpire.TakeOwnershipOfBuiltObject(builtObject2, shakturiEmpire, setDesignAsObsolete: false);
                    builtObject2.SupportCostFactor = 0.2f;
                    builtObject2.IsAutoControlled = true;
                }
            }
            shakturiEmpire.TargetHabitat = mechanoidEmpire.Capital;
        }

        public BuiltObject GenerateDeliverancePlanetDestroyer()
        {
            Design design = GeneratePlanetDestroyerDesign(1.6);
            PlayerEmpire.Designs.Add(design);
            design.Empire = PlayerEmpire;
            BuiltObject builtObject = PlayerEmpire.GenerateNewBuiltObject(design, PlayerEmpire.Capital);
            builtObject.Name = "Deliverance";
            PlayerEmpire.TakeOwnershipOfBuiltObject(builtObject, PlayerEmpire, setDesignAsObsolete: false);
            builtObject.SupportCostFactor = 0.1f;
            builtObject.IsAutoControlled = false;
            return builtObject;
        }

        public void GenerateShakturiReturnTriggerRuins()
        {
            if (ShakturiTriggerHabitat != null)
            {
                return;
            }
            Ruin ruin = new Ruin("Beacon of Shaktur", 5, 0.05, 0.0, 0.0, 0, 0, 0);
            ruin.Type = RuinType.StoryEvent;
            ruin.StoryEventData = 1;
            Empire empire = null;
            for (int i = 0; i < Empires.Count; i++)
            {
                if (Empires[i].DominantRace != null && Empires[i].DominantRace.Name.ToLower(CultureInfo.InvariantCulture) == "mechanoid")
                {
                    empire = Empires[i];
                }
            }
            double num = 0.0;
            double num2 = 0.0;
            if (PlayerEmpire != null && PlayerEmpire.Capital != null)
            {
                num = PlayerEmpire.Capital.Xpos;
                num2 = PlayerEmpire.Capital.Ypos;
            }
            else if (PlayerEmpire != null && PlayerEmpire.PirateEmpireBaseHabitat != null)
            {
                num = PlayerEmpire.PirateEmpireBaseHabitat.Xpos;
                num2 = PlayerEmpire.PirateEmpireBaseHabitat.Ypos;
            }
            Point point = Point.Empty;
            double num3 = 0.0;
            int num4 = 0;
            while (num3 < (double)SectorSize * 3.0 && num4 < 50)
            {
                point = FindNearestGalaxyEdgeCoordsMinimumRange(num, num2, (double)SectorSize * 2.0, (double)SectorSize * 2.0);
                num3 = CalculateDistance(point.X, point.Y, num, num2);
                if (num4 > 0 && empire != null && empire.Capital != null)
                {
                    num = empire.Capital.Xpos;
                    num2 = empire.Capital.Ypos;
                    num3 = CalculateDistance(point.X, point.Y, num, num2);
                }
                num4++;
            }
            Habitat habitat = FindLonelyHabitat(point.X, point.Y, HabitatType.Ice);
            if (habitat == null)
            {
                habitat = FindLonelyHabitatGalacticEdge(RuinType.Government);
            }
            if (habitat == null)
            {
                habitat = FindLonelyHabitat();
            }
            if (habitat == null)
            {
                habitat = FindNearestHabitat(0.0, 0.0, HabitatCategoryType.Moon);
            }
            if (habitat != null)
            {
                habitat.Ruin = ruin;
                ShakturiTriggerHabitat = habitat;
            }
        }

        public void CheckGenerateAncientHelpers()
        {
            if (!StoryReturnOfTheShakturiEnabled)
            {
                return;
            }
            Empire empire = IdentifyMechanoidEmpire();
            if (empire == null)
            {
                double startX = 0.0;
                double startY = 0.0;
                if (PlayerEmpire.Capital != null)
                {
                    startX = PlayerEmpire.Capital.Xpos;
                    startY = PlayerEmpire.Capital.Ypos;
                }
                else if (PlayerEmpire.PirateEmpireBaseHabitat != null)
                {
                    startX = PlayerEmpire.PirateEmpireBaseHabitat.Xpos;
                    startY = PlayerEmpire.PirateEmpireBaseHabitat.Ypos;
                }
                double x = 0.0;
                double y = 0.0;
                ObtainRandomGalaxyCoordinatesFromPoint(startX, startY, (double)SectorSize * 2.0, out x, out y);
                Habitat habitat = FindNearestHabitatUnoccupiedSystem(x, y, HabitatType.Continental);
                if (habitat == null)
                {
                    habitat = FindNearestHabitatUnoccupiedSystem(x, y, HabitatType.MarshySwamp);
                }
                if (habitat == null)
                {
                    habitat = FindNearestHabitatUnoccupiedSystem(x, y, HabitatType.Desert);
                }
                if (habitat == null)
                {
                    habitat = FindNearestHabitatUnoccupiedSystem(x, y, HabitatType.Ocean);
                }
                if (habitat == null)
                {
                    habitat = FindNearestHabitatUnoccupiedSystem(x, y, HabitatType.Ice);
                }
                if (habitat == null)
                {
                    habitat = FindNearestHabitatUnoccupiedSystem(x, y, HabitatType.Volcanic);
                }
                if (habitat == null)
                {
                    habitat = FindNearestHabitatUnoccupiedSystem(x, y, HabitatType.BarrenRock);
                }
                if (habitat.Population != null && habitat.Population.Count > 0)
                {
                    habitat.Population.Clear();
                    habitat.Population.RecalculateTotalAmount();
                }
                habitat.Name = "Utopia";
                GenerateAncientHelpers(habitat);
            }
        }

        public void GenerateAncientHelpers(Habitat homeColony)
        {
            if (NextEmpireID >= MaximumEmpireCount)
            {
                return;
            }
            Race race = Races["Mechanoid"];
            if (race == null)
            {
                return;
            }
            string text = TextResolver.GetText("Normal");
            HabitatType capitalHabitatType = HabitatType.Undefined;
            double homeSystemFactor = 0.0;
            ResolveHomeSystem(text, out capitalHabitatType, out homeSystemFactor);
            double expansion = 0.0;
            int governmentId = 0;
            GovernmentAttributes firstByAvailability = Governments.GetFirstByAvailability(2);
            if (firstByAvailability != null)
            {
                governmentId = firstByAvailability.GovernmentId;
            }
            Empire empire = GenerateEmpire(this, isPlayerEmpire: false, "Ancient Guardians", homeColony, race, race.DesignPictureFamilyIndex, governmentId, homeSystemFactor, text, 1, 7.0, 1.0, out expansion, null, null);
            if (empire.Policy != null)
            {
                empire.Policy.ColonyAllowFacilityCloningFacility = false;
                empire.Policy.ColonyAllowFacilityTroopTrainingCenter = false;
                empire.Policy.ConstructionMilitary = 1;
                empire.Policy.DiplomacyTradeSanctionsUseBlockades = false;
                empire.Policy.WarAttacksHarassEnemies = false;
            }
            homeColony.BaseQuality = 1f;
            for (int i = 0; i < Systems[homeColony.SystemIndex].Habitats.Count; i++)
            {
                Habitat habitat = Systems[homeColony.SystemIndex].Habitats[i];
                if (habitat == homeColony)
                {
                    continue;
                }
                if (habitat.Population != null && habitat.Population.TotalAmount > 0)
                {
                    habitat.ClearColony(null);
                }
                switch (habitat.Type)
                {
                    case HabitatType.Volcanic:
                    case HabitatType.Desert:
                    case HabitatType.MarshySwamp:
                    case HabitatType.Continental:
                    case HabitatType.Ocean:
                    case HabitatType.Ice:
                        if (habitat.Category == HabitatCategoryType.Planet || habitat.Category == HabitatCategoryType.Moon)
                        {
                            if (habitat.Ruin != null)
                            {
                                habitat.Ruin = null;
                            }
                            HabitatType type = HabitatType.BarrenRock;
                            int pictureRef = 0;
                            int landscapePictureRef = 0;
                            int diameter = 0;
                            int minOrbitDistance = 0;
                            int maxOrbitDistance = 0;
                            SelectBarrenRockPlanet(out type, out pictureRef, out diameter, out minOrbitDistance, out maxOrbitDistance, out landscapePictureRef);
                            habitat.Type = type;
                            habitat.Diameter = (short)diameter;
                            habitat.PictureRef = (short)pictureRef;
                            habitat.LandscapePictureRef = (short)landscapePictureRef;
                            habitat.BaseQuality = SelectHabitatQuality(habitat, (float)ColonyPrevalence);
                            if (habitat.Resources != null)
                            {
                                habitat.Resources.Clear();
                            }
                            SelectResources(habitat);
                        }
                        break;
                }
            }
            Ruin ruin = new Ruin("Ancient Galactic Archives", 11, 0.5, -30.0, -30.0, 0, 0, 0);
            ruin.BonusWealth = 1.0;
            ruin.PlayerEmpireEncountered = true;
            homeColony.Ruin = ruin;
            if (homeColony.Resources == null)
            {
                homeColony.Resources = new HabitatResourceList();
            }
            ResourceDefinition byName = ResourceSystem.Resources.GetByName("Loros Fruit");
            if (byName != null)
            {
                if (homeColony.Resources.Count < 5)
                {
                    homeColony.Resources.Add(new HabitatResource(byName.ResourceID, Rnd.Next(500, 600)));
                }
                else
                {
                    homeColony.Resources[4] = new HabitatResource(byName.ResourceID, Rnd.Next(500, 600));
                }
            }
            empire.DefendHabitat = homeColony;
            SetEmpireExplorationAmount(empire, (int)((double)StarCount * 0.05));
            CreateSpacePorts(this, empire, empire.Colonies);
            CreateMiningStations(this, empire, allowEmpiresToStartInSameSystem: false);
            CreateStateShips(this, empire);
            CreatePrivateShips(this, empire);
            FillShipsWithTroops(this, empire);
            empire.AssignMissionsToBuiltObjectList(empire.BuiltObjects, atWar: false, null);
            empire.AssignMissionsToBuiltObjectList(empire.PrivateBuiltObjects, atWar: false, null);
            DoTasks(gameFinished: false, PlayerEmpire, null, null, null);
        }

        public ShipGroup GenerateFreedomAlliance(bool includePlayer, ref Game game)
        {
            ShipGroup shipGroup = null;
            Empire empire = null;
            Empire empire2 = null;
            EmpireList empireList = new EmpireList();
            if (includePlayer)
            {
                empireList.Add(PlayerEmpire);
            }
            Empire empire3 = null;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire4 = Empires[i];
                if (empire4 == PlayerEmpire)
                {
                    continue;
                }
                string text = ResolveRaceFamilyDescription(empire4.DominantRace.FamilyId);
                if ((empire4.DominantRace != null && text.ToLower(CultureInfo.InvariantCulture) == "humanoid") || (empire4.DominantRace != null && empire4.DominantRace.Name.ToLower(CultureInfo.InvariantCulture) == "ackdarian"))
                {
                    if (empire3 == null || empire3.MilitaryPotency < empire4.MilitaryPotency)
                    {
                        empire3 = empire4;
                    }
                    empireList.Add(empire4);
                }
                else if (empire4.DominantRace != null && empire4.DominantRace.Name.ToLower(CultureInfo.InvariantCulture) == "mechanoid")
                {
                    empire2 = empire4;
                    empireList.Add(empire4);
                }
                if (empire4.DominantRace != null && empire4.DominantRace == ShakturiActualRace)
                {
                    empire = empire4;
                    if (empireList.Contains(empire))
                    {
                        empireList.Remove(empire);
                    }
                }
            }
            if (includePlayer)
            {
                empire3 = PlayerEmpire;
            }
            if (empire3 == null)
            {
                empire3 = empire2;
            }
            if (empire2 != null)
            {
                for (int j = 0; j < empireList.Count; j++)
                {
                    Empire empire5 = empireList[j];
                    for (int k = 0; k < empireList.Count; k++)
                    {
                        if (empire5 != empireList[k])
                        {
                            DiplomaticRelation diplomaticRelation = empire5.ObtainDiplomaticRelation(empireList[k]);
                            if (diplomaticRelation.Type == DiplomaticRelationType.War)
                            {
                                empire5.ResetAttitudeLevelsAtEndOfWar(diplomaticRelation);
                                diplomaticRelation.Type = DiplomaticRelationType.None;
                                diplomaticRelation.LastDiplomacyTradeOfferDate = CurrentStarDate;
                                DiplomaticRelation diplomaticRelation2 = empireList[k].ObtainDiplomaticRelation(empire5);
                                diplomaticRelation2.Type = DiplomaticRelationType.None;
                                diplomaticRelation2.LastDiplomacyTradeOfferDate = CurrentStarDate;
                                empire5.ProcessEndOfWarWithEmpire(empireList[k]);
                                empireList[k].ProcessEndOfWarWithEmpire(empire5);
                                empire5.ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.None);
                                empire5.SendNewsBroadcastWarStartEnd(diplomaticRelation);
                            }
                            if (diplomaticRelation.Type != DiplomaticRelationType.MutualDefensePact)
                            {
                                empire5.ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.MutualDefensePact, blockFlowonEffects: true);
                                EmpireEvaluation empireEvaluation = empire5.ObtainEmpireEvaluation(empireList[k]);
                                empireEvaluation.IncidentEvaluation = Math.Max(10.0, empireEvaluation.IncidentEvaluation);
                                empireEvaluation.Bias += 30.0;
                                EmpireEvaluation empireEvaluation2 = empireList[k].ObtainEmpireEvaluation(empire5);
                                empireEvaluation2.IncidentEvaluation = Math.Max(10.0, empireEvaluation2.IncidentEvaluation);
                                empireEvaluation2.Bias += 30.0;
                            }
                        }
                    }
                    empire5.ObtainDiplomaticRelation(empire);
                    empire5.DeclareWar(empire, null, lockedWar: true);
                    empire5.ObtainEmpireEvaluation(empire).Bias -= 30.0;
                }
                int num = 1;
                num = ((StarCount < 700) ? 1 : 2);
                if (empire3 != null)
                {
                    shipGroup = new ShipGroup(this);
                    shipGroup.Name = "Guardian Fleet";
                    shipGroup.Empire = empire3;
                    empire3.ShipGroups.Add(shipGroup);
                    empire3.ShipGroups.Sort();
                    for (int l = 0; l < 8; l++)
                    {
                        Design design = null;
                        int num2 = 0;
                        switch (l)
                        {
                            case 0:
                                design = empire2.Designs.FindNewestCanBuild(BuiltObjectSubRole.Escort);
                                num2 = 3 * num;
                                break;
                            case 1:
                                design = empire2.Designs.FindNewestCanBuild(BuiltObjectSubRole.Frigate);
                                num2 = 4 * num;
                                break;
                            case 2:
                                design = empire2.Designs.FindNewestCanBuild(BuiltObjectSubRole.Destroyer);
                                num2 = 3 * num;
                                break;
                            case 3:
                                design = empire2.Designs.FindNewestCanBuild(BuiltObjectSubRole.Cruiser);
                                num2 = 2 * num;
                                break;
                            case 4:
                                design = empire2.Designs.FindNewestCanBuild(BuiltObjectSubRole.CapitalShip);
                                num2 = num;
                                break;
                            case 5:
                                design = empire2.Designs.FindNewestCanBuild(BuiltObjectSubRole.TroopTransport);
                                num2 = 2 * num;
                                break;
                            case 6:
                                design = empire2.Designs.FindNewestCanBuild(BuiltObjectSubRole.Carrier);
                                num2 = 2 * num;
                                break;
                            case 7:
                                design = empire2.Designs.FindNewestCanBuild(BuiltObjectSubRole.ResupplyShip);
                                num2 = num;
                                break;
                        }
                        if (design == null || num2 <= 0)
                        {
                            continue;
                        }
                        for (int m = 0; m < num2; m++)
                        {
                            BuiltObject builtObject = empire2.GenerateNewBuiltObject(design, empire2.Capital);
                            builtObject.MaintenanceSavings = 0.2f;
                            empire3.TakeOwnershipOfBuiltObject(builtObject, empire3, setDesignAsObsolete: false);
                            if (builtObject.SubRole != BuiltObjectSubRole.ResupplyShip)
                            {
                                shipGroup.AddShipToFleet(builtObject);
                            }
                            builtObject.IsAutoControlled = false;
                        }
                    }
                    if (empire2 != null && empire2.Capital != null)
                    {
                        shipGroup.GatherPoint = empire2.Capital;
                    }
                    else
                    {
                        shipGroup.GatherPoint = empire3.Capital;
                    }
                    shipGroup.Update();
                }
                empire2.Reclusive = false;
                if (includePlayer)
                {
                    game.GlobalVictoryConditions.DefendHabitat = empire2.Capital;
                    game.GlobalVictoryConditions.DefendHabitatEmpire = empire2;
                    game.GlobalVictoryConditions.TargetHabitat = empire.Capital;
                    game.GlobalVictoryConditions.TargetHabitatEmpire = empire;
                }
            }
            return shipGroup;
        }

        public static BuiltObjectSubRole ResolveLegacySubRole(BuiltObjectSubRole subRole)
        {
            BuiltObjectSubRole result = subRole;
            switch (subRole)
            {
                case BuiltObjectSubRole.EnergyResearchStation:
                case BuiltObjectSubRole.WeaponsResearchStation:
                case BuiltObjectSubRole.HighTechResearchStation:
                case BuiltObjectSubRole.MonitoringStation:
                    result = BuiltObjectSubRole.GenericBase;
                    break;
                case BuiltObjectSubRole.DefensiveBase:
                    result = BuiltObjectSubRole.MediumSpacePort;
                    break;
            }
            return result;
        }

        public int DetermineLifeSupportRequired(ComponentImprovement lifeSupportComponent, Design design)
        {
            int designSize = design.QuickCalculateSize();
            bool designIsBase = false;
            if (design.Role == BuiltObjectRole.Base)
            {
                designIsBase = true;
            }
            return DetermineLifeSupportRequired(lifeSupportComponent, designSize, designIsBase);
        }

        public int DetermineLifeSupportRequired(ComponentImprovement lifeSupportComponent, int designSize, bool designIsBase)
        {
            int num = 0;
            int num2 = 0;
            if (lifeSupportComponent != null)
            {
                if (designIsBase)
                {
                    num = designSize / 2 / lifeSupportComponent.Value1 + 1;
                    num2 = designSize / 2 % lifeSupportComponent.Value1;
                }
                else
                {
                    num = designSize / lifeSupportComponent.Value1 + 1;
                    num2 = designSize % lifeSupportComponent.Value1;
                }
                if (num2 == 0)
                {
                    num--;
                }
            }
            return num;
        }

        public int DetermineHabModulesRequired(ComponentImprovement habModuleComponent, Design design)
        {
            int designSize = design.QuickCalculateSize();
            bool designIsBase = false;
            if (design.Role == BuiltObjectRole.Base)
            {
                designIsBase = true;
            }
            return DetermineHabModulesRequired(habModuleComponent, designSize, designIsBase);
        }

        public int DetermineHabModulesRequired(ComponentImprovement habModuleComponent, int designSize, bool designIsBase)
        {
            int num = 0;
            int num2 = 0;
            if (habModuleComponent != null)
            {
                if (designIsBase)
                {
                    num = designSize / 2 / habModuleComponent.Value1 + 1;
                    num2 = designSize / 2 % habModuleComponent.Value1;
                }
                else
                {
                    num = designSize / habModuleComponent.Value1 + 1;
                    num2 = designSize % habModuleComponent.Value1;
                }
                if (num2 == 0)
                {
                    num--;
                }
            }
            return num;
        }

        private Design AddComponentsToDesign(Design design, ComponentList components, ResearchSystem research)
        {
            foreach (Component component3 in components)
            {
                if (component3 != null)
                {
                    design.Components.Add(component3);
                }
            }
            Component component = Component.EvaluateLatest(ComponentType.HabitationHabModule, 1000000.0);
            ComponentImprovement componentImprovement = new ComponentImprovement(component);
            Component component2 = Component.EvaluateLatest(ComponentType.HabitationLifeSupport, 1000000.0);
            ComponentImprovement componentImprovement2 = new ComponentImprovement(component2);
            if (research != null)
            {
                if (component != null)
                {
                    componentImprovement = research.ResolveImprovedComponentValues(component);
                }
                if (component2 != null)
                {
                    componentImprovement2 = research.ResolveImprovedComponentValues(component2);
                }
            }
            int num = DetermineHabModulesRequired(componentImprovement, design);
            int num2 = DetermineLifeSupportRequired(componentImprovement2, design);
            for (int i = 0; i < num; i++)
            {
                design.Components.Add(componentImprovement.ImprovedComponent);
            }
            for (int j = 0; j < num2; j++)
            {
                design.Components.Add(componentImprovement2.ImprovedComponent);
            }
            return design;
        }

        public ComponentList GetPlanetDestroyerComponents(double overpowerFactor)
        {
            return GetPlanetDestroyerComponents(overpowerFactor, null);
        }

        public ComponentList GetPlanetDestroyerComponents(double overpowerFactor, Empire empire)
        {
            ComponentList componentList = new ComponentList();
            int num = (int)(30.0 * overpowerFactor);
            int num2 = (int)(18.0 * overpowerFactor);
            int num3 = (int)(12.0 * overpowerFactor);
            int num4 = (int)(8.0 * overpowerFactor);
            int num5 = (int)(16.0 * overpowerFactor);
            int num6 = (int)(12.0 * overpowerFactor);
            int num7 = (int)(5.0 * overpowerFactor);
            int num8 = (int)(6.0 * overpowerFactor);
            int num9 = (int)(14.0 * overpowerFactor);
            if (empire != null && empire.Research != null)
            {
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.ComputerCommandCenter));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.ComputerCommandCenter));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.DamageControl));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.DamageControl));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.DamageControl));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.DamageControl));
                for (int i = 0; i < num8; i++)
                {
                    componentList.Add(empire.Research.GetLatestComponent(ComponentType.Reactor));
                }
                for (int j = 0; j < 60; j++)
                {
                    componentList.Add(empire.Research.GetLatestComponent(ComponentType.StorageFuel));
                }
                for (int k = 0; k < 60; k++)
                {
                    componentList.Add(empire.Research.GetLatestComponent(ComponentType.StorageCargo));
                }
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.StorageDockingBay));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.StorageDockingBay));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.StorageDockingBay));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.StorageDockingBay));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.ComputerCommerceCenter));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.HabitationMedicalCenter));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.HabitationRecreationCenter));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.SensorProximityArray));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.ComputerTargetting));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.ComputerCountermeasures));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.ComputerTargettingFleet));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.ComputerCountermeasuresFleet));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.SensorLongRange));
                for (int l = 0; l < num; l++)
                {
                    componentList.Add(empire.Research.GetLatestComponent(ComponentType.Armor));
                }
                for (int m = 0; m < num2; m++)
                {
                    componentList.Add(empire.Research.GetLatestComponent(ComponentCategoryType.Shields));
                }
                for (int n = 0; n < num9; n++)
                {
                    componentList.Add(empire.Research.GetLatestComponent(ComponentType.EnergyCollector));
                }
                for (int num10 = 0; num10 < num5; num10++)
                {
                    componentList.Add(empire.Research.GetLatestComponent(ComponentType.EngineMainThrust));
                }
                for (int num11 = 0; num11 < 1; num11++)
                {
                    componentList.Add(empire.Research.GetLatestComponent(ComponentType.EngineVectoring));
                }
                for (int num12 = 0; num12 < num3; num12++)
                {
                    componentList.Add(empire.Research.GetLatestComponent(ComponentCategoryType.WeaponBeam));
                }
                for (int num13 = 0; num13 < num6; num13++)
                {
                    componentList.Add(empire.Research.GetLatestComponent(ComponentCategoryType.WeaponPointDefense));
                }
                for (int num14 = 0; num14 < num4; num14++)
                {
                    componentList.Add(empire.Research.GetLatestComponent(ComponentCategoryType.WeaponTorpedo));
                }
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.WeaponTractorBeam));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.WeaponTractorBeam));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.WeaponTractorBeam));
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.WeaponTractorBeam));
                for (int num15 = 0; num15 < num7; num15++)
                {
                    componentList.Add(empire.Research.GetLatestComponent(ComponentType.FighterBay));
                }
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.WeaponAreaDestruction));
                ComponentDefinition highestTechByType = ComponentDefinition.GetHighestTechByType(ComponentType.WeaponSuperBeam, ComponentDefinitionsStatic);
                if (highestTechByType != null)
                {
                    componentList.Add(new Component(highestTechByType.ComponentID));
                }
                componentList.Add(empire.Research.GetLatestComponent(ComponentType.WeaponIonDefense));
                componentList.Add(empire.Research.GetLatestComponent(ComponentCategoryType.HyperDrive));
            }
            else
            {
                componentList.Add(Component.EvaluateLatest(ComponentType.ComputerCommandCenter, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.ComputerCommandCenter, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.DamageControl, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.DamageControl, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.DamageControl, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.DamageControl, 1000000.0));
                for (int num16 = 0; num16 < num8; num16++)
                {
                    componentList.Add(Component.EvaluateLatest(ComponentType.Reactor, 1000000.0));
                }
                for (int num17 = 0; num17 < 60; num17++)
                {
                    componentList.Add(Component.EvaluateLatest(ComponentType.StorageFuel, 1000000.0));
                }
                for (int num18 = 0; num18 < 60; num18++)
                {
                    componentList.Add(Component.EvaluateLatest(ComponentType.StorageCargo, 1000000.0));
                }
                componentList.Add(Component.EvaluateLatest(ComponentType.StorageDockingBay, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.StorageDockingBay, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.StorageDockingBay, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.StorageDockingBay, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.ComputerCommerceCenter, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.HabitationMedicalCenter, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.HabitationRecreationCenter, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.SensorProximityArray, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.ComputerTargetting, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.ComputerCountermeasures, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.ComputerTargettingFleet, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.ComputerCountermeasuresFleet, 1000000.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.SensorLongRange, 1000000.0));
                for (int num19 = 0; num19 < num; num19++)
                {
                    componentList.Add(Component.EvaluateLatest(ComponentType.Armor, 1000000.0));
                }
                for (int num20 = 0; num20 < num2; num20++)
                {
                    componentList.Add(Component.EvaluateLatest(ComponentCategoryType.Shields, 6.0));
                }
                for (int num21 = 0; num21 < num9; num21++)
                {
                    componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, 1000000.0));
                }
                for (int num22 = 0; num22 < num5; num22++)
                {
                    componentList.Add(Component.EvaluateLatest(ComponentType.EngineMainThrust, 6.0));
                }
                for (int num23 = 0; num23 < 1; num23++)
                {
                    componentList.Add(Component.EvaluateLatest(ComponentType.EngineVectoring, 6.0));
                }
                for (int num24 = 0; num24 < num3; num24++)
                {
                    componentList.Add(Component.EvaluateLatest(ComponentCategoryType.WeaponBeam, 6.0));
                }
                for (int num25 = 0; num25 < num6; num25++)
                {
                    componentList.Add(Component.EvaluateLatest(ComponentCategoryType.WeaponPointDefense, 6.0));
                }
                for (int num26 = 0; num26 < num4; num26++)
                {
                    componentList.Add(Component.EvaluateLatest(ComponentCategoryType.WeaponTorpedo, 6.0));
                }
                componentList.Add(Component.EvaluateLatest(ComponentType.WeaponTractorBeam, 6.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.WeaponTractorBeam, 6.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.WeaponTractorBeam, 6.0));
                componentList.Add(Component.EvaluateLatest(ComponentType.WeaponTractorBeam, 6.0));
                for (int num27 = 0; num27 < num7; num27++)
                {
                    componentList.Add(Component.EvaluateLatest(ComponentType.FighterBay, 6.0));
                }
                componentList.Add(Component.EvaluateLatest(ComponentType.WeaponAreaDestruction, 6.0));
                ComponentDefinition highestTechByType2 = ComponentDefinition.GetHighestTechByType(ComponentType.WeaponSuperBeam, ComponentDefinitionsStatic);
                if (highestTechByType2 != null)
                {
                    componentList.Add(new Component(highestTechByType2.ComponentID));
                }
                componentList.Add(Component.EvaluateLatest(ComponentType.WeaponIonDefense, 6.0));
                componentList.Add(Component.EvaluateLatest(ComponentCategoryType.HyperDrive, 6.0));
            }
            return componentList;
        }

        public DesignSpecification GetPlanetDestroyerDesignSpec()
        {
            DesignSpecification designSpecification = new DesignSpecification(BuiltObjectSubRole.CapitalShip, mobile: true);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommandCenter, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 30));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 30));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.EnergyCollector, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommerceCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.HabitationMedicalCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.HabitationRecreationCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorProximityArray, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorLongRange, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 30));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 40));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineMainThrust, 16));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.EngineVectoring, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 30));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponTorpedo, 20));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponAreaDestruction, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponTractorBeam, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.HyperDeny, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.WeaponSuperBeam, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageTroop, 8));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.HyperDrive, 1));
            return designSpecification;
        }

        public bool CanDestroyHabitat(BuiltObject builtObject, Habitat habitat)
        {
            switch (habitat.Category)
            {
                case HabitatCategoryType.Star:
                case HabitatCategoryType.GasCloud:
                    return false;
                case HabitatCategoryType.Planet:
                case HabitatCategoryType.Moon:
                case HabitatCategoryType.Asteroid:
                    if (builtObject.IsPlanetDestroyer && habitat.Diameter <= 400)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        public Design DesignPirateBase(Design design)
        {
            return DesignPirateBase(design, 0.0);
        }

        public Design DesignPirateBase(Design design, double techLevel)
        {
            Design design2 = design.Clone();
            Component component;
            Component component2;
            Component component3;
            Component component4;
            Component component5;
            Component component6;
            Component component7;
            Component component8;
            Component component9;
            Component component10;
            Component component11;
            Component component12;
            Component component13;
            Component component14;
            Component component15;
            Component component16;
            if (techLevel > 0.0)
            {
                component = Component.EvaluateLatest(ComponentCategoryType.WeaponBeam, techLevel);
                component2 = Component.EvaluateLatest(ComponentCategoryType.WeaponTorpedo, techLevel);
                component3 = Component.EvaluateLatest(ComponentCategoryType.Armor, techLevel);
                component4 = Component.EvaluateLatest(ComponentCategoryType.Shields, techLevel);
                component5 = Component.EvaluateLatest(ComponentType.ConstructionBuild, techLevel);
                component6 = Component.EvaluateLatest(ComponentType.ManufacturerEnergyPlant, techLevel);
                component7 = Component.EvaluateLatest(ComponentType.ManufacturerWeaponsPlant, techLevel);
                component8 = Component.EvaluateLatest(ComponentType.ManufacturerHighTechPlant, techLevel);
                component9 = Component.EvaluateLatest(ComponentType.DamageControl, techLevel);
                component10 = Component.EvaluateLatest(ComponentType.HabitationLifeSupport, techLevel);
                component11 = Component.EvaluateLatest(ComponentType.HabitationHabModule, techLevel);
                component12 = Component.EvaluateLatest(ComponentType.FighterBay, techLevel);
                component13 = Component.EvaluateLatest(ComponentType.WeaponPointDefense, techLevel);
                component14 = Component.EvaluateLatest(ComponentType.WeaponIonDefense, techLevel);
                component15 = Component.EvaluateLatest(ComponentType.WeaponTractorBeam, techLevel);
                component16 = Component.EvaluateLatest(ComponentType.AssaultPod, techLevel);
            }
            else
            {
                component = design.Empire.Research.EvaluateDesiredComponent(ComponentCategoryType.WeaponBeam, ShipDesignFocus.Balanced);
                component2 = design.Empire.Research.EvaluateDesiredComponent(ComponentCategoryType.WeaponTorpedo, ShipDesignFocus.Balanced);
                component3 = design.Empire.Research.EvaluateDesiredComponent(ComponentCategoryType.Armor, ShipDesignFocus.Balanced);
                component4 = design.Empire.Research.EvaluateDesiredComponent(ComponentCategoryType.Shields, ShipDesignFocus.Balanced);
                component5 = design.Empire.Research.EvaluateDesiredComponent(ComponentType.ConstructionBuild, ShipDesignFocus.Balanced);
                component6 = design.Empire.Research.EvaluateDesiredComponent(ComponentType.ManufacturerEnergyPlant, ShipDesignFocus.Balanced);
                component7 = design.Empire.Research.EvaluateDesiredComponent(ComponentType.ManufacturerWeaponsPlant, ShipDesignFocus.Balanced);
                component8 = design.Empire.Research.EvaluateDesiredComponent(ComponentType.ManufacturerHighTechPlant, ShipDesignFocus.Balanced);
                component9 = design.Empire.Research.EvaluateDesiredComponent(ComponentType.DamageControl, ShipDesignFocus.Balanced);
                component10 = design.Empire.Research.EvaluateDesiredComponent(ComponentType.HabitationLifeSupport, ShipDesignFocus.Balanced);
                component11 = design.Empire.Research.EvaluateDesiredComponent(ComponentType.HabitationHabModule, ShipDesignFocus.Balanced);
                component12 = design.Empire.Research.EvaluateDesiredComponent(ComponentType.FighterBay, ShipDesignFocus.Balanced);
                component13 = design.Empire.Research.EvaluateDesiredComponent(ComponentType.WeaponPointDefense, ShipDesignFocus.Balanced);
                component14 = design.Empire.Research.EvaluateDesiredComponent(ComponentType.WeaponIonDefense, ShipDesignFocus.Balanced);
                component15 = design.Empire.Research.EvaluateDesiredComponent(ComponentType.WeaponTractorBeam, ShipDesignFocus.Balanced);
                component16 = design.Empire.Research.EvaluateDesiredComponent(ComponentType.AssaultPod, ShipDesignFocus.Balanced);
            }
            if (component != null)
            {
                design2.Components.Add(component);
                design2.Components.Add(component);
                design2.Components.Add(component);
                design2.Components.Add(component);
            }
            if (component2 != null)
            {
                design2.Components.Add(component2);
                design2.Components.Add(component2);
            }
            if (component4 != null)
            {
                design2.Components.Add(component4);
                design2.Components.Add(component4);
                design2.Components.Add(component4);
                design2.Components.Add(component4);
            }
            if (component3 != null)
            {
                design2.Components.Add(component3);
                design2.Components.Add(component3);
                design2.Components.Add(component3);
                design2.Components.Add(component3);
            }
            if (component12 != null)
            {
                design2.Components.Add(component12);
                design2.Components.Add(component12);
            }
            if (component13 != null)
            {
                design2.Components.Add(component13);
                design2.Components.Add(component13);
                design2.Components.Add(component13);
                design2.Components.Add(component13);
            }
            if (component14 != null)
            {
                design2.Components.Add(component14);
            }
            if (component15 != null)
            {
                design2.Components.Add(component15);
                design2.Components.Add(component15);
            }
            if (component16 != null)
            {
                design2.Components.Add(component16);
                design2.Components.Add(component16);
            }
            if (component5 != null)
            {
                design2.Components.Add(component5);
            }
            if (component6 != null)
            {
                design2.Components.Add(component6);
            }
            if (component7 != null)
            {
                design2.Components.Add(component7);
            }
            if (component8 != null)
            {
                design2.Components.Add(component8);
            }
            if (component9 != null)
            {
                design2.Components.Add(component9);
            }
            if (component10 != null)
            {
                design2.Components.Add(component10);
                design2.Components.Add(component10);
            }
            if (component11 != null)
            {
                design2.Components.Add(component11);
                design2.Components.Add(component11);
            }
            design2.SubRole = BuiltObjectSubRole.GenericBase;
            design2.Empire = null;
            design2.Stance = BuiltObjectStance.AttackEnemies;
            design2.Name += " XT";
            design2.BuildCount = 0;
            design2.DateCreated = CurrentStarDate;
            design2.ReDefine();
            return design2;
        }

        public BuiltObject GeneratePirateShip(Empire pirateEmpire, BuiltObjectSubRole subRole, Habitat habitat)
        {
            Design design = null;
            for (int i = 0; i < pirateEmpire.Designs.Count; i++)
            {
                Design design2 = pirateEmpire.Designs[i];
                if (design2.SubRole == subRole)
                {
                    design = design2;
                    break;
                }
            }
            if (design != null)
            {
                design.BuildCount++;
                string name = SelectRandomUniqueMilitaryShipName();
                BuiltObject builtObject = new BuiltObject(design, name, this, fullyBuilt: true);
                builtObject.Empire = pirateEmpire;
                builtObject.Heading = SelectRandomHeading();
                builtObject.TargetHeading = builtObject.Heading;
                builtObject.ReDefine();
                builtObject.CurrentFuel = builtObject.FuelCapacity;
                builtObject.CurrentShields = builtObject.ShieldsCapacity;
                pirateEmpire.AddBuiltObjectToGalaxy(builtObject, habitat, offsetLocationFromParent: true, isStateOwned: true);
                return builtObject;
            }
            return null;
        }

        private void GenerateNewPirateShips()
        {
        }

        private Habitat RelocatePirateBase(Resource fuelType, Empire pirateFaction, Habitat existingPirateBaseHabitat)
        {
            double num = 0.0;
            double num2 = 0.0;
            bool flag = false;
            Habitat habitat = null;
            int num3 = 0;
            double num4 = 500000.0;
            while (!flag && num3 < 50)
            {
                double num5 = Rnd.NextDouble() * num4;
                double num6 = Rnd.NextDouble() * Math.PI * 2.0;
                num = existingPirateBaseHabitat.Xpos + Math.Cos(num6) * num5;
                num2 = existingPirateBaseHabitat.Ypos + Math.Sin(num6) * num5;
                habitat = FindNearestHabitatWithResource(num, num2, fuelType.ResourceID, existingPirateBaseHabitat, null);
                if (habitat != null && !PlayerEmpire.IsObjectVisibleToThisEmpire(habitat, includeLongRangeScanners: true, includeShipsOutsideSystems: false))
                {
                    Empire empire = FindNearestPirateFaction(num, num2, pirateFaction, includeSuperPirates: true);
                    if (empire != null)
                    {
                        double num7 = CalculateDistance(habitat.Xpos, habitat.Ypos, empire.PirateEmpireBaseHabitat.Xpos, empire.PirateEmpireBaseHabitat.Ypos);
                        if (num7 > (double)(MaxSolarSystemSize * 2))
                        {
                            BuiltObject builtObject = FindNearestBuiltObject((int)habitat.Xpos, (int)habitat.Ypos, BuiltObjectRole.Undefined, includeIndependentBuiltObjects: false);
                            double num8 = double.MaxValue;
                            if (builtObject != null)
                            {
                                num8 = CalculateDistance(habitat.Xpos, habitat.Ypos, builtObject.Xpos, builtObject.Ypos);
                            }
                            if (num8 > (double)(MaxSolarSystemSize * 2))
                            {
                                Habitat habitat2 = FindNearestColony(habitat.Xpos, habitat.Ypos, null, 0, includeIndependentColonies: false);
                                double num9 = double.MaxValue;
                                if (habitat2 != null)
                                {
                                    num9 = CalculateDistance(habitat.Xpos, habitat.Ypos, habitat2.Xpos, habitat2.Ypos);
                                }
                                if (num9 > (double)(MaxSolarSystemSize * 2))
                                {
                                    flag = true;
                                }
                            }
                        }
                    }
                }
                num3++;
            }
            return habitat;
        }

        public Empire FindNearestPirateFactionBaseUnknown(Empire empire, double x, double y, Empire pirateFactionToExclude)
        {
            double num = double.MaxValue;
            Empire result = null;
            EmpireList empireList = new EmpireList();
            for (int i = 0; i < empire.KnownPirateBases.Count; i++)
            {
                BuiltObject builtObject = empire.KnownPirateBases[i];
                if (!empireList.Contains(builtObject.Empire))
                {
                    empireList.Add(builtObject.Empire);
                }
            }
            for (int j = 0; j < PirateEmpires.Count; j++)
            {
                Empire empire2 = PirateEmpires[j];
                if (empire2 == null || empire2.PirateEmpireBaseHabitat == null || empire2.BuiltObjects == null || empireList.Contains(empire2) || (pirateFactionToExclude != null && empire2 == pirateFactionToExclude))
                {
                    continue;
                }
                double num2 = CalculateDistanceSquared(x, y, empire2.PirateEmpireBaseHabitat.Xpos, empire2.PirateEmpireBaseHabitat.Ypos);
                if (!(num2 < num))
                {
                    continue;
                }
                bool flag = false;
                for (int k = 0; k < empire2.BuiltObjects.Count; k++)
                {
                    BuiltObject builtObject2 = empire2.BuiltObjects[k];
                    if (builtObject2 != null && (builtObject2.SubRole == BuiltObjectSubRole.GenericBase || builtObject2.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject2.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject2.SubRole == BuiltObjectSubRole.LargeSpacePort))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    result = empire2;
                    num = num2;
                }
            }
            return result;
        }

        public Empire FindNearestPirateFaction(double x, double y, Empire pirateFactionToExclude, bool includeSuperPirates)
        {
            double num = double.MaxValue;
            Empire result = null;
            for (int i = 0; i < PirateEmpires.Count; i++)
            {
                Empire empire = PirateEmpires[i];
                if (empire == null || empire.PirateEmpireBaseHabitat == null || empire.BuiltObjects == null || !empire.Active || (pirateFactionToExclude != null && empire == pirateFactionToExclude) || (!includeSuperPirates && empire.PirateEmpireSuperPirates))
                {
                    continue;
                }
                double num2 = CalculateDistanceSquared(x, y, empire.PirateEmpireBaseHabitat.Xpos, empire.PirateEmpireBaseHabitat.Ypos);
                if (!(num2 < num))
                {
                    continue;
                }
                bool flag = false;
                for (int j = 0; j < empire.BuiltObjects.Count; j++)
                {
                    BuiltObject builtObject = empire.BuiltObjects[j];
                    if (builtObject != null && (builtObject.SubRole == BuiltObjectSubRole.GenericBase || builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    result = empire;
                    num = num2;
                }
            }
            return result;
        }

        public void CheckMergePirateFactions()
        {
            EmpireList empireList = new EmpireList();
            EmpireList empireList2 = new EmpireList();
            for (int i = 0; i < PirateEmpires.Count; i++)
            {
                Empire empire = PirateEmpires[i];
                if (empire != null && empire.Active)
                {
                    if (CheckPirateFactionShouldBeMerged(empire))
                    {
                        empireList.Add(empire);
                    }
                    else if (CheckPirateFactionCanAcceptMerge(empire))
                    {
                        empireList2.Add(empire);
                    }
                }
            }
            if (empireList.Count <= 0 || empireList2.Count <= 0)
            {
                return;
            }
            for (int j = 0; j < empireList.Count; j++)
            {
                Empire empire2 = empireList[j];
                if (empire2 != null && empire2 != PlayerEmpire && empire2.PirateEmpireBaseHabitat != null)
                {
                    double nearestDistanceSquared = double.MaxValue;
                    Empire empire3 = FindNearestPirateFactionKnownToFaction(empire2.PirateEmpireBaseHabitat.Xpos, empire2.PirateEmpireBaseHabitat.Ypos, empire2, empireList2, out nearestDistanceSquared);
                    if (empire3 != null && nearestDistanceSquared < 64000000000000.0)
                    {
                        Habitat habitat = DetermineHabitatSystemStar(empire2.PirateEmpireBaseHabitat);
                        string message = string.Format(TextResolver.GetText("Weak Pirate Faction Joins"), empire2.Name, habitat.Name);
                        EliminatePirateFaction(empire2, empire3);
                        string text = TextResolver.GetText("Pirate Faction Joins Your Empire");
                        empire3.SendEventMessageToEmpire(EventMessageType.PirateFactionJoinsYou, text, message, empire2, habitat);
                    }
                }
            }
        }

        public Empire FindNearestPirateFactionKnownToFaction(double x, double y, Empire pirateFaction, EmpireList pirateFactionsToCheck, out double nearestDistanceSquared)
        {
            Empire empire = null;
            nearestDistanceSquared = double.MaxValue;
            if (pirateFactionsToCheck != null)
            {
                for (int i = 0; i < pirateFactionsToCheck.Count; i++)
                {
                    Empire empire2 = pirateFactionsToCheck[i];
                    if (empire2 == null || empire2.PirateEmpireBaseHabitat == null)
                    {
                        continue;
                    }
                    PirateRelation pirateRelation = pirateFaction.ObtainPirateRelation(empire2);
                    if (pirateRelation.Type != 0)
                    {
                        double num = CalculateDistanceSquared(x, y, empire2.PirateEmpireBaseHabitat.Xpos, empire2.PirateEmpireBaseHabitat.Ypos);
                        if (empire == null || num < nearestDistanceSquared)
                        {
                            empire = empire2;
                            nearestDistanceSquared = num;
                        }
                    }
                }
            }
            return empire;
        }

        public bool CheckPirateFactionCanAcceptMerge(Empire pirateFaction)
        {
            if (pirateFaction != null && pirateFaction.BuiltObjects != null)
            {
                int num = pirateFaction.BuiltObjects.TotalMobileMilitaryFirepower();
                int num2 = pirateFaction.BuiltObjects.CountSpaceports();
                double num3 = pirateFaction.CalculateAnnualCashflow();
                if (num > 200 && num2 >= 1 && pirateFaction.StateMoney > 0.0 && num3 > 0.0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckPirateFactionShouldBeMerged(Empire pirateFaction)
        {
            if (pirateFaction != null && pirateFaction.BuiltObjects != null)
            {
                int num = pirateFaction.BuiltObjects.TotalMobileMilitaryFirepower();
                int num2 = pirateFaction.BuiltObjects.CountSpaceports();
                double num3 = pirateFaction.CalculateAnnualCashflow();
                if (num < 200 && num2 <= 0 && pirateFaction.StateMoney < 0.0 && num3 < 0.0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckPirateEmpireTerminated(Empire pirateFaction)
        {
            if (pirateFaction != null)
            {
                BuiltObject builtObject = IdentifyPirateBase(pirateFaction);
                if (builtObject == null && (pirateFaction.Colonies == null || pirateFaction.Colonies.Count <= 0) && pirateFaction.BuiltObjects.GetBuiltObjectsBySubRole(BuiltObjectSubRole.ConstructionShip).Count <= 0 && pirateFaction.BuiltObjects.GetBuiltObjectsBySubRole(BuiltObjectSubRole.ResupplyShip).Count <= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void EliminatePirateFaction(Empire pirateFaction)
        {
            Empire conqueror = FindNearestPirateFaction(pirateFaction.PirateEmpireBaseHabitat.Xpos, pirateFaction.PirateEmpireBaseHabitat.Ypos, pirateFaction, includeSuperPirates: true);
            EliminatePirateFaction(pirateFaction, conqueror);
        }

        public void EliminatePirateFaction(Empire pirateFaction, Empire conqueror)
        {
            if (pirateFaction == PlayerEmpire)
            {
                string description = TextResolver.GetText("Your pirate empire has been eliminated") + "!";
                OnGameEnd(new GameEndEventArgs(null, GameEndOutcome.Defeat, description, 0));
            }
            double num = 0.0;
            double num2 = 0.0;
            if (conqueror != null)
            {
                num = conqueror.CalculateAccurateAnnualCashflow();
                num2 = conqueror.GetPrivateAnnualCashflow();
            }
            bool flag = true;
            if (conqueror != null && conqueror.PirateEmpireBaseHabitat == null)
            {
                flag = false;
            }
            double num3 = 0.0;
            double num4 = 0.0;
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(pirateFaction.BuiltObjects);
            builtObjectList.AddRange(pirateFaction.PrivateBuiltObjects);
            foreach (BuiltObject item in builtObjectList)
            {
                bool flag2 = true;
                if (DetermineBuiltObjectIsState(item.SubRole) || flag)
                {
                    if (num3 + (double)item.AnnualSupportCost > num)
                    {
                        flag2 = false;
                    }
                    else
                    {
                        num3 += (double)item.AnnualSupportCost;
                    }
                }
                else if (num4 + (double)item.AnnualSupportCost > num2)
                {
                    flag2 = false;
                }
                else
                {
                    num4 += (double)item.AnnualSupportCost;
                }
                if (!flag2)
                {
                    item.CompleteTeardown(this);
                }
                else if (conqueror != null)
                {
                    if (!flag)
                    {
                        if (item.Role == BuiltObjectRole.Military)
                        {
                            item.Stance = BuiltObjectStance.AttackEnemies;
                            item.FleeWhen = BuiltObjectFleeWhen.Shields20;
                            item.Design.Stance = BuiltObjectStance.AttackEnemies;
                            item.Design.FleeWhen = BuiltObjectFleeWhen.Shields20;
                        }
                        else
                        {
                            item.Stance = BuiltObjectStance.AttackIfAttacked;
                            item.FleeWhen = BuiltObjectFleeWhen.Shields20;
                            item.Design.Stance = BuiltObjectStance.AttackIfAttacked;
                            item.Design.FleeWhen = BuiltObjectFleeWhen.Shields20;
                        }
                    }
                    switch (item.SubRole)
                    {
                        case BuiltObjectSubRole.SmallFreighter:
                        case BuiltObjectSubRole.MediumFreighter:
                        case BuiltObjectSubRole.LargeFreighter:
                            item.ClearPreviousMissionRequirements();
                            IndependentEmpire.TakeOwnershipOfBuiltObject(item, IndependentEmpire);
                            break;
                        case BuiltObjectSubRole.Escort:
                        case BuiltObjectSubRole.Frigate:
                        case BuiltObjectSubRole.Destroyer:
                        case BuiltObjectSubRole.Cruiser:
                        case BuiltObjectSubRole.CapitalShip:
                        case BuiltObjectSubRole.TroopTransport:
                        case BuiltObjectSubRole.Carrier:
                        case BuiltObjectSubRole.ResupplyShip:
                            if (conqueror != null)
                            {
                                item.ClearPreviousMissionRequirements();
                                string description3 = string.Format(TextResolver.GetText("Pirate Ship Joins Us"), item.Name, item.Empire.Name);
                                conqueror.TakeOwnershipOfBuiltObject(item, conqueror, setDesignAsObsolete: true);
                                conqueror.SendMessageToEmpire(conqueror, EmpireMessageType.Informational, item, description3);
                            }
                            else
                            {
                                item.CompleteTeardown(this);
                            }
                            break;
                        case BuiltObjectSubRole.ConstructionShip:
                        case BuiltObjectSubRole.GasMiningShip:
                        case BuiltObjectSubRole.MiningShip:
                        case BuiltObjectSubRole.GasMiningStation:
                        case BuiltObjectSubRole.MiningStation:
                        case BuiltObjectSubRole.ResortBase:
                        case BuiltObjectSubRole.EnergyResearchStation:
                        case BuiltObjectSubRole.WeaponsResearchStation:
                        case BuiltObjectSubRole.HighTechResearchStation:
                            IndependentEmpire.TakeOwnershipOfBuiltObject(item, null);
                            break;
                        case BuiltObjectSubRole.SmallSpacePort:
                        case BuiltObjectSubRole.MediumSpacePort:
                        case BuiltObjectSubRole.LargeSpacePort:
                        case BuiltObjectSubRole.DefensiveBase:
                            if (conqueror != null)
                            {
                                if (item.ParentHabitat != null && conqueror.ResourceMap != null)
                                {
                                    conqueror.ResourceMap.SetResourcesKnown(item.ParentHabitat, known: true);
                                }
                                string description2 = string.Format(TextResolver.GetText("Pirate Ship Joins Us"), item.Name, item.Empire.Name);
                                conqueror.TakeOwnershipOfBuiltObject(item, conqueror, setDesignAsObsolete: true);
                                conqueror.SendMessageToEmpire(conqueror, EmpireMessageType.Informational, item, description2);
                            }
                            else
                            {
                                item.CompleteTeardown(this);
                            }
                            break;
                        default:
                            item.CompleteTeardown(this);
                            break;
                    }
                }
                else
                {
                    item.CompleteTeardown(this);
                }
            }
            ClearPirateColonyFacilities(pirateFaction, conqueror);
            if (conqueror != null && conqueror.PirateEmpireBaseHabitat != null)
            {
                MergeGalaxyMap(pirateFaction, conqueror);
            }
            ClearFromKnownPirateBases(pirateFaction);
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                empire.KnownPirateEmpires.Remove(pirateFaction);
            }
            PirateEmpires.Remove(pirateFaction);
            if (conqueror != null)
            {
                conqueror.SendMessageToEmpire(pirateFaction, EmpireMessageType.EmpireDefeated, pirateFaction, TextResolver.GetText("You have been defeated!"));
            }
            else
            {
                pirateFaction.SendMessageToEmpire(pirateFaction, EmpireMessageType.EmpireDefeated, pirateFaction, TextResolver.GetText("You have been defeated!"));
            }
            pirateFaction.CompleteTeardown(conqueror, removeFromGalaxy: true, sendMessages: false);
        }

        public void ClearPirateColonyFacilities(Empire pirateFaction, Empire conqueror)
        {
            pirateFaction.PirateReviewColoniesToControl();
            if (conqueror == null || conqueror.PirateEmpireBaseHabitat == null)
            {
                for (int i = 0; i < pirateFaction.Colonies.Count; i++)
                {
                    Habitat habitat = pirateFaction.Colonies[i];
                    if (habitat == null || habitat.HasBeenDestroyed)
                    {
                        continue;
                    }
                    PirateColonyControl byFaction = habitat.GetPirateControl().GetByFaction(pirateFaction);
                    if (byFaction == null)
                    {
                        continue;
                    }
                    if (byFaction.HasFacilityControl)
                    {
                        PlanetaryFacilityList planetaryFacilityList = new PlanetaryFacilityList();
                        for (int j = 0; j < habitat.Facilities.Count; j++)
                        {
                            PlanetaryFacility planetaryFacility = habitat.Facilities[j];
                            if (planetaryFacility != null)
                            {
                                switch (planetaryFacility.Type)
                                {
                                    case PlanetaryFacilityType.PirateBase:
                                    case PlanetaryFacilityType.PirateFortress:
                                    case PlanetaryFacilityType.PirateCriminalNetwork:
                                        planetaryFacilityList.Add(planetaryFacility);
                                        break;
                                }
                            }
                        }
                        for (int k = 0; k < planetaryFacilityList.Count; k++)
                        {
                            habitat.Facilities.Remove(planetaryFacilityList[k]);
                            habitat.CheckRemoveFacilityTracking(planetaryFacilityList[k]);
                        }
                        byFaction.HasFacilityControl = false;
                    }
                    habitat.GetPirateControl().Remove(byFaction);
                }
                pirateFaction.Colonies.Clear();
                return;
            }
            for (int l = 0; l < pirateFaction.Colonies.Count; l++)
            {
                Habitat habitat2 = pirateFaction.Colonies[l];
                if (habitat2 == null || habitat2.HasBeenDestroyed)
                {
                    continue;
                }
                PirateColonyControl byFaction2 = habitat2.GetPirateControl().GetByFaction(pirateFaction);
                PirateColonyControl byFaction3 = habitat2.GetPirateControl().GetByFaction(conqueror);
                if (byFaction2 == null)
                {
                    continue;
                }
                if (byFaction3 == null)
                {
                    byFaction2.EmpireId = (byte)conqueror.EmpireId;
                }
                else
                {
                    byFaction3.ControlLevel = Math.Max(byFaction3.ControlLevel, byFaction2.ControlLevel);
                    if (byFaction2.HasFacilityControl)
                    {
                        byFaction2.HasFacilityControl = false;
                        byFaction3.HasFacilityControl = true;
                    }
                    habitat2.GetPirateControl().Remove(byFaction2);
                }
                if (!conqueror.Colonies.Contains(habitat2))
                {
                    conqueror.Colonies.Add(habitat2);
                }
            }
            pirateFaction.Colonies.Clear();
        }

        private void CheckForTerminatedPirateEmpires()
        {
            EmpireList empireList = new EmpireList();
            for (int i = 0; i < PirateEmpires.Count; i++)
            {
                Empire empire = PirateEmpires[i];
                if (!CheckPirateEmpireTerminated(empire))
                {
                    continue;
                }
                if (empire == PlayerEmpire)
                {
                    string description = TextResolver.GetText("Your pirate empire has been eliminated") + "!";
                    OnGameEnd(new GameEndEventArgs(null, GameEndOutcome.Defeat, description, 0));
                }
                Empire empire2 = FindNearestPirateFaction(empire.PirateEmpireBaseHabitat.Xpos, empire.PirateEmpireBaseHabitat.Ypos, empire, includeSuperPirates: true);
                BuiltObjectList builtObjectList = new BuiltObjectList();
                builtObjectList.AddRange(empire.BuiltObjects);
                builtObjectList.AddRange(empire.PrivateBuiltObjects);
                for (int j = 0; j < builtObjectList.Count; j++)
                {
                    BuiltObject builtObject = builtObjectList[j];
                    switch (builtObject.SubRole)
                    {
                        case BuiltObjectSubRole.SmallFreighter:
                        case BuiltObjectSubRole.MediumFreighter:
                        case BuiltObjectSubRole.LargeFreighter:
                            IndependentEmpire.TakeOwnershipOfBuiltObject(builtObject, IndependentEmpire);
                            break;
                        case BuiltObjectSubRole.Escort:
                        case BuiltObjectSubRole.Frigate:
                        case BuiltObjectSubRole.Destroyer:
                        case BuiltObjectSubRole.Cruiser:
                        case BuiltObjectSubRole.CapitalShip:
                        case BuiltObjectSubRole.TroopTransport:
                        case BuiltObjectSubRole.Carrier:
                        case BuiltObjectSubRole.ResupplyShip:
                            if (empire2 != null)
                            {
                                if (Rnd.Next(0, 2) == 1)
                                {
                                    string description2 = string.Format(TextResolver.GetText("Pirate Ship Joins Us"), builtObject.Name, builtObject.Empire.Name);
                                    empire2.TakeOwnershipOfBuiltObject(builtObject, empire2);
                                    empire2.SendMessageToEmpire(empire2, EmpireMessageType.Informational, builtObject, description2);
                                }
                                else
                                {
                                    builtObject.CompleteTeardown(this);
                                }
                            }
                            else
                            {
                                builtObject.CompleteTeardown(this);
                            }
                            break;
                        case BuiltObjectSubRole.ConstructionShip:
                        case BuiltObjectSubRole.GasMiningShip:
                        case BuiltObjectSubRole.MiningShip:
                        case BuiltObjectSubRole.GasMiningStation:
                        case BuiltObjectSubRole.MiningStation:
                        case BuiltObjectSubRole.ResortBase:
                        case BuiltObjectSubRole.EnergyResearchStation:
                        case BuiltObjectSubRole.WeaponsResearchStation:
                        case BuiltObjectSubRole.HighTechResearchStation:
                            IndependentEmpire.TakeOwnershipOfBuiltObject(builtObject, null);
                            break;
                        default:
                            builtObject.CompleteTeardown(this);
                            break;
                    }
                }
                ClearFromKnownPirateBases(empire);
                empireList.Add(empire);
            }
            foreach (Empire item in empireList)
            {
                for (int k = 0; k < Empires.Count; k++)
                {
                    Empire empire3 = Empires[k];
                    empire3.KnownPirateEmpires.Remove(item);
                }
                PirateEmpires.Remove(item);
                item.CompleteTeardown(null, removeFromGalaxy: true, sendMessages: false);
            }
        }

        public void ClearFromKnownPirateBases(BuiltObject pirateBase)
        {
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                BuiltObject builtObject = null;
                for (int j = 0; j < empire.KnownPirateBases.Count; j++)
                {
                    BuiltObject builtObject2 = empire.KnownPirateBases[j];
                    if (builtObject2 == pirateBase)
                    {
                        builtObject = builtObject2;
                        break;
                    }
                }
                if (builtObject != null)
                {
                    empire.KnownPirateBases.Remove(builtObject);
                }
            }
        }

        public void ClearFromKnownPirateBases(Empire pirateEmpire)
        {
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                BuiltObject builtObject = null;
                for (int j = 0; j < empire.KnownPirateBases.Count; j++)
                {
                    BuiltObject builtObject2 = empire.KnownPirateBases[j];
                    if (builtObject2.Empire == pirateEmpire)
                    {
                        builtObject = builtObject2;
                        break;
                    }
                }
                if (builtObject != null)
                {
                    empire.KnownPirateBases.Remove(builtObject);
                }
            }
        }

        private void ReviewPirateEmpireActivities()
        {
            long currentStarDate = CurrentStarDate;
            EmpireActivityList empireActivityList = new EmpireActivityList();
            for (int i = 0; i < PirateEmpires.Count; i++)
            {
                Empire empire = PirateEmpires[i];
                if (empire == null || empire.PirateEmpireSuperPirates)
                {
                    continue;
                }
                EmpireList empireList = new EmpireList();
                EmpireActivityList empireActivityList2 = new EmpireActivityList();
                new EmpireList();
                for (int j = 0; j < empire.PirateMissions.Count; j++)
                {
                    EmpireActivity empireActivity = empire.PirateMissions[j];
                    if (empireActivity == null || empireActivity.ExpiryDate > currentStarDate)
                    {
                        continue;
                    }
                    if (empireActivity.RequestingEmpire != null && !empireList.Contains(empireActivity.RequestingEmpire))
                    {
                        empireList.Add(empireActivity.RequestingEmpire);
                    }
                    switch (empireActivity.Type)
                    {
                        case EmpireActivityType.Attack:
                            if (empireActivity.Target != null && !empireActivity.Target.HasBeenDestroyed && empireActivity.Target.Empire != empireActivity.AssignedEmpire)
                            {
                                PirateRelation pirateRelation = empireActivity.RequestingEmpire.ObtainPirateRelation(empire);
                                pirateRelation.EvaluationPirateMissionsFail -= 15f;
                                if (empireActivity.AssignedEmpire != null)
                                {
                                    string description2 = string.Format(TextResolver.GetText("Pirate Attack Mission Failed Pirate"), empireActivity.RequestingEmpire.Name, empireActivity.Target.Name, empireActivity.Price.ToString("0"));
                                    empireActivity.AssignedEmpire.SendMessageToEmpire(empireActivity.AssignedEmpire, EmpireMessageType.PirateAttackMissionFailed, empireActivity.Target, description2);
                                    description2 = string.Format(TextResolver.GetText("Pirate Attack Mission Failed Other"), empireActivity.AssignedEmpire.Name, empireActivity.Target.Name, empireActivity.Price.ToString("0"));
                                    empireActivity.RequestingEmpire.SendMessageToEmpire(empireActivity.RequestingEmpire, EmpireMessageType.PirateAttackMissionFailed, empireActivity.Target, description2);
                                }
                            }
                            break;
                        case EmpireActivityType.Smuggle:
                            {
                                string empty = string.Empty;
                                if (empireActivity.Target != null)
                                {
                                    empire.SendMessageToEmpire(description: (empireActivity.RequestingEmpire == IndependentEmpire) ? ((empireActivity.ResourceId != byte.MaxValue) ? string.Format(TextResolver.GetText("Pirate Smuggle Mission Completed Pirate Independent"), empireActivity.Target.Name, new Resource(empireActivity.ResourceId).Name) : string.Format(TextResolver.GetText("Pirate Smuggle Mission Completed Pirate Independent All Resources"), empireActivity.Target.Name)) : ((empireActivity.ResourceId != byte.MaxValue) ? string.Format(TextResolver.GetText("Pirate Smuggle Mission Completed Pirate"), empireActivity.Target.Name, new Resource(empireActivity.ResourceId).Name, empireActivity.RequestingEmpire.Name) : string.Format(TextResolver.GetText("Pirate Smuggle Mission Completed Pirate All Resources"), empireActivity.Target.Name, empireActivity.RequestingEmpire.Name)), recipientEmpire: empire, messageType: EmpireMessageType.PirateSmugglingMissionCompleted, subject: empireActivity.Target);
                                }
                                if (!empireActivityList.Contains(empireActivity))
                                {
                                    empireActivityList.Add(empireActivity);
                                }
                                break;
                            }
                    }
                    if (empireActivity.Type == EmpireActivityType.Attack || empireActivity.Type == EmpireActivityType.Smuggle)
                    {
                        empireActivityList2.Add(empireActivity);
                    }
                }
                for (int k = 0; k < empireActivityList2.Count; k++)
                {
                    EmpireActivity empireActivity2 = empireActivityList2[k];
                    if (empireActivity2 != null)
                    {
                        if (empireActivity2.Type == EmpireActivityType.Smuggle && empireActivity2.RelatedOrder != null)
                        {
                            empireActivity2.RelatedOrder.ExpiryDate = CurrentStarDate;
                        }
                        empire.PirateMissions.Remove(empireActivity2);
                        if (empireActivity2.RequestingEmpire != null && empireActivity2.RequestingEmpire.PirateMissions != null)
                        {
                            empireActivity2.RequestingEmpire.PirateMissions.RemoveEquivalent(empireActivity2);
                        }
                        PirateMissions.RemoveEquivalent(empireActivity2);
                    }
                }
            }
            for (int l = 0; l < empireActivityList.Count; l++)
            {
                EmpireActivity empireActivity3 = empireActivityList[l];
                if (empireActivity3 != null && empireActivity3.Target != null && empireActivity3.RequestingEmpire != null && empireActivity3.RequestingEmpire != IndependentEmpire)
                {
                    string empty2 = string.Empty;
                    empty2 = ((empireActivity3.ResourceId != byte.MaxValue) ? string.Format(TextResolver.GetText("Pirate Smuggle Mission Completed Other"), empireActivity3.Target.Name, new Resource(empireActivity3.ResourceId).Name) : string.Format(TextResolver.GetText("Pirate Smuggle Mission Completed Other All Resources"), empireActivity3.Target.Name));
                    empireActivity3.RequestingEmpire.SendMessageToEmpire(empireActivity3.RequestingEmpire, EmpireMessageType.PirateSmugglingMissionCompleted, empireActivity3.Target, empty2);
                }
            }
        }

        public void CancelPirateMissionsForTarget(StellarObject target, EmpireActivityType type)
        {
            if (IndependentEmpire != null && IndependentEmpire.PirateMissions != null)
            {
                EmpireActivityList empireActivityList = IndependentEmpire.PirateMissions.ResolveByTypeAndTarget(type, target);
                for (int i = 0; i < empireActivityList.Count; i++)
                {
                    EmpireActivity empireActivity = empireActivityList[i];
                    if (empireActivity != null)
                    {
                        IndependentEmpire.PirateMissions.Remove(empireActivity);
                        if (empireActivity.Type == EmpireActivityType.Smuggle && empireActivity.RelatedOrder != null)
                        {
                            empireActivity.RelatedOrder.ExpiryDate = CurrentStarDate;
                        }
                    }
                }
            }
            for (int j = 0; j < Empires.Count; j++)
            {
                Empire empire = Empires[j];
                if (empire == null || empire.PirateMissions == null)
                {
                    continue;
                }
                EmpireActivityList empireActivityList2 = empire.PirateMissions.ResolveByTypeAndTarget(type, target);
                for (int k = 0; k < empireActivityList2.Count; k++)
                {
                    EmpireActivity empireActivity2 = empireActivityList2[k];
                    if (empireActivity2 != null)
                    {
                        empire.PirateMissions.Remove(empireActivity2);
                        if (empireActivity2.Type == EmpireActivityType.Smuggle && empireActivity2.RelatedOrder != null)
                        {
                            empireActivity2.RelatedOrder.ExpiryDate = CurrentStarDate;
                        }
                    }
                }
            }
            for (int l = 0; l < PirateEmpires.Count; l++)
            {
                Empire empire2 = PirateEmpires[l];
                if (empire2 == null || empire2.PirateMissions == null)
                {
                    continue;
                }
                EmpireActivityList empireActivityList3 = empire2.PirateMissions.ResolveByTypeAndTarget(type, target);
                for (int m = 0; m < empireActivityList3.Count; m++)
                {
                    EmpireActivity empireActivity3 = empireActivityList3[m];
                    if (empireActivity3 != null)
                    {
                        empire2.PirateMissions.Remove(empireActivity3);
                        if (empireActivity3.Type == EmpireActivityType.Smuggle && empireActivity3.RelatedOrder != null)
                        {
                            empireActivity3.RelatedOrder.ExpiryDate = CurrentStarDate;
                        }
                    }
                }
            }
            EmpireActivityList empireActivityList4 = PirateMissions.ResolveByTypeAndTarget(type, target);
            for (int n = 0; n < empireActivityList4.Count; n++)
            {
                EmpireActivity empireActivity4 = empireActivityList4[n];
                if (empireActivity4 != null)
                {
                    PirateMissions.Remove(empireActivity4);
                    if (empireActivity4.Type == EmpireActivityType.Smuggle && empireActivity4.RelatedOrder != null)
                    {
                        empireActivity4.RelatedOrder.ExpiryDate = CurrentStarDate;
                    }
                }
            }
        }

        public void CheckCancelIntelligenceMissionsWithTarget(StellarObject target)
        {
            if (target == null)
            {
                return;
            }
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire == null || empire.Characters == null)
                {
                    continue;
                }
                for (int j = 0; j < empire.Characters.Count; j++)
                {
                    Character character = empire.Characters[j];
                    if (character == null || character.Role != CharacterRole.IntelligenceAgent)
                    {
                        continue;
                    }
                    IntelligenceMission mission = character.Mission;
                    if (mission == null || mission.Target == null)
                    {
                        continue;
                    }
                    if (mission.Target is Habitat)
                    {
                        Habitat habitat = (Habitat)mission.Target;
                        if (habitat == target)
                        {
                            empire.CancelIntelligenceMission(mission);
                            character.Mission = null;
                        }
                    }
                    else if (mission.Target is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)mission.Target;
                        if (builtObject == target)
                        {
                            empire.CancelIntelligenceMission(mission);
                            character.Mission = null;
                        }
                    }
                }
            }
            for (int k = 0; k < PirateEmpires.Count; k++)
            {
                Empire empire2 = PirateEmpires[k];
                if (empire2 == null || empire2.Characters == null)
                {
                    continue;
                }
                for (int l = 0; l < empire2.Characters.Count; l++)
                {
                    Character character2 = empire2.Characters[l];
                    if (character2 == null || character2.Role != CharacterRole.IntelligenceAgent)
                    {
                        continue;
                    }
                    IntelligenceMission mission2 = character2.Mission;
                    if (mission2 == null || mission2.Target == null)
                    {
                        continue;
                    }
                    if (mission2.Target is Habitat)
                    {
                        Habitat habitat2 = (Habitat)mission2.Target;
                        if (habitat2 == target)
                        {
                            empire2.CancelIntelligenceMission(mission2);
                            character2.Mission = null;
                        }
                    }
                    else if (mission2.Target is BuiltObject)
                    {
                        BuiltObject builtObject2 = (BuiltObject)mission2.Target;
                        if (builtObject2 == target)
                        {
                            empire2.CancelIntelligenceMission(mission2);
                            character2.Mission = null;
                        }
                    }
                }
            }
        }

        public void SetupPirateAlliance(Empire empire, Empire pirateFaction, double monthlyFee, long startDate)
        {
        }

        public void CancelPirateAlliance(Empire pirateFaction, EmpireActivity alliance)
        {
        }

        public Race SelectRandomRacePreferHospitableHabitats(int intelligenceThreshhold, bool allowUnplayableRaces)
        {
            RaceList raceList = new RaceList();
            foreach (Race race in Races)
            {
                if ((allowUnplayableRaces || race.Playable) && race.IntelligenceLevel >= intelligenceThreshhold)
                {
                    int num = 1;
                    switch (race.NativeHabitatType)
                    {
                        case HabitatType.Continental:
                            num = 5;
                            break;
                        case HabitatType.MarshySwamp:
                            num = 4;
                            break;
                        case HabitatType.Desert:
                            num = 3;
                            break;
                        case HabitatType.Ocean:
                            num = 2;
                            break;
                        case HabitatType.Ice:
                            num = 1;
                            break;
                        case HabitatType.Volcanic:
                            num = 1;
                            break;
                        case HabitatType.BarrenRock:
                            num = 0;
                            break;
                    }
                    for (int i = 0; i < num; i++)
                    {
                        raceList.Add(race);
                    }
                }
            }
            Race result = null;
            if (raceList.Count > 0)
            {
                result = raceList[Rnd.Next(0, raceList.Count)];
            }
            return result;
        }

        public Race SelectRandomRace(int intelligenceThreshhold)
        {
            RaceList raceList = new RaceList();
            for (int i = 0; i < Races.Count; i++)
            {
                Race race = Races[i];
                if (race.IntelligenceLevel >= intelligenceThreshhold && race.Playable)
                {
                    raceList.Add(race);
                }
            }
            Race result = null;
            if (raceList.Count > 0)
            {
                result = raceList[Rnd.Next(0, raceList.Count)];
            }
            return result;
        }

        private Race SelectRandomPirateRace()
        {
            RaceList raceList = new RaceList();
            for (int i = 0; i < Races.Count; i++)
            {
                Race race = Races[i];
                if (race.CanBePirate)
                {
                    raceList.Add(race);
                }
            }
            Race result = null;
            if (raceList.Count > 0)
            {
                result = raceList[Rnd.Next(0, raceList.Count)];
            }
            return result;
        }

        private Race SelectRandomAggressiveRace(int aggressionThreshhold)
        {
            RaceList raceList = new RaceList();
            for (int i = 0; i < Races.Count; i++)
            {
                Race race = Races[i];
                if (race.AggressionLevel >= aggressionThreshhold && race.Playable)
                {
                    raceList.Add(race);
                }
            }
            Race result = null;
            if (raceList.Count > 0)
            {
                result = raceList[Rnd.Next(0, raceList.Count)];
            }
            return result;
        }

        public ComponentList GetSuperPirateBaseComponents(double overpowerFactor, double techLevel)
        {
            ComponentList componentList = new ComponentList();
            int num = (int)(30.0 * overpowerFactor);
            int num2 = (int)(18.0 * overpowerFactor);
            int num3 = (int)(18.0 * overpowerFactor);
            int num4 = (int)(12.0 * overpowerFactor);
            int num5 = (int)(10.0 * overpowerFactor);
            int num6 = (int)(5.0 * overpowerFactor);
            int num7 = (int)(6.0 * overpowerFactor);
            int num8 = (int)(4.0 * overpowerFactor);
            int num9 = (int)(4.0 * overpowerFactor);
            componentList.Add(Component.EvaluateLatest(ComponentType.ComputerCommandCenter, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.ComputerCommandCenter, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.DamageControl, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.DamageControl, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.DamageControl, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.DamageControl, techLevel));
            for (int i = 0; i < num7; i++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentType.Reactor, techLevel));
            }
            for (int j = 0; j < 20; j++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentType.StorageFuel, techLevel));
            }
            for (int k = 0; k < 30; k++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentType.StorageCargo, techLevel));
            }
            componentList.Add(Component.EvaluateLatest(ComponentType.ExtractorGasExtractor, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.StorageDockingBay, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.StorageDockingBay, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.StorageDockingBay, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.StorageDockingBay, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.StorageDockingBay, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.StorageDockingBay, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.ComputerCommerceCenter, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.HabitationMedicalCenter, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.HabitationRecreationCenter, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.SensorProximityArray, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.ComputerTargetting, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.ComputerCountermeasures, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.SensorLongRange, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.ConstructionBuild, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.ConstructionBuild, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.WeaponIonCannon, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.WeaponIonCannon, techLevel));
            for (int l = 0; l < num; l++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentType.Armor, techLevel));
            }
            for (int m = 0; m < num2; m++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentCategoryType.Shields, techLevel));
            }
            for (int n = 0; n < num3; n++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentCategoryType.WeaponBeam, techLevel));
            }
            for (int num10 = 0; num10 < num5; num10++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentCategoryType.WeaponPointDefense, techLevel));
            }
            for (int num11 = 0; num11 < num4; num11++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentCategoryType.WeaponTorpedo, techLevel));
            }
            for (int num12 = 0; num12 < num8; num12++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentType.WeaponTractorBeam, techLevel));
            }
            for (int num13 = 0; num13 < num9; num13++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentCategoryType.AssaultPod, techLevel));
            }
            for (int num14 = 0; num14 < num6; num14++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentType.FighterBay, techLevel));
            }
            componentList.Add(Component.EvaluateLatest(ComponentType.WeaponAreaDestruction, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.WeaponIonDefense, techLevel));
            return componentList;
        }

        public ComponentList GetSuperPirateDefensiveBaseComponents(double overpowerFactor, double techLevel)
        {
            ComponentList componentList = new ComponentList();
            int num = (int)(15.0 * overpowerFactor);
            int num2 = (int)(12.0 * overpowerFactor);
            int num3 = (int)(12.0 * overpowerFactor);
            int num4 = (int)(8.0 * overpowerFactor);
            int num5 = (int)(8.0 * overpowerFactor);
            int num6 = (int)(2.0 * overpowerFactor);
            int num7 = (int)(3.0 * overpowerFactor);
            int num8 = (int)(2.0 * overpowerFactor);
            int num9 = (int)(2.0 * overpowerFactor);
            componentList.Add(Component.EvaluateLatest(ComponentType.ComputerCommandCenter, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.DamageControl, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.DamageControl, techLevel));
            for (int i = 0; i < num7; i++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentType.Reactor, techLevel));
            }
            for (int j = 0; j < 10; j++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentType.StorageFuel, techLevel));
            }
            for (int k = 0; k < 15; k++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentType.StorageCargo, techLevel));
            }
            componentList.Add(Component.EvaluateLatest(ComponentType.ExtractorGasExtractor, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.StorageDockingBay, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.StorageDockingBay, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.StorageDockingBay, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.EnergyCollector, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.ComputerCommerceCenter, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.SensorProximityArray, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.ConstructionBuild, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.ComputerTargetting, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.ComputerCountermeasures, techLevel));
            for (int l = 0; l < num; l++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentType.Armor, techLevel));
            }
            for (int m = 0; m < num2; m++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentCategoryType.Shields, techLevel));
            }
            for (int n = 0; n < num3; n++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentCategoryType.WeaponBeam, techLevel));
            }
            for (int num10 = 0; num10 < num5; num10++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentCategoryType.WeaponPointDefense, techLevel));
            }
            for (int num11 = 0; num11 < num4; num11++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentCategoryType.WeaponTorpedo, techLevel));
            }
            for (int num12 = 0; num12 < num8; num12++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentType.WeaponTractorBeam, techLevel));
            }
            for (int num13 = 0; num13 < num9; num13++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentCategoryType.AssaultPod, techLevel));
            }
            for (int num14 = 0; num14 < num6; num14++)
            {
                componentList.Add(Component.EvaluateLatest(ComponentType.FighterBay, techLevel));
            }
            componentList.Add(Component.EvaluateLatest(ComponentType.WeaponIonCannon, techLevel));
            componentList.Add(Component.EvaluateLatest(ComponentType.WeaponIonDefense, techLevel));
            return componentList;
        }

        public Design GenerateSuperPirateBaseDesign(double techLevel)
        {
            return GenerateSuperPirateBaseDesign(1.0, techLevel);
        }

        public Design GenerateSuperPirateBaseDesign(double overpowerFactor, double techLevel)
        {
            ComponentList superPirateBaseComponents = GetSuperPirateBaseComponents(overpowerFactor, techLevel);
            string text = TextResolver.GetText("Phantom Pirate Base");
            Design design = new Design(text);
            design.Role = BuiltObjectRole.Base;
            design.SubRole = BuiltObjectSubRole.GenericBase;
            design = AddComponentsToDesign(design, superPirateBaseComponents, null);
            design.Stance = BuiltObjectStance.AttackEnemies;
            design.FleeWhen = BuiltObjectFleeWhen.Never;
            design.TacticsStrongerShips = BattleTactics.PointBlank;
            design.TacticsWeakerShips = BattleTactics.PointBlank;
            design.TacticsInvasion = InvasionTactics.DoNotInvade;
            design.Name = text;
            design.DateCreated = CurrentStarDate;
            design.Empire = null;
            design.PictureRef = ShipImageHelper.ResolveSuperPirateShipImageIndex(BuiltObjectSubRole.GenericBase);
            design.ReDefine();
            return design;
        }

        public Design GenerateSuperPirateDefensiveBaseDesign(double techLevel)
        {
            return GenerateSuperPirateDefensiveBaseDesign(1.0, techLevel);
        }

        public Design GenerateSuperPirateDefensiveBaseDesign(double overpowerFactor, double techLevel)
        {
            ComponentList superPirateDefensiveBaseComponents = GetSuperPirateDefensiveBaseComponents(overpowerFactor, techLevel);
            string text = TextResolver.GetText("Phantom Pirate Defensive Base");
            Design design = new Design(text);
            design.Role = BuiltObjectRole.Base;
            design.SubRole = BuiltObjectSubRole.DefensiveBase;
            design = AddComponentsToDesign(design, superPirateDefensiveBaseComponents, null);
            design.Stance = BuiltObjectStance.AttackEnemies;
            design.FleeWhen = BuiltObjectFleeWhen.Never;
            design.TacticsStrongerShips = BattleTactics.PointBlank;
            design.TacticsWeakerShips = BattleTactics.PointBlank;
            design.TacticsInvasion = InvasionTactics.DoNotInvade;
            design.Name = text;
            design.DateCreated = CurrentStarDate;
            design.Empire = null;
            design.PictureRef = ShipImageHelper.ResolveSuperPirateShipImageIndex(BuiltObjectSubRole.DefensiveBase);
            design.ReDefine();
            return design;
        }

        public Empire GenerateSuperPirateFaction(Habitat habitat, string name, Race race, double techLevel)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = GeneratePirateEmpireName(habitat, PiratePlayStyle.Pirate);
            }
            if (race == null)
            {
                race = SelectRandomPirateRace();
                if (race == null)
                {
                    race = SelectRandomAggressiveRace(115);
                }
                if (race == null)
                {
                    race = SelectRandomRace(0);
                }
            }
            SelectRelativeHabitatSurfacePoint(habitat, out var x, out var y);
            Empire empire = GeneratePirateEmpire(habitat, (int)x, (int)y, race, -1, techLevel, PiratePlayStyle.Pirate, isPlayerEmpire: false, isSuperPirates: true);
            empire.PirateEmpireSuperPirates = true;
            Design design = empire.GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Escort), techLevel);
            Design design2 = empire.GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Frigate), techLevel);
            Design design3 = empire.GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Destroyer), techLevel);
            Design design4 = empire.GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Cruiser), techLevel);
            Design design5 = empire.GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.CapitalShip), techLevel);
            Design design6 = empire.GenerateDesignFromSpec(DesignSpecifications.GetBySubRole(BuiltObjectSubRole.Carrier), techLevel);
            Design design7 = GenerateSuperPirateDefensiveBaseDesign(techLevel);
            Design design8 = GenerateSuperPirateBaseDesign(techLevel);
            design = UpgradeMilitaryShipDesignMoreEngines(design);
            design2 = UpgradeMilitaryShipDesignMoreEngines(design2);
            design3 = UpgradeMilitaryShipDesignMoreEngines(design3);
            design4 = UpgradeMilitaryShipDesignMoreEngines(design4);
            design5 = UpgradeMilitaryShipDesignMoreEngines(design5);
            design6 = UpgradeMilitaryShipDesignMoreEngines(design6);
            design = UpgradeMilitaryShipDesignMoreWeapons(design);
            design2 = UpgradeMilitaryShipDesignMoreWeapons(design2);
            design3 = UpgradeMilitaryShipDesignMoreWeapons(design3);
            design4 = UpgradeMilitaryShipDesignMoreWeapons(design4);
            design5 = UpgradeMilitaryShipDesignMoreWeapons(design5);
            design6 = UpgradeMilitaryShipDesignMoreWeapons(design6);
            design.PictureRef = ShipImageHelper.ResolveSuperPirateShipImageIndex(BuiltObjectSubRole.Escort);
            design2.PictureRef = ShipImageHelper.ResolveSuperPirateShipImageIndex(BuiltObjectSubRole.Frigate);
            design3.PictureRef = ShipImageHelper.ResolveSuperPirateShipImageIndex(BuiltObjectSubRole.Destroyer);
            design4.PictureRef = ShipImageHelper.ResolveSuperPirateShipImageIndex(BuiltObjectSubRole.Cruiser);
            design5.PictureRef = ShipImageHelper.ResolveSuperPirateShipImageIndex(BuiltObjectSubRole.CapitalShip);
            design6.PictureRef = ShipImageHelper.ResolveSuperPirateShipImageIndex(BuiltObjectSubRole.Carrier);
            empire.Designs.Add(design);
            empire.Designs.Add(design2);
            empire.Designs.Add(design3);
            empire.Designs.Add(design4);
            empire.Designs.Add(design5);
            empire.Designs.Add(design6);
            empire.Designs.Add(design7);
            empire.Designs.Add(design8);
            string name2 = GeneratePirateBaseName(habitat);
            design8.BuildCount++;
            BuiltObject builtObject = new BuiltObject(design8, name2, this, fullyBuilt: true);
            builtObject.Empire = empire;
            builtObject.Heading = SelectRandomHeading();
            builtObject.TargetHeading = builtObject.Heading;
            builtObject.ReDefine();
            builtObject.CurrentFuel = builtObject.FuelCapacity;
            builtObject.CurrentShields = builtObject.ShieldsCapacity;
            empire.AddBuiltObjectToGalaxy(builtObject, habitat, offsetLocationFromParent: false, isStateOwned: true, 0, 0, sendMessage: false);
            for (int i = 0; i < 3; i++)
            {
                string name3 = GenerateBuiltObjectName(design7, habitat);
                design7.BuildCount++;
                BuiltObject builtObject2 = new BuiltObject(design7, name3, this, fullyBuilt: true);
                builtObject2.Empire = empire;
                builtObject2.Heading = SelectRandomHeading();
                builtObject2.TargetHeading = builtObject2.Heading;
                builtObject2.ReDefine();
                builtObject2.CurrentFuel = builtObject2.FuelCapacity;
                builtObject2.CurrentShields = builtObject2.ShieldsCapacity;
                empire.DetermineOrbitalBaseLocation(habitat, out x, out y);
                empire.AddBuiltObjectToGalaxy(builtObject2, habitat, offsetLocationFromParent: false, isStateOwned: true, (int)x, (int)y, sendMessage: false);
            }
            int num = Rnd.Next(20, 30);
            for (int j = 0; j < num; j++)
            {
                Design design9 = null;
                switch (Rnd.Next(0, 25))
                {
                    case 0:
                    case 1:
                    case 2:
                        design9 = design;
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        design9 = design2;
                        break;
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        design9 = design3;
                        break;
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                        design9 = design4;
                        break;
                    case 21:
                    case 22:
                        design9 = design5;
                        break;
                    case 23:
                    case 24:
                        design9 = design6;
                        break;
                }
                design9.BuildCount++;
                string name4 = SelectRandomUniqueMilitaryShipName();
                BuiltObject builtObject3 = new BuiltObject(design9, name4, this, fullyBuilt: true);
                builtObject3.Empire = empire;
                builtObject3.Heading = SelectRandomHeading();
                builtObject3.TargetHeading = builtObject3.Heading;
                builtObject3.ReDefine();
                builtObject3.CurrentFuel = builtObject3.FuelCapacity;
                builtObject3.CurrentShields = builtObject3.ShieldsCapacity;
                empire.AddBuiltObjectToGalaxy(builtObject3, habitat, offsetLocationFromParent: true, isStateOwned: true, sendMessage: false);
            }
            return empire;
        }

        public Design UpgradeMilitaryShipDesignMoreWeapons(Design design)
        {
            if (design != null)
            {
                int num = design.Components.CountByCategory(ComponentCategoryType.WeaponBeam);
                int num2 = design.Components.CountByCategory(ComponentCategoryType.WeaponTorpedo);
                int num3 = design.Components.CountByCategory(ComponentCategoryType.WeaponPointDefense);
                int num4 = 3;
                int num5 = 0;
                int num6 = 0;
                switch (design.SubRole)
                {
                    case BuiltObjectSubRole.Escort:
                        num4 = 3;
                        num5 = 0;
                        num6 = 0;
                        break;
                    case BuiltObjectSubRole.Frigate:
                        num4 = 5;
                        num5 = 0;
                        num6 = 1;
                        break;
                    case BuiltObjectSubRole.Destroyer:
                        num4 = 6;
                        num5 = 1;
                        num6 = 2;
                        break;
                    case BuiltObjectSubRole.Cruiser:
                        num4 = 10;
                        num5 = 4;
                        num6 = 4;
                        break;
                    case BuiltObjectSubRole.CapitalShip:
                        num4 = 16;
                        num5 = 8;
                        num6 = 8;
                        break;
                    case BuiltObjectSubRole.Carrier:
                        num4 = 4;
                        num5 = 0;
                        num6 = 4;
                        break;
                    case BuiltObjectSubRole.TroopTransport:
                        num4 = 2;
                        num5 = 0;
                        num6 = 1;
                        break;
                }
                if (num < num4)
                {
                    Component firstByCategory = design.Components.GetFirstByCategory(ComponentCategoryType.WeaponBeam);
                    if (firstByCategory != null)
                    {
                        for (int i = 0; i < num4 - num; i++)
                        {
                            design.Components.Add(new Component(firstByCategory.ComponentID));
                        }
                    }
                }
                if (num2 < num5)
                {
                    Component firstByCategory2 = design.Components.GetFirstByCategory(ComponentCategoryType.WeaponTorpedo);
                    if (firstByCategory2 != null)
                    {
                        for (int j = 0; j < num5 - num2; j++)
                        {
                            design.Components.Add(new Component(firstByCategory2.ComponentID));
                        }
                    }
                }
                if (num3 < num6)
                {
                    Component firstByCategory3 = design.Components.GetFirstByCategory(ComponentCategoryType.WeaponPointDefense);
                    if (firstByCategory3 != null)
                    {
                        for (int k = 0; k < num6 - num3; k++)
                        {
                            design.Components.Add(new Component(firstByCategory3.ComponentID));
                        }
                    }
                }
            }
            return design;
        }

        public Design UpgradeMilitaryShipDesignMoreEngines(Design design)
        {
            if (design != null)
            {
                int num = design.Components.CountByType(ComponentType.EngineMainThrust);
                int num2 = design.Components.CountByType(ComponentType.EngineVectoring);
                int num3 = 5;
                int num4 = 2;
                switch (design.SubRole)
                {
                    case BuiltObjectSubRole.Escort:
                        num3 = 5;
                        num4 = 2;
                        break;
                    case BuiltObjectSubRole.Frigate:
                        num3 = 7;
                        num4 = 2;
                        break;
                    case BuiltObjectSubRole.Destroyer:
                        num3 = 7;
                        num4 = 2;
                        break;
                    case BuiltObjectSubRole.Cruiser:
                        num3 = 10;
                        num4 = 3;
                        break;
                    case BuiltObjectSubRole.CapitalShip:
                        num3 = 12;
                        num4 = 4;
                        break;
                    case BuiltObjectSubRole.Carrier:
                        num3 = 10;
                        num4 = 3;
                        break;
                    case BuiltObjectSubRole.TroopTransport:
                        num3 = 6;
                        num4 = 2;
                        break;
                }
                if (num < num3)
                {
                    Component firstByType = design.Components.GetFirstByType(ComponentType.EngineMainThrust);
                    if (firstByType != null)
                    {
                        design.Components.Add(new Component(firstByType.ComponentID));
                    }
                }
                if (num2 < num4)
                {
                    Component firstByType2 = design.Components.GetFirstByType(ComponentType.EngineVectoring);
                    if (firstByType2 != null)
                    {
                        design.Components.Add(new Component(firstByType2.ComponentID));
                    }
                }
            }
            return design;
        }

        public static double CalculatePlanetaryFacilityBuildTimeFactor(PlanetaryFacility planetaryFacility, Empire empire)
        {
            double num = 1.0;
            if (planetaryFacility != null && empire != null)
            {
                PlanetaryFacilityType type = planetaryFacility.Type;
                num = ((type != PlanetaryFacilityType.Wonder && type != PlanetaryFacilityType.PirateCriminalNetwork) ? (num * empire.PlanetaryFacilityBuildFactor) : (num * empire.PlanetaryWonderBuildFactor));
            }
            return num;
        }

        public static double CalculatePlanetaryFacilityCost(PlanetaryFacilityDefinition planetaryFacility, Empire empire)
        {
            double num = 0.0;
            if (planetaryFacility != null)
            {
                num = planetaryFacility.Cost;
                if (empire != null)
                {
                    PlanetaryFacilityType type = planetaryFacility.Type;
                    num = ((type != PlanetaryFacilityType.Wonder && type != PlanetaryFacilityType.PirateCriminalNetwork) ? (num * empire.PlanetaryFacilityBuildFactor) : (num * empire.PlanetaryWonderBuildFactor));
                }
            }
            return num;
        }

        public PiratePlayStyle SelectRandomPiratePlaystyle()
        {
            return Rnd.Next(0, 4) switch
            {
                0 => PiratePlayStyle.Balanced,
                1 => PiratePlayStyle.Pirate,
                2 => PiratePlayStyle.Mercenary,
                3 => PiratePlayStyle.Smuggler,
                _ => PiratePlayStyle.Balanced,
            };
        }

        public static List<string> ResolvePirateFactionModifierDescriptions(PiratePlayStyle piratePlayStyle, out List<double> factorValues, out List<bool> modifiersAreBonuses)
        {
            double smugglingIncomeFactor = 1.0;
            double raidStrengthFactor = 1.0;
            double raidBonusFactor = 1.0;
            double shipMaintenancePrivateFactor = 1.0;
            double shipMaintenanceStateFactor = 1.0;
            double researchWeaponsFactor = 1.0;
            double researchEnergyFactor = 1.0;
            double researchHighTechFactor = 1.0;
            double planetaryFacilityEliminationFactor = 1.0;
            double lootingFactor = 1.0;
            double planetaryFacilityBuildFactor = 1.0;
            double planetaryWonderBuildFactor = 1.0;
            SetPirateFactionModifiers(piratePlayStyle, out smugglingIncomeFactor, out raidStrengthFactor, out raidBonusFactor, out shipMaintenancePrivateFactor, out shipMaintenanceStateFactor, out researchWeaponsFactor, out researchEnergyFactor, out researchHighTechFactor, out planetaryFacilityEliminationFactor, out lootingFactor, out planetaryFacilityBuildFactor, out planetaryWonderBuildFactor);
            List<string> list = new List<string>();
            factorValues = new List<double>();
            modifiersAreBonuses = new List<bool>();
            if (smugglingIncomeFactor != 1.0)
            {
                list.Add(TextResolver.GetText("Pirate Bonus Modifier Smuggling Income") + ": " + (smugglingIncomeFactor - 1.0).ToString("+0%;-0%"));
                factorValues.Add(smugglingIncomeFactor);
                modifiersAreBonuses.Add(item: true);
            }
            if (raidStrengthFactor != 1.0)
            {
                list.Add(TextResolver.GetText("Pirate Bonus Modifier Raid Strength") + ": " + (raidStrengthFactor - 1.0).ToString("+0%;-0%"));
                factorValues.Add(raidStrengthFactor);
                modifiersAreBonuses.Add(item: true);
            }
            if (raidBonusFactor != 1.0)
            {
                list.Add(TextResolver.GetText("Pirate Bonus Modifier Raid Bonuses") + ": " + (raidBonusFactor - 1.0).ToString("+0%;-0%"));
                factorValues.Add(raidBonusFactor);
                modifiersAreBonuses.Add(item: true);
            }
            if (lootingFactor != 1.0)
            {
                list.Add(TextResolver.GetText("Pirate Bonus Modifier Looting Bonuses") + ": " + (lootingFactor - 1.0).ToString("+0%;-0%"));
                factorValues.Add(lootingFactor);
                modifiersAreBonuses.Add(item: true);
            }
            if (shipMaintenanceStateFactor != 1.0)
            {
                list.Add(TextResolver.GetText("Pirate Bonus Modifier Ship Maintenance State") + ": " + (shipMaintenanceStateFactor - 1.0).ToString("+0%;-0%"));
                factorValues.Add(shipMaintenanceStateFactor);
                modifiersAreBonuses.Add(item: false);
            }
            if (shipMaintenancePrivateFactor != 1.0)
            {
                list.Add(TextResolver.GetText("Pirate Bonus Modifier Ship Maintenance Private") + ": " + (shipMaintenancePrivateFactor - 1.0).ToString("+0%;-0%"));
                factorValues.Add(shipMaintenancePrivateFactor);
                modifiersAreBonuses.Add(item: false);
            }
            if (researchWeaponsFactor != 1.0)
            {
                list.Add(TextResolver.GetText("Pirate Bonus Modifier Research Weapons") + ": " + (researchWeaponsFactor - 1.0).ToString("+0%;-0%"));
                factorValues.Add(researchWeaponsFactor);
                modifiersAreBonuses.Add(item: true);
            }
            if (researchEnergyFactor != 1.0)
            {
                list.Add(TextResolver.GetText("Pirate Bonus Modifier Research Energy") + ": " + (researchEnergyFactor - 1.0).ToString("+0%;-0%"));
                factorValues.Add(researchEnergyFactor);
                modifiersAreBonuses.Add(item: true);
            }
            if (researchHighTechFactor != 1.0)
            {
                list.Add(TextResolver.GetText("Pirate Bonus Modifier Research HighTech") + ": " + (researchHighTechFactor - 1.0).ToString("+0%;-0%"));
                factorValues.Add(researchHighTechFactor);
                modifiersAreBonuses.Add(item: true);
            }
            if (planetaryFacilityBuildFactor != 1.0)
            {
                list.Add(TextResolver.GetText("Pirate Bonus Modifier Facility Build") + ": " + (planetaryFacilityBuildFactor - 1.0).ToString("+0%;-0%"));
                factorValues.Add(planetaryFacilityBuildFactor);
                modifiersAreBonuses.Add(item: false);
            }
            if (planetaryWonderBuildFactor != 1.0)
            {
                list.Add(TextResolver.GetText("Pirate Bonus Modifier Wonder Build") + ": " + (planetaryWonderBuildFactor - 1.0).ToString("+0%;-0%"));
                factorValues.Add(planetaryWonderBuildFactor);
                modifiersAreBonuses.Add(item: false);
            }
            if (planetaryFacilityEliminationFactor != 1.0)
            {
                list.Add(TextResolver.GetText("Pirate Bonus Modifier Facility Elimination") + ": " + (planetaryFacilityEliminationFactor - 1.0).ToString("+0%;-0%"));
                factorValues.Add(planetaryFacilityEliminationFactor);
                modifiersAreBonuses.Add(item: true);
            }
            return list;
        }

        public static void SetPirateFactionModifiers(PiratePlayStyle piratePlayStyle, out double smugglingIncomeFactor, out double raidStrengthFactor, out double raidBonusFactor, out double shipMaintenancePrivateFactor, out double shipMaintenanceStateFactor, out double researchWeaponsFactor, out double researchEnergyFactor, out double researchHighTechFactor, out double planetaryFacilityEliminationFactor, out double lootingFactor, out double planetaryFacilityBuildFactor, out double planetaryWonderBuildFactor)
        {
            smugglingIncomeFactor = 1.0;
            raidStrengthFactor = 1.0;
            raidBonusFactor = 1.0;
            shipMaintenancePrivateFactor = 1.0;
            shipMaintenanceStateFactor = 1.0;
            researchWeaponsFactor = 1.0;
            researchEnergyFactor = 1.0;
            researchHighTechFactor = 1.0;
            planetaryFacilityEliminationFactor = 1.0;
            lootingFactor = 1.0;
            planetaryFacilityBuildFactor = 1.0;
            planetaryWonderBuildFactor = 1.0;
            switch (piratePlayStyle)
            {
                case PiratePlayStyle.Balanced:
                    smugglingIncomeFactor = 1.0;
                    raidStrengthFactor = 1.0;
                    raidBonusFactor = 1.0;
                    shipMaintenancePrivateFactor = 1.0;
                    shipMaintenanceStateFactor = 1.0;
                    researchWeaponsFactor = 1.0;
                    researchEnergyFactor = 1.0;
                    researchHighTechFactor = 1.0;
                    planetaryFacilityEliminationFactor = 1.0;
                    lootingFactor = 1.0;
                    planetaryFacilityBuildFactor = 1.0;
                    planetaryWonderBuildFactor = 1.0;
                    break;
                case PiratePlayStyle.Pirate:
                    smugglingIncomeFactor = 0.7;
                    raidStrengthFactor = 1.25;
                    raidBonusFactor = 1.4;
                    shipMaintenancePrivateFactor = 1.1;
                    shipMaintenanceStateFactor = 1.1;
                    researchWeaponsFactor = 1.2;
                    researchEnergyFactor = 1.0;
                    researchHighTechFactor = 0.8;
                    planetaryFacilityEliminationFactor = 0.8;
                    lootingFactor = 1.0;
                    planetaryFacilityBuildFactor = 1.5;
                    planetaryWonderBuildFactor = 1.5;
                    break;
                case PiratePlayStyle.Mercenary:
                    smugglingIncomeFactor = 0.75;
                    raidStrengthFactor = 1.25;
                    raidBonusFactor = 0.75;
                    shipMaintenancePrivateFactor = 1.25;
                    shipMaintenanceStateFactor = 0.75;
                    researchWeaponsFactor = 1.1;
                    researchEnergyFactor = 1.1;
                    researchHighTechFactor = 0.8;
                    planetaryFacilityEliminationFactor = 1.33;
                    lootingFactor = 1.33;
                    planetaryFacilityBuildFactor = 0.75;
                    planetaryWonderBuildFactor = 1.25;
                    break;
                case PiratePlayStyle.Smuggler:
                    smugglingIncomeFactor = 1.5;
                    raidStrengthFactor = 0.75;
                    raidBonusFactor = 0.75;
                    shipMaintenancePrivateFactor = 0.75;
                    shipMaintenanceStateFactor = 1.25;
                    researchWeaponsFactor = 0.8;
                    researchEnergyFactor = 1.1;
                    researchHighTechFactor = 1.1;
                    planetaryFacilityEliminationFactor = 1.7;
                    lootingFactor = 0.75;
                    planetaryFacilityBuildFactor = 1.0;
                    planetaryWonderBuildFactor = 0.75;
                    break;
            }
        }

        public Empire GeneratePirateEmpire(Habitat habitat, int offsetX, int offsetY, bool useRace)
        {
            Race race = null;
            PiratePlayStyle piratePlaystyle = SelectRandomPiratePlaystyle();
            if (useRace)
            {
                race = SelectRandomPirateRace();
                if (race == null)
                {
                    race = SelectRandomAggressiveRace(115);
                }
                if (race == null)
                {
                    race = SelectRandomRace(0);
                }
                piratePlaystyle = race.DefaultPiratePlaystyle;
            }
            return GeneratePirateEmpire(habitat, offsetX, offsetY, race, piratePlaystyle);
        }

        public Empire GeneratePirateEmpire(Habitat habitat, int offsetX, int offsetY, Race race, PiratePlayStyle piratePlaystyle)
        {
            return GeneratePirateEmpire(habitat, offsetX, offsetY, race, -1, 0.5, piratePlaystyle, isPlayerEmpire: false, isSuperPirates: false);
        }

        public Empire GeneratePirateEmpire(Habitat habitat, int offsetX, int offsetY, Race race, int designPictureFamilyIndex, double techLevel, PiratePlayStyle piratePlaystyle, bool isPlayerEmpire, bool isSuperPirates)
        {
            if (Empires != null && Empires.Count > 0)
            {
                int index = Rnd.Next(0, Empires.Count);
                _ = Empires[index];
            }
            EmpirePolicy empirePolicy = LoadEmpirePolicy(race, isPirate: true);
            if (isPlayerEmpire)
            {
                empirePolicy.ImplementEnslavementWithPenalColonies = false;
                empirePolicy.AcceptPirateSmugglingMissions = false;
                empirePolicy.BidOnPirateAttackMissions = false;
                empirePolicy.BidOnPirateDefendMissions = false;
                empirePolicy.OfferSmugglingPirateMissions = 0;
                empirePolicy.OfferDefensivePirateMissionsSituation = 0;
                empirePolicy.OfferPirateAttackMissions = 0;
            }
            string name = GeneratePirateEmpireName(habitat, piratePlaystyle);
            Empire empire = new Empire(this, name, isIndependentEmpire: false, habitat, race, empirePolicy);
            empire.PiratePlayStyle = piratePlaystyle;
            bool flag = false;
            if (StartingAge == 0)
            {
                long num = CurrentStarDate - _StartStarDate;
                if (num <= 120000)
                {
                    flag = true;
                }
            }
            if (!flag && !isSuperPirates)
            {
                empire.DifficultyLevelModifier = 0.5;
            }
            SetEmpireDifficultyFactors(empire);
            empire.SetPirateFactionModifiers(empire.PiratePlayStyle);
            empire.PreWarpProgressEventOccurredSendPirateRaid = true;
            empire.PreWarpProgressEventOccurredExperienceFirstPirateRaid = true;
            empire.PreWarpProgressEventOccurredFirstContactPirateOrIndependent = true;
            empire.PreWarpProgressEventOccurredFirstContactNormalEmpire = true;
            empire.PreWarpProgressEventOccurredBuildFirstShip = true;
            empire.PreWarpProgressEventOccurredBuildFirstSpaceport = true;
            empire.PreWarpProgressEventOccurredBuildFirstMiningStation = true;
            empire.PreWarpProgressEventOccurredBuildFirstResearchStation = true;
            empire.PreWarpProgressEventOccurredDiscoverHyperspaceTech = true;
            empire.PreWarpProgressEventOccurredDiscoverColonizationTech = true;
            empire.PreWarpProgressEventOccurredFirstHyperjump = true;
            empire.PreWarpProgressEventOccurredEncounterFirstKaltor = true;
            empire.PreWarpProgressEventOccurredBuildFirstMilitaryShip = true;
            Habitat habitat2 = FastFindNearestIndependentHabitat(habitat.Xpos, habitat.Ypos);
            if (habitat2 != null && !empire.CheckSystemExplored(habitat2.SystemIndex))
            {
                Habitat systemStar = DetermineHabitatSystemStar(habitat2);
                empire.SetSystemVisibility(systemStar, SystemVisibilityStatus.Explored);
                empire.ResourceMap.SetResourcesKnown(Systems[systemStar].SystemStar, known: true);
                for (int i = 0; i < Systems[systemStar].Habitats.Count; i++)
                {
                    empire.ResourceMap.SetResourcesKnown(Systems[systemStar].Habitats[i], known: true);
                }
            }
            PirateEmpires.Add(empire);
            empire.LargeFlagPicture = _PirateFlagLarge;
            empire.SmallFlagPicture = _PirateFlagSmall;
            empire.MainColor = Color.FromArgb(1, 1, 1);
            empire.SecondaryColor = Color.FromArgb(254, 254, 254);
            Bitmap smallFlagPicture = null;
            Bitmap largeFlagPicture = null;
            if (!isSuperPirates)
            {
                Color mainColor = Color.FromArgb(1, 1, 1);
                Color secondaryColor = Color.FromArgb(254, 254, 254);
                empire.SelectEmpireColors(isPirateFaction: true, out mainColor, out secondaryColor);
                empire.MainColor = mainColor;
                empire.SecondaryColor = secondaryColor;
                if (FlagShapesPirates.Count >= 30 && piratePlaystyle != 0)
                {
                    List<Bitmap> range = FlagShapesPirates.GetRange(0, 18);
                    List<Bitmap> range2 = FlagShapesPirates.GetRange(18, FlagShapesPirates.Count - 18);
                    switch (piratePlaystyle)
                    {
                        case PiratePlayStyle.Balanced:
                            empire.FlagShape = GenerateEmpireFlag(mainColor, secondaryColor, -1, range2, ref smallFlagPicture, ref largeFlagPicture);
                            break;
                        case PiratePlayStyle.Mercenary:
                            empire.FlagShape = GenerateEmpireFlag(mainColor, secondaryColor, -1, range, ref smallFlagPicture, ref largeFlagPicture);
                            break;
                        case PiratePlayStyle.Pirate:
                            empire.FlagShape = GenerateEmpireFlag(mainColor, secondaryColor, -1, range, ref smallFlagPicture, ref largeFlagPicture);
                            break;
                        case PiratePlayStyle.Smuggler:
                            empire.FlagShape = GenerateEmpireFlag(mainColor, secondaryColor, -1, range2, ref smallFlagPicture, ref largeFlagPicture);
                            break;
                    }
                }
                else
                {
                    empire.FlagShape = GenerateEmpireFlag(mainColor, secondaryColor, -1, FlagShapesPirates, ref smallFlagPicture, ref largeFlagPicture);
                }
                empire.LargeFlagPicture = largeFlagPicture;
                empire.SmallFlagPicture = smallFlagPicture;
            }
            Bitmap bitmap = (empire.MediumFlagPicture = GraphicsHelper.ScaleImage(empire.LargeFlagPicture, 35, 21, 1f, lowQuality: false));
            if (!isSuperPirates)
            {
                using Graphics graphics = Graphics.FromImage(largeFlagPicture);
                GraphicsHelper.SetGraphicsQualityToHigh(graphics);
                graphics.DrawImage(srcRect: new Rectangle(0, 0, PirateFlagLarge.Width, PirateFlagLarge.Height), destRect: new Rectangle(2, 2, 35, 22), image: PirateFlagLarge, srcUnit: GraphicsUnit.Pixel);
            }
            empire.Policy = empirePolicy;
            if (designPictureFamilyIndex >= 0)
            {
                empire.DesignPictureFamilyIndex = designPictureFamilyIndex;
            }
            if (techLevel != 0.5)
            {
                empire.Research.TechTree = ResearchNodeDefinitionsStatic.SetTechTreeLevel(this, empire.Research.TechTree, race, techLevel, isPirate: true);
            }
            else
            {
                empire.Research.TechTree = ResearchNodeDefinitionsStatic.SetTechTreeStartingDefaultsPirates(empire.Research.TechTree, race, empire.Policy);
            }
            empire.Research.Update(race);
            empire.ReviewResearchAbilities();
            empire.ReviewDesignsBuiltObjectsImprovedComponents();
            empire.PirateEmpireBaseHabitat = habitat;
            empire.GenerateDesignSpecifications(this, empire.DominantRace, isPirate: true, empire.DominantRace.Name);
            empire.CreateNewDesigns(CurrentStarDate);
            if (!isSuperPirates)
            {
                BuiltObject builtObject = null;
                Design design = empire.LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.SmallSpacePort, empire);
                if (design != null)
                {
                    string name2 = GeneratePirateBaseName(habitat);
                    design.BuildCount++;
                    builtObject = new BuiltObject(design, name2, this, fullyBuilt: true);
                    builtObject.Empire = empire;
                    builtObject.Heading = SelectRandomHeading();
                    builtObject.TargetHeading = builtObject.Heading;
                    builtObject.SupportCostFactor = 0f;
                    builtObject.ReDefine();
                    builtObject.CurrentFuel = builtObject.FuelCapacity;
                    builtObject.CurrentShields = builtObject.ShieldsCapacity;
                    empire.AddBuiltObjectToGalaxy(builtObject, habitat, offsetLocationFromParent: false, isStateOwned: true, offsetX, offsetY, sendMessage: false);
                    int num2 = 6000;
                    int num3 = 4000;
                    int num4 = 2000;
                    int num5 = 800;
                    for (int j = 0; j < ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; j++)
                    {
                        ResourceDefinition resourceDefinition = ResourceSystem.StrategicResourcesOrderedByRelativeImportance[j];
                        if (resourceDefinition != null)
                        {
                            int num6 = num2;
                            num6 = ((resourceDefinition.RelativeImportance > 0.4f || resourceDefinition.IsFuel) ? num2 : ((resourceDefinition.RelativeImportance > 0.25f) ? num3 : ((!(resourceDefinition.RelativeImportance > 0.1f)) ? num5 : num4)));
                            builtObject.Cargo.Add(new Cargo(new Resource(resourceDefinition.ResourceID), num6, empire));
                        }
                    }
                    int escortShipsCount = 2;
                    int explorationShipCount = 2;
                    int smallFreighterCount = 2;
                    int constructorShipCount = 1;
                    int resuplyShipCount = 1;
                    int minishShipCount = 1;
                    int count = 2;
                    switch (empire.PiratePlayStyle)
                    {
                        case PiratePlayStyle.Pirate:
                            escortShipsCount = 3;
                            smallFreighterCount = 1;
                            minishShipCount = 1;
                            break;
                        case PiratePlayStyle.Mercenary:
                            escortShipsCount = 4;
                            explorationShipCount = 1;
                            smallFreighterCount = 1;
                            minishShipCount = 0;
                            count = 1;
                            break;
                        case PiratePlayStyle.Smuggler:
                            escortShipsCount = 1;
                            smallFreighterCount = 4;
                            minishShipCount = 1;
                            count = 3;
                            break;
                    }
                    if (!flag)
                    {
                        escortShipsCount /= 2;
                        explorationShipCount /= 2;
                        resuplyShipCount = 0;
                    }
                    int num13 = 1;
                    for (int k = 0; k < escortShipsCount; k++)
                    {
                        Design design2 = null;
                        if (num13 == 1)
                        {
                            if (design2 == null)
                            {
                                design2 = empire.LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.Escort, empire);
                            }
                        }
                        else
                        {
                            design2 = empire.LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.Escort, empire);
                        }
                        num13 = Rnd.Next(0, 3);
                        if (design2 != null)
                        {
                            design2.BuildCount++;
                            string name3 = SelectRandomUniqueMilitaryShipName();
                            BuiltObject builtObject2 = new BuiltObject(design2, name3, this, fullyBuilt: true);
                            builtObject2.Empire = empire;
                            builtObject2.Heading = SelectRandomHeading();
                            builtObject2.TargetHeading = builtObject2.Heading;
                            builtObject2.ReDefine();
                            builtObject2.CurrentFuel = builtObject2.FuelCapacity;
                            builtObject2.CurrentShields = builtObject2.ShieldsCapacity;
                            empire.AddBuiltObjectToGalaxy(builtObject2, habitat, offsetLocationFromParent: true, isStateOwned: true, sendMessage: false);
                        }
                    }
                    Design design3 = empire.LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.ExplorationShip, empire);
                    if (design3 != null)
                    {
                        for (int l = 0; l < explorationShipCount; l++)
                        {
                            design3.BuildCount++;
                            string name4 = SelectRandomUniqueStandardShipName(habitat);
                            BuiltObject builtObject3 = new BuiltObject(design3, name4, this, fullyBuilt: true);
                            builtObject3.Empire = empire;
                            builtObject3.Heading = SelectRandomHeading();
                            builtObject3.TargetHeading = builtObject3.Heading;
                            builtObject3.ReDefine();
                            builtObject3.CurrentFuel = builtObject3.FuelCapacity;
                            builtObject3.CurrentShields = builtObject3.ShieldsCapacity;
                            empire.AddBuiltObjectToGalaxy(builtObject3, habitat, offsetLocationFromParent: true, isStateOwned: true, sendMessage: false);
                        }
                    }
                    Design design4 = empire.LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.SmallFreighter, empire);
                    if (design4 != null)
                    {
                        for (int m = 0; m < smallFreighterCount; m++)
                        {
                            design4.BuildCount++;
                            string name5 = SelectUniqueBuiltObjectName(design4, habitat);
                            BuiltObject builtObject4 = new BuiltObject(design4, name5, this, fullyBuilt: true);
                            builtObject4.Empire = empire;
                            builtObject4.Heading = SelectRandomHeading();
                            builtObject4.TargetHeading = builtObject4.Heading;
                            builtObject4.ReDefine();
                            builtObject4.CurrentFuel = builtObject4.FuelCapacity;
                            builtObject4.CurrentShields = builtObject4.ShieldsCapacity;
                            empire.AddBuiltObjectToGalaxy(builtObject4, habitat, offsetLocationFromParent: true, isStateOwned: false, sendMessage: false);
                        }
                    }
                    Design design5 = empire.LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.MiningShip, empire);
                    if (design5 != null)
                    {
                        for (int n = 0; n < minishShipCount; n++)
                        {
                            design5.BuildCount++;
                            string name6 = SelectUniqueBuiltObjectName(design5, habitat);
                            BuiltObject builtObject5 = new BuiltObject(design5, name6, this, fullyBuilt: true);
                            builtObject5.Empire = empire;
                            builtObject5.Heading = SelectRandomHeading();
                            builtObject5.TargetHeading = builtObject5.Heading;
                            builtObject5.ReDefine();
                            builtObject5.CurrentFuel = builtObject5.FuelCapacity;
                            builtObject5.CurrentShields = builtObject5.ShieldsCapacity;
                            empire.AddBuiltObjectToGalaxy(builtObject5, habitat, offsetLocationFromParent: true, isStateOwned: false, sendMessage: false);
                        }
                    }
                    Design design6 = empire.LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.GasMiningShip, empire);
                    if (design6 != null)
                    {
                        for (int num14 = 0; num14 < minishShipCount; num14++)
                        {
                            design6.BuildCount++;
                            string name7 = SelectUniqueBuiltObjectName(design6, habitat);
                            BuiltObject builtObject6 = new BuiltObject(design6, name7, this, fullyBuilt: true);
                            builtObject6.Empire = empire;
                            builtObject6.Heading = SelectRandomHeading();
                            builtObject6.TargetHeading = builtObject6.Heading;
                            builtObject6.ReDefine();
                            builtObject6.CurrentFuel = builtObject6.FuelCapacity;
                            builtObject6.CurrentShields = builtObject6.ShieldsCapacity;
                            empire.AddBuiltObjectToGalaxy(builtObject6, habitat, offsetLocationFromParent: true, isStateOwned: false, sendMessage: false);
                        }
                    }
                    Design design7 = empire.LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.ConstructionShip, empire);
                    if (design7 != null)
                    {
                        for (int num15 = 0; num15 < constructorShipCount; num15++)
                        {
                            design7.BuildCount++;
                            string name8 = SelectRandomUniqueStandardShipName(habitat);
                            BuiltObject builtObject7 = new BuiltObject(design7, name8, this, fullyBuilt: true);
                            builtObject7.Empire = empire;
                            builtObject7.Heading = SelectRandomHeading();
                            builtObject7.TargetHeading = builtObject7.Heading;
                            builtObject7.ReDefine();
                            builtObject7.CurrentFuel = builtObject7.FuelCapacity;
                            builtObject7.CurrentShields = builtObject7.ShieldsCapacity;
                            empire.AddBuiltObjectToGalaxy(builtObject7, habitat, offsetLocationFromParent: true, isStateOwned: true, sendMessage: false);
                        }
                    }
                    Design design8 = empire.Designs.FindNewest(BuiltObjectSubRole.ResupplyShip);
                    if (design8 != null)
                    {
                        for (int num16 = 0; num16 < resuplyShipCount; num16++)
                        {
                            design8.BuildCount++;
                            string name9 = SelectUniqueBuiltObjectName(design4, habitat);
                            BuiltObject builtObject8 = new BuiltObject(design8, name9, this, fullyBuilt: true);
                            builtObject8.Empire = empire;
                            builtObject8.Heading = SelectRandomHeading();
                            builtObject8.TargetHeading = builtObject8.Heading;
                            builtObject8.ReDefine();
                            builtObject8.CurrentFuel = builtObject8.FuelCapacity;
                            builtObject8.CurrentShields = builtObject8.ShieldsCapacity;
                            empire.AddBuiltObjectToGalaxy(builtObject8, habitat, offsetLocationFromParent: true, isStateOwned: true, sendMessage: false);
                        }
                    }
                    CreatePirateMiningStations(this, empire, count, allowEmpiresToStartInSameSystem: false);
                }
                empire.GenerateStartingCharacters(builtObject);
            }
            empire.ColonizationTargets = empire.PirateReviewColoniesToControl();
            empire.StateMoney = 20000.0;
            return empire;
        }

    }
}
