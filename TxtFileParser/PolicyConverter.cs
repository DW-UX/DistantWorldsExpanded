using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TxtFileParser
{
    internal class PolicyConverter
    {
        private const string _tableName = "EmpirePolicy";
        private const string _ID = "ID";
        private const string _EmpireNameCol = "EmpireName";
        private const string _ImmediatelyRecruitNewTroopsWhenColonizeCol = "ImmediatelyRecruitNewTroopsWhenColonize";
        private const string _ColonyAllowFacilityCloningFacilityCol = "ColonyAllowFacilityCloningFacility";
        private const string _ColonyAllowFacilityFortifiedBunkerCol = "ColonyAllowFacilityFortifiedBunker";
        private const string _ColonyAllowFacilityGiantIonCannonCol = "ColonyAllowFacilityGiantIonCannon";
        private const string _ColonyAllowFacilityPlanetaryShieldCol = "ColonyAllowFacilityPlanetaryShield";
        private const string _ColonyAllowFacilityRegionalCapitalCol = "ColonyAllowFacilityRegionalCapital";
        private const string _ColonyAllowFacilityRoboticTroopFoundryCol = "ColonyAllowFacilityRoboticTroopFoundry";
        private const string _ColonyAllowFacilityTerraformingFacilityCol = "ColonyAllowFacilityTerraformingFacility";
        private const string _ColonyAllowFacilityTroopTrainingCenterCol = "ColonyAllowFacilityTroopTrainingCenter";
        private const string _ColonyAllowFacilityArmoredFactoryCol = "ColonyAllowFacilityArmoredFactory";
        private const string _ColonyAllowFacilitySpyAcademyCol = "ColonyAllowFacilitySpyAcademy";
        private const string _ColonyAllowFacilityScienceAcademyCol = "ColonyAllowFacilityScienceAcademy";
        private const string _ColonyAllowFacilityNavalAcademyCol = "ColonyAllowFacilityNavalAcademy";
        private const string _ColonyAllowFacilityMilitaryAcademyCol = "ColonyAllowFacilityMilitaryAcademy";
        private const string _ColonyFacilityPopulationThresholdCloningFacilityCol = "ColonyFacilityPopulationThresholdCloningFacility";
        private const string _ColonyFacilityPopulationThresholdFortifiedBunkerCol = "ColonyFacilityPopulationThresholdFortifiedBunker";
        private const string _ColonyFacilityPopulationThresholdGiantIonCannonCol = "ColonyFacilityPopulationThresholdGiantIonCannon";
        private const string _ColonyFacilityPopulationThresholdPlanetaryShieldCol = "ColonyFacilityPopulationThresholdPlanetaryShield";
        private const string _ColonyFacilityPopulationThresholdRegionalCapitalCol = "ColonyFacilityPopulationThresholdRegionalCapital";
        private const string _ColonyFacilityPopulationThresholdRoboticTroopFoundryCol = "ColonyFacilityPopulationThresholdRoboticTroopFoundry";
        private const string _ColonyFacilityPopulationThresholdTerraformingFacilityCol = "ColonyFacilityPopulationThresholdTerraformingFacility";
        private const string _ColonyFacilityPopulationThresholdTroopTrainingCenterCol = "ColonyFacilityPopulationThresholdTroopTrainingCenter";
        private const string _ColonyFacilityPopulationThresholdArmoredFactoryCol = "ColonyFacilityPopulationThresholdArmoredFactory";
        private const string _ColonyFacilityPopulationThresholdSpyAcademyCol = "ColonyFacilityPopulationThresholdSpyAcademy";
        private const string _ColonyFacilityPopulationThresholdScienceAcademyCol = "ColonyFacilityPopulationThresholdScienceAcademy";
        private const string _ColonyFacilityPopulationThresholdNavalAcademyCol = "ColonyFacilityPopulationThresholdNavalAcademy";
        private const string _ColonyFacilityPopulationThresholdMilitaryAcademyCol = "ColonyFacilityPopulationThresholdMilitaryAcademy";
        private const string _ColonyPopulationThresholdTroopRecruitmentCol = "ColonyPopulationThresholdTroopRecruitment";
        private const string _ColonyTaxRateIncreaseWhenAtWarCol = "ColonyTaxRateIncreaseWhenAtWar";
        private const string _ColonyTaxRateLargeColonyCol = "ColonyTaxRateLargeColony";
        private const string _ColonyTaxRateMediumColonyCol = "ColonyTaxRateMediumColony";
        private const string _ColonyTaxRateSmallColonyCol = "ColonyTaxRateSmallColony";
        private const string _MilitaryConstructionLevelCol = "MilitaryConstructionLevel";
        private const string _ConstructionMilitaryCapitalShipCol = "ConstructionMilitaryCapitalShip";
        private const string _ConstructionMilitaryCarrierCol = "ConstructionMilitaryCarrier";
        private const string _ConstructionMilitaryCruiserCol = "ConstructionMilitaryCruiser";
        private const string _ConstructionMilitaryDestroyerCol = "ConstructionMilitaryDestroyer";
        private const string _ConstructionMilitaryEscortCol = "ConstructionMilitaryEscort";
        private const string _ConstructionMilitaryFrigateCol = "ConstructionMilitaryFrigate";
        private const string _ConstructionMilitaryTroopTransportCol = "ConstructionMilitaryTroopTransport";
        private const string _ConstructionSpaceportLargeColonyPopulationThresholdCol = "ConstructionSpaceportLargeColonyPopulationThreshold";
        private const string _ConstructionSpaceportMediumColonyPopulationThresholdCol = "ConstructionSpaceportMediumColonyPopulationThreshold";
        private const string _ConstructionSpaceportSmallColonyPopulationThresholdCol = "ConstructionSpaceportSmallColonyPopulationThreshold";
        private const string _ConstructionSpaceportMinimumDistanceCol = "ConstructionSpaceportMinimumDistance";
        private const string _DiplomacySendGiftsUpToAmountCol = "DiplomacySendGiftsUpToAmount";
        private const string _DiplomacyTradeSanctionsUseBlockadesCol = "DiplomacyTradeSanctionsUseBlockades";
        private const string _FleetMilitaryProportionForFleetsCol = "FleetMilitaryProportionForFleets";
        private const string _FleetStrikeForceTypicalSizeCol = "FleetStrikeForceTypicalSize";
        private const string _FleetTypicalSizeCol = "FleetTypicalSize";
        private const string _IntelligenceAllowMissionDeepCoverCol = "IntelligenceAllowMissionDeepCover";
        private const string _IntelligenceAllowMissionInciteRevolutionCol = "IntelligenceAllowMissionInciteRevolution";
        private const string _IntelligenceAllowMissionSabotageColonyCol = "IntelligenceAllowMissionSabotageColony";
        private const string _IntelligenceAllowMissionSabotageConstructionCol = "IntelligenceAllowMissionSabotageConstruction";
        private const string _IntelligenceAllowMissionStealGalaxyMapCol = "IntelligenceAllowMissionStealGalaxyMap";
        private const string _IntelligenceAllowMissionStealOperationsMapCol = "IntelligenceAllowMissionStealOperationsMap";
        private const string _IntelligenceAllowMissionStealTechDataCol = "IntelligenceAllowMissionStealTechData";
        private const string _IntelligenceAllowMissionStealTerritoryMapCol = "IntelligenceAllowMissionStealTerritoryMap";
        private const string _IntelligenceAllowMissionAssassinateCharacterCol = "IntelligenceAllowMissionAssassinateCharacter";
        private const string _IntelligenceAllowMissionDestroyBaseCol = "IntelligenceAllowMissionDestroyBase";
        private const string _IntelligenceCounterIntelligenceProportionCol = "IntelligenceCounterIntelligenceProportion";
        private const string _IntelligenceUseEspionageAgainstEmpireWhenCol = "IntelligenceUseEspionageAgainstEmpireWhen";
        private const string _IntelligenceUseSabotageAgainstEmpireWhenCol = "IntelligenceUseSabotageAgainstEmpireWhen";
        private const string _ResearchDesignAutoRetrofitCol = "ResearchDesignAutoRetrofit";
        private const string _ResearchDesignOverallFocusCol = "ResearchDesignOverallFocus";
        private const string _ResearchDesignTechFocus1Col = "ResearchDesignTechFocus1";
        private const string _ResearchDesignTechFocus2Col = "ResearchDesignTechFocus2";
        private const string _ResearchDesignTechFocus3Col = "ResearchDesignTechFocus3";
        private const string _ResearchDesignTechFocus4Col = "ResearchDesignTechFocus4";
        private const string _ResearchDesignTechFocus5Col = "ResearchDesignTechFocus5";
        private const string _ResearchDesignTechFocus6Col = "ResearchDesignTechFocus6";
        private const string _ResearchDesignAutoUpgradeFightersCol = "ResearchDesignAutoUpgradeFighters";
        private const string _WarAttacksAllowColonyBombardmentCol = "WarAttacksAllowColonyBombardment";
        private const string _WarAttacksAllowPlanetDestroyingCol = "WarAttacksAllowPlanetDestroying";
        private const string _WarAttacksHarassEnemiesCol = "WarAttacksHarassEnemies";
        private const string _TradeWithOtherEmpiresCol = "TradeWithOtherEmpires";
        private const string _EngageInTourismCol = "EngageInTourism";
        private const string _NewColonyPopulationPolicyYourRaceFamilyCol = "NewColonyPopulationPolicyYourRaceFamily";
        private const string _NewColonyPopulationPolicyAllRacesCol = "NewColonyPopulationPolicyAllRaces";
        private const string _ImplementEnslavementWithPenalColoniesCol = "ImplementEnslavementWithPenalColonies";
        private const string _HomeworldDefensePriorityCol = "HomeworldDefensePriority";
        private const string _ProtectLeaderAtAllCostsCol = "ProtectLeaderAtAllCosts";
        private const string _PrioritizeBuildWonderIdCol = "PrioritizeBuildWonderId";
        private const string _ColonizeContinentalPriorityCol = "ColonizeContinentalPriority";
        private const string _ColonizeMarshySwampPriorityCol = "ColonizeMarshySwampPriority";
        private const string _ColonizeOceanPriorityCol = "ColonizeOceanPriority";
        private const string _ColonizeDesertPriorityCol = "ColonizeDesertPriority";
        private const string _ColonizeIcePriorityCol = "ColonizeIcePriority";
        private const string _ColonizeVolcanicPriorityCol = "ColonizeVolcanicPriority";
        private const string _ColonizeRuinsPriorityCol = "ColonizeRuinsPriority";
        private const string _ControlRestrictedResourcesPriorityCol = "ControlRestrictedResourcesPriority";
        private const string _ResearchIndustryFocusCol = "ResearchIndustryFocus";
        private const string _ResearchPriorityCol = "ResearchPriority";
        private const string _TradePriorityCol = "TradePriority";
        private const string _AlliancePriorityCol = "AlliancePriority";
        private const string _SubjugationPriorityCol = "SubjugationPriority";
        private const string _TourismPriorityCol = "TourismPriority";
        private const string _ExplorationPriorityCol = "ExplorationPriority";
        private const string _WarWillingnessCol = "WarWillingness";
        private const string _BreakTreatyWillingnessCol = "BreakTreatyWillingness";
        private const string _InvasionOverkillFactorCol = "InvasionOverkillFactor";
        private const string _ShipBattleCautionFactorCol = "ShipBattleCautionFactor";
        private const string _DefaultMilitaryFleeWhenCol = "DefaultMilitaryFleeWhen";
        private const string _DesignUpgradeEscortCol = "DesignUpgradeEscort";
        private const string _DesignUpgradeFrigateCol = "DesignUpgradeFrigate";
        private const string _DesignUpgradeDestroyerCol = "DesignUpgradeDestroyer";
        private const string _DesignUpgradeCruiserCol = "DesignUpgradeCruiser";
        private const string _DesignUpgradeCapitalShipCol = "DesignUpgradeCapitalShip";
        private const string _DesignUpgradeTroopTransportCol = "DesignUpgradeTroopTransport";
        private const string _DesignUpgradeCarrierCol = "DesignUpgradeCarrier";
        private const string _DesignUpgradeResupplyShipCol = "DesignUpgradeResupplyShip";
        private const string _DesignUpgradeExplorationShipCol = "DesignUpgradeExplorationShip";
        private const string _DesignUpgradeColonyShipCol = "DesignUpgradeColonyShip";
        private const string _DesignUpgradeConstructionShipCol = "DesignUpgradeConstructionShip";
        private const string _DesignUpgradeSmallSpacePortCol = "DesignUpgradeSmallSpacePort";
        private const string _DesignUpgradeMediumSpacePortCol = "DesignUpgradeMediumSpacePort";
        private const string _DesignUpgradeLargeSpacePortCol = "DesignUpgradeLargeSpacePort";
        private const string _DesignUpgradeResortBaseCol = "DesignUpgradeResortBase";
        private const string _DesignUpgradeGenericBaseCol = "DesignUpgradeGenericBase";
        private const string _DesignUpgradeEnergyResearchStationCol = "DesignUpgradeEnergyResearchStation";
        private const string _DesignUpgradeWeaponsResearchStationCol = "DesignUpgradeWeaponsResearchStation";
        private const string _DesignUpgradeHighTechResearchStationCol = "DesignUpgradeHighTechResearchStation";
        private const string _DesignUpgradeMonitoringStationCol = "DesignUpgradeMonitoringStation";
        private const string _DesignUpgradeDefensiveBaseCol = "DesignUpgradeDefensiveBase";
        private const string _DesignUpgradeSmallFreighterCol = "DesignUpgradeSmallFreighter";
        private const string _DesignUpgradeMediumFreighterCol = "DesignUpgradeMediumFreighter";
        private const string _DesignUpgradeLargeFreighterCol = "DesignUpgradeLargeFreighter";
        private const string _DesignUpgradePassengerShipCol = "DesignUpgradePassengerShip";
        private const string _DesignUpgradeGasMiningShipCol = "DesignUpgradeGasMiningShip";
        private const string _DesignUpgradeMiningShipCol = "DesignUpgradeMiningShip";
        private const string _DesignUpgradeGasMiningStationCol = "DesignUpgradeGasMiningStation";
        private const string _DesignUpgradeMiningStationCol = "DesignUpgradeMiningStation";
        private const string _CaptureTargetConditionShipCol = "CaptureTargetConditionShip";
        private const string _CaptureTargetConditionBaseCol = "CaptureTargetConditionBase";
        private const string _OfferPirateAttackMissionsCol = "OfferPirateAttackMissions";
        private const string _BidOnPirateAttackMissionsCol = "BidOnPirateAttackMissions";
        private const string _BidOnPirateDefendMissionsCol = "BidOnPirateDefendMissions";
        private const string _OfferDefensivePirateMissionsCol = "OfferDefensivePirateMissions";
        private const string _OfferDefensivePirateMissionsSituationCol = "OfferDefensivePirateMissionsSituation";
        private const string _AcceptPirateSmugglingMissionsCol = "AcceptPirateSmugglingMissions";
        private const string _OfferSmugglingPirateMissionsCol = "OfferSmugglingPirateMissions";
        private const string _PirateSmugglerFreighterLevelCol = "PirateSmugglerFreighterLevel";
        private const string _PirateSmugglerMiningLevelCol = "PirateSmugglerMiningLevel";
        private const string _PirateSmugglerPassengerLevelCol = "PirateSmugglerPassengerLevel";
        private const string _CaptureEnlistMilitaryShipCol = "CaptureEnlistMilitaryShip";
        private const string _CaptureDisassembleMilitaryShipCol = "CaptureDisassembleMilitaryShip";
        private const string _CaptureEnlistCivilianShipCol = "CaptureEnlistCivilianShip";
        private const string _CaptureDisassembleCivilianShipCol = "CaptureDisassembleCivilianShip";
        private const string _CaptureEnlistBaseCol = "CaptureEnlistBase";
        private const string _UpgradeEnlistedMilitaryShipsCol = "UpgradeEnlistedMilitaryShips";
        private const string _UpgradeEnlistedCivilianShipsCol = "UpgradeEnlistedCivilianShips";
        private const string _TroopRecruitInfantryLevelCol = "TroopRecruitInfantryLevel";
        private const string _TroopRecruitArmorLevelCol = "TroopRecruitArmorLevel";
        private const string _TroopRecruitArtilleryLevelCol = "TroopRecruitArtilleryLevel";
        private const string _TroopRecruitSpecialForcesLevelCol = "TroopRecruitSpecialForcesLevel";
        private const string _TroopUseDefaultTransportLoadoutCol = "TroopUseDefaultTransportLoadout";
        private const string _TroopDefaultTransportLoadoutInfantryCol = "TroopDefaultTransportLoadoutInfantry";
        private const string _TroopDefaultTransportLoadoutArmorCol = "TroopDefaultTransportLoadoutArmor";
        private const string _TroopDefaultTransportLoadoutArtilleryCol = "TroopDefaultTransportLoadoutArtillery";
        private const string _TroopDefaultTransportLoadoutSpecialForcesCol = "TroopDefaultTransportLoadoutSpecialForces";
        private const string _TroopGarrisonMinimumPerColonyCol = "TroopGarrisonMinimumPerColony";
        private const string _TroopGarrisonLevelCol = "TroopGarrisonLevel";
        private const string _UseExplorationShipsToScoutEnemySystemsCol = "UseExplorationShipsToScoutEnemySystems";
        private const string _BuildPlanetDestroyersCol = "BuildPlanetDestroyers";
        private readonly ConvertType convertType;

        public PolicyConverter(ConvertType convertType)
        {
            this.convertType = convertType;
        }

        public bool Parse(string dirPath, string outputFolder)
        {
            bool res = true;
            if (!Directory.Exists(dirPath))
            {
                Console.WriteLine($"Policy folder {dirPath} not found");
                return true;
            }
            if (!Directory.EnumerateFiles(dirPath).Any())
            {
                Console.WriteLine("Policy folder contains no files, skipped");
                return true;
            }
            try
            {
                string fields = "ID,EmpireName,ImmediatelyRecruitNewTroopsWhenColonize,ColonyAllowFacilityCloningFacility,ColonyAllowFacilityFortifiedBunker,ColonyAllowFacilityGiantIonCannon,ColonyAllowFacilityPlanetaryShield,ColonyAllowFacilityRegionalCapital,ColonyAllowFacilityRoboticTroopFoundry,ColonyAllowFacilityTerraformingFacility,ColonyAllowFacilityTroopTrainingCenter,ColonyAllowFacilityArmoredFactory,ColonyAllowFacilitySpyAcademy,ColonyAllowFacilityScienceAcademy,ColonyAllowFacilityNavalAcademy,ColonyAllowFacilityMilitaryAcademy,ColonyFacilityPopulationThresholdCloningFacility,ColonyFacilityPopulationThresholdFortifiedBunker,ColonyFacilityPopulationThresholdGiantIonCannon,ColonyFacilityPopulationThresholdPlanetaryShield,ColonyFacilityPopulationThresholdRegionalCapital,ColonyFacilityPopulationThresholdRoboticTroopFoundry,ColonyFacilityPopulationThresholdTerraformingFacility,ColonyFacilityPopulationThresholdTroopTrainingCenter,ColonyFacilityPopulationThresholdArmoredFactory,ColonyFacilityPopulationThresholdSpyAcademy,ColonyFacilityPopulationThresholdScienceAcademy,ColonyFacilityPopulationThresholdNavalAcademy,ColonyFacilityPopulationThresholdMilitaryAcademy,ColonyPopulationThresholdTroopRecruitment,ColonyTaxRateIncreaseWhenAtWar,ColonyTaxRateLargeColony,ColonyTaxRateMediumColony,ColonyTaxRateSmallColony,MilitaryConstructionLevel,ConstructionMilitaryCapitalShip,ConstructionMilitaryCarrier,ConstructionMilitaryCruiser,ConstructionMilitaryDestroyer,ConstructionMilitaryEscort,ConstructionMilitaryFrigate,ConstructionMilitaryTroopTransport,ConstructionSpaceportLargeColonyPopulationThreshold,ConstructionSpaceportMediumColonyPopulationThreshold,ConstructionSpaceportSmallColonyPopulationThreshold,ConstructionSpaceportMinimumDistance,DiplomacySendGiftsUpToAmount,DiplomacyTradeSanctionsUseBlockades,FleetMilitaryProportionForFleets,FleetStrikeForceTypicalSize,FleetTypicalSize,IntelligenceAllowMissionDeepCover,IntelligenceAllowMissionInciteRevolution,IntelligenceAllowMissionSabotageColony,IntelligenceAllowMissionSabotageConstruction,IntelligenceAllowMissionStealGalaxyMap,IntelligenceAllowMissionStealOperationsMap,IntelligenceAllowMissionStealTechData,IntelligenceAllowMissionStealTerritoryMap,IntelligenceAllowMissionAssassinateCharacter,IntelligenceAllowMissionDestroyBase,IntelligenceCounterIntelligenceProportion,IntelligenceUseEspionageAgainstEmpireWhen,IntelligenceUseSabotageAgainstEmpireWhen,ResearchDesignAutoRetrofit,ResearchDesignOverallFocus,ResearchDesignTechFocus1,ResearchDesignTechFocus2,ResearchDesignTechFocus3,ResearchDesignTechFocus4,ResearchDesignTechFocus5,ResearchDesignTechFocus6,ResearchDesignAutoUpgradeFighters,WarAttacksAllowColonyBombardment,WarAttacksAllowPlanetDestroying,WarAttacksHarassEnemies,TradeWithOtherEmpires,EngageInTourism,NewColonyPopulationPolicyYourRaceFamily,NewColonyPopulationPolicyAllRaces,ImplementEnslavementWithPenalColonies,HomeworldDefensePriority,ProtectLeaderAtAllCosts,PrioritizeBuildWonderId,ColonizeContinentalPriority,ColonizeMarshySwampPriority,ColonizeOceanPriority,ColonizeDesertPriority,ColonizeIcePriority,ColonizeVolcanicPriority,ColonizeRuinsPriority,ControlRestrictedResourcesPriority,ResearchIndustryFocus,ResearchPriority,TradePriority,AlliancePriority,SubjugationPriority,TourismPriority,ExplorationPriority,WarWillingness,BreakTreatyWillingness,InvasionOverkillFactor,ShipBattleCautionFactor,DefaultMilitaryFleeWhen,DesignUpgradeEscort,DesignUpgradeFrigate,DesignUpgradeDestroyer,DesignUpgradeCruiser,DesignUpgradeCapitalShip,DesignUpgradeTroopTransport,DesignUpgradeCarrier,DesignUpgradeResupplyShip,DesignUpgradeExplorationShip,DesignUpgradeColonyShip,DesignUpgradeConstructionShip,DesignUpgradeSmallSpacePort,DesignUpgradeMediumSpacePort,DesignUpgradeLargeSpacePort,DesignUpgradeResortBase,DesignUpgradeGenericBase,DesignUpgradeEnergyResearchStation,DesignUpgradeWeaponsResearchStation,DesignUpgradeHighTechResearchStation,DesignUpgradeMonitoringStation,DesignUpgradeDefensiveBase,DesignUpgradeSmallFreighter,DesignUpgradeMediumFreighter,DesignUpgradeLargeFreighter,DesignUpgradePassengerShip,DesignUpgradeGasMiningShip,DesignUpgradeMiningShip,DesignUpgradeGasMiningStation,DesignUpgradeMiningStation,CaptureTargetConditionShip,CaptureTargetConditionBase,OfferPirateAttackMissions,BidOnPirateAttackMissions,BidOnPirateDefendMissions,OfferDefensivePirateMissions,OfferDefensivePirateMissionsSituation,AcceptPirateSmugglingMissions,OfferSmugglingPirateMissions,PirateSmugglerFreighterLevel,PirateSmugglerMiningLevel,PirateSmugglerPassengerLevel,CaptureEnlistMilitaryShip,CaptureDisassembleMilitaryShip,CaptureEnlistCivilianShip,CaptureDisassembleCivilianShip,CaptureEnlistBase,UpgradeEnlistedMilitaryShips,UpgradeEnlistedCivilianShips,TroopRecruitInfantryLevel,TroopRecruitArmorLevel,TroopRecruitArtilleryLevel,TroopRecruitSpecialForcesLevel,TroopUseDefaultTransportLoadout,TroopDefaultTransportLoadoutInfantry,TroopDefaultTransportLoadoutArmor,TroopDefaultTransportLoadoutArtillery,TroopDefaultTransportLoadoutSpecialForces,TroopGarrisonMinimumPerColony,TroopGarrisonLevel,UseExplorationShipsToScoutEnemySystems,BuildPlanetDestroyers";


                DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
                List<Dictionary<string, string>> values = new List<Dictionary<string, string>>();
                int id = 0;
                foreach (var item in directoryInfo.GetFiles("*.txt", SearchOption.TopDirectoryOnly))
                {
                    using FileStream fileStream = item.OpenRead();
                    using StreamReader streamReader = new StreamReader(fileStream);

                    values.Add(new Dictionary<string, string>());
                    foreach (var itemField in fields.Split(','))
                    {
                        values[id].Add(itemField, string.Empty);
                    }
                    while (!streamReader.EndOfStream)
                    {
                        var temp = GetValidFileLine(streamReader).Replace('\'', '′').Split(";", StringSplitOptions.TrimEntries);
                        values[id][temp[0]] = temp[1];
                    }
                    values[id][_ID] = id.ToString();
                    values[id][_EmpireNameCol] = Path.GetFileNameWithoutExtension(item.FullName).Replace('\'', '′');
                    id++;
                }
                ReplaceBoolValue(values);
                WriteXml(dirPath, outputFolder, values);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Policy conversion error: {ex.Message}");
                res = false;
            }
            return res;
        }

        private void ReplaceBoolValue(List<Dictionary<string, string>> values)
        {
            foreach (var valueDict in values)
            {
                // Colony-related settings
                valueDict[_ImmediatelyRecruitNewTroopsWhenColonizeCol] = ConvertBool(valueDict[_ImmediatelyRecruitNewTroopsWhenColonizeCol]);
                valueDict[_ColonyAllowFacilityCloningFacilityCol] = ConvertBool(valueDict[_ColonyAllowFacilityCloningFacilityCol]);
                valueDict[_ColonyAllowFacilityFortifiedBunkerCol] = ConvertBool(valueDict[_ColonyAllowFacilityFortifiedBunkerCol]);
                valueDict[_ColonyAllowFacilityGiantIonCannonCol] = ConvertBool(valueDict[_ColonyAllowFacilityGiantIonCannonCol]);
                valueDict[_ColonyAllowFacilityPlanetaryShieldCol] = ConvertBool(valueDict[_ColonyAllowFacilityPlanetaryShieldCol]);
                valueDict[_ColonyAllowFacilityRegionalCapitalCol] = ConvertBool(valueDict[_ColonyAllowFacilityRegionalCapitalCol]);
                valueDict[_ColonyAllowFacilityRoboticTroopFoundryCol] = ConvertBool(valueDict[_ColonyAllowFacilityRoboticTroopFoundryCol]);
                valueDict[_ColonyAllowFacilityTerraformingFacilityCol] = ConvertBool(valueDict[_ColonyAllowFacilityTerraformingFacilityCol]);
                valueDict[_ColonyAllowFacilityTroopTrainingCenterCol] = ConvertBool(valueDict[_ColonyAllowFacilityTroopTrainingCenterCol]);
                valueDict[_ColonyAllowFacilityArmoredFactoryCol] = ConvertBool(valueDict[_ColonyAllowFacilityArmoredFactoryCol]);
                valueDict[_ColonyAllowFacilitySpyAcademyCol] = ConvertBool(valueDict[_ColonyAllowFacilitySpyAcademyCol]);
                valueDict[_ColonyAllowFacilityScienceAcademyCol] = ConvertBool(valueDict[_ColonyAllowFacilityScienceAcademyCol]);
                valueDict[_ColonyAllowFacilityNavalAcademyCol] = ConvertBool(valueDict[_ColonyAllowFacilityNavalAcademyCol]);
                valueDict[_ColonyAllowFacilityMilitaryAcademyCol] = ConvertBool(valueDict[_ColonyAllowFacilityMilitaryAcademyCol]);

                // Research and Design
                valueDict[_ResearchDesignAutoRetrofitCol] = ConvertBool(valueDict[_ResearchDesignAutoRetrofitCol]);
                valueDict[_ResearchDesignAutoUpgradeFightersCol] = ConvertBool(valueDict[_ResearchDesignAutoUpgradeFightersCol]);

                // Tax and Colony settings
                valueDict[_ColonyTaxRateIncreaseWhenAtWarCol] = ConvertBool(valueDict[_ColonyTaxRateIncreaseWhenAtWarCol]);

                // Diplomacy settings
                valueDict[_DiplomacyTradeSanctionsUseBlockadesCol] = ConvertBool(valueDict[_DiplomacyTradeSanctionsUseBlockadesCol]);

                // Intelligence settings
                valueDict[_IntelligenceAllowMissionDeepCoverCol] = ConvertBool(valueDict[_IntelligenceAllowMissionDeepCoverCol]);
                valueDict[_IntelligenceAllowMissionInciteRevolutionCol] = ConvertBool(valueDict[_IntelligenceAllowMissionInciteRevolutionCol]);
                valueDict[_IntelligenceAllowMissionSabotageColonyCol] = ConvertBool(valueDict[_IntelligenceAllowMissionSabotageColonyCol]);
                valueDict[_IntelligenceAllowMissionSabotageConstructionCol] = ConvertBool(valueDict[_IntelligenceAllowMissionSabotageConstructionCol]);
                valueDict[_IntelligenceAllowMissionStealGalaxyMapCol] = ConvertBool(valueDict[_IntelligenceAllowMissionStealGalaxyMapCol]);
                valueDict[_IntelligenceAllowMissionStealOperationsMapCol] = ConvertBool(valueDict[_IntelligenceAllowMissionStealOperationsMapCol]);
                valueDict[_IntelligenceAllowMissionStealTechDataCol] = ConvertBool(valueDict[_IntelligenceAllowMissionStealTechDataCol]);
                valueDict[_IntelligenceAllowMissionStealTerritoryMapCol] = ConvertBool(valueDict[_IntelligenceAllowMissionStealTerritoryMapCol]);
                valueDict[_IntelligenceAllowMissionAssassinateCharacterCol] = ConvertBool(valueDict[_IntelligenceAllowMissionAssassinateCharacterCol]);
                valueDict[_IntelligenceAllowMissionDestroyBaseCol] = ConvertBool(valueDict[_IntelligenceAllowMissionDestroyBaseCol]);

                // War and Military settings
                valueDict[_WarAttacksHarassEnemiesCol] = ConvertBool(valueDict[_WarAttacksHarassEnemiesCol]);

                // Design Upgrade settings
                valueDict[_DesignUpgradeEscortCol] = ConvertBool(valueDict[_DesignUpgradeEscortCol]);
                valueDict[_DesignUpgradeFrigateCol] = ConvertBool(valueDict[_DesignUpgradeFrigateCol]);
                valueDict[_DesignUpgradeDestroyerCol] = ConvertBool(valueDict[_DesignUpgradeDestroyerCol]);
                valueDict[_DesignUpgradeCruiserCol] = ConvertBool(valueDict[_DesignUpgradeCruiserCol]);
                valueDict[_DesignUpgradeCapitalShipCol] = ConvertBool(valueDict[_DesignUpgradeCapitalShipCol]);
                valueDict[_DesignUpgradeTroopTransportCol] = ConvertBool(valueDict[_DesignUpgradeTroopTransportCol]);
                valueDict[_DesignUpgradeCarrierCol] = ConvertBool(valueDict[_DesignUpgradeCarrierCol]);
                valueDict[_DesignUpgradeResupplyShipCol] = ConvertBool(valueDict[_DesignUpgradeResupplyShipCol]);
                valueDict[_DesignUpgradeExplorationShipCol] = ConvertBool(valueDict[_DesignUpgradeExplorationShipCol]);
                valueDict[_DesignUpgradeColonyShipCol] = ConvertBool(valueDict[_DesignUpgradeColonyShipCol]);
                valueDict[_DesignUpgradeConstructionShipCol] = ConvertBool(valueDict[_DesignUpgradeConstructionShipCol]);
                valueDict[_DesignUpgradeSmallSpacePortCol] = ConvertBool(valueDict[_DesignUpgradeSmallSpacePortCol]);
                valueDict[_DesignUpgradeMediumSpacePortCol] = ConvertBool(valueDict[_DesignUpgradeMediumSpacePortCol]);
                valueDict[_DesignUpgradeLargeSpacePortCol] = ConvertBool(valueDict[_DesignUpgradeLargeSpacePortCol]);
                valueDict[_DesignUpgradeResortBaseCol] = ConvertBool(valueDict[_DesignUpgradeResortBaseCol]);
                valueDict[_DesignUpgradeGenericBaseCol] = ConvertBool(valueDict[_DesignUpgradeGenericBaseCol]);
                valueDict[_DesignUpgradeEnergyResearchStationCol] = ConvertBool(valueDict[_DesignUpgradeEnergyResearchStationCol]);
                valueDict[_DesignUpgradeWeaponsResearchStationCol] = ConvertBool(valueDict[_DesignUpgradeWeaponsResearchStationCol]);
                valueDict[_DesignUpgradeHighTechResearchStationCol] = ConvertBool(valueDict[_DesignUpgradeHighTechResearchStationCol]);
                valueDict[_DesignUpgradeMonitoringStationCol] = ConvertBool(valueDict[_DesignUpgradeMonitoringStationCol]);
                valueDict[_DesignUpgradeDefensiveBaseCol] = ConvertBool(valueDict[_DesignUpgradeDefensiveBaseCol]);
                valueDict[_DesignUpgradeSmallFreighterCol] = ConvertBool(valueDict[_DesignUpgradeSmallFreighterCol]);
                valueDict[_DesignUpgradeMediumFreighterCol] = ConvertBool(valueDict[_DesignUpgradeMediumFreighterCol]);
                valueDict[_DesignUpgradeLargeFreighterCol] = ConvertBool(valueDict[_DesignUpgradeLargeFreighterCol]);
                valueDict[_DesignUpgradePassengerShipCol] = ConvertBool(valueDict[_DesignUpgradePassengerShipCol]);
                valueDict[_DesignUpgradeGasMiningShipCol] = ConvertBool(valueDict[_DesignUpgradeGasMiningShipCol]);
                valueDict[_DesignUpgradeMiningShipCol] = ConvertBool(valueDict[_DesignUpgradeMiningShipCol]);
                valueDict[_DesignUpgradeGasMiningStationCol] = ConvertBool(valueDict[_DesignUpgradeGasMiningStationCol]);
                valueDict[_DesignUpgradeMiningStationCol] = ConvertBool(valueDict[_DesignUpgradeMiningStationCol]);

                // Additional game settings
                valueDict[_TradeWithOtherEmpiresCol] = ConvertBool(valueDict[_TradeWithOtherEmpiresCol]);
                valueDict[_EngageInTourismCol] = ConvertBool(valueDict[_EngageInTourismCol]);
                valueDict[_ImplementEnslavementWithPenalColoniesCol] = ConvertBool(valueDict[_ImplementEnslavementWithPenalColoniesCol]);
                valueDict[_ProtectLeaderAtAllCostsCol] = ConvertBool(valueDict[_ProtectLeaderAtAllCostsCol]);
                valueDict[_UseExplorationShipsToScoutEnemySystemsCol] = ConvertBool(valueDict[_UseExplorationShipsToScoutEnemySystemsCol]);
                valueDict[_BuildPlanetDestroyersCol] = ConvertBool(valueDict[_BuildPlanetDestroyersCol]);
                valueDict[_TroopUseDefaultTransportLoadoutCol] = ConvertBool(valueDict[_TroopUseDefaultTransportLoadoutCol]);
                valueDict[_UpgradeEnlistedCivilianShipsCol] = ConvertBool(valueDict[_UpgradeEnlistedCivilianShipsCol]);
                valueDict[_UpgradeEnlistedMilitaryShipsCol] = ConvertBool(valueDict[_UpgradeEnlistedMilitaryShipsCol]);
                valueDict[_AcceptPirateSmugglingMissionsCol] = ConvertBool(valueDict[_AcceptPirateSmugglingMissionsCol]);
                valueDict[_BidOnPirateAttackMissionsCol] = ConvertBool(valueDict[_BidOnPirateAttackMissionsCol]);
                valueDict[_BidOnPirateDefendMissionsCol] = ConvertBool(valueDict[_BidOnPirateDefendMissionsCol]);
            }
        }

        private string ConvertBool(string value)
        {
            string res = "";
            if (value.ToUpper() == "Y")
                res = "True";
            else if (value.ToUpper() == "N")
                res = "False";
            else if (string.IsNullOrWhiteSpace(value))
                res = "False";
            else
                throw new ApplicationException($"Unknown string, bool value expected: {value}");
            return res;
        }

        private void WriteXml(string filePath, string outputFolder, List<Dictionary<string, string>> values)
        {
            string xmlFilePath = Path.Combine(outputFolder, Path.ChangeExtension(filePath, ".xml"));
            using FileStream fileStream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            fileStream.SetLength(0);
            XDocument doc = new XDocument();
            var root = new XElement("root");
            doc.Add(root);
            for (int i = 0; i < values.Count; i++)
            {
                var component = new XElement("Component");
                if (convertType == ConvertType.Update)
                {
                    component.Value = $"UPDATE {_tableName} SET {_ImmediatelyRecruitNewTroopsWhenColonizeCol} = {values[i][_ImmediatelyRecruitNewTroopsWhenColonizeCol]}, {_ColonyAllowFacilityCloningFacilityCol} = {values[i][_ColonyAllowFacilityCloningFacilityCol]}, {_ColonyAllowFacilityFortifiedBunkerCol} = {values[i][_ColonyAllowFacilityFortifiedBunkerCol]}, {_ColonyAllowFacilityGiantIonCannonCol} = {values[i][_ColonyAllowFacilityGiantIonCannonCol]}, {_ColonyAllowFacilityPlanetaryShieldCol} = {values[i][_ColonyAllowFacilityPlanetaryShieldCol]}, {_ColonyAllowFacilityRegionalCapitalCol} = {values[i][_ColonyAllowFacilityRegionalCapitalCol]}, {_ColonyAllowFacilityRoboticTroopFoundryCol} = {values[i][_ColonyAllowFacilityRoboticTroopFoundryCol]}, {_ColonyAllowFacilityTerraformingFacilityCol} = {values[i][_ColonyAllowFacilityTerraformingFacilityCol]}, {_ColonyAllowFacilityTroopTrainingCenterCol} = {values[i][_ColonyAllowFacilityTroopTrainingCenterCol]}, {_ColonyAllowFacilityArmoredFactoryCol} = {values[i][_ColonyAllowFacilityArmoredFactoryCol]}, {_ColonyAllowFacilitySpyAcademyCol} = {values[i][_ColonyAllowFacilitySpyAcademyCol]}, {_ColonyAllowFacilityScienceAcademyCol} = {values[i][_ColonyAllowFacilityScienceAcademyCol]}, {_ColonyAllowFacilityNavalAcademyCol} = {values[i][_ColonyAllowFacilityNavalAcademyCol]}, {_ColonyAllowFacilityMilitaryAcademyCol} = {values[i][_ColonyAllowFacilityMilitaryAcademyCol]}, {_ColonyFacilityPopulationThresholdCloningFacilityCol} = {values[i][_ColonyFacilityPopulationThresholdCloningFacilityCol]}, {_ColonyFacilityPopulationThresholdFortifiedBunkerCol} = {values[i][_ColonyFacilityPopulationThresholdFortifiedBunkerCol]}, {_ColonyFacilityPopulationThresholdGiantIonCannonCol} = {values[i][_ColonyFacilityPopulationThresholdGiantIonCannonCol]}, {_ColonyFacilityPopulationThresholdPlanetaryShieldCol} = {values[i][_ColonyFacilityPopulationThresholdPlanetaryShieldCol]}, {_ColonyFacilityPopulationThresholdRegionalCapitalCol} = {values[i][_ColonyFacilityPopulationThresholdRegionalCapitalCol]}, {_ColonyFacilityPopulationThresholdRoboticTroopFoundryCol} = {values[i][_ColonyFacilityPopulationThresholdRoboticTroopFoundryCol]}, {_ColonyFacilityPopulationThresholdTerraformingFacilityCol} = {values[i][_ColonyFacilityPopulationThresholdTerraformingFacilityCol]}, {_ColonyFacilityPopulationThresholdTroopTrainingCenterCol} = {values[i][_ColonyFacilityPopulationThresholdTroopTrainingCenterCol]}, {_ColonyFacilityPopulationThresholdArmoredFactoryCol} = {values[i][_ColonyFacilityPopulationThresholdArmoredFactoryCol]}, {_ColonyFacilityPopulationThresholdSpyAcademyCol} = {values[i][_ColonyFacilityPopulationThresholdSpyAcademyCol]}, {_ColonyFacilityPopulationThresholdScienceAcademyCol} = {values[i][_ColonyFacilityPopulationThresholdScienceAcademyCol]}, {_ColonyFacilityPopulationThresholdNavalAcademyCol} = {values[i][_ColonyFacilityPopulationThresholdNavalAcademyCol]}, {_ColonyFacilityPopulationThresholdMilitaryAcademyCol} = {values[i][_ColonyFacilityPopulationThresholdMilitaryAcademyCol]}, {_ColonyPopulationThresholdTroopRecruitmentCol} = {values[i][_ColonyPopulationThresholdTroopRecruitmentCol]}, {_ColonyTaxRateIncreaseWhenAtWarCol} = {values[i][_ColonyTaxRateIncreaseWhenAtWarCol]}, {_ColonyTaxRateLargeColonyCol} = {values[i][_ColonyTaxRateLargeColonyCol]}, {_ColonyTaxRateMediumColonyCol} = {values[i][_ColonyTaxRateMediumColonyCol]}, {_ColonyTaxRateSmallColonyCol} = {values[i][_ColonyTaxRateSmallColonyCol]}, {_MilitaryConstructionLevelCol} = {values[i][_MilitaryConstructionLevelCol]}, {_ConstructionMilitaryCapitalShipCol} = {values[i][_ConstructionMilitaryCapitalShipCol]}, {_ConstructionMilitaryCarrierCol} = {values[i][_ConstructionMilitaryCarrierCol]}, {_ConstructionMilitaryCruiserCol} = {values[i][_ConstructionMilitaryCruiserCol]}, {_ConstructionMilitaryDestroyerCol} = {values[i][_ConstructionMilitaryDestroyerCol]}, {_ConstructionMilitaryEscortCol} = {values[i][_ConstructionMilitaryEscortCol]}, {_ConstructionMilitaryFrigateCol} = {values[i][_ConstructionMilitaryFrigateCol]}, {_ConstructionMilitaryTroopTransportCol} = {values[i][_ConstructionMilitaryTroopTransportCol]}, {_ConstructionSpaceportLargeColonyPopulationThresholdCol} = {values[i][_ConstructionSpaceportLargeColonyPopulationThresholdCol]}, {_ConstructionSpaceportMediumColonyPopulationThresholdCol} = {values[i][_ConstructionSpaceportMediumColonyPopulationThresholdCol]}, {_ConstructionSpaceportSmallColonyPopulationThresholdCol} = {values[i][_ConstructionSpaceportSmallColonyPopulationThresholdCol]}, {_ConstructionSpaceportMinimumDistanceCol} = {values[i][_ConstructionSpaceportMinimumDistanceCol]}, {_DiplomacySendGiftsUpToAmountCol} = {values[i][_DiplomacySendGiftsUpToAmountCol]}, {_DiplomacyTradeSanctionsUseBlockadesCol} = {values[i][_DiplomacyTradeSanctionsUseBlockadesCol]}, {_FleetMilitaryProportionForFleetsCol} = {values[i][_FleetMilitaryProportionForFleetsCol]}, {_FleetStrikeForceTypicalSizeCol} = {values[i][_FleetStrikeForceTypicalSizeCol]}, {_FleetTypicalSizeCol} = {values[i][_FleetTypicalSizeCol]}, {_IntelligenceAllowMissionDeepCoverCol} = {values[i][_IntelligenceAllowMissionDeepCoverCol]}, {_IntelligenceAllowMissionInciteRevolutionCol} = {values[i][_IntelligenceAllowMissionInciteRevolutionCol]}, {_IntelligenceAllowMissionSabotageColonyCol} = {values[i][_IntelligenceAllowMissionSabotageColonyCol]}, {_IntelligenceAllowMissionSabotageConstructionCol} = {values[i][_IntelligenceAllowMissionSabotageConstructionCol]}, {_IntelligenceAllowMissionStealGalaxyMapCol} = {values[i][_IntelligenceAllowMissionStealGalaxyMapCol]}, {_IntelligenceAllowMissionStealOperationsMapCol} = {values[i][_IntelligenceAllowMissionStealOperationsMapCol]}, {_IntelligenceAllowMissionStealTechDataCol} = {values[i][_IntelligenceAllowMissionStealTechDataCol]}, {_IntelligenceAllowMissionStealTerritoryMapCol} = {values[i][_IntelligenceAllowMissionStealTerritoryMapCol]}, {_IntelligenceAllowMissionAssassinateCharacterCol} = {values[i][_IntelligenceAllowMissionAssassinateCharacterCol]}, {_IntelligenceAllowMissionDestroyBaseCol} = {values[i][_IntelligenceAllowMissionDestroyBaseCol]}, {_IntelligenceCounterIntelligenceProportionCol} = {values[i][_IntelligenceCounterIntelligenceProportionCol]}, {_IntelligenceUseEspionageAgainstEmpireWhenCol} = {values[i][_IntelligenceUseEspionageAgainstEmpireWhenCol]}, {_IntelligenceUseSabotageAgainstEmpireWhenCol} = {values[i][_IntelligenceUseSabotageAgainstEmpireWhenCol]}, {_ResearchDesignAutoRetrofitCol} = {values[i][_ResearchDesignAutoRetrofitCol]}, {_ResearchDesignOverallFocusCol} = {values[i][_ResearchDesignOverallFocusCol]}, {_ResearchDesignTechFocus1Col} = {values[i][_ResearchDesignTechFocus1Col]}, {_ResearchDesignTechFocus2Col} = {values[i][_ResearchDesignTechFocus2Col]}, {_ResearchDesignTechFocus3Col} = {values[i][_ResearchDesignTechFocus3Col]}, {_ResearchDesignTechFocus4Col} = {values[i][_ResearchDesignTechFocus4Col]}, {_ResearchDesignTechFocus5Col} = {values[i][_ResearchDesignTechFocus5Col]}, {_ResearchDesignTechFocus6Col} = {values[i][_ResearchDesignTechFocus6Col]}, {_ResearchDesignAutoUpgradeFightersCol} = {values[i][_ResearchDesignAutoUpgradeFightersCol]}, {_WarAttacksAllowColonyBombardmentCol} = {values[i][_WarAttacksAllowColonyBombardmentCol]}, {_WarAttacksAllowPlanetDestroyingCol} = {values[i][_WarAttacksAllowPlanetDestroyingCol]}, {_WarAttacksHarassEnemiesCol} = {values[i][_WarAttacksHarassEnemiesCol]}, {_TradeWithOtherEmpiresCol} = {values[i][_TradeWithOtherEmpiresCol]}, {_EngageInTourismCol} = {values[i][_EngageInTourismCol]}, {_NewColonyPopulationPolicyYourRaceFamilyCol} = {values[i][_NewColonyPopulationPolicyYourRaceFamilyCol]}, {_NewColonyPopulationPolicyAllRacesCol} = {values[i][_NewColonyPopulationPolicyAllRacesCol]}, {_ImplementEnslavementWithPenalColoniesCol} = {values[i][_ImplementEnslavementWithPenalColoniesCol]}, {_HomeworldDefensePriorityCol} = {values[i][_HomeworldDefensePriorityCol]}, {_ProtectLeaderAtAllCostsCol} = {values[i][_ProtectLeaderAtAllCostsCol]}, {_PrioritizeBuildWonderIdCol} = {values[i][_PrioritizeBuildWonderIdCol]}, {_ColonizeContinentalPriorityCol} = {values[i][_ColonizeContinentalPriorityCol]}, {_ColonizeMarshySwampPriorityCol} = {values[i][_ColonizeMarshySwampPriorityCol]}, {_ColonizeOceanPriorityCol} = {values[i][_ColonizeOceanPriorityCol]}, {_ColonizeDesertPriorityCol} = {values[i][_ColonizeDesertPriorityCol]}, {_ColonizeIcePriorityCol} = {values[i][_ColonizeIcePriorityCol]}, {_ColonizeVolcanicPriorityCol} = {values[i][_ColonizeVolcanicPriorityCol]}, {_ColonizeRuinsPriorityCol} = {values[i][_ColonizeRuinsPriorityCol]}, {_ControlRestrictedResourcesPriorityCol} = {values[i][_ControlRestrictedResourcesPriorityCol]}, {_ResearchIndustryFocusCol} = {values[i][_ResearchIndustryFocusCol]}, {_ResearchPriorityCol} = {values[i][_ResearchPriorityCol]}, {_TradePriorityCol} = {values[i][_TradePriorityCol]}, {_AlliancePriorityCol} = {values[i][_AlliancePriorityCol]}, {_SubjugationPriorityCol} = {values[i][_SubjugationPriorityCol]}, {_TourismPriorityCol} = {values[i][_TourismPriorityCol]}, {_ExplorationPriorityCol} = {values[i][_ExplorationPriorityCol]}, {_WarWillingnessCol} = {values[i][_WarWillingnessCol]}, {_BreakTreatyWillingnessCol} = {values[i][_BreakTreatyWillingnessCol]}, {_InvasionOverkillFactorCol} = {values[i][_InvasionOverkillFactorCol]}, {_ShipBattleCautionFactorCol} = {values[i][_ShipBattleCautionFactorCol]}, {_DefaultMilitaryFleeWhenCol} = {values[i][_DefaultMilitaryFleeWhenCol]}, {_DesignUpgradeEscortCol} = {values[i][_DesignUpgradeEscortCol]}, {_DesignUpgradeFrigateCol} = {values[i][_DesignUpgradeFrigateCol]}, {_DesignUpgradeDestroyerCol} = {values[i][_DesignUpgradeDestroyerCol]}, {_DesignUpgradeCruiserCol} = {values[i][_DesignUpgradeCruiserCol]}, {_DesignUpgradeCapitalShipCol} = {values[i][_DesignUpgradeCapitalShipCol]}, {_DesignUpgradeTroopTransportCol} = {values[i][_DesignUpgradeTroopTransportCol]}, {_DesignUpgradeCarrierCol} = {values[i][_DesignUpgradeCarrierCol]}, {_DesignUpgradeResupplyShipCol} = {values[i][_DesignUpgradeResupplyShipCol]}, {_DesignUpgradeExplorationShipCol} = {values[i][_DesignUpgradeExplorationShipCol]}, {_DesignUpgradeColonyShipCol} = {values[i][_DesignUpgradeColonyShipCol]}, {_DesignUpgradeConstructionShipCol} = {values[i][_DesignUpgradeConstructionShipCol]}, {_DesignUpgradeSmallSpacePortCol} = {values[i][_DesignUpgradeSmallSpacePortCol]}, {_DesignUpgradeMediumSpacePortCol} = {values[i][_DesignUpgradeMediumSpacePortCol]}, {_DesignUpgradeLargeSpacePortCol} = {values[i][_DesignUpgradeLargeSpacePortCol]}, {_DesignUpgradeResortBaseCol} = {values[i][_DesignUpgradeResortBaseCol]}, {_DesignUpgradeGenericBaseCol} = {values[i][_DesignUpgradeGenericBaseCol]}, {_DesignUpgradeEnergyResearchStationCol} = {values[i][_DesignUpgradeEnergyResearchStationCol]}, {_DesignUpgradeWeaponsResearchStationCol} = {values[i][_DesignUpgradeWeaponsResearchStationCol]}, {_DesignUpgradeHighTechResearchStationCol} = {values[i][_DesignUpgradeHighTechResearchStationCol]}, {_DesignUpgradeMonitoringStationCol} = {values[i][_DesignUpgradeMonitoringStationCol]}, {_DesignUpgradeDefensiveBaseCol} = {values[i][_DesignUpgradeDefensiveBaseCol]}, {_DesignUpgradeSmallFreighterCol} = {values[i][_DesignUpgradeSmallFreighterCol]}, {_DesignUpgradeMediumFreighterCol} = {values[i][_DesignUpgradeMediumFreighterCol]}, {_DesignUpgradeLargeFreighterCol} = {values[i][_DesignUpgradeLargeFreighterCol]}, {_DesignUpgradePassengerShipCol} = {values[i][_DesignUpgradePassengerShipCol]}, {_DesignUpgradeGasMiningShipCol} = {values[i][_DesignUpgradeGasMiningShipCol]}, {_DesignUpgradeMiningShipCol} = {values[i][_DesignUpgradeMiningShipCol]}, {_DesignUpgradeGasMiningStationCol} = {values[i][_DesignUpgradeGasMiningStationCol]}, {_DesignUpgradeMiningStationCol} = {values[i][_DesignUpgradeMiningStationCol]}, {_CaptureTargetConditionShipCol} = {values[i][_CaptureTargetConditionShipCol]}, {_CaptureTargetConditionBaseCol} = {values[i][_CaptureTargetConditionBaseCol]}, {_OfferPirateAttackMissionsCol} = {values[i][_OfferPirateAttackMissionsCol]}, {_BidOnPirateAttackMissionsCol} = {values[i][_BidOnPirateAttackMissionsCol]}, {_BidOnPirateDefendMissionsCol} = {values[i][_BidOnPirateDefendMissionsCol]}, {_OfferDefensivePirateMissionsCol} = {values[i][_OfferDefensivePirateMissionsCol]}, {_OfferDefensivePirateMissionsSituationCol} = {values[i][_OfferDefensivePirateMissionsSituationCol]}, {_AcceptPirateSmugglingMissionsCol} = {values[i][_AcceptPirateSmugglingMissionsCol]}, {_OfferSmugglingPirateMissionsCol} = {values[i][_OfferSmugglingPirateMissionsCol]}, {_PirateSmugglerFreighterLevelCol} = {values[i][_PirateSmugglerFreighterLevelCol]}, {_PirateSmugglerMiningLevelCol} = {values[i][_PirateSmugglerMiningLevelCol]}, {_PirateSmugglerPassengerLevelCol} = {values[i][_PirateSmugglerPassengerLevelCol]}, {_CaptureEnlistMilitaryShipCol} = {values[i][_CaptureEnlistMilitaryShipCol]}, {_CaptureDisassembleMilitaryShipCol} = {values[i][_CaptureDisassembleMilitaryShipCol]}, {_CaptureEnlistCivilianShipCol} = {values[i][_CaptureEnlistCivilianShipCol]}, {_CaptureDisassembleCivilianShipCol} = {values[i][_CaptureDisassembleCivilianShipCol]}, {_CaptureEnlistBaseCol} = {values[i][_CaptureEnlistBaseCol]}, {_UpgradeEnlistedMilitaryShipsCol} = {values[i][_UpgradeEnlistedMilitaryShipsCol]}, {_UpgradeEnlistedCivilianShipsCol} = {values[i][_UpgradeEnlistedCivilianShipsCol]}, {_TroopRecruitInfantryLevelCol} = {values[i][_TroopRecruitInfantryLevelCol]}, {_TroopRecruitArmorLevelCol} = {values[i][_TroopRecruitArmorLevelCol]}, {_TroopRecruitArtilleryLevelCol} = {values[i][_TroopRecruitArtilleryLevelCol]}, {_TroopRecruitSpecialForcesLevelCol} = {values[i][_TroopRecruitSpecialForcesLevelCol]}, {_TroopUseDefaultTransportLoadoutCol} = {_TroopUseDefaultTransportLoadoutCol}, {_TroopDefaultTransportLoadoutInfantryCol} = {values[i][_TroopDefaultTransportLoadoutInfantryCol]}, {_TroopDefaultTransportLoadoutArmorCol} = {values[i][_TroopDefaultTransportLoadoutArmorCol]}, {_TroopDefaultTransportLoadoutArtilleryCol} = {values[i][_TroopDefaultTransportLoadoutArtilleryCol]}, {_TroopDefaultTransportLoadoutSpecialForcesCol} = {values[i][_TroopDefaultTransportLoadoutSpecialForcesCol]}, {_TroopGarrisonMinimumPerColonyCol}={values[i][_TroopGarrisonMinimumPerColonyCol]}, {_TroopGarrisonLevelCol} = {values[i][_TroopGarrisonLevelCol]}, {_UseExplorationShipsToScoutEnemySystemsCol}= {values[i][_UseExplorationShipsToScoutEnemySystemsCol]}, {_BuildPlanetDestroyersCol}= {values[i][_BuildPlanetDestroyersCol]} WHERE {_EmpireNameCol} = '{values[i][_EmpireNameCol]}'";
                }
                else
                {
                    component.Value = $"INSERT INTO {_tableName} ({_ID}, {_EmpireNameCol},{_ImmediatelyRecruitNewTroopsWhenColonizeCol}, {_ColonyAllowFacilityCloningFacilityCol}, {_ColonyAllowFacilityFortifiedBunkerCol}, {_ColonyAllowFacilityGiantIonCannonCol}, {_ColonyAllowFacilityPlanetaryShieldCol}, {_ColonyAllowFacilityRegionalCapitalCol}, {_ColonyAllowFacilityRoboticTroopFoundryCol}, {_ColonyAllowFacilityTerraformingFacilityCol}, {_ColonyAllowFacilityTroopTrainingCenterCol}, {_ColonyAllowFacilityArmoredFactoryCol}, {_ColonyAllowFacilitySpyAcademyCol}, {_ColonyAllowFacilityScienceAcademyCol}, {_ColonyAllowFacilityNavalAcademyCol}, {_ColonyAllowFacilityMilitaryAcademyCol}, {_ColonyFacilityPopulationThresholdCloningFacilityCol}, {_ColonyFacilityPopulationThresholdFortifiedBunkerCol}, {_ColonyFacilityPopulationThresholdGiantIonCannonCol}, {_ColonyFacilityPopulationThresholdPlanetaryShieldCol}, {_ColonyFacilityPopulationThresholdRegionalCapitalCol}, {_ColonyFacilityPopulationThresholdRoboticTroopFoundryCol}, {_ColonyFacilityPopulationThresholdTerraformingFacilityCol}, {_ColonyFacilityPopulationThresholdTroopTrainingCenterCol}, {_ColonyFacilityPopulationThresholdArmoredFactoryCol}, {_ColonyFacilityPopulationThresholdSpyAcademyCol}, {_ColonyFacilityPopulationThresholdScienceAcademyCol}, {_ColonyFacilityPopulationThresholdNavalAcademyCol}, {_ColonyFacilityPopulationThresholdMilitaryAcademyCol}, {_ColonyPopulationThresholdTroopRecruitmentCol}, {_ColonyTaxRateIncreaseWhenAtWarCol}, {_ColonyTaxRateLargeColonyCol}, {_ColonyTaxRateMediumColonyCol}, {_ColonyTaxRateSmallColonyCol}, {_MilitaryConstructionLevelCol}, {_ConstructionMilitaryCapitalShipCol}, {_ConstructionMilitaryCarrierCol}, {_ConstructionMilitaryCruiserCol}, {_ConstructionMilitaryDestroyerCol}, {_ConstructionMilitaryEscortCol}, {_ConstructionMilitaryFrigateCol}, {_ConstructionMilitaryTroopTransportCol}, {_ConstructionSpaceportLargeColonyPopulationThresholdCol}, {_ConstructionSpaceportMediumColonyPopulationThresholdCol}, {_ConstructionSpaceportSmallColonyPopulationThresholdCol}, {_ConstructionSpaceportMinimumDistanceCol}, {_DiplomacySendGiftsUpToAmountCol}, {_DiplomacyTradeSanctionsUseBlockadesCol}, {_FleetMilitaryProportionForFleetsCol}, {_FleetStrikeForceTypicalSizeCol}, {_FleetTypicalSizeCol}, {_IntelligenceAllowMissionDeepCoverCol}, {_IntelligenceAllowMissionInciteRevolutionCol}, {_IntelligenceAllowMissionSabotageColonyCol}, {_IntelligenceAllowMissionSabotageConstructionCol}, {_IntelligenceAllowMissionStealGalaxyMapCol}, {_IntelligenceAllowMissionStealOperationsMapCol}, {_IntelligenceAllowMissionStealTechDataCol}, {_IntelligenceAllowMissionStealTerritoryMapCol}, {_IntelligenceAllowMissionAssassinateCharacterCol}, {_IntelligenceAllowMissionDestroyBaseCol}, {_IntelligenceCounterIntelligenceProportionCol}, {_IntelligenceUseEspionageAgainstEmpireWhenCol}, {_IntelligenceUseSabotageAgainstEmpireWhenCol}, {_ResearchDesignAutoRetrofitCol}, {_ResearchDesignOverallFocusCol}, {_ResearchDesignTechFocus1Col}, {_ResearchDesignTechFocus2Col}, {_ResearchDesignTechFocus3Col}, {_ResearchDesignTechFocus4Col}, {_ResearchDesignTechFocus5Col}, {_ResearchDesignTechFocus6Col}, {_ResearchDesignAutoUpgradeFightersCol}, {_WarAttacksAllowColonyBombardmentCol}, {_WarAttacksAllowPlanetDestroyingCol}, {_WarAttacksHarassEnemiesCol}, {_TradeWithOtherEmpiresCol}, {_EngageInTourismCol}, {_NewColonyPopulationPolicyYourRaceFamilyCol}, {_NewColonyPopulationPolicyAllRacesCol}, {_ImplementEnslavementWithPenalColoniesCol}, {_HomeworldDefensePriorityCol}, {_ProtectLeaderAtAllCostsCol}, {_PrioritizeBuildWonderIdCol}, {_ColonizeContinentalPriorityCol}, {_ColonizeMarshySwampPriorityCol}, {_ColonizeOceanPriorityCol}, {_ColonizeDesertPriorityCol}, {_ColonizeIcePriorityCol}, {_ColonizeVolcanicPriorityCol}, {_ColonizeRuinsPriorityCol}, {_ControlRestrictedResourcesPriorityCol}, {_ResearchIndustryFocusCol}, {_ResearchPriorityCol}, {_TradePriorityCol}, {_AlliancePriorityCol}, {_SubjugationPriorityCol}, {_TourismPriorityCol}, {_ExplorationPriorityCol}, {_WarWillingnessCol}, {_BreakTreatyWillingnessCol}, {_InvasionOverkillFactorCol}, {_ShipBattleCautionFactorCol}, {_DefaultMilitaryFleeWhenCol}, {_DesignUpgradeEscortCol}, {_DesignUpgradeFrigateCol}, {_DesignUpgradeDestroyerCol}, {_DesignUpgradeCruiserCol}, {_DesignUpgradeCapitalShipCol}, {_DesignUpgradeTroopTransportCol}, {_DesignUpgradeCarrierCol}, {_DesignUpgradeResupplyShipCol}, {_DesignUpgradeExplorationShipCol}, {_DesignUpgradeColonyShipCol}, {_DesignUpgradeConstructionShipCol}, {_DesignUpgradeSmallSpacePortCol}, {_DesignUpgradeMediumSpacePortCol}, {_DesignUpgradeLargeSpacePortCol}, {_DesignUpgradeResortBaseCol}, {_DesignUpgradeGenericBaseCol}, {_DesignUpgradeEnergyResearchStationCol}, {_DesignUpgradeWeaponsResearchStationCol}, {_DesignUpgradeHighTechResearchStationCol}, {_DesignUpgradeMonitoringStationCol}, {_DesignUpgradeDefensiveBaseCol}, {_DesignUpgradeSmallFreighterCol}, {_DesignUpgradeMediumFreighterCol}, {_DesignUpgradeLargeFreighterCol}, {_DesignUpgradePassengerShipCol}, {_DesignUpgradeGasMiningShipCol}, {_DesignUpgradeMiningShipCol}, {_DesignUpgradeGasMiningStationCol}, {_DesignUpgradeMiningStationCol}, {_CaptureTargetConditionShipCol}, {_CaptureTargetConditionBaseCol}, {_OfferPirateAttackMissionsCol}, {_BidOnPirateAttackMissionsCol}, {_BidOnPirateDefendMissionsCol}, {_OfferDefensivePirateMissionsCol}, {_OfferDefensivePirateMissionsSituationCol}, {_AcceptPirateSmugglingMissionsCol}, {_OfferSmugglingPirateMissionsCol}, {_PirateSmugglerFreighterLevelCol}, {_PirateSmugglerMiningLevelCol}, {_PirateSmugglerPassengerLevelCol}, {_CaptureEnlistMilitaryShipCol}, {_CaptureDisassembleMilitaryShipCol}, {_CaptureEnlistCivilianShipCol}, {_CaptureDisassembleCivilianShipCol}, {_CaptureEnlistBaseCol}, {_UpgradeEnlistedMilitaryShipsCol}, {_UpgradeEnlistedCivilianShipsCol}, {_TroopRecruitInfantryLevelCol}, {_TroopRecruitArmorLevelCol}, {_TroopRecruitArtilleryLevelCol}, {_TroopRecruitSpecialForcesLevelCol}, {_TroopUseDefaultTransportLoadoutCol}, {_TroopDefaultTransportLoadoutInfantryCol}, {_TroopDefaultTransportLoadoutArmorCol}, {_TroopDefaultTransportLoadoutArtilleryCol}, {_TroopDefaultTransportLoadoutSpecialForcesCol}, {_TroopGarrisonMinimumPerColonyCol}, {_TroopGarrisonLevelCol}, {_UseExplorationShipsToScoutEnemySystemsCol}, {_BuildPlanetDestroyersCol}) VALUES ({values[i][_ID]}, '{values[i][_EmpireNameCol]}',{values[i][_ImmediatelyRecruitNewTroopsWhenColonizeCol]}, {values[i][_ColonyAllowFacilityCloningFacilityCol]}, {values[i][_ColonyAllowFacilityFortifiedBunkerCol]}, {values[i][_ColonyAllowFacilityGiantIonCannonCol]}, {values[i][_ColonyAllowFacilityPlanetaryShieldCol]}, {values[i][_ColonyAllowFacilityRegionalCapitalCol]}, {values[i][_ColonyAllowFacilityRoboticTroopFoundryCol]}, {values[i][_ColonyAllowFacilityTerraformingFacilityCol]}, {values[i][_ColonyAllowFacilityTroopTrainingCenterCol]}, {values[i][_ColonyAllowFacilityArmoredFactoryCol]}, {values[i][_ColonyAllowFacilitySpyAcademyCol]}, {values[i][_ColonyAllowFacilityScienceAcademyCol]}, {values[i][_ColonyAllowFacilityNavalAcademyCol]}, {values[i][_ColonyAllowFacilityMilitaryAcademyCol]}, {values[i][_ColonyFacilityPopulationThresholdCloningFacilityCol]}, {values[i][_ColonyFacilityPopulationThresholdFortifiedBunkerCol]}, {values[i][_ColonyFacilityPopulationThresholdGiantIonCannonCol]}, {values[i][_ColonyFacilityPopulationThresholdPlanetaryShieldCol]}, {values[i][_ColonyFacilityPopulationThresholdRegionalCapitalCol]}, {values[i][_ColonyFacilityPopulationThresholdRoboticTroopFoundryCol]}, {values[i][_ColonyFacilityPopulationThresholdTerraformingFacilityCol]}, {values[i][_ColonyFacilityPopulationThresholdTroopTrainingCenterCol]}, {values[i][_ColonyFacilityPopulationThresholdArmoredFactoryCol]}, {values[i][_ColonyFacilityPopulationThresholdSpyAcademyCol]}, {values[i][_ColonyFacilityPopulationThresholdScienceAcademyCol]}, {values[i][_ColonyFacilityPopulationThresholdNavalAcademyCol]}, {values[i][_ColonyFacilityPopulationThresholdMilitaryAcademyCol]}, {values[i][_ColonyPopulationThresholdTroopRecruitmentCol]}, {values[i][_ColonyTaxRateIncreaseWhenAtWarCol]}, {values[i][_ColonyTaxRateLargeColonyCol]}, {values[i][_ColonyTaxRateMediumColonyCol]}, {values[i][_ColonyTaxRateSmallColonyCol]}, {values[i][_MilitaryConstructionLevelCol]}, {values[i][_ConstructionMilitaryCapitalShipCol]}, {values[i][_ConstructionMilitaryCarrierCol]}, {values[i][_ConstructionMilitaryCruiserCol]}, {values[i][_ConstructionMilitaryDestroyerCol]}, {values[i][_ConstructionMilitaryEscortCol]}, {values[i][_ConstructionMilitaryFrigateCol]}, {values[i][_ConstructionMilitaryTroopTransportCol]}, {values[i][_ConstructionSpaceportLargeColonyPopulationThresholdCol]}, {values[i][_ConstructionSpaceportMediumColonyPopulationThresholdCol]}, {values[i][_ConstructionSpaceportSmallColonyPopulationThresholdCol]}, {values[i][_ConstructionSpaceportMinimumDistanceCol]}, {values[i][_DiplomacySendGiftsUpToAmountCol]}, {values[i][_DiplomacyTradeSanctionsUseBlockadesCol]}, {values[i][_FleetMilitaryProportionForFleetsCol]}, {values[i][_FleetStrikeForceTypicalSizeCol]}, {values[i][_FleetTypicalSizeCol]}, {values[i][_IntelligenceAllowMissionDeepCoverCol]}, {values[i][_IntelligenceAllowMissionInciteRevolutionCol]}, {values[i][_IntelligenceAllowMissionSabotageColonyCol]}, {values[i][_IntelligenceAllowMissionSabotageConstructionCol]}, {values[i][_IntelligenceAllowMissionStealGalaxyMapCol]}, {values[i][_IntelligenceAllowMissionStealOperationsMapCol]}, {values[i][_IntelligenceAllowMissionStealTechDataCol]}, {values[i][_IntelligenceAllowMissionStealTerritoryMapCol]}, {values[i][_IntelligenceAllowMissionAssassinateCharacterCol]}, {values[i][_IntelligenceAllowMissionDestroyBaseCol]}, {values[i][_IntelligenceCounterIntelligenceProportionCol]}, {values[i][_IntelligenceUseEspionageAgainstEmpireWhenCol]}, {values[i][_IntelligenceUseSabotageAgainstEmpireWhenCol]}, {values[i][_ResearchDesignAutoRetrofitCol]}, {values[i][_ResearchDesignOverallFocusCol]}, {values[i][_ResearchDesignTechFocus1Col]}, {values[i][_ResearchDesignTechFocus2Col]}, {values[i][_ResearchDesignTechFocus3Col]}, {values[i][_ResearchDesignTechFocus4Col]}, {values[i][_ResearchDesignTechFocus5Col]}, {values[i][_ResearchDesignTechFocus6Col]}, {values[i][_ResearchDesignAutoUpgradeFightersCol]}, {values[i][_WarAttacksAllowColonyBombardmentCol]}, {values[i][_WarAttacksAllowPlanetDestroyingCol]}, {values[i][_WarAttacksHarassEnemiesCol]}, {values[i][_TradeWithOtherEmpiresCol]}, {values[i][_EngageInTourismCol]}, {values[i][_NewColonyPopulationPolicyYourRaceFamilyCol]}, {values[i][_NewColonyPopulationPolicyAllRacesCol]}, {values[i][_ImplementEnslavementWithPenalColoniesCol]}, {values[i][_HomeworldDefensePriorityCol]}, {values[i][_ProtectLeaderAtAllCostsCol]}, {values[i][_PrioritizeBuildWonderIdCol]}, {values[i][_ColonizeContinentalPriorityCol]}, {values[i][_ColonizeMarshySwampPriorityCol]}, {values[i][_ColonizeOceanPriorityCol]}, {values[i][_ColonizeDesertPriorityCol]}, {values[i][_ColonizeIcePriorityCol]}, {values[i][_ColonizeVolcanicPriorityCol]}, {values[i][_ColonizeRuinsPriorityCol]}, {values[i][_ControlRestrictedResourcesPriorityCol]}, {values[i][_ResearchIndustryFocusCol]}, {values[i][_ResearchPriorityCol]}, {values[i][_TradePriorityCol]}, {values[i][_AlliancePriorityCol]}, {values[i][_SubjugationPriorityCol]}, {values[i][_TourismPriorityCol]}, {values[i][_ExplorationPriorityCol]}, {values[i][_WarWillingnessCol]}, {values[i][_BreakTreatyWillingnessCol]}, {values[i][_InvasionOverkillFactorCol]}, {values[i][_ShipBattleCautionFactorCol]}, {values[i][_DefaultMilitaryFleeWhenCol]}, {values[i][_DesignUpgradeEscortCol]}, {values[i][_DesignUpgradeFrigateCol]}, {values[i][_DesignUpgradeDestroyerCol]}, {values[i][_DesignUpgradeCruiserCol]}, {values[i][_DesignUpgradeCapitalShipCol]}, {values[i][_DesignUpgradeTroopTransportCol]}, {values[i][_DesignUpgradeCarrierCol]}, {values[i][_DesignUpgradeResupplyShipCol]}, {values[i][_DesignUpgradeExplorationShipCol]}, {values[i][_DesignUpgradeColonyShipCol]}, {values[i][_DesignUpgradeConstructionShipCol]}, {values[i][_DesignUpgradeSmallSpacePortCol]}, {values[i][_DesignUpgradeMediumSpacePortCol]}, {values[i][_DesignUpgradeLargeSpacePortCol]}, {values[i][_DesignUpgradeResortBaseCol]}, {values[i][_DesignUpgradeGenericBaseCol]}, {values[i][_DesignUpgradeEnergyResearchStationCol]}, {values[i][_DesignUpgradeWeaponsResearchStationCol]}, {values[i][_DesignUpgradeHighTechResearchStationCol]}, {values[i][_DesignUpgradeMonitoringStationCol]}, {values[i][_DesignUpgradeDefensiveBaseCol]}, {values[i][_DesignUpgradeSmallFreighterCol]}, {values[i][_DesignUpgradeMediumFreighterCol]}, {values[i][_DesignUpgradeLargeFreighterCol]}, {values[i][_DesignUpgradePassengerShipCol]}, {values[i][_DesignUpgradeGasMiningShipCol]}, {values[i][_DesignUpgradeMiningShipCol]}, {values[i][_DesignUpgradeGasMiningStationCol]}, {values[i][_DesignUpgradeMiningStationCol]}, {values[i][_CaptureTargetConditionShipCol]}, {values[i][_CaptureTargetConditionBaseCol]}, {values[i][_OfferPirateAttackMissionsCol]}, {values[i][_BidOnPirateAttackMissionsCol]}, {values[i][_BidOnPirateDefendMissionsCol]}, {values[i][_OfferDefensivePirateMissionsCol]}, {values[i][_OfferDefensivePirateMissionsSituationCol]}, {values[i][_AcceptPirateSmugglingMissionsCol]}, {values[i][_OfferSmugglingPirateMissionsCol]}, {values[i][_PirateSmugglerFreighterLevelCol]}, {values[i][_PirateSmugglerMiningLevelCol]}, {values[i][_PirateSmugglerPassengerLevelCol]}, {values[i][_CaptureEnlistMilitaryShipCol]}, {values[i][_CaptureDisassembleMilitaryShipCol]}, {values[i][_CaptureEnlistCivilianShipCol]}, {values[i][_CaptureDisassembleCivilianShipCol]}, {values[i][_CaptureEnlistBaseCol]}, {values[i][_UpgradeEnlistedMilitaryShipsCol]}, {values[i][_UpgradeEnlistedCivilianShipsCol]}, {values[i][_TroopRecruitInfantryLevelCol]}, {values[i][_TroopRecruitArmorLevelCol]}, {values[i][_TroopRecruitArtilleryLevelCol]}, {values[i][_TroopRecruitSpecialForcesLevelCol]}, {values[i][_TroopUseDefaultTransportLoadoutCol]}, {values[i][_TroopDefaultTransportLoadoutInfantryCol]}, {values[i][_TroopDefaultTransportLoadoutArmorCol]}, {values[i][_TroopDefaultTransportLoadoutArtilleryCol]}, {values[i][_TroopDefaultTransportLoadoutSpecialForcesCol]}, {values[i][_TroopGarrisonMinimumPerColonyCol]}, {values[i][_TroopGarrisonLevelCol]}, {values[i][_UseExplorationShipsToScoutEnemySystemsCol]}, {values[i][_BuildPlanetDestroyersCol]})";


                }
                root.Add(component);
            }
            doc.Save(fileStream);
        }

        private string GetValidFileLine(StreamReader reader)
        {
            string text = string.Empty;
            while (!reader.EndOfStream && (string.IsNullOrEmpty(text) || text.Trim() == string.Empty || text.Trim().Substring(0, 1) == "'"))
            {
                text = reader.ReadLine()?.Trim();
            }
            return text;
        }
    }
}