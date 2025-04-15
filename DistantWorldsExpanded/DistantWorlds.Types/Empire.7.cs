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
        private void ReviewCharacterTraits()
        {
            if (Characters == null)
            {
                return;
            }
            long currentStarDate = _Galaxy.CurrentStarDate;
            double stateMoney = StateMoney;
            double num = CalculateAccurateAnnualCashflow();
            double num2 = AverageHappiness();
            double num3 = AverageStateCashPerPopulation();
            double num4 = AverageCashflowPerPopulation();
            double num5 = AverageShipMaintenancePerPopulation();
            double num6 = AverageSpaceportsPerColony();
            double num7 = AverageResearchStationsPerColony();
            double num8 = AverageCapitalShipsPerColony();
            double num9 = AverageMiningStationsPerColony();
            double num10 = AverageConstructionShipsPerColony();
            double num11 = _Galaxy.CalculateAverageHappiness();
            double num12 = _Galaxy.CalculateAverageCashflowPerPopulation();
            double num13 = _Galaxy.CalculateAverageStateCashPerPopulation();
            double num14 = _Galaxy.CalculateAverageShipMaintenancePerPopulation();
            double num15 = _Galaxy.CalculateAverageSpaceportsPerColony();
            double num16 = _Galaxy.CalculateAverageResearchStationsPerColony();
            double num17 = _Galaxy.CalculateAverageCapitalShipsPerColony();
            double num18 = _Galaxy.CalculateAverageMiningStationsPerColony();
            double num19 = _Galaxy.CalculateAverageConstructionShipsPerColony();
            double num20 = _Galaxy.CalculateAverageCorruption();
            CharacterList characterList = new CharacterList();
            int num21 = Galaxy.Rnd.Next(0, Characters.Count);
            for (int i = num21; i < Characters.Count; i++)
            {
                characterList.Add(Characters[i]);
            }
            for (int j = 0; j < num21; j++)
            {
                characterList.Add(Characters[j]);
            }
            for (int k = 0; k < characterList.Count; k++)
            {
                Character character = characterList[k];
                if (character == null || !character.BonusesKnown)
                {
                    continue;
                }
                if (Galaxy.Rnd.Next(0, 90) == 1)
                {
                    List<CharacterTraitType> traits = Character.DetermineValidTraitsForRole(character.Role);
                    double num22 = 0.0;
                    double num23 = 0.0;
                    double num24 = 0.0;
                    double num25 = 0.0;
                    double num26 = 0.0;
                    double num27 = 0.0;
                    bool flag = true;
                    long val = currentStarDate;
                    int num28 = 0;
                    long val2 = currentStarDate;
                    long val3 = currentStarDate;
                    switch (character.Role)
                    {
                        case CharacterRole.PirateLeader:
                            num22 = num;
                            break;
                        case CharacterRole.Leader:
                            num23 = num2;
                            num22 = num;
                            num24 = num4;
                            num25 = num3;
                            num26 = num5;
                            num27 = Corruption;
                            break;
                        case CharacterRole.ColonyGovernor:
                            if (character.Location != null && character.Location is Habitat)
                            {
                                Habitat habitat2 = (Habitat)character.Location;
                                if (habitat2 != null && habitat2.Empire == character.Empire && habitat2.Population != null)
                                {
                                    num23 = habitat2.EmpireApprovalRating;
                                    num22 = habitat2.AnnualTaxRevenue;
                                    num24 = num22 / (double)habitat2.Population.TotalAmount;
                                    num25 = num3;
                                    num26 = num5;
                                    num27 = habitat2.Corruption;
                                }
                            }
                            val3 = character.EventHistory.GetDateOfMostRecentEventByType(CharacterEventType.TroopComplete);
                            break;
                        case CharacterRole.Ambassador:
                            flag = false;
                            if (character.Location != null && character.Location is Habitat)
                            {
                                Habitat habitat = (Habitat)character.Location;
                                if (habitat != null && habitat.Empire != null && habitat.Empire != character.Empire && habitat.Empire.Capital != null && habitat.Empire.Capital == habitat)
                                {
                                    flag = true;
                                }
                            }
                            break;
                        case CharacterRole.FleetAdmiral:
                            val = character.EventHistory.GetDateOfMostRecentEventByType(CharacterEventType.SpaceBattle);
                            num28 = character.EventHistory.CountEventsByType(CharacterEventType.HyperjumpExit);
                            val2 = character.EventHistory.GetDateOfMostRecentEventByType(CharacterEventType.HyperjumpExit);
                            break;
                        case CharacterRole.ShipCaptain:
                            val = character.EventHistory.GetDateOfMostRecentEventByType(CharacterEventType.SpaceBattle);
                            num28 = character.EventHistory.CountEventsByType(CharacterEventType.HyperjumpExit);
                            val2 = character.EventHistory.GetDateOfMostRecentEventByType(CharacterEventType.HyperjumpExit);
                            break;
                        case CharacterRole.TroopGeneral:
                            val = character.EventHistory.GetDateOfMostRecentEventByType(CharacterEventType.GroundInvasion);
                            val3 = character.EventHistory.GetDateOfMostRecentEventByType(CharacterEventType.TroopComplete);
                            break;
                    }
                    List<CharacterTraitType> list = new List<CharacterTraitType>();
                    if (PirateEmpireBaseHabitat != null)
                    {
                        if (stateMoney > 0.0 && num22 > 5000.0)
                        {
                            list.Add(CharacterTraitType.GoodAdministrator);
                        }
                        else if (stateMoney < 0.0 && num22 < 0.0)
                        {
                            list.Add(CharacterTraitType.PoorAdministrator);
                        }
                        if (Math.Max(val, character.StartDate) < currentStarDate - Galaxy.RealSecondsInGalacticYear * 1000 * 3)
                        {
                            list.Add(CharacterTraitType.Drunk);
                        }
                        if (num28 > 15 && Math.Max(val2, character.StartDate) > currentStarDate - (long)((double)Galaxy.RealSecondsInGalacticYear * 1000.0 * 0.5))
                        {
                            list.Add(CharacterTraitType.SkilledNavigator);
                            list.Add(CharacterTraitType.GoodSpaceLogistician);
                        }
                        else if (Math.Max(val2, character.StartDate) < currentStarDate - Galaxy.RealSecondsInGalacticYear * 1000 * 2)
                        {
                            list.Add(CharacterTraitType.PoorNavigator);
                            list.Add(CharacterTraitType.PoorSpaceLogistician);
                        }
                    }
                    else
                    {
                        if (num23 > num11 * 1.5)
                        {
                            list.Add(CharacterTraitType.Famous);
                        }
                        else if (num23 < num11 * 0.4)
                        {
                            list.Add(CharacterTraitType.Disliked);
                        }
                        if (num25 > num13 * 1.3 && num24 > num12 * 1.3)
                        {
                            list.Add(CharacterTraitType.GoodAdministrator);
                        }
                        else if (stateMoney < 0.0 && num22 < 0.0)
                        {
                            list.Add(CharacterTraitType.PoorAdministrator);
                        }
                        if (num26 > num14 * 1.3 && num22 > 0.0)
                        {
                            list.Add(CharacterTraitType.BeanCounter);
                        }
                        else if (num26 < num14 * 0.7 && num22 > 0.0)
                        {
                            list.Add(CharacterTraitType.Generous);
                        }
                        if ((Designs != null && Designs.FindNewestCanBuild(BuiltObjectSubRole.CapitalShip) != null && num8 < num17 * 0.33) || num6 < num15 * 0.4 || num7 < num16 * 0.33)
                        {
                            list.Add(CharacterTraitType.Luddite);
                        }
                        if (num10 < num19 * 0.5 || num9 < num18 * 0.5)
                        {
                            list.Add(CharacterTraitType.Environmentalist);
                        }
                        else if (num10 > num19 * 1.5 || num9 > num18 * 1.5)
                        {
                            list.Add(CharacterTraitType.Industrialist);
                        }
                        if (num5 < num14 * 0.4 && num22 > 0.0)
                        {
                            list.Add(CharacterTraitType.Disorganized);
                        }
                        if (num27 > num20 * 1.5)
                        {
                            list.Add(CharacterTraitType.Measured);
                            list.Add(CharacterTraitType.IntelligenceMeasured);
                            if (num27 > num20 * 2.2)
                            {
                                list.Add(CharacterTraitType.Addict);
                                list.Add(CharacterTraitType.Corrupt);
                                list.Add(CharacterTraitType.IntelligenceAddict);
                                list.Add(CharacterTraitType.IntelligenceCorrupt);
                            }
                        }
                        else if (num27 < num20 * 0.5)
                        {
                            list.Add(CharacterTraitType.Sober);
                            list.Add(CharacterTraitType.IntelligenceSober);
                        }
                        if (num23 > num11 * 1.7 && num25 > num13 * 1.7 && num22 > 0.0)
                        {
                            list.Add(CharacterTraitType.Uninhibited);
                            list.Add(CharacterTraitType.Addict);
                            list.Add(CharacterTraitType.IntelligenceUninhibited);
                            list.Add(CharacterTraitType.IntelligenceAddict);
                        }
                        else if (num23 < num11 * 0.5)
                        {
                            list.Add(CharacterTraitType.Measured);
                            list.Add(CharacterTraitType.Sober);
                            list.Add(CharacterTraitType.PoorSpeaker);
                            list.Add(CharacterTraitType.IntelligenceMeasured);
                            list.Add(CharacterTraitType.IntelligenceSober);
                            list.Add(CharacterTraitType.IntelligencePoorSpeaker);
                        }
                        if (Policy.NewColonyPopulationPolicyAllRaces == ColonyPopulationPolicy.Assimilate)
                        {
                            list.Add(CharacterTraitType.Tolerant);
                            list.Add(CharacterTraitType.IntelligenceTolerant);
                        }
                        else if (Policy.NewColonyPopulationPolicyAllRaces == ColonyPopulationPolicy.Enslave || Policy.NewColonyPopulationPolicyAllRaces == ColonyPopulationPolicy.Exterminate)
                        {
                            list.Add(CharacterTraitType.Xenophobic);
                            list.Add(CharacterTraitType.IntelligenceXenophobic);
                        }
                        if (num25 > num13 * 2.2 && num22 > 0.0)
                        {
                            list.Add(CharacterTraitType.Corrupt);
                            list.Add(CharacterTraitType.IntelligenceCorrupt);
                        }
                        else if (num27 < num20 * 0.7)
                        {
                            list.Add(CharacterTraitType.Lawful);
                            list.Add(CharacterTraitType.IntelligenceLawful);
                        }
                        if (num23 > num11 * 3.0)
                        {
                            list.Add(CharacterTraitType.Lazy);
                        }
                        if (!flag)
                        {
                            list.Add(CharacterTraitType.TongueTied);
                        }
                        if (Math.Max(val, character.StartDate) < currentStarDate - Galaxy.RealSecondsInGalacticYear * 1000 * 3)
                        {
                            list.Add(CharacterTraitType.Drunk);
                        }
                        if (num28 > 15 && Math.Max(val2, character.StartDate) > currentStarDate - (long)((double)Galaxy.RealSecondsInGalacticYear * 1000.0 * 0.5))
                        {
                            list.Add(CharacterTraitType.SkilledNavigator);
                            list.Add(CharacterTraitType.GoodSpaceLogistician);
                        }
                        else if (Math.Max(val2, character.StartDate) < currentStarDate - Galaxy.RealSecondsInGalacticYear * 1000 * 2)
                        {
                            list.Add(CharacterTraitType.PoorNavigator);
                            list.Add(CharacterTraitType.PoorSpaceLogistician);
                        }
                        if (Math.Max(val3, character.StartDate) < currentStarDate - Galaxy.RealSecondsInGalacticYear * 1000 * 2)
                        {
                            list.Add(CharacterTraitType.PoorRecruiter);
                            if (Math.Max(val, character.StartDate) < currentStarDate - Galaxy.RealSecondsInGalacticYear * 1000 * 3)
                            {
                                list.Add(CharacterTraitType.PoorGroundLogistician);
                            }
                        }
                    }
                    List<CharacterTraitType> list2 = _Galaxy.IntersectTraitLists(list, traits);
                    if (list2 != null && list2.Count > 0)
                    {
                        CharacterTraitType characterTraitType = list2[Galaxy.Rnd.Next(0, list2.Count)];
                        if (character.AddTrait(characterTraitType, starting: false, _Galaxy))
                        {
                            string description = string.Format(TextResolver.GetText("Character New Trait Review"), Galaxy.ResolveDescription(character.Role), character.Name, Galaxy.ResolveDescription(characterTraitType));
                            character.Empire.SendMessageToEmpire(character.Empire, EmpireMessageType.CharacterSkillTraitChange, character, description);
                            break;
                        }
                    }
                }
                if (character.EventHistory.Count <= 30)
                {
                    continue;
                }
                int num29 = character.EventHistory.Count - 30;
                long num30 = _Galaxy.CurrentStarDate - Galaxy.RealSecondsInGalacticYear * 1000 * 3;
                CharacterEventList characterEventList = new CharacterEventList();
                for (int l = 0; l < character.EventHistory.Count; l++)
                {
                    if (characterEventList.Count < num29)
                    {
                        CharacterEvent characterEvent = character.EventHistory[l];
                        if (characterEvent != null && !Galaxy.DetermineCharacterEventIsPublic(characterEvent.Type) && characterEvent.StarDate < num30)
                        {
                            characterEventList.Add(characterEvent);
                        }
                    }
                }
                for (int m = 0; m < characterEventList.Count; m++)
                {
                    character.EventHistory.Remove(characterEventList[m]);
                }
            }
        }

        private void ReviewDemoralizingCharacters()
        {
            if (!_ControlCharacterLocations)
            {
                return;
            }
            CharacterList characterList = new CharacterList();
            if (Characters != null)
            {
                for (int i = 0; i < Characters.Count; i++)
                {
                    Character character = Characters[i];
                    if (character.Traits.Contains(CharacterTraitType.Demoralizing))
                    {
                        characterList.Add(character);
                    }
                }
            }
            for (int j = 0; j < characterList.Count; j++)
            {
                Character character2 = characterList[j];
                if (character2 != null && character2.GetSkillLevelTotal() < Galaxy.Rnd.Next(15, 30))
                {
                    character2.SendDeathMessage(CharacterDeathType.Dismissed, _Galaxy);
                    character2.Kill(_Galaxy);
                }
            }
        }

        private void ReviewCharacterLocations()
        {
            if (Characters == null)
            {
                return;
            }
            for (int i = 0; i < Characters.Count; i++)
            {
                Character character = Characters[i];
                if (_ControlCharacterLocations)
                {
                    ReviewCharacterLocation(character, transferToLocation: true);
                }
                ApplyCharacterLocationBonusToOtherCharacters(character);
            }
        }

        private void ApplyCharacterLocationBonusToOtherCharacters(Character character)
        {
            if (character == null || character.Location == null || character.Empire == null || character.Empire.Characters == null)
            {
                return;
            }
            bool flag = character.Traits.Contains(CharacterTraitType.InspiringPresence);
            bool flag2 = character.Traits.Contains(CharacterTraitType.Demoralizing);
            if (!flag && !flag2)
            {
                return;
            }
            CharacterList characterList = new CharacterList();
            switch (character.Role)
            {
                case CharacterRole.FleetAdmiral:
                case CharacterRole.TroopGeneral:
                    if (character.Location is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)character.Location;
                        if (builtObject != null)
                        {
                            if (builtObject.ShipGroup != null)
                            {
                                characterList.AddRange(character.Empire.Characters.GetFleetAdmiralsAndGenerals(builtObject.ShipGroup));
                            }
                            else
                            {
                                characterList.AddRange(builtObject.Characters);
                            }
                        }
                    }
                    else
                    {
                        characterList.AddRange(character.Empire.Characters.FindCharactersAtLocation(character.Location));
                    }
                    break;
                default:
                    characterList.AddRange(character.Empire.Characters.FindCharactersAtLocation(character.Location));
                    break;
            }
            if (characterList.Contains(character))
            {
                characterList.Remove(character);
            }
            for (int i = 0; i < characterList.Count; i++)
            {
                if (characterList[i].Skills == null || characterList[i].Skills.Count <= 0)
                {
                    continue;
                }
                CharacterSkill characterSkill = characterList[i].Skills[Galaxy.Rnd.Next(0, characterList[i].Skills.Count)];
                if (characterSkill != null)
                {
                    if (flag)
                    {
                        characterList[i].IncrementSkillProgress(characterSkill.Type, 0.05f, _Galaxy);
                    }
                    else if (flag2)
                    {
                        characterList[i].IncrementSkillProgress(characterSkill.Type, -0.05f, _Galaxy);
                    }
                }
            }
        }

        public bool CheckLocationSafeForDemoralizingCharacter(bool characterIsDemoralizing, StellarObject location, Character characterToExclude)
        {
            if (characterIsDemoralizing)
            {
                return CheckLocationSafeForDemoralizingCharacter(location, characterToExclude);
            }
            return true;
        }

        public bool CheckLocationSafeForDemoralizingCharacter(StellarObject location, Character characterToExclude)
        {
            if (location != null && location.Characters != null)
            {
                CharacterList characterList = location.Characters.FindCharactersAtLocationNotTransferring(location, this, null);
                if (characterList != null && characterList.Count > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckLocationSafeForDemoralizingCharacter(bool characterIsDemoralizing, ShipGroup fleet, Character characterToExclude)
        {
            if (characterIsDemoralizing)
            {
                return CheckLocationSafeForDemoralizingCharacter(fleet, characterToExclude);
            }
            return true;
        }

        public bool CheckLocationSafeForDemoralizingCharacter(ShipGroup fleet, Character characterToExclude)
        {
            if (fleet != null)
            {
                CharacterList characterList = fleet.ObtainCharacters();
                if (characterList != null)
                {
                    CharacterList nonTransferringCharacters = characterList.GetNonTransferringCharacters(CharacterRole.Undefined, characterToExclude);
                    if (nonTransferringCharacters != null && nonTransferringCharacters.Count > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private StellarObject ReviewCharacterLocation(Character character, bool transferToLocation)
        {
            if (character != null && character.TransferDestination == null && character.TransferTimeRemaining <= 0f)
            {
                _ = character.Location;
                Empire empire = character.DetermineLocationEmpire();
                bool flag = character.Traits.Contains(CharacterTraitType.Demoralizing);
                if (flag && character.Role != CharacterRole.Leader && !CheckLocationSafeForDemoralizingCharacter(character.Location, character))
                {
                    if (PirateEmpireBaseHabitat != null)
                    {
                        for (int i = 0; i < BuiltObjects.Count; i++)
                        {
                            BuiltObject builtObject = BuiltObjects[i];
                            if (builtObject != null && builtObject.Role == BuiltObjectRole.Base && CheckLocationSafeForDemoralizingCharacter(flag, builtObject, character))
                            {
                                if (transferToLocation)
                                {
                                    character.TransferToNewLocation(builtObject, _Galaxy);
                                }
                                return builtObject;
                            }
                        }
                    }
                    else if (Colonies != null)
                    {
                        HabitatList habitatsPopulationBelowThreshold = Colonies.GetHabitatsPopulationBelowThreshold(500000000L, HabitatType.Undefined);
                        for (int j = 0; j < habitatsPopulationBelowThreshold.Count; j++)
                        {
                            Habitat habitat = habitatsPopulationBelowThreshold[j];
                            if (habitat != null && CheckLocationSafeForDemoralizingCharacter(flag, habitat, character))
                            {
                                if (transferToLocation)
                                {
                                    character.TransferToNewLocation(habitat, _Galaxy);
                                }
                                return habitat;
                            }
                        }
                        for (int k = 0; k < Colonies.Count; k++)
                        {
                            Habitat habitat2 = Colonies[k];
                            if (habitat2 != null && CheckLocationSafeForDemoralizingCharacter(flag, habitat2, character))
                            {
                                if (transferToLocation)
                                {
                                    character.TransferToNewLocation(habitat2, _Galaxy);
                                }
                                return habitat2;
                            }
                        }
                    }
                }
                switch (character.Role)
                {
                    case CharacterRole.Ambassador:
                        {
                            CharacterList charactersByRole4 = Characters.GetCharactersByRole(CharacterRole.Ambassador);
                            double num13 = 0.0;
                            Empire empire2 = null;
                            if (Reclusive)
                            {
                                break;
                            }
                            for (int num14 = 0; num14 < DiplomaticRelations.Count; num14++)
                            {
                                DiplomaticRelation diplomaticRelation = DiplomaticRelations[num14];
                                if (diplomaticRelation == null || diplomaticRelation.Type == DiplomaticRelationType.NotMet || diplomaticRelation.Type == DiplomaticRelationType.War || diplomaticRelation.OtherEmpire == null || diplomaticRelation.OtherEmpire.Reclusive || diplomaticRelation.OtherEmpire.Capital == null)
                                {
                                    continue;
                                }
                                Habitat systemStar = Galaxy.DetermineHabitatSystemStar(diplomaticRelation.OtherEmpire.Capital);
                                if (!CheckSystemExplored(systemStar))
                                {
                                    continue;
                                }
                                bool flag5 = false;
                                for (int num15 = 0; num15 < charactersByRole4.Count; num15++)
                                {
                                    Character character4 = charactersByRole4[num15];
                                    if (character4 != character)
                                    {
                                        Empire empire3 = character4.DetermineLocationEmpireWithTransfer();
                                        if (empire3 == diplomaticRelation.OtherEmpire)
                                        {
                                            flag5 = true;
                                            break;
                                        }
                                    }
                                }
                                if (flag5)
                                {
                                    continue;
                                }
                                double num16 = 0.0;
                                switch (diplomaticRelation.Strategy)
                                {
                                    case DiplomaticStrategy.Ally:
                                        if (diplomaticRelation.Type != DiplomaticRelationType.MutualDefensePact && diplomaticRelation.Type != DiplomaticRelationType.Protectorate)
                                        {
                                            num16 = 100.0;
                                        }
                                        break;
                                    case DiplomaticStrategy.Befriend:
                                        if (diplomaticRelation.Type != DiplomaticRelationType.FreeTradeAgreement && diplomaticRelation.Type != DiplomaticRelationType.MutualDefensePact && diplomaticRelation.Type != DiplomaticRelationType.Protectorate)
                                        {
                                            num16 = 50.0;
                                        }
                                        break;
                                    case DiplomaticStrategy.DefendPlacate:
                                        if (diplomaticRelation.Type != DiplomaticRelationType.War)
                                        {
                                            num16 = 25.0;
                                        }
                                        break;
                                    case DiplomaticStrategy.Placate:
                                        if (diplomaticRelation.Type != DiplomaticRelationType.War)
                                        {
                                            num16 = 10.0;
                                        }
                                        break;
                                    default:
                                        num16 = 1.0;
                                        break;
                                }
                                num16 *= (double)diplomaticRelation.OtherEmpire.TotalColonyStrategicValue / 1000.0;
                                if (num16 > num13 && CheckLocationSafeForDemoralizingCharacter(flag, diplomaticRelation.OtherEmpire.Capital, character))
                                {
                                    num13 = num16;
                                    empire2 = diplomaticRelation.OtherEmpire;
                                }
                            }
                            if (empire2 != null && empire2.Capital != null && empire2 != empire)
                            {
                                if (transferToLocation)
                                {
                                    character.TransferToNewLocation(empire2.Capital, _Galaxy);
                                }
                                return empire2.Capital;
                            }
                            break;
                        }
                    case CharacterRole.ColonyGovernor:
                        {
                            CharacterList charactersByRole3 = Characters.GetCharactersByRole(CharacterRole.ColonyGovernor);
                            if ((character.ColonyIncome > character.ColonyHappiness && character.ColonyIncome > character.PopulationGrowth) || (character.ColonyHappiness > character.ColonyIncome && character.ColonyHappiness > character.PopulationGrowth))
                            {
                                HabitatList habitatList = Colonies.OrderByRevenue();
                                for (int num7 = 0; num7 < habitatList.Count; num7++)
                                {
                                    Habitat habitat6 = habitatList[num7];
                                    if (habitat6 == character.Location)
                                    {
                                        break;
                                    }
                                    if (charactersByRole3.FindCharactersAtLocationOrTransferring(habitat6).Count <= 0 && CheckLocationSafeForDemoralizingCharacter(flag, habitat6, character))
                                    {
                                        if (transferToLocation)
                                        {
                                            character.TransferToNewLocation(habitat6, _Galaxy);
                                        }
                                        return habitat6;
                                    }
                                }
                            }
                            else
                            {
                                if (character.PopulationGrowth <= character.ColonyIncome || character.PopulationGrowth <= character.ColonyHappiness)
                                {
                                    break;
                                }
                                Habitat habitat7 = null;
                                if (character.Location != null && character.Location is Habitat && empire == this)
                                {
                                    habitat7 = (Habitat)character.Location;
                                    if (habitat7.Population != null)
                                    {
                                        long num8 = Math.Min(1000000000L, habitat7.MaximumPopulation - 10000000);
                                        if (habitat7.Population.TotalAmount < num8)
                                        {
                                            return habitat7;
                                        }
                                    }
                                }
                                Habitat habitat8 = habitat7;
                                double num9 = 0.0;
                                if (habitat8 != null)
                                {
                                    num9 = DetermineColonizationValue(habitat8);
                                }
                                for (int num10 = 0; num10 < Colonies.Count; num10++)
                                {
                                    Habitat habitat9 = Colonies[num10];
                                    if (habitat9.Population == null)
                                    {
                                        continue;
                                    }
                                    long num11 = Math.Min(1000000000L, habitat9.MaximumPopulation - 10000000);
                                    if (habitat9.Population.TotalAmount < num11)
                                    {
                                        double num12 = DetermineColonizationValue(habitat9);
                                        if (num12 > num9 && charactersByRole3.FindCharactersAtLocationOrTransferring(habitat9).Count <= 0 && CheckLocationSafeForDemoralizingCharacter(flag, habitat9, character))
                                        {
                                            habitat8 = habitat9;
                                            num9 = num12;
                                        }
                                    }
                                }
                                if (habitat8 != null && habitat8 != habitat7)
                                {
                                    if (transferToLocation)
                                    {
                                        character.TransferToNewLocation(habitat8, _Galaxy);
                                    }
                                    return habitat8;
                                }
                            }
                            break;
                        }
                    case CharacterRole.PirateLeader:
                        {
                            List<CharacterSkillType> skills = Character.DetermineValidSkillsForRole(CharacterRole.FleetAdmiral, primarySkills: true, secondarySkills: false);
                            List<CharacterSkillType> skills2 = Character.DetermineValidSkillsForRole(CharacterRole.Leader, primarySkills: false, secondarySkills: true);
                            int num21 = character.TotalSkillValuesIfPresent(skills);
                            int num22 = character.TotalSkillValuesIfPresent(skills2);
                            if (num22 > num21)
                            {
                                BuiltObject builtObject5 = _Galaxy.IdentifyPirateBase(this);
                                if (builtObject5 != null && !builtObject5.HasBeenDestroyed && character.Location != builtObject5 && CheckLocationSafeForDemoralizingCharacter(flag, builtObject5, character))
                                {
                                    if (transferToLocation)
                                    {
                                        character.TransferToNewLocation(builtObject5, _Galaxy);
                                    }
                                    return builtObject5;
                                }
                            }
                            else
                            {
                                if (ShipGroups == null || ShipGroups.Count <= 0)
                                {
                                    break;
                                }
                                ShipGroup shipGroup9 = ShipGroups.IdentifyLargestFleet();
                                if (shipGroup9 != null && shipGroup9.LeadShip != null)
                                {
                                    if (character.Location != shipGroup9.LeadShip && CheckLocationSafeForDemoralizingCharacter(flag, shipGroup9, character))
                                    {
                                        if (transferToLocation)
                                        {
                                            character.TransferToNewLocation(shipGroup9.LeadShip, _Galaxy);
                                        }
                                        return shipGroup9.LeadShip;
                                    }
                                    break;
                                }
                                BuiltObject builtObject6 = _Galaxy.IdentifyPirateBase(this);
                                if (builtObject6 != null && !builtObject6.HasBeenDestroyed && character.Location != builtObject6 && CheckLocationSafeForDemoralizingCharacter(flag, builtObject6, character))
                                {
                                    if (transferToLocation)
                                    {
                                        character.TransferToNewLocation(builtObject6, _Galaxy);
                                    }
                                    return builtObject6;
                                }
                            }
                            break;
                        }
                    case CharacterRole.ShipCaptain:
                        {
                            if (PirateEmpireBaseHabitat == null)
                            {
                                bool flag9 = false;
                                if (!(character.Location is BuiltObject))
                                {
                                    flag9 = true;
                                }
                                if (character.Location is BuiltObject)
                                {
                                    BuiltObject builtObject13 = (BuiltObject)character.Location;
                                    if (builtObject13.Role != BuiltObjectRole.Military)
                                    {
                                        flag9 = true;
                                    }
                                }
                                if (!flag9)
                                {
                                    break;
                                }
                                List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
                                list.Add(BuiltObjectSubRole.Escort);
                                list.Add(BuiltObjectSubRole.Frigate);
                                list.Add(BuiltObjectSubRole.Destroyer);
                                list.Add(BuiltObjectSubRole.Cruiser);
                                list.Add(BuiltObjectSubRole.CapitalShip);
                                list.Add(BuiltObjectSubRole.Carrier);
                                List<BuiltObjectSubRole> subRoles = list;
                                BuiltObjectList builtObjectsBySubRole = BuiltObjects.GetBuiltObjectsBySubRole(subRoles);
                                if (builtObjectsBySubRole.Count <= 0)
                                {
                                    break;
                                }
                                int index = Galaxy.Rnd.Next(0, builtObjectsBySubRole.Count);
                                BuiltObject builtObject14 = builtObjectsBySubRole[index];
                                if (builtObject14 != null && !builtObject14.HasBeenDestroyed && CheckLocationSafeForDemoralizingCharacter(flag, builtObject14, character))
                                {
                                    if (character.Location != builtObject14 && transferToLocation)
                                    {
                                        character.TransferToNewLocation(builtObject14, _Galaxy);
                                    }
                                    return builtObject14;
                                }
                                break;
                            }
                            bool flag10 = false;
                            if (!(character.Location is BuiltObject))
                            {
                                flag10 = true;
                            }
                            if (character.Location is BuiltObject)
                            {
                                BuiltObject builtObject15 = (BuiltObject)character.Location;
                                if (builtObject15.Role != BuiltObjectRole.Military && builtObject15.Role != BuiltObjectRole.Freight)
                                {
                                    flag10 = true;
                                }
                            }
                            if (!flag10)
                            {
                                break;
                            }
                            List<BuiltObjectSubRole> list2 = new List<BuiltObjectSubRole>();
                            list2.Add(BuiltObjectSubRole.Escort);
                            list2.Add(BuiltObjectSubRole.Frigate);
                            list2.Add(BuiltObjectSubRole.Destroyer);
                            list2.Add(BuiltObjectSubRole.Cruiser);
                            list2.Add(BuiltObjectSubRole.CapitalShip);
                            list2.Add(BuiltObjectSubRole.Carrier);
                            list2.Add(BuiltObjectSubRole.SmallFreighter);
                            list2.Add(BuiltObjectSubRole.MediumFreighter);
                            list2.Add(BuiltObjectSubRole.LargeFreighter);
                            List<BuiltObjectSubRole> subRoles2 = list2;
                            BuiltObjectList builtObjectsBySubRole2 = BuiltObjects.GetBuiltObjectsBySubRole(subRoles2);
                            if (builtObjectsBySubRole2.Count <= 0)
                            {
                                break;
                            }
                            int index2 = Galaxy.Rnd.Next(0, builtObjectsBySubRole2.Count);
                            BuiltObject builtObject16 = builtObjectsBySubRole2[index2];
                            if (builtObject16 != null && !builtObject16.HasBeenDestroyed && CheckLocationSafeForDemoralizingCharacter(flag, builtObject16, character))
                            {
                                if (character.Location != builtObject16 && transferToLocation)
                                {
                                    character.TransferToNewLocation(builtObject16, _Galaxy);
                                }
                                return builtObject16;
                            }
                            break;
                        }
                    case CharacterRole.FleetAdmiral:
                        {
                            ShipGroup shipGroup5 = character.DetermineFleet();
                            bool flag3 = true;
                            if (shipGroup5 != null)
                            {
                                flag3 = false;
                                if (shipGroup5.Mission == null || shipGroup5.Mission.Type == BuiltObjectMissionType.Undefined || shipGroup5.Mission.Priority == BuiltObjectMissionPriority.Low || shipGroup5.Mission.Priority == BuiltObjectMissionPriority.Normal)
                                {
                                    flag3 = true;
                                }
                            }
                            if (!flag3)
                            {
                                break;
                            }
                            CharacterList charactersByRole5 = Characters.GetCharactersByRole(CharacterRole.FleetAdmiral);
                            ShipGroupList shipGroupList3 = new ShipGroupList();
                            for (int num17 = 0; num17 < charactersByRole5.Count; num17++)
                            {
                                Character character5 = charactersByRole5[num17];
                                ShipGroup shipGroup6 = character5.DetermineFleet();
                                if (shipGroup6 != null && !shipGroupList3.Contains(shipGroup6))
                                {
                                    shipGroupList3.Add(shipGroup6);
                                }
                            }
                            ShipGroup shipGroup7 = null;
                            if (shipGroup5 != null && shipGroup5.Posture == FleetPosture.Attack && CheckLocationSafeForDemoralizingCharacter(flag, shipGroup5, character))
                            {
                                shipGroup7 = shipGroup5;
                            }
                            double num18 = 0.0;
                            bool flag6 = false;
                            ShipGroupList shipGroupList4 = GenerateOrderedFleetsByOverallStrength();
                            if (character.Fighters > character.Countermeasures && character.Fighters > character.Targeting && character.Fighters > character.ShipManeuvering)
                            {
                                flag6 = true;
                                shipGroupList4 = GenerateOrderedFleetsByFighterStrength();
                                if (shipGroup7 != null)
                                {
                                    num18 = shipGroup7.TotalFighterCount;
                                }
                            }
                            else
                            {
                                shipGroupList4 = GenerateOrderedFleetsByOverallStrength();
                                if (shipGroup7 != null)
                                {
                                    num18 = shipGroup7.TotalOverallStrengthFactor;
                                }
                            }
                            for (int num19 = 0; num19 < shipGroupList4.Count; num19++)
                            {
                                ShipGroup shipGroup8 = shipGroupList4[num19];
                                if (shipGroup8.Posture == FleetPosture.Attack && !shipGroupList3.Contains(shipGroup8))
                                {
                                    double num20 = 0.0;
                                    num20 = ((!flag6) ? ((double)shipGroup8.TotalOverallStrengthFactor) : ((double)shipGroup8.TotalFighterCount));
                                    if (num20 > num18 && CheckLocationSafeForDemoralizingCharacter(flag, shipGroup8, character))
                                    {
                                        shipGroup7 = shipGroup8;
                                        num18 = num20;
                                    }
                                }
                            }
                            if (shipGroup7 != null && shipGroup7 != shipGroup5 && shipGroup7.LeadShip != null)
                            {
                                BuiltObject builtObject3 = shipGroup7.DetermineStrongestShip(null, useFleetIndexing: false, null);
                                if (builtObject3 == null)
                                {
                                    builtObject3 = shipGroup7.LeadShip;
                                }
                                if (transferToLocation)
                                {
                                    character.TransferToNewLocation(builtObject3, _Galaxy);
                                }
                                return builtObject3;
                            }
                            if (shipGroup5 == null)
                            {
                                break;
                            }
                            BuiltObject builtObject4 = shipGroup5.DetermineStrongestShip(null, useFleetIndexing: false, null);
                            if (character.Location != builtObject4)
                            {
                                if (transferToLocation)
                                {
                                    character.TransferToNewLocation(builtObject4, _Galaxy);
                                }
                                return builtObject4;
                            }
                            return character.Location;
                        }
                    case CharacterRole.Leader:
                        if (character.Location != Capital)
                        {
                            if (Capital == null)
                            {
                                break;
                            }
                            if (Capital.InvadingTroops == null || Capital.InvadingTroops.Count <= 0)
                            {
                                if (transferToLocation)
                                {
                                    character.TransferToNewLocation(Capital, _Galaxy);
                                }
                                return Capital;
                            }
                            Habitat habitat10 = null;
                            if (character.Location != null && character.Location is Habitat)
                            {
                                habitat10 = (Habitat)character.Location;
                            }
                            if (habitat10 != null && Capitals.Contains(habitat10))
                            {
                                break;
                            }
                            for (int num24 = 0; num24 < Capitals.Count; num24++)
                            {
                                Habitat habitat11 = Capitals[num24];
                                if (habitat11.InvadingTroops == null || habitat11.InvadingTroops.Count <= 0)
                                {
                                    if (transferToLocation)
                                    {
                                        character.TransferToNewLocation(habitat11, _Galaxy);
                                    }
                                    return habitat11;
                                }
                            }
                        }
                        else
                        {
                            if (Capital == null || character.Location != Capital || Capital.InvadingTroops == null || Capital.InvadingTroops.Count <= 0)
                            {
                                break;
                            }
                            for (int num25 = 0; num25 < Capitals.Count; num25++)
                            {
                                Habitat habitat12 = Capitals[num25];
                                if (habitat12.InvadingTroops == null || habitat12.InvadingTroops.Count <= 0)
                                {
                                    if (transferToLocation)
                                    {
                                        character.TransferToNewLocation(habitat12, _Galaxy);
                                    }
                                    return habitat12;
                                }
                            }
                            for (int num26 = 0; num26 < Colonies.Count; num26++)
                            {
                                Habitat habitat13 = Colonies[num26];
                                if (habitat13.InvadingTroops == null || habitat13.InvadingTroops.Count <= 0)
                                {
                                    if (transferToLocation)
                                    {
                                        character.TransferToNewLocation(habitat13, _Galaxy);
                                    }
                                    return habitat13;
                                }
                            }
                        }
                        break;
                    case CharacterRole.Scientist:
                        {
                            Characters.GetCharactersByRole(CharacterRole.Scientist);
                            BuiltObject builtObject7 = null;
                            BuiltObject builtObject8 = IdentifyResearchStationHighestBonus(IndustryType.Weapon);
                            BuiltObject builtObject9 = IdentifyResearchStationHighestBonus(IndustryType.Energy);
                            BuiltObject builtObject10 = IdentifyResearchStationHighestBonus(IndustryType.HighTech);
                            IndustryType industryType = IndustryType.Undefined;
                            if (character.ResearchWeapons > character.ResearchEnergy && character.ResearchWeapons > character.ResearchHighTech)
                            {
                                industryType = IndustryType.Weapon;
                            }
                            else if (character.ResearchEnergy > character.ResearchWeapons && character.ResearchEnergy > character.ResearchHighTech)
                            {
                                industryType = IndustryType.Energy;
                            }
                            else if (character.ResearchHighTech > character.ResearchWeapons && character.ResearchHighTech > character.ResearchEnergy)
                            {
                                industryType = IndustryType.HighTech;
                            }
                            switch (industryType)
                            {
                                case IndustryType.Weapon:
                                    builtObject7 = builtObject8;
                                    if (builtObject7 != null)
                                    {
                                        break;
                                    }
                                    if (character.ResearchEnergy > 0 && character.ResearchEnergy > character.ResearchHighTech && builtObject9 != null)
                                    {
                                        if (CheckLocationSafeForDemoralizingCharacter(flag, builtObject9, character))
                                        {
                                            builtObject7 = builtObject9;
                                        }
                                    }
                                    else if (character.ResearchHighTech > 0 && character.ResearchHighTech > character.ResearchEnergy && builtObject10 != null && CheckLocationSafeForDemoralizingCharacter(flag, builtObject10, character))
                                    {
                                        builtObject7 = builtObject10;
                                    }
                                    break;
                                case IndustryType.Energy:
                                    builtObject7 = builtObject9;
                                    if (builtObject7 != null)
                                    {
                                        break;
                                    }
                                    if (character.ResearchWeapons > 0 && character.ResearchWeapons > character.ResearchHighTech && builtObject8 != null)
                                    {
                                        if (CheckLocationSafeForDemoralizingCharacter(flag, builtObject8, character))
                                        {
                                            builtObject7 = builtObject8;
                                        }
                                    }
                                    else if (character.ResearchHighTech > 0 && character.ResearchHighTech > character.ResearchWeapons && builtObject10 != null && CheckLocationSafeForDemoralizingCharacter(flag, builtObject10, character))
                                    {
                                        builtObject7 = builtObject10;
                                    }
                                    break;
                                case IndustryType.HighTech:
                                    builtObject7 = builtObject10;
                                    if (builtObject7 != null)
                                    {
                                        break;
                                    }
                                    if (character.ResearchEnergy > 0 && character.ResearchEnergy > character.ResearchWeapons && builtObject9 != null)
                                    {
                                        if (CheckLocationSafeForDemoralizingCharacter(flag, builtObject9, character))
                                        {
                                            builtObject7 = builtObject9;
                                        }
                                    }
                                    else if (character.ResearchWeapons > 0 && character.ResearchWeapons > character.ResearchEnergy && builtObject8 != null && CheckLocationSafeForDemoralizingCharacter(flag, builtObject8, character))
                                    {
                                        builtObject7 = builtObject8;
                                    }
                                    break;
                            }
                            if (builtObject7 != null && character.Location != builtObject7)
                            {
                                if (transferToLocation)
                                {
                                    character.TransferToNewLocation(builtObject7, _Galaxy);
                                }
                                return builtObject7;
                            }
                            bool flag7 = false;
                            if (character.Location != null && character.Location is BuiltObject)
                            {
                                BuiltObject builtObject11 = (BuiltObject)character.Location;
                                if (builtObject11.ResearchWeapons > 0 || builtObject11.ResearchEnergy > 0 || builtObject11.ResearchHighTech > 0)
                                {
                                    flag7 = true;
                                }
                            }
                            if (flag7)
                            {
                                break;
                            }
                            for (int num23 = 0; num23 < ResearchFacilities.Count; num23++)
                            {
                                BuiltObject builtObject12 = ResearchFacilities[num23];
                                bool flag8 = false;
                                switch (industryType)
                                {
                                    case IndustryType.Energy:
                                        if (builtObject12.ResearchEnergy > 0)
                                        {
                                            flag8 = true;
                                        }
                                        break;
                                    case IndustryType.HighTech:
                                        if (builtObject12.ResearchHighTech > 0)
                                        {
                                            flag8 = true;
                                        }
                                        break;
                                    case IndustryType.Weapon:
                                        if (builtObject12.ResearchWeapons > 0)
                                        {
                                            flag8 = true;
                                        }
                                        break;
                                }
                                if (flag8 && CheckLocationSafeForDemoralizingCharacter(flag, builtObject12, character))
                                {
                                    if (transferToLocation)
                                    {
                                        character.TransferToNewLocation(builtObject12, _Galaxy);
                                    }
                                    return builtObject12;
                                }
                            }
                            break;
                        }
                    case CharacterRole.TroopGeneral:
                        {
                            if (character.TroopGroundAttack >= character.TroopGroundDefense)
                            {
                                ShipGroup shipGroup = character.DetermineFleet();
                                bool flag2 = true;
                                if (shipGroup != null)
                                {
                                    if (shipGroup.Mission == null || shipGroup.Mission.Type == BuiltObjectMissionType.Undefined || shipGroup.Mission.Priority == BuiltObjectMissionPriority.Low || shipGroup.Mission.Priority == BuiltObjectMissionPriority.Normal)
                                    {
                                        flag2 = true;
                                    }
                                }
                                else if (character.Location != null && character.Location.Empire != null && character.Location.Empire != character.Empire && character.Location is Habitat)
                                {
                                    Habitat habitat3 = (Habitat)character.Location;
                                    if (habitat3.InvadingCharacters != null && habitat3.InvadingCharacters.Contains(character))
                                    {
                                        flag2 = false;
                                    }
                                }
                                if (!flag2)
                                {
                                    break;
                                }
                                CharacterList charactersByRole = Characters.GetCharactersByRole(CharacterRole.TroopGeneral);
                                ShipGroupList shipGroupList = new ShipGroupList();
                                for (int l = 0; l < charactersByRole.Count; l++)
                                {
                                    Character character2 = charactersByRole[l];
                                    ShipGroup shipGroup2 = character2.DetermineFleet();
                                    if (shipGroup2 != null && !shipGroupList.Contains(shipGroup2))
                                    {
                                        shipGroupList.Add(shipGroup2);
                                    }
                                }
                                ShipGroup shipGroup3 = null;
                                if (shipGroup != null && shipGroup.Posture == FleetPosture.Attack && CheckLocationSafeForDemoralizingCharacter(flag, shipGroup, character))
                                {
                                    shipGroup3 = shipGroup;
                                }
                                double num = 0.0;
                                ShipGroupList shipGroupList2 = GenerateOrderedFleetsByTroopAttackStrength();
                                if (shipGroup3 != null)
                                {
                                    num = shipGroup3.TotalTroopAttackStrength;
                                }
                                for (int m = 0; m < shipGroupList2.Count; m++)
                                {
                                    ShipGroup shipGroup4 = shipGroupList2[m];
                                    if (shipGroup4.Posture == FleetPosture.Attack && !shipGroupList.Contains(shipGroup4))
                                    {
                                        double num2 = shipGroup4.TotalTroopAttackStrength;
                                        if (num2 > num && CheckLocationSafeForDemoralizingCharacter(flag, shipGroup4, character))
                                        {
                                            shipGroup3 = shipGroup4;
                                            num = num2;
                                        }
                                    }
                                }
                                if (shipGroup3 == null || shipGroup3 == shipGroup)
                                {
                                    break;
                                }
                                BuiltObject builtObject2 = shipGroup3.DetermineStrongestTroopTransport();
                                if (builtObject2 != null)
                                {
                                    if (transferToLocation)
                                    {
                                        character.TransferToNewLocation(builtObject2, _Galaxy);
                                    }
                                    return builtObject2;
                                }
                                break;
                            }
                            CharacterList charactersByRole2 = Characters.GetCharactersByRole(CharacterRole.TroopGeneral);
                            StellarObjectList stellarObjectList = ResolveLocationsToDefend();
                            StellarObjectList stellarObjectList2 = new StellarObjectList();
                            for (int n = 0; n < stellarObjectList.Count; n++)
                            {
                                if (!(stellarObjectList[n] is Habitat))
                                {
                                    stellarObjectList2.Add(stellarObjectList[n]);
                                }
                            }
                            for (int num3 = 0; num3 < stellarObjectList2.Count; num3++)
                            {
                                stellarObjectList.Remove(stellarObjectList2[num3]);
                            }
                            for (int num4 = 0; num4 < Colonies.Count; num4++)
                            {
                                Habitat habitat4 = Colonies[num4];
                                int strategicValue = habitat4.StrategicValue;
                                if ((strategicValue > 500000 || Capitals.Contains(habitat4)) && !stellarObjectList.Contains(habitat4))
                                {
                                    stellarObjectList.Add(habitat4);
                                }
                            }
                            for (int num5 = 0; num5 < stellarObjectList.Count; num5++)
                            {
                                if (!(stellarObjectList[num5] is Habitat))
                                {
                                    continue;
                                }
                                Habitat habitat5 = (Habitat)stellarObjectList[num5];
                                if (character.Location == habitat5)
                                {
                                    CharacterList characterList = charactersByRole2.FindCharactersAtLocationOrTransferring(habitat5);
                                    bool flag4 = false;
                                    for (int num6 = 0; num6 < characterList.Count; num6++)
                                    {
                                        Character character3 = characterList[num6];
                                        if (character3 != character && character3.TroopGroundDefense > character3.TroopGroundAttack)
                                        {
                                            flag4 = true;
                                            break;
                                        }
                                    }
                                    if (!flag4)
                                    {
                                        break;
                                    }
                                }
                                if (charactersByRole2.FindCharactersAtLocationOrTransferring(habitat5).Count <= 0 && CheckLocationSafeForDemoralizingCharacter(flag, habitat5, character))
                                {
                                    if (transferToLocation)
                                    {
                                        character.TransferToNewLocation(habitat5, _Galaxy);
                                    }
                                    return habitat5;
                                }
                            }
                            break;
                        }
                }
            }
            return character.Location;
        }

        public BuiltObject GenerateNewBuiltObject(Design design, Habitat parentHabitat)
        {
            return GenerateNewBuiltObject(design, parentHabitat, 0.0, 0.0);
        }

        public BuiltObject GenerateNewBuiltObject(Design design, Habitat parentHabitat, double x, double y)
        {
            design.BuildCount++;
            bool isState = _Galaxy.DetermineBuiltObjectIsState(design.SubRole);
            string name = _Galaxy.GenerateBuiltObjectName(design, parentHabitat);
            if (parentHabitat != null)
            {
                x = parentHabitat.Xpos;
                y = parentHabitat.Ypos;
            }
            BuiltObject builtObject = GenerateBuiltObjectFromDesign(design, name, isState, x, y);
            builtObject.DateBuilt = _Galaxy.CurrentStarDate;
            builtObject.DateRetrofit = _Galaxy.CurrentStarDate;
            if (parentHabitat != null)
            {
                builtObject.ParentHabitat = parentHabitat;
                _Galaxy.SelectRelativeParkingPoint(400.0, out var x2, out var y2);
                builtObject.ParentOffsetX = x2;
                builtObject.ParentOffsetY = y2;
            }
            builtObject.Heading = _Galaxy.SelectRandomHeading();
            builtObject.TargetHeading = builtObject.Heading;
            builtObject.ReDefine();
            builtObject.CurrentFuel = builtObject.FuelCapacity;
            builtObject.CurrentShields = builtObject.ShieldsCapacity;
            if (builtObject.TroopCapacity > 0 && Policy != null)
            {
                builtObject.SetTroopLoadoutsFromPolicy(Policy);
            }
            return builtObject;
        }

        public void AddBuiltObjectToGalaxy(BuiltObject builtObject, object parent, bool offsetLocationFromParent, bool isStateOwned)
        {
            AddBuiltObjectToGalaxy(builtObject, parent, offsetLocationFromParent, isStateOwned, -2000000001, -2000000001, sendMessage: true);
        }

        public void AddBuiltObjectToGalaxy(BuiltObject builtObject, object parent, bool offsetLocationFromParent, bool isStateOwned, bool sendMessage)
        {
            AddBuiltObjectToGalaxy(builtObject, parent, offsetLocationFromParent, isStateOwned, -2000000001, -2000000001, sendMessage);
        }

        public void AddBuiltObjectToGalaxy(BuiltObject builtObject, object parent, bool offsetLocationFromParent, bool isStateOwned, int offsetX, int offsetY)
        {
            AddBuiltObjectToGalaxy(builtObject, parent, offsetLocationFromParent, isStateOwned, offsetX, offsetY, sendMessage: true);
        }

        public void AddBuiltObjectToGalaxy(BuiltObject builtObject, object parent, bool offsetLocationFromParent, bool isStateOwned, int offsetX, int offsetY, bool sendMessage)
        {
            builtObject.BuiltObjectID = _Galaxy.GetNextBuiltObjectID();
            builtObject.DateBuilt = _Galaxy.CurrentStarDate;
            builtObject.DateRetrofit = _Galaxy.CurrentStarDate;
            string arg = string.Empty;
            if (parent != null)
            {
                double num = 0.0;
                if (parent is Habitat)
                {
                    num = ((!offsetLocationFromParent) ? 0.0 : ((double)((Habitat)parent).Diameter / 2.0 - Galaxy.Rnd.NextDouble() * (double)((Habitat)parent).Diameter));
                    builtObject.ParentHabitat = (Habitat)parent;
                    builtObject.Xpos = builtObject.ParentHabitat.Xpos;
                    builtObject.Ypos = builtObject.ParentHabitat.Ypos;
                    arg = builtObject.ParentHabitat.Name;
                    if (builtObject.Role == BuiltObjectRole.Base)
                    {
                        ((Habitat)parent).BasesAtHabitat.Add(builtObject);
                    }
                }
                else
                {
                    if (!(parent is BuiltObject))
                    {
                        throw new ApplicationException("Invalid parent type");
                    }
                    num = 0.0;
                    builtObject.ParentBuiltObject = (BuiltObject)parent;
                    builtObject.Xpos = builtObject.ParentBuiltObject.Xpos;
                    builtObject.Ypos = builtObject.ParentBuiltObject.Ypos;
                    arg = builtObject.ParentBuiltObject.Name;
                }
                if (offsetX > -2000000001 && offsetY > -2000000001)
                {
                    builtObject.ParentOffsetX = offsetX;
                    builtObject.ParentOffsetY = offsetY;
                }
                else
                {
                    builtObject.ParentOffsetX = 0.0;
                    builtObject.ParentOffsetY = 0.0;
                    if (offsetLocationFromParent)
                    {
                        double num2 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                        double parentOffsetX = Math.Cos(num2) * num;
                        double parentOffsetY = Math.Sin(num2) * num;
                        builtObject.ParentOffsetX = parentOffsetX;
                        builtObject.ParentOffsetY = parentOffsetY;
                    }
                }
                builtObject.Xpos += builtObject.ParentOffsetX;
                builtObject.Ypos += builtObject.ParentOffsetY;
            }
            string empty = string.Empty;
            empty = ((builtObject.Role != BuiltObjectRole.Base) ? string.Format(TextResolver.GetText("Ship Purchased NAME LOCATION"), builtObject.Name, arg) : string.Format(TextResolver.GetText("Base Purchased NAME LOCATION"), builtObject.Name, arg));
            GalaxyIndex galaxyIndex = Galaxy.ResolveIndex(builtObject.Xpos, builtObject.Ypos);
            int x = galaxyIndex.X;
            int y = galaxyIndex.Y;
            if (PirateEmpireBaseHabitat != null)
            {
                builtObject.PirateEmpireId = (byte)EmpireId;
            }
            if (isStateOwned)
            {
                BuiltObjects.Add(builtObject);
                builtObject.Owner = this;
            }
            else
            {
                PrivateBuiltObjects.Add(builtObject);
            }
            _Galaxy.BuiltObjects.Add(builtObject);
            _Galaxy.BuiltObjectIndex[x][y].Add(builtObject);
            if ((builtObject.SubRole == BuiltObjectSubRole.Outpost || builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort) && builtObject.IsSpacePort && !builtObject.Empire.SpacePorts.Contains(builtObject))
            {
                builtObject.Empire.SpacePorts.Add(builtObject);
            }
            if ((builtObject.SubRole == BuiltObjectSubRole.GasMiningStation || builtObject.SubRole == BuiltObjectSubRole.MiningStation) && builtObject.IsResourceExtractor && !builtObject.Empire.MiningStations.Contains(builtObject))
            {
                builtObject.Empire.MiningStations.Add(builtObject);
            }
            if (builtObject.NearestSystemStar == null)
            {
                Habitat habitat = _Galaxy.FastFindNearestSystem(builtObject.Xpos, builtObject.Ypos);
                if (habitat != null)
                {
                    double num3 = _Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, habitat.Xpos, habitat.Ypos);
                    if ((int)num3 <= Galaxy.MaxSolarSystemSize + 500)
                    {
                        builtObject.NearestSystemStar = habitat;
                    }
                }
            }
            builtObject.Empire.ResolveSystemVisibility(builtObject.Xpos, builtObject.Ypos, null, null);
            builtObject.ReDefine();
            if (builtObject.TroopCapacity > 0 && Policy != null)
            {
                builtObject.SetTroopLoadoutsFromPolicy(Policy);
            }
            if (sendMessage)
            {
                builtObject.Empire.SendMessageToEmpire(builtObject.Empire, EmpireMessageType.ShipBasePurchased, builtObject, empty);
            }
        }

        public bool CanBuiltObjectColonizeHabitat(BuiltObject builtObject, Habitat habitat, out int newPopulationAmount)
        {
            newPopulationAmount = 0;
            if (builtObject.Role != BuiltObjectRole.Colony)
            {
                return false;
            }
            if (habitat.Category != HabitatCategoryType.Planet && habitat.Category != HabitatCategoryType.Moon)
            {
                return false;
            }
            for (int i = 0; i < builtObject.Components.Count; i++)
            {
                BuiltObjectComponent builtObjectComponent = builtObject.Components[i];
                if (builtObjectComponent.Type == ComponentType.HabitationColonization && builtObjectComponent.Status == ComponentStatus.Normal)
                {
                    newPopulationAmount = builtObjectComponent.Value1;
                }
            }
            if (_Galaxy.ShakturiActualRace != null && builtObject.NativeRace != null && builtObject.NativeRace == _Galaxy.ShakturiActualRace)
            {
                newPopulationAmount = 1000000000;
            }
            if (!_Galaxy.CheckEmpireTerritoryCanColonizeHabitat(this, habitat))
            {
                return false;
            }
            if (habitat.Population.TotalAmount > 0 && (habitat.Empire == null || habitat.Empire == _Galaxy.IndependentEmpire))
            {
                return true;
            }
            if (builtObject.SubRole == BuiltObjectSubRole.ColonyShip && builtObject.NativeRace != null && builtObject.NativeRace.NativeHabitatType == habitat.Type)
            {
                if (newPopulationAmount <= 0)
                {
                    newPopulationAmount = 15000000;
                }
                return true;
            }
            return habitat.Type switch
            {
                HabitatType.Continental => CanColonizeContinental,
                HabitatType.MarshySwamp => CanColonizeMarshySwamp,
                HabitatType.Desert => CanColonizeDesert,
                HabitatType.Ocean => CanColonizeOcean,
                HabitatType.Ice => CanColonizeIce,
                HabitatType.Volcanic => CanColonizeVolcanic,
                HabitatType.BarrenRock => false,
                _ => false,
            };
        }

        public int CalculateColonizationPopulation(Design design)
        {
            int num = 0;
            ComponentList componentList = new ComponentList();
            ComponentDefinitionList byType = ComponentDefinition.GetByType(ComponentType.HabitationColonization, Galaxy.ComponentDefinitionsStatic);
            foreach (ComponentDefinition item in byType)
            {
                componentList.Add(new Component(item.ComponentID));
            }
            for (int i = 0; i < componentList.Count; i++)
            {
                Component component = componentList[i];
                for (int j = 0; j < design.Components.Count; j++)
                {
                    Component component2 = design.Components[j];
                    if (component2.ComponentID == component.ComponentID && component2.Value1 > num)
                    {
                        num = component2.Value1;
                    }
                }
            }
            return num;
        }

        public bool CanDesignColonizeHabitat(Design design, Habitat habitat)
        {
            if (design != null && design.Role != BuiltObjectRole.Colony)
            {
                return false;
            }
            if (habitat.Category != HabitatCategoryType.Planet && habitat.Category != HabitatCategoryType.Moon)
            {
                return false;
            }
            if (habitat.Population != null && habitat.Population.TotalAmount > 0 && (habitat.Empire == null || habitat.Empire == _Galaxy.IndependentEmpire))
            {
                return true;
            }
            Empire empire = null;
            if (design != null)
            {
                empire = design.Empire;
            }
            if (empire == null)
            {
                empire = this;
            }
            return habitat.Type switch
            {
                HabitatType.Continental => empire.CanColonizeContinental,
                HabitatType.MarshySwamp => empire.CanColonizeMarshySwamp,
                HabitatType.Desert => empire.CanColonizeDesert,
                HabitatType.Ocean => empire.CanColonizeOcean,
                HabitatType.Ice => empire.CanColonizeIce,
                HabitatType.Volcanic => empire.CanColonizeVolcanic,
                _ => false,
            };
        }

        public List<HabitatType> ColonizableHabitatTypesFromColonyShips(Empire empire, List<HabitatType> empireHabitatTypes)
        {
            for (int i = 0; i < empire.BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = empire.BuiltObjects[i];
                if (builtObject.SubRole != BuiltObjectSubRole.ColonyShip)
                {
                    continue;
                }
                List<HabitatType> list = ColonizableHabitatTypesForBuiltObject(builtObject);
                foreach (HabitatType item in list)
                {
                    if (!empireHabitatTypes.Contains(item))
                    {
                        empireHabitatTypes.Add(item);
                    }
                }
            }
            return empireHabitatTypes;
        }

        public List<HabitatType> ColonizableHabitatTypesForEmpireTechOnly(Empire empire)
        {
            List<HabitatType> list = new List<HabitatType>();
            if (empire.CanColonizeContinental)
            {
                list.Add(HabitatType.Continental);
            }
            if (empire.CanColonizeMarshySwamp)
            {
                list.Add(HabitatType.MarshySwamp);
            }
            if (empire.CanColonizeOcean)
            {
                list.Add(HabitatType.Ocean);
            }
            if (empire.CanColonizeDesert)
            {
                list.Add(HabitatType.Desert);
            }
            if (empire.CanColonizeIce)
            {
                list.Add(HabitatType.Ice);
            }
            if (empire.CanColonizeVolcanic)
            {
                list.Add(HabitatType.Volcanic);
            }
            return list;
        }

        public List<HabitatType> ColonizableHabitatTypesForEmpire(Empire empire)
        {
            List<HabitatType> list = new List<HabitatType>();
            if (empire.CanColonizeContinental)
            {
                list.Add(HabitatType.Continental);
            }
            if (empire.CanColonizeMarshySwamp)
            {
                list.Add(HabitatType.MarshySwamp);
            }
            if (empire.CanColonizeOcean)
            {
                list.Add(HabitatType.Ocean);
            }
            if (empire.CanColonizeDesert)
            {
                list.Add(HabitatType.Desert);
            }
            if (empire.CanColonizeIce)
            {
                list.Add(HabitatType.Ice);
            }
            if (empire.CanColonizeVolcanic)
            {
                list.Add(HabitatType.Volcanic);
            }
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat.Population != null && habitat.Population.TotalAmount >= Galaxy.BuildColonyShipPopulationRequirement)
                {
                    Race dominantRace = habitat.Population.DominantRace;
                    if (dominantRace != null && !list.Contains(dominantRace.NativeHabitatType))
                    {
                        list.Add(dominantRace.NativeHabitatType);
                    }
                }
            }
            return list;
        }

        public List<HabitatType> ColonizableHabitatTypesNonTechForEmpire(Empire empire)
        {
            List<HabitatType> list = new List<HabitatType>();
            List<HabitatType> list2 = ColonizableHabitatTypesForEmpireTechOnly(empire);
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat.Population != null && habitat.Population.TotalAmount >= Galaxy.BuildColonyShipPopulationRequirement)
                {
                    Race dominantRace = habitat.Population.DominantRace;
                    if (dominantRace != null && !list2.Contains(dominantRace.NativeHabitatType) && !list.Contains(dominantRace.NativeHabitatType))
                    {
                        list.Add(dominantRace.NativeHabitatType);
                    }
                }
            }
            return list;
        }

        public List<HabitatType> ColonizableHabitatTypesForBuiltObjectAndEmpire(BuiltObject builtObject)
        {
            List<HabitatType> list = ColonizableHabitatTypesForBuiltObject(builtObject);
            if (builtObject.Empire != null)
            {
                List<HabitatType> list2 = builtObject.Empire.ColonizableHabitatTypesForEmpireTechOnly(builtObject.Empire);
                if (list2 != null)
                {
                    for (int i = 0; i < list2.Count; i++)
                    {
                        if (!list.Contains(list2[i]))
                        {
                            list.Add(list2[i]);
                        }
                    }
                }
            }
            return list;
        }

        public List<HabitatType> ColonizableHabitatTypesForBuiltObject(BuiltObject builtObject)
        {
            List<HabitatType> list = new List<HabitatType>();
            if (builtObject.NativeRace != null && !list.Contains(builtObject.NativeRace.NativeHabitatType))
            {
                list.Add(builtObject.NativeRace.NativeHabitatType);
            }
            return list;
        }

        private void DetermineFriendsAndEnemies(Empire empire, out EmpireList friends, out EmpireList closeFriends, out EmpireList enemies, out EmpireList severeEnemies)
        {
            closeFriends = new EmpireList();
            friends = new EmpireList();
            enemies = new EmpireList();
            severeEnemies = new EmpireList();
            for (int i = 0; i < empire.DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = empire.DiplomaticRelations[i];
                switch (diplomaticRelation.Type)
                {
                    case DiplomaticRelationType.FreeTradeAgreement:
                        friends.Add(diplomaticRelation.OtherEmpire);
                        break;
                    case DiplomaticRelationType.MutualDefensePact:
                    case DiplomaticRelationType.Protectorate:
                        closeFriends.Add(diplomaticRelation.OtherEmpire);
                        break;
                    case DiplomaticRelationType.TradeSanctions:
                        enemies.Add(diplomaticRelation.OtherEmpire);
                        break;
                    case DiplomaticRelationType.War:
                        severeEnemies.Add(diplomaticRelation.OtherEmpire);
                        break;
                }
            }
        }

        private int CalculateRelationshipWithFriends(Empire empire, EmpireList friends, EmpireList closeFriends, out int positiveRelationship, out int negativeRelationship)
        {
            positiveRelationship = 0;
            negativeRelationship = 0;
            foreach (Empire closeFriend in closeFriends)
            {
                DiplomaticRelation diplomaticRelation = closeFriend.DiplomaticRelations[empire];
                if (diplomaticRelation != null)
                {
                    switch (diplomaticRelation.Type)
                    {
                        case DiplomaticRelationType.MutualDefensePact:
                        case DiplomaticRelationType.Protectorate:
                            positiveRelationship += 8;
                            break;
                        case DiplomaticRelationType.FreeTradeAgreement:
                            positiveRelationship += 5;
                            break;
                        case DiplomaticRelationType.SubjugatedDominion:
                            positiveRelationship += 2;
                            break;
                        case DiplomaticRelationType.TradeSanctions:
                        case DiplomaticRelationType.Truce:
                            negativeRelationship -= 7;
                            break;
                        case DiplomaticRelationType.War:
                            negativeRelationship -= 15;
                            break;
                    }
                }
            }
            foreach (Empire friend in friends)
            {
                DiplomaticRelation diplomaticRelation2 = friend.DiplomaticRelations[empire];
                if (diplomaticRelation2 != null)
                {
                    switch (diplomaticRelation2.Type)
                    {
                        case DiplomaticRelationType.MutualDefensePact:
                        case DiplomaticRelationType.Protectorate:
                            positiveRelationship += 5;
                            break;
                        case DiplomaticRelationType.FreeTradeAgreement:
                            positiveRelationship += 3;
                            break;
                        case DiplomaticRelationType.SubjugatedDominion:
                            positiveRelationship++;
                            break;
                        case DiplomaticRelationType.TradeSanctions:
                        case DiplomaticRelationType.Truce:
                            negativeRelationship -= 5;
                            break;
                        case DiplomaticRelationType.War:
                            negativeRelationship -= 10;
                            break;
                    }
                }
            }
            positiveRelationship = Math.Min(positiveRelationship, 30);
            negativeRelationship = Math.Max(negativeRelationship, -30);
            int val = positiveRelationship + negativeRelationship;
            val = Math.Min(20, val);
            return Math.Max(-20, val);
        }

        public TradeOfferResponse EvaluateTradeOffer(Empire offeringEmpire, TradeableItemList offered, TradeableItemList requested, bool disallowCriticalItems)
        {
            if (offeringEmpire != this && !Reclusive && offeringEmpire != null)
            {
                _ = _Galaxy.IntoleranceLevel;
                DiplomaticRelation diplomaticRelation = null;
                EmpireEvaluation empireEvaluation = null;
                PirateRelation pirateRelation = null;
                if (PirateEmpireBaseHabitat == null && offeringEmpire.PirateEmpireBaseHabitat == null)
                {
                    diplomaticRelation = ObtainDiplomaticRelation(offeringEmpire);
                    empireEvaluation = ObtainEmpireEvaluation(offeringEmpire);
                }
                else
                {
                    pirateRelation = ObtainPirateRelation(offeringEmpire);
                }
                double val = (double)(100 + (DominantRace.AggressionLevel - DominantRace.FriendlinessLevel) / 2) / 100.0;
                val = Math.Max(0.97, val);
                int num = (int)((double)requested.TotalValue * val);
                double val2 = (double)(requested.TotalValue + 1) / (double)(offered.TotalValue + 1);
                val2 = Math.Min(10.0, Math.Max(0.2, val2));
                if (diplomaticRelation != null && diplomaticRelation.Type == DiplomaticRelationType.War && offered.ContainsType(TradeableItemType.EndWar))
                {
                    if (diplomaticRelation.Locked)
                    {
                        return TradeOfferResponse.Refuse;
                    }
                    DiplomaticRelationType diplomaticRelationType = DetermineDesiredDiplomaticRelationTypical(diplomaticRelation.Strategy, diplomaticRelation.Type);
                    if (diplomaticRelationType != DiplomaticRelationType.War && offered.TotalValue >= requested.TotalValue)
                    {
                        if (CheckForCapitalTradeItems(requested))
                        {
                            return TradeOfferResponse.Refuse;
                        }
                        return TradeOfferResponse.Accept;
                    }
                    if (offered.ContainsType(TradeableItemType.Colony) || offered.ContainsType(TradeableItemType.Base))
                    {
                        if (offered.TotalValue >= requested.TotalValue)
                        {
                            if (CheckForCapitalTradeItems(requested))
                            {
                                return TradeOfferResponse.Refuse;
                            }
                            return TradeOfferResponse.Accept;
                        }
                        if (offered.TotalValue >= num)
                        {
                            if (CheckForCapitalTradeItems(requested))
                            {
                                return TradeOfferResponse.Refuse;
                            }
                            return TradeOfferResponse.Accept;
                        }
                        if (offered.TotalValue >= requested.TotalValue)
                        {
                            if (CheckForCapitalTradeItems(requested))
                            {
                                return TradeOfferResponse.Refuse;
                            }
                            return TradeOfferResponse.PromptForImprovement;
                        }
                        return TradeOfferResponse.Refuse;
                    }
                }
                if (diplomaticRelation != null && diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions && offered.ContainsType(TradeableItemType.LiftTradeSanctions))
                {
                    if (diplomaticRelation.Locked)
                    {
                        return TradeOfferResponse.Refuse;
                    }
                    DiplomaticRelationType diplomaticRelationType2 = DetermineDesiredDiplomaticRelationTypical(diplomaticRelation.Strategy, diplomaticRelation.Type);
                    if (diplomaticRelationType2 != DiplomaticRelationType.TradeSanctions && diplomaticRelationType2 != DiplomaticRelationType.War && offered.TotalValue >= requested.TotalValue)
                    {
                        if (CheckForCapitalTradeItems(requested))
                        {
                            return TradeOfferResponse.Refuse;
                        }
                        return TradeOfferResponse.Accept;
                    }
                    if (offered.ContainsType(TradeableItemType.Colony) || offered.ContainsType(TradeableItemType.Base))
                    {
                        if (offered.TotalValue >= requested.TotalValue)
                        {
                            if (CheckForCapitalTradeItems(requested))
                            {
                                return TradeOfferResponse.Refuse;
                            }
                            return TradeOfferResponse.Accept;
                        }
                        if (offered.TotalValue >= num)
                        {
                            if (CheckForCapitalTradeItems(requested))
                            {
                                return TradeOfferResponse.Refuse;
                            }
                            return TradeOfferResponse.Accept;
                        }
                        if (offered.TotalValue >= requested.TotalValue)
                        {
                            if (CheckForCapitalTradeItems(requested))
                            {
                                return TradeOfferResponse.Refuse;
                            }
                            return TradeOfferResponse.PromptForImprovement;
                        }
                        return TradeOfferResponse.Refuse;
                    }
                }
                bool flag = true;
                if (disallowCriticalItems && CheckForCriticalTradeItems(requested))
                {
                    flag = false;
                }
                if (diplomaticRelation != null && diplomaticRelation.Type != DiplomaticRelationType.War && offered.ContainsType(TradeableItemType.ThreatenWar))
                {
                    double d = (double)offeringEmpire.WeightedMilitaryPotency / (double)WeightedMilitaryPotency;
                    d = Math.Sqrt(d);
                    d *= 0.7;
                    double num2 = 1.0 + (double)(DominantRace.AggressionLevel - DominantRace.CautionLevel) / 100.0;
                    num2 *= num2;
                    num2 = Math.Max(1.0, num2);
                    offeringEmpire.CivilityRating -= val2 * 0.5;
                    empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - val2 * 3.0;
                    if (d > num2 && Galaxy.Rnd.Next(0, 3) > 0 && empireEvaluation.OverallAttitude > -50)
                    {
                        if (flag)
                        {
                            return TradeOfferResponse.AcceptUnfair;
                        }
                        return TradeOfferResponse.RefuseUnfair;
                    }
                    if (offered.TotalValue >= requested.TotalValue)
                    {
                        if (flag)
                        {
                            return TradeOfferResponse.Accept;
                        }
                        return TradeOfferResponse.Refuse;
                    }
                    return TradeOfferResponse.RefuseUnfair;
                }
                if (diplomaticRelation != null && diplomaticRelation.Type != DiplomaticRelationType.War && diplomaticRelation.Type != DiplomaticRelationType.TradeSanctions && offered.ContainsType(TradeableItemType.ThreatenTradeSanctions))
                {
                    double d2 = (double)offeringEmpire.WeightedMilitaryPotency / (double)WeightedMilitaryPotency;
                    d2 = Math.Sqrt(d2);
                    d2 *= 0.7;
                    double num3 = 1.0 + (double)(DominantRace.AggressionLevel - DominantRace.CautionLevel) / 100.0;
                    num3 *= num3;
                    num3 = Math.Max(1.0, num3);
                    offeringEmpire.CivilityRating -= val2 * 0.3;
                    empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - val2 * 2.0;
                    if (d2 > num3 * 2.0 && Galaxy.Rnd.Next(0, 3) > 0 && empireEvaluation.OverallAttitude > -50)
                    {
                        if (flag)
                        {
                            return TradeOfferResponse.AcceptUnfair;
                        }
                        return TradeOfferResponse.RefuseUnfair;
                    }
                    if (offered.TotalValue >= requested.TotalValue)
                    {
                        if (flag)
                        {
                            return TradeOfferResponse.Accept;
                        }
                        return TradeOfferResponse.Refuse;
                    }
                    return TradeOfferResponse.RefuseUnfair;
                }
                if (!flag)
                {
                    return TradeOfferResponse.Refuse;
                }
                if (offered.TotalValue >= num)
                {
                    double num4 = (double)offered.TotalValue / (double)num;
                    if (num4 > 1.0)
                    {
                        int num5 = offered.TotalValue - num;
                        if (num5 >= 1000)
                        {
                            double num6 = Math.Sqrt(Math.Sqrt(Math.Sqrt(num5))) - 1.37;
                            if (PirateEmpireBaseHabitat == null && offeringEmpire.PirateEmpireBaseHabitat == null)
                            {
                                EmpireEvaluation empireEvaluation2 = ObtainEmpireEvaluation(offeringEmpire);
                                empireEvaluation2.IncidentEvaluation = empireEvaluation2.IncidentEvaluationRaw + num6;
                            }
                            else if (pirateRelation != null)
                            {
                                pirateRelation.EvaluationOffenseOverRequests += (float)num6;
                            }
                        }
                    }
                    return TradeOfferResponse.Accept;
                }
                if (offered.TotalValue >= requested.TotalValue)
                {
                    return TradeOfferResponse.PromptForImprovement;
                }
                if (offered.TotalValue > 0 && !(val2 < 0.5))
                {
                    return TradeOfferResponse.Refuse;
                }
                double d3 = (double)offeringEmpire.WeightedMilitaryPotency / (double)WeightedMilitaryPotency;
                d3 = Math.Sqrt(d3);
                d3 *= 0.7;
                double num7 = 1.0 + (double)(DominantRace.AggressionLevel - DominantRace.CautionLevel) / 100.0;
                num7 *= num7;
                num7 = Math.Max(1.0, num7);
                double val3 = (double)num / 5000.0;
                val3 = Math.Min(val3, 15.0);
                if (empireEvaluation != null && PirateEmpireBaseHabitat == null && offeringEmpire.PirateEmpireBaseHabitat == null)
                {
                    if (offered.TotalValue <= 0)
                    {
                        empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - val3;
                    }
                    else
                    {
                        empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - (double)offered.TotalValue / (double)num * val3;
                    }
                    if (d3 > num7 * 4.0 && Galaxy.Rnd.Next(0, 3) > 1 && empireEvaluation.OverallAttitude > -20)
                    {
                        return TradeOfferResponse.AcceptUnfair;
                    }
                    return TradeOfferResponse.RefuseUnfair;
                }
                if (pirateRelation != null)
                {
                    if (offered.TotalValue <= 0)
                    {
                        pirateRelation.EvaluationOffenseOverRequests -= (float)val3;
                    }
                    else
                    {
                        pirateRelation.EvaluationOffenseOverRequests -= (float)((double)offered.TotalValue / (double)num * val3);
                    }
                    if (d3 > num7 * 4.0 && Galaxy.Rnd.Next(0, 3) > 1 && pirateRelation.Evaluation > -20f)
                    {
                        return TradeOfferResponse.AcceptUnfair;
                    }
                    return TradeOfferResponse.RefuseUnfair;
                }
            }
            return TradeOfferResponse.Refuse;
        }

        private bool CheckForCapitalTradeItems(TradeableItemList items)
        {
            foreach (TradeableItem item in items)
            {
                if (item.Type == TradeableItemType.Colony)
                {
                    Habitat habitat = (Habitat)item.Item;
                    if (habitat == Capital)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckForCriticalTradeItems(TradeableItemList items)
        {
            foreach (TradeableItem item in items)
            {
                if (item.Type == TradeableItemType.Colony)
                {
                    Habitat habitat = (Habitat)item.Item;
                    if (Capitals.Contains(habitat) || habitat == Capital || CapitalSystemStars.Contains(Galaxy.DetermineHabitatSystemStar(habitat)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void ReviewEnemyHelpEnlistment()
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            if (this == _Galaxy.PlayerEmpire)
            {
                return;
            }
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire == this)
                {
                    continue;
                }
                DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                if (diplomaticRelation.Type == DiplomaticRelationType.War)
                {
                    double num = (double)WeightedMilitaryPotency / (double)empire.WeightedMilitaryPotency;
                    if (empire == _Galaxy.PlayerEmpire)
                    {
                        num /= _Galaxy.PlayerEmpire.DifficultyLevel;
                    }
                    if (!(num < 0.9))
                    {
                        continue;
                    }
                    Empire empire2 = IdentifyBestEmpireToAttackEnemy(empire, DiplomaticRelationType.War, currentStarDate);
                    if (empire2 == null)
                    {
                        continue;
                    }
                    TradeableItemList tradeableItemList = new TradeableItemList();
                    int num2 = _Galaxy.ValueDeclareWarOnEmpire(empire2, empire);
                    if (num2 >= 0)
                    {
                        num2 = _Galaxy.UpdateValueDeclareWarOnEmpire(num2, this, empire2, empire);
                        num2 = _Galaxy.RefactorValueForEmpire(num2, this, empire2);
                        tradeableItemList.Add(new TradeableItem(TradeableItemType.DeclareWarOther, empire, num2));
                        TradeableItemList tradeableItems = _Galaxy.ResolveTradeableItems(this, empire2, includeNearestColony: false, refactorValuesForEmpire: true);
                        TradeableItemList tradeableItemList2 = DetermineOfferedTradeItems(num2, tradeableItems, 6);
                        if (tradeableItemList2 != null && tradeableItemList2.Count > 0)
                        {
                            string description = string.Format(TextResolver.GetText("Request help against EMPIRE"), empire.Name);
                            SendMessageToEmpire(empire2, EmpireMessageType.OfferTrade, new object[2] { tradeableItemList2, tradeableItemList }, description);
                            DiplomaticRelation diplomaticRelation2 = ObtainDiplomaticRelation(empire2);
                            diplomaticRelation2.LastDiplomacyTradeOfferDate = currentStarDate;
                        }
                    }
                }
                else
                {
                    if (diplomaticRelation.Type != DiplomaticRelationType.TradeSanctions)
                    {
                        continue;
                    }
                    double num3 = (double)WeightedMilitaryPotency / (double)empire.WeightedMilitaryPotency;
                    if (empire == _Galaxy.PlayerEmpire)
                    {
                        num3 /= _Galaxy.PlayerEmpire.DifficultyLevel;
                    }
                    if (!(num3 < 0.6))
                    {
                        continue;
                    }
                    Empire empire3 = IdentifyBestEmpireToAttackEnemy(empire, DiplomaticRelationType.TradeSanctions, currentStarDate);
                    if (empire3 == null)
                    {
                        continue;
                    }
                    TradeableItemList tradeableItemList3 = new TradeableItemList();
                    int num4 = _Galaxy.ValueInitiateTradeSanctionsAgainstEmpire(empire3, empire);
                    if (num4 >= 0)
                    {
                        num4 = _Galaxy.RefactorValueForEmpire(num4, this, empire3);
                        tradeableItemList3.Add(new TradeableItem(TradeableItemType.InitiateTradeSanctionsOther, empire, num4));
                        TradeableItemList tradeableItems2 = _Galaxy.ResolveTradeableItems(this, empire3, includeNearestColony: false, refactorValuesForEmpire: true);
                        TradeableItemList tradeableItemList4 = DetermineOfferedTradeItems(num4, tradeableItems2, 6);
                        if (tradeableItemList4 != null && tradeableItemList4.Count > 0)
                        {
                            string description2 = string.Format(TextResolver.GetText("Request help against EMPIRE"), empire.Name);
                            SendMessageToEmpire(empire3, EmpireMessageType.OfferTrade, new object[2] { tradeableItemList4, tradeableItemList3 }, description2);
                            DiplomaticRelation diplomaticRelation3 = ObtainDiplomaticRelation(empire3);
                            diplomaticRelation3.LastDiplomacyTradeOfferDate = currentStarDate;
                        }
                    }
                }
            }
        }

        private Empire IdentifyBestEmpireToAttackEnemy(Empire enemyEmpire, DiplomaticRelationType desiredRelationType, long currentStarDate)
        {
            Empire result = null;
            double num = double.MaxValue;
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                long num2 = CalculateNextAllowableProposalDate(diplomaticRelation);
                if (currentStarDate < num2 || diplomaticRelation.Type == DiplomaticRelationType.NotMet || diplomaticRelation.Type == DiplomaticRelationType.War || diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions)
                {
                    continue;
                }
                DiplomaticRelation diplomaticRelation2 = empire.ObtainDiplomaticRelation(enemyEmpire);
                if (diplomaticRelation2.Type != 0 && diplomaticRelation2.Type != DiplomaticRelationType.MutualDefensePact && diplomaticRelation2.Type != DiplomaticRelationType.Protectorate && diplomaticRelation2.Type != DiplomaticRelationType.War && diplomaticRelation2.Type != desiredRelationType)
                {
                    double num3 = _Galaxy.CalculateDistance(empire.Capital.Xpos, empire.Capital.Ypos, enemyEmpire.Capital.Xpos, enemyEmpire.Capital.Ypos);
                    EmpireEvaluation empireEvaluation = empire.ObtainEmpireEvaluation(enemyEmpire);
                    double num4 = (double)empire.WeightedMilitaryPotency / (double)enemyEmpire.WeightedMilitaryPotency;
                    double num5 = empireEvaluation.OverallAttitude + 50;
                    double num6 = num5 * num3 / num4;
                    if (num6 < num)
                    {
                        result = empire;
                        num = num6;
                    }
                }
            }
            return result;
        }

        public void ReviewPirateSystemInfluence()
        {
            List<int> list = new List<int>();
            if (PirateEmpireBaseHabitat != null)
            {
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat = Colonies[i];
                    if (habitat != null && !habitat.HasBeenDestroyed && habitat.GetPirateControl().GetByFaction(this) != null && !list.Contains(habitat.SystemIndex))
                    {
                        list.Add(habitat.SystemIndex);
                    }
                }
                for (int j = 0; j < SpacePorts.Count; j++)
                {
                    BuiltObject builtObject = SpacePorts[j];
                    if (builtObject != null && !builtObject.HasBeenDestroyed && builtObject.Empire == this && builtObject.NearestSystemStar != null && !list.Contains(builtObject.NearestSystemStar.SystemIndex))
                    {
                        list.Add(builtObject.NearestSystemStar.SystemIndex);
                    }
                }
            }
            PirateInfluenceSystemIds = list;
        }

        private void ReviewDisputedTerritory()
        {
            if (this == _Galaxy.PlayerEmpire || DominantRace == null)
            {
                return;
            }
            long currentStarDate = _Galaxy.CurrentStarDate;
            HabitatList habitatList = DetermineEmpireDominatedSystems(this, includeAllTerritory: true);
            _ = _Galaxy.IntoleranceLevel;
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire == this)
                {
                    continue;
                }
                DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                if (diplomaticRelation == null || diplomaticRelation.Type == DiplomaticRelationType.NotMet)
                {
                    continue;
                }
                if (diplomaticRelation.Type == DiplomaticRelationType.War)
                {
                    DiplomaticRelationType diplomaticRelationType = DetermineDesiredDiplomaticRelationTypical(diplomaticRelation.Strategy, diplomaticRelation.Type);
                    if (diplomaticRelationType == DiplomaticRelationType.War)
                    {
                        Galaxy.Rnd.Next(0, 3);
                        _ = 1;
                    }
                }
                long num = CalculateNextAllowableTradeProposalDate(diplomaticRelation);
                if (currentStarDate < num)
                {
                    continue;
                }
                HabitatList habitatList2 = new HabitatList();
                BuiltObjectList builtObjectList = new BuiltObjectList();
                int num2 = 60 - (DominantRace.FriendlinessLevel - DominantRace.CautionLevel);
                EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(empire);
                if (empireEvaluation != null && empireEvaluation.OverallAttitude < num2)
                {
                    BuiltObjectList builtObjectList2 = new BuiltObjectList();
                    builtObjectList2.AddRange(empire.BuiltObjects);
                    builtObjectList2.AddRange(empire.PrivateBuiltObjects);
                    for (int j = 0; j < builtObjectList2.Count; j++)
                    {
                        BuiltObject builtObject = builtObjectList2[j];
                        if (builtObject == null || builtObject.Role != BuiltObjectRole.Base || builtObject.BuiltAt != null || builtObject.UnbuiltComponentCount > 0 || (builtObject.SubRole != BuiltObjectSubRole.MiningStation && builtObject.SubRole != BuiltObjectSubRole.GasMiningStation && builtObject.SubRole != BuiltObjectSubRole.GenericBase && builtObject.SubRole != BuiltObjectSubRole.EnergyResearchStation && builtObject.SubRole != BuiltObjectSubRole.WeaponsResearchStation && builtObject.SubRole != BuiltObjectSubRole.HighTechResearchStation && builtObject.SubRole != BuiltObjectSubRole.MonitoringStation && builtObject.SubRole != BuiltObjectSubRole.DefensiveBase && builtObject.SubRole != BuiltObjectSubRole.ResortBase) || builtObject.NearestSystemStar == null || !habitatList.Contains(builtObject.NearestSystemStar))
                        {
                            continue;
                        }
                        if (builtObject.SubRole == BuiltObjectSubRole.MiningStation || builtObject.SubRole == BuiltObjectSubRole.GasMiningStation)
                        {
                            if (builtObject.ParentHabitat != null)
                            {
                                builtObject.SortTag = 100.0 * DetermineResourceValue(builtObject.ParentHabitat);
                            }
                            else
                            {
                                builtObject.SortTag = 1000.0;
                            }
                        }
                        else if (builtObject.SubRole == BuiltObjectSubRole.ResortBase)
                        {
                            builtObject.SortTag = 10000.0;
                        }
                        else
                        {
                            builtObject.SortTag = 10000.0;
                        }
                        builtObjectList.Add(builtObject);
                    }
                }
                for (int k = 0; k < habitatList.Count; k++)
                {
                    Habitat habitat = habitatList[k];
                    if (habitat == null || habitat.SystemIndex < 0 || habitat.SystemIndex >= _Galaxy.Systems.Count)
                    {
                        continue;
                    }
                    SystemInfo systemInfo = _Galaxy.Systems[habitat.SystemIndex];
                    if (systemInfo == null || !systemInfo.IsDisputed || systemInfo.DominantEmpire == null || systemInfo.OtherEmpires == null || (systemInfo.DominantEmpire.Empire != empire && !systemInfo.OtherEmpires.Contains(empire)) || systemInfo.DominantEmpire.Empire != this || systemInfo.Habitats == null)
                    {
                        continue;
                    }
                    for (int l = 0; l < systemInfo.Habitats.Count; l++)
                    {
                        Habitat habitat2 = systemInfo.Habitats[l];
                        if (habitat2 != null && habitat2.Owner == empire && habitat2.Owner.Capital != habitat2)
                        {
                            habitatList2.Add(habitat2);
                        }
                    }
                }
                habitatList2.Sort();
                habitatList2.Reverse();
                builtObjectList.Sort();
                builtObjectList.Reverse();
                TradeableItemList tradeableItemList = new TradeableItemList();
                TradeableItemList tradeableItemList2 = new TradeableItemList();
                if (habitatList2.Count > 0 && Galaxy.Rnd.Next(0, 3) > 0)
                {
                    TradeableItemList tradeableItems = _Galaxy.ResolveTradeableItems(this, empire, includeNearestColony: false, refactorValuesForEmpire: true);
                    int num3 = _Galaxy.ValueColonyForEmpire(habitatList2[0], this);
                    if (num3 >= 0)
                    {
                        num3 = _Galaxy.RefactorValueForEmpire(num3, this, empire);
                    }
                    tradeableItemList.Add(new TradeableItem(TradeableItemType.Colony, habitatList2[0], num3));
                    Habitat systemToExclude = Galaxy.DetermineHabitatSystemStar(habitatList2[0]);
                    tradeableItemList2 = DetermineOfferedTradeItemsForTarget(num3, systemToExclude, tradeableItems, diplomaticRelation, empireEvaluation);
                }
                else if (builtObjectList.Count > 0)
                {
                    TradeableItemList tradeableItems2 = _Galaxy.ResolveTradeableItems(this, empire, includeNearestColony: false, refactorValuesForEmpire: true);
                    int num4 = _Galaxy.ValueBaseForEmpire(builtObjectList[0], this);
                    if (num4 >= 0)
                    {
                        num4 = _Galaxy.RefactorValueForEmpire(num4, this, empire);
                    }
                    tradeableItemList.Add(new TradeableItem(TradeableItemType.Base, builtObjectList[0], num4));
                    tradeableItemList2 = DetermineOfferedTradeItemsForTarget(num4, builtObjectList[0].NearestSystemStar, tradeableItems2, diplomaticRelation, empireEvaluation);
                }
                bool flag = true;
                if (DominantRace != null)
                {
                    flag = DominantRace.Expanding;
                }
                if (tradeableItemList.Count > 0 && tradeableItemList2 != null)
                {
                    string empty = string.Empty;
                    if (tradeableItemList2.Count > 0 && flag)
                    {
                        TradeableItemList tradeableItemList3 = tradeableItemList2.ExtractHighOrderedItemsByType(new TradeableItemType[2]
                        {
                        TradeableItemType.ThreatenWar,
                        TradeableItemType.ThreatenTradeSanctions
                        });
                        empty = ((tradeableItemList3.Count <= 0) ? string.Format(TextResolver.GetText("Trade Offer"), tradeableItemList2.ToString(), tradeableItemList.ToString()) : string.Format(TextResolver.GetText("Trade Demand Threat"), tradeableItemList.ToString(), tradeableItemList2.ToString()));
                    }
                    else
                    {
                        empty = string.Format(TextResolver.GetText("Trade Demand"), tradeableItemList.ToString());
                    }
                    SendMessageToEmpire(empire, EmpireMessageType.OfferTrade, new object[2] { tradeableItemList2, tradeableItemList }, empty);
                    diplomaticRelation.LastTradeDealOfferDate = currentStarDate;
                }
            }
        }

        public long CalculateNextAllowableProposalDate(DiplomaticRelation relation)
        {
            long num = (long)((double)Galaxy.RealSecondsInGalacticYear * 1000.0 * (Galaxy.MinimumDiplomacyTradeProposalIntervalYears * _Galaxy.ColonyFillFactor));
            int num2 = _Galaxy.Empires.Count;
            if (relation.OtherEmpire != null && relation.OtherEmpire.DiplomaticRelations != null)
            {
                num2 = relation.OtherEmpire.DiplomaticRelations.CountMet();
            }
            double val = (double)num2 / (double)_Galaxy.Empires.Count;
            val = Math.Max(0.3, Math.Min(1.0, val));
            num = (long)((double)num * val * 2.0);
            return relation.LastDiplomacyTradeOfferDate + num;
        }

        public long CalculateNextAllowableTradeProposalDate(DiplomaticRelation relation)
        {
            long num = (long)((double)Galaxy.RealSecondsInGalacticYear * 1000.0 * (Galaxy.MinimumDiplomacyTradeProposalIntervalYears * _Galaxy.ColonyFillFactor));
            int num2 = _Galaxy.Empires.Count;
            if (relation.OtherEmpire != null && relation.OtherEmpire.DiplomaticRelations != null)
            {
                num2 = relation.OtherEmpire.DiplomaticRelations.CountMet();
            }
            double val = (double)num2 / (double)_Galaxy.Empires.Count;
            val = Math.Max(0.3, Math.Min(1.0, val));
            num = (long)((double)num * val * 2.0);
            return relation.LastTradeDealOfferDate + num;
        }

        public long CalculateNextAllowableProposalDate(PirateRelation relation)
        {
            long num = (long)((double)Galaxy.RealSecondsInGalacticYear * 1000.0 * 1.0);
            int num2 = _Galaxy.PirateEmpires.Count;
            if (relation.OtherEmpire != null && relation.OtherEmpire.PirateRelations != null)
            {
                num2 = relation.OtherEmpire.PirateRelations.CountKnownPirateFactions();
            }
            double val = (double)num2 / (double)_Galaxy.PirateEmpires.Count;
            val = Math.Max(0.3, Math.Min(1.0, val));
            num = (long)((double)num * val * 3.0);
            return relation.LastOfferDate + num;
        }

        public long CalculateNextAllowableChangeDate(PirateRelation relation)
        {
            long num = (long)((double)Galaxy.RealSecondsInGalacticYear * 1000.0 * 1.0);
            int num2 = _Galaxy.PirateEmpires.Count;
            if (relation.OtherEmpire != null && relation.OtherEmpire.PirateRelations != null)
            {
                num2 = relation.OtherEmpire.PirateRelations.CountKnownPirateFactions();
            }
            double val = (double)num2 / (double)_Galaxy.PirateEmpires.Count;
            val = Math.Max(0.3, Math.Min(1.0, val));
            num = (long)((double)num * val * 3.0);
            return relation.LastChangeDate + num;
        }

        private TradeableItemList DetermineOfferedTradeItemsForTarget(int value, Habitat systemToExclude, TradeableItemList tradeableItems, DiplomaticRelation relation, EmpireEvaluation evaluation)
        {
            TradeableItemList tradeableItemList = new TradeableItemList();
            if (systemToExclude != null)
            {
                TradeableItemList tradeableItemList2 = new TradeableItemList();
                foreach (TradeableItem tradeableItem in tradeableItems)
                {
                    if (tradeableItem.Type == TradeableItemType.Base)
                    {
                        BuiltObject builtObject = (BuiltObject)tradeableItem.Item;
                        if (builtObject.NearestSystemStar == systemToExclude)
                        {
                            tradeableItemList2.Add(tradeableItem);
                        }
                    }
                    else if (tradeableItem.Type == TradeableItemType.Colony)
                    {
                        Habitat habitat = (Habitat)tradeableItem.Item;
                        Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                        if (habitat2 == systemToExclude)
                        {
                            tradeableItemList2.Add(tradeableItem);
                        }
                    }
                }
                if (tradeableItemList2.Count > 0)
                {
                    foreach (TradeableItem item in tradeableItemList2)
                    {
                        tradeableItems.Remove(item);
                    }
                }
            }
            double num = Math.Sqrt((double)DominantRace.FriendlinessLevel / 100.0);
            num += Galaxy.Rnd.NextDouble() * 0.05;
            num = Math.Min(1.1, num);
            int valueWillingToPay = (int)((double)value * num);
            if (relation.Type == DiplomaticRelationType.War)
            {
                TradeableItemList tradeableItemList3 = tradeableItems.ExtractHighOrderedItemsByType(TradeableItemType.EndWar);
                if (tradeableItemList3.Count > 0)
                {
                    tradeableItemList.AddRange(tradeableItemList3);
                }
            }
            else if (relation.Type == DiplomaticRelationType.TradeSanctions)
            {
                TradeableItemList tradeableItemList4 = tradeableItems.ExtractHighOrderedItemsByType(TradeableItemType.LiftTradeSanctions);
                if (tradeableItemList4.Count > 0)
                {
                    tradeableItemList.AddRange(tradeableItemList4);
                }
            }
            else if ((DominantRace.AggressionLevel >= 125 || evaluation.OverallAttitude < -45) && relation.Strategy == DiplomaticStrategy.Conquer && Galaxy.Rnd.Next(0, 2) == 1)
            {
                TradeableItemList tradeableItemList5 = tradeableItems.ExtractHighOrderedItemsByType(TradeableItemType.ThreatenWar);
                TradeableItemList tradeableItemList6 = tradeableItems.ExtractHighOrderedItemsByType(TradeableItemType.ThreatenTradeSanctions);
                if (tradeableItemList5.Count > 0)
                {
                    tradeableItemList.AddRange(tradeableItemList5);
                }
                else if (tradeableItemList6.Count > 0)
                {
                    tradeableItemList.AddRange(tradeableItemList6);
                }
            }
            else if ((DominantRace.AggressionLevel < 115 && evaluation.OverallAttitude >= -25) || (relation.Strategy != DiplomaticStrategy.Conquer && relation.Strategy != DiplomaticStrategy.Punish) || Galaxy.Rnd.Next(0, 3) <= 0)
            {
                tradeableItemList = DetermineOfferedTradeItems(valueWillingToPay, tradeableItems, 6);
            }
            return tradeableItemList;
        }

        private TradeableItemList DetermineOfferedTradeItems(int valueWillingToPay, TradeableItemList tradeableItems, int maximumItems)
        {
            TradeableItemList tradeableItemList = new TradeableItemList();
            int num = (int)((double)valueWillingToPay * 1.5);
            TradeableItemList tradeableItemList2 = tradeableItems.ExtractHighOrderedItemsByType(new TradeableItemType[2]
            {
            TradeableItemType.EndWar,
            TradeableItemType.LiftTradeSanctions
            });
            if (tradeableItemList2.Count > 0 && tradeableItemList2.TotalValue >= valueWillingToPay && tradeableItemList2.TotalValue < num)
            {
                if (tradeableItemList.Count < maximumItems)
                {
                    tradeableItemList.AddRange(tradeableItemList2);
                }
                return tradeableItemList;
            }
            if (tradeableItemList.TotalValue < valueWillingToPay)
            {
                TradeableItemList tradeableItemList3 = tradeableItems.ExtractHighOrderedItemsByType(TradeableItemType.Colony);
                for (int i = 0; i < tradeableItemList3.Count; i++)
                {
                    if (tradeableItemList.TotalValue < valueWillingToPay && tradeableItemList.TotalValue + tradeableItemList3[i].Value < num && tradeableItemList.Count < maximumItems)
                    {
                        tradeableItemList.Add(tradeableItemList3[i]);
                    }
                }
            }
            if (tradeableItemList.TotalValue < valueWillingToPay)
            {
                TradeableItemList tradeableItemList4 = tradeableItems.ExtractHighOrderedItemsByType(TradeableItemType.Base);
                for (int j = 0; j < tradeableItemList4.Count; j++)
                {
                    if (tradeableItemList.TotalValue < valueWillingToPay && tradeableItemList.TotalValue + tradeableItemList4[j].Value < num && tradeableItemList.Count < maximumItems)
                    {
                        tradeableItemList.Add(tradeableItemList4[j]);
                    }
                }
            }
            if (tradeableItemList.TotalValue < valueWillingToPay)
            {
                TradeableItemList tradeableItemList5 = tradeableItems.ExtractHighOrderedItemsByType(TradeableItemType.ResearchProject);
                for (int k = 0; k < tradeableItemList5.Count; k++)
                {
                    if (tradeableItemList.TotalValue < valueWillingToPay && tradeableItemList.TotalValue + tradeableItemList5[k].Value < num && tradeableItemList.Count < maximumItems)
                    {
                        tradeableItemList.Add(tradeableItemList5[k]);
                    }
                }
            }
            if (tradeableItemList.TotalValue < valueWillingToPay)
            {
                TradeableItemList tradeableItemList6 = tradeableItems.ExtractHighOrderedItemsByType(TradeableItemType.TerritoryMap);
                TradeableItemList tradeableItemList7 = tradeableItems.ExtractHighOrderedItemsByType(TradeableItemType.GalaxyMap);
                if (tradeableItemList6.Count > 0 && tradeableItemList.TotalValue + tradeableItemList6.TotalValue < num && tradeableItemList.Count < maximumItems)
                {
                    tradeableItemList.AddRange(tradeableItemList6);
                }
                if (tradeableItemList7.Count > 0 && tradeableItemList.TotalValue < valueWillingToPay && tradeableItemList.TotalValue + tradeableItemList7.TotalValue < num)
                {
                    if (tradeableItemList6.Count > 0)
                    {
                        tradeableItemList.Remove(tradeableItemList6[0]);
                    }
                    if (tradeableItemList.Count < maximumItems)
                    {
                        tradeableItemList.AddRange(tradeableItemList7);
                    }
                }
            }
            if (tradeableItemList.TotalValue < valueWillingToPay)
            {
                int num2 = (int)(StateMoney * 0.2);
                if (num2 > 0)
                {
                    int val = valueWillingToPay - tradeableItemList.TotalValue;
                    val = Math.Min(val, num2);
                    if (tradeableItemList.TotalValue + val < num && tradeableItemList.Count < maximumItems)
                    {
                        tradeableItemList.Add(new TradeableItem(TradeableItemType.Money, (double)val, val));
                    }
                }
            }
            if (tradeableItemList.TotalValue < valueWillingToPay)
            {
                tradeableItemList.Clear();
                tradeableItemList = null;
            }
            return tradeableItemList;
        }

        public void TradeItems()
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            long num = currentStarDate - (long)(Galaxy.MinimumDiplomacyTradeProposalIntervalYears * _Galaxy.ColonyFillFactor * (double)Galaxy.RealSecondsInGalacticYear * 1000.0);
            if (this == _Galaxy.PlayerEmpire)
            {
                return;
            }
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation.Type == DiplomaticRelationType.NotMet || diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions || diplomaticRelation.Type == DiplomaticRelationType.War || diplomaticRelation.LastTradeDealOfferDate > num || (diplomaticRelation.Strategy != DiplomaticStrategy.Ally && diplomaticRelation.Strategy != DiplomaticStrategy.Befriend))
                {
                    continue;
                }
                EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(diplomaticRelation.OtherEmpire);
                int overallAttitude = empireEvaluation.OverallAttitude;
                TradeableItemList tradeableItemList = new TradeableItemList();
                int num2 = Galaxy.TradeResearchThreshhold;
                if (diplomaticRelation.OtherEmpire == _Galaxy.PlayerEmpire)
                {
                    num2 = (int)((double)Galaxy.TradeResearchThreshhold * _Galaxy.DifficultyLevel);
                }
                if (diplomaticRelation.Strategy == DiplomaticStrategy.Ally && overallAttitude >= num2 && _Galaxy.AllowTechTrading)
                {
                    int num3 = Galaxy.TradeResearchSpecialThreshhold;
                    if (diplomaticRelation.OtherEmpire == _Galaxy.PlayerEmpire)
                    {
                        num3 = (int)((double)Galaxy.TradeResearchSpecialThreshhold * _Galaxy.DifficultyLevel);
                    }
                    bool includeSpecialTech = false;
                    if (overallAttitude >= num3)
                    {
                        includeSpecialTech = true;
                    }
                    tradeableItemList.AddRange(_Galaxy.ResolveTradeableItemsResearchProjects(this, diplomaticRelation.OtherEmpire, refactorValuesForEmpire: true, includeSpecialTech));
                }
                if (diplomaticRelation.Type != DiplomaticRelationType.MutualDefensePact && diplomaticRelation.Type != DiplomaticRelationType.Protectorate)
                {
                    tradeableItemList.AddRange(_Galaxy.ResolveTradeableItemsMaps(this, diplomaticRelation.OtherEmpire, refactorValuesForEmpire: true));
                }
                if (tradeableItemList.Count <= 0)
                {
                    continue;
                }
                int index = Galaxy.Rnd.Next(0, tradeableItemList.Count);
                TradeableItem tradeableItem = tradeableItemList[index];
                bool flag = true;
                string description = "We offer ";
                switch (tradeableItem.Type)
                {
                    case TradeableItemType.TerritoryMap:
                        {
                            description = string.Format(TextResolver.GetText("Trade Swap Maps"), tradeableItem.ToString());
                            int offeredValue2 = _Galaxy.RefactorValueForEmpire(_Galaxy.ValueTerritoryMapForEmpire(diplomaticRelation.OtherEmpire, this), this, diplomaticRelation.OtherEmpire);
                            flag = DetermineAcceptTerritoryMapTrade(offeredValue2, diplomaticRelation.OtherEmpire);
                            break;
                        }
                    case TradeableItemType.GalaxyMap:
                        {
                            description = string.Format(TextResolver.GetText("Trade Swap Maps"), tradeableItem.ToString());
                            int offeredValue = _Galaxy.RefactorValueForEmpire(_Galaxy.ValueGalaxyMapForEmpire(diplomaticRelation.OtherEmpire, this), this, diplomaticRelation.OtherEmpire);
                            flag = DetermineAcceptGalaxyMapTrade(offeredValue, diplomaticRelation.OtherEmpire);
                            break;
                        }
                    case TradeableItemType.ResearchProject:
                        {
                            string arg = tradeableItem.ToString();
                            if (tradeableItem.Item is ResearchNode)
                            {
                                ResearchNode researchNode = (ResearchNode)tradeableItem.Item;
                                arg = researchNode.Name;
                            }
                            description = string.Format(TextResolver.GetText("Trade Tech"), arg, tradeableItem.Value.ToString());
                            double num4 = (double)tradeableItem.Value * 1.2;
                            if (diplomaticRelation.OtherEmpire.StateMoney < num4)
                            {
                                flag = false;
                            }
                            break;
                        }
                }
                if (flag)
                {
                    diplomaticRelation.LastTradeDealOfferDate = currentStarDate;
                    SendMessageToEmpire(diplomaticRelation.OtherEmpire, EmpireMessageType.OfferTrade, tradeableItem, description);
                }
            }
        }

        public void PirateTradeItems()
        {
            long currentStarDate = _Galaxy.CurrentStarDate;
            _ = Galaxy.RealSecondsInGalacticYear;
            if (this == _Galaxy.PlayerEmpire)
            {
                return;
            }
            for (int i = 0; i < PirateRelations.Count; i++)
            {
                PirateRelation pirateRelation = PirateRelations[i];
                if (pirateRelation.Type == PirateRelationType.NotMet || pirateRelation.OtherEmpire == null)
                {
                    continue;
                }
                long num = (long)((double)Galaxy.RealSecondsInGalacticYear * 2.0 * 1000.0);
                int num2 = pirateRelation.OtherEmpire.PirateRelations.CountKnownPirateFactions();
                double num3 = Math.Min(7.0, Math.Max(1.0, (double)num2 / 3.0));
                num = (long)((double)num * num3);
                long num4 = (long)((double)Galaxy.RealSecondsInGalacticYear * 0.5);
                long num5 = (long)(((double)num4 * 0.5 + (double)num4 * 0.5 * Galaxy.Rnd.NextDouble()) * num3);
                num += num5;
                long num6 = currentStarDate - num;
                if (pirateRelation.LastInfoDate > num6)
                {
                    continue;
                }
                int num7 = (int)pirateRelation.Evaluation;
                TradeableItemList tradeableItemList = new TradeableItemList();
                if (_Galaxy.AllowTechTrading)
                {
                    int num8 = 0;
                    if (pirateRelation.OtherEmpire == _Galaxy.PlayerEmpire && _Galaxy.DifficultyLevel > 1.0)
                    {
                        num8 = (int)(20.0 * (_Galaxy.DifficultyLevel - 1.0));
                    }
                    if (num7 >= num8)
                    {
                        bool includeSpecialTech = false;
                        int num9 = 30;
                        if (pirateRelation.OtherEmpire == _Galaxy.PlayerEmpire)
                        {
                            num9 = (int)(30.0 * _Galaxy.DifficultyLevel);
                        }
                        if (num7 >= num9)
                        {
                            includeSpecialTech = true;
                        }
                        tradeableItemList.AddRange(_Galaxy.ResolveTradeableItemsResearchProjects(this, pirateRelation.OtherEmpire, refactorValuesForEmpire: true, includeSpecialTech, includeWarpColonizationWeapons: false));
                    }
                }
                if (tradeableItemList.Count <= 0)
                {
                    continue;
                }
                int index = Galaxy.Rnd.Next(0, tradeableItemList.Count);
                TradeableItem tradeableItem = tradeableItemList[index];
                bool flag = true;
                string description = "We offer ";
                TradeableItemType type = tradeableItem.Type;
                if (type == TradeableItemType.ResearchProject)
                {
                    string arg = tradeableItem.ToString();
                    ResearchNode researchNode = null;
                    if (tradeableItem.Item is ResearchNode)
                    {
                        researchNode = (ResearchNode)tradeableItem.Item;
                        arg = researchNode.Name;
                    }
                    description = string.Format(TextResolver.GetText("Trade Tech"), arg, tradeableItem.Value.ToString());
                    double num10 = (double)tradeableItem.Value * 1.2;
                    if (pirateRelation.OtherEmpire.StateMoney < num10)
                    {
                        flag = false;
                    }
                    if (pirateRelation.OtherEmpire.Research != null && researchNode != null)
                    {
                        ResearchNode researchNode2 = pirateRelation.OtherEmpire.Research.TechTree.FindNodeById(researchNode.ResearchNodeId);
                        if (researchNode2 != null && (researchNode2.IsResearched || researchNode2.Progress > 0.8f))
                        {
                            flag = false;
                        }
                    }
                }
                if (flag)
                {
                    pirateRelation.LastInfoDate = currentStarDate;
                    SendMessageToEmpire(pirateRelation.OtherEmpire, EmpireMessageType.OfferTrade, tradeableItem, description);
                }
            }
        }

        public bool DetermineAcceptTerritoryMapTrade(int offeredValue, Empire offeringEmpire)
        {
            bool flag = false;
            if (PirateEmpireBaseHabitat == null && offeringEmpire.PirateEmpireBaseHabitat == null)
            {
                EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(offeringEmpire);
                if (empireEvaluation.OverallAttitude >= Galaxy.TradeTerritoryMapThreshhold && !Reclusive)
                {
                    flag = true;
                }
            }
            else
            {
                PirateRelation pirateRelation = ObtainPirateRelation(offeringEmpire);
                if (pirateRelation.Evaluation >= (float)Galaxy.TradeTerritoryMapThreshhold && !Reclusive)
                {
                    flag = true;
                }
            }
            if (flag)
            {
                int num = _Galaxy.RefactorValueForEmpire(_Galaxy.ValueTerritoryMapForEmpire(this, offeringEmpire), offeringEmpire, this);
                int num2 = (int)((double)num * 0.75);
                if (offeredValue >= num2)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckKnowledgeOfSecretLocations(Empire otherEmpire)
        {
            GalaxyLocationList galaxyLocationList = new GalaxyLocationList();
            galaxyLocationList.AddRange(KnownGalaxyLocations.FindLocations(GalaxyLocationType.RestrictedArea));
            galaxyLocationList.AddRange(KnownGalaxyLocations.FindLocations(GalaxyLocationType.DebrisField));
            galaxyLocationList.AddRange(KnownGalaxyLocations.FindLocations(GalaxyLocationType.PlanetDestroyer));
            foreach (GalaxyLocation item in galaxyLocationList)
            {
                if (!otherEmpire.KnownGalaxyLocations.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        public int ObtainAttitude(Empire empire)
        {
            int result = 0;
            if (empire != null)
            {
                if (PirateEmpireBaseHabitat == null && empire.PirateEmpireBaseHabitat == null)
                {
                    EmpireEvaluation empireEvaluation = ObtainEmpireEvaluation(empire);
                    result = empireEvaluation.OverallAttitude;
                }
                else
                {
                    PirateRelation pirateRelation = ObtainPirateRelation(empire);
                    result = (int)pirateRelation.Evaluation;
                }
            }
            return result;
        }

        public bool DetermineAcceptGalaxyMapTrade(int offeredValue, Empire offeringEmpire)
        {
            int num = ObtainAttitude(offeringEmpire);
            if (num >= Galaxy.TradeGalaxyMapThreshhold && !Reclusive)
            {
                int num2 = _Galaxy.RefactorValueForEmpire(_Galaxy.ValueGalaxyMapForEmpire(this, offeringEmpire), offeringEmpire, this);
                int num3 = (int)((double)num2 * 0.85);
                if (offeredValue >= num3 && !CheckKnowledgeOfSecretLocations(offeringEmpire))
                {
                    return true;
                }
            }
            return false;
        }

        public HabitatList DetermineEmpireSystems(Empire empire)
        {
            return DetermineEmpireSystems(empire, mustOwnColonies: false);
        }

        public HabitatList DetermineEmpireSystems(Empire empire, bool mustOwnColonies)
        {
            HabitatList habitatList = new HabitatList();
            for (int i = 0; i < empire.Colonies.Count; i++)
            {
                Habitat habitat = empire.Colonies[i];
                if (!mustOwnColonies || habitat.Owner == empire)
                {
                    Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                    if (habitat2 != null && habitatList.IndexOf(habitat2) < 0)
                    {
                        habitatList.Add(habitat2);
                    }
                }
            }
            return habitatList;
        }

        public HabitatList DetermineEmpireDominatedSystems(Empire empire, bool includeAllTerritory)
        {
            HabitatList habitatList = new HabitatList();
            if (includeAllTerritory)
            {
                for (int i = 0; i < _Galaxy.Systems.Count; i++)
                {
                    SystemInfo systemInfo = _Galaxy.Systems[i];
                    if (systemInfo != null && systemInfo.SystemStar != null)
                    {
                        bool disputed = false;
                        int num = _Galaxy.EmpireTerritory.CheckSystemOwnership(_Galaxy, systemInfo.SystemStar, out disputed);
                        if (num == empire.EmpireId && habitatList.IndexOf(systemInfo.SystemStar) < 0)
                        {
                            habitatList.Add(systemInfo.SystemStar);
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < empire.Colonies.Count; j++)
                {
                    Habitat habitat = empire.Colonies[j];
                    Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                    if (habitat2 != null)
                    {
                        EmpireSystemSummary dominantEmpire = _Galaxy.Systems[habitat2.SystemIndex].DominantEmpire;
                        if (dominantEmpire != null && dominantEmpire.Empire != null && dominantEmpire.Empire == empire && habitatList.IndexOf(habitat2) < 0)
                        {
                            habitatList.Add(habitat2);
                        }
                    }
                }
            }
            return habitatList;
        }

        private int CalculateSystemCompetition(Empire empire, HabitatList ourSystemStars)
        {
            return BaconEmpire.CalculateSystemCompetition(this, empire, ourSystemStars);
        }

        private int CalculateTradeVolume(Empire empire)
        {
            DiplomaticRelation diplomaticRelation = DiplomaticRelations[empire];
            if (diplomaticRelation == null)
            {
                diplomaticRelation = new DiplomaticRelation(DiplomaticRelationType.NotMet, this, this, empire, tradeRestrictedResources: false);
            }
            int val = (int)(diplomaticRelation.NormalizedAnnualTradeValue / 4000.0);
            return Math.Min(25, val);
        }

        public void SendMessageToEmpire(Empire recipientEmpire, EmpireMessageType messageType, object subject, string description)
        {
            SendMessageToEmpire(recipientEmpire, messageType, subject, description, Point.Empty, string.Empty);
        }

        public void SendMessageToEmpireWithTitle(Empire recipientEmpire, EmpireMessageType messageType, object subject, string description, string title)
        {
            SendMessageToEmpire(recipientEmpire, messageType, subject, description, Point.Empty, string.Empty, title);
        }

        public void SendMessageToEmpire(Empire recipientEmpire, EmpireMessageType messageType, object subject, string description, string messageHint)
        {
            SendMessageToEmpire(recipientEmpire, messageType, subject, description, Point.Empty, messageHint);
        }

        public void SendMessageToEmpire(Empire recipientEmpire, EmpireMessageType messageType, object subject, string description, Point location, string messageHint)
        {
            SendMessageToEmpire(recipientEmpire, messageType, subject, description, location, messageHint, string.Empty);
        }

        public void SendMessageToEmpire(Empire recipientEmpire, EmpireMessageType messageType, object subject, string description, Point location, string messageHint, string title)
        {
            EmpireMessage empireMessage = new EmpireMessage(this, messageType, subject);
            empireMessage.Description = description;
            empireMessage.Title = title;
            empireMessage.Location = location;
            empireMessage.Hint = messageHint;
            SendMessageToEmpire(empireMessage, recipientEmpire);
        }

        public void SendMessageToEmpire(EmpireMessage message, Empire recipientEmpire)
        {
            if (recipientEmpire != null)
            {
                if (recipientEmpire.Messages != null)
                {
                    recipientEmpire.Messages.Add(message);
                }
                if (recipientEmpire.MessageRecipient != null)
                {
                    recipientEmpire.MessageRecipient.ReceiveMessage(message);
                }
            }
        }

        public void SendNewsBroadcast(EventMessageType eventType, object subject)
        {
            SendNewsBroadcast(eventType, subject, DisasterEventType.Undefined, warStartEnd: false, wonderBegun: false);
        }

        public void SendNewsBroadcastWarStartEnd(DiplomaticRelation relation)
        {
            SendNewsBroadcast(EventMessageType.Undefined, relation, DisasterEventType.Undefined, warStartEnd: true, wonderBegun: false);
        }

        public void SendNewsBroadcastWonderBegin(PlanetaryFacilityDefinition wonder, Habitat colony)
        {
            SendNewsBroadcast(EventMessageType.Undefined, wonder, DisasterEventType.Undefined, warStartEnd: false, wonderBegun: true, colony);
        }

        public void SendNewsBroadcast(EventMessageType eventType, object subject, DisasterEventType disasterType, bool warStartEnd, bool wonderBegun)
        {
            SendNewsBroadcast(eventType, subject, disasterType, warStartEnd, wonderBegun, null);
        }

        public void SendNewsBroadcast(EventMessageType eventType, object subject, DisasterEventType disasterType, bool warStartEnd, bool wonderBegun, object extraData)
        {
            SendNewsBroadcast(eventType, subject, disasterType, warStartEnd, wonderBegun, EmpireMessageType.Undefined, extraData);
        }

        public void SendNewsBroadcast(EventMessageType eventType, object subject, DisasterEventType disasterType, bool warStartEnd, bool wonderBegun, EmpireMessageType messageType, object extraData)
        {
            object[] state = new object[7] { eventType, subject, disasterType, warStartEnd, wonderBegun, messageType, extraData };
            ThreadPool.QueueUserWorkItem(SendNewsBroadcastCallback, state);
        }

        public void SendNewsBroadcastCallback(object data)
        {
            if (data is object[])
            {
                object[] array = (object[])data;
                EventMessageType eventType = (EventMessageType)array[0];
                object subject = array[1];
                DisasterEventType disasterType = (DisasterEventType)array[2];
                bool warStartEnd = (bool)array[3];
                bool wonderBegun = (bool)array[4];
                EmpireMessageType messageType = (EmpireMessageType)array[5];
                object extraData = array[6];
                SendNewsBroadcastCore(eventType, subject, disasterType, warStartEnd, wonderBegun, messageType, extraData);
            }
        }

        public void SendNewsBroadcastCore(EventMessageType eventType, object subject, DisasterEventType disasterType, bool warStartEnd, bool wonderBegun, EmpireMessageType messageType, object extraData)
        {
            string text = string.Empty;
            switch (messageType)
            {
                case EmpireMessageType.EmpireDefeated:
                    {
                        if (!(subject is Empire))
                        {
                            break;
                        }
                        Empire empire5 = (Empire)subject;
                        if (empire5 != null)
                        {
                            Empire empire6 = null;
                            if (extraData != null && extraData is Empire)
                            {
                                empire6 = (Empire)extraData;
                            }
                            text = ((empire6 != null) ? string.Format(TextResolver.GetText("X has been defeated by Y"), empire5.Name, empire6.Name) : string.Format(TextResolver.GetText("X has been defeated"), empire5.Name));
                        }
                        break;
                    }
                case EmpireMessageType.ResearchBreakthrough:
                    {
                        if (subject == null || !(subject is ResearchNode))
                        {
                            break;
                        }
                        ResearchNode researchNode = (ResearchNode)subject;
                        if (researchNode != null)
                        {
                            Empire empire4 = null;
                            if (extraData != null && extraData is Empire)
                            {
                                empire4 = (Empire)extraData;
                            }
                            text = ((empire4 != null) ? string.Format(TextResolver.GetText("The EMPIRE has made a breakthrough in the key technology of X"), empire4.Name, researchNode.Name) : string.Format(TextResolver.GetText("An empire has made a breakthrough in the key technology of X"), researchNode.Name));
                        }
                        break;
                    }
                default:
                    if (warStartEnd)
                    {
                        if (!(subject is DiplomaticRelation))
                        {
                            break;
                        }
                        DiplomaticRelation diplomaticRelation = (DiplomaticRelation)subject;
                        if (diplomaticRelation != null)
                        {
                            Empire empire = null;
                            Empire empire2 = null;
                            empire = diplomaticRelation.Initiator;
                            empire2 = ((diplomaticRelation.ThisEmpire != empire) ? diplomaticRelation.ThisEmpire : diplomaticRelation.OtherEmpire);
                            if (empire != null && empire2 != null)
                            {
                                text = ((diplomaticRelation.Type != DiplomaticRelationType.War) ? string.Format(TextResolver.GetText("The war between X and Y has ended"), empire.Name, empire2.Name) : string.Format(TextResolver.GetText("X has declared war on Y"), empire.Name, empire2.Name));
                            }
                        }
                    }
                    else if (wonderBegun)
                    {
                        if (!(subject is PlanetaryFacilityDefinition))
                        {
                            break;
                        }
                        PlanetaryFacilityDefinition planetaryFacilityDefinition = (PlanetaryFacilityDefinition)subject;
                        if (planetaryFacilityDefinition == null || planetaryFacilityDefinition.Type != PlanetaryFacilityType.Wonder)
                        {
                            break;
                        }
                        if (extraData != null && extraData is Habitat)
                        {
                            Habitat habitat = (Habitat)extraData;
                            if (habitat != null)
                            {
                                Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                                text = string.Format(TextResolver.GetText("Wonder construction begun WONDER COLONY SYSTEM"), planetaryFacilityDefinition.Name, habitat.Name, habitat2.Name);
                            }
                            else
                            {
                                text = string.Format(TextResolver.GetText("Wonder construction begun WONDER"), planetaryFacilityDefinition.Name);
                            }
                        }
                        else
                        {
                            text = string.Format(TextResolver.GetText("Wonder construction begun WONDER"), planetaryFacilityDefinition.Name);
                        }
                    }
                    else
                    {
                        if (eventType == EventMessageType.Undefined)
                        {
                            break;
                        }
                        switch (eventType)
                        {
                            case EventMessageType.CreatureOutbreak:
                                {
                                    if (subject == null || !(subject is Creature))
                                    {
                                        break;
                                    }
                                    Creature creature = (Creature)subject;
                                    if (creature != null && creature.Type == CreatureType.SilverMist && extraData != null && extraData is Habitat)
                                    {
                                        Habitat habitat3 = (Habitat)extraData;
                                        if (habitat3 != null)
                                        {
                                            Habitat habitat4 = Galaxy.DetermineHabitatSystemStar(habitat3);
                                            string arg = Galaxy.ResolveSectorDescription(habitat3.Xpos, habitat3.Ypos);
                                            text = string.Format(TextResolver.GetText("SilverMist Released Broadcast"), habitat3.Name, habitat4.Name, arg);
                                        }
                                    }
                                    break;
                                }
                            case EventMessageType.DisasterEvent:
                                if (disasterType == DisasterEventType.EconomicCrisis)
                                {
                                    text = Galaxy.ResolveDescription(disasterType);
                                }
                                else
                                {
                                    if (subject == null || !(subject is Habitat))
                                    {
                                        break;
                                    }
                                    Habitat habitat7 = (Habitat)subject;
                                    if (habitat7 == null)
                                    {
                                        break;
                                    }
                                    if (messageType == EmpireMessageType.ColonyLost && disasterType == DisasterEventType.Plague)
                                    {
                                        string arg2 = string.Empty;
                                        string arg3 = string.Empty;
                                        if (habitat7.Empire != null)
                                        {
                                            arg2 = habitat7.Empire.Name;
                                        }
                                        if (extraData != null && extraData is Plague)
                                        {
                                            arg3 = ((Plague)extraData).Name;
                                        }
                                        text = string.Format(TextResolver.GetText("News EMPIRE COLONY has been completely wiped out by PLAGUE"), arg2, habitat7.Name, arg3);
                                    }
                                    else
                                    {
                                        text = string.Format(TextResolver.GetText("Disaster at COLONY"), Galaxy.ResolveDescription(disasterType), habitat7.Name);
                                    }
                                }
                                break;
                            case EventMessageType.WonderBuilt:
                                {
                                    if (!(subject is PlanetaryFacility))
                                    {
                                        break;
                                    }
                                    PlanetaryFacility planetaryFacility = (PlanetaryFacility)subject;
                                    if (planetaryFacility == null || planetaryFacility.Type != PlanetaryFacilityType.Wonder)
                                    {
                                        break;
                                    }
                                    if (extraData != null && extraData is Habitat)
                                    {
                                        Habitat habitat5 = (Habitat)extraData;
                                        if (habitat5 != null)
                                        {
                                            Habitat habitat6 = Galaxy.DetermineHabitatSystemStar(habitat5);
                                            text = string.Format(TextResolver.GetText("Wonder construction completed WONDER COLONY SYSTEM"), planetaryFacility.Name, habitat5.Name, habitat6.Name);
                                        }
                                        else
                                        {
                                            text = string.Format(TextResolver.GetText("Wonder construction completed WONDER"), planetaryFacility.Name);
                                        }
                                    }
                                    else
                                    {
                                        text = string.Format(TextResolver.GetText("Wonder construction completed WONDER"), planetaryFacility.Name);
                                    }
                                    break;
                                }
                            case EventMessageType.LeaderChange:
                                if (subject != null && subject is Character)
                                {
                                    Character character2 = (Character)subject;
                                    if (character2 != null && character2.Role == CharacterRole.Leader)
                                    {
                                        text = string.Format(TextResolver.GetText("Empire Leader Replaced"), Name, character2.Name);
                                    }
                                }
                                break;
                            case EventMessageType.PhantomPirates:
                                if (subject != null && subject is Empire)
                                {
                                    Empire empire3 = (Empire)subject;
                                    if (empire3 != null)
                                    {
                                        text = string.Format(TextResolver.GetText("Phantom Pirates encountered"), empire3.Name);
                                    }
                                }
                                break;
                            case EventMessageType.CharacterEvent:
                                if (subject != null && subject is Character)
                                {
                                    Character character = (Character)subject;
                                    if (character != null && (character.Role == CharacterRole.Leader || character.Role == CharacterRole.PirateLeader))
                                    {
                                        text = string.Format(TextResolver.GetText("Empire Leader killed"), Name, character.Name);
                                    }
                                }
                                break;
                        }
                    }
                    break;
            }
            if (DiplomaticRelations != null)
            {
                for (int i = 0; i < DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation2 = DiplomaticRelations[i];
                    if (diplomaticRelation2 == null || diplomaticRelation2.Type == DiplomaticRelationType.NotMet || diplomaticRelation2.OtherEmpire == null || diplomaticRelation2.OtherEmpire == this || diplomaticRelation2.OtherEmpire.PirateEmpireBaseHabitat != null)
                    {
                        continue;
                    }
                    Empire otherEmpire = diplomaticRelation2.OtherEmpire;
                    bool flag = false;
                    if (messageType == EmpireMessageType.EmpireDefeated)
                    {
                        flag = true;
                    }
                    else if (warStartEnd)
                    {
                        flag = true;
                    }
                    else if (wonderBegun)
                    {
                        flag = true;
                    }
                    else if (eventType != 0)
                    {
                        switch (eventType)
                        {
                            case EventMessageType.DisasterEvent:
                                if (disasterType == DisasterEventType.EconomicCrisis)
                                {
                                    flag = true;
                                }
                                else if (subject is Habitat)
                                {
                                    Habitat habitat8 = (Habitat)subject;
                                    if (habitat8 != null && otherEmpire.CheckSystemExplored(habitat8.SystemIndex))
                                    {
                                        flag = true;
                                    }
                                }
                                break;
                            case EventMessageType.WonderBuilt:
                                flag = true;
                                break;
                            case EventMessageType.CreatureOutbreak:
                                flag = true;
                                break;
                            case EventMessageType.PhantomPirates:
                                flag = true;
                                break;
                            case EventMessageType.LeaderChange:
                                flag = true;
                                break;
                            case EventMessageType.CharacterEvent:
                                if (!string.IsNullOrEmpty(text))
                                {
                                    flag = true;
                                }
                                break;
                        }
                    }
                    if (flag)
                    {
                        string description = TextResolver.GetText("Galactic NewsNet").ToUpper(CultureInfo.InvariantCulture) + ": " + Name + " - " + text;
                        string title = TextResolver.GetText("Galactic NewsNet").ToUpper(CultureInfo.InvariantCulture) + ": " + Name;
                        EmpireMessage empireMessage = new EmpireMessage(this, EmpireMessageType.GalacticNewsNet, subject);
                        empireMessage.Description = description;
                        empireMessage.Title = title;
                        SendMessageToEmpire(empireMessage, diplomaticRelation2.OtherEmpire);
                    }
                }
            }
            for (int j = 0; j < _Galaxy.PirateEmpires.Count; j++)
            {
                Empire empire7 = _Galaxy.PirateEmpires[j];
                if (empire7 == null || !empire7.Active || empire7.PirateEmpireBaseHabitat == null)
                {
                    continue;
                }
                PirateRelation pirateRelation = ObtainPirateRelation(empire7);
                bool flag2 = false;
                switch (messageType)
                {
                    case EmpireMessageType.EmpireDefeated:
                        if (pirateRelation.Type != 0)
                        {
                            flag2 = true;
                        }
                        break;
                    case EmpireMessageType.ResearchBreakthrough:
                        flag2 = true;
                        break;
                    default:
                        if (warStartEnd)
                        {
                            if (pirateRelation.Type != 0)
                            {
                                flag2 = true;
                            }
                        }
                        else if (wonderBegun)
                        {
                            if (pirateRelation.Type != 0)
                            {
                                flag2 = true;
                            }
                        }
                        else
                        {
                            if (eventType == EventMessageType.Undefined)
                            {
                                break;
                            }
                            switch (eventType)
                            {
                                case EventMessageType.DisasterEvent:
                                    if (disasterType == DisasterEventType.EconomicCrisis)
                                    {
                                        if (pirateRelation.Type != 0)
                                        {
                                            flag2 = true;
                                        }
                                    }
                                    else if (subject is Habitat)
                                    {
                                        Habitat habitat9 = (Habitat)subject;
                                        if (habitat9 != null && empire7.CheckSystemExplored(habitat9.SystemIndex))
                                        {
                                            flag2 = true;
                                        }
                                    }
                                    break;
                                case EventMessageType.WonderBuilt:
                                    if (pirateRelation.Type != 0)
                                    {
                                        flag2 = true;
                                    }
                                    break;
                                case EventMessageType.CreatureOutbreak:
                                    if (pirateRelation.Type != 0)
                                    {
                                        flag2 = true;
                                    }
                                    break;
                                case EventMessageType.PhantomPirates:
                                    flag2 = true;
                                    break;
                                case EventMessageType.LeaderChange:
                                    if (pirateRelation.Type != 0)
                                    {
                                        flag2 = true;
                                    }
                                    break;
                                case EventMessageType.CharacterEvent:
                                    if (!string.IsNullOrEmpty(text) && pirateRelation.Type != 0)
                                    {
                                        flag2 = true;
                                    }
                                    break;
                            }
                        }
                        break;
                }
                if (flag2)
                {
                    string description2 = TextResolver.GetText("Galactic NewsNet").ToUpper(CultureInfo.InvariantCulture) + ": " + Name + " - " + text;
                    string title2 = TextResolver.GetText("Galactic NewsNet").ToUpper(CultureInfo.InvariantCulture) + ": " + Name;
                    EmpireMessage empireMessage2 = new EmpireMessage(this, EmpireMessageType.GalacticNewsNet, subject);
                    empireMessage2.Description = description2;
                    empireMessage2.Title = title2;
                    SendMessageToEmpire(empireMessage2, empire7);
                }
            }
        }

        public void SendEventMessageToEmpire(EventMessageType eventMessageType, string title, string message, object additionalData, object location)
        {
            if (EventMessageRecipient != null)
            {
                EventMessageRecipient.ReceiveEventMessage(eventMessageType, title, message, additionalData, location);
            }
        }

        private void CheckReviewSpecialPirateEvents()
        {
            if (PirateExtortionOfferMade && !PreWarpProgressEventOccurredExperienceFirstPirateRaid && Capital != null)
            {
                CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType.FirstPirateRaid, Capital, null, "avoid");
            }
        }

        public bool CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType eventType, object subject)
        {
            return CheckSendPreWarpProgressEventMessage(eventType, subject, null);
        }

        public bool CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType eventType, object subject, Empire empire)
        {
            return CheckSendPreWarpProgressEventMessage(eventType, subject, empire, string.Empty);
        }

        public bool CheckSendPreWarpProgressEventMessage(PreWarpProgressEventType eventType, object subject, Empire empire, string hint)
        {
            if (this != _Galaxy.IndependentEmpire)
            {
                StellarObject stellarObject = null;
                BuiltObject builtObject = null;
                Habitat habitat = null;
                Creature creature = null;
                Component component = null;
                if (subject is StellarObject)
                {
                    stellarObject = (StellarObject)subject;
                    if (stellarObject is BuiltObject)
                    {
                        builtObject = (BuiltObject)stellarObject;
                    }
                    else if (stellarObject is Habitat)
                    {
                        habitat = (Habitat)stellarObject;
                    }
                    else if (stellarObject is Creature)
                    {
                        creature = (Creature)stellarObject;
                    }
                }
                else if (subject is BuiltObject)
                {
                    builtObject = (BuiltObject)subject;
                    stellarObject = builtObject;
                }
                else if (subject is Habitat)
                {
                    habitat = (Habitat)subject;
                    stellarObject = habitat;
                }
                else if (subject is Empire)
                {
                    empire = (Empire)subject;
                }
                else if (subject is Creature)
                {
                    creature = (Creature)subject;
                    stellarObject = creature;
                }
                else if (subject is ResearchNode)
                {
                    ResearchNode researchNode = (ResearchNode)subject;
                    if (researchNode.Components != null && researchNode.Components.Count > 0)
                    {
                        component = researchNode.Components[0];
                    }
                    else if (researchNode.ComponentImprovements != null && researchNode.ComponentImprovements.Count > 0)
                    {
                        component = researchNode.ComponentImprovements[0].ImprovedComponent;
                    }
                }
                switch (eventType)
                {
                    case PreWarpProgressEventType.FirstPirateRaid:
                        if (PreWarpProgressEventOccurredExperienceFirstPirateRaid)
                        {
                            break;
                        }
                        if (habitat != null)
                        {
                            string arg = string.Empty;
                            if (empire != null)
                            {
                                arg = empire.Name;
                            }
                            string text16 = TextResolver.GetText("PreWarpProgressEvent Title ExperienceFirstPirateRaid");
                            string message11 = string.Format(TextResolver.GetText("PreWarpProgressEvent Message ExperienceFirstPirateRaid"), habitat.Name, arg);
                            if (hint == "avoid")
                            {
                                message11 = string.Format(TextResolver.GetText("PreWarpProgressEvent Message AvoidFirstPirateRaid"), habitat.Name);
                            }
                            SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text16, message11, subject, subject);
                        }
                        PreWarpProgressEventOccurredExperienceFirstPirateRaid = true;
                        return true;
                    case PreWarpProgressEventType.BuildFirstMiningStation:
                        if (!PreWarpProgressEventOccurredBuildFirstMiningStation)
                        {
                            if (builtObject != null && builtObject.ParentHabitat != null)
                            {
                                _EconomyEfficiency += 0.25;
                                string text15 = TextResolver.GetText("PreWarpProgressEvent Title BuildFirstMiningStation");
                                string message10 = string.Format(TextResolver.GetText("PreWarpProgressEvent Message BuildFirstMiningStation"), builtObject.Name, builtObject.ParentHabitat.Name);
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text15, message10, subject, subject);
                            }
                            PreWarpProgressEventOccurredBuildFirstMiningStation = true;
                            return true;
                        }
                        break;
                    case PreWarpProgressEventType.BuildFirstResearchStation:
                        if (PreWarpProgressEventOccurredBuildFirstResearchStation)
                        {
                            break;
                        }
                        if (builtObject != null)
                        {
                            if (Galaxy.Rnd.Next(0, 2) > 0)
                            {
                                Character character = GenerateNewCharacter(CharacterRole.Scientist, builtObject);
                                string text5 = TextResolver.GetText("PreWarpProgressEvent Title BuildFirstResearchStation");
                                string message4 = string.Format(TextResolver.GetText("PreWarpProgressEvent Message BuildFirstResearchStation New Scientist"), character.Name);
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text5, message4, character, character);
                            }
                            else
                            {
                                _EconomyEfficiency += 0.15;
                                string text6 = TextResolver.GetText("PreWarpProgressEvent Title BuildFirstResearchStation");
                                string message5 = string.Format(TextResolver.GetText("PreWarpProgressEvent Message BuildFirstResearchStation"), builtObject.Name);
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text6, message5, subject, subject);
                            }
                        }
                        PreWarpProgressEventOccurredBuildFirstResearchStation = true;
                        return true;
                    case PreWarpProgressEventType.BuildFirstMilitaryShip:
                        if (PreWarpProgressEventOccurredBuildFirstMilitaryShip)
                        {
                            break;
                        }
                        if (builtObject != null)
                        {
                            string text18 = TextResolver.GetText("PreWarpProgressEvent Title BuildFirstMilitaryShip");
                            string message13 = string.Format(TextResolver.GetText("PreWarpProgressEvent Message BuildFirstMilitaryShip"), builtObject.Name);
                            SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text18, message13, subject, subject);
                            if (_Galaxy.StoryShadowsEnabled)
                            {
                                PirateRelationList relationsByType2 = PirateRelations.GetRelationsByType(PirateRelationType.None);
                                bool flag2 = false;
                                if (Colonies != null && Colonies.Count > 0)
                                {
                                    Habitat habitat7 = Colonies[0];
                                    if (habitat7 != null && !habitat7.HasBeenDestroyed && relationsByType2.Count > 0)
                                    {
                                        for (int k = 0; k < relationsByType2.Count; k++)
                                        {
                                            if (relationsByType2[k] != null && relationsByType2[k].OtherEmpire != null && relationsByType2[k].OtherEmpire != _Galaxy.PlayerEmpire && relationsByType2[k].OtherEmpire.Active)
                                            {
                                                ShipGroup shipGroup = relationsByType2[k].OtherEmpire.IdentifyNearestAvailableFleet(habitat7.Xpos, habitat7.Ypos, mustBeAutomated: true, mustBeWithinFuelRange: true, 0.1);
                                                if (shipGroup != null)
                                                {
                                                    shipGroup.AssignMission(BuiltObjectMissionType.Raid, habitat7, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                                    flag2 = true;
                                                }
                                                if (flag2)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (!flag2 && MiningStations != null && MiningStations.Count > 0)
                                {
                                    BuiltObject builtObject2 = MiningStations[0];
                                    if (!builtObject2.HasBeenDestroyed)
                                    {
                                        for (int l = 0; l < relationsByType2.Count; l++)
                                        {
                                            if (relationsByType2[l] == null || relationsByType2[l].OtherEmpire == null || relationsByType2[l].OtherEmpire == _Galaxy.PlayerEmpire || !relationsByType2[l].OtherEmpire.Active)
                                            {
                                                continue;
                                            }
                                            BuiltObject firstAvailableWithinRange = relationsByType2[l].OtherEmpire.BuiltObjects.GetFirstAvailableWithinRange(BuiltObjectRole.Military, builtObject2.Xpos, builtObject2.Ypos, 0.1, includeLowAndNormalPriorityMissions: true);
                                            if (firstAvailableWithinRange != null)
                                            {
                                                firstAvailableWithinRange.ClearPreviousMissionRequirements();
                                                if (firstAvailableWithinRange.AssaultStrength > 0)
                                                {
                                                    firstAvailableWithinRange.AssignMission(BuiltObjectMissionType.Raid, builtObject2, null, BuiltObjectMissionPriority.High);
                                                }
                                                else
                                                {
                                                    firstAvailableWithinRange.AssignMission(BuiltObjectMissionType.Attack, builtObject2, null, BuiltObjectMissionPriority.High);
                                                }
                                                flag2 = true;
                                            }
                                            if (flag2)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        PreWarpProgressEventOccurredBuildFirstMilitaryShip = true;
                        return true;
                    case PreWarpProgressEventType.BuildFirstShip:
                        if (!PreWarpProgressEventOccurredBuildFirstShip)
                        {
                            if (builtObject != null)
                            {
                                string text17 = TextResolver.GetText("PreWarpProgressEvent Title BuildFirstShip");
                                string message12 = string.Format(TextResolver.GetText("PreWarpProgressEvent Message BuildFirstShip"), builtObject.Name);
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text17, message12, subject, subject);
                            }
                            PreWarpProgressEventOccurredBuildFirstShip = true;
                            return true;
                        }
                        break;
                    case PreWarpProgressEventType.BuildFirstSpaceport:
                        if (!PreWarpProgressEventOccurredBuildFirstSpaceport)
                        {
                            if (builtObject != null)
                            {
                                _EconomyEfficiency += 0.25;
                                string text12 = TextResolver.GetText("PreWarpProgressEvent Title BuildFirstSpaceport");
                                string text13 = TextResolver.GetText("PreWarpProgressEvent Message BuildFirstSpaceport");
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text12, text13, subject, subject);
                            }
                            PreWarpProgressEventOccurredBuildFirstSpaceport = true;
                            return true;
                        }
                        break;
                    case PreWarpProgressEventType.DiscoverColonizationTech:
                        if (!PreWarpProgressEventOccurredDiscoverColonizationTech)
                        {
                            if (component != null)
                            {
                                _EconomyEfficiency += 0.5;
                                string text4 = TextResolver.GetText("PreWarpProgressEvent Title DiscoverColonizationTech");
                                string message3 = string.Format(TextResolver.GetText("PreWarpProgressEvent Message DiscoverColonizationTech"), component.Name);
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text4, message3, component, component);
                            }
                            PreWarpProgressEventOccurredDiscoverColonizationTech = true;
                            return true;
                        }
                        break;
                    case PreWarpProgressEventType.DiscoverHyperspaceTech:
                        if (!PreWarpProgressEventOccurredDiscoverHyperspaceTech)
                        {
                            if (Galaxy.Rnd.Next(0, 3) > 0)
                            {
                                Character character2 = GenerateNewCharacter(CharacterRole.Scientist, Capital);
                                string text9 = TextResolver.GetText("PreWarpProgressEvent Title DiscoverHyperspaceTech");
                                string message8 = string.Format(TextResolver.GetText("PreWarpProgressEvent Message DiscoverHyperspaceTech New Scientist"), character2.Name);
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text9, message8, character2, character2);
                            }
                            else
                            {
                                _EconomyEfficiency += 0.15;
                                string text10 = TextResolver.GetText("PreWarpProgressEvent Title DiscoverHyperspaceTech");
                                string text11 = TextResolver.GetText("PreWarpProgressEvent Message DiscoverHyperspaceTech");
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text10, text11, component, component);
                            }
                            PreWarpProgressEventOccurredDiscoverHyperspaceTech = true;
                            return true;
                        }
                        break;
                    case PreWarpProgressEventType.EncounterFirstKaltor:
                        if (PreWarpProgressEventOccurredEncounterFirstKaltor)
                        {
                            break;
                        }
                        if (creature != null)
                        {
                            Habitat habitat6 = _Galaxy.FindNearestHabitat(creature.Xpos, creature.Ypos);
                            if (habitat6 != null)
                            {
                                string text14 = TextResolver.GetText("PreWarpProgressEvent Title EncounterFirstKaltor");
                                string message9 = string.Format(TextResolver.GetText("PreWarpProgressEvent Message EncounterFirstKaltor"), habitat6.Name);
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text14, message9, subject, subject);
                            }
                        }
                        PreWarpProgressEventOccurredEncounterFirstKaltor = true;
                        return true;
                    case PreWarpProgressEventType.FirstContactNormalEmpire:
                        if (PreWarpProgressEventOccurredFirstContactNormalEmpire)
                        {
                            break;
                        }
                        if (empire != null)
                        {
                            bool flag = false;
                            if (_Galaxy.StoryShadowsEnabled && empire.PirateEmpireBaseHabitat == null)
                            {
                                DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                                if (diplomaticRelation.Type != DiplomaticRelationType.War && empire != _Galaxy.PlayerEmpire && Galaxy.ResolveStandardRaceBias(empire.DominantRace, DominantRace) < 0.0)
                                {
                                    int num4 = empire.BuiltObjects.TotalMobileMilitaryFirepower();
                                    int num5 = BuiltObjects.TotalMobileMilitaryFirepower();
                                    double num6 = (double)num5 / (double)num4;
                                    if (num6 < 2.0 && empire.CheckEmpireHasHyperDriveTech(empire))
                                    {
                                        empire.DeclareWar(this);
                                        flag = true;
                                    }
                                }
                            }
                            if (flag)
                            {
                                string text7 = TextResolver.GetText("PreWarpProgressEvent Title FirstContactNormalEmpire");
                                string message6 = string.Format(TextResolver.GetText("PreWarpProgressEvent Message FirstContactNormalEmpire War"), empire.Name, empire.DominantRace.Name);
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text7, message6, subject, subject);
                            }
                            else
                            {
                                string text8 = TextResolver.GetText("PreWarpProgressEvent Title FirstContactNormalEmpire");
                                string message7 = string.Format(TextResolver.GetText("PreWarpProgressEvent Message FirstContactNormalEmpire"), empire.Name, empire.DominantRace.Name);
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text8, message7, subject, subject);
                            }
                        }
                        PreWarpProgressEventOccurredFirstContactNormalEmpire = true;
                        return true;
                    case PreWarpProgressEventType.FirstContactPirateOrIndependent:
                        if (!PreWarpProgressEventOccurredFirstContactPirateOrIndependent && empire != null && stellarObject != null && empire == _Galaxy.IndependentEmpire)
                        {
                            string text19 = TextResolver.GetText("PreWarpProgressEvent Title FirstContactIndependent");
                            if (stellarObject.Empire == empire)
                            {
                                string message14 = string.Format(TextResolver.GetText("PreWarpProgressEvent Message FirstContactIndependent"), stellarObject.Name);
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text19, message14, stellarObject, stellarObject);
                            }
                            else
                            {
                                string text20 = TextResolver.GetText("PreWarpProgressEvent Message FirstContactIndependent NoShip");
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text19, text20, null, stellarObject);
                            }
                            PreWarpProgressEventOccurredFirstContactPirateOrIndependent = true;
                            return true;
                        }
                        break;
                    case PreWarpProgressEventType.FirstHyperjump:
                        if (PreWarpProgressEventOccurredFirstHyperjump)
                        {
                            break;
                        }
                        if (builtObject != null)
                        {
                            Habitat habitat2 = null;
                            if (_Galaxy.StoryShadowsEnabled)
                            {
                                Habitat habitat3 = _Galaxy.FastFindNearestSystem(builtObject.Xpos, builtObject.Ypos);
                                if (habitat3 != null)
                                {
                                    habitat2 = _Galaxy.FindNearestHabitat(habitat3.Xpos, habitat3.Ypos, HabitatType.FrozenGasGiant);
                                    if (habitat2 == null || habitat2.SystemIndex != habitat3.SystemIndex)
                                    {
                                        double num = (double)Galaxy.MaxSolarSystemSize + Galaxy.Rnd.NextDouble() * (double)Galaxy.MaxSolarSystemSize;
                                        double num2 = (double)Galaxy.MaxSolarSystemSize + Galaxy.Rnd.NextDouble() * (double)Galaxy.MaxSolarSystemSize;
                                        if (Galaxy.Rnd.Next(0, 2) == 1)
                                        {
                                            num *= -1.0;
                                        }
                                        if (Galaxy.Rnd.Next(0, 2) == 1)
                                        {
                                            num2 *= -1.0;
                                        }
                                        habitat2 = _Galaxy.FindNearestHabitat(habitat3.Xpos + num, habitat3.Ypos + num2, HabitatType.BarrenRock);
                                    }
                                }
                                PirateRelationList relationsByType = PirateRelations.GetRelationsByType(PirateRelationType.None);
                                if (Colonies != null && Colonies.Count > 0)
                                {
                                    Habitat habitat4 = Colonies[0];
                                    if (habitat4 != null && !habitat4.HasBeenDestroyed)
                                    {
                                        if (relationsByType.Count > 0)
                                        {
                                            for (int i = 0; i < relationsByType.Count; i++)
                                            {
                                                if (relationsByType[i] != null && relationsByType[i].OtherEmpire != null && relationsByType[i].OtherEmpire != _Galaxy.PlayerEmpire && relationsByType[i].OtherEmpire.Active)
                                                {
                                                    relationsByType[i].OtherEmpire.IdentifyNearestAvailableFleet(habitat4.Xpos, habitat4.Ypos, mustBeAutomated: true, mustBeWithinFuelRange: true, 0.1)?.AssignMission(BuiltObjectMissionType.Raid, habitat4, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                                                }
                                            }
                                        }
                                        if (_Galaxy.PlayerEmpire != this && _Galaxy.PlayerEmpire.PirateEmpireBaseHabitat != null)
                                        {
                                            PirateRelation pirateRelation = _Galaxy.PlayerEmpire.ObtainPirateRelation(this);
                                            if (pirateRelation != null && pirateRelation.Type != 0)
                                            {
                                                Habitat habitat5 = Galaxy.DetermineHabitatSystemStar(habitat4);
                                                string text = TextResolver.GetText("Empire achieves Hyperspace Travel");
                                                string description = string.Format(TextResolver.GetText("Empire Achieved Warp Notification"), habitat5.Name, Name);
                                                _Galaxy.PlayerEmpire.SendMessageToEmpireWithTitle(_Galaxy.PlayerEmpire, EmpireMessageType.GeneralNeutralEvent, habitat4, description, text);
                                            }
                                        }
                                    }
                                }
                            }
                            if (habitat2 != null && _Galaxy.CreaturePrevalence > 0.0 && _Galaxy.AllowGiantKaltorGeneration)
                            {
                                int num3 = Galaxy.Rnd.Next(2, 4);
                                for (int j = 0; j < num3; j++)
                                {
                                    _Galaxy.GenerateCreatureAtHabitat(CreatureType.Kaltor, habitat2, lockLocation: false);
                                }
                                string text2 = TextResolver.GetText("PreWarpProgressEvent Title FirstHyperjump");
                                string message = string.Format(TextResolver.GetText("PreWarpProgressEvent Message FirstHyperjump Creature Outbreak"), builtObject.Name, habitat2.Name);
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text2, message, subject, habitat2);
                            }
                            else
                            {
                                string text3 = TextResolver.GetText("PreWarpProgressEvent Title FirstHyperjump");
                                string message2 = string.Format(TextResolver.GetText("PreWarpProgressEvent Message FirstHyperjump"), builtObject.Name);
                                SendEventMessageToEmpire(EventMessageType.GeneralDiscovery, text3, message2, subject, subject);
                            }
                        }
                        PreWarpProgressEventOccurredFirstHyperjump = true;
                        return true;
                }
            }
            return false;
        }

        private void PromptPlayerForAuthorization(string message, object target, EmpireMessage empireMessage)
        {
            if (_AutomationAuthorizer != null)
            {
                _AutomationAuthorizer.PromptForAuthorization(message, target, empireMessage);
            }
        }

        public string GenerateMessageDescriptionEndWarRequest()
        {
            return TextResolver.GetText("We urge you to consider our proposal for an end to this pointless war");
        }

        public string GenerateMessageDescriptionEndSubjugation()
        {
            return TextResolver.GetText("We are releasing you from subjugation to us. We no longer consider you to be our conquered dominion.");
        }

        public string GenerateMessageDescriptionLiftTradeSanctions()
        {
            return TextResolver.GetText("Our trade sanctions against you have been lifted - we will now resume trade.");
        }

        public string GenerateMessageDescription(DiplomaticRelation currentRelation, DiplomaticRelationType diplomaticRelationType, int ourPotencyVersusThem)
        {
            string result = string.Empty;
            if (currentRelation != null)
            {
                if (ourPotencyVersusThem < 0)
                {
                    switch (diplomaticRelationType)
                    {
                        case DiplomaticRelationType.FreeTradeAgreement:
                            result = TextResolver.GetText("May you look kindly on our proposal of free trade with your wealthy empire");
                            break;
                        case DiplomaticRelationType.MutualDefensePact:
                            result = TextResolver.GetText("We would prove a most loyal ally to your mighty empire in a mutual defense pact");
                            break;
                        case DiplomaticRelationType.None:
                            switch (currentRelation.Type)
                            {
                                case DiplomaticRelationType.FreeTradeAgreement:
                                case DiplomaticRelationType.MutualDefensePact:
                                case DiplomaticRelationType.Protectorate:
                                    result = TextResolver.GetText("We are cancelling our treaty with you forthwith");
                                    break;
                                case DiplomaticRelationType.War:
                                    result = TextResolver.GetText("We urge you to consider our proposal for an end to this pointless war");
                                    break;
                                case DiplomaticRelationType.TradeSanctions:
                                    result = TextResolver.GetText("We ask you to end your trade sanctions against us");
                                    break;
                                case DiplomaticRelationType.SubjugatedDominion:
                                    result = TextResolver.GetText("We beg for release from Subjugation");
                                    break;
                                default:
                                    result = TextResolver.GetText("We urge you to consider our proposal for an end to this pointless war");
                                    break;
                            }
                            break;
                        case DiplomaticRelationType.Protectorate:
                            result = TextResolver.GetText("We offer you refuge in a Protectorate treaty with our strong empire");
                            break;
                        case DiplomaticRelationType.SubjugatedDominion:
                            result = TextResolver.GetText("We propose an end to this war, but you must become our subjugated dominion");
                            break;
                        case DiplomaticRelationType.TradeSanctions:
                            result = TextResolver.GetText("We regret to inform you that all trade between our two empires has been suspended");
                            break;
                        case DiplomaticRelationType.Truce:
                            result = TextResolver.GetText("We beg you to consider a truce");
                            break;
                        case DiplomaticRelationType.War:
                            result = TextResolver.GetText("We are afraid that we have no choice but to declare war on you");
                            break;
                    }
                }
                else if (ourPotencyVersusThem == 0)
                {
                    switch (diplomaticRelationType)
                    {
                        case DiplomaticRelationType.FreeTradeAgreement:
                            result = TextResolver.GetText("We propose a free trade agreement between our two dynamic societies");
                            break;
                        case DiplomaticRelationType.MutualDefensePact:
                            result = TextResolver.GetText("It would be in both our best interests to form a mutual defense pact");
                            break;
                        case DiplomaticRelationType.None:
                            switch (currentRelation.Type)
                            {
                                case DiplomaticRelationType.FreeTradeAgreement:
                                case DiplomaticRelationType.MutualDefensePact:
                                case DiplomaticRelationType.Protectorate:
                                    result = TextResolver.GetText("We are cancelling our treaty with you forthwith");
                                    break;
                                case DiplomaticRelationType.War:
                                    result = TextResolver.GetText("We urge you to consider our proposal for an end to this pointless war");
                                    break;
                                case DiplomaticRelationType.TradeSanctions:
                                    result = TextResolver.GetText("We ask you to end your trade sanctions against us");
                                    break;
                                case DiplomaticRelationType.SubjugatedDominion:
                                    result = TextResolver.GetText("We beg for release from Subjugation");
                                    break;
                                default:
                                    result = TextResolver.GetText("We urge you to consider our proposal for an end to this pointless war");
                                    break;
                            }
                            break;
                        case DiplomaticRelationType.Protectorate:
                            result = TextResolver.GetText("We offer you refuge in a Protectorate treaty with our strong empire");
                            break;
                        case DiplomaticRelationType.SubjugatedDominion:
                            result = TextResolver.GetText("We propose an end to this war, but you must become our subjugated dominion");
                            break;
                        case DiplomaticRelationType.TradeSanctions:
                            result = TextResolver.GetText("All trade between us has been terminated until further notice");
                            break;
                        case DiplomaticRelationType.Truce:
                            result = TextResolver.GetText("We feel a truce is in order at this point");
                            break;
                        case DiplomaticRelationType.War:
                            result = TextResolver.GetText("We inform you that a state of war now exists between us");
                            break;
                    }
                }
                else
                {
                    switch (diplomaticRelationType)
                    {
                        case DiplomaticRelationType.FreeTradeAgreement:
                            result = TextResolver.GetText("Accept our free trade agreement proposal and prosper with us");
                            break;
                        case DiplomaticRelationType.MutualDefensePact:
                            result = TextResolver.GetText("You would be wise to ally yourselves with our strong empire in a mutual defense pact");
                            break;
                        case DiplomaticRelationType.None:
                            switch (currentRelation.Type)
                            {
                                case DiplomaticRelationType.FreeTradeAgreement:
                                case DiplomaticRelationType.MutualDefensePact:
                                case DiplomaticRelationType.Protectorate:
                                    result = TextResolver.GetText("We are pleased to rid ourselves of this troublesome treaty with you!");
                                    break;
                                case DiplomaticRelationType.War:
                                    result = TextResolver.GetText("We urge you to consider our proposal for an end to this pointless war");
                                    break;
                                case DiplomaticRelationType.TradeSanctions:
                                    result = TextResolver.GetText("We ask you to end your trade sanctions against us");
                                    break;
                                case DiplomaticRelationType.SubjugatedDominion:
                                    result = TextResolver.GetText("We beg for release from Subjugation");
                                    break;
                                default:
                                    result = TextResolver.GetText("We urge you to consider our proposal for an end to this pointless war");
                                    break;
                            }
                            break;
                        case DiplomaticRelationType.Protectorate:
                            result = TextResolver.GetText("We offer you refuge in a Protectorate treaty with our strong empire");
                            break;
                        case DiplomaticRelationType.SubjugatedDominion:
                            result = TextResolver.GetText("We propose an end to this war, but you must become our subjugated dominion");
                            break;
                        case DiplomaticRelationType.TradeSanctions:
                            result = TextResolver.GetText("Effective immediately, all our trade with your tyrannical empire is terminated");
                            break;
                        case DiplomaticRelationType.Truce:
                            result = TextResolver.GetText("We offer your pathetic empire a respite from your miserable defeats in a truce");
                            break;
                        case DiplomaticRelationType.War:
                            result = TextResolver.GetText("We declare war on your worthless empire. Your colonies await liberation from your tyranny");
                            break;
                    }
                }
            }
            return result;
        }

        public string GenerateMessageDescription(EmpireMessageType messageType, int ourPotencyVersusThem, Empire targetEmpire)
        {
            string result = string.Empty;
            if (ourPotencyVersusThem < 0)
            {
                switch (messageType)
                {
                    case EmpireMessageType.GiveGift:
                        result = TextResolver.GetText("We are greatly honored to present this humble gift to your noble empire");
                        break;
                    case EmpireMessageType.LeaveSystem:
                        result = TextResolver.GetText("We respectfully request that you leave the X system at your earliest convenience");
                        break;
                    case EmpireMessageType.RemoveColoniesFromSystem:
                        result = TextResolver.GetText("We would be most appreciative if you were able to decolonize the X system");
                        break;
                    case EmpireMessageType.RequestJointWar:
                        result = string.Format(TextResolver.GetText("We beg you to consider our request to declare war on the EMPIRE"), targetEmpire.Name);
                        break;
                    case EmpireMessageType.RequestLiftTradeSanctions:
                        result = string.Format(TextResolver.GetText("We would be most gratified if you were able to resume trade with the EMPIRE"), targetEmpire.Name);
                        break;
                    case EmpireMessageType.RequestJointTradeSanctions:
                        result = string.Format(TextResolver.GetText("We respectfully appeal to your illustrious empire to cease trade with the {0}"), targetEmpire.Name);
                        break;
                    case EmpireMessageType.RequestStopWar:
                        result = string.Format(TextResolver.GetText("Please hear our respectful plea to stop your war with the EMPIRE"), targetEmpire.Name);
                        break;
                    case EmpireMessageType.StopAttacks:
                        result = TextResolver.GetText("We are greatly troubled by your unfriendly attacks. Please try to avoid these unfortunate incidents");
                        break;
                    case EmpireMessageType.StopMissionsAgainstUs:
                        result = TextResolver.GetText("We have heard baseless rumours that your virtuous empire has been involved in covert actions against us");
                        break;
                }
            }
            else if (ourPotencyVersusThem == 0)
            {
                switch (messageType)
                {
                    case EmpireMessageType.GiveGift:
                        result = TextResolver.GetText("Please accept this gift as a token of our goodwill");
                        break;
                    case EmpireMessageType.LeaveSystem:
                        result = TextResolver.GetText("Please leave the X system");
                        break;
                    case EmpireMessageType.RemoveColoniesFromSystem:
                        result = TextResolver.GetText("Please remove your colonies from the X system");
                        break;
                    case EmpireMessageType.RequestJointWar:
                        result = string.Format(TextResolver.GetText("We formally request that you declare war on the EMPIRE"), targetEmpire.Name);
                        break;
                    case EmpireMessageType.RequestLiftTradeSanctions:
                        result = string.Format(TextResolver.GetText("Please resume trade with the EMPIRE"), targetEmpire.Name);
                        break;
                    case EmpireMessageType.RequestJointTradeSanctions:
                        result = string.Format(TextResolver.GetText("We request that you terminate trade with the EMPIRE"), targetEmpire.Name);
                        break;
                    case EmpireMessageType.RequestStopWar:
                        result = string.Format(TextResolver.GetText("Please end your war with the EMPIRE"), targetEmpire.Name);
                        break;
                    case EmpireMessageType.StopAttacks:
                        result = TextResolver.GetText("We serve official warning on you to cease all attacks against us");
                        break;
                    case EmpireMessageType.StopMissionsAgainstUs:
                        result = TextResolver.GetText("Your covert actions against us have been detected. We warn you to stop these activities");
                        break;
                }
            }
            else
            {
                switch (messageType)
                {
                    case EmpireMessageType.GiveGift:
                        result = TextResolver.GetText("We are pleased to present this gift to you");
                        break;
                    case EmpireMessageType.LeaveSystem:
                        result = TextResolver.GetText("We demand that you immediately leave the X system");
                        break;
                    case EmpireMessageType.RemoveColoniesFromSystem:
                        result = TextResolver.GetText("We insist that you remove all colonies from the X system at once");
                        break;
                    case EmpireMessageType.RequestJointWar:
                        result = string.Format(TextResolver.GetText("Without delay, we require that you declare war on the EMPIRE"), targetEmpire.Name);
                        break;
                    case EmpireMessageType.RequestLiftTradeSanctions:
                        result = string.Format(TextResolver.GetText("We demand that you restore trade with the EMPIRE at once!"), targetEmpire.Name);
                        break;
                    case EmpireMessageType.RequestJointTradeSanctions:
                        result = string.Format(TextResolver.GetText("We order you to instantly cease all trade with the EMPIRE"), targetEmpire.Name);
                        break;
                    case EmpireMessageType.RequestStopWar:
                        result = string.Format(TextResolver.GetText("We command you to stop your war with the EMPIRE straight away!"), targetEmpire.Name);
                        break;
                    case EmpireMessageType.StopAttacks:
                        result = TextResolver.GetText("You are ordered to immediately cease all attacks against us!");
                        break;
                    case EmpireMessageType.StopMissionsAgainstUs:
                        result = TextResolver.GetText("We warn you to end your treacherous covert actvities against us immediately!");
                        break;
                }
            }
            return result;
        }

        public double CalculateRelativeEmpireMilitaryStrength()
        {
            double num = 0.0;
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire != this)
                {
                    num += (double)Math.Max(1, empire.MilitaryPotency);
                }
            }
            double num2 = num / (double)(_Galaxy.Empires.Count - 1);
            return (double)Math.Max(1, MilitaryPotency) / num2;
        }

        public double CalculateRelativeEmpireSize()
        {
            if (PirateEmpireBaseHabitat == null)
            {
                double num = 0.0;
                for (int i = 0; i < _Galaxy.Empires.Count; i++)
                {
                    Empire empire = _Galaxy.Empires[i];
                    if (empire != null && empire.Active && empire != this)
                    {
                        num += (double)empire.TotalColonyStrategicValue;
                    }
                }
                double num2 = num / (double)(_Galaxy.Empires.Count - 1);
                return (double)TotalColonyStrategicValue / num2;
            }
            double num3 = 0.0;
            for (int j = 0; j < _Galaxy.PirateEmpires.Count; j++)
            {
                Empire empire2 = _Galaxy.PirateEmpires[j];
                if (empire2 != null && empire2.Active && empire2 != this && empire2.BuiltObjects != null)
                {
                    num3 += (double)empire2.BuiltObjects.TotalMobileMilitaryFirepower();
                }
            }
            double num4 = num3 / (double)(_Galaxy.PirateEmpires.Count - 1);
            return (double)BuiltObjects.TotalMobileMilitaryFirepower() / num4;
        }

        public EmpireRelationshipFactorList DetermineEmpireRelationshipFactors(Empire otherEmpire)
        {
            EmpireRelationshipFactorList empireRelationshipFactorList = new EmpireRelationshipFactorList();
            if (otherEmpire.PirateEmpireBaseHabitat == null && PirateEmpireBaseHabitat == null)
            {
                EmpireEvaluation empireEvaluation = otherEmpire.EmpireEvaluations[this];
                if (empireEvaluation != null)
                {
                    if (empireEvaluation.FirstContactPenalty < 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.FirstContactPenalty, TextResolver.GetText("First Contact Penalty Description")));
                    }
                    if (empireEvaluation.MilitaryForcesInSystems < 0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.MilitaryForcesInSystems, TextResolver.GetText("Your military forces in our systems violate our territory")));
                    }
                    if (empireEvaluation.RelationshipWithFriendsPositiveCumulative > 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.RelationshipWithFriendsPositiveCumulative, TextResolver.GetText("You have formed beneficial treaties with our friends")));
                    }
                    if (empireEvaluation.RelationshipWithFriendsNegativeCumulative < 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.RelationshipWithFriendsNegativeCumulative, TextResolver.GetText("You have trade sanctions or are at war with our friends")));
                    }
                    if (empireEvaluation.SystemCompetitionCumulative < 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.SystemCompetitionCumulative, TextResolver.GetText("Your colonies and bases trespass in our systems!")));
                    }
                    if (empireEvaluation.ReputationWeighted > 0.0)
                    {
                        _ = string.Empty;
                        string arg = empireEvaluation.Empire.CivilityDescription();
                        arg = string.Format(TextResolver.GetText("We respect your good reputation"), arg);
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.ReputationWeighted, arg));
                    }
                    else if (empireEvaluation.ReputationWeighted < 0.0)
                    {
                        _ = string.Empty;
                        string arg2 = empireEvaluation.Empire.CivilityDescription();
                        arg2 = string.Format(TextResolver.GetText("We are troubled by your poor reputation"), arg2);
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.ReputationWeighted, arg2));
                    }
                    if (empireEvaluation.TradeVolume > 0)
                    {
                        string empty = string.Empty;
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(description: (empireEvaluation.TradeVolume > 20) ? TextResolver.GetText("Our empires generate a colossal amount of trade") : ((empireEvaluation.TradeVolume > 13) ? TextResolver.GetText("Our empires produce a large amount of trade") : ((empireEvaluation.TradeVolume <= 6) ? TextResolver.GetText("Our empires share a small volume of trade") : TextResolver.GetText("Our empires share a fair amount of trade"))), value: empireEvaluation.TradeVolume));
                    }
                    if (empireEvaluation.GovernmentStyleAffinityCumulative < 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.GovernmentStyleAffinityCumulative, string.Format(TextResolver.GetText("We are unhappy with your style of government"), GovernmentAttributes.Name)));
                    }
                    if (empireEvaluation.GovernmentStyleAffinityCumulative > 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.GovernmentStyleAffinityCumulative, string.Format(TextResolver.GetText("We like your style of government"), GovernmentAttributes.Name)));
                    }
                    if (empireEvaluation.CovetousnessCumulative < 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.CovetousnessCumulative, TextResolver.GetText("We covet your colonies and resources...")));
                    }
                    if (empireEvaluation.Blockades < 0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.Blockades, TextResolver.GetText("You have blockaded our colonies and space ports!")));
                    }
                    if (empireEvaluation.BiasRaw > 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.BiasRaw, TextResolver.GetText("We naturally like you")));
                    }
                    else if (empireEvaluation.BiasRaw < 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.BiasRaw, TextResolver.GetText("We instinctively dislike you")));
                    }
                    if (empireEvaluation.Envy < 0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.Envy, TextResolver.GetText("We are envious of your huge strength and power")));
                    }
                    if (empireEvaluation.RestrictedResourceTrading < 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.RestrictedResourceTrading, TextResolver.GetText("We are upset that you refuse to trade valuable resources with us")));
                    }
                    if (empireEvaluation.RestrictedResourceTrading > 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.RestrictedResourceTrading, TextResolver.GetText("We are happy that you trade valuable resources with us")));
                    }
                    if (empireEvaluation.MilitaryRefueling > 0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.MilitaryRefueling, TextResolver.GetText("We appreciate military refueling")));
                    }
                    if (empireEvaluation.MiningRights > 0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.MiningRights, TextResolver.GetText("We appreciate mining rights")));
                    }
                    if (empireEvaluation.IncidentEvaluationRaw < 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.IncidentEvaluationRaw, TextResolver.GetText("Our past dealings with you have been terrible")));
                    }
                    if (empireEvaluation.IncidentEvaluationRaw > 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.IncidentEvaluationRaw, TextResolver.GetText("Our past dealings with you have been good")));
                    }
                    if (empireEvaluation.SlaveryOffense < 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.SlaveryOffense, TextResolver.GetText("We are angry at your enslavement of our race at your colonies")));
                    }
                    if (empireEvaluation.RacialOffense < 0.0)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(empireEvaluation.RacialOffense, TextResolver.GetText("We are outraged at your extermination of our race at your colonies")));
                    }
                }
                for (int i = 0; i < empireRelationshipFactorList.Count; i++)
                {
                    if (empireRelationshipFactorList[i].Value > 0.0)
                    {
                        empireRelationshipFactorList[i].Value /= _Galaxy.AggressionLevel;
                        empireRelationshipFactorList[i].Value *= empireEvaluation.DiplomacyFactor;
                    }
                    else
                    {
                        empireRelationshipFactorList[i].Value *= _Galaxy.AggressionLevel;
                        empireRelationshipFactorList[i].Value /= empireEvaluation.DiplomacyFactor;
                    }
                }
                empireRelationshipFactorList.Sort();
                empireRelationshipFactorList.Reverse();
            }
            else
            {
                PirateRelation relationByOtherEmpire = otherEmpire.PirateRelations.GetRelationByOtherEmpire(this);
                if (relationByOtherEmpire != null)
                {
                    if (relationByOtherEmpire.EvaluationGiftsFactored > 0f)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(relationByOtherEmpire.EvaluationGiftsFactored, TextResolver.GetText("Pirate Evaluation Gift Description")));
                    }
                    if (relationByOtherEmpire.EvaluationOffenseOverRequestsFactored < 0f)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(relationByOtherEmpire.EvaluationOffenseOverRequestsFactored, TextResolver.GetText("Pirate Evaluation Offense Over Requests Description")));
                    }
                    else if (relationByOtherEmpire.EvaluationOffenseOverRequests > 0f)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(relationByOtherEmpire.EvaluationOffenseOverRequestsFactored, TextResolver.GetText("Pirate Evaluation Happy Over Requests Description")));
                    }
                    if (relationByOtherEmpire.EvaluationDetectedIntelligenceMissionsFactored < 0f)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(relationByOtherEmpire.EvaluationDetectedIntelligenceMissionsFactored, TextResolver.GetText("Pirate Evaluation Detected Intelligence Missions Description")));
                    }
                    if (relationByOtherEmpire.EvaluationPirateMissionsSucceedFactored > 0f)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(relationByOtherEmpire.EvaluationPirateMissionsSucceedFactored, TextResolver.GetText("Pirate Evaluation Pirate Missions Succeed Description")));
                    }
                    if (relationByOtherEmpire.EvaluationPirateMissionsFailFactored < 0f)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(relationByOtherEmpire.EvaluationPirateMissionsFailFactored, TextResolver.GetText("Pirate Evaluation Pirate Missions Fail Description")));
                    }
                    if (relationByOtherEmpire.EvaluationShipAttacksFactored < 0f)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(relationByOtherEmpire.EvaluationShipAttacksFactored, TextResolver.GetText("Pirate Evaluation Ship Attacks Description")));
                    }
                    if (relationByOtherEmpire.EvaluationProtectionCancelledFactored < 0f)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(relationByOtherEmpire.EvaluationProtectionCancelledFactored, TextResolver.GetText("Pirate Evaluation Protection Cancelled Description")));
                    }
                    if (relationByOtherEmpire.EvaluationCovetedColoniesFactored < 0f)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(relationByOtherEmpire.EvaluationCovetedColoniesFactored, TextResolver.GetText("Pirate Evaluation Coveted Colonies Description")));
                    }
                    if (relationByOtherEmpire.EvaluationLongRelationshipFactored > 0f)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(relationByOtherEmpire.EvaluationLongRelationshipFactored, TextResolver.GetText("Pirate Evaluation Long Relationship Description")));
                    }
                    if (relationByOtherEmpire.EvaluationRaidsAgainstOurColoniesFactored < 0f)
                    {
                        empireRelationshipFactorList.Add(new EmpireRelationshipFactor(relationByOtherEmpire.EvaluationRaidsAgainstOurColoniesFactored, TextResolver.GetText("Pirate Evaluation Raids Against Our Colonies Description")));
                    }
                }
            }
            return empireRelationshipFactorList;
        }

        public EmpireList DetermineEmpiresNotAtWarWithNoAmbassador()
        {
            EmpireList empireList = new EmpireList();
            EmpireList empireList2 = DetermineEmpiresKnownNotAtWarWith();
            for (int i = 0; i < empireList2.Count; i++)
            {
                Empire empire = empireList2[i];
                CharacterList characterList = Characters.FindCharactersAtLocationNotTransferring(empire.Capital);
                if (characterList.Count == 0)
                {
                    empireList.Add(empire);
                }
            }
            return empireList;
        }

        public EmpireList DetermineEmpiresKnownNotAtWarWith()
        {
            EmpireList empireList = new EmpireList();
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation.OtherEmpire != this && diplomaticRelation.OtherEmpire.PirateEmpireBaseHabitat == null && diplomaticRelation.Type != 0 && diplomaticRelation.Type != DiplomaticRelationType.War && !empireList.Contains(diplomaticRelation.OtherEmpire))
                {
                    empireList.Add(diplomaticRelation.OtherEmpire);
                }
            }
            return empireList;
        }

        public EmpireList DetermineEmpiresNotKnown()
        {
            EmpireList empireList = new EmpireList();
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation.OtherEmpire != this && diplomaticRelation.Type == DiplomaticRelationType.NotMet && diplomaticRelation.OtherEmpire.PirateEmpireBaseHabitat == null && !empireList.Contains(diplomaticRelation.OtherEmpire))
                {
                    empireList.Add(diplomaticRelation.OtherEmpire);
                }
            }
            return empireList;
        }

        public EmpireList DetermineEmpiresKnown()
        {
            EmpireList empireList = new EmpireList();
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation.OtherEmpire != this && diplomaticRelation.Type != 0 && diplomaticRelation.OtherEmpire.PirateEmpireBaseHabitat == null && !empireList.Contains(diplomaticRelation.OtherEmpire))
                {
                    empireList.Add(diplomaticRelation.OtherEmpire);
                }
            }
            return empireList;
        }

        public EmpireList DetermineEmpiresWarOrConquer()
        {
            EmpireList empireList = new EmpireList();
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if ((diplomaticRelation.Type == DiplomaticRelationType.War || diplomaticRelation.Strategy == DiplomaticStrategy.Conquer) && !empireList.Contains(diplomaticRelation.OtherEmpire))
                {
                    empireList.Add(diplomaticRelation.OtherEmpire);
                }
            }
            return empireList;
        }

        public EmpireList DetermineEmpiresWithConquerStrategy()
        {
            EmpireList empireList = new EmpireList();
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation.Strategy == DiplomaticStrategy.Conquer && !empireList.Contains(diplomaticRelation.OtherEmpire))
                {
                    empireList.Add(diplomaticRelation.OtherEmpire);
                }
            }
            return empireList;
        }

        public EmpireList DetermineEmpiresAtWarWith()
        {
            EmpireList empireList = new EmpireList();
            for (int i = 0; i < DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                if (diplomaticRelation.Type == DiplomaticRelationType.War && !empireList.Contains(diplomaticRelation.OtherEmpire))
                {
                    empireList.Add(diplomaticRelation.OtherEmpire);
                }
            }
            return empireList;
        }

        public EmpireList DetermineEmpiresAtWarWith(out int militaryStrength)
        {
            EmpireList empireList = new EmpireList();
            militaryStrength = 0;
            if (DiplomaticRelations != null)
            {
                for (int i = 0; i < DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                    if (diplomaticRelation.Type == DiplomaticRelationType.War && !empireList.Contains(diplomaticRelation.OtherEmpire))
                    {
                        empireList.Add(diplomaticRelation.OtherEmpire);
                        militaryStrength += diplomaticRelation.OtherEmpire.MilitaryPotency;
                    }
                }
            }
            return empireList;
        }

        public int CountEmpiresWeHaveMet()
        {
            int num = 0;
            if (DiplomaticRelations != null)
            {
                for (int i = 0; i < DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                    if (diplomaticRelation.Type != 0 && diplomaticRelation.OtherEmpire != null && diplomaticRelation.OtherEmpire.Active)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public EmpireList GetEmpiresWeHaveMetOfMatchingType()
        {
            EmpireList empireList = new EmpireList();
            empireList.Add(this);
            if (PirateEmpireBaseHabitat != null)
            {
                if (PirateRelations != null)
                {
                    for (int i = 0; i < PirateRelations.Count; i++)
                    {
                        PirateRelation pirateRelation = PirateRelations[i];
                        if (pirateRelation.Type != 0 && pirateRelation.OtherEmpire != null && pirateRelation.OtherEmpire.Active && !empireList.Contains(pirateRelation.OtherEmpire))
                        {
                            empireList.Add(pirateRelation.OtherEmpire);
                        }
                    }
                }
            }
            else if (DiplomaticRelations != null)
            {
                for (int j = 0; j < DiplomaticRelations.Count; j++)
                {
                    DiplomaticRelation diplomaticRelation = DiplomaticRelations[j];
                    if (diplomaticRelation.Type != 0 && diplomaticRelation.OtherEmpire != null && diplomaticRelation.OtherEmpire.Active && !empireList.Contains(diplomaticRelation.OtherEmpire))
                    {
                        empireList.Add(diplomaticRelation.OtherEmpire);
                    }
                }
            }
            return empireList;
        }

        public int CountEmpiresWeDeclaredWarOnNonLocked()
        {
            int num = 0;
            if (DiplomaticRelations != null)
            {
                for (int i = 0; i < DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                    if (diplomaticRelation.Type == DiplomaticRelationType.War && !diplomaticRelation.Locked && diplomaticRelation.Initiator == this)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public int CountEmpiresWhoDeclaredWarOnUsNonLocked()
        {
            int num = 0;
            if (DiplomaticRelations != null)
            {
                for (int i = 0; i < DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                    if (diplomaticRelation.Type == DiplomaticRelationType.War && !diplomaticRelation.Locked && diplomaticRelation.Initiator != this)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public int CountEmpiresWeDeclaredWarOn()
        {
            int num = 0;
            if (DiplomaticRelations != null)
            {
                for (int i = 0; i < DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                    if (diplomaticRelation.Type == DiplomaticRelationType.War && diplomaticRelation.Initiator == this)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public int CountEmpiresWhoDeclaredWarOnUs()
        {
            int num = 0;
            if (DiplomaticRelations != null)
            {
                for (int i = 0; i < DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                    if (diplomaticRelation.Type == DiplomaticRelationType.War && diplomaticRelation.Initiator != this)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        private Empire IdentifyTopCompetitor()
        {
            Empire result = null;
            double num = 0.0;
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire == null || !empire.Active || empire == this)
                {
                    continue;
                }
                DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(empire);
                if (diplomaticRelation == null || diplomaticRelation.Type == DiplomaticRelationType.NotMet || empire.Capital == null)
                {
                    continue;
                }
                int totalColonyStrategicValue = empire.TotalColonyStrategicValue;
                Habitat habitat = _Galaxy.FastFindNearestColony((int)empire.Capital.Xpos, (int)empire.Capital.Ypos, this, Galaxy.MajorColonyStrategicThreshhold);
                if (habitat == null)
                {
                    continue;
                }
                Habitat habitat2 = _Galaxy.FastFindNearestColony((int)habitat.Xpos, (int)habitat.Ypos, empire, Galaxy.MajorColonyStrategicThreshhold);
                if (habitat2 != null)
                {
                    double d = _Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, habitat2.Xpos, habitat2.Ypos);
                    double num2 = Math.Sqrt(d);
                    double num3 = (double)totalColonyStrategicValue / num2;
                    if (num3 > num)
                    {
                        result = empire;
                        num = num3;
                    }
                }
            }
            return result;
        }

        private bool WarInevitability(EmpireEvaluation evaluation, int ourPotencyVersusThem, double aggressionFactor, int empiresWeDeclaredWarOn, int empiresWhoDeclaredWarOnUs)
        {
            bool flag = false;
            double num = (double)DominantRace.CautionLevel / 100.0;
            if (evaluation.IncidentEvaluation < -150.0)
            {
                return true;
            }
            int num2 = Math.Max(1, (int)(aggressionFactor * 0.65 * (_Galaxy.AggressionLevel * _Galaxy.AggressionLevel)));
            _ = _Galaxy.PlayerEmpire;
            int num3 = empiresWeDeclaredWarOn + empiresWhoDeclaredWarOnUs;
            if (num3 < num2)
            {
                flag = true;
            }
            if (flag)
            {
                double num4 = (double)Galaxy.WarIncidentLevel * num;
                if (evaluation.IncidentEvaluation < num4)
                {
                    return true;
                }
                switch (ourPotencyVersusThem)
                {
                    case 1:
                        return true;
                    case 0:
                        {
                            int num6 = (int)((double)Galaxy.WarWhenEvenAngerLevel * num);
                            if (evaluation.OverallAttitude < num6)
                            {
                                return true;
                            }
                            break;
                        }
                    case -1:
                        {
                            int num5 = (int)((double)Galaxy.WarWhenStrongerAngerLevel * num);
                            if (evaluation.OverallAttitude < num5)
                            {
                                return true;
                            }
                            break;
                        }
                }
            }
            return false;
        }

        public string GenerateEmpireRestrictedResourcesDescription(out bool plural)
        {
            string text = string.Empty;
            int num = 0;
            plural = false;
            ResourceList resourceList = DetermineResourcesEmpireSupplies();
            for (int i = 0; i < resourceList.Count; i++)
            {
                Resource resource = resourceList[i];
                if (resource != null && resource.IsRestrictedResource)
                {
                    text = text + resource.Name + ", ";
                    num++;
                }
            }
            if (text.Length > 0)
            {
                text = text.Substring(0, text.Length - 2);
            }
            if (num > 1)
            {
                plural = true;
            }
            return text;
        }

        public bool CheckEmpireSuppliesRestrictedResources()
        {
            byte resource = byte.MaxValue;
            return CheckEmpireSuppliesRestrictedResources(out resource);
        }

        public bool CheckEmpireSuppliesRestrictedResources(out byte resource)
        {
            resource = byte.MaxValue;
            ResourceList resourceList = DetermineResourcesEmpireSupplies();
            for (int i = 0; i < _Galaxy.ResourceSystem.SuperLuxuryResources.Count; i++)
            {
                ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.SuperLuxuryResources[i];
                if (resourceDefinition != null && resourceList.Contains(new Resource(resourceDefinition.ResourceID)))
                {
                    resource = resourceDefinition.ResourceID;
                    return true;
                }
            }
            return false;
        }

        public double CalculateVictoryBonusFromStandingWonders(long starDate)
        {
            double num = 0.0;
            List<int> list = new List<int>();
            list.Add(1);
            List<int> list2 = list;
            if (Colonies != null && TrackedWonders != null)
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
                        if (planetaryFacility == null || planetaryFacility.Type != PlanetaryFacilityType.Wonder || planetaryFacility.WonderType != WonderType.RaceAchievement || !list2.Contains(planetaryFacility.Value2))
                        {
                            continue;
                        }
                        long buildDate = 0L;
                        if (TrackedWonders.CheckBuildDate(habitat, planetaryFacility.PlanetaryFacilityDefinitionId, out buildDate))
                        {
                            long num2 = starDate - buildDate;
                            if (num2 > 0)
                            {
                                long num3 = (long)Galaxy.RealSecondsInGalacticYear * 1000L;
                                double num4 = (double)num2 / (double)num3;
                                double num5 = num4 * 0.05;
                                num += num5;
                            }
                        }
                    }
                }
            }
            return num;
        }

        private int CalculateEnvy(Empire empire)
        {
            double relativeEmpireSize = empire.RelativeEmpireSize;
            relativeEmpireSize -= 2.0;
            relativeEmpireSize = Math.Max(0.0, relativeEmpireSize);
            double num = (double)Math.Max(1, empire.TotalColonyStrategicValue) / (double)Math.Max(1, TotalColonyStrategicValue);
            if (relativeEmpireSize > 0.0 && num > 1.0)
            {
                double num2 = 1.0;
                num2 = 1.0 + (empire.CivilityRating + 5.0) / -10.0;
                num2 = Math.Max(0.1, num2);
                num -= 1.0;
                if (num > 1.0)
                {
                    num = Math.Sqrt(Math.Min(4.0, num));
                }
                if (relativeEmpireSize > 1.0)
                {
                    relativeEmpireSize = Math.Sqrt(Math.Max(1.0, relativeEmpireSize));
                }
                int val = (int)(-12.0 * relativeEmpireSize * num2 * num);
                val = Math.Max(val, -25);
                if (empire.PlanetDestroyers != null && empire.PlanetDestroyers.Count > 0)
                {
                    val -= Math.Min(20, empire.PlanetDestroyers.Count * 8);
                }
                return BaconEmpire.CalculateEnvy(this, empire, val);
            }
            return 0;
        }

        public void SetWarObjectives(Empire otherEmpire)
        {
            DiplomaticRelation warObjectives = ObtainDiplomaticRelation(otherEmpire);
            SetWarObjectives(warObjectives);
        }

        public void SetWarObjectives(DiplomaticRelation relation)
        {
            if (relation != null)
            {
                WarObjective warObjective = WarObjective.Undefined;
                switch (relation.Strategy)
                {
                    case DiplomaticStrategy.Befriend:
                    case DiplomaticStrategy.Placate:
                    case DiplomaticStrategy.Defend:
                    case DiplomaticStrategy.Ally:
                    case DiplomaticStrategy.Undermine:
                    case DiplomaticStrategy.DefendPlacate:
                    case DiplomaticStrategy.DefendUndermine:
                    case DiplomaticStrategy.Punish:
                        warObjective = WarObjective.EndWar;
                        break;
                    case DiplomaticStrategy.Conquer:
                        warObjective = WarObjective.CaptureObjectives;
                        break;
                }
                relation.WarObjective = warObjective;
                IdentifyEmpireWarObjectives(relation.OtherEmpire, out var targetedColonies, out var targetedBases);
                if (targetedColonies.Count > 0 || targetedBases.Count > 0)
                {
                    relation.WarObjective = WarObjective.CaptureObjectives;
                    relation.WarObjectiveColonies = targetedColonies;
                    relation.WarObjectiveBases = targetedBases;
                }
                else
                {
                    relation.WarObjective = WarObjective.EndWar;
                    relation.WarObjectiveColonies = new HabitatList();
                    relation.WarObjectiveBases = new BuiltObjectList();
                }
            }
        }

        private void SendAttackFleets(Empire targetEmpire)
        {
            for (int i = 0; i < ShipGroups.Count; i++)
            {
                ShipGroup shipGroup = ShipGroups[i];
                if (shipGroup.LeadShip == null || !shipGroup.LeadShip.IsAutoControlled || shipGroup.AttackPoint == null || shipGroup.Posture != 0 || shipGroup.AttackPoint.Empire != targetEmpire || (shipGroup.Mission != null && shipGroup.Mission.Type != 0 && shipGroup.Mission.Priority != BuiltObjectMissionPriority.Low))
                {
                    continue;
                }
                if (shipGroup.AttackPoint is Habitat)
                {
                    Habitat habitat = (Habitat)shipGroup.AttackPoint;
                    if (!shipGroup.CheckFleetTargetWithinFuelRangeAndRefuel(habitat.Xpos, habitat.Ypos, 0.0))
                    {
                        ResourceList requiredFuel = new ResourceList();
                        shipGroup.CheckShipsRequiringRefuelling(0.6, out requiredFuel, includeShipsAlreadyRefuelling: true);
                        AssignFleetRefuelling(shipGroup, requiredFuel);
                    }
                    else if (CheckBombardEnemyColony(habitat, shipGroup))
                    {
                        shipGroup.AssignMission(BuiltObjectMissionType.Bombard, habitat, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                    }
                    else
                    {
                        shipGroup.AssignMission(BuiltObjectMissionType.Attack, habitat, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                    }
                }
                else if (!shipGroup.CheckFleetTargetWithinFuelRangeAndRefuel(shipGroup.AttackPoint.Xpos, shipGroup.AttackPoint.Ypos, 0.0))
                {
                    ResourceList requiredFuel2 = new ResourceList();
                    shipGroup.CheckShipsRequiringRefuelling(0.6, out requiredFuel2, includeShipsAlreadyRefuelling: true);
                    AssignFleetRefuelling(shipGroup, requiredFuel2);
                }
                else
                {
                    shipGroup.AssignMission(BuiltObjectMissionType.Attack, shipGroup.AttackPoint, null, BuiltObjectMissionPriority.High, manuallyAssigned: false);
                }
            }
        }

        public void DeclareWar(Empire target)
        {
            DeclareWar(target, null);
        }

        public void DeclareWar(Empire target, Empire persuader)
        {
            DeclareWar(target, persuader, lockedWar: false, blockFlowonEffects: false);
        }

        public void DeclareWar(Empire target, Empire persuader, bool lockedWar)
        {
            DeclareWar(target, persuader, lockedWar, blockFlowonEffects: false);
        }

        public void DeclareWar(Empire target, Empire persuader, bool lockedWar, bool blockFlowonEffects)
        {
            if (target == null)
            {
                return;
            }
            DiplomaticRelation diplomaticRelation = ObtainDiplomaticRelation(target);
            if (diplomaticRelation.Type != DiplomaticRelationType.War)
            {
                bool flag = target.CheckAtWar();
                ChangeDiplomaticRelation(diplomaticRelation, DiplomaticRelationType.War, blockFlowonEffects, lockedWar);
                CancelBlockades(target);
                target.CancelBlockades(this);
                EmpireEvaluation empireEvaluation = target.ObtainEmpireEvaluation(this);
                empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - 40.0;
                diplomaticRelation.LastDiplomacyTradeOfferDate = _Galaxy.CurrentStarDate;
                diplomaticRelation.StartDateOfLastChange = _Galaxy.CurrentStarDate;
                string description = GenerateMessageDescription(diplomaticRelation, DiplomaticRelationType.War, 0);
                if (persuader != null)
                {
                    SendMessageToEmpire(target, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.War, description, "PERSUADED");
                }
                else
                {
                    SendMessageToEmpire(target, EmpireMessageType.DiplomaticRelationChange, DiplomaticRelationType.War, description);
                }
                SendNewsBroadcastWarStartEnd(diplomaticRelation);
                if (diplomaticRelation.WarObjective == WarObjective.Undefined)
                {
                    SetWarObjectives(diplomaticRelation);
                }
                SendAttackFleets(target);
                if (_ControlMilitaryAttacks != 0 && Policy.UseExplorationShipsToScoutEnemySystems)
                {
                    EmpireList empireList = new EmpireList();
                    empireList.Add(target);
                    SendScoutShipsToEnemyLocations(empireList);
                }
                DiplomaticRelation diplomaticRelation2 = target.ObtainDiplomaticRelation(this);
                if (diplomaticRelation2 != null && diplomaticRelation2.WarObjective == WarObjective.Undefined)
                {
                    target.SetWarObjectives(diplomaticRelation2);
                }
                if (diplomaticRelation2 != null)
                {
                    diplomaticRelation2.StartDateOfLastChange = _Galaxy.CurrentStarDate;
                }
                CharacterList characterList = Characters.FindCharactersAtLocation(target.Capital);
                for (int i = 0; i < characterList.Count; i++)
                {
                    characterList[i].CompleteLocationTransfer(Capital, _Galaxy);
                }
                CharacterList characterList2 = target.Characters.FindCharactersAtLocation(Capital);
                for (int j = 0; j < characterList2.Count; j++)
                {
                    characterList2[j].CompleteLocationTransfer(target.Capital, _Galaxy);
                }
                if (!flag)
                {
                    target.ClearAttackFleetAssignments();
                }
                target.PrepareFleetsForWar(this);
                target.SendAttackFleets(this);
                if (diplomaticRelation2.WarObjective == WarObjective.CaptureObjectives)
                {
                    target.SetDefendFleets();
                }
                else
                {
                    target.SetDefendFleets(defendingFromAttack: true, assignMovement: true);
                }
                if ((this != _Galaxy.PlayerEmpire || _ControlMilitaryAttacks == AutomationLevel.FullyAutomated) && diplomaticRelation.WarObjective != WarObjective.CaptureObjectives)
                {
                    IdentifyMilitaryObjectives();
                }
                if (target != _Galaxy.PlayerEmpire && diplomaticRelation2.WarObjective != WarObjective.CaptureObjectives)
                {
                    target.IdentifyMilitaryObjectives();
                }
                ReviewDefensiveFleetLocations();
            }
            else if (lockedWar)
            {
                diplomaticRelation.Locked = true;
                DiplomaticRelation diplomaticRelation3 = target.ObtainDiplomaticRelation(this);
                if (diplomaticRelation3 != null)
                {
                    diplomaticRelation3.Locked = true;
                }
            }
        }

        public int DetermineRelativeStrength(int ourStrength, Empire otherEmpire)
        {
            int result = 0;
            double num = 1.5;
            double num2 = 0.65;
            if (otherEmpire == _Galaxy.PlayerEmpire)
            {
                num /= _Galaxy.PlayerEmpire.DifficultyLevel;
                num2 /= _Galaxy.PlayerEmpire.DifficultyLevel;
            }
            double num3 = (double)ourStrength / (double)otherEmpire.MilitaryPotency;
            if (num3 < num2)
            {
                result = -1;
            }
            else if (num3 > num)
            {
                result = 1;
            }
            return result;
        }

        private DiplomaticRelationList SummaryCopy(DiplomaticRelation[] relations)
        {
            DiplomaticRelationList diplomaticRelationList = new DiplomaticRelationList();
            foreach (DiplomaticRelation diplomaticRelation in relations)
            {
                DiplomaticRelation diplomaticRelation2 = new DiplomaticRelation(diplomaticRelation.Type, diplomaticRelation.Initiator, diplomaticRelation.ThisEmpire, diplomaticRelation.OtherEmpire, diplomaticRelation.SupplyRestrictedResources);
                diplomaticRelation2.Strategy = diplomaticRelation.Strategy;
                diplomaticRelationList.Add(diplomaticRelation2);
            }
            return diplomaticRelationList;
        }

        public Habitat CheckEmpireBuildingVictoryWonderAtKnownColony(Empire empire)
        {
            Habitat habitat = CheckEmpireBuildingVictoryWonder(empire);
            if (habitat != null)
            {
                Habitat systemStar = Galaxy.DetermineHabitatSystemStar(habitat);
                if (CheckSystemExplored(systemStar))
                {
                    return habitat;
                }
            }
            return null;
        }

    }
}
