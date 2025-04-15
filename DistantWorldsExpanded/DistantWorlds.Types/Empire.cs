// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Empire
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

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
    [Serializable]
    public partial class Empire : IComparable<Empire>
    {
        public const double _MaxTaxRate = 0.75;

        public object _LockObject = new object();

        public Galaxy _Galaxy;

        private bool _Active;

        private int _EmpireId;

        private IMessageRecipient _MessageRecipient;

        private IAutomationAuthorizer _AutomationAuthorizer;

        private AutomationResponse _AutomationResponse;

        private IEventMessageRecipient _EventMessageRecipient;

        public string Description = string.Empty;

        public bool PlayableInScenario;

        public DesignList _Designs;

        private DesignList _ForeignDesigns;

        private Design _MonitoringStationCurrentDesign;

        private Design _WeaponsResearchStation;

        private Design _EnergyResearchStation;

        private Design _HighTechResearchStation;

        [OptionalField]
        private Design _DefenseBaseDesign;

        public DesignList _LatestDesigns;

        [NonSerialized]
        public double BuildFactor = 1.0;

        [OptionalField]
        public bool Reclusive;

        [OptionalField]
        public double CorruptionMultiplier = 1.0;

        [OptionalField]
        public double Corruption;

        public double ColonyRevenueDivisor = Galaxy.ColonyRevenueDivisor;

        private IntelligenceMissionList _IntelligenceMissions;

        private BuiltObjectList _Outlaws;

        private DiplomaticRelationList _ProposedDiplomaticRelations;

        private EmpireMessageList _Messages;

        private EmpireMessageList _MessageHistory = new EmpireMessageList();

        private int _MaximumHistoryMessages = 1000;

        private object _MessageHistoryLock = new object();

        private ResearchSystem _Research = new ResearchSystem();

        private GalaxyResourceMap _ResourceMap = new GalaxyResourceMap();

        private HabitatList _SystemsVisible = new HabitatList();

        private EmpireCounters _Counters;

        [OptionalField]
        public AchievementList Achievements = new AchievementList();

        private PirateEconomy _PirateEconomy;

        public RaceEventType RaceEventType;

        public long RaceEventEndDate;

        [NonSerialized]
        public object RaceEventData;

        private bool _EncounteredSilverMistCreature;

        private DistressSignalList _DistressSignals;

        [NonSerialized]
        private HabitatList _DangerousHabitats = new HabitatList();

        [NonSerialized]
        private HabitatPrioritizationList _ColonizationTargets = new HabitatPrioritizationList();

        [NonSerialized]
        private HabitatPrioritizationList _ResourceTargets = new HabitatPrioritizationList();

        [NonSerialized]
        private HabitatPrioritizationList _EmpireResourceTargets = new HabitatPrioritizationList();

        private HabitatPrioritizationList _IndependentColonyTargets = new HabitatPrioritizationList();

        private ResourceList _UnavailableLuxuryResources = new ResourceList();

        [OptionalField]
        private ResourceList _SelfSuppliedLuxuryResources = new ResourceList();

        [NonSerialized]
        private HabitatPrioritizationList _DesiredForeignColonies = new HabitatPrioritizationList();

        [NonSerialized]
        private EmpireList _EmpiresWithDesiredColonies = new EmpireList();

        private EmpireList _EmpiresToAttack = new EmpireList();

        private EmpireList _EmpiresViewable = new EmpireList();

        private List<long> _EmpiresViewableExpiry = new List<long>();

        private EmpireList _EmpiresSharedVisibility = new EmpireList();

        public SystemVisibilityList SystemVisibility = new SystemVisibilityList();

        private int _SystemExploredCount = 1;

        private int _ExplorationShipCount = 1;

        private BuiltObjectList _ResourceExtractors;

        private BuiltObjectList _Manufacturers;

        private BuiltObjectList _ConstructionYards;

        public BuiltObjectList SpacePorts;

        private BuiltObjectList _ConstructionShips;

        private BuiltObjectList _PlanetDestroyers;

        private BuiltObjectList _ResortBases;

        private BuiltObjectList _ResupplyShips;

        private BuiltObject _SystemScout;

        [OptionalField]
        private BuiltObjectList _SystemScouts = new BuiltObjectList();

        private ForceStructureProjectionList _StateForceStructureProjections;

        private ForceStructureProjectionList _PrivateForceStructureProjections;

        private double _ShortProcessingInterval = 3.0;

        private double _RegularProcessingInterval = 10.0;

        private double _PeriodicProcessingInterval = 30.0;

        private double _IntermediateProcessingInterval = 60.0;

        private double _LongProcessingInterval = 120.0;

        private double _HugeProcessingInterval = 240.0;

        private DateTime _LastShortTouch;

        private DateTime _LastRegularTouch;

        private DateTime _LastPeriodicTouch;

        private DateTime _LastIntermediateTouch;

        private DateTime _LastLongTouch;

        private DateTime _LastHugeTouch;

        private DeclinedTaskList _DeclinedTasks = new DeclinedTaskList();

        public int DiscoveryActionRuin;

        public int DiscoveryActionAbandonedShipBase;

        public bool NewShipsAutomated = true;

        public bool CanColonizeContinental;

        public bool CanColonizeMarshySwamp;

        public bool CanColonizeDesert;

        public bool CanColonizeOcean;

        public bool CanColonizeIce;

        public bool CanColonizeVolcanic;

        public float ColonyGrowthRateContinental = 1f;

        public float ColonyGrowthRateMarshySwamp = 1f;

        public float ColonyGrowthRateDesert = 1f;

        public float ColonyGrowthRateOcean = 1f;

        public float ColonyGrowthRateIce = 1f;

        public float ColonyGrowthRateVolcanic = 1f;

        private double _SpecialBonusResearchEnergy;

        private double _SpecialBonusResearchHighTech;

        private double _SpecialBonusResearchWeapons;

        private double _SpecialBonusWealth;

        private double _SpecialBonusHappiness;

        private double _SpecialBonusDiplomacy;

        private double _SpecialBonusPopulationGrowth;

        private Ruin _SpecialBonusResearchEnergyRuin;

        private Ruin _SpecialBonusResearchHighTechRuin;

        private Ruin _SpecialBonusResearchWeaponsRuin;

        private Ruin _SpecialBonusWealthRuin;

        private Ruin _SpecialBonusHappinessRuin;

        private Ruin _SpecialBonusDiplomacyRuin;

        private PlanetaryFacility _SpecialBonusResearchEnergyWonder;

        private PlanetaryFacility _SpecialBonusResearchHighTechWonder;

        private PlanetaryFacility _SpecialBonusResearchWeaponsWonder;

        private PlanetaryFacility _SpecialBonusWealthWonder;

        private PlanetaryFacility _SpecialBonusHappinessWonder;

        private PlanetaryFacility _SpecialBonusPopulationGrowthWonder;

        [NonSerialized]
        private HabitatList _MonitoringHabitats = new HabitatList();

        [NonSerialized]
        private List<Point> _MonitoringPoints = new List<Point>();

        [NonSerialized]
        private HabitatList _ResearchHabitats = new HabitatList();

        [NonSerialized]
        private PrioritizedTargetList _ResortHabitats = new PrioritizedTargetList();

        [NonSerialized]
        private StellarObjectList _RefuellingLocations = new StellarObjectList();

        [NonSerialized]
        private StellarObjectList _RefuellingLocationsMilitaryOnly = new StellarObjectList();

        [OptionalField]
        public int EmpireSplitCount;

        [OptionalField]
        public bool HaveDefeatedAncientGuardians;

        [OptionalField]
        public bool HaveDefeatedShakturi;

        [OptionalField]
        public int DefeatedLegendaryPiratesCount;

        [OptionalField]
        public HabitatList ColoniesNeedingTroops = new HabitatList();

        private HabitatList _PenalColonies = new HabitatList();

        [NonSerialized]
        private PrioritizedTargetList _ResettleSources = new PrioritizedTargetList();

        [NonSerialized]
        private PrioritizedTargetList _MigrationSources = new PrioritizedTargetList();

        [NonSerialized]
        private PrioritizedTargetList _MigrationDestinations = new PrioritizedTargetList();

        [NonSerialized]
        private PrioritizedTargetList _TourismSources = new PrioritizedTargetList();

        [NonSerialized]
        private PrioritizedTargetList _TourismDestinations = new PrioritizedTargetList();

        [NonSerialized]
        private PrioritizedTargetList _ResortBaseBuildLocations = new PrioritizedTargetList();

        public BuiltObjectList DisputedBases = new BuiltObjectList();

        private EmpireList _RecentAttackingEmpires = new EmpireList();

        private EmpireList _RecentSpyingEmpires = new EmpireList();

        private GalaxyLocationList _KnownGalaxyLocations = new GalaxyLocationList();

        private List<Point> _LocationHints = new List<Point>();

        public object LocationHintLock = new object();

        private bool _InitiateConstruction = true;

        public Character VictoryLoseLeader;

        public Habitat VictoryLoseHomeworld;

        public float VictoryBonus;

        [OptionalField]
        public long LastDisasterDate = long.MinValue;

        [OptionalField]
        public long LastLeaderChangeDate = long.MinValue;

        private string _Name;

        private int _Score;

        private double _StateMoney;

        private double _PrivateMoney;

        private int _IntelligenceSpending;

        private int _CounterEspionageSpending;

        private int _CounterSabotageSpending;

        public int AttackRangePatrol = 48000;

        public int AttackRangeEscort = 2000;

        public int AttackRangeOther = 48000;

        [OptionalField]
        public int AttackRangeAttack = 2000;

        public float AttackOvermatchFactor = 2f;

        [OptionalField]
        public float FleetAttackRefuelPortion = 0.3f;

        [OptionalField]
        public float FleetAttackGatherPortion = 0.3f;

        [OptionalField]
        public int AttackRangePatrolManual = -1;

        [OptionalField]
        public int AttackRangeEscortManual = -1;

        [OptionalField]
        public int AttackRangeOtherManual = -1;

        [OptionalField]
        public int AttackRangeAttackManual = -1;

        private double _ShipMaintenanceSavings;

        private double _ResourceExtractionBonus;

        private double _ResearchBonus;

        private double _EspionageBonus;

        private double _TradeBonus;

        private double _RelativeEmpireSize;

        [OptionalField]
        public PlanetaryFacilityBuildDateList TrackedWonders = new PlanetaryFacilityBuildDateList();

        public double DifficultyLevel = 1.0;

        public double ColonyCorruptionFactor = Galaxy.ColonyCorruptionFactorDefault;

        public double ResearchRate = Galaxy.ResearchRateDefault;

        public double PopulationGrowthRate = Galaxy.PopulationGrowthRateDefault;

        public double MiningRate = Galaxy.MiningRateDefault;

        public double TargettingFactor = Galaxy.TargettingFactorDefault;

        public double CountermeasuresFactor = Galaxy.CountermeasuresFactorDefault;

        public double ColonyShipBuildSpeedRate = Galaxy.ColonyShipBuildSpeedRateDefault;

        public double WarWearinessFactor = Galaxy.WarWearinessFactorDefault;

        public double ColonyIncomeFactor = Galaxy.ColonyIncomeFactorDefault;

        [OptionalField]
        public double SmugglingIncomeFactor = 1.0;

        [OptionalField]
        public double RaidStrengthFactor = 1.0;

        [OptionalField]
        public double RaidBonusFactor = 1.0;

        [OptionalField]
        public double ShipMaintenancePrivateFactor = 1.0;

        [OptionalField]
        public double ShipMaintenanceStateFactor = 1.0;

        [OptionalField]
        public double ResearchWeaponsFactor = 1.0;

        [OptionalField]
        public double ResearchEnergyFactor = 1.0;

        [OptionalField]
        public double ResearchHighTechFactor = 1.0;

        [OptionalField]
        public double PlanetaryFacilityEliminationFactor = 1.0;

        [OptionalField]
        public double LootingFactor = 1.0;

        [OptionalField]
        public double PlanetaryFacilityBuildFactor = 1.0;

        [OptionalField]
        public double PlanetaryWonderBuildFactor = 1.0;

        [OptionalField]
        public double DifficultyLevelModifier;

        public PiratePlayStyle PiratePlayStyle;

        private Race _ShipMaintenanceSavingsRace;

        private Race _ResourceExtractionBonusRace;

        private Race _ResearchBonusRace;

        private Race _EspionageBonusRace;

        private Race _TradeBonusRace;

        [OptionalField]
        private int _EmpireOrderCount;

        private BuiltObjectList _KnownPirateBases = new BuiltObjectList();

        private EmpireList _KnownPirateEmpires = new EmpireList();

        private Habitat _PirateEmpireBaseHabitat;

        private EmpireActivityList _PirateMissions = new EmpireActivityList();

        public bool PirateEmpireSuperPirates;

        public List<int> PirateInfluenceSystemIds = new List<int>();

        public PirateRelationList PirateRelations = new PirateRelationList();

        private int _GovernmentId;

        private GovernmentAttributes _GovernmentAttributes;

        private List<int> _AllowableGovernmentTypes;

        private double _EconomyEfficiency = 1.0;

        private double _TaxRate;

        private Race _DominantRace;

        private int _DesignPictureFamilyIndex;

        private long _TotalPopulation;

        private Habitat _Capital;

        public HabitatList Capitals = new HabitatList();

        public HabitatList CapitalSystemStars = new HabitatList();

        public Habitat HomeWorld;

        public Habitat TargetHabitat;

        public Habitat DefendHabitat;

        private TroopList _Troops;

        private string _TroopDescription;

        private int _TroopPictureRef;

        private int _TroopCount;

        private Empire _TopCompetitor;

        private bool _ControlDesigns;

        private bool _ControlTroopGeneration;

        private bool _ControlMilitaryFleets;

        private bool _ControlColonyDevelopment;

        private bool _ControlColonyTaxRates;

        private bool _ControlColonyStockLevels;

        private bool _ControlResearch;

        private bool _ControlPopulationPolicy;

        [OptionalField]
        private bool _ControlCharacterLocations = true;

        private AutomationLevel _ControlColonization;

        private AutomationLevel _ControlStateConstruction;

        private AutomationLevel _ControlMilitaryAttacks;

        private AutomationLevel _ControlDiplomacyGifts;

        private AutomationLevel _ControlDiplomacyTreaties;

        private AutomationLevel _ControlDiplomacyOffense;

        private AutomationLevel _ControlAgentAssignment;

        private AutomationLevel _ControlColonyFacilities;

        [OptionalField]
        private AutomationLevel _ControlOfferPirateMissions = AutomationLevel.FullyAutomated;

        public EmpirePolicy Policy = new EmpirePolicy();

        [OptionalField]
        public bool AutoRefuelStateShips = true;

        private int _FleetIdentity;

        private double _CivilityRating;

        private double _WarWeariness;

        private Empire _ParentEmpire;

        private bool _LetOthersProvideFreight;

        private Color _MainColor;

        private Color _SecondaryColor;

        private int _FlagShape;

        private Bitmap _LargeFlagPicture;

        private Bitmap _SmallFlagPicture;

        private int _DesignNamesIndex;

        private int[] _DesignNamesUsage = new int[36];

        private string _SmallFreighterPrefix;

        private string _MediumFreighterPrefix;

        private string _LargeFreighterPrefix;

        private string _MiningShipPrefix;

        private string _GasMiningShipPrefix;

        private string _EscortPrefix;

        private string _FrigatePrefix;

        private string _DestroyerPrefix;

        private string _CruiserPrefix;

        private string _CapitalShipPrefix;

        private string _TroopTransportPrefix;

        private int _SmallFreighterCurrentModelNumber = 1000;

        private int _MediumFreighterCurrentModelNumber = 1000;

        private int _LargeFreighterCurrentModelNumber = 1000;

        private int _MiningShipCurrentModelNumber = 1000;

        private int _GasMiningShipCurrentModelNumber = 1000;

        private int _ResortBaseCurrentModelNumber;

        [OptionalField]
        private int _EnergyResearchStationCurrentModelNumber;

        [OptionalField]
        private int _WeaponsResearchStationCurrentModelNumber;

        [OptionalField]
        private int _HighTechResearchStationCurrentModelNumber;

        [OptionalField]
        private int _MonitoringStationCurrentModelNumber;

        [OptionalField]
        private int _DefensiveBaseCurrentModelNumber;

        private int _SmallSpacePortCurrentModelNumber;

        [OptionalField]
        private int _OutpostCurrentModelNumber;

        private int _MediumSpacePortCurrentModelNumber;

        private int _LargeSpacePortCurrentModelNumber;

        private int _MiningStationCurrentModelNumber;

        private int _GasMiningStationCurrentModelNumber;

        private int _ConstructionShipCurrentModelNumber;

        private int _ExplorationShipCurrentModelNumber;

        private int _PassengerShipCurrentModelNumber;

        private int _ColonyShipCurrentModelNumber;

        private int _EscortCurrentModelNumber;

        private int _FrigateCurrentModelNumber;

        private int _DestroyerCurrentModelNumber;

        private int _CruiserCurrentModelNumber;

        private int _CapitalShipCurrentModelNumber;

        private int _TroopTransportCurrentModelNumber;

        private int _ResupplyShipCurrentModelNumber;

        private int _CarrierCurrentModelNumber;

        private string _SmallFreighterName;

        private string _MediumFreighterName;

        private string _LargeFreighterName;

        private string _MiningShipName;

        private string _GasMiningShipName;

        private Number[] _RomanNumbers = new Number[13]
        {
        new Number(1000, "M"),
        new Number(900, "CM"),
        new Number(500, "D"),
        new Number(400, "CD"),
        new Number(100, "C"),
        new Number(90, "XC"),
        new Number(50, "L"),
        new Number(40, "XL"),
        new Number(10, "X"),
        new Number(9, "IX"),
        new Number(5, "V"),
        new Number(4, "IV"),
        new Number(1, "I")
        };

        [NonSerialized]
        private StringBuilder _StringBuilder = new StringBuilder();

        [OptionalField]
        public DesignSpecification PlanetDestroyerDesignSpecification;

        public DesignSpecificationList _DesignSpecifications = new DesignSpecificationList();

        public EmpireEvaluationList EmpireEvaluations = new EmpireEvaluationList();

        [OptionalField]
        private bool _ReviewDesignsAndRetrofit;

        [OptionalField]
        private bool _ReviewDesignsAndRetrofitImportantBreakthrough;

        private int _TaskSkipCount;

        [NonSerialized]
        public volatile bool FuelSystemsUpdating;

        [NonSerialized]
        public List<FuelSourceSystemList> FuelSystemsSources = new List<FuelSourceSystemList>();

        private int _BaseMaximumConstructionSize = 160;

        public bool TroopCanRecruitInfantry;

        public bool TroopCanRecruitArmored;

        public bool TroopCanRecruitArtillery;

        public bool TroopCanRecruitSpecialForces;

        public float TroopAttackStrengthBonusFactorInfantry = 1f;

        public float TroopAttackStrengthBonusFactorArmored = 1f;

        public float TroopAttackStrengthBonusFactorArtillery = 1f;

        public float TroopAttackStrengthBonusFactorSpecialForces = 1f;

        public float TroopDefendStrengthBonusFactorInfantry = 1f;

        public float TroopDefendStrengthBonusFactorArmored = 1f;

        public float TroopDefendStrengthBonusFactorSpecialForces = 1f;

        [OptionalField]
        public float TroopPlanetaryDefenseInterceptBonusFactor = 1f;

        public float TroopMaintenanceFactor = 1f;

        public float BoardingAttackFactor = 1f;

        public float BoardingDefenseFactor = 1f;

        private bool _CanBuildCarriers;

        private bool _CanBuildResupplyShips;

        public float ResearchBonusWeapons;

        public BuiltObject ResearchBonusWeaponsStation;

        public float ResearchBonusEnergy;

        public BuiltObject ResearchBonusEnergyStation;

        public float ResearchBonusHighTech;

        public BuiltObject ResearchBonusHighTechStation;

        [OptionalField]
        private double _TaxResistanceRate = 1.05;

        [OptionalField]
        private long _TaxResistanceThreshold = 4000000000L;

        private long _LastResortIncomeAddDate;

        private double _ThisYearsResortIncome;

        [OptionalField]
        private List<double> _VariableIncome = new List<double>();

        [OptionalField]
        private long _LastVariableIncomeUpdate;

        [OptionalField]
        private bool _UseAveragedVariableIncome;

        [OptionalField]
        private long _DateOfLastStateFuelCost;

        [OptionalField]
        private double _ThisYearsStateFuelCosts;

        [OptionalField]
        private double _ThisYearsPrivateFuelCosts;

        [OptionalField]
        private long _DateOfLastPrivateFuelCost;

        [OptionalField]
        public bool PreWarpProgressEventOccurredSendPirateRaid;

        [OptionalField]
        public bool PreWarpProgressEventOccurredExperienceFirstPirateRaid;

        public bool PreWarpProgressEventOccurredFirstContactPirateOrIndependent;

        public bool PreWarpProgressEventOccurredFirstContactNormalEmpire;

        public bool PreWarpProgressEventOccurredBuildFirstShip;

        public bool PreWarpProgressEventOccurredBuildFirstSpaceport;

        public bool PreWarpProgressEventOccurredBuildFirstMiningStation;

        public bool PreWarpProgressEventOccurredBuildFirstResearchStation;

        public bool PreWarpProgressEventOccurredDiscoverHyperspaceTech;

        public bool PreWarpProgressEventOccurredDiscoverColonizationTech;

        public bool PreWarpProgressEventOccurredFirstHyperjump;

        public bool PreWarpProgressEventOccurredEncounterFirstKaltor;

        [OptionalField]
        public bool PreWarpProgressEventOccurredBuildFirstMilitaryShip;

        [OptionalField]
        public bool PirateExtortionOfferMade;

        public bool[] _ComponentsAvailable = new bool[Enum.GetValues(typeof(BuiltObjectSubRole)).Length];

        public BuiltObjectList PrivateBuiltObjects = new BuiltObjectList();

        public BuiltObjectList BuiltObjects = new BuiltObjectList();

        public BuiltObjectList LongRangeScanners = new BuiltObjectList();

        public BuiltObjectList ResearchFacilities = new BuiltObjectList();

        public BuiltObjectList RefuellingDepots = new BuiltObjectList();

        public BuiltObjectList MiningStations = new BuiltObjectList();

        public BuiltObjectList Freighters = new BuiltObjectList();

        public CharacterList AvailableCharacters = new CharacterList();

        public CharacterList Characters = new CharacterList();

        public Character Leader;

        private double _LeaderChangeInfluence;

        public DiplomaticRelationList DiplomaticRelations = new DiplomaticRelationList();

        [OptionalField]
        public Bitmap MediumFlagPicture;

        public HabitatList Colonies = new HabitatList();

        public ShipGroupList ShipGroups = new ShipGroupList();

        public FleetAttackList IncomingEnemyFleetsAndPlanetDestroyers = new FleetAttackList();

        public DateTime LastXaraktorVirusDeploy = DateTime.MinValue;

        internal Galaxy Galaxy => _Galaxy;

        public bool Active => _Active;

        public int EmpireId => _EmpireId;

        public Design DefenseBaseDesign => _DefenseBaseDesign;

        public Design WeaponsResearchStation => _WeaponsResearchStation;

        public Design EnergyResearchStation => _EnergyResearchStation;

        public Design HighTechResearchStation => _HighTechResearchStation;

        public EmpireCounters Counters => _Counters;

        public PirateEconomy PirateEconomy => _PirateEconomy;

        public bool EncounteredSilverMistCreature
        {
            get
            {
                return _EncounteredSilverMistCreature;
            }
            set
            {
                _EncounteredSilverMistCreature = value;
            }
        }

        public EmpireList EmpiresSharedVisibility => _EmpiresSharedVisibility;

        public ResourceList SelfSuppliedLuxuryResources => _SelfSuppliedLuxuryResources;

        public HabitatPrioritizationList EmpireResourceTargets
        {
            get
            {
                return _EmpireResourceTargets;
            }
            set
            {
                _EmpireResourceTargets = value;
            }
        }

        public HabitatPrioritizationList DesiredForeignColonies
        {
            get
            {
                return _DesiredForeignColonies;
            }
            set
            {
                _DesiredForeignColonies = value;
            }
        }

        public EmpireList EmpiresViewable => _EmpiresViewable;

        public List<long> EmpiresViewableExpiry => _EmpiresViewableExpiry;

        public HabitatList SystemsVisible => _SystemsVisible;

        public DeclinedTaskList DeclinedTasks => _DeclinedTasks;

        public PlanetaryFacility SpecialBonusResearchEnergyWonder => _SpecialBonusResearchEnergyWonder;

        public PlanetaryFacility SpecialBonusResearchHighTechWonder => _SpecialBonusResearchHighTechWonder;

        public PlanetaryFacility SpecialBonusResearchWeaponsWonder => _SpecialBonusResearchWeaponsWonder;

        public PlanetaryFacility SpecialBonusWealthWonder => _SpecialBonusWealthWonder;

        public PlanetaryFacility SpecialBonusHappinessWonder => _SpecialBonusHappinessWonder;

        public PlanetaryFacility SpecialBonusPopulationGrowthWonder => _SpecialBonusPopulationGrowthWonder;

        public Ruin SpecialBonusResearchEnergyRuin => _SpecialBonusResearchEnergyRuin;

        public Ruin SpecialBonusResearchHighTechRuin => _SpecialBonusResearchHighTechRuin;

        public Ruin SpecialBonusResearchWeaponsRuin => _SpecialBonusResearchWeaponsRuin;

        public Ruin SpecialBonusWealthRuin => _SpecialBonusWealthRuin;

        public Ruin SpecialBonusHappinessRuin => _SpecialBonusHappinessRuin;

        public Ruin SpecialBonusDiplomacyRuin => _SpecialBonusDiplomacyRuin;

        public double SpecialBonusResearchEnergy => _SpecialBonusResearchEnergy;

        public double SpecialBonusResearchHighTech => _SpecialBonusResearchHighTech;

        public double SpecialBonusResearchWeapons => _SpecialBonusResearchWeapons;

        public double SpecialBonusWealth => _SpecialBonusWealth;

        public double SpecialBonusHappiness => _SpecialBonusHappiness;

        public double SpecialBonusDiplomacy => _SpecialBonusDiplomacy;

        public double SpecialBonusPopulationGrowth => _SpecialBonusPopulationGrowth;

        public PrioritizedTargetList ResortHabitats => _ResortHabitats;

        public HabitatList PenalColonies => _PenalColonies;

        public PrioritizedTargetList ResettleSources => _ResettleSources;

        public PrioritizedTargetList MigrationSources => _MigrationSources;

        public PrioritizedTargetList MigrationDestinations => _MigrationDestinations;

        public PrioritizedTargetList TourismSources => _TourismSources;

        public PrioritizedTargetList TourismDestinations => _TourismDestinations;

        public PrioritizedTargetList ResortBaseBuildLocations => _ResortBaseBuildLocations;

        public HabitatList ResearchHabitats => _ResearchHabitats;

        public EmpireList RecentAttackingEmpires
        {
            get
            {
                return _RecentAttackingEmpires;
            }
            set
            {
                _RecentAttackingEmpires = value;
            }
        }

        public EmpireList RecentSpyingEmpires
        {
            get
            {
                return _RecentSpyingEmpires;
            }
            set
            {
                _RecentSpyingEmpires = value;
            }
        }

        public GalaxyLocationList KnownGalaxyLocations => _KnownGalaxyLocations;

        public List<Point> LocationHints => _LocationHints;

        public double RelativeEmpireSize => _RelativeEmpireSize;

        public double ShipMaintenanceSavings => _ShipMaintenanceSavings;

        public double ResourceExtractionBonus => _ResourceExtractionBonus * BaconEmpire.MultiplyResourceExtractionBonus(this);

        public double ResearchBonus => _ResearchBonus;

        public double EspionageBonus => _EspionageBonus;

        public double TradeBonus => _TradeBonus;

        public Race ResearchBonusRace => _ResearchBonusRace;

        public EmpireList KnownPirateEmpires => _KnownPirateEmpires;

        public BuiltObjectList KnownPirateBases => _KnownPirateBases;

        public EmpireActivityList PirateMissions
        {
            get
            {
                return _PirateMissions;
            }
            set
            {
                _PirateMissions = value;
            }
        }

        public Habitat PirateEmpireBaseHabitat
        {
            get
            {
                return _PirateEmpireBaseHabitat;
            }
            set
            {
                _PirateEmpireBaseHabitat = value;
            }
        }

        public double EconomyEfficiency => _EconomyEfficiency;

        public int DesignPictureFamilyIndex
        {
            get
            {
                return _DesignPictureFamilyIndex;
            }
            set
            {
                _DesignPictureFamilyIndex = value;
            }
        }

        public List<int> AllowableGovernmentTypes => _AllowableGovernmentTypes;

        public Design MonitoringStationCurrentDesign => _MonitoringStationCurrentDesign;

        public AutomationLevel ControlAgentAssignment
        {
            get
            {
                return _ControlAgentAssignment;
            }
            set
            {
                _ControlAgentAssignment = value;
            }
        }

        public AutomationLevel ControlColonization
        {
            get
            {
                return _ControlColonization;
            }
            set
            {
                _ControlColonization = value;
            }
        }

        public bool ControlDesigns
        {
            get
            {
                return _ControlDesigns;
            }
            set
            {
                _ControlDesigns = value;
            }
        }

        public AutomationLevel ControlStateConstruction
        {
            get
            {
                return _ControlStateConstruction;
            }
            set
            {
                _ControlStateConstruction = value;
            }
        }

        public bool ControlTroopGeneration
        {
            get
            {
                return _ControlTroopGeneration;
            }
            set
            {
                _ControlTroopGeneration = value;
            }
        }

        public bool ControlCharacterLocations
        {
            get
            {
                return _ControlCharacterLocations;
            }
            set
            {
                _ControlCharacterLocations = value;
            }
        }

        public AutomationLevel ControlMilitaryAttacks
        {
            get
            {
                return _ControlMilitaryAttacks;
            }
            set
            {
                _ControlMilitaryAttacks = value;
            }
        }

        public bool ControlMilitaryFleets
        {
            get
            {
                return _ControlMilitaryFleets;
            }
            set
            {
                _ControlMilitaryFleets = value;
            }
        }

        public bool ControlColonyDevelopment
        {
            get
            {
                return _ControlColonyDevelopment;
            }
            set
            {
                _ControlColonyDevelopment = value;
            }
        }

        public bool ControlColonyTaxRates
        {
            get
            {
                return _ControlColonyTaxRates;
            }
            set
            {
                _ControlColonyTaxRates = value;
            }
        }

        public bool ControlColonyStockLevels
        {
            get
            {
                return _ControlColonyStockLevels;
            }
            set
            {
                _ControlColonyStockLevels = value;
            }
        }

        public AutomationLevel ControlDiplomacyGifts
        {
            get
            {
                return _ControlDiplomacyGifts;
            }
            set
            {
                _ControlDiplomacyGifts = value;
            }
        }

        public AutomationLevel ControlDiplomacyTreaties
        {
            get
            {
                return _ControlDiplomacyTreaties;
            }
            set
            {
                _ControlDiplomacyTreaties = value;
            }
        }

        public AutomationLevel ControlDiplomacyOffense
        {
            get
            {
                return _ControlDiplomacyOffense;
            }
            set
            {
                _ControlDiplomacyOffense = value;
            }
        }

        public bool ControlResearch
        {
            get
            {
                return _ControlResearch;
            }
            set
            {
                _ControlResearch = value;
            }
        }

        public bool ControlPopulationPolicy
        {
            get
            {
                return _ControlPopulationPolicy;
            }
            set
            {
                _ControlPopulationPolicy = value;
            }
        }

        public AutomationLevel ControlColonyFacilities
        {
            get
            {
                return _ControlColonyFacilities;
            }
            set
            {
                _ControlColonyFacilities = value;
            }
        }

        public AutomationLevel ControlOfferPirateMissions
        {
            get
            {
                return _ControlOfferPirateMissions;
            }
            set
            {
                _ControlOfferPirateMissions = value;
            }
        }

        public double LongProcessingInterval => _LongProcessingInterval;

        public double PeriodicProcessingInterval => _PeriodicProcessingInterval;

        public DateTime LastShortTouch
        {
            get
            {
                return _LastShortTouch;
            }
            set
            {
                _LastShortTouch = value;
            }
        }

        public DateTime LastRegularTouch
        {
            get
            {
                return _LastRegularTouch;
            }
            set
            {
                _LastRegularTouch = value;
            }
        }

        public DateTime LastPeriodicTouch
        {
            get
            {
                return _LastPeriodicTouch;
            }
            set
            {
                _LastPeriodicTouch = value;
            }
        }

        public DateTime LastIntermediateTouch
        {
            get
            {
                return _LastIntermediateTouch;
            }
            set
            {
                _LastIntermediateTouch = value;
            }
        }

        public DateTime LastLongTouch
        {
            get
            {
                return _LastLongTouch;
            }
            set
            {
                _LastLongTouch = value;
            }
        }

        public DateTime LastHugeTouch
        {
            get
            {
                return _LastHugeTouch;
            }
            set
            {
                _LastHugeTouch = value;
            }
        }

        public HabitatList MonitoringHabitats => _MonitoringHabitats;

        public List<Point> MonitoringPoints => _MonitoringPoints;

        public HabitatPrioritizationList ResourceTargets
        {
            get
            {
                return _ResourceTargets;
            }
            set
            {
                _ResourceTargets = value;
            }
        }

        public ForceStructureProjectionList StateForceStructureProjections => _StateForceStructureProjections;

        public ForceStructureProjectionList PrivateForceStructureProjections => _PrivateForceStructureProjections;

        public double WarWeariness
        {
            get
            {
                double num = 1.0;
                if (Leader != null)
                {
                    num = 1.0 + (double)Leader.WarWeariness / 100.0;
                }
                return _WarWeariness / num;
            }
        }

        public double WarWearinessRaw
        {
            get
            {
                return _WarWeariness;
            }
            set
            {
                _WarWeariness = value;
            }
        }

        public double CivilityRating
        {
            get
            {
                return _CivilityRating;
            }
            set
            {
                _CivilityRating = value;
                if (_CivilityRating > 30.0)
                {
                    _CivilityRating = 30.0;
                }
                if (_CivilityRating < -100.0)
                {
                    _CivilityRating = -100.0;
                }
            }
        }

        public double CivilityRatingApprovalRaw
        {
            get
            {
                double num = 0.0;
                if (this != _Galaxy.IndependentEmpire)
                {
                    num = CivilityRating / 5.0;
                }
                if (GovernmentAttributes != null && num < 0.0)
                {
                    num *= GovernmentAttributes.ConcernForOwnReputation;
                }
                return num;
            }
        }

        public bool InitiateConstruction
        {
            get
            {
                return _InitiateConstruction;
            }
            set
            {
                _InitiateConstruction = value;
            }
        }

        public IMessageRecipient MessageRecipient
        {
            get
            {
                return _MessageRecipient;
            }
            set
            {
                _MessageRecipient = value;
            }
        }

        public IAutomationAuthorizer AutomationAuthorizer
        {
            get
            {
                return _AutomationAuthorizer;
            }
            set
            {
                _AutomationAuthorizer = value;
            }
        }

        public AutomationResponse AutomationResponse
        {
            get
            {
                return _AutomationResponse;
            }
            set
            {
                _AutomationResponse = value;
            }
        }

        public IEventMessageRecipient EventMessageRecipient
        {
            get
            {
                return _EventMessageRecipient;
            }
            set
            {
                _EventMessageRecipient = value;
            }
        }

        public HabitatPrioritizationList ColonizationTargets
        {
            get
            {
                return _ColonizationTargets;
            }
            set
            {
                _ColonizationTargets = value;
            }
        }

        public HabitatList DangerousHabitats => _DangerousHabitats;

        public DesignSpecificationList DesignSpecifications => _DesignSpecifications;

        public int TotalColonyStrategicValue
        {
            get
            {
                int num = 0;
                if (Colonies != null)
                {
                    for (int i = 0; i < Colonies.Count; i++)
                    {
                        Habitat habitat = Colonies[i];
                        num += habitat.StrategicValue;
                    }
                }
                return num;
            }
        }

        public EmpireList EmpiresToAttack => _EmpiresToAttack;

        public int MilitaryPotencyCapped
        {
            get
            {
                int num = 0;
                int val = 300;
                for (int i = 0; i < BuiltObjects.Count; i++)
                {
                    num += Math.Min(BuiltObjects[i].FirepowerRaw, val);
                }
                return num;
            }
        }

        public int MilitaryPotency
        {
            get
            {
                int num = 0;
                for (int i = 0; i < BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = BuiltObjects[i];
                    if (builtObject != null)
                    {
                        num += builtObject.CalculateOverallStrengthFactorWithoutShields();
                    }
                }
                return num;
            }
        }

        public int WeightedMilitaryPotency
        {
            get
            {
                int num = MilitaryPotency;
                for (int i = 0; i < DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                    if (diplomaticRelation.Type == DiplomaticRelationType.War)
                    {
                        num -= diplomaticRelation.OtherEmpire.MilitaryPotency;
                    }
                    if (diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact)
                    {
                        num += diplomaticRelation.OtherEmpire.MilitaryPotency;
                    }
                    if (diplomaticRelation.Type == DiplomaticRelationType.Protectorate && diplomaticRelation.Initiator != this)
                    {
                        num += diplomaticRelation.ThisEmpire.MilitaryPotency;
                    }
                }
                if (num < 1)
                {
                    num = 1;
                }
                return num;
            }
        }

        public double PrivateAnnualRevenueUnadjusted
        {
            get
            {
                double num = 0.0;
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat = Colonies[i];
                    if (habitat != null && habitat.Empire == this && (PirateEmpireBaseHabitat == null || !habitat.CheckColonyRevenueFromPirateControl(this)))
                    {
                        num += habitat.AnnualRevenue;
                    }
                }
                return num;
            }
        }

        public double PrivateAnnualRevenue
        {
            get
            {
                double num = PrivateAnnualRevenueUnadjusted;
                long totalPopulation = TotalPopulation;
                if (totalPopulation > Galaxy.RevenueDropoffPopulationThreshholdMin)
                {
                    long num2 = totalPopulation - Galaxy.RevenueDropoffPopulationThreshholdMin;
                    long num3 = Math.Min(num2, Galaxy.RevenueDropoffPopulationThreshholdMax - Galaxy.RevenueDropoffPopulationThreshholdMin);
                    double num4 = (double)num3 / (double)(Galaxy.RevenueDropoffPopulationThreshholdMax - Galaxy.RevenueDropoffPopulationThreshholdMin);
                    double num5 = num4 * Galaxy.RevenueDropoffRate;
                    double num6 = (double)num2 / (double)totalPopulation * num5;
                    if (GovernmentAttributes != null)
                    {
                        num6 *= GovernmentAttributes.Corruption;
                    }
                    double num7 = num * num6;
                    num -= num7;
                }
                if (num < 0.0)
                {
                    num = 0.0;
                }
                return num;
            }
        }

        public double AnnualTaxRevenue
        {
            get
            {
                double num = 0.0;
                long num2 = 0L;
                if (Colonies != null)
                {
                    for (int i = 0; i < Colonies.Count; i++)
                    {
                        Habitat habitat = Colonies[i];
                        if (habitat == null || habitat.Empire != this || (PirateEmpireBaseHabitat != null && habitat.CheckColonyRevenueFromPirateControl(this)) || habitat.Population == null)
                        {
                            continue;
                        }
                        num2 += habitat.Population.TotalAmount;
                        if (!habitat.Rebelling)
                        {
                            if (double.IsNaN(habitat.AnnualRevenue))
                            {
                                habitat.RecalculateAnnualTaxRevenue();
                            }
                            num += habitat.AnnualTaxRevenue;
                        }
                    }
                }
                if (num2 > Galaxy.RevenueDropoffPopulationThreshholdMin)
                {
                    long num3 = num2 - Galaxy.RevenueDropoffPopulationThreshholdMin;
                    long num4 = Math.Min(num3, Galaxy.RevenueDropoffPopulationThreshholdMax - Galaxy.RevenueDropoffPopulationThreshholdMin);
                    double num5 = (double)num4 / (double)(Galaxy.RevenueDropoffPopulationThreshholdMax - Galaxy.RevenueDropoffPopulationThreshholdMin);
                    double num6 = num5 * Galaxy.RevenueDropoffRate;
                    double num7 = (double)num3 / (double)num2 * num6;
                    if (GovernmentAttributes != null)
                    {
                        num7 *= GovernmentAttributes.Corruption;
                    }
                    double num8 = num * num7;
                    num -= num8;
                }
                if (num < 0.0)
                {
                    num = 0.0;
                }
                return num;
            }
        }

        public EmpireMessageList Messages
        {
            get
            {
                return _Messages;
            }
            set
            {
                _Messages = value;
            }
        }

        public EmpireMessageList MessageHistory
        {
            get
            {
                return _MessageHistory;
            }
            set
            {
                _MessageHistory = value;
            }
        }

        public double AnnualFacilityMaintenance
        {
            get
            {
                double num = 0.0;
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat = Colonies[i];
                    if (habitat != null && habitat.Owner == this && habitat.Facilities != null)
                    {
                        num += habitat.Facilities.CalculateAnnualMaintenance();
                    }
                }
                return num;
            }
        }

        public double AnnualSubjugationTribute
        {
            get
            {
                double num = 0.0;
                double num2 = AnnualTaxRevenue + ThisYearsForeignTradeBonuses + ThisYearsSpacePortIncome;
                for (int i = 0; i < DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                    if (diplomaticRelation.Type == DiplomaticRelationType.SubjugatedDominion && diplomaticRelation.Initiator != this)
                    {
                        double num3 = num2 * Galaxy.SubjugationTributePercentage;
                        num += num3;
                    }
                }
                return num;
            }
        }

        public double AnnualPirateProtection
        {
            get
            {
                double num = 0.0;
                if (PirateRelations != null)
                {
                    for (int i = 0; i < PirateRelations.Count; i++)
                    {
                        PirateRelation pirateRelation = PirateRelations[i];
                        if (pirateRelation != null && pirateRelation.Type == PirateRelationType.Protection && pirateRelation.OtherEmpire != null)
                        {
                            PirateRelation pirateRelation2 = pirateRelation.OtherEmpire.ObtainPirateRelation(this);
                            if (pirateRelation2 != null)
                            {
                                num += pirateRelation2.MonthlyProtectionFeeToThisEmpire * 12.0;
                            }
                        }
                    }
                }
                return num;
            }
        }

        public double ColonyApprovalAverage
        {
            get
            {
                double num = 0.0;
                for (int i = 0; i < Colonies.Count; i++)
                {
                    Habitat habitat = Colonies[i];
                    num += habitat.EmpireApprovalRating;
                }
                return num / (double)Colonies.Count;
            }
        }

        public bool CanBuildCarriers => _CanBuildCarriers;

        public bool CanBuildResupplyShips => _CanBuildResupplyShips;

        public double AnnualResearchPotential
        {
            get
            {
                if (PirateEmpireBaseHabitat != null)
                {
                    double num = 0.0;
                    if (BuiltObjects.Count > 0)
                    {
                        num = Math.Sqrt(BuiltObjects.Count) * 10000.0;
                    }
                    if (ResearchFacilities != null && ResearchFacilities.Count > 0)
                    {
                        double num2 = 0.0;
                        for (int i = 0; i < ResearchFacilities.Count; i++)
                        {
                            BuiltObject builtObject = ResearchFacilities[i];
                            if (builtObject != null && !builtObject.HasBeenDestroyed && (builtObject.SubRole == BuiltObjectSubRole.WeaponsResearchStation || builtObject.SubRole == BuiltObjectSubRole.EnergyResearchStation || builtObject.SubRole == BuiltObjectSubRole.HighTechResearchStation))
                            {
                                num2 += 0.5 * (double)(builtObject.ResearchEnergy + builtObject.ResearchHighTech + builtObject.ResearchWeapons);
                            }
                        }
                        num += num2;
                    }
                    double num3 = CalculatePirateResearchBonusFromFacilities();
                    num *= num3;
                    num *= _EconomyEfficiency;
                    if (Characters != null && Characters.CheckCharactersForTrait(CharacterRole.Scientist, CharacterTraitType.UltraGenius))
                    {
                        num *= 1.2;
                    }
                    return num * ResearchRate;
                }
                double num4 = Math.Sqrt(Math.Sqrt((double)TotalPopulation / 1000.0)) * 10000.0;
                num4 *= _EconomyEfficiency;
                if (Characters != null && Characters.CheckCharactersForTrait(CharacterRole.Scientist, CharacterTraitType.UltraGenius))
                {
                    num4 *= 1.2;
                }
                return num4 * ResearchRate;
            }
        }

        public double ResearchEnergyPotential
        {
            get
            {
                double num = 0.0;
                BuiltObjectList builtObjectList = new BuiltObjectList();
                builtObjectList.AddRange(BuiltObjects);
                builtObjectList.AddRange(PrivateBuiltObjects);
                foreach (BuiltObject item in builtObjectList)
                {
                    if (item != null && item.IsResearchLab)
                    {
                        num += (double)item.ResearchEnergy;
                    }
                }
                double val = Math.Max(12000.0, (double)(_TotalPopulation / 1000000) * 3.0);
                num = Math.Max(val, num);
                return num * ResearchEnergyFactor;
            }
        }

        public double ResearchHighTechPotential
        {
            get
            {
                double num = 0.0;
                BuiltObjectList builtObjectList = new BuiltObjectList();
                builtObjectList.AddRange(BuiltObjects);
                builtObjectList.AddRange(PrivateBuiltObjects);
                foreach (BuiltObject item in builtObjectList)
                {
                    if (item != null && item.IsResearchLab)
                    {
                        num += (double)item.ResearchHighTech;
                    }
                }
                double val = Math.Max(12000.0, (double)(_TotalPopulation / 1000000) * 3.0);
                num = Math.Max(val, num);
                return num * ResearchHighTechFactor;
            }
        }

        public double ResearchWeaponsPotential
        {
            get
            {
                double num = 0.0;
                BuiltObjectList builtObjectList = new BuiltObjectList();
                builtObjectList.AddRange(BuiltObjects);
                builtObjectList.AddRange(PrivateBuiltObjects);
                foreach (BuiltObject item in builtObjectList)
                {
                    if (item != null && item.IsResearchLab)
                    {
                        num += (double)item.ResearchWeapons;
                    }
                }
                double val = Math.Max(12000.0, (double)(_TotalPopulation / 1000000) * 3.0);
                num = Math.Max(val, num);
                return num * ResearchWeaponsFactor;
            }
        }

        public double ResearchWeaponsOutput
        {
            get
            {
                double researchWeaponsTotal = ResearchWeaponsTotal;
                double num = CalculateResearchOutputBonuses(IndustryType.Weapon);
                return researchWeaponsTotal * num;
            }
        }

        public double ResearchEnergyOutput
        {
            get
            {
                double researchEnergyTotal = ResearchEnergyTotal;
                double num = CalculateResearchOutputBonuses(IndustryType.Energy);
                return researchEnergyTotal * num;
            }
        }

        public double ResearchHighTechOutput
        {
            get
            {
                double researchHighTechTotal = ResearchHighTechTotal;
                double num = CalculateResearchOutputBonuses(IndustryType.HighTech);
                return researchHighTechTotal * num;
            }
        }

        public double ResearchEnergyTotal
        {
            get
            {
                double researchEnergy = 0.0;
                double researchWeapons = 0.0;
                double researchHighTech = 0.0;
                CalculateResearchTotal(out researchEnergy, out researchHighTech, out researchWeapons);
                return researchEnergy;
            }
        }

        public double ResearchHighTechTotal
        {
            get
            {
                double researchEnergy = 0.0;
                double researchWeapons = 0.0;
                double researchHighTech = 0.0;
                CalculateResearchTotal(out researchEnergy, out researchHighTech, out researchWeapons);
                return researchHighTech;
            }
        }

        public double ResearchWeaponsTotal
        {
            get
            {
                double researchEnergy = 0.0;
                double researchWeapons = 0.0;
                double researchHighTech = 0.0;
                CalculateResearchTotal(out researchEnergy, out researchHighTech, out researchWeapons);
                return researchWeapons;
            }
        }

        private double AcceptableAnnualMaintenanceProportionOfIncome => Galaxy.SpendingShipPercentage / ((double)DominantRace.CautionLevel / 100.0);

        public double AnnualStateMaintenance
        {
            get
            {
                double num = 0.0;
                for (int i = 0; i < BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = BuiltObjects[i];
                    num += (double)builtObject.AnnualSupportCost;
                }
                double num2 = num * _ShipMaintenanceSavings;
                return num - num2;
            }
        }

        public double AnnualStateMaintenanceExcludingUnderConstruction => BaconEmpire.AnnualStateMaintenanceExcludingUnderConstruction(this);

        private double AnnualStateMaintenanceWithoutRetirees
        {
            get
            {
                double num = 0.0;
                for (int i = 0; i < BuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = BuiltObjects[i];
                    if (!builtObject.RetireForNextMission && (builtObject.Mission == null || (builtObject.Mission != null && builtObject.Mission.Type != BuiltObjectMissionType.Retire)))
                    {
                        num += (double)builtObject.AnnualSupportCost;
                    }
                }
                double num2 = num * _ShipMaintenanceSavings;
                return num - num2;
            }
        }

        public double AnnualTroopMaintenance
        {
            get
            {
                double num = 0.0;
                double num2 = 1.0;
                if (Leader != null)
                {
                    num2 *= 1.0 + (double)Leader.TroopMaintenance / 100.0;
                }
                for (int i = 0; i < _Troops.Count; i++)
                {
                    Troop troop = _Troops[i];
                    if (troop == null || troop.Type == TroopType.PirateRaider || troop.BeingRecruited)
                    {
                        continue;
                    }
                    double num3 = Galaxy.TroopAnnualMaintenance * (double)troop.MaintenanceMultiplier * (double)TroopMaintenanceFactor;
                    if (GovernmentAttributes != null)
                    {
                        num3 *= GovernmentAttributes.MaintenanceCosts;
                    }
                    num3 /= num2;
                    if (troop.Colony != null)
                    {
                        if (troop.Colony.Characters != null && troop.Colony.Characters.Count > 0)
                        {
                            int highestSkillLevelExcludeLeaders = troop.Colony.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.TroopMaintenance);
                            double num4 = 1.0 + (double)highestSkillLevelExcludeLeaders / 100.0;
                            num3 /= num4;
                        }
                    }
                    else if (troop.BuiltObject != null && troop.BuiltObject.Characters != null && troop.BuiltObject.Characters.Count > 0)
                    {
                        int highestSkillLevel = troop.BuiltObject.Characters.GetHighestSkillLevel(CharacterSkillType.TroopMaintenance);
                        double num5 = 1.0 + (double)highestSkillLevel / 100.0;
                        num3 /= num5;
                    }
                    num += num3;
                }
                return num;
            }
        }

        public double AnnualTroopMaintenanceIncludeRecruiting
        {
            get
            {
                double num = 0.0;
                double num2 = 1.0;
                if (Leader != null)
                {
                    num2 *= 1.0 + (double)Leader.TroopMaintenance / 100.0;
                }
                for (int i = 0; i < _Troops.Count; i++)
                {
                    Troop troop = _Troops[i];
                    if (troop == null)
                    {
                        continue;
                    }
                    double num3 = Galaxy.TroopAnnualMaintenance * (double)troop.MaintenanceMultiplier * (double)TroopMaintenanceFactor;
                    if (GovernmentAttributes != null)
                    {
                        num3 *= GovernmentAttributes.MaintenanceCosts;
                    }
                    num3 /= num2;
                    if (troop.Colony != null)
                    {
                        if (troop.Colony.Characters != null && troop.Colony.Characters.Count > 0)
                        {
                            int highestSkillLevelExcludeLeaders = troop.Colony.Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.TroopMaintenance);
                            double num4 = 1.0 + (double)highestSkillLevelExcludeLeaders / 100.0;
                            num3 /= num4;
                        }
                    }
                    else if (troop.BuiltObject != null && troop.BuiltObject.Characters != null && troop.BuiltObject.Characters.Count > 0)
                    {
                        int highestSkillLevel = troop.BuiltObject.Characters.GetHighestSkillLevel(CharacterSkillType.TroopMaintenance);
                        double num5 = 1.0 + (double)highestSkillLevel / 100.0;
                        num3 /= num5;
                    }
                    num += num3;
                }
                return num;
            }
        }

        public double AnnualAgentMaintenance => 0.0;

        public double AnnualPrivateMaintenance
        {
            get
            {
                double num = 0.0;
                for (int i = 0; i < PrivateBuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = PrivateBuiltObjects[i];
                    num += (double)builtObject.AnnualSupportCost;
                }
                double num2 = num * _ShipMaintenanceSavings;
                return num - num2;
            }
        }

        public double AnnualPrivateMaintenanceExcludingUnderConstruction
        {
            get
            {
                double num = 0.0;
                for (int i = 0; i < PrivateBuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = PrivateBuiltObjects[i];
                    if (builtObject.UnbuiltComponentCount <= 0)
                    {
                        num += (double)builtObject.AnnualSupportCost;
                    }
                }
                double num2 = num * _ShipMaintenanceSavings;
                return num - num2;
            }
        }

        private double AnnualPrivateMaintenanceWithoutRetirees
        {
            get
            {
                double num = 0.0;
                for (int i = 0; i < PrivateBuiltObjects.Count; i++)
                {
                    BuiltObject builtObject = PrivateBuiltObjects[i];
                    if (!builtObject.RetireForNextMission && (builtObject.Mission == null || (builtObject.Mission != null && builtObject.Mission.Type != BuiltObjectMissionType.Retire)))
                    {
                        num += (double)builtObject.AnnualSupportCost;
                    }
                }
                double num2 = num * _ShipMaintenanceSavings;
                return num - num2;
            }
        }

        public int DesiredIntelligenceAgentAmount
        {
            get
            {
                int num = 0;
                int maximumAgentCount = MaximumAgentCount;
                double num2 = (double)DominantRace.AggressionLevel / 100.0;
                double num3 = (double)DominantRace.IntelligenceLevel / 100.0;
                int num4 = (int)(TotalPopulation / 3000000000L);
                num = (int)((double)num4 * 0.75 * num2 * num3 * num3);
                if (CheckAtWar())
                {
                    num = (int)((double)num * 1.6);
                }
                return Math.Min(maximumAgentCount, num);
            }
        }

        public double MinimumIntelligenceAgentSpending => 0.0;

        private double MinimumTroopSpending
        {
            get
            {
                double num2 = CalculateAccurateAnnualIncome();
                double num3 = num2 * Galaxy.SpendingTroopPercentage;
                if (num3 < Galaxy.TroopAnnualMaintenance)
                {
                    num3 = 0.0;
                }
                double val = AnnualTroopMaintenance * 1.05;
                return Math.Min(val, num3);
            }
        }

        public double MinimumShipSpending
        {
            get
            {
                double num = CalculateAccurateAnnualIncome();
                return num * Galaxy.SpendingShipPercentage;
            }
        }

        public double TaxResistanceRate => _TaxResistanceRate;

        public long TaxResistanceThreshold => _TaxResistanceThreshold;

        public long TotalPopulation => _TotalPopulation;

        public int MaximumAgentCount
        {
            get
            {
                if (PirateEmpireBaseHabitat == null)
                {
                    int val = Colonies.Count / 3;
                    val = Math.Max(2, val);
                    if (DominantRace != null && !DominantRace.Expanding)
                    {
                        val = 6;
                    }
                    val = Math.Min(6, val);
                    if (DominantRace != null)
                    {
                        val += DominantRace.IntelligenceAgentAdditional;
                    }
                    return val;
                }
                int val2 = (int)(Math.Sqrt(BuiltObjects.Count) + 1.0);
                val2 = Math.Max(1, val2);
                if (DominantRace != null && !DominantRace.Expanding)
                {
                    val2 = 6;
                }
                val2 = Math.Min(6, val2);
                if (DominantRace != null)
                {
                    val2 += DominantRace.IntelligenceAgentAdditional;
                }
                return val2;
            }
        }

        public double ThisYearsForeignTradeBonuses
        {
            get
            {
                double num = 0.0;
                for (int i = 0; i < DiplomaticRelations.Count; i++)
                {
                    DiplomaticRelation diplomaticRelation = DiplomaticRelations[i];
                    double annualTradeBonus = diplomaticRelation.AnnualTradeBonus;
                    num += annualTradeBonus;
                }
                return num;
            }
        }

        public double ThisYearsResortIncome
        {
            get
            {
                long currentStarDate = _Galaxy.CurrentStarDate;
                long num = currentStarDate % (Galaxy.RealSecondsInGalacticYear * 1000);
                long num2 = currentStarDate - num;
                if (_LastResortIncomeAddDate < num2)
                {
                    _ThisYearsResortIncome = 0.0;
                }
                return _ThisYearsResortIncome;
            }
        }

        public double ThisYearsSpacePortIncome
        {
            get
            {
                double num = 0.0;
                long currentStarDate = _Galaxy.CurrentStarDate;
                long num2 = currentStarDate % (Galaxy.RealSecondsInGalacticYear * 1000);
                long num3 = currentStarDate - num2;
                if (SpacePorts != null)
                {
                    for (int i = 0; i < SpacePorts.Count; i++)
                    {
                        BuiltObject builtObject = SpacePorts[i];
                        if (builtObject == null)
                        {
                            continue;
                        }
                        if (!_UseAveragedVariableIncome && builtObject.DateOfLastIncome < num3)
                        {
                            if (builtObject.CurrentYearsIncome < (double)(builtObject.AnnualSupportCost * 2))
                            {
                                builtObject.ConsecutiveUnprofitableYears++;
                            }
                            builtObject.CurrentYearsIncome = 0.0;
                        }
                        num += builtObject.CurrentYearsIncome;
                    }
                }
                if (MiningStations != null)
                {
                    for (int j = 0; j < MiningStations.Count; j++)
                    {
                        BuiltObject builtObject2 = MiningStations[j];
                        if (builtObject2 == null)
                        {
                            continue;
                        }
                        if (!_UseAveragedVariableIncome && builtObject2.DateOfLastIncome < num3)
                        {
                            if (builtObject2.CurrentYearsIncome < (double)(builtObject2.AnnualSupportCost * 2))
                            {
                                builtObject2.ConsecutiveUnprofitableYears++;
                            }
                            builtObject2.CurrentYearsIncome = 0.0;
                        }
                        num += builtObject2.CurrentYearsIncome;
                    }
                }
                return num;
            }
        }

        public double ThisYearsStateFuelCosts => _ThisYearsStateFuelCosts;

        public double ThisYearsPrivateFuelCosts => _ThisYearsPrivateFuelCosts;

        public long NextAllowableLeaderChangeDate
        {
            get
            {
                double val = 1.0;
                if (_GovernmentAttributes != null)
                {
                    val = _GovernmentAttributes.LeaderReplacementLikeliness;
                }
                double num = 3.0 * (1.0 / Math.Max(0.001, val));
                return LastLeaderChangeDate + (long)((double)Galaxy.RealSecondsInGalacticYear * 1000.0 * num);
            }
        }

        public int FleetMaximumCount
        {
            get
            {
                int num = Galaxy.FleetMaximumCount;
                if (BuiltObjects != null)
                {
                    num = Math.Min(100, Math.Max(num, BuiltObjects.Count / 10));
                }
                return num;
            }
        }

        public DistressSignalList DistressSignals
        {
            get
            {
                return _DistressSignals;
            }
            set
            {
                _DistressSignals = value;
            }
        }

        public Habitat Capital
        {
            get
            {
                return _Capital;
            }
            set
            {
                _Capital = value;
            }
        }

        public GalaxyResourceMap ResourceMap
        {
            get
            {
                return _ResourceMap;
            }
            set
            {
                _ResourceMap = value;
            }
        }

        public string TroopDescription
        {
            get
            {
                return _TroopDescription;
            }
            set
            {
                _TroopDescription = value;
            }
        }

        public TroopList Troops
        {
            get
            {
                return _Troops;
            }
            set
            {
                _Troops = value;
            }
        }

        public int TroopPictureRef
        {
            get
            {
                return _TroopPictureRef;
            }
            set
            {
                _TroopPictureRef = value;
            }
        }

        public ResearchSystem Research
        {
            get
            {
                return _Research;
            }
            set
            {
                _Research = value;
            }
        }

        public DesignList Designs
        {
            get
            {
                return _Designs;
            }
            set
            {
                _Designs = value;
            }
        }

        public DesignList LatestDesigns
        {
            get
            {
                return _LatestDesigns;
            }
            set
            {
                _LatestDesigns = value;
            }
        }

        public DesignList ForeignDesigns
        {
            get
            {
                return _ForeignDesigns;
            }
            set
            {
                _ForeignDesigns = value;
            }
        }

        public BuiltObjectList ResourceExtractors
        {
            get
            {
                return _ResourceExtractors;
            }
            set
            {
                _ResourceExtractors = value;
            }
        }

        public BuiltObjectList Manufacturers
        {
            get
            {
                return _Manufacturers;
            }
            set
            {
                _Manufacturers = value;
            }
        }

        public BuiltObjectList ConstructionYards
        {
            get
            {
                return _ConstructionYards;
            }
            set
            {
                _ConstructionYards = value;
            }
        }

        public BuiltObjectList ResortBases
        {
            get
            {
                return _ResortBases;
            }
            set
            {
                _ResortBases = value;
            }
        }

        public BuiltObjectList ResupplyShips
        {
            get
            {
                return _ResupplyShips;
            }
            set
            {
                _ResupplyShips = value;
            }
        }

        public BuiltObjectList PlanetDestroyers
        {
            get
            {
                return _PlanetDestroyers;
            }
            set
            {
                _PlanetDestroyers = value;
            }
        }

        public BuiltObjectList ConstructionShips
        {
            get
            {
                return _ConstructionShips;
            }
            set
            {
                _ConstructionShips = value;
            }
        }

        public double LeaderChangeInfluence => _LeaderChangeInfluence;

        public IntelligenceMissionList IntelligenceMissions
        {
            get
            {
                return _IntelligenceMissions;
            }
            set
            {
                _IntelligenceMissions = value;
            }
        }

        public BuiltObjectList Outlaws
        {
            get
            {
                return _Outlaws;
            }
            set
            {
                _Outlaws = value;
            }
        }

        public DiplomaticRelationList ProposedDiplomaticRelations
        {
            get
            {
                return _ProposedDiplomaticRelations;
            }
            set
            {
                _ProposedDiplomaticRelations = value;
            }
        }

        public Color MainColor
        {
            get
            {
                return _MainColor;
            }
            set
            {
                _MainColor = value;
            }
        }

        public Color SecondaryColor
        {
            get
            {
                return _SecondaryColor;
            }
            set
            {
                _SecondaryColor = value;
            }
        }

        public int FlagShape
        {
            get
            {
                return _FlagShape;
            }
            set
            {
                _FlagShape = value;
            }
        }

        public Bitmap SmallFlagPicture
        {
            get
            {
                return _SmallFlagPicture;
            }
            set
            {
                _SmallFlagPicture = value;
            }
        }

        public Bitmap LargeFlagPicture
        {
            get
            {
                return _LargeFlagPicture;
            }
            set
            {
                _LargeFlagPicture = value;
            }
        }

        public Race DominantRace
        {
            get
            {
                return _DominantRace;
            }
            set
            {
                _DominantRace = value;
                if (_DominantRace != null)
                {
                    _DesignPictureFamilyIndex = _DominantRace.DesignPictureFamilyIndex;
                }
            }
        }

        public double TaxRate => _TaxRate;

        public Empire ParentEmpire
        {
            get
            {
                return _ParentEmpire;
            }
            set
            {
                _ParentEmpire = value;
            }
        }

        public bool LetOthersProvideFreight
        {
            get
            {
                return _LetOthersProvideFreight;
            }
            set
            {
                _LetOthersProvideFreight = value;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public int Score
        {
            get
            {
                return _Score;
            }
            set
            {
                _Score = value;
            }
        }

        public double StateMoney
        {
            get
            {
                return _StateMoney;
            }
            set
            {
                _StateMoney = value;
            }
        }

        public double PrivateMoney
        {
            get
            {
                return _PrivateMoney;
            }
            set
            {
                _PrivateMoney = value;
            }
        }

        public int IntelligenceSpending
        {
            get
            {
                return _IntelligenceSpending;
            }
            set
            {
                _IntelligenceSpending = value;
            }
        }

        public int CounterEspionageSpending
        {
            get
            {
                return _CounterEspionageSpending;
            }
            set
            {
                _CounterEspionageSpending = value;
            }
        }

        public int CounterSabotageSpending
        {
            get
            {
                return _CounterSabotageSpending;
            }
            set
            {
                _CounterSabotageSpending = value;
            }
        }

        public int GovernmentId
        {
            get
            {
                return _GovernmentId;
            }
            set
            {
                _GovernmentId = value;
            }
        }

        public GovernmentAttributes GovernmentAttributes => _GovernmentAttributes;

        public void AddLocationHint(Point location)
        {
            lock (LocationHintLock)
            {
                Rectangle rectangle = new Rectangle(location.X - Galaxy.MaxSolarSystemSize, location.Y - Galaxy.MaxSolarSystemSize, Galaxy.MaxSolarSystemSize * 2, Galaxy.MaxSolarSystemSize * 2);
                foreach (Point locationHint in LocationHints)
                {
                    if (rectangle.Contains(locationHint))
                    {
                        return;
                    }
                }
                LocationHints.Add(location);
            }
        }

        public void SetPirateFactionModifiers(PiratePlayStyle piratePlayStyle)
        {
            BaconEmpire.SetPirateFactionModifiers(this, piratePlayStyle);
        }

        public List<string> ResolveEmpireAbilityBonusDescriptions()
        {
            RaceList bonusRaces = new RaceList();
            return ResolveEmpireAbilityBonusDescriptions(includeDominantRaceInDescriptions: false, out bonusRaces);
        }

        public List<string> ResolveEmpireAbilityBonusDescriptions(bool includeDominantRaceInDescriptions, out RaceList bonusRaces)
        {
            List<string> list = new List<string>();
            bonusRaces = new RaceList();
            if (_ShipMaintenanceSavings > 0.0)
            {
                string text = Galaxy.ResolveEmpireAbilityBonusDescriptionShipMaintenance(_ShipMaintenanceSavings);
                if (includeDominantRaceInDescriptions || _ShipMaintenanceSavingsRace != DominantRace)
                {
                    text = text + " (" + string.Format(TextResolver.GetText("BONUS from RACE"), _ShipMaintenanceSavingsRace.Name) + ")";
                }
                list.Add(text);
                bonusRaces.Add(_ShipMaintenanceSavingsRace);
            }
            if (_ResourceExtractionBonus > 0.0)
            {
                string text2 = Galaxy.ResolveEmpireAbilityBonusDescriptionResourceExtraction(_ResourceExtractionBonus);
                if (includeDominantRaceInDescriptions || _ResourceExtractionBonusRace != DominantRace)
                {
                    text2 = text2 + " (" + string.Format(TextResolver.GetText("BONUS from RACE"), _ResourceExtractionBonusRace.Name) + ")";
                }
                list.Add(text2);
                bonusRaces.Add(_ResourceExtractionBonusRace);
            }
            if (_ResearchBonus > 0.0)
            {
                string text3 = Galaxy.ResolveEmpireAbilityBonusDescriptionResearch(_ResearchBonus);
                if (includeDominantRaceInDescriptions || _ResearchBonusRace != DominantRace)
                {
                    text3 = text3 + " (" + string.Format(TextResolver.GetText("BONUS from RACE"), _ResearchBonusRace.Name) + ")";
                }
                list.Add(text3);
                bonusRaces.Add(_ResearchBonusRace);
            }
            if (_EspionageBonus > 0.0)
            {
                string text4 = Galaxy.ResolveEmpireAbilityBonusDescriptionEspionage(_EspionageBonus);
                if (includeDominantRaceInDescriptions || _EspionageBonusRace != DominantRace)
                {
                    text4 = text4 + " (" + string.Format(TextResolver.GetText("BONUS from RACE"), _EspionageBonusRace.Name) + ")";
                }
                list.Add(text4);
                bonusRaces.Add(_EspionageBonusRace);
            }
            if (_TradeBonus > 0.0)
            {
                string text5 = Galaxy.ResolveEmpireAbilityBonusDescriptionTrade(_TradeBonus);
                if (includeDominantRaceInDescriptions || _TradeBonusRace != DominantRace)
                {
                    text5 = text5 + " (" + string.Format(TextResolver.GetText("BONUS from RACE"), _TradeBonusRace.Name) + ")";
                }
                list.Add(text5);
                bonusRaces.Add(_TradeBonusRace);
            }
            return list;
        }

        public List<string> ReviewEmpireAbilityBonuses()
        {
            RaceList newAbilityRaces = new RaceList();
            Race raceChanged = null;
            return ReviewEmpireAbilityBonuses(out newAbilityRaces, out raceChanged);
        }

        public List<string> ReviewEmpireAbilityBonuses(out RaceList newAbilityRaces, out Race raceChanged)
        {
            newAbilityRaces = new RaceList();
            raceChanged = null;
            if (this == _Galaxy.IndependentEmpire)
            {
                return new List<string>();
            }
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            Race race = null;
            Race race2 = null;
            Race race3 = null;
            Race race4 = null;
            Race race5 = null;
            RaceList raceList = new RaceList();
            List<double> list = new List<double>();
            raceList.Add(DominantRace);
            list.Add(0.0);
            double num6 = 0.0;
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                for (int j = 0; j < habitat.Population.Count; j++)
                {
                    Population population = habitat.Population[j];
                    int num7 = raceList.IndexOf(population.Race);
                    if (num7 >= 0)
                    {
                        list[num7] += (double)population.Amount / 1000000.0;
                    }
                    else
                    {
                        raceList.Add(population.Race);
                        list.Add((double)population.Amount / 1000000.0);
                    }
                    num6 += (double)population.Amount / 1000000.0;
                }
            }
            for (int k = 0; k < raceList.Count; k++)
            {
                double val = 0.0;
                if (list[k] >= 10.0)
                {
                    val = list[k] / (num6 / 5.0);
                    val = Math.Max(0.1, val);
                }
                else if (list[k] > 0.0)
                {
                    val = 0.0;
                }
                val = Math.Min(1.0, val);
                double num8 = (double)raceList[k].ShipMaintenanceSavings / 100.0;
                double num9 = (double)raceList[k].ResourceExtractionBonus / 100.0;
                double num10 = (double)raceList[k].ResearchBonus / 100.0;
                double num11 = (double)raceList[k].EspionageBonus / 100.0;
                double num12 = (double)raceList[k].TradeBonus / 100.0;
                if (num8 * val > num)
                {
                    num = num8 * val;
                    race = raceList[k];
                }
                if (num9 * val > num2)
                {
                    num2 = num9 * val;
                    race2 = raceList[k];
                }
                if (num10 * val > num3)
                {
                    num3 = num10 * val;
                    race3 = raceList[k];
                }
                if (num11 * val > num4)
                {
                    num4 = num11 * val;
                    race4 = raceList[k];
                }
                if (num12 * val > num5)
                {
                    num5 = num12 * val;
                    race5 = raceList[k];
                }
            }
            List<string> list2 = new List<string>();
            if (num > _ShipMaintenanceSavings)
            {
                if (race != _ShipMaintenanceSavingsRace)
                {
                    raceChanged = race;
                }
                _ShipMaintenanceSavings = num;
                _ShipMaintenanceSavingsRace = race;
                list2.Add(Galaxy.ResolveEmpireAbilityBonusDescriptionShipMaintenance(num));
                newAbilityRaces.Add(race);
            }
            else if (num <= 0.0)
            {
                _ShipMaintenanceSavings = 0.0;
                _ShipMaintenanceSavingsRace = null;
            }
            if (num2 > _ResourceExtractionBonus)
            {
                if (race2 != _ResourceExtractionBonusRace)
                {
                    raceChanged = race2;
                }
                _ResourceExtractionBonus = num2;
                _ResourceExtractionBonusRace = race2;
                list2.Add(Galaxy.ResolveEmpireAbilityBonusDescriptionResourceExtraction(num2));
                newAbilityRaces.Add(race2);
            }
            else if (num2 <= 0.0)
            {
                _ResourceExtractionBonus = 0.0;
                _ResourceExtractionBonusRace = null;
            }
            if (num3 > _ResearchBonus)
            {
                if (race3 != _ResearchBonusRace)
                {
                    raceChanged = race3;
                }
                _ResearchBonus = num3;
                _ResearchBonusRace = race3;
                list2.Add(Galaxy.ResolveEmpireAbilityBonusDescriptionResearch(num3));
                newAbilityRaces.Add(race3);
            }
            else if (num3 <= 0.0)
            {
                _ResearchBonus = 0.0;
                _ResearchBonusRace = null;
            }
            if (num4 > _EspionageBonus)
            {
                if (race4 != _EspionageBonusRace)
                {
                    raceChanged = race4;
                }
                _EspionageBonus = num4;
                _EspionageBonusRace = race4;
                list2.Add(Galaxy.ResolveEmpireAbilityBonusDescriptionEspionage(num4));
                newAbilityRaces.Add(race4);
            }
            else if (num4 <= 0.0)
            {
                _EspionageBonus = 0.0;
                _EspionageBonusRace = null;
            }
            if (num5 > _TradeBonus)
            {
                if (race5 != _TradeBonusRace)
                {
                    raceChanged = race5;
                }
                _TradeBonus = num5;
                _TradeBonusRace = race5;
                list2.Add(Galaxy.ResolveEmpireAbilityBonusDescriptionTrade(num5));
                newAbilityRaces.Add(race5);
            }
            else if (num5 <= 0.0)
            {
                _TradeBonus = 0.0;
                _TradeBonusRace = null;
            }
            return list2;
        }

        public void RefreshAllowableGovernmentTypes()
        {
            if (PirateEmpireBaseHabitat == null)
            {
                _AllowableGovernmentTypes = ResolveDefaultAllowableGovernmentTypes(DominantRace, forceIncludeSpecialTypesIfRaceAllows: true);
            }
        }

        public string GetNextFleetNumberDescription()
        {
            _FleetIdentity++;
            string text = _FleetIdentity.ToString();
            switch (text.Substring(text.Length - 1, 1))
            {
                case "0":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    text += "th";
                    break;
                case "1":
                    text = ((_FleetIdentity % 100 != 11) ? (text + "st") : (text + "th"));
                    break;
                case "2":
                    text = ((_FleetIdentity % 100 != 12) ? (text + "nd") : (text + "th"));
                    break;
                case "3":
                    text = ((_FleetIdentity % 100 != 13) ? (text + "rd") : (text + "th"));
                    break;
            }
            return text;
        }

        public double CalculateRacialReputationConcern(Race race)
        {
            double result = 1.0;
            if (race != null)
            {
                result = (double)race.AggressionLevel / (double)race.FriendlinessLevel;
                result = result * result * result * result * result;
                result = Math.Max(1.0, result);
            }
            return result;
        }

        private string GetNewProperDesignName(BuiltObjectSubRole subRole)
        {
            string text = string.Empty;
            if (_DesignNamesUsage.Length < _Galaxy.DesignNames[_DesignNamesIndex].Length)
            {
                _DesignNamesUsage = new int[_Galaxy.DesignNames[_DesignNamesIndex].Length];
            }
            int num = 0;
            int[] designNamesUsage = _DesignNamesUsage;
            foreach (int num2 in designNamesUsage)
            {
                if (num2 > num)
                {
                    num = num2;
                }
            }
            bool flag = false;
            int[] designNamesUsage2 = _DesignNamesUsage;
            foreach (int num3 in designNamesUsage2)
            {
                if (num3 < num)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                num++;
            }
            int num4 = 0;
            while (string.IsNullOrEmpty(text) && num4 < 50)
            {
                int num5 = Galaxy.Rnd.Next(0, _Galaxy.DesignNames[_DesignNamesIndex].Length);
                if (_DesignNamesUsage[num5] < num)
                {
                    text = _Galaxy.DesignNames[_DesignNamesIndex][num5];
                    _DesignNamesUsage[num5]++;
                }
                num4++;
            }
            if (string.IsNullOrEmpty(text))
            {
                Galaxy.SetRandom(new Random((int)DateTime.Now.Ticks));
                num4 = 0;
                while (string.IsNullOrEmpty(text) && num4 < 50)
                {
                    int num6 = Galaxy.Rnd.Next(0, _Galaxy.DesignNames[_DesignNamesIndex].Length);
                    if (_DesignNamesUsage[num6] < num)
                    {
                        text = _Galaxy.DesignNames[_DesignNamesIndex][num6];
                        _DesignNamesUsage[num6]++;
                    }
                    num4++;
                }
            }
            if (string.IsNullOrEmpty(text))
            {
                text = Galaxy.ResolveDescription(subRole);
            }
            return text;
        }

        private string RomanNumeral(int number)
        {
            if (number > 10000 || number < 0)
            {
                return "[.]";
            }
            if (_StringBuilder == null)
            {
                _StringBuilder = new StringBuilder();
            }
            _StringBuilder.Clear();
            int num = 0;
            int iterationCount = 0;
            while (Galaxy.ConditionCheckLimit(number > 0, 100, ref iterationCount))
            {
                int value = _RomanNumbers[num].Value;
                if (number >= value)
                {
                    number -= value;
                    _StringBuilder.Append(_RomanNumbers[num].Rep);
                }
                else
                {
                    num++;
                }
            }
            return _StringBuilder.ToString();
        }

        public string GenerateDesignName(BuiltObjectSubRole subRole, Design previousDesign)
        {
            string text = string.Empty;
            int num = 0;
            bool flag = false;
            if (previousDesign == null)
            {
                flag = true;
            }
            else
            {
                Component component = Research.EvaluateDesiredComponent(ComponentCategoryType.Reactor, ShipDesignFocus.Balanced);
                foreach (Component component2 in previousDesign.Components)
                {
                    if (component2.Category == ComponentCategoryType.Reactor && component2.ComponentID != component.ComponentID)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            switch (subRole)
            {
                case BuiltObjectSubRole.Escort:
                    if (string.IsNullOrEmpty(_EscortPrefix))
                    {
                        _EscortPrefix = GetNewProperDesignName(BuiltObjectSubRole.Escort);
                    }
                    _EscortCurrentModelNumber++;
                    if (flag)
                    {
                        _EscortPrefix = GetNewProperDesignName(BuiltObjectSubRole.Escort);
                        _EscortCurrentModelNumber = 1;
                    }
                    text = _EscortPrefix;
                    if (_EscortCurrentModelNumber > 1)
                    {
                        text = text + " " + RomanNumeral(_EscortCurrentModelNumber);
                    }
                    break;
                case BuiltObjectSubRole.Frigate:
                    if (string.IsNullOrEmpty(_FrigatePrefix))
                    {
                        _FrigatePrefix = GetNewProperDesignName(BuiltObjectSubRole.Frigate);
                    }
                    _FrigateCurrentModelNumber++;
                    if (flag)
                    {
                        _FrigatePrefix = GetNewProperDesignName(BuiltObjectSubRole.Frigate);
                        _FrigateCurrentModelNumber = 1;
                    }
                    text = _FrigatePrefix;
                    if (_FrigateCurrentModelNumber > 1)
                    {
                        text = text + " " + RomanNumeral(_FrigateCurrentModelNumber);
                    }
                    break;
                case BuiltObjectSubRole.Destroyer:
                    if (string.IsNullOrEmpty(_DestroyerPrefix))
                    {
                        _DestroyerPrefix = GetNewProperDesignName(BuiltObjectSubRole.Destroyer);
                    }
                    _DestroyerCurrentModelNumber++;
                    if (flag)
                    {
                        _DestroyerPrefix = GetNewProperDesignName(BuiltObjectSubRole.Destroyer);
                        _DestroyerCurrentModelNumber = 1;
                    }
                    text = _DestroyerPrefix;
                    if (_DestroyerCurrentModelNumber > 1)
                    {
                        text = text + " " + RomanNumeral(_DestroyerCurrentModelNumber);
                    }
                    break;
                case BuiltObjectSubRole.Cruiser:
                    if (string.IsNullOrEmpty(_CruiserPrefix))
                    {
                        _CruiserPrefix = GetNewProperDesignName(BuiltObjectSubRole.Cruiser);
                    }
                    _CruiserCurrentModelNumber++;
                    if (flag)
                    {
                        _CruiserPrefix = GetNewProperDesignName(BuiltObjectSubRole.Cruiser);
                        _CruiserCurrentModelNumber = 1;
                    }
                    text = _CruiserPrefix;
                    if (_CruiserCurrentModelNumber > 1)
                    {
                        text = text + " " + RomanNumeral(_CruiserCurrentModelNumber);
                    }
                    break;
                case BuiltObjectSubRole.CapitalShip:
                    if (string.IsNullOrEmpty(_CapitalShipPrefix))
                    {
                        _CapitalShipPrefix = GetNewProperDesignName(BuiltObjectSubRole.CapitalShip);
                    }
                    _CapitalShipCurrentModelNumber++;
                    if (flag)
                    {
                        _CapitalShipPrefix = GetNewProperDesignName(BuiltObjectSubRole.CapitalShip);
                        _CapitalShipCurrentModelNumber = 1;
                    }
                    text = _CapitalShipPrefix;
                    if (_CapitalShipCurrentModelNumber > 1)
                    {
                        text = text + " " + RomanNumeral(_CapitalShipCurrentModelNumber);
                    }
                    break;
                case BuiltObjectSubRole.TroopTransport:
                    if (string.IsNullOrEmpty(_TroopTransportPrefix))
                    {
                        _TroopTransportPrefix = GetNewProperDesignName(BuiltObjectSubRole.TroopTransport);
                    }
                    _TroopTransportCurrentModelNumber++;
                    if (flag)
                    {
                        _TroopTransportPrefix = GetNewProperDesignName(BuiltObjectSubRole.TroopTransport);
                        _TroopTransportCurrentModelNumber = 1;
                    }
                    text = _TroopTransportPrefix;
                    if (_TroopTransportCurrentModelNumber > 1)
                    {
                        text = text + " " + RomanNumeral(_TroopTransportCurrentModelNumber);
                    }
                    break;
                case BuiltObjectSubRole.Carrier:
                    _CarrierCurrentModelNumber++;
                    text = "CX-" + _CarrierCurrentModelNumber + " " + TextResolver.GetText("Ship SubRole Carrier");
                    break;
                case BuiltObjectSubRole.ResupplyShip:
                    {
                        string[] array = new string[4]
                        {
                TextResolver.GetText("Ship SubRole ResupplyShip"),
                TextResolver.GetText("Mothership"),
                TextResolver.GetText("Refuelling Base"),
                TextResolver.GetText("Fleet Supply Ship")
                        };
                        string text2 = array[Galaxy.Rnd.Next(0, array.Length)];
                        _ResupplyShipCurrentModelNumber++;
                        text = "RS-" + _ResupplyShipCurrentModelNumber + " " + text2;
                        break;
                    }
                case BuiltObjectSubRole.SmallFreighter:
                    {
                        string[] array = new string[6]
                        {
                TextResolver.GetText("Light Transport"),
                TextResolver.GetText("Light Trader"),
                TextResolver.GetText("Light Hauler"),
                TextResolver.GetText("Light Freighter"),
                TextResolver.GetText("Cargo Shuttle"),
                TextResolver.GetText("Merchant Freighter")
                        };
                        if (string.IsNullOrEmpty(_SmallFreighterPrefix))
                        {
                            _SmallFreighterPrefix = GenerateDesignNamePrefix();
                        }
                        num = Galaxy.Rnd.Next(1, 4);
                        _SmallFreighterCurrentModelNumber += num * 100;
                        if (flag)
                        {
                            _SmallFreighterPrefix = GenerateDesignNamePrefix();
                            _SmallFreighterName = array[Galaxy.Rnd.Next(0, array.Length)];
                            _SmallFreighterCurrentModelNumber = 1000;
                        }
                        text = _SmallFreighterPrefix + _SmallFreighterCurrentModelNumber + " " + _SmallFreighterName;
                        break;
                    }
                case BuiltObjectSubRole.MediumFreighter:
                    {
                        string[] array = new string[6]
                        {
                TextResolver.GetText("Medium Transport"),
                TextResolver.GetText("Cargo Freighter"),
                TextResolver.GetText("Cargo Hauler"),
                TextResolver.GetText("Ship SubRole MediumFreighter"),
                TextResolver.GetText("Cargo Ferry"),
                TextResolver.GetText("Freight Hauler")
                        };
                        if (string.IsNullOrEmpty(_MediumFreighterPrefix))
                        {
                            _MediumFreighterPrefix = GenerateDesignNamePrefix();
                        }
                        num = Galaxy.Rnd.Next(1, 4);
                        _MediumFreighterCurrentModelNumber += num * 100;
                        if (flag)
                        {
                            _MediumFreighterPrefix = GenerateDesignNamePrefix();
                            _MediumFreighterName = array[Galaxy.Rnd.Next(0, array.Length)];
                            _MediumFreighterCurrentModelNumber = 1000;
                        }
                        text = _MediumFreighterPrefix + _MediumFreighterCurrentModelNumber + " " + _MediumFreighterName;
                        break;
                    }
                case BuiltObjectSubRole.LargeFreighter:
                    {
                        string[] array = new string[7]
                        {
                TextResolver.GetText("Super Hauler"),
                TextResolver.GetText("Cargo Carrier"),
                TextResolver.GetText("Heavy Freighter"),
                TextResolver.GetText("Cargo Barge"),
                TextResolver.GetText("Bulk Freighter"),
                TextResolver.GetText("Heavy Lifter"),
                TextResolver.GetText("Bulk Transport")
                        };
                        if (string.IsNullOrEmpty(_LargeFreighterPrefix))
                        {
                            _LargeFreighterPrefix = GenerateDesignNamePrefix();
                        }
                        num = Galaxy.Rnd.Next(1, 4);
                        _LargeFreighterCurrentModelNumber += num * 100;
                        if (flag)
                        {
                            _LargeFreighterPrefix = GenerateDesignNamePrefix();
                            _LargeFreighterName = array[Galaxy.Rnd.Next(0, array.Length)];
                            _LargeFreighterCurrentModelNumber = 1000;
                        }
                        text = _LargeFreighterPrefix + _LargeFreighterCurrentModelNumber + " " + _LargeFreighterName;
                        break;
                    }
                case BuiltObjectSubRole.GasMiningShip:
                    {
                        string[] array = new string[5]
                        {
                TextResolver.GetText("Gas Prospector"),
                TextResolver.GetText("Ship SubRole GasMiningShip"),
                TextResolver.GetText("Gas Miner"),
                TextResolver.GetText("Gas Hauler"),
                TextResolver.GetText("Gas Tanker")
                        };
                        if (string.IsNullOrEmpty(_GasMiningShipPrefix))
                        {
                            _GasMiningShipPrefix = GenerateDesignNamePrefix();
                        }
                        num = Galaxy.Rnd.Next(1, 4);
                        _GasMiningShipCurrentModelNumber += num * 100;
                        if (flag)
                        {
                            _GasMiningShipPrefix = GenerateDesignNamePrefix();
                            _GasMiningShipName = array[Galaxy.Rnd.Next(0, array.Length)];
                            _GasMiningShipCurrentModelNumber = 1000;
                        }
                        text = _GasMiningShipPrefix + _GasMiningShipCurrentModelNumber + " " + _GasMiningShipName;
                        break;
                    }
                case BuiltObjectSubRole.MiningShip:
                    {
                        string[] array = new string[5]
                        {
                TextResolver.GetText("Ore Prospector"),
                TextResolver.GetText("Ore Hauler"),
                TextResolver.GetText("Miner"),
                TextResolver.GetText("Ship SubRole MiningShip"),
                TextResolver.GetText("Mineral Miner")
                        };
                        if (string.IsNullOrEmpty(_MiningShipPrefix))
                        {
                            _MiningShipPrefix = GenerateDesignNamePrefix();
                        }
                        num = Galaxy.Rnd.Next(1, 4);
                        _MiningShipCurrentModelNumber += num * 100;
                        if (flag)
                        {
                            _MiningShipPrefix = GenerateDesignNamePrefix();
                            _MiningShipName = array[Galaxy.Rnd.Next(0, array.Length)];
                            _MiningShipCurrentModelNumber = 1000;
                        }
                        text = _MiningShipPrefix + _MiningShipCurrentModelNumber + " " + _MiningShipName;
                        break;
                    }
                case BuiltObjectSubRole.ResortBase:
                    _ResortBaseCurrentModelNumber++;
                    text = "RB-" + _ResortBaseCurrentModelNumber;
                    break;
                case BuiltObjectSubRole.WeaponsResearchStation:
                    _WeaponsResearchStationCurrentModelNumber++;
                    text = "WRS-" + _WeaponsResearchStationCurrentModelNumber;
                    break;
                case BuiltObjectSubRole.EnergyResearchStation:
                    _EnergyResearchStationCurrentModelNumber++;
                    text = "ERS-" + _EnergyResearchStationCurrentModelNumber;
                    break;
                case BuiltObjectSubRole.HighTechResearchStation:
                    _HighTechResearchStationCurrentModelNumber++;
                    text = "HTRS-" + _HighTechResearchStationCurrentModelNumber;
                    break;
                case BuiltObjectSubRole.MonitoringStation:
                    _MonitoringStationCurrentModelNumber++;
                    text = "MON-" + _MonitoringStationCurrentModelNumber;
                    break;
                case BuiltObjectSubRole.DefensiveBase:
                    _DefensiveBaseCurrentModelNumber++;
                    text = "DFB-" + _DefensiveBaseCurrentModelNumber;
                    break;
                case BuiltObjectSubRole.Outpost:
                    _OutpostCurrentModelNumber++;
                    text = "OPost-" + _OutpostCurrentModelNumber;
                    break;
                case BuiltObjectSubRole.SmallSpacePort:
                    _SmallSpacePortCurrentModelNumber++;
                    text = "SSP-" + _SmallSpacePortCurrentModelNumber;
                    break;
                case BuiltObjectSubRole.MediumSpacePort:
                    _MediumSpacePortCurrentModelNumber++;
                    text = "MSP-" + _MediumSpacePortCurrentModelNumber;
                    break;
                case BuiltObjectSubRole.LargeSpacePort:
                    _LargeSpacePortCurrentModelNumber++;
                    text = "LSP-" + _LargeSpacePortCurrentModelNumber;
                    break;
                case BuiltObjectSubRole.MiningStation:
                    _MiningStationCurrentModelNumber++;
                    text = "MS-" + _MiningStationCurrentModelNumber;
                    break;
                case BuiltObjectSubRole.GasMiningStation:
                    _GasMiningStationCurrentModelNumber++;
                    text = "GMS-" + _GasMiningStationCurrentModelNumber;
                    break;
                case BuiltObjectSubRole.ConstructionShip:
                    {
                        string[] array = new string[4]
                        {
                TextResolver.GetText("Ship SubRole ConstructionShip"),
                TextResolver.GetText("Constructor"),
                TextResolver.GetText("Ship Yard"),
                TextResolver.GetText("Space Dock")
                        };
                        string text2 = array[Galaxy.Rnd.Next(0, array.Length)];
                        _ConstructionShipCurrentModelNumber++;
                        text = "CST-" + _ConstructionShipCurrentModelNumber + " " + text2;
                        break;
                    }
                case BuiltObjectSubRole.PassengerShip:
                    {
                        string[] array = new string[6]
                        {
                TextResolver.GetText("Ship SubRole PassengerShip"),
                TextResolver.GetText("Space Liner"),
                TextResolver.GetText("Commuter"),
                TextResolver.GetText("Traveller"),
                TextResolver.GetText("Tourist"),
                TextResolver.GetText("Migrant")
                        };
                        string text2 = array[Galaxy.Rnd.Next(0, array.Length)];
                        _PassengerShipCurrentModelNumber++;
                        text = "PS-" + _PassengerShipCurrentModelNumber + " " + text2;
                        break;
                    }
                case BuiltObjectSubRole.ExplorationShip:
                    {
                        string[] array = new string[6]
                        {
                TextResolver.GetText("Scout"),
                TextResolver.GetText("Surveyor"),
                TextResolver.GetText("Pathfinder"),
                TextResolver.GetText("Explorer"),
                TextResolver.GetText("Traveller"),
                TextResolver.GetText("Navigator")
                        };
                        string text2 = array[Galaxy.Rnd.Next(0, array.Length)];
                        _ExplorationShipCurrentModelNumber++;
                        text = "EX-" + _ExplorationShipCurrentModelNumber + " " + text2;
                        break;
                    }
                case BuiltObjectSubRole.ColonyShip:
                    {
                        string[] array = new string[4]
                        {
                TextResolver.GetText("Colonizer"),
                TextResolver.GetText("Settler"),
                TextResolver.GetText("World Founder"),
                TextResolver.GetText("Ship SubRole ColonyShip")
                        };
                        string text2 = array[Galaxy.Rnd.Next(0, array.Length)];
                        _ColonyShipCurrentModelNumber++;
                        text = "CLN-" + _ColonyShipCurrentModelNumber + " " + text2;
                        break;
                    }
                case BuiltObjectSubRole.GenericBase:
                    text = TextResolver.GetText("Ship SubRole GenericBase");
                    break;
            }
            return text;
        }

        private string GenerateDesignNamePrefix()
        {
            return ((char)Galaxy.Rnd.Next(65, 91)).ToString() + (char)Galaxy.Rnd.Next(65, 91);
        }

        public Habitat SelectBestCandidateForCapital()
        {
            Habitat result = null;
            int num = 0;
            for (int i = 0; i < Colonies.Count; i++)
            {
                Habitat habitat = Colonies[i];
                if (habitat.StrategicValue > num)
                {
                    result = habitat;
                    num = habitat.StrategicValue;
                }
            }
            return result;
        }

        public void SetPirateRelationEmpires(Galaxy galaxy)
        {
            if (PirateRelations != null)
            {
                PirateRelations.FixupEmpires(galaxy);
            }
        }

        public void SetAutomationSettings(GameOptions gameOptions)
        {
            ControlColonization = gameOptions.ControlColonizationDefault;
            ControlColonyTaxRates = gameOptions.ControlColonyTaxRatesDefault;
            ControlDesigns = gameOptions.ControlShipDesignDefault;
            ControlDiplomacyGifts = gameOptions.ControlDiplomaticGiftsDefault;
            ControlDiplomacyOffense = gameOptions.ControlWarTradeSanctionsDefault;
            ControlDiplomacyTreaties = gameOptions.ControlTreatyNegotiationDefault;
            ControlMilitaryAttacks = gameOptions.ControlAttacksOnEnemiesDefault;
            ControlMilitaryFleets = gameOptions.ControlFleetFormationDefault;
            ControlStateConstruction = gameOptions.ControlShipBuildingDefault;
            ControlTroopGeneration = gameOptions.ControlTroopRecruitmentDefault;
            ControlAgentAssignment = gameOptions.ControlAgentAssignmentDefault;
            ControlResearch = gameOptions.ControlResearchDefault;
            ControlColonyFacilities = gameOptions.ControlColonyFacilitiesDefault;
            ControlCharacterLocations = gameOptions.ControlCharacterLocationsDefault;
            ControlPopulationPolicy = gameOptions.ControlPopulationPolicyDefault;
            ControlOfferPirateMissions = gameOptions.ControlOfferPirateMissionsDefault;
            AttackOvermatchFactor = gameOptions.AttackOverMatchFactor;
            AttackRangePatrol = gameOptions.AttackRangePatrol;
            AttackRangeEscort = gameOptions.AttackRangeEscort;
            AttackRangeOther = gameOptions.AttackRangeOther;
            AttackRangeAttack = gameOptions.AttackRangeAttack;
            FleetAttackRefuelPortion = gameOptions.FleetAttackRefuelPortion;
            FleetAttackGatherPortion = gameOptions.FleetAttackGatherPortion;
            DiscoveryActionRuin = gameOptions.DiscoveryActionRuin;
            DiscoveryActionAbandonedShipBase = gameOptions.DiscoveryActionAbandonedShipBase;
            NewShipsAutomated = gameOptions.NewShipsAutomated;
        }

        public void SetAutomationSettingsFullyAutomated()
        {
            ControlColonization = AutomationLevel.FullyAutomated;
            ControlColonyTaxRates = true;
            ControlDesigns = true;
            ControlDiplomacyGifts = AutomationLevel.FullyAutomated;
            ControlDiplomacyOffense = AutomationLevel.FullyAutomated;
            ControlDiplomacyTreaties = AutomationLevel.FullyAutomated;
            ControlMilitaryAttacks = AutomationLevel.FullyAutomated;
            ControlMilitaryFleets = true;
            ControlStateConstruction = AutomationLevel.FullyAutomated;
            ControlTroopGeneration = true;
            ControlAgentAssignment = AutomationLevel.FullyAutomated;
            ControlResearch = true;
            ControlColonyFacilities = AutomationLevel.FullyAutomated;
            ControlCharacterLocations = true;
            ControlPopulationPolicy = true;
            ControlOfferPirateMissions = AutomationLevel.FullyAutomated;
            AttackOvermatchFactor = 2f;
            AttackRangePatrol = 48000;
            AttackRangeEscort = 2000;
            AttackRangeOther = 48000;
            AttackRangeAttack = 2000;
            FleetAttackRefuelPortion = 0.3f;
            FleetAttackGatherPortion = 0.3f;
            DiscoveryActionRuin = 0;
            DiscoveryActionAbandonedShipBase = 0;
            NewShipsAutomated = true;
        }

        public void SetDefaultsForLists()
        {
            _ColonizationTargets = new HabitatPrioritizationList();
            _ResourceTargets = new HabitatPrioritizationList();
            _EmpireResourceTargets = new HabitatPrioritizationList();
            _DesiredForeignColonies = new HabitatPrioritizationList();
            _EmpiresWithDesiredColonies = new EmpireList();
            _MonitoringHabitats = new HabitatList();
            _MonitoringPoints = new List<Point>();
            _ResearchHabitats = new HabitatList();
            _ResortHabitats = new PrioritizedTargetList();
            _MigrationSources = new PrioritizedTargetList();
            _MigrationDestinations = new PrioritizedTargetList();
            _TourismSources = new PrioritizedTargetList();
            _TourismDestinations = new PrioritizedTargetList();
            _ResortBaseBuildLocations = new PrioritizedTargetList();
            _RefuellingLocations = new StellarObjectList();
            _RefuellingLocationsMilitaryOnly = new StellarObjectList();
            if (SmugglingIncomeFactor == 0.0)
            {
                SmugglingIncomeFactor = 1.0;
            }
            if (RaidStrengthFactor == 0.0)
            {
                RaidStrengthFactor = 1.0;
            }
            if (RaidBonusFactor == 0.0)
            {
                RaidBonusFactor = 1.0;
            }
            if (ShipMaintenancePrivateFactor == 0.0)
            {
                ShipMaintenancePrivateFactor = 1.0;
            }
            if (ShipMaintenanceStateFactor == 0.0)
            {
                ShipMaintenanceStateFactor = 1.0;
            }
            if (ResearchWeaponsFactor == 0.0)
            {
                ResearchWeaponsFactor = 1.0;
            }
            if (ResearchEnergyFactor == 0.0)
            {
                ResearchEnergyFactor = 1.0;
            }
            if (ResearchHighTechFactor == 0.0)
            {
                ResearchHighTechFactor = 1.0;
            }
            if (PlanetaryFacilityEliminationFactor == 0.0)
            {
                PlanetaryFacilityEliminationFactor = 1.0;
            }
            if (LootingFactor == 0.0)
            {
                LootingFactor = 1.0;
            }
            if (PlanetaryFacilityBuildFactor == 0.0)
            {
                PlanetaryFacilityBuildFactor = 1.0;
            }
            if (PlanetaryWonderBuildFactor == 0.0)
            {
                PlanetaryWonderBuildFactor = 1.0;
            }
        }

        public Empire(Galaxy galaxy, string name, Habitat capital, Race dominantRace, int governmentId, double corruptionMultiplier, EmpirePolicy policy)
            : this(galaxy, name, capital, dominantRace, governmentId, corruptionMultiplier, policy, isPlayerEmpire: false)
        {
        }

        public Empire(Galaxy galaxy, string name, Habitat capital, Race dominantRace, int governmentId, double corruptionMultiplier, EmpirePolicy policy, bool isPlayerEmpire)
        {
            _Galaxy = galaxy;
            _Active = true;
            _EmpireId = _Galaxy.GetNextEmpireID();
            _Counters = new EmpireCounters(this);
            _PirateEconomy = new PirateEconomy(galaxy.CurrentStarDate);
            _ResourceMap.InitializeFlags(_Galaxy.Habitats.Count, _Galaxy);
            _Name = name;
            _Capital = capital;
            HomeWorld = _Capital;
            _DominantRace = dominantRace;
            CorruptionMultiplier = corruptionMultiplier;
            if (isPlayerEmpire && galaxy.ColonyNames != null && galaxy.ColonyNames.Count > galaxy.ColonyNameIndex)
            {
                string name2 = galaxy.ColonyNames[galaxy.ColonyNameIndex];
                galaxy.ColonyNameIndex++;
                capital.Name = name2;
            }
            LastDisasterDate = galaxy.CurrentStarDate;
            if (_DominantRace != null)
            {
                Policy.ResearchDesignOverallFocus = _DominantRace.ShipDesignFocus;
                Policy.ResearchDesignTechFocus1 = _DominantRace.TechFocus1;
                Policy.ResearchDesignTechFocus2 = _DominantRace.TechFocus2;
                Policy.ResearchDesignTechFocusType1 = _DominantRace.TechFocusType1;
                Policy.ResearchDesignTechFocusType2 = _DominantRace.TechFocusType2;
                if (!_DominantRace.Expanding)
                {
                    Reclusive = true;
                }
            }
            if (policy != null)
            {
                Policy = policy;
            }
            _AllowableGovernmentTypes = ResolveDefaultAllowableGovernmentTypes(_DominantRace, forceIncludeSpecialTypesIfRaceAllows: true);
            ChangeGovernment(governmentId);
            _DesignNamesIndex = _DominantRace.DesignNameIndex;
            if (string.IsNullOrEmpty(name))
            {
                _Name = GenerateEmpireName(governmentId);
            }
            BuiltObjects = new BuiltObjectList();
            ShipGroups = new ShipGroupList();
            Designs = new DesignList();
            LatestDesigns = new DesignList();
            Array values = Enum.GetValues(typeof(BuiltObjectSubRole));
            for (int i = 0; i < values.Length; i++)
            {
                LatestDesigns.Add(null);
            }
            ForeignDesigns = new DesignList();
            Characters = new CharacterList();
            Troops = new TroopList();
            IntelligenceMissions = new IntelligenceMissionList();
            Outlaws = new BuiltObjectList();
            DiplomaticRelations = new DiplomaticRelationList();
            _ProposedDiplomaticRelations = new DiplomaticRelationList();
            _ProposedDiplomaticRelations.InvertEmpireIndexing = true;
            Colonies = new HabitatList();
            ConstructionYards = new BuiltObjectList();
            DistressSignals = new DistressSignalList();
            Manufacturers = new BuiltObjectList();
            PrivateBuiltObjects = new BuiltObjectList();
            RefuellingDepots = new BuiltObjectList();
            ResourceExtractors = new BuiltObjectList();
            SpacePorts = new BuiltObjectList();
            MiningStations = new BuiltObjectList();
            Freighters = new BuiltObjectList();
            ConstructionShips = new BuiltObjectList();
            LongRangeScanners = new BuiltObjectList();
            ResearchFacilities = new BuiltObjectList();
            ResortBases = new BuiltObjectList();
            ResupplyShips = new BuiltObjectList();
            PlanetDestroyers = new BuiltObjectList();
            Messages = new EmpireMessageList();
            EmpireEvaluations = new EmpireEvaluationList();
            SystemVisibility = new SystemVisibilityList();
            for (int j = 0; j < _Galaxy.Systems.Count; j++)
            {
                SystemVisibility item = new SystemVisibility
                {
                    Status = SystemVisibilityStatus.Unexplored,
                    SystemStar = _Galaxy.Systems[j].SystemStar
                };
                SystemVisibility.Add(item);
            }
            ControlColonization = AutomationLevel.FullyAutomated;
            ControlColonyDevelopment = true;
            ControlColonyStockLevels = true;
            ControlColonyTaxRates = true;
            ControlDesigns = true;
            ControlDiplomacyGifts = AutomationLevel.FullyAutomated;
            ControlDiplomacyOffense = AutomationLevel.FullyAutomated;
            ControlDiplomacyTreaties = AutomationLevel.FullyAutomated;
            ControlMilitaryAttacks = AutomationLevel.FullyAutomated;
            ControlMilitaryFleets = true;
            ControlStateConstruction = AutomationLevel.FullyAutomated;
            ControlTroopGeneration = true;
            ControlAgentAssignment = AutomationLevel.FullyAutomated;
            ControlResearch = true;
            ControlColonyFacilities = AutomationLevel.FullyAutomated;
            ControlPopulationPolicy = true;
            ControlCharacterLocations = true;
            ControlOfferPirateMissions = AutomationLevel.FullyAutomated;
            SelectEmpireColors(isPirateFaction: false, out _MainColor, out _SecondaryColor);
            if (_DominantRace != null)
            {
                FlagShape = Galaxy.GenerateEmpireFlag(_MainColor, _SecondaryColor, _DominantRace.DefaultFlagShape, Galaxy.FlagShapes, ref _SmallFlagPicture, ref _LargeFlagPicture);
            }
            else
            {
                FlagShape = Galaxy.GenerateEmpireFlag(_MainColor, _SecondaryColor, -1, Galaxy.FlagShapes, ref _SmallFlagPicture, ref _LargeFlagPicture);
            }
            Habitat habitat = null;
            for (int k = 0; k < Galaxy.IndexMaxX; k++)
            {
                for (int l = 0; l < Galaxy.IndexMaxY; l++)
                {
                    HabitatList habitatList = galaxy.HabitatIndex[k][l];
                    for (int m = 0; m < habitatList.Count; m++)
                    {
                        if (habitatList[m] == _Capital)
                        {
                            if (_Capital.Category == HabitatCategoryType.Asteroid || _Capital.Category == HabitatCategoryType.Planet)
                            {
                                habitat = _Capital.Parent;
                                k = Galaxy.IndexMaxX;
                                l = Galaxy.IndexMaxY;
                                break;
                            }
                            if (_Capital.Category == HabitatCategoryType.Moon)
                            {
                                habitat = _Capital.Parent.Parent;
                                k = Galaxy.IndexMaxX;
                                l = Galaxy.IndexMaxY;
                                break;
                            }
                        }
                    }
                }
            }
            for (int n = 0; n < _Galaxy.Habitats.Count; n++)
            {
                Habitat habitat2 = _Galaxy.Habitats[n];
                bool known = false;
                if (habitat2.Category == HabitatCategoryType.Star)
                {
                    known = true;
                }
                if (habitat != null && (habitat2.Parent == habitat || (habitat2.Parent != null && habitat2.Parent.Parent == habitat)))
                {
                    known = true;
                    SystemVisibility[habitat2.SystemIndex].Status = SystemVisibilityStatus.Visible;
                }
                _ResourceMap.SetResourcesKnown(_Galaxy.Habitats[n], known);
            }
            if (_Capital != null)
            {
                if (_Capital.Troops == null)
                {
                    _Capital.Troops = new TroopList();
                }
                _TroopDescription = _DominantRace.TroopName;
                _TroopPictureRef = _DominantRace.PictureRef;
                _Capital.SetDevelopmentLevel(10);
            }
            _LastLongTouch = galaxy.CurrentDateTime.Subtract(new TimeSpan(0, 0, (int)_LongProcessingInterval + 1));
            _LastIntermediateTouch = _LastLongTouch;
            _LastPeriodicTouch = _LastLongTouch;
            _LastRegularTouch = _LastLongTouch;
            _LastShortTouch = _LastLongTouch;
            if (capital != null)
            {
                CargoList cargoList = new CargoList();
                if (capital.Cargo != null)
                {
                    foreach (Cargo item2 in capital.Cargo)
                    {
                        if (item2.EmpireId == _Galaxy.IndependentEmpire.EmpireId)
                        {
                            cargoList.Add(item2);
                        }
                    }
                    foreach (Cargo item3 in cargoList)
                    {
                        capital.Cargo.Remove(item3);
                        Cargo cargo = null;
                        if (item3.CommodityIsComponent)
                        {
                            cargo = new Cargo(item3.Component, item3.Amount, this, item3.Reserved);
                        }
                        else if (item3.CommodityIsResource)
                        {
                            cargo = new Cargo(item3.Resource, item3.Amount, this, item3.Reserved);
                        }
                        if (cargo != null)
                        {
                            capital.Cargo.Add(cargo);
                        }
                    }
                }
                SetStartupColonyResourceCargo(Capital);
            }
            _StateMoney = 30000.0;
            _PrivateMoney = 100000.0;
            _Research = new ResearchSystem();
            _Research.TechTree = Galaxy.ResearchNodeDefinitionsStatic.ObtainTechTree(dominantRace);
            _Research.TechTree = Galaxy.ResearchNodeDefinitionsStatic.SetTechTreeStartingDefaults(_Research.TechTree, dominantRace, policy);
            Research.Update(DominantRace);
            ReviewResearchAbilities();
            ReviewDesignsBuiltObjectsImprovedComponents();
            ReviewColonizationTypes();
            ReviewPopulationGrowthRates();
            int newSize = 0;
            ReviewMaximumConstructionSize(out newSize);
            ReviewCanBuildShipTypes();
            ReviewTroopTypes();
            LastLeaderChangeDate = _Galaxy.CurrentStarDate;
        }

        public int SetStartupColonyResourceCargo(Habitat colony)
        {
            int val = 1 + (int)(colony.Population.TotalAmount / 250000000);
            val = Math.Min(10, val);
            double num = Galaxy.ColonyAnnualResourceConsumptionRate * ((double)colony.Population.TotalAmount / 15.0);
            if (num < 1.0)
            {
                num = 1.0;
            }
            else if (num > 5.0)
            {
                num = 5.0;
            }
            Cargo cargo = null;
            if (colony.Cargo == null)
            {
                colony.Cargo = new CargoList();
            }
            for (int i = 0; i < _Galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; i++)
            {
                ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance[i];
                if (resourceDefinition != null && resourceDefinition.ColonyManufacturingLevel <= 0)
                {
                    cargo = new Cargo(new Resource(resourceDefinition.ResourceID), (int)((double)(resourceDefinition.RelativeImportance * 6000f) * num), this);
                    colony.Cargo.Add(cargo);
                }
            }
            if (_Galaxy.ResourceSystem.LuxuryResources.Count != 0)
            {
                for (int j = 0; j < 4; j++)
                {
                    int index = Galaxy.Rnd.Next(0, _Galaxy.ResourceSystem.LuxuryResources.Count);
                    ResourceDefinition resourceDefinition2 = _Galaxy.ResourceSystem.LuxuryResources[index];
                    if (resourceDefinition2 != null && resourceDefinition2.SuperLuxuryBonusAmount <= 0 && resourceDefinition2.ColonyManufacturingLevel <= 0)
                    {
                        cargo = new Cargo(new Resource(resourceDefinition2.ResourceID), 600, this);
                        colony.Cargo.Add(cargo);
                    }
                }
                long num2 = Math.Max(500000000L, colony.Population.TotalAmount);
                int num3 = (int)(Galaxy.ColonyAnnualLuxuryResourceConsumptionRate * (double)num2 * 5.0);
                num3 = Math.Max(num3 * 3, Galaxy.MinimumLuxuryResourceReorderAmount);
                num3 = Math.Max(400, num3);
                num3 = (int)((double)num3 * 1.5);
                for (int k = 0; k < val; k++)
                {
                    Resource resource = _Galaxy.SelectRandomLuxuryResource();
                    int num4 = colony.Cargo.IndexOf(resource, this);
                    int num5 = 0;
                    while (num4 >= 0 && num5 < 10)
                    {
                        resource = _Galaxy.SelectRandomLuxuryResource();
                        num4 = colony.Cargo.IndexOf(resource, this);
                        num5++;
                    }
                    if (num4 >= 0)
                    {
                        resource = _Galaxy.SelectRandomLuxuryResource();
                    }
                    cargo = new Cargo(new Resource(resource.ResourceID), num3, this);
                    colony.Cargo.Add(cargo);
                }
            }
            return val;
        }

        public static List<int> ResolveRaceSpecificGovernmentTypes(Race dominantRace)
        {
            List<int> list = new List<int>();
            if (dominantRace.SpecialGovernmentId >= 0)
            {
                list.Add(dominantRace.SpecialGovernmentId);
            }
            return list;
        }

        public static List<int> ResolveDefaultAllowableGovernmentTypes(Race dominantRace)
        {
            return ResolveDefaultAllowableGovernmentTypes(dominantRace, forceIncludeSpecialTypesIfRaceAllows: false);
        }

        public static List<int> ResolveDefaultAllowableGovernmentTypes(Race dominantRace, bool forceIncludeSpecialTypesIfRaceAllows)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < Galaxy.GovernmentsStatic.Count; i++)
            {
                GovernmentAttributes governmentAttributes = Galaxy.GovernmentsStatic[i];
                if (governmentAttributes == null)
                {
                    continue;
                }
                bool flag = true;
                if (dominantRace != null && dominantRace.DisallowedGovernmentIds.Contains(governmentAttributes.GovernmentId))
                {
                    flag = false;
                }
                if (!flag)
                {
                    continue;
                }
                switch (governmentAttributes.Availability)
                {
                    case 0:
                        list.Add(governmentAttributes.GovernmentId);
                        break;
                    case 1:
                        if (dominantRace != null && dominantRace.SpecialGovernmentId == governmentAttributes.GovernmentId)
                        {
                            list.Add(governmentAttributes.GovernmentId);
                        }
                        break;
                    case 2:
                        if (dominantRace != null && (forceIncludeSpecialTypesIfRaceAllows || dominantRace.Name == "Mechanoid") && dominantRace.SpecialGovernmentId == governmentAttributes.GovernmentId)
                        {
                            list.Add(governmentAttributes.GovernmentId);
                        }
                        else if (dominantRace != null && dominantRace.SpecialGovernmentId == governmentAttributes.GovernmentId)
                        {
                            list.Add(governmentAttributes.GovernmentId);
                        }
                        break;
                    case 3:
                        if (dominantRace != null && (forceIncludeSpecialTypesIfRaceAllows || dominantRace.Name == "Shakturi") && dominantRace.SpecialGovernmentId == governmentAttributes.GovernmentId)
                        {
                            list.Add(governmentAttributes.GovernmentId);
                        }
                        else if (dominantRace != null && dominantRace.SpecialGovernmentId == governmentAttributes.GovernmentId)
                        {
                            list.Add(governmentAttributes.GovernmentId);
                        }
                        break;
                }
            }
            return list;
        }

        public void GenerateDesignSpecifications(Galaxy galaxy, Race dominantRace, bool isPirate, string raceNameOverride)
        {
            _DesignSpecifications.Clear();
            PlanetDestroyerDesignSpecification = null;
            if (!isPirate)
            {
                PlanetDestroyerDesignSpecification = DesignSpecification.LoadFromFile(galaxy.ApplicationStartupPath, galaxy.CustomizationSetPath, "PlanetDestroyer", BuiltObjectSubRole.CapitalShip, isMobile: true, dominantRace, isPirate, standAlone: true, raceNameOverride);
            }
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "CapitalShip", BuiltObjectSubRole.CapitalShip, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "Carrier", BuiltObjectSubRole.Carrier, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "ColonyShip", BuiltObjectSubRole.ColonyShip, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "ConstructionShip", BuiltObjectSubRole.ConstructionShip, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "Cruiser", BuiltObjectSubRole.Cruiser, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "DefensiveBase", BuiltObjectSubRole.DefensiveBase, isMobile: false, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "Destroyer", BuiltObjectSubRole.Destroyer, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "EnergyResearchStation", BuiltObjectSubRole.EnergyResearchStation, isMobile: false, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "Escort", BuiltObjectSubRole.Escort, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "ExplorationShip", BuiltObjectSubRole.ExplorationShip, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "Frigate", BuiltObjectSubRole.Frigate, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "GasMiningShip", BuiltObjectSubRole.GasMiningShip, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "GasMiningStation", BuiltObjectSubRole.GasMiningStation, isMobile: false, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "HighTechResearchStation", BuiltObjectSubRole.HighTechResearchStation, isMobile: false, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "LargeFreighter", BuiltObjectSubRole.LargeFreighter, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "LargeSpacePort", BuiltObjectSubRole.LargeSpacePort, isMobile: false, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "MediumFreighter", BuiltObjectSubRole.MediumFreighter, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "MediumSpacePort", BuiltObjectSubRole.MediumSpacePort, isMobile: false, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "MiningShip", BuiltObjectSubRole.MiningShip, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "MiningStation", BuiltObjectSubRole.MiningStation, isMobile: false, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "MonitoringStation", BuiltObjectSubRole.MonitoringStation, isMobile: false, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "PassengerShip", BuiltObjectSubRole.PassengerShip, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "ResortBase", BuiltObjectSubRole.ResortBase, isMobile: false, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "ResupplyShip", BuiltObjectSubRole.ResupplyShip, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "SmallFreighter", BuiltObjectSubRole.SmallFreighter, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "OutPost", BuiltObjectSubRole.Outpost, isMobile: false, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "SmallSpacePort", BuiltObjectSubRole.SmallSpacePort, isMobile: false, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "TroopTransport", BuiltObjectSubRole.TroopTransport, isMobile: true, dominantRace, isPirate, raceNameOverride));
            _DesignSpecifications.Add(DesignSpecification.LoadFromFile(galaxy, "WeaponsResearchStation", BuiltObjectSubRole.WeaponsResearchStation, isMobile: false, dominantRace, isPirate, raceNameOverride));
        }

        public Empire(Galaxy galaxy, string name, bool isIndependentEmpire, Habitat homeHabitat, Race dominantRace, EmpirePolicy policy)
        {
            _Galaxy = galaxy;
            _Active = true;
            if (isIndependentEmpire)
            {
                _EmpireId = 0;
            }
            else
            {
                _EmpireId = _Galaxy.GetNextEmpireID();
            }
            _Counters = new EmpireCounters(this);
            _PirateEconomy = new PirateEconomy(galaxy.CurrentStarDate);
            _ResourceMap.InitializeFlags(_Galaxy.Habitats.Count, _Galaxy);
            _Name = name;
            _DominantRace = dominantRace;
            LastDisasterDate = galaxy.CurrentStarDate;
            if (policy != null && !isIndependentEmpire)
            {
                Policy = policy;
            }
            if (string.IsNullOrEmpty(name))
            {
                _Name = TextResolver.GetText("Independent");
            }
            _AllowableGovernmentTypes = ResolveDefaultAllowableGovernmentTypes(dominantRace, forceIncludeSpecialTypesIfRaceAllows: true);
            BuiltObjects = new BuiltObjectList();
            ShipGroups = new ShipGroupList();
            Designs = new DesignList();
            LatestDesigns = new DesignList();
            Array values = Enum.GetValues(typeof(BuiltObjectSubRole));
            for (int i = 0; i < values.Length; i++)
            {
                LatestDesigns.Add(null);
            }
            ForeignDesigns = new DesignList();
            Characters = new CharacterList();
            Troops = new TroopList();
            IntelligenceMissions = new IntelligenceMissionList();
            Outlaws = new BuiltObjectList();
            DiplomaticRelations = new DiplomaticRelationList();
            _ProposedDiplomaticRelations = new DiplomaticRelationList();
            _ProposedDiplomaticRelations.InvertEmpireIndexing = true;
            Colonies = new HabitatList();
            ConstructionYards = new BuiltObjectList();
            DistressSignals = new DistressSignalList();
            Manufacturers = new BuiltObjectList();
            PrivateBuiltObjects = new BuiltObjectList();
            RefuellingDepots = new BuiltObjectList();
            ResourceExtractors = new BuiltObjectList();
            SpacePorts = new BuiltObjectList();
            MiningStations = new BuiltObjectList();
            Freighters = new BuiltObjectList();
            ConstructionShips = new BuiltObjectList();
            LongRangeScanners = new BuiltObjectList();
            ResearchFacilities = new BuiltObjectList();
            ResortBases = new BuiltObjectList();
            ResupplyShips = new BuiltObjectList();
            PlanetDestroyers = new BuiltObjectList();
            Messages = new EmpireMessageList();
            EmpireEvaluations = new EmpireEvaluationList();
            SystemVisibility = new SystemVisibilityList();
            for (int j = 0; j < _Galaxy.Systems.Count; j++)
            {
                SystemVisibility item = new SystemVisibility
                {
                    Status = SystemVisibilityStatus.Unexplored,
                    SystemStar = _Galaxy.Systems[j].SystemStar
                };
                SystemVisibility.Add(item);
            }
            ControlColonization = AutomationLevel.FullyAutomated;
            ControlColonyDevelopment = true;
            ControlColonyStockLevels = true;
            ControlColonyTaxRates = true;
            ControlDesigns = true;
            ControlDiplomacyGifts = AutomationLevel.FullyAutomated;
            ControlDiplomacyOffense = AutomationLevel.FullyAutomated;
            ControlDiplomacyTreaties = AutomationLevel.FullyAutomated;
            ControlMilitaryAttacks = AutomationLevel.FullyAutomated;
            ControlMilitaryFleets = true;
            ControlStateConstruction = AutomationLevel.FullyAutomated;
            ControlTroopGeneration = true;
            ControlAgentAssignment = AutomationLevel.FullyAutomated;
            ControlResearch = true;
            ControlPopulationPolicy = true;
            ControlColonyFacilities = AutomationLevel.FullyAutomated;
            ControlCharacterLocations = true;
            ControlOfferPirateMissions = AutomationLevel.FullyAutomated;
            SelectEmpireColors(isPirateFaction: false, out _MainColor, out _SecondaryColor);
            if (isIndependentEmpire)
            {
                _MainColor = Color.FromArgb(96, 96, 96);
                _SecondaryColor = Color.FromArgb(96, 96, 96);
            }
            FlagShape = Galaxy.GenerateEmpireFlag(_MainColor, _SecondaryColor, -1, Galaxy.FlagShapes, ref _SmallFlagPicture, ref _LargeFlagPicture);
            if (homeHabitat != null)
            {
                _ResourceMap.SetResourcesKnown(galaxy.Systems[homeHabitat.SystemIndex].SystemStar, known: true);
                for (int k = 0; k > galaxy.Systems[homeHabitat.SystemIndex].Habitats.Count; k++)
                {
                    _ResourceMap.SetResourcesKnown(galaxy.Systems[homeHabitat.SystemIndex].Habitats[k], known: true);
                }
                int num = (int)(2.0 * Math.Sqrt(galaxy.StarCount));
                for (int l = 0; l < num; l++)
                {
                    Habitat habitat = galaxy.FastFindNearestUnexploredSystem(homeHabitat.Xpos, homeHabitat.Ypos, this);
                    if (habitat != null)
                    {
                        SystemVisibility[habitat.SystemIndex].Status = SystemVisibilityStatus.Explored;
                        _ResourceMap.SetResourcesKnown(galaxy.Systems[habitat].SystemStar, known: true);
                        for (int m = 0; m < galaxy.Systems[habitat].Habitats.Count; m++)
                        {
                            _ResourceMap.SetResourcesKnown(galaxy.Systems[habitat].Habitats[m], known: true);
                        }
                    }
                }
            }
            else
            {
                for (int n = 0; n < _Galaxy.Habitats.Count; n++)
                {
                    _ResourceMap.SetResourcesKnown(_Galaxy.Habitats[n], known: true);
                }
                for (int num2 = 0; num2 < SystemVisibility.Count; num2++)
                {
                    SystemVisibility[num2].Status = SystemVisibilityStatus.Visible;
                }
            }
            if (galaxy.Age > 0)
            {
                for (int num3 = 0; num3 < galaxy.Empires.Count; num3++)
                {
                    Empire empire = galaxy.Empires[num3];
                    if (empire == null || !empire.Active || empire == this)
                    {
                        continue;
                    }
                    for (int num4 = 0; num4 < galaxy.Systems.Count; num4++)
                    {
                        SystemInfo systemInfo = galaxy.Systems[num4];
                        SystemVisibilityStatus status = empire.SystemVisibility[systemInfo.SystemStar.SystemIndex].Status;
                        SystemVisibilityStatus status2 = SystemVisibility[systemInfo.SystemStar.SystemIndex].Status;
                        bool flag = false;
                        if ((status2 == SystemVisibilityStatus.Explored || status2 == SystemVisibilityStatus.Visible) && status == SystemVisibilityStatus.Visible)
                        {
                            flag = true;
                        }
                        if ((status == SystemVisibilityStatus.Explored || status == SystemVisibilityStatus.Visible) && (status2 == SystemVisibilityStatus.Explored || status2 == SystemVisibilityStatus.Visible) && Galaxy.Rnd.Next(0, 3) == 1)
                        {
                            flag = true;
                        }
                        if (!flag)
                        {
                            continue;
                        }
                        PirateRelation pirateRelation = ObtainPirateRelation(empire);
                        if (pirateRelation.Type == PirateRelationType.NotMet)
                        {
                            ChangePirateRelation(empire, PirateRelationType.None, galaxy.CurrentStarDate);
                            if (PirateEmpireBaseHabitat != null && empire.KnownPirateEmpires != null && !empire.KnownPirateEmpires.Contains(this))
                            {
                                empire.KnownPirateEmpires.Add(this);
                            }
                            if (empire.PirateEmpireBaseHabitat != null && KnownPirateEmpires != null && !KnownPirateEmpires.Contains(empire))
                            {
                                KnownPirateEmpires.Add(empire);
                            }
                        }
                        break;
                    }
                }
            }
            _LastLongTouch = galaxy.CurrentDateTime.Subtract(new TimeSpan(0, 0, (int)_LongProcessingInterval + 1));
            _LastIntermediateTouch = _LastLongTouch;
            _LastPeriodicTouch = _LastLongTouch;
            _LastRegularTouch = _LastLongTouch;
            _LastShortTouch = _LastLongTouch;
            _StateMoney = 30000.0;
            _PrivateMoney = 100000.0;
            _Research = new ResearchSystem();
            _Research.TechTree = Galaxy.ResearchNodeDefinitionsStatic.ObtainTechTree(dominantRace);
            _Research.TechTree = Galaxy.ResearchNodeDefinitionsStatic.SetTechTreeStartingDefaults(_Research.TechTree, dominantRace, policy);
            Research.Update(DominantRace);
            ReviewResearchAbilities();
            ReviewDesignsBuiltObjectsImprovedComponents();
            ReviewColonizationTypes();
            ReviewPopulationGrowthRates();
            int newSize = 0;
            ReviewMaximumConstructionSize(out newSize);
            ReviewCanBuildShipTypes();
            ReviewTroopTypes();
        }

        public BuiltObject GenerateBuiltObjectFromDesign(Design design, string name, bool isState, double x, double y)
        {
            BuiltObject builtObject = new BuiltObject(design, name, _Galaxy);
            for (int i = 0; i < builtObject.Components.Count; i++)
            {
                builtObject.Components[i].Status = ComponentStatus.Normal;
            }
            builtObject.BuiltObjectID = _Galaxy.GetNextBuiltObjectID();
            builtObject.Empire = this;
            builtObject.Xpos = x;
            builtObject.Ypos = y;
            if (design.SubRole == BuiltObjectSubRole.ColonyShip)
            {
                builtObject.NativeRace = _Galaxy.SelectRandomRace(Galaxy.HabitatToEmpireMinimumIntelligence);
            }
            builtObject.ReDefine();
            builtObject.CurrentFuel = builtObject.FuelCapacity;
            if (isState)
            {
                builtObject.Owner = this;
                BuiltObjects.Add(builtObject);
            }
            else
            {
                PrivateBuiltObjects.Add(builtObject);
            }
            _Galaxy.BuiltObjects.Add(builtObject);
            int x2 = (int)x / Galaxy.IndexSize;
            int y2 = (int)y / Galaxy.IndexSize;
            Galaxy.CorrectIndexCoords(ref x2, ref y2);
            _Galaxy.BuiltObjectIndex[x2][y2].Add(builtObject);
            Habitat habitat = _Galaxy.FastFindNearestSystem(builtObject.Xpos, builtObject.Ypos);
            if (habitat != null)
            {
                double num = _Galaxy.CalculateDistance(builtObject.Xpos, builtObject.Ypos, habitat.Xpos, habitat.Ypos);
                if (num < (double)Galaxy.MaxSolarSystemSize + 500.0)
                {
                    builtObject.NearestSystemStar = habitat;
                }
            }
            builtObject.ReDefine();
            return builtObject;
        }

        public void SelectEmpireColors(bool isPirateFaction, out Color mainColor, out Color secondaryColor)
        {
            bool flag = false;
            mainColor = Color.Empty;
            secondaryColor = Color.Empty;
            int iterationCount = 0;
            while (Galaxy.ConditionCheckLimit(!flag, 200, ref iterationCount))
            {
                int unusedColorKey = 0;
                Color color = Color.Empty;
                if (DominantRace != null)
                {
                    color = ((!isPirateFaction) ? DominantRace.DefaultMainColor : DominantRace.DefaultMainColorPirates);
                }
                if (DominantRace != null && !_Galaxy.CheckEmpireColorUsed(isPirateFaction, color))
                {
                    mainColor = color;
                    secondaryColor = DominantRace.DefaultSecondaryColor;
                }
                else
                {
                    mainColor = _Galaxy.SelectUnusedMainColor(isPirateFaction, out unusedColorKey);
                    if (unusedColorKey < 0)
                    {
                        if (isPirateFaction)
                        {
                            secondaryColor = Color.FromArgb(254, 254, 254);
                        }
                        else
                        {
                            secondaryColor = Galaxy.SelectColorFromKey(Galaxy.Rnd.Next(0, 23));
                        }
                    }
                    else
                    {
                        secondaryColor = Galaxy.SelectColorFromKey(Galaxy.SelectComplementaryColorKey(unusedColorKey));
                    }
                }
                if (isPirateFaction)
                {
                    secondaryColor = Galaxy.DetermineSecondaryColor(mainColor);
                }
                flag = true;
                if (isPirateFaction)
                {
                    for (int i = 0; i < _Galaxy.PirateEmpires.Count; i++)
                    {
                        Empire empire = _Galaxy.PirateEmpires[i];
                        if (empire.MainColor.ToArgb() == mainColor.ToArgb() && empire.SecondaryColor.ToArgb() == secondaryColor.ToArgb())
                        {
                            flag = false;
                            break;
                        }
                        if (mainColor.ToArgb() == secondaryColor.ToArgb())
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < _Galaxy.Empires.Count; j++)
                    {
                        Empire empire2 = _Galaxy.Empires[j];
                        if (empire2.MainColor.ToArgb() == mainColor.ToArgb() && empire2.SecondaryColor.ToArgb() == secondaryColor.ToArgb())
                        {
                            flag = false;
                            break;
                        }
                        if (mainColor.ToArgb() == secondaryColor.ToArgb())
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (mainColor == secondaryColor)
                {
                    flag = false;
                }
            }
        }

        private bool CheckEmpireNameInUse(string empireName)
        {
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire.Name == empireName)
                {
                    return true;
                }
            }
            return false;
        }

        private string GenerateEmpireName(int governmentId)
        {
            string text = string.Empty;
            GovernmentAttributes governmentAttributes = null;
            if (governmentId >= 0 && governmentId < _Galaxy.Governments.Count)
            {
                governmentAttributes = _Galaxy.Governments[governmentId];
            }
            string text2 = string.Empty;
            string empty = string.Empty;
            string empty2 = string.Empty;
            bool flag = true;
            int num = 0;
            while (flag && num < 50)
            {
                text = string.Empty;
                if ((Galaxy.Rnd.Next(0, 2) == 1 && _DominantRace.Name.ToLower(CultureInfo.InvariantCulture) != "human") || _Capital == null)
                {
                    empty = _DominantRace.Name;
                }
                else
                {
                    Habitat habitat = Galaxy.DetermineHabitatSystemStar(_Capital);
                    empty = habitat.Name;
                }
                List<string> list = new List<string>();
                List<string> list2 = new List<string>();
                if (governmentAttributes != null)
                {
                    list = governmentAttributes.EmpireNameAdjectives;
                    list2 = governmentAttributes.EmpireNameNouns;
                }
                if (list.Count == 0)
                {
                    List<string> list3 = new List<string>();
                    list3.Add("United");
                    list3.Add("Combined");
                    list3.Add("Imperial");
                    list3.Add("Great");
                    list3.Add("Grand");
                    list = list3;
                }
                if (list2.Count == 0)
                {
                    List<string> list4 = new List<string>();
                    list4.Add("Empire");
                    list4.Add("Alliance");
                    list4.Add("Group");
                    list4.Add("Dominion");
                    list4.Add("Territory");
                    list4.Add("Nation");
                    list4.Add("Realm");
                    list4.Add("Federation");
                    list4.Add("Authority");
                    list4.Add("Enclave");
                    list4.Add("Confederacy");
                    list4.Add("Coalition");
                    list4.Add("Domain");
                    list2 = list4;
                }
                empty2 = list2[Galaxy.Rnd.Next(0, list2.Count)];
                string text3 = empty + " " + empty2;
                if (Galaxy.Rnd.Next(0, 4) == 1 && text3.Length < 18 && list.Count > 0)
                {
                    text2 = list[Galaxy.Rnd.Next(0, list.Count)];
                }
                if (!string.IsNullOrEmpty(text2))
                {
                    text = text + text2 + " ";
                }
                text = text + empty + " ";
                text += empty2;
                flag = CheckEmpireNameInUse(text);
                num++;
            }
            return text;
        }

        private string GenerateEmpireName()
        {
            int num = 0;
            string empty = string.Empty;
            string text = string.Empty;
            string text2 = string.Empty;
            string text3 = string.Empty;
            while (true)
            {
                num++;
                empty = ((Galaxy.Rnd.Next(0, 2) != 1 && !(DominantRace.Name == "Human")) ? DominantRace.Name : ((_Capital != null && (_Capital.Category == HabitatCategoryType.Planet || _Capital.Category == HabitatCategoryType.Asteroid)) ? _Capital.Parent.Name : ((_Capital == null || _Capital.Category != HabitatCategoryType.Moon) ? DominantRace.Name : _Capital.Parent.Parent.Name)));
                int num2 = Galaxy.Rnd.Next(0, 62);
                if (num2 >= 0 && num2 <= 11)
                {
                    text = "Empire";
                }
                if (num2 >= 12 && num2 <= 21)
                {
                    text = "Republic";
                }
                if (num2 >= 22 && num2 <= 30)
                {
                    text = "Alliance";
                }
                if (num2 >= 31 && num2 <= 37)
                {
                    text = "Union";
                }
                if (num2 >= 38 && num2 <= 42)
                {
                    text = "Collective";
                }
                if (num2 >= 43 && num2 <= 46)
                {
                    text = "Group";
                }
                if (num2 >= 47 && num2 <= 51)
                {
                    text = "Dominion";
                }
                if (num2 >= 52 && num2 <= 56)
                {
                    text = "Territory";
                }
                if (num2 >= 57 && num2 <= 58)
                {
                    text = "Kingdom";
                }
                if (num2 >= 59 && num2 <= 59)
                {
                    text = "Cooperative";
                }
                if (num2 >= 60 && num2 <= 60)
                {
                    text = "Nation";
                }
                if (num2 >= 61 && num2 <= 61)
                {
                    text = "Realm";
                }
                if (Galaxy.Rnd.Next(0, 2) == 1)
                {
                    int num3 = Galaxy.Rnd.Next(0, 36);
                    if (num3 >= 0 && num3 <= 9)
                    {
                        text2 = "United";
                    }
                    if (num3 >= 10 && num3 <= 16)
                    {
                        text2 = "Great";
                    }
                    if (num3 >= 17 && num3 <= 21)
                    {
                        text2 = "Grand";
                    }
                    if (num3 >= 22 && num3 <= 27)
                    {
                        text2 = "Imperial";
                    }
                    if (num3 >= 28 && num3 <= 32)
                    {
                        text2 = "Royal";
                    }
                    if (num3 >= 33 && num3 <= 35)
                    {
                        text2 = "Combined";
                    }
                }
                if (text2 != string.Empty)
                {
                    text3 = text3 + text2 + " ";
                }
                text3 = text3 + empty + " ";
                text3 += text;
                if (IsEmpireNameInUse(text3))
                {
                    int x = (int)_Capital.Xpos / Galaxy.SectorSize;
                    int y = (int)_Capital.Ypos / Galaxy.SectorSize;
                    Galaxy.CorrectSectorCoords(ref x, ref y);
                    text3 = ((x <= 3 || x >= 8 || y <= 3 || y >= 8) ? ("Outer " + empty + " " + text) : ("Central " + empty + " " + text));
                    if (IsEmpireNameInUse(text3))
                    {
                        text3 = "New " + empty + " " + text;
                        if (IsEmpireNameInUse(text3) && num < 10)
                        {
                            text2 = string.Empty;
                            empty = string.Empty;
                            text = string.Empty;
                            text3 = string.Empty;
                            continue;
                        }
                    }
                }
                if (text3.Length <= 26 || num >= 10)
                {
                    break;
                }
                text2 = string.Empty;
                empty = string.Empty;
                text = string.Empty;
                text3 = string.Empty;
            }
            return text3;
        }

        private bool IsEmpireNameInUse(string name)
        {
            for (int i = 0; i < _Galaxy.Empires.Count; i++)
            {
                Empire empire = _Galaxy.Empires[i];
                if (empire.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddHistoryMessage(EmpireMessage message)
        {
            lock (_MessageHistoryLock)
            {
                if (!_MessageHistory.Contains(message))
                {
                    _MessageHistory.Add(message);
                }
            }
        }

        public void RemoveOldHistoryMessages()
        {
            lock (_MessageHistoryLock)
            {
                if (_MessageHistory.Count <= _MaximumHistoryMessages)
                {
                    return;
                }
                _MessageHistory.Sort();
                _MessageHistory.Reverse();
                EmpireMessageList empireMessageList = new EmpireMessageList();
                for (int i = _MaximumHistoryMessages; i < _MessageHistory.Count; i++)
                {
                    if (_MessageHistory[i].MessageType != EmpireMessageType.GalacticHistory)
                    {
                        empireMessageList.Add(_MessageHistory[i]);
                    }
                }
                for (int j = 0; j < empireMessageList.Count; j++)
                {
                    _MessageHistory.Remove(empireMessageList[j]);
                }
            }
        }

        private void ExertCulturalInfluence()
        {
            HabitatList habitatList = DetermineEmpireSystems(this);
            for (int i = 0; i < habitatList.Count; i++)
            {
                Habitat habitat = habitatList[i];
                int num = 0;
                for (int j = 0; j < Colonies.Count; j++)
                {
                    Habitat habitat2 = Colonies[j];
                    switch (habitat2.Category)
                    {
                        case HabitatCategoryType.Planet:
                        case HabitatCategoryType.Asteroid:
                            if (habitat2.Parent == habitat)
                            {
                                num += habitat2.StrategicValue;
                            }
                            break;
                        case HabitatCategoryType.Moon:
                            if (habitat2.Parent.Parent == habitat)
                            {
                                num += habitat2.StrategicValue;
                            }
                            break;
                        default:
                            if (habitat2 == habitat)
                            {
                                num += habitat2.StrategicValue;
                            }
                            break;
                    }
                }
                EmpireList empireList = new EmpireList();
                List<int> list = new List<int>();
                HabitatList habitats = _Galaxy.Systems[habitat.SystemIndex].Habitats;
                for (int k = 0; k < habitats.Count; k++)
                {
                    Habitat habitat3 = habitats[k];
                    if (habitat3.Empire == null || habitat3.Empire == this)
                    {
                        continue;
                    }
                    if (!empireList.Contains(habitat3.Empire))
                    {
                        empireList.Add(habitat3.Empire);
                        list.Add(habitat3.StrategicValue);
                        continue;
                    }
                    int num2 = empireList.IndexOf(habitat3.Empire);
                    if (num2 >= 0)
                    {
                        list[num2] += habitat3.StrategicValue;
                    }
                }
                int num3 = 0;
                foreach (int item in list)
                {
                    num3 += item;
                }
                for (int l = 0; l < habitats.Count; l++)
                {
                    Habitat habitat4 = habitats[l];
                    if (habitat4.Empire != null && habitat4.Empire == this)
                    {
                        double num4 = (double)habitat4.StrategicValue / ((double)num3 / 5.0);
                        if (num4 < 1.0)
                        {
                            habitat4.CulturalDistressFactor = (float)((1.0 - num4) * 30.0);
                        }
                        else
                        {
                            habitat4.CulturalDistressFactor = 0f;
                        }
                    }
                    else
                    {
                        if (habitat4.Empire == null || habitat4.Empire == this || !habitat4.Rebelling)
                        {
                            continue;
                        }
                        int num5 = empireList.IndexOf(habitat4.Empire);
                        if (num5 < 0)
                        {
                            continue;
                        }
                        double num6 = 1.0;
                        if (empireList[num5].DominantRace != null)
                        {
                            num6 = Math.Pow((double)empireList[num5].DominantRace.LoyaltyLevel / 100.0, 2.0);
                            num6 += habitat4.EmpireApprovalRating / 100.0;
                        }
                        double num7 = 1.0 - CivilityRating / 100.0;
                        int num8 = 0;
                        if (habitat4.Troops != null && habitat4.Troops.Count > 0 && habitat4.Troops.TotalDefendStrength > 0 && habitat4.Population != null && habitat4.Population.TotalAmount > 0)
                        {
                            int num9 = (int)(Math.Sqrt(habitat4.Population.TotalAmount) / 10.0);
                            if (num9 > 0)
                            {
                                num8 = Troops.TotalDefendStrength / num9;
                            }
                        }
                        double num10 = (double)list[num5] / ((double)num / 10.0);
                        num10 *= num6;
                        num10 *= num7;
                        if (num8 >= 40 || !(Galaxy.Rnd.NextDouble() > num10))
                        {
                            continue;
                        }
                        TakeOwnershipOfColony(habitat4, this);
                        habitat4.CulturalDistressFactor = 0f;
                        string text = string.Format(TextResolver.GetText("There has been a revolution on X"), habitat4.Name);
                        string text2 = " - " + TextResolver.GetText("the inhabitants have switched allegiance and joined us!");
                        SendMessageToEmpire(this, EmpireMessageType.ColonyGained, habitat4, text + text2);
                        text2 = " - " + string.Format(TextResolver.GetText("the inhabitants have treacherously betrayed us and joined the X"), Name);
                        empireList[num5].SendMessageToEmpire(empireList[num5], EmpireMessageType.ColonyLost, habitat4, text + text2);
                        if (habitat4.Population == null || habitat4.Population.DominantRace == null)
                        {
                            continue;
                        }
                        RaceList newAbilityRaces = new RaceList();
                        Race raceChanged = null;
                        List<string> list2 = ReviewEmpireAbilityBonuses(out newAbilityRaces, out raceChanged);
                        if (list2.Count <= 0 || raceChanged == null)
                        {
                            continue;
                        }
                        string text3 = string.Format(TextResolver.GetText("The recent revolt PLANETTYPE NAME RACE"), Galaxy.ResolveDescription(habitat4.Category).ToLower(CultureInfo.InvariantCulture), habitat4.Name, raceChanged.Name);
                        text3 += ":\n";
                        foreach (string item2 in list2)
                        {
                            text3 = text3 + "\n" + item2;
                        }
                        string text4 = TextResolver.GetText("New Ability for our Empire");
                        SendEventMessageToEmpire(EventMessageType.NewEmpireRaceAbility, text4, text3, raceChanged, habitat4);
                    }
                }
            }
        }

        public void CompleteTeardown(Empire conqueror)
        {
            CompleteTeardown(conqueror, removeFromGalaxy: true, sendMessages: true);
        }

        public void CompleteTeardown(Empire conqueror, bool removeFromGalaxy, bool sendMessages)
        {
            short matchingGameEventIdEmpireEliminated = _Galaxy.GetMatchingGameEventIdEmpireEliminated(this, conqueror);
            _Galaxy.CheckTriggerEvent(matchingGameEventIdEmpireEliminated, this, EventTriggerType.EmpireEliminated, this);
            if (conqueror != null && conqueror.Counters != null)
            {
                conqueror.Counters.ProcessEmpireElimination(this, _Galaxy, conqueror);
            }
            _Active = false;
            SendNewsBroadcast(EventMessageType.Undefined, this, DisasterEventType.Undefined, warStartEnd: false, wonderBegun: false, EmpireMessageType.EmpireDefeated, conqueror);
            EmpireList empireList = new EmpireList();
            empireList.Add(_Galaxy.IndependentEmpire);
            empireList.AddRange(_Galaxy.Empires);
            empireList.AddRange(_Galaxy.PirateEmpires);
            for (int i = 0; i < empireList.Count; i++)
            {
                Empire empire = empireList[i];
                if (empire == null || empire == this)
                {
                    continue;
                }
                if (PirateEmpireBaseHabitat == null && empire.PirateEmpireBaseHabitat == null)
                {
                    DiplomaticRelation diplomaticRelation = empire.ObtainDiplomaticRelation(this);
                    if (diplomaticRelation != null)
                    {
                        if (sendMessages && conqueror != null)
                        {
                            switch (diplomaticRelation.Type)
                            {
                                case DiplomaticRelationType.TradeSanctions:
                                case DiplomaticRelationType.War:
                                case DiplomaticRelationType.Truce:
                                    if (conqueror != empire && conqueror != _Galaxy.IndependentEmpire)
                                    {
                                        conqueror.SendMessageToEmpire(empire, EmpireMessageType.EmpireDefeated, this, string.Format(TextResolver.GetText("We have wiped out your enemy, the X"), Name));
                                    }
                                    break;
                                case DiplomaticRelationType.MutualDefensePact:
                                case DiplomaticRelationType.Protectorate:
                                    if (conqueror != empire && conqueror != _Galaxy.IndependentEmpire)
                                    {
                                        conqueror.SendMessageToEmpire(empire, EmpireMessageType.EmpireDefeated, this, string.Format(TextResolver.GetText("We have wiped out your allies, the X"), Name));
                                    }
                                    break;
                                case DiplomaticRelationType.SubjugatedDominion:
                                    if (diplomaticRelation.Initiator == this)
                                    {
                                        if (conqueror != empire && conqueror != _Galaxy.IndependentEmpire)
                                        {
                                            conqueror.SendMessageToEmpire(empire, EmpireMessageType.EmpireDefeated, this, string.Format(TextResolver.GetText("We have liberated you from the X"), Name));
                                        }
                                    }
                                    else if (conqueror != empire && conqueror != _Galaxy.IndependentEmpire)
                                    {
                                        conqueror.SendMessageToEmpire(empire, EmpireMessageType.EmpireDefeated, this, string.Format(TextResolver.GetText("We have eliminated your slaves, the X"), Name));
                                    }
                                    break;
                                case DiplomaticRelationType.None:
                                case DiplomaticRelationType.FreeTradeAgreement:
                                    if (conqueror != empire && conqueror != _Galaxy.IndependentEmpire)
                                    {
                                        conqueror.SendMessageToEmpire(empire, EmpireMessageType.EmpireDefeated, this, string.Format(TextResolver.GetText("We have wiped out the X"), Name));
                                    }
                                    break;
                            }
                        }
                        if (diplomaticRelation.Type != 0)
                        {
                            empire.Counters.ProcessRelationChange(diplomaticRelation, this, DiplomaticRelationType.None, _Galaxy.CurrentStarDate);
                        }
                        empire.DiplomaticRelations.Remove(diplomaticRelation);
                    }
                    if (empire.EmpireEvaluations != null)
                    {
                        EmpireEvaluation empireEvaluation = empire.ObtainEmpireEvaluation(this);
                        if (empireEvaluation != null)
                        {
                            empire.EmpireEvaluations.Remove(empireEvaluation);
                        }
                    }
                    if (empire.PirateRelations != null)
                    {
                        PirateRelation pirateRelation = empire.ObtainPirateRelation(this);
                        if (pirateRelation != null)
                        {
                            empire.PirateRelations.Remove(pirateRelation);
                        }
                    }
                }
                else if (empire.PirateRelations != null)
                {
                    PirateRelation pirateRelation2 = empire.ObtainPirateRelation(this);
                    if (pirateRelation2 != null)
                    {
                        empire.PirateRelations.Remove(pirateRelation2);
                    }
                }
                if (empire.EmpiresSharedVisibility != null && empire.EmpiresSharedVisibility.Contains(this))
                {
                    empire.EmpiresSharedVisibility.Remove(this);
                }
                if (empire.Characters != null && empire.Characters.Count > 0)
                {
                    foreach (Character character2 in empire.Characters)
                    {
                        if (character2 == null)
                        {
                            continue;
                        }
                        IntelligenceMission mission = character2.Mission;
                        if (mission != null && mission.Type != 0 && mission.TargetEmpire == this)
                        {
                            if (mission.Type == IntelligenceMissionType.DeepCover && mission.Outcome == IntelligenceMissionOutcome.SucceedNotDetect)
                            {
                                character2.Mission = null;
                            }
                            else
                            {
                                character2.Mission = null;
                            }
                        }
                    }
                }
                if (empire.EmpiresViewable != null)
                {
                    for (int num = empire.EmpiresViewable.IndexOf(this); num >= 0; num = empire.EmpiresViewable.IndexOf(this))
                    {
                        empire.EmpiresViewable.RemoveAt(num);
                        empire.EmpiresViewableExpiry.RemoveAt(num);
                    }
                }
                empire.CancelBlockades(this);
                empire.CancelAttacksAgainstEmpire(this);
                if (empire.PirateMissions != null)
                {
                    int iterationCount = 0;
                    while (Galaxy.ConditionCheckLimit(empire.PirateMissions.Contains(this), 1000, ref iterationCount))
                    {
                        int num2 = empire.PirateMissions.IndexOf(this);
                        if (num2 >= 0)
                        {
                            empire.PirateMissions.RemoveAt(num2);
                        }
                    }
                }
                if (empire.KnownPirateEmpires != null && empire.KnownPirateEmpires.Contains(this))
                {
                    empire.KnownPirateEmpires.Remove(this);
                }
            }
            _Galaxy.ClearPirateColonyFacilities(this, conqueror);
            if (ShipGroups != null)
            {
                ShipGroupList shipGroupList = new ShipGroupList();
                shipGroupList.AddRange(ShipGroups);
                for (int j = 0; j < shipGroupList.Count; j++)
                {
                    ShipGroup shipGroup = shipGroupList[j];
                    if (shipGroup == null)
                    {
                        continue;
                    }
                    BuiltObjectList builtObjectList = new BuiltObjectList();
                    if (shipGroup.Ships != null)
                    {
                        builtObjectList.AddRange(shipGroup.Ships);
                        for (int k = 0; k < builtObjectList.Count; k++)
                        {
                            BuiltObject builtObject = builtObjectList[k];
                            builtObject.LeaveShipGroup();
                        }
                        shipGroup.Ships.Clear();
                    }
                    shipGroup.Empire = null;
                    shipGroup.GatherPoint = null;
                    shipGroup.LeadShip = null;
                    shipGroup.Mission = null;
                    shipGroup.AttackPoint = null;
                }
                ShipGroups.Clear();
            }
            if (conqueror != null && conqueror != _Galaxy.IndependentEmpire)
            {
                BuiltObjectList builtObjectList2 = new BuiltObjectList();
                if (PrivateBuiltObjects != null)
                {
                    builtObjectList2.AddRange(PrivateBuiltObjects);
                    for (int l = 0; l < builtObjectList2.Count; l++)
                    {
                        BuiltObject builtObject2 = builtObjectList2[l];
                        if (builtObject2 == null)
                        {
                            continue;
                        }
                        TakeOwnershipOfCargo(builtObject2.Cargo, this, conqueror);
                        if (builtObject2.Empire != conqueror)
                        {
                            if (builtObject2.Mission != null)
                            {
                                builtObject2.ClearPreviousMissionRequirements();
                                builtObject2.SubsequentMissions.Clear();
                            }
                            TakeOwnershipOfBuiltObject(builtObject2, conqueror, setDesignAsObsolete: true);
                        }
                    }
                    PrivateBuiltObjects.Clear();
                }
                builtObjectList2.Clear();
                if (BuiltObjects != null)
                {
                    builtObjectList2.AddRange(BuiltObjects);
                    for (int m = 0; m < builtObjectList2.Count; m++)
                    {
                        BuiltObject builtObject3 = builtObjectList2[m];
                        if (builtObject3 == null)
                        {
                            continue;
                        }
                        TakeOwnershipOfCargo(builtObject3.Cargo, this, conqueror);
                        if (builtObject3.Empire != conqueror)
                        {
                            if (builtObject3.Mission != null)
                            {
                                builtObject3.ClearPreviousMissionRequirements();
                                builtObject3.SubsequentMissions.Clear();
                            }
                            TakeOwnershipOfBuiltObject(builtObject3, conqueror, setDesignAsObsolete: true);
                        }
                    }
                    BuiltObjects.Clear();
                }
            }
            else
            {
                BuiltObjectList builtObjectList3 = new BuiltObjectList();
                if (PrivateBuiltObjects != null)
                {
                    builtObjectList3.AddRange(PrivateBuiltObjects);
                    for (int n = 0; n < builtObjectList3.Count; n++)
                    {
                        BuiltObject builtObject4 = builtObjectList3[n];
                        if (builtObject4 != null)
                        {
                            builtObject4.CompleteTeardown(_Galaxy, removeFromEmpire: false);
                            _Galaxy.BuiltObjects.Remove(builtObject4);
                        }
                    }
                    PrivateBuiltObjects.Clear();
                }
                builtObjectList3.Clear();
                if (BuiltObjects != null)
                {
                    builtObjectList3.AddRange(BuiltObjects);
                    for (int num3 = 0; num3 < builtObjectList3.Count; num3++)
                    {
                        BuiltObject builtObject5 = builtObjectList3[num3];
                        if (builtObject5 != null)
                        {
                            builtObject5.CompleteTeardown(_Galaxy, removeFromEmpire: false);
                            _Galaxy.BuiltObjects.Remove(builtObject5);
                        }
                    }
                    BuiltObjects.Clear();
                }
            }
            if (ShipGroups != null)
            {
                ShipGroups.Clear();
            }
            BlockadeList blockadesForEmpire = _Galaxy.Blockades.GetBlockadesForEmpire(this);
            if (blockadesForEmpire != null)
            {
                foreach (Blockade item in blockadesForEmpire)
                {
                    if (item != null)
                    {
                        if (item.TargetIsColony)
                        {
                            CancelBlockade(item.Colony);
                        }
                        else
                        {
                            CancelBlockade(item.BuiltObject);
                        }
                    }
                }
            }
            if (_ResourceMap != null)
            {
                _ResourceMap._ResourcesKnown = null;
                _ResourceMap = null;
            }
            if (Troops != null)
            {
                for (int num4 = 0; num4 < Troops.Count; num4++)
                {
                    Troop troop = Troops[num4];
                    if (troop == null)
                    {
                        continue;
                    }
                    if (conqueror != null && conqueror.Counters != null)
                    {
                        conqueror.Counters.ProcessTroopDestruction(troop);
                    }
                    if (troop.BuiltObject != null)
                    {
                        if (troop.BuiltObject.Troops != null)
                        {
                            troop.BuiltObject.Troops.Remove(troop);
                        }
                        troop.BuiltObject = null;
                    }
                    if (troop.Colony != null)
                    {
                        if (troop.Colony.Troops != null)
                        {
                            troop.Colony.Troops.Remove(troop);
                        }
                        if (troop.Colony.TroopsToRecruit != null)
                        {
                            troop.Colony.TroopsToRecruit.Remove(troop);
                        }
                        if (troop.Colony.InvadingTroops != null)
                        {
                            troop.Colony.InvadingTroops.Remove(troop);
                        }
                        troop.Colony = null;
                    }
                    troop.Empire = null;
                }
                Troops.Clear();
            }
            if (Characters != null)
            {
                Character[] array = ListHelper.ToArrayThreadSafe(Characters);
                foreach (Character character in array)
                {
                    if (character != null)
                    {
                        if (conqueror != null && conqueror.Counters != null)
                        {
                            conqueror.Counters.ProcessCharacterDeath(character);
                        }
                        character.Kill(_Galaxy);
                    }
                }
            }
            if (Outlaws != null)
            {
                Outlaws.Clear();
            }
            ResearchBonusWeaponsStation = null;
            ResearchBonusEnergyStation = null;
            ResearchBonusHighTechStation = null;
            if (EmpireEvaluations != null)
            {
                for (int num6 = 0; num6 < EmpireEvaluations.Count; num6++)
                {
                    EmpireEvaluations[num6]?.Clear();
                }
                EmpireEvaluations.Clear();
            }
            if (PirateRelations != null)
            {
                PirateRelations.Clear();
            }
            if (_ColonizationTargets != null)
            {
                for (int num7 = 0; num7 < ColonizationTargets.Count; num7++)
                {
                    ColonizationTargets[num7]?.Clear();
                }
                _ColonizationTargets.Clear();
            }
            if (_ResourceTargets != null)
            {
                for (int num8 = 0; num8 < _ResourceTargets.Count; num8++)
                {
                    _ResourceTargets[num8]?.Clear();
                }
                _ResourceTargets.Clear();
            }
            if (_DesiredForeignColonies != null)
            {
                for (int num9 = 0; num9 < _DesiredForeignColonies.Count; num9++)
                {
                    _DesiredForeignColonies[num9]?.Clear();
                }
                _DesiredForeignColonies.Clear();
            }
            if (_EmpiresWithDesiredColonies != null)
            {
                _EmpiresWithDesiredColonies.Clear();
            }
            if (_EmpiresToAttack != null)
            {
                _EmpiresToAttack.Clear();
            }
            if (!removeFromGalaxy)
            {
                return;
            }
            if (_Galaxy.Empires.Contains(this))
            {
                _Galaxy.Empires.Remove(this);
                if (!_Galaxy.DefeatedEmpires.Contains(this))
                {
                    _Galaxy.DefeatedEmpires.Add(this);
                }
            }
            if (_Galaxy.PirateEmpires.Contains(this))
            {
                _Galaxy.PirateEmpires.Remove(this);
            }
        }

       


    }
}
