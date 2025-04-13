// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpirePolicy
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
    [Serializable]
    public class EmpirePolicy
    {
        public float IntelligenceCounterIntelligenceProportion = 30f;
        public bool IntelligenceAllowMissionSabotageConstruction = true;
        public bool IntelligenceAllowMissionStealTerritoryMap = true;
        public bool IntelligenceAllowMissionStealGalaxyMap = true;
        public bool IntelligenceAllowMissionStealOperationsMap = true;
        public bool IntelligenceAllowMissionStealTechData = true;
        public bool IntelligenceAllowMissionSabotageColony = true;
        public bool IntelligenceAllowMissionDeepCover = true;
        public bool IntelligenceAllowMissionInciteRevolution = true;
        public bool IntelligenceAllowMissionAssassinateCharacter = true;
        public bool IntelligenceAllowMissionDestroyBase = true;
        public int IntelligenceUseEspionageAgainstEmpireWhen = 2;
        public int IntelligenceUseSabotageAgainstEmpireWhen = 1;
        public bool DiplomacyTradeSanctionsUseBlockades = true;
        public int DiplomacySendGiftsUpToAmount = 20000;
        [OptionalField]
        public bool ColonyActionForNewTroopRecruitment;
        public Design ColonyActionForNewBuildDesign;
        public bool ColonyAllowFacilityTroopTrainingCenter = true;
        public bool ColonyAllowFacilityRoboticTroopFoundry = true;
        public bool ColonyAllowFacilityCloningFacility = true;
        public bool ColonyAllowFacilityPlanetaryShield = true;
        public bool ColonyAllowFacilityGiantIonCannon = true;
        public bool ColonyAllowFacilityFortifiedBunker = true;
        public bool ColonyAllowFacilityRegionalCapital = true;
        public bool ColonyAllowFacilityTerraformingFacility = true;
        public bool ColonyAllowFacilityArmoredFactory = true;
        public bool ColonyAllowFacilityMilitaryAcademy = true;
        public bool ColonyAllowFacilitySpyAcademy = true;
        public bool ColonyAllowFacilityNavalAcademy = true;
        public bool ColonyAllowFacilityScienceAcademy = true;
        public int ColonyFacilityPopulationThresholdTroopTrainingCenter = 500;
        public int ColonyFacilityPopulationThresholdRoboticTroopFoundry = 500;
        public int ColonyFacilityPopulationThresholdCloningFacility = 500;
        public int ColonyFacilityPopulationThresholdPlanetaryShield = 2000;
        public int ColonyFacilityPopulationThresholdGiantIonCannon = 5000;
        public int ColonyFacilityPopulationThresholdFortifiedBunker = 500;
        public int ColonyFacilityPopulationThresholdRegionalCapital = 5000;
        public int ColonyFacilityPopulationThresholdTerraformingFacility = 500;
        public int ColonyFacilityPopulationThresholdArmoredFactory = 500;
        public int ColonyFacilityPopulationThresholdMilitaryAcademy = 2000;
        public int ColonyFacilityPopulationThresholdSpyAcademy = 2000;
        public int ColonyFacilityPopulationThresholdNavalAcademy = 5000;
        public int ColonyFacilityPopulationThresholdScienceAcademy = 5000;
        public int ColonyTaxRateSmallColony;
        public int ColonyTaxRateMediumColony = 2;
        public int ColonyTaxRateLargeColony = 3;
        public bool ColonyTaxRateIncreaseWhenAtWar = true;
        public int ColonyPopulationThresholdTroopRecruitment;
        public ShipDesignFocus ResearchDesignOverallFocus;
        public ComponentCategoryType ResearchDesignTechFocus1;
        public ComponentCategoryType ResearchDesignTechFocus2;
        public ComponentType ResearchDesignTechFocusType1;
        public ComponentType ResearchDesignTechFocusType2;
        public bool ResearchDesignAutoRetrofit = true;
        [OptionalField]
        public bool ResearchDesignAutoUpgradeFighters = true;
        public int ConstructionMilitary = 1;
        public float ConstructionMilitaryEscort = 18f;
        public float ConstructionMilitaryFrigate = 24f;
        public float ConstructionMilitaryDestroyer = 20f;
        public float ConstructionMilitaryCruiser = 15f;
        public float ConstructionMilitaryCapitalShip = 7f;
        public float ConstructionMilitaryTroopTransport = 8f;
        public float ConstructionMilitaryCarrier = 8f;
        public int ConstructionSpaceportMinimumDistance = 700;
        [OptionalField]
        public int ConstructionOutpostColonyPopulationThreshold = 1;
        public int ConstructionSpaceportSmallColonyPopulationThreshold = 30;
        public int ConstructionSpaceportMediumColonyPopulationThreshold = 500;
        public int ConstructionSpaceportLargeColonyPopulationThreshold = 3000;
        public int WarAttacksAllowColonyBombardment = 2;
        public int WarAttacksAllowPlanetDestroying = 2;
        public bool WarAttacksHarassEnemies = true;
        public int FleetTypicalSize = 15;
        public int FleetStrikeForceTypicalSize = 4;
        public float FleetMilitaryProportionForFleets = 60f;
        public bool TradeWithOtherEmpires = true;
        public bool EngageInTourism = true;
        public ColonyPopulationPolicy NewColonyPopulationPolicyAllRaces;
        public ColonyPopulationPolicy NewColonyPopulationPolicyYourRaceFamily;
        public bool ImplementEnslavementWithPenalColonies = true;
        public double HomeworldDefensePriority = 1.0;
        public bool ProtectLeaderAtAllCosts;
        public int PrioritizeBuildWonderId = -1;
        public double ColonizeContinentalPriority = 1.0;
        public double ColonizeMarshySwampPriority = 1.0;
        public double ColonizeOceanPriority = 1.0;
        public double ColonizeDesertPriority = 1.0;
        public double ColonizeIcePriority = 1.0;
        public double ColonizeVolcanicPriority = 1.0;
        public double ColonizeRuinsPriority = 1.0;
        public double ControlRestrictedResourcesPriority = 1.0;
        public IndustryType ResearchIndustryFocus;
        public double ResearchPriority = 1.0;
        public double TradePriority = 1.0;
        public double AlliancePriority = 1.0;
        public double SubjugationPriority = 1.0;
        public double TourismPriority = 1.0;
        public double ExplorationPriority = 1.0;
        public double WarWillingness = 1.0;
        public double BreakTreatyWillingness = 1.0;
        public double InvasionOverkillFactor = 1.0;
        public double ShipBattleCautionFactor = 1.0;
        public BuiltObjectFleeWhen DefaultMilitaryFleeWhen = BuiltObjectFleeWhen.Shields20;
        [OptionalField]
        public bool DesignUpgradeEscort = true;
        [OptionalField]
        public bool DesignUpgradeFrigate = true;
        [OptionalField]
        public bool DesignUpgradeDestroyer = true;
        [OptionalField]
        public bool DesignUpgradeCruiser = true;
        [OptionalField]
        public bool DesignUpgradeCapitalShip = true;
        [OptionalField]
        public bool DesignUpgradeTroopTransport = true;
        [OptionalField]
        public bool DesignUpgradeCarrier = true;
        [OptionalField]
        public bool DesignUpgradeResupplyShip = true;
        [OptionalField]
        public bool DesignUpgradeExplorationShip = true;
        [OptionalField]
        public bool DesignUpgradeColonyShip = true;
        [OptionalField]
        public bool DesignUpgradeConstructionShip = true;
        [OptionalField]
        public bool DesignUpgradeOutpost = true;
        [OptionalField]
        public bool DesignUpgradeSmallSpacePort = true;
        [OptionalField]
        public bool DesignUpgradeMediumSpacePort = true;
        [OptionalField]
        public bool DesignUpgradeLargeSpacePort = true;
        [OptionalField]
        public bool DesignUpgradeResortBase = true;
        [OptionalField]
        public bool DesignUpgradeGenericBase = true;
        [OptionalField]
        public bool DesignUpgradeEnergyResearchStation = true;
        [OptionalField]
        public bool DesignUpgradeWeaponsResearchStation = true;
        [OptionalField]
        public bool DesignUpgradeHighTechResearchStation = true;
        [OptionalField]
        public bool DesignUpgradeMonitoringStation = true;
        [OptionalField]
        public bool DesignUpgradeDefensiveBase = true;
        [OptionalField]
        public bool DesignUpgradeSmallFreighter = true;
        [OptionalField]
        public bool DesignUpgradeMediumFreighter = true;
        [OptionalField]
        public bool DesignUpgradeLargeFreighter = true;
        [OptionalField]
        public bool DesignUpgradePassengerShip = true;
        [OptionalField]
        public bool DesignUpgradeGasMiningShip = true;
        [OptionalField]
        public bool DesignUpgradeMiningShip = true;
        [OptionalField]
        public bool DesignUpgradeGasMiningStation = true;
        [OptionalField]
        public bool DesignUpgradeMiningStation = true;
        [OptionalField]
        public ComponentCategoryType ResearchDesignTechFocus3;
        [OptionalField]
        public ComponentCategoryType ResearchDesignTechFocus4;
        [OptionalField]
        public ComponentCategoryType ResearchDesignTechFocus5;
        [OptionalField]
        public ComponentCategoryType ResearchDesignTechFocus6;
        [OptionalField]
        public ComponentType ResearchDesignTechFocusType3;
        [OptionalField]
        public ComponentType ResearchDesignTechFocusType4;
        [OptionalField]
        public ComponentType ResearchDesignTechFocusType5;
        [OptionalField]
        public ComponentType ResearchDesignTechFocusType6;
        public int CaptureTargetConditionShip = 1;
        public int CaptureTargetConditionBase = 1;
        public int OfferPirateAttackMissions = 2;
        public bool BidOnPirateAttackMissions = true;
        public int CaptureEnlistMilitaryShip;
        public int CaptureDisassembleMilitaryShip = 1;
        public int CaptureEnlistCivilianShip = 2;
        public int CaptureDisassembleCivilianShip = 1;
        public int CaptureEnlistBase;
        public bool UpgradeEnlistedMilitaryShips = true;
        public bool UpgradeEnlistedCivilianShips = true;
        public int OfferDefensivePirateMissions = 2;
        public double PirateSmugglerFreighterLevel = 1.0;
        public double PirateSmugglerMiningLevel = 1.0;
        public double PirateSmugglerPassengerLevel = 1.0;
        public bool BidOnPirateDefendMissions = true;
        public bool AcceptPirateSmugglingMissions = true;
        public int OfferDefensivePirateMissionsSituation = 2;
        public int OfferSmugglingPirateMissions = 2;
        public double TroopRecruitInfantryLevel = 1.0;
        public double TroopRecruitArmorLevel = 1.0;
        public double TroopRecruitArtilleryLevel = 1.0;
        public double TroopRecruitSpecialForcesLevel = 1.0;
        public bool TroopUseDefaultTransportLoadout = true;
        public float TroopDefaultTransportLoadoutInfantry = 0.25f;
        public float TroopDefaultTransportLoadoutArmor = 0.5f;
        public float TroopDefaultTransportLoadoutArtillery;
        public float TroopDefaultTransportLoadoutSpecialForces = 0.25f;
        public int TroopGarrisonMinimumPerColony;
        public double TroopGarrisonLevel = 1.0;
        [OptionalField]
        public bool UseExplorationShipsToScoutEnemySystems = true;
        [OptionalField]
        public bool BuildPlanetDestroyers;

        public void LoadFromFile(string filePath)
        {
            try
            {
                string str1 = ";";
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)fileStream))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            string str2 = streamReader.ReadLine();
                            if (!string.IsNullOrEmpty(str2) && str2.Trim() != string.Empty && str2.Trim().Substring(0, 1) != "'")
                            {
                                int length = str2.IndexOf(str1);
                                if (length >= 0)
                                    this.SetNameValuePair(str2.Substring(0, length).Trim(), str2.Substring(length + 1, str2.Length - (length + 1)).Trim());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void LoadFromFile(SQLiteDataReader reader, string name)
        {
            if (reader.Read())
            {
                // Colony-related settings
                this.ColonyActionForNewTroopRecruitment = reader.GetBoolean(reader.GetOrdinal("ImmediatelyRecruitNewTroopsWhenColonize"));
                this.ColonyAllowFacilityCloningFacility = reader.GetBoolean(reader.GetOrdinal("ColonyAllowFacilityCloningFacility"));
                this.ColonyAllowFacilityFortifiedBunker = reader.GetBoolean(reader.GetOrdinal("ColonyAllowFacilityFortifiedBunker"));
                this.ColonyAllowFacilityGiantIonCannon = reader.GetBoolean(reader.GetOrdinal("ColonyAllowFacilityGiantIonCannon"));
                this.ColonyAllowFacilityPlanetaryShield = reader.GetBoolean(reader.GetOrdinal("ColonyAllowFacilityPlanetaryShield"));
                this.ColonyAllowFacilityRegionalCapital = reader.GetBoolean(reader.GetOrdinal("ColonyAllowFacilityRegionalCapital"));
                this.ColonyAllowFacilityTerraformingFacility = reader.GetBoolean(reader.GetOrdinal("ColonyAllowFacilityTerraformingFacility"));
                this.ColonyAllowFacilityRoboticTroopFoundry = reader.GetBoolean(reader.GetOrdinal("ColonyAllowFacilityRoboticTroopFoundry"));
                this.ColonyAllowFacilityTroopTrainingCenter = reader.GetBoolean(reader.GetOrdinal("ColonyAllowFacilityTroopTrainingCenter"));
                this.ColonyAllowFacilityArmoredFactory = reader.GetBoolean(reader.GetOrdinal("ColonyAllowFacilityArmoredFactory"));
                this.ColonyAllowFacilitySpyAcademy = reader.GetBoolean(reader.GetOrdinal("ColonyAllowFacilitySpyAcademy"));
                this.ColonyAllowFacilityScienceAcademy = reader.GetBoolean(reader.GetOrdinal("ColonyAllowFacilityScienceAcademy"));
                this.ColonyAllowFacilityNavalAcademy = reader.GetBoolean(reader.GetOrdinal("ColonyAllowFacilityNavalAcademy"));
                this.ColonyAllowFacilityMilitaryAcademy = reader.GetBoolean(reader.GetOrdinal("ColonyAllowFacilityMilitaryAcademy"));


                // Colony population thresholds
                this.ColonyFacilityPopulationThresholdCloningFacility = reader.GetInt32(reader.GetOrdinal("ColonyFacilityPopulationThresholdCloningFacility"));
                this.ColonyFacilityPopulationThresholdFortifiedBunker = reader.GetInt32(reader.GetOrdinal("ColonyFacilityPopulationThresholdFortifiedBunker"));
                this.ColonyFacilityPopulationThresholdGiantIonCannon = reader.GetInt32(reader.GetOrdinal("ColonyFacilityPopulationThresholdGiantIonCannon"));
                this.ColonyFacilityPopulationThresholdPlanetaryShield = reader.GetInt32(reader.GetOrdinal("ColonyFacilityPopulationThresholdPlanetaryShield"));
                this.ColonyFacilityPopulationThresholdRegionalCapital = reader.GetInt32(reader.GetOrdinal("ColonyFacilityPopulationThresholdRegionalCapital"));
                this.ColonyFacilityPopulationThresholdTerraformingFacility = reader.GetInt32(reader.GetOrdinal("ColonyFacilityPopulationThresholdTerraformingFacility"));
                this.ColonyFacilityPopulationThresholdRoboticTroopFoundry = reader.GetInt32(reader.GetOrdinal("ColonyFacilityPopulationThresholdRoboticTroopFoundry"));
                this.ColonyFacilityPopulationThresholdTroopTrainingCenter = reader.GetInt32(reader.GetOrdinal("ColonyFacilityPopulationThresholdTroopTrainingCenter"));
                this.ColonyFacilityPopulationThresholdArmoredFactory = reader.GetInt32(reader.GetOrdinal("ColonyFacilityPopulationThresholdArmoredFactory"));
                this.ColonyFacilityPopulationThresholdSpyAcademy = reader.GetInt32(reader.GetOrdinal("ColonyFacilityPopulationThresholdSpyAcademy"));
                this.ColonyFacilityPopulationThresholdScienceAcademy = reader.GetInt32(reader.GetOrdinal("ColonyFacilityPopulationThresholdScienceAcademy"));
                this.ColonyFacilityPopulationThresholdNavalAcademy = reader.GetInt32(reader.GetOrdinal("ColonyFacilityPopulationThresholdNavalAcademy"));
                this.ColonyFacilityPopulationThresholdMilitaryAcademy = reader.GetInt32(reader.GetOrdinal("ColonyFacilityPopulationThresholdMilitaryAcademy"));

                // Research and Design
                this.ResearchDesignAutoRetrofit = reader.GetBoolean(reader.GetOrdinal("ResearchDesignAutoRetrofit"));
                this.ResearchDesignAutoUpgradeFighters = reader.GetBoolean(reader.GetOrdinal("ResearchDesignAutoUpgradeFighters"));

                // Special handling for ResearchDesignOverallFocus
                int designFocus = reader.GetInt32(reader.GetOrdinal("ResearchDesignOverallFocus"));
                if (designFocus >= 0 && designFocus <= 3)
                {
                    this.ResearchDesignOverallFocus = (ShipDesignFocus)designFocus;
                }

                // Tech Focus fields
                void SetTechFocus(string fieldName, out ComponentCategoryType category, out ComponentType type)
                {
                    int value = reader.GetInt32(reader.GetOrdinal(fieldName));
                    Galaxy.ResolveTechFocus(value, out category, out type);
                }

                SetTechFocus("ResearchDesignTechFocus1", out this.ResearchDesignTechFocus1, out this.ResearchDesignTechFocusType1);
                SetTechFocus("ResearchDesignTechFocus2", out this.ResearchDesignTechFocus2, out this.ResearchDesignTechFocusType2);
                SetTechFocus("ResearchDesignTechFocus3", out this.ResearchDesignTechFocus3, out this.ResearchDesignTechFocusType3);
                SetTechFocus("ResearchDesignTechFocus4", out this.ResearchDesignTechFocus4, out this.ResearchDesignTechFocusType4);
                SetTechFocus("ResearchDesignTechFocus5", out this.ResearchDesignTechFocus5, out this.ResearchDesignTechFocusType5);
                SetTechFocus("ResearchDesignTechFocus6", out this.ResearchDesignTechFocus6, out this.ResearchDesignTechFocusType6);

                // Priorities (with clamping)
                double ClampPriority(string fieldName)
                {
                    return Math.Max(0.5, Math.Min(4.0, reader.GetDouble(reader.GetOrdinal(fieldName))));
                }

                this.HomeworldDefensePriority = ClampPriority("HomeworldDefensePriority");
                this.ColonizeContinentalPriority = ClampPriority("ColonizeContinentalPriority");
                this.ColonizeMarshySwampPriority = ClampPriority("ColonizeMarshySwampPriority");
                this.ColonizeOceanPriority = ClampPriority("ColonizeOceanPriority");
                this.ColonizeDesertPriority = ClampPriority("ColonizeDesertPriority");
                this.ColonizeIcePriority = ClampPriority("ColonizeIcePriority");
                this.ColonizeVolcanicPriority = ClampPriority("ColonizeVolcanicPriority");
                this.ColonizeRuinsPriority = ClampPriority("ColonizeRuinsPriority");

                // Tax and Colony settings
                this.ColonyTaxRateSmallColony = reader.GetInt32(reader.GetOrdinal("ColonyTaxRateSmallColony"));
                this.ColonyTaxRateMediumColony = reader.GetInt32(reader.GetOrdinal("ColonyTaxRateMediumColony"));
                this.ColonyTaxRateLargeColony = reader.GetInt32(reader.GetOrdinal("ColonyTaxRateLargeColony"));
                this.ColonyTaxRateIncreaseWhenAtWar = reader.GetBoolean(reader.GetOrdinal("ColonyTaxRateIncreaseWhenAtWar"));

                // Construction settings
                this.ConstructionMilitary = reader.GetInt32(reader.GetOrdinal("MilitaryConstructionLevel"));
                this.ConstructionMilitaryCapitalShip = reader.GetFloat(reader.GetOrdinal("ConstructionMilitaryCapitalShip"));
                this.ConstructionMilitaryCarrier = reader.GetFloat(reader.GetOrdinal("ConstructionMilitaryCarrier"));
                this.ConstructionMilitaryCruiser = reader.GetFloat(reader.GetOrdinal("ConstructionMilitaryCruiser"));
                this.ConstructionMilitaryDestroyer = reader.GetFloat(reader.GetOrdinal("ConstructionMilitaryDestroyer"));
                this.ConstructionMilitaryEscort = reader.GetFloat(reader.GetOrdinal("ConstructionMilitaryEscort"));
                this.ConstructionMilitaryFrigate = reader.GetFloat(reader.GetOrdinal("ConstructionMilitaryFrigate"));
                this.ConstructionMilitaryTroopTransport = reader.GetFloat(reader.GetOrdinal("ConstructionMilitaryTroopTransport"));

                // Spaceport thresholds
                this.ConstructionSpaceportLargeColonyPopulationThreshold = reader.GetInt32(reader.GetOrdinal("ConstructionSpaceportLargeColonyPopulationThreshold"));
                this.ConstructionSpaceportMediumColonyPopulationThreshold = reader.GetInt32(reader.GetOrdinal("ConstructionSpaceportMediumColonyPopulationThreshold"));
                this.ConstructionSpaceportMinimumDistance = reader.GetInt32(reader.GetOrdinal("ConstructionSpaceportMinimumDistance"));
                this.ConstructionSpaceportSmallColonyPopulationThreshold = reader.GetInt32(reader.GetOrdinal("ConstructionSpaceportSmallColonyPopulationThreshold"));

                // Diplomacy settings
                this.DiplomacySendGiftsUpToAmount = reader.GetInt32(reader.GetOrdinal("DiplomacySendGiftsUpToAmount"));
                this.DiplomacyTradeSanctionsUseBlockades = reader.GetBoolean(reader.GetOrdinal("DiplomacyTradeSanctionsUseBlockades"));

                // Fleet settings
                this.FleetMilitaryProportionForFleets = reader.GetFloat(reader.GetOrdinal("FleetMilitaryProportionForFleets"));
                this.FleetStrikeForceTypicalSize = reader.GetInt32(reader.GetOrdinal("FleetStrikeForceTypicalSize"));
                this.FleetTypicalSize = reader.GetInt32(reader.GetOrdinal("FleetTypicalSize"));

                // Intelligence settings
                this.IntelligenceAllowMissionDeepCover = reader.GetBoolean(reader.GetOrdinal("IntelligenceAllowMissionDeepCover"));
                this.IntelligenceAllowMissionInciteRevolution = reader.GetBoolean(reader.GetOrdinal("IntelligenceAllowMissionInciteRevolution"));
                this.IntelligenceAllowMissionSabotageColony = reader.GetBoolean(reader.GetOrdinal("IntelligenceAllowMissionSabotageColony"));
                this.IntelligenceAllowMissionSabotageConstruction = reader.GetBoolean(reader.GetOrdinal("IntelligenceAllowMissionSabotageConstruction"));
                this.IntelligenceAllowMissionStealGalaxyMap = reader.GetBoolean(reader.GetOrdinal("IntelligenceAllowMissionStealGalaxyMap"));
                this.IntelligenceAllowMissionStealOperationsMap = reader.GetBoolean(reader.GetOrdinal("IntelligenceAllowMissionStealOperationsMap"));
                this.IntelligenceAllowMissionStealTechData = reader.GetBoolean(reader.GetOrdinal("IntelligenceAllowMissionStealTechData"));
                this.IntelligenceAllowMissionStealTerritoryMap = reader.GetBoolean(reader.GetOrdinal("IntelligenceAllowMissionStealTerritoryMap"));
                this.IntelligenceAllowMissionAssassinateCharacter = reader.GetBoolean(reader.GetOrdinal("IntelligenceAllowMissionAssassinateCharacter"));
                this.IntelligenceAllowMissionDestroyBase = reader.GetBoolean(reader.GetOrdinal("IntelligenceAllowMissionDestroyBase"));
                this.IntelligenceCounterIntelligenceProportion = reader.GetFloat(reader.GetOrdinal("IntelligenceCounterIntelligenceProportion"));
                this.IntelligenceUseEspionageAgainstEmpireWhen = reader.GetInt32(reader.GetOrdinal("IntelligenceUseEspionageAgainstEmpireWhen"));
                this.IntelligenceUseSabotageAgainstEmpireWhen = reader.GetInt32(reader.GetOrdinal("IntelligenceUseSabotageAgainstEmpireWhen"));

                // Colony Population Policy
                byte colonyPolicyAllRaces = (byte)reader.GetInt32(reader.GetOrdinal("NewColonyPopulationPolicyAllRaces"));
                byte colonyPolicyYourRace = (byte)reader.GetInt32(reader.GetOrdinal("NewColonyPopulationPolicyYourRaceFamily"));
                this.NewColonyPopulationPolicyAllRaces = (ColonyPopulationPolicy)colonyPolicyAllRaces;
                this.NewColonyPopulationPolicyYourRaceFamily = (ColonyPopulationPolicy)colonyPolicyYourRace;

                // War and Military settings
                this.WarAttacksAllowColonyBombardment = reader.GetInt32(reader.GetOrdinal("WarAttacksAllowColonyBombardment"));
                this.WarAttacksAllowPlanetDestroying = reader.GetInt32(reader.GetOrdinal("WarAttacksAllowPlanetDestroying"));
                this.WarAttacksHarassEnemies = reader.GetBoolean(reader.GetOrdinal("WarAttacksHarassEnemies"));

                // Design Upgrade settings - all boolean values=
                this.DesignUpgradeEscort = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeEscort"));
                this.DesignUpgradeFrigate = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeFrigate"));
                this.DesignUpgradeDestroyer = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeDestroyer"));
                this.DesignUpgradeCruiser = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeCruiser"));
                this.DesignUpgradeCapitalShip = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeCapitalShip"));
                this.DesignUpgradeTroopTransport = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeTroopTransport"));
                this.DesignUpgradeCarrier = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeCarrier"));
                this.DesignUpgradeResupplyShip = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeResupplyShip"));
                this.DesignUpgradeExplorationShip = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeExplorationShip"));
                this.DesignUpgradeColonyShip = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeColonyShip"));
                this.DesignUpgradeConstructionShip = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeConstructionShip"));
                this.DesignUpgradeOutpost = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeOutpost"));
                this.DesignUpgradeSmallSpacePort = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeSmallSpacePort"));
                this.DesignUpgradeMediumSpacePort = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeMediumSpacePort"));
                this.DesignUpgradeLargeSpacePort = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeLargeSpacePort"));
                this.DesignUpgradeResortBase = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeResortBase"));
                this.DesignUpgradeGenericBase = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeGenericBase"));
                this.DesignUpgradeEnergyResearchStation = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeEnergyResearchStation"));
                this.DesignUpgradeWeaponsResearchStation = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeWeaponsResearchStation"));
                this.DesignUpgradeHighTechResearchStation = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeHighTechResearchStation"));
                this.DesignUpgradeMonitoringStation = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeMonitoringStation"));
                this.DesignUpgradeDefensiveBase = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeDefensiveBase"));
                this.DesignUpgradeSmallFreighter = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeSmallFreighter"));
                this.DesignUpgradeMediumFreighter = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeMediumFreighter"));
                this.DesignUpgradeLargeFreighter = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeLargeFreighter"));
                this.DesignUpgradePassengerShip = reader.GetBoolean(reader.GetOrdinal("DesignUpgradePassengerShip"));
                this.DesignUpgradeGasMiningShip = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeGasMiningShip"));
                this.DesignUpgradeMiningShip = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeMiningShip"));
                this.DesignUpgradeGasMiningStation = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeGasMiningStation"));
                this.DesignUpgradeMiningStation = reader.GetBoolean(reader.GetOrdinal("DesignUpgradeMiningStation"));


                // Troop settings
                this.TroopRecruitInfantryLevel = reader.GetDouble(reader.GetOrdinal("TroopRecruitInfantryLevel"));
                this.TroopRecruitArmorLevel = reader.GetDouble(reader.GetOrdinal("TroopRecruitArmorLevel"));
                this.TroopRecruitArtilleryLevel = reader.GetDouble(reader.GetOrdinal("TroopRecruitArtilleryLevel"));
                this.TroopRecruitSpecialForcesLevel = reader.GetDouble(reader.GetOrdinal("TroopRecruitSpecialForcesLevel"));

                // Troop transport settings
                this.TroopUseDefaultTransportLoadout = reader.GetBoolean(reader.GetOrdinal("TroopUseDefaultTransportLoadout"));
                this.TroopDefaultTransportLoadoutInfantry = reader.GetFloat(reader.GetOrdinal("TroopDefaultTransportLoadoutInfantry"));
                this.TroopDefaultTransportLoadoutArmor = reader.GetFloat(reader.GetOrdinal("TroopDefaultTransportLoadoutArmor"));
                this.TroopDefaultTransportLoadoutArtillery = reader.GetFloat(reader.GetOrdinal("TroopDefaultTransportLoadoutArtillery"));
                this.TroopDefaultTransportLoadoutSpecialForces = reader.GetFloat(reader.GetOrdinal("TroopDefaultTransportLoadoutSpecialForces"));

                // Troop garrison settings
                this.TroopGarrisonMinimumPerColony = reader.GetInt32(reader.GetOrdinal("TroopGarrisonMinimumPerColony"));
                this.TroopGarrisonLevel = reader.GetDouble(reader.GetOrdinal("TroopGarrisonLevel"));

                // Capture settings
                this.CaptureTargetConditionShip = reader.GetInt32(reader.GetOrdinal("CaptureTargetConditionShip"));
                this.CaptureTargetConditionBase = reader.GetInt32(reader.GetOrdinal("CaptureTargetConditionBase"));
                this.CaptureEnlistMilitaryShip = reader.GetInt32(reader.GetOrdinal("CaptureEnlistMilitaryShip"));
                this.CaptureDisassembleMilitaryShip = reader.GetInt32(reader.GetOrdinal("CaptureDisassembleMilitaryShip"));
                this.CaptureEnlistCivilianShip = reader.GetInt32(reader.GetOrdinal("CaptureEnlistCivilianShip"));
                this.CaptureDisassembleCivilianShip = reader.GetInt32(reader.GetOrdinal("CaptureDisassembleCivilianShip"));
                this.CaptureEnlistBase = reader.GetInt32(reader.GetOrdinal("CaptureEnlistBase"));

                // Upgrade settings
                this.UpgradeEnlistedMilitaryShips = reader.GetBoolean(reader.GetOrdinal("UpgradeEnlistedMilitaryShips"));
                this.UpgradeEnlistedCivilianShips = reader.GetBoolean(reader.GetOrdinal("UpgradeEnlistedCivilianShips"));

                // Pirate mission settings
                this.OfferPirateAttackMissions = reader.GetInt32(reader.GetOrdinal("OfferPirateAttackMissions"));
                this.BidOnPirateAttackMissions = reader.GetBoolean(reader.GetOrdinal("BidOnPirateAttackMissions"));
                this.BidOnPirateDefendMissions = reader.GetBoolean(reader.GetOrdinal("BidOnPirateDefendMissions"));
                this.OfferDefensivePirateMissions = reader.GetInt32(reader.GetOrdinal("OfferDefensivePirateMissions"));
                this.OfferDefensivePirateMissionsSituation = reader.GetInt32(reader.GetOrdinal("OfferDefensivePirateMissionsSituation"));
                this.AcceptPirateSmugglingMissions = reader.GetBoolean(reader.GetOrdinal("AcceptPirateSmugglingMissions"));
                this.OfferSmugglingPirateMissions = reader.GetInt32(reader.GetOrdinal("OfferSmugglingPirateMissions"));

                // Pirate smuggler levels
                this.PirateSmugglerFreighterLevel = reader.GetDouble(reader.GetOrdinal("PirateSmugglerFreighterLevel"));
                this.PirateSmugglerMiningLevel = reader.GetDouble(reader.GetOrdinal("PirateSmugglerMiningLevel"));
                this.PirateSmugglerPassengerLevel = reader.GetDouble(reader.GetOrdinal("PirateSmugglerPassengerLevel"));

                // Exploration settings
                this.UseExplorationShipsToScoutEnemySystems = reader.GetBoolean(reader.GetOrdinal("UseExplorationShipsToScoutEnemySystems"));
                this.BuildPlanetDestroyers = reader.GetBoolean(reader.GetOrdinal("BuildPlanetDestroyers"));

                // Remaining priority fields with clamping
                this.ControlRestrictedResourcesPriority = ClampPriority("ControlRestrictedResourcesPriority");
                this.ResearchPriority = ClampPriority("ResearchPriority");
                this.TradePriority = ClampPriority("TradePriority");
                this.AlliancePriority = ClampPriority("AlliancePriority");
                this.SubjugationPriority = ClampPriority("SubjugationPriority");
                this.TourismPriority = ClampPriority("TourismPriority");
                this.ExplorationPriority = ClampPriority("ExplorationPriority");
                this.WarWillingness = ClampPriority("WarWillingness");
                this.BreakTreatyWillingness = ClampPriority("BreakTreatyWillingness");
                this.InvasionOverkillFactor = ClampPriority("InvasionOverkillFactor");
                this.ShipBattleCautionFactor = ClampPriority("ShipBattleCautionFactor");

                // Additional game settings
                this.TradeWithOtherEmpires = reader.GetBoolean(reader.GetOrdinal("TradeWithOtherEmpires"));
                this.EngageInTourism = reader.GetBoolean(reader.GetOrdinal("EngageInTourism"));
                this.ImplementEnslavementWithPenalColonies = reader.GetBoolean(reader.GetOrdinal("ImplementEnslavementWithPenalColonies"));
                this.ProtectLeaderAtAllCosts = reader.GetBoolean(reader.GetOrdinal("ProtectLeaderAtAllCosts"));
                this.PrioritizeBuildWonderId = reader.GetInt32(reader.GetOrdinal("PrioritizeBuildWonderId"));

                // Research Industry Focus
                byte industryValue = (byte)reader.GetInt32(reader.GetOrdinal("ResearchIndustryFocus"));
                this.ResearchIndustryFocus = (IndustryType)industryValue;

                // Military settings
                byte fleeWhenValue = (byte)reader.GetInt32(reader.GetOrdinal("DefaultMilitaryFleeWhen"));
                this.DefaultMilitaryFleeWhen = (BuiltObjectFleeWhen)fleeWhenValue;

                this.UseExplorationShipsToScoutEnemySystems = reader.GetBoolean(reader.GetOrdinal("UseExplorationShipsToScoutEnemySystems"));
                this.BuildPlanetDestroyers = reader.GetBoolean(reader.GetOrdinal("BuildPlanetDestroyers"));
                this.ResearchDesignAutoUpgradeFighters = reader.GetBoolean(reader.GetOrdinal("ResearchDesignAutoUpgradeFighters"));
            }
            else
                throw new ApplicationException($"Failed to read AI settings from database. No Empire with name '{name}' found");
        }

        private void SetNameValuePair(string name, string value)
        {
            name = name.Trim();
            value = value.Trim();
            switch (name)
            {
                case "ImmediatelyRecruitNewTroopsWhenColonize":
                    this.ColonyActionForNewTroopRecruitment = this.ParseBoolValue(value);
                    break;
                case "ColonyAllowFacilityCloningFacility":
                    this.ColonyAllowFacilityCloningFacility = this.ParseBoolValue(value);
                    break;
                case "ColonyAllowFacilityFortifiedBunker":
                    this.ColonyAllowFacilityFortifiedBunker = this.ParseBoolValue(value);
                    break;
                case "ColonyAllowFacilityGiantIonCannon":
                    this.ColonyAllowFacilityGiantIonCannon = this.ParseBoolValue(value);
                    break;
                case "ColonyAllowFacilityPlanetaryShield":
                    this.ColonyAllowFacilityPlanetaryShield = this.ParseBoolValue(value);
                    break;
                case "ColonyAllowFacilityRegionalCapital":
                    this.ColonyAllowFacilityRegionalCapital = this.ParseBoolValue(value);
                    break;
                case "ColonyAllowFacilityTerraformingFacility":
                    this.ColonyAllowFacilityTerraformingFacility = this.ParseBoolValue(value);
                    break;
                case "ColonyAllowFacilityRoboticTroopFoundry":
                    this.ColonyAllowFacilityRoboticTroopFoundry = this.ParseBoolValue(value);
                    break;
                case "ColonyAllowFacilityTroopTrainingCenter":
                    this.ColonyAllowFacilityTroopTrainingCenter = this.ParseBoolValue(value);
                    break;
                case "ColonyAllowFacilityArmoredFactory":
                    this.ColonyAllowFacilityArmoredFactory = this.ParseBoolValue(value);
                    break;
                case "ColonyAllowFacilitySpyAcademy":
                    this.ColonyAllowFacilitySpyAcademy = this.ParseBoolValue(value);
                    break;
                case "ColonyAllowFacilityScienceAcademy":
                    this.ColonyAllowFacilityScienceAcademy = this.ParseBoolValue(value);
                    break;
                case "ColonyAllowFacilityNavalAcademy":
                    this.ColonyAllowFacilityNavalAcademy = this.ParseBoolValue(value);
                    break;
                case "ColonyAllowFacilityMilitaryAcademy":
                    this.ColonyAllowFacilityMilitaryAcademy = this.ParseBoolValue(value);
                    break;
                case "ColonyFacilityPopulationThresholdCloningFacility":
                    this.ColonyFacilityPopulationThresholdCloningFacility = this.ParseIntValue(value);
                    break;
                case "ColonyFacilityPopulationThresholdFortifiedBunker":
                    this.ColonyFacilityPopulationThresholdFortifiedBunker = this.ParseIntValue(value);
                    break;
                case "ColonyFacilityPopulationThresholdGiantIonCannon":
                    this.ColonyFacilityPopulationThresholdGiantIonCannon = this.ParseIntValue(value);
                    break;
                case "ColonyFacilityPopulationThresholdPlanetaryShield":
                    this.ColonyFacilityPopulationThresholdPlanetaryShield = this.ParseIntValue(value);
                    break;
                case "ColonyFacilityPopulationThresholdRegionalCapital":
                    this.ColonyFacilityPopulationThresholdRegionalCapital = this.ParseIntValue(value);
                    break;
                case "ColonyFacilityPopulationThresholdTerraformingFacility":
                    this.ColonyFacilityPopulationThresholdTerraformingFacility = this.ParseIntValue(value);
                    break;
                case "ColonyFacilityPopulationThresholdRoboticTroopFoundry":
                    this.ColonyFacilityPopulationThresholdRoboticTroopFoundry = this.ParseIntValue(value);
                    break;
                case "ColonyFacilityPopulationThresholdTroopTrainingCenter":
                    this.ColonyFacilityPopulationThresholdTroopTrainingCenter = this.ParseIntValue(value);
                    break;
                case "ColonyFacilityPopulationThresholdArmoredFactory":
                    this.ColonyFacilityPopulationThresholdArmoredFactory = this.ParseIntValue(value);
                    break;
                case "ColonyFacilityPopulationThresholdSpyAcademy":
                    this.ColonyFacilityPopulationThresholdSpyAcademy = this.ParseIntValue(value);
                    break;
                case "ColonyFacilityPopulationThresholdScienceAcademy":
                    this.ColonyFacilityPopulationThresholdScienceAcademy = this.ParseIntValue(value);
                    break;
                case "ColonyFacilityPopulationThresholdNavalAcademy":
                    this.ColonyFacilityPopulationThresholdNavalAcademy = this.ParseIntValue(value);
                    break;
                case "ColonyFacilityPopulationThresholdMilitaryAcademy":
                    this.ColonyFacilityPopulationThresholdMilitaryAcademy = this.ParseIntValue(value);
                    break;
                case "ColonyPopulationThresholdTroopRecruitment":
                    this.ColonyPopulationThresholdTroopRecruitment = this.ParseIntValue(value);
                    break;
                case "ColonyTaxRateIncreaseWhenAtWar":
                    this.ColonyTaxRateIncreaseWhenAtWar = this.ParseBoolValue(value);
                    break;
                case "ColonyTaxRateLargeColony":
                    this.ColonyTaxRateLargeColony = this.ParseIntValue(value);
                    break;
                case "ColonyTaxRateMediumColony":
                    this.ColonyTaxRateMediumColony = this.ParseIntValue(value);
                    break;
                case "ColonyTaxRateSmallColony":
                    this.ColonyTaxRateSmallColony = this.ParseIntValue(value);
                    break;
                case "MilitaryConstructionLevel":
                    this.ConstructionMilitary = this.ParseIntValue(value);
                    break;
                case "ConstructionMilitaryCapitalShip":
                    this.ConstructionMilitaryCapitalShip = this.ParseFloatValue(value);
                    break;
                case "ConstructionMilitaryCarrier":
                    this.ConstructionMilitaryCarrier = this.ParseFloatValue(value);
                    break;
                case "ConstructionMilitaryCruiser":
                    this.ConstructionMilitaryCruiser = this.ParseFloatValue(value);
                    break;
                case "ConstructionMilitaryDestroyer":
                    this.ConstructionMilitaryDestroyer = this.ParseFloatValue(value);
                    break;
                case "ConstructionMilitaryEscort":
                    this.ConstructionMilitaryEscort = this.ParseFloatValue(value);
                    break;
                case "ConstructionMilitaryFrigate":
                    this.ConstructionMilitaryFrigate = this.ParseFloatValue(value);
                    break;
                case "ConstructionMilitaryTroopTransport":
                    this.ConstructionMilitaryTroopTransport = this.ParseFloatValue(value);
                    break;
                case "ConstructionSpaceportLargeColonyPopulationThreshold":
                    this.ConstructionSpaceportLargeColonyPopulationThreshold = this.ParseIntValue(value);
                    break;
                case "ConstructionSpaceportMediumColonyPopulationThreshold":
                    this.ConstructionSpaceportMediumColonyPopulationThreshold = this.ParseIntValue(value);
                    break;
                case "ConstructionSpaceportMinimumDistance":
                    this.ConstructionSpaceportMinimumDistance = this.ParseIntValue(value);
                    break;
                case "ConstructionSpaceportSmallColonyPopulationThreshold":
                    this.ConstructionSpaceportSmallColonyPopulationThreshold = this.ParseIntValue(value);
                    break;
                case "DiplomacySendGiftsUpToAmount":
                    this.DiplomacySendGiftsUpToAmount = this.ParseIntValue(value);
                    break;
                case "DiplomacyTradeSanctionsUseBlockades":
                    this.DiplomacyTradeSanctionsUseBlockades = this.ParseBoolValue(value);
                    break;
                case "EngageInTourism":
                    this.EngageInTourism = this.ParseBoolValue(value);
                    break;
                case "FleetMilitaryProportionForFleets":
                    this.FleetMilitaryProportionForFleets = this.ParseFloatValue(value);
                    break;
                case "FleetStrikeForceTypicalSize":
                    this.FleetStrikeForceTypicalSize = this.ParseIntValue(value);
                    break;
                case "FleetTypicalSize":
                    this.FleetTypicalSize = this.ParseIntValue(value);
                    break;
                case "ImplementEnslavementWithPenalColonies":
                    this.ImplementEnslavementWithPenalColonies = this.ParseBoolValue(value);
                    break;
                case "IntelligenceAllowMissionDeepCover":
                    this.IntelligenceAllowMissionDeepCover = this.ParseBoolValue(value);
                    break;
                case "IntelligenceAllowMissionInciteRevolution":
                    this.IntelligenceAllowMissionInciteRevolution = this.ParseBoolValue(value);
                    break;
                case "IntelligenceAllowMissionSabotageColony":
                    this.IntelligenceAllowMissionSabotageColony = this.ParseBoolValue(value);
                    break;
                case "IntelligenceAllowMissionSabotageConstruction":
                    this.IntelligenceAllowMissionSabotageConstruction = this.ParseBoolValue(value);
                    break;
                case "IntelligenceAllowMissionStealGalaxyMap":
                    this.IntelligenceAllowMissionStealGalaxyMap = this.ParseBoolValue(value);
                    break;
                case "IntelligenceAllowMissionStealOperationsMap":
                    this.IntelligenceAllowMissionStealOperationsMap = this.ParseBoolValue(value);
                    break;
                case "IntelligenceAllowMissionStealTechData":
                    this.IntelligenceAllowMissionStealTechData = this.ParseBoolValue(value);
                    break;
                case "IntelligenceAllowMissionStealTerritoryMap":
                    this.IntelligenceAllowMissionStealTerritoryMap = this.ParseBoolValue(value);
                    break;
                case "IntelligenceAllowMissionAssassinateCharacter":
                    this.IntelligenceAllowMissionAssassinateCharacter = this.ParseBoolValue(value);
                    break;
                case "IntelligenceAllowMissionDestroyBase":
                    this.IntelligenceAllowMissionDestroyBase = this.ParseBoolValue(value);
                    break;
                case "IntelligenceCounterIntelligenceProportion":
                    this.IntelligenceCounterIntelligenceProportion = this.ParseFloatValue(value);
                    break;
                case "IntelligenceUseEspionageAgainstEmpireWhen":
                    this.IntelligenceUseEspionageAgainstEmpireWhen = this.ParseIntValue(value);
                    break;
                case "IntelligenceUseSabotageAgainstEmpireWhen":
                    this.IntelligenceUseSabotageAgainstEmpireWhen = this.ParseIntValue(value);
                    break;
                case "NewColonyPopulationPolicyAllRaces":
                    this.NewColonyPopulationPolicyAllRaces = this.ParseColonyPopulationPolicyValue(value);
                    break;
                case "NewColonyPopulationPolicyYourRaceFamily":
                    this.NewColonyPopulationPolicyYourRaceFamily = this.ParseColonyPopulationPolicyValue(value);
                    break;
                case "ResearchDesignAutoRetrofit":
                    this.ResearchDesignAutoRetrofit = this.ParseBoolValue(value);
                    break;
                case "ResearchDesignAutoUpgradeFighters":
                    this.ResearchDesignAutoUpgradeFighters = this.ParseBoolValue(value);
                    break;
                case "ResearchDesignOverallFocus":
                    int intValue = this.ParseIntValue(value);
                    switch (intValue)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            this.ResearchDesignOverallFocus = (ShipDesignFocus)intValue;
                            return;
                        default:
                            return;
                    }
                case "ResearchDesignTechFocus1":
                    ComponentCategoryType category1 = ComponentCategoryType.Undefined;
                    ComponentType type1 = ComponentType.Undefined;
                    Galaxy.ResolveTechFocus(this.ParseIntValue(value), out category1, out type1);
                    this.ResearchDesignTechFocus1 = category1;
                    this.ResearchDesignTechFocusType1 = type1;
                    break;
                case "ResearchDesignTechFocus2":
                    ComponentCategoryType category2 = ComponentCategoryType.Undefined;
                    ComponentType type2 = ComponentType.Undefined;
                    Galaxy.ResolveTechFocus(this.ParseIntValue(value), out category2, out type2);
                    this.ResearchDesignTechFocus2 = category2;
                    this.ResearchDesignTechFocusType2 = type2;
                    break;
                case "TradeWithOtherEmpires":
                    this.TradeWithOtherEmpires = this.ParseBoolValue(value);
                    break;
                case "WarAttacksAllowColonyBombardment":
                    this.WarAttacksAllowColonyBombardment = this.ParseIntValue(value);
                    break;
                case "WarAttacksAllowPlanetDestroying":
                    this.WarAttacksAllowPlanetDestroying = this.ParseIntValue(value);
                    break;
                case "WarAttacksHarassEnemies":
                    this.WarAttacksHarassEnemies = this.ParseBoolValue(value);
                    break;
                case "HomeworldDefensePriority":
                    this.HomeworldDefensePriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "ColonizeContinentalPriority":
                    this.ColonizeContinentalPriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "ColonizeMarshySwampPriority":
                    this.ColonizeMarshySwampPriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "ColonizeOceanPriority":
                    this.ColonizeOceanPriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "ColonizeDesertPriority":
                    this.ColonizeDesertPriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "ColonizeIcePriority":
                    this.ColonizeIcePriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "ColonizeVolcanicPriority":
                    this.ColonizeVolcanicPriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "ColonizeRuinsPriority":
                    this.ColonizeRuinsPriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "ControlRestrictedResourcesPriority":
                    this.ControlRestrictedResourcesPriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "ResearchPriority":
                    this.ResearchPriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "TradePriority":
                    this.TradePriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "AlliancePriority":
                    this.AlliancePriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "SubjugationPriority":
                    this.SubjugationPriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "TourismPriority":
                    this.TourismPriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "ExplorationPriority":
                    this.ExplorationPriority = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "WarWillingness":
                    this.WarWillingness = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "BreakTreatyWillingness":
                    this.BreakTreatyWillingness = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "InvasionOverkillFactor":
                    this.InvasionOverkillFactor = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "ShipBattleCautionFactor":
                    this.ShipBattleCautionFactor = Math.Max(0.5, Math.Min(4.0, this.ParseDoubleValue(value)));
                    break;
                case "ProtectLeaderAtAllCosts":
                    this.ProtectLeaderAtAllCosts = this.ParseBoolValue(value);
                    break;
                case "PrioritizeBuildWonderId":
                    this.PrioritizeBuildWonderId = this.ParseIntValue(value);
                    break;
                case "ResearchIndustryFocus":
                    this.ResearchIndustryFocus = this.ParseIndustryValue(value);
                    break;
                case "DefaultMilitaryFleeWhen":
                    this.DefaultMilitaryFleeWhen = this.ParseFleeWhenValue(value);
                    break;
                case "DesignUpgradeEscort":
                    this.DesignUpgradeEscort = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeFrigate":
                    this.DesignUpgradeFrigate = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeDestroyer":
                    this.DesignUpgradeDestroyer = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeCruiser":
                    this.DesignUpgradeCruiser = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeCapitalShip":
                    this.DesignUpgradeCapitalShip = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeTroopTransport":
                    this.DesignUpgradeTroopTransport = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeCarrier":
                    this.DesignUpgradeCarrier = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeResupplyShip":
                    this.DesignUpgradeResupplyShip = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeExplorationShip":
                    this.DesignUpgradeExplorationShip = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeColonyShip":
                    this.DesignUpgradeColonyShip = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeConstructionShip":
                    this.DesignUpgradeConstructionShip = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeOutpost":
                    this.DesignUpgradeOutpost = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeSmallSpacePort":
                    this.DesignUpgradeSmallSpacePort = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeMediumSpacePort":
                    this.DesignUpgradeMediumSpacePort = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeLargeSpacePort":
                    this.DesignUpgradeLargeSpacePort = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeResortBase":
                    this.DesignUpgradeResortBase = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeGenericBase":
                    this.DesignUpgradeGenericBase = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeEnergyResearchStation":
                    this.DesignUpgradeEnergyResearchStation = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeWeaponsResearchStation":
                    this.DesignUpgradeWeaponsResearchStation = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeHighTechResearchStation":
                    this.DesignUpgradeHighTechResearchStation = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeMonitoringStation":
                    this.DesignUpgradeMonitoringStation = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeDefensiveBase":
                    this.DesignUpgradeDefensiveBase = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeSmallFreighter":
                    this.DesignUpgradeSmallFreighter = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeMediumFreighter":
                    this.DesignUpgradeMediumFreighter = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeLargeFreighter":
                    this.DesignUpgradeLargeFreighter = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradePassengerShip":
                    this.DesignUpgradePassengerShip = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeGasMiningShip":
                    this.DesignUpgradeGasMiningShip = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeMiningShip":
                    this.DesignUpgradeMiningShip = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeGasMiningStation":
                    this.DesignUpgradeGasMiningStation = this.ParseBoolValue(value);
                    break;
                case "DesignUpgradeMiningStation":
                    this.DesignUpgradeMiningStation = this.ParseBoolValue(value);
                    break;
                case "ResearchDesignTechFocus3":
                    ComponentCategoryType category3 = ComponentCategoryType.Undefined;
                    ComponentType type3 = ComponentType.Undefined;
                    Galaxy.ResolveTechFocus(this.ParseIntValue(value), out category3, out type3);
                    this.ResearchDesignTechFocus3 = category3;
                    this.ResearchDesignTechFocusType3 = type3;
                    break;
                case "ResearchDesignTechFocus4":
                    ComponentCategoryType category4 = ComponentCategoryType.Undefined;
                    ComponentType type4 = ComponentType.Undefined;
                    Galaxy.ResolveTechFocus(this.ParseIntValue(value), out category4, out type4);
                    this.ResearchDesignTechFocus4 = category4;
                    this.ResearchDesignTechFocusType4 = type4;
                    break;
                case "ResearchDesignTechFocus5":
                    ComponentCategoryType category5 = ComponentCategoryType.Undefined;
                    ComponentType type5 = ComponentType.Undefined;
                    Galaxy.ResolveTechFocus(this.ParseIntValue(value), out category5, out type5);
                    this.ResearchDesignTechFocus5 = category5;
                    this.ResearchDesignTechFocusType5 = type5;
                    break;
                case "ResearchDesignTechFocus6":
                    ComponentCategoryType category6 = ComponentCategoryType.Undefined;
                    ComponentType type6 = ComponentType.Undefined;
                    Galaxy.ResolveTechFocus(this.ParseIntValue(value), out category6, out type6);
                    this.ResearchDesignTechFocus6 = category6;
                    this.ResearchDesignTechFocusType6 = type6;
                    break;
                case "CaptureTargetConditionShip":
                    this.CaptureTargetConditionShip = this.ParseIntValue(value);
                    break;
                case "CaptureTargetConditionBase":
                    this.CaptureTargetConditionBase = this.ParseIntValue(value);
                    break;
                case "OfferPirateAttackMissions":
                    this.OfferPirateAttackMissions = this.ParseIntValue(value);
                    break;
                case "BidOnPirateAttackMissions":
                    this.BidOnPirateAttackMissions = this.ParseBoolValue(value);
                    break;
                case "BidOnPirateDefendMissions":
                    this.BidOnPirateDefendMissions = this.ParseBoolValue(value);
                    break;
                case "AcceptPirateSmugglingMissions":
                    this.AcceptPirateSmugglingMissions = this.ParseBoolValue(value);
                    break;
                case "OfferDefensivePirateMissionsSituation":
                    this.OfferDefensivePirateMissionsSituation = this.ParseIntValue(value);
                    break;
                case "OfferSmugglingPirateMissions":
                    this.OfferSmugglingPirateMissions = this.ParseIntValue(value);
                    break;
                case "CaptureEnlistMilitaryShip":
                    this.CaptureEnlistMilitaryShip = this.ParseIntValue(value);
                    break;
                case "CaptureDisassembleMilitaryShip":
                    this.CaptureDisassembleMilitaryShip = this.ParseIntValue(value);
                    break;
                case "CaptureEnlistCivilianShip":
                    this.CaptureEnlistCivilianShip = this.ParseIntValue(value);
                    break;
                case "CaptureDisassembleCivilianShip":
                    this.CaptureDisassembleCivilianShip = this.ParseIntValue(value);
                    break;
                case "CaptureEnlistBase":
                    this.CaptureEnlistBase = this.ParseIntValue(value);
                    break;
                case "UpgradeEnlistedMilitaryShips":
                    this.UpgradeEnlistedMilitaryShips = this.ParseBoolValue(value);
                    break;
                case "UpgradeEnlistedCivilianShips":
                    this.UpgradeEnlistedCivilianShips = this.ParseBoolValue(value);
                    break;
                case "OfferDefensivePirateMissions":
                    this.OfferDefensivePirateMissions = this.ParseIntValue(value);
                    break;
                case "PirateSmugglerFreighterLevel":
                    this.PirateSmugglerFreighterLevel = this.ParseDoubleValue(value);
                    break;
                case "PirateSmugglerMiningLevel":
                    this.PirateSmugglerMiningLevel = this.ParseDoubleValue(value);
                    break;
                case "PirateSmugglerPassengerLevel":
                    this.PirateSmugglerPassengerLevel = this.ParseDoubleValue(value);
                    break;
                case "TroopRecruitInfantryLevel":
                    this.TroopRecruitInfantryLevel = this.ParseDoubleValue(value);
                    break;
                case "TroopRecruitArmorLevel":
                    this.TroopRecruitArmorLevel = this.ParseDoubleValue(value);
                    break;
                case "TroopRecruitArtilleryLevel":
                    this.TroopRecruitArtilleryLevel = this.ParseDoubleValue(value);
                    break;
                case "TroopRecruitSpecialForcesLevel":
                    this.TroopRecruitSpecialForcesLevel = this.ParseDoubleValue(value);
                    break;
                case "TroopUseDefaultTransportLoadout":
                    this.TroopUseDefaultTransportLoadout = this.ParseBoolValue(value);
                    break;
                case "TroopDefaultTransportLoadoutInfantry":
                    this.TroopDefaultTransportLoadoutInfantry = this.ParseFloatValue(value);
                    break;
                case "TroopDefaultTransportLoadoutArmor":
                    this.TroopDefaultTransportLoadoutArmor = this.ParseFloatValue(value);
                    break;
                case "TroopDefaultTransportLoadoutArtillery":
                    this.TroopDefaultTransportLoadoutArtillery = this.ParseFloatValue(value);
                    break;
                case "TroopDefaultTransportLoadoutSpecialForces":
                    this.TroopDefaultTransportLoadoutSpecialForces = this.ParseFloatValue(value);
                    break;
                case "TroopGarrisonMinimumPerColony":
                    this.TroopGarrisonMinimumPerColony = this.ParseIntValue(value);
                    break;
                case "TroopGarrisonLevel":
                    this.TroopGarrisonLevel = this.ParseDoubleValue(value);
                    break;
                case "UseExplorationShipsToScoutEnemySystems":
                    this.UseExplorationShipsToScoutEnemySystems = this.ParseBoolValue(value);
                    break;
                case "BuildPlanetDestroyers":
                    this.BuildPlanetDestroyers = this.ParseBoolValue(value);
                    break;
            }
        }

        private IndustryType ParseIndustryValue(string value)
        {
            byte result = 0;
            byte.TryParse(value, out result);
            return (IndustryType)result;
        }

        private BuiltObjectFleeWhen ParseFleeWhenValue(string value)
        {
            byte result = 0;
            byte.TryParse(value, out result);
            return (BuiltObjectFleeWhen)result;
        }

        private int ParseIntValue(string value)
        {
            int result = 0;
            int.TryParse(value, NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result);
            return result;
        }

        private float ParseFloatValue(string value)
        {
            float result = 0.0f;
            float.TryParse(value, NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result);
            return result;
        }

        private double ParseDoubleValue(string value)
        {
            double result = 0.0;
            double.TryParse(value, NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result);
            return result;
        }

        private bool ParseBoolValue(string value)
        {
            bool boolValue = false;
            if (value.Trim().ToLower(CultureInfo.InvariantCulture) == "y")
                boolValue = true;
            else if (value.Trim().ToLower(CultureInfo.InvariantCulture) == "n")
                boolValue = false;
            return boolValue;
        }

        private ColonyPopulationPolicy ParseColonyPopulationPolicyValue(string value)
        {
            byte result = 0;
            byte.TryParse(value, out result);
            return (ColonyPopulationPolicy)result;
        }

        public void SaveToFile(string filePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)fileStream))
                    {
                        streamWriter.WriteLine("'Distant Worlds - Empire Policy - 1.9.0.0");
                        streamWriter.WriteLine();
                        streamWriter.WriteLine(this.BuildPolicyLine("ImmediatelyRecruitNewTroopsWhenColonize", (object)this.ColonyActionForNewTroopRecruitment));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyAllowFacilityCloningFacility", (object)this.ColonyAllowFacilityCloningFacility));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyAllowFacilityFortifiedBunker", (object)this.ColonyAllowFacilityFortifiedBunker));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyAllowFacilityGiantIonCannon", (object)this.ColonyAllowFacilityGiantIonCannon));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyAllowFacilityPlanetaryShield", (object)this.ColonyAllowFacilityPlanetaryShield));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyAllowFacilityRegionalCapital", (object)this.ColonyAllowFacilityRegionalCapital));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyAllowFacilityRoboticTroopFoundry", (object)this.ColonyAllowFacilityRoboticTroopFoundry));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyAllowFacilityTerraformingFacility", (object)this.ColonyAllowFacilityTerraformingFacility));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyAllowFacilityTroopTrainingCenter", (object)this.ColonyAllowFacilityTroopTrainingCenter));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyAllowFacilityArmoredFactory", (object)this.ColonyAllowFacilityArmoredFactory));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyAllowFacilitySpyAcademy", (object)this.ColonyAllowFacilitySpyAcademy));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyAllowFacilityScienceAcademy", (object)this.ColonyAllowFacilityScienceAcademy));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyAllowFacilityNavalAcademy", (object)this.ColonyAllowFacilityNavalAcademy));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyAllowFacilityMilitaryAcademy", (object)this.ColonyAllowFacilityMilitaryAcademy));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyFacilityPopulationThresholdCloningFacility", (object)this.ColonyFacilityPopulationThresholdCloningFacility));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyFacilityPopulationThresholdFortifiedBunker", (object)this.ColonyFacilityPopulationThresholdFortifiedBunker));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyFacilityPopulationThresholdGiantIonCannon", (object)this.ColonyFacilityPopulationThresholdGiantIonCannon));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyFacilityPopulationThresholdPlanetaryShield", (object)this.ColonyFacilityPopulationThresholdPlanetaryShield));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyFacilityPopulationThresholdRegionalCapital", (object)this.ColonyFacilityPopulationThresholdRegionalCapital));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyFacilityPopulationThresholdRoboticTroopFoundry", (object)this.ColonyFacilityPopulationThresholdRoboticTroopFoundry));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyFacilityPopulationThresholdTerraformingFacility", (object)this.ColonyFacilityPopulationThresholdTerraformingFacility));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyFacilityPopulationThresholdTroopTrainingCenter", (object)this.ColonyFacilityPopulationThresholdTroopTrainingCenter));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyFacilityPopulationThresholdArmoredFactory", (object)this.ColonyFacilityPopulationThresholdArmoredFactory));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyFacilityPopulationThresholdSpyAcademy", (object)this.ColonyFacilityPopulationThresholdSpyAcademy));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyFacilityPopulationThresholdScienceAcademy", (object)this.ColonyFacilityPopulationThresholdScienceAcademy));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyFacilityPopulationThresholdNavalAcademy", (object)this.ColonyFacilityPopulationThresholdNavalAcademy));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyFacilityPopulationThresholdMilitaryAcademy", (object)this.ColonyFacilityPopulationThresholdMilitaryAcademy));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyPopulationThresholdTroopRecruitment", (object)this.ColonyPopulationThresholdTroopRecruitment));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyTaxRateIncreaseWhenAtWar", (object)this.ColonyTaxRateIncreaseWhenAtWar));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyTaxRateLargeColony", (object)this.ColonyTaxRateLargeColony));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyTaxRateMediumColony", (object)this.ColonyTaxRateMediumColony));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonyTaxRateSmallColony", (object)this.ColonyTaxRateSmallColony));
                        streamWriter.WriteLine(this.BuildPolicyLine("MilitaryConstructionLevel", (object)this.ConstructionMilitary));
                        streamWriter.WriteLine(this.BuildPolicyLine("ConstructionMilitaryCapitalShip", (object)this.ConstructionMilitaryCapitalShip));
                        streamWriter.WriteLine(this.BuildPolicyLine("ConstructionMilitaryCarrier", (object)this.ConstructionMilitaryCarrier));
                        streamWriter.WriteLine(this.BuildPolicyLine("ConstructionMilitaryCruiser", (object)this.ConstructionMilitaryCruiser));
                        streamWriter.WriteLine(this.BuildPolicyLine("ConstructionMilitaryDestroyer", (object)this.ConstructionMilitaryDestroyer));
                        streamWriter.WriteLine(this.BuildPolicyLine("ConstructionMilitaryEscort", (object)this.ConstructionMilitaryEscort));
                        streamWriter.WriteLine(this.BuildPolicyLine("ConstructionMilitaryFrigate", (object)this.ConstructionMilitaryFrigate));
                        streamWriter.WriteLine(this.BuildPolicyLine("ConstructionMilitaryTroopTransport", (object)this.ConstructionMilitaryTroopTransport));
                        streamWriter.WriteLine(this.BuildPolicyLine("ConstructionSpaceportLargeColonyPopulationThreshold", (object)this.ConstructionSpaceportLargeColonyPopulationThreshold));
                        streamWriter.WriteLine(this.BuildPolicyLine("ConstructionSpaceportMediumColonyPopulationThreshold", (object)this.ConstructionSpaceportMediumColonyPopulationThreshold));
                        streamWriter.WriteLine(this.BuildPolicyLine("ConstructionSpaceportSmallColonyPopulationThreshold", (object)this.ConstructionSpaceportSmallColonyPopulationThreshold));
                        streamWriter.WriteLine(this.BuildPolicyLine("ConstructionSpaceportMinimumDistance", (object)this.ConstructionSpaceportMinimumDistance));
                        streamWriter.WriteLine(this.BuildPolicyLine("DiplomacySendGiftsUpToAmount", (object)this.DiplomacySendGiftsUpToAmount));
                        streamWriter.WriteLine(this.BuildPolicyLine("DiplomacyTradeSanctionsUseBlockades", (object)this.DiplomacyTradeSanctionsUseBlockades));
                        streamWriter.WriteLine(this.BuildPolicyLine("FleetMilitaryProportionForFleets", (object)this.FleetMilitaryProportionForFleets));
                        streamWriter.WriteLine(this.BuildPolicyLine("FleetStrikeForceTypicalSize", (object)this.FleetStrikeForceTypicalSize));
                        streamWriter.WriteLine(this.BuildPolicyLine("FleetTypicalSize", (object)this.FleetTypicalSize));
                        streamWriter.WriteLine(this.BuildPolicyLine("IntelligenceAllowMissionDeepCover", (object)this.IntelligenceAllowMissionDeepCover));
                        streamWriter.WriteLine(this.BuildPolicyLine("IntelligenceAllowMissionInciteRevolution", (object)this.IntelligenceAllowMissionInciteRevolution));
                        streamWriter.WriteLine(this.BuildPolicyLine("IntelligenceAllowMissionSabotageColony", (object)this.IntelligenceAllowMissionSabotageColony));
                        streamWriter.WriteLine(this.BuildPolicyLine("IntelligenceAllowMissionSabotageConstruction", (object)this.IntelligenceAllowMissionSabotageConstruction));
                        streamWriter.WriteLine(this.BuildPolicyLine("IntelligenceAllowMissionStealGalaxyMap", (object)this.IntelligenceAllowMissionStealGalaxyMap));
                        streamWriter.WriteLine(this.BuildPolicyLine("IntelligenceAllowMissionStealOperationsMap", (object)this.IntelligenceAllowMissionStealOperationsMap));
                        streamWriter.WriteLine(this.BuildPolicyLine("IntelligenceAllowMissionStealTechData", (object)this.IntelligenceAllowMissionStealTechData));
                        streamWriter.WriteLine(this.BuildPolicyLine("IntelligenceAllowMissionStealTerritoryMap", (object)this.IntelligenceAllowMissionStealTerritoryMap));
                        streamWriter.WriteLine(this.BuildPolicyLine("IntelligenceAllowMissionAssassinateCharacter", (object)this.IntelligenceAllowMissionAssassinateCharacter));
                        streamWriter.WriteLine(this.BuildPolicyLine("IntelligenceAllowMissionDestroyBase", (object)this.IntelligenceAllowMissionDestroyBase));
                        streamWriter.WriteLine(this.BuildPolicyLine("IntelligenceCounterIntelligenceProportion", (object)this.IntelligenceCounterIntelligenceProportion));
                        streamWriter.WriteLine(this.BuildPolicyLine("IntelligenceUseEspionageAgainstEmpireWhen", (object)this.IntelligenceUseEspionageAgainstEmpireWhen));
                        streamWriter.WriteLine(this.BuildPolicyLine("IntelligenceUseSabotageAgainstEmpireWhen", (object)this.IntelligenceUseSabotageAgainstEmpireWhen));
                        streamWriter.WriteLine(this.BuildPolicyLine("ResearchDesignAutoRetrofit", (object)this.ResearchDesignAutoRetrofit));
                        streamWriter.WriteLine(this.BuildPolicyLine("ResearchDesignOverallFocus", (object)this.ResearchDesignOverallFocus));
                        int num1 = 0;
                        if (this.ResearchDesignTechFocus1 != ComponentCategoryType.Undefined)
                            num1 = Galaxy.ResolveTechFocusIndex(this.ResearchDesignTechFocus1);
                        else if (this.ResearchDesignTechFocusType1 != ComponentType.Undefined)
                            num1 = Galaxy.ResolveTechFocusIndex(this.ResearchDesignTechFocusType1);
                        streamWriter.WriteLine(this.BuildPolicyLine("ResearchDesignTechFocus1", (object)num1));
                        int num2 = 0;
                        if (this.ResearchDesignTechFocus2 != ComponentCategoryType.Undefined)
                            num2 = Galaxy.ResolveTechFocusIndex(this.ResearchDesignTechFocus2);
                        else if (this.ResearchDesignTechFocusType2 != ComponentType.Undefined)
                            num2 = Galaxy.ResolveTechFocusIndex(this.ResearchDesignTechFocusType2);
                        streamWriter.WriteLine(this.BuildPolicyLine("ResearchDesignTechFocus2", (object)num2));
                        int num3 = 0;
                        if (this.ResearchDesignTechFocus3 != ComponentCategoryType.Undefined)
                            num3 = Galaxy.ResolveTechFocusIndex(this.ResearchDesignTechFocus3);
                        else if (this.ResearchDesignTechFocusType3 != ComponentType.Undefined)
                            num3 = Galaxy.ResolveTechFocusIndex(this.ResearchDesignTechFocusType3);
                        streamWriter.WriteLine(this.BuildPolicyLine("ResearchDesignTechFocus3", (object)num3));
                        int num4 = 0;
                        if (this.ResearchDesignTechFocus4 != ComponentCategoryType.Undefined)
                            num4 = Galaxy.ResolveTechFocusIndex(this.ResearchDesignTechFocus4);
                        else if (this.ResearchDesignTechFocusType4 != ComponentType.Undefined)
                            num4 = Galaxy.ResolveTechFocusIndex(this.ResearchDesignTechFocusType4);
                        streamWriter.WriteLine(this.BuildPolicyLine("ResearchDesignTechFocus4", (object)num4));
                        int num5 = 0;
                        if (this.ResearchDesignTechFocus5 != ComponentCategoryType.Undefined)
                            num5 = Galaxy.ResolveTechFocusIndex(this.ResearchDesignTechFocus5);
                        else if (this.ResearchDesignTechFocusType5 != ComponentType.Undefined)
                            num5 = Galaxy.ResolveTechFocusIndex(this.ResearchDesignTechFocusType5);
                        streamWriter.WriteLine(this.BuildPolicyLine("ResearchDesignTechFocus5", (object)num5));
                        int num6 = 0;
                        if (this.ResearchDesignTechFocus6 != ComponentCategoryType.Undefined)
                            num6 = Galaxy.ResolveTechFocusIndex(this.ResearchDesignTechFocus6);
                        else if (this.ResearchDesignTechFocusType6 != ComponentType.Undefined)
                            num6 = Galaxy.ResolveTechFocusIndex(this.ResearchDesignTechFocusType6);
                        streamWriter.WriteLine(this.BuildPolicyLine("ResearchDesignTechFocus6", (object)num6));
                        streamWriter.WriteLine(this.BuildPolicyLine("ResearchDesignAutoUpgradeFighters", (object)this.ResearchDesignAutoUpgradeFighters));
                        streamWriter.WriteLine(this.BuildPolicyLine("WarAttacksAllowColonyBombardment", (object)this.WarAttacksAllowColonyBombardment));
                        streamWriter.WriteLine(this.BuildPolicyLine("WarAttacksAllowPlanetDestroying", (object)this.WarAttacksAllowPlanetDestroying));
                        streamWriter.WriteLine(this.BuildPolicyLine("WarAttacksHarassEnemies", (object)this.WarAttacksHarassEnemies));
                        streamWriter.WriteLine(this.BuildPolicyLine("TradeWithOtherEmpires", (object)this.TradeWithOtherEmpires));
                        streamWriter.WriteLine(this.BuildPolicyLine("EngageInTourism", (object)this.EngageInTourism));
                        streamWriter.WriteLine(this.BuildPolicyLine("NewColonyPopulationPolicyYourRaceFamily", (object)this.NewColonyPopulationPolicyYourRaceFamily));
                        streamWriter.WriteLine(this.BuildPolicyLine("NewColonyPopulationPolicyAllRaces", (object)this.NewColonyPopulationPolicyAllRaces));
                        streamWriter.WriteLine(this.BuildPolicyLine("ImplementEnslavementWithPenalColonies", (object)this.ImplementEnslavementWithPenalColonies));
                        streamWriter.WriteLine(this.BuildPolicyLine("HomeworldDefensePriority", (object)this.HomeworldDefensePriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("ProtectLeaderAtAllCosts", (object)this.ProtectLeaderAtAllCosts));
                        streamWriter.WriteLine(this.BuildPolicyLine("PrioritizeBuildWonderId", (object)this.PrioritizeBuildWonderId));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonizeContinentalPriority", (object)this.ColonizeContinentalPriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonizeMarshySwampPriority", (object)this.ColonizeMarshySwampPriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonizeOceanPriority", (object)this.ColonizeOceanPriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonizeDesertPriority", (object)this.ColonizeDesertPriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonizeIcePriority", (object)this.ColonizeIcePriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonizeVolcanicPriority", (object)this.ColonizeVolcanicPriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("ColonizeRuinsPriority", (object)this.ColonizeRuinsPriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("ControlRestrictedResourcesPriority", (object)this.ControlRestrictedResourcesPriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("ResearchIndustryFocus", (object)this.ResearchIndustryFocus));
                        streamWriter.WriteLine(this.BuildPolicyLine("ResearchPriority", (object)this.ResearchPriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("TradePriority", (object)this.TradePriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("AlliancePriority", (object)this.AlliancePriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("SubjugationPriority", (object)this.SubjugationPriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("TourismPriority", (object)this.TourismPriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("ExplorationPriority", (object)this.ExplorationPriority));
                        streamWriter.WriteLine(this.BuildPolicyLine("WarWillingness", (object)this.WarWillingness));
                        streamWriter.WriteLine(this.BuildPolicyLine("BreakTreatyWillingness", (object)this.BreakTreatyWillingness));
                        streamWriter.WriteLine(this.BuildPolicyLine("InvasionOverkillFactor", (object)this.InvasionOverkillFactor));
                        streamWriter.WriteLine(this.BuildPolicyLine("ShipBattleCautionFactor", (object)this.ShipBattleCautionFactor));
                        streamWriter.WriteLine(this.BuildPolicyLine("DefaultMilitaryFleeWhen", (object)this.DefaultMilitaryFleeWhen));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeEscort", (object)this.DesignUpgradeEscort));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeFrigate", (object)this.DesignUpgradeFrigate));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeDestroyer", (object)this.DesignUpgradeDestroyer));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeCruiser", (object)this.DesignUpgradeCruiser));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeCapitalShip", (object)this.DesignUpgradeCapitalShip));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeTroopTransport", (object)this.DesignUpgradeTroopTransport));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeCarrier", (object)this.DesignUpgradeCarrier));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeResupplyShip", (object)this.DesignUpgradeResupplyShip));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeExplorationShip", (object)this.DesignUpgradeExplorationShip));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeColonyShip", (object)this.DesignUpgradeColonyShip));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeConstructionShip", (object)this.DesignUpgradeConstructionShip));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeOutpost", (object)this.DesignUpgradeOutpost));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeSmallSpacePort", (object)this.DesignUpgradeSmallSpacePort));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeMediumSpacePort", (object)this.DesignUpgradeMediumSpacePort));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeLargeSpacePort", (object)this.DesignUpgradeLargeSpacePort));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeResortBase", (object)this.DesignUpgradeResortBase));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeGenericBase", (object)this.DesignUpgradeGenericBase));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeEnergyResearchStation", (object)this.DesignUpgradeEnergyResearchStation));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeWeaponsResearchStation", (object)this.DesignUpgradeWeaponsResearchStation));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeHighTechResearchStation", (object)this.DesignUpgradeHighTechResearchStation));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeMonitoringStation", (object)this.DesignUpgradeMonitoringStation));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeDefensiveBase", (object)this.DesignUpgradeDefensiveBase));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeSmallFreighter", (object)this.DesignUpgradeSmallFreighter));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeMediumFreighter", (object)this.DesignUpgradeMediumFreighter));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeLargeFreighter", (object)this.DesignUpgradeLargeFreighter));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradePassengerShip", (object)this.DesignUpgradePassengerShip));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeGasMiningShip", (object)this.DesignUpgradeGasMiningShip));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeMiningShip", (object)this.DesignUpgradeMiningShip));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeGasMiningStation", (object)this.DesignUpgradeGasMiningStation));
                        streamWriter.WriteLine(this.BuildPolicyLine("DesignUpgradeMiningStation", (object)this.DesignUpgradeMiningStation));
                        streamWriter.WriteLine(this.BuildPolicyLine("CaptureTargetConditionShip", (object)this.CaptureTargetConditionShip));
                        streamWriter.WriteLine(this.BuildPolicyLine("CaptureTargetConditionBase", (object)this.CaptureTargetConditionBase));
                        streamWriter.WriteLine(this.BuildPolicyLine("OfferPirateAttackMissions", (object)this.OfferPirateAttackMissions));
                        streamWriter.WriteLine(this.BuildPolicyLine("BidOnPirateAttackMissions", (object)this.BidOnPirateAttackMissions));
                        streamWriter.WriteLine(this.BuildPolicyLine("BidOnPirateDefendMissions", (object)this.BidOnPirateDefendMissions));
                        streamWriter.WriteLine(this.BuildPolicyLine("OfferDefensivePirateMissions", (object)this.OfferDefensivePirateMissions));
                        streamWriter.WriteLine(this.BuildPolicyLine("OfferDefensivePirateMissionsSituation", (object)this.OfferDefensivePirateMissionsSituation));
                        streamWriter.WriteLine(this.BuildPolicyLine("AcceptPirateSmugglingMissions", (object)this.AcceptPirateSmugglingMissions));
                        streamWriter.WriteLine(this.BuildPolicyLine("OfferSmugglingPirateMissions", (object)this.OfferSmugglingPirateMissions));
                        streamWriter.WriteLine(this.BuildPolicyLine("PirateSmugglerFreighterLevel", (object)this.PirateSmugglerFreighterLevel));
                        streamWriter.WriteLine(this.BuildPolicyLine("PirateSmugglerMiningLevel", (object)this.PirateSmugglerMiningLevel));
                        streamWriter.WriteLine(this.BuildPolicyLine("PirateSmugglerPassengerLevel", (object)this.PirateSmugglerPassengerLevel));
                        streamWriter.WriteLine(this.BuildPolicyLine("CaptureEnlistMilitaryShip", (object)this.CaptureEnlistMilitaryShip));
                        streamWriter.WriteLine(this.BuildPolicyLine("CaptureDisassembleMilitaryShip", (object)this.CaptureDisassembleMilitaryShip));
                        streamWriter.WriteLine(this.BuildPolicyLine("CaptureEnlistCivilianShip", (object)this.CaptureEnlistCivilianShip));
                        streamWriter.WriteLine(this.BuildPolicyLine("CaptureDisassembleCivilianShip", (object)this.CaptureDisassembleCivilianShip));
                        streamWriter.WriteLine(this.BuildPolicyLine("CaptureEnlistBase", (object)this.CaptureEnlistBase));
                        streamWriter.WriteLine(this.BuildPolicyLine("UpgradeEnlistedMilitaryShips", (object)this.UpgradeEnlistedMilitaryShips));
                        streamWriter.WriteLine(this.BuildPolicyLine("UpgradeEnlistedCivilianShips", (object)this.UpgradeEnlistedCivilianShips));
                        streamWriter.WriteLine(this.BuildPolicyLine("TroopRecruitInfantryLevel", (object)this.TroopRecruitInfantryLevel));
                        streamWriter.WriteLine(this.BuildPolicyLine("TroopRecruitArmorLevel", (object)this.TroopRecruitArmorLevel));
                        streamWriter.WriteLine(this.BuildPolicyLine("TroopRecruitArtilleryLevel", (object)this.TroopRecruitArtilleryLevel));
                        streamWriter.WriteLine(this.BuildPolicyLine("TroopRecruitSpecialForcesLevel", (object)this.TroopRecruitSpecialForcesLevel));
                        streamWriter.WriteLine(this.BuildPolicyLine("TroopUseDefaultTransportLoadout", (object)this.TroopUseDefaultTransportLoadout));
                        streamWriter.WriteLine(this.BuildPolicyLine("TroopDefaultTransportLoadoutInfantry", (object)this.TroopDefaultTransportLoadoutInfantry));
                        streamWriter.WriteLine(this.BuildPolicyLine("TroopDefaultTransportLoadoutArmor", (object)this.TroopDefaultTransportLoadoutArmor));
                        streamWriter.WriteLine(this.BuildPolicyLine("TroopDefaultTransportLoadoutArtillery", (object)this.TroopDefaultTransportLoadoutArtillery));
                        streamWriter.WriteLine(this.BuildPolicyLine("TroopDefaultTransportLoadoutSpecialForces", (object)this.TroopDefaultTransportLoadoutSpecialForces));
                        streamWriter.WriteLine(this.BuildPolicyLine("TroopGarrisonMinimumPerColony", (object)this.TroopGarrisonMinimumPerColony));
                        streamWriter.WriteLine(this.BuildPolicyLine("TroopGarrisonLevel", (object)this.TroopGarrisonLevel));
                        streamWriter.WriteLine(this.BuildPolicyLine("UseExplorationShipsToScoutEnemySystems", (object)this.UseExplorationShipsToScoutEnemySystems));
                        streamWriter.WriteLine(this.BuildPolicyLine("BuildPlanetDestroyers", (object)this.BuildPlanetDestroyers));
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private string BuildPolicyLine(string name, object value)
        {
            string str = name + "\t\t;";
            switch (value)
            {
                case int num1:
                    str += num1.ToString((IFormatProvider)CultureInfo.InvariantCulture);
                    break;
                case string _:
                    str += (string)value;
                    break;
                case bool flag:
                    str = !flag ? str + "N" : str + "Y";
                    break;
                case float num2:
                    str += num2.ToString((IFormatProvider)CultureInfo.InvariantCulture);
                    break;
                case double num3:
                    str += num3.ToString((IFormatProvider)CultureInfo.InvariantCulture);
                    break;
                case ColonyPopulationPolicy populationPolicy:
                    str += ((int)populationPolicy).ToString();
                    break;
                case ComponentCategoryType componentCategoryType:
                    str += ((int)componentCategoryType).ToString();
                    break;
                case ShipDesignFocus shipDesignFocus:
                    str += ((int)shipDesignFocus).ToString();
                    break;
                case IndustryType industryType:
                    str += ((int)industryType).ToString();
                    break;
                case BuiltObjectFleeWhen builtObjectFleeWhen:
                    str += ((int)builtObjectFleeWhen).ToString();
                    break;
            }
            return str;
        }

        public EmpirePolicy Clone() => new EmpirePolicy()
        {
            ColonyActionForNewBuildDesign = this.ColonyActionForNewBuildDesign,
            ColonyActionForNewTroopRecruitment = this.ColonyActionForNewTroopRecruitment,
            ColonyAllowFacilityCloningFacility = this.ColonyAllowFacilityCloningFacility,
            ColonyAllowFacilityFortifiedBunker = this.ColonyAllowFacilityFortifiedBunker,
            ColonyAllowFacilityGiantIonCannon = this.ColonyAllowFacilityGiantIonCannon,
            ColonyAllowFacilityPlanetaryShield = this.ColonyAllowFacilityPlanetaryShield,
            ColonyAllowFacilityRegionalCapital = this.ColonyAllowFacilityRegionalCapital,
            ColonyAllowFacilityRoboticTroopFoundry = this.ColonyAllowFacilityRoboticTroopFoundry,
            ColonyAllowFacilityTerraformingFacility = this.ColonyAllowFacilityTerraformingFacility,
            ColonyAllowFacilityTroopTrainingCenter = this.ColonyAllowFacilityTroopTrainingCenter,
            ColonyAllowFacilityArmoredFactory = this.ColonyAllowFacilityArmoredFactory,
            ColonyAllowFacilitySpyAcademy = this.ColonyAllowFacilitySpyAcademy,
            ColonyAllowFacilityScienceAcademy = this.ColonyAllowFacilityScienceAcademy,
            ColonyAllowFacilityNavalAcademy = this.ColonyAllowFacilityNavalAcademy,
            ColonyAllowFacilityMilitaryAcademy = this.ColonyAllowFacilityMilitaryAcademy,
            ColonyFacilityPopulationThresholdCloningFacility = this.ColonyFacilityPopulationThresholdCloningFacility,
            ColonyFacilityPopulationThresholdFortifiedBunker = this.ColonyFacilityPopulationThresholdFortifiedBunker,
            ColonyFacilityPopulationThresholdGiantIonCannon = this.ColonyFacilityPopulationThresholdGiantIonCannon,
            ColonyFacilityPopulationThresholdPlanetaryShield = this.ColonyFacilityPopulationThresholdPlanetaryShield,
            ColonyFacilityPopulationThresholdRegionalCapital = this.ColonyFacilityPopulationThresholdRegionalCapital,
            ColonyFacilityPopulationThresholdRoboticTroopFoundry = this.ColonyFacilityPopulationThresholdRoboticTroopFoundry,
            ColonyFacilityPopulationThresholdTerraformingFacility = this.ColonyFacilityPopulationThresholdTerraformingFacility,
            ColonyFacilityPopulationThresholdTroopTrainingCenter = this.ColonyFacilityPopulationThresholdTroopTrainingCenter,
            ColonyFacilityPopulationThresholdArmoredFactory = this.ColonyFacilityPopulationThresholdArmoredFactory,
            ColonyFacilityPopulationThresholdSpyAcademy = this.ColonyFacilityPopulationThresholdSpyAcademy,
            ColonyFacilityPopulationThresholdScienceAcademy = this.ColonyFacilityPopulationThresholdScienceAcademy,
            ColonyFacilityPopulationThresholdNavalAcademy = this.ColonyFacilityPopulationThresholdNavalAcademy,
            ColonyFacilityPopulationThresholdMilitaryAcademy = this.ColonyFacilityPopulationThresholdMilitaryAcademy,
            ColonyPopulationThresholdTroopRecruitment = this.ColonyPopulationThresholdTroopRecruitment,
            ColonyTaxRateIncreaseWhenAtWar = this.ColonyTaxRateIncreaseWhenAtWar,
            ColonyTaxRateLargeColony = this.ColonyTaxRateLargeColony,
            ColonyTaxRateMediumColony = this.ColonyTaxRateMediumColony,
            ColonyTaxRateSmallColony = this.ColonyTaxRateSmallColony,
            ConstructionMilitary = this.ConstructionMilitary,
            ConstructionMilitaryCapitalShip = this.ConstructionMilitaryCapitalShip,
            ConstructionMilitaryCarrier = this.ConstructionMilitaryCarrier,
            ConstructionMilitaryCruiser = this.ConstructionMilitaryCruiser,
            ConstructionMilitaryDestroyer = this.ConstructionMilitaryDestroyer,
            ConstructionMilitaryEscort = this.ConstructionMilitaryEscort,
            ConstructionMilitaryFrigate = this.ConstructionMilitaryFrigate,
            ConstructionMilitaryTroopTransport = this.ConstructionMilitaryTroopTransport,
            ConstructionSpaceportLargeColonyPopulationThreshold = this.ConstructionSpaceportLargeColonyPopulationThreshold,
            ConstructionSpaceportMediumColonyPopulationThreshold = this.ConstructionSpaceportMediumColonyPopulationThreshold,
            ConstructionSpaceportMinimumDistance = this.ConstructionSpaceportMinimumDistance,
            ConstructionSpaceportSmallColonyPopulationThreshold = this.ConstructionSpaceportSmallColonyPopulationThreshold,
            DiplomacySendGiftsUpToAmount = this.DiplomacySendGiftsUpToAmount,
            DiplomacyTradeSanctionsUseBlockades = this.DiplomacyTradeSanctionsUseBlockades,
            FleetMilitaryProportionForFleets = this.FleetMilitaryProportionForFleets,
            FleetStrikeForceTypicalSize = this.FleetStrikeForceTypicalSize,
            FleetTypicalSize = this.FleetTypicalSize,
            IntelligenceAllowMissionDeepCover = this.IntelligenceAllowMissionDeepCover,
            IntelligenceAllowMissionInciteRevolution = this.IntelligenceAllowMissionInciteRevolution,
            IntelligenceAllowMissionSabotageColony = this.IntelligenceAllowMissionSabotageColony,
            IntelligenceAllowMissionSabotageConstruction = this.IntelligenceAllowMissionSabotageConstruction,
            IntelligenceAllowMissionStealGalaxyMap = this.IntelligenceAllowMissionStealGalaxyMap,
            IntelligenceAllowMissionStealOperationsMap = this.IntelligenceAllowMissionStealOperationsMap,
            IntelligenceAllowMissionStealTechData = this.IntelligenceAllowMissionStealTechData,
            IntelligenceAllowMissionStealTerritoryMap = this.IntelligenceAllowMissionStealTerritoryMap,
            IntelligenceAllowMissionAssassinateCharacter = this.IntelligenceAllowMissionAssassinateCharacter,
            IntelligenceAllowMissionDestroyBase = this.IntelligenceAllowMissionDestroyBase,
            IntelligenceCounterIntelligenceProportion = this.IntelligenceCounterIntelligenceProportion,
            IntelligenceUseEspionageAgainstEmpireWhen = this.IntelligenceUseEspionageAgainstEmpireWhen,
            IntelligenceUseSabotageAgainstEmpireWhen = this.IntelligenceUseSabotageAgainstEmpireWhen,
            ResearchDesignAutoRetrofit = this.ResearchDesignAutoRetrofit,
            ResearchDesignOverallFocus = this.ResearchDesignOverallFocus,
            ResearchDesignTechFocus1 = this.ResearchDesignTechFocus1,
            ResearchDesignTechFocus2 = this.ResearchDesignTechFocus2,
            ResearchDesignTechFocus3 = this.ResearchDesignTechFocus3,
            ResearchDesignTechFocus4 = this.ResearchDesignTechFocus4,
            ResearchDesignTechFocus5 = this.ResearchDesignTechFocus5,
            ResearchDesignTechFocus6 = this.ResearchDesignTechFocus6,
            WarAttacksAllowColonyBombardment = this.WarAttacksAllowColonyBombardment,
            WarAttacksAllowPlanetDestroying = this.WarAttacksAllowPlanetDestroying,
            WarAttacksHarassEnemies = this.WarAttacksHarassEnemies,
            ResearchDesignAutoUpgradeFighters = this.ResearchDesignAutoUpgradeFighters,
            TradeWithOtherEmpires = this.TradeWithOtherEmpires,
            EngageInTourism = this.EngageInTourism,
            NewColonyPopulationPolicyAllRaces = this.NewColonyPopulationPolicyAllRaces,
            NewColonyPopulationPolicyYourRaceFamily = this.NewColonyPopulationPolicyYourRaceFamily,
            ImplementEnslavementWithPenalColonies = this.ImplementEnslavementWithPenalColonies,
            HomeworldDefensePriority = this.HomeworldDefensePriority,
            ProtectLeaderAtAllCosts = this.ProtectLeaderAtAllCosts,
            PrioritizeBuildWonderId = this.PrioritizeBuildWonderId,
            ColonizeContinentalPriority = this.ColonizeContinentalPriority,
            ColonizeMarshySwampPriority = this.ColonizeMarshySwampPriority,
            ColonizeOceanPriority = this.ColonizeOceanPriority,
            ColonizeDesertPriority = this.ColonizeDesertPriority,
            ColonizeIcePriority = this.ColonizeIcePriority,
            ColonizeVolcanicPriority = this.ColonizeVolcanicPriority,
            ColonizeRuinsPriority = this.ColonizeRuinsPriority,
            ControlRestrictedResourcesPriority = this.ControlRestrictedResourcesPriority,
            ResearchIndustryFocus = this.ResearchIndustryFocus,
            ResearchPriority = this.ResearchPriority,
            TradePriority = this.TradePriority,
            AlliancePriority = this.AlliancePriority,
            SubjugationPriority = this.SubjugationPriority,
            TourismPriority = this.TourismPriority,
            ExplorationPriority = this.ExplorationPriority,
            WarWillingness = this.WarWillingness,
            BreakTreatyWillingness = this.BreakTreatyWillingness,
            InvasionOverkillFactor = this.InvasionOverkillFactor,
            ShipBattleCautionFactor = this.ShipBattleCautionFactor,
            DefaultMilitaryFleeWhen = this.DefaultMilitaryFleeWhen,
            DesignUpgradeEscort = this.DesignUpgradeEscort,
            DesignUpgradeFrigate = this.DesignUpgradeFrigate,
            DesignUpgradeDestroyer = this.DesignUpgradeDestroyer,
            DesignUpgradeCruiser = this.DesignUpgradeCruiser,
            DesignUpgradeCapitalShip = this.DesignUpgradeCapitalShip,
            DesignUpgradeTroopTransport = this.DesignUpgradeTroopTransport,
            DesignUpgradeCarrier = this.DesignUpgradeCarrier,
            DesignUpgradeResupplyShip = this.DesignUpgradeResupplyShip,
            DesignUpgradeExplorationShip = this.DesignUpgradeExplorationShip,
            DesignUpgradeColonyShip = this.DesignUpgradeColonyShip,
            DesignUpgradeConstructionShip = this.DesignUpgradeConstructionShip,
            DesignUpgradeOutpost = this.DesignUpgradeOutpost,
            DesignUpgradeSmallSpacePort = this.DesignUpgradeSmallSpacePort,
            DesignUpgradeMediumSpacePort = this.DesignUpgradeMediumSpacePort,
            DesignUpgradeLargeSpacePort = this.DesignUpgradeLargeSpacePort,
            DesignUpgradeResortBase = this.DesignUpgradeResortBase,
            DesignUpgradeGenericBase = this.DesignUpgradeGenericBase,
            DesignUpgradeEnergyResearchStation = this.DesignUpgradeEnergyResearchStation,
            DesignUpgradeWeaponsResearchStation = this.DesignUpgradeWeaponsResearchStation,
            DesignUpgradeHighTechResearchStation = this.DesignUpgradeHighTechResearchStation,
            DesignUpgradeMonitoringStation = this.DesignUpgradeMonitoringStation,
            DesignUpgradeDefensiveBase = this.DesignUpgradeDefensiveBase,
            DesignUpgradeSmallFreighter = this.DesignUpgradeSmallFreighter,
            DesignUpgradeMediumFreighter = this.DesignUpgradeMediumFreighter,
            DesignUpgradeLargeFreighter = this.DesignUpgradeLargeFreighter,
            DesignUpgradePassengerShip = this.DesignUpgradePassengerShip,
            DesignUpgradeGasMiningShip = this.DesignUpgradeGasMiningShip,
            DesignUpgradeMiningShip = this.DesignUpgradeMiningShip,
            DesignUpgradeGasMiningStation = this.DesignUpgradeGasMiningStation,
            DesignUpgradeMiningStation = this.DesignUpgradeMiningStation,
            CaptureTargetConditionShip = this.CaptureTargetConditionShip,
            CaptureTargetConditionBase = this.CaptureTargetConditionBase,
            OfferPirateAttackMissions = this.OfferPirateAttackMissions,
            BidOnPirateAttackMissions = this.BidOnPirateAttackMissions,
            BidOnPirateDefendMissions = this.BidOnPirateDefendMissions,
            OfferDefensivePirateMissions = this.OfferDefensivePirateMissions,
            OfferDefensivePirateMissionsSituation = this.OfferDefensivePirateMissionsSituation,
            AcceptPirateSmugglingMissions = this.AcceptPirateSmugglingMissions,
            OfferSmugglingPirateMissions = this.OfferSmugglingPirateMissions,
            PirateSmugglerFreighterLevel = this.PirateSmugglerFreighterLevel,
            PirateSmugglerMiningLevel = this.PirateSmugglerMiningLevel,
            PirateSmugglerPassengerLevel = this.PirateSmugglerPassengerLevel,
            CaptureEnlistMilitaryShip = this.CaptureEnlistMilitaryShip,
            CaptureDisassembleMilitaryShip = this.CaptureDisassembleMilitaryShip,
            CaptureEnlistCivilianShip = this.CaptureEnlistCivilianShip,
            CaptureDisassembleCivilianShip = this.CaptureDisassembleCivilianShip,
            CaptureEnlistBase = this.CaptureEnlistBase,
            UpgradeEnlistedMilitaryShips = this.UpgradeEnlistedMilitaryShips,
            UpgradeEnlistedCivilianShips = this.UpgradeEnlistedCivilianShips,
            TroopRecruitInfantryLevel = this.TroopRecruitInfantryLevel,
            TroopRecruitArmorLevel = this.TroopRecruitArmorLevel,
            TroopRecruitArtilleryLevel = this.TroopRecruitArtilleryLevel,
            TroopRecruitSpecialForcesLevel = this.TroopRecruitSpecialForcesLevel,
            TroopUseDefaultTransportLoadout = this.TroopUseDefaultTransportLoadout,
            TroopDefaultTransportLoadoutInfantry = this.TroopDefaultTransportLoadoutInfantry,
            TroopDefaultTransportLoadoutArmor = this.TroopDefaultTransportLoadoutArmor,
            TroopDefaultTransportLoadoutArtillery = this.TroopDefaultTransportLoadoutArtillery,
            TroopDefaultTransportLoadoutSpecialForces = this.TroopDefaultTransportLoadoutSpecialForces,
            TroopGarrisonMinimumPerColony = this.TroopGarrisonMinimumPerColony,
            TroopGarrisonLevel = this.TroopGarrisonLevel,
            UseExplorationShipsToScoutEnemySystems = this.UseExplorationShipsToScoutEnemySystems,
            BuildPlanetDestroyers = this.BuildPlanetDestroyers
        };
    }
}
