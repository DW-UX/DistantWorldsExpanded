using BaconDistantWorlds;

using DistantWorlds.Types.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace DistantWorlds.Types
{
    public partial class Empire
    {
        public ResourceList IdentifyDeficientEmpireResources()
        {
            return IdentifyDeficientEmpireResources(includeLuxuryResources: false, 0.0);
        }

        public ResourceList IdentifyDeficientEmpireResources(bool includeLuxuryResources, double minimumDemand)
        {
            OrderList orders = _Galaxy.Orders.GetOrders(this);
            int[] array = new int[_Galaxy.ResourceSystem.Resources.Count];
            foreach (Order item in orders)
            {
                if (item.CommodityResource != null && item.AmountOutstandingToContract > 0)
                {
                    Resource commodityResource = item.CommodityResource;
                    array[commodityResource.ResourceID] += item.AmountOutstandingToContract;
                }
            }
            int[] array2 = new int[_Galaxy.ResourceSystem.Resources.Count];
            for (int i = 0; i < SpacePorts.Count; i++)
            {
                BuiltObject builtObject = SpacePorts[i];
                if (builtObject.Cargo == null)
                {
                    continue;
                }
                foreach (Cargo item2 in builtObject.Cargo)
                {
                    if (item2.CommodityResource != null && item2.Available > 0)
                    {
                        Resource commodityResource2 = item2.CommodityResource;
                        array2[commodityResource2.ResourceID] += item2.Available;
                    }
                }
            }
            if (CheckEmpireHasHyperDriveTech(this))
            {
                for (int j = 0; j < MiningStations.Count; j++)
                {
                    BuiltObject builtObject2 = MiningStations[j];
                    if (builtObject2.Cargo == null)
                    {
                        continue;
                    }
                    foreach (Cargo item3 in builtObject2.Cargo)
                    {
                        if (item3.CommodityResource != null && item3.Available > 0)
                        {
                            Resource commodityResource3 = item3.CommodityResource;
                            array2[commodityResource3.ResourceID] += item3.Available;
                        }
                    }
                }
            }
            for (int k = 0; k < array2.Length; k++)
            {
                Resource resource = new Resource(_Galaxy.ResourceSystem.Resources[k].ResourceID);
                bool isCriticalEmpireResource = false;
                if (DominantRace != null && DominantRace.CriticalResources.GetBonusByResourceType(resource.ResourceID) != null)
                {
                    isCriticalEmpireResource = true;
                }
                if (CheckResourceSupplyMeetsExpected(resource, isCriticalEmpireResource, 1.5))
                {
                    array2[k] = Math.Max(10000, Math.Max(array2[k] * 2, array[k] * 5));
                }
            }
            ResourceList resourceList = new ResourceList();
            for (int l = 0; l < Galaxy.ResourceSystem.Resources.Count; l++)
            {
                ResourceDefinition resourceDefinition = Galaxy.ResourceSystem.Resources[l];
                Resource resource2 = new Resource(resourceDefinition.ResourceID);
                double val = 0.0;
                if (includeLuxuryResources)
                {
                    val = (double)array[l] / Math.Max(1.0, array2[l]);
                }
                else if (!resource2.IsLuxuryResource)
                {
                    val = (double)array[l] / Math.Max(1.0, array2[l]);
                }
                resource2.SortTag = Math.Max(minimumDemand, val);
                resourceList.Add(resource2);
            }
            resourceList.Sort();
            resourceList.Reverse();
            return resourceList;
        }

        public void RefreshColonyFacilityInfo()
        {
            Capitals = IdentifyEmpireCapitals();
            CapitalSystemStars.Clear();
            for (int i = 0; i < Capitals.Count; i++)
            {
                CapitalSystemStars.Add(Galaxy.DetermineHabitatSystemStar(Capitals[i]));
            }
        }

        public void ReviewColonyWonders()
        {
            if (DominantRace == null || !DominantRace.Expanding)
            {
                return;
            }
            PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList = Research.BuildablePlanetaryFacilities.Clone();
            PlanetaryFacilityDefinitionList wonders = planetaryFacilityDefinitionList.GetWonders();
            PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList2 = new PlanetaryFacilityDefinitionList();
            for (int i = 0; i < wonders.Count; i++)
            {
                if (_Galaxy.CheckWonderBuilt(wonders[i]))
                {
                    planetaryFacilityDefinitionList2.Add(wonders[i]);
                    continue;
                }
                for (int j = 0; j < Colonies.Count; j++)
                {
                    Habitat habitat = Colonies[j];
                    if (habitat == null || habitat.Facilities == null || habitat.HasBeenDestroyed)
                    {
                        continue;
                    }
                    for (int k = 0; k < habitat.Facilities.Count; k++)
                    {
                        PlanetaryFacility planetaryFacility = habitat.Facilities[k];
                        if (planetaryFacility != null && planetaryFacility.PlanetaryFacilityDefinitionId == wonders[i].PlanetaryFacilityDefinitionId)
                        {
                            planetaryFacilityDefinitionList2.Add(wonders[i]);
                        }
                    }
                }
            }
            for (int l = 0; l < planetaryFacilityDefinitionList2.Count; l++)
            {
                wonders.Remove(planetaryFacilityDefinitionList2[l]);
            }
            int refusalCount = 0;
            double annualEmpireExpenses = 0.0;
            double num = CalculateAccurateAnnualCashflowIncludingUnderConstruction(out annualEmpireExpenses);
            HabitatList habitatList = new HabitatList();
            habitatList.AddRange(Colonies);
            habitatList.Sort();
            habitatList.Reverse();
            for (int m = 0; m < wonders.Count; m++)
            {
                PlanetaryFacilityDefinition planetaryFacilityDefinition = wonders[m];
                if (planetaryFacilityDefinition == null || planetaryFacilityDefinition.Type != PlanetaryFacilityType.Wonder)
                {
                    continue;
                }
                double num2 = StateMoney / 1.5;
                if (Policy.PrioritizeBuildWonderId >= 0 && planetaryFacilityDefinition.PlanetaryFacilityDefinitionId == Policy.PrioritizeBuildWonderId)
                {
                    num2 = StateMoney;
                }
                double num3 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition, this);
                if (num3 < num2 && num > planetaryFacilityDefinition.Maintenance)
                {
                    Habitat habitat2 = null;
                    switch (planetaryFacilityDefinition.WonderType)
                    {
                        case WonderType.ColonyIncome:
                            {
                                HabitatList orderedColonies2 = Colonies.OrderByRevenue();
                                habitat2 = BuildWonderAtFirstAvailableColony(planetaryFacilityDefinition, orderedColonies2, ref refusalCount);
                                break;
                            }
                        case WonderType.ColonyPopulationGrowth:
                            {
                                HabitatList orderedColonies = Colonies.OrderByPopulationProportion(0.5, 1000000000L);
                                habitat2 = BuildWonderAtFirstAvailableColony(planetaryFacilityDefinition, orderedColonies, ref refusalCount);
                                break;
                            }
                        case WonderType.EmpirePopulationGrowth:
                        case WonderType.EmpireHappiness:
                        case WonderType.EmpireResearchWeapons:
                        case WonderType.EmpireResearchEnergy:
                        case WonderType.EmpireResearchHighTech:
                        case WonderType.EmpireIncome:
                        case WonderType.ColonyHappiness:
                        case WonderType.ColonyDefense:
                        case WonderType.ColonyConstructionSpeed:
                        case WonderType.RaceAchievement:
                            habitat2 = BuildWonderAtFirstAvailableColony(planetaryFacilityDefinition, habitatList, ref refusalCount);
                            break;
                    }
                    if (habitat2 != null)
                    {
                        SendNewsBroadcastWonderBegin(planetaryFacilityDefinition, habitat2);
                    }
                }
            }
        }

        private Habitat BuildWonderAtFirstAvailableColony(PlanetaryFacilityDefinition wonder, HabitatList orderedColonies, ref int refusalCount)
        {
            for (int i = 0; i < orderedColonies.Count; i++)
            {
                Habitat habitat = CheckBuildWonder(wonder, orderedColonies[i], ref refusalCount);
                if (habitat != null)
                {
                    return habitat;
                }
            }
            return null;
        }

        private Habitat CheckBuildWonder(PlanetaryFacilityDefinition wonder, Habitat colony, ref int refusalCount)
        {
            if (wonder != null && colony != null && !colony.HasBeenDestroyed && colony.Facilities != null)
            {
                double num = Galaxy.CalculatePlanetaryFacilityCost(wonder, this);
                if (StateMoney >= num)
                {
                    bool flag = false;
                    if (colony.Facilities.Count > 0)
                    {
                        PlanetaryFacility planetaryFacility = colony.Facilities[colony.Facilities.Count - 1];
                        if (planetaryFacility != null && planetaryFacility.Type == PlanetaryFacilityType.Wonder && planetaryFacility.ConstructionProgress < 1f)
                        {
                            flag = true;
                        }
                    }
                    if (!flag && colony.CanBuildWonder(wonder))
                    {
                        double annualEmpireExpenses = 0.0;
                        double num2 = CalculateAccurateAnnualCashflowIncludingUnderConstruction(out annualEmpireExpenses);
                        if (num2 > wonder.Maintenance && CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(colony, wonder), colony, AdvisorMessageType.ColonyFacility, wonder, null) && colony.QueueWonderConstruction(wonder))
                        {
                            StateMoney -= num;
                            PirateEconomy.PerformExpense(num, PirateExpenseType.FacilityConstruction, _Galaxy.CurrentStarDate);
                            return colony;
                        }
                    }
                }
            }
            return null;
        }

        public void PirateReviewColonyFacilities()
        {
            PlanetaryFacilityDefinition planetaryFacilityDefinition = Galaxy.PlanetaryFacilityDefinitionsStatic.FindFacilityByType(PlanetaryFacilityType.PirateBase);
            PlanetaryFacilityDefinition planetaryFacilityDefinition2 = Galaxy.PlanetaryFacilityDefinitionsStatic.FindFacilityByType(PlanetaryFacilityType.PirateFortress);
            PlanetaryFacilityDefinition planetaryFacilityDefinition3 = Galaxy.PlanetaryFacilityDefinitionsStatic.FindFacilityByType(PlanetaryFacilityType.PirateCriminalNetwork);
            int refusalCount = 0;
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat == null)
                {
                    continue;
                }
                bool flag = false;
                PirateColonyControl pirateColonyControl = habitat.GetPirateControl().GetByFacilityControl();
                if (pirateColonyControl != null && pirateColonyControl.EmpireId == EmpireId)
                {
                    flag = true;
                }
                else
                {
                    pirateColonyControl = habitat.GetPirateControl().GetHighestControl();
                    if (pirateColonyControl != null && pirateColonyControl.EmpireId == EmpireId && pirateColonyControl.ControlLevel >= 0.5f)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    continue;
                }
                if (habitat.Facilities == null)
                {
                    habitat.Facilities = new PlanetaryFacilityList();
                }
                if (habitat.Facilities == null)
                {
                    continue;
                }
                if (pirateColonyControl.ControlLevel >= 1f)
                {
                    if (habitat.Facilities.CountByType(PlanetaryFacilityType.PirateBase) <= 0)
                    {
                        bool flag2 = false;
                        double num = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition, this);
                        if (StateMoney >= num)
                        {
                            flag2 = true;
                        }
                        if (!flag2)
                        {
                            if (!CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitat, planetaryFacilityDefinition, haveFunds: false), habitat, AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition, null))
                            {
                            }
                        }
                        else if (CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitat, planetaryFacilityDefinition), habitat, AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition, null) && habitat.QueueFacilityConstruction(PlanetaryFacilityType.PirateBase))
                        {
                            StateMoney -= num;
                            PirateEconomy.PerformExpense(num, PirateExpenseType.FacilityConstruction, _Galaxy.CurrentStarDate);
                            pirateColonyControl.HasFacilityControl = true;
                        }
                    }
                    else if (habitat.Facilities.CountByType(PlanetaryFacilityType.PirateFortress) <= 0 && habitat.Facilities.CountCompletedByType(PlanetaryFacilityType.PirateBase) > 0)
                    {
                        bool flag3 = false;
                        double num2 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition2, this);
                        if (StateMoney >= num2)
                        {
                            flag3 = true;
                        }
                        if (!flag3)
                        {
                            if (!CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitat, planetaryFacilityDefinition2, haveFunds: false), habitat, AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition2, null))
                            {
                            }
                        }
                        else if (CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitat, planetaryFacilityDefinition2), habitat, AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition2, null) && habitat.QueueFacilityConstruction(PlanetaryFacilityType.PirateFortress))
                        {
                            StateMoney -= num2;
                            PirateEconomy.PerformExpense(num2, PirateExpenseType.FacilityConstruction, _Galaxy.CurrentStarDate);
                            pirateColonyControl.HasFacilityControl = true;
                        }
                    }
                    else
                    {
                        if (habitat.Facilities.CountCompletedByType(PlanetaryFacilityType.PirateFortress) <= 0)
                        {
                            continue;
                        }
                        bool flag4 = false;
                        double num3 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition3, this);
                        if (StateMoney >= num3)
                        {
                            flag4 = true;
                        }
                        if (CountPirateCriminalNetworks() > 0)
                        {
                            continue;
                        }
                        if (!flag4)
                        {
                            if (!CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitat, planetaryFacilityDefinition3, haveFunds: false), habitat, AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition3, null))
                            {
                            }
                        }
                        else if (CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitat, planetaryFacilityDefinition3), habitat, AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition3, null) && habitat.QueueFacilityConstruction(PlanetaryFacilityType.PirateCriminalNetwork))
                        {
                            StateMoney -= num3;
                            PirateEconomy.PerformExpense(num3, PirateExpenseType.FacilityConstruction, _Galaxy.CurrentStarDate);
                            pirateColonyControl.HasFacilityControl = true;
                        }
                    }
                }
                else
                {
                    if (habitat.Facilities.CountByType(PlanetaryFacilityType.PirateBase) > 0)
                    {
                        continue;
                    }
                    bool flag5 = false;
                    double num4 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition, this);
                    if (StateMoney >= num4)
                    {
                        flag5 = true;
                    }
                    if (!flag5)
                    {
                        if (!CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitat, planetaryFacilityDefinition, haveFunds: false), habitat, AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition, null))
                        {
                        }
                    }
                    else if (CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitat, planetaryFacilityDefinition), habitat, AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition, null) && habitat.QueueFacilityConstruction(PlanetaryFacilityType.PirateBase))
                    {
                        StateMoney -= num4;
                        PirateEconomy.PerformExpense(num4, PirateExpenseType.FacilityConstruction, _Galaxy.CurrentStarDate);
                        pirateColonyControl.HasFacilityControl = true;
                    }
                }
            }
        }

        public void ReviewColonyFacilities()
        {
            PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList = Research.BuildablePlanetaryFacilities.Clone();
            PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList2 = new PlanetaryFacilityDefinitionList();
            for (int i = 0; i < planetaryFacilityDefinitionList.Count; i++)
            {
                switch (planetaryFacilityDefinitionList[i].Type)
                {
                    case PlanetaryFacilityType.ArmoredFactory:
                        if (!Policy.ColonyAllowFacilityArmoredFactory)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                        }
                        break;
                    case PlanetaryFacilityType.SpyAcademy:
                        if (!Policy.ColonyAllowFacilitySpyAcademy)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                        }
                        break;
                    case PlanetaryFacilityType.ScienceAcademy:
                        if (!Policy.ColonyAllowFacilityScienceAcademy)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                        }
                        break;
                    case PlanetaryFacilityType.NavalAcademy:
                        if (!Policy.ColonyAllowFacilityNavalAcademy)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                        }
                        break;
                    case PlanetaryFacilityType.MilitaryAcademy:
                        if (!Policy.ColonyAllowFacilityMilitaryAcademy)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                        }
                        break;
                    case PlanetaryFacilityType.CloningFacility:
                        if (!Policy.ColonyAllowFacilityCloningFacility)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                        }
                        break;
                    case PlanetaryFacilityType.FortifiedBunker:
                        if (!Policy.ColonyAllowFacilityFortifiedBunker)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                        }
                        break;
                    case PlanetaryFacilityType.IonCannon:
                        if (!Policy.ColonyAllowFacilityGiantIonCannon)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                        }
                        break;
                    case PlanetaryFacilityType.PlanetaryShield:
                        if (!Policy.ColonyAllowFacilityPlanetaryShield)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                        }
                        break;
                    case PlanetaryFacilityType.RegionalCapital:
                        if (!Policy.ColonyAllowFacilityRegionalCapital)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                        }
                        break;
                    case PlanetaryFacilityType.RoboticTroopFoundry:
                        if (!Policy.ColonyAllowFacilityRoboticTroopFoundry)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                        }
                        break;
                    case PlanetaryFacilityType.TroopTrainingCenter:
                        if (!Policy.ColonyAllowFacilityTroopTrainingCenter)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                        }
                        break;
                    case PlanetaryFacilityType.TerraformingFacility:
                        if (!Policy.ColonyAllowFacilityTerraformingFacility)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                        }
                        break;
                }
            }
            for (int j = 0; j < planetaryFacilityDefinitionList2.Count; j++)
            {
                planetaryFacilityDefinitionList.Remove(planetaryFacilityDefinitionList2[j]);
            }
            int refusalCount = 0;
            double annualEmpireExpenses = 0.0;
            double num = CalculateAccurateAnnualCashflowIncludingUnderConstruction(out annualEmpireExpenses);
            HabitatList habitatList = IdentifyEmpireRegionalCapitals(includeUnderConstruction: true);
            PlanetaryFacilityDefinition planetaryFacilityDefinition = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.RegionalCapital);
            int num2 = planetaryFacilityDefinitionList.CountByType(PlanetaryFacilityType.RegionalCapital);
            double num3 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition, this);
            if (habitatList.Count < num2 && planetaryFacilityDefinition != null && StateMoney >= num3 && num > planetaryFacilityDefinition.Maintenance)
            {
                habitatList.Add(Capital);
                HabitatList habitatList2 = new HabitatList();
                for (int k = 0; k < Colonies.Count; k++)
                {
                    if (!habitatList.Contains(Colonies[k]) && Colonies[k].Population != null && Colonies[k].Population.TotalAmount >= (long)Policy.ColonyFacilityPopulationThresholdRegionalCapital * 1000000L)
                    {
                        habitatList2.Add(Colonies[k]);
                    }
                }
                habitatList2.Sort();
                habitatList2.Reverse();
                Habitat habitat = null;
                double num4 = (double)Galaxy.SectorSize * 0.7;
                for (int l = 0; l < habitatList2.Count && habitatList2[l].StrategicValue >= 100000; l++)
                {
                    bool flag = true;
                    for (int m = 0; m < habitatList.Count; m++)
                    {
                        double num5 = _Galaxy.CalculateDistance(habitatList2[l].Xpos, habitatList2[l].Ypos, habitatList[m].Xpos, habitatList[m].Ypos);
                        if (num5 < num4)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        habitat = habitatList2[l];
                        break;
                    }
                }
                if (habitat != null && StateMoney >= num3 && CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitat, planetaryFacilityDefinition), habitat, AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition, null) && habitat.QueueFacilityConstruction(PlanetaryFacilityType.RegionalCapital))
                {
                    StateMoney -= num3;
                    num -= planetaryFacilityDefinition.Maintenance;
                }
            }
            PlanetaryFacilityDefinition planetaryFacilityDefinition2 = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.TerraformingFacility);
            if (planetaryFacilityDefinition2 != null)
            {
                double num6 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition2, this);
                for (int n = 0; n < Colonies.Count; n++)
                {
                    Habitat habitat2 = Colonies[n];
                    if (habitat2 != null && !habitat2.HasBeenDestroyed && habitat2.Population != null)
                    {
                        long totalAmount = habitat2.Population.TotalAmount;
                        if (totalAmount >= (long)Policy.ColonyFacilityPopulationThresholdTerraformingFacility * 1000000L && StateMoney > num6 * 2.5 && num > planetaryFacilityDefinition2.Maintenance && habitat2.Damage > 0f && habitat2.Facilities.CountByType(PlanetaryFacilityType.TerraformingFacility) <= 0 && CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitat2, planetaryFacilityDefinition2), habitat2, AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition2, null) && habitat2.QueueFacilityConstruction(PlanetaryFacilityType.TerraformingFacility))
                        {
                            StateMoney -= num6;
                            num -= planetaryFacilityDefinition2.Maintenance;
                        }
                    }
                }
            }
            HabitatList habitatList3 = new HabitatList();
            habitatList3.AddRange(Colonies);
            habitatList3.Sort();
            habitatList3.Reverse();
            PlanetaryFacilityDefinition planetaryFacilityDefinition3 = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.PlanetaryShield);
            PlanetaryFacilityDefinition planetaryFacilityDefinition4 = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.IonCannon);
            PlanetaryFacilityDefinition planetaryFacilityDefinition5 = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.SpyAcademy);
            PlanetaryFacilityDefinition planetaryFacilityDefinition6 = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.ScienceAcademy);
            PlanetaryFacilityDefinition planetaryFacilityDefinition7 = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.NavalAcademy);
            double num7 = 0.0;
            double num8 = 0.0;
            if (planetaryFacilityDefinition5 != null)
            {
                num7 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition5, this);
                num8 = planetaryFacilityDefinition5.Maintenance;
            }
            else if (planetaryFacilityDefinition6 != null)
            {
                num7 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition6, this);
                num8 = planetaryFacilityDefinition6.Maintenance;
            }
            else if (planetaryFacilityDefinition7 != null)
            {
                num7 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition7, this);
                num8 = planetaryFacilityDefinition7.Maintenance;
            }
            else if (planetaryFacilityDefinition3 != null)
            {
                num7 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition3, this);
                num8 = planetaryFacilityDefinition3.Maintenance;
            }
            else if (planetaryFacilityDefinition4 != null)
            {
                num7 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition4, this);
                num8 = planetaryFacilityDefinition4.Maintenance;
            }
            if (num7 > 0.0)
            {
                int num9 = 1;
                int num10 = 1;
                int num11 = 1;
                num10 = ((!(Policy.ResearchPriority < 1.0)) ? ((Policy.ResearchPriority < 1.5) ? 1 : ((!(Policy.ResearchPriority < 2.0)) ? 3 : 2)) : 0);
                switch (Policy.ConstructionMilitary)
                {
                    case 0:
                        num11 = 0;
                        break;
                    case 1:
                        num11 = 1;
                        break;
                    case 2:
                        num11 = 2;
                        break;
                }
                if (DominantRace != null && DominantRace.EspionageBonus > 0)
                {
                    num9 = 2;
                }
                int num12 = CountFacilities(PlanetaryFacilityType.SpyAcademy);
                int num13 = CountFacilities(PlanetaryFacilityType.ScienceAcademy);
                int num14 = CountFacilities(PlanetaryFacilityType.NavalAcademy);
                for (int num15 = 0; num15 < habitatList3.Count; num15++)
                {
                    if (StateMoney > num7 * 2.5 && num > num8 && habitatList3[num15].StrategicValue > 200000)
                    {
                        int num16 = 0;
                        int num17 = 0;
                        int num18 = 0;
                        int num19 = 0;
                        int num20 = 0;
                        if (habitatList3[num15].Facilities != null)
                        {
                            num16 = habitatList3[num15].Facilities.CountByType(PlanetaryFacilityType.PlanetaryShield);
                            num17 = habitatList3[num15].Facilities.CountByType(PlanetaryFacilityType.IonCannon);
                            num18 = habitatList3[num15].Facilities.CountByType(PlanetaryFacilityType.SpyAcademy);
                            num19 = habitatList3[num15].Facilities.CountByType(PlanetaryFacilityType.ScienceAcademy);
                            num20 = habitatList3[num15].Facilities.CountByType(PlanetaryFacilityType.NavalAcademy);
                        }
                        if (planetaryFacilityDefinition3 != null && num16 <= 0 && StateMoney > Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition3, this) && num > planetaryFacilityDefinition3.Maintenance && habitatList3[num15].Population != null && habitatList3[num15].Population.TotalAmount >= (long)Policy.ColonyFacilityPopulationThresholdPlanetaryShield * 1000000L && CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitatList3[num15], planetaryFacilityDefinition3), habitatList3[num15], AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition3, null) && habitatList3[num15].QueueFacilityConstruction(PlanetaryFacilityType.PlanetaryShield))
                        {
                            StateMoney -= Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition3, this);
                            num -= planetaryFacilityDefinition3.Maintenance;
                        }
                        if (planetaryFacilityDefinition4 != null && num17 <= 0 && StateMoney > Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition4, this) && num > planetaryFacilityDefinition4.Maintenance && habitatList3[num15].Population != null && habitatList3[num15].Population.TotalAmount >= (long)Policy.ColonyFacilityPopulationThresholdGiantIonCannon * 1000000L && CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitatList3[num15], planetaryFacilityDefinition4), habitatList3[num15], AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition4, null) && habitatList3[num15].QueueFacilityConstruction(PlanetaryFacilityType.IonCannon))
                        {
                            StateMoney -= Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition4, this);
                            num -= planetaryFacilityDefinition4.Maintenance;
                        }
                        if (planetaryFacilityDefinition5 != null && num18 <= 0 && num12 < num9 && StateMoney > Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition5, this) && num > planetaryFacilityDefinition5.Maintenance && habitatList3[num15].Population != null && habitatList3[num15].Population.TotalAmount >= (long)Policy.ColonyFacilityPopulationThresholdSpyAcademy * 1000000L && CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitatList3[num15], planetaryFacilityDefinition5), habitatList3[num15], AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition5, null) && habitatList3[num15].QueueFacilityConstruction(PlanetaryFacilityType.SpyAcademy))
                        {
                            StateMoney -= Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition5, this);
                            num -= planetaryFacilityDefinition5.Maintenance;
                            num12++;
                        }
                        if (planetaryFacilityDefinition6 != null && num19 <= 0 && num13 < num10 && StateMoney > Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition6, this) && num > planetaryFacilityDefinition6.Maintenance && habitatList3[num15].Population != null && habitatList3[num15].Population.TotalAmount >= (long)Policy.ColonyFacilityPopulationThresholdScienceAcademy * 1000000L && CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitatList3[num15], planetaryFacilityDefinition6), habitatList3[num15], AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition6, null) && habitatList3[num15].QueueFacilityConstruction(PlanetaryFacilityType.ScienceAcademy))
                        {
                            StateMoney -= Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition6, this);
                            num -= planetaryFacilityDefinition6.Maintenance;
                            num13++;
                        }
                        if (planetaryFacilityDefinition7 != null && num20 <= 0 && num14 < num11 && StateMoney > Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition7, this) && num > planetaryFacilityDefinition7.Maintenance && habitatList3[num15].Population != null && habitatList3[num15].Population.TotalAmount >= (long)Policy.ColonyFacilityPopulationThresholdNavalAcademy * 1000000L && CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitatList3[num15], planetaryFacilityDefinition7), habitatList3[num15], AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition7, null) && habitatList3[num15].QueueFacilityConstruction(PlanetaryFacilityType.NavalAcademy))
                        {
                            StateMoney -= Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition7, this);
                            num -= planetaryFacilityDefinition7.Maintenance;
                            num14++;
                        }
                    }
                }
            }
            PlanetaryFacilityDefinition planetaryFacilityDefinition8 = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.FortifiedBunker);
            if (planetaryFacilityDefinition8 != null)
            {
                StellarObjectList stellarObjectList = ResolveLocationsToDefend(includeBases: false);
                for (int num21 = 0; num21 < Colonies.Count; num21++)
                {
                    if (!(StateMoney > Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition8, this)) || !(num > planetaryFacilityDefinition8.Maintenance))
                    {
                        continue;
                    }
                    int num22 = 0;
                    if (Colonies[num21].Facilities != null)
                    {
                        num22 = Colonies[num21].Facilities.CountByType(PlanetaryFacilityType.FortifiedBunker);
                    }
                    if (num22 > 0)
                    {
                        continue;
                    }
                    if (stellarObjectList.Contains(Colonies[num21]) && Colonies[num21].Population != null && Colonies[num21].Population.TotalAmount >= (long)Policy.ColonyFacilityPopulationThresholdFortifiedBunker * 1000000L)
                    {
                        if (CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(Colonies[num21], planetaryFacilityDefinition8), Colonies[num21], AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition8, null) && Colonies[num21].QueueFacilityConstruction(PlanetaryFacilityType.FortifiedBunker))
                        {
                            StateMoney -= Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition8, this);
                            num -= planetaryFacilityDefinition8.Maintenance;
                        }
                    }
                    else if (_Galaxy.DetermineSpacePortAtColony(Colonies[num21]) != null && Galaxy.Rnd.Next(0, 2) == 1 && Colonies[num21].Population != null && Colonies[num21].Population.TotalAmount >= (long)Policy.ColonyFacilityPopulationThresholdFortifiedBunker * 1000000L && CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(Colonies[num21], planetaryFacilityDefinition8), Colonies[num21], AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition8, null) && Colonies[num21].QueueFacilityConstruction(PlanetaryFacilityType.FortifiedBunker))
                    {
                        StateMoney -= Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition8, this);
                        num -= planetaryFacilityDefinition8.Maintenance;
                    }
                }
            }
            PlanetaryFacilityDefinition planetaryFacilityDefinition9 = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.TroopTrainingCenter);
            PlanetaryFacilityDefinition planetaryFacilityDefinition10 = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.RoboticTroopFoundry);
            PlanetaryFacilityDefinition planetaryFacilityDefinition11 = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.CloningFacility);
            PlanetaryFacilityDefinition planetaryFacilityDefinition12 = null;
            int num23 = ResolveEmpireRaceTendency(DominantRace);
            double num24 = DominantRace.TroopStrength;
            long num25 = 0L;
            if (num23 == 2 || num24 < 100.0)
            {
                planetaryFacilityDefinition12 = planetaryFacilityDefinition10;
                num25 = (long)Policy.ColonyFacilityPopulationThresholdRoboticTroopFoundry * 1000000L;
            }
            else if (num23 == 1 || num24 > 125.0)
            {
                if (planetaryFacilityDefinition11 != null)
                {
                    planetaryFacilityDefinition12 = planetaryFacilityDefinition11;
                    num25 = (long)Policy.ColonyFacilityPopulationThresholdCloningFacility * 1000000L;
                }
                else
                {
                    planetaryFacilityDefinition12 = planetaryFacilityDefinition9;
                    num25 = (long)Policy.ColonyFacilityPopulationThresholdTroopTrainingCenter * 1000000L;
                }
            }
            else if (num23 == 3)
            {
                planetaryFacilityDefinition12 = planetaryFacilityDefinition9;
                num25 = (long)Policy.ColonyFacilityPopulationThresholdTroopTrainingCenter * 1000000L;
            }
            else
            {
                planetaryFacilityDefinition12 = planetaryFacilityDefinition9;
                num25 = (long)Policy.ColonyFacilityPopulationThresholdTroopTrainingCenter * 1000000L;
            }
            num7 = 0.0;
            num8 = 0.0;
            if (planetaryFacilityDefinition12 != null)
            {
                num7 = Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition12, this);
                num8 = planetaryFacilityDefinition12.Maintenance;
            }
            HabitatList habitatList4 = DetermineTroopFacilityColonies();
            int num26 = habitatList4.Count;
            int num27 = Math.Max(1, Colonies.Count / 7);
            if (num26 < num27 && num7 > 0.0 && StateMoney > num7 && num > num8)
            {
                for (int num28 = 0; num28 < Colonies.Count; num28++)
                {
                    if (Colonies[num28].Population != null && Colonies[num28].Population.TotalAmount > num25 && Colonies[num28].EmpireApprovalRating >= 10.0)
                    {
                        int num29 = 0;
                        if (Colonies[num28].Facilities != null)
                        {
                            num29 = Math.Max(num29, Colonies[num28].Facilities.CountByType(PlanetaryFacilityType.CloningFacility));
                            num29 = Math.Max(num29, Colonies[num28].Facilities.CountByType(PlanetaryFacilityType.RoboticTroopFoundry));
                            num29 = Math.Max(num29, Colonies[num28].Facilities.CountByType(PlanetaryFacilityType.TroopTrainingCenter));
                        }
                        if (num29 <= 0 && StateMoney > num7 && num > num8 && CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(Colonies[num28], planetaryFacilityDefinition12), Colonies[num28], AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition12, null) && Colonies[num28].QueueFacilityConstruction(planetaryFacilityDefinition12.Type))
                        {
                            StateMoney -= num7;
                            num -= num8;
                            num26++;
                        }
                    }
                }
            }
            PlanetaryFacilityDefinition planetaryFacilityDefinition13 = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.ArmoredFactory);
            HabitatList habitatList5 = DetermineFacilityColonies(PlanetaryFacilityType.ArmoredFactory);
            int num30 = habitatList5.Count;
            int num31 = Math.Max(1, Colonies.Count / 8);
            num31 = ((Policy.TroopRecruitArmorLevel < 1.0) ? Math.Max(1, (int)((double)num31 * 0.5)) : ((!(Policy.TroopRecruitArmorLevel < 1.5)) ? Math.Max(1, (int)((double)num31 * 1.5)) : Math.Max(1, (int)((double)num31 * 1.0))));
            if (planetaryFacilityDefinition13 != null && num30 < num31 && Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition13, this) > 0.0 && StateMoney > Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition13, this) && num > planetaryFacilityDefinition13.Maintenance)
            {
                num25 = (long)Policy.ColonyFacilityPopulationThresholdArmoredFactory * 1000000L;
                for (int num32 = 0; num32 < habitatList3.Count; num32++)
                {
                    Habitat habitat3 = habitatList3[num32];
                    if (habitat3.Population != null && habitat3.Population.TotalAmount > num25 && num30 < num31)
                    {
                        int num33 = 0;
                        if (habitat3.Facilities != null)
                        {
                            num33 = habitat3.Facilities.CountByType(PlanetaryFacilityType.ArmoredFactory);
                        }
                        if (num33 <= 0 && StateMoney > Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition13, this) && num > planetaryFacilityDefinition13.Maintenance && CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitat3, planetaryFacilityDefinition13), habitat3, AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition13, null) && habitat3.QueueFacilityConstruction(planetaryFacilityDefinition13.Type))
                        {
                            StateMoney -= Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition13, this);
                            num -= planetaryFacilityDefinition13.Maintenance;
                            num30++;
                        }
                    }
                }
            }
            PlanetaryFacilityDefinition planetaryFacilityDefinition14 = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.MilitaryAcademy);
            HabitatList habitatList6 = DetermineFacilityColonies(PlanetaryFacilityType.MilitaryAcademy);
            int num34 = habitatList6.Count;
            int num35 = 1;
            num35 = ((!(Policy.TroopGarrisonLevel < 0.5)) ? ((Policy.TroopGarrisonLevel < 1.0) ? 1 : ((!(Policy.TroopGarrisonLevel < 1.5)) ? 3 : 2)) : 0);
            if (planetaryFacilityDefinition14 == null || num34 >= num35 || !(Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition14, this) > 0.0) || !(StateMoney > Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition14, this)) || !(num > planetaryFacilityDefinition14.Maintenance))
            {
                return;
            }
            num25 = (long)Policy.ColonyFacilityPopulationThresholdMilitaryAcademy * 1000000L;
            for (int num36 = 0; num36 < habitatList3.Count; num36++)
            {
                Habitat habitat4 = habitatList3[num36];
                if (habitat4.Population != null && habitat4.Population.TotalAmount > num25 && num34 < num35)
                {
                    int num37 = 0;
                    if (habitat4.Facilities != null)
                    {
                        num37 = habitat4.Facilities.CountByType(PlanetaryFacilityType.MilitaryAcademy);
                    }
                    if (num37 <= 0 && StateMoney > Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition14, this) && num > planetaryFacilityDefinition14.Maintenance && CheckTaskAuthorized(_ControlColonyFacilities, ref refusalCount, GenerateAutomationMessageColonyFacility(habitat4, planetaryFacilityDefinition14), habitat4, AdvisorMessageType.ColonyFacility, planetaryFacilityDefinition14, null) && habitat4.QueueFacilityConstruction(planetaryFacilityDefinition14.Type))
                    {
                        StateMoney -= Galaxy.CalculatePlanetaryFacilityCost(planetaryFacilityDefinition14, this);
                        num -= planetaryFacilityDefinition14.Maintenance;
                        num34++;
                    }
                }
            }
        }

        public int CumulateFacilityValue1(PlanetaryFacilityType facilityType)
        {
            return CumulateFacilityValue1(facilityType, mustBeCompleted: false);
        }

        public int CumulateFacilityValue1(PlanetaryFacilityType facilityType, bool mustBeCompleted)
        {
            int num = 0;
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat != null && habitat.Facilities != null)
                {
                    num = ((!mustBeCompleted) ? (num + habitat.Facilities.CumulateValue1ByType(facilityType)) : (num + habitat.Facilities.CumulateValue1ByTypeCompleted(facilityType)));
                }
            }
            return num;
        }

        public int CountFacilities(PlanetaryFacilityType facilityType)
        {
            return CountFacilities(facilityType, mustBeCompleted: false);
        }

        public int CountFacilities(PlanetaryFacilityType facilityType, bool mustBeCompleted)
        {
            int num = 0;
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat != null && habitat.Facilities != null)
                {
                    num = ((!mustBeCompleted) ? (num + habitat.Facilities.CountByType(facilityType)) : (num + habitat.Facilities.CountCompletedByType(facilityType)));
                }
            }
            return num;
        }

        public long CalculatePirateControlPopulationValue()
        {
            long num = 0L;
            if (PirateEmpireBaseHabitat != null)
            {
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat = Colonies[i];
                    if (habitat == null || habitat.HasBeenDestroyed || habitat.Population == null)
                    {
                        continue;
                    }
                    if (habitat.Empire == this)
                    {
                        num += habitat.Population.TotalAmount;
                        continue;
                    }
                    PirateColonyControl byFaction = habitat.GetPirateControl().GetByFaction(this);
                    if (byFaction != null)
                    {
                        long num2 = (long)(byFaction.ControlLevel * (float)habitat.Population.TotalAmount);
                        num += num2;
                    }
                }
            }
            return num;
        }

        public HabitatList DetermineFacilityColonies(PlanetaryFacilityType facilityType)
        {
            HabitatList habitatList = new HabitatList();
            for (int i = 0; i < Colonies.Count; i++)
            {
                int num = 0;
                if (Colonies[i].Facilities != null)
                {
                    num = Math.Max(num, Colonies[i].Facilities.CountByType(facilityType));
                }
                if (num > 0)
                {
                    habitatList.Add(Colonies[i]);
                }
            }
            return habitatList;
        }

        public HabitatList DetermineTroopFacilityColonies()
        {
            HabitatList habitatList = new HabitatList();
            for (int i = 0; i < Colonies.Count; i++)
            {
                int num = 0;
                if (Colonies[i].Facilities != null)
                {
                    num = Math.Max(num, Colonies[i].Facilities.CountByType(PlanetaryFacilityType.CloningFacility));
                    num = Math.Max(num, Colonies[i].Facilities.CountByType(PlanetaryFacilityType.RoboticTroopFoundry));
                    num = Math.Max(num, Colonies[i].Facilities.CountByType(PlanetaryFacilityType.TroopTrainingCenter));
                }
                if (num > 0)
                {
                    habitatList.Add(Colonies[i]);
                }
            }
            return habitatList;
        }

        private EmpireList DetermineUnfriendlyEmpires(int attitudeThreshold)
        {
            EmpireList empireList = new EmpireList();
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                if (DiplomaticRelations[i].Type != 0)
                {
                    EmpireEvaluation empireEvaluation = DiplomaticRelations[i].OtherEmpire.ObtainEmpireEvaluation(this);
                    if (empireEvaluation.OverallAttitude <= attitudeThreshold)
                    {
                        empireList.Add(DiplomaticRelations[i].OtherEmpire);
                    }
                }
            }
            return empireList;
        }

        public void ReviewSpecialBonusesRuinsWonders()
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            Ruin specialBonusResearchEnergyRuin = null;
            Ruin specialBonusResearchHighTechRuin = null;
            Ruin specialBonusResearchWeaponsRuin = null;
            Ruin specialBonusWealthRuin = null;
            Ruin specialBonusHappinessRuin = null;
            Ruin specialBonusDiplomacyRuin = null;
            PlanetaryFacility specialBonusResearchEnergyWonder = null;
            PlanetaryFacility specialBonusResearchHighTechWonder = null;
            PlanetaryFacility specialBonusResearchWeaponsWonder = null;
            PlanetaryFacility specialBonusWealthWonder = null;
            PlanetaryFacility specialBonusHappinessWonder = null;
            PlanetaryFacility specialBonusPopulationGrowthWonder = null;
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat.Ruin != null)
                {
                    if (habitat.Ruin.BonusDiplomacy > num6)
                    {
                        num6 = habitat.Ruin.BonusDiplomacy;
                        specialBonusDiplomacyRuin = habitat.Ruin;
                    }
                    if (habitat.Ruin.BonusWealth > num4)
                    {
                        num4 = habitat.Ruin.BonusWealth;
                        specialBonusWealthRuin = habitat.Ruin;
                        specialBonusWealthWonder = null;
                    }
                    if (habitat.Ruin.BonusHappiness > num5)
                    {
                        num5 = habitat.Ruin.BonusHappiness;
                        specialBonusHappinessRuin = habitat.Ruin;
                        specialBonusHappinessWonder = null;
                    }
                    if (habitat.Ruin.BonusResearchWeapons > num3)
                    {
                        num3 = habitat.Ruin.BonusResearchWeapons;
                        specialBonusResearchWeaponsRuin = habitat.Ruin;
                        specialBonusResearchWeaponsWonder = null;
                    }
                    if (habitat.Ruin.BonusResearchEnergy > num)
                    {
                        num = habitat.Ruin.BonusResearchEnergy;
                        specialBonusResearchEnergyRuin = habitat.Ruin;
                        specialBonusResearchEnergyWonder = null;
                    }
                    if (habitat.Ruin.BonusResearchHighTech > num2)
                    {
                        num2 = habitat.Ruin.BonusResearchHighTech;
                        specialBonusResearchHighTechRuin = habitat.Ruin;
                        specialBonusResearchHighTechWonder = null;
                    }
                }
                if (habitat.Facilities == null || habitat.Facilities.Count <= 0)
                {
                    continue;
                }
                for (int j = 0; j < habitat.Facilities.Count; j++)
                {
                    PlanetaryFacility planetaryFacility = habitat.Facilities[j];
                    if (planetaryFacility == null || planetaryFacility.Type != PlanetaryFacilityType.Wonder || !(planetaryFacility.ConstructionProgress >= 1f))
                    {
                        continue;
                    }
                    switch (planetaryFacility.WonderType)
                    {
                        case WonderType.EmpireResearchWeapons:
                            {
                                double num14 = (double)planetaryFacility.Value2 / 100.0;
                                if (num14 > num3)
                                {
                                    num3 = num14;
                                    specialBonusResearchWeaponsWonder = planetaryFacility;
                                    specialBonusResearchWeaponsRuin = null;
                                }
                                break;
                            }
                        case WonderType.EmpireResearchEnergy:
                            {
                                double num10 = (double)planetaryFacility.Value2 / 100.0;
                                if (num10 > num)
                                {
                                    num = num10;
                                    specialBonusResearchEnergyWonder = planetaryFacility;
                                    specialBonusResearchEnergyRuin = null;
                                }
                                break;
                            }
                        case WonderType.EmpireResearchHighTech:
                            {
                                double num12 = (double)planetaryFacility.Value2 / 100.0;
                                if (num12 > num2)
                                {
                                    num2 = num12;
                                    specialBonusResearchHighTechWonder = planetaryFacility;
                                    specialBonusResearchHighTechRuin = null;
                                }
                                break;
                            }
                        case WonderType.EmpireHappiness:
                            {
                                double num9 = (double)planetaryFacility.Value2 / 100.0;
                                if (num9 > num5)
                                {
                                    num5 = num9;
                                    specialBonusHappinessWonder = planetaryFacility;
                                    specialBonusHappinessRuin = null;
                                }
                                break;
                            }
                        case WonderType.EmpireIncome:
                            {
                                double num13 = (double)planetaryFacility.Value2 / 100.0;
                                if (num13 > num4)
                                {
                                    num4 = num13;
                                    specialBonusWealthWonder = planetaryFacility;
                                    specialBonusWealthRuin = null;
                                }
                                break;
                            }
                        case WonderType.EmpirePopulationGrowth:
                            {
                                double num11 = (double)planetaryFacility.Value2 / 100.0;
                                if (num11 > num7)
                                {
                                    num7 = num11;
                                    specialBonusPopulationGrowthWonder = planetaryFacility;
                                }
                                break;
                            }
                        case WonderType.RaceAchievement:
                            if (planetaryFacility.Value2 == 1)
                            {
                                double num8 = 0.75;
                                if (num8 > num2)
                                {
                                    num2 = num8;
                                    specialBonusResearchHighTechWonder = planetaryFacility;
                                    specialBonusResearchHighTechRuin = null;
                                }
                            }
                            break;
                    }
                }
            }
            _SpecialBonusResearchEnergy = num;
            _SpecialBonusResearchHighTech = num2;
            _SpecialBonusResearchWeapons = num3;
            _SpecialBonusWealth = num4;
            _SpecialBonusHappiness = num5;
            _SpecialBonusDiplomacy = num6;
            _SpecialBonusPopulationGrowth = num7;
            _SpecialBonusResearchEnergyRuin = specialBonusResearchEnergyRuin;
            _SpecialBonusResearchHighTechRuin = specialBonusResearchHighTechRuin;
            _SpecialBonusResearchWeaponsRuin = specialBonusResearchWeaponsRuin;
            _SpecialBonusWealthRuin = specialBonusWealthRuin;
            _SpecialBonusHappinessRuin = specialBonusHappinessRuin;
            _SpecialBonusDiplomacyRuin = specialBonusDiplomacyRuin;
            _SpecialBonusHappinessWonder = specialBonusHappinessWonder;
            _SpecialBonusPopulationGrowthWonder = specialBonusPopulationGrowthWonder;
            _SpecialBonusResearchEnergyWonder = specialBonusResearchEnergyWonder;
            _SpecialBonusResearchHighTechWonder = specialBonusResearchHighTechWonder;
            _SpecialBonusResearchWeaponsWonder = specialBonusResearchWeaponsWonder;
            _SpecialBonusWealthWonder = specialBonusWealthWonder;
        }

        public string SummarizeSpecialResearchBonuses()
        {
            string text = string.Empty;
            int num = 0;
            if (Leader != null)
            {
                if (Leader.ResearchEnergy > 0)
                {
                    text = text + string.Format(TextResolver.GetText("Research Leader Bonus Energy Increase"), Leader.Name, ((double)Leader.ResearchEnergy / 100.0).ToString("+0%")) + "\n";
                }
                else if (Leader.ResearchEnergy < 0)
                {
                    text = text + string.Format(TextResolver.GetText("Research Leader Bonus Energy Decrease"), Leader.Name, ((double)Leader.ResearchEnergy / 100.0).ToString("-0%")) + "\n";
                }
                if (Leader.ResearchHighTech > 0)
                {
                    text = text + string.Format(TextResolver.GetText("Research Leader Bonus HighTech Increase"), Leader.Name, ((double)Leader.ResearchHighTech / 100.0).ToString("+0%")) + "\n";
                }
                else if (Leader.ResearchHighTech < 0)
                {
                    text = text + string.Format(TextResolver.GetText("Research Leader Bonus HighTech Decrease"), Leader.Name, ((double)Leader.ResearchHighTech / 100.0).ToString("-0%")) + "\n";
                }
                if (Leader.ResearchWeapons > 0)
                {
                    text = text + string.Format(TextResolver.GetText("Research Leader Bonus Weapons Increase"), Leader.Name, ((double)Leader.ResearchWeapons / 100.0).ToString("+0%")) + "\n";
                }
                else if (Leader.ResearchWeapons < 0)
                {
                    text = text + string.Format(TextResolver.GetText("Research Leader Bonus Weapons Decrease"), Leader.Name, ((double)Leader.ResearchWeapons / 100.0).ToString("-0%")) + "\n";
                }
            }
            if (Characters != null && Characters.CheckCharactersForTrait(CharacterRole.Scientist, CharacterTraitType.UltraGenius))
            {
                Character firstCharacterWithTrait = Characters.GetFirstCharacterWithTrait(CharacterTraitType.UltraGenius);
                string arg = string.Empty;
                if (firstCharacterWithTrait != null)
                {
                    arg = firstCharacterWithTrait.Name;
                }
                text = text + string.Format(TextResolver.GetText("Research UltraGenius Scientist Bonus"), arg, "+20%") + "\n";
            }
            if (ResearchBonusWeapons > 0f && ResearchBonusWeaponsStation != null)
            {
                text = text + string.Format(TextResolver.GetText("Weapons Research Bonus from Station"), ResearchBonusWeapons.ToString("+#%"), ResearchBonusWeaponsStation.Name) + "\n";
                CharacterList characterList = new CharacterList();
                if (ResearchBonusWeaponsStation.Characters != null)
                {
                    characterList = ResearchBonusWeaponsStation.Characters.GetNonTransferringCharacters(CharacterRole.Scientist);
                }
                float num2 = (float)characterList.TotalDiminishingResearchBonusesWeapons();
                float num3 = 0f;
                if (ResearchBonusWeaponsStation.ParentHabitat != null && ResearchBonusWeaponsStation.ParentHabitat.ResearchBonusIndustry == IndustryType.Weapon && ResearchBonusWeaponsStation.ParentHabitat.ResearchBonus > 0)
                {
                    num3 = (float)(int)ResearchBonusWeaponsStation.ParentHabitat.ResearchBonus / 100f;
                }
                else if (ResearchBonusWeaponsStation.NearestSystemStar != null && ResearchBonusWeaponsStation.NearestSystemStar.ResearchBonusIndustry == IndustryType.Weapon && ResearchBonusWeaponsStation.NearestSystemStar.ResearchBonus > 0)
                {
                    num3 = (float)(int)ResearchBonusWeaponsStation.NearestSystemStar.ResearchBonus / 100f;
                }
                text = text + "    (" + string.Format(TextResolver.GetText("Research Bonus Breakdown"), num3.ToString("+0%"), num2.ToString("+0%")) + ")";
                text += "\n";
            }
            if (ResearchBonusEnergy > 0f && ResearchBonusEnergyStation != null)
            {
                text = text + string.Format(TextResolver.GetText("Energy Research Bonus from Station"), ResearchBonusEnergy.ToString("+#%"), ResearchBonusEnergyStation.Name) + "\n";
                CharacterList characterList2 = new CharacterList();
                if (ResearchBonusEnergyStation.Characters != null)
                {
                    characterList2 = ResearchBonusEnergyStation.Characters.GetNonTransferringCharacters(CharacterRole.Scientist);
                }
                float num4 = (float)characterList2.TotalDiminishingResearchBonusesEnergy();
                float num5 = 0f;
                if (ResearchBonusEnergyStation.ParentHabitat != null && ResearchBonusEnergyStation.ParentHabitat.ResearchBonusIndustry == IndustryType.Energy && ResearchBonusEnergyStation.ParentHabitat.ResearchBonus > 0)
                {
                    num5 = (float)(int)ResearchBonusEnergyStation.ParentHabitat.ResearchBonus / 100f;
                }
                else if (ResearchBonusEnergyStation.NearestSystemStar != null && ResearchBonusEnergyStation.NearestSystemStar.ResearchBonusIndustry == IndustryType.Energy && ResearchBonusEnergyStation.NearestSystemStar.ResearchBonus > 0)
                {
                    num5 = (float)(int)ResearchBonusEnergyStation.NearestSystemStar.ResearchBonus / 100f;
                }
                text = text + "    (" + string.Format(TextResolver.GetText("Research Bonus Breakdown"), num5.ToString("+0%"), num4.ToString("+0%")) + ")";
                text += "\n";
            }
            if (ResearchBonusHighTech > 0f && ResearchBonusHighTechStation != null)
            {
                text = text + string.Format(TextResolver.GetText("HighTech Research Bonus from Station"), ResearchBonusHighTech.ToString("+#%"), ResearchBonusHighTechStation.Name) + "\n";
                CharacterList characterList3 = new CharacterList();
                if (ResearchBonusHighTechStation.Characters != null)
                {
                    characterList3 = ResearchBonusHighTechStation.Characters.GetNonTransferringCharacters(CharacterRole.Scientist);
                }
                float num6 = (float)characterList3.TotalDiminishingResearchBonusesHighTech();
                float num7 = 0f;
                if (ResearchBonusHighTechStation.ParentHabitat != null && ResearchBonusHighTechStation.ParentHabitat.ResearchBonusIndustry == IndustryType.HighTech && ResearchBonusHighTechStation.ParentHabitat.ResearchBonus > 0)
                {
                    num7 = (float)(int)ResearchBonusHighTechStation.ParentHabitat.ResearchBonus / 100f;
                }
                else if (ResearchBonusHighTechStation.NearestSystemStar != null && ResearchBonusHighTechStation.NearestSystemStar.ResearchBonusIndustry == IndustryType.HighTech && ResearchBonusHighTechStation.NearestSystemStar.ResearchBonus > 0)
                {
                    num7 = (float)(int)ResearchBonusHighTechStation.NearestSystemStar.ResearchBonus / 100f;
                }
                text = text + "    (" + string.Format(TextResolver.GetText("Research Bonus Breakdown"), num7.ToString("+0%"), num6.ToString("+0%")) + ")";
                text += "\n";
            }
            if (_SpecialBonusResearchEnergy > 0.0)
            {
                if (_SpecialBonusResearchEnergyRuin != null)
                {
                    text = text + string.Format(TextResolver.GetText("Energy Research Bonus from Ruin"), _SpecialBonusResearchEnergy.ToString("+#%"), _SpecialBonusResearchEnergyRuin.Name) + ", ";
                    num++;
                }
                else if (_SpecialBonusResearchEnergyWonder != null)
                {
                    text = text + string.Format(TextResolver.GetText("Energy Research Bonus from Ruin"), _SpecialBonusResearchEnergy.ToString("+#%"), _SpecialBonusResearchEnergyWonder.Name) + ", ";
                    num++;
                }
            }
            if (_SpecialBonusResearchWeapons > 0.0)
            {
                if (_SpecialBonusResearchWeaponsRuin != null)
                {
                    text = text + string.Format(TextResolver.GetText("Weapons Research Bonus from Ruin"), _SpecialBonusResearchWeapons.ToString("+#%"), _SpecialBonusResearchWeaponsRuin.Name) + ", ";
                    num++;
                }
                else if (_SpecialBonusResearchWeaponsWonder != null)
                {
                    text = text + string.Format(TextResolver.GetText("Weapons Research Bonus from Ruin"), _SpecialBonusResearchWeapons.ToString("+#%"), _SpecialBonusResearchWeaponsWonder.Name) + ", ";
                    num++;
                }
            }
            if (_SpecialBonusResearchHighTech > 0.0)
            {
                if (_SpecialBonusResearchHighTechRuin != null)
                {
                    text = text + string.Format(TextResolver.GetText("HighTech Research Bonus from Ruin"), _SpecialBonusResearchHighTech.ToString("+#%"), _SpecialBonusResearchHighTechRuin.Name) + ", ";
                    num++;
                }
                else if (_SpecialBonusResearchHighTechWonder != null)
                {
                    text = text + string.Format(TextResolver.GetText("HighTech Research Bonus from Ruin"), _SpecialBonusResearchHighTech.ToString("+#%"), _SpecialBonusResearchHighTechWonder.Name) + ", ";
                    num++;
                }
            }
            if (RaceEventType == RaceEventType.HistoricalDiscoveryExploreRuinsForResearchBoost)
            {
                text = text + string.Format(TextResolver.GetText("Research Bonus From Historical Discovery in Ruins"), "10%") + "\n";
                num++;
            }
            double num8 = 0.0;
            double num9 = 0.0;
            double num10 = 0.0;
            byte resourceId = byte.MaxValue;
            byte resourceId2 = byte.MaxValue;
            byte resourceId3 = byte.MaxValue;
            if (Colonies != null)
            {
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat = Colonies[i];
                    if (habitat.ResourceBonuses == null)
                    {
                        continue;
                    }
                    for (int j = 0; j < habitat.ResourceBonuses.Count; j++)
                    {
                        ResourceBonus resourceBonus = habitat.ResourceBonuses[j];
                        if (resourceBonus.Effect == ColonyResourceEffect.ResearchWeapons)
                        {
                            num8 += resourceBonus.Value / 100.0;
                            resourceId = resourceBonus.ResourceId;
                        }
                        else if (resourceBonus.Effect == ColonyResourceEffect.ResearchEnergy)
                        {
                            num9 += resourceBonus.Value / 100.0;
                            resourceId2 = resourceBonus.ResourceId;
                        }
                        else if (resourceBonus.Effect == ColonyResourceEffect.ResearchHighTech)
                        {
                            num10 += resourceBonus.Value / 100.0;
                            resourceId3 = resourceBonus.ResourceId;
                        }
                    }
                    num8 = Math.Min(1.0, num8);
                    num9 = Math.Min(1.0, num9);
                    num10 = Math.Min(1.0, num10);
                }
            }
            if (num8 > 0.0)
            {
                Resource resource = new Resource(resourceId);
                text = text + string.Format(TextResolver.GetText("Weapons Research Bonus From Colony Resources"), num8.ToString("+#%"), resource.Name) + " \n";
                num++;
            }
            if (num9 > 0.0)
            {
                Resource resource2 = new Resource(resourceId2);
                text = text + string.Format(TextResolver.GetText("Energy Research Bonus From Colony Resources"), num9.ToString("+#%"), resource2.Name) + " \n";
                num++;
            }
            if (num10 > 0.0)
            {
                Resource resource3 = new Resource(resourceId3);
                text = text + string.Format(TextResolver.GetText("HighTech Research Bonus From Colony Resources"), num10.ToString("+#%"), resource3.Name) + " \n";
                num++;
            }
            foreach (BuiltObject constructionShip in ConstructionShips)
            {
                if (constructionShip.ConstructionQueue == null)
                {
                    continue;
                }
                foreach (ConstructionYard constructionYard in constructionShip.ConstructionQueue.ConstructionYards)
                {
                    if (!((double)constructionYard.BuildSpeedModifier > 1.0) || constructionYard.ShipUnderConstruction == null)
                    {
                        continue;
                    }
                    if (constructionYard.ShipUnderConstruction.IsPlanetDestroyer)
                    {
                        text = text + string.Format(TextResolver.GetText("Ongoing bonus while repairing advanced tech"), constructionYard.ShipUnderConstruction.Name) + ", ";
                        num++;
                        continue;
                    }
                    bool flag = false;
                    GalaxyLocationList galaxyLocationList = _Galaxy.DetermineGalaxyLocationsAtPoint(constructionYard.ShipUnderConstruction.Xpos, constructionYard.ShipUnderConstruction.Ypos);
                    foreach (GalaxyLocation item in galaxyLocationList)
                    {
                        if (item.Type == GalaxyLocationType.DebrisField || item.Type == GalaxyLocationType.RestrictedArea)
                        {
                            text = text + string.Format(TextResolver.GetText("Ongoing bonus while repairing advanced ships"), item.Name) + ", ";
                            num++;
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        text = text + string.Format(TextResolver.GetText("Ongoing bonus while repairing advanced tech"), constructionYard.ShipUnderConstruction.Name) + ", ";
                        num++;
                    }
                }
            }
            if (PirateEmpireBaseHabitat != null)
            {
                double num11 = CalculatePirateResearchBonusFromFacilities();
                if (num11 > 1.0)
                {
                    text = text + string.Format(TextResolver.GetText("Bonus from Pirate Bases and Fortresses at Controlled Colonies"), (num11 - 1.0).ToString("+#%")) + " \n";
                    num++;
                }
            }
            if (text.Length > 0 && num > 0)
            {
                text = text.Substring(0, text.Length - 2);
            }
            return text;
        }

        private ResearchNode CheckNextResearchForSpecialCases()
        {
            return null;
        }

        private List<int> GenerateResearchProjectIdListFromProjects(List<ResearchNode> projects)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < projects.Count; i++)
            {
                ResearchNode researchNode = projects[i];
                if (researchNode != null && !list.Contains(researchNode.ResearchNodeId))
                {
                    list.Add(researchNode.ResearchNodeId);
                }
            }
            return list;
        }

        private void SelectNextResearchProject(IndustryType industry, ResearchNodeList researchQueue)
        {
            List<ComponentCategoryType> targettedCategories = new List<ComponentCategoryType>();
            List<ComponentType> targettedTypes = new List<ComponentType>();
            List<ComponentCategoryType> optimizedDesignCategories = new List<ComponentCategoryType>();
            List<ComponentType> optimizedDesignTypes = new List<ComponentType>();
            ComponentList raceAllowedComponents = new ComponentList();
            DeterminePreferredEmpireResearchFocuses(out targettedCategories, out targettedTypes, out optimizedDesignCategories, out optimizedDesignTypes, out raceAllowedComponents);
            if (Research.LatestProjects == null || Research.NextProjects == null)
            {
                Research.RefreshLatestNextProjects(DominantRace);
            }
            ResearchNodeList projectsByIndustry = Research.NextProjects.GetProjectsByIndustry(industry);
            if (DominantRace != null)
            {
                List<int> list = new List<int>();
                switch (industry)
                {
                    case IndustryType.Weapon:
                        list = DominantRace.ResearchPathWeapons;
                        break;
                    case IndustryType.Energy:
                        list = DominantRace.ResearchPathEnergy;
                        break;
                    case IndustryType.HighTech:
                        list = DominantRace.ResearchPathHighTech;
                        break;
                }
                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        ResearchNode researchNode = Research.TechTree.FindNodeById(list[i]);
                        if (researchNode != null && !researchNode.IsResearched && Research.CanResearchNode(researchNode) && projectsByIndustry.ContainsById(researchNode.ResearchNodeId) && !researchQueue.Contains(researchNode))
                        {
                            researchQueue.Add(researchNode);
                            return;
                        }
                    }
                }
            }
            ResearchNodeList researchNodeList = new ResearchNodeList();
            for (int j = 0; j < targettedCategories.Count; j++)
            {
                ComponentCategoryType componentCategoryType = targettedCategories[j];
                if (componentCategoryType == ComponentCategoryType.Undefined)
                {
                    continue;
                }
                ResearchNode highestProjectForCategory = Research.TechTree.GetHighestProjectForCategory(componentCategoryType);
                if (highestProjectForCategory != null && !highestProjectForCategory.IsResearched)
                {
                    ResearchNodeList currentPath = Research.TechTree.GetCurrentPath(highestProjectForCategory, DominantRace);
                    if (!currentPath.ContainsById(highestProjectForCategory.ResearchNodeId))
                    {
                        currentPath.Add(highestProjectForCategory);
                    }
                    ResearchNodeList researchNodeList2 = projectsByIndustry.Intersect(currentPath);
                    if (researchNodeList2 != null && researchNodeList2.Count > 0)
                    {
                        researchNodeList.AddRange(researchNodeList2);
                    }
                }
            }
            for (int k = 0; k < targettedTypes.Count; k++)
            {
                ComponentType componentType = targettedTypes[k];
                if (componentType == ComponentType.Undefined)
                {
                    continue;
                }
                ResearchNode highestProjectForTypeAny = Research.TechTree.GetHighestProjectForTypeAny(componentType);
                if (highestProjectForTypeAny != null && !highestProjectForTypeAny.IsResearched)
                {
                    ResearchNodeList currentPath2 = Research.TechTree.GetCurrentPath(highestProjectForTypeAny, DominantRace);
                    if (!currentPath2.ContainsById(highestProjectForTypeAny.ResearchNodeId))
                    {
                        currentPath2.Add(highestProjectForTypeAny);
                    }
                    ResearchNodeList researchNodeList3 = projectsByIndustry.Intersect(currentPath2);
                    if (researchNodeList3 != null && researchNodeList3.Count > 0)
                    {
                        researchNodeList.AddRange(researchNodeList3);
                    }
                }
            }
            ResearchNodeList researchNodeList4 = Research.ResolveEssentialProjects_NEW(this, _Galaxy, industry, projectsByIndustry, targettedTypes, targettedCategories);
            for (int l = 0; l < researchNodeList4.Count; l++)
            {
                ResearchNode researchNode2 = researchNodeList4[l];
                if (researchNode2 != null && !researchNode2.IsResearched)
                {
                    ResearchNodeList currentPath3 = Research.TechTree.GetCurrentPath(researchNode2, DominantRace);
                    if (!currentPath3.ContainsById(researchNode2.ResearchNodeId))
                    {
                        currentPath3.Add(researchNode2);
                    }
                    ResearchNodeList researchNodeList5 = projectsByIndustry.Intersect(currentPath3);
                    if (researchNodeList5 != null && researchNodeList5.Count > 0)
                    {
                        researchNodeList = researchNodeList.Merge(researchNodeList5);
                    }
                }
            }
            if (industry == IndustryType.Energy)
            {
                ResearchNodeList projectsByAbility = projectsByIndustry.GetProjectsByAbility(ResearchAbilityType.ConstructionSize);
                if (projectsByAbility != null && projectsByAbility.Count > 0 && researchNodeList.Count > 0 && !researchNodeList.ContainsById(projectsByAbility[0].ResearchNodeId))
                {
                    int num = 0;
                    ResearchNode highestResearchedProjectForIndustry = Research.TechTree.GetHighestResearchedProjectForIndustry(industry);
                    if (highestResearchedProjectForIndustry != null)
                    {
                        num = highestResearchedProjectForIndustry.TechLevel;
                    }
                    if (num >= projectsByAbility[0].TechLevel)
                    {
                        researchNodeList.Add(projectsByAbility[0]);
                    }
                }
            }
            if (industry == IndustryType.HighTech && researchNodeList.Count > 0)
            {
                int num2 = 0;
                if (CanColonizeContinental)
                {
                    num2++;
                }
                if (CanColonizeMarshySwamp)
                {
                    num2++;
                }
                if (CanColonizeOcean)
                {
                    num2++;
                }
                if (CanColonizeDesert)
                {
                    num2++;
                }
                if (CanColonizeIce)
                {
                    num2++;
                }
                if (CanColonizeVolcanic)
                {
                    num2++;
                }
                double num3 = 0.99;
                if (num2 >= 2)
                {
                    num3 = 1.0;
                }
                ResearchNodeList researchNodeList6 = new ResearchNodeList();
                if (Policy.ColonizeContinentalPriority > num3)
                {
                    ResearchNode lowestProjectForColonization = Research.TechTree.GetLowestProjectForColonization(HabitatType.Continental);
                    if (lowestProjectForColonization != null && !researchNodeList6.ContainsById(lowestProjectForColonization.ResearchNodeId))
                    {
                        researchNodeList6.Add(lowestProjectForColonization);
                    }
                }
                if (Policy.ColonizeMarshySwampPriority > num3)
                {
                    ResearchNode lowestProjectForColonization2 = Research.TechTree.GetLowestProjectForColonization(HabitatType.MarshySwamp);
                    if (lowestProjectForColonization2 != null && !researchNodeList6.ContainsById(lowestProjectForColonization2.ResearchNodeId))
                    {
                        researchNodeList6.Add(lowestProjectForColonization2);
                    }
                }
                if (Policy.ColonizeOceanPriority > num3)
                {
                    ResearchNode lowestProjectForColonization3 = Research.TechTree.GetLowestProjectForColonization(HabitatType.Ocean);
                    if (lowestProjectForColonization3 != null && !researchNodeList6.ContainsById(lowestProjectForColonization3.ResearchNodeId))
                    {
                        researchNodeList6.Add(lowestProjectForColonization3);
                    }
                }
                if (Policy.ColonizeDesertPriority > num3)
                {
                    ResearchNode lowestProjectForColonization4 = Research.TechTree.GetLowestProjectForColonization(HabitatType.Desert);
                    if (lowestProjectForColonization4 != null && !researchNodeList6.ContainsById(lowestProjectForColonization4.ResearchNodeId))
                    {
                        researchNodeList6.Add(lowestProjectForColonization4);
                    }
                }
                if (Policy.ColonizeIcePriority > num3)
                {
                    ResearchNode lowestProjectForColonization5 = Research.TechTree.GetLowestProjectForColonization(HabitatType.Ice);
                    if (lowestProjectForColonization5 != null && !researchNodeList6.ContainsById(lowestProjectForColonization5.ResearchNodeId))
                    {
                        researchNodeList6.Add(lowestProjectForColonization5);
                    }
                }
                if (Policy.ColonizeVolcanicPriority > num3)
                {
                    ResearchNode lowestProjectForColonization6 = Research.TechTree.GetLowestProjectForColonization(HabitatType.Volcanic);
                    if (lowestProjectForColonization6 != null && !researchNodeList6.ContainsById(lowestProjectForColonization6.ResearchNodeId))
                    {
                        researchNodeList6.Add(lowestProjectForColonization6);
                    }
                }
                for (int m = 0; m < Math.Min(2, researchNodeList6.Count); m++)
                {
                    ResearchNode researchNode3 = researchNodeList6[m];
                    if (researchNode3 != null && !researchNode3.IsResearched)
                    {
                        ResearchNodeList currentPath4 = Research.TechTree.GetCurrentPath(researchNode3, DominantRace);
                        if (!currentPath4.ContainsById(researchNode3.ResearchNodeId))
                        {
                            currentPath4.Add(researchNode3);
                        }
                        ResearchNodeList researchNodeList7 = projectsByIndustry.Intersect(currentPath4);
                        if (researchNodeList7 != null && researchNodeList7.Count > 0)
                        {
                            researchNodeList = researchNodeList.Merge(researchNodeList7);
                        }
                    }
                }
            }
            ResearchNodeList researchNodeList8 = projectsByIndustry.NotIntersect(researchNodeList);
            ResearchNode lowestProjectForTypeAny = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.WeaponBeam);
            ResearchNode lowestProjectForTypeAny2 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.WeaponGravityBeam);
            ResearchNode lowestProjectForTypeAny3 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.WeaponRailGun);
            ResearchNode lowestProjectForTypeAny4 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.WeaponMissile);
            ResearchNode lowestProjectForTypeAny5 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.WeaponTorpedo);
            ResearchNode lowestProjectForTypeAny6 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.WeaponAreaDestruction);
            ResearchNode lowestProjectForTroopType = Research.TechTree.GetLowestProjectForTroopType(TroopType.Infantry);
            ResearchNode lowestProjectForTypeAny7 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.HyperDrive);
            ResearchNode lowestProjectForTypeAny8 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.ExtractorMine);
            ResearchNode lowestProjectForTypeAny9 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.EnergyCollector);
            ResearchNode lowestProjectForTypeAny10 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.Shields);
            ResearchNode lowestProjectForTypeAny11 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.ConstructionBuild);
            ResearchNode lowestProjectForTypeAny12 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.Reactor);
            ResearchNode lowestProjectForTypeAny13 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.EngineMainThrust);
            ResearchNode lowestProjectForTypeAny14 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.EngineVectoring);
            ResearchNode secondLowestProjectForTypeAny = Research.TechTree.GetSecondLowestProjectForTypeAny(ComponentType.HyperDrive);
            ResearchNode secondLowestProjectForTypeAny2 = Research.TechTree.GetSecondLowestProjectForTypeAny(ComponentType.ConstructionBuild);
            ResearchNode lowestProjectForTypeAny15 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.ComputerCommandCenter);
            ResearchNode lowestProjectForTypeAny16 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.StorageDockingBay);
            ResearchNode lowestProjectForTypeAny17 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.ComputerCommerceCenter);
            ResearchNode lowestProjectForTypeAny18 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.LabsEnergyLab);
            ResearchNode lowestProjectForTypeAny19 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.StorageTroop);
            ResearchNode lowestProjectForTypeAny20 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.StorageCargo);
            ResearchNode lowestProjectForTypeAny21 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.SensorResourceProfileSensor);
            ResearchNode lowestProjectForTypeAny22 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.StorageCargo);
            ResearchNode lowestProjectForTypeAny23 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.StorageFuel);
            ResearchNode lowestProjectForTypeAny24 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.HabitationLifeSupport);
            ResearchNode lowestProjectForTypeAny25 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.HabitationHabModule);
            ResearchNode lowestProjectForTypeAny26 = Research.TechTree.GetLowestProjectForTypeAny(ComponentType.HabitationColonization);
            List<int> list2 = new List<int>();
            List<int> list3 = new List<int>();
            List<int> list4 = new List<int>();
            switch (industry)
            {
                case IndustryType.Weapon:
                    list2 = (targettedTypes.Contains(ComponentType.WeaponGravityBeam) ? GenerateResearchProjectIdListFromProjects(new List<ResearchNode> { lowestProjectForTypeAny2 }) : (targettedTypes.Contains(ComponentType.WeaponRailGun) ? GenerateResearchProjectIdListFromProjects(new List<ResearchNode> { lowestProjectForTypeAny3 }) : (targettedTypes.Contains(ComponentType.WeaponMissile) ? GenerateResearchProjectIdListFromProjects(new List<ResearchNode> { lowestProjectForTypeAny4 }) : (targettedCategories.Contains(ComponentCategoryType.WeaponBeam) ? GenerateResearchProjectIdListFromProjects(new List<ResearchNode> { lowestProjectForTypeAny }) : (targettedCategories.Contains(ComponentCategoryType.WeaponTorpedo) ? GenerateResearchProjectIdListFromProjects(new List<ResearchNode> { lowestProjectForTypeAny5 }) : ((!targettedCategories.Contains(ComponentCategoryType.WeaponArea)) ? GenerateResearchProjectIdListFromProjects(new List<ResearchNode> { lowestProjectForTypeAny }) : GenerateResearchProjectIdListFromProjects(new List<ResearchNode> { lowestProjectForTypeAny6 })))))));
                    list3 = GenerateResearchProjectIdListFromProjects(new List<ResearchNode> { lowestProjectForTroopType });
                    break;
                case IndustryType.Energy:
                    list2 = GenerateResearchProjectIdListFromProjects(new List<ResearchNode> { lowestProjectForTypeAny7 });
                    list3 = GenerateResearchProjectIdListFromProjects(new List<ResearchNode> { lowestProjectForTypeAny8, lowestProjectForTypeAny9, lowestProjectForTypeAny10, lowestProjectForTypeAny11, lowestProjectForTypeAny12, lowestProjectForTypeAny13, lowestProjectForTypeAny14 });
                    list4 = GenerateResearchProjectIdListFromProjects(new List<ResearchNode> { secondLowestProjectForTypeAny, secondLowestProjectForTypeAny2 });
                    break;
                case IndustryType.HighTech:
                    list2 = GenerateResearchProjectIdListFromProjects(new List<ResearchNode>
            {
                lowestProjectForTypeAny15, lowestProjectForTypeAny16, lowestProjectForTypeAny17, lowestProjectForTypeAny18, lowestProjectForTypeAny19, lowestProjectForTypeAny20, lowestProjectForTypeAny21, lowestProjectForTypeAny22, lowestProjectForTypeAny23, lowestProjectForTypeAny24,
                lowestProjectForTypeAny25
            });
                    if (PirateEmpireBaseHabitat == null)
                    {
                        list3 = GenerateResearchProjectIdListFromProjects(new List<ResearchNode> { lowestProjectForTypeAny26 });
                    }
                    break;
            }
            bool flag = false;
            if (projectsByIndustry.CheckContainsAnyNodeId(list2))
            {
                researchNodeList = projectsByIndustry.FindNodesByIdsUnresearched(list2);
                flag = true;
            }
            else if (projectsByIndustry.CheckContainsAnyNodeId(list3))
            {
                researchNodeList = projectsByIndustry.FindNodesByIdsUnresearched(list3);
                flag = true;
            }
            else if (projectsByIndustry.CheckContainsAnyNodeId(list4))
            {
                researchNodeList = projectsByIndustry.FindNodesByIdsUnresearched(list4);
                flag = true;
            }
            if (industry == IndustryType.Weapon && Research.ResearchedComponents.GetFirstWeaponOrFighter() == null)
            {
                ResearchNodeList projectsByCategory = researchNodeList.GetProjectsByCategory(ComponentCategoryType.Armor);
                if (projectsByCategory != null && projectsByCategory.Count > 0 && researchNodeList.Count > projectsByCategory.Count)
                {
                    for (int n = 0; n < projectsByCategory.Count; n++)
                    {
                        researchNodeList.Remove(projectsByCategory[n]);
                    }
                }
                projectsByCategory = researchNodeList8.GetProjectsByCategory(ComponentCategoryType.Armor);
                if (projectsByCategory != null && projectsByCategory.Count > 0 && researchNodeList8.Count > projectsByCategory.Count)
                {
                    for (int num4 = 0; num4 < projectsByCategory.Count; num4++)
                    {
                        researchNodeList8.Remove(projectsByCategory[num4]);
                    }
                }
            }
            if ((DominantRace != null && DominantRace.SpecialComponent != null) || (raceAllowedComponents != null && raceAllowedComponents.Count > 0))
            {
                researchNodeList = ResearchSystem.RemoveNonRaceSpecificProjectTypes(DominantRace, Research.TechTree, researchNodeList, optimizedDesignCategories, optimizedDesignTypes, raceAllowedComponents);
                researchNodeList8 = ResearchSystem.RemoveNonRaceSpecificProjectTypes(DominantRace, Research.TechTree, researchNodeList8, optimizedDesignCategories, optimizedDesignTypes, raceAllowedComponents);
            }
            if (PirateEmpireBaseHabitat != null)
            {
                researchNodeList.StripProjectsByType(ComponentType.HabitationColonization);
                researchNodeList8.StripProjectsByType(ComponentType.HabitationColonization);
                researchNodeList.StripProjectsByAbility(ResearchAbilityType.ColonizeHabitatType);
                researchNodeList8.StripProjectsByAbility(ResearchAbilityType.ColonizeHabitatType);
                researchNodeList.StripProjectsByAbility(ResearchAbilityType.Troop);
                researchNodeList8.StripProjectsByAbility(ResearchAbilityType.Troop);
            }
            int lowest = projectsByIndustry.GetLowestTechLevel();
            int techLevel = lowest + 3;
            if (researchNodeList != null && researchNodeList.Count > 0)
            {
                researchNodeList = researchNodeList.RemoveProjectsWithTechLevelHigherThan(techLevel);
            }
            if (researchNodeList8 != null && researchNodeList8.Count > 0)
            {
                researchNodeList8 = researchNodeList8.RemoveProjectsWithTechLevelHigherThan(techLevel);
            }
            if (researchNodeList8 != null && researchNodeList8.Count > 0)
            {
                researchNodeList8.GetTechLevelRange(out lowest, out var highest);
                if (lowest < highest)
                {
                    researchNodeList8 = researchNodeList8.GetProjectsAtTechLevel(lowest);
                }
            }
            researchNodeList8.StripProjectsAboveTechLevel(99);
            researchNodeList.StripProjectsAboveTechLevel(99);
            projectsByIndustry.StripProjectsAboveTechLevel(99);
            ResearchNode researchNode4 = null;
            if (!flag && researchNodeList8 != null && researchNodeList8.Count > 0 && researchNodeList.Count == 0)
            {
                researchNode4 = researchNodeList8.SelectRandomLowestProject(_Galaxy);
            }
            else if (researchNodeList != null && researchNodeList.Count > 0)
            {
                researchNode4 = researchNodeList.SelectRandomLowestProject(_Galaxy);
            }
            else if (projectsByIndustry != null && projectsByIndustry.Count > 0)
            {
                researchNode4 = projectsByIndustry.SelectRandomLowestProject(_Galaxy);
            }
            if (researchNode4 != null && !researchNode4.IsResearched && !researchQueue.Contains(researchNode4))
            {
                researchQueue.Add(researchNode4);
            }
        }

        public void PerformResearch(double timePassed, bool allowResearchEvents)
        {
            double researchEnergy = 0.0;
            double researchHighTech = 0.0;
            double researchWeapons = 0.0;
            CalculateResearchTotal(out researchEnergy, out researchHighTech, out researchWeapons);
            PerformResearchProjects(timePassed, Research.ResearchQueueEnergy, researchEnergy, IndustryType.Energy, allowResearchEvents);
            PerformResearchProjects(timePassed, Research.ResearchQueueHighTech, researchHighTech, IndustryType.HighTech, allowResearchEvents);
            PerformResearchProjects(timePassed, Research.ResearchQueueWeapons, researchWeapons, IndustryType.Weapon, allowResearchEvents);
            Research.Update(DominantRace);
            BaconResearchSystem.DetermineComponentImprovements(this);
            ReviewDesignComponentsAvailable();
        }

        public void ReviewDesignComponentsAvailable()
        {
            if (!_ComponentsAvailable[5])
            {
                _ComponentsAvailable[5] = CheckDesignComponentsAvailable(BuiltObjectRole.Military, BuiltObjectSubRole.CapitalShip);
            }
            if (!_ComponentsAvailable[7])
            {
                _ComponentsAvailable[7] = CheckDesignComponentsAvailable(BuiltObjectRole.Military, BuiltObjectSubRole.Carrier);
            }
            if (!_ComponentsAvailable[13])
            {
                _ComponentsAvailable[13] = CheckDesignComponentsAvailable(BuiltObjectRole.Colony, BuiltObjectSubRole.ColonyShip);
            }
            if (!_ComponentsAvailable[15])
            {
                _ComponentsAvailable[15] = CheckDesignComponentsAvailable(BuiltObjectRole.Build, BuiltObjectSubRole.ConstructionShip);
            }
            if (!_ComponentsAvailable[4])
            {
                _ComponentsAvailable[4] = CheckDesignComponentsAvailable(BuiltObjectRole.Military, BuiltObjectSubRole.Cruiser);
            }
            if (!_ComponentsAvailable[29])
            {
                _ComponentsAvailable[29] = CheckDesignComponentsAvailable(BuiltObjectRole.Base, BuiltObjectSubRole.DefensiveBase);
            }
            if (!_ComponentsAvailable[3])
            {
                _ComponentsAvailable[3] = CheckDesignComponentsAvailable(BuiltObjectRole.Military, BuiltObjectSubRole.Destroyer);
            }
            if (!_ComponentsAvailable[25])
            {
                _ComponentsAvailable[25] = CheckDesignComponentsAvailable(BuiltObjectRole.Base, BuiltObjectSubRole.EnergyResearchStation);
            }
            if (!_ComponentsAvailable[1])
            {
                _ComponentsAvailable[1] = CheckDesignComponentsAvailable(BuiltObjectRole.Military, BuiltObjectSubRole.Escort);
            }
            if (!_ComponentsAvailable[9])
            {
                _ComponentsAvailable[9] = CheckDesignComponentsAvailable(BuiltObjectRole.Exploration, BuiltObjectSubRole.ExplorationShip);
            }
            if (!_ComponentsAvailable[2])
            {
                _ComponentsAvailable[2] = CheckDesignComponentsAvailable(BuiltObjectRole.Military, BuiltObjectSubRole.Frigate);
            }
            if (!_ComponentsAvailable[16])
            {
                _ComponentsAvailable[16] = CheckDesignComponentsAvailable(BuiltObjectRole.Resource, BuiltObjectSubRole.GasMiningShip);
            }
            if (!_ComponentsAvailable[18])
            {
                _ComponentsAvailable[18] = CheckDesignComponentsAvailable(BuiltObjectRole.Base, BuiltObjectSubRole.GasMiningStation);
            }
            if (!_ComponentsAvailable[24])
            {
                _ComponentsAvailable[24] = CheckDesignComponentsAvailable(BuiltObjectRole.Base, BuiltObjectSubRole.GenericBase);
            }
            if (!_ComponentsAvailable[27])
            {
                _ComponentsAvailable[27] = CheckDesignComponentsAvailable(BuiltObjectRole.Base, BuiltObjectSubRole.HighTechResearchStation);
            }
            if (!_ComponentsAvailable[12])
            {
                _ComponentsAvailable[12] = CheckDesignComponentsAvailable(BuiltObjectRole.Freight, BuiltObjectSubRole.LargeFreighter);
            }
            if (!_ComponentsAvailable[22])
            {
                _ComponentsAvailable[22] = CheckDesignComponentsAvailable(BuiltObjectRole.Base, BuiltObjectSubRole.LargeSpacePort);
            }
            if (!_ComponentsAvailable[11])
            {
                _ComponentsAvailable[11] = CheckDesignComponentsAvailable(BuiltObjectRole.Freight, BuiltObjectSubRole.MediumFreighter);
            }
            if (!_ComponentsAvailable[21])
            {
                _ComponentsAvailable[21] = CheckDesignComponentsAvailable(BuiltObjectRole.Base, BuiltObjectSubRole.MediumSpacePort);
            }
            if (!_ComponentsAvailable[17])
            {
                _ComponentsAvailable[17] = CheckDesignComponentsAvailable(BuiltObjectRole.Resource, BuiltObjectSubRole.MiningShip);
            }
            if (!_ComponentsAvailable[19])
            {
                _ComponentsAvailable[19] = CheckDesignComponentsAvailable(BuiltObjectRole.Base, BuiltObjectSubRole.MiningStation);
            }
            if (!_ComponentsAvailable[28])
            {
                _ComponentsAvailable[28] = CheckDesignComponentsAvailable(BuiltObjectRole.Base, BuiltObjectSubRole.MonitoringStation);
            }
            if (!_ComponentsAvailable[14])
            {
                _ComponentsAvailable[14] = CheckDesignComponentsAvailable(BuiltObjectRole.Passenger, BuiltObjectSubRole.PassengerShip);
            }
            if (!_ComponentsAvailable[23])
            {
                _ComponentsAvailable[23] = CheckDesignComponentsAvailable(BuiltObjectRole.Base, BuiltObjectSubRole.ResortBase);
            }
            if (!_ComponentsAvailable[8])
            {
                _ComponentsAvailable[8] = CheckDesignComponentsAvailable(BuiltObjectRole.Military, BuiltObjectSubRole.ResupplyShip);
            }
            if (!_ComponentsAvailable[10])
            {
                _ComponentsAvailable[10] = CheckDesignComponentsAvailable(BuiltObjectRole.Freight, BuiltObjectSubRole.SmallFreighter);
            }
            if (!_ComponentsAvailable[20])
            {
                _ComponentsAvailable[20] = CheckDesignComponentsAvailable(BuiltObjectRole.Base, BuiltObjectSubRole.SmallSpacePort);
            }
            if (!_ComponentsAvailable[6])
            {
                _ComponentsAvailable[6] = CheckDesignComponentsAvailable(BuiltObjectRole.Military, BuiltObjectSubRole.TroopTransport);
            }
            if (!_ComponentsAvailable[26])
            {
                _ComponentsAvailable[26] = CheckDesignComponentsAvailable(BuiltObjectRole.Base, BuiltObjectSubRole.WeaponsResearchStation);
            }
        }

        private void PerformResearchProjects(double timePassed, ResearchNodeList projects, double researchPower, IndustryType industry, bool allowResearchEvents)
        {
            if (projects == null)
            {
                return;
            }
            if (_ControlResearch && projects.Count <= 0)
            {
                SelectNextResearchProject(industry, projects);
            }
            double num = researchPower * timePassed / (double)Galaxy.RealSecondsInGalacticYear * _Galaxy.ResearchSpeedModifier;
            double num2 = CalculateResearchOutputBonuses(industry);
            num *= num2;
            int iterationCount = 0;
            while (Galaxy.ConditionCheckLimit(num > 0.0, 50, ref iterationCount))
            {
                ResearchNode researchNode = null;
                if (projects == null || projects.Count <= 0)
                {
                    break;
                }
                researchNode = projects[0];
                float progress = researchNode.Progress;
                float num3 = (float)num;
                if (researchNode.IsRushing)
                {
                    num3 *= 3f;
                }
                researchNode.Progress += num3;
                if (researchNode.Progress >= researchNode.Cost)
                {
                    DoResearchBreakthrough(researchNode, selfResearched: true);
                    num -= (double)(researchNode.Cost - progress);
                    if (_ControlResearch && projects.Count <= 0)
                    {
                        SelectNextResearchProject(industry, projects);
                    }
                    if (!_Galaxy.ChanceNewScientist(this, researchNode))
                    {
                        _Galaxy.ChanceScientistPromotion(this, researchNode);
                    }
                    if (researchNode.SpecialFunctionCode == 2 || researchNode.SpecialFunctionCode == 4)
                    {
                        SendNewsBroadcast(EventMessageType.Undefined, researchNode, DisasterEventType.Undefined, warStartEnd: false, wonderBegun: false, EmpireMessageType.ResearchBreakthrough, this);
                    }
                }
                else
                {
                    num = 0.0;
                }
            }
            if (!allowResearchEvents)
            {
                return;
            }
            int num4 = Math.Max(4, (int)(6000.0 / timePassed));
            if (projects != null && projects.Count > 0)
            {
                double num5 = Math.Max(6000.0, Math.Sqrt(projects[0].Cost) * 10.0);
                num4 = Math.Max(6, (int)(num5 / timePassed));
            }
            Character character = null;
            CharacterList characterList = new CharacterList();
            if (Characters != null)
            {
                characterList = Characters.GetScientistsAtResearchStations(industry);
                character = characterList.GetFirstCharacterWithTrait(CharacterTraitType.Creative);
                CharacterSkillType skillType = CharacterSkillType.ResearchEnergy;
                switch (industry)
                {
                    case IndustryType.Energy:
                        skillType = CharacterSkillType.ResearchEnergy;
                        break;
                    case IndustryType.HighTech:
                        skillType = CharacterSkillType.ResearchHighTech;
                        break;
                    case IndustryType.Weapon:
                        skillType = CharacterSkillType.ResearchWeapons;
                        break;
                }
                if (character == null || character.Skills.GetSkillByType(skillType) == null)
                {
                    character = characterList.GetFirstCharacterWithSkill(skillType);
                }
                if (characterList.CheckCharactersForTrait(CharacterRole.Scientist, CharacterTraitType.Creative))
                {
                    num4 /= 2;
                }
                else if (characterList.CheckCharactersForTrait(CharacterRole.Scientist, CharacterTraitType.Methodical))
                {
                    num4 *= 2;
                }
            }
            if (Galaxy.Rnd.Next(0, num4) != 1)
            {
                return;
            }
            ResearchNode researchNode2 = null;
            if (projects != null && projects.Count > 0)
            {
                researchNode2 = projects[0];
                if (researchNode2 != null && researchNode2.IsRushing)
                {
                    researchNode2 = null;
                }
            }
            if (researchNode2 == null)
            {
                return;
            }
            if (Galaxy.Rnd.Next(0, 3) == 1)
            {
                float num6 = (float)(0.5 + Galaxy.Rnd.NextDouble() * 0.5);
                float num7 = Math.Max(0f, Math.Min(researchNode2.Progress, researchNode2.Progress * num6));
                researchNode2.Progress -= num7;
                _Galaxy.DoCharacterEvent(CharacterEventType.CriticalResearchFailure, researchNode2, characterList);
                string empty = string.Empty;
                if (character != null)
                {
                    string arg = string.Empty;
                    if (character.Location != null)
                    {
                        arg = character.Location.Name;
                    }
                    empty = string.Format(TextResolver.GetText("Research Critical Failure SCIENTIST LOCATION RESEARCHPROJECT"), character.Name, arg, researchNode2.Name);
                }
                else
                {
                    empty = string.Format(TextResolver.GetText("Research Critical Failure RESEARCHPROJECT"), researchNode2.Name);
                }
                SendMessageToEmpire(this, EmpireMessageType.ResearchCriticalFailure, researchNode2, empty);
                return;
            }
            researchNode2.IsRushing = true;
            _Galaxy.DoCharacterEvent(CharacterEventType.CriticalResearchSuccess, researchNode2, characterList);
            string empty2 = string.Empty;
            if (character != null)
            {
                string arg2 = string.Empty;
                if (character.Location != null)
                {
                    arg2 = character.Location.Name;
                }
                empty2 = string.Format(TextResolver.GetText("Research Critical Success SCIENTIST LOCATION RESEARCHPROJECT"), character.Name, arg2, researchNode2.Name);
            }
            else
            {
                empty2 = string.Format(TextResolver.GetText("Research Critical Success RESEARCHPROJECT"), researchNode2.Name);
            }
            SendMessageToEmpire(this, EmpireMessageType.ResearchCriticalBreakthrough, researchNode2, empty2);
            _Galaxy.ChanceNewScientistCriticalSuccess(this, researchNode2);
        }

        public void ReviewDesignsBuiltObjectsImprovedComponents()
        {
            for (int i = 0; i < Designs.Count; i++)
            {
                Designs[i].ReDefine();
            }
            for (int j = 0; j < BuiltObjects.Count; j++)
            {
                BuiltObjects[j].ReDefine();
            }
            for (int k = 0; k < PrivateBuiltObjects.Count; k++)
            {
                PrivateBuiltObjects[k].ReDefine();
            }
        }

        public void ReviewResearchAbilities()
        {
            ReviewColonizationTypes();
            ReviewPopulationGrowthRates();
            int newSize = 0;
            ReviewMaximumConstructionSize(out newSize);
            ReviewCanBuildShipTypes();
            ReviewTroopTypes();
        }

        public string DoResearchAbilityBreakthrough(ResearchNode researchProject, out object relatedObject)
        {
            string text = string.Empty;
            relatedObject = null;
            ReviewColonizationTypes();
            ReviewPopulationGrowthRates();
            int newSize = 0;
            ReviewMaximumConstructionSize(out newSize);
            ReviewCanBuildShipTypes();
            ReviewTroopTypes();
            if (researchProject.Abilities != null && researchProject.Abilities.Count > 0)
            {
                for (int i = 0; i < researchProject.Abilities.Count; i++)
                {
                    int value = researchProject.Abilities[i].Value;
                    switch (researchProject.Abilities[i].Type)
                    {
                        case ResearchAbilityType.Boarding:
                            if (researchProject.Abilities[i].Value > 0)
                            {
                                text += TextResolver.GetText("Improved Boarding attack strength").ToLower(CultureInfo.InvariantCulture);
                            }
                            else if (researchProject.Abilities[i].Value < 0)
                            {
                                text += TextResolver.GetText("Improved Boarding defense strength").ToLower(CultureInfo.InvariantCulture);
                            }
                            break;
                        case ResearchAbilityType.Troop:
                            if (researchProject.Abilities[i].RelatedObject != null && researchProject.Abilities[i].RelatedObject is TroopType)
                            {
                                TroopType troopType = (TroopType)researchProject.Abilities[i].RelatedObject;
                                if (troopType != 0)
                                {
                                    text = ((researchProject.Abilities[i].Value > 0) ? (text + " " + string.Format(TextResolver.GetText("Increases the Attack Strength of newly recruited TROOPTYPE").ToLower(CultureInfo.InvariantCulture), Galaxy.ResolveDescription(troopType))) : ((researchProject.Abilities[i].Value >= 0) ? (text + " " + string.Format(TextResolver.GetText("the ability to recruit TROOPTYPE"), Galaxy.ResolveDescription(troopType))) : (text + " " + string.Format(TextResolver.GetText("Increases the Defend Strength of newly recruited TROOPTYPE").ToLower(CultureInfo.InvariantCulture), Galaxy.ResolveDescription(troopType)))));
                                    relatedObject = troopType;
                                }
                                else
                                {
                                    text = text + " " + TextResolver.GetText("Lowers the maintenance costs of all troops").ToLower(CultureInfo.InvariantCulture);
                                }
                            }
                            else
                            {
                                text = text + " " + TextResolver.GetText("Lowers the maintenance costs of all troops").ToLower(CultureInfo.InvariantCulture);
                            }
                            break;
                        case ResearchAbilityType.EnableShipSubRole:
                            if (researchProject.Abilities[i].RelatedObject != null && researchProject.Abilities[i].RelatedObject is BuiltObjectSubRole)
                            {
                                BuiltObjectSubRole builtObjectSubRole = (BuiltObjectSubRole)researchProject.Abilities[i].RelatedObject;
                                text = text + " " + string.Format(TextResolver.GetText("the ability to build SHIPTYPE"), Galaxy.ResolveDescription(builtObjectSubRole));
                                relatedObject = builtObjectSubRole;
                            }
                            break;
                        case ResearchAbilityType.ColonizeHabitatType:
                            switch (value)
                            {
                                case 1:
                                    text = text + " " + string.Format(TextResolver.GetText("the ability to colonize PLANETTYPE planets and moons"), Galaxy.ResolveDescription(HabitatType.Continental));
                                    relatedObject = HabitatType.Continental;
                                    break;
                                case 2:
                                    text = text + " " + string.Format(TextResolver.GetText("the ability to colonize PLANETTYPE planets and moons"), Galaxy.ResolveDescription(HabitatType.MarshySwamp));
                                    relatedObject = HabitatType.MarshySwamp;
                                    break;
                                case 3:
                                    text = text + " " + string.Format(TextResolver.GetText("the ability to colonize PLANETTYPE planets and moons"), Galaxy.ResolveDescription(HabitatType.Ocean));
                                    relatedObject = HabitatType.Ocean;
                                    break;
                                case 4:
                                    text = text + " " + string.Format(TextResolver.GetText("the ability to colonize PLANETTYPE planets and moons"), Galaxy.ResolveDescription(HabitatType.Desert));
                                    relatedObject = HabitatType.Desert;
                                    break;
                                case 5:
                                    text = text + " " + string.Format(TextResolver.GetText("the ability to colonize PLANETTYPE planets and moons"), Galaxy.ResolveDescription(HabitatType.Ice));
                                    relatedObject = HabitatType.Ice;
                                    break;
                                case 6:
                                    text = text + " " + string.Format(TextResolver.GetText("the ability to colonize PLANETTYPE planets and moons"), Galaxy.ResolveDescription(HabitatType.Volcanic));
                                    relatedObject = HabitatType.Volcanic;
                                    break;
                            }
                            break;
                        case ResearchAbilityType.ConstructionSize:
                            text = text + " " + string.Format(TextResolver.GetText("an increase to the maximum construction sizes of ships and bases"), value.ToString(), (value * 3).ToString());
                            break;
                        case ResearchAbilityType.PopulationGrowthRate:
                            switch (value)
                            {
                                case 1:
                                    text = text + " " + string.Format(TextResolver.GetText("double population growth rate at all of our PLANETTYPE colonies"), Galaxy.ResolveDescription(HabitatType.Continental));
                                    break;
                                case 2:
                                    text = text + " " + string.Format(TextResolver.GetText("double population growth rate at all of our PLANETTYPE colonies"), Galaxy.ResolveDescription(HabitatType.MarshySwamp));
                                    break;
                                case 3:
                                    text = text + " " + string.Format(TextResolver.GetText("double population growth rate at all of our PLANETTYPE colonies"), Galaxy.ResolveDescription(HabitatType.Ocean));
                                    break;
                                case 4:
                                    text = text + " " + string.Format(TextResolver.GetText("double population growth rate at all of our PLANETTYPE colonies"), Galaxy.ResolveDescription(HabitatType.Desert));
                                    break;
                                case 5:
                                    text = text + " " + string.Format(TextResolver.GetText("double population growth rate at all of our PLANETTYPE colonies"), Galaxy.ResolveDescription(HabitatType.Ice));
                                    break;
                                case 6:
                                    text = text + " " + string.Format(TextResolver.GetText("double population growth rate at all of our PLANETTYPE colonies"), Galaxy.ResolveDescription(HabitatType.Volcanic));
                                    break;
                            }
                            break;
                    }
                }
            }
            return text;
        }

        public void ReviewColonizationTypes()
        {
            bool canColonizeContinental = false;
            bool canColonizeMarshySwamp = false;
            bool canColonizeOcean = false;
            bool canColonizeDesert = false;
            bool canColonizeIce = false;
            bool canColonizeVolcanic = false;
            if (Research != null && Research.Abilities != null && Research.Abilities.Count > 0)
            {
                for (int i = 0; i < Research.Abilities.Count; i++)
                {
                    if (Research.Abilities[i].Type == ResearchAbilityType.ColonizeHabitatType)
                    {
                        switch (Research.Abilities[i].Value)
                        {
                            case 1:
                                canColonizeContinental = true;
                                break;
                            case 2:
                                canColonizeMarshySwamp = true;
                                break;
                            case 3:
                                canColonizeOcean = true;
                                break;
                            case 4:
                                canColonizeDesert = true;
                                break;
                            case 5:
                                canColonizeIce = true;
                                break;
                            case 6:
                                canColonizeVolcanic = true;
                                break;
                        }
                    }
                }
            }
            CanColonizeContinental = canColonizeContinental;
            CanColonizeMarshySwamp = canColonizeMarshySwamp;
            CanColonizeOcean = canColonizeOcean;
            CanColonizeDesert = canColonizeDesert;
            CanColonizeIce = canColonizeIce;
            CanColonizeVolcanic = canColonizeVolcanic;
        }

        public void ReviewPopulationGrowthRates()
        {
            float colonyGrowthRateContinental = 0.5f;
            float colonyGrowthRateMarshySwamp = 0.5f;
            float colonyGrowthRateOcean = 0.5f;
            float colonyGrowthRateDesert = 0.5f;
            float colonyGrowthRateIce = 0.5f;
            float colonyGrowthRateVolcanic = 0.5f;
            if (Research != null && Research.Abilities != null && Research.Abilities.Count > 0)
            {
                for (int i = 0; i < Research.Abilities.Count; i++)
                {
                    if (Research.Abilities[i].Type == ResearchAbilityType.PopulationGrowthRate)
                    {
                        switch (Research.Abilities[i].Value)
                        {
                            case 1:
                                colonyGrowthRateContinental = 1f;
                                break;
                            case 2:
                                colonyGrowthRateMarshySwamp = 1f;
                                break;
                            case 3:
                                colonyGrowthRateOcean = 1f;
                                break;
                            case 4:
                                colonyGrowthRateDesert = 1f;
                                break;
                            case 5:
                                colonyGrowthRateIce = 1f;
                                break;
                            case 6:
                                colonyGrowthRateVolcanic = 1f;
                                break;
                        }
                    }
                }
            }
            ColonyGrowthRateContinental = colonyGrowthRateContinental;
            ColonyGrowthRateMarshySwamp = colonyGrowthRateMarshySwamp;
            ColonyGrowthRateOcean = colonyGrowthRateOcean;
            ColonyGrowthRateDesert = colonyGrowthRateDesert;
            ColonyGrowthRateIce = colonyGrowthRateIce;
            ColonyGrowthRateVolcanic = colonyGrowthRateVolcanic;
        }

        public void ReviewMaximumConstructionSize(out int newSize)
        {
            newSize = -1;
            _BaseMaximumConstructionSize = 160;
            if (Research == null || Research.Abilities == null || Research.Abilities.Count <= 0)
            {
                return;
            }
            ResearchAbility researchAbility = null;
            for (int i = 0; i < Research.Abilities.Count; i++)
            {
                if (Research.Abilities[i].Type == ResearchAbilityType.ConstructionSize && (researchAbility == null || Research.Abilities[i].Level > researchAbility.Level))
                {
                    researchAbility = Research.Abilities[i];
                }
            }
            if (researchAbility != null)
            {
                _BaseMaximumConstructionSize = researchAbility.Value;
                newSize = _BaseMaximumConstructionSize;
            }
        }

        public void ReviewTroopTypes()
        {
            bool troopCanRecruitInfantry = false;
            bool troopCanRecruitArmored = false;
            bool troopCanRecruitArtillery = false;
            bool troopCanRecruitSpecialForces = false;
            float num = 1f;
            float num2 = 1f;
            float num3 = 1f;
            float num4 = 1f;
            float num5 = 1f;
            float num6 = 1f;
            float num7 = 1f;
            float num8 = 1f;
            float num9 = 1f;
            float num10 = 1f;
            float num11 = 1f;
            if (Research != null && Research.Abilities != null && Research.Abilities.Count > 0)
            {
                for (int i = 0; i < Research.Abilities.Count; i++)
                {
                    ResearchAbility researchAbility = Research.Abilities[i];
                    if (researchAbility.Type == ResearchAbilityType.Boarding)
                    {
                        if (researchAbility.Value > 0)
                        {
                            float num12 = 1f + Math.Abs((float)researchAbility.Value / 100f);
                            if (num12 > num10)
                            {
                                num10 = num12;
                            }
                        }
                        else if (researchAbility.Value < 0)
                        {
                            float num13 = 1f + Math.Abs((float)researchAbility.Value / 100f);
                            if (num13 > num11)
                            {
                                num11 = num13;
                            }
                        }
                    }
                    else
                    {
                        if (researchAbility.Type != ResearchAbilityType.Troop)
                        {
                            continue;
                        }
                        if (researchAbility.RelatedObject != null && researchAbility.RelatedObject is TroopType)
                        {
                            switch ((TroopType)researchAbility.RelatedObject)
                            {
                                case TroopType.Undefined:
                                    if (researchAbility.Value < 0)
                                    {
                                        float num16 = Math.Abs((float)researchAbility.Value / 100f);
                                        float num17 = 1f - num16;
                                        if (num17 < num9)
                                        {
                                            num9 = num17;
                                        }
                                    }
                                    break;
                                case TroopType.Infantry:
                                    troopCanRecruitInfantry = true;
                                    if (researchAbility.Value > 0)
                                    {
                                        float num18 = 1f + Math.Abs((float)researchAbility.Value / 100f);
                                        if (num18 > num)
                                        {
                                            num = num18;
                                        }
                                    }
                                    else if (researchAbility.Value < 0)
                                    {
                                        float num19 = 1f + Math.Abs((float)researchAbility.Value / 100f);
                                        if (num19 > num2)
                                        {
                                            num2 = num19;
                                        }
                                    }
                                    break;
                                case TroopType.Armored:
                                    troopCanRecruitArmored = true;
                                    if (researchAbility.Value > 0)
                                    {
                                        float num22 = 1f + Math.Abs((float)researchAbility.Value / 100f);
                                        if (num22 > num3)
                                        {
                                            num3 = num22;
                                        }
                                    }
                                    else if (researchAbility.Value < 0)
                                    {
                                        float num23 = 1f + Math.Abs((float)researchAbility.Value / 100f);
                                        if (num23 > num4)
                                        {
                                            num4 = num23;
                                        }
                                    }
                                    break;
                                case TroopType.Artillery:
                                    troopCanRecruitArtillery = true;
                                    if (researchAbility.Value > 0)
                                    {
                                        float num20 = 1f + Math.Abs((float)researchAbility.Value / 100f);
                                        if (num20 > num6)
                                        {
                                            num6 = num20;
                                        }
                                    }
                                    else if (researchAbility.Value < 0)
                                    {
                                        float num21 = 1f + Math.Abs((float)researchAbility.Value / 100f);
                                        if (num21 > num5)
                                        {
                                            num5 = num21;
                                        }
                                    }
                                    break;
                                case TroopType.SpecialForces:
                                    troopCanRecruitSpecialForces = true;
                                    if (researchAbility.Value > 0)
                                    {
                                        float num14 = 1f + Math.Abs((float)researchAbility.Value / 100f);
                                        if (num14 > num7)
                                        {
                                            num7 = num14;
                                        }
                                    }
                                    else if (researchAbility.Value < 0)
                                    {
                                        float num15 = 1f + Math.Abs((float)researchAbility.Value / 100f);
                                        if (num15 > num8)
                                        {
                                            num8 = num15;
                                        }
                                    }
                                    break;
                            }
                        }
                        else if (researchAbility.Value < 0)
                        {
                            float num24 = Math.Abs((float)researchAbility.Value / 100f);
                            float num25 = 1f - num24;
                            if (num25 < num9)
                            {
                                num9 = num25;
                            }
                        }
                    }
                }
            }
            TroopCanRecruitInfantry = troopCanRecruitInfantry;
            TroopCanRecruitArmored = troopCanRecruitArmored;
            TroopCanRecruitArtillery = troopCanRecruitArtillery;
            TroopCanRecruitSpecialForces = troopCanRecruitSpecialForces;
            TroopAttackStrengthBonusFactorInfantry = num;
            TroopAttackStrengthBonusFactorArmored = num3;
            TroopAttackStrengthBonusFactorArtillery = num6;
            TroopAttackStrengthBonusFactorSpecialForces = num7;
            TroopDefendStrengthBonusFactorInfantry = num2;
            TroopDefendStrengthBonusFactorArmored = num4;
            TroopDefendStrengthBonusFactorSpecialForces = num8;
            TroopPlanetaryDefenseInterceptBonusFactor = num5;
            TroopMaintenanceFactor = num9 * BaconEmpire.MultiplyTroopMaintenance(this);
            BoardingAttackFactor = num10;
            BoardingDefenseFactor = num11;
        }

        public void ReviewCanBuildShipTypes()
        {
            bool canBuildCarriers = false;
            bool canBuildResupplyShips = false;
            if (Research != null && Research.Abilities != null && Research.Abilities.Count > 0)
            {
                for (int i = 0; i < Research.Abilities.Count; i++)
                {
                    ResearchAbility researchAbility = Research.Abilities[i];
                    if (researchAbility.Type == ResearchAbilityType.EnableShipSubRole && researchAbility.RelatedObject != null && researchAbility.RelatedObject is BuiltObjectSubRole)
                    {
                        switch ((BuiltObjectSubRole)researchAbility.RelatedObject)
                        {
                            case BuiltObjectSubRole.Carrier:
                                canBuildCarriers = true;
                                break;
                            case BuiltObjectSubRole.ResupplyShip:
                                canBuildResupplyShips = true;
                                break;
                        }
                    }
                }
            }
            _CanBuildCarriers = canBuildCarriers;
            _CanBuildResupplyShips = canBuildResupplyShips;
        }

        public void DoResearchBreakthrough(ResearchNode researchProject, bool selfResearched)
        {
            DoResearchBreakthrough(researchProject, selfResearched, blockMessages: false, suppressUpdate: false);
        }

        public void DoResearchBreakthrough(ResearchNode researchProject, bool selfResearched, bool blockMessages, bool suppressUpdate)
        {
            researchProject.IsResearched = true;
            researchProject.SelfResearched = selfResearched;
            researchProject.Progress = researchProject.Cost;
            short matchingGameEventIdResearchBreakthrough = _Galaxy.GetMatchingGameEventIdResearchBreakthrough(this, researchProject.ResearchNodeId);
            _Galaxy.CheckTriggerEvent(matchingGameEventIdResearchBreakthrough, this, EventTriggerType.ResearchBreakthrough, null);
            if ((researchProject.Components != null && researchProject.Components.Count > 0) || (researchProject.Abilities != null && researchProject.Abilities.Count > 0 && researchProject.Abilities[0].Type == ResearchAbilityType.ConstructionSize))
            {
                _ReviewDesignsAndRetrofit = true;
                bool flag = false;
                if (researchProject.Category == ComponentCategoryType.HyperDrive)
                {
                    Component latestComponent = Research.GetLatestComponent(ComponentType.HyperDrive);
                    if (latestComponent != null && latestComponent.Value1 < 5000 && researchProject.Components != null && researchProject.Components.Count > 0)
                    {
                        for (int i = 0; i < researchProject.Components.Count; i++)
                        {
                            Component component = researchProject.Components[i];
                            if (component != null && component.Type == ComponentType.HyperDrive && component.Value1 >= 5000)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                }
                if (researchProject.SpecialFunctionCode == 2 || flag)
                {
                    _ReviewDesignsAndRetrofitImportantBreakthrough = true;
                }
            }
            if (researchProject.SpecialFunctionCode == 2 && _ControlMilitaryFleets && ShipGroups != null)
            {
                ShipGroupList shipGroupList = new ShipGroupList();
                for (int j = 0; j < ShipGroups.Count; j++)
                {
                    ShipGroup shipGroup = ShipGroups[j];
                    if (shipGroup != null && shipGroup.LeadShip != null && shipGroup.LeadShip.IsAutoControlled && shipGroup.WarpSpeed <= 0)
                    {
                        shipGroupList.Add(shipGroup);
                    }
                }
                for (int k = 0; k < shipGroupList.Count; k++)
                {
                    ShipGroup shipGroup2 = shipGroupList[k];
                    if (shipGroup2 != null)
                    {
                        DisbandShipGroup(shipGroup2);
                    }
                }
            }
            if (Research.RecentProjects == null)
            {
                Research.RecentProjects = new ResearchNodeList();
            }
            Research.RecentProjects.Add(researchProject);
            switch (researchProject.Industry)
            {
                case IndustryType.Weapon:
                    if (Research.ResearchQueueWeapons.Contains(researchProject))
                    {
                        Research.ResearchQueueWeapons.Remove(researchProject);
                    }
                    break;
                case IndustryType.Energy:
                    if (Research.ResearchQueueEnergy.Contains(researchProject))
                    {
                        Research.ResearchQueueEnergy.Remove(researchProject);
                    }
                    break;
                case IndustryType.HighTech:
                    if (Research.ResearchQueueHighTech.Contains(researchProject))
                    {
                        Research.ResearchQueueHighTech.Remove(researchProject);
                    }
                    break;
            }
            object relatedObject = null;
            string text = string.Empty;
            if (researchProject.Abilities != null && researchProject.Abilities.Count > 0)
            {
                text = DoResearchAbilityBreakthrough(researchProject, out relatedObject) + ", ";
            }
            if (researchProject.SpecialFunctionCode == 2)
            {
                if (CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.DiscoverHyperspaceTech, researchProject) && this == _Galaxy.PlayerEmpire)
                {
                    _Galaxy.CheckGenerateAncientHelpers();
                }
            }
            else if (researchProject.SpecialFunctionCode == 4)
            {
                CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.DiscoverColonizationTech, researchProject);
            }
            if (!blockMessages)
            {
                if (researchProject.Components != null && researchProject.Components.Count > 0)
                {
                    text = text + " " + string.Format(TextResolver.GetText("the new component X for our ships and bases"), researchProject.Components[0].Name) + ", ";
                    relatedObject = researchProject.Components[0];
                }
                if (researchProject.ComponentImprovements != null && researchProject.ComponentImprovements.Count > 0)
                {
                    if (Research.ComponentImprovements[researchProject.ComponentImprovements[0].ImprovedComponent.ComponentID] == null)
                    {
                        text = text + " " + string.Format(TextResolver.GetText("improvements to the existing component X"), researchProject.ComponentImprovements[0].ImprovedComponent.Name) + ", ";
                        relatedObject = researchProject.ComponentImprovements[0].ImprovedComponent;
                    }
                    else if (researchProject.ComponentImprovements[0].TechLevel > Research.ComponentImprovements[researchProject.ComponentImprovements[0].ImprovedComponent.ComponentID].TechLevel)
                    {
                        text = text + " " + string.Format(TextResolver.GetText("improvements to the existing component X"), researchProject.ComponentImprovements[0].ImprovedComponent.Name) + ", ";
                        relatedObject = researchProject.ComponentImprovements[0].ImprovedComponent;
                    }
                }
                if (researchProject.PlanetaryFacility != null && !Research.BuildablePlanetaryFacilities.Contains(researchProject.PlanetaryFacility))
                {
                    text = text + " " + string.Format(TextResolver.GetText("the ability to build a new planetary facility X"), researchProject.PlanetaryFacility.Name) + ", ";
                    relatedObject = researchProject.PlanetaryFacility;
                }
                if (researchProject.PlagueChange != null)
                {
                    Plague plague = _Galaxy.Plagues[researchProject.PlagueChange.PlagueId];
                    text = ((!Research.EnabledPlagues.ContainsById(plague.PlagueId)) ? (text + " " + string.Format(TextResolver.GetText("creates the new PLAGUE"), plague.Name) + ", ") : (text + " " + string.Format(TextResolver.GetText("changes to PLAGUE"), plague.Name) + ", "));
                }
                if (researchProject.Fighters != null && researchProject.Fighters.Count > 0)
                {
                    for (int l = 0; l < researchProject.Fighters.Count; l++)
                    {
                        if (!Research.ResearchedFighters.Contains(researchProject.Fighters[l]))
                        {
                            text = text + " " + string.Format(TextResolver.GetText("access to a new fighter type X"), researchProject.Fighters[l].Name) + ", ";
                            relatedObject = researchProject.Fighters[l];
                        }
                    }
                }
                string text2 = string.Format(TextResolver.GetText("Our engineers have completed research in RESEARCHPROJECT"), researchProject.Name);
                if (!string.IsNullOrEmpty(text))
                {
                    text2 = text2 + ". " + string.Format(TextResolver.GetText("This breakthrough provides BENEFITS"), text);
                    text2 = text2.Substring(0, text2.Length - 2);
                }
                SendMessageToEmpire(this, EmpireMessageType.ResearchBreakthrough, researchProject, text2);
            }
            if (!suppressUpdate && Research != null)
            {
                Research.Update(DominantRace);
                ReviewDesignsBuiltObjectsImprovedComponents();
                ReviewColonizationTypes();
                ReviewPopulationGrowthRates();
                int newSize = 0;
                ReviewMaximumConstructionSize(out newSize);
                ReviewCanBuildShipTypes();
                ReviewTroopTypes();
            }
        }

        public double CalculatePirateResearchBonusFromFacilities()
        {
            double num = 1.0;
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat == null || habitat.HasBeenDestroyed)
                {
                    continue;
                }
                PirateColonyControl byFacilityControl = habitat.GetPirateControl().GetByFacilityControl();
                if (byFacilityControl == null || byFacilityControl.EmpireId != EmpireId || habitat.Facilities == null)
                {
                    continue;
                }
                for (int j = 0; j < habitat.Facilities.Count; j++)
                {
                    PlanetaryFacility planetaryFacility = habitat.Facilities[j];
                    if (planetaryFacility != null && planetaryFacility.ConstructionProgress >= 1f)
                    {
                        switch (planetaryFacility.Type)
                        {
                            case PlanetaryFacilityType.PirateBase:
                            case PlanetaryFacilityType.PirateFortress:
                            case PlanetaryFacilityType.PirateCriminalNetwork:
                                num += (double)planetaryFacility.Value1 / 100.0;
                                break;
                        }
                    }
                }
            }
            return num;
        }

        public BuiltObject IdentifyResearchStationHighestBonus(IndustryType industry)
        {
            BuiltObject result = null;
            float num = 0f;
            for (int i = 0; i < ResearchFacilities.Count; i++)
            {
                BuiltObject builtObject = ResearchFacilities[i];
                int num2 = 0;
                switch (industry)
                {
                    case IndustryType.Weapon:
                        num2 = builtObject.ResearchWeapons;
                        break;
                    case IndustryType.Energy:
                        num2 = builtObject.ResearchEnergy;
                        break;
                    case IndustryType.HighTech:
                        num2 = builtObject.ResearchHighTech;
                        break;
                }
                if (num2 > 0)
                {
                    float num3 = 0f;
                    if (builtObject.ParentHabitat != null && builtObject.ParentHabitat.ResearchBonusIndustry == industry && builtObject.ParentHabitat.ResearchBonus > 0)
                    {
                        num3 = (float)(int)builtObject.ParentHabitat.ResearchBonus / 100f;
                    }
                    else if (builtObject.NearestSystemStar != null && builtObject.NearestSystemStar.ResearchBonusIndustry == industry && builtObject.NearestSystemStar.ResearchBonus > 0)
                    {
                        num3 = (float)(int)builtObject.NearestSystemStar.ResearchBonus / 100f;
                    }
                    if (num3 > num)
                    {
                        num = num3;
                        result = builtObject;
                    }
                }
            }
            return result;
        }

        public void ReviewResearchStationBonuses()
        {
            BuiltObject researchBonusWeaponsStation = null;
            float num = 0f;
            BuiltObject researchBonusEnergyStation = null;
            float num2 = 0f;
            BuiltObject researchBonusHighTechStation = null;
            float num3 = 0f;
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = BuiltObjects[i];
                if (builtObject.Role != BuiltObjectRole.Base || (builtObject.ResearchWeapons <= 0 && builtObject.ResearchEnergy <= 0 && builtObject.ResearchHighTech <= 0))
                {
                    continue;
                }
                CharacterList characterList = new CharacterList();
                if (builtObject.Characters != null)
                {
                    characterList = builtObject.Characters.GetNonTransferringCharacters(CharacterRole.Scientist);
                }
                float num4 = (float)characterList.TotalDiminishingResearchBonusesWeapons();
                float num5 = (float)characterList.TotalDiminishingResearchBonusesEnergy();
                float num6 = (float)characterList.TotalDiminishingResearchBonusesHighTech();
                if (builtObject.ResearchWeapons > 0)
                {
                    float num7 = 0f;
                    if (builtObject.ParentHabitat != null && builtObject.ParentHabitat.ResearchBonusIndustry == IndustryType.Weapon && builtObject.ParentHabitat.ResearchBonus > 0)
                    {
                        num7 = (float)(int)builtObject.ParentHabitat.ResearchBonus / 100f;
                    }
                    else if (builtObject.NearestSystemStar != null && builtObject.NearestSystemStar.ResearchBonusIndustry == IndustryType.Weapon && builtObject.NearestSystemStar.ResearchBonus > 0)
                    {
                        num7 = (float)(int)builtObject.NearestSystemStar.ResearchBonus / 100f;
                    }
                    float num8 = num4 + num7;
                    if (num8 > num)
                    {
                        num = num8;
                        researchBonusWeaponsStation = builtObject;
                    }
                }
                if (builtObject.ResearchEnergy > 0)
                {
                    float num9 = 0f;
                    if (builtObject.ParentHabitat != null && builtObject.ParentHabitat.ResearchBonusIndustry == IndustryType.Energy && builtObject.ParentHabitat.ResearchBonus > 0)
                    {
                        num9 = (float)(int)builtObject.ParentHabitat.ResearchBonus / 100f;
                    }
                    else if (builtObject.NearestSystemStar != null && builtObject.NearestSystemStar.ResearchBonusIndustry == IndustryType.Energy && builtObject.NearestSystemStar.ResearchBonus > 0)
                    {
                        num9 = (float)(int)builtObject.NearestSystemStar.ResearchBonus / 100f;
                    }
                    float num10 = num5 + num9;
                    if (num10 > num2)
                    {
                        num2 = num10;
                        researchBonusEnergyStation = builtObject;
                    }
                }
                if (builtObject.ResearchHighTech > 0)
                {
                    float num11 = 0f;
                    if (builtObject.ParentHabitat != null && builtObject.ParentHabitat.ResearchBonusIndustry == IndustryType.HighTech && builtObject.ParentHabitat.ResearchBonus > 0)
                    {
                        num11 = (float)(int)builtObject.ParentHabitat.ResearchBonus / 100f;
                    }
                    else if (builtObject.NearestSystemStar != null && builtObject.NearestSystemStar.ResearchBonusIndustry == IndustryType.HighTech && builtObject.NearestSystemStar.ResearchBonus > 0)
                    {
                        num11 = (float)(int)builtObject.NearestSystemStar.ResearchBonus / 100f;
                    }
                    float num12 = num6 + num11;
                    if (num12 > num3)
                    {
                        num3 = num12;
                        researchBonusHighTechStation = builtObject;
                    }
                }
            }
            ResearchBonusWeaponsStation = researchBonusWeaponsStation;
            ResearchBonusWeapons = num;
            ResearchBonusEnergyStation = researchBonusEnergyStation;
            ResearchBonusEnergy = num2;
            ResearchBonusHighTechStation = researchBonusHighTechStation;
            ResearchBonusHighTech = num3;
        }

        public double CalculateResearchOutputBonuses(IndustryType industry)
        {
            double num = 1.0;
            if (ResearchBonus > 0.0)
            {
                num *= 1.0 + ResearchBonus;
            }
            if (GovernmentAttributes != null)
            {
                num *= GovernmentAttributes.ResearchSpeed;
            }
            switch (industry)
            {
                case IndustryType.Weapon:
                    if (_SpecialBonusResearchWeapons > 0.0)
                    {
                        num *= 1.0 + _SpecialBonusResearchWeapons;
                    }
                    if (ResearchBonusWeapons > 0f)
                    {
                        num *= (double)(1f + ResearchBonusWeapons);
                    }
                    break;
                case IndustryType.Energy:
                    if (_SpecialBonusResearchEnergy > 0.0)
                    {
                        num *= 1.0 + _SpecialBonusResearchEnergy;
                    }
                    if (ResearchBonusEnergy > 0f)
                    {
                        num *= (double)(1f + ResearchBonusEnergy);
                    }
                    break;
                case IndustryType.HighTech:
                    if (_SpecialBonusResearchHighTech > 0.0)
                    {
                        num *= 1.0 + _SpecialBonusResearchHighTech;
                    }
                    if (ResearchBonusHighTech > 0f)
                    {
                        num *= (double)(1f + ResearchBonusHighTech);
                    }
                    break;
            }
            double researchResourceBonus = GetResearchResourceBonus(industry);
            num *= 1.0 + researchResourceBonus;
            if (RaceEventType == RaceEventType.HistoricalDiscoveryExploreRuinsForResearchBoost)
            {
                num *= 1.1;
            }
            if (Leader != null)
            {
                switch (industry)
                {
                    case IndustryType.Weapon:
                        num *= 1.0 + (double)Leader.ResearchWeapons / 100.0;
                        break;
                    case IndustryType.Energy:
                        num *= 1.0 + (double)Leader.ResearchEnergy / 100.0;
                        break;
                    case IndustryType.HighTech:
                        num *= 1.0 + (double)Leader.ResearchHighTech / 100.0;
                        break;
                }
            }
            return num;
        }

        public double GetResearchResourceBonus(IndustryType industry)
        {
            double num = 0.0;
            if (Colonies != null)
            {
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat = Colonies[i];
                    if (habitat.ResourceBonuses != null)
                    {
                        switch (industry)
                        {
                            case IndustryType.Energy:
                                num += habitat.ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.ResearchEnergy) / 100.0;
                                break;
                            case IndustryType.HighTech:
                                num += habitat.ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.ResearchHighTech) / 100.0;
                                break;
                            case IndustryType.Weapon:
                                num += habitat.ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.ResearchWeapons) / 100.0;
                                break;
                        }
                    }
                }
                num = Math.Min(1.0, num);
            }
            return num;
        }

        private void CalculateResearchTotal(out double researchEnergy, out double researchHighTech, out double researchWeapons)
        {
            researchEnergy = ResearchEnergyPotential;
            researchWeapons = ResearchWeaponsPotential;
            researchHighTech = ResearchHighTechPotential;
            double num = 1.0 + researchEnergy + researchHighTech + researchWeapons;
            double annualResearchPotential = AnnualResearchPotential;
            double num2 = annualResearchPotential / num;
            if (num2 < 1.0)
            {
                researchEnergy *= num2;
                researchWeapons *= num2;
                researchHighTech *= num2;
            }
        }

        public Component ResolveLatestMissileWeapon()
        {
            return Research.GetLatestComponent(ComponentType.WeaponMissile);
        }

        public Component ResolveLatestStandardTorpedoWeapon()
        {
            Component component = null;
            for (int i = 0; i < Galaxy.ComponentDefinitionsStatic.Length; i++)
            {
                if (Galaxy.ComponentDefinitionsStatic[i].Category != ComponentCategoryType.WeaponTorpedo || Galaxy.ComponentDefinitionsStatic[i].Value1 <= 0 || Galaxy.ComponentDefinitionsStatic[i].Value7 > 0 || Galaxy.ComponentDefinitionsStatic[i].Type == ComponentType.WeaponMissile)
                {
                    continue;
                }
                Component component2 = new Component(Galaxy.ComponentDefinitionsStatic[i].ComponentID);
                if (!Research.CheckComponentResearched(component2))
                {
                    continue;
                }
                if (component == null)
                {
                    component = component2;
                    continue;
                }
                int num = Research.CalculateCurrentTechPoints(component, _Galaxy);
                int num2 = Research.CalculateCurrentTechPoints(component2, _Galaxy);
                if (num < num2)
                {
                    component = component2;
                }
            }
            return component;
        }

        public ComponentImprovement ResolveLatestBombardWeaponImprovement()
        {
            Component component = ResolveLatestBombardWeapon();
            ComponentImprovement result = null;
            if (component != null)
            {
                result = Research.ResolveImprovedComponentValues(component);
            }
            return result;
        }

        public Component ResolveLatestBombardWeapon()
        {
            Component component = null;
            for (int i = 0; i < Galaxy.ComponentDefinitionsStatic.Length; i++)
            {
                if (Galaxy.ComponentDefinitionsStatic[i].Category != ComponentCategoryType.WeaponTorpedo || Galaxy.ComponentDefinitionsStatic[i].Value7 <= 0)
                {
                    continue;
                }
                Component component2 = new Component(Galaxy.ComponentDefinitionsStatic[i].ComponentID);
                if (!Research.CheckComponentResearched(component2))
                {
                    continue;
                }
                if (component == null)
                {
                    component = component2;
                    continue;
                }
                int num = Research.CalculateCurrentTechPoints(component, _Galaxy);
                int num2 = Research.CalculateCurrentTechPoints(component2, _Galaxy);
                if (num < num2)
                {
                    component = component2;
                }
            }
            return component;
        }

        private int ResolveEmpireRaceTendency(Race race)
        {
            //int num = 0;
            if (race.AggressionLevel > race.CautionLevel && race.AggressionLevel > race.IntelligenceLevel)
            {
                return 3;
            }
            if (race.CautionLevel > race.AggressionLevel && race.CautionLevel > race.IntelligenceLevel)
            {
                return 2;
            }
            if (race.IntelligenceLevel > race.CautionLevel && race.IntelligenceLevel > race.AggressionLevel)
            {
                return 1;
            }
            return 0;
        }

        private ResearchNode SelectResearchNodeToCrash(ResearchNodeList potentialCrashProjects, List<ComponentCategoryType> targettedCategories, List<ComponentType> targettedTypes)
        {
            if (potentialCrashProjects != null)
            {
                if (targettedTypes != null && targettedTypes.Count > 0)
                {
                    int num = Galaxy.Rnd.Next(0, potentialCrashProjects.Count);
                    for (int i = num; i < potentialCrashProjects.Count; i++)
                    {
                        List<ComponentType> list = potentialCrashProjects[i].ResolveComponentTypesAll();
                        if (list.Count <= 0)
                        {
                            switch (potentialCrashProjects[i].ResolveResearchAbilityType())
                            {
                                case ResearchAbilityType.ColonizeHabitatType:
                                    list.Add(ComponentType.HabitationColonization);
                                    break;
                                case ResearchAbilityType.ConstructionSize:
                                    list.Add(ComponentType.ConstructionBuild);
                                    break;
                            }
                        }
                        if (list.Count > 0 && Component.TypesIntersect(list, targettedTypes))
                        {
                            return potentialCrashProjects[i];
                        }
                    }
                    for (int j = 0; j < num; j++)
                    {
                        List<ComponentType> list2 = potentialCrashProjects[j].ResolveComponentTypesAll();
                        if (list2.Count <= 0)
                        {
                            switch (potentialCrashProjects[j].ResolveResearchAbilityType())
                            {
                                case ResearchAbilityType.ColonizeHabitatType:
                                    list2.Add(ComponentType.HabitationColonization);
                                    break;
                                case ResearchAbilityType.ConstructionSize:
                                    list2.Add(ComponentType.ConstructionBuild);
                                    break;
                            }
                        }
                        if (list2.Count > 0 && Component.TypesIntersect(list2, targettedTypes))
                        {
                            return potentialCrashProjects[j];
                        }
                    }
                }
                if (targettedCategories != null && targettedCategories.Count > 0)
                {
                    int num2 = Galaxy.Rnd.Next(0, potentialCrashProjects.Count);
                    for (int k = num2; k < potentialCrashProjects.Count; k++)
                    {
                        if (targettedCategories.Contains(potentialCrashProjects[k].Category))
                        {
                            return potentialCrashProjects[k];
                        }
                    }
                    for (int l = 0; l < num2; l++)
                    {
                        if (targettedCategories.Contains(potentialCrashProjects[l].Category))
                        {
                            return potentialCrashProjects[l];
                        }
                    }
                }
            }
            return null;
        }

        private void DoCrashResearch()
        {
            if (this == _Galaxy.PlayerEmpire)
            {
                return;
            }
            ResearchNode researchNode = null;
            ResearchNodeList researchNodeList = new ResearchNodeList();
            if (Research.ResearchQueueEnergy != null && Research.ResearchQueueEnergy.Count > 0 && !Research.ResearchQueueEnergy[0].IsRushing)
            {
                researchNodeList.Add(Research.ResearchQueueEnergy[0]);
            }
            if (Research.ResearchQueueHighTech != null && Research.ResearchQueueHighTech.Count > 0 && !Research.ResearchQueueHighTech[0].IsRushing)
            {
                researchNodeList.Add(Research.ResearchQueueHighTech[0]);
            }
            if (Research.ResearchQueueWeapons != null && Research.ResearchQueueWeapons.Count > 0 && !Research.ResearchQueueWeapons[0].IsRushing)
            {
                researchNodeList.Add(Research.ResearchQueueWeapons[0]);
            }
            if (researchNodeList.Count <= 0 || Galaxy.Rnd.Next(0, 2) != 1)
            {
                return;
            }
            List<ComponentCategoryType> targettedCategories = new List<ComponentCategoryType>();
            List<ComponentType> targettedTypes = new List<ComponentType>();
            List<ComponentCategoryType> optimizedDesignCategories = new List<ComponentCategoryType>();
            List<ComponentType> optimizedDesignTypes = new List<ComponentType>();
            ComponentList raceAllowedComponents = new ComponentList();
            DeterminePreferredEmpireResearchFocuses(out targettedCategories, out targettedTypes, out optimizedDesignCategories, out optimizedDesignTypes, out raceAllowedComponents);
            for (int i = 0; i < raceAllowedComponents.Count; i++)
            {
                Component component = raceAllowedComponents[i];
                if (component != null && !targettedTypes.Contains(component.Type))
                {
                    targettedTypes.Add(component.Type);
                }
            }
            researchNode = SelectResearchNodeToCrash(researchNodeList, targettedCategories, targettedTypes);
            int num = researchNodeList.IndexBySpecialFunctionCode(2);
            if (num >= 0)
            {
                researchNode = researchNodeList[num];
            }
            if (researchNode == null)
            {
                switch (ResolveEmpireRaceTendency(DominantRace))
                {
                    case 0:
                        {
                            int index = Galaxy.Rnd.Next(0, researchNodeList.Count);
                            researchNode = researchNodeList[index];
                            if (Galaxy.Rnd.Next(0, 2) == 1)
                            {
                                researchNode = null;
                            }
                            break;
                        }
                    case 1:
                        targettedCategories.AddRange(new List<ComponentCategoryType>
                {
                    ComponentCategoryType.Reactor,
                    ComponentCategoryType.Construction
                });
                        targettedTypes.AddRange(new List<ComponentType>
                {
                    ComponentType.EngineMainThrust,
                    ComponentType.EngineVectoring,
                    ComponentType.ComputerTargetting,
                    ComponentType.ComputerCountermeasures
                });
                        researchNode = SelectResearchNodeToCrash(researchNodeList, targettedCategories, targettedTypes);
                        break;
                    case 2:
                        targettedCategories.AddRange(new List<ComponentCategoryType>
                {
                    ComponentCategoryType.Shields,
                    ComponentCategoryType.Sensor,
                    ComponentCategoryType.WeaponPointDefense
                });
                        targettedTypes.AddRange(new List<ComponentType> { ComponentType.Armor });
                        researchNode = SelectResearchNodeToCrash(researchNodeList, targettedCategories, targettedTypes);
                        break;
                    case 3:
                        targettedCategories.AddRange(new List<ComponentCategoryType>
                {
                    ComponentCategoryType.HyperDrive,
                    ComponentCategoryType.WeaponBeam,
                    ComponentCategoryType.WeaponTorpedo,
                    ComponentCategoryType.Fighter
                });
                        researchNode = SelectResearchNodeToCrash(researchNodeList, targettedCategories, targettedTypes);
                        break;
                }
            }
            if (researchNode != null)
            {
                double num2 = Galaxy.CalculateCrashResearchProgramCost(this, researchNode);
                double num3 = StateMoney * 0.7;
                if (DifficultyLevel < 1.0)
                {
                    num3 /= DifficultyLevel;
                }
                num3 = Math.Min(num3, StateMoney);
                if (num2 <= num3 && this != _Galaxy.PlayerEmpire && InitiateConstruction)
                {
                    InitiateCrashResearchProgram(researchNode, num2);
                }
            }
        }

        public void InitiateCrashResearchProgram(ResearchNode project, double cost)
        {
            if (StateMoney >= cost)
            {
                project.IsRushing = true;
                StateMoney -= cost;
                PirateEconomy.PerformExpense(cost, PirateExpenseType.CrashResearch, _Galaxy.CurrentStarDate);
            }
        }

        private void DeterminePreferredEmpireResearchFocuses(out List<ComponentCategoryType> targettedCategories, out List<ComponentType> targettedTypes, out List<ComponentCategoryType> optimizedDesignCategories, out List<ComponentType> optimizedDesignTypes, out ComponentList raceAllowedComponents)
        {
            targettedCategories = new List<ComponentCategoryType>();
            targettedTypes = new List<ComponentType>();
            optimizedDesignCategories = new List<ComponentCategoryType>();
            optimizedDesignTypes = new List<ComponentType>();
            raceAllowedComponents = new ComponentList();
            if (Policy != null)
            {
                List<ComponentCategoryType> list = new List<ComponentCategoryType>();
                List<ComponentType> list2 = new List<ComponentType>();
                if (Policy.ResearchDesignTechFocus1 != 0 && !list.Contains(Policy.ResearchDesignTechFocus1))
                {
                    list.Add(Policy.ResearchDesignTechFocus1);
                }
                if (Policy.ResearchDesignTechFocus2 != 0 && !list.Contains(Policy.ResearchDesignTechFocus2))
                {
                    list.Add(Policy.ResearchDesignTechFocus2);
                }
                if (Policy.ResearchDesignTechFocus3 != 0 && !list.Contains(Policy.ResearchDesignTechFocus3))
                {
                    list.Add(Policy.ResearchDesignTechFocus3);
                }
                if (Policy.ResearchDesignTechFocus4 != 0 && !list.Contains(Policy.ResearchDesignTechFocus4))
                {
                    list.Add(Policy.ResearchDesignTechFocus4);
                }
                if (Policy.ResearchDesignTechFocus5 != 0 && !list.Contains(Policy.ResearchDesignTechFocus5))
                {
                    list.Add(Policy.ResearchDesignTechFocus5);
                }
                if (Policy.ResearchDesignTechFocus6 != 0 && !list.Contains(Policy.ResearchDesignTechFocus6))
                {
                    list.Add(Policy.ResearchDesignTechFocus6);
                }
                if (Policy.ResearchDesignTechFocusType1 != 0 && !list2.Contains(Policy.ResearchDesignTechFocusType1))
                {
                    list2.Add(Policy.ResearchDesignTechFocusType1);
                }
                if (Policy.ResearchDesignTechFocusType2 != 0 && !list2.Contains(Policy.ResearchDesignTechFocusType2))
                {
                    list2.Add(Policy.ResearchDesignTechFocusType2);
                }
                if (Policy.ResearchDesignTechFocusType3 != 0 && !list2.Contains(Policy.ResearchDesignTechFocusType3))
                {
                    list2.Add(Policy.ResearchDesignTechFocusType3);
                }
                if (Policy.ResearchDesignTechFocusType4 != 0 && !list2.Contains(Policy.ResearchDesignTechFocusType4))
                {
                    list2.Add(Policy.ResearchDesignTechFocusType4);
                }
                if (Policy.ResearchDesignTechFocusType5 != 0 && !list2.Contains(Policy.ResearchDesignTechFocusType5))
                {
                    list2.Add(Policy.ResearchDesignTechFocusType5);
                }
                if (Policy.ResearchDesignTechFocusType6 != 0 && !list2.Contains(Policy.ResearchDesignTechFocusType6))
                {
                    list2.Add(Policy.ResearchDesignTechFocusType6);
                }
                targettedCategories.Clear();
                targettedTypes.Clear();
                if (list.Count > 0)
                {
                    targettedCategories.AddRange(list);
                }
                if (list2.Count > 0)
                {
                    targettedTypes.AddRange(list2);
                }
            }
            DetermineTechForUnbuildableOptimizedDesigns(out optimizedDesignCategories, out optimizedDesignTypes);
            if (optimizedDesignCategories.Count > 0)
            {
                targettedCategories.AddRange(optimizedDesignCategories);
            }
            if (optimizedDesignTypes.Count > 0)
            {
                targettedTypes.AddRange(optimizedDesignTypes);
            }
            if (DominantRace != null && DominantRace.SpecialComponent != null && !targettedTypes.Contains(DominantRace.SpecialComponent.Type))
            {
                targettedTypes.Add(DominantRace.SpecialComponent.Type);
            }
            if (DominantRace == null)
            {
                return;
            }
            raceAllowedComponents = _Galaxy.ResearchNodeDefinitions.ResolveRaceSpecificComponents(DominantRace, includeImprovements: true);
            if (raceAllowedComponents == null || raceAllowedComponents.Count <= 0)
            {
                return;
            }
            List<ComponentType> list3 = raceAllowedComponents.ResolveComponentTypes();
            for (int i = 0; i < list3.Count; i++)
            {
                if (!targettedTypes.Contains(list3[i]))
                {
                    targettedTypes.Add(list3[i]);
                }
            }
        }

        public void ResetAttitudeLevelsAtEndOfWar(DiplomaticRelation diplomaticRelation)
        {
            ResetAttitudeLevelsAtEndOfWar(diplomaticRelation, null);
        }

        public void ResetAttitudeLevelsAtEndOfWar(DiplomaticRelation diplomaticRelation, Empire specifiedWinner)
        {
            double winningRatio = 1.0;
            int loserRawDamageBuiltObject = 0;
            int loserRawDamageColony = 0;
            int winnerRawDamageBuiltObject = 0;
            int winnerRawDamageColony = 0;
            Empire loser = null;
            Empire empire = DetermineVictorInWar(diplomaticRelation, out winningRatio, out loser, out loserRawDamageBuiltObject, out loserRawDamageColony, out winnerRawDamageBuiltObject, out winnerRawDamageColony);
            if (specifiedWinner != null)
            {
                if (loser == specifiedWinner)
                {
                    loser = empire;
                }
                empire = specifiedWinner;
                winningRatio = 1.5;
            }
            if (winningRatio > 2.0)
            {
                EmpireEvaluation empireEvaluation = empire.ObtainEmpireEvaluation(loser);
                empireEvaluation.IncidentEvaluation = 0.0;
                empireEvaluation.GovernmentStyleAffinityCumulative = 0.0;
                empireEvaluation.RelationshipWithFriendsPositiveCumulative = 0.0;
                empireEvaluation.RelationshipWithFriendsNegativeCumulative = 0.0;
                empireEvaluation.SystemCompetitionCumulative = 0.0;
                empireEvaluation.CovetousnessCumulative = 0.0;
                EmpireEvaluation empireEvaluation2 = loser.ObtainEmpireEvaluation(empire);
                empireEvaluation2.IncidentEvaluation = Math.Max(-5.0, empireEvaluation2.IncidentEvaluation);
                empireEvaluation2.GovernmentStyleAffinityCumulative = 0.0;
                empireEvaluation2.RelationshipWithFriendsPositiveCumulative = 0.0;
                empireEvaluation2.RelationshipWithFriendsNegativeCumulative = 0.0;
                empireEvaluation2.SystemCompetitionCumulative = 0.0;
                empireEvaluation2.CovetousnessCumulative = 0.0;
            }
            else if (winningRatio > 1.3)
            {
                EmpireEvaluation empireEvaluation3 = empire.ObtainEmpireEvaluation(loser);
                empireEvaluation3.IncidentEvaluation = Math.Max(-3.0, empireEvaluation3.IncidentEvaluation);
                empireEvaluation3.GovernmentStyleAffinityCumulative = 0.0;
                empireEvaluation3.RelationshipWithFriendsPositiveCumulative = 0.0;
                empireEvaluation3.RelationshipWithFriendsNegativeCumulative = 0.0;
                empireEvaluation3.SystemCompetitionCumulative = 0.0;
                empireEvaluation3.CovetousnessCumulative = 0.0;
                EmpireEvaluation empireEvaluation4 = loser.ObtainEmpireEvaluation(empire);
                empireEvaluation4.SystemCompetitionCumulative = 0.0;
                empireEvaluation4.CovetousnessCumulative = 0.0;
                empireEvaluation4.IncidentEvaluation = Math.Max(-8.0, empireEvaluation4.IncidentEvaluation);
            }
            else
            {
                EmpireEvaluation empireEvaluation5 = empire.ObtainEmpireEvaluation(loser);
                empireEvaluation5.IncidentEvaluation = Math.Max(-5.0, empireEvaluation5.IncidentEvaluation);
                empireEvaluation5.SystemCompetitionCumulative = 0.0;
                empireEvaluation5.CovetousnessCumulative = 0.0;
                EmpireEvaluation empireEvaluation6 = loser.ObtainEmpireEvaluation(empire);
                empireEvaluation6.IncidentEvaluation = Math.Max(-10.0, empireEvaluation6.IncidentEvaluation);
                empireEvaluation6.SystemCompetitionCumulative = 0.0;
                empireEvaluation6.CovetousnessCumulative = 0.0;
            }
        }

        public void ClearOutlawsFromEmpire(Empire empire, Empire outlawEmpire)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            foreach (BuiltObject outlaw in empire.Outlaws)
            {
                if (outlaw.Empire == outlawEmpire)
                {
                    builtObjectList.Add(outlaw);
                }
            }
            foreach (BuiltObject item in builtObjectList)
            {
                empire.Outlaws.Remove(item);
            }
        }

        private void ClearAttackersFromEmpire(BuiltObject ship, Empire empire)
        {
            StellarObjectList stellarObjectList = new StellarObjectList();
            foreach (StellarObject attacker in ship.Attackers)
            {
                if (attacker is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)attacker;
                    if (builtObject.Empire == empire)
                    {
                        stellarObjectList.Add(attacker);
                    }
                }
            }
            foreach (StellarObject item in stellarObjectList)
            {
                ship.Attackers.Remove(item);
            }
        }

        private void CancelAttackMissionAgainstEmpireForSingleShip(BuiltObject ship, Empire empire)
        {
            ship.ClearAllMissionsForTarget(ship, empire, BuiltObjectMissionType.Attack, dropOutOfHyperspace: true);
            ship.ClearAllMissionsForTarget(ship, empire, BuiltObjectMissionType.WaitAndAttack, dropOutOfHyperspace: true);
            ship.ClearAllMissionsForTarget(ship, empire, BuiltObjectMissionType.Bombard, dropOutOfHyperspace: true);
            ship.ClearAllMissionsForTarget(ship, empire, BuiltObjectMissionType.WaitAndBombard, dropOutOfHyperspace: true);
            ship.ClearAllMissionsForTarget(ship, empire, BuiltObjectMissionType.Capture, dropOutOfHyperspace: true);
            ship.ClearAllMissionsForTarget(ship, empire, BuiltObjectMissionType.Raid, dropOutOfHyperspace: true);
            ClearAttackersFromEmpire(ship, empire);
        }

        private void CancelAttackMissionAgainstEmpireForSingleShipGroup(ShipGroup shipGroup, Empire empire)
        {
            if (shipGroup.Mission != null)
            {
                shipGroup.ClearAllMissionsForTarget(shipGroup, empire, BuiltObjectMissionType.Attack);
                shipGroup.ClearAllMissionsForTarget(shipGroup, empire, BuiltObjectMissionType.WaitAndAttack);
                shipGroup.ClearAllMissionsForTarget(shipGroup, empire, BuiltObjectMissionType.Bombard);
                shipGroup.ClearAllMissionsForTarget(shipGroup, empire, BuiltObjectMissionType.WaitAndBombard);
                shipGroup.ClearAllMissionsForTarget(shipGroup, empire, BuiltObjectMissionType.Capture);
                shipGroup.ClearAllMissionsForTarget(shipGroup, empire, BuiltObjectMissionType.Raid);
            }
        }

        public void CancelAttacksAgainstEmpire(Empire empire)
        {
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject ship = BuiltObjects[i];
                CancelAttackMissionAgainstEmpireForSingleShip(ship, empire);
            }
            for (int j = 0; j < PrivateBuiltObjects.Count; j++)
            {
                BuiltObject ship2 = PrivateBuiltObjects[j];
                CancelAttackMissionAgainstEmpireForSingleShip(ship2, empire);
            }
            if (ShipGroups != null)
            {
                for (int k = 0; k < ShipGroups.Count; k++)
                {
                    ShipGroup shipGroup = ShipGroups[k];
                    CancelAttackMissionAgainstEmpireForSingleShipGroup(shipGroup, empire);
                }
            }
            ClearOutlawsFromEmpire(this, empire);
        }

        public void ProcessEndOfWarWithEmpire(Empire empire)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
            diplomaticRelation.WarObjective = WarObjective.Undefined;
            diplomaticRelation.WarObjectiveColonies.Clear();
            diplomaticRelation.WarObjectiveBases.Clear();
            DiplomaticRelation diplomaticRelation2 = empire.ObtainDiplomaticRelation(this);
            diplomaticRelation2.WarObjective = WarObjective.Undefined;
            diplomaticRelation2.WarObjectiveColonies.Clear();
            diplomaticRelation2.WarObjectiveBases.Clear();
            CharacterList characterList = new CharacterList();
            characterList.AddRange(Characters.GetCharactersByRole(CharacterRole.FleetAdmiral));
            characterList.AddRange(Characters.GetCharactersByRole(CharacterRole.TroopGeneral));
            _Galaxy.DoCharacterEvent(CharacterEventType.WarEnded, empire, characterList, includeLeader: true, this);
            Counters.ProcessRelationChange(diplomaticRelation, this, DiplomaticRelationType.None, _Galaxy.CurrentStarDate, DiplomaticRelationType.War);
            if (diplomaticRelation.Type == DiplomaticRelationType.SubjugatedDominion && diplomaticRelation.Initiator != this)
            {
                _Galaxy.DoCharacterEventLeader(CharacterEventType.Subjugated, diplomaticRelation.Initiator, this);
            }
            diplomaticRelation.StartDateOfLastChange = _Galaxy.CurrentStarDate;
            diplomaticRelation2.StartDateOfLastChange = _Galaxy.CurrentStarDate;
            CancelAttacksAgainstEmpire(empire);
            CancelBlockades(empire);
            int num = CountEmpiresWeDeclaredWarOnNonLocked();
            int num2 = CountEmpiresWhoDeclaredWarOnUsNonLocked();
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < Outlaws.Count; i++)
            {
                BuiltObject builtObject = Outlaws[i];
                if (builtObject.Empire == empire)
                {
                    builtObjectList.Add(builtObject);
                }
            }
            foreach (BuiltObject item in builtObjectList)
            {
                Outlaws.Remove(item);
            }
            if (RecentAttackingEmpires.Contains(empire))
            {
                RecentAttackingEmpires.Remove(empire);
            }
            EmpireList empireList = DetermineEmpiresWarOrConquer();
            if (empireList.Contains(empire))
            {
                empireList.Remove(empire);
            }
            CheckAttackFleetTargets(empireList);
            if (!CheckAtWar())
            {
                EmpireList empireList2 = ResolveEmpiresToDefendAgainst();
                if (empireList2.Count <= 0)
                {
                    ClearDefendFleets();
                }
                else
                {
                    ReviewDefensiveFleetLocations();
                }
            }
        }

        public bool CheckHaveMetPirates(Empire empire)
        {
            if (empire != null && empire.PirateRelations != null)
            {
                for (int i = 0; i < empire.PirateRelations.Count; i++)
                {
                    PirateRelation pirateRelation = empire.PirateRelations[i];
                    if (pirateRelation != null && pirateRelation.Type != 0 && pirateRelation.OtherEmpire != null && pirateRelation.OtherEmpire.PirateEmpireBaseHabitat != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckEmpireHasColonizationTech(Empire empire)
        {
            if (empire != null && empire.Research != null)
            {
                Component latestComponent = empire.Research.GetLatestComponent(ComponentType.HabitationColonization);
                if (latestComponent != null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckPirateEmpireHasCriminalNetwork(Empire empire)
        {
            if (empire != null && empire.Colonies != null)
            {
                for (int i = 0; i < empire.Colonies.Count; i++)
                {
                    Habitat habitat = empire.Colonies[i];
                    if (habitat != null && habitat.Facilities != null)
                    {
                        int num = habitat.Facilities.CountCompletedByType(PlanetaryFacilityType.PirateCriminalNetwork);
                        if (num > 0 && habitat.GetPirateControl().CheckFactionHasControl(empire))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool CheckEmpireHasOwnedColonies(Empire empire)
        {
            if (empire != null && empire.Colonies != null)
            {
                for (int i = 0; i < empire.Colonies.Count; i++)
                {
                    Habitat habitat = empire.Colonies[i];
                    if (habitat != null && habitat.Owner == empire)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckEmpireHasHyperDriveTech(Empire empire)
        {
            if (empire != null && empire.Research != null)
            {
                Component latestComponent = empire.Research.GetLatestComponent(ComponentType.HyperDrive);
                if (latestComponent != null)
                {
                    return true;
                }
            }
            return false;
        }

        private void ConsiderTreatyProposals()
        {
            if (ControlDiplomacyTreaties != AutomationLevel.FullyAutomated && ControlDiplomacyOffense != AutomationLevel.FullyAutomated)
            {
                return;
            }
            int weightedMilitaryPotency = WeightedMilitaryPotency;
            int militaryPotency = MilitaryPotency;
            _ = _Galaxy.IntoleranceLevel;
            DiplomaticRelationList diplomaticRelationList = new DiplomaticRelationList();
            if (_ProposedDiplomaticRelations.Count > 0)
            {
                ReviewDiplomaticStrategies();
            }
            for (int i = 0; i < _ProposedDiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = _ProposedDiplomaticRelations[i];
                Empire thisEmpire = diplomaticRelation.ThisEmpire;
                EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(thisEmpire);
                int weightedMilitaryPotency2 = diplomaticRelation.ThisEmpire.WeightedMilitaryPotency;
                int militaryPotency2 = diplomaticRelation.ThisEmpire.MilitaryPotency;
                EvaluateMilitaryPotency(weightedMilitaryPotency, weightedMilitaryPotency2, thisEmpire);
                DiplomaticRelation diplomaticRelation2 = DiplomaticRelations[diplomaticRelation.ThisEmpire];
                if (diplomaticRelation2 == null)
                {
                    diplomaticRelation2 = new DiplomaticRelation(DiplomaticRelationType.NotMet, diplomaticRelation.Initiator, this, diplomaticRelation.ThisEmpire, tradeRestrictedResources: false);
                    DiplomaticRelations.Add(diplomaticRelation2);
                }
                DiplomaticRelation diplomaticRelation3 = diplomaticRelation.ThisEmpire.DiplomaticRelations[diplomaticRelation.OtherEmpire];
                if (diplomaticRelation3 == null)
                {
                    diplomaticRelation3 = new DiplomaticRelation(DiplomaticRelationType.NotMet, diplomaticRelation.Initiator, diplomaticRelation.ThisEmpire, diplomaticRelation.OtherEmpire, tradeRestrictedResources: false);
                    diplomaticRelation.ThisEmpire.DiplomaticRelations.Add(diplomaticRelation3);
                }
                diplomaticRelation3.LastDiplomacyTradeOfferDate = diplomaticRelation.LastDiplomacyTradeOfferDate;
                _ = diplomaticRelation.ThisEmpire.SpecialBonusDiplomacy;
                bool flag = false;
                if (diplomaticRelation2.Locked || diplomaticRelation3.Locked)
                {
                    continue;
                }
                if (diplomaticRelation.Type == DiplomaticRelationType.None && diplomaticRelation2.Type == DiplomaticRelationType.War && ControlDiplomacyOffense == AutomationLevel.FullyAutomated)
                {
                    diplomaticRelationList.Add(diplomaticRelation);
                    WarEndReason endReason = WarEndReason.Undefined;
                    if (ConsiderEndWar(thisEmpire, out endReason))
                    {
                        double winningRatio = 0.0;
                        int loserRawDamageBuiltObject = 0;
                        int loserRawDamageColony = 0;
                        int winnerRawDamageBuiltObject = 0;
                        int winnerRawDamageColony = 0;
                        Empire loser = null;
                        Empire empire = DetermineVictorInWar(diplomaticRelation2, out winningRatio, out loser, out loserRawDamageBuiltObject, out loserRawDamageColony, out winnerRawDamageBuiltObject, out winnerRawDamageColony);
                        if (empire != null && empire == this && DetermineWhetherWantToOfferSubjugation(this) && DetermineSubjugationOfLoserInWar(empire, loser, winningRatio, empire.MilitaryPotency, loser.MilitaryPotency))
                        {
                            DiplomaticRelation diplomaticRelation4 = new DiplomaticRelation(DiplomaticRelationType.SubjugatedDominion, this, this, diplomaticRelation.ThisEmpire, _Galaxy.CurrentStarDate, diplomaticRelation.SupplyRestrictedResources);
                            diplomaticRelation.ThisEmpire.ProposedDiplomaticRelations.Add(diplomaticRelation4);
                            continue;
                        }
                        ResetAttitudeLevelsAtEndOfWar(diplomaticRelation2);
                        diplomaticRelation2.Type = diplomaticRelation.Type;
                        diplomaticRelation2.LastDiplomacyTradeOfferDate = diplomaticRelation.LastDiplomacyTradeOfferDate;
                        diplomaticRelation3.Type = diplomaticRelation.Type;
                        diplomaticRelation3.LastDiplomacyTradeOfferDate = diplomaticRelation.LastDiplomacyTradeOfferDate;
                        flag = true;
                        SendMessageToEmpire(diplomaticRelation.ThisEmpire, EmpireMessageType.AcceptDiplomaticRelation, DiplomaticRelationType.None, TextResolver.GetText("We agree to end this war. We will cease hostilities immediately."));
                        ProcessEndOfWarWithEmpire(diplomaticRelation.ThisEmpire);
                        diplomaticRelation.ThisEmpire.ProcessEndOfWarWithEmpire(this);
                        SendNewsBroadcastWarStartEnd(diplomaticRelation2);
                        continue;
                    }
                }
                if (diplomaticRelation.Type == DiplomaticRelationType.SubjugatedDominion && !diplomaticRelation2.Locked)
                {
                    if (diplomaticRelation2.Type == DiplomaticRelationType.War)
                    {
                        if (ControlDiplomacyOffense == AutomationLevel.FullyAutomated)
                        {
                            diplomaticRelationList.Add(diplomaticRelation);
                            double winningRatio2 = 1.0;
                            int loserRawDamageBuiltObject2 = 0;
                            int loserRawDamageColony2 = 0;
                            int winnerRawDamageBuiltObject2 = 0;
                            int winnerRawDamageColony2 = 0;
                            Empire loser2 = null;
                            Empire empire2 = DetermineVictorInWar(diplomaticRelation, out winningRatio2, out loser2, out loserRawDamageBuiltObject2, out loserRawDamageColony2, out winnerRawDamageBuiltObject2, out winnerRawDamageColony2);
                            if (empire2 != this)
                            {
                                if (DetermineSubjugationOfLoserInWar(empire2, loser2, winningRatio2, empire2.MilitaryPotency, MilitaryPotency))
                                {
                                    ResetAttitudeLevelsAtEndOfWar(diplomaticRelation2);
                                    diplomaticRelation2.Type = DiplomaticRelationType.SubjugatedDominion;
                                    diplomaticRelation2.LastDiplomacyTradeOfferDate = diplomaticRelation.LastDiplomacyTradeOfferDate;
                                    diplomaticRelation2.Initiator = diplomaticRelation.ThisEmpire;
                                    diplomaticRelation3.Type = DiplomaticRelationType.SubjugatedDominion;
                                    diplomaticRelation3.LastDiplomacyTradeOfferDate = diplomaticRelation.LastDiplomacyTradeOfferDate;
                                    diplomaticRelation3.Initiator = diplomaticRelation.ThisEmpire;
                                    flag = true;
                                    SendMessageToEmpire(diplomaticRelation.ThisEmpire, EmpireMessageType.AcceptDiplomaticRelation, DiplomaticRelationType.SubjugatedDominion, TextResolver.GetText("We accept defeat and acknowledge your status as our ruler."));
                                    ProcessEndOfWarWithEmpire(diplomaticRelation.ThisEmpire);
                                    diplomaticRelation.ThisEmpire.ProcessEndOfWarWithEmpire(this);
                                    long num = long.MaxValue;
                                    int num2 = diplomaticRelation.ThisEmpire.EmpiresViewable.IndexOf(this);
                                    if (num2 >= 0)
                                    {
                                        diplomaticRelation.ThisEmpire.EmpiresViewableExpiry[num2] = num;
                                    }
                                    else
                                    {
                                        diplomaticRelation.ThisEmpire.EmpiresViewable.Add(this);
                                        diplomaticRelation.ThisEmpire.EmpiresViewableExpiry.Add(num);
                                    }
                                    SendNewsBroadcastWarStartEnd(diplomaticRelation2);
                                    _Galaxy.DoCharacterEventLeader(CharacterEventType.Subjugated, diplomaticRelation.ThisEmpire, this);
                                }
                                else
                                {
                                    SendMessageToEmpire(diplomaticRelation.ThisEmpire, EmpireMessageType.RefuseDiplomaticRelation, DiplomaticRelationType.SubjugatedDominion, TextResolver.GetText("We refuse to become your slaves - we will fight on."));
                                }
                            }
                            else
                            {
                                SendMessageToEmpire(diplomaticRelation.ThisEmpire, EmpireMessageType.RefuseDiplomaticRelation, DiplomaticRelationType.SubjugatedDominion, TextResolver.GetText("We will not accede to your outrageous demand for subjugation!"));
                            }
                            continue;
                        }
                    }
                    else if (ControlDiplomacyTreaties == AutomationLevel.FullyAutomated)
                    {
                        diplomaticRelationList.Add(diplomaticRelation);
                        SendMessageToEmpire(diplomaticRelation.ThisEmpire, EmpireMessageType.RefuseDiplomaticRelation, DiplomaticRelationType.SubjugatedDominion, TextResolver.GetText("We will not accede to your outrageous demand for subjugation!"));
                        empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - 12.0;
                        continue;
                    }
                }
                DiplomaticRelationType diplomaticRelationType = DetermineDesiredDiplomaticRelationTypical(diplomaticRelation2.Strategy, diplomaticRelation2.Type);
                bool flag2 = false;
                if (diplomaticRelation2.Type == DiplomaticRelationType.TradeSanctions)
                {
                    flag2 = true;
                }
                if (diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact && !diplomaticRelation2.Locked)
                {
                    if (ControlDiplomacyTreaties == AutomationLevel.FullyAutomated)
                    {
                        diplomaticRelationList.Add(diplomaticRelation);
                        if (!Reclusive && (CheckEmpireHasHyperDriveTech(this) || CheckEmpireHasHyperDriveTech(thisEmpire)))
                        {
                            double num3 = (double)militaryPotency / (double)militaryPotency2;
                            if (num3 > 5.0)
                            {
                                if (diplomaticRelationType == DiplomaticRelationType.MutualDefensePact || diplomaticRelationType == DiplomaticRelationType.Protectorate)
                                {
                                    DiplomaticRelation diplomaticRelation5 = new DiplomaticRelation(DiplomaticRelationType.Protectorate, this, this, diplomaticRelation.ThisEmpire, _Galaxy.CurrentStarDate, diplomaticRelation.SupplyRestrictedResources);
                                    diplomaticRelation.ThisEmpire.ProposedDiplomaticRelations.Add(diplomaticRelation5);
                                    continue;
                                }
                            }
                            else if (diplomaticRelationType == DiplomaticRelationType.MutualDefensePact)
                            {
                                diplomaticRelation2.Type = diplomaticRelation.Type;
                                diplomaticRelation2.LastDiplomacyTradeOfferDate = diplomaticRelation.LastDiplomacyTradeOfferDate;
                                diplomaticRelation2.StartDateOfLastChange = _Galaxy.CurrentStarDate;
                                diplomaticRelation3.Type = diplomaticRelation.Type;
                                diplomaticRelation3.LastDiplomacyTradeOfferDate = diplomaticRelation.LastDiplomacyTradeOfferDate;
                                diplomaticRelation3.StartDateOfLastChange = _Galaxy.CurrentStarDate;
                                diplomaticRelation2.MilitaryRefuelingToOther = true;
                                diplomaticRelation3.MilitaryRefuelingToOther = true;
                                if (flag2)
                                {
                                    CancelBlockades(diplomaticRelation.ThisEmpire);
                                    diplomaticRelation.ThisEmpire.CancelBlockades(this);
                                }
                                flag = true;
                                SetEmpireSharedVisibility(diplomaticRelation.ThisEmpire);
                                diplomaticRelation.ThisEmpire.SetEmpireSharedVisibility(this);
                                SendMessageToEmpire(diplomaticRelation.ThisEmpire, EmpireMessageType.AcceptDiplomaticRelation, diplomaticRelation.Type, TextResolver.GetText("We graciously accept your magnanimous treaty proposal!"));
                                continue;
                            }
                        }
                    }
                }
                else if (diplomaticRelation.Type == DiplomaticRelationType.Protectorate && !diplomaticRelation2.Locked && ControlDiplomacyTreaties == AutomationLevel.FullyAutomated)
                {
                    diplomaticRelationList.Add(diplomaticRelation);
                    if (!Reclusive && (diplomaticRelationType == DiplomaticRelationType.MutualDefensePact || diplomaticRelationType == DiplomaticRelationType.Protectorate) && (CheckEmpireHasHyperDriveTech(this) || CheckEmpireHasHyperDriveTech(thisEmpire)))
                    {
                        diplomaticRelation2.Type = diplomaticRelation.Type;
                        diplomaticRelation2.LastDiplomacyTradeOfferDate = diplomaticRelation.LastDiplomacyTradeOfferDate;
                        diplomaticRelation2.StartDateOfLastChange = _Galaxy.CurrentStarDate;
                        diplomaticRelation3.Type = diplomaticRelation.Type;
                        diplomaticRelation3.LastDiplomacyTradeOfferDate = diplomaticRelation.LastDiplomacyTradeOfferDate;
                        diplomaticRelation3.StartDateOfLastChange = _Galaxy.CurrentStarDate;
                        diplomaticRelation2.Initiator = diplomaticRelation.Initiator;
                        diplomaticRelation3.Initiator = diplomaticRelation.Initiator;
                        diplomaticRelation2.MilitaryRefuelingToOther = true;
                        diplomaticRelation3.MilitaryRefuelingToOther = true;
                        if (flag2)
                        {
                            CancelBlockades(diplomaticRelation.ThisEmpire);
                            diplomaticRelation.ThisEmpire.CancelBlockades(this);
                        }
                        flag = true;
                        SetEmpireSharedVisibility(diplomaticRelation.ThisEmpire);
                        diplomaticRelation.ThisEmpire.SetEmpireSharedVisibility(this);
                        SendMessageToEmpire(diplomaticRelation.ThisEmpire, EmpireMessageType.AcceptDiplomaticRelation, diplomaticRelation.Type, TextResolver.GetText("We graciously accept your magnanimous treaty proposal!"));
                        continue;
                    }
                }
                if (ControlDiplomacyTreaties == AutomationLevel.FullyAutomated)
                {
                    if (diplomaticRelation2.Type == DiplomaticRelationType.SubjugatedDominion && diplomaticRelation2.Initiator == this && diplomaticRelation.Type == DiplomaticRelationType.None && !diplomaticRelation2.Locked)
                    {
                        if (DetermineWhetherWantToEmancipate(diplomaticRelation2, empireEvaluation.OverallAttitude))
                        {
                            diplomaticRelation2.Type = diplomaticRelation.Type;
                            diplomaticRelation2.LastDiplomacyTradeOfferDate = diplomaticRelation.LastDiplomacyTradeOfferDate;
                            diplomaticRelation2.StartDateOfLastChange = _Galaxy.CurrentStarDate;
                            diplomaticRelation3.Type = diplomaticRelation.Type;
                            diplomaticRelation3.LastDiplomacyTradeOfferDate = diplomaticRelation.LastDiplomacyTradeOfferDate;
                            diplomaticRelation3.StartDateOfLastChange = _Galaxy.CurrentStarDate;
                            if (flag2)
                            {
                                CancelBlockades(diplomaticRelation.ThisEmpire);
                                diplomaticRelation.ThisEmpire.CancelBlockades(this);
                            }
                            int num4 = _EmpiresViewable.IndexOf(diplomaticRelation.ThisEmpire);
                            if (num4 >= 0)
                            {
                                _EmpiresViewable.RemoveAt(num4);
                                _EmpiresViewableExpiry.RemoveAt(num4);
                            }
                            flag = true;
                            SendMessageToEmpire(diplomaticRelation.ThisEmpire, EmpireMessageType.AcceptDiplomaticRelation, diplomaticRelation.Type, TextResolver.GetText("We agree to free you from subjugation to us."));
                            continue;
                        }
                    }
                    else
                    {
                        bool flag3 = false;
                        if (diplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement && !diplomaticRelation2.Locked && (CheckEmpireHasHyperDriveTech(this) || CheckEmpireHasHyperDriveTech(thisEmpire)) && diplomaticRelation2.Type != DiplomaticRelationType.FreeTradeAgreement && (diplomaticRelationType == DiplomaticRelationType.FreeTradeAgreement || diplomaticRelationType == DiplomaticRelationType.MutualDefensePact || diplomaticRelationType == DiplomaticRelationType.Protectorate))
                        {
                            diplomaticRelationList.Add(diplomaticRelation);
                            if (!Reclusive)
                            {
                                flag3 = true;
                            }
                        }
                        if (flag3)
                        {
                            if (diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || diplomaticRelation.Type == DiplomaticRelationType.Protectorate)
                            {
                                diplomaticRelation.ThisEmpire.SetEmpireSharedVisibility(this);
                                SetEmpireSharedVisibility(diplomaticRelation.ThisEmpire);
                            }
                            else if (diplomaticRelation2.Type == DiplomaticRelationType.MutualDefensePact || diplomaticRelation2.Type == DiplomaticRelationType.Protectorate)
                            {
                                if (diplomaticRelation.Type != DiplomaticRelationType.Protectorate && diplomaticRelation.Type != DiplomaticRelationType.MutualDefensePact)
                                {
                                    diplomaticRelation.ThisEmpire.ClearEmpireSharedVisibility(this);
                                    ClearEmpireSharedVisibility(diplomaticRelation.ThisEmpire);
                                }
                            }
                            else if (diplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement)
                            {
                                if (!diplomaticRelation2.OtherEmpire.CheckWhetherKnowAnySystemsOfOtherEmpire(this))
                                {
                                    Habitat habitat = _Galaxy.FastFindNearestColony((int)diplomaticRelation2.OtherEmpire.Capital.Xpos, (int)diplomaticRelation2.OtherEmpire.Capital.Ypos, this, 0);
                                    if (habitat != null)
                                    {
                                        SystemVisibilityStatus status = diplomaticRelation2.OtherEmpire.SystemVisibility[habitat.SystemIndex].Status;
                                        if (status != SystemVisibilityStatus.Visible)
                                        {
                                            diplomaticRelation2.OtherEmpire.SetSystemVisibility(habitat, SystemVisibilityStatus.Explored);
                                        }
                                    }
                                }
                                if (!CheckWhetherKnowAnySystemsOfOtherEmpire(diplomaticRelation2.OtherEmpire))
                                {
                                    Habitat habitat2 = _Galaxy.FastFindNearestColony((int)Capital.Xpos, (int)Capital.Ypos, diplomaticRelation2.OtherEmpire, 0);
                                    if (habitat2 != null)
                                    {
                                        SystemVisibilityStatus status2 = SystemVisibility[habitat2.SystemIndex].Status;
                                        if (status2 != SystemVisibilityStatus.Visible)
                                        {
                                            SetSystemVisibility(habitat2, SystemVisibilityStatus.Explored);
                                        }
                                    }
                                }
                            }
                            diplomaticRelation2.Type = diplomaticRelation.Type;
                            diplomaticRelation2.LastDiplomacyTradeOfferDate = diplomaticRelation.LastDiplomacyTradeOfferDate;
                            diplomaticRelation2.StartDateOfLastChange = _Galaxy.CurrentStarDate;
                            diplomaticRelation3.Type = diplomaticRelation.Type;
                            diplomaticRelation3.LastDiplomacyTradeOfferDate = diplomaticRelation.LastDiplomacyTradeOfferDate;
                            diplomaticRelation3.StartDateOfLastChange = _Galaxy.CurrentStarDate;
                            if (flag2)
                            {
                                CancelBlockades(diplomaticRelation.ThisEmpire);
                                diplomaticRelation.ThisEmpire.CancelBlockades(this);
                            }
                            flag = true;
                            SendMessageToEmpire(diplomaticRelation.ThisEmpire, EmpireMessageType.AcceptDiplomaticRelation, diplomaticRelation.Type, TextResolver.GetText("We graciously accept your magnanimous treaty proposal!"));
                            continue;
                        }
                    }
                }
                if (!flag)
                {
                    diplomaticRelationList.Add(diplomaticRelation);
                    SendMessageToEmpire(diplomaticRelation.ThisEmpire, EmpireMessageType.RefuseDiplomaticRelation, diplomaticRelation.Type, TextResolver.GetText("We reject your treaty proposal"));
                }
            }
            foreach (DiplomaticRelation item in diplomaticRelationList)
            {
                _ProposedDiplomaticRelations.Remove(item);
            }
        }

        private DiplomaticRelationType UpgradeDesiredDiplomaticRelationTypeIfAtWar(Empire offeringEmpire, int ourMilitaryPotency, double galaxyIntoleranceLevel, double diplomacyFactor)
        {
            //DiplomaticRelationType diplomaticRelationType = DiplomaticRelationType.None;
            int militaryStrength = 0;
            DetermineEmpiresAtWarWith(out militaryStrength);
            double num = (double)ourMilitaryPotency / (double)militaryStrength;
            double num2 = Math.Pow((double)DominantRace.AggressionLevel / 100.0, 2.0);
            double num3 = 0.6 / num2;
            if (num < num3)
            {
                DiplomaticRelation currentDiplomaticRelation = ObtainDiplomaticRelation(offeringEmpire);
                EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(offeringEmpire);
                int overallAttitude = empireEvaluation.OverallAttitude;
                overallAttitude += 15;
                return ResolveDesiredDiplomaticRelationType(currentDiplomaticRelation, overallAttitude, DominantRace.IntelligenceLevel, DominantRace.FriendlinessLevel, DominantRace.LoyaltyLevel, DominantRace.AggressionLevel, WarWeariness, galaxyIntoleranceLevel, diplomacyFactor);
            }
            return ResolveDesiredDiplomaticRelationType(offeringEmpire, galaxyIntoleranceLevel, diplomacyFactor);
        }

        public double LeaveSystem(Habitat systemStar)
        {
            double num = 0.0;
            int num2 = 536870911;
            BuiltObject builtObject = null;
            for (int i = 0; i < RefuellingDepots.Count; i++)
            {
                BuiltObject builtObject2 = RefuellingDepots[i];
                if (builtObject2.NearestSystemStar != systemStar)
                {
                    int num3 = (int)_Galaxy.CalculateDistance(systemStar.Xpos, systemStar.Ypos, builtObject2.Xpos, builtObject2.Ypos);
                    if (num3 < num2)
                    {
                        num2 = num3;
                        builtObject = builtObject2;
                    }
                }
            }
            if (builtObject != null)
            {
                for (int j = 0; j < BuiltObjects.Count; j++)
                {
                    BuiltObject builtObject3 = BuiltObjects[j];
                    if (builtObject3.Role == BuiltObjectRole.Base)
                    {
                        continue;
                    }
                    int num4 = (int)_Galaxy.CalculateDistance(builtObject3.Xpos, builtObject3.Ypos, systemStar.Xpos, systemStar.Ypos);
                    if (builtObject3.NearestSystemStar == systemStar || num4 <= Galaxy.MaxSolarSystemSize)
                    {
                        if (builtObject3.ShipGroup != null && (builtObject3.ShipGroup.Mission == null || builtObject3.ShipGroup.Mission.Type == BuiltObjectMissionType.Undefined || builtObject3.ShipGroup.Mission.Type == BuiltObjectMissionType.MoveAndWait || builtObject3.ShipGroup.Mission.Type == BuiltObjectMissionType.Hold))
                        {
                            builtObject3.ShipGroup.AssignMission(BuiltObjectMissionType.Move, builtObject, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                            num += 10.0;
                        }
                        else if (builtObject3.ShipGroup == null && builtObject3.TopSpeed > 0 && builtObject != null)
                        {
                            builtObject3.ClearPreviousMissionRequirements();
                            builtObject3.AssignMission(BuiltObjectMissionType.Move, builtObject, null, BuiltObjectMissionPriority.Normal);
                            num += (double)builtObject3.FirepowerRaw / 20.0;
                        }
                    }
                }
            }
            return num;
        }

        private bool CheckSufficientCashflow(double expense, double allowableRatio)
        {
            double num = Math.Max(0.0, CalculateAnnualCashflow() * allowableRatio);
            if (expense < num)
            {
                return true;
            }
            return false;
        }

        private bool CheckSufficientFunds(double cost, double allowableRatio)
        {
            double num = Math.Max(0.0, StateMoney * allowableRatio);
            if (cost < num)
            {
                return true;
            }
            return false;
        }

        public double ValueMoneyGiftFromEmpire(Empire giver, double amount)
        {
            double num = giver.StateMoney / 8.0;
            double num2 = Math.Sqrt(Math.Sqrt(amount)) * (amount / num);
            if (PirateEmpireBaseHabitat == null && giver.PirateEmpireBaseHabitat == null)
            {
                DiplomaticRelation diplomaticRelation = giver.ObtainDiplomaticRelation(this);
                double num3 = Math.Min(1.0, (double)(_Galaxy.CurrentStarDate - diplomaticRelation.LastGiftDate) / (double)Galaxy.IdealTimeBetweenGifts);
                num2 *= num3;
            }
            int num4 = ObtainAttitude(giver);
            if (num4 < 0)
            {
                double num5 = Math.Max(1.0, (double)num4 / -2.5);
                num2 /= num5;
            }
            num2 = Math.Max(0.0, Math.Min(num2, 15.0));
            return num2 * (1.0 + giver.SpecialBonusDiplomacy);
        }

        public int RemoveMilitaryForcesFromSystem(Habitat systemStar, Empire requester)
        {
            int num = DetermineRelativeStrength(MilitaryPotency, requester);
            bool flag = false;
            if (PirateEmpireBaseHabitat == null && requester.PirateEmpireBaseHabitat == null)
            {
                DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(requester);
                if (diplomaticRelation.Strategy == DiplomaticStrategy.Ally || diplomaticRelation.Strategy == DiplomaticStrategy.Befriend || diplomaticRelation.Strategy == DiplomaticStrategy.DefendPlacate || diplomaticRelation.Strategy == DiplomaticStrategy.Placate || num < 0)
                {
                    flag = true;
                }
            }
            else
            {
                PirateRelation pirateRelation = ObtainPirateRelation(requester);
                if (pirateRelation.Type == PirateRelationType.Protection || pirateRelation.Evaluation > 10f)
                {
                    flag = true;
                }
            }
            if (flag)
            {
                int num2 = 0;
                int num3 = 536870911;
                BuiltObject builtObject = null;
                for (int i = 0; i < RefuellingDepots.Count; i++)
                {
                    BuiltObject builtObject2 = RefuellingDepots[i];
                    if (builtObject2.NearestSystemStar != systemStar)
                    {
                        int num4 = (int)_Galaxy.CalculateDistance(systemStar.Xpos, systemStar.Ypos, builtObject2.Xpos, builtObject2.Ypos);
                        if (num4 < num3)
                        {
                            num3 = num4;
                            builtObject = builtObject2;
                        }
                    }
                }
                if (builtObject != null)
                {
                    for (int j = 0; j < BuiltObjects.Count; j++)
                    {
                        if (BuiltObjects[j].Role == BuiltObjectRole.Military && BuiltObjects[j].FirepowerRaw > 0 && BuiltObjects[j].NearestSystemStar == systemStar)
                        {
                            BuiltObjects[j].ClearPreviousMissionRequirements();
                            BuiltObjects[j].AssignMission(BuiltObjectMissionType.Refuel, builtObject, null, BuiltObjectMissionPriority.Unavailable);
                            num2++;
                        }
                    }
                }
                if (num2 > 0)
                {
                    return 1;
                }
                return 0;
            }
            return -1;
        }

        public double CalculateAnnualCashflow()
        {
            if (PirateEmpireBaseHabitat != null)
            {
                return CalculatePirateCashflow();
            }
            double num = AnnualStateMaintenanceExcludingUnderConstruction + ThisYearsStateFuelCosts + AnnualTroopMaintenance + AnnualSubjugationTribute + AnnualPirateProtection;
            double num2 = AnnualTaxRevenue + ThisYearsForeignTradeBonuses + ThisYearsSpacePortIncome + ThisYearsResortIncome + CalculateAnnualSubjugationTributeIncome();
            if (GovernmentAttributes != null && GovernmentAttributes.SpecialFunctionCode == 1)
            {
                num += AnnualPrivateMaintenanceExcludingUnderConstruction;
            }
            return num2 - num;
        }

        public double CalculateAccurateAnnualCashflowIncludingUnderConstruction(out double annualEmpireExpenses)
        {
            annualEmpireExpenses = 0.0;
            if (PirateEmpireBaseHabitat != null)
            {
                return CalculatePirateCashflow(includeShipsUnderConstruction: true);
            }
            annualEmpireExpenses = AnnualStateMaintenance + AnnualTroopMaintenanceIncludeRecruiting + AnnualSubjugationTribute + AnnualPirateProtection + AnnualFacilityMaintenance;
            double num = AnnualTaxRevenue + CalculateAnnualSubjugationTributeIncome();
            if (GovernmentAttributes != null && GovernmentAttributes.SpecialFunctionCode == 1)
            {
                annualEmpireExpenses += AnnualPrivateMaintenance;
            }
            return num - annualEmpireExpenses;
        }

        public double CalculateAccurateAnnualCashflow()
        {
            if (PirateEmpireBaseHabitat != null)
            {
                return CalculatePirateCashflow();
            }
            double num = AnnualStateMaintenanceExcludingUnderConstruction + AnnualTroopMaintenance + AnnualSubjugationTribute + AnnualPirateProtection;
            double num2 = AnnualTaxRevenue + CalculateAnnualSubjugationTributeIncome();
            if (GovernmentAttributes != null && GovernmentAttributes.SpecialFunctionCode == 1)
            {
                num += AnnualPrivateMaintenanceExcludingUnderConstruction;
            }
            return num2 - num;
        }

        public double CalculatePirateCashflow()
        {
            return CalculatePirateCashflow(includeShipsUnderConstruction: false);
        }

        public double CalculatePirateCashflow(bool includeShipsUnderConstruction)
        {
            double num = CalculatePirateIncome();
            double num2 = CalculatePirateExpenses(includeShipsUnderConstruction);
            return num - num2;
        }

        public double CalculatePirateExpenses()
        {
            return CalculatePirateExpenses(includeShipsUnderConstruction: false);
        }

        public double CalculatePirateExpenses(bool includeShipsUnderConstruction)
        {
            double num = 0.0;
            num = ((!includeShipsUnderConstruction) ? (AnnualStateMaintenanceExcludingUnderConstruction + AnnualFacilityMaintenance) : (AnnualStateMaintenance + AnnualFacilityMaintenance));
            return num + AnnualTroopMaintenance;
        }

        public double CalculatePirateIncome()
        {
            double num = 0.0;
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat != null && !habitat.HasBeenDestroyed)
                {
                    PirateColonyControl byFaction = habitat.GetPirateControl().GetByFaction(this);
                    if (byFaction != null && byFaction.EmpireId == EmpireId)
                    {
                        double num2 = habitat.AnnualRevenue * (double)byFaction.ControlLevel * 0.5;
                        num += num2;
                    }
                }
            }
            for (int j = 0; j < PirateRelations.Count; j++)
            {
                PirateRelation pirateRelation = PirateRelations[j];
                if (pirateRelation != null && pirateRelation.Type == PirateRelationType.Protection)
                {
                    num += pirateRelation.MonthlyProtectionFeeToThisEmpire * 12.0;
                }
            }
            if (PirateEconomy != null)
            {
                double num3 = (PirateEconomy.ThisYear.TotalIncome - PirateEconomy.ThisYear.StableIncome) / 2.0;
                if (PirateEconomy.LastYear != null)
                {
                    double num4 = PirateEconomy.LastYear.TotalIncome - PirateEconomy.LastYear.StableIncome;
                    num3 += num4 / 2.0;
                    num3 /= 2.0;
                }
                num += num3;
            }
            return num;
        }

        public double CalculateAccurateAnnualIncome()
        {
            if (PirateEmpireBaseHabitat != null)
            {
                return CalculatePirateIncome();
            }
            double num = AnnualTaxRevenue + CalculateAnnualSubjugationTributeIncome();
            if (GovernmentAttributes != null && GovernmentAttributes.SpecialFunctionCode == 1)
            {
                num -= AnnualPrivateMaintenanceExcludingUnderConstruction;
            }
            return num;
        }

        public void AcceptPirateProtection(Empire pirateEmpire, double monthlyPrice)
        {
            pirateEmpire.ChangePirateRelation(this, PirateRelationType.Protection, _Galaxy.CurrentStarDate, monthlyPrice);
            int num = pirateEmpire.PirateMissions.IndexOfTarget(this, EmpireActivityType.Attack);
            int iterationCount = 0;
            while (Galaxy.ConditionCheckLimit(num >= 0, 200, ref iterationCount))
            {
                EmpireActivity empireActivity = pirateEmpire.PirateMissions[num];
                if (empireActivity != null && empireActivity.RequestingEmpire != null && empireActivity.RequestingEmpire != pirateEmpire && pirateEmpire != null)
                {
                    PirateRelation pirateRelation = empireActivity.RequestingEmpire.ObtainPirateRelation(pirateEmpire);
                    pirateRelation.EvaluationPirateMissionsFail -= 20f;
                }
                pirateEmpire.PirateMissions.RemoveAt(num);
                num = pirateEmpire.PirateMissions.IndexOfTarget(this, EmpireActivityType.Attack);
            }
            if (monthlyPrice > 0.0)
            {
                StateMoney -= monthlyPrice;
                pirateEmpire.StateMoney += monthlyPrice;
                pirateEmpire.PirateEconomy.PerformIncome(monthlyPrice, PirateIncomeType.ProtectionAgreement, _Galaxy.CurrentStarDate);
                pirateEmpire.Counters.PirateProtectionIncome += monthlyPrice;
            }
            pirateEmpire.CancelAttacksAgainstEmpire(this);
            CancelAttacksAgainstEmpire(pirateEmpire);
        }

        private void ProcessMessages()
        {
            EmpireMessage[] array = ListHelper.ToArrayThreadSafe(_Messages);
            _ = _Galaxy.IntoleranceLevel;
            foreach (EmpireMessage empireMessage in array)
            {
                if (empireMessage == null)
                {
                    continue;
                }
                Empire empire = null;
                Empire empire2 = null;
                EmpireEvaluation empireEvaluation = null;
                DiplomaticRelation diplomaticRelation = null;
                double num = 0.0;
                switch (empireMessage.MessageType)
                {
                    case EmpireMessageType.RemoveForcesFromSystem:
                        if (this != _Galaxy.PlayerEmpire)
                        {
                            Habitat systemStar = (Habitat)empireMessage.Subject;
                            RemoveMilitaryForcesFromSystem(systemStar, empireMessage.Sender);
                        }
                        break;
                    case EmpireMessageType.CancelPirateProtection:
                        if (PirateEmpireBaseHabitat != null && empireMessage.Sender != null)
                        {
                            PirateRelation pirateRelation3 = ObtainPirateRelation(empireMessage.Sender);
                            float evaluationChangeAmount = pirateRelation3.CalculateOffenseOverCancellingProtection(_Galaxy.CurrentStarDate);
                            ChangePirateEvaluation(empireMessage.Sender, evaluationChangeAmount, PirateRelationEvaluationType.ProtectionCancelled);
                        }
                        break;
                    case EmpireMessageType.PirateOfferProtection:
                        if (_ControlDiplomacyTreaties == AutomationLevel.FullyAutomated && DetermineDesirePirateProtection(empireMessage.Sender))
                        {
                            double num13 = empireMessage.Money;
                            if (num13 <= 0.0)
                            {
                                num13 = empireMessage.Sender.CalculatePirateProtectionPricePerMonth(this);
                            }
                            AcceptPirateProtection(empireMessage.Sender, num13);
                        }
                        break;
                    case EmpireMessageType.SellInfoUnmetEmpire:
                        {
                            if (this == _Galaxy.PlayerEmpire || PirateEmpireBaseHabitat != null || Galaxy.Rnd.Next(0, 2) != 1 || !CheckSufficientFunds(empireMessage.Money, 0.2) || !(empireMessage.Subject is Empire))
                            {
                                break;
                            }
                            empire = (Empire)empireMessage.Subject;
                            int num3 = 0;
                            for (int j = 0; j < DiplomaticRelations.Count; j++)
                            {
                                DiplomaticRelation diplomaticRelation4 = DiplomaticRelations[j];
                                if (diplomaticRelation4.Type != 0)
                                {
                                    num3++;
                                }
                            }
                            double num4 = (double)num3 / (double)_Galaxy.Empires.Count;
                            if (num4 < 0.4)
                            {
                                string title = string.Format(TextResolver.GetText("Inform Empire Their Contact Details Sold Title"), Name);
                                string description = string.Format(TextResolver.GetText("Inform Empire Their Contact Details Sold"), empireMessage.Sender.Name, Name);
                                empire.SendMessageToEmpireWithTitle(empire, EmpireMessageType.GeneralNeutralEvent, null, description, title);
                                DiplomaticRelation diplomaticRelation5 = ObtainDiplomaticRelation(empire);
                                diplomaticRelation5.Type = DiplomaticRelationType.None;
                                diplomaticRelation5 = empire.ObtainDiplomaticRelation(this);
                                diplomaticRelation5.Type = DiplomaticRelationType.None;
                                StateMoney -= empireMessage.Money;
                                empireMessage.Sender.StateMoney += empireMessage.Money;
                                empireMessage.Sender.PirateEconomy.PerformIncome(empireMessage.Money, PirateIncomeType.SellInfo, _Galaxy.CurrentStarDate);
                            }
                            break;
                        }
                    case EmpireMessageType.SellInfoSystemMap:
                        {
                            if (this == _Galaxy.PlayerEmpire || PirateEmpireBaseHabitat != null || Galaxy.Rnd.Next(0, 2) != 1 || !CheckSufficientFunds(empireMessage.Money, 0.1) || !(empireMessage.Subject is Habitat))
                            {
                                break;
                            }
                            Habitat habitat4 = (Habitat)empireMessage.Subject;
                            int num11 = 0;
                            for (int k = 0; k < DiplomaticRelations.Count; k++)
                            {
                                DiplomaticRelation diplomaticRelation6 = DiplomaticRelations[k];
                                if (diplomaticRelation6.Type != 0)
                                {
                                    num11++;
                                }
                            }
                            double num12 = (double)num11 / (double)_Galaxy.Empires.Count;
                            if (num12 < 0.4)
                            {
                                SystemVisibility[habitat4.SystemIndex].Status = SystemVisibilityStatus.Explored;
                                StateMoney -= empireMessage.Money;
                                empireMessage.Sender.StateMoney += empireMessage.Money;
                                empireMessage.Sender.PirateEconomy.PerformIncome(empireMessage.Money, PirateIncomeType.SellInfo, _Galaxy.CurrentStarDate);
                            }
                            break;
                        }
                    case EmpireMessageType.SellInfoIndependentColony:
                        if (this != _Galaxy.PlayerEmpire && Galaxy.Rnd.Next(0, 2) == 1 && CheckSufficientFunds(empireMessage.Money, 0.2) && empireMessage.Subject is Habitat)
                        {
                            Habitat habitat5 = (Habitat)empireMessage.Subject;
                            if (Colonies.Count < 8)
                            {
                                SystemVisibility[habitat5.SystemIndex].Status = SystemVisibilityStatus.Explored;
                                StateMoney -= empireMessage.Money;
                                empireMessage.Sender.StateMoney += empireMessage.Money;
                                empireMessage.Sender.PirateEconomy.PerformIncome(empireMessage.Money, PirateIncomeType.SellInfo, _Galaxy.CurrentStarDate);
                            }
                        }
                        break;
                    case EmpireMessageType.SellInfoRuins:
                        if (this != _Galaxy.PlayerEmpire && Galaxy.Rnd.Next(0, 2) == 1 && CheckSufficientFunds(empireMessage.Money, 0.2) && empireMessage.Subject is Habitat)
                        {
                            Habitat habitat3 = (Habitat)empireMessage.Subject;
                            SystemVisibility[habitat3.SystemIndex].Status = SystemVisibilityStatus.Explored;
                            StateMoney -= empireMessage.Money;
                            empireMessage.Sender.StateMoney += empireMessage.Money;
                            empireMessage.Sender.PirateEconomy.PerformIncome(empireMessage.Money, PirateIncomeType.SellInfo, _Galaxy.CurrentStarDate);
                        }
                        break;
                    case EmpireMessageType.SellInfoDebrisField:
                    case EmpireMessageType.SellInfoRestrictedArea:
                    case EmpireMessageType.SellInfoPlanetDestroyer:
                        if (this != _Galaxy.PlayerEmpire && Galaxy.Rnd.Next(0, 2) == 1 && CheckSufficientFunds(empireMessage.Money, 0.2) && empireMessage.Subject is GalaxyLocation)
                        {
                            GalaxyLocation item = (GalaxyLocation)empireMessage.Subject;
                            if (!KnownGalaxyLocations.Contains(item))
                            {
                                KnownGalaxyLocations.Add(item);
                            }
                            StateMoney -= empireMessage.Money;
                            empireMessage.Sender.StateMoney += empireMessage.Money;
                            empireMessage.Sender.PirateEconomy.PerformIncome(empireMessage.Money, PirateIncomeType.SellInfo, _Galaxy.CurrentStarDate);
                        }
                        break;
                    case EmpireMessageType.OfferTrade:
                        if (this == _Galaxy.PlayerEmpire)
                        {
                            break;
                        }
                        if (empireMessage.Subject is object[])
                        {
                            object[] array2 = (object[])empireMessage.Subject;
                            TradeableItemList tradeableItemList = (TradeableItemList)array2[0];
                            TradeableItemList tradeableItemList2 = (TradeableItemList)array2[1];
                            TradeOfferResponse tradeOfferResponse = EvaluateTradeOffer(empireMessage.Sender, tradeableItemList, tradeableItemList2, disallowCriticalItems: true);
                            if (tradeOfferResponse == TradeOfferResponse.Accept || tradeOfferResponse == TradeOfferResponse.AcceptUnfair)
                            {
                                foreach (TradeableItem item2 in tradeableItemList)
                                {
                                    _Galaxy.GiveTradeableItem(empireMessage.Sender, this, item2, tradeableItemList2);
                                }
                                foreach (TradeableItem item3 in tradeableItemList2)
                                {
                                    _Galaxy.GiveTradeableItem(this, empireMessage.Sender, item3, tradeableItemList);
                                }
                            }
                            else
                            {
                                if (empireMessage.Sender == _Galaxy.PlayerEmpire || empireMessage.Sender.PirateEmpireBaseHabitat != null || PirateEmpireBaseHabitat != null)
                                {
                                    break;
                                }
                                foreach (TradeableItem item4 in tradeableItemList)
                                {
                                    if (item4.Type == TradeableItemType.ThreatenWar)
                                    {
                                        empireMessage.Sender.DeclareWar(this);
                                    }
                                    else if (item4.Type == TradeableItemType.ThreatenTradeSanctions)
                                    {
                                        DiplomaticRelation currentDiplomaticRelation = empireMessage.Sender.ObtainDiplomaticRelation(this);
                                        empireMessage.Sender.ChangeDiplomaticRelation(currentDiplomaticRelation, DiplomaticRelationType.TradeSanctions);
                                        empireMessage.Sender.SendMessageToEmpire(this, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.TradeSanctions, TextResolver.GetText("We terminate all trade with you effective immediately!"));
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!(empireMessage.Subject is TradeableItem))
                            {
                                break;
                            }
                            TradeableItem tradeableItem = (TradeableItem)empireMessage.Subject;
                            if (tradeableItem.Type == TradeableItemType.TerritoryMap)
                            {
                                if (!DetermineAcceptTerritoryMapTrade(tradeableItem.Value, empireMessage.Sender))
                                {
                                    break;
                                }
                                HabitatList habitatList2 = empireMessage.Sender.DetermineEmpireSystems(empireMessage.Sender);
                                HabitatList habitatList3 = DetermineEmpireSystems(this);
                                foreach (Habitat item5 in habitatList2)
                                {
                                    if (!CheckSystemExplored(item5))
                                    {
                                        SetSystemVisibility(item5, SystemVisibilityStatus.Explored);
                                    }
                                }
                                foreach (Habitat item6 in habitatList3)
                                {
                                    if (!empireMessage.Sender.CheckSystemExplored(item6))
                                    {
                                        empireMessage.Sender.SetSystemVisibility(item6, SystemVisibilityStatus.Explored);
                                    }
                                }
                            }
                            else if (tradeableItem.Type == TradeableItemType.GalaxyMap)
                            {
                                if (DetermineAcceptGalaxyMapTrade(tradeableItem.Value, empireMessage.Sender))
                                {
                                    _Galaxy.MergeGalaxyMap(empireMessage.Sender, this);
                                    _Galaxy.MergeGalaxyMap(this, empireMessage.Sender);
                                }
                            }
                            else
                            {
                                if (tradeableItem.Type != TradeableItemType.ResearchProject || !((double)tradeableItem.Value <= StateMoney))
                                {
                                    break;
                                }
                                double num14 = StateMoney * (0.25 + Galaxy.Rnd.NextDouble() * 0.25);
                                if (StateMoney >= num14 && tradeableItem.Item is ResearchNode)
                                {
                                    ResearchNode researchNode = (ResearchNode)tradeableItem.Item;
                                    ResearchNode equivalent = Research.TechTree.GetEquivalent(researchNode);
                                    if (!equivalent.IsResearched)
                                    {
                                        DoResearchBreakthrough(equivalent, selfResearched: false, blockMessages: true, suppressUpdate: true);
                                        Research.Update(DominantRace);
                                        ReviewDesignsBuiltObjectsImprovedComponents();
                                        ReviewResearchAbilities();
                                        StateMoney -= tradeableItem.Value;
                                        empireMessage.Sender.StateMoney += tradeableItem.Value;
                                        empireMessage.Sender.PirateEconomy.PerformIncome(tradeableItem.Value, PirateIncomeType.SellInfo, _Galaxy.CurrentStarDate);
                                    }
                                }
                            }
                        }
                        break;
                    case EmpireMessageType.GiveGift:
                        {
                            double num15 = ValueMoneyGiftFromEmpire(empireMessage.Sender, empireMessage.Money);
                            if (empireMessage.Sender.PirateEmpireBaseHabitat == null && PirateEmpireBaseHabitat == null)
                            {
                                EmpireEvaluation empireEvaluation2 = ObtainEmpireEvaluation(empireMessage.Sender);
                                empireEvaluation2.IncidentEvaluation = empireEvaluation2.IncidentEvaluationRaw + num15;
                            }
                            else
                            {
                                PirateRelation pirateRelation2 = ObtainPirateRelation(empireMessage.Sender);
                                pirateRelation2.EvaluationGifts += (float)num15;
                            }
                            empireMessage.Sender.CivilityRating += num15 * 0.1;
                            StateMoney += empireMessage.Money;
                            PirateEconomy.PerformIncome(empireMessage.Money, PirateIncomeType.Undefined, _Galaxy.CurrentStarDate);
                            SendMessageToEmpire(empireMessage.Sender, EmpireMessageType.Informational, null, TextResolver.GetText("Thank you for your gift."));
                            if (empireMessage.Sender.PirateEmpireBaseHabitat == null && PirateEmpireBaseHabitat == null)
                            {
                                diplomaticRelation = empireMessage.Sender.ObtainDiplomaticRelation(this);
                                diplomaticRelation.LastGiftDate = _Galaxy.CurrentStarDate;
                            }
                            break;
                        }
                    case EmpireMessageType.LeaveSystem:
                        if (_ControlDiplomacyTreaties != AutomationLevel.FullyAutomated)
                        {
                            break;
                        }
                        if (empireMessage.Sender.PirateEmpireBaseHabitat == null && PirateEmpireBaseHabitat == null)
                        {
                            diplomaticRelation = ObtainDiplomaticRelation(empireMessage.Sender);
                            if (diplomaticRelation.Type == DiplomaticRelationType.War || diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions)
                            {
                                break;
                            }
                        }
                        num = (double)WeightedMilitaryPotency / (double)empireMessage.Sender.WeightedMilitaryPotency;
                        if ((Galaxy.Rnd.Next(0, 3) == 1 || num < (Galaxy.Rnd.NextDouble() * 50.0 + (double)DominantRace.CautionLevel - 50.0) / 100.0) && empireMessage.Subject is Habitat)
                        {
                            Habitat habitat2 = (Habitat)empireMessage.Subject;
                            double val = LeaveSystem(habitat2);
                            if (empireMessage.Sender.PirateEmpireBaseHabitat == null && PirateEmpireBaseHabitat == null)
                            {
                                empireEvaluation = ObtainEmpireEvaluation(empireMessage.Sender);
                                val = Math.Min(val, 3.0);
                                empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - val;
                            }
                            else
                            {
                                PirateRelation pirateRelation = ObtainPirateRelation(empireMessage.Sender);
                                val = Math.Min(val, 3.0);
                                pirateRelation.EvaluationOffenseOverRequests -= (float)val;
                            }
                            SendMessageToEmpire(empireMessage.Sender, EmpireMessageType.Informational, null, string.Format(TextResolver.GetText("We comply with your request to leave the X system"), habitat2.Name));
                        }
                        break;
                    case EmpireMessageType.RemoveColoniesFromSystem:
                        {
                            if (_ControlDiplomacyTreaties == AutomationLevel.Manual)
                            {
                                break;
                            }
                            diplomaticRelation = ObtainDiplomaticRelation(empireMessage.Sender);
                            if (diplomaticRelation.Type == DiplomaticRelationType.War || diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions)
                            {
                                break;
                            }
                            num = (double)WeightedMilitaryPotency / (double)empireMessage.Sender.WeightedMilitaryPotency;
                            if (!(num < (Galaxy.Rnd.NextDouble() * 30.0 + (double)DominantRace.CautionLevel - 65.0) / 100.0) || !(empireMessage.Subject is Habitat))
                            {
                                break;
                            }
                            double num5 = 0.0;
                            long num6 = 0L;
                            Habitat habitat = (Habitat)empireMessage.Subject;
                            HabitatList habitatList = new HabitatList();
                            HabitatList habitats = _Galaxy.Systems[habitat].Habitats;
                            foreach (Habitat item7 in habitats)
                            {
                                for (int num7 = Colonies.IndexOf(item7); num7 >= 0; num7 = ((num7 >= Colonies.Count - 1) ? (-1) : Colonies.IndexOf(item7, num7 + 1)))
                                {
                                    habitatList.Add(Colonies[num7]);
                                    num6 += Colonies[num7].DevelopmentLevel * Colonies[num7].Population.TotalAmount;
                                }
                            }
                            bool flag = true;
                            foreach (Habitat item8 in habitatList)
                            {
                                if (item8 == _Capital)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                double num8 = 50.0 * ((Galaxy.Rnd.NextDouble() * 40.0 + 80.0) * Math.Pow(1.0 / num, 2.0));
                                if (num8 > (double)(num6 / 1000000))
                                {
                                    foreach (Habitat item9 in habitatList)
                                    {
                                        OrderList orders = _Galaxy.Orders.GetOrders(item9);
                                        if (orders.Count > 0)
                                        {
                                            foreach (Order item10 in orders)
                                            {
                                                if (item10.Contracts != null)
                                                {
                                                    foreach (Contract contract in item10.Contracts)
                                                    {
                                                        if (contract.Freighter == null)
                                                        {
                                                            continue;
                                                        }
                                                        if (item10.CommodityResource != null)
                                                        {
                                                            Resource commodityResource = item10.CommodityResource;
                                                            int num9 = -1;
                                                            if (contract.Freighter.Cargo != null)
                                                            {
                                                                num9 = contract.Freighter.Cargo.IndexOf(commodityResource, this);
                                                            }
                                                            if (num9 >= 0)
                                                            {
                                                                int amount = contract.Freighter.Cargo[num9].Amount;
                                                                contract.Freighter.Cargo.RemoveAt(num9);
                                                                Cargo cargo = new Cargo(commodityResource, amount, contract.Freighter.Empire);
                                                                contract.Freighter.Cargo.Add(cargo);
                                                            }
                                                        }
                                                        else if (item10.CommodityComponent != null)
                                                        {
                                                            Component commodityComponent = item10.CommodityComponent;
                                                            int num10 = -1;
                                                            if (contract.Freighter.Cargo != null)
                                                            {
                                                                num10 = contract.Freighter.Cargo.IndexOf(commodityComponent, this);
                                                            }
                                                            if (num10 >= 0)
                                                            {
                                                                int amount2 = contract.Freighter.Cargo[num10].Amount;
                                                                contract.Freighter.Cargo.RemoveAt(num10);
                                                                Cargo cargo2 = new Cargo(commodityComponent, amount2, contract.Freighter.Empire);
                                                                contract.Freighter.Cargo.Add(cargo2);
                                                            }
                                                        }
                                                        contract.Freighter.ClearPreviousMissionRequirements();
                                                    }
                                                }
                                                _Galaxy.Orders.Remove(item10);
                                            }
                                        }
                                        item9.Owner = null;
                                        item9.Empire = null;
                                        Colonies.Remove(item9);
                                        num5 += (double)item9.DevelopmentLevel / 3.0;
                                    }
                                    empireEvaluation = ObtainEmpireEvaluation(empireMessage.Sender);
                                    num5 = Math.Min(num5, 30.0);
                                    empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - num5;
                                    SendMessageToEmpire(empireMessage.Sender, EmpireMessageType.Informational, null, string.Format(TextResolver.GetText("We comply with your request to remove all colonies from the X system"), habitat.Name));
                                }
                                else
                                {
                                    empireEvaluation = ObtainEmpireEvaluation(empireMessage.Sender);
                                    num5 = 8.0;
                                    empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - num5;
                                    SendMessageToEmpire(empireMessage.Sender, EmpireMessageType.Informational, null, string.Format(TextResolver.GetText("We will not remove our colonies from the X system"), habitat.Name));
                                }
                            }
                            else
                            {
                                empireEvaluation = ObtainEmpireEvaluation(empireMessage.Sender);
                                num5 = 8.0;
                                empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - num5;
                                SendMessageToEmpire(empireMessage.Sender, EmpireMessageType.Informational, null, string.Format(TextResolver.GetText("We will not remove our colonies from the X system"), habitat.Name) + TextResolver.GetText("This is our capital system."));
                            }
                            break;
                        }
                    case EmpireMessageType.RequestJointWar:
                        {
                            if (!(empireMessage.Subject is Empire))
                            {
                                break;
                            }
                            empire = (Empire)empireMessage.Subject;
                            empire2 = empireMessage.Sender;
                            if (empire == this || _ControlDiplomacyOffense == AutomationLevel.Manual || Reclusive || empire2.PirateEmpireBaseHabitat != null || empire.PirateEmpireBaseHabitat != null || PirateEmpireBaseHabitat != null)
                            {
                                break;
                            }
                            diplomaticRelation = ObtainDiplomaticRelation(empire);
                            if (diplomaticRelation.Type == DiplomaticRelationType.War)
                            {
                                break;
                            }
                            bool flag3 = false;
                            DiplomaticStrategy strategy = diplomaticRelation.Strategy;
                            if (strategy == DiplomaticStrategy.Conquer || strategy == DiplomaticStrategy.Undermine || strategy == DiplomaticStrategy.Punish)
                            {
                                flag3 = true;
                            }
                            if (!flag3)
                            {
                                break;
                            }
                            int weightedMilitaryPotency2 = empire.WeightedMilitaryPotency;
                            num = (double)WeightedMilitaryPotency / (double)weightedMilitaryPotency2;
                            if (!(num > (Galaxy.Rnd.NextDouble() * 40.0 + (double)DominantRace.CautionLevel - 30.0) / 100.0))
                            {
                                break;
                            }
                            DiplomaticRelation diplomaticRelation8 = ObtainDiplomaticRelation(empire2);
                            if (diplomaticRelation8.Strategy == DiplomaticStrategy.Ally || diplomaticRelation8.Strategy == DiplomaticStrategy.Befriend)
                            {
                                bool flag4 = true;
                                if (diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || (diplomaticRelation.Type == DiplomaticRelationType.Protectorate && diplomaticRelation.Initiator == this))
                                {
                                    flag4 = false;
                                }
                                else if (diplomaticRelation.Locked && diplomaticRelation.Type != DiplomaticRelationType.War)
                                {
                                    flag4 = false;
                                }
                                if (flag4 && CheckTaskAuthorized(_ControlDiplomacyOffense, GenerateAutomationMessageAskedWar(empire2, empire), empire, AdvisorMessageType.ComplyWarOther))
                                {
                                    DeclareWar(empire, empire2);
                                    SendMessageToEmpire(empire2, EmpireMessageType.Informational, null, string.Format(TextResolver.GetText("We join you in battle against the EMPIRE"), empire.Name));
                                    EmpireEvaluation empireEvaluation4 = empire2.ObtainEmpireEvaluation(this);
                                    empireEvaluation4.IncidentEvaluation = empireEvaluation4.IncidentEvaluationRaw + 10.0;
                                }
                            }
                            break;
                        }
                    case EmpireMessageType.RequestJointTradeSanctions:
                        {
                            if (!(empireMessage.Subject is Empire))
                            {
                                break;
                            }
                            empire = (Empire)empireMessage.Subject;
                            empire2 = empireMessage.Sender;
                            if (empire == this || _ControlDiplomacyOffense == AutomationLevel.Manual || Reclusive || empire2.PirateEmpireBaseHabitat != null || empire.PirateEmpireBaseHabitat != null || PirateEmpireBaseHabitat != null)
                            {
                                break;
                            }
                            diplomaticRelation = ObtainDiplomaticRelation(empire);
                            if (diplomaticRelation.Type == DiplomaticRelationType.War || diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions)
                            {
                                break;
                            }
                            empireEvaluation = ObtainEmpireEvaluation(empire);
                            bool flag2 = false;
                            switch (diplomaticRelation.Strategy)
                            {
                                case DiplomaticStrategy.Conquer:
                                case DiplomaticStrategy.Undermine:
                                case DiplomaticStrategy.DefendUndermine:
                                case DiplomaticStrategy.Punish:
                                    flag2 = true;
                                    break;
                            }
                            if (!flag2)
                            {
                                break;
                            }
                            long num16 = CalculateNextAllowableProposalDate(diplomaticRelation);
                            if (_Galaxy.CurrentStarDate < num16)
                            {
                                break;
                            }
                            int weightedMilitaryPotency = empire.WeightedMilitaryPotency;
                            num = (double)WeightedMilitaryPotency / (double)weightedMilitaryPotency;
                            if (!(num > (Galaxy.Rnd.NextDouble() * 40.0 + (double)DominantRace.CautionLevel - 40.0) / 100.0))
                            {
                                break;
                            }
                            int num17 = (int)StateMoney / 2;
                            if (empireEvaluation.TradeVolume >= num17)
                            {
                                break;
                            }
                            DiplomaticRelation diplomaticRelation7 = ObtainDiplomaticRelation(empire2);
                            if (diplomaticRelation7.Strategy == DiplomaticStrategy.Ally || diplomaticRelation7.Strategy == DiplomaticStrategy.Befriend)
                            {
                                int refusalCount = 0;
                                if (CheckTaskAuthorized(_ControlDiplomacyOffense, ref refusalCount, GenerateAutomationMessageAskedTradeSanctions(empire2, empire), empire, AdvisorMessageType.ComplyTradeSanctionsOther, empire2, null))
                                {
                                    DiplomaticRelation currentDiplomaticRelation2 = ObtainDiplomaticRelation(empire);
                                    ChangeDiplomaticRelation(currentDiplomaticRelation2, DiplomaticRelationType.TradeSanctions);
                                    SendMessageToEmpire(empire, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.TradeSanctions, TextResolver.GetText("We terminate all trade with you effective immediately!"));
                                    SendMessageToEmpire(empire2, EmpireMessageType.Informational, null, string.Format(TextResolver.GetText("We join you in trade embargo against the EMPIRE"), empire.Name));
                                    EmpireEvaluation empireEvaluation3 = empire2.ObtainEmpireEvaluation(this);
                                    empireEvaluation3.IncidentEvaluation = empireEvaluation3.IncidentEvaluationRaw + 5.0;
                                }
                            }
                            break;
                        }
                    case EmpireMessageType.RequestHonorMutualDefense:
                        {
                            if (empireMessage.Sender == null || empireMessage.Subject == null || !(empireMessage.Subject is Empire))
                            {
                                break;
                            }
                            Empire empire3 = (Empire)empireMessage.Subject;
                            if (empire3 != null && empire3.PirateEmpireBaseHabitat == null && empireMessage.Sender.PirateEmpireBaseHabitat == null && PirateEmpireBaseHabitat == null)
                            {
                                DiplomaticRelation diplomaticRelation3 = ObtainDiplomaticRelation(empire3);
                                if (diplomaticRelation3.Type != DiplomaticRelationType.War)
                                {
                                    ConsiderHonorMutualDefensePactOrProtectorate(empireMessage.Sender, empire3);
                                }
                            }
                            break;
                        }
                    case EmpireMessageType.RequestLiftTradeSanctions:
                        {
                            if (!(empireMessage.Subject is Empire))
                            {
                                break;
                            }
                            empire = (Empire)empireMessage.Subject;
                            if (_ControlDiplomacyOffense != AutomationLevel.FullyAutomated || Reclusive || empire.PirateEmpireBaseHabitat != null || empireMessage.Sender.PirateEmpireBaseHabitat != null || PirateEmpireBaseHabitat != null)
                            {
                                break;
                            }
                            diplomaticRelation = ObtainDiplomaticRelation(empire);
                            if (diplomaticRelation.Type != DiplomaticRelationType.TradeSanctions || diplomaticRelation.Initiator != this)
                            {
                                break;
                            }
                            long num2 = CalculateNextAllowableProposalDate(diplomaticRelation);
                            if (_Galaxy.CurrentStarDate < num2)
                            {
                                break;
                            }
                            num = (double)WeightedMilitaryPotency / (double)empireMessage.Sender.WeightedMilitaryPotency;
                            DiplomaticRelation diplomaticRelation2 = ObtainDiplomaticRelation(empire2);
                            if (diplomaticRelation2.Strategy == DiplomaticStrategy.Ally || diplomaticRelation2.Strategy == DiplomaticStrategy.Befriend)
                            {
                                if (diplomaticRelation.Strategy != DiplomaticStrategy.Conquer)
                                {
                                    ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.None);
                                    SendMessageToEmpire(empire, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.None, TextResolver.GetText("Our trade sanctions against you have been lifted - we will now resume trade."), Galaxy.ResolveDescription(DiplomaticRelationType.TradeSanctions));
                                    CancelBlockades(empire);
                                    empire.CancelBlockades(this);
                                    SendMessageToEmpire(empireMessage.Sender, EmpireMessageType.Informational, null, string.Format(TextResolver.GetText("We will resume trade with the EMPIRE"), empire.Name));
                                }
                            }
                            else if (num < (Galaxy.Rnd.NextDouble() * 30.0 + (double)DominantRace.CautionLevel - 65.0) / 100.0 && diplomaticRelation.Strategy != DiplomaticStrategy.Conquer)
                            {
                                ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.None);
                                SendMessageToEmpire(empire, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.None, TextResolver.GetText("Our trade sanctions against you have been lifted - we will now resume trade."), Galaxy.ResolveDescription(DiplomaticRelationType.TradeSanctions));
                                CancelBlockades(empire);
                                empire.CancelBlockades(this);
                                SendMessageToEmpire(empireMessage.Sender, EmpireMessageType.Informational, null, string.Format(TextResolver.GetText("We will resume trade with the EMPIRE"), empire.Name));
                            }
                            break;
                        }
                }
            }
            _Messages.Clear();
        }

        public bool ConsiderHonorMutualDefensePactOrProtectorate(Empire requester, Empire targetEmpire)
        {
            if (requester != null && targetEmpire != null)
            {
                DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(requester);
                if (diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || (diplomaticRelation.Type == DiplomaticRelationType.Protectorate && diplomaticRelation.Initiator == this))
                {
                    bool flag = true;
                    DiplomaticRelation diplomaticRelation2 = ObtainDiplomaticRelation(targetEmpire);
                    if (diplomaticRelation2.Type == DiplomaticRelationType.MutualDefensePact || (diplomaticRelation2.Type == DiplomaticRelationType.Protectorate && diplomaticRelation2.Initiator == this))
                    {
                        flag = false;
                    }
                    else if (diplomaticRelation2.Locked && diplomaticRelation2.Type != DiplomaticRelationType.War)
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        double num = (double)MilitaryPotency / (double)targetEmpire.MilitaryPotency;
                        double num2 = 1.0;
                        if (DominantRace != null)
                        {
                            num2 = (double)DominantRace.LoyaltyLevel / 100.0;
                            num2 *= num2;
                        }
                        double num3 = num * num2;
                        if (num3 >= 0.5)
                        {
                            DeclareWar(targetEmpire, null, lockedWar: false, blockFlowonEffects: true);
                            requester.ObtainEmpireEvaluation(this).IncidentEvaluation += 30.0;
                            CivilityRating += 8.0;
                            string description = string.Format(TextResolver.GetText("We stand alongside our friends and allies"), targetEmpire.Name);
                            SendMessageToEmpire(requester, EmpireMessageType.Informational, targetEmpire, description);
                            return true;
                        }
                        string text = TextResolver.GetText("Sorry, we can't help you right now...");
                        SendMessageToEmpire(requester, EmpireMessageType.Informational, targetEmpire, text);
                        requester.ObtainEmpireEvaluation(this).IncidentEvaluation -= 30.0;
                        CivilityRating -= 6.0;
                        ChangeDiplomaticRelation(ObtainDiplomaticRelation(requester), DiplomaticRelationType.None, blockFlowonEffects: true);
                        return false;
                    }
                }
            }
            return false;
        }

        private void ClearOutOldDistressSignals()
        {
            long num = _Galaxy.CurrentStarDate - 50000;
            DistressSignalList distressSignalList = new DistressSignalList();
            for (int i = 0; i < DistressSignals.Count; i++)
            {
                if (DistressSignals[i].Date < num)
                {
                    distressSignalList.Add(DistressSignals[i]);
                }
            }
            for (int j = 0; j < distressSignalList.Count; j++)
            {
                DistressSignals.Remove(distressSignalList[j]);
            }
        }

        public void ProcessDistressSignals()
        {
            for (int i = 0; i < DistressSignals.Count; i++)
            {
                DistressSignal distressSignal = DistressSignals[i];
                if (distressSignal.Source is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)distressSignal.Source;
                    if (builtObject.Role != BuiltObjectRole.Base)
                    {
                        continue;
                    }
                    int num = _Galaxy.DetermineDefendingStrength(builtObject, this);
                    ShipGroupList shipGroupList = ShipGroups.DetermineFleetsTravellingToLocation(builtObject.Xpos, builtObject.Ypos, 48000.0);
                    num += shipGroupList.CountTotalOverallStrengthFactor();
                    int num2 = (int)((double)distressSignal.AttackStrength * 0.75);
                    if (num >= num2)
                    {
                        continue;
                    }
                    ShipGroup shipGroup = IdentifyNearestResponseFleet(builtObject.Xpos, builtObject.Ypos, mustBeWithinFuelRange: true, 0.1, 48000.0);
                    if (shipGroup == null)
                    {
                        continue;
                    }
                    bool flag = true;
                    double num3 = _Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos);
                    if (num3 > Galaxy.DistressSignalResponseMaximumDistance)
                    {
                        ShipGroup shipGroup2 = FindNearestFleet(builtObject.Xpos, builtObject.Ypos);
                        if (shipGroup2 != null && shipGroup2 != shipGroup)
                        {
                            flag = false;
                        }
                    }
                    if (num3 > (double)(Galaxy.SectorSize * 2))
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        StellarObject stellarObject = _Galaxy.DetermineNearestHabitatIfPossible(builtObject);
                        if (stellarObject == null)
                        {
                            shipGroup.AssignMission(BuiltObjectMissionType.Move, null, null, builtObject.Xpos, builtObject.Ypos, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        }
                        else
                        {
                            shipGroup.AssignMission(BuiltObjectMissionType.Move, stellarObject, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        }
                        shipGroup.AllowImmediateThreatEvaluation = true;
                        shipGroup.AttackRangeSquared = (float)AttackRangeAttack * (float)AttackRangeAttack;
                    }
                }
                else
                {
                    if (!(distressSignal.Source is Habitat))
                    {
                        continue;
                    }
                    Habitat habitat = (Habitat)distressSignal.Source;
                    Galaxy.DetermineHabitatSystemStar(habitat);
                    int num4 = _Galaxy.DetermineDefendingStrength(habitat, this);
                    ShipGroupList shipGroupList2 = ShipGroups.DetermineFleetsTravellingToLocation(habitat.Xpos, habitat.Ypos, 48000.0);
                    num4 += shipGroupList2.CountTotalOverallStrengthFactor();
                    int num5 = (int)((double)distressSignal.AttackStrength * 0.75);
                    if (num4 >= num5)
                    {
                        continue;
                    }
                    ShipGroup shipGroup3 = IdentifyNearestResponseFleet(habitat.Xpos, habitat.Ypos, mustBeWithinFuelRange: true, 0.1, 48000.0);
                    if (shipGroup3 == null)
                    {
                        continue;
                    }
                    bool flag2 = true;
                    double num6 = _Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, shipGroup3.LeadShip.Xpos, shipGroup3.LeadShip.Ypos);
                    if (num6 > Galaxy.DistressSignalResponseMaximumDistance)
                    {
                        ShipGroup shipGroup4 = FindNearestFleet(habitat.Xpos, habitat.Ypos);
                        if (shipGroup4 != null && shipGroup4 != shipGroup3)
                        {
                            flag2 = false;
                        }
                    }
                    if (num6 > (double)(Galaxy.SectorSize * 2))
                    {
                        flag2 = false;
                    }
                    if (flag2)
                    {
                        StellarObject stellarObject2 = _Galaxy.DetermineNearestHabitatIfPossible(habitat);
                        if (stellarObject2 == null)
                        {
                            shipGroup3.AssignMission(BuiltObjectMissionType.Move, habitat, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        }
                        else
                        {
                            shipGroup3.AssignMission(BuiltObjectMissionType.Move, stellarObject2, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        }
                        shipGroup3.AllowImmediateThreatEvaluation = true;
                        shipGroup3.AttackRangeSquared = (float)AttackRangeAttack * (float)AttackRangeAttack;
                    }
                }
            }
        }

        public DesignList CheckBasesToBeBuiltAtHabitat(Habitat habitat)
        {
            DesignList designList = new DesignList();
            for (int i = 0; i < ConstructionShips.Count; i++)
            {
                BuiltObject builtObject = ConstructionShips[i];
                if (builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Build && builtObject.Mission.TargetHabitat == habitat && builtObject.Mission.Design != null)
                {
                    designList.Add(builtObject.Mission.Design);
                }
                if (builtObject.SubsequentMissions == null || builtObject.SubsequentMissions.Count <= 0)
                {
                    continue;
                }
                for (int j = 0; j < builtObject.SubsequentMissions.Count; j++)
                {
                    if (builtObject.SubsequentMissions[j] != null && builtObject.SubsequentMissions[j].Type == BuiltObjectMissionType.Build && builtObject.SubsequentMissions[j].TargetHabitat == habitat && builtObject.SubsequentMissions[j].Design != null)
                    {
                        designList.Add(builtObject.SubsequentMissions[j].Design);
                    }
                }
            }
            return designList;
        }

        public ShipGroup IdentifyNearestAvailableFleet(double x, double y, bool mustBeAutomated)
        {
            return IdentifyNearestAvailableFleet(x, y, mustBeAutomated, mustBeWithinFuelRange: false, 0.0);
        }

        public ShipGroup IdentifyNearestAvailableFleet(double x, double y, bool mustBeAutomated, bool mustBeWithinFuelRange, double fuelPortionMargin)
        {
            return IdentifyNearestAvailableFleet(x, y, mustBeAutomated, mustBeWithinFuelRange, fuelPortionMargin, 0.0);
        }

        public ShipGroup IdentifyNearestAvailableFleet(double x, double y, bool mustBeAutomated, bool mustBeWithinFuelRange, double fuelPortionMargin, double excludeRange)
        {
            return IdentifyNearestAvailableFleet(x, y, mustBeAutomated, mustBeWithinFuelRange, fuelPortionMargin, excludeRange, defendFleetsMustBeWithinPostureRange: true);
        }

        public ShipGroup IdentifyNearestAvailableFleet(double x, double y, bool mustBeAutomated, bool mustBeWithinFuelRange, double fuelPortionMargin, double excludeRange, bool defendFleetsMustBeWithinPostureRange)
        {
            return IdentifyNearestAvailableFleet(x, y, mustBeAutomated, mustBeWithinFuelRange, fuelPortionMargin, excludeRange, defendFleetsMustBeWithinPostureRange, forceFleetUse: false, 0);
        }

        public ShipGroup IdentifyNearestAvailableFleet(double x, double y, bool mustBeAutomated, bool mustBeWithinFuelRange, double fuelPortionMargin, double excludeRange, bool defendFleetsMustBeWithinPostureRange, bool forceFleetUse, int minimumShipCount)
        {
            double num = excludeRange * excludeRange;
            bool atWar = CheckAtWar();
            ShipGroupList shipGroupList = GenerateDistanceOrderedFleetList(x, y, ShipGroups);
            for (int i = 0; i < shipGroupList.Count; i++)
            {
                ShipGroup shipGroup = shipGroupList[i];
                if (shipGroup == null || shipGroup.LeadShip == null)
                {
                    continue;
                }
                bool flag = true;
                if (mustBeAutomated && !shipGroup.LeadShip.IsAutoControlled)
                {
                    flag = false;
                }
                if (!flag || shipGroup.Ships.Count < minimumShipCount || (!forceFleetUse && !CheckFleetDefenseReponse(shipGroup, x, y, atWar)))
                {
                    continue;
                }
                bool flag2 = true;
                if (shipGroup.Mission != null)
                {
                    if (forceFleetUse)
                    {
                        if (shipGroup.Mission.Priority == BuiltObjectMissionPriority.High || shipGroup.Mission.Priority == BuiltObjectMissionPriority.VeryHigh || shipGroup.Mission.Priority == BuiltObjectMissionPriority.Unavailable)
                        {
                            flag2 = false;
                        }
                    }
                    else
                    {
                        flag2 = false;
                        if (shipGroup.Mission.Type == BuiltObjectMissionType.Undefined || shipGroup.Mission.Type == BuiltObjectMissionType.MoveAndWait || shipGroup.Mission.Type == BuiltObjectMissionType.Hold || (shipGroup.Mission.Type == BuiltObjectMissionType.Patrol && shipGroup.Mission.Priority == BuiltObjectMissionPriority.Low))
                        {
                            flag2 = true;
                        }
                    }
                }
                if (!flag2)
                {
                    continue;
                }
                bool flag3 = false;
                if (excludeRange > 0.0)
                {
                    double num2 = _Galaxy.CalculateDistanceSquared(x, y, shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos);
                    if (num2 < num)
                    {
                        flag3 = true;
                    }
                }
                if (flag3)
                {
                    continue;
                }
                bool flag4 = true;
                if (!forceFleetUse && defendFleetsMustBeWithinPostureRange && shipGroup.Posture == FleetPosture.Defend)
                {
                    flag4 = false;
                    if (shipGroup.GatherPoint != null)
                    {
                        double num3 = _Galaxy.CalculateDistanceSquared(x, y, shipGroup.GatherPoint.Xpos, shipGroup.GatherPoint.Ypos);
                        if (shipGroup.PostureRangeSquared >= num3)
                        {
                            flag4 = true;
                        }
                    }
                }
                if (flag4)
                {
                    if (!mustBeWithinFuelRange)
                    {
                        return shipGroup;
                    }
                    if (shipGroup.CheckFleetTargetWithinFuelRangeAndRefuel(x, y, fuelPortionMargin))
                    {
                        return shipGroup;
                    }
                }
            }
            return null;
        }

        public bool CheckFleetDefenseReponse(ShipGroup fleet, double attackX, double attackY)
        {
            bool atWar = CheckAtWar();
            return CheckFleetDefenseReponse(fleet, attackX, attackY, atWar);
        }

        public bool CheckFleetDefenseReponse(ShipGroup fleet, double attackX, double attackY, bool atWar)
        {
            if (fleet != null && fleet.LeadShip != null && (fleet.Posture == FleetPosture.Defend || !atWar) && (fleet.Mission == null || fleet.Mission.Type == BuiltObjectMissionType.Undefined || fleet.Mission.Priority == BuiltObjectMissionPriority.Low))
            {
                if (fleet.Posture == FleetPosture.Attack)
                {
                    return true;
                }
                double xpos = fleet.LeadShip.Xpos;
                double ypos = fleet.LeadShip.Ypos;
                if (fleet.GatherPoint != null)
                {
                    xpos = fleet.GatherPoint.Xpos;
                    ypos = fleet.GatherPoint.Ypos;
                }
                double num = _Galaxy.CalculateDistanceSquared(xpos, ypos, attackX, attackY);
                if (fleet.PostureRangeSquared >= num)
                {
                    return true;
                }
            }
            return false;
        }

        public ShipGroup IdentifyNearestResponseFleet(double x, double y, bool mustBeWithinFuelRange, double fuelPortionMargin, double excludeRange)
        {
            ShipGroup shipGroup = null;
            double num = excludeRange * excludeRange;
            bool atWar = CheckAtWar();
            ShipGroupList shipGroupList = GenerateDistanceOrderedFleetList(x, y, ShipGroups);
            for (int i = 0; i < shipGroupList.Count; i++)
            {
                ShipGroup shipGroup2 = shipGroupList[i];
                if (shipGroup2 == null || shipGroup2.LeadShip == null)
                {
                    continue;
                }
                bool flag = true;
                if (shipGroup2.Posture == FleetPosture.Attack && !shipGroup2.LeadShip.IsAutoControlled)
                {
                    flag = false;
                }
                if (!flag || !CheckFleetDefenseReponse(shipGroup2, x, y, atWar) || (shipGroup2.Mission != null && shipGroup2.Mission.Type != 0 && shipGroup2.Mission.Type != BuiltObjectMissionType.MoveAndWait && shipGroup2.Mission.Type != BuiltObjectMissionType.Hold && (shipGroup2.Mission.Type != BuiltObjectMissionType.Patrol || shipGroup2.Mission.Priority != BuiltObjectMissionPriority.Low)))
                {
                    continue;
                }
                bool flag2 = false;
                if (excludeRange > 0.0)
                {
                    double num2 = _Galaxy.CalculateDistanceSquared(x, y, shipGroup2.LeadShip.Xpos, shipGroup2.LeadShip.Ypos);
                    if (num2 < num)
                    {
                        flag2 = true;
                    }
                }
                if (flag2)
                {
                    continue;
                }
                bool flag3 = false;
                if (mustBeWithinFuelRange)
                {
                    if (shipGroup2.CheckFleetTargetWithinFuelRangeAndRefuel(x, y, fuelPortionMargin))
                    {
                        flag3 = true;
                    }
                }
                else
                {
                    flag3 = true;
                }
                if (flag3)
                {
                    if (shipGroup2.Posture == FleetPosture.Defend)
                    {
                        return shipGroup2;
                    }
                    if (shipGroup == null)
                    {
                        shipGroup = shipGroup2;
                    }
                }
            }
            if (shipGroup != null)
            {
                return shipGroup;
            }
            return null;
        }

    }
}
