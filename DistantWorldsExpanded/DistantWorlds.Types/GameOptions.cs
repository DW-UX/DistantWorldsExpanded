// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GameOptions
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
    [Serializable]
    public class GameOptions
    {
        private double _SoundEffectsVolume;
        private double _MusicVolume;
        private int _MainViewScrollSpeed;
        private int _StarFieldSize;
        private bool _ShowSystemNebulae;
        private int _MainViewZoomSpeed;
        private bool _AutoPauseWhenInPopupWindow;
        public EmpirePolicy DefaultEmpirePolicy;
        [OptionalField]
        private StartGameOptions _StartGameOptions;
        [OptionalField]
        private int _AutoSaveInterval;
        private bool _ShowEncyclopediaAtStart;
        private int _MouseScrollWheelBehaviour;
        private int _MaximumFramerate = -1;
        private bool _ControlTroopRecruitmentDefault;
        private bool _ControlColonyTaxRatesDefault;
        private bool _ControlShipDesignDefault;
        private bool _ControlFleetFormationDefault;
        private bool _ControlFleetPosturesDefault;
        private bool _ControlResearchDefault;
        private bool _ControlPopulationPolicyDefault;
        private bool _ControlCharacterLocationsDefault;
        private AutomationLevel _ControlAgentAssignmentDefault;
        private AutomationLevel _ControlColonizationDefault;
        private AutomationLevel _ControlShipBuildingDefault;
        private AutomationLevel _ControlDiplomaticGiftsDefault;
        private AutomationLevel _ControlTreatyNegotiationDefault;
        private AutomationLevel _ControlWarTradeSanctionsDefault;
        private AutomationLevel _ControlAttacksOnEnemiesDefault;
        private AutomationLevel _ControlColonyFacilitiesDefault;
        [OptionalField]
        private AutomationLevel _ControlOfferPirateMissionsDefault = AutomationLevel.FullyAutomated;
        [OptionalField]
        private string _SaveGamePath = string.Empty;
        [OptionalField]
        public int AttackRangePatrol = 48000;
        [OptionalField]
        public int AttackRangeEscort = 2000;
        [OptionalField]
        public int AttackRangeOther = 48000;
        [OptionalField]
        public int AttackRangeAttack = 2000;
        [OptionalField]
        public float AttackOverMatchFactor = 2f;
        [OptionalField]
        public int AttackRangePatrolManual = -1;
        [OptionalField]
        public int AttackRangeEscortManual = -1;
        [OptionalField]
        public int AttackRangeOtherManual = -1;
        [OptionalField]
        public int AttackRangeAttackManual = -1;
        [OptionalField]
        public float FleetAttackRefuelPortion = 0.05f;
        [OptionalField]
        public float FleetAttackGatherPortion = 0.05f;
        [OptionalField]
        public bool GalaxyViewDisplayFleets = true;
        [OptionalField]
        public bool GalaxyViewDisplayResupplyShips = true;
        [OptionalField]
        public bool GalaxyViewDisplayMilitaryShips = true;
        [OptionalField]
        public bool GalaxyViewDisplaySpacePorts = true;
        [OptionalField]
        public bool GalaxyViewDisplayOtherBases = true;
        [OptionalField]
        public bool GalaxyViewDisplayExplorationShips = true;
        [OptionalField]
        public bool GalaxyViewDisplayColonyShips = true;
        [OptionalField]
        public bool GalaxyViewDisplayConstructionShips = true;
        [OptionalField]
        public bool GalaxyViewDisplayCivilianShips;
        [OptionalField]
        public bool GalaxyViewDisplayAlwaysEnemyFleets = true;
        [OptionalField]
        public bool GalaxyViewDisplayAlwaysEnemyMilitaryShips = true;
        [OptionalField]
        public bool GalaxyViewDisplayAlwaysPirates = true;
        [OptionalField]
        private string _DesignsPath = string.Empty;
        [OptionalField]
        private int _SystemNebulaeDetail;
        [OptionalField]
        private string _CustomizationSetName;
        [OptionalField]
        public int DiscoveryActionRuin;
        [OptionalField]
        public int DiscoveryActionAbandonedShipBase;
        [OptionalField]
        public bool LoadedGamesPaused = true;
        [OptionalField]
        public bool NewShipsAutomated = true;
        [OptionalField]
        public bool SuppressAllPopups;
        public bool MapOverlayFleetPostures = true;
        public bool MapOverlayTravelVectorsState;
        public bool MapOverlayTravelVectorsPrivate;
        public bool MapOverlayPotentialColonies;
        public bool MapOverlayScenicLocations;
        public bool MapOverlayResearchLocations;
        public bool MapOverlayLongRangeScanners = true;
        public bool MapOverlayEmpireTerritory;
        [OptionalField]
        public bool MapOverlayFadeCivilianShips;
        private bool _DisplayMessageBuiltObjectBuilt;
        private bool _DisplayMessageDiplomacyGift;
        private bool _DisplayMessageDiplomacyTreaty;
        private bool _DisplayMessageDiplomacyWarTradeSanctions;
        private bool _DisplayMessageDiplomacyEmpireMetDestroyed;
        private bool _DisplayMessageDiplomacyRequestWarning;
        private bool _DisplayMessageNewColony;
        private bool _DisplayMessageColonyInvaded;
        private bool _DisplayMessageResearchNewComponent;
        private bool _DisplayMessageIntelligenceMissions;
        private bool _DisplayMessageExploration;
        private bool _DisplayMessageShipMissionComplete;
        private bool _DisplayMessageShipNeedsRefuelling;
        [OptionalField]
        private bool _DisplayMessageConstructionResourceShortage = true;
        private bool _DisplayPopupBuiltObjectBuilt;
        private bool _DisplayPopupDiplomacyGift;
        private bool _DisplayPopupDiplomacyTreaty;
        private bool _DisplayPopupDiplomacyWarTradeSanctions;
        private bool _DisplayPopupDiplomacyEmpireMetDestroyed;
        private bool _DisplayPopupDiplomacyRequestWarning;
        private bool _DisplayPopupNewColony;
        private bool _DisplayPopupColonyInvaded;
        private bool _DisplayPopupResearchNewComponent;
        private bool _DisplayPopupIntelligenceMissions;
        private bool _DisplayPopupExploration;
        private bool _DisplayPopupShipMissionComplete;
        private bool _DisplayPopupShipNeedsRefuelling;
        [OptionalField]
        private bool _DisplayPopupConstructionResourceShortage = true;
        public bool DisplayMessageUnderAttackCivilianShips;
        public bool DisplayMessageUnderAttackCivilianBases;
        public bool DisplayMessageUnderAttackExplorationShips;
        public bool DisplayMessageUnderAttackColonyConstructionShips;
        public bool DisplayMessageUnderAttackMilitaryShips;
        public bool DisplayMessageUnderAttackOtherStateBases;
        public bool DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases;
        public bool DisplayPopupUnderAttackCivilianShips;
        public bool DisplayPopupUnderAttackCivilianBases;
        public bool DisplayPopupUnderAttackExplorationShips;
        public bool DisplayPopupUnderAttackColonyConstructionShips;
        public bool DisplayPopupUnderAttackMilitaryShips;
        public bool DisplayPopupUnderAttackOtherStateBases;
        public bool DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases;
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
        public bool CleanGalaxyView;
        [OptionalField]
        public int SelectionPanelSize;
        [OptionalField]
        public int EmpireNavigationToolSize;
        [OptionalField, DefaultValue(1.0)]
        private double _GuiScale = 1.0;

        public string SaveGamePath
        {
            get => this._SaveGamePath;
            set => this._SaveGamePath = value;
        }

        public int AutoSaveInterval
        {
            get => this._AutoSaveInterval;
            set => this._AutoSaveInterval = value;
        }

        public string DesignsPath
        {
            get => this._DesignsPath;
            set => this._DesignsPath = value;
        }

        public int SystemNebulaeDetail
        {
            get => this._SystemNebulaeDetail;
            set => this._SystemNebulaeDetail = value;
        }

        public string CustomizationSetName
        {
            get => this._CustomizationSetName;
            set => this._CustomizationSetName = value;
        }

        public StartGameOptions StartGameOptions
        {
            get => this._StartGameOptions;
            set => this._StartGameOptions = value;
        }

        public bool CompareAutomationEquality(GameOptions other) => this.ControlAgentAssignmentDefault == other.ControlAgentAssignmentDefault && this.ControlAttacksOnEnemiesDefault == other.ControlAttacksOnEnemiesDefault && this.ControlColonizationDefault == other.ControlColonizationDefault && this.ControlColonyTaxRatesDefault == other.ControlColonyTaxRatesDefault && this.ControlDiplomaticGiftsDefault == other.ControlDiplomaticGiftsDefault && this.ControlFleetFormationDefault == other.ControlFleetFormationDefault && this.ControlFleetPosturesDefault == other.ControlFleetPosturesDefault && this.ControlShipBuildingDefault == other.ControlShipBuildingDefault && this.ControlShipDesignDefault == other.ControlShipDesignDefault && this.ControlTreatyNegotiationDefault == other.ControlTreatyNegotiationDefault && this.ControlTroopRecruitmentDefault == other.ControlTroopRecruitmentDefault && this.ControlWarTradeSanctionsDefault == other.ControlWarTradeSanctionsDefault && this.ControlResearchDefault == other.ControlResearchDefault && this.ControlColonyFacilitiesDefault == other.ControlColonyFacilitiesDefault && this.ControlPopulationPolicyDefault == other.ControlPopulationPolicyDefault && this.ControlCharacterLocationsDefault == other.ControlCharacterLocationsDefault && this.ControlOfferPirateMissionsDefault == other.ControlOfferPirateMissionsDefault;

        public bool ControlTroopRecruitmentDefault
        {
            get => this._ControlTroopRecruitmentDefault;
            set => this._ControlTroopRecruitmentDefault = value;
        }

        public bool ControlCharacterLocationsDefault
        {
            get => this._ControlCharacterLocationsDefault;
            set => this._ControlCharacterLocationsDefault = value;
        }

        public AutomationLevel ControlAgentAssignmentDefault
        {
            get => this._ControlAgentAssignmentDefault;
            set => this._ControlAgentAssignmentDefault = value;
        }

        public bool ControlColonyTaxRatesDefault
        {
            get => this._ControlColonyTaxRatesDefault;
            set => this._ControlColonyTaxRatesDefault = value;
        }

        public bool ControlPopulationPolicyDefault
        {
            get => this._ControlPopulationPolicyDefault;
            set => this._ControlPopulationPolicyDefault = value;
        }

        public AutomationLevel ControlColonizationDefault
        {
            get => this._ControlColonizationDefault;
            set => this._ControlColonizationDefault = value;
        }

        public bool ControlShipDesignDefault
        {
            get => this._ControlShipDesignDefault;
            set => this._ControlShipDesignDefault = value;
        }

        public AutomationLevel ControlShipBuildingDefault
        {
            get => this._ControlShipBuildingDefault;
            set => this._ControlShipBuildingDefault = value;
        }

        public bool ControlFleetFormationDefault
        {
            get => this._ControlFleetFormationDefault;
            set => this._ControlFleetFormationDefault = value;
        }

        public bool ControlFleetPosturesDefault
        {
            get => this._ControlFleetPosturesDefault;
            set => this._ControlFleetPosturesDefault = value;
        }

        public bool ControlResearchDefault
        {
            get => this._ControlResearchDefault;
            set => this._ControlResearchDefault = value;
        }

        public AutomationLevel ControlDiplomaticGiftsDefault
        {
            get => this._ControlDiplomaticGiftsDefault;
            set => this._ControlDiplomaticGiftsDefault = value;
        }

        public AutomationLevel ControlTreatyNegotiationDefault
        {
            get => this._ControlTreatyNegotiationDefault;
            set => this._ControlTreatyNegotiationDefault = value;
        }

        public AutomationLevel ControlWarTradeSanctionsDefault
        {
            get => this._ControlWarTradeSanctionsDefault;
            set => this._ControlWarTradeSanctionsDefault = value;
        }

        public AutomationLevel ControlAttacksOnEnemiesDefault
        {
            get => this._ControlAttacksOnEnemiesDefault;
            set => this._ControlAttacksOnEnemiesDefault = value;
        }

        public AutomationLevel ControlColonyFacilitiesDefault
        {
            get => this._ControlColonyFacilitiesDefault;
            set => this._ControlColonyFacilitiesDefault = value;
        }

        public AutomationLevel ControlOfferPirateMissionsDefault
        {
            get => this._ControlOfferPirateMissionsDefault;
            set => this._ControlOfferPirateMissionsDefault = value;
        }

        public bool DisplayMessageIntelligenceMissions
        {
            get => this._DisplayMessageIntelligenceMissions;
            set => this._DisplayMessageIntelligenceMissions = value;
        }

        public bool DisplayMessageConstructionResourceShortage
        {
            get => this._DisplayMessageConstructionResourceShortage;
            set => this._DisplayMessageConstructionResourceShortage = value;
        }

        public bool DisplayMessageDiplomacyTreaty
        {
            get => this._DisplayMessageDiplomacyTreaty;
            set => this._DisplayMessageDiplomacyTreaty = value;
        }

        public bool DisplayMessageDiplomacyWarTradeSanctions
        {
            get => this._DisplayMessageDiplomacyWarTradeSanctions;
            set => this._DisplayMessageDiplomacyWarTradeSanctions = value;
        }

        public bool DisplayMessageDiplomacyEmpireMetDestroyed
        {
            get => this._DisplayMessageDiplomacyEmpireMetDestroyed;
            set => this._DisplayMessageDiplomacyEmpireMetDestroyed = value;
        }

        public bool DisplayMessageDiplomacyRequestWarning
        {
            get => this._DisplayMessageDiplomacyRequestWarning;
            set => this._DisplayMessageDiplomacyRequestWarning = value;
        }

        public bool DisplayMessageNewColony
        {
            get => this._DisplayMessageNewColony;
            set => this._DisplayMessageNewColony = value;
        }

        public bool DisplayMessageColonyInvaded
        {
            get => this._DisplayMessageColonyInvaded;
            set => this._DisplayMessageColonyInvaded = value;
        }

        public bool DisplayMessageResearchNewComponent
        {
            get => this._DisplayMessageResearchNewComponent;
            set => this._DisplayMessageResearchNewComponent = value;
        }

        public bool DisplayMessageBuiltObjectBuilt
        {
            get => this._DisplayMessageBuiltObjectBuilt;
            set => this._DisplayMessageBuiltObjectBuilt = value;
        }

        public bool DisplayMessageDiplomacyGift
        {
            get => this._DisplayMessageDiplomacyGift;
            set => this._DisplayMessageDiplomacyGift = value;
        }

        public bool DisplayMessageExploration
        {
            get => this._DisplayMessageExploration;
            set => this._DisplayMessageExploration = value;
        }

        public bool DisplayMessageShipMissionComplete
        {
            get => this._DisplayMessageShipMissionComplete;
            set => this._DisplayMessageShipMissionComplete = value;
        }

        public bool DisplayMessageShipNeedsRefuelling
        {
            get => this._DisplayMessageShipNeedsRefuelling;
            set => this._DisplayMessageShipNeedsRefuelling = value;
        }

        public bool DisplayPopupConstructionResourceShortage
        {
            get => this._DisplayPopupConstructionResourceShortage;
            set => this._DisplayPopupConstructionResourceShortage = value;
        }

        public bool DisplayPopupExploration
        {
            get => this._DisplayPopupExploration;
            set => this._DisplayPopupExploration = value;
        }

        public bool DisplayPopupIntelligenceMissions
        {
            get => this._DisplayPopupIntelligenceMissions;
            set => this._DisplayPopupIntelligenceMissions = value;
        }

        public bool DisplayPopupDiplomacyTreaty
        {
            get => this._DisplayPopupDiplomacyTreaty;
            set => this._DisplayPopupDiplomacyTreaty = value;
        }

        public bool DisplayPopupDiplomacyWarTradeSanctions
        {
            get => this._DisplayPopupDiplomacyWarTradeSanctions;
            set => this._DisplayPopupDiplomacyWarTradeSanctions = value;
        }

        public bool DisplayPopupDiplomacyEmpireMetDestroyed
        {
            get => this._DisplayPopupDiplomacyEmpireMetDestroyed;
            set => this._DisplayPopupDiplomacyEmpireMetDestroyed = value;
        }

        public bool DisplayPopupDiplomacyRequestWarning
        {
            get => this._DisplayPopupDiplomacyRequestWarning;
            set => this._DisplayPopupDiplomacyRequestWarning = value;
        }

        public bool DisplayPopupNewColony
        {
            get => this._DisplayPopupNewColony;
            set => this._DisplayPopupNewColony = value;
        }

        public bool DisplayPopupColonyInvaded
        {
            get => this._DisplayPopupColonyInvaded;
            set => this._DisplayPopupColonyInvaded = value;
        }

        public bool DisplayPopupResearchNewComponent
        {
            get => this._DisplayPopupResearchNewComponent;
            set => this._DisplayPopupResearchNewComponent = value;
        }

        public bool DisplayPopupBuiltObjectBuilt
        {
            get => this._DisplayPopupBuiltObjectBuilt;
            set => this._DisplayPopupBuiltObjectBuilt = value;
        }

        public bool DisplayPopupDiplomacyGift
        {
            get => this._DisplayPopupDiplomacyGift;
            set => this._DisplayPopupDiplomacyGift = value;
        }

        public bool DisplayPopupShipMissionComplete
        {
            get => this._DisplayPopupShipMissionComplete;
            set => this._DisplayPopupShipMissionComplete = value;
        }

        public bool DisplayPopupShipNeedsRefuelling
        {
            get => this._DisplayPopupShipNeedsRefuelling;
            set => this._DisplayPopupShipNeedsRefuelling = value;
        }

        public int StarFieldSize
        {
            get => this._StarFieldSize;
            set => this._StarFieldSize = value;
        }

        public double GuiScale
        {
            get => _GuiScale == 0 ? 1 : Math.Clamp(_GuiScale, 0.5, 4.0);
            set => this._GuiScale = value;
        }

        public double SoundEffectsVolume
        {
            get => this._SoundEffectsVolume;
            set => this._SoundEffectsVolume = value;
        }

        public double MusicVolume
        {
            get => this._MusicVolume;
            set => this._MusicVolume = value;
        }

        public int MainViewScrollSpeed
        {
            get => this._MainViewScrollSpeed;
            set => this._MainViewScrollSpeed = value;
        }

        public int MainViewZoomSpeed
        {
            get => this._MainViewZoomSpeed;
            set => this._MainViewZoomSpeed = value;
        }

        public bool AutoPauseWhenInPopupWindow
        {
            get => this._AutoPauseWhenInPopupWindow;
            set => this._AutoPauseWhenInPopupWindow = value;
        }

        public bool ShowSystemNebulae
        {
            get => this._ShowSystemNebulae;
            set => this._ShowSystemNebulae = value;
        }

        public bool ShowEncyclopediaAtStart
        {
            get => this._ShowEncyclopediaAtStart;
            set => this._ShowEncyclopediaAtStart = value;
        }

        public int MouseScrollWheelBehaviour
        {
            get => this._MouseScrollWheelBehaviour;
            set => this._MouseScrollWheelBehaviour = value;
        }

        public int MaximumFramerate
        {
            get => this._MaximumFramerate;
            set => this._MaximumFramerate = value;
        }
    }
}
