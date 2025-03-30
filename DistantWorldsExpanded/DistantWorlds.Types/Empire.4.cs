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
        public string ResolveFeelingDescription(PirateRelation pirateRelation)
        {
            string result = string.Empty;
            if (pirateRelation == null)
            {
                return result;
            }
            int num = (int)pirateRelation.Evaluation;
            if (num <= -45)
            {
                result = TextResolver.GetText("Furious");
            }
            if (num >= -44 && num <= -20)
            {
                result = TextResolver.GetText("Angry");
            }
            if (num >= -19 && num <= -5)
            {
                result = TextResolver.GetText("Annoyed");
            }
            if (num >= -4 && num <= 7)
            {
                result = TextResolver.GetText("Cautious");
            }
            if (num >= 8 && num <= 20)
            {
                result = TextResolver.GetText("Pleased");
            }
            if (num >= 21 && num <= 44)
            {
                result = TextResolver.GetText("Friendly");
            }
            if (num >= 45)
            {
                result = TextResolver.GetText("Delighted");
            }
            return result;
        }

        public string ResolveFeelingDescription(EmpireEvaluation evaluation)
        {
            string result = string.Empty;
            if (evaluation == null)
            {
                return result;
            }
            int overallAttitude = evaluation.OverallAttitude;
            if (overallAttitude <= -45)
            {
                result = TextResolver.GetText("Furious");
            }
            if (overallAttitude >= -44 && overallAttitude <= -20)
            {
                result = TextResolver.GetText("Angry");
            }
            if (overallAttitude >= -19 && overallAttitude <= -5)
            {
                result = TextResolver.GetText("Annoyed");
            }
            if (overallAttitude >= -4 && overallAttitude <= 7)
            {
                result = TextResolver.GetText("Cautious");
            }
            if (overallAttitude >= 8 && overallAttitude <= 20)
            {
                result = TextResolver.GetText("Pleased");
            }
            if (overallAttitude >= 21 && overallAttitude <= 44)
            {
                result = TextResolver.GetText("Friendly");
            }
            if (overallAttitude >= 45)
            {
                result = TextResolver.GetText("Delighted");
            }
            return result;
        }

        public DiplomaticRelationType ResolveDesiredDiplomaticRelationType(Empire empire, double galaxyIntoleranceLevel)
        {
            return ResolveDesiredDiplomaticRelationType(empire, galaxyIntoleranceLevel, 1.0);
        }

        public DiplomaticRelationType ResolveDesiredDiplomaticRelationType(Empire empire, double galaxyIntoleranceLevel, double diplomacyFactor)
        {
            DiplomaticRelation currentDiplomaticRelation = ObtainDiplomaticRelation(empire);
            EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(empire);
            return ResolveDesiredDiplomaticRelationType(currentDiplomaticRelation, empireEvaluation.OverallAttitude, DominantRace.IntelligenceLevel, DominantRace.FriendlinessLevel, DominantRace.LoyaltyLevel, DominantRace.AggressionLevel, WarWeariness, galaxyIntoleranceLevel, diplomacyFactor);
        }

        public EmpireEvaluation ObtainEmpireEvaluation(Empire empire)
        {
            if (empire == null)
            {
                return new EmpireEvaluation(empire, _Galaxy);
            }
            if (empire == _Galaxy.IndependentEmpire)
            {
                return new EmpireEvaluation(empire, _Galaxy);
            }
            if (empire.PirateEmpireBaseHabitat != null || PirateEmpireBaseHabitat != null)
            {
                return new EmpireEvaluation(empire, _Galaxy);
            }
            if (EmpireEvaluations != null && empire != null)
            {
                EmpireEvaluation empireEvaluation = EmpireEvaluations[empire];
                if (empireEvaluation == null)
                {
                    empireEvaluation = new EmpireEvaluation(empire, _Galaxy);
                    empireEvaluation.Bias = Galaxy.ResolveStandardRaceBias(DominantRace, empire.DominantRace);
                    if (empire.Active)
                    {
                        EmpireEvaluations.Add(empireEvaluation);
                    }
                }
                return empireEvaluation;
            }
            return new EmpireEvaluation(empire, _Galaxy);
        }

        public DiplomaticRelation ObtainDiplomaticRelation(Empire empire)
        {
            if (empire == null)
            {
                return new DiplomaticRelation(DiplomaticRelationType.None, this, this, empire, tradeRestrictedResources: true);
            }
            if (empire == _Galaxy.IndependentEmpire)
            {
                return new DiplomaticRelation(DiplomaticRelationType.None, this, this, empire, tradeRestrictedResources: true);
            }
            if (empire.PirateEmpireBaseHabitat != null || PirateEmpireBaseHabitat != null)
            {
                return new DiplomaticRelation(DiplomaticRelationType.None, this, this, empire, tradeRestrictedResources: true);
            }
            if (empire == this)
            {
                return new DiplomaticRelation(DiplomaticRelationType.None, this, this, empire, tradeRestrictedResources: true);
            }
            if (DiplomaticRelations != null)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[empire];
                if (diplomaticRelation == null)
                {
                    diplomaticRelation = new DiplomaticRelation(DiplomaticRelationType.NotMet, this, this, empire, tradeRestrictedResources: false);
                    if (empire.Active)
                    {
                        DiplomaticRelations.Add(diplomaticRelation);
                    }
                }
                return diplomaticRelation;
            }
            return new DiplomaticRelation(DiplomaticRelationType.None, this, this, empire, tradeRestrictedResources: true);
        }

        private int CalculateMinimumOrderFulfillmentThreshhold(Order order)
        {
            //int num = 0;
            int num2 = 1000;
            int amountOutstandingToContract = order.AmountOutstandingToContract;
            if (amountOutstandingToContract > num2)
            {
                return num2;
            }
            return amountOutstandingToContract;
        }

        private void AttemptToFulfillOrderAtTradingPost(BuiltObject tradingPost, double x, double y, Order order, ref int largestAmount, ref StellarObject bestTradingPost, ref int bestTradingPostCargoIndex, BuiltObjectList availableIndependentFreighters)
        {
            if (!tradingPost.IsSpacePort)
            {
                return;
            }
            int num = -1;
            if (tradingPost.NearestSystemStar != null)
            {
                num = tradingPost.NearestSystemStar.SystemIndex;
            }
            Empire empire = null;
            if (order.RequestingColony != null)
            {
                empire = order.RequestingColony.Empire;
                if (tradingPost.ParentHabitat != null && tradingPost.ParentHabitat == order.RequestingColony)
                {
                    return;
                }
            }
            else if (order.RequestingBuiltObject != null)
            {
                empire = order.RequestingBuiltObject.ActualEmpire;
                if (tradingPost == order.RequestingBuiltObject)
                {
                    return;
                }
            }
            if (empire != _Galaxy.IndependentEmpire)
            {
                if (num >= 0 && empire != null)
                {
                    if (!CheckSystemExplored(num))
                    {
                        return;
                    }
                }
                else if (empire == null || !empire.IsObjectVisibleToThisEmpire(tradingPost, includeLongRangeScanners: true, includeShipsOutsideSystems: false))
                {
                    return;
                }
            }
            int num2 = -1;
            if (tradingPost.Cargo != null)
            {
                if (order.CommodityResource != null && tradingPost.Cargo.GetExists(order.CommodityResource))
                {
                    Resource commodityResource = order.CommodityResource;
                    num2 = tradingPost.Cargo.IndexOf(commodityResource, tradingPost.Empire);
                }
                else if (order.CommodityComponent != null && tradingPost.Cargo.GetExists(order.CommodityComponent))
                {
                    Component commodityComponent = order.CommodityComponent;
                    num2 = tradingPost.Cargo.IndexOf(commodityComponent, tradingPost.Empire);
                }
            }
            if (num2 >= 0)
            {
                _ = tradingPost.Cargo[num2].Amount;
                int available = tradingPost.Cargo[num2].Available;
                int num3 = Galaxy.CalculateResourceLevel(tradingPost.Cargo[num2], tradingPost);
                available -= num3;
                if (available > largestAmount)
                {
                    largestAmount = available;
                    bestTradingPost = tradingPost;
                    bestTradingPostCargoIndex = num2;
                }
            }
        }

        private void AttemptToFulfillOrderAtTradingPost(Habitat tradingPost, double x, double y, Order order, ref int largestAmount, ref StellarObject bestTradingPost, BuiltObjectList availableIndependentFreighters)
        {
            int num = -1;
            num = tradingPost.SystemIndex;
            Empire empire = null;
            if (order.RequestingColony != null)
            {
                empire = order.RequestingColony.Empire;
                if (tradingPost == order.RequestingColony)
                {
                    return;
                }
            }
            else if (order.RequestingBuiltObject != null)
            {
                empire = order.RequestingBuiltObject.ActualEmpire;
                if (order.RequestingBuiltObject.ParentHabitat != null && tradingPost == order.RequestingBuiltObject.ParentHabitat)
                {
                    return;
                }
            }
            if (this != _Galaxy.IndependentEmpire)
            {
                if (num >= 0 && empire != null)
                {
                    if (!CheckSystemExplored(num))
                    {
                        return;
                    }
                }
                else if (empire == null || !empire.IsObjectVisibleToThisEmpire(tradingPost, includeLongRangeScanners: true, includeShipsOutsideSystems: false))
                {
                    return;
                }
            }
            int num2 = -1;
            Empire empire2 = tradingPost.Empire;
            if (empire2 == null)
            {
                empire2 = _Galaxy.IndependentEmpire;
            }
            if (order.CommodityResource != null)
            {
                Resource commodityResource = order.CommodityResource;
                if (tradingPost.Cargo != null && tradingPost.Cargo.GetExists(commodityResource))
                {
                    num2 = tradingPost.Cargo.IndexOf(commodityResource, empire2);
                }
            }
            else if (order.CommodityComponent != null)
            {
                Component commodityComponent = order.CommodityComponent;
                if (tradingPost.Cargo != null && tradingPost.Cargo.GetExists(commodityComponent))
                {
                    num2 = tradingPost.Cargo.IndexOf(commodityComponent, empire2);
                }
            }
            if (num2 < 0)
            {
                return;
            }
            _ = tradingPost.Cargo[num2].Amount;
            int available = tradingPost.Cargo[num2].Available;
            int num3 = 0;
            if (tradingPost.Empire == _Galaxy.IndependentEmpire)
            {
                if (tradingPost.Cargo[num2].CommodityIsResource)
                {
                    num3 = Galaxy.CalculateResourceLevel(tradingPost.Cargo[num2].CommodityResource, tradingPost, isMiningStation: false, isIndependent: true);
                }
            }
            else
            {
                num3 = Galaxy.CalculateResourceLevel(tradingPost.Cargo[num2], tradingPost);
            }
            available -= num3;
            if (available > largestAmount)
            {
                largestAmount = available;
                bestTradingPost = tradingPost;
            }
        }

        private int FindFreighterToFulfillOrder(Order order, int available, StellarObject tradingPost, Empire requestor, BuiltObjectList availableFreighters, int[] empireTotalFreighterCount, ref int[] empireAvailableFreighterCount, BuiltObjectList availableIndependentFreighters)
        {
            int num = Math.Min(order.AmountOutstandingToContract, available);
            Empire empire = null;
            StellarObject destination = null;
            if (order.RequestingBuiltObject != null)
            {
                empire = order.RequestingBuiltObject.ActualEmpire;
                destination = order.RequestingBuiltObject;
            }
            else if (order.RequestingColony != null)
            {
                empire = order.RequestingColony.Owner;
                destination = order.RequestingColony;
            }
            int freighterTypeIndex = 0;
            BuiltObject builtObject = FindFreighterForContract(empire, tradingPost.Empire, Galaxy.MinimumContractSize, tradingPost, destination, availableFreighters, empireTotalFreighterCount, ref empireAvailableFreighterCount, availableIndependentFreighters, ref freighterTypeIndex);
            if (num > 0 && builtObject != null)
            {
                ContractList contracts = new ContractList();
                int num2 = Math.Min(num, builtObject.CargoSpace);
                Contract contract = null;
                if (order.CommodityResource != null)
                {
                    contract = new Contract(tradingPost, num2, order.CommodityResource.ResourceID, -1, empire.EmpireId);
                }
                else if (order.CommodityComponent != null)
                {
                    contract = new Contract(tradingPost, num2, -1, order.CommodityComponent.ComponentID, empire.EmpireId);
                }
                contract.Freighter = builtObject;
                contracts.Add(contract);
                num -= num2;
                long currentStarDate = _Galaxy.CurrentStarDate;
                InitiateContract(tradingPost, order, contract, tradingPost.Empire, currentStarDate);
                CargoList cargoList = new CargoList();
                Cargo cargo = null;
                if (order.CommodityResource != null)
                {
                    cargo = new Cargo(order.CommodityResource, num2, empire);
                }
                else if (order.CommodityComponent != null)
                {
                    cargo = new Cargo(order.CommodityComponent, num2, empire);
                }
                cargoList.Add(cargo);
                FreighterFulfillOrdersForDestination(destination, tradingPost, builtObject, cargoList, currentStarDate, ref contracts, order);
                object target = null;
                if (order.RequestingBuiltObject != null)
                {
                    target = order.RequestingBuiltObject;
                }
                else if (order.RequestingColony != null)
                {
                    target = order.RequestingColony;
                }
                if (tradingPost is BuiltObject)
                {
                    builtObject.AssignMission(BuiltObjectMissionType.Transport, (BuiltObject)tradingPost, target, cargoList, BuiltObjectMissionPriority.Normal);
                }
                else if (tradingPost is Habitat)
                {
                    builtObject.AssignMission(BuiltObjectMissionType.Transport, (Habitat)tradingPost, target, cargoList, BuiltObjectMissionPriority.Normal);
                }
                switch (freighterTypeIndex)
                {
                    case 0:
                        availableFreighters.Remove(builtObject);
                        break;
                    case 1:
                        availableIndependentFreighters.Remove(builtObject);
                        break;
                }
                builtObject.ContractsToFulfill.AddRange(contracts);
                return num2;
            }
            return 0;
        }

        private bool CheckOrderIsAffordable(Order order)
        {
            bool result = true;
            double num = 0.0;
            double num2 = 0.0;
            if (order.CommodityResource != null)
            {
                Resource commodityResource = order.CommodityResource;
                num = _Galaxy.ResourceCurrentPrices[commodityResource.ResourceID];
            }
            else if (order.CommodityComponent != null)
            {
                Component commodityComponent = order.CommodityComponent;
                num = _Galaxy.ComponentCurrentPrices[commodityComponent.ComponentID];
            }
            num2 = num * (double)order.AmountOutstandingToContract;
            if (order.IsStateOrder)
            {
                if (num2 > StateMoney)
                {
                    result = false;
                }
            }
            else if (num2 > GetPrivateFunds())
            {
                result = false;
            }
            return result;
        }

        private SortableStellarObjectList GenerateValidSpaceports(Empire empire)
        {
            SortableStellarObjectList sortableStellarObjectList = new SortableStellarObjectList();
            int num = 100;
            for (int i = 0; i < empire.SpacePorts.Count; i++)
            {
                BuiltObject builtObject = empire.SpacePorts[i];
                if (builtObject != null && builtObject.IsSpacePort && !builtObject.IsBlockaded && builtObject.DockingBayWaitQueue != null && builtObject.DockingBayWaitQueue.Count < num && !sortableStellarObjectList.Contains(builtObject))
                {
                    sortableStellarObjectList.Add(new SortableStellarObject(builtObject));
                }
            }
            return sortableStellarObjectList;
        }

        private SortableStellarObjectList GenerateValidTradingPosts(Empire empire)
        {
            SortableStellarObjectList sortableStellarObjectList = new SortableStellarObjectList();
            int num = 50;
            for (int i = 0; i < empire.SpacePorts.Count; i++)
            {
                BuiltObject builtObject = empire.SpacePorts[i];
                if (builtObject != null && builtObject.IsSpacePort && !builtObject.IsBlockaded && builtObject.DockingBayWaitQueue != null && builtObject.DockingBayWaitQueue.Count < num && !sortableStellarObjectList.Contains(builtObject))
                {
                    sortableStellarObjectList.Add(new SortableStellarObject(builtObject));
                }
            }
            for (int j = 0; j < empire.MiningStations.Count; j++)
            {
                BuiltObject builtObject2 = empire.MiningStations[j];
                if (builtObject2 != null && builtObject2.IsResourceExtractor && builtObject2.IsSpacePort && !builtObject2.IsBlockaded && builtObject2.DockingBayWaitQueue != null && builtObject2.DockingBayWaitQueue.Count < num && !sortableStellarObjectList.Contains(builtObject2))
                {
                    sortableStellarObjectList.Add(new SortableStellarObject(builtObject2));
                }
            }
            if (empire.PirateEmpireBaseHabitat == null)
            {
                for (int k = 0; k < empire.Colonies.Count; k++)
                {
                    Habitat habitat = empire.Colonies[k];
                    if (habitat.IsBlockaded)
                    {
                        continue;
                    }
                    bool flag = false;
                    if (habitat.BasesAtHabitat != null && habitat.BasesAtHabitat.Count > 0)
                    {
                        for (int l = 0; l < habitat.BasesAtHabitat.Count; l++)
                        {
                            if (habitat.BasesAtHabitat[l].IsSpacePort && (habitat.BasesAtHabitat[l].SubRole == BuiltObjectSubRole.SmallSpacePort || habitat.BasesAtHabitat[l].SubRole == BuiltObjectSubRole.MediumSpacePort || habitat.BasesAtHabitat[l].SubRole == BuiltObjectSubRole.LargeSpacePort))
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (!flag && habitat.DockingBayWaitQueue != null && habitat.DockingBayWaitQueue.Count < num && !sortableStellarObjectList.Contains(habitat))
                    {
                        sortableStellarObjectList.Add(new SortableStellarObject(habitat));
                    }
                }
            }
            if (empire.Policy != null && empire.Policy.TradeWithOtherEmpires)
            {
                if (empire.PirateEmpireBaseHabitat == null)
                {
                    for (int m = 0; m < _Galaxy.Empires.Count; m++)
                    {
                        Empire empire2 = _Galaxy.Empires[m];
                        if (empire2 == empire || empire2 == null || empire2.Policy == null || !empire2.Policy.TradeWithOtherEmpires)
                        {
                            continue;
                        }
                        DiplomaticRelation diplomaticRelation = empire.ObtainDiplomaticRelation(empire2);
                        if (diplomaticRelation.Type == DiplomaticRelationType.NotMet || diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions || diplomaticRelation.Type == DiplomaticRelationType.War)
                        {
                            continue;
                        }
                        for (int n = 0; n < empire2.SpacePorts.Count; n++)
                        {
                            BuiltObject builtObject3 = empire2.SpacePorts[n];
                            if (builtObject3 != null && builtObject3.IsSpacePort)
                            {
                                bool flag2 = false;
                                if (builtObject3.NearestSystemStar != null && empire.CheckSystemExplored(builtObject3.NearestSystemStar.SystemIndex))
                                {
                                    flag2 = true;
                                }
                                if (flag2 && !builtObject3.IsBlockaded && builtObject3.DockingBayWaitQueue != null && builtObject3.DockingBayWaitQueue.Count < num && !sortableStellarObjectList.Contains(builtObject3))
                                {
                                    sortableStellarObjectList.Add(new SortableStellarObject(builtObject3));
                                }
                            }
                        }
                        for (int num2 = 0; num2 < empire2.MiningStations.Count; num2++)
                        {
                            BuiltObject builtObject4 = empire2.MiningStations[num2];
                            if (builtObject4 != null && builtObject4.IsSpacePort && builtObject4.IsResourceExtractor)
                            {
                                bool flag3 = false;
                                if (builtObject4.NearestSystemStar != null && empire.CheckSystemExplored(builtObject4.NearestSystemStar.SystemIndex))
                                {
                                    flag3 = true;
                                }
                                if (flag3 && !builtObject4.IsBlockaded && builtObject4.DockingBayWaitQueue != null && builtObject4.DockingBayWaitQueue.Count < num && !sortableStellarObjectList.Contains(builtObject4))
                                {
                                    sortableStellarObjectList.Add(new SortableStellarObject(builtObject4));
                                }
                            }
                        }
                    }
                }
                for (int num3 = 0; num3 < empire.PirateRelations.Count; num3++)
                {
                    PirateRelation pirateRelation = empire.PirateRelations[num3];
                    if (pirateRelation == null || pirateRelation.Type != PirateRelationType.Protection)
                    {
                        continue;
                    }
                    Empire otherEmpire = pirateRelation.OtherEmpire;
                    if (otherEmpire == empire || otherEmpire == null || otherEmpire.Policy == null || !otherEmpire.Policy.TradeWithOtherEmpires)
                    {
                        continue;
                    }
                    for (int num4 = 0; num4 < otherEmpire.SpacePorts.Count; num4++)
                    {
                        BuiltObject builtObject5 = otherEmpire.SpacePorts[num4];
                        if (builtObject5 != null && builtObject5.IsSpacePort)
                        {
                            bool flag4 = false;
                            if (builtObject5.NearestSystemStar != null && empire.CheckSystemExplored(builtObject5.NearestSystemStar.SystemIndex))
                            {
                                flag4 = true;
                            }
                            if (flag4 && !builtObject5.IsBlockaded && builtObject5.DockingBayWaitQueue != null && builtObject5.DockingBayWaitQueue.Count < num && !sortableStellarObjectList.Contains(builtObject5))
                            {
                                sortableStellarObjectList.Add(new SortableStellarObject(builtObject5));
                            }
                        }
                    }
                    for (int num5 = 0; num5 < otherEmpire.MiningStations.Count; num5++)
                    {
                        BuiltObject builtObject6 = otherEmpire.MiningStations[num5];
                        if (builtObject6 != null && builtObject6.IsSpacePort && builtObject6.IsResourceExtractor)
                        {
                            bool flag5 = false;
                            if (builtObject6.NearestSystemStar != null && empire.CheckSystemExplored(builtObject6.NearestSystemStar.SystemIndex))
                            {
                                flag5 = true;
                            }
                            if (flag5 && !builtObject6.IsBlockaded && builtObject6.DockingBayWaitQueue != null && builtObject6.DockingBayWaitQueue.Count < num && !sortableStellarObjectList.Contains(builtObject6))
                            {
                                sortableStellarObjectList.Add(new SortableStellarObject(builtObject6));
                            }
                        }
                    }
                }
            }
            if (empire == _Galaxy.IndependentEmpire)
            {
                for (int num6 = 0; num6 < _Galaxy.IndependentColonies.Count; num6++)
                {
                    Habitat habitat2 = _Galaxy.IndependentColonies[num6];
                    if (habitat2 != null && habitat2.DockingBayWaitQueue != null && habitat2.DockingBayWaitQueue.Count < num && !sortableStellarObjectList.Contains(habitat2))
                    {
                        sortableStellarObjectList.Add(new SortableStellarObject(habitat2));
                    }
                }
            }
            else
            {
                for (int num7 = 0; num7 < _Galaxy.Systems.Count; num7++)
                {
                    if (!empire.CheckSystemExplored(_Galaxy.Systems[num7].SystemStar.SystemIndex) || _Galaxy.Systems[num7].Habitats.Count <= 0)
                    {
                        continue;
                    }
                    for (int num8 = 0; num8 < _Galaxy.Systems[num7].Habitats.Count; num8++)
                    {
                        Habitat habitat3 = _Galaxy.Systems[num7].Habitats[num8];
                        if (habitat3 != null && habitat3.Population.Count > 0 && habitat3.Empire == _Galaxy.IndependentEmpire && habitat3.DockingBayWaitQueue != null && habitat3.DockingBayWaitQueue.Count < num && !sortableStellarObjectList.Contains(habitat3))
                        {
                            sortableStellarObjectList.Add(new SortableStellarObject(habitat3));
                        }
                    }
                }
            }
            return sortableStellarObjectList;
        }

        private SortableStellarObjectList SortTradingPostsByDistance(SortableStellarObjectList tradingPosts, double x, double y)
        {
            for (int i = 0; i < tradingPosts.Count; i++)
            {
                tradingPosts[i].SortTag = _Galaxy.CalculateDistanceSquared(x, y, tradingPosts[i].StellarObject.Xpos, tradingPosts[i].StellarObject.Ypos);
            }
            tradingPosts.Sort();
            return tradingPosts;
        }

        public BuiltObjectList DetermineAvailableFreighters(out int totalFreighters)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            totalFreighters = 0;
            for (int i = 0; i < Freighters.Count; i++)
            {
                if (Freighters[i].BuiltAt == null && !Freighters[i].HasBeenDestroyed)
                {
                    totalFreighters++;
                    if ((Freighters[i].Mission == null || Freighters[i].Mission.Type == BuiltObjectMissionType.Undefined) && !Freighters[i].RetireForNextMission && !Freighters[i].RepairForNextMission && !Freighters[i].RefuelForNextMission && !Freighters[i].RetrofitForNextMission)
                    {
                        builtObjectList.Add(Freighters[i]);
                    }
                }
            }
            return builtObjectList;
        }

        private void SendFreightersToSmugglingDestinations(HabitatList colonies, ref BuiltObjectList availableFreighters, List<byte> resourceTypesToSupply)
        {
            int freightersToSend = Math.Max(1, (int)((double)availableFreighters.Count * 0.6 / (double)colonies.Count));
            for (int i = 0; i < colonies.Count; i++)
            {
                SendFreightersToSmugglingDestination(colonies[i], ref availableFreighters, freightersToSend, resourceTypesToSupply[i]);
            }
        }

        private void SendFreightersToSmugglingDestination(Habitat colony, ref BuiltObjectList availableFreighters, int freightersToSend, byte resourceTypeToSupply)
        {
            BuiltObject builtObject = _Galaxy.FastFindNearestSpacePort(colony.Xpos, colony.Ypos, this);
            if (builtObject == null || builtObject.Cargo == null)
            {
                return;
            }
            for (int i = 0; i < freightersToSend; i++)
            {
                int highestAvailable = 0;
                Cargo cargo = null;
                if (resourceTypeToSupply == byte.MaxValue)
                {
                    cargo = builtObject.Cargo.GetHighestAvailableResource(this, builtObject, out highestAvailable);
                }
                else
                {
                    Resource resource = new Resource(resourceTypeToSupply);
                    cargo = builtObject.Cargo.GetCargoOptimized(resource, builtObject.Empire.EmpireId);
                    if (cargo != null)
                    {
                        highestAvailable = cargo.Available;
                    }
                    int num = Galaxy.CalculateResourceLevel(resource, builtObject);
                    highestAvailable -= num;
                }
                if (cargo != null && highestAvailable > 0)
                {
                    int index = 0;
                    BuiltObject nearestBuiltObjectWithinRange = availableFreighters.GetNearestBuiltObjectWithinRange(builtObject.Xpos, builtObject.Ypos, 0.1, mustBeAvailable: true, out index);
                    if (nearestBuiltObjectWithinRange != null && nearestBuiltObjectWithinRange.Role == BuiltObjectRole.Freight && cargo != null && cargo.Available > 0 && highestAvailable > 0)
                    {
                        int num2 = Math.Min(nearestBuiltObjectWithinRange.CargoCapacity, highestAvailable);
                        CargoList cargoList = new CargoList();
                        cargoList.Add(new Cargo(cargo.Resource, num2, colony.Empire));
                        Contract contract = new Contract(builtObject, num2, cargo.Resource.ResourceID, -1, colony.Empire.EmpireId);
                        contract.Freighter = nearestBuiltObjectWithinRange;
                        nearestBuiltObjectWithinRange.ContractsToFulfill.Add(contract);
                        double transactionAmount = CalculateCurrentContractValue(cargo.Resource, num2);
                        InitiateContract(builtObject, colony, colony.Owner, isState: false, cargo.Resource, null, transactionAmount, contract, this, _Galaxy.CurrentStarDate);
                        nearestBuiltObjectWithinRange.AssignMission(BuiltObjectMissionType.Transport, builtObject, colony, cargoList, BuiltObjectMissionPriority.Normal);
                        availableFreighters.Remove(nearestBuiltObjectWithinRange);
                    }
                }
            }
        }

        public void CheckMarketOrders()
        {
            SortableStellarObjectList tradingPosts = GenerateValidTradingPosts(this);
            SortableStellarObjectList tradingPosts2 = GenerateValidSpaceports(this);
            OrderList orderList = new OrderList();
            int totalFreighters = 0;
            BuiltObjectList ships = DetermineAvailableFreighters(out totalFreighters);
            ships = BaconEmpire.RemoveStateShips(this, ships);
            int totalFreighters2 = 0;
            BuiltObjectList availableIndependentFreighters = _Galaxy.IndependentEmpire.DetermineAvailableFreighters(out totalFreighters2);
            HabitatList habitatList = new HabitatList();
            List<byte> list = new List<byte>();
            EmpireActivityList empireActivityList = _PirateMissions.ResolveActivitiesByType(EmpireActivityType.Smuggle);
            if (empireActivityList.Count > 0 && ships.Count > 0)
            {
                for (int i = 0; i < empireActivityList.Count; i++)
                {
                    EmpireActivity empireActivity = empireActivityList[i];
                    if (empireActivity == null || empireActivity.Target == null || !(empireActivity.Target is Habitat) || empireActivity.RequestingEmpire == this)
                    {
                        continue;
                    }
                    Habitat habitat = (Habitat)empireActivity.Target;
                    //bool flag = false;
                    if (!((PirateEmpireBaseHabitat == null) ? _Galaxy.IsStellarObjectDockable(habitat, this) : _Galaxy.IsStellarObjectDockable(habitat, _Galaxy.IndependentEmpire)))
                    {
                        continue;
                    }
                    OrderList orders = _Galaxy.Orders.GetOrders(habitat);
                    if (empireActivity.ResourceId == byte.MaxValue)
                    {
                        if (orders == null || orders.Count <= 0)
                        {
                            habitatList.Add(habitat);
                            list.Add(byte.MaxValue);
                        }
                        else
                        {
                            orderList.AddRange(orders);
                        }
                        continue;
                    }
                    OrderList orders2 = orders.GetOrders(empireActivity.ResourceId);
                    if (habitat.Empire == _Galaxy.IndependentEmpire)
                    {
                        if (orders2 == null || orders2.Count <= 0)
                        {
                            habitatList.Add(habitat);
                            list.Add(empireActivity.ResourceId);
                        }
                        else
                        {
                            orderList.AddRange(orders2);
                        }
                    }
                    else
                    {
                        orderList.AddRange(orders2);
                    }
                }
                orderList.MergeRange(_Galaxy.Orders.GetOrders(this));
            }
            else
            {
                orderList = _Galaxy.Orders.GetOrders(this);
            }
            _EmpireOrderCount = orderList.Count;
            if (habitatList != null && habitatList.Count > 0)
            {
                SendFreightersToSmugglingDestinations(habitatList, ref ships, list);
            }
            bool useOptimizedSorting = false;
            if (CheckEmpireHasHyperDriveTech(this))
            {
                useOptimizedSorting = true;
            }
            Dictionary<int, SortableStellarObjectList> sortedTradingPosts = new Dictionary<int, SortableStellarObjectList>();
            Dictionary<int, SortableStellarObjectList> sortedTradingPosts2 = new Dictionary<int, SortableStellarObjectList>();
            int[] empireAvailableFreighterCount = new int[Galaxy.MaximumEmpireCount + 1];
            int[] array = new int[Galaxy.MaximumEmpireCount + 1];
            for (int j = 0; j < _Galaxy.Empires.Count; j++)
            {
                Empire empire = _Galaxy.Empires[j];
                if (empire != null)
                {
                    int totalFreighters3 = 0;
                    BuiltObjectList builtObjectList = empire.DetermineAvailableFreighters(out totalFreighters3);
                    empireAvailableFreighterCount[empire.EmpireId] = builtObjectList.Count;
                    array[empire.EmpireId] = totalFreighters3;
                }
            }
            double allowableRangeSquared = (double)Galaxy.SectorSize * 5.0 * ((double)Galaxy.SectorSize * 5.0);
            Design design = _Designs.FindNewest(BuiltObjectSubRole.MediumFreighter);
            if (design != null && design.WarpSpeed > 0)
            {
                allowableRangeSquared = design.MaximumRange();
                allowableRangeSquared *= allowableRangeSquared;
            }
            orderList.SplitOrdersByType(out var standardOrders, out var constructionShortageOrders, out var constructionShortageMobileOrders, out var retrofitResourcesForBaseOrders);
            FulfillOrdersAtTradingPosts(constructionShortageMobileOrders, tradingPosts, ships, allowableRangeSquared, array, ref empireAvailableFreighterCount, availableIndependentFreighters, useOptimizedSorting, ref sortedTradingPosts);
            FulfillOrdersAtTradingPosts(constructionShortageOrders, tradingPosts, ships, allowableRangeSquared, array, ref empireAvailableFreighterCount, availableIndependentFreighters, useOptimizedSorting, ref sortedTradingPosts);
            FulfillOrdersAtTradingPosts(retrofitResourcesForBaseOrders, tradingPosts2, ships, allowableRangeSquared, array, ref empireAvailableFreighterCount, availableIndependentFreighters, useOptimizedSorting, ref sortedTradingPosts2);
            FulfillOrdersAtTradingPosts(standardOrders, tradingPosts, ships, allowableRangeSquared, array, ref empireAvailableFreighterCount, availableIndependentFreighters, useOptimizedSorting, ref sortedTradingPosts);
        }

        public void FulfillOrdersAtTradingPosts(OrderList orders, SortableStellarObjectList tradingPosts, BuiltObjectList availableFreighters, double allowableRangeSquared, int[] empireTotalFreighterCount, ref int[] empireAvailableFreighterCount, BuiltObjectList availableIndependentFreighters, bool useOptimizedSorting, ref Dictionary<int, SortableStellarObjectList> sortedTradingPosts)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                Order order = orders[i];
                if (order.AmountOutstandingToContract <= 0 || (this != _Galaxy.IndependentEmpire && PirateEmpireBaseHabitat == null && !CheckOrderIsAffordable(order)))
                {
                    continue;
                }
                bool requesterIsConstructionShip = false;
                if (order.RequestingBuiltObject != null)
                {
                    if (order.RequestingBuiltObject.HasBeenDestroyed)
                    {
                        continue;
                    }
                    if (order.RequestingBuiltObject.SubRole == BuiltObjectSubRole.ConstructionShip)
                    {
                        requesterIsConstructionShip = true;
                    }
                }
                if (order.RequestingBuiltObject != null)
                {
                    if (order.RequestingBuiltObject.IsBlockaded)
                    {
                        continue;
                    }
                }
                else if (order.RequestingColony != null && order.RequestingColony.IsBlockaded)
                {
                    continue;
                }
                Habitat habitat = null;
                Empire empire = null;
                StellarObject stellarObject = null;
                double x = 0.0;
                double y = 0.0;
                if (order.RequestingBuiltObject != null)
                {
                    stellarObject = order.RequestingBuiltObject;
                    empire = order.RequestingBuiltObject.ActualEmpire;
                    x = order.RequestingBuiltObject.Xpos;
                    y = order.RequestingBuiltObject.Ypos;
                    if (useOptimizedSorting)
                    {
                        habitat = order.RequestingBuiltObject.NearestSystemStar;
                    }
                }
                else if (order.RequestingColony != null)
                {
                    stellarObject = order.RequestingColony;
                    empire = order.RequestingColony.Empire;
                    x = order.RequestingColony.Xpos;
                    y = order.RequestingColony.Ypos;
                    if (useOptimizedSorting)
                    {
                        habitat = Galaxy.DetermineHabitatSystemStar(order.RequestingColony);
                    }
                }
                if (empire == null)
                {
                    _Galaxy.Orders.Remove(order);
                    continue;
                }
                SortableStellarObjectList value = tradingPosts;
                if (habitat != null)
                {
                    if (!sortedTradingPosts.TryGetValue(habitat.SystemIndex, out value))
                    {
                        value = SortTradingPostsByDistance(tradingPosts, habitat.Xpos, habitat.Ypos);
                        sortedTradingPosts.Add(habitat.SystemIndex, value);
                    }
                }
                else
                {
                    value = SortTradingPostsByDistance(tradingPosts, x, y);
                }
                Resource commodityResource = order.CommodityResource;
                bool resourceIsRestricted = false;
                bool resourceIsLuxury = false;
                if (commodityResource != null)
                {
                    resourceIsRestricted = commodityResource.IsRestrictedResource;
                    resourceIsLuxury = commodityResource.IsLuxuryResource;
                }
                for (int j = 0; j < value.Count; j++)
                {
                    if (value[j].StellarObject != stellarObject)
                    {
                        AttemptToFulfillOrderAtTradingPost(value[j].StellarObject, order, commodityResource, resourceIsRestricted, resourceIsLuxury, availableFreighters, allowableRangeSquared, empireTotalFreighterCount, ref empireAvailableFreighterCount, availableIndependentFreighters, requesterIsConstructionShip);
                        if (order.AmountOutstandingToContract <= 0)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private int AttemptToFulfillOrderAtTradingPost(StellarObject tradingPost, Order order, Resource orderResource, bool resourceIsRestricted, bool resourceIsLuxury, BuiltObjectList availableFreighters, double allowableRangeSquared, int[] empireTotalFreighterCount, ref int[] empireAvailableFreighterCount, BuiltObjectList availableIndependentFreighters, bool requesterIsConstructionShip)
        {
            if (tradingPost == null || order == null)
            {
                return 0;
            }
            if (tradingPost is BuiltObject && !((BuiltObject)tradingPost).IsFunctional)
            {
                return 0;
            }
            Empire empire = null;
            double x = 0.0;
            double y = 0.0;
            Habitat requestingColony = order.RequestingColony;
            BuiltObject requestingBuiltObject = order.RequestingBuiltObject;
            if (requestingColony != null)
            {
                empire = requestingColony.Empire;
                if (tradingPost is Habitat)
                {
                    Habitat habitat = (Habitat)tradingPost;
                    x = requestingColony.Xpos;
                    y = requestingColony.Ypos;
                    if (habitat == requestingColony)
                    {
                        return 0;
                    }
                }
                else if (tradingPost is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)tradingPost;
                    x = requestingColony.Xpos;
                    y = requestingColony.Ypos;
                    if (builtObject.ParentHabitat != null && builtObject.ParentHabitat == requestingColony)
                    {
                        return 0;
                    }
                }
            }
            else if (requestingBuiltObject != null)
            {
                empire = requestingBuiltObject.ActualEmpire;
                x = requestingBuiltObject.Xpos;
                y = requestingBuiltObject.Ypos;
                if (tradingPost == requestingBuiltObject)
                {
                    return 0;
                }
            }
            if (orderResource != null && resourceIsRestricted)
            {
                if (tradingPost.Empire != null && tradingPost.Empire != empire)
                {
                    if (tradingPost.Empire.PirateEmpireBaseHabitat != null || empire.PirateEmpireBaseHabitat != null)
                    {
                        return 0;
                    }
                    DiplomaticRelation diplomaticRelation = tradingPost.Empire.ObtainDiplomaticRelation(empire);
                    if (!diplomaticRelation.SupplyRestrictedResources)
                    {
                        return 0;
                    }
                }
                allowableRangeSquared = (double)Galaxy.SizeX * 1.415 * ((double)Galaxy.SizeX * 1.415);
            }
            double num = _Galaxy.CalculateDistanceSquared(tradingPost.Xpos, tradingPost.Ypos, x, y);
            if (num > allowableRangeSquared)
            {
                return 0;
            }
            Cargo cargo = null;
            int num2 = 0;
            CargoList cargo2 = tradingPost.Cargo;
            if (cargo2 != null)
            {
                if (orderResource != null && cargo2.GetExists(orderResource))
                {
                    Empire empire2 = tradingPost.Empire;
                    if (empire2 != null)
                    {
                        cargo = cargo2.GetCargoOptimized(orderResource, empire2.EmpireId);
                        if (resourceIsLuxury && tradingPost is Habitat)
                        {
                            Habitat habitat2 = (Habitat)tradingPost;
                            num2 = ((!resourceIsRestricted) ? habitat2.CalculateMinimumLuxuryResourceLevel() : habitat2.CalculateMinimumLuxuryResourceLevelRestricted());
                        }
                    }
                }
                else if (order.CommodityComponent != null && cargo2.GetExists(order.CommodityComponent))
                {
                    Component commodityComponent = order.CommodityComponent;
                    cargo = cargo2.GetCargo(commodityComponent, tradingPost.Empire);
                }
            }
            if (cargo != null)
            {
                _ = cargo.Amount;
                int available = cargo.Available;
                if (available > 0)
                {
                    int num3 = 0;
                    if ((requesterIsConstructionShip || order.Type == OrderType.ConstructionShortage || order.Type == OrderType.ConstructionShortageMobile || order.Type == OrderType.RetrofitResourcesForBase) && tradingPost.Empire == empire)
                    {
                        num3 = 0;
                    }
                    else if (tradingPost is BuiltObject)
                    {
                        num3 = Galaxy.CalculateResourceLevel(cargo, (BuiltObject)tradingPost);
                    }
                    else if (tradingPost is Habitat)
                    {
                        num3 = ((num2 <= 0) ? Galaxy.CalculateResourceLevel(cargo, (Habitat)tradingPost) : num2);
                    }
                    available -= num3;
                    if (available > 0 && available > Math.Min(order.AmountOutstandingToContract, Galaxy.MinimumContractSize))
                    {
                        return FindFreighterToFulfillOrder(order, available, tradingPost, empire, availableFreighters, empireTotalFreighterCount, ref empireAvailableFreighterCount, availableIndependentFreighters);
                    }
                }
            }
            return 0;
        }

        private double CalculateCurrentContractValue(Resource resource, int amount)
        {
            double num = 0.0;
            if (resource != null)
            {
                num = _Galaxy.ResourceCurrentPrices[resource.ResourceID];
            }
            return num * (double)amount;
        }

        private double CalculateCurrentContractValue(Order order, int amount)
        {
            double num = 0.0;
            if (order.CommodityResource != null)
            {
                Resource commodityResource = order.CommodityResource;
                num = _Galaxy.ResourceCurrentPrices[commodityResource.ResourceID];
            }
            else if (order.CommodityComponent != null)
            {
                Component commodityComponent = order.CommodityComponent;
                num = _Galaxy.ComponentCurrentPrices[commodityComponent.ComponentID];
            }
            return num * (double)amount;
        }

        private void PayForFreight(StellarObject sellingPoint, Contract contract, double requestorX, double requestorY, bool requestorIsState, Empire requestingEmpire)
        {
            _Galaxy.CalculateDistance(sellingPoint.Xpos, sellingPoint.Ypos, requestorX, requestorY);
            double num = 0.0;
            if (contract.Freighter.Owner != null)
            {
                if (requestingEmpire != null)
                {
                    if (requestorIsState)
                    {
                        requestingEmpire.StateMoney -= num;
                    }
                    else
                    {
                        requestingEmpire.PerformPrivateTransaction(0.0 - num);
                    }
                }
                contract.Freighter.Owner.StateMoney += num;
            }
            else
            {
                if (contract.Freighter.Empire == null)
                {
                    return;
                }
                if (requestingEmpire != null)
                {
                    if (requestorIsState)
                    {
                        requestingEmpire.StateMoney -= num;
                    }
                    else
                    {
                        requestingEmpire.PerformPrivateTransaction(0.0 - num);
                    }
                }
                contract.Freighter.Empire.PerformPrivateTransaction(num);
            }
        }

        private void InitiateContract(StellarObject sellingPoint, Order order, Contract contract, Empire empire, long starDate)
        {
            StellarObject destination = null;
            Empire requestingEmpire = null;
            bool isStateOrder = order.IsStateOrder;
            Resource commodityResource = order.CommodityResource;
            Component commodityComponent = order.CommodityComponent;
            if (order.RequestingBuiltObject != null)
            {
                destination = order.RequestingBuiltObject;
                requestingEmpire = order.RequestingBuiltObject.ActualEmpire;
            }
            else if (order.RequestingColony != null)
            {
                destination = order.RequestingColony;
                requestingEmpire = order.RequestingColony.Owner;
            }
            order.Contracts.Add(contract);
            double transactionAmount = CalculateCurrentContractValue(order, contract.AmountToFulfill);
            InitiateContract(sellingPoint, destination, requestingEmpire, isStateOrder, commodityResource, commodityComponent, transactionAmount, contract, empire, starDate);
        }

        private void InitiateContract(StellarObject sellingPoint, StellarObject destination, Empire requestingEmpire, bool isState, Resource resource, Component component, double transactionAmount, Contract contract, Empire empire, long starDate)
        {
            if (requestingEmpire == null)
            {
                return;
            }
            double num = 0.0;
            if (sellingPoint is BuiltObject)
            {
                num = transactionAmount * (double)((BuiltObject)sellingPoint).TradeBonuses;
            }
            if (empire.DominantRace != null)
            {
                num *= empire.DominantRace.FreeTradeIncomeFactor;
            }
            if (sellingPoint.Empire != null && sellingPoint.Empire.Leader != null)
            {
                double num2 = 1.0 + (double)sellingPoint.Empire.Leader.TradeIncome / 100.0;
                num *= num2;
            }
            if (sellingPoint.Characters != null && sellingPoint.Characters.Count > 0)
            {
                int highestSkillLevelExcludeLeaders = sellingPoint.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.TradeIncome);
                double num3 = 1.0 + (double)highestSkillLevelExcludeLeaders / 100.0;
                num *= num3;
            }
            empire.StateMoney += empire.ApplyCorruptionToIncome(num);
            double num4 = empire.ApplyCorruptionToIncome(transactionAmount);
            empire.PerformPrivateTransaction(num4);
            empire.PirateEconomy.PerformIncome(num4, PirateIncomeType.Mining, starDate);
            if (requestingEmpire != null)
            {
                if (isState || requestingEmpire.PirateEmpireBaseHabitat != null)
                {
                    requestingEmpire.StateMoney -= transactionAmount;
                    requestingEmpire.PirateEconomy.PerformExpense(transactionAmount, PirateExpenseType.PurchaseResources, starDate);
                }
                else
                {
                    requestingEmpire.PerformPrivateTransaction(0.0 - transactionAmount);
                }
            }
            if (sellingPoint is BuiltObject)
            {
                ((BuiltObject)sellingPoint).PerformFinancialTransaction(transactionAmount, starDate, incomeFromTax: true);
            }
            PayForFreight(sellingPoint, contract, destination.Xpos, destination.Ypos, isState, requestingEmpire);
            Cargo cargo = null;
            Cargo cargo2 = null;
            if (sellingPoint.Cargo != null)
            {
                if (resource != null)
                {
                    cargo = sellingPoint.Cargo.GetCargo(resource, sellingPoint.Empire);
                    cargo2 = new Cargo(resource, contract.AmountToFulfill, requestingEmpire, contract.AmountToFulfill);
                }
                else if (component != null)
                {
                    cargo = sellingPoint.Cargo.GetCargo(component, sellingPoint.Empire);
                    cargo2 = new Cargo(component, contract.AmountToFulfill, requestingEmpire, contract.AmountToFulfill);
                }
                if (cargo != null)
                {
                    if (cargo.Amount > contract.AmountToFulfill)
                    {
                        cargo.Amount -= contract.AmountToFulfill;
                    }
                    else
                    {
                        sellingPoint.Cargo.Remove(cargo);
                    }
                }
                sellingPoint.Cargo.Add(cargo2);
            }
            if (requestingEmpire != _Galaxy.IndependentEmpire && sellingPoint.Empire != _Galaxy.IndependentEmpire && requestingEmpire != sellingPoint.Empire && requestingEmpire.PirateEmpireBaseHabitat == null && sellingPoint.Empire.PirateEmpireBaseHabitat == null)
            {
                DiplomaticRelation diplomaticRelation = sellingPoint.Empire.DiplomaticRelations[requestingEmpire];
                if (diplomaticRelation == null)
                {
                    diplomaticRelation = new DiplomaticRelation(DiplomaticRelationType.NotMet, sellingPoint.Empire, sellingPoint.Empire, requestingEmpire, tradeRestrictedResources: false);
                    sellingPoint.Empire.DiplomaticRelations.Add(diplomaticRelation);
                }
                diplomaticRelation.PerformTradeTransaction(transactionAmount, starDate);
                DiplomaticRelation diplomaticRelation2 = requestingEmpire.DiplomaticRelations[sellingPoint.Empire];
                if (diplomaticRelation2 == null)
                {
                    diplomaticRelation2 = new DiplomaticRelation(DiplomaticRelationType.NotMet, sellingPoint.Empire, requestingEmpire, sellingPoint.Empire, tradeRestrictedResources: false);
                    DiplomaticRelations.Add(diplomaticRelation2);
                }
                diplomaticRelation2.PerformTradeTransaction(transactionAmount, starDate);
            }
        }

        private void FreighterFulfillOrdersForDestination(StellarObject destination, StellarObject supplier, BuiltObject freighter, CargoList freighterMissionCargo, long starDate, ref ContractList contracts, Order orderToExclude)
        {
            OrderList orderList = null;
            if (destination is BuiltObject)
            {
                orderList = _Galaxy.Orders.GetOrders((BuiltObject)destination);
            }
            else if (destination is Habitat)
            {
                orderList = _Galaxy.Orders.GetOrders((Habitat)destination);
            }
            if (orderList == null)
            {
                return;
            }
            for (int i = 0; i < orderList.Count; i++)
            {
                Order order = orderList[i];
                if (order == orderToExclude || order.AmountOutstandingToContract <= 0)
                {
                    continue;
                }
                int num = -1;
                Cargo cargo = null;
                if (supplier.Cargo == null)
                {
                    continue;
                }
                if (order.CommodityResource != null && supplier.Cargo.GetExists(order.CommodityResource))
                {
                    Resource commodityResource = order.CommodityResource;
                    num = supplier.Cargo.IndexOf(commodityResource, supplier.Empire);
                    cargo = new Cargo(commodityResource, 0, destination.Empire, 0);
                }
                else if (order.CommodityComponent != null && supplier.Cargo.GetExists(order.CommodityComponent))
                {
                    Component commodityComponent = order.CommodityComponent;
                    num = supplier.Cargo.IndexOf(commodityComponent, supplier.Empire);
                    cargo = new Cargo(commodityComponent, 0, destination.Empire, 0);
                }
                if (num < 0)
                {
                    continue;
                }
                int num2 = freighter.CargoSpace - freighterMissionCargo.TotalUnits;
                if (num2 <= 0)
                {
                    break;
                }
                int available = supplier.Cargo[num].Available;
                int num3 = 0;
                if (supplier is Habitat)
                {
                    num3 = Galaxy.CalculateResourceLevel(supplier.Cargo[num], (Habitat)supplier);
                }
                else if (supplier is BuiltObject)
                {
                    num3 = Galaxy.CalculateResourceLevel(supplier.Cargo[num], (BuiltObject)supplier);
                }
                available = Math.Max(0, available - num3);
                int val = Math.Min(order.AmountOutstandingToContract, available);
                val = Math.Min(val, num2);
                if (val > 0)
                {
                    Contract contract = null;
                    if (order.CommodityResource != null)
                    {
                        contract = new Contract(supplier, val, order.CommodityResource.ResourceID, -1, destination.Empire.EmpireId);
                    }
                    else if (order.CommodityComponent != null)
                    {
                        contract = new Contract(supplier, val, -1, order.CommodityComponent.ComponentID, destination.Empire.EmpireId);
                    }
                    cargo.Amount = val;
                    freighterMissionCargo.Add(cargo);
                    contract.Freighter = freighter;
                    contracts.Add(contract);
                    InitiateContract(supplier, order, contract, supplier.Empire, starDate);
                }
            }
        }

        private int DetermineNumberOfAvailableFreighters(Empire empire, int minimumCargoSpaceRequired)
        {
            int num = 0;
            for (int i = 0; i < empire.Freighters.Count; i++)
            {
                BuiltObject builtObject = empire.Freighters[i];
                if ((builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined) && builtObject.BuiltAt == null && builtObject.CargoCapacity > minimumCargoSpaceRequired)
                {
                    num++;
                }
            }
            return num;
        }

        private BuiltObject FindFreighterForContract(Empire buyer, Empire seller, int minimumCargoSpaceRequired, StellarObject pickupPoint, StellarObject destination, BuiltObjectList availableFreighters, int[] empireTotalFreighterCount, ref int[] empireAvailableFreighterCount, BuiltObjectList availableIndependentFreighters, ref int freighterTypeIndex)
        {
            if (buyer != null && seller != null && pickupPoint != null && destination != null)
            {
                BuiltObject builtObject = null;
                double num = double.MaxValue;
                double rangeFactor = 0.0;
                freighterTypeIndex = 0;
                int num2 = 0;
                if (availableFreighters != null && availableFreighters.Count > 0)
                {
                    num2 = Galaxy.Rnd.Next(0, availableFreighters.Count);
                    for (int i = num2; i < availableFreighters.Count; i++)
                    {
                        BuiltObject builtObject2 = availableFreighters[i];
                        if (builtObject2 == null)
                        {
                            continue;
                        }
                        BuiltObjectMission mission = builtObject2.Mission;
                        if ((mission == null || mission.Type == BuiltObjectMissionType.Undefined) && builtObject2.BuiltAt == null && !builtObject2.HasBeenDestroyed && builtObject2.CargoCapacity > minimumCargoSpaceRequired)
                        {
                            if (builtObject2.WithinFuelRange(pickupPoint.Xpos, pickupPoint.Ypos, 0.0, out rangeFactor) && builtObject2.WithinFuelRange(destination.Xpos, destination.Ypos, 0.0, out rangeFactor))
                            {
                                return builtObject2;
                            }
                            if (rangeFactor < num)
                            {
                                builtObject = builtObject2;
                                num = rangeFactor;
                            }
                        }
                    }
                    for (int j = 0; j < num2; j++)
                    {
                        BuiltObject builtObject3 = availableFreighters[j];
                        if (builtObject3 == null)
                        {
                            continue;
                        }
                        BuiltObjectMission mission2 = builtObject3.Mission;
                        if ((mission2 == null || mission2.Type == BuiltObjectMissionType.Undefined) && builtObject3.BuiltAt == null && !builtObject3.HasBeenDestroyed && builtObject3.CargoCapacity > minimumCargoSpaceRequired)
                        {
                            if (builtObject3.WithinFuelRange(pickupPoint.Xpos, pickupPoint.Ypos, 0.0, out rangeFactor) && builtObject3.WithinFuelRange(destination.Xpos, destination.Ypos, 0.0, out rangeFactor))
                            {
                                return builtObject3;
                            }
                            if (rangeFactor < num)
                            {
                                builtObject = builtObject3;
                                num = rangeFactor;
                            }
                        }
                    }
                }
                freighterTypeIndex = 1;
                if (availableIndependentFreighters != null && availableIndependentFreighters.Count > 0)
                {
                    num2 = Galaxy.Rnd.Next(0, availableIndependentFreighters.Count);
                    for (int k = num2; k < availableIndependentFreighters.Count; k++)
                    {
                        BuiltObject builtObject4 = availableIndependentFreighters[k];
                        if (builtObject4 == null)
                        {
                            continue;
                        }
                        BuiltObjectMission mission3 = builtObject4.Mission;
                        if ((mission3 != null && mission3.Type != 0) || builtObject4.BuiltAt != null || builtObject4.HasBeenDestroyed || builtObject4.CargoCapacity <= minimumCargoSpaceRequired)
                        {
                            continue;
                        }
                        if (buyer.PirateEmpireBaseHabitat != null)
                        {
                            double num3 = _Galaxy.CalculateDistance(builtObject4.Xpos, builtObject4.Ypos, pickupPoint.Xpos, pickupPoint.Ypos);
                            if ((int)num3 <= Galaxy.IndependentTraderFreightRange && builtObject4.WithinFuelRange(pickupPoint.Xpos, pickupPoint.Ypos, 0.1) && builtObject4.WithinFuelRange(destination.Xpos, destination.Ypos, 0.1) && builtObject4.Role == BuiltObjectRole.Freight)
                            {
                                return builtObject4;
                            }
                        }
                        else
                        {
                            double num4 = _Galaxy.CalculateDistance(builtObject4.Xpos, builtObject4.Ypos, pickupPoint.Xpos, pickupPoint.Ypos);
                            if ((int)num4 <= Galaxy.IndependentTraderFreightRange && builtObject4.WithinFuelRange(pickupPoint.Xpos, pickupPoint.Ypos, 0.1) && builtObject4.WithinFuelRange(destination.Xpos, destination.Ypos, 0.1) && builtObject4.Role == BuiltObjectRole.Freight)
                            {
                                return builtObject4;
                            }
                        }
                    }
                    for (int l = 0; l < num2; l++)
                    {
                        BuiltObject builtObject5 = availableIndependentFreighters[l];
                        if (builtObject5 == null)
                        {
                            continue;
                        }
                        BuiltObjectMission mission4 = builtObject5.Mission;
                        if ((mission4 != null && mission4.Type != 0) || builtObject5.BuiltAt != null || builtObject5.HasBeenDestroyed || builtObject5.CargoCapacity <= minimumCargoSpaceRequired)
                        {
                            continue;
                        }
                        if (buyer.PirateEmpireBaseHabitat != null)
                        {
                            double num5 = _Galaxy.CalculateDistance(builtObject5.Xpos, builtObject5.Ypos, pickupPoint.Xpos, pickupPoint.Ypos);
                            if ((int)num5 <= Galaxy.IndependentTraderFreightRange && builtObject5.WithinFuelRange(pickupPoint.Xpos, pickupPoint.Ypos, 0.1) && builtObject5.WithinFuelRange(destination.Xpos, destination.Ypos, 0.1) && builtObject5.Role == BuiltObjectRole.Freight)
                            {
                                return builtObject5;
                            }
                        }
                        else
                        {
                            double num6 = _Galaxy.CalculateDistance(builtObject5.Xpos, builtObject5.Ypos, pickupPoint.Xpos, pickupPoint.Ypos);
                            if ((int)num6 <= Galaxy.IndependentTraderFreightRange && builtObject5.WithinFuelRange(pickupPoint.Xpos, pickupPoint.Ypos, 0.1) && builtObject5.WithinFuelRange(destination.Xpos, destination.Ypos, 0.1) && builtObject5.Role == BuiltObjectRole.Freight)
                            {
                                return builtObject5;
                            }
                        }
                    }
                }
                if (buyer != _Galaxy.IndependentEmpire)
                {
                    freighterTypeIndex = 2;
                    double num7 = 0.0;
                    if (seller != null)
                    {
                        BuiltObjectList freighters = seller.Freighters;
                        if (freighters != null)
                        {
                            num7 = (double)empireAvailableFreighterCount[seller.EmpireId] / Math.Max(1.0, empireTotalFreighterCount[seller.EmpireId]);
                            if (num7 > 0.5)
                            {
                                Habitat habitat = null;
                                if (destination is Habitat)
                                {
                                    habitat = Galaxy.DetermineHabitatSystemStar((Habitat)destination);
                                }
                                else if (destination is BuiltObject)
                                {
                                    habitat = ((BuiltObject)destination).NearestSystemStar;
                                }
                                if (habitat != null && seller.CheckSystemExplored(habitat))
                                {
                                    for (int m = 0; m < freighters.Count; m++)
                                    {
                                        BuiltObject builtObject6 = freighters[m];
                                        if (builtObject6 == null)
                                        {
                                            continue;
                                        }
                                        BuiltObjectMission mission5 = builtObject6.Mission;
                                        if ((mission5 != null && mission5.Type != 0) || builtObject6.BuiltAt != null || builtObject6.HasBeenDestroyed || builtObject6.CargoCapacity <= minimumCargoSpaceRequired)
                                        {
                                            continue;
                                        }
                                        if (buyer.PirateEmpireBaseHabitat != null)
                                        {
                                            if (!builtObject6.CheckPirateRelationOk(buyer))
                                            {
                                                continue;
                                            }
                                            if (builtObject6.WithinFuelRange(pickupPoint.Xpos, pickupPoint.Ypos, 0.0) && builtObject6.WithinFuelRange(destination.Xpos, destination.Ypos, 0.0))
                                            {
                                                empireAvailableFreighterCount[seller.EmpireId]--;
                                                return builtObject6;
                                            }
                                            if (rangeFactor < num)
                                            {
                                                if (builtObject == null)
                                                {
                                                    empireAvailableFreighterCount[seller.EmpireId]--;
                                                }
                                                builtObject = builtObject6;
                                                num = rangeFactor;
                                            }
                                            continue;
                                        }
                                        if (builtObject6.WithinFuelRange(pickupPoint.Xpos, pickupPoint.Ypos, 0.0) && builtObject6.WithinFuelRange(destination.Xpos, destination.Ypos, 0.0))
                                        {
                                            empireAvailableFreighterCount[seller.EmpireId]--;
                                            return builtObject6;
                                        }
                                        if (rangeFactor < num)
                                        {
                                            if (builtObject == null)
                                            {
                                                empireAvailableFreighterCount[seller.EmpireId]--;
                                            }
                                            builtObject = builtObject6;
                                            num = rangeFactor;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (builtObject != null && builtObject.WithinFuelRange(pickupPoint.Xpos, pickupPoint.Ypos, 0.0) && builtObject.WithinFuelRange(destination.Xpos, destination.Ypos, 0.0))
                {
                    return builtObject;
                }
            }
            return null;
        }

        public void CancelPirateMission(EmpireActivity mission)
        {
            if (mission != null)
            {
                PirateMissions.RemoveEquivalent(mission);
                if (mission.RequestingEmpire != null && mission.RequestingEmpire.PirateMissions != null)
                {
                    mission.RequestingEmpire.PirateMissions.RemoveEquivalent(mission);
                }
                if (mission.AssignedEmpire != null && mission.AssignedEmpire.PirateMissions != null)
                {
                    mission.AssignedEmpire.PirateMissions.RemoveEquivalent(mission);
                }
            }
        }

        public void CompletePirateMission(EmpireActivity mission)
        {
            if (mission != null && mission.RequestingEmpire != null)
            {
                mission.RequestingEmpire.ChangePirateEvaluation(this, 10f, PirateRelationEvaluationType.PirateMissionsSucceed);
                mission.RequestingEmpire.StateMoney -= mission.Price;
                StateMoney += mission.Price;
                PirateEconomy.PerformIncome(mission.Price, PirateIncomeType.Missions, _Galaxy.CurrentStarDate);
                switch (mission.Type)
                {
                    case EmpireActivityType.Attack:
                        Counters.CompletedPirateMissionAttackCount++;
                        break;
                    case EmpireActivityType.Defend:
                        Counters.CompletedPirateMissionDefendCount++;
                        break;
                }
                PirateMissions.Remove(mission);
                mission.RequestingEmpire.PirateMissions.Remove(mission);
                if (mission.AssignedEmpire != null && mission.AssignedEmpire.PirateMissions != null)
                {
                    mission.AssignedEmpire.PirateMissions.Remove(mission);
                }
                string description = string.Empty;
                EmpireMessageType messageType = EmpireMessageType.PirateAttackMissionCompleted;
                switch (mission.Type)
                {
                    case EmpireActivityType.Attack:
                        description = string.Format(TextResolver.GetText("Pirate Attack Mission Completed Pirate"), mission.RequestingEmpire.Name, mission.Target.Name, mission.Price.ToString("0"));
                        messageType = EmpireMessageType.PirateAttackMissionCompleted;
                        break;
                    case EmpireActivityType.Defend:
                        description = string.Format(TextResolver.GetText("Pirate Defend Mission Completed Pirate"), mission.RequestingEmpire.Name, mission.Target.Name, mission.Price.ToString("0"));
                        messageType = EmpireMessageType.PirateDefendMissionCompleted;
                        break;
                }
                SendMessageToEmpire(this, messageType, mission.Target, description);
                description = string.Empty;
                switch (mission.Type)
                {
                    case EmpireActivityType.Attack:
                        description = string.Format(TextResolver.GetText("Pirate Attack Mission Completed Other"), Name, mission.Target.Name, mission.Price.ToString("0"));
                        break;
                    case EmpireActivityType.Defend:
                        description = string.Format(TextResolver.GetText("Pirate Defend Mission Completed Other"), Name, mission.Target.Name, mission.Price.ToString("0"));
                        break;
                }
                mission.RequestingEmpire.SendMessageToEmpire(mission.RequestingEmpire, messageType, mission.Target, description);
            }
        }

        public double CalculateCostPerTroop(TroopType troopType, Habitat colony, BuiltObject builtObject)
        {
            double num = 0.0;
            double num2 = Galaxy.CalculateDefaultTroopMaintenanceMultiplier(troopType);
            double num3 = 1.0;
            if (Leader != null)
            {
                num3 *= 1.0 + (double)Leader.TroopMaintenance / 100.0;
            }
            num = Galaxy.TroopAnnualMaintenance * num2;
            if (GovernmentAttributes != null)
            {
                num *= GovernmentAttributes.MaintenanceCosts;
            }
            num /= num3;
            if (colony != null)
            {
                if (colony.Characters != null && colony.Characters.Count > 0)
                {
                    int highestSkillLevelExcludeLeaders = colony.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.TroopMaintenance);
                    double num4 = 1.0 + (double)highestSkillLevelExcludeLeaders / 100.0;
                    num /= num4;
                }
            }
            else if (builtObject != null && builtObject.Characters != null && builtObject.Characters.Count > 0)
            {
                int highestSkillLevel = builtObject.Characters.GetHighestSkillLevel(CharacterSkillType.TroopMaintenance);
                double num5 = 1.0 + (double)highestSkillLevel / 100.0;
                num /= num5;
            }
            return num;
        }

        private void DisbandExcessTroops()
        {
            if (Troops == null || DominantRace == null || Colonies == null || BuiltObjects == null)
            {
                return;
            }
            double num = CalculateAccurateAnnualIncome();
            double num2 = (double)DominantRace.AggressionLevel / 100.0;
            double num3 = (double)DominantRace.CautionLevel / 100.0;
            double num4 = (num2 + num3) / 2.0 * Galaxy.SpendingTroopPercentage;
            double num5 = AnnualTroopMaintenanceIncludeRecruiting / num;
            int count = Troops.Count;
            double num6 = AnnualStateMaintenanceExcludingUnderConstruction + AnnualSubjugationTribute + AnnualTroopMaintenanceIncludeRecruiting + AnnualPirateProtection;
            double num7 = StateMoney / num6;
            double num8 = CalculateAccurateAnnualCashflow();
            if (!(num8 < 0.0) || !(num7 < Galaxy.AllowableYearsMaintenanceFromCashOnHand) || !(num5 > num4))
            {
                return;
            }
            int num9 = (int)(1.0 + (num5 - num4) * (double)count);
            if (num9 > count)
            {
                num9 = count;
            }
            int num10 = 0;
            if (num10 < num9)
            {
                StellarObjectList stellarObjectList = ResolveLocationsToDefend(includeBases: false);
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat = Colonies[i];
                    if (habitat == null || habitat.Troops == null || stellarObjectList.Contains(habitat) || habitat.Troops.Count <= 0 || habitat.Troops.TotalDefendStrengthExcludeReadiness / 100 <= (int)((double)habitat.TroopLevelRequired * 0.5) || habitat.CheckTroopFacilitiesPresent() || habitat.DefensiveFortressBonus > 0)
                    {
                        continue;
                    }
                    Troop troop = habitat.Troops[0];
                    if (troop != null)
                    {
                        if (troop.Empire != null && troop.Empire.Troops != null)
                        {
                            troop.Empire.Troops.Remove(troop);
                        }
                        troop.Colony = null;
                        troop.BuiltObject = null;
                        troop.Empire = null;
                        habitat.Troops.RemoveAt(0);
                        num10++;
                        if (num10 >= num9)
                        {
                            break;
                        }
                    }
                }
            }
            if (num10 < num9)
            {
                for (int j = 0; j < BuiltObjects.Count; j++)
                {
                    BuiltObject builtObject = BuiltObjects[j];
                    if (builtObject == null || builtObject.Troops == null || builtObject.Troops.Count <= 0 || builtObject.SubRole == BuiltObjectSubRole.TroopTransport || !builtObject.IsAutoControlled)
                    {
                        continue;
                    }
                    bool flag = true;
                    if (builtObject.Mission != null && (builtObject.Mission.Type == BuiltObjectMissionType.Attack || builtObject.Mission.Type == BuiltObjectMissionType.WaitAndAttack) && builtObject.Mission.TargetHabitat != null)
                    {
                        flag = false;
                    }
                    if (!flag || builtObject.Troops.Count <= 0)
                    {
                        continue;
                    }
                    Troop troop2 = builtObject.Troops[0];
                    if (troop2 != null && troop2.Empire != null && troop2.Empire.Troops != null)
                    {
                        troop2.Empire.Troops.Remove(troop2);
                        troop2.BuiltObject = null;
                        troop2.Colony = null;
                        troop2.Empire = null;
                        builtObject.Troops.RemoveAt(0);
                        num10++;
                        if (num10 >= num9)
                        {
                            break;
                        }
                    }
                }
            }
            if (num10 >= num9)
            {
                return;
            }
            for (int k = 0; k < BuiltObjects.Count; k++)
            {
                BuiltObject builtObject2 = BuiltObjects[k];
                if (builtObject2 == null || builtObject2.Troops == null || builtObject2.Troops.Count <= 0 || !builtObject2.IsAutoControlled)
                {
                    continue;
                }
                Troop troop3 = builtObject2.Troops[0];
                if (troop3 != null && troop3.Empire != null && troop3.Empire.Troops != null)
                {
                    troop3.Empire.Troops.Remove(troop3);
                    troop3.BuiltObject = null;
                    troop3.Colony = null;
                    troop3.Empire = null;
                    builtObject2.Troops.RemoveAt(0);
                    num10++;
                    if (num10 >= num9)
                    {
                        break;
                    }
                }
            }
        }

        private void PayMaintenanceForBuiltObjects(double timePassed)
        {
            double num = timePassed / (double)Galaxy.RealSecondsInGalacticYear;
            double num2 = AnnualStateMaintenanceExcludingUnderConstruction * num;
            _StateMoney -= num2;
            PirateEconomy.PerformExpense(num2, PirateExpenseType.ShipMaintenance, _Galaxy.CurrentStarDate);
            if (PirateEmpireBaseHabitat == null)
            {
                double num3 = AnnualPrivateMaintenanceExcludingUnderConstruction * num;
                PerformPrivateTransaction(0.0 - num3);
            }
            BaconEmpire.PayAnnualMaintenanceCostForFreeTraders(this, timePassed);
        }

        public double GetPrivateFunds()
        {
            if (GovernmentAttributes != null && GovernmentAttributes.SpecialFunctionCode == 1)
            {
                return _StateMoney;
            }
            return _PrivateMoney;
        }

        public double PerformPrivateTransaction(double transactionAmount)
        {
            if (GovernmentAttributes != null && GovernmentAttributes.SpecialFunctionCode == 1)
            {
                _StateMoney += transactionAmount;
                return _StateMoney;
            }
            if (PirateEmpireBaseHabitat != null)
            {
                _StateMoney += transactionAmount;
                return _StateMoney;
            }
            Counters.ProcessColonyRevenue(transactionAmount);
            _PrivateMoney += transactionAmount;
            return _PrivateMoney;
        }

        private void EvaluateViabilityOfPrivateEnterprises()
        {
            for (int i = 0; i < PrivateBuiltObjects.Count; i++)
            {
                BuiltObject builtObject = PrivateBuiltObjects[i];
                if (builtObject.ConsecutiveUnprofitableYears >= 3)
                {
                    AssignScrapMission(builtObject);
                }
            }
        }

        private void EvaluateViabilityOfStateEnterprises()
        {
            int num = Math.Max(1, Colonies.Count / 10);
            int num2 = Math.Max(1, Colonies.Count / 10);
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = BuiltObjects[i];
                if (builtObject.ConsecutiveUnprofitableYears < 3)
                {
                    continue;
                }
                if (builtObject.IsSpacePort)
                {
                    if (SpacePorts.Count > num)
                    {
                        AssignScrapMission(builtObject);
                    }
                }
                else if (builtObject.IsShipYard)
                {
                    if (_ConstructionYards.Count > num2)
                    {
                        AssignScrapMission(builtObject);
                    }
                }
                else if (builtObject.Role != BuiltObjectRole.Military && builtObject.Role != BuiltObjectRole.Exploration && builtObject.Role != BuiltObjectRole.Colony)
                {
                    AssignScrapMission(builtObject);
                }
            }
        }

        public void IdentifyUnavailableLuxuryResources()
        {
            bool canExtract = false;
            if (Research != null && Research.EvaluateDesiredComponent(ComponentType.ExtractorLuxury, ShipDesignFocus.Balanced) != null)
            {
                canExtract = true;
            }
            if (_SelfSuppliedLuxuryResources == null)
            {
                _SelfSuppliedLuxuryResources = new ResourceList();
            }
            _SelfSuppliedLuxuryResources.Clear();
            _UnavailableLuxuryResources.Clear();
            for (int i = 0; i < _Galaxy.ResourceSystem.LuxuryResources.Count; i++)
            {
                ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.LuxuryResources[i];
                if (resourceDefinition != null && !CheckResourceAvailable(resourceDefinition.ResourceID, canExtract))
                {
                    _UnavailableLuxuryResources.Add(new Resource(resourceDefinition.ResourceID));
                }
            }
        }

        private bool CheckResourceAvailable(byte resourceId, bool canExtract)
        {
            if (CheckResourceSelfSupplied(resourceId, canExtract))
            {
                _SelfSuppliedLuxuryResources.Add(new Resource(resourceId));
                return true;
            }
            for (int i = 0; i < _ResourceTargets.Count; i++)
            {
                HabitatPrioritization habitatPrioritization = _ResourceTargets[i];
                if (!canExtract)
                {
                    if (habitatPrioritization.Habitat.Population != null && habitatPrioritization.Habitat.Population.TotalAmount > 0)
                    {
                        int num = habitatPrioritization.Habitat.Resources.IndexOf(resourceId, 0);
                        if (num >= 0)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    int num2 = habitatPrioritization.Habitat.Resources.IndexOf(resourceId, 0);
                    if (num2 >= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckResourceSelfSupplied(byte resourceId, bool canExtract)
        {
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                int num = habitat.Resources.IndexOf(resourceId, 0);
                if (num >= 0)
                {
                    return true;
                }
            }
            if (canExtract)
            {
                for (int j = 0; j < MiningStations.Count; j++)
                {
                    BuiltObject builtObject = MiningStations[j];
                    if (builtObject.ParentHabitat != null)
                    {
                        int num2 = builtObject.ParentHabitat.Resources.IndexOf(resourceId, 0);
                        if (num2 >= 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public HabitatPrioritizationList ResolveResourceSupplyLocations()
        {
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                int num = habitatPrioritizationList.IndexOf(habitat);
                if (num < 0)
                {
                    int priority = (int)DetermineResourceValue(habitat);
                    if (habitat.Resources != null && habitat.Resources.Count > 0)
                    {
                        HabitatPrioritization item = new HabitatPrioritization(habitat, priority);
                        habitatPrioritizationList.Add(item);
                    }
                }
            }
            for (int j = 0; j < MiningStations.Count; j++)
            {
                BuiltObject builtObject = MiningStations[j];
                if (builtObject.ParentHabitat != null)
                {
                    int num2 = habitatPrioritizationList.IndexOf(builtObject.ParentHabitat);
                    if (num2 < 0)
                    {
                        int priority2 = (int)DetermineResourceValue(builtObject.ParentHabitat);
                        HabitatPrioritization item2 = new HabitatPrioritization(builtObject.ParentHabitat, priority2);
                        habitatPrioritizationList.Add(item2);
                    }
                }
            }
            habitatPrioritizationList.Sort();
            habitatPrioritizationList.Reverse();
            return habitatPrioritizationList;
        }

        public HabitatPrioritizationList IdentifyResourceCentres(Galaxy galaxy)
        {
            return IdentifyResourceCentres(galaxy, filterOutAssignedHabitats: true, filterOutDangerousTargets: true);
        }

        public HabitatPrioritizationList IdentifyResourceCentres(Galaxy galaxy, bool filterOutAssignedHabitats, bool filterOutDangerousTargets)
        {
            return IdentifyResourceCentres(galaxy, filterOutAssignedHabitats, filterOutDangerousTargets, includeAsteroids: true);
        }

        public HabitatPrioritizationList IdentifyResourceCentres(Galaxy galaxy, bool filterOutAssignedHabitats, bool filterOutDangerousTargets, bool includeAsteroids)
        {
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            StellarObjectList stellarObjectList = new StellarObjectList();
            if (PirateEmpireBaseHabitat != null)
            {
                stellarObjectList.AddRange(SpacePorts);
            }
            else
            {
                for (int i = 0; i < BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = BuiltObjects[i];
                    if (builtObject != null && !builtObject.HasBeenDestroyed && (builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort))
                    {
                        stellarObjectList.Add(builtObject);
                    }
                }
                for (int j = 0; j < Colonies.Count; j++)
                {
                    Habitat habitat = Colonies[j];
                    if (habitat != null && !habitat.HasBeenDestroyed && !habitat.HasSpacePort && habitat.Population != null && habitat.Population.TotalAmount >= 500000000)
                    {
                        stellarObjectList.Add(habitat);
                    }
                }
            }
            Design design = _Designs.FindNewest(BuiltObjectSubRole.MiningStation);
            int num = 0;
            if (design != null)
            {
                num = design.ExtractionLuxury;
            }
            bool flag = CheckConstructionShipAndMiningStationCanSurviveStorms();
            bool flag2 = true;
            if (Research != null && Research.ResearchedComponents != null && Research.ResearchedComponents.CountByCategory(ComponentCategoryType.HyperDrive) <= 0)
            {
                flag2 = false;
            }
            double num2 = 1.0;
            double num3 = 1.0;
            if (DominantRace != null)
            {
                num2 = (double)DominantRace.AggressionLevel / 100.0;
                num3 = (double)DominantRace.CautionLevel / 100.0;
            }
            int num4 = (int)(1000.0 / (num2 * num2 * num2 / (num3 * num3 * num3)));
            HabitatPrioritizationList habitatPrioritizationList2 = DetermineHabitatsBuildingMiningStations();
            for (int k = 0; k < SystemVisibility.Count; k++)
            {
                if (!CheckSystemExplored(k))
                {
                    continue;
                }
                SystemInfo systemInfo = null;
                if (_Galaxy.Systems.Count > k)
                {
                    systemInfo = _Galaxy.Systems[k];
                }
                if (systemInfo == null || systemInfo.SystemStar == null)
                {
                    continue;
                }
                bool flag3 = false;
                if (PirateEmpireBaseHabitat == null && systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire != null && systemInfo.DominantEmpire.Empire != this)
                {
                    flag3 = true;
                }
                bool flag4 = false;
                bool flag5 = false;
                if (filterOutDangerousTargets)
                {
                    flag4 = _Galaxy.CheckInStorm(systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
                    if (flag4 && flag)
                    {
                        flag4 = false;
                    }
                    flag5 = CheckNearPirateBase(systemInfo.SystemStar, systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos, this);
                }
                if (flag4 || flag5)
                {
                    continue;
                }
                HabitatList habitatList = systemInfo.Habitats;
                StellarObject stellarObject = stellarObjectList.FindNearest(systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos);
                bool flag6 = false;
                if (systemInfo.SystemStar.Category == HabitatCategoryType.GasCloud)
                {
                    habitatList = new HabitatList();
                    habitatList.Add(systemInfo.SystemStar);
                    flag6 = true;
                }
                for (int l = 0; l < habitatList.Count; l++)
                {
                    Habitat habitat2 = habitatList[l];
                    double num5 = 0.0;
                    if (habitat2 == null || habitat2.Resources == null || habitat2.Resources.Count <= 0 || !_ResourceMap.CheckResourcesKnown(habitat2) || (!includeAsteroids && habitat2.Category == HabitatCategoryType.Asteroid) || (habitat2.Owner != null && habitat2.Owner != _Galaxy.IndependentEmpire))
                    {
                        continue;
                    }
                    bool flag7 = true;
                    if (flag6)
                    {
                        if (_Galaxy.DetermineMiningStationAtHabitatForEmpire(habitat2, this) == null)
                        {
                            flag7 = false;
                        }
                    }
                    else if (_Galaxy.DetermineMiningStationAtHabitat(habitat2) == null)
                    {
                        flag7 = false;
                    }
                    if (flag7)
                    {
                        continue;
                    }
                    bool flag8 = true;
                    BuiltObject assignedShip = null;
                    int num6 = habitatPrioritizationList2.IndexOf(habitat2);
                    if (num6 >= 0)
                    {
                        if (filterOutAssignedHabitats)
                        {
                            flag8 = false;
                        }
                        else
                        {
                            assignedShip = habitatPrioritizationList2[num6].AssignedShip;
                        }
                    }
                    if (PirateEmpireBaseHabitat == null && flag8)
                    {
                        flag8 = _Galaxy.CheckEmpireTerritoryCanBuildAtHabitat(this, habitat2);
                    }
                    if (!flag8)
                    {
                        continue;
                    }
                    Habitat habitat3 = habitat2;
                    num5 = ((num <= 0) ? habitat3.CalculateCurrentStrategicResourceValue(_Galaxy) : habitat3.CalculateCurrentCompleteResourceValue(_Galaxy));
                    double x = 0.0;
                    double y = 0.0;
                    if (stellarObject != null)
                    {
                        x = stellarObject.Xpos;
                        y = stellarObject.Ypos;
                    }
                    else if (PirateEmpireBaseHabitat != null)
                    {
                        x = PirateEmpireBaseHabitat.Xpos;
                        y = PirateEmpireBaseHabitat.Ypos;
                    }
                    else if (Capital != null)
                    {
                        x = Capital.Xpos;
                        y = Capital.Ypos;
                    }
                    if (!flag2)
                    {
                        double d = galaxy.CalculateDistance(x, y, habitat3.Xpos, habitat3.Ypos);
                        double num7 = Math.Max(1.0, Math.Sqrt(d) / 10.0);
                        num5 /= num7;
                    }
                    else
                    {
                        double num8 = galaxy.CalculateDistance(x, y, habitat3.Xpos, habitat3.Ypos);
                        num8 -= (double)(Galaxy.MaxSolarSystemSize * 4);
                        num8 = Math.Max(1.0, num8);
                        double num9 = Math.Max(1.0, num8 / 10000.0);
                        num5 /= num9;
                    }
                    if (num5 > 1.0)
                    {
                        bool flag9 = true;
                        if (flag3 && filterOutDangerousTargets && num5 < (double)num4)
                        {
                            flag9 = false;
                        }
                        if (filterOutDangerousTargets && CheckWhetherHabitatIsDangerous(habitat3))
                        {
                            flag9 = false;
                        }
                        if (flag9)
                        {
                            HabitatPrioritization habitatPrioritization = new HabitatPrioritization(habitat3, (int)num5);
                            habitatPrioritization.AssignedShip = assignedShip;
                            habitatPrioritizationList.Add(habitatPrioritization);
                        }
                    }
                }
            }
            habitatPrioritizationList.Sort();
            habitatPrioritizationList.Reverse();
            return habitatPrioritizationList;
        }

        private bool CheckWhetherBuildingMiningStationAtHabitat(Habitat habitat)
        {
            for (int i = 0; i < ConstructionShips.Count; i++)
            {
                BuiltObject builtObject = ConstructionShips[i];
                if (builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Build && builtObject.Mission.TargetHabitat != null)
                {
                    Habitat targetHabitat = builtObject.Mission.TargetHabitat;
                    if (targetHabitat == habitat)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public HabitatList DetermineHabitatsWithBasesIncludingBuilding(List<BuiltObjectSubRole> subRoles)
        {
            HabitatList habitatList = new HabitatList();
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(BuiltObjects);
            builtObjectList.AddRange(PrivateBuiltObjects);
            for (int i = 0; i < builtObjectList.Count; i++)
            {
                BuiltObject builtObject = builtObjectList[i];
                if (builtObject == null || builtObject.HasBeenDestroyed)
                {
                    continue;
                }
                switch (builtObject.SubRole)
                {
                    case BuiltObjectSubRole.ConstructionShip:
                        if (builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Build && builtObject.Mission.Design != null && subRoles.Contains(builtObject.Mission.Design.SubRole) && builtObject.Mission.TargetHabitat != null)
                        {
                            habitatList.Add(builtObject.Mission.TargetHabitat);
                        }
                        break;
                    case BuiltObjectSubRole.GasMiningStation:
                    case BuiltObjectSubRole.MiningStation:
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
                        if (builtObject.ParentHabitat != null)
                        {
                            habitatList.Add(builtObject.ParentHabitat);
                        }
                        break;
                }
            }
            return habitatList;
        }

        public HabitatList DetermineHabitatsBeingMinedIncludingBuildingMiningStations(bool includeMiningShips)
        {
            HabitatList habitatList = new HabitatList();
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(BuiltObjects);
            builtObjectList.AddRange(PrivateBuiltObjects);
            for (int i = 0; i < builtObjectList.Count; i++)
            {
                BuiltObject builtObject = builtObjectList[i];
                if (builtObject == null || builtObject.HasBeenDestroyed)
                {
                    continue;
                }
                switch (builtObject.SubRole)
                {
                    case BuiltObjectSubRole.ConstructionShip:
                        if (builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Build && builtObject.Mission.Design != null && (builtObject.Mission.Design.SubRole == BuiltObjectSubRole.GasMiningStation || builtObject.Mission.Design.SubRole == BuiltObjectSubRole.MiningStation) && builtObject.Mission.TargetHabitat != null)
                        {
                            habitatList.Add(builtObject.Mission.TargetHabitat);
                        }
                        break;
                    case BuiltObjectSubRole.GasMiningShip:
                    case BuiltObjectSubRole.MiningShip:
                        if (includeMiningShips && builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.ExtractResources && builtObject.Mission.TargetHabitat != null)
                        {
                            habitatList.Add(builtObject.Mission.TargetHabitat);
                        }
                        break;
                    case BuiltObjectSubRole.GasMiningStation:
                    case BuiltObjectSubRole.MiningStation:
                        if (builtObject.ParentHabitat != null)
                        {
                            habitatList.Add(builtObject.ParentHabitat);
                        }
                        break;
                    case BuiltObjectSubRole.SmallSpacePort:
                    case BuiltObjectSubRole.MediumSpacePort:
                    case BuiltObjectSubRole.LargeSpacePort:
                        if ((builtObject.ExtractionGas > 0 || builtObject.ExtractionMine > 0 || builtObject.ExtractionLuxury > 0) && builtObject.ParentHabitat != null)
                        {
                            habitatList.Add(builtObject.ParentHabitat);
                        }
                        break;
                }
            }
            return habitatList;
        }

        private HabitatList DetermineHabitatsBeingMined(HabitatList minedHabitats, BuiltObjectList builtObjects)
        {
            for (int i = 0; i < builtObjects.Count; i++)
            {
                BuiltObject builtObject = builtObjects[i];
                if (builtObject.IsResourceExtractor && builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.ExtractResources && builtObject.Mission.TargetHabitat != null)
                {
                    minedHabitats.Add(builtObject.Mission.TargetHabitat);
                }
                if ((builtObject.SubRole == BuiltObjectSubRole.GasMiningStation || builtObject.SubRole == BuiltObjectSubRole.MiningStation) && builtObject.ParentHabitat != null)
                {
                    minedHabitats.Add(builtObject.ParentHabitat);
                }
            }
            return minedHabitats;
        }

        private HabitatPrioritizationList DetermineHabitatsBuildingMiningStations()
        {
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            for (int i = 0; i < ConstructionShips.Count; i++)
            {
                BuiltObject builtObject = ConstructionShips[i];
                if (builtObject.Mission == null || builtObject.Mission.Type != BuiltObjectMissionType.Build)
                {
                    continue;
                }
                if (builtObject.Mission.TargetHabitat != null)
                {
                    HabitatPrioritization habitatPrioritization = new HabitatPrioritization(builtObject.Mission.TargetHabitat, 0);
                    habitatPrioritization.AssignedShip = builtObject;
                    habitatPrioritizationList.Add(habitatPrioritization);
                }
                if (builtObject.SubsequentMissions == null || builtObject.SubsequentMissions.Count <= 0)
                {
                    continue;
                }
                foreach (BuiltObjectMission subsequentMission in builtObject.SubsequentMissions)
                {
                    if (subsequentMission != null && subsequentMission.Type == BuiltObjectMissionType.Build && subsequentMission.TargetHabitat != null)
                    {
                        HabitatPrioritization habitatPrioritization2 = new HabitatPrioritization(subsequentMission.TargetHabitat, 0);
                        habitatPrioritization2.AssignedShip = builtObject;
                        habitatPrioritizationList.Add(habitatPrioritization2);
                    }
                }
            }
            return habitatPrioritizationList;
        }

        private HabitatList DetermineHabitatsBeingColonized()
        {
            HabitatList habitatList = new HabitatList();
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = BuiltObjects[i];
                if (builtObject.IsColony && builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Colonize && builtObject.Mission.TargetHabitat != null)
                {
                    Habitat targetHabitat = builtObject.Mission.TargetHabitat;
                    if (!habitatList.Contains(targetHabitat))
                    {
                        habitatList.Add(targetHabitat);
                    }
                }
            }
            return habitatList;
        }

        private void MaintainColonyCriticalResourceLevels(BuiltObject spacePort, Habitat colony)
        {
            if (colony == null || colony.HasBeenDestroyed)
            {
                return;
            }
            ResourceList resourceList = colony.DetermineCriticalResources();
            if (resourceList.Count <= 0)
            {
                return;
            }
            OrderList orders = _Galaxy.Orders.GetOrders(colony);
            if (spacePort != null && spacePort.IsSpacePort)
            {
                OrderList orders2 = _Galaxy.Orders.GetOrders(spacePort);
                if (orders2.Count > 0)
                {
                    orders.AddRange(orders2);
                }
            }
            for (int i = 0; i < resourceList.Count; i++)
            {
                Resource resource = resourceList[i];
                CheckAndOrderResource(colony, orders, spacePort, resource, isCriticalResource: true);
            }
        }

        private void MaintainColonyResourceLevels(BuiltObject spacePort, Habitat colony)
        {
            OrderList orders = _Galaxy.Orders.GetOrders(colony);
            if (spacePort != null && spacePort.IsSpacePort)
            {
                OrderList orders2 = _Galaxy.Orders.GetOrders(spacePort);
                if (orders2.Count > 0)
                {
                    orders.AddRange(orders2);
                }
            }
            int num = 0;
            if (ShipGroups != null)
            {
                for (int i = 0; i < ShipGroups.Count; i++)
                {
                    ShipGroup shipGroup = ShipGroups[i];
                    if (shipGroup != null && shipGroup.GatherPoint != null && shipGroup.GatherPoint == colony)
                    {
                        num += shipGroup.TotalFuelCapacity;
                    }
                }
            }
            Resource resource = null;
            for (int j = 0; j < _Galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; j++)
            {
                ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance[j];
                if (resourceDefinition != null)
                {
                    resource = new Resource(resourceDefinition.ResourceID);
                    if (resourceDefinition.IsFuel)
                    {
                        CheckAndOrderResource(colony, orders, spacePort, resource, isCriticalResource: false, num);
                    }
                    else
                    {
                        CheckAndOrderResource(colony, orders, spacePort, resource);
                    }
                }
            }
        }

        private void MaintainPirateSpaceportResourceLevels()
        {
            if (PirateEmpireBaseHabitat == null || SpacePorts == null)
            {
                return;
            }
            for (int i = 0; i < SpacePorts.Count; i++)
            {
                BuiltObject builtObject = SpacePorts[i];
                if (builtObject == null || builtObject.HasBeenDestroyed || !builtObject.IsFunctional || !builtObject.IsSpacePort)
                {
                    continue;
                }
                OrderList orders = _Galaxy.Orders.GetOrders(builtObject);
                Resource resource = null;
                for (int j = 0; j < _Galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; j++)
                {
                    ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance[j];
                    if (resourceDefinition != null)
                    {
                        resource = new Resource(resourceDefinition.ResourceID);
                        CheckAndOrderResourcePirates(orders, builtObject, resource);
                    }
                }
            }
        }

        private void CheckAndOrderResourcePirates(OrderList spaceportOrders, BuiltObject pirateSpaceport, Resource resource)
        {
            int amountToOrder = 0;
            int num = Galaxy.CalculateResourceLevelPirates(resource, pirateSpaceport);
            int minimumResourceLevel = (int)((double)num * 0.6);
            if (!CheckResourceMeetsMinimumLevel(resource, minimumResourceLevel, num, pirateSpaceport, spaceportOrders, out amountToOrder))
            {
                double num2 = (double)amountToOrder * _Galaxy.ResourceCurrentPrices[resource.ResourceID];
                if (num2 < GetPrivateFunds())
                {
                    CreateOrder(pirateSpaceport, resource, amountToOrder, isState: false, OrderType.Standard);
                }
            }
        }

        private bool CheckResourceMeetsMinimumLevel(Resource resource, int minimumResourceLevel, int maximumResourceLevel, BuiltObject pirateSpaceport, OrderList spaceportOrders, out int amountToOrder)
        {
            bool result = false;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = -1;
            if (pirateSpaceport.Cargo != null && pirateSpaceport.Cargo.GetExists(resource))
            {
                num4 = pirateSpaceport.Cargo.IndexOf(resource, pirateSpaceport.Owner);
            }
            if (num4 >= 0)
            {
                num = pirateSpaceport.Cargo[num4].Available;
                num2 = num;
            }
            int num5;
            for (num5 = spaceportOrders.IndexOf(resource.ResourceID, 0); num5 >= 0; num5 = spaceportOrders.IndexOf(resource.ResourceID, num5))
            {
                num3 = spaceportOrders[num5].AmountRequested;
                num2 += num3;
                num5++;
            }
            amountToOrder = Math.Max(0, maximumResourceLevel - num2);
            if (amountToOrder > 0)
            {
                if (resource.IsRestrictedResource)
                {
                    amountToOrder = Math.Max(amountToOrder, Galaxy.MinimumRestrictedResourceReorderAmount);
                }
                else
                {
                    amountToOrder = Math.Max(amountToOrder, Galaxy.MinimumContractSize);
                }
            }
            if (num2 >= minimumResourceLevel)
            {
                result = true;
            }
            return result;
        }

        private void CheckAndOrderResource(Habitat colony, OrderList colonyOrders, BuiltObject colonySpacePort, Resource resource)
        {
            CheckAndOrderResource(colony, colonyOrders, colonySpacePort, resource, isCriticalResource: false);
        }

        private void CheckAndOrderResource(Habitat colony, OrderList colonyOrders, BuiltObject colonySpacePort, Resource resource, bool isCriticalResource)
        {
            CheckAndOrderResource(colony, colonyOrders, colonySpacePort, resource, isCriticalResource: false, 0);
        }

        private void CheckAndOrderResource(Habitat colony, OrderList colonyOrders, BuiltObject colonySpacePort, Resource resource, bool isCriticalResource, int fleetFuelAmount)
        {
            int amountToOrder = 0;
            int num = Galaxy.CalculateResourceLevel(resource, colony, isMiningStation: false, isIndependent: false, isCriticalResource, fleetFuelAmount);
            int minimumResourceLevel = (int)((double)num * 0.6);
            if (CheckResourceMeetsMinimumLevel(resource, minimumResourceLevel, num, colony, colonyOrders, out amountToOrder))
            {
                return;
            }
            double num2 = (double)amountToOrder * _Galaxy.ResourceCurrentPrices[resource.ResourceID];
            if (num2 < GetPrivateFunds())
            {
                if (colonySpacePort != null && colonySpacePort.IsSpacePort)
                {
                    CreateOrder(colonySpacePort, resource, amountToOrder, isState: false, OrderType.Standard);
                }
                else
                {
                    CreateOrder(colony, resource, amountToOrder, isState: false, OrderType.Standard);
                }
            }
        }

        private void CheckForStrandedShips()
        {
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = BuiltObjects[i];
                if (builtObject.DamagedComponentCount > 0 && builtObject.Role != BuiltObjectRole.Base && builtObject.WarpSpeed <= 0 && builtObject.BuiltAt == null && !builtObject.StrandedMessageSent)
                {
                    string arg = string.Empty;
                    if (builtObject.NearestSystemStar != null)
                    {
                        arg = builtObject.NearestSystemStar.Name;
                    }
                    string description = string.Format(TextResolver.GetText("Stranded Ship SHIPTYPE NAME SYSTEM"), Galaxy.ResolveDescription(builtObject.SubRole), builtObject.Name, arg);
                    SendMessageToEmpire(this, EmpireMessageType.ShipNeedsRepair, builtObject, description);
                    builtObject.StrandedMessageSent = true;
                }
            }
            for (int j = 0; j < PrivateBuiltObjects.Count; j++)
            {
                BuiltObject builtObject2 = PrivateBuiltObjects[j];
                if (builtObject2.DamagedComponentCount > 0 && builtObject2.Role != BuiltObjectRole.Base && builtObject2.WarpSpeed <= 0 && builtObject2.BuiltAt == null && !builtObject2.StrandedMessageSent)
                {
                    string arg2 = string.Empty;
                    if (builtObject2.NearestSystemStar != null)
                    {
                        arg2 = builtObject2.NearestSystemStar.Name;
                    }
                    string description2 = string.Format(TextResolver.GetText("Stranded Ship SHIPTYPE NAME SYSTEM"), Galaxy.ResolveDescription(builtObject2.SubRole), builtObject2.Name, arg2);
                    SendMessageToEmpire(this, EmpireMessageType.ShipNeedsRepair, builtObject2, description2);
                    builtObject2.StrandedMessageSent = true;
                }
            }
        }

        public void CalculateColonyTaxResistance()
        {
            long num = 20000000000L;
            long num2 = 200000000000L;
            long num3 = num2 - num;
            long totalPopulation = TotalPopulation;
            double num4 = (double)Math.Max(0L, totalPopulation - num) / (double)num3;
            if (double.IsNaN(num4))
            {
                num4 = 0.0;
            }
            num4 = Math.Min(num4, 1.0);
            _TaxResistanceThreshold = 5000000000L - (long)(num4 * 2000000000.0);
            _TaxResistanceRate = 1.05 + num4 * 0.45;
            if (double.IsNaN(_TaxResistanceRate))
            {
                _TaxResistanceRate = 1.05;
            }
        }

        public void CheckColoniesForBaseFacilities()
        {
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                habitat.CheckForSpacePortFacilities(_Galaxy);
            }
        }

        private void EvaluateColonyVariablesPirate(Galaxy galaxy, double timePassed)
        {
            long num = 0L;
            double troopStrengthNeutralizationAmount = (double)Galaxy.TroopStrengthAnnualNeutralizationAmount * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
            double troopSizeRegenerationAmount = (double)Galaxy.TroopSizeAnnualRegenerationAmount * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
            double troopRecruitmentAmount = (double)Galaxy.TroopAnnualRecruitmentAmount * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
            Troop strongestEmpireTroop = IdentifyStrongestRaceAttackTroop();
            double totalIncome = CalculateAccurateAnnualIncome();
            double annualStateMaintenance = AnnualStateMaintenance;
            double annualFacilityMaintenance = AnnualFacilityMaintenance;
            double minimumShipSpending = MinimumShipSpending;
            ResourceList resourceList = _Galaxy.ShowCheapestLuxuryResources();
            ResourceList resourceList2 = _Galaxy.ShowAvailableRestrictedResourcesForEmpire(this);
            ResourceList resourceList3 = _Galaxy.ShowAvailableRestrictedResourcesForEmpireSelfSupplied(this);
            if (_SelfSuppliedLuxuryResources == null)
            {
                _SelfSuppliedLuxuryResources = new ResourceList();
            }
            Resource[] array = resourceList.ToArray();
            for (int num2 = array.Length - 1; num2 > 0; num2--)
            {
                if (_SelfSuppliedLuxuryResources.Contains(array[num2]))
                {
                    resourceList.RemoveAt(num2);
                    resourceList.Insert(0, array[num2]);
                }
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (_UnavailableLuxuryResources.Contains(array[i]))
                {
                    resourceList.RemoveAt(i);
                    resourceList.Add(array[i]);
                }
            }
            for (int j = 0; j < Colonies.Count; j++)
            {
                Habitat habitat = Colonies[j];
                if (habitat != null && habitat.Owner == this)
                {
                    if (habitat.HasBeenDestroyed)
                    {
                        if (habitat.Explosion != null)
                        {
                            continue;
                        }
                        if (_Galaxy.Habitats.Contains(habitat))
                        {
                            if (!habitat.DoingRemove)
                            {
                                habitat.DoPlanetRemove();
                            }
                        }
                        else
                        {
                            Colonies.Remove(habitat);
                        }
                        continue;
                    }
                    double num3 = Math.Max(0.2, (15.0 + habitat.TaxApproval) / 30.0);
                    BuiltObject builtObject = null;
                    for (int k = 0; k < SpacePorts.Count; k++)
                    {
                        BuiltObject builtObject2 = SpacePorts[k];
                        if (builtObject2.ParentHabitat == habitat && builtObject2.IsSpacePort)
                        {
                            builtObject = builtObject2;
                            break;
                        }
                    }
                    bool flag = true;
                    if (_ControlColonyStockLevels)
                    {
                        MaintainColonyCriticalResourceLevels(builtObject, habitat);
                        MaintainColonyResourceLevels(builtObject, habitat);
                    }
                    habitat.RecalculateCriticalResourceSupplyBonuses();
                    int num4 = 0;
                    int num5 = 0;
                    if (habitat.Cargo != null)
                    {
                        num4 = habitat.Cargo.ResourceGroupCount(ResourceGroup.Luxury, habitat.Owner);
                        num5 = habitat.Cargo.RestrictedResourceCount(habitat.Owner);
                    }
                    Race dominantRace = habitat.Population.DominantRace;
                    if (dominantRace == null)
                    {
                        dominantRace = DominantRace;
                    }
                    galaxy.CalculateMaximumOrderFulfillmentDistance(habitat);
                    int num6 = habitat.CalculateMinimumLuxuryResourceLevel();
                    int num7 = habitat.CalculateMinimumLuxuryResourceLevelRestricted();
                    int val = (int)((double)num7 * 1.5);
                    double num8 = habitat.CalculateStrategicResourceSupplyGrowthFactor();
                    if (num8 > 0.0)
                    {
                        double num9 = timePassed / (double)Galaxy.RealSecondsInGalacticYear * (double)Galaxy.ColonyDevelopmentLevelMaximumAnnualChange;
                        num9 *= _EconomyEfficiency;
                        num9 *= num8;
                        int num10 = habitat.GetDevelopmentLevel() / 5;
                        double num11 = num4 - num10;
                        if (num5 > 0)
                        {
                            habitat.RestrictedResourcesPresent = true;
                        }
                        else
                        {
                            habitat.RestrictedResourcesPresent = false;
                        }
                        double num12 = 0.0;
                        num12 = ((num11 == 0.0) ? 0.0 : ((!(num11 < 0.0)) ? Math.Min(num9, num11) : Math.Max(num9, num11)));
                        if (_EconomyEfficiency < 0.9)
                        {
                            num12 = -1.0;
                        }
                        int developmentLevel = Math.Max(0, habitat.GetDevelopmentLevel() + (int)num12);
                        habitat.SetDevelopmentLevel(developmentLevel);
                        if ((int)num12 >= 1)
                        {
                            _Galaxy.DoCharacterEvent(CharacterEventType.ColonyDevelopmentIncrease, habitat, habitat.Characters, includeLeader: true, this);
                        }
                        else if ((int)num12 <= -1)
                        {
                            _Galaxy.DoCharacterEvent(CharacterEventType.ColonyDevelopmentDecrease, habitat, habitat.Characters, includeLeader: true, this);
                        }
                    }
                    double num13 = 1.0 + habitat.EmpireApprovalRating / 200.0;
                    if (num8 > 0.0 && flag)
                    {
                        double num14 = 1.0;
                        if (Characters != null)
                        {
                            CharacterList characterList = Characters.FindCharactersAtLocationNotTransferring(habitat, CharacterRole.ColonyGovernor);
                            int num15 = -100;
                            for (int l = 0; l < characterList.Count; l++)
                            {
                                num15 = Math.Max(num15, characterList[l].PopulationGrowth);
                            }
                            if (num15 <= -100)
                            {
                                num15 = 0;
                            }
                            num14 *= 1.0 + (double)num15 / 100.0;
                            CharacterList charactersByRole = Characters.GetCharactersByRole(CharacterRole.Leader);
                            int num16 = -100;
                            for (int m = 0; m < charactersByRole.Count; m++)
                            {
                                if (charactersByRole[m].Location != null && charactersByRole[m].Location is BuiltObject && ((BuiltObject)charactersByRole[m].Location).ParentHabitat != null && ((BuiltObject)charactersByRole[m].Location).ParentHabitat == PirateEmpireBaseHabitat)
                                {
                                    num16 = Math.Max(num16, charactersByRole[m].PopulationGrowth);
                                }
                            }
                            if (num16 <= -100)
                            {
                                num16 = 0;
                            }
                            num14 *= 1.0 + (double)num16 / 100.0;
                        }
                        for (int n = 0; n < habitat.Population.Count; n++)
                        {
                            Population population = habitat.Population[n];
                            double num17 = (double)Math.Min(5, Math.Max(1, num4)) / 5.0 * num3 * (population.Race.ReproductiveRate - 1.0) * num13 * (double)habitat.Quality;
                            float num18 = CalculateColonyGrowthRateMultiplier(population.Race, habitat);
                            num17 *= (double)num18;
                            num17 *= habitat.GrowthFactor;
                            if (GovernmentAttributes != null)
                            {
                                num17 *= GovernmentAttributes.PopulationGrowth;
                            }
                            num17 *= _EconomyEfficiency;
                            num17 *= PopulationGrowthRate;
                            num17 = Math.Min(1.0, Math.Max(num17, 0.01));
                            if (habitat.Population.TotalAmount < 500000000)
                            {
                                num17 += num17 * 0.5;
                            }
                            if (SpecialBonusPopulationGrowth > 0.0)
                            {
                                num17 *= 1.0 + SpecialBonusPopulationGrowth;
                            }
                            double bonusTotalByEffectType = habitat.ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.PopulationGrowthRate);
                            double num19 = 1.0 + bonusTotalByEffectType / 100.0;
                            num17 *= num19;
                            if (habitat.Population != null && dominantRace.ColonyPopulationPolicyGrowthFactorExterminate != 1.0)
                            {
                                bool flag2 = false;
                                for (int num20 = 0; num20 < habitat.Population.Count; num20++)
                                {
                                    Population population2 = habitat.Population[num20];
                                    if (population2 != null && population2.Race != null)
                                    {
                                        if (habitat.ColonyPopulationPolicyRaceFamily == ColonyPopulationPolicy.Exterminate && population2.Race != dominantRace && population2.Race.FamilyId == dominantRace.FamilyId)
                                        {
                                            flag2 = true;
                                            break;
                                        }
                                        if (habitat.ColonyPopulationPolicy == ColonyPopulationPolicy.Exterminate && population2.Race != dominantRace && population2.Race.FamilyId != dominantRace.FamilyId)
                                        {
                                            flag2 = true;
                                            break;
                                        }
                                    }
                                }
                                if (flag2)
                                {
                                    num17 *= dominantRace.ColonyPopulationPolicyGrowthFactorExterminate;
                                }
                            }
                            num17 *= num14;
                            num17 *= num8;
                            ColonyPopulationPolicy colonyPopulationPolicy = ColonyPopulationPolicy.Assimilate;
                            if (DominantRace != null && population.Race != DominantRace)
                            {
                                colonyPopulationPolicy = habitat.ColonyPopulationPolicy;
                                if (population.Race.FamilyId == DominantRace.FamilyId)
                                {
                                    colonyPopulationPolicy = habitat.ColonyPopulationPolicyRaceFamily;
                                }
                            }
                            switch (colonyPopulationPolicy)
                            {
                                case ColonyPopulationPolicy.Resettle:
                                case ColonyPopulationPolicy.Enslave:
                                case ColonyPopulationPolicy.Exterminate:
                                    num17 = 0.0;
                                    break;
                            }
                            population.GrowthRate = 1f + (float)num17;
                            num += population.Amount;
                        }
                    }
                    else
                    {
                        foreach (Population item in habitat.Population)
                        {
                            item.GrowthRate = 1f;
                            num += item.Amount;
                        }
                    }
                    habitat.Population.RecalculateTotalAmount();
                    OrderList orders = _Galaxy.Orders.GetOrders(habitat);
                    if (builtObject != null)
                    {
                        OrderList orders2 = _Galaxy.Orders.GetOrders(builtObject);
                        if (orders2 != null)
                        {
                            orders.AddRange(orders2);
                        }
                    }
                    int num21 = 10;
                    if (habitat.Population.TotalAmount < 200000000)
                    {
                        num21 = 5;
                    }
                    int num22 = 0;
                    num22 += CheckResourcesMeetingMinimumLevel(ResourceGroup.Luxury, num6, habitat, orders);
                    int num23 = num21 - num22;
                    Resource resource = null;
                    if (_ControlColonyDevelopment)
                    {
                        for (int num24 = 0; num24 < num23; num24++)
                        {
                            resource = resourceList[num24];
                            int iterationCount = 0;
                            bool flag3 = false;
                            do
                            {
                                flag3 = false;
                                resource = resourceList[num24];
                                foreach (HabitatResource resource5 in habitat.Resources)
                                {
                                    if (resource5.ResourceID == resource.ResourceID)
                                    {
                                        flag3 = true;
                                        num24++;
                                    }
                                }
                            }
                            while (Galaxy.ConditionCheckLimit(flag3, 100, ref iterationCount));
                            int amountToOrder = 0;
                            int maximumResourceLevel = (int)((double)num6 * 1.5);
                            CheckResourceMeetsMinimumLevel(resource, num6, maximumResourceLevel, habitat, orders, out amountToOrder);
                            if (amountToOrder <= 0)
                            {
                                continue;
                            }
                            double num25 = (double)amountToOrder * _Galaxy.ResourceCurrentPrices[resource.ResourceID];
                            if (num25 < GetPrivateFunds())
                            {
                                if (builtObject != null && builtObject.IsSpacePort)
                                {
                                    CreateOrder(builtObject, resource, amountToOrder, isState: false, OrderType.Standard, allowExpiry: true);
                                }
                                else
                                {
                                    CreateOrder(habitat, resource, amountToOrder, isState: false, OrderType.Standard, allowExpiry: true);
                                }
                            }
                        }
                        if (resourceList2.Count > 0 && habitat.DevelopmentLevel >= 80)
                        {
                            int num26 = 0;
                            int maximumResourceLevel2 = num7 * 2;
                            Resource resource2 = null;
                            int num27 = 0;
                            if (resourceList3 != null && resourceList3.Count > 0)
                            {
                                for (int num28 = 0; num28 < _Galaxy.ResourceSystem.SuperLuxuryResources.Count; num28++)
                                {
                                    Resource resource3 = new Resource(_Galaxy.ResourceSystem.SuperLuxuryResources[num28].ResourceID);
                                    if (resourceList3.Contains(resource3))
                                    {
                                        int amountToOrder2 = 0;
                                        CheckResourceMeetsMinimumLevel(resource3, num7, maximumResourceLevel2, habitat, orders, out amountToOrder2);
                                        if (amountToOrder2 > num26)
                                        {
                                            num26 = amountToOrder2;
                                            resource2 = resource3;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int num29 = 0; num29 < _Galaxy.ResourceSystem.SuperLuxuryResources.Count; num29++)
                                {
                                    Resource resource4 = new Resource(_Galaxy.ResourceSystem.SuperLuxuryResources[num29].ResourceID);
                                    if (resourceList2.Contains(resource4))
                                    {
                                        int amountToOrder3 = 0;
                                        CheckResourceMeetsMinimumLevel(resource4, num7, maximumResourceLevel2, habitat, orders, out amountToOrder3);
                                        if (amountToOrder3 > num26)
                                        {
                                            num26 = amountToOrder3;
                                            resource2 = resource4;
                                        }
                                    }
                                }
                            }
                            num27 = Math.Max(val, num26);
                            if (resource2 != null && num27 > 0)
                            {
                                double num30 = (double)num27 * _Galaxy.ResourceCurrentPrices[resource2.ResourceID];
                                if (num30 < GetPrivateFunds())
                                {
                                    if (builtObject != null && builtObject.IsSpacePort)
                                    {
                                        CreateOrder(builtObject, resource2, num27, isState: false, OrderType.Standard, allowExpiry: true);
                                    }
                                    else
                                    {
                                        CreateOrder(habitat, resource2, num27, isState: false, OrderType.Standard, allowExpiry: true);
                                    }
                                }
                            }
                        }
                    }
                    habitat.RecalculateAnnualTaxRevenue();
                    ProcessColonyTroops(habitat, strongestEmpireTroop, troopStrengthNeutralizationAmount, troopSizeRegenerationAmount, troopRecruitmentAmount, totalIncome, annualStateMaintenance, 0.0, annualFacilityMaintenance, 0.0, 0.0, minimumShipSpending, atWar: false, null, performRecruitment: true);
                }
                _TotalPopulation = num;
            }
        }

        private void EvaluateColonyVariables(Galaxy galaxy, double timePassed)
        {
            long num = 0L;
            bool atWar = CheckAtWar();
            StellarObjectList defendColonies = ResolveLocationsToDefend(includeBases: false);
            double troopStrengthNeutralizationAmount = (double)Galaxy.TroopStrengthAnnualNeutralizationAmount * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
            double troopSizeRegenerationAmount = (double)Galaxy.TroopSizeAnnualRegenerationAmount * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
            double troopRecruitmentAmount = (double)Galaxy.TroopAnnualRecruitmentAmount * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
            Troop strongestEmpireTroop = IdentifyStrongestRaceAttackTroop();
            double totalIncome = CalculateAccurateAnnualIncome();
            double annualStateMaintenance = AnnualStateMaintenance;
            double annualPirateProtection = AnnualPirateProtection;
            double tribute = AnnualSubjugationTribute + AnnualPirateProtection;
            double annualFacilityMaintenance = AnnualFacilityMaintenance;
            double minimumIntelligenceAgentSpending = MinimumIntelligenceAgentSpending;
            double minimumShipSpending = MinimumShipSpending;
            ResourceList resourceList = _Galaxy.ShowCheapestLuxuryResources();
            ResourceList resourceList2 = _Galaxy.ShowAvailableRestrictedResourcesForEmpire(this);
            ResourceList resourceList3 = _Galaxy.ShowAvailableRestrictedResourcesForEmpireSelfSupplied(this);
            if (_SelfSuppliedLuxuryResources == null)
            {
                _SelfSuppliedLuxuryResources = new ResourceList();
            }
            Resource[] array = resourceList.ToArray();
            for (int num2 = array.Length - 1; num2 > 0; num2--)
            {
                if (_SelfSuppliedLuxuryResources.Contains(array[num2]))
                {
                    resourceList.RemoveAt(num2);
                    resourceList.Insert(0, array[num2]);
                }
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (_UnavailableLuxuryResources.Contains(array[i]))
                {
                    resourceList.RemoveAt(i);
                    resourceList.Add(array[i]);
                }
            }
            for (int j = 0; j < Colonies.Count; j++)
            {
                Habitat habitat = Colonies[j];
                if (habitat.HasBeenDestroyed)
                {
                    if (habitat.Explosion != null)
                    {
                        continue;
                    }
                    if (_Galaxy.Habitats.Contains(habitat))
                    {
                        if (!habitat.DoingRemove)
                        {
                            habitat.DoPlanetRemove();
                        }
                    }
                    else
                    {
                        Colonies.Remove(habitat);
                    }
                    continue;
                }
                double num3 = Math.Max(0.2, (15.0 + habitat.TaxApproval) / 30.0);
                BuiltObject builtObject = null;
                for (int k = 0; k < SpacePorts.Count; k++)
                {
                    BuiltObject builtObject2 = SpacePorts[k];
                    if (builtObject2.ParentHabitat == habitat && builtObject2.IsSpacePort)
                    {
                        builtObject = builtObject2;
                        break;
                    }
                }
                bool flag = true;
                if (_ControlColonyStockLevels)
                {
                    MaintainColonyCriticalResourceLevels(builtObject, habitat);
                    MaintainColonyResourceLevels(builtObject, habitat);
                }
                habitat.RecalculateCriticalResourceSupplyBonuses();
                int num4 = 0;
                int num5 = 0;
                if (habitat.Cargo != null)
                {
                    num4 = habitat.Cargo.ResourceGroupCount(ResourceGroup.Luxury, habitat.Owner);
                    num5 = habitat.Cargo.RestrictedResourceCount(habitat.Owner);
                }
                Race dominantRace = habitat.Population.DominantRace;
                if (dominantRace == null)
                {
                    dominantRace = DominantRace;
                }
                galaxy.CalculateMaximumOrderFulfillmentDistance(habitat);
                int num6 = habitat.CalculateMinimumLuxuryResourceLevel();
                int num7 = habitat.CalculateMinimumLuxuryResourceLevelRestricted();
                int val = (int)((double)num7 * 1.5);
                double num8 = habitat.CalculateStrategicResourceSupplyGrowthFactor();
                if (num8 > 0.0)
                {
                    double num9 = timePassed / (double)Galaxy.RealSecondsInGalacticYear * (double)Galaxy.ColonyDevelopmentLevelMaximumAnnualChange;
                    num9 *= _EconomyEfficiency;
                    num9 *= num8;
                    int num10 = habitat.GetDevelopmentLevel() / 5;
                    double num11 = num4 - num10;
                    if (num5 > 0)
                    {
                        habitat.RestrictedResourcesPresent = true;
                    }
                    else
                    {
                        habitat.RestrictedResourcesPresent = false;
                    }
                    double num12 = 0.0;
                    num12 = ((num11 == 0.0) ? 0.0 : ((!(num11 < 0.0)) ? Math.Min(num9, num11) : Math.Max(num9, num11)));
                    if (_EconomyEfficiency < 0.9)
                    {
                        num12 = -1.0;
                    }
                    int developmentLevel = Math.Max(0, habitat.GetDevelopmentLevel() + (int)num12);
                    habitat.SetDevelopmentLevel(developmentLevel);
                    if ((int)num12 >= 1)
                    {
                        _Galaxy.DoCharacterEvent(CharacterEventType.ColonyDevelopmentIncrease, habitat, habitat.Characters, includeLeader: true, this);
                    }
                    else if ((int)num12 <= -1)
                    {
                        _Galaxy.DoCharacterEvent(CharacterEventType.ColonyDevelopmentDecrease, habitat, habitat.Characters, includeLeader: true, this);
                    }
                }
                double num13 = 1.0 + habitat.EmpireApprovalRating / 200.0;
                if (num8 > 0.0 && flag)
                {
                    double num14 = 1.0;
                    if (Characters != null)
                    {
                        CharacterList characterList = Characters.FindCharactersAtLocationNotTransferring(habitat, CharacterRole.ColonyGovernor);
                        int num15 = -100;
                        for (int l = 0; l < characterList.Count; l++)
                        {
                            num15 = Math.Max(num15, characterList[l].PopulationGrowth);
                        }
                        if (num15 <= -100)
                        {
                            num15 = 0;
                        }
                        num14 *= 1.0 + (double)num15 / 100.0;
                        CharacterList charactersByRole = Characters.GetCharactersByRole(CharacterRole.Leader);
                        int num16 = -100;
                        for (int m = 0; m < charactersByRole.Count; m++)
                        {
                            if (charactersByRole[m].Location != null && charactersByRole[m].Location is Habitat && Capitals.Contains((Habitat)charactersByRole[m].Location))
                            {
                                num16 = Math.Max(num16, charactersByRole[m].PopulationGrowth);
                            }
                        }
                        if (num16 <= -100)
                        {
                            num16 = 0;
                        }
                        num14 *= 1.0 + (double)num16 / 100.0;
                    }
                    for (int n = 0; n < habitat.Population.Count; n++)
                    {
                        Population population = habitat.Population[n];
                        double num17 = (double)Math.Min(5, Math.Max(1, num4)) / 5.0 * num3 * (population.Race.ReproductiveRate - 1.0) * num13 * (double)habitat.Quality;
                        float num18 = CalculateColonyGrowthRateMultiplier(population.Race, habitat);
                        num17 *= (double)num18;
                        num17 *= habitat.GrowthFactor;
                        if (GovernmentAttributes != null)
                        {
                            num17 *= GovernmentAttributes.PopulationGrowth;
                        }
                        num17 *= _EconomyEfficiency;
                        num17 *= PopulationGrowthRate;
                        num17 = Math.Min(1.0, Math.Max(num17, 0.01));
                        if (habitat.Population.TotalAmount < 500000000)
                        {
                            num17 += num17 * 0.5;
                        }
                        if (SpecialBonusPopulationGrowth > 0.0)
                        {
                            num17 *= 1.0 + SpecialBonusPopulationGrowth;
                        }
                        double bonusTotalByEffectType = habitat.ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.PopulationGrowthRate);
                        double num19 = 1.0 + bonusTotalByEffectType / 100.0;
                        num17 *= num19;
                        if (habitat.Population != null && dominantRace.ColonyPopulationPolicyGrowthFactorExterminate != 1.0)
                        {
                            bool flag2 = false;
                            for (int num20 = 0; num20 < habitat.Population.Count; num20++)
                            {
                                Population population2 = habitat.Population[num20];
                                if (population2 != null && population2.Race != null)
                                {
                                    if (habitat.ColonyPopulationPolicyRaceFamily == ColonyPopulationPolicy.Exterminate && population2.Race != dominantRace && population2.Race.FamilyId == dominantRace.FamilyId)
                                    {
                                        flag2 = true;
                                        break;
                                    }
                                    if (habitat.ColonyPopulationPolicy == ColonyPopulationPolicy.Exterminate && population2.Race != dominantRace && population2.Race.FamilyId != dominantRace.FamilyId)
                                    {
                                        flag2 = true;
                                        break;
                                    }
                                }
                            }
                            if (flag2)
                            {
                                num17 *= dominantRace.ColonyPopulationPolicyGrowthFactorExterminate;
                            }
                        }
                        num17 *= num14;
                        num17 *= num8;
                        ColonyPopulationPolicy colonyPopulationPolicy = ColonyPopulationPolicy.Assimilate;
                        if (DominantRace != null && population.Race != DominantRace)
                        {
                            colonyPopulationPolicy = habitat.ColonyPopulationPolicy;
                            if (population.Race.FamilyId == DominantRace.FamilyId)
                            {
                                colonyPopulationPolicy = habitat.ColonyPopulationPolicyRaceFamily;
                            }
                        }
                        switch (colonyPopulationPolicy)
                        {
                            case ColonyPopulationPolicy.Resettle:
                            case ColonyPopulationPolicy.Enslave:
                            case ColonyPopulationPolicy.Exterminate:
                                num17 = 0.0;
                                break;
                        }
                        population.GrowthRate = 1f + (float)num17;
                        num += population.Amount;
                    }
                }
                else
                {
                    foreach (Population item in habitat.Population)
                    {
                        item.GrowthRate = 1f;
                        num += item.Amount;
                    }
                }
                habitat.Population.RecalculateTotalAmount();
                OrderList orders = _Galaxy.Orders.GetOrders(habitat);
                if (builtObject != null)
                {
                    OrderList orders2 = _Galaxy.Orders.GetOrders(builtObject);
                    if (orders2 != null)
                    {
                        orders.AddRange(orders2);
                    }
                }
                int num21 = 10;
                if (habitat.Population.TotalAmount < 200000000)
                {
                    num21 = 5;
                }
                int num22 = 0;
                num22 += CheckResourcesMeetingMinimumLevel(ResourceGroup.Luxury, num6, habitat, orders);
                int num23 = num21 - num22;
                Resource resource = null;
                if (_ControlColonyDevelopment)
                {
                    if (resourceList.Count != 0)
                    {
                        for (int num24 = 0; num24 < num23; num24++)
                        {
                            resource = resourceList[num24];
                            int iterationCount = 0;
                            bool flag3 = false;
                            do
                            {
                                flag3 = false;
                                resource = resourceList[num24];
                                foreach (HabitatResource resource5 in habitat.Resources)
                                {
                                    if (resource5.ResourceID == resource.ResourceID)
                                    {
                                        flag3 = true;
                                        num24++;
                                    }
                                }
                            }
                            while (Galaxy.ConditionCheckLimit(flag3, 100, ref iterationCount));
                            int amountToOrder = 0;
                            int maximumResourceLevel = (int)((double)num6 * 1.5);
                            CheckResourceMeetsMinimumLevel(resource, num6, maximumResourceLevel, habitat, orders, out amountToOrder);
                            if (amountToOrder <= 0)
                            {
                                continue;
                            }
                            double num25 = (double)amountToOrder * _Galaxy.ResourceCurrentPrices[resource.ResourceID];
                            if (num25 < GetPrivateFunds())
                            {
                                if (builtObject != null && builtObject.IsSpacePort)
                                {
                                    CreateOrder(builtObject, resource, amountToOrder, isState: false, OrderType.Standard, allowExpiry: true);
                                }
                                else
                                {
                                    CreateOrder(habitat, resource, amountToOrder, isState: false, OrderType.Standard, allowExpiry: true);
                                }
                            }
                        }
                    }
                    if (resourceList2.Count > 0 && habitat.DevelopmentLevel >= 80)
                    {
                        int num26 = 0;
                        int maximumResourceLevel2 = num7 * 2;
                        Resource resource2 = null;
                        int num27 = 0;
                        if (resourceList3 != null && resourceList3.Count > 0)
                        {
                            for (int num28 = 0; num28 < _Galaxy.ResourceSystem.SuperLuxuryResources.Count; num28++)
                            {
                                Resource resource3 = new Resource(_Galaxy.ResourceSystem.SuperLuxuryResources[num28].ResourceID);
                                if (resourceList3.Contains(resource3))
                                {
                                    int amountToOrder2 = 0;
                                    CheckResourceMeetsMinimumLevel(resource3, num7, maximumResourceLevel2, habitat, orders, out amountToOrder2);
                                    if (amountToOrder2 > num26)
                                    {
                                        num26 = amountToOrder2;
                                        resource2 = resource3;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int num29 = 0; num29 < _Galaxy.ResourceSystem.SuperLuxuryResources.Count; num29++)
                            {
                                Resource resource4 = new Resource(_Galaxy.ResourceSystem.SuperLuxuryResources[num29].ResourceID);
                                if (resourceList2.Contains(resource4))
                                {
                                    int amountToOrder3 = 0;
                                    CheckResourceMeetsMinimumLevel(resource4, num7, maximumResourceLevel2, habitat, orders, out amountToOrder3);
                                    if (amountToOrder3 > num26)
                                    {
                                        num26 = amountToOrder3;
                                        resource2 = resource4;
                                    }
                                }
                            }
                        }
                        num27 = Math.Max(val, num26);
                        if (resource2 != null && num27 > 0)
                        {
                            double num30 = (double)num27 * _Galaxy.ResourceCurrentPrices[resource2.ResourceID];
                            if (num30 < GetPrivateFunds())
                            {
                                if (builtObject != null && builtObject.IsSpacePort)
                                {
                                    CreateOrder(builtObject, resource2, num27, isState: false, OrderType.Standard, allowExpiry: true);
                                }
                                else
                                {
                                    CreateOrder(habitat, resource2, num27, isState: false, OrderType.Standard, allowExpiry: true);
                                }
                            }
                        }
                    }
                }
                habitat.RecalculateAnnualTaxRevenue();
                ProcessColonyTroops(habitat, strongestEmpireTroop, troopStrengthNeutralizationAmount, troopSizeRegenerationAmount, troopRecruitmentAmount, totalIncome, annualStateMaintenance, tribute, annualFacilityMaintenance, annualPirateProtection, minimumIntelligenceAgentSpending, minimumShipSpending, atWar, defendColonies, performRecruitment: true);
            }
            _TotalPopulation = num;
        }

        public float CalculateColonyGrowthRateMultiplier(Race race, Habitat colony)
        {
            float result = 1f;
            switch (colony.Type)
            {
                case HabitatType.Continental:
                    result = ColonyGrowthRateContinental;
                    break;
                case HabitatType.MarshySwamp:
                    result = ColonyGrowthRateMarshySwamp;
                    break;
                case HabitatType.Desert:
                    result = ColonyGrowthRateDesert;
                    break;
                case HabitatType.Ocean:
                    result = ColonyGrowthRateOcean;
                    break;
                case HabitatType.Ice:
                    result = ColonyGrowthRateIce;
                    break;
                case HabitatType.Volcanic:
                    result = ColonyGrowthRateVolcanic;
                    break;
            }
            if (race.NativeHabitatType == colony.Type)
            {
                result = 1f;
            }
            return result;
        }

        public HabitatList IdentifyEmpireCapitals()
        {
            HabitatList habitatList = new HabitatList();
            if (Capital != null && !Capital.HasBeenDestroyed)
            {
                habitatList.Add(Capital);
            }
            HabitatList habitatList2 = IdentifyEmpireRegionalCapitals();
            if (habitatList2 != null && habitatList2.Count > 0)
            {
                habitatList.AddRange(habitatList2);
            }
            return habitatList;
        }

        public HabitatList IdentifyEmpireRegionalCapitals()
        {
            return IdentifyEmpireRegionalCapitals(includeUnderConstruction: false);
        }

        public HabitatList IdentifyEmpireRegionalCapitals(bool includeUnderConstruction)
        {
            HabitatList habitatList = new HabitatList();
            if (Colonies != null)
            {
                for (int i = 0; i < Colonies.Count; i++)
                {
                    if (Colonies[i] == Capital || Colonies[i].HasBeenDestroyed || Colonies[i].Facilities == null || Colonies[i].Facilities.Count <= 0)
                    {
                        continue;
                    }
                    for (int j = 0; j < Colonies[i].Facilities.Count; j++)
                    {
                        if (Colonies[i].Facilities[j].Type == PlanetaryFacilityType.RegionalCapital && (includeUnderConstruction || Colonies[i].Facilities[j].ConstructionProgress >= 1f))
                        {
                            habitatList.Add(Colonies[i]);
                            break;
                        }
                    }
                }
            }
            return habitatList;
        }

        public void RecalculateColonyDistancesFromCapital()
        {
            HabitatList empireCapitals = IdentifyEmpireCapitals();
            if (Colonies != null && Colonies.Count > 0)
            {
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Colonies[i].RecalculateDistanceFactor(empireCapitals);
                }
            }
        }

        public double ApplyCorruptionToIncome(double incomeAmount)
        {
            if (PirateEmpireBaseHabitat != null)
            {
                double num = incomeAmount * Corruption;
                return incomeAmount - num;
            }
            return incomeAmount;
        }

        public void PirateRecalculateEmpireCorruption()
        {
            BaconEmpire.PirateRecalculateEmpireCorruption(this);
        }

        public void RecalculateEmpireCorruption()
        {
            double num = 0.0;
            long totalPopulation = TotalPopulation;
            long num2 = 10000000000L;
            if (PirateEmpireBaseHabitat != null)
            {
                num2 = 0L;
            }
            if (totalPopulation > num2)
            {
                long num3 = 300000000000L;
                double num4 = 0.5;
                if (PirateEmpireBaseHabitat != null)
                {
                    num4 = 0.65;
                }
                long num5 = totalPopulation - num2;
                long num6 = num3 - num2;
                double val = (double)num5 / (double)num6;
                val = Math.Max(0.0, Math.Min(1.0, val));
                double val2 = num4 * val;
                val2 = Math.Min(num4, Math.Max(0.0, val2));
                double num7 = (double)num2 * 1.0 + (double)num5 * (1.0 - val2);
                num = num7 / (double)totalPopulation;
                num = 1.0 - num;
                num *= ColonyCorruptionFactor;
                num = Math.Min(1.0, Math.Max(0.0, num));
            }
            if (_EconomyEfficiency < 1.0)
            {
                num += 1.0 - _EconomyEfficiency;
                num = Math.Min(1.0, Math.Max(0.0, num));
            }
            Corruption = num;
        }

        public void RecalculateEmpirePopulation()
        {
            long num = 0L;
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat.Population != null)
                {
                    habitat.Population.RecalculateTotalAmount();
                }
                num += habitat.Population.TotalAmount;
            }
            _TotalPopulation = num;
        }

        public void ProcessColonyTroops(Habitat colony, Troop strongestEmpireTroop, double troopStrengthNeutralizationAmount, double troopSizeRegenerationAmount, double troopRecruitmentAmount)
        {
            double totalIncome = CalculateAccurateAnnualIncome();
            ProcessColonyTroops(colony, strongestEmpireTroop, troopStrengthNeutralizationAmount, troopSizeRegenerationAmount, troopRecruitmentAmount, totalIncome, AnnualStateMaintenance, AnnualSubjugationTribute + AnnualPirateProtection, AnnualFacilityMaintenance, AnnualPirateProtection, MinimumIntelligenceAgentSpending, MinimumShipSpending, atWar: false, new StellarObjectList(), performRecruitment: true);
        }

        public void ProcessColonyTroops(Habitat colony, Troop strongestEmpireTroop, double troopStrengthNeutralizationAmount, double troopSizeRegenerationAmount, double troopRecruitmentAmount, double totalIncome, double shipMaintenance, double tribute, double facilityMaintenance, double pirateProtection, double minAgentSpending, double minShipSpending, bool atWar, StellarObjectList defendColonies, bool performRecruitment)
        {
            troopRecruitmentAmount *= BaconEmpire.MultiplyTroopRecruitment(this);
            if (colony == null || colony.Troops == null || colony.TroopsToRecruit == null || colony.Population == null)
            {
                return;
            }
            Race race = null;
            if (colony.Population != null)
            {
                race = colony.Population.DominantRace;
            }
            if (race == null)
            {
                race = DominantRace;
            }
            int num = 100;
            if (race != null)
            {
                num = race.TroopStrength;
            }
            if (colony.Ruin != null)
            {
                num = (int)((double)num * (1.0 + colony.Ruin.BonusDefensive));
            }
            double num2 = 1.0 + Math.Max(-0.9, colony.EmpireApprovalRating / 50.0);
            double d = 1.0;
            if (colony.Population != null)
            {
                d = (double)Math.Min(colony.Population.TotalAmount, 2000000000L) / 1000000.0;
            }
            d = Math.Sqrt(d);
            d /= 20.0;
            num2 *= d;
            if (colony.Ruin != null)
            {
                num2 *= 1.0 + colony.Ruin.BonusDefensive;
            }
            troopRecruitmentAmount *= num2;
            troopSizeRegenerationAmount *= num2;
            if (race != null)
            {
                troopSizeRegenerationAmount *= race.TroopRegenerationFactor;
            }
            if (colony.RaceEventType == RaceEventType.WarriorWaveTroopRecruitment)
            {
                troopSizeRegenerationAmount *= 1.2;
            }
            if (GovernmentAttributes != null)
            {
                troopRecruitmentAmount *= GovernmentAttributes.TroopRecruitment;
                troopSizeRegenerationAmount *= GovernmentAttributes.TroopRecruitment;
            }
            if (Leader != null)
            {
                troopRecruitmentAmount *= 1.0 + (double)Leader.TroopRecruitmentRate / 100.0;
                troopSizeRegenerationAmount *= 1.0 + (double)Leader.TroopRecoveryRate / 100.0;
            }
            if (colony.Characters != null && colony.Characters.Count > 0)
            {
                int highestSkillLevelExcludeLeaders = colony.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.TroopRecruitment);
                int highestSkillLevelExcludeLeaders2 = colony.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.TroopRecoveryRate);
                troopRecruitmentAmount *= 1.0 + (double)highestSkillLevelExcludeLeaders / 100.0;
                troopSizeRegenerationAmount *= 1.0 + (double)highestSkillLevelExcludeLeaders2 / 100.0;
            }
            if (colony.InvadingTroops != null && colony.InvadingTroops.Count > 0)
            {
                troopSizeRegenerationAmount *= 0.5;
            }
            int num3 = 0;
            int num4 = 0;
            if (colony.TroopsToRecruit.Count > 0)
            {
                int iterationCount = 0;
                while (Galaxy.ConditionCheckLimit(troopRecruitmentAmount > 0.0 && colony.TroopsToRecruit.Count > 0, 100, ref iterationCount))
                {
                    Troop troop = colony.TroopsToRecruit[0];
                    if (troop == null)
                    {
                        continue;
                    }
                    float num5 = (float)troopRecruitmentAmount;
                    if (troop.Type == TroopType.Infantry && troop.MaintenanceMultiplier == 0.25f && troop.PictureRef == _Galaxy.Races.Count)
                    {
                        num5 *= 2f;
                    }
                    troop.Readiness += num5;
                    troopRecruitmentAmount = 0.0;
                    if (troop.Readiness >= 100f)
                    {
                        troopRecruitmentAmount = troop.Readiness - 100f;
                        troop.Readiness = 100f;
                        troop.Colony = colony;
                        colony.ResolveInvasionEmpires(out var _, out var invader);
                        if (invader == this && colony.InvadingTroops != null)
                        {
                            colony.InvadingTroops.Add(troop);
                        }
                        else
                        {
                            colony.Troops.Add(troop);
                        }
                        if (_Troops != null && !_Troops.Contains(troop))
                        {
                            _Troops.Add(troop);
                        }
                        colony.TroopsToRecruit.Remove(troop);
                        _Galaxy.DoCharacterEvent(CharacterEventType.TroopComplete, troop, colony.Characters, includeLeader: true, colony.Empire);
                        _Galaxy.ChanceNewTroopGeneralFromRecruitment(troop, this, colony);
                    }
                }
            }
            for (int i = 0; i < colony.Troops.Count; i++)
            {
                Troop troop2 = colony.Troops[i];
                if (troop2 != null)
                {
                    troop2.Readiness += (float)troopSizeRegenerationAmount;
                    if (troop2.Readiness > 100f)
                    {
                        troop2.Readiness = 100f;
                    }
                    if (troop2.AttackStrength > 900)
                    {
                        troop2.SetAttackStrength(900);
                    }
                    num3 += (int)((double)troop2.DefendStrength * ((double)troop2.Readiness / 100.0));
                    num4 += troop2.DefendStrength;
                }
            }
            if (!performRecruitment)
            {
                return;
            }
            for (int j = 0; j < colony.TroopsToRecruit.Count; j++)
            {
                Troop troop3 = colony.TroopsToRecruit[j];
                if (troop3 != null)
                {
                    num3 += troop3.DefendStrength;
                    num4 += troop3.DefendStrength;
                }
            }
            int num6 = colony.TroopLevelRequired;
            if (colony.CheckTroopFacilitiesPresent())
            {
                num6 = (int)((double)num6 * 1.25);
                num6 = Math.Max(num6, 400);
            }
            else if (colony.DefensiveFortressBonus > 0)
            {
                num6 = (int)((double)num6 * 1.25);
                num6 = Math.Max(num6, 300);
            }
            if (defendColonies != null && defendColonies.Contains(colony))
            {
                num6 = (int)((double)num6 * 1.5);
                num6 = Math.Max(num6, 300);
            }
            if (Policy != null)
            {
                if (Policy.ColonyPopulationThresholdTroopRecruitment > 0 && colony.Population != null && colony.Population.TotalAmount < (long)Policy.ColonyPopulationThresholdTroopRecruitment * 1000000L)
                {
                    num6 = 0;
                }
                num6 = Math.Max(num6, Policy.TroopGarrisonMinimumPerColony * 100);
            }
            if (num6 > 0 && num6 < 200 && colony.Empire != null)
            {
                bool flag = true;
                SystemInfo systemInfo = null;
                if (_Galaxy.Systems.Count > colony.SystemIndex)
                {
                    systemInfo = _Galaxy.Systems[colony.SystemIndex];
                }
                if (atWar)
                {
                    flag = false;
                }
                else if ((systemInfo != null && systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire != colony.Empire) || (systemInfo != null && systemInfo.OtherEmpires != null && systemInfo.OtherEmpires.Count > 0) || (systemInfo != null && systemInfo.IndependentColonyCount > 0))
                {
                    flag = false;
                }
                else if (colony.AnnualTaxRevenue > Galaxy.TroopAnnualMaintenance)
                {
                    flag = false;
                }
                else if (colony.BasesAtHabitat != null && colony.BasesAtHabitat.Count > 0)
                {
                    for (int k = 0; k < colony.BasesAtHabitat.Count; k++)
                    {
                        BuiltObject builtObject = colony.BasesAtHabitat[k];
                        if (builtObject != null && (builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort))
                        {
                            flag = false;
                        }
                    }
                }
                if (flag && Policy != null && Policy.TroopGarrisonMinimumPerColony <= 0)
                {
                    num6 = 0;
                }
            }
            if (!_ControlTroopGeneration)
            {
                return;
            }
            if (num3 < num6)
            {
                double num7 = AnnualTroopMaintenanceIncludeRecruiting + Galaxy.TroopAnnualMaintenance;
                num7 += shipMaintenance + tribute + facilityMaintenance + pirateProtection;
                double num8 = StateMoney / num7;
                double num9 = Math.Max(shipMaintenance, minShipSpending);
                double num10 = totalIncome - (num9 + tribute + facilityMaintenance + pirateProtection);
                double num11 = num10 - AnnualTroopMaintenanceIncludeRecruiting;
                bool flag2 = false;
                if (totalIncome > num7)
                {
                    flag2 = true;
                }
                else if (num8 >= Galaxy.AllowableYearsMaintenanceFromCashOnHand)
                {
                    flag2 = true;
                }
                else if (num8 < Galaxy.AllowableYearsMaintenanceFromCashOnHand && num11 >= Galaxy.TroopAnnualMaintenance)
                {
                    flag2 = true;
                }
                if (num11 < Galaxy.TroopAnnualMaintenance)
                {
                    int num12 = colony.Troops.TotalDefendStrengthExcludeReadiness / 100;
                    num12 += colony.TroopsToRecruit.TotalDefendStrengthExcludeReadiness / 100;
                    if (num12 >= colony.TroopLevelMinimum)
                    {
                        flag2 = false;
                    }
                }
                if (flag2 && TroopCanRecruitInfantry)
                {
                    Troop item = colony.GenerateNewTroop(strongestEmpireTroop, recruitDefaultTroops: false);
                    colony.TroopsToRecruit.Add(item);
                    if (Troops != null && !Troops.Contains(item))
                    {
                        Troops.Add(item);
                    }
                }
            }
            if (TroopCanRecruitArtillery)
            {
                int num13 = colony.Troops.CountByType(TroopType.Artillery);
                num13 += colony.TroopsToRecruit.CountByType(TroopType.Artillery);
                int num14 = (int)((double)num6 / 800.0 * Policy.TroopRecruitArtilleryLevel);
                if (num13 < num14)
                {
                    double num15 = CalculateCostPerTroop(TroopType.Artillery, colony, null);
                    double num16 = AnnualTroopMaintenanceIncludeRecruiting + num15;
                    num16 += shipMaintenance + tribute + facilityMaintenance;
                    double num17 = StateMoney / num16;
                    bool flag3 = false;
                    if (totalIncome > num16)
                    {
                        flag3 = true;
                    }
                    else if (num17 >= Galaxy.AllowableYearsMaintenanceFromCashOnHand)
                    {
                        flag3 = true;
                    }
                    else if (num17 < Galaxy.AllowableYearsMaintenanceFromCashOnHand)
                    {
                        double num18 = Math.Max(shipMaintenance, minShipSpending);
                        double num19 = totalIncome - (num18 + tribute + facilityMaintenance);
                        double num20 = num19 - AnnualTroopMaintenanceIncludeRecruiting;
                        if (num20 >= num15)
                        {
                            flag3 = true;
                        }
                    }
                    if (flag3)
                    {
                        Troop item2 = colony.GenerateNewTroop(TroopType.Artillery, strongestEmpireTroop, recruitDefaultTroops: false);
                        colony.TroopsToRecruit.Add(item2);
                        if (Troops != null && !Troops.Contains(item2))
                        {
                            Troops.Add(item2);
                        }
                    }
                }
            }
            ReviewColonyTroopGarrison(colony, num6, num4);
        }

        public void ReviewColonyTroopGarrison(Habitat colony)
        {
            int num = colony.TroopLevelRequired;
            int num2 = 0;
            num2 += colony.Troops.TotalDefendStrengthExcludeReadiness;
            num2 += colony.TroopsToRecruit.TotalDefendStrengthExcludeReadiness;
            if (colony.CheckTroopFacilitiesPresent())
            {
                num = (int)((double)num * 1.25);
                num = Math.Max(num, 400);
            }
            else if (colony.DefensiveFortressBonus > 0)
            {
                num = (int)((double)num * 1.25);
                num = Math.Max(num, 300);
            }
            if (Policy != null)
            {
                if (Policy.ColonyPopulationThresholdTroopRecruitment > 0 && colony.Population != null && colony.Population.TotalAmount < (long)Policy.ColonyPopulationThresholdTroopRecruitment * 1000000L)
                {
                    num = 0;
                }
                num = Math.Max(num, Policy.TroopGarrisonMinimumPerColony * 100);
            }
            if (num > 0 && num < 200 && colony.Empire != null)
            {
                bool flag = true;
                SystemInfo systemInfo = null;
                if (_Galaxy.Systems.Count > colony.SystemIndex)
                {
                    systemInfo = _Galaxy.Systems[colony.SystemIndex];
                }
                if (CheckAtWar())
                {
                    flag = false;
                }
                else if ((systemInfo != null && systemInfo.DominantEmpire != null && systemInfo.DominantEmpire.Empire != colony.Empire) || (systemInfo != null && systemInfo.OtherEmpires != null && systemInfo.OtherEmpires.Count > 0) || (systemInfo != null && systemInfo.IndependentColonyCount > 0))
                {
                    flag = false;
                }
                else if (colony.AnnualTaxRevenue > Galaxy.TroopAnnualMaintenance)
                {
                    flag = false;
                }
                else if (colony.BasesAtHabitat != null && colony.BasesAtHabitat.Count > 0)
                {
                    for (int i = 0; i < colony.BasesAtHabitat.Count; i++)
                    {
                        BuiltObject builtObject = colony.BasesAtHabitat[i];
                        if (builtObject != null && (builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort))
                        {
                            flag = false;
                        }
                    }
                }
                if (flag && Policy != null && Policy.TroopGarrisonMinimumPerColony <= 0)
                {
                    num = 0;
                }
            }
            ReviewColonyTroopGarrison(colony, num, num2);
        }

        public void ReviewColonyTroopGarrison(Habitat colony, int requiredTroopLevel, int currentTroopLevelCompleteTroops)
        {
            if (colony == null)
            {
                return;
            }
            int val = (int)((double)Math.Min(requiredTroopLevel, currentTroopLevelCompleteTroops) * 0.5 * 100.0);
            val = Math.Max(val, Policy.TroopGarrisonMinimumPerColony * 10000);
            TroopList byType = colony.Troops.GetByType(TroopType.Infantry);
            TroopList byType2 = colony.Troops.GetByType(TroopType.Artillery);
            int totalDefendStrengthExcludeReadiness = byType.TotalDefendStrengthExcludeReadiness;
            int totalDefendStrengthExcludeReadiness2 = byType2.TotalDefendStrengthExcludeReadiness;
            int num = 0;
            TroopList troopList = new TroopList();
            if (totalDefendStrengthExcludeReadiness2 > 0 && byType2.Count > 0)
            {
                int num2 = 0;
                if (totalDefendStrengthExcludeReadiness > 0 && byType.Count > 0)
                {
                    num2 = ((totalDefendStrengthExcludeReadiness < val / 2) ? totalDefendStrengthExcludeReadiness : (val / 2));
                }
                for (int i = 0; i < byType2.Count; i++)
                {
                    Troop troop = byType2[i];
                    if (troop != null)
                    {
                        num += (int)troop.OverallDefendStrengthExcludeReadiness;
                        troopList.Add(troop);
                        if (num >= val - num2)
                        {
                            break;
                        }
                    }
                }
            }
            for (int j = 0; j < byType.Count; j++)
            {
                Troop troop2 = byType[j];
                if (troop2 != null)
                {
                    num += (int)troop2.OverallDefendStrengthExcludeReadiness;
                    troopList.Add(troop2);
                    if (num >= val)
                    {
                        break;
                    }
                }
            }
            if (num < val)
            {
                TroopList byType3 = colony.Troops.GetByType(TroopType.SpecialForces);
                if (byType3.Count > 0)
                {
                    for (int k = 0; k < byType3.Count; k++)
                    {
                        Troop troop3 = byType3[k];
                        if (troop3 != null)
                        {
                            num += (int)troop3.OverallDefendStrengthExcludeReadiness;
                            troopList.Add(troop3);
                            if (num >= val)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            for (int l = 0; l < colony.Troops.Count; l++)
            {
                Troop troop4 = colony.Troops[l];
                if (troop4 != null)
                {
                    if (troopList.Contains(troop4))
                    {
                        troop4.Garrisoned = true;
                    }
                    else
                    {
                        troop4.Garrisoned = false;
                    }
                }
            }
        }

        private void RecruitAttackTroops()
        {
            if (!_ControlTroopGeneration || Colonies == null || Troops == null)
            {
                return;
            }
            int num = Troops.CountByType(TroopType.Armored);
            int num2 = Troops.CountByType(TroopType.SpecialForces);
            int val = (int)((double)Colonies.Count * 1.0 * Policy.TroopRecruitArmorLevel);
            int num3 = Math.Max(val, (int)((double)TotalColonyStrategicValue / 150000.0 * Policy.TroopRecruitArmorLevel));
            int val2 = (int)((double)Colonies.Count * 0.3 * Policy.TroopRecruitSpecialForcesLevel);
            int num4 = Math.Max(val2, (int)((double)TotalColonyStrategicValue / 600000.0 * Policy.TroopRecruitSpecialForcesLevel));
            double num5 = CalculateCostPerTroop(TroopType.Armored, null, null);
            CalculateCostPerTroop(TroopType.Artillery, null, null);
            CalculateCostPerTroop(TroopType.SpecialForces, null, null);
            int num6 = Math.Max(0, num3 - num);
            int num7 = Math.Max(0, num4 - num2);
            if (!TroopCanRecruitArmored)
            {
                num6 = 0;
            }
            if (!TroopCanRecruitSpecialForces)
            {
                num7 = 0;
            }
            double num8 = (double)num6 * num5;
            double num9 = (double)num7 * num5;
            double num10 = num8 + num9;
            double annualEmpireExpenses = 0.0;
            double num11 = CalculateAccurateAnnualCashflowIncludingUnderConstruction(out annualEmpireExpenses);
            double num12 = StateMoney / annualEmpireExpenses;
            double num13 = 1.0;
            bool flag = false;
            double num14 = num11 - num10;
            if (num14 > 0.0)
            {
                flag = true;
            }
            else if (num12 >= Galaxy.AllowableYearsMaintenanceFromCashOnHand)
            {
                flag = true;
            }
            else if (num11 > 0.0)
            {
                num13 = num11 / num10;
                flag = true;
            }
            if (!flag)
            {
                return;
            }
            if (num6 > 0 && TroopCanRecruitArmored)
            {
                HabitatList habitatsWithCompletedFacilities = Colonies.GetHabitatsWithCompletedFacilities(PlanetaryFacilityType.ArmoredFactory);
                for (int i = 0; i < habitatsWithCompletedFacilities.Count; i++)
                {
                    Habitat habitat = habitatsWithCompletedFacilities[i];
                    if (habitat != null && !habitat.HasBeenDestroyed && habitat.Empire == this && habitat.TroopsToRecruit != null && habitat.Population != null && habitat.Population.DominantRace != null)
                    {
                        Race dominantRace = habitat.Population.DominantRace;
                        int troopStrength = dominantRace.TroopStrength;
                        Troop troop = Galaxy.GenerateNewTroop(GenerateTroopDescription(dominantRace.TroopNameArmored), TroopType.Armored, troopStrength, this, dominantRace);
                        if (troop != null)
                        {
                            troop.Colony = habitat;
                            troop.Readiness = 0f;
                            habitat.TroopsToRecruit.Add(troop);
                            Troops.Add(troop);
                        }
                        num6--;
                        if (num6 <= 0)
                        {
                            break;
                        }
                    }
                }
            }
            if (num7 <= 0 || !TroopCanRecruitSpecialForces)
            {
                return;
            }
            HabitatList habitatsWithCompletedFacilities2 = Colonies.GetHabitatsWithCompletedFacilities(PlanetaryFacilityType.MilitaryAcademy);
            for (int j = 0; j < habitatsWithCompletedFacilities2.Count; j++)
            {
                Habitat habitat2 = habitatsWithCompletedFacilities2[j];
                if (habitat2 != null && !habitat2.HasBeenDestroyed && habitat2.Empire == this && habitat2.TroopsToRecruit != null && habitat2.Population != null && habitat2.Population.DominantRace != null)
                {
                    Race dominantRace2 = habitat2.Population.DominantRace;
                    int troopStrength2 = dominantRace2.TroopStrength;
                    Troop troop2 = Galaxy.GenerateNewTroop(GenerateTroopDescription(dominantRace2.TroopNameSpecialForces), TroopType.SpecialForces, troopStrength2, this, dominantRace2);
                    if (troop2 != null)
                    {
                        troop2.Colony = habitat2;
                        troop2.Readiness = 0f;
                        habitat2.TroopsToRecruit.Add(troop2);
                        Troops.Add(troop2);
                    }
                    num7--;
                    if (num7 <= 0)
                    {
                        break;
                    }
                }
            }
        }

        public Troop IdentifyStrongestRaceAttackTroop()
        {
            return IdentifyStrongestRaceAttackTroop(TroopType.Infantry);
        }

        public Troop IdentifyStrongestRaceAttackTroop(TroopType troopType)
        {
            Troop troop = null;
            if (Troops != null)
            {
                for (int i = 0; i < Troops.Count; i++)
                {
                    Troop troop2 = Troops[i];
                    if (troop2 != null && (troop == null || troop2.AttackStrength > troop.AttackStrength) && troop2.Race != null && troop2.Type == troopType)
                    {
                        troop = troop2;
                    }
                }
            }
            return troop;
        }

        public string GenerateTroopDescription()
        {
            return GenerateTroopDescription(_TroopDescription);
        }

        public string GenerateTroopDescription(string troopLabel)
        {
            _TroopCount++;
            string text = Galaxy.OrderedNumberDescription(_TroopCount);
            return text + " " + troopLabel;
        }

        public Order CreateOrder(Habitat colony, Resource resource, int amount, bool isState, OrderType type)
        {
            return CreateOrder(colony, resource, amount, isState, type, allowExpiry: false);
        }

        public Order CreateOrder(Habitat colony, Resource resource, int amount, bool isState, OrderType type, bool allowExpiry)
        {
            long expiryDate = _Galaxy.CurrentStarDate + (long)(Galaxy.OrderExpiryYearsLuxury * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
            if (!allowExpiry || !resource.IsLuxuryResource)
            {
                expiryDate = _Galaxy.CurrentStarDate + (long)(1000.0 * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
            }
            return CreateOrder(colony, resource, amount, isState, type, expiryDate);
        }

        public Order CreateOrder(Habitat colony, Resource resource, int amount, bool isState, OrderType type, long expiryDate)
        {
            int maximumFulfillmentDistance = _Galaxy.CalculateMaximumOrderFulfillmentDistance(colony);
            Order order = new Order(_Galaxy, colony, resource, amount, expiryDate, maximumFulfillmentDistance);
            order.Type = type;
            order.MinimumContractSize = Galaxy.MinimumContractSize;
            order.IsStateOrder = isState;
            _Galaxy.Orders.Add(order);
            return order;
        }

        public Order CreateOrder(BuiltObject builtObject, Resource resource, int amount, bool isState, OrderType type)
        {
            return CreateOrder(builtObject, resource, amount, isState, type, allowExpiry: false);
        }

        public Order CreateOrder(BuiltObject builtObject, Resource resource, int amount, bool isState, OrderType type, bool allowExpiry)
        {
            long expiryDate = _Galaxy.CurrentStarDate + (long)(Galaxy.OrderExpiryYearsLuxury * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
            if (!allowExpiry || !resource.IsLuxuryResource)
            {
                expiryDate = _Galaxy.CurrentStarDate + (long)(1000.0 * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
            }
            return CreateOrder(builtObject, resource, amount, isState, type, expiryDate);
        }

        public Order CreateOrder(BuiltObject builtObject, Resource resource, int amount, bool isState, OrderType type, long expiryDate)
        {
            int maximumFulfillmentDistance = _Galaxy.CalculateMaximumOrderFulfillmentDistance(builtObject);
            _ = _Galaxy.ResourceCurrentPrices[resource.ResourceID];
            Order order = new Order(_Galaxy, builtObject, resource, amount, expiryDate, maximumFulfillmentDistance);
            order.Type = type;
            order.MinimumContractSize = Galaxy.MinimumContractSize;
            order.IsStateOrder = isState;
            _Galaxy.Orders.Add(order);
            return order;
        }

        private bool CheckResourceMeetsMinimumLevel(Resource resource, int minimumResourceLevel, int maximumResourceLevel, Habitat colony, OrderList colonyOrders, out int amountToOrder)
        {
            bool result = false;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = -1;
            if (colony.Cargo != null && colony.Cargo.GetExists(resource))
            {
                num4 = colony.Cargo.IndexOf(resource, colony.Owner);
            }
            if (num4 >= 0)
            {
                num = colony.Cargo[num4].Available;
                num2 = num;
            }
            int num5;
            for (num5 = colonyOrders.IndexOf(resource.ResourceID, 0); num5 >= 0; num5 = colonyOrders.IndexOf(resource.ResourceID, num5))
            {
                num3 = colonyOrders[num5].AmountRequested;
                num2 += num3;
                num5++;
            }
            amountToOrder = Math.Max(0, maximumResourceLevel - num2);
            if (amountToOrder > 0)
            {
                if (resource.IsRestrictedResource)
                {
                    amountToOrder = Math.Max(amountToOrder, Galaxy.MinimumRestrictedResourceReorderAmount);
                }
                else
                {
                    amountToOrder = Math.Max(amountToOrder, Galaxy.MinimumContractSize);
                }
                amountToOrder = Math.Min(20000, amountToOrder);
            }
            if (num2 >= minimumResourceLevel)
            {
                result = true;
            }
            return result;
        }

        private int CheckResourcesMeetingMinimumLevel(ResourceGroup resourceGroup, int minimumResourceLevel, Habitat colony, OrderList colonyOrders)
        {
            int num = 0;
            int num2 = 0;
            int num3 = -1;
            int num4 = 0;
            ResourceList resourceList = new ResourceList();
            List<int> list = new List<int>();
            int num5 = -1;
            if (colony.Cargo != null)
            {
                num5 = colony.Cargo.IndexOf(resourceGroup, colony.Owner, 0);
            }
            while (num5 >= 0)
            {
                num2 = colony.Cargo[num5].Amount - colony.Cargo[num5].Reserved;
                num3 = resourceList.IndexOf(colony.Cargo[num5].CommodityResource);
                if (num3 < 0)
                {
                    resourceList.Add(colony.Cargo[num5].CommodityResource);
                    list.Add(num2);
                }
                else
                {
                    list[num3] += num2;
                }
                num5++;
                num5 = colony.Cargo.IndexOf(resourceGroup, colony.Owner, num5);
            }
            int num6;
            for (num6 = colonyOrders.IndexOf(resourceGroup, 0); num6 >= 0; num6 = colonyOrders.IndexOf(resourceGroup, num6))
            {
                num4 = colonyOrders[num6].AmountRequested;
                num3 = resourceList.IndexOf(colonyOrders[num6].CommodityResource);
                if (num3 < 0)
                {
                    resourceList.Add(colonyOrders[num6].CommodityResource);
                    list.Add(num4);
                }
                else
                {
                    list[num3] += num4;
                }
                num6++;
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] > minimumResourceLevel)
                {
                    num++;
                }
            }
            return num;
        }

        public int DetermineColonizationValue(Habitat habitat)
        {
            double num = 0.0;
            num = (int)((double)(habitat.Quality * habitat.Quality) * 20000.0);
            double num2 = 1.0;
            if (habitat.Population != null && habitat.Population.Count > 0 && (habitat.Empire == _Galaxy.IndependentEmpire || habitat.Empire == null))
            {
                int val = _Galaxy.CheckColonizationLikeliness(habitat, DominantRace);
                val = Math.Min(105, val);
                double num3 = 1.0 + (double)val / 100.0;
                num3 *= num3;
                num3 *= num3;
                num3 *= 1.0 + Math.Sqrt(habitat.Population.TotalAmount / 10000000);
                num *= num3;
                num2 = 3.0;
            }
            if (habitat.Ruin != null)
            {
                num *= 1.0 + habitat.Ruin.DevelopmentBonus;
                if (habitat.Ruin.BonusDefensive > 0.0 || habitat.Ruin.BonusDiplomacy > 0.0 || habitat.Ruin.BonusHappiness > 0.0 || habitat.Ruin.BonusResearchEnergy > 0.0 || habitat.Ruin.BonusResearchHighTech > 0.0 || habitat.Ruin.BonusResearchWeapons > 0.0 || habitat.Ruin.BonusWealth > 0.0)
                {
                    num *= 30.0;
                }
                num *= Policy.ColonizeRuinsPriority;
            }
            double d = DetermineResourceValue(habitat);
            d = Math.Sqrt(d);
            num *= d;
            double num4 = 1.0;
            switch (habitat.Type)
            {
                case HabitatType.Continental:
                    num4 = Policy.ColonizeContinentalPriority;
                    break;
                case HabitatType.MarshySwamp:
                    num4 = Policy.ColonizeMarshySwampPriority;
                    break;
                case HabitatType.Ocean:
                    num4 = Policy.ColonizeOceanPriority;
                    break;
                case HabitatType.Desert:
                    num4 = Policy.ColonizeDesertPriority;
                    break;
                case HabitatType.Ice:
                    num4 = Policy.ColonizeIcePriority;
                    break;
                case HabitatType.Volcanic:
                    num4 = Policy.ColonizeVolcanicPriority;
                    break;
            }
            num *= num4;
            num /= DetermineProximityFromMajorColony(habitat, 50000) / num2;
            return (int)num;
        }

        public bool DetermineColonizeLowQualityHabitat(Habitat habitat)
        {
            bool result = true;
            if (habitat.Quality < 0.5f)
            {
                result = false;
                if (habitat.Resources != null && habitat.Resources.Count > 0 && habitat.Resources.HasSuperLuxuryResources())
                {
                    result = true;
                }
                if (habitat.Ruin != null && (habitat.Ruin.BonusDefensive > 0.0 || habitat.Ruin.BonusDiplomacy > 0.0 || habitat.Ruin.BonusHappiness > 0.0 || habitat.Ruin.BonusResearchEnergy > 0.0 || habitat.Ruin.BonusResearchHighTech > 0.0 || habitat.Ruin.BonusResearchWeapons > 0.0 || habitat.Ruin.BonusWealth > 0.0))
                {
                    result = true;
                }
            }
            return result;
        }

        public double DetermineResourceValue(Habitat habitat)
        {
            double num = 1.0;
            if (ResourceMap != null && ResourceMap.CheckResourcesKnown(habitat))
            {
                HabitatResourceList habitatResourceList = new HabitatResourceList();
                if (habitat.Resources != null)
                {
                    habitatResourceList = habitat.Resources.Clone();
                }
                for (int i = 0; i < habitatResourceList.Count; i++)
                {
                    HabitatResource habitatResource = habitatResourceList[i];
                    if (habitatResource != null)
                    {
                        double num2 = _Galaxy.ResourceCurrentPrices[habitatResource.ResourceID] / 10.0;
                        double num3 = 100.0;
                        if (Policy != null && habitatResource.IsRestrictedResource)
                        {
                            num2 *= 50.0;
                            num3 = 2000.0;
                            num2 *= Policy.ControlRestrictedResourcesPriority;
                            num3 *= Policy.ControlRestrictedResourcesPriority;
                        }
                        if (num2 > num3)
                        {
                            num2 = num3;
                        }
                        if (num2 < 0.1)
                        {
                            num2 = 0.1;
                        }
                        num += num2;
                    }
                }
            }
            return num;
        }

        private void ReviewRestrictedResourceTrading()
        {
            if (!CheckEmpireSuppliesRestrictedResources() || Reclusive)
            {
                return;
            }
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation.OtherEmpire == this)
                {
                    continue;
                }
                bool flag = DetermineWhetherTradeRestrictedResourcesWithEmpire(diplomaticRelation.OtherEmpire);
                if (flag == diplomaticRelation.SupplyRestrictedResources)
                {
                    continue;
                }
                AdvisorMessageType advisorMessageType = AdvisorMessageType.AllowTradeRestrictedResources;
                if (!flag)
                {
                    advisorMessageType = AdvisorMessageType.DisallowTradeRestrictedResources;
                }
                if (CheckTaskAuthorized(_ControlDiplomacyTreaties, GenerateAutomationMessageTradeRestrictedResources(diplomaticRelation.OtherEmpire, flag), diplomaticRelation.OtherEmpire, advisorMessageType))
                {
                    string empty = string.Empty;
                    EmpireMessageType empireMessageType = EmpireMessageType.RestrictedResourceTradingAllowed;
                    if (flag)
                    {
                        empty = string.Format(TextResolver.GetText("Trade Restricted Resource EMPIRE"), Name);
                        empireMessageType = EmpireMessageType.RestrictedResourceTradingAllowed;
                    }
                    else
                    {
                        empty = string.Format(TextResolver.GetText("Trade Restricted Resource Refuse EMPIRE"), Name);
                        empireMessageType = EmpireMessageType.RestrictedResourceTradingBlocked;
                    }
                    SendMessageToEmpire(diplomaticRelation.OtherEmpire, empireMessageType, this, empty);
                    diplomaticRelation.SupplyRestrictedResources = flag;
                }
            }
        }

        public bool DetermineWhetherTradeRestrictedResourcesWithEmpire(Empire otherEmpire)
        {
            bool result = false;
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(otherEmpire);
            switch (diplomaticRelation.Strategy)
            {
                case DiplomaticStrategy.Befriend:
                case DiplomaticStrategy.Placate:
                case DiplomaticStrategy.Ally:
                case DiplomaticStrategy.DefendPlacate:
                    result = true;
                    break;
            }
            return result;
        }

        public double DetermineProximityFromMajorColony(Habitat habitat, int strategicThreshold)
        {
            if (habitat != null)
            {
                Habitat habitat2 = _Galaxy.FastFindNearestColony(habitat.Xpos, habitat.Ypos, this, strategicThreshold);
                if (habitat2 == null)
                {
                    habitat2 = Capital;
                }
                if (habitat2 != null)
                {
                    double distance = _Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, habitat2.Xpos, habitat2.Ypos);
                    return Galaxy.CalculateDistanceFactor(distance);
                }
            }
            return 1.0;
        }

        public double DetermineProximityFromCapital(Habitat habitat)
        {
            //double num = 0.0;
            if (PirateEmpireBaseHabitat == null)
            {
                return 1.0 + Math.Sqrt(Math.Sqrt(1.0 + _Galaxy.CalculateDistance(Capital.Xpos, Capital.Ypos, habitat.Xpos, habitat.Ypos)) / 100.0);
            }
            return 1.0 + Math.Sqrt(Math.Sqrt(1.0 + _Galaxy.CalculateDistance(PirateEmpireBaseHabitat.Xpos, PirateEmpireBaseHabitat.Ypos, habitat.Xpos, habitat.Ypos)) / 100.0);
        }

        public double DetermineProximityFromCapital(BuiltObject builtObject)
        {
            if (PirateEmpireBaseHabitat == null)
            {
                return Math.Sqrt(Math.Sqrt(1.0 + _Galaxy.CalculateDistance(Capital.Xpos, Capital.Ypos, builtObject.Xpos, builtObject.Ypos)) / 100.0);
            }
            return Math.Sqrt(Math.Sqrt(1.0 + _Galaxy.CalculateDistance(PirateEmpireBaseHabitat.Xpos, PirateEmpireBaseHabitat.Ypos, builtObject.Xpos, builtObject.Ypos)) / 100.0);
        }

        public bool CanEmpireColonizeHabitatRange(Empire empire, Habitat habitat)
        {
            bool result = false;
            if (_Galaxy.ColonizationRangeEnforceLimit)
            {
                Habitat habitat2 = _Galaxy.FastFindNearestColony(habitat.Xpos, habitat.Ypos, empire, 0);
                if (habitat2 != null)
                {
                    double num = _Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, habitat2.Xpos, habitat2.Ypos);
                    if (num <= (double)_Galaxy.ColonizationRange)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }

        public bool CanEmpireColonizeHabitat(Empire empire, Habitat habitat, List<HabitatType> colonizableHabitatTypes, Design latestColonyShip)
        {
            return CanEmpireColonizeHabitat(empire, habitat, colonizableHabitatTypes, latestColonyShip, checkRange: true);
        }

        public bool CanEmpireColonizeHabitat(Empire empire, Habitat habitat, List<HabitatType> colonizableHabitatTypes, Design latestColonyShip, bool checkRange)
        {
            bool result = false;
            if (empire.CheckSystemExplored(habitat.SystemIndex) && (habitat.Owner == null || habitat.Owner == _Galaxy.IndependentEmpire) && _Galaxy.CheckEmpireTerritoryCanColonizeHabitat(empire, habitat) && ((latestColonyShip != null && CanDesignColonizeHabitat(latestColonyShip, habitat)) || colonizableHabitatTypes.Contains(habitat.Type)) && (habitat.Category == HabitatCategoryType.Planet || habitat.Category == HabitatCategoryType.Moon))
            {
                result = !checkRange || CanEmpireColonizeHabitatRange(this, habitat);
            }
            return result;
        }

        private void InvadeUnwillingColonizationTargets(Galaxy galaxy)
        {
            int refusalCount = 0;
            int num = (int)(100.0 + Galaxy.Rnd.NextDouble() * 10.0);
            if (DominantRace.AggressionLevel <= num)
            {
                return;
            }
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            int num2 = Math.Min(10, _ColonizationTargets.Count);
            for (int i = 0; i < num2; i++)
            {
                if (_ColonizationTargets[i].AssignedShip != null)
                {
                    continue;
                }
                Habitat habitat = _ColonizationTargets[i].Habitat;
                if (habitat == null)
                {
                    continue;
                }
                int num3 = galaxy.CheckColonizationLikeliness(habitat, DominantRace);
                if (num3 < -3 && DetermineColonizeLowQualityHabitat(habitat))
                {
                    ShipGroup shipGroup = FindNearestAvailableFleet(habitat.Xpos, habitat.Ypos, BuiltObjectMissionPriority.Low, 0, FleetPosture.Attack, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: false, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: true, 40000);
                    if (shipGroup != null && (shipGroup.LeadShip.IsAutoControlled || _ControlMilitaryAttacks == AutomationLevel.SemiAutomated) && CheckTaskAuthorized(_ControlMilitaryAttacks, ref refusalCount, GenerateAutomationMessageInvadeIndependent(habitat, shipGroup), habitat, AdvisorMessageType.InvadeIndependent, shipGroup, null))
                    {
                        shipGroup.AssignMission(BuiltObjectMissionType.Attack, habitat, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                        habitatPrioritizationList.Add(_ColonizationTargets[i]);
                    }
                }
            }
            foreach (HabitatPrioritization item in habitatPrioritizationList)
            {
                _ColonizationTargets.Remove(item);
            }
        }

        public bool CheckShipCanSurviveStorms(BuiltObject ship)
        {
            if (ship != null && ship.ArmorReactive >= 5)
            {
                return true;
            }
            return false;
        }

        public bool CheckPassengerShipsCanSurviveStorms()
        {
            BuiltObject builtObject = null;
            for (int num = PrivateBuiltObjects.Count - 1; num >= 0; num--)
            {
                builtObject = PrivateBuiltObjects[num];
                if (builtObject.UnbuiltOrDamagedComponentCount == 0 && builtObject.SubRole == BuiltObjectSubRole.PassengerShip)
                {
                    break;
                }
            }
            if (builtObject != null && builtObject.ArmorReactive >= 5)
            {
                return true;
            }
            return false;
        }

        public bool CheckMiningShipsCanSurviveStorms()
        {
            BuiltObject builtObject = null;
            if (ResourceExtractors.Count > 0)
            {
                for (int num = ResourceExtractors.Count - 1; num >= 0; num--)
                {
                    builtObject = ResourceExtractors[num];
                    if (builtObject.UnbuiltOrDamagedComponentCount == 0 && (builtObject.SubRole == BuiltObjectSubRole.MiningShip || builtObject.SubRole == BuiltObjectSubRole.GasMiningShip))
                    {
                        break;
                    }
                }
            }
            if (builtObject != null && builtObject.ArmorReactive >= 5)
            {
                return true;
            }
            return false;
        }

        public bool CheckConstructionShipAndMiningStationCanSurviveStorms()
        {
            BuiltObject builtObject = null;
            if (ConstructionShips.Count > 0)
            {
                builtObject = ConstructionShips[ConstructionShips.Count - 1];
            }
            Design design = Designs.FindNewest(BuiltObjectSubRole.MiningStation);
            if (design != null && builtObject != null && design.ArmorReactive >= 5 && builtObject.ArmorReactive >= 5)
            {
                return true;
            }
            return false;
        }

        public bool CheckEmpireTechCanSurviveStorms()
        {
            Component component = Research.EvaluateDesiredComponent(ComponentCategoryType.Armor, ShipDesignFocus.Balanced);
            if (component != null && component.Value2 >= 5)
            {
                return true;
            }
            return false;
        }

        public bool CanEmpireColonizeHabitat(Habitat habitat, out string explanation)
        {
            if (!_Galaxy.CheckEmpireTerritoryCanColonizeHabitat(this, habitat))
            {
                explanation = TextResolver.GetText("Colonization Not Possible - Foreign Territory");
                return false;
            }
            if (habitat.Empire == _Galaxy.IndependentEmpire && habitat.Population != null && habitat.Population.TotalAmount > 0)
            {
                if (!CanEmpireColonizeHabitatRange(this, habitat))
                {
                    explanation = TextResolver.GetText("Colonization Not Possible - Outside Range Limit");
                    return false;
                }
                int num = _Galaxy.CheckColonizationLikeliness(habitat, DominantRace);
                if (num <= -20)
                {
                    explanation = TextResolver.GetText("Colonization Probability Most unlikely");
                }
                else if (num <= -5)
                {
                    explanation = TextResolver.GetText("Colonization Probability Unlikely");
                }
                else if (num <= 5)
                {
                    explanation = TextResolver.GetText("Colonization Probability Possible");
                }
                else
                {
                    explanation = TextResolver.GetText("Colonization Probability Probable");
                }
                return true;
            }
            Design design = Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
            if (design == null)
            {
                design = Designs.FindNewest(BuiltObjectSubRole.ColonyShip);
            }
            if (CanDesignColonizeHabitat(design, habitat))
            {
                if (!CanEmpireColonizeHabitatRange(this, habitat))
                {
                    explanation = TextResolver.GetText("Colonization Not Possible - Outside Range Limit");
                    return false;
                }
                explanation = TextResolver.GetText("Colonization - Yes, using current colonization tech");
                return true;
            }
            List<HabitatType> list = ColonizableHabitatTypesNonTechForEmpire(this);
            if (list.Contains(habitat.Type))
            {
                if (!CanEmpireColonizeHabitatRange(this, habitat))
                {
                    explanation = TextResolver.GetText("Colonization Not Possible - Outside Range Limit");
                    return false;
                }
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat2 = Colonies[i];
                    if (habitat2.Population != null && habitat2.Population.DominantRace != null && habitat2.Population.DominantRace.NativeHabitatType == habitat.Type && (habitat.Category == HabitatCategoryType.Planet || habitat.Category == HabitatCategoryType.Moon))
                    {
                        explanation = string.Format(TextResolver.GetText("Colonization - Yes, native type for RACE"), habitat2.Population.DominantRace.Name);
                        return true;
                    }
                }
            }
            list = ColonizableHabitatTypesFromColonyShips(this, list);
            if (list.Contains(habitat.Type))
            {
                if (!CanEmpireColonizeHabitatRange(this, habitat))
                {
                    explanation = TextResolver.GetText("Colonization Not Possible - Outside Range Limit");
                    return false;
                }
                for (int j = 0; j < BuiltObjects.Count; j++)
                {
                    BuiltObject builtObject = BuiltObjects[j];
                    if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip)
                    {
                        int newPopulationAmount = 0;
                        if (CanBuiltObjectColonizeHabitat(builtObject, habitat, out newPopulationAmount))
                        {
                            explanation = string.Format(TextResolver.GetText("Colonization - Yes, using colony ship NAME"), builtObject.Name);
                            return true;
                        }
                    }
                }
            }
            explanation = TextResolver.GetText("No, unable to colonize");
            return false;
        }

        public HabitatPrioritizationList IdentifyColonizationTargets(Galaxy galaxy)
        {
            return IdentifyColonizationTargets(galaxy, filterOutDangerousTargets: true, 1, int.MaxValue);
        }

        public HabitatPrioritizationList IdentifyColonizationTargets(Galaxy galaxy, bool filterOutDangerousTargets, int thresholdValue, int maximumListSize)
        {
            return IdentifyColonizationTargets(galaxy, filterOutDangerousTargets, thresholdValue, maximumListSize, includeLowQualityTargets: true, includeDistantTargets: false);
        }

        public HabitatPrioritizationList IdentifyColonizationTargets(Galaxy galaxy, bool filterOutDangerousTargets, int thresholdValue, int maximumListSize, bool includeLowQualityTargets, bool includeDistantTargets)
        {
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            Design design = Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
            if (design == null)
            {
                design = Designs.FindNewest(BuiltObjectSubRole.ColonyShip);
            }
            bool flag = false;
            if (filterOutDangerousTargets)
            {
                flag = CheckEmpireTechCanSurviveStorms();
            }
            double num = (double)DominantRace.AggressionLevel / 100.0;
            double num2 = (double)DominantRace.CautionLevel / 100.0;
            int num3 = (int)(2000.0 / (num * num / (num2 * num2)));
            List<HabitatType> empireHabitatTypes = ColonizableHabitatTypesForEmpire(this);
            empireHabitatTypes = ColonizableHabitatTypesFromColonyShips(this, empireHabitatTypes);
            for (int i = 0; i < SystemVisibility.Count; i++)
            {
                if (!CheckSystemExplored(i))
                {
                    continue;
                }
                bool flag2 = false;
                if (_Galaxy.Systems[i].DominantEmpire != null && _Galaxy.Systems[i].DominantEmpire.Empire != null && _Galaxy.Systems[i].DominantEmpire.Empire != this)
                {
                    flag2 = true;
                }
                if (!flag2 && _Galaxy.Systems[i].SystemStar != null)
                {
                    bool disputed = false;
                    int num4 = _Galaxy.EmpireTerritory.CheckSystemOwnership(_Galaxy, _Galaxy.Systems[i].SystemStar, out disputed);
                    if (num4 >= 0 && num4 != EmpireId)
                    {
                        flag2 = true;
                    }
                }
                bool flag3 = false;
                if (filterOutDangerousTargets)
                {
                    flag3 = _Galaxy.CheckInStorm(_Galaxy.Systems[i].SystemStar.Xpos, _Galaxy.Systems[i].SystemStar.Ypos);
                    if (flag3 && flag)
                    {
                        flag3 = false;
                    }
                }
                if (flag3)
                {
                    continue;
                }
                for (int j = 0; j < _Galaxy.Systems[SystemVisibility[i].SystemStar.SystemIndex].Habitats.Count; j++)
                {
                    Habitat habitat = _Galaxy.Systems[SystemVisibility[i].SystemStar.SystemIndex].Habitats[j];
                    if ((habitat.Category != HabitatCategoryType.Planet && habitat.Category != HabitatCategoryType.Moon) || habitat.Type == HabitatType.BarrenRock || habitat.Type == HabitatType.FrozenGasGiant || habitat.Type == HabitatType.GasGiant || (!includeLowQualityTargets && !(habitat.Quality >= 0.5f) && (_ResourceMap == null || !_ResourceMap.CheckResourcesKnown(habitat) || habitat.Resources == null || !habitat.Resources.HasSuperLuxuryResources()) && (habitat.Ruin == null || !habitat.Ruin.PlayerEmpireEncountered || (!(habitat.Ruin.BonusDefensive > 0.0) && !(habitat.Ruin.BonusDiplomacy > 0.0) && !(habitat.Ruin.BonusHappiness > 0.0) && !(habitat.Ruin.BonusResearchEnergy > 0.0) && !(habitat.Ruin.BonusResearchHighTech > 0.0) && !(habitat.Ruin.BonusResearchWeapons > 0.0) && !(habitat.Ruin.BonusWealth > 0.0)))))
                    {
                        continue;
                    }
                    double num5 = 0.0;
                    if (((habitat.Empire != _Galaxy.IndependentEmpire || habitat.Population == null || habitat.Population.TotalAmount <= 0) && !CanEmpireColonizeHabitat(this, habitat, empireHabitatTypes, design, checkRange: false)) || (!includeDistantTargets && !CanEmpireColonizeHabitatRange(this, habitat)) || !_Galaxy.CheckEmpireTerritoryCanColonizeHabitat(this, habitat) || habitatPrioritizationList.IndexOf(habitat) >= 0 || (filterOutDangerousTargets && CheckNearPirateBase(habitat, habitat.Xpos, habitat.Ypos)))
                    {
                        continue;
                    }
                    Habitat habitat2 = habitat;
                    num5 = DetermineColonizationValue(habitat2);
                    if (!(num5 >= (double)thresholdValue))
                    {
                        continue;
                    }
                    bool flag4 = true;
                    if (flag2 && filterOutDangerousTargets && num5 < (double)num3)
                    {
                        flag4 = false;
                    }
                    if (filterOutDangerousTargets && CheckWhetherHabitatIsDangerous(habitat2))
                    {
                        flag4 = false;
                        if (_DangerousHabitats == null)
                        {
                            _DangerousHabitats = new HabitatList();
                        }
                        if (_DangerousHabitats.Count < 20 && !_DangerousHabitats.Contains(habitat2))
                        {
                            _DangerousHabitats.Add(habitat2);
                        }
                    }
                    if (flag4)
                    {
                        HabitatPrioritization item = new HabitatPrioritization(habitat2, (int)num5);
                        habitatPrioritizationList.Add(item);
                    }
                }
            }
            habitatPrioritizationList.Sort();
            habitatPrioritizationList.Reverse();
            for (int k = 0; k < BuiltObjects.Count; k++)
            {
                BuiltObject builtObject = BuiltObjects[k];
                if (builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Colonize && builtObject.Mission.TargetHabitat != null)
                {
                    Habitat targetHabitat = builtObject.Mission.TargetHabitat;
                    int num6 = habitatPrioritizationList.IndexOf(targetHabitat);
                    if (num6 >= 0)
                    {
                        habitatPrioritizationList[num6].AssignedShip = builtObject;
                    }
                }
            }
            if (maximumListSize < int.MaxValue && habitatPrioritizationList.Count > maximumListSize)
            {
                HabitatPrioritization[] sourceArray = habitatPrioritizationList.ToArray();
                HabitatPrioritization[] array = new HabitatPrioritization[maximumListSize];
                Array.Copy(sourceArray, 0, array, 0, maximumListSize);
                habitatPrioritizationList.Clear();
                habitatPrioritizationList.AddRange(array);
            }
            return habitatPrioritizationList;
        }

        public BuiltObject CheckColonizingHabitat(Habitat habitat)
        {
            BuiltObject result = null;
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = BuiltObjects[i];
                if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip && builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Colonize && builtObject.Mission.TargetHabitat == habitat)
                {
                    result = builtObject;
                    break;
                }
            }
            return result;
        }

        private void AssignShipMissions()
        {
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                habitat.CurrentDefensiveForceAssigned = 0;
            }
            for (int j = 0; j < BuiltObjects.Count; j++)
            {
                BuiltObject builtObject = BuiltObjects[j];
                if (builtObject.Role == BuiltObjectRole.Base && builtObject.ParentHabitat != null && builtObject.ParentHabitat.Empire == this)
                {
                    builtObject.ParentHabitat.CurrentDefensiveForceAssigned += builtObject.FirepowerRaw;
                }
                else if (builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.Patrol && builtObject.Mission.TargetHabitat != null)
                {
                    builtObject.Mission.TargetHabitat.CurrentDefensiveForceAssigned += builtObject.FirepowerRaw;
                }
            }
            Colonies.Sort();
            Colonies.Reverse();
            for (int k = 0; k < PrivateBuiltObjects.Count; k++)
            {
                BuiltObject builtObject2 = PrivateBuiltObjects[k];
                if (builtObject2.IsColony || builtObject2.IsResourceExtractor)
                {
                    builtObject2.CurrentEscortForceAssigned = 0;
                }
            }
            for (int l = 0; l < BuiltObjects.Count; l++)
            {
                BuiltObject builtObject3 = BuiltObjects[l];
                if (builtObject3.IsColony || builtObject3.IsResourceExtractor)
                {
                    builtObject3.CurrentEscortForceAssigned = 0;
                }
            }
            for (int m = 0; m < PrivateBuiltObjects.Count; m++)
            {
                BuiltObject builtObject4 = PrivateBuiltObjects[m];
                if (builtObject4.Mission != null && (builtObject4.Mission.Type == BuiltObjectMissionType.Escort || builtObject4.Mission.Type == BuiltObjectMissionType.Patrol) && builtObject4.Mission.TargetBuiltObject != null)
                {
                    BuiltObject targetBuiltObject = builtObject4.Mission.TargetBuiltObject;
                    targetBuiltObject.CurrentEscortForceAssigned += builtObject4.FirepowerRaw;
                }
            }
            for (int n = 0; n < BuiltObjects.Count; n++)
            {
                BuiltObject builtObject5 = BuiltObjects[n];
                if (builtObject5.Mission != null && (builtObject5.Mission.Type == BuiltObjectMissionType.Escort || builtObject5.Mission.Type == BuiltObjectMissionType.Patrol) && builtObject5.Mission.TargetBuiltObject != null)
                {
                    BuiltObject targetBuiltObject2 = builtObject5.Mission.TargetBuiltObject;
                    targetBuiltObject2.CurrentEscortForceAssigned += builtObject5.FirepowerRaw;
                }
            }
            int militaryStrength = 0;
            EmpireList empireList = DetermineEmpiresAtWarWith(out militaryStrength);
            bool atWar = false;
            if (empireList.Count > 0)
            {
                atWar = true;
            }
            BuiltObjectList patrolMiningStations = ResolvePrioritizedPatrolMiningStations();
            AssignMissionsToBuiltObjectList(BuiltObjects, atWar, patrolMiningStations);
            AssignMissionsToBuiltObjectList(PrivateBuiltObjects, atWar, patrolMiningStations);
        }

        public bool AssignRepairMission(BuiltObject builtObject)
        {
            Empire actualEmpire = builtObject.ActualEmpire;
            if (actualEmpire == null || actualEmpire == _Galaxy.IndependentEmpire)
            {
                return false;
            }
            if (builtObject.Role != BuiltObjectRole.Base && builtObject.TopSpeed <= 0)
            {
                return false;
            }
            StellarObject stellarObject = FindNearestShipYard(builtObject, canRepairOrBuild: true, includeVerySmallYards: true);
            if (stellarObject != null)
            {
                double num = _Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, stellarObject.Xpos, stellarObject.Ypos);
                if (builtObject.WarpSpeed <= 0 && num > (double)Galaxy.HyperJumpThreshhold && (builtObject.TopSpeed <= 0 || !(num < (double)Galaxy.MaxSolarSystemSize)))
                {
                    return false;
                }
            }
            if (stellarObject != null)
            {
                if (builtObject.UnbuiltComponentCount > 0)
                {
                    ComponentList componentList = ResolveUnbuiltComponents(builtObject);
                    if (componentList.Count > 0)
                    {
                        CargoList resourcesToOrder;
                        if (stellarObject is BuiltObject)
                        {
                            ProcureConstructionComponents(builtObject, (BuiltObject)stellarObject, orderPreciseResourceAmounts: true, out resourcesToOrder, componentList);
                            foreach (Cargo item in resourcesToOrder)
                            {
                                CreateOrder((BuiltObject)stellarObject, item.CommodityResource, item.Amount, isState: false, OrderType.ConstructionShortage);
                            }
                        }
                        else if (stellarObject is Habitat)
                        {
                            ProcureConstructionComponents(builtObject, (Habitat)stellarObject, out resourcesToOrder, componentList);
                            foreach (Cargo item2 in resourcesToOrder)
                            {
                                CreateOrder((Habitat)stellarObject, item2.CommodityResource, item2.Amount, isState: false, OrderType.ConstructionShortage);
                            }
                        }
                    }
                }
                builtObject.ClearPreviousMissionRequirements();
                builtObject.AssignMission(BuiltObjectMissionType.Repair, stellarObject, null, BuiltObjectMissionPriority.VeryHigh);
                return true;
            }
            return false;
        }

        private ComponentList ResolveUnbuiltComponents(BuiltObject builtObject)
        {
            ComponentList componentList = new ComponentList();
            for (int i = 0; i < builtObject.Components.Count; i++)
            {
                BuiltObjectComponent builtObjectComponent = builtObject.Components[i];
                if (builtObjectComponent.Status == ComponentStatus.Unbuilt)
                {
                    componentList.Add(new Component(builtObjectComponent.ComponentID));
                }
            }
            return componentList;
        }
    }
}
