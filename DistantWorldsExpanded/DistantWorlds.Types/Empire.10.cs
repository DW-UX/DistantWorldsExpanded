using BaconDistantWorlds;

using DistantWorlds.Types.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace DistantWorlds.Types
{
    public partial class Empire
    {
        private void MarkEmpireAsRecentSpy(Empire spy, Empire target)
        {
            if (target != null && target != _Galaxy.IndependentEmpire && spy != null && spy != _Galaxy.IndependentEmpire && !target.RecentSpyingEmpires.Contains(spy))
            {
                target.RecentSpyingEmpires.Add(spy);
            }
        }


        public ResourceList DetermineFuelRequiredForFleet(ShipGroup fleet)
        {
            int fleetFuelCapacity = 0;
            return DetermineFuelRequiredForFleet(fleet, setFuelLevelToZero: false, out fleetFuelCapacity);
        }

        public ResourceList DetermineFuelRequiredForFleet(ShipGroup fleet, bool setFuelLevelToZero)
        {
            int fleetFuelCapacity = 0;
            return DetermineFuelRequiredForFleet(fleet, setFuelLevelToZero, out fleetFuelCapacity);
        }

        public ResourceList DetermineFuelRequiredForFleet(ShipGroup fleet, out int fleetFuelCapacity)
        {
            return DetermineFuelRequiredForFleet(fleet, setFuelLevelToZero: false, out fleetFuelCapacity);
        }


        public void RecalculateColonyTaxRevenues()
        {
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat != null && habitat.Empire == this)
                {
                    habitat.RecalculateAnnualTaxRevenue();
                }
            }
        }

        private void ReviewGovernmentEffects(double timePassed)
        {
            if (GovernmentAttributes != null && GovernmentAttributes.SpecialFunctionCode == 1)
            {
                double num = timePassed / (double)Galaxy.RealSecondsInGalacticYear * 0.075;
                double val = _EconomyEfficiency - num;
                _EconomyEfficiency = Math.Max(0.35, Math.Min(1.0, val));
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat = Colonies[i];
                    habitat.RecalculateAnnualTaxRevenue();
                }
            }
            else if (_EconomyEfficiency != 1.0)
            {
                double val2 = timePassed / (double)Galaxy.RealSecondsInGalacticYear * 0.25;
                val2 = Math.Min(val2, Math.Abs(_EconomyEfficiency - 1.0));
                if (_EconomyEfficiency > 1.0)
                {
                    val2 *= -1.0;
                }
                double val3 = _EconomyEfficiency + val2;
                _EconomyEfficiency = Math.Max(0.5, Math.Min(2.0, val3));
                for (int j = 0; j < Colonies.Count; j++)
                {
                    Habitat habitat2 = Colonies[j];
                    habitat2.RecalculateAnnualTaxRevenue();
                }
            }
        }

        private void CheckChangeGovernment()
        {
            if (this == _Galaxy.PlayerEmpire)
            {
                return;
            }
            GovernmentAttributesList bySpecialFunctionCode = _Galaxy.Governments.GetBySpecialFunctionCode(1);
            List<int> list = new List<int>();
            for (int i = 0; i < bySpecialFunctionCode.Count; i++)
            {
                list.Add(bySpecialFunctionCode[i].GovernmentId);
            }
            int num = -1;
            for (int j = 0; j < list.Count; j++)
            {
                if (AllowableGovernmentTypes.Contains(list[j]))
                {
                    num = list[j];
                    break;
                }
            }
            if (_EconomyEfficiency < 0.9)
            {
                if (GovernmentAttributes == null || GovernmentAttributes.SpecialFunctionCode != 1)
                {
                    return;
                }
                double num2 = 1.0;
                if (DominantRace != null)
                {
                    num2 = (double)DominantRace.IntelligenceLevel / 100.0;
                    num2 *= num2;
                }
                double num3 = num2 * 0.8;
                if (!(_EconomyEfficiency < num3))
                {
                    return;
                }
                int num4 = -1;
                GovernmentAttributesList governmentAttributesList = DetermineMostSuitableGovermentTypes(DominantRace, AllowableGovernmentTypes);
                for (int k = 0; k < governmentAttributesList.Count; k++)
                {
                    if (!list.Contains(num4) && num4 >= 0)
                    {
                        break;
                    }
                    num4 = governmentAttributesList[k].GovernmentId;
                }
                double val = 1.0 / _EconomyEfficiency;
                val = Math.Max(1.0, Math.Min(3.0, val));
                HaveRevolution(DominantRace, num4, val);
                ReviewTaxes();
            }
            else
            {
                if (GovernmentAttributes == null || GovernmentAttributes.SpecialFunctionCode == 1 || num < 0 || !(_EconomyEfficiency >= 1.0))
                {
                    return;
                }
                int militaryStrength = 0;
                EmpireList empireList = DetermineEmpiresAtWarWith(out militaryStrength);
                if (empireList.Count > 0 && militaryStrength > MilitaryPotency)
                {
                    double num5 = CalculateAnnualCashflow();
                    if (num5 < 5000.0 && StateMoney < 50000.0)
                    {
                        HaveRevolution(DominantRace, num, 1.0);
                    }
                }
            }
        }

        public void ReviewTaxes()
        {
            bool atWar = CheckAtWar();
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat != null && habitat.Empire == this)
                {
                    if (PirateEmpireBaseHabitat != null && habitat.CheckColonyRevenueFromPirateControl(this))
                    {
                        habitat.TaxRate = 0f;
                    }
                    else
                    {
                        SetColonyTaxRate(habitat, atWar);
                    }
                }
            }
        }

        public ForceStructureProjectionList CurrentPrivateForceStructure(out double annualSupportCosts)
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            ForceStructureProjectionList forceStructureProjectionList = new ForceStructureProjectionList();
            annualSupportCosts = 0.0;
            for (int i = 0; i < PrivateBuiltObjects.Count; i++)
            {
                BuiltObject builtObject = PrivateBuiltObjects[i];
                ForceStructureProjection forceStructureProjection = forceStructureProjectionList.GetBySubRole(builtObject.SubRole);
                if (forceStructureProjection == null)
                {
                    forceStructureProjection = new ForceStructureProjection(builtObject.SubRole, 0, currentStarDate);
                    forceStructureProjectionList.Add(forceStructureProjection);
                }
                forceStructureProjection.Amount++;
                annualSupportCosts += builtObject.AnnualSupportCost;
            }
            double num = annualSupportCosts * _ShipMaintenanceSavings;
            annualSupportCosts -= num;
            return forceStructureProjectionList;
        }

        public ForceStructureProjectionList CurrentStateForceStructure(out double annualSupportCosts)
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            ForceStructureProjectionList forceStructureProjectionList = new ForceStructureProjectionList();
            annualSupportCosts = 0.0;
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = BuiltObjects[i];
                ForceStructureProjection forceStructureProjection = forceStructureProjectionList.GetBySubRole(builtObject.SubRole);
                if (forceStructureProjection == null)
                {
                    forceStructureProjection = new ForceStructureProjection(builtObject.SubRole, 0, currentStarDate);
                    forceStructureProjectionList.Add(forceStructureProjection);
                }
                forceStructureProjection.Amount++;
                annualSupportCosts += builtObject.AnnualSupportCost;
            }
            double num = annualSupportCosts * _ShipMaintenanceSavings;
            annualSupportCosts -= num;
            return forceStructureProjectionList;
        }

        private HabitatList DetermineLargestColonyInEachSystem()
        {
            HabitatList habitatList = new HabitatList();
            HabitatList habitatList2 = DetermineEmpireSystems(this);
            for (int i = 0; i < habitatList2.Count; i++)
            {
                Habitat habitat = habitatList2[i];
                HabitatList habitatList3 = new HabitatList();
                for (int j = 0; j < Colonies.Count; j++)
                {
                    Habitat habitat2 = Colonies[j];
                    if (habitat2.SystemIndex == habitat.SystemIndex)
                    {
                        habitatList3.Add(habitat2);
                    }
                }
                if (habitatList3.Count <= 0)
                {
                    continue;
                }
                Habitat habitat3 = habitatList3[0];
                foreach (Habitat item in habitatList3)
                {
                    if (item.Population.TotalAmount > habitat3.Population.TotalAmount)
                    {
                        habitat3 = item;
                    }
                }
                habitatList.Add(habitat3);
            }
            return habitatList;
        }

        public bool CanBuildDesignTech(Design design)
        {
            ComponentList distinctComponentList = design.Components.GetDistinctComponentList();
            for (int i = 0; i < distinctComponentList.Count; i++)
            {
                Component component = distinctComponentList[i];
                if (!Research.CheckComponentResearched(component))
                {
                    return false;
                }
            }
            if (design.SubRole == BuiltObjectSubRole.Carrier)
            {
                return _CanBuildCarriers;
            }
            if (design.SubRole == BuiltObjectSubRole.ResupplyShip)
            {
                return _CanBuildResupplyShips;
            }
            return true;
        }

        public bool CheckDesignWithinConstructionSize(Design design)
        {
            return CheckDesignWithinConstructionSize(design, null);
        }

        public bool CheckDesignWithinConstructionSize(Design design, Habitat colony)
        {
            int num = 0;
            if (design.Role == BuiltObjectRole.Base)
            {
                num = ((design.SubRole != BuiltObjectSubRole.GasMiningStation && design.SubRole != BuiltObjectSubRole.GenericBase && design.SubRole != BuiltObjectSubRole.MiningStation && design.SubRole != BuiltObjectSubRole.EnergyResearchStation && design.SubRole != BuiltObjectSubRole.WeaponsResearchStation && design.SubRole != BuiltObjectSubRole.HighTechResearchStation && design.SubRole != BuiltObjectSubRole.MonitoringStation && design.SubRole != BuiltObjectSubRole.DefensiveBase && design.SubRole != BuiltObjectSubRole.ResortBase) ? int.MaxValue : ((colony == null || colony.Empire != this || colony.Population == null || colony.Population.TotalAmount <= 0) ? MaximumConstructionSizeBase(design.SubRole) : int.MaxValue));
            }
            else if (design.IsPlanetDestroyer)
            {
                num = MaximumConstructionSizeBase();
            }
            else
            {
                num = MaximumConstructionSize(design.SubRole);
                if (design.SubRole == BuiltObjectSubRole.ColonyShip || design.SubRole == BuiltObjectSubRole.ConstructionShip || design.SubRole == BuiltObjectSubRole.ResupplyShip)
                {
                    num = MaximumConstructionSizeBase(design.SubRole);
                }
            }
            if (design.Size > num)
            {
                return false;
            }
            return true;
        }

        public bool CheckDesignInUseForConstructionOrRetrofits(Design design)
        {
            if (design != null)
            {
                if (ConstructionShips != null)
                {
                    for (int i = 0; i < ConstructionShips.Count; i++)
                    {
                        BuiltObject builtObject = ConstructionShips[i];
                        if (builtObject != null && builtObject.Mission != null && builtObject.Mission.Design != null && builtObject.Mission.Design == design)
                        {
                            return true;
                        }
                    }
                }
                if (ConstructionYards != null)
                {
                    for (int j = 0; j < ConstructionYards.Count; j++)
                    {
                        BuiltObject builtObject2 = ConstructionYards[j];
                        if (builtObject2 == null || builtObject2.ConstructionQueue == null)
                        {
                            continue;
                        }
                        if (builtObject2.ConstructionQueue.ConstructionWaitQueue != null)
                        {
                            for (int k = 0; k < builtObject2.ConstructionQueue.ConstructionWaitQueue.Count; k++)
                            {
                                if (builtObject2.ConstructionQueue.ConstructionWaitQueue[k] != null)
                                {
                                    if (builtObject2.ConstructionQueue.ConstructionWaitQueue[k].Design != null && builtObject2.ConstructionQueue.ConstructionWaitQueue[k].Design == design)
                                    {
                                        return true;
                                    }
                                    if (builtObject2.ConstructionQueue.ConstructionWaitQueue[k].RetrofitDesign != null && builtObject2.ConstructionQueue.ConstructionWaitQueue[k].RetrofitDesign == design)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                        if (builtObject2.ConstructionQueue.ConstructionYards == null)
                        {
                            continue;
                        }
                        for (int l = 0; l < builtObject2.ConstructionQueue.ConstructionYards.Count; l++)
                        {
                            if (builtObject2.ConstructionQueue.ConstructionYards[l] != null && builtObject2.ConstructionQueue.ConstructionYards[l].ShipUnderConstruction != null)
                            {
                                if (builtObject2.ConstructionQueue.ConstructionYards[l].ShipUnderConstruction.Design != null && builtObject2.ConstructionQueue.ConstructionYards[l].ShipUnderConstruction.Design == design)
                                {
                                    return true;
                                }
                                if (builtObject2.ConstructionQueue.ConstructionYards[l].ShipUnderConstruction.RetrofitDesign != null && builtObject2.ConstructionQueue.ConstructionYards[l].ShipUnderConstruction.RetrofitDesign == design)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                if (BuiltObjects != null)
                {
                    for (int m = 0; m < BuiltObjects.Count; m++)
                    {
                        if (BuiltObjects[m] != null)
                        {
                            if (BuiltObjects[m].RetrofitDesign != null && BuiltObjects[m].RetrofitDesign == design)
                            {
                                return true;
                            }
                            if (BuiltObjects[m].Mission != null && BuiltObjects[m].Mission.Design != null && BuiltObjects[m].Mission.Design == design)
                            {
                                return true;
                            }
                        }
                    }
                }
                if (PrivateBuiltObjects != null)
                {
                    for (int n = 0; n < PrivateBuiltObjects.Count; n++)
                    {
                        if (PrivateBuiltObjects[n] != null)
                        {
                            if (PrivateBuiltObjects[n].RetrofitDesign != null && PrivateBuiltObjects[n].RetrofitDesign == design)
                            {
                                return true;
                            }
                            if (PrivateBuiltObjects[n].Mission != null && PrivateBuiltObjects[n].Mission.Design != null && PrivateBuiltObjects[n].Mission.Design == design)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void ReasonCannotBuildDesign(Design design, Habitat colony, out bool missingTech, out bool sizeTooBig)
        {
            missingTech = CanBuildDesignTech(design);
            sizeTooBig = CheckDesignWithinConstructionSize(design, colony);
        }

        public bool CanBuildDesign(Design design)
        {
            return CanBuildDesign(design, includeSizeCheck: true);
        }

        public bool CanBuildDesign(Design design, bool includeSizeCheck)
        {
            return CanBuildDesign(design, includeSizeCheck, null);
        }

        public bool CanBuildDesign(Design design, bool includeSizeCheck, Habitat colony)
        {
            bool reasonCannotBuildMissingTech = false;
            bool reasonCannotBuildSizeTooBig = false;
            return CanBuildDesign(design, includeSizeCheck, colony, out reasonCannotBuildMissingTech, out reasonCannotBuildSizeTooBig);
        }

        public bool CanBuildDesign(Design design, bool includeSizeCheck, Habitat colony, out bool reasonCannotBuildMissingTech, out bool reasonCannotBuildSizeTooBig)
        {
            reasonCannotBuildMissingTech = false;
            reasonCannotBuildSizeTooBig = false;
            if (!CanBuildDesignTech(design))
            {
                reasonCannotBuildMissingTech = true;
                return false;
            }
            if (includeSizeCheck && !CheckDesignWithinConstructionSize(design, colony))
            {
                reasonCannotBuildSizeTooBig = true;
                return false;
            }
            return true;
        }

        public bool CheckDesignComponentsResearched(Design design)
        {
            for (int i = 0; i < design.Components.Count; i++)
            {
                Component component = design.Components[i];
                if (!Research.CheckComponentResearched(component))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckThreatIsEnemy(BuiltObject threat)
        {
            if (threat != null && threat.Empire != null)
            {
                if (threat.Empire == this)
                {
                    return false;
                }
                if (threat.Empire == _Galaxy.IndependentEmpire)
                {
                    return false;
                }
                if (PirateEmpireBaseHabitat != null || threat.Empire.PirateEmpireBaseHabitat != null)
                {
                    PirateRelation pirateRelation = ObtainPirateRelation(threat.Empire);
                    if (pirateRelation.Type != PirateRelationType.Protection)
                    {
                        return true;
                    }
                }
                else
                {
                    DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(threat.Empire);
                    if (diplomaticRelation.Type == DiplomaticRelationType.War)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckSafeToBuildAtLocation(Habitat habitat)
        {
            if (habitat != null)
            {
                if (habitat.IsBlockaded)
                {
                    return false;
                }
                if (SystemVisibility != null && habitat.SystemIndex >= 0 && habitat.SystemIndex < SystemVisibility.Count)
                {
                    SystemVisibility systemVisibility = SystemVisibility[habitat.SystemIndex];
                    if (systemVisibility != null && systemVisibility.Threats != null && systemVisibility.Threats.Count > 0)
                    {
                        for (int i = 0; i < systemVisibility.Threats.Count; i++)
                        {
                            BuiltObject builtObject = systemVisibility.Threats[i];
                            if (builtObject == null || builtObject.HasBeenDestroyed || (builtObject.FirepowerRaw <= 0 && builtObject.FighterCapacity <= 0))
                            {
                                continue;
                            }
                            if (builtObject.Role == BuiltObjectRole.Base)
                            {
                                double num = _Galaxy.CalculateDistanceSquared(habitat.Xpos, habitat.Ypos, builtObject.Xpos, builtObject.Ypos);
                                if (num < (double)builtObject.MaximumWeaponsRange && CheckThreatIsEnemy(builtObject))
                                {
                                    return false;
                                }
                            }
                            else if (builtObject.TopSpeed > 0 && builtObject.Role == BuiltObjectRole.Military && CheckThreatIsEnemy(builtObject))
                            {
                                if (builtObject.WarpSpeed > 0)
                                {
                                    return false;
                                }
                                double num2 = _Galaxy.CalculateDistanceSquared(habitat.Xpos, habitat.Ypos, builtObject.Xpos, builtObject.Ypos);
                                if (num2 < 8000.0)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                if (_Galaxy.Systems != null && habitat.SystemIndex >= 0 && habitat.SystemIndex < _Galaxy.Systems.Count)
                {
                    SystemInfo systemInfo = _Galaxy.Systems[habitat.SystemIndex];
                    if (systemInfo != null && systemInfo.Creatures != null && systemInfo.Creatures.Count > 0)
                    {
                        for (int j = 0; j < systemInfo.Creatures.Count; j++)
                        {
                            Creature creature = systemInfo.Creatures[j];
                            if (creature != null && !creature.HasBeenDestroyed && _Galaxy.CheckWithinCreatureAttackRange(habitat.Xpos, habitat.Ypos, creature))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public bool CanBuildBuiltObject(BuiltObject builtObject)
        {
            return CanBuildBuiltObject(builtObject, null);
        }

        public bool CanBuildBuiltObject(BuiltObject builtObject, Habitat colony)
        {
            if (colony != null)
            {
                if (builtObject.Role == BuiltObjectRole.Base)
                {
                    return true;
                }
                if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip)
                {
                    if (builtObject.Design.Size <= MaximumConstructionSizeBase(builtObject.Design.SubRole))
                    {
                        if (colony.Population != null && colony.Population.TotalAmount >= Galaxy.BuildColonyShipPopulationRequirement)
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                if (builtObject.SubRole == BuiltObjectSubRole.ConstructionShip || builtObject.SubRole == BuiltObjectSubRole.ResupplyShip)
                {
                    if (builtObject.Design.Size <= MaximumConstructionSizeBase(builtObject.Design.SubRole))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip || builtObject.SubRole == BuiltObjectSubRole.ConstructionShip || builtObject.SubRole == BuiltObjectSubRole.ResupplyShip)
            {
                return false;
            }
            if (PirateEmpireBaseHabitat == null && (builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort))
            {
                return false;
            }
            int num = 0;
            if (builtObject.Role != BuiltObjectRole.Base)
            {
                num = ((!builtObject.Design.IsPlanetDestroyer) ? MaximumConstructionSize(builtObject.SubRole) : MaximumConstructionSizeBase());
            }
            else
            {
                num = MaximumConstructionSizeBase(builtObject.SubRole);
                if (PirateEmpireBaseHabitat != null && (builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort))
                {
                    num = int.MaxValue;
                }
            }
            if (builtObject.Design.Size <= num)
            {
                if (builtObject.SubRole == BuiltObjectSubRole.Carrier)
                {
                    return _CanBuildCarriers;
                }
                if (builtObject.SubRole == BuiltObjectSubRole.ResupplyShip)
                {
                    return _CanBuildResupplyShips;
                }
                return true;
            }
            return false;
        }

        public int MaximumConstructionSize()
        {
            return MaximumConstructionSize(BuiltObjectSubRole.Undefined);
        }

        public int MaximumConstructionSize(BuiltObjectSubRole shipSubRole)
        {
            if (DominantRace != null)
            {
                switch (shipSubRole)
                {
                    case BuiltObjectSubRole.SmallFreighter:
                    case BuiltObjectSubRole.MediumFreighter:
                    case BuiltObjectSubRole.LargeFreighter:
                    case BuiltObjectSubRole.PassengerShip:
                    case BuiltObjectSubRole.GasMiningShip:
                    case BuiltObjectSubRole.MiningShip:
                        return (int)((double)_BaseMaximumConstructionSize * DominantRace.CivilianShipSizeFactor * BaconRace.CivilianShipSizeMultiplier(this));
                    case BuiltObjectSubRole.Escort:
                    case BuiltObjectSubRole.Frigate:
                    case BuiltObjectSubRole.Destroyer:
                    case BuiltObjectSubRole.Cruiser:
                    case BuiltObjectSubRole.CapitalShip:
                    case BuiltObjectSubRole.TroopTransport:
                    case BuiltObjectSubRole.ResupplyShip:
                    case BuiltObjectSubRole.ExplorationShip:
                        return (int)((double)_BaseMaximumConstructionSize * DominantRace.MilitaryShipSizeFactor * BaconRace.MilitaryShipSizeMultiplier(this));
                    case BuiltObjectSubRole.Carrier:
                        return (int)((double)_BaseMaximumConstructionSize * 1.5 * DominantRace.MilitaryShipSizeFactor * BaconRace.MilitaryShipSizeMultiplier(this));
                    default:
                        return _BaseMaximumConstructionSize;
                }
            }
            return _BaseMaximumConstructionSize;
        }

        public int MaximumConstructionSizeBase()
        {
            return MaximumConstructionSizeBase(BuiltObjectSubRole.Undefined);
        }

        public int MaximumConstructionSizeBase(BuiltObjectSubRole baseSubRole)
        {
            int num = _BaseMaximumConstructionSize * 3;
            BuiltObjectSubRole builtObjectSubRole = baseSubRole;
            if (builtObjectSubRole == BuiltObjectSubRole.ResupplyShip)
            {
                if (DominantRace != null)
                {
                    return (int)((double)num * DominantRace.MilitaryShipSizeFactor);
                }
                return num;
            }
            return num;
        }

        public string CivilityDescription()
        {
            string result = string.Empty;
            if (CivilityRating < -50.0)
            {
                result = TextResolver.GetText("Diabolical");
            }
            else if (CivilityRating >= -50.0 && CivilityRating <= -30.0)
            {
                result = TextResolver.GetText("Evil");
            }
            else if (CivilityRating >= -30.0 && CivilityRating <= -20.0)
            {
                result = TextResolver.GetText("Notorious");
            }
            else if (CivilityRating >= -20.0 && CivilityRating <= -10.0)
            {
                result = TextResolver.GetText("Nasty");
            }
            else if (CivilityRating >= -10.0 && CivilityRating <= -1.0)
            {
                result = TextResolver.GetText("Dubious");
            }
            else if (CivilityRating >= -1.0 && CivilityRating <= 4.0)
            {
                result = TextResolver.GetText("Satisfactory");
            }
            else if (CivilityRating >= 4.0 && CivilityRating <= 10.0)
            {
                result = TextResolver.GetText("Respectable");
            }
            else if (CivilityRating >= 10.0 && CivilityRating <= 16.0)
            {
                result = TextResolver.GetText("Admired");
            }
            else if (CivilityRating >= 16.0 && CivilityRating <= 22.0)
            {
                result = TextResolver.GetText("Noble");
            }
            else if (CivilityRating > 22.0)
            {
                result = TextResolver.GetText("Heroic");
            }
            return result;
        }

        private bool CheckAttackTemptingTarget(StellarObject target)
        {
            int num = 95 + Galaxy.Rnd.Next(0, 15);
            if (DominantRace.AggressionLevel >= num && Galaxy.Rnd.Next(0, 3) == 1)
            {
                double val = 0.6 + (double)(DominantRace.CautionLevel - 100) / 50.0;
                val = Math.Min(1.2, Math.Max(0.01, val));
                if (target.Empire != null && (double)WeightedMilitaryPotency > (double)target.Empire.WeightedMilitaryPotency * val)
                {
                    if (target is Habitat)
                    {
                        Habitat habitat = (Habitat)target;
                        int minimumTroopStrength = Galaxy.DetermineRequiredTroopStrength(this, habitat);
                        ShipGroup shipGroup = FindNearestAvailableFleet(habitat.Xpos, habitat.Ypos, BuiltObjectMissionPriority.Low, 0, FleetPosture.Attack, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: true, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: true, minimumTroopStrength);
                        if (shipGroup != null)
                        {
                            shipGroup.AssignMission(BuiltObjectMissionType.Attack, habitat, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                            return true;
                        }
                    }
                    else if (target is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)target;
                        int overallStrength = builtObject.CalculateOverallStrengthFactor();
                        if (CheckSystemVisible(builtObject.NearestSystemStar))
                        {
                            overallStrength = CalculateDefendingStrength(builtObject);
                        }
                        ShipGroup shipGroup2 = FindNearestAvailableFleet(builtObject.Xpos, builtObject.Ypos, BuiltObjectMissionPriority.Low, overallStrength, FleetPosture.Attack, mustBeWithinFuelRange: true, 0.1, mustBeAutomated: true, shouldBeSmallFleet: false, gatherPointMustBeBlank: false, mustBeWithinPostureRange: true, 0);
                        if (shipGroup2 != null)
                        {
                            BuiltObjectMissionType missionType = DetermineDestroyOrCaptureTarget(shipGroup2, builtObject);
                            shipGroup2.AssignMission(missionType, builtObject, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: false);
                            return true;
                        }
                        for (int i = 0; i < BuiltObjects.Count; i++)
                        {
                            BuiltObject builtObject2 = BuiltObjects[i];
                            if (builtObject2.IsAutoControlled && builtObject2.ShipGroup == null && builtObject2.BuiltAt == null && builtObject2.UnbuiltOrDamagedComponentCount == 0 && (builtObject2.SubRole == BuiltObjectSubRole.Destroyer || builtObject2.SubRole == BuiltObjectSubRole.Cruiser || builtObject2.SubRole == BuiltObjectSubRole.CapitalShip) && (builtObject2.Mission == null || builtObject2.Mission.Type == BuiltObjectMissionType.Undefined || builtObject2.Mission.Priority == BuiltObjectMissionPriority.Low))
                            {
                                BuiltObjectMissionType missionType2 = DetermineDestroyOrCaptureTarget(builtObject2, builtObject, attackingAsGroup: false);
                                builtObject2.AssignMission(missionType2, builtObject, null, BuiltObjectMissionPriority.Normal);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool CheckTemptingTargetColony(Habitat colony)
        {
            if (IsObjectVisibleToThisEmpire(colony))
            {
                int num = DetermineColonizationValue(colony);
                if (num > 1000)
                {
                    int num2 = colony.Troops.TotalDefendStrength + colony.TroopsToRecruit.TotalDefendStrength;
                    if (num2 < colony.TroopLevelMinimum * 100)
                    {
                        int num3 = 0;
                        if (colony.BasesAtHabitat != null)
                        {
                            foreach (BuiltObject item in colony.BasesAtHabitat)
                            {
                                if (item.IsFunctional)
                                {
                                    num3 += item.FirepowerRaw;
                                }
                            }
                        }
                        Design design = _Designs.FindNewestCanBuild(BuiltObjectSubRole.MediumSpacePort);
                        int num4 = 100;
                        if (design != null)
                        {
                            num4 = design.FirepowerRaw;
                        }
                        if (num3 < num4 && CheckAttackTemptingTarget(colony))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool CheckTemptingTargetShip(BuiltObject constructionShip)
        {
            if (IsObjectVisibleToThisEmpire(constructionShip) && constructionShip.Mission != null && constructionShip.Mission.Type == BuiltObjectMissionType.Repair)
            {
                BuiltObject targetBuiltObject = constructionShip.Mission.TargetBuiltObject;
                if (targetBuiltObject != null && targetBuiltObject.BuiltAt == constructionShip)
                {
                    bool flag = false;
                    bool flag2 = false;
                    GalaxyLocationList galaxyLocationList = _Galaxy.DetermineGalaxyLocationsAtPoint(targetBuiltObject.Xpos, targetBuiltObject.Ypos);
                    foreach (GalaxyLocation item in galaxyLocationList)
                    {
                        if (item.Type == GalaxyLocationType.DebrisField)
                        {
                            flag2 = true;
                        }
                        else if (item.Type == GalaxyLocationType.PlanetDestroyer)
                        {
                            flag = true;
                        }
                    }
                    if ((flag2 || flag) && CheckAttackTemptingTarget(targetBuiltObject))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void CheckTemptingTargetsInEmpire(Empire empire)
        {
            if (empire == this)
            {
                return;
            }
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
            if (diplomaticRelation.Type == DiplomaticRelationType.NotMet || diplomaticRelation.Type == DiplomaticRelationType.War || (diplomaticRelation.Strategy != DiplomaticStrategy.Conquer && diplomaticRelation.Strategy != DiplomaticStrategy.Punish))
            {
                return;
            }
            bool flag = false;
            if (empire.Colonies.Count > 0)
            {
                int num = Galaxy.Rnd.Next(0, empire.Colonies.Count);
                if (!flag)
                {
                    for (int i = num; i < empire.Colonies.Count; i++)
                    {
                        if (CheckTemptingTargetColony(empire.Colonies[i]))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    for (int j = 0; j < num; j++)
                    {
                        if (CheckTemptingTargetColony(empire.Colonies[j]))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
            }
            if (empire.ConstructionShips.Count <= 0)
            {
                return;
            }
            int num2 = Galaxy.Rnd.Next(0, empire.ConstructionShips.Count);
            if (!flag)
            {
                for (int k = num2; k < empire.ConstructionShips.Count; k++)
                {
                    if (CheckTemptingTargetShip(empire.ConstructionShips[k]))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            if (flag)
            {
                return;
            }
            for (int l = 0; l < num2; l++)
            {
                if (CheckTemptingTargetShip(empire.ConstructionShips[l]))
                {
                    flag = true;
                    break;
                }
            }
        }

        private void CheckTemptingTargets()
        {
            if (this != _Galaxy.PlayerEmpire && _ControlMilitaryAttacks == AutomationLevel.FullyAutomated)
            {
                int num = Galaxy.Rnd.Next(0, _Galaxy.Empires.Count);
                for (int i = num; i < _Galaxy.Empires.Count; i++)
                {
                    CheckTemptingTargetsInEmpire(_Galaxy.Empires[i]);
                }
                for (int j = 0; j < num; j++)
                {
                    CheckTemptingTargetsInEmpire(_Galaxy.Empires[j]);
                }
            }
        }

        private void DesignResearchStation(IndustryType industryType)
        {
            Design design = null;
            ComponentType componentType = ComponentType.Undefined;
            switch (industryType)
            {
                case IndustryType.Energy:
                    design = _EnergyResearchStation;
                    componentType = ComponentType.LabsEnergyLab;
                    break;
                case IndustryType.HighTech:
                    design = _HighTechResearchStation;
                    componentType = ComponentType.LabsHighTechLab;
                    break;
                case IndustryType.Weapon:
                    design = _WeaponsResearchStation;
                    componentType = ComponentType.LabsWeaponsLab;
                    break;
            }
            Component component = Research.EvaluateDesiredComponent(componentType, ShipDesignFocus.Balanced);
            if (component == null)
            {
                return;
            }
            long currentStarDate = _Galaxy.CurrentStarDate;
            long num = (long)(Galaxy.MinimumDesignReviewIntervalYears * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
            long num2 = 0L;
            if (design != null)
            {
                num2 = design.DateCreated + num;
            }
            if (currentStarDate <= num2)
            {
                return;
            }
            Design design2 = GenerateResearchStationDesign(currentStarDate, componentType);
            if (design == null || !design2.IsEquivalent(design))
            {
                if (design != null)
                {
                    design.IsObsolete = true;
                }
                switch (industryType)
                {
                    case IndustryType.Energy:
                        _EnergyResearchStation = design2;
                        break;
                    case IndustryType.HighTech:
                        _HighTechResearchStation = design2;
                        break;
                    case IndustryType.Weapon:
                        _WeaponsResearchStation = design2;
                        break;
                }
                Designs.Add(design2);
            }
        }

        public Design GenerateResearchStationDesign(long designDate, ComponentType labComponentType)
        {
            DesignSpecification designSpecification = new DesignSpecification(BuiltObjectSubRole.GenericBase, mobile: false);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommandCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.EnergyCollector, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, labComponentType, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HabitationMedicalCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HabitationRecreationCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.SensorProximityArray, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.Armor, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentCategoryType.Shields, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentCategoryType.WeaponBeam, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.DamageControl, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.SensorStealth, 1));
            string name = string.Empty;
            switch (labComponentType)
            {
                case ComponentType.LabsEnergyLab:
                    name = TextResolver.GetText("Energy Research Station");
                    break;
                case ComponentType.LabsHighTechLab:
                    name = TextResolver.GetText("High Tech Research Station");
                    break;
                case ComponentType.LabsWeaponsLab:
                    name = TextResolver.GetText("Weapons Research Station");
                    break;
            }
            Design design = new Design(name);
            design.Role = designSpecification.Role;
            design.SubRole = designSpecification.SubRole;
            design.ImageScalingType = designSpecification.ImageScalingMode;
            design.ImageScalingFactor = designSpecification.ImageScalingFactor;
            design = PlaceComponentsOnDesign(design, designSpecification, null);
            design.Stance = BuiltObjectStance.AttackIfAttacked;
            design.FleeWhen = BuiltObjectFleeWhen.Never;
            design.TacticsStrongerShips = BattleTactics.PointBlank;
            design.TacticsWeakerShips = BattleTactics.PointBlank;
            design.TacticsInvasion = InvasionTactics.DoNotInvade;
            int num = DesignPictureFamilyIndex;
            if (DominantRace != null && PirateEmpireBaseHabitat != null)
            {
                num = DominantRace.DesignPictureFamilyIndexPirates;
                if (num < 0)
                {
                    num = DominantRace.DesignPictureFamilyIndex;
                }
            }
            design.Name = name;
            design.DateCreated = designDate;
            design.Empire = this;
            design.PictureRef = ShipImageHelper.StandardShipImageStartIndex + num * ShipImageHelper.ShipSetImageCount + (int)(Galaxy.ResolveLegacySubRole(designSpecification.SubRole) - 1);
            design.Role = designSpecification.Role;
            design.SubRole = designSpecification.SubRole;
            design.ReDefine();
            return design;
        }

        private void DesignMonitoringStation()
        {
            Component component = Research.EvaluateDesiredComponent(ComponentType.SensorLongRange, ShipDesignFocus.Balanced);
            if (component == null)
            {
                return;
            }
            long currentStarDate = _Galaxy.CurrentStarDate;
            long num = (long)(Galaxy.MinimumDesignReviewIntervalYears * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
            long num2 = 0L;
            if (_MonitoringStationCurrentDesign != null)
            {
                num2 = _MonitoringStationCurrentDesign.DateCreated + num;
            }
            if (currentStarDate <= num2)
            {
                return;
            }
            Design design = GenerateMonitoringStationDesign(currentStarDate);
            if (_MonitoringStationCurrentDesign == null || !design.IsEquivalent(_MonitoringStationCurrentDesign))
            {
                if (_MonitoringStationCurrentDesign != null)
                {
                    _MonitoringStationCurrentDesign.IsObsolete = true;
                }
                _MonitoringStationCurrentDesign = design;
                Designs.Add(design);
            }
        }

        public DesignSpecification GetMonitoringStationDesignSpec()
        {
            DesignSpecification designSpecification = new DesignSpecification(BuiltObjectSubRole.GenericBase, mobile: false);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommandCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 2));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.EnergyCollector, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommerceCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HabitationMedicalCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.HabitationRecreationCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.SensorProximityArray, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.SensorLongRange, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.Armor, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentCategoryType.Shields, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentCategoryType.WeaponBeam, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.DamageControl, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.SensorStealth, 1));
            return designSpecification;
        }

        public Design GenerateMonitoringStationDesign(long designDate)
        {
            DesignSpecification monitoringStationDesignSpec = GetMonitoringStationDesignSpec();
            string text = TextResolver.GetText("Monitoring Station");
            Design design = new Design(text);
            design.Role = monitoringStationDesignSpec.Role;
            design.SubRole = monitoringStationDesignSpec.SubRole;
            design.ImageScalingType = monitoringStationDesignSpec.ImageScalingMode;
            design.ImageScalingFactor = monitoringStationDesignSpec.ImageScalingFactor;
            design = PlaceComponentsOnDesign(design, monitoringStationDesignSpec, null);
            design.Stance = BuiltObjectStance.AttackIfAttacked;
            design.FleeWhen = BuiltObjectFleeWhen.Never;
            design.TacticsStrongerShips = BattleTactics.PointBlank;
            design.TacticsWeakerShips = BattleTactics.PointBlank;
            design.TacticsInvasion = InvasionTactics.DoNotInvade;
            int num = DesignPictureFamilyIndex;
            if (DominantRace != null && PirateEmpireBaseHabitat != null)
            {
                num = DominantRace.DesignPictureFamilyIndexPirates;
                if (num < 0)
                {
                    num = DominantRace.DesignPictureFamilyIndex;
                }
            }
            design.Name = text;
            design.DateCreated = designDate;
            design.Empire = this;
            design.PictureRef = ShipImageHelper.StandardShipImageStartIndex + num * ShipImageHelper.ShipSetImageCount + (int)(Galaxy.ResolveLegacySubRole(monitoringStationDesignSpec.SubRole) - 1);
            design.Role = monitoringStationDesignSpec.Role;
            design.SubRole = monitoringStationDesignSpec.SubRole;
            design.ReDefine();
            return design;
        }

        public Design GenerateDefenseBaseDesign(long designDate)
        {
            DesignSpecification designSpecification = new DesignSpecification(BuiltObjectSubRole.GenericBase, mobile: false);
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommandCenter, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Reactor, 3));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageFuel, 4));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageDockingBay, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.StorageCargo, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.EnergyCollector, 6));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerTargetting, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCountermeasures, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.Armor, 15));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.Shields, 12));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.WeaponBeam, 16));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentCategoryType.WeaponTorpedo, 10));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentCategoryType.WeaponArea, 1));
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.ShouldHave, ComponentType.DamageControl, 1));
            string text = TextResolver.GetText("Defensive Base");
            Design design = new Design(text);
            design.Role = designSpecification.Role;
            design.SubRole = designSpecification.SubRole;
            design.ImageScalingType = designSpecification.ImageScalingMode;
            design.ImageScalingFactor = designSpecification.ImageScalingFactor;
            design = PlaceComponentsOnDesign(design, designSpecification, null);
            design.Stance = BuiltObjectStance.AttackEnemies;
            design.FleeWhen = BuiltObjectFleeWhen.Never;
            design.TacticsStrongerShips = BattleTactics.PointBlank;
            design.TacticsWeakerShips = BattleTactics.PointBlank;
            design.TacticsInvasion = InvasionTactics.DoNotInvade;
            int num = DesignPictureFamilyIndex;
            if (DominantRace != null && PirateEmpireBaseHabitat != null)
            {
                num = DominantRace.DesignPictureFamilyIndexPirates;
                if (num < 0)
                {
                    num = DominantRace.DesignPictureFamilyIndex;
                }
            }
            design.Name = text;
            design.DateCreated = designDate;
            design.Empire = this;
            design.PictureRef = ShipImageHelper.StandardShipImageStartIndex + num * ShipImageHelper.ShipSetImageCount + 20;
            design.Role = designSpecification.Role;
            design.SubRole = designSpecification.SubRole;
            design.ReDefine();
            return design;
        }

        private void DesignDefensiveBase()
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            long num = (long)(Galaxy.MinimumDesignReviewIntervalYears * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
            long num2 = 0L;
            if (_DefenseBaseDesign != null)
            {
                num2 = _DefenseBaseDesign.DateCreated + num;
            }
            if (currentStarDate <= num2)
            {
                return;
            }
            Design design = GenerateDefenseBaseDesign(currentStarDate);
            if (_DefenseBaseDesign == null || !design.IsEquivalent(_DefenseBaseDesign))
            {
                if (_DefenseBaseDesign != null)
                {
                    _DefenseBaseDesign.IsObsolete = true;
                }
                _DefenseBaseDesign = design;
                Designs.Add(design);
            }
        }

        private void BuildDefensiveBases()
        {
            Design design = Designs.FindNewestCanBuildFullEvaluate(BuiltObjectSubRole.DefensiveBase, Capital);
            if (design == null)
            {
                return;
            }
            HabitatList habitatList = new HabitatList();
            StellarObjectList stellarObjectList = ResolveLocationsToDefend();
            StellarObjectList stellarObjectList2 = new StellarObjectList();
            for (int i = 0; i < stellarObjectList.Count; i++)
            {
                if (!(stellarObjectList[i] is Habitat))
                {
                    stellarObjectList2.Add(stellarObjectList[i]);
                }
            }
            for (int j = 0; j < stellarObjectList2.Count; j++)
            {
                stellarObjectList.Remove(stellarObjectList2[j]);
            }
            for (int k = 0; k < Colonies.Count; k++)
            {
                Habitat habitat = Colonies[k];
                int strategicValue = habitat.StrategicValue;
                if (strategicValue > 250000 && !stellarObjectList.Contains(habitat))
                {
                    stellarObjectList.Add(habitat);
                }
            }
            for (int l = 0; l < stellarObjectList.Count; l++)
            {
                if (!(stellarObjectList[l] is Habitat))
                {
                    continue;
                }
                Habitat habitat2 = (Habitat)stellarObjectList[l];
                int num = 0;
                for (int m = 0; m < habitat2.BasesAtHabitat.Count; m++)
                {
                    num = ((habitat2.BasesAtHabitat[m].UnbuiltOrDamagedComponentCount <= 0) ? (num + habitat2.BasesAtHabitat[m].FirepowerRaw) : (num + habitat2.BasesAtHabitat[m].Design.FirepowerRaw));
                }
                int num2 = (int)((double)habitat2.EstimatedDefensiveForceRequired(atWar: false) * 1.2);
                bool flag = true;
                if (DominantRace != null && !DominantRace.Expanding)
                {
                    flag = false;
                }
                if (flag)
                {
                    num2 = Math.Min(num2, 2000);
                    if (Colonies.Count < 6 || SpacePorts.Count < 3)
                    {
                        num2 = (int)((double)num2 / 1.7);
                    }
                    else if (Colonies.Count < 12 || SpacePorts.Count < 5)
                    {
                        num2 = (int)((double)num2 / 1.3);
                    }
                }
                int num3 = 1;
                if (habitat2.ConstructionQueue != null)
                {
                    num3 = habitat2.ConstructionQueue.ConstructionSpeed;
                }
                int num4 = habitat2.BasesAtHabitat.CountBySubRole(BuiltObjectSubRole.DefensiveBase);
                if (num < num2 && num4 < 4 && num3 >= 100 && (_Galaxy.DetermineSpacePortAtColony(habitat2) != null || Colonies.Count > 1))
                {
                    habitatList.Add(habitat2);
                }
            }
            if (habitatList.Count <= 0)
            {
                return;
            }
            double num5 = design.CalculateCurrentPurchasePrice(_Galaxy);
            double num6 = CalculateSupportCost(design);
            List<CargoList> list = new List<CargoList>();
            HabitatList habitatList2 = new HabitatList();
            CargoList resourcesToOrder = null;
            int refusalCount = 0;
            for (int n = 0; n < habitatList.Count; n++)
            {
                Habitat habitat3 = habitatList[n];
                bool flag2 = true;
                if (habitat3.ConstructionQueue != null && habitat3.ConstructionQueue.ConstructionWaitQueue.Count > 0)
                {
                    flag2 = false;
                }
                if (!flag2)
                {
                    continue;
                }
                double num7 = CalculateSpareAnnualRevenueComplete();
                if (!(num6 <= num7) || !(num5 <= StateMoney))
                {
                    continue;
                }
                design.BuildCount++;
                BuiltObject builtObject = new BuiltObject(design, _Galaxy.GenerateBuiltObjectName(design), _Galaxy);
                builtObject.PurchasePrice = num5;
                if (CheckTaskAuthorized(_ControlStateConstruction, ref refusalCount, GenerateAutomationMessageDefensiveBase(habitat3, design), habitat3, AdvisorMessageType.BuildOneOff, design, null))
                {
                    if (habitat3.ConstructionQueue != null && habitat3.ConstructionQueue.AddBuiltObjectToConstruct(builtObject))
                    {
                        string[] array = new string[5]
                        {
                        TextResolver.GetText("Ship SubRole DefensiveBase"),
                        TextResolver.GetText("Weapons Platform"),
                        TextResolver.GetText("Defense Platform"),
                        TextResolver.GetText("Defense Battery"),
                        TextResolver.GetText("Orbital Battery")
                        };
                        builtObject.Name = habitat3.Name + " " + array[Galaxy.Rnd.Next(0, array.Length)];
                        double offsetX = 0.0;
                        double offsetY = 0.0;
                        DetermineOrbitalBaseLocation(habitat3, out offsetX, out offsetY);
                        builtObject.Heading = _Galaxy.SelectRandomHeading();
                        builtObject.TargetHeading = builtObject.Heading;
                        AddBuiltObjectToGalaxy(builtObject, habitat3, offsetLocationFromParent: false, isStateOwned: true, (int)offsetX, (int)offsetY);
                        StateMoney -= num5;
                        PirateEconomy.PerformExpense(num5, PirateExpenseType.Construction, _Galaxy.CurrentStarDate);
                        builtObject.BuiltAt = habitat3;
                        ProcureConstructionComponents(builtObject, habitat3, out resourcesToOrder);
                        list.Add(resourcesToOrder);
                        habitatList2.Add(habitat3);
                    }
                    else
                    {
                        design.BuildCount--;
                    }
                }
                else
                {
                    design.BuildCount--;
                }
            }
            HabitatList habitatList3 = new HabitatList();
            foreach (Habitat item in habitatList2)
            {
                if (!habitatList3.Contains(item))
                {
                    habitatList3.Add(item);
                }
            }
            foreach (Habitat item2 in habitatList3)
            {
                CargoList cargoList = new CargoList();
                for (int num8 = 0; num8 < habitatList2.Count; num8++)
                {
                    if (habitatList2[num8] != item2)
                    {
                        continue;
                    }
                    foreach (Cargo item3 in list[num8])
                    {
                        cargoList.Add(item3);
                    }
                }
                foreach (Cargo item4 in cargoList)
                {
                    CreateOrder(item2, item4.CommodityResource, item4.Amount, isState: false, OrderType.ConstructionShortage);
                }
            }
        }

        public void DetermineOrbitalBaseLocation(Habitat colony, out double offsetX, out double offsetY)
        {
            offsetX = 0.0;
            offsetY = 0.0;
            double num = 400.0;
            if (colony.BasesAtHabitat != null && colony.BasesAtHabitat.Count > 0 && colony.BasesAtHabitat.Count >= 3)
            {
                num /= Math.Sqrt(colony.BasesAtHabitat.Count - 2);
            }
            bool flag = true;
            int num2 = 0;
            while (flag && num2 < 100)
            {
                flag = false;
                double num3 = Galaxy.Rnd.NextDouble() * Math.PI;
                if (Galaxy.Rnd.Next(0, 2) == 1)
                {
                    num3 *= -1.0;
                }
                double num4 = (double)(colony.Diameter / 2) + 150.0 + Galaxy.Rnd.NextDouble() * 100.0;
                offsetX = Math.Cos(num3) * num4;
                offsetY = Math.Sin(num3) * num4;
                for (int i = 0; i < colony.BasesAtHabitat.Count; i++)
                {
                    BuiltObject builtObject = colony.BasesAtHabitat[i];
                    double num5 = _Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, colony.Xpos, colony.Ypos);
                    if (num5 > 150.0)
                    {
                        double num6 = _Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, colony.Xpos + offsetX, colony.Ypos + offsetY);
                        if (num6 < num)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                num2++;
            }
        }

        public bool CheckDesignComponentsAvailable(BuiltObjectRole role, BuiltObjectSubRole subRole)
        {
            List<ComponentType> list = new List<ComponentType>();
            List<ComponentCategoryType> list2 = new List<ComponentCategoryType>();
            if (Research != null && Research.ResearchedComponents != null)
            {
                list.Add(ComponentType.ComputerCommandCenter);
                list.Add(ComponentType.StorageFuel);
                list.Add(ComponentType.HabitationLifeSupport);
                list.Add(ComponentType.HabitationHabModule);
                list2.Add(ComponentCategoryType.Reactor);
                switch (role)
                {
                    case BuiltObjectRole.Military:
                    case BuiltObjectRole.Exploration:
                    case BuiltObjectRole.Freight:
                    case BuiltObjectRole.Passenger:
                    case BuiltObjectRole.Colony:
                    case BuiltObjectRole.Build:
                    case BuiltObjectRole.Resource:
                        list.Add(ComponentType.EngineMainThrust);
                        list.Add(ComponentType.EngineVectoring);
                        break;
                    case BuiltObjectRole.Base:
                        list.Add(ComponentType.StorageDockingBay);
                        break;
                }
                switch (role)
                {
                    case BuiltObjectRole.Build:
                        list.Add(ComponentType.StorageDockingBay);
                        list.Add(ComponentType.StorageCargo);
                        list.Add(ComponentType.ConstructionBuild);
                        list.Add(ComponentType.ManufacturerEnergyPlant);
                        list.Add(ComponentType.ManufacturerHighTechPlant);
                        list.Add(ComponentType.ManufacturerWeaponsPlant);
                        break;
                    case BuiltObjectRole.Colony:
                        list.Add(ComponentType.HabitationColonization);
                        break;
                    case BuiltObjectRole.Exploration:
                        list.Add(ComponentType.SensorResourceProfileSensor);
                        break;
                    case BuiltObjectRole.Passenger:
                        list.Add(ComponentType.StoragePassenger);
                        break;
                    case BuiltObjectRole.Freight:
                        list.Add(ComponentType.StorageCargo);
                        break;
                    case BuiltObjectRole.Resource:
                        list.Add(ComponentType.StorageCargo);
                        list2.Add(ComponentCategoryType.Extractor);
                        break;
                }
                switch (subRole)
                {
                    case BuiltObjectSubRole.TroopTransport:
                        list.Add(ComponentType.StorageTroop);
                        break;
                    case BuiltObjectSubRole.Carrier:
                        list.Add(ComponentType.FighterBay);
                        break;
                    case BuiltObjectSubRole.ResupplyShip:
                        list.Add(ComponentType.ExtractorGasExtractor);
                        list.Add(ComponentType.StorageCargo);
                        list.Add(ComponentType.StorageDockingBay);
                        break;
                    case BuiltObjectSubRole.GasMiningStation:
                        list.Add(ComponentType.ExtractorGasExtractor);
                        list.Add(ComponentType.ComputerCommerceCenter);
                        list.Add(ComponentType.StorageCargo);
                        break;
                    case BuiltObjectSubRole.MiningStation:
                        list.Add(ComponentType.ExtractorMine);
                        list.Add(ComponentType.ComputerCommerceCenter);
                        list.Add(ComponentType.StorageCargo);
                        break;
                    case BuiltObjectSubRole.SmallSpacePort:
                    case BuiltObjectSubRole.MediumSpacePort:
                    case BuiltObjectSubRole.LargeSpacePort:
                        list.Add(ComponentType.ComputerCommerceCenter);
                        list.Add(ComponentType.ConstructionBuild);
                        list.Add(ComponentType.ManufacturerEnergyPlant);
                        list.Add(ComponentType.ManufacturerHighTechPlant);
                        list.Add(ComponentType.ManufacturerWeaponsPlant);
                        break;
                    case BuiltObjectSubRole.EnergyResearchStation:
                        list.Add(ComponentType.LabsEnergyLab);
                        break;
                    case BuiltObjectSubRole.WeaponsResearchStation:
                        list.Add(ComponentType.LabsWeaponsLab);
                        break;
                    case BuiltObjectSubRole.HighTechResearchStation:
                        list.Add(ComponentType.LabsHighTechLab);
                        break;
                    case BuiltObjectSubRole.MonitoringStation:
                        list.Add(ComponentType.SensorLongRange);
                        break;
                    case BuiltObjectSubRole.ResortBase:
                        list.Add(ComponentType.ComputerCommerceCenter);
                        list.Add(ComponentType.HabitationRecreationCenter);
                        break;
                    case BuiltObjectSubRole.GenericBase:
                        list.Add(ComponentType.StorageCargo);
                        break;
                }
                for (int i = 0; i < list2.Count; i++)
                {
                    ComponentCategoryType componentCategoryType = list2[i];
                    bool flag = false;
                    for (int j = 0; j < Research.ResearchedComponents.Count; j++)
                    {
                        Component component = Research.ResearchedComponents[j];
                        if (component.Category == componentCategoryType)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        return false;
                    }
                }
                for (int k = 0; k < list.Count; k++)
                {
                    ComponentType componentType = list[k];
                    bool flag2 = false;
                    for (int l = 0; l < Research.ResearchedComponents.Count; l++)
                    {
                        Component component2 = Research.ResearchedComponents[l];
                        if (component2.Type == componentType)
                        {
                            flag2 = true;
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        return false;
                    }
                }
                switch (subRole)
                {
                    case BuiltObjectSubRole.Escort:
                    case BuiltObjectSubRole.Frigate:
                    case BuiltObjectSubRole.Destroyer:
                    case BuiltObjectSubRole.Cruiser:
                    case BuiltObjectSubRole.CapitalShip:
                    case BuiltObjectSubRole.DefensiveBase:
                        {
                            bool flag3 = false;
                            for (int m = 0; m < Research.ResearchedComponents.Count; m++)
                            {
                                Component component3 = Research.ResearchedComponents[m];
                                if (component3.Category == ComponentCategoryType.WeaponBeam || component3.Category == ComponentCategoryType.WeaponTorpedo || component3.Category == ComponentCategoryType.WeaponArea || component3.Category == ComponentCategoryType.WeaponIon || component3.Category == ComponentCategoryType.WeaponSuperArea || component3.Category == ComponentCategoryType.WeaponSuperBeam || component3.Category == ComponentCategoryType.WeaponSuperTorpedo || component3.Type == ComponentType.WeaponAreaGravity || component3.Type == ComponentType.WeaponGravityBeam)
                                {
                                    flag3 = true;
                                    break;
                                }
                            }
                            if (!flag3)
                            {
                                return false;
                            }
                            break;
                        }
                }
            }
            return true;
        }

        public ComponentImprovement SelectPreferredSuperWeapon(List<ComponentCategoryType> techCategories, List<ComponentType> techTypes, bool mustBePlanetDestroyer)
        {
            ComponentImprovement componentImprovement = null;
            ComponentImprovementList componentImprovementList = new ComponentImprovementList();
            ComponentImprovement componentImprovement2 = Research.EvaluateDesiredComponentImprovement(ComponentType.WeaponSuperBeam, ShipDesignFocus.Balanced);
            if (componentImprovement2 != null && (!mustBePlanetDestroyer || componentImprovement2.IsPlanetDestroyer))
            {
                componentImprovementList.Add(componentImprovement2);
            }
            ComponentImprovement componentImprovement3 = Research.EvaluateDesiredComponentImprovement(ComponentType.WeaponSuperArea, ShipDesignFocus.Balanced);
            if (componentImprovement3 != null && (!mustBePlanetDestroyer || componentImprovement3.IsPlanetDestroyer))
            {
                componentImprovementList.Add(componentImprovement3);
            }
            ComponentImprovement componentImprovement4 = Research.EvaluateDesiredComponentImprovement(ComponentType.WeaponSuperPhaser, ShipDesignFocus.Balanced);
            if (componentImprovement4 != null && (!mustBePlanetDestroyer || componentImprovement4.IsPlanetDestroyer))
            {
                componentImprovementList.Add(componentImprovement4);
            }
            ComponentImprovement componentImprovement5 = Research.EvaluateDesiredComponentImprovement(ComponentType.WeaponSuperRailGun, ShipDesignFocus.Balanced);
            if (componentImprovement5 != null && (!mustBePlanetDestroyer || componentImprovement5.IsPlanetDestroyer))
            {
                componentImprovementList.Add(componentImprovement5);
            }
            ComponentImprovement componentImprovement6 = Research.EvaluateDesiredComponentImprovement(ComponentType.WeaponSuperTorpedo, ShipDesignFocus.Balanced);
            if (componentImprovement6 != null && (!mustBePlanetDestroyer || componentImprovement6.IsPlanetDestroyer))
            {
                componentImprovementList.Add(componentImprovement6);
            }
            ComponentImprovement componentImprovement7 = Research.EvaluateDesiredComponentImprovement(ComponentType.WeaponSuperMissile, ShipDesignFocus.Balanced);
            if (componentImprovement7 != null && (!mustBePlanetDestroyer || componentImprovement7.IsPlanetDestroyer))
            {
                componentImprovementList.Add(componentImprovement7);
            }
            if (componentImprovementList.Count > 0)
            {
                if (componentImprovement4 != null && (techTypes.Contains(ComponentType.WeaponPhaser) || techTypes.Contains(ComponentType.WeaponSuperPhaser)) && (!mustBePlanetDestroyer || componentImprovement4.IsPlanetDestroyer))
                {
                    componentImprovement = componentImprovement4;
                }
                else if (componentImprovement5 != null && (techTypes.Contains(ComponentType.WeaponRailGun) || techTypes.Contains(ComponentType.WeaponSuperRailGun)) && (!mustBePlanetDestroyer || componentImprovement5.IsPlanetDestroyer))
                {
                    componentImprovement = componentImprovement5;
                }
                else if (componentImprovement7 != null && (techTypes.Contains(ComponentType.WeaponMissile) || techTypes.Contains(ComponentType.WeaponSuperMissile)) && (!mustBePlanetDestroyer || componentImprovement7.IsPlanetDestroyer))
                {
                    componentImprovement = componentImprovement7;
                }
                else if (componentImprovement6 != null && (techTypes.Contains(ComponentType.WeaponTorpedo) || techTypes.Contains(ComponentType.WeaponSuperTorpedo)) && (!mustBePlanetDestroyer || componentImprovement6.IsPlanetDestroyer))
                {
                    componentImprovement = componentImprovement6;
                }
                else if (componentImprovement2 != null && (techTypes.Contains(ComponentType.WeaponBeam) || techTypes.Contains(ComponentType.WeaponSuperBeam)) && (!mustBePlanetDestroyer || componentImprovement2.IsPlanetDestroyer))
                {
                    componentImprovement = componentImprovement2;
                }
                else if (componentImprovement3 != null && (techTypes.Contains(ComponentType.WeaponAreaDestruction) || techTypes.Contains(ComponentType.WeaponSuperArea)) && (!mustBePlanetDestroyer || componentImprovement3.IsPlanetDestroyer))
                {
                    componentImprovement = componentImprovement3;
                }
                if (componentImprovement == null)
                {
                    int index = Galaxy.Rnd.Next(0, componentImprovementList.Count);
                    componentImprovement = componentImprovementList[index];
                }
            }
            return componentImprovement;
        }

        private Design PlaceComponentsOnDesign(Design design, DesignSpecification designSpec, ComponentImprovementList torpedoWeapons)
        {
            int maxShipSize = MaximumConstructionSize(design.SubRole);
            int maxBaseSize = MaximumConstructionSizeBase(design.SubRole);
            return PlaceComponentsOnDesign(design, designSpec, torpedoWeapons, maxShipSize, maxBaseSize, null, 0.0);
        }

        private Design PlaceComponentsOnDesign(Design design, DesignSpecification designSpec, ComponentImprovementList torpedoWeapons, double techAdvanceAmount)
        {
            int maxShipSize = MaximumConstructionSize(design.SubRole);
            int maxBaseSize = MaximumConstructionSizeBase(design.SubRole);
            return PlaceComponentsOnDesign(design, designSpec, torpedoWeapons, maxShipSize, maxBaseSize, null, techAdvanceAmount);
        }

        public Design PlaceComponentsOnDesign(Design design, DesignSpecification designSpec, ComponentImprovementList torpedoWeapons, int maxShipSize, int maxBaseSize, Design mostRecentDesign)
        {
            return PlaceComponentsOnDesign(design, designSpec, torpedoWeapons, maxShipSize, maxBaseSize, mostRecentDesign, 0.0);
        }

        public Design PlaceComponentsOnDesign(Design design, DesignSpecification designSpec, ComponentImprovementList torpedoWeapons, int maxShipSize, int maxBaseSize, Design mostRecentDesign, double techAdvanceAmount)
        {
            ShipDesignFocus designFocus = ShipDesignFocus.Balanced;
            List<ComponentCategoryType> techFocusCategories = new List<ComponentCategoryType>();
            List<ComponentType> techFocusTypes = new List<ComponentType>();
            Galaxy.ResolveTechFocuses(this, out techFocusCategories, out techFocusTypes);
            if (DominantRace != null && Policy != null)
            {
                designFocus = Policy.ResearchDesignOverallFocus;
            }
            if (torpedoWeapons == null)
            {
                torpedoWeapons = Galaxy.GenerateOrderedComponentImprovementList(ComponentCategoryType.WeaponTorpedo, 1);
            }
            double energyConsumed = 0.0;
            double num = 0.0;
            ComponentImprovement componentImprovement = Research.EvaluateDesiredComponentImprovement(ComponentCategoryType.Reactor, designFocus);
            ComponentImprovement componentImprovement2 = Research.EvaluateDesiredComponentImprovement(ComponentType.StorageFuel, designFocus);
            ComponentImprovement componentImprovement3 = Research.EvaluateDesiredComponentImprovement(ComponentCategoryType.HyperDrive, designFocus);
            ComponentImprovement componentImprovement4 = Research.EvaluateDesiredComponentImprovement(ComponentType.EnergyCollector, designFocus);
            ComponentImprovement componentImprovement5 = Research.EvaluateDesiredComponentImprovement(ComponentType.HabitationHabModule, designFocus);
            ComponentImprovement componentImprovement6 = Research.EvaluateDesiredComponentImprovement(ComponentType.HabitationLifeSupport, designFocus);
            ComponentImprovement componentImprovement7 = Research.EvaluateDesiredComponentImprovement(ComponentType.WeaponMissile, designFocus);
            ComponentImprovement componentImprovement8 = Research.EvaluateDesiredComponentImprovement(ComponentType.WeaponPhaser, designFocus);
            ComponentImprovement componentImprovement9 = Research.EvaluateDesiredComponentImprovement(ComponentType.WeaponRailGun, designFocus);
            ComponentImprovement componentImprovement10 = Research.EvaluateDesiredComponentImprovement(ComponentType.WeaponGravityBeam, designFocus);
            ComponentImprovement componentImprovement11 = Research.EvaluateDesiredComponentImprovement(ComponentType.WeaponAreaGravity, designFocus);
            ComponentImprovement componentImprovement12 = Research.EvaluateDesiredComponentImprovement(ComponentType.WeaponAreaDestruction, designFocus);
            ComponentImprovement componentImprovement13 = Research.EvaluateDesiredComponentImprovement(ComponentCategoryType.WeaponTorpedo, designFocus);
            ComponentImprovement componentImprovement14 = Research.EvaluateDesiredComponentImprovement(ComponentCategoryType.WeaponBeam, designFocus);
            if (techAdvanceAmount > 0.0)
            {
                Component component = Component.EvaluateLatest(ComponentCategoryType.Reactor, techAdvanceAmount);
                if (component != null)
                {
                    componentImprovement = Research.ResolveImprovedComponentValues(component);
                }
                component = Component.EvaluateLatest(ComponentType.StorageFuel, techAdvanceAmount);
                if (component != null)
                {
                    componentImprovement2 = Research.ResolveImprovedComponentValues(component);
                }
                component = Component.EvaluateLatest(ComponentType.HabitationHabModule, techAdvanceAmount);
                if (component != null)
                {
                    componentImprovement5 = Research.ResolveImprovedComponentValues(component);
                }
                component = Component.EvaluateLatest(ComponentType.HabitationLifeSupport, techAdvanceAmount);
                if (component != null)
                {
                    componentImprovement6 = Research.ResolveImprovedComponentValues(component);
                }
                component = Component.EvaluateLatest(ComponentType.WeaponMissile, techAdvanceAmount);
                if (component != null)
                {
                    componentImprovement7 = Research.ResolveImprovedComponentValues(component);
                }
                component = Component.EvaluateLatest(ComponentType.WeaponPhaser, techAdvanceAmount);
                if (component != null)
                {
                    componentImprovement8 = Research.ResolveImprovedComponentValues(component);
                }
                component = Component.EvaluateLatest(ComponentType.WeaponRailGun, techAdvanceAmount);
                if (component != null)
                {
                    componentImprovement9 = Research.ResolveImprovedComponentValues(component);
                }
                component = Component.EvaluateLatest(ComponentType.WeaponGravityBeam, techAdvanceAmount);
                if (component != null)
                {
                    componentImprovement10 = Research.ResolveImprovedComponentValues(component);
                }
                component = Component.EvaluateLatest(ComponentType.WeaponAreaGravity, techAdvanceAmount);
                if (component != null)
                {
                    componentImprovement11 = Research.ResolveImprovedComponentValues(component);
                }
            }
            int recommendedReactorComponentCount = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            int num12 = 0;
            int num13 = 0;
            for (int i = 0; i < designSpec.ComponentRules.Count; i++)
            {
                DesignSpecificationComponentRule designSpecificationComponentRule = designSpec.ComponentRules[i];
                ComponentImprovement componentImprovement15 = null;
                if (!(techAdvanceAmount > 0.0))
                {
                    componentImprovement15 = ((designSpecificationComponentRule.ComponentType == ComponentType.Undefined) ? Research.EvaluateDesiredComponentImprovement(designSpecificationComponentRule.ComponentCategory, designFocus) : Research.EvaluateDesiredComponentImprovement(designSpecificationComponentRule.ComponentType, designFocus));
                }
                else if (designSpecificationComponentRule.ComponentType != 0)
                {
                    Component component2 = Component.EvaluateLatest(designSpecificationComponentRule.ComponentType, techAdvanceAmount);
                    if (component2 != null)
                    {
                        componentImprovement15 = Research.ResolveImprovedComponentValues(component2);
                    }
                }
                else
                {
                    Component component3 = Component.EvaluateLatest(designSpecificationComponentRule.ComponentCategory, techAdvanceAmount);
                    if (component3 != null)
                    {
                        componentImprovement15 = Research.ResolveImprovedComponentValues(component3);
                    }
                }
                int num14 = 1;
                ComponentType componentType = designSpecificationComponentRule.ComponentType;
                if (componentType == ComponentType.WeaponGravityBeam)
                {
                    if (componentImprovement15 != null && componentImprovement15.ImprovedComponent != null)
                    {
                        componentType = componentImprovement15.ImprovedComponent.Type;
                        if (componentType == ComponentType.WeaponGravityBeam)
                        {
                            if (componentImprovement9 != null && componentImprovement15.TechLevel < componentImprovement9.TechLevel - num14)
                            {
                                componentImprovement15 = componentImprovement9;
                            }
                            if (componentImprovement8 != null && componentImprovement15.TechLevel < componentImprovement8.TechLevel - num14)
                            {
                                componentImprovement15 = componentImprovement8;
                            }
                            if (componentImprovement14 != null && componentImprovement15.TechLevel < componentImprovement14.TechLevel - num14)
                            {
                                componentImprovement15 = componentImprovement14;
                            }
                        }
                    }
                    if (componentImprovement15 == null)
                    {
                        if (componentImprovement10 != null)
                        {
                            componentImprovement15 = componentImprovement10;
                        }
                        else if (componentImprovement14 != null)
                        {
                            componentImprovement15 = componentImprovement14;
                        }
                        else if (componentImprovement8 != null)
                        {
                            componentImprovement15 = componentImprovement8;
                        }
                        else if (componentImprovement9 != null)
                        {
                            componentImprovement15 = componentImprovement9;
                        }
                        else if (componentImprovement13 != null)
                        {
                            componentImprovement15 = componentImprovement13;
                        }
                        else if (componentImprovement7 != null)
                        {
                            componentImprovement15 = componentImprovement7;
                        }
                        else if (componentImprovement12 != null && num3 <= 0)
                        {
                            componentImprovement15 = componentImprovement12;
                        }
                        else if (componentImprovement11 != null && num3 <= 0)
                        {
                            componentImprovement15 = componentImprovement11;
                        }
                    }
                }
                switch (designSpecificationComponentRule.ComponentCategory)
                {
                    case ComponentCategoryType.WeaponArea:
                        if (componentImprovement11 != null && techFocusTypes.Contains(ComponentType.WeaponAreaGravity))
                        {
                            componentImprovement15 = componentImprovement11;
                        }
                        if (componentImprovement15 == null && componentImprovement11 != null)
                        {
                            componentImprovement15 = componentImprovement11;
                        }
                        break;
                    case ComponentCategoryType.WeaponBeam:
                        if (componentImprovement14 != null && techFocusCategories.Contains(ComponentCategoryType.WeaponBeam))
                        {
                            componentImprovement15 = componentImprovement14;
                        }
                        else if (componentImprovement8 != null && techFocusTypes.Contains(ComponentType.WeaponPhaser))
                        {
                            componentImprovement15 = componentImprovement8;
                        }
                        else if (componentImprovement9 != null && techFocusTypes.Contains(ComponentType.WeaponRailGun))
                        {
                            componentImprovement15 = componentImprovement9;
                        }
                        else if (componentImprovement10 != null && techFocusTypes.Contains(ComponentType.WeaponGravityBeam))
                        {
                            componentImprovement15 = componentImprovement10;
                        }
                        if (componentImprovement15 != null && componentImprovement15.ImprovedComponent != null)
                        {
                            switch (componentImprovement15.ImprovedComponent.Type)
                            {
                                case ComponentType.WeaponBeam:
                                    if (componentImprovement9 != null && componentImprovement15.TechLevel < componentImprovement9.TechLevel - num14)
                                    {
                                        componentImprovement15 = componentImprovement9;
                                    }
                                    if (componentImprovement8 != null && componentImprovement15.TechLevel < componentImprovement8.TechLevel - num14)
                                    {
                                        componentImprovement15 = componentImprovement8;
                                    }
                                    if (componentImprovement10 != null && componentImprovement15.TechLevel < componentImprovement10.TechLevel - num14)
                                    {
                                        componentImprovement15 = componentImprovement10;
                                    }
                                    break;
                                case ComponentType.WeaponGravityBeam:
                                    if (componentImprovement9 != null && componentImprovement15.TechLevel < componentImprovement9.TechLevel - num14)
                                    {
                                        componentImprovement15 = componentImprovement9;
                                    }
                                    if (componentImprovement8 != null && componentImprovement15.TechLevel < componentImprovement8.TechLevel - num14)
                                    {
                                        componentImprovement15 = componentImprovement8;
                                    }
                                    if (componentImprovement14 != null && componentImprovement15.TechLevel < componentImprovement14.TechLevel - num14)
                                    {
                                        componentImprovement15 = componentImprovement14;
                                    }
                                    break;
                                case ComponentType.WeaponPhaser:
                                    if (componentImprovement9 != null && componentImprovement15.TechLevel < componentImprovement9.TechLevel - num14)
                                    {
                                        componentImprovement15 = componentImprovement9;
                                    }
                                    if (componentImprovement10 != null && componentImprovement15.TechLevel < componentImprovement10.TechLevel - num14)
                                    {
                                        componentImprovement15 = componentImprovement10;
                                    }
                                    if (componentImprovement14 != null && componentImprovement15.TechLevel < componentImprovement14.TechLevel - num14)
                                    {
                                        componentImprovement15 = componentImprovement14;
                                    }
                                    break;
                                case ComponentType.WeaponRailGun:
                                    if (componentImprovement8 != null && componentImprovement15.TechLevel < componentImprovement8.TechLevel - num14)
                                    {
                                        componentImprovement15 = componentImprovement8;
                                    }
                                    if (componentImprovement10 != null && componentImprovement15.TechLevel < componentImprovement10.TechLevel - num14)
                                    {
                                        componentImprovement15 = componentImprovement10;
                                    }
                                    if (componentImprovement14 != null && componentImprovement15.TechLevel < componentImprovement14.TechLevel - num14)
                                    {
                                        componentImprovement15 = componentImprovement14;
                                    }
                                    break;
                            }
                        }
                        if (componentImprovement15 == null)
                        {
                            if (componentImprovement14 != null)
                            {
                                componentImprovement15 = componentImprovement14;
                            }
                            else if (componentImprovement8 != null)
                            {
                                componentImprovement15 = componentImprovement8;
                            }
                            else if (componentImprovement9 != null)
                            {
                                componentImprovement15 = componentImprovement9;
                            }
                            else if (componentImprovement10 != null)
                            {
                                componentImprovement15 = componentImprovement10;
                            }
                            else if (componentImprovement13 != null)
                            {
                                componentImprovement15 = componentImprovement13;
                            }
                            else if (componentImprovement7 != null)
                            {
                                componentImprovement15 = componentImprovement7;
                            }
                            else if (componentImprovement12 != null && num3 <= 0)
                            {
                                componentImprovement15 = componentImprovement12;
                            }
                            else if (componentImprovement11 != null && num3 <= 0)
                            {
                                componentImprovement15 = componentImprovement11;
                            }
                        }
                        break;
                    case ComponentCategoryType.WeaponTorpedo:
                        if (componentImprovement15 != null && designSpecificationComponentRule.ComponentType != ComponentType.WeaponBombard)
                        {
                            int num15 = 0;
                            int num16 = 0;
                            while (componentImprovement15.Value7 > 0 && componentImprovement15.ImprovedComponent.ComponentID != 9 && num15 < torpedoWeapons.Count)
                            {
                                if (torpedoWeapons.Count > num16 && Research.CheckComponentResearched(torpedoWeapons[num16].ImprovedComponent))
                                {
                                    componentImprovement15 = torpedoWeapons[num16];
                                }
                                num16++;
                                num15++;
                            }
                        }
                        if (componentImprovement15 != null && componentImprovement15.ImprovedComponent != null)
                        {
                            switch (componentImprovement15.ImprovedComponent.Type)
                            {
                                case ComponentType.WeaponTorpedo:
                                    if (componentImprovement7 != null && componentImprovement15.TechLevel < componentImprovement7.TechLevel - num14)
                                    {
                                        componentImprovement15 = componentImprovement7;
                                    }
                                    break;
                                case ComponentType.WeaponMissile:
                                    if (componentImprovement13 != null && componentImprovement15.TechLevel < componentImprovement13.TechLevel - num14)
                                    {
                                        componentImprovement15 = componentImprovement13;
                                    }
                                    break;
                            }
                        }
                        if (componentImprovement15 == null)
                        {
                            if (componentImprovement13 != null && componentImprovement13.Value1 > 0)
                            {
                                componentImprovement15 = componentImprovement13;
                            }
                            else if (componentImprovement7 != null)
                            {
                                componentImprovement15 = componentImprovement7;
                            }
                        }
                        break;
                }
                if (componentImprovement15 == null)
                {
                    continue;
                }
                bool flag = false;
                bool flag2 = false;
                if (componentImprovement15.ImprovedComponent != null && componentImprovement15.ImprovedComponent.Type == ComponentType.Reactor)
                {
                    flag = true;
                }
                else if (componentImprovement15.ImprovedComponent != null && componentImprovement15.ImprovedComponent.Type == ComponentType.EnergyCollector)
                {
                    flag2 = true;
                }
                int num17 = designSpecificationComponentRule.Amount;
                switch (designSpecificationComponentRule.ComponentRuleType)
                {
                    case DesignSpecificationComponentRuleType.MustHave:
                        {
                            num17 = AdjustComponentAmount(designSpec, designSpecificationComponentRule);
                            for (int k = 0; k < num17; k++)
                            {
                                if (flag)
                                {
                                    recommendedReactorComponentCount++;
                                }
                                else if (flag2)
                                {
                                    num2++;
                                }
                                else
                                {
                                    design.Components.Add(componentImprovement15.ImprovedComponent);
                                }
                                num13 += componentImprovement15.ImprovedComponent.Size;
                                energyConsumed += Design.DetermineComponentEnergyRequirementsExcludeHyperdrive(componentImprovement15);
                                num += Design.DetermineComponentEnergyOutput(componentImprovement15);
                            }
                            break;
                        }
                    case DesignSpecificationComponentRuleType.MustNotHave:
                        num17 = 0;
                        break;
                    case DesignSpecificationComponentRuleType.ShouldNotHave:
                    case DesignSpecificationComponentRuleType.ShouldHave:
                        {
                            num17 = 0;
                            if (!IncludeOptionalComponent(designSpecificationComponentRule, techFocusCategories, techFocusTypes))
                            {
                                break;
                            }
                            num17 = AdjustComponentAmount(designSpec, designSpecificationComponentRule);
                            for (int j = 0; j < num17; j++)
                            {
                                if (flag)
                                {
                                    recommendedReactorComponentCount++;
                                }
                                else if (flag2)
                                {
                                    num2++;
                                }
                                else
                                {
                                    design.Components.Add(componentImprovement15.ImprovedComponent);
                                }
                                num13 += componentImprovement15.ImprovedComponent.Size;
                                energyConsumed += Design.DetermineComponentEnergyRequirementsExcludeHyperdrive(componentImprovement15);
                                num += Design.DetermineComponentEnergyOutput(componentImprovement15);
                            }
                            break;
                        }
                }
                switch (componentImprovement15.ImprovedComponent.Type)
                {
                    case ComponentType.WeaponBeam:
                    case ComponentType.WeaponGravityBeam:
                    case ComponentType.WeaponPhaser:
                    case ComponentType.WeaponRailGun:
                        num4 += num17;
                        num3 += num17;
                        break;
                    case ComponentType.WeaponTorpedo:
                    case ComponentType.WeaponMissile:
                        num5 += num17;
                        num3 += num17;
                        break;
                    case ComponentType.WeaponBombard:
                    case ComponentType.WeaponIonCannon:
                    case ComponentType.WeaponIonPulse:
                    case ComponentType.WeaponAreaGravity:
                    case ComponentType.WeaponAreaDestruction:
                    case ComponentType.WeaponSuperBeam:
                    case ComponentType.WeaponSuperArea:
                    case ComponentType.WeaponSuperTorpedo:
                    case ComponentType.WeaponSuperMissile:
                    case ComponentType.WeaponSuperPhaser:
                    case ComponentType.WeaponSuperRailGun:
                        num3 += num17;
                        break;
                    case ComponentType.FighterBay:
                        num6 += num17;
                        break;
                    case ComponentType.EngineMainThrust:
                        num7 += num17;
                        break;
                    case ComponentType.Shields:
                        num8 += num17;
                        break;
                    case ComponentType.StoragePassenger:
                        num9 += num17;
                        break;
                    case ComponentType.ExtractorGasExtractor:
                        num10 += num17;
                        break;
                    case ComponentType.ExtractorMine:
                        num11 += num17;
                        break;
                    case ComponentType.ExtractorLuxury:
                        num12 += num17;
                        break;
                }
                switch (componentImprovement15.ImprovedComponent.Type)
                {
                    case ComponentType.EnergyCollector:
                        if (design.Role == BuiltObjectRole.Base && designSpec.Contains(ComponentType.SensorLongRange))
                        {
                            num2 += 2;
                            num13 += componentImprovement15.ImprovedComponent.Size;
                            num13 += componentImprovement15.ImprovedComponent.Size;
                            num += Design.DetermineComponentEnergyOutput(componentImprovement15);
                            num += Design.DetermineComponentEnergyOutput(componentImprovement15);
                        }
                        break;
                    case ComponentType.EngineMainThrust:
                    case ComponentType.EngineVectoring:
                        if (Policy.ResearchDesignOverallFocus == ShipDesignFocus.SpeedAgility)
                        {
                            design.Components.Add(componentImprovement15.ImprovedComponent);
                            num13 += componentImprovement15.ImprovedComponent.Size;
                            energyConsumed += Design.DetermineComponentEnergyRequirementsExcludeHyperdrive(componentImprovement15);
                        }
                        break;
                }
            }
            if (designSpec.Role == BuiltObjectRole.Military && num3 <= 0 && num6 <= 0)
            {
                return null;
            }
            if (!CheckEmpireHasHyperDriveTech(this))
            {
                BuiltObjectSubRole subRole = designSpec.SubRole;
                if (subRole == BuiltObjectSubRole.ConstructionShip)
                {
                    ComponentImprovement componentImprovement16 = Research.EvaluateDesiredComponentImprovement(ComponentType.StorageFuel, designFocus);
                    if (componentImprovement16 != null && componentImprovement16.ImprovedComponent != null)
                    {
                        design.Components.Add(componentImprovement16.ImprovedComponent);
                        design.Components.Add(componentImprovement16.ImprovedComponent);
                        design.Components.Add(componentImprovement16.ImprovedComponent);
                        design.Components.Add(componentImprovement16.ImprovedComponent);
                        num13 += componentImprovement16.ImprovedComponent.Size * 4;
                    }
                }
            }
            if (PirateEmpireBaseHabitat != null && (designSpec.SubRole == BuiltObjectSubRole.SmallSpacePort || designSpec.SubRole == BuiltObjectSubRole.MediumSpacePort || designSpec.SubRole == BuiltObjectSubRole.LargeSpacePort) && design.Components.GetFirstByType(ComponentType.ExtractorGasExtractor) == null)
            {
                Component component4 = Research.EvaluateDesiredComponent(ComponentType.ExtractorGasExtractor, designFocus);
                if (component4 != null)
                {
                    design.Components.Add(component4);
                    design.Components.Add(component4);
                    num13 += component4.Size * 2;
                }
            }
            double staticEnergyUsed = Design.CalculateStaticEnergyUsage(design.Components);
            double num18 = energyConsumed + staticEnergyUsed - num;
            if (num18 > 0.0 && componentImprovement != null && componentImprovement2 != null)
            {
                int num19 = (int)(0.99 + num18 / (double)componentImprovement.Value1);
                for (int l = 0; l < num19; l++)
                {
                    recommendedReactorComponentCount++;
                    num13 += componentImprovement.ImprovedComponent.Size;
                    num += Design.DetermineComponentEnergyOutput(componentImprovement);
                }
                if (designSpec.Role == BuiltObjectRole.Base)
                {
                    bool flag3 = true;
                    switch (designSpec.SubRole)
                    {
                        case BuiltObjectSubRole.SmallSpacePort:
                        case BuiltObjectSubRole.MediumSpacePort:
                        case BuiltObjectSubRole.LargeSpacePort:
                        case BuiltObjectSubRole.DefensiveBase:
                            flag3 = false;
                            break;
                    }
                    if (!flag3 || num13 + num19 * componentImprovement2.ImprovedComponent.Size <= maxBaseSize)
                    {
                        for (int m = 0; m < num19; m++)
                        {
                            design.Components.Add(componentImprovement2.ImprovedComponent);
                            num13 += componentImprovement2.ImprovedComponent.Size;
                        }
                    }
                }
            }
            switch (designSpec.SubRole)
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
                case BuiltObjectSubRole.SmallFreighter:
                case BuiltObjectSubRole.MediumFreighter:
                case BuiltObjectSubRole.LargeFreighter:
                case BuiltObjectSubRole.ColonyShip:
                case BuiltObjectSubRole.PassengerShip:
                case BuiltObjectSubRole.ConstructionShip:
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                    {
                        staticEnergyUsed = Design.CalculateStaticEnergyUsage(design.Components);
                        double num20 = num - staticEnergyUsed;
                        ComponentImprovement componentImprovement17 = Research.EvaluateDesiredComponentImprovement(ComponentCategoryType.HyperDrive, designFocus);
                        if (componentImprovement17 != null && (double)componentImprovement17.Value2 > num20)
                        {
                            double num21 = Design.DetermineComponentEnergyOutput(componentImprovement);
                            int num22 = (int)(0.99 + ((double)componentImprovement17.Value2 - num20) / num21);
                            for (int n = 0; n < num22; n++)
                            {
                                recommendedReactorComponentCount++;
                                num13 += componentImprovement.ImprovedComponent.Size;
                                num += Design.DetermineComponentEnergyOutput(componentImprovement);
                            }
                        }
                        break;
                    }
            }
            int num23 = 0;
            int num24 = 0;
            if (design.SubRole == BuiltObjectSubRole.CapitalShip || design.SubRole == BuiltObjectSubRole.Carrier)
            {
                num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, num13, designIsBase: false);
                num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, num13, designIsBase: false);
                int num25 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                int num26 = MaximumConstructionSize(design.SubRole);
                if (num25 < num26)
                {
                    double num27 = (double)num26 / (double)num25;
                    if (num27 > 1.05)
                    {
                        int[] array = design.Components.ResolveComponentCountsByType();
                        for (int num28 = 0; num28 < array.Length; num28++)
                        {
                            if (array[num28] <= 0)
                            {
                                continue;
                            }
                            Component component5 = new Component(num28);
                            switch (component5.Type)
                            {
                                case ComponentType.Reactor:
                                    {
                                        int num32 = (int)((double)array[num28] * num27);
                                        int num33 = num32 - array[num28];
                                        for (int num34 = 0; num34 < num33; num34++)
                                        {
                                            recommendedReactorComponentCount++;
                                        }
                                        break;
                                    }
                                case ComponentType.EnergyCollector:
                                    {
                                        int num35 = (int)((double)array[num28] * num27);
                                        int num36 = num35 - array[num28];
                                        for (int num37 = 0; num37 < num36; num37++)
                                        {
                                            num2++;
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        int num29 = (int)((double)array[num28] * num27);
                                        int num30 = num29 - array[num28];
                                        for (int num31 = 0; num31 < num30; num31++)
                                        {
                                            design.Components.Add(component5);
                                        }
                                        break;
                                    }
                                case ComponentType.HyperDeny:
                                case ComponentType.HyperStop:
                                case ComponentType.HyperDrive:
                                case ComponentType.SensorProximityArray:
                                case ComponentType.SensorResourceProfileSensor:
                                case ComponentType.SensorLongRange:
                                case ComponentType.SensorTraceScanner:
                                case ComponentType.SensorScannerJammer:
                                case ComponentType.SensorStealth:
                                case ComponentType.ComputerTargetting:
                                case ComponentType.ComputerTargettingFleet:
                                case ComponentType.ComputerCountermeasures:
                                case ComponentType.ComputerCountermeasuresFleet:
                                case ComponentType.ComputerCommandCenter:
                                case ComponentType.ComputerCommerceCenter:
                                case ComponentType.HabitationLifeSupport:
                                case ComponentType.HabitationHabModule:
                                case ComponentType.DamageControl:
                                case ComponentType.HabitationMedicalCenter:
                                case ComponentType.HabitationRecreationCenter:
                                case ComponentType.HabitationColonization:
                                case ComponentType.EnergyToFuel:
                                    break;
                            }
                        }
                    }
                }
            }
            int num38 = maxShipSize;
            switch (design.SubRole)
            {
                case BuiltObjectSubRole.ResupplyShip:
                case BuiltObjectSubRole.ColonyShip:
                case BuiltObjectSubRole.ConstructionShip:
                    num38 = maxBaseSize;
                    break;
                default:
                    num38 = maxShipSize;
                    break;
            }
            switch (design.SubRole)
            {
                case BuiltObjectSubRole.Escort:
                case BuiltObjectSubRole.Frigate:
                case BuiltObjectSubRole.Destroyer:
                case BuiltObjectSubRole.Cruiser:
                case BuiltObjectSubRole.CapitalShip:
                case BuiltObjectSubRole.TroopTransport:
                case BuiltObjectSubRole.Carrier:
                case BuiltObjectSubRole.ExplorationShip:
                case BuiltObjectSubRole.SmallFreighter:
                case BuiltObjectSubRole.MediumFreighter:
                case BuiltObjectSubRole.LargeFreighter:
                case BuiltObjectSubRole.ColonyShip:
                case BuiltObjectSubRole.PassengerShip:
                case BuiltObjectSubRole.ConstructionShip:
                case BuiltObjectSubRole.GasMiningShip:
                case BuiltObjectSubRole.MiningShip:
                    {
                        num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, num13, designIsBase: false);
                        num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, num13, designIsBase: false);
                        int num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                        if (num39 <= num38)
                        {
                            break;
                        }
                        ComponentList componentList = new ComponentList();
                        ComponentList componentList2 = new ComponentList();
                        ComponentList componentList3 = new ComponentList();
                        ComponentList componentList4 = new ComponentList();
                        ComponentList componentList5 = new ComponentList();
                        ComponentList componentList6 = new ComponentList();
                        ComponentList componentList7 = new ComponentList();
                        ComponentList componentList8 = new ComponentList();
                        ComponentList componentList9 = new ComponentList();
                        ComponentList componentList10 = new ComponentList();
                        ComponentList componentList11 = new ComponentList();
                        ComponentList componentList12 = new ComponentList();
                        int num40 = 0;
                        int num41 = 0;
                        int num42 = 0;
                        int num43 = 0;
                        int num44 = 0;
                        int num45 = 0;
                        int num46 = 0;
                        int num47 = 0;
                        int num48 = 0;
                        int num49 = 0;
                        int num50 = 0;
                        int num51 = 0;
                        bool flag4 = false;
                        List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
                        list.Add(BuiltObjectSubRole.ColonyShip);
                        list.Add(BuiltObjectSubRole.PassengerShip);
                        List<BuiltObjectSubRole> list2 = list;
                        if (mostRecentDesign != null && componentImprovement3 != null)
                        {
                            float num52 = mostRecentDesign.WarpSpeed;
                            float num53 = componentImprovement3.Value1;
                            if (num52 <= 0f || num53 / num52 >= 2f)
                            {
                                flag4 = true;
                            }
                        }
                        else if (mostRecentDesign == null && componentImprovement3 != null && list2.Contains(designSpec.SubRole))
                        {
                            flag4 = true;
                        }
                        double num54 = 0.25;
                        double num55 = 0.33;
                        double num56 = 0.25;
                        double num57 = 0.33;
                        double num58 = 0.5;
                        double num59 = 0.5;
                        int num60 = 1;
                        if (flag4)
                        {
                            num54 = 0.6;
                            num55 = 0.5;
                            num56 = 1.0;
                            num57 = 0.7;
                            num59 = 0.7;
                            num60 = 2;
                        }
                        if (design.Role == BuiltObjectRole.Military)
                        {
                            for (int num61 = 0; num61 < design.Components.Count; num61++)
                            {
                                if (design.Components[num61].Type == ComponentType.ExtractorGasExtractor)
                                {
                                    int num62 = Math.Max(1, (int)((double)num10 * num58));
                                    if (componentList10.Count < num62)
                                    {
                                        componentList10.Add(design.Components[num61]);
                                        num49 += design.Components[num61].Size;
                                    }
                                }
                                else if (design.Components[num61].Type == ComponentType.ExtractorMine)
                                {
                                    int num63 = Math.Max(1, (int)((double)num11 * num58));
                                    if (componentList11.Count < num63)
                                    {
                                        componentList11.Add(design.Components[num61]);
                                        num50 += design.Components[num61].Size;
                                    }
                                }
                                else if (design.Components[num61].Type == ComponentType.ExtractorLuxury)
                                {
                                    int num64 = Math.Max(1, (int)((double)num12 * num58));
                                    if (componentList12.Count < num64)
                                    {
                                        componentList12.Add(design.Components[num61]);
                                        num51 += design.Components[num61].Size;
                                    }
                                }
                                else if (design.Components[num61].Type == ComponentType.EngineMainThrust)
                                {
                                    int num65 = Math.Max(1, (int)((double)num7 * num54));
                                    if (componentList5.Count < num65)
                                    {
                                        componentList5.Add(design.Components[num61]);
                                        num44 += design.Components[num61].Size;
                                    }
                                }
                                else if (design.Components[num61].Type == ComponentType.FighterBay)
                                {
                                    if (design.SubRole == BuiltObjectSubRole.Carrier)
                                    {
                                        if (componentList4.Count <= 0)
                                        {
                                            componentList4.Add(design.Components[num61]);
                                            num43 += design.Components[num61].Size;
                                        }
                                        continue;
                                    }
                                    int num66 = Math.Max(1, (int)((double)num6 * num59));
                                    if (componentList4.Count < num66)
                                    {
                                        componentList4.Add(design.Components[num61]);
                                        num44 += design.Components[num61].Size;
                                    }
                                }
                                else if (design.Components[num61].Type == ComponentType.StorageTroop)
                                {
                                    if (design.SubRole != BuiltObjectSubRole.TroopTransport || componentList3.Count < num60)
                                    {
                                        componentList3.Add(design.Components[num61]);
                                        num42 += design.Components[num61].Size;
                                    }
                                }
                                else if (design.Components[num61].Category == ComponentCategoryType.Armor)
                                {
                                    componentList.Add(design.Components[num61]);
                                    num40 += design.Components[num61].Size;
                                }
                                else if (design.Components[num61].Type == ComponentType.Shields)
                                {
                                    int num67 = Math.Max(1, (int)((double)num8 * num56));
                                    if (componentList6.Count < num67)
                                    {
                                        componentList6.Add(design.Components[num61]);
                                        num45 += design.Components[num61].Size;
                                    }
                                }
                                else if (design.Components[num61].Type == ComponentType.StoragePassenger)
                                {
                                    int val = Math.Max(1, (int)((double)num9 * num57));
                                    val = Math.Min(val, num9 - 1);
                                    if (componentList7.Count < val)
                                    {
                                        componentList7.Add(design.Components[num61]);
                                        num46 += design.Components[num61].Size;
                                    }
                                }
                                else if (design.Components[num61].Category == ComponentCategoryType.EnergyCollector)
                                {
                                    componentList2.Add(design.Components[num61]);
                                    num41 += design.Components[num61].Size;
                                }
                                else if (design.Components[num61].Category == ComponentCategoryType.WeaponBeam || design.Components[num61].Category == ComponentCategoryType.WeaponGravity)
                                {
                                    int num68 = Math.Max(1, (int)((double)num4 * num55));
                                    if (componentList8.Count < num68)
                                    {
                                        componentList8.Add(design.Components[num61]);
                                        num47 += design.Components[num61].Size;
                                    }
                                }
                                else if (design.Components[num61].Category == ComponentCategoryType.WeaponTorpedo)
                                {
                                    int num69 = Math.Max(1, (int)((double)num5 * num55));
                                    if (componentList9.Count < num69)
                                    {
                                        componentList9.Add(design.Components[num61]);
                                        num48 += design.Components[num61].Size;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int num70 = 0; num70 < design.Components.Count; num70++)
                            {
                                if (design.Components[num70].Type == ComponentType.ExtractorGasExtractor)
                                {
                                    int num71 = Math.Max(1, (int)((double)num10 * num58));
                                    if (componentList10.Count < num71)
                                    {
                                        componentList10.Add(design.Components[num70]);
                                        num49 += design.Components[num70].Size;
                                    }
                                }
                                else if (design.Components[num70].Type == ComponentType.ExtractorMine)
                                {
                                    int num72 = Math.Max(1, (int)((double)num11 * num58));
                                    if (componentList11.Count < num72)
                                    {
                                        componentList11.Add(design.Components[num70]);
                                        num50 += design.Components[num70].Size;
                                    }
                                }
                                else if (design.Components[num70].Type == ComponentType.ExtractorLuxury)
                                {
                                    int num73 = Math.Max(1, (int)((double)num12 * num58));
                                    if (componentList12.Count < num73)
                                    {
                                        componentList12.Add(design.Components[num70]);
                                        num51 += design.Components[num70].Size;
                                    }
                                }
                                else if (design.Components[num70].Category == ComponentCategoryType.Armor)
                                {
                                    componentList.Add(design.Components[num70]);
                                    num40 += design.Components[num70].Size;
                                }
                                else if (design.Components[num70].Type == ComponentType.Shields)
                                {
                                    int num74 = Math.Max(1, (int)((double)num8 * num56));
                                    if (componentList6.Count < num74)
                                    {
                                        componentList6.Add(design.Components[num70]);
                                        num45 += design.Components[num70].Size;
                                    }
                                }
                                else if (design.Components[num70].Type == ComponentType.StoragePassenger)
                                {
                                    int num75 = Math.Max(1, (int)((double)num9 * num57));
                                    if (componentList7.Count < num75)
                                    {
                                        componentList7.Add(design.Components[num70]);
                                        num46 += design.Components[num70].Size;
                                    }
                                }
                                else if (design.Components[num70].Category == ComponentCategoryType.EnergyCollector)
                                {
                                    componentList2.Add(design.Components[num70]);
                                    num41 += design.Components[num70].Size;
                                }
                                else if (design.Components[num70].Type == ComponentType.EngineMainThrust)
                                {
                                    int num76 = Math.Max(1, (int)((double)num7 * num54));
                                    if (componentList5.Count < num76)
                                    {
                                        componentList5.Add(design.Components[num70]);
                                        num44 += design.Components[num70].Size;
                                    }
                                }
                                else if (design.Components[num70].Category == ComponentCategoryType.WeaponBeam || design.Components[num70].Category == ComponentCategoryType.WeaponGravity)
                                {
                                    int num77 = Math.Max(1, (int)((double)num4 * num55));
                                    if (componentList8.Count < num77)
                                    {
                                        componentList8.Add(design.Components[num70]);
                                        num47 += design.Components[num70].Size;
                                    }
                                }
                                else if (design.Components[num70].Category == ComponentCategoryType.WeaponTorpedo)
                                {
                                    int num78 = Math.Max(1, (int)((double)num5 * num55));
                                    if (componentList9.Count < num78)
                                    {
                                        componentList9.Add(design.Components[num70]);
                                        num48 += design.Components[num70].Size;
                                    }
                                }
                                else if (design.Components[num70].Type == ComponentType.FighterBay)
                                {
                                    if (design.SubRole == BuiltObjectSubRole.Carrier)
                                    {
                                        if (componentList4.Count <= 0)
                                        {
                                            componentList4.Add(design.Components[num70]);
                                            num43 += design.Components[num70].Size;
                                        }
                                        continue;
                                    }
                                    int num79 = Math.Max(1, (int)((double)num6 * num59));
                                    if (componentList4.Count < num79)
                                    {
                                        componentList4.Add(design.Components[num70]);
                                        num44 += design.Components[num70].Size;
                                    }
                                }
                                else if (design.Components[num70].Type == ComponentType.StorageTroop && componentList3.Count < num60)
                                {
                                    componentList3.Add(design.Components[num70]);
                                    num42 += design.Components[num70].Size;
                                }
                            }
                        }
                        if (componentList.Count > 1)
                        {
                            num40 -= componentList[0].Size;
                            componentList.RemoveAt(0);
                        }
                        if (num39 - (num40 + num41 + num42 + num43 + num46 + num44 + num45 + num47 + num48 + num49 + num50 + num51) > num38)
                        {
                            break;
                        }
                        if (num39 > num38 && componentList10.Count > 0)
                        {
                            for (int num80 = 0; num80 < componentList10.Count; num80++)
                            {
                                design.Components.Remove(componentList10[num80]);
                                num13 -= componentList10[num80].Size;
                                if (CheckDesignReactorCountDecreased(componentList10[num80], ref staticEnergyUsed, ref energyConsumed, ref recommendedReactorComponentCount, componentImprovement, componentImprovement3))
                                {
                                    num13 -= componentImprovement.ImprovedComponent.Size;
                                }
                                num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, num13, designIsBase: false);
                                num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, num13, designIsBase: false);
                                num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                                if (num39 <= num38)
                                {
                                    break;
                                }
                            }
                        }
                        num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                        if (num39 > num38 && componentList11.Count > 0)
                        {
                            for (int num81 = 0; num81 < componentList11.Count; num81++)
                            {
                                design.Components.Remove(componentList11[num81]);
                                num13 -= componentList11[num81].Size;
                                if (CheckDesignReactorCountDecreased(componentList11[num81], ref staticEnergyUsed, ref energyConsumed, ref recommendedReactorComponentCount, componentImprovement, componentImprovement3))
                                {
                                    num13 -= componentImprovement.ImprovedComponent.Size;
                                }
                                num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, num13, designIsBase: false);
                                num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, num13, designIsBase: false);
                                num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                                if (num39 <= num38)
                                {
                                    break;
                                }
                            }
                        }
                        num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                        if (num39 > num38 && componentList10.Count > 0)
                        {
                            for (int num82 = 0; num82 < componentList12.Count; num82++)
                            {
                                design.Components.Remove(componentList12[num82]);
                                num13 -= componentList12[num82].Size;
                                if (CheckDesignReactorCountDecreased(componentList12[num82], ref staticEnergyUsed, ref energyConsumed, ref recommendedReactorComponentCount, componentImprovement, componentImprovement3))
                                {
                                    num13 -= componentImprovement.ImprovedComponent.Size;
                                }
                                num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, num13, designIsBase: false);
                                num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, num13, designIsBase: false);
                                num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                                if (num39 <= num38)
                                {
                                    break;
                                }
                            }
                        }
                        num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                        if (num39 > num38 && componentList3.Count > 0)
                        {
                            for (int num83 = 0; num83 < componentList3.Count; num83++)
                            {
                                design.Components.Remove(componentList3[num83]);
                                num13 -= componentList3[num83].Size;
                                if (CheckDesignReactorCountDecreased(componentList3[num83], ref staticEnergyUsed, ref energyConsumed, ref recommendedReactorComponentCount, componentImprovement, componentImprovement3))
                                {
                                    num13 -= componentImprovement.ImprovedComponent.Size;
                                }
                                num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, num13, designIsBase: false);
                                num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, num13, designIsBase: false);
                                num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                                if (num39 <= num38)
                                {
                                    break;
                                }
                            }
                        }
                        num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                        if (num39 > num38 && componentList7.Count > 0)
                        {
                            for (int num84 = 0; num84 < componentList7.Count; num84++)
                            {
                                design.Components.Remove(componentList7[num84]);
                                num13 -= componentList7[num84].Size;
                                if (CheckDesignReactorCountDecreased(componentList7[num84], ref staticEnergyUsed, ref energyConsumed, ref recommendedReactorComponentCount, componentImprovement, componentImprovement3))
                                {
                                    num13 -= componentImprovement.ImprovedComponent.Size;
                                }
                                num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, num13, designIsBase: false);
                                num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, num13, designIsBase: false);
                                num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                                if (num39 <= num38)
                                {
                                    break;
                                }
                            }
                        }
                        num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                        if (num39 > num38 && componentList5.Count > 0)
                        {
                            for (int num85 = 0; num85 < componentList5.Count; num85++)
                            {
                                design.Components.Remove(componentList5[num85]);
                                num13 -= componentList5[num85].Size;
                                if (CheckDesignReactorCountDecreased(componentList5[num85], ref staticEnergyUsed, ref energyConsumed, ref recommendedReactorComponentCount, componentImprovement, componentImprovement3))
                                {
                                    num13 -= componentImprovement.ImprovedComponent.Size;
                                }
                                num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, num13, designIsBase: false);
                                num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, num13, designIsBase: false);
                                num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                                if (num39 <= num38)
                                {
                                    break;
                                }
                            }
                        }
                        num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                        if (num39 > num38 && componentList4.Count > 0)
                        {
                            for (int num86 = 0; num86 < componentList4.Count; num86++)
                            {
                                design.Components.Remove(componentList4[num86]);
                                num13 -= componentList4[num86].Size;
                                if (CheckDesignReactorCountDecreased(componentList4[num86], ref staticEnergyUsed, ref energyConsumed, ref recommendedReactorComponentCount, componentImprovement, componentImprovement3))
                                {
                                    num13 -= componentImprovement.ImprovedComponent.Size;
                                }
                                num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, num13, designIsBase: false);
                                num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, num13, designIsBase: false);
                                num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                                if (num39 <= num38)
                                {
                                    break;
                                }
                            }
                        }
                        num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                        if (num39 > num38 && componentList8.Count > 0)
                        {
                            for (int num87 = 0; num87 < componentList8.Count; num87++)
                            {
                                design.Components.Remove(componentList8[num87]);
                                num13 -= componentList8[num87].Size;
                                if (CheckDesignReactorCountDecreased(componentList8[num87], ref staticEnergyUsed, ref energyConsumed, ref recommendedReactorComponentCount, componentImprovement, componentImprovement3))
                                {
                                    num13 -= componentImprovement.ImprovedComponent.Size;
                                }
                                num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, num13, designIsBase: false);
                                num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, num13, designIsBase: false);
                                num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                                if (num39 <= num38)
                                {
                                    break;
                                }
                            }
                        }
                        num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                        if (num39 > num38 && componentList9.Count > 0)
                        {
                            for (int num88 = 0; num88 < componentList9.Count; num88++)
                            {
                                design.Components.Remove(componentList9[num88]);
                                num13 -= componentList9[num88].Size;
                                if (CheckDesignReactorCountDecreased(componentList9[num88], ref staticEnergyUsed, ref energyConsumed, ref recommendedReactorComponentCount, componentImprovement, componentImprovement3))
                                {
                                    num13 -= componentImprovement.ImprovedComponent.Size;
                                }
                                num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, num13, designIsBase: false);
                                num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, num13, designIsBase: false);
                                num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                                if (num39 <= num38)
                                {
                                    break;
                                }
                            }
                        }
                        num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                        if (num39 > num38 && componentList.Count > 0)
                        {
                            for (int num89 = 0; num89 < componentList.Count; num89++)
                            {
                                design.Components.Remove(componentList[num89]);
                                num13 -= componentList[num89].Size;
                                num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, num13, designIsBase: false);
                                num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, num13, designIsBase: false);
                                num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                                if (num39 <= num38)
                                {
                                    break;
                                }
                            }
                        }
                        num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                        if (num39 > num38 && componentList6.Count > 0)
                        {
                            for (int num90 = 0; num90 < componentList6.Count; num90++)
                            {
                                design.Components.Remove(componentList6[num90]);
                                num13 -= componentList6[num90].Size;
                                if (CheckDesignReactorCountDecreased(componentList6[num90], ref staticEnergyUsed, ref energyConsumed, ref recommendedReactorComponentCount, componentImprovement, componentImprovement3))
                                {
                                    num13 -= componentImprovement.ImprovedComponent.Size;
                                }
                                num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, num13, designIsBase: false);
                                num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, num13, designIsBase: false);
                                num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                                if (num39 <= num38)
                                {
                                    break;
                                }
                            }
                        }
                        num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                        if (num39 > num38 && componentList2.Count > 0)
                        {
                            for (int num91 = 0; num91 < componentList2.Count; num91++)
                            {
                                design.Components.Remove(componentList2[num91]);
                                num13 -= componentList2[num91].Size;
                                num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, num13, designIsBase: false);
                                num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, num13, designIsBase: false);
                                num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                                if (num39 <= num38)
                                {
                                    break;
                                }
                            }
                        }
                        num39 = num13 + num23 * componentImprovement5.ImprovedComponent.Size + num24 * componentImprovement6.ImprovedComponent.Size;
                        break;
                    }
            }
            staticEnergyUsed = Design.CalculateStaticEnergyUsage(design.Components);
            energyConsumed = 0.0;
            for (int num92 = 0; num92 < design.Components.Count; num92++)
            {
                ComponentImprovement component6 = Research.ResolveImprovedComponentValues(design.Components[num92]);
                energyConsumed += Design.DetermineComponentEnergyRequirementsExcludeHyperdrive(component6);
            }
            double num93 = energyConsumed + staticEnergyUsed;
            if (componentImprovement3 != null)
            {
                num93 = Math.Max(num93, componentImprovement3.Value2);
            }
            double num94 = Design.DetermineComponentEnergyOutput(componentImprovement);
            int num95 = (int)(0.99 + num93 / num94);
            for (int num96 = 0; num96 < num95; num96++)
            {
                design.Components.Add(componentImprovement.ImprovedComponent);
            }
            if (num2 > 0 && componentImprovement4 != null)
            {
                double num97 = 0.01;
                double num98 = 50.0;
                double num99 = (double)componentImprovement4.Value1 * num97 * num98;
                int val2 = (int)(0.99 + staticEnergyUsed / num99);
                val2 = Math.Max(num2, val2);
                for (int num100 = 0; num100 < val2; num100++)
                {
                    design.Components.Add(componentImprovement4.ImprovedComponent);
                }
            }
            if (design.SubRole == BuiltObjectSubRole.CapitalShip && DominantRace.AggressionLevel >= 100 && DominantRace.IntelligenceLevel >= 100)
            {
                ComponentImprovement componentImprovement18 = SelectPreferredSuperWeapon(techFocusCategories, techFocusTypes, mustBePlanetDestroyer: false);
                if (componentImprovement18 != null && !componentImprovement18.IsPlanetDestroyer)
                {
                    int num101 = MaximumConstructionSize(design.SubRole);
                    int num102 = componentImprovement18.ImprovedComponent.EnergyUsed;
                    if (componentImprovement18.ImprovedComponent.Category == ComponentCategoryType.WeaponSuperArea || componentImprovement18.ImprovedComponent.Category == ComponentCategoryType.WeaponSuperBeam || componentImprovement18.ImprovedComponent.Category == ComponentCategoryType.WeaponSuperTorpedo)
                    {
                        num102 += componentImprovement18.Value3;
                    }
                    if (componentImprovement != null && componentImprovement2 != null)
                    {
                        int num103 = num102 / componentImprovement.Value2 + 1;
                        int size = componentImprovement18.ImprovedComponent.Size;
                        size += componentImprovement.ImprovedComponent.Size * num103;
                        size += componentImprovement2.ImprovedComponent.Size * num103;
                        if (num101 >= num13 + size)
                        {
                            design.Components.Add(componentImprovement18.ImprovedComponent);
                            for (int num104 = 0; num104 < num103; num104++)
                            {
                                design.Components.Add(componentImprovement.ImprovedComponent);
                            }
                            for (int num105 = 0; num105 < num103; num105++)
                            {
                                design.Components.Add(componentImprovement2.ImprovedComponent);
                            }
                        }
                    }
                }
            }
            num23 = _Galaxy.DetermineHabModulesRequired(componentImprovement5, design);
            num24 = _Galaxy.DetermineLifeSupportRequired(componentImprovement6, design);
            for (int num106 = 0; num106 < num23; num106++)
            {
                design.Components.Add(componentImprovement5.ImprovedComponent);
            }
            for (int num107 = 0; num107 < num24; num107++)
            {
                design.Components.Add(componentImprovement6.ImprovedComponent);
            }
            return design;
        }

        private bool CheckDesignReactorCountDecreased(Component removedComponent, ref double staticEnergyUsed, ref double energyConsumed, ref int recommendedReactorComponentCount, ComponentImprovement reactorComponent, ComponentImprovement hyperdriveComponent)
        {
            double num = removedComponent.EnergyUsed;
            ComponentImprovement component = Research.ResolveImprovedComponentValues(removedComponent);
            double num2 = Design.DetermineComponentEnergyRequirementsExcludeHyperdrive(component);
            staticEnergyUsed -= num;
            energyConsumed -= num2;
            double num3 = energyConsumed + staticEnergyUsed;
            if (hyperdriveComponent != null)
            {
                num3 = Math.Max(num3, hyperdriveComponent.Value2);
            }
            double num4 = Design.DetermineComponentEnergyOutput(reactorComponent);
            int num5 = (int)(0.99 + num3 / num4);
            if (recommendedReactorComponentCount > num5)
            {
                recommendedReactorComponentCount = num5;
                return true;
            }
            return false;
        }

        public void ReviewUnpersistedColonyData()
        {
            if (Colonies == null)
            {
                return;
            }
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat != null)
                {
                    habitat.RecalculateCriticalResourceSupplyBonuses();
                    habitat.ReviewPlanetaryFacilities(habitat.Empire);
                }
            }
        }

        public void ReviewBuiltObjectWeaponsComponentValues()
        {
            if (Research != null)
            {
                Research.Update(DominantRace);
            }
            if (BuiltObjects != null)
            {
                for (int i = 0; i < BuiltObjects.Count; i++)
                {
                    BuiltObjects[i].ReviewWeaponsComponentValues();
                }
            }
            if (PrivateBuiltObjects != null)
            {
                for (int j = 0; j < PrivateBuiltObjects.Count; j++)
                {
                    PrivateBuiltObjects[j].ReviewWeaponsComponentValues();
                }
            }
        }

        public void ExtendLatestDesignsWithNewSubRoles()
        {
            Array values = Enum.GetValues(typeof(BuiltObjectSubRole));
            if (_LatestDesigns.Count < values.Length)
            {
                for (int i = _LatestDesigns.Count; i < values.Length; i++)
                {
                    Design item = _Designs.FindNewestCanBuildFullEvaluate((BuiltObjectSubRole)i);
                    _LatestDesigns.Add(item);
                }
            }
        }

        public void ReviewLatestDesigns()
        {
            for (int i = 0; i < _DesignSpecifications.Count; i++)
            {
                DesignSpecification designSpecification = _DesignSpecifications[i];
                Design design = _Designs.FindNewestCanBuildFullEvaluate(designSpecification.SubRole, null, includePlanetDestroyers: false);
                if (design != null)
                {
                    _LatestDesigns[(int)design.SubRole] = design;
                }
                else
                {
                    _LatestDesigns[(int)designSpecification.SubRole] = null;
                }
            }
        }

        public bool CheckDesignSubRoleShouldBeUpgraded(BuiltObjectSubRole subRole)
        {
            bool result = true;
            if (Policy != null)
            {
                result = subRole switch
                {
                    BuiltObjectSubRole.Escort => Policy.DesignUpgradeEscort,
                    BuiltObjectSubRole.Frigate => Policy.DesignUpgradeFrigate,
                    BuiltObjectSubRole.Destroyer => Policy.DesignUpgradeDestroyer,
                    BuiltObjectSubRole.Cruiser => Policy.DesignUpgradeCruiser,
                    BuiltObjectSubRole.CapitalShip => Policy.DesignUpgradeCapitalShip,
                    BuiltObjectSubRole.TroopTransport => Policy.DesignUpgradeTroopTransport,
                    BuiltObjectSubRole.Carrier => Policy.DesignUpgradeCarrier,
                    BuiltObjectSubRole.ResupplyShip => Policy.DesignUpgradeResupplyShip,
                    BuiltObjectSubRole.ExplorationShip => Policy.DesignUpgradeExplorationShip,
                    BuiltObjectSubRole.ColonyShip => Policy.DesignUpgradeColonyShip,
                    BuiltObjectSubRole.ConstructionShip => Policy.DesignUpgradeConstructionShip,
                    BuiltObjectSubRole.SmallSpacePort => Policy.DesignUpgradeSmallSpacePort,
                    BuiltObjectSubRole.MediumSpacePort => Policy.DesignUpgradeMediumSpacePort,
                    BuiltObjectSubRole.LargeSpacePort => Policy.DesignUpgradeLargeSpacePort,
                    BuiltObjectSubRole.ResortBase => Policy.DesignUpgradeResortBase,
                    BuiltObjectSubRole.GenericBase => Policy.DesignUpgradeGenericBase,
                    BuiltObjectSubRole.EnergyResearchStation => Policy.DesignUpgradeEnergyResearchStation,
                    BuiltObjectSubRole.WeaponsResearchStation => Policy.DesignUpgradeWeaponsResearchStation,
                    BuiltObjectSubRole.HighTechResearchStation => Policy.DesignUpgradeHighTechResearchStation,
                    BuiltObjectSubRole.MonitoringStation => Policy.DesignUpgradeMonitoringStation,
                    BuiltObjectSubRole.DefensiveBase => Policy.DesignUpgradeDefensiveBase,
                    BuiltObjectSubRole.SmallFreighter => Policy.DesignUpgradeSmallFreighter,
                    BuiltObjectSubRole.MediumFreighter => Policy.DesignUpgradeMediumFreighter,
                    BuiltObjectSubRole.LargeFreighter => Policy.DesignUpgradeLargeFreighter,
                    BuiltObjectSubRole.PassengerShip => Policy.DesignUpgradePassengerShip,
                    BuiltObjectSubRole.GasMiningShip => Policy.DesignUpgradeGasMiningShip,
                    BuiltObjectSubRole.MiningShip => Policy.DesignUpgradeMiningShip,
                    BuiltObjectSubRole.GasMiningStation => Policy.DesignUpgradeGasMiningStation,
                    BuiltObjectSubRole.MiningStation => Policy.DesignUpgradeMiningStation,
                    _ => true,
                };
            }
            return result;
        }

        public void SetDesignSubRoleShouldBeUpgraded(BuiltObjectSubRole subRole, bool upgrade)
        {
            if (Policy != null)
            {
                switch (subRole)
                {
                    case BuiltObjectSubRole.Escort:
                        Policy.DesignUpgradeEscort = upgrade;
                        break;
                    case BuiltObjectSubRole.Frigate:
                        Policy.DesignUpgradeFrigate = upgrade;
                        break;
                    case BuiltObjectSubRole.Destroyer:
                        Policy.DesignUpgradeDestroyer = upgrade;
                        break;
                    case BuiltObjectSubRole.Cruiser:
                        Policy.DesignUpgradeCruiser = upgrade;
                        break;
                    case BuiltObjectSubRole.CapitalShip:
                        Policy.DesignUpgradeCapitalShip = upgrade;
                        break;
                    case BuiltObjectSubRole.TroopTransport:
                        Policy.DesignUpgradeTroopTransport = upgrade;
                        break;
                    case BuiltObjectSubRole.Carrier:
                        Policy.DesignUpgradeCarrier = upgrade;
                        break;
                    case BuiltObjectSubRole.ResupplyShip:
                        Policy.DesignUpgradeResupplyShip = upgrade;
                        break;
                    case BuiltObjectSubRole.ExplorationShip:
                        Policy.DesignUpgradeExplorationShip = upgrade;
                        break;
                    case BuiltObjectSubRole.ColonyShip:
                        Policy.DesignUpgradeColonyShip = upgrade;
                        break;
                    case BuiltObjectSubRole.ConstructionShip:
                        Policy.DesignUpgradeConstructionShip = upgrade;
                        break;
                    case BuiltObjectSubRole.SmallSpacePort:
                        Policy.DesignUpgradeSmallSpacePort = upgrade;
                        break;
                    case BuiltObjectSubRole.MediumSpacePort:
                        Policy.DesignUpgradeMediumSpacePort = upgrade;
                        break;
                    case BuiltObjectSubRole.LargeSpacePort:
                        Policy.DesignUpgradeLargeSpacePort = upgrade;
                        break;
                    case BuiltObjectSubRole.ResortBase:
                        Policy.DesignUpgradeResortBase = upgrade;
                        break;
                    case BuiltObjectSubRole.GenericBase:
                        Policy.DesignUpgradeGenericBase = upgrade;
                        break;
                    case BuiltObjectSubRole.EnergyResearchStation:
                        Policy.DesignUpgradeEnergyResearchStation = upgrade;
                        break;
                    case BuiltObjectSubRole.WeaponsResearchStation:
                        Policy.DesignUpgradeWeaponsResearchStation = upgrade;
                        break;
                    case BuiltObjectSubRole.HighTechResearchStation:
                        Policy.DesignUpgradeHighTechResearchStation = upgrade;
                        break;
                    case BuiltObjectSubRole.MonitoringStation:
                        Policy.DesignUpgradeMonitoringStation = upgrade;
                        break;
                    case BuiltObjectSubRole.DefensiveBase:
                        Policy.DesignUpgradeDefensiveBase = upgrade;
                        break;
                    case BuiltObjectSubRole.SmallFreighter:
                        Policy.DesignUpgradeSmallFreighter = upgrade;
                        break;
                    case BuiltObjectSubRole.MediumFreighter:
                        Policy.DesignUpgradeMediumFreighter = upgrade;
                        break;
                    case BuiltObjectSubRole.LargeFreighter:
                        Policy.DesignUpgradeLargeFreighter = upgrade;
                        break;
                    case BuiltObjectSubRole.PassengerShip:
                        Policy.DesignUpgradePassengerShip = upgrade;
                        break;
                    case BuiltObjectSubRole.GasMiningShip:
                        Policy.DesignUpgradeGasMiningShip = upgrade;
                        break;
                    case BuiltObjectSubRole.MiningShip:
                        Policy.DesignUpgradeMiningShip = upgrade;
                        break;
                    case BuiltObjectSubRole.GasMiningStation:
                        Policy.DesignUpgradeGasMiningStation = upgrade;
                        break;
                    case BuiltObjectSubRole.MiningStation:
                        Policy.DesignUpgradeMiningStation = upgrade;
                        break;
                }
            }
        }

        private void DetermineTechForUnbuildableOptimizedDesigns(out List<ComponentCategoryType> categories, out List<ComponentType> types)
        {
            categories = new List<ComponentCategoryType>();
            types = new List<ComponentType>();
            DesignList designList = Designs.ResolveOptimizedDesigns();
            if (designList != null && designList.Count > 0)
            {
                DesignList unbuildableNonObsoleteDesigns = designList.GetUnbuildableNonObsoleteDesigns(this);
                if (unbuildableNonObsoleteDesigns != null && unbuildableNonObsoleteDesigns.Count > 0)
                {
                    ComponentList componentList = unbuildableNonObsoleteDesigns.DetermineUniqueUnbuildableComponents(this);
                    if (componentList != null && componentList.Count > 0)
                    {
                        for (int i = 0; i < componentList.Count; i++)
                        {
                            if (!types.Contains(componentList[i].Type))
                            {
                                types.Add(componentList[i].Type);
                            }
                        }
                    }
                }
            }
            for (int j = 0; j < types.Count; j++)
            {
                ComponentCategoryType category = ComponentCategoryType.Undefined;
                ComponentType type = ComponentType.Undefined;
                ResearchSystem.DetermineTechCategoryType(types[j], out category, out type);
                if (category != 0 && !categories.Contains(category))
                {
                    categories.Add(category);
                }
            }
        }

        public void CreateNewDesigns(long designDate)
        {
            CreateNewDesigns(designDate, forceUpdate: false);
        }

        public void CreateNewDesigns(long designDate, bool forceUpdate)
        {
            BaconEmpire.CreateNewDesigns(this, designDate, forceUpdate);
        }

        public void ReviewRemoveObsoleteDesignsForSubRole(BuiltObjectSubRole subRole, Design designToExclude, bool removeManualDesigns)
        {
            DesignList designList = new DesignList();
            for (int i = 0; i < Designs.Count; i++)
            {
                Design design = Designs[i];
                if (design == null || design.SubRole != subRole || (designToExclude != null && design == designToExclude))
                {
                    continue;
                }
                bool flag = false;
                if (design.IsManuallyCreated && design.OptimizedDesign == 0)
                {
                    flag = true;
                }
                if (removeManualDesigns || !flag)
                {
                    switch (design.SubRole)
                    {
                        case BuiltObjectSubRole.GenericBase:
                        case BuiltObjectSubRole.EnergyResearchStation:
                        case BuiltObjectSubRole.WeaponsResearchStation:
                        case BuiltObjectSubRole.HighTechResearchStation:
                        case BuiltObjectSubRole.MonitoringStation:
                        case BuiltObjectSubRole.DefensiveBase:
                            _ = Capital;
                            break;
                    }
                    design.IsObsolete = true;
                    if (!CheckDesignInUse(design))
                    {
                        designList.Add(design);
                    }
                }
            }
            for (int j = 0; j < designList.Count; j++)
            {
                Designs.Remove(designList[j]);
            }
        }

        private bool CheckDesignInUse(Design design)
        {
            for (int i = 0; i < BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = BuiltObjects[i];
                if (builtObject != null && !builtObject.HasBeenDestroyed && (builtObject.Design == design || builtObject.RetrofitDesign == design))
                {
                    return true;
                }
            }
            for (int j = 0; j < PrivateBuiltObjects.Count; j++)
            {
                BuiltObject builtObject2 = PrivateBuiltObjects[j];
                if (builtObject2 != null && !builtObject2.HasBeenDestroyed && (builtObject2.Design == design || builtObject2.RetrofitDesign == design))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IncludeOptionalComponent(DesignSpecificationComponentRule rule, List<ComponentCategoryType> techFocusCategories, List<ComponentType> techFocusTypes)
        {
            bool result = false;
            switch (rule.ComponentRuleType)
            {
                case DesignSpecificationComponentRuleType.MustHave:
                    result = true;
                    break;
                case DesignSpecificationComponentRuleType.MustNotHave:
                    result = false;
                    break;
                case DesignSpecificationComponentRuleType.ShouldHave:
                    if (techFocusCategories.Contains(rule.ComponentCategory) || techFocusTypes.Contains(rule.ComponentType))
                    {
                        result = true;
                    }
                    else if ((rule.ComponentCategory == ComponentCategoryType.WeaponArea || rule.ComponentCategory == ComponentCategoryType.WeaponBeam || rule.ComponentCategory == ComponentCategoryType.WeaponTorpedo) && DominantRace.AggressionLevel > 110)
                    {
                        result = true;
                    }
                    else if (DominantRace.IntelligenceLevel > 105)
                    {
                        result = true;
                    }
                    break;
                case DesignSpecificationComponentRuleType.ShouldNotHave:
                    if (techFocusCategories.Contains(rule.ComponentCategory) || techFocusTypes.Contains(rule.ComponentType))
                    {
                        result = true;
                    }
                    else if (DominantRace.IntelligenceLevel > 120)
                    {
                        result = true;
                    }
                    break;
            }
            return result;
        }

        public int AdjustComponentAmount(DesignSpecification designSpec, DesignSpecificationComponentRule rule)
        {
            return rule.Amount;
        }

        public DesignSpecification ObtainDesignSpec(BuiltObjectSubRole subRole)
        {
            DesignSpecification result = null;
            for (int i = 0; i < _DesignSpecifications.Count; i++)
            {
                DesignSpecification designSpecification = _DesignSpecifications[i];
                if (designSpecification.SubRole == subRole)
                {
                    result = designSpecification;
                    break;
                }
            }
            return result;
        }

        public Design GenerateDesignFromSpec(DesignSpecification designSpec, double techAdvanceAmount)
        {
            Design design = null;
            BuiltObjectFleeWhen fleeWhen = BuiltObjectFleeWhen.Shields20;
            BuiltObjectStance stance = BuiltObjectStance.AttackEnemies;
            BuiltObjectFleeWhen fleeWhen2 = BuiltObjectFleeWhen.EnemyMilitarySighted;
            BuiltObjectStance stance2 = BuiltObjectStance.DoNotAttack;
            BuiltObjectFleeWhen fleeWhen3 = BuiltObjectFleeWhen.EnemyMilitarySighted;
            BuiltObjectStance stance3 = BuiltObjectStance.DoNotAttack;
            BuiltObjectFleeWhen fleeWhen4 = BuiltObjectFleeWhen.EnemyMilitarySighted;
            BuiltObjectStance stance4 = BuiltObjectStance.AttackIfAttacked;
            BuiltObjectFleeWhen fleeWhen5 = BuiltObjectFleeWhen.EnemyMilitarySighted;
            BuiltObjectStance stance5 = BuiltObjectStance.DoNotAttack;
            BuiltObjectFleeWhen fleeWhen6 = BuiltObjectFleeWhen.EnemyMilitarySighted;
            BuiltObjectStance stance6 = BuiltObjectStance.DoNotAttack;
            BuiltObjectFleeWhen fleeWhen7 = BuiltObjectFleeWhen.Shields50;
            BuiltObjectStance stance7 = BuiltObjectStance.AttackEnemies;
            if (designSpec != null)
            {
                Design previousDesign = null;
                if (_Designs != null)
                {
                    previousDesign = _Designs.FindNewestCanBuild(designSpec.SubRole);
                }
                string text = Galaxy.ResolveDescription(designSpec.SubRole);
                string empty = string.Empty;
                empty = ((DominantRace == null) ? text : (DominantRace.Name + " " + text));
                design = new Design(empty);
                design.Role = designSpec.Role;
                design.SubRole = designSpec.SubRole;
                design.ImageScalingType = designSpec.ImageScalingMode;
                design.ImageScalingFactor = designSpec.ImageScalingFactor;
                design = PlaceComponentsOnDesign(design, designSpec, null, techAdvanceAmount);
                switch (designSpec.SubRole)
                {
                    case BuiltObjectSubRole.SmallSpacePort:
                    case BuiltObjectSubRole.MediumSpacePort:
                    case BuiltObjectSubRole.LargeSpacePort:
                    case BuiltObjectSubRole.GenericBase:
                    case BuiltObjectSubRole.EnergyResearchStation:
                    case BuiltObjectSubRole.WeaponsResearchStation:
                    case BuiltObjectSubRole.HighTechResearchStation:
                    case BuiltObjectSubRole.MonitoringStation:
                    case BuiltObjectSubRole.DefensiveBase:
                        design.Stance = BuiltObjectStance.AttackEnemies;
                        design.FleeWhen = BuiltObjectFleeWhen.Never;
                        design.TacticsStrongerShips = BattleTactics.PointBlank;
                        design.TacticsWeakerShips = BattleTactics.PointBlank;
                        design.TacticsInvasion = InvasionTactics.DoNotInvade;
                        break;
                    case BuiltObjectSubRole.GasMiningStation:
                    case BuiltObjectSubRole.MiningStation:
                        design.Stance = BuiltObjectStance.AttackIfAttacked;
                        design.FleeWhen = BuiltObjectFleeWhen.Never;
                        design.TacticsStrongerShips = BattleTactics.PointBlank;
                        design.TacticsWeakerShips = BattleTactics.PointBlank;
                        design.TacticsInvasion = InvasionTactics.DoNotInvade;
                        break;
                    case BuiltObjectSubRole.Escort:
                    case BuiltObjectSubRole.Frigate:
                    case BuiltObjectSubRole.Destroyer:
                    case BuiltObjectSubRole.Cruiser:
                    case BuiltObjectSubRole.CapitalShip:
                        design.Stance = stance;
                        design.FleeWhen = fleeWhen;
                        design.TacticsStrongerShips = BattleTactics.Standoff;
                        design.TacticsWeakerShips = BattleTactics.AllWeapons;
                        design.TacticsInvasion = InvasionTactics.InvadeWhenClear;
                        break;
                    case BuiltObjectSubRole.Carrier:
                        design.Stance = stance7;
                        design.FleeWhen = fleeWhen7;
                        design.TacticsStrongerShips = BattleTactics.Evade;
                        design.TacticsWeakerShips = BattleTactics.AllWeapons;
                        design.TacticsInvasion = InvasionTactics.InvadeWhenClear;
                        break;
                    case BuiltObjectSubRole.SmallFreighter:
                    case BuiltObjectSubRole.MediumFreighter:
                    case BuiltObjectSubRole.LargeFreighter:
                        design.Stance = stance2;
                        design.FleeWhen = fleeWhen2;
                        design.TacticsStrongerShips = BattleTactics.Evade;
                        design.TacticsWeakerShips = BattleTactics.Evade;
                        design.TacticsInvasion = InvasionTactics.DoNotInvade;
                        break;
                    case BuiltObjectSubRole.ExplorationShip:
                        design.Stance = stance4;
                        design.FleeWhen = fleeWhen4;
                        design.TacticsStrongerShips = BattleTactics.Evade;
                        design.TacticsWeakerShips = BattleTactics.Evade;
                        design.TacticsInvasion = InvasionTactics.DoNotInvade;
                        break;
                    case BuiltObjectSubRole.ColonyShip:
                        design.Stance = stance5;
                        design.FleeWhen = fleeWhen5;
                        design.TacticsStrongerShips = BattleTactics.Evade;
                        design.TacticsWeakerShips = BattleTactics.Evade;
                        design.TacticsInvasion = InvasionTactics.DoNotInvade;
                        break;
                    case BuiltObjectSubRole.ConstructionShip:
                        design.Stance = stance6;
                        design.FleeWhen = fleeWhen6;
                        design.TacticsStrongerShips = BattleTactics.Evade;
                        design.TacticsWeakerShips = BattleTactics.Evade;
                        design.TacticsInvasion = InvasionTactics.DoNotInvade;
                        break;
                    case BuiltObjectSubRole.GasMiningShip:
                    case BuiltObjectSubRole.MiningShip:
                        design.Stance = stance3;
                        design.FleeWhen = fleeWhen3;
                        design.TacticsStrongerShips = BattleTactics.Evade;
                        design.TacticsWeakerShips = BattleTactics.Evade;
                        design.TacticsInvasion = InvasionTactics.DoNotInvade;
                        break;
                    case BuiltObjectSubRole.TroopTransport:
                        design.Stance = stance7;
                        design.FleeWhen = fleeWhen7;
                        design.TacticsStrongerShips = BattleTactics.Evade;
                        design.TacticsWeakerShips = BattleTactics.AllWeapons;
                        design.TacticsInvasion = InvasionTactics.InvadeImmediately;
                        break;
                    case BuiltObjectSubRole.ResupplyShip:
                        design.Stance = stance7;
                        design.FleeWhen = fleeWhen7;
                        design.TacticsStrongerShips = BattleTactics.Evade;
                        design.TacticsWeakerShips = BattleTactics.AllWeapons;
                        design.TacticsInvasion = InvasionTactics.DoNotInvade;
                        break;
                    default:
                        design.Stance = BuiltObjectStance.DoNotAttack;
                        design.FleeWhen = BuiltObjectFleeWhen.Attacked;
                        design.TacticsStrongerShips = BattleTactics.Standoff;
                        design.TacticsWeakerShips = BattleTactics.AllWeapons;
                        design.TacticsInvasion = InvasionTactics.DoNotInvade;
                        break;
                }
                int num = DesignPictureFamilyIndex;
                if (DominantRace != null && PirateEmpireBaseHabitat != null)
                {
                    num = DominantRace.DesignPictureFamilyIndexPirates;
                    if (num < 0)
                    {
                        num = DominantRace.DesignPictureFamilyIndex;
                    }
                }
                empty = (design.Name = GenerateDesignName(designSpec.SubRole, previousDesign));
                design.DateCreated = _Galaxy.CurrentStarDate;
                design.Empire = this;
                design.PictureRef = ShipImageHelper.StandardShipImageStartIndex + num * ShipImageHelper.ShipSetImageCount + (int)(Galaxy.ResolveLegacySubRole(designSpec.SubRole) - 1);
                design.Role = designSpec.Role;
                design.SubRole = designSpec.SubRole;
                design.ReDefine();
            }
            return design;
        }

        public void SetTaxRate(Galaxy galaxy, double taxRate)
        {
            _TaxRate = taxRate;
            double num = 0.0;
            for (int i = 0; i < galaxy.Empires.Count; i++)
            {
                Empire empire = galaxy.Empires[i];
                num += empire.TaxRate;
            }
            galaxy.AverageTaxRate = num / (double)galaxy.Empires.Count;
        }

        private string GenerateAutomationMessageColonyFacility(Habitat colony, PlanetaryFacilityDefinition facility)
        {
            return GenerateAutomationMessageColonyFacility(colony, facility, haveFunds: true);
        }

        private string GenerateAutomationMessageColonyFacility(Habitat colony, PlanetaryFacilityDefinition facility, bool haveFunds)
        {
            Habitat habitat = Galaxy.DetermineHabitatSystemStar(colony);
            string text = TextResolver.GetText("advisors");
            string text2 = string.Empty;
            switch (facility.Type)
            {
                case PlanetaryFacilityType.CloningFacility:
                    text = TextResolver.GetText("military planners");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Cloning");
                    break;
                case PlanetaryFacilityType.RoboticTroopFoundry:
                    text = TextResolver.GetText("military planners");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Robotic");
                    break;
                case PlanetaryFacilityType.TroopTrainingCenter:
                    text = TextResolver.GetText("military planners");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Troop Academy");
                    break;
                case PlanetaryFacilityType.RegionalCapital:
                    text = TextResolver.GetText("bureaucrats");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Regional Capital");
                    break;
                case PlanetaryFacilityType.FortifiedBunker:
                    text = TextResolver.GetText("military planners");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Bunker");
                    break;
                case PlanetaryFacilityType.ArmoredFactory:
                    text = TextResolver.GetText("military planners");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Armored Factory");
                    break;
                case PlanetaryFacilityType.SpyAcademy:
                    text = TextResolver.GetText("advisors");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Spy Academy");
                    break;
                case PlanetaryFacilityType.ScienceAcademy:
                    text = TextResolver.GetText("advisors");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Science Academy");
                    break;
                case PlanetaryFacilityType.NavalAcademy:
                    text = TextResolver.GetText("military planners");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Naval Academy");
                    break;
                case PlanetaryFacilityType.MilitaryAcademy:
                    text = TextResolver.GetText("military planners");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Military Academy");
                    break;
                case PlanetaryFacilityType.IonCannon:
                    text = TextResolver.GetText("military planners");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Ion Cannon");
                    break;
                case PlanetaryFacilityType.PlanetaryShield:
                    text = TextResolver.GetText("military planners");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Shield");
                    break;
                case PlanetaryFacilityType.PirateBase:
                    text = TextResolver.GetText("pirate council");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Pirate Base");
                    break;
                case PlanetaryFacilityType.PirateFortress:
                    text = TextResolver.GetText("pirate council");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Pirate Fortress");
                    break;
                case PlanetaryFacilityType.PirateCriminalNetwork:
                    text = TextResolver.GetText("pirate council");
                    text2 = TextResolver.GetText("Planetary Facility Explanation Pirate Criminal Network");
                    break;
                case PlanetaryFacilityType.Wonder:
                    text2 = facility.Description;
                    break;
            }
            double num = Galaxy.CalculatePlanetaryFacilityCost(facility, this);
            string empty = string.Empty;
            if (haveFunds)
            {
                return string.Format(TextResolver.GetText("Planetary Facility Build Recommendation With Maintenance"), text, facility.Name, colony.Name, habitat.Name, text2, num.ToString("###,###,###,##0"), facility.Maintenance.ToString("###,###,###,##0"));
            }
            return string.Format(TextResolver.GetText("Planetary Facility Build Recommendation Need Funds"), text, facility.Name, colony.Name, habitat.Name, text2, num.ToString("###,###,###,##0"));
        }

        private string GenerateAutomationMessageDefensiveBase(Habitat colony, Design baseDesign)
        {
            Habitat habitat = Galaxy.DetermineHabitatSystemStar(colony);
            return string.Format(arg2: baseDesign.CalculateCurrentPurchasePrice(_Galaxy).ToString("###,###,###,##0"), format: TextResolver.GetText("Automation Defensive Base"), arg0: colony.Name, arg1: habitat.Name);
        }

        private string GenerateAutomationMessageColonization(Habitat newColony, BuiltObject colonyShip, Habitat colonyShipBuildLocation)
        {
            Habitat habitat = Galaxy.DetermineHabitatSystemStar(newColony);
            string result = string.Empty;
            if (colonyShip != null)
            {
                result = string.Format(TextResolver.GetText("Automation Colonization Existing Ship"), Galaxy.ResolveDescription(newColony.Type).ToLower(CultureInfo.InvariantCulture), Galaxy.ResolveDescription(newColony.Category).ToLower(CultureInfo.InvariantCulture), newColony.Name, habitat.Name, colonyShip.Name);
            }
            else if (colonyShipBuildLocation != null)
            {
                Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(colonyShipBuildLocation);
                result = string.Format(TextResolver.GetText("Automation Colonization New Ship"), Galaxy.ResolveDescription(newColony.Type).ToLower(CultureInfo.InvariantCulture), Galaxy.ResolveDescription(newColony.Category).ToLower(CultureInfo.InvariantCulture), newColony.Name, habitat.Name, colonyShipBuildLocation.Name, habitat2.Name);
            }
            return result;
        }

        private string GenerateAutomationMessageConstruction(BuiltObject builtObject, Habitat habitat, double cost)
        {
            string text = string.Empty;
            string text2 = string.Empty;
            string text3 = string.Empty;
            string text4 = string.Empty;
            if (habitat != null)
            {
                Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                text3 = habitat.Name;
                text = Galaxy.ResolveDescription(habitat.Type);
                text2 = Galaxy.ResolveDescription(habitat.Category);
                text4 = habitat2.Name;
            }
            return string.Format(TextResolver.GetText("Automation Construction Colony"), Galaxy.ResolveDescription(builtObject.SubRole), builtObject.Design.Name, cost.ToString("###,###,###,##0"), text, text2, text3, text4);
        }

        private string GenerateAutomationMessageConstruction(ForceStructureProjectionList newForces, double cost)
        {
            string text = string.Empty;
            foreach (ForceStructureProjection newForce in newForces)
            {
                if (newForce.Amount > 0)
                {
                    text = text + newForce.Amount + " × ";
                    text = text + Galaxy.ResolveDescription(newForce.SubRole) + ", ";
                }
            }
            if (text.Length > 1)
            {
                text = text.Substring(0, text.Length - 2);
            }
            return string.Format(TextResolver.GetText("Automation Construction Forces"), text, cost.ToString("###,###,###,##0"));
        }

        private string GenerateAutomationMessageAttackForcesInOurSystem(Habitat systemStar, Empire otherEmpire)
        {
            return GenerateAutomationMessageAttackForcesInOurSystem(systemStar, otherEmpire, null, null);
        }

        private string GenerateAutomationMessageAttackForcesInOurSystem(Habitat systemStar, Empire otherEmpire, ShipGroup enemyFleet, ShipGroup ourFleet)
        {
            return string.Format(TextResolver.GetText("Automation Attack Forces In Our System"), otherEmpire.Name, systemStar.Name);
        }

        private string GenerateAutomationMessageAgentMission(IntelligenceMission mission)
        {
            string arg = string.Empty;
            if (mission.TargetEmpire != null)
            {
                arg = mission.TargetEmpire.Name;
            }
            return string.Format(TextResolver.GetText("Automation Intelligence Mission"), mission.Agent.Name, arg, Galaxy.ResolveDescription(mission, this));
        }

        private string GenerateAutomationMessagePrepareAttack(Empire enemyEmpire, object waypoint, object target, ShipGroup attackFleet)
        {
            string text = string.Empty;
            string text2 = string.Empty;
            Habitat habitat = null;
            if (waypoint is Habitat)
            {
                Habitat habitat2 = (Habitat)waypoint;
                text = habitat2.Name;
                habitat = Galaxy.DetermineHabitatSystemStar((Habitat)waypoint);
                text2 = habitat.Name;
            }
            else if (waypoint is BuiltObject)
            {
                text = ((BuiltObject)waypoint).Name;
                habitat = ((BuiltObject)waypoint).NearestSystemStar;
                text2 = habitat.Name;
            }
            string text3 = string.Empty;
            string text4 = string.Empty;
            Habitat habitat3 = null;
            if (target is Habitat)
            {
                text3 = ((Habitat)target).Name;
                habitat3 = Galaxy.DetermineHabitatSystemStar((Habitat)target);
                text4 = habitat3.Name;
            }
            else if (target is BuiltObject)
            {
                text3 = ((BuiltObject)target).Name;
                habitat3 = ((BuiltObject)target).NearestSystemStar;
                text4 = habitat3.Name;
            }
            else if (target is ShipGroup)
            {
                text3 = ((ShipGroup)target).Name;
                if (((ShipGroup)target).LeadShip != null)
                {
                    habitat3 = ((ShipGroup)target).LeadShip.NearestSystemStar;
                    text4 = habitat3.Name;
                }
            }
            return string.Format(TextResolver.GetText("Automation Prepare Attack"), enemyEmpire.Name, attackFleet.Name, text, text2, text3, text4);
        }

        private string GenerateAutomationMessageRaid(Empire targetEmpire)
        {
            return string.Format(TextResolver.GetText("Automation Raid"), targetEmpire.Name);
        }

        private string GenerateAutomationMessageAskedWar(Empire requestingEmpire, Empire targetEmpire)
        {
            return string.Format(TextResolver.GetText("Automation Assist War"), requestingEmpire.Name, targetEmpire.Name);
        }

        private string GenerateAutomationMessageAskedTradeSanctions(Empire requestingEmpire, Empire targetEmpire)
        {
            return string.Format(TextResolver.GetText("Automation Assist Trade Sanctions"), requestingEmpire.Name, targetEmpire.Name);
        }

        private string GenerateAutomationMessageInvadeIndependent(Habitat habitat, ShipGroup invasionFleet)
        {
            Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
            string text = string.Empty;
            string text2 = string.Empty;
            string text3 = string.Empty;
            string text4 = string.Empty;
            string text5 = string.Empty;
            string text6 = string.Empty;
            if (habitat2 != null)
            {
                text = habitat2.Name;
            }
            if (habitat != null)
            {
                text4 = habitat.Name;
                text5 = Galaxy.ResolveDescription(habitat.Type);
                text6 = Galaxy.ResolveDescription(habitat.Category).ToLower(CultureInfo.InvariantCulture);
                if (habitat.Population != null && habitat.Population.DominantRace != null)
                {
                    text2 = habitat.Population.DominantRace.Name;
                }
            }
            if (invasionFleet != null)
            {
                text3 = invasionFleet.Name;
            }
            return string.Format(TextResolver.GetText("Automation Invade Independent"), text5, text6, text4, text, text2, text3);
        }

        private string GenerateAutomationMessageAttackEnemy(ShipGroup shipGroup, ShipGroup attackFleet)
        {
            string arg = string.Empty;
            if (shipGroup != null && shipGroup.Empire != null)
            {
                arg = shipGroup.Empire.Name;
            }
            return string.Format(TextResolver.GetText("Automation Attack Enemy Fleet"), shipGroup.Name, arg, attackFleet.Name);
        }

        private string GenerateAutomationMessageWaypointFleet(ShipGroup fleet, StellarObject waypoint, Empire enemyEmpire)
        {
            string text = "the ";
            if (fleet.Name.ToLower(CultureInfo.InvariantCulture).StartsWith("the"))
            {
                text = string.Empty;
            }
            Habitat habitat = null;
            if (fleet.LeadShip != null)
            {
                habitat = fleet.LeadShip.NearestSystemStar;
            }
            Habitat habitat2 = null;
            if (waypoint != null)
            {
                if (waypoint is Habitat)
                {
                    habitat2 = Galaxy.DetermineHabitatSystemStar((Habitat)waypoint);
                }
                else if (waypoint is BuiltObject)
                {
                    habitat2 = Galaxy.DetermineHabitatSystemStar(((BuiltObject)waypoint).NearestSystemStar);
                }
            }
            string text2 = "Our military advisors recommend that we ";
            string text3 = text2;
            text2 = text3 + "move " + text + fleet.Name + " ";
            if (habitat != null)
            {
                text2 = text2 + "from the " + habitat.Name + " system ";
            }
            text2 = text2 + "to " + waypoint.Name;
            if (habitat2 != null)
            {
                text2 = text2 + " in the " + habitat2.Name + " system";
            }
            text2 += ".\n\n";
            text2 = text2 + "This would put the fleet within strike range of our enemy, the " + enemyEmpire.Name + ".\n\n";
            return text2 + "Should we move this fleet?";
        }

        private string GenerateAutomationMessageRaidBase(BuiltObject target, ShipGroup attackFleet)
        {
            string result = string.Empty;
            if (target != null && attackFleet != null)
            {
                string text = string.Empty;
                if (target.ParentHabitat != null)
                {
                    Habitat habitat = Galaxy.DetermineHabitatSystemStar(target.ParentHabitat);
                    text = habitat.Name;
                }
                result = ((target.Empire == null) ? string.Format(TextResolver.GetText("Automation Raid Base"), target.Name, string.Empty, text, attackFleet.Name) : string.Format(TextResolver.GetText("Automation Raid Base"), target.Name, target.Empire.Name, text, attackFleet.Name));
            }
            return result;
        }

        private string GenerateAutomationMessageRaidColony(Habitat target, ShipGroup attackFleet)
        {
            string result = string.Empty;
            if (target != null && attackFleet != null)
            {
                Habitat habitat = Galaxy.DetermineHabitatSystemStar(target);
                string name = habitat.Name;
                result = ((target.Empire == _Galaxy.IndependentEmpire) ? string.Format(TextResolver.GetText("Automation Raid Independent Colony"), target.Name, name, attackFleet.Name) : ((target.Empire == null) ? string.Format(TextResolver.GetText("Automation Raid Colony"), target.Name, string.Empty, name, attackFleet.Name) : string.Format(TextResolver.GetText("Automation Raid Colony"), target.Name, target.Empire.Name, name, attackFleet.Name)));
            }
            return result;
        }

        private string GenerateAutomationMessageAttackPirateBase(BuiltObject pirateBase, ShipGroup attackFleet)
        {
            string text = string.Empty;
            if (pirateBase.ParentHabitat != null)
            {
                Habitat habitat = Galaxy.DetermineHabitatSystemStar(pirateBase.ParentHabitat);
                text = habitat.Name;
            }
            return string.Format(TextResolver.GetText("Automation Attack Pirate Base"), pirateBase.Name, pirateBase.Empire.Name, text, attackFleet.Name);
        }

        public string GenerateAutomationMessageAttackPirateFacility(Habitat colony, Empire pirateFaction, PlanetaryFacility pirateFacility)
        {
            string text = string.Empty;
            string text2 = string.Empty;
            if (colony != null)
            {
                text = colony.Name;
                text2 = Galaxy.DetermineHabitatSystemStar(colony).Name;
            }
            return string.Format(TextResolver.GetText("Automation Attack Pirate Facility"), text, text2, pirateFaction.Name, pirateFacility.Name);
        }

        private string GenerateAutomationMessagePiratesAttackPirates(BuiltObject attackTarget, ShipGroup attackFleet)
        {
            string text = string.Empty;
            if (attackTarget.ParentHabitat != null)
            {
                Habitat habitat = Galaxy.DetermineHabitatSystemStar(attackTarget.ParentHabitat);
                text = habitat.Name;
            }
            return string.Format(TextResolver.GetText("Automation Pirate Attack Pirate"), attackTarget.Empire.Name, attackTarget.Name, text, attackFleet.Name);
        }

        private string GenerateAutomationMessagePiratesAttackMission(EmpireActivity attackMission, ShipGroup attackFleet)
        {
            string text = string.Empty;
            string text2 = string.Empty;
            string text3 = string.Empty;
            string text4 = string.Empty;
            if (attackMission != null && attackMission.TargetEmpire != null && attackMission.RequestingEmpire != null && attackMission.Target != null)
            {
                text4 = attackMission.TargetEmpire.Name;
                text = attackMission.Target.Name;
                text3 = attackMission.RequestingEmpire.Name;
                if (attackMission.Target.ParentHabitat != null)
                {
                    Habitat habitat = Galaxy.DetermineHabitatSystemStar(attackMission.Target.ParentHabitat);
                    text2 = habitat.Name;
                }
            }
            return string.Format(TextResolver.GetText("Automation Pirate Attack Mission"), text3, text, text4, text2, attackFleet.Name);
        }

        private string GenerateAutomationMessagePiratesDefendMission(EmpireActivity defendMission, ShipGroup defendFleet)
        {
            string text = string.Empty;
            string text2 = string.Empty;
            string text3 = string.Empty;
            string text4 = string.Empty;
            if (defendMission != null && defendMission.TargetEmpire != null && defendMission.RequestingEmpire != null && defendMission.Target != null)
            {
                text4 = defendMission.TargetEmpire.Name;
                text = defendMission.Target.Name;
                text3 = defendMission.RequestingEmpire.Name;
                if (defendMission.Target.ParentHabitat != null)
                {
                    Habitat habitat = Galaxy.DetermineHabitatSystemStar(defendMission.Target.ParentHabitat);
                    text2 = habitat.Name;
                }
            }
            return string.Format(TextResolver.GetText("Automation Pirate Defend Mission"), text3, text, text4, text2, defendFleet.Name);
        }

        private string GenerateAutomationMessageAttackEnemy(BuiltObject builtObject, ShipGroup attackFleet)
        {
            return GenerateAutomationMessageAttackEnemy(builtObject, blockade: false, attackFleet);
        }

        private string GenerateAutomationMessageAttackEnemy(BuiltObject builtObject, bool blockade, ShipGroup attackFleet)
        {
            string text = string.Empty;
            if (builtObject.NearestSystemStar != null)
            {
                text = builtObject.NearestSystemStar.Name;
            }
            else
            {
                Habitat habitat = _Galaxy.FastFindNearestSystem(builtObject.Xpos, builtObject.Ypos);
                if (habitat != null)
                {
                    text = habitat.Name;
                }
            }
            string text2 = string.Empty;
            if (attackFleet != null)
            {
                text2 = attackFleet.Name;
            }
            string empty = string.Empty;
            string text3 = string.Empty;
            if (builtObject != null && builtObject.Empire != null)
            {
                text3 = builtObject.Empire.Name;
            }
            if (blockade)
            {
                return string.Format(TextResolver.GetText("Automation Blockade Enemy Base"), builtObject.Name, text3, text, text2);
            }
            return string.Format(TextResolver.GetText("Automation Attack Enemy Base"), builtObject.Name, text3, text, text2);
        }

        private string GenerateAutomationMessageAttackEnemy(Habitat habitat, ShipGroup attackFleet)
        {
            return GenerateAutomationMessageAttackEnemy(habitat, blockade: false, attackFleet);
        }

        private string GenerateAutomationMessageRequestLiftTradeSanctions(Empire targetEmpire, Empire friendEmpire)
        {
            return string.Format(TextResolver.GetText("Automation Request Lift Trade Sanctions"), friendEmpire.Name, targetEmpire.Name);
        }

        private string GenerateAutomationMessageRequestEndWar(Empire targetEmpire, Empire friendEmpire)
        {
            return string.Format(TextResolver.GetText("Automation Request End War"), friendEmpire.Name, targetEmpire.Name);
        }

        private string GenerateAutomationMessageRequestLeaveSystem(Empire empire, Habitat systemStar)
        {
            return string.Format(TextResolver.GetText("Automation Request Leave System"), empire.Name, systemStar.Name);
        }

        private string GenerateAutomationMessageMilitaryRefueling(Empire empire, bool refuel)
        {
            string empty = string.Empty;
            if (refuel)
            {
                return string.Format(TextResolver.GetText("Military Refueling Check Offer"), empire.Name);
            }
            return string.Format(TextResolver.GetText("Military Refueling Check Cancel"), empire.Name);
        }

        private string GenerateAutomationMessageMiningRights(Empire empire, bool allowMining)
        {
            string empty = string.Empty;
            if (allowMining)
            {
                return string.Format(TextResolver.GetText("Mining Rights Check Offer"), empire.Name);
            }
            return string.Format(TextResolver.GetText("Mining Rights Check Cancel"), empire.Name);
        }

        private string GenerateAutomationMessageTradeRestrictedResources(Empire empire, bool trade)
        {
            bool plural = false;
            string arg = GenerateEmpireRestrictedResourcesDescription(out plural);
            string empty = string.Empty;
            if (trade)
            {
                return string.Format(TextResolver.GetText("Automation Trade Restricted Resources"), arg, empire.Name);
            }
            return string.Format(TextResolver.GetText("Automation Terminate Restricted Resources"), arg, empire.Name);
        }

        private string GenerateAutomationMessageDestroyPlanet(Habitat habitat, BuiltObject planetDestroyer)
        {
            Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
            string text = string.Empty;
            if (habitat != null && habitat.Empire != null)
            {
                text = habitat.Empire.Name;
            }
            return string.Format(TextResolver.GetText("Automation Destroy Planet"), Galaxy.ResolveDescription(habitat.Type), Galaxy.ResolveDescription(habitat.Category).ToLower(CultureInfo.InvariantCulture), habitat.Name, text, habitat2.Name, planetDestroyer.Name);
        }

        private string GenerateAutomationMessageBombardColony(Habitat habitat, ShipGroup attackFleet)
        {
            Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
            string text = string.Empty;
            if (habitat.Population != null && habitat.Population.DominantRace != null)
            {
                text = habitat.Population.DominantRace.Name;
            }
            return string.Format(TextResolver.GetText("Automation Bombard Colony"), Galaxy.ResolveDescription(habitat.Type), Galaxy.ResolveDescription(habitat.Category), habitat.Name, habitat.Empire.Name, habitat2.Name, text, attackFleet.Name);
        }

        private string GenerateAutomationMessageAttackEnemyWithWaypoint(StellarObject target, bool blockade, ShipGroup attackFleet, StellarObject waypoint)
        {
            string text = string.Empty;
            if (attackFleet != null)
            {
                text = attackFleet.Name;
            }
            string empty = string.Empty;
            if (blockade)
            {
                if (target is Habitat)
                {
                    Habitat habitat = (Habitat)target;
                    Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                    return string.Format(TextResolver.GetText("Automation Blockade Enemy Colony With Waypoint"), Galaxy.ResolveDescription(habitat.Type), Galaxy.ResolveDescription(habitat.Category), habitat.Name, target.Empire.Name, habitat2.Name, text, waypoint.Name);
                }
                string text2 = string.Empty;
                if (target is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)target;
                    if (builtObject != null && builtObject.NearestSystemStar != null)
                    {
                        text2 = builtObject.NearestSystemStar.Name;
                    }
                }
                return string.Format(TextResolver.GetText("Automation Blockade Enemy Base With Waypoint"), target.Name, target.Empire.Name, text2, text, waypoint.Name);
            }
            if (target is Habitat)
            {
                Habitat habitat3 = (Habitat)target;
                Habitat habitat4 = Galaxy.DetermineHabitatSystemStar(habitat3);
                return string.Format(TextResolver.GetText("Automation Attack Enemy Colony With Waypoint"), Galaxy.ResolveDescription(habitat3.Type), Galaxy.ResolveDescription(habitat3.Category), habitat3.Name, target.Empire.Name, habitat4.Name, text, waypoint.Name);
            }
            string text3 = string.Empty;
            if (target is BuiltObject)
            {
                BuiltObject builtObject2 = (BuiltObject)target;
                if (builtObject2 != null && builtObject2.NearestSystemStar != null)
                {
                    text3 = builtObject2.NearestSystemStar.Name;
                }
            }
            return string.Format(TextResolver.GetText("Automation Attack Enemy Base With Waypoint"), target.Name, target.Empire.Name, text3, text, waypoint.Name);
        }

        private string GenerateAutomationMessageAttackEnemy(Habitat habitat, bool blockade, ShipGroup attackFleet)
        {
            string text = string.Empty;
            if (attackFleet != null)
            {
                text = attackFleet.Name;
            }
            Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
            string empty = string.Empty;
            string text2 = string.Empty;
            if (habitat != null && habitat.Empire != null)
            {
                text2 = habitat.Empire.Name;
            }
            if (blockade)
            {
                return string.Format(TextResolver.GetText("Automation Blockade Enemy Colony"), Galaxy.ResolveDescription(habitat.Type), Galaxy.ResolveDescription(habitat.Category), habitat.Name, text2, habitat2.Name, text);
            }
            return string.Format(TextResolver.GetText("Automation Attack Enemy Colony"), Galaxy.ResolveDescription(habitat.Type), Galaxy.ResolveDescription(habitat.Category), habitat.Name, text2, habitat2.Name, text);
        }

        private string GenerateAutomationMessageOfferPirateAttackMission(EmpireActivity attackMission)
        {
            string result = string.Empty;
            if (attackMission != null && attackMission.Type == EmpireActivityType.Attack && attackMission.Target != null && attackMission.TargetEmpire != null)
            {
                string text = string.Empty;
                if (attackMission.Target is BuiltObject)
                {
                    Habitat nearestSystemStar = ((BuiltObject)attackMission.Target).NearestSystemStar;
                    if (nearestSystemStar != null)
                    {
                        text = nearestSystemStar.Name;
                    }
                }
                else if (attackMission.Target is Habitat)
                {
                    Habitat habitat = Galaxy.DetermineHabitatSystemStar((Habitat)attackMission.Target);
                    if (habitat != null)
                    {
                        text = habitat.Name;
                    }
                }
                result = string.Format(TextResolver.GetText("Offer Pirate Attack Mission Advisor Suggestion"), attackMission.Target.Name, attackMission.TargetEmpire.Name, text, attackMission.Price.ToString("0"));
            }
            return result;
        }

        private string GenerateAutomationMessageOfferPirateDefendMission(EmpireActivity defendMission)
        {
            string result = string.Empty;
            if (defendMission != null && defendMission.Type == EmpireActivityType.Defend && defendMission.Target != null && defendMission.TargetEmpire != null)
            {
                string arg = string.Empty;
                if (defendMission.Target is BuiltObject)
                {
                    Habitat nearestSystemStar = ((BuiltObject)defendMission.Target).NearestSystemStar;
                    if (nearestSystemStar != null)
                    {
                        arg = nearestSystemStar.Name;
                    }
                }
                else if (defendMission.Target is Habitat)
                {
                    Habitat habitat = Galaxy.DetermineHabitatSystemStar((Habitat)defendMission.Target);
                    if (habitat != null)
                    {
                        arg = habitat.Name;
                    }
                }
                result = string.Format(TextResolver.GetText("Offer Pirate Defend Mission Advisor Suggestion"), defendMission.Target.Name, arg, defendMission.Price.ToString("0"));
            }
            return result;
        }

        private string GenerateAutomationMessageOfferPirateSmuggleMission(EmpireActivity smuggleMission)
        {
            string result = string.Empty;
            if (smuggleMission != null && smuggleMission.Type == EmpireActivityType.Smuggle && smuggleMission.Target != null && smuggleMission.TargetEmpire != null)
            {
                string text = string.Empty;
                if (smuggleMission.Target is BuiltObject)
                {
                    Habitat nearestSystemStar = ((BuiltObject)smuggleMission.Target).NearestSystemStar;
                    if (nearestSystemStar != null)
                    {
                        text = nearestSystemStar.Name;
                    }
                }
                else if (smuggleMission.Target is Habitat)
                {
                    Habitat habitat = Galaxy.DetermineHabitatSystemStar((Habitat)smuggleMission.Target);
                    if (habitat != null)
                    {
                        text = habitat.Name;
                    }
                }
                result = ((smuggleMission.ResourceId != byte.MaxValue) ? string.Format(TextResolver.GetText("Offer Pirate Smuggle Mission Advisor Suggestion"), smuggleMission.Target.Name, text, new Resource(smuggleMission.ResourceId).Name, (smuggleMission.Price * 100.0).ToString("0.#")) : string.Format(TextResolver.GetText("Offer Pirate Smuggle Mission All Resources Advisor Suggestion"), smuggleMission.Target.Name, text, (smuggleMission.Price * 100.0).ToString("0.#")));
            }
            return result;
        }

        private string GenerateAutomationMessageAcceptPirateSmuggleMission(EmpireActivity smuggleMission)
        {
            string result = string.Empty;
            if (smuggleMission != null && smuggleMission.Type == EmpireActivityType.Smuggle && smuggleMission.Target != null && smuggleMission.TargetEmpire != null)
            {
                string text = string.Empty;
                if (smuggleMission.Target is BuiltObject)
                {
                    Habitat nearestSystemStar = ((BuiltObject)smuggleMission.Target).NearestSystemStar;
                    if (nearestSystemStar != null)
                    {
                        text = nearestSystemStar.Name;
                    }
                }
                else if (smuggleMission.Target is Habitat)
                {
                    Habitat habitat = Galaxy.DetermineHabitatSystemStar((Habitat)smuggleMission.Target);
                    if (habitat != null)
                    {
                        text = habitat.Name;
                    }
                }
                result = ((smuggleMission.RequestingEmpire == _Galaxy.IndependentEmpire) ? ((smuggleMission.ResourceId != byte.MaxValue) ? string.Format(TextResolver.GetText("Accept Pirate Smuggle Mission Independent Advisor Suggestion"), new Resource(smuggleMission.ResourceId).Name, smuggleMission.Target.Name, text, (smuggleMission.Price * 100.0).ToString("0.#")) : string.Format(TextResolver.GetText("Accept Pirate Smuggle Mission Independent All Resources Advisor Suggestion"), smuggleMission.Target.Name, text, (smuggleMission.Price * 100.0).ToString("0.#"))) : ((smuggleMission.ResourceId != byte.MaxValue) ? string.Format(TextResolver.GetText("Accept Pirate Smuggle Mission Advisor Suggestion"), smuggleMission.RequestingEmpire.Name, new Resource(smuggleMission.ResourceId).Name, smuggleMission.Target.Name, text, (smuggleMission.Price * 100.0).ToString("0.#")) : string.Format(TextResolver.GetText("Accept Pirate Smuggle Mission All Resources Advisor Suggestion"), smuggleMission.RequestingEmpire.Name, smuggleMission.Target.Name, text, (smuggleMission.Price * 100.0).ToString("0.#"))));
            }
            return result;
        }

        private string GenerateAutomationMessageDiplomaticGift(Empire empire, double moneyAmount)
        {
            return string.Format(TextResolver.GetText("Automation Give Gift"), moneyAmount.ToString("###,###,###,##0"), empire.Name);
        }

        private string GenerateAutomationMessageCancelPirateProtection(Empire empire, double monthlyFee)
        {
            string result = string.Empty;
            if (empire != null)
            {
                result = ((PirateEmpireBaseHabitat == null || empire.PirateEmpireBaseHabitat == null) ? string.Format(TextResolver.GetText("Automation Pirate Cancel Protection"), empire.Name, monthlyFee.ToString("0")) : string.Format(TextResolver.GetText("Automation Pirate Cancel Protection To Pirates"), empire.Name));
            }
            return result;
        }

        private string GenerateAutomationMessagePirateProtection(Empire empire, double monthlyFee)
        {
            string result = string.Empty;
            if (empire != null)
            {
                result = string.Format(TextResolver.GetText("Automation Pirate Offer Protection"), empire.Name, monthlyFee.ToString("0"));
            }
            return result;
        }

        private string GenerateAutomationMessagePirateProtectionToPirates(Empire empire)
        {
            string result = string.Empty;
            if (empire != null)
            {
                result = string.Format(TextResolver.GetText("Automation Pirate Offer Protection To Pirates"), empire.Name);
            }
            return result;
        }

        private string GenerateAutomationMessageTreaty(Empire empire, DiplomaticRelationType relationType)
        {
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
            string empty = string.Empty;
            if (relationType == DiplomaticRelationType.None)
            {
                if (diplomaticRelation.Type == DiplomaticRelationType.SubjugatedDominion)
                {
                    if (diplomaticRelation.Initiator == this)
                    {
                        return string.Format(TextResolver.GetText("Automation Free From Subjugation"), empire.Name);
                    }
                    return string.Format(TextResolver.GetText("Automation Request Release From Subjugation"), empire.Name);
                }
                return string.Format(TextResolver.GetText("Automation Cancel Treaty"), empire.Name);
            }
            return string.Format(TextResolver.GetText("Automation Offer Treaty"), empire.Name, Galaxy.ResolveDescription(relationType));
        }

        private string GenerateAutomationMessageWarTradeSanctions(Empire empire, DiplomaticRelationType relationType)
        {
            string result = string.Empty;
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
            switch (relationType)
            {
                case DiplomaticRelationType.None:
                    result = ((diplomaticRelation.Type != DiplomaticRelationType.War) ? ((diplomaticRelation.Type != DiplomaticRelationType.TradeSanctions) ? string.Format(TextResolver.GetText("Automation Cancel Treaty"), empire.Name) : string.Format(TextResolver.GetText("Automation Lift Trade Sanctions"), empire.Name)) : string.Format(TextResolver.GetText("Automation End War"), empire.Name));
                    break;
                case DiplomaticRelationType.War:
                    result = string.Format(TextResolver.GetText("Automation Declare War"), empire.Name);
                    break;
                case DiplomaticRelationType.TradeSanctions:
                    result = string.Format(TextResolver.GetText("Automation Initiate Trade Sanctions"), empire.Name);
                    break;
                case DiplomaticRelationType.SubjugatedDominion:
                    result = string.Format(TextResolver.GetText("Automation Subjugated Dominion"), empire.Name);
                    break;
            }
            return result;
        }

        public static GovernmentAttributesList DetermineMostSuitableGovermentTypes(Race race, List<int> allowableGovernmentTypes)
        {
            return DetermineMostSuitableGovermentTypes(race, allowableGovernmentTypes, 3);
        }

        public static GovernmentAttributesList DetermineMostSuitableGovermentTypes(Race race, List<int> allowableGovernmentTypes, int maximumCount)
        {
            int bracket = 0;
            DetermineRacialCharacteristicFactor(race.AggressionLevel, out bracket);
            int bracket2 = 0;
            DetermineRacialCharacteristicFactor(race.CautionLevel, out bracket2);
            int bracket3 = 0;
            DetermineRacialCharacteristicFactor(race.IntelligenceLevel, out bracket3);
            int bracket4 = 0;
            DetermineRacialCharacteristicFactor(race.FriendlinessLevel, out bracket4);
            GovernmentAttributesList governmentAttributesList = new GovernmentAttributesList();
            List<(double suitability, GovernmentAttributes govAtr)> list = new List<(double suitability, GovernmentAttributes govAtr)>();
            for (int i = 0; i < Galaxy.GovernmentsStatic.Count; i++)
            {
                GovernmentAttributes governmentAttributes = Galaxy.GovernmentsStatic[i];
                if (governmentAttributes != null)
                {
                    double num = 1.0 - governmentAttributes.WarWeariness + (governmentAttributes.TroopRecruitment - 1.0);
                    double num2 = (governmentAttributes.TroopRecruitment - 1.0) * 0.5 + (1.0 - governmentAttributes.MaintenanceCosts) * 0.5;
                    double num3 = governmentAttributes.ResearchSpeed - 1.0 + (1.0 - governmentAttributes.MaintenanceCosts) + (1.0 - governmentAttributes.Corruption);
                    double num4 = governmentAttributes.TradeBonus - 1.0 + (governmentAttributes.ApprovalRating - 1.0) + (governmentAttributes.PopulationGrowth - 1.0);
                    double num5 = 0.0;
                    num5 += (double)(race.AggressionLevel - 100) / 100.0 * num;
                    num5 += (double)(race.CautionLevel - 100) / 100.0 * num2;
                    num5 += (double)(race.IntelligenceLevel - 100) / 100.0 * num3;
                    num5 += (double)(race.FriendlinessLevel - 100) / 100.0 * num4;
                    if (governmentAttributes.Availability != 0)
                    {
                        num5 += 2.0;
                    }
                    if (allowableGovernmentTypes.Contains(governmentAttributes.GovernmentId))
                    {
                        //governmentAttributes.SortTag = (float)num5;
                        //governmentAttributesList.Add(governmentAttributes);
                        list.Add((num, governmentAttributes));
                    }
                }
            }
            //governmentAttributesList.Sort();
            //governmentAttributesList.Reverse();
            list.Sort((x, y) => x.suitability.CompareTo(y.suitability));
            list.Reverse();
            if (list.Count > maximumCount)
            {
                GovernmentAttributesList governmentAttributesList2 = new GovernmentAttributesList();
                //for (int j = 0; j < maximumCount; j++)
                //{
                //    governmentAttributesList2.Add(governmentAttributesList[j]);
                //}
                governmentAttributesList2.AddRange(list.Select(x => x.govAtr).Take(maximumCount));
                return governmentAttributesList2;
            }
            return governmentAttributesList;
        }

        public void ChangeGovernment(int governmentId)
        {
            GovernmentAttributes governmentAttributes = _Galaxy.Governments[governmentId];
            GovernmentAttributes governmentAttributes2 = _Galaxy.Governments[_GovernmentId];
            bool flag = false;
            if (governmentAttributes2.SpecialFunctionCode == 1)
            {
                flag = true;
            }
            _GovernmentId = governmentId;
            _GovernmentAttributes = governmentAttributes;
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire != null && empire.Active && empire != this && empire != _Galaxy.IndependentEmpire && empire.PirateEmpireBaseHabitat == null)
                {
                    EmpireEvaluation empireEvaluation = empire.ObtainEmpireEvaluation(this);
                    empireEvaluation.GovernmentStyleAffinity = 0;
                    empireEvaluation.GovernmentStyleAffinityCumulative = 0.0;
                }
            }
            if (flag)
            {
                ReviewTaxes();
            }
        }

        public static int SelectSuitableGovernment(Race race, int excludeId, List<int> allowableGovernmentTypes)
        {
            GovernmentAttributesList governmentAttributesList = DetermineMostSuitableGovermentTypes(race, allowableGovernmentTypes);
            int result = -1;
            if (governmentAttributesList.Count > 0)
            {
                result = governmentAttributesList[0].GovernmentId;
                int num = -1;
                for (int i = 0; i < governmentAttributesList.Count; i++)
                {
                    if (governmentAttributesList[i].Availability == 1)
                    {
                        num = i;
                        break;
                    }
                }
                int num2 = 0;
                if (num >= 0)
                {
                    num2 = num;
                }
                int num3 = Galaxy.Rnd.Next(0, governmentAttributesList.Count + 1);
                if (num3 == governmentAttributesList.Count)
                {
                    num3 = num2;
                }
                if (excludeId >= 0)
                {
                    int iterationCount = 0;
                    while (Galaxy.ConditionCheckLimit(governmentAttributesList[num3].GovernmentId == excludeId, 20, ref iterationCount))
                    {
                        num3 = Galaxy.Rnd.Next(0, 4);
                        if (num3 == 3)
                        {
                            num3 = num2;
                        }
                    }
                }
                result = governmentAttributesList[num3].GovernmentId;
            }
            return result;
        }

        public int HaveRevolution(Race dominantRace)
        {
            return HaveRevolution(dominantRace, -1);
        }

        public int HaveRevolution(Race dominantRace, int governmentId)
        {
            return HaveRevolution(dominantRace, governmentId, 1.0);
        }

        public int HaveRevolution(Race dominantRace, int governmentId, double damageFactor)
        {
            int governmentId2 = _GovernmentId;
            if (governmentId == -1)
            {
                governmentId = SelectSuitableGovernment(dominantRace, _GovernmentId, _AllowableGovernmentTypes);
                if (governmentId >= 0)
                {
                    int iterationCount = 0;
                    while (Galaxy.ConditionCheckLimit(governmentId == governmentId2, 20, ref iterationCount))
                    {
                        governmentId = SelectSuitableGovernment(dominantRace, _GovernmentId, _AllowableGovernmentTypes);
                    }
                }
            }
            if (governmentId >= 0)
            {
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat = Colonies[i];
                    int num = Galaxy.Rnd.Next(0, 25);
                    num = (int)((double)num * damageFactor);
                    num = Math.Min(num, habitat.GetDevelopmentLevel());
                    long num2 = habitat.Population.TotalAmount / 10;
                    num2 = (long)((double)num2 * damageFactor);
                    if (num2 > 2000000000)
                    {
                        num2 = 2000000000L;
                    }
                    int num3 = Galaxy.Rnd.Next(0, (int)num2);
                    num3 = (int)Math.Min(num3, habitat.Population[0].Amount);
                    habitat.SetDevelopmentLevel(habitat.GetDevelopmentLevel() - num);
                    habitat.Population[0].Amount -= num3;
                    if (habitat.Population[0].Amount < 10000000)
                    {
                        habitat.Population[0].Amount = 10000000L;
                    }
                    habitat.Population.RecalculateTotalAmount();
                    _Galaxy.DoCharacterEvent(CharacterEventType.ColonyDevelopmentDecrease, habitat, habitat.Characters, includeLeader: true, habitat.Empire);
                }
                double num4 = 0.0;
                GovernmentAttributes governmentAttributes = _Galaxy.Governments[governmentId];
                num4 = governmentAttributes.LeaderReplacementDisruptionLevel;
                if (num4 > 0.0)
                {
                    double num5 = 0.7 + Galaxy.Rnd.NextDouble() * 0.3;
                    int num6 = (int)(num5 * num4 * ((double)Colonies.Count * 0.1));
                    for (int j = 0; j < num6; j++)
                    {
                        int index = Galaxy.Rnd.Next(0, Colonies.Count);
                        if (Capital != Colonies[index])
                        {
                            Colonies[index].LeaveEmpire();
                        }
                    }
                }
                ChangeGovernment(governmentId);
            }
            return governmentId;
        }

        public bool CanDeployXaraktorVirus(out Plague xaraktorVirus, out string cannotDeployReason)
        {
            xaraktorVirus = null;
            cannotDeployReason = string.Empty;
            if (Research != null && Research.EnabledPlagues != null && Research.EnabledPlagues.Count > 0)
            {
                xaraktorVirus = Research.EnabledPlagues.GetFirstBySpecialFunctionCode(1);
                if (xaraktorVirus != null)
                {
                    if (Colonies != null && Colonies.Count > 0)
                    {
                        int num = Colonies.CountColoniesWithFacilityType(PlanetaryFacilityType.Wonder, WonderType.RaceAchievement, 2);
                        if (num > 0)
                        {
                            if (_Galaxy.CurrentDateTime.Subtract(LastXaraktorVirusDeploy).TotalSeconds > 150.0)
                            {
                                return true;
                            }
                            cannotDeployReason = TextResolver.GetText("Cannot Deploy Xaraktor Virus - too soon");
                        }
                        else
                        {
                            cannotDeployReason = TextResolver.GetText("Cannot Deploy Xaraktor Virus - no facility");
                        }
                    }
                    else
                    {
                        cannotDeployReason = TextResolver.GetText("Cannot Deploy Xaraktor Virus - no facility");
                    }
                }
            }
            return false;
        }

        private static int DetermineRacialCharacteristicFactor(int characteristicRating, out int bracket)
        {
            int result = 0;
            bracket = 0;
            if (characteristicRating < 85)
            {
                bracket = -1;
                result = 85 - characteristicRating;
            }
            else if (characteristicRating >= 85 && characteristicRating <= 115)
            {
                bracket = 0;
                result = 16 - Math.Abs(characteristicRating - 100);
            }
            else if (characteristicRating > 115)
            {
                bracket = 1;
                result = characteristicRating - 115;
            }
            return result;
        }

        int IComparable<Empire>.CompareTo(Empire other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
