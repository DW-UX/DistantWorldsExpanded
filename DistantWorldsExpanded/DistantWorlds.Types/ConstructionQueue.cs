// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ConstructionQueue
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using BaconDistantWorlds;

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
    [Serializable]
    public class ConstructionQueue
    {
        [NonSerialized]
        private object _LockObject = new object();

        private Galaxy _Galaxy;

        private BuiltObject _ParentBuiltObject;

        private Habitat _ParentHabitat;

        private ConstructionYardList _ConstructionYards;

        private BuiltObjectList _ConstructionWaitQueue;

        private DateTime _LastProcessed;

        public double MilitaryConstructionSpeedModifier = 1.0;

        public double CivilianConstructionSpeedModifier = 1.0;

        public double ColonyConstructionSpeedModifier = 1.0;

        public int _ConstructionSpeed = 1;

        public BuiltObject ParentBuiltObject => _ParentBuiltObject;

        public Habitat ParentHabitat => _ParentHabitat;

        public int ConstructionSpeed => _ConstructionSpeed;

        public Empire Empire
        {
            get
            {
                if (_ParentBuiltObject != null)
                {
                    return _ParentBuiltObject.Empire;
                }
                if (_ParentHabitat != null)
                {
                    return _ParentHabitat.Empire;
                }
                return null;
            }
        }

        public ConstructionYardList ConstructionYards => _ConstructionYards;

        public BuiltObjectList ConstructionWaitQueue => _ConstructionWaitQueue;

        public void ReviewConstructionSpeed()
        {
            BaconConstructionQueue.ReviewConstructionSpeed(this);
        }

        public ConstructionQueue(BuiltObject builtObject, Galaxy galaxy)
        {
            _Galaxy = galaxy;
            _ParentBuiltObject = builtObject;
            _ParentHabitat = null;
            Redefine(builtObject);
            _LastProcessed = _Galaxy.CurrentDateTime;
        }

        public ConstructionQueue(Habitat habitat, Galaxy galaxy)
        {
            _Galaxy = galaxy;
            _ParentHabitat = habitat;
            _ParentBuiltObject = null;
            Redefine(habitat);
            _LastProcessed = _Galaxy.CurrentDateTime;
        }

        public BuiltObjectList GetUnderConstruction(List<BuiltObjectSubRole> subRoles)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < ConstructionYards.Count; i++)
            {
                ConstructionYard constructionYard = ConstructionYards[i];
                if (constructionYard.ShipUnderConstruction != null && subRoles.Contains(constructionYard.ShipUnderConstruction.SubRole))
                {
                    builtObjectList.Add(constructionYard.ShipUnderConstruction);
                }
            }
            if (ConstructionWaitQueue != null)
            {
                for (int j = 0; j < ConstructionWaitQueue.Count; j++)
                {
                    BuiltObject builtObject = ConstructionWaitQueue[j];
                    if (builtObject != null && subRoles.Contains(builtObject.SubRole))
                    {
                        builtObjectList.Add(builtObject);
                    }
                }
            }
            return builtObjectList;
        }

        public int CountUnderConstruction(BuiltObjectSubRole subRole)
        {
            int num = 0;
            for (int i = 0; i < ConstructionYards.Count; i++)
            {
                if (ConstructionYards[i].ShipUnderConstruction != null && ConstructionYards[i].ShipUnderConstruction.SubRole == subRole)
                {
                    num++;
                }
            }
            if (ConstructionWaitQueue != null)
            {
                for (int j = 0; j < ConstructionWaitQueue.Count; j++)
                {
                    if (ConstructionWaitQueue[j] != null && ConstructionWaitQueue[j].SubRole == subRole)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public double EstimateCurrentWaitQueueTime()
        {
            double num = 40.0 / ((double)ConstructionSpeed / 1000.0);
            if (_ConstructionWaitQueue.Count == 0)
            {
                for (int i = 0; i < _ConstructionYards.Count; i++)
                {
                    ConstructionYard constructionYard = _ConstructionYards[i];
                    if (constructionYard.ShipUnderConstruction == null)
                    {
                        return num;
                    }
                }
            }
            double num2 = 4.4942328371557893E+307;
            for (int j = 0; j < _ConstructionYards.Count; j++)
            {
                ConstructionYard constructionYard2 = _ConstructionYards[j];
                if (constructionYard2.ShipUnderConstruction != null)
                {
                    double num3 = (double)constructionYard2.ShipUnderConstruction.UnbuiltOrDamagedComponentCount / ((double)constructionYard2.ConstructionSpeed / 1000.0);
                    if (num3 < num2)
                    {
                        num2 = num3;
                    }
                    continue;
                }
                num2 = 0.0;
                break;
            }
            int num4 = 0;
            for (int k = 0; k < _ConstructionWaitQueue.Count; k++)
            {
                BuiltObject builtObject = _ConstructionWaitQueue[k];
                num4 += builtObject.UnbuiltOrDamagedComponentCount;
            }
            int num5 = 0;
            num5 = ((_ConstructionYards.Count > 0) ? (num4 / _ConstructionYards.Count) : 536870911);
            double num6 = (double)num5 / ((double)ConstructionSpeed / 1000.0);
            return num + num2 + num6;
        }

        public void Redefine(Habitat habitat)
        {
            if (_ConstructionYards == null)
            {
                _ConstructionYards = new ConstructionYardList();
            }
            if (_ConstructionWaitQueue == null)
            {
                _ConstructionWaitQueue = new BuiltObjectList();
            }
            BuiltObjectComponent builtObjectComponent = new BuiltObjectComponent(94, ComponentStatus.Normal);
            ConstructionYard item = new ConstructionYard(builtObjectComponent.ComponentID, builtObjectComponent.BuiltObjectComponentId, 20000, builtObjectComponent.Value1);
            _ConstructionYards.Add(item);
            ReviewConstructionSpeed();
        }

        public bool Redefine(BuiltObject builtObject)
        {
            return Redefine(builtObject, forceSingleConstructionYard: false);
        }

        public bool Redefine(BuiltObject builtObject, bool forceSingleConstructionYard)
        {
            if (_ConstructionYards == null)
            {
                _ConstructionYards = new ConstructionYardList();
            }
            if (_ConstructionWaitQueue == null)
            {
                _ConstructionWaitQueue = new BuiltObjectList();
            }
            bool flag = false;
            for (int i = 0; i < _ParentBuiltObject.Components.Count; i++)
            {
                BuiltObjectComponent builtObjectComponent = _ParentBuiltObject.Components[i];
                if (builtObjectComponent.Type != ComponentType.ConstructionBuild)
                {
                    continue;
                }
                int num = _ConstructionYards.IndexOf(builtObjectComponent);
                if (builtObjectComponent.Status == ComponentStatus.Damaged || builtObjectComponent.Status == ComponentStatus.Unbuilt)
                {
                    if (num >= 0)
                    {
                        if (_ConstructionYards[num].ShipUnderConstruction != null)
                        {
                            _ConstructionYards[num].RetrofitComponentsToBeBuilt = null;
                            _ConstructionWaitQueue.Insert(0, _ConstructionYards[num].ShipUnderConstruction);
                            _ConstructionYards[num].ShipUnderConstruction = null;
                        }
                        _ConstructionYards.RemoveAt(num);
                    }
                }
                else
                {
                    flag = true;
                    if (num < 0)
                    {
                        ConstructionYard item = new ConstructionYard(builtObjectComponent.ComponentID, builtObjectComponent.BuiltObjectComponentId, builtObjectComponent.Value2, builtObjectComponent.Value1);
                        _ConstructionYards.Add(item);
                    }
                }
            }
            if (!flag && forceSingleConstructionYard)
            {
                ConstructionYard item2 = new ConstructionYard(94, -1, 5000, 50);
                _ConstructionYards.Add(item2);
                flag = true;
            }
            if (!flag)
            {
                _ConstructionYards = null;
                _ConstructionWaitQueue = null;
            }
            ReviewConstructionSpeed();
            return flag;
        }

        private void ProcessWaitQueue()
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            Empire buildingEmpire = null;
            if (_ParentBuiltObject != null)
            {
                buildingEmpire = _ParentBuiltObject.Empire;
            }
            else if (_ParentHabitat != null)
            {
                buildingEmpire = _ParentHabitat.Empire;
            }
            if (_ConstructionWaitQueue.Count <= 0)
            {
                return;
            }
            foreach (BuiltObject item in _ConstructionWaitQueue)
            {
                if (_ConstructionYards.AddBuiltObjectToConstruct(item, buildingEmpire))
                {
                    builtObjectList.Add(item);
                }
            }
            foreach (BuiltObject item2 in builtObjectList)
            {
                _ConstructionWaitQueue.Remove(item2);
            }
        }

        private void ProcessSingleConstructionYard(ConstructionYard constructionYard, TimeSpan timePassed, Galaxy galaxy, BuiltObjectList completedBuiltObjects)
        {
            if (constructionYard == null || constructionYard.ShipUnderConstruction == null)
            {
                return;
            }
            if (constructionYard.IncrementalProgress < 0f)
            {
                constructionYard.IncrementalProgress = 0f;
            }
            double num = 0.0;
            if (_ParentBuiltObject != null)
            {
                _ParentBuiltObject.DoingConstruction = true;
            }
            BuiltObject shipUnderConstruction = constructionYard.ShipUnderConstruction;
            if (shipUnderConstruction.Scrap)
            {
                num = timePassed.TotalMilliseconds / 1000.0 * ((double)constructionYard.ConstructionSpeed / 1000.0 * 4.0) / ((double)constructionYard.BuildSpeedModifier * 2.0);
            }
            else if (shipUnderConstruction.RetrofitDesign != null && constructionYard.RetrofitComponentsToBeScrapped != null && constructionYard.RetrofitComponentsToBeScrapped.Count > 0 && (constructionYard.RetrofitComponentsToBeBuilt == null || constructionYard.RetrofitComponentsToBeBuilt.Count <= 0))
            {
                num = timePassed.TotalMilliseconds / 1000.0 * ((double)constructionYard.ConstructionSpeed / 1000.0 * 4.0) / (double)constructionYard.BuildSpeedModifier;
            }
            else
            {
                num = timePassed.TotalMilliseconds / 1000.0 * ((double)constructionYard.ConstructionSpeed / 1000.0) / (double)constructionYard.BuildSpeedModifier;
                if (shipUnderConstruction.SubRole == BuiltObjectSubRole.ColonyShip)
                {
                    num /= Galaxy.ColonyShipBuildFactor;
                    if (Empire != null)
                    {
                        num *= Empire.ColonyShipBuildSpeedRate;
                    }
                }
                else if (shipUnderConstruction.Role != BuiltObjectRole.Base && shipUnderConstruction.Design.IsPlanetDestroyer)
                {
                    num /= 5.0;
                }
            }
            Empire empire = null;
            if (_ParentBuiltObject != null)
            {
                empire = _ParentBuiltObject.Empire;
            }
            else if (_ParentHabitat != null)
            {
                empire = _ParentHabitat.Empire;
            }
            if (empire != null && empire.DominantRace != null)
            {
                num = num * empire.DominantRace.ConstructionSpeedModifier * BaconConstructionQueue.ConstructionSpeedMultiplier(this);
            }
            if (shipUnderConstruction != null)
            {
                switch (shipUnderConstruction.Role)
                {
                    case BuiltObjectRole.Military:
                        num *= MilitaryConstructionSpeedModifier;
                        break;
                    case BuiltObjectRole.Base:
                        switch (shipUnderConstruction.SubRole)
                        {
                            case BuiltObjectSubRole.SmallSpacePort:
                            case BuiltObjectSubRole.MediumSpacePort:
                            case BuiltObjectSubRole.LargeSpacePort:
                            case BuiltObjectSubRole.DefensiveBase:
                                num *= MilitaryConstructionSpeedModifier;
                                break;
                            default:
                                num *= CivilianConstructionSpeedModifier;
                                break;
                        }
                        break;
                    default:
                        num = ((shipUnderConstruction.SubRole != BuiltObjectSubRole.ColonyShip) ? (num * CivilianConstructionSpeedModifier) : (num * ColonyConstructionSpeedModifier));
                        break;
                }
            }
            if ((double)constructionYard.BuildSpeedModifier > 1.0 && constructionYard.ShipUnderConstruction != null && !constructionYard.ShipUnderConstruction.Scrap && Empire != null && Empire.Research != null)
            {
                ResearchNode researchNode = Empire.Research.SelectRandomNextResearchProjectExcludeSuperWeapons(galaxy);
                if (researchNode != null)
                {
                    double num2 = num * Galaxy.AdvancedTechBonusFactor * (double)constructionYard.BuildSpeedModifier;
                    if (double.IsNaN(num2))
                    {
                        num2 = 100.0;
                    }
                    researchNode.Progress += (float)num2;
                    if (researchNode.Progress >= researchNode.Cost)
                    {
                        Empire.DoResearchBreakthrough(researchNode, selfResearched: true);
                    }
                }
            }
            constructionYard.IncrementalProgress += (float)num;
            if (!((double)constructionYard.IncrementalProgress >= 1.0))
            {
                return;
            }
            int num3 = (int)constructionYard.IncrementalProgress;
            constructionYard.IncrementalProgress -= num3;
            BuiltObject shipUnderConstruction2 = constructionYard.ShipUnderConstruction;
            int iterationCount = 0;
            while (Galaxy.ConditionCheckLimit(num3 > 0 && shipUnderConstruction2 != null && shipUnderConstruction2.Components != null, 500, ref iterationCount))
            {
                int num4 = 0;
                if (shipUnderConstruction2.Scrap)
                {
                    num4 = shipUnderConstruction2.Components.FindNextBuiltComponent(0);
                    if (num4 >= 0)
                    {
                        int iterationCount2 = 0;
                        while (Galaxy.ConditionCheckLimit(num4 >= 0 && num3 > 0, 500, ref iterationCount2))
                        {
                            CargoList cargoList = null;
                            Empire empire2 = null;
                            int num5 = 0;
                            if (_ParentBuiltObject != null)
                            {
                                if (_ParentBuiltObject.Cargo != null)
                                {
                                    cargoList = _ParentBuiltObject.Cargo;
                                }
                                empire2 = _ParentBuiltObject.Empire;
                                num5 = _ParentBuiltObject.CargoSpace;
                            }
                            else if (_ParentHabitat != null && _ParentHabitat.Population.TotalAmount > 0)
                            {
                                if (_ParentHabitat.Cargo != null)
                                {
                                    cargoList = _ParentHabitat.Cargo;
                                }
                                empire2 = _ParentHabitat.Owner;
                                num5 = 536870911;
                            }
                            if (shipUnderConstruction2.Components[num4].Status == ComponentStatus.Normal && cargoList != null && num5 > 0)
                            {
                                foreach (ComponentResource requiredResource in shipUnderConstruction2.Components[num4].RequiredResources)
                                {
                                    Resource resource = new Resource(requiredResource.ResourceID);
                                    Cargo cargo = new Cargo(resource, requiredResource.Quantity, empire2);
                                    cargoList.Add(cargo);
                                }
                            }
                            shipUnderConstruction2.Components[num4].Status = ComponentStatus.Unbuilt;
                            num4 = shipUnderConstruction2.Components.FindNextBuiltComponent(num4 + 1);
                            num3--;
                        }
                        continue;
                    }
                    _Galaxy.CheckTriggerEvent(shipUnderConstruction2.GameEventId, Empire, EventTriggerType.Destroy, null);
                    if (Empire != null && Empire.Research != null)
                    {
                        ComponentCategoryType researchCategory = ComponentCategoryType.Undefined;
                        double num6 = Galaxy.ResolveBuildSpeed(Empire, _Galaxy, shipUnderConstruction2, out researchCategory);
                        if (num6 > 1.0)
                        {
                            string text = string.Empty;
                            ResearchNode researchNode2 = Empire.Research.SelectRandomNextResearchProjectExcludeSuperWeapons(galaxy, researchCategory);
                            if (researchNode2 == null)
                            {
                                researchNode2 = Empire.Research.SelectRandomNextResearchProjectExcludeSuperWeapons(galaxy);
                            }
                            if (researchNode2 == null || Galaxy.Rnd.Next(0, 5) == 6)
                            {
                                if (_ParentBuiltObject != null)
                                {
                                    text = string.Format(TextResolver.GetText("We have disassembled the ship X at Y"), shipUnderConstruction2.Name, _ParentBuiltObject.Name);
                                }
                                else if (_ParentHabitat != null)
                                {
                                    text = string.Format(TextResolver.GetText("We have disassembled the ship X at Y"), shipUnderConstruction2.Name, _ParentHabitat.Name);
                                }
                                text = text + ". " + TextResolver.GetText("Unfortunately our engineers were unable to learn anything new from inspecting its technology") + ".";
                            }
                            else
                            {
                                double num7 = (double)shipUnderConstruction2.Size * Galaxy.AdvancedTechBonusFactor * num6;
                                if (double.IsNaN(num7))
                                {
                                    num7 = 10000.0;
                                }
                                researchNode2.Progress += (float)num7;
                                if (_ParentBuiltObject != null)
                                {
                                    text = string.Format(TextResolver.GetText("We have received a research bonus in X from disassembling Y"), researchNode2.Name, shipUnderConstruction2.Name, _ParentBuiltObject.Name);
                                }
                                else if (_ParentHabitat != null)
                                {
                                    text = string.Format(TextResolver.GetText("We have received a research bonus in X from disassembling Y"), researchNode2.Name, shipUnderConstruction2.Name, _ParentHabitat.Name);
                                }
                                text += ".";
                                if (researchNode2.Progress >= researchNode2.Cost)
                                {
                                    Empire.DoResearchBreakthrough(researchNode2, selfResearched: true, blockMessages: true, suppressUpdate: false);
                                }
                            }
                            if (_ParentBuiltObject == null)
                            {
                                _ = _ParentHabitat;
                            }
                            Empire.SendMessageToEmpire(Empire, EmpireMessageType.GeneralGoodEvent, shipUnderConstruction2, text);
                        }
                    }
                    if (shipUnderConstruction2.Characters != null && shipUnderConstruction2.Characters.Count > 0)
                    {
                        if (_ParentBuiltObject != null)
                        {
                            if (_ParentBuiltObject.ParentHabitat != null)
                            {
                                if (_ParentBuiltObject.ParentHabitat.Characters != null)
                                {
                                    Character[] array = ListHelper.ToArrayThreadSafe(shipUnderConstruction2.Characters);
                                    for (int i = 0; i < array.Length; i++)
                                    {
                                        array[i]?.CompleteLocationTransfer(_ParentBuiltObject.ParentHabitat, _Galaxy);
                                    }
                                }
                            }
                            else if (_ParentBuiltObject.Characters != null)
                            {
                                Character[] array2 = ListHelper.ToArrayThreadSafe(shipUnderConstruction2.Characters);
                                for (int j = 0; j < array2.Length; j++)
                                {
                                    array2[j]?.CompleteLocationTransfer(_ParentBuiltObject, _Galaxy);
                                }
                            }
                        }
                        else if (_ParentHabitat != null && _ParentHabitat.Characters != null)
                        {
                            Character[] array3 = ListHelper.ToArrayThreadSafe(shipUnderConstruction2.Characters);
                            for (int k = 0; k < array3.Length; k++)
                            {
                                array3[k]?.CompleteLocationTransfer(_ParentHabitat, _Galaxy);
                            }
                        }
                    }
                    if (shipUnderConstruction2.Troops != null && shipUnderConstruction2.Troops.Count > 0)
                    {
                        if (_ParentBuiltObject != null)
                        {
                            if (_ParentBuiltObject.ParentHabitat != null)
                            {
                                if (_ParentBuiltObject.ParentHabitat.Troops != null)
                                {
                                    for (int l = 0; l < shipUnderConstruction2.Troops.Count; l++)
                                    {
                                        Troop troop = shipUnderConstruction2.Troops[l];
                                        if (troop != null)
                                        {
                                            troop.BuiltObject = null;
                                            shipUnderConstruction2.Troops.Remove(troop);
                                            _ParentBuiltObject.ParentHabitat.Troops.Add(troop);
                                            troop.Colony = _ParentBuiltObject.ParentHabitat;
                                        }
                                    }
                                }
                            }
                            else if (_ParentBuiltObject.Troops != null)
                            {
                                for (int m = 0; m < shipUnderConstruction2.Troops.Count; m++)
                                {
                                    Troop troop2 = shipUnderConstruction2.Troops[m];
                                    if (troop2 != null)
                                    {
                                        troop2.BuiltObject = null;
                                        troop2.Colony = null;
                                        shipUnderConstruction2.Troops.Remove(troop2);
                                        _ParentBuiltObject.Troops.Add(troop2);
                                        troop2.BuiltObject = _ParentBuiltObject;
                                    }
                                }
                            }
                        }
                        else if (_ParentHabitat != null && _ParentHabitat.Troops != null)
                        {
                            for (int n = 0; n < shipUnderConstruction2.Troops.Count; n++)
                            {
                                Troop troop3 = shipUnderConstruction2.Troops[n];
                                if (troop3 != null)
                                {
                                    troop3.BuiltObject = null;
                                    shipUnderConstruction2.Troops.Remove(troop3);
                                    _ParentHabitat.Troops.Add(troop3);
                                    troop3.Colony = _ParentHabitat;
                                }
                            }
                        }
                    }
                    shipUnderConstruction2.BuiltAt = null;
                    shipUnderConstruction2.ParentBuiltObject = null;
                    shipUnderConstruction2.ParentHabitat = null;
                    shipUnderConstruction2.ParentOffsetX = -2000000001.0;
                    shipUnderConstruction2.ParentOffsetY = -2000000001.0;
                    constructionYard.ShipUnderConstruction = null;
                    num3 = 0;
                    shipUnderConstruction2.CompleteTeardown(galaxy);
                    constructionYard.IncrementalProgress = 0f;
                    constructionYard.ShipUnderConstruction = null;
                    continue;
                }
                if (shipUnderConstruction2.RetrofitDesign != null)
                {
                    if (constructionYard.RetrofitComponentsToBeBuilt == null)
                    {
                        constructionYard.RetrofitComponentsToBeBuilt = shipUnderConstruction2.Components.ResolveComponentList().Diff(shipUnderConstruction2.RetrofitDesign.Components);
                        constructionYard.RetrofitComponentsToBeScrapped = shipUnderConstruction2.RetrofitDesign.Components.Diff(shipUnderConstruction2.Components.ResolveComponentList());
                    }
                    CargoList cargoList2 = new CargoList();
                    int num8 = 0;
                    Empire empire3 = null;
                    if (_ParentBuiltObject != null)
                    {
                        if (_ParentBuiltObject.Cargo != null)
                        {
                            cargoList2 = _ParentBuiltObject.Cargo;
                        }
                        num8 = _ParentBuiltObject.CargoSpace;
                        empire3 = _ParentBuiltObject.Empire;
                    }
                    else if (_ParentHabitat != null)
                    {
                        if (_ParentHabitat.Cargo != null)
                        {
                            cargoList2 = _ParentHabitat.Cargo;
                        }
                        num8 = _ParentHabitat.CargoSpace;
                        empire3 = _ParentHabitat.Empire;
                    }
                    int num9 = IdentifyComponentToRepair(shipUnderConstruction2);
                    if (num9 >= 0)
                    {
                        shipUnderConstruction2.Components[num9].Status = ComponentStatus.Normal;
                        num3--;
                        continue;
                    }
                    if (constructionYard.RetrofitComponentsToBeBuilt != null && constructionYard.RetrofitComponentsToBeBuilt.Count > 0)
                    {
                        num4 = IdentifyComponentToBuild(cargoList2, empire3, constructionYard.RetrofitComponentsToBeBuilt);
                        if (num4 >= 0)
                        {
                            Cargo cargo2 = cargoList2.GetCargo(constructionYard.RetrofitComponentsToBeBuilt[num4], empire3);
                            if (cargo2 != null && cargo2.Amount > 0)
                            {
                                cargo2.Amount--;
                                cargo2.Reserved--;
                                if (cargo2.Amount <= 0 && cargo2.Reserved <= 0)
                                {
                                    cargoList2.Remove(cargo2);
                                }
                                BuiltObjectComponent component = new BuiltObjectComponent(constructionYard.RetrofitComponentsToBeBuilt[num4].ComponentID, ComponentStatus.Normal);
                                shipUnderConstruction2.Components.Add(component);
                                constructionYard.RetrofitComponentsToBeBuilt.RemoveAt(num4);
                            }
                        }
                        num3--;
                        continue;
                    }
                    if (constructionYard.RetrofitComponentsToBeBuilt != null && constructionYard.RetrofitComponentsToBeScrapped != null && constructionYard.RetrofitComponentsToBeScrapped.Count > 0 && constructionYard.RetrofitComponentsToBeBuilt.Count <= 0)
                    {
                        shipUnderConstruction2.PictureRef = shipUnderConstruction2.RetrofitDesign.PictureRef;
                        shipUnderConstruction2.SubRole = shipUnderConstruction2.RetrofitDesign.SubRole;
                        int num10 = shipUnderConstruction2.Components.IndexByIdAndStatus(constructionYard.RetrofitComponentsToBeScrapped[0].ComponentID, ComponentStatus.Normal);
                        if (num10 >= 0)
                        {
                            if (cargoList2 != null && num8 > 0)
                            {
                                foreach (ComponentResource requiredResource2 in shipUnderConstruction2.Components[num10].RequiredResources)
                                {
                                    Resource resource2 = new Resource(requiredResource2.ResourceID);
                                    Cargo cargo3 = new Cargo(resource2, requiredResource2.Quantity, empire3);
                                    cargoList2.Add(cargo3);
                                }
                            }
                            shipUnderConstruction2.Components[num10].Status = ComponentStatus.Unbuilt;
                        }
                        constructionYard.RetrofitComponentsToBeScrapped.RemoveAt(0);
                        num3--;
                        continue;
                    }
                    shipUnderConstruction2.Design = shipUnderConstruction2.RetrofitDesign;
                    if (shipUnderConstruction2.SubRole != shipUnderConstruction2.Design.SubRole && (shipUnderConstruction2.SubRole == BuiltObjectSubRole.SmallSpacePort || shipUnderConstruction2.SubRole == BuiltObjectSubRole.MediumSpacePort || shipUnderConstruction2.SubRole == BuiltObjectSubRole.LargeSpacePort) && (shipUnderConstruction2.Design.SubRole == BuiltObjectSubRole.SmallSpacePort || shipUnderConstruction2.Design.SubRole == BuiltObjectSubRole.MediumSpacePort || shipUnderConstruction2.Design.SubRole == BuiltObjectSubRole.LargeSpacePort))
                    {
                        shipUnderConstruction2.SubRole = shipUnderConstruction2.Design.SubRole;
                        shipUnderConstruction2.PictureRef = shipUnderConstruction2.Design.PictureRef;
                    }
                    shipUnderConstruction2.ReDefine();
                    for (int num11 = shipUnderConstruction2.Components.FindNextUnbuiltComponent(0); num11 >= 0; num11 = shipUnderConstruction2.Components.FindNextUnbuiltComponent(0))
                    {
                        shipUnderConstruction2.Components.RemoveAt(num11);
                    }
                    shipUnderConstruction2.ReDefine();
                    constructionYard.RetrofitComponentsToBeBuilt = null;
                    constructionYard.RetrofitComponentsToBeScrapped = null;
                    shipUnderConstruction2.RetrofitDesign = null;
                    shipUnderConstruction2.BuiltAt = null;
                    shipUnderConstruction2.DateRetrofit = _Galaxy.CurrentStarDate;
                    string arg = string.Empty;
                    if (_ParentBuiltObject != null)
                    {
                        arg = ((_ParentBuiltObject.ParentHabitat == null) ? _ParentBuiltObject.Name : _ParentBuiltObject.ParentHabitat.Name);
                    }
                    else if (_ParentHabitat != null)
                    {
                        arg = _ParentHabitat.Name;
                    }
                    string empty = string.Empty;
                    empty = ((shipUnderConstruction2.Role != BuiltObjectRole.Base) ? (empty + string.Format(TextResolver.GetText("Retrofitting for the SHIPTYPE NAME has been completed at LOCATION"), Galaxy.ResolveDescription(shipUnderConstruction2.SubRole), shipUnderConstruction2.Name, arg)) : (empty + string.Format(TextResolver.GetText("Retrofitting for the SHIPTYPE NAME has been completed at LOCATION"), "base", shipUnderConstruction2.Name, arg)));
                    constructionYard.IncrementalProgress = 0f;
                    constructionYard.ShipUnderConstruction = null;
                    num3 = 0;
                    shipUnderConstruction2.FirstExecutionOfCommand = true;
                    if (shipUnderConstruction2.Mission != null)
                    {
                        shipUnderConstruction2.Mission.CompleteCommand();
                    }
                    completedBuiltObjects.Add(shipUnderConstruction2);
                    if (double.IsNaN(shipUnderConstruction2.CurrentFuel))
                    {
                        shipUnderConstruction2.CurrentFuel = 0.0;
                    }
                    if (double.IsNaN(shipUnderConstruction2.CurrentEnergy))
                    {
                        shipUnderConstruction2.CurrentEnergy = 0.0;
                    }
                    shipUnderConstruction2.ReDefine();
                    if (shipUnderConstruction2.FuelType != null)
                    {
                        int num12 = -1;
                        CargoList cargoList3 = null;
                        if (_ParentBuiltObject != null)
                        {
                            cargoList3 = _ParentBuiltObject.Cargo;
                        }
                        else if (_ParentHabitat != null)
                        {
                            cargoList3 = _ParentHabitat.Cargo;
                        }
                        if (cargoList3 != null)
                        {
                            num12 = cargoList3.IndexOf(shipUnderConstruction2.FuelType, shipUnderConstruction2.Empire);
                        }
                        if (num12 >= 0)
                        {
                            int num13 = 0;
                            num13 = ((cargoList3[num12].Available < shipUnderConstruction2.FuelCapacity - (int)shipUnderConstruction2.CurrentFuel) ? cargoList3[num12].Available : (shipUnderConstruction2.FuelCapacity - (int)shipUnderConstruction2.CurrentFuel));
                            cargoList3[num12].Amount -= num13;
                            shipUnderConstruction2.CurrentFuel += num13;
                        }
                    }
                    shipUnderConstruction2.CurrentEnergy = Math.Max(shipUnderConstruction2.CurrentEnergy, 0.0);
                    if (shipUnderConstruction2.TroopCapacity > 0 && shipUnderConstruction2.Empire != null && shipUnderConstruction2.Empire.Policy != null && shipUnderConstruction2.TroopLoadoutArmored != byte.MaxValue && shipUnderConstruction2.TroopLoadoutArtillery != byte.MaxValue && shipUnderConstruction2.TroopLoadoutInfantry != byte.MaxValue && shipUnderConstruction2.TroopLoadoutSpecialForces != byte.MaxValue)
                    {
                        shipUnderConstruction2.SetTroopLoadoutsFromPolicy(shipUnderConstruction2.Empire.Policy);
                    }
                    if (!_Galaxy.AssignFleetWaypointMission(shipUnderConstruction2, allowMissionOverride: true, null) && shipUnderConstruction2.TopSpeed > 0 && (shipUnderConstruction2.Mission == null || shipUnderConstruction2.Mission.Type == BuiltObjectMissionType.Undefined))
                    {
                        _Galaxy.SelectRelativeParkingPoint(out var x, out var y);
                        if (_ParentHabitat != null)
                        {
                            shipUnderConstruction2.AssignMission(BuiltObjectMissionType.Move, _ParentHabitat, null, x, y, BuiltObjectMissionPriority.Low);
                        }
                        else if (_ParentBuiltObject != null)
                        {
                            shipUnderConstruction2.AssignMission(BuiltObjectMissionType.Move, _ParentBuiltObject, null, x, y, BuiltObjectMissionPriority.Low);
                        }
                    }
                    if (shipUnderConstruction2.Empire != null && shipUnderConstruction2.Empire.BuiltObjects != null && shipUnderConstruction2.Empire.BuiltObjects.Contains(shipUnderConstruction2))
                    {
                        shipUnderConstruction2.Empire.SendMessageToEmpire(shipUnderConstruction2.Empire, EmpireMessageType.ShipBaseCompleted, shipUnderConstruction2, empty);
                    }
                    ProcessWaitQueue();
                    if (constructionYard.ShipUnderConstruction == null)
                    {
                        break;
                    }
                    shipUnderConstruction2 = constructionYard.ShipUnderConstruction;
                    continue;
                }
                CargoList cargoList4 = new CargoList();
                Empire empire4 = null;
                if (_ParentBuiltObject != null)
                {
                    if (_ParentBuiltObject.Cargo != null)
                    {
                        cargoList4 = _ParentBuiltObject.Cargo;
                    }
                    empire4 = _ParentBuiltObject.Empire;
                }
                else if (_ParentHabitat != null)
                {
                    if (_ParentHabitat.Cargo != null)
                    {
                        cargoList4 = _ParentHabitat.Cargo;
                    }
                    empire4 = _ParentHabitat.Empire;
                }
                num4 = IdentifyComponentToBuild(cargoList4, empire4, shipUnderConstruction2);
                if (num4 >= 0)
                {
                    if (shipUnderConstruction2.Components[num4].Status == ComponentStatus.Damaged)
                    {
                        shipUnderConstruction2.Components[num4].Status = ComponentStatus.Normal;
                    }
                    else
                    {
                        Cargo cargo4 = cargoList4.GetCargo(shipUnderConstruction2.Components[num4], empire4);
                        if (cargo4 != null && cargo4.Amount > 0)
                        {
                            cargo4.Amount--;
                            cargo4.Reserved--;
                            if (cargo4.Amount <= 0 && cargo4.Reserved <= 0)
                            {
                                cargoList4.Remove(cargo4);
                            }
                            shipUnderConstruction2.Components[num4].Status = ComponentStatus.Normal;
                        }
                    }
                }
                if (num4 == int.MinValue)
                {
                    if (double.IsNaN(shipUnderConstruction2.CurrentFuel))
                    {
                        shipUnderConstruction2.CurrentFuel = 0.0;
                    }
                    if (double.IsNaN(shipUnderConstruction2.CurrentEnergy))
                    {
                        shipUnderConstruction2.CurrentEnergy = 0.0;
                    }
                    shipUnderConstruction2.ReDefine();
                    shipUnderConstruction2.DateBuilt = _Galaxy.CurrentStarDate;
                    shipUnderConstruction2.DateRetrofit = _Galaxy.CurrentStarDate;
                    shipUnderConstruction2.BuiltAt = null;
                    string arg2 = string.Empty;
                    if (_ParentBuiltObject != null)
                    {
                        arg2 = ((shipUnderConstruction2.ParentBuiltObject == null) ? _ParentBuiltObject.Name : shipUnderConstruction2.ParentBuiltObject.Name);
                    }
                    else if (_ParentHabitat != null)
                    {
                        arg2 = _ParentHabitat.Name;
                    }
                    if (shipUnderConstruction2.Empire == null)
                    {
                        shipUnderConstruction2.ScanForNewOwner(_ParentBuiltObject);
                    }
                    string empty2 = string.Empty;
                    empty2 = ((shipUnderConstruction2.Role != BuiltObjectRole.Base) ? (empty2 + string.Format(TextResolver.GetText("The SHIPTYPE NAME has been completed at LOCATION"), Galaxy.ResolveDescription(shipUnderConstruction2.SubRole), shipUnderConstruction2.Name, arg2)) : (empty2 + string.Format(TextResolver.GetText("The SHIPTYPE NAME has been completed at LOCATION"), "base", shipUnderConstruction2.Name, arg2)));
                    if (empire4 != null && empire4.Counters != null)
                    {
                        empire4.Counters.ProcessBuiltObjectConstruction(shipUnderConstruction2);
                    }
                    if (shipUnderConstruction2.IsPlanetDestroyer && shipUnderConstruction2.TopSpeed > 0)
                    {
                        shipUnderConstruction2.CurrentFuel = shipUnderConstruction2.FuelCapacity;
                        short matchingGameEventIdPlanetDestroyerConstructionCompleted = _Galaxy.GetMatchingGameEventIdPlanetDestroyerConstructionCompleted(empire4);
                        _Galaxy.CheckTriggerEvent(matchingGameEventIdPlanetDestroyerConstructionCompleted, empire4, EventTriggerType.PlanetDestroyerConstructionCompleted, shipUnderConstruction2);
                    }
                    CharacterEventType eventType = CharacterEventType.BuildCivilianShip;
                    switch (shipUnderConstruction2.SubRole)
                    {
                        case BuiltObjectSubRole.Escort:
                        case BuiltObjectSubRole.Frigate:
                        case BuiltObjectSubRole.Destroyer:
                        case BuiltObjectSubRole.Cruiser:
                        case BuiltObjectSubRole.CapitalShip:
                        case BuiltObjectSubRole.TroopTransport:
                        case BuiltObjectSubRole.Carrier:
                        case BuiltObjectSubRole.ResupplyShip:
                            eventType = CharacterEventType.BuildMilitaryShip;
                            break;
                        case BuiltObjectSubRole.ExplorationShip:
                        case BuiltObjectSubRole.SmallFreighter:
                        case BuiltObjectSubRole.MediumFreighter:
                        case BuiltObjectSubRole.LargeFreighter:
                        case BuiltObjectSubRole.PassengerShip:
                        case BuiltObjectSubRole.ConstructionShip:
                        case BuiltObjectSubRole.GasMiningShip:
                        case BuiltObjectSubRole.MiningShip:
                            eventType = CharacterEventType.BuildCivilianShip;
                            break;
                        case BuiltObjectSubRole.ColonyShip:
                            eventType = CharacterEventType.BuildColonyShip;
                            break;
                        case BuiltObjectSubRole.DefensiveBase:
                            eventType = CharacterEventType.BuildMilitaryBase;
                            break;
                        case BuiltObjectSubRole.GasMiningStation:
                        case BuiltObjectSubRole.MiningStation:
                            eventType = CharacterEventType.BuildMiningStation;
                            break;
                        case BuiltObjectSubRole.GenericBase:
                        case BuiltObjectSubRole.MonitoringStation:
                            eventType = CharacterEventType.BuildOtherBase;
                            break;
                        case BuiltObjectSubRole.EnergyResearchStation:
                            eventType = CharacterEventType.BuildResearchStationEnergy;
                            break;
                        case BuiltObjectSubRole.HighTechResearchStation:
                            eventType = CharacterEventType.BuildResearchStationHighTech;
                            break;
                        case BuiltObjectSubRole.WeaponsResearchStation:
                            eventType = CharacterEventType.BuildResearchStationWeapons;
                            break;
                        case BuiltObjectSubRole.ResortBase:
                            eventType = CharacterEventType.BuildResortBase;
                            break;
                        case BuiltObjectSubRole.SmallSpacePort:
                        case BuiltObjectSubRole.MediumSpacePort:
                        case BuiltObjectSubRole.LargeSpacePort:
                            eventType = CharacterEventType.BuildSpaceport;
                            break;
                    }
                    CharacterList characterList = new CharacterList();
                    if (_ParentBuiltObject != null)
                    {
                        if (_ParentBuiltObject.Characters != null)
                        {
                            characterList.AddRange(_ParentBuiltObject.Characters);
                        }
                        if (_ParentBuiltObject.ParentHabitat != null && _ParentBuiltObject.ParentHabitat.Characters != null)
                        {
                            characterList.AddRange(_ParentBuiltObject.ParentHabitat.Characters);
                        }
                    }
                    else if (_ParentHabitat != null && _ParentHabitat.Characters != null)
                    {
                        characterList.AddRange(_ParentHabitat.Characters);
                    }
                    _Galaxy.DoCharacterEvent(eventType, shipUnderConstruction2, characterList, includeLeader: true, shipUnderConstruction2.Empire);
                    if (empire4 != null)
                    {
                        if (_ParentBuiltObject != null)
                        {
                            if (_ParentBuiltObject.ParentHabitat != null)
                            {
                                _Galaxy.ChanceNewFleetAdmiralFromConstruction(shipUnderConstruction2, empire4, _ParentBuiltObject.ParentHabitat);
                            }
                            else
                            {
                                _Galaxy.ChanceNewFleetAdmiralFromConstruction(shipUnderConstruction2, empire4, _ParentBuiltObject);
                            }
                        }
                        else if (_ParentHabitat != null)
                        {
                            _Galaxy.ChanceNewFleetAdmiralFromConstruction(shipUnderConstruction2, empire4, _ParentHabitat);
                        }
                    }
                    if (shipUnderConstruction2.Empire != null)
                    {
                        switch (shipUnderConstruction2.SubRole)
                        {
                            case BuiltObjectSubRole.GasMiningStation:
                            case BuiltObjectSubRole.MiningStation:
                                shipUnderConstruction2.Empire.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.BuildFirstMiningStation, shipUnderConstruction2);
                                break;
                            case BuiltObjectSubRole.EnergyResearchStation:
                            case BuiltObjectSubRole.WeaponsResearchStation:
                            case BuiltObjectSubRole.HighTechResearchStation:
                                shipUnderConstruction2.Empire.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.BuildFirstResearchStation, shipUnderConstruction2);
                                break;
                            case BuiltObjectSubRole.SmallSpacePort:
                            case BuiltObjectSubRole.MediumSpacePort:
                            case BuiltObjectSubRole.LargeSpacePort:
                                shipUnderConstruction2.Empire.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.BuildFirstSpaceport, shipUnderConstruction2);
                                break;
                            default:
                                if (shipUnderConstruction2.Role != BuiltObjectRole.Base)
                                {
                                    if (shipUnderConstruction2.Role == BuiltObjectRole.Military)
                                    {
                                        shipUnderConstruction2.Empire.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.BuildFirstMilitaryShip, shipUnderConstruction2);
                                    }
                                    shipUnderConstruction2.Empire.CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.BuildFirstShip, shipUnderConstruction2);
                                }
                                break;
                        }
                    }
                    int num14 = 0;
                    if (_ParentBuiltObject != null)
                    {
                        if (shipUnderConstruction2.ParentBuiltObject != null)
                        {
                            shipUnderConstruction2.ParentBuiltObject = null;
                            shipUnderConstruction2.ParentOffsetX = -2000000001.0;
                            shipUnderConstruction2.ParentOffsetY = -2000000001.0;
                        }
                        if ((_ParentBuiltObject.Role == BuiltObjectRole.Base || shipUnderConstruction2.Role != BuiltObjectRole.Base) && (_ParentBuiltObject != shipUnderConstruction2 || shipUnderConstruction2.Role != BuiltObjectRole.Base))
                        {
                            shipUnderConstruction2.ParentHabitat = null;
                            shipUnderConstruction2.ParentOffsetX = -2000000001.0;
                            shipUnderConstruction2.ParentOffsetY = -2000000001.0;
                        }
                        if (_ParentBuiltObject.Role != BuiltObjectRole.Base && shipUnderConstruction2.Role == BuiltObjectRole.Base)
                        {
                            if (_ParentBuiltObject.ParentHabitat != null)
                            {
                                shipUnderConstruction2.ParentHabitat = _ParentBuiltObject.ParentHabitat;
                                bool flag = false;
                                if (_ParentBuiltObject.Mission != null)
                                {
                                    Command command = _ParentBuiltObject.Mission.FastPeekCurrentCommand();
                                    if (command != null && command.Action == CommandAction.Build && command.TargetHabitat != null)
                                    {
                                        Habitat targetHabitat = command.TargetHabitat;
                                        if (targetHabitat == _ParentBuiltObject.ParentHabitat)
                                        {
                                            shipUnderConstruction2.ParentOffsetX = command.TargetRelativeXpos;
                                            shipUnderConstruction2.ParentOffsetY = command.TargetRelativeYpos;
                                            flag = true;
                                        }
                                    }
                                }
                                if (!flag)
                                {
                                    shipUnderConstruction2.ParentOffsetX = _ParentBuiltObject.Xpos - _ParentBuiltObject.ParentHabitat.Xpos;
                                    shipUnderConstruction2.ParentOffsetY = _ParentBuiltObject.Ypos - _ParentBuiltObject.ParentHabitat.Ypos;
                                }
                            }
                        }
                        else
                        {
                            shipUnderConstruction2.ParentBuiltObject = null;
                            shipUnderConstruction2.ParentOffsetX = -2000000001.0;
                            shipUnderConstruction2.ParentOffsetY = -2000000001.0;
                            if (_ParentBuiltObject != null && _ParentBuiltObject.ParentBuiltObject == shipUnderConstruction2)
                            {
                                _ParentBuiltObject.ParentBuiltObject = null;
                                _ParentBuiltObject.ParentOffsetX = -2000000001.0;
                                _ParentBuiltObject.ParentOffsetY = -2000000001.0;
                            }
                        }
                        if (shipUnderConstruction2.FuelType != null)
                        {
                            int num15 = -1;
                            if (_ParentBuiltObject.Cargo != null)
                            {
                                num15 = _ParentBuiltObject.Cargo.IndexOf(shipUnderConstruction2.FuelType, shipUnderConstruction2.ActualEmpire);
                            }
                            if (num15 >= 0)
                            {
                                num14 = ((_ParentBuiltObject.Cargo[num15].Available < shipUnderConstruction2.FuelCapacity - (int)shipUnderConstruction2.CurrentFuel) ? _ParentBuiltObject.Cargo[num15].Available : (shipUnderConstruction2.FuelCapacity - (int)shipUnderConstruction2.CurrentFuel));
                                _ParentBuiltObject.Cargo[num15].Amount -= num14;
                            }
                        }
                    }
                    else if (_ParentHabitat != null)
                    {
                        shipUnderConstruction2.ParentHabitat = _ParentHabitat;
                        if (shipUnderConstruction2.FuelType != null)
                        {
                            int num16 = -1;
                            if (_ParentHabitat.Cargo != null)
                            {
                                num16 = _ParentHabitat.Cargo.IndexOf(shipUnderConstruction2.FuelType, shipUnderConstruction2.Empire);
                            }
                            if (num16 >= 0)
                            {
                                num14 = ((_ParentHabitat.Cargo[num16].Available < shipUnderConstruction2.FuelCapacity - (int)shipUnderConstruction2.CurrentFuel) ? _ParentHabitat.Cargo[num16].Available : (shipUnderConstruction2.FuelCapacity - (int)shipUnderConstruction2.CurrentFuel));
                                _ParentHabitat.Cargo[num16].Amount -= num14;
                            }
                        }
                    }
                    if (shipUnderConstruction2.ParentHabitat != null)
                    {
                        _Galaxy.CheckTriggerEvent(shipUnderConstruction2.ParentHabitat.GameEventId, Empire, EventTriggerType.Build, shipUnderConstruction2);
                    }
                    shipUnderConstruction2.CurrentFuel += num14;
                    shipUnderConstruction2.CurrentEnergy = Math.Max(shipUnderConstruction2.CurrentEnergy, 0.0);
                    if (_ParentBuiltObject != null && _ParentBuiltObject.SubRole == BuiltObjectSubRole.ConstructionShip && _ParentBuiltObject.Cargo != null && shipUnderConstruction2.Role == BuiltObjectRole.Base && shipUnderConstruction2.Cargo != null && (shipUnderConstruction2.ParentHabitat == null || shipUnderConstruction2.ParentHabitat.Empire != shipUnderConstruction2.ActualEmpire))
                    {
                        BaconConstructionQueue.MoveCargoFromBuilderToBuiltBase(_ParentBuiltObject, shipUnderConstruction2);
                    }
                    if (shipUnderConstruction2.TroopCapacity > 0 && shipUnderConstruction2.Empire != null && shipUnderConstruction2.Empire.Policy != null)
                    {
                        shipUnderConstruction2.SetTroopLoadoutsFromPolicy(shipUnderConstruction2.Empire.Policy);
                    }
                    constructionYard.ShipUnderConstruction.BuiltAt = null;
                    constructionYard.ShipUnderConstruction = null;
                    num3 = 0;
                    shipUnderConstruction2.FirstExecutionOfCommand = true;
                    completedBuiltObjects.Add(shipUnderConstruction2);
                    shipUnderConstruction2.RepairForNextMission = false;
                    shipUnderConstruction2.RefuelForNextMission = false;
                    shipUnderConstruction2.RetireForNextMission = false;
                    shipUnderConstruction2.ReDefine();
                    constructionYard.IncrementalProgress = 0f;
                    constructionYard.ShipUnderConstruction = null;
                    if (shipUnderConstruction2.Role != BuiltObjectRole.Base && (_ParentBuiltObject == null || _ParentBuiltObject.SubRole != BuiltObjectSubRole.ConstructionShip) && !_Galaxy.AssignFleetWaypointMission(shipUnderConstruction2, allowMissionOverride: true, null) && shipUnderConstruction2.TopSpeed > 0)
                    {
                        if (shipUnderConstruction2.Mission == null || shipUnderConstruction2.Mission.Type == BuiltObjectMissionType.Undefined)
                        {
                            _Galaxy.SelectRelativeParkingPoint(out var x2, out var y2);
                            if (_ParentHabitat != null)
                            {
                                shipUnderConstruction2.AssignMission(BuiltObjectMissionType.Move, _ParentHabitat, null, x2, y2, BuiltObjectMissionPriority.Low);
                            }
                            else if (_ParentBuiltObject != null)
                            {
                                shipUnderConstruction2.AssignMission(BuiltObjectMissionType.Move, _ParentBuiltObject, null, x2, y2, BuiltObjectMissionPriority.Low);
                            }
                        }
                        else if (shipUnderConstruction2.SubRole == BuiltObjectSubRole.ColonyShip && shipUnderConstruction2.Mission != null && shipUnderConstruction2.Mission.Type == BuiltObjectMissionType.Colonize && shipUnderConstruction2.Mission.TargetHabitat != null)
                        {
                            int newPopulationAmount = 0;
                            if (!shipUnderConstruction2.Empire.CanBuiltObjectColonizeHabitat(shipUnderConstruction2, shipUnderConstruction2.Mission.TargetHabitat, out newPopulationAmount))
                            {
                                shipUnderConstruction2.ClearPreviousMissionRequirements();
                                _Galaxy.SelectRelativeParkingPoint(out var x3, out var y3);
                                if (_ParentHabitat != null)
                                {
                                    shipUnderConstruction2.AssignMission(BuiltObjectMissionType.Move, _ParentHabitat, null, x3, y3, BuiltObjectMissionPriority.Low);
                                }
                                else if (_ParentBuiltObject != null)
                                {
                                    shipUnderConstruction2.AssignMission(BuiltObjectMissionType.Move, _ParentBuiltObject, null, x3, y3, BuiltObjectMissionPriority.Low);
                                }
                            }
                        }
                    }
                    if (shipUnderConstruction2.ActualEmpire != null && shipUnderConstruction2.ActualEmpire.BuiltObjects != null && shipUnderConstruction2.ActualEmpire.BuiltObjects.Contains(shipUnderConstruction2))
                    {
                        shipUnderConstruction2.ActualEmpire.SendMessageToEmpire(shipUnderConstruction2.ActualEmpire, EmpireMessageType.ShipBaseCompleted, shipUnderConstruction2, empty2);
                    }
                    ProcessWaitQueue();
                    if (constructionYard.ShipUnderConstruction == null)
                    {
                        break;
                    }
                    shipUnderConstruction2 = constructionYard.ShipUnderConstruction;
                }
                num3--;
            }
            if (shipUnderConstruction2.RetrofitDesign == null)
            {
                shipUnderConstruction2.ReDefine();
            }
        }

        private int IdentifyComponentToBuild(CargoList cargo, Empire empire, ComponentList components)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (cargo.GetExists(components[i]))
                {
                    int num = cargo.IndexOf(components[i], empire);
                    if (num >= 0 && cargo[num].Amount > 0)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private int IdentifyComponentToBuild(CargoList cargo, Empire empire, BuiltObject builtObject)
        {
            int num = 0;
            int num2 = -1;
            for (int i = 0; i < builtObject.Components.Count; i++)
            {
                switch (builtObject.Components[i].Status)
                {
                    case ComponentStatus.Damaged:
                        return i;
                    case ComponentStatus.Unbuilt:
                        num++;
                        if (builtObject.Components[i].ComponentID == num2)
                        {
                            break;
                        }
                        num2 = builtObject.Components[i].ComponentID;
                        if (cargo.GetExists(builtObject.Components[i]))
                        {
                            int num3 = cargo.IndexOf(builtObject.Components[i], empire);
                            if (num3 >= 0)
                            {
                                return i;
                            }
                        }
                        break;
                }
            }
            if (num == 0)
            {
                return int.MinValue;
            }
            return -1;
        }

        private int IdentifyComponentToRepair(BuiltObject builtObject)
        {
            for (int i = 0; i < builtObject.Components.Count; i++)
            {
                ComponentStatus status = builtObject.Components[i].Status;
                if (status == ComponentStatus.Damaged)
                {
                    return i;
                }
            }
            return -1;
        }

        public void ResetProcessTime(DateTime time)
        {
            _LastProcessed = time;
        }

        public BuiltObjectList DoConstruction(Galaxy galaxy, DateTime tempNow)
        {
            if (_LockObject == null)
            {
                _LockObject = new object();
            }
            lock (_LockObject)
            {
                BuiltObjectList builtObjectList = new BuiltObjectList();
                TimeSpan timePassed = tempNow.Subtract(_LastProcessed);
                ProcessWaitQueue();
                int num = Galaxy.Rnd.Next(0, _ConstructionYards.Count);
                for (int i = num; i < _ConstructionYards.Count; i++)
                {
                    ConstructionYard constructionYard = _ConstructionYards[i];
                    ProcessSingleConstructionYard(constructionYard, timePassed, galaxy, builtObjectList);
                }
                for (int j = 0; j < num; j++)
                {
                    ConstructionYard constructionYard2 = _ConstructionYards[j];
                    ProcessSingleConstructionYard(constructionYard2, timePassed, galaxy, builtObjectList);
                }
                ProcessWaitQueue();
                _LastProcessed = tempNow;
                return builtObjectList;
            }
        }

        public void Clear()
        {
            for (int i = 0; i < _ConstructionWaitQueue.Count; i++)
            {
                BuiltObject builtObject = _ConstructionWaitQueue[i];
                builtObject.BuiltAt = null;
            }
            _ConstructionWaitQueue.Clear();
            foreach (ConstructionYard constructionYard in _ConstructionYards)
            {
                if (constructionYard.ShipUnderConstruction != null)
                {
                    constructionYard.ShipUnderConstruction.BuiltAt = null;
                    constructionYard.ShipUnderConstruction.FirstExecutionOfCommand = true;
                    constructionYard.ShipUnderConstruction.ReDefine();
                    constructionYard.ShipUnderConstruction = null;
                }
                constructionYard.IncrementalProgress = 0f;
                constructionYard.RetrofitComponentsToBeBuilt = null;
                constructionYard.RetrofitComponentsToBeScrapped = null;
                constructionYard.ShipUnderConstruction = null;
            }
        }

        public bool RemoveBuiltObject(BuiltObject builtObject)
        {
            if (_ConstructionWaitQueue.Contains(builtObject))
            {
                builtObject.BuiltAt = null;
                _ConstructionWaitQueue.Remove(builtObject);
                return true;
            }
            int num = _ConstructionYards.IndexOf(builtObject);
            if (num >= 0 && _ConstructionYards[num].ShipUnderConstruction != null)
            {
                _ConstructionYards[num].ShipUnderConstruction.BuiltAt = null;
                _ConstructionYards[num].ShipUnderConstruction.FirstExecutionOfCommand = true;
                _ConstructionYards[num].ShipUnderConstruction.ReDefine();
                _ConstructionYards[num].ShipUnderConstruction = null;
                return true;
            }
            return false;
        }

        public bool AddBuiltObjectToRetrofit(BuiltObject builtObject, Design design)
        {
            if (_ParentHabitat != null)
            {
                if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip || builtObject.SubRole == BuiltObjectSubRole.ConstructionShip || builtObject.SubRole == BuiltObjectSubRole.ResupplyShip || builtObject.Role == BuiltObjectRole.Base)
                {
                    builtObject.RetrofitDesign = design;
                    _ConstructionWaitQueue.Add(builtObject);
                    builtObject.RetrofitForNextMission = false;
                    return true;
                }
                if (builtObject.Design.TopSpeed > 0 && builtObject.Role != BuiltObjectRole.Colony && builtObject.Role != BuiltObjectRole.Build)
                {
                    return false;
                }
            }
            builtObject.RetrofitDesign = design;
            _ConstructionWaitQueue.Add(builtObject);
            builtObject.RetrofitForNextMission = false;
            return true;
        }

        public bool AddBuiltObjectToScrap(BuiltObject builtObject)
        {
            if (_ParentHabitat != null)
            {
                if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip || builtObject.SubRole == BuiltObjectSubRole.ConstructionShip || builtObject.SubRole == BuiltObjectSubRole.ResupplyShip || builtObject.Role == BuiltObjectRole.Base)
                {
                    builtObject.Scrap = true;
                    _ConstructionWaitQueue.Add(builtObject);
                    builtObject.RetireForNextMission = false;
                    return true;
                }
                if (builtObject.Design.TopSpeed > 0 && builtObject.Role != BuiltObjectRole.Colony && builtObject.Role != BuiltObjectRole.Build)
                {
                    return false;
                }
            }
            builtObject.Scrap = true;
            _ConstructionWaitQueue.Add(builtObject);
            builtObject.RetireForNextMission = false;
            return true;
        }

        public bool AddBuiltObjectToRepair(BuiltObject builtObject)
        {
            if (_ParentHabitat != null)
            {
                if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip || builtObject.SubRole == BuiltObjectSubRole.ConstructionShip || builtObject.SubRole == BuiltObjectSubRole.ResupplyShip || builtObject.Role == BuiltObjectRole.Base)
                {
                    _ConstructionWaitQueue.Add(builtObject);
                    return true;
                }
                if (builtObject.Design.TopSpeed > 0 && builtObject.Role != BuiltObjectRole.Colony && builtObject.Role != BuiltObjectRole.Build)
                {
                    return false;
                }
            }
            _ConstructionWaitQueue.Add(builtObject);
            return true;
        }

        public bool AddBuiltObjectToConstruct(BuiltObject builtObject)
        {
            double num = 0.0;
            double num2 = 0.0;
            float heading = 0f;
            if (_ParentHabitat != null)
            {
                if (builtObject.Role != BuiltObjectRole.Base)
                {
                    num = _ParentHabitat.Xpos;
                    num2 = _ParentHabitat.Ypos;
                    heading = 0f;
                }
                else
                {
                    num = builtObject.Xpos;
                    num2 = builtObject.Ypos;
                    heading = builtObject.Heading;
                }
            }
            else if (_ParentBuiltObject != null)
            {
                num = _ParentBuiltObject.Xpos;
                num2 = _ParentBuiltObject.Ypos;
                heading = _ParentBuiltObject.Heading;
            }
            if (_ParentHabitat != null)
            {
                if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip || builtObject.SubRole == BuiltObjectSubRole.ConstructionShip || builtObject.SubRole == BuiltObjectSubRole.ResupplyShip || builtObject.Role == BuiltObjectRole.Base)
                {
                    Habitat habitat = _Galaxy.FastFindNearestSystem(num, num2);
                    if (habitat != null)
                    {
                        double num3 = _Galaxy.CalculateDistance(num, num2, habitat.Xpos, habitat.Ypos);
                        if (num3 < (double)Galaxy.MaxSolarSystemSize + 500.0)
                        {
                            builtObject.NearestSystemStar = habitat;
                        }
                    }
                    if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip)
                    {
                        builtObject.NativeRace = _ParentHabitat.Population.DominantRace;
                        for (int i = 0; i < _ParentHabitat.Population.Count; i++)
                        {
                            if (_ParentHabitat.Population[i].Race == _ParentHabitat.Population.DominantRace)
                            {
                                int num4 = _ParentHabitat.Empire.CalculateColonizationPopulation(builtObject.Design);
                                _ParentHabitat.Population[i].Amount -= num4;
                                _ParentHabitat.Population[i].Amount = Math.Max(1000000L, _ParentHabitat.Population[i].Amount);
                                _ParentHabitat.Population.RecalculateTotalAmount();
                                break;
                            }
                        }
                    }
                    _ConstructionWaitQueue.Add(builtObject);
                    return true;
                }
                if (builtObject.Design.TopSpeed > 0 && builtObject.Role != BuiltObjectRole.Colony && builtObject.Role != BuiltObjectRole.Build)
                {
                    return false;
                }
            }
            if (Empire.CanBuildBuiltObject(builtObject))
            {
                Habitat habitat2 = _Galaxy.FastFindNearestSystem(num, num2);
                if (habitat2 != null)
                {
                    double num5 = _Galaxy.CalculateDistance(num, num2, habitat2.Xpos, habitat2.Ypos);
                    if (num5 <= (double)Galaxy.MaxSolarSystemSize + 500.0)
                    {
                        builtObject.NearestSystemStar = habitat2;
                    }
                }
                _ConstructionWaitQueue.Add(builtObject);
                builtObject.Heading = heading;
                builtObject.TargetHeading = builtObject.Heading;
                return true;
            }
            return false;
        }
    }
}
