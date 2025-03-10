// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Galaxy
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using BaconDistantWorlds;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DistantWorlds.Types
{
    [Serializable]
    public partial class Galaxy
    {
        public object _LockObject = new object();

        public string Title = string.Empty;

        public string Description = string.Empty;

        public GameEventList GameEvents = new GameEventList();

        public static List<Bitmap> FlagShapes;

        public static List<Bitmap> FlagShapesPirates;

        private RaceList _RaceList = new RaceList();

        private static CharacterList _CharacterList;

        public static DesignSpecificationList DesignSpecifications;

        private CreatureList _Creatures = new CreatureList();

        private RaceList _ContinentalRaces = new RaceList();

        private RaceList _MarshySwampRaces = new RaceList();

        private RaceList _DesertRaces = new RaceList();

        private RaceList _OceanRaces = new RaceList();

        private RaceList _IceRaces = new RaceList();

        private RaceList _VolcanicRaces = new RaceList();

        private RaceList _BarrenRockRaces = new RaceList();

        private RaceList _WidespreadRaces = new RaceList();

        private bool[] _RaceUsed;

        private List<int> _RaceIndependentColonyCount = new List<int>();

        private DateTime _LastGalaxyProcessTimeSensitive = DateTime.Now.ToUniversalTime().Subtract(new TimeSpan(0, 0, 1));

        private DateTime _LastGalaxyProcessTime = DateTime.Now.ToUniversalTime().Subtract(new TimeSpan(0, 2, 0));

        private DateTime _LastGalaxyHugeProcessTime = DateTime.Now.ToUniversalTime().Subtract(new TimeSpan(0, 10, 0));

        [OptionalField]
        public bool ResetRandom;

        [ThreadStatic]
        private static Random _Rnd;

        [ThreadStatic]
        private static CryptoRandom _CryptoRnd;

        private int _RandomSeed;

        public volatile bool _Reindexing;

        public static int IndexMaxX;

        public static int IndexMaxY;

        public static int SizeX;

        public static int SizeY;

        public static readonly int IndexSize;

        public static int SectorMaxX;

        public static int SectorMaxY;

        public static int SectorSizeX;

        public static int SectorSizeY;

        public static readonly int SectorSize;

        public int SectorWidth = 10;

        public int SectorHeight = 10;

        public string CustomizationSetPath;

        public string ApplicationStartupPath;

        private EmpireTerritory _EmpireTerritory = new EmpireTerritory();

        private double _ResearchSpeedModifier;

        private List<string> SystemNames = new List<string>();

        private List<bool> SystemNamesUsedPlain = new List<bool>();

        private List<bool> SystemNamesUsedAlternative = new List<bool>();

        private List<string[]> _DesignNames = new List<string[]>();

        private int _NextBuiltObjectID;

        private int _NextFighterID;

        private int _NextEmpireID;

        private int _NextCreatureID;

        private List<string[]> _AgentFirstNames = new List<string[]>();

        private List<string[]> _AgentLastNames = new List<string[]>();

        private SubRoleNameSet _SubRoleNameSet;

        [OptionalField]
        public List<string> ColonyNames = new List<string>();

        [OptionalField]
        public int ColonyNameIndex;

        public static int HyperJumpThreshhold;

        public static double BaseHyperJumpAccuracy;

        public static int HyperJumpKickout;

        public static readonly int ThreatRange;

        public static readonly double StrikeRangeSquared;

        public static readonly int PatrolOrbitDistance;

        public static readonly int EscortRange;

        public static readonly int MaxSolarSystemSize;

        public static readonly int MaxMoonOrbitSize;

        public static readonly int MovementPrecision;

        public static readonly int InvasionDropoffRange;

        public static readonly int MovementDecelerationRangeInvasion;

        public static readonly int MovementDecelerationRange;

        public static readonly int MovementImpulseSpeed;

        public static readonly int UndockRange;

        public static readonly int RefuelRate;

        public static readonly int ImpulseMargin;

        public static readonly int ParentRelativeRange;

        public static readonly int ParentRelativeRangeSquared;

        public static readonly int EscapeSprintDistance;

        public static readonly int EscapeHyperDistance;

        public static readonly int ExplosionExpansionRate;

        public static readonly int ExplosionMinimumLifetime;

        public static readonly int ExplosionImageCount;

        public static readonly int ExplosionHabitatImageCount;

        public static readonly int HabitatColonizationThreshhold;

        public static readonly int MiningStationResourceThreshhold;

        public static readonly long HabitatSmallSpacePortPopulationRequirement;

        public static readonly long HabitatMediumSpacePortPopulationRequirement;

        public static readonly long HabitatLargeSpacePortPopulationRequirement;

        public static readonly long BuildColonyShipPopulationRequirement;

        public static readonly double FreightBaseCharge;

        public static readonly double FreightChargePerUnitPerDistance;

        public static readonly double ColonyAnnualResourceConsumptionRate;

        public static readonly double ColonyAnnualLuxuryResourceConsumptionRate;

        public static readonly int TypicalMaximumOrderFulfillmentDistance;

        public static readonly long HabitatToEmpireThreshhold;

        public static readonly int HabitatToEmpireMinimumIntelligence;

        public static readonly int IncidentEvaluationAnnualNeutralizationAmount;

        public static readonly int CivilityRatingAnnualNeutralizationAmount;

        public static readonly double CivilityRatingAnnualRiseAmount;

        public static readonly int TroopStrengthAnnualNeutralizationAmount;

        public static readonly int TroopSizeAnnualRegenerationAmount;

        public static readonly int TroopAnnualRecruitmentAmount;

        public static readonly double OrderExpiryYears;

        public static readonly double OrderExpiryYearsLuxury;

        public static readonly double MinimumDiplomacyTradeProposalIntervalYears;

        public static readonly int MinimumLevelForRefuellingPoint;

        public static readonly double RefuelThreshholdPercentage;

        public static long MinimumHabitatPopulationAmount;

        public static readonly double MinimumDesignReviewIntervalYears;

        public static readonly int MinimumContractSize;

        public static readonly int MiningStationResourceTransportThreshhold;

        public static readonly int ColonyResourceTransportThreshhold;

        public static readonly int ColonyMinimumResourceReorderAmount;

        public static readonly int MinimumLuxuryResourceReorderAmount;

        public static readonly int MinimumRestrictedResourceReorderAmount;

        public static readonly int MinimumOrderAmount;

        public static readonly int MinimumDistanceBetweenBases;

        public static readonly double DistressSignalResponseMaximumDistance;

        public static readonly int RetirementYears;

        public static readonly int RetrofitYears;

        public static readonly double MaximumConstructionQueueWaitTimeYears;

        public static readonly int MaximumEmpireCount;

        public static readonly int MajorColonyStrategicThreshhold;

        public static readonly double TorpedoWeaponHitRange;

        public static readonly double AttackOvermatchFactor;

        public static readonly double AttackEvaluationRangeFactor;

        public static readonly int ColonyMaximumTroopStrength;

        public static readonly double TroopAnnualMaintenance;

        public static readonly double AgentAnnualMaintenance;

        public static readonly int BlockadeEmpireEvaluationValue;

        public static readonly double GovernmentStyleAffinityFactor;

        public static readonly int SystemCompetitionColonyFactor;

        public static readonly int SystemCompetitionMiningStationFactor;

        public static readonly double AcceptableWarValueLossesBuiltObject;

        public static readonly double AcceptableWarValueLossesColony;

        public static readonly double DistressSignalLocationOverlapRangeSquared;

        public static readonly long DistressSignalDateRange;

        public static readonly double AllowableYearsMaintenanceFromCashOnHand;

        public static readonly int FleetMaximumCount;

        public static readonly double MouseHoverHabitatProximityRange;

        public static readonly double EmpireAgeExpansionRateMinimum;

        public static readonly double EmpireAgeExpansionRateMaximum;

        public static readonly int PointBlankWeaponsRange;

        public static readonly double EmpireEvaluationTrendingFactor;

        public static readonly int IncidentImpactWhenDeclareWar;

        public static readonly double DeclareWarReputationImpact;

        public static readonly double TreatyOfferValidYears;

        public static readonly double AttackOnPiratesRange;

        public static readonly int EspionageStealResearchMaxAmount;

        public static readonly double DestroySilverMistReputationBonus;

        public static readonly double TradeBonusAnnualIncrease;

        public static readonly double TradeBonusMaximumFreeTrade;

        public static readonly double TradeBonusMaximumFreeTradeAmount;

        public static readonly double TradeBonusMaximumMutualDefense;

        public static readonly double TradeBonusMaximumMutualDefenseAmount;

        public static double SubjugationTributePercentage;

        public static readonly int IndependentTraderFreightRange;

        public static double ShipMarkupFactor;

        public static double ShipMarkupFactorPirates;

        public static readonly int PirateEmpireMaxShips;

        public static readonly int PirateEmpireMaxShipsSuper;

        public static readonly double BuiltObjectDrawResizeFactor;

        public static readonly double CreatureDrawResizeFactor;

        public static readonly int TradeColonyThreshhold;

        public static readonly int TradeMiningStationThreshhold;

        public static readonly int TradeTerritoryMapThreshhold;

        public static readonly int TradeGalaxyMapThreshhold;

        public static readonly int TradeResearchThreshhold;

        public static readonly int TradeResearchSpecialThreshhold;

        public static readonly int FleetTypicalSize;

        public static readonly int StrikeForceTypicalSize;

        public static double ShipMaintenanceCostPerSizeUnit;

        [OptionalField]
        public double PirateShipMaintenanceFactor = 0.4;

        public static readonly double ColonyStrategicResourceConsumptionPerMillionPerYear;

        public static readonly double ColonyShipBuildFactor;

        public static readonly double ColonyStateSupportCost;

        public static readonly double ColonyRevenueDivisor;

        public static readonly long RevenueDropoffPopulationThreshholdMin;

        public static readonly long RevenueDropoffPopulationThreshholdMax;

        public static readonly double RevenueDropoffRate;

        public static readonly long ColonyTaxResistanceThreshhold;

        public static readonly double ColonyTaxResistanceRate;

        public static readonly int ColonyDevelopmentLevelMaximumAnnualChange;

        public static readonly double ColonyAnnualRestrictedResourceConsumptionRate;

        public static readonly int ColonyDevelopmentBaseline;

        public static readonly long ColonyCorruptionPopulationThreshhold;

        public static readonly int MainViewShipHighlightDistance;

        public static readonly int WarWhenStrongerAngerLevel;

        public static readonly int WarWhenEvenAngerLevel;

        public static readonly int WarIncidentLevel;

        public static readonly double SpendingAgentPercentage;

        public static readonly double SpendingTroopPercentage;

        public static readonly double SpendingShipPercentage;

        public static readonly int DesiredForeignColonyStrategicThreshhold;

        public static readonly int DesiredForeignColonyResourceThreshhold;

        public static double WarWearinessMaximum;

        public static readonly double SpacePortMinimumDistance;

        public static readonly double ResupplyShipMinimumDistance;

        public static readonly int ColonyResourceLimit;

        public static readonly double RaceFamilyAffinityBias;

        public static readonly int PlanetDestroyReputationImpact;

        public static readonly double IndependentColonyInvadeReputationImpact;

        public static readonly double PirateEmpireAttackDistance;

        public static readonly long PirateEmpireAttackExpiryDateLength;

        public static readonly long FleetAssembleAttackWaitPeriodPerShip;

        public static readonly int MaximumMissionRefusals;

        public static readonly long IdealTimeBetweenGifts;

        public static readonly double AdvancedTechBonusFactor;

        public static readonly double ColonyBuildSpeedIdealPopulation;

        public static readonly double HabitatDamageAnnualRegeneration;

        public static readonly double MinimumWarLengthPeriodYears;

        public static double ResourceLevelOneQuantity;

        public static double ResourceLevelTwoQuantity;

        public static double ResourceLevelThreeQuantity;

        public static double ResourceLevelFourQuantity;

        public static double ResourceLevelFiveQuantity;

        public static double ResourceLevelSixQuantity;

        public double DifficultyLevel = 1.0;

        public bool DifficultyLevelScalesAsPlayerApproachesVictory;

        public static readonly double ColonyCorruptionFactorDefault;

        public static readonly double ResearchRateDefault;

        public static readonly double PopulationGrowthRateDefault;

        public static readonly double MiningRateDefault;

        public static readonly double TargettingFactorDefault;

        public static readonly double CountermeasuresFactorDefault;

        public static readonly double ColonyShipBuildSpeedRateDefault;

        public static readonly double WarWearinessFactorDefault;

        public static readonly double ColonyIncomeFactorDefault;

        [OptionalField]
        public bool DestroyedPiratesDoNotRespawn;

        public static readonly int RealSecondsInGalacticYear;

        public static TimeSpan IntermediateProcessingSpan;

        public static TimeSpan PeriodicProcessingSpan;

        public static TimeSpan LongProcessingSpan;

        public static TimeSpan HugeProcessingSpan;

        public bool DeferEventsForGameStart = true;

        public bool StoryCluesEnabled;

        public List<StellarObject> StoryClueLocations = new List<StellarObject>();

        public List<bool> StoryClueUsed = new List<bool>();

        public List<bool> StorySecondaryClueUsed = new List<bool>();

        [NonSerialized]
        public HabitatList[][] HabitatIndex = new HabitatList[IndexMaxX][];

        [NonSerialized]
        public BuiltObjectList[][] BuiltObjectIndex = new BuiltObjectList[IndexMaxX][];

        public HabitatList IndependentColonies = new HabitatList();

        public bool StoryDistantWorldsEnabled = true;

        [OptionalField]
        public bool StoryShadowsEnabled = true;

        public bool StoryReturnOfTheShakturiEnabled = true;

        public int StoryReturnOfTheShakturiEventLevel;

        public Habitat ShakturiTriggerHabitat;

        public bool StoryShakturiEnraged;

        public Race ShakturiOriginalRace;

        [OptionalField]
        public Race ShakturiActualRace;

        public long StoryShakturiEnrageTimer = long.MaxValue;

        public bool ShakturiDefeated;

        public object StoryLock = new object();

        public Habitat SilverMistCreatureRuinsHabitat;

        [OptionalField]
        public int SilverMistCreatureCount;

        public Empire PlayerEmpire;

        public HabitatList Habitats = new HabitatList();

        public EmpireList Empires = new EmpireList();

        public EmpireList DefeatedEmpires = new EmpireList();

        public EmpireList PirateEmpires = new EmpireList();

        public Empire IndependentEmpire;

        public BuiltObjectList BuiltObjects = new BuiltObjectList();

        public DesignList PopularDesigns = new DesignList();

        public OrderList Orders = new OrderList();

        public List<double> ResourceCurrentPrices = new List<double>();

        public List<double> ComponentCurrentPrices = new List<double>();

        private Bitmap _PirateFlagLarge;

        private Bitmap _PirateFlagSmall;

        private int _SuperPirateFactionsGenerated;

        public SystemInfoList Systems = new SystemInfoList();

        [NonSerialized]
        public SystemInfoList[][] SystemsIndex = new SystemInfoList[IndexMaxX][];

        public BlockadeList Blockades = new BlockadeList();

        private GalaxyLocationList _GalaxyLocations = new GalaxyLocationList();

        [NonSerialized]
        public GalaxyLocationList[][] GalaxyLocationIndex = new GalaxyLocationList[IndexMaxX][];

        private List<HabitatList> _AsteroidFields = new List<HabitatList>();

        private List<Point> _StarClusterLocations = new List<Point>();

        private List<double> _StarClusterPortions = new List<double>();

        public EmpireActivityList PirateMissions = new EmpireActivityList();

        public double AverageTaxRate;

        public int InvasionAttempts;

        public int InvasionSuccesses;

        public int InvasionFailures;

        public int RuinCount;

        public int AbandonedShipCount;

        public int IndependentCount;

        public int IndependentPotentialCount;

        public BuiltObjectList AbandonedBuiltObjects = new BuiltObjectList();

        private HabitatList _RuinsHabitats = new HabitatList();

        private int _RuinsGovernmentWayOfAncients;

        private int _RuinsGovernmentWayOfDarkness;

        private int _BaseTechCost = 60000;

        private double _HyperdriveSpeedMultiplier = 1.0;

        public GalaxyShape GalaxyShape;

        private int _StarCount;

        private double _ColonyPrevalence = 0.75;

        [OptionalField]
        private int _PlanetCountContinental;

        [OptionalField]
        private int _PlanetCountMarshySwamp;

        [OptionalField]
        private int _PlanetCountDesert;

        [OptionalField]
        private int _PlanetCountOcean;

        [OptionalField]
        private int _PlanetCountIce;

        [OptionalField]
        private int _PlanetCountVolcanic;

        [OptionalField]
        private int _MoonCountContinental;

        [OptionalField]
        private int _MoonCountMarshySwamp;

        [OptionalField]
        private int _MoonCountDesert;

        [OptionalField]
        private int _MoonCountOcean;

        [OptionalField]
        private int _MoonCountIce;

        [OptionalField]
        private int _MoonCountVolcanic;

        private double _ColonyFillFactor = 1.0;

        private int _LifePrevalence;

        private double _LifePrevalenceMultiplier = 1.0;

        private int _Age;

        private double _AggressionLevel;

        private int _MaximumEmpireAmount;

        private bool _SpawnNewEmpires;

        private double _CreaturePrevalence;

        private double _PiratePrevalence;

        private int _PirateProximity;

        [OptionalField]
        public bool AllowTechTrading = true;

        [OptionalField]
        public GameSummary GameSummary = new GameSummary();

        [OptionalField]
        public bool AllowRaceStartingCharacters = true;

        private object _DelayedActionLockObject = new object();

        public EventActionExecutionPackageList DelayedActions = new EventActionExecutionPackageList();

        public bool AllowGiantKaltorGeneration = true;

        private DateTime _StartDateTime;

        private DateTime _TrackedDateTime;

        internal long _StartStarDate;

        private BasicStopWatch _StopWatch = new BasicStopWatch();

        private GalaxyTimeState _TimeState;

        private TimeSpan _TotalPausedTime = default(TimeSpan);

        private DateTime _PauseDateTime = DateTime.MaxValue;

        public static long YearLength;

        [NonSerialized]
        public VictoryConditions GlobalVictoryConditions;

        public bool GameRaceSpecificVictoryConditionsEnabled = true;

        public bool GameRaceSpecificEventsEnabled = true;

        public bool GameDisasterEventsEnabled = true;

        [OptionalField]
        public float EmpireTerritoryColonyInfluenceRangeFactor = 1f;

        [OptionalField]
        public bool ColonizationRangeEnforceLimit = true;

        [OptionalField]
        public float ColonizationRange = 3000000f;

        private volatile bool _RegeneratingEmpireTerritory;

        private volatile bool _RegenerateEmpireTerritoryAgain;

        public static ComponentList ComponentsWeaponBeamOrderedByRange;

        public static ComponentList ComponentsWeaponTorpedoOrderedByRange;

        public static ComponentList ComponentsWeaponAreaOrderedByRange;

        public static ComponentList ComponentsWeaponBeamOrderedByPower;

        public static ComponentList ComponentsWeaponTorpedoOrderedByPower;

        public static ComponentList ComponentsWeaponAreaOrderedByPower;

        public static ComponentList ComponentsReactorOrderedByEfficiency;

        public static ComponentList ComponentsReactorOrderedByPower;

        public static ComponentList ComponentsEngineMainThrustOrderedByPower;

        public static ComponentList ComponentsEngineVectoringOrderedByPower;

        public static ComponentList ComponentsEngineMainThrustOrderedByEfficiency;

        public static ComponentList ComponentsEngineVectoringOrderedByEfficiency;

        public static ComponentList ComponentsHyperdriveOrderedByPower;

        public static ComponentList ComponentsHyperdriveOrderedByEfficiency;

        public static ComponentList ComponentsHyperdriveOrderedByJumpInitiation;

        public static Random RndStatic;

        public static long StartStarDate;

        public static ResourceSystem ResourceSystemStatic;

        public static ComponentDefinition[] ComponentDefinitionsStatic;

        public static ResearchNodeDefinitionList ResearchNodeDefinitionsStatic;

        public static PlanetaryFacilityDefinitionList PlanetaryFacilityDefinitionsStatic;

        public static FighterSpecificationList FighterSpecificationsStatic;

        public static GovernmentAttributesList GovernmentsStatic;

        public static RaceFamilyList RaceFamiliesStatic;

        public static PlagueList PlaguesStatic;

        public static ResourceSystem BackupResourceSystemStatic;

        public static ComponentDefinition[] BackupComponentDefinitionsStatic;

        public static ResearchNodeDefinitionList BackupResearchNodeDefinitionsStatic;

        public static PlanetaryFacilityDefinitionList BackupPlanetaryFacilityDefinitionsStatic;

        public static FighterSpecificationList BackupFighterSpecificationsStatic;

        public static GovernmentAttributesList BackupGovernmentsStatic;

        public static RaceFamilyList BackupRaceFamiliesStatic;

        public static PlagueList BackupPlaguesStatic;

        public ResourceSystem ResourceSystem = new ResourceSystem();

        public ComponentDefinition[] ComponentDefinitions = new ComponentDefinition[130];

        public ResearchNodeDefinitionList ResearchNodeDefinitions = new ResearchNodeDefinitionList();

        public PlanetaryFacilityDefinitionList PlanetaryFacilityDefinitions = new PlanetaryFacilityDefinitionList();

        public FighterSpecificationList FighterSpecifications = new FighterSpecificationList();

        public GovernmentAttributesList Governments = new GovernmentAttributesList();

        public RaceFamilyList RaceFamilies = new RaceFamilyList();

        public PlagueList Plagues = new PlagueList();

        private object _WonderLockObject = new object();

        private bool[] _WondersBuilt = new bool[PlanetaryFacilityDefinitionsStatic.Count];

        public CreatureList Creatures => _Creatures;

        public static Random Rnd
        {
            get
            {
                if (_Rnd == null)
                {
                    _Rnd = new Random();
                }
                return _Rnd;
            }
        }

        public static CryptoRandom CryptoRnd
        {
            get
            {
                if (_CryptoRnd == null)
                {
                    _CryptoRnd = new CryptoRandom();
                }
                return _CryptoRnd;
            }
        }

        public int RandomSeed
        {
            get
            {
                return _RandomSeed;
            }
            set
            {
                _RandomSeed = value;
            }
        }

        public EmpireTerritory EmpireTerritory => _EmpireTerritory;

        public double ResearchSpeedModifier
        {
            get
            {
                return _ResearchSpeedModifier;
            }
            set
            {
                _ResearchSpeedModifier = value;
            }
        }

        public List<string[]> DesignNames => _DesignNames;

        public int NextEmpireID => _NextEmpireID;

        public SubRoleNameSet SubRoleNameSet
        {
            get
            {
                return _SubRoleNameSet;
            }
            set
            {
                _SubRoleNameSet = value;
            }
        }

        public GalaxyLocationList GalaxyLocations => _GalaxyLocations;

        public List<HabitatList> AsteroidFields => _AsteroidFields;

        public HabitatList RuinsHabitats
        {
            get
            {
                return _RuinsHabitats;
            }
            set
            {
                _RuinsHabitats = value;
            }
        }

        public int BaseTechCost
        {
            get
            {
                return _BaseTechCost;
            }
            set
            {
                _BaseTechCost = value;
            }
        }

        public double HyperdriveSpeedMultiplier
        {
            get
            {
                return _HyperdriveSpeedMultiplier;
            }
            set
            {
                _HyperdriveSpeedMultiplier = value;
            }
        }

        public double ColonyPrevalence
        {
            get
            {
                return _ColonyPrevalence;
            }
            set
            {
                _ColonyPrevalence = value;
            }
        }

        public double ColonyFillFactor => _ColonyFillFactor;

        public double LifePrevalenceMultiplier
        {
            get
            {
                return _LifePrevalenceMultiplier;
            }
            set
            {
                _LifePrevalenceMultiplier = value;
            }
        }

        public int LifePrevalence
        {
            get
            {
                return _LifePrevalence;
            }
            set
            {
                _LifePrevalence = value;
            }
        }

        public int Age
        {
            get
            {
                return _Age;
            }
            set
            {
                _Age = value;
            }
        }

        public int StartingAge => _Age;

        public int ExpectedMaximumColoniesInGalaxy
        {
            get
            {
                double num = 0.47;
                if (_StarCount >= 1400)
                {
                    num = 0.38;
                }
                else if (_StarCount >= 1000)
                {
                    num = 0.41;
                }
                else if (_StarCount >= 700)
                {
                    num = 0.42;
                }
                else if (_StarCount >= 400)
                {
                    num = 0.43;
                }
                num *= _ColonyPrevalence;
                return (int)((double)_StarCount * num);
            }
        }

        public int AllowableMaximumStartingColonies => (int)((double)ExpectedMaximumColoniesInGalaxy * 0.75);

        public double TypicalDistanceBetweenColoniesAtMaximumFill
        {
            get
            {
                double num = (double)SizeX / Math.Sqrt(StarCount);
                double num2 = Math.Sqrt((double)ExpectedMaximumColoniesInGalaxy / (double)StarCount);
                return num / num2;
            }
        }

        public int MaximumEmpireAmount
        {
            get
            {
                return _MaximumEmpireAmount;
            }
            set
            {
                _MaximumEmpireAmount = value;
            }
        }

        public double AggressionLevel
        {
            get
            {
                return _AggressionLevel;
            }
            set
            {
                _AggressionLevel = value;
            }
        }

        public bool SpawnNewEmpires
        {
            get
            {
                return _SpawnNewEmpires;
            }
            set
            {
                _SpawnNewEmpires = value;
            }
        }

        public double CreaturePrevalence
        {
            get
            {
                return _CreaturePrevalence;
            }
            set
            {
                _CreaturePrevalence = value;
            }
        }

        public double PiratePrevalence
        {
            get
            {
                return _PiratePrevalence;
            }
            set
            {
                _PiratePrevalence = value;
            }
        }

        public int PirateProximity
        {
            get
            {
                return _PirateProximity;
            }
            set
            {
                _PirateProximity = value;
            }
        }

        public long ActualStartDate => _StartStarDate;

        public TimeSpan TotalRunningTime => CurrentDateTime.Subtract(_StartDateTime);

        public long CurrentStarDate => (CurrentDateTime.Ticks - _StartDateTime.Ticks) / 10000 + _StartStarDate;

        public GalaxyTimeState TimeState => _TimeState;

        public double TimeSpeed
        {
            get
            {
                if (_StopWatch != null)
                {
                    return _StopWatch.TimeSpeed;
                }
                return 1.0;
            }
        }

        public DateTime CurrentDateTime
        {
            get
            {
                if (_TimeState == GalaxyTimeState.Running)
                {
                    return _TrackedDateTime.ToUniversalTime().AddTicks(_StopWatch.Elapsed.Ticks);
                }
                if (_TimeState == GalaxyTimeState.Paused)
                {
                    return _TrackedDateTime.ToUniversalTime().AddTicks(_StopWatch.Elapsed.Ticks);
                }
                throw new ApplicationException("Invalid GalaxyTimeState value.");
            }
        }

        public int StarCount => _StarCount;

        public Bitmap PirateFlagLarge
        {
            get
            {
                return _PirateFlagLarge;
            }
            set
            {
                _PirateFlagLarge = value;
            }
        }

        public Bitmap PirateFlagSmall
        {
            get
            {
                return _PirateFlagSmall;
            }
            set
            {
                _PirateFlagSmall = value;
            }
        }

        public double ColonyFillRatio
        {
            get
            {
                int num = Habitats.Count / 45;
                return (double)ColonyCount / (double)num;
            }
        }

        public double IntoleranceLevel => Math.Max(0.0, Math.Min(1.0, 1.0 - ColonyFillRatio));

        public int ColonyCount
        {
            get
            {
                int num = 0;
                for (int i = 0; i < Empires.Count; i++)
                {
                    Empire empire = Empires[i];
                    num += empire.Colonies.Count;
                }
                return num;
            }
        }

        public double TotalStateMoneyInGalaxy
        {
            get
            {
                double num = 0.0;
                for (int i = 0; i < Empires.Count; i++)
                {
                    Empire empire = Empires[i];
                    if (empire.PirateEmpireBaseHabitat == null && empire != IndependentEmpire && empire.Active)
                    {
                        num += empire.StateMoney;
                    }
                }
                return num;
            }
        }

        public double TotalMoneyInGalaxy
        {
            get
            {
                double num = 0.0;
                for (int i = 0; i < Empires.Count; i++)
                {
                    Empire empire = Empires[i];
                    num += empire.StateMoney;
                    num += empire.PrivateMoney;
                }
                return num;
            }
        }

        public RaceList Races
        {
            get
            {
                return _RaceList;
            }
            set
            {
                _RaceList = value;
            }
        }

        public static CharacterList Characters
        {
            get
            {
                return _CharacterList;
            }
            set
            {
                _CharacterList = value;
            }
        }

        public event EventHandler LocationPinged;

        public event EventHandler SystemsUpdated;

        public event EventHandler<GameEndEventArgs> GameEnd;

        public event EventHandler<RefreshViewEventArgs> RefreshView;

        [field: NonSerialized]
        public event EventHandler<CharacterImageChangedEventArgs> CharacterImageChanged;

        public void ClearRaceUsed()
        {
            _RaceUsed = null;
        }

        public static void SetRandom(Random rnd)
        {
            _Rnd = rnd;
        }

        protected virtual void OnLocationPinged(EventArgs e)
        {
            if (this.LocationPinged != null)
            {
                this.LocationPinged(this, e);
            }
        }

        protected virtual void OnSystemsUpdated(EventArgs e)
        {
            if (this.SystemsUpdated != null)
            {
                this.SystemsUpdated(this, e);
            }
        }

        protected virtual void OnGameEnd(GameEndEventArgs e)
        {
            if (this.GameEnd != null)
            {
                this.GameEnd(this, e);
            }
        }

        public virtual void OnRefreshView(RefreshViewEventArgs e)
        {
            if (this.RefreshView != null)
            {
                this.RefreshView(this, e);
            }
        }

        public virtual void OnCharacterImageChanged(CharacterImageChangedEventArgs e)
        {
            if (this.CharacterImageChanged != null)
            {
                this.CharacterImageChanged(this, e);
            }
        }

        public void ResetNextEmpireId()
        {
            _NextEmpireID = 0;
        }

        public int GetNextBuiltObjectID()
        {
            if (_NextBuiltObjectID < int.MaxValue)
            {
                _NextBuiltObjectID++;
                return _NextBuiltObjectID;
            }
            throw new ApplicationException("Maximum allowable ship number exceeded!");
        }

        public int GetNextFighterID()
        {
            if (_NextFighterID < int.MaxValue)
            {
                _NextFighterID++;
                return _NextFighterID;
            }
            throw new ApplicationException("Maximum allowable fighter number exceeded!");
        }

        public int GetNextEmpireID()
        {
            if (_NextEmpireID < MaximumEmpireCount)
            {
                _NextEmpireID++;
                return _NextEmpireID;
            }
            throw new ApplicationException("Maximum allowable empire number exceeded!");
        }

        public int GetNextCreatureID()
        {
            if (_NextCreatureID < int.MaxValue)
            {
                _NextCreatureID++;
                return _NextCreatureID;
            }
            throw new ApplicationException("Maximum allowable creature number exceeded!");
        }

        public static string ResolveStarDateDescription(long starDate)
        {
            return ResolveStarDateDescription(starDate, ".");
        }

        public static string ResolveStarDateDescription(long starDate, string datePartSeparator)
        {
            int num = (int)(starDate / (1000 * RealSecondsInGalacticYear));
            long num2 = (long)num * (long)(1000 * RealSecondsInGalacticYear);
            double num3 = 1000.0 * (double)RealSecondsInGalacticYear / 12.0;
            int num4 = (int)((double)(starDate - num2) / num3);
            long num5 = (long)((double)num4 * num3);
            double num6 = num3 / 30.0;
            int num7 = (int)((double)(starDate - (num2 + num5)) / num6);
            num4++;
            num7++;
            return num.ToString("0000") + datePartSeparator + num4.ToString("00") + datePartSeparator + num7.ToString("00");
        }

        public static long CalculateStartOfYear(long date)
        {
            long num = date % YearLength;
            return date - num;
        }

        public void ChangeTimeSpeed(double timeSpeed)
        {
            if (_TimeState == GalaxyTimeState.Paused)
            {
                _StopWatch.TimeSpeed = timeSpeed;
                return;
            }
            Pause();
            _StopWatch.TimeSpeed = timeSpeed;
            Resume();
        }

        public void Pause()
        {
            if (_TimeState == GalaxyTimeState.Running)
            {
                if (!(_PauseDateTime == DateTime.MaxValue))
                {
                    throw new ApplicationException("Pause time was set while not paused!");
                }
                _StopWatch.Stop();
                _PauseDateTime = DateTime.Now.ToUniversalTime();
                _TimeState = GalaxyTimeState.Paused;
                _TrackedDateTime = _TrackedDateTime.ToUniversalTime().Add(_StopWatch.Elapsed);
                _StopWatch.Reset();
            }
        }

        public void Resume()
        {
            if (_TimeState == GalaxyTimeState.Paused)
            {
                if (!(_PauseDateTime != DateTime.MaxValue))
                {
                    throw new ApplicationException("Pause time was not set while paused!");
                }
                _StopWatch.Start();
                TimeSpan ts = DateTime.Now.ToUniversalTime().Subtract(_PauseDateTime.ToUniversalTime());
                _TotalPausedTime = _TotalPausedTime.Add(ts);
                _PauseDateTime = DateTime.MaxValue;
                _TimeState = GalaxyTimeState.Running;
            }
        }

        public void SetEmpireDifficultyFactors(Empire empire)
        {
            SetEmpireDifficultyFactors(empire, null);
        }

        public void SetEmpireDifficultyFactors(Empire empire, VictoryConditionProgressList conditionProgresses)
        {
            if (empire == PlayerEmpire)
            {
                empire.DifficultyLevel = DifficultyLevel;
                empire.DifficultyLevel += empire.DifficultyLevelModifier;
                if (DifficultyLevelScalesAsPlayerApproachesVictory)
                {
                    if (conditionProgresses == null)
                    {
                        conditionProgresses = GenerateVictoryConditionProgresses(this, GlobalVictoryConditions, filterOutUnmetEmpires: true);
                    }
                    if (conditionProgresses.Count > 0)
                    {
                        VictoryConditionProgress byEmpire = conditionProgresses.GetByEmpire(empire);
                        if (byEmpire != null)
                        {
                            double totalProgress = byEmpire.TotalProgress;
                            if (totalProgress > 0.5)
                            {
                                double num = Math.Min(0.5, Math.Max(0.0, totalProgress - 0.5));
                                empire.DifficultyLevel += num * Math.Max(1.0, DifficultyLevel);
                            }
                        }
                    }
                }
            }
            else
            {
                empire.DifficultyLevel = 1.0 + (1.0 - Math.Sqrt(DifficultyLevel));
                empire.DifficultyLevel += empire.DifficultyLevelModifier;
            }
            BaconGalaxy.SetEmpireDifficultyFactors(empire);
        }

        public void ReviewEmpireDifficultyFactors()
        {
            VictoryConditionProgressList conditionProgresses = GenerateVictoryConditionProgresses(this, GlobalVictoryConditions, filterOutUnmetEmpires: true);
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Active)
                {
                    SetEmpireDifficultyFactors(empire, conditionProgresses);
                }
            }
            for (int j = 0; j < PirateEmpires.Count; j++)
            {
                Empire empire2 = PirateEmpires[j];
                if (empire2 != null && empire2.Active)
                {
                    SetEmpireDifficultyFactors(empire2, conditionProgresses);
                }
            }
        }

        public static int CalculateResourceLevel(Resource resource, BuiltObject tradingPost)
        {
            if (tradingPost.ParentHabitat != null)
            {
                if (tradingPost != null && !tradingPost.HasBeenDestroyed && tradingPost.Role == BuiltObjectRole.Base && (tradingPost.ParentHabitat == null || tradingPost.ParentHabitat.Empire == null || tradingPost.ParentHabitat.Empire != tradingPost.Empire) && tradingPost.BuiltAt == null && tradingPost.CargoSpace > 0 && resource != null)
                {
                    return tradingPost.SubRole switch
                    {
                        BuiltObjectSubRole.SmallSpacePort => CalculateResourceLevelSpaceport(resource, 0, 1.0),
                        BuiltObjectSubRole.MediumSpacePort => CalculateResourceLevelSpaceport(resource, 0, 2.0),
                        BuiltObjectSubRole.LargeSpacePort => CalculateResourceLevelSpaceport(resource, 0, 4.0),
                        _ => CalculateResourceLevelStockForBaseRetrofit(resource.ResourceID),
                    };
                }
                if (tradingPost.IsSpacePort)
                {
                    return CalculateResourceLevel(resource, tradingPost.ParentHabitat, isMiningStation: false, isIndependent: false);
                }
                return CalculateResourceLevel(resource, tradingPost.ParentHabitat, isMiningStation: true, isIndependent: false);
            }
            if (tradingPost != null && !tradingPost.HasBeenDestroyed && tradingPost.Role == BuiltObjectRole.Base && (tradingPost.ParentHabitat == null || tradingPost.ParentHabitat.Empire == null || tradingPost.ParentHabitat.Empire != tradingPost.Empire) && tradingPost.BuiltAt == null && tradingPost.CargoSpace > 0 && resource != null)
            {
                return CalculateResourceLevelStockForBaseRetrofit(resource.ResourceID);
            }
            return 0;
        }

        public static int CalculateResourceLevel(Cargo cargo, BuiltObject tradingPost)
        {
            if (tradingPost.ParentHabitat != null)
            {
                if (tradingPost != null && !tradingPost.HasBeenDestroyed && tradingPost.Role == BuiltObjectRole.Base && (tradingPost.ParentHabitat == null || tradingPost.ParentHabitat.Empire == null || tradingPost.ParentHabitat.Empire != tradingPost.Empire) && tradingPost.BuiltAt == null && cargo != null && cargo.CommodityIsResource && cargo.CommodityResource != null)
                {
                    return tradingPost.SubRole switch
                    {
                        BuiltObjectSubRole.SmallSpacePort => CalculateResourceLevelSpaceport(cargo.CommodityResource, 0, 1.0),
                        BuiltObjectSubRole.MediumSpacePort => CalculateResourceLevelSpaceport(cargo.CommodityResource, 0, 2.0),
                        BuiltObjectSubRole.LargeSpacePort => CalculateResourceLevelSpaceport(cargo.CommodityResource, 0, 4.0),
                        _ => CalculateResourceLevelStockForBaseRetrofit(cargo.CommodityResource.ResourceID),
                    };
                }
                if (tradingPost.IsSpacePort)
                {
                    return CalculateResourceLevel(cargo, tradingPost.ParentHabitat);
                }
                return CalculateResourceLevel(cargo, tradingPost.ParentHabitat, isMiningStation: true);
            }
            if (tradingPost != null && !tradingPost.HasBeenDestroyed && tradingPost.Role == BuiltObjectRole.Base && (tradingPost.ParentHabitat == null || tradingPost.ParentHabitat.Empire == null || tradingPost.ParentHabitat.Empire != tradingPost.Empire) && tradingPost.BuiltAt == null && tradingPost.CargoSpace > 0 && cargo != null && cargo.CommodityIsResource && cargo.CommodityResource != null)
            {
                return CalculateResourceLevelStockForBaseRetrofit(cargo.CommodityResource.ResourceID);
            }
            return 0;
        }

        public static int CalculateResourceLevel(Cargo cargo, Habitat colony)
        {
            return CalculateResourceLevel(cargo, colony, isMiningStation: false);
        }

        public static int CalculateResourceLevel(Cargo cargo, Habitat colony, bool isMiningStation)
        {
            if (cargo.CommodityResource != null)
            {
                return CalculateResourceLevel(cargo.CommodityResource, colony, isMiningStation, isIndependent: false);
            }
            return 0;
        }

        public static int CalculateResourceLevelSpaceport(Resource resource, int fleetFuelAmount, double multiplier)
        {
            double num = 0.0;
            for (int i = 0; i < ResourceSystemStatic.StrategicResourcesOrderedByRelativeImportance.Count; i++)
            {
                ResourceDefinition resourceDefinition = ResourceSystemStatic.StrategicResourcesOrderedByRelativeImportance[i];
                if (resourceDefinition != null)
                {
                    num = ResourceLevelOneQuantity * (double)resourceDefinition.RelativeImportance;
                    if (resourceDefinition.IsFuel)
                    {
                        num += (double)fleetFuelAmount;
                    }
                }
            }
            return (int)(num * multiplier);
        }

        public static int CalculateResourceLevel(Resource resource, Habitat colony)
        {
            return CalculateResourceLevel(resource, colony, isMiningStation: false, isIndependent: false);
        }

        public static int CalculateResourceLevel(Resource resource, Habitat colony, bool isMiningStation, bool isIndependent)
        {
            return CalculateResourceLevel(resource, colony, isMiningStation, isIndependent, isCriticalResource: false);
        }

        public static int CalculateResourceLevel(Resource resource, Habitat colony, bool isMiningStation, bool isIndependent, bool isCriticalResource)
        {
            return CalculateResourceLevel(resource, colony, isMiningStation, isIndependent, isCriticalResource, 0);
        }

        public static int CalculateResourceLevel(Resource resource, Habitat colony, bool isMiningStation, bool isIndependent, bool isCriticalResource, int fleetFuelAmount)
        {
            double num = 0.0;
            if (isIndependent)
            {
                int result = 0;
                if (resource.IsFuel)
                {
                    result = 4000;
                }
                return result;
            }
            if (isMiningStation)
            {
                if (resource.Group == ResourceGroup.Mineral || resource.Group == ResourceGroup.Gas)
                {
                    if (resource.RelativeImportance > 0.25f)
                    {
                        return 4000;
                    }
                    return 2000;
                }
                return 2000;
            }
            if (!colony.HasSpacePort)
            {
                if (resource.IsFuel)
                {
                    num = ResourceLevelTwoQuantity + (double)fleetFuelAmount;
                }
                long num3 = 0L;
                if (colony.Population != null)
                {
                    num3 = colony.Population.TotalAmount;
                }
                if (num3 >= 1000000000)
                {
                    num = ((resource.RelativeImportance > 0.4f) ? ResourceLevelThreeQuantity : ((!(resource.RelativeImportance > 0.15f)) ? ResourceLevelFiveQuantity : ResourceLevelFourQuantity));
                }
                else if (num3 >= 200000000)
                {
                    num = ((resource.RelativeImportance > 0.4f) ? ResourceLevelFourQuantity : ((!(resource.RelativeImportance > 0.15f)) ? ResourceLevelSixQuantity : ResourceLevelFiveQuantity));
                }
            }
            else
            {
                num = ((resource.RelativeImportance > 0.4f) ? ResourceLevelOneQuantity : ((resource.RelativeImportance > 0.25f) ? ResourceLevelTwoQuantity : ((!(resource.RelativeImportance > 0.15f)) ? ResourceLevelFourQuantity : ResourceLevelThreeQuantity)));
                if (resource.IsFuel)
                {
                    num += (double)fleetFuelAmount;
                }
            }
            if (isCriticalResource)
            {
                num = Math.Max(num, ResourceLevelFourQuantity);
            }
            return (int)(num * (double)colony.ResourceMultiplier);
        }

        public static CargoList ResolveRetrofitResourcesForBase(Empire empire)
        {
            CargoList cargoList = new CargoList();
            for (int i = 0; i < ResourceSystemStatic.StrategicResourcesOrderedByRelativeImportance.Count; i++)
            {
                ResourceDefinition resourceDefinition = ResourceSystemStatic.StrategicResourcesOrderedByRelativeImportance[i];
                if (resourceDefinition != null)
                {
                    cargoList.Add(new Cargo(new Resource(resourceDefinition.ResourceID), CalculateResourceLevelStockForBaseRetrofit(resourceDefinition.ResourceID), empire));
                }
            }
            return cargoList;
        }

        public static int CalculateResourceLevelStockForBaseRetrofit(byte resourceId)
        {
            int result = 0;
            ResourceDefinition resourceDefinition = ResourceSystemStatic.Resources[resourceId];
            if (resourceDefinition != null)
            {
                result = ((!resourceDefinition.IsFuel) ? ((!(resourceDefinition.RelativeImportance > 0.25f)) ? 25 : 50) : 0);
            }
            return result;
        }

        public static int CalculateResourceLevelPirates(Resource resource, BuiltObject pirateSpaceport)
        {
            double num = 0.0;
            num = ((resource.RelativeImportance > 0.4f || resource.IsFuel) ? ResourceLevelOneQuantity : ((resource.RelativeImportance > 0.25f) ? ResourceLevelTwoQuantity : ((!(resource.RelativeImportance > 0.1f)) ? ResourceLevelFourQuantity : ResourceLevelThreeQuantity)));
            double num2 = 1.0;
            if (pirateSpaceport != null)
            {
                switch (pirateSpaceport.SubRole)
                {
                    case BuiltObjectSubRole.SmallSpacePort:
                        num2 = 1.0;
                        break;
                    case BuiltObjectSubRole.MediumSpacePort:
                        num2 = 2.0;
                        break;
                    case BuiltObjectSubRole.LargeSpacePort:
                        num2 = 4.0;
                        break;
                }
            }
            return (int)(num * num2);
        }

        private void MaintainIndependentColonyFuelLevels()
        {
            for (int i = 0; i < IndependentColonies.Count; i++)
            {
                Habitat habitat = IndependentColonies[i];
                if (habitat.Owner != IndependentEmpire)
                {
                    continue;
                }
                OrderList orders = Orders.GetOrders(habitat);
                for (int j = 0; j < ResourceSystem.FuelResources.Count; j++)
                {
                    ResourceDefinition resourceDefinition = ResourceSystem.FuelResources[j];
                    if (resourceDefinition != null)
                    {
                        CheckAndOrderResource(habitat, orders, new Resource(resourceDefinition.ResourceID));
                    }
                }
            }
        }

        private void CheckAndOrderResource(Habitat colony, OrderList colonyOrders, Resource resource)
        {
            int amountToOrder = 0;
            int num = CalculateResourceLevel(resource, colony, isMiningStation: true, isIndependent: true);
            int minimumResourceLevel = (int)((double)num * 0.6);
            if (!CheckResourceMeetsMinimumLevel(resource, minimumResourceLevel, num, colony, colonyOrders, out amountToOrder))
            {
                _ = ResourceCurrentPrices[resource.ResourceID];
                CreateOrder(colony, resource, amountToOrder, isState: false);
            }
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
                num = colony.Cargo[num4].Amount;
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
            if (amountToOrder < ColonyMinimumResourceReorderAmount)
            {
                amountToOrder = 0;
            }
            if (num2 >= minimumResourceLevel)
            {
                result = true;
            }
            return result;
        }

        public Order CreateOrder(Habitat colony, Resource resource, int amount, bool isState)
        {
            return CreateOrder(colony, resource, amount, isState, allowExpiry: false);
        }

        public Order CreateOrder(Habitat colony, Resource resource, int amount, bool isState, bool allowExpiry)
        {
            long expiryDate = CurrentStarDate + (int)(OrderExpiryYearsLuxury * (double)RealSecondsInGalacticYear * 1000.0);
            if (!allowExpiry || !resource.IsLuxuryResource)
            {
                expiryDate = CurrentStarDate + (int)(1000.0 * (double)RealSecondsInGalacticYear * 1000.0);
            }
            return CreateOrder(colony, resource, amount, isState, expiryDate);
        }

        public Order CreateOrder(Habitat colony, Resource resource, int amount, bool isState, long expiryDate)
        {
            int maximumFulfillmentDistance = CalculateMaximumOrderFulfillmentDistance(colony);
            Order order = new Order(this, colony, resource, amount, expiryDate, maximumFulfillmentDistance);
            order.MinimumContractSize = MinimumContractSize;
            order.IsStateOrder = isState;
            Orders.Add(order);
            return order;
        }

        public double CalculateCurrentCargoValue(Cargo cargo, int amount)
        {
            double num = 0.0;
            if (cargo.CommodityResource != null)
            {
                Resource commodityResource = cargo.CommodityResource;
                num = ResourceCurrentPrices[commodityResource.ResourceID];
            }
            else if (cargo.CommodityComponent != null)
            {
                Component commodityComponent = cargo.CommodityComponent;
                num = ComponentCurrentPrices[commodityComponent.ComponentID];
            }
            return num * (double)amount;
        }

        public double CalculateAverageHappiness()
        {
            double num = 0.0;
            int num2 = 0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Active)
                {
                    num += empire.AverageHappiness();
                    num2++;
                }
            }
            return num / (double)num2;
        }

        public double CalculateAverageStateCashPerPopulation()
        {
            double num = 0.0;
            int num2 = 0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Active)
                {
                    num += empire.AverageStateCashPerPopulation();
                    num2++;
                }
            }
            return num / (double)num2;
        }

        public double CalculateAverageCashflowPerPopulation()
        {
            double num = 0.0;
            int num2 = 0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Active)
                {
                    num += empire.AverageCashflowPerPopulation();
                    num2++;
                }
            }
            return num / (double)num2;
        }

        public double CalculateAverageShipMaintenancePerPopulation()
        {
            double num = 0.0;
            int num2 = 0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Active)
                {
                    num += empire.AverageShipMaintenancePerPopulation();
                    num2++;
                }
            }
            return num / (double)num2;
        }

        public double CalculateAverageMilitaryStrengthPerPopulation()
        {
            double num = 0.0;
            int num2 = 0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Active)
                {
                    num += empire.AverageMilitaryStrengthPerPopulation();
                    num2++;
                }
            }
            return num / (double)num2;
        }

        public double CalculateAverageSpaceportsPerColony()
        {
            double num = 0.0;
            int num2 = 0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Active)
                {
                    num += empire.AverageSpaceportsPerColony();
                    num2++;
                }
            }
            return num / (double)num2;
        }

        public double CalculateAverageResearchStationsPerColony()
        {
            double num = 0.0;
            int num2 = 0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Active)
                {
                    num += empire.AverageResearchStationsPerColony();
                    num2++;
                }
            }
            return num / (double)num2;
        }

        public double CalculateAverageMiningStationsPerColony()
        {
            double num = 0.0;
            int num2 = 0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Active)
                {
                    num += empire.AverageMiningStationsPerColony();
                    num2++;
                }
            }
            return num / (double)num2;
        }

        public double CalculateAverageCapitalShipsPerColony()
        {
            double num = 0.0;
            int num2 = 0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Active)
                {
                    num += empire.AverageCapitalShipsPerColony();
                    num2++;
                }
            }
            return num / (double)num2;
        }

        public double CalculateAverageCorruption()
        {
            double num = 0.0;
            int num2 = 0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Active)
                {
                    num += empire.Corruption;
                    num2++;
                }
            }
            return num / (double)num2;
        }

        public double CalculateAverageConstructionShipsPerColony()
        {
            double num = 0.0;
            int num2 = 0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Active)
                {
                    num += empire.AverageConstructionShipsPerColony();
                    num2++;
                }
            }
            return num / (double)num2;
        }

        public static void LoadRaceBiases(string applicationStartupPath, string customizationSetName, RaceList races)
        {
            int num = 0;
            string text = applicationStartupPath + "\\raceBiases.txt";
            if (!string.IsNullOrEmpty(customizationSetName) && customizationSetName.ToLower(CultureInfo.InvariantCulture) != "default")
            {
                text = applicationStartupPath + "\\Customization\\" + customizationSetName + "\\raceBiases.txt";
            }
            if (!File.Exists(text))
            {
                text = applicationStartupPath + "\\raceBiases.txt";
            }
            try
            {
                if (!File.Exists(text))
                {
                    return;
                }
                List<List<int>> list = new List<List<int>>();
                List<string> list2 = new List<string>();
                List<int> list3 = new List<int>();
                FileStream fileStream = File.OpenRead(text);
                StreamReader streamReader = new StreamReader(fileStream);
                while (!streamReader.EndOfStream)
                {
                    num++;
                    string empty = string.Empty;
                    string text2 = streamReader.ReadLine();
                    if (string.IsNullOrEmpty(text2) || !(text2.Trim() != string.Empty) || !(text2.Trim().Substring(0, 1) != "'"))
                    {
                        continue;
                    }
                    int num2 = 0;
                    int num3 = text2.IndexOf(",", num2);
                    if (num3 >= 0)
                    {
                        string text3 = text2.Substring(num2, num3 - num2);
                        text3 = text3.Trim();
                        int result = -1;
                        if (int.TryParse(text3, out result))
                        {
                            list3.Add(result);
                            num2 = num3 + 1;
                            num3 = text2.IndexOf(",", num2);
                            if (num3 >= 0)
                            {
                                string text4 = text2.Substring(num2, num3 - num2);
                                text4 = text4.Trim();
                                empty = text4;
                                list2.Add(empty);
                                num2 = num3 + 1;
                                List<int> list4 = new List<int>();
                                int num4 = 1;
                                while (num3 >= 0)
                                {
                                    num3 = text2.IndexOf(",", num2);
                                    string empty2 = string.Empty;
                                    empty2 = ((num3 < 0) ? text2.Substring(num2, text2.Length - num2) : text2.Substring(num2, num3 - num2));
                                    empty2 = empty2.Trim();
                                    int result2 = 0;
                                    if (int.TryParse(empty2, out result2))
                                    {
                                        result2 = Math.Max(-50, Math.Min(result2, 50));
                                        list4.Add(result2);
                                        if (list4.Count > races.Count)
                                        {
                                            throw new ApplicationException("More bias values than races at line " + num + " in file " + text);
                                        }
                                        num2 = num3 + 1;
                                        num4++;
                                        continue;
                                    }
                                    throw new ApplicationException("Could not read Bias Value " + num4 + " at line " + num + " of file " + text);
                                }
                                list.Add(list4);
                                continue;
                            }
                            throw new ApplicationException("Could not read Race Name at line " + num + " of file " + text);
                        }
                        throw new ApplicationException("Could not read Race Index number at line " + num + " of file " + text);
                    }
                    throw new ApplicationException("Could not read Race Index number at line " + num + " of file " + text);
                }
                for (int i = 0; i < races.Count; i++)
                {
                    int num5 = list2.IndexOf(races[i].Name);
                    if (num5 < 0 || num5 >= list.Count)
                    {
                        continue;
                    }
                    List<int> list5 = new List<int>();
                    for (int j = 0; j < list[num5].Count; j++)
                    {
                        list5.Add(0);
                    }
                    for (int k = 0; k < list[num5].Count; k++)
                    {
                        int num6 = list2.IndexOf(races[k].Name);
                        if (num6 >= 0 && num6 < list[num5].Count)
                        {
                            list5[k] = list[num5][num6];
                        }
                    }
                    races[i].Biases = new RaceBiasList();
                    races[i].Biases.LoadBiases(races, list5);
                }
                streamReader.Close();
                fileStream.Close();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error at line " + num + " reading file " + text);
            }
        }

        public static double ResolveStandardRaceBias(Race race, Race otherRace)
        {
            double result = 0.0;
            if (race != null && otherRace != null)
            {
                if (race.Biases != null && race.Biases.Populated)
                {
                    return race.Biases.GetBias(otherRace);
                }
                if (race.FamilyId >= 0 && race.FamilyId < RaceFamiliesStatic.Count)
                {
                    return RaceFamiliesStatic[race.FamilyId].Biases.GetBias(otherRace.FamilyId);
                }
            }
            return result;
        }

        public Empire GetEmpireById(int empireId)
        {
            Empire empire = null;
            empire = ((empireId != IndependentEmpire.EmpireId) ? Empires.GetByEmpireId(empireId) : IndependentEmpire);
            if (empire == null)
            {
                empire = PirateEmpires.GetByEmpireId(empireId);
            }
            return empire;
        }

        public static string ResolveWarWearinessDescription(double warWearinessValue)
        {
            string result = string.Empty;
            if (warWearinessValue <= 0.0)
            {
                result = TextResolver.GetText("None");
            }
            else if (warWearinessValue > 0.0 && warWearinessValue <= 6.0)
            {
                result = TextResolver.GetText("Mild");
            }
            else if (warWearinessValue >= 6.0 && warWearinessValue <= 12.0)
            {
                result = TextResolver.GetText("Tolerable");
            }
            else if (warWearinessValue >= 12.0 && warWearinessValue <= 18.0)
            {
                result = TextResolver.GetText("Significant");
            }
            else if (warWearinessValue >= 18.0 && warWearinessValue <= 26.0)
            {
                result = TextResolver.GetText("Serious");
            }
            else if (warWearinessValue >= 26.0 && warWearinessValue <= 34.0)
            {
                result = TextResolver.GetText("Critical");
            }
            else if (warWearinessValue > 34.0)
            {
                result = TextResolver.GetText("Rampant");
            }
            return result;
        }

        public static List<string> ResolveRaceCharacteristics(Race race)
        {
            List<string> list = new List<string>();
            string empty = string.Empty;
            string empty2 = string.Empty;
            string empty3 = string.Empty;
            empty3 = ResolveRaceCharacteristicIntensity(race.AggressionLevel);
            empty = string.Format(arg1: (race.AggressionLevel < 100) ? TextResolver.GetText("Passive") : TextResolver.GetText("Aggressive"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: empty3);
            list.Add(empty);
            empty3 = ResolveRaceCharacteristicIntensity(race.CautionLevel);
            empty = string.Format(arg1: (race.CautionLevel < 100) ? TextResolver.GetText("Reckless") : TextResolver.GetText("Cautious"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: empty3);
            list.Add(empty);
            empty3 = ResolveRaceCharacteristicIntensity(race.FriendlinessLevel);
            empty = string.Format(arg1: (race.FriendlinessLevel < 100) ? TextResolver.GetText("Unfriendly") : TextResolver.GetText("Friendly"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: empty3);
            list.Add(empty);
            empty3 = ResolveRaceCharacteristicIntensity(race.IntelligenceLevel);
            empty = string.Format(arg1: (race.IntelligenceLevel < 100) ? TextResolver.GetText("Stupid") : TextResolver.GetText("Intelligent"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: empty3);
            list.Add(empty);
            empty3 = ResolveRaceCharacteristicIntensity(race.LoyaltyLevel);
            empty = string.Format(arg1: (race.LoyaltyLevel < 100) ? TextResolver.GetText("Unreliable") : TextResolver.GetText("Dependable"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: empty3);
            list.Add(empty);
            return list;
        }

        private static string ResolveRaceCharacteristicIntensity(int level)
        {
            string result = string.Empty;
            level -= 100;
            level = Math.Abs(level);
            if (level >= 30)
            {
                result = TextResolver.GetText("Extremely");
            }
            else if (level >= 17)
            {
                result = TextResolver.GetText("Very");
            }
            else if (level >= 6)
            {
                result = TextResolver.GetText("Quite");
            }
            else if (level < 6)
            {
                result = TextResolver.GetText("Slightly");
            }
            return result;
        }

        public static List<string> ResolveRaceBonuses(Race race)
        {
            List<string> list = new List<string>();
            if (race.EspionageBonus > 0)
            {
                list.Add(ResolveEmpireAbilityBonusDescriptionEspionage((double)race.EspionageBonus / 100.0));
            }
            if (race.ResearchBonus > 0)
            {
                list.Add(ResolveEmpireAbilityBonusDescriptionResearch((double)race.ResearchBonus / 100.0));
            }
            if (race.ResourceExtractionBonus > 0)
            {
                list.Add(ResolveEmpireAbilityBonusDescriptionResourceExtraction((double)race.ResourceExtractionBonus / 100.0));
            }
            if (race.SatisfactionModifier > 0)
            {
                list.Add(ResolveEmpireAbilityBonusDescriptionSatisfaction((double)race.SatisfactionModifier / 100.0));
            }
            if (race.ShipMaintenanceSavings > 0)
            {
                list.Add(ResolveEmpireAbilityBonusDescriptionShipMaintenance((double)race.ShipMaintenanceSavings / 100.0));
            }
            if (race.TroopMaintenanceSavings > 0)
            {
                list.Add(ResolveEmpireAbilityBonusDescriptionTroopMaintenance((double)race.TroopMaintenanceSavings / 100.0));
            }
            if (race.WarWearinessAttenuation > 0)
            {
                list.Add(ResolveEmpireAbilityBonusDescriptionWarWeariness((double)race.WarWearinessAttenuation / 100.0));
            }
            if (race.TradeBonus > 0)
            {
                list.Add(ResolveEmpireAbilityBonusDescriptionTrade((double)race.TradeBonus / 100.0));
            }
            return list;
        }

        public static string ResolveEmpireAbilityBonusDescriptionShipMaintenance(double value)
        {
            string result = string.Empty;
            if (value > 0.0)
            {
                result = string.Format(TextResolver.GetText("Ship Maintenance Ability Bonus"), "-" + value.ToString("0%"));
            }
            return result;
        }

        public static string ResolveEmpireAbilityBonusDescriptionTroopMaintenance(double value)
        {
            string result = string.Empty;
            if (value > 0.0)
            {
                result = string.Format(TextResolver.GetText("Troop Maintenance Ability Bonus"), "-" + value.ToString("0%"));
            }
            return result;
        }

        public static string ResolveEmpireAbilityBonusDescriptionResourceExtraction(double value)
        {
            string result = string.Empty;
            if (value > 0.0)
            {
                result = string.Format(TextResolver.GetText("Resource Extraction Ability Bonus"), "+" + value.ToString("0%"));
            }
            return result;
        }

        public static string ResolveEmpireAbilityBonusDescriptionWarWeariness(double value)
        {
            string result = string.Empty;
            if (value > 0.0)
            {
                result = string.Format(TextResolver.GetText("War Weariness Ability Bonus"), "-" + value.ToString("0%"));
            }
            return result;
        }

        public static string ResolveEmpireAbilityBonusDescriptionSatisfaction(double value)
        {
            string result = string.Empty;
            if (value > 0.0)
            {
                result = string.Format(TextResolver.GetText("Satisfaction Ability Bonus"), "+" + value.ToString("0%"));
            }
            return result;
        }

        public static string ResolveEmpireAbilityBonusDescriptionResearch(double value)
        {
            string result = string.Empty;
            if (value > 0.0)
            {
                result = string.Format(TextResolver.GetText("Research Ability Bonus"), "+" + value.ToString("0%"));
            }
            return result;
        }

        public static string ResolveEmpireAbilityBonusDescriptionEspionage(double value)
        {
            string result = string.Empty;
            if (value > 0.0)
            {
                result = string.Format(TextResolver.GetText("Espionage Ability Bonus"), "+" + value.ToString("0%"));
            }
            return result;
        }

        public static string ResolveEmpireAbilityBonusDescriptionTrade(double value)
        {
            string result = string.Empty;
            if (value > 0.0)
            {
                result = string.Format(TextResolver.GetText("Trade Ability Bonus"), "+" + value.ToString("0%"));
            }
            return result;
        }

        internal static ImageAttributes CalculateImageAttributes(Color tintColor)
        {
            float num = (float)(int)tintColor.R / 255f;
            float num2 = (float)(int)tintColor.G / 255f;
            float num3 = (float)(int)tintColor.B / 255f;
            float[][] newColorMatrix = new float[5][]
            {
            new float[5] { num, 0f, 0f, 0f, 0f },
            new float[5] { 0f, num2, 0f, 0f, 0f },
            new float[5] { 0f, 0f, num3, 0f, 0f },
            new float[5] { 0f, 0f, 0f, 1f, 0f },
            new float[5] { 0f, 0f, 0f, 0f, 1f }
            };
            ColorMatrix colorMatrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            return imageAttributes;
        }

        internal static Bitmap TintBitmap(Bitmap inputImage, Color tintColor)
        {
            ImageAttributes imageAttr = CalculateImageAttributes(tintColor);
            Bitmap bitmap = new Bitmap(inputImage);
            Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle destRect = new Rectangle(0, 0, inputImage.Width, inputImage.Height);
            graphics.DrawImage(inputImage, destRect, 0, 0, inputImage.Width, inputImage.Height, GraphicsUnit.Pixel, imageAttr);
            return bitmap;
        }

        public static int GenerateEmpireFlag(Color mainColor, Color secondaryColor, int flagStyle, List<Bitmap> flagShapes, ref Bitmap smallFlagPicture, ref Bitmap largeFlagPicture)
        {
            if (flagStyle < 0)
            {
                Random random = new Random((int)DateTime.Now.Ticks);
                flagStyle = random.Next(0, flagShapes.Count);
            }
            Bitmap bitmap = null;
            if (flagStyle < flagShapes.Count)
            {
                bitmap = flagShapes[flagStyle];
            }
            else if (flagShapes.Count > 0)
            {
                bitmap = flagShapes[0];
            }
            if (bitmap != null)
            {
                largeFlagPicture = new Bitmap(100, 60, PixelFormat.Format32bppPArgb);
                using (Graphics graphics = Graphics.FromImage(largeFlagPicture))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    SolidBrush brush = new SolidBrush(mainColor);
                    new SolidBrush(secondaryColor);
                    Rectangle rectangle = new Rectangle(0, 0, 100, 60);
                    graphics.FillRectangle(brush, rectangle);
                    using Bitmap bitmap2 = TintBitmap(bitmap, secondaryColor);
                    Rectangle srcRect = new Rectangle(0, 0, bitmap2.Width, bitmap2.Height);
                    graphics.DrawImage(bitmap2, rectangle, srcRect, GraphicsUnit.Pixel);
                }
                smallFlagPicture = new Bitmap(13, 8, PixelFormat.Format32bppPArgb);
                using Graphics graphics2 = Graphics.FromImage(smallFlagPicture);
                graphics2.SmoothingMode = SmoothingMode.AntiAlias;
                graphics2.CompositingQuality = CompositingQuality.HighQuality;
                graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics2.DrawImage(largeFlagPicture, new Rectangle(0, 0, 13, 8));
                return flagStyle;
            }
            return flagStyle;
        }

        public bool CheckEmpireColorUsed(bool isPirateFaction, Color color)
        {
            List<Color> colorList = DetermineUsedEmpireColors(isPirateFaction);
            return CheckListContainsColor(colorList, color);
        }

        public List<Color> DetermineUsedEmpireColors(bool isPirateFaction)
        {
            List<Color> list = new List<Color>();
            if (isPirateFaction)
            {
                for (int i = 0; i < PirateEmpires.Count; i++)
                {
                    list.Add(PirateEmpires[i].MainColor);
                }
            }
            else
            {
                for (int j = 0; j < Empires.Count; j++)
                {
                    list.Add(Empires[j].MainColor);
                }
            }
            return list;
        }

        public Color SelectUnusedMainColor(bool isPirateFaction, out int unusedColorKey)
        {
            unusedColorKey = 0;
            List<Color> colorList = DetermineUsedEmpireColors(isPirateFaction);
            List<Color> list = new List<Color>();
            List<int> list2 = new List<int>();
            List<Color> list3 = new List<Color>();
            List<int> list4 = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                if (isPirateFaction)
                {
                    list3.Add(SelectColorFromKeyDark(i));
                }
                else
                {
                    list3.Add(SelectColorFromKey(i));
                }
                list4.Add(i);
            }
            for (int j = 0; j < list3.Count; j++)
            {
                if (!CheckListContainsColor(colorList, list3[j]))
                {
                    list.Add(list3[j]);
                    list2.Add(list4[j]);
                }
            }
            Color result = Color.Transparent;
            if (list.Count > 0)
            {
                int index = Rnd.Next(0, list.Count);
                result = list[index];
                unusedColorKey = list2[index];
            }
            else
            {
                if (isPirateFaction)
                {
                    switch (Rnd.Next(0, 3))
                    {
                        case 0:
                            result = Color.FromArgb(Rnd.Next(48, 96), Rnd.Next(8, 64), Rnd.Next(8, 64));
                            break;
                        case 1:
                            result = Color.FromArgb(Rnd.Next(8, 64), Rnd.Next(48, 96), Rnd.Next(8, 64));
                            break;
                        case 2:
                            result = Color.FromArgb(Rnd.Next(8, 64), Rnd.Next(8, 64), Rnd.Next(48, 96));
                            break;
                    }
                }
                else
                {
                    result = Color.FromArgb(Rnd.Next(32, 256), Rnd.Next(32, 256), Rnd.Next(32, 256));
                }
                unusedColorKey = -1;
            }
            return result;
        }

        private bool CheckListContainsColor(List<Color> colorList, Color checkColor)
        {
            for (int i = 0; i < colorList.Count; i++)
            {
                if (CheckColorsEqual(colorList[i], checkColor))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckColorsEqual(Color color1, Color color2)
        {
            if (color1.R == color2.R && color1.G == color2.G && color1.B == color2.B)
            {
                return true;
            }
            return false;
        }

        public static Color DetermineContrastDropShadowColor(Color color)
        {
            return DetermineContrastDropShadowColor(color, Color.White, Color.Black);
        }

        public static Color DetermineContrastDropShadowColor(Color color, Color lightColor, Color darkColor)
        {
            double num = 1.0 - (0.333 * (double)(int)color.R + 0.333 * (double)(int)color.G + 0.333 * (double)(int)color.B) / 255.0;
            if (num < 0.7)
            {
                return darkColor;
            }
            return lightColor;
        }

        public static Color DetermineSecondaryColor(Color color)
        {
            int num = (color.R + color.G + color.B) / 3;
            if (num > 80 && num < 176)
            {
                Color darkColor = Color.Black;
                if (color.R > color.G && color.R > color.B)
                {
                    darkColor = Color.FromArgb(48, 0, 0);
                }
                else if (color.G > color.R && color.G > color.B)
                {
                    darkColor = Color.FromArgb(0, 48, 0);
                }
                else if (color.B > color.G && color.B > color.R)
                {
                    darkColor = Color.FromArgb(0, 0, 48);
                }
                return DetermineContrastDropShadowColor(color, Color.FromArgb(255, 255, 255), darkColor);
            }
            return DetermineContrastColor(color);
        }

        public static Color DetermineContrastColor(Color color)
        {
            int red = 255 - color.R;
            int green = 255 - color.G;
            int blue = 255 - color.B;
            return Color.FromArgb(red, green, blue);
        }

        public static Color DetermineComplementaryColor(Color color)
        {
            Color white = Color.White;
            int r = color.R;
            int g = color.G;
            int b = color.B;
            int num = Math.Min(r, Math.Min(g, b));
            int num2 = Math.Max(r, Math.Max(g, b));
            int num3 = num + num2;
            byte red = (byte)Math.Max(0, Math.Min(255, num3 - r));
            byte green = (byte)Math.Max(0, Math.Min(255, num3 - g));
            byte blue = (byte)Math.Max(0, Math.Min(255, num3 - b));
            return Color.FromArgb(red, green, blue);
        }

        public static int SelectComplementaryColorKey(int mainColorKey)
        {
            int result = 0;
            switch (mainColorKey)
            {
                case 0:
                    result = 3;
                    break;
                case 1:
                    result = 8;
                    break;
                case 2:
                    result = 20;
                    break;
                case 3:
                    result = 1;
                    break;
                case 4:
                    result = 14;
                    break;
                case 5:
                    result = 9;
                    break;
                case 6:
                    result = 4;
                    break;
                case 7:
                    result = 12;
                    break;
                case 8:
                    result = 11;
                    break;
                case 9:
                    result = 12;
                    break;
                case 10:
                    result = 20;
                    break;
                case 11:
                    result = 14;
                    break;
                case 12:
                    result = 9;
                    break;
                case 13:
                    result = 20;
                    break;
                case 14:
                    result = 12;
                    break;
                case 15:
                    result = 8;
                    break;
                case 16:
                    result = 20;
                    break;
                case 17:
                    result = 8;
                    break;
                case 18:
                    result = 11;
                    break;
                case 19:
                    result = 11;
                    break;
                case 20:
                    result = 10;
                    break;
                case 21:
                    result = 4;
                    break;
                case 22:
                    result = 19;
                    break;
            }
            return result;
        }

        public static Color SelectColorFromKey(int key)
        {
            Color result = Color.Empty;
            switch (key)
            {
                case 0:
                    result = Color.FromArgb(255, 0, 0, 176);
                    break;
                case 1:
                    result = Color.FromArgb(255, 0, 64, 232);
                    break;
                case 2:
                    result = Color.FromArgb(255, 0, 128, 255);
                    break;
                case 3:
                    result = Color.FromArgb(255, 0, 255, 255);
                    break;
                case 4:
                    result = Color.FromArgb(255, 24, 80, 24);
                    break;
                case 5:
                    result = Color.FromArgb(255, 0, 128, 0);
                    break;
                case 6:
                    result = Color.FromArgb(255, 0, 204, 0);
                    break;
                case 7:
                    result = Color.FromArgb(255, 160, 255, 0);
                    break;
                case 8:
                    result = Color.FromArgb(255, 255, 255, 32);
                    break;
                case 9:
                    result = Color.FromArgb(255, 255, 104, 31);
                    break;
                case 10:
                    result = Color.FromArgb(255, 255, 0, 48);
                    break;
                case 11:
                    result = Color.FromArgb(255, 135, 0, 0);
                    break;
                case 12:
                    result = Color.FromArgb(255, 96, 64, 0);
                    break;
                case 13:
                    result = Color.FromArgb(255, 144, 112, 48);
                    break;
                case 14:
                    result = Color.FromArgb(255, 224, 192, 96);
                    break;
                case 15:
                    result = Color.FromArgb(255, 168, 64, 255);
                    break;
                case 16:
                    result = Color.FromArgb(255, 112, 32, 204);
                    break;
                case 17:
                    result = Color.FromArgb(255, 132, 49, 121);
                    break;
                case 18:
                    result = Color.FromArgb(255, 255, 0, 255);
                    break;
                case 19:
                    result = Color.FromArgb(255, 255, 166, 201);
                    break;
                case 20:
                    result = Color.FromArgb(255, 255, 255, 255);
                    break;
                case 21:
                    result = Color.FromArgb(255, 153, 153, 51);
                    break;
                case 22:
                    result = Color.FromArgb(255, 192, 0, 128);
                    break;
                case 23:
                    result = Color.FromArgb(255, 1, 1, 1);
                    break;
            }
            return result;
        }

        public static Color SelectColorFromKeyDark(int key)
        {
            Color result = Color.Empty;
            switch (key)
            {
                case 0:
                    result = Color.FromArgb(255, 0, 0, 59);
                    break;
                case 1:
                    result = Color.FromArgb(255, 0, 21, 77);
                    break;
                case 2:
                    result = Color.FromArgb(255, 0, 43, 85);
                    break;
                case 3:
                    result = Color.FromArgb(255, 0, 85, 85);
                    break;
                case 4:
                    result = Color.FromArgb(255, 8, 27, 8);
                    break;
                case 5:
                    result = Color.FromArgb(255, 0, 43, 0);
                    break;
                case 6:
                    result = Color.FromArgb(255, 0, 68, 0);
                    break;
                case 7:
                    result = Color.FromArgb(255, 53, 85, 0);
                    break;
                case 8:
                    result = Color.FromArgb(255, 85, 85, 11);
                    break;
                case 9:
                    result = Color.FromArgb(255, 85, 35, 10);
                    break;
                case 10:
                    result = Color.FromArgb(255, 85, 0, 16);
                    break;
                case 11:
                    result = Color.FromArgb(255, 45, 0, 0);
                    break;
                case 12:
                    result = Color.FromArgb(255, 32, 21, 0);
                    break;
                case 13:
                    result = Color.FromArgb(255, 48, 37, 16);
                    break;
                case 14:
                    result = Color.FromArgb(255, 75, 64, 32);
                    break;
                case 15:
                    result = Color.FromArgb(255, 56, 21, 85);
                    break;
                case 16:
                    result = Color.FromArgb(255, 37, 11, 68);
                    break;
                case 17:
                    result = Color.FromArgb(255, 44, 16, 40);
                    break;
                case 18:
                    result = Color.FromArgb(255, 85, 0, 85);
                    break;
                case 19:
                    result = Color.FromArgb(255, 85, 55, 67);
                    break;
                case 20:
                    result = Color.FromArgb(255, 255, 255, 255);
                    break;
                case 21:
                    result = Color.FromArgb(255, 51, 51, 17);
                    break;
                case 22:
                    result = Color.FromArgb(255, 64, 0, 43);
                    break;
                case 23:
                    result = Color.FromArgb(255, 1, 1, 1);
                    break;
            }
            return result;
        }

        public float SelectRandomHeading()
        {
            return (float)(Math.PI - Rnd.NextDouble() * Math.PI * 2.0);
        }

        private void GeneratePirateOffers()
        {
            int maxValue = (int)(6.0 * _ColonyFillFactor);
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (Rnd.Next(0, maxValue) == 1 && empire.KnownPirateEmpires.Count > 0)
                {
                    int index = Rnd.Next(0, empire.KnownPirateEmpires.Count);
                    Empire empire2 = empire.KnownPirateEmpires[index];
                    if (empire2 != null && !empire2.PirateEmpireSuperPirates && empire.KnownPirateEmpires.Contains(empire2))
                    {
                        GeneratePirateOffersForSingleEmpire(empire2, empire);
                    }
                }
            }
        }

        public bool GeneratePirateOffersForSingleEmpire(Empire pirateFaction, Empire empire)
        {
            bool flag = false;
            if (empire != null && pirateFaction != PlayerEmpire)
            {
                PirateRelation pirateRelation = pirateFaction.ObtainPirateRelation(empire);
                if (pirateRelation != null)
                {
                    long num = (long)((double)RealSecondsInGalacticYear * 3.0 * 1000.0);
                    int num2 = empire.PirateRelations.CountKnownPirateFactions();
                    double num3 = Math.Min(10.0, Math.Max(1.0, (double)num2 / 3.0));
                    num = (long)((double)num * num3);
                    long num4 = (long)((double)RealSecondsInGalacticYear * 0.5);
                    long num5 = (long)(((double)num4 * 0.5 + (double)num4 * 0.5 * Rnd.NextDouble()) * num3);
                    num += num5;
                    long currentStarDate = CurrentStarDate;
                    long num6 = currentStarDate - num;
                    switch (Rnd.Next(0, 3))
                    {
                        case 0:
                            {
                                if (!pirateFaction.DetermineDesirePirateProtection(empire))
                                {
                                    break;
                                }
                                long num15 = (long)((double)RealSecondsInGalacticYear * 1.5 * 1000.0);
                                num15 = (long)((double)num15 * num3);
                                long num16 = currentStarDate - num15;
                                if (pirateRelation.Type != PirateRelationType.Protection && pirateRelation.LastOfferDate < num16)
                                {
                                    string text8 = TextResolver.GetText("Pirate Offer Protection");
                                    if (pirateFaction.PirateEmpireBaseHabitat != null && empire.PirateEmpireBaseHabitat != null)
                                    {
                                        text8 = TextResolver.GetText("Pirate Offer Protection Other Pirate");
                                    }
                                    pirateFaction.SendMessageToEmpire(empire, EmpireMessageType.PirateOfferProtection, null, text8);
                                    pirateRelation.LastOfferDate = currentStarDate;
                                }
                                break;
                            }
                        case 1:
                        case 2:
                            {
                                if (pirateRelation.LastInfoDate >= num6)
                                {
                                    break;
                                }
                                pirateFaction.GenerateSaleableInfoForEmpire(pirateFaction, empire, out var unmetEmpires, out var unexploredSystems, out var independentColonies, out var ruinHabitats, out var debrisFieldLocations, out var planetDestroyerLocations, out var restrictedAreaLocations);
                                if (!empire.CheckEmpireHasHyperDriveTech(empire))
                                {
                                    unmetEmpires.Clear();
                                    unexploredSystems.Clear();
                                }
                                if (unmetEmpires.Count > 0)
                                {
                                    Empire[] array = unmetEmpires.ToArray();
                                    foreach (Empire item in array)
                                    {
                                        if (empire != null && !empire.CheckEmpireHasHyperDriveTech(empire))
                                        {
                                            unmetEmpires.Remove(item);
                                        }
                                    }
                                }
                                bool flag2 = false;
                                if (unmetEmpires.Count > 0 || unexploredSystems.Count > 0 || independentColonies.Count > 0 || ruinHabitats.Count > 0 || debrisFieldLocations.Count > 0 || restrictedAreaLocations.Count > 0 || planetDestroyerLocations.Count > 0)
                                {
                                    flag2 = true;
                                }
                                double num7 = empire.StateMoney * 0.75;
                                if (!flag2)
                                {
                                    break;
                                }
                                int num8 = 0;
                                while (!flag && num8 < 10)
                                {
                                    switch (Rnd.Next(0, 4))
                                    {
                                        case 0:
                                            if (unmetEmpires.Count > 0)
                                            {
                                                int index7 = Rnd.Next(0, unmetEmpires.Count);
                                                string text7 = TextResolver.GetText("Pirate Offer Contact Empire");
                                                double value = (double)unmetEmpires[0].TotalColonyStrategicValue / 200.0;
                                                value = Math.Round(value, 0);
                                                value = Math.Min(value, 10000.0);
                                                if (num7 >= value)
                                                {
                                                    EmpireMessage empireMessage7 = new EmpireMessage(pirateFaction, EmpireMessageType.SellInfoUnmetEmpire, unmetEmpires[index7]);
                                                    empireMessage7.Description = text7;
                                                    empireMessage7.Money = (int)value;
                                                    pirateFaction.SendMessageToEmpire(empireMessage7, empire);
                                                    flag = true;
                                                }
                                            }
                                            break;
                                        case 1:
                                            if (unexploredSystems.Count > 0)
                                            {
                                                int index5 = Rnd.Next(0, unexploredSystems.Count);
                                                string text5 = TextResolver.GetText("Pirate Offer System Map");
                                                double num13 = 2000.0;
                                                if (num7 >= num13)
                                                {
                                                    EmpireMessage empireMessage5 = new EmpireMessage(pirateFaction, EmpireMessageType.SellInfoSystemMap, unexploredSystems[index5]);
                                                    empireMessage5.Description = text5;
                                                    empireMessage5.Money = (int)num13;
                                                    pirateFaction.SendMessageToEmpire(empireMessage5, empire);
                                                    flag = true;
                                                }
                                            }
                                            break;
                                        case 2:
                                            if (independentColonies.Count > 0)
                                            {
                                                int index6 = Rnd.Next(0, independentColonies.Count);
                                                string text6 = TextResolver.GetText("Pirate Offer Independent Colony");
                                                double num14 = 20000.0;
                                                if (num7 >= num14)
                                                {
                                                    EmpireMessage empireMessage6 = new EmpireMessage(pirateFaction, EmpireMessageType.SellInfoIndependentColony, independentColonies[index6]);
                                                    empireMessage6.Description = text6;
                                                    empireMessage6.Money = (int)num14;
                                                    pirateFaction.SendMessageToEmpire(empireMessage6, empire);
                                                    flag = true;
                                                }
                                            }
                                            break;
                                        case 3:
                                            if (ruinHabitats.Count > 0)
                                            {
                                                int index = Rnd.Next(0, ruinHabitats.Count);
                                                string text = TextResolver.GetText("Pirate Offer Discovery");
                                                double num9 = 30000.0;
                                                if (num7 >= num9)
                                                {
                                                    EmpireMessage empireMessage = new EmpireMessage(pirateFaction, EmpireMessageType.SellInfoRuins, ruinHabitats[index]);
                                                    empireMessage.Description = text;
                                                    empireMessage.Money = (int)num9;
                                                    pirateFaction.SendMessageToEmpire(empireMessage, empire);
                                                    flag = true;
                                                }
                                            }
                                            else if (restrictedAreaLocations.Count > 0)
                                            {
                                                int index2 = Rnd.Next(0, restrictedAreaLocations.Count);
                                                string text2 = TextResolver.GetText("Pirate Offer Discovery");
                                                double num10 = 30000.0;
                                                if (num7 >= num10)
                                                {
                                                    EmpireMessage empireMessage2 = new EmpireMessage(pirateFaction, EmpireMessageType.SellInfoRestrictedArea, restrictedAreaLocations[index2]);
                                                    empireMessage2.Description = text2;
                                                    empireMessage2.Money = (int)num10;
                                                    pirateFaction.SendMessageToEmpire(empireMessage2, empire);
                                                    flag = true;
                                                }
                                            }
                                            else if (debrisFieldLocations.Count > 0)
                                            {
                                                int index3 = Rnd.Next(0, debrisFieldLocations.Count);
                                                string text3 = TextResolver.GetText("Pirate Offer Discovery");
                                                double num11 = 30000.0;
                                                if (num7 >= num11)
                                                {
                                                    EmpireMessage empireMessage3 = new EmpireMessage(pirateFaction, EmpireMessageType.SellInfoDebrisField, debrisFieldLocations[index3]);
                                                    empireMessage3.Description = text3;
                                                    empireMessage3.Money = (int)num11;
                                                    pirateFaction.SendMessageToEmpire(empireMessage3, empire);
                                                    flag = true;
                                                }
                                            }
                                            else if (planetDestroyerLocations.Count > 0)
                                            {
                                                int index4 = Rnd.Next(0, planetDestroyerLocations.Count);
                                                string text4 = TextResolver.GetText("Pirate Offer Discovery");
                                                double num12 = 30000.0;
                                                if (num7 >= num12)
                                                {
                                                    EmpireMessage empireMessage4 = new EmpireMessage(pirateFaction, EmpireMessageType.SellInfoPlanetDestroyer, planetDestroyerLocations[index4]);
                                                    empireMessage4.Description = text4;
                                                    empireMessage4.Money = (int)num12;
                                                    pirateFaction.SendMessageToEmpire(empireMessage4, empire);
                                                    flag = true;
                                                }
                                            }
                                            break;
                                    }
                                    if (flag)
                                    {
                                        pirateRelation.LastInfoDate = currentStarDate;
                                    }
                                    num8++;
                                }
                                break;
                            }
                    }
                }
            }
            return flag;
        }

        private void ReviewColonyFillFactor()
        {
            int num = 0;
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                num += empire.Colonies.Count;
            }
            int num2 = Math.Min(700, StarCount);
            _ColonyFillFactor = 10.0 * ((double)num / (double)num2);
            _ColonyFillFactor = Math.Min(2.5, Math.Max(0.7, _ColonyFillFactor));
        }

        public void ReseedRandom()
        {
            SetRandom(new Random((int)DateTime.Now.Ticks));
            ResetRandom = false;
        }

        public void DoTasksTimeSensitive()
        {
            long currentStarDate = CurrentStarDate;
            DateTime currentDateTime = CurrentDateTime;
            DoTasksTimeSensitive(currentStarDate, currentDateTime);
        }

        public void DoTasksTimeSensitive(long starDate, DateTime time)
        {
            double totalSeconds = time.Subtract(_LastGalaxyProcessTimeSensitive).TotalSeconds;
            ReviewPirateMissionsAndAssign(starDate, totalSeconds);
            ProcessDelayedEventActions(starDate);
            _LastGalaxyProcessTimeSensitive = time;
        }

        public void DoTasks(bool gameFinished, Empire playerEmpire, VictoryConditions globalVictoryConditions, EmpireVictoryConditions playerConditionsToAchieve, EmpireVictoryConditions playerConditionsToPrevent)
        {
            DateTime currentDateTime = CurrentDateTime;
            TimeSpan timeSpan = currentDateTime.Subtract(_LastGalaxyProcessTime);
            TimeSpan timeSpan2 = currentDateTime.Subtract(_LastGalaxyHugeProcessTime);
            if (timeSpan.TotalSeconds < 0.0)
            {
                _LastGalaxyProcessTime = currentDateTime;
            }
            if (timeSpan2.TotalSeconds < 0.0)
            {
                _LastGalaxyHugeProcessTime = currentDateTime;
            }
            if (timeSpan2 >= HugeProcessingSpan)
            {
                _LastGalaxyHugeProcessTime = currentDateTime;
            }
            if (timeSpan >= LongProcessingSpan)
            {
                _LastGalaxyProcessTime = currentDateTime;
            }
            if (ResetRandom)
            {
                ReseedRandom();
            }
            ProcessPirateFleets(currentDateTime);
            bool flag = false;
            if (timeSpan2 >= HugeProcessingSpan)
            {
                ReviewEmpireTerritory(onlySystems: false);
                flag = true;
                SelectPopularDesignCandidates();
                DoGalaxyEvents();
                CleanupInvalidShipsInIndexes();
                ReseedRandom();
            }
            if (timeSpan >= LongProcessingSpan)
            {
                DeferEventsForGameStart = false;
                ReviewResourcePrices();
                ReviewComponentPrices();
                RemoveCompletedOrders();
                CancelExpiredOrders();
                UpdateSystemInfo(playerEmpire);
                ReviewIndependentColonies();
                IndependentEmpire.UpdateEmpireRefuellingLocations();
                IdentifyDisputedBases();
                GenerateIndependentTraders();
                AssignIndependentTraderMissions();
                GenerateNewPirateEmpires();
                CheckForTerminatedPirateEmpires();
                GenerateNewPirateShips();
                DoSuperPirateTasks();
                ClearEmptyDebrisFields();
                ClearCompletedPlanetDestroyerProjects();
                CheckMergePirateFactions();
                ReviewPirateEmpireActivities();
                MaintainIndependentColonyFuelLevels();
                IndependentEmpire.CheckMarketOrders();
                long currentStarDate = CurrentStarDate;
                IndependentEmpire.ReviewPirateSmugglingMissions(currentStarDate);
                IndependentEmpire.ReviewPirateDefendMissions(currentStarDate);
                IndependentColoniesMakeSmugglingOffersToPirates(currentStarDate);
                IndependentColoniesMakeDefendOffersToPirates(currentStarDate);
                ReviewRacePeriodicChanges();
                ReviewColonyFillFactor();
                if (!flag)
                {
                    ReviewEmpireTerritory(onlySystems: true);
                }
                ReviewWondersBuilt();
                ReviewEmpireDifficultyFactors();
                ReviewAchievements();
                if (!gameFinished)
                {
                    CheckVictoryConditions(playerEmpire, globalVictoryConditions, playerConditionsToAchieve, playerConditionsToPrevent);
                }
            }
        }

        public void ResetLastTouchTimes()
        {
            _LastGalaxyHugeProcessTime = DateTime.MinValue;
            _LastGalaxyProcessTime = DateTime.MinValue;
            _LastGalaxyProcessTimeSensitive = DateTime.MinValue;
        }

        public void FixResearchParents()
        {
            SetAllParentsToRequired(ResearchNodeDefinitionsStatic.FindNodeById(273));
            SetAllParentsToRequired(ResearchNodeDefinitions.FindNodeById(273));
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire != null && empire.Research != null)
                {
                    SetAllParentsToRequired(empire.Research.TechTree.FindNodeById(273));
                }
            }
            for (int j = 0; j < PirateEmpires.Count; j++)
            {
                Empire empire2 = PirateEmpires[j];
                if (empire2 != null && empire2.Research != null)
                {
                    SetAllParentsToRequired(empire2.Research.TechTree.FindNodeById(273));
                }
            }
        }

        private void SetAllParentsToRequired(ResearchNodeDefinition project)
        {
            for (int i = 0; i < project.ParentIsRequired.Count; i++)
            {
                project.ParentIsRequired[i] = true;
            }
        }

        private void SetAllParentsToRequired(ResearchNode project)
        {
            for (int i = 0; i < project.ParentIsRequired.Count; i++)
            {
                project.ParentIsRequired[i] = true;
            }
        }

        public void SetAllEncounterPenaltiesToZero()
        {
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire == null || !empire.Active || empire.EmpireEvaluations == null)
                {
                    continue;
                }
                for (int j = 0; j < empire.EmpireEvaluations.Count; j++)
                {
                    EmpireEvaluation empireEvaluation = empire.EmpireEvaluations[j];
                    if (empireEvaluation != null)
                    {
                        DiplomaticRelation diplomaticRelation = empire.ObtainDiplomaticRelation(empireEvaluation.Empire);
                        if (diplomaticRelation != null && diplomaticRelation.Type != 0)
                        {
                            empireEvaluation.FirstContactPenalty = 0.0;
                        }
                    }
                }
            }
        }

        public void IndependentColoniesMakeDefendOffersToPirates(long starDate)
        {
            for (int i = 0; i < IndependentColonies.Count; i++)
            {
                Habitat habitat = IndependentColonies[i];
                if (habitat == null || habitat.HasBeenDestroyed || habitat.Empire != IndependentEmpire || Rnd.Next(0, 30) != 1)
                {
                    continue;
                }
                double attackPrice = IndependentEmpire.CalculatePirateDefendPrice(habitat);
                if (PirateEmpires.Count <= 0)
                {
                    continue;
                }
                long expiryDate = starDate + (long)(1.0 * (double)RealSecondsInGalacticYear * 1000.0);
                EmpireActivity empireActivity = new EmpireActivity(IndependentEmpire, habitat, attackPrice, IndependentEmpire, expiryDate, EmpireActivityType.Defend);
                if (IndependentEmpire.PirateMissions.ContainsEquivalent(empireActivity.Target, empireActivity.Type))
                {
                    continue;
                }
                IndependentEmpire.PirateMissions.Add(empireActivity);
                PirateMissions.Add(empireActivity);
                for (int j = 0; j < PirateEmpires.Count; j++)
                {
                    Empire empire = PirateEmpires[j];
                    if (empire != null && empire.PirateEmpireBaseHabitat != null && empire.IsObjectAreaKnownToThisEmpire(empireActivity.Target))
                    {
                        string description = string.Format(TextResolver.GetText("Pirate Defend Mission Available Independent"), empireActivity.Target.Name);
                        empire.SendMessageToEmpire(empire, EmpireMessageType.PirateDefendMissionAvailable, empireActivity, description);
                    }
                }
            }
        }

        public void IndependentColoniesMakeSmugglingOffersToPirates(long starDate)
        {
            OrderList orders = Orders.GetOrders(IndependentEmpire);
            long maximumOrderTimeLength = (long)((double)RealSecondsInGalacticYear * 1000.0 * 0.25);
            int maximumAmountOutstanding = 100;
            for (int i = 0; i < IndependentColonies.Count; i++)
            {
                Habitat habitat = IndependentColonies[i];
                if (habitat == null || habitat.HasBeenDestroyed || habitat.Population == null || habitat.Population.Count <= 0 || habitat.Empire != IndependentEmpire || Rnd.Next(0, 2) != 1)
                {
                    continue;
                }
                byte deficientResourceId = byte.MaxValue;
                if (!IndependentEmpire.DetermineColonyDeficientInResources(habitat, orders, checkForExistingSmugglingMission: true, maximumOrderTimeLength, maximumAmountOutstanding, out deficientResourceId))
                {
                    continue;
                }
                double attackPrice = IndependentEmpire.CalculatePirateSmugglePricePerUnit(habitat, deficientResourceId);
                long expiryDate = starDate + (long)(3.0 * (double)RealSecondsInGalacticYear * 1000.0);
                EmpireActivity empireActivity = new EmpireActivity(IndependentEmpire, habitat, attackPrice, IndependentEmpire, expiryDate, EmpireActivityType.Smuggle);
                empireActivity.ResourceId = deficientResourceId;
                if (IndependentEmpire.PirateMissions.ContainsEquivalent(empireActivity.Target, empireActivity.Type))
                {
                    continue;
                }
                Order order = (empireActivity.RelatedOrder = CreateOrder(habitat, new Resource(deficientResourceId), 10000, isState: true, expiryDate));
                IndependentEmpire.PirateMissions.Add(empireActivity);
                PirateMissions.Add(empireActivity);
                for (int j = 0; j < PirateEmpires.Count; j++)
                {
                    Empire empire = PirateEmpires[j];
                    if (empire != null && empire.PirateEmpireBaseHabitat != null && empire.IsObjectAreaKnownToThisEmpire(empireActivity.Target))
                    {
                        string empty = string.Empty;
                        empty = ((empireActivity.ResourceId != byte.MaxValue) ? string.Format(TextResolver.GetText("Pirate Smuggle Mission Available Independent"), empireActivity.Target.Name, new Resource(empireActivity.ResourceId).Name) : string.Format(TextResolver.GetText("Pirate Smuggle Mission Available Independent All Resources"), empireActivity.Target.Name));
                        IndependentEmpire.SendMessageToEmpire(empire, EmpireMessageType.PirateSmugglingMissionAvailable, empireActivity, empty);
                    }
                }
            }
        }

        private void ProcessPirateFleets(DateTime galaxyDate)
        {
            if (PirateEmpires == null)
            {
                return;
            }
            for (int i = 0; i < PirateEmpires.Count; i++)
            {
                Empire empire = PirateEmpires[i];
                if (empire != null && empire.Active && empire.ShipGroups != null)
                {
                    for (int j = 0; j < empire.ShipGroups.Count; j++)
                    {
                        empire.ShipGroups[j]?.DoTasks(galaxyDate);
                    }
                }
            }
        }

        private void DoGalaxyEvents()
        {
            if (GameDisasterEventsEnabled && _PiratePrevalence > 0.0)
            {
                int num = 15;
                if (_SuperPirateFactionsGenerated > 0)
                {
                    num = (int)((double)num * Math.Sqrt(1 + _SuperPirateFactionsGenerated));
                }
                if (Rnd.Next(0, num) == 1)
                {
                    GalaxyEventSuperPirates();
                }
            }
        }

        private void GalaxyEventSuperPirates()
        {
            if (NextEmpireID >= MaximumEmpireCount || !(ColonyFillRatio > 0.2) || PirateEmpires == null)
            {
                return;
            }
            Empire empire = null;
            for (int i = 0; i < PirateEmpires.Count; i++)
            {
                Empire empire2 = PirateEmpires[i];
                if (empire2.PirateEmpireSuperPirates)
                {
                    empire = empire2;
                    break;
                }
            }
            if (empire != null)
            {
                return;
            }
            Habitat habitat = null;
            double num = 0.0;
            if (PlayerEmpire != null && PlayerEmpire.Colonies != null && PlayerEmpire.Capital != null)
            {
                for (int j = 0; j < PlayerEmpire.Colonies.Count; j++)
                {
                    Habitat habitat2 = PlayerEmpire.Colonies[j];
                    if (habitat2 != null && !habitat2.HasBeenDestroyed)
                    {
                        double num2 = CalculateDistance(PlayerEmpire.Capital.Xpos, PlayerEmpire.Capital.Ypos, habitat2.Xpos, habitat2.Ypos);
                        if (num2 > num)
                        {
                            habitat = habitat2;
                            num = num2;
                        }
                    }
                }
            }
            if (habitat == null)
            {
                return;
            }
            ResourceDefinition resourceDefinition = ResourceSystem.FuelResources[0];
            Habitat habitat3 = FindNearestHabitatUnoccupiedSystemWithResourceNotVisibleToPlayer(habitat.Xpos, habitat.Ypos, resourceDefinition.ResourceID);
            if (habitat3 != null)
            {
                string[] array = new string[4] { "Deadly Phantoms", "Phantom Scourge", "Dark Ghostriders", "Dread Wraiths" };
                int num3 = Rnd.Next(0, array.Length);
                string name = array[num3];
                double techLevel = Math.Min(7.0, 4 + Math.Min(3, _SuperPirateFactionsGenerated));
                empire = GenerateSuperPirateFaction(habitat3, name, null, techLevel);
                if (empire != null)
                {
                    _SuperPirateFactionsGenerated++;
                }
            }
        }

        public void SetSuperPirateFactionsGenerated(int newValue)
        {
            _SuperPirateFactionsGenerated = newValue;
        }

        public void ReviewEmpireTerritory(bool onlySystems)
        {
            if (!_RegeneratingEmpireTerritory)
            {
                ThreadPool.QueueUserWorkItem(ReviewEmpireTerritoryCore, onlySystems);
            }
        }

        public void ReviewEmpireTerritoryCore(object state)
        {
            if (state == null || !(state is bool))
            {
                return;
            }
            bool onlySystems = (bool)state;
            if (!_RegeneratingEmpireTerritory)
            {
                _RegeneratingEmpireTerritory = true;
                bool flag = false;
                if (_EmpireTerritory == null)
                {
                    _EmpireTerritory = new EmpireTerritory();
                }
                if (_EmpireTerritory != null)
                {
                    _EmpireTerritory.ReviewEmpireTerritory(this, onlySystems);
                    flag = true;
                }
                if (_RegenerateEmpireTerritoryAgain)
                {
                    _RegenerateEmpireTerritoryAgain = false;
                    if (_EmpireTerritory != null)
                    {
                        _EmpireTerritory.ReviewEmpireTerritory(this, onlySystems);
                        flag = true;
                    }
                }
                if (flag)
                {
                    OnRefreshView(new RefreshViewEventArgs(0.0, 0.0, null, onlyGalaxyBackdrops: true));
                }
                _RegeneratingEmpireTerritory = false;
            }
            else
            {
                _RegenerateEmpireTerritoryAgain = true;
            }
        }

        private void ReviewRacePeriodicChanges()
        {
            long currentStarDate = CurrentStarDate;
            for (int i = 0; i < Races.Count; i++)
            {
                Race race = Races[i];
                if (race.ChangePeriodYearsInterval <= 0 || race.ChangePeriodYearsLength <= 0)
                {
                    continue;
                }
                int num = (int)((currentStarDate - ActualStartDate) / (RealSecondsInGalacticYear * 1000));
                if ((num - race.ChangePeriodYearsInterval) % (race.ChangePeriodYearsInterval + race.ChangePeriodYearsLength) == 0)
                {
                    if (race.ChangePeriodActive)
                    {
                        continue;
                    }
                    if (PlayerEmpire != null)
                    {
                        if (PlayerEmpire.DominantRace == race)
                        {
                            string text = ResolveRaceChangeQualitiesDescription(race);
                            string description = string.Format(TextResolver.GetText("Race Periodic Change Begin"), race.Name, race.ChangePeriodYearsInterval.ToString(), race.ChangePeriodYearsLength.ToString(), text);
                            PlayerEmpire.SendMessageToEmpireWithTitle(PlayerEmpire, EmpireMessageType.GeneralNeutralEvent, race, description, string.Format(TextResolver.GetText("Race Periodic Change Begin Title"), race.Name));
                        }
                        else
                        {
                            for (int j = 0; j < PlayerEmpire.DiplomaticRelations.Count; j++)
                            {
                                DiplomaticRelation diplomaticRelation = PlayerEmpire.DiplomaticRelations[j];
                                if (diplomaticRelation.Type != 0 && diplomaticRelation.OtherEmpire != null && diplomaticRelation.OtherEmpire.DominantRace == race)
                                {
                                    string text2 = ResolveRaceChangeQualitiesDescription(race);
                                    string description2 = string.Format(TextResolver.GetText("Race Periodic Change Begin Other"), race.Name, race.ChangePeriodYearsInterval.ToString(), race.ChangePeriodYearsLength.ToString(), text2);
                                    PlayerEmpire.SendMessageToEmpireWithTitle(PlayerEmpire, EmpireMessageType.GeneralNeutralEvent, race, description2, string.Format(TextResolver.GetText("Race Periodic Change Begin Title"), race.Name));
                                    break;
                                }
                            }
                        }
                    }
                    race.ChangePeriodActive = true;
                }
                else
                {
                    if (num % (race.ChangePeriodYearsInterval + race.ChangePeriodYearsLength) != 0 || !race.ChangePeriodActive)
                    {
                        continue;
                    }
                    race.ChangePeriodActive = false;
                    if (PlayerEmpire == null)
                    {
                        continue;
                    }
                    if (PlayerEmpire.DominantRace == race)
                    {
                        string description3 = string.Format(TextResolver.GetText("Race Periodic Change End"), race.Name, race.ChangePeriodYearsInterval.ToString());
                        PlayerEmpire.SendMessageToEmpireWithTitle(PlayerEmpire, EmpireMessageType.GeneralNeutralEvent, race, description3, string.Format(TextResolver.GetText("Race Periodic Change End Title"), race.Name));
                        continue;
                    }
                    for (int k = 0; k < PlayerEmpire.DiplomaticRelations.Count; k++)
                    {
                        DiplomaticRelation diplomaticRelation2 = PlayerEmpire.DiplomaticRelations[k];
                        if (diplomaticRelation2.Type != 0 && diplomaticRelation2.OtherEmpire != null && diplomaticRelation2.OtherEmpire.DominantRace == race)
                        {
                            string description4 = string.Format(TextResolver.GetText("Race Periodic Change End Other"), race.Name, race.ChangePeriodYearsInterval.ToString());
                            PlayerEmpire.SendMessageToEmpireWithTitle(PlayerEmpire, EmpireMessageType.GeneralNeutralEvent, race, description4, string.Format(TextResolver.GetText("Race Periodic Change End Title"), race.Name));
                            break;
                        }
                    }
                }
            }
        }

        public static string ResolveRaceChangeQualitiesDescription(Race race)
        {
            string text = string.Empty;
            if (race.PeriodicAggressionLevel > race.AggressionLevelOriginal)
            {
                text = text + TextResolver.GetText("increased aggression") + ", ";
            }
            else if (race.PeriodicAggressionLevel < race.AggressionLevelOriginal)
            {
                text = text + TextResolver.GetText("decreased aggression") + ", ";
            }
            if (race.PeriodicCautionLevel > race.CautionLevelOriginal)
            {
                text = text + TextResolver.GetText("increased caution") + ", ";
            }
            else if (race.PeriodicCautionLevel < race.CautionLevelOriginal)
            {
                text = text + TextResolver.GetText("decreased caution") + ", ";
            }
            if (race.PeriodicFriendlinessLevel > race.FriendlinessLevelOriginal)
            {
                text = text + TextResolver.GetText("increased friendliness") + ", ";
            }
            else if (race.PeriodicFriendlinessLevel < race.FriendlinessLevelOriginal)
            {
                text = text + TextResolver.GetText("decreased friendliness") + ", ";
            }
            if (race.PeriodicGrowthRate > race.ReproductiveRateOriginal)
            {
                text = text + TextResolver.GetText("increased population growth") + ", ";
            }
            else if (race.PeriodicGrowthRate < race.ReproductiveRateOriginal)
            {
                text = text + TextResolver.GetText("decreased population growth") + ", ";
            }
            if (text.Length > 2)
            {
                text = text.Substring(0, text.Length - 2);
            }
            return text;
        }

        public bool CheckRemoveInvalidDockingShipsFromWaitQueue(StellarObject stellarObject)
        {
            if (stellarObject != null)
            {
                BuiltObjectList dockingBayWaitQueue = stellarObject.DockingBayWaitQueue;
                if (dockingBayWaitQueue != null)
                {
                    BuiltObjectList builtObjectList = new BuiltObjectList();
                    for (int i = 0; i < dockingBayWaitQueue.Count; i++)
                    {
                        BuiltObject builtObject = dockingBayWaitQueue[i];
                        if (builtObject != null && (builtObject.HasBeenDestroyed || !builtObject.IsFunctional || builtObject.TopSpeed == 0 || builtObject.Mission == null || builtObject.Mission.Type == BuiltObjectMissionType.Undefined))
                        {
                            builtObjectList.Add(builtObject);
                        }
                    }
                    if (builtObjectList.Count > 0)
                    {
                        for (int j = 0; j < builtObjectList.Count; j++)
                        {
                            dockingBayWaitQueue.Remove(builtObjectList[j]);
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public Empire CheckSystemOwnership(Habitat systemStar)
        {
            if (systemStar != null)
            {
                bool disputed = false;
                int num = _EmpireTerritory.CheckSystemOwnership(this, systemStar, out disputed);
                if (num >= 0)
                {
                    return Empires.GetByEmpireId(num);
                }
            }
            return null;
        }

        public Empire CheckSystemOwnership(Habitat systemStar, out bool disputed)
        {
            disputed = false;
            if (systemStar != null)
            {
                int num = _EmpireTerritory.CheckSystemOwnership(this, systemStar, out disputed);
                if (num >= 0)
                {
                    return Empires.GetByEmpireId(num);
                }
            }
            return null;
        }

        public int CheckSystemOwnershipId(Habitat systemStar)
        {
            if (systemStar != null)
            {
                bool disputed = false;
                return _EmpireTerritory.CheckSystemOwnership(this, systemStar, out disputed);
            }
            return -1;
        }

        public bool CheckEmpireTerritoryCanColonizeHabitat(Empire empire, Habitat habitat)
        {
            bool canColonizeBecauseAtWar = false;
            return CheckEmpireTerritoryCanColonizeHabitat(empire, habitat, out canColonizeBecauseAtWar);
        }

        public bool CheckEmpireTerritoryCanColonizeHabitat(Empire empire, Habitat habitat, out bool canColonizeBecauseAtWar)
        {
            canColonizeBecauseAtWar = false;
            bool disputed = false;
            Habitat systemStar = DetermineHabitatSystemStar(habitat);
            int num = _EmpireTerritory.CheckSystemOwnership(this, systemStar, out disputed);
            if (num >= 0 && num != empire.EmpireId)
            {
                Empire byEmpireId = Empires.GetByEmpireId(num);
                DiplomaticRelation diplomaticRelation = empire.ObtainDiplomaticRelation(byEmpireId);
                if (diplomaticRelation.Type == DiplomaticRelationType.War)
                {
                    canColonizeBecauseAtWar = true;
                    return true;
                }
                return false;
            }
            if (disputed)
            {
                return false;
            }
            return true;
        }

        public bool CheckEmpireTerritoryCanBuildAtHabitat(Empire empire, Habitat habitat)
        {
            if (habitat.Owner == empire)
            {
                return true;
            }
            bool disputed = false;
            Habitat systemStar = DetermineHabitatSystemStar(habitat);
            List<int> otherEmpireIds;
            int num = _EmpireTerritory.CheckSystemOwnershipWithOthers(this, systemStar, out disputed, out otherEmpireIds);
            if (disputed)
            {
                return true;
            }
            if (num >= 0 && num != empire.EmpireId)
            {
                Empire byEmpireId = Empires.GetByEmpireId(num);
                if (byEmpireId != null)
                {
                    if (empire.PirateEmpireBaseHabitat != null || byEmpireId.PirateEmpireBaseHabitat != null)
                    {
                        return true;
                    }
                    DiplomaticRelation diplomaticRelation = byEmpireId.ObtainDiplomaticRelation(empire);
                    if (diplomaticRelation != null && diplomaticRelation.MiningRightsToOther)
                    {
                        return true;
                    }
                }
                return BaconGalaxy.CheckEmpireTerritoryCanBuildAtHabitat(this, empire, habitat);
            }
            return true;
        }

        public bool CheckEmpireTerritoryCanBuildAtLocation(Empire empire, double x, double y)
        {
            int num = _EmpireTerritory.CheckLocationOwnership(x, y);
            if (num >= 0 && num != empire.EmpireId)
            {
                Empire byEmpireId = Empires.GetByEmpireId(num);
                if (byEmpireId != null)
                {
                    if (empire.PirateEmpireBaseHabitat != null || byEmpireId.PirateEmpireBaseHabitat != null)
                    {
                        return true;
                    }
                    DiplomaticRelation diplomaticRelation = byEmpireId.ObtainDiplomaticRelation(empire);
                    if (diplomaticRelation != null && diplomaticRelation.MiningRightsToOther)
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }

        public int CheckEmpireTerritoryIdAtLocation(double x, double y)
        {
            return _EmpireTerritory.CheckLocationOwnership(x, y);
        }

        public bool CheckMilitaryShipWelcomeAtTerritoryLocation(double x, double y, Empire empire)
        {
            if (empire != null)
            {
                int num = _EmpireTerritory.CheckLocationOwnership(x, y);
                if (num < 0 || num == empire.EmpireId)
                {
                    return true;
                }
                Empire byEmpireId = Empires.GetByEmpireId(num);
                if (byEmpireId != null)
                {
                    DiplomaticRelation diplomaticRelation = byEmpireId.ObtainDiplomaticRelation(empire);
                    if (diplomaticRelation.MilitaryRefuelingToOther)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public double CalculateEmpireColonyProximityValueAtPoint(Empire empire, double x, double y, double distanceThresholdSquared)
        {
            double num = 0.0;
            for (int i = 0; i < empire.Colonies.Count; i++)
            {
                Habitat habitat = empire.Colonies[i];
                if (habitat != null && !habitat.HasBeenDestroyed)
                {
                    double num2 = CalculateDistanceSquared(x, y, habitat.Xpos, habitat.Ypos);
                    if (num2 < distanceThresholdSquared)
                    {
                        double val = (double)habitat.StrategicValue * 1000.0 / (num2 / 1000000.0);
                        val = Math.Min(val, 1000000000.0);
                        num += val;
                    }
                }
            }
            return num;
        }

        public void IdentifyDisputedBases()
        {
            for (int i = 0; i < Empires.Count; i++)
            {
                Empire empire = Empires[i];
                if (empire == null || !empire.Active)
                {
                    continue;
                }
                if (empire.DisputedBases == null)
                {
                    empire.DisputedBases = new BuiltObjectList();
                }
                empire.DisputedBases.Clear();
                for (int j = 0; j < empire.BuiltObjects.Count; j++)
                {
                    BuiltObject builtObject = empire.BuiltObjects[j];
                    if (builtObject.Role == BuiltObjectRole.Base && !builtObject.HasBeenDestroyed)
                    {
                        if (!((builtObject.ParentHabitat == null) ? CheckEmpireTerritoryCanBuildAtLocation(empire, builtObject.Xpos, builtObject.Ypos) : CheckEmpireTerritoryCanBuildAtHabitat(empire, builtObject.ParentHabitat)))
                        {
                            empire.DisputedBases.Add(builtObject);
                        }
                    }
                }
                for (int k = 0; k < empire.PrivateBuiltObjects.Count; k++)
                {
                    BuiltObject builtObject2 = empire.PrivateBuiltObjects[k];
                    if (builtObject2.Role == BuiltObjectRole.Base && !builtObject2.HasBeenDestroyed)
                    {
                        if (!((builtObject2.ParentHabitat == null) ? CheckEmpireTerritoryCanBuildAtLocation(empire, builtObject2.Xpos, builtObject2.Ypos) : CheckEmpireTerritoryCanBuildAtHabitat(empire, builtObject2.ParentHabitat)))
                        {
                            empire.DisputedBases.Add(builtObject2);
                        }
                    }
                }
            }
        }

        private void CleanupInvalidShipsInIndexes()
        {
            for (int i = 0; i < IndexMaxX; i++)
            {
                for (int j = 0; j < IndexMaxY; j++)
                {
                    BuiltObjectList builtObjectList = new BuiltObjectList();
                    for (int k = 0; k < BuiltObjectIndex[i][j].Count; k++)
                    {
                        BuiltObject builtObject = BuiltObjectIndex[i][j][k];
                        if (builtObject != null && builtObject.HasBeenDestroyed)
                        {
                            builtObjectList.Add(builtObject);
                        }
                    }
                    for (int l = 0; l < builtObjectList.Count; l++)
                    {
                        while (BuiltObjectIndex[i][j].Contains(builtObjectList[l]))
                        {
                            BuiltObjectIndex[i][j].Remove(builtObjectList[l]);
                        }
                    }
                }
            }
        }

        public static double CalculateRaceVictoryConditionsProgress(Galaxy galaxy, Empire empire, Race race, out RaceVictoryConditionProgressList conditionProgresses)
        {
            double num = 0.0;
            conditionProgresses = new RaceVictoryConditionProgressList();
            if (galaxy != null && race != null && empire != null)
            {
                RaceVictoryConditionList raceVictoryConditionList = null;
                raceVictoryConditionList = ((empire.PirateEmpireBaseHabitat != null) ? galaxy.ResolvePirateVictoryConditions(empire.PiratePlayStyle) : race.VictoryConditions);
                if (raceVictoryConditionList == null || raceVictoryConditionList.Count == 0)
                {
                    num = 1.0;
                }
                else
                {
                    _ = 1.0 / (double)raceVictoryConditionList.Count;
                    for (int i = 0; i < raceVictoryConditionList.Count; i++)
                    {
                        RaceVictoryCondition raceVictoryCondition = raceVictoryConditionList[i];
                        if (raceVictoryCondition != null)
                        {
                            string detail = string.Empty;
                            Empire bestEmpire = null;
                            double num2 = CalculateRaceVictoryConditionProgress(galaxy, empire, raceVictoryCondition, out detail, out bestEmpire);
                            double num3 = num2 * (double)(raceVictoryCondition.Proportion / 100f);
                            num += num3;
                            RaceVictoryConditionProgress item = new RaceVictoryConditionProgress(raceVictoryCondition.Type, num3, num2, bestEmpire, detail, raceVictoryCondition);
                            conditionProgresses.Add(item);
                        }
                    }
                }
            }
            conditionProgresses.Sort();
            conditionProgresses.Reverse();
            return num;
        }

        public static double RaceVictoryConditionMetCompareEmpires(Galaxy galaxy, Empire empire, RaceVictoryCondition condition, out string detail, out Empire bestEmpire)
        {
            detail = string.Empty;
            bestEmpire = null;
            if (galaxy != null && empire != null && empire.Counters != null && condition != null)
            {
                long currentStarDate = galaxy.CurrentStarDate;
                double num = 0.0;
                object obj = null;
                switch (condition.Type)
                {
                    case RaceVictoryConditionType.CaptureMostShips:
                        num = empire.Counters.CaptureShipCount;
                        break;
                    case RaceVictoryConditionType.PirateEliminateMostPirateFactions:
                        num = empire.Counters.EliminatePirateEmpireCount;
                        break;
                    case RaceVictoryConditionType.PirateMostProtectionIncome:
                        num = empire.Counters.PirateProtectionIncome;
                        break;
                    case RaceVictoryConditionType.PirateMostSmugglingIncome:
                        num = empire.Counters.PirateSmugglingIncome;
                        break;
                    case RaceVictoryConditionType.PirateMostSuccessfulMissionsAttack:
                        num = empire.Counters.CompletedPirateMissionAttackCount;
                        break;
                    case RaceVictoryConditionType.PirateMostSuccessfulRaids:
                        num = empire.Counters.RaidSuccessCount;
                        break;
                    case RaceVictoryConditionType.PirateMostSuccessfulMissionsDefend:
                        num = empire.Counters.CompletedPirateMissionDefendCount;
                        break;
                    case RaceVictoryConditionType.PirateBuildMostHiddenBases:
                        num = empire.Colonies.CountPirateControlledColoniesWithHiddenPirateBase(empire);
                        break;
                    case RaceVictoryConditionType.ConquerMostEnemyColonies:
                        num = empire.Counters.ColoniesConqueredCount;
                        break;
                    case RaceVictoryConditionType.ControlMostRuins:
                        num = empire.Colonies.CountHabitatWithRuins();
                        break;
                    case RaceVictoryConditionType.DestroyMostCreaturesByType:
                        if (condition.AdditionalData is CreatureType)
                        {
                            switch ((CreatureType)condition.AdditionalData)
                            {
                                case CreatureType.SilverMist:
                                    num = empire.Counters.DestroyedCreatureCountSilverMist;
                                    break;
                                case CreatureType.Ardilus:
                                    num = empire.Counters.DestroyedCreatureCountArdilus;
                                    break;
                                case CreatureType.DesertSpaceSlug:
                                    num = empire.Counters.DestroyedCreatureCountSandSlug;
                                    break;
                                case CreatureType.Kaltor:
                                    num = empire.Counters.DestroyedCreatureCountKaltor;
                                    break;
                                case CreatureType.RockSpaceSlug:
                                    num = empire.Counters.DestroyedCreatureCountSpaceSlug;
                                    break;
                            }
                        }
                        break;
                    case RaceVictoryConditionType.DestroyMostShips:
                        num = empire.Counters.DestroyedEnemyMilitaryShipCount + empire.Counters.DestroyedEnemyCivilianShipCount;
                        break;
                    case RaceVictoryConditionType.DestroyMostTroops:
                        num = empire.Counters.DestroyedEnemyTroopCount;
                        break;
                    case RaceVictoryConditionType.ExploreMostSystems:
                        num = empire.SystemVisibility.CountExploredSystems();
                        break;
                    case RaceVictoryConditionType.ExterminateOrEnslaveMostPopulation:
                        num = empire.Counters.ExterminatedPopulationAmount + empire.CalculateEnslavedPopulationAmount();
                        break;
                    case RaceVictoryConditionType.HighestPrivateRevenue:
                        num = empire.Counters.ColonyPrivateRevenueTotal;
                        break;
                    case RaceVictoryConditionType.HighestTradeVolume:
                        num = empire.Counters.TradeIncomeTotalVolume;
                        break;
                    case RaceVictoryConditionType.LargestMilitary:
                        num = empire.CalculateMilitaryShipSizeTotal();
                        break;
                    case RaceVictoryConditionType.LargestMilitaryNonAllied:
                        num = empire.CalculateMilitaryShipSizeTotal();
                        break;
                    case RaceVictoryConditionType.MostTroops:
                        num = empire.Troops.CountTroopsNotRecruiting();
                        break;
                    case RaceVictoryConditionType.MostTroopsNonAllied:
                        num = empire.Troops.CountTroopsNotRecruiting();
                        break;
                    case RaceVictoryConditionType.LeastBrokenTreaties:
                        num = empire.Counters.BrokenTreatyCount;
                        break;
                    case RaceVictoryConditionType.LeastTimeWarring:
                        num = empire.Counters.TimeSpentAtWar(currentStarDate);
                        break;
                    case RaceVictoryConditionType.LeastTreaties:
                        num = empire.DiplomaticRelations.CountTreaties();
                        break;
                    case RaceVictoryConditionType.LeastWarsStarted:
                        num = empire.Counters.WarsWeStartedCount;
                        break;
                    case RaceVictoryConditionType.LoseFewestShips:
                        num = empire.Counters.LossesCivilianShipCount + empire.Counters.LossesMilitaryShipCount;
                        break;
                    case RaceVictoryConditionType.LoseFewestTroops:
                        num = empire.Counters.LossesTroopCount;
                        break;
                    case RaceVictoryConditionType.MostExperiencedAdmiral:
                        {
                            CharacterList charactersByRole2 = empire.Characters.GetCharactersByRole(CharacterRole.FleetAdmiral);
                            if (charactersByRole2.Count <= 0)
                            {
                                break;
                            }
                            Character character3 = null;
                            for (int j = 0; j < charactersByRole2.Count; j++)
                            {
                                Character character4 = charactersByRole2[j];
                                if (character3 == null || character4.GetSkillLevelTotal() > character3.GetSkillLevelTotal())
                                {
                                    character3 = character4;
                                }
                            }
                            num = character3.GetSkillLevelTotal();
                            obj = character3;
                            break;
                        }
                    case RaceVictoryConditionType.MostExperiencedGeneral:
                        {
                            CharacterList charactersByRole = empire.Characters.GetCharactersByRole(CharacterRole.TroopGeneral);
                            if (charactersByRole.Count <= 0)
                            {
                                break;
                            }
                            Character character = null;
                            for (int i = 0; i < charactersByRole.Count; i++)
                            {
                                Character character2 = charactersByRole[i];
                                if (character == null || character2.GetSkillLevelTotal() > character.GetSkillLevelTotal())
                                {
                                    character = character2;
                                }
                            }
                            num = character.GetSkillLevelTotal();
                            obj = character;
                            break;
                        }
                    case RaceVictoryConditionType.MostHomeworlds:
                        num = empire.CountHomeworldsOwned();
                        break;
                    case RaceVictoryConditionType.MostIntelligenceMissionsIntercepted:
                        num = empire.Counters.IntelligenceMissionSuccessCounterIntelligenceCount;
                        break;
                    case RaceVictoryConditionType.MostIntelligenceMissionsSucceed:
                        num = empire.Counters.IntelligenceMissionSuccessEspionageCount + empire.Counters.IntelligenceMissionSuccessSabotageCount;
                        break;
                    case RaceVictoryConditionType.MostMiningStations:
                        num = empire.MiningStations.Count;
                        break;
                    case RaceVictoryConditionType.MostResortBases:
                        num = empire.ResortBases.Count;
                        break;
                    case RaceVictoryConditionType.MostScientists:
                        num = empire.Characters.CountCharactersByRole(CharacterRole.Scientist);
                        break;
                    case RaceVictoryConditionType.MostSpaceports:
                        num = empire.SpacePorts.Count;
                        break;
                    case RaceVictoryConditionType.MostSubjugatedDominions:
                        num = empire.Counters.SubjugationsMade;
                        break;
                    case RaceVictoryConditionType.MostTimeWarring:
                        num = empire.Counters.TimeSpentAtWar(currentStarDate);
                        break;
                    case RaceVictoryConditionType.MostTourismIncome:
                        num = empire.Counters.TourismIncome;
                        break;
                    case RaceVictoryConditionType.MostTradeIncome:
                        num = empire.Counters.TradeIncomeStateBonus;
                        break;
                    case RaceVictoryConditionType.OldestFreeTradeAgreement:
                        {
                            DiplomaticRelation diplomaticRelation2 = empire.DiplomaticRelations.FindOldestRelationByType(DiplomaticRelationType.FreeTradeAgreement);
                            if (diplomaticRelation2 != null)
                            {
                                num = diplomaticRelation2.StartDateOfLastChange;
                                obj = diplomaticRelation2;
                            }
                            break;
                        }
                    case RaceVictoryConditionType.OldestMutualDefensePact:
                        {
                            DiplomaticRelation diplomaticRelation = empire.DiplomaticRelations.FindOldestRelationByType(DiplomaticRelationType.MutualDefensePact);
                            if (diplomaticRelation != null)
                            {
                                num = diplomaticRelation.StartDateOfLastChange;
                                obj = diplomaticRelation;
                            }
                            break;
                        }
                    case RaceVictoryConditionType.OwnLargestCapitalShip:
                        {
                            BuiltObject builtObject = empire.LargestCapitalShip();
                            if (builtObject != null)
                            {
                                num = builtObject.Size;
                                obj = builtObject;
                            }
                            break;
                        }
                    case RaceVictoryConditionType.PopulationHappiest:
                        num = empire.AverageHappiness();
                        break;
                    case RaceVictoryConditionType.PopulationHighest:
                        num = empire.TotalPopulation;
                        break;
                    case RaceVictoryConditionType.ResearchLeastAdvanced:
                        num = empire.Research.TechTree.CalculateTotalCostResearchedProjects();
                        break;
                    case RaceVictoryConditionType.ResearchMostAdvanced:
                        num = empire.Research.TechTree.CalculateTotalCostResearchedProjects();
                        break;
                    case RaceVictoryConditionType.ResearchMostCompletedBranches:
                        num = empire.Research.TechTree.CountCompletedCategories();
                        break;
                    case RaceVictoryConditionType.ResearchMostCompletedBranchesByIndustry:
                        if (condition.AdditionalData is IndustryType)
                        {
                            IndustryType industry = (IndustryType)condition.AdditionalData;
                            num = empire.Research.TechTree.CountCompletedCategories(industry);
                        }
                        break;
                    case RaceVictoryConditionType.MineMostResourcesLuxury:
                        num = empire.Counters.MiningExtractionLuxury;
                        break;
                    case RaceVictoryConditionType.MineMostResourcesStrategic:
                        num = empire.Counters.MiningExtractionGas + empire.Counters.MiningExtractionStrategic;
                        break;
                    case RaceVictoryConditionType.MineMostResourcesColonyManufactured:
                        num = empire.Counters.MiningExtractionColonyManufactured;
                        break;
                    case RaceVictoryConditionType.BuildMostMilitaryShips:
                        num = empire.Counters.BuildMilitaryShipCount;
                        break;
                    case RaceVictoryConditionType.BuildMostCivilianShips:
                        num = empire.Counters.BuildCivilianShipCount;
                        break;
                    case RaceVictoryConditionType.BuildMostBases:
                        num = empire.Counters.BuildBaseCount;
                        break;
                }
                bool flag = false;
                switch (condition.Type)
                {
                    case RaceVictoryConditionType.ControlMostRuins:
                    case RaceVictoryConditionType.PopulationHighest:
                    case RaceVictoryConditionType.PopulationHappiest:
                    case RaceVictoryConditionType.MostHomeworlds:
                    case RaceVictoryConditionType.OwnLargestCapitalShip:
                    case RaceVictoryConditionType.MostSpaceports:
                    case RaceVictoryConditionType.MostMiningStations:
                    case RaceVictoryConditionType.MostResortBases:
                    case RaceVictoryConditionType.DestroyMostShips:
                    case RaceVictoryConditionType.DestroyMostTroops:
                    case RaceVictoryConditionType.DestroyMostCreaturesByType:
                    case RaceVictoryConditionType.MostIntelligenceMissionsSucceed:
                    case RaceVictoryConditionType.MostIntelligenceMissionsIntercepted:
                    case RaceVictoryConditionType.ConquerMostEnemyColonies:
                    case RaceVictoryConditionType.ExterminateOrEnslaveMostPopulation:
                    case RaceVictoryConditionType.MostScientists:
                    case RaceVictoryConditionType.MostExperiencedAdmiral:
                    case RaceVictoryConditionType.MostExperiencedGeneral:
                    case RaceVictoryConditionType.ResearchMostAdvanced:
                    case RaceVictoryConditionType.ResearchMostCompletedBranches:
                    case RaceVictoryConditionType.ResearchMostCompletedBranchesByIndustry:
                    case RaceVictoryConditionType.HighestTradeVolume:
                    case RaceVictoryConditionType.MostTourismIncome:
                    case RaceVictoryConditionType.MostTradeIncome:
                    case RaceVictoryConditionType.HighestPrivateRevenue:
                    case RaceVictoryConditionType.LargestMilitary:
                    case RaceVictoryConditionType.LargestMilitaryNonAllied:
                    case RaceVictoryConditionType.MostTroops:
                    case RaceVictoryConditionType.MostTroopsNonAllied:
                    case RaceVictoryConditionType.MostTimeWarring:
                    case RaceVictoryConditionType.MostSubjugatedDominions:
                    case RaceVictoryConditionType.OldestMutualDefensePact:
                    case RaceVictoryConditionType.OldestFreeTradeAgreement:
                    case RaceVictoryConditionType.ExploreMostSystems:
                    case RaceVictoryConditionType.MineMostResourcesLuxury:
                    case RaceVictoryConditionType.MineMostResourcesStrategic:
                    case RaceVictoryConditionType.BuildMostMilitaryShips:
                    case RaceVictoryConditionType.BuildMostCivilianShips:
                    case RaceVictoryConditionType.BuildMostBases:
                    case RaceVictoryConditionType.CaptureMostShips:
                    case RaceVictoryConditionType.PirateBuildMostHiddenBases:
                    case RaceVictoryConditionType.PirateEliminateMostPirateFactions:
                    case RaceVictoryConditionType.PirateMostSuccessfulMissionsAttack:
                    case RaceVictoryConditionType.PirateMostSuccessfulMissionsDefend:
                    case RaceVictoryConditionType.PirateMostSmugglingIncome:
                    case RaceVictoryConditionType.PirateMostProtectionIncome:
                    case RaceVictoryConditionType.PirateMostSuccessfulRaids:
                    case RaceVictoryConditionType.MineMostResourcesColonyManufactured:
                        if (num > 0.0)
                        {
                            flag = true;
                        }
                        break;
                    case RaceVictoryConditionType.LoseFewestShips:
                    case RaceVictoryConditionType.LoseFewestTroops:
                    case RaceVictoryConditionType.ResearchLeastAdvanced:
                    case RaceVictoryConditionType.LeastWarsStarted:
                    case RaceVictoryConditionType.LeastBrokenTreaties:
                    case RaceVictoryConditionType.LeastTreaties:
                    case RaceVictoryConditionType.LeastTimeWarring:
                        flag = true;
                        break;
                }
                List<double> list = new List<double>();
                list.Add(num);
                double num2 = 0.0;
                double num3 = 0.0;
                object obj2 = null;
                switch (condition.Type)
                {
                    case RaceVictoryConditionType.OldestMutualDefensePact:
                    case RaceVictoryConditionType.OldestFreeTradeAgreement:
                        num2 = currentStarDate;
                        break;
                }
                EmpireList empireList = galaxy.Empires;
                if (empire.PirateEmpireBaseHabitat != null)
                {
                    empireList = galaxy.PirateEmpires;
                }
                for (int k = 0; k < empireList.Count; k++)
                {
                    Empire empire2 = empireList[k];
                    if (empire2 == null || !empire2.Active || empire2 == obj || empire2.DominantRace == null || !empire2.DominantRace.Playable || empire2.Counters == null)
                    {
                        continue;
                    }
                    double num4 = 0.0;
                    object obj3 = null;
                    switch (condition.Type)
                    {
                        case RaceVictoryConditionType.CaptureMostShips:
                            num4 = empire2.Counters.CaptureShipCount;
                            break;
                        case RaceVictoryConditionType.PirateEliminateMostPirateFactions:
                            num4 = empire2.Counters.EliminatePirateEmpireCount;
                            break;
                        case RaceVictoryConditionType.PirateMostProtectionIncome:
                            num4 = empire2.Counters.PirateProtectionIncome;
                            break;
                        case RaceVictoryConditionType.PirateMostSmugglingIncome:
                            num4 = empire2.Counters.PirateSmugglingIncome;
                            break;
                        case RaceVictoryConditionType.PirateMostSuccessfulMissionsAttack:
                            num4 = empire2.Counters.CompletedPirateMissionAttackCount;
                            break;
                        case RaceVictoryConditionType.PirateMostSuccessfulRaids:
                            num4 = empire2.Counters.RaidSuccessCount;
                            break;
                        case RaceVictoryConditionType.PirateMostSuccessfulMissionsDefend:
                            num4 = empire2.Counters.CompletedPirateMissionDefendCount;
                            break;
                        case RaceVictoryConditionType.PirateBuildMostHiddenBases:
                            num4 = empire2.Colonies.CountPirateControlledColoniesWithHiddenPirateBase(empire2);
                            break;
                        case RaceVictoryConditionType.ConquerMostEnemyColonies:
                            num4 = empire2.Counters.ColoniesConqueredCount;
                            break;
                        case RaceVictoryConditionType.ControlMostRuins:
                            num4 = empire2.Colonies.CountHabitatWithRuins();
                            break;
                        case RaceVictoryConditionType.DestroyMostCreaturesByType:
                            if (condition.AdditionalData is CreatureType)
                            {
                                switch ((CreatureType)condition.AdditionalData)
                                {
                                    case CreatureType.SilverMist:
                                        num4 = empire2.Counters.DestroyedCreatureCountSilverMist;
                                        break;
                                    case CreatureType.Ardilus:
                                        num4 = empire2.Counters.DestroyedCreatureCountArdilus;
                                        break;
                                    case CreatureType.DesertSpaceSlug:
                                        num4 = empire2.Counters.DestroyedCreatureCountSandSlug;
                                        break;
                                    case CreatureType.Kaltor:
                                        num4 = empire2.Counters.DestroyedCreatureCountKaltor;
                                        break;
                                    case CreatureType.RockSpaceSlug:
                                        num4 = empire2.Counters.DestroyedCreatureCountSpaceSlug;
                                        break;
                                }
                            }
                            break;
                        case RaceVictoryConditionType.DestroyMostShips:
                            num4 = empire2.Counters.DestroyedEnemyMilitaryShipCount + empire2.Counters.DestroyedEnemyCivilianShipCount;
                            break;
                        case RaceVictoryConditionType.DestroyMostTroops:
                            num4 = empire2.Counters.DestroyedEnemyTroopCount;
                            break;
                        case RaceVictoryConditionType.ExploreMostSystems:
                            num4 = empire2.SystemVisibility.CountExploredSystems();
                            break;
                        case RaceVictoryConditionType.ExterminateOrEnslaveMostPopulation:
                            num4 = empire2.Counters.ExterminatedPopulationAmount + empire2.CalculateEnslavedPopulationAmount();
                            break;
                        case RaceVictoryConditionType.HighestPrivateRevenue:
                            num4 = empire2.Counters.ColonyPrivateRevenueTotal;
                            break;
                        case RaceVictoryConditionType.HighestTradeVolume:
                            num4 = empire2.Counters.TradeIncomeTotalVolume;
                            break;
                        case RaceVictoryConditionType.LargestMilitary:
                            num4 = empire2.CalculateMilitaryShipSizeTotal();
                            break;
                        case RaceVictoryConditionType.LargestMilitaryNonAllied:
                            {
                                DiplomaticRelation diplomaticRelation6 = empire.ObtainDiplomaticRelation(empire2);
                                if (diplomaticRelation6.Type != DiplomaticRelationType.MutualDefensePact && diplomaticRelation6.Type != DiplomaticRelationType.Protectorate)
                                {
                                    num4 = empire2.CalculateMilitaryShipSizeTotal();
                                }
                                break;
                            }
                        case RaceVictoryConditionType.MostTroops:
                            num4 = empire2.Troops.CountTroopsNotRecruiting();
                            break;
                        case RaceVictoryConditionType.MostTroopsNonAllied:
                            {
                                DiplomaticRelation diplomaticRelation5 = empire.ObtainDiplomaticRelation(empire2);
                                if (diplomaticRelation5.Type != DiplomaticRelationType.MutualDefensePact && diplomaticRelation5.Type != DiplomaticRelationType.Protectorate)
                                {
                                    num4 = empire2.Troops.CountTroopsNotRecruiting();
                                }
                                break;
                            }
                        case RaceVictoryConditionType.LeastBrokenTreaties:
                            num4 = empire2.Counters.BrokenTreatyCount;
                            break;
                        case RaceVictoryConditionType.LeastTimeWarring:
                            num4 = empire2.Counters.TimeSpentAtWar(currentStarDate);
                            break;
                        case RaceVictoryConditionType.LeastTreaties:
                            num4 = empire2.DiplomaticRelations.CountTreaties();
                            break;
                        case RaceVictoryConditionType.LeastWarsStarted:
                            num4 = empire2.Counters.WarsWeStartedCount;
                            break;
                        case RaceVictoryConditionType.LoseFewestShips:
                            num4 = empire2.Counters.LossesCivilianShipCount + empire2.Counters.LossesMilitaryShipCount;
                            break;
                        case RaceVictoryConditionType.LoseFewestTroops:
                            num4 = empire2.Counters.LossesTroopCount;
                            break;
                        case RaceVictoryConditionType.MostExperiencedAdmiral:
                            {
                                CharacterList charactersByRole4 = empire2.Characters.GetCharactersByRole(CharacterRole.FleetAdmiral);
                                if (charactersByRole4.Count <= 0)
                                {
                                    break;
                                }
                                Character character7 = null;
                                for (int m = 0; m < charactersByRole4.Count; m++)
                                {
                                    Character character8 = charactersByRole4[m];
                                    if (character7 == null || character8.GetSkillLevelTotal() > character7.GetSkillLevelTotal())
                                    {
                                        character7 = character8;
                                    }
                                }
                                num4 = character7.GetSkillLevelTotal();
                                obj3 = character7;
                                break;
                            }
                        case RaceVictoryConditionType.MostExperiencedGeneral:
                            {
                                CharacterList charactersByRole3 = empire2.Characters.GetCharactersByRole(CharacterRole.TroopGeneral);
                                if (charactersByRole3.Count <= 0)
                                {
                                    break;
                                }
                                Character character5 = null;
                                for (int l = 0; l < charactersByRole3.Count; l++)
                                {
                                    Character character6 = charactersByRole3[l];
                                    if (character5 == null || character6.GetSkillLevelTotal() > character5.GetSkillLevelTotal())
                                    {
                                        character5 = character6;
                                    }
                                }
                                num4 = character5.GetSkillLevelTotal();
                                obj3 = character5;
                                break;
                            }
                        case RaceVictoryConditionType.MostHomeworlds:
                            num4 = empire2.CountHomeworldsOwned();
                            break;
                        case RaceVictoryConditionType.MostIntelligenceMissionsIntercepted:
                            num4 = empire2.Counters.IntelligenceMissionSuccessCounterIntelligenceCount;
                            break;
                        case RaceVictoryConditionType.MostIntelligenceMissionsSucceed:
                            num4 = empire2.Counters.IntelligenceMissionSuccessEspionageCount + empire2.Counters.IntelligenceMissionSuccessSabotageCount;
                            break;
                        case RaceVictoryConditionType.MostMiningStations:
                            num4 = empire2.MiningStations.Count;
                            break;
                        case RaceVictoryConditionType.MostResortBases:
                            num4 = empire2.ResortBases.Count;
                            break;
                        case RaceVictoryConditionType.MostScientists:
                            num4 = empire2.Characters.CountCharactersByRole(CharacterRole.Scientist);
                            break;
                        case RaceVictoryConditionType.MostSpaceports:
                            num4 = empire2.SpacePorts.Count;
                            break;
                        case RaceVictoryConditionType.MostSubjugatedDominions:
                            num4 = empire2.Counters.SubjugationsMade;
                            break;
                        case RaceVictoryConditionType.MostTimeWarring:
                            num4 = empire2.Counters.TimeSpentAtWar(currentStarDate);
                            break;
                        case RaceVictoryConditionType.MostTourismIncome:
                            num4 = empire2.Counters.TourismIncome;
                            break;
                        case RaceVictoryConditionType.MostTradeIncome:
                            num4 = empire2.Counters.TradeIncomeStateBonus;
                            break;
                        case RaceVictoryConditionType.OldestFreeTradeAgreement:
                            {
                                DiplomaticRelation diplomaticRelation4 = empire2.DiplomaticRelations.FindOldestRelationByType(DiplomaticRelationType.FreeTradeAgreement);
                                if (diplomaticRelation4 != null)
                                {
                                    num4 = diplomaticRelation4.StartDateOfLastChange;
                                    obj3 = diplomaticRelation4;
                                }
                                break;
                            }
                        case RaceVictoryConditionType.OldestMutualDefensePact:
                            {
                                DiplomaticRelation diplomaticRelation3 = empire2.DiplomaticRelations.FindOldestRelationByType(DiplomaticRelationType.MutualDefensePact);
                                if (diplomaticRelation3 != null)
                                {
                                    num4 = diplomaticRelation3.StartDateOfLastChange;
                                    obj3 = diplomaticRelation3;
                                }
                                break;
                            }
                        case RaceVictoryConditionType.OwnLargestCapitalShip:
                            {
                                BuiltObject builtObject2 = empire2.LargestCapitalShip();
                                if (builtObject2 != null)
                                {
                                    num4 = builtObject2.Size;
                                    obj3 = builtObject2;
                                }
                                break;
                            }
                        case RaceVictoryConditionType.PopulationHappiest:
                            num4 = empire2.AverageHappiness();
                            break;
                        case RaceVictoryConditionType.PopulationHighest:
                            num4 = empire2.TotalPopulation;
                            break;
                        case RaceVictoryConditionType.ResearchLeastAdvanced:
                            num4 = empire2.Research.TechTree.CalculateTotalCostResearchedProjects();
                            break;
                        case RaceVictoryConditionType.ResearchMostAdvanced:
                            num4 = empire2.Research.TechTree.CalculateTotalCostResearchedProjects();
                            break;
                        case RaceVictoryConditionType.ResearchMostCompletedBranches:
                            num4 = empire2.Research.TechTree.CountCompletedCategories();
                            break;
                        case RaceVictoryConditionType.ResearchMostCompletedBranchesByIndustry:
                            if (condition.AdditionalData is IndustryType)
                            {
                                IndustryType industry2 = (IndustryType)condition.AdditionalData;
                                num4 = empire2.Research.TechTree.CountCompletedCategories(industry2);
                            }
                            break;
                        case RaceVictoryConditionType.MineMostResourcesLuxury:
                            num4 = empire2.Counters.MiningExtractionLuxury;
                            break;
                        case RaceVictoryConditionType.MineMostResourcesStrategic:
                            num4 = empire2.Counters.MiningExtractionGas + empire2.Counters.MiningExtractionStrategic;
                            break;
                        case RaceVictoryConditionType.MineMostResourcesColonyManufactured:
                            num4 = empire2.Counters.MiningExtractionColonyManufactured;
                            break;
                        case RaceVictoryConditionType.BuildMostMilitaryShips:
                            num4 = empire2.Counters.BuildMilitaryShipCount;
                            break;
                        case RaceVictoryConditionType.BuildMostCivilianShips:
                            num4 = empire2.Counters.BuildCivilianShipCount;
                            break;
                        case RaceVictoryConditionType.BuildMostBases:
                            num4 = empire2.Counters.BuildBaseCount;
                            break;
                    }
                    if (!list.Contains(num4))
                    {
                        list.Add(num4);
                    }
                    switch (condition.Type)
                    {
                        case RaceVictoryConditionType.ControlMostRuins:
                        case RaceVictoryConditionType.PopulationHighest:
                        case RaceVictoryConditionType.PopulationHappiest:
                        case RaceVictoryConditionType.MostHomeworlds:
                        case RaceVictoryConditionType.OwnLargestCapitalShip:
                        case RaceVictoryConditionType.MostSpaceports:
                        case RaceVictoryConditionType.MostMiningStations:
                        case RaceVictoryConditionType.MostResortBases:
                        case RaceVictoryConditionType.DestroyMostShips:
                        case RaceVictoryConditionType.DestroyMostTroops:
                        case RaceVictoryConditionType.DestroyMostCreaturesByType:
                        case RaceVictoryConditionType.MostIntelligenceMissionsSucceed:
                        case RaceVictoryConditionType.MostIntelligenceMissionsIntercepted:
                        case RaceVictoryConditionType.ConquerMostEnemyColonies:
                        case RaceVictoryConditionType.ExterminateOrEnslaveMostPopulation:
                        case RaceVictoryConditionType.MostScientists:
                        case RaceVictoryConditionType.MostExperiencedAdmiral:
                        case RaceVictoryConditionType.MostExperiencedGeneral:
                        case RaceVictoryConditionType.ResearchMostAdvanced:
                        case RaceVictoryConditionType.ResearchMostCompletedBranches:
                        case RaceVictoryConditionType.ResearchMostCompletedBranchesByIndustry:
                        case RaceVictoryConditionType.HighestTradeVolume:
                        case RaceVictoryConditionType.MostTourismIncome:
                        case RaceVictoryConditionType.MostTradeIncome:
                        case RaceVictoryConditionType.HighestPrivateRevenue:
                        case RaceVictoryConditionType.LargestMilitary:
                        case RaceVictoryConditionType.LargestMilitaryNonAllied:
                        case RaceVictoryConditionType.MostTroops:
                        case RaceVictoryConditionType.MostTroopsNonAllied:
                        case RaceVictoryConditionType.MostTimeWarring:
                        case RaceVictoryConditionType.MostSubjugatedDominions:
                        case RaceVictoryConditionType.ExploreMostSystems:
                        case RaceVictoryConditionType.MineMostResourcesLuxury:
                        case RaceVictoryConditionType.MineMostResourcesStrategic:
                        case RaceVictoryConditionType.BuildMostMilitaryShips:
                        case RaceVictoryConditionType.BuildMostCivilianShips:
                        case RaceVictoryConditionType.BuildMostBases:
                        case RaceVictoryConditionType.CaptureMostShips:
                        case RaceVictoryConditionType.PirateBuildMostHiddenBases:
                        case RaceVictoryConditionType.PirateEliminateMostPirateFactions:
                        case RaceVictoryConditionType.PirateMostSuccessfulMissionsAttack:
                        case RaceVictoryConditionType.PirateMostSuccessfulMissionsDefend:
                        case RaceVictoryConditionType.PirateMostSmugglingIncome:
                        case RaceVictoryConditionType.PirateMostProtectionIncome:
                        case RaceVictoryConditionType.PirateMostSuccessfulRaids:
                        case RaceVictoryConditionType.MineMostResourcesColonyManufactured:
                            if (num4 > num2)
                            {
                                bestEmpire = empire2;
                                num2 = num4;
                                num3 = num4;
                                obj2 = obj3;
                            }
                            break;
                        case RaceVictoryConditionType.LoseFewestShips:
                        case RaceVictoryConditionType.LoseFewestTroops:
                        case RaceVictoryConditionType.ResearchLeastAdvanced:
                        case RaceVictoryConditionType.LeastWarsStarted:
                        case RaceVictoryConditionType.LeastBrokenTreaties:
                        case RaceVictoryConditionType.LeastTreaties:
                        case RaceVictoryConditionType.LeastTimeWarring:
                            if (num4 < num2)
                            {
                                bestEmpire = empire2;
                                num2 = num4;
                                obj2 = obj3;
                            }
                            if (num4 > num3)
                            {
                                num3 = num4;
                            }
                            break;
                        case RaceVictoryConditionType.OldestMutualDefensePact:
                        case RaceVictoryConditionType.OldestFreeTradeAgreement:
                            if (num4 != 0.0 && num4 < num2)
                            {
                                bestEmpire = empire2;
                                num2 = num4;
                                obj2 = obj3;
                            }
                            if (num4 > num3)
                            {
                                num3 = num4;
                            }
                            break;
                    }
                }
                double result = 0.0;
                switch (condition.Type)
                {
                    case RaceVictoryConditionType.ControlMostRuins:
                    case RaceVictoryConditionType.PopulationHighest:
                    case RaceVictoryConditionType.PopulationHappiest:
                    case RaceVictoryConditionType.MostSpaceports:
                    case RaceVictoryConditionType.MostMiningStations:
                    case RaceVictoryConditionType.MostResortBases:
                    case RaceVictoryConditionType.DestroyMostShips:
                    case RaceVictoryConditionType.DestroyMostTroops:
                    case RaceVictoryConditionType.DestroyMostCreaturesByType:
                    case RaceVictoryConditionType.MostIntelligenceMissionsSucceed:
                    case RaceVictoryConditionType.MostIntelligenceMissionsIntercepted:
                    case RaceVictoryConditionType.ConquerMostEnemyColonies:
                    case RaceVictoryConditionType.ExterminateOrEnslaveMostPopulation:
                    case RaceVictoryConditionType.MostScientists:
                    case RaceVictoryConditionType.ResearchMostAdvanced:
                    case RaceVictoryConditionType.HighestTradeVolume:
                    case RaceVictoryConditionType.MostTourismIncome:
                    case RaceVictoryConditionType.MostTradeIncome:
                    case RaceVictoryConditionType.HighestPrivateRevenue:
                    case RaceVictoryConditionType.LargestMilitary:
                    case RaceVictoryConditionType.LargestMilitaryNonAllied:
                    case RaceVictoryConditionType.MostTroops:
                    case RaceVictoryConditionType.MostTroopsNonAllied:
                    case RaceVictoryConditionType.MostTimeWarring:
                    case RaceVictoryConditionType.MostSubjugatedDominions:
                    case RaceVictoryConditionType.ExploreMostSystems:
                    case RaceVictoryConditionType.MineMostResourcesLuxury:
                    case RaceVictoryConditionType.MineMostResourcesStrategic:
                    case RaceVictoryConditionType.BuildMostMilitaryShips:
                    case RaceVictoryConditionType.BuildMostCivilianShips:
                    case RaceVictoryConditionType.BuildMostBases:
                    case RaceVictoryConditionType.CaptureMostShips:
                    case RaceVictoryConditionType.PirateBuildMostHiddenBases:
                    case RaceVictoryConditionType.PirateEliminateMostPirateFactions:
                    case RaceVictoryConditionType.PirateMostSuccessfulMissionsAttack:
                    case RaceVictoryConditionType.PirateMostSuccessfulMissionsDefend:
                    case RaceVictoryConditionType.PirateMostSmugglingIncome:
                    case RaceVictoryConditionType.PirateMostProtectionIncome:
                    case RaceVictoryConditionType.PirateMostSuccessfulRaids:
                    case RaceVictoryConditionType.MineMostResourcesColonyManufactured:
                        if (!flag || !(num >= num2))
                        {
                            result = ((!(num2 <= 0.0)) ? (num / num2) : 0.0);
                            break;
                        }
                        result = 1.0;
                        bestEmpire = empire;
                        break;
                    case RaceVictoryConditionType.LoseFewestTroops:
                    case RaceVictoryConditionType.ResearchLeastAdvanced:
                    case RaceVictoryConditionType.LeastTimeWarring:
                    case RaceVictoryConditionType.OldestMutualDefensePact:
                    case RaceVictoryConditionType.OldestFreeTradeAgreement:
                        if (flag && num <= num2)
                        {
                            result = 1.0;
                            bestEmpire = empire;
                        }
                        else
                        {
                            result = (num3 - num) / Math.Max(0.0001, num3 - num2);
                        }
                        break;
                    case RaceVictoryConditionType.MostHomeworlds:
                    case RaceVictoryConditionType.OwnLargestCapitalShip:
                    case RaceVictoryConditionType.MostExperiencedAdmiral:
                    case RaceVictoryConditionType.MostExperiencedGeneral:
                    case RaceVictoryConditionType.ResearchMostCompletedBranches:
                    case RaceVictoryConditionType.ResearchMostCompletedBranchesByIndustry:
                        if (flag)
                        {
                            list.Sort();
                            list.Reverse();
                            switch (list.IndexOf(num))
                            {
                                case 0:
                                    result = 1.0;
                                    bestEmpire = empire;
                                    break;
                                case 1:
                                    result = 0.5;
                                    break;
                                case 2:
                                    result = 0.33;
                                    break;
                                default:
                                    result = 0.0;
                                    break;
                            }
                        }
                        else
                        {
                            result = 0.0;
                        }
                        break;
                    case RaceVictoryConditionType.LoseFewestShips:
                    case RaceVictoryConditionType.LeastWarsStarted:
                    case RaceVictoryConditionType.LeastBrokenTreaties:
                    case RaceVictoryConditionType.LeastTreaties:
                        if (flag)
                        {
                            list.Sort();
                            switch (list.IndexOf(num))
                            {
                                case 0:
                                    result = 1.0;
                                    bestEmpire = empire;
                                    break;
                                case 1:
                                    result = 0.5;
                                    break;
                                case 2:
                                    result = 0.33;
                                    break;
                                default:
                                    result = 0.0;
                                    break;
                            }
                        }
                        else
                        {
                            result = 0.0;
                        }
                        break;
                }
                bool flag2 = true;
                if (bestEmpire != empire)
                {
                    if (bestEmpire == null)
                    {
                        flag2 = false;
                    }
                    else if (empire.PirateEmpireBaseHabitat == null && bestEmpire.PirateEmpireBaseHabitat == null)
                    {
                        DiplomaticRelation diplomaticRelation7 = galaxy.PlayerEmpire.ObtainDiplomaticRelation(bestEmpire);
                        if (diplomaticRelation7.Type == DiplomaticRelationType.NotMet)
                        {
                            flag2 = false;
                        }
                    }
                    else
                    {
                        PirateRelation pirateRelation = galaxy.PlayerEmpire.ObtainPirateRelation(bestEmpire);
                        if (pirateRelation.Type == PirateRelationType.NotMet)
                        {
                            flag2 = false;
                        }
                    }
                }
                if (flag2 && bestEmpire != null)
                {
                    switch (condition.Type)
                    {
                        case RaceVictoryConditionType.PirateBuildMostHiddenBases:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail PirateBuildMostHiddenBases"), bestEmpire.Colonies.CountPirateControlledColoniesWithHiddenPirateBase(bestEmpire).ToString("0"));
                            break;
                        case RaceVictoryConditionType.PirateEliminateMostPirateFactions:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail PirateEliminateMostPirateFactions"), bestEmpire.Counters.EliminatePirateEmpireCount.ToString("0"));
                            break;
                        case RaceVictoryConditionType.PirateMostProtectionIncome:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail PirateMostProtectionIncome"), bestEmpire.Counters.PirateProtectionIncome.ToString("0"));
                            break;
                        case RaceVictoryConditionType.PirateMostSmugglingIncome:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail PirateMostSmugglingIncome"), bestEmpire.Counters.PirateSmugglingIncome.ToString("0"));
                            break;
                        case RaceVictoryConditionType.PirateMostSuccessfulMissionsAttack:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail PirateMostSuccessfulMissionsAttack"), bestEmpire.Counters.CompletedPirateMissionAttackCount.ToString("0"));
                            break;
                        case RaceVictoryConditionType.PirateMostSuccessfulRaids:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail PirateMostSuccessfulRaids"), bestEmpire.Counters.RaidSuccessCount.ToString("0"));
                            break;
                        case RaceVictoryConditionType.PirateMostSuccessfulMissionsDefend:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail PirateMostSuccessfulMissionsDefend"), bestEmpire.Counters.CompletedPirateMissionDefendCount.ToString("0"));
                            break;
                        case RaceVictoryConditionType.CaptureMostShips:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail CaptureMostShips"), bestEmpire.Counters.CaptureShipCount.ToString("0"));
                            break;
                        case RaceVictoryConditionType.ConquerMostEnemyColonies:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail ConquerMostEnemyColonies"), bestEmpire.Counters.ColoniesConqueredCount.ToString("0"));
                            break;
                        case RaceVictoryConditionType.ControlMostRuins:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail ControlMostRuins"), bestEmpire.Colonies.CountHabitatWithRuins().ToString("0"));
                            break;
                        case RaceVictoryConditionType.DestroyMostCreaturesByType:
                            if (condition.AdditionalData is CreatureType)
                            {
                                CreatureType creatureType = (CreatureType)condition.AdditionalData;
                                int num5 = 0;
                                switch (creatureType)
                                {
                                    case CreatureType.SilverMist:
                                        num5 = bestEmpire.Counters.DestroyedCreatureCountSilverMist;
                                        break;
                                    case CreatureType.Ardilus:
                                        num5 = bestEmpire.Counters.DestroyedCreatureCountArdilus;
                                        break;
                                    case CreatureType.DesertSpaceSlug:
                                        num5 = bestEmpire.Counters.DestroyedCreatureCountSandSlug;
                                        break;
                                    case CreatureType.Kaltor:
                                        num5 = bestEmpire.Counters.DestroyedCreatureCountKaltor;
                                        break;
                                    case CreatureType.RockSpaceSlug:
                                        num5 = bestEmpire.Counters.DestroyedCreatureCountSpaceSlug;
                                        break;
                                }
                                detail = string.Format(TextResolver.GetText("Race Victory Condition Detail DestroyMostCreaturesByType"), num5.ToString("0"));
                            }
                            break;
                        case RaceVictoryConditionType.DestroyMostShips:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail DestroyMostShips"), (bestEmpire.Counters.DestroyedEnemyMilitaryShipCount + bestEmpire.Counters.DestroyedEnemyCivilianShipCount).ToString("0"));
                            break;
                        case RaceVictoryConditionType.DestroyMostTroops:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail DestroyMostTroops"), bestEmpire.Counters.DestroyedEnemyTroopCount.ToString("0"));
                            break;
                        case RaceVictoryConditionType.ExploreMostSystems:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail ExploreMostSystems"), bestEmpire.SystemVisibility.CountExploredSystems().ToString("0"));
                            break;
                        case RaceVictoryConditionType.ExterminateOrEnslaveMostPopulation:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail ExterminateOrEnslaveMostPopulation"), bestEmpire.CalculateEnslavedPopulationAmount().ToString("0,,M"), bestEmpire.Counters.ExterminatedPopulationAmount.ToString("0,,M"));
                            break;
                        case RaceVictoryConditionType.HighestPrivateRevenue:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail HighestPrivateRevenue"), bestEmpire.Counters.ColonyPrivateRevenueTotal.ToString("###,###,###,###,##0"));
                            break;
                        case RaceVictoryConditionType.HighestTradeVolume:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail HighestTradeVolume"), bestEmpire.Counters.TradeIncomeTotalVolume.ToString("###,###,###,###,##0"));
                            break;
                        case RaceVictoryConditionType.LargestMilitary:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail LargestMilitary"), bestEmpire.CalculateMilitaryShipSizeTotal().ToString("0,K"));
                            break;
                        case RaceVictoryConditionType.LargestMilitaryNonAllied:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail LargestMilitaryNonAllied"), bestEmpire.CalculateMilitaryShipSizeTotal().ToString("0,K"));
                            break;
                        case RaceVictoryConditionType.MostTroops:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MostTroops"), bestEmpire.Troops.CountTroopsNotRecruiting().ToString("0"));
                            break;
                        case RaceVictoryConditionType.MostTroopsNonAllied:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MostTroopsNonAllied"), bestEmpire.Troops.CountTroopsNotRecruiting().ToString("0"));
                            break;
                        case RaceVictoryConditionType.LeastBrokenTreaties:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail LeastBrokenTreaties"), bestEmpire.Counters.BrokenTreatyCount.ToString("0"));
                            break;
                        case RaceVictoryConditionType.LeastTimeWarring:
                            detail = string.Format(arg0: ((double)bestEmpire.Counters.TimeSpentAtWar(currentStarDate) / ((double)RealSecondsInGalacticYear * 1000.0)).ToString("0.0"), format: TextResolver.GetText("Race Victory Condition Detail LeastTimeWarring"));
                            break;
                        case RaceVictoryConditionType.LeastTreaties:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail LeastTreaties"), bestEmpire.DiplomaticRelations.CountTreaties().ToString("0"));
                            break;
                        case RaceVictoryConditionType.LeastWarsStarted:
                            detail = string.Format(arg0: ((double)bestEmpire.Counters.WarsWeStartedCount).ToString("0"), format: TextResolver.GetText("Race Victory Condition Detail LeastWars"));
                            break;
                        case RaceVictoryConditionType.LoseFewestShips:
                            detail = string.Format(arg0: ((double)(bestEmpire.Counters.LossesCivilianShipCount + bestEmpire.Counters.LossesMilitaryShipCount)).ToString("0"), format: TextResolver.GetText("Race Victory Condition Detail LoseFewestShips"));
                            break;
                        case RaceVictoryConditionType.LoseFewestTroops:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail LoseFewestTroops"), bestEmpire.Counters.LossesTroopCount.ToString("0"));
                            break;
                        case RaceVictoryConditionType.MostExperiencedAdmiral:
                            {
                                CharacterList charactersByRole6 = bestEmpire.Characters.GetCharactersByRole(CharacterRole.FleetAdmiral);
                                Character character11 = null;
                                for (int num7 = 0; num7 < charactersByRole6.Count; num7++)
                                {
                                    Character character12 = charactersByRole6[num7];
                                    if (character11 == null || character12.GetSkillLevelTotal() > character11.GetSkillLevelTotal())
                                    {
                                        character11 = character12;
                                    }
                                }
                                if (character11 != null)
                                {
                                    detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MostExperiencedAdmiral"), character11.Name);
                                }
                                break;
                            }
                        case RaceVictoryConditionType.MostExperiencedGeneral:
                            {
                                CharacterList charactersByRole5 = bestEmpire.Characters.GetCharactersByRole(CharacterRole.TroopGeneral);
                                Character character9 = null;
                                for (int num6 = 0; num6 < charactersByRole5.Count; num6++)
                                {
                                    Character character10 = charactersByRole5[num6];
                                    if (character9 == null || character10.GetSkillLevelTotal() > character9.GetSkillLevelTotal())
                                    {
                                        character9 = character10;
                                    }
                                }
                                if (character9 != null)
                                {
                                    detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MostExperiencedGeneral"), character9.Name);
                                }
                                break;
                            }
                        case RaceVictoryConditionType.MostHomeworlds:
                            {
                                detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MostHomeworlds"), bestEmpire.CountHomeworldsOwned().ToString("0"));
                                HabitatList homeworldsOwned = bestEmpire.GetHomeworldsOwned();
                                string text = string.Empty;
                                for (int n = 0; n < homeworldsOwned.Count; n++)
                                {
                                    text = text + homeworldsOwned[n].Name + ", ";
                                }
                                if (!string.IsNullOrEmpty(text) && text.Length >= 2)
                                {
                                    text = text.Substring(0, text.Length - 2);
                                    detail = detail + ": " + text;
                                }
                                break;
                            }
                        case RaceVictoryConditionType.MostIntelligenceMissionsIntercepted:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MostIntelligenceMissionsIntercepted"), bestEmpire.Counters.IntelligenceMissionSuccessCounterIntelligenceCount.ToString("0"));
                            break;
                        case RaceVictoryConditionType.MostIntelligenceMissionsSucceed:
                            detail = string.Format(arg0: (bestEmpire.Counters.IntelligenceMissionSuccessEspionageCount + bestEmpire.Counters.IntelligenceMissionSuccessSabotageCount).ToString("0"), format: TextResolver.GetText("Race Victory Condition Detail MostIntelligenceMissionsSucceed"));
                            break;
                        case RaceVictoryConditionType.MostMiningStations:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MostMiningStations"), bestEmpire.MiningStations.Count.ToString("0"));
                            break;
                        case RaceVictoryConditionType.MostResortBases:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MostResortBases"), bestEmpire.ResortBases.Count.ToString("0"));
                            break;
                        case RaceVictoryConditionType.MostScientists:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MostScientists"), bestEmpire.Characters.CountCharactersByRole(CharacterRole.Scientist).ToString("0"));
                            break;
                        case RaceVictoryConditionType.MostSpaceports:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MostSpaceports"), bestEmpire.SpacePorts.Count.ToString("0"));
                            break;
                        case RaceVictoryConditionType.MostSubjugatedDominions:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MostSubjugatedDominions"), bestEmpire.DiplomaticRelations.CountSubjugatedDominions().ToString("0"));
                            break;
                        case RaceVictoryConditionType.MostTimeWarring:
                            detail = string.Format(arg0: ((double)bestEmpire.Counters.TimeSpentAtWar(currentStarDate) / ((double)RealSecondsInGalacticYear * 1000.0)).ToString("0.0"), format: TextResolver.GetText("Race Victory Condition Detail MostTimeWarring"));
                            break;
                        case RaceVictoryConditionType.MostTourismIncome:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MostTourismIncome"), bestEmpire.Counters.TourismIncome.ToString("###,###,###,###,##0"));
                            break;
                        case RaceVictoryConditionType.MostTradeIncome:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MostTradeIncome"), bestEmpire.Counters.TradeIncomeStateBonus.ToString("###,###,###,###,##0"));
                            break;
                        case RaceVictoryConditionType.OldestFreeTradeAgreement:
                            {
                                DiplomaticRelation diplomaticRelation9 = bestEmpire.DiplomaticRelations.FindOldestRelationByType(DiplomaticRelationType.FreeTradeAgreement);
                                if (diplomaticRelation9 != null)
                                {
                                    detail = string.Format(TextResolver.GetText("Race Victory Condition Detail OldestFreeTradeAgreement"), diplomaticRelation9.OtherEmpire.Name, ResolveStarDateDescription(diplomaticRelation9.StartDateOfLastChange));
                                }
                                break;
                            }
                        case RaceVictoryConditionType.OldestMutualDefensePact:
                            {
                                DiplomaticRelation diplomaticRelation8 = bestEmpire.DiplomaticRelations.FindOldestRelationByType(DiplomaticRelationType.MutualDefensePact);
                                if (diplomaticRelation8 != null)
                                {
                                    detail = string.Format(TextResolver.GetText("Race Victory Condition Detail OldestMutualDefensePact"), diplomaticRelation8.OtherEmpire.Name, ResolveStarDateDescription(diplomaticRelation8.StartDateOfLastChange));
                                }
                                break;
                            }
                        case RaceVictoryConditionType.OwnLargestCapitalShip:
                            {
                                BuiltObject builtObject3 = bestEmpire.LargestCapitalShip();
                                if (builtObject3 != null)
                                {
                                    detail = string.Format(TextResolver.GetText("Race Victory Condition Detail OwnLargestCapitalShip"), builtObject3.Name, builtObject3.Size.ToString("0"));
                                }
                                break;
                            }
                        case RaceVictoryConditionType.PopulationHappiest:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail PopulationHappiest"), bestEmpire.AverageHappiness().ToString("+0.0;-0.0;0"));
                            break;
                        case RaceVictoryConditionType.PopulationHighest:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail PopulationHighest"), bestEmpire.TotalPopulation.ToString("0,,M"));
                            break;
                        case RaceVictoryConditionType.ResearchLeastAdvanced:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail ResearchLeastAdvanced"), bestEmpire.Research.TechTree.CalculateTotalCostResearchedProjects().ToString("0,K"));
                            break;
                        case RaceVictoryConditionType.ResearchMostAdvanced:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail ResearchMostAdvanced"), bestEmpire.Research.TechTree.CalculateTotalCostResearchedProjects().ToString("0,K"));
                            break;
                        case RaceVictoryConditionType.ResearchMostCompletedBranches:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail ResearchMostCompletedBranches"), bestEmpire.Research.TechTree.CountCompletedCategories().ToString("0"));
                            break;
                        case RaceVictoryConditionType.ResearchMostCompletedBranchesByIndustry:
                            if (condition.AdditionalData is IndustryType)
                            {
                                IndustryType industry3 = (IndustryType)condition.AdditionalData;
                                detail = string.Format(TextResolver.GetText("Race Victory Condition Detail ResearchMostCompletedBranchesByIndustry"), bestEmpire.Research.TechTree.CountCompletedCategories(industry3).ToString("0"), ResolveDescription(industry3));
                            }
                            break;
                        case RaceVictoryConditionType.MineMostResourcesLuxury:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MineMostResourcesLuxury"), bestEmpire.Counters.MiningExtractionLuxury.ToString("0,K"));
                            break;
                        case RaceVictoryConditionType.MineMostResourcesStrategic:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MineMostResourcesStrategic"), (bestEmpire.Counters.MiningExtractionGas + bestEmpire.Counters.MiningExtractionStrategic).ToString("0,K"));
                            break;
                        case RaceVictoryConditionType.MineMostResourcesColonyManufactured:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MineMostResourcesColonyManufactured"), bestEmpire.Counters.MiningExtractionColonyManufactured.ToString("0,K"));
                            break;
                        case RaceVictoryConditionType.BuildMostMilitaryShips:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail BuildMostMilitaryShips"), bestEmpire.Counters.BuildMilitaryShipCount.ToString("0"));
                            break;
                        case RaceVictoryConditionType.BuildMostCivilianShips:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail BuildMostCivilianShips"), bestEmpire.Counters.BuildCivilianShipCount.ToString("0"));
                            break;
                        case RaceVictoryConditionType.BuildMostBases:
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail BuildMostBases"), bestEmpire.Counters.BuildBaseCount.ToString("0"));
                            break;
                    }
                }
                return result;
            }
            return 0.0;
        }

        public static double CalculateRaceVictoryConditionProgress(Galaxy galaxy, Empire empire, RaceVictoryCondition condition, out string detail, out Empire bestEmpire)
        {
            double val = 0.0;
            detail = string.Empty;
            bestEmpire = empire;
            if (galaxy != null && condition != null && empire != null && empire.Counters != null)
            {
                switch (condition.Type)
                {
                    case RaceVictoryConditionType.ControlMostRuins:
                    case RaceVictoryConditionType.PopulationHighest:
                    case RaceVictoryConditionType.PopulationHappiest:
                    case RaceVictoryConditionType.MostHomeworlds:
                    case RaceVictoryConditionType.OwnLargestCapitalShip:
                    case RaceVictoryConditionType.MostSpaceports:
                    case RaceVictoryConditionType.MostMiningStations:
                    case RaceVictoryConditionType.MostResortBases:
                    case RaceVictoryConditionType.DestroyMostShips:
                    case RaceVictoryConditionType.DestroyMostTroops:
                    case RaceVictoryConditionType.DestroyMostCreaturesByType:
                    case RaceVictoryConditionType.LoseFewestShips:
                    case RaceVictoryConditionType.LoseFewestTroops:
                    case RaceVictoryConditionType.MostIntelligenceMissionsSucceed:
                    case RaceVictoryConditionType.MostIntelligenceMissionsIntercepted:
                    case RaceVictoryConditionType.ConquerMostEnemyColonies:
                    case RaceVictoryConditionType.ExterminateOrEnslaveMostPopulation:
                    case RaceVictoryConditionType.MostScientists:
                    case RaceVictoryConditionType.MostExperiencedAdmiral:
                    case RaceVictoryConditionType.MostExperiencedGeneral:
                    case RaceVictoryConditionType.ResearchLeastAdvanced:
                    case RaceVictoryConditionType.ResearchMostAdvanced:
                    case RaceVictoryConditionType.ResearchMostCompletedBranches:
                    case RaceVictoryConditionType.ResearchMostCompletedBranchesByIndustry:
                    case RaceVictoryConditionType.HighestTradeVolume:
                    case RaceVictoryConditionType.MostTourismIncome:
                    case RaceVictoryConditionType.MostTradeIncome:
                    case RaceVictoryConditionType.HighestPrivateRevenue:
                    case RaceVictoryConditionType.LargestMilitary:
                    case RaceVictoryConditionType.LargestMilitaryNonAllied:
                    case RaceVictoryConditionType.MostTroops:
                    case RaceVictoryConditionType.MostTroopsNonAllied:
                    case RaceVictoryConditionType.LeastWarsStarted:
                    case RaceVictoryConditionType.LeastBrokenTreaties:
                    case RaceVictoryConditionType.LeastTreaties:
                    case RaceVictoryConditionType.MostTimeWarring:
                    case RaceVictoryConditionType.LeastTimeWarring:
                    case RaceVictoryConditionType.MostSubjugatedDominions:
                    case RaceVictoryConditionType.OldestMutualDefensePact:
                    case RaceVictoryConditionType.OldestFreeTradeAgreement:
                    case RaceVictoryConditionType.ExploreMostSystems:
                    case RaceVictoryConditionType.MineMostResourcesLuxury:
                    case RaceVictoryConditionType.MineMostResourcesStrategic:
                    case RaceVictoryConditionType.BuildMostMilitaryShips:
                    case RaceVictoryConditionType.BuildMostCivilianShips:
                    case RaceVictoryConditionType.BuildMostBases:
                    case RaceVictoryConditionType.CaptureMostShips:
                    case RaceVictoryConditionType.PirateBuildMostHiddenBases:
                    case RaceVictoryConditionType.PirateEliminateMostPirateFactions:
                    case RaceVictoryConditionType.PirateMostSuccessfulMissionsAttack:
                    case RaceVictoryConditionType.PirateMostSuccessfulMissionsDefend:
                    case RaceVictoryConditionType.PirateMostSmugglingIncome:
                    case RaceVictoryConditionType.PirateMostProtectionIncome:
                    case RaceVictoryConditionType.PirateMostSuccessfulRaids:
                    case RaceVictoryConditionType.MineMostResourcesColonyManufactured:
                        val = RaceVictoryConditionMetCompareEmpires(galaxy, empire, condition, out detail, out bestEmpire);
                        break;
                    case RaceVictoryConditionType.BuildWonder:
                        {
                            if (!(condition.AdditionalData is PlanetaryFacilityDefinition))
                            {
                                break;
                            }
                            PlanetaryFacilityDefinition planetaryFacilityDefinition = (PlanetaryFacilityDefinition)condition.AdditionalData;
                            bool flag = false;
                            if (empire.Colonies != null)
                            {
                                for (int l = 0; l < empire.Colonies.Count; l++)
                                {
                                    PlanetaryFacility planetaryFacility = empire.Colonies[l].Facilities.FindWonderByType(planetaryFacilityDefinition.WonderType);
                                    if (planetaryFacility != null && planetaryFacility.ConstructionProgress >= 1f)
                                    {
                                        detail = empire.Colonies[l].Name;
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                            if (flag)
                            {
                                val = 1.0;
                            }
                            break;
                        }
                    case RaceVictoryConditionType.ControlHomeworld:
                        if (empire.HomeWorld != null && !empire.HomeWorld.HasBeenDestroyed && empire.HomeWorld.Empire == empire)
                        {
                            detail = empire.HomeWorld.Name;
                            val = 1.0;
                        }
                        else if (empire.HomeWorld != null && empire.HomeWorld.Empire != null)
                        {
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail ControlHomeworld Other"), empire.HomeWorld.Name, empire.HomeWorld.Empire.Name);
                        }
                        break;
                    case RaceVictoryConditionType.ControlLargestColoniesByType:
                        {
                            if (!(condition.AdditionalData is HabitatType))
                            {
                                break;
                            }
                            string text = string.Empty;
                            HabitatType type = (HabitatType)condition.AdditionalData;
                            HabitatList habitatList = galaxy.DetermineLargestColoniesByType(type);
                            int num6 = (int)condition.Amount;
                            int num7 = 0;
                            for (int m = 0; m < num6; m++)
                            {
                                if (m < habitatList.Count && habitatList[m].Empire == empire)
                                {
                                    text = text + habitatList[m].Name + ", ";
                                    num7++;
                                }
                            }
                            if (!string.IsNullOrEmpty(text) && text.Length >= 2)
                            {
                                text = text.Substring(0, text.Length - 2);
                            }
                            detail = text;
                            val = (double)num7 / (double)num6;
                            break;
                        }
                    case RaceVictoryConditionType.ControlPlanetTypePercentage:
                        if (condition.AdditionalData is HabitatType)
                        {
                            HabitatType type2 = (HabitatType)condition.AdditionalData;
                            int num19 = galaxy.CountColoniesByType(type2);
                            int num20 = empire.Colonies.CountByType(type2);
                            double num21 = (double)num20 / Math.Max(1.0, num19);
                            val = num21 / (condition.Amount / 100.0);
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail ControlPlanetTypePercentage"), num20.ToString("0"), ResolveDescription(type2), num21.ToString("0%"));
                        }
                        break;
                    case RaceVictoryConditionType.PirateControlColoniesPercentage:
                        {
                            int num4 = Math.Max(1, galaxy.CountPirateControlledColonies());
                            int count = empire.Colonies.Count;
                            double num5 = (double)count / (double)num4;
                            val = num5 / (condition.Amount / 100.0);
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail PirateControlColoniesPercentage"), count.ToString("0"), num5.ToString("0%"));
                            break;
                        }
                    case RaceVictoryConditionType.PirateBuildHiddenFortress:
                        {
                            for (int j = 0; j < empire.Colonies.Count; j++)
                            {
                                Habitat habitat = empire.Colonies[j];
                                if (habitat != null && !habitat.HasBeenDestroyed && habitat.Facilities != null)
                                {
                                    PirateColonyControl byFaction = habitat.GetPirateControl().GetByFaction(empire);
                                    if (byFaction != null && byFaction.HasFacilityControl && habitat.Facilities.CountCompletedByType(PlanetaryFacilityType.PirateFortress) > 0)
                                    {
                                        val = 1.0;
                                        detail = habitat.Name;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    case RaceVictoryConditionType.PirateBuildCriminalNetwork:
                        {
                            for (int n = 0; n < empire.Colonies.Count; n++)
                            {
                                Habitat habitat2 = empire.Colonies[n];
                                if (habitat2 != null && !habitat2.HasBeenDestroyed && habitat2.Facilities != null)
                                {
                                    PirateColonyControl byFaction2 = habitat2.GetPirateControl().GetByFaction(empire);
                                    if (byFaction2 != null && byFaction2.HasFacilityControl && habitat2.Facilities.CountCompletedByType(PlanetaryFacilityType.PirateCriminalNetwork) > 0)
                                    {
                                        val = 1.0;
                                        detail = habitat2.Name;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    case RaceVictoryConditionType.ControlRestrictedResourceSupply:
                        {
                            HabitatList habitatsWithRestrictedResources = empire.Colonies.GetHabitatsWithRestrictedResources();
                            for (int k = 0; k < habitatsWithRestrictedResources.Count; k++)
                            {
                                detail = detail + habitatsWithRestrictedResources[k].Name + ", ";
                            }
                            if (!string.IsNullOrEmpty(detail) && detail.Length >= 2)
                            {
                                detail = detail.Substring(0, detail.Length - 2);
                            }
                            val = (double)habitatsWithRestrictedResources.Count / condition.Amount;
                            break;
                        }
                    case RaceVictoryConditionType.DestroyMoreEnemyTroopsThanLoseTimesFactor:
                        {
                            double amount2 = condition.Amount;
                            if ((double)empire.Counters.DestroyedEnemyTroopCount > (double)empire.Counters.LossesTroopCount * amount2)
                            {
                                val = 1.0;
                            }
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail DestroyMoreEnemyTroopsThanLoseTimesFactor"), empire.Counters.DestroyedEnemyTroopCount.ToString("0"), empire.Counters.LossesTroopCount.ToString("0"));
                            break;
                        }
                    case RaceVictoryConditionType.DestroyMoreShipsThanLoseTimesFactor:
                        {
                            double amount = condition.Amount;
                            double num17 = (double)empire.Counters.DestroyedEnemyMilitaryShipCount + (double)empire.Counters.DestroyedEnemyCivilianShipCount;
                            double num18 = (double)empire.Counters.LossesMilitaryShipCount + (double)empire.Counters.LossesCivilianShipCount;
                            if (num17 > num18 * amount)
                            {
                                val = 1.0;
                            }
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail DestroyMoreShipsThanLoseTimesFactor"), num17.ToString("0"), num18.ToString("0"));
                            break;
                        }
                    case RaceVictoryConditionType.EnslavePopulationProportionEmpire:
                        {
                            double num15 = empire.CalculateEnslavedPopulationAmount();
                            double num16 = num15 / (double)empire.TotalPopulation;
                            val = num16 / (condition.Amount / 100.0);
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail EnslavePopulationProportionEmpire"), num15.ToString("0,,M"), empire.TotalPopulation.ToString("0,,M"));
                            break;
                        }
                    case RaceVictoryConditionType.ExploreGalaxyPercentage:
                        {
                            int num13 = empire.SystemVisibility.CountExploredSystems();
                            double num14 = Math.Max(0.0, Math.Min(1.0, (double)num13 / (double)galaxy.Systems.Count));
                            val = num14 / (condition.Amount / 100.0);
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail ExploreGalaxyPercentage"), num13.ToString("0"), galaxy.Systems.Count.ToString("0"));
                            break;
                        }
                    case RaceVictoryConditionType.FreeTradeAgreementsFormedProportionAllEmpires:
                        {
                            int num8 = empire.DiplomaticRelations.CountRelationsByType(DiplomaticRelationType.FreeTradeAgreement);
                            int num9 = empire.DiplomaticRelations.CountRelationsByType(DiplomaticRelationType.MutualDefensePact);
                            int num10 = empire.DiplomaticRelations.CountRelationsByType(DiplomaticRelationType.Protectorate);
                            int num11 = num8 + num9 + num10;
                            double num12 = (double)num11 / (double)(galaxy.Empires.Count - 1);
                            val = num12 / (condition.Amount / 100.0);
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail FreeTradeAgreementsFormedProportionAllEmpires"), num11.ToString("0"));
                            break;
                        }
                    case RaceVictoryConditionType.KeepLeaderAlive:
                        if (empire.Characters != null)
                        {
                            CharacterList charactersByRole = empire.Characters.GetCharactersByRole(CharacterRole.Leader);
                            if (charactersByRole.Count > 0)
                            {
                                val = 1.0;
                            }
                            for (int i = 0; i < charactersByRole.Count; i++)
                            {
                                detail = detail + charactersByRole[i].Name + ", ";
                            }
                            if (!string.IsNullOrEmpty(detail) && detail.Length >= 2)
                            {
                                detail = detail.Substring(0, detail.Length - 2);
                            }
                        }
                        break;
                    case RaceVictoryConditionType.MutualDefensePactsFormedProportionAllEmpires:
                        {
                            int num = empire.DiplomaticRelations.CountRelationsByType(DiplomaticRelationType.MutualDefensePact);
                            int num2 = Math.Max(1, galaxy.Empires.Count - 1);
                            double num3 = (double)num / (double)num2;
                            val = num3 / (condition.Amount / 100.0);
                            detail = string.Format(TextResolver.GetText("Race Victory Condition Detail MutualDefensePactsFormedProportionAllEmpires"), num.ToString("0"));
                            break;
                        }
                }
            }
            return Math.Max(0.0, Math.Min(1.0, val));
        }






        private Ruin GenerateRuin(Habitat habitat, string name, int pictureRef, RuinType type)
        {
            DetermineHabitatSystemStar(habitat);
            SelectRelativeHabitatSurfacePoint(habitat, out var x, out var y);
            Ruin ruin = new Ruin(name, pictureRef, 0.1 + Rnd.NextDouble() * 0.2, x, y, 0, 0, 0);
            ruin.Type = type;
            return ruin;
        }
    }
}
