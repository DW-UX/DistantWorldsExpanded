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
        public void MakeHabitatIntoColony(Habitat habitat, Empire empire, Race race, long newPopulationAmount)
        {
            if (habitat == null)
            {
                return;
            }
            TakeOwnershipOfColony(habitat, empire);
            habitat.IsRefuellingDepot = true;
            Population population = new Population(race, newPopulationAmount);
            if (habitat.Population == null)
            {
                habitat.Population = new PopulationList();
            }
            habitat.Population.Add(population);
            habitat.Population.RecalculateTotalAmount();
            if (empire != null)
            {
                if (empire != _Galaxy.IndependentEmpire && empire == _Galaxy.PlayerEmpire && _Galaxy != null && _Galaxy.ColonyNames != null && _Galaxy.ColonyNames.Count > _Galaxy.ColonyNameIndex)
                {
                    string name = _Galaxy.ColonyNames[_Galaxy.ColonyNameIndex];
                    _Galaxy.ColonyNameIndex++;
                    habitat.Name = name;
                }
                empire.SetColonyTaxRate(habitat, atWar: false);
                if (habitat.Cargo == null)
                {
                    habitat.Cargo = new CargoList();
                }
                int amount = 2000;
                for (int i = 0; i < _Galaxy.ResourceSystem.FuelResources.Count; i++)
                {
                    ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.FuelResources[i];
                    Cargo cargo = new Cargo(new Resource(resourceDefinition.ResourceID), amount, empire);
                    habitat.Cargo.Add(cargo);
                }
            }
        }

        public void TakeOwnershipOfColony(Habitat colony, Empire newEmpire)
        {
            TakeOwnershipOfColony(colony, newEmpire, destroyAllBuiltObjectsAndTroopsAtColony: false);
        }

        public void TakeOwnershipOfColony(Habitat colony, Empire newEmpire, bool destroyAllBuiltObjectsAndTroopsAtColony)
        {
            TakeOwnershipOfColony(colony, newEmpire, destroyAllBuiltObjectsAndTroopsAtColony, destroyAllBuiltObjectsAndTroopsAtColony);
        }

        public void TakeOwnershipOfColony(Habitat colony, Empire newEmpire, bool destroyBases, bool destroyTroops)
        {
            Empire empire = colony.Empire;
            _Galaxy.CheckTriggerEvent(colony.GameEventId, newEmpire, EventTriggerType.Capture, null);
            bool flag = false;
            if (colony.Empire != null)
            {
                if (colony.Empire.Capital == colony)
                {
                    flag = true;
                }
                if (colony.Empire.Colonies.Contains(colony))
                {
                    colony.Empire.Colonies.Remove(colony);
                }
            }
            if (colony.Cargo == null)
            {
                colony.Cargo = new CargoList();
            }
            if (colony.Troops == null)
            {
                colony.Troops = new TroopList();
            }
            if (colony.TroopsToRecruit == null)
            {
                colony.TroopsToRecruit = new TroopList();
            }
            if (colony.InvadingTroops == null)
            {
                colony.InvadingTroops = new TroopList();
            }
            if (colony.Characters == null)
            {
                colony.Characters = new CharacterList();
            }
            if (colony.InvadingCharacters == null)
            {
                colony.InvadingCharacters = new CharacterList();
            }
            if (colony.Facilities == null)
            {
                colony.Facilities = new PlanetaryFacilityList();
            }
            if (colony.ConstructionQueue == null)
            {
                colony.ConstructionQueue = new ConstructionQueue(colony, _Galaxy);
            }
            else
            {
                colony.ConstructionQueue.ReviewConstructionSpeed();
            }
            if (colony.ManufacturingQueue == null)
            {
                colony.ManufacturingQueue = new ManufacturingQueue(colony, _Galaxy);
            }
            if (colony.DockingBays == null)
            {
                colony.DockingBays = new DockingBayList();
                int num = 20;
                for (int i = 0; i < num; i++)
                {
                    BuiltObjectComponent builtObjectComponent = new BuiltObjectComponent(74, ComponentStatus.Normal);
                    DockingBay item = new DockingBay(builtObjectComponent.ComponentID, builtObjectComponent.BuiltObjectComponentId, 100);
                    colony.DockingBays.Add(item);
                }
            }
            if (colony.DockingBayWaitQueue == null)
            {
                colony.DockingBayWaitQueue = new BuiltObjectList();
            }
            _Galaxy.ClearFleetHomeBases(colony);
            colony.Owner = newEmpire;
            colony.Empire = newEmpire;
            if (colony.Troops != null)
            {
                for (int j = 0; j < colony.Troops.Count; j++)
                {
                    Troop troop = colony.Troops[j];
                    if (troop.Empire != null && troop.Empire.Troops != null)
                    {
                        troop.Empire.Troops.Remove(troop);
                    }
                    if (!destroyTroops && newEmpire != null && newEmpire.Troops != null)
                    {
                        troop.Empire = newEmpire;
                        newEmpire.Troops.Add(troop);
                    }
                }
                if (destroyTroops || newEmpire == null)
                {
                    colony.Troops.Clear();
                }
            }
            if (colony.TroopsToRecruit != null)
            {
                for (int k = 0; k < colony.TroopsToRecruit.Count; k++)
                {
                    Troop troop2 = colony.TroopsToRecruit[k];
                    if (troop2.Empire != null && troop2.Empire.Troops != null && troop2.Empire.Troops.Contains(troop2))
                    {
                        troop2.Empire.Troops.Remove(troop2);
                    }
                    if (!destroyTroops && newEmpire != null)
                    {
                        troop2.Empire = newEmpire;
                    }
                }
                if (destroyTroops || newEmpire == null)
                {
                    colony.TroopsToRecruit.Clear();
                }
            }
            if (colony.Characters != null)
            {
                Character[] array = ListHelper.ToArrayThreadSafe(colony.Characters);
                for (int l = 0; l < array.Length; l++)
                {
                    array[l].CompleteEmpireChange(newEmpire);
                }
            }
            if (newEmpire != null)
            {
                newEmpire.CancelBlockade(colony);
                newEmpire.CancelAttacks(colony);
                newEmpire.CancelAllShipAttacks(colony, alsoCancelAttackForBases: false);
                newEmpire.CancelUnloadTroops(colony);
                newEmpire.CancelAllShipUnloadTroops(colony);
                newEmpire.CancelAllShipAttacksNonEnemies(colony);
                _Galaxy.CheckCancelIntelligenceMissionsWithTarget(colony);
                newEmpire.CancelAllCharacterTransfers(colony);
            }
            _Galaxy.ReevaluateMissionsAgainstHabitat(colony, newEmpire);
            if (empire != null)
            {
                if (flag)
                {
                    Habitat habitat2 = (empire.Capital = empire.SelectBestCandidateForCapital());
                    empire.RecalculateColonyDistancesFromCapital();
                }
                if (empire.Colonies.Count <= 0 && empire != _Galaxy.IndependentEmpire)
                {
                    if (empire.PirateEmpireBaseHabitat != null)
                    {
                        if (_Galaxy.CheckPirateEmpireTerminated(empire))
                        {
                            _Galaxy.EliminatePirateFaction(empire, newEmpire);
                        }
                    }
                    else if (newEmpire != null)
                    {
                        newEmpire.SendMessageToEmpire(empire, EmpireMessageType.EmpireDefeated, empire, TextResolver.GetText("You have been defeated!"));
                        empire.CompleteTeardown(newEmpire);
                    }
                    else
                    {
                        empire.SendMessageToEmpire(empire, EmpireMessageType.EmpireDefeated, empire, TextResolver.GetText("You have been defeated!"));
                        empire.CompleteTeardown(null);
                    }
                }
            }
            if (newEmpire != null)
            {
                colony.IsRefuellingDepot = true;
                TakeOwnershipOfCargo(colony.Cargo, empire, newEmpire);
                TakeOwnershipOfOrders(colony, empire, newEmpire);
                colony.RestrictedResourcesPresent = false;
                newEmpire.ReviewSpecialBonusesRuinsWonders();
                if (newEmpire.Capital == null)
                {
                    newEmpire.Capital = colony;
                }
                if (!newEmpire.Colonies.Contains(colony))
                {
                    newEmpire.Colonies.Add(colony);
                }
                colony.RecalculateDistanceFactor();
                newEmpire.SetColonyTaxRate(colony, atWar: false);
                if (newEmpire.Policy != null)
                {
                    colony.ColonyPopulationPolicy = newEmpire.Policy.NewColonyPopulationPolicyAllRaces;
                    colony.ColonyPopulationPolicyRaceFamily = newEmpire.Policy.NewColonyPopulationPolicyYourRaceFamily;
                }
            }
            else
            {
                colony.IsRefuellingDepot = false;
                OrderList orders = _Galaxy.Orders.GetOrders(colony);
                foreach (Order item2 in orders)
                {
                    if (item2.AmountStillToArrive > 0)
                    {
                        for (int m = 0; m < item2.Contracts.Count; m++)
                        {
                            Contract contract = item2.Contracts[m];
                            if (contract.Freighter != null && contract.Freighter.Mission != null && contract.Freighter.Mission.Type == BuiltObjectMissionType.Transport && contract.Freighter.Mission.SecondaryTargetHabitat == item2.RequestingColony)
                            {
                                contract.Freighter.ClearPreviousMissionRequirements();
                            }
                        }
                    }
                    _Galaxy.Orders.Remove(item2);
                }
            }
            colony.RecalculateDevelopmentLevelBaseline();
            colony.RecalculateAnnualTaxRevenue();
            bool empireHasWarptech = CheckEmpireHasHyperDriveTech(this);
            colony.RecalculateColonyInfluenceRadius(empireHasWarptech);
            BuiltObject builtObject = _Galaxy.DetermineMiningStationAtHabitat(colony);
            if (builtObject != null && builtObject.SubRole != BuiltObjectSubRole.SmallSpacePort && builtObject.SubRole != BuiltObjectSubRole.MediumSpacePort && builtObject.SubRole != BuiltObjectSubRole.LargeSpacePort)
            {
                builtObject.CompleteTeardown(_Galaxy);
            }
            for (int n = 0; n < colony.BasesAtHabitat.Count; n++)
            {
                BuiltObject builtObject2 = colony.BasesAtHabitat[n];
                if (builtObject2.SubRole == BuiltObjectSubRole.GenericBase || builtObject2.SubRole == BuiltObjectSubRole.EnergyResearchStation || builtObject2.SubRole == BuiltObjectSubRole.WeaponsResearchStation || builtObject2.SubRole == BuiltObjectSubRole.HighTechResearchStation || builtObject2.SubRole == BuiltObjectSubRole.MonitoringStation || builtObject2.SubRole == BuiltObjectSubRole.ResortBase || builtObject2.SubRole == BuiltObjectSubRole.DefensiveBase)
                {
                    if (destroyBases)
                    {
                        builtObject2.CompleteTeardown(_Galaxy, removeFromEmpire: true);
                    }
                    else
                    {
                        TakeOwnershipOfBuiltObject(builtObject2, newEmpire, setDesignAsObsolete: true);
                    }
                }
            }
            if (empire != null)
            {
                List<BuiltObjectRole> list = new List<BuiltObjectRole>();
                list.Add(BuiltObjectRole.Base);
                BuiltObjectList builtObjectsByRole = empire.BuiltObjects.GetBuiltObjectsByRole(list);
                BuiltObjectList builtObjectsByRole2 = empire.PrivateBuiltObjects.GetBuiltObjectsByRole(list);
                for (int num2 = 0; num2 < builtObjectsByRole.Count; num2++)
                {
                    BuiltObject builtObject3 = builtObjectsByRole[num2];
                    if (builtObject3.ParentHabitat == colony || builtObject3.BuiltAt == colony || builtObject3.DockedAt == colony)
                    {
                        if (destroyBases)
                        {
                            builtObject3.CompleteTeardown(_Galaxy, removeFromEmpire: true);
                        }
                        else
                        {
                            TakeOwnershipOfBuiltObject(builtObject3, newEmpire, setDesignAsObsolete: true);
                        }
                    }
                }
                for (int num3 = 0; num3 < builtObjectsByRole2.Count; num3++)
                {
                    BuiltObject builtObject4 = builtObjectsByRole2[num3];
                    if (builtObject4.ParentHabitat == colony || builtObject4.BuiltAt == colony || builtObject4.DockedAt == colony)
                    {
                        if (destroyBases)
                        {
                            builtObject4.CompleteTeardown(_Galaxy, removeFromEmpire: true);
                        }
                        else
                        {
                            TakeOwnershipOfBuiltObject(builtObject4, newEmpire, setDesignAsObsolete: true);
                        }
                    }
                }
            }
            if (colony.ConstructionQueue != null && colony.ConstructionQueue.ConstructionYards.Count > 0)
            {
                for (int num4 = 0; num4 < colony.ConstructionQueue.ConstructionYards.Count; num4++)
                {
                    ConstructionYard constructionYard = colony.ConstructionQueue.ConstructionYards[num4];
                    if (constructionYard.ShipUnderConstruction != null && constructionYard.ShipUnderConstruction.Empire == empire)
                    {
                        if (destroyBases)
                        {
                            constructionYard.ShipUnderConstruction.CompleteTeardown(_Galaxy, removeFromEmpire: true);
                        }
                        else
                        {
                            TakeOwnershipOfBuiltObject(constructionYard.ShipUnderConstruction, newEmpire, setDesignAsObsolete: true);
                        }
                    }
                }
                if (colony.ConstructionQueue.ConstructionWaitQueue != null)
                {
                    BuiltObjectList builtObjectList = new BuiltObjectList();
                    builtObjectList.AddRange(colony.ConstructionQueue.ConstructionWaitQueue);
                    for (int num5 = 0; num5 < builtObjectList.Count; num5++)
                    {
                        BuiltObject builtObject5 = builtObjectList[num5];
                        if (destroyBases)
                        {
                            builtObject5.CompleteTeardown(_Galaxy, removeFromEmpire: true);
                        }
                        else
                        {
                            TakeOwnershipOfBuiltObject(builtObject5, newEmpire, setDesignAsObsolete: true);
                        }
                    }
                    if (destroyBases)
                    {
                        colony.ConstructionQueue.ConstructionWaitQueue.Clear();
                    }
                }
            }
            _Galaxy.CancelPirateMissionsForTarget(colony, EmpireActivityType.Smuggle);
            _Galaxy.CancelPirateMissionsForTarget(colony, EmpireActivityType.Attack);
            ResolveSystemVisibility(colony.Xpos, colony.Ypos, null, null);
            if (ResourceMap != null)
            {
                ResourceMap.SetResourcesKnown(colony, known: true);
            }
            colony.StopRebelling();
            colony.CalculateWarWithOurRace();
            colony.CulturalDistressFactor = 0f;
            SystemInfo other = _Galaxy.DetermineSystemInfo(_Galaxy.Systems[colony.SystemIndex], null, null, null);
            _Galaxy.Systems[colony.SystemIndex].CopyFromOther(other);
            if (empire != null)
            {
                empire.ReviewSpecialBonusesRuinsWonders();
                empire.EvaluateSystemLinks();
            }
            newEmpire?.EvaluateSystemLinks();
        }

        public void TakeOwnershipOfOrders(Habitat colony, Empire oldEmpire, Empire newEmpire)
        {
            OrderList orderList = new OrderList();
            if (colony != null)
            {
                OrderList orders = _Galaxy.Orders.GetOrders(colony);
                orderList.AddRange(orders);
            }
            TakeOwnershipOfOrders(orderList, oldEmpire, newEmpire);
        }

        public void TakeOwnershipOfOrders(BuiltObject spacePort, Empire oldEmpire, Empire newEmpire)
        {
            OrderList orderList = new OrderList();
            if (spacePort != null)
            {
                OrderList orders = _Galaxy.Orders.GetOrders(spacePort);
                orderList.AddRange(orders);
            }
            TakeOwnershipOfOrders(orderList, oldEmpire, newEmpire);
        }

        private void TakeOwnershipOfOrders(OrderList orders, Empire oldEmpire, Empire newEmpire)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                Order order = orders[i];
                if (order.Contracts == null || order.Contracts.Count <= 0)
                {
                    continue;
                }
                for (int j = 0; j < order.Contracts.Count; j++)
                {
                    Contract contract = order.Contracts[j];
                    if (contract.Freighter == null || contract.Freighter.Cargo == null || contract.Freighter.HasBeenDestroyed)
                    {
                        continue;
                    }
                    if (order.CommodityComponent != null)
                    {
                        Cargo cargo = contract.Freighter.Cargo.GetCargo(order.CommodityComponent, oldEmpire);
                        if (cargo != null && cargo.Amount >= contract.AmountToFulfill)
                        {
                            Cargo cargo2 = new Cargo(order.CommodityComponent, contract.AmountToFulfill, newEmpire);
                            if (cargo.Amount == contract.AmountToFulfill)
                            {
                                contract.Freighter.Cargo.Remove(cargo);
                            }
                            else
                            {
                                cargo.Amount -= contract.AmountToFulfill;
                            }
                            contract.Freighter.Cargo.Add(cargo2);
                        }
                    }
                    else
                    {
                        if (order.CommodityResource == null)
                        {
                            continue;
                        }
                        Cargo cargo3 = contract.Freighter.Cargo.GetCargo(order.CommodityResource, oldEmpire);
                        if (cargo3 != null && cargo3.Amount >= contract.AmountToFulfill)
                        {
                            Cargo cargo4 = new Cargo(order.CommodityResource, contract.AmountToFulfill, newEmpire);
                            if (cargo3.Amount == contract.AmountToFulfill)
                            {
                                contract.Freighter.Cargo.Remove(cargo3);
                            }
                            else
                            {
                                cargo3.Amount -= contract.AmountToFulfill;
                            }
                            contract.Freighter.Cargo.Add(cargo4);
                        }
                    }
                }
            }
        }

        public void TakeOwnershipOfCargo(CargoList cargoList, Empire oldEmpire, Empire newEmpire)
        {
            if (cargoList == null)
            {
                return;
            }
            int num = -1;
            if (oldEmpire != null)
            {
                num = oldEmpire.EmpireId;
            }
            CargoList cargoList2 = new CargoList();
            CargoList cargoList3 = new CargoList();
            for (int i = 0; i < cargoList.Count; i++)
            {
                Cargo cargo = cargoList[i];
                if (cargo != null && (cargo.EmpireId == num || cargo.EmpireId < 0 || cargo.EmpireId == _Galaxy.IndependentEmpire.EmpireId))
                {
                    Cargo cargo2 = null;
                    if (cargo.CommodityIsComponent)
                    {
                        cargo2 = new Cargo(cargo.Component, cargo.Amount, newEmpire, cargo.Reserved);
                    }
                    else if (cargo.CommodityIsResource)
                    {
                        cargo2 = new Cargo(cargo.Resource, cargo.Amount, newEmpire, cargo.Reserved);
                    }
                    cargoList2.Add(cargo2);
                    cargoList3.Add(cargo);
                }
            }
            for (int j = 0; j < cargoList3.Count; j++)
            {
                cargoList.Remove(cargoList3[j]);
            }
            foreach (Cargo item in cargoList2)
            {
                cargoList.Add(item);
            }
        }

        public void DestroyUnreservedCargoOfEmpire(CargoList cargoList, Empire empire)
        {
            BaconEmpire.DestroyUnreservedCargoOfEmpire(cargoList, empire);
        }

        public void TakeOwnershipOfBuiltObject(BuiltObject builtObject, Empire newEmpire)
        {
            TakeOwnershipOfBuiltObject(builtObject, newEmpire, setDesignAsObsolete: false);
        }

        public void TakeOwnershipOfBuiltObject(BuiltObject builtObject, Empire newEmpire, bool setDesignAsObsolete)
        {
            TakeOwnershipOfBuiltObject(builtObject, newEmpire, setDesignAsObsolete, removeFromFleet: true);
        }

        public void TakeOwnershipOfBuiltObject(BuiltObject builtObject, Empire newEmpire, bool setDesignAsObsolete, bool removeFromFleet)
        {
            BaconEmpire.TakePossessionOfBuiltObject(this, builtObject, newEmpire);
            Empire actualEmpire = builtObject.ActualEmpire;
            _Galaxy.CheckTriggerEvent(builtObject.GameEventId, newEmpire, EventTriggerType.Capture, null);
            if (removeFromFleet && builtObject.ShipGroup != null)
            {
                builtObject.LeaveShipGroup();
            }
            if (actualEmpire != null)
            {
                int num = actualEmpire.SpacePorts.IndexOf(builtObject);
                if (num >= 0)
                {
                    actualEmpire.SpacePorts.RemoveAt(num);
                }
                int num2 = actualEmpire.ConstructionYards.IndexOf(builtObject);
                if (num2 >= 0)
                {
                    actualEmpire.ConstructionYards.RemoveAt(num2);
                }
                int num3 = actualEmpire.MiningStations.IndexOf(builtObject);
                if (num3 >= 0)
                {
                    actualEmpire.MiningStations.RemoveAt(num3);
                }
                int num4 = actualEmpire.BuiltObjects.IndexOf(builtObject);
                if (num4 >= 0)
                {
                    actualEmpire.BuiltObjects.RemoveAt(num4);
                }
                int num5 = actualEmpire.PrivateBuiltObjects.IndexOf(builtObject);
                if (num5 >= 0)
                {
                    actualEmpire.PrivateBuiltObjects.RemoveAt(num5);
                }
                int num6 = actualEmpire.ResourceExtractors.IndexOf(builtObject);
                if (num6 >= 0)
                {
                    actualEmpire.ResourceExtractors.RemoveAt(num6);
                }
                int num7 = actualEmpire.Manufacturers.IndexOf(builtObject);
                if (num7 >= 0)
                {
                    actualEmpire.Manufacturers.RemoveAt(num7);
                }
                int num8 = actualEmpire.LongRangeScanners.IndexOf(builtObject);
                if (num8 >= 0)
                {
                    actualEmpire.LongRangeScanners.RemoveAt(num8);
                }
                int num9 = actualEmpire.ResearchFacilities.IndexOf(builtObject);
                if (num9 >= 0)
                {
                    actualEmpire.ResearchFacilities.RemoveAt(num9);
                }
                int num10 = actualEmpire.ResortBases.IndexOf(builtObject);
                if (num10 >= 0)
                {
                    actualEmpire.ResortBases.RemoveAt(num10);
                }
                int num11 = actualEmpire.PlanetDestroyers.IndexOf(builtObject);
                if (num11 >= 0)
                {
                    actualEmpire.PlanetDestroyers.RemoveAt(num11);
                }
                int num12 = actualEmpire.RefuellingDepots.IndexOf(builtObject);
                if (num12 >= 0)
                {
                    actualEmpire.RefuellingDepots.RemoveAt(num12);
                }
                int num13 = actualEmpire.Freighters.IndexOf(builtObject);
                if (num13 >= 0)
                {
                    actualEmpire.Freighters.RemoveAt(num13);
                }
                int num14 = actualEmpire.ConstructionShips.IndexOf(builtObject);
                if (num14 >= 0)
                {
                    actualEmpire.ConstructionShips.RemoveAt(num14);
                }
                int num15 = actualEmpire.Outlaws.IndexOf(builtObject);
                if (num15 >= 0)
                {
                    actualEmpire.Outlaws.RemoveAt(num15);
                }
            }
            else if (_Galaxy.AbandonedBuiltObjects.Contains(builtObject))
            {
                _Galaxy.AbandonedBuiltObjects.Remove(builtObject);
            }
            builtObject.Empire = newEmpire;
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire != null && empire.Outlaws != null && empire.Outlaws.Contains(builtObject))
                {
                    empire.Outlaws.Remove(builtObject);
                }
            }
            for (int j = 0; j < _Galaxy.PirateEmpires.Count; j++)
            {
                Empire empire2 = _Galaxy.PirateEmpires[j];
                if (empire2 != null && empire2.Outlaws != null && empire2.Outlaws.Contains(builtObject))
                {
                    empire2.Outlaws.Remove(builtObject);
                }
            }
            if (actualEmpire != null && actualEmpire.PirateEmpireBaseHabitat != null && (newEmpire == null || newEmpire.PirateEmpireBaseHabitat == null) && builtObject.Role == BuiltObjectRole.Base)
            {
                for (int k = 0; k < _Galaxy.Empires.Count; k++)
                {
                    Empire empire3 = _Galaxy.Empires[k];
                    if (empire3.KnownPirateBases.Contains(builtObject))
                    {
                        empire3.KnownPirateBases.Remove(builtObject);
                    }
                }
            }
            if (newEmpire != null)
            {
                ClearAttackersFromEmpire(builtObject, newEmpire);
                builtObject.Attackers.Clear();
                builtObject.CurrentTarget = null;
                if (builtObject.Mission != null)
                {
                    builtObject.Mission.Clear();
                }
                builtObject.RevertMission = null;
                builtObject.SubsequentMissions.Clear();
                newEmpire.CancelAllShipAttacksNonEnemies(builtObject);
                _Galaxy.CheckCancelIntelligenceMissionsWithTarget(builtObject);
                if (actualEmpire != null)
                {
                    if (builtObject.Owner == actualEmpire)
                    {
                        builtObject.Owner = newEmpire;
                        newEmpire.BuiltObjects.Add(builtObject);
                    }
                    else
                    {
                        builtObject.Owner = null;
                        newEmpire.PrivateBuiltObjects.Add(builtObject);
                    }
                }
                else
                {
                    switch (builtObject.SubRole)
                    {
                        case BuiltObjectSubRole.Escort:
                        case BuiltObjectSubRole.Frigate:
                        case BuiltObjectSubRole.Destroyer:
                        case BuiltObjectSubRole.Cruiser:
                        case BuiltObjectSubRole.CapitalShip:
                        case BuiltObjectSubRole.TroopTransport:
                        case BuiltObjectSubRole.Carrier:
                        case BuiltObjectSubRole.ResupplyShip:
                        case BuiltObjectSubRole.ExplorationShip:
                        case BuiltObjectSubRole.ColonyShip:
                        case BuiltObjectSubRole.ConstructionShip:
                        case BuiltObjectSubRole.SmallSpacePort:
                        case BuiltObjectSubRole.MediumSpacePort:
                        case BuiltObjectSubRole.LargeSpacePort:
                        case BuiltObjectSubRole.ResortBase:
                        case BuiltObjectSubRole.GenericBase:
                        case BuiltObjectSubRole.EnergyResearchStation:
                        case BuiltObjectSubRole.WeaponsResearchStation:
                        case BuiltObjectSubRole.HighTechResearchStation:
                        case BuiltObjectSubRole.MonitoringStation:
                        case BuiltObjectSubRole.DefensiveBase:
                            builtObject.Owner = newEmpire;
                            newEmpire.BuiltObjects.Add(builtObject);
                            break;
                        case BuiltObjectSubRole.SmallFreighter:
                        case BuiltObjectSubRole.MediumFreighter:
                        case BuiltObjectSubRole.LargeFreighter:
                        case BuiltObjectSubRole.GasMiningShip:
                        case BuiltObjectSubRole.MiningShip:
                        case BuiltObjectSubRole.GasMiningStation:
                        case BuiltObjectSubRole.MiningStation:
                            builtObject.Owner = null;
                            newEmpire.PrivateBuiltObjects.Add(builtObject);
                            break;
                    }
                }
                Design design = builtObject.Design.Clone();
                design.Empire = newEmpire;
                Design design2 = null;
                for (int l = 0; l < newEmpire.Designs.Count; l++)
                {
                    Design design3 = newEmpire.Designs[l];
                    if (design3.IsEquivalent(design) && design3.Name == design.Name)
                    {
                        design2 = design3;
                        break;
                    }
                }
                if (design2 == null)
                {
                    design.BuildCount = 1;
                    if (setDesignAsObsolete)
                    {
                        design.IsObsolete = true;
                    }
                    if (design.Stance == BuiltObjectStance.AttackUnallied)
                    {
                        design.Stance = BuiltObjectStance.AttackEnemies;
                    }
                    newEmpire.Designs.Add(design);
                    builtObject.Design = design;
                }
                else
                {
                    design2.BuildCount++;
                    if (design2.Stance == BuiltObjectStance.AttackUnallied)
                    {
                        design2.Stance = BuiltObjectStance.AttackEnemies;
                    }
                    builtObject.Design = design2;
                }
                if (newEmpire.PirateEmpireBaseHabitat == null)
                {
                    builtObject.PirateEmpireId = 0;
                }
                else
                {
                    builtObject.PirateEmpireId = (byte)newEmpire.EmpireId;
                    switch (builtObject.SubRole)
                    {
                        case BuiltObjectSubRole.SmallFreighter:
                        case BuiltObjectSubRole.MediumFreighter:
                        case BuiltObjectSubRole.LargeFreighter:
                        case BuiltObjectSubRole.PassengerShip:
                        case BuiltObjectSubRole.GasMiningShip:
                        case BuiltObjectSubRole.MiningShip:
                            builtObject.Empire = _Galaxy.IndependentEmpire;
                            if (!_Galaxy.IndependentEmpire.PrivateBuiltObjects.Contains(builtObject))
                            {
                                _Galaxy.IndependentEmpire.PrivateBuiltObjects.Add(builtObject);
                            }
                            if (newEmpire.PrivateBuiltObjects != null && !newEmpire.PrivateBuiltObjects.Contains(builtObject))
                            {
                                newEmpire.PrivateBuiltObjects.Add(builtObject);
                            }
                            if (newEmpire.BuiltObjects != null && newEmpire.BuiltObjects.Contains(builtObject))
                            {
                                newEmpire.BuiltObjects.Remove(builtObject);
                            }
                            break;
                    }
                }
            }
            else
            {
                builtObject.PlayerEmpireEncounterAction = BuiltObjectEncounterAction.Prompt;
                builtObject.ClearPreviousMissionRequirements();
                builtObject.Owner = null;
                builtObject.Empire = null;
                builtObject.PirateEmpireId = 0;
            }
            builtObject.ReDefine();
            if (builtObject.Fighters != null && builtObject.Fighters.Count > 0)
            {
                if (newEmpire == null)
                {
                    Fighter[] array = ListHelper.ToArrayThreadSafe(builtObject.Fighters);
                    for (int m = 0; m < array.Length; m++)
                    {
                        array[m].CompleteTeardown(_Galaxy);
                    }
                }
                else
                {
                    for (int n = 0; n < builtObject.Fighters.Count; n++)
                    {
                        builtObject.Fighters[n].Empire = newEmpire;
                        builtObject.Fighters[n].Owner = newEmpire;
                    }
                }
            }
            if (builtObject.Characters != null && builtObject.Characters.Count > 0)
            {
                Character[] array2 = ListHelper.ToArrayThreadSafe(builtObject.Characters);
                foreach (Character character in array2)
                {
                    character.Kill(_Galaxy);
                }
            }
            if (builtObject.Troops != null && builtObject.Troops.Count > 0)
            {
                if (newEmpire == _Galaxy.IndependentEmpire || newEmpire == null)
                {
                    for (int num17 = 0; num17 < builtObject.Troops.Count; num17++)
                    {
                        Troop troop = builtObject.Troops[num17];
                        actualEmpire?.Troops.Remove(troop);
                        troop.Empire = null;
                        troop.Colony = null;
                        troop.BuiltObject = null;
                    }
                    builtObject.Troops.Clear();
                }
                else
                {
                    for (int num18 = 0; num18 < builtObject.Troops.Count; num18++)
                    {
                        Troop troop2 = builtObject.Troops[num18];
                        actualEmpire?.Troops.Remove(troop2);
                        troop2.Empire = newEmpire;
                        newEmpire.Troops.Add(troop2);
                    }
                }
            }
            if (builtObject.TroopCapacity > 0 && newEmpire != null && newEmpire.Policy != null)
            {
                builtObject.SetTroopLoadoutsFromPolicy(newEmpire.Policy);
            }
            if (newEmpire != null)
            {
                DestroyUnreservedCargoOfEmpire(builtObject.Cargo, actualEmpire);
                TakeOwnershipOfCargo(builtObject.Cargo, actualEmpire, newEmpire);
            }
            if (actualEmpire != null)
            {
                if (newEmpire != null)
                {
                    TakeOwnershipOfOrders(builtObject, actualEmpire, newEmpire);
                }
                else
                {
                    if (builtObject.ContractsToFulfill != null && builtObject.ContractsToFulfill.Count > 0)
                    {
                        builtObject.CheckCancelContracts();
                    }
                    OrderList orders = _Galaxy.Orders.GetOrders(builtObject);
                    if (orders.Count > 0)
                    {
                        lock (_Galaxy.Orders._LockObject)
                        {
                            foreach (Order item in orders)
                            {
                                _Galaxy.Orders.Remove(item);
                            }
                        }
                    }
                    if (builtObject.Cargo != null)
                    {
                        builtObject.Cargo.Clear();
                    }
                }
            }
            if (builtObject.ConstructionQueue != null && builtObject.ConstructionQueue.ConstructionYards.Count > 0)
            {
                for (int num19 = 0; num19 < builtObject.ConstructionQueue.ConstructionYards.Count; num19++)
                {
                    ConstructionYard constructionYard = builtObject.ConstructionQueue.ConstructionYards[num19];
                    if (constructionYard.ShipUnderConstruction != null && constructionYard.ShipUnderConstruction.Empire == actualEmpire)
                    {
                        TakeOwnershipOfBuiltObject(constructionYard.ShipUnderConstruction, newEmpire, setDesignAsObsolete);
                    }
                }
                if (builtObject.ConstructionQueue.ConstructionWaitQueue != null)
                {
                    for (int num20 = 0; num20 < builtObject.ConstructionQueue.ConstructionWaitQueue.Count; num20++)
                    {
                        BuiltObject builtObject2 = builtObject.ConstructionQueue.ConstructionWaitQueue[num20];
                        TakeOwnershipOfBuiltObject(builtObject2, newEmpire, setDesignAsObsolete);
                    }
                }
            }
            actualEmpire?.ResolveSystemVisibility(builtObject.Xpos, builtObject.Ypos, null, null);
            newEmpire?.ResolveSystemVisibility(builtObject.Xpos, builtObject.Ypos, null, null);
            builtObject.IsAutoControlled = true;
        }

        public void ClearFleetHomeBases(StellarObject currentBase)
        {
            if (ShipGroups == null || currentBase == null)
            {
                return;
            }
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (shipGroup == null || shipGroup.GatherPoint == null)
                {
                    continue;
                }
                bool flag = false;
                if (currentBase is Habitat)
                {
                    Habitat habitat = (Habitat)currentBase;
                    if (shipGroup.GatherPoint is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)shipGroup.GatherPoint;
                        if (builtObject.ParentHabitat != null && builtObject.ParentHabitat == habitat)
                        {
                            flag = true;
                        }
                    }
                    else if (shipGroup.GatherPoint is Habitat)
                    {
                        Habitat habitat2 = (Habitat)shipGroup.GatherPoint;
                        if (habitat2 == habitat)
                        {
                            flag = true;
                        }
                    }
                }
                else if (currentBase is BuiltObject)
                {
                    BuiltObject builtObject2 = (BuiltObject)currentBase;
                    if (shipGroup.GatherPoint is BuiltObject)
                    {
                        BuiltObject builtObject3 = (BuiltObject)shipGroup.GatherPoint;
                        if (builtObject3 != null && builtObject3 == builtObject2)
                        {
                            flag = true;
                        }
                    }
                    else if (shipGroup.GatherPoint is Habitat)
                    {
                        Habitat habitat3 = (Habitat)shipGroup.GatherPoint;
                        if (builtObject2.ParentHabitat != null && builtObject2.ParentHabitat == habitat3)
                        {
                            flag = true;
                        }
                    }
                }
                if (flag)
                {
                    StellarObject stellarObject = SelectFleetBase(shipGroup);
                    if (stellarObject != shipGroup.GatherPoint)
                    {
                        shipGroup.GatherPoint = stellarObject;
                    }
                    else
                    {
                        shipGroup.GatherPoint = null;
                    }
                }
            }
        }

        public void RecalculateColonyInfluenceRadiuses(bool empireHasWarptech)
        {
            if (Colonies != null)
            {
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Colonies[i]?.RecalculateColonyInfluenceRadius(empireHasWarptech);
                }
            }
        }

        private void CheckKnownPirateBases()
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < KnownPirateBases.Count; i++)
            {
                BuiltObject builtObject = KnownPirateBases[i];
                if (!_Galaxy.PirateEmpires.Contains(builtObject.Empire) || builtObject.Empire == null || builtObject.Empire == _Galaxy.IndependentEmpire)
                {
                    builtObjectList.Add(builtObject);
                }
            }
            foreach (BuiltObject item in builtObjectList)
            {
                KnownPirateBases.Remove(item);
            }
        }

        private void ProcessSubjugationTribute(double timePassed)
        {
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation.Type == DiplomaticRelationType.SubjugatedDominion && diplomaticRelation.Initiator != this)
                {
                    double num = AnnualTaxRevenue + ThisYearsForeignTradeBonuses + ThisYearsSpacePortIncome;
                    double num2 = num * Galaxy.SubjugationTributePercentage;
                    double num3 = timePassed / (double)Galaxy.RealSecondsInGalacticYear;
                    double num4 = num2 * num3;
                    diplomaticRelation.Initiator.StateMoney += num4;
                    StateMoney -= num4;
                }
            }
        }

        public double CalculateAnnualSubjugationTributeIncome()
        {
            double num = 0.0;
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation.Type == DiplomaticRelationType.SubjugatedDominion && diplomaticRelation.Initiator == this)
                {
                    Empire otherEmpire = diplomaticRelation.OtherEmpire;
                    double num2 = otherEmpire.AnnualTaxRevenue + otherEmpire.ThisYearsForeignTradeBonuses + otherEmpire.ThisYearsSpacePortIncome;
                    num += num2 * Galaxy.SubjugationTributePercentage;
                }
            }
            return num;
        }

        private void ProcessTradeBonuses(double timePassed)
        {
            double num = timePassed / (double)Galaxy.RealSecondsInGalacticYear;
            double num2 = Galaxy.TradeBonusAnnualIncrease * num;
            long currentStarDate = _Galaxy.CurrentStarDate;
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                diplomaticRelation.AgeTradeValues(currentStarDate);
                double num3 = 0.0;
                switch (diplomaticRelation.Type)
                {
                    case DiplomaticRelationType.FreeTradeAgreement:
                        diplomaticRelation.TradeBonus += num2;
                        diplomaticRelation.TradeBonus = Math.Min(diplomaticRelation.TradeBonus, Galaxy.TradeBonusMaximumFreeTrade);
                        num3 = diplomaticRelation.AnnualTradeBonus * num;
                        break;
                    case DiplomaticRelationType.MutualDefensePact:
                    case DiplomaticRelationType.Protectorate:
                        diplomaticRelation.TradeBonus += num2;
                        diplomaticRelation.TradeBonus = Math.Min(diplomaticRelation.TradeBonus, Galaxy.TradeBonusMaximumMutualDefense);
                        num3 = diplomaticRelation.AnnualTradeBonus * num;
                        break;
                    default:
                        diplomaticRelation.TradeBonus = 0.0;
                        break;
                }
                StateMoney += num3;
            }
        }

        private void MergeGalaxyMapsForSharedVisibilityEmpires()
        {
            for (int i = 0; i < _EmpiresSharedVisibility.Count; i++)
            {
                Empire empire = _EmpiresSharedVisibility[i];
                if (empire.Active)
                {
                    _Galaxy.MergeGalaxyMap(empire, this);
                }
            }
        }

        private void MergeKnownPirateBasesForSharedVisibilityEmpires()
        {
            for (int i = 0; i < _EmpiresSharedVisibility.Count; i++)
            {
                Empire empire = _EmpiresSharedVisibility[i];
                if (!empire.Active)
                {
                    continue;
                }
                for (int j = 0; j < empire.KnownPirateBases.Count; j++)
                {
                    BuiltObject builtObject = empire.KnownPirateBases[j];
                    if (!_KnownPirateBases.Contains(builtObject) && !builtObject.HasBeenDestroyed)
                    {
                        _KnownPirateBases.Add(builtObject);
                    }
                }
            }
        }

        private void InitiateEmpireSplit(double splinterPortion)
        {
            bool declareWar = false;
            if (Galaxy.Rnd.Next(0, 4) > 0)
            {
                declareWar = true;
            }
            InitiateEmpireSplit(splinterPortion, declareWar);
        }

        public void InitiateEmpireSplit(double splinterPortion, bool declareWar)
        {
            HabitatList coloniesLost = new HabitatList();
            Empire empire = SplinterEmpire(this, splinterPortion, out coloniesLost);
            if (empire == null)
            {
                return;
            }
            if (PirateEmpireBaseHabitat == null)
            {
                if (declareWar)
                {
                    EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(empire);
                    empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - 40.0;
                    DeclareWar(empire);
                }
                else
                {
                    EmpireEvaluation empireEvaluation2 = ObtainEmpireEvaluation(empire);
                    empireEvaluation2.IncidentEvaluation = empireEvaluation2.IncidentEvaluationRaw - 10.0;
                }
            }
            string text = "";
            string text2 = "";
            if (declareWar)
            {
                text2 = string.Format(TextResolver.GetText("Civil War in the EMPIRE"), Name);
                text = text + string.Format(TextResolver.GetText("A civil war is underway in the OTHEREMPIRE"), Name) + "\n\n";
            }
            else
            {
                text2 = string.Format(TextResolver.GetText("Revolution in the EMPIRE"), Name);
                text = text + string.Format(TextResolver.GetText("A split has occurred in the OTHEREMPIRE"), Name) + "\n\n";
            }
            text += string.Format(TextResolver.GetText("Empire Split Detail COLONYCOUNT EMPIRE NEWEMPIRE"), coloniesLost.Count.ToString(), Name, empire.Name);
            string text3 = TextResolver.GetText("Revolution!");
            string text4 = string.Format(TextResolver.GetText("Your Empire Split Detail COLONYCOUNT NEWEMPIRE"), coloniesLost.Count.ToString(), empire.Name);
            if (declareWar)
            {
                text3 = TextResolver.GetText("Civil War!");
                text4 = text4 + "\n\n" + TextResolver.GetText("We are now at war with these traitors");
            }
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire2 = _Galaxy.Empires[i];
                if (empire2 == this)
                {
                    empire2.SendEventMessageToEmpire(EventMessageType.EmpireSplits, text3, text4, empire, empire.Capital);
                }
                else
                {
                    if (empire2 == empire)
                    {
                        continue;
                    }
                    bool flag = false;
                    if (PirateEmpireBaseHabitat != null)
                    {
                        DiplomaticRelation diplomaticRelation = empire2.ObtainDiplomaticRelation(this);
                        if (diplomaticRelation.Type != 0)
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        PirateRelation pirateRelation = empire2.ObtainPirateRelation(this);
                        if (pirateRelation.Type != 0)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        empire2.SendEventMessageToEmpire(EventMessageType.EmpireSplits, text2, text, empire, empire.Capital);
                    }
                }
            }
            for (int j = 0; j < _Galaxy.PirateEmpires.Count; j++)
            {
                Empire empire3 = _Galaxy.PirateEmpires[j];
                if (empire3 == this)
                {
                    empire3.SendEventMessageToEmpire(EventMessageType.EmpireSplits, text3, text4, empire, empire.Capital);
                }
                else if (empire3 != empire)
                {
                    bool flag2 = false;
                    PirateRelation pirateRelation2 = empire3.ObtainPirateRelation(this);
                    if (pirateRelation2.Type != 0)
                    {
                        flag2 = true;
                    }
                    if (flag2)
                    {
                        empire3.SendEventMessageToEmpire(EventMessageType.EmpireSplits, text2, text, empire, empire.Capital);
                    }
                }
            }
            LastDisasterDate = _Galaxy.CurrentStarDate;
        }

        private void RandomEventPirateControlledColonyGivesShip()
        {
            if (PirateEmpireBaseHabitat == null)
            {
                return;
            }
            Habitat habitat = null;
            if (Colonies.Count > 0)
            {
                habitat = Colonies[Galaxy.Rnd.Next(0, Colonies.Count)];
            }
            if (habitat == null || !habitat.GetPirateControl().CheckFactionHasControl(this))
            {
                return;
            }
            Design design = null;
            switch (Galaxy.Rnd.Next(0, 5))
            {
                case 0:
                    design = LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.SmallFreighter, this);
                    break;
                case 1:
                    design = LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.MediumFreighter, this);
                    break;
                case 2:
                    design = LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.Escort, this);
                    break;
                case 3:
                    design = LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.Frigate, this);
                    break;
                case 4:
                    design = LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.ConstructionShip, this);
                    break;
            }
            if (design == null)
            {
                return;
            }
            BuiltObject builtObject = GenerateNewBuiltObject(design, habitat);
            if (builtObject == null)
            {
                return;
            }
            Character character = null;
            if (Galaxy.Rnd.Next(0, 2) == 1)
            {
                bool isRandomCharacter = false;
                character = GenerateNewCharacter(CharacterRole.ShipCaptain, builtObject, activate: true, out isRandomCharacter);
            }
            object additionalData = builtObject;
            string empty = string.Empty;
            string empty2 = string.Empty;
            if (character != null)
            {
                additionalData = character;
                switch (design.Role)
                {
                    case BuiltObjectRole.Freight:
                        empty = TextResolver.GetText("Smuggler Joins Us");
                        empty2 = string.Format(TextResolver.GetText("Pirate Event New Freighter With Captain"), character.Name, builtObject.Name, habitat.Name);
                        character.AddTrait(CharacterTraitType.Smuggler, starting: true, _Galaxy);
                        break;
                    case BuiltObjectRole.Military:
                        builtObject.Name = _Galaxy.SelectRandomUniqueMilitaryShipName();
                        empty = TextResolver.GetText("Bounty Hunter Joins Us");
                        empty2 = string.Format(TextResolver.GetText("Pirate Event New Military Ship With Captain"), character.Name, builtObject.Name, habitat.Name);
                        character.AddTrait(CharacterTraitType.BountyHunter, starting: true, _Galaxy);
                        break;
                    default:
                        empty = TextResolver.GetText("Ship Captain Joins Us");
                        empty2 = string.Format(TextResolver.GetText("Pirate Event New Ship With Captain"), character.Name, builtObject.Name, habitat.Name);
                        break;
                }
            }
            else
            {
                switch (design.Role)
                {
                    case BuiltObjectRole.Freight:
                        empty = TextResolver.GetText("Smuggling Ship Acquired");
                        empty2 = string.Format(TextResolver.GetText("Pirate Event New Freighter"), builtObject.Name, habitat.Name);
                        break;
                    case BuiltObjectRole.Military:
                        builtObject.Name = _Galaxy.SelectRandomUniqueMilitaryShipName();
                        empty = TextResolver.GetText("Military Ship Acquired");
                        empty2 = string.Format(TextResolver.GetText("Pirate Event New Military Ship"), habitat.Name, builtObject.Name);
                        break;
                    default:
                        empty = TextResolver.GetText("Ship Acquired");
                        empty2 = string.Format(TextResolver.GetText("Pirate Event New Ship"), habitat.Name, builtObject.Name);
                        break;
                }
            }
            SendEventMessageToEmpire(EventMessageType.FreeSuperShip, empty, empty2, additionalData, builtObject);
        }

        public void RandomEventRareResourceIntercepted()
        {
            bool flag = false;
            byte resource = byte.MaxValue;
            Empire empire = null;
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire2 = _Galaxy.Empires[i];
                if (empire2 != this && !empire2.Reclusive && empire2.CheckEmpireSuppliesRestrictedResources(out resource))
                {
                    flag = true;
                    empire = empire2;
                    break;
                }
            }
            if (flag && resource != byte.MaxValue && empire != null && empire.Capital != null)
            {
                BuiltObject spaceport = _Galaxy.FastFindNearestSpacePort(empire.Capital.Xpos, empire.Capital.Ypos, this);
                RandomEventRareResourceIntercepted(new Resource(resource), spaceport, empire);
            }
        }

        public void RandomEventRareResourceIntercepted(Resource resource, BuiltObject spaceport, Empire supplyingEmpire)
        {
            if (spaceport == null)
            {
                return;
            }
            spaceport.Cargo.Add(new Cargo(resource, 200, this));
            string empty = string.Empty;
            string text = string.Format(arg1: (spaceport.NearestSystemStar == null) ? Capital.Name : spaceport.NearestSystemStar.Name, format: TextResolver.GetText("Intercept Resource RESOURCE SYSTEM SPACEPORT"), arg0: resource.Name, arg2: spaceport.Name);
            bool flag = false;
            if (supplyingEmpire != null)
            {
                if (PirateEmpireBaseHabitat == null)
                {
                    DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(supplyingEmpire);
                    if (diplomaticRelation.Type != 0)
                    {
                        flag = true;
                    }
                }
                else
                {
                    PirateRelation pirateRelation = ObtainPirateRelation(supplyingEmpire);
                    if (pirateRelation.Type != 0)
                    {
                        flag = true;
                    }
                }
            }
            if (!flag)
            {
                text = text + "\n\n" + string.Format(TextResolver.GetText("Intercept Resource unclear origin"), resource.Name);
            }
            SendEventMessageToEmpire(EventMessageType.RareResourceIntercepted, TextResolver.GetText("Rare Resource Intercepted"), text, resource, spaceport);
        }

        private void RandomEventUncoverKnownLocations()
        {
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire == this || empire == null)
                {
                    continue;
                }
                bool flag = false;
                if (empire.PirateEmpireBaseHabitat == null && PirateEmpireBaseHabitat == null)
                {
                    DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                    if (diplomaticRelation.Type != 0)
                    {
                        flag = true;
                    }
                }
                else
                {
                    PirateRelation pirateRelation = ObtainPirateRelation(empire);
                    if (pirateRelation.Type != 0)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    continue;
                }
                GalaxyLocationList galaxyLocationList = empire.KnownGalaxyLocations.FindLocations(GalaxyLocationType.PlanetDestroyer);
                if (galaxyLocationList != null && galaxyLocationList.Count > 0)
                {
                    for (int j = 0; j < empire.BuiltObjects.Count; j++)
                    {
                        BuiltObject builtObject = empire.BuiltObjects[j];
                        if (builtObject.SubRole != BuiltObjectSubRole.ConstructionShip || builtObject.Mission == null || (builtObject.Mission.Type != BuiltObjectMissionType.Build && builtObject.Mission.Type != BuiltObjectMissionType.BuildRepair))
                        {
                            continue;
                        }
                        foreach (GalaxyLocation item in galaxyLocationList)
                        {
                            if (KnownGalaxyLocations.Contains(item) || builtObject.Mission.TargetBuiltObject != item.RelatedBuiltObject)
                            {
                                continue;
                            }
                            BuiltObject relatedBuiltObject = item.RelatedBuiltObject;
                            string text = string.Empty;
                            BuiltObject builtObject2 = _Galaxy.FastFindNearestLongRangeScanner((int)item.Xpos, (int)item.Ypos, this);
                            if (builtObject2 == null)
                            {
                                builtObject2 = _Galaxy.FindNearestBuiltObject((int)item.Xpos, (int)item.Ypos, this, BuiltObjectSubRole.ExplorationShip, fullyFunctional: true);
                                if (builtObject2 != null)
                                {
                                    text = string.Format(TextResolver.GetText("Communications Intercept SHIPNAME EMPIRE"), builtObject2.Name, empire.Name) + ".\n\n";
                                }
                            }
                            else
                            {
                                text = string.Format(TextResolver.GetText("Communications Intercept MONITORINGSTATION EMPIRE"), builtObject2.Name, empire.Name) + ".\n\n";
                            }
                            if (builtObject2 == null)
                            {
                                continue;
                            }
                            string message = text + string.Format(TextResolver.GetText("Communications Intercept Planet Destroyer"), relatedBuiltObject.NearestSystemStar.Name, Galaxy.ResolveSectorDescription(Galaxy.ResolveSector(relatedBuiltObject.Xpos, relatedBuiltObject.Ypos)), empire.Name);
                            KnownGalaxyLocations.Add(item);
                            AddLocationHint(new Point((int)item.Xpos + (int)item.Width / 2, (int)item.Ypos + (int)item.Height / 2));
                            object[] additionalData = new object[2] { empire, item };
                            SendEventMessageToEmpire(EventMessageType.UncoverPlanetDestroyerConstruction, TextResolver.GetText("Secret Construction Project Revealed"), message, additionalData, relatedBuiltObject);
                            if (this == _Galaxy.PlayerEmpire)
                            {
                                return;
                            }
                            bool flag2 = false;
                            if (PirateEmpireBaseHabitat == null && empire.PirateEmpireBaseHabitat == null)
                            {
                                DiplomaticRelation diplomaticRelation2 = ObtainDiplomaticRelation(empire);
                                if (diplomaticRelation2.Type == DiplomaticRelationType.MutualDefensePact || diplomaticRelation2.Type == DiplomaticRelationType.Protectorate || diplomaticRelation2.Type == DiplomaticRelationType.FreeTradeAgreement)
                                {
                                    flag2 = true;
                                }
                            }
                            else
                            {
                                PirateRelation pirateRelation2 = ObtainPirateRelation(empire);
                                if (pirateRelation2.Type == PirateRelationType.Protection)
                                {
                                    flag2 = true;
                                }
                            }
                            if (!flag2)
                            {
                                double num = (double)MilitaryPotency / (double)empire.MilitaryPotency;
                                double num2 = 0.3 * ((double)DominantRace.CautionLevel / 100.0);
                                if (num > num2)
                                {
                                    ExposePlanetDestroyerConstruction(empire, item, this);
                                }
                            }
                            return;
                        }
                    }
                }
                galaxyLocationList = empire.KnownGalaxyLocations.FindLocations(GalaxyLocationType.DebrisField);
                if (galaxyLocationList != null && galaxyLocationList.Count > 0)
                {
                    foreach (GalaxyLocation item2 in galaxyLocationList)
                    {
                        if (KnownGalaxyLocations.Contains(item2))
                        {
                            continue;
                        }
                        string text2 = string.Empty;
                        BuiltObject builtObject3 = _Galaxy.FastFindNearestLongRangeScanner((int)item2.Xpos, (int)item2.Ypos, this);
                        if (builtObject3 == null)
                        {
                            builtObject3 = _Galaxy.FindNearestBuiltObject((int)item2.Xpos, (int)item2.Ypos, this, BuiltObjectSubRole.ExplorationShip, fullyFunctional: true);
                            if (builtObject3 != null)
                            {
                                text2 = string.Format(TextResolver.GetText("Communications Intercept SHIPNAME EMPIRE"), builtObject3.Name, empire.Name) + ".\n\n";
                            }
                        }
                        else
                        {
                            text2 = string.Format(TextResolver.GetText("Communications Intercept MONITORINGSTATION EMPIRE"), builtObject3.Name, empire.Name) + ".\n\n";
                        }
                        if (builtObject3 != null)
                        {
                            Habitat habitat = _Galaxy.FastFindNearestSystem(item2.Xpos, item2.Ypos);
                            string message2 = text2 + string.Format(TextResolver.GetText("Communications Intercept Debris Field"), empire.Name, habitat.Name, Galaxy.ResolveSectorDescription(Galaxy.ResolveSector(item2.Xpos, item2.Ypos)));
                            KnownGalaxyLocations.Add(item2);
                            Point point = new Point((int)item2.Xpos + (int)item2.Width / 2, (int)item2.Ypos + (int)item2.Height / 2);
                            AddLocationHint(point);
                            SendEventMessageToEmpire(EventMessageType.UncoverKnownLocation, TextResolver.GetText("Debris Field Revealed"), message2, item2, point);
                            return;
                        }
                    }
                }
                galaxyLocationList = empire.KnownGalaxyLocations.FindLocations(GalaxyLocationType.RestrictedArea);
                if (galaxyLocationList == null || galaxyLocationList.Count <= 0)
                {
                    continue;
                }
                foreach (GalaxyLocation item3 in galaxyLocationList)
                {
                    if (KnownGalaxyLocations.Contains(item3))
                    {
                        continue;
                    }
                    string text3 = string.Empty;
                    BuiltObject builtObject4 = _Galaxy.FastFindNearestLongRangeScanner((int)item3.Xpos, (int)item3.Ypos, this);
                    if (builtObject4 == null)
                    {
                        builtObject4 = _Galaxy.FindNearestBuiltObject((int)item3.Xpos, (int)item3.Ypos, this, BuiltObjectSubRole.ExplorationShip, fullyFunctional: true);
                        if (builtObject4 != null)
                        {
                            text3 = string.Format(TextResolver.GetText("Communications Intercept SHIPNAME EMPIRE"), builtObject4.Name, empire.Name) + ".\n\n";
                        }
                    }
                    else
                    {
                        text3 = string.Format(TextResolver.GetText("Communications Intercept MONITORINGSTATION EMPIRE"), builtObject4.Name, empire.Name) + ".\n\n";
                    }
                    if (builtObject4 != null)
                    {
                        Habitat habitat2 = _Galaxy.FastFindNearestSystem(item3.Xpos, item3.Ypos);
                        string message3 = text3 + string.Format(TextResolver.GetText("Communications Intercept Restricted Area"), empire.Name, habitat2.Name, Galaxy.ResolveSectorDescription(Galaxy.ResolveSector(item3.Xpos, item3.Ypos)));
                        KnownGalaxyLocations.Add(item3);
                        Point point2 = new Point((int)item3.Xpos + (int)item3.Width / 2, (int)item3.Ypos + (int)item3.Height / 2);
                        AddLocationHint(point2);
                        SendEventMessageToEmpire(EventMessageType.UncoverKnownLocation, TextResolver.GetText("Restricted Area Revealed"), message3, item3, point2);
                        return;
                    }
                }
            }
        }

        public void ExposePlanetDestroyerConstruction(Empire planetDestroyerBuilder, GalaxyLocation planetDestroyerLocation, Empire exposer)
        {
            if (planetDestroyerBuilder.PirateEmpireBaseHabitat != null)
            {
                return;
            }
            Habitat habitat = _Galaxy.FastFindNearestSystem(planetDestroyerLocation.Xpos, planetDestroyerLocation.Ypos);
            string description = string.Format(TextResolver.GetText("Warning Planet Destroyer Construction"), planetDestroyerBuilder.Name, habitat.Name, Galaxy.ResolveSectorDescription(Galaxy.ResolveSector(habitat.Xpos, habitat.Ypos)));
            if (exposer.PirateEmpireBaseHabitat == null)
            {
                for (int i = 0; i < exposer.DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = exposer.DiplomaticRelations[i];
                    if (diplomaticRelation.Type != 0 && diplomaticRelation.OtherEmpire != planetDestroyerBuilder)
                    {
                        EmpireEvaluation empireEvaluation = diplomaticRelation.OtherEmpire.ObtainEmpireEvaluation(planetDestroyerBuilder);
                        empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - 12.0;
                        if (!diplomaticRelation.OtherEmpire.KnownGalaxyLocations.Contains(planetDestroyerLocation))
                        {
                            diplomaticRelation.OtherEmpire.KnownGalaxyLocations.Add(planetDestroyerLocation);
                        }
                        exposer.SendMessageToEmpire(diplomaticRelation.OtherEmpire, EmpireMessageType.GeneralWarning, planetDestroyerLocation.RelatedBuiltObject, description);
                    }
                }
            }
            else
            {
                for (int j = 0; j < exposer.PirateRelations.Count; j++)
                {
                    PirateRelation pirateRelation = exposer.PirateRelations[j];
                    if (pirateRelation.Type != 0 && pirateRelation.OtherEmpire != planetDestroyerBuilder && pirateRelation.OtherEmpire.PirateEmpireBaseHabitat == null)
                    {
                        EmpireEvaluation empireEvaluation2 = pirateRelation.OtherEmpire.ObtainEmpireEvaluation(planetDestroyerBuilder);
                        empireEvaluation2.IncidentEvaluation = empireEvaluation2.IncidentEvaluationRaw - 12.0;
                        if (!pirateRelation.OtherEmpire.KnownGalaxyLocations.Contains(planetDestroyerLocation))
                        {
                            pirateRelation.OtherEmpire.KnownGalaxyLocations.Add(planetDestroyerLocation);
                        }
                        exposer.SendMessageToEmpire(pirateRelation.OtherEmpire, EmpireMessageType.GeneralWarning, planetDestroyerLocation.RelatedBuiltObject, description);
                    }
                }
            }
            planetDestroyerBuilder.CivilityRating -= 7.0;
            if (exposer.PirateEmpireBaseHabitat == null)
            {
                EmpireEvaluation empireEvaluation3 = planetDestroyerBuilder.ObtainEmpireEvaluation(exposer);
                empireEvaluation3.IncidentEvaluation = empireEvaluation3.IncidentEvaluationRaw - 20.0;
            }
            else
            {
                planetDestroyerBuilder.ChangePirateEvaluation(exposer, -20f, PirateRelationEvaluationType.DetectedIntelligenceMissions);
            }
        }

        private void RandomEventUncoverPirateAttackFunding()
        {
            for (int i = 0; i < _Galaxy.PirateEmpires.Count; i++)
            {
                Empire empire = _Galaxy.PirateEmpires[i];
                if (!KnownPirateEmpires.Contains(empire))
                {
                    continue;
                }
                EmpireActivityList empireActivityList = empire.PirateMissions.ResolveActivitiesByType(EmpireActivityType.Attack);
                if (empireActivityList == null || empireActivityList.Count <= 0)
                {
                    continue;
                }
                for (int j = 0; j < empireActivityList.Count; j++)
                {
                    EmpireActivity empireActivity = empireActivityList[j];
                    if (empireActivity.RequestingEmpire == this || empireActivity.AssignedEmpire != empire)
                    {
                        continue;
                    }
                    if (empireActivity.TargetEmpire == this)
                    {
                        string message = string.Format(TextResolver.GetText("Uncover Pirate Attack Funding Against Us"), empireActivity.RequestingEmpire.Name, empire.Name);
                        SendEventMessageToEmpire(EventMessageType.UncoverPirateAttackFundingYourEmpire, TextResolver.GetText("Pirates secretly funded to attack us"), message, empireActivity, null);
                        EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(empireActivity.RequestingEmpire);
                        empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - 15.0;
                        empireActivity.RequestingEmpire.CivilityRating -= 3.0;
                        if (this != _Galaxy.PlayerEmpire)
                        {
                            string description = string.Format(TextResolver.GetText("Uncover Pirate Attack Funding Against Us Threaten"), empire.Name);
                            SendMessageToEmpire(empireActivity.RequestingEmpire, EmpireMessageType.GeneralWarning, null, description);
                        }
                        return;
                    }
                    DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empireActivity.TargetEmpire);
                    DiplomaticRelation diplomaticRelation2 = ObtainDiplomaticRelation(empireActivity.RequestingEmpire);
                    if (diplomaticRelation.Type != 0 && diplomaticRelation2.Type != 0)
                    {
                        string message2 = string.Format(TextResolver.GetText("Uncover Pirate Attack Funding Against Other"), empireActivity.RequestingEmpire.Name, empire.Name, empireActivity.TargetEmpire.Name);
                        SendEventMessageToEmpire(EventMessageType.UncoverPirateAttackFundingAnotherEmpire, TextResolver.GetText("Pirates secretly funded to attack empire"), message2, empireActivity, null);
                        return;
                    }
                }
            }
        }

        public void EmpireEventRogueFleetDefects()
        {
            Empire empireToDefectTo = null;
            double num = double.MaxValue;
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation.Type == DiplomaticRelationType.NotMet || diplomaticRelation.OtherEmpire == this)
                {
                    continue;
                }
                double num2 = 1.0;
                num2 = diplomaticRelation.Type switch
                {
                    DiplomaticRelationType.War => 2.0,
                    DiplomaticRelationType.TradeSanctions => 1.5,
                    DiplomaticRelationType.None => 1.2,
                    _ => 1.0,
                };
                if (diplomaticRelation.OtherEmpire.CivilityRating >= -5.0)
                {
                    double d = diplomaticRelation.OtherEmpire.CivilityRating + 6.0;
                    num2 *= Math.Sqrt(Math.Sqrt(d));
                    double num3 = _Galaxy.CalculateDistance(Capital.Xpos, Capital.Ypos, diplomaticRelation.OtherEmpire.Capital.Xpos, diplomaticRelation.OtherEmpire.Capital.Ypos);
                    num3 /= num2;
                    if (num3 < num)
                    {
                        empireToDefectTo = diplomaticRelation.OtherEmpire;
                        num = num3;
                    }
                }
            }
            EmpireEventRogueFleetDefects(empireToDefectTo);
        }

        public ShipGroup EmpireEventRogueFleetDefects(Empire empireToDefectTo)
        {
            ShipGroup result = null;
            if (ShipGroups.Count > 1)
            {
                if (empireToDefectTo != null)
                {
                    ShipGroup shipGroup = null;
                    double num = double.MaxValue;
                    for (int i = 0; i < ShipGroups.Count; i++)
                    {
                        ShipGroup shipGroup2 = ShipGroups[i];
                        double num2 = _Galaxy.CalculateDistance(empireToDefectTo.Capital.Xpos, empireToDefectTo.Capital.Ypos, shipGroup2.LeadShip.Xpos, shipGroup2.LeadShip.Ypos);
                        if (num2 < num)
                        {
                            shipGroup = shipGroup2;
                            num = num2;
                        }
                    }
                    if (shipGroup != null)
                    {
                        result = shipGroup;
                        empireToDefectTo.DefectFleet(shipGroup, empireToDefectTo);
                    }
                }
                LastDisasterDate = _Galaxy.CurrentStarDate;
            }
            return result;
        }

        public void DefectFleet(ShipGroup fleet, Empire newEmpire)
        {
            EmpireEvaluation empireEvaluation = fleet.Empire.ObtainEmpireEvaluation(newEmpire);
            empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - 25.0;
            string message = string.Format(TextResolver.GetText("Rogue Fleet Defects Detail"), fleet.Name, newEmpire.Name);
            string text = TextResolver.GetText("Rogue Fleet Defects!");
            fleet.Empire.SendEventMessageToEmpire(EventMessageType.RogueFleetDefectsFromUs, text, message, newEmpire, fleet.LeadShip);
            if (fleet.Empire != _Galaxy.PlayerEmpire)
            {
                string description = string.Format(TextResolver.GetText("Rogue Fleet Defects Warning"), fleet.Name);
                fleet.Empire.SendMessageToEmpire(newEmpire, EmpireMessageType.GeneralWarning, fleet, description);
            }
            fleet.SubsequentMissions.Clear();
            fleet.ForceCompleteMission();
            fleet.Empire.ShipGroups.Remove(fleet);
            for (int i = 0; i < fleet.Ships.Count; i++)
            {
                BuiltObject builtObject = fleet.Ships[i];
                newEmpire.TakeOwnershipOfBuiltObject(builtObject, newEmpire, setDesignAsObsolete: true, removeFromFleet: false);
            }
            if (!newEmpire.ShipGroups.Contains(fleet))
            {
                newEmpire.ShipGroups.Add(fleet);
                newEmpire.ShipGroups.Sort();
            }
            fleet.Empire = newEmpire;
            fleet.GatherPoint = newEmpire.SelectFleetBase(fleet);
        }

        private void PirateReviewRandomEvents()
        {
            if (Galaxy.Rnd.Next(0, 7) == 1)
            {
                RandomEventUncoverPirateAttackFunding();
                return;
            }
            int maxValue = Math.Max(5, 8 - Colonies.Count);
            if (Galaxy.Rnd.Next(0, maxValue) == 1)
            {
                RandomEventPirateControlledColonyGivesShip();
                return;
            }
            double num = CalculateRelativeEmpireSize();
            if (num < 1.0)
            {
                if (Galaxy.Rnd.Next(0, 7) == 1)
                {
                    RandomEventUncoverKnownLocations();
                }
                else if (Galaxy.Rnd.Next(0, 10) == 1)
                {
                    RandomEventRareResourceIntercepted();
                }
            }
        }

        private void ReviewRandomEvents()
        {
            if (Galaxy.Rnd.Next(0, 10) == 1)
            {
                RandomEventUncoverPirateAttackFunding();
                return;
            }
            if (Galaxy.Rnd.Next(0, 20) == 1)
            {
                RandomEventRestrictedZoneRevealed();
                return;
            }
            double num = (CalculateRelativeEmpireSize() + CalculateRelativeEmpireMilitaryStrength()) / 2.0;
            if (num < 1.0)
            {
                if (Galaxy.Rnd.Next(0, 10) == 1)
                {
                    RandomEventUncoverKnownLocations();
                }
                else if (Galaxy.Rnd.Next(0, 20) == 1)
                {
                    RandomEventRareResourceIntercepted();
                }
                else if (Galaxy.Rnd.Next(0, 20) == 1 && _Galaxy.GameDisasterEventsEnabled)
                {
                    EmpireEventColonyResourceAppearance();
                }
            }
        }

        private void RandomEventRestrictedZoneRevealed()
        {
        }

        private void EmpireEventPlague()
        {
            int index = Galaxy.Rnd.Next(0, Colonies.Count);
            Habitat colony = Colonies[index];
            EmpireEventPlague(colony);
        }

        public void EmpireEventPlague(Habitat colony)
        {
            if (colony == null || colony.Population == null || colony.Population.Count <= 0 || colony.HasBeenDestroyed)
            {
                return;
            }
            if (RaceEventType == RaceEventType.PredictiveHistory || (DominantRace.RaceEvents.ContainsEventType(RaceEventType.LuckyAvertColonyDisaster) && Galaxy.Rnd.Next(0, 2) == 1))
            {
                Habitat habitat = Galaxy.DetermineHabitatSystemStar(colony);
                string title = TextResolver.GetText("Avert Disaster") + "!";
                string message = string.Format(TextResolver.GetText("Avert Plague Description"), colony.Name, habitat.Name);
                SendEventMessageToEmpire(EventMessageType.RaceEvent, title, message, RaceEventType.LuckyAvertColonyDisaster, colony);
            }
            else
            {
                Plague plague = null;
                int num = 0;
                for (int i = 0; i < _Galaxy.Plagues.Count; i++)
                {
                    num += _Galaxy.Plagues[i].NaturalOccurrenceLevel;
                }
                if (num > 0)
                {
                    int num2 = Galaxy.Rnd.Next(0, num);
                    int num3 = 0;
                    for (int j = 0; j < _Galaxy.Plagues.Count; j++)
                    {
                        Plague plague2 = _Galaxy.Plagues[j];
                        if (plague2 != null)
                        {
                            int num4 = num3;
                            int num5 = num3 + plague2.NaturalOccurrenceLevel;
                            if (num2 >= num4 && num2 < num5)
                            {
                                plague = plague2;
                                break;
                            }
                            num3 += plague2.NaturalOccurrenceLevel;
                        }
                    }
                }
                if (plague != null)
                {
                    colony.PlagueId = plague.PlagueId;
                    colony.PlagueTimeRemaining = plague.Duration + (float)((Galaxy.Rnd.NextDouble() - 0.5) * ((double)plague.Duration * 0.3));
                    if (colony.Population != null)
                    {
                        for (int k = 0; k < colony.Population.Count; k++)
                        {
                            Population population = colony.Population[k];
                            if (population != null)
                            {
                                population.GrowthRate = 1f;
                            }
                        }
                    }
                    Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(colony);
                    string description = plague.Description;
                    string title2 = TextResolver.GetText("Colony Disaster Plague") + "!";
                    string text = string.Format(TextResolver.GetText("Colony Disaster Plague Description"), plague.Name, colony.Name, habitat2.Name, description);
                    text = text + "\n\n" + TextResolver.GetText("Plague Warn Spread") + "...";
                    SendEventMessageToEmpire(EventMessageType.DisasterEvent, title2, text, DisasterEventType.Plague, colony);
                    SendNewsBroadcast(EventMessageType.DisasterEvent, colony, DisasterEventType.Plague, warStartEnd: false, wonderBegun: false);
                }
            }
            LastDisasterDate = _Galaxy.CurrentStarDate;
        }

        private void EmpireEventColonyResourceDepletion()
        {
            int index = Galaxy.Rnd.Next(0, Colonies.Count);
            Habitat habitat = Colonies[index];
            if (habitat != null && habitat.Resources != null && habitat.Resources.Count > 0 && !habitat.HasBeenDestroyed)
            {
                int num = 0;
                while (habitat.DoingTasks && num < 100)
                {
                    Thread.Sleep(1);
                    num++;
                }
                if (!habitat.DoingTasks)
                {
                    int index2 = Galaxy.Rnd.Next(0, habitat.Resources.Count);
                    Resource resource = new Resource(habitat.Resources[index2].ResourceID);
                    EmpireEventColonyResourceDepletion(habitat, resource, this, _Galaxy);
                }
            }
        }

        public static void EmpireEventColonyResourceDepletion(Habitat habitat, Resource resource, Empire empire, Galaxy galaxy)
        {
            if (habitat == null || habitat.Resources == null || habitat.Resources.Count <= 0 || habitat.HasBeenDestroyed)
            {
                return;
            }
            if (!habitat.DoingTasks)
            {
                int num = habitat.Resources.IndexOf(resource.ResourceID, 0);
                if (num >= 0)
                {
                    habitat.Resources.RemoveAt(num);
                    Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                    string title = string.Format(TextResolver.GetText("Resource Depletion"), resource.Name) + "!";
                    string message = string.Format(TextResolver.GetText("Resource Depletion Description"), resource.Name, habitat.Name, habitat2.Name);
                    empire?.SendEventMessageToEmpire(EventMessageType.ResourceDepletion, title, message, resource, habitat);
                }
            }
            if (empire != null)
            {
                empire.LastDisasterDate = galaxy.CurrentStarDate;
            }
        }

        private void EmpireEventColonyResourceAppearance()
        {
            if (Colonies.Count <= 0)
            {
                return;
            }
            int num = Galaxy.Rnd.Next(0, Colonies.Count);
            if (num < 0 || num >= Colonies.Count)
            {
                return;
            }
            Habitat habitat = Colonies[num];
            if (habitat == null || habitat.Resources == null || habitat.Resources.Count >= 5 || habitat.HasBeenDestroyed)
            {
                return;
            }
            int num2 = 0;
            while (habitat.DoingTasks && num2 < 100)
            {
                Thread.Sleep(1);
                num2++;
            }
            if (!habitat.DoingTasks)
            {
                List<byte> list = Galaxy.ResolveValidResourcesForHabitatTypeExcludeManufactured(habitat.Type, allowSuperLuxuryResources: false);
                num2 = 0;
                int index = Galaxy.Rnd.Next(0, list.Count);
                int num3 = habitat.Resources.IndexOf(list[index], 0);
                while (num3 >= 0 && num2 < 20)
                {
                    index = Galaxy.Rnd.Next(0, list.Count);
                    num3 = habitat.Resources.IndexOf(list[index], 0);
                    num2++;
                }
                if (num3 < 0)
                {
                    Resource resource = new Resource(list[index]);
                    EmpireEventColonyResourceAppearance(habitat, resource, this);
                }
            }
        }

        public static void EmpireEventColonyResourceAppearance(Habitat habitat, Resource resource, Empire empire)
        {
            if (resource == null || habitat == null)
            {
                return;
            }
            if (habitat.Resources == null)
            {
                habitat.Resources = new HabitatResourceList();
            }
            if (habitat.Resources == null || habitat.Resources.Count >= 5 || habitat.HasBeenDestroyed)
            {
                return;
            }
            int num = 0;
            while (habitat.DoingTasks && num < 100)
            {
                Thread.Sleep(1);
                num++;
            }
            if (!habitat.DoingTasks)
            {
                int num2 = habitat.Resources.IndexOf(resource.ResourceID, 0);
                if (num2 < 0)
                {
                    habitat.Resources.Add(new HabitatResource(resource.ResourceID, Galaxy.Rnd.Next(300, 700)));
                    Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                    string title = string.Format(TextResolver.GetText("Resource Appearance"), resource.Name) + "!";
                    string message = string.Format(TextResolver.GetText("Resource Appearance Description"), resource.Name, habitat.Name, habitat2.Name);
                    empire?.SendEventMessageToEmpire(EventMessageType.ResourceAppearance, title, message, resource, habitat);
                }
            }
        }

        private void EmpireEventEconomicCrisis()
        {
            double num = StateMoney * (0.4 + Galaxy.Rnd.NextDouble() * 0.2);
            StateMoney -= num;
            string title = TextResolver.GetText("Empire Disaster Economic Crisis") + "!";
            string message = string.Format(TextResolver.GetText("Empire Disaster Economic Crisis Description"), num.ToString("###,###,###,##0"));
            SendEventMessageToEmpire(EventMessageType.DisasterEvent, title, message, DisasterEventType.EconomicCrisis, null);
            SendNewsBroadcast(EventMessageType.DisasterEvent, null, DisasterEventType.EconomicCrisis, warStartEnd: false, wonderBegun: false);
            LastDisasterDate = _Galaxy.CurrentStarDate;
        }

        private void EmpireEventColonyNaturalDisaster()
        {
            int index = Galaxy.Rnd.Next(0, Colonies.Count);
            Habitat habitat = Colonies[index];
            if (habitat != null && !habitat.HasBeenDestroyed)
            {
                if (RaceEventType == RaceEventType.PredictiveHistory || (DominantRace.RaceEvents.ContainsEventType(RaceEventType.LuckyAvertColonyDisaster) && Galaxy.Rnd.Next(0, 2) == 1))
                {
                    //DisasterEventType disasterEventType = DisasterEventType.Undefined;
                    Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                    string arg = Galaxy.ResolveDescription(habitat.Type switch
                    {
                        HabitatType.Continental => DisasterEventType.Earthquake,
                        HabitatType.MarshySwamp => DisasterEventType.Sinkhole,
                        HabitatType.Ocean => DisasterEventType.Tsunami,
                        HabitatType.Desert => DisasterEventType.Sandstorm,
                        HabitatType.Ice => DisasterEventType.Blizzard,
                        HabitatType.Volcanic => DisasterEventType.Eruption,
                        _ => DisasterEventType.Earthquake,
                    });
                    string title = TextResolver.GetText("Avert Disaster") + "!";
                    string message = string.Format(TextResolver.GetText("Avert Disaster Description"), arg, habitat.Name, habitat2.Name);
                    SendEventMessageToEmpire(EventMessageType.RaceEvent, title, message, RaceEventType.LuckyAvertColonyDisaster, habitat);
                }
                else
                {
                    EmpireEventColonyNaturalDisaster(habitat);
                }
                LastDisasterDate = _Galaxy.CurrentStarDate;
            }
        }

        public void EmpireEventColonyNaturalDisaster(Habitat colony)
        {
            float num = (float)(0.2 + Galaxy.Rnd.NextDouble() * 0.1);
            colony.Damage += num;
            colony.Damage = Math.Min(1f, colony.Damage);
            colony.RecalculateQuality();
            int num2 = Galaxy.Rnd.Next(12, 21);
            int developmentLevel = Math.Max(0, colony.GetDevelopmentLevel() - num2);
            colony.SetDevelopmentLevel(developmentLevel);
            _Galaxy.DoCharacterEvent(CharacterEventType.ColonyDevelopmentDecrease, colony, colony.Characters, includeLeader: true, colony.Empire);
            if (colony.Population != null && colony.Population.Count > 0 && colony.Population.TotalAmount > 0)
            {
                long num3 = (long)((double)colony.Population[0].Amount * (0.15 + Galaxy.Rnd.NextDouble() * 0.08));
                colony.Population[0].Amount -= num3;
                colony.Population.RecalculateTotalAmount();
            }
            string empty = string.Empty;
            string empty2 = string.Empty;
            DisasterEventType disasterEventType = DisasterEventType.Undefined;
            Habitat habitat = Galaxy.DetermineHabitatSystemStar(colony);
            switch (colony.Type)
            {
                case HabitatType.Continental:
                    disasterEventType = DisasterEventType.Earthquake;
                    empty = TextResolver.GetText("Colony Disaster Earthquake") + "!";
                    empty2 = string.Format(TextResolver.GetText("Colony Disaster Earthquake Description"), colony.Name, habitat.Name);
                    break;
                case HabitatType.MarshySwamp:
                    disasterEventType = DisasterEventType.Sinkhole;
                    empty = TextResolver.GetText("Colony Disaster Sinkhole") + "!";
                    empty2 = string.Format(TextResolver.GetText("Colony Disaster Sinkhole Description"), colony.Name, habitat.Name);
                    break;
                case HabitatType.Ocean:
                    disasterEventType = DisasterEventType.Tsunami;
                    empty = TextResolver.GetText("Colony Disaster Tsunami") + "!";
                    empty2 = string.Format(TextResolver.GetText("Colony Disaster Tsunami Description"), colony.Name, habitat.Name);
                    break;
                case HabitatType.Desert:
                    disasterEventType = DisasterEventType.Sandstorm;
                    empty = TextResolver.GetText("Colony Disaster Sandstorm") + "!";
                    empty2 = string.Format(TextResolver.GetText("Colony Disaster Sandstorm Description"), colony.Name, habitat.Name);
                    break;
                case HabitatType.Ice:
                    disasterEventType = DisasterEventType.Blizzard;
                    empty = TextResolver.GetText("Colony Disaster Blizzard") + "!";
                    empty2 = string.Format(TextResolver.GetText("Colony Disaster Blizzard Description"), colony.Name, habitat.Name);
                    break;
                case HabitatType.Volcanic:
                    disasterEventType = DisasterEventType.Eruption;
                    empty = TextResolver.GetText("Colony Disaster Eruption") + "!";
                    empty2 = string.Format(TextResolver.GetText("Colony Disaster Eruption Description"), colony.Name, habitat.Name);
                    break;
                default:
                    disasterEventType = DisasterEventType.Earthquake;
                    empty = TextResolver.GetText("Colony Disaster Earthquake") + "!";
                    empty2 = string.Format(TextResolver.GetText("Colony Disaster Earthquake Description"), colony.Name, habitat.Name);
                    break;
            }
            SendEventMessageToEmpire(EventMessageType.DisasterEvent, empty, empty2, disasterEventType, colony);
            SendNewsBroadcast(EventMessageType.DisasterEvent, colony, disasterEventType, warStartEnd: false, wonderBegun: false);
            LastDisasterDate = _Galaxy.CurrentStarDate;
        }

        private void ResetRaceEvents()
        {
            if (RaceEventEndDate <= 0 || _Galaxy.CurrentStarDate <= RaceEventEndDate)
            {
                return;
            }
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat == null || habitat.HasBeenDestroyed)
                {
                    continue;
                }
                if (habitat.RaceEventType != 0)
                {
                    switch (habitat.RaceEventType)
                    {
                        case RaceEventType.AntiXenoRiotsExterminate:
                            habitat.ColonyPopulationPolicy = Policy.NewColonyPopulationPolicyAllRaces;
                            habitat.ColonyPopulationPolicyRaceFamily = Policy.NewColonyPopulationPolicyYourRaceFamily;
                            break;
                        case RaceEventType.DeathCultExterminate:
                            habitat.ColonyPopulationPolicy = Policy.NewColonyPopulationPolicyAllRaces;
                            habitat.ColonyPopulationPolicyRaceFamily = Policy.NewColonyPopulationPolicyYourRaceFamily;
                            break;
                        case RaceEventType.XenophobiaNoAssimilate:
                            habitat.ColonyPopulationPolicy = Policy.NewColonyPopulationPolicyAllRaces;
                            habitat.ColonyPopulationPolicyRaceFamily = Policy.NewColonyPopulationPolicyYourRaceFamily;
                            break;
                    }
                }
                habitat.RaceEventType = RaceEventType.Undefined;
            }
            RaceEventType = RaceEventType.Undefined;
            RaceEventEndDate = 0L;
        }

        private void DoRaceEvent(Race race)
        {
            if (race == null || race.RaceEvents == null || race.RaceEvents.Count <= 0)
            {
                return;
            }
            int index = Galaxy.Rnd.Next(0, race.RaceEvents.Count);
            RaceEvent raceEvent = race.RaceEvents[index];
            if (raceEvent != null)
            {
                double num = Galaxy.Rnd.NextDouble();
                num *= 20.0;
                if (num < raceEvent.Frequency)
                {
                    InitiateRaceEvent(race, raceEvent);
                }
            }
        }

        private void InitiateRaceEvent(Race race, RaceEvent raceEvent)
        {
            if (race == null || raceEvent == null)
            {
                return;
            }
            string title = string.Empty;
            string text = string.Empty;
            StellarObject stellarObject = null;
            HabitatList habitatList = null;
            Habitat habitat = null;
            int num = 0;
            ResearchNode researchNode = null;
            long currentStarDate = _Galaxy.CurrentStarDate;
            long raceEventEndDate = currentStarDate + Galaxy.RealSecondsInGalacticYear * 1000;
            long raceEventEndDate2 = currentStarDate + Galaxy.RealSecondsInGalacticYear * 1000 / 12;
            switch (raceEvent.Type)
            {
                case RaceEventType.AntiXenoRiotsExterminate:
                    if (Colonies != null)
                    {
                        habitat = Colonies[Galaxy.Rnd.Next(0, Colonies.Count)];
                        if (habitat != null)
                        {
                            Habitat habitat4 = Galaxy.DetermineHabitatSystemStar(habitat);
                            habitat.ColonyPopulationPolicy = ColonyPopulationPolicy.Exterminate;
                            habitat.ColonyPopulationPolicyRaceFamily = ColonyPopulationPolicy.Enslave;
                            habitat.RaceEventType = RaceEventType.AntiXenoRiotsExterminate;
                            RaceEventEndDate = raceEventEndDate2;
                            stellarObject = habitat;
                            title = Galaxy.ResolveDescription(raceEvent.Type);
                            text = string.Format(TextResolver.GetText("Race Event Description AntiXenoRiotsExterminate"), habitat.Name, habitat4.Name);
                        }
                    }
                    break;
                case RaceEventType.CannibalismPopulationShrinks:
                    if (race.ChangePeriodActive && Colonies != null)
                    {
                        Habitat habitat2 = Colonies[Galaxy.Rnd.Next(0, Colonies.Count)];
                        if (habitat2 != null && habitat2.Population != null && habitat2.Population.Count > 0)
                        {
                            long amount = habitat2.Population[0].Amount;
                            int maxValue = (int)Math.Min(20000000L, habitat2.Population[0].Amount / 50);
                            amount = amount / 50 + Galaxy.Rnd.Next(0, maxValue);
                            habitat2.Population[0].Amount -= amount;
                            habitat2.Population.RecalculateTotalAmount();
                            stellarObject = habitat2;
                            title = Galaxy.ResolveDescription(raceEvent.Type);
                            text = string.Format(TextResolver.GetText("Race Event Description CannibalismPopulationShrinks"), habitat2.Name);
                        }
                    }
                    break;
                case RaceEventType.CreativeReengineeringFreeCrashResearch:
                    if (Research != null && Research.ResearchQueueHighTech != null && Research.ResearchQueueHighTech.Count > 0)
                    {
                        researchNode = Research.ResearchQueueHighTech[0];
                        if (researchNode != null && !researchNode.IsRushing && researchNode.Progress < researchNode.Cost * 0.6f)
                        {
                            researchNode.IsRushing = true;
                            title = Galaxy.ResolveDescription(raceEvent.Type);
                            text = string.Format(TextResolver.GetText("Race Event Description CreativeReengineeringFreeCrashResearch"), researchNode.Name);
                        }
                    }
                    break;
                case RaceEventType.DeathCultExterminate:
                    if (Colonies != null)
                    {
                        habitat = Colonies.GetRandomHabitatPopulationAnotherRace(_Galaxy, DominantRace);
                        if (habitat != null && habitat.ColonyPopulationPolicy != ColonyPopulationPolicy.Exterminate && habitat.ColonyPopulationPolicyRaceFamily != ColonyPopulationPolicy.Exterminate)
                        {
                            habitat.ColonyPopulationPolicyRaceFamily = ColonyPopulationPolicy.Exterminate;
                            habitat.ColonyPopulationPolicy = ColonyPopulationPolicy.Exterminate;
                            habitat.RaceEventType = RaceEventType.DeathCultExterminate;
                            RaceEventType = RaceEventType.DeathCultExterminate;
                            RaceEventEndDate = raceEventEndDate;
                            stellarObject = habitat;
                            title = Galaxy.ResolveDescription(raceEvent.Type);
                            text = string.Format(TextResolver.GetText("Race Event Description DeathCultExterminate"), habitat.Name, DominantRace.Name);
                        }
                    }
                    break;
                case RaceEventType.DestinyCharacterTraits:
                    {
                        if (Characters == null || Characters.Count <= 0)
                        {
                            break;
                        }
                        Character character5 = Characters[Galaxy.Rnd.Next(0, Characters.Count)];
                        if (character5 == null || !character5.Active || !character5.BonusesKnown)
                        {
                            break;
                        }
                        CharacterTraitType characterTraitType3 = CharacterTraitType.Undefined;
                        List<CharacterTraitType> list5 = Character.DetermineValidTraitsForRole(character5.Role, includeStartingTraits: false, includeHighlyNegativeTraits: false);
                        if (list5.Count > 0)
                        {
                            int num6 = 0;
                            while (characterTraitType3 == CharacterTraitType.Undefined && num6 < 20)
                            {
                                characterTraitType3 = list5[Galaxy.Rnd.Next(0, list5.Count)];
                                if (!character5.CheckNewTraitValid(characterTraitType3))
                                {
                                    characterTraitType3 = CharacterTraitType.Undefined;
                                }
                                num6++;
                            }
                        }
                        if (characterTraitType3 != 0 && character5.AddTrait(characterTraitType3, starting: false, _Galaxy))
                        {
                            stellarObject = character5.Location;
                            RaceEventData = character5;
                            title = Galaxy.ResolveDescription(raceEvent.Type);
                            text = string.Format(TextResolver.GetText("Race Event Description DestinyCharacterTraits"), Galaxy.ResolveDescription(character5.Role).ToLower(CultureInfo.InvariantCulture), character5.Name, Galaxy.ResolveDescription(characterTraitType3));
                        }
                        break;
                    }
                case RaceEventType.ForcedRetirementLeaderReplaced:
                    {
                        if (Characters == null)
                        {
                            break;
                        }
                        CharacterList charactersByRole2 = Characters.GetCharactersByRole(CharacterRole.Leader);
                        if (charactersByRole2.Count > 0 && _Galaxy.CurrentStarDate > NextAllowableLeaderChangeDate)
                        {
                            Character character6 = GenerateNewCharacter(CharacterRole.Leader, Capital);
                            string text3 = string.Empty;
                            if (charactersByRole2[0].Location != null)
                            {
                                text3 = charactersByRole2[0].Location.Name;
                            }
                            title = Galaxy.ResolveDescription(raceEvent.Type);
                            RaceEventData = charactersByRole2[0];
                            text = string.Format(TextResolver.GetText("Race Event Description ForcedRetirementLeaderReplaced"), Galaxy.ResolveDescription(charactersByRole2[0].Role).ToLower(CultureInfo.InvariantCulture), charactersByRole2[0].Name, text3, character6.Name);
                            charactersByRole2[0].Kill(_Galaxy);
                            stellarObject = Capital;
                            Leader = character6;
                            LastLeaderChangeDate = _Galaxy.CurrentStarDate;
                            break;
                        }
                        CharacterList charactersByRole3 = Characters.GetCharactersByRole(CharacterRole.ColonyGovernor);
                        if (charactersByRole3.Count > 0)
                        {
                            stellarObject = charactersByRole3[0].Location;
                            string text4 = string.Empty;
                            if (stellarObject != null)
                            {
                                text4 = stellarObject.Name;
                            }
                            title = Galaxy.ResolveDescription(raceEvent.Type);
                            Character character7 = GenerateNewCharacter(CharacterRole.ColonyGovernor, stellarObject);
                            text = string.Format(TextResolver.GetText("Race Event Description ForcedRetirementLeaderReplaced"), Galaxy.ResolveDescription(charactersByRole3[0].Role).ToLower(CultureInfo.InvariantCulture), charactersByRole3[0].Name, text4, character7.Name);
                            charactersByRole3[0].Kill(_Galaxy);
                        }
                        break;
                    }
                case RaceEventType.FriendsInManyPlacesRevealTerritory:
                    {
                        if (ResortBases == null || ResortBases.Count <= 0)
                        {
                            break;
                        }
                        bool flag = false;
                        for (int m = 0; m < ResortBases.Count; m++)
                        {
                            if (Galaxy.Rnd.Next(0, 3) == 1)
                            {
                                switch (Galaxy.Rnd.Next(0, 3))
                                {
                                    case 0:
                                    case 1:
                                        {
                                            EmpireList empireList2 = DetermineEmpiresKnown();
                                            if (empireList2.Count > 0)
                                            {
                                                num = Galaxy.Rnd.Next(0, empireList2.Count);
                                                BuiltObject builtObject3 = null;
                                                if (empireList2[num].Capital != null)
                                                {
                                                    builtObject3 = FindNearestResortBase(empireList2[num].Capital.Xpos, empireList2[num].Capital.Ypos);
                                                }
                                                _Galaxy.GiveTerritoryMap(empireList2[num], this);
                                                stellarObject = builtObject3;
                                                string arg4 = string.Empty;
                                                if (builtObject3 != null)
                                                {
                                                    arg4 = builtObject3.Name;
                                                }
                                                flag = true;
                                                title = Galaxy.ResolveDescription(raceEvent.Type);
                                                text = string.Format(TextResolver.GetText("Race Event Description FriendsInManyPlacesRevealTerritory"), arg4, empireList2[num].Name);
                                            }
                                            break;
                                        }
                                    case 2:
                                        {
                                            EmpireList empireList = DetermineEmpiresNotKnown();
                                            if (empireList.Count <= 0)
                                            {
                                                break;
                                            }
                                            num = Galaxy.Rnd.Next(0, empireList.Count);
                                            Empire empire = empireList[num];
                                            if (empire != null && empire.Active)
                                            {
                                                BuiltObject builtObject2 = null;
                                                if (empire.Capital != null)
                                                {
                                                    builtObject2 = FindNearestResortBase(empire.Capital.Xpos, empire.Capital.Ypos);
                                                }
                                                _Galaxy.DoEmpireEncounter(this, empire, builtObject2);
                                                stellarObject = builtObject2;
                                                string arg3 = string.Empty;
                                                if (builtObject2 != null)
                                                {
                                                    arg3 = builtObject2.Name;
                                                }
                                                flag = true;
                                                title = Galaxy.ResolveDescription(raceEvent.Type);
                                                text = string.Format(TextResolver.GetText("Race Event Description FriendsInManyPlacesNewEmpire"), arg3, empire.Name);
                                            }
                                            break;
                                        }
                                }
                            }
                            if (flag)
                            {
                                break;
                            }
                        }
                        break;
                    }
                case RaceEventType.GrandPerformanceDiplomacyBonus:
                    {
                        EmpireList empireList3 = DetermineEmpiresNotAtWarWithNoAmbassador();
                        if (empireList3 == null || empireList3.Count <= 0)
                        {
                            break;
                        }
                        num = Galaxy.Rnd.Next(0, empireList3.Count);
                        Empire empire2 = empireList3[num];
                        if (empire2 != null)
                        {
                            EmpireEvaluation empireEvaluation2 = ObtainEmpireEvaluation(empire2);
                            if (empireEvaluation2 != null)
                            {
                                empireEvaluation2.DiplomacyFactor *= 1.1;
                                RaceEventType = RaceEventType.GrandPerformanceDiplomacyBonus;
                                RaceEventEndDate = raceEventEndDate;
                                title = Galaxy.ResolveDescription(raceEvent.Type);
                                text = string.Format(TextResolver.GetText("Race Event Description GrandPerformanceDiplomacyBonus"), race.Name, empire2.Name);
                            }
                        }
                        break;
                    }
                case RaceEventType.GreatHuntStrongTroops:
                    if (Capital != null)
                    {
                        Capital.RaceEventType = RaceEventType.GreatHuntStrongTroops;
                        RaceEventEndDate = raceEventEndDate;
                        stellarObject = Capital;
                        title = Galaxy.ResolveDescription(raceEvent.Type);
                        text = string.Format(TextResolver.GetText("Race Event Description GreatHuntStrongTroops"), Capital.Name);
                    }
                    break;
                case RaceEventType.HistoricalKnowledgeUncoverHiddenLocation:
                    {
                        GalaxyLocationList galaxyLocationList = _Galaxy.GalaxyLocations.FindLocations(GalaxyLocationType.RestrictedArea);
                        if (galaxyLocationList == null || galaxyLocationList.Count <= 0 || KnownGalaxyLocations == null)
                        {
                            break;
                        }
                        int num3 = 0;
                        GalaxyLocation galaxyLocation = null;
                        while (galaxyLocation == null && num3 < 50)
                        {
                            num = Galaxy.Rnd.Next(0, galaxyLocationList.Count);
                            if (!KnownGalaxyLocations.Contains(galaxyLocationList[num]))
                            {
                                galaxyLocation = galaxyLocationList[num];
                            }
                            num3++;
                        }
                        if (galaxyLocation != null)
                        {
                            KnownGalaxyLocations.Add(galaxyLocation);
                            if (galaxyLocation.RelatedBuiltObject != null)
                            {
                                stellarObject = galaxyLocation.RelatedBuiltObject;
                            }
                            string arg = Galaxy.ResolveSectorDescription(galaxyLocation.Xpos, galaxyLocation.Ypos);
                            string arg2 = galaxyLocation.Xpos.ToString("0,K") + ", " + galaxyLocation.Ypos.ToString("0,K");
                            title = Galaxy.ResolveDescription(raceEvent.Type);
                            text = string.Format(TextResolver.GetText("Race Event Description HistoricalKnowledgeUncoverHiddenLocation"), galaxyLocation.Name, arg, arg2);
                        }
                        break;
                    }
                case RaceEventType.IsolationistsResetFirstContactPenalty:
                    if (EmpireEvaluations != null)
                    {
                        for (int j = 0; j < EmpireEvaluations.Count; j++)
                        {
                            EmpireEvaluation empireEvaluation = EmpireEvaluations[j];
                            if (empireEvaluation != null)
                            {
                                DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empireEvaluation.Empire);
                                if (diplomaticRelation != null && diplomaticRelation.Type != 0)
                                {
                                    empireEvaluation.FirstContactPenalty = EmpireEvaluation.FirstContactPenaltyStartAmount * _Galaxy.AggressionLevel;
                                }
                            }
                        }
                    }
                    title = Galaxy.ResolveDescription(raceEvent.Type);
                    text = TextResolver.GetText("Race Event Description IsolationistsResetFirstContactPenalty");
                    break;
                case RaceEventType.MetamorphosisCharacterChange:
                    {
                        if (!race.ChangePeriodActive || Characters == null || Characters.Count <= 0)
                        {
                            break;
                        }
                        Character character3 = Characters[Galaxy.Rnd.Next(0, Characters.Count)];
                        if (character3 == null || !character3.Active)
                        {
                            break;
                        }
                        stellarObject = character3.Location;
                        string text2 = string.Empty;
                        if (character3.Location != null)
                        {
                            text2 = character3.Location.Name;
                        }
                        title = Galaxy.ResolveDescription(raceEvent.Type);
                        if (character3.Traits != null && character3.Traits.Count > 0 && Galaxy.Rnd.Next(0, 2) == 1)
                        {
                            CharacterTraitType characterTraitType = character3.Traits[Galaxy.Rnd.Next(0, character3.Traits.Count)];
                            if (character3.RemoveTrait(characterTraitType))
                            {
                                text = string.Format(TextResolver.GetText("Race Event Description MetamorphosisCharacterChange LoseTrait"), character3.Name, Galaxy.ResolveDescription(character3.Role), text2, Galaxy.ResolveDescription(characterTraitType));
                            }
                            break;
                        }
                        CharacterTraitType characterTraitType2 = CharacterTraitType.Undefined;
                        List<CharacterTraitType> list2 = Character.DetermineValidTraitsForRole(character3.Role, includeStartingTraits: false, includeHighlyNegativeTraits: false);
                        if (list2.Count > 0)
                        {
                            int num4 = 0;
                            while (characterTraitType2 == CharacterTraitType.Undefined && num4 < 20)
                            {
                                characterTraitType2 = list2[Galaxy.Rnd.Next(0, list2.Count)];
                                if (!character3.CheckNewTraitValid(characterTraitType2))
                                {
                                    characterTraitType2 = CharacterTraitType.Undefined;
                                }
                                num4++;
                            }
                        }
                        if (characterTraitType2 != 0 && character3.AddTrait(characterTraitType2, starting: false, _Galaxy))
                        {
                            text = string.Format(TextResolver.GetText("Race Event Description MetamorphosisCharacterChange NewTrait"), character3.Name, Galaxy.ResolveDescription(character3.Role), text2, Galaxy.ResolveDescription(characterTraitType2));
                        }
                        break;
                    }
                case RaceEventType.NaturalHarmonyColonyQualityIncreased:
                    if (Colonies != null)
                    {
                        habitat = Colonies.GetRandomHabitatPopulationBelowThreshold(_Galaxy, long.MaxValue, HabitatType.Continental);
                        if (habitat != null && habitat.BaseQuality < 1f)
                        {
                            Habitat habitat3 = Galaxy.DetermineHabitatSystemStar(habitat);
                            habitat.BaseQuality += 0.01f;
                            stellarObject = habitat;
                            title = Galaxy.ResolveDescription(raceEvent.Type);
                            text = string.Format(TextResolver.GetText("Race Event Description NaturalHarmonyColonyQualityIncreased"), Galaxy.ResolveDescription(habitat.Type).ToLower(CultureInfo.InvariantCulture), habitat.Name, habitat3.Name);
                        }
                    }
                    break;
                case RaceEventType.NepthysWineVintage:
                    {
                        Resource resource = new Resource(_Galaxy.ResourceSystem.Resources.GetByName("Nepthys Wine").ResourceID);
                        if (resource != null && Colonies != null)
                        {
                            habitatList = Colonies.GetHabitatsWithResource(resource.ResourceID);
                            for (int n = 0; n < habitatList.Count; n++)
                            {
                                habitatList[n].RaceEventType = RaceEventType.NepthysWineVintage;
                            }
                            RaceEventEndDate = raceEventEndDate;
                            title = Galaxy.ResolveDescription(raceEvent.Type);
                            text = TextResolver.GetText("Race Event Description NepthysWineVintage");
                        }
                        break;
                    }
                case RaceEventType.PredictiveHistory:
                    RaceEventType = RaceEventType.PredictiveHistory;
                    RaceEventEndDate = raceEventEndDate;
                    title = Galaxy.ResolveDescription(raceEvent.Type);
                    text = TextResolver.GetText("Race Event Description PredictiveHistory");
                    break;
                case RaceEventType.ScientificBreakthroughResearchProgress:
                    if (Research == null || Research.ResearchQueueEnergy == null || Research.ResearchQueueHighTech == null || Research.ResearchQueueWeapons == null)
                    {
                        break;
                    }
                    switch (Galaxy.Rnd.Next(0, 3))
                    {
                        case 0:
                            if (Research.ResearchQueueWeapons.Count > 0)
                            {
                                researchNode = Research.ResearchQueueWeapons[0];
                            }
                            break;
                        case 1:
                            if (Research.ResearchQueueEnergy.Count > 0)
                            {
                                researchNode = Research.ResearchQueueEnergy[0];
                            }
                            break;
                        case 2:
                            if (Research.ResearchQueueHighTech.Count > 0)
                            {
                                researchNode = Research.ResearchQueueHighTech[0];
                            }
                            break;
                    }
                    if (researchNode != null)
                    {
                        researchNode.Progress += researchNode.Cost * 0.1f;
                        if (researchNode.Progress >= researchNode.Cost)
                        {
                            researchNode.Progress = researchNode.Cost - 1f;
                        }
                        title = Galaxy.ResolveDescription(raceEvent.Type);
                        text = string.Format(TextResolver.GetText("Race Event Description ScientificBreakthroughResearchProgress"), researchNode.Name);
                    }
                    break;
                case RaceEventType.SecurityConcernsCharacterReplaced:
                    {
                        if (Characters == null || Characters.Count <= 0)
                        {
                            break;
                        }
                        CharacterList characterList = new CharacterList();
                        characterList.AddRange(Characters);
                        if (characterList.Count <= 0)
                        {
                            break;
                        }
                        List<CharacterTraitType> list = new List<CharacterTraitType>();
                        list.Add(CharacterTraitType.Drunk);
                        list.Add(CharacterTraitType.Addict);
                        list.Add(CharacterTraitType.Corrupt);
                        list.Add(CharacterTraitType.ForeignSpy);
                        list.Add(CharacterTraitType.DoubleAgent);
                        list.Add(CharacterTraitType.IntelligenceAddict);
                        list.Add(CharacterTraitType.IntelligenceCorrupt);
                        list.Add(CharacterTraitType.Lazy);
                        List<CharacterTraitType> traits = list;
                        CharacterList charactersWithTraits = characterList.GetCharactersWithTraits(traits);
                        Character character = null;
                        character = ((charactersWithTraits.Count <= 0) ? characterList[Galaxy.Rnd.Next(0, characterList.Count)] : charactersWithTraits[Galaxy.Rnd.Next(0, charactersWithTraits.Count)]);
                        if (character != null && character.Active && (character.Role != CharacterRole.Leader || _Galaxy.CurrentStarDate > NextAllowableLeaderChangeDate))
                        {
                            Character character2 = GenerateNewCharacter(CharacterRole.IntelligenceAgent, Capital);
                            stellarObject = Capital;
                            RaceEventData = character;
                            title = Galaxy.ResolveDescription(raceEvent.Type);
                            text = string.Format(TextResolver.GetText("Race Event Description SecurityConcernsCharacterReplaced"), Galaxy.ResolveDescription(character.Role).ToLower(CultureInfo.InvariantCulture), character.Name, character2.Name);
                            if (character.Role == CharacterRole.Leader)
                            {
                                LastLeaderChangeDate = _Galaxy.CurrentStarDate;
                            }
                            character.Kill(_Galaxy);
                        }
                        break;
                    }
                case RaceEventType.ShakturiArtifactWeaponResearch:
                    if (Research != null)
                    {
                        ResearchNodeList researchNodeList = Research.ResolveNextProjects(_Galaxy, DominantRace, IndustryType.Weapon);
                        if (researchNodeList != null && researchNodeList.Count > 0)
                        {
                            num = Galaxy.Rnd.Next(0, researchNodeList.Count);
                            researchNodeList[num].Progress += (researchNodeList[num].Cost - researchNodeList[num].Progress) / 2f;
                            title = Galaxy.ResolveDescription(raceEvent.Type);
                            text = string.Format(TextResolver.GetText("Race Event Description ShakturiArtifactWeaponResearch"), researchNodeList[num].Name);
                        }
                    }
                    break;
                case RaceEventType.SuppressedKnowledgeLoseResearch:
                    if (Research == null || Research.ResearchQueueEnergy == null || Research.ResearchQueueHighTech == null || Research.ResearchQueueWeapons == null)
                    {
                        break;
                    }
                    switch (Galaxy.Rnd.Next(0, 3))
                    {
                        case 0:
                            if (Research.ResearchQueueWeapons.Count > 0)
                            {
                                researchNode = Research.ResearchQueueWeapons[0];
                            }
                            break;
                        case 1:
                            if (Research.ResearchQueueEnergy.Count > 0)
                            {
                                researchNode = Research.ResearchQueueEnergy[0];
                            }
                            break;
                        case 2:
                            if (Research.ResearchQueueHighTech.Count > 0)
                            {
                                researchNode = Research.ResearchQueueHighTech[0];
                            }
                            break;
                    }
                    if (researchNode != null)
                    {
                        researchNode.Progress /= 2f;
                        title = Galaxy.ResolveDescription(raceEvent.Type);
                        text = string.Format(TextResolver.GetText("Race Event Description SuppressedKnowledgeLoseResearch"), researchNode.Name);
                    }
                    break;
                case RaceEventType.SupremeWarriorNewGeneral:
                    {
                        if (Characters == null || Capital == null || Colonies == null)
                        {
                            break;
                        }
                        CharacterList charactersByRole = Characters.GetCharactersByRole(CharacterRole.TroopGeneral);
                        if (charactersByRole.Count < Colonies.Count / 3)
                        {
                            Character character4 = GenerateNewCharacter(CharacterRole.TroopGeneral, Capital);
                            List<CharacterTraitType> list3 = new List<CharacterTraitType>();
                            list3.Add(CharacterTraitType.InspiringPresence);
                            list3.Add(CharacterTraitType.GoodTactician);
                            list3.Add(CharacterTraitType.Energetic);
                            list3.Add(CharacterTraitType.StrongGroundAttacker);
                            list3.Add(CharacterTraitType.StrongGroundDefender);
                            list3.Add(CharacterTraitType.ToughDiscipline);
                            list3.Add(CharacterTraitType.GoodGroundLogistician);
                            list3.Add(CharacterTraitType.NaturalGroundLeader);
                            list3.Add(CharacterTraitType.GoodRecruiter);
                            List<CharacterTraitType> list4 = list3;
                            int num5 = Math.Min(3, list4.Count);
                            for (int l = 0; l < num5; l++)
                            {
                                CharacterTraitType trait = list4[Galaxy.Rnd.Next(0, list4.Count)];
                                character4.AddTrait(trait, starting: true, null);
                            }
                            stellarObject = character4.Location;
                            RaceEventData = character4;
                            title = Galaxy.ResolveDescription(raceEvent.Type);
                            text = string.Format(TextResolver.GetText("Race Event Description SupremeWarriorNewGeneral"), character4.Name);
                        }
                        break;
                    }
                case RaceEventType.SwarmsFullTroopTransport:
                    {
                        if (!race.ChangePeriodActive || Capital == null)
                        {
                            break;
                        }
                        BuiltObject builtObject = GenerateNewShip(BuiltObjectSubRole.TroopTransport, Capital);
                        if (builtObject == null)
                        {
                            break;
                        }
                        int num2 = builtObject.TroopCapacity / 100;
                        for (int k = 0; k < num2; k++)
                        {
                            Troop troop = Capital.GenerateNewTroop();
                            if (troop != null)
                            {
                                troop.Colony.Troops.Remove(troop);
                                troop.Readiness = 100f;
                                troop.BuiltObject = builtObject;
                                troop.BuiltObject.Troops.Add(troop);
                                if (!Troops.Contains(troop))
                                {
                                    Troops.Add(troop);
                                }
                            }
                        }
                        stellarObject = builtObject;
                        title = Galaxy.ResolveDescription(raceEvent.Type);
                        text = string.Format(TextResolver.GetText("Race Event Description SwarmsFullTroopTransport"), Capital.Name);
                        break;
                    }
                case RaceEventType.TodashGalacticChampionships:
                    RaceEventType = RaceEventType.TodashGalacticChampionships;
                    RaceEventEndDate = raceEventEndDate;
                    SetRaceEventForAllColonies(RaceEventType.TodashGalacticChampionships);
                    title = Galaxy.ResolveDescription(raceEvent.Type);
                    text = string.Format(TextResolver.GetText("Race Event Description TodashGalacticChampionships"), Capital.Name);
                    break;
                case RaceEventType.UnderwaterLeviathan:
                    if (Colonies != null)
                    {
                        habitat = Colonies.GetRandomHabitatPopulationBelowThreshold(_Galaxy, 100000000L, HabitatType.Ocean);
                        if (habitat != null && habitat.Population != null && habitat.Population.Count > 0)
                        {
                            long amount2 = habitat.Population[0].Amount;
                            amount2 = amount2 / 6 + Galaxy.Rnd.Next(0, (int)(amount2 / 6));
                            habitat.Population[0].Amount -= amount2;
                            habitat.Population.RecalculateTotalAmount();
                            habitat.Damage += 0.05f + (float)(Galaxy.Rnd.NextDouble() * 0.05);
                            stellarObject = habitat;
                            title = Galaxy.ResolveDescription(raceEvent.Type);
                            text = string.Format(TextResolver.GetText("Race Event Description UnderwaterLeviathan"), habitat.Name);
                        }
                    }
                    break;
                case RaceEventType.WarriorWaveTroopRecruitment:
                    SetRaceEventForAllColonies(RaceEventType.WarriorWaveTroopRecruitment);
                    RaceEventEndDate = raceEventEndDate;
                    title = Galaxy.ResolveDescription(raceEvent.Type);
                    text = TextResolver.GetText("Race Event Description WarriorWaveTroopRecruitment");
                    break;
                case RaceEventType.XenophobiaNoAssimilate:
                    {
                        if (Colonies == null)
                        {
                            break;
                        }
                        RaceEventType = RaceEventType.XenophobiaNoAssimilate;
                        RaceEventEndDate = raceEventEndDate;
                        for (int i = 0; i < Colonies.Count; i++)
                        {
                            Colonies[i].RaceEventType = RaceEventType.XenophobiaNoAssimilate;
                            if (Colonies[i].ColonyPopulationPolicy == ColonyPopulationPolicy.Assimilate)
                            {
                                Colonies[i].ColonyPopulationPolicy = ColonyPopulationPolicy.DoNotAccept;
                            }
                            if (Colonies[i].ColonyPopulationPolicyRaceFamily == ColonyPopulationPolicy.Assimilate)
                            {
                                Colonies[i].ColonyPopulationPolicyRaceFamily = ColonyPopulationPolicy.DoNotAccept;
                            }
                        }
                        title = Galaxy.ResolveDescription(raceEvent.Type);
                        text = TextResolver.GetText("Race Event Description XenophobiaNoAssimilate");
                        break;
                    }
            }
            if (!string.IsNullOrEmpty(text))
            {
                SendEventMessageToEmpire(EventMessageType.RaceEvent, title, text, raceEvent.Type, stellarObject);
            }
        }

        private void SetRaceEventForAllColonies(RaceEventType raceEventType)
        {
            if (Colonies != null)
            {
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Colonies[i].RaceEventType = raceEventType;
                }
            }
        }

        private void ReviewEmpireEvents()
        {
            if (DominantRace == _Galaxy.ShakturiActualRace)
            {
                return;
            }
            if (_Galaxy.GameRaceSpecificEventsEnabled && RaceEventEndDate < _Galaxy.CurrentStarDate)
            {
                DoRaceEvent(DominantRace);
            }
            double num = (CalculateRelativeEmpireSize() + CalculateRelativeEmpireMilitaryStrength()) / 2.0;
            long currentStarDate = _Galaxy.CurrentStarDate;
            long num2 = LastDisasterDate + Galaxy.RealSecondsInGalacticYear * 1000 * 4;
            if (num > 2.5 && Colonies.Count > 5 && currentStarDate > num2)
            {
                double colonyApprovalAverage = ColonyApprovalAverage;
                double num3 = 1.0;
                if (DominantRace != null)
                {
                    num3 = CalculateRacialReputationConcern(DominantRace);
                }
                double num4 = CivilityRating / 2.0 / num3;
                double num5 = num4 + colonyApprovalAverage;
                if (GovernmentAttributes != null)
                {
                    num5 += 5.0 * (GovernmentAttributes.Stability - 1.0);
                }
                if (num5 < -3.0 && Galaxy.Rnd.Next(0, 5) == 1)
                {
                    double splinterPortion = 0.2 + Galaxy.Rnd.NextDouble() * 0.25;
                    InitiateEmpireSplit(splinterPortion);
                    return;
                }
                if (num5 < 0.0 && Galaxy.Rnd.Next(0, 5) == 1)
                {
                    EmpireEventRogueFleetDefects();
                    return;
                }
            }
            if (num > 1.0 && Colonies.Count > 5 && _Galaxy.GameDisasterEventsEnabled && currentStarDate > num2)
            {
                if (Galaxy.Rnd.Next(0, 20) == 1)
                {
                    EmpireEventPlague();
                    return;
                }
                if (Galaxy.Rnd.Next(0, 15) == 1)
                {
                    EmpireEventColonyNaturalDisaster();
                    return;
                }
                if (Galaxy.Rnd.Next(0, 15) == 1)
                {
                    EmpireEventColonyResourceDepletion();
                    return;
                }
                if (StateMoney > 0.0)
                {
                    double num6 = StateMoney / _Galaxy.TotalStateMoneyInGalaxy * (double)_Galaxy.Empires.Count;
                    if (num6 > 4.0 && Galaxy.Rnd.Next(0, 15) == 1)
                    {
                        EmpireEventEconomicCrisis();
                        return;
                    }
                }
            }
            if (Galaxy.Rnd.Next(0, 20) == 1 && _Galaxy.GameDisasterEventsEnabled)
            {
                EmpireEventColonyResourceAppearance();
            }
        }

        private Empire SplinterEmpire(Empire sourceEmpire, double splinterPortion, out HabitatList coloniesLost)
        {
            Empire empire = null;
            coloniesLost = new HabitatList();
            if (_Galaxy.NextEmpireID < Galaxy.MaximumEmpireCount)
            {
                int num = Math.Max(1, (int)(splinterPortion * (double)sourceEmpire.Colonies.Count));
                double x = 0.0;
                double y = 0.0;
                double num2 = 0.0;
                int iterationCount = 0;
                while (Galaxy.ConditionCheckLimit(num2 < (double)Galaxy.SectorSize * 4.5, 50, ref iterationCount))
                {
                    _Galaxy.ObtainRandomGalaxyCoordinates(out x, out y);
                    num2 = _Galaxy.CalculateDistance(y, y, sourceEmpire.Capital.Xpos, sourceEmpire.Capital.Ypos);
                }
                Habitat habitat = _Galaxy.FastFindNearestColony((int)x, (int)y, sourceEmpire, 20000, sourceEmpire.Capital);
                if (habitat != null)
                {
                    EmpireSplitCount++;
                    Race dominantRace = sourceEmpire.DominantRace;
                    List<int> allowableGovernmentTypes = ResolveDefaultAllowableGovernmentTypes(dominantRace);
                    int governmentId = SelectSuitableGovernment(dominantRace, -1, allowableGovernmentTypes);
                    int developmentLevel = habitat.GetDevelopmentLevel();
                    EmpirePolicy policy = _Galaxy.LoadEmpirePolicy(dominantRace, isPirate: false);
                    empire = new Empire(_Galaxy, "", habitat, dominantRace, governmentId, 1.0, policy);
                    empire.TakeOwnershipOfColony(habitat, empire, destroyAllBuiltObjectsAndTroopsAtColony: false);
                    coloniesLost.Add(habitat);
                    habitat.SetDevelopmentLevel(developmentLevel);
                    if (num > 1)
                    {
                        double colonyApprovalAverage = ColonyApprovalAverage;
                        colonyApprovalAverage = ((!(colonyApprovalAverage < 0.0)) ? (colonyApprovalAverage * 2.0) : (colonyApprovalAverage / 2.0));
                        for (int i = 0; i < num - 1; i++)
                        {
                            Habitat habitat2 = _Galaxy.FastFindNearestColonyBelowApproval((int)habitat.Xpos, (int)habitat.Ypos, sourceEmpire, colonyApprovalAverage);
                            if (habitat2 != null)
                            {
                                empire.TakeOwnershipOfColony(habitat2, empire, destroyAllBuiltObjectsAndTroopsAtColony: false);
                                coloniesLost.Add(habitat2);
                            }
                        }
                    }
                    empire.RecalculateEmpirePopulation();
                    PopulationList populationList = new PopulationList();
                    for (int j = 0; j < coloniesLost.Count; j++)
                    {
                        for (int k = 0; k < coloniesLost[j].Population.Count; k++)
                        {
                            populationList.Add(coloniesLost[j].Population[k]);
                        }
                    }
                    populationList.RecalculateTotalAmount();
                    if (populationList.DominantRace != dominantRace)
                    {
                        Race dominantRace2 = populationList.DominantRace;
                        allowableGovernmentTypes = ResolveDefaultAllowableGovernmentTypes(dominantRace2);
                        int governmentId2 = SelectSuitableGovernment(dominantRace2, -1, allowableGovernmentTypes);
                        empire.DominantRace = dominantRace2;
                        empire.ChangeGovernment(governmentId2);
                        empire.Name = GenerateEmpireName(governmentId2);
                    }
                    empire.GenerateDesignSpecifications(_Galaxy, empire.DominantRace, isPirate: false, empire.DominantRace.Name);
                    empire.Research = sourceEmpire.Research.Clone(sourceEmpire.DominantRace);
                    _Galaxy.MergeGalaxyMap(sourceEmpire, empire);
                    DiplomaticRelation diplomaticRelation = sourceEmpire.ObtainDiplomaticRelation(empire);
                    diplomaticRelation.Type = DiplomaticRelationType.None;
                    diplomaticRelation = empire.ObtainDiplomaticRelation(sourceEmpire);
                    diplomaticRelation.Type = DiplomaticRelationType.None;
                    foreach (DiplomaticRelation diplomaticRelation4 in sourceEmpire.DiplomaticRelations)
                    {
                        if (diplomaticRelation4.Type != 0)
                        {
                            DiplomaticRelation diplomaticRelation2 = empire.ObtainDiplomaticRelation(diplomaticRelation4.OtherEmpire);
                            diplomaticRelation2.Type = DiplomaticRelationType.None;
                            DiplomaticRelation diplomaticRelation3 = diplomaticRelation4.OtherEmpire.ObtainDiplomaticRelation(empire);
                            diplomaticRelation3.Type = DiplomaticRelationType.None;
                        }
                    }
                    double num3 = splinterPortion * 0.7;
                    BuiltObjectList builtObjectList = new BuiltObjectList();
                    builtObjectList.AddRange(sourceEmpire.PrivateBuiltObjects);
                    foreach (BuiltObject item in builtObjectList)
                    {
                        if (ConsiderTakeoverOfBuiltObject(item, sourceEmpire, empire, num3))
                        {
                            empire.TakeOwnershipOfBuiltObject(item, empire, setDesignAsObsolete: true);
                        }
                    }
                    BuiltObjectList builtObjectList2 = new BuiltObjectList();
                    builtObjectList2.AddRange(sourceEmpire.BuiltObjects);
                    foreach (BuiltObject item2 in builtObjectList2)
                    {
                        if (ConsiderTakeoverOfBuiltObject(item2, sourceEmpire, empire, num3))
                        {
                            empire.TakeOwnershipOfBuiltObject(item2, empire, setDesignAsObsolete: true);
                        }
                    }
                    ShipGroupList shipGroupList = new ShipGroupList();
                    foreach (ShipGroup shipGroup in sourceEmpire.ShipGroups)
                    {
                        if ((shipGroup.GatherPoint == null || shipGroup.GatherPoint.Empire != empire) && !(Galaxy.Rnd.NextDouble() < num3))
                        {
                            continue;
                        }
                        shipGroupList.Add(shipGroup);
                        empire.ShipGroups.Add(shipGroup);
                        empire.ShipGroups.Sort();
                        foreach (BuiltObject ship in shipGroup.Ships)
                        {
                            empire.TakeOwnershipOfBuiltObject(ship, empire, setDesignAsObsolete: true);
                        }
                    }
                    foreach (ShipGroup item3 in shipGroupList)
                    {
                        sourceEmpire.ShipGroups.Remove(item3);
                    }
                    _Galaxy.Empires.Add(empire);
                }
            }
            return empire;
        }

        private bool ConsiderTakeoverOfBuiltObject(BuiltObject builtObject, Empire sourceEmpire, Empire newEmpire, double chance)
        {
            bool flag = false;
            if (builtObject.ParentHabitat != null && builtObject.ParentHabitat.Empire == newEmpire)
            {
                flag = true;
            }
            if (builtObject.ParentBuiltObject != null && builtObject.ParentBuiltObject.Empire == newEmpire)
            {
                flag = true;
            }
            BuiltObjectMission mission = builtObject.Mission;
            if (mission != null)
            {
                switch (mission.Type)
                {
                    case BuiltObjectMissionType.Patrol:
                    case BuiltObjectMissionType.Escort:
                    case BuiltObjectMissionType.Retire:
                    case BuiltObjectMissionType.Retrofit:
                    case BuiltObjectMissionType.Hold:
                    case BuiltObjectMissionType.MoveAndWait:
                    case BuiltObjectMissionType.Refuel:
                    case BuiltObjectMissionType.LoadTroops:
                    case BuiltObjectMissionType.UnloadTroops:
                    case BuiltObjectMissionType.Repair:
                    case BuiltObjectMissionType.Move:
                        if (mission.TargetBuiltObject != null && mission.TargetBuiltObject.Empire == newEmpire)
                        {
                            flag = true;
                        }
                        if (mission.TargetHabitat != null && mission.TargetHabitat.Empire == newEmpire)
                        {
                            flag = true;
                        }
                        break;
                    case BuiltObjectMissionType.Transport:
                        if (mission.TargetBuiltObject != null && mission.TargetBuiltObject.Empire == newEmpire)
                        {
                            flag = true;
                        }
                        if (mission.SecondaryTargetBuiltObject != null && mission.SecondaryTargetBuiltObject.Empire == newEmpire)
                        {
                            flag = true;
                        }
                        if (mission.TargetHabitat != null && mission.TargetHabitat.Empire == newEmpire)
                        {
                            flag = true;
                        }
                        if (mission.SecondaryTargetHabitat != null && mission.SecondaryTargetHabitat.Empire == newEmpire)
                        {
                            flag = true;
                        }
                        break;
                    case BuiltObjectMissionType.WaitAndAttack:
                    case BuiltObjectMissionType.WaitAndBombard:
                        if (mission.SecondaryTargetBuiltObject != null && mission.SecondaryTargetBuiltObject.Empire == newEmpire)
                        {
                            flag = true;
                        }
                        if (mission.SecondaryTargetHabitat != null && mission.SecondaryTargetHabitat.Empire == newEmpire)
                        {
                            flag = true;
                        }
                        break;
                }
            }
            if (!flag && builtObject.Role == BuiltObjectRole.Base)
            {
                double num = double.MaxValue;
                double num2 = double.MaxValue;
                Habitat habitat = _Galaxy.FastFindNearestColony((int)builtObject.Xpos, (int)builtObject.Ypos, sourceEmpire, 0);
                if (habitat != null)
                {
                    num = _Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, builtObject.Xpos, builtObject.Ypos);
                }
                Habitat habitat2 = _Galaxy.FastFindNearestColony((int)builtObject.Xpos, (int)builtObject.Ypos, newEmpire, 0);
                if (habitat2 != null)
                {
                    num2 = _Galaxy.CalculateDistance(habitat2.Xpos, habitat2.Ypos, builtObject.Xpos, builtObject.Ypos);
                }
                if (num2 < num)
                {
                    flag = true;
                }
            }
            if (!flag && builtObject.Role != BuiltObjectRole.Base && Galaxy.Rnd.NextDouble() < chance)
            {
                flag = true;
            }
            if (builtObject.ShipGroup != null)
            {
                flag = false;
            }
            return flag;
        }

        public HabitatPrioritizationList PirateReviewColoniesToControl()
        {
            HabitatPrioritizationList coloniesToControl = new HabitatPrioritizationList();
            HabitatList controlledColonies = new HabitatList();
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat != null && !habitat.HasBeenDestroyed && habitat.Empire == this && !controlledColonies.Contains(habitat))
                {
                    controlledColonies.Add(habitat);
                }
            }
            if (PirateEmpireBaseHabitat != null)
            {
                double xpos = PirateEmpireBaseHabitat.Xpos;
                double ypos = PirateEmpireBaseHabitat.Ypos;
                for (int j = 0; j < _Galaxy.IndependentColonies.Count; j++)
                {
                    Habitat colony = _Galaxy.IndependentColonies[j];
                    PirateCheckControlColony(colony, xpos, ypos, ref coloniesToControl, ref controlledColonies);
                }
                for (int k = 0; k < _Galaxy.Empires.Count; k++)
                {
                    Empire empire = _Galaxy.Empires[k];
                    if (empire != null && empire.Active && empire.Colonies != null)
                    {
                        for (int l = 0; l < empire.Colonies.Count; l++)
                        {
                            Habitat colony2 = empire.Colonies[l];
                            PirateCheckControlColony(colony2, xpos, ypos, ref coloniesToControl, ref controlledColonies);
                        }
                    }
                }
                coloniesToControl.Sort();
                coloniesToControl.Reverse();
                Colonies = controlledColonies;
            }
            return coloniesToControl;
        }

        private void PirateCheckControlColony(Habitat colony, double pirateEmpireX, double pirateEmpireY, ref HabitatPrioritizationList coloniesToControl, ref HabitatList controlledColonies)
        {
            if (colony == null || colony.HasBeenDestroyed || colony.Population == null || colony.Population.TotalAmount <= 0 || (colony.Empire != null && colony.Empire.Reclusive))
            {
                return;
            }
            PirateColonyControl byFaction = colony.GetPirateControl().GetByFaction(this);
            if (colony.Empire == this || byFaction != null)
            {
                if (!controlledColonies.Contains(colony))
                {
                    controlledColonies.Add(colony);
                }
            }
            else
            {
                if (!CheckSystemExplored(colony.SystemIndex))
                {
                    return;
                }
                bool flag = false;
                double num = 0.0;
                if (colony.Empire == _Galaxy.IndependentEmpire)
                {
                    flag = true;
                }
                else if (colony.Empire != null && colony.Population != null && colony.Population.TotalAmount < 2000000000)
                {
                    num = colony.CurrentDefensiveForceAssigned;
                    flag = true;
                }
                if (!flag)
                {
                    return;
                }
                double num2 = _Galaxy.CalculateDistance(pirateEmpireX, pirateEmpireY, colony.Xpos, colony.Ypos);
                double num3 = (double)Galaxy.SizeX * 0.25;
                if (PiratePlayStyle == PiratePlayStyle.Balanced || PiratePlayStyle == PiratePlayStyle.Legendary)
                {
                    num3 *= 1.4;
                }
                if (num2 < num3)
                {
                    double num4 = Math.Sqrt(num3 - num2) * ((double)colony.Population.TotalAmount / 1000.0);
                    if (num > 0.0)
                    {
                        num4 /= Math.Sqrt(num);
                    }
                    num4 = Math.Min(num4, 2147483647.0);
                    HabitatPrioritization item = new HabitatPrioritization(colony, (int)num4);
                    coloniesToControl.Add(item);
                }
            }
        }

        private void RespondToIncomingEnemyFleetsAndPlanetDestroyers()
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            FleetAttackList fleetAttackList = new FleetAttackList();
            if (IncomingEnemyFleetsAndPlanetDestroyers.Count > 0)
            {
                IncomingEnemyFleetsAndPlanetDestroyers.Sort();
                for (int i = 0; i < IncomingEnemyFleetsAndPlanetDestroyers.Count; i++)
                {
                    FleetAttack fleetAttack = IncomingEnemyFleetsAndPlanetDestroyers[i];
                    if (fleetAttack.PlanetDestroyer != null)
                    {
                        if (fleetAttack.PlanetDestroyer.Mission == null || (fleetAttack.PlanetDestroyer.Mission.Type != BuiltObjectMissionType.Attack && fleetAttack.PlanetDestroyer.Mission.Type != BuiltObjectMissionType.WaitAndAttack && fleetAttack.PlanetDestroyer.Mission.Type != BuiltObjectMissionType.Bombard && fleetAttack.PlanetDestroyer.Mission.Type != BuiltObjectMissionType.WaitAndBombard))
                        {
                            fleetAttackList.Add(fleetAttack);
                            continue;
                        }
                        Empire empire = BuiltObjectMission.ResolveMissionTargetEmpire(fleetAttack.PlanetDestroyer.Mission);
                        if (empire != this)
                        {
                            bool flag = true;
                            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                            if ((diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact || (diplomaticRelation.Type == DiplomaticRelationType.Protectorate && diplomaticRelation.Initiator == this)) && fleetAttack.PlanetDestroyer != null && fleetAttack.PlanetDestroyer.Mission != null && _Galaxy.GlobalVictoryConditions != null)
                            {
                                if (_Galaxy.GlobalVictoryConditions.DefendHabitat != null && _Galaxy.GlobalVictoryConditions.DefendHabitat == fleetAttack.PlanetDestroyer.Mission.Target)
                                {
                                    flag = false;
                                }
                                if (_Galaxy.GlobalVictoryConditions.TargetHabitat != null && _Galaxy.GlobalVictoryConditions.TargetHabitat == fleetAttack.PlanetDestroyer.Mission.Target)
                                {
                                    flag = false;
                                }
                            }
                            if (flag)
                            {
                                fleetAttackList.Add(fleetAttack);
                                continue;
                            }
                        }
                        Point point = fleetAttack.PlanetDestroyer.Mission.ResolveTargetCoordinates(fleetAttack.PlanetDestroyer.Mission);
                        int num = 0;
                        BuiltObjectList ships = new BuiltObjectList();
                        num = _Galaxy.DetermineBuiltObjectStrengthAtLocation(point.X, point.Y, this, 0, includeAllies: false, out ships);
                        ShipGroupList shipGroupList = ShipGroups.DetermineFleetsTravellingToLocation(point.X, point.Y, 2000.0);
                        num += shipGroupList.CountTotalOverallStrengthFactor();
                        long num2 = fleetAttack.PlanetDestroyer.CalculateTimeToArrivalAtDestination();
                        long starDate = currentStarDate + num2 + 20000;
                        int num3 = 10000;
                        if (num >= num3)
                        {
                            continue;
                        }
                        ShipGroup shipGroup = IdentifyNearestAvailableFleet(point.X, point.Y, mustBeAutomated: true, mustBeWithinFuelRange: true, 0.0, 48000.0);
                        if (shipGroup != null)
                        {
                            StellarObject stellarObject = fleetAttack.PlanetDestroyer.Mission.ResolveMissionTargetHabitatIfPossible();
                            if (stellarObject == null)
                            {
                                shipGroup.AssignMission(BuiltObjectMissionType.MoveAndWait, null, null, null, null, point.X, point.Y, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            }
                            else
                            {
                                shipGroup.AssignMission(BuiltObjectMissionType.MoveAndWait, stellarObject, null, null, null, -2000000001.0, -2000000001.0, starDate, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            }
                            shipGroup.AllowImmediateThreatEvaluation = true;
                            shipGroup.AttackRangeSquared = (float)AttackRangeAttack * (float)AttackRangeAttack;
                            StellarObject stellarObject3 = (shipGroup.GatherPoint = FindNearestRefuellingPoint(point.X, point.Y, shipGroup.LeadShip.FuelType, 4));
                        }
                    }
                    else
                    {
                        if (fleetAttack.Fleet == null)
                        {
                            continue;
                        }
                        if (fleetAttack.Fleet.Mission == null || (fleetAttack.Fleet.Mission.Type != BuiltObjectMissionType.Attack && fleetAttack.Fleet.Mission.Type != BuiltObjectMissionType.WaitAndAttack && fleetAttack.Fleet.Mission.Type != BuiltObjectMissionType.Bombard && fleetAttack.Fleet.Mission.Type != BuiltObjectMissionType.WaitAndBombard))
                        {
                            fleetAttackList.Add(fleetAttack);
                            continue;
                        }
                        Empire empire2 = BuiltObjectMission.ResolveMissionTargetEmpire(fleetAttack.Fleet.Mission);
                        if (empire2 != this)
                        {
                            fleetAttackList.Add(fleetAttack);
                            continue;
                        }
                        Point point2 = fleetAttack.Fleet.Mission.ResolveTargetCoordinates(fleetAttack.Fleet.Mission);
                        int num4 = 0;
                        BuiltObjectList ships2 = new BuiltObjectList();
                        num4 = _Galaxy.DetermineBuiltObjectStrengthAtLocation(point2.X, point2.Y, this, 0, includeAllies: false, out ships2);
                        ShipGroupList shipGroupList2 = ShipGroups.DetermineFleetsTravellingToLocation(point2.X, point2.Y, 2000.0);
                        num4 += shipGroupList2.CountTotalOverallStrengthFactor();
                        long num5 = fleetAttack.Fleet.CalculateTimeToArrivalAtDestination();
                        long starDate2 = currentStarDate + num5 + 20000;
                        int num6 = (int)((double)fleetAttack.Fleet.TotalOverallStrengthFactor * 1.3);
                        if (num4 >= num6)
                        {
                            continue;
                        }
                        ShipGroup shipGroup2 = IdentifyNearestAvailableFleet(point2.X, point2.Y, mustBeAutomated: true, mustBeWithinFuelRange: true, 0.0, 48000.0);
                        if (shipGroup2 != null)
                        {
                            StellarObject stellarObject4 = fleetAttack.Fleet.Mission.ResolveMissionTargetHabitatIfPossible();
                            if (stellarObject4 == null)
                            {
                                shipGroup2.AssignMission(BuiltObjectMissionType.MoveAndWait, null, null, null, null, point2.X, point2.Y, starDate2, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            }
                            else
                            {
                                shipGroup2.AssignMission(BuiltObjectMissionType.MoveAndWait, stellarObject4, null, null, null, 2000000001.0, -2000000001.0, starDate2, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                            }
                            shipGroup2.AllowImmediateThreatEvaluation = true;
                            shipGroup2.AttackRangeSquared = (float)AttackRangeAttack * (float)AttackRangeAttack;
                            StellarObject stellarObject6 = (shipGroup2.GatherPoint = FindNearestRefuellingPoint(point2.X, point2.Y, shipGroup2.LeadShip.FuelType, 4));
                        }
                    }
                }
            }
            for (int j = 0; j < fleetAttackList.Count; j++)
            {
                IncomingEnemyFleetsAndPlanetDestroyers.Remove(fleetAttackList[j]);
            }
        }

        public void RemoveDefeatedEmpireRelations()
        {
            DiplomaticRelationList diplomaticRelationList = new DiplomaticRelationList();
            if (DiplomaticRelations != null)
            {
                for (int i = 0; i < DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                    if (!diplomaticRelation.OtherEmpire.Active)
                    {
                        diplomaticRelationList.Add(diplomaticRelation);
                    }
                }
            }
            for (int j = 0; j < diplomaticRelationList.Count; j++)
            {
                DiplomaticRelations.Remove(diplomaticRelationList[j]);
            }
            EmpireEvaluationList empireEvaluationList = new EmpireEvaluationList();
            if (EmpireEvaluations != null)
            {
                for (int k = 0; k < EmpireEvaluations.Count; k++)
                {
                    EmpireEvaluation empireEvaluation = EmpireEvaluations[k];
                    if (!empireEvaluation.Empire.Active)
                    {
                        empireEvaluationList.Add(empireEvaluation);
                    }
                }
            }
            for (int l = 0; l < empireEvaluationList.Count; l++)
            {
                EmpireEvaluations.Remove(empireEvaluationList[l]);
            }
            PirateRelationList pirateRelationList = new PirateRelationList();
            if (PirateRelations != null)
            {
                for (int m = 0; m < PirateRelations.Count; m++)
                {
                    PirateRelation pirateRelation = PirateRelations[m];
                    if (pirateRelation != null && pirateRelation.OtherEmpire != null && !pirateRelation.OtherEmpire.Active)
                    {
                        pirateRelationList.AddRaw(pirateRelation);
                    }
                }
            }
            for (int n = 0; n < pirateRelationList.Count; n++)
            {
                PirateRelations.Remove(pirateRelationList[n]);
            }
        }

        public bool CheckDesignResourcesAtConstructionYard(Design design, StellarObject constructionYard, out ResourceList deficientResources)
        {
            deficientResources = new ResourceList();
            if (design != null && design.Components != null && constructionYard != null && constructionYard.Cargo != null)
            {
                ComponentResourceList componentResourceList = ResolveResourcesFromComponents(design.Components);
                for (int i = 0; i < componentResourceList.Count; i++)
                {
                    Cargo cargo = constructionYard.Cargo.GetCargo(componentResourceList[i], this);
                    if (cargo != null)
                    {
                        if (cargo.Available < componentResourceList[i].Quantity)
                        {
                            deficientResources.Add(componentResourceList[i]);
                        }
                    }
                    else
                    {
                        deficientResources.Add(componentResourceList[i]);
                    }
                }
                if (deficientResources.Count > 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void ReviewDesignsAndRetrofit()
        {
            if (_ReviewDesignsAndRetrofit)
            {
                if (_ControlDesigns)
                {
                    CreateNewDesigns(_Galaxy.CurrentStarDate);
                }
                long privateRetrofitAge = Galaxy.RealSecondsInGalacticYear * 1000 * 2;
                if (_ReviewDesignsAndRetrofitImportantBreakthrough)
                {
                    privateRetrofitAge = 0L;
                }
                RetrofitBuiltObjects(0L, privateRetrofitAge, breakthroughInitiated: true);
                _ReviewDesignsAndRetrofit = false;
                _ReviewDesignsAndRetrofitImportantBreakthrough = false;
            }
        }

        public void ResetLastTouchTimesToMinimum()
        {
            _LastHugeTouch = (_LastLongTouch = (_LastIntermediateTouch = (_LastPeriodicTouch = (_LastRegularTouch = (_LastShortTouch = DateTime.MinValue)))));
        }

        public void DoTasks()
        {
            if (PirateEmpireBaseHabitat != null)
            {
                DoTasksPirates();
            }
            else
            {
                if (!Active)
                {
                    return;
                }
                DateTime currentDateTime = _Galaxy.CurrentDateTime;
                double num = (double)currentDateTime.Subtract(_LastShortTouch).Ticks / 10000000.0;
                currentDateTime.Subtract(_LastShortTouch);
                double num2 = (double)currentDateTime.Subtract(_LastRegularTouch).Ticks / 10000000.0;
                currentDateTime.Subtract(_LastRegularTouch);
                double num3 = (double)currentDateTime.Subtract(_LastPeriodicTouch).Ticks / 10000000.0;
                currentDateTime.Subtract(_LastPeriodicTouch);
                double num4 = (double)currentDateTime.Subtract(_LastIntermediateTouch).Ticks / 10000000.0;
                currentDateTime.Subtract(_LastIntermediateTouch);
                double num5 = (double)currentDateTime.Subtract(_LastLongTouch).Ticks / 10000000.0;
                TimeSpan timePassedSpan = currentDateTime.Subtract(_LastLongTouch);
                double num6 = (double)currentDateTime.Subtract(_LastHugeTouch).Ticks / 10000000.0;
                currentDateTime.Subtract(_LastHugeTouch);
                if (num < 0.0)
                {
                    _LastShortTouch = currentDateTime;
                }
                if (num2 < 0.0)
                {
                    _LastRegularTouch = currentDateTime;
                }
                if (num3 < 0.0)
                {
                    _LastPeriodicTouch = currentDateTime;
                }
                if (num4 < 0.0)
                {
                    _LastIntermediateTouch = currentDateTime;
                }
                if (num5 < 0.0)
                {
                    _LastLongTouch = currentDateTime;
                }
                if (num6 < 0.0)
                {
                    _LastHugeTouch = currentDateTime;
                }
                if (num >= _ShortProcessingInterval)
                {
                    _LastShortTouch = currentDateTime;
                }
                if (num2 >= _RegularProcessingInterval)
                {
                    _LastRegularTouch = currentDateTime;
                }
                if (num3 >= _PeriodicProcessingInterval)
                {
                    _LastPeriodicTouch = currentDateTime;
                }
                if (num4 >= _IntermediateProcessingInterval)
                {
                    _LastIntermediateTouch = currentDateTime;
                }
                if (num5 >= _LongProcessingInterval)
                {
                    _LastLongTouch = currentDateTime;
                }
                if (num6 >= _HugeProcessingInterval)
                {
                    _LastHugeTouch = currentDateTime;
                }
                bool flag = true;
                if (DominantRace != null)
                {
                    flag = DominantRace.Expanding;
                }
                if (num >= _ShortProcessingInterval)
                {
                    RespondToIncomingEnemyFleetsAndPlanetDestroyers();
                    for (int i = 0; i < ShipGroups.Count; i++)
                    {
                        ShipGroup shipGroup = ShipGroups[i];
                        shipGroup.DoTasks(currentDateTime);
                    }
                    ProcessCharacters(num);
                }
                if (num2 >= _RegularProcessingInterval)
                {
                    ProcessDistressSignals();
                    ClearOutOldDistressSignals();
                    ClearExpiredViewableEmpires();
                    ReviewDesignsAndRetrofit();
                    UpdateEmpireRefuellingLocations();
                }
                if (num3 >= _PeriodicProcessingInterval)
                {
                    _RelativeEmpireSize = CalculateRelativeEmpireSize();
                    CheckReviewSpecialPirateEvents();
                    CheckSendPirateRaid();
                    RemoveDefeatedEmpireRelations();
                    ProcessMessages();
                    ConsiderTreatyProposals();
                    EvaluateColonyVariables(_Galaxy, num3);
                    RecruitAttackTroops();
                    RecalculateEmpireCorruption();
                    if (_ControlColonyTaxRates)
                    {
                        ReviewTaxes();
                    }
                    RecalculateColonyTaxRevenues();
                    if (_ControlMilitaryAttacks != 0)
                    {
                        IdentifyMilitaryObjectives();
                    }
                    if (_ControlMilitaryFleets)
                    {
                        MaintainShipGroups();
                        UpdateFleetLeadShips();
                        ReviewFleetPostures();
                    }
                    CancelInactiveBlockades();
                    ReviewPirateDefendMissions(_Galaxy.CurrentStarDate);
                    ReviewPirateSmugglingMissions(_Galaxy.CurrentStarDate);
                    CheckMarketOrders();
                    AssignShipMissions();
                    if (_ControlAgentAssignment != 0)
                    {
                        AssignSpecialMissions();
                    }
                    PerformIntelligenceMissions();
                    PerformResearch(num3, allowResearchEvents: true);
                    EvaluateSystemLinks();
                }
                if (num4 >= _IntermediateProcessingInterval)
                {
                    if (_ControlMilitaryFleets)
                    {
                        TaskShipGroups();
                    }
                    ReviewFleetAdmiralBonuses();
                    TaskResupplyShips();
                    ReviewResearchStationBonuses();
                    if (flag)
                    {
                        ReviewIndependentColonyTargets();
                    }
                    ProcessTradeBonuses(num4);
                    ReviewColonyPopulationPolicy(num4);
                    UpdateSystemExplorationStatus();
                    CheckKnownPirateBases();
                    double privateAnnualRevenue = PrivateAnnualRevenue;
                    double num7 = privateAnnualRevenue * (num4 / (double)Galaxy.RealSecondsInGalacticYear);
                    if (double.IsNaN(num7))
                    {
                        num7 = 0.0;
                    }
                    Counters.ProcessColonyRevenue(num7);
                    _PrivateMoney += num7;
                    double annualTaxRevenue = AnnualTaxRevenue;
                    double val = annualTaxRevenue * (num4 / (double)Galaxy.RealSecondsInGalacticYear);
                    val = Math.Max(0.0, val);
                    if (double.IsNaN(val))
                    {
                        val = 0.0;
                    }
                    _StateMoney += val;
                    _PrivateMoney -= val;
                    if (double.IsNaN(_StateMoney))
                    {
                        _StateMoney = 0.0;
                    }
                    if (double.IsNaN(_PrivateMoney))
                    {
                        _PrivateMoney = 0.0;
                    }
                    ProcessSubjugationTribute(num4);
                    if (_StateMoney < 0.0)
                    {
                        _Galaxy.DoCharacterEventLeader(CharacterEventType.CashNegative, null, this);
                    }
                    else
                    {
                        _Galaxy.DoCharacterEventLeader(CharacterEventType.CashPositive, null, this);
                    }
                    if (this != _Galaxy.IndependentEmpire && DominantRace != null)
                    {
                        CheckForCharacterAppearance();
                    }
                    ReviewCharacterLeaderChange(num4);
                    ProcessLeaderChangeInfluence(num4);
                    ReviewCharacterBonusesKnown();
                    ReviewCharacterTraits();
                    ReviewDemoralizingCharacters();
                    ReviewCharacterLocations();
                    if (_ControlDesigns)
                    {
                        CreateNewDesigns(_Galaxy.CurrentStarDate);
                    }
                    ReviewSystemThreats();
                    _ColonizationTargets = IdentifyColonizationTargets(_Galaxy);
                    if (flag)
                    {
                        InvadeUnwillingColonizationTargets(_Galaxy);
                    }
                    _ResourceTargets = IdentifyResourceCentres(_Galaxy);
                    _EmpireResourceTargets = PrioritizeEmpireResourceNeeds();
                    IdentifyUnavailableLuxuryResources();
                    if (this == _Galaxy.PlayerEmpire)
                    {
                        UpdateSystemRefuellingStatus();
                        CheckForStrandedShips();
                    }
                    UpdateSystemFuelSourceStatus();
                    UpdateAchievements();
                }
                if (num5 >= _LongProcessingInterval)
                {
                    MergeGalaxyMapsForSharedVisibilityEmpires();
                    MergeKnownPirateBasesForSharedVisibilityEmpires();
                    if (flag)
                    {
                        CheckTemptingTargets();
                    }
                    ReviewColonyWonders();
                    ReviewColonyFacilities();
                    RefreshColonyFacilityInfo();
                    if (flag)
                    {
                        SendAvailableFleetsToGuardStrategicLocations();
                    }
                    ReviewEmpireAbilityBonuses();
                    ReviewGovernmentEffects(num5);
                    CheckChangeGovernment();
                    RecalculateColonyTaxRevenues();
                    ClearInvalidDiplomaticRelations();
                    EvaluatePoliticalSituation(timePassedSpan);
                    ReviewDiplomaticStrategies();
                    ReviewDiplomaticSituations();
                    ReviewPirateRelations(_Galaxy.CurrentStarDate, num5);
                    if (CheckHaveMetPirates(this))
                    {
                        MakeAttackOffersToPirates(_Galaxy.CurrentStarDate);
                        MakeDefendOffersToPirates(_Galaxy.CurrentStarDate);
                        MakeSmugglingOffersToPirates(_Galaxy.CurrentStarDate);
                    }
                    ReviewRestrictedResourceTrading();
                    ReviewSpecialBonusesRuinsWonders();
                    ReviewMigrationTourism();
                    DoCrashResearch();
                    if (!Reclusive)
                    {
                        TradeItems();
                    }
                    if (_ControlTroopGeneration)
                    {
                        DisbandExcessTroops();
                    }
                    if (_ControlMilitaryAttacks != 0 && !Reclusive)
                    {
                        DetermineRandomAttacks();
                    }
                    ProjectForceStructure();
                    ProjectPrivateForceStructure();
                    PayMaintenanceForBuiltObjects(num5);
                    PayForTroops(num5);
                    PayForPlanetaryFacilities(num5);
                    RetireOldBuiltObjects();
                    if (_InitiateConstruction)
                    {
                        ReviewLatestDesigns();
                        DirectConstruction();
                        DirectPrivateConstruction();
                    }
                    ExertCulturalInfluence();
                    ClearOldDistressSignals();
                    ClearExpiredDeclinedTasks();
                    DetermineMonitoringStationLocation();
                    DetermineResearchStationLocation(allowOccupiedSystems: false, mustHaveBuildableResearchStationDesign: true);
                }
                if (num6 >= _HugeProcessingInterval)
                {
                    CleanupInvalidShips();
                    ReviewEmpireEndsAllWars(_Galaxy.CurrentStarDate);
                    BuildDefensiveBases();
                    CheckColoniesForPirateFacilitiesAndAttack();
                    ShakturiSendConvoy();
                    CheckOfferStoryHint();
                    ResetRaceEvents();
                    ReviewRandomEvents();
                    ReviewEmpireEvents();
                    CheckSendShipConvoysViaGateway(num6);
                    MaintainBaseResourceLevels();
                    if (!Reclusive)
                    {
                        ReviewEnemyHelpEnlistment();
                    }
                    ReviewDisputedTerritory();
                }
            }
        }

        public bool CoordinateFleetAttacksWithAllies(ShipGroup fleet)
        {
            if (this != _Galaxy.PlayerEmpire && _ControlMilitaryAttacks == AutomationLevel.FullyAutomated && fleet != null && fleet.Ships != null && fleet.Ships.Count >= 10 && fleet.Posture == FleetPosture.Attack && fleet.LeadShip != null && fleet.LeadShip.IsAutoControlled)
            {
                DetermineFriendsAndEnemies(this, out var _, out var closeFriends, out var _, out var severeEnemies);
                if (closeFriends.Count > 0 && severeEnemies.Count > 0)
                {
                    for (int i = 0; i < closeFriends.Count; i++)
                    {
                        Empire empire = closeFriends[i];
                        if (empire != null && empire.ShipGroups != null && empire.ShipGroups.Count > 0 && CoordinateFleetAttacksWithAllies(fleet, empire, severeEnemies))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool CoordinateFleetAttacksWithAllies(ShipGroup fleet, Empire empire, EmpireList enemies)
        {
            if (fleet != null && empire != null && empire.ShipGroups != null && empire.ShipGroups.Count > 0)
            {
                for (int i = 0; i < empire.ShipGroups.Count; i++)
                {
                    ShipGroup shipGroup = empire.ShipGroups[i];
                    if (shipGroup == null || shipGroup == fleet || shipGroup.Ships == null || shipGroup.Ships.Count < 10)
                    {
                        continue;
                    }
                    BuiltObjectMission mission = shipGroup.Mission;
                    if (mission == null)
                    {
                        continue;
                    }
                    BuiltObjectMissionType type = mission.Type;
                    if (type != BuiltObjectMissionType.Attack)
                    {
                        continue;
                    }
                    Empire empire2 = BuiltObjectMission.ResolveMissionTargetEmpire(mission);
                    if (enemies != null && !enemies.Contains(empire2))
                    {
                        continue;
                    }
                    Point point = mission.ResolveTargetCoordinates(mission);
                    BuiltObject leadShip = fleet.LeadShip;
                    if (leadShip == null || !fleet.CheckFleetTargetWithinFuelRange(point.X, point.Y, 0.2))
                    {
                        continue;
                    }
                    double num = Galaxy.CalculateDistanceStatic(point.X, point.Y, leadShip.Xpos, leadShip.Ypos);
                    if (!(num < (double)Galaxy.SectorSize * 3.0))
                    {
                        continue;
                    }
                    double nearestDistance = double.MaxValue;
                    ShipGroupList shipGroupList = ShipGroups.ResolveFleetsWithAttackTarget(mission.Target, out nearestDistance);
                    double num2 = num / nearestDistance;
                    if ((shipGroupList.Count > 0 && (shipGroupList.Count >= 3 || !(num2 < 0.5))) || !fleet.AssignMission(BuiltObjectMissionType.Attack, mission.Target, null, BuiltObjectMissionPriority.High, manuallyAssigned: false))
                    {
                        continue;
                    }
                    if (empire != this)
                    {
                        string arg = string.Empty;
                        if (mission.TargetBuiltObject != null)
                        {
                            arg = mission.TargetBuiltObject.Name;
                        }
                        else if (mission.TargetHabitat != null)
                        {
                            arg = mission.TargetHabitat.Name;
                        }
                        else if (mission.TargetShipGroup != null)
                        {
                            arg = mission.TargetShipGroup.Name;
                        }
                        else if (mission.TargetCreature != null)
                        {
                            arg = mission.TargetCreature.Name;
                        }
                        string description = string.Format(TextResolver.GetText("We are sending our FLEET to join your attack on TARGET of EMPIRE"), fleet.Name, arg, empire2.Name);
                        string title = string.Format(TextResolver.GetText("EMPIRE sends fleet to join our attack on TARGET"), Name, arg);
                        SendMessageToEmpireWithTitle(empire, EmpireMessageType.BattleAttacking, mission.Target, description, title);
                    }
                    return true;
                }
            }
            return false;
        }

        private void CoordinateFleetAttacksWithAllies()
        {
            if (this == _Galaxy.PlayerEmpire || _ControlMilitaryAttacks != AutomationLevel.FullyAutomated)
            {
                return;
            }
            DetermineFriendsAndEnemies(this, out var _, out var closeFriends, out var _, out var severeEnemies);
            if (closeFriends.Count <= 0 || severeEnemies.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < closeFriends.Count; i++)
            {
                Empire empire = closeFriends[i];
                if (empire == null || empire.ShipGroups == null || empire.ShipGroups.Count <= 0)
                {
                    continue;
                }
                bool flag = false;
                for (int j = 0; j < empire.ShipGroups.Count; j++)
                {
                    ShipGroup shipGroup = empire.ShipGroups[j];
                    if (shipGroup != null && shipGroup.Ships != null && shipGroup.Ships.Count >= 10)
                    {
                        BuiltObjectMission mission = shipGroup.Mission;
                        if (mission != null)
                        {
                            BuiltObjectMissionType type = mission.Type;
                            if (type == BuiltObjectMissionType.Attack)
                            {
                                Empire empire2 = BuiltObjectMission.ResolveMissionTargetEmpire(mission);
                                if (severeEnemies.Contains(empire2))
                                {
                                    double nearestDistance = double.MaxValue;
                                    ShipGroupList shipGroupList = ShipGroups.ResolveFleetsWithAttackTarget(mission.Target, out nearestDistance);
                                    if (shipGroupList.Count <= 0 || nearestDistance > 8000000.0)
                                    {
                                        Point point = mission.ResolveTargetCoordinates(mission);
                                        ShipGroup shipGroup2 = FindNearestAvailableFleet(point.X, point.Y, BuiltObjectMissionPriority.Normal, 0, FleetPosture.Attack, mustBeWithinFuelRange: true, 0.2, mustBeAutomated: true, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: false, 0, 0);
                                        if (shipGroup2 != null && shipGroup2.AssignMission(BuiltObjectMissionType.Attack, mission.Target, null, BuiltObjectMissionPriority.High, manuallyAssigned: false))
                                        {
                                            flag = true;
                                            string arg = string.Empty;
                                            if (mission.TargetBuiltObject != null)
                                            {
                                                arg = mission.TargetBuiltObject.Name;
                                            }
                                            else if (mission.TargetHabitat != null)
                                            {
                                                arg = mission.TargetHabitat.Name;
                                            }
                                            else if (mission.TargetShipGroup != null)
                                            {
                                                arg = mission.TargetShipGroup.Name;
                                            }
                                            else if (mission.TargetCreature != null)
                                            {
                                                arg = mission.TargetCreature.Name;
                                            }
                                            string description = string.Format(TextResolver.GetText("We are sending our FLEET to join your attack on TARGET of EMPIRE"), shipGroup2.Name, arg, empire2.Name);
                                            string title = string.Format(TextResolver.GetText("EMPIRE sends fleet to join our attack on TARGET"), Name, arg);
                                            SendMessageToEmpireWithTitle(empire, EmpireMessageType.BattleAttacking, mission.Target, description, title);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                }
            }
        }

        private void CheckSendShipConvoysViaGateway(double timePassed)
        {
            bool flag = false;
            Race race = null;
            if (Colonies != null)
            {
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat = Colonies[i];
                    if (habitat == null || habitat.Facilities == null)
                    {
                        continue;
                    }
                    for (int j = 0; j < habitat.Facilities.Count; j++)
                    {
                        PlanetaryFacility planetaryFacility = habitat.Facilities[j];
                        if (planetaryFacility != null && planetaryFacility.Type == PlanetaryFacilityType.Wonder && planetaryFacility.WonderType == WonderType.RaceAchievement && planetaryFacility.Value2 == 3)
                        {
                            flag = true;
                            if (habitat.Population != null)
                            {
                                race = habitat.Population.DominantRace;
                            }
                            break;
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                }
            }
            if (!flag)
            {
                return;
            }
            float supportCostFactor = 1f;
            if (race != null && race.Name == "Shakturi")
            {
                supportCostFactor = 0.2f;
            }
            if (Galaxy.Rnd.Next(0, 10) < 8)
            {
                switch (Galaxy.Rnd.Next(0, 3))
                {
                    case 0:
                    case 1:
                        {
                            int size2 = Galaxy.Rnd.Next(7, 22);
                            _Galaxy.GenerateMilitaryConvoy(this, size2, supportCostFactor);
                            break;
                        }
                    case 2:
                        {
                            int size = Galaxy.Rnd.Next(7, 22);
                            _Galaxy.GenerateCivilianConvoy(this, size, supportCostFactor, string.Empty);
                            break;
                        }
                }
            }
        }

        public void ReviewEmpireEndsAllWars(long starDate)
        {
            if (Counters.AtWarStartDate != long.MaxValue && !CheckAtWar(null))
            {
                Counters.FixupAtWarCounter(starDate);
            }
        }

        public void UpdateAchievements()
        {
            if (Achievements == null)
            {
                Achievements = new AchievementList();
            }
            AchievementList achievementList = _Galaxy.ReviewAchievementsForEmpire(this);
            for (int i = 0; i < achievementList.Count; i++)
            {
                Achievement achievement = achievementList[i];
                if (achievement == null)
                {
                    continue;
                }
                Achievement firstByType = Achievements.GetFirstByType(achievement.Type);
                if (firstByType != null)
                {
                    if (achievement.Value > firstByType.Value)
                    {
                        firstByType.Value = achievement.Value;
                    }
                }
                else
                {
                    Achievements.Add(achievement);
                }
                if (this == _Galaxy.PlayerEmpire)
                {
                    SteamAPI.SetAchievementIfNecessary(achievement);
                }
            }
        }

        private bool CheckBuildoutResearchCapacityAtColonies(out Design researchStationDesignToBuild, out Habitat colonyToBuildAt)
        {
            researchStationDesignToBuild = null;
            colonyToBuildAt = null;
            Design weaponsResearchStation;
            Design energyResearchStation;
            Design highTechResearchStation;
            Design design = AnalyzeNewResearchFacilities(out weaponsResearchStation, out energyResearchStation, out highTechResearchStation);
            if (design != null && (_ResearchHabitats == null || _ResearchHabitats.Count <= 0 || !CheckEmpireHasHyperDriveTech(this)))
            {
                if (ResearchFacilities.Count <= 0 || ResearchFacilities.CountResearchStations() <= 0)
                {
                    if (!CheckEmpireHasHyperDriveTech(this) && energyResearchStation != null)
                    {
                        design = energyResearchStation;
                    }
                    if (Policy.ResearchIndustryFocus != 0)
                    {
                        switch (Policy.ResearchIndustryFocus)
                        {
                            case IndustryType.Energy:
                                design = energyResearchStation;
                                break;
                            case IndustryType.HighTech:
                                design = highTechResearchStation;
                                break;
                            case IndustryType.Weapon:
                                design = weaponsResearchStation;
                                break;
                        }
                    }
                }
                if (design != null)
                {
                    double num = design.CalculateMaintenanceCosts(_Galaxy, this);
                    double num2 = design.CalculateCurrentPurchasePrice(_Galaxy);
                    if (num2 <= StateMoney && num <= CalculateSpareAnnualRevenueComplete())
                    {
                        Habitat habitat = null;
                        for (int i = 0; i < Colonies.Count; i++)
                        {
                            Habitat habitat2 = Colonies[i];
                            if (habitat2 != null && habitat2.BasesAtHabitat != null && habitat2.Population != null && habitat2.Population.TotalAmount > 2000000000)
                            {
                                int num3 = habitat2.BasesAtHabitat.CountResearchStations();
                                if (num3 < 2 && (habitat == null || habitat2.BasesAtHabitat.Count < habitat.BasesAtHabitat.Count))
                                {
                                    habitat = habitat2;
                                }
                            }
                        }
                        if (habitat != null)
                        {
                            researchStationDesignToBuild = design;
                            colonyToBuildAt = habitat;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void CheckSendPirateRaid()
        {
            if (PreWarpProgressEventOccurredSendPirateRaid || Capital == null)
            {
                return;
            }
            Empire empire = _Galaxy.FindNearestPirateFaction(Capital.Xpos, Capital.Ypos, _Galaxy.PlayerEmpire, includeSuperPirates: false);
            if (empire == null)
            {
                return;
            }
            if (empire.ShipGroups != null && empire.ShipGroups.Count > 0)
            {
                empire.ShipGroups[0]?.AssignMission(BuiltObjectMissionType.Raid, Capital, null, BuiltObjectMissionPriority.High, manuallyAssigned: true);
            }
            else
            {
                for (int i = 0; i < empire.BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = empire.BuiltObjects[i];
                    if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.BuiltAt == null && builtObject.UnbuiltComponentCount <= 0 && builtObject.Role == BuiltObjectRole.Military && builtObject.SubRole != BuiltObjectSubRole.ResupplyShip)
                    {
                        builtObject.ClearPreviousMissionRequirements();
                        builtObject.AssignMission(BuiltObjectMissionType.Raid, Capital, null, BuiltObjectMissionPriority.High, manuallyAssigned: true);
                    }
                }
            }
            PreWarpProgressEventOccurredSendPirateRaid = true;
        }

        public void DoTasksPirates()
        {
            if (!Active)
            {
                return;
            }
            long currentStarDate = _Galaxy.CurrentStarDate;
            DateTime currentDateTime = _Galaxy.CurrentDateTime;
            double num = (double)currentDateTime.Subtract(_LastShortTouch).Ticks / 10000000.0;
            currentDateTime.Subtract(_LastShortTouch);
            double num2 = (double)currentDateTime.Subtract(_LastRegularTouch).Ticks / 10000000.0;
            currentDateTime.Subtract(_LastRegularTouch);
            double num3 = (double)currentDateTime.Subtract(_LastPeriodicTouch).Ticks / 10000000.0;
            currentDateTime.Subtract(_LastPeriodicTouch);
            double num4 = (double)currentDateTime.Subtract(_LastIntermediateTouch).Ticks / 10000000.0;
            currentDateTime.Subtract(_LastIntermediateTouch);
            double num5 = (double)currentDateTime.Subtract(_LastLongTouch).Ticks / 10000000.0;
            currentDateTime.Subtract(_LastLongTouch);
            double num6 = (double)currentDateTime.Subtract(_LastHugeTouch).Ticks / 10000000.0;
            currentDateTime.Subtract(_LastHugeTouch);
            if (num < 0.0)
            {
                _LastShortTouch = currentDateTime;
            }
            if (num2 < 0.0)
            {
                _LastRegularTouch = currentDateTime;
            }
            if (num3 < 0.0)
            {
                _LastPeriodicTouch = currentDateTime;
            }
            if (num4 < 0.0)
            {
                _LastIntermediateTouch = currentDateTime;
            }
            if (num5 < 0.0)
            {
                _LastLongTouch = currentDateTime;
            }
            if (num6 < 0.0)
            {
                _LastHugeTouch = currentDateTime;
            }
            if (num >= _ShortProcessingInterval)
            {
                _LastShortTouch = currentDateTime;
            }
            if (num2 >= _RegularProcessingInterval)
            {
                _LastRegularTouch = currentDateTime;
            }
            if (num3 >= _PeriodicProcessingInterval)
            {
                _LastPeriodicTouch = currentDateTime;
            }
            if (num4 >= _IntermediateProcessingInterval)
            {
                _LastIntermediateTouch = currentDateTime;
            }
            if (num5 >= _LongProcessingInterval)
            {
                _LastLongTouch = currentDateTime;
            }
            if (num6 >= _HugeProcessingInterval)
            {
                _LastHugeTouch = currentDateTime;
            }
            if (num >= _ShortProcessingInterval)
            {
                for (int i = 0; i < ShipGroups.Count; i++)
                {
                    ShipGroup shipGroup = ShipGroups[i];
                    shipGroup.DoTasks(currentDateTime);
                }
                ProcessCharacters(num);
            }
            if (num2 >= _RegularProcessingInterval)
            {
                ReviewDesignsAndRetrofit();
                ClearExpiredViewableEmpires();
                PirateCheckMissionsOnOffer(currentStarDate);
                UpdateEmpireRefuellingLocations();
            }
            if (num3 >= _PeriodicProcessingInterval)
            {
                RemoveDefeatedEmpireRelations();
                PirateRecalculateEmpireCorruption();
                ProcessMessages();
                EvaluateColonyVariablesPirate(_Galaxy, num3);
                if (_ControlMilitaryFleets)
                {
                    MaintainShipGroups();
                    UpdateFleetLeadShips();
                    ReviewFleetPostures();
                }
                CheckMarketOrders();
                PirateAssignShipMissions(currentStarDate);
                if (_ControlAgentAssignment != 0)
                {
                    AssignSpecialMissions();
                }
                PerformIntelligenceMissions();
                PerformResearch(num3, allowResearchEvents: true);
            }
            if (num4 >= _IntermediateProcessingInterval)
            {
                if (_ControlMilitaryAttacks != 0)
                {
                    PirateTaskFleets();
                }
                ReviewFleetAdmiralBonuses();
                TaskResupplyShips();
                ReviewResearchStationBonuses();
                UpdateSystemExplorationStatus();
                CheckKnownPirateBases();
                PirateCollectIncomeFromControlledColonies(num4);
                if (this != _Galaxy.IndependentEmpire && DominantRace != null)
                {
                    CheckForCharacterAppearance();
                }
                ReviewCharacterLeaderChange(num4);
                ProcessLeaderChangeInfluence(num4);
                ReviewCharacterTraits();
                ReviewDemoralizingCharacters();
                ReviewCharacterLocations();
                if (_ControlDesigns)
                {
                    CreateNewDesigns(currentStarDate);
                }
                PirateReviewSystemThreats();
                _ColonizationTargets = PirateReviewColoniesToControl();
                _EmpireResourceTargets = PrioritizeEmpireResourceNeeds(includeLuxuryResources: false, 5, 1.0);
                _ResourceTargets = IdentifyResourceCentres(_Galaxy);
                IdentifyUnavailableLuxuryResources();
                PirateGenerateSellInfoOffers();
                if (this == _Galaxy.PlayerEmpire)
                {
                    UpdateSystemRefuellingStatus();
                    CheckForStrandedShips();
                }
                UpdateSystemFuelSourceStatus();
                ReviewPirateSystemInfluence();
                if (this == _Galaxy.PlayerEmpire)
                {
                    UpdateAchievements();
                }
            }
            if (num5 >= _LongProcessingInterval)
            {
                BaconEmpire.DoTaskPiratesLongInterval(this);
                MergeGalaxyMapsForSharedVisibilityEmpires();
                MergeKnownPirateBasesForSharedVisibilityEmpires();
                MaintainPirateSpaceportResourceLevels();
                PirateReviewColonyFacilities();
                ClearInvalidDiplomaticRelations();
                PirateReviewEmpireRelations(currentStarDate, num4);
                PirateProjectForces(currentStarDate);
                PayMaintenanceForBuiltObjects(num5);
                PayForTroops(num5);
                PayForPlanetaryFacilities(num5);
                ReviewMigrationTourism();
                if (_InitiateConstruction)
                {
                    ReviewLatestDesigns();
                    PirateDoConstruction();
                }
                PirateResetCivilianShipEmpireToIndependent();
                PiratesMakeAttackOffers(currentStarDate);
                PirateTradeItems();
                ClearExpiredDeclinedTasks();
            }
            if (num6 >= _HugeProcessingInterval)
            {
                PirateReviewRandomEvents();
                MaintainBaseResourceLevels();
                CleanupInvalidShips();
            }
        }

        private void MaintainBaseResourceLevels()
        {
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = BuiltObjects[i];
                if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Role == BuiltObjectRole.Base && (builtObject.ParentHabitat == null || builtObject.ParentHabitat.Empire == null || builtObject.ParentHabitat.Empire != builtObject.Empire) && builtObject.BuiltAt == null && builtObject.CargoSpace > 0)
                {
                    MaintainBaseResourceLevelsSingleBase(builtObject);
                }
            }
            for (int j = 0; j < PrivateBuiltObjects.Count; j++)
            {
                BuiltObject builtObject2 = PrivateBuiltObjects[j];
                if (builtObject2 != null && !builtObject2.HasBeenDestroyed && builtObject2.Role == BuiltObjectRole.Base && (builtObject2.ParentHabitat == null || builtObject2.ParentHabitat.Empire == null || builtObject2.ParentHabitat.Empire != builtObject2.Empire) && builtObject2.BuiltAt == null && builtObject2.CargoSpace > 0)
                {
                    MaintainBaseResourceLevelsSingleBase(builtObject2);
                }
            }
        }

        private void MaintainBaseResourceLevelsSingleBase(BuiltObject baseNotAtColony)
        {
            OrderList orders = _Galaxy.Orders.GetOrders(baseNotAtColony);
            for (int i = 0; i < _Galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; i++)
            {
                ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance[i];
                if (resourceDefinition != null)
                {
                    Resource resource = new Resource(resourceDefinition.ResourceID);
                    CheckAndOrderResource(baseNotAtColony, orders, resource);
                }
            }
        }

        private void CheckAndOrderResource(BuiltObject baseNotAtColony, OrderList baseOrders, Resource resource)
        {
            int amountToOrder = 0;
            int num = Galaxy.CalculateResourceLevelStockForBaseRetrofit(resource.ResourceID);
            if (!CheckResourceMeetsMinimumLevelBaseNotAtColony(resource, num, num, baseNotAtColony, baseOrders, out amountToOrder))
            {
                double num2 = (double)amountToOrder * _Galaxy.ResourceCurrentPrices[resource.ResourceID];
                if (num2 < GetPrivateFunds())
                {
                    CreateOrder(baseNotAtColony, resource, amountToOrder, isState: false, OrderType.RetrofitResourcesForBase);
                }
            }
        }

        private bool CheckResourceMeetsMinimumLevelBaseNotAtColony(Resource resource, int minimumResourceLevel, int maximumResourceLevel, BuiltObject baseNotAtColony, OrderList baseOrders, out int amountToOrder)
        {
            return BaconEmpire.CheckResourceMeetsMinimumLevelBaseNotAtColony(this, resource, minimumResourceLevel, maximumResourceLevel, baseNotAtColony, baseOrders, out amountToOrder);
        }

        private void CheckColoniesForPirateFacilitiesAndAttack()
        {
            BaconEmpire.CheckColoniesForPirateFacilitiesAndAttack(this);
        }

        private void PirateResetCivilianShipEmpireToIndependent()
        {
            if (PirateEmpireBaseHabitat == null || PrivateBuiltObjects == null)
            {
                return;
            }
            for (int i = 0; i < PrivateBuiltObjects.Count; i++)
            {
                BuiltObject builtObject = PrivateBuiltObjects[i];
                if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.PirateEmpireId == EmpireId && builtObject.Empire == this)
                {
                    switch (builtObject.SubRole)
                    {
                        case BuiltObjectSubRole.SmallFreighter:
                        case BuiltObjectSubRole.MediumFreighter:
                        case BuiltObjectSubRole.LargeFreighter:
                        case BuiltObjectSubRole.PassengerShip:
                        case BuiltObjectSubRole.GasMiningShip:
                        case BuiltObjectSubRole.MiningShip:
                            builtObject.Empire = _Galaxy.IndependentEmpire;
                            break;
                    }
                }
            }
        }

        private void PirateGenerateSellInfoOffers()
        {
            if (this == _Galaxy.PlayerEmpire)
            {
                return;
            }
            _ = _Galaxy.ColonyFillFactor;
            int num = Galaxy.Rnd.Next(0, PirateRelations.Count);
            for (int i = num; i < PirateRelations.Count; i++)
            {
                PirateRelation pirateRelation = PirateRelations[i];
                if (pirateRelation != null && pirateRelation.Type != 0 && pirateRelation.Evaluation > -10f && _Galaxy.GeneratePirateOffersForSingleEmpire(this, pirateRelation.OtherEmpire))
                {
                    return;
                }
            }
            for (int j = 0; j < Math.Min(num, PirateRelations.Count); j++)
            {
                PirateRelation pirateRelation2 = PirateRelations[j];
                if (pirateRelation2 != null && pirateRelation2.Type != 0 && pirateRelation2.Evaluation > -10f && _Galaxy.GeneratePirateOffersForSingleEmpire(this, pirateRelation2.OtherEmpire))
                {
                    break;
                }
            }
        }

        private void PirateAssignShipMissions(long starDate)
        {
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = BuiltObjects[i];
                builtObject.CurrentEscortForceAssigned = 0;
            }
            for (int j = 0; j < PrivateBuiltObjects.Count; j++)
            {
                BuiltObject builtObject2 = PrivateBuiltObjects[j];
                builtObject2.CurrentEscortForceAssigned = 0;
            }
            for (int k = 0; k < BuiltObjects.Count; k++)
            {
                BuiltObject builtObject3 = BuiltObjects[k];
                if (builtObject3.Mission != null && (builtObject3.Mission.Type == BuiltObjectMissionType.Escort || builtObject3.Mission.Type == BuiltObjectMissionType.Patrol) && builtObject3.Mission.TargetBuiltObject != null)
                {
                    BuiltObject targetBuiltObject = builtObject3.Mission.TargetBuiltObject;
                    targetBuiltObject.CurrentEscortForceAssigned += builtObject3.FirepowerRaw;
                }
            }
            for (int l = 0; l < BuiltObjects.Count; l++)
            {
                BuiltObject builtObject4 = BuiltObjects[l];
                if (builtObject4 != null && !builtObject4.HasBeenDestroyed)
                {
                    PirateAssignShipMission(builtObject4, starDate);
                }
            }
            for (int m = 0; m < PrivateBuiltObjects.Count; m++)
            {
                BuiltObject builtObject5 = PrivateBuiltObjects[m];
                if (builtObject5 != null && !builtObject5.HasBeenDestroyed)
                {
                    PirateAssignShipMission(builtObject5, starDate);
                }
            }
        }

        public void PirateAssignShipMission(BuiltObject ship, long starDate)
        {
            if (ship == null || ship.HasBeenDestroyed || ship.Role == BuiltObjectRole.Base || ship.TopSpeed <= 0 || ship.BuiltAt != null || !ship.IsAutoControlled || (ship.Mission != null && ship.Mission.Type != 0))
            {
                return;
            }
            if (ship.RetireForNextMission)
            {
                if (ship.ShipGroup != null)
                {
                    if (ship.ShipGroup.Mission == null || ship.ShipGroup.Mission.Type == BuiltObjectMissionType.Undefined || ship.ShipGroup.Mission.Priority == BuiltObjectMissionPriority.Undefined || ship.ShipGroup.Mission.Priority == BuiltObjectMissionPriority.Low)
                    {
                        ship.LeaveShipGroup();
                        if (AssignScrapMission(ship))
                        {
                            ship.RetireForNextMission = false;
                            return;
                        }
                    }
                }
                else if (AssignScrapMission(ship))
                {
                    ship.RetireForNextMission = false;
                    return;
                }
            }
            if (ship.RetrofitForNextMission)
            {
                if (ship.ShipGroup != null)
                {
                    if ((ship.ShipGroup.Mission == null || ship.ShipGroup.Mission.Type == BuiltObjectMissionType.Undefined || ship.ShipGroup.Mission.Priority == BuiltObjectMissionPriority.Undefined || ship.ShipGroup.Mission.Priority == BuiltObjectMissionPriority.Low) && AssignRetrofitMission(ship))
                    {
                        ship.RetrofitForNextMission = false;
                        return;
                    }
                }
                else if (AssignRetrofitMission(ship))
                {
                    ship.RetrofitForNextMission = false;
                    return;
                }
            }
            if (ship.RepairForNextMission)
            {
                if (ship.DamagedComponentCount > 0)
                {
                    if (AssignRepairMission(ship))
                    {
                        ship.RepairForNextMission = false;
                        return;
                    }
                }
                else
                {
                    ship.RepairForNextMission = false;
                }
            }
            if (ship.RefuelForNextMission)
            {
                bool flag = true;
                if (ship.ShipGroup != null)
                {
                    flag = false;
                    double num = ship.CurrentFuel / Math.Max(1.0, ship.FuelCapacity);
                    if (num < 0.05)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    ship.SetupRefuelling();
                    return;
                }
            }
            switch (ship.SubRole)
            {
                case BuiltObjectSubRole.ExplorationShip:
                    {
                        Point location = Point.Empty;
                        Habitat habitat10 = _Galaxy.FindNextHabitatToExplore(ship.Xpos, ship.Ypos, ship.ActualEmpire, ship, out location);
                        if (location != Point.Empty)
                        {
                            ship.AssignMission(BuiltObjectMissionType.Move, null, null, location.X, location.Y, BuiltObjectMissionPriority.Normal);
                        }
                        else if (habitat10 != null)
                        {
                            ship.AssignMission(BuiltObjectMissionType.Explore, habitat10, null, BuiltObjectMissionPriority.Normal);
                        }
                        if (habitat10 == null && location.IsEmpty && Galaxy.Rnd.Next(0, 10) == 1)
                        {
                            GalaxyLocation location2 = null;
                            Habitat habitat11 = _Galaxy.FindUnexploredRuinsOrLocations(ship.Xpos, ship.Ypos, ship.Empire, out location2);
                            if (habitat11 != null)
                            {
                                ship.AssignMission(BuiltObjectMissionType.Move, habitat11, null, BuiltObjectMissionPriority.Normal);
                            }
                            else if (location2 != null)
                            {
                                location2.ResolveLocationCenter(out var x, out var y);
                                ship.AssignMission(BuiltObjectMissionType.Move, null, null, x, y, BuiltObjectMissionPriority.Normal);
                            }
                        }
                        break;
                    }
                case BuiltObjectSubRole.Escort:
                case BuiltObjectSubRole.Frigate:
                case BuiltObjectSubRole.Destroyer:
                case BuiltObjectSubRole.Cruiser:
                case BuiltObjectSubRole.CapitalShip:
                case BuiltObjectSubRole.Carrier:
                    {
                        if (ship.ShipGroup != null)
                        {
                            break;
                        }
                        if (_ColonizationTargets != null && _ColonizationTargets.Count > 0)
                        {
                            double num2 = 2250000.0;
                            for (int i = 0; i < _ColonizationTargets.Count; i++)
                            {
                                Habitat habitat = _ColonizationTargets[i].Habitat;
                                if (habitat == null || habitat.HasBeenDestroyed)
                                {
                                    continue;
                                }
                                bool flag5 = true;
                                PirateColonyControl byFacilityControl = habitat.GetPirateControl().GetByFacilityControl();
                                habitat.GetPirateControl().GetByFacilityControl();
                                if (byFacilityControl != null)
                                {
                                    flag5 = false;
                                }
                                if (flag5)
                                {
                                    double num3 = _Galaxy.CalculateDistanceSquared(ship.Xpos, ship.Ypos, habitat.Xpos, habitat.Ypos);
                                    if (num3 < num2)
                                    {
                                        return;
                                    }
                                }
                            }
                            for (int j = 0; j < Colonies.Count; j++)
                            {
                                Habitat habitat2 = Colonies[j];
                                if (habitat2 == null || habitat2.HasBeenDestroyed)
                                {
                                    continue;
                                }
                                bool flag6 = true;
                                PirateColonyControl byFacilityControl2 = habitat2.GetPirateControl().GetByFacilityControl();
                                habitat2.GetPirateControl().GetByFacilityControl();
                                if (byFacilityControl2 != null)
                                {
                                    flag6 = false;
                                }
                                if (flag6)
                                {
                                    double num4 = _Galaxy.CalculateDistanceSquared(ship.Xpos, ship.Ypos, habitat2.Xpos, habitat2.Ypos);
                                    if (num4 < num2)
                                    {
                                        return;
                                    }
                                }
                            }
                            for (int k = 0; k < _ColonizationTargets.Count; k++)
                            {
                                Habitat habitat3 = _ColonizationTargets[k].Habitat;
                                if (habitat3 == null || habitat3.HasBeenDestroyed)
                                {
                                    continue;
                                }
                                SystemVisibility systemVisibility = SystemVisibility[habitat3.SystemIndex];
                                if (systemVisibility == null || (systemVisibility.Status == SystemVisibilityStatus.Visible && (systemVisibility.Threats == null || systemVisibility.Threats.Count <= 0)))
                                {
                                    continue;
                                }
                                bool flag7 = false;
                                if (systemVisibility.Status != SystemVisibilityStatus.Visible)
                                {
                                    flag7 = true;
                                }
                                else if (systemVisibility.EmpireStrength <= 0)
                                {
                                    flag7 = true;
                                }
                                else
                                {
                                    BuiltObjectList shipsAtHabitatNotLeaving = BuiltObjects.GetShipsAtHabitatNotLeaving(habitat3, 1500.0);
                                    shipsAtHabitatNotLeaving.Remove(ship);
                                    int num5 = shipsAtHabitatNotLeaving.TotalMobileMilitaryFirepower(this);
                                    if (num5 <= 0)
                                    {
                                        flag7 = true;
                                    }
                                    else
                                    {
                                        for (int l = 0; l < systemVisibility.Threats.Count; l++)
                                        {
                                            if (systemVisibility.Threats[l].Role != BuiltObjectRole.Base && systemVisibility.Threats[l].FirepowerRaw > 0)
                                            {
                                                flag7 = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (!flag7 || !ship.WithinFuelRange(habitat3.Xpos, habitat3.Ypos, 0.1))
                                {
                                    continue;
                                }
                                long starDate2 = _Galaxy.CurrentStarDate + (long)(0.5 * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
                                EmpireActivity firstByTargetAndType = PirateMissions.GetFirstByTargetAndType(habitat3, EmpireActivityType.Defend);
                                if (firstByTargetAndType == null && ship.AssaultStrength > 0)
                                {
                                    PirateColonyControl byFaction = habitat3.GetPirateControl().GetByFaction(this);
                                    if (byFaction == null || byFaction.ControlLevel < 0.5f)
                                    {
                                        if (habitat3.GetPirateControl().CheckEmpireHasRelationTypeWithAny(_Galaxy, this, PirateRelationType.Protection))
                                        {
                                            ship.AssignMission(BuiltObjectMissionType.MoveAndWait, habitat3, null, -2000000001.0, -2000000001.0, starDate2, BuiltObjectMissionPriority.Normal, allowReprocessing: false);
                                            return;
                                        }
                                        bool flag8 = true;
                                        if (habitat3.Owner != null && habitat3.Owner != this)
                                        {
                                            PirateRelation pirateRelation = ObtainPirateRelation(habitat3.Owner);
                                            if (pirateRelation.Type == PirateRelationType.Protection)
                                            {
                                                flag8 = false;
                                            }
                                        }
                                        if (flag8)
                                        {
                                            ship.AssignMission(BuiltObjectMissionType.Raid, habitat3, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                                        }
                                        else
                                        {
                                            ship.AssignMission(BuiltObjectMissionType.MoveAndWait, habitat3, null, -2000000001.0, -2000000001.0, starDate2, BuiltObjectMissionPriority.Normal, allowReprocessing: false);
                                        }
                                    }
                                    else
                                    {
                                        ship.AssignMission(BuiltObjectMissionType.MoveAndWait, habitat3, null, -2000000001.0, -2000000001.0, starDate2, BuiltObjectMissionPriority.Normal, allowReprocessing: false);
                                    }
                                }
                                else
                                {
                                    ship.AssignMission(BuiltObjectMissionType.MoveAndWait, habitat3, null, -2000000001.0, -2000000001.0, starDate2, BuiltObjectMissionPriority.Normal, allowReprocessing: false);
                                }
                                return;
                            }
                        }
                        if (Colonies != null && Colonies.Count > 0)
                        {
                            for (int m = 0; m < Colonies.Count; m++)
                            {
                                Habitat habitat4 = Colonies[m];
                                if (habitat4 == null || habitat4.HasBeenDestroyed)
                                {
                                    continue;
                                }
                                SystemVisibility systemVisibility2 = SystemVisibility[habitat4.SystemIndex];
                                if (systemVisibility2 == null)
                                {
                                    continue;
                                }
                                bool flag9 = false;
                                if (systemVisibility2.Status != SystemVisibilityStatus.Visible)
                                {
                                    flag9 = true;
                                }
                                else if (systemVisibility2.EmpireStrength <= 0)
                                {
                                    flag9 = true;
                                }
                                else
                                {
                                    BuiltObjectList shipsAtHabitatNotLeaving2 = BuiltObjects.GetShipsAtHabitatNotLeaving(habitat4, 1500.0);
                                    shipsAtHabitatNotLeaving2.Remove(ship);
                                    int num6 = shipsAtHabitatNotLeaving2.TotalMobileMilitaryFirepower(this);
                                    if (num6 <= 0)
                                    {
                                        flag9 = true;
                                    }
                                    else if (systemVisibility2.Threats != null && systemVisibility2.Threats.Count > 0)
                                    {
                                        for (int n = 0; n < systemVisibility2.Threats.Count; n++)
                                        {
                                            if (systemVisibility2.Threats[n].Role != BuiltObjectRole.Base && systemVisibility2.Threats[n].FirepowerRaw > 0)
                                            {
                                                flag9 = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (flag9 && ship.WithinFuelRange(habitat4.Xpos, habitat4.Ypos, 0.1))
                                {
                                    long starDate3 = _Galaxy.CurrentStarDate + (long)(0.5 * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
                                    ship.AssignMission(BuiltObjectMissionType.MoveAndWait, habitat4, null, -2000000001.0, -2000000001.0, starDate3, BuiltObjectMissionPriority.Normal, allowReprocessing: false);
                                    return;
                                }
                            }
                        }
                        if (Galaxy.Rnd.Next(0, 2) == 1 && PirateEmpireBaseHabitat != null && PirateRelations != null)
                        {
                            int num7 = Galaxy.Rnd.Next(0, PirateRelations.Count);
                            for (int num8 = num7; num8 < PirateRelations.Count; num8++)
                            {
                                PirateRelation pirateRelation2 = PirateRelations[num8];
                                if (pirateRelation2 != null && pirateRelation2.Type == PirateRelationType.Protection && pirateRelation2.OtherEmpire != null && pirateRelation2.Evaluation >= 5f && !pirateRelation2.OtherEmpire.CheckEmpireHasHyperDriveTech(pirateRelation2.OtherEmpire))
                                {
                                    Habitat habitat5 = _Galaxy.FastFindNearestColony(PirateEmpireBaseHabitat.Xpos, PirateEmpireBaseHabitat.Ypos, pirateRelation2.OtherEmpire, 0);
                                    if (habitat5 != null && ship.WithinFuelRange(habitat5.Xpos, habitat5.Ypos, 0.1))
                                    {
                                        ship.AssignMission(BuiltObjectMissionType.Patrol, habitat5, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                                        return;
                                    }
                                }
                            }
                            for (int num9 = 0; num9 < num7; num9++)
                            {
                                PirateRelation pirateRelation3 = PirateRelations[num9];
                                if (pirateRelation3 != null && pirateRelation3.Type == PirateRelationType.Protection && pirateRelation3.OtherEmpire != null && pirateRelation3.Evaluation >= 5f && !pirateRelation3.OtherEmpire.CheckEmpireHasHyperDriveTech(pirateRelation3.OtherEmpire))
                                {
                                    Habitat habitat6 = _Galaxy.FastFindNearestColony(PirateEmpireBaseHabitat.Xpos, PirateEmpireBaseHabitat.Ypos, pirateRelation3.OtherEmpire, 0);
                                    if (habitat6 != null && ship.WithinFuelRange(habitat6.Xpos, habitat6.Ypos, 0.1))
                                    {
                                        ship.AssignMission(BuiltObjectMissionType.Patrol, habitat6, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                                        return;
                                    }
                                }
                            }
                        }
                        BuiltObject builtObject = _Galaxy.FindNearestKnownBaseForPirateAttack(this, ship.Xpos, ship.Ypos);
                        if (builtObject != null && ship.WithinFuelRange(builtObject.Xpos, builtObject.Ypos, 0.1))
                        {
                            int num10 = builtObject.CalculateOverallStrengthFactor();
                            if (CheckSystemVisible(builtObject.NearestSystemStar))
                            {
                                num10 = CalculateDefendingStrength(builtObject);
                            }
                            if (ship.CalculateOverallStrengthFactor() >= num10)
                            {
                                int maxValue = 3;
                                if (PiratePlayStyle == PiratePlayStyle.Pirate)
                                {
                                    maxValue = 5;
                                }
                                else if (PiratePlayStyle == PiratePlayStyle.Mercenary)
                                {
                                    maxValue = 4;
                                }
                                else if (PiratePlayStyle == PiratePlayStyle.Legendary)
                                {
                                    maxValue = 5;
                                }
                                switch (Galaxy.Rnd.Next(0, maxValue))
                                {
                                    case 0:
                                    case 1:
                                        {
                                            BuiltObjectMissionType missionType = DetermineDestroyOrCaptureTarget(ship, builtObject, attackingAsGroup: false);
                                            ship.AssignMission(missionType, builtObject, null, BuiltObjectMissionPriority.Normal);
                                            break;
                                        }
                                    default:
                                        {
                                            if (builtObject.RaidCountdown <= 0)
                                            {
                                                ship.AssignMission(BuiltObjectMissionType.Raid, builtObject, null, BuiltObjectMissionPriority.Normal);
                                                break;
                                            }
                                            BuiltObjectMissionType missionType = DetermineDestroyOrCaptureTarget(ship, builtObject, attackingAsGroup: false);
                                            ship.AssignMission(missionType, builtObject, null, BuiltObjectMissionPriority.Normal);
                                            break;
                                        }
                                }
                                break;
                            }
                        }
                        BuiltObject builtObject2 = _Galaxy.IdentifyPirateBase(this);
                        Habitat habitat7 = null;
                        BuiltObject builtObject3 = null;
                        if (this != _Galaxy.PlayerEmpire && builtObject2 != null)
                        {
                            switch (Galaxy.Rnd.Next(0, 3))
                            {
                                case 0:
                                    builtObject3 = _Galaxy.FindNearestBuiltObject((int)builtObject2.Xpos, (int)builtObject2.Ypos, BuiltObjectSubRole.MiningStation, includeSecondaryEmpires: false);
                                    break;
                                case 1:
                                    builtObject3 = _Galaxy.FindNearestBuiltObject((int)builtObject2.Xpos, (int)builtObject2.Ypos, BuiltObjectSubRole.GasMiningStation, includeSecondaryEmpires: false);
                                    break;
                                case 2:
                                    switch (Galaxy.Rnd.Next(0, 5))
                                    {
                                        case 0:
                                            builtObject3 = _Galaxy.FindNearestBuiltObject((int)builtObject2.Xpos, (int)builtObject2.Ypos, BuiltObjectSubRole.WeaponsResearchStation, includeSecondaryEmpires: false);
                                            break;
                                        case 1:
                                            builtObject3 = _Galaxy.FindNearestBuiltObject((int)builtObject2.Xpos, (int)builtObject2.Ypos, BuiltObjectSubRole.EnergyResearchStation, includeSecondaryEmpires: false);
                                            break;
                                        case 2:
                                            builtObject3 = _Galaxy.FindNearestBuiltObject((int)builtObject2.Xpos, (int)builtObject2.Ypos, BuiltObjectSubRole.HighTechResearchStation, includeSecondaryEmpires: false);
                                            break;
                                        case 3:
                                            builtObject3 = _Galaxy.FindNearestBuiltObject((int)builtObject2.Xpos, (int)builtObject2.Ypos, BuiltObjectSubRole.ResortBase, includeSecondaryEmpires: false);
                                            break;
                                        case 4:
                                            builtObject3 = _Galaxy.FindNearestBuiltObject((int)builtObject2.Xpos, (int)builtObject2.Ypos, BuiltObjectSubRole.MonitoringStation, includeSecondaryEmpires: false);
                                            break;
                                    }
                                    break;
                            }
                        }
                        if (builtObject3 != null && builtObject3.NearestSystemStar != null)
                        {
                            double num11 = _Galaxy.CalculateDistance(builtObject3.Xpos, builtObject3.Ypos, ship.Xpos, ship.Ypos);
                            habitat7 = ((!(num11 < (double)Galaxy.SectorSize)) ? _Galaxy.FastFindNearestUncolonizedOwnedSystem(ship.Xpos, ship.Ypos) : builtObject3.NearestSystemStar);
                        }
                        else
                        {
                            habitat7 = _Galaxy.FastFindNearestUncolonizedOwnedSystem(ship.Xpos, ship.Ypos);
                        }
                        if (habitat7 != null)
                        {
                            SystemVisibilityStatus systemVisibilityStatus = CheckSystemVisibilityStatus(habitat7.SystemIndex);
                            if (systemVisibilityStatus == SystemVisibilityStatus.Explored)
                            {
                                SystemInfo systemInfo = _Galaxy.Systems[habitat7.SystemIndex];
                                if (systemInfo != null && systemInfo.Habitats != null && systemInfo.Habitats.Count > 0)
                                {
                                    int index = Galaxy.Rnd.Next(0, systemInfo.Habitats.Count);
                                    Habitat habitat8 = systemInfo.Habitats[index];
                                    if (habitat8 != null && ship.WithinFuelRange(habitat8.Xpos, habitat8.Ypos, 0.1))
                                    {
                                        ship.AssignMission(BuiltObjectMissionType.Move, habitat8, null, BuiltObjectMissionPriority.Normal);
                                        break;
                                    }
                                }
                                else if (ship.WithinFuelRange(habitat7.Xpos, habitat7.Ypos, 0.1))
                                {
                                    ship.AssignMission(BuiltObjectMissionType.Move, habitat7, null, BuiltObjectMissionPriority.Normal);
                                    break;
                                }
                            }
                            else
                            {
                                habitat7 = _Galaxy.FastFindNearestUnexploredSystem(ship.Xpos, ship.Ypos, this);
                                if (habitat7 != null && ship.WithinFuelRange(habitat7.Xpos, habitat7.Ypos, 0.1))
                                {
                                    ship.AssignMission(BuiltObjectMissionType.Move, habitat7, null, BuiltObjectMissionPriority.Normal);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            habitat7 = _Galaxy.FastFindNearestUnexploredSystem(ship.Xpos, ship.Ypos, this);
                            if (habitat7 != null && ship.WithinFuelRange(habitat7.Xpos, habitat7.Ypos, 0.1))
                            {
                                ship.AssignMission(BuiltObjectMissionType.Move, habitat7, null, BuiltObjectMissionPriority.Normal);
                                break;
                            }
                        }
                        if (builtObject2 != null && builtObject2.CurrentEscortForceAssigned <= 0 && ship.WithinFuelRange(builtObject2.Xpos, builtObject2.Ypos, 0.1))
                        {
                            ship.AssignMission(BuiltObjectMissionType.Patrol, builtObject2, null, BuiltObjectMissionPriority.Normal);
                            builtObject2.CurrentEscortForceAssigned += ship.FirepowerRaw;
                        }
                        break;
                    }
                case BuiltObjectSubRole.SmallFreighter:
                case BuiltObjectSubRole.MediumFreighter:
                case BuiltObjectSubRole.LargeFreighter:
                    {
                        if (Galaxy.Rnd.Next(0, 2) == 1)
                        {
                            bool flag10 = false;
                            int num12 = Galaxy.Rnd.Next(0, MiningStations.Count);
                            ResourceList empireDeficientResources = IdentifyDeficientEmpireResources();
                            for (int num13 = num12; num13 < MiningStations.Count; num13++)
                            {
                                if (CheckMiningStationForResourceClearance(ship, MiningStations[num13], empireDeficientResources))
                                {
                                    flag10 = true;
                                    break;
                                }
                            }
                            if (!flag10)
                            {
                                for (int num14 = 0; num14 < num12; num14++)
                                {
                                    if (CheckMiningStationForResourceClearance(ship, MiningStations[num14], empireDeficientResources))
                                    {
                                        flag10 = true;
                                        break;
                                    }
                                }
                            }
                        }
                        BuiltObject builtObject4 = _Galaxy.FastFindNearestSpacePort((int)ship.Xpos, (int)ship.Ypos, ship.ActualEmpire);
                        if (builtObject4 != null)
                        {
                            double num15 = _Galaxy.CalculateDistance(ship.Xpos, ship.Ypos, builtObject4.Xpos, builtObject4.Ypos);
                            if (num15 > (double)Galaxy.SectorSize * 2.5)
                            {
                                ship.AssignMission(BuiltObjectMissionType.Move, builtObject4, null, BuiltObjectMissionPriority.Normal);
                                break;
                            }
                        }
                        Habitat habitat9 = _Galaxy.FastFindNearestColony((int)ship.Xpos, (int)ship.Ypos, ship.ActualEmpire, 0);
                        if (habitat9 != null)
                        {
                            double num16 = _Galaxy.CalculateDistance(ship.Xpos, ship.Ypos, habitat9.Xpos, habitat9.Ypos);
                            if (num16 > (double)Galaxy.SectorSize * 2.5)
                            {
                                ship.AssignMission(BuiltObjectMissionType.Move, habitat9, null, BuiltObjectMissionPriority.Normal);
                            }
                        }
                        break;
                    }
                case BuiltObjectSubRole.ConstructionShip:
                    {
                        double stateMoney = _StateMoney;
                        stateMoney -= CalculateCashReservesForNewPirateFacilities();
                        int num17 = BuiltObjects.CountSpaceports();
                        int num18 = 1 + BuiltObjects.Count / 20;
                        if (num17 < num18)
                        {
                            Design design = _LatestDesigns.FindNewestCanBuild(BuiltObjectSubRole.SmallSpacePort, this);
                            if (design != null)
                            {
                                double num19 = design.CalculateCurrentPurchasePrice(_Galaxy);
                                if (num19 < stateMoney)
                                {
                                    Habitat habitat12 = _Galaxy.IdentifyPirateNewHomeLocation(this);
                                    if (habitat12 != null)
                                    {
                                        _Galaxy.SelectRelativeHabitatSurfacePoint(habitat12, out var x2, out var y2);
                                        ship.AssignMission(BuiltObjectMissionType.Build, habitat12, null, design, x2, y2, BuiltObjectMissionPriority.Normal);
                                        if (num17 == 0)
                                        {
                                            PirateEmpireBaseHabitat = habitat12;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        GalaxyLocation galaxyLocation = CheckWhetherAtLocation(ship.Xpos, ship.Ypos);
                        if ((galaxyLocation != null && galaxyLocation.Type == GalaxyLocationType.DebrisField) || (Galaxy.Rnd.Next(0, 3) == 1 && ConstructionShips != null && ConstructionShips.Count > 1))
                        {
                            if (galaxyLocation == null)
                            {
                                for (int num20 = 0; num20 < KnownGalaxyLocations.Count; num20++)
                                {
                                    if (KnownGalaxyLocations[num20].Type == GalaxyLocationType.DebrisField)
                                    {
                                        galaxyLocation = KnownGalaxyLocations[num20];
                                        break;
                                    }
                                }
                            }
                            if (galaxyLocation != null)
                            {
                                BuiltObject builtObject5 = SelectBestSalvageableShip(galaxyLocation);
                                if (builtObject5 != null && ship.WithinFuelRange(builtObject5.Xpos, builtObject5.Ypos, 0.1))
                                {
                                    ship.AssignMission(BuiltObjectMissionType.Build, null, builtObject5, builtObject5.Xpos, builtObject5.Ypos, BuiltObjectMissionPriority.High);
                                    break;
                                }
                            }
                        }
                        GalaxyLocationList galaxyLocationList = KnownGalaxyLocations.FindLocations(GalaxyLocationType.PlanetDestroyer);
                        if (galaxyLocationList.Count > 0)
                        {
                            for (int num21 = 0; num21 < galaxyLocationList.Count; num21++)
                            {
                                BuiltObject relatedBuiltObject = galaxyLocationList[num21].RelatedBuiltObject;
                                if (relatedBuiltObject == null || relatedBuiltObject.UnbuiltComponentCount <= 0 || relatedBuiltObject.BuiltAt != null || relatedBuiltObject.Empire != null || relatedBuiltObject.HasBeenDestroyed)
                                {
                                    continue;
                                }
                                bool flag11 = false;
                                for (int num22 = 0; num22 < ConstructionShips.Count; num22++)
                                {
                                    BuiltObject builtObject6 = ConstructionShips[num22];
                                    if (builtObject6.Mission != null && (builtObject6.Mission.Type == BuiltObjectMissionType.Build || builtObject6.Mission.Type == BuiltObjectMissionType.BuildRepair || builtObject6.Mission.Type == BuiltObjectMissionType.Repair) && builtObject6.Mission.SecondaryTargetBuiltObject == relatedBuiltObject)
                                    {
                                        flag11 = true;
                                        break;
                                    }
                                }
                                if (!flag11 && Galaxy.Rnd.Next(0, 3) == 1 && ConstructionShips != null && ConstructionShips.Count > 1 && ship.WithinFuelRange(relatedBuiltObject.Xpos, relatedBuiltObject.Ypos, 0.1))
                                {
                                    ship.AssignMission(BuiltObjectMissionType.Build, null, relatedBuiltObject, relatedBuiltObject.Xpos, relatedBuiltObject.Ypos, BuiltObjectMissionPriority.High);
                                    return;
                                }
                            }
                        }
                        BuiltObjectList builtObjectList = new BuiltObjectList();
                        builtObjectList.AddRange(BuiltObjects);
                        builtObjectList.AddRange(PrivateBuiltObjects);
                        if (Galaxy.Rnd.Next(0, 3) == 1)
                        {
                            int num23 = Galaxy.Rnd.Next(0, builtObjectList.Count);
                            for (int num24 = num23; num24 < builtObjectList.Count; num24++)
                            {
                                BuiltObject builtObject7 = builtObjectList[num24];
                                if (builtObject7 != null && builtObject7.ActualEmpire == this && DetermineWhetherShouldRepair(ship, builtObject7) && ship.WithinFuelRange(builtObject7.Xpos, builtObject7.Ypos, 0.1))
                                {
                                    ship.AssignMission(BuiltObjectMissionType.BuildRepair, null, builtObject7, BuiltObjectMissionPriority.Normal);
                                    return;
                                }
                            }
                            for (int num25 = 0; num25 < num23; num25++)
                            {
                                BuiltObject builtObject8 = builtObjectList[num25];
                                if (builtObject8 != null && builtObject8.ActualEmpire == this && DetermineWhetherShouldRepair(ship, builtObject8) && ship.WithinFuelRange(builtObject8.Xpos, builtObject8.Ypos, 0.1))
                                {
                                    ship.AssignMission(BuiltObjectMissionType.BuildRepair, null, builtObject8, BuiltObjectMissionPriority.Normal);
                                    return;
                                }
                            }
                        }
                        HabitatList habitatList = DetermineHabitatsBeingMinedIncludingBuildingMiningStations(includeMiningShips: false);
                        if (_ResourceTargets != null && _ResourceTargets.Count > 0)
                        {
                            double num26 = CalculateAccurateAnnualCashflow();
                            if (num26 > 0.0 && StateMoney > 0.0 && BuildStrategicResourceSupply(ship, habitatList))
                            {
                                break;
                            }
                        }
                        if (_EmpireResourceTargets != null && _EmpireResourceTargets.Count > 0)
                        {
                            double num27 = CalculateAccurateAnnualCashflow();
                            int num28 = 0;
                            Design design2 = null;
                            int iterationCount = 0;
                            while (Galaxy.ConditionCheckLimit(design2 == null && num28 < _EmpireResourceTargets.Count, 1500, ref iterationCount))
                            {
                                HabitatPrioritization habitatPrioritization = _EmpireResourceTargets[num28];
                                if (habitatPrioritization != null)
                                {
                                    Habitat habitat13 = habitatPrioritization.Habitat;
                                    if (habitat13 != null && !habitatList.Contains(habitat13) && (habitat13.Empire == null || habitat13.Empire == _Galaxy.IndependentEmpire))
                                    {
                                        if (habitat13.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            design2 = Designs.FindNewestCanBuild(BuiltObjectSubRole.GasMiningStation);
                                        }
                                        if (habitat13.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            design2 = Designs.FindNewestCanBuild(BuiltObjectSubRole.MiningStation);
                                        }
                                        if (design2 == null && habitat13.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            design2 = Designs.FindNewestCanBuild(BuiltObjectSubRole.MiningStation);
                                        }
                                        if (design2 != null)
                                        {
                                            double num29 = design2.CalculateMaintenanceCosts(_Galaxy, this);
                                            double num30 = design2.CalculateCurrentPurchasePrice(_Galaxy);
                                            if (StateMoney > num30 && num27 > num29 && habitatPrioritization.Priority > Galaxy.MiningStationResourceThreshhold && !CheckNearPirateBase(habitatPrioritization.Habitat, habitatPrioritization.Habitat.Xpos, habitatPrioritization.Habitat.Ypos, this))
                                            {
                                                BuiltObject builtObject9 = _Galaxy.DetermineMiningStationAtHabitat(habitat13);
                                                if (builtObject9 == null)
                                                {
                                                    _Galaxy.SelectRelativeHabitatSurfacePoint(habitat13, out var x3, out var y3);
                                                    BuiltObject builtObject10 = _Galaxy.FindNearestBuiltObject((int)(habitat13.Xpos + x3), (int)(habitat13.Ypos + y3), BuiltObjectRole.Base);
                                                    double num31 = double.MaxValue;
                                                    if (builtObject10 != null)
                                                    {
                                                        num31 = _Galaxy.CalculateDistance(habitat13.Xpos + x3, habitat13.Ypos + y3, builtObject10.Xpos, builtObject10.Ypos);
                                                    }
                                                    int num32 = 0;
                                                    while (num31 < (double)Galaxy.MinimumDistanceBetweenBases)
                                                    {
                                                        _Galaxy.SelectRelativeHabitatSurfacePoint(habitat13, out x3, out y3);
                                                        builtObject10 = _Galaxy.FindNearestBuiltObject((int)(habitat13.Xpos + x3), (int)(habitat13.Ypos + y3), BuiltObjectRole.Base);
                                                        num31 = _Galaxy.CalculateDistance(habitat13.Xpos + x3, habitat13.Ypos + y3, builtObject10.Xpos, builtObject10.Ypos);
                                                        num32++;
                                                        if (num32 > 5)
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    ship.AssignMission(BuiltObjectMissionType.Build, habitat13, null, design2, x3, y3, BuiltObjectMissionPriority.Normal);
                                                    habitatList.Add(habitat13);
                                                    _EmpireResourceTargets.RemoveAt(num28);
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                }
                                num28++;
                                design2 = null;
                            }
                        }
                        if (_ResortBaseBuildLocations == null || _ResortBaseBuildLocations.Count <= 0 || !Policy.EngageInTourism || Galaxy.Rnd.Next(0, 2) != 1)
                        {
                            break;
                        }
                        int num33 = Math.Min(20, 1 + Colonies.Count / 6);
                        num33 = (int)((double)num33 * Policy.TourismPriority);
                        if (ResortBases.Count >= num33)
                        {
                            break;
                        }
                        Design design3 = _Designs.FindNewestCanBuild(BuiltObjectSubRole.ResortBase);
                        if (design3 != null)
                        {
                            double num34 = CalculateSupportCost(design3);
                            double num35 = design3.CalculateCurrentPurchasePrice(_Galaxy);
                            if (num35 <= stateMoney && num34 <= CalculateSpareAnnualRevenueComplete() && !AssignBuildResortBaseMissionToBuiltObject(ship, design3))
                            {
                            }
                        }
                        break;
                    }
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                    {
                        bool flag12 = CheckShipCanSurviveStorms(ship);
                        List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
                        list.Add(BuiltObjectSubRole.MiningShip);
                        list.Add(BuiltObjectSubRole.GasMiningShip);
                        List<BuiltObjectSubRole> subRoles = list;
                        if (_EmpireResourceTargets != null && _EmpireResourceTargets.Count > 0)
                        {
                            bool flag13 = false;
                            int num36 = 0;
                            for (int iterationCount2 = 0; Galaxy.ConditionCheckLimit(!flag13 && num36 < _EmpireResourceTargets.Count, 1500, ref iterationCount2); num36++)
                            {
                                _ = _EmpireResourceTargets[num36];
                                if (!ship.IsResourceExtractor)
                                {
                                    continue;
                                }
                                Habitat habitat14 = _EmpireResourceTargets[num36].Habitat;
                                if (!flag12 && _Galaxy.CheckInStorm(habitat14.Xpos, habitat14.Ypos))
                                {
                                    continue;
                                }
                                switch (habitat14.Type)
                                {
                                    case HabitatType.BarrenRock:
                                        if (ship.ExtractionMine > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionGas > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag13 = true;
                                        }
                                        break;
                                    case HabitatType.GasGiant:
                                    case HabitatType.FrozenGasGiant:
                                    case HabitatType.Hydrogen:
                                    case HabitatType.Helium:
                                    case HabitatType.Argon:
                                    case HabitatType.Ammonia:
                                    case HabitatType.CarbonDioxide:
                                    case HabitatType.Oxygen:
                                    case HabitatType.NitrogenOxygen:
                                    case HabitatType.Chlorine:
                                        if (ship.ExtractionGas > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionMine > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag13 = true;
                                        }
                                        break;
                                    case HabitatType.Volcanic:
                                        if (ship.ExtractionMine > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionGas > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag13 = true;
                                        }
                                        break;
                                    case HabitatType.Continental:
                                        if (ship.ExtractionMine > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionGas > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag13 = true;
                                        }
                                        break;
                                    case HabitatType.Ice:
                                        if (ship.ExtractionGas > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionMine > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag13 = true;
                                        }
                                        break;
                                    case HabitatType.MarshySwamp:
                                        if (ship.ExtractionMine > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionGas > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag13 = true;
                                        }
                                        break;
                                    case HabitatType.Ocean:
                                        if (ship.ExtractionMine > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionGas > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag13 = true;
                                        }
                                        break;
                                    case HabitatType.Desert:
                                        if (ship.ExtractionMine > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Mineral))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionGas > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Gas))
                                        {
                                            flag13 = true;
                                        }
                                        else if (ship.ExtractionLuxury > 0 && habitat14.Resources.ContainsGroup(ResourceGroup.Luxury))
                                        {
                                            flag13 = true;
                                        }
                                        break;
                                }
                                if (!flag13)
                                {
                                    continue;
                                }
                                if (ship.WithinFuelRangeAndRefuel(habitat14.Xpos, habitat14.Ypos, 0.0))
                                {
                                    int num37 = PrivateBuiltObjects.CountBuiltObjectsWithTargetHabitat(habitat14, subRoles);
                                    if (num37 < 3)
                                    {
                                        ship.AssignMission(BuiltObjectMissionType.ExtractResources, habitat14, null, BuiltObjectMissionPriority.Normal);
                                        _EmpireResourceTargets.RemoveAt(num36);
                                    }
                                }
                                else
                                {
                                    flag13 = false;
                                }
                            }
                        }
                        if ((ship.Mission != null && ship.Mission.Type != 0) || _ResourceTargets == null || _ResourceTargets.Count <= 0)
                        {
                            break;
                        }
                        bool flag14 = false;
                        int num38 = 0;
                        for (int iterationCount3 = 0; Galaxy.ConditionCheckLimit(!flag14 && num38 < _ResourceTargets.Count, 1000, ref iterationCount3); num38++)
                        {
                            HabitatPrioritization habitatPrioritization2 = _ResourceTargets[num38];
                            if (!ship.IsResourceExtractor || CheckNearPirateBase(habitatPrioritization2.Habitat, habitatPrioritization2.Habitat.Xpos, habitatPrioritization2.Habitat.Ypos) || (!flag12 && _Galaxy.CheckInStorm(habitatPrioritization2.Habitat.Xpos, habitatPrioritization2.Habitat.Ypos)))
                            {
                                continue;
                            }
                            Habitat habitat15 = _ResourceTargets[num38].Habitat;
                            if (habitat15 == null || _Galaxy.CheckAlreadyHaveMiningStationAtHabitat(habitat15, this))
                            {
                                continue;
                            }
                            switch (habitat15.Type)
                            {
                                case HabitatType.BarrenRock:
                                    if (ship.ExtractionMine > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionGas > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag14 = true;
                                    }
                                    break;
                                case HabitatType.GasGiant:
                                case HabitatType.FrozenGasGiant:
                                case HabitatType.Hydrogen:
                                case HabitatType.Helium:
                                case HabitatType.Argon:
                                case HabitatType.Ammonia:
                                case HabitatType.CarbonDioxide:
                                case HabitatType.Oxygen:
                                case HabitatType.NitrogenOxygen:
                                case HabitatType.Chlorine:
                                    if (ship.ExtractionGas > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionMine > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag14 = true;
                                    }
                                    break;
                                case HabitatType.Volcanic:
                                    if (ship.ExtractionMine > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionGas > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag14 = true;
                                    }
                                    break;
                                case HabitatType.Continental:
                                    if (ship.ExtractionMine > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionGas > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag14 = true;
                                    }
                                    break;
                                case HabitatType.Ice:
                                    if (ship.ExtractionGas > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionMine > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag14 = true;
                                    }
                                    break;
                                case HabitatType.MarshySwamp:
                                    if (ship.ExtractionMine > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionGas > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag14 = true;
                                    }
                                    break;
                                case HabitatType.Ocean:
                                    if (ship.ExtractionMine > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionGas > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag14 = true;
                                    }
                                    break;
                                case HabitatType.Desert:
                                    if (ship.ExtractionMine > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Mineral))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionGas > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Gas))
                                    {
                                        flag14 = true;
                                    }
                                    else if (ship.ExtractionLuxury > 0 && habitat15.Resources.ContainsGroup(ResourceGroup.Luxury))
                                    {
                                        flag14 = true;
                                    }
                                    break;
                            }
                            if (!flag14)
                            {
                                continue;
                            }
                            if (ship.WithinFuelRangeAndRefuel(habitat15.Xpos, habitat15.Ypos, 0.0))
                            {
                                int num39 = PrivateBuiltObjects.CountBuiltObjectsWithTargetHabitat(habitat15, subRoles);
                                if (num39 < 3)
                                {
                                    ship.AssignMission(BuiltObjectMissionType.ExtractResources, habitat15, null, BuiltObjectMissionPriority.Normal);
                                }
                            }
                            else
                            {
                                flag14 = false;
                            }
                        }
                        break;
                    }
                case BuiltObjectSubRole.PassengerShip:
                    {
                        bool flag2 = false;
                        bool flag3 = false;
                        if (_MigrationDestinations != null && _MigrationDestinations.Count > 0 && _MigrationSources.Count > 0)
                        {
                            flag2 = true;
                        }
                        if (_TourismDestinations != null && _TourismSources != null && _TourismDestinations.Count > 0 && _TourismSources.Count > 0 && Policy.EngageInTourism)
                        {
                            flag3 = true;
                        }
                        if ((!flag2 || (flag3 && Galaxy.Rnd.Next(0, 2) != 1) || !AssignMigrationMissionToBuiltObject(ship)) && flag3)
                        {
                            bool flag4 = false;
                            if ((!flag2 || Galaxy.Rnd.Next(0, 3) > 0) && AssignTourismMissionToBuiltObject(ship))
                            {
                                flag4 = true;
                            }
                            else if (!flag4 && flag2)
                            {
                                AssignMigrationMissionToBuiltObject(ship);
                            }
                        }
                        break;
                    }
                case BuiltObjectSubRole.TroopTransport:
                case BuiltObjectSubRole.ResupplyShip:
                case BuiltObjectSubRole.ColonyShip:
                    break;
            }
        }
    }
}
